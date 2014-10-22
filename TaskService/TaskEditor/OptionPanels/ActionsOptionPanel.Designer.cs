namespace Microsoft.Win32.TaskScheduler.OptionPanels
{
	partial class ActionsOptionPanel
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
			this.actionListView = new Microsoft.Win32.TaskScheduler.ReorderableListView();
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.actionUpButton = new System.Windows.Forms.Button();
			this.actionDownButton = new System.Windows.Forms.Button();
			this.actionNewButton = new System.Windows.Forms.Button();
			this.actionDeleteButton = new System.Windows.Forms.Button();
			this.actionEditButton = new System.Windows.Forms.Button();
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
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(425, 261);
			this.tableLayoutPanel1.TabIndex = 2;
			// 
			// optionPanelHeaderLabel1
			// 
			this.optionPanelHeaderLabel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.optionPanelHeaderLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.optionPanelHeaderLabel1.Location = new System.Drawing.Point(0, 5);
			this.optionPanelHeaderLabel1.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
			this.optionPanelHeaderLabel1.Name = "optionPanelHeaderLabel1";
			this.optionPanelHeaderLabel1.Size = new System.Drawing.Size(425, 23);
			this.optionPanelHeaderLabel1.TabIndex = 0;
			this.optionPanelHeaderLabel1.Text = "Add, remove and change actions";
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.AutoSize = true;
			this.tableLayoutPanel2.ColumnCount = 4;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.Controls.Add(this.actionListView, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.actionUpButton, 3, 0);
			this.tableLayoutPanel2.Controls.Add(this.actionDownButton, 3, 1);
			this.tableLayoutPanel2.Controls.Add(this.actionNewButton, 0, 2);
			this.tableLayoutPanel2.Controls.Add(this.actionDeleteButton, 2, 2);
			this.tableLayoutPanel2.Controls.Add(this.actionEditButton, 1, 2);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(9, 33);
			this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(9, 0, 0, 0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 3;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.Size = new System.Drawing.Size(416, 228);
			this.tableLayoutPanel2.TabIndex = 1;
			// 
			// actionListView
			// 
			this.actionListView.AllowDrop = true;
			this.actionListView.AllowRowReorder = true;
			this.actionListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5});
			this.tableLayoutPanel2.SetColumnSpan(this.actionListView, 3);
			this.actionListView.Dock = System.Windows.Forms.DockStyle.Top;
			this.actionListView.FullRowSelect = true;
			this.actionListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.actionListView.HideSelection = false;
			this.actionListView.Location = new System.Drawing.Point(0, 0);
			this.actionListView.Margin = new System.Windows.Forms.Padding(0);
			this.actionListView.Name = "actionListView";
			this.tableLayoutPanel2.SetRowSpan(this.actionListView, 2);
			this.actionListView.ShowItemToolTips = true;
			this.actionListView.Size = new System.Drawing.Size(387, 200);
			this.actionListView.TabIndex = 8;
			this.actionListView.UseCompatibleStateImageBehavior = false;
			this.actionListView.View = System.Windows.Forms.View.Details;
			this.actionListView.SelectedIndexChanged += new System.EventHandler(this.actionListView_SelectedIndexChanged);
			this.actionListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.actionListView_MouseDoubleClick);
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Action";
			this.columnHeader4.Width = 112;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Details";
			this.columnHeader5.Width = 247;
			// 
			// actionUpButton
			// 
			this.actionUpButton.Font = new System.Drawing.Font("Marlett", 14F);
			this.actionUpButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.actionUpButton.Location = new System.Drawing.Point(392, 0);
			this.actionUpButton.Margin = new System.Windows.Forms.Padding(5, 0, 0, 2);
			this.actionUpButton.Name = "actionUpButton";
			this.actionUpButton.Size = new System.Drawing.Size(24, 26);
			this.actionUpButton.TabIndex = 13;
			this.actionUpButton.Text = "5 ";
			this.actionUpButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.actionUpButton.UseVisualStyleBackColor = true;
			this.actionUpButton.Click += new System.EventHandler(this.actionUpButton_Click);
			// 
			// actionDownButton
			// 
			this.actionDownButton.Font = new System.Drawing.Font("Marlett", 14F);
			this.actionDownButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.actionDownButton.Location = new System.Drawing.Point(392, 30);
			this.actionDownButton.Margin = new System.Windows.Forms.Padding(5, 2, 0, 0);
			this.actionDownButton.Name = "actionDownButton";
			this.actionDownButton.Size = new System.Drawing.Size(24, 26);
			this.actionDownButton.TabIndex = 12;
			this.actionDownButton.Text = "6";
			this.actionDownButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.actionDownButton.UseVisualStyleBackColor = true;
			this.actionDownButton.Click += new System.EventHandler(this.actionDownButton_Click);
			// 
			// actionNewButton
			// 
			this.actionNewButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.actionNewButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.actionNewButton.Location = new System.Drawing.Point(0, 205);
			this.actionNewButton.Margin = new System.Windows.Forms.Padding(0, 5, 5, 0);
			this.actionNewButton.Name = "actionNewButton";
			this.actionNewButton.Size = new System.Drawing.Size(78, 23);
			this.actionNewButton.TabIndex = 5;
			this.actionNewButton.Text = "&New...";
			this.actionNewButton.UseVisualStyleBackColor = true;
			this.actionNewButton.Click += new System.EventHandler(this.actionNewButton_Click);
			// 
			// actionDeleteButton
			// 
			this.actionDeleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.actionDeleteButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.actionDeleteButton.Location = new System.Drawing.Point(166, 205);
			this.actionDeleteButton.Margin = new System.Windows.Forms.Padding(0, 5, 5, 0);
			this.actionDeleteButton.Name = "actionDeleteButton";
			this.actionDeleteButton.Size = new System.Drawing.Size(78, 23);
			this.actionDeleteButton.TabIndex = 7;
			this.actionDeleteButton.Text = "&Delete";
			this.actionDeleteButton.UseVisualStyleBackColor = true;
			this.actionDeleteButton.Click += new System.EventHandler(this.actionDeleteButton_Click);
			// 
			// actionEditButton
			// 
			this.actionEditButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.actionEditButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.actionEditButton.Location = new System.Drawing.Point(83, 205);
			this.actionEditButton.Margin = new System.Windows.Forms.Padding(0, 5, 5, 0);
			this.actionEditButton.Name = "actionEditButton";
			this.actionEditButton.Size = new System.Drawing.Size(78, 23);
			this.actionEditButton.TabIndex = 6;
			this.actionEditButton.Text = "&Edit...";
			this.actionEditButton.UseVisualStyleBackColor = true;
			this.actionEditButton.Click += new System.EventHandler(this.actionEditButton_Click);
			// 
			// ActionsOptionPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "ActionsOptionPanel";
			this.Size = new System.Drawing.Size(425, 547);
			this.Title = "Task Actions";
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
		private System.Windows.Forms.Button actionEditButton;
		private Microsoft.Win32.TaskScheduler.ReorderableListView actionListView;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.Button actionDeleteButton;
		private System.Windows.Forms.Button actionNewButton;
		private System.Windows.Forms.Button actionUpButton;
		private System.Windows.Forms.Button actionDownButton;
	}
}
