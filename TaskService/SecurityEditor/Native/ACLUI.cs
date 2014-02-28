using System;
using System.Runtime.InteropServices;
using System.Security.AccessControl;

namespace Microsoft.Win32
{
	internal enum SI_PAGE_ACTIVATED
	{
		DEFAULT = 0,
		PERM_ACTIVATED,
		AUDIT_ACTIVATED,
		OWNER_ACTIVATED,
		EFFECTIVE_ACTIVATED,
		SHARE_ACTIVATED,
		CENTRAL_POLICY_ACTIVATED,
	}

	internal enum SI_PAGE_TYPE
	{
		PERM = 0,
		ADVPERM,
		AUDIT,
		OWNER,
		EFFECTIVE,
		TAKEOWNERSHIP,
		SHARE,
	}

	[Flags]
	internal enum SiAccess
	{
		// SI_ACCESS flags
		SPECIFIC = 0x00010000L,
		GENERAL = 0x00020000L,
		CONTAINER = 0x00040000L, // general access, container-only
		PROPERTY = 0x00080000L,
	}

	[Flags]
	internal enum SiObjectInfo : uint
	{
		// SI_OBJECT_INFO flags
		EDIT_PERMS = 0x00000000L, // always implied
		EDIT_OWNER = 0x00000001L,
		EDIT_AUDITS = 0x00000002L,
		EDIT_ALL = 0x00000003L,
		CONTAINER = 0x00000004L,
		READONLY = 0x00000008L,
		ADVANCED = 0x00000010L,
		RESET = 0x00000020L, //equals to SI_RESET_DACL|SI_RESET_SACL|SI_RESET_OWNER
		OWNER_READONLY = 0x00000040L,
		EDIT_PROPERTIES = 0x00000080L,
		OWNER_RECURSE = 0x00000100L,
		NO_ACL_PROTECT = 0x00000200L,
		NO_TREE_APPLY = 0x00000400L,
		PAGE_TITLE = 0x00000800L,
		SERVER_IS_DC = 0x00001000L,
		RESET_DACL_TREE = 0x00004000L,
		RESET_SACL_TREE = 0x00008000L,
		OBJECT_GUID = 0x00010000L,
		EDIT_EFFECTIVE = 0x00020000L,
		RESET_DACL = 0x00040000L,
		RESET_SACL = 0x00080000L,
		RESET_OWNER = 0x00100000L,
		NO_ADDITIONAL_PERMISSION = 0x00200000L,
		VIEW_ONLY = 0x00400000L,
		PERMS_ELEVATION_REQUIRED = 0x01000000L,
		AUDITS_ELEVATION_REQUIRED = 0x02000000L,
		OWNER_ELEVATION_REQUIRED = 0x04000000L,
		SCOPE_ELEVATION_REQUIRED = 0x08000000L,
		MAY_WRITE = 0x10000000L, //not sure if user can write permission
		ENABLE_EDIT_ATTRIBUTE_CONDITION = 0x20000000L,
		ENABLE_CENTRAL_POLICY = 0x40000000L,
		DISABLE_DENY_ACE = 0x80000000L,
	}

	[Guid("965FC360-16FF-11d0-91CB-00AA00BBB723"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown), System.Security.SuppressUnmanagedCodeSecurity]
	internal interface ISecurityInformation
	{
		void GetAccessRights([In, MarshalAs(UnmanagedType.LPStruct)] Guid pguidObjectType,
						uint dwFlags, // SI_EDIT_AUDITS, SI_EDIT_PROPERTIES
						out SI_ACCESS ppAccess,
						out uint pcAccesses,
						out uint piDefaultAccess);

		void GetInheritTypes(IntPtr ppInheritTypes, IntPtr pcInheritTypes);

		void GetObjectInformation(/*PSI_OBJECT_INFO*/ IntPtr pObjectInfo);

		void GetSecurity(SecurityInfos RequestedInformation, out SECURITY_DESCRIPTOR ppSecurityDescriptor, [MarshalAs(UnmanagedType.Bool), In] bool fDefault);

		void MapGeneric([In, MarshalAs(UnmanagedType.LPStruct)] Guid pguidObjectType, IntPtr pAceFlags, IntPtr pMask);

		void PropertySheetPageCallback(IntPtr hwnd, uint uMsg, SI_PAGE_TYPE uPage);

		void SetSecurity(SecurityInfos SecurityInformation, ref SECURITY_DESCRIPTOR pSecurityDescriptor);
	}

	[StructLayoutAttribute(LayoutKind.Sequential)]
	internal struct SECURITY_DESCRIPTOR
	{
		public byte revision;
		public byte size;
		public short control;
		public IntPtr owner;
		public IntPtr group;
		public IntPtr sacl;
		public IntPtr dacl;
	}

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct SI_ACCESS
	{
		public IntPtr pguid;
		public UInt32 mask;
		public IntPtr pszName;            // may be resource ID
		public UInt32 dwFlags;
	}

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct SI_INHERIT_TYPE
	{
		private IntPtr pguid;
		private UInt32 dwFlags;
		private IntPtr pszName;            // may be resource ID
	}

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct SI_OBJECT_INFO
	{
		public SiObjectInfo dwFlags;
		public IntPtr hInstance;          // resources (e.g. strings) reside here
		private IntPtr pszServerName;      // must be present
		private IntPtr pszObjectName;      // must be present
		private IntPtr pszPageTitle;       // only valid if SI_PAGE_TITLE is set
		public Guid guidObjectType;     // only valid if SI_OBJECT_GUID is set
	}

	/*
	#define GET_PAGE_TYPE(X)    (UINT)((X) & 0x0000ffff)
	#define GET_ACTIVATION_TYPE(Y) (UINT)(((Y) >> 16) & 0x0000ffff)
	#define COMBINE_PAGE_ACTIVATION(X,Y) (UINT)(((Y) << 16) | X)

	#define DOBJ_RES_CONT           0x00000001L
	#define DOBJ_RES_ROOT           0x00000002L
	#define DOBJ_VOL_NTACLS         0x00000004L     // NTFS or OFS
	#define DOBJ_COND_NTACLS        0x00000008L     // Conditional aces supported.
	#define DOBJ_RIBBON_LAUNCH      0x00000010L     // Invoked from explorer ribbon.

	// Message to PropertySheetPageCallback (in addition to
	// PSPCB_CREATE and PSPCB_RELEASE)
	#define PSPCB_SI_INITDIALOG    (WM_USER + 1)
	*/
	/*
	#undef INTERFACE
	#define INTERFACE   ISecurityInformation2
	DECLARE_INTERFACE_IID_(ISecurityInformation2, IUnknown, "c3ccfdb4-6f88-11d2-a3ce-00c04fb1782a")
	{
		// *** IUnknown methods ***
		STDMETHOD(QueryInterface) (THIS_ _In_ REFIID riid, _Outptr_ void **ppvObj) PURE;
		STDMETHOD_(ULONG, AddRef) (THIS)  PURE;
		STDMETHOD_(ULONG, Release) (THIS) PURE;

		// *** ISecurityInformation2 methods ***
		STDMETHOD_(BOOL,IsDaclCanonical) (THIS_ IN PACL pDacl) PURE;
		STDMETHOD(LookupSids) (THIS_ IN ULONG cSids, IN PSID *rgpSids, OUT LPDATAOBJECT *ppdo) PURE;
	};
	ISecurityInformation2 *LPSECURITYINFO2;

	// HGLOBAL containing SID_INFO_LIST returned by ISecurityInformation2::LookupSids
	#define CFSTR_ACLUI_SID_INFO_LIST   TEXT("CFSTR_ACLUI_SID_INFO_LIST")

	// Data structures corresponding to CFSTR_ACLUI_SID_INFO_LIST
	struct _SID_INFO
	{
		PSID    pSid;
		PWSTR   pwzCommonName;
		PWSTR   pwzClass;       // Used for selecting icon, e.g. "User" or "Group"
		PWSTR   pwzUPN;         // Optional, may be NULL
	}
	struct _SID_INFO_LIST
	{
		ULONG       cItems;
		SID_INFO    aSidInfo[ANYSIZE_ARRAY];
	}

	#undef INTERFACE
	#define INTERFACE   IEffectivePermission
	DECLARE_INTERFACE_IID_(IEffectivePermission, IUnknown, "3853DC76-9F35-407c-88A1-D19344365FBC")
	{
		// *** IUnknown methods ***
		STDMETHOD(QueryInterface) (THIS_ _In_ REFIID riid, _Outptr_ void **ppvObj) PURE;
		STDMETHOD_(ULONG, AddRef) (THIS)  PURE;
		STDMETHOD_(ULONG, Release) (THIS) PURE;

		// *** ISecurityInformation methods ***
		STDMETHOD(GetEffectivePermission) (  THIS_ const GUID* pguidObjectType,
											 PSID pUserSid,
											 LPCWSTR pszServerName,
											 PSECURITY_DESCRIPTOR pSD,
											 POBJECT_TYPE_LIST *ppObjectTypeList,
											 ULONG *pcObjectTypeListLength,
											 PACCESS_MASK *ppGrantedAccessList,
											 ULONG *pcGrantedAccessListLength) PURE;
	};
	IEffectivePermission *LPEFFECTIVEPERMISSION;

	#undef INTERFACE
	#define INTERFACE   ISecurityObjectTypeInfo
	DECLARE_INTERFACE_IID_(ISecurityObjectTypeInfo, IUnknown, "FC3066EB-79EF-444b-9111-D18A75EBF2FA")
	{
		// *** IUnknown methods ***
		STDMETHOD(QueryInterface) (THIS_ _In_ REFIID riid, _Outptr_ void **ppvObj) PURE;
		STDMETHOD_(ULONG, AddRef) (THIS)  PURE;
		STDMETHOD_(ULONG, Release) (THIS) PURE;

		// *** ISecurityInformation methods ***
		STDMETHOD(GetInheritSource)(SECURITY_INFORMATION si,
									PACL pACL,
									PINHERITED_FROM *ppInheritArray) PURE;
	};
	ISecurityObjectTypeInfo *LPSecurityObjectTypeInfo;

	#if (NTDDI_VERSION >= NTDDI_VISTA)
	// Support for separation or read-only ACL viewer and elevated ACL editor
	#undef INTERFACE
	#define INTERFACE   ISecurityInformation3
	DECLARE_INTERFACE_IID_(ISecurityInformation3, IUnknown, "E2CDC9CC-31BD-4f8f-8C8B-B641AF516A1A")
	{
		// *** IUnknown methods ***
		STDMETHOD(QueryInterface) (THIS_ _In_ REFIID riid, _Outptr_ void **ppvObj) PURE;
		STDMETHOD_(ULONG, AddRef) (THIS)  PURE;
		STDMETHOD_(ULONG, Release) (THIS) PURE;

		// *** ISecurityInformation3 methods ***
		STDMETHOD(GetFullResourceName) (THIS_ _Outptr_ LPWSTR *ppszResourceName) PURE;
		STDMETHOD(OpenElevatedEditor) (THIS_ _In_ HWND hWnd, _In_ SI_PAGE_TYPE uPage) PURE;
	};
	ISecurityInformation3 *LPSECURITYINFO3;
	#endif // (NTDDI_VERSION >= NTDDI_VISTA)

	#if (NTDDI_VERSION >= NTDDI_WIN8)

	struct _SECURITY_OBJECT
	{
		PWSTR pwszName;
		_Field_size_bytes_ (cbData) PVOID pData;
		DWORD cbData;
		_Field_size_bytes_ (cbData2) PVOID pData2;
		DWORD cbData2;
		DWORD Id;
		BOOLEAN fWellKnown;
	} SECURITY_OBJECT, *PSECURITY_OBJECT;

	#define SECURITY_OBJECT_ID_OBJECT_SD      1
	#define SECURITY_OBJECT_ID_SHARE          2
	#define SECURITY_OBJECT_ID_CENTRAL_POLICY 3
	#define SECURITY_OBJECT_ID_CENTRAL_ACCESS_RULE 4

	struct _EFFPERM_RESULT_LIST
	{
		BOOLEAN fEvaluated;
		ULONG cObjectTypeListLength;
		_Field_size_(cObjectTypeListLength)
		OBJECT_TYPE_LIST *pObjectTypeList;
		_Field_size_(cObjectTypeListLength)
		ACCESS_MASK *pGrantedAccessList;
	} EFFPERM_RESULT_LIST, *PEFFPERM_RESULT_LIST;

	#undef INTERFACE
	#define INTERFACE   ISecurityInformation4
	DECLARE_INTERFACE_IID_(ISecurityInformation4, IUnknown, "EA961070-CD14-4621-ACE4-F63C03E583E4")
	{
		// *** IUnknown methods ***
		STDMETHOD(QueryInterface) (THIS_ _In_ REFIID riid, _Outptr_ void **ppvObj) PURE;
		STDMETHOD_(ULONG, AddRef) (THIS)  PURE;
		STDMETHOD_(ULONG, Release) (THIS) PURE;

		// *** ISecurityInformation4 methods ***
		STDMETHOD(GetSecondarySecurity) (THIS_
										 _Outptr_result_buffer_(*pSecurityObjectCount) PSECURITY_OBJECT *pSecurityObjects,
										 _Out_ PULONG pSecurityObjectCount) PURE;
	};

	ISecurityInformation4 *LPSECURITYINFO4;

	#undef INTERFACE
	#define INTERFACE   IEffectivePermission
	DECLARE_INTERFACE_IID_(IEffectivePermission2, IUnknown, "941FABCA-DD47-4FCA-90BB-B0E10255F20D")
	{
		// *** IUnknown methods ***
		STDMETHOD(QueryInterface) (THIS_ _In_ REFIID riid, _Outptr_ void **ppvObj) PURE;
		STDMETHOD_(ULONG, AddRef) (THIS)  PURE;
		STDMETHOD_(ULONG, Release) (THIS) PURE;

		// *** IEffectivePermission2 methods ***
		//STDMETHOD(GetEffectiveScopePermission) (THIS);
		STDMETHOD(ComputeEffectivePermissionWithSecondarySecurity) (THIS_
			_In_ PSID pSid,
			_In_opt_ PSID pDeviceSid,
			_In_ PCWSTR pszServerName,
			_Inout_updates_(dwSecurityObjectCount) PSECURITY_OBJECT pSecurityObjects,
			_In_ DWORD dwSecurityObjectCount,
			_In_opt_ PTOKEN_GROUPS pUserGroups,
			_When_(pUserGroups != NULL && *pAuthzUserGroupsOperations != AUTHZ_SID_OPERATION_REPLACE_ALL, _In_reads_(pUserGroups->GroupCount))
			_In_opt_ PAUTHZ_SID_OPERATION pAuthzUserGroupsOperations,
			_In_opt_ PTOKEN_GROUPS pDeviceGroups,
			_When_(pDeviceGroups != NULL && *pAuthzDeviceGroupsOperations != AUTHZ_SID_OPERATION_REPLACE_ALL, _In_reads_(pDeviceGroups->GroupCount))
			_In_opt_ PAUTHZ_SID_OPERATION pAuthzDeviceGroupsOperations,
			_In_opt_ PAUTHZ_SECURITY_ATTRIBUTES_INFORMATION pAuthzUserClaims,
			_When_(pAuthzUserClaims != NULL && *pAuthzUserClaimsOperations != AUTHZ_SECURITY_ATTRIBUTE_OPERATION_REPLACE_ALL, _In_reads_(pAuthzUserClaims->AttributeCount))
			_In_opt_ PAUTHZ_SECURITY_ATTRIBUTE_OPERATION pAuthzUserClaimsOperations,
			_In_opt_ PAUTHZ_SECURITY_ATTRIBUTES_INFORMATION pAuthzDeviceClaims,
			_When_(pAuthzDeviceClaims != NULL && *pAuthzDeviceClaimsOperations != AUTHZ_SECURITY_ATTRIBUTE_OPERATION_REPLACE_ALL, _In_reads_(pAuthzDeviceClaims->AttributeCount))
			_In_opt_ PAUTHZ_SECURITY_ATTRIBUTE_OPERATION pAuthzDeviceClaimsOperations,
			_Inout_updates_(dwSecurityObjectCount) PEFFPERM_RESULT_LIST pEffpermResultLists);
	};*/

	internal static partial class NativeMethods
	{
		[DllImport("aclui.dll", SetLastError=true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool EditSecurity(IntPtr hwndOwner, ref ISecurityInformation psi);
	}
}