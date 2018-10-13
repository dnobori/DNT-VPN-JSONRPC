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

namespace SoftEther.VPNServerRpc
{
    public class VpnServerRpc
    {
        JsonRpcClient rpc_client;

        public VpnServerRpc(string vpnserver_host, int vpnserver_port, string admin_password, string hub_name = null)
        {
            rpc_client = new JsonRpcClient($"https://{vpnserver_host}:{vpnserver_port}/api/", null);

            /*
            LABEL_A:
            A a = new A();

            try
            {
                RpcServerInfo ret = c.Call<RpcServerInfo>("GetServerInfo", a).Result;
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

            }
            Console.ReadLine();

            goto LABEL_A;
            */
        }

        public async Task<T> Call<T>(string method_name, T request)
        {
            T response = await rpc_client.Call<T>(method_name, request);

            return response;
        }

        public async Task<string> CallInternal<T>(string method_name, T request)
        {
            string response = await rpc_client.CallInternal<T>(method_name, request);

            return response;
        }

        /// <summary>
        /// test RPC function (Async mode)
        /// </summary>
        public async Task<VpnRpcTest> TestAsync(VpnRpcTest t) => await Call<VpnRpcTest>("Test", t);

        /// <summary>
        /// test RPC function (Sync mode)
        /// </summary>
        public VpnRpcTest Test(VpnRpcTest t) => TestAsync(t).Result;

        /// <summary>
        /// Get the current VPN Server information (Async mode)
        /// </summary>
        public async Task<VpnRpcServerInfo> GetServerInfoAsync() => await Call<VpnRpcServerInfo>("GetServerInfo", new VpnRpcServerInfo());

        /// <summary>
        /// Get the current VPN Server information (Sync mode)
        /// </summary>
        public VpnRpcServerInfo GetServerInfo() => GetServerInfoAsync().Result;

        /// <summary>
        /// Get server status (Async mode)
        /// </summary>
        public async Task<VpnRpcServerStatus> GetServerStatusAsync() => await Call<VpnRpcServerStatus>("GetServerStatus", new VpnRpcServerStatus());

        /// <summary>
        /// Get server status (Sync mode)
        /// </summary>
        public VpnRpcServerStatus GetServerStatus() => GetServerStatusAsync().Result;

        /// <summary>
        /// Create a listener (Async mode)
        /// </summary>
        public async Task<VpnRpcListener> CreateListenerAsync(VpnRpcListener t) => await Call<VpnRpcListener>("CreateListener", t);

        /// <summary>
        /// Create a listener (Sync mode)
        /// </summary>
        public VpnRpcListener CreateListener(VpnRpcListener t) => CreateListenerAsync(t).Result;

        /// <summary>
        /// Enumerating listeners (Async mode)
        /// </summary>
        public async Task<VpnRpcListenerList> EnumListenerAsync() => await Call<VpnRpcListenerList>("EnumListener", new VpnRpcListenerList());

        /// <summary>
        /// Enumerating listeners (Sync mode)
        /// </summary>
        public VpnRpcListenerList EnumListener() => EnumListenerAsync().Result;

        /// <summary>
        /// Delete a listener (Async mode)
        /// </summary>
        public async Task<VpnRpcListener> DeleteListenerAsync(VpnRpcListener t) => await Call<VpnRpcListener>("DeleteListener", t);

        /// <summary>
        /// Delete a listener (Sync mode)
        /// </summary>
        public VpnRpcListener DeleteListener(VpnRpcListener t) => DeleteListenerAsync(t).Result;

        /// <summary>
        /// Enable / Disable listener (Async mode)
        /// </summary>
        public async Task<VpnRpcListener> EnableListenerAsync(VpnRpcListener t) => await Call<VpnRpcListener>("EnableListener", t);

        /// <summary>
        /// Enable / Disable listener (Sync mode)
        /// </summary>
        public VpnRpcListener EnableListener(VpnRpcListener t) => EnableListenerAsync(t).Result;

        /// <summary>
        /// Set server password (Async mode)
        /// </summary>
        public async Task<VpnRpcSetPassword> SetServerPasswordAsync(VpnRpcSetPassword t) => await Call<VpnRpcSetPassword>("SetServerPassword", t);

        /// <summary>
        /// Set server password (Sync mode)
        /// </summary>
        public VpnRpcSetPassword SetServerPassword(VpnRpcSetPassword t) => SetServerPasswordAsync(t).Result;

        /// <summary>
        /// Set clustering configuration (Async mode)
        /// </summary>
        public async Task<VpnRpcFarm> SetFarmSettingAsync(VpnRpcFarm t) => await Call<VpnRpcFarm>("SetFarmSetting", t);

        /// <summary>
        /// Set clustering configuration (Sync mode)
        /// </summary>
        public VpnRpcFarm SetFarmSetting(VpnRpcFarm t) => SetFarmSettingAsync(t).Result;

        /// <summary>
        /// Get clustering configuration (Async mode)
        /// </summary>
        public async Task<VpnRpcFarm> GetFarmSettingAsync() => await Call<VpnRpcFarm>("GetFarmSetting", new VpnRpcFarm());

        /// <summary>
        /// Get clustering configuration (Sync mode)
        /// </summary>
        public VpnRpcFarm GetFarmSetting() => GetFarmSettingAsync().Result;

        /// <summary>
        /// Get cluster member information (Async mode)
        /// </summary>
        public async Task<VpnRpcFarmInfo> GetFarmInfoAsync(VpnRpcFarmInfo t) => await Call<VpnRpcFarmInfo>("GetFarmInfo", t);

        /// <summary>
        /// Get cluster member information (Sync mode)
        /// </summary>
        public VpnRpcFarmInfo GetFarmInfo(VpnRpcFarmInfo t) => GetFarmInfoAsync(t).Result;

        /// <summary>
        /// Enumerate cluster members (Async mode)
        /// </summary>
        public async Task<VpnRpcEnumFarm> EnumFarmMemberAsync() => await Call<VpnRpcEnumFarm>("EnumFarmMember", new VpnRpcEnumFarm());

        /// <summary>
        /// Enumerate cluster members (Sync mode)
        /// </summary>
        public VpnRpcEnumFarm EnumFarmMember() => EnumFarmMemberAsync().Result;

        /// <summary>
        /// Get status of connection to cluster controller (Async mode)
        /// </summary>
        public async Task<VpnRpcFarmConnectionStatus> GetFarmConnectionStatusAsync() => await Call<VpnRpcFarmConnectionStatus>("GetFarmConnectionStatus", new VpnRpcFarmConnectionStatus());

        /// <summary>
        /// Get status of connection to cluster controller (Sync mode)
        /// </summary>
        public VpnRpcFarmConnectionStatus GetFarmConnectionStatus() => GetFarmConnectionStatusAsync().Result;

        /// <summary>
        /// Set the server certification (Async mode)
        /// </summary>
        public async Task<VpnRpcKeyPair> SetServerCertAsync(VpnRpcKeyPair t) => await Call<VpnRpcKeyPair>("SetServerCert", t);

        /// <summary>
        /// Set the server certification (Sync mode)
        /// </summary>
        public VpnRpcKeyPair SetServerCert(VpnRpcKeyPair t) => SetServerCertAsync(t).Result;

        /// <summary>
        /// Get the server certification (Async mode)
        /// </summary>
        public async Task<VpnRpcKeyPair> GetServerCertAsync() => await Call<VpnRpcKeyPair>("GetServerCert", new VpnRpcKeyPair());

        /// <summary>
        /// Get the server certification (Sync mode)
        /// </summary>
        public VpnRpcKeyPair GetServerCert() => GetServerCertAsync().Result;

        /// <summary>
        /// Get cipher for SSL (Async mode)
        /// </summary>
        public async Task<VpnRpcStr> GetServerCipherAsync() => await Call<VpnRpcStr>("GetServerCipher", new VpnRpcStr());

        /// <summary>
        /// Get cipher for SSL (Sync mode)
        /// </summary>
        public VpnRpcStr GetServerCipher() => GetServerCipherAsync().Result;

        /// <summary>
        /// Set cipher for SSL to the server (Async mode)
        /// </summary>
        public async Task<VpnRpcStr> SetServerCipherAsync(VpnRpcStr t) => await Call<VpnRpcStr>("SetServerCipher", t);

        /// <summary>
        /// Set cipher for SSL to the server (Sync mode)
        /// </summary>
        public VpnRpcStr SetServerCipher(VpnRpcStr t) => SetServerCipherAsync(t).Result;

        /// <summary>
        /// Create a hub (Async mode)
        /// </summary>
        public async Task<VpnRpcCreateHub> CreateHubAsync(VpnRpcCreateHub input_param) => await Call<VpnRpcCreateHub>("CreateHub", input_param);

        /// <summary>
        /// Create a hub (Sync mode)
        /// </summary>
        public VpnRpcCreateHub CreateHub(VpnRpcCreateHub input_param) => CreateHubAsync(input_param).Result;

        /// <summary>
        /// Set hub configuration (Async mode)
        /// </summary>
        public async Task<VpnRpcCreateHub> SetHubAsync(VpnRpcCreateHub input_param) => await Call<VpnRpcCreateHub>("SetHub", input_param);

        /// <summary>
        /// Set hub configuration (Sync mode)
        /// </summary>
        public VpnRpcCreateHub SetHub(VpnRpcCreateHub input_param) => SetHubAsync(input_param).Result;

        /// <summary>
        /// Get hub configuration (Async mode)
        /// </summary>
        public async Task<VpnRpcCreateHub> GetHubAsync(VpnRpcCreateHub input_param) => await Call<VpnRpcCreateHub>("GetHub", input_param);

        /// <summary>
        /// Get hub configuration (Sync mode)
        /// </summary>
        public VpnRpcCreateHub GetHub(VpnRpcCreateHub input_param) => GetHubAsync(input_param).Result;

        /// <summary>
        /// Enumerate hubs (Async mode)
        /// </summary>
        public async Task<VpnRpcEnumHub> EnumHubAsync() => await Call<VpnRpcEnumHub>("EnumHub", new VpnRpcEnumHub());

        /// <summary>
        /// Enumerate hubs (Sync mode)
        /// </summary>
        public VpnRpcEnumHub EnumHub() => EnumHubAsync().Result;

        /// <summary>
        /// Delete a hub (Async mode)
        /// </summary>
        public async Task<VpnRpcDeleteHub> DeleteHubAsync(VpnRpcDeleteHub input_param) => await Call<VpnRpcDeleteHub>("DeleteHub", input_param);

        /// <summary>
        /// Delete a hub (Sync mode)
        /// </summary>
        public VpnRpcDeleteHub DeleteHub(VpnRpcDeleteHub input_param) => DeleteHubAsync(input_param).Result;

        /// <summary>
        /// Get Radius options of the hub (Async mode)
        /// </summary>
        public async Task<VpnRpcRadius> GetHubRadiusAsync(VpnRpcRadius input_param) => await Call<VpnRpcRadius>("GetHubRadius", input_param);

        /// <summary>
        /// Get Radius options of the hub (Sync mode)
        /// </summary>
        public VpnRpcRadius GetHubRadius(VpnRpcRadius input_param) => GetHubRadiusAsync(input_param).Result;

        /// <summary>
        /// Set Radius options of the hub (Async mode)
        /// </summary>
        public async Task<VpnRpcRadius> SetHubRadiusAsync(VpnRpcRadius input_param) => await Call<VpnRpcRadius>("SetHubRadius", input_param);

        /// <summary>
        /// Set Radius options of the hub (Sync mode)
        /// </summary>
        public VpnRpcRadius SetHubRadius(VpnRpcRadius input_param) => SetHubRadiusAsync(input_param).Result;

        /// <summary>
        /// Enumerate connections (Async mode)
        /// </summary>
        public async Task<VpnRpcEnumConnection> EnumConnectionAsync() => await Call<VpnRpcEnumConnection>("EnumConnection", new VpnRpcEnumConnection());

        /// <summary>
        /// Enumerate connections (Sync mode)
        /// </summary>
        public VpnRpcEnumConnection EnumConnection() => EnumConnectionAsync().Result;

        /// <summary>
        /// Disconnect a connection (Async mode)
        /// </summary>
        public async Task<VpnRpcDisconnectConnection> DisconnectConnectionAsync(VpnRpcDisconnectConnection input_param) => await Call<VpnRpcDisconnectConnection>("DisconnectConnection", input_param);

        /// <summary>
        /// Disconnect a connection (Sync mode)
        /// </summary>
        public VpnRpcDisconnectConnection DisconnectConnection(VpnRpcDisconnectConnection input_param) => DisconnectConnectionAsync(input_param).Result;

        /// <summary>
        /// Get connection information (Async mode)
        /// </summary>
        public async Task<VpnRpcConnectionInfo> GetConnectionInfoAsync(VpnRpcConnectionInfo input_param) => await Call<VpnRpcConnectionInfo>("GetConnectionInfo", input_param);

        /// <summary>
        /// Get connection information (Sync mode)
        /// </summary>
        public VpnRpcConnectionInfo GetConnectionInfo(VpnRpcConnectionInfo input_param) => GetConnectionInfoAsync(input_param).Result;

        /// <summary>
        /// Make a hub on-line or off-line (Async mode)
        /// </summary>
        public async Task<VpnRpcSetHubOnline> SetHubOnlineAsync(VpnRpcSetHubOnline input_param) => await Call<VpnRpcSetHubOnline>("SetHubOnline", input_param);

        /// <summary>
        /// Make a hub on-line or off-line (Sync mode)
        /// </summary>
        public VpnRpcSetHubOnline SetHubOnline(VpnRpcSetHubOnline input_param) => SetHubOnlineAsync(input_param).Result;

        /// <summary>
        /// Get hub status (Async mode)
        /// </summary>
        public async Task<VpnRpcHubStatus> GetHubStatusAsync(VpnRpcHubStatus input_param) => await Call<VpnRpcHubStatus>("GetHubStatus", input_param);

        /// <summary>
        /// Get hub status (Sync mode)
        /// </summary>
        public VpnRpcHubStatus GetHubStatus(VpnRpcHubStatus input_param) => GetHubStatusAsync(input_param).Result;

        /// <summary>
        /// Set logging configuration into the hub (Async mode)
        /// </summary>
        public async Task<VpnRpcHubLog> SetHubLogAsync(VpnRpcHubLog input_param) => await Call<VpnRpcHubLog>("SetHubLog", input_param);

        /// <summary>
        /// Set logging configuration into the hub (Sync mode)
        /// </summary>
        public VpnRpcHubLog SetHubLog(VpnRpcHubLog input_param) => SetHubLogAsync(input_param).Result;

        /// <summary>
        /// Get logging configuration of the hub (Async mode)
        /// </summary>
        public async Task<VpnRpcHubLog> GetHubLogAsync(VpnRpcHubLog input_param) => await Call<VpnRpcHubLog>("GetHubLog", input_param);

        /// <summary>
        /// Get logging configuration of the hub (Sync mode)
        /// </summary>
        public VpnRpcHubLog GetHubLog(VpnRpcHubLog input_param) => GetHubLogAsync(input_param).Result;

        /// <summary>
        /// Add CA(Certificate Authority) into the hub (Async mode)
        /// </summary>
        public async Task<VpnRpcHubAddCA> AddCaAsync(VpnRpcHubAddCA input_param) => await Call<VpnRpcHubAddCA>("AddCa", input_param);

        /// <summary>
        /// Add CA(Certificate Authority) into the hub (Sync mode)
        /// </summary>
        public VpnRpcHubAddCA AddCa(VpnRpcHubAddCA input_param) => AddCaAsync(input_param).Result;

        /// <summary>
        /// Enumerate CA(Certificate Authority) in the hub (Async mode)
        /// </summary>
        public async Task<VpnRpcHubEnumCA> EnumCaAsync(VpnRpcHubEnumCA input_param) => await Call<VpnRpcHubEnumCA>("EnumCa", input_param);

        /// <summary>
        /// Enumerate CA(Certificate Authority) in the hub (Sync mode)
        /// </summary>
        public VpnRpcHubEnumCA EnumCa(VpnRpcHubEnumCA input_param) => EnumCaAsync(input_param).Result;

        /// <summary>
        /// Get CA(Certificate Authority) setting from the hub (Async mode)
        /// </summary>
        public async Task<VpnRpcHubGetCA> GetCaAsync(VpnRpcHubGetCA input_param) => await Call<VpnRpcHubGetCA>("GetCa", input_param);

        /// <summary>
        /// Get CA(Certificate Authority) setting from the hub (Sync mode)
        /// </summary>
        public VpnRpcHubGetCA GetCa(VpnRpcHubGetCA input_param) => GetCaAsync(input_param).Result;

        /// <summary>
        /// Delete a CA(Certificate Authority) setting from the hub (Async mode)
        /// </summary>
        public async Task<VpnRpcHubDeleteCA> DeleteCaAsync(VpnRpcHubDeleteCA input_param) => await Call<VpnRpcHubDeleteCA>("DeleteCa", input_param);

        /// <summary>
        /// Delete a CA(Certificate Authority) setting from the hub (Sync mode)
        /// </summary>
        public VpnRpcHubDeleteCA DeleteCa(VpnRpcHubDeleteCA input_param) => DeleteCaAsync(input_param).Result;

        /// <summary>
        /// Make a link into on-line (Async mode)
        /// </summary>
        public async Task<VpnRpcLink> SetLinkOnlineAsync(VpnRpcLink input_param) => await Call<VpnRpcLink>("SetLinkOnline", input_param);

        /// <summary>
        /// Make a link into on-line (Sync mode)
        /// </summary>
        public VpnRpcLink SetLinkOnline(VpnRpcLink input_param) => SetLinkOnlineAsync(input_param).Result;

        /// <summary>
        /// Make a link into off-line (Async mode)
        /// </summary>
        public async Task<VpnRpcLink> SetLinkOfflineAsync(VpnRpcLink input_param) => await Call<VpnRpcLink>("SetLinkOffline", input_param);

        /// <summary>
        /// Make a link into off-line (Sync mode)
        /// </summary>
        public VpnRpcLink SetLinkOffline(VpnRpcLink input_param) => SetLinkOfflineAsync(input_param).Result;

        /// <summary>
        /// Delete a link (Async mode)
        /// </summary>
        public async Task<VpnRpcLink> DeleteLinkAsync(VpnRpcLink input_param) => await Call<VpnRpcLink>("DeleteLink", input_param);

        /// <summary>
        /// Delete a link (Sync mode)
        /// </summary>
        public VpnRpcLink DeleteLink(VpnRpcLink input_param) => DeleteLinkAsync(input_param).Result;

        /// <summary>
        /// Rename link (cascade connection) (Async mode)
        /// </summary>
        public async Task<VpnRpcRenameLink> RenameLinkAsync(VpnRpcRenameLink input_param) => await Call<VpnRpcRenameLink>("RenameLink", input_param);

        /// <summary>
        /// Rename link (cascade connection) (Sync mode)
        /// </summary>
        public VpnRpcRenameLink RenameLink(VpnRpcRenameLink input_param) => RenameLinkAsync(input_param).Result;

        /// <summary>
        /// Create a new link(cascade) (Async mode)
        /// </summary>
        public async Task<VpnRpcCreateLink> CreateLinkAsync(VpnRpcCreateLink input_param) => await Call<VpnRpcCreateLink>("CreateLink", input_param);

        /// <summary>
        /// Create a new link(cascade) (Sync mode)
        /// </summary>
        public VpnRpcCreateLink CreateLink(VpnRpcCreateLink input_param) => CreateLinkAsync(input_param).Result;

        /// <summary>
        /// Get link configuration (Async mode)
        /// </summary>
        public async Task<VpnRpcCreateLink> GetLinkAsync(VpnRpcCreateLink input_param) => await Call<VpnRpcCreateLink>("GetLink", input_param);

        /// <summary>
        /// Get link configuration (Sync mode)
        /// </summary>
        public VpnRpcCreateLink GetLink(VpnRpcCreateLink input_param) => GetLinkAsync(input_param).Result;

        /// <summary>
        /// Set link configuration (Async mode)
        /// </summary>
        public async Task<VpnRpcCreateLink> SetLinkAsync(VpnRpcCreateLink input_param) => await Call<VpnRpcCreateLink>("SetLink", input_param);

        /// <summary>
        /// Set link configuration (Sync mode)
        /// </summary>
        public VpnRpcCreateLink SetLink(VpnRpcCreateLink input_param) => SetLinkAsync(input_param).Result;

        /// <summary>
        /// Enumerate links (Async mode)
        /// </summary>
        public async Task<VpnRpcEnumLink> EnumLinkAsync(VpnRpcEnumLink input_param) => await Call<VpnRpcEnumLink>("EnumLink", input_param);

        /// <summary>
        /// Enumerate links (Sync mode)
        /// </summary>
        public VpnRpcEnumLink EnumLink(VpnRpcEnumLink input_param) => EnumLinkAsync(input_param).Result;

        /// <summary>
        /// Get link status (Async mode)
        /// </summary>
        public async Task<VpnRpcLinkStatus> GetLinkStatusAsync(VpnRpcLinkStatus input_param) => await Call<VpnRpcLinkStatus>("GetLinkStatus", input_param);

        /// <summary>
        /// Get link status (Sync mode)
        /// </summary>
        public VpnRpcLinkStatus GetLinkStatus(VpnRpcLinkStatus input_param) => GetLinkStatusAsync(input_param).Result;

        /// <summary>
        /// Add access list entry (Async mode)
        /// </summary>
        public async Task<VpnRpcAddAccess> AddAccessAsync(VpnRpcAddAccess input_param) => await Call<VpnRpcAddAccess>("AddAccess", input_param);

        /// <summary>
        /// Add access list entry (Sync mode)
        /// </summary>
        public VpnRpcAddAccess AddAccess(VpnRpcAddAccess input_param) => AddAccessAsync(input_param).Result;

        /// <summary>
        /// Delete access list entry (Async mode)
        /// </summary>
        public async Task<VpnRpcDeleteAccess> DeleteAccessAsync(VpnRpcDeleteAccess input_param) => await Call<VpnRpcDeleteAccess>("DeleteAccess", input_param);

        /// <summary>
        /// Delete access list entry (Sync mode)
        /// </summary>
        public VpnRpcDeleteAccess DeleteAccess(VpnRpcDeleteAccess input_param) => DeleteAccessAsync(input_param).Result;

        /// <summary>
        /// Get access list (Async mode)
        /// </summary>
        public async Task<VpnRpcEnumAccessList> EnumAccessAsync(VpnRpcEnumAccessList input_param) => await Call<VpnRpcEnumAccessList>("EnumAccess", input_param);

        /// <summary>
        /// Get access list (Sync mode)
        /// </summary>
        public VpnRpcEnumAccessList EnumAccess(VpnRpcEnumAccessList input_param) => EnumAccessAsync(input_param).Result;

        /// <summary>
        /// Set access list (Async mode)
        /// </summary>
        public async Task<VpnRpcEnumAccessList> SetAccessListAsync(VpnRpcEnumAccessList input_param) => await Call<VpnRpcEnumAccessList>("SetAccessList", input_param);

        /// <summary>
        /// Set access list (Sync mode)
        /// </summary>
        public VpnRpcEnumAccessList SetAccessList(VpnRpcEnumAccessList input_param) => SetAccessListAsync(input_param).Result;

        /// <summary>
        /// Create a user (Async mode)
        /// </summary>
        public async Task<VpnRpcSetUser> CreateUserAsync(VpnRpcSetUser input_param) => await Call<VpnRpcSetUser>("CreateUser", input_param);

        /// <summary>
        /// Create a user (Sync mode)
        /// </summary>
        public VpnRpcSetUser CreateUser(VpnRpcSetUser input_param) => CreateUserAsync(input_param).Result;

        /// <summary>
        /// Set user setting (Async mode)
        /// </summary>
        public async Task<VpnRpcSetUser> SetUserAsync(VpnRpcSetUser input_param) => await Call<VpnRpcSetUser>("SetUser", input_param);

        /// <summary>
        /// Set user setting (Sync mode)
        /// </summary>
        public VpnRpcSetUser SetUser(VpnRpcSetUser input_param) => SetUserAsync(input_param).Result;

        /// <summary>
        /// Get user setting (Async mode)
        /// </summary>
        public async Task<VpnRpcSetUser> GetUserAsync(VpnRpcSetUser input_param) => await Call<VpnRpcSetUser>("GetUser", input_param);

        /// <summary>
        /// Get user setting (Sync mode)
        /// </summary>
        public VpnRpcSetUser GetUser(VpnRpcSetUser input_param) => GetUserAsync(input_param).Result;

        /// <summary>
        /// Delete a user (Async mode)
        /// </summary>
        public async Task<VpnRpcDeleteUser> DeleteUserAsync(VpnRpcDeleteUser input_param) => await Call<VpnRpcDeleteUser>("DeleteUser", input_param);

        /// <summary>
        /// Delete a user (Sync mode)
        /// </summary>
        public VpnRpcDeleteUser DeleteUser(VpnRpcDeleteUser input_param) => DeleteUserAsync(input_param).Result;

        /// <summary>
        /// Enumerate users (Async mode)
        /// </summary>
        public async Task<VpnRpcEnumUser> EnumUserAsync(VpnRpcEnumUser input_param) => await Call<VpnRpcEnumUser>("EnumUser", input_param);

        /// <summary>
        /// Enumerate users (Sync mode)
        /// </summary>
        public VpnRpcEnumUser EnumUser(VpnRpcEnumUser input_param) => EnumUserAsync(input_param).Result;

        /// <summary>
        /// Create a group (Async mode)
        /// </summary>
        public async Task<VpnRpcSetGroup> CreateGroupAsync(VpnRpcSetGroup input_param) => await Call<VpnRpcSetGroup>("CreateGroup", input_param);

        /// <summary>
        /// Create a group (Sync mode)
        /// </summary>
        public VpnRpcSetGroup CreateGroup(VpnRpcSetGroup input_param) => CreateGroupAsync(input_param).Result;

        /// <summary>
        /// Set group setting (Async mode)
        /// </summary>
        public async Task<VpnRpcSetGroup> SetGroupAsync(VpnRpcSetGroup input_param) => await Call<VpnRpcSetGroup>("SetGroup", input_param);

        /// <summary>
        /// Set group setting (Sync mode)
        /// </summary>
        public VpnRpcSetGroup SetGroup(VpnRpcSetGroup input_param) => SetGroupAsync(input_param).Result;

        /// <summary>
        /// Get group information (Async mode)
        /// </summary>
        public async Task<VpnRpcSetGroup> GetGroupAsync(VpnRpcSetGroup input_param) => await Call<VpnRpcSetGroup>("GetGroup", input_param);

        /// <summary>
        /// Get group information (Sync mode)
        /// </summary>
        public VpnRpcSetGroup GetGroup(VpnRpcSetGroup input_param) => GetGroupAsync(input_param).Result;

        /// <summary>
        /// Delete a group (Async mode)
        /// </summary>
        public async Task<VpnRpcDeleteUser> DeleteGroupAsync(VpnRpcDeleteUser input_param) => await Call<VpnRpcDeleteUser>("DeleteGroup", input_param);

        /// <summary>
        /// Delete a group (Sync mode)
        /// </summary>
        public VpnRpcDeleteUser DeleteGroup(VpnRpcDeleteUser input_param) => DeleteGroupAsync(input_param).Result;

        /// <summary>
        /// Enumerate groups (Async mode)
        /// </summary>
        public async Task<VpnRpcEnumGroup> EnumGroupAsync(VpnRpcEnumGroup input_param) => await Call<VpnRpcEnumGroup>("EnumGroup", input_param);

        /// <summary>
        /// Enumerate groups (Sync mode)
        /// </summary>
        public VpnRpcEnumGroup EnumGroup(VpnRpcEnumGroup input_param) => EnumGroupAsync(input_param).Result;

        /// <summary>
        /// Enumerate sessions (Async mode)
        /// </summary>
        public async Task<VpnRpcEnumSession> EnumSessionAsync(VpnRpcEnumSession input_param) => await Call<VpnRpcEnumSession>("EnumSession", input_param);

        /// <summary>
        /// Enumerate sessions (Sync mode)
        /// </summary>
        public VpnRpcEnumSession EnumSession(VpnRpcEnumSession input_param) => EnumSessionAsync(input_param).Result;

        /// <summary>
        /// Get session status (Async mode)
        /// </summary>
        public async Task<VpnRpcSessionStatus> GetSessionStatusAsync(VpnRpcSessionStatus input_param) => await Call<VpnRpcSessionStatus>("GetSessionStatus", input_param);

        /// <summary>
        /// Get session status (Sync mode)
        /// </summary>
        public VpnRpcSessionStatus GetSessionStatus(VpnRpcSessionStatus input_param) => GetSessionStatusAsync(input_param).Result;

        /// <summary>
        /// Delete a session (Async mode)
        /// </summary>
        public async Task<VpnRpcDeleteSession> DeleteSessionAsync(VpnRpcDeleteSession input_param) => await Call<VpnRpcDeleteSession>("DeleteSession", input_param);

        /// <summary>
        /// Delete a session (Sync mode)
        /// </summary>
        public VpnRpcDeleteSession DeleteSession(VpnRpcDeleteSession input_param) => DeleteSessionAsync(input_param).Result;

        /// <summary>
        /// Get MAC address table (Async mode)
        /// </summary>
        public async Task<VpnRpcEnumMacTable> EnumMacTableAsync(VpnRpcEnumMacTable input_param) => await Call<VpnRpcEnumMacTable>("EnumMacTable", input_param);

        /// <summary>
        /// Get MAC address table (Sync mode)
        /// </summary>
        public VpnRpcEnumMacTable EnumMacTable(VpnRpcEnumMacTable input_param) => EnumMacTableAsync(input_param).Result;

        /// <summary>
        /// Delete MAC address table entry (Async mode)
        /// </summary>
        public async Task<VpnRpcDeleteTable> DeleteMacTableAsync(VpnRpcDeleteTable input_param) => await Call<VpnRpcDeleteTable>("DeleteMacTable", input_param);

        /// <summary>
        /// Delete MAC address table entry (Sync mode)
        /// </summary>
        public VpnRpcDeleteTable DeleteMacTable(VpnRpcDeleteTable input_param) => DeleteMacTableAsync(input_param).Result;

        /// <summary>
        /// Get IP address table (Async mode)
        /// </summary>
        public async Task<VpnRpcEnumIpTable> EnumIpTableAsync(VpnRpcEnumIpTable input_param) => await Call<VpnRpcEnumIpTable>("EnumIpTable", input_param);

        /// <summary>
        /// Get IP address table (Sync mode)
        /// </summary>
        public VpnRpcEnumIpTable EnumIpTable(VpnRpcEnumIpTable input_param) => EnumIpTableAsync(input_param).Result;

        /// <summary>
        /// Delete IP address table entry (Async mode)
        /// </summary>
        public async Task<VpnRpcDeleteTable> DeleteIpTableAsync(VpnRpcDeleteTable input_param) => await Call<VpnRpcDeleteTable>("DeleteIpTable", input_param);

        /// <summary>
        /// Delete IP address table entry (Sync mode)
        /// </summary>
        public VpnRpcDeleteTable DeleteIpTable(VpnRpcDeleteTable input_param) => DeleteIpTableAsync(input_param).Result;

        /// <summary>
        /// Set keep-alive function setting (Async mode)
        /// </summary>
        public async Task<VpnRpcKeep> SetKeepAsync(VpnRpcKeep input_param) => await Call<VpnRpcKeep>("SetKeep", input_param);

        /// <summary>
        /// Set keep-alive function setting (Sync mode)
        /// </summary>
        public VpnRpcKeep SetKeep(VpnRpcKeep input_param) => SetKeepAsync(input_param).Result;

        /// <summary>
        /// Get keep-alive function setting (Async mode)
        /// </summary>
        public async Task<VpnRpcKeep> GetKeepAsync(VpnRpcKeep input_param) => await Call<VpnRpcKeep>("GetKeep", input_param);

        /// <summary>
        /// Get keep-alive function setting (Sync mode)
        /// </summary>
        public VpnRpcKeep GetKeep(VpnRpcKeep input_param) => GetKeepAsync(input_param).Result;

        /// <summary>
        /// Enable SecureNAT function of the hub (Async mode)
        /// </summary>
        public async Task<VpnRpcHub> EnableSecureNATAsync(VpnRpcHub input_param) => await Call<VpnRpcHub>("EnableSecureNAT", input_param);

        /// <summary>
        /// Enable SecureNAT function of the hub (Sync mode)
        /// </summary>
        public VpnRpcHub EnableSecureNAT(VpnRpcHub input_param) => EnableSecureNATAsync(input_param).Result;

        /// <summary>
        /// Disable the SecureNAT function of the hub (Async mode)
        /// </summary>
        public async Task<VpnRpcHub> DisableSecureNATAsync(VpnRpcHub input_param) => await Call<VpnRpcHub>("DisableSecureNAT", input_param);

        /// <summary>
        /// Disable the SecureNAT function of the hub (Sync mode)
        /// </summary>
        public VpnRpcHub DisableSecureNAT(VpnRpcHub input_param) => DisableSecureNATAsync(input_param).Result;

        /// <summary>
        /// Set SecureNAT options (Async mode)
        /// </summary>
        public async Task<VpnVhOption> SetSecureNATOptionAsync(VpnVhOption input_param) => await Call<VpnVhOption>("SetSecureNATOption", input_param);

        /// <summary>
        /// Set SecureNAT options (Sync mode)
        /// </summary>
        public VpnVhOption SetSecureNATOption(VpnVhOption input_param) => SetSecureNATOptionAsync(input_param).Result;

        /// <summary>
        /// Get SecureNAT options (Async mode)
        /// </summary>
        public async Task<VpnVhOption> GetSecureNATOptionAsync(VpnVhOption input_param) => await Call<VpnVhOption>("GetSecureNATOption", input_param);

        /// <summary>
        /// Get SecureNAT options (Sync mode)
        /// </summary>
        public VpnVhOption GetSecureNATOption(VpnVhOption input_param) => GetSecureNATOptionAsync(input_param).Result;

        /// <summary>
        /// Enumerate NAT entries of the SecureNAT (Async mode)
        /// </summary>
        public async Task<VpnRpcEnumNat> EnumNATAsync(VpnRpcEnumNat input_param) => await Call<VpnRpcEnumNat>("EnumNAT", input_param);

        /// <summary>
        /// Enumerate NAT entries of the SecureNAT (Sync mode)
        /// </summary>
        public VpnRpcEnumNat EnumNAT(VpnRpcEnumNat input_param) => EnumNATAsync(input_param).Result;

        /// <summary>
        /// Enumerate DHCP entries (Async mode)
        /// </summary>
        public async Task<VpnRpcEnumDhcp> EnumDHCPAsync(VpnRpcEnumDhcp input_param) => await Call<VpnRpcEnumDhcp>("EnumDHCP", input_param);

        /// <summary>
        /// Enumerate DHCP entries (Sync mode)
        /// </summary>
        public VpnRpcEnumDhcp EnumDHCP(VpnRpcEnumDhcp input_param) => EnumDHCPAsync(input_param).Result;

        /// <summary>
        /// Get status of the SecureNAT (Async mode)
        /// </summary>
        public async Task<VpnRpcNatStatus> GetSecureNATStatusAsync(VpnRpcNatStatus input_param) => await Call<VpnRpcNatStatus>("GetSecureNATStatus", input_param);

        /// <summary>
        /// Get status of the SecureNAT (Sync mode)
        /// </summary>
        public VpnRpcNatStatus GetSecureNATStatus(VpnRpcNatStatus input_param) => GetSecureNATStatusAsync(input_param).Result;

        /// <summary>
        /// Enumerate Ethernet devices (Async mode)
        /// </summary>
        public async Task<VpnRpcEnumEth> EnumEthernetAsync(VpnRpcEnumEth input_param) => await Call<VpnRpcEnumEth>("EnumEthernet", input_param);

        /// <summary>
        /// Enumerate Ethernet devices (Sync mode)
        /// </summary>
        public VpnRpcEnumEth EnumEthernet(VpnRpcEnumEth input_param) => EnumEthernetAsync(input_param).Result;

        /// <summary>
        /// Add a new local bridge (Async mode)
        /// </summary>
        public async Task<VpnRpcLocalBridge> AddLocalBridgeAsync(VpnRpcLocalBridge input_param) => await Call<VpnRpcLocalBridge>("AddLocalBridge", input_param);

        /// <summary>
        /// Add a new local bridge (Sync mode)
        /// </summary>
        public VpnRpcLocalBridge AddLocalBridge(VpnRpcLocalBridge input_param) => AddLocalBridgeAsync(input_param).Result;

        /// <summary>
        /// Delete a local bridge (Async mode)
        /// </summary>
        public async Task<VpnRpcLocalBridge> DeleteLocalBridgeAsync(VpnRpcLocalBridge input_param) => await Call<VpnRpcLocalBridge>("DeleteLocalBridge", input_param);

        /// <summary>
        /// Delete a local bridge (Sync mode)
        /// </summary>
        public VpnRpcLocalBridge DeleteLocalBridge(VpnRpcLocalBridge input_param) => DeleteLocalBridgeAsync(input_param).Result;

        /// <summary>
        /// Enumerate local bridges (Async mode)
        /// </summary>
        public async Task<VpnRpcEnumLocalBridge> EnumLocalBridgeAsync(VpnRpcEnumLocalBridge input_param) => await Call<VpnRpcEnumLocalBridge>("EnumLocalBridge", input_param);

        /// <summary>
        /// Enumerate local bridges (Sync mode)
        /// </summary>
        public VpnRpcEnumLocalBridge EnumLocalBridge(VpnRpcEnumLocalBridge input_param) => EnumLocalBridgeAsync(input_param).Result;

        /// <summary>
        /// Get availability to localbridge function (Async mode)
        /// </summary>
        public async Task<VpnRpcBridgeSupport> GetBridgeSupportAsync(VpnRpcBridgeSupport input_param) => await Call<VpnRpcBridgeSupport>("GetBridgeSupport", input_param);

        /// <summary>
        /// Get availability to localbridge function (Sync mode)
        /// </summary>
        public VpnRpcBridgeSupport GetBridgeSupport(VpnRpcBridgeSupport input_param) => GetBridgeSupportAsync(input_param).Result;

        /// <summary>
        /// Reboot server itself (Async mode)
        /// </summary>
        public async Task<VpnRpcTest> RebootServerAsync(VpnRpcTest input_param) => await Call<VpnRpcTest>("RebootServer", input_param);

        /// <summary>
        /// Reboot server itself (Sync mode)
        /// </summary>
        public VpnRpcTest RebootServer(VpnRpcTest input_param) => RebootServerAsync(input_param).Result;

        /// <summary>
        /// Get capabilities (Async mode)
        /// </summary>
        public async Task<VpnCapslist> GetCapsAsync(VpnCapslist input_param) => await Call<VpnCapslist>("GetCaps", input_param);

        /// <summary>
        /// Get capabilities (Sync mode)
        /// </summary>
        public VpnCapslist GetCaps(VpnCapslist input_param) => GetCapsAsync(input_param).Result;

        /// <summary>
        /// Get configuration file stream (Async mode)
        /// </summary>
        public async Task<VpnRpcConfig> GetConfigAsync(VpnRpcConfig input_param) => await Call<VpnRpcConfig>("GetConfig", input_param);

        /// <summary>
        /// Get configuration file stream (Sync mode)
        /// </summary>
        public VpnRpcConfig GetConfig(VpnRpcConfig input_param) => GetConfigAsync(input_param).Result;

        /// <summary>
        /// Overwrite configuration file by specified data (Async mode)
        /// </summary>
        public async Task<VpnRpcConfig> SetConfigAsync(VpnRpcConfig input_param) => await Call<VpnRpcConfig>("SetConfig", input_param);

        /// <summary>
        /// Overwrite configuration file by specified data (Sync mode)
        /// </summary>
        public VpnRpcConfig SetConfig(VpnRpcConfig input_param) => SetConfigAsync(input_param).Result;

        /// <summary>
        /// Get default hub administration options (Async mode)
        /// </summary>
        public async Task<VpnRpcAdminOption> GetDefaultHubAdminOptionsAsync(VpnRpcAdminOption input_param) => await Call<VpnRpcAdminOption>("GetDefaultHubAdminOptions", input_param);

        /// <summary>
        /// Get default hub administration options (Sync mode)
        /// </summary>
        public VpnRpcAdminOption GetDefaultHubAdminOptions(VpnRpcAdminOption input_param) => GetDefaultHubAdminOptionsAsync(input_param).Result;

        /// <summary>
        /// Get hub administration options (Async mode)
        /// </summary>
        public async Task<VpnRpcAdminOption> GetHubAdminOptionsAsync(VpnRpcAdminOption input_param) => await Call<VpnRpcAdminOption>("GetHubAdminOptions", input_param);

        /// <summary>
        /// Get hub administration options (Sync mode)
        /// </summary>
        public VpnRpcAdminOption GetHubAdminOptions(VpnRpcAdminOption input_param) => GetHubAdminOptionsAsync(input_param).Result;

        /// <summary>
        /// Set hub administration options (Async mode)
        /// </summary>
        public async Task<VpnRpcAdminOption> SetHubAdminOptionsAsync(VpnRpcAdminOption input_param) => await Call<VpnRpcAdminOption>("SetHubAdminOptions", input_param);

        /// <summary>
        /// Set hub administration options (Sync mode)
        /// </summary>
        public VpnRpcAdminOption SetHubAdminOptions(VpnRpcAdminOption input_param) => SetHubAdminOptionsAsync(input_param).Result;

        /// <summary>
        /// Get hub extended options (Async mode)
        /// </summary>
        public async Task<VpnRpcAdminOption> GetHubExtOptionsAsync(VpnRpcAdminOption input_param) => await Call<VpnRpcAdminOption>("GetHubExtOptions", input_param);

        /// <summary>
        /// Get hub extended options (Sync mode)
        /// </summary>
        public VpnRpcAdminOption GetHubExtOptions(VpnRpcAdminOption input_param) => GetHubExtOptionsAsync(input_param).Result;

        /// <summary>
        /// Set hub extended options (Async mode)
        /// </summary>
        public async Task<VpnRpcAdminOption> SetHubExtOptionsAsync(VpnRpcAdminOption input_param) => await Call<VpnRpcAdminOption>("SetHubExtOptions", input_param);

        /// <summary>
        /// Set hub extended options (Sync mode)
        /// </summary>
        public VpnRpcAdminOption SetHubExtOptions(VpnRpcAdminOption input_param) => SetHubExtOptionsAsync(input_param).Result;

        /// <summary>
        /// Add a new virtual layer-3 switch (Async mode)
        /// </summary>
        public async Task<VpnRpcL3Sw> AddL3SwitchAsync(VpnRpcL3Sw input_param) => await Call<VpnRpcL3Sw>("AddL3Switch", input_param);

        /// <summary>
        /// Add a new virtual layer-3 switch (Sync mode)
        /// </summary>
        public VpnRpcL3Sw AddL3Switch(VpnRpcL3Sw input_param) => AddL3SwitchAsync(input_param).Result;

        /// <summary>
        /// Delete a virtual layer-3 switch (Async mode)
        /// </summary>
        public async Task<VpnRpcL3Sw> DelL3SwitchAsync(VpnRpcL3Sw input_param) => await Call<VpnRpcL3Sw>("DelL3Switch", input_param);

        /// <summary>
        /// Delete a virtual layer-3 switch (Sync mode)
        /// </summary>
        public VpnRpcL3Sw DelL3Switch(VpnRpcL3Sw input_param) => DelL3SwitchAsync(input_param).Result;

        /// <summary>
        /// Enumerate virtual layer-3 switches (Async mode)
        /// </summary>
        public async Task<VpnRpcEnumL3Sw> EnumL3SwitchAsync(VpnRpcEnumL3Sw input_param) => await Call<VpnRpcEnumL3Sw>("EnumL3Switch", input_param);

        /// <summary>
        /// Enumerate virtual layer-3 switches (Sync mode)
        /// </summary>
        public VpnRpcEnumL3Sw EnumL3Switch(VpnRpcEnumL3Sw input_param) => EnumL3SwitchAsync(input_param).Result;

        /// <summary>
        /// Start a virtual layer-3 switch (Async mode)
        /// </summary>
        public async Task<VpnRpcL3Sw> StartL3SwitchAsync(VpnRpcL3Sw input_param) => await Call<VpnRpcL3Sw>("StartL3Switch", input_param);

        /// <summary>
        /// Start a virtual layer-3 switch (Sync mode)
        /// </summary>
        public VpnRpcL3Sw StartL3Switch(VpnRpcL3Sw input_param) => StartL3SwitchAsync(input_param).Result;

        /// <summary>
        /// Stop a virtual layer-3 switch (Async mode)
        /// </summary>
        public async Task<VpnRpcL3Sw> StopL3SwitchAsync(VpnRpcL3Sw input_param) => await Call<VpnRpcL3Sw>("StopL3Switch", input_param);

        /// <summary>
        /// Stop a virtual layer-3 switch (Sync mode)
        /// </summary>
        public VpnRpcL3Sw StopL3Switch(VpnRpcL3Sw input_param) => StopL3SwitchAsync(input_param).Result;

        /// <summary>
        /// Add new virtual interface on virtual L3 switch (Async mode)
        /// </summary>
        public async Task<VpnRpcL3If> AddL3IfAsync(VpnRpcL3If input_param) => await Call<VpnRpcL3If>("AddL3If", input_param);

        /// <summary>
        /// Add new virtual interface on virtual L3 switch (Sync mode)
        /// </summary>
        public VpnRpcL3If AddL3If(VpnRpcL3If input_param) => AddL3IfAsync(input_param).Result;

        /// <summary>
        /// Delete a virtual interface on virtual L3 switch (Async mode)
        /// </summary>
        public async Task<VpnRpcL3If> DelL3IfAsync(VpnRpcL3If input_param) => await Call<VpnRpcL3If>("DelL3If", input_param);

        /// <summary>
        /// Delete a virtual interface on virtual L3 switch (Sync mode)
        /// </summary>
        public VpnRpcL3If DelL3If(VpnRpcL3If input_param) => DelL3IfAsync(input_param).Result;

        /// <summary>
        /// Enumerate virtual interfaces on virtual L3 switch (Async mode)
        /// </summary>
        public async Task<VpnRpcEnumL3If> EnumL3IfAsync(VpnRpcEnumL3If input_param) => await Call<VpnRpcEnumL3If>("EnumL3If", input_param);

        /// <summary>
        /// Enumerate virtual interfaces on virtual L3 switch (Sync mode)
        /// </summary>
        public VpnRpcEnumL3If EnumL3If(VpnRpcEnumL3If input_param) => EnumL3IfAsync(input_param).Result;

        /// <summary>
        /// Add new routing table entry on virtual L3 switch (Async mode)
        /// </summary>
        public async Task<VpnRpcL3Table> AddL3TableAsync(VpnRpcL3Table input_param) => await Call<VpnRpcL3Table>("AddL3Table", input_param);

        /// <summary>
        /// Add new routing table entry on virtual L3 switch (Sync mode)
        /// </summary>
        public VpnRpcL3Table AddL3Table(VpnRpcL3Table input_param) => AddL3TableAsync(input_param).Result;

        /// <summary>
        /// Delete routing table entry on virtual L3 switch (Async mode)
        /// </summary>
        public async Task<VpnRpcL3Table> DelL3TableAsync(VpnRpcL3Table input_param) => await Call<VpnRpcL3Table>("DelL3Table", input_param);

        /// <summary>
        /// Delete routing table entry on virtual L3 switch (Sync mode)
        /// </summary>
        public VpnRpcL3Table DelL3Table(VpnRpcL3Table input_param) => DelL3TableAsync(input_param).Result;

        /// <summary>
        /// Get routing table on virtual L3 switch (Async mode)
        /// </summary>
        public async Task<VpnRpcEnumL3Table> EnumL3TableAsync(VpnRpcEnumL3Table input_param) => await Call<VpnRpcEnumL3Table>("EnumL3Table", input_param);

        /// <summary>
        /// Get routing table on virtual L3 switch (Sync mode)
        /// </summary>
        public VpnRpcEnumL3Table EnumL3Table(VpnRpcEnumL3Table input_param) => EnumL3TableAsync(input_param).Result;

        /// <summary>
        /// Get CRL (Certificate Revocation List) index (Async mode)
        /// </summary>
        public async Task<VpnRpcEnumCrl> EnumCrlAsync(VpnRpcEnumCrl input_param) => await Call<VpnRpcEnumCrl>("EnumCrl", input_param);

        /// <summary>
        /// Get CRL (Certificate Revocation List) index (Sync mode)
        /// </summary>
        public VpnRpcEnumCrl EnumCrl(VpnRpcEnumCrl input_param) => EnumCrlAsync(input_param).Result;

        /// <summary>
        /// Add new CRL (Certificate Revocation List) entry (Async mode)
        /// </summary>
        public async Task<VpnRpcCrl> AddCrlAsync(VpnRpcCrl input_param) => await Call<VpnRpcCrl>("AddCrl", input_param);

        /// <summary>
        /// Add new CRL (Certificate Revocation List) entry (Sync mode)
        /// </summary>
        public VpnRpcCrl AddCrl(VpnRpcCrl input_param) => AddCrlAsync(input_param).Result;

        /// <summary>
        /// Delete CRL (Certificate Revocation List) entry (Async mode)
        /// </summary>
        public async Task<VpnRpcCrl> DelCrlAsync(VpnRpcCrl input_param) => await Call<VpnRpcCrl>("DelCrl", input_param);

        /// <summary>
        /// Delete CRL (Certificate Revocation List) entry (Sync mode)
        /// </summary>
        public VpnRpcCrl DelCrl(VpnRpcCrl input_param) => DelCrlAsync(input_param).Result;

        /// <summary>
        /// Get CRL (Certificate Revocation List) entry (Async mode)
        /// </summary>
        public async Task<VpnRpcCrl> GetCrlAsync(VpnRpcCrl input_param) => await Call<VpnRpcCrl>("GetCrl", input_param);

        /// <summary>
        /// Get CRL (Certificate Revocation List) entry (Sync mode)
        /// </summary>
        public VpnRpcCrl GetCrl(VpnRpcCrl input_param) => GetCrlAsync(input_param).Result;

        /// <summary>
        /// Set CRL (Certificate Revocation List) entry (Async mode)
        /// </summary>
        public async Task<VpnRpcCrl> SetCrlAsync(VpnRpcCrl input_param) => await Call<VpnRpcCrl>("SetCrl", input_param);

        /// <summary>
        /// Set CRL (Certificate Revocation List) entry (Sync mode)
        /// </summary>
        public VpnRpcCrl SetCrl(VpnRpcCrl input_param) => SetCrlAsync(input_param).Result;

        /// <summary>
        /// Set access control list (Async mode)
        /// </summary>
        public async Task<VpnRpcAcList> SetAcListAsync(VpnRpcAcList input_param) => await Call<VpnRpcAcList>("SetAcList", input_param);

        /// <summary>
        /// Set access control list (Sync mode)
        /// </summary>
        public VpnRpcAcList SetAcList(VpnRpcAcList input_param) => SetAcListAsync(input_param).Result;

        /// <summary>
        /// Get access control list (Async mode)
        /// </summary>
        public async Task<VpnRpcAcList> GetAcListAsync(VpnRpcAcList input_param) => await Call<VpnRpcAcList>("GetAcList", input_param);

        /// <summary>
        /// Get access control list (Sync mode)
        /// </summary>
        public VpnRpcAcList GetAcList(VpnRpcAcList input_param) => GetAcListAsync(input_param).Result;

        /// <summary>
        /// Enumerate log files (Async mode)
        /// </summary>
        public async Task<VpnRpcEnumLogFile> EnumLogFileAsync(VpnRpcEnumLogFile input_param) => await Call<VpnRpcEnumLogFile>("EnumLogFile", input_param);

        /// <summary>
        /// Enumerate log files (Sync mode)
        /// </summary>
        public VpnRpcEnumLogFile EnumLogFile(VpnRpcEnumLogFile input_param) => EnumLogFileAsync(input_param).Result;

        /// <summary>
        /// Read a log file (Async mode)
        /// </summary>
        public async Task<VpnRpcReadLogFile> ReadLogFileAsync(VpnRpcReadLogFile input_param) => await Call<VpnRpcReadLogFile>("ReadLogFile", input_param);

        /// <summary>
        /// Read a log file (Sync mode)
        /// </summary>
        public VpnRpcReadLogFile ReadLogFile(VpnRpcReadLogFile input_param) => ReadLogFileAsync(input_param).Result;

        /// <summary>
        /// Add new license key (Async mode)
        /// </summary>
        public async Task<VpnRpcTest> AddLicenseKeyAsync(VpnRpcTest input_param) => await Call<VpnRpcTest>("AddLicenseKey", input_param);

        /// <summary>
        /// Add new license key (Sync mode)
        /// </summary>
        public VpnRpcTest AddLicenseKey(VpnRpcTest input_param) => AddLicenseKeyAsync(input_param).Result;

        /// <summary>
        /// Delete a license key (Async mode)
        /// </summary>
        public async Task<VpnRpcTest> DelLicenseKeyAsync(VpnRpcTest input_param) => await Call<VpnRpcTest>("DelLicenseKey", input_param);

        /// <summary>
        /// Delete a license key (Sync mode)
        /// </summary>
        public VpnRpcTest DelLicenseKey(VpnRpcTest input_param) => DelLicenseKeyAsync(input_param).Result;

        /// <summary>
        /// Enumerate license key (Async mode)
        /// </summary>
        public async Task<VpnRpcEnumLicenseKey> EnumLicenseKeyAsync(VpnRpcEnumLicenseKey input_param) => await Call<VpnRpcEnumLicenseKey>("EnumLicenseKey", input_param);

        /// <summary>
        /// Enumerate license key (Sync mode)
        /// </summary>
        public VpnRpcEnumLicenseKey EnumLicenseKey(VpnRpcEnumLicenseKey input_param) => EnumLicenseKeyAsync(input_param).Result;

        /// <summary>
        /// Get license status (Async mode)
        /// </summary>
        public async Task<VpnRpcLicenseStatus> GetLicenseStatusAsync(VpnRpcLicenseStatus input_param) => await Call<VpnRpcLicenseStatus>("GetLicenseStatus", input_param);

        /// <summary>
        /// Get license status (Sync mode)
        /// </summary>
        public VpnRpcLicenseStatus GetLicenseStatus(VpnRpcLicenseStatus input_param) => GetLicenseStatusAsync(input_param).Result;

        /// <summary>
        /// Set syslog function setting (Async mode)
        /// </summary>
        public async Task<VpnSyslogSetting> SetSysLogAsync(VpnSyslogSetting input_param) => await Call<VpnSyslogSetting>("SetSysLog", input_param);

        /// <summary>
        /// Set syslog function setting (Sync mode)
        /// </summary>
        public VpnSyslogSetting SetSysLog(VpnSyslogSetting input_param) => SetSysLogAsync(input_param).Result;

        /// <summary>
        /// Get syslog function setting (Async mode)
        /// </summary>
        public async Task<VpnSyslogSetting> GetSysLogAsync(VpnSyslogSetting input_param) => await Call<VpnSyslogSetting>("GetSysLog", input_param);

        /// <summary>
        /// Get syslog function setting (Sync mode)
        /// </summary>
        public VpnSyslogSetting GetSysLog(VpnSyslogSetting input_param) => GetSysLogAsync(input_param).Result;

        /// <summary>
        /// Enumerate VLAN tag transparent setting (Async mode)
        /// </summary>
        public async Task<VpnRpcEnumEthVlan> EnumEthVLanAsync(VpnRpcEnumEthVlan input_param) => await Call<VpnRpcEnumEthVlan>("EnumEthVLan", input_param);

        /// <summary>
        /// Enumerate VLAN tag transparent setting (Sync mode)
        /// </summary>
        public VpnRpcEnumEthVlan EnumEthVLan(VpnRpcEnumEthVlan input_param) => EnumEthVLanAsync(input_param).Result;

        /// <summary>
        /// Set VLAN tag transparent setting (Async mode)
        /// </summary>
        public async Task<VpnRpcTest> SetEnableEthVLanAsync(VpnRpcTest input_param) => await Call<VpnRpcTest>("SetEnableEthVLan", input_param);

        /// <summary>
        /// Set VLAN tag transparent setting (Sync mode)
        /// </summary>
        public VpnRpcTest SetEnableEthVLan(VpnRpcTest input_param) => SetEnableEthVLanAsync(input_param).Result;

        /// <summary>
        /// Set message of today on hub (Async mode)
        /// </summary>
        public async Task<VpnRpcMsg> SetHubMsgAsync(VpnRpcMsg input_param) => await Call<VpnRpcMsg>("SetHubMsg", input_param);

        /// <summary>
        /// Set message of today on hub (Sync mode)
        /// </summary>
        public VpnRpcMsg SetHubMsg(VpnRpcMsg input_param) => SetHubMsgAsync(input_param).Result;

        /// <summary>
        /// Get message of today on hub (Async mode)
        /// </summary>
        public async Task<VpnRpcMsg> GetHubMsgAsync(VpnRpcMsg input_param) => await Call<VpnRpcMsg>("GetHubMsg", input_param);

        /// <summary>
        /// Get message of today on hub (Sync mode)
        /// </summary>
        public VpnRpcMsg GetHubMsg(VpnRpcMsg input_param) => GetHubMsgAsync(input_param).Result;

        /// <summary>
        /// Do Crash (Async mode)
        /// </summary>
        public async Task<VpnRpcTest> CrashAsync(VpnRpcTest input_param) => await Call<VpnRpcTest>("Crash", input_param);

        /// <summary>
        /// Do Crash (Sync mode)
        /// </summary>
        public VpnRpcTest Crash(VpnRpcTest input_param) => CrashAsync(input_param).Result;

        /// <summary>
        /// Get message for administrators (Async mode)
        /// </summary>
        public async Task<VpnRpcMsg> GetAdminMsgAsync(VpnRpcMsg input_param) => await Call<VpnRpcMsg>("GetAdminMsg", input_param);

        /// <summary>
        /// Get message for administrators (Sync mode)
        /// </summary>
        public VpnRpcMsg GetAdminMsg(VpnRpcMsg input_param) => GetAdminMsgAsync(input_param).Result;

        /// <summary>
        /// Flush configuration file (Async mode)
        /// </summary>
        public async Task<VpnRpcTest> FlushAsync(VpnRpcTest input_param) => await Call<VpnRpcTest>("Flush", input_param);

        /// <summary>
        /// Flush configuration file (Sync mode)
        /// </summary>
        public VpnRpcTest Flush(VpnRpcTest input_param) => FlushAsync(input_param).Result;

        /// <summary>
        /// Do debug function (Async mode)
        /// </summary>
        public async Task<VpnRpcTest> DebugAsync(VpnRpcTest input_param) => await Call<VpnRpcTest>("Debug", input_param);

        /// <summary>
        /// Do debug function (Sync mode)
        /// </summary>
        public VpnRpcTest Debug(VpnRpcTest input_param) => DebugAsync(input_param).Result;

        /// <summary>
        /// Set IPsec service configuration (Async mode)
        /// </summary>
        public async Task<VpnIPsecServices> SetIPsecServicesAsync(VpnIPsecServices input_param) => await Call<VpnIPsecServices>("SetIPsecServices", input_param);

        /// <summary>
        /// Set IPsec service configuration (Sync mode)
        /// </summary>
        public VpnIPsecServices SetIPsecServices(VpnIPsecServices input_param) => SetIPsecServicesAsync(input_param).Result;

        /// <summary>
        /// Get IPsec service configuration (Async mode)
        /// </summary>
        public async Task<VpnIPsecServices> GetIPsecServicesAsync(VpnIPsecServices input_param) => await Call<VpnIPsecServices>("GetIPsecServices", input_param);

        /// <summary>
        /// Get IPsec service configuration (Sync mode)
        /// </summary>
        public VpnIPsecServices GetIPsecServices(VpnIPsecServices input_param) => GetIPsecServicesAsync(input_param).Result;

        /// <summary>
        /// Add EtherIP ID setting (Async mode)
        /// </summary>
        public async Task<VpnEtherIpId> AddEtherIpIdAsync(VpnEtherIpId input_param) => await Call<VpnEtherIpId>("AddEtherIpId", input_param);

        /// <summary>
        /// Add EtherIP ID setting (Sync mode)
        /// </summary>
        public VpnEtherIpId AddEtherIpId(VpnEtherIpId input_param) => AddEtherIpIdAsync(input_param).Result;

        /// <summary>
        /// Get EtherIP ID setting (Async mode)
        /// </summary>
        public async Task<VpnEtherIpId> GetEtherIpIdAsync(VpnEtherIpId input_param) => await Call<VpnEtherIpId>("GetEtherIpId", input_param);

        /// <summary>
        /// Get EtherIP ID setting (Sync mode)
        /// </summary>
        public VpnEtherIpId GetEtherIpId(VpnEtherIpId input_param) => GetEtherIpIdAsync(input_param).Result;

        /// <summary>
        /// Delete EtherIP ID setting (Async mode)
        /// </summary>
        public async Task<VpnEtherIpId> DeleteEtherIpIdAsync(VpnEtherIpId input_param) => await Call<VpnEtherIpId>("DeleteEtherIpId", input_param);

        /// <summary>
        /// Delete EtherIP ID setting (Sync mode)
        /// </summary>
        public VpnEtherIpId DeleteEtherIpId(VpnEtherIpId input_param) => DeleteEtherIpIdAsync(input_param).Result;

        /// <summary>
        /// Enumerate EtherIP ID settings (Async mode)
        /// </summary>
        public async Task<VpnRpcEnumEtherIpId> EnumEtherIpIdAsync(VpnRpcEnumEtherIpId input_param) => await Call<VpnRpcEnumEtherIpId>("EnumEtherIpId", input_param);

        /// <summary>
        /// Enumerate EtherIP ID settings (Sync mode)
        /// </summary>
        public VpnRpcEnumEtherIpId EnumEtherIpId(VpnRpcEnumEtherIpId input_param) => EnumEtherIpIdAsync(input_param).Result;

        /// <summary>
        /// Set configurations for OpenVPN and SSTP (Async mode)
        /// </summary>
        public async Task<VpnOpenVpnSstpConfig> SetOpenVpnSstpConfigAsync(VpnOpenVpnSstpConfig input_param) => await Call<VpnOpenVpnSstpConfig>("SetOpenVpnSstpConfig", input_param);

        /// <summary>
        /// Set configurations for OpenVPN and SSTP (Sync mode)
        /// </summary>
        public VpnOpenVpnSstpConfig SetOpenVpnSstpConfig(VpnOpenVpnSstpConfig input_param) => SetOpenVpnSstpConfigAsync(input_param).Result;

        /// <summary>
        /// Get configurations for OpenVPN and SSTP (Async mode)
        /// </summary>
        public async Task<VpnOpenVpnSstpConfig> GetOpenVpnSstpConfigAsync(VpnOpenVpnSstpConfig input_param) => await Call<VpnOpenVpnSstpConfig>("GetOpenVpnSstpConfig", input_param);

        /// <summary>
        /// Get configurations for OpenVPN and SSTP (Sync mode)
        /// </summary>
        public VpnOpenVpnSstpConfig GetOpenVpnSstpConfig(VpnOpenVpnSstpConfig input_param) => GetOpenVpnSstpConfigAsync(input_param).Result;

        /// <summary>
        /// Get status of DDNS client (Async mode)
        /// </summary>
        public async Task<VpnDDnsClientStatus> GetDDnsClientStatusAsync(VpnDDnsClientStatus input_param) => await Call<VpnDDnsClientStatus>("GetDDnsClientStatus", input_param);

        /// <summary>
        /// Get status of DDNS client (Sync mode)
        /// </summary>
        public VpnDDnsClientStatus GetDDnsClientStatus(VpnDDnsClientStatus input_param) => GetDDnsClientStatusAsync(input_param).Result;

        /// <summary>
        /// Change host-name for DDNS client (Async mode)
        /// </summary>
        public async Task<VpnRpcTest> ChangeDDnsClientHostnameAsync(VpnRpcTest input_param) => await Call<VpnRpcTest>("ChangeDDnsClientHostname", input_param);

        /// <summary>
        /// Change host-name for DDNS client (Sync mode)
        /// </summary>
        public VpnRpcTest ChangeDDnsClientHostname(VpnRpcTest input_param) => ChangeDDnsClientHostnameAsync(input_param).Result;

        /// <summary>
        /// Regenerate server certification (Async mode)
        /// </summary>
        public async Task<VpnRpcTest> RegenerateServerCertAsync(VpnRpcTest input_param) => await Call<VpnRpcTest>("RegenerateServerCert", input_param);

        /// <summary>
        /// Regenerate server certification (Sync mode)
        /// </summary>
        public VpnRpcTest RegenerateServerCert(VpnRpcTest input_param) => RegenerateServerCertAsync(input_param).Result;

        /// <summary>
        /// Generate OpenVPN configuration files (Async mode)
        /// </summary>
        public async Task<VpnRpcReadLogFile> MakeOpenVpnConfigFileAsync(VpnRpcReadLogFile input_param) => await Call<VpnRpcReadLogFile>("MakeOpenVpnConfigFile", input_param);

        /// <summary>
        /// Generate OpenVPN configuration files (Sync mode)
        /// </summary>
        public VpnRpcReadLogFile MakeOpenVpnConfigFile(VpnRpcReadLogFile input_param) => MakeOpenVpnConfigFileAsync(input_param).Result;

        /// <summary>
        /// Set special listener status (Async mode)
        /// </summary>
        public async Task<VpnRpcSpecialListener> SetSpecialListenerAsync(VpnRpcSpecialListener input_param) => await Call<VpnRpcSpecialListener>("SetSpecialListener", input_param);

        /// <summary>
        /// Set special listener status (Sync mode)
        /// </summary>
        public VpnRpcSpecialListener SetSpecialListener(VpnRpcSpecialListener input_param) => SetSpecialListenerAsync(input_param).Result;

        /// <summary>
        /// Get special listener status (Async mode)
        /// </summary>
        public async Task<VpnRpcSpecialListener> GetSpecialListenerAsync(VpnRpcSpecialListener input_param) => await Call<VpnRpcSpecialListener>("GetSpecialListener", input_param);

        /// <summary>
        /// Get special listener status (Sync mode)
        /// </summary>
        public VpnRpcSpecialListener GetSpecialListener(VpnRpcSpecialListener input_param) => GetSpecialListenerAsync(input_param).Result;

        /// <summary>
        /// Get Azure status (Async mode)
        /// </summary>
        public async Task<VpnRpcAzureStatus> GetAzureStatusAsync(VpnRpcAzureStatus input_param) => await Call<VpnRpcAzureStatus>("GetAzureStatus", input_param);

        /// <summary>
        /// Get Azure status (Sync mode)
        /// </summary>
        public VpnRpcAzureStatus GetAzureStatus(VpnRpcAzureStatus input_param) => GetAzureStatusAsync(input_param).Result;

        /// <summary>
        /// Set Azure status (Async mode)
        /// </summary>
        public async Task<VpnRpcAzureStatus> SetAzureStatusAsync(VpnRpcAzureStatus input_param) => await Call<VpnRpcAzureStatus>("SetAzureStatus", input_param);

        /// <summary>
        /// Set Azure status (Sync mode)
        /// </summary>
        public VpnRpcAzureStatus SetAzureStatus(VpnRpcAzureStatus input_param) => SetAzureStatusAsync(input_param).Result;

        /// <summary>
        /// Get DDNS proxy configuration (Async mode)
        /// </summary>
        public async Task<VpnInternetSetting> GetDDnsInternetSettngAsync(VpnInternetSetting input_param) => await Call<VpnInternetSetting>("GetDDnsInternetSettng", input_param);

        /// <summary>
        /// Get DDNS proxy configuration (Sync mode)
        /// </summary>
        public VpnInternetSetting GetDDnsInternetSettng(VpnInternetSetting input_param) => GetDDnsInternetSettngAsync(input_param).Result;

        /// <summary>
        /// Set DDNS proxy configuration (Async mode)
        /// </summary>
        public async Task<VpnInternetSetting> SetDDnsInternetSettngAsync(VpnInternetSetting input_param) => await Call<VpnInternetSetting>("SetDDnsInternetSettng", input_param);

        /// <summary>
        /// Set DDNS proxy configuration (Sync mode)
        /// </summary>
        public VpnInternetSetting SetDDnsInternetSettng(VpnInternetSetting input_param) => SetDDnsInternetSettngAsync(input_param).Result;

        /// <summary>
        /// Setting VPN Gate Server Configuration (Async mode)
        /// </summary>
        public async Task<VpnVgsConfig> SetVgsConfigAsync(VpnVgsConfig input_param) => await Call<VpnVgsConfig>("SetVgsConfig", input_param);

        /// <summary>
        /// Setting VPN Gate Server Configuration (Sync mode)
        /// </summary>
        public VpnVgsConfig SetVgsConfig(VpnVgsConfig input_param) => SetVgsConfigAsync(input_param).Result;

        /// <summary>
        /// Get VPN Gate configuration (Async mode)
        /// </summary>
        public async Task<VpnVgsConfig> GetVgsConfigAsync(VpnVgsConfig input_param) => await Call<VpnVgsConfig>("GetVgsConfig", input_param);

        /// <summary>
        /// Get VPN Gate configuration (Sync mode)
        /// </summary>
        public VpnVgsConfig GetVgsConfig(VpnVgsConfig input_param) => GetVgsConfigAsync(input_param).Result;


    }
}
