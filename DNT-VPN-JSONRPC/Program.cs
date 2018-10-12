using System;
using SoftEther.JsonRpc;
using SoftEther.VPNServerRpc;

namespace DNT_VPN_JSONRPC
{
    class Program
    {
        static void Main(string[] args)
        {
            VPNRPCTest test = new VPNRPCTest();

            test.Test_All();

            //Tools.GenCode1();

            //RpcServerInfo a = r.GetServerInfoAsync().Result;

            //a.Print();



            //VpnRpcSetUser a = new VpnRpcSetUser(){

        }
    }
}
