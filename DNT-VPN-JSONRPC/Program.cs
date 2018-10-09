using System;
using SoftEther.JsonRpc;
using SoftEther.VPNServerRpc;

namespace DNT_VPN_JSONRPC
{
    class Program
    {
        static void Main(string[] args)
        {
            VpnServerRpc r = new VpnServerRpc("127.0.0.1", 443, "", "");

            RpcServerInfo a = r.GetServerInfoAsync().Result;

            a.Print();

            Tools.GenCode1();
        }
    }
}
