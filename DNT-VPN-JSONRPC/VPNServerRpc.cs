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
            rpc_client = new JsonRpcClient($"https://{vpnserver_host}:{vpnserver_port }/api/", null);

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

        //public async Task<RpcServerInfo> GetServerInfoAsync() => await Call<RpcServerInfo>("GetServerInfo", new RpcServerInfo());
        //public RpcServerInfo GetServerInfo() => GetServerInfoAsync().Result;
        /// <summary>VPN RPC: Add access list entry (Async mode)</summary>
        public async Task<VpnRpcAddAccess> AddAccessAsync() => await Call<VpnRpcAddAccess>("AddAccess", new VpnRpcAddAccess());

        /// <summary>VPN RPC: Add access list entry (Sync mode)</summary>
        public VpnRpcAddAccess AddAccess() => AddAccessAsync().Result;

        /// <summary>VPN RPC: Add CA(Certificate Authority) into the hub (Async mode)</summary>
        public async Task<VpnRpcHubAddCA> AddCaAsync() => await Call<VpnRpcHubAddCA>("AddCa", new VpnRpcHubAddCA());

        /// <summary>VPN RPC: Add CA(Certificate Authority) into the hub (Sync mode)</summary>
        public VpnRpcHubAddCA AddCa() => AddCaAsync().Result;

        /// <summary>VPN RPC: Add new CRL (Certificate Revocation List) entry (Async mode)</summary>
        public async Task<VpnRpcCrl> AddCrlAsync() => await Call<VpnRpcCrl>("AddCrl", new VpnRpcCrl());

        /// <summary>VPN RPC: Add new CRL (Certificate Revocation List) entry (Sync mode)</summary>
        public VpnRpcCrl AddCrl() => AddCrlAsync().Result;

        /// <summary>VPN RPC: Add EtherIP ID setting (Async mode)</summary>
        public async Task<VpnEtherIpId> AddEtherIpIdAsync() => await Call<VpnEtherIpId>("AddEtherIpId", new VpnEtherIpId());

        /// <summary>VPN RPC: Add EtherIP ID setting (Sync mode)</summary>
        public VpnEtherIpId AddEtherIpId() => AddEtherIpIdAsync().Result;

        /// <summary>VPN RPC: Add new virtual interface on virtual L3 switch (Async mode)</summary>
        public async Task<VpnRpcL3If> AddL3IfAsync() => await Call<VpnRpcL3If>("AddL3If", new VpnRpcL3If());

        /// <summary>VPN RPC: Add new virtual interface on virtual L3 switch (Sync mode)</summary>
        public VpnRpcL3If AddL3If() => AddL3IfAsync().Result;

        /// <summary>VPN RPC: Add a new virtual layer-3 switch (Async mode)</summary>
        public async Task<VpnRpcL3Sw> AddL3SwitchAsync() => await Call<VpnRpcL3Sw>("AddL3Switch", new VpnRpcL3Sw());

        /// <summary>VPN RPC: Add a new virtual layer-3 switch (Sync mode)</summary>
        public VpnRpcL3Sw AddL3Switch() => AddL3SwitchAsync().Result;

        /// <summary>VPN RPC: Add new routing table entry on virtual L3 switch (Async mode)</summary>
        public async Task<VpnRpcL3Table> AddL3TableAsync() => await Call<VpnRpcL3Table>("AddL3Table", new VpnRpcL3Table());

        /// <summary>VPN RPC: Add new routing table entry on virtual L3 switch (Sync mode)</summary>
        public VpnRpcL3Table AddL3Table() => AddL3TableAsync().Result;

        /// <summary>VPN RPC: Add new license key (Async mode)</summary>
        public async Task<VpnRpcTest> AddLicenseKeyAsync() => await Call<VpnRpcTest>("AddLicenseKey", new VpnRpcTest());

        /// <summary>VPN RPC: Add new license key (Sync mode)</summary>
        public VpnRpcTest AddLicenseKey() => AddLicenseKeyAsync().Result;

        /// <summary>VPN RPC: Add a new local bridge (Async mode)</summary>
        public async Task<VpnRpcLocalBridge> AddLocalBridgeAsync() => await Call<VpnRpcLocalBridge>("AddLocalBridge", new VpnRpcLocalBridge());

        /// <summary>VPN RPC: Add a new local bridge (Sync mode)</summary>
        public VpnRpcLocalBridge AddLocalBridge() => AddLocalBridgeAsync().Result;

        /// <summary>VPN RPC: Change host-name for DDNS client (Async mode)</summary>
        public async Task<VpnRpcTest> ChangeDDnsClientHostnameAsync() => await Call<VpnRpcTest>("ChangeDDnsClientHostname", new VpnRpcTest());

        /// <summary>VPN RPC: Change host-name for DDNS client (Sync mode)</summary>
        public VpnRpcTest ChangeDDnsClientHostname() => ChangeDDnsClientHostnameAsync().Result;

        /// <summary>VPN RPC: Do Crash (Async mode)</summary>
        public async Task<VpnRpcTest> CrashAsync() => await Call<VpnRpcTest>("Crash", new VpnRpcTest());

        /// <summary>VPN RPC: Do Crash (Sync mode)</summary>
        public VpnRpcTest Crash() => CrashAsync().Result;

        /// <summary>VPN RPC: Create a group (Async mode)</summary>
        public async Task<VpnRpcSetGroup> CreateGroupAsync() => await Call<VpnRpcSetGroup>("CreateGroup", new VpnRpcSetGroup());

        /// <summary>VPN RPC: Create a group (Sync mode)</summary>
        public VpnRpcSetGroup CreateGroup() => CreateGroupAsync().Result;

        /// <summary>VPN RPC: Create a hub (Async mode)</summary>
        public async Task<VpnRpcCreateHub> CreateHubAsync() => await Call<VpnRpcCreateHub>("CreateHub", new VpnRpcCreateHub());

        /// <summary>VPN RPC: Create a hub (Sync mode)</summary>
        public VpnRpcCreateHub CreateHub() => CreateHubAsync().Result;

        /// <summary>VPN RPC: Create a new link(cascade) (Async mode)</summary>
        public async Task<VpnRpcCreateLink> CreateLinkAsync() => await Call<VpnRpcCreateLink>("CreateLink", new VpnRpcCreateLink());

        /// <summary>VPN RPC: Create a new link(cascade) (Sync mode)</summary>
        public VpnRpcCreateLink CreateLink() => CreateLinkAsync().Result;

        /// <summary>VPN RPC: Create a listener (Async mode)</summary>
        public async Task<VpnRpcListener> CreateListenerAsync() => await Call<VpnRpcListener>("CreateListener", new VpnRpcListener());

        /// <summary>VPN RPC: Create a listener (Sync mode)</summary>
        public VpnRpcListener CreateListener() => CreateListenerAsync().Result;

        /// <summary>VPN RPC: Create a user (Async mode)</summary>
        public async Task<VpnRpcSetUser> CreateUserAsync() => await Call<VpnRpcSetUser>("CreateUser", new VpnRpcSetUser());

        /// <summary>VPN RPC: Create a user (Sync mode)</summary>
        public VpnRpcSetUser CreateUser() => CreateUserAsync().Result;

        /// <summary>VPN RPC: Do debug function (Async mode)</summary>
        public async Task<VpnRpcTest> DebugAsync() => await Call<VpnRpcTest>("Debug", new VpnRpcTest());

        /// <summary>VPN RPC: Do debug function (Sync mode)</summary>
        public VpnRpcTest Debug() => DebugAsync().Result;

        /// <summary>VPN RPC: Delete CRL (Certificate Revocation List) entry (Async mode)</summary>
        public async Task<VpnRpcCrl> DelCrlAsync() => await Call<VpnRpcCrl>("DelCrl", new VpnRpcCrl());

        /// <summary>VPN RPC: Delete CRL (Certificate Revocation List) entry (Sync mode)</summary>
        public VpnRpcCrl DelCrl() => DelCrlAsync().Result;

        /// <summary>VPN RPC: Delete access list entry (Async mode)</summary>
        public async Task<VpnRpcDeleteAccess> DeleteAccessAsync() => await Call<VpnRpcDeleteAccess>("DeleteAccess", new VpnRpcDeleteAccess());

        /// <summary>VPN RPC: Delete access list entry (Sync mode)</summary>
        public VpnRpcDeleteAccess DeleteAccess() => DeleteAccessAsync().Result;

        /// <summary>VPN RPC: Delete a CA(Certificate Authority) setting from the hub (Async mode)</summary>
        public async Task<VpnRpcHubDeleteCA> DeleteCaAsync() => await Call<VpnRpcHubDeleteCA>("DeleteCa", new VpnRpcHubDeleteCA());

        /// <summary>VPN RPC: Delete a CA(Certificate Authority) setting from the hub (Sync mode)</summary>
        public VpnRpcHubDeleteCA DeleteCa() => DeleteCaAsync().Result;

        /// <summary>VPN RPC: Delete EtherIP ID setting (Async mode)</summary>
        public async Task<VpnEtherIpId> DeleteEtherIpIdAsync() => await Call<VpnEtherIpId>("DeleteEtherIpId", new VpnEtherIpId());

        /// <summary>VPN RPC: Delete EtherIP ID setting (Sync mode)</summary>
        public VpnEtherIpId DeleteEtherIpId() => DeleteEtherIpIdAsync().Result;

        /// <summary>VPN RPC: Delete a group (Async mode)</summary>
        public async Task<VpnRpcDeleteUser> DeleteGroupAsync() => await Call<VpnRpcDeleteUser>("DeleteGroup", new VpnRpcDeleteUser());

        /// <summary>VPN RPC: Delete a group (Sync mode)</summary>
        public VpnRpcDeleteUser DeleteGroup() => DeleteGroupAsync().Result;

        /// <summary>VPN RPC: Delete a hub (Async mode)</summary>
        public async Task<VpnRpcDeleteHub> DeleteHubAsync() => await Call<VpnRpcDeleteHub>("DeleteHub", new VpnRpcDeleteHub());

        /// <summary>VPN RPC: Delete a hub (Sync mode)</summary>
        public VpnRpcDeleteHub DeleteHub() => DeleteHubAsync().Result;

        /// <summary>VPN RPC: Delete IP address table entry (Async mode)</summary>
        public async Task<VpnRpcDeleteTable> DeleteIpTableAsync() => await Call<VpnRpcDeleteTable>("DeleteIpTable", new VpnRpcDeleteTable());

        /// <summary>VPN RPC: Delete IP address table entry (Sync mode)</summary>
        public VpnRpcDeleteTable DeleteIpTable() => DeleteIpTableAsync().Result;

        /// <summary>VPN RPC: Delete a link (Async mode)</summary>
        public async Task<VpnRpcLink> DeleteLinkAsync() => await Call<VpnRpcLink>("DeleteLink", new VpnRpcLink());

        /// <summary>VPN RPC: Delete a link (Sync mode)</summary>
        public VpnRpcLink DeleteLink() => DeleteLinkAsync().Result;

        /// <summary>VPN RPC: Delete a listener (Async mode)</summary>
        public async Task<VpnRpcListener> DeleteListenerAsync() => await Call<VpnRpcListener>("DeleteListener", new VpnRpcListener());

        /// <summary>VPN RPC: Delete a listener (Sync mode)</summary>
        public VpnRpcListener DeleteListener() => DeleteListenerAsync().Result;

        /// <summary>VPN RPC: Delete a local bridge (Async mode)</summary>
        public async Task<VpnRpcLocalBridge> DeleteLocalBridgeAsync() => await Call<VpnRpcLocalBridge>("DeleteLocalBridge", new VpnRpcLocalBridge());

        /// <summary>VPN RPC: Delete a local bridge (Sync mode)</summary>
        public VpnRpcLocalBridge DeleteLocalBridge() => DeleteLocalBridgeAsync().Result;

        /// <summary>VPN RPC: Delete MAC address table entry (Async mode)</summary>
        public async Task<VpnRpcDeleteTable> DeleteMacTableAsync() => await Call<VpnRpcDeleteTable>("DeleteMacTable", new VpnRpcDeleteTable());

        /// <summary>VPN RPC: Delete MAC address table entry (Sync mode)</summary>
        public VpnRpcDeleteTable DeleteMacTable() => DeleteMacTableAsync().Result;

        /// <summary>VPN RPC: Delete a session (Async mode)</summary>
        public async Task<VpnRpcDeleteSession> DeleteSessionAsync() => await Call<VpnRpcDeleteSession>("DeleteSession", new VpnRpcDeleteSession());

        /// <summary>VPN RPC: Delete a session (Sync mode)</summary>
        public VpnRpcDeleteSession DeleteSession() => DeleteSessionAsync().Result;

        /// <summary>VPN RPC: Delete a user (Async mode)</summary>
        public async Task<VpnRpcDeleteUser> DeleteUserAsync() => await Call<VpnRpcDeleteUser>("DeleteUser", new VpnRpcDeleteUser());

        /// <summary>VPN RPC: Delete a user (Sync mode)</summary>
        public VpnRpcDeleteUser DeleteUser() => DeleteUserAsync().Result;

        /// <summary>VPN RPC: Delete a virtual interface on virtual L3 switch (Async mode)</summary>
        public async Task<VpnRpcL3If> DelL3IfAsync() => await Call<VpnRpcL3If>("DelL3If", new VpnRpcL3If());

        /// <summary>VPN RPC: Delete a virtual interface on virtual L3 switch (Sync mode)</summary>
        public VpnRpcL3If DelL3If() => DelL3IfAsync().Result;

        /// <summary>VPN RPC: Delete a virtual layer-3 switch (Async mode)</summary>
        public async Task<VpnRpcL3Sw> DelL3SwitchAsync() => await Call<VpnRpcL3Sw>("DelL3Switch", new VpnRpcL3Sw());

        /// <summary>VPN RPC: Delete a virtual layer-3 switch (Sync mode)</summary>
        public VpnRpcL3Sw DelL3Switch() => DelL3SwitchAsync().Result;

        /// <summary>VPN RPC: Delete routing table entry on virtual L3 switch (Async mode)</summary>
        public async Task<VpnRpcL3Table> DelL3TableAsync() => await Call<VpnRpcL3Table>("DelL3Table", new VpnRpcL3Table());

        /// <summary>VPN RPC: Delete routing table entry on virtual L3 switch (Sync mode)</summary>
        public VpnRpcL3Table DelL3Table() => DelL3TableAsync().Result;

        /// <summary>VPN RPC: Delete a license key (Async mode)</summary>
        public async Task<VpnRpcTest> DelLicenseKeyAsync() => await Call<VpnRpcTest>("DelLicenseKey", new VpnRpcTest());

        /// <summary>VPN RPC: Delete a license key (Sync mode)</summary>
        public VpnRpcTest DelLicenseKey() => DelLicenseKeyAsync().Result;

        /// <summary>VPN RPC: Disable the SecureNAT function of the hub (Async mode)</summary>
        public async Task<VpnRpcHub> DisableSecureNATAsync() => await Call<VpnRpcHub>("DisableSecureNAT", new VpnRpcHub());

        /// <summary>VPN RPC: Disable the SecureNAT function of the hub (Sync mode)</summary>
        public VpnRpcHub DisableSecureNAT() => DisableSecureNATAsync().Result;

        /// <summary>VPN RPC: Disconnect a connection (Async mode)</summary>
        public async Task<VpnRpcDisconnectConnection> DisconnectConnectionAsync() => await Call<VpnRpcDisconnectConnection>("DisconnectConnection", new VpnRpcDisconnectConnection());

        /// <summary>VPN RPC: Disconnect a connection (Sync mode)</summary>
        public VpnRpcDisconnectConnection DisconnectConnection() => DisconnectConnectionAsync().Result;

        /// <summary>VPN RPC: Enable / Disable listener (Async mode)</summary>
        public async Task<VpnRpcListener> EnableListenerAsync() => await Call<VpnRpcListener>("EnableListener", new VpnRpcListener());

        /// <summary>VPN RPC: Enable / Disable listener (Sync mode)</summary>
        public VpnRpcListener EnableListener() => EnableListenerAsync().Result;

        /// <summary>VPN RPC: Enable SecureNAT function of the hub (Async mode)</summary>
        public async Task<VpnRpcHub> EnableSecureNATAsync() => await Call<VpnRpcHub>("EnableSecureNAT", new VpnRpcHub());

        /// <summary>VPN RPC: Enable SecureNAT function of the hub (Sync mode)</summary>
        public VpnRpcHub EnableSecureNAT() => EnableSecureNATAsync().Result;

        /// <summary>VPN RPC: Get access list (Async mode)</summary>
        public async Task<VpnRpcEnumAccessList> EnumAccessAsync() => await Call<VpnRpcEnumAccessList>("EnumAccess", new VpnRpcEnumAccessList());

        /// <summary>VPN RPC: Get access list (Sync mode)</summary>
        public VpnRpcEnumAccessList EnumAccess() => EnumAccessAsync().Result;

        /// <summary>VPN RPC: Enumerate CA(Certificate Authority) in the hub (Async mode)</summary>
        public async Task<VpnRpcHubEnumCA> EnumCaAsync() => await Call<VpnRpcHubEnumCA>("EnumCa", new VpnRpcHubEnumCA());

        /// <summary>VPN RPC: Enumerate CA(Certificate Authority) in the hub (Sync mode)</summary>
        public VpnRpcHubEnumCA EnumCa() => EnumCaAsync().Result;

        /// <summary>VPN RPC: Enumerate connections (Async mode)</summary>
        public async Task<VpnRpcEnumConnection> EnumConnectionAsync() => await Call<VpnRpcEnumConnection>("EnumConnection", new VpnRpcEnumConnection());

        /// <summary>VPN RPC: Enumerate connections (Sync mode)</summary>
        public VpnRpcEnumConnection EnumConnection() => EnumConnectionAsync().Result;

        /// <summary>VPN RPC: Get CRL (Certificate Revocation List) index (Async mode)</summary>
        public async Task<VpnRpcEnumCrl> EnumCrlAsync() => await Call<VpnRpcEnumCrl>("EnumCrl", new VpnRpcEnumCrl());

        /// <summary>VPN RPC: Get CRL (Certificate Revocation List) index (Sync mode)</summary>
        public VpnRpcEnumCrl EnumCrl() => EnumCrlAsync().Result;

        /// <summary>VPN RPC: Enumerate DHCP entries (Async mode)</summary>
        public async Task<VpnRpcEnumDhcp> EnumDHCPAsync() => await Call<VpnRpcEnumDhcp>("EnumDHCP", new VpnRpcEnumDhcp());

        /// <summary>VPN RPC: Enumerate DHCP entries (Sync mode)</summary>
        public VpnRpcEnumDhcp EnumDHCP() => EnumDHCPAsync().Result;

        /// <summary>VPN RPC: Enumerate EtherIP ID settings (Async mode)</summary>
        public async Task<VpnRpcEnumEtherIpId> EnumEtherIpIdAsync() => await Call<VpnRpcEnumEtherIpId>("EnumEtherIpId", new VpnRpcEnumEtherIpId());

        /// <summary>VPN RPC: Enumerate EtherIP ID settings (Sync mode)</summary>
        public VpnRpcEnumEtherIpId EnumEtherIpId() => EnumEtherIpIdAsync().Result;

        /// <summary>VPN RPC: Enumerate Ethernet devices (Async mode)</summary>
        public async Task<VpnRpcEnumEth> EnumEthernetAsync() => await Call<VpnRpcEnumEth>("EnumEthernet", new VpnRpcEnumEth());

        /// <summary>VPN RPC: Enumerate Ethernet devices (Sync mode)</summary>
        public VpnRpcEnumEth EnumEthernet() => EnumEthernetAsync().Result;

        /// <summary>VPN RPC: Enumerate VLAN tag transparent setting (Async mode)</summary>
        public async Task<VpnRpcEnumEthVlan> EnumEthVLanAsync() => await Call<VpnRpcEnumEthVlan>("EnumEthVLan", new VpnRpcEnumEthVlan());

        /// <summary>VPN RPC: Enumerate VLAN tag transparent setting (Sync mode)</summary>
        public VpnRpcEnumEthVlan EnumEthVLan() => EnumEthVLanAsync().Result;

        /// <summary>VPN RPC: Enumerate cluster members (Async mode)</summary>
        public async Task<VpnRpcEnumFarm> EnumFarmMemberAsync() => await Call<VpnRpcEnumFarm>("EnumFarmMember", new VpnRpcEnumFarm());

        /// <summary>VPN RPC: Enumerate cluster members (Sync mode)</summary>
        public VpnRpcEnumFarm EnumFarmMember() => EnumFarmMemberAsync().Result;

        /// <summary>VPN RPC: Enumerate groups (Async mode)</summary>
        public async Task<VpnRpcEnumGroup> EnumGroupAsync() => await Call<VpnRpcEnumGroup>("EnumGroup", new VpnRpcEnumGroup());

        /// <summary>VPN RPC: Enumerate groups (Sync mode)</summary>
        public VpnRpcEnumGroup EnumGroup() => EnumGroupAsync().Result;

        /// <summary>VPN RPC: Enumerate hubs (Async mode)</summary>
        public async Task<VpnRpcEnumHub> EnumHubAsync() => await Call<VpnRpcEnumHub>("EnumHub", new VpnRpcEnumHub());

        /// <summary>VPN RPC: Enumerate hubs (Sync mode)</summary>
        public VpnRpcEnumHub EnumHub() => EnumHubAsync().Result;

        /// <summary>VPN RPC: Get IP address table (Async mode)</summary>
        public async Task<VpnRpcEnumIpTable> EnumIpTableAsync() => await Call<VpnRpcEnumIpTable>("EnumIpTable", new VpnRpcEnumIpTable());

        /// <summary>VPN RPC: Get IP address table (Sync mode)</summary>
        public VpnRpcEnumIpTable EnumIpTable() => EnumIpTableAsync().Result;

        /// <summary>VPN RPC: Enumerate virtual interfaces on virtual L3 switch (Async mode)</summary>
        public async Task<VpnRpcEnumL3If> EnumL3IfAsync() => await Call<VpnRpcEnumL3If>("EnumL3If", new VpnRpcEnumL3If());

        /// <summary>VPN RPC: Enumerate virtual interfaces on virtual L3 switch (Sync mode)</summary>
        public VpnRpcEnumL3If EnumL3If() => EnumL3IfAsync().Result;

        /// <summary>VPN RPC: Enumerate virtual layer-3 switches (Async mode)</summary>
        public async Task<VpnRpcEnumL3Sw> EnumL3SwitchAsync() => await Call<VpnRpcEnumL3Sw>("EnumL3Switch", new VpnRpcEnumL3Sw());

        /// <summary>VPN RPC: Enumerate virtual layer-3 switches (Sync mode)</summary>
        public VpnRpcEnumL3Sw EnumL3Switch() => EnumL3SwitchAsync().Result;

        /// <summary>VPN RPC: Get routing table on virtual L3 switch (Async mode)</summary>
        public async Task<VpnRpcEnumL3Table> EnumL3TableAsync() => await Call<VpnRpcEnumL3Table>("EnumL3Table", new VpnRpcEnumL3Table());

        /// <summary>VPN RPC: Get routing table on virtual L3 switch (Sync mode)</summary>
        public VpnRpcEnumL3Table EnumL3Table() => EnumL3TableAsync().Result;

        /// <summary>VPN RPC: Enumerate license key (Async mode)</summary>
        public async Task<VpnRpcEnumLicenseKey> EnumLicenseKeyAsync() => await Call<VpnRpcEnumLicenseKey>("EnumLicenseKey", new VpnRpcEnumLicenseKey());

        /// <summary>VPN RPC: Enumerate license key (Sync mode)</summary>
        public VpnRpcEnumLicenseKey EnumLicenseKey() => EnumLicenseKeyAsync().Result;

        /// <summary>VPN RPC: Enumerate links (Async mode)</summary>
        public async Task<VpnRpcEnumLink> EnumLinkAsync() => await Call<VpnRpcEnumLink>("EnumLink", new VpnRpcEnumLink());

        /// <summary>VPN RPC: Enumerate links (Sync mode)</summary>
        public VpnRpcEnumLink EnumLink() => EnumLinkAsync().Result;

        /// <summary>VPN RPC: Enumerating listeners (Async mode)</summary>
        public async Task<VpnRpcListenerList> EnumListenerAsync() => await Call<VpnRpcListenerList>("EnumListener", new VpnRpcListenerList());

        /// <summary>VPN RPC: Enumerating listeners (Sync mode)</summary>
        public VpnRpcListenerList EnumListener() => EnumListenerAsync().Result;

        /// <summary>VPN RPC: Enumerate local bridges (Async mode)</summary>
        public async Task<VpnRpcEnumLocalBridge> EnumLocalBridgeAsync() => await Call<VpnRpcEnumLocalBridge>("EnumLocalBridge", new VpnRpcEnumLocalBridge());

        /// <summary>VPN RPC: Enumerate local bridges (Sync mode)</summary>
        public VpnRpcEnumLocalBridge EnumLocalBridge() => EnumLocalBridgeAsync().Result;

        /// <summary>VPN RPC: Enumerate log files (Async mode)</summary>
        public async Task<VpnRpcEnumLogFile> EnumLogFileAsync() => await Call<VpnRpcEnumLogFile>("EnumLogFile", new VpnRpcEnumLogFile());

        /// <summary>VPN RPC: Enumerate log files (Sync mode)</summary>
        public VpnRpcEnumLogFile EnumLogFile() => EnumLogFileAsync().Result;

        /// <summary>VPN RPC: Get MAC address table (Async mode)</summary>
        public async Task<VpnRpcEnumMacTable> EnumMacTableAsync() => await Call<VpnRpcEnumMacTable>("EnumMacTable", new VpnRpcEnumMacTable());

        /// <summary>VPN RPC: Get MAC address table (Sync mode)</summary>
        public VpnRpcEnumMacTable EnumMacTable() => EnumMacTableAsync().Result;

        /// <summary>VPN RPC: Enumerate NAT entries of the SecureNAT (Async mode)</summary>
        public async Task<VpnRpcEnumNat> EnumNATAsync() => await Call<VpnRpcEnumNat>("EnumNAT", new VpnRpcEnumNat());

        /// <summary>VPN RPC: Enumerate NAT entries of the SecureNAT (Sync mode)</summary>
        public VpnRpcEnumNat EnumNAT() => EnumNATAsync().Result;

        /// <summary>VPN RPC: Enumerate sessions (Async mode)</summary>
        public async Task<VpnRpcEnumSession> EnumSessionAsync() => await Call<VpnRpcEnumSession>("EnumSession", new VpnRpcEnumSession());

        /// <summary>VPN RPC: Enumerate sessions (Sync mode)</summary>
        public VpnRpcEnumSession EnumSession() => EnumSessionAsync().Result;

        /// <summary>VPN RPC: Enumerate users (Async mode)</summary>
        public async Task<VpnRpcEnumUser> EnumUserAsync() => await Call<VpnRpcEnumUser>("EnumUser", new VpnRpcEnumUser());

        /// <summary>VPN RPC: Enumerate users (Sync mode)</summary>
        public VpnRpcEnumUser EnumUser() => EnumUserAsync().Result;

        /// <summary>VPN RPC: Flush configuration file (Async mode)</summary>
        public async Task<VpnRpcTest> FlushAsync() => await Call<VpnRpcTest>("Flush", new VpnRpcTest());

        /// <summary>VPN RPC: Flush configuration file (Sync mode)</summary>
        public VpnRpcTest Flush() => FlushAsync().Result;

        /// <summary>VPN RPC: Get access control list (Async mode)</summary>
        public async Task<VpnRpcAcList> GetAcListAsync() => await Call<VpnRpcAcList>("GetAcList", new VpnRpcAcList());

        /// <summary>VPN RPC: Get access control list (Sync mode)</summary>
        public VpnRpcAcList GetAcList() => GetAcListAsync().Result;

        /// <summary>VPN RPC: Get message for administrators (Async mode)</summary>
        public async Task<VpnRpcMsg> GetAdminMsgAsync() => await Call<VpnRpcMsg>("GetAdminMsg", new VpnRpcMsg());

        /// <summary>VPN RPC: Get message for administrators (Sync mode)</summary>
        public VpnRpcMsg GetAdminMsg() => GetAdminMsgAsync().Result;

        /// <summary>VPN RPC: Get Azure status (Async mode)</summary>
        public async Task<VpnRpcAzureStatus> GetAzureStatusAsync() => await Call<VpnRpcAzureStatus>("GetAzureStatus", new VpnRpcAzureStatus());

        /// <summary>VPN RPC: Get Azure status (Sync mode)</summary>
        public VpnRpcAzureStatus GetAzureStatus() => GetAzureStatusAsync().Result;

        /// <summary>VPN RPC: Get availability to localbridge function (Async mode)</summary>
        public async Task<VpnRpcBridgeSupport> GetBridgeSupportAsync() => await Call<VpnRpcBridgeSupport>("GetBridgeSupport", new VpnRpcBridgeSupport());

        /// <summary>VPN RPC: Get availability to localbridge function (Sync mode)</summary>
        public VpnRpcBridgeSupport GetBridgeSupport() => GetBridgeSupportAsync().Result;

        /// <summary>VPN RPC: Get CA(Certificate Authority) setting from the hub (Async mode)</summary>
        public async Task<VpnRpcHubGetCA> GetCaAsync() => await Call<VpnRpcHubGetCA>("GetCa", new VpnRpcHubGetCA());

        /// <summary>VPN RPC: Get CA(Certificate Authority) setting from the hub (Sync mode)</summary>
        public VpnRpcHubGetCA GetCa() => GetCaAsync().Result;

        /// <summary>VPN RPC: Get capabilities (Async mode)</summary>
        public async Task<VpnCapslist> GetCapsAsync() => await Call<VpnCapslist>("GetCaps", new VpnCapslist());

        /// <summary>VPN RPC: Get capabilities (Sync mode)</summary>
        public VpnCapslist GetCaps() => GetCapsAsync().Result;

        /// <summary>VPN RPC: Get configuration file stream (Async mode)</summary>
        public async Task<VpnRpcConfig> GetConfigAsync() => await Call<VpnRpcConfig>("GetConfig", new VpnRpcConfig());

        /// <summary>VPN RPC: Get configuration file stream (Sync mode)</summary>
        public VpnRpcConfig GetConfig() => GetConfigAsync().Result;

        /// <summary>VPN RPC: Get connection information (Async mode)</summary>
        public async Task<VpnRpcConnectionInfo> GetConnectionInfoAsync() => await Call<VpnRpcConnectionInfo>("GetConnectionInfo", new VpnRpcConnectionInfo());

        /// <summary>VPN RPC: Get connection information (Sync mode)</summary>
        public VpnRpcConnectionInfo GetConnectionInfo() => GetConnectionInfoAsync().Result;

        /// <summary>VPN RPC: Get CRL (Certificate Revocation List) entry (Async mode)</summary>
        public async Task<VpnRpcCrl> GetCrlAsync() => await Call<VpnRpcCrl>("GetCrl", new VpnRpcCrl());

        /// <summary>VPN RPC: Get CRL (Certificate Revocation List) entry (Sync mode)</summary>
        public VpnRpcCrl GetCrl() => GetCrlAsync().Result;

        /// <summary>VPN RPC: Get status of DDNS client (Async mode)</summary>
        public async Task<VpnDDnsClientStatus> GetDDnsClientStatusAsync() => await Call<VpnDDnsClientStatus>("GetDDnsClientStatus", new VpnDDnsClientStatus());

        /// <summary>VPN RPC: Get status of DDNS client (Sync mode)</summary>
        public VpnDDnsClientStatus GetDDnsClientStatus() => GetDDnsClientStatusAsync().Result;

        /// <summary>VPN RPC: Get DDNS proxy configuration (Async mode)</summary>
        public async Task<VpnInternetSetting> GetDDnsInternetSettngAsync() => await Call<VpnInternetSetting>("GetDDnsInternetSettng", new VpnInternetSetting());

        /// <summary>VPN RPC: Get DDNS proxy configuration (Sync mode)</summary>
        public VpnInternetSetting GetDDnsInternetSettng() => GetDDnsInternetSettngAsync().Result;

        /// <summary>VPN RPC: Get default hub administration options (Async mode)</summary>
        public async Task<VpnRpcAdminOption> GetDefaultHubAdminOptionsAsync() => await Call<VpnRpcAdminOption>("GetDefaultHubAdminOptions", new VpnRpcAdminOption());

        /// <summary>VPN RPC: Get default hub administration options (Sync mode)</summary>
        public VpnRpcAdminOption GetDefaultHubAdminOptions() => GetDefaultHubAdminOptionsAsync().Result;

        /// <summary>VPN RPC: Get EtherIP ID setting (Async mode)</summary>
        public async Task<VpnEtherIpId> GetEtherIpIdAsync() => await Call<VpnEtherIpId>("GetEtherIpId", new VpnEtherIpId());

        /// <summary>VPN RPC: Get EtherIP ID setting (Sync mode)</summary>
        public VpnEtherIpId GetEtherIpId() => GetEtherIpIdAsync().Result;

        /// <summary>VPN RPC: Get status of connection to cluster controller (Async mode)</summary>
        public async Task<VpnRpcFarmConnectionStatus> GetFarmConnectionStatusAsync() => await Call<VpnRpcFarmConnectionStatus>("GetFarmConnectionStatus", new VpnRpcFarmConnectionStatus());

        /// <summary>VPN RPC: Get status of connection to cluster controller (Sync mode)</summary>
        public VpnRpcFarmConnectionStatus GetFarmConnectionStatus() => GetFarmConnectionStatusAsync().Result;

        /// <summary>VPN RPC: Get cluster member information (Async mode)</summary>
        public async Task<VpnRpcFarmInfo> GetFarmInfoAsync() => await Call<VpnRpcFarmInfo>("GetFarmInfo", new VpnRpcFarmInfo());

        /// <summary>VPN RPC: Get cluster member information (Sync mode)</summary>
        public VpnRpcFarmInfo GetFarmInfo() => GetFarmInfoAsync().Result;

        /// <summary>VPN RPC: Get clustering configuration (Async mode)</summary>
        public async Task<VpnRpcFarm> GetFarmSettingAsync() => await Call<VpnRpcFarm>("GetFarmSetting", new VpnRpcFarm());

        /// <summary>VPN RPC: Get clustering configuration (Sync mode)</summary>
        public VpnRpcFarm GetFarmSetting() => GetFarmSettingAsync().Result;

        /// <summary>VPN RPC: Get group information (Async mode)</summary>
        public async Task<VpnRpcSetGroup> GetGroupAsync() => await Call<VpnRpcSetGroup>("GetGroup", new VpnRpcSetGroup());

        /// <summary>VPN RPC: Get group information (Sync mode)</summary>
        public VpnRpcSetGroup GetGroup() => GetGroupAsync().Result;

        /// <summary>VPN RPC: Get hub configuration (Async mode)</summary>
        public async Task<VpnRpcCreateHub> GetHubAsync() => await Call<VpnRpcCreateHub>("GetHub", new VpnRpcCreateHub());

        /// <summary>VPN RPC: Get hub configuration (Sync mode)</summary>
        public VpnRpcCreateHub GetHub() => GetHubAsync().Result;

        /// <summary>VPN RPC: Get hub administration options (Async mode)</summary>
        public async Task<VpnRpcAdminOption> GetHubAdminOptionsAsync() => await Call<VpnRpcAdminOption>("GetHubAdminOptions", new VpnRpcAdminOption());

        /// <summary>VPN RPC: Get hub administration options (Sync mode)</summary>
        public VpnRpcAdminOption GetHubAdminOptions() => GetHubAdminOptionsAsync().Result;

        /// <summary>VPN RPC: Get hub extended options (Async mode)</summary>
        public async Task<VpnRpcAdminOption> GetHubExtOptionsAsync() => await Call<VpnRpcAdminOption>("GetHubExtOptions", new VpnRpcAdminOption());

        /// <summary>VPN RPC: Get hub extended options (Sync mode)</summary>
        public VpnRpcAdminOption GetHubExtOptions() => GetHubExtOptionsAsync().Result;

        /// <summary>VPN RPC: Get logging configuration of the hub (Async mode)</summary>
        public async Task<VpnRpcHubLog> GetHubLogAsync() => await Call<VpnRpcHubLog>("GetHubLog", new VpnRpcHubLog());

        /// <summary>VPN RPC: Get logging configuration of the hub (Sync mode)</summary>
        public VpnRpcHubLog GetHubLog() => GetHubLogAsync().Result;

        /// <summary>VPN RPC: Get message of today on hub (Async mode)</summary>
        public async Task<VpnRpcMsg> GetHubMsgAsync() => await Call<VpnRpcMsg>("GetHubMsg", new VpnRpcMsg());

        /// <summary>VPN RPC: Get message of today on hub (Sync mode)</summary>
        public VpnRpcMsg GetHubMsg() => GetHubMsgAsync().Result;

        /// <summary>VPN RPC: Get Radius options of the hub (Async mode)</summary>
        public async Task<VpnRpcRadius> GetHubRadiusAsync() => await Call<VpnRpcRadius>("GetHubRadius", new VpnRpcRadius());

        /// <summary>VPN RPC: Get Radius options of the hub (Sync mode)</summary>
        public VpnRpcRadius GetHubRadius() => GetHubRadiusAsync().Result;

        /// <summary>VPN RPC: Get hub status (Async mode)</summary>
        public async Task<VpnRpcHubStatus> GetHubStatusAsync() => await Call<VpnRpcHubStatus>("GetHubStatus", new VpnRpcHubStatus());

        /// <summary>VPN RPC: Get hub status (Sync mode)</summary>
        public VpnRpcHubStatus GetHubStatus() => GetHubStatusAsync().Result;

        /// <summary>VPN RPC: Get IPsec service configuration (Async mode)</summary>
        public async Task<VpnIPsecServices> GetIPsecServicesAsync() => await Call<VpnIPsecServices>("GetIPsecServices", new VpnIPsecServices());

        /// <summary>VPN RPC: Get IPsec service configuration (Sync mode)</summary>
        public VpnIPsecServices GetIPsecServices() => GetIPsecServicesAsync().Result;

        /// <summary>VPN RPC: Get keep-alive function setting (Async mode)</summary>
        public async Task<VpnRpcKeep> GetKeepAsync() => await Call<VpnRpcKeep>("GetKeep", new VpnRpcKeep());

        /// <summary>VPN RPC: Get keep-alive function setting (Sync mode)</summary>
        public VpnRpcKeep GetKeep() => GetKeepAsync().Result;

        /// <summary>VPN RPC: Get license status (Async mode)</summary>
        public async Task<VpnRpcLicenseStatus> GetLicenseStatusAsync() => await Call<VpnRpcLicenseStatus>("GetLicenseStatus", new VpnRpcLicenseStatus());

        /// <summary>VPN RPC: Get license status (Sync mode)</summary>
        public VpnRpcLicenseStatus GetLicenseStatus() => GetLicenseStatusAsync().Result;

        /// <summary>VPN RPC: Get link configuration (Async mode)</summary>
        public async Task<VpnRpcCreateLink> GetLinkAsync() => await Call<VpnRpcCreateLink>("GetLink", new VpnRpcCreateLink());

        /// <summary>VPN RPC: Get link configuration (Sync mode)</summary>
        public VpnRpcCreateLink GetLink() => GetLinkAsync().Result;

        /// <summary>VPN RPC: Get link status (Async mode)</summary>
        public async Task<VpnRpcLinkStatus> GetLinkStatusAsync() => await Call<VpnRpcLinkStatus>("GetLinkStatus", new VpnRpcLinkStatus());

        /// <summary>VPN RPC: Get link status (Sync mode)</summary>
        public VpnRpcLinkStatus GetLinkStatus() => GetLinkStatusAsync().Result;

        /// <summary>VPN RPC: Get configurations for OpenVPN and SSTP (Async mode)</summary>
        public async Task<VpnOpenVpnSstpConfig> GetOpenVpnSstpConfigAsync() => await Call<VpnOpenVpnSstpConfig>("GetOpenVpnSstpConfig", new VpnOpenVpnSstpConfig());

        /// <summary>VPN RPC: Get configurations for OpenVPN and SSTP (Sync mode)</summary>
        public VpnOpenVpnSstpConfig GetOpenVpnSstpConfig() => GetOpenVpnSstpConfigAsync().Result;

        /// <summary>VPN RPC: Get SecureNAT options (Async mode)</summary>
        public async Task<VpnVhOption> GetSecureNATOptionAsync() => await Call<VpnVhOption>("GetSecureNATOption", new VpnVhOption());

        /// <summary>VPN RPC: Get SecureNAT options (Sync mode)</summary>
        public VpnVhOption GetSecureNATOption() => GetSecureNATOptionAsync().Result;

        /// <summary>VPN RPC: Get status of the SecureNAT (Async mode)</summary>
        public async Task<VpnRpcNatStatus> GetSecureNATStatusAsync() => await Call<VpnRpcNatStatus>("GetSecureNATStatus", new VpnRpcNatStatus());

        /// <summary>VPN RPC: Get status of the SecureNAT (Sync mode)</summary>
        public VpnRpcNatStatus GetSecureNATStatus() => GetSecureNATStatusAsync().Result;

        /// <summary>VPN RPC: Get the server certification (Async mode)</summary>
        public async Task<VpnRpcKeyPair> GetServerCertAsync() => await Call<VpnRpcKeyPair>("GetServerCert", new VpnRpcKeyPair());

        /// <summary>VPN RPC: Get the server certification (Sync mode)</summary>
        public VpnRpcKeyPair GetServerCert() => GetServerCertAsync().Result;

        /// <summary>VPN RPC: Get cipher for SSL (Async mode)</summary>
        public async Task<VpnRpcStr> GetServerCipherAsync() => await Call<VpnRpcStr>("GetServerCipher", new VpnRpcStr());

        /// <summary>VPN RPC: Get cipher for SSL (Sync mode)</summary>
        public VpnRpcStr GetServerCipher() => GetServerCipherAsync().Result;

        /// <summary>VPN RPC: Get server information (Async mode)</summary>
        public async Task<VpnRpcServerInfo> GetServerInfoAsync() => await Call<VpnRpcServerInfo>("GetServerInfo", new VpnRpcServerInfo());

        /// <summary>VPN RPC: Get server information (Sync mode)</summary>
        public VpnRpcServerInfo GetServerInfo() => GetServerInfoAsync().Result;

        /// <summary>VPN RPC: Get server status (Async mode)</summary>
        public async Task<VpnRpcServerStatus> GetServerStatusAsync() => await Call<VpnRpcServerStatus>("GetServerStatus", new VpnRpcServerStatus());

        /// <summary>VPN RPC: Get server status (Sync mode)</summary>
        public VpnRpcServerStatus GetServerStatus() => GetServerStatusAsync().Result;

        /// <summary>VPN RPC: Get session status (Async mode)</summary>
        public async Task<VpnRpcSessionStatus> GetSessionStatusAsync() => await Call<VpnRpcSessionStatus>("GetSessionStatus", new VpnRpcSessionStatus());

        /// <summary>VPN RPC: Get session status (Sync mode)</summary>
        public VpnRpcSessionStatus GetSessionStatus() => GetSessionStatusAsync().Result;

        /// <summary>VPN RPC: Get special listener status (Async mode)</summary>
        public async Task<VpnRpcSpecialListener> GetSpecialListenerAsync() => await Call<VpnRpcSpecialListener>("GetSpecialListener", new VpnRpcSpecialListener());

        /// <summary>VPN RPC: Get special listener status (Sync mode)</summary>
        public VpnRpcSpecialListener GetSpecialListener() => GetSpecialListenerAsync().Result;

        /// <summary>VPN RPC: Get syslog function setting (Async mode)</summary>
        public async Task<VpnSyslogSetting> GetSysLogAsync() => await Call<VpnSyslogSetting>("GetSysLog", new VpnSyslogSetting());

        /// <summary>VPN RPC: Get syslog function setting (Sync mode)</summary>
        public VpnSyslogSetting GetSysLog() => GetSysLogAsync().Result;

        /// <summary>VPN RPC: Get user setting (Async mode)</summary>
        public async Task<VpnRpcSetUser> GetUserAsync() => await Call<VpnRpcSetUser>("GetUser", new VpnRpcSetUser());

        /// <summary>VPN RPC: Get user setting (Sync mode)</summary>
        public VpnRpcSetUser GetUser() => GetUserAsync().Result;

        /// <summary>VPN RPC: Get VPN Gate configuration (Async mode)</summary>
        public async Task<VpnVgsConfig> GetVgsConfigAsync() => await Call<VpnVgsConfig>("GetVgsConfig", new VpnVgsConfig());

        /// <summary>VPN RPC: Get VPN Gate configuration (Sync mode)</summary>
        public VpnVgsConfig GetVgsConfig() => GetVgsConfigAsync().Result;

        /// <summary>VPN RPC: Generate OpenVPN configuration files (Async mode)</summary>
        public async Task<VpnRpcReadLogFile> MakeOpenVpnConfigFileAsync() => await Call<VpnRpcReadLogFile>("MakeOpenVpnConfigFile", new VpnRpcReadLogFile());

        /// <summary>VPN RPC: Generate OpenVPN configuration files (Sync mode)</summary>
        public VpnRpcReadLogFile MakeOpenVpnConfigFile() => MakeOpenVpnConfigFileAsync().Result;

        /// <summary>VPN RPC: Read a log file (Async mode)</summary>
        public async Task<VpnRpcReadLogFile> ReadLogFileAsync() => await Call<VpnRpcReadLogFile>("ReadLogFile", new VpnRpcReadLogFile());

        /// <summary>VPN RPC: Read a log file (Sync mode)</summary>
        public VpnRpcReadLogFile ReadLogFile() => ReadLogFileAsync().Result;

        /// <summary>VPN RPC: Reboot server itself (Async mode)</summary>
        public async Task<VpnRpcTest> RebootServerAsync() => await Call<VpnRpcTest>("RebootServer", new VpnRpcTest());

        /// <summary>VPN RPC: Reboot server itself (Sync mode)</summary>
        public VpnRpcTest RebootServer() => RebootServerAsync().Result;

        /// <summary>VPN RPC: Regenerate server certification (Async mode)</summary>
        public async Task<VpnRpcTest> RegenerateServerCertAsync() => await Call<VpnRpcTest>("RegenerateServerCert", new VpnRpcTest());

        /// <summary>VPN RPC: Regenerate server certification (Sync mode)</summary>
        public VpnRpcTest RegenerateServerCert() => RegenerateServerCertAsync().Result;

        /// <summary>VPN RPC: Rename link (cascade connection) (Async mode)</summary>
        public async Task<VpnRpcRenameLink> RenameLinkAsync() => await Call<VpnRpcRenameLink>("RenameLink", new VpnRpcRenameLink());

        /// <summary>VPN RPC: Rename link (cascade connection) (Sync mode)</summary>
        public VpnRpcRenameLink RenameLink() => RenameLinkAsync().Result;

        /// <summary>VPN RPC: Set access list (Async mode)</summary>
        public async Task<VpnRpcEnumAccessList> SetAccessListAsync() => await Call<VpnRpcEnumAccessList>("SetAccessList", new VpnRpcEnumAccessList());

        /// <summary>VPN RPC: Set access list (Sync mode)</summary>
        public VpnRpcEnumAccessList SetAccessList() => SetAccessListAsync().Result;

        /// <summary>VPN RPC: Set access control list (Async mode)</summary>
        public async Task<VpnRpcAcList> SetAcListAsync() => await Call<VpnRpcAcList>("SetAcList", new VpnRpcAcList());

        /// <summary>VPN RPC: Set access control list (Sync mode)</summary>
        public VpnRpcAcList SetAcList() => SetAcListAsync().Result;

        /// <summary>VPN RPC: Set Azure status (Async mode)</summary>
        public async Task<VpnRpcAzureStatus> SetAzureStatusAsync() => await Call<VpnRpcAzureStatus>("SetAzureStatus", new VpnRpcAzureStatus());

        /// <summary>VPN RPC: Set Azure status (Sync mode)</summary>
        public VpnRpcAzureStatus SetAzureStatus() => SetAzureStatusAsync().Result;

        /// <summary>VPN RPC: Overwrite configuration file by specified data (Async mode)</summary>
        public async Task<VpnRpcConfig> SetConfigAsync() => await Call<VpnRpcConfig>("SetConfig", new VpnRpcConfig());

        /// <summary>VPN RPC: Overwrite configuration file by specified data (Sync mode)</summary>
        public VpnRpcConfig SetConfig() => SetConfigAsync().Result;

        /// <summary>VPN RPC: Set CRL (Certificate Revocation List) entry (Async mode)</summary>
        public async Task<VpnRpcCrl> SetCrlAsync() => await Call<VpnRpcCrl>("SetCrl", new VpnRpcCrl());

        /// <summary>VPN RPC: Set CRL (Certificate Revocation List) entry (Sync mode)</summary>
        public VpnRpcCrl SetCrl() => SetCrlAsync().Result;

        /// <summary>VPN RPC: Set DDNS proxy configuration (Async mode)</summary>
        public async Task<VpnInternetSetting> SetDDnsInternetSettngAsync() => await Call<VpnInternetSetting>("SetDDnsInternetSettng", new VpnInternetSetting());

        /// <summary>VPN RPC: Set DDNS proxy configuration (Sync mode)</summary>
        public VpnInternetSetting SetDDnsInternetSettng() => SetDDnsInternetSettngAsync().Result;

        /// <summary>VPN RPC: Set VLAN tag transparent setting (Async mode)</summary>
        public async Task<VpnRpcTest> SetEnableEthVLanAsync() => await Call<VpnRpcTest>("SetEnableEthVLan", new VpnRpcTest());

        /// <summary>VPN RPC: Set VLAN tag transparent setting (Sync mode)</summary>
        public VpnRpcTest SetEnableEthVLan() => SetEnableEthVLanAsync().Result;

        /// <summary>VPN RPC: Set clustering configuration (Async mode)</summary>
        public async Task<VpnRpcFarm> SetFarmSettingAsync() => await Call<VpnRpcFarm>("SetFarmSetting", new VpnRpcFarm());

        /// <summary>VPN RPC: Set clustering configuration (Sync mode)</summary>
        public VpnRpcFarm SetFarmSetting() => SetFarmSettingAsync().Result;

        /// <summary>VPN RPC: Set group setting (Async mode)</summary>
        public async Task<VpnRpcSetGroup> SetGroupAsync() => await Call<VpnRpcSetGroup>("SetGroup", new VpnRpcSetGroup());

        /// <summary>VPN RPC: Set group setting (Sync mode)</summary>
        public VpnRpcSetGroup SetGroup() => SetGroupAsync().Result;

        /// <summary>VPN RPC: Set hub configuration (Async mode)</summary>
        public async Task<VpnRpcCreateHub> SetHubAsync() => await Call<VpnRpcCreateHub>("SetHub", new VpnRpcCreateHub());

        /// <summary>VPN RPC: Set hub configuration (Sync mode)</summary>
        public VpnRpcCreateHub SetHub() => SetHubAsync().Result;

        /// <summary>VPN RPC: Set hub administration options (Async mode)</summary>
        public async Task<VpnRpcAdminOption> SetHubAdminOptionsAsync() => await Call<VpnRpcAdminOption>("SetHubAdminOptions", new VpnRpcAdminOption());

        /// <summary>VPN RPC: Set hub administration options (Sync mode)</summary>
        public VpnRpcAdminOption SetHubAdminOptions() => SetHubAdminOptionsAsync().Result;

        /// <summary>VPN RPC: Set hub extended options (Async mode)</summary>
        public async Task<VpnRpcAdminOption> SetHubExtOptionsAsync() => await Call<VpnRpcAdminOption>("SetHubExtOptions", new VpnRpcAdminOption());

        /// <summary>VPN RPC: Set hub extended options (Sync mode)</summary>
        public VpnRpcAdminOption SetHubExtOptions() => SetHubExtOptionsAsync().Result;

        /// <summary>VPN RPC: Set logging configuration into the hub (Async mode)</summary>
        public async Task<VpnRpcHubLog> SetHubLogAsync() => await Call<VpnRpcHubLog>("SetHubLog", new VpnRpcHubLog());

        /// <summary>VPN RPC: Set logging configuration into the hub (Sync mode)</summary>
        public VpnRpcHubLog SetHubLog() => SetHubLogAsync().Result;

        /// <summary>VPN RPC: Set message of today on hub (Async mode)</summary>
        public async Task<VpnRpcMsg> SetHubMsgAsync() => await Call<VpnRpcMsg>("SetHubMsg", new VpnRpcMsg());

        /// <summary>VPN RPC: Set message of today on hub (Sync mode)</summary>
        public VpnRpcMsg SetHubMsg() => SetHubMsgAsync().Result;

        /// <summary>VPN RPC: Make a hub on-line or off-line (Async mode)</summary>
        public async Task<VpnRpcSetHubOnline> SetHubOnlineAsync() => await Call<VpnRpcSetHubOnline>("SetHubOnline", new VpnRpcSetHubOnline());

        /// <summary>VPN RPC: Make a hub on-line or off-line (Sync mode)</summary>
        public VpnRpcSetHubOnline SetHubOnline() => SetHubOnlineAsync().Result;

        /// <summary>VPN RPC: Set Radius options of the hub (Async mode)</summary>
        public async Task<VpnRpcRadius> SetHubRadiusAsync() => await Call<VpnRpcRadius>("SetHubRadius", new VpnRpcRadius());

        /// <summary>VPN RPC: Set Radius options of the hub (Sync mode)</summary>
        public VpnRpcRadius SetHubRadius() => SetHubRadiusAsync().Result;

        /// <summary>VPN RPC: Set IPsec service configuration (Async mode)</summary>
        public async Task<VpnIPsecServices> SetIPsecServicesAsync() => await Call<VpnIPsecServices>("SetIPsecServices", new VpnIPsecServices());

        /// <summary>VPN RPC: Set IPsec service configuration (Sync mode)</summary>
        public VpnIPsecServices SetIPsecServices() => SetIPsecServicesAsync().Result;

        /// <summary>VPN RPC: Set keep-alive function setting (Async mode)</summary>
        public async Task<VpnRpcKeep> SetKeepAsync() => await Call<VpnRpcKeep>("SetKeep", new VpnRpcKeep());

        /// <summary>VPN RPC: Set keep-alive function setting (Sync mode)</summary>
        public VpnRpcKeep SetKeep() => SetKeepAsync().Result;

        /// <summary>VPN RPC: Set link configuration (Async mode)</summary>
        public async Task<VpnRpcCreateLink> SetLinkAsync() => await Call<VpnRpcCreateLink>("SetLink", new VpnRpcCreateLink());

        /// <summary>VPN RPC: Set link configuration (Sync mode)</summary>
        public VpnRpcCreateLink SetLink() => SetLinkAsync().Result;

        /// <summary>VPN RPC: Make a link into off-line (Async mode)</summary>
        public async Task<VpnRpcLink> SetLinkOfflineAsync() => await Call<VpnRpcLink>("SetLinkOffline", new VpnRpcLink());

        /// <summary>VPN RPC: Make a link into off-line (Sync mode)</summary>
        public VpnRpcLink SetLinkOffline() => SetLinkOfflineAsync().Result;

        /// <summary>VPN RPC: Make a link into on-line (Async mode)</summary>
        public async Task<VpnRpcLink> SetLinkOnlineAsync() => await Call<VpnRpcLink>("SetLinkOnline", new VpnRpcLink());

        /// <summary>VPN RPC: Make a link into on-line (Sync mode)</summary>
        public VpnRpcLink SetLinkOnline() => SetLinkOnlineAsync().Result;

        /// <summary>VPN RPC: Set configurations for OpenVPN and SSTP (Async mode)</summary>
        public async Task<VpnOpenVpnSstpConfig> SetOpenVpnSstpConfigAsync() => await Call<VpnOpenVpnSstpConfig>("SetOpenVpnSstpConfig", new VpnOpenVpnSstpConfig());

        /// <summary>VPN RPC: Set configurations for OpenVPN and SSTP (Sync mode)</summary>
        public VpnOpenVpnSstpConfig SetOpenVpnSstpConfig() => SetOpenVpnSstpConfigAsync().Result;

        /// <summary>VPN RPC: Set SecureNAT options (Async mode)</summary>
        public async Task<VpnVhOption> SetSecureNATOptionAsync() => await Call<VpnVhOption>("SetSecureNATOption", new VpnVhOption());

        /// <summary>VPN RPC: Set SecureNAT options (Sync mode)</summary>
        public VpnVhOption SetSecureNATOption() => SetSecureNATOptionAsync().Result;

        /// <summary>VPN RPC: Set the server certification (Async mode)</summary>
        public async Task<VpnRpcKeyPair> SetServerCertAsync() => await Call<VpnRpcKeyPair>("SetServerCert", new VpnRpcKeyPair());

        /// <summary>VPN RPC: Set the server certification (Sync mode)</summary>
        public VpnRpcKeyPair SetServerCert() => SetServerCertAsync().Result;

        /// <summary>VPN RPC: Set cipher for SSL to the server (Async mode)</summary>
        public async Task<VpnRpcStr> SetServerCipherAsync() => await Call<VpnRpcStr>("SetServerCipher", new VpnRpcStr());

        /// <summary>VPN RPC: Set cipher for SSL to the server (Sync mode)</summary>
        public VpnRpcStr SetServerCipher() => SetServerCipherAsync().Result;

        /// <summary>VPN RPC: Set server password (Async mode)</summary>
        public async Task<VpnRpcSetPassword> SetServerPasswordAsync() => await Call<VpnRpcSetPassword>("SetServerPassword", new VpnRpcSetPassword());

        /// <summary>VPN RPC: Set server password (Sync mode)</summary>
        public VpnRpcSetPassword SetServerPassword() => SetServerPasswordAsync().Result;

        /// <summary>VPN RPC: Set special listener status (Async mode)</summary>
        public async Task<VpnRpcSpecialListener> SetSpecialListenerAsync() => await Call<VpnRpcSpecialListener>("SetSpecialListener", new VpnRpcSpecialListener());

        /// <summary>VPN RPC: Set special listener status (Sync mode)</summary>
        public VpnRpcSpecialListener SetSpecialListener() => SetSpecialListenerAsync().Result;

        /// <summary>VPN RPC: Set syslog function setting (Async mode)</summary>
        public async Task<VpnSyslogSetting> SetSysLogAsync() => await Call<VpnSyslogSetting>("SetSysLog", new VpnSyslogSetting());

        /// <summary>VPN RPC: Set syslog function setting (Sync mode)</summary>
        public VpnSyslogSetting SetSysLog() => SetSysLogAsync().Result;

        /// <summary>VPN RPC: Set user setting (Async mode)</summary>
        public async Task<VpnRpcSetUser> SetUserAsync() => await Call<VpnRpcSetUser>("SetUser", new VpnRpcSetUser());

        /// <summary>VPN RPC: Set user setting (Sync mode)</summary>
        public VpnRpcSetUser SetUser() => SetUserAsync().Result;

        /// <summary>VPN RPC: Setting VPN Gate Server Configuration (Async mode)</summary>
        public async Task<VpnVgsConfig> SetVgsConfigAsync() => await Call<VpnVgsConfig>("SetVgsConfig", new VpnVgsConfig());

        /// <summary>VPN RPC: Setting VPN Gate Server Configuration (Sync mode)</summary>
        public VpnVgsConfig SetVgsConfig() => SetVgsConfigAsync().Result;

        /// <summary>VPN RPC: Start a virtual layer-3 switch (Async mode)</summary>
        public async Task<VpnRpcL3Sw> StartL3SwitchAsync() => await Call<VpnRpcL3Sw>("StartL3Switch", new VpnRpcL3Sw());

        /// <summary>VPN RPC: Start a virtual layer-3 switch (Sync mode)</summary>
        public VpnRpcL3Sw StartL3Switch() => StartL3SwitchAsync().Result;

        /// <summary>VPN RPC: Stop a virtual layer-3 switch (Async mode)</summary>
        public async Task<VpnRpcL3Sw> StopL3SwitchAsync() => await Call<VpnRpcL3Sw>("StopL3Switch", new VpnRpcL3Sw());

        /// <summary>VPN RPC: Stop a virtual layer-3 switch (Sync mode)</summary>
        public VpnRpcL3Sw StopL3Switch() => StopL3SwitchAsync().Result;

        /// <summary>VPN RPC: test RPC function (Async mode)</summary>
        public async Task<VpnRpcTest> TestAsync() => await Call<VpnRpcTest>("Test", new VpnRpcTest());

        /// <summary>VPN RPC: test RPC function (Sync mode)</summary>
        public VpnRpcTest Test() => TestAsync().Result;

    }
}
