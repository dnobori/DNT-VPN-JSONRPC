using System;
using SoftEther.JsonRpc;
using SoftEther.VPNClientRpc;

#pragma warning disable CS0162 // 到達できないコードが検出されました

namespace DNT_VPN_JSONRPC
{
    class Program
    {
        static void Main(string[] args)
        {
            if (true)
            {
                VPNRPCTest test = new VPNRPCTest();

                int i = 0;

                for (i = 0; i < 1; i++)
                {
                    Console.WriteLine("-------------");
                    Console.WriteLine($"Test #{i} start");
                    test.Test_All();
                    Console.WriteLine($"Test #{i} finish");
                    Console.WriteLine("-------------");
                }

            }
            else
            {
                Tools.GenCode1();

                //RpcServerInfo a = r.GetServerInfoAsync().Result;

                //a.Print();



                //VpnRpcSetUser a = new VpnRpcSetUser(){
            }
        }
    }
}
