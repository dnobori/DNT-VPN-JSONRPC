using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Http;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SoftEther.JsonRpc;

namespace SoftEther.VPNServerRpc
{

    // Ref クラス
    public class Ref<T>
    {
        public Ref() : this(default(T)) { }
        public Ref(T value)
        {
            Value = value;
        }

        public T Value { get; set; }
        public void Set(T value) => this.Value = value;
        public T Get() => this.Value;
        public override string ToString() => Value?.ToString() ?? null;

        public override bool Equals(object obj)
        {
            var @ref = obj as Ref<T>;
            return @ref != null &&
                   EqualityComparer<T>.Default.Equals(Value, @ref.Value);
        }

        public override int GetHashCode()
        {
            return -1937169414 + EqualityComparer<T>.Default.GetHashCode(Value);
        }
    }


    public static class Tools
    {
        public static string TypeNameConv(string name)
        {
            string[] tokens = name.Split('_', StringSplitOptions.RemoveEmptyEntries);

            string ret = "";

            foreach (string token in tokens)
            {
                string s1 = token.Substring(0, 1);
                string s2 = token.Substring(1);

                s1 = s1.ToUpperInvariant();
                s2 = s2.ToLowerInvariant();

                string s = s1 + s2;

                if (s.Equals("L3SW", StringComparison.InvariantCultureIgnoreCase)) s = "L3Sw";
                if (s.Equals("L3IF", StringComparison.InvariantCultureIgnoreCase)) s = "L3If";
                if (s.Equals("L3TABLE", StringComparison.InvariantCultureIgnoreCase)) s = "L3Table";
                if (s.Equals("CA", StringComparison.InvariantCultureIgnoreCase)) s = "CA";

                s = s.Replace("Localbridge", "LocalBridge");
                s = s.Replace("Ipsec", "IPsec");
                s = s.Replace("Etherip", "EtherIp");
                s = s.Replace("Ddns", "DDns");
                s = s.Replace("Openvpn", "OpenVpn");

                ret += s;
            }

            /*if (ret.StartsWith("Rpc") == false)
            {
                ret = "Rpc" + ret;
            }*/

            ret = "Vpn" + ret;

            return ret;
        }

        class CStruct
        {
            public string OrigName = "";
            public string Name = "";

            [JsonIgnore]
            public StringWriter Body = new StringWriter();

            public string BodyStr => Body.ToString();
            public string Comment = "";

            public override string ToString()
            {
                StringWriter w = new StringWriter();

                StringReader r = new StringReader(this.BodyStr);

                if (string.IsNullOrEmpty(Comment) == false)
                {
                    w.WriteLine($"/// <summary>\r\n/// {Comment}\r\n/// </summary>");
                }
                else
                {
                    w.WriteLine($"/// <summary>\r\n/// TODO\r\n///</summary>");
                }

                w.WriteLine($"public class {Name}");
                w.WriteLine("{");

                int num = 0;

                while (true)
                {
                    string line = r.ReadLine();
                    if (line == null)
                    {
                        break;
                    }
                    line = line.Trim();

                    string comment = "";

                    int comment_index = line.IndexOf("//");
                    if (comment_index != -1)
                    {
                        comment = line.Substring(comment_index + 2).Trim();
                        line = line.Substring(0, comment_index).Trim();
                    }

                    string[] tokens = line.Split(new char[] { ' ', '\t', '[', '*', ';', }, StringSplitOptions.RemoveEmptyEntries);

                    if (tokens.Length >= 2)
                    {
                        num++;

                        string type = tokens[0];
                        string name = tokens[1];

                        if (num >= 2)
                        {
                            w.WriteLine();
                        }

                        if (string.IsNullOrEmpty(comment) == false)
                        {
                            w.WriteLine($"    /// <summary>\r\n    /// {comment}\r\n    /// </summary>");
                        }

                        switch (type)
                        {
                            case "char":
                                w.WriteLine($"    public string {name}_str;");
                                break;

                            case "wchar_t":
                                w.WriteLine($"    public string {name}_utf;");
                                break;

                            case "UINT":
                                w.WriteLine($"    public uint {name}_u32;");
                                break;

                            case "UINT64":
                                w.WriteLine($"    public ulong {name}_u64;");
                                break;

                            case "bool":
                                w.WriteLine($"    public bool {name}_bool;");
                                break;

                            case "UCHAR":
                                w.WriteLine($"    public byte[] {name}_bin;");
                                break;

                            case "IP":
                                w.WriteLine($"    public string {name}_ip;");
                                break;

                            default:
                                w.WriteLine($"    // TODO: {line}");
                                break;
                        }
                    }
                }

                w.WriteLine("}");

                return w.ToString();
            }
        }

        public static void GenCode1()
        {
            SortedList<string, (string FuncName, string TypeName, string OrigFuncName, Ref<string> Comment)> funcs = new SortedList<string, (string FuncName, string TypeName, string OrigFuncName, Ref<string> Comment)>();
            Dictionary<string, string> type_name_conv_table = new Dictionary<string, string>();
            Dictionary<string, CStruct> struct_defs = new Dictionary<string, CStruct>();

            string admin_c = File.ReadAllText(@"c:\sec\VPN4\current\VPN4\Main\Cedar\Admin.c");
            StringReader admin_c_reader = new StringReader(admin_c);

            StringWriter all_headers_w = new StringWriter();
            string[] headers = Directory.GetFiles(@"c:\sec\VPN4\current\VPN4\Main", "*.h", SearchOption.AllDirectories);
            foreach (string header_fn in headers)
            {
                string hb = File.ReadAllText(header_fn);

                all_headers_w.WriteLine(hb);
            }

            StringReader all_headers_r = new StringReader(all_headers_w.ToString());

            string last_comment = "";

            CStruct cs = null;

            while (true)
            {
                string line = all_headers_r.ReadLine();
                if (line == null) break;

                if (line.StartsWith("//"))
                {
                    last_comment = line.Substring(2).Trim();
                    continue;
                }

                /*int comment_index = line.IndexOf("//");

                if (comment_index != -1)
                {
                    line = line.Substring(0, comment_index);
                }*/

                string[] tokens = line.Split(new char[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries);

                if (tokens.Length == 2 && tokens[0] == "struct")
                {
                    cs = new CStruct() { OrigName = tokens[1], Name = TypeNameConv(tokens[1]), Comment = last_comment };
                }
                else if (line == "};")
                {
                    if (cs != null)
                    {
                        struct_defs[cs.Name] = cs;
                    }
                    cs = null;
                }
                else
                {
                    if (cs != null)
                    {
                        cs.Body.WriteLine(line);
                    }
                }

                last_comment = "";
            }

            while (true)
            {
                string line = admin_c_reader.ReadLine();
                if (line == null) break;

                line = line.Trim();

                if (line.StartsWith("DECLARE_RPC_EX") || line.StartsWith("DECLARE_RPC"))
                {
                    int i = line.IndexOf("\"");
                    if (i != -1)
                    {
                        string s1 = line.Substring(i + 1);

                        i = s1.IndexOf("\"");
                        if (i != -1)
                        {
                            string s2 = s1.Substring(i + 1);

                            string func_name = s1.Substring(0, i).Trim();

                            //Console.WriteLine(func_name);

                            s2 = s2.Trim();

                            if (s2.StartsWith(","))
                            {
                                s2 = s2.Substring(1).Trim();

                                i = s2.IndexOf(",");

                                string type_name_orig = s2.Substring(0, i);
                                string type_name = TypeNameConv(type_name_orig);


                                //Console.WriteLine(type_name);

                                string s3 = s2.Substring(i + 1);

                                i = s3.IndexOf(",");

                                string orig_func_name = s3.Substring(0, i).Trim();

                                funcs[func_name] = (func_name, type_name, orig_func_name, new Ref<string>(""));

                                type_name_conv_table[type_name] = type_name_orig;
                            }
                        }
                    }
                }
            }

            StringWriter all_sources_w = new StringWriter();
            string[] sources = Directory.GetFiles(@"c:\sec\VPN4\current\VPN4\Main", "*.c", SearchOption.AllDirectories);
            foreach (string source_fn in sources)
            {
                string hb = File.ReadAllText(source_fn);

                all_sources_w.WriteLine(hb);
            }

            StringReader all_sources_r = new StringReader(all_sources_w.ToString());

            last_comment = "";

            while (true)
            {
                string line = all_sources_r.ReadLine();
                if (line == null)
                {
                    break;
                }

                if (line.StartsWith("//"))
                {
                    last_comment = line.Substring(2).Trim();
                    continue;
                }

                int comment_index = line.IndexOf("//");

                if (comment_index != -1)
                {
                    line = line.Substring(0, comment_index);
                }

                string[] tokens = line.Split(new char[] { ' ', '\t', '(', '*', }, StringSplitOptions.RemoveEmptyEntries);

                if (tokens.Length >= 2)
                {
                    if (tokens[1] == "StGetConfig")
                    {
                        Console.WriteLine();
                    }
                    foreach (var t in funcs.Values)
                    {
                        if (t.OrigFuncName == tokens[1])
                        {
                            if (string.IsNullOrEmpty(last_comment) == false)
                            {
                                t.Comment.Set(last_comment);

                                //Console.WriteLine($"{t2.OrigFuncName} = {t2.Comment}");
                            }
                        }
                    }
                }

                last_comment = "";
            }


            StringWriter funcs_gen = new StringWriter();

            StringWriter structs_gen = new StringWriter();

            HashSet<string> structs_exists = new HashSet<string>();

            foreach (var item in funcs.Values)
            {
                if (string.IsNullOrEmpty(item.Comment.Value) == false)
                    funcs_gen.WriteLine($"/// <summary>\r\n/// {item.Comment.Value} (Async mode)\r\n/// </summary>");
                else
                    funcs_gen.WriteLine($"/// <summary>\r\n/// TODO (Async mode)\r\n/// </summary>");
                funcs_gen.WriteLine($"public async Task<{item.TypeName}> {item.FuncName}Async() => await Call<{item.TypeName}>(\"{item.FuncName}\", new {item.TypeName}());");
                funcs_gen.WriteLine();
                if (string.IsNullOrEmpty(item.Comment.Value) == false)
                    funcs_gen.WriteLine($"/// <summary>\r\n/// {item.Comment.Value} (Sync mode)\r\n/// </summary>");
                else
                    funcs_gen.WriteLine($"/// <summary>\r\n/// TODO (Sync mode)\r\n/// </summary>");
                funcs_gen.WriteLine($"public {item.TypeName} {item.FuncName}() => {item.FuncName}Async().Result;");
                funcs_gen.WriteLine();

                var st = struct_defs[item.TypeName];

                if (structs_exists.Contains(st.OrigName) == false)
                {
                    structs_exists.Add(st.OrigName);
                    structs_gen.WriteLine(st.ToString());
                }
            }

            string code_all_funcs = funcs_gen.ToString();

            string code_used_structs = structs_gen.ToString();

            StringWriter structs_gen_all = new StringWriter();

            foreach (var st in struct_defs.Values)
            {
                structs_gen_all.WriteLine(st.ToString());
            }

            string code_all_structs = structs_gen_all.ToString();

            //Console.WriteLine(code_used_structs);

            //struct_defs["RPC_LOCALBRIDGE"].ToString().Print();

            string dir = @"c:\tmp\vpnrpc";
            try { Directory.CreateDirectory(dir); } catch { };

            File.WriteAllText(Path.Combine(dir, "code_all_funcs.cs"), code_all_funcs);
            File.WriteAllText(Path.Combine(dir, "code_used_structs.cs"), code_used_structs);
            File.WriteAllText(Path.Combine(dir, "code_all_structs.cs"), code_all_structs);
        }
    }
}

