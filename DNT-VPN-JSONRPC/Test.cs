﻿using System;
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

        string hub_name;

        public VPNRPCTest()
        {
            Rpc = new VpnServerRpc("127.0.0.1", 443, "", "");
        }

        public void Test_All()
        {
            hub_name = "TEST";

            Test_Test();

            if (false)
            {

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

                Test_SetServerCert();

                Test_GetServerCipher();

                Test_SetServerCipher();

                VpnRpcEnumConnection enum_connection = Test_EnumConnection();
                //Test_DisconnectConnection();
                foreach (VpnRpcEnumConnectionItem connecton in enum_connection.ConnectionList)
                {
                    Test_GetConnectionInfo(connecton.Name_str);
                }

                //hub_name = Test_CreateHub();
                

                Test_SetHub();
                Test_GetHub();
                Test_EnumHub();
                //Test_DeleteHub();
                Test_SetHubRadius();
                Test_GetHubRadius();

                //return;

                Test_SetHubOnline();
                Test_GetHubStatus();

                VpnRpcHubLog hub_log_settings = Test_GetHubLog();
                Test_SetHubLog(hub_log_settings);

                Test_AddCa();
                VpnRpcHubEnumCA enum_ca = Test_EnumCa();
                foreach (VpnRpcHubEnumCAItem ca in enum_ca.CAList)
                {
                    Test_GetCa(ca.Key_u32);
                    Test_DeleteCa(ca.Key_u32);
                }
            }

            if (false)
            {
                Test_CreateLink();
                Test_GetLink();
                Test_SetLink();
                Test_SetLinkOffline();
                Test_SetLinkOnline();
                VpnRpcEnumLink enum_link = Test_EnumLink();
                foreach (var link in enum_link.LinkList)
                {
                    Test_GetLinkStatus(link.AccountName_utf);
                }
                Test_RenameLink();
                Test_DeleteLink();
            }

            if (false)
            {
                Test_AddAccess();
                Test_EnumAccess();
                Test_DeleteAccess();
                Test_SetAccessList();
            }

            if (false)
            {
                Test_CreateGroup();
                Test_SetGroup();
                Test_GetGroup();

                Test_CreateUser();
                Test_SetUser();
                Test_GetUser();
                Test_EnumUser();
                Test_EnumGroup();

                Test_DeleteUser();
                Test_DeleteGroup();

                VpnRpcEnumSession enum_session = Test_EnumSession();

                if (enum_session.SessionList != null)
                {
                    foreach (VpnRpcEnumSessionItem session in enum_session.SessionList)
                    {
                        Test_GetSessionStatus(session.Name_str);

                        //Test_DeleteSession(session.Name_str);
                    }
                }

                VpnRpcEnumMacTable enum_mac = Test_EnumMacTable();

                if (enum_mac.MacTable != null)
                {
                    foreach (VpnRpcEnumMacTableItem mac in enum_mac.MacTable)
                    {
                        Test_DeleteMacTable(mac.Key_u32);
                    }
                }

                VpnRpcEnumIpTable enum_ip = Test_EnumIpTable();

                if (enum_ip.IpTable != null)
                {
                    foreach (VpnRpcEnumIpTableItem ip in enum_ip.IpTable)
                    {
                        Test_DeleteIpTable(ip.Key_u32);
                    }
                }

                Test_SetKeep();
                Test_GetKeep();

                Test_EnableSecureNAT();
                Test_GetSecureNATOption();
                Test_SetSecureNATOption();
                Test_EnumNAT();
                Test_EnumDHCP();
                Test_GetSecureNATStatus();
                Test_DisableSecureNAT();
            }

            if (false)
            {
                Test_EnumEthernet();
                Test_AddLocalBridge();
                Test_EnumLocalBridge();
                Test_DeleteLocalBridge();
                Test_GetBridgeSupport();
            }

            //Test_RebootServer();
            Test_GetCaps();
            return;
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

            VpnRpcStr in_rpc_str = new VpnRpcStr() { String_str = "RC4-MD5" };
            VpnRpcStr out_rpc_str = Rpc.SetServerCipher(in_rpc_str);

            print_object(out_rpc_str);

            Console.WriteLine("End: Test_SetServerCipher");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'CreateHub', Create a hub
        /// </summary>
        public string Test_CreateHub()
        {
            string hub_name = $"Test_{rand.Next(100000, 999999)}";
            Console.WriteLine("Begin: Test_CreateHub");

            VpnRpcCreateHub in_rpc_create_hub = new VpnRpcCreateHub()
            {
                HubName_str = hub_name,
                HubType_u32 = VpnRpcHubType.Standalone,
                Online_bool = true,
                AdminPasswordPlainText_str = "microsoft",
            };

            VpnRpcCreateHub out_rpc_create_hub = Rpc.CreateHub(in_rpc_create_hub);

            print_object(out_rpc_create_hub);

            Console.WriteLine("End: Test_CreateHub");
            Console.WriteLine("-----");
            Console.WriteLine();

            return hub_name;
        }

        /// <summary>
        /// API test for 'SetHub', Set hub configuration
        /// </summary>
        public void Test_SetHub()
        {
            Console.WriteLine("Begin: Test_SetHub");

            VpnRpcCreateHub in_rpc_create_hub = new VpnRpcCreateHub()
            {
                HubName_str = hub_name,
                AdminPasswordPlainText_str = "aho",
                HubType_u32 = VpnRpcHubType.Standalone,
                NoEnum_bool = false,
                MaxSession_u32 = 128,
                Online_bool = true,
            };

            VpnRpcCreateHub out_rpc_create_hub = Rpc.SetHub(in_rpc_create_hub);

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

            VpnRpcCreateHub in_rpc_create_hub = new VpnRpcCreateHub()
            {
                HubName_str = hub_name,
            };

            VpnRpcCreateHub out_rpc_create_hub = Rpc.GetHub(in_rpc_create_hub);

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

            VpnRpcDeleteHub in_rpc_delete_hub = new VpnRpcDeleteHub()
            {
                HubName_str = hub_name,
            };
            VpnRpcDeleteHub out_rpc_delete_hub = Rpc.DeleteHub(in_rpc_delete_hub);

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

            VpnRpcRadius in_rpc_radius = new VpnRpcRadius()
            {
                HubName_str = hub_name,
            };
            VpnRpcRadius out_rpc_radius = Rpc.GetHubRadius(in_rpc_radius);

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

            VpnRpcRadius in_rpc_radius = new VpnRpcRadius()
            {
                HubName_str = hub_name,
                RadiusServerName_str = "1.2.3.4",
                RadiusPort_u32 = 1234,
                RadiusSecret_str = "microsoft",
                RadiusRetryInterval_u32 = 1000,
            };
            VpnRpcRadius out_rpc_radius = Rpc.SetHubRadius(in_rpc_radius);

            print_object(out_rpc_radius);

            Console.WriteLine("End: Test_SetHubRadius");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'EnumConnection', Enumerate connections
        /// </summary>
        public VpnRpcEnumConnection Test_EnumConnection()
        {
            Console.WriteLine("Begin: Test_EnumConnection");

            VpnRpcEnumConnection out_rpc_enum_connection = Rpc.EnumConnection();

            print_object(out_rpc_enum_connection);

            Console.WriteLine("End: Test_EnumConnection");
            Console.WriteLine("-----");
            Console.WriteLine();

            return out_rpc_enum_connection;
        }

        /// <summary>
        /// API test for 'DisconnectConnection', Disconnect a connection
        /// </summary>
        public void Test_DisconnectConnection()
        {
            Console.WriteLine("Begin: Test_DisconnectConnection");

            VpnRpcDisconnectConnection in_rpc_disconnect_connection = new VpnRpcDisconnectConnection()
            {
                Name_str = "CID-32-5791811E91",
            };
            VpnRpcDisconnectConnection out_rpc_disconnect_connection = Rpc.DisconnectConnection(in_rpc_disconnect_connection);

            print_object(out_rpc_disconnect_connection);

            Console.WriteLine("End: Test_DisconnectConnection");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'GetConnectionInfo', Get connection information
        /// </summary>
        public void Test_GetConnectionInfo(string name)
        {
            Console.WriteLine("Begin: Test_GetConnectionInfo");

            VpnRpcConnectionInfo in_rpc_connection_info = new VpnRpcConnectionInfo()
            {
                Name_str = name,
            };
            VpnRpcConnectionInfo out_rpc_connection_info = Rpc.GetConnectionInfo(in_rpc_connection_info);

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

            VpnRpcSetHubOnline in_rpc_set_hub_online = new VpnRpcSetHubOnline()
            {
                HubName_str = hub_name,
                Online_bool = true,
            };
            VpnRpcSetHubOnline out_rpc_set_hub_online = Rpc.SetHubOnline(in_rpc_set_hub_online);

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

            VpnRpcHubStatus in_rpc_hub_status = new VpnRpcHubStatus()
            {
                HubName_str = hub_name,
            };
            VpnRpcHubStatus out_rpc_hub_status = Rpc.GetHubStatus(in_rpc_hub_status);

            print_object(out_rpc_hub_status);

            Console.WriteLine("End: Test_GetHubStatus");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'SetHubLog', Set logging configuration into the hub
        /// </summary>
        public void Test_SetHubLog(VpnRpcHubLog in_rpc_hub_log)
        {
            Console.WriteLine("Begin: Test_SetHubLog");

            VpnRpcHubLog out_rpc_hub_log = Rpc.SetHubLog(in_rpc_hub_log);

            print_object(out_rpc_hub_log);

            Console.WriteLine("End: Test_SetHubLog");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'GetHubLog', Get logging configuration of the hub
        /// </summary>
        public VpnRpcHubLog Test_GetHubLog()
        {
            Console.WriteLine("Begin: Test_GetHubLog");

            VpnRpcHubLog in_rpc_hub_log = new VpnRpcHubLog()
            {
                HubName_str = hub_name,
            };
            VpnRpcHubLog out_rpc_hub_log = Rpc.GetHubLog(in_rpc_hub_log);

            print_object(out_rpc_hub_log);

            Console.WriteLine("End: Test_GetHubLog");
            Console.WriteLine("-----");
            Console.WriteLine();

            return out_rpc_hub_log;
        }

        /// <summary>
        /// API test for 'AddCa', Add CA(Certificate Authority) into the hub
        /// </summary>
        public void Test_AddCa()
        {
            Console.WriteLine("Begin: Test_AddCa");

            VpnRpcHubAddCA in_rpc_hub_add_ca = new VpnRpcHubAddCA()
            {
                HubName_str = hub_name,
                Cert_bin = File.ReadAllBytes(@"c:\tmp\a.cer"),
            };
            VpnRpcHubAddCA out_rpc_hub_add_ca = Rpc.AddCa(in_rpc_hub_add_ca);

            print_object(out_rpc_hub_add_ca);

            Console.WriteLine("End: Test_AddCa");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'EnumCa', Enumerate CA(Certificate Authority) in the hub
        /// </summary>
        public VpnRpcHubEnumCA Test_EnumCa()
        {
            Console.WriteLine("Begin: Test_EnumCa");

            VpnRpcHubEnumCA in_rpc_hub_enum_ca = new VpnRpcHubEnumCA()
            {
                HubName_str = hub_name,
            };
            VpnRpcHubEnumCA out_rpc_hub_enum_ca = Rpc.EnumCa(in_rpc_hub_enum_ca);

            print_object(out_rpc_hub_enum_ca);

            Console.WriteLine("End: Test_EnumCa");
            Console.WriteLine("-----");
            Console.WriteLine();

            return out_rpc_hub_enum_ca;
        }

        /// <summary>
        /// API test for 'GetCa', Get CA(Certificate Authority) setting from the hub
        /// </summary>
        public void Test_GetCa(uint key)
        {
            Console.WriteLine("Begin: Test_GetCa");

            VpnRpcHubGetCA in_rpc_hub_get_ca = new VpnRpcHubGetCA()
            {
                HubName_str = hub_name,
                Key_u32 = key,
            };
            VpnRpcHubGetCA out_rpc_hub_get_ca = Rpc.GetCa(in_rpc_hub_get_ca);

            print_object(out_rpc_hub_get_ca);

            Console.WriteLine("End: Test_GetCa");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'DeleteCa', Delete a CA(Certificate Authority) setting from the hub
        /// </summary>
        public void Test_DeleteCa(uint key)
        {
            Console.WriteLine("Begin: Test_DeleteCa");

            VpnRpcHubDeleteCA in_rpc_hub_delete_ca = new VpnRpcHubDeleteCA()
            {
                HubName_str = hub_name,
                Key_u32 = key,
            };
            VpnRpcHubDeleteCA out_rpc_hub_delete_ca = Rpc.DeleteCa(in_rpc_hub_delete_ca);

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

            VpnRpcLink in_rpc_link = new VpnRpcLink()
            {
                HubName_str = hub_name,
                AccountName_utf = "linktest",
            };
            VpnRpcLink out_rpc_link = Rpc.SetLinkOnline(in_rpc_link);

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

            VpnRpcLink in_rpc_link = new VpnRpcLink()
            {
                HubName_str = hub_name,
                AccountName_utf = "linktest",
            };
            VpnRpcLink out_rpc_link = Rpc.SetLinkOffline(in_rpc_link);

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

            VpnRpcLink in_rpc_link = new VpnRpcLink()
            {
                HubName_str = hub_name,
                AccountName_utf = "linktest2",
            };
            VpnRpcLink out_rpc_link = Rpc.DeleteLink(in_rpc_link);

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

            VpnRpcRenameLink in_rpc_rename_link = new VpnRpcRenameLink()
            {
                HubName_str = hub_name,
                OldAccountName_utf = "linktest",
                NewAccountName_utf = "linktest2",
            };
            VpnRpcRenameLink out_rpc_rename_link = Rpc.RenameLink(in_rpc_rename_link);

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

            VpnRpcCreateLink in_rpc_create_link = new VpnRpcCreateLink()
            {
                HubName_Ex_str = hub_name,
                CheckServerCert_bool = false,

                ClientOption_AccountName_utf = "linktest",
                ClientOption_Hostname_str = "vpn1.sehosts.com",
                ClientOption_Port_u32 = 443,
                ClientOption_ProxyType_u32 = 0,
                ClientOption_NumRetry_u32 = uint.MaxValue,
                ClientOption_RetryInterval_u32 = 5,
                ClientOption_HubName_str = "SoftEther Network",
                ClientOption_MaxConnection_u32 = 16,
                ClientOption_UseEncrypt_bool = true,
                ClientOption_UseCompress_bool = false,
                ClientOption_HalfConnection_bool = true,
                ClientOption_AdditionalConnectionInterval_u32 = 2,
                ClientOption_ConnectionDisconnectSpan_u32 = 24,

                ClientAuth_AuthType_u32 = VpnRpcClientAuthType.PlainPassword,
                ClientAuth_Username_str = "181012",
                ClientAuth_PlainPassword_str = "microsoft",

                SecPol_DHCPFilter_bool = true,
                SecPol_DHCPNoServer_bool = true,
                SecPol_DHCPForce_bool = true,
                SecPol_CheckMac_bool = true,
                SecPol_CheckIP_bool = true,
                SecPol_ArpDhcpOnly_bool = true,
                SecPol_PrivacyFilter_bool = true,
                SecPol_NoServer_bool = true,
                SecPol_NoBroadcastLimiter_bool = true,
                SecPol_MaxMac_u32 = 32,
                SecPol_MaxIP_u32 = 64,
                SecPol_MaxUpload_u32 = 960000,
                SecPol_MaxDownload_u32 = 1280000,
                SecPol_RSandRAFilter_bool = true,
                SecPol_RAFilter_bool = true,
                SecPol_DHCPv6Filter_bool = true,
                SecPol_DHCPv6NoServer_bool = true,
                SecPol_CheckIPv6_bool = true,
                SecPol_NoServerV6_bool = true,
                SecPol_MaxIPv6_u32 = 127,
                SecPol_FilterIPv4_bool = true,
                SecPol_FilterIPv6_bool = true,
                SecPol_FilterNonIP_bool = true,
                SecPol_NoIPv6DefaultRouterInRA_bool = true,
                SecPol_VLanId_u32 = 123,
            };
            VpnRpcCreateLink out_rpc_create_link = Rpc.CreateLink(in_rpc_create_link);

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

            VpnRpcCreateLink in_rpc_create_link = new VpnRpcCreateLink()
            {
                HubName_Ex_str = hub_name,
                ClientOption_AccountName_utf = "linktest",
            };
            VpnRpcCreateLink out_rpc_create_link = Rpc.GetLink(in_rpc_create_link);

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

            VpnRpcCreateLink in_rpc_create_link = new VpnRpcCreateLink()
            {
                HubName_Ex_str = hub_name,
                CheckServerCert_bool = false,

                ClientOption_AccountName_utf = "linktest",
                ClientOption_Hostname_str = "vpn1.sehosts.com",
                ClientOption_Port_u32 = 443,
                ClientOption_ProxyType_u32 = 0,
                ClientOption_NumRetry_u32 = uint.MaxValue,
                ClientOption_RetryInterval_u32 = 5,
                ClientOption_HubName_str = "SoftEther Network",
                ClientOption_MaxConnection_u32 = 16,
                ClientOption_UseEncrypt_bool = true,
                ClientOption_UseCompress_bool = false,
                ClientOption_HalfConnection_bool = true,
                ClientOption_AdditionalConnectionInterval_u32 = 2,
                ClientOption_ConnectionDisconnectSpan_u32 = 24,

                ClientAuth_AuthType_u32 = VpnRpcClientAuthType.PlainPassword,
                ClientAuth_Username_str = "181012",
                ClientAuth_PlainPassword_str = "microsoft",

                SecPol_DHCPFilter_bool = true,
                SecPol_DHCPNoServer_bool = true,
                SecPol_DHCPForce_bool = true,
                SecPol_CheckMac_bool = true,
                SecPol_CheckIP_bool = true,
                SecPol_ArpDhcpOnly_bool = true,
                SecPol_PrivacyFilter_bool = true,
                SecPol_NoServer_bool = true,
                SecPol_NoBroadcastLimiter_bool = true,
                SecPol_MaxMac_u32 = 32,
                SecPol_MaxIP_u32 = 64,
                SecPol_MaxUpload_u32 = 960000,
                SecPol_MaxDownload_u32 = 1280000,
                SecPol_RSandRAFilter_bool = true,
                SecPol_RAFilter_bool = true,
                SecPol_DHCPv6Filter_bool = true,
                SecPol_DHCPv6NoServer_bool = true,
                SecPol_CheckIPv6_bool = true,
                SecPol_NoServerV6_bool = true,
                SecPol_MaxIPv6_u32 = 127,
                SecPol_FilterIPv4_bool = true,
                SecPol_FilterIPv6_bool = true,
                SecPol_FilterNonIP_bool = true,
                SecPol_NoIPv6DefaultRouterInRA_bool = true,
                SecPol_VLanId_u32 = 123,
            };
            VpnRpcCreateLink out_rpc_create_link = Rpc.SetLink(in_rpc_create_link);

            print_object(out_rpc_create_link);

            Console.WriteLine("End: Test_SetLink");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'EnumLink', Enumerate links
        /// </summary>
        public VpnRpcEnumLink Test_EnumLink()
        {
            Console.WriteLine("Begin: Test_EnumLink");

            VpnRpcEnumLink in_rpc_enum_link = new VpnRpcEnumLink()
            {
                HubName_str = hub_name,
            };
            VpnRpcEnumLink out_rpc_enum_link = Rpc.EnumLink(in_rpc_enum_link);

            print_object(out_rpc_enum_link);

            Console.WriteLine("End: Test_EnumLink");
            Console.WriteLine("-----");
            Console.WriteLine();

            return out_rpc_enum_link;
        }

        /// <summary>
        /// API test for 'GetLinkStatus', Get link status
        /// </summary>
        public void Test_GetLinkStatus(string name)
        {
            Console.WriteLine("Begin: Test_GetLinkStatus");

            VpnRpcLinkStatus in_rpc_link_status = new VpnRpcLinkStatus()
            {
                HubName_Ex_str = hub_name,
                AccountName_utf = name,
            };
            VpnRpcLinkStatus out_rpc_link_status = Rpc.GetLinkStatus(in_rpc_link_status);

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

            VpnRpcAddAccess in_rpc_add_access_ipv4 = new VpnRpcAddAccess()
            {
                HubName_str = hub_name,

                AccessListSingle = new VpnAccess[1]
                {
                    new VpnAccess()
                    {
                        Note_utf = "IPv4 Test",
                        Active_bool = true,
                        Priority_u32 = 100,
                        Discard_bool = true,
                        IsIPv6_bool = false,
                        SrcIpAddress_ip = "192.168.0.0",
                        SrcSubnetMask_ip = "255.255.255.0",
                        DestIpAddress_ip = "10.0.0.0",
                        DestSubnetMask_ip = "255.255.0.0",
                        Protocol_u32 = VpnIpProtocolNumber.TCP,
                        SrcPortStart_u32 = 123,
                        SrcPortEnd_u32 = 456,
                        DestPortStart_u32 = 555,
                        DestPortEnd_u32 = 666,
                        SrcUsername_str = "dnobori",
                        DestUsername_str = "nekosan",
                        CheckSrcMac_bool = true,
                        SrcMacAddress_bin = new byte[] { 1, 2, 3, 0, 0, 0 },
                        SrcMacMask_bin = new byte[] { 255, 255, 255, 0, 0, 0 },
                        CheckTcpState_bool = true,
                        Established_bool = true,
                        Delay_u32 = 10,
                        Jitter_u32 = 20,
                        Loss_u32 = 30,
                        RedirectUrl_str = "aho",
                    },
                },
            };
            VpnRpcAddAccess out_rpc_add_access_ipv4 = Rpc.AddAccess(in_rpc_add_access_ipv4);

            VpnRpcAddAccess in_rpc_add_access_ipv6 = new VpnRpcAddAccess()
            {
                HubName_str = hub_name,

                AccessListSingle = new VpnAccess[1]
                {
                    new VpnAccess()
                    {
                        Note_utf = "IPv6 Test",
                        Active_bool = true,
                        Priority_u32 = 100,
                        Discard_bool = true,
                        IsIPv6_bool = true,
                        SrcIpAddress6_bin = new byte[] { 0x20, 0x01, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
                        SrcSubnetMask6_bin = new byte[] { 0xff, 0xff, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
                        Protocol_u32 = VpnIpProtocolNumber.UDP,
                        SrcPortStart_u32 = 123,
                        SrcPortEnd_u32 = 456,
                        DestPortStart_u32 = 555,
                        DestPortEnd_u32 = 666,
                        SrcUsername_str = "dnobori",
                        DestUsername_str = "nekosan",
                        CheckSrcMac_bool = true,
                        SrcMacAddress_bin = new byte[] { 1, 2, 3, 0, 0, 0 },
                        SrcMacMask_bin = new byte[] { 255, 255, 255, 0, 0, 0 },
                        CheckTcpState_bool = true,
                        Established_bool = true,
                        Delay_u32 = 10,
                        Jitter_u32 = 20,
                        Loss_u32 = 30,
                        RedirectUrl_str = "aho",
                    },
                },
            };
            VpnRpcAddAccess out_rpc_add_access_ipv6 = Rpc.AddAccess(in_rpc_add_access_ipv6);

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

            VpnRpcDeleteAccess in_rpc_delete_access = new VpnRpcDeleteAccess()
            {
                HubName_str = hub_name,
                Id_u32 = 1,
            };
            VpnRpcDeleteAccess out_rpc_delete_access = Rpc.DeleteAccess(in_rpc_delete_access);

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

            VpnRpcEnumAccessList in_rpc_enum_access_list = new VpnRpcEnumAccessList()
            {
                HubName_str = hub_name,
            };
            VpnRpcEnumAccessList out_rpc_enum_access_list = Rpc.EnumAccess(in_rpc_enum_access_list);

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

            VpnRpcEnumAccessList in_rpc_enum_access_list = new VpnRpcEnumAccessList()
            {
                HubName_str = hub_name,
                AccessList = new VpnAccess[]
                {
                    new VpnAccess()
                    {
                        Note_utf = "IPv4 Test 2",
                        Active_bool = true,
                        Priority_u32 = 100,
                        Discard_bool = true,
                        IsIPv6_bool = false,
                        SrcIpAddress_ip = "192.168.0.0",
                        SrcSubnetMask_ip = "255.255.255.0",
                        DestIpAddress_ip = "10.0.0.0",
                        DestSubnetMask_ip = "255.255.0.0",
                        Protocol_u32 = VpnIpProtocolNumber.TCP,
                        SrcPortStart_u32 = 123,
                        SrcPortEnd_u32 = 456,
                        DestPortStart_u32 = 555,
                        DestPortEnd_u32 = 666,
                        SrcUsername_str = "dnobori",
                        DestUsername_str = "nekosan",
                        CheckSrcMac_bool = true,
                        SrcMacAddress_bin = new byte[] { 1, 2, 3, 0, 0, 0 },
                        SrcMacMask_bin = new byte[] { 255, 255, 255, 0, 0, 0 },
                        CheckTcpState_bool = true,
                        Established_bool = true,
                        Delay_u32 = 10,
                        Jitter_u32 = 20,
                        Loss_u32 = 30,
                        RedirectUrl_str = "aho",
                    },
                    new VpnAccess()
                    {
                        Note_utf = "IPv6 Test 2",
                        Active_bool = true,
                        Priority_u32 = 100,
                        Discard_bool = true,
                        IsIPv6_bool = true,
                        SrcIpAddress6_bin = new byte[] { 0x20, 0x01, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
                        SrcSubnetMask6_bin = new byte[] { 0xff, 0xff, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
                        Protocol_u32 = VpnIpProtocolNumber.UDP,
                        SrcPortStart_u32 = 123,
                        SrcPortEnd_u32 = 456,
                        DestPortStart_u32 = 555,
                        DestPortEnd_u32 = 666,
                        SrcUsername_str = "dnobori",
                        DestUsername_str = "nekosan",
                        CheckSrcMac_bool = true,
                        SrcMacAddress_bin = new byte[] { 1, 2, 3, 0, 0, 0 },
                        SrcMacMask_bin = new byte[] { 255, 255, 255, 0, 0, 0 },
                        CheckTcpState_bool = true,
                        Established_bool = true,
                        Delay_u32 = 10,
                        Jitter_u32 = 20,
                        Loss_u32 = 30,
                        RedirectUrl_str = "aho",
                    },
                }
            };
            VpnRpcEnumAccessList out_rpc_enum_access_list = Rpc.SetAccessList(in_rpc_enum_access_list);

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

            VpnRpcSetUser in_rpc_set_user = new VpnRpcSetUser()
            {
                HubName_str = hub_name,
                Name_str = "test1",
                Realname_utf = "ねこさん",
                Note_utf = "こらっ！",
                AuthType_u32 = VpnRpcUserAuthType.Password,
                Auth_Password_str = "microsoft",
                ExpireTime_dt = new DateTime(2019,1,1),
                UsePolicy_bool = true,
                SecPol_Access_bool = true,
                SecPol_DHCPNoServer_bool = true,
                SecPol_MaxMac_u32 = 32,
            };
            VpnRpcSetUser out_rpc_set_user = Rpc.CreateUser(in_rpc_set_user);

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

            VpnRpcSetUser in_rpc_set_user = new VpnRpcSetUser()
            {
                HubName_str = hub_name,
                Name_str = "test1",
                Realname_utf = "ねこさん",
                Note_utf = "こらっ！",
                GroupName_str = "group1",
                AuthType_u32 = VpnRpcUserAuthType.Anonymous,
                Auth_Password_str = "microsoft",
                ExpireTime_dt = new DateTime(2019, 1, 1),
                UsePolicy_bool = true,
                SecPol_Access_bool = true,
                SecPol_DHCPNoServer_bool = true,
                SecPol_MaxMac_u32 = 16,
            };
            VpnRpcSetUser out_rpc_set_user = Rpc.SetUser(in_rpc_set_user);

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

            VpnRpcSetUser in_rpc_set_user = new VpnRpcSetUser()
            {
                HubName_str = hub_name,
                Name_str = "test1",
            };
            VpnRpcSetUser out_rpc_set_user = Rpc.GetUser(in_rpc_set_user);

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

            VpnRpcDeleteUser in_rpc_delete_user = new VpnRpcDeleteUser()
            {
                HubName_str = hub_name,
                Name_str = "test1",
            };
            VpnRpcDeleteUser out_rpc_delete_user = Rpc.DeleteUser(in_rpc_delete_user);

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

            VpnRpcEnumUser in_rpc_enum_user = new VpnRpcEnumUser()
            {
                HubName_str = hub_name,
            };
            VpnRpcEnumUser out_rpc_enum_user = Rpc.EnumUser(in_rpc_enum_user);

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

            VpnRpcSetGroup in_rpc_set_group = new VpnRpcSetGroup()
            {
                HubName_str = hub_name,
                Name_str = "group1",
                Realname_utf = "ねこグループ",
                Note_utf = "これは これは",
            };
            VpnRpcSetGroup out_rpc_set_group = Rpc.CreateGroup(in_rpc_set_group);

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

            VpnRpcSetGroup in_rpc_set_group = new VpnRpcSetGroup()
            {
                HubName_str = hub_name,
                Name_str = "group1",
                Realname_utf = "ねこグループ A",
                Note_utf = "これは これは A",
            };
            VpnRpcSetGroup out_rpc_set_group = Rpc.SetGroup(in_rpc_set_group);

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

            VpnRpcSetGroup in_rpc_set_group = new VpnRpcSetGroup()
            {
                HubName_str = hub_name,
                Name_str = "group1",
            };
            VpnRpcSetGroup out_rpc_set_group = Rpc.GetGroup(in_rpc_set_group);

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

            VpnRpcDeleteUser in_rpc_delete_user = new VpnRpcDeleteUser()
            {
                HubName_str = hub_name,
                Name_str = "group1",
            };
            VpnRpcDeleteUser out_rpc_delete_user = Rpc.DeleteGroup(in_rpc_delete_user);

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

            VpnRpcEnumGroup in_rpc_enum_group = new VpnRpcEnumGroup()
            {
                HubName_str = hub_name,
            };
            VpnRpcEnumGroup out_rpc_enum_group = Rpc.EnumGroup(in_rpc_enum_group);

            print_object(out_rpc_enum_group);

            Console.WriteLine("End: Test_EnumGroup");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'EnumSession', Enumerate sessions
        /// </summary>
        public VpnRpcEnumSession Test_EnumSession()
        {
            Console.WriteLine("Begin: Test_EnumSession");

            VpnRpcEnumSession in_rpc_enum_session = new VpnRpcEnumSession()
            {
                HubName_str = hub_name,
            };
            VpnRpcEnumSession out_rpc_enum_session = Rpc.EnumSession(in_rpc_enum_session);

            print_object(out_rpc_enum_session);

            Console.WriteLine("End: Test_EnumSession");
            Console.WriteLine("-----");
            Console.WriteLine();

            return out_rpc_enum_session;
        }

        /// <summary>
        /// API test for 'GetSessionStatus', Get session status
        /// </summary>
        public void Test_GetSessionStatus(string session_name)
        {
            Console.WriteLine("Begin: Test_GetSessionStatus");

            VpnRpcSessionStatus in_rpc_session_status = new VpnRpcSessionStatus()
            {
                HubName_str = hub_name,
                Name_str = session_name,
            };
            VpnRpcSessionStatus out_rpc_session_status = Rpc.GetSessionStatus(in_rpc_session_status);

            print_object(out_rpc_session_status);

            Console.WriteLine("End: Test_GetSessionStatus");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'DeleteSession', Delete a session
        /// </summary>
        public void Test_DeleteSession(string session_id)
        {
            Console.WriteLine("Begin: Test_DeleteSession");

            VpnRpcDeleteSession in_rpc_delete_session = new VpnRpcDeleteSession()
            {
                HubName_str = hub_name,
                Name_str = session_id,
            };
            VpnRpcDeleteSession out_rpc_delete_session = Rpc.DeleteSession(in_rpc_delete_session);

            print_object(out_rpc_delete_session);

            Console.WriteLine("End: Test_DeleteSession");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'EnumMacTable', Get MAC address table
        /// </summary>
        public VpnRpcEnumMacTable Test_EnumMacTable()
        {
            Console.WriteLine("Begin: Test_EnumMacTable");

            VpnRpcEnumMacTable in_rpc_enum_mac_table = new VpnRpcEnumMacTable()
            {
                HubName_str = hub_name,
            };
            VpnRpcEnumMacTable out_rpc_enum_mac_table = Rpc.EnumMacTable(in_rpc_enum_mac_table);

            print_object(out_rpc_enum_mac_table);

            Console.WriteLine("End: Test_EnumMacTable");
            Console.WriteLine("-----");
            Console.WriteLine();

            return out_rpc_enum_mac_table;
        }

        /// <summary>
        /// API test for 'DeleteMacTable', Delete MAC address table entry
        /// </summary>
        public void Test_DeleteMacTable(uint key32)
        {
            Console.WriteLine("Begin: Test_DeleteMacTable");

            VpnRpcDeleteTable in_rpc_delete_table = new VpnRpcDeleteTable()
            {
                HubName_str = hub_name,
                Key_u32 = key32,
            };
            VpnRpcDeleteTable out_rpc_delete_table = Rpc.DeleteMacTable(in_rpc_delete_table);

            Console.WriteLine("End: Test_DeleteMacTable");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'EnumIpTable', Get IP address table
        /// </summary>
        public VpnRpcEnumIpTable Test_EnumIpTable()
        {
            Console.WriteLine("Begin: Test_EnumIpTable");

            VpnRpcEnumIpTable in_rpc_enum_ip_table = new VpnRpcEnumIpTable()
            {
                HubName_str = hub_name,
            };
            VpnRpcEnumIpTable out_rpc_enum_ip_table = Rpc.EnumIpTable(in_rpc_enum_ip_table);

            print_object(out_rpc_enum_ip_table);

            Console.WriteLine("End: Test_EnumIpTable");
            Console.WriteLine("-----");
            Console.WriteLine();

            return out_rpc_enum_ip_table;
        }

        /// <summary>
        /// API test for 'DeleteIpTable', Delete IP address table entry
        /// </summary>
        public void Test_DeleteIpTable(uint key32)
        {
            Console.WriteLine("Begin: Test_DeleteIpTable");

            VpnRpcDeleteTable in_rpc_delete_table = new VpnRpcDeleteTable()
            {
                HubName_str = hub_name,
                Key_u32 = key32,
            };
            VpnRpcDeleteTable out_rpc_delete_table = Rpc.DeleteIpTable(in_rpc_delete_table);

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

            VpnRpcKeep in_rpc_keep = new VpnRpcKeep()
            {
                UseKeepConnect_bool = true,
                KeepConnectHost_str = "www.softether.org",
                KeepConnectPort_u32 = 123,
                KeepConnectProtocol_u32 = VpnRpcKeepAliveProtocol.UDP,
                KeepConnectInterval_u32 = 1,
            };
            VpnRpcKeep out_rpc_keep = Rpc.SetKeep(in_rpc_keep);

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

            VpnRpcKeep in_rpc_keep = new VpnRpcKeep()
            {
            };
            VpnRpcKeep out_rpc_keep = Rpc.GetKeep(in_rpc_keep);

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

            VpnRpcHub in_rpc_hub = new VpnRpcHub()
            {
                HubName_str = hub_name,
            };
            VpnRpcHub out_rpc_hub = Rpc.EnableSecureNAT(in_rpc_hub);

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

            VpnRpcHub in_rpc_hub = new VpnRpcHub()
            {
                HubName_str = hub_name,
            };
            VpnRpcHub out_rpc_hub = Rpc.DisableSecureNAT(in_rpc_hub);

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

            VpnVhOption in_vh_option = new VpnVhOption()
            {
                RpcHubName_str = hub_name,
                MacAddress_bin = new byte[] { 0x00, 0xAC, 0x00, 0x11, 0x22, 0x33 },
                Ip_ip = "10.0.0.254",
                Mask_ip = "255.255.255.0",
                UseNat_bool = true,
                Mtu_u32 = 1200,
                NatTcpTimeout_u32 = 100,
                NatUdpTimeout_u32 = 50,
                UseDhcp_bool = true,
                DhcpLeaseIPStart_ip = "10.0.0.101",
                DhcpLeaseIPEnd_ip = "10.0.0.199",
                DhcpSubnetMask_ip = "255.255.255.0",
                DhcpExpireTimeSpan_u32 = 3600,
                DhcpGatewayAddress_ip = "10.0.0.254",
                DhcpDnsServerAddress_ip = "10.0.0.254",
                DhcpDnsServerAddress2_ip = "8.8.8.8",
                DhcpDomainName_str = "lab.coe.ad.jp",
                SaveLog_bool = true,
                ApplyDhcpPushRoutes_bool = true,
                DhcpPushRoutes_str = "10.1.0.0/255.255.0.0/192.168.0.1 , 10.2.0.0/255.255.0.0/192.168.0.2",
            };
            VpnVhOption out_vh_option = Rpc.SetSecureNATOption(in_vh_option);

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

            VpnVhOption in_vh_option = new VpnVhOption()
            {
                RpcHubName_str = hub_name,
            };
            VpnVhOption out_vh_option = Rpc.GetSecureNATOption(in_vh_option);

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

            VpnRpcEnumNat in_rpc_enum_nat = new VpnRpcEnumNat()
            {
                HubName_str = hub_name,
            };
            VpnRpcEnumNat out_rpc_enum_nat = Rpc.EnumNAT(in_rpc_enum_nat);

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

            VpnRpcEnumDhcp in_rpc_enum_dhcp = new VpnRpcEnumDhcp()
            {
                HubName_str = hub_name,
            };
            VpnRpcEnumDhcp out_rpc_enum_dhcp = Rpc.EnumDHCP(in_rpc_enum_dhcp);

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

            VpnRpcNatStatus in_rpc_nat_status = new VpnRpcNatStatus()
            {
                HubName_str = hub_name,
            };
            VpnRpcNatStatus out_rpc_nat_status = Rpc.GetSecureNATStatus(in_rpc_nat_status);

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

            VpnRpcLocalBridge in_rpc_localbridge = new VpnRpcLocalBridge()
            {
                DeviceName_str = "Intel(R) Ethernet Connection (2) I219-V (ID=3632031273)",
                HubNameLB_str = "test",
            };
            VpnRpcLocalBridge out_rpc_localbridge = Rpc.AddLocalBridge(in_rpc_localbridge);

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

            VpnRpcLocalBridge in_rpc_localbridge = new VpnRpcLocalBridge()
            {
                DeviceName_str = "Intel(R) Ethernet Connection (2) I219-V (ID=3632031273)",
                HubNameLB_str = "test",
            };
            VpnRpcLocalBridge out_rpc_localbridge = Rpc.DeleteLocalBridge(in_rpc_localbridge);

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

            VpnRpcTest in_rpc_test = new VpnRpcTest()
            {
            };
            VpnRpcTest out_rpc_test = Rpc.RebootServer(in_rpc_test);

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

            VpnRpcConfig in_rpc_config = new VpnRpcConfig()
            {
            };
            VpnRpcConfig out_rpc_config = Rpc.GetConfig(in_rpc_config);

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

            VpnRpcConfig in_rpc_config = new VpnRpcConfig()
            {
            };
            VpnRpcConfig out_rpc_config = Rpc.SetConfig(in_rpc_config);

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

            VpnRpcAdminOption in_rpc_admin_option = new VpnRpcAdminOption()
            {
            };
            VpnRpcAdminOption out_rpc_admin_option = Rpc.GetDefaultHubAdminOptions(in_rpc_admin_option);

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

            VpnRpcAdminOption in_rpc_admin_option = new VpnRpcAdminOption()
            {
            };
            VpnRpcAdminOption out_rpc_admin_option = Rpc.GetHubAdminOptions(in_rpc_admin_option);

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

            VpnRpcAdminOption in_rpc_admin_option = new VpnRpcAdminOption()
            {
            };
            VpnRpcAdminOption out_rpc_admin_option = Rpc.SetHubAdminOptions(in_rpc_admin_option);

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

            VpnRpcAdminOption in_rpc_admin_option = new VpnRpcAdminOption()
            {
            };
            VpnRpcAdminOption out_rpc_admin_option = Rpc.GetHubExtOptions(in_rpc_admin_option);

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

            VpnRpcAdminOption in_rpc_admin_option = new VpnRpcAdminOption()
            {
            };
            VpnRpcAdminOption out_rpc_admin_option = Rpc.SetHubExtOptions(in_rpc_admin_option);

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

            VpnRpcL3Sw in_rpc_l3sw = new VpnRpcL3Sw()
            {
            };
            VpnRpcL3Sw out_rpc_l3sw = Rpc.AddL3Switch(in_rpc_l3sw);

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

            VpnRpcL3Sw in_rpc_l3sw = new VpnRpcL3Sw()
            {
            };
            VpnRpcL3Sw out_rpc_l3sw = Rpc.DelL3Switch(in_rpc_l3sw);

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

            VpnRpcEnumL3Sw in_rpc_enum_l3sw = new VpnRpcEnumL3Sw()
            {
            };
            VpnRpcEnumL3Sw out_rpc_enum_l3sw = Rpc.EnumL3Switch(in_rpc_enum_l3sw);

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

            VpnRpcL3Sw in_rpc_l3sw = new VpnRpcL3Sw()
            {
            };
            VpnRpcL3Sw out_rpc_l3sw = Rpc.StartL3Switch(in_rpc_l3sw);

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

            VpnRpcL3Sw in_rpc_l3sw = new VpnRpcL3Sw()
            {
            };
            VpnRpcL3Sw out_rpc_l3sw = Rpc.StopL3Switch(in_rpc_l3sw);

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

            VpnRpcL3If in_rpc_l3if = new VpnRpcL3If()
            {
            };
            VpnRpcL3If out_rpc_l3if = Rpc.AddL3If(in_rpc_l3if);

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

            VpnRpcL3If in_rpc_l3if = new VpnRpcL3If()
            {
            };
            VpnRpcL3If out_rpc_l3if = Rpc.DelL3If(in_rpc_l3if);

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

            VpnRpcEnumL3If in_rpc_enum_l3if = new VpnRpcEnumL3If()
            {
            };
            VpnRpcEnumL3If out_rpc_enum_l3if = Rpc.EnumL3If(in_rpc_enum_l3if);

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

            VpnRpcL3Table in_rpc_l3table = new VpnRpcL3Table()
            {
            };
            VpnRpcL3Table out_rpc_l3table = Rpc.AddL3Table(in_rpc_l3table);

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

            VpnRpcL3Table in_rpc_l3table = new VpnRpcL3Table()
            {
            };
            VpnRpcL3Table out_rpc_l3table = Rpc.DelL3Table(in_rpc_l3table);

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

            VpnRpcEnumL3Table in_rpc_enum_l3table = new VpnRpcEnumL3Table()
            {
            };
            VpnRpcEnumL3Table out_rpc_enum_l3table = Rpc.EnumL3Table(in_rpc_enum_l3table);

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

            VpnRpcEnumCrl in_rpc_enum_crl = new VpnRpcEnumCrl()
            {
            };
            VpnRpcEnumCrl out_rpc_enum_crl = Rpc.EnumCrl(in_rpc_enum_crl);

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

            VpnRpcCrl in_rpc_crl = new VpnRpcCrl()
            {
            };
            VpnRpcCrl out_rpc_crl = Rpc.AddCrl(in_rpc_crl);

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

            VpnRpcCrl in_rpc_crl = new VpnRpcCrl()
            {
            };
            VpnRpcCrl out_rpc_crl = Rpc.DelCrl(in_rpc_crl);

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

            VpnRpcCrl in_rpc_crl = new VpnRpcCrl()
            {
            };
            VpnRpcCrl out_rpc_crl = Rpc.GetCrl(in_rpc_crl);

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

            VpnRpcCrl in_rpc_crl = new VpnRpcCrl()
            {
            };
            VpnRpcCrl out_rpc_crl = Rpc.SetCrl(in_rpc_crl);

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

            VpnRpcAcList in_rpc_ac_list = new VpnRpcAcList()
            {
            };
            VpnRpcAcList out_rpc_ac_list = Rpc.SetAcList(in_rpc_ac_list);

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

            VpnRpcAcList in_rpc_ac_list = new VpnRpcAcList()
            {
            };
            VpnRpcAcList out_rpc_ac_list = Rpc.GetAcList(in_rpc_ac_list);

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

            VpnRpcEnumLogFile in_rpc_enum_log_file = new VpnRpcEnumLogFile()
            {
            };
            VpnRpcEnumLogFile out_rpc_enum_log_file = Rpc.EnumLogFile(in_rpc_enum_log_file);

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

            VpnRpcReadLogFile in_rpc_read_log_file = new VpnRpcReadLogFile()
            {
            };
            VpnRpcReadLogFile out_rpc_read_log_file = Rpc.ReadLogFile(in_rpc_read_log_file);

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

            VpnRpcTest in_rpc_test = new VpnRpcTest()
            {
            };
            VpnRpcTest out_rpc_test = Rpc.AddLicenseKey(in_rpc_test);

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

            VpnRpcTest in_rpc_test = new VpnRpcTest()
            {
            };
            VpnRpcTest out_rpc_test = Rpc.DelLicenseKey(in_rpc_test);

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

            VpnRpcEnumLicenseKey in_rpc_enum_license_key = new VpnRpcEnumLicenseKey()
            {
            };
            VpnRpcEnumLicenseKey out_rpc_enum_license_key = Rpc.EnumLicenseKey(in_rpc_enum_license_key);

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

            VpnRpcLicenseStatus in_rpc_license_status = new VpnRpcLicenseStatus()
            {
            };
            VpnRpcLicenseStatus out_rpc_license_status = Rpc.GetLicenseStatus(in_rpc_license_status);

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

            VpnSyslogSetting in_syslog_setting = new VpnSyslogSetting()
            {
            };
            VpnSyslogSetting out_syslog_setting = Rpc.SetSysLog(in_syslog_setting);

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

            VpnSyslogSetting in_syslog_setting = new VpnSyslogSetting()
            {
            };
            VpnSyslogSetting out_syslog_setting = Rpc.GetSysLog(in_syslog_setting);

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

            VpnRpcEnumEthVlan in_rpc_enum_eth_vlan = new VpnRpcEnumEthVlan()
            {
            };
            VpnRpcEnumEthVlan out_rpc_enum_eth_vlan = Rpc.EnumEthVLan(in_rpc_enum_eth_vlan);

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

            VpnRpcTest in_rpc_test = new VpnRpcTest()
            {
            };
            VpnRpcTest out_rpc_test = Rpc.SetEnableEthVLan(in_rpc_test);

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

            VpnRpcMsg in_rpc_msg = new VpnRpcMsg()
            {
            };
            VpnRpcMsg out_rpc_msg = Rpc.SetHubMsg(in_rpc_msg);

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

            VpnRpcMsg in_rpc_msg = new VpnRpcMsg()
            {
            };
            VpnRpcMsg out_rpc_msg = Rpc.GetHubMsg(in_rpc_msg);

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

            VpnRpcTest in_rpc_test = new VpnRpcTest()
            {
            };
            VpnRpcTest out_rpc_test = Rpc.Crash(in_rpc_test);

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

            VpnRpcMsg in_rpc_msg = new VpnRpcMsg()
            {
            };
            VpnRpcMsg out_rpc_msg = Rpc.GetAdminMsg(in_rpc_msg);

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

            VpnRpcTest in_rpc_test = new VpnRpcTest()
            {
            };
            VpnRpcTest out_rpc_test = Rpc.Flush(in_rpc_test);

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

            VpnRpcTest in_rpc_test = new VpnRpcTest()
            {
            };
            VpnRpcTest out_rpc_test = Rpc.Debug(in_rpc_test);

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

            VpnIPsecServices in_ipsec_services = new VpnIPsecServices()
            {
            };
            VpnIPsecServices out_ipsec_services = Rpc.SetIPsecServices(in_ipsec_services);

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

            VpnIPsecServices in_ipsec_services = new VpnIPsecServices()
            {
            };
            VpnIPsecServices out_ipsec_services = Rpc.GetIPsecServices(in_ipsec_services);

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

            VpnEtherIpId in_etherip_id = new VpnEtherIpId()
            {
            };
            VpnEtherIpId out_etherip_id = Rpc.AddEtherIpId(in_etherip_id);

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

            VpnEtherIpId in_etherip_id = new VpnEtherIpId()
            {
            };
            VpnEtherIpId out_etherip_id = Rpc.GetEtherIpId(in_etherip_id);

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

            VpnEtherIpId in_etherip_id = new VpnEtherIpId()
            {
            };
            VpnEtherIpId out_etherip_id = Rpc.DeleteEtherIpId(in_etherip_id);

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

            VpnRpcEnumEtherIpId in_rpc_enum_etherip_id = new VpnRpcEnumEtherIpId()
            {
            };
            VpnRpcEnumEtherIpId out_rpc_enum_etherip_id = Rpc.EnumEtherIpId(in_rpc_enum_etherip_id);

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

            VpnOpenVpnSstpConfig in_openvpn_sstp_config = new VpnOpenVpnSstpConfig()
            {
            };
            VpnOpenVpnSstpConfig out_openvpn_sstp_config = Rpc.SetOpenVpnSstpConfig(in_openvpn_sstp_config);

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

            VpnOpenVpnSstpConfig in_openvpn_sstp_config = new VpnOpenVpnSstpConfig()
            {
            };
            VpnOpenVpnSstpConfig out_openvpn_sstp_config = Rpc.GetOpenVpnSstpConfig(in_openvpn_sstp_config);

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

            VpnDDnsClientStatus in_ddns_client_status = new VpnDDnsClientStatus()
            {
            };
            VpnDDnsClientStatus out_ddns_client_status = Rpc.GetDDnsClientStatus(in_ddns_client_status);

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

            VpnRpcTest in_rpc_test = new VpnRpcTest()
            {
            };
            VpnRpcTest out_rpc_test = Rpc.ChangeDDnsClientHostname(in_rpc_test);

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

            VpnRpcTest in_rpc_test = new VpnRpcTest()
            {
            };
            VpnRpcTest out_rpc_test = Rpc.RegenerateServerCert(in_rpc_test);

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

            VpnRpcReadLogFile in_rpc_read_log_file = new VpnRpcReadLogFile()
            {
            };
            VpnRpcReadLogFile out_rpc_read_log_file = Rpc.MakeOpenVpnConfigFile(in_rpc_read_log_file);

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

            VpnRpcSpecialListener in_rpc_special_listener = new VpnRpcSpecialListener()
            {
            };
            VpnRpcSpecialListener out_rpc_special_listener = Rpc.SetSpecialListener(in_rpc_special_listener);

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

            VpnRpcSpecialListener in_rpc_special_listener = new VpnRpcSpecialListener()
            {
            };
            VpnRpcSpecialListener out_rpc_special_listener = Rpc.GetSpecialListener(in_rpc_special_listener);

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

            VpnRpcAzureStatus in_rpc_azure_status = new VpnRpcAzureStatus()
            {
            };
            VpnRpcAzureStatus out_rpc_azure_status = Rpc.GetAzureStatus(in_rpc_azure_status);

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

            VpnRpcAzureStatus in_rpc_azure_status = new VpnRpcAzureStatus()
            {
            };
            VpnRpcAzureStatus out_rpc_azure_status = Rpc.SetAzureStatus(in_rpc_azure_status);

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

            VpnInternetSetting in_internet_setting = new VpnInternetSetting()
            {
            };
            VpnInternetSetting out_internet_setting = Rpc.GetDDnsInternetSettng(in_internet_setting);

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

            VpnInternetSetting in_internet_setting = new VpnInternetSetting()
            {
            };
            VpnInternetSetting out_internet_setting = Rpc.SetDDnsInternetSettng(in_internet_setting);

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

            VpnVgsConfig in_vgs_config = new VpnVgsConfig()
            {
            };
            VpnVgsConfig out_vgs_config = Rpc.SetVgsConfig(in_vgs_config);

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

            VpnVgsConfig in_vgs_config = new VpnVgsConfig()
            {
            };
            VpnVgsConfig out_vgs_config = Rpc.GetVgsConfig(in_vgs_config);

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
