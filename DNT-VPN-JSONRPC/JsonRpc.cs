﻿using System;
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

namespace SoftEther.JsonRpc
{
    static class ClientUtil
    {
        public const int DefaultMaxDepth = 8;

        public static string NonNull(this string s) { if (s == null) return ""; else return s; }
        public static bool IsEmpty(this string str)
        {
            if (str == null || str.Trim().Length == 0)
                return true;
            else
                return false;
        }
        public static bool IsFilled(this string str) => !IsEmpty(str);

        public static string ObjectToJson(this object obj, bool include_null = false, bool escape_html = false, int? max_depth = ClientUtil.DefaultMaxDepth, bool compact = false, bool reference_handling = false) => ClientUtil.Serialize(obj, include_null, escape_html, max_depth, compact, reference_handling);
        public static T JsonToObject<T>(this string str, bool include_null = false, int? max_depth = ClientUtil.DefaultMaxDepth) => ClientUtil.Deserialize<T>(str, include_null, max_depth);
        public static object JsonToObject(this string str, Type type, bool include_null = false, int? max_depth = ClientUtil.DefaultMaxDepth) => ClientUtil.Deserialize(str, type, include_null, max_depth);

        public static string Serialize(object obj, bool include_null = false, bool escape_html = false, int? max_depth = ClientUtil.DefaultMaxDepth, bool compact = false, bool reference_handling = false)
        {
            JsonSerializerSettings setting = new JsonSerializerSettings()
            {
                MaxDepth = max_depth,
                NullValueHandling = include_null ? NullValueHandling.Include : NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Error,
                PreserveReferencesHandling = reference_handling ? PreserveReferencesHandling.All : PreserveReferencesHandling.None,
                StringEscapeHandling = escape_html ? StringEscapeHandling.EscapeHtml : StringEscapeHandling.Default,

            };
            return JsonConvert.SerializeObject(obj, compact ? Formatting.None : Formatting.Indented, setting);
        }

        public static T Deserialize<T>(string str, bool include_null = false, int? max_depth = ClientUtil.DefaultMaxDepth)
            => (T)Deserialize(str, typeof(T), include_null, max_depth);

        public static object Deserialize(string str, Type type, bool include_null = false, int? max_depth = ClientUtil.DefaultMaxDepth)
        {
            JsonSerializerSettings setting = new JsonSerializerSettings()
            {
                MaxDepth = max_depth,
                NullValueHandling = include_null ? NullValueHandling.Include : NullValueHandling.Ignore,
                ObjectCreationHandling = ObjectCreationHandling.Replace,
                ReferenceLoopHandling = ReferenceLoopHandling.Error,
            };
            return JsonConvert.DeserializeObject(str, type, setting);
        }

        public static void Print(this object o)
        {
            string str = o.ObjectToJson();

            if (o is string) str = (string)o;

            Console.WriteLine(str);
        }
    }

    class JsonRpcException : Exception
    {
        public JsonRpcError RpcError { get; }
        public JsonRpcException(JsonRpcError err)
            : base($"Code={err.Code}, Message={err.Message.NonNull()}" +
                  (err == null || err.Data == null ? "" : $", Data={err.Data.ObjectToJson(compact: true)}"))
        {
            this.RpcError = err;
        }
    }

    class JsonRpcRequest
    {
        [JsonProperty("jsonrpc")]
        public string Version { get; set; } = "2.0";

        [JsonProperty("method")]
        public string Method { get; set; } = "";

        [JsonProperty("params")]
        public object Params { get; set; } = null;

        [JsonProperty("id")]
        public string Id { get; set; } = null;

        public JsonRpcRequest() { }

        public JsonRpcRequest(string method, object param, string id)
        {
            this.Method = method;
            this.Params = param;
            this.Id = id;
        }
    }

    class JsonRpcResponse<TResult>
    {
        [JsonProperty("jsonrpc")]
        public virtual string Version { get; set; } = "2.0";

        [JsonProperty("result")]
        public virtual TResult Result { get; set; } = default(TResult);

        [JsonProperty("error")]
        public virtual JsonRpcError Error { get; set; } = null;

        [JsonProperty("id", NullValueHandling = NullValueHandling.Include)]
        public virtual string Id { get; set; } = null;

        [JsonIgnore]
        public virtual bool IsError => this.Error != null;

        [JsonIgnore]
        public virtual bool IsOk => !IsError;

        public virtual void ThrowIfError()
        {
            if (this.IsError) throw new JsonRpcException(this.Error);
        }

        public override string ToString()
        {
            return this.ObjectToJson(compact: true);
        }
    }

    class JsonRpcError
    {
        public JsonRpcError() { }
        public JsonRpcError(int code, string message, object data = null)
        {
            this.Code = code;
            this.Message = message.NonNull();
            if (this.Message.IsEmpty()) this.Message = $"JSON-RPC Error {code}";
            this.Data = data;
        }

        [JsonProperty("code")]
        public int Code { get; set; } = 0;

        [JsonProperty("message")]
        public string Message { get; set; } = null;

        [JsonProperty("data")]
        public object Data { get; set; } = null;
    }

    class JsonRpcClient
    {
        HttpClientHandler client_handler;
        HttpClient client;
        public const int DefaultTimeoutMsecs = 60 * 1000;
        public int TimeoutMsecs { get => (int)client.Timeout.TotalMilliseconds; set => client.Timeout = new TimeSpan(0, 0, 0, 0, value); }
        public Dictionary<string, string> HttpHeaders { get; } = new Dictionary<string, string>();

        string base_url;

        public JsonRpcClient(string url, Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, bool> cert_check_proc = null)
        {
            if (cert_check_proc == null) cert_check_proc = (message, cert, chain, errors) => true;
            client_handler = new HttpClientHandler();

            this.client_handler.AllowAutoRedirect = true;
            this.client_handler.MaxAutomaticRedirections = 10;

            client_handler.ServerCertificateCustomValidationCallback = cert_check_proc;

            client = new HttpClient(client_handler, true);
            //Console.WriteLine("new HttpClient(client_handler, true);");

            this.base_url = url;

            this.TimeoutMsecs = DefaultTimeoutMsecs;
        }

        public async Task<string> CallInternalAsync(string method_name, object param)
        {
            string id = DateTime.Now.Ticks.ToString();

            JsonRpcRequest req = new JsonRpcRequest(method_name, param, id);

            string req_string = req.ObjectToJson();

            HttpContent content = new StringContent(req_string, Encoding.UTF8, "application/json");

            foreach (string key in this.HttpHeaders.Keys)
            {
                string value = this.HttpHeaders[key];

                content.Headers.Add(key, value);
            }

            HttpResponseMessage response = await this.client.PostAsync(base_url, content);

            Stream responseStream = await response.Content.ReadAsStreamAsync();

            if (!response.IsSuccessStatusCode)
            {
                using (StreamReader streamReader = new StreamReader(responseStream))
                {
                    throw new Exception($"Error: {response.StatusCode}: {await streamReader.ReadToEndAsync()}");
                }
            }

            string ret_string;

            using (StreamReader streamReader = new StreamReader(responseStream))
            {
                ret_string = await streamReader.ReadToEndAsync();
            }

            //Console.WriteLine($"ret: {ret_string}");

            return ret_string;
        }

        public async Task<TResult> CallAsync<TResult>(string method_name, object param)
        {
            string ret_string = await CallInternalAsync(method_name, param);

            JsonRpcResponse <TResult> ret = ret_string.JsonToObject<JsonRpcResponse<TResult>>();

            ret.ThrowIfError();

            return ret.Result;
        }
    }
}

