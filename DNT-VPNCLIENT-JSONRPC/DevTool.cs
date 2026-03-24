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

namespace SoftEther.VPNClientRpc
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
            List<string> rpc_seqs = new List<string>();
            SortedList<string, (string FuncName, string TypeName, string OrigFuncName, Ref<string> Comment)> funcs = new SortedList<string, (string FuncName, string TypeName, string OrigFuncName, Ref<string> Comment)>();
            Dictionary<string, string> type_name_conv_table = new Dictionary<string, string>();
            Dictionary<string, CStruct> struct_defs = new Dictionary<string, CStruct>();

            string client_c = File.ReadAllText(@"c:\sec\VPN4\current\VPN4\Main\Cedar\Client.c");
            StringReader client_c_reader = new StringReader(client_c);

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

            // --- [TAG_260324_EL_06_TXZ9L] ここから ---
            {
                // 旧形式 DECLARE_RPC / DECLARE_RPC_EX と、
                // 新形式 CiRpcDispatch の直書き if / else if の両方を解釈する。
                // 既存互換性を壊さないため、まず旧形式を読み、その後に新形式も読む。

                // ===== 旧形式: DECLARE_RPC / DECLARE_RPC_EX =====
                while (true)
                {
                    string line = client_c_reader.ReadLine();
                    if (line == null) break;

                    line = line.Trim();

                    if (line.StartsWith("DECLARE_RPC_EX", StringComparison.Ordinal) ||
                        line.StartsWith("DECLARE_RPC", StringComparison.Ordinal))
                    {
                        int i = line.IndexOf("\"", StringComparison.Ordinal);
                        if (i != -1)
                        {
                            string s1 = line.Substring(i + 1);

                            i = s1.IndexOf("\"", StringComparison.Ordinal);
                            if (i != -1)
                            {
                                string s2 = s1.Substring(i + 1);

                                string func_name = s1.Substring(0, i).Trim();

                                s2 = s2.Trim();

                                if (s2.StartsWith(",", StringComparison.Ordinal))
                                {
                                    s2 = s2.Substring(1).Trim();

                                    i = s2.IndexOf(",", StringComparison.Ordinal);
                                    if (i != -1)
                                    {
                                        string type_name_orig = s2.Substring(0, i).Trim();
                                        string type_name = TypeNameConv(type_name_orig);

                                        string s3 = s2.Substring(i + 1);

                                        i = s3.IndexOf(",", StringComparison.Ordinal);

                                        string orig_func_name = (i == -1 ? s3 : s3.Substring(0, i)).Trim();

                                        // 同名 RPC の二重追加を防ぎつつ、順序は最初の出現順を維持する
                                        if (funcs.ContainsKey(func_name) == false)
                                        {
                                            rpc_seqs.Add(func_name);
                                        }

                                        funcs[func_name] = (func_name, type_name, orig_func_name, new Ref<string>(""));
                                        type_name_conv_table[type_name] = type_name_orig;
                                    }
                                }
                            }
                        }
                    }
                }

                // ===== 新形式: CiRpcDispatch の if / else if (StrCmpi(name, "...") == 0) =====
                int dispatchFuncIndex = client_c.IndexOf("PACK *CiRpcDispatch(", StringComparison.Ordinal);
                if (dispatchFuncIndex < 0)
                {
                    dispatchFuncIndex = client_c.IndexOf("CiRpcDispatch(", StringComparison.Ordinal);
                }

                if (dispatchFuncIndex >= 0)
                {
                    string clientDispatchBody = null;

                    // CiRpcDispatch 関数本体のみを簡易的に切り出す
                    int dispatchBodyStart = client_c.IndexOf('{', dispatchFuncIndex);
                    if (dispatchBodyStart >= 0)
                    {
                        int braceDepth = 0;
                        int dispatchBodyEnd = -1;

                        for (int i = dispatchBodyStart; i < client_c.Length; i++)
                        {
                            char ch = client_c[i];

                            if (ch == '{')
                            {
                                braceDepth++;
                            }
                            else if (ch == '}')
                            {
                                braceDepth--;
                                if (braceDepth == 0)
                                {
                                    dispatchBodyEnd = i;
                                    break;
                                }
                            }
                        }

                        if (dispatchBodyEnd > dispatchBodyStart)
                        {
                            clientDispatchBody = client_c.Substring(dispatchBodyStart + 1, dispatchBodyEnd - dispatchBodyStart - 1);
                        }
                    }

                    if (string.IsNullOrEmpty(clientDispatchBody) == false)
                    {
                        StringReader dispatchReader = new StringReader(clientDispatchBody);

                        string currentRpcName = null;
                        StringWriter currentRpcBody = null;
                        int currentRpcBraceDepth = 0;
                        bool currentRpcBlockStarted = false;

                        while (true)
                        {
                            string rawLine = dispatchReader.ReadLine();
                            if (rawLine == null)
                            {
                                break;
                            }

                            string line = rawLine.Trim();

                            // まだ RPC ブロック収集中でない場合は、条件式行を探す
                            if (string.IsNullOrEmpty(currentRpcName))
                            {
                                int condIndex = line.IndexOf("StrCmpi(name, \"", StringComparison.Ordinal);
                                if (condIndex >= 0)
                                {
                                    int quote1 = line.IndexOf('"', condIndex);
                                    int quote2 = quote1 >= 0 ? line.IndexOf('"', quote1 + 1) : -1;

                                    if (quote1 >= 0 && quote2 > quote1)
                                    {
                                        currentRpcName = line.Substring(quote1 + 1, quote2 - quote1 - 1);
                                        currentRpcBody = new StringWriter();
                                        currentRpcBraceDepth = 0;
                                        currentRpcBlockStarted = false;

                                        // 万一 if (...) { のように同一行でブロック開始していても追従する
                                        string lineWithoutComment = rawLine;
                                        int lineCommentIndex = lineWithoutComment.IndexOf("//", StringComparison.Ordinal);
                                        if (lineCommentIndex >= 0)
                                        {
                                            lineWithoutComment = lineWithoutComment.Substring(0, lineCommentIndex);
                                        }

                                        foreach (char ch in lineWithoutComment)
                                        {
                                            if (ch == '{')
                                            {
                                                currentRpcBraceDepth++;
                                                currentRpcBlockStarted = true;
                                            }
                                            else if (ch == '}')
                                            {
                                                currentRpcBraceDepth--;
                                            }
                                        }

                                        if (currentRpcBlockStarted)
                                        {
                                            currentRpcBody.WriteLine(rawLine);
                                        }
                                    }
                                }

                                continue;
                            }

                            // 収集中の RPC ブロック本文を蓄積する
                            currentRpcBody.WriteLine(rawLine);

                            string rawLineWithoutComment = rawLine;
                            int commentIndex = rawLineWithoutComment.IndexOf("//", StringComparison.Ordinal);
                            if (commentIndex >= 0)
                            {
                                rawLineWithoutComment = rawLineWithoutComment.Substring(0, commentIndex);
                            }

                            foreach (char ch in rawLineWithoutComment)
                            {
                                if (ch == '{')
                                {
                                    currentRpcBraceDepth++;
                                    currentRpcBlockStarted = true;
                                }
                                else if (ch == '}')
                                {
                                    currentRpcBraceDepth--;
                                }
                            }

                            // 1 RPC 分のブロックが閉じたら、その中から型名と Ct 実装関数名を抽出する
                            if (currentRpcBlockStarted && currentRpcBraceDepth <= 0)
                            {
                                string type_name_orig = "";
                                string variableName = "";
                                string orig_func_name = "";

                                // 先頭付近の宣言行 (例: RPC_CLIENT_VERSION a;) から型名と変数名を拾う
                                StringReader rpcBodyReader = new StringReader(currentRpcBody.ToString());
                                while (true)
                                {
                                    string blockLine = rpcBodyReader.ReadLine();
                                    if (blockLine == null)
                                    {
                                        break;
                                    }

                                    int lineCommentIndex = blockLine.IndexOf("//", StringComparison.Ordinal);
                                    if (lineCommentIndex >= 0)
                                    {
                                        blockLine = blockLine.Substring(0, lineCommentIndex);
                                    }

                                    string blockLineTrimmed = blockLine.Trim();
                                    if (string.IsNullOrEmpty(blockLineTrimmed))
                                    {
                                        continue;
                                    }

                                    // 宣言行だけを対象とする
                                    if (blockLineTrimmed.IndexOf("(", StringComparison.Ordinal) != -1 ||
                                        blockLineTrimmed.IndexOf(")", StringComparison.Ordinal) != -1)
                                    {
                                        continue;
                                    }

                                    if (blockLineTrimmed.EndsWith(";", StringComparison.Ordinal) == false)
                                    {
                                        continue;
                                    }

                                    string work = blockLineTrimmed.Substring(0, blockLineTrimmed.Length - 1).Replace("*", " ");
                                    string[] tokens = work.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                                    if (tokens.Length == 2)
                                    {
                                        type_name_orig = tokens[0].Trim();
                                        variableName = tokens[1].Trim();
                                        break;
                                    }
                                }

                                // 取得した変数名 (a / t など) を使って CtXXXX(...) 呼び出し行を特定する
                                if (string.IsNullOrEmpty(variableName) == false)
                                {
                                    rpcBodyReader = new StringReader(currentRpcBody.ToString());

                                    while (true)
                                    {
                                        string blockLine = rpcBodyReader.ReadLine();
                                        if (blockLine == null)
                                        {
                                            break;
                                        }

                                        int lineCommentIndex = blockLine.IndexOf("//", StringComparison.Ordinal);
                                        if (lineCommentIndex >= 0)
                                        {
                                            blockLine = blockLine.Substring(0, lineCommentIndex);
                                        }

                                        string blockLineTrimmed = blockLine.Trim();
                                        if (string.IsNullOrEmpty(blockLineTrimmed))
                                        {
                                            continue;
                                        }

                                        if (blockLineTrimmed.IndexOf("&" + variableName, StringComparison.Ordinal) == -1)
                                        {
                                            continue;
                                        }

                                        int searchPos = 0;

                                        while (true)
                                        {
                                            int ctIndex = blockLineTrimmed.IndexOf("Ct", searchPos, StringComparison.Ordinal);
                                            if (ctIndex < 0)
                                            {
                                                break;
                                            }

                                            bool isIdentifierHead =
                                                (ctIndex == 0) ||
                                                (
                                                    (blockLineTrimmed[ctIndex - 1] >= 'A' && blockLineTrimmed[ctIndex - 1] <= 'Z') == false &&
                                                    (blockLineTrimmed[ctIndex - 1] >= 'a' && blockLineTrimmed[ctIndex - 1] <= 'z') == false &&
                                                    (blockLineTrimmed[ctIndex - 1] >= '0' && blockLineTrimmed[ctIndex - 1] <= '9') == false &&
                                                    blockLineTrimmed[ctIndex - 1] != '_'
                                                );

                                            if (isIdentifierHead)
                                            {
                                                int parenIndex = blockLineTrimmed.IndexOf("(", ctIndex, StringComparison.Ordinal);
                                                if (parenIndex > ctIndex)
                                                {
                                                    string candidate = blockLineTrimmed.Substring(ctIndex, parenIndex - ctIndex).Trim();
                                                    if (candidate.StartsWith("Ct", StringComparison.Ordinal))
                                                    {
                                                        orig_func_name = candidate;
                                                        break;
                                                    }
                                                }
                                            }

                                            searchPos = ctIndex + 2;
                                        }

                                        if (string.IsNullOrEmpty(orig_func_name) == false)
                                        {
                                            break;
                                        }
                                    }
                                }

                                // 型名が取れたものだけ funcs に登録する
                                if (string.IsNullOrEmpty(type_name_orig) == false)
                                {
                                    string type_name = TypeNameConv(type_name_orig);

                                    if (funcs.ContainsKey(currentRpcName) == false)
                                    {
                                        rpc_seqs.Add(currentRpcName);
                                    }

                                    funcs[currentRpcName] = (currentRpcName, type_name, orig_func_name, new Ref<string>(""));
                                    type_name_conv_table[type_name] = type_name_orig;

                                    Console.WriteLine(currentRpcName);
                                }

                                // 次の RPC ブロック解析へ進む
                                currentRpcName = null;
                                currentRpcBody = null;
                                currentRpcBraceDepth = 0;
                                currentRpcBlockStarted = false;
                            }
                        }
                    }
                }
            }
            // --- [TAG_260324_EL_06_TXZ9L] ここまで ---

            //funcs.Print();
            //type_name_conv_table.ObjectToJson().Print();

            //return;

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

            StringWriter test_gen = new StringWriter();

            StringWriter testcall_gen = new StringWriter();

            foreach (string func_name in rpc_seqs)
            {
                var item = funcs[func_name];

                if (string.IsNullOrEmpty(item.Comment.Value) == false)
                    funcs_gen.WriteLine($"/// <summary>\r\n/// {item.Comment.Value} (Async mode)\r\n/// </summary>");
                else
                    funcs_gen.WriteLine($"/// <summary>\r\n/// TODO (Async mode)\r\n/// </summary>");
                funcs_gen.WriteLine($"public async Task<{item.TypeName}> {item.FuncName}Async({item.TypeName} input_param) => await Call<{item.TypeName}>(\"{item.FuncName}\", input_param);");
                funcs_gen.WriteLine();
                if (string.IsNullOrEmpty(item.Comment.Value) == false)
                    funcs_gen.WriteLine($"/// <summary>\r\n/// {item.Comment.Value} (Sync mode)\r\n/// </summary>");
                else
                    funcs_gen.WriteLine($"/// <summary>\r\n/// TODO (Sync mode)\r\n/// </summary>");
                funcs_gen.WriteLine($"public {item.TypeName} {item.FuncName}({item.TypeName} input_param) => {item.FuncName}Async(input_param).Result;");
                funcs_gen.WriteLine();

                var st = struct_defs[item.TypeName];

                if (structs_exists.Contains(st.OrigName) == false)
                {
                    structs_exists.Add(st.OrigName);
                    structs_gen.WriteLine(st.ToString());
                }

                test_gen.WriteLine($"/// <summary>");
                test_gen.WriteLine($"/// API test for '{func_name}', {(string.IsNullOrEmpty(item.Comment.Value) ? "TODO" : item.Comment.Value)}");
                test_gen.WriteLine($"/// </summary>");
                test_gen.WriteLine($"public void Test_{func_name}()");
                test_gen.WriteLine("{");
                test_gen.WriteLine($"    Console.WriteLine(\"Begin: Test_{func_name}\");");
                test_gen.WriteLine($"    ");
                test_gen.WriteLine($"    {st.Name} in_{st.OrigName.ToLowerInvariant()} = new {st.Name}()");
                test_gen.WriteLine("    {");
                test_gen.WriteLine("    };");
                test_gen.WriteLine($"    {st.Name} out_{st.OrigName.ToLowerInvariant()} = Rpc.{func_name}(in_{st.OrigName.ToLowerInvariant()});");
                test_gen.WriteLine($"    ");
                test_gen.WriteLine($"    print_object(out_{st.OrigName.ToLowerInvariant()});");
                test_gen.WriteLine("    ");
                test_gen.WriteLine($"    Console.WriteLine(\"End: Test_{func_name}\");");
                test_gen.WriteLine($"    Console.WriteLine(\"-----\");");
                test_gen.WriteLine($"    Console.WriteLine();");
                test_gen.WriteLine("}");
                test_gen.WriteLine();

                testcall_gen.WriteLine($"Test_{func_name}();");
            }

            string code_all_funcs = funcs_gen.ToString();

            string test_all = test_gen.ToString();

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
            File.WriteAllText(Path.Combine(dir, "test.cs"), test_all);
            File.WriteAllText(Path.Combine(dir, "testcall.cs"), testcall_gen.ToString());
        }
    }
}

