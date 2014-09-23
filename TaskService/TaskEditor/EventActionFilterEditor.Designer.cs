namespace Microsoft.Win32.TaskScheduler
{
	partial class EventActionFilterEditor
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EventActionFilterEditor));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.cancelButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.filterTab = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.clearButton = new System.Windows.Forms.Button();
			this.logTimeCombo = new System.Windows.Forms.ComboBox();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.criticalLevelCheckBox = new System.Windows.Forms.CheckBox();
			this.warningLevelCheckBox = new System.Windows.Forms.CheckBox();
			this.infoLevelCheckBox = new System.Windows.Forms.CheckBox();
			this.errorLevelCheckBox = new System.Windows.Forms.CheckBox();
			this.verboseLevelCheckBox = new System.Windows.Forms.CheckBox();
			this.eventLogCombo = new Microsoft.Win32.TaskScheduler.DropDownCheckTree();
			this.eventSourceCombo = new Microsoft.Win32.TaskScheduler.DropDownCheckList();
			this.byLogRadio = new System.Windows.Forms.RadioButton();
			this.bySourceRadio = new System.Windows.Forms.RadioButton();
			this.userText = new System.Windows.Forms.TextBox();
			this.computerText = new System.Windows.Forms.TextBox();
			this.categoryCombo = new Microsoft.Win32.TaskScheduler.DropDownCheckList();
			this.keywordsCombo = new Microsoft.Win32.TaskScheduler.DropDownCheckList();
			this.eventIDsText = new System.Windows.Forms.TextBox();
			this.dataBtn = new System.Windows.Forms.Button();
			this.xmlTab = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.editManuallyCheckBox = new System.Windows.Forms.CheckBox();
			this.queryText = new System.Windows.Forms.TextBox();
			this.wrapCheckBox = new System.Windows.Forms.CheckBox();
			this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
			this.tableLayoutPanel1.SuspendLayout();
			this.tabControl.SuspendLayout();
			this.filterTab.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.xmlTab.SuspendLayout();
			this.tableLayoutPanel4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
			this.tableLayoutPanel1.Controls.Add(this.cancelButton, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.okButton, 1, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			// 
			// cancelButton
			// 
			resources.ApplyResources(this.cancelButton, "cancelButton");
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// okButton
			// 
			resources.ApplyResources(this.okButton, "okButton");
			this.okButton.Name = "okButton";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// tabControl
			// 
			resources.ApplyResources(this.tabControl, "tabControl");
			this.tabControl.Controls.Add(this.filterTab);
			this.tabControl.Controls.Add(this.xmlTab);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl_Selecting);
			// 
			// filterTab
			// 
			this.filterTab.Controls.Add(this.tableLayoutPanel2);
			resources.ApplyResources(this.filterTab, "filterTab");
			this.filterTab.Name = "filterTab";
			this.filterTab.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.BackColor = System.Drawing.SystemColors.Control;
			resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
			this.tableLayoutPanel2.Controls.Add(this.label2, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.label3, 1, 2);
			this.tableLayoutPanel2.Controls.Add(this.label4, 1, 3);
			this.tableLayoutPanel2.Controls.Add(this.label5, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.label6, 0, 4);
			this.tableLayoutPanel2.Controls.Add(this.label7, 0, 6);
			this.tableLayoutPanel2.Controls.Add(this.label8, 0, 7);
			this.tableLayoutPanel2.Controls.Add(this.label9, 0, 8);
			this.tableLayoutPanel2.Controls.Add(this.label10, 0, 9);
			this.tableLayoutPanel2.Controls.Add(this.clearButton, 2, 10);
			this.tableLayoutPanel2.Controls.Add(this.logTimeCombo, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 1, 1);
			this.tableLayoutPanel2.Controls.Add(this.eventLogCombo, 2, 2);
			this.tableLayoutPanel2.Controls.Add(this.eventSourceCombo, 2, 3);
			this.tableLayoutPanel2.Controls.Add(this.byLogRadio, 0, 2);
			this.tableLayoutPanel2.Controls.Add(this.bySourceRadio, 0, 3);
			this.tableLayoutPanel2.Controls.Add(this.userText, 1, 8);
			this.tableLayoutPanel2.Controls.Add(this.computerText, 1, 9);
			this.tableLayoutPanel2.Controls.Add(this.categoryCombo, 1, 6);
			this.tableLayoutPanel2.Controls.Add(this.keywordsCombo, 1, 7);
			this.tableLayoutPanel2.Controls.Add(this.eventIDsText, 1, 5);
			this.tableLayoutPanel2.Controls.Add(this.dataBtn, 0, 10);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			// 
			// label2
			// 
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			// 
			// label3
			// 
			resources.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			// 
			// label4
			// 
			resources.ApplyResources(this.label4, "label4");
			this.label4.Name = "label4";
			// 
			// label5
			// 
			resources.ApplyResources(this.label5, "label5");
			this.label5.Name = "label5";
			// 
			// label6
			// 
			this.tableLayoutPanel2.SetColumnSpan(this.label6, 3);
			resources.ApplyResources(this.label6, "label6");
			this.label6.Name = "label6";
			// 
			// label7
			// 
			resources.ApplyResources(this.label7, "label7");
			this.label7.Name = "label7";
			// 
			// label8
			// 
			resources.ApplyResources(this.label8, "label8");
			this.label8.Name = "label8";
			// 
			// label9
			// 
			resources.ApplyResources(this.label9, "label9");
			this.label9.Name = "label9";
			// 
			// label10
			// 
			resources.ApplyResources(this.label10, "label10");
			this.label10.Name = "label10";
			// 
			// clearButton
			// 
			resources.ApplyResources(this.clearButton, "clearButton");
			this.clearButton.Name = "clearButton";
			this.clearButton.UseVisualStyleBackColor = true;
			this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
			// 
			// logTimeCombo
			// 
			this.tableLayoutPanel2.SetColumnSpan(this.logTimeCombo, 2);
			resources.ApplyResources(this.logTimeCombo, "logTimeCombo");
			this.logTimeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.logTimeCombo.FormattingEnabled = true;
			this.logTimeCombo.Name = "logTimeCombo";
			this.logTimeCombo.SelectedIndexChanged += new System.EventHandler(this.logTimeCombo_SelectedIndexChanged);
			// 
			// tableLayoutPanel3
			// 
			resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
			this.tableLayoutPanel2.SetColumnSpan(this.tableLayoutPanel3, 2);
			this.tableLayoutPanel3.Controls.Add(this.criticalLevelCheckBox, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.warningLevelCheckBox, 1, 0);
			this.tableLayoutPanel3.Controls.Add(this.infoLevelCheckBox, 1, 1);
			this.tableLayoutPanel3.Controls.Add(this.errorLevelCheckBox, 0, 1);
			this.tableLayoutPanel3.Controls.Add(this.verboseLevelCheckBox, 2, 0);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			// 
			// criticalLevelCheckBox
			// 
			resources.ApplyResources(this.criticalLevelCheckBox, "criticalLevelCheckBox");
			this.criticalLevelCheckBox.Name = "criticalLevelCheckBox";
			this.criticalLevelCheckBox.UseVisualStyleBackColor = true;
			this.criticalLevelCheckBox.CheckedChanged += new System.EventHandler(this.level_checkedChanged);
			// 
			// warningLevelCheckBox
			// 
			resources.ApplyResources(this.warningLevelCheckBox, "warningLevelCheckBox");
			this.warningLevelCheckBox.Name = "warningLevelCheckBox";
			this.warningLevelCheckBox.UseVisualStyleBackColor = true;
			this.warningLevelCheckBox.CheckedChanged += new System.EventHandler(this.level_checkedChanged);
			// 
			// infoLevelCheckBox
			// 
			resources.ApplyResources(this.infoLevelCheckBox, "infoLevelCheckBox");
			this.infoLevelCheckBox.Name = "infoLevelCheckBox";
			this.infoLevelCheckBox.UseVisualStyleBackColor = true;
			this.infoLevelCheckBox.CheckedChanged += new System.EventHandler(this.level_checkedChanged);
			// 
			// errorLevelCheckBox
			// 
			resources.ApplyResources(this.errorLevelCheckBox, "errorLevelCheckBox");
			this.errorLevelCheckBox.Name = "errorLevelCheckBox";
			this.errorLevelCheckBox.UseVisualStyleBackColor = true;
			this.errorLevelCheckBox.CheckedChanged += new System.EventHandler(this.level_checkedChanged);
			// 
			// verboseLevelCheckBox
			// 
			resources.ApplyResources(this.verboseLevelCheckBox, "verboseLevelCheckBox");
			this.verboseLevelCheckBox.Name = "verboseLevelCheckBox";
			this.verboseLevelCheckBox.UseVisualStyleBackColor = true;
			this.verboseLevelCheckBox.CheckedChanged += new System.EventHandler(this.level_checkedChanged);
			// 
			// eventLogCombo
			// 
			this.eventLogCombo.ControlSize = new System.Drawing.Size(187, 105);
			resources.ApplyResources(this.eventLogCombo, "eventLogCombo");
			this.eventLogCombo.DropSize = new System.Drawing.Size(121, 106);
			this.eventLogCombo.Name = "eventLogCombo";
			this.eventLogCombo.SelectedItemsChanged += new System.EventHandler(this.eventLogCombo_SelectedItemsChanged);
			// 
			// eventSourceCombo
			// 
			this.eventSourceCombo.ControlSize = new System.Drawing.Size(187, 105);
			resources.ApplyResources(this.eventSourceCombo, "eventSourceCombo");
			this.eventSourceCombo.DropSize = new System.Drawing.Size(121, 106);
			this.eventSourceCombo.Name = "eventSourceCombo";
			this.eventSourceCombo.SelectedItemsChanged += new System.EventHandler(this.eventSourceCombo_SelectedItemsChanged);
			// 
			// byLogRadio
			// 
			resources.ApplyResources(this.byLogRadio, "byLogRadio");
			this.byLogRadio.Name = "byLogRadio";
			this.byLogRadio.UseVisualStyleBackColor = true;
			this.byLogRadio.CheckedChanged += new System.EventHandler(this.radio_CheckedChanged);
			// 
			// bySourceRadio
			// 
			resources.ApplyResources(this.bySourceRadio, "bySourceRadio");
			this.bySourceRadio.Name = "bySourceRadio";
			this.bySourceRadio.UseVisualStyleBackColor = true;
			this.bySourceRadio.CheckedChanged += new System.EventHandler(this.radio_CheckedChanged);
			// 
			// userText
			// 
			this.tableLayoutPanel2.SetColumnSpan(this.userText, 2);
			resources.ApplyResources(this.userText, "userText");
			this.userText.Name = "userText";
			this.userText.Enter += new System.EventHandler(this.nullableText_Enter);
			this.userText.Leave += new System.EventHandler(this.nullableText_Leave);
			// 
			// computerText
			// 
			this.tableLayoutPanel2.SetColumnSpan(this.computerText, 2);
			resources.ApplyResources(this.computerText, "computerText");
			this.computerText.Name = "computerText";
			this.computerText.Enter += new System.EventHandler(this.nullableText_Enter);
			this.computerText.Leave += new System.EventHandler(this.nullableText_Leave);
			// 
			// categoryCombo
			// 
			this.tableLayoutPanel2.SetColumnSpan(this.categoryCombo, 2);
			this.categoryCombo.ControlSize = new System.Drawing.Size(187, 105);
			resources.ApplyResources(this.categoryCombo, "categoryCombo");
			this.categoryCombo.DropSize = new System.Drawing.Size(121, 106);
			this.categoryCombo.Name = "categoryCombo";
			this.categoryCombo.SelectedItemsChanged += new System.EventHandler(this.categoryCombo_SelectedItemsChanged);
			// 
			// keywordsCombo
			// 
			this.tableLayoutPanel2.SetColumnSpan(this.keywordsCombo, 2);
			this.keywordsCombo.ControlSize = new System.Drawing.Size(187, 105);
			resources.ApplyResources(this.keywordsCombo, "keywordsCombo");
			this.keywordsCombo.DropSize = new System.Drawing.Size(121, 106);
			this.keywordsCombo.Name = "keywordsCombo";
			this.keywordsCombo.SelectedItemsChanged += new System.EventHandler(this.keywordsCombo_SelectedItemsChanged);
			// 
			// eventIDsText
			// 
			this.tableLayoutPanel2.SetColumnSpan(this.eventIDsText, 2);
			resources.ApplyResources(this.eventIDsText, "eventIDsText");
			this.errorProvider.SetIconPadding(this.eventIDsText, ((int)(resources.GetObject("eventIDsText.IconPadding"))));
			this.eventIDsText.Name = "eventIDsText";
			this.eventIDsText.TextChanged += new System.EventHandler(this.eventIDsText_TextChanged);
			this.eventIDsText.Enter += new System.EventHandler(this.nullableText_Enter);
			this.eventIDsText.Leave += new System.EventHandler(this.nullableText_Leave);
			// 
			// dataBtn
			// 
			resources.ApplyResources(this.dataBtn, "dataBtn");
			this.dataBtn.Name = "dataBtn";
			this.dataBtn.UseVisualStyleBackColor = true;
			this.dataBtn.Click += new System.EventHandler(this.dataBtn_Click);
			// 
			// xmlTab
			// 
			this.xmlTab.Controls.Add(this.tableLayoutPanel4);
			resources.ApplyResources(this.xmlTab, "xmlTab");
			this.xmlTab.Name = "xmlTab";
			this.xmlTab.UseVisualStyleBackColor = true;
			this.xmlTab.Enter += new System.EventHandler(this.xmlTab_Enter);
			this.xmlTab.Leave += new System.EventHandler(this.xmlTab_Leave);
			// 
			// tableLayoutPanel4
			// 
			this.tableLayoutPanel4.BackColor = System.Drawing.SystemColors.Control;
			resources.ApplyResources(this.tableLayoutPanel4, "tableLayoutPanel4");
			this.tableLayoutPanel4.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel4.Controls.Add(this.editManuallyCheckBox, 0, 2);
			this.tableLayoutPanel4.Controls.Add(this.queryText, 0, 1);
			this.tableLayoutPanel4.Controls.Add(this.wrapCheckBox, 1, 2);
			this.tableLayoutPanel4.Name = "tableLayoutPanel4";
			// 
			// label1
			// 
			this.tableLayoutPanel4.SetColumnSpan(this.label1, 2);
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// editManuallyCheckBox
			// 
			resources.ApplyResources(this.editManuallyCheckBox, "editManuallyCheckBox");
			this.editManuallyCheckBox.Name = "editManuallyCheckBox";
			this.editManuallyCheckBox.UseVisualStyleBackColor = true;
			this.editManuallyCheckBox.CheckedChanged += new System.EventHandler(this.editManuallyCheckBox_CheckedChanged);
			// 
			// queryText
			// 
			this.queryText.AcceptsReturn = true;
			this.queryText.AcceptsTab = true;
			this.queryText.AllowDrop = true;
			this.tableLayoutPanel4.SetColumnSpan(this.queryText, 2);
			resources.ApplyResources(this.queryText, "queryText");
			this.queryText.Name = "queryText";
			this.queryText.TextChanged += new System.EventHandler(this.queryText_TextChanged);
			this.queryText.DragDrop += new System.Windows.Forms.DragEventHandler(this.queryText_DragDrop);
			this.queryText.DragEnter += new System.Windows.Forms.DragEventHandler(this.queryText_DragEnter);
			// 
			// wrapCheckBox
			// 
			resources.ApplyResources(this.wrapCheckBox, "wrapCheckBox");
			this.wrapCheckBox.Name = "wrapCheckBox";
			this.wrapCheckBox.UseVisualStyleBackColor = true;
			this.wrapCheckBox.CheckedChanged += new System.EventHandler(this.wrapCheckBox_CheckedChanged);
			// 
			// errorProvider
			// 
			this.errorProvider.ContainerControl = this;
			// 
			// EventActionFilterEditor
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.tableLayoutPanel1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "EventActionFilterEditor";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tabControl.ResumeLayout(false);
			this.filterTab.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			this.xmlTab.ResumeLayout(false);
			this.tableLayoutPanel4.ResumeLayout(false);
			this.tableLayoutPanel4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage filterTab;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Button clearButton;
		private System.Windows.Forms.ComboBox logTimeCombo;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.CheckBox criticalLevelCheckBox;
		private System.Windows.Forms.CheckBox warningLevelCheckBox;
		private System.Windows.Forms.CheckBox infoLevelCheckBox;
		private System.Windows.Forms.CheckBox errorLevelCheckBox;
		private System.Windows.Forms.CheckBox verboseLevelCheckBox;
		private Microsoft.Win32.TaskScheduler.DropDownCheckTree eventLogCombo;
		private Microsoft.Win32.TaskScheduler.DropDownCheckList eventSourceCombo;
		private System.Windows.Forms.RadioButton byLogRadio;
		private System.Windows.Forms.RadioButton bySourceRadio;
		private System.Windows.Forms.TextBox userText;
		private System.Windows.Forms.TextBox computerText;
		private Microsoft.Win32.TaskScheduler.DropDownCheckList categoryCombo;
		private Microsoft.Win32.TaskScheduler.DropDownCheckList keywordsCombo;
		private System.Windows.Forms.TextBox eventIDsText;
		private System.Windows.Forms.TabPage xmlTab;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox editManuallyCheckBox;
		private System.Windows.Forms.TextBox queryText;
		private System.Windows.Forms.ErrorProvider errorProvider;
		private System.Windows.Forms.CheckBox wrapCheckBox;
		private System.Windows.Forms.Button dataBtn;
	}
}