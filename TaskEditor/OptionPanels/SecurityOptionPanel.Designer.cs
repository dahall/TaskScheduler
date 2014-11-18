namespace Microsoft.Win32.TaskScheduler.OptionPanels
{
	partial class SecurityOptionPanel
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SecurityOptionPanel));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
			this.taskRegSDDLBtn = new System.Windows.Forms.Button();
			this.taskRegSDDLText = new System.Windows.Forms.TextBox();
			this.optionPanelHeaderLabel2 = new Microsoft.Win32.TaskScheduler.OptionPanels.OptionPanelHeaderLabel();
			this.optionPanelHeaderLabel1 = new Microsoft.Win32.TaskScheduler.OptionPanels.OptionPanelHeaderLabel();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.taskLocalOnlyCheck = new System.Windows.Forms.CheckBox();
			this.taskLoggedOptionalRadio = new System.Windows.Forms.RadioButton();
			this.taskLoggedOnRadio = new System.Windows.Forms.RadioButton();
			this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
			this.changePrincipalButton = new System.Windows.Forms.Button();
			this.taskPrincipalText = new System.Windows.Forms.TextBox();
			this.taskUserAcctLabel = new System.Windows.Forms.Label();
			this.taskRunLevelCheck = new System.Windows.Forms.CheckBox();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.principalSIDTypeCombo = new System.Windows.Forms.ComboBox();
			this.principalSIDTypeLabel = new System.Windows.Forms.Label();
			this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
			this.principalReqPrivilegesDropDown = new Microsoft.Win32.TaskScheduler.DropDownCheckList();
			this.principalReqPrivilegesLabel = new System.Windows.Forms.Label();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel4.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.tableLayoutPanel5.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.tableLayoutPanel6.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.optionPanelHeaderLabel2, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.optionPanelHeaderLabel1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			// 
			// tableLayoutPanel4
			// 
			resources.ApplyResources(this.tableLayoutPanel4, "tableLayoutPanel4");
			this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel4, 2);
			this.tableLayoutPanel4.Controls.Add(this.taskRegSDDLBtn, 1, 0);
			this.tableLayoutPanel4.Controls.Add(this.taskRegSDDLText, 0, 0);
			this.tableLayoutPanel4.Name = "tableLayoutPanel4";
			// 
			// taskRegSDDLBtn
			// 
			resources.ApplyResources(this.taskRegSDDLBtn, "taskRegSDDLBtn");
			this.taskRegSDDLBtn.Name = "taskRegSDDLBtn";
			this.taskRegSDDLBtn.UseVisualStyleBackColor = true;
			this.taskRegSDDLBtn.Click += new System.EventHandler(this.taskRegSDDLBtn_Click);
			// 
			// taskRegSDDLText
			// 
			resources.ApplyResources(this.taskRegSDDLText, "taskRegSDDLText");
			this.taskRegSDDLText.Name = "taskRegSDDLText";
			this.taskRegSDDLText.Validated += new System.EventHandler(this.taskRegSDDLText_Validated);
			// 
			// optionPanelHeaderLabel2
			// 
			resources.ApplyResources(this.optionPanelHeaderLabel2, "optionPanelHeaderLabel2");
			this.optionPanelHeaderLabel2.Name = "optionPanelHeaderLabel2";
			// 
			// optionPanelHeaderLabel1
			// 
			resources.ApplyResources(this.optionPanelHeaderLabel1, "optionPanelHeaderLabel1");
			this.optionPanelHeaderLabel1.Name = "optionPanelHeaderLabel1";
			// 
			// tableLayoutPanel2
			// 
			resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
			this.tableLayoutPanel2.Controls.Add(this.taskLocalOnlyCheck, 0, 3);
			this.tableLayoutPanel2.Controls.Add(this.taskLoggedOptionalRadio, 0, 2);
			this.tableLayoutPanel2.Controls.Add(this.taskLoggedOnRadio, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel5, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.taskRunLevelCheck, 0, 4);
			this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 5);
			this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel6, 0, 6);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			// 
			// taskLocalOnlyCheck
			// 
			resources.ApplyResources(this.taskLocalOnlyCheck, "taskLocalOnlyCheck");
			this.taskLocalOnlyCheck.Name = "taskLocalOnlyCheck";
			this.taskLocalOnlyCheck.UseVisualStyleBackColor = true;
			this.taskLocalOnlyCheck.CheckedChanged += new System.EventHandler(this.taskLocalOnlyCheck_CheckedChanged);
			// 
			// taskLoggedOptionalRadio
			// 
			resources.ApplyResources(this.taskLoggedOptionalRadio, "taskLoggedOptionalRadio");
			this.taskLoggedOptionalRadio.Name = "taskLoggedOptionalRadio";
			this.taskLoggedOptionalRadio.TabStop = true;
			this.taskLoggedOptionalRadio.UseVisualStyleBackColor = true;
			this.taskLoggedOptionalRadio.CheckedChanged += new System.EventHandler(this.taskLoggedOptionalRadio_CheckedChanged);
			// 
			// taskLoggedOnRadio
			// 
			resources.ApplyResources(this.taskLoggedOnRadio, "taskLoggedOnRadio");
			this.taskLoggedOnRadio.Name = "taskLoggedOnRadio";
			this.taskLoggedOnRadio.TabStop = true;
			this.taskLoggedOnRadio.UseVisualStyleBackColor = true;
			this.taskLoggedOnRadio.CheckedChanged += new System.EventHandler(this.taskLoggedOnRadio_CheckedChanged);
			// 
			// tableLayoutPanel5
			// 
			resources.ApplyResources(this.tableLayoutPanel5, "tableLayoutPanel5");
			this.tableLayoutPanel5.Controls.Add(this.changePrincipalButton, 1, 1);
			this.tableLayoutPanel5.Controls.Add(this.taskPrincipalText, 0, 1);
			this.tableLayoutPanel5.Controls.Add(this.taskUserAcctLabel, 0, 0);
			this.tableLayoutPanel5.Name = "tableLayoutPanel5";
			// 
			// changePrincipalButton
			// 
			resources.ApplyResources(this.changePrincipalButton, "changePrincipalButton");
			this.changePrincipalButton.Name = "changePrincipalButton";
			this.changePrincipalButton.UseVisualStyleBackColor = true;
			this.changePrincipalButton.Click += new System.EventHandler(this.changePrincipalButton_Click);
			// 
			// taskPrincipalText
			// 
			resources.ApplyResources(this.taskPrincipalText, "taskPrincipalText");
			this.taskPrincipalText.Name = "taskPrincipalText";
			this.taskPrincipalText.ReadOnly = true;
			this.taskPrincipalText.TabStop = false;
			// 
			// taskUserAcctLabel
			// 
			resources.ApplyResources(this.taskUserAcctLabel, "taskUserAcctLabel");
			this.tableLayoutPanel5.SetColumnSpan(this.taskUserAcctLabel, 2);
			this.taskUserAcctLabel.Name = "taskUserAcctLabel";
			// 
			// taskRunLevelCheck
			// 
			resources.ApplyResources(this.taskRunLevelCheck, "taskRunLevelCheck");
			this.taskRunLevelCheck.Name = "taskRunLevelCheck";
			this.taskRunLevelCheck.UseVisualStyleBackColor = true;
			this.taskRunLevelCheck.CheckedChanged += new System.EventHandler(this.taskRunLevelCheck_CheckedChanged);
			// 
			// tableLayoutPanel3
			// 
			resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
			this.tableLayoutPanel3.Controls.Add(this.principalSIDTypeCombo, 1, 0);
			this.tableLayoutPanel3.Controls.Add(this.principalSIDTypeLabel, 0, 0);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			// 
			// principalSIDTypeCombo
			// 
			this.principalSIDTypeCombo.DisplayMember = "Text";
			this.principalSIDTypeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			resources.ApplyResources(this.principalSIDTypeCombo, "principalSIDTypeCombo");
			this.principalSIDTypeCombo.Name = "principalSIDTypeCombo";
			this.principalSIDTypeCombo.ValueMember = "Value";
			this.principalSIDTypeCombo.SelectedIndexChanged += new System.EventHandler(this.principalSIDTypeCombo_SelectedIndexChanged);
			// 
			// principalSIDTypeLabel
			// 
			resources.ApplyResources(this.principalSIDTypeLabel, "principalSIDTypeLabel");
			this.principalSIDTypeLabel.Name = "principalSIDTypeLabel";
			// 
			// tableLayoutPanel6
			// 
			resources.ApplyResources(this.tableLayoutPanel6, "tableLayoutPanel6");
			this.tableLayoutPanel6.Controls.Add(this.principalReqPrivilegesDropDown, 1, 0);
			this.tableLayoutPanel6.Controls.Add(this.principalReqPrivilegesLabel, 0, 0);
			this.tableLayoutPanel6.Name = "tableLayoutPanel6";
			// 
			// principalReqPrivilegesDropDown
			// 
			this.principalReqPrivilegesDropDown.BackColor = System.Drawing.Color.White;
			this.principalReqPrivilegesDropDown.ControlSize = new System.Drawing.Size(187, 105);
			this.principalReqPrivilegesDropDown.DropSize = new System.Drawing.Size(121, 106);
			resources.ApplyResources(this.principalReqPrivilegesDropDown, "principalReqPrivilegesDropDown");
			this.principalReqPrivilegesDropDown.Name = "principalReqPrivilegesDropDown";
			this.principalReqPrivilegesDropDown.SelectedIndexChanged += new System.EventHandler(this.principalReqPrivilegesDropDown_SelectedIndexChanged);
			// 
			// principalReqPrivilegesLabel
			// 
			resources.ApplyResources(this.principalReqPrivilegesLabel, "principalReqPrivilegesLabel");
			this.principalReqPrivilegesLabel.Name = "principalReqPrivilegesLabel";
			// 
			// SecurityOptionPanel
			// 
			resources.ApplyResources(this, "$this");
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "SecurityOptionPanel";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tableLayoutPanel4.ResumeLayout(false);
			this.tableLayoutPanel4.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.tableLayoutPanel5.ResumeLayout(false);
			this.tableLayoutPanel5.PerformLayout();
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			this.tableLayoutPanel6.ResumeLayout(false);
			this.tableLayoutPanel6.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private OptionPanelHeaderLabel optionPanelHeaderLabel1;
		private OptionPanelHeaderLabel optionPanelHeaderLabel2;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.CheckBox taskRunLevelCheck;
		private System.Windows.Forms.CheckBox taskLocalOnlyCheck;
		private System.Windows.Forms.RadioButton taskLoggedOptionalRadio;
		private System.Windows.Forms.RadioButton taskLoggedOnRadio;
		private System.Windows.Forms.TextBox taskPrincipalText;
		private System.Windows.Forms.Button changePrincipalButton;
		private System.Windows.Forms.Label taskUserAcctLabel;
		private System.Windows.Forms.Label principalSIDTypeLabel;
		private System.Windows.Forms.ComboBox principalSIDTypeCombo;
		private System.Windows.Forms.Label principalReqPrivilegesLabel;
		private DropDownCheckList principalReqPrivilegesDropDown;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
		private System.Windows.Forms.Button taskRegSDDLBtn;
		private System.Windows.Forms.TextBox taskRegSDDLText;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
	}
}
