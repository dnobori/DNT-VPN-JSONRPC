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
    /// A data structure that stores the result of retrieving the VPN Client version information
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
    /// VPN Client Password Setting
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
    /// VPN Client Password Setting
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
        /// Certificate key (internal 32-bit ID)
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
    /// Certificate enumeration
    /// </summary>
    public class VpnRpcClientEnumCA
    {
        /// <summary>
        /// Number of items
        /// </summary>
        public uint NumItem_u32;

        /// <summary>
        /// Item list
        /// </summary>
        public VpnRpcClientEnumCAItem[] CAList;
    }

    /// <summary>
    /// A X.509 certificate item
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
        /// Certificate key (internal 32-bit ID)
        /// </summary>
        public uint Key_u32;
    }

    /// <summary>
    /// Get the certificate
    /// </summary>
    public class VpnRpcGetCA
    {
        /// <summary>
        /// Certificate key (internal 32-bit ID)
        /// </summary>
        public uint Key_u32;

        /// <summary>
        /// Certificate
        /// </summary>
        public byte[] X_bin;
    }

    /// <summary>
    /// security device enumeration item
    /// </summary>
    public class VpnRpcClientEnumSecureItem
    {
        /// <summary>
        /// Device ID (internal 32-bit ID)
        /// </summary>
        public uint DeviceId_u32;

        /// <summary>
        /// Device type
        /// </summary>
        public uint Type_u32;

        /// <summary>
        /// Device name
        /// </summary>
        public string DeviceName_str;

        /// <summary>
        /// Manufacturer
        /// </summary>
        public string Manufacturer_str;
    }

    /// <summary>
    /// Enumeration of security devices
    /// </summary>
    public class VpnRpcClientEnumSecure
    {
        /// <summary>
        /// Number of items
        /// </summary>
        public uint NumItem_u32;

        /// <summary>
        /// Items list
        /// </summary>
        public VpnRpcClientEnumSecureItem[] SecureDeviceList;
    }

    /// <summary>
    /// Specify a security device
    /// </summary>
    public class VpnRpcUseSecure
    {
        /// <summary>
        /// Device ID
        /// </summary>
        public uint DeviceId_u32;
    }

    /// <summary>
    /// An security device item
    /// </summary>
    public class VpnRpcEnumObjectInSecureItem
    {
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
    /// Enumerate objects in the security device
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

        public VpnRpcEnumObjectInSecureItem[] ObjectList;
    }

    /// <summary>
    /// Specify an Virtual Network Adapter
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
    /// Virtual LAN enumeration item
    /// </summary>
    public class VpnRpcClientEnumVlanItem
    {
        /// <summary>
        /// Device name
        /// </summary>
        public string DeviceName_str;

        /// <summary>
        /// Operation flag
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
        public VpnRpcClientEnumVlanItem[] VLanList;
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
    }

    /// <summary>
    /// Create an account
    /// </summary>
    public class VpnRpcClientCreateAccount
    {
        // ---- Start of Client Option Parameters ---
        /// <summary>
        /// Client Option Parameters: Specify the name of the Cascade Connection
        /// </summary>
        [JsonProperty("AccountName_utf")]
        public string ClientOption_AccountName_utf;

        /// <summary>
        /// Client Option Parameters: Specify the hostname of the destination VPN Server. You can also specify by IP address.
        /// </summary>
        [JsonProperty("Hostname_str")]
        public string ClientOption_Hostname_str;

        /// <summary>
        /// Client Option Parameters: Specify the port number of the destination VPN Server.
        /// </summary>
        [JsonProperty("Port_u32")]
        public uint ClientOption_Port_u32;

        /// <summary>
        /// Client Option Parameters: The type of the proxy server
        /// </summary>
        [JsonProperty("ProxyType_u32")]
        public VpnRpcProxyType ClientOption_ProxyType_u32;

        /// <summary>
        /// Client Option Parameters: The hostname or IP address of the proxy server name
        /// </summary>
        [JsonProperty("ProxyName_str")]
        public string ClientOption_ProxyName_str;

        /// <summary>
        /// Client Option Parameters: The port number of the proxy server
        /// </summary>
        [JsonProperty("ProxyPort_u32")]
        public uint ClientOption_ProxyPort_u32;

        /// <summary>
        /// Client Option Parameters: The username to connect to the proxy server
        /// </summary>
        [JsonProperty("ProxyUsername_str")]
        public string ClientOption_ProxyUsername_str;

        /// <summary>
        /// Client Option Parameters: The password to connect to the proxy server
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
        /// Client Option Parameters: The Virtual Hub on the destination VPN Server
        /// </summary>
        [JsonProperty("HubName_str")]
        public string ClientOption_HubName_str;

        /// <summary>
        /// Client Option Parameters: Number of TCP Connections to Use in VPN Communication
        /// </summary>
        [JsonProperty("MaxConnection_u32")]
        public uint ClientOption_MaxConnection_u32;

        /// <summary>
        /// Client Option Parameters: The flag to enable the encryption on the communication
        /// </summary>
        [JsonProperty("UseEncrypt_bool")]
        public bool ClientOption_UseEncrypt_bool;

        /// <summary>
        /// Client Option Parameters: Enable / Disable Data Compression when Communicating by Cascade Connection
        /// </summary>
        [JsonProperty("UseCompress_bool")]
        public bool ClientOption_UseCompress_bool;

        /// <summary>
        /// Client Option Parameters: Specify true when enabling half duplex mode. When using two or more TCP connections for VPN communication, it is possible to use Half Duplex Mode. By enabling half duplex mode it is possible to automatically fix data transmission direction as half and half for each TCP connection. In the case where a VPN using 8 TCP connections is established, for example, when half-duplex is enabled, communication can be fixes so that 4 TCP connections are dedicated to the upload direction and the other 4 connections are dedicated to the download direction.
        /// </summary>
        [JsonProperty("HalfConnection_bool")]
        public bool ClientOption_HalfConnection_bool;

        /// <summary>
        /// Client Option Parameters: Disable the routing tracking
        /// </summary>
        [JsonProperty("NoRoutingTracking_bool")]
        public bool ClientOption_NoRoutingTracking_bool;

        /// <summary>
        /// Client Option Parameters: VLAN device name
        /// </summary>
        [JsonProperty("DeviceName_str")]
        public string ClientOption_DeviceName_str;

        /// <summary>
        /// Client Option Parameters: Connection attempt interval when additional connection will be established
        /// </summary>
        [JsonProperty("AdditionalConnectionInterval_u32")]
        public uint ClientOption_AdditionalConnectionInterval_u32;

        /// <summary>
        /// Client Option Parameters: Connection Life of Each TCP Connection (0 for no keep-alive)
        /// </summary>
        [JsonProperty("ConnectionDisconnectSpan_u32")]
        public uint ClientOption_ConnectionDisconnectSpan_u32;

        /// <summary>
        /// Client Option Parameters: Hide the status window
        /// </summary>
        [JsonProperty("HideStatusWindow_bool")]
        public bool ClientOption_HideStatusWindow_bool;

        /// <summary>
        /// Client Option Parameters: Hide the NIC status window
        /// </summary>
        [JsonProperty("HideNicInfoWindow_bool")]
        public bool ClientOption_HideNicInfoWindow_bool;

        /// <summary>
        /// Client Option Parameters: Monitor port mode
        /// </summary>
        [JsonProperty("RequireMonitorMode_bool")]
        public bool ClientOption_RequireMonitorMode_bool;

        /// <summary>
        /// Client Option Parameters: Bridge or routing mode
        /// </summary>
        [JsonProperty("RequireBridgeRoutingMode_bool")]
        public bool ClientOption_RequireBridgeRoutingMode_bool;

        /// <summary>
        /// Client Option Parameters: Disable QoS Control Function if the value is true
        /// </summary>
        [JsonProperty("DisableQoS_bool")]
        public bool ClientOption_DisableQoS_bool;

        /// <summary>
        /// Client Option Parameters: Do not use TLS 1.x of the value is true
        /// </summary>
        [JsonProperty("NoTls1_bool")]
        public bool ClientOption_NoTls1_bool;

        /// <summary>
        /// Client Option Parameters: Do not use UDP acceleration mode if the value is true
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
        /// SHA-0 Hashed password. Valid only if ClientAuth_AuthType_u32 == SHA0_Hashed_Password (1). The SHA-0 hashed password must be caluclated by the SHA0(password_ascii_string + UpperCase(username_ascii_string)).
        /// </summary>
        [JsonProperty("HashedPassword_bin")]
        public byte[] ClientAuth_HashedPassword_bin;

        /// <summary>
        /// Plaintext Password. Valid only if ClientAuth_AuthType_u32 == PlainPassword (2).
        /// </summary>
        [JsonProperty("PlainPassword_str")]
        public string ClientAuth_PlainPassword_str;

        /// <summary>
        /// Client certificate. Valid only if ClientAuth_AuthType_u32 == Cert (3).
        /// </summary>
        [JsonProperty("ClientX_bin")]
        public byte[] ClientAuth_ClientX_bin;

        /// <summary>
        /// Client private key of the certificate. Valid only if ClientAuth_AuthType_u32 == Cert (3).
        /// </summary>
        [JsonProperty("ClientK_bin")]
        public byte[] ClientAuth_ClientK_bin;
        // ---- End of Client Auth Parameters ---

        /// <summary>
        /// Startup account
        /// </summary>
        public bool StartupAccount_bool;

        /// <summary>
        /// Checking of the server certificate
        /// </summary>
        public bool CheckServerCert_bool;

        /// <summary>
        /// The body of server X.509 certificate to compare. Valid only if the CheckServerCert_bool flag is true.
        /// </summary>
        public byte[] ServerCert_bin;

        /// <summary>
        /// Shortcut Key
        /// </summary>
        public byte[] ShortcutKey_bin;
    }

    /// <summary>
    /// Proxy type
    /// </summary>
    public enum VpnRpcProxyType
    {
        /// <summary>
        /// Direct TCP connection
        /// </summary>
        Direct = 0,

        /// <summary>
        /// Connection via HTTP proxy server
        /// </summary>
        HTTP = 1,

        /// <summary>
        /// Connection via SOCKS proxy server
        /// </summary>
        SOCKS = 2,
    }

    /// <summary>
    /// Enumeration item of account
    /// </summary>
    public class VpnRpcClientEnumAccountItem
    {
        /// <summary>
        /// Account name
        /// </summary>
        public string AccountName_utf;

        /// <summary>
        /// User name
        /// </summary>
        public string UserName_str;

        /// <summary>
        /// Server name
        /// </summary>
        public string ServerName_str;

        /// <summary>
        /// Device name
        /// </summary>
        public string DeviceName_str;

        /// <summary>
        /// Type of proxy connection
        /// </summary>
        public VpnRpcProxyType ProxyType_u32;

        /// <summary>
        /// Host name
        /// </summary>
        public string ProxyName_str;

        /// <summary>
        /// Operation flag
        /// </summary>
        public bool Active_bool;

        /// <summary>
        /// Connection completion flag
        /// </summary>
        public bool Connected_bool;

        /// <summary>
        /// Startup account
        /// </summary>
        public bool StartupAccount_bool;

        /// <summary>
        /// Port number (Ver 3.0 or later)
        /// </summary>
        public uint Port_u32;

        /// <summary>
        /// Virtual HUB name (Ver 3.0 or later)
        /// </summary>
        public string HubName_str;

        /// <summary>
        /// Creation date and time (Ver 3.0 or later)
        /// </summary>
        public DateTime CreateDateTime_dt;

        /// <summary>
        /// Modified date (Ver 3.0 or later)
        /// </summary>
        public DateTime UpdateDateTime_dt;

        /// <summary>
        /// Last connection date and time (Ver 3.0 or later)
        /// </summary>
        public DateTime LastConnectDateTime_dt;
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
        public VpnRpcClientEnumAccountItem[] AccountList;
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
    /// Get the issuer of a specified certificate
    /// </summary>
    public class VpnRpcGetIssuer
    {
        /// <summary>
        /// Certificate
        /// </summary>
        public byte[] x_bin;

        /// <summary>
        /// Issuer
        /// </summary>
        public byte[] issuer_x_bin;
    }

    /// <summary>
    /// Test RPC parameter
    /// </summary>
    public class VpnRpcTest
    {
        public uint IntValue_u32;

        public ulong Int64Value_u64;

        public string StrValue_str;

        public string UniStrValue_utf;
    }

    /// <summary>
    /// Get an account object
    /// </summary>
    public class VpnRpcClientGetAccount
    {
        // ---- Start of Client Option Parameters ---
        /// <summary>
        /// Client Option Parameters: Specify the name of the Cascade Connection
        /// </summary>
        [JsonProperty("AccountName_utf")]
        public string ClientOption_AccountName_utf;

        /// <summary>
        /// Client Option Parameters: Specify the hostname of the destination VPN Server. You can also specify by IP address.
        /// </summary>
        [JsonProperty("Hostname_str")]
        public string ClientOption_Hostname_str;

        /// <summary>
        /// Client Option Parameters: Specify the port number of the destination VPN Server.
        /// </summary>
        [JsonProperty("Port_u32")]
        public uint ClientOption_Port_u32;

        /// <summary>
        /// Client Option Parameters: The type of the proxy server
        /// </summary>
        [JsonProperty("ProxyType_u32")]
        public VpnRpcProxyType ClientOption_ProxyType_u32;

        /// <summary>
        /// Client Option Parameters: The hostname or IP address of the proxy server name
        /// </summary>
        [JsonProperty("ProxyName_str")]
        public string ClientOption_ProxyName_str;

        /// <summary>
        /// Client Option Parameters: The port number of the proxy server
        /// </summary>
        [JsonProperty("ProxyPort_u32")]
        public uint ClientOption_ProxyPort_u32;

        /// <summary>
        /// Client Option Parameters: The username to connect to the proxy server
        /// </summary>
        [JsonProperty("ProxyUsername_str")]
        public string ClientOption_ProxyUsername_str;

        /// <summary>
        /// Client Option Parameters: The password to connect to the proxy server
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
        /// Client Option Parameters: The Virtual Hub on the destination VPN Server
        /// </summary>
        [JsonProperty("HubName_str")]
        public string ClientOption_HubName_str;

        /// <summary>
        /// Client Option Parameters: Number of TCP Connections to Use in VPN Communication
        /// </summary>
        [JsonProperty("MaxConnection_u32")]
        public uint ClientOption_MaxConnection_u32;

        /// <summary>
        /// Client Option Parameters: The flag to enable the encryption on the communication
        /// </summary>
        [JsonProperty("UseEncrypt_bool")]
        public bool ClientOption_UseEncrypt_bool;

        /// <summary>
        /// Client Option Parameters: Enable / Disable Data Compression when Communicating by Cascade Connection
        /// </summary>
        [JsonProperty("UseCompress_bool")]
        public bool ClientOption_UseCompress_bool;

        /// <summary>
        /// Client Option Parameters: Specify true when enabling half duplex mode. When using two or more TCP connections for VPN communication, it is possible to use Half Duplex Mode. By enabling half duplex mode it is possible to automatically fix data transmission direction as half and half for each TCP connection. In the case where a VPN using 8 TCP connections is established, for example, when half-duplex is enabled, communication can be fixes so that 4 TCP connections are dedicated to the upload direction and the other 4 connections are dedicated to the download direction.
        /// </summary>
        [JsonProperty("HalfConnection_bool")]
        public bool ClientOption_HalfConnection_bool;

        /// <summary>
        /// Client Option Parameters: Disable the routing tracking
        /// </summary>
        [JsonProperty("NoRoutingTracking_bool")]
        public bool ClientOption_NoRoutingTracking_bool;

        /// <summary>
        /// Client Option Parameters: VLAN device name
        /// </summary>
        [JsonProperty("DeviceName_str")]
        public string ClientOption_DeviceName_str;

        /// <summary>
        /// Client Option Parameters: Connection attempt interval when additional connection will be established
        /// </summary>
        [JsonProperty("AdditionalConnectionInterval_u32")]
        public uint ClientOption_AdditionalConnectionInterval_u32;

        /// <summary>
        /// Client Option Parameters: Connection Life of Each TCP Connection (0 for no keep-alive)
        /// </summary>
        [JsonProperty("ConnectionDisconnectSpan_u32")]
        public uint ClientOption_ConnectionDisconnectSpan_u32;

        /// <summary>
        /// Client Option Parameters: Hide the status window
        /// </summary>
        [JsonProperty("HideStatusWindow_bool")]
        public bool ClientOption_HideStatusWindow_bool;

        /// <summary>
        /// Client Option Parameters: Hide the NIC status window
        /// </summary>
        [JsonProperty("HideNicInfoWindow_bool")]
        public bool ClientOption_HideNicInfoWindow_bool;

        /// <summary>
        /// Client Option Parameters: Monitor port mode
        /// </summary>
        [JsonProperty("RequireMonitorMode_bool")]
        public bool ClientOption_RequireMonitorMode_bool;

        /// <summary>
        /// Client Option Parameters: Bridge or routing mode
        /// </summary>
        [JsonProperty("RequireBridgeRoutingMode_bool")]
        public bool ClientOption_RequireBridgeRoutingMode_bool;

        /// <summary>
        /// Client Option Parameters: Disable QoS Control Function if the value is true
        /// </summary>
        [JsonProperty("DisableQoS_bool")]
        public bool ClientOption_DisableQoS_bool;

        /// <summary>
        /// Client Option Parameters: Do not use TLS 1.x of the value is true
        /// </summary>
        [JsonProperty("NoTls1_bool")]
        public bool ClientOption_NoTls1_bool;

        /// <summary>
        /// Client Option Parameters: Do not use UDP acceleration mode if the value is true
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
        /// SHA-0 Hashed password. Valid only if ClientAuth_AuthType_u32 == SHA0_Hashed_Password (1). The SHA-0 hashed password must be caluclated by the SHA0(password_ascii_string + UpperCase(username_ascii_string)).
        /// </summary>
        [JsonProperty("HashedPassword_bin")]
        public byte[] ClientAuth_HashedPassword_bin;

        /// <summary>
        /// Plaintext Password. Valid only if ClientAuth_AuthType_u32 == PlainPassword (2).
        /// </summary>
        [JsonProperty("PlainPassword_str")]
        public string ClientAuth_PlainPassword_str;

        /// <summary>
        /// Client certificate. Valid only if ClientAuth_AuthType_u32 == Cert (3).
        /// </summary>
        [JsonProperty("ClientX_bin")]
        public byte[] ClientAuth_ClientX_bin;

        /// <summary>
        /// Client private key of the certificate. Valid only if ClientAuth_AuthType_u32 == Cert (3).
        /// </summary>
        [JsonProperty("ClientK_bin")]
        public byte[] ClientAuth_ClientK_bin;
        // ---- End of Client Auth Parameters ---

        /// <summary>
        /// Startup account
        /// </summary>
        public bool StartupAccount_bool;

        /// <summary>
        /// Check the server certificate
        /// </summary>
        public bool CheckServerCert_bool;

        /// <summary>
        /// The body of server X.509 certificate to compare. Valid only if the CheckServerCert_bool flag is true.
        /// </summary>
        public byte[] ServerCert_bin;

        /// <summary>
        /// Shortcut Key
        /// </summary>
        public byte[] ShortcutKey_bin;

        /// <summary>
        /// Creation date and time (Ver 3.0 or later)
        /// </summary>
        public DateTime CreateDateTime_dt;

        /// <summary>
        /// Modified date (Ver 3.0 or later)
        /// </summary>
        public DateTime UpdateDateTime_dt;

        /// <summary>
        /// Last connection date and time (Ver 3.0 or later)
        /// </summary>
        public DateTime LastConnectDateTime_dt;
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
        public VpnRpcKeepAliveProtocol KeepConnectProtocol_u32;

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

        // ---- Start of Security policy ---
        /// <summary>
        /// Security policy: Allow Access. The users, which this policy value is true, have permission to make VPN connection to VPN Server.
        /// </summary>
        [JsonProperty("policy:Access_bool")]
        public bool SecPol_Access_bool;

        /// <summary>
        /// Security policy: Filter DHCP Packets (IPv4). All IPv4 DHCP packets in sessions defined this policy will be filtered.
        /// </summary>
        [JsonProperty("policy:DHCPFilter_bool")]
        public bool SecPol_DHCPFilter_bool;

        /// <summary>
        /// Security policy: Disallow DHCP Server Operation (IPv4). Computers connected to sessions that have this policy setting will not be allowed to become a DHCP server and distribute IPv4 addresses to DHCP clients.
        /// </summary>
        [JsonProperty("policy:DHCPNoServer_bool")]
        public bool SecPol_DHCPNoServer_bool;

        /// <summary>
        /// Security policy: Enforce DHCP Allocated IP Addresses (IPv4). Computers in sessions that have this policy setting will only be able to use IPv4 addresses allocated by a DHCP server on the virtual network side.
        /// </summary>
        [JsonProperty("policy:DHCPForce_bool")]
        public bool SecPol_DHCPForce_bool;

        /// <summary>
        /// Security policy: Deny Bridge Operation. Bridge-mode connections are denied for user sessions that have this policy setting. Even in cases when the Ethernet Bridge is configured in the client side, communication will not be possible.
        /// </summary>
        [JsonProperty("policy:NoBridge_bool")]
        public bool SecPol_NoBridge_bool;

        /// <summary>
        /// Security policy: Deny Routing Operation (IPv4). IPv4 routing will be denied for sessions that have this policy setting. Even in the case where the IP router is operating on the user client side, communication will not be possible.
        /// </summary>
        [JsonProperty("policy:NoRouting_bool")]
        public bool SecPol_NoRouting_bool;

        /// <summary>
        /// Security policy: Deny MAC Addresses Duplication. The use of duplicating MAC addresses that are in use by computers of different sessions cannot be used by sessions with this policy setting.
        /// </summary>
        [JsonProperty("policy:CheckMac_bool")]
        public bool SecPol_CheckMac_bool;

        /// <summary>
        /// Security policy: Deny IP Address Duplication (IPv4). The use of duplicating IPv4 addresses that are in use by computers of different sessions cannot be used by sessions with this policy setting.
        /// </summary>
        [JsonProperty("policy:CheckIP_bool")]
        public bool SecPol_CheckIP_bool;

        /// <summary>
        /// Security policy: Deny Non-ARP / Non-DHCP / Non-ICMPv6 broadcasts. The sending or receiving of broadcast packets that are not ARP protocol, DHCP protocol, nor ICMPv6 on the virtual network will not be allowed for sessions with this policy setting.
        /// </summary>
        [JsonProperty("policy:ArpDhcpOnly_bool")]
        public bool SecPol_ArpDhcpOnly_bool;

        /// <summary>
        /// Security policy: Privacy Filter Mode. All direct communication between sessions with the privacy filter mode policy setting will be filtered.
        /// </summary>
        [JsonProperty("policy:PrivacyFilter_bool")]
        public bool SecPol_PrivacyFilter_bool;

        /// <summary>
        /// Security policy: Deny Operation as TCP/IP Server (IPv4). Computers of sessions with this policy setting can't listen and accept TCP/IP connections in IPv4.
        /// </summary>
        [JsonProperty("policy:NoServer_bool")]
        public bool SecPol_NoServer_bool;

        /// <summary>
        /// Security policy: Unlimited Number of Broadcasts. If a computer of a session with this policy setting sends broadcast packets of a number unusually larger than what would be considered normal on the virtual network, there will be no automatic limiting.
        /// </summary>
        [JsonProperty("policy:NoBroadcastLimiter_bool")]
        public bool SecPol_NoBroadcastLimiter_bool;

        /// <summary>
        /// Security policy: Allow Monitoring Mode. Users with this policy setting will be granted to connect to the Virtual Hub in Monitoring Mode. Sessions in Monitoring Mode are able to monitor (tap) all packets flowing through the Virtual Hub.
        /// </summary>
        [JsonProperty("policy:MonitorPort_bool")]
        public bool SecPol_MonitorPort_bool;

        /// <summary>
        /// Security policy: Maximum Number of TCP Connections. For sessions with this policy setting, this sets the maximum number of physical TCP connections consists in a physical VPN session.
        /// </summary>
        [JsonProperty("policy:MaxConnection_u32")]
        public uint SecPol_MaxConnection_u32;

        /// <summary>
        /// Security policy: Time-out Period. For sessions with this policy setting, this sets, in seconds, the time-out period to wait before disconnecting a session when communication trouble occurs between the VPN Client / VPN Server.
        /// </summary>
        [JsonProperty("policy:TimeOut_u32")]
        public uint SecPol_TimeOut_u32;

        /// <summary>
        /// Security policy: Maximum Number of MAC Addresses. For sessions with this policy setting, this limits the number of MAC addresses per session.
        /// </summary>
        [JsonProperty("policy:MaxMac_u32")]
        public uint SecPol_MaxMac_u32;

        /// <summary>
        /// Security policy: Maximum Number of IP Addresses (IPv4). For sessions with this policy setting, this specifies the number of IPv4 addresses that can be registered for a single session.
        /// </summary>
        [JsonProperty("policy:MaxIP_u32")]
        public uint SecPol_MaxIP_u32;

        /// <summary>
        /// Security policy: Upload Bandwidth. For sessions with this policy setting, this limits the traffic bandwidth that is in the inwards direction from outside to inside the Virtual Hub.
        /// </summary>
        [JsonProperty("policy:MaxUpload_u32")]
        public uint SecPol_MaxUpload_u32;

        /// <summary>
        /// Security policy: Download Bandwidth. For sessions with this policy setting, this limits the traffic bandwidth that is in the outwards direction from inside the Virtual Hub to outside the Virtual Hub.
        /// </summary>
        [JsonProperty("policy:MaxDownload_u32")]
        public uint SecPol_MaxDownload_u32;

        /// <summary>
        /// Security policy: Deny Changing Password. The users which use password authentication with this policy setting are not allowed to change their own password from the VPN Client Manager or similar.
        /// </summary>
        [JsonProperty("policy:FixPassword_bool")]
        public bool SecPol_FixPassword_bool;

        /// <summary>
        /// Security policy: Maximum Number of Multiple Logins. Users with this policy setting are unable to have more than this number of concurrent logins. Bridge Mode sessions are not subjects to this policy.
        /// </summary>
        [JsonProperty("policy:MultiLogins_u32")]
        public uint SecPol_MultiLogins_u32;

        /// <summary>
        /// Security policy: Deny VoIP / QoS Function. Users with this security policy are unable to use VoIP / QoS functions in VPN connection sessions.
        /// </summary>
        [JsonProperty("policy:NoQoS_bool")]
        public bool SecPol_NoQoS_bool;

        /// <summary>
        /// Security policy: Filter RS / RA Packets (IPv6). All ICMPv6 packets which the message-type is 133 (Router Solicitation) or 134 (Router Advertisement) in sessions defined this policy will be filtered. As a result, an IPv6 client will be unable to use IPv6 address prefix auto detection and IPv6 default gateway auto detection.
        /// </summary>
        [JsonProperty("policy:RSandRAFilter_bool")]
        public bool SecPol_RSandRAFilter_bool;

        /// <summary>
        /// Security policy: Filter RA Packets (IPv6). All ICMPv6 packets which the message-type is 134 (Router Advertisement) in sessions defined this policy will be filtered. As a result, a malicious users will be unable to spread illegal IPv6 prefix or default gateway advertisements on the network.
        /// </summary>
        [JsonProperty("policy:RAFilter_bool")]
        public bool SecPol_RAFilter_bool;

        /// <summary>
        /// Security policy: Filter DHCP Packets (IPv6). All IPv6 DHCP packets in sessions defined this policy will be filtered.
        /// </summary>
        [JsonProperty("policy:DHCPv6Filter_bool")]
        public bool SecPol_DHCPv6Filter_bool;

        /// <summary>
        /// Security policy: Disallow DHCP Server Operation (IPv6). Computers connected to sessions that have this policy setting will not be allowed to become a DHCP server and distribute IPv6 addresses to DHCP clients.
        /// </summary>
        [JsonProperty("policy:DHCPv6NoServer_bool")]
        public bool SecPol_DHCPv6NoServer_bool;

        /// <summary>
        /// Security policy: Deny Routing Operation (IPv6). IPv6 routing will be denied for sessions that have this policy setting. Even in the case where the IP router is operating on the user client side, communication will not be possible.
        /// </summary>
        [JsonProperty("policy:NoRoutingV6_bool")]
        public bool SecPol_NoRoutingV6_bool;

        /// <summary>
        /// Security policy: Deny IP Address Duplication (IPv6). The use of duplicating IPv6 addresses that are in use by computers of different sessions cannot be used by sessions with this policy setting.
        /// </summary>
        [JsonProperty("policy:CheckIPv6_bool")]
        public bool SecPol_CheckIPv6_bool;

        /// <summary>
        /// Security policy: Deny Operation as TCP/IP Server (IPv6). Computers of sessions with this policy setting can't listen and accept TCP/IP connections in IPv6.
        /// </summary>
        [JsonProperty("policy:NoServerV6_bool")]
        public bool SecPol_NoServerV6_bool;

        /// <summary>
        /// Security policy: Maximum Number of IP Addresses (IPv6). For sessions with this policy setting, this specifies the number of IPv6 addresses that can be registered for a single session.
        /// </summary>
        [JsonProperty("policy:MaxIPv6_u32")]
        public uint SecPol_MaxIPv6_u32;

        /// <summary>
        /// Security policy: Disallow Password Save in VPN Client. For users with this policy setting, when the user is using *standard* password authentication, the user will be unable to save the password in VPN Client. The user will be required to input passwords for every time to connect a VPN. This will improve the security. If this policy is enabled, VPN Client Version 2.0 will be denied to access.
        /// </summary>
        [JsonProperty("policy:NoSavePassword_bool")]
        public bool SecPol_NoSavePassword_bool;

        /// <summary>
        /// Security policy: VPN Client Automatic Disconnect. For users with this policy setting, a user's VPN session will be disconnected automatically after the specific period will elapse. In this case no automatic re-connection will be performed. This can prevent a lot of inactive VPN Sessions. If this policy is enabled, VPN Client Version 2.0 will be denied to access.
        /// </summary>
        [JsonProperty("policy:AutoDisconnect_u32")]
        public uint SecPol_AutoDisconnect_u32;

        /// <summary>
        /// Security policy: Filter All IPv4 Packets. All IPv4 and ARP packets in sessions defined this policy will be filtered.
        /// </summary>
        [JsonProperty("policy:FilterIPv4_bool")]
        public bool SecPol_FilterIPv4_bool;

        /// <summary>
        /// Security policy: Filter All IPv6 Packets. All IPv6 packets in sessions defined this policy will be filtered.
        /// </summary>
        [JsonProperty("policy:FilterIPv6_bool")]
        public bool SecPol_FilterIPv6_bool;

        /// <summary>
        /// Security policy: Filter All Non-IP Packets. All non-IP packets in sessions defined this policy will be filtered. "Non-IP packet" mean a packet which is not IPv4, ARP nor IPv6. Any tagged-VLAN packets via the Virtual Hub will be regarded as non-IP packets.
        /// </summary>
        [JsonProperty("policy:FilterNonIP_bool")]
        public bool SecPol_FilterNonIP_bool;

        /// <summary>
        /// Security policy: No Default-Router on IPv6 RA. In all VPN Sessions defines this policy, any IPv6 RA (Router Advertisement) packet with non-zero value in the router-lifetime will set to zero-value. This is effective to avoid the horrible behavior from the IPv6 routing confusion which is caused by the VPN client's attempts to use the remote-side IPv6 router as its local IPv6 router.
        /// </summary>
        [JsonProperty("policy:NoIPv6DefaultRouterInRA_bool")]
        public bool SecPol_NoIPv6DefaultRouterInRA_bool;

        /// <summary>
        /// Security policy: No Default-Router on IPv6 RA (physical IPv6). In all VPN Sessions defines this policy (only when the physical communication protocol between VPN Client / VPN Bridge and VPN Server is IPv6), any IPv6 RA (Router Advertisement) packet with non-zero value in the router-lifetime will set to zero-value. This is effective to avoid the horrible behavior from the IPv6 routing confusion which is caused by the VPN client's attempts to use the remote-side IPv6 router as its local IPv6 router.
        /// </summary>
        [JsonProperty("policy:NoIPv6DefaultRouterInRAWhenIPv6_bool")]
        public bool SecPol_NoIPv6DefaultRouterInRAWhenIPv6_bool;

        /// <summary>
        /// Security policy: VLAN ID (IEEE802.1Q). You can specify the VLAN ID on the security policy. All VPN Sessions defines this policy, all Ethernet packets toward the Virtual Hub from the user will be inserted a VLAN tag (IEEE 802.1Q) with the VLAN ID. The user can also receive only packets with a VLAN tag which has the same VLAN ID. (Receiving process removes the VLAN tag automatically.) Any Ethernet packets with any other VLAN IDs or non-VLAN packets will not be received. All VPN Sessions without this policy definition can send / receive any kinds of Ethernet packets regardless of VLAN tags, and VLAN tags are not inserted or removed automatically. Any tagged-VLAN packets via the Virtual Hub will be regarded as non-IP packets. Therefore, tagged-VLAN packets are not subjects for IPv4 / IPv6 security policies, access lists nor other IPv4 / IPv6 specific deep processing.
        /// </summary>
        [JsonProperty("policy:VLanId_u32")]
        public uint SecPol_VLanId_u32;

        /// <summary>
        /// Security policy: Whether version 3.0 (must be true)
        /// </summary>
        [JsonProperty("policy:Ver3_bool")]
        public bool SecPol_Ver3_bool = true;
        // ---- End of Security policy ---


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
