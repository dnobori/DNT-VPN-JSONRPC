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

namespace SoftEther.VPNClientRpc
{
    public class VpnClientRpc
    {
        JsonRpcClient rpc_client;

        public VpnClientRpc(string vpnclient_host, int vpnclient_port, string admin_password, string hub_name = null)
        {
            rpc_client = new JsonRpcClient($"http://{vpnclient_host}:{vpnclient_port}/api/", null);

            rpc_client.HttpHeaders.Add("X-VPNADMIN-PASSWORD", admin_password);
        }

        public async Task<T> CallAsync<T>(string method_name, T request)
        {
            T response = await rpc_client.CallAsync<T>(method_name, request);

            return response;
        }

        /// <summary>
        /// Get the client version (Async mode)
        /// </summary>
        public async Task<VpnRpcClientVersion> GetClientVersionAsync(VpnRpcClientVersion input_param) => await CallAsync<VpnRpcClientVersion>("GetClientVersion", input_param);

        /// <summary>
        /// Get the client version (Sync mode)
        /// </summary>
        public VpnRpcClientVersion GetClientVersion(VpnRpcClientVersion input_param) => GetClientVersionAsync(input_param).Result;

        /// <summary>
        /// Get the CM_SETTING (Async mode)
        /// </summary>
        public async Task<VpnCmSetting> GetCmSettingAsync(VpnCmSetting input_param) => await CallAsync<VpnCmSetting>("GetCmSetting", input_param);

        /// <summary>
        /// Get the CM_SETTING (Sync mode)
        /// </summary>
        public VpnCmSetting GetCmSetting(VpnCmSetting input_param) => GetCmSettingAsync(input_param).Result;

        /// <summary>
        /// Set the CM_SETTING (Async mode)
        /// </summary>
        public async Task<VpnCmSetting> SetCmSettingAsync(VpnCmSetting input_param) => await CallAsync<VpnCmSetting>("SetCmSetting", input_param);

        /// <summary>
        /// Set the CM_SETTING (Sync mode)
        /// </summary>
        public VpnCmSetting SetCmSetting(VpnCmSetting input_param) => SetCmSettingAsync(input_param).Result;

        /// <summary>
        /// Set the password (Async mode)
        /// </summary>
        public async Task<VpnRpcClientPassword> SetPasswordAsync(VpnRpcClientPassword input_param) => await CallAsync<VpnRpcClientPassword>("SetPassword", input_param);

        /// <summary>
        /// Set the password (Sync mode)
        /// </summary>
        public VpnRpcClientPassword SetPassword(VpnRpcClientPassword input_param) => SetPasswordAsync(input_param).Result;

        /// <summary>
        /// Get the password setting (Async mode)
        /// </summary>
        public async Task<VpnRpcClientPasswordSetting> GetPasswordSettingAsync(VpnRpcClientPasswordSetting input_param) => await CallAsync<VpnRpcClientPasswordSetting>("GetPasswordSetting", input_param);

        /// <summary>
        /// Get the password setting (Sync mode)
        /// </summary>
        public VpnRpcClientPasswordSetting GetPasswordSetting(VpnRpcClientPasswordSetting input_param) => GetPasswordSettingAsync(input_param).Result;

        /// <summary>
        /// Enumerate the trusted CA (Async mode)
        /// </summary>
        public async Task<VpnRpcClientEnumCA> EnumCaAsync(VpnRpcClientEnumCA input_param) => await CallAsync<VpnRpcClientEnumCA>("EnumCa", input_param);

        /// <summary>
        /// Enumerate the trusted CA (Sync mode)
        /// </summary>
        public VpnRpcClientEnumCA EnumCa(VpnRpcClientEnumCA input_param) => EnumCaAsync(input_param).Result;

        /// <summary>
        /// Add a CA certificate (Async mode)
        /// </summary>
        public async Task<VpnRpcCert> AddCaAsync(VpnRpcCert input_param) => await CallAsync<VpnRpcCert>("AddCa", input_param);

        /// <summary>
        /// Add a CA certificate (Sync mode)
        /// </summary>
        public VpnRpcCert AddCa(VpnRpcCert input_param) => AddCaAsync(input_param).Result;

        /// <summary>
        /// Delete the CA certificate (Async mode)
        /// </summary>
        public async Task<VpnRpcClientDeleteCA> DeleteCaAsync(VpnRpcClientDeleteCA input_param) => await CallAsync<VpnRpcClientDeleteCA>("DeleteCa", input_param);

        /// <summary>
        /// Delete the CA certificate (Sync mode)
        /// </summary>
        public VpnRpcClientDeleteCA DeleteCa(VpnRpcClientDeleteCA input_param) => DeleteCaAsync(input_param).Result;

        /// <summary>
        /// Get the CA certificate (Async mode)
        /// </summary>
        public async Task<VpnRpcGetCA> GetCaAsync(VpnRpcGetCA input_param) => await CallAsync<VpnRpcGetCA>("GetCa", input_param);

        /// <summary>
        /// Get the CA certificate (Sync mode)
        /// </summary>
        public VpnRpcGetCA GetCa(VpnRpcGetCA input_param) => GetCaAsync(input_param).Result;

        /// <summary>
        /// Enumeration of secure devices (Async mode)
        /// </summary>
        public async Task<VpnRpcClientEnumSecure> EnumSecureAsync(VpnRpcClientEnumSecure input_param) => await CallAsync<VpnRpcClientEnumSecure>("EnumSecure", input_param);

        /// <summary>
        /// Enumeration of secure devices (Sync mode)
        /// </summary>
        public VpnRpcClientEnumSecure EnumSecure(VpnRpcClientEnumSecure input_param) => EnumSecureAsync(input_param).Result;

        /// <summary>
        /// Specifying a secure device to be used (Async mode)
        /// </summary>
        public async Task<VpnRpcUseSecure> UseSecureAsync(VpnRpcUseSecure input_param) => await CallAsync<VpnRpcUseSecure>("UseSecure", input_param);

        /// <summary>
        /// Specifying a secure device to be used (Sync mode)
        /// </summary>
        public VpnRpcUseSecure UseSecure(VpnRpcUseSecure input_param) => UseSecureAsync(input_param).Result;

        /// <summary>
        /// Get the secure device to be used (Async mode)
        /// </summary>
        public async Task<VpnRpcUseSecure> GetUseSecureAsync(VpnRpcUseSecure input_param) => await CallAsync<VpnRpcUseSecure>("GetUseSecure", input_param);

        /// <summary>
        /// Get the secure device to be used (Sync mode)
        /// </summary>
        public VpnRpcUseSecure GetUseSecure(VpnRpcUseSecure input_param) => GetUseSecureAsync(input_param).Result;

        /// <summary>
        /// Enumerate objects in the secure device (Async mode)
        /// </summary>
        public async Task<VpnRpcEnumObjectInSecure> EnumObjectInSecureAsync(VpnRpcEnumObjectInSecure input_param) => await CallAsync<VpnRpcEnumObjectInSecure>("EnumObjectInSecure", input_param);

        /// <summary>
        /// Enumerate objects in the secure device (Sync mode)
        /// </summary>
        public VpnRpcEnumObjectInSecure EnumObjectInSecure(VpnRpcEnumObjectInSecure input_param) => EnumObjectInSecureAsync(input_param).Result;

        /// <summary>
        /// Create a virtual LAN card (Async mode)
        /// </summary>
        public async Task<VpnRpcClientCreateVlan> CreateVLanAsync(VpnRpcClientCreateVlan input_param) => await CallAsync<VpnRpcClientCreateVlan>("CreateVLan", input_param);

        /// <summary>
        /// Create a virtual LAN card (Sync mode)
        /// </summary>
        public VpnRpcClientCreateVlan CreateVLan(VpnRpcClientCreateVlan input_param) => CreateVLanAsync(input_param).Result;

        /// <summary>
        /// Upgrade the virtual LAN card (Async mode)
        /// </summary>
        public async Task<VpnRpcClientCreateVlan> UpgradeVLanAsync(VpnRpcClientCreateVlan input_param) => await CallAsync<VpnRpcClientCreateVlan>("UpgradeVLan", input_param);

        /// <summary>
        /// Upgrade the virtual LAN card (Sync mode)
        /// </summary>
        public VpnRpcClientCreateVlan UpgradeVLan(VpnRpcClientCreateVlan input_param) => UpgradeVLanAsync(input_param).Result;

        /// <summary>
        /// Get the information about the virtual LAN card (Async mode)
        /// </summary>
        public async Task<VpnRpcClientGetVlan> GetVLanAsync(VpnRpcClientGetVlan input_param) => await CallAsync<VpnRpcClientGetVlan>("GetVLan", input_param);

        /// <summary>
        /// Get the information about the virtual LAN card (Sync mode)
        /// </summary>
        public VpnRpcClientGetVlan GetVLan(VpnRpcClientGetVlan input_param) => GetVLanAsync(input_param).Result;

        /// <summary>
        /// Set the information about the virtual LAN card (Async mode)
        /// </summary>
        public async Task<VpnRpcClientSetVlan> SetVLanAsync(VpnRpcClientSetVlan input_param) => await CallAsync<VpnRpcClientSetVlan>("SetVLan", input_param);

        /// <summary>
        /// Set the information about the virtual LAN card (Sync mode)
        /// </summary>
        public VpnRpcClientSetVlan SetVLan(VpnRpcClientSetVlan input_param) => SetVLanAsync(input_param).Result;

        /// <summary>
        /// Enumerate virtual LAN cards (Async mode)
        /// </summary>
        public async Task<VpnRpcClientEnumVlan> EnumVLanAsync(VpnRpcClientEnumVlan input_param) => await CallAsync<VpnRpcClientEnumVlan>("EnumVLan", input_param);

        /// <summary>
        /// Enumerate virtual LAN cards (Sync mode)
        /// </summary>
        public VpnRpcClientEnumVlan EnumVLan(VpnRpcClientEnumVlan input_param) => EnumVLanAsync(input_param).Result;

        /// <summary>
        /// Delete the virtual LAN card (Async mode)
        /// </summary>
        public async Task<VpnRpcClientCreateVlan> DeleteVLanAsync(VpnRpcClientCreateVlan input_param) => await CallAsync<VpnRpcClientCreateVlan>("DeleteVLan", input_param);

        /// <summary>
        /// Delete the virtual LAN card (Sync mode)
        /// </summary>
        public VpnRpcClientCreateVlan DeleteVLan(VpnRpcClientCreateVlan input_param) => DeleteVLanAsync(input_param).Result;

        /// <summary>
        /// Start the virtual LAN card (Async mode)
        /// </summary>
        public async Task<VpnRpcClientCreateVlan> EnableVLanAsync(VpnRpcClientCreateVlan input_param) => await CallAsync<VpnRpcClientCreateVlan>("EnableVLan", input_param);

        /// <summary>
        /// Start the virtual LAN card (Sync mode)
        /// </summary>
        public VpnRpcClientCreateVlan EnableVLan(VpnRpcClientCreateVlan input_param) => EnableVLanAsync(input_param).Result;

        /// <summary>
        /// Stop the virtual LAN card (Async mode)
        /// </summary>
        public async Task<VpnRpcClientCreateVlan> DisableVLanAsync(VpnRpcClientCreateVlan input_param) => await CallAsync<VpnRpcClientCreateVlan>("DisableVLan", input_param);

        /// <summary>
        /// Stop the virtual LAN card (Sync mode)
        /// </summary>
        public VpnRpcClientCreateVlan DisableVLan(VpnRpcClientCreateVlan input_param) => DisableVLanAsync(input_param).Result;

        /// <summary>
        /// Create an account (Async mode)
        /// </summary>
        public async Task<VpnRpcClientCreateAccount> CreateAccountAsync(VpnRpcClientCreateAccount input_param) => await CallAsync<VpnRpcClientCreateAccount>("CreateAccount", input_param);

        /// <summary>
        /// Create an account (Sync mode)
        /// </summary>
        public VpnRpcClientCreateAccount CreateAccount(VpnRpcClientCreateAccount input_param) => CreateAccountAsync(input_param).Result;

        /// <summary>
        /// Enumeration of accounts (Async mode)
        /// </summary>
        public async Task<VpnRpcClientEnumAccount> EnumAccountAsync(VpnRpcClientEnumAccount input_param) => await CallAsync<VpnRpcClientEnumAccount>("EnumAccount", input_param);

        /// <summary>
        /// Enumeration of accounts (Sync mode)
        /// </summary>
        public VpnRpcClientEnumAccount EnumAccount(VpnRpcClientEnumAccount input_param) => EnumAccountAsync(input_param).Result;

        /// <summary>
        /// Delete the account (Async mode)
        /// </summary>
        public async Task<VpnRpcClientDeleteAccount> DeleteAccountAsync(VpnRpcClientDeleteAccount input_param) => await CallAsync<VpnRpcClientDeleteAccount>("DeleteAccount", input_param);

        /// <summary>
        /// Delete the account (Sync mode)
        /// </summary>
        public VpnRpcClientDeleteAccount DeleteAccount(VpnRpcClientDeleteAccount input_param) => DeleteAccountAsync(input_param).Result;

        /// <summary>
        /// Set the account as a start-up account (Async mode)
        /// </summary>
        public async Task<VpnRpcClientDeleteAccount> SetStartupAccountAsync(VpnRpcClientDeleteAccount input_param) => await CallAsync<VpnRpcClientDeleteAccount>("SetStartupAccount", input_param);

        /// <summary>
        /// Set the account as a start-up account (Sync mode)
        /// </summary>
        public VpnRpcClientDeleteAccount SetStartupAccount(VpnRpcClientDeleteAccount input_param) => SetStartupAccountAsync(input_param).Result;

        /// <summary>
        /// Unset the startup attribute of the account (Async mode)
        /// </summary>
        public async Task<VpnRpcClientDeleteAccount> RemoveStartupAccountAsync(VpnRpcClientDeleteAccount input_param) => await CallAsync<VpnRpcClientDeleteAccount>("RemoveStartupAccount", input_param);

        /// <summary>
        /// Unset the startup attribute of the account (Sync mode)
        /// </summary>
        public VpnRpcClientDeleteAccount RemoveStartupAccount(VpnRpcClientDeleteAccount input_param) => RemoveStartupAccountAsync(input_param).Result;

        /// <summary>
        /// Get the issuer (Async mode)
        /// </summary>
        public async Task<VpnRpcGetIssuer> GetIssuerAsync(VpnRpcGetIssuer input_param) => await CallAsync<VpnRpcGetIssuer>("GetIssuer", input_param);

        /// <summary>
        /// Get the issuer (Sync mode)
        /// </summary>
        public VpnRpcGetIssuer GetIssuer(VpnRpcGetIssuer input_param) => GetIssuerAsync(input_param).Result;

        /// <summary>
        /// Get VPN Gate list (Async mode)
        /// </summary>
        public async Task<VpnVghostlist> VgcGetListAsync(VpnVghostlist input_param) => await CallAsync<VpnVghostlist>("VgcGetList", input_param);

        /// <summary>
        /// Get VPN Gate list (Sync mode)
        /// </summary>
        public VpnVghostlist VgcGetList(VpnVghostlist input_param) => VgcGetListAsync(input_param).Result;

        /// <summary>
        /// Refresh VPN Gate list (Async mode)
        /// </summary>
        public async Task<VpnRpcTest> VgcRefreshListAsync(VpnRpcTest input_param) => await CallAsync<VpnRpcTest>("VgcRefreshList", input_param);

        /// <summary>
        /// Refresh VPN Gate list (Sync mode)
        /// </summary>
        public VpnRpcTest VgcRefreshList(VpnRpcTest input_param) => VgcRefreshListAsync(input_param).Result;

        /// <summary>
        /// Get status of VPN Gate list (Async mode)
        /// </summary>
        public async Task<VpnRpcTest> VgcGetListStatusAsync(VpnRpcTest input_param) => await CallAsync<VpnRpcTest>("VgcGetListStatus", input_param);

        /// <summary>
        /// Get status of VPN Gate list (Sync mode)
        /// </summary>
        public VpnRpcTest VgcGetListStatus(VpnRpcTest input_param) => VgcGetListStatusAsync(input_param).Result;

        /// <summary>
        /// Get configuration of VPN Gate (Async mode)
        /// </summary>
        public async Task<VpnRpcTest> VgcGetConfigAsync(VpnRpcTest input_param) => await CallAsync<VpnRpcTest>("VgcGetConfig", input_param);

        /// <summary>
        /// Get configuration of VPN Gate (Sync mode)
        /// </summary>
        public VpnRpcTest VgcGetConfig(VpnRpcTest input_param) => VgcGetConfigAsync(input_param).Result;

        /// <summary>
        /// Set the configuration of the VPN Gate (Async mode)
        /// </summary>
        public async Task<VpnRpcTest> VgcSetConfigAsync(VpnRpcTest input_param) => await CallAsync<VpnRpcTest>("VgcSetConfig", input_param);

        /// <summary>
        /// Set the configuration of the VPN Gate (Sync mode)
        /// </summary>
        public VpnRpcTest VgcSetConfig(VpnRpcTest input_param) => VgcSetConfigAsync(input_param).Result;

        /// <summary>
        /// Start connection to VPN Gate (Async mode)
        /// </summary>
        public async Task<VpnRpcTest> VgcConnectAsync(VpnRpcTest input_param) => await CallAsync<VpnRpcTest>("VgcConnect", input_param);

        /// <summary>
        /// Start connection to VPN Gate (Sync mode)
        /// </summary>
        public VpnRpcTest VgcConnect(VpnRpcTest input_param) => VgcConnectAsync(input_param).Result;

        /// <summary>
        /// Get the common proxy settings (Async mode)
        /// </summary>
        public async Task<VpnInternetSetting> GetCommonProxySettingAsync(VpnInternetSetting input_param) => await CallAsync<VpnInternetSetting>("GetCommonProxySetting", input_param);

        /// <summary>
        /// Get the common proxy settings (Sync mode)
        /// </summary>
        public VpnInternetSetting GetCommonProxySetting(VpnInternetSetting input_param) => GetCommonProxySettingAsync(input_param).Result;

        /// <summary>
        /// Set the common proxy settings (Async mode)
        /// </summary>
        public async Task<VpnInternetSetting> SetCommonProxySettingAsync(VpnInternetSetting input_param) => await CallAsync<VpnInternetSetting>("SetCommonProxySetting", input_param);

        /// <summary>
        /// Set the common proxy settings (Sync mode)
        /// </summary>
        public VpnInternetSetting SetCommonProxySetting(VpnInternetSetting input_param) => SetCommonProxySettingAsync(input_param).Result;

        /// <summary>
        /// Configure the account (Async mode)
        /// </summary>
        public async Task<VpnRpcClientCreateAccount> SetAccountAsync(VpnRpcClientCreateAccount input_param) => await CallAsync<VpnRpcClientCreateAccount>("SetAccount", input_param);

        /// <summary>
        /// Configure the account (Sync mode)
        /// </summary>
        public VpnRpcClientCreateAccount SetAccount(VpnRpcClientCreateAccount input_param) => SetAccountAsync(input_param).Result;

        /// <summary>
        /// Get the account information (Async mode)
        /// </summary>
        public async Task<VpnRpcClientGetAccount> GetAccountAsync(VpnRpcClientGetAccount input_param) => await CallAsync<VpnRpcClientGetAccount>("GetAccount", input_param);

        /// <summary>
        /// Get the account information (Sync mode)
        /// </summary>
        public VpnRpcClientGetAccount GetAccount(VpnRpcClientGetAccount input_param) => GetAccountAsync(input_param).Result;

        /// <summary>
        /// Change the account name (Async mode)
        /// </summary>
        public async Task<VpnRpcRenameAccount> RenameAccountAsync(VpnRpcRenameAccount input_param) => await CallAsync<VpnRpcRenameAccount>("RenameAccount", input_param);

        /// <summary>
        /// Change the account name (Sync mode)
        /// </summary>
        public VpnRpcRenameAccount RenameAccount(VpnRpcRenameAccount input_param) => RenameAccountAsync(input_param).Result;

        /// <summary>
        /// Set the client configuration (Async mode)
        /// </summary>
        public async Task<VpnClientConfig> SetClientConfigAsync(VpnClientConfig input_param) => await CallAsync<VpnClientConfig>("SetClientConfig", input_param);

        /// <summary>
        /// Set the client configuration (Sync mode)
        /// </summary>
        public VpnClientConfig SetClientConfig(VpnClientConfig input_param) => SetClientConfigAsync(input_param).Result;

        /// <summary>
        /// Get the network client configuration (Async mode)
        /// </summary>
        public async Task<VpnClientConfig> GetClientConfigAsync(VpnClientConfig input_param) => await CallAsync<VpnClientConfig>("GetClientConfig", input_param);

        /// <summary>
        /// Get the network client configuration (Sync mode)
        /// </summary>
        public VpnClientConfig GetClientConfig(VpnClientConfig input_param) => GetClientConfigAsync(input_param).Result;

        /// <summary>
        /// Connect (Async mode)
        /// </summary>
        public async Task<VpnRpcClientConnect> ConnectAsync(VpnRpcClientConnect input_param) => await CallAsync<VpnRpcClientConnect>("Connect", input_param);

        /// <summary>
        /// Connect (Sync mode)
        /// </summary>
        public VpnRpcClientConnect Connect(VpnRpcClientConnect input_param) => ConnectAsync(input_param).Result;

        /// <summary>
        /// Disconnect (Async mode)
        /// </summary>
        public async Task<VpnRpcClientConnect> DisconnectAsync(VpnRpcClientConnect input_param) => await CallAsync<VpnRpcClientConnect>("Disconnect", input_param);

        /// <summary>
        /// Disconnect (Sync mode)
        /// </summary>
        public VpnRpcClientConnect Disconnect(VpnRpcClientConnect input_param) => DisconnectAsync(input_param).Result;

        /// <summary>
        /// Get the connection status (Async mode)
        /// </summary>
        public async Task<VpnRpcClientGetConnectionStatus> GetAccountStatusAsync(VpnRpcClientGetConnectionStatus input_param) => await CallAsync<VpnRpcClientGetConnectionStatus>("GetAccountStatus", input_param);

        /// <summary>
        /// Get the connection status (Sync mode)
        /// </summary>
        public VpnRpcClientGetConnectionStatus GetAccountStatus(VpnRpcClientGetConnectionStatus input_param) => GetAccountStatusAsync(input_param).Result;




    }
}
