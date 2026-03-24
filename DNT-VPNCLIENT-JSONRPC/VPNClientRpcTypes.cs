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
    /// <summary>
    /// A data structure that stores the result of retrieving the VPN Client version information.
    /// </summary>
    public class VpnRpcClientVersion
    {
        /// <summary>
        /// Client product name
        /// </summary>
        public string ClientProductName_str;

        /// <summary>
        /// Client version string
        /// </summary>
        public string ClientVersionString_str;

        /// <summary>
        /// Build client information string
        /// </summary>
        public string ClientBuildInfoString_str;

        /// <summary>
        /// Client version integer value
        /// </summary>
        public uint ClientVerInt_u32;

        /// <summary>
        /// Client build number integer value
        /// </summary>
        public uint ClientBuildInt_u32;

        /// <summary>
        /// Process ID
        /// </summary>
        public uint ProcessId_u32;

        /// <summary>
        /// OS type
        /// </summary>
        public uint OsType_u32;

        /// <summary>
        /// Whether a virtual LAN card name must be "VLAN" + number
        /// </summary>
        public bool IsVLanNameRegulated_bool;

        /// <summary>
        /// Whether the VPN Gate Client is supported
        /// </summary>
        public bool IsVgcSupported_bool;

        /// <summary>
        /// Display a VPN Gate Client link
        /// </summary>
        public bool ShowVgcLink_bool;

        /// <summary>
        /// Client OD
        /// </summary>
        public string ClientId_str;
    }

    /// <summary>
    /// Settings to restrict operations performed by users of the VPN Client Manager
    /// </summary>
    public class VpnCmSetting
    {
        /// <summary>
        /// A flag indicating whether EasyMode is enabled
        /// </summary>
        public bool EasyMode_bool;

        /// <summary>
        /// A flag indicating whether SettingLock is enabled
        /// </summary>
        public bool LockMode_bool;

        /// <summary>
        /// This is the hash of the VPN Client Manager UI configuration lock administrative password. Assign the 20-byte result of hashing the password data, represented as an ASCII string, using SHA-0. If all bytes are 0, it indicates that no password has been set. Note: This is a password used to lock GUI configuration items, allowing the GUI program to gray out (disable) setting changes on its own. It does not prevent connections to the RPC entry point and does not provide any inherent security.
        /// </summary>
        public byte[] HashedPassword_bin;
    }

    /// <summary>
    /// VPN Client Password Setting (for set direction)
    /// </summary>
    public class VpnRpcClientPassword
    {
        /// <summary>
        /// Password
        /// </summary>
        public string Password_str;

        /// <summary>
        /// The flag whether the password is required only remote access
        /// </summary>
        public bool PasswordRemoteOnly_bool;
    }

    /// <summary>
    /// VPN Client Password Setting (for get direction)
    /// </summary>
    public class VpnRpcClientPasswordSetting
    {
        /// <summary>
        /// The flag whether password exists
        /// </summary>
        public bool IsPasswordPresented_bool;

        /// <summary>
        /// The flag whether the password is required only remote access
        /// </summary>
        public bool PasswordRemoteOnly_bool;
    }

    /// <summary>
    /// Certificate enumeration item
    /// </summary>
    public class VpnRpcClientEnumCAItem
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
        public ulong Expires_u64;
    }

    /// <summary>
    /// Certificate enumeration
    /// </summary>
    public class VpnRpcClientEnumCA
    {
        /// <summary>
        /// Number of items
        /// </summary>
        public uint NumItem_u32;

        /// <summary>
        /// Item
        /// </summary>
        public VpnRpcClientEnumCAItem[] CAList;
    }

    /// <summary>
    /// Certificate item
    /// </summary>
    public class VpnRpcCert
    {
        /// <summary>
        /// The body of the X.509 certificate
        /// </summary>
        public byte[] X_bin;
    }

    /// <summary>
    /// Delete the certificate
    /// </summary>
    public class VpnRpcClientDeleteCA
    {
        /// <summary>
        /// Certificate key
        /// </summary>
        public uint Key_u32;
    }

    /// <summary>
    /// Get the certificate
    /// </summary>
    public class VpnRpcGetCA
    {
        /// <summary>
        /// Certificate key
        /// </summary>
        public uint Key_u32;

        /// <summary>
        /// Certificate
        /// </summary>
        // TODO: X *x;
    }

    /// <summary>
    /// Enumeration of secure devices
    /// </summary>
    public class VpnRpcClientEnumSecure
    {
        /// <summary>
        /// Number of items
        /// </summary>
        public uint NumItem_u32;

        /// <summary>
        /// Item
        /// </summary>
        // TODO: RPC_CLIENT_ENUM_SECURE_ITEM **Items;
    }

    /// <summary>
    /// Specify a secure device
    /// </summary>
    public class VpnRpcUseSecure
    {
        /// <summary>
        /// Device ID
        /// </summary>
        public uint DeviceId_u32;
    }

    /// <summary>
    /// Enumerate objects in the secure device
    /// </summary>
    public class VpnRpcEnumObjectInSecure
    {
        /// <summary>
        /// Window handle
        /// </summary>
        public uint hWnd_u32;

        /// <summary>
        /// Number of items
        /// </summary>
        public uint NumItem_u32;

        /// <summary>
        /// Item name
        /// </summary>
        public string ItemName_str;

        /// <summary>
        /// Type (true = secret key, false = public key)
        /// </summary>
        public bool ItemType_bool;
    }

    /// <summary>
    /// Create a virtual LAN
    /// </summary>
    public class VpnRpcClientCreateVlan
    {
        /// <summary>
        /// Device name
        /// </summary>
        public string DeviceName_str;
    }

    /// <summary>
    /// Get a Virtual LAN information
    /// </summary>
    public class VpnRpcClientGetVlan
    {
        /// <summary>
        /// Device name
        /// </summary>
        public string DeviceName_str;

        /// <summary>
        /// Flag of whether it works or not
        /// </summary>
        public bool Enabled_bool;

        /// <summary>
        /// MAC address
        /// </summary>
        public string MacAddress_str;

        /// <summary>
        /// Version
        /// </summary>
        public string Version_str;

        /// <summary>
        /// Driver file name
        /// </summary>
        public string FileName_str;

        /// <summary>
        /// GUID
        /// </summary>
        public string Guid_str;
    }

    /// <summary>
    /// Set the virtual LAN information
    /// </summary>
    public class VpnRpcClientSetVlan
    {
        /// <summary>
        /// Device name
        /// </summary>
        public string DeviceName_str;

        /// <summary>
        /// MAC address
        /// </summary>
        public string MacAddress_str;
    }

    /// <summary>
    /// Enumerate the virtual LANs
    /// </summary>
    public class VpnRpcClientEnumVlan
    {
        /// <summary>
        /// Item count
        /// </summary>
        public uint NumItem_u32;

        /// <summary>
        /// Item
        /// </summary>
        // TODO: RPC_CLIENT_ENUM_VLAN_ITEM **Items;
    }

    /// <summary>
    /// Create an account
    /// </summary>
    public class VpnRpcClientCreateAccount
    {
        /// <summary>
        /// Client Option
        /// </summary>
        // TODO: CLIENT_OPTION *ClientOption;

        /// <summary>
        /// Client authentication data
        /// </summary>
        // TODO: CLIENT_AUTH *ClientAuth;

        /// <summary>
        /// Startup account
        /// </summary>
        public bool StartupAccount_bool;

        /// <summary>
        /// Checking of the server certificate
        /// </summary>
        public bool CheckServerCert_bool;

        /// <summary>
        /// Server certificate
        /// </summary>
        // TODO: X *ServerCert;

        /// <summary>
        /// Shortcut Key
        /// </summary>
        public byte[] ShortcutKey_bin;
    }

    /// <summary>
    /// Enumeration of accounts
    /// </summary>
    public class VpnRpcClientEnumAccount
    {
        /// <summary>
        /// Item count
        /// </summary>
        public uint NumItem_u32;

        /// <summary>
        /// Items
        /// </summary>
        // TODO: RPC_CLIENT_ENUM_ACCOUNT_ITEM **Items;
    }

    /// <summary>
    /// Delete the Account
    /// </summary>
    public class VpnRpcClientDeleteAccount
    {
        /// <summary>
        /// Account name
        /// </summary>
        public string AccountName_utf;
    }

    /// <summary>
    /// Get the issuer
    /// </summary>
    public class VpnRpcGetIssuer
    {
        /// <summary>
        /// Certificate
        /// </summary>
        // TODO: X *x;

        /// <summary>
        /// Issuer
        /// </summary>
        // TODO: X *issuer_x;
    }

    /// <summary>
    /// VPN Gate Hostdata List
    /// </summary>
    public class VpnVghostlist
    {
        // TODO: LIST *HostList;

        public uint Version_u32;

        public string WebLink_str;

        public string DatClientIp_str;

        public string DatRequestIp_str;

        public string DatId_str;

        public string UdpIp_str;

        public uint UdpPort_u32;

        public byte[] UdpSrKey_bin;

        public string HttpProxyIp_str;

        public uint HttpProxyPort_u32;

        public byte[] HttpProxyKey_bin;

        public string VpnRelayIp_str;

        public uint VpnRelayPort_u32;

        public byte[] VpnRelayKey_bin;

        public string VpnRelayIpP2P_str;

        public uint VpnRelayPortP2P_u32;

        public byte[] VpnRelayKeyP2P_bin;

        public bool DisableRelayFunction_bool;
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
    /// Get the account
    /// </summary>
    public class VpnRpcClientGetAccount
    {
        /// <summary>
        /// Account name
        /// </summary>
        public string AccountName_utf;

        /// <summary>
        /// Client Option
        /// </summary>
        // TODO: CLIENT_OPTION *ClientOption;

        /// <summary>
        /// Client authentication data
        /// </summary>
        // TODO: CLIENT_AUTH *ClientAuth;

        /// <summary>
        /// Startup account
        /// </summary>
        public bool StartupAccount_bool;

        /// <summary>
        /// Check the server certificate
        /// </summary>
        public bool CheckServerCert_bool;

        /// <summary>
        /// Server certificate
        /// </summary>
        // TODO: X *ServerCert;

        /// <summary>
        /// Shortcut Key
        /// </summary>
        public byte[] ShortcutKey_bin;

        /// <summary>
        /// Creation date and time (Ver 3.0 or later)
        /// </summary>
        public ulong CreateDateTime_u64;

        /// <summary>
        /// Modified date (Ver 3.0 or later)
        /// </summary>
        public ulong UpdateDateTime_u64;

        /// <summary>
        /// Last connection date and time (Ver 3.0 or later)
        /// </summary>
        public ulong LastConnectDateTime_u64;
    }

    /// <summary>
    /// Change the account name
    /// </summary>
    public class VpnRpcRenameAccount
    {
        /// <summary>
        /// Old name
        /// </summary>
        public string OldName_utf;

        /// <summary>
        /// New Name
        /// </summary>
        public string NewName_utf;
    }

    /// <summary>
    /// Client Settings
    /// </summary>
    public class VpnClientConfig
    {
        /// <summary>
        /// Allow the remote configuration
        /// </summary>
        public bool AllowRemoteConfig_bool;

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

        /// <summary>
        /// Don't change the WCM network settings on Windows 8
        /// </summary>
        public bool NoChangeWcmNetworkSettingOnWindows8_bool;

        public bool DisableRpcDynamicPortListener_bool;

        public bool EnableTunnelCrackProtect_bool;
    }

    /// <summary>
    /// Connection
    /// </summary>
    public class VpnRpcClientConnect
    {
        /// <summary>
        /// Account name
        /// </summary>
        public string AccountName_utf;
    }

    /// <summary>
    /// Get the Connection status
    /// </summary>
    public class VpnRpcClientGetConnectionStatus
    {
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
        public uint SessionStatus_u32;

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
        // TODO: X *ServerX;

        /// <summary>
        /// Client certificate
        /// </summary>
        // TODO: X *ClientX;

        /// <summary>
        /// Connection start time
        /// </summary>
        public ulong StartTime_u64;

        /// <summary>
        /// Connection completion time of the first connection
        /// </summary>
        public ulong FirstConnectionEstablisiedTime_u64;

        /// <summary>
        /// Connection completion time of this connection
        /// </summary>
        public ulong CurrentConnectionEstablishTime_u64;

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
        /// Protocol name
        /// </summary>
        public string ProtocolName_str;

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
        /// Protocol Details
        /// </summary>
        public string ProtocolDetails_str;

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
        /// Policy
        /// </summary>
        // TODO: POLICY Policy;

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
        /// Traffic data
        /// </summary>
        // TODO: TRAFFIC Traffic;

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

}
