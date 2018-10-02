using System;
using SoftEther.VPNServerRpc;

namespace DNT_VPN_JSONRPC
{
    class Program
    {
        static void Main(string[] args)
        {
            VpnServerRpc r = new VpnServerRpc("127.0.0.1", 88, "", "");
        }
    }
}
