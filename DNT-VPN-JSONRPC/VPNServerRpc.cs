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
    public class VpnServerRpc
    {
        JsonRpcClient rpc_client;

        public VpnServerRpc(string vpnserver_host, int vpnserver_port, string admin_password, string hub_name = null)
        {
            rpc_client = new JsonRpcClient($"https://{vpnserver_host}:{vpnserver_port }/api/", null);

            /*
            LABEL_A:
            A a = new A();

            try
            {
                RpcServerInfo ret = c.Call<RpcServerInfo>("GetServerInfo", a).Result;
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

            }
            Console.ReadLine();

            goto LABEL_A;
            */
        }

        public async Task<T> Call<T>(string method_name, T request)
        {
            T response = await rpc_client.Call<T>(method_name, request);

            return response;
        }

        public async Task<RpcServerInfo> GetServerInfoAsync() => await Call<RpcServerInfo>("GetServerInfo", new RpcServerInfo());
        public RpcServerInfo GetServerInfo() => GetServerInfoAsync().Result;
    }
}
