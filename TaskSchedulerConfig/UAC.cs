/****************************** Module Header ******************************\
 Module Name:  NativeMethod.cs
 Project:      CSUACSelfElevation
 Copyright (c) Microsoft Corporation.

 The P/Invoke signature some native Windows APIs used by the code sample.

 This source is subject to the Microsoft Public License.
 See http://www.microsoft.com/en-us/openness/resources/licenses.aspx#MPL
 All other rights reserved.

 THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
 EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
 WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
\***************************************************************************/

using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using System.ComponentModel;
using System.Diagnostics;
using System.Security.Principal;

namespace CSUACSelfElevation
{
	internal static class UACHandler
	{
		/// <summary>
		/// The function checks whether the primary access token of the process belongs 
		/// to user account that is a member of the local Administrators group, even if 
		/// it currently is not elevated.
		/// </summary>
		/// <returns>
		/// Returns true if the primary access token of the process belongs to user 
		/// account that is a member of the local Administrators group. Returns false 
		/// if the token does not.
		/// </returns>
		/// <exception cref="System.ComponentModel.Win32Exception">
		/// When any native Windows API call fails, the function throws a Win32Exception 
		/// with the last error code.
		/// </exception>
		public static bool IsUserInAdminGroup()
		{
			bool fInAdminGroup = false;
			SafeTokenHandle hToken = null;
			SafeTokenHandle hTokenToCheck = null;
			IntPtr pElevationType = IntPtr.Zero;
			IntPtr pLinkedToken = IntPtr.Zero;
			int cbSize = 0;

			try
			{
				// Open the access token of the current process for query and duplicate.
				if (!NativeMethods.OpenProcessToken(Process.GetCurrentProcess().Handle,
					NativeMethods.TOKEN_QUERY | NativeMethods.TOKEN_DUPLICATE, out hToken))
				{
					throw new Win32Exception();
				}

				// Determine whether system is running Windows Vista or later operating 
				// systems (major version >= 6) because they support linked tokens, but 
				// previous versions (major version < 6) do not.
				if (Environment.OSVersion.Version.Major >= 6)
				{
					// Running Windows Vista or later (major version >= 6). 
					// Determine token type: limited, elevated, or default. 

					// Allocate a buffer for the elevation type information.
					cbSize = sizeof(TOKEN_ELEVATION_TYPE);
					pElevationType = Marshal.AllocHGlobal(cbSize);
					if (pElevationType == IntPtr.Zero)
					{
						throw new Win32Exception();
					}

					// Retrieve token elevation type information.
					if (!NativeMethods.GetTokenInformation(hToken,
						TOKEN_INFORMATION_CLASS.TokenElevationType, pElevationType,
						cbSize, out cbSize))
					{
						throw new Win32Exception();
					}

					// Marshal the TOKEN_ELEVATION_TYPE enum from native to .NET.
					TOKEN_ELEVATION_TYPE elevType = (TOKEN_ELEVATION_TYPE)
						Marshal.ReadInt32(pElevationType);

					// If limited, get the linked elevated token for further check.
					if (elevType == TOKEN_ELEVATION_TYPE.TokenElevationTypeLimited)
					{
						// Allocate a buffer for the linked token.
						cbSize = IntPtr.Size;
						pLinkedToken = Marshal.AllocHGlobal(cbSize);
						if (pLinkedToken == IntPtr.Zero)
						{
							throw new Win32Exception();
						}

						// Get the linked token.
						if (!NativeMethods.GetTokenInformation(hToken,
							TOKEN_INFORMATION_CLASS.TokenLinkedToken, pLinkedToken,
							cbSize, out cbSize))
						{
							throw new Win32Exception();
						}

						// Marshal the linked token value from native to .NET.
						IntPtr hLinkedToken = Marshal.ReadIntPtr(pLinkedToken);
						hTokenToCheck = new SafeTokenHandle(hLinkedToken);
					}
				}

				// CheckTokenMembership requires an impersonation token. If we just got 
				// a linked token, it already is an impersonation token.  If we did not 
				// get a linked token, duplicate the original into an impersonation 
				// token for CheckTokenMembership.
				if (hTokenToCheck == null)
				{
					if (!NativeMethods.DuplicateToken(hToken,
						SECURITY_IMPERSONATION_LEVEL.SecurityIdentification,
						out hTokenToCheck))
					{
						throw new Win32Exception();
					}
				}

				// Check if the token to be checked contains admin SID.
				WindowsIdentity id = new WindowsIdentity(hTokenToCheck.DangerousGetHandle());
				WindowsPrincipal principal = new WindowsPrincipal(id);
				fInAdminGroup = principal.IsInRole(WindowsBuiltInRole.Administrator);
			}
			finally
			{
				// Centralized cleanup for all allocated resources. 
				if (hToken != null)
				{
					hToken.Close();
					hToken = null;
				}
				if (hTokenToCheck != null)
				{
					hTokenToCheck.Close();
					hTokenToCheck = null;
				}
				if (pElevationType != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(pElevationType);
					pElevationType = IntPtr.Zero;
				}
				if (pLinkedToken != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(pLinkedToken);
					pLinkedToken = IntPtr.Zero;
				}
			}

			return fInAdminGroup;
		}


		/// <summary>
		/// The function checks whether the current process is run as administrator.
		/// In other words, it dictates whether the primary access token of the 
		/// process belongs to user account that is a member of the local 
		/// Administrators group and it is elevated.
		/// </summary>
		/// <returns>
		/// Returns true if the primary access token of the process belongs to user 
		/// account that is a member of the local Administrators group and it is 
		/// elevated. Returns false if the token does not.
		/// </returns>
		public static bool IsRunAsAdmin()
		{
			WindowsIdentity id = WindowsIdentity.GetCurrent();
			WindowsPrincipal principal = new WindowsPrincipal(id);
			return principal.IsInRole(WindowsBuiltInRole.Administrator);
		}


		/// <summary>
		/// The function gets the elevation information of the current process. It 
		/// dictates whether the process is elevated or not. Token elevation is only 
		/// available on Windows Vista and newer operating systems, thus 
		/// IsProcessElevated throws a C++ exception if it is called on systems prior 
		/// to Windows Vista. It is not appropriate to use this function to determine 
		/// whether a process is run as administrator.
		/// </summary>
		/// <returns>
		/// Returns true if the process is elevated. Returns false if it is not.
		/// </returns>
		/// <exception cref="System.ComponentModel.Win32Exception">
		/// When any native Windows API call fails, the function throws a Win32Exception 
		/// with the last error code.
		/// </exception>
		/// <remarks>
		/// TOKEN_INFORMATION_CLASS provides TokenElevationType to check the elevation 
		/// type (TokenElevationTypeDefault / TokenElevationTypeLimited / 
		/// TokenElevationTypeFull) of the process. It is different from TokenElevation 
		/// in that, when UAC is turned off, elevation type always returns 
		/// TokenElevationTypeDefault even though the process is elevated (Integrity 
		/// Level == High). In other words, it is not safe to say if the process is 
		/// elevated based on elevation type. Instead, we should use TokenElevation. 
		/// </remarks>
		public static bool IsProcessElevated()
		{
			bool fIsElevated = false;
			SafeTokenHandle hToken = null;
			int cbTokenElevation = 0;
			IntPtr pTokenElevation = IntPtr.Zero;

			try
			{
				// Open the access token of the current process with TOKEN_QUERY.
				if (!NativeMethods.OpenProcessToken(Process.GetCurrentProcess().Handle,
					NativeMethods.TOKEN_QUERY, out hToken))
				{
					throw new Win32Exception();
				}

				// Allocate a buffer for the elevation information.
				cbTokenElevation = Marshal.SizeOf(typeof(TOKEN_ELEVATION));
				pTokenElevation = Marshal.AllocHGlobal(cbTokenElevation);
				if (pTokenElevation == IntPtr.Zero)
				{
					throw new Win32Exception();
				}

				// Retrieve token elevation information.
				if (!NativeMethods.GetTokenInformation(hToken,
					TOKEN_INFORMATION_CLASS.TokenElevation, pTokenElevation,
					cbTokenElevation, out cbTokenElevation))
				{
					// When the process is run on operating systems prior to Windows 
					// Vista, GetTokenInformation returns false with the error code 
					// ERROR_INVALID_PARAMETER because TokenElevation is not supported 
					// on those operating systems.
					throw new Win32Exception();
				}

				// Marshal the TOKEN_ELEVATION struct from native to .NET object.
				TOKEN_ELEVATION elevation = (TOKEN_ELEVATION)Marshal.PtrToStructure(
					pTokenElevation, typeof(TOKEN_ELEVATION));

				// TOKEN_ELEVATION.TokenIsElevated is a non-zero value if the token 
				// has elevated privileges; otherwise, a zero value.
				fIsElevated = (elevation.TokenIsElevated != 0);
			}
			finally
			{
				// Centralized cleanup for all allocated resources. 
				if (hToken != null)
				{
					hToken.Close();
					hToken = null;
				}
				if (pTokenElevation != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(pTokenElevation);
					pTokenElevation = IntPtr.Zero;
					cbTokenElevation = 0;
				}
			}

			return fIsElevated;
		}

		public static bool IsUacEnabled()
		{
			if (System.Environment.OSVersion.Version.Major < 6)
				return true;
			using (var uacKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System", false))
				return uacKey.GetValue("EnableLUA", 0).Equals(1);
		}

		/// <summary>
		/// The function gets the integrity level of the current process. Integrity 
		/// level is only available on Windows Vista and newer operating systems, thus 
		/// GetProcessIntegrityLevel throws a C++ exception if it is called on systems 
		/// prior to Windows Vista.
		/// </summary>
		/// <returns>
		/// Returns the integrity level of the current process. It is usually one of 
		/// these values:
		/// 
		///    SECURITY_MANDATORY_UNTRUSTED_RID - means untrusted level. It is used 
		///    by processes started by the Anonymous group. Blocks most write access.
		///    (SID: S-1-16-0x0)
		///    
		///    SECURITY_MANDATORY_LOW_RID - means low integrity level. It is used by
		///    Protected Mode Internet Explorer. Blocks write access to most objects 
		///    (such as files and registry keys) on the system. (SID: S-1-16-0x1000)
		/// 
		///    SECURITY_MANDATORY_MEDIUM_RID - means medium integrity level. It is 
		///    used by normal applications being launched while UAC is enabled. 
		///    (SID: S-1-16-0x2000)
		///    
		///    SECURITY_MANDATORY_HIGH_RID - means high integrity level. It is used 
		///    by administrative applications launched through elevation when UAC is 
		///    enabled, or normal applications if UAC is disabled and the user is an 
		///    administrator. (SID: S-1-16-0x3000)
		///    
		///    SECURITY_MANDATORY_SYSTEM_RID - means system integrity level. It is 
		///    used by services and other system-level applications (such as WinInit, 
		///    WinLogon, SMSS, etc.)  (SID: S-1-16-0x4000)
		/// 
		/// </returns>
		/// <exception cref="System.ComponentModel.Win32Exception">
		/// When any native Windows API call fails, the function throws a Win32Exception 
		/// with the last error code.
		/// </exception>
		public static int GetProcessIntegrityLevel()
		{
			int IL = -1;
			SafeTokenHandle hToken = null;
			int cbTokenIL = 0;
			IntPtr pTokenIL = IntPtr.Zero;

			try
			{
				// Open the access token of the current process with TOKEN_QUERY.
				if (!NativeMethods.OpenProcessToken(Process.GetCurrentProcess().Handle,
					NativeMethods.TOKEN_QUERY, out hToken))
				{
					throw new Win32Exception();
				}

				// Then we must query the size of the integrity level information 
				// associated with the token. Note that we expect GetTokenInformation 
				// to return false with the ERROR_INSUFFICIENT_BUFFER error code 
				// because we've given it a null buffer. On exit cbTokenIL will tell 
				// the size of the group information.
				if (!NativeMethods.GetTokenInformation(hToken,
					TOKEN_INFORMATION_CLASS.TokenIntegrityLevel, IntPtr.Zero, 0,
					out cbTokenIL))
				{
					int error = Marshal.GetLastWin32Error();
					if (error != NativeMethods.ERROR_INSUFFICIENT_BUFFER)
					{
						// When the process is run on operating systems prior to 
						// Windows Vista, GetTokenInformation returns false with the 
						// ERROR_INVALID_PARAMETER error code because 
						// TokenIntegrityLevel is not supported on those OS's.
						throw new Win32Exception(error);
					}
				}

				// Now we allocate a buffer for the integrity level information.
				pTokenIL = Marshal.AllocHGlobal(cbTokenIL);
				if (pTokenIL == IntPtr.Zero)
				{
					throw new Win32Exception();
				}

				// Now we ask for the integrity level information again. This may fail 
				// if an administrator has added this account to an additional group 
				// between our first call to GetTokenInformation and this one.
				if (!NativeMethods.GetTokenInformation(hToken,
					TOKEN_INFORMATION_CLASS.TokenIntegrityLevel, pTokenIL, cbTokenIL,
					out cbTokenIL))
				{
					throw new Win32Exception();
				}

				// Marshal the TOKEN_MANDATORY_LABEL struct from native to .NET object.
				TOKEN_MANDATORY_LABEL tokenIL = (TOKEN_MANDATORY_LABEL)
					Marshal.PtrToStructure(pTokenIL, typeof(TOKEN_MANDATORY_LABEL));

				// Integrity Level SIDs are in the form of S-1-16-0xXXXX. (e.g. 
				// S-1-16-0x1000 stands for low integrity level SID). There is one 
				// and only one sub-authority.
				IntPtr pIL = NativeMethods.GetSidSubAuthority(tokenIL.Label.Sid, 0);
				IL = Marshal.ReadInt32(pIL);
			}
			finally
			{
				// Centralized cleanup for all allocated resources. 
				if (hToken != null)
				{
					hToken.Close();
					hToken = null;
				}
				if (pTokenIL != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(pTokenIL);
					pTokenIL = IntPtr.Zero;
					cbTokenIL = 0;
				}
			}

			return IL;
		}

		public static void RestartElevated()
		{
			// Elevate the process if it is not run as administrator.
			if (!IsRunAsAdmin())
			{
				// Launch itself as administrator
				ProcessStartInfo proc = new ProcessStartInfo(System.Windows.Forms.Application.ExecutablePath) { UseShellExecute = true, WorkingDirectory = Environment.CurrentDirectory, Verb = "runas" };
				try
				{
					Process.Start(proc);
				}
				catch
				{
					// The user refused the elevation. Do nothing and return directly.
					return;
				}

				System.Windows.Forms.Application.Exit();
			}
			else
			{
				System.Windows.Forms.MessageBox.Show("The process is running as administrator", "UAC");
			}
		}

		/// <summary>
		/// The TOKEN_INFORMATION_CLASS enumeration type contains values that 
		/// specify the type of information being assigned to or retrieved from 
		/// an access token.
		/// </summary>
		private enum TOKEN_INFORMATION_CLASS
		{
			TokenUser = 1,
			TokenGroups,
			TokenPrivileges,
			TokenOwner,
			TokenPrimaryGroup,
			TokenDefaultDacl,
			TokenSource,
			TokenType,
			TokenImpersonationLevel,
			TokenStatistics,
			TokenRestrictedSids,
			TokenSessionId,
			TokenGroupsAndPrivileges,
			TokenSessionReference,
			TokenSandBoxInert,
			TokenAuditPolicy,
			TokenOrigin,
			TokenElevationType,
			TokenLinkedToken,
			TokenElevation,
			TokenHasRestrictions,
			TokenAccessInformation,
			TokenVirtualizationAllowed,
			TokenVirtualizationEnabled,
			TokenIntegrityLevel,
			TokenUIAccess,
			TokenMandatoryPolicy,
			TokenLogonSid,
			MaxTokenInfoClass
		}

		/// <summary>
		/// The WELL_KNOWN_SID_TYPE enumeration type is a list of commonly used 
		/// security identifiers (SIDs). Programs can pass these values to the 
		/// CreateWellKnownSid function to create a SID from this list.
		/// </summary>
		private enum WELL_KNOWN_SID_TYPE
		{
			WinNullSid = 0,
			WinWorldSid = 1,
			WinLocalSid = 2,
			WinCreatorOwnerSid = 3,
			WinCreatorGroupSid = 4,
			WinCreatorOwnerServerSid = 5,
			WinCreatorGroupServerSid = 6,
			WinNtAuthoritySid = 7,
			WinDialupSid = 8,
			WinNetworkSid = 9,
			WinBatchSid = 10,
			WinInteractiveSid = 11,
			WinServiceSid = 12,
			WinAnonymousSid = 13,
			WinProxySid = 14,
			WinEnterpriseControllersSid = 15,
			WinSelfSid = 16,
			WinAuthenticatedUserSid = 17,
			WinRestrictedCodeSid = 18,
			WinTerminalServerSid = 19,
			WinRemoteLogonIdSid = 20,
			WinLogonIdsSid = 21,
			WinLocalSystemSid = 22,
			WinLocalServiceSid = 23,
			WinNetworkServiceSid = 24,
			WinBuiltinDomainSid = 25,
			WinBuiltinAdministratorsSid = 26,
			WinBuiltinUsersSid = 27,
			WinBuiltinGuestsSid = 28,
			WinBuiltinPowerUsersSid = 29,
			WinBuiltinAccountOperatorsSid = 30,
			WinBuiltinSystemOperatorsSid = 31,
			WinBuiltinPrintOperatorsSid = 32,
			WinBuiltinBackupOperatorsSid = 33,
			WinBuiltinReplicatorSid = 34,
			WinBuiltinPreWindows2000CompatibleAccessSid = 35,
			WinBuiltinRemoteDesktopUsersSid = 36,
			WinBuiltinNetworkConfigurationOperatorsSid = 37,
			WinAccountAdministratorSid = 38,
			WinAccountGuestSid = 39,
			WinAccountKrbtgtSid = 40,
			WinAccountDomainAdminsSid = 41,
			WinAccountDomainUsersSid = 42,
			WinAccountDomainGuestsSid = 43,
			WinAccountComputersSid = 44,
			WinAccountControllersSid = 45,
			WinAccountCertAdminsSid = 46,
			WinAccountSchemaAdminsSid = 47,
			WinAccountEnterpriseAdminsSid = 48,
			WinAccountPolicyAdminsSid = 49,
			WinAccountRasAndIasServersSid = 50,
			WinNTLMAuthenticationSid = 51,
			WinDigestAuthenticationSid = 52,
			WinSChannelAuthenticationSid = 53,
			WinThisOrganizationSid = 54,
			WinOtherOrganizationSid = 55,
			WinBuiltinIncomingForestTrustBuildersSid = 56,
			WinBuiltinPerfMonitoringUsersSid = 57,
			WinBuiltinPerfLoggingUsersSid = 58,
			WinBuiltinAuthorizationAccessSid = 59,
			WinBuiltinTerminalServerLicenseServersSid = 60,
			WinBuiltinDCOMUsersSid = 61,
			WinBuiltinIUsersSid = 62,
			WinIUserSid = 63,
			WinBuiltinCryptoOperatorsSid = 64,
			WinUntrustedLabelSid = 65,
			WinLowLabelSid = 66,
			WinMediumLabelSid = 67,
			WinHighLabelSid = 68,
			WinSystemLabelSid = 69,
			WinWriteRestrictedCodeSid = 70,
			WinCreatorOwnerRightsSid = 71,
			WinCacheablePrincipalsGroupSid = 72,
			WinNonCacheablePrincipalsGroupSid = 73,
			WinEnterpriseReadonlyControllersSid = 74,
			WinAccountReadonlyControllersSid = 75,
			WinBuiltinEventLogReadersGroup = 76,
			WinNewEnterpriseReadonlyControllersSid = 77,
			WinBuiltinCertSvcDComAccessGroup = 78
		}

		/// <summary>
		/// The SECURITY_IMPERSONATION_LEVEL enumeration type contains values 
		/// that specify security impersonation levels. Security impersonation 
		/// levels govern the degree to which a server process can act on behalf 
		/// of a client process.
		/// </summary>
		private enum SECURITY_IMPERSONATION_LEVEL
		{
			SecurityAnonymous,
			SecurityIdentification,
			SecurityImpersonation,
			SecurityDelegation
		}

		/// <summary>
		/// The TOKEN_ELEVATION_TYPE enumeration indicates the elevation type of 
		/// token being queried by the GetTokenInformation function or set by 
		/// the SetTokenInformation function.
		/// </summary>
		private enum TOKEN_ELEVATION_TYPE
		{
			TokenElevationTypeDefault = 1,
			TokenElevationTypeFull,
			TokenElevationTypeLimited
		}

		/// <summary>
		/// The structure represents a security identifier (SID) and its 
		/// attributes. SIDs are used to uniquely identify users or groups.
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		private struct SID_AND_ATTRIBUTES
		{
			public IntPtr Sid;
			public UInt32 Attributes;
		}

		/// <summary>
		/// The structure indicates whether a token has elevated privileges.
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		private struct TOKEN_ELEVATION
		{
			public Int32 TokenIsElevated;
		}

		/// <summary>
		/// The structure specifies the mandatory integrity level for a token.
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		private struct TOKEN_MANDATORY_LABEL
		{
			public SID_AND_ATTRIBUTES Label;
		}

		/// <summary>
		/// Represents a wrapper class for a token handle.
		/// </summary>
		private class SafeTokenHandle : SafeHandleZeroOrMinusOneIsInvalid
		{
			private SafeTokenHandle() : base(true)
			{
			}

			internal SafeTokenHandle(IntPtr handle) : base(true)
			{
				SetHandle(handle);
			}

			[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			internal static extern bool CloseHandle(IntPtr handle);

			protected override bool ReleaseHandle() => CloseHandle(handle);
		}

		private class NativeMethods
		{
			// Token Specific Access Rights

			public const UInt32 STANDARD_RIGHTS_REQUIRED = 0x000F0000;
			public const UInt32 STANDARD_RIGHTS_READ = 0x00020000;
			public const UInt32 TOKEN_ASSIGN_PRIMARY = 0x0001;
			public const UInt32 TOKEN_DUPLICATE = 0x0002;
			public const UInt32 TOKEN_IMPERSONATE = 0x0004;
			public const UInt32 TOKEN_QUERY = 0x0008;
			public const UInt32 TOKEN_QUERY_SOURCE = 0x0010;
			public const UInt32 TOKEN_ADJUST_PRIVILEGES = 0x0020;
			public const UInt32 TOKEN_ADJUST_GROUPS = 0x0040;
			public const UInt32 TOKEN_ADJUST_DEFAULT = 0x0080;
			public const UInt32 TOKEN_ADJUST_SESSIONID = 0x0100;
			public const UInt32 TOKEN_READ = (STANDARD_RIGHTS_READ | TOKEN_QUERY);
			public const UInt32 TOKEN_ALL_ACCESS = (STANDARD_RIGHTS_REQUIRED |
				TOKEN_ASSIGN_PRIMARY | TOKEN_DUPLICATE | TOKEN_IMPERSONATE |
				TOKEN_QUERY | TOKEN_QUERY_SOURCE | TOKEN_ADJUST_PRIVILEGES |
				TOKEN_ADJUST_GROUPS | TOKEN_ADJUST_DEFAULT | TOKEN_ADJUST_SESSIONID);


			public const Int32 ERROR_INSUFFICIENT_BUFFER = 122;


			// Integrity Levels

			public const Int32 SECURITY_MANDATORY_UNTRUSTED_RID = 0x00000000;
			public const Int32 SECURITY_MANDATORY_LOW_RID = 0x00001000;
			public const Int32 SECURITY_MANDATORY_MEDIUM_RID = 0x00002000;
			public const Int32 SECURITY_MANDATORY_HIGH_RID = 0x00003000;
			public const Int32 SECURITY_MANDATORY_SYSTEM_RID = 0x00004000;


			/// <summary>
			/// The function opens the access token associated with a process.
			/// </summary>
			/// <param name="hProcess">
			/// A handle to the process whose access token is opened.
			/// </param>
			/// <param name="desiredAccess">
			/// Specifies an access mask that specifies the requested types of 
			/// access to the access token. 
			/// </param>
			/// <param name="hToken">
			/// Outputs a handle that identifies the newly opened access token 
			/// when the function returns.
			/// </param>
			/// <returns></returns>
			[DllImport("advapi32", CharSet = CharSet.Auto, SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			public static extern bool OpenProcessToken(IntPtr hProcess,
				UInt32 desiredAccess, out SafeTokenHandle hToken);


			/// <summary>
			/// The function creates a new access token that duplicates one 
			/// already in existence.
			/// </summary>
			/// <param name="ExistingTokenHandle">
			/// A handle to an access token opened with TOKEN_DUPLICATE access.
			/// </param>
			/// <param name="ImpersonationLevel">
			/// Specifies a SECURITY_IMPERSONATION_LEVEL enumerated type that 
			/// supplies the impersonation level of the new token.
			/// </param>
			/// <param name="DuplicateTokenHandle">
			/// Outputs a handle to the duplicate token. 
			/// </param>
			/// <returns></returns>
			[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			public extern static bool DuplicateToken(
				SafeTokenHandle ExistingTokenHandle,
				SECURITY_IMPERSONATION_LEVEL ImpersonationLevel,
				out SafeTokenHandle DuplicateTokenHandle);


			/// <summary>
			/// The function retrieves a specified type of information about an 
			/// access token. The calling process must have appropriate access 
			/// rights to obtain the information.
			/// </summary>
			/// <param name="hToken">
			/// A handle to an access token from which information is retrieved.
			/// </param>
			/// <param name="tokenInfoClass">
			/// Specifies a value from the TOKEN_INFORMATION_CLASS enumerated 
			/// type to identify the type of information the function retrieves.
			/// </param>
			/// <param name="pTokenInfo">
			/// A pointer to a buffer the function fills with the requested 
			/// information.
			/// </param>
			/// <param name="tokenInfoLength">
			/// Specifies the size, in bytes, of the buffer pointed to by the 
			/// TokenInformation parameter. 
			/// </param>
			/// <param name="returnLength">
			/// A pointer to a variable that receives the number of bytes needed 
			/// for the buffer pointed to by the TokenInformation parameter. 
			/// </param>
			/// <returns></returns>
			[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			public static extern bool GetTokenInformation(
				SafeTokenHandle hToken,
				TOKEN_INFORMATION_CLASS tokenInfoClass,
				IntPtr pTokenInfo,
				Int32 tokenInfoLength,
				out Int32 returnLength);


			/// <summary>
			/// Sets the elevation required state for a specified button or 
			/// command link to display an elevated icon. 
			/// </summary>
			public const UInt32 BCM_SETSHIELD = 0x160C;


			/// <summary>
			/// Sends the specified message to a window or windows. The function 
			/// calls the window procedure for the specified window and does not 
			/// return until the window procedure has processed the message. 
			/// </summary>
			/// <param name="hWnd">
			/// Handle to the window whose window procedure will receive the 
			/// message.
			/// </param>
			/// <param name="Msg">Specifies the message to be sent.</param>
			/// <param name="wParam">
			/// Specifies additional message-specific information.
			/// </param>
			/// <param name="lParam">
			/// Specifies additional message-specific information.
			/// </param>
			/// <returns></returns>
			[DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
			public static extern int SendMessage(IntPtr hWnd, UInt32 Msg, int wParam, IntPtr lParam);


			/// <summary>
			/// The function returns a pointer to a specified sub-authority in a 
			/// security identifier (SID). The sub-authority value is a relative 
			/// identifier (RID).
			/// </summary>
			/// <param name="pSid">
			/// A pointer to the SID structure from which a pointer to a 
			/// sub-authority is to be returned.
			/// </param>
			/// <param name="nSubAuthority">
			/// Specifies an index value identifying the sub-authority array 
			/// element whose address the function will return.
			/// </param>
			/// <returns>
			/// If the function succeeds, the return value is a pointer to the 
			/// specified SID sub-authority. To get extended error information, 
			/// call GetLastError. If the function fails, the return value is 
			/// undefined. The function fails if the specified SID structure is 
			/// not valid or if the index value specified by the nSubAuthority 
			/// parameter is out of bounds.
			/// </returns>
			[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			public static extern IntPtr GetSidSubAuthority(IntPtr pSid, UInt32 nSubAuthority);
		}
	}
}