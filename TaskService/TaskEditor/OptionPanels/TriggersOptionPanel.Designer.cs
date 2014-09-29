namespace Microsoft.Win32.TaskScheduler.OptionPanels
{
	partial class TriggersOptionPanel
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.optionPanelHeaderLabel1 = new Microsoft.Win32.TaskScheduler.OptionPanels.OptionPanelHeaderLabel();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.triggerListView = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.triggerDeleteButton = new System.Windows.Forms.Button();
			this.triggerEditButton = new System.Windows.Forms.Button();
			this.triggerNewButton = new System.Windows.Forms.Button();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.optionPanelHeaderLabel1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 37);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(401, 267);
			this.tableLayoutPanel1.TabIndex = 1;
			// 
			// optionPanelHeaderLabel1
			// 
			this.optionPanelHeaderLabel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.optionPanelHeaderLabel1.Location = new System.Drawing.Point(0, 0);
			this.optionPanelHeaderLabel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
			this.optionPanelHeaderLabel1.Name = "optionPanelHeaderLabel1";
			this.optionPanelHeaderLabel1.Size = new System.Drawing.Size(401, 23);
			this.optionPanelHeaderLabel1.TabIndex = 0;
			this.optionPanelHeaderLabel1.Text = "Add, remove and change triggers";
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.AutoSize = true;
			this.tableLayoutPanel2.ColumnCount = 3;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Controls.Add(this.triggerDeleteButton, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.triggerEditButton, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.triggerNewButton, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.triggerListView, 0, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(7, 28);
			this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(7, 0, 0, 0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 2;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.Size = new System.Drawing.Size(394, 239);
			this.tableLayoutPanel2.TabIndex = 1;
			// 
			// triggerListView
			// 
			this.triggerListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
			this.tableLayoutPanel2.SetColumnSpan(this.triggerListView, 3);
			this.triggerListView.Dock = System.Windows.Forms.DockStyle.Top;
			this.triggerListView.FullRowSelect = true;
			this.triggerListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.triggerListView.HideSelection = false;
			this.triggerListView.Location = new System.Drawing.Point(0, 0);
			this.triggerListView.Margin = new System.Windows.Forms.Padding(0);
			this.triggerListView.Name = "triggerListView";
			this.triggerListView.ShowItemToolTips = true;
			this.triggerListView.Size = new System.Drawing.Size(394, 211);
			this.triggerListView.TabIndex = 2;
			this.triggerListView.UseCompatibleStateImageBehavior = false;
			this.triggerListView.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Trigger";
			this.columnHeader1.Width = 95;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Details";
			this.columnHeader2.Width = 221;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Status";
			this.columnHeader3.Width = 74;
			// 
			// triggerDeleteButton
			// 
			this.triggerDeleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.triggerDeleteButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.triggerDeleteButton.Location = new System.Drawing.Point(0, 216);
			this.triggerDeleteButton.Margin = new System.Windows.Forms.Padding(0, 5, 5, 0);
			this.triggerDeleteButton.Name = "triggerDeleteButton";
			this.triggerDeleteButton.Size = new System.Drawing.Size(78, 23);
			this.triggerDeleteButton.TabIndex = 7;
			this.triggerDeleteButton.Text = "&Delete";
			this.triggerDeleteButton.UseVisualStyleBackColor = true;
			// 
			// triggerEditButton
			// 
			this.triggerEditButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.triggerEditButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.triggerEditButton.Location = new System.Drawing.Point(166, 216);
			this.triggerEditButton.Margin = new System.Windows.Forms.Padding(0, 5, 5, 0);
			this.triggerEditButton.Name = "triggerEditButton";
			this.triggerEditButton.Size = new System.Drawing.Size(78, 23);
			this.triggerEditButton.TabIndex = 6;
			this.triggerEditButton.Text = "&Edit...";
			this.triggerEditButton.UseVisualStyleBackColor = true;
			// 
			// triggerNewButton
			// 
			this.triggerNewButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.triggerNewButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.triggerNewButton.Location = new System.Drawing.Point(83, 216);
			this.triggerNewButton.Margin = new System.Windows.Forms.Padding(0, 5, 5, 0);
			this.triggerNewButton.Name = "triggerNewButton";
			this.triggerNewButton.Size = new System.Drawing.Size(78, 23);
			this.triggerNewButton.TabIndex = 5;
			this.triggerNewButton.Text = "&New...";
			this.triggerNewButton.UseVisualStyleBackColor = true;
			// 
			// TriggersOptionPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "TriggersOptionPanel";
			this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private OptionPanelHeaderLabel optionPanelHeaderLabel1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.ListView triggerListView;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.Button triggerDeleteButton;
		private System.Windows.Forms.Button triggerEditButton;
		private System.Windows.Forms.Button triggerNewButton;
	}
}
