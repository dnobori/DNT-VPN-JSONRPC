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

namespace SoftEther.VPNServerRpc.Types
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
}
