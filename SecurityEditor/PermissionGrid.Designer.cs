namespace SecurityEditor
{
	partial class PermissionGrid
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
			this.labelPanel = new System.Windows.Forms.TableLayoutPanel();
			this.col1Label = new System.Windows.Forms.Label();
			this.col2Label = new System.Windows.Forms.Label();
			this.permLabel = new System.Windows.Forms.Label();
			this.col3Label = new System.Windows.Forms.Label();
			this.gridPanel = new System.Windows.Forms.TableLayoutPanel();
			this.labelPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// labelPanel
			// 
			this.labelPanel.AutoSize = true;
			this.labelPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.labelPanel.ColumnCount = 4;
			this.labelPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.labelPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.labelPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.labelPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.labelPanel.Controls.Add(this.col1Label, 1, 0);
			this.labelPanel.Controls.Add(this.col2Label, 2, 0);
			this.labelPanel.Controls.Add(this.permLabel, 0, 0);
			this.labelPanel.Controls.Add(this.col3Label, 3, 0);
			this.labelPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.labelPanel.Location = new System.Drawing.Point(0, 0);
			this.labelPanel.Name = "labelPanel";
			this.labelPanel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
			this.labelPanel.RowCount = 1;
			this.labelPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.labelPanel.Size = new System.Drawing.Size(311, 18);
			this.labelPanel.TabIndex = 11;
			// 
			// col1Label
			// 
			this.col1Label.Dock = System.Windows.Forms.DockStyle.Top;
			this.col1Label.Location = new System.Drawing.Point(164, 0);
			this.col1Label.Name = "col1Label";
			this.col1Label.Size = new System.Drawing.Size(44, 13);
			this.col1Label.TabIndex = 0;
			this.col1Label.Text = "Allow";
			this.col1Label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// col2Label
			// 
			this.col2Label.Dock = System.Windows.Forms.DockStyle.Top;
			this.col2Label.Location = new System.Drawing.Point(214, 0);
			this.col2Label.Name = "col2Label";
			this.col2Label.Size = new System.Drawing.Size(44, 13);
			this.col2Label.TabIndex = 0;
			this.col2Label.Text = "Deny";
			this.col2Label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// permLabel
			// 
			this.permLabel.Dock = System.Windows.Forms.DockStyle.Top;
			this.permLabel.Location = new System.Drawing.Point(0, 0);
			this.permLabel.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.permLabel.Name = "permLabel";
			this.permLabel.Size = new System.Drawing.Size(158, 13);
			this.permLabel.TabIndex = 0;
			this.permLabel.Text = "Permissions:";
			// 
			// col3Label
			// 
			this.col3Label.Dock = System.Windows.Forms.DockStyle.Top;
			this.col3Label.Location = new System.Drawing.Point(264, 0);
			this.col3Label.Name = "col3Label";
			this.col3Label.Size = new System.Drawing.Size(44, 13);
			this.col3Label.TabIndex = 0;
			this.col3Label.Text = "Other";
			this.col3Label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// gridPanel
			// 
			this.gridPanel.AutoScroll = true;
			this.gridPanel.BackColor = System.Drawing.SystemColors.Window;
			this.gridPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.gridPanel.ColumnCount = 4;
			this.gridPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.gridPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.gridPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.gridPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.gridPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridPanel.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
			this.gridPanel.Location = new System.Drawing.Point(0, 18);
			this.gridPanel.Name = "gridPanel";
			this.gridPanel.RowCount = 1;
			this.gridPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.gridPanel.Size = new System.Drawing.Size(311, 157);
			this.gridPanel.TabIndex = 3;
			this.gridPanel.Resize += new System.EventHandler(this.panel1_SizeChanged);
			// 
			// PermissionGrid
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.gridPanel);
			this.Controls.Add(this.labelPanel);
			this.Name = "PermissionGrid";
			this.Size = new System.Drawing.Size(311, 175);
			this.Load += new System.EventHandler(this.panel1_SizeChanged);
			this.labelPanel.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel labelPanel;
		private System.Windows.Forms.Label col1Label;
		private System.Windows.Forms.Label col2Label;
		private System.Windows.Forms.Label permLabel;
		private System.Windows.Forms.Label col3Label;
		private System.Windows.Forms.TableLayoutPanel gridPanel;
	}
}
