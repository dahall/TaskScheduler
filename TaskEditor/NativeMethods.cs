using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler
{
    internal static class NativeMethods
    {
        public enum ObjectPickerTypes
        {
            Builtin = 8,
            Computer = 1,
            Group = 4,
            None = 0,
            User = 2
        }

        [DllImport("Kernel32.dll")]
        public static extern IntPtr GlobalLock(IntPtr hMem);

        [DllImport("Kernel32.dll")]
        public static extern bool GlobalUnlock(IntPtr hMem);

        [StructLayout(LayoutKind.Sequential)]
        public struct PickerObject
        {
            private string name;
            private ObjectPickerTypes objectType;
            private string adsPath;
            private string @class;
            private string userPrincipalName;
            private object[] attributes;

            public PickerObject(string adsPath, string @class, string name, string userPrincipalName, object[] attributes, ObjectPickerTypes type)
            {
                this.adsPath = adsPath;
                this.@class = @class;
                this.name = name;
                this.userPrincipalName = userPrincipalName;
                this.attributes = attributes;
                this.objectType = type;
            }

            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            public string AdsPath
            {
                [DebuggerStepThrough]
                get
                {
                    return this.adsPath;
                }
            }

            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            public string Class
            {
                [DebuggerStepThrough]
                get
                {
                    return this.@class;
                }
            }

            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            public string Name
            {
                [DebuggerStepThrough]
                get
                {
                    return this.name;
                }
            }

            public string ObjectName
            {
                get
                {
                    return this.name;
                }
                set
                {
                    this.name = value;
                }
            }

            public ObjectPickerTypes ObjectType
            {
                get
                {
                    return this.objectType;
                }
                set
                {
                    this.objectType = value;
                }
            }

            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            public string UserPrincipalName
            {
                [DebuggerStepThrough]
                get
                {
                    return this.userPrincipalName;
                }
            }

            public object[] GetAttributes()
            {
                object[] array = new object[this.attributes.Length];
                this.attributes.CopyTo(array, 0);
                return array;
            }
        }

        public static class AccountUtils
        {
            private static string systemAccount, networkServiceAccount, localServiceAccount;

            public static bool CurrentUserIsAdmin(string computerName)
            {
                if (!string.IsNullOrEmpty(computerName) || computerName.Equals(".", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                WindowsPrincipal principal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
                return principal.IsInRole(0x220);
            }

            public static bool SelectAccount(IWin32Window parent, string targetComputerName, ref string acctName, ref bool isGroup, ref bool isService)
            {
                NativeMethods.ObjectPicker dlg = new NativeMethods.ObjectPicker();
                dlg.TargetComputer = targetComputerName;
                if (dlg.ShowDialog(parent) == DialogResult.OK && dlg.Picks.Length > 0)
                {
                    acctName = dlg.Picks[0].ObjectName;
                    isGroup = NativeMethods.ObjectPickerTypes.Group == dlg.Picks[0].ObjectType;
                    isService = NativeMethods.AccountUtils.UserIsServiceAccount(acctName);
                    return true;
                }
                return false;
            }

            public static bool UserIsServiceAccount(string userName)
            {
                if (((systemAccount == null) || (networkServiceAccount == null)) || (localServiceAccount == null))
                {
                    systemAccount = FormattedUserNameFromStringSid("S-1-5-18", null);
                    networkServiceAccount = FormattedUserNameFromStringSid("S-1-5-20", null);
                    localServiceAccount = FormattedUserNameFromStringSid("S-1-5-19", null);
                }

                int num = string.Compare(userName, systemAccount, StringComparison.CurrentCultureIgnoreCase);
                if (num != 0)
                {
                    num = string.Compare(userName, networkServiceAccount, StringComparison.CurrentCultureIgnoreCase);
                }
                if (num != 0)
                {
                    num = string.Compare(userName, localServiceAccount, StringComparison.CurrentCultureIgnoreCase);
                }
                return (0 == num);
            }

            [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal static extern bool ConvertStringSidToSid([In, MarshalAs(UnmanagedType.LPTStr)] string pStringSid, ref IntPtr sid);

            [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            internal static extern bool LookupAccountSid(string systemName, byte[] accountSid, StringBuilder accountName, ref int nameLength, StringBuilder domainName, ref int domainLength, out int accountType);

            internal static uint LookupAccountSid(SecurityIdentifier sid, out string accountName, out string domainName, out int use)
            {
                uint num = 0;
                byte[] binaryForm = new byte[sid.BinaryLength];
                sid.GetBinaryForm(binaryForm, 0);
                int capacity = 0x44;
                int num3 = 0x44;
                StringBuilder builder = new StringBuilder(capacity);
                StringBuilder builder2 = new StringBuilder(num3);
                if (!LookupAccountSid(null, binaryForm, builder, ref capacity, builder2, ref num3, out use))
                {
                    num = (uint)Marshal.GetLastWin32Error();
                }
                accountName = builder.ToString().TrimEnd(new char[] { '$' });
                domainName = builder2.ToString();
                return num;
            }

            private static bool FindUserFromSid(IntPtr incomingSid, string computerName, ref string userName)
            {
                int num3;
                bool flag = false;
                int cchName = 0;
                int cchReferencedDomainName = 0;
                StringBuilder referencedDomainName = new StringBuilder();
                int error = 0;
                StringBuilder name = new StringBuilder();
                if (!LookupAccountSid(computerName, incomingSid, name, ref cchName, referencedDomainName, ref cchReferencedDomainName, out num3))
                {
                    error = Marshal.GetLastWin32Error();
                    if (0x7a != error)
                    {
                        throw new Win32Exception(error);
                    }
                }
                if ((error == 0) || (0x7a == error))
                {
                    referencedDomainName = new StringBuilder(cchReferencedDomainName);
                    name = new StringBuilder(cchName);
                    if (!LookupAccountSid(computerName, incomingSid, name, ref cchName, referencedDomainName, ref cchReferencedDomainName, out num3))
                    {
                        throw new Win32Exception(Marshal.GetLastWin32Error());
                    }
                    flag = IsUserFromUse(num3);
                    if (userName == null)
                    {
                        return flag;
                    }
                    if (0 < cchReferencedDomainName)
                    {
                        userName = referencedDomainName.ToString();
                    }
                    else
                    {
                        userName = computerName;
                    }
                    userName = string.Format((IFormatProvider)CultureInfo.CurrentCulture.GetFormat(typeof(string)), "{0}\\{1}", new object[] { userName, name });
                }
                return flag;
            }

            private static string FormattedUserNameFromSid(IntPtr incomingSid, string computerName)
            {
                string userName = string.Empty;
                FindUserFromSid(incomingSid, computerName, ref userName);
                if (!string.IsNullOrEmpty(userName))
                {
                    SecurityIdentifier identifier = new SecurityIdentifier(incomingSid);
                    string[] strArray = userName.Split(new char[] { '\\' });
                    if (strArray.Length != 2)
                    {
                        return userName;
                    }
                    string str2 = strArray[1];
                    if ((identifier.IsWellKnown(WellKnownSidType.NetworkServiceSid) || identifier.IsWellKnown(WellKnownSidType.AnonymousSid)) || ((identifier.IsWellKnown(WellKnownSidType.LocalSystemSid) || identifier.IsWellKnown(WellKnownSidType.LocalServiceSid)) || identifier.IsWellKnown(WellKnownSidType.LocalSid)))
                    {
                        return str2;
                    }
                    if (string.Compare(strArray[0], computerName, StringComparison.CurrentCultureIgnoreCase) == 0)
                    {
                        userName = str2;
                    }
                }
                return userName;
            }

            private static string FormattedUserNameFromStringSid(string incomingSid, string computerName)
            {
                string str = string.Empty;
                IntPtr zero = IntPtr.Zero;
                if (!ConvertStringSidToSid(incomingSid, ref zero))
                {
                    throw new Win32Exception();
                }
                str = FormattedUserNameFromSid(zero, computerName);
                Marshal.FreeHGlobal(zero);
                return str;
            }

            private static bool IsUserFromUse(int use)
            {
                bool flag = false;
                if (use == 1)
                {
                    flag = true;
                }
                return flag;
            }

            [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            private static extern bool LookupAccountSid([In, MarshalAs(UnmanagedType.LPTStr)] string systemName, IntPtr sid, StringBuilder name, ref int cchName, StringBuilder referencedDomainName, ref int cchReferencedDomainName, out int use);
        }

        public class ObjectPicker : System.Windows.Forms.CommonDialog
        {
            // Fields
            public const string AttributeDnsHostName = "dnshostname";
            public const string AttributeObjectSid = "objectsid";
            public const string AttributeSamAccountName = "sAMAccountName";

            private string[] attributeNames;
            private DSOP_INIT_INFO_FLAGS initInfoFlags;
            private List<DSOP_SCOPE_INIT_INFO> scopeInfos;
            private string targetComputer;
            private ObjectPickerTypes types;

            public ObjectPicker()
            {
                TargetComputer = Environment.MachineName;
                ObjectPickerTypes user = ObjectPickerTypes.User;
                if (AccountUtils.CurrentUserIsAdmin(TargetComputer))
                    user |= ObjectPickerTypes.Builtin | ObjectPickerTypes.Group;
                ObjectPickerTypes = user;
            }

            public ObjectPicker(ObjectPickerTypes types, string targetComputer)
            {
                TargetComputer = targetComputer;
                ObjectPickerTypes = types;
            }

            [Flags]
            private enum DSOP_DOWNLEVEL_FILTER_FLAGS : uint
            {
                ALL_WELLKNOWN_SIDS = 0x80020000,
                ANONYMOUS = 0x80000040,
                AUTHENTICATED_USER = 0x80000020,
                BATCH = 0x80000080,
                COMPUTERS = 0x80000008,
                CREATOR_GROUP = 0x80000200,
                CREATOR_OWNER = 0x80000100,
                DIALUP = 0x80000400,
                EXCLUDE_BUILTIN_GROUPS = 0x80008000,
                GLOBAL_GROUPS = 0x80000004,
                INTERACTIVE = 0x80000800,
                LOCAL_GROUPS = 0x80000002,
                LOCAL_SERVICE = 0x80040000,
                NETWORK = 0x80001000,
                NETWORK_SERVICE = 0x80080000,
                REMOTE_LOGON = 0x80100000,
                SERVICE = 0x80002000,
                SYSTEM = 0x80004000,
                TERMINAL_SERVER = 0x80010000,
                USERS = 0x80000001,
                WORLD = 0x80000010
            }

            [Flags]
            private enum DSOP_INIT_INFO_FLAGS : uint
            {
                MULTISELECT = 1,
                SKIP_TARGET_COMPUTER_DC_CHECK = 2
            }

            [Flags]
            private enum DSOP_SCOPE_FLAGS : uint
            {
                DEFAULT_FILTER_COMPUTERS = 0x100,
                DEFAULT_FILTER_CONTACTS = 0x200,
                DEFAULT_FILTER_GROUPS = 0x80,
                DEFAULT_FILTER_USERS = 0x40,
                STARTING_SCOPE = 1,
                WANT_DOWNLEVEL_BUILTIN_PATH = 0x20,
                WANT_PROVIDER_GC = 8,
                WANT_PROVIDER_LDAP = 4,
                WANT_PROVIDER_WINNT = 2,
                WANT_SID_PATH = 0x10
            }

            [Flags]
            private enum DSOP_SCOPE_TYPE_FLAGS : uint
            {
                DOWNLEVEL_JOINED_DOMAIN = 4,
                ENTERPRISE_DOMAIN = 8,
                EXTERNAL_DOWNLEVEL_DOMAIN = 0x40,
                EXTERNAL_UPLEVEL_DOMAIN = 0x20,
                GLOBAL_CATALOG = 0x10,
                SECOND_SEARCH_SCOPE = 0x37f,
                TARGET_COMPUTER = 1,
                UPLEVEL_JOINED_DOMAIN = 2,
                USER_ENTERED_DOWNLEVEL_SCOPE = 0x200,
                USER_ENTERED_UPLEVEL_SCOPE = 0x100,
                WORKGROUP = 0x80
            }

            [Flags]
            private enum DSOP_UPLEVEL_FILTER_FLAGS : uint
            {
                BUILTIN_GROUPS = 4,
                COMPUTERS = 0x800,
                CONTACTS = 0x400,
                DOMAIN_LOCAL_GROUPS_DL = 0x100,
                DOMAIN_LOCAL_GROUPS_SE = 0x200,
                GLOBAL_GROUPS_DL = 0x40,
                GLOBAL_GROUPS_SE = 0x80,
                INCLUDE_ADVANCED_VIEW = 1,
                UNIVERSAL_GROUPS_DL = 0x10,
                UNIVERSAL_GROUPS_SE = 0x20,
                USERS = 2,
                WELL_KNOWN_PRINCIPALS = 8
            }

            // Nested Types
            private enum TypeOfMedium
            {
                Global = 1
            }

            [ComImport,
            Guid("0000010e-0000-0000-C000-000000000046"),
            InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
            private interface IDataObject
            {
                #region Methods

                void DAdvise(ref FORMATETC pFormatetc, uint advf, IntPtr pAdvSink, out uint pdwConnection);

                void DUnadvise(uint dwConnection);

                void EnumDAdvise(out IntPtr ppenumAdvise);

                void EnumFormatEtc(uint dwDirection, out IntPtr ppenumFormatetc);

                void GetCanonicalFormatEtc(ref FORMATETC pFormatectIn, out FORMATETC pFormatetcOut);

                void GetData(ref FORMATETC pFormatetc, out STGMEDIUM pmedium);

                void GetDataHere(ref FORMATETC pFormatetc, out STGMEDIUM pmedium);

                void QueryGetData(ref FORMATETC pFormatetc);

                void SetData(ref FORMATETC pFormatetc, ref STGMEDIUM pmedium, int fRelease);

                #endregion Methods
            }

            [ComImport,
            Guid("0C87E64E-3B7A-11D2-B9E0-00C04FD8DBF7"),
            InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
            private interface IDsObjectPicker
            {
                #region Methods

                void Initialize(ref DSOP_INIT_INFO pInitInfo);

                void InvokeDialog(IntPtr HWND, out IDataObject lpDataObject);

                #endregion Methods
            }

            public string[] AttributesToRetrieve
            {
                get
                {
                    string[] array = new string[this.attributeNames.Length];
                    this.attributeNames.CopyTo(array, 0);
                    return array;
                }
                set
                {
                    if (value == null)
                        throw new ArgumentNullException("names");

                    this.attributeNames = value;
                }
            }

            public bool FQDN
            {
                get; set;
            }

            // Properties
            [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi")]
            public bool MultiSelect
            {
                get
                {
                    return ((this.initInfoFlags & DSOP_INIT_INFO_FLAGS.MULTISELECT) != ((DSOP_INIT_INFO_FLAGS)0));
                }
                set
                {
                    if (value)
                    {
                        this.initInfoFlags |= DSOP_INIT_INFO_FLAGS.MULTISELECT;
                    }
                    else
                    {
                        this.initInfoFlags &= ~DSOP_INIT_INFO_FLAGS.MULTISELECT;
                    }
                }
            }

            public ObjectPickerTypes ObjectPickerTypes
            {
                get
                {
                    return types;
                }
                set
                {
                    ObjectPickerTypes[] typesArray = new ObjectPickerTypes[] { ObjectPickerTypes.Computer, ObjectPickerTypes.User, ObjectPickerTypes.Group, ObjectPickerTypes.Builtin };
                    if (value == ObjectPickerTypes.None)
                    {
                        throw new ArgumentException("type");
                    }
                    this.attributeNames = new string[0];
                    this.initInfoFlags = 0;
                    this.scopeInfos = new List<DSOP_SCOPE_INIT_INFO>();
                    this.types = value;
                    if (((value & typesArray[1]) != ObjectPickerTypes.None) && ((value & typesArray[2]) != ObjectPickerTypes.None))
                    {
                        DSOP_UPLEVEL_FILTER_FLAGS uplevelFilterFlags = DSOP_UPLEVEL_FILTER_FLAGS.USERS | DSOP_UPLEVEL_FILTER_FLAGS.BUILTIN_GROUPS;
                        DSOP_DOWNLEVEL_FILTER_FLAGS downlevelFlags = DSOP_DOWNLEVEL_FILTER_FLAGS.LOCAL_GROUPS | DSOP_DOWNLEVEL_FILTER_FLAGS.GLOBAL_GROUPS | DSOP_DOWNLEVEL_FILTER_FLAGS.USERS;
                        string[] names = new string[] { "sAMAccountName", "objectsid" };
                        this.AttributesToRetrieve = names;
                        if ((value & typesArray[3]) != ObjectPickerTypes.None)
                        {
                            uplevelFilterFlags |= DSOP_UPLEVEL_FILTER_FLAGS.WELL_KNOWN_PRINCIPALS;
                            downlevelFlags |= DSOP_DOWNLEVEL_FILTER_FLAGS.LOCAL_SERVICE | DSOP_DOWNLEVEL_FILTER_FLAGS.NETWORK_SERVICE | DSOP_DOWNLEVEL_FILTER_FLAGS.SYSTEM;
                        }
                        this.AddScopeInfo(DSOP_SCOPE_TYPE_FLAGS.WORKGROUP | DSOP_SCOPE_TYPE_FLAGS.GLOBAL_CATALOG | DSOP_SCOPE_TYPE_FLAGS.ENTERPRISE_DOMAIN, DSOP_SCOPE_FLAGS.DEFAULT_FILTER_USERS | DSOP_SCOPE_FLAGS.DEFAULT_FILTER_GROUPS, uplevelFilterFlags, downlevelFlags);
                    }
                    else if ((value & typesArray[0]) != ObjectPickerTypes.None)
                    {
                        if ((value & typesArray[2]) != ObjectPickerTypes.None)
                        {
                            this.AddScopeInfo(DSOP_SCOPE_TYPE_FLAGS.WORKGROUP | DSOP_SCOPE_TYPE_FLAGS.EXTERNAL_DOWNLEVEL_DOMAIN | DSOP_SCOPE_TYPE_FLAGS.EXTERNAL_UPLEVEL_DOMAIN | DSOP_SCOPE_TYPE_FLAGS.USER_ENTERED_DOWNLEVEL_SCOPE | DSOP_SCOPE_TYPE_FLAGS.USER_ENTERED_UPLEVEL_SCOPE | DSOP_SCOPE_TYPE_FLAGS.UPLEVEL_JOINED_DOMAIN | DSOP_SCOPE_TYPE_FLAGS.TARGET_COMPUTER | DSOP_SCOPE_TYPE_FLAGS.GLOBAL_CATALOG | DSOP_SCOPE_TYPE_FLAGS.ENTERPRISE_DOMAIN | DSOP_SCOPE_TYPE_FLAGS.DOWNLEVEL_JOINED_DOMAIN, DSOP_SCOPE_FLAGS.DEFAULT_FILTER_GROUPS | DSOP_SCOPE_FLAGS.DEFAULT_FILTER_COMPUTERS, DSOP_UPLEVEL_FILTER_FLAGS.DOMAIN_LOCAL_GROUPS_DL | DSOP_UPLEVEL_FILTER_FLAGS.GLOBAL_GROUPS_SE | DSOP_UPLEVEL_FILTER_FLAGS.GLOBAL_GROUPS_DL | DSOP_UPLEVEL_FILTER_FLAGS.COMPUTERS | DSOP_UPLEVEL_FILTER_FLAGS.DOMAIN_LOCAL_GROUPS_SE | DSOP_UPLEVEL_FILTER_FLAGS.UNIVERSAL_GROUPS_SE | DSOP_UPLEVEL_FILTER_FLAGS.UNIVERSAL_GROUPS_DL, DSOP_DOWNLEVEL_FILTER_FLAGS.LOCAL_GROUPS | DSOP_DOWNLEVEL_FILTER_FLAGS.GLOBAL_GROUPS | DSOP_DOWNLEVEL_FILTER_FLAGS.COMPUTERS);
                            this.AddScopeInfo(DSOP_SCOPE_TYPE_FLAGS.GLOBAL_CATALOG, DSOP_SCOPE_FLAGS.STARTING_SCOPE, DSOP_UPLEVEL_FILTER_FLAGS.DOMAIN_LOCAL_GROUPS_DL | DSOP_UPLEVEL_FILTER_FLAGS.GLOBAL_GROUPS_SE | DSOP_UPLEVEL_FILTER_FLAGS.GLOBAL_GROUPS_DL | DSOP_UPLEVEL_FILTER_FLAGS.COMPUTERS | DSOP_UPLEVEL_FILTER_FLAGS.DOMAIN_LOCAL_GROUPS_SE | DSOP_UPLEVEL_FILTER_FLAGS.UNIVERSAL_GROUPS_SE | DSOP_UPLEVEL_FILTER_FLAGS.UNIVERSAL_GROUPS_DL, DSOP_DOWNLEVEL_FILTER_FLAGS.LOCAL_GROUPS | DSOP_DOWNLEVEL_FILTER_FLAGS.GLOBAL_GROUPS | DSOP_DOWNLEVEL_FILTER_FLAGS.COMPUTERS);
                        }
                        else
                        {
                            this.AddScopeInfo(DSOP_SCOPE_TYPE_FLAGS.WORKGROUP | DSOP_SCOPE_TYPE_FLAGS.EXTERNAL_DOWNLEVEL_DOMAIN | DSOP_SCOPE_TYPE_FLAGS.EXTERNAL_UPLEVEL_DOMAIN | DSOP_SCOPE_TYPE_FLAGS.USER_ENTERED_DOWNLEVEL_SCOPE | DSOP_SCOPE_TYPE_FLAGS.USER_ENTERED_UPLEVEL_SCOPE | DSOP_SCOPE_TYPE_FLAGS.UPLEVEL_JOINED_DOMAIN | DSOP_SCOPE_TYPE_FLAGS.TARGET_COMPUTER | DSOP_SCOPE_TYPE_FLAGS.GLOBAL_CATALOG | DSOP_SCOPE_TYPE_FLAGS.ENTERPRISE_DOMAIN | DSOP_SCOPE_TYPE_FLAGS.DOWNLEVEL_JOINED_DOMAIN, DSOP_SCOPE_FLAGS.DEFAULT_FILTER_COMPUTERS, DSOP_UPLEVEL_FILTER_FLAGS.COMPUTERS, DSOP_DOWNLEVEL_FILTER_FLAGS.COMPUTERS);
                        }
                    }
                    else if ((value & typesArray[1]) != ObjectPickerTypes.None)
                    {
                        this.AddScopeInfo(DSOP_SCOPE_TYPE_FLAGS.WORKGROUP | DSOP_SCOPE_TYPE_FLAGS.GLOBAL_CATALOG | DSOP_SCOPE_TYPE_FLAGS.ENTERPRISE_DOMAIN, DSOP_SCOPE_FLAGS.DEFAULT_FILTER_USERS, DSOP_UPLEVEL_FILTER_FLAGS.USERS, DSOP_DOWNLEVEL_FILTER_FLAGS.USERS);
                        string[] strArray2 = new string[] { "sAMAccountName", "objectsid" };
                        this.AttributesToRetrieve = strArray2;
                    }
                    DSOP_SCOPE_TYPE_FLAGS scopeTypeFlags = DSOP_SCOPE_TYPE_FLAGS.EXTERNAL_DOWNLEVEL_DOMAIN | DSOP_SCOPE_TYPE_FLAGS.EXTERNAL_UPLEVEL_DOMAIN | DSOP_SCOPE_TYPE_FLAGS.USER_ENTERED_DOWNLEVEL_SCOPE | DSOP_SCOPE_TYPE_FLAGS.USER_ENTERED_UPLEVEL_SCOPE | DSOP_SCOPE_TYPE_FLAGS.UPLEVEL_JOINED_DOMAIN | DSOP_SCOPE_TYPE_FLAGS.TARGET_COMPUTER | DSOP_SCOPE_TYPE_FLAGS.GLOBAL_CATALOG | DSOP_SCOPE_TYPE_FLAGS.ENTERPRISE_DOMAIN | DSOP_SCOPE_TYPE_FLAGS.DOWNLEVEL_JOINED_DOMAIN;
                    if (((value & typesArray[1]) != ObjectPickerTypes.None) && ((value & typesArray[2]) != ObjectPickerTypes.None))
                    {
                        DSOP_UPLEVEL_FILTER_FLAGS dsop_uplevel_filter_flags2 = DSOP_UPLEVEL_FILTER_FLAGS.DOMAIN_LOCAL_GROUPS_DL | DSOP_UPLEVEL_FILTER_FLAGS.GLOBAL_GROUPS_SE | DSOP_UPLEVEL_FILTER_FLAGS.GLOBAL_GROUPS_DL | DSOP_UPLEVEL_FILTER_FLAGS.DOMAIN_LOCAL_GROUPS_SE | DSOP_UPLEVEL_FILTER_FLAGS.UNIVERSAL_GROUPS_SE | DSOP_UPLEVEL_FILTER_FLAGS.USERS | DSOP_UPLEVEL_FILTER_FLAGS.INCLUDE_ADVANCED_VIEW | DSOP_UPLEVEL_FILTER_FLAGS.UNIVERSAL_GROUPS_DL;
                        DSOP_DOWNLEVEL_FILTER_FLAGS dsop_downlevel_filter_flags2 = DSOP_DOWNLEVEL_FILTER_FLAGS.LOCAL_GROUPS | DSOP_DOWNLEVEL_FILTER_FLAGS.GLOBAL_GROUPS | DSOP_DOWNLEVEL_FILTER_FLAGS.USERS;
                        string[] strArray3 = new string[] { "sAMAccountName", "objectsid" };
                        this.AttributesToRetrieve = strArray3;
                        if ((value & typesArray[3]) != ObjectPickerTypes.None)
                        {
                            dsop_uplevel_filter_flags2 |= DSOP_UPLEVEL_FILTER_FLAGS.WELL_KNOWN_PRINCIPALS | DSOP_UPLEVEL_FILTER_FLAGS.BUILTIN_GROUPS;
                            dsop_downlevel_filter_flags2 |= DSOP_DOWNLEVEL_FILTER_FLAGS.LOCAL_SERVICE | DSOP_DOWNLEVEL_FILTER_FLAGS.NETWORK_SERVICE | DSOP_DOWNLEVEL_FILTER_FLAGS.SYSTEM;
                        }
                        this.AddScopeInfo(scopeTypeFlags, DSOP_SCOPE_FLAGS.DEFAULT_FILTER_USERS | DSOP_SCOPE_FLAGS.DEFAULT_FILTER_GROUPS, dsop_uplevel_filter_flags2, dsop_downlevel_filter_flags2);
                    }
                    else if ((value & typesArray[0]) != ObjectPickerTypes.None)
                    {
                        this.AddScopeInfo(DSOP_SCOPE_TYPE_FLAGS.WORKGROUP | DSOP_SCOPE_TYPE_FLAGS.EXTERNAL_DOWNLEVEL_DOMAIN | DSOP_SCOPE_TYPE_FLAGS.EXTERNAL_UPLEVEL_DOMAIN | DSOP_SCOPE_TYPE_FLAGS.USER_ENTERED_DOWNLEVEL_SCOPE | DSOP_SCOPE_TYPE_FLAGS.USER_ENTERED_UPLEVEL_SCOPE | DSOP_SCOPE_TYPE_FLAGS.GLOBAL_CATALOG | DSOP_SCOPE_TYPE_FLAGS.ENTERPRISE_DOMAIN, DSOP_SCOPE_FLAGS.DEFAULT_FILTER_COMPUTERS, DSOP_UPLEVEL_FILTER_FLAGS.COMPUTERS, DSOP_DOWNLEVEL_FILTER_FLAGS.COMPUTERS);
                    }
                    else if ((value & typesArray[1]) != ObjectPickerTypes.None)
                    {
                        string[] strArray4 = new string[] { "sAMAccountName", "objectsid" };
                        this.AttributesToRetrieve = strArray4;
                        this.AddScopeInfo(scopeTypeFlags, DSOP_SCOPE_FLAGS.DEFAULT_FILTER_USERS, DSOP_UPLEVEL_FILTER_FLAGS.USERS, DSOP_DOWNLEVEL_FILTER_FLAGS.USERS);
                    }
                }
            }

            public PickerObject[] Picks
            {
                get; private set;
            }

            public string TargetComputer
            {
                get { return targetComputer; } set { targetComputer = value; }
            }

            public override void Reset()
            {
            }

            internal static IntPtr AddOffset(IntPtr ptr, int offset)
            {
                if (IntPtr.Size == 4)
                {
                    return new IntPtr(ptr.ToInt32() + offset);
                }
                return new IntPtr(ptr.ToInt64() + offset);
            }

            protected override bool RunDialog(IntPtr owner)
            {
                PickerObject[] ret = Show(owner);
                Picks = ret;
                return (ret != null && ret.Length > 0);
            }

            [DllImport("ole32.dll")]
            private static extern void ReleaseStgMedium(ref STGMEDIUM pmedium);

            private void AddScopeInfo(DSOP_SCOPE_TYPE_FLAGS scopeTypeFlags, DSOP_SCOPE_FLAGS scopeInitInfoFlags, DSOP_UPLEVEL_FILTER_FLAGS uplevelFilterFlags, DSOP_DOWNLEVEL_FILTER_FLAGS downlevelFlags)
            {
                if (scopeTypeFlags == ((DSOP_SCOPE_TYPE_FLAGS)0))
                {
                    throw new ArgumentOutOfRangeException("scopeTypeFlags");
                }
                DSOP_SCOPE_INIT_INFO item = new DSOP_SCOPE_INIT_INFO();
                item.cbSize = (uint)Marshal.SizeOf(typeof(DSOP_SCOPE_INIT_INFO));
                item.flType = scopeTypeFlags;
                item.flScope = scopeInitInfoFlags;
                item.FilterFlags.Uplevel.flBothModes = uplevelFilterFlags;
                item.FilterFlags.flDownlevel = downlevelFlags;
                item.pwzADsPath = null;
                item.pwzDcName = null;
                item.hr = 0;
                this.scopeInfos.Add(item);
            }

            private PickerObject[] GetSelections(IDataObject dataObject)
            {
                STGMEDIUM stgmedium;
                if (dataObject == null)
                {
                    return null;
                }
                PickerObject[] objArray = null;
                FORMATETC pFormatetc = new FORMATETC();
                pFormatetc.cfFormat = DataFormats.GetFormat("CFSTR_DSOP_DS_SELECTION_LIST").Id;
                pFormatetc.ptd = IntPtr.Zero;
                pFormatetc.dwAspect = 1;
                pFormatetc.lindex = -1;
                pFormatetc.tymed = 1;
                dataObject.GetData(ref pFormatetc, out stgmedium);
                IntPtr ptr = GlobalLock(stgmedium.hGlobal);
                try
                {
                    int num = Marshal.ReadInt32(ptr);
                    objArray = new PickerObject[num];
                    ptr = AddOffset(ptr, Marshal.SizeOf(typeof(uint)));
                    int cVars = Marshal.ReadInt32(ptr);
                    ptr = AddOffset(ptr, Marshal.SizeOf(typeof(uint)));
                    int offset = Marshal.SizeOf(typeof(DS_SELECTION));
                    for (int i = 0; i < num; i++)
                    {
                        DS_SELECTION ds_selection = (DS_SELECTION)Marshal.PtrToStructure(ptr, typeof(DS_SELECTION));
                        object[] attributes = null;
                        if (ds_selection.pvarFetchedAttributes != IntPtr.Zero)
                        {
                            attributes = Marshal.GetObjectsForNativeVariants(ds_selection.pvarFetchedAttributes, cVars);
                        }
                        ObjectPickerTypes none = ObjectPickerTypes.None;
                        string pwzName = string.Empty;
                        if (((attributes != null) && (0 < attributes.Length)) && !string.IsNullOrEmpty((string)attributes[0]))
                        {
                            pwzName = (string)attributes[0];
                        }
                        else if (string.IsNullOrEmpty(ds_selection.pwzUPN))
                        {
                            pwzName = ds_selection.pwzName;
                        }
                        else
                        {
                            string[] strArray = ds_selection.pwzUPN.Split(new char[] { '@' });
                            if (strArray.Length > 0)
                            {
                                pwzName = strArray[0];
                            }
                        }
                        bool flag = false;
                        string domainName = string.Empty;
                        if ((attributes != null) && !string.IsNullOrEmpty(ds_selection.pwzADsPath))
                        {
                            string str4;
                            int num5;
                            SecurityIdentifier sid = new SecurityIdentifier((byte[])attributes[1], 0);
                            if (AccountUtils.LookupAccountSid(sid, out str4, out domainName, out num5) != 0)
                            {
                                string[] strArray2 = ds_selection.pwzADsPath.Split(new char[] { ',' });
                                if (strArray2.Length > 0)
                                {
                                    for (int j = 0; j < strArray2.Length; j++)
                                    {
                                        if (strArray2[j].Contains("DC="))
                                        {
                                            string[] strArray3 = strArray2[j].Split(new char[] { '=' });
                                            if (strArray3.Length == 2)
                                            {
                                                domainName = strArray3[1];
                                                break;
                                            }
                                        }
                                    }
                                    if (domainName.EndsWith(".", StringComparison.Ordinal))
                                    {
                                        domainName = domainName.Substring(0, domainName.Length - 1);
                                    }
                                }
                            }
                        }
                        if (string.IsNullOrEmpty(domainName))
                        {
                            if (string.IsNullOrEmpty(this.targetComputer) || (string.Compare(this.targetComputer, ".", StringComparison.OrdinalIgnoreCase) == 0))
                            {
                                domainName = Environment.MachineName;
                            }
                            else
                            {
                                domainName = this.targetComputer;
                            }
                            pwzName = ds_selection.pwzName;
                            flag = true;
                        }
                        StringBuilder builder = new StringBuilder();
                        if (string.Compare(ds_selection.pwzClass, "Group", StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            none = ObjectPickerTypes.Group;
                            if (!flag)
                            {
                                builder.Append(domainName.ToUpper(CultureInfo.InvariantCulture));
                                builder.Append(@"\");
                            }
                        }
                        else if (string.Compare(ds_selection.pwzClass, "Computer", StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            none = ObjectPickerTypes.Computer;
                            if (!flag)
                            {
                                builder.Append(domainName.ToUpper(CultureInfo.InvariantCulture));
                                builder.Append(@"\");
                            }
                        }
                        else
                        {
                            none = ObjectPickerTypes.User;
                            string str7 = pwzName;
                            if (str7.Contains(@"\"))
                            {
                                char[] separator = @"\".ToCharArray();
                                string[] strArray4 = str7.Split(separator);
                                if (strArray4.Length > 0)
                                {
                                    pwzName = strArray4[strArray4.Length - 1];
                                }
                            }
                            else
                            {
                                pwzName = str7;
                            }
                            builder.Append(domainName.ToUpper(CultureInfo.InvariantCulture));
                            builder.Append(@"\");
                        }
                        builder.Append(pwzName);
                        objArray[i] = new PickerObject(ds_selection.pwzADsPath, ds_selection.pwzClass, builder.ToString(), ds_selection.pwzUPN, attributes, none);
                        ptr = AddOffset(ptr, offset);
                    }
                }
                finally
                {
                    GlobalUnlock(stgmedium.hGlobal);
                    ReleaseStgMedium(ref stgmedium);
                }
                return objArray;
            }

            private PickerObject[] Show(IntPtr owner)
            {
                PickerObject[] selections = null;
                DSOP_INIT_INFO structure = new DSOP_INIT_INFO();
                structure.cbSize = (uint)Marshal.SizeOf(structure);
                structure.pwzTargetComputer = this.targetComputer;
                structure.flOptions = this.initInfoFlags;
                structure.cAttributesToFetch = (uint)this.attributeNames.Length;
                SafeGlobalHandle handle = null;
                SafeGlobalHandle handle2 = null;
                int index = 0;
                int num2 = 0;
                try
                {
                    IDataObject obj2;
                    handle = new SafeGlobalHandle(Marshal.SizeOf(typeof(IntPtr)) * this.attributeNames.Length);
                    handle2 = new SafeGlobalHandle(Marshal.SizeOf(typeof(DSOP_SCOPE_INIT_INFO)) * this.scopeInfos.Count);
                    index = 0;
                    while (index < this.attributeNames.Length)
                    {
                        Marshal.WriteIntPtr(handle.Handle, index * Marshal.SizeOf(typeof(IntPtr)), Marshal.StringToHGlobalUni(this.attributeNames[index]));
                        index++;
                    }
                    structure.apwzAttributeNames = handle.Handle;
                    IntPtr ptr = handle2.Handle;
                    num2 = 0;
                    while (num2 < this.scopeInfos.Count)
                    {
                        Marshal.StructureToPtr(this.scopeInfos[num2], ptr, false);
                        ptr = AddOffset(ptr, Marshal.SizeOf(typeof(DSOP_SCOPE_INIT_INFO)));
                        num2++;
                    }
                    structure.cDsScopeInfos = (uint)this.scopeInfos.Count;
                    structure.aDsScopeInfos = handle2.Handle;
                    IDsObjectPicker picker = (IDsObjectPicker)new DSObjectPicker();
                    picker.Initialize(ref structure);
                    picker.InvokeDialog(owner, out obj2);
                    selections = this.GetSelections(obj2);
                }
                finally
                {
                    if (handle != null)
                    {
                        for (int i = 0; i < index; i++)
                        {
                            Marshal.FreeHGlobal(Marshal.ReadIntPtr(handle.Handle, i * Marshal.SizeOf(typeof(IntPtr))));
                        }
                        handle.Close();
                    }
                    if (handle2 != null)
                    {
                        IntPtr ptr2 = handle2.Handle;
                        for (int j = 0; j < num2; j++)
                        {
                            Marshal.DestroyStructure(ptr2, typeof(DSOP_SCOPE_INIT_INFO));
                            ptr2 = AddOffset(ptr2, Marshal.SizeOf(typeof(DSOP_SCOPE_INIT_INFO)));
                        }
                        handle2.Close();
                    }
                }
                return selections;
            }

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
            private struct DSOP_FILTER
            {
                public DSOP_UPLEVEL_FILTER Uplevel;
                public DSOP_DOWNLEVEL_FILTER_FLAGS flDownlevel;
            }

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
            private struct DSOP_INIT_INFO
            {
                public uint cbSize;
                [MarshalAs(UnmanagedType.LPWStr)]
                public string pwzTargetComputer;
                public uint cDsScopeInfos;
                [SuppressMessage("Microsoft.Reliability", "CA2006:UseSafeHandleToEncapsulateNativeResources")]
                public IntPtr aDsScopeInfos;
                public DSOP_INIT_INFO_FLAGS flOptions;
                public uint cAttributesToFetch;
                [SuppressMessage("Microsoft.Reliability", "CA2006:UseSafeHandleToEncapsulateNativeResources")]
                public IntPtr apwzAttributeNames;
            }

            [Serializable,
            StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
            private struct DSOP_SCOPE_INIT_INFO
            {
                public uint cbSize;
                public DSOP_SCOPE_TYPE_FLAGS flType;
                public DSOP_SCOPE_FLAGS flScope;
                [NonSerialized,
                MarshalAs(UnmanagedType.Struct)]
                public DSOP_FILTER FilterFlags;
                [MarshalAs(UnmanagedType.LPWStr)]
                public string pwzDcName;
                [MarshalAs(UnmanagedType.LPWStr)]
                public string pwzADsPath;
                public uint hr;
            }

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
            private struct DSOP_UPLEVEL_FILTER
            {
                public DSOP_UPLEVEL_FILTER_FLAGS flBothModes;
                public DSOP_UPLEVEL_FILTER_FLAGS flMixedModeOnly;
                public DSOP_UPLEVEL_FILTER_FLAGS flNativeModeOnly;
            }

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
            private struct DS_SELECTION
            {
                [MarshalAs(UnmanagedType.LPWStr)]
                public string pwzName;
                [MarshalAs(UnmanagedType.LPWStr)]
                public string pwzADsPath;
                [MarshalAs(UnmanagedType.LPWStr)]
                public string pwzClass;
                [MarshalAs(UnmanagedType.LPWStr)]
                public string pwzUPN;
                [SuppressMessage("Microsoft.Reliability", "CA2006:UseSafeHandleToEncapsulateNativeResources")]
                public IntPtr pvarFetchedAttributes;
                public DSOP_SCOPE_TYPE_FLAGS flScopeType;
            }

            [StructLayout(LayoutKind.Sequential)]
            private struct FORMATETC
            {
                public int cfFormat;
                [SuppressMessage("Microsoft.Reliability", "CA2006:UseSafeHandleToEncapsulateNativeResources")]
                public IntPtr ptd;
                public uint dwAspect;
                public int lindex;
                public uint tymed;
            }

            [StructLayout(LayoutKind.Sequential)]
            private struct STGMEDIUM
            {
                public uint tymed;
                [SuppressMessage("Microsoft.Reliability", "CA2006:UseSafeHandleToEncapsulateNativeResources")]
                public IntPtr hGlobal;
                [SuppressMessage("Microsoft.Reliability", "CA2006:UseSafeHandleToEncapsulateNativeResources")]
                public IntPtr pUnkForRelease;
            }

            [ComImport,
            Guid("17D6CCD8-3B7B-11D2-B9E0-00C04FD8DBF7")]
            private class DSObjectPicker
            {
            }
        }

        internal class SafeGlobalHandle : SafeHandle
        {
            // Methods
            public SafeGlobalHandle()
                : base(IntPtr.Zero, true)
            {
            }

            public SafeGlobalHandle(int cb)
                : base(IntPtr.Zero, true)
            {
                base.SetHandle(Marshal.AllocHGlobal(cb));
            }

            // Properties
            public IntPtr Handle
            {
                get
                {
                    return base.handle;
                }
            }

            public override bool IsInvalid
            {
                get
                {
                    return (base.handle == IntPtr.Zero);
                }
            }

            public void AllocateHandle(int cb)
            {
                base.SetHandle(Marshal.AllocHGlobal(cb));
            }

            public void InitHandle(IntPtr inHandle)
            {
                base.SetHandle(inHandle);
            }

            protected override bool ReleaseHandle()
            {
                if (base.handle != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(base.handle);
                    base.SetHandleAsInvalid();
                }
                return true;
            }
        }
    }
}