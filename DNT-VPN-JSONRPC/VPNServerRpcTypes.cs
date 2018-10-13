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
    /// <summary>
    /// IP Protocol Numbers
    /// </summary>
    public enum VpnIpProtocolNumber
    {
        /// <summary>
        /// ICMP for IPv4
        /// </summary>
        ICMPv4 = 1,

        /// <summary>
        ///  TCP
        /// </summary>
        TCP = 6,

        /// <summary>
        ///  UDP
        /// </summary>
        UDP = 17,

        /// <summary>
        /// ICMP for IPv6
        /// </summary>
        ICMPv6 = 58,
    }

    /// <summary>
    /// Access list
    /// </summary>
    public class VpnAccess
    {
        /// <summary>
        /// ID
        /// </summary>
        public uint Id_u32;

        /// <summary>
        /// Note
        /// </summary>
        public string Note_utf;

        /// <summary>
        /// Enable flag
        /// </summary>
        public bool Active_bool;

        /// <summary>
        /// Priority
        /// </summary>
        public uint Priority_u32;

        /// <summary>
        /// Discard flag
        /// </summary>
        public bool Discard_bool;

        /// <summary>
        /// Whether it's an IPv6
        /// </summary>
        public bool IsIPv6_bool;

        /// <summary>
        /// Source IP address (IPv4)
        /// </summary>
        public string SrcIpAddress_ip;

        /// <summary>
        /// Source subnet mask (IPv4)
        /// </summary>
        public string SrcSubnetMask_ip;

        /// <summary>
        /// Destination IP address (IPv4)
        /// </summary>
        public string DestIpAddress_ip;

        /// <summary>
        /// Destination subnet mask (IPv4)
        /// </summary>
        public string DestSubnetMask_ip;

        /// <summary>
        /// The source IP address (IPv6) (16 bytes, 128 bits)
        /// </summary>
        public byte[] SrcIpAddress6_bin;

        /// <summary>
        /// Source subnet mask (IPv6) (16 bytes, 128 bits)
        /// </summary>
        public byte[] SrcSubnetMask6_bin;

        /// <summary>
        /// Destination IP address (IPv6) (16 bytes, 128 bits)
        /// </summary>
        public byte[] DestIpAddress6_bin;

        /// <summary>
        /// Destination subnet mask (IPv6) (16 bytes, 128 bits)
        /// </summary>
        public byte[] DestSubnetMask6_bin;

        /// <summary>
        /// Protocol
        /// </summary>
        public VpnIpProtocolNumber Protocol_u32;

        /// <summary>
        /// Source port number starting point
        /// </summary>
        public uint SrcPortStart_u32;

        /// <summary>
        /// Source port number end point
        /// </summary>
        public uint SrcPortEnd_u32;

        /// <summary>
        /// Destination port number starting point
        /// </summary>
        public uint DestPortStart_u32;

        /// <summary>
        /// Destination port number end point
        /// </summary>
        public uint DestPortEnd_u32;

        /// <summary>
        /// Source user name
        /// </summary>
        public string SrcUsername_str;

        /// <summary>
        /// Destination user name
        /// </summary>
        public string DestUsername_str;

        /// <summary>
        /// Presence of a source MAC address setting
        /// </summary>
        public bool CheckSrcMac_bool;

        /// <summary>
        /// Source MAC address (6 bytes)
        /// </summary>
        public byte[] SrcMacAddress_bin;

        /// <summary>
        /// Source MAC address mask (6 bytes)
        /// </summary>
        public byte[] SrcMacMask_bin;

        /// <summary>
        /// Whether the setting of the destination MAC address exists
        /// </summary>
        public bool CheckDstMac_bool;

        /// <summary>
        /// Destination MAC address (6 bytes)
        /// </summary>
        public byte[] DstMacAddress_bin;

        /// <summary>
        /// Destination MAC address mask (6 bytes)
        /// </summary>
        public byte[] DstMacMask_bin;

        /// <summary>
        /// The state of the TCP connection
        /// </summary>
        public bool CheckTcpState_bool;

        /// <summary>
        /// Establieshed(TCP)
        /// </summary>
        public bool Established_bool;

        /// <summary>
        /// Delay
        /// </summary>
        public uint Delay_u32;

        /// <summary>
        /// Jitter
        /// </summary>
        public uint Jitter_u32;

        /// <summary>
        /// Packet loss
        /// </summary>
        public uint Loss_u32;

        /// <summary>
        /// URL to redirect to
        /// </summary>
        public string RedirectUrl_str;
    }

    /// <summary>
    /// Add to Access List
    /// </summary>
    public class VpnRpcAddAccess
    {
        /// <summary>
        /// HUB Name
        /// </summary>
        public string HubName_str;

        /// <summary>
        /// Access list (Must be a single item)
        /// </summary>
        public VpnAccess[] AccessListSingle;
    }

    /// <summary>
    /// Add CA to HUB *
    /// </summary>
    public class VpnRpcHubAddCA
    {
        /// <summary>
        /// HUB Name
        /// </summary>
        public string HubName_str;

        /// <summary>
        /// Certificate
        /// </summary>
        public byte[] Cert_bin;
    }

    /// <summary>
    /// CRL entry
    /// </summary>
    public class VpnRpcCrl
    {
        /// <summary>
        /// HUB Name
        /// </summary>
        public string HubName_str;

        /// <summary>
        /// Key
        /// </summary>
        public uint Key_u32;

        /// <summary>
        /// CRL body
        /// </summary>
        // TODO: CRL *Crl;
    }

    /// <summary>
    /// EtherIP key list entry
    /// </summary>
    public class VpnEtherIpId
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id_str;

        /// <summary>
        /// Virtual HUB name
        /// </summary>
        public string HubName_str;

        /// <summary>
        /// User name
        /// </summary>
        public string UserName_str;

        /// <summary>
        /// Password
        /// </summary>
        public string Password_str;
    }

    /// <summary>
    /// Layer-3 interface
    /// </summary>
    public class VpnRpcL3If
    {
        /// <summary>
        /// L3 switch name
        /// </summary>
        public string Name_str;

        /// <summary>
        /// Virtual HUB name
        /// </summary>
        public string HubName_str;

        /// <summary>
        /// IP address
        /// </summary>
        public uint IpAddress_u32;

        /// <summary>
        /// Subnet mask
        /// </summary>
        public uint SubnetMask_u32;
    }

    /// <summary>
    /// Layer-3 switch
    /// </summary>
    public class VpnRpcL3Sw
    {
        /// <summary>
        /// L3 switch name
        /// </summary>
        public string Name_str;
    }

    /// <summary>
    /// Routing table
    /// </summary>
    public class VpnRpcL3Table
    {
        /// <summary>
        /// L3 switch name
        /// </summary>
        public string Name_str;

        /// <summary>
        /// Network address
        /// </summary>
        public uint NetworkAddress_u32;

        /// <summary>
        /// Subnet mask
        /// </summary>
        public uint SubnetMask_u32;

        /// <summary>
        /// Gateway address
        /// </summary>
        public uint GatewayAddress_u32;

        /// <summary>
        /// Metric
        /// </summary>
        public uint Metric_u32;
    }

    /// <summary>
    /// Test
    /// </summary>
    public class VpnRpcTest
    {
        public uint IntValue_u32;

        public ulong Int64Value_u64;

        public string StrValue_str;

        public string UniStrValue_utf;
    }

    /// <summary>
    /// Bridge item
    /// </summary>
    public class VpnRpcLocalBridge
    {
        /// <summary>
        /// Device name
        /// </summary>
        public string DeviceName_str;

        /// <summary>
        /// HUB Name
        /// </summary>
        public string HubName_str;

        /// <summary>
        /// Online flag
        /// </summary>
        public bool Online_bool;

        /// <summary>
        /// Running flag
        /// </summary>
        public bool Active_bool;

        /// <summary>
        /// Tap mode
        /// </summary>
        public bool TapMode_bool;
    }

    /// <summary>
    /// Create, configure, and get the group *
    /// </summary>
    public class VpnRpcSetGroup
    {
        /// <summary>
        /// HUB Name
        /// </summary>
        public string HubName_str;

        /// <summary>
        /// User name
        /// </summary>
        public string Name_str;

        /// <summary>
        /// Real name
        /// </summary>
        public string Realname_utf;

        /// <summary>
        /// Note
        /// </summary>
        public string Note_utf;

        /// <summary>
        /// Number of broadcast packets (Recv)
        /// </summary>
        [JsonProperty("Recv.BroadcastBytes_u64")]
        public ulong Recv_BroadcastBytes_u64;

        /// <summary>
        /// Broadcast bytes (Recv)
        /// </summary>
        [JsonProperty("Recv.BroadcastCount_u64")]
        public ulong Recv_BroadcastCount_u64;

        /// <summary>
        /// Unicast count (Recv)
        /// </summary>
        [JsonProperty("Recv.UnicastBytes_u64")]
        public ulong Recv_UnicastBytes_u64;

        /// <summary>
        /// Unicast bytes (Recv)
        /// </summary>
        [JsonProperty("Recv.UnicastCount_u64")]
        public ulong Recv_UnicastCount_u64;

        /// <summary>
        /// Number of broadcast packets (Send)
        /// </summary>
        [JsonProperty("Send.BroadcastBytes_u64")]
        public ulong Send_BroadcastBytes_u64;

        /// <summary>
        /// Broadcast bytes (Send)
        /// </summary>
        [JsonProperty("Send.BroadcastCount_u64")]
        public ulong Send_BroadcastCount_u64;

        /// <summary>
        /// Unicast bytes (Send)
        /// </summary>
        [JsonProperty("Send.UnicastBytes_u64")]
        public ulong Send_UnicastBytes_u64;

        /// <summary>
        /// Unicast bytes (Send)
        /// </summary>
        [JsonProperty("Send.UnicastCount_u64")]
        public ulong Send_UnicastCount_u64;

        /// <summary>
        /// The flag whether to use security policy
        /// </summary>
        public bool UsePolicy_bool;

        // ---- Start of Security policy ---
        /// <summary>
        /// Security policy: Grant access
        /// </summary>
        [JsonProperty("policy:Access_bool")]
        public bool SecPol_Access_bool;

        /// <summary>
        /// Security policy: Filter DHCP packets (IPv4)
        /// </summary>
        [JsonProperty("policy:DHCPFilter_bool")]
        public bool SecPol_DHCPFilter_bool;

        /// <summary>
        /// Security policy: Prohibit the behavior of the DHCP server (IPv4)
        /// </summary>
        [JsonProperty("policy:DHCPNoServer_bool")]
        public bool SecPol_DHCPNoServer_bool;

        /// <summary>
        /// Security policy: Force DHCP-assigned IP address (IPv4)
        /// </summary>
        [JsonProperty("policy:DHCPForce_bool")]
        public bool SecPol_DHCPForce_bool;

        /// <summary>
        /// Security policy: Prohibit the bridge behavior
        /// </summary>
        [JsonProperty("policy:NoBridge_bool")]
        public bool SecPol_NoBridge_bool;

        /// <summary>
        /// Security policy: Prohibit the router behavior (IPv4)
        /// </summary>
        [JsonProperty("policy:NoRouting_bool")]
        public bool SecPol_NoRouting_bool;

        /// <summary>
        /// Security policy: Prohibit the duplicate MAC address
        /// </summary>
        [JsonProperty("policy:CheckMac_bool")]
        public bool SecPol_CheckMac_bool;

        /// <summary>
        /// Security policy: Prohibit a duplicate IP address (IPv4)
        /// </summary>
        [JsonProperty("policy:CheckIP_bool")]
        public bool SecPol_CheckIP_bool;

        /// <summary>
        /// Security policy: Prohibit the broadcast other than ARP, DHCP, ICMPv6
        /// </summary>
        [JsonProperty("policy:ArpDhcpOnly_bool")]
        public bool SecPol_ArpDhcpOnly_bool;

        /// <summary>
        /// Security policy: Privacy filter mode
        /// </summary>
        [JsonProperty("policy:PrivacyFilter_bool")]
        public bool SecPol_PrivacyFilter_bool;

        /// <summary>
        /// Security policy: Prohibit to operate as a TCP/IP server (IPv4)
        /// </summary>
        [JsonProperty("policy:NoServer_bool")]
        public bool SecPol_NoServer_bool;

        /// <summary>
        /// Security policy: Not to limit the number of broadcast
        /// </summary>
        [JsonProperty("policy:NoBroadcastLimiter_bool")]
        public bool SecPol_NoBroadcastLimiter_bool;

        /// <summary>
        /// Security policy: Allow monitoring mode
        /// </summary>
        [JsonProperty("policy:MonitorPort_bool")]
        public bool SecPol_MonitorPort_bool;

        /// <summary>
        /// Security policy: Maximum number of TCP connections
        /// </summary>
        [JsonProperty("policy:MaxConnection_u32")]
        public uint SecPol_MaxConnection_u32;

        /// <summary>
        /// Security policy: Communication time-out period
        /// </summary>
        [JsonProperty("policy:TimeOut_u32")]
        public uint SecPol_TimeOut_u32;

        /// <summary>
        /// Security policy: Maximum number of MAC address
        /// </summary>
        [JsonProperty("policy:MaxMac_u32")]
        public uint SecPol_MaxMac_u32;

        /// <summary>
        /// Security policy: Maximum number of IP address (IPv4)
        /// </summary>
        [JsonProperty("policy:MaxIP_u32")]
        public uint SecPol_MaxIP_u32;

        /// <summary>
        /// Security policy: Upload bandwidth
        /// </summary>
        [JsonProperty("policy:MaxUpload_u32")]
        public uint SecPol_MaxUpload_u32;

        /// <summary>
        /// Security policy: Download bandwidth
        /// </summary>
        [JsonProperty("policy:MaxDownload_u32")]
        public uint SecPol_MaxDownload_u32;

        /// <summary>
        /// Security policy: User can not change password
        /// </summary>
        [JsonProperty("policy:FixPassword_bool")]
        public bool SecPol_FixPassword_bool;

        /// <summary>
        /// Security policy: Multiple logins limit
        /// </summary>
        [JsonProperty("policy:MultiLogins_u32")]
        public uint SecPol_MultiLogins_u32;

        /// <summary>
        /// Security policy: Prohibit the use of VoIP / QoS features
        /// </summary>
        [JsonProperty("policy:NoQoS_bool")]
        public bool SecPol_NoQoS_bool;

        /// <summary>
        /// Security policy: Filter the Router Solicitation / Advertising packet (IPv6)
        /// </summary>
        [JsonProperty("policy:RSandRAFilter_bool")]
        public bool SecPol_RSandRAFilter_bool;

        /// <summary>
        /// Security policy: Filter the router advertisement packet (IPv6)
        /// </summary>
        [JsonProperty("policy:RAFilter_bool")]
        public bool SecPol_RAFilter_bool;

        /// <summary>
        /// Security policy: Filter DHCP packets (IPv6)
        /// </summary>
        [JsonProperty("policy:DHCPv6Filter_bool")]
        public bool SecPol_DHCPv6Filter_bool;

        /// <summary>
        /// Security policy: Prohibit the behavior of the DHCP server (IPv6)
        /// </summary>
        [JsonProperty("policy:DHCPv6NoServer_bool")]
        public bool SecPol_DHCPv6NoServer_bool;

        /// <summary>
        /// Security policy: Prohibit the router behavior (IPv6)
        /// </summary>
        [JsonProperty("policy:NoRoutingV6_bool")]
        public bool SecPol_NoRoutingV6_bool;

        /// <summary>
        /// Security policy: Prohibit the duplicate IP address (IPv6)
        /// </summary>
        [JsonProperty("policy:CheckIPv6_bool")]
        public bool SecPol_CheckIPv6_bool;

        /// <summary>
        /// Security policy: Prohibit to operate as a TCP/IP server (IPv6)
        /// </summary>
        [JsonProperty("policy:NoServerV6_bool")]
        public bool SecPol_NoServerV6_bool;

        /// <summary>
        /// Security policy: Maximum number of IP address (IPv6)
        /// </summary>
        [JsonProperty("policy:MaxIPv6_u32")]
        public uint SecPol_MaxIPv6_u32;

        /// <summary>
        /// Security policy: Prohibit to save the password in the VPN Client
        /// </summary>
        [JsonProperty("policy:NoSavePassword_bool")]
        public bool SecPol_NoSavePassword_bool;

        /// <summary>
        /// Security policy: Disconnect the VPN Client automatically at a certain period of time
        /// </summary>
        [JsonProperty("policy:AutoDisconnect_u32")]
        public uint SecPol_AutoDisconnect_u32;

        /// <summary>
        /// Security policy: Filter all IPv4 packets
        /// </summary>
        [JsonProperty("policy:FilterIPv4_bool")]
        public bool SecPol_FilterIPv4_bool;

        /// <summary>
        /// Security policy: Filter all IPv6 packets
        /// </summary>
        [JsonProperty("policy:FilterIPv6_bool")]
        public bool SecPol_FilterIPv6_bool;

        /// <summary>
        /// Security policy: Filter all non-IP packets
        /// </summary>
        [JsonProperty("policy:FilterNonIP_bool")]
        public bool SecPol_FilterNonIP_bool;

        /// <summary>
        /// Security policy: Delete the default router specification from the IPv6 router advertisement
        /// </summary>
        [JsonProperty("policy:NoIPv6DefaultRouterInRA_bool")]
        public bool SecPol_NoIPv6DefaultRouterInRA_bool;

        /// <summary>
        /// Security policy: Delete the default router specification from the IPv6 router advertisement (Enable IPv6 connection)
        /// </summary>
        [JsonProperty("policy:NoIPv6DefaultRouterInRAWhenIPv6_bool")]
        public bool SecPol_NoIPv6DefaultRouterInRAWhenIPv6_bool;

        /// <summary>
        /// Security policy: Specify the VLAN ID
        /// </summary>
        [JsonProperty("policy:VLanId_u32")]
        public uint SecPol_VLanId_u32;

        /// <summary>
        /// Security policy: Whether version 3.0
        /// </summary>
        [JsonProperty("policy:Ver3_bool")]
        public bool SecPol_Ver3_bool = true;
        // ---- End of Security policy ---
    }

    /// <summary>
    /// Hub types
    /// </summary>
    public enum VpnRpcHubType
    {
        /// <summary>
        /// Stand-alone HUB
        /// </summary>
        Standalone = 0,

        /// <summary>
        /// Static HUB
        /// </summary>
        FarmStatic = 1,

        /// <summary>
        /// Dynamic HUB
        /// </summary>
        FarmDynamic = 2,
    }

    /// <summary>
    /// Create a HUB
    /// </summary>
    public class VpnRpcCreateHub
    {
        /// <summary>
        /// HUB Name
        /// </summary>
        public string HubName_str;

        /// <summary>
        /// Administrative password
        /// </summary>
        public string AdminPasswordPlainText_str;

        /// <summary>
        /// Online flag
        /// </summary>
        public bool Online_bool;

        /// <summary>
        /// Maximum number of sessions
        /// </summary>
        public uint MaxSession_u32;

        /// <summary>
        /// Not listed
        /// </summary>
        public bool NoEnum_bool;

        /// <summary>
        /// Type of HUB
        /// </summary>
        public VpnRpcHubType HubType_u32;
    }

    public enum VpnRpcClientAuthType
    {
        /// <summary>
        /// Anonymous authentication
        /// </summary>
        Anonymous = 0,

        /// <summary>
        /// SHA-0 hashed password authentication
        /// </summary>
        SHA0_Hashed_Password = 1,

        /// <summary>
        /// Plain password authentication
        /// </summary>
        PlainPassword = 2,

        /// <summary>
        /// Certificate authentication
        /// </summary>
        Cert = 3,

        /// <summary>
        /// Security token devices authentication
        /// </summary>
        Secure = 4,
    }

    /// <summary>
    /// Create and set of link *
    /// </summary>
    public class VpnRpcCreateLink
    {
        /// <summary>
        /// HUB Name
        /// </summary>
        public string HubName_Ex_str;

        /// <summary>
        /// Online flag
        /// </summary>
        public bool Online_bool;

        /// <summary>
        /// Validate the server certificate
        /// </summary>
        public bool CheckServerCert_bool;

        /// <summary>
        /// Server certificate
        /// </summary>
        public byte[] ServerCert_bin;

        // ---- Start of Client Option Parameters ---
        /// <summary>
        /// Client Option Parameters: Connection setting name
        /// </summary>
        [JsonProperty("AccountName_utf")]
        public string ClientOption_AccountName_utf;

        /// <summary>
        /// Client Option Parameters: Host name
        /// </summary>
        [JsonProperty("Hostname_str")]
        public string ClientOption_Hostname_str;

        /// <summary>
        /// Client Option Parameters: Port number
        /// </summary>
        [JsonProperty("Port_u32")]
        public uint ClientOption_Port_u32;

        /// <summary>
        /// Client Option Parameters: Type of proxy
        /// </summary>
        [JsonProperty("ProxyType_u32")]
        public uint ClientOption_ProxyType_u32;

        /// <summary>
        /// Client Option Parameters: Proxy server name
        /// </summary>
        [JsonProperty("ProxyName_str")]
        public string ClientOption_ProxyName_str;

        /// <summary>
        /// Client Option Parameters: Port number of the proxy server
        /// </summary>
        [JsonProperty("ProxyPort_u32")]
        public uint ClientOption_ProxyPort_u32;

        /// <summary>
        /// Client Option Parameters: Maximum user name length
        /// </summary>
        [JsonProperty("ProxyUsername_str")]
        public string ClientOption_ProxyUsername_str;

        /// <summary>
        /// Client Option Parameters: Maximum password length
        /// </summary>
        [JsonProperty("ProxyPassword_str")]
        public string ClientOption_ProxyPassword_str;

        /// <summary>
        /// Client Option Parameters: Automatic retries
        /// </summary>
        [JsonProperty("NumRetry_u32")]
        public uint ClientOption_NumRetry_u32;

        /// <summary>
        /// Client Option Parameters: Retry interval
        /// </summary>
        [JsonProperty("RetryInterval_u32")]
        public uint ClientOption_RetryInterval_u32;

        /// <summary>
        /// Client Option Parameters: HUB name
        /// </summary>
        [JsonProperty("HubName_str")]
        public string ClientOption_HubName_str;

        /// <summary>
        /// Client Option Parameters: Maximum number of concurrent TCP connections
        /// </summary>
        [JsonProperty("MaxConnection_u32")]
        public uint ClientOption_MaxConnection_u32;

        /// <summary>
        /// Client Option Parameters: Use encrypted communication
        /// </summary>
        [JsonProperty("UseEncrypt_bool")]
        public bool ClientOption_UseEncrypt_bool;

        /// <summary>
        /// Client Option Parameters: Use data compression
        /// </summary>
        [JsonProperty("UseCompress_bool")]
        public bool ClientOption_UseCompress_bool;

        /// <summary>
        /// Client Option Parameters: Use half connection in TCP
        /// </summary>
        [JsonProperty("HalfConnection_bool")]
        public bool ClientOption_HalfConnection_bool;

        /// <summary>
        /// Client Option Parameters: Disable the routing tracking
        /// </summary>
        //[JsonProperty("NoRoutingTracking_bool")]
        //public bool ClientOption_NoRoutingTracking_bool;

        /// <summary>
        /// Client Option Parameters: VLAN device name
        /// </summary>
        //public string DeviceName_str;

        /// <summary>
        /// Client Option Parameters: Connection attempt interval when additional connection establish
        /// </summary>
        [JsonProperty("AdditionalConnectionInterval_u32")]
        public uint ClientOption_AdditionalConnectionInterval_u32;

        /// <summary>
        /// Client Option Parameters: Disconnection interval
        /// </summary>
        [JsonProperty("ConnectionDisconnectSpan_u32")]
        public uint ClientOption_ConnectionDisconnectSpan_u32;

        /// <summary>
        /// Client Option Parameters: Hide the status window
        /// </summary>
        //[JsonProperty("HideStatusWindow_bool")]
        //public bool ClientOption_HideStatusWindow_bool;

        /// <summary>
        /// Client Option Parameters: Hide the NIC status window
        /// </summary>
        //[JsonProperty("HideNicInfoWindow_bool")]
        //public bool ClientOption_HideNicInfoWindow_bool;

        /// <summary>
        /// Client Option Parameters: Monitor port mode
        /// </summary>
        //[JsonProperty("RequireMonitorMode_bool")]
        //public bool ClientOption_RequireMonitorMode_bool;

        /// <summary>
        /// Client Option Parameters: Bridge or routing mode
        /// </summary>
        //[JsonProperty("RequireBridgeRoutingMode_bool")]
        //public bool ClientOption_RequireBridgeRoutingMode_bool;

        /// <summary>
        /// Client Option Parameters: Disable the VoIP / QoS function
        /// </summary>
        [JsonProperty("DisableQoS_bool")]
        public bool ClientOption_DisableQoS_bool;

        /// <summary>
        /// Client Option Parameters: Do not use TLS 1.x
        /// </summary>
        [JsonProperty("NoTls1_bool")]
        public bool ClientOption_NoTls1_bool;

        /// <summary>
        /// Client Option Parameters: Do not use UDP acceleration mode
        /// </summary>
        [JsonProperty("NoUdpAcceleration_bool")]
        public bool ClientOption_NoUdpAcceleration_bool;
        // ---- End of Client Option ---

        // ---- Start of Client Auth Parameters ---
        /// <summary>
        /// Authentication type
        /// </summary>
        [JsonProperty("AuthType_u32")]
        public VpnRpcClientAuthType ClientAuth_AuthType_u32;

        /// <summary>
        /// User name
        /// </summary>
        [JsonProperty("Username_str")]
        public string ClientAuth_Username_str;

        /// <summary>
        /// SHA-0 Hashed password
        /// </summary>
        [JsonProperty("HashedPassword_bin")]
        public byte[] ClientAuth_HashedPassword_bin;

        /// <summary>
        /// Plaintext Password
        /// </summary>
        [JsonProperty("PlainPassword_str")]
        public string ClientAuth_PlainPassword_str;

        /// <summary>
        /// Client certificate
        /// </summary>
        [JsonProperty("ClientX_bin")]
        public byte[] ClientAuth_ClientX_bin;

        /// <summary>
        /// Client private key
        /// </summary>
        [JsonProperty("ClientK_bin")]
        public byte[] ClientAuth_ClientK_bin;

        /// <summary>
        /// Secure device certificate name
        /// </summary>
        //public string SecurePublicCertName_str;

        /// <summary>
        /// Secure device secret key name
        /// </summary>
        //public string SecurePrivateKeyName_str;

        // ---- End of Client Auth Parameters ---

        // ---- Start of Security policy ---
        /// <summary>
        /// Security policy: Grant access
        /// </summary>
        //[JsonProperty("policy:Access_bool")]
        //public bool SecPol_Access_bool;

        /// <summary>
        /// Security policy: Filter DHCP packets (IPv4)
        /// </summary>
        [JsonProperty("policy:DHCPFilter_bool")]
        public bool SecPol_DHCPFilter_bool;

        /// <summary>
        /// Security policy: Prohibit the behavior of the DHCP server (IPv4)
        /// </summary>
        [JsonProperty("policy:DHCPNoServer_bool")]
        public bool SecPol_DHCPNoServer_bool;

        /// <summary>
        /// Security policy: Force DHCP-assigned IP address (IPv4)
        /// </summary>
        [JsonProperty("policy:DHCPForce_bool")]
        public bool SecPol_DHCPForce_bool;

        /// <summary>
        /// Security policy: Prohibit the bridge behavior
        /// </summary>
        //[JsonProperty("policy:NoBridge_bool")]
        //public bool SecPol_NoBridge_bool;

        /// <summary>
        /// Security policy: Prohibit the router behavior (IPv4)
        /// </summary>
        //[JsonProperty("policy:NoRouting_bool")]
        //public bool SecPol_NoRouting_bool;

        /// <summary>
        /// Security policy: Prohibit the duplicate MAC address
        /// </summary>
        [JsonProperty("policy:CheckMac_bool")]
        public bool SecPol_CheckMac_bool;

        /// <summary>
        /// Security policy: Prohibit a duplicate IP address (IPv4)
        /// </summary>
        [JsonProperty("policy:CheckIP_bool")]
        public bool SecPol_CheckIP_bool;

        /// <summary>
        /// Security policy: Prohibit the broadcast other than ARP, DHCP, ICMPv6
        /// </summary>
        [JsonProperty("policy:ArpDhcpOnly_bool")]
        public bool SecPol_ArpDhcpOnly_bool;

        /// <summary>
        /// Security policy: Privacy filter mode
        /// </summary>
        [JsonProperty("policy:PrivacyFilter_bool")]
        public bool SecPol_PrivacyFilter_bool;

        /// <summary>
        /// Security policy: Prohibit to operate as a TCP/IP server (IPv4)
        /// </summary>
        [JsonProperty("policy:NoServer_bool")]
        public bool SecPol_NoServer_bool;

        /// <summary>
        /// Security policy: Not to limit the number of broadcast
        /// </summary>
        [JsonProperty("policy:NoBroadcastLimiter_bool")]
        public bool SecPol_NoBroadcastLimiter_bool;

        /// <summary>
        /// Security policy: Allow monitoring mode
        /// </summary>
        //[JsonProperty("policy:MonitorPort_bool")]
        //public bool SecPol_MonitorPort_bool;

        /// <summary>
        /// Security policy: Maximum number of TCP connections
        /// </summary>
        //[JsonProperty("policy:MaxConnection_u32")]
        //public uint SecPol_MaxConnection_u32;

        /// <summary>
        /// Security policy: Communication time-out period
        /// </summary>
        //[JsonProperty("policy:TimeOut_u32")]
        //public uint SecPol_TimeOut_u32;

        /// <summary>
        /// Security policy: Maximum number of MAC address
        /// </summary>
        [JsonProperty("policy:MaxMac_u32")]
        public uint SecPol_MaxMac_u32;

        /// <summary>
        /// Security policy: Maximum number of IP address (IPv4)
        /// </summary>
        [JsonProperty("policy:MaxIP_u32")]
        public uint SecPol_MaxIP_u32;

        /// <summary>
        /// Security policy: Upload bandwidth
        /// </summary>
        [JsonProperty("policy:MaxUpload_u32")]
        public uint SecPol_MaxUpload_u32;

        /// <summary>
        /// Security policy: Download bandwidth
        /// </summary>
        [JsonProperty("policy:MaxDownload_u32")]
        public uint SecPol_MaxDownload_u32;

        /// <summary>
        /// Security policy: User can not change password
        /// </summary>
        //[JsonProperty("policy:FixPassword_bool")]
        //public bool SecPol_FixPassword_bool;

        /// <summary>
        /// Security policy: Multiple logins limit
        /// </summary>
        //[JsonProperty("policy:MultiLogins_u32")]
        //public uint SecPol_MultiLogins_u32;

        /// <summary>
        /// Security policy: Prohibit the use of VoIP / QoS features
        /// </summary>
        //[JsonProperty("policy:NoQoS_bool")]
        //public bool SecPol_NoQoS_bool;

        /// <summary>
        /// Security policy: Filter the Router Solicitation / Advertising packet (IPv6)
        /// </summary>
        [JsonProperty("policy:RSandRAFilter_bool")]
        public bool SecPol_RSandRAFilter_bool;

        /// <summary>
        /// Security policy: Filter the router advertisement packet (IPv6)
        /// </summary>
        [JsonProperty("policy:RAFilter_bool")]
        public bool SecPol_RAFilter_bool;

        /// <summary>
        /// Security policy: Filter DHCP packets (IPv6)
        /// </summary>
        [JsonProperty("policy:DHCPv6Filter_bool")]
        public bool SecPol_DHCPv6Filter_bool;

        /// <summary>
        /// Security policy: Prohibit the behavior of the DHCP server (IPv6)
        /// </summary>
        [JsonProperty("policy:DHCPv6NoServer_bool")]
        public bool SecPol_DHCPv6NoServer_bool;

        /// <summary>
        /// Security policy: Prohibit the router behavior (IPv6)
        /// </summary>
        //[JsonProperty("policy:NoRoutingV6_bool")]
        //public bool SecPol_NoRoutingV6_bool;

        /// <summary>
        /// Security policy: Prohibit the duplicate IP address (IPv6)
        /// </summary>
        [JsonProperty("policy:CheckIPv6_bool")]
        public bool SecPol_CheckIPv6_bool;

        /// <summary>
        /// Security policy: Prohibit to operate as a TCP/IP server (IPv6)
        /// </summary>
        [JsonProperty("policy:NoServerV6_bool")]
        public bool SecPol_NoServerV6_bool;

        /// <summary>
        /// Security policy: Maximum number of IP address (IPv6)
        /// </summary>
        [JsonProperty("policy:MaxIPv6_u32")]
        public uint SecPol_MaxIPv6_u32;

        /// <summary>
        /// Security policy: Prohibit to save the password in the VPN Client
        /// </summary>
        //[JsonProperty("policy:NoSavePassword_bool")]
        //public bool SecPol_NoSavePassword_bool;

        /// <summary>
        /// Security policy: Disconnect the VPN Client automatically at a certain period of time
        /// </summary>
        //[JsonProperty("policy:AutoDisconnect_u32")]
        //public uint SecPol_AutoDisconnect_u32;

        /// <summary>
        /// Security policy: Filter all IPv4 packets
        /// </summary>
        [JsonProperty("policy:FilterIPv4_bool")]
        public bool SecPol_FilterIPv4_bool;

        /// <summary>
        /// Security policy: Filter all IPv6 packets
        /// </summary>
        [JsonProperty("policy:FilterIPv6_bool")]
        public bool SecPol_FilterIPv6_bool;

        /// <summary>
        /// Security policy: Filter all non-IP packets
        /// </summary>
        [JsonProperty("policy:FilterNonIP_bool")]
        public bool SecPol_FilterNonIP_bool;

        /// <summary>
        /// Security policy: Delete the default router specification from the IPv6 router advertisement
        /// </summary>
        [JsonProperty("policy:NoIPv6DefaultRouterInRA_bool")]
        public bool SecPol_NoIPv6DefaultRouterInRA_bool;

        /// <summary>
        /// Security policy: Delete the default router specification from the IPv6 router advertisement (Enable IPv6 connection)
        /// </summary>
        //[JsonProperty("policy:NoIPv6DefaultRouterInRAWhenIPv6_bool")]
        //public bool SecPol_NoIPv6DefaultRouterInRAWhenIPv6_bool;

        /// <summary>
        /// Security policy: Specify the VLAN ID
        /// </summary>
        [JsonProperty("policy:VLanId_u32")]
        public uint SecPol_VLanId_u32;

        /// <summary>
        /// Security policy: Whether version 3.0
        /// </summary>
        [JsonProperty("policy:Ver3_bool")]
        public bool SecPol_Ver3_bool = true;
        // ---- End of Security policy ---
    }

    /// <summary>
    /// Listener
    /// </summary>
    public class VpnRpcListener
    {
        /// <summary>
        /// Port number
        /// </summary>
        public uint Port_u32;

        /// <summary>
        /// Active state
        /// </summary>
        public bool Enable_bool;
    }

    /// <summary>
    /// User authentication type (server side)
    /// </summary>
    public enum VpnRpcUserAuthType
    {
        /// <summary>
        /// Anonymous authentication
        /// </summary>
        Anonymous = 0,

        /// <summary>
        /// Password authentication
        /// </summary>
        Password = 1,

        /// <summary>
        /// User certificate authentication
        /// </summary>
        UserCert = 2,

        /// <summary>
        /// Root certificate which is issued by trusted Certificate Authority
        /// </summary>
        RootCert = 3,

        /// <summary>
        /// Radius authentication
        /// </summary>
        Radius = 4,

        /// <summary>
        /// Windows NT authentication
        /// </summary>
        NTDomain = 5,
    }

    /// <summary>
    /// Create, configure, and get the user *
    /// </summary>
    public class VpnRpcSetUser
    {
        /// <summary>
        /// HUB Name
        /// </summary>
        public string HubName_str;

        /// <summary>
        /// User name
        /// </summary>
        public string Name_str;

        /// <summary>
        /// Group name
        /// </summary>
        public string GroupName_str;

        /// <summary>
        /// Real name
        /// </summary>
        public string Realname_utf;

        /// <summary>
        /// Note
        /// </summary>
        public string Note_utf;

        /// <summary>
        /// Creation date and time
        /// </summary>
        public DateTime CreatedTime_dt;

        /// <summary>
        /// Updating date
        /// </summary>
        public DateTime UpdatedTime_dt;

        /// <summary>
        /// Expiration date
        /// </summary>
        public DateTime ExpireTime_dt;

        /// <summary>
        /// Authentication method
        /// </summary>
        public VpnRpcUserAuthType AuthType_u32;

        /// <summary>
        /// User password
        /// </summary>
        public string Auth_Password_str;

        /// <summary>
        /// User certificate
        /// </summary>
        [JsonProperty("UserX_bin")]
        public byte[] Auth_UserCert_CertData;

        /// <summary>
        /// Certificate Serial Number
        /// </summary>
        [JsonProperty("Serial_bin")]
        public byte[] Auth_RootCert_Serial;

        /// <summary>
        /// Certificate Common Name
        /// </summary>
        [JsonProperty("CommonName_utf")]
        public string Auth_RootCert_CommonName;

        /// <summary>
        /// Username in RADIUS server
        /// </summary>
        [JsonProperty("RadiusUsername_utf")]
        public string Auth_Radius_RadiusUsername;

        /// <summary>
        /// Username in NT Domain server
        /// </summary>
        [JsonProperty("NtUsername_utf")]
        public string Auth_NT_NTUsername;

        /// <summary>
        /// Number of logins
        /// </summary>
        public uint NumLogin_u32;

        /// <summary>
        /// Number of broadcast packets (Recv)
        /// </summary>
        [JsonProperty("Recv.BroadcastBytes_u64")]
        public ulong Recv_BroadcastBytes_u64;

        /// <summary>
        /// Broadcast bytes (Recv)
        /// </summary>
        [JsonProperty("Recv.BroadcastCount_u64")]
        public ulong Recv_BroadcastCount_u64;

        /// <summary>
        /// Unicast count (Recv)
        /// </summary>
        [JsonProperty("Recv.UnicastBytes_u64")]
        public ulong Recv_UnicastBytes_u64;

        /// <summary>
        /// Unicast bytes (Recv)
        /// </summary>
        [JsonProperty("Recv.UnicastCount_u64")]
        public ulong Recv_UnicastCount_u64;

        /// <summary>
        /// Number of broadcast packets (Send)
        /// </summary>
        [JsonProperty("Send.BroadcastBytes_u64")]
        public ulong Send_BroadcastBytes_u64;

        /// <summary>
        /// Broadcast bytes (Send)
        /// </summary>
        [JsonProperty("Send.BroadcastCount_u64")]
        public ulong Send_BroadcastCount_u64;

        /// <summary>
        /// Unicast bytes (Send)
        /// </summary>
        [JsonProperty("Send.UnicastBytes_u64")]
        public ulong Send_UnicastBytes_u64;

        /// <summary>
        /// Unicast bytes (Send)
        /// </summary>
        [JsonProperty("Send.UnicastCount_u64")]
        public ulong Send_UnicastCount_u64;

        /// <summary>
        /// The flag whether to use security policy
        /// </summary>
        public bool UsePolicy_bool;

        // ---- Start of Security policy ---
        /// <summary>
        /// Security policy: Grant access
        /// </summary>
        [JsonProperty("policy:Access_bool")]
        public bool SecPol_Access_bool;

        /// <summary>
        /// Security policy: Filter DHCP packets (IPv4)
        /// </summary>
        [JsonProperty("policy:DHCPFilter_bool")]
        public bool SecPol_DHCPFilter_bool;

        /// <summary>
        /// Security policy: Prohibit the behavior of the DHCP server (IPv4)
        /// </summary>
        [JsonProperty("policy:DHCPNoServer_bool")]
        public bool SecPol_DHCPNoServer_bool;

        /// <summary>
        /// Security policy: Force DHCP-assigned IP address (IPv4)
        /// </summary>
        [JsonProperty("policy:DHCPForce_bool")]
        public bool SecPol_DHCPForce_bool;

        /// <summary>
        /// Security policy: Prohibit the bridge behavior
        /// </summary>
        [JsonProperty("policy:NoBridge_bool")]
        public bool SecPol_NoBridge_bool;

        /// <summary>
        /// Security policy: Prohibit the router behavior (IPv4)
        /// </summary>
        [JsonProperty("policy:NoRouting_bool")]
        public bool SecPol_NoRouting_bool;

        /// <summary>
        /// Security policy: Prohibit the duplicate MAC address
        /// </summary>
        [JsonProperty("policy:CheckMac_bool")]
        public bool SecPol_CheckMac_bool;

        /// <summary>
        /// Security policy: Prohibit a duplicate IP address (IPv4)
        /// </summary>
        [JsonProperty("policy:CheckIP_bool")]
        public bool SecPol_CheckIP_bool;

        /// <summary>
        /// Security policy: Prohibit the broadcast other than ARP, DHCP, ICMPv6
        /// </summary>
        [JsonProperty("policy:ArpDhcpOnly_bool")]
        public bool SecPol_ArpDhcpOnly_bool;

        /// <summary>
        /// Security policy: Privacy filter mode
        /// </summary>
        [JsonProperty("policy:PrivacyFilter_bool")]
        public bool SecPol_PrivacyFilter_bool;

        /// <summary>
        /// Security policy: Prohibit to operate as a TCP/IP server (IPv4)
        /// </summary>
        [JsonProperty("policy:NoServer_bool")]
        public bool SecPol_NoServer_bool;

        /// <summary>
        /// Security policy: Not to limit the number of broadcast
        /// </summary>
        [JsonProperty("policy:NoBroadcastLimiter_bool")]
        public bool SecPol_NoBroadcastLimiter_bool;

        /// <summary>
        /// Security policy: Allow monitoring mode
        /// </summary>
        [JsonProperty("policy:MonitorPort_bool")]
        public bool SecPol_MonitorPort_bool;

        /// <summary>
        /// Security policy: Maximum number of TCP connections
        /// </summary>
        [JsonProperty("policy:MaxConnection_u32")]
        public uint SecPol_MaxConnection_u32;

        /// <summary>
        /// Security policy: Communication time-out period
        /// </summary>
        [JsonProperty("policy:TimeOut_u32")]
        public uint SecPol_TimeOut_u32;

        /// <summary>
        /// Security policy: Maximum number of MAC address
        /// </summary>
        [JsonProperty("policy:MaxMac_u32")]
        public uint SecPol_MaxMac_u32;

        /// <summary>
        /// Security policy: Maximum number of IP address (IPv4)
        /// </summary>
        [JsonProperty("policy:MaxIP_u32")]
        public uint SecPol_MaxIP_u32;

        /// <summary>
        /// Security policy: Upload bandwidth
        /// </summary>
        [JsonProperty("policy:MaxUpload_u32")]
        public uint SecPol_MaxUpload_u32;

        /// <summary>
        /// Security policy: Download bandwidth
        /// </summary>
        [JsonProperty("policy:MaxDownload_u32")]
        public uint SecPol_MaxDownload_u32;

        /// <summary>
        /// Security policy: User can not change password
        /// </summary>
        [JsonProperty("policy:FixPassword_bool")]
        public bool SecPol_FixPassword_bool;

        /// <summary>
        /// Security policy: Multiple logins limit
        /// </summary>
        [JsonProperty("policy:MultiLogins_u32")]
        public uint SecPol_MultiLogins_u32;

        /// <summary>
        /// Security policy: Prohibit the use of VoIP / QoS features
        /// </summary>
        [JsonProperty("policy:NoQoS_bool")]
        public bool SecPol_NoQoS_bool;

        /// <summary>
        /// Security policy: Filter the Router Solicitation / Advertising packet (IPv6)
        /// </summary>
        [JsonProperty("policy:RSandRAFilter_bool")]
        public bool SecPol_RSandRAFilter_bool;

        /// <summary>
        /// Security policy: Filter the router advertisement packet (IPv6)
        /// </summary>
        [JsonProperty("policy:RAFilter_bool")]
        public bool SecPol_RAFilter_bool;

        /// <summary>
        /// Security policy: Filter DHCP packets (IPv6)
        /// </summary>
        [JsonProperty("policy:DHCPv6Filter_bool")]
        public bool SecPol_DHCPv6Filter_bool;

        /// <summary>
        /// Security policy: Prohibit the behavior of the DHCP server (IPv6)
        /// </summary>
        [JsonProperty("policy:DHCPv6NoServer_bool")]
        public bool SecPol_DHCPv6NoServer_bool;

        /// <summary>
        /// Security policy: Prohibit the router behavior (IPv6)
        /// </summary>
        [JsonProperty("policy:NoRoutingV6_bool")]
        public bool SecPol_NoRoutingV6_bool;

        /// <summary>
        /// Security policy: Prohibit the duplicate IP address (IPv6)
        /// </summary>
        [JsonProperty("policy:CheckIPv6_bool")]
        public bool SecPol_CheckIPv6_bool;

        /// <summary>
        /// Security policy: Prohibit to operate as a TCP/IP server (IPv6)
        /// </summary>
        [JsonProperty("policy:NoServerV6_bool")]
        public bool SecPol_NoServerV6_bool;

        /// <summary>
        /// Security policy: Maximum number of IP address (IPv6)
        /// </summary>
        [JsonProperty("policy:MaxIPv6_u32")]
        public uint SecPol_MaxIPv6_u32;

        /// <summary>
        /// Security policy: Prohibit to save the password in the VPN Client
        /// </summary>
        [JsonProperty("policy:NoSavePassword_bool")]
        public bool SecPol_NoSavePassword_bool;

        /// <summary>
        /// Security policy: Disconnect the VPN Client automatically at a certain period of time
        /// </summary>
        [JsonProperty("policy:AutoDisconnect_u32")]
        public uint SecPol_AutoDisconnect_u32;

        /// <summary>
        /// Security policy: Filter all IPv4 packets
        /// </summary>
        [JsonProperty("policy:FilterIPv4_bool")]
        public bool SecPol_FilterIPv4_bool;

        /// <summary>
        /// Security policy: Filter all IPv6 packets
        /// </summary>
        [JsonProperty("policy:FilterIPv6_bool")]
        public bool SecPol_FilterIPv6_bool;

        /// <summary>
        /// Security policy: Filter all non-IP packets
        /// </summary>
        [JsonProperty("policy:FilterNonIP_bool")]
        public bool SecPol_FilterNonIP_bool;

        /// <summary>
        /// Security policy: Delete the default router specification from the IPv6 router advertisement
        /// </summary>
        [JsonProperty("policy:NoIPv6DefaultRouterInRA_bool")]
        public bool SecPol_NoIPv6DefaultRouterInRA_bool;

        /// <summary>
        /// Security policy: Delete the default router specification from the IPv6 router advertisement (Enable IPv6 connection)
        /// </summary>
        [JsonProperty("policy:NoIPv6DefaultRouterInRAWhenIPv6_bool")]
        public bool SecPol_NoIPv6DefaultRouterInRAWhenIPv6_bool;

        /// <summary>
        /// Security policy: Specify the VLAN ID
        /// </summary>
        [JsonProperty("policy:VLanId_u32")]
        public uint SecPol_VLanId_u32;

        /// <summary>
        /// Security policy: Whether version 3.0
        /// </summary>
        [JsonProperty("policy:Ver3_bool")]
        public bool SecPol_Ver3_bool = true;
        // ---- End of Security policy ---
    }

    /// <summary>
    /// Delete the access list
    /// </summary>
    public class VpnRpcDeleteAccess
    {
        /// <summary>
        /// HUB Name
        /// </summary>
        public string HubName_str;

        /// <summary>
        /// ID
        /// </summary>
        public uint Id_u32;
    }

    /// <summary>
    /// Delete the CA of HUB
    /// </summary>
    public class VpnRpcHubDeleteCA
    {
        /// <summary>
        /// HUB Name
        /// </summary>
        public string HubName_str;

        /// <summary>
        /// Certificate key to be deleted
        /// </summary>
        public uint Key_u32;
    }

    /// <summary>
    /// Deleting a user or group
    /// </summary>
    public class VpnRpcDeleteUser
    {
        /// <summary>
        /// HUB Name
        /// </summary>
        public string HubName_str;

        /// <summary>
        /// User or group name
        /// </summary>
        public string Name_str;
    }

    /// <summary>
    /// Delete the HUB
    /// </summary>
    public class VpnRpcDeleteHub
    {
        /// <summary>
        /// HUB Name
        /// </summary>
        public string HubName_str;
    }

    /// <summary>
    /// Delete the table
    /// </summary>
    public class VpnRpcDeleteTable
    {
        /// <summary>
        /// HUB Name
        /// </summary>
        public string HubName_str;

        /// <summary>
        /// Key
        /// </summary>
        public uint Key_u32;
    }

    /// <summary>
    /// Specify the Link
    /// </summary>
    public class VpnRpcLink
    {
        /// <summary>
        /// HUB Name
        /// </summary>
        public string HubName_str;

        /// <summary>
        /// Account name
        /// </summary>
        public string AccountName_utf;
    }

    /// <summary>
    /// Disconnect the session
    /// </summary>
    public class VpnRpcDeleteSession
    {
        /// <summary>
        /// HUB Name
        /// </summary>
        public string HubName_str;

        /// <summary>
        /// Session name
        /// </summary>
        public string Name_str;
    }

    /// <summary>
    /// Specify the HUB
    /// </summary>
    public class VpnRpcHub
    {
        /// <summary>
        /// HUB Name
        /// </summary>
        public string HubName_str;
    }

    /// <summary>
    /// Disconnection
    /// </summary>
    public class VpnRpcDisconnectConnection
    {
        /// <summary>
        /// Connection name
        /// </summary>
        public string Name_str;
    }

    /// <summary>
    /// Enumeration of the access list
    /// </summary>
    public class VpnRpcEnumAccessList
    {
        /// <summary>
        /// HUB Name
        /// </summary>
        public string HubName_str;

        /// <summary>
        /// Access list
        /// </summary>
        public VpnAccess[] AccessList;
    }

    /// <summary>
    /// CA enumeration items of HUB
    /// </summary>
    public class VpnRpcHubEnumCAItem
    {
        /// <summary>
        /// Certificate key
        /// </summary>
        public uint Key_u32;

        /// <summary>
        /// Issued to
        /// </summary>
        public string SubjectName_utf;

        /// <summary>
        /// Issuer
        /// </summary>
        public string IssuerName_utf;

        /// <summary>
        /// Expiration date
        /// </summary>
        public DateTime Expires_dt;
    }

    /// <summary>
    /// CA enumeration of HUB *
    /// </summary>
    public class VpnRpcHubEnumCA
    {
        /// <summary>
        /// HUB Name
        /// </summary>
        public string HubName_str;

        /// <summary>
        /// CA
        /// </summary>
        public VpnRpcHubEnumCAItem[] CAList;
    }

    /// <summary>
    /// Type of connection
    /// </summary>
    public enum VpnRpcConnectionType
    {
        /// <summary>
        ///  VPN Client
        /// </summary>
        Client = 0,

        /// <summary>
        /// During initialization
        /// </summary>
        Init = 1,

        /// <summary>
        /// Login connection
        /// </summary>
        Login = 2,

        /// <summary>
        /// Additional connection
        /// </summary>
        Additional = 3,

        /// <summary>
        /// RPC for server farm
        /// </summary>
        FarmRpc = 4,

        /// <summary>
        /// RPC for Management
        /// </summary>
        AdminRpc = 5,

        /// <summary>
        /// HUB enumeration
        /// </summary>
        EnumHub = 6,

        /// <summary>
        /// Password change
        /// </summary>
        Password = 7,

        /// <summary>
        /// SSTP
        /// </summary>
        SSTP = 8,

        /// <summary>
        /// OpenVPN
        /// </summary>
        OpenVPN = 9,
    }

    /// <summary>
    /// Connection enumeration items
    /// </summary>
    public class VpnRpcEnumConnectionItem
    {
        /// <summary>
        /// Connection name
        /// </summary>
        public string Name_str;

        /// <summary>
        /// Host name
        /// </summary>
        public string Hostname_str;

        /// <summary>
        /// IP address
        /// </summary>
        public string Ip_ip;

        /// <summary>
        /// Port number
        /// </summary>
        public uint Port_u32;

        /// <summary>
        /// Connected time
        /// </summary>
        public DateTime ConnectedTime_dt;

        /// <summary>
        /// Type
        /// </summary>
        public VpnRpcConnectionType Type_u32;
    }

    /// <summary>
    /// Connection enumeration
    /// </summary>
    public class VpnRpcEnumConnection
    {
        /// <summary>
        /// Number of connections
        /// </summary>
        public uint NumConnection_u32;

        /// <summary>
        /// Connection list
        /// </summary>
        public VpnRpcEnumConnectionItem[] ConnectionList;
    }

    /// <summary>
    /// TODO
    ///</summary>
    public class VpnRpcEnumCrl
    {
        /// <summary>
        /// HUB Name
        /// </summary>
        public string HubName_str;

        /// <summary>
        /// Number of items
        /// </summary>
        public uint NumItem_u32;

        /// <summary>
        /// List
        /// </summary>
        // TODO: RPC_ENUM_CRL_ITEM *Items;
    }

    /// <summary>
    /// RPC_ENUM_DHCP_ITEM
    /// </summary>
    public class VpnRpcEnumDhcpItem
    {
        /// <summary>
        /// ID
        /// </summary>
        public uint Id_u32;

        /// <summary>
        /// Lease time
        /// </summary>
        public DateTime LeasedTime_dt;

        /// <summary>
        /// Expiration date
        /// </summary>
        public DateTime ExpireTime_dt;

        /// <summary>
        /// MAC address
        /// </summary>
        public byte[] MacAddress_bin;

        /// <summary>
        /// IP address
        /// </summary>
        public string IpAddress_ip;

        /// <summary>
        /// Subnet mask
        /// </summary>
        public uint Mask_u32;

        /// <summary>
        /// Host name
        /// </summary>
        public string Hostname_str;
    }

    /// <summary>
    /// RPC_ENUM_DHCP
    /// </summary>
    public class VpnRpcEnumDhcp
    {
        /// <summary>
        /// HUB name
        /// </summary>
        public string HubName_str;

        /// <summary>
        /// Item
        /// </summary>
        public VpnRpcEnumDhcpItem[] DhcpTable;
    }

    /// <summary>
    /// EtherIP setting list
    /// </summary>
    public class VpnRpcEnumEtherIpId
    {
        public uint NumItem_u32;

        // TODO: ETHERIP_ID *IdList;
    }

    /// <summary>
    /// Ethernet enumeration item
    /// </summary>
    public class VpnRpcEnumEthItem
    {
        /// <summary>
        /// Device name
        /// </summary>
        public string DeviceName_str;

        /// <summary>
        /// Network connection name
        /// </summary>
        public string NetworkConnectionName_utf;
    }

    /// <summary>
    /// Ethernet enumeration
    /// </summary>
    public class VpnRpcEnumEth
    {
        /// <summary>
        /// Item
        /// </summary>
        public VpnRpcEnumEthItem[] EthList;
    }

    /// <summary>
    /// TODO
    ///</summary>
    public class VpnRpcEnumEthVlan
    {
        /// <summary>
        /// Number of items
        /// </summary>
        public uint NumItem_u32;

        /// <summary>
        /// List
        /// </summary>
        // TODO: RPC_ENUM_ETH_VLAN_ITEM *Items;
    }

    /// <summary>
    /// Server farm members enumeration items
    /// </summary>
    public class VpnRpcEnumFarmItem
    {
        /// <summary>
        /// ID
        /// </summary>
        public uint Id_u32;

        /// <summary>
        /// Controller
        /// </summary>
        public bool Controller_bool;

        /// <summary>
        /// Connection time
        /// </summary>
        public DateTime ConnectedTime_dt;

        /// <summary>
        /// IP address
        /// </summary>
        public string Ip_ip;

        /// <summary>
        /// Host name
        /// </summary>
        public string Hostname_str;

        /// <summary>
        /// Point
        /// </summary>
        public uint Point_u32;

        /// <summary>
        /// Number of sessions
        /// </summary>
        public uint NumSessions_u32;

        /// <summary>
        /// Number of TCP connections
        /// </summary>
        public uint NumTcpConnections_u32;

        /// <summary>
        /// Number of HUBs
        /// </summary>
        public uint NumHubs_u32;

        /// <summary>
        /// Number of assigned client licenses
        /// </summary>
        public uint AssignedClientLicense_u32;

        /// <summary>
        /// Number of assigned bridge licenses
        /// </summary>
        public uint AssignedBridgeLicense_u32;
    }

    /// <summary>
    /// Server farm member enumeration 
    /// </summary>
    public class VpnRpcEnumFarm
    {
        /// <summary>
        /// Number of farm members
        /// </summary>
        public uint NumFarm_u32;

        /// <summary>
        /// Farm member list
        /// </summary>
        public VpnRpcEnumFarmItem[] FarmMemberList;
    }

    /// <summary>
    /// Enumeration items in the group
    /// </summary>
    public class VpnRpcEnumGroupItem
    {
        /// <summary>
        /// User name
        /// </summary>
        public string Name_str;

        /// <summary>
        /// Real name
        /// </summary>
        public string Realname_utf;

        /// <summary>
        /// Note
        /// </summary>
        public string Note_utf;

        /// <summary>
        /// Number of users
        /// </summary>
        public uint NumUsers_u32;

        /// <summary>
        /// Access denied
        /// </summary>
        public bool DenyAccess_bool;
    }

    /// <summary>
    /// Group enumeration
    /// </summary>
    public class VpnRpcEnumGroup
    {
        /// <summary>
        /// HUB Name
        /// </summary>
        public string HubName_str;

        /// <summary>
        /// Group
        /// </summary>
        public VpnRpcEnumGroupItem[] GroupList;
    }

    /// <summary>
    /// Enumeration items of HUB
    /// </summary>
    public class VpnRpcEnumHubItem
    {
        /// <summary>
        /// HUB Name
        /// </summary>
        public string HubName_str;

        /// <summary>
        /// Online
        /// </summary>
        public bool Online_bool;

        /// <summary>
        /// Type of HUB
        /// </summary>
        public VpnRpcHubType HubType_u32;

        /// <summary>
        /// Number of users
        /// </summary>
        public uint NumUsers_u32;

        /// <summary>
        /// Number of groups
        /// </summary>
        public uint NumGroups_u32;

        /// <summary>
        /// Number of sessions
        /// </summary>
        public uint NumSessions_u32;

        /// <summary>
        /// Number of MAC table entries
        /// </summary>
        public uint NumMacTables_u32;

        /// <summary>
        /// Number of IP table entries
        /// </summary>
        public uint NumIpTables_u32;

        /// <summary>
        /// Last communication date and time
        /// </summary>
        public DateTime LastCommTime_dt;

        /// <summary>
        /// Last login date and time
        /// </summary>
        public DateTime LastLoginTime_dt;

        /// <summary>
        /// Creation date and time
        /// </summary>
        public DateTime CreatedTime_dt;

        /// <summary>
        /// Number of logins
        /// </summary>
        public uint NumLogin_u32;

        /// <summary>
        /// Whether the traffic information exists
        /// </summary>
        public bool IsTrafficFilled_bool;

        /// <summary>
        /// Number of broadcast packets (Recv)
        /// </summary>
        [JsonProperty("Ex.Recv.BroadcastBytes_u64")]
        public ulong Recv_BroadcastBytes_u64;

        /// <summary>
        /// Broadcast bytes (Recv)
        /// </summary>
        [JsonProperty("Ex.Recv.BroadcastCount_u64")]
        public ulong Recv_BroadcastCount_u64;

        /// <summary>
        /// Unicast count (Recv)
        /// </summary>
        [JsonProperty("Ex.Recv.UnicastBytes_u64")]
        public ulong Recv_UnicastBytes_u64;

        /// <summary>
        /// Unicast bytes (Recv)
        /// </summary>
        [JsonProperty("Ex.Recv.UnicastCount_u64")]
        public ulong Recv_UnicastCount_u64;

        /// <summary>
        /// Number of broadcast packets (Send)
        /// </summary>
        [JsonProperty("Ex.Send.BroadcastBytes_u64")]
        public ulong Send_BroadcastBytes_u64;

        /// <summary>
        /// Broadcast bytes (Send)
        /// </summary>
        [JsonProperty("Ex.Send.BroadcastCount_u64")]
        public ulong Send_BroadcastCount_u64;

        /// <summary>
        /// Unicast bytes (Send)
        /// </summary>
        [JsonProperty("Ex.Send.UnicastBytes_u64")]
        public ulong Send_UnicastBytes_u64;

        /// <summary>
        /// Unicast bytes (Send)
        /// </summary>
        [JsonProperty("Ex.Send.UnicastCount_u64")]
        public ulong Send_UnicastCount_u64;
    }

    /// <summary>
    /// Enumeration of HUB
    /// </summary>
    public class VpnRpcEnumHub
    {
        /// <summary>
        /// Number of HUBs
        /// </summary>
        public uint NumHub_u32;

        /// <summary>
        /// HUB
        /// </summary>
        public VpnRpcEnumHubItem[] HubList;
    }

    /// <summary>
    /// Enumeration items of IP table
    /// </summary>
    public class VpnRpcEnumIpTableItem
    {
        /// <summary>
        /// Key
        /// </summary>
        public uint Key_u32;

        /// <summary>
        /// Session name
        /// </summary>
        public string SessionName_str;

        /// <summary>
        /// IP address
        /// </summary>
        public string IpAddress_ip;

        /// <summary>
        /// Assigned by the DHCP
        /// </summary>
        public bool DhcpAllocated_bool;

        /// <summary>
        /// Creation date and time
        /// </summary>
        public DateTime CreatedTime_dt;

        /// <summary>
        /// Updating date
        /// </summary>
        public DateTime UpdatedTime_dt;

        /// <summary>
        /// Remote items
        /// </summary>
        public bool RemoteItem_bool;

        /// <summary>
        /// Remote host name
        /// </summary>
        public string RemoteHostname_str;
    }

    /// <summary>
    /// Enumeration of IP table
    /// </summary>
    public class VpnRpcEnumIpTable
    {
        /// <summary>
        /// HUB Name
        /// </summary>
        public string HubName_str;

        /// <summary>
        /// MAC table
        /// </summary>
        public VpnRpcEnumIpTableItem[] IpTable;
    }

    /// <summary>
    /// Layer-3 interface enumeration
    /// </summary>
    public class VpnRpcEnumL3If
    {
        /// <summary>
        /// L3 switch name
        /// </summary>
        public string Name_str;

        public uint NumItem_u32;

        // TODO: RPC_L3IF *Items;
    }

    /// <summary>
    /// TODO
    ///</summary>
    public class VpnRpcEnumL3Sw
    {
        public uint NumItem_u32;

        // TODO: RPC_ENUM_L3SW_ITEM *Items;
    }

    /// <summary>
    /// Routing table enumeration
    /// </summary>
    public class VpnRpcEnumL3Table
    {
        /// <summary>
        /// L3 switch name
        /// </summary>
        public string Name_str;

        public uint NumItem_u32;

        // TODO: RPC_L3TABLE *Items;
    }

    /// <summary>
    /// TODO
    ///</summary>
    public class VpnRpcEnumLicenseKey
    {
        /// <summary>
        /// Number of items
        /// </summary>
        public uint NumItem_u32;

        /// <summary>
        /// List
        /// </summary>
        // TODO: RPC_ENUM_LICENSE_KEY_ITEM *Items;
    }

    /// <summary>
    /// Enumeration items of link
    /// </summary>
    public class VpnRpcEnumLinkItem
    {
        /// <summary>
        /// Account name
        /// </summary>
        public string AccountName_utf;

        /// <summary>
        /// Online flag
        /// </summary>
        public bool Online_bool;

        /// <summary>
        /// Connection completion flag
        /// </summary>
        public bool Connected_bool;

        /// <summary>
        /// The error that last occurred
        /// </summary>
        public uint LastError_u32;

        /// <summary>
        /// Connection completion time
        /// </summary>
        public DateTime ConnectedTime_dt;

        /// <summary>
        /// Host name
        /// </summary>
        public string Hostname_str;

        /// <summary>
        /// HUB Name
        /// </summary>
        public string TargetHubName_str;
    }

    /// <summary>
    /// Enumeration of the link
    /// </summary>
    public class VpnRpcEnumLink
    {
        /// <summary>
        /// HUB Name
        /// </summary>
        public string HubName_str;

        /// <summary>
        /// Number of links
        /// </summary>
        public uint NumLink_u32;

        /// <summary>
        /// Link List
        /// </summary>
        public VpnRpcEnumLinkItem[] LinkList;
    }

    /// <summary>
    /// List of listeners item
    /// </summary>
    public class VpnRpcListenerListItem
    {
        /// <summary>
        /// Port number
        /// </summary>
        public uint Ports_u32;

        /// <summary>
        /// Effective state
        /// </summary>
        public bool Enables_bool;

        /// <summary>
        /// If error occurred
        /// </summary>
        public bool Errors_bool;
    }

    /// <summary>
    /// List of listeners
    /// </summary>
    public class VpnRpcListenerList
    {
        /// <summary>
        /// List of listener items
        /// </summary>
        public VpnRpcListenerListItem[] ListenerList;
    }

    /// <summary>
    /// Bridge enumeration
    /// </summary>
    public class VpnRpcEnumLocalBridge
    {
        /// <summary>
        /// Number of items
        /// </summary>
        public uint NumItem_u32;

        /// <summary>
        /// Item
        /// </summary>
        // TODO: RPC_LOCALBRIDGE *Items;
    }

    /// <summary>
    /// TODO
    ///</summary>
    public class VpnRpcEnumLogFile
    {
        /// <summary>
        /// Number of items
        /// </summary>
        public uint NumItem_u32;

        /// <summary>
        /// List
        /// </summary>
        // TODO: RPC_ENUM_LOG_FILE_ITEM *Items;
    }

    /// <summary>
    /// Enumeration items of the MAC table
    /// </summary>
    public class VpnRpcEnumMacTableItem
    {
        /// <summary>
        /// Key
        /// </summary>
        public uint Key_u32;

        /// <summary>
        /// Session name
        /// </summary>
        public string SessionName_str;

        /// <summary>
        /// MAC address
        /// </summary>
        public byte[] MacAddress_bin;

        /// <summary>
        /// Creation date and time
        /// </summary>
        public DateTime CreatedTime_dt;

        /// <summary>
        /// Updating date
        /// </summary>
        public DateTime UpdatedTime_dt;

        /// <summary>
        /// Remote items
        /// </summary>
        public bool RemoteItem_bool;

        /// <summary>
        /// Remote host name
        /// </summary>
        public string RemoteHostname_str;

        /// <summary>
        /// VLAN ID
        /// </summary>
        public uint VlanId_u32;
    }

    /// <summary>
    /// Enumeration of the MAC table
    /// </summary>
    public class VpnRpcEnumMacTable
    {
        /// <summary>
        /// HUB Name
        /// </summary>
        public string HubName_str;

        /// <summary>
        /// MAC table
        /// </summary>
        public VpnRpcEnumMacTableItem[] MacTable;
    }

    /// <summary>
    /// NAT Entry Protocol Number
    /// </summary>
    public enum VpnRpcNatProtocol
    {
        /// <summary>
        ///  TCP
        /// </summary>
        TCP = 0,

        /// <summary>
        /// UDP
        /// </summary>
        UDP = 1,

        /// <summary>
        ///  DNS
        /// </summary>
        DNS = 2,

        /// <summary>
        /// ICMP
        /// </summary>
        ICMP = 3,
    }

    /// <summary>
    /// State of NAT session (TCP)
    /// </summary>
    public enum VpnRpcNatTcpState
    {
        /// <summary>
        /// Connecting
        /// </summary>
        Connecting = 0,

        /// <summary>
        /// Send the RST (Connection failure or disconnected)
        /// </summary>
        SendReset = 1,

        /// <summary>
        /// Connection complete
        /// </summary>
        Connected = 2,

        /// <summary>
        /// Connection established
        /// </summary>
        Established = 3,

        /// <summary>
        /// Wait for socket disconnection
        /// </summary>
        WaitDisconnect = 4,
    }

    /// <summary>
    /// VpnRpcEnumNat List Item
    /// </summary>
    public class VpnRpcEnumNatItem
    {
        /// <summary>
        /// ID
        /// </summary>
        public uint Id_u32;

        /// <summary>
        /// Protocol
        /// </summary>
        public VpnRpcNatProtocol Protocol_u32;

        /// <summary>
        /// Source IP address
        /// </summary>
        public string SrcIp_ip;

        /// <summary>
        /// Source host name
        /// </summary>
        public string SrcHost_str;

        /// <summary>
        /// Source port number
        /// </summary>
        public uint SrcPort_u32;

        /// <summary>
        /// Destination IP address
        /// </summary>
        public string DestIp_ip;

        /// <summary>
        /// Destination host name
        /// </summary>
        public string DestHost_str;

        /// <summary>
        /// Destination port number
        /// </summary>
        public uint DestPort_u32;

        /// <summary>
        /// Connection time
        /// </summary>
        public DateTime CreatedTime_dt;

        /// <summary>
        /// Last communication time
        /// </summary>
        public DateTime LastCommTime_dt;

        /// <summary>
        /// Transmission size
        /// </summary>
        public ulong SendSize_u64;

        /// <summary>
        /// Receive size
        /// </summary>
        public ulong RecvSize_u64;

        /// <summary>
        /// TCP state
        /// </summary>
        public VpnRpcNatTcpState TcpStatus_u32;
    }

    /// <summary>
    /// RPC_ENUM_NAT
    /// </summary>
    public class VpnRpcEnumNat
    {
        /// <summary>
        /// HUB name
        /// </summary>
        public string HubName_str;

        /// <summary>
        /// Item
        /// </summary>
        public VpnRpcEnumNatItem[] NatTable;
    }

    /// <summary>
    /// Enumeration items of session
    /// </summary>
    public class VpnRpcEnumSessionItem
    {
        /// <summary>
        /// Session name
        /// </summary>
        public string Name_str;

        /// <summary>
        /// Remote session
        /// </summary>
        public bool RemoteSession_bool;

        /// <summary>
        /// Remote server name
        /// </summary>
        public string RemoteHostname_str;

        /// <summary>
        /// User name
        /// </summary>
        public string Username_str;

        /// <summary>
        /// IP address
        /// </summary>
        public string ClientIP_ip;

        /// <summary>
        /// Host name
        /// </summary>
        public string Hostname_str;

        /// <summary>
        /// Maximum number of TCP connections
        /// </summary>
        public uint MaxNumTcp_u32;

        /// <summary>
        /// Number of currentl TCP connections
        /// </summary>
        public uint CurrentNumTcp_u32;

        /// <summary>
        /// Packet size
        /// </summary>
        public ulong PacketSize_u64;

        /// <summary>
        /// Number of packets
        /// </summary>
        public ulong PacketNum_u64;

        /// <summary>
        /// Link mode
        /// </summary>
        public bool LinkMode_bool;

        /// <summary>
        /// SecureNAT mode
        /// </summary>
        public bool SecureNATMode_bool;

        /// <summary>
        /// Bridge mode
        /// </summary>
        public bool BridgeMode_bool;

        /// <summary>
        /// Layer 3 mode
        /// </summary>
        public bool Layer3Mode_bool;

        /// <summary>
        /// Client is bridge mode
        /// </summary>
        public bool Client_BridgeMode_bool;

        /// <summary>
        /// Client is monitoring mode
        /// </summary>
        public bool Client_MonitorMode_bool;

        /// <summary>
        /// VLAN ID
        /// </summary>
        public uint VLanId_u32;

        /// <summary>
        /// Unique ID
        /// </summary>
        public byte[] UniqueId_bin;

        /// <summary>
        /// Creation date and time
        /// </summary>
        public DateTime CreatedTime_dt;

        /// <summary>
        /// Last communication date and time
        /// </summary>
        public DateTime LastCommTime_dt;
    }

    /// <summary>
    /// Enumerate sessions
    /// </summary>
    public class VpnRpcEnumSession
    {
        /// <summary>
        /// HUB Name
        /// </summary>
        public string HubName_str;

        /// <summary>
        /// Session list
        /// </summary>
        public VpnRpcEnumSessionItem[] SessionList;
    }

    /// <summary>
    /// Enumeration item of user
    /// </summary>
    public class VpnRpcEnumUserItem
    {
        /// <summary>
        /// User name
        /// </summary>
        public string Name_str;

        /// <summary>
        /// Group name
        /// </summary>
        public string GroupName_str;

        /// <summary>
        /// Real name
        /// </summary>
        public string Realname_utf;

        /// <summary>
        /// Note
        /// </summary>
        public string Note_utf;

        /// <summary>
        /// Authentication method
        /// </summary>
        public VpnRpcUserAuthType AuthType_u32;

        /// <summary>
        /// Number of logins
        /// </summary>
        public uint NumLogin_u32;

        /// <summary>
        /// Last login date and time
        /// </summary>
        public DateTime LastLoginTime_dt;

        /// <summary>
        /// Access denied
        /// </summary>
        public bool DenyAccess_bool;

        /// <summary>
        /// Flag of whether the traffic variable is set
        /// </summary>
        public bool IsTrafficFilled_bool;

        /// <summary>
        /// Flag of whether expiration date variable is set
        /// </summary>
        public bool IsExpiresFilled_bool;

        /// <summary>
        /// Expiration date
        /// </summary>
        public DateTime Expires_dt;

        /// <summary>
        /// Number of broadcast packets (Recv)
        /// </summary>
        [JsonProperty("Ex.Recv.BroadcastBytes_u64")]
        public ulong Recv_BroadcastBytes_u64;

        /// <summary>
        /// Broadcast bytes (Recv)
        /// </summary>
        [JsonProperty("Ex.Recv.BroadcastCount_u64")]
        public ulong Recv_BroadcastCount_u64;

        /// <summary>
        /// Unicast count (Recv)
        /// </summary>
        [JsonProperty("Ex.Recv.UnicastBytes_u64")]
        public ulong Recv_UnicastBytes_u64;

        /// <summary>
        /// Unicast bytes (Recv)
        /// </summary>
        [JsonProperty("Ex.Recv.UnicastCount_u64")]
        public ulong Recv_UnicastCount_u64;

        /// <summary>
        /// Number of broadcast packets (Send)
        /// </summary>
        [JsonProperty("Ex.Send.BroadcastBytes_u64")]
        public ulong Send_BroadcastBytes_u64;

        /// <summary>
        /// Broadcast bytes (Send)
        /// </summary>
        [JsonProperty("Ex.Send.BroadcastCount_u64")]
        public ulong Send_BroadcastCount_u64;

        /// <summary>
        /// Unicast bytes (Send)
        /// </summary>
        [JsonProperty("Ex.Send.UnicastBytes_u64")]
        public ulong Send_UnicastBytes_u64;

        /// <summary>
        /// Unicast bytes (Send)
        /// </summary>
        [JsonProperty("Ex.Send.UnicastCount_u64")]
        public ulong Send_UnicastCount_u64;
    }

    /// <summary>
    /// Enumeration of user
    /// </summary>
    public class VpnRpcEnumUser
    {
        /// <summary>
        /// HUB Name
        /// </summary>
        public string HubName_str;

        /// <summary>
        /// User
        /// </summary>
        public VpnRpcEnumUserItem[] UserList;
    }

    /// <summary>
    /// AC list
    /// </summary>
    public class VpnRpcAcList
    {
        /// <summary>
        /// HUB Name
        /// </summary>
        public string HubName_str;

        /// <summary>
        /// List body
        /// </summary>
        // TODO: LIST *o;

        public bool InternalFlag1_bool;
    }

    /// <summary>
    /// Message
    /// </summary>
    public class VpnRpcMsg
    {
        /// <summary>
        /// HUB Name
        /// </summary>
        public string HubName_str;

        /// <summary>
        /// Message
        /// </summary>
        public string Msg_utf;
    }

    /// <summary>
    /// Get / Set the Azure state
    /// </summary>
    public class VpnRpcAzureStatus
    {
        /// <summary>
        /// Whether enabled
        /// </summary>
        public bool IsEnabled_bool;

        /// <summary>
        /// Whether it's connected
        /// </summary>
        public bool IsConnected_bool;
    }

    /// <summary>
    /// Bridge support information
    /// </summary>
    public class VpnRpcBridgeSupport
    {
        /// <summary>
        /// Whether the OS supports the bridge
        /// </summary>
        public bool IsBridgeSupportedOs_bool;

        /// <summary>
        /// Whether WinPcap is necessary
        /// </summary>
        public bool IsWinPcapNeeded_bool;
    }

    /// <summary>
    /// Get the CA of HUB *
    /// </summary>
    public class VpnRpcHubGetCA
    {
        /// <summary>
        /// HUB Name
        /// </summary>
        public string HubName_str;

        /// <summary>
        /// Certificate key
        /// </summary>
        public uint Key_u32;

        /// <summary>
        /// Certificate
        /// </summary>
        public byte[] Cert_bin;
    }

    /// <summary>
    /// TODO
    ///</summary>
    public class VpnCapslist
    {
        /// <summary>
        /// Caps list
        /// </summary>
        // TODO: LIST *CapsList;
    }

    /// <summary>
    /// Config operation
    /// </summary>
    public class VpnRpcConfig
    {
        /// <summary>
        /// File name
        /// </summary>
        public string FileName_str;

        /// <summary>
        /// File data
        /// </summary>
        public string FileData_str;
    }

    /// <summary>
    /// Connection information
    /// </summary>
    public class VpnRpcConnectionInfo
    {
        /// <summary>
        /// Connection name
        /// </summary>
        public string Name_str;

        /// <summary>
        /// Type
        /// </summary>
        public VpnRpcConnectionType Type_u32;

        /// <summary>
        /// Host name
        /// </summary>
        public string Hostname_str;

        /// <summary>
        /// IP address
        /// </summary>
        public string Ip_ip;

        /// <summary>
        /// Port number
        /// </summary>
        public uint Port_u32;

        /// <summary>
        /// Connected time
        /// </summary>
        public DateTime ConnectedTime_dt;

        /// <summary>
        /// Server string
        /// </summary>
        public string ServerStr_str;

        /// <summary>
        /// Server version
        /// </summary>
        public uint ServerVer_u32;

        /// <summary>
        /// Server build number
        /// </summary>
        public uint ServerBuild_u32;

        /// <summary>
        /// Client string
        /// </summary>
        public string ClientStr_str;

        /// <summary>
        /// Client version
        /// </summary>
        public uint ClientVer_u32;

        /// <summary>
        /// Client build number
        /// </summary>
        public uint ClientBuild_u32;
    }

    /// <summary>
    /// The current status of the DDNS
    /// </summary>
    public class VpnDDnsClientStatus
    {
        /// <summary>
        /// Last error
        /// </summary>
        public uint Err_IPv4, _u32;

        /// <summary>
        /// Current host name
        /// </summary>
        public string CurrentHostName_str;

        /// <summary>
        /// Current FQDN
        /// </summary>
        public string CurrentFqdn_str;

        /// <summary>
        /// DNS suffix
        /// </summary>
        public string DnsSuffix_str;

        /// <summary>
        /// Current IPv4 address
        /// </summary>
        public string CurrentIPv4_str;

        /// <summary>
        /// Current IPv6 address
        /// </summary>
        public string CurrentIPv6_str;

        /// <summary>
        /// IP address of Azure Server to be used
        /// </summary>
        public string CurrentAzureIp_str;

        /// <summary>
        /// Time stamp to be presented to the Azure Server
        /// </summary>
        public ulong CurrentAzureTimestamp_u64;

        /// <summary>
        /// Signature to be presented to the Azure Server
        /// </summary>
        public string CurrentAzureSignature_str;

        /// <summary>
        /// Azure Server certificate hash
        /// </summary>
        public string AzureCertHash_str;

        /// <summary>
        /// Internet settings
        /// </summary>
        // TODO: INTERNET_SETTING InternetSetting;
    }

    /// <summary>
    /// Internet connection settings
    /// </summary>
    public class VpnInternetSetting
    {
        /// <summary>
        /// Type of proxy server
        /// </summary>
        public uint ProxyType_u32;

        /// <summary>
        /// Proxy server host name
        /// </summary>
        public string ProxyHostName_str;

        /// <summary>
        /// Proxy server port number
        /// </summary>
        public uint ProxyPort_u32;

        /// <summary>
        /// Proxy server user name
        /// </summary>
        public string ProxyUsername_str;

        /// <summary>
        /// Proxy server password
        /// </summary>
        public string ProxyPassword_str;
    }

    /// <summary>
    /// Administration options list
    /// </summary>
    public class VpnRpcAdminOption
    {
        /// <summary>
        /// Virtual HUB name
        /// </summary>
        public string HubName_str;

        /// <summary>
        /// Count
        /// </summary>
        public uint NumItem_u32;

        /// <summary>
        /// Data
        /// </summary>
        // TODO: ADMIN_OPTION *Items;
    }

    /// <summary>
    /// Connection state to the controller
    /// </summary>
    public class VpnRpcFarmConnectionStatus
    {
        /// <summary>
        /// IP address
        /// </summary>
        public string Ip_ip;

        /// <summary>
        /// Port number
        /// </summary>
        public uint Port_u32;

        /// <summary>
        /// Online state
        /// </summary>
        public bool Online_bool;

        /// <summary>
        /// Last error
        /// </summary>
        public uint LastError_u32;

        /// <summary>
        /// Connection start time
        /// </summary>
        public DateTime StartedTime_dt;

        /// <summary>
        /// First connection time
        /// </summary>
        public DateTime FirstConnectedTime_dt;

        /// <summary>
        /// Connection time of this time
        /// </summary>
        public DateTime CurrentConnectedTime_dt;

        /// <summary>
        /// Number of trials
        /// </summary>
        public uint NumTry_u32;

        /// <summary>
        /// Number of connection count
        /// </summary>
        public uint NumConnected_u32;

        /// <summary>
        /// Connection failure count
        /// </summary>
        public uint NumFailed_u32;
    }

    /// <summary>
    /// HUB item of each farm member
    /// </summary>
    public class VpnRpcFarmHub
    {
        /// <summary>
        /// HUB Name
        /// </summary>
        public string HubName_str;

        /// <summary>
        /// Dynamic HUB
        /// </summary>
        public bool DynamicHub_bool;
    }


    /// <summary>
    /// Server farm member information acquisition *
    /// </summary>
    public class VpnRpcFarmInfo
    {
        /// <summary>
        /// ID
        /// </summary>
        public uint Id_u32;

        /// <summary>
        /// Controller
        /// </summary>
        public bool Controller_bool;

        /// <summary>
        /// Connection time
        /// </summary>
        public DateTime ConnectedTime_dt;

        /// <summary>
        /// IP address
        /// </summary>
        public string Ip_ip;

        /// <summary>
        /// Host name
        /// </summary>
        public string Hostname_str;

        /// <summary>
        /// Point
        /// </summary>
        public uint Point_u32;

        /// <summary>
        /// Number of ports
        /// </summary>
        public uint NumPort_u32;

        /// <summary>
        /// Port
        /// </summary>
        public uint[] Ports_u32;

        /// <summary>
        /// Server certificate
        /// </summary>
        public byte[] ServerCert_bin;

        /// <summary>
        /// Number of farm HUB
        /// </summary>
        public uint NumFarmHub_u32;

        /// <summary>
        /// Farm HUB
        /// </summary>
        public VpnRpcFarmHub[] HubsList;

        /// <summary>
        /// Number of sessions
        /// </summary>
        public uint NumSessions_u32;

        /// <summary>
        /// Number of TCP connections
        /// </summary>
        public uint NumTcpConnections_u32;

        /// <summary>
        /// Performance ratio
        /// </summary>
        public uint Weight_u32;
    }

    /// <summary>
    /// Server farm configuration *
    /// </summary>
    public class VpnRpcFarm
    {
        /// <summary>
        /// Type of server
        /// </summary>
        public VpnRpcServerType ServerType_u32;

        /// <summary>
        /// Number of public ports
        /// </summary>
        public uint NumPort_u32;

        /// <summary>
        /// Public port list
        /// </summary>
        public uint[] Ports_u32;

        /// <summary>
        /// Public IP
        /// </summary>
        public string PublicIp_ip;

        /// <summary>
        /// Controller name
        /// </summary>
        public string ControllerName_str;

        /// <summary>
        /// Controller port
        /// </summary>
        public uint ControllerPort_u32;

        /// <summary>
        /// Member password
        /// </summary>
        public string MemberPasswordPlaintext_str;

        /// <summary>
        /// Performance ratio
        /// </summary>
        public uint Weight_u32;

        /// <summary>
        /// Only controller function
        /// </summary>
        public bool ControllerOnly_bool;
    }

    /// <summary>
    /// Log switch type
    /// </summary>
    public enum VpnRpcLogSwitchType
    {
        /// <summary>
        /// No switching
        /// </summary>
        No = 0,

        /// <summary>
        /// Secondly basis
        /// </summary>
        Second = 1,

        /// <summary>
        /// Minutely basis
        /// </summary>
        Minute = 2,

        /// <summary>
        /// Hourly basis
        /// </summary>
        Hour = 3,

        /// <summary>
        /// Daily basis
        /// </summary>
        Day = 4,

        /// <summary>
        /// Monthly basis
        /// </summary>
        Month = 5,
    }

    /// <summary>
    /// Packet log settings
    /// </summary>
    public enum VpnRpcPacketLogSetting
    {
        /// <summary>
        /// Not save
        /// </summary>
        None = 0,

        /// <summary>
        /// Only header
        /// </summary>
        Header = 1,

        /// <summary>
        /// All payloads
        /// </summary>
        All = 2,
    }

    /// <summary>
    /// Packet log settings array index
    /// </summary>
    public enum VpnRpcPacketLogSettingIndex
    {
        /// <summary>
        /// TCP connection log
        /// </summary>
        TcpConnection = 0,

        /// <summary>
        /// TCP packet log
        /// </summary>
        TcpAll = 1,

        /// <summary>
        /// DHCP Log
        /// </summary>
        Dhcp = 2,

        /// <summary>
        /// UDP log
        /// </summary>
        Udp = 3,

        /// <summary>
        /// ICMP log
        /// </summary>
        Icmp = 4,

        /// <summary>
        /// IP log
        /// </summary>
        Ip = 5,

        /// <summary>
        ///  ARP log
        /// </summary>
        Arp = 6,

        /// <summary>
        /// Ethernet log
        /// </summary>
        Ethernet = 7,
    }

    /// <summary>
    /// HUB log settings
    /// </summary>
    public class VpnRpcHubLog
    {
        /// <summary>
        /// HUB Name
        /// </summary>
        public string HubName_str;

        /// <summary>
        /// To save the security log
        /// </summary>
        public bool SaveSecurityLog_bool;

        /// <summary>
        /// Switching type of security log
        /// </summary>
        public VpnRpcLogSwitchType SecurityLogSwitchType_u32;

        /// <summary>
        /// To save the packet log
        /// </summary>
        public bool SavePacketLog_bool;

        /// <summary>
        /// Switching type of packet log
        /// </summary>
        public VpnRpcLogSwitchType PacketLogSwitchType_u32;

        /// <summary>
        /// Packet log settings (uint * 16 array)
        /// </summary>
        public VpnRpcPacketLogSetting[] PacketLogConfig_u32 = new VpnRpcPacketLogSetting[16];
    }

    /// <summary>
    /// Radius server options
    /// </summary>
    public class VpnRpcRadius
    {
        /// <summary>
        /// HUB Name
        /// </summary>
        public string HubName_str;

        /// <summary>
        /// Radius server name
        /// </summary>
        public string RadiusServerName_str;

        /// <summary>
        /// Radius port number
        /// </summary>
        public uint RadiusPort_u32;

        /// <summary>
        /// Secret key
        /// </summary>
        public string RadiusSecret_str;

        /// <summary>
        /// Radius retry interval
        /// </summary>
        public uint RadiusRetryInterval_u32;
    }

    /// <summary>
    /// Get the state HUB
    /// </summary>
    public class VpnRpcHubStatus
    {
        /// <summary>
        /// HUB Name
        /// </summary>
        public string HubName_str;

        /// <summary>
        /// Online
        /// </summary>
        public bool Online_bool;

        /// <summary>
        /// Type of HUB
        /// </summary>
        public VpnRpcHubType HubType_u32;

        /// <summary>
        /// Number of sessions
        /// </summary>
        public uint NumSessions_u32;

        /// <summary>
        /// Number of sessions (client)
        /// </summary>
        public uint NumSessionsClient_u32;

        /// <summary>
        /// Number of sessions (bridge)
        /// </summary>
        public uint NumSessionsBridge_u32;

        /// <summary>
        /// Number of Access list entries
        /// </summary>
        public uint NumAccessLists_u32;

        /// <summary>
        /// Number of users
        /// </summary>
        public uint NumUsers_u32;

        /// <summary>
        /// Number of groups
        /// </summary>
        public uint NumGroups_u32;

        /// <summary>
        /// Number of MAC table entries
        /// </summary>
        public uint NumMacTables_u32;

        /// <summary>
        /// Number of IP table entries
        /// </summary>
        public uint NumIpTables_u32;

        /// <summary>
        /// Number of broadcast packets (Recv)
        /// </summary>
        [JsonProperty("Recv.BroadcastBytes_u64")]
        public ulong Recv_BroadcastBytes_u64;

        /// <summary>
        /// Broadcast bytes (Recv)
        /// </summary>
        [JsonProperty("Recv.BroadcastCount_u64")]
        public ulong Recv_BroadcastCount_u64;

        /// <summary>
        /// Unicast count (Recv)
        /// </summary>
        [JsonProperty("Recv.UnicastBytes_u64")]
        public ulong Recv_UnicastBytes_u64;

        /// <summary>
        /// Unicast bytes (Recv)
        /// </summary>
        [JsonProperty("Recv.UnicastCount_u64")]
        public ulong Recv_UnicastCount_u64;

        /// <summary>
        /// Number of broadcast packets (Send)
        /// </summary>
        [JsonProperty("Send.BroadcastBytes_u64")]
        public ulong Send_BroadcastBytes_u64;

        /// <summary>
        /// Broadcast bytes (Send)
        /// </summary>
        [JsonProperty("Send.BroadcastCount_u64")]
        public ulong Send_BroadcastCount_u64;

        /// <summary>
        /// Unicast bytes (Send)
        /// </summary>
        [JsonProperty("Send.UnicastBytes_u64")]
        public ulong Send_UnicastBytes_u64;

        /// <summary>
        /// Unicast bytes (Send)
        /// </summary>
        [JsonProperty("Send.UnicastCount_u64")]
        public ulong Send_UnicastCount_u64;

        /// <summary>
        /// Whether SecureNAT is enabled
        /// </summary>
        public bool SecureNATEnabled_bool;

        /// <summary>
        /// Last communication date and time
        /// </summary>
        public DateTime LastCommTime_dt;

        /// <summary>
        /// Last login date and time
        /// </summary>
        public DateTime LastLoginTime_dt;

        /// <summary>
        /// Creation date and time
        /// </summary>
        public DateTime CreatedTime_dt;

        /// <summary>
        /// Number of logins
        /// </summary>
        public uint NumLogin_u32;
    }

    /// <summary>
    /// List of services provided by IPsec server
    /// </summary>
    public class VpnIPsecServices
    {
        /// <summary>
        /// Raw L2TP
        /// </summary>
        public bool L2TP_Raw_bool;

        /// <summary>
        /// L2TP over IPsec
        /// </summary>
        public bool L2TP_IPsec_bool;

        /// <summary>
        /// EtherIP over IPsec
        /// </summary>
        public bool EtherIP_IPsec_bool;

        /// <summary>
        /// IPsec pre-shared key
        /// </summary>
        public string IPsec_Secret_str;

        /// <summary>
        /// Default Virtual HUB name for L2TP connection
        /// </summary>
        public string L2TP_DefaultHub_str;
    }

    /// <summary>
    /// Keep alive protocol
    /// </summary>
    public enum VpnRpcKeepAliveProtocol
    {
        /// <summary>
        /// TCP
        /// </summary>
        TCP = 0,

        /// <summary>
        /// UDP
        /// </summary>
        UDP = 1,
    }

    /// <summary>
    /// Keep Alive setting
    /// </summary>
    public class VpnRpcKeep
    {
        /// <summary>
        /// Keep connected to the Internet
        /// </summary>
        public bool UseKeepConnect_bool;

        /// <summary>
        /// Host name
        /// </summary>
        public string KeepConnectHost_str;

        /// <summary>
        /// Port number
        /// </summary>
        public uint KeepConnectPort_u32;

        /// <summary>
        /// Protocol
        /// </summary>
        public VpnRpcKeepAliveProtocol KeepConnectProtocol_u32;

        /// <summary>
        /// Interval
        /// </summary>
        public uint KeepConnectInterval_u32;
    }

    /// <summary>
    /// License status of the server
    /// </summary>
    public class VpnRpcLicenseStatus
    {
        /// <summary>
        /// Edition ID
        /// </summary>
        public uint EditionId_u32;

        /// <summary>
        /// Edition name
        /// </summary>
        public string EditionStr_str;

        /// <summary>
        /// System ID
        /// </summary>
        public ulong SystemId_u64;

        /// <summary>
        /// System expiration date
        /// </summary>
        public ulong SystemExpires_u64;

        /// <summary>
        /// Maximum number of concurrent client connections
        /// </summary>
        public uint NumClientConnectLicense_u32;

        /// <summary>
        /// Available number of concurrent bridge connections
        /// </summary>
        public uint NumBridgeConnectLicense_u32;

        /// <summary>
        /// Subscription system is enabled
        /// </summary>
        public bool NeedSubscription_bool;

        /// <summary>
        /// Subscription expiration date
        /// </summary>
        public ulong SubscriptionExpires_u64;

        /// <summary>
        /// Whether the subscription is expired
        /// </summary>
        public bool IsSubscriptionExpired_bool;

        /// <summary>
        /// Maximum number of users
        /// </summary>
        public uint NumUserCreationLicense_u32;

        /// <summary>
        /// Operation of the enterprise function
        /// </summary>
        public bool AllowEnterpriseFunction_bool;

        /// <summary>
        /// Release date
        /// </summary>
        public ulong ReleaseDate_u64;
    }

    /// <summary>
    /// State of the client session
    /// </summary>
    public enum VpnRpcClientSessionStatus
    {
        /// <summary>
        /// Connecting
        /// </summary>
        Connecting = 0,

        /// <summary>
        /// Negotiating
        /// </summary>
        Negotiation = 1,

        /// <summary>
        /// During user authentication
        /// </summary>
        Auth = 2,

        /// <summary>
        /// Connection complete
        /// </summary>
        Established = 3,

        /// <summary>
        /// Wait to retry
        /// </summary>
        Retry = 4,

        /// <summary>
        /// Idle state
        /// </summary>
        Idle = 5,
    }

    /// <summary>
    /// Get the link state *
    /// </summary>
    public class VpnRpcLinkStatus
    {
        /// <summary>
        /// HUB Name
        /// </summary>
        public string HubName_Ex_str;

        /// <summary>
        /// Account name
        /// </summary>
        public string AccountName_utf;

        /// <summary>
        /// Operation flag
        /// </summary>
        public bool Active_bool;

        /// <summary>
        /// Connected flag
        /// </summary>
        public bool Connected_bool;

        /// <summary>
        /// Session status
        /// </summary>
        public VpnRpcClientSessionStatus SessionStatus_u32;

        /// <summary>
        /// Server name
        /// </summary>
        public string ServerName_str;

        /// <summary>
        /// Port number of the server
        /// </summary>
        public uint ServerPort_u32;

        /// <summary>
        /// Server product name
        /// </summary>
        public string ServerProductName_str;

        /// <summary>
        /// Server product version
        /// </summary>
        public uint ServerProductVer_u32;

        /// <summary>
        /// Server product build number
        /// </summary>
        public uint ServerProductBuild_u32;

        /// <summary>
        /// Server certificate
        /// </summary>
        public byte[] ServerX_bin;

        /// <summary>
        /// Client certificate
        /// </summary>
        public byte[] ClientX_bin;

        /// <summary>
        /// Connection start time
        /// </summary>
        public DateTime StartTime_dt;

        /// <summary>
        /// Connection completion time of the first connection
        /// </summary>
        public DateTime FirstConnectionEstablisiedTime_dt;

        /// <summary>
        /// Connection completion time of this connection
        /// </summary>
        public DateTime CurrentConnectionEstablishTime_dt;

        /// <summary>
        /// Number of connections have been established so far
        /// </summary>
        public uint NumConnectionsEatablished_u32;

        /// <summary>
        /// Half-connection
        /// </summary>
        public bool HalfConnection_bool;

        /// <summary>
        /// VoIP / QoS
        /// </summary>
        public bool QoS_bool;

        /// <summary>
        /// Maximum number of the TCP connections
        /// </summary>
        public uint MaxTcpConnections_u32;

        /// <summary>
        /// Number of current TCP connections
        /// </summary>
        public uint NumTcpConnections_u32;

        /// <summary>
        /// Number of inbound connections
        /// </summary>
        public uint NumTcpConnectionsUpload_u32;

        /// <summary>
        /// Number of outbound connections
        /// </summary>
        public uint NumTcpConnectionsDownload_u32;

        /// <summary>
        /// Use of encryption
        /// </summary>
        public bool UseEncrypt_bool;

        /// <summary>
        /// Cipher algorithm name
        /// </summary>
        public string CipherName_str;

        /// <summary>
        /// Use of compression
        /// </summary>
        public bool UseCompress_bool;

        /// <summary>
        /// R-UDP session
        /// </summary>
        public bool IsRUDPSession_bool;

        /// <summary>
        /// Physical communication protocol
        /// </summary>
        public string UnderlayProtocol_str;

        /// <summary>
        /// The UDP acceleration is enabled
        /// </summary>
        public bool IsUdpAccelerationEnabled_bool;

        /// <summary>
        /// Using the UDP acceleration function
        /// </summary>
        public bool IsUsingUdpAcceleration_bool;

        /// <summary>
        /// Session name
        /// </summary>
        public string SessionName_str;

        /// <summary>
        /// Connection name
        /// </summary>
        public string ConnectionName_str;

        /// <summary>
        /// Session key
        /// </summary>
        public byte[] SessionKey_bin;

        /// <summary>
        /// Total transmitted data size
        /// </summary>
        public ulong TotalSendSize_u64;

        /// <summary>
        /// Total received data size
        /// </summary>
        public ulong TotalRecvSize_u64;

        /// <summary>
        /// Total transmitted data size (no compression)
        /// </summary>
        public ulong TotalSendSizeReal_u64;

        /// <summary>
        /// Total received data size (no compression)
        /// </summary>
        public ulong TotalRecvSizeReal_u64;

        /// <summary>
        /// Bridge Mode
        /// </summary>
        public bool IsBridgeMode_bool;

        /// <summary>
        /// Monitor mode
        /// </summary>
        public bool IsMonitorMode_bool;

        /// <summary>
        /// VLAN ID
        /// </summary>
        public uint VLanId_u32;
    }

    /// <summary>
    /// Setting of SSTP and OpenVPN
    /// </summary>
    public class VpnOpenVpnSstpConfig
    {
        /// <summary>
        /// OpenVPN is enabled
        /// </summary>
        public bool EnableOpenVPN_bool;

        /// <summary>
        /// OpenVPN UDP port number list
        /// </summary>
        public string OpenVPNPortList_str;

        /// <summary>
        /// SSTP is enabled
        /// </summary>
        public bool EnableSSTP_bool;
    }

    /// <summary>
    /// Virtual host option
    /// </summary>
    public class VpnVhOption
    {
        /// <summary>
        /// Target Virtual HUB name
        /// </summary>
        public string RpcHubName_str;

        /// <summary>
        /// MAC address
        /// </summary>
        public byte[] MacAddress_bin;

        /// <summary>
        /// IP address
        /// </summary>
        public string Ip_ip;

        /// <summary>
        /// Subnet mask
        /// </summary>
        public string Mask_ip;

        /// <summary>
        /// Use flag of NAT function
        /// </summary>
        public bool UseNat_bool;

        /// <summary>
        /// MTU value
        /// </summary>
        public uint Mtu_u32;

        /// <summary>
        /// NAT TCP timeout in seconds
        /// </summary>
        public uint NatTcpTimeout_u32;

        /// <summary>
        /// NAT UDP timeout in seconds
        /// </summary>
        public uint NatUdpTimeout_u32;

        /// <summary>
        /// Using flag of DHCP function
        /// </summary>
        public bool UseDhcp_bool;

        /// <summary>
        /// Start of IP address range for DHCP distribution
        /// </summary>
        public string DhcpLeaseIPStart_ip;

        /// <summary>
        /// End of IP address range for DHCP distribution
        /// </summary>
        public string DhcpLeaseIPEnd_ip;

        /// <summary>
        /// DHCP subnet mask
        /// </summary>
        public string DhcpSubnetMask_ip;

        /// <summary>
        /// DHCP expiration time in seconds
        /// </summary>
        public uint DhcpExpireTimeSpan_u32;

        /// <summary>
        /// Assigned gateway address
        /// </summary>
        public string DhcpGatewayAddress_ip;

        /// <summary>
        /// Assigned DNS server address 1
        /// </summary>
        public string DhcpDnsServerAddress_ip;

        /// <summary>
        /// Assigned DNS server address 2
        /// </summary>
        public string DhcpDnsServerAddress2_ip;

        /// <summary>
        /// Assigned domain name
        /// </summary>
        public string DhcpDomainName_str;

        /// <summary>
        /// Save a log
        /// </summary>
        public bool SaveLog_bool;

        /// <summary>
        /// Apply flag for DhcpPushRoutes
        /// </summary>
        public bool ApplyDhcpPushRoutes_bool;

        /// <summary>
        /// DHCP pushing routes
        /// </summary>
        public string DhcpPushRoutes_str;
    }

    /// <summary>
    /// RPC_NAT_STATUS
    /// </summary>
    public class VpnRpcNatStatus
    {
        /// <summary>
        /// HUB name
        /// </summary>
        public string HubName_str;

        /// <summary>
        /// Number of TCP sessions
        /// </summary>
        public uint NumTcpSessions_u32;

        /// <summary>
        /// Ntmber of UDP sessions
        /// </summary>
        public uint NumUdpSessions_u32;

        /// <summary>
        /// Nymber of ICMP sessions
        /// </summary>
        public uint NumIcmpSessions_u32;

        /// <summary>
        /// Number of DNS sessions
        /// </summary>
        public uint NumDnsSessions_u32;

        /// <summary>
        /// Number of DHCP clients
        /// </summary>
        public uint NumDhcpClients_u32;

        /// <summary>
        /// Whether kernel mode
        /// </summary>
        public bool IsKernelMode_bool;

        /// <summary>
        /// Whether raw IP mode
        /// </summary>
        public bool IsRawIpMode_bool;
    }

    /// <summary>
    /// Key pair
    /// </summary>
    public class VpnRpcKeyPair
    {
        /// <summary>
        /// Certificate
        /// </summary>
        public byte[] Cert_bin;

        /// <summary>
        /// Secret key
        /// </summary>
        public byte[] Key_bin;

        /// <summary>
        /// Flag1
        /// </summary>
        public uint Flag1_u32;
    }

    /// <summary>
    /// String *
    /// </summary>
    public class VpnRpcStr
    {
        /// <summary>
        /// String
        /// </summary>
        public string String_str;
    }

    /// <summary>
    /// Type of VPN Server
    /// </summary>
    public enum VpnRpcServerType
    {
        /// <summary>
        /// Stand-alone server
        /// </summary>
        Standalone = 0,

        /// <summary>
        /// Farm controller server
        /// </summary>
        FarmController = 1,

        /// <summary>
        /// Farm member server
        /// </summary>
        FarmMember = 2,
    }

    /// <summary>
    /// Operating system type
    /// </summary>
    public enum VpnRpcOsType
    {
        /// <summary>
        /// Windows 95
        /// </summary>
        WINDOWS_95 = 1100,

        /// <summary>
        /// Windows 98
        /// </summary>
        WINDOWS_98 = 1200,

        /// <summary>
        /// Windows Me
        /// </summary>
        WINDOWS_ME = 1300,

        /// <summary>
        /// Windows (unknown)
        /// </summary>
        WINDOWS_UNKNOWN = 1400,

        /// <summary>
        /// Windows NT 4.0 Workstation
        /// </summary>
        WINDOWS_NT_4_WORKSTATION = 2100,

        /// <summary>
        /// Windows NT 4.0 Server
        /// </summary>
        WINDOWS_NT_4_SERVER = 2110,

        /// <summary>
        /// Windows NT 4.0 Server, Enterprise Edition
        /// </summary>
        WINDOWS_NT_4_SERVER_ENTERPRISE = 2111,

        /// <summary>
        /// Windows NT 4.0 Terminal Server
        /// </summary>
        WINDOWS_NT_4_TERMINAL_SERVER = 2112,

        /// <summary>
        /// BackOffice Server 4.5
        /// </summary>
        WINDOWS_NT_4_BACKOFFICE = 2113,

        /// <summary>
        /// Small Business Server 4.5
        /// </summary>
        WINDOWS_NT_4_SMS = 2114,

        /// <summary>
        /// Windows 2000 Professional
        /// </summary>
        WINDOWS_2000_PROFESSIONAL = 2200,

        /// <summary>
        /// Windows 2000 Server
        /// </summary>
        WINDOWS_2000_SERVER = 2211,

        /// <summary>
        /// Windows 2000 Advanced Server
        /// </summary>
        WINDOWS_2000_ADVANCED_SERVER = 2212,

        /// <summary>
        /// Windows 2000 Datacenter Server
        /// </summary>
        WINDOWS_2000_DATACENTER_SERVER = 2213,

        /// <summary>
        /// BackOffice Server 2000
        /// </summary>
        WINDOWS_2000_BACKOFFICE = 2214,

        /// <summary>
        /// Small Business Server 2000
        /// </summary>
        WINDOWS_2000_SBS = 2215,

        /// <summary>
        /// Windows XP Home Edition
        /// </summary>
        WINDOWS_XP_HOME = 2300,

        /// <summary>
        /// Windows XP Professional
        /// </summary>
        WINDOWS_XP_PROFESSIONAL = 2301,

        /// <summary>
        /// Windows Server 2003 Web Edition
        /// </summary>
        WINDOWS_2003_WEB = 2410,

        /// <summary>
        /// Windows Server 2003 Standard Edition
        /// </summary>
        WINDOWS_2003_STANDARD = 2411,

        /// <summary>
        /// Windows Server 2003 Enterprise Edition
        /// </summary>
        WINDOWS_2003_ENTERPRISE = 2412,

        /// <summary>
        /// Windows Server 2003 DataCenter Edition
        /// </summary>
        WINDOWS_2003_DATACENTER = 2413,

        /// <summary>
        /// BackOffice Server 2003
        /// </summary>
        WINDOWS_2003_BACKOFFICE = 2414,

        /// <summary>
        /// Small Business Server 2003
        /// </summary>
        WINDOWS_2003_SBS = 2415,

        /// <summary>
        /// Windows Vista
        /// </summary>
        WINDOWS_LONGHORN_PROFESSIONAL = 2500,

        /// <summary>
        /// Windows Server 2008
        /// </summary>
        WINDOWS_LONGHORN_SERVER = 2510,

        /// <summary>
        /// Windows 7
        /// </summary>
        WINDOWS_7 = 2600,

        /// <summary>
        /// Windows Server 2008 R2
        /// </summary>
        WINDOWS_SERVER_2008_R2 = 2610,

        /// <summary>
        /// Windows 8
        /// </summary>
        WINDOWS_8 = 2700,

        /// <summary>
        /// Windows Server 2012
        /// </summary>
        WINDOWS_SERVER_8 = 2710,

        /// <summary>
        /// Windows 8.1
        /// </summary>
        WINDOWS_81 = 2701,

        /// <summary>
        /// Windows Server 2012 R2
        /// </summary>
        WINDOWS_SERVER_81 = 2711,

        /// <summary>
        /// Windows 10
        /// </summary>
        WINDOWS_10 = 2702,

        /// <summary>
        /// Windows Server 10
        /// </summary>
        WINDOWS_SERVER_10 = 2712,

        /// <summary>
        /// Windows 11 or later
        /// </summary>
        WINDOWS_11 = 2800,

        /// <summary>
        /// Windows Server 11 or later
        /// </summary>
        WINDOWS_SERVER_11 = 2810,

        /// <summary>
        /// Unknown UNIX
        /// </summary>
        UNIX_UNKNOWN = 3000,

        /// <summary>
        /// Linux
        /// </summary>
        LINUX = 3100,

        /// <summary>
        /// Solaris
        /// </summary>
        SOLARIS = 3200,

        /// <summary>
        /// Cygwin
        /// </summary>
        CYGWIN = 3300,

        /// <summary>
        /// BSD
        /// </summary>
        BSD = 3400,

        /// <summary>
        /// MacOS X
        /// </summary>
        MACOS_X = 3500,
    }

    /// <summary>
    /// VPN Server Information
    /// </summary>
    public class VpnRpcServerInfo
    {
        /// <summary>
        /// Server product name
        /// </summary>
        public string ServerProductName_str;

        /// <summary>
        /// Server version string
        /// </summary>
        public string ServerVersionString_str;

        /// <summary>
        /// Server build information string
        /// </summary>
        public string ServerBuildInfoString_str;

        /// <summary>
        /// Server version integer value
        /// </summary>
        public uint ServerVerInt_u32;

        /// <summary>
        /// Server build number integer value
        /// </summary>
        public uint ServerBuildInt_u32;

        /// <summary>
        /// Server host name
        /// </summary>
        public string ServerHostName_str;

        /// <summary>
        /// Type of server
        /// </summary>
        public VpnRpcServerType ServerType_u32;

        /// <summary>
        /// Build date and time of the server
        /// </summary>
        public DateTime ServerBuildDate_dt;

        /// <summary>
        /// Family name
        /// </summary>
        public string ServerFamilyName_str;

        /// <summary>
        /// OS type
        /// </summary>
        public VpnRpcOsType OsType_u32;

        /// <summary>
        /// Service pack number
        /// </summary>
        public uint OsServicePack_u32;

        /// <summary>
        /// OS system name
        /// </summary>
        public string OsSystemName_str;

        /// <summary>
        /// OS product name
        /// </summary>
        public string OsProductName_str;

        /// <summary>
        /// OS vendor name
        /// </summary>
        public string OsVendorName_str;

        /// <summary>
        /// OS version
        /// </summary>
        public string OsVersion_str;

        /// <summary>
        /// Kernel name
        /// </summary>
        public string KernelName_str;

        /// <summary>
        /// Kernel version
        /// </summary>
        public string KernelVersion_str;
    }

    /// <summary>
    /// Server status
    /// </summary>
    public class VpnRpcServerStatus
    {
        /// <summary>
        /// Type of server
        /// </summary>
        public VpnRpcServerType ServerType_u32;

        /// <summary>
        /// Total number of TCP connections
        /// </summary>
        public uint NumTcpConnections_u32;

        /// <summary>
        /// Number of Local TCP connections
        /// </summary>
        public uint NumTcpConnectionsLocal_u32;

        /// <summary>
        /// Number of remote TCP connections
        /// </summary>
        public uint NumTcpConnectionsRemote_u32;

        /// <summary>
        /// Total number of HUBs
        /// </summary>
        public uint NumHubTotal_u32;

        /// <summary>
        /// Nymber of stand-alone HUB
        /// </summary>
        public uint NumHubStandalone_u32;

        /// <summary>
        /// Number of static HUBs
        /// </summary>
        public uint NumHubStatic_u32;

        /// <summary>
        /// Number of Dynamic HUBs
        /// </summary>
        public uint NumHubDynamic_u32;

        /// <summary>
        /// Total number of sessions
        /// </summary>
        public uint NumSessionsTotal_u32;

        /// <summary>
        /// Number of Local sessions (only controller)
        /// </summary>
        public uint NumSessionsLocal_u32;

        /// <summary>
        /// The number of remote sessions (other than the controller)
        /// </summary>
        public uint NumSessionsRemote_u32;

        /// <summary>
        /// Number of MAC table entries
        /// </summary>
        public uint NumMacTables_u32;

        /// <summary>
        /// Number of IP table entries
        /// </summary>
        public uint NumIpTables_u32;

        /// <summary>
        /// Number of users
        /// </summary>
        public uint NumUsers_u32;

        /// <summary>
        /// Number of groups
        /// </summary>
        public uint NumGroups_u32;

        /// <summary>
        /// Number of assigned bridge licenses
        /// </summary>
        public uint AssignedBridgeLicenses_u32;

        /// <summary>
        /// Number of assigned client licenses
        /// </summary>
        public uint AssignedClientLicenses_u32;

        /// <summary>
        /// Number of Assigned bridge license (cluster-wide)
        /// </summary>
        public uint AssignedBridgeLicensesTotal_u32;

        /// <summary>
        /// Number of assigned client licenses (cluster-wide)
        /// </summary>
        public uint AssignedClientLicensesTotal_u32;

        /// <summary>
        /// Number of broadcast packets (Recv)
        /// </summary>
        [JsonProperty("Recv.BroadcastBytes_u64")]
        public ulong Recv_BroadcastBytes_u64;

        /// <summary>
        /// Broadcast bytes (Recv)
        /// </summary>
        [JsonProperty("Recv.BroadcastCount_u64")]
        public ulong Recv_BroadcastCount_u64;

        /// <summary>
        /// Unicast count (Recv)
        /// </summary>
        [JsonProperty("Recv.UnicastBytes_u64")]
        public ulong Recv_UnicastBytes_u64;

        /// <summary>
        /// Unicast bytes (Recv)
        /// </summary>
        [JsonProperty("Recv.UnicastCount_u64")]
        public ulong Recv_UnicastCount_u64;

        /// <summary>
        /// Number of broadcast packets (Send)
        /// </summary>
        [JsonProperty("Send.BroadcastBytes_u64")]
        public ulong Send_BroadcastBytes_u64;

        /// <summary>
        /// Broadcast bytes (Send)
        /// </summary>
        [JsonProperty("Send.BroadcastCount_u64")]
        public ulong Send_BroadcastCount_u64;

        /// <summary>
        /// Unicast bytes (Send)
        /// </summary>
        [JsonProperty("Send.UnicastBytes_u64")]
        public ulong Send_UnicastBytes_u64;

        /// <summary>
        /// Unicast bytes (Send)
        /// </summary>
        [JsonProperty("Send.UnicastCount_u64")]
        public ulong Send_UnicastCount_u64;

        /// <summary>
        /// Current time
        /// </summary>
        public DateTime CurrentTime_dt;

        /// <summary>
        /// Current tick
        /// </summary>
        public ulong CurrentTick_u64;

        /// <summary>
        /// Start-up time
        /// </summary>
        public DateTime StartTime_dt;

        /// <summary>
        /// Memory information: Total Memory
        /// </summary>
        public ulong TotalMemory_u64;

        /// <summary>
        /// Memory information: Used Memory
        /// </summary>
        public ulong UsedMemory_u64;

        /// <summary>
        /// Memory information: Free Memory
        /// </summary>
        public ulong FreeMemory_u64;

        /// <summary>
        /// Memory information: Total Phys
        /// </summary>
        public ulong TotalPhys_u64;

        /// <summary>
        /// Memory information: Used Phys
        /// </summary>
        public ulong UsedPhys_u64;

        /// <summary>
        /// Memory information: Free Phys
        /// </summary>
        public ulong FreePhys_u64;
    }

    /// <summary>
    /// Session status *
    /// </summary>
    public class VpnRpcSessionStatus
    {
        /// <summary>
        /// HUB Name
        /// </summary>
        public string HubName_str;

        /// <summary>
        /// Session name
        /// </summary>
        public string Name_str;

        /// <summary>
        /// User name
        /// </summary>
        public string Username_str;

        /// <summary>
        /// Real user name
        /// </summary>
        public string RealUsername_str;

        /// <summary>
        /// Group name
        /// </summary>
        public string GroupName_str;

        /// <summary>
        /// Link mode
        /// </summary>
        public bool LinkMode_bool;

        /// <summary>
        /// Client IP address
        /// </summary>
        public string Client_Ip_Address_ip;

        /// <summary>
        /// Client host name
        /// </summary>
        [JsonProperty("SessionStatus_ClientHostName_str")]
        public string ClientHostName_str;

        /// <summary>
        /// Operation flag
        /// </summary>
        public bool Active_bool;

        /// <summary>
        /// Connected flag
        /// </summary>
        public bool Connected_bool;

        /// <summary>
        /// Session status
        /// </summary>
        public VpnRpcClientSessionStatus SessionStatus_u32;

        /// <summary>
        /// Server name
        /// </summary>
        public string ServerName_str;

        /// <summary>
        /// Port number of the server
        /// </summary>
        public uint ServerPort_u32;

        /// <summary>
        /// Server product name
        /// </summary>
        public string ServerProductName_str;

        /// <summary>
        /// Server product version
        /// </summary>
        public uint ServerProductVer_u32;

        /// <summary>
        /// Server product build number
        /// </summary>
        public uint ServerProductBuild_u32;

        /// <summary>
        /// Connection start time
        /// </summary>
        public DateTime StartTime_dt;

        /// <summary>
        /// Connection completion time of the first connection
        /// </summary>
        public DateTime FirstConnectionEstablisiedTime_dt;

        /// <summary>
        /// Connection completion time of this connection
        /// </summary>
        public DateTime CurrentConnectionEstablishTime_dt;

        /// <summary>
        /// Number of connections have been established so far
        /// </summary>
        public uint NumConnectionsEatablished_u32;

        /// <summary>
        /// Half-connection
        /// </summary>
        public bool HalfConnection_bool;

        /// <summary>
        /// VoIP / QoS
        /// </summary>
        public bool QoS_bool;

        /// <summary>
        /// Maximum number of the TCP connections
        /// </summary>
        public uint MaxTcpConnections_u32;

        /// <summary>
        /// Number of current TCP connections
        /// </summary>
        public uint NumTcpConnections_u32;

        /// <summary>
        /// Number of inbound connections
        /// </summary>
        public uint NumTcpConnectionsUpload_u32;

        /// <summary>
        /// Number of outbound connections
        /// </summary>
        public uint NumTcpConnectionsDownload_u32;

        /// <summary>
        /// Use of encryption
        /// </summary>
        public bool UseEncrypt_bool;

        /// <summary>
        /// Cipher algorithm name
        /// </summary>
        public string CipherName_str;

        /// <summary>
        /// Use of compression
        /// </summary>
        public bool UseCompress_bool;

        /// <summary>
        /// R-UDP session
        /// </summary>
        public bool IsRUDPSession_bool;

        /// <summary>
        /// Physical communication protocol
        /// </summary>
        public string UnderlayProtocol_str;

        /// <summary>
        /// The UDP acceleration is enabled
        /// </summary>
        public bool IsUdpAccelerationEnabled_bool;

        /// <summary>
        /// Using the UDP acceleration function
        /// </summary>
        public bool IsUsingUdpAcceleration_bool;

        /// <summary>
        /// Session name
        /// </summary>
        public string SessionName_str;

        /// <summary>
        /// Connection name
        /// </summary>
        public string ConnectionName_str;

        /// <summary>
        /// Session key
        /// </summary>
        public byte[] SessionKey_bin;

        /// <summary>
        /// Total transmitted data size
        /// </summary>
        public ulong TotalSendSize_u64;

        /// <summary>
        /// Total received data size
        /// </summary>
        public ulong TotalRecvSize_u64;

        /// <summary>
        /// Total transmitted data size (no compression)
        /// </summary>
        public ulong TotalSendSizeReal_u64;

        /// <summary>
        /// Total received data size (no compression)
        /// </summary>
        public ulong TotalRecvSizeReal_u64;

        /// <summary>
        /// Bridge Mode
        /// </summary>
        public bool IsBridgeMode_bool;

        /// <summary>
        /// Monitor mode
        /// </summary>
        public bool IsMonitorMode_bool;

        /// <summary>
        /// VLAN ID
        /// </summary>
        public uint VLanId_u32;

        /// <summary>
        /// Client product name
        /// </summary>
        public string ClientProductName_str;

        /// <summary>
        /// Client version
        /// </summary>
        public uint ClientProductVer_u32;

        /// <summary>
        /// Client build number
        /// </summary>
        public uint ClientProductBuild_u32;

        /// <summary>
        /// Client OS name
        /// </summary>
        public string ClientOsName_str;

        /// <summary>
        /// Client OS version
        /// </summary>
        public string ClientOsVer_str;

        /// <summary>
        /// Client OS Product ID
        /// </summary>
        public string ClientOsProductId_str;

        /// <summary>
        /// Client host name
        /// </summary>
        public string ClientHostname_str;

        /// <summary>
        /// Unique ID
        /// </summary>
        public byte[] UniqueId_bin;
    }

    /// <summary>
    /// Set the special listener
    /// </summary>
    public class VpnRpcSpecialListener
    {
        /// <summary>
        /// VPN over ICMP
        /// </summary>
        public bool VpnOverIcmpListener_bool;

        /// <summary>
        /// VPN over DNS
        /// </summary>
        public bool VpnOverDnsListener_bool;
    }

    /// <summary>
    /// Syslog configuration
    /// </summary>
    public class VpnSyslogSetting
    {
        /// <summary>
        /// Save type
        /// </summary>
        public uint SaveType_u32;

        /// <summary>
        /// Host name
        /// </summary>
        public string Hostname_str;

        /// <summary>
        /// Port number
        /// </summary>
        public uint Port_u32;
    }

    /// <summary>
    /// VPN Gate Server Config
    /// </summary>
    public class VpnVgsConfig
    {
        /// <summary>
        /// Active flag
        /// </summary>
        public bool IsEnabled_bool;

        /// <summary>
        /// Message
        /// </summary>
        public string Message_utf;

        /// <summary>
        /// Owner name
        /// </summary>
        public string Owner_utf;

        /// <summary>
        /// Report
        /// </summary>
        public string Abuse_utf;

        /// <summary>
        /// Log save flag
        /// </summary>
        public bool NoLog_bool;

        /// <summary>
        /// Save log permanently
        /// </summary>
        public bool LogPermanent_bool;

        /// <summary>
        /// Enable the L2TP VPN function
        /// </summary>
        public bool EnableL2TP_bool;
    }

    /// <summary>
    /// Read a Log file
    /// </summary>
    public class VpnRpcReadLogFile
    {
        /// <summary>
        /// Server name
        /// </summary>
        public string ServerName_str;

        /// <summary>
        /// File Path
        /// </summary>
        public string FilePath_str;

        /// <summary>
        /// Offset
        /// </summary>
        public uint Offset_u32;

        /// <summary>
        /// Buffer
        /// </summary>
        // TODO: BUF *Buffer;
    }

    /// <summary>
    /// Rename link
    /// </summary>
    public class VpnRpcRenameLink
    {
        /// <summary>
        /// HUB Name
        /// </summary>
        public string HubName_str;

        /// <summary>
        /// Old account name
        /// </summary>
        public string OldAccountName_utf;

        /// <summary>
        /// New account name
        /// </summary>
        public string NewAccountName_utf;
    }

    /// <summary>
    /// Online or offline the HUB
    /// </summary>
    public class VpnRpcSetHubOnline
    {
        /// <summary>
        /// HUB Name
        /// </summary>
        public string HubName_str;

        /// <summary>
        /// Online / offline flag
        /// </summary>
        public bool Online_bool;
    }

    /// <summary>
    /// Set Password
    /// </summary>
    public class VpnRpcSetPassword
    {
        /// <summary>
        /// Password
        /// </summary>
        public string PlainTextPassword_str;
    }

}
