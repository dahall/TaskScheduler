namespace Microsoft.Win32.TaskScheduler
{
	public partial class ActionEditDialog
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActionEditDialog));
			this.promptLabel = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.actionsCombo = new System.Windows.Forms.ComboBox();
			this.settingsGroup = new System.Windows.Forms.GroupBox();
			this.settingsTabs = new System.Windows.Forms.TabControl();
			this.execTab = new System.Windows.Forms.TabPage();
			this.execActionUI1 = new Microsoft.Win32.TaskScheduler.UIComponents.ExecActionUI();
			this.emailTab = new System.Windows.Forms.TabPage();
			this.emailActionUI1 = new Microsoft.Win32.TaskScheduler.UIComponents.EmailActionUI();
			this.messageTab = new System.Windows.Forms.TabPage();
			this.showMessageActionUI1 = new Microsoft.Win32.TaskScheduler.UIComponents.ShowMessageActionUI();
			this.comTab = new System.Windows.Forms.TabPage();
			this.comHandlerActionUI1 = new Microsoft.Win32.TaskScheduler.UIComponents.ComHandlerActionUI();
			this.okBtn = new System.Windows.Forms.Button();
			this.cancelBtn = new System.Windows.Forms.Button();
			this.actionsLabel = new System.Windows.Forms.Label();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.settingsGroup.SuspendLayout();
			this.settingsTabs.SuspendLayout();
			this.execTab.SuspendLayout();
			this.emailTab.SuspendLayout();
			this.messageTab.SuspendLayout();
			this.comTab.SuspendLayout();
			this.SuspendLayout();
			// 
			// promptLabel
			// 
			resources.ApplyResources(this.promptLabel, "promptLabel");
			this.promptLabel.Name = "promptLabel";
			this.toolTip.SetToolTip(this.promptLabel, resources.GetString("promptLabel.ToolTip"));
			// 
			// groupBox1
			// 
			resources.ApplyResources(this.groupBox1, "groupBox1");
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.TabStop = false;
			this.toolTip.SetToolTip(this.groupBox1, resources.GetString("groupBox1.ToolTip"));
			// 
			// actionsCombo
			// 
			resources.ApplyResources(this.actionsCombo, "actionsCombo");
			this.actionsCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.actionsCombo.FormattingEnabled = true;
			this.actionsCombo.Items.AddRange(new object[] {
            resources.GetString("actionsCombo.Items"),
            resources.GetString("actionsCombo.Items1"),
            resources.GetString("actionsCombo.Items2"),
            resources.GetString("actionsCombo.Items3")});
			this.actionsCombo.Name = "actionsCombo";
			this.toolTip.SetToolTip(this.actionsCombo, resources.GetString("actionsCombo.ToolTip"));
			this.actionsCombo.SelectedIndexChanged += new System.EventHandler(this.actionsCombo_SelectedIndexChanged);
			// 
			// settingsGroup
			// 
			resources.ApplyResources(this.settingsGroup, "settingsGroup");
			this.settingsGroup.Controls.Add(this.settingsTabs);
			this.settingsGroup.Name = "settingsGroup";
			this.settingsGroup.TabStop = false;
			this.toolTip.SetToolTip(this.settingsGroup, resources.GetString("settingsGroup.ToolTip"));
			// 
			// settingsTabs
			// 
			resources.ApplyResources(this.settingsTabs, "settingsTabs");
			this.settingsTabs.Controls.Add(this.execTab);
			this.settingsTabs.Controls.Add(this.emailTab);
			this.settingsTabs.Controls.Add(this.messageTab);
			this.settingsTabs.Controls.Add(this.comTab);
			this.settingsTabs.Name = "settingsTabs";
			this.settingsTabs.SelectedIndex = 0;
			this.settingsTabs.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.settingsTabs.TabStop = false;
			this.toolTip.SetToolTip(this.settingsTabs, resources.GetString("settingsTabs.ToolTip"));
			// 
			// execTab
			// 
			resources.ApplyResources(this.execTab, "execTab");
			this.execTab.Controls.Add(this.execActionUI1);
			this.execTab.Name = "execTab";
			this.toolTip.SetToolTip(this.execTab, resources.GetString("execTab.ToolTip"));
			this.execTab.UseVisualStyleBackColor = true;
			// 
			// execActionUI1
			// 
			resources.ApplyResources(this.execActionUI1, "execActionUI1");
			this.execActionUI1.Name = "execActionUI1";
			this.toolTip.SetToolTip(this.execActionUI1, resources.GetString("execActionUI1.ToolTip"));
			this.execActionUI1.KeyValueChanged += new System.EventHandler(this.keyField_TextChanged);
			// 
			// emailTab
			// 
			resources.ApplyResources(this.emailTab, "emailTab");
			this.emailTab.Controls.Add(this.emailActionUI1);
			this.emailTab.Name = "emailTab";
			this.toolTip.SetToolTip(this.emailTab, resources.GetString("emailTab.ToolTip"));
			this.emailTab.UseVisualStyleBackColor = true;
			// 
			// emailActionUI1
			// 
			resources.ApplyResources(this.emailActionUI1, "emailActionUI1");
			this.emailActionUI1.Name = "emailActionUI1";
			this.toolTip.SetToolTip(this.emailActionUI1, resources.GetString("emailActionUI1.ToolTip"));
			this.emailActionUI1.KeyValueChanged += new System.EventHandler(this.keyField_TextChanged);
			// 
			// messageTab
			// 
			resources.ApplyResources(this.messageTab, "messageTab");
			this.messageTab.Controls.Add(this.showMessageActionUI1);
			this.messageTab.Name = "messageTab";
			this.toolTip.SetToolTip(this.messageTab, resources.GetString("messageTab.ToolTip"));
			this.messageTab.UseVisualStyleBackColor = true;
			// 
			// showMessageActionUI1
			// 
			resources.ApplyResources(this.showMessageActionUI1, "showMessageActionUI1");
			this.showMessageActionUI1.Name = "showMessageActionUI1";
			this.toolTip.SetToolTip(this.showMessageActionUI1, resources.GetString("showMessageActionUI1.ToolTip"));
			this.showMessageActionUI1.KeyValueChanged += new System.EventHandler(this.keyField_TextChanged);
			// 
			// comTab
			// 
			resources.ApplyResources(this.comTab, "comTab");
			this.comTab.Controls.Add(this.comHandlerActionUI1);
			this.comTab.Name = "comTab";
			this.toolTip.SetToolTip(this.comTab, resources.GetString("comTab.ToolTip"));
			this.comTab.UseVisualStyleBackColor = true;
			// 
			// comHandlerActionUI1
			// 
			resources.ApplyResources(this.comHandlerActionUI1, "comHandlerActionUI1");
			this.comHandlerActionUI1.Name = "comHandlerActionUI1";
			this.toolTip.SetToolTip(this.comHandlerActionUI1, resources.GetString("comHandlerActionUI1.ToolTip"));
			this.comHandlerActionUI1.KeyValueChanged += new System.EventHandler(this.keyField_TextChanged);
			// 
			// okBtn
			// 
			resources.ApplyResources(this.okBtn, "okBtn");
			this.okBtn.Name = "okBtn";
			this.toolTip.SetToolTip(this.okBtn, resources.GetString("okBtn.ToolTip"));
			this.okBtn.UseVisualStyleBackColor = true;
			this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
			// 
			// cancelBtn
			// 
			resources.ApplyResources(this.cancelBtn, "cancelBtn");
			this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelBtn.Name = "cancelBtn";
			this.toolTip.SetToolTip(this.cancelBtn, resources.GetString("cancelBtn.ToolTip"));
			this.cancelBtn.UseVisualStyleBackColor = true;
			this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
			// 
			// actionsLabel
			// 
			resources.ApplyResources(this.actionsLabel, "actionsLabel");
			this.actionsLabel.Name = "actionsLabel";
			this.toolTip.SetToolTip(this.actionsLabel, resources.GetString("actionsLabel.ToolTip"));
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			resources.ApplyResources(this.openFileDialog1, "openFileDialog1");
			// 
			// ActionEditDialog
			// 
			this.AcceptButton = this.okBtn;
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelBtn;
			this.Controls.Add(this.cancelBtn);
			this.Controls.Add(this.okBtn);
			this.Controls.Add(this.settingsGroup);
			this.Controls.Add(this.actionsCombo);
			this.Controls.Add(this.actionsLabel);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.promptLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ActionEditDialog";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.toolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
			this.settingsGroup.ResumeLayout(false);
			this.settingsTabs.ResumeLayout(false);
			this.execTab.ResumeLayout(false);
			this.emailTab.ResumeLayout(false);
			this.messageTab.ResumeLayout(false);
			this.comTab.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label promptLabel;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox actionsCombo;
		private System.Windows.Forms.GroupBox settingsGroup;
		private System.Windows.Forms.Button okBtn;
		private System.Windows.Forms.Button cancelBtn;
		private System.Windows.Forms.TabControl settingsTabs;
		private System.Windows.Forms.TabPage execTab;
		private System.Windows.Forms.TabPage emailTab;
		private System.Windows.Forms.TabPage messageTab;
		private System.Windows.Forms.Label actionsLabel;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.TabPage comTab;
		private System.Windows.Forms.ToolTip toolTip;
		private UIComponents.ExecActionUI execActionUI1;
		private UIComponents.ComHandlerActionUI comHandlerActionUI1;
		private UIComponents.EmailActionUI emailActionUI1;
		private UIComponents.ShowMessageActionUI showMessageActionUI1;
	}
}