namespace SecurityEditor
{
	partial class AdvancedSecuritySettingsDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdvancedSecuritySettingsDialog));
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.permEditor = new SecurityEditor.ACLEditor();
			this.permHeaderLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.audEditor = new SecurityEditor.ACLEditor();
			this.audHeaderLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.notEditableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.label14 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.audContinueBtn = new System.Windows.Forms.Button();
			this.label16 = new System.Windows.Forms.Label();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.comboBox2 = new System.Windows.Forms.ComboBox();
			this.button2 = new System.Windows.Forms.Button();
			this.viewEffAccBtn = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.listView1 = new System.Windows.Forms.ListView();
			this.effAccNotificationLabel = new System.Windows.Forms.Label();
			this.helpProvider1 = new System.Windows.Forms.HelpProvider();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.applyBtn = new System.Windows.Forms.Button();
			this.cancelBtn = new System.Windows.Forms.Button();
			this.okBtn = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.intLevelText = new System.Windows.Forms.TextBox();
			this.objNameText = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.ownerText = new System.Windows.Forms.TextBox();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.chgOwnerLink = new System.Windows.Forms.LinkLabel();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.permHeaderLayoutPanel.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.audHeaderLayoutPanel.SuspendLayout();
			this.notEditableLayoutPanel.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.tableLayoutPanel5.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			resources.ApplyResources(this.tabControl1, "tabControl1");
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.helpProvider1.SetShowHelp(this.tabControl1, ((bool)(resources.GetObject("tabControl1.ShowHelp"))));
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.permEditor);
			this.tabPage1.Controls.Add(this.permHeaderLayoutPanel);
			this.helpProvider1.SetHelpKeyword(this.tabPage1, resources.GetString("tabPage1.HelpKeyword"));
			this.helpProvider1.SetHelpNavigator(this.tabPage1, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("tabPage1.HelpNavigator"))));
			resources.ApplyResources(this.tabPage1, "tabPage1");
			this.tabPage1.Name = "tabPage1";
			this.helpProvider1.SetShowHelp(this.tabPage1, ((bool)(resources.GetObject("tabPage1.ShowHelp"))));
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// permEditor
			// 
			this.permEditor.Display = SecurityEditor.SecurityRuleType.Access;
			resources.ApplyResources(this.permEditor, "permEditor");
			this.permEditor.Name = "permEditor";
			this.permEditor.ObjectName = null;
			this.permEditor.ObjectSecurity = null;
			this.helpProvider1.SetShowHelp(this.permEditor, ((bool)(resources.GetObject("permEditor.ShowHelp"))));
			this.permEditor.TargetComputer = null;
			// 
			// permHeaderLayoutPanel
			// 
			resources.ApplyResources(this.permHeaderLayoutPanel, "permHeaderLayoutPanel");
			this.permHeaderLayoutPanel.Controls.Add(this.label4, 0, 0);
			this.permHeaderLayoutPanel.Controls.Add(this.label5, 0, 1);
			this.permHeaderLayoutPanel.Name = "permHeaderLayoutPanel";
			this.helpProvider1.SetShowHelp(this.permHeaderLayoutPanel, ((bool)(resources.GetObject("permHeaderLayoutPanel.ShowHelp"))));
			// 
			// label4
			// 
			resources.ApplyResources(this.label4, "label4");
			this.label4.Name = "label4";
			this.helpProvider1.SetShowHelp(this.label4, ((bool)(resources.GetObject("label4.ShowHelp"))));
			// 
			// label5
			// 
			resources.ApplyResources(this.label5, "label5");
			this.label5.Name = "label5";
			this.helpProvider1.SetShowHelp(this.label5, ((bool)(resources.GetObject("label5.ShowHelp"))));
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.audEditor);
			this.tabPage2.Controls.Add(this.audHeaderLayoutPanel);
			this.tabPage2.Controls.Add(this.notEditableLayoutPanel);
			this.helpProvider1.SetHelpKeyword(this.tabPage2, resources.GetString("tabPage2.HelpKeyword"));
			this.helpProvider1.SetHelpNavigator(this.tabPage2, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("tabPage2.HelpNavigator"))));
			resources.ApplyResources(this.tabPage2, "tabPage2");
			this.tabPage2.Name = "tabPage2";
			this.helpProvider1.SetShowHelp(this.tabPage2, ((bool)(resources.GetObject("tabPage2.ShowHelp"))));
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// audEditor
			// 
			this.audEditor.Display = SecurityEditor.SecurityRuleType.Audit;
			resources.ApplyResources(this.audEditor, "audEditor");
			this.audEditor.Name = "audEditor";
			this.audEditor.ObjectName = null;
			this.audEditor.ObjectSecurity = null;
			this.helpProvider1.SetShowHelp(this.audEditor, ((bool)(resources.GetObject("audEditor.ShowHelp"))));
			this.audEditor.TargetComputer = null;
			// 
			// audHeaderLayoutPanel
			// 
			resources.ApplyResources(this.audHeaderLayoutPanel, "audHeaderLayoutPanel");
			this.audHeaderLayoutPanel.Controls.Add(this.label6, 0, 0);
			this.audHeaderLayoutPanel.Controls.Add(this.label7, 0, 1);
			this.audHeaderLayoutPanel.Name = "audHeaderLayoutPanel";
			this.helpProvider1.SetShowHelp(this.audHeaderLayoutPanel, ((bool)(resources.GetObject("audHeaderLayoutPanel.ShowHelp"))));
			// 
			// label6
			// 
			resources.ApplyResources(this.label6, "label6");
			this.label6.Name = "label6";
			this.helpProvider1.SetShowHelp(this.label6, ((bool)(resources.GetObject("label6.ShowHelp"))));
			// 
			// label7
			// 
			resources.ApplyResources(this.label7, "label7");
			this.label7.Name = "label7";
			this.helpProvider1.SetShowHelp(this.label7, ((bool)(resources.GetObject("label7.ShowHelp"))));
			// 
			// notEditableLayoutPanel
			// 
			resources.ApplyResources(this.notEditableLayoutPanel, "notEditableLayoutPanel");
			this.notEditableLayoutPanel.Controls.Add(this.label14, 1, 0);
			this.notEditableLayoutPanel.Controls.Add(this.label15, 1, 1);
			this.notEditableLayoutPanel.Controls.Add(this.audContinueBtn, 1, 2);
			this.notEditableLayoutPanel.Controls.Add(this.label16, 0, 0);
			this.notEditableLayoutPanel.Name = "notEditableLayoutPanel";
			this.helpProvider1.SetShowHelp(this.notEditableLayoutPanel, ((bool)(resources.GetObject("notEditableLayoutPanel.ShowHelp"))));
			// 
			// label14
			// 
			resources.ApplyResources(this.label14, "label14");
			this.label14.Name = "label14";
			this.helpProvider1.SetShowHelp(this.label14, ((bool)(resources.GetObject("label14.ShowHelp"))));
			// 
			// label15
			// 
			resources.ApplyResources(this.label15, "label15");
			this.label15.Name = "label15";
			this.helpProvider1.SetShowHelp(this.label15, ((bool)(resources.GetObject("label15.ShowHelp"))));
			// 
			// audContinueBtn
			// 
			resources.ApplyResources(this.audContinueBtn, "audContinueBtn");
			this.audContinueBtn.Name = "audContinueBtn";
			this.helpProvider1.SetShowHelp(this.audContinueBtn, ((bool)(resources.GetObject("audContinueBtn.ShowHelp"))));
			this.audContinueBtn.UseVisualStyleBackColor = true;
			// 
			// label16
			// 
			resources.ApplyResources(this.label16, "label16");
			this.label16.Name = "label16";
			this.helpProvider1.SetShowHelp(this.label16, ((bool)(resources.GetObject("label16.ShowHelp"))));
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.tableLayoutPanel5);
			resources.ApplyResources(this.tabPage3, "tabPage3");
			this.tabPage3.Name = "tabPage3";
			this.helpProvider1.SetShowHelp(this.tabPage3, ((bool)(resources.GetObject("tabPage3.ShowHelp"))));
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel5
			// 
			resources.ApplyResources(this.tableLayoutPanel5, "tableLayoutPanel5");
			this.tableLayoutPanel5.Controls.Add(this.label8, 0, 1);
			this.tableLayoutPanel5.Controls.Add(this.label9, 0, 2);
			this.tableLayoutPanel5.Controls.Add(this.label10, 1, 3);
			this.tableLayoutPanel5.Controls.Add(this.label11, 0, 4);
			this.tableLayoutPanel5.Controls.Add(this.label12, 1, 5);
			this.tableLayoutPanel5.Controls.Add(this.button1, 3, 3);
			this.tableLayoutPanel5.Controls.Add(this.comboBox1, 2, 3);
			this.tableLayoutPanel5.Controls.Add(this.comboBox2, 2, 5);
			this.tableLayoutPanel5.Controls.Add(this.button2, 3, 5);
			this.tableLayoutPanel5.Controls.Add(this.viewEffAccBtn, 0, 7);
			this.tableLayoutPanel5.Controls.Add(this.groupBox1, 0, 6);
			this.tableLayoutPanel5.Controls.Add(this.listView1, 0, 8);
			this.tableLayoutPanel5.Controls.Add(this.effAccNotificationLabel, 0, 0);
			this.tableLayoutPanel5.Name = "tableLayoutPanel5";
			this.helpProvider1.SetShowHelp(this.tableLayoutPanel5, ((bool)(resources.GetObject("tableLayoutPanel5.ShowHelp"))));
			// 
			// label8
			// 
			resources.ApplyResources(this.label8, "label8");
			this.tableLayoutPanel5.SetColumnSpan(this.label8, 4);
			this.label8.Name = "label8";
			this.helpProvider1.SetShowHelp(this.label8, ((bool)(resources.GetObject("label8.ShowHelp"))));
			// 
			// label9
			// 
			resources.ApplyResources(this.label9, "label9");
			this.label9.Name = "label9";
			this.helpProvider1.SetShowHelp(this.label9, ((bool)(resources.GetObject("label9.ShowHelp"))));
			// 
			// label10
			// 
			resources.ApplyResources(this.label10, "label10");
			this.label10.Name = "label10";
			this.helpProvider1.SetShowHelp(this.label10, ((bool)(resources.GetObject("label10.ShowHelp"))));
			// 
			// label11
			// 
			resources.ApplyResources(this.label11, "label11");
			this.label11.Name = "label11";
			this.helpProvider1.SetShowHelp(this.label11, ((bool)(resources.GetObject("label11.ShowHelp"))));
			// 
			// label12
			// 
			resources.ApplyResources(this.label12, "label12");
			this.label12.Name = "label12";
			this.helpProvider1.SetShowHelp(this.label12, ((bool)(resources.GetObject("label12.ShowHelp"))));
			// 
			// button1
			// 
			resources.ApplyResources(this.button1, "button1");
			this.button1.Name = "button1";
			this.helpProvider1.SetShowHelp(this.button1, ((bool)(resources.GetObject("button1.ShowHelp"))));
			this.button1.UseVisualStyleBackColor = true;
			// 
			// comboBox1
			// 
			this.comboBox1.FormattingEnabled = true;
			resources.ApplyResources(this.comboBox1, "comboBox1");
			this.comboBox1.Name = "comboBox1";
			this.helpProvider1.SetShowHelp(this.comboBox1, ((bool)(resources.GetObject("comboBox1.ShowHelp"))));
			// 
			// comboBox2
			// 
			this.comboBox2.FormattingEnabled = true;
			resources.ApplyResources(this.comboBox2, "comboBox2");
			this.comboBox2.Name = "comboBox2";
			this.helpProvider1.SetShowHelp(this.comboBox2, ((bool)(resources.GetObject("comboBox2.ShowHelp"))));
			// 
			// button2
			// 
			resources.ApplyResources(this.button2, "button2");
			this.button2.Name = "button2";
			this.helpProvider1.SetShowHelp(this.button2, ((bool)(resources.GetObject("button2.ShowHelp"))));
			this.button2.UseVisualStyleBackColor = true;
			// 
			// viewEffAccBtn
			// 
			resources.ApplyResources(this.viewEffAccBtn, "viewEffAccBtn");
			this.tableLayoutPanel5.SetColumnSpan(this.viewEffAccBtn, 4);
			this.viewEffAccBtn.Name = "viewEffAccBtn";
			this.helpProvider1.SetShowHelp(this.viewEffAccBtn, ((bool)(resources.GetObject("viewEffAccBtn.ShowHelp"))));
			this.viewEffAccBtn.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.tableLayoutPanel5.SetColumnSpan(this.groupBox1, 4);
			resources.ApplyResources(this.groupBox1, "groupBox1");
			this.groupBox1.Name = "groupBox1";
			this.helpProvider1.SetShowHelp(this.groupBox1, ((bool)(resources.GetObject("groupBox1.ShowHelp"))));
			this.groupBox1.TabStop = false;
			// 
			// listView1
			// 
			this.tableLayoutPanel5.SetColumnSpan(this.listView1, 4);
			resources.ApplyResources(this.listView1, "listView1");
			this.listView1.Name = "listView1";
			this.helpProvider1.SetShowHelp(this.listView1, ((bool)(resources.GetObject("listView1.ShowHelp"))));
			this.listView1.UseCompatibleStateImageBehavior = false;
			// 
			// effAccNotificationLabel
			// 
			resources.ApplyResources(this.effAccNotificationLabel, "effAccNotificationLabel");
			this.effAccNotificationLabel.BackColor = System.Drawing.SystemColors.Info;
			this.effAccNotificationLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tableLayoutPanel5.SetColumnSpan(this.effAccNotificationLabel, 4);
			this.effAccNotificationLabel.Name = "effAccNotificationLabel";
			this.helpProvider1.SetShowHelp(this.effAccNotificationLabel, ((bool)(resources.GetObject("effAccNotificationLabel.ShowHelp"))));
			// 
			// label2
			// 
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			this.helpProvider1.SetShowHelp(this.label2, ((bool)(resources.GetObject("label2.ShowHelp"))));
			// 
			// label3
			// 
			resources.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			this.helpProvider1.SetShowHelp(this.label3, ((bool)(resources.GetObject("label3.ShowHelp"))));
			// 
			// tableLayoutPanel1
			// 
			resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
			this.tableLayoutPanel1.Controls.Add(this.applyBtn, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.cancelBtn, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.okBtn, 0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.helpProvider1.SetShowHelp(this.tableLayoutPanel1, ((bool)(resources.GetObject("tableLayoutPanel1.ShowHelp"))));
			// 
			// applyBtn
			// 
			resources.ApplyResources(this.applyBtn, "applyBtn");
			this.applyBtn.Name = "applyBtn";
			this.helpProvider1.SetShowHelp(this.applyBtn, ((bool)(resources.GetObject("applyBtn.ShowHelp"))));
			this.applyBtn.UseVisualStyleBackColor = true;
			// 
			// cancelBtn
			// 
			resources.ApplyResources(this.cancelBtn, "cancelBtn");
			this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelBtn.Name = "cancelBtn";
			this.helpProvider1.SetShowHelp(this.cancelBtn, ((bool)(resources.GetObject("cancelBtn.ShowHelp"))));
			this.cancelBtn.UseVisualStyleBackColor = true;
			// 
			// okBtn
			// 
			resources.ApplyResources(this.okBtn, "okBtn");
			this.okBtn.Name = "okBtn";
			this.helpProvider1.SetShowHelp(this.okBtn, ((bool)(resources.GetObject("okBtn.ShowHelp"))));
			this.okBtn.UseVisualStyleBackColor = true;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.Window;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.tabControl1);
			this.panel1.Controls.Add(this.tableLayoutPanel2);
			resources.ApplyResources(this.panel1, "panel1");
			this.panel1.Name = "panel1";
			this.helpProvider1.SetShowHelp(this.panel1, ((bool)(resources.GetObject("panel1.ShowHelp"))));
			// 
			// tableLayoutPanel2
			// 
			resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
			this.tableLayoutPanel2.Controls.Add(this.intLevelText, 1, 2);
			this.tableLayoutPanel2.Controls.Add(this.objNameText, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.label2, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.label3, 0, 2);
			this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel1, 1, 1);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.helpProvider1.SetShowHelp(this.tableLayoutPanel2, ((bool)(resources.GetObject("tableLayoutPanel2.ShowHelp"))));
			// 
			// intLevelText
			// 
			resources.ApplyResources(this.intLevelText, "intLevelText");
			this.intLevelText.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.intLevelText.Name = "intLevelText";
			this.intLevelText.ReadOnly = true;
			this.helpProvider1.SetShowHelp(this.intLevelText, ((bool)(resources.GetObject("intLevelText.ShowHelp"))));
			this.intLevelText.TabStop = false;
			// 
			// objNameText
			// 
			resources.ApplyResources(this.objNameText, "objNameText");
			this.objNameText.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.objNameText.Name = "objNameText";
			this.objNameText.ReadOnly = true;
			this.helpProvider1.SetShowHelp(this.objNameText, ((bool)(resources.GetObject("objNameText.ShowHelp"))));
			this.objNameText.TabStop = false;
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			this.helpProvider1.SetShowHelp(this.label1, ((bool)(resources.GetObject("label1.ShowHelp"))));
			// 
			// flowLayoutPanel1
			// 
			resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
			this.flowLayoutPanel1.Controls.Add(this.ownerText);
			this.flowLayoutPanel1.Controls.Add(this.pictureBox1);
			this.flowLayoutPanel1.Controls.Add(this.chgOwnerLink);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.helpProvider1.SetShowHelp(this.flowLayoutPanel1, ((bool)(resources.GetObject("flowLayoutPanel1.ShowHelp"))));
			// 
			// ownerText
			// 
			this.ownerText.BorderStyle = System.Windows.Forms.BorderStyle.None;
			resources.ApplyResources(this.ownerText, "ownerText");
			this.ownerText.Name = "ownerText";
			this.ownerText.ReadOnly = true;
			this.helpProvider1.SetShowHelp(this.ownerText, ((bool)(resources.GetObject("ownerText.ShowHelp"))));
			this.ownerText.TabStop = false;
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
			resources.ApplyResources(this.pictureBox1, "pictureBox1");
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.TabStop = false;
			// 
			// chgOwnerLink
			// 
			this.chgOwnerLink.FlatStyle = System.Windows.Forms.FlatStyle.System;
			resources.ApplyResources(this.chgOwnerLink, "chgOwnerLink");
			this.chgOwnerLink.Name = "chgOwnerLink";
			this.helpProvider1.SetShowHelp(this.chgOwnerLink, ((bool)(resources.GetObject("chgOwnerLink.ShowHelp"))));
			this.chgOwnerLink.TabStop = true;
			// 
			// AdvancedSecuritySettingsDialog
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "AdvancedSecuritySettingsDialog";
			this.helpProvider1.SetShowHelp(this, ((bool)(resources.GetObject("$this.ShowHelp"))));
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.permHeaderLayoutPanel.ResumeLayout(false);
			this.permHeaderLayoutPanel.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.audHeaderLayoutPanel.ResumeLayout(false);
			this.audHeaderLayoutPanel.PerformLayout();
			this.notEditableLayoutPanel.ResumeLayout(false);
			this.notEditableLayoutPanel.PerformLayout();
			this.tabPage3.ResumeLayout(false);
			this.tabPage3.PerformLayout();
			this.tableLayoutPanel5.ResumeLayout(false);
			this.tableLayoutPanel5.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage3;
		private ACLEditor permEditor;
		private System.Windows.Forms.HelpProvider helpProvider1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Button applyBtn;
		private System.Windows.Forms.Button cancelBtn;
		private System.Windows.Forms.Button okBtn;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox intLevelText;
		private System.Windows.Forms.TextBox objNameText;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.TextBox ownerText;
		private System.Windows.Forms.LinkLabel chgOwnerLink;
		private System.Windows.Forms.TableLayoutPanel permHeaderLayoutPanel;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private ACLEditor audEditor;
		private System.Windows.Forms.TableLayoutPanel audHeaderLayoutPanel;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TableLayoutPanel notEditableLayoutPanel;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Button audContinueBtn;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.ComboBox comboBox2;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button viewEffAccBtn;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.Label effAccNotificationLabel;
		private System.Windows.Forms.PictureBox pictureBox1;
	}
}