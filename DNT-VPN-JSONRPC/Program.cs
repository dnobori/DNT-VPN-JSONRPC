using System;
using SoftEther.JsonRpc;
using SoftEther.VPNServerRpc;
using SoftEther.VPNServerRpc.Types;

namespace DNT_VPN_JSONRPC
{
    class Program
    {
        static void Main(string[] args)
        {
            VpnServerRpc r = new VpnServerRpc("127.0.0.1", 443, "", "");

            RpcServerInfo a = r.Call<RpcServerInfo>("GetServerInfo", new RpcServerInfo()).Result;

            a.Print();
        }
    }
}
