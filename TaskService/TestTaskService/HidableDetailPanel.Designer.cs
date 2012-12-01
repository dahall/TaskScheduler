namespace TestTaskService
{
	partial class HidableDetailPanel
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.headerPanel = new TestTaskService.GradientPanel();
			this.hideButton = new System.Windows.Forms.Button();
			this.headerTitle = new System.Windows.Forms.Label();
			this.detailPanel = new System.Windows.Forms.Panel();
			this.tableLayoutPanel.SuspendLayout();
			this.headerPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel
			// 
			this.tableLayoutPanel.BackColor = System.Drawing.Color.Transparent;
			this.tableLayoutPanel.ColumnCount = 1;
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel.Controls.Add(this.headerPanel, 0, 0);
			this.tableLayoutPanel.Controls.Add(this.detailPanel, 0, 1);
			this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 2;
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.Size = new System.Drawing.Size(516, 278);
			this.tableLayoutPanel.TabIndex = 0;
			// 
			// headerPanel
			// 
			this.headerPanel.Controls.Add(this.hideButton);
			this.headerPanel.Controls.Add(this.headerTitle);
			this.headerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.headerPanel.Location = new System.Drawing.Point(0, 0);
			this.headerPanel.Margin = new System.Windows.Forms.Padding(0);
			this.headerPanel.Name = "headerPanel";
			this.headerPanel.Padding = new System.Windows.Forms.Padding(3);
			this.headerPanel.Size = new System.Drawing.Size(516, 24);
			this.headerPanel.TabIndex = 0;
			// 
			// hideButton
			// 
			this.hideButton.BackColor = System.Drawing.Color.Transparent;
			this.hideButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.hideButton.Dock = System.Windows.Forms.DockStyle.Right;
			this.hideButton.FlatAppearance.BorderSize = 0;
			this.hideButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.hideButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
			this.hideButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.hideButton.Font = new System.Drawing.Font("Marlett", 9F);
			this.hideButton.Location = new System.Drawing.Point(495, 3);
			this.hideButton.Name = "hideButton";
			this.hideButton.Size = new System.Drawing.Size(18, 18);
			this.hideButton.TabIndex = 1;
			this.hideButton.Text = "5";
			this.hideButton.UseVisualStyleBackColor = false;
			this.hideButton.Click += new System.EventHandler(this.hideButton_Click);
			// 
			// headerTitle
			// 
			this.headerTitle.BackColor = System.Drawing.Color.Transparent;
			this.headerTitle.Cursor = System.Windows.Forms.Cursors.Hand;
			this.headerTitle.Dock = System.Windows.Forms.DockStyle.Fill;
			this.headerTitle.Location = new System.Drawing.Point(3, 3);
			this.headerTitle.Name = "headerTitle";
			this.headerTitle.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
			this.headerTitle.Size = new System.Drawing.Size(510, 18);
			this.headerTitle.TabIndex = 0;
			this.headerTitle.Text = "label1";
			this.headerTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.headerTitle.Click += new System.EventHandler(this.hideButton_Click);
			// 
			// detailPanel
			// 
			this.detailPanel.BackColor = System.Drawing.Color.Transparent;
			this.detailPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.detailPanel.Location = new System.Drawing.Point(3, 27);
			this.detailPanel.Name = "detailPanel";
			this.detailPanel.Size = new System.Drawing.Size(510, 248);
			this.detailPanel.TabIndex = 1;
			// 
			// HidableDetailPanel
			// 
			this.Controls.Add(this.tableLayoutPanel);
			this.Size = new System.Drawing.Size(516, 278);
			this.tableLayoutPanel.ResumeLayout(false);
			this.headerPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
		private GradientPanel headerPanel;
		private System.Windows.Forms.Button hideButton;
		private System.Windows.Forms.Label headerTitle;
		private System.Windows.Forms.Panel detailPanel;
	}
}
