namespace SecurityEditor
{
	partial class RuleEditor
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
			this.hdrTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.principalTextBox = new System.Windows.Forms.TextBox();
			this.selectPrincipalBtn = new System.Windows.Forms.LinkLabel();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.typeComboBox = new System.Windows.Forms.ComboBox();
			this.appliesToComboBox = new System.Windows.Forms.ComboBox();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.showAdvPermBtn = new System.Windows.Forms.LinkLabel();
			this.clearAllBtn = new System.Windows.Forms.Button();
			this.applySettingsIntCheckBox = new System.Windows.Forms.CheckBox();
			this.rightsCheckBoxList = new GroupControls.CheckBoxList();
			this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			this.basicPermLabel = new System.Windows.Forms.Label();
			this.advPermLabel = new System.Windows.Forms.Label();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.addCondBtn = new System.Windows.Forms.LinkLabel();
			this.label5 = new System.Windows.Forms.Label();
			this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
			this.cancelBtn = new System.Windows.Forms.Button();
			this.okBtn = new System.Windows.Forms.Button();
			this.bgPanel = new System.Windows.Forms.Panel();
			this.bgTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.conditionPanel = new System.Windows.Forms.Panel();
			this.helpProvider = new System.Windows.Forms.HelpProvider();
			this.hdrTableLayoutPanel.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.rightsCheckBoxList.SuspendLayout();
			this.flowLayoutPanel2.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.tableLayoutPanel4.SuspendLayout();
			this.bgPanel.SuspendLayout();
			this.bgTableLayoutPanel.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.conditionPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// hdrTableLayoutPanel
			// 
			this.hdrTableLayoutPanel.AutoSize = true;
			this.hdrTableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.hdrTableLayoutPanel.ColumnCount = 2;
			this.hdrTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.hdrTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.hdrTableLayoutPanel.Controls.Add(this.flowLayoutPanel1, 1, 0);
			this.hdrTableLayoutPanel.Controls.Add(this.label1, 0, 0);
			this.hdrTableLayoutPanel.Controls.Add(this.label2, 0, 1);
			this.hdrTableLayoutPanel.Controls.Add(this.label3, 0, 2);
			this.hdrTableLayoutPanel.Controls.Add(this.typeComboBox, 1, 1);
			this.hdrTableLayoutPanel.Controls.Add(this.appliesToComboBox, 1, 2);
			this.hdrTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.hdrTableLayoutPanel.Location = new System.Drawing.Point(8, 15);
			this.hdrTableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
			this.hdrTableLayoutPanel.Name = "hdrTableLayoutPanel";
			this.hdrTableLayoutPanel.RowCount = 3;
			this.hdrTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.hdrTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.hdrTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.hdrTableLayoutPanel.Size = new System.Drawing.Size(875, 110);
			this.hdrTableLayoutPanel.TabIndex = 0;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.AutoSize = true;
			this.flowLayoutPanel1.Controls.Add(this.principalTextBox);
			this.flowLayoutPanel1.Controls.Add(this.selectPrincipalBtn);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(56, 7);
			this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 7, 0, 7);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(819, 13);
			this.flowLayoutPanel1.TabIndex = 0;
			// 
			// principalTextBox
			// 
			this.principalTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.principalTextBox.Location = new System.Drawing.Point(0, 0);
			this.principalTextBox.Margin = new System.Windows.Forms.Padding(0);
			this.principalTextBox.Name = "principalTextBox";
			this.principalTextBox.ReadOnly = true;
			this.principalTextBox.Size = new System.Drawing.Size(194, 13);
			this.principalTextBox.TabIndex = 1;
			this.principalTextBox.Visible = false;
			// 
			// selectPrincipalBtn
			// 
			this.selectPrincipalBtn.AutoSize = true;
			this.selectPrincipalBtn.Location = new System.Drawing.Point(197, 0);
			this.selectPrincipalBtn.Name = "selectPrincipalBtn";
			this.selectPrincipalBtn.Size = new System.Drawing.Size(88, 13);
			this.selectPrincipalBtn.TabIndex = 0;
			this.selectPrincipalBtn.TabStop = true;
			this.selectPrincipalBtn.Text = "Select a principal";
			this.selectPrincipalBtn.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.selectPrincipalBtn_LinkClicked);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Margin = new System.Windows.Forms.Padding(0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(50, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Principal:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(0, 27);
			this.label2.Margin = new System.Windows.Forms.Padding(0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(34, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Type:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(0, 62);
			this.label3.Margin = new System.Windows.Forms.Padding(0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(56, 13);
			this.label3.TabIndex = 1;
			this.label3.Text = "Applies to:";
			// 
			// typeComboBox
			// 
			this.typeComboBox.DisplayMember = "Text";
			this.typeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.typeComboBox.Enabled = false;
			this.typeComboBox.FormattingEnabled = true;
			this.typeComboBox.Items.AddRange(new object[] {
            "All",
            "Fail",
            "Success"});
			this.typeComboBox.Location = new System.Drawing.Point(56, 34);
			this.typeComboBox.Margin = new System.Windows.Forms.Padding(0, 7, 0, 7);
			this.typeComboBox.Name = "typeComboBox";
			this.typeComboBox.Size = new System.Drawing.Size(270, 21);
			this.typeComboBox.TabIndex = 2;
			this.typeComboBox.ValueMember = "Value";
			this.typeComboBox.SelectedIndexChanged += new System.EventHandler(this.typeComboBox_SelectedIndexChanged);
			// 
			// appliesToComboBox
			// 
			this.appliesToComboBox.DisplayMember = "Text";
			this.appliesToComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.appliesToComboBox.Enabled = false;
			this.appliesToComboBox.FormattingEnabled = true;
			this.appliesToComboBox.Items.AddRange(new object[] {
            "This folder only",
            "This folder, subfolders and files",
            "This folder and subfolders",
            "This folder and files",
            "Subfolders and files only",
            "Subfolders only",
            "Files only"});
			this.appliesToComboBox.Location = new System.Drawing.Point(56, 69);
			this.appliesToComboBox.Margin = new System.Windows.Forms.Padding(0, 7, 0, 7);
			this.appliesToComboBox.Name = "appliesToComboBox";
			this.appliesToComboBox.Size = new System.Drawing.Size(270, 21);
			this.appliesToComboBox.TabIndex = 2;
			this.appliesToComboBox.ValueMember = "Value";
			this.appliesToComboBox.SelectedIndexChanged += new System.EventHandler(this.appliesToComboBox_SelectedIndexChanged);
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.AutoSize = true;
			this.tableLayoutPanel2.ColumnCount = 2;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.Controls.Add(this.showAdvPermBtn, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this.clearAllBtn, 1, 2);
			this.tableLayoutPanel2.Controls.Add(this.applySettingsIntCheckBox, 0, 2);
			this.tableLayoutPanel2.Controls.Add(this.rightsCheckBoxList, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel2, 0, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(8, 15);
			this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0, 7, 0, 0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 3;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.Size = new System.Drawing.Size(875, 205);
			this.tableLayoutPanel2.TabIndex = 0;
			// 
			// showAdvPermBtn
			// 
			this.showAdvPermBtn.AutoSize = true;
			this.showAdvPermBtn.Enabled = false;
			this.showAdvPermBtn.Location = new System.Drawing.Point(733, 0);
			this.showAdvPermBtn.Margin = new System.Windows.Forms.Padding(0);
			this.showAdvPermBtn.Name = "showAdvPermBtn";
			this.showAdvPermBtn.Size = new System.Drawing.Size(142, 13);
			this.showAdvPermBtn.TabIndex = 0;
			this.showAdvPermBtn.TabStop = true;
			this.showAdvPermBtn.Text = "Show advanced permissions";
			this.showAdvPermBtn.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.showAdvPermBtn_LinkClicked);
			// 
			// clearAllBtn
			// 
			this.clearAllBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.clearAllBtn.Enabled = false;
			this.clearAllBtn.Location = new System.Drawing.Point(800, 182);
			this.clearAllBtn.Margin = new System.Windows.Forms.Padding(0);
			this.clearAllBtn.Name = "clearAllBtn";
			this.clearAllBtn.Size = new System.Drawing.Size(75, 23);
			this.clearAllBtn.TabIndex = 2;
			this.clearAllBtn.Text = "Clear all";
			this.clearAllBtn.UseVisualStyleBackColor = true;
			this.clearAllBtn.Click += new System.EventHandler(this.clearAllBtn_Click);
			// 
			// applySettingsIntCheckBox
			// 
			this.applySettingsIntCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.applySettingsIntCheckBox.AutoSize = true;
			this.applySettingsIntCheckBox.Enabled = false;
			this.applySettingsIntCheckBox.Location = new System.Drawing.Point(0, 182);
			this.applySettingsIntCheckBox.Margin = new System.Windows.Forms.Padding(0);
			this.applySettingsIntCheckBox.Name = "applySettingsIntCheckBox";
			this.applySettingsIntCheckBox.Size = new System.Drawing.Size(375, 23);
			this.applySettingsIntCheckBox.TabIndex = 3;
			this.applySettingsIntCheckBox.Text = "Only apply these settings to objects and/or containers within this container";
			this.applySettingsIntCheckBox.UseVisualStyleBackColor = true;
			this.applySettingsIntCheckBox.CheckedChanged += new System.EventHandler(this.applySettingsIntCheckBox_CheckedChanged);
			// 
			// rightsCheckBoxList
			// 
			this.rightsCheckBoxList.AutoSize = false;
			this.tableLayoutPanel2.SetColumnSpan(this.rightsCheckBoxList, 2);
			this.rightsCheckBoxList.Dock = System.Windows.Forms.DockStyle.Top;
			this.rightsCheckBoxList.Enabled = false;
			this.rightsCheckBoxList.Location = new System.Drawing.Point(60, 16);
			this.rightsCheckBoxList.Margin = new System.Windows.Forms.Padding(60, 3, 3, 3);
			this.rightsCheckBoxList.Name = "rightsCheckBoxList";
			this.rightsCheckBoxList.Size = new System.Drawing.Size(812, 163);
			this.rightsCheckBoxList.TabIndex = 4;
			this.rightsCheckBoxList.Text = "rightsCheckBoxList";
			// 
			// flowLayoutPanel2
			// 
			this.flowLayoutPanel2.AutoSize = true;
			this.flowLayoutPanel2.Controls.Add(this.basicPermLabel);
			this.flowLayoutPanel2.Controls.Add(this.advPermLabel);
			this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
			this.flowLayoutPanel2.Name = "flowLayoutPanel2";
			this.flowLayoutPanel2.Size = new System.Drawing.Size(733, 13);
			this.flowLayoutPanel2.TabIndex = 5;
			// 
			// basicPermLabel
			// 
			this.basicPermLabel.AutoSize = true;
			this.basicPermLabel.Location = new System.Drawing.Point(0, 0);
			this.basicPermLabel.Margin = new System.Windows.Forms.Padding(0);
			this.basicPermLabel.Name = "basicPermLabel";
			this.basicPermLabel.Size = new System.Drawing.Size(93, 13);
			this.basicPermLabel.TabIndex = 1;
			this.basicPermLabel.Text = "Basic permissions:";
			// 
			// advPermLabel
			// 
			this.advPermLabel.AutoSize = true;
			this.advPermLabel.Location = new System.Drawing.Point(93, 0);
			this.advPermLabel.Margin = new System.Windows.Forms.Padding(0);
			this.advPermLabel.Name = "advPermLabel";
			this.advPermLabel.Size = new System.Drawing.Size(116, 13);
			this.advPermLabel.TabIndex = 1;
			this.advPermLabel.Text = "Advanced permissions:";
			this.advPermLabel.Visible = false;
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.AutoSize = true;
			this.tableLayoutPanel3.ColumnCount = 1;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.Controls.Add(this.addCondBtn, 0, 1);
			this.tableLayoutPanel3.Controls.Add(this.label5, 0, 0);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(8, 15);
			this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0, 7, 0, 0);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 2;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.Size = new System.Drawing.Size(875, 87);
			this.tableLayoutPanel3.TabIndex = 0;
			// 
			// addCondBtn
			// 
			this.addCondBtn.AutoSize = true;
			this.addCondBtn.Enabled = false;
			this.addCondBtn.Location = new System.Drawing.Point(0, 13);
			this.addCondBtn.Margin = new System.Windows.Forms.Padding(0);
			this.addCondBtn.Name = "addCondBtn";
			this.addCondBtn.Size = new System.Drawing.Size(81, 13);
			this.addCondBtn.TabIndex = 0;
			this.addCondBtn.TabStop = true;
			this.addCondBtn.Text = "Add a condition";
			this.addCondBtn.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.addCondBtn_LinkClicked);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(0, 0);
			this.label5.Margin = new System.Windows.Forms.Padding(0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(528, 13);
			this.label5.TabIndex = 1;
			this.label5.Text = "Add a condition to limit the scope of this auditing entry. Security events will b" +
    "e logged only if conditions are met.";
			// 
			// tableLayoutPanel4
			// 
			this.tableLayoutPanel4.AutoSize = true;
			this.tableLayoutPanel4.ColumnCount = 2;
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel4.Controls.Add(this.cancelBtn, 1, 0);
			this.tableLayoutPanel4.Controls.Add(this.okBtn, 0, 0);
			this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.tableLayoutPanel4.Location = new System.Drawing.Point(10, 524);
			this.tableLayoutPanel4.Name = "tableLayoutPanel4";
			this.tableLayoutPanel4.RowCount = 1;
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel4.Size = new System.Drawing.Size(893, 30);
			this.tableLayoutPanel4.TabIndex = 3;
			// 
			// cancelBtn
			// 
			this.cancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelBtn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.cancelBtn.Location = new System.Drawing.Point(818, 7);
			this.cancelBtn.Margin = new System.Windows.Forms.Padding(6, 7, 0, 0);
			this.cancelBtn.Name = "cancelBtn";
			this.cancelBtn.Size = new System.Drawing.Size(75, 23);
			this.cancelBtn.TabIndex = 1;
			this.cancelBtn.Text = "Cancel";
			this.cancelBtn.UseVisualStyleBackColor = true;
			// 
			// okBtn
			// 
			this.okBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.okBtn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.okBtn.Location = new System.Drawing.Point(737, 7);
			this.okBtn.Margin = new System.Windows.Forms.Padding(6, 7, 0, 0);
			this.okBtn.Name = "okBtn";
			this.okBtn.Size = new System.Drawing.Size(75, 23);
			this.okBtn.TabIndex = 0;
			this.okBtn.Text = "OK";
			this.okBtn.UseVisualStyleBackColor = true;
			// 
			// bgPanel
			// 
			this.bgPanel.AutoScroll = true;
			this.bgPanel.Controls.Add(this.bgTableLayoutPanel);
			this.bgPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bgPanel.Location = new System.Drawing.Point(10, 10);
			this.bgPanel.Name = "bgPanel";
			this.bgPanel.Size = new System.Drawing.Size(893, 514);
			this.bgPanel.TabIndex = 4;
			// 
			// bgTableLayoutPanel
			// 
			this.bgTableLayoutPanel.AutoScroll = true;
			this.bgTableLayoutPanel.AutoSize = true;
			this.bgTableLayoutPanel.ColumnCount = 1;
			this.bgTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.bgTableLayoutPanel.Controls.Add(this.panel1, 0, 0);
			this.bgTableLayoutPanel.Controls.Add(this.panel2, 0, 1);
			this.bgTableLayoutPanel.Controls.Add(this.conditionPanel, 0, 2);
			this.bgTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bgTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.bgTableLayoutPanel.Name = "bgTableLayoutPanel";
			this.bgTableLayoutPanel.RowCount = 3;
			this.bgTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.bgTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.bgTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.bgTableLayoutPanel.Size = new System.Drawing.Size(893, 514);
			this.bgTableLayoutPanel.TabIndex = 2;
			// 
			// panel1
			// 
			this.panel1.AutoSize = true;
			this.panel1.BackColor = System.Drawing.SystemColors.Window;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.hdrTableLayoutPanel);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Margin = new System.Windows.Forms.Padding(0);
			this.panel1.Name = "panel1";
			this.panel1.Padding = new System.Windows.Forms.Padding(8, 15, 8, 15);
			this.panel1.Size = new System.Drawing.Size(893, 142);
			this.panel1.TabIndex = 1;
			// 
			// panel2
			// 
			this.panel2.AutoSize = true;
			this.panel2.BackColor = System.Drawing.SystemColors.Window;
			this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel2.Controls.Add(this.tableLayoutPanel2);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(0, 150);
			this.panel2.Margin = new System.Windows.Forms.Padding(0, 8, 0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Padding = new System.Windows.Forms.Padding(8, 15, 8, 15);
			this.panel2.Size = new System.Drawing.Size(893, 237);
			this.panel2.TabIndex = 1;
			// 
			// conditionPanel
			// 
			this.conditionPanel.AutoSize = true;
			this.conditionPanel.BackColor = System.Drawing.SystemColors.Window;
			this.conditionPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.conditionPanel.Controls.Add(this.tableLayoutPanel3);
			this.conditionPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.conditionPanel.Location = new System.Drawing.Point(0, 395);
			this.conditionPanel.Margin = new System.Windows.Forms.Padding(0, 8, 0, 0);
			this.conditionPanel.MinimumSize = new System.Drawing.Size(2, 110);
			this.conditionPanel.Name = "conditionPanel";
			this.conditionPanel.Padding = new System.Windows.Forms.Padding(8, 15, 8, 15);
			this.conditionPanel.Size = new System.Drawing.Size(893, 119);
			this.conditionPanel.TabIndex = 1;
			this.conditionPanel.Visible = false;
			// 
			// RuleEditor
			// 
			this.AcceptButton = this.okBtn;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelBtn;
			this.ClientSize = new System.Drawing.Size(913, 564);
			this.Controls.Add(this.bgPanel);
			this.Controls.Add(this.tableLayoutPanel4);
			this.Name = "RuleEditor";
			this.Padding = new System.Windows.Forms.Padding(10);
			this.Text = "RuleEditor";
			this.hdrTableLayoutPanel.ResumeLayout(false);
			this.hdrTableLayoutPanel.PerformLayout();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.rightsCheckBoxList.ResumeLayout(true);
			this.flowLayoutPanel2.ResumeLayout(false);
			this.flowLayoutPanel2.PerformLayout();
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			this.tableLayoutPanel4.ResumeLayout(false);
			this.bgPanel.ResumeLayout(false);
			this.bgPanel.PerformLayout();
			this.bgTableLayoutPanel.ResumeLayout(false);
			this.bgTableLayoutPanel.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.conditionPanel.ResumeLayout(false);
			this.conditionPanel.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel hdrTableLayoutPanel;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.TextBox principalTextBox;
		private System.Windows.Forms.LinkLabel selectPrincipalBtn;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox typeComboBox;
		private System.Windows.Forms.ComboBox appliesToComboBox;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.LinkLabel showAdvPermBtn;
		private System.Windows.Forms.Button clearAllBtn;
		private System.Windows.Forms.CheckBox applySettingsIntCheckBox;
		private GroupControls.CheckBoxList rightsCheckBoxList;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
		private System.Windows.Forms.Label basicPermLabel;
		private System.Windows.Forms.Label advPermLabel;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.LinkLabel addCondBtn;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
		private System.Windows.Forms.Button cancelBtn;
		private System.Windows.Forms.Button okBtn;
		private System.Windows.Forms.Panel bgPanel;
		private System.Windows.Forms.TableLayoutPanel bgTableLayoutPanel;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel conditionPanel;
		private System.Windows.Forms.HelpProvider helpProvider;
	}
}