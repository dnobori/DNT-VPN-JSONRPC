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

            VpnRpcServerInfo a = r.GetServerInfo();

            a.Print();

            //RpcServerInfo a = r.GetServerInfoAsync().Result;

            //a.Print();

            //Tools.GenCode1();

            //VpnRpcSetUser a = new VpnRpcSetUser(){
            
        }
    }
}
