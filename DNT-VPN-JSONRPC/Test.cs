using System;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json;
using SoftEther.JsonRpc;
using SoftEther.VPNServerRpc;

namespace DNT_VPN_JSONRPC
{
    class VPNRPCTest
    {
        VpnServerRpc Rpc;

        Random rand = new Random();

        public VPNRPCTest()
        {
            Rpc = new VpnServerRpc("127.0.0.1", 443, "", "");
        }

        public void Test_All()
        {
            if (false)
            {
                Test_Test();

                Test_GetServerInfo();
                Test_GetServerStatus();

                uint new_listener_port = Test_CreateListener();
                Test_EnableListener(new_listener_port, false);
                Test_EnumListener();
                Test_EnableListener(new_listener_port, true);
                Test_EnumListener();
                Test_DeleteListener(new_listener_port);

                Test_SetServerPassword();

                if (false)
                {
                    Test_GetFarmSetting();

                    Test_SetFarmSetting();

                    VpnRpcEnumFarm farm_members = Test_EnumFarmMember();

                    foreach (VpnRpcEnumFarmItem farm_member in farm_members.FarmMemberList)
                    {
                        Test_GetFarmInfo(farm_member.Id_u32);
                    }

                    Test_GetFarmConnectionStatus();
                }

                Test_GetServerCert();

            }

            Test_SetServerCert();

            return;
            Test_GetServerCipher();
            Test_SetServerCipher();
            Test_CreateHub();
            Test_SetHub();
            Test_GetHub();
            Test_EnumHub();
            Test_DeleteHub();
            Test_GetHubRadius();
            Test_SetHubRadius();
            Test_EnumConnection();
            Test_DisconnectConnection();
            Test_GetConnectionInfo();
            Test_SetHubOnline();
            Test_GetHubStatus();
            Test_SetHubLog();
            Test_GetHubLog();
            Test_AddCa();
            Test_EnumCa();
            Test_GetCa();
            Test_DeleteCa();
            Test_SetLinkOnline();
            Test_SetLinkOffline();
            Test_DeleteLink();
            Test_RenameLink();
            Test_CreateLink();
            Test_GetLink();
            Test_SetLink();
            Test_EnumLink();
            Test_GetLinkStatus();
            Test_AddAccess();
            Test_DeleteAccess();
            Test_EnumAccess();
            Test_SetAccessList();
            Test_CreateUser();
            Test_SetUser();
            Test_GetUser();
            Test_DeleteUser();
            Test_EnumUser();
            Test_CreateGroup();
            Test_SetGroup();
            Test_GetGroup();
            Test_DeleteGroup();
            Test_EnumGroup();
            Test_EnumSession();
            Test_GetSessionStatus();
            Test_DeleteSession();
            Test_EnumMacTable();
            Test_DeleteMacTable();
            Test_EnumIpTable();
            Test_DeleteIpTable();
            Test_SetKeep();
            Test_GetKeep();
            Test_EnableSecureNAT();
            Test_DisableSecureNAT();
            Test_SetSecureNATOption();
            Test_GetSecureNATOption();
            Test_EnumNAT();
            Test_EnumDHCP();
            Test_GetSecureNATStatus();
            Test_EnumEthernet();
            Test_AddLocalBridge();
            Test_DeleteLocalBridge();
            Test_EnumLocalBridge();
            Test_GetBridgeSupport();
            Test_RebootServer();
            Test_GetCaps();
            Test_GetConfig();
            Test_SetConfig();
            Test_GetDefaultHubAdminOptions();
            Test_GetHubAdminOptions();
            Test_SetHubAdminOptions();
            Test_GetHubExtOptions();
            Test_SetHubExtOptions();
            Test_AddL3Switch();
            Test_DelL3Switch();
            Test_EnumL3Switch();
            Test_StartL3Switch();
            Test_StopL3Switch();
            Test_AddL3If();
            Test_DelL3If();
            Test_EnumL3If();
            Test_AddL3Table();
            Test_DelL3Table();
            Test_EnumL3Table();
            Test_EnumCrl();
            Test_AddCrl();
            Test_DelCrl();
            Test_GetCrl();
            Test_SetCrl();
            Test_SetAcList();
            Test_GetAcList();
            Test_EnumLogFile();
            Test_ReadLogFile();
            Test_AddLicenseKey();
            Test_DelLicenseKey();
            Test_EnumLicenseKey();
            Test_GetLicenseStatus();
            Test_SetSysLog();
            Test_GetSysLog();
            Test_EnumEthVLan();
            Test_SetEnableEthVLan();
            Test_SetHubMsg();
            Test_GetHubMsg();
            Test_Crash();
            Test_GetAdminMsg();
            Test_Flush();
            Test_Debug();
            Test_SetIPsecServices();
            Test_GetIPsecServices();
            Test_AddEtherIpId();
            Test_GetEtherIpId();
            Test_DeleteEtherIpId();
            Test_EnumEtherIpId();
            Test_SetOpenVpnSstpConfig();
            Test_GetOpenVpnSstpConfig();
            Test_GetDDnsClientStatus();
            Test_ChangeDDnsClientHostname();
            Test_RegenerateServerCert();
            Test_MakeOpenVpnConfigFile();
            Test_SetSpecialListener();
            Test_GetSpecialListener();
            Test_GetAzureStatus();
            Test_SetAzureStatus();
            Test_GetDDnsInternetSettng();
            Test_SetDDnsInternetSettng();
            Test_SetVgsConfig();
            Test_GetVgsConfig();
        }


        /// <summary>
        /// API test for 'Test', test RPC function
        /// </summary>
        public void Test_Test()
        {
            Console.WriteLine("Begin: Test_Test");

            VpnRpcTest a = new VpnRpcTest() { IntValue_u32 = (uint)DateTime.Now.Millisecond };

            VpnRpcTest b = Rpc.Test(a);

            Debug.Assert(a.IntValue_u32.ToString() == b.StrValue_str);

            Console.WriteLine("End: Test_Test");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'GetServerInfo', Get server information
        /// </summary>
        public void Test_GetServerInfo()
        {
            Console.WriteLine("Begin: Test_GetServerInfo");

            VpnRpcServerInfo info = Rpc.GetServerInfo();

            print_object(info);

            Console.WriteLine("End: Test_GetServerInfo");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'GetServerStatus', Get server status
        /// </summary>
        public void Test_GetServerStatus()
        {
            Console.WriteLine("Begin: Test_GetServerStatus");

            VpnRpcServerStatus out_rpc_server_status = Rpc.GetServerStatus();

            print_object(out_rpc_server_status);

            Console.WriteLine("End: Test_GetServerStatus");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'CreateListener', Create a listener
        /// </summary>
        public uint Test_CreateListener()
        {
            Console.WriteLine("Begin: Test_CreateListener");

            uint port = (uint)rand.Next(1025, 65534);

            Console.WriteLine($"Creating a new listener port: Port {port}");
            VpnRpcListener in_rpc_listener = new VpnRpcListener() { Enable_bool = true, Port_u32 = port, };
            VpnRpcListener out_rpc_listener = Rpc.CreateListener(in_rpc_listener);

            Console.WriteLine("Done.");
            Console.WriteLine("End: Test_CreateListener");
            Console.WriteLine("-----");
            Console.WriteLine();

            return port;
        }

        /// <summary>
        /// API test for 'EnumListener', Enumerating listeners
        /// </summary>
        public void Test_EnumListener()
        {
            Console.WriteLine("Begin: Test_EnumListener");

            VpnRpcListenerList out_rpc_listener_list = Rpc.EnumListener();

            print_object(out_rpc_listener_list);

            Console.WriteLine("End: Test_EnumListener");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'DeleteListener', Delete a listener
        /// </summary>
        public void Test_DeleteListener(uint port)
        {
            Console.WriteLine("Begin: Test_DeleteListener");

            Console.WriteLine($"Deleting a new listener port: Port {port}");
            VpnRpcListener in_rpc_listener = new VpnRpcListener() { Port_u32 = port };
            VpnRpcListener out_rpc_listener = Rpc.DeleteListener(in_rpc_listener);

            Console.WriteLine("Done.");
            Console.WriteLine("End: Test_DeleteListener");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'EnableListener', Enable / Disable listener
        /// </summary>
        public void Test_EnableListener(uint port, bool enabled)
        {
            Console.WriteLine("Begin: Test_EnableListener");

            if (enabled)
            {
                Console.WriteLine($"Enabling listener port = {port}");
            }
            else
            {
                Console.WriteLine($"Disabling listener port = {port}");
            }

            VpnRpcListener in_rpc_listener = new VpnRpcListener() { Port_u32 = port, Enable_bool = enabled };
            VpnRpcListener out_rpc_listener = Rpc.EnableListener(in_rpc_listener);

            Console.WriteLine("Done.");

            Console.WriteLine("End: Test_EnableListener");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'SetServerPassword', Set server password
        /// </summary>
        public void Test_SetServerPassword()
        {
            string password = "microsoft";

            Console.WriteLine("Begin: Test_SetServerPassword");

            Console.WriteLine($"Set the server administrator password to '{password}'.");

            VpnRpcSetPassword in_rpc_set_password = new VpnRpcSetPassword() { PlainTextPassword_str = password };
            VpnRpcSetPassword out_rpc_set_password = Rpc.SetServerPassword(in_rpc_set_password);

            Console.WriteLine("Done.");

            Console.WriteLine("End: Test_SetServerPassword");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'SetFarmSetting', Set clustering configuration
        /// </summary>
        public void Test_SetFarmSetting()
        {
            Console.WriteLine("Begin: Test_SetFarmSetting");

            VpnRpcFarm in_rpc_farm = new VpnRpcFarm()
            {
                ServerType_u32 = VpnRpcServerType.FarmController,
                NumPort_u32 = 2,
                Ports_u32 = new uint[] { 443, 444, 445 },
                PublicIp_ip = "1.2.3.4",
                ControllerName_str = "controller",
                MemberPasswordPlaintext_str = "microsoft",
                ControllerPort_u32 = 443,
                Weight_u32 = 100,
                ControllerOnly_bool = false,
            };

            VpnRpcFarm out_rpc_farm = Rpc.SetFarmSetting(in_rpc_farm);

            Console.WriteLine("End: Test_SetFarmSetting");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'GetFarmSetting', Get clustering configuration
        /// </summary>
        public void Test_GetFarmSetting()
        {
            Console.WriteLine("Begin: Test_GetFarmSetting");

            // VpnRpcFarm in_rpc_farm = new VpnRpcFarm();
            VpnRpcFarm out_rpc_farm = Rpc.GetFarmSetting();

            print_object(out_rpc_farm);

            Console.WriteLine("End: Test_GetFarmSetting");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'GetFarmInfo', Get cluster member information
        /// </summary>
        public void Test_GetFarmInfo(uint id)
        {
            Console.WriteLine("Begin: Test_GetFarmInfo");

            VpnRpcFarmInfo in_rpc_farm_info = new VpnRpcFarmInfo() { Id_u32 = id };
            VpnRpcFarmInfo out_rpc_farm_info = Rpc.GetFarmInfo(in_rpc_farm_info);

            print_object(out_rpc_farm_info);

            Console.WriteLine("End: Test_GetFarmInfo");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'EnumFarmMember', Enumerate cluster members
        /// </summary>
        public VpnRpcEnumFarm Test_EnumFarmMember()
        {
            Console.WriteLine("Begin: Test_EnumFarmMember");

            VpnRpcEnumFarm out_rpc_enum_farm = Rpc.EnumFarmMember();

            print_object(out_rpc_enum_farm);

            Console.WriteLine("End: Test_EnumFarmMember");
            Console.WriteLine("-----");
            Console.WriteLine();

            return out_rpc_enum_farm;
        }

        /// <summary>
        /// API test for 'GetFarmConnectionStatus', Get status of connection to cluster controller
        /// </summary>
        public void Test_GetFarmConnectionStatus()
        {
            Console.WriteLine("Begin: Test_GetFarmConnectionStatus");

            VpnRpcFarmConnectionStatus out_rpc_farm_connection_status = Rpc.GetFarmConnectionStatus();

            print_object(out_rpc_farm_connection_status);

            Console.WriteLine("End: Test_GetFarmConnectionStatus");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'SetServerCert', Set the server certification
        /// </summary>
        public void Test_SetServerCert()
        {
            Console.WriteLine("Begin: Test_SetServerCert");

            VpnRpcKeyPair in_rpc_key_pair = new VpnRpcKeyPair()
            {
                Cert_bin = File.ReadAllBytes(@"c:\tmp\a.cer"),
                Key_bin = File.ReadAllBytes(@"c:\tmp\a.key"),
            };

            VpnRpcKeyPair out_rpc_key_pair = Rpc.SetServerCert(in_rpc_key_pair);

            print_object(out_rpc_key_pair);

            Console.WriteLine("End: Test_SetServerCert");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'GetServerCert', Get the server certification
        /// </summary>
        public void Test_GetServerCert()
        {
            Console.WriteLine("Begin: Test_GetServerCert");

            VpnRpcKeyPair out_rpc_key_pair = Rpc.GetServerCert();

            print_object(out_rpc_key_pair);

            Console.WriteLine("End: Test_GetServerCert");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'GetServerCipher', Get cipher for SSL
        /// </summary>
        public void Test_GetServerCipher()
        {
            Console.WriteLine("Begin: Test_GetServerCipher");

            // VpnRpcStr in_rpc_str = new VpnRpcStr();
            VpnRpcStr out_rpc_str = Rpc.GetServerCipher();

            print_object(out_rpc_str);

            Console.WriteLine("End: Test_GetServerCipher");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'SetServerCipher', Set cipher for SSL to the server
        /// </summary>
        public void Test_SetServerCipher()
        {
            Console.WriteLine("Begin: Test_SetServerCipher");

            // VpnRpcStr in_rpc_str = new VpnRpcStr();
            VpnRpcStr out_rpc_str = Rpc.SetServerCipher();

            print_object(out_rpc_str);

            Console.WriteLine("End: Test_SetServerCipher");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'CreateHub', Create a hub
        /// </summary>
        public void Test_CreateHub()
        {
            Console.WriteLine("Begin: Test_CreateHub");

            // VpnRpcCreateHub in_rpc_create_hub = new VpnRpcCreateHub();
            VpnRpcCreateHub out_rpc_create_hub = Rpc.CreateHub();

            print_object(out_rpc_create_hub);

            Console.WriteLine("End: Test_CreateHub");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'SetHub', Set hub configuration
        /// </summary>
        public void Test_SetHub()
        {
            Console.WriteLine("Begin: Test_SetHub");

            // VpnRpcCreateHub in_rpc_create_hub = new VpnRpcCreateHub();
            VpnRpcCreateHub out_rpc_create_hub = Rpc.SetHub();

            print_object(out_rpc_create_hub);

            Console.WriteLine("End: Test_SetHub");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'GetHub', Get hub configuration
        /// </summary>
        public void Test_GetHub()
        {
            Console.WriteLine("Begin: Test_GetHub");

            // VpnRpcCreateHub in_rpc_create_hub = new VpnRpcCreateHub();
            VpnRpcCreateHub out_rpc_create_hub = Rpc.GetHub();

            print_object(out_rpc_create_hub);

            Console.WriteLine("End: Test_GetHub");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'EnumHub', Enumerate hubs
        /// </summary>
        public void Test_EnumHub()
        {
            Console.WriteLine("Begin: Test_EnumHub");

            // VpnRpcEnumHub in_rpc_enum_hub = new VpnRpcEnumHub();
            VpnRpcEnumHub out_rpc_enum_hub = Rpc.EnumHub();

            print_object(out_rpc_enum_hub);

            Console.WriteLine("End: Test_EnumHub");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'DeleteHub', Delete a hub
        /// </summary>
        public void Test_DeleteHub()
        {
            Console.WriteLine("Begin: Test_DeleteHub");

            // VpnRpcDeleteHub in_rpc_delete_hub = new VpnRpcDeleteHub();
            VpnRpcDeleteHub out_rpc_delete_hub = Rpc.DeleteHub();

            print_object(out_rpc_delete_hub);

            Console.WriteLine("End: Test_DeleteHub");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'GetHubRadius', Get Radius options of the hub
        /// </summary>
        public void Test_GetHubRadius()
        {
            Console.WriteLine("Begin: Test_GetHubRadius");

            // VpnRpcRadius in_rpc_radius = new VpnRpcRadius();
            VpnRpcRadius out_rpc_radius = Rpc.GetHubRadius();

            print_object(out_rpc_radius);

            Console.WriteLine("End: Test_GetHubRadius");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'SetHubRadius', Set Radius options of the hub
        /// </summary>
        public void Test_SetHubRadius()
        {
            Console.WriteLine("Begin: Test_SetHubRadius");

            // VpnRpcRadius in_rpc_radius = new VpnRpcRadius();
            VpnRpcRadius out_rpc_radius = Rpc.SetHubRadius();

            print_object(out_rpc_radius);

            Console.WriteLine("End: Test_SetHubRadius");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'EnumConnection', Enumerate connections
        /// </summary>
        public void Test_EnumConnection()
        {
            Console.WriteLine("Begin: Test_EnumConnection");

            // VpnRpcEnumConnection in_rpc_enum_connection = new VpnRpcEnumConnection();
            VpnRpcEnumConnection out_rpc_enum_connection = Rpc.EnumConnection();

            print_object(out_rpc_enum_connection);

            Console.WriteLine("End: Test_EnumConnection");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'DisconnectConnection', Disconnect a connection
        /// </summary>
        public void Test_DisconnectConnection()
        {
            Console.WriteLine("Begin: Test_DisconnectConnection");

            // VpnRpcDisconnectConnection in_rpc_disconnect_connection = new VpnRpcDisconnectConnection();
            VpnRpcDisconnectConnection out_rpc_disconnect_connection = Rpc.DisconnectConnection();

            print_object(out_rpc_disconnect_connection);

            Console.WriteLine("End: Test_DisconnectConnection");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'GetConnectionInfo', Get connection information
        /// </summary>
        public void Test_GetConnectionInfo()
        {
            Console.WriteLine("Begin: Test_GetConnectionInfo");

            // VpnRpcConnectionInfo in_rpc_connection_info = new VpnRpcConnectionInfo();
            VpnRpcConnectionInfo out_rpc_connection_info = Rpc.GetConnectionInfo();

            print_object(out_rpc_connection_info);

            Console.WriteLine("End: Test_GetConnectionInfo");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'SetHubOnline', Make a hub on-line or off-line
        /// </summary>
        public void Test_SetHubOnline()
        {
            Console.WriteLine("Begin: Test_SetHubOnline");

            // VpnRpcSetHubOnline in_rpc_set_hub_online = new VpnRpcSetHubOnline();
            VpnRpcSetHubOnline out_rpc_set_hub_online = Rpc.SetHubOnline();

            print_object(out_rpc_set_hub_online);

            Console.WriteLine("End: Test_SetHubOnline");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'GetHubStatus', Get hub status
        /// </summary>
        public void Test_GetHubStatus()
        {
            Console.WriteLine("Begin: Test_GetHubStatus");

            // VpnRpcHubStatus in_rpc_hub_status = new VpnRpcHubStatus();
            VpnRpcHubStatus out_rpc_hub_status = Rpc.GetHubStatus();

            print_object(out_rpc_hub_status);

            Console.WriteLine("End: Test_GetHubStatus");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'SetHubLog', Set logging configuration into the hub
        /// </summary>
        public void Test_SetHubLog()
        {
            Console.WriteLine("Begin: Test_SetHubLog");

            // VpnRpcHubLog in_rpc_hub_log = new VpnRpcHubLog();
            VpnRpcHubLog out_rpc_hub_log = Rpc.SetHubLog();

            print_object(out_rpc_hub_log);

            Console.WriteLine("End: Test_SetHubLog");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'GetHubLog', Get logging configuration of the hub
        /// </summary>
        public void Test_GetHubLog()
        {
            Console.WriteLine("Begin: Test_GetHubLog");

            // VpnRpcHubLog in_rpc_hub_log = new VpnRpcHubLog();
            VpnRpcHubLog out_rpc_hub_log = Rpc.GetHubLog();

            print_object(out_rpc_hub_log);

            Console.WriteLine("End: Test_GetHubLog");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'AddCa', Add CA(Certificate Authority) into the hub
        /// </summary>
        public void Test_AddCa()
        {
            Console.WriteLine("Begin: Test_AddCa");

            // VpnRpcHubAddCA in_rpc_hub_add_ca = new VpnRpcHubAddCA();
            VpnRpcHubAddCA out_rpc_hub_add_ca = Rpc.AddCa();

            print_object(out_rpc_hub_add_ca);

            Console.WriteLine("End: Test_AddCa");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'EnumCa', Enumerate CA(Certificate Authority) in the hub
        /// </summary>
        public void Test_EnumCa()
        {
            Console.WriteLine("Begin: Test_EnumCa");

            // VpnRpcHubEnumCA in_rpc_hub_enum_ca = new VpnRpcHubEnumCA();
            VpnRpcHubEnumCA out_rpc_hub_enum_ca = Rpc.EnumCa();

            print_object(out_rpc_hub_enum_ca);

            Console.WriteLine("End: Test_EnumCa");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'GetCa', Get CA(Certificate Authority) setting from the hub
        /// </summary>
        public void Test_GetCa()
        {
            Console.WriteLine("Begin: Test_GetCa");

            // VpnRpcHubGetCA in_rpc_hub_get_ca = new VpnRpcHubGetCA();
            VpnRpcHubGetCA out_rpc_hub_get_ca = Rpc.GetCa();

            print_object(out_rpc_hub_get_ca);

            Console.WriteLine("End: Test_GetCa");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'DeleteCa', Delete a CA(Certificate Authority) setting from the hub
        /// </summary>
        public void Test_DeleteCa()
        {
            Console.WriteLine("Begin: Test_DeleteCa");

            // VpnRpcHubDeleteCA in_rpc_hub_delete_ca = new VpnRpcHubDeleteCA();
            VpnRpcHubDeleteCA out_rpc_hub_delete_ca = Rpc.DeleteCa();

            print_object(out_rpc_hub_delete_ca);

            Console.WriteLine("End: Test_DeleteCa");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'SetLinkOnline', Make a link into on-line
        /// </summary>
        public void Test_SetLinkOnline()
        {
            Console.WriteLine("Begin: Test_SetLinkOnline");

            // VpnRpcLink in_rpc_link = new VpnRpcLink();
            VpnRpcLink out_rpc_link = Rpc.SetLinkOnline();

            print_object(out_rpc_link);

            Console.WriteLine("End: Test_SetLinkOnline");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'SetLinkOffline', Make a link into off-line
        /// </summary>
        public void Test_SetLinkOffline()
        {
            Console.WriteLine("Begin: Test_SetLinkOffline");

            // VpnRpcLink in_rpc_link = new VpnRpcLink();
            VpnRpcLink out_rpc_link = Rpc.SetLinkOffline();

            print_object(out_rpc_link);

            Console.WriteLine("End: Test_SetLinkOffline");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'DeleteLink', Delete a link
        /// </summary>
        public void Test_DeleteLink()
        {
            Console.WriteLine("Begin: Test_DeleteLink");

            // VpnRpcLink in_rpc_link = new VpnRpcLink();
            VpnRpcLink out_rpc_link = Rpc.DeleteLink();

            print_object(out_rpc_link);

            Console.WriteLine("End: Test_DeleteLink");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'RenameLink', Rename link (cascade connection)
        /// </summary>
        public void Test_RenameLink()
        {
            Console.WriteLine("Begin: Test_RenameLink");

            // VpnRpcRenameLink in_rpc_rename_link = new VpnRpcRenameLink();
            VpnRpcRenameLink out_rpc_rename_link = Rpc.RenameLink();

            print_object(out_rpc_rename_link);

            Console.WriteLine("End: Test_RenameLink");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'CreateLink', Create a new link(cascade)
        /// </summary>
        public void Test_CreateLink()
        {
            Console.WriteLine("Begin: Test_CreateLink");

            // VpnRpcCreateLink in_rpc_create_link = new VpnRpcCreateLink();
            VpnRpcCreateLink out_rpc_create_link = Rpc.CreateLink();

            print_object(out_rpc_create_link);

            Console.WriteLine("End: Test_CreateLink");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'GetLink', Get link configuration
        /// </summary>
        public void Test_GetLink()
        {
            Console.WriteLine("Begin: Test_GetLink");

            // VpnRpcCreateLink in_rpc_create_link = new VpnRpcCreateLink();
            VpnRpcCreateLink out_rpc_create_link = Rpc.GetLink();

            print_object(out_rpc_create_link);

            Console.WriteLine("End: Test_GetLink");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'SetLink', Set link configuration
        /// </summary>
        public void Test_SetLink()
        {
            Console.WriteLine("Begin: Test_SetLink");

            // VpnRpcCreateLink in_rpc_create_link = new VpnRpcCreateLink();
            VpnRpcCreateLink out_rpc_create_link = Rpc.SetLink();

            print_object(out_rpc_create_link);

            Console.WriteLine("End: Test_SetLink");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'EnumLink', Enumerate links
        /// </summary>
        public void Test_EnumLink()
        {
            Console.WriteLine("Begin: Test_EnumLink");

            // VpnRpcEnumLink in_rpc_enum_link = new VpnRpcEnumLink();
            VpnRpcEnumLink out_rpc_enum_link = Rpc.EnumLink();

            print_object(out_rpc_enum_link);

            Console.WriteLine("End: Test_EnumLink");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'GetLinkStatus', Get link status
        /// </summary>
        public void Test_GetLinkStatus()
        {
            Console.WriteLine("Begin: Test_GetLinkStatus");

            // VpnRpcLinkStatus in_rpc_link_status = new VpnRpcLinkStatus();
            VpnRpcLinkStatus out_rpc_link_status = Rpc.GetLinkStatus();

            print_object(out_rpc_link_status);

            Console.WriteLine("End: Test_GetLinkStatus");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'AddAccess', Add access list entry
        /// </summary>
        public void Test_AddAccess()
        {
            Console.WriteLine("Begin: Test_AddAccess");

            // VpnRpcAddAccess in_rpc_add_access = new VpnRpcAddAccess();
            VpnRpcAddAccess out_rpc_add_access = Rpc.AddAccess();

            print_object(out_rpc_add_access);

            Console.WriteLine("End: Test_AddAccess");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'DeleteAccess', Delete access list entry
        /// </summary>
        public void Test_DeleteAccess()
        {
            Console.WriteLine("Begin: Test_DeleteAccess");

            // VpnRpcDeleteAccess in_rpc_delete_access = new VpnRpcDeleteAccess();
            VpnRpcDeleteAccess out_rpc_delete_access = Rpc.DeleteAccess();

            print_object(out_rpc_delete_access);

            Console.WriteLine("End: Test_DeleteAccess");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'EnumAccess', Get access list
        /// </summary>
        public void Test_EnumAccess()
        {
            Console.WriteLine("Begin: Test_EnumAccess");

            // VpnRpcEnumAccessList in_rpc_enum_access_list = new VpnRpcEnumAccessList();
            VpnRpcEnumAccessList out_rpc_enum_access_list = Rpc.EnumAccess();

            print_object(out_rpc_enum_access_list);

            Console.WriteLine("End: Test_EnumAccess");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'SetAccessList', Set access list
        /// </summary>
        public void Test_SetAccessList()
        {
            Console.WriteLine("Begin: Test_SetAccessList");

            // VpnRpcEnumAccessList in_rpc_enum_access_list = new VpnRpcEnumAccessList();
            VpnRpcEnumAccessList out_rpc_enum_access_list = Rpc.SetAccessList();

            print_object(out_rpc_enum_access_list);

            Console.WriteLine("End: Test_SetAccessList");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'CreateUser', Create a user
        /// </summary>
        public void Test_CreateUser()
        {
            Console.WriteLine("Begin: Test_CreateUser");

            // VpnRpcSetUser in_rpc_set_user = new VpnRpcSetUser();
            VpnRpcSetUser out_rpc_set_user = Rpc.CreateUser();

            print_object(out_rpc_set_user);

            Console.WriteLine("End: Test_CreateUser");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'SetUser', Set user setting
        /// </summary>
        public void Test_SetUser()
        {
            Console.WriteLine("Begin: Test_SetUser");

            // VpnRpcSetUser in_rpc_set_user = new VpnRpcSetUser();
            VpnRpcSetUser out_rpc_set_user = Rpc.SetUser();

            print_object(out_rpc_set_user);

            Console.WriteLine("End: Test_SetUser");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'GetUser', Get user setting
        /// </summary>
        public void Test_GetUser()
        {
            Console.WriteLine("Begin: Test_GetUser");

            // VpnRpcSetUser in_rpc_set_user = new VpnRpcSetUser();
            VpnRpcSetUser out_rpc_set_user = Rpc.GetUser();

            print_object(out_rpc_set_user);

            Console.WriteLine("End: Test_GetUser");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'DeleteUser', Delete a user
        /// </summary>
        public void Test_DeleteUser()
        {
            Console.WriteLine("Begin: Test_DeleteUser");

            // VpnRpcDeleteUser in_rpc_delete_user = new VpnRpcDeleteUser();
            VpnRpcDeleteUser out_rpc_delete_user = Rpc.DeleteUser();

            print_object(out_rpc_delete_user);

            Console.WriteLine("End: Test_DeleteUser");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'EnumUser', Enumerate users
        /// </summary>
        public void Test_EnumUser()
        {
            Console.WriteLine("Begin: Test_EnumUser");

            // VpnRpcEnumUser in_rpc_enum_user = new VpnRpcEnumUser();
            VpnRpcEnumUser out_rpc_enum_user = Rpc.EnumUser();

            print_object(out_rpc_enum_user);

            Console.WriteLine("End: Test_EnumUser");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'CreateGroup', Create a group
        /// </summary>
        public void Test_CreateGroup()
        {
            Console.WriteLine("Begin: Test_CreateGroup");

            // VpnRpcSetGroup in_rpc_set_group = new VpnRpcSetGroup();
            VpnRpcSetGroup out_rpc_set_group = Rpc.CreateGroup();

            print_object(out_rpc_set_group);

            Console.WriteLine("End: Test_CreateGroup");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'SetGroup', Set group setting
        /// </summary>
        public void Test_SetGroup()
        {
            Console.WriteLine("Begin: Test_SetGroup");

            // VpnRpcSetGroup in_rpc_set_group = new VpnRpcSetGroup();
            VpnRpcSetGroup out_rpc_set_group = Rpc.SetGroup();

            print_object(out_rpc_set_group);

            Console.WriteLine("End: Test_SetGroup");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'GetGroup', Get group information
        /// </summary>
        public void Test_GetGroup()
        {
            Console.WriteLine("Begin: Test_GetGroup");

            // VpnRpcSetGroup in_rpc_set_group = new VpnRpcSetGroup();
            VpnRpcSetGroup out_rpc_set_group = Rpc.GetGroup();

            print_object(out_rpc_set_group);

            Console.WriteLine("End: Test_GetGroup");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'DeleteGroup', Delete a group
        /// </summary>
        public void Test_DeleteGroup()
        {
            Console.WriteLine("Begin: Test_DeleteGroup");

            // VpnRpcDeleteUser in_rpc_delete_user = new VpnRpcDeleteUser();
            VpnRpcDeleteUser out_rpc_delete_user = Rpc.DeleteGroup();

            print_object(out_rpc_delete_user);

            Console.WriteLine("End: Test_DeleteGroup");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'EnumGroup', Enumerate groups
        /// </summary>
        public void Test_EnumGroup()
        {
            Console.WriteLine("Begin: Test_EnumGroup");

            // VpnRpcEnumGroup in_rpc_enum_group = new VpnRpcEnumGroup();
            VpnRpcEnumGroup out_rpc_enum_group = Rpc.EnumGroup();

            print_object(out_rpc_enum_group);

            Console.WriteLine("End: Test_EnumGroup");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'EnumSession', Enumerate sessions
        /// </summary>
        public void Test_EnumSession()
        {
            Console.WriteLine("Begin: Test_EnumSession");

            // VpnRpcEnumSession in_rpc_enum_session = new VpnRpcEnumSession();
            VpnRpcEnumSession out_rpc_enum_session = Rpc.EnumSession();

            print_object(out_rpc_enum_session);

            Console.WriteLine("End: Test_EnumSession");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'GetSessionStatus', Get session status
        /// </summary>
        public void Test_GetSessionStatus()
        {
            Console.WriteLine("Begin: Test_GetSessionStatus");

            // VpnRpcSessionStatus in_rpc_session_status = new VpnRpcSessionStatus();
            VpnRpcSessionStatus out_rpc_session_status = Rpc.GetSessionStatus();

            print_object(out_rpc_session_status);

            Console.WriteLine("End: Test_GetSessionStatus");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'DeleteSession', Delete a session
        /// </summary>
        public void Test_DeleteSession()
        {
            Console.WriteLine("Begin: Test_DeleteSession");

            // VpnRpcDeleteSession in_rpc_delete_session = new VpnRpcDeleteSession();
            VpnRpcDeleteSession out_rpc_delete_session = Rpc.DeleteSession();

            print_object(out_rpc_delete_session);

            Console.WriteLine("End: Test_DeleteSession");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'EnumMacTable', Get MAC address table
        /// </summary>
        public void Test_EnumMacTable()
        {
            Console.WriteLine("Begin: Test_EnumMacTable");

            // VpnRpcEnumMacTable in_rpc_enum_mac_table = new VpnRpcEnumMacTable();
            VpnRpcEnumMacTable out_rpc_enum_mac_table = Rpc.EnumMacTable();

            print_object(out_rpc_enum_mac_table);

            Console.WriteLine("End: Test_EnumMacTable");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'DeleteMacTable', Delete MAC address table entry
        /// </summary>
        public void Test_DeleteMacTable()
        {
            Console.WriteLine("Begin: Test_DeleteMacTable");

            // VpnRpcDeleteTable in_rpc_delete_table = new VpnRpcDeleteTable();
            VpnRpcDeleteTable out_rpc_delete_table = Rpc.DeleteMacTable();

            print_object(out_rpc_delete_table);

            Console.WriteLine("End: Test_DeleteMacTable");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'EnumIpTable', Get IP address table
        /// </summary>
        public void Test_EnumIpTable()
        {
            Console.WriteLine("Begin: Test_EnumIpTable");

            // VpnRpcEnumIpTable in_rpc_enum_ip_table = new VpnRpcEnumIpTable();
            VpnRpcEnumIpTable out_rpc_enum_ip_table = Rpc.EnumIpTable();

            print_object(out_rpc_enum_ip_table);

            Console.WriteLine("End: Test_EnumIpTable");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'DeleteIpTable', Delete IP address table entry
        /// </summary>
        public void Test_DeleteIpTable()
        {
            Console.WriteLine("Begin: Test_DeleteIpTable");

            // VpnRpcDeleteTable in_rpc_delete_table = new VpnRpcDeleteTable();
            VpnRpcDeleteTable out_rpc_delete_table = Rpc.DeleteIpTable();

            print_object(out_rpc_delete_table);

            Console.WriteLine("End: Test_DeleteIpTable");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'SetKeep', Set keep-alive function setting
        /// </summary>
        public void Test_SetKeep()
        {
            Console.WriteLine("Begin: Test_SetKeep");

            // VpnRpcKeep in_rpc_keep = new VpnRpcKeep();
            VpnRpcKeep out_rpc_keep = Rpc.SetKeep();

            print_object(out_rpc_keep);

            Console.WriteLine("End: Test_SetKeep");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'GetKeep', Get keep-alive function setting
        /// </summary>
        public void Test_GetKeep()
        {
            Console.WriteLine("Begin: Test_GetKeep");

            // VpnRpcKeep in_rpc_keep = new VpnRpcKeep();
            VpnRpcKeep out_rpc_keep = Rpc.GetKeep();

            print_object(out_rpc_keep);

            Console.WriteLine("End: Test_GetKeep");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'EnableSecureNAT', Enable SecureNAT function of the hub
        /// </summary>
        public void Test_EnableSecureNAT()
        {
            Console.WriteLine("Begin: Test_EnableSecureNAT");

            // VpnRpcHub in_rpc_hub = new VpnRpcHub();
            VpnRpcHub out_rpc_hub = Rpc.EnableSecureNAT();

            print_object(out_rpc_hub);

            Console.WriteLine("End: Test_EnableSecureNAT");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'DisableSecureNAT', Disable the SecureNAT function of the hub
        /// </summary>
        public void Test_DisableSecureNAT()
        {
            Console.WriteLine("Begin: Test_DisableSecureNAT");

            // VpnRpcHub in_rpc_hub = new VpnRpcHub();
            VpnRpcHub out_rpc_hub = Rpc.DisableSecureNAT();

            print_object(out_rpc_hub);

            Console.WriteLine("End: Test_DisableSecureNAT");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'SetSecureNATOption', Set SecureNAT options
        /// </summary>
        public void Test_SetSecureNATOption()
        {
            Console.WriteLine("Begin: Test_SetSecureNATOption");

            // VpnVhOption in_vh_option = new VpnVhOption();
            VpnVhOption out_vh_option = Rpc.SetSecureNATOption();

            print_object(out_vh_option);

            Console.WriteLine("End: Test_SetSecureNATOption");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'GetSecureNATOption', Get SecureNAT options
        /// </summary>
        public void Test_GetSecureNATOption()
        {
            Console.WriteLine("Begin: Test_GetSecureNATOption");

            // VpnVhOption in_vh_option = new VpnVhOption();
            VpnVhOption out_vh_option = Rpc.GetSecureNATOption();

            print_object(out_vh_option);

            Console.WriteLine("End: Test_GetSecureNATOption");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'EnumNAT', Enumerate NAT entries of the SecureNAT
        /// </summary>
        public void Test_EnumNAT()
        {
            Console.WriteLine("Begin: Test_EnumNAT");

            // VpnRpcEnumNat in_rpc_enum_nat = new VpnRpcEnumNat();
            VpnRpcEnumNat out_rpc_enum_nat = Rpc.EnumNAT();

            print_object(out_rpc_enum_nat);

            Console.WriteLine("End: Test_EnumNAT");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'EnumDHCP', Enumerate DHCP entries
        /// </summary>
        public void Test_EnumDHCP()
        {
            Console.WriteLine("Begin: Test_EnumDHCP");

            // VpnRpcEnumDhcp in_rpc_enum_dhcp = new VpnRpcEnumDhcp();
            VpnRpcEnumDhcp out_rpc_enum_dhcp = Rpc.EnumDHCP();

            print_object(out_rpc_enum_dhcp);

            Console.WriteLine("End: Test_EnumDHCP");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'GetSecureNATStatus', Get status of the SecureNAT
        /// </summary>
        public void Test_GetSecureNATStatus()
        {
            Console.WriteLine("Begin: Test_GetSecureNATStatus");

            // VpnRpcNatStatus in_rpc_nat_status = new VpnRpcNatStatus();
            VpnRpcNatStatus out_rpc_nat_status = Rpc.GetSecureNATStatus();

            print_object(out_rpc_nat_status);

            Console.WriteLine("End: Test_GetSecureNATStatus");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'EnumEthernet', Enumerate Ethernet devices
        /// </summary>
        public void Test_EnumEthernet()
        {
            Console.WriteLine("Begin: Test_EnumEthernet");

            // VpnRpcEnumEth in_rpc_enum_eth = new VpnRpcEnumEth();
            VpnRpcEnumEth out_rpc_enum_eth = Rpc.EnumEthernet();

            print_object(out_rpc_enum_eth);

            Console.WriteLine("End: Test_EnumEthernet");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'AddLocalBridge', Add a new local bridge
        /// </summary>
        public void Test_AddLocalBridge()
        {
            Console.WriteLine("Begin: Test_AddLocalBridge");

            // VpnRpcLocalBridge in_rpc_localbridge = new VpnRpcLocalBridge();
            VpnRpcLocalBridge out_rpc_localbridge = Rpc.AddLocalBridge();

            print_object(out_rpc_localbridge);

            Console.WriteLine("End: Test_AddLocalBridge");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'DeleteLocalBridge', Delete a local bridge
        /// </summary>
        public void Test_DeleteLocalBridge()
        {
            Console.WriteLine("Begin: Test_DeleteLocalBridge");

            // VpnRpcLocalBridge in_rpc_localbridge = new VpnRpcLocalBridge();
            VpnRpcLocalBridge out_rpc_localbridge = Rpc.DeleteLocalBridge();

            print_object(out_rpc_localbridge);

            Console.WriteLine("End: Test_DeleteLocalBridge");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'EnumLocalBridge', Enumerate local bridges
        /// </summary>
        public void Test_EnumLocalBridge()
        {
            Console.WriteLine("Begin: Test_EnumLocalBridge");

            // VpnRpcEnumLocalBridge in_rpc_enum_localbridge = new VpnRpcEnumLocalBridge();
            VpnRpcEnumLocalBridge out_rpc_enum_localbridge = Rpc.EnumLocalBridge();

            print_object(out_rpc_enum_localbridge);

            Console.WriteLine("End: Test_EnumLocalBridge");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'GetBridgeSupport', Get availability to localbridge function
        /// </summary>
        public void Test_GetBridgeSupport()
        {
            Console.WriteLine("Begin: Test_GetBridgeSupport");

            // VpnRpcBridgeSupport in_rpc_bridge_support = new VpnRpcBridgeSupport();
            VpnRpcBridgeSupport out_rpc_bridge_support = Rpc.GetBridgeSupport();

            print_object(out_rpc_bridge_support);

            Console.WriteLine("End: Test_GetBridgeSupport");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'RebootServer', Reboot server itself
        /// </summary>
        public void Test_RebootServer()
        {
            Console.WriteLine("Begin: Test_RebootServer");

            // VpnRpcTest in_rpc_test = new VpnRpcTest();
            VpnRpcTest out_rpc_test = Rpc.RebootServer();

            print_object(out_rpc_test);

            Console.WriteLine("End: Test_RebootServer");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'GetCaps', Get capabilities
        /// </summary>
        public void Test_GetCaps()
        {
            Console.WriteLine("Begin: Test_GetCaps");

            // VpnCapslist in_capslist = new VpnCapslist();
            VpnCapslist out_capslist = Rpc.GetCaps();

            print_object(out_capslist);

            Console.WriteLine("End: Test_GetCaps");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'GetConfig', Get configuration file stream
        /// </summary>
        public void Test_GetConfig()
        {
            Console.WriteLine("Begin: Test_GetConfig");

            // VpnRpcConfig in_rpc_config = new VpnRpcConfig();
            VpnRpcConfig out_rpc_config = Rpc.GetConfig();

            print_object(out_rpc_config);

            Console.WriteLine("End: Test_GetConfig");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'SetConfig', Overwrite configuration file by specified data
        /// </summary>
        public void Test_SetConfig()
        {
            Console.WriteLine("Begin: Test_SetConfig");

            // VpnRpcConfig in_rpc_config = new VpnRpcConfig();
            VpnRpcConfig out_rpc_config = Rpc.SetConfig();

            print_object(out_rpc_config);

            Console.WriteLine("End: Test_SetConfig");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'GetDefaultHubAdminOptions', Get default hub administration options
        /// </summary>
        public void Test_GetDefaultHubAdminOptions()
        {
            Console.WriteLine("Begin: Test_GetDefaultHubAdminOptions");

            // VpnRpcAdminOption in_rpc_admin_option = new VpnRpcAdminOption();
            VpnRpcAdminOption out_rpc_admin_option = Rpc.GetDefaultHubAdminOptions();

            print_object(out_rpc_admin_option);

            Console.WriteLine("End: Test_GetDefaultHubAdminOptions");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'GetHubAdminOptions', Get hub administration options
        /// </summary>
        public void Test_GetHubAdminOptions()
        {
            Console.WriteLine("Begin: Test_GetHubAdminOptions");

            // VpnRpcAdminOption in_rpc_admin_option = new VpnRpcAdminOption();
            VpnRpcAdminOption out_rpc_admin_option = Rpc.GetHubAdminOptions();

            print_object(out_rpc_admin_option);

            Console.WriteLine("End: Test_GetHubAdminOptions");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'SetHubAdminOptions', Set hub administration options
        /// </summary>
        public void Test_SetHubAdminOptions()
        {
            Console.WriteLine("Begin: Test_SetHubAdminOptions");

            // VpnRpcAdminOption in_rpc_admin_option = new VpnRpcAdminOption();
            VpnRpcAdminOption out_rpc_admin_option = Rpc.SetHubAdminOptions();

            print_object(out_rpc_admin_option);

            Console.WriteLine("End: Test_SetHubAdminOptions");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'GetHubExtOptions', Get hub extended options
        /// </summary>
        public void Test_GetHubExtOptions()
        {
            Console.WriteLine("Begin: Test_GetHubExtOptions");

            // VpnRpcAdminOption in_rpc_admin_option = new VpnRpcAdminOption();
            VpnRpcAdminOption out_rpc_admin_option = Rpc.GetHubExtOptions();

            print_object(out_rpc_admin_option);

            Console.WriteLine("End: Test_GetHubExtOptions");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'SetHubExtOptions', Set hub extended options
        /// </summary>
        public void Test_SetHubExtOptions()
        {
            Console.WriteLine("Begin: Test_SetHubExtOptions");

            // VpnRpcAdminOption in_rpc_admin_option = new VpnRpcAdminOption();
            VpnRpcAdminOption out_rpc_admin_option = Rpc.SetHubExtOptions();

            print_object(out_rpc_admin_option);

            Console.WriteLine("End: Test_SetHubExtOptions");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'AddL3Switch', Add a new virtual layer-3 switch
        /// </summary>
        public void Test_AddL3Switch()
        {
            Console.WriteLine("Begin: Test_AddL3Switch");

            // VpnRpcL3Sw in_rpc_l3sw = new VpnRpcL3Sw();
            VpnRpcL3Sw out_rpc_l3sw = Rpc.AddL3Switch();

            print_object(out_rpc_l3sw);

            Console.WriteLine("End: Test_AddL3Switch");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'DelL3Switch', Delete a virtual layer-3 switch
        /// </summary>
        public void Test_DelL3Switch()
        {
            Console.WriteLine("Begin: Test_DelL3Switch");

            // VpnRpcL3Sw in_rpc_l3sw = new VpnRpcL3Sw();
            VpnRpcL3Sw out_rpc_l3sw = Rpc.DelL3Switch();

            print_object(out_rpc_l3sw);

            Console.WriteLine("End: Test_DelL3Switch");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'EnumL3Switch', Enumerate virtual layer-3 switches
        /// </summary>
        public void Test_EnumL3Switch()
        {
            Console.WriteLine("Begin: Test_EnumL3Switch");

            // VpnRpcEnumL3Sw in_rpc_enum_l3sw = new VpnRpcEnumL3Sw();
            VpnRpcEnumL3Sw out_rpc_enum_l3sw = Rpc.EnumL3Switch();

            print_object(out_rpc_enum_l3sw);

            Console.WriteLine("End: Test_EnumL3Switch");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'StartL3Switch', Start a virtual layer-3 switch
        /// </summary>
        public void Test_StartL3Switch()
        {
            Console.WriteLine("Begin: Test_StartL3Switch");

            // VpnRpcL3Sw in_rpc_l3sw = new VpnRpcL3Sw();
            VpnRpcL3Sw out_rpc_l3sw = Rpc.StartL3Switch();

            print_object(out_rpc_l3sw);

            Console.WriteLine("End: Test_StartL3Switch");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'StopL3Switch', Stop a virtual layer-3 switch
        /// </summary>
        public void Test_StopL3Switch()
        {
            Console.WriteLine("Begin: Test_StopL3Switch");

            // VpnRpcL3Sw in_rpc_l3sw = new VpnRpcL3Sw();
            VpnRpcL3Sw out_rpc_l3sw = Rpc.StopL3Switch();

            print_object(out_rpc_l3sw);

            Console.WriteLine("End: Test_StopL3Switch");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'AddL3If', Add new virtual interface on virtual L3 switch
        /// </summary>
        public void Test_AddL3If()
        {
            Console.WriteLine("Begin: Test_AddL3If");

            // VpnRpcL3If in_rpc_l3if = new VpnRpcL3If();
            VpnRpcL3If out_rpc_l3if = Rpc.AddL3If();

            print_object(out_rpc_l3if);

            Console.WriteLine("End: Test_AddL3If");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'DelL3If', Delete a virtual interface on virtual L3 switch
        /// </summary>
        public void Test_DelL3If()
        {
            Console.WriteLine("Begin: Test_DelL3If");

            // VpnRpcL3If in_rpc_l3if = new VpnRpcL3If();
            VpnRpcL3If out_rpc_l3if = Rpc.DelL3If();

            print_object(out_rpc_l3if);

            Console.WriteLine("End: Test_DelL3If");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'EnumL3If', Enumerate virtual interfaces on virtual L3 switch
        /// </summary>
        public void Test_EnumL3If()
        {
            Console.WriteLine("Begin: Test_EnumL3If");

            // VpnRpcEnumL3If in_rpc_enum_l3if = new VpnRpcEnumL3If();
            VpnRpcEnumL3If out_rpc_enum_l3if = Rpc.EnumL3If();

            print_object(out_rpc_enum_l3if);

            Console.WriteLine("End: Test_EnumL3If");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'AddL3Table', Add new routing table entry on virtual L3 switch
        /// </summary>
        public void Test_AddL3Table()
        {
            Console.WriteLine("Begin: Test_AddL3Table");

            // VpnRpcL3Table in_rpc_l3table = new VpnRpcL3Table();
            VpnRpcL3Table out_rpc_l3table = Rpc.AddL3Table();

            print_object(out_rpc_l3table);

            Console.WriteLine("End: Test_AddL3Table");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'DelL3Table', Delete routing table entry on virtual L3 switch
        /// </summary>
        public void Test_DelL3Table()
        {
            Console.WriteLine("Begin: Test_DelL3Table");

            // VpnRpcL3Table in_rpc_l3table = new VpnRpcL3Table();
            VpnRpcL3Table out_rpc_l3table = Rpc.DelL3Table();

            print_object(out_rpc_l3table);

            Console.WriteLine("End: Test_DelL3Table");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'EnumL3Table', Get routing table on virtual L3 switch
        /// </summary>
        public void Test_EnumL3Table()
        {
            Console.WriteLine("Begin: Test_EnumL3Table");

            // VpnRpcEnumL3Table in_rpc_enum_l3table = new VpnRpcEnumL3Table();
            VpnRpcEnumL3Table out_rpc_enum_l3table = Rpc.EnumL3Table();

            print_object(out_rpc_enum_l3table);

            Console.WriteLine("End: Test_EnumL3Table");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'EnumCrl', Get CRL (Certificate Revocation List) index
        /// </summary>
        public void Test_EnumCrl()
        {
            Console.WriteLine("Begin: Test_EnumCrl");

            // VpnRpcEnumCrl in_rpc_enum_crl = new VpnRpcEnumCrl();
            VpnRpcEnumCrl out_rpc_enum_crl = Rpc.EnumCrl();

            print_object(out_rpc_enum_crl);

            Console.WriteLine("End: Test_EnumCrl");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'AddCrl', Add new CRL (Certificate Revocation List) entry
        /// </summary>
        public void Test_AddCrl()
        {
            Console.WriteLine("Begin: Test_AddCrl");

            // VpnRpcCrl in_rpc_crl = new VpnRpcCrl();
            VpnRpcCrl out_rpc_crl = Rpc.AddCrl();

            print_object(out_rpc_crl);

            Console.WriteLine("End: Test_AddCrl");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'DelCrl', Delete CRL (Certificate Revocation List) entry
        /// </summary>
        public void Test_DelCrl()
        {
            Console.WriteLine("Begin: Test_DelCrl");

            // VpnRpcCrl in_rpc_crl = new VpnRpcCrl();
            VpnRpcCrl out_rpc_crl = Rpc.DelCrl();

            print_object(out_rpc_crl);

            Console.WriteLine("End: Test_DelCrl");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'GetCrl', Get CRL (Certificate Revocation List) entry
        /// </summary>
        public void Test_GetCrl()
        {
            Console.WriteLine("Begin: Test_GetCrl");

            // VpnRpcCrl in_rpc_crl = new VpnRpcCrl();
            VpnRpcCrl out_rpc_crl = Rpc.GetCrl();

            print_object(out_rpc_crl);

            Console.WriteLine("End: Test_GetCrl");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'SetCrl', Set CRL (Certificate Revocation List) entry
        /// </summary>
        public void Test_SetCrl()
        {
            Console.WriteLine("Begin: Test_SetCrl");

            // VpnRpcCrl in_rpc_crl = new VpnRpcCrl();
            VpnRpcCrl out_rpc_crl = Rpc.SetCrl();

            print_object(out_rpc_crl);

            Console.WriteLine("End: Test_SetCrl");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'SetAcList', Set access control list
        /// </summary>
        public void Test_SetAcList()
        {
            Console.WriteLine("Begin: Test_SetAcList");

            // VpnRpcAcList in_rpc_ac_list = new VpnRpcAcList();
            VpnRpcAcList out_rpc_ac_list = Rpc.SetAcList();

            print_object(out_rpc_ac_list);

            Console.WriteLine("End: Test_SetAcList");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'GetAcList', Get access control list
        /// </summary>
        public void Test_GetAcList()
        {
            Console.WriteLine("Begin: Test_GetAcList");

            // VpnRpcAcList in_rpc_ac_list = new VpnRpcAcList();
            VpnRpcAcList out_rpc_ac_list = Rpc.GetAcList();

            print_object(out_rpc_ac_list);

            Console.WriteLine("End: Test_GetAcList");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'EnumLogFile', Enumerate log files
        /// </summary>
        public void Test_EnumLogFile()
        {
            Console.WriteLine("Begin: Test_EnumLogFile");

            // VpnRpcEnumLogFile in_rpc_enum_log_file = new VpnRpcEnumLogFile();
            VpnRpcEnumLogFile out_rpc_enum_log_file = Rpc.EnumLogFile();

            print_object(out_rpc_enum_log_file);

            Console.WriteLine("End: Test_EnumLogFile");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'ReadLogFile', Read a log file
        /// </summary>
        public void Test_ReadLogFile()
        {
            Console.WriteLine("Begin: Test_ReadLogFile");

            // VpnRpcReadLogFile in_rpc_read_log_file = new VpnRpcReadLogFile();
            VpnRpcReadLogFile out_rpc_read_log_file = Rpc.ReadLogFile();

            print_object(out_rpc_read_log_file);

            Console.WriteLine("End: Test_ReadLogFile");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'AddLicenseKey', Add new license key
        /// </summary>
        public void Test_AddLicenseKey()
        {
            Console.WriteLine("Begin: Test_AddLicenseKey");

            // VpnRpcTest in_rpc_test = new VpnRpcTest();
            VpnRpcTest out_rpc_test = Rpc.AddLicenseKey();

            print_object(out_rpc_test);

            Console.WriteLine("End: Test_AddLicenseKey");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'DelLicenseKey', Delete a license key
        /// </summary>
        public void Test_DelLicenseKey()
        {
            Console.WriteLine("Begin: Test_DelLicenseKey");

            // VpnRpcTest in_rpc_test = new VpnRpcTest();
            VpnRpcTest out_rpc_test = Rpc.DelLicenseKey();

            print_object(out_rpc_test);

            Console.WriteLine("End: Test_DelLicenseKey");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'EnumLicenseKey', Enumerate license key
        /// </summary>
        public void Test_EnumLicenseKey()
        {
            Console.WriteLine("Begin: Test_EnumLicenseKey");

            // VpnRpcEnumLicenseKey in_rpc_enum_license_key = new VpnRpcEnumLicenseKey();
            VpnRpcEnumLicenseKey out_rpc_enum_license_key = Rpc.EnumLicenseKey();

            print_object(out_rpc_enum_license_key);

            Console.WriteLine("End: Test_EnumLicenseKey");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'GetLicenseStatus', Get license status
        /// </summary>
        public void Test_GetLicenseStatus()
        {
            Console.WriteLine("Begin: Test_GetLicenseStatus");

            // VpnRpcLicenseStatus in_rpc_license_status = new VpnRpcLicenseStatus();
            VpnRpcLicenseStatus out_rpc_license_status = Rpc.GetLicenseStatus();

            print_object(out_rpc_license_status);

            Console.WriteLine("End: Test_GetLicenseStatus");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'SetSysLog', Set syslog function setting
        /// </summary>
        public void Test_SetSysLog()
        {
            Console.WriteLine("Begin: Test_SetSysLog");

            // VpnSyslogSetting in_syslog_setting = new VpnSyslogSetting();
            VpnSyslogSetting out_syslog_setting = Rpc.SetSysLog();

            print_object(out_syslog_setting);

            Console.WriteLine("End: Test_SetSysLog");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'GetSysLog', Get syslog function setting
        /// </summary>
        public void Test_GetSysLog()
        {
            Console.WriteLine("Begin: Test_GetSysLog");

            // VpnSyslogSetting in_syslog_setting = new VpnSyslogSetting();
            VpnSyslogSetting out_syslog_setting = Rpc.GetSysLog();

            print_object(out_syslog_setting);

            Console.WriteLine("End: Test_GetSysLog");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'EnumEthVLan', Enumerate VLAN tag transparent setting
        /// </summary>
        public void Test_EnumEthVLan()
        {
            Console.WriteLine("Begin: Test_EnumEthVLan");

            // VpnRpcEnumEthVlan in_rpc_enum_eth_vlan = new VpnRpcEnumEthVlan();
            VpnRpcEnumEthVlan out_rpc_enum_eth_vlan = Rpc.EnumEthVLan();

            print_object(out_rpc_enum_eth_vlan);

            Console.WriteLine("End: Test_EnumEthVLan");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'SetEnableEthVLan', Set VLAN tag transparent setting
        /// </summary>
        public void Test_SetEnableEthVLan()
        {
            Console.WriteLine("Begin: Test_SetEnableEthVLan");

            // VpnRpcTest in_rpc_test = new VpnRpcTest();
            VpnRpcTest out_rpc_test = Rpc.SetEnableEthVLan();

            print_object(out_rpc_test);

            Console.WriteLine("End: Test_SetEnableEthVLan");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'SetHubMsg', Set message of today on hub
        /// </summary>
        public void Test_SetHubMsg()
        {
            Console.WriteLine("Begin: Test_SetHubMsg");

            // VpnRpcMsg in_rpc_msg = new VpnRpcMsg();
            VpnRpcMsg out_rpc_msg = Rpc.SetHubMsg();

            print_object(out_rpc_msg);

            Console.WriteLine("End: Test_SetHubMsg");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'GetHubMsg', Get message of today on hub
        /// </summary>
        public void Test_GetHubMsg()
        {
            Console.WriteLine("Begin: Test_GetHubMsg");

            // VpnRpcMsg in_rpc_msg = new VpnRpcMsg();
            VpnRpcMsg out_rpc_msg = Rpc.GetHubMsg();

            print_object(out_rpc_msg);

            Console.WriteLine("End: Test_GetHubMsg");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'Crash', Do Crash
        /// </summary>
        public void Test_Crash()
        {
            Console.WriteLine("Begin: Test_Crash");

            // VpnRpcTest in_rpc_test = new VpnRpcTest();
            VpnRpcTest out_rpc_test = Rpc.Crash();

            print_object(out_rpc_test);

            Console.WriteLine("End: Test_Crash");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'GetAdminMsg', Get message for administrators
        /// </summary>
        public void Test_GetAdminMsg()
        {
            Console.WriteLine("Begin: Test_GetAdminMsg");

            // VpnRpcMsg in_rpc_msg = new VpnRpcMsg();
            VpnRpcMsg out_rpc_msg = Rpc.GetAdminMsg();

            print_object(out_rpc_msg);

            Console.WriteLine("End: Test_GetAdminMsg");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'Flush', Flush configuration file
        /// </summary>
        public void Test_Flush()
        {
            Console.WriteLine("Begin: Test_Flush");

            // VpnRpcTest in_rpc_test = new VpnRpcTest();
            VpnRpcTest out_rpc_test = Rpc.Flush();

            print_object(out_rpc_test);

            Console.WriteLine("End: Test_Flush");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'Debug', Do debug function
        /// </summary>
        public void Test_Debug()
        {
            Console.WriteLine("Begin: Test_Debug");

            // VpnRpcTest in_rpc_test = new VpnRpcTest();
            VpnRpcTest out_rpc_test = Rpc.Debug();

            print_object(out_rpc_test);

            Console.WriteLine("End: Test_Debug");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'SetIPsecServices', Set IPsec service configuration
        /// </summary>
        public void Test_SetIPsecServices()
        {
            Console.WriteLine("Begin: Test_SetIPsecServices");

            // VpnIPsecServices in_ipsec_services = new VpnIPsecServices();
            VpnIPsecServices out_ipsec_services = Rpc.SetIPsecServices();

            print_object(out_ipsec_services);

            Console.WriteLine("End: Test_SetIPsecServices");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'GetIPsecServices', Get IPsec service configuration
        /// </summary>
        public void Test_GetIPsecServices()
        {
            Console.WriteLine("Begin: Test_GetIPsecServices");

            // VpnIPsecServices in_ipsec_services = new VpnIPsecServices();
            VpnIPsecServices out_ipsec_services = Rpc.GetIPsecServices();

            print_object(out_ipsec_services);

            Console.WriteLine("End: Test_GetIPsecServices");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'AddEtherIpId', Add EtherIP ID setting
        /// </summary>
        public void Test_AddEtherIpId()
        {
            Console.WriteLine("Begin: Test_AddEtherIpId");

            // VpnEtherIpId in_etherip_id = new VpnEtherIpId();
            VpnEtherIpId out_etherip_id = Rpc.AddEtherIpId();

            print_object(out_etherip_id);

            Console.WriteLine("End: Test_AddEtherIpId");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'GetEtherIpId', Get EtherIP ID setting
        /// </summary>
        public void Test_GetEtherIpId()
        {
            Console.WriteLine("Begin: Test_GetEtherIpId");

            // VpnEtherIpId in_etherip_id = new VpnEtherIpId();
            VpnEtherIpId out_etherip_id = Rpc.GetEtherIpId();

            print_object(out_etherip_id);

            Console.WriteLine("End: Test_GetEtherIpId");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'DeleteEtherIpId', Delete EtherIP ID setting
        /// </summary>
        public void Test_DeleteEtherIpId()
        {
            Console.WriteLine("Begin: Test_DeleteEtherIpId");

            // VpnEtherIpId in_etherip_id = new VpnEtherIpId();
            VpnEtherIpId out_etherip_id = Rpc.DeleteEtherIpId();

            print_object(out_etherip_id);

            Console.WriteLine("End: Test_DeleteEtherIpId");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'EnumEtherIpId', Enumerate EtherIP ID settings
        /// </summary>
        public void Test_EnumEtherIpId()
        {
            Console.WriteLine("Begin: Test_EnumEtherIpId");

            // VpnRpcEnumEtherIpId in_rpc_enum_etherip_id = new VpnRpcEnumEtherIpId();
            VpnRpcEnumEtherIpId out_rpc_enum_etherip_id = Rpc.EnumEtherIpId();

            print_object(out_rpc_enum_etherip_id);

            Console.WriteLine("End: Test_EnumEtherIpId");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'SetOpenVpnSstpConfig', Set configurations for OpenVPN and SSTP
        /// </summary>
        public void Test_SetOpenVpnSstpConfig()
        {
            Console.WriteLine("Begin: Test_SetOpenVpnSstpConfig");

            // VpnOpenVpnSstpConfig in_openvpn_sstp_config = new VpnOpenVpnSstpConfig();
            VpnOpenVpnSstpConfig out_openvpn_sstp_config = Rpc.SetOpenVpnSstpConfig();

            print_object(out_openvpn_sstp_config);

            Console.WriteLine("End: Test_SetOpenVpnSstpConfig");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'GetOpenVpnSstpConfig', Get configurations for OpenVPN and SSTP
        /// </summary>
        public void Test_GetOpenVpnSstpConfig()
        {
            Console.WriteLine("Begin: Test_GetOpenVpnSstpConfig");

            // VpnOpenVpnSstpConfig in_openvpn_sstp_config = new VpnOpenVpnSstpConfig();
            VpnOpenVpnSstpConfig out_openvpn_sstp_config = Rpc.GetOpenVpnSstpConfig();

            print_object(out_openvpn_sstp_config);

            Console.WriteLine("End: Test_GetOpenVpnSstpConfig");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'GetDDnsClientStatus', Get status of DDNS client
        /// </summary>
        public void Test_GetDDnsClientStatus()
        {
            Console.WriteLine("Begin: Test_GetDDnsClientStatus");

            // VpnDDnsClientStatus in_ddns_client_status = new VpnDDnsClientStatus();
            VpnDDnsClientStatus out_ddns_client_status = Rpc.GetDDnsClientStatus();

            print_object(out_ddns_client_status);

            Console.WriteLine("End: Test_GetDDnsClientStatus");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'ChangeDDnsClientHostname', Change host-name for DDNS client
        /// </summary>
        public void Test_ChangeDDnsClientHostname()
        {
            Console.WriteLine("Begin: Test_ChangeDDnsClientHostname");

            // VpnRpcTest in_rpc_test = new VpnRpcTest();
            VpnRpcTest out_rpc_test = Rpc.ChangeDDnsClientHostname();

            print_object(out_rpc_test);

            Console.WriteLine("End: Test_ChangeDDnsClientHostname");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'RegenerateServerCert', Regenerate server certification
        /// </summary>
        public void Test_RegenerateServerCert()
        {
            Console.WriteLine("Begin: Test_RegenerateServerCert");

            // VpnRpcTest in_rpc_test = new VpnRpcTest();
            VpnRpcTest out_rpc_test = Rpc.RegenerateServerCert();

            print_object(out_rpc_test);

            Console.WriteLine("End: Test_RegenerateServerCert");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'MakeOpenVpnConfigFile', Generate OpenVPN configuration files
        /// </summary>
        public void Test_MakeOpenVpnConfigFile()
        {
            Console.WriteLine("Begin: Test_MakeOpenVpnConfigFile");

            // VpnRpcReadLogFile in_rpc_read_log_file = new VpnRpcReadLogFile();
            VpnRpcReadLogFile out_rpc_read_log_file = Rpc.MakeOpenVpnConfigFile();

            print_object(out_rpc_read_log_file);

            Console.WriteLine("End: Test_MakeOpenVpnConfigFile");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'SetSpecialListener', Set special listener status
        /// </summary>
        public void Test_SetSpecialListener()
        {
            Console.WriteLine("Begin: Test_SetSpecialListener");

            // VpnRpcSpecialListener in_rpc_special_listener = new VpnRpcSpecialListener();
            VpnRpcSpecialListener out_rpc_special_listener = Rpc.SetSpecialListener();

            print_object(out_rpc_special_listener);

            Console.WriteLine("End: Test_SetSpecialListener");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'GetSpecialListener', Get special listener status
        /// </summary>
        public void Test_GetSpecialListener()
        {
            Console.WriteLine("Begin: Test_GetSpecialListener");

            // VpnRpcSpecialListener in_rpc_special_listener = new VpnRpcSpecialListener();
            VpnRpcSpecialListener out_rpc_special_listener = Rpc.GetSpecialListener();

            print_object(out_rpc_special_listener);

            Console.WriteLine("End: Test_GetSpecialListener");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'GetAzureStatus', Get Azure status
        /// </summary>
        public void Test_GetAzureStatus()
        {
            Console.WriteLine("Begin: Test_GetAzureStatus");

            // VpnRpcAzureStatus in_rpc_azure_status = new VpnRpcAzureStatus();
            VpnRpcAzureStatus out_rpc_azure_status = Rpc.GetAzureStatus();

            print_object(out_rpc_azure_status);

            Console.WriteLine("End: Test_GetAzureStatus");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'SetAzureStatus', Set Azure status
        /// </summary>
        public void Test_SetAzureStatus()
        {
            Console.WriteLine("Begin: Test_SetAzureStatus");

            // VpnRpcAzureStatus in_rpc_azure_status = new VpnRpcAzureStatus();
            VpnRpcAzureStatus out_rpc_azure_status = Rpc.SetAzureStatus();

            print_object(out_rpc_azure_status);

            Console.WriteLine("End: Test_SetAzureStatus");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'GetDDnsInternetSettng', Get DDNS proxy configuration
        /// </summary>
        public void Test_GetDDnsInternetSettng()
        {
            Console.WriteLine("Begin: Test_GetDDnsInternetSettng");

            // VpnInternetSetting in_internet_setting = new VpnInternetSetting();
            VpnInternetSetting out_internet_setting = Rpc.GetDDnsInternetSettng();

            print_object(out_internet_setting);

            Console.WriteLine("End: Test_GetDDnsInternetSettng");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'SetDDnsInternetSettng', Set DDNS proxy configuration
        /// </summary>
        public void Test_SetDDnsInternetSettng()
        {
            Console.WriteLine("Begin: Test_SetDDnsInternetSettng");

            // VpnInternetSetting in_internet_setting = new VpnInternetSetting();
            VpnInternetSetting out_internet_setting = Rpc.SetDDnsInternetSettng();

            print_object(out_internet_setting);

            Console.WriteLine("End: Test_SetDDnsInternetSettng");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'SetVgsConfig', Setting VPN Gate Server Configuration
        /// </summary>
        public void Test_SetVgsConfig()
        {
            Console.WriteLine("Begin: Test_SetVgsConfig");

            // VpnVgsConfig in_vgs_config = new VpnVgsConfig();
            VpnVgsConfig out_vgs_config = Rpc.SetVgsConfig();

            print_object(out_vgs_config);

            Console.WriteLine("End: Test_SetVgsConfig");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'GetVgsConfig', Get VPN Gate configuration
        /// </summary>
        public void Test_GetVgsConfig()
        {
            Console.WriteLine("Begin: Test_GetVgsConfig");

            // VpnVgsConfig in_vgs_config = new VpnVgsConfig();
            VpnVgsConfig out_vgs_config = Rpc.GetVgsConfig();

            print_object(out_vgs_config);

            Console.WriteLine("End: Test_GetVgsConfig");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        void print_object(object obj)
        {
            JsonSerializerSettings setting = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Include,
                ReferenceLoopHandling = ReferenceLoopHandling.Error,
            };
            string str = JsonConvert.SerializeObject(obj, Formatting.Indented, setting);
            Console.WriteLine(str);
        }
    }
}
