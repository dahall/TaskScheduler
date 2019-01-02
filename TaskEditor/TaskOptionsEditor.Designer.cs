namespace Microsoft.Win32.TaskScheduler
{
	partial class TaskOptionsEditor
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskOptionsEditor));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.cancelButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.menuPanel = new System.Windows.Forms.Panel();
			this.menuList = new System.Windows.Forms.ListBox();
			this.generalItem = new System.Windows.Forms.ToolStripMenuItem();
			this.triggersItem = new System.Windows.Forms.ToolStripMenuItem();
			this.actionsItem = new System.Windows.Forms.ToolStripMenuItem();
			this.securityItem = new System.Windows.Forms.ToolStripMenuItem();
			this.startupItem = new System.Windows.Forms.ToolStripMenuItem();
			this.runItem = new System.Windows.Forms.ToolStripMenuItem();
			this.bodyPanel = new System.Windows.Forms.Panel();
			this.panelHeading = new System.Windows.Forms.TableLayoutPanel();
			this.panelTitleLabel = new System.Windows.Forms.Label();
			this.panelImage = new System.Windows.Forms.Label();
			this.splitterPanel = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.taskVersionCombo = new Vanara.Windows.Forms.DisabledItemComboBox();
			this.taskNameText = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.menuItemsContainer = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tableLayoutPanel1.SuspendLayout();
			this.menuPanel.SuspendLayout();
			this.bodyPanel.SuspendLayout();
			this.panelHeading.SuspendLayout();
			this.panel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.menuItemsContainer.SuspendLayout();
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
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Name = "okButton";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// menuPanel
			// 
			this.menuPanel.BackColor = System.Drawing.SystemColors.Window;
			this.menuPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.menuPanel.Controls.Add(this.menuList);
			resources.ApplyResources(this.menuPanel, "menuPanel");
			this.menuPanel.Name = "menuPanel";
			// 
			// menuList
			// 
			this.menuList.BorderStyle = System.Windows.Forms.BorderStyle.None;
			resources.ApplyResources(this.menuList, "menuList");
			this.menuList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
			this.menuList.Name = "menuList";
			this.menuList.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.menuList_DrawItem);
			this.menuList.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.menuList_MeasureItem);
			this.menuList.SelectedIndexChanged += new System.EventHandler(this.menuList_SelectedIndexChanged);
			this.menuList.MouseLeave += new System.EventHandler(this.menuList_MouseLeave);
			this.menuList.MouseMove += new System.Windows.Forms.MouseEventHandler(this.menuList_MouseMove);
			// 
			// generalItem
			// 
			this.generalItem.Name = "generalItem";
			this.generalItem.Padding = new System.Windows.Forms.Padding(4);
			resources.ApplyResources(this.generalItem, "generalItem");
			this.generalItem.Click += new System.EventHandler(this.menuItem_Click);
			// 
			// triggersItem
			// 
			this.triggersItem.Name = "triggersItem";
			this.triggersItem.Padding = new System.Windows.Forms.Padding(4);
			resources.ApplyResources(this.triggersItem, "triggersItem");
			this.triggersItem.Click += new System.EventHandler(this.menuItem_Click);
			// 
			// actionsItem
			// 
			this.actionsItem.Name = "actionsItem";
			this.actionsItem.Padding = new System.Windows.Forms.Padding(4);
			resources.ApplyResources(this.actionsItem, "actionsItem");
			this.actionsItem.Click += new System.EventHandler(this.menuItem_Click);
			// 
			// securityItem
			// 
			this.securityItem.Name = "securityItem";
			this.securityItem.Padding = new System.Windows.Forms.Padding(4);
			resources.ApplyResources(this.securityItem, "securityItem");
			this.securityItem.Click += new System.EventHandler(this.menuItem_Click);
			// 
			// startupItem
			// 
			this.startupItem.Name = "startupItem";
			this.startupItem.Padding = new System.Windows.Forms.Padding(4);
			resources.ApplyResources(this.startupItem, "startupItem");
			this.startupItem.Click += new System.EventHandler(this.menuItem_Click);
			// 
			// runItem
			// 
			this.runItem.Name = "runItem";
			this.runItem.Padding = new System.Windows.Forms.Padding(4);
			resources.ApplyResources(this.runItem, "runItem");
			this.runItem.Click += new System.EventHandler(this.menuItem_Click);
			// 
			// bodyPanel
			// 
			this.bodyPanel.BackColor = System.Drawing.SystemColors.Window;
			this.bodyPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.bodyPanel.Controls.Add(this.panelHeading);
			resources.ApplyResources(this.bodyPanel, "bodyPanel");
			this.bodyPanel.Name = "bodyPanel";
			// 
			// panelHeading
			// 
			resources.ApplyResources(this.panelHeading, "panelHeading");
			this.panelHeading.Controls.Add(this.panelTitleLabel, 1, 0);
			this.panelHeading.Controls.Add(this.panelImage, 0, 0);
			this.panelHeading.Name = "panelHeading";
			// 
			// panelTitleLabel
			// 
			resources.ApplyResources(this.panelTitleLabel, "panelTitleLabel");
			this.panelTitleLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
			this.panelTitleLabel.Name = "panelTitleLabel";
			// 
			// panelImage
			// 
			resources.ApplyResources(this.panelImage, "panelImage");
			this.panelImage.Name = "panelImage";
			// 
			// splitterPanel
			// 
			resources.ApplyResources(this.splitterPanel, "splitterPanel");
			this.splitterPanel.Name = "splitterPanel";
			// 
			// panel1
			// 
			resources.ApplyResources(this.panel1, "panel1");
			this.panel1.BackColor = System.Drawing.SystemColors.Window;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.tableLayoutPanel2);
			this.panel1.Name = "panel1";
			// 
			// tableLayoutPanel2
			// 
			resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
			this.tableLayoutPanel2.Controls.Add(this.taskVersionCombo, 1, 1);
			this.tableLayoutPanel2.Controls.Add(this.taskNameText, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.label4, 0, 1);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			// 
			// taskVersionCombo
			// 
			resources.ApplyResources(this.taskVersionCombo, "taskVersionCombo");
			this.taskVersionCombo.Name = "taskVersionCombo";
			this.taskVersionCombo.SelectedIndexChanged += new System.EventHandler(this.taskVersionCombo_SelectedIndexChanged);
			this.taskVersionCombo.Enter += new System.EventHandler(this.taskVersionCombo_GotFocus);
			// 
			// taskNameText
			// 
			resources.ApplyResources(this.taskNameText, "taskNameText");
			this.taskNameText.Name = "taskNameText";
			this.taskNameText.Validating += new System.ComponentModel.CancelEventHandler(this.taskNameText_Validating);
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// label4
			// 
			resources.ApplyResources(this.label4, "label4");
			this.label4.Name = "label4";
			// 
			// panel2
			// 
			resources.ApplyResources(this.panel2, "panel2");
			this.panel2.Name = "panel2";
			// 
			// menuItemsContainer
			// 
			this.menuItemsContainer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.generalItem,
			this.triggersItem,
			this.actionsItem,
			this.securityItem,
			this.startupItem,
			this.runItem});
			this.menuItemsContainer.Name = "menuItemsContainer";
			resources.ApplyResources(this.menuItemsContainer, "menuItemsContainer");
			// 
			// TaskOptionsEditor
			// 
			this.AcceptButton = this.okButton;
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ControlBox = false;
			this.Controls.Add(this.bodyPanel);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.splitterPanel);
			this.Controls.Add(this.menuPanel);
			this.Controls.Add(this.tableLayoutPanel1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "TaskOptionsEditor";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.TaskOptionsEditor_HelpButtonClicked);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.menuPanel.ResumeLayout(false);
			this.bodyPanel.ResumeLayout(false);
			this.bodyPanel.PerformLayout();
			this.panelHeading.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.menuItemsContainer.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Panel menuPanel;
		private System.Windows.Forms.ListBox menuList;
		private System.Windows.Forms.ToolStripMenuItem generalItem;
		private System.Windows.Forms.ToolStripMenuItem triggersItem;
		private System.Windows.Forms.ToolStripMenuItem actionsItem;
		private System.Windows.Forms.ToolStripMenuItem securityItem;
		private System.Windows.Forms.ToolStripMenuItem startupItem;
		private System.Windows.Forms.ToolStripMenuItem runItem;
		private System.Windows.Forms.Panel bodyPanel;
		private System.Windows.Forms.Panel splitterPanel;
		private System.Windows.Forms.TableLayoutPanel panelHeading;
		private System.Windows.Forms.Label panelTitleLabel;
		private System.Windows.Forms.Label panelImage;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.TextBox taskNameText;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label4;
		private Vanara.Windows.Forms.DisabledItemComboBox taskVersionCombo;
		private System.Windows.Forms.ContextMenuStrip menuItemsContainer;
	}
}