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
			this.headerPanel = new TestTaskService.PanelHeader();
			this.detailPanel = new System.Windows.Forms.Panel();
			this.tableLayoutPanel.SuspendLayout();
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
			this.headerPanel.Checked = true;
			this.headerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.headerPanel.Location = new System.Drawing.Point(0, 0);
			this.headerPanel.Margin = new System.Windows.Forms.Padding(0);
			this.headerPanel.Name = "headerPanel";
			this.headerPanel.Padding = new System.Windows.Forms.Padding(3);
			this.headerPanel.Size = new System.Drawing.Size(516, 24);
			this.headerPanel.TabIndex = 0;
			this.headerPanel.CheckChanged += new System.EventHandler(this.headerPanel_CheckedChanged);
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
			this.Name = "HidableDetailPanel";
			this.Size = new System.Drawing.Size(516, 278);
			this.tableLayoutPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
		private PanelHeader headerPanel;
		private System.Windows.Forms.Panel detailPanel;
	}
}
