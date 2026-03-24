using System;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json;
using SoftEther.JsonRpc;
using SoftEther.VPNClientRpc;
using System.Text;

namespace DNT_VPN_JSONRPC
{
    class VPNRPCTest
    {
        VpnClientRpc Rpc;

        Random rand = new Random();

        string hub_name;

        public VPNRPCTest()
        {
            Rpc = new VpnClientRpc("127.0.0.1", 9999, "microsoft", "");
        }

        public void Test_All()
        {
            Test_GetClientVersion();

            Test_GetCmSetting();
            Test_SetCmSetting(true);
            Test_GetCmSetting();
            Test_SetCmSetting(false);
            Test_GetCmSetting();

            Test_GetPasswordSetting();
            Test_SetPassword(true);
            Test_GetPasswordSetting();
            Test_SetPassword(false);
            Test_GetPasswordSetting();

            Test_EnumCa();
            Test_AddCa();
            Test_EnumCa();
            return;

            Test_DeleteCa();
            Test_GetCa();
            Test_EnumSecure();
            Test_UseSecure();
            Test_GetUseSecure();
            Test_EnumObjectInSecure();
            Test_CreateVLan();
            Test_UpgradeVLan();
            Test_GetVLan();
            Test_SetVLan();
            Test_EnumVLan();
            Test_DeleteVLan();
            Test_EnableVLan();
            Test_DisableVLan();
            Test_CreateAccount();
            Test_EnumAccount();
            Test_DeleteAccount();
            Test_SetStartupAccount();
            Test_RemoveStartupAccount();
            Test_GetIssuer();
            Test_VgcGetList();
            Test_VgcRefreshList();
            Test_VgcGetListStatus();
            Test_VgcGetConfig();
            Test_VgcSetConfig();
            Test_VgcConnect();
            Test_GetCommonProxySetting();
            Test_SetCommonProxySetting();
            Test_SetAccount();
            Test_GetAccount();
            Test_RenameAccount();
            Test_SetClientConfig();
            Test_GetClientConfig();
            Test_Connect();
            Test_Disconnect();
            Test_GetAccountStatus();
            return;
        }


        /// <summary>
        /// API test for 'GetClientVersion', Get the client version
        /// </summary>
        public void Test_GetClientVersion()
        {
            Console.WriteLine("Begin: Test_GetClientVersion");

            VpnRpcClientVersion in_rpc_client_version = new VpnRpcClientVersion()
            {
            };
            VpnRpcClientVersion out_rpc_client_version = Rpc.GetClientVersion(in_rpc_client_version);

            print_object(out_rpc_client_version);

            Console.WriteLine("End: Test_GetClientVersion");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'GetCmSetting', Get the CM_SETTING
        /// </summary>
        public void Test_GetCmSetting()
        {
            Console.WriteLine("Begin: Test_GetCmSetting");

            VpnCmSetting in_cm_setting = new VpnCmSetting()
            {
            };
            VpnCmSetting out_cm_setting = Rpc.GetCmSetting(in_cm_setting);

            print_object(out_cm_setting);

            Console.WriteLine("End: Test_GetCmSetting");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'SetCmSetting', Set the CM_SETTING
        /// </summary>
        public void Test_SetCmSetting(bool setPassword)
        {
            Console.WriteLine("Begin: Test_SetCmSetting");

            VpnCmSetting in_cm_setting = new VpnCmSetting()
            {
                HashedPassword_bin = new byte[]
                {
                    // The SHA-0 hash digest of the string "password"
                    0x80, 0x07, 0x25, 0x68, 0xBE, 0xB3, 0xB2, 0x10, 0x23, 0x25,
                    0xEB, 0x20, 0x3F, 0x6D, 0x0F, 0xF9, 0x2F, 0x5C, 0xEF, 0x8E
                },
                LockMode_bool = setPassword,
                EasyMode_bool = setPassword,
            };

            if (setPassword == false)
            {
                in_cm_setting.HashedPassword_bin = new byte[20];
            }

            VpnCmSetting out_cm_setting = Rpc.SetCmSetting(in_cm_setting);

            print_object(out_cm_setting);

            Console.WriteLine("End: Test_SetCmSetting");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'SetPassword', Set the password
        /// </summary>
        public void Test_SetPassword(bool flag)
        {
            Console.WriteLine("Begin: Test_SetPassword");

            VpnRpcClientPassword in_rpc_client_password = new VpnRpcClientPassword()
            {
                PasswordRemoteOnly_bool = flag,
                Password_str = flag ? "password" : "",
            };
            VpnRpcClientPassword out_rpc_client_password = Rpc.SetPassword(in_rpc_client_password);

            print_object(out_rpc_client_password);

            Console.WriteLine("End: Test_SetPassword");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'GetPasswordSetting', Get the password setting
        /// </summary>
        public void Test_GetPasswordSetting()
        {
            Console.WriteLine("Begin: Test_GetPasswordSetting");

            VpnRpcClientPasswordSetting in_rpc_client_password_setting = new VpnRpcClientPasswordSetting()
            {
            };
            VpnRpcClientPasswordSetting out_rpc_client_password_setting = Rpc.GetPasswordSetting(in_rpc_client_password_setting);

            print_object(out_rpc_client_password_setting);

            Console.WriteLine("End: Test_GetPasswordSetting");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'EnumCa', Enumerate the trusted CA
        /// </summary>
        public void Test_EnumCa()
        {
            Console.WriteLine("Begin: Test_EnumCa");

            VpnRpcClientEnumCA in_rpc_client_enum_ca = new VpnRpcClientEnumCA()
            {
            };
            VpnRpcClientEnumCA out_rpc_client_enum_ca = Rpc.EnumCa(in_rpc_client_enum_ca);

            print_object(out_rpc_client_enum_ca);

            Console.WriteLine("End: Test_EnumCa");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'AddCa', Add a CA certificate
        /// </summary>
        public void Test_AddCa()
        {
            Console.WriteLine("Begin: Test_AddCa");

            VpnRpcCert in_rpc_cert = new VpnRpcCert()
            {
                X_bin = new byte[]
                {
                    0x2d,0x2d,0x2d,0x2d,0x2d,0x42,0x45,0x47,0x49,0x4e,0x20,0x43,0x45,0x52,0x54,0x49,
                    0x46,0x49,0x43,0x41,0x54,0x45,0x2d,0x2d,0x2d,0x2d,0x2d,0x0a,0x4d,0x49,0x49,0x44,
                    0x72,0x6a,0x43,0x43,0x41,0x70,0x61,0x67,0x41,0x77,0x49,0x42,0x41,0x67,0x49,0x42,
                    0x41,0x44,0x41,0x4e,0x42,0x67,0x6b,0x71,0x68,0x6b,0x69,0x47,0x39,0x77,0x30,0x42,
                    0x41,0x51,0x73,0x46,0x41,0x44,0x42,0x57,0x4d,0x51,0x77,0x77,0x43,0x67,0x59,0x44,
                    0x56,0x51,0x51,0x44,0x44,0x41,0x4e,0x68,0x59,0x57,0x45,0x78,0x0a,0x46,0x54,0x41,
                    0x54,0x42,0x67,0x4e,0x56,0x42,0x41,0x6f,0x4d,0x44,0x4f,0x4f,0x42,0x72,0x2b,0x4f,
                    0x42,0x71,0x75,0x4f,0x42,0x6a,0x2b,0x4f,0x42,0x6e,0x54,0x45,0x4c,0x4d,0x41,0x6b,
                    0x47,0x41,0x31,0x55,0x45,0x42,0x68,0x4d,0x43,0x53,0x6c,0x41,0x78,0x45,0x44,0x41,
                    0x4f,0x42,0x67,0x4e,0x56,0x42,0x41,0x67,0x4d,0x42,0x30,0x6c,0x69,0x0a,0x59,0x58,
                    0x4a,0x68,0x61,0x32,0x6b,0x78,0x45,0x44,0x41,0x4f,0x42,0x67,0x4e,0x56,0x42,0x41,
                    0x63,0x4d,0x42,0x31,0x52,0x7a,0x64,0x57,0x74,0x31,0x59,0x6d,0x45,0x77,0x48,0x68,
                    0x63,0x4e,0x4d,0x54,0x67,0x78,0x4d,0x44,0x45,0x78,0x4d,0x6a,0x4d,0x7a,0x4e,0x54,
                    0x41,0x78,0x57,0x68,0x63,0x4e,0x4e,0x44,0x49,0x78,0x4d,0x44,0x41,0x31,0x0a,0x4d,
                    0x6a,0x4d,0x7a,0x4e,0x54,0x41,0x78,0x57,0x6a,0x42,0x57,0x4d,0x51,0x77,0x77,0x43,
                    0x67,0x59,0x44,0x56,0x51,0x51,0x44,0x44,0x41,0x4e,0x68,0x59,0x57,0x45,0x78,0x46,
                    0x54,0x41,0x54,0x42,0x67,0x4e,0x56,0x42,0x41,0x6f,0x4d,0x44,0x4f,0x4f,0x42,0x72,
                    0x2b,0x4f,0x42,0x71,0x75,0x4f,0x42,0x6a,0x2b,0x4f,0x42,0x6e,0x54,0x45,0x4c,0x0a,
                    0x4d,0x41,0x6b,0x47,0x41,0x31,0x55,0x45,0x42,0x68,0x4d,0x43,0x53,0x6c,0x41,0x78,
                    0x45,0x44,0x41,0x4f,0x42,0x67,0x4e,0x56,0x42,0x41,0x67,0x4d,0x42,0x30,0x6c,0x69,
                    0x59,0x58,0x4a,0x68,0x61,0x32,0x6b,0x78,0x45,0x44,0x41,0x4f,0x42,0x67,0x4e,0x56,
                    0x42,0x41,0x63,0x4d,0x42,0x31,0x52,0x7a,0x64,0x57,0x74,0x31,0x59,0x6d,0x45,0x77,
                    0x0a,0x67,0x67,0x45,0x69,0x4d,0x41,0x30,0x47,0x43,0x53,0x71,0x47,0x53,0x49,0x62,
                    0x33,0x44,0x51,0x45,0x42,0x41,0x51,0x55,0x41,0x41,0x34,0x49,0x42,0x44,0x77,0x41,
                    0x77,0x67,0x67,0x45,0x4b,0x41,0x6f,0x49,0x42,0x41,0x51,0x44,0x58,0x45,0x63,0x76,
                    0x72,0x59,0x37,0x56,0x2b,0x7a,0x64,0x42,0x79,0x72,0x64,0x4e,0x78,0x4a,0x59,0x45,
                    0x6d,0x0a,0x61,0x41,0x4e,0x59,0x55,0x4f,0x37,0x76,0x57,0x34,0x68,0x64,0x41,0x35,
                    0x49,0x42,0x49,0x46,0x6d,0x4d,0x70,0x6e,0x62,0x79,0x69,0x4e,0x6e,0x5a,0x77,0x36,
                    0x57,0x39,0x6f,0x61,0x67,0x78,0x33,0x5a,0x49,0x65,0x65,0x48,0x56,0x59,0x62,0x52,
                    0x69,0x4b,0x36,0x41,0x66,0x46,0x74,0x53,0x31,0x32,0x2b,0x45,0x31,0x4d,0x59,0x31,
                    0x64,0x32,0x0a,0x61,0x71,0x51,0x31,0x53,0x72,0x49,0x43,0x39,0x51,0x35,0x55,0x6e,
                    0x5a,0x61,0x42,0x72,0x62,0x57,0x32,0x32,0x6d,0x4e,0x75,0x6c,0x4d,0x34,0x2f,0x6c,
                    0x49,0x4a,0x72,0x48,0x70,0x51,0x55,0x68,0x50,0x78,0x6f,0x62,0x79,0x34,0x2f,0x36,
                    0x4e,0x41,0x37,0x71,0x4b,0x67,0x55,0x48,0x69,0x79,0x4f,0x64,0x33,0x4a,0x42,0x70,
                    0x4f,0x66,0x77,0x0a,0x38,0x54,0x76,0x53,0x74,0x51,0x78,0x34,0x4c,0x38,0x59,0x64,
                    0x4b,0x51,0x35,0x68,0x74,0x7a,0x6b,0x32,0x68,0x70,0x52,0x4a,0x4c,0x30,0x6c,0x4b,
                    0x67,0x47,0x31,0x57,0x34,0x75,0x4b,0x32,0x39,0x39,0x42,0x74,0x7a,0x64,0x41,0x67,
                    0x66,0x42,0x76,0x43,0x54,0x33,0x41,0x31,0x61,0x53,0x70,0x6a,0x49,0x47,0x74,0x6e,
                    0x69,0x72,0x49,0x31,0x0a,0x46,0x4c,0x52,0x58,0x47,0x79,0x38,0x31,0x31,0x57,0x4a,
                    0x39,0x4a,0x68,0x68,0x34,0x41,0x4b,0x4c,0x66,0x79,0x56,0x70,0x42,0x4a,0x67,0x65,
                    0x34,0x73,0x56,0x72,0x36,0x4e,0x75,0x75,0x49,0x66,0x32,0x71,0x47,0x31,0x6f,0x79,
                    0x31,0x30,0x70,0x61,0x51,0x4e,0x65,0x71,0x32,0x33,0x55,0x47,0x61,0x59,0x74,0x2f,
                    0x7a,0x55,0x56,0x4a,0x77,0x0a,0x55,0x74,0x30,0x57,0x45,0x6b,0x58,0x38,0x48,0x4f,
                    0x63,0x62,0x33,0x75,0x49,0x6f,0x54,0x6d,0x61,0x4f,0x34,0x72,0x48,0x42,0x55,0x4a,
                    0x71,0x45,0x79,0x39,0x51,0x58,0x7a,0x53,0x57,0x77,0x43,0x35,0x78,0x45,0x43,0x64,
                    0x37,0x43,0x4a,0x53,0x53,0x68,0x31,0x30,0x4f,0x75,0x6e,0x6c,0x75,0x4c,0x32,0x4d,
                    0x47,0x65,0x5a,0x47,0x6e,0x76,0x0a,0x41,0x67,0x4d,0x42,0x41,0x41,0x47,0x6a,0x67,
                    0x59,0x59,0x77,0x67,0x59,0x4d,0x77,0x44,0x77,0x59,0x44,0x56,0x52,0x30,0x54,0x41,
                    0x51,0x48,0x2f,0x42,0x41,0x55,0x77,0x41,0x77,0x45,0x42,0x2f,0x7a,0x41,0x4c,0x42,
                    0x67,0x4e,0x56,0x48,0x51,0x38,0x45,0x42,0x41,0x4d,0x43,0x41,0x66,0x59,0x77,0x59,
                    0x77,0x59,0x44,0x56,0x52,0x30,0x6c,0x0a,0x42,0x46,0x77,0x77,0x57,0x67,0x59,0x49,
                    0x4b,0x77,0x59,0x42,0x42,0x51,0x55,0x48,0x41,0x77,0x45,0x47,0x43,0x43,0x73,0x47,
                    0x41,0x51,0x55,0x46,0x42,0x77,0x4d,0x43,0x42,0x67,0x67,0x72,0x42,0x67,0x45,0x46,
                    0x42,0x51,0x63,0x44,0x41,0x77,0x59,0x49,0x4b,0x77,0x59,0x42,0x42,0x51,0x55,0x48,
                    0x41,0x77,0x51,0x47,0x43,0x43,0x73,0x47,0x0a,0x41,0x51,0x55,0x46,0x42,0x77,0x4d,
                    0x46,0x42,0x67,0x67,0x72,0x42,0x67,0x45,0x46,0x42,0x51,0x63,0x44,0x42,0x67,0x59,
                    0x49,0x4b,0x77,0x59,0x42,0x42,0x51,0x55,0x48,0x41,0x77,0x63,0x47,0x43,0x43,0x73,
                    0x47,0x41,0x51,0x55,0x46,0x42,0x77,0x4d,0x49,0x42,0x67,0x67,0x72,0x42,0x67,0x45,
                    0x46,0x42,0x51,0x63,0x44,0x43,0x54,0x41,0x4e,0x0a,0x42,0x67,0x6b,0x71,0x68,0x6b,
                    0x69,0x47,0x39,0x77,0x30,0x42,0x41,0x51,0x73,0x46,0x41,0x41,0x4f,0x43,0x41,0x51,
                    0x45,0x41,0x46,0x6d,0x34,0x37,0x47,0x55,0x70,0x50,0x57,0x35,0x2b,0x37,0x69,0x46,
                    0x74,0x69,0x6c,0x6f,0x6b,0x35,0x32,0x49,0x6f,0x54,0x57,0x72,0x74,0x46,0x67,0x32,
                    0x79,0x69,0x36,0x6b,0x49,0x32,0x69,0x52,0x4e,0x51,0x0a,0x4b,0x75,0x67,0x48,0x55,
                    0x49,0x4f,0x34,0x4b,0x53,0x71,0x4a,0x56,0x42,0x50,0x38,0x61,0x4b,0x4f,0x61,0x54,
                    0x5a,0x47,0x45,0x31,0x4b,0x4d,0x68,0x2f,0x59,0x6a,0x68,0x36,0x71,0x2f,0x67,0x50,
                    0x61,0x6c,0x67,0x64,0x2f,0x38,0x44,0x6d,0x72,0x78,0x53,0x4a,0x6d,0x55,0x78,0x33,
                    0x62,0x4e,0x62,0x38,0x52,0x59,0x36,0x70,0x4b,0x7a,0x74,0x0a,0x5a,0x64,0x75,0x53,
                    0x61,0x53,0x2b,0x57,0x55,0x30,0x59,0x74,0x2b,0x6c,0x47,0x35,0x76,0x56,0x67,0x61,
                    0x70,0x48,0x45,0x71,0x36,0x79,0x71,0x4c,0x62,0x65,0x56,0x78,0x51,0x4c,0x75,0x62,
                    0x54,0x69,0x6e,0x4f,0x66,0x56,0x56,0x5a,0x58,0x79,0x45,0x43,0x59,0x47,0x4d,0x73,
                    0x59,0x71,0x65,0x6e,0x4a,0x6a,0x4e,0x63,0x62,0x49,0x5a,0x4e,0x0a,0x79,0x4d,0x75,
                    0x72,0x46,0x63,0x67,0x30,0x34,0x36,0x4f,0x34,0x59,0x79,0x68,0x56,0x79,0x71,0x53,
                    0x69,0x74,0x43,0x59,0x37,0x68,0x2f,0x65,0x71,0x67,0x6b,0x50,0x4a,0x51,0x30,0x68,
                    0x6b,0x70,0x39,0x45,0x64,0x51,0x77,0x62,0x6e,0x38,0x56,0x6c,0x66,0x78,0x64,0x42,
                    0x58,0x77,0x51,0x34,0x4e,0x48,0x4b,0x30,0x4a,0x56,0x46,0x2f,0x33,0x0a,0x71,0x48,
                    0x61,0x68,0x4e,0x48,0x4f,0x35,0x64,0x62,0x4a,0x5a,0x57,0x59,0x41,0x62,0x42,0x44,
                    0x70,0x32,0x51,0x45,0x53,0x70,0x76,0x6f,0x2b,0x38,0x33,0x6c,0x68,0x34,0x64,0x6e,
                    0x58,0x6a,0x46,0x58,0x4d,0x43,0x48,0x76,0x52,0x68,0x35,0x31,0x79,0x2f,0x54,0x71,
                    0x79,0x42,0x34,0x56,0x76,0x72,0x52,0x4b,0x49,0x4b,0x74,0x54,0x6f,0x7a,0x0a,0x5a,
                    0x6a,0x48,0x59,0x49,0x63,0x62,0x6a,0x76,0x53,0x58,0x4d,0x7a,0x61,0x44,0x50,0x6a,
                    0x50,0x63,0x5a,0x47,0x6a,0x42,0x4a,0x6c,0x47,0x36,0x43,0x76,0x44,0x34,0x4c,0x6d,
                    0x59,0x7a,0x72,0x6b,0x48,0x34,0x31,0x63,0x7a,0x72,0x34,0x57,0x41,0x3d,0x3d,0x0a,
                    0x2d,0x2d,0x2d,0x2d,0x2d,0x45,0x4e,0x44,0x20,0x43,0x45,0x52,0x54,0x49,0x46,0x49,
                    0x43,0x41,0x54,0x45,0x2d,0x2d,0x2d,0x2d,0x2d,0x0a
                },
            };
            VpnRpcCert out_rpc_cert = Rpc.AddCa(in_rpc_cert);

            print_object(out_rpc_cert);

            Console.WriteLine("End: Test_AddCa");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'DeleteCa', Delete the CA certificate
        /// </summary>
        public void Test_DeleteCa()
        {
            Console.WriteLine("Begin: Test_DeleteCa");

            VpnRpcClientDeleteCA in_rpc_client_delete_ca = new VpnRpcClientDeleteCA()
            {
            };
            VpnRpcClientDeleteCA out_rpc_client_delete_ca = Rpc.DeleteCa(in_rpc_client_delete_ca);

            print_object(out_rpc_client_delete_ca);

            Console.WriteLine("End: Test_DeleteCa");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'GetCa', Get the CA certificate
        /// </summary>
        public void Test_GetCa()
        {
            Console.WriteLine("Begin: Test_GetCa");

            VpnRpcGetCA in_rpc_get_ca = new VpnRpcGetCA()
            {
            };
            VpnRpcGetCA out_rpc_get_ca = Rpc.GetCa(in_rpc_get_ca);

            print_object(out_rpc_get_ca);

            Console.WriteLine("End: Test_GetCa");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'EnumSecure', Enumeration of secure devices
        /// </summary>
        public void Test_EnumSecure()
        {
            Console.WriteLine("Begin: Test_EnumSecure");

            VpnRpcClientEnumSecure in_rpc_client_enum_secure = new VpnRpcClientEnumSecure()
            {
            };
            VpnRpcClientEnumSecure out_rpc_client_enum_secure = Rpc.EnumSecure(in_rpc_client_enum_secure);

            print_object(out_rpc_client_enum_secure);

            Console.WriteLine("End: Test_EnumSecure");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'UseSecure', Specifying a secure device to be used
        /// </summary>
        public void Test_UseSecure()
        {
            Console.WriteLine("Begin: Test_UseSecure");

            VpnRpcUseSecure in_rpc_use_secure = new VpnRpcUseSecure()
            {
            };
            VpnRpcUseSecure out_rpc_use_secure = Rpc.UseSecure(in_rpc_use_secure);

            print_object(out_rpc_use_secure);

            Console.WriteLine("End: Test_UseSecure");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'GetUseSecure', Get the secure device to be used
        /// </summary>
        public void Test_GetUseSecure()
        {
            Console.WriteLine("Begin: Test_GetUseSecure");

            VpnRpcUseSecure in_rpc_use_secure = new VpnRpcUseSecure()
            {
            };
            VpnRpcUseSecure out_rpc_use_secure = Rpc.GetUseSecure(in_rpc_use_secure);

            print_object(out_rpc_use_secure);

            Console.WriteLine("End: Test_GetUseSecure");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'EnumObjectInSecure', Enumerate objects in the secure device
        /// </summary>
        public void Test_EnumObjectInSecure()
        {
            Console.WriteLine("Begin: Test_EnumObjectInSecure");

            VpnRpcEnumObjectInSecure in_rpc_enum_object_in_secure = new VpnRpcEnumObjectInSecure()
            {
            };
            VpnRpcEnumObjectInSecure out_rpc_enum_object_in_secure = Rpc.EnumObjectInSecure(in_rpc_enum_object_in_secure);

            print_object(out_rpc_enum_object_in_secure);

            Console.WriteLine("End: Test_EnumObjectInSecure");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'CreateVLan', Create a virtual LAN card
        /// </summary>
        public void Test_CreateVLan()
        {
            Console.WriteLine("Begin: Test_CreateVLan");

            VpnRpcClientCreateVlan in_rpc_client_create_vlan = new VpnRpcClientCreateVlan()
            {
            };
            VpnRpcClientCreateVlan out_rpc_client_create_vlan = Rpc.CreateVLan(in_rpc_client_create_vlan);

            print_object(out_rpc_client_create_vlan);

            Console.WriteLine("End: Test_CreateVLan");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'UpgradeVLan', Upgrade the virtual LAN card
        /// </summary>
        public void Test_UpgradeVLan()
        {
            Console.WriteLine("Begin: Test_UpgradeVLan");

            VpnRpcClientCreateVlan in_rpc_client_create_vlan = new VpnRpcClientCreateVlan()
            {
            };
            VpnRpcClientCreateVlan out_rpc_client_create_vlan = Rpc.UpgradeVLan(in_rpc_client_create_vlan);

            print_object(out_rpc_client_create_vlan);

            Console.WriteLine("End: Test_UpgradeVLan");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'GetVLan', Get the information about the virtual LAN card
        /// </summary>
        public void Test_GetVLan()
        {
            Console.WriteLine("Begin: Test_GetVLan");

            VpnRpcClientGetVlan in_rpc_client_get_vlan = new VpnRpcClientGetVlan()
            {
            };
            VpnRpcClientGetVlan out_rpc_client_get_vlan = Rpc.GetVLan(in_rpc_client_get_vlan);

            print_object(out_rpc_client_get_vlan);

            Console.WriteLine("End: Test_GetVLan");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'SetVLan', Set the information about the virtual LAN card
        /// </summary>
        public void Test_SetVLan()
        {
            Console.WriteLine("Begin: Test_SetVLan");

            VpnRpcClientSetVlan in_rpc_client_set_vlan = new VpnRpcClientSetVlan()
            {
            };
            VpnRpcClientSetVlan out_rpc_client_set_vlan = Rpc.SetVLan(in_rpc_client_set_vlan);

            print_object(out_rpc_client_set_vlan);

            Console.WriteLine("End: Test_SetVLan");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'EnumVLan', Enumerate virtual LAN cards
        /// </summary>
        public void Test_EnumVLan()
        {
            Console.WriteLine("Begin: Test_EnumVLan");

            VpnRpcClientEnumVlan in_rpc_client_enum_vlan = new VpnRpcClientEnumVlan()
            {
            };
            VpnRpcClientEnumVlan out_rpc_client_enum_vlan = Rpc.EnumVLan(in_rpc_client_enum_vlan);

            print_object(out_rpc_client_enum_vlan);

            Console.WriteLine("End: Test_EnumVLan");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'DeleteVLan', Delete the virtual LAN card
        /// </summary>
        public void Test_DeleteVLan()
        {
            Console.WriteLine("Begin: Test_DeleteVLan");

            VpnRpcClientCreateVlan in_rpc_client_create_vlan = new VpnRpcClientCreateVlan()
            {
            };
            VpnRpcClientCreateVlan out_rpc_client_create_vlan = Rpc.DeleteVLan(in_rpc_client_create_vlan);

            print_object(out_rpc_client_create_vlan);

            Console.WriteLine("End: Test_DeleteVLan");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'EnableVLan', Start the virtual LAN card
        /// </summary>
        public void Test_EnableVLan()
        {
            Console.WriteLine("Begin: Test_EnableVLan");

            VpnRpcClientCreateVlan in_rpc_client_create_vlan = new VpnRpcClientCreateVlan()
            {
            };
            VpnRpcClientCreateVlan out_rpc_client_create_vlan = Rpc.EnableVLan(in_rpc_client_create_vlan);

            print_object(out_rpc_client_create_vlan);

            Console.WriteLine("End: Test_EnableVLan");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'DisableVLan', Stop the virtual LAN card
        /// </summary>
        public void Test_DisableVLan()
        {
            Console.WriteLine("Begin: Test_DisableVLan");

            VpnRpcClientCreateVlan in_rpc_client_create_vlan = new VpnRpcClientCreateVlan()
            {
            };
            VpnRpcClientCreateVlan out_rpc_client_create_vlan = Rpc.DisableVLan(in_rpc_client_create_vlan);

            print_object(out_rpc_client_create_vlan);

            Console.WriteLine("End: Test_DisableVLan");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'CreateAccount', Create an account
        /// </summary>
        public void Test_CreateAccount()
        {
            Console.WriteLine("Begin: Test_CreateAccount");

            VpnRpcClientCreateAccount in_rpc_client_create_account = new VpnRpcClientCreateAccount()
            {
            };
            VpnRpcClientCreateAccount out_rpc_client_create_account = Rpc.CreateAccount(in_rpc_client_create_account);

            print_object(out_rpc_client_create_account);

            Console.WriteLine("End: Test_CreateAccount");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'EnumAccount', Enumeration of accounts
        /// </summary>
        public void Test_EnumAccount()
        {
            Console.WriteLine("Begin: Test_EnumAccount");

            VpnRpcClientEnumAccount in_rpc_client_enum_account = new VpnRpcClientEnumAccount()
            {
            };
            VpnRpcClientEnumAccount out_rpc_client_enum_account = Rpc.EnumAccount(in_rpc_client_enum_account);

            print_object(out_rpc_client_enum_account);

            Console.WriteLine("End: Test_EnumAccount");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'DeleteAccount', Delete the account
        /// </summary>
        public void Test_DeleteAccount()
        {
            Console.WriteLine("Begin: Test_DeleteAccount");

            VpnRpcClientDeleteAccount in_rpc_client_delete_account = new VpnRpcClientDeleteAccount()
            {
            };
            VpnRpcClientDeleteAccount out_rpc_client_delete_account = Rpc.DeleteAccount(in_rpc_client_delete_account);

            print_object(out_rpc_client_delete_account);

            Console.WriteLine("End: Test_DeleteAccount");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'SetStartupAccount', Set the account as a start-up account
        /// </summary>
        public void Test_SetStartupAccount()
        {
            Console.WriteLine("Begin: Test_SetStartupAccount");

            VpnRpcClientDeleteAccount in_rpc_client_delete_account = new VpnRpcClientDeleteAccount()
            {
            };
            VpnRpcClientDeleteAccount out_rpc_client_delete_account = Rpc.SetStartupAccount(in_rpc_client_delete_account);

            print_object(out_rpc_client_delete_account);

            Console.WriteLine("End: Test_SetStartupAccount");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'RemoveStartupAccount', Unset the startup attribute of the account
        /// </summary>
        public void Test_RemoveStartupAccount()
        {
            Console.WriteLine("Begin: Test_RemoveStartupAccount");

            VpnRpcClientDeleteAccount in_rpc_client_delete_account = new VpnRpcClientDeleteAccount()
            {
            };
            VpnRpcClientDeleteAccount out_rpc_client_delete_account = Rpc.RemoveStartupAccount(in_rpc_client_delete_account);

            print_object(out_rpc_client_delete_account);

            Console.WriteLine("End: Test_RemoveStartupAccount");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'GetIssuer', Get the issuer
        /// </summary>
        public void Test_GetIssuer()
        {
            Console.WriteLine("Begin: Test_GetIssuer");

            VpnRpcGetIssuer in_rpc_get_issuer = new VpnRpcGetIssuer()
            {
            };
            VpnRpcGetIssuer out_rpc_get_issuer = Rpc.GetIssuer(in_rpc_get_issuer);

            print_object(out_rpc_get_issuer);

            Console.WriteLine("End: Test_GetIssuer");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'VgcGetList', Get VPN Gate list
        /// </summary>
        public void Test_VgcGetList()
        {
            Console.WriteLine("Begin: Test_VgcGetList");

            VpnVghostlist in_vghostlist = new VpnVghostlist()
            {
            };
            VpnVghostlist out_vghostlist = Rpc.VgcGetList(in_vghostlist);

            print_object(out_vghostlist);

            Console.WriteLine("End: Test_VgcGetList");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'VgcRefreshList', Refresh VPN Gate list
        /// </summary>
        public void Test_VgcRefreshList()
        {
            Console.WriteLine("Begin: Test_VgcRefreshList");

            VpnRpcTest in_rpc_test = new VpnRpcTest()
            {
            };
            VpnRpcTest out_rpc_test = Rpc.VgcRefreshList(in_rpc_test);

            print_object(out_rpc_test);

            Console.WriteLine("End: Test_VgcRefreshList");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'VgcGetListStatus', Get status of VPN Gate list
        /// </summary>
        public void Test_VgcGetListStatus()
        {
            Console.WriteLine("Begin: Test_VgcGetListStatus");

            VpnRpcTest in_rpc_test = new VpnRpcTest()
            {
            };
            VpnRpcTest out_rpc_test = Rpc.VgcGetListStatus(in_rpc_test);

            print_object(out_rpc_test);

            Console.WriteLine("End: Test_VgcGetListStatus");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'VgcGetConfig', Get configuration of VPN Gate
        /// </summary>
        public void Test_VgcGetConfig()
        {
            Console.WriteLine("Begin: Test_VgcGetConfig");

            VpnRpcTest in_rpc_test = new VpnRpcTest()
            {
            };
            VpnRpcTest out_rpc_test = Rpc.VgcGetConfig(in_rpc_test);

            print_object(out_rpc_test);

            Console.WriteLine("End: Test_VgcGetConfig");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'VgcSetConfig', Set the configuration of the VPN Gate
        /// </summary>
        public void Test_VgcSetConfig()
        {
            Console.WriteLine("Begin: Test_VgcSetConfig");

            VpnRpcTest in_rpc_test = new VpnRpcTest()
            {
            };
            VpnRpcTest out_rpc_test = Rpc.VgcSetConfig(in_rpc_test);

            print_object(out_rpc_test);

            Console.WriteLine("End: Test_VgcSetConfig");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'VgcConnect', Start connection to VPN Gate
        /// </summary>
        public void Test_VgcConnect()
        {
            Console.WriteLine("Begin: Test_VgcConnect");

            VpnRpcTest in_rpc_test = new VpnRpcTest()
            {
            };
            VpnRpcTest out_rpc_test = Rpc.VgcConnect(in_rpc_test);

            print_object(out_rpc_test);

            Console.WriteLine("End: Test_VgcConnect");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'GetCommonProxySetting', Get the common proxy settings
        /// </summary>
        public void Test_GetCommonProxySetting()
        {
            Console.WriteLine("Begin: Test_GetCommonProxySetting");

            VpnInternetSetting in_internet_setting = new VpnInternetSetting()
            {
            };
            VpnInternetSetting out_internet_setting = Rpc.GetCommonProxySetting(in_internet_setting);

            print_object(out_internet_setting);

            Console.WriteLine("End: Test_GetCommonProxySetting");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'SetCommonProxySetting', Set the common proxy settings
        /// </summary>
        public void Test_SetCommonProxySetting()
        {
            Console.WriteLine("Begin: Test_SetCommonProxySetting");

            VpnInternetSetting in_internet_setting = new VpnInternetSetting()
            {
            };
            VpnInternetSetting out_internet_setting = Rpc.SetCommonProxySetting(in_internet_setting);

            print_object(out_internet_setting);

            Console.WriteLine("End: Test_SetCommonProxySetting");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'SetAccount', Configure the account
        /// </summary>
        public void Test_SetAccount()
        {
            Console.WriteLine("Begin: Test_SetAccount");

            VpnRpcClientCreateAccount in_rpc_client_create_account = new VpnRpcClientCreateAccount()
            {
            };
            VpnRpcClientCreateAccount out_rpc_client_create_account = Rpc.SetAccount(in_rpc_client_create_account);

            print_object(out_rpc_client_create_account);

            Console.WriteLine("End: Test_SetAccount");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'GetAccount', Get the account information
        /// </summary>
        public void Test_GetAccount()
        {
            Console.WriteLine("Begin: Test_GetAccount");

            VpnRpcClientGetAccount in_rpc_client_get_account = new VpnRpcClientGetAccount()
            {
            };
            VpnRpcClientGetAccount out_rpc_client_get_account = Rpc.GetAccount(in_rpc_client_get_account);

            print_object(out_rpc_client_get_account);

            Console.WriteLine("End: Test_GetAccount");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'RenameAccount', Change the account name
        /// </summary>
        public void Test_RenameAccount()
        {
            Console.WriteLine("Begin: Test_RenameAccount");

            VpnRpcRenameAccount in_rpc_rename_account = new VpnRpcRenameAccount()
            {
            };
            VpnRpcRenameAccount out_rpc_rename_account = Rpc.RenameAccount(in_rpc_rename_account);

            print_object(out_rpc_rename_account);

            Console.WriteLine("End: Test_RenameAccount");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'SetClientConfig', Set the client configuration
        /// </summary>
        public void Test_SetClientConfig()
        {
            Console.WriteLine("Begin: Test_SetClientConfig");

            VpnClientConfig in_client_config = new VpnClientConfig()
            {
            };
            VpnClientConfig out_client_config = Rpc.SetClientConfig(in_client_config);

            print_object(out_client_config);

            Console.WriteLine("End: Test_SetClientConfig");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'GetClientConfig', Get the network client configuration
        /// </summary>
        public void Test_GetClientConfig()
        {
            Console.WriteLine("Begin: Test_GetClientConfig");

            VpnClientConfig in_client_config = new VpnClientConfig()
            {
            };
            VpnClientConfig out_client_config = Rpc.GetClientConfig(in_client_config);

            print_object(out_client_config);

            Console.WriteLine("End: Test_GetClientConfig");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'Connect', Connect
        /// </summary>
        public void Test_Connect()
        {
            Console.WriteLine("Begin: Test_Connect");

            VpnRpcClientConnect in_rpc_client_connect = new VpnRpcClientConnect()
            {
            };
            VpnRpcClientConnect out_rpc_client_connect = Rpc.Connect(in_rpc_client_connect);

            print_object(out_rpc_client_connect);

            Console.WriteLine("End: Test_Connect");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'Disconnect', Disconnect
        /// </summary>
        public void Test_Disconnect()
        {
            Console.WriteLine("Begin: Test_Disconnect");

            VpnRpcClientConnect in_rpc_client_connect = new VpnRpcClientConnect()
            {
            };
            VpnRpcClientConnect out_rpc_client_connect = Rpc.Disconnect(in_rpc_client_connect);

            print_object(out_rpc_client_connect);

            Console.WriteLine("End: Test_Disconnect");
            Console.WriteLine("-----");
            Console.WriteLine();
        }

        /// <summary>
        /// API test for 'GetAccountStatus', Get the connection status
        /// </summary>
        public void Test_GetAccountStatus()
        {
            Console.WriteLine("Begin: Test_GetAccountStatus");

            VpnRpcClientGetConnectionStatus in_rpc_client_get_connection_status = new VpnRpcClientGetConnectionStatus()
            {
            };
            VpnRpcClientGetConnectionStatus out_rpc_client_get_connection_status = Rpc.GetAccountStatus(in_rpc_client_get_connection_status);

            print_object(out_rpc_client_get_connection_status);

            Console.WriteLine("End: Test_GetAccountStatus");
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
