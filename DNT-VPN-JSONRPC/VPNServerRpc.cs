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

        /// <summary>
        /// Add access list entry (Async mode)
        /// </summary>
        public async Task<VpnRpcAddAccess> AddAccessAsync() => await Call<VpnRpcAddAccess>("AddAccess", new VpnRpcAddAccess());

        /// <summary>
        /// Add access list entry (Sync mode)
        /// </summary>
        public VpnRpcAddAccess AddAccess() => AddAccessAsync().Result;

        /// <summary>
        /// Add CA(Certificate Authority) into the hub (Async mode)
        /// </summary>
        public async Task<VpnRpcHubAddCA> AddCaAsync() => await Call<VpnRpcHubAddCA>("AddCa", new VpnRpcHubAddCA());

        /// <summary>
        /// Add CA(Certificate Authority) into the hub (Sync mode)
        /// </summary>
        public VpnRpcHubAddCA AddCa() => AddCaAsync().Result;

        /// <summary>
        /// Add new CRL (Certificate Revocation List) entry (Async mode)
        /// </summary>
        public async Task<VpnRpcCrl> AddCrlAsync() => await Call<VpnRpcCrl>("AddCrl", new VpnRpcCrl());

        /// <summary>
        /// Add new CRL (Certificate Revocation List) entry (Sync mode)
        /// </summary>
        public VpnRpcCrl AddCrl() => AddCrlAsync().Result;

        /// <summary>
        /// Add EtherIP ID setting (Async mode)
        /// </summary>
        public async Task<VpnEtherIpId> AddEtherIpIdAsync() => await Call<VpnEtherIpId>("AddEtherIpId", new VpnEtherIpId());

        /// <summary>
        /// Add EtherIP ID setting (Sync mode)
        /// </summary>
        public VpnEtherIpId AddEtherIpId() => AddEtherIpIdAsync().Result;

        /// <summary>
        /// Add new virtual interface on virtual L3 switch (Async mode)
        /// </summary>
        public async Task<VpnRpcL3If> AddL3IfAsync() => await Call<VpnRpcL3If>("AddL3If", new VpnRpcL3If());

        /// <summary>
        /// Add new virtual interface on virtual L3 switch (Sync mode)
        /// </summary>
        public VpnRpcL3If AddL3If() => AddL3IfAsync().Result;

        /// <summary>
        /// Add a new virtual layer-3 switch (Async mode)
        /// </summary>
        public async Task<VpnRpcL3Sw> AddL3SwitchAsync() => await Call<VpnRpcL3Sw>("AddL3Switch", new VpnRpcL3Sw());

        /// <summary>
        /// Add a new virtual layer-3 switch (Sync mode)
        /// </summary>
        public VpnRpcL3Sw AddL3Switch() => AddL3SwitchAsync().Result;

        /// <summary>
        /// Add new routing table entry on virtual L3 switch (Async mode)
        /// </summary>
        public async Task<VpnRpcL3Table> AddL3TableAsync() => await Call<VpnRpcL3Table>("AddL3Table", new VpnRpcL3Table());

        /// <summary>
        /// Add new routing table entry on virtual L3 switch (Sync mode)
        /// </summary>
        public VpnRpcL3Table AddL3Table() => AddL3TableAsync().Result;

        /// <summary>
        /// Add new license key (Async mode)
        /// </summary>
        public async Task<VpnRpcTest> AddLicenseKeyAsync() => await Call<VpnRpcTest>("AddLicenseKey", new VpnRpcTest());

        /// <summary>
        /// Add new license key (Sync mode)
        /// </summary>
        public VpnRpcTest AddLicenseKey() => AddLicenseKeyAsync().Result;

        /// <summary>
        /// Add a new local bridge (Async mode)
        /// </summary>
        public async Task<VpnRpcLocalBridge> AddLocalBridgeAsync() => await Call<VpnRpcLocalBridge>("AddLocalBridge", new VpnRpcLocalBridge());

        /// <summary>
        /// Add a new local bridge (Sync mode)
        /// </summary>
        public VpnRpcLocalBridge AddLocalBridge() => AddLocalBridgeAsync().Result;

        /// <summary>
        /// Change host-name for DDNS client (Async mode)
        /// </summary>
        public async Task<VpnRpcTest> ChangeDDnsClientHostnameAsync() => await Call<VpnRpcTest>("ChangeDDnsClientHostname", new VpnRpcTest());

        /// <summary>
        /// Change host-name for DDNS client (Sync mode)
        /// </summary>
        public VpnRpcTest ChangeDDnsClientHostname() => ChangeDDnsClientHostnameAsync().Result;

        /// <summary>
        /// Do Crash (Async mode)
        /// </summary>
        public async Task<VpnRpcTest> CrashAsync() => await Call<VpnRpcTest>("Crash", new VpnRpcTest());

        /// <summary>
        /// Do Crash (Sync mode)
        /// </summary>
        public VpnRpcTest Crash() => CrashAsync().Result;

        /// <summary>
        /// Create a group (Async mode)
        /// </summary>
        public async Task<VpnRpcSetGroup> CreateGroupAsync() => await Call<VpnRpcSetGroup>("CreateGroup", new VpnRpcSetGroup());

        /// <summary>
        /// Create a group (Sync mode)
        /// </summary>
        public VpnRpcSetGroup CreateGroup() => CreateGroupAsync().Result;

        /// <summary>
        /// Create a hub (Async mode)
        /// </summary>
        public async Task<VpnRpcCreateHub> CreateHubAsync() => await Call<VpnRpcCreateHub>("CreateHub", new VpnRpcCreateHub());

        /// <summary>
        /// Create a hub (Sync mode)
        /// </summary>
        public VpnRpcCreateHub CreateHub() => CreateHubAsync().Result;

        /// <summary>
        /// Create a new link(cascade) (Async mode)
        /// </summary>
        public async Task<VpnRpcCreateLink> CreateLinkAsync() => await Call<VpnRpcCreateLink>("CreateLink", new VpnRpcCreateLink());

        /// <summary>
        /// Create a new link(cascade) (Sync mode)
        /// </summary>
        public VpnRpcCreateLink CreateLink() => CreateLinkAsync().Result;

        /// <summary>
        /// Create a listener (Async mode)
        /// </summary>
        public async Task<VpnRpcListener> CreateListenerAsync() => await Call<VpnRpcListener>("CreateListener", new VpnRpcListener());

        /// <summary>
        /// Create a listener (Sync mode)
        /// </summary>
        public VpnRpcListener CreateListener() => CreateListenerAsync().Result;

        /// <summary>
        /// Create a user (Async mode)
        /// </summary>
        public async Task<VpnRpcSetUser> CreateUserAsync() => await Call<VpnRpcSetUser>("CreateUser", new VpnRpcSetUser());

        /// <summary>
        /// Create a user (Sync mode)
        /// </summary>
        public VpnRpcSetUser CreateUser() => CreateUserAsync().Result;

        /// <summary>
        /// Do debug function (Async mode)
        /// </summary>
        public async Task<VpnRpcTest> DebugAsync() => await Call<VpnRpcTest>("Debug", new VpnRpcTest());

        /// <summary>
        /// Do debug function (Sync mode)
        /// </summary>
        public VpnRpcTest Debug() => DebugAsync().Result;

        /// <summary>
        /// Delete CRL (Certificate Revocation List) entry (Async mode)
        /// </summary>
        public async Task<VpnRpcCrl> DelCrlAsync() => await Call<VpnRpcCrl>("DelCrl", new VpnRpcCrl());

        /// <summary>
        /// Delete CRL (Certificate Revocation List) entry (Sync mode)
        /// </summary>
        public VpnRpcCrl DelCrl() => DelCrlAsync().Result;

        /// <summary>
        /// Delete access list entry (Async mode)
        /// </summary>
        public async Task<VpnRpcDeleteAccess> DeleteAccessAsync() => await Call<VpnRpcDeleteAccess>("DeleteAccess", new VpnRpcDeleteAccess());

        /// <summary>
        /// Delete access list entry (Sync mode)
        /// </summary>
        public VpnRpcDeleteAccess DeleteAccess() => DeleteAccessAsync().Result;

        /// <summary>
        /// Delete a CA(Certificate Authority) setting from the hub (Async mode)
        /// </summary>
        public async Task<VpnRpcHubDeleteCA> DeleteCaAsync() => await Call<VpnRpcHubDeleteCA>("DeleteCa", new VpnRpcHubDeleteCA());

        /// <summary>
        /// Delete a CA(Certificate Authority) setting from the hub (Sync mode)
        /// </summary>
        public VpnRpcHubDeleteCA DeleteCa() => DeleteCaAsync().Result;

        /// <summary>
        /// Delete EtherIP ID setting (Async mode)
        /// </summary>
        public async Task<VpnEtherIpId> DeleteEtherIpIdAsync() => await Call<VpnEtherIpId>("DeleteEtherIpId", new VpnEtherIpId());

        /// <summary>
        /// Delete EtherIP ID setting (Sync mode)
        /// </summary>
        public VpnEtherIpId DeleteEtherIpId() => DeleteEtherIpIdAsync().Result;

        /// <summary>
        /// Delete a group (Async mode)
        /// </summary>
        public async Task<VpnRpcDeleteUser> DeleteGroupAsync() => await Call<VpnRpcDeleteUser>("DeleteGroup", new VpnRpcDeleteUser());

        /// <summary>
        /// Delete a group (Sync mode)
        /// </summary>
        public VpnRpcDeleteUser DeleteGroup() => DeleteGroupAsync().Result;

        /// <summary>
        /// Delete a hub (Async mode)
        /// </summary>
        public async Task<VpnRpcDeleteHub> DeleteHubAsync() => await Call<VpnRpcDeleteHub>("DeleteHub", new VpnRpcDeleteHub());

        /// <summary>
        /// Delete a hub (Sync mode)
        /// </summary>
        public VpnRpcDeleteHub DeleteHub() => DeleteHubAsync().Result;

        /// <summary>
        /// Delete IP address table entry (Async mode)
        /// </summary>
        public async Task<VpnRpcDeleteTable> DeleteIpTableAsync() => await Call<VpnRpcDeleteTable>("DeleteIpTable", new VpnRpcDeleteTable());

        /// <summary>
        /// Delete IP address table entry (Sync mode)
        /// </summary>
        public VpnRpcDeleteTable DeleteIpTable() => DeleteIpTableAsync().Result;

        /// <summary>
        /// Delete a link (Async mode)
        /// </summary>
        public async Task<VpnRpcLink> DeleteLinkAsync() => await Call<VpnRpcLink>("DeleteLink", new VpnRpcLink());

        /// <summary>
        /// Delete a link (Sync mode)
        /// </summary>
        public VpnRpcLink DeleteLink() => DeleteLinkAsync().Result;

        /// <summary>
        /// Delete a listener (Async mode)
        /// </summary>
        public async Task<VpnRpcListener> DeleteListenerAsync() => await Call<VpnRpcListener>("DeleteListener", new VpnRpcListener());

        /// <summary>
        /// Delete a listener (Sync mode)
        /// </summary>
        public VpnRpcListener DeleteListener() => DeleteListenerAsync().Result;

        /// <summary>
        /// Delete a local bridge (Async mode)
        /// </summary>
        public async Task<VpnRpcLocalBridge> DeleteLocalBridgeAsync() => await Call<VpnRpcLocalBridge>("DeleteLocalBridge", new VpnRpcLocalBridge());

        /// <summary>
        /// Delete a local bridge (Sync mode)
        /// </summary>
        public VpnRpcLocalBridge DeleteLocalBridge() => DeleteLocalBridgeAsync().Result;

        /// <summary>
        /// Delete MAC address table entry (Async mode)
        /// </summary>
        public async Task<VpnRpcDeleteTable> DeleteMacTableAsync() => await Call<VpnRpcDeleteTable>("DeleteMacTable", new VpnRpcDeleteTable());

        /// <summary>
        /// Delete MAC address table entry (Sync mode)
        /// </summary>
        public VpnRpcDeleteTable DeleteMacTable() => DeleteMacTableAsync().Result;

        /// <summary>
        /// Delete a session (Async mode)
        /// </summary>
        public async Task<VpnRpcDeleteSession> DeleteSessionAsync() => await Call<VpnRpcDeleteSession>("DeleteSession", new VpnRpcDeleteSession());

        /// <summary>
        /// Delete a session (Sync mode)
        /// </summary>
        public VpnRpcDeleteSession DeleteSession() => DeleteSessionAsync().Result;

        /// <summary>
        /// Delete a user (Async mode)
        /// </summary>
        public async Task<VpnRpcDeleteUser> DeleteUserAsync() => await Call<VpnRpcDeleteUser>("DeleteUser", new VpnRpcDeleteUser());

        /// <summary>
        /// Delete a user (Sync mode)
        /// </summary>
        public VpnRpcDeleteUser DeleteUser() => DeleteUserAsync().Result;

        /// <summary>
        /// Delete a virtual interface on virtual L3 switch (Async mode)
        /// </summary>
        public async Task<VpnRpcL3If> DelL3IfAsync() => await Call<VpnRpcL3If>("DelL3If", new VpnRpcL3If());

        /// <summary>
        /// Delete a virtual interface on virtual L3 switch (Sync mode)
        /// </summary>
        public VpnRpcL3If DelL3If() => DelL3IfAsync().Result;

        /// <summary>
        /// Delete a virtual layer-3 switch (Async mode)
        /// </summary>
        public async Task<VpnRpcL3Sw> DelL3SwitchAsync() => await Call<VpnRpcL3Sw>("DelL3Switch", new VpnRpcL3Sw());

        /// <summary>
        /// Delete a virtual layer-3 switch (Sync mode)
        /// </summary>
        public VpnRpcL3Sw DelL3Switch() => DelL3SwitchAsync().Result;

        /// <summary>
        /// Delete routing table entry on virtual L3 switch (Async mode)
        /// </summary>
        public async Task<VpnRpcL3Table> DelL3TableAsync() => await Call<VpnRpcL3Table>("DelL3Table", new VpnRpcL3Table());

        /// <summary>
        /// Delete routing table entry on virtual L3 switch (Sync mode)
        /// </summary>
        public VpnRpcL3Table DelL3Table() => DelL3TableAsync().Result;

        /// <summary>
        /// Delete a license key (Async mode)
        /// </summary>
        public async Task<VpnRpcTest> DelLicenseKeyAsync() => await Call<VpnRpcTest>("DelLicenseKey", new VpnRpcTest());

        /// <summary>
        /// Delete a license key (Sync mode)
        /// </summary>
        public VpnRpcTest DelLicenseKey() => DelLicenseKeyAsync().Result;

        /// <summary>
        /// Disable the SecureNAT function of the hub (Async mode)
        /// </summary>
        public async Task<VpnRpcHub> DisableSecureNATAsync() => await Call<VpnRpcHub>("DisableSecureNAT", new VpnRpcHub());

        /// <summary>
        /// Disable the SecureNAT function of the hub (Sync mode)
        /// </summary>
        public VpnRpcHub DisableSecureNAT() => DisableSecureNATAsync().Result;

        /// <summary>
        /// Disconnect a connection (Async mode)
        /// </summary>
        public async Task<VpnRpcDisconnectConnection> DisconnectConnectionAsync() => await Call<VpnRpcDisconnectConnection>("DisconnectConnection", new VpnRpcDisconnectConnection());

        /// <summary>
        /// Disconnect a connection (Sync mode)
        /// </summary>
        public VpnRpcDisconnectConnection DisconnectConnection() => DisconnectConnectionAsync().Result;

        /// <summary>
        /// Enable / Disable listener (Async mode)
        /// </summary>
        public async Task<VpnRpcListener> EnableListenerAsync() => await Call<VpnRpcListener>("EnableListener", new VpnRpcListener());

        /// <summary>
        /// Enable / Disable listener (Sync mode)
        /// </summary>
        public VpnRpcListener EnableListener() => EnableListenerAsync().Result;

        /// <summary>
        /// Enable SecureNAT function of the hub (Async mode)
        /// </summary>
        public async Task<VpnRpcHub> EnableSecureNATAsync() => await Call<VpnRpcHub>("EnableSecureNAT", new VpnRpcHub());

        /// <summary>
        /// Enable SecureNAT function of the hub (Sync mode)
        /// </summary>
        public VpnRpcHub EnableSecureNAT() => EnableSecureNATAsync().Result;

        /// <summary>
        /// Get access list (Async mode)
        /// </summary>
        public async Task<VpnRpcEnumAccessList> EnumAccessAsync() => await Call<VpnRpcEnumAccessList>("EnumAccess", new VpnRpcEnumAccessList());

        /// <summary>
        /// Get access list (Sync mode)
        /// </summary>
        public VpnRpcEnumAccessList EnumAccess() => EnumAccessAsync().Result;

        /// <summary>
        /// Enumerate CA(Certificate Authority) in the hub (Async mode)
        /// </summary>
        public async Task<VpnRpcHubEnumCA> EnumCaAsync() => await Call<VpnRpcHubEnumCA>("EnumCa", new VpnRpcHubEnumCA());

        /// <summary>
        /// Enumerate CA(Certificate Authority) in the hub (Sync mode)
        /// </summary>
        public VpnRpcHubEnumCA EnumCa() => EnumCaAsync().Result;

        /// <summary>
        /// Enumerate connections (Async mode)
        /// </summary>
        public async Task<VpnRpcEnumConnection> EnumConnectionAsync() => await Call<VpnRpcEnumConnection>("EnumConnection", new VpnRpcEnumConnection());

        /// <summary>
        /// Enumerate connections (Sync mode)
        /// </summary>
        public VpnRpcEnumConnection EnumConnection() => EnumConnectionAsync().Result;

        /// <summary>
        /// Get CRL (Certificate Revocation List) index (Async mode)
        /// </summary>
        public async Task<VpnRpcEnumCrl> EnumCrlAsync() => await Call<VpnRpcEnumCrl>("EnumCrl", new VpnRpcEnumCrl());

        /// <summary>
        /// Get CRL (Certificate Revocation List) index (Sync mode)
        /// </summary>
        public VpnRpcEnumCrl EnumCrl() => EnumCrlAsync().Result;

        /// <summary>
        /// Enumerate DHCP entries (Async mode)
        /// </summary>
        public async Task<VpnRpcEnumDhcp> EnumDHCPAsync() => await Call<VpnRpcEnumDhcp>("EnumDHCP", new VpnRpcEnumDhcp());

        /// <summary>
        /// Enumerate DHCP entries (Sync mode)
        /// </summary>
        public VpnRpcEnumDhcp EnumDHCP() => EnumDHCPAsync().Result;

        /// <summary>
        /// Enumerate EtherIP ID settings (Async mode)
        /// </summary>
        public async Task<VpnRpcEnumEtherIpId> EnumEtherIpIdAsync() => await Call<VpnRpcEnumEtherIpId>("EnumEtherIpId", new VpnRpcEnumEtherIpId());

        /// <summary>
        /// Enumerate EtherIP ID settings (Sync mode)
        /// </summary>
        public VpnRpcEnumEtherIpId EnumEtherIpId() => EnumEtherIpIdAsync().Result;

        /// <summary>
        /// Enumerate Ethernet devices (Async mode)
        /// </summary>
        public async Task<VpnRpcEnumEth> EnumEthernetAsync() => await Call<VpnRpcEnumEth>("EnumEthernet", new VpnRpcEnumEth());

        /// <summary>
        /// Enumerate Ethernet devices (Sync mode)
        /// </summary>
        public VpnRpcEnumEth EnumEthernet() => EnumEthernetAsync().Result;

        /// <summary>
        /// Enumerate VLAN tag transparent setting (Async mode)
        /// </summary>
        public async Task<VpnRpcEnumEthVlan> EnumEthVLanAsync() => await Call<VpnRpcEnumEthVlan>("EnumEthVLan", new VpnRpcEnumEthVlan());

        /// <summary>
        /// Enumerate VLAN tag transparent setting (Sync mode)
        /// </summary>
        public VpnRpcEnumEthVlan EnumEthVLan() => EnumEthVLanAsync().Result;

        /// <summary>
        /// Enumerate cluster members (Async mode)
        /// </summary>
        public async Task<VpnRpcEnumFarm> EnumFarmMemberAsync() => await Call<VpnRpcEnumFarm>("EnumFarmMember", new VpnRpcEnumFarm());

        /// <summary>
        /// Enumerate cluster members (Sync mode)
        /// </summary>
        public VpnRpcEnumFarm EnumFarmMember() => EnumFarmMemberAsync().Result;

        /// <summary>
        /// Enumerate groups (Async mode)
        /// </summary>
        public async Task<VpnRpcEnumGroup> EnumGroupAsync() => await Call<VpnRpcEnumGroup>("EnumGroup", new VpnRpcEnumGroup());

        /// <summary>
        /// Enumerate groups (Sync mode)
        /// </summary>
        public VpnRpcEnumGroup EnumGroup() => EnumGroupAsync().Result;

        /// <summary>
        /// Enumerate hubs (Async mode)
        /// </summary>
        public async Task<VpnRpcEnumHub> EnumHubAsync() => await Call<VpnRpcEnumHub>("EnumHub", new VpnRpcEnumHub());

        /// <summary>
        /// Enumerate hubs (Sync mode)
        /// </summary>
        public VpnRpcEnumHub EnumHub() => EnumHubAsync().Result;

        /// <summary>
        /// Get IP address table (Async mode)
        /// </summary>
        public async Task<VpnRpcEnumIpTable> EnumIpTableAsync() => await Call<VpnRpcEnumIpTable>("EnumIpTable", new VpnRpcEnumIpTable());

        /// <summary>
        /// Get IP address table (Sync mode)
        /// </summary>
        public VpnRpcEnumIpTable EnumIpTable() => EnumIpTableAsync().Result;

        /// <summary>
        /// Enumerate virtual interfaces on virtual L3 switch (Async mode)
        /// </summary>
        public async Task<VpnRpcEnumL3If> EnumL3IfAsync() => await Call<VpnRpcEnumL3If>("EnumL3If", new VpnRpcEnumL3If());

        /// <summary>
        /// Enumerate virtual interfaces on virtual L3 switch (Sync mode)
        /// </summary>
        public VpnRpcEnumL3If EnumL3If() => EnumL3IfAsync().Result;

        /// <summary>
        /// Enumerate virtual layer-3 switches (Async mode)
        /// </summary>
        public async Task<VpnRpcEnumL3Sw> EnumL3SwitchAsync() => await Call<VpnRpcEnumL3Sw>("EnumL3Switch", new VpnRpcEnumL3Sw());

        /// <summary>
        /// Enumerate virtual layer-3 switches (Sync mode)
        /// </summary>
        public VpnRpcEnumL3Sw EnumL3Switch() => EnumL3SwitchAsync().Result;

        /// <summary>
        /// Get routing table on virtual L3 switch (Async mode)
        /// </summary>
        public async Task<VpnRpcEnumL3Table> EnumL3TableAsync() => await Call<VpnRpcEnumL3Table>("EnumL3Table", new VpnRpcEnumL3Table());

        /// <summary>
        /// Get routing table on virtual L3 switch (Sync mode)
        /// </summary>
        public VpnRpcEnumL3Table EnumL3Table() => EnumL3TableAsync().Result;

        /// <summary>
        /// Enumerate license key (Async mode)
        /// </summary>
        public async Task<VpnRpcEnumLicenseKey> EnumLicenseKeyAsync() => await Call<VpnRpcEnumLicenseKey>("EnumLicenseKey", new VpnRpcEnumLicenseKey());

        /// <summary>
        /// Enumerate license key (Sync mode)
        /// </summary>
        public VpnRpcEnumLicenseKey EnumLicenseKey() => EnumLicenseKeyAsync().Result;

        /// <summary>
        /// Enumerate links (Async mode)
        /// </summary>
        public async Task<VpnRpcEnumLink> EnumLinkAsync() => await Call<VpnRpcEnumLink>("EnumLink", new VpnRpcEnumLink());

        /// <summary>
        /// Enumerate links (Sync mode)
        /// </summary>
        public VpnRpcEnumLink EnumLink() => EnumLinkAsync().Result;

        /// <summary>
        /// Enumerating listeners (Async mode)
        /// </summary>
        public async Task<VpnRpcListenerList> EnumListenerAsync() => await Call<VpnRpcListenerList>("EnumListener", new VpnRpcListenerList());

        /// <summary>
        /// Enumerating listeners (Sync mode)
        /// </summary>
        public VpnRpcListenerList EnumListener() => EnumListenerAsync().Result;

        /// <summary>
        /// Enumerate local bridges (Async mode)
        /// </summary>
        public async Task<VpnRpcEnumLocalBridge> EnumLocalBridgeAsync() => await Call<VpnRpcEnumLocalBridge>("EnumLocalBridge", new VpnRpcEnumLocalBridge());

        /// <summary>
        /// Enumerate local bridges (Sync mode)
        /// </summary>
        public VpnRpcEnumLocalBridge EnumLocalBridge() => EnumLocalBridgeAsync().Result;

        /// <summary>
        /// Enumerate log files (Async mode)
        /// </summary>
        public async Task<VpnRpcEnumLogFile> EnumLogFileAsync() => await Call<VpnRpcEnumLogFile>("EnumLogFile", new VpnRpcEnumLogFile());

        /// <summary>
        /// Enumerate log files (Sync mode)
        /// </summary>
        public VpnRpcEnumLogFile EnumLogFile() => EnumLogFileAsync().Result;

        /// <summary>
        /// Get MAC address table (Async mode)
        /// </summary>
        public async Task<VpnRpcEnumMacTable> EnumMacTableAsync() => await Call<VpnRpcEnumMacTable>("EnumMacTable", new VpnRpcEnumMacTable());

        /// <summary>
        /// Get MAC address table (Sync mode)
        /// </summary>
        public VpnRpcEnumMacTable EnumMacTable() => EnumMacTableAsync().Result;

        /// <summary>
        /// Enumerate NAT entries of the SecureNAT (Async mode)
        /// </summary>
        public async Task<VpnRpcEnumNat> EnumNATAsync() => await Call<VpnRpcEnumNat>("EnumNAT", new VpnRpcEnumNat());

        /// <summary>
        /// Enumerate NAT entries of the SecureNAT (Sync mode)
        /// </summary>
        public VpnRpcEnumNat EnumNAT() => EnumNATAsync().Result;

        /// <summary>
        /// Enumerate sessions (Async mode)
        /// </summary>
        public async Task<VpnRpcEnumSession> EnumSessionAsync() => await Call<VpnRpcEnumSession>("EnumSession", new VpnRpcEnumSession());

        /// <summary>
        /// Enumerate sessions (Sync mode)
        /// </summary>
        public VpnRpcEnumSession EnumSession() => EnumSessionAsync().Result;

        /// <summary>
        /// Enumerate users (Async mode)
        /// </summary>
        public async Task<VpnRpcEnumUser> EnumUserAsync() => await Call<VpnRpcEnumUser>("EnumUser", new VpnRpcEnumUser());

        /// <summary>
        /// Enumerate users (Sync mode)
        /// </summary>
        public VpnRpcEnumUser EnumUser() => EnumUserAsync().Result;

        /// <summary>
        /// Flush configuration file (Async mode)
        /// </summary>
        public async Task<VpnRpcTest> FlushAsync() => await Call<VpnRpcTest>("Flush", new VpnRpcTest());

        /// <summary>
        /// Flush configuration file (Sync mode)
        /// </summary>
        public VpnRpcTest Flush() => FlushAsync().Result;

        /// <summary>
        /// Get access control list (Async mode)
        /// </summary>
        public async Task<VpnRpcAcList> GetAcListAsync() => await Call<VpnRpcAcList>("GetAcList", new VpnRpcAcList());

        /// <summary>
        /// Get access control list (Sync mode)
        /// </summary>
        public VpnRpcAcList GetAcList() => GetAcListAsync().Result;

        /// <summary>
        /// Get message for administrators (Async mode)
        /// </summary>
        public async Task<VpnRpcMsg> GetAdminMsgAsync() => await Call<VpnRpcMsg>("GetAdminMsg", new VpnRpcMsg());

        /// <summary>
        /// Get message for administrators (Sync mode)
        /// </summary>
        public VpnRpcMsg GetAdminMsg() => GetAdminMsgAsync().Result;

        /// <summary>
        /// Get Azure status (Async mode)
        /// </summary>
        public async Task<VpnRpcAzureStatus> GetAzureStatusAsync() => await Call<VpnRpcAzureStatus>("GetAzureStatus", new VpnRpcAzureStatus());

        /// <summary>
        /// Get Azure status (Sync mode)
        /// </summary>
        public VpnRpcAzureStatus GetAzureStatus() => GetAzureStatusAsync().Result;

        /// <summary>
        /// Get availability to localbridge function (Async mode)
        /// </summary>
        public async Task<VpnRpcBridgeSupport> GetBridgeSupportAsync() => await Call<VpnRpcBridgeSupport>("GetBridgeSupport", new VpnRpcBridgeSupport());

        /// <summary>
        /// Get availability to localbridge function (Sync mode)
        /// </summary>
        public VpnRpcBridgeSupport GetBridgeSupport() => GetBridgeSupportAsync().Result;

        /// <summary>
        /// Get CA(Certificate Authority) setting from the hub (Async mode)
        /// </summary>
        public async Task<VpnRpcHubGetCA> GetCaAsync() => await Call<VpnRpcHubGetCA>("GetCa", new VpnRpcHubGetCA());

        /// <summary>
        /// Get CA(Certificate Authority) setting from the hub (Sync mode)
        /// </summary>
        public VpnRpcHubGetCA GetCa() => GetCaAsync().Result;

        /// <summary>
        /// Get capabilities (Async mode)
        /// </summary>
        public async Task<VpnCapslist> GetCapsAsync() => await Call<VpnCapslist>("GetCaps", new VpnCapslist());

        /// <summary>
        /// Get capabilities (Sync mode)
        /// </summary>
        public VpnCapslist GetCaps() => GetCapsAsync().Result;

        /// <summary>
        /// Get configuration file stream (Async mode)
        /// </summary>
        public async Task<VpnRpcConfig> GetConfigAsync() => await Call<VpnRpcConfig>("GetConfig", new VpnRpcConfig());

        /// <summary>
        /// Get configuration file stream (Sync mode)
        /// </summary>
        public VpnRpcConfig GetConfig() => GetConfigAsync().Result;

        /// <summary>
        /// Get connection information (Async mode)
        /// </summary>
        public async Task<VpnRpcConnectionInfo> GetConnectionInfoAsync() => await Call<VpnRpcConnectionInfo>("GetConnectionInfo", new VpnRpcConnectionInfo());

        /// <summary>
        /// Get connection information (Sync mode)
        /// </summary>
        public VpnRpcConnectionInfo GetConnectionInfo() => GetConnectionInfoAsync().Result;

        /// <summary>
        /// Get CRL (Certificate Revocation List) entry (Async mode)
        /// </summary>
        public async Task<VpnRpcCrl> GetCrlAsync() => await Call<VpnRpcCrl>("GetCrl", new VpnRpcCrl());

        /// <summary>
        /// Get CRL (Certificate Revocation List) entry (Sync mode)
        /// </summary>
        public VpnRpcCrl GetCrl() => GetCrlAsync().Result;

        /// <summary>
        /// Get status of DDNS client (Async mode)
        /// </summary>
        public async Task<VpnDDnsClientStatus> GetDDnsClientStatusAsync() => await Call<VpnDDnsClientStatus>("GetDDnsClientStatus", new VpnDDnsClientStatus());

        /// <summary>
        /// Get status of DDNS client (Sync mode)
        /// </summary>
        public VpnDDnsClientStatus GetDDnsClientStatus() => GetDDnsClientStatusAsync().Result;

        /// <summary>
        /// Get DDNS proxy configuration (Async mode)
        /// </summary>
        public async Task<VpnInternetSetting> GetDDnsInternetSettngAsync() => await Call<VpnInternetSetting>("GetDDnsInternetSettng", new VpnInternetSetting());

        /// <summary>
        /// Get DDNS proxy configuration (Sync mode)
        /// </summary>
        public VpnInternetSetting GetDDnsInternetSettng() => GetDDnsInternetSettngAsync().Result;

        /// <summary>
        /// Get default hub administration options (Async mode)
        /// </summary>
        public async Task<VpnRpcAdminOption> GetDefaultHubAdminOptionsAsync() => await Call<VpnRpcAdminOption>("GetDefaultHubAdminOptions", new VpnRpcAdminOption());

        /// <summary>
        /// Get default hub administration options (Sync mode)
        /// </summary>
        public VpnRpcAdminOption GetDefaultHubAdminOptions() => GetDefaultHubAdminOptionsAsync().Result;

        /// <summary>
        /// Get EtherIP ID setting (Async mode)
        /// </summary>
        public async Task<VpnEtherIpId> GetEtherIpIdAsync() => await Call<VpnEtherIpId>("GetEtherIpId", new VpnEtherIpId());

        /// <summary>
        /// Get EtherIP ID setting (Sync mode)
        /// </summary>
        public VpnEtherIpId GetEtherIpId() => GetEtherIpIdAsync().Result;

        /// <summary>
        /// Get status of connection to cluster controller (Async mode)
        /// </summary>
        public async Task<VpnRpcFarmConnectionStatus> GetFarmConnectionStatusAsync() => await Call<VpnRpcFarmConnectionStatus>("GetFarmConnectionStatus", new VpnRpcFarmConnectionStatus());

        /// <summary>
        /// Get status of connection to cluster controller (Sync mode)
        /// </summary>
        public VpnRpcFarmConnectionStatus GetFarmConnectionStatus() => GetFarmConnectionStatusAsync().Result;

        /// <summary>
        /// Get cluster member information (Async mode)
        /// </summary>
        public async Task<VpnRpcFarmInfo> GetFarmInfoAsync() => await Call<VpnRpcFarmInfo>("GetFarmInfo", new VpnRpcFarmInfo());

        /// <summary>
        /// Get cluster member information (Sync mode)
        /// </summary>
        public VpnRpcFarmInfo GetFarmInfo() => GetFarmInfoAsync().Result;

        /// <summary>
        /// Get clustering configuration (Async mode)
        /// </summary>
        public async Task<VpnRpcFarm> GetFarmSettingAsync() => await Call<VpnRpcFarm>("GetFarmSetting", new VpnRpcFarm());

        /// <summary>
        /// Get clustering configuration (Sync mode)
        /// </summary>
        public VpnRpcFarm GetFarmSetting() => GetFarmSettingAsync().Result;

        /// <summary>
        /// Get group information (Async mode)
        /// </summary>
        public async Task<VpnRpcSetGroup> GetGroupAsync() => await Call<VpnRpcSetGroup>("GetGroup", new VpnRpcSetGroup());

        /// <summary>
        /// Get group information (Sync mode)
        /// </summary>
        public VpnRpcSetGroup GetGroup() => GetGroupAsync().Result;

        /// <summary>
        /// Get hub configuration (Async mode)
        /// </summary>
        public async Task<VpnRpcCreateHub> GetHubAsync() => await Call<VpnRpcCreateHub>("GetHub", new VpnRpcCreateHub());

        /// <summary>
        /// Get hub configuration (Sync mode)
        /// </summary>
        public VpnRpcCreateHub GetHub() => GetHubAsync().Result;

        /// <summary>
        /// Get hub administration options (Async mode)
        /// </summary>
        public async Task<VpnRpcAdminOption> GetHubAdminOptionsAsync() => await Call<VpnRpcAdminOption>("GetHubAdminOptions", new VpnRpcAdminOption());

        /// <summary>
        /// Get hub administration options (Sync mode)
        /// </summary>
        public VpnRpcAdminOption GetHubAdminOptions() => GetHubAdminOptionsAsync().Result;

        /// <summary>
        /// Get hub extended options (Async mode)
        /// </summary>
        public async Task<VpnRpcAdminOption> GetHubExtOptionsAsync() => await Call<VpnRpcAdminOption>("GetHubExtOptions", new VpnRpcAdminOption());

        /// <summary>
        /// Get hub extended options (Sync mode)
        /// </summary>
        public VpnRpcAdminOption GetHubExtOptions() => GetHubExtOptionsAsync().Result;

        /// <summary>
        /// Get logging configuration of the hub (Async mode)
        /// </summary>
        public async Task<VpnRpcHubLog> GetHubLogAsync() => await Call<VpnRpcHubLog>("GetHubLog", new VpnRpcHubLog());

        /// <summary>
        /// Get logging configuration of the hub (Sync mode)
        /// </summary>
        public VpnRpcHubLog GetHubLog() => GetHubLogAsync().Result;

        /// <summary>
        /// Get message of today on hub (Async mode)
        /// </summary>
        public async Task<VpnRpcMsg> GetHubMsgAsync() => await Call<VpnRpcMsg>("GetHubMsg", new VpnRpcMsg());

        /// <summary>
        /// Get message of today on hub (Sync mode)
        /// </summary>
        public VpnRpcMsg GetHubMsg() => GetHubMsgAsync().Result;

        /// <summary>
        /// Get Radius options of the hub (Async mode)
        /// </summary>
        public async Task<VpnRpcRadius> GetHubRadiusAsync() => await Call<VpnRpcRadius>("GetHubRadius", new VpnRpcRadius());

        /// <summary>
        /// Get Radius options of the hub (Sync mode)
        /// </summary>
        public VpnRpcRadius GetHubRadius() => GetHubRadiusAsync().Result;

        /// <summary>
        /// Get hub status (Async mode)
        /// </summary>
        public async Task<VpnRpcHubStatus> GetHubStatusAsync() => await Call<VpnRpcHubStatus>("GetHubStatus", new VpnRpcHubStatus());

        /// <summary>
        /// Get hub status (Sync mode)
        /// </summary>
        public VpnRpcHubStatus GetHubStatus() => GetHubStatusAsync().Result;

        /// <summary>
        /// Get IPsec service configuration (Async mode)
        /// </summary>
        public async Task<VpnIPsecServices> GetIPsecServicesAsync() => await Call<VpnIPsecServices>("GetIPsecServices", new VpnIPsecServices());

        /// <summary>
        /// Get IPsec service configuration (Sync mode)
        /// </summary>
        public VpnIPsecServices GetIPsecServices() => GetIPsecServicesAsync().Result;

        /// <summary>
        /// Get keep-alive function setting (Async mode)
        /// </summary>
        public async Task<VpnRpcKeep> GetKeepAsync() => await Call<VpnRpcKeep>("GetKeep", new VpnRpcKeep());

        /// <summary>
        /// Get keep-alive function setting (Sync mode)
        /// </summary>
        public VpnRpcKeep GetKeep() => GetKeepAsync().Result;

        /// <summary>
        /// Get license status (Async mode)
        /// </summary>
        public async Task<VpnRpcLicenseStatus> GetLicenseStatusAsync() => await Call<VpnRpcLicenseStatus>("GetLicenseStatus", new VpnRpcLicenseStatus());

        /// <summary>
        /// Get license status (Sync mode)
        /// </summary>
        public VpnRpcLicenseStatus GetLicenseStatus() => GetLicenseStatusAsync().Result;

        /// <summary>
        /// Get link configuration (Async mode)
        /// </summary>
        public async Task<VpnRpcCreateLink> GetLinkAsync() => await Call<VpnRpcCreateLink>("GetLink", new VpnRpcCreateLink());

        /// <summary>
        /// Get link configuration (Sync mode)
        /// </summary>
        public VpnRpcCreateLink GetLink() => GetLinkAsync().Result;

        /// <summary>
        /// Get link status (Async mode)
        /// </summary>
        public async Task<VpnRpcLinkStatus> GetLinkStatusAsync() => await Call<VpnRpcLinkStatus>("GetLinkStatus", new VpnRpcLinkStatus());

        /// <summary>
        /// Get link status (Sync mode)
        /// </summary>
        public VpnRpcLinkStatus GetLinkStatus() => GetLinkStatusAsync().Result;

        /// <summary>
        /// Get configurations for OpenVPN and SSTP (Async mode)
        /// </summary>
        public async Task<VpnOpenVpnSstpConfig> GetOpenVpnSstpConfigAsync() => await Call<VpnOpenVpnSstpConfig>("GetOpenVpnSstpConfig", new VpnOpenVpnSstpConfig());

        /// <summary>
        /// Get configurations for OpenVPN and SSTP (Sync mode)
        /// </summary>
        public VpnOpenVpnSstpConfig GetOpenVpnSstpConfig() => GetOpenVpnSstpConfigAsync().Result;

        /// <summary>
        /// Get SecureNAT options (Async mode)
        /// </summary>
        public async Task<VpnVhOption> GetSecureNATOptionAsync() => await Call<VpnVhOption>("GetSecureNATOption", new VpnVhOption());

        /// <summary>
        /// Get SecureNAT options (Sync mode)
        /// </summary>
        public VpnVhOption GetSecureNATOption() => GetSecureNATOptionAsync().Result;

        /// <summary>
        /// Get status of the SecureNAT (Async mode)
        /// </summary>
        public async Task<VpnRpcNatStatus> GetSecureNATStatusAsync() => await Call<VpnRpcNatStatus>("GetSecureNATStatus", new VpnRpcNatStatus());

        /// <summary>
        /// Get status of the SecureNAT (Sync mode)
        /// </summary>
        public VpnRpcNatStatus GetSecureNATStatus() => GetSecureNATStatusAsync().Result;

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
        /// Get server information (Async mode)
        /// </summary>
        public async Task<VpnRpcServerInfo> GetServerInfoAsync() => await Call<VpnRpcServerInfo>("GetServerInfo", new VpnRpcServerInfo());

        /// <summary>
        /// Get server information (Sync mode)
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
        /// Get session status (Async mode)
        /// </summary>
        public async Task<VpnRpcSessionStatus> GetSessionStatusAsync() => await Call<VpnRpcSessionStatus>("GetSessionStatus", new VpnRpcSessionStatus());

        /// <summary>
        /// Get session status (Sync mode)
        /// </summary>
        public VpnRpcSessionStatus GetSessionStatus() => GetSessionStatusAsync().Result;

        /// <summary>
        /// Get special listener status (Async mode)
        /// </summary>
        public async Task<VpnRpcSpecialListener> GetSpecialListenerAsync() => await Call<VpnRpcSpecialListener>("GetSpecialListener", new VpnRpcSpecialListener());

        /// <summary>
        /// Get special listener status (Sync mode)
        /// </summary>
        public VpnRpcSpecialListener GetSpecialListener() => GetSpecialListenerAsync().Result;

        /// <summary>
        /// Get syslog function setting (Async mode)
        /// </summary>
        public async Task<VpnSyslogSetting> GetSysLogAsync() => await Call<VpnSyslogSetting>("GetSysLog", new VpnSyslogSetting());

        /// <summary>
        /// Get syslog function setting (Sync mode)
        /// </summary>
        public VpnSyslogSetting GetSysLog() => GetSysLogAsync().Result;

        /// <summary>
        /// Get user setting (Async mode)
        /// </summary>
        public async Task<VpnRpcSetUser> GetUserAsync() => await Call<VpnRpcSetUser>("GetUser", new VpnRpcSetUser());

        /// <summary>
        /// Get user setting (Sync mode)
        /// </summary>
        public VpnRpcSetUser GetUser() => GetUserAsync().Result;

        /// <summary>
        /// Get VPN Gate configuration (Async mode)
        /// </summary>
        public async Task<VpnVgsConfig> GetVgsConfigAsync() => await Call<VpnVgsConfig>("GetVgsConfig", new VpnVgsConfig());

        /// <summary>
        /// Get VPN Gate configuration (Sync mode)
        /// </summary>
        public VpnVgsConfig GetVgsConfig() => GetVgsConfigAsync().Result;

        /// <summary>
        /// Generate OpenVPN configuration files (Async mode)
        /// </summary>
        public async Task<VpnRpcReadLogFile> MakeOpenVpnConfigFileAsync() => await Call<VpnRpcReadLogFile>("MakeOpenVpnConfigFile", new VpnRpcReadLogFile());

        /// <summary>
        /// Generate OpenVPN configuration files (Sync mode)
        /// </summary>
        public VpnRpcReadLogFile MakeOpenVpnConfigFile() => MakeOpenVpnConfigFileAsync().Result;

        /// <summary>
        /// Read a log file (Async mode)
        /// </summary>
        public async Task<VpnRpcReadLogFile> ReadLogFileAsync() => await Call<VpnRpcReadLogFile>("ReadLogFile", new VpnRpcReadLogFile());

        /// <summary>
        /// Read a log file (Sync mode)
        /// </summary>
        public VpnRpcReadLogFile ReadLogFile() => ReadLogFileAsync().Result;

        /// <summary>
        /// Reboot server itself (Async mode)
        /// </summary>
        public async Task<VpnRpcTest> RebootServerAsync() => await Call<VpnRpcTest>("RebootServer", new VpnRpcTest());

        /// <summary>
        /// Reboot server itself (Sync mode)
        /// </summary>
        public VpnRpcTest RebootServer() => RebootServerAsync().Result;

        /// <summary>
        /// Regenerate server certification (Async mode)
        /// </summary>
        public async Task<VpnRpcTest> RegenerateServerCertAsync() => await Call<VpnRpcTest>("RegenerateServerCert", new VpnRpcTest());

        /// <summary>
        /// Regenerate server certification (Sync mode)
        /// </summary>
        public VpnRpcTest RegenerateServerCert() => RegenerateServerCertAsync().Result;

        /// <summary>
        /// Rename link (cascade connection) (Async mode)
        /// </summary>
        public async Task<VpnRpcRenameLink> RenameLinkAsync() => await Call<VpnRpcRenameLink>("RenameLink", new VpnRpcRenameLink());

        /// <summary>
        /// Rename link (cascade connection) (Sync mode)
        /// </summary>
        public VpnRpcRenameLink RenameLink() => RenameLinkAsync().Result;

        /// <summary>
        /// Set access list (Async mode)
        /// </summary>
        public async Task<VpnRpcEnumAccessList> SetAccessListAsync() => await Call<VpnRpcEnumAccessList>("SetAccessList", new VpnRpcEnumAccessList());

        /// <summary>
        /// Set access list (Sync mode)
        /// </summary>
        public VpnRpcEnumAccessList SetAccessList() => SetAccessListAsync().Result;

        /// <summary>
        /// Set access control list (Async mode)
        /// </summary>
        public async Task<VpnRpcAcList> SetAcListAsync() => await Call<VpnRpcAcList>("SetAcList", new VpnRpcAcList());

        /// <summary>
        /// Set access control list (Sync mode)
        /// </summary>
        public VpnRpcAcList SetAcList() => SetAcListAsync().Result;

        /// <summary>
        /// Set Azure status (Async mode)
        /// </summary>
        public async Task<VpnRpcAzureStatus> SetAzureStatusAsync() => await Call<VpnRpcAzureStatus>("SetAzureStatus", new VpnRpcAzureStatus());

        /// <summary>
        /// Set Azure status (Sync mode)
        /// </summary>
        public VpnRpcAzureStatus SetAzureStatus() => SetAzureStatusAsync().Result;

        /// <summary>
        /// Overwrite configuration file by specified data (Async mode)
        /// </summary>
        public async Task<VpnRpcConfig> SetConfigAsync() => await Call<VpnRpcConfig>("SetConfig", new VpnRpcConfig());

        /// <summary>
        /// Overwrite configuration file by specified data (Sync mode)
        /// </summary>
        public VpnRpcConfig SetConfig() => SetConfigAsync().Result;

        /// <summary>
        /// Set CRL (Certificate Revocation List) entry (Async mode)
        /// </summary>
        public async Task<VpnRpcCrl> SetCrlAsync() => await Call<VpnRpcCrl>("SetCrl", new VpnRpcCrl());

        /// <summary>
        /// Set CRL (Certificate Revocation List) entry (Sync mode)
        /// </summary>
        public VpnRpcCrl SetCrl() => SetCrlAsync().Result;

        /// <summary>
        /// Set DDNS proxy configuration (Async mode)
        /// </summary>
        public async Task<VpnInternetSetting> SetDDnsInternetSettngAsync() => await Call<VpnInternetSetting>("SetDDnsInternetSettng", new VpnInternetSetting());

        /// <summary>
        /// Set DDNS proxy configuration (Sync mode)
        /// </summary>
        public VpnInternetSetting SetDDnsInternetSettng() => SetDDnsInternetSettngAsync().Result;

        /// <summary>
        /// Set VLAN tag transparent setting (Async mode)
        /// </summary>
        public async Task<VpnRpcTest> SetEnableEthVLanAsync() => await Call<VpnRpcTest>("SetEnableEthVLan", new VpnRpcTest());

        /// <summary>
        /// Set VLAN tag transparent setting (Sync mode)
        /// </summary>
        public VpnRpcTest SetEnableEthVLan() => SetEnableEthVLanAsync().Result;

        /// <summary>
        /// Set clustering configuration (Async mode)
        /// </summary>
        public async Task<VpnRpcFarm> SetFarmSettingAsync() => await Call<VpnRpcFarm>("SetFarmSetting", new VpnRpcFarm());

        /// <summary>
        /// Set clustering configuration (Sync mode)
        /// </summary>
        public VpnRpcFarm SetFarmSetting() => SetFarmSettingAsync().Result;

        /// <summary>
        /// Set group setting (Async mode)
        /// </summary>
        public async Task<VpnRpcSetGroup> SetGroupAsync() => await Call<VpnRpcSetGroup>("SetGroup", new VpnRpcSetGroup());

        /// <summary>
        /// Set group setting (Sync mode)
        /// </summary>
        public VpnRpcSetGroup SetGroup() => SetGroupAsync().Result;

        /// <summary>
        /// Set hub configuration (Async mode)
        /// </summary>
        public async Task<VpnRpcCreateHub> SetHubAsync() => await Call<VpnRpcCreateHub>("SetHub", new VpnRpcCreateHub());

        /// <summary>
        /// Set hub configuration (Sync mode)
        /// </summary>
        public VpnRpcCreateHub SetHub() => SetHubAsync().Result;

        /// <summary>
        /// Set hub administration options (Async mode)
        /// </summary>
        public async Task<VpnRpcAdminOption> SetHubAdminOptionsAsync() => await Call<VpnRpcAdminOption>("SetHubAdminOptions", new VpnRpcAdminOption());

        /// <summary>
        /// Set hub administration options (Sync mode)
        /// </summary>
        public VpnRpcAdminOption SetHubAdminOptions() => SetHubAdminOptionsAsync().Result;

        /// <summary>
        /// Set hub extended options (Async mode)
        /// </summary>
        public async Task<VpnRpcAdminOption> SetHubExtOptionsAsync() => await Call<VpnRpcAdminOption>("SetHubExtOptions", new VpnRpcAdminOption());

        /// <summary>
        /// Set hub extended options (Sync mode)
        /// </summary>
        public VpnRpcAdminOption SetHubExtOptions() => SetHubExtOptionsAsync().Result;

        /// <summary>
        /// Set logging configuration into the hub (Async mode)
        /// </summary>
        public async Task<VpnRpcHubLog> SetHubLogAsync() => await Call<VpnRpcHubLog>("SetHubLog", new VpnRpcHubLog());

        /// <summary>
        /// Set logging configuration into the hub (Sync mode)
        /// </summary>
        public VpnRpcHubLog SetHubLog() => SetHubLogAsync().Result;

        /// <summary>
        /// Set message of today on hub (Async mode)
        /// </summary>
        public async Task<VpnRpcMsg> SetHubMsgAsync() => await Call<VpnRpcMsg>("SetHubMsg", new VpnRpcMsg());

        /// <summary>
        /// Set message of today on hub (Sync mode)
        /// </summary>
        public VpnRpcMsg SetHubMsg() => SetHubMsgAsync().Result;

        /// <summary>
        /// Make a hub on-line or off-line (Async mode)
        /// </summary>
        public async Task<VpnRpcSetHubOnline> SetHubOnlineAsync() => await Call<VpnRpcSetHubOnline>("SetHubOnline", new VpnRpcSetHubOnline());

        /// <summary>
        /// Make a hub on-line or off-line (Sync mode)
        /// </summary>
        public VpnRpcSetHubOnline SetHubOnline() => SetHubOnlineAsync().Result;

        /// <summary>
        /// Set Radius options of the hub (Async mode)
        /// </summary>
        public async Task<VpnRpcRadius> SetHubRadiusAsync() => await Call<VpnRpcRadius>("SetHubRadius", new VpnRpcRadius());

        /// <summary>
        /// Set Radius options of the hub (Sync mode)
        /// </summary>
        public VpnRpcRadius SetHubRadius() => SetHubRadiusAsync().Result;

        /// <summary>
        /// Set IPsec service configuration (Async mode)
        /// </summary>
        public async Task<VpnIPsecServices> SetIPsecServicesAsync() => await Call<VpnIPsecServices>("SetIPsecServices", new VpnIPsecServices());

        /// <summary>
        /// Set IPsec service configuration (Sync mode)
        /// </summary>
        public VpnIPsecServices SetIPsecServices() => SetIPsecServicesAsync().Result;

        /// <summary>
        /// Set keep-alive function setting (Async mode)
        /// </summary>
        public async Task<VpnRpcKeep> SetKeepAsync() => await Call<VpnRpcKeep>("SetKeep", new VpnRpcKeep());

        /// <summary>
        /// Set keep-alive function setting (Sync mode)
        /// </summary>
        public VpnRpcKeep SetKeep() => SetKeepAsync().Result;

        /// <summary>
        /// Set link configuration (Async mode)
        /// </summary>
        public async Task<VpnRpcCreateLink> SetLinkAsync() => await Call<VpnRpcCreateLink>("SetLink", new VpnRpcCreateLink());

        /// <summary>
        /// Set link configuration (Sync mode)
        /// </summary>
        public VpnRpcCreateLink SetLink() => SetLinkAsync().Result;

        /// <summary>
        /// Make a link into off-line (Async mode)
        /// </summary>
        public async Task<VpnRpcLink> SetLinkOfflineAsync() => await Call<VpnRpcLink>("SetLinkOffline", new VpnRpcLink());

        /// <summary>
        /// Make a link into off-line (Sync mode)
        /// </summary>
        public VpnRpcLink SetLinkOffline() => SetLinkOfflineAsync().Result;

        /// <summary>
        /// Make a link into on-line (Async mode)
        /// </summary>
        public async Task<VpnRpcLink> SetLinkOnlineAsync() => await Call<VpnRpcLink>("SetLinkOnline", new VpnRpcLink());

        /// <summary>
        /// Make a link into on-line (Sync mode)
        /// </summary>
        public VpnRpcLink SetLinkOnline() => SetLinkOnlineAsync().Result;

        /// <summary>
        /// Set configurations for OpenVPN and SSTP (Async mode)
        /// </summary>
        public async Task<VpnOpenVpnSstpConfig> SetOpenVpnSstpConfigAsync() => await Call<VpnOpenVpnSstpConfig>("SetOpenVpnSstpConfig", new VpnOpenVpnSstpConfig());

        /// <summary>
        /// Set configurations for OpenVPN and SSTP (Sync mode)
        /// </summary>
        public VpnOpenVpnSstpConfig SetOpenVpnSstpConfig() => SetOpenVpnSstpConfigAsync().Result;

        /// <summary>
        /// Set SecureNAT options (Async mode)
        /// </summary>
        public async Task<VpnVhOption> SetSecureNATOptionAsync() => await Call<VpnVhOption>("SetSecureNATOption", new VpnVhOption());

        /// <summary>
        /// Set SecureNAT options (Sync mode)
        /// </summary>
        public VpnVhOption SetSecureNATOption() => SetSecureNATOptionAsync().Result;

        /// <summary>
        /// Set the server certification (Async mode)
        /// </summary>
        public async Task<VpnRpcKeyPair> SetServerCertAsync() => await Call<VpnRpcKeyPair>("SetServerCert", new VpnRpcKeyPair());

        /// <summary>
        /// Set the server certification (Sync mode)
        /// </summary>
        public VpnRpcKeyPair SetServerCert() => SetServerCertAsync().Result;

        /// <summary>
        /// Set cipher for SSL to the server (Async mode)
        /// </summary>
        public async Task<VpnRpcStr> SetServerCipherAsync() => await Call<VpnRpcStr>("SetServerCipher", new VpnRpcStr());

        /// <summary>
        /// Set cipher for SSL to the server (Sync mode)
        /// </summary>
        public VpnRpcStr SetServerCipher() => SetServerCipherAsync().Result;

        /// <summary>
        /// Set server password (Async mode)
        /// </summary>
        public async Task<VpnRpcSetPassword> SetServerPasswordAsync() => await Call<VpnRpcSetPassword>("SetServerPassword", new VpnRpcSetPassword());

        /// <summary>
        /// Set server password (Sync mode)
        /// </summary>
        public VpnRpcSetPassword SetServerPassword() => SetServerPasswordAsync().Result;

        /// <summary>
        /// Set special listener status (Async mode)
        /// </summary>
        public async Task<VpnRpcSpecialListener> SetSpecialListenerAsync() => await Call<VpnRpcSpecialListener>("SetSpecialListener", new VpnRpcSpecialListener());

        /// <summary>
        /// Set special listener status (Sync mode)
        /// </summary>
        public VpnRpcSpecialListener SetSpecialListener() => SetSpecialListenerAsync().Result;

        /// <summary>
        /// Set syslog function setting (Async mode)
        /// </summary>
        public async Task<VpnSyslogSetting> SetSysLogAsync() => await Call<VpnSyslogSetting>("SetSysLog", new VpnSyslogSetting());

        /// <summary>
        /// Set syslog function setting (Sync mode)
        /// </summary>
        public VpnSyslogSetting SetSysLog() => SetSysLogAsync().Result;

        /// <summary>
        /// Set user setting (Async mode)
        /// </summary>
        public async Task<VpnRpcSetUser> SetUserAsync() => await Call<VpnRpcSetUser>("SetUser", new VpnRpcSetUser());

        /// <summary>
        /// Set user setting (Sync mode)
        /// </summary>
        public VpnRpcSetUser SetUser() => SetUserAsync().Result;

        /// <summary>
        /// Setting VPN Gate Server Configuration (Async mode)
        /// </summary>
        public async Task<VpnVgsConfig> SetVgsConfigAsync() => await Call<VpnVgsConfig>("SetVgsConfig", new VpnVgsConfig());

        /// <summary>
        /// Setting VPN Gate Server Configuration (Sync mode)
        /// </summary>
        public VpnVgsConfig SetVgsConfig() => SetVgsConfigAsync().Result;

        /// <summary>
        /// Start a virtual layer-3 switch (Async mode)
        /// </summary>
        public async Task<VpnRpcL3Sw> StartL3SwitchAsync() => await Call<VpnRpcL3Sw>("StartL3Switch", new VpnRpcL3Sw());

        /// <summary>
        /// Start a virtual layer-3 switch (Sync mode)
        /// </summary>
        public VpnRpcL3Sw StartL3Switch() => StartL3SwitchAsync().Result;

        /// <summary>
        /// Stop a virtual layer-3 switch (Async mode)
        /// </summary>
        public async Task<VpnRpcL3Sw> StopL3SwitchAsync() => await Call<VpnRpcL3Sw>("StopL3Switch", new VpnRpcL3Sw());

        /// <summary>
        /// Stop a virtual layer-3 switch (Sync mode)
        /// </summary>
        public VpnRpcL3Sw StopL3Switch() => StopL3SwitchAsync().Result;

        /// <summary>
        /// test RPC function (Async mode)
        /// </summary>
        public async Task<VpnRpcTest> TestAsync() => await Call<VpnRpcTest>("Test", new VpnRpcTest());

        /// <summary>
        /// test RPC function (Sync mode)
        /// </summary>
        public VpnRpcTest Test() => TestAsync().Result;

    }
}
