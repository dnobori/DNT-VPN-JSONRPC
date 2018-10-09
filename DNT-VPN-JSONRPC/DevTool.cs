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
    public static class Tools
    {
        public static string TypeNameConv(string name)
        {
            string[] tokens = name.Split('_');

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

            return ret;
        }

        class CStruct
        {
            public string OrigName = "";

            [JsonIgnore]
            public StringWriter Body = new StringWriter();

            public string BodyStr => Body.ToString();
            public string Comment = "";
        }

        public static void GenCode1()
        {
            SortedList<string, (string FuncName, string TypeName, string OrigFuncName, string Comment)> funcs = new SortedList<string, (string FuncName, string TypeName, string OrigFuncName, string Comment)>();
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

                int comment_index = line.IndexOf("//");

                if (comment_index != -1)
                {
                    line = line.Substring(0, comment_index);
                }

                string[] tokens = line.Split(new char[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries);

                if (tokens.Length == 2 && tokens[0] == "struct")
                {
                    cs = new CStruct() { OrigName = tokens[1], Comment = last_comment };
                }
                else if (line == "};")
                {
                    if (cs != null)
                    {
                        struct_defs[cs.OrigName] = cs;
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

                                if (type_name.StartsWith("Rpc") == false)
                                {
                                    type_name = "Rpc" + type_name;
                                }

                                type_name = "Vpn" + type_name;

                                //Console.WriteLine(type_name);

                                string s3 = s2.Substring(i + 1);

                                i = s3.IndexOf(",");

                                string orig_func_name = s3.Substring(0, i).Trim();

                                funcs[func_name] = (func_name, type_name, orig_func_name, "");

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
                                var t2 = t;
                                t2.Comment = last_comment;

                                Console.WriteLine($"{t2.OrigFuncName} = {t2.Comment}");
                            }
                        }
                    }
                }

                last_comment = "";
            }


            StringWriter funcs_gen = new StringWriter();

            foreach (var item in funcs.Values)
            {
                funcs_gen.WriteLine($"public async Task<{item.TypeName}> {item.FuncName}Async() => await Call<{item.TypeName}>(\"{item.FuncName}\", new {item.TypeName}());");
                funcs_gen.WriteLine($"public {item.TypeName} {item.FuncName}() => {item.FuncName}Async().Result;");
                funcs_gen.WriteLine();
            }

            Console.WriteLine(funcs_gen.ToString());
        }
    }
}

