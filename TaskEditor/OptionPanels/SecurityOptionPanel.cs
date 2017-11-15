using System;
using System.Security.Principal;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler.OptionPanels
{
	internal partial class SecurityOptionPanel : Microsoft.Win32.TaskScheduler.OptionPanels.OptionPanel
	{
		private static SecEdShim secEd = SecEdShim.GetNew();

		private bool flagUserIsAnAdmin, flagExecutorIsServiceAccount, flagRunOnlyWhenUserIsLoggedOn, flagExecutorIsGroup;

		public SecurityOptionPanel()
		{
			InitializeComponent();
			ComboBoxExtension.InitializeFromEnum(principalSIDTypeCombo.Items, typeof(TaskProcessTokenSidType), EditorProperties.Resources.ResourceManager, "SIDType", out var allVal);
			principalReqPrivilegesDropDown.Sorted = true;
			principalReqPrivilegesDropDown.InitializeFromEnum(typeof(TaskPrincipalPrivilege), EditorProperties.Resources.ResourceManager, "");
		}

		protected override void InitializePanel()
		{
			var v2_1 = td.Settings.Compatibility >= TaskCompatibility.V2_1;
			taskRunLevelCheck.Enabled = taskRegSDDLText.Enabled = parent.Editable && parent.IsV2;
			principalSIDTypeLabel.Enabled = principalSIDTypeCombo.Enabled = parent.Editable && v2_1;
			principalReqPrivilegesLabel.Enabled = principalReqPrivilegesDropDown.Enabled = false;
			taskRegSDDLBtn.Visible = (secEd != null && parent.IsV2);
			principalSIDTypeCombo.SelectedIndex = principalSIDTypeCombo.Items.IndexOf((long)td.Principal.ProcessTokenSidType);
			principalReqPrivilegesDropDown.CheckedFlagValue = 0;
			foreach (var s in td.Principal.RequiredPrivileges)
				principalReqPrivilegesDropDown.SetItemChecked(principalReqPrivilegesDropDown.Items.IndexOf(s.ToString()), true);
			taskRunLevelCheck.Checked = td.Principal.RunLevel == TaskRunLevel.Highest;
			flagUserIsAnAdmin = NativeMethods.AccountUtils.CurrentUserIsAdmin(parent.TaskService.TargetServer);
			if (td != null && td.Principal.LogonType == TaskLogonType.None) td.Principal.LogonType = TaskLogonType.InteractiveToken;
			SetUserControls(td != null ? td.Principal.LogonType : TaskLogonType.InteractiveTokenOrPassword);
			var sddl = td.RegistrationInfo.SecurityDescriptorSddlForm;
			if (string.IsNullOrEmpty(sddl) && parent.Task != null && parent.IsV2)
				try { sddl = parent.Task.GetSecurityDescriptorSddlForm(); } catch { }
			taskRegSDDLText.Text = sddl;
		}

		private void InvokeObjectPicker(string targetComputerName)
		{
			var v2 = parent.IsV2;
			var acct = String.Empty;
			if (!HelperMethods.SelectAccount(this, targetComputerName, ref acct, out flagExecutorIsGroup, out flagExecutorIsServiceAccount, out var sid))
				return;

			if (!ValidateAccountForSidType(acct))
				return;

			if (flagExecutorIsServiceAccount)
			{
				if (!v2 && acct != "SYSTEM")
				{
					MessageBox.Show(this, EditorProperties.Resources.Error_NoGroupsUnderV1, EditorProperties.Resources.TaskSchedulerName, MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}
				flagExecutorIsGroup = false;
				if (v2)
					td.Principal.GroupId = null;
				td.Principal.UserId = acct;
				td.Principal.LogonType = TaskLogonType.ServiceAccount;
				//this.flagExecutorIsCurrentUser = false;
			}
			else if (flagExecutorIsGroup)
			{
				if (!v2)
				{
					MessageBox.Show(this, EditorProperties.Resources.Error_NoGroupsUnderV1, EditorProperties.Resources.TaskSchedulerName, MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}
				td.Principal.GroupId = acct;
				td.Principal.UserId = null;
				td.Principal.LogonType = TaskLogonType.Group;
				//this.flagExecutorIsCurrentUser = false;
			}
			else
			{
				if (v2)
					td.Principal.GroupId = null;
				td.Principal.UserId = acct;
				//this.flagExecutorIsCurrentUser = this.UserIsExecutor(objArray[0].ObjectName);
				if (td.Principal.LogonType == TaskLogonType.Group)
				{
					td.Principal.LogonType = TaskLogonType.InteractiveToken;
				}
				else if (td.Principal.LogonType == TaskLogonType.ServiceAccount)
				{
					td.Principal.LogonType = TaskLogonType.InteractiveTokenOrPassword;
				}
			}
			SetUserControls(td.Principal.LogonType);
		}

		private void changePrincipalButton_Click(object sender, EventArgs e)
		{
			InvokeObjectPicker(parent.TaskService.TargetServer);
		}

		private void principalSIDTypeCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
			{
				if (!ValidateAccountForSidType(td.Principal.ToString()))
				{
					principalSIDTypeCombo.SelectedIndex = principalSIDTypeCombo.Items.IndexOf((long)TaskProcessTokenSidType.Default);
					return;
				}
				td.Principal.ProcessTokenSidType = (TaskProcessTokenSidType)principalSIDTypeCombo.SelectedIndex;
			}
			principalReqPrivilegesDropDown.Enabled = principalReqPrivilegesLabel.Enabled = parent.Editable && (principalSIDTypeCombo.SelectedIndex == (int)TaskProcessTokenSidType.Unrestricted);
		}

		private void principalReqPrivilegesDropDown_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
			{
				// TODO: Find a way to clear this list
				foreach (var item in principalReqPrivilegesDropDown.SelectedItems)
					td.Principal.RequiredPrivileges.Add((TaskPrincipalPrivilege)((long)item.Value));
			}
		}

		private void SetUserControls(TaskLogonType logonType)
		{
			var editable = parent.Editable;
			var prevOnAssignment = onAssignment;
			onAssignment = true;
			switch (logonType)
			{
				case TaskLogonType.InteractiveToken:
					flagRunOnlyWhenUserIsLoggedOn = true;
					flagExecutorIsServiceAccount = false;
					flagExecutorIsGroup = false;
					break;
				case TaskLogonType.Group:
					flagRunOnlyWhenUserIsLoggedOn = true;
					flagExecutorIsServiceAccount = false;
					flagExecutorIsGroup = true;
					break;
				case TaskLogonType.ServiceAccount:
					flagRunOnlyWhenUserIsLoggedOn = false;
					flagExecutorIsServiceAccount = true;
					flagExecutorIsGroup = false;
					break;
				default:
					flagRunOnlyWhenUserIsLoggedOn = false;
					flagExecutorIsServiceAccount = false;
					flagExecutorIsGroup = false;
					break;
			}

			if (flagExecutorIsServiceAccount)
			{
				taskLoggedOnRadio.Enabled = false;
				taskLoggedOptionalRadio.Enabled = false;
				taskLocalOnlyCheck.Enabled = false;
			}
			else if (flagExecutorIsGroup)
			{
				taskLoggedOnRadio.Enabled = editable;
				taskLoggedOptionalRadio.Enabled = false;
				taskLocalOnlyCheck.Enabled = false;
			}
			else if (flagRunOnlyWhenUserIsLoggedOn)
			{
				taskLoggedOnRadio.Enabled = editable;
				taskLoggedOptionalRadio.Enabled = editable;
				taskLocalOnlyCheck.Enabled = false;
			}
			else
			{
				taskLoggedOnRadio.Enabled = editable;
				taskLoggedOptionalRadio.Enabled = editable;
				taskLocalOnlyCheck.Enabled = editable && parent.IsV2;
			}

			taskLoggedOnRadio.Checked = flagRunOnlyWhenUserIsLoggedOn;
			taskLoggedOptionalRadio.Checked = !flagRunOnlyWhenUserIsLoggedOn;
			taskLocalOnlyCheck.Checked = !flagRunOnlyWhenUserIsLoggedOn && logonType == TaskLogonType.S4U;

			var user = td?.Principal.ToString();
			if (string.IsNullOrEmpty(user))
				user = WindowsIdentity.GetCurrent().Name;
			taskPrincipalText.Text = user;
			changePrincipalButton.Text = flagUserIsAnAdmin ? EditorProperties.Resources.ChangeUserBtn : EditorProperties.Resources.ChangeUserBtnNonAdmin;
			onAssignment = prevOnAssignment;
		}

		private void taskLocalOnlyCheck_CheckedChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
				td.Principal.LogonType = parent.IsV2 ? ((taskLocalOnlyCheck.Checked) ? TaskLogonType.S4U : TaskLogonType.Password) : TaskLogonType.InteractiveTokenOrPassword;
		}

		private void taskLoggedOnRadio_CheckedChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
				td.Principal.LogonType = TaskLogonType.InteractiveToken;
		}

		private void taskLoggedOptionalRadio_CheckedChanged(object sender, EventArgs e)
		{
			taskLocalOnlyCheck.Enabled = parent.Editable && parent.IsV2 && taskLoggedOptionalRadio.Checked && !(flagExecutorIsGroup | flagExecutorIsServiceAccount);
			taskLocalOnlyCheck_CheckedChanged(sender, e);
		}

		private void taskRegSDDLText_Validated(object sender, EventArgs e)
		{
			if (!onAssignment)
				td.RegistrationInfo.SecurityDescriptorSddlForm = taskRegSDDLText.TextLength > 0 ? taskRegSDDLText.Text : null;
		}

		private void taskRegSDDLBtn_Click(object sender, EventArgs e)
		{
			if (parent.Task != null)
				secEd.Initialize(parent.Task);
			else
			{
				var tsec = TaskSecurity.DefaultTaskSecurity;
				if (taskRegSDDLText.TextLength > 0) { tsec = new TaskSecurity(); tsec.SetSecurityDescriptorSddlForm(taskRegSDDLText.Text); }
				secEd.Initialize(parent.TaskName, false, parent.TaskService.TargetServer, tsec);
			}
			if (secEd.ShowDialog(this) == DialogResult.OK)
			{
				td.RegistrationInfo.SecurityDescriptorSddlForm = secEd.SecurityDescriptorSddlForm;
				taskRegSDDLText.Text = td.RegistrationInfo.SecurityDescriptorSddlForm;
			}
		}

		private void taskRunLevelCheck_CheckedChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
				td.Principal.RunLevel = taskRunLevelCheck.Checked ? TaskRunLevel.Highest : TaskRunLevel.LUA;
		}

		private bool ValidateAccountForSidType(string user)
		{
			if (!TaskPrincipal.ValidateAccountForSidType(user, td.Principal.ProcessTokenSidType))
			{
				MessageBox.Show(this, EditorProperties.Resources.Error_PrincipalSidTypeInvalid, EditorProperties.Resources.TaskSchedulerName, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			return true;
		}
	}
}
