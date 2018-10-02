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
    class VpnServerRpc
    {
        class A
        {
            public int a = 1, b = 2, c = 3;
        };
        class B
        {
            public int a = 1;
            public string b = "Hello";
        };

        class rpc_t
        {
            public string Str1;
            public int Int1;
        }

        public VpnServerRpc(string vpnserver_host, int vpnserver_port, string admin_password, string hub_name = null)
        {
            //RpcClient c = new RpcClient(new Uri("http://127.0.0.1:88/rpc/"), transportClient: new VpnServerRpcTransportClient());
            //RpcClient c = new RpcClient(new Uri("https://127.0.0.1:8081/rpc/"), transportClient: new VpnServerRpcTransportClient());
            //var a = c.TransportClient;

            //JsonRpcClient c = new JsonRpcClient("http://127.0.0.1:88/rpc/", null);
            JsonRpcClient c = new JsonRpcClient("https://127.0.0.1:8081/rpc/", null);

            //A a = new A();
            B b = new B();

            rpc_t ret = c.Call<rpc_t>("Test5", b).Result;
            Console.WriteLine($"{ret.Str1}  {ret.Int1}");
        }
    }
}
