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
    /// Type of VPN Server
    /// </summary>
    public enum RpcServerType
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
    public enum RpcOsType
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
    public class RpcServerInfo
    {
        public string ServerProductName_str;
        public string ServerVersionString_str;
        public string ServerBuildInfoString_str;
        public uint ServerVerInt_u32;
        public uint ServerBuildInt_u32;
        public string ServerHostName_str;
        public RpcServerType ServerType_u32;
        public DateTime ServerBuildDate_dt;
        public string ServerFamilyName_str;
        public RpcOsType OsType_u32;
        public uint OsServicePack_u32;
        public string OsSystemName_str;
        public string OsProductName_str;
        public string OsVendorName_str;
        public string OsVersion_str;
        public string KernelName_str;
        public string KernelVersion_str;
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
        /// Access list
        /// </summary>
        // TODO: ACCESS Access;
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
        // TODO: X *Cert;
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
        /// Traffic data
        /// </summary>
        // TODO: TRAFFIC Traffic;

        /// <summary>
        /// Policy
        /// </summary>
        // TODO: POLICY *Policy;
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
        public byte[] HashedPassword_bin;

        /// <summary>
        /// Administrator password
        /// </summary>
        public byte[] SecurePassword_bin;

        /// <summary>
        /// Online flag
        /// </summary>
        public bool Online_bool;

        /// <summary>
        /// HUB options
        /// </summary>
        // TODO: RPC_HUB_OPTION HubOption;

        /// <summary>
        /// Type of HUB
        /// </summary>
        public uint HubType_u32;
    }

    /// <summary>
    /// Create and set of link *
    /// </summary>
    public class VpnRpcCreateLink
    {
        /// <summary>
        /// HUB Name
        /// </summary>
        public string HubName_str;

        /// <summary>
        /// Online flag
        /// </summary>
        public bool Online_bool;

        /// <summary>
        /// Client Option
        /// </summary>
        // TODO: CLIENT_OPTION *ClientOption;

        /// <summary>
        /// Client authentication data
        /// </summary>
        // TODO: CLIENT_AUTH *ClientAuth;

        /// <summary>
        /// Policy
        /// </summary>
        // TODO: POLICY Policy;

        /// <summary>
        /// Validate the server certificate
        /// </summary>
        public bool CheckServerCert_bool;

        /// <summary>
        /// Server certificate
        /// </summary>
        // TODO: X *ServerCert;
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
        public ulong CreatedTime_u64;

        /// <summary>
        /// Updating date
        /// </summary>
        public ulong UpdatedTime_u64;

        /// <summary>
        /// Expiration date
        /// </summary>
        public ulong ExpireTime_u64;

        /// <summary>
        /// Authentication method
        /// </summary>
        public uint AuthType_u32;

        /// <summary>
        /// Authentication data
        /// </summary>
        // TODO: void *AuthData;

        /// <summary>
        /// Number of logins
        /// </summary>
        public uint NumLogin_u32;

        /// <summary>
        /// Traffic data
        /// </summary>
        // TODO: TRAFFIC Traffic;

        /// <summary>
        /// Policy
        /// </summary>
        // TODO: POLICY *Policy;
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
    /// Enumeration of the access list *
    /// </summary>
    public class VpnRpcEnumAccessList
    {
        /// <summary>
        /// HUB Name
        /// </summary>
        public string HubName_str;

        /// <summary>
        /// Number of Access list entries
        /// </summary>
        public uint NumAccess_u32;

        /// <summary>
        /// Access list
        /// </summary>
        // TODO: ACCESS *Accesses;
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
        /// CA number
        /// </summary>
        public uint NumCa_u32;

        /// <summary>
        /// CA
        /// </summary>
        // TODO: RPC_HUB_ENUM_CA_ITEM *Ca;
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
        // TODO: RPC_ENUM_CONNECTION_ITEM *Connections;
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
    /// RPC_ENUM_DHCP *
    /// </summary>
    public class VpnRpcEnumDhcp
    {
        /// <summary>
        /// HUB name
        /// </summary>
        public string HubName_str;

        /// <summary>
        /// Number of items
        /// </summary>
        public uint NumItem_u32;

        /// <summary>
        /// Item
        /// </summary>
        // TODO: RPC_ENUM_DHCP_ITEM *Items;
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
    /// Ethernet enumeration
    /// </summary>
    public class VpnRpcEnumEth
    {
        /// <summary>
        /// Number of items
        /// </summary>
        public uint NumItem_u32;

        /// <summary>
        /// Item
        /// </summary>
        // TODO: RPC_ENUM_ETH_ITEM *Items;
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
    /// Server farm member enumeration *
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
        // TODO: RPC_ENUM_FARM_ITEM *Farms;
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
        /// Number of groups
        /// </summary>
        public uint NumGroup_u32;

        /// <summary>
        /// Group
        /// </summary>
        // TODO: RPC_ENUM_GROUP_ITEM *Groups;
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
        // TODO: RPC_ENUM_HUB_ITEM *Hubs;
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
        /// Number of tables
        /// </summary>
        public uint NumIpTable_u32;

        /// <summary>
        /// MAC table
        /// </summary>
        // TODO: RPC_ENUM_IP_TABLE_ITEM *IpTables;
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
    /// Enumeration of the link *
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
        // TODO: RPC_ENUM_LINK_ITEM *Links;
    }

    /// <summary>
    /// List of listeners *
    /// </summary>
    public class VpnRpcListenerList
    {
        /// <summary>
        /// Number of ports
        /// </summary>
        public uint NumPort_u32;

        /// <summary>
        /// Port List
        /// </summary>
        public uint Ports_u32;

        /// <summary>
        /// Effective state
        /// </summary>
        public bool Enables_bool;

        /// <summary>
        /// An error occurred
        /// </summary>
        public bool Errors_bool;
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
    /// Enumeration of the MAC table
    /// </summary>
    public class VpnRpcEnumMacTable
    {
        /// <summary>
        /// HUB Name
        /// </summary>
        public string HubName_str;

        /// <summary>
        /// Number of tables
        /// </summary>
        public uint NumMacTable_u32;

        /// <summary>
        /// MAC table
        /// </summary>
        // TODO: RPC_ENUM_MAC_TABLE_ITEM *MacTables;
    }

    /// <summary>
    /// RPC_ENUM_NAT *
    /// </summary>
    public class VpnRpcEnumNat
    {
        /// <summary>
        /// HUB name
        /// </summary>
        public string HubName_str;

        /// <summary>
        /// Number of items
        /// </summary>
        public uint NumItem_u32;

        /// <summary>
        /// Item
        /// </summary>
        // TODO: RPC_ENUM_NAT_ITEM *Items;
    }

    /// <summary>
    /// Enumerate sessions *
    /// </summary>
    public class VpnRpcEnumSession
    {
        /// <summary>
        /// HUB Name
        /// </summary>
        public string HubName_str;

        /// <summary>
        /// Number of sessions
        /// </summary>
        public uint NumSession_u32;

        /// <summary>
        /// Session list
        /// </summary>
        // TODO: struct RPC_ENUM_SESSION_ITEM *Sessions;
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
        /// Number of users
        /// </summary>
        public uint NumUser_u32;

        /// <summary>
        /// User
        /// </summary>
        // TODO: RPC_ENUM_USER_ITEM *Users;
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
        // TODO: X *Cert;
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
        public uint Type_u32;

        /// <summary>
        /// Host name
        /// </summary>
        public string Hostname_str;

        /// <summary>
        /// IP address
        /// </summary>
        public uint Ip_u32;

        /// <summary>
        /// Port number
        /// </summary>
        public uint Port_u32;

        /// <summary>
        /// Connected time
        /// </summary>
        public ulong ConnectedTime_u64;

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
        public uint Ip_u32;

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
        public ulong StartedTime_u64;

        /// <summary>
        /// First connection time
        /// </summary>
        public ulong FirstConnectedTime_u64;

        /// <summary>
        /// Connection time of this time
        /// </summary>
        public ulong CurrentConnectedTime_u64;

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
        public ulong ConnectedTime_u64;

        /// <summary>
        /// IP address
        /// </summary>
        public uint Ip_u32;

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
        public uint Ports_u32;

        /// <summary>
        /// Server certificate
        /// </summary>
        // TODO: X *ServerCert;

        /// <summary>
        /// Number of farm HUB
        /// </summary>
        public uint NumFarmHub_u32;

        /// <summary>
        /// Farm HUB
        /// </summary>
        // TODO: RPC_FARM_HUB *FarmHubs;

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
        public uint ServerType_u32;

        /// <summary>
        /// Number of public ports
        /// </summary>
        public uint NumPort_u32;

        /// <summary>
        /// Public port list
        /// </summary>
        public uint Ports_u32;

        /// <summary>
        /// Public IP
        /// </summary>
        public uint PublicIp_u32;

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
        public byte[] MemberPassword_bin;

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
    /// HUB log settings
    /// </summary>
    public class VpnRpcHubLog
    {
        /// <summary>
        /// HUB Name
        /// </summary>
        public string HubName_str;

        /// <summary>
        /// Log Settings
        /// </summary>
        // TODO: HUB_LOG LogSetting;
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
        public uint HubType_u32;

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
        /// Traffic
        /// </summary>
        // TODO: TRAFFIC Traffic;

        /// <summary>
        /// Whether SecureNAT is enabled
        /// </summary>
        public bool SecureNATEnabled_bool;

        /// <summary>
        /// Last communication date and time
        /// </summary>
        public ulong LastCommTime_u64;

        /// <summary>
        /// Last login date and time
        /// </summary>
        public ulong LastLoginTime_u64;

        /// <summary>
        /// Creation date and time
        /// </summary>
        public ulong CreatedTime_u64;

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
    /// KEEP setting
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
        public uint KeepConnectProtocol_u32;

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
    /// Get the link state *
    /// </summary>
    public class VpnRpcLinkStatus
    {
        /// <summary>
        /// HUB Name
        /// </summary>
        public string HubName_str;

        /// <summary>
        /// Account name
        /// </summary>
        public string AccountName_utf;

        /// <summary>
        /// Status
        /// </summary>
        // TODO: RPC_CLIENT_GET_CONNECTION_STATUS Status;
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
        public string HubName_str;

        /// <summary>
        /// MAC address
        /// </summary>
        public byte[] MacAddress_bin;

        public byte[] Padding_bin;

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
        /// DHCP expiration date
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
        // TODO: X *Cert;

        /// <summary>
        /// Secret key
        /// </summary>
        // TODO: K *Key;

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
    /// Server Information *
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
        public uint ServerType_u32;

        /// <summary>
        /// Build date and time of the server
        /// </summary>
        public ulong ServerBuildDate_u64;

        /// <summary>
        /// Family name
        /// </summary>
        public string ServerFamilyName_str;

        /// <summary>
        /// OS information
        /// </summary>
        // TODO: OS_INFO OsInfo;
    }

    /// <summary>
    /// Server status
    /// </summary>
    public class VpnRpcServerStatus
    {
        /// <summary>
        /// Type of server
        /// </summary>
        public uint ServerType_u32;

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
        /// Traffic information
        /// </summary>
        // TODO: TRAFFIC Traffic;

        /// <summary>
        /// Current time
        /// </summary>
        public ulong CurrentTime_u64;

        /// <summary>
        /// Current tick
        /// </summary>
        public ulong CurrentTick_u64;

        /// <summary>
        /// Start-up time
        /// </summary>
        public ulong StartTime_u64;

        /// <summary>
        /// Memory information
        /// </summary>
        // TODO: MEMINFO MemInfo;
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
        /// Status
        /// </summary>
        // TODO: RPC_CLIENT_GET_CONNECTION_STATUS Status;

        /// <summary>
        /// Client IP address
        /// </summary>
        public uint ClientIp_u32;

        /// <summary>
        /// Client IPv6 address
        /// </summary>
        public byte[] ClientIp6_bin;

        /// <summary>
        /// Client host name
        /// </summary>
        public string ClientHostName_str;

        /// <summary>
        /// Node information
        /// </summary>
        // TODO: NODE_INFO NodeInfo;
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
        /// Hashed password
        /// </summary>
        public byte[] HashedPassword_bin;
    }

}
