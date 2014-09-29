namespace Microsoft.Win32.TaskScheduler.OptionPanels
{
	partial class OptionPanel
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
			this.panelHeading = new System.Windows.Forms.TableLayoutPanel();
			this.panelTitleLabel = new System.Windows.Forms.Label();
			this.panelImage = new System.Windows.Forms.Label();
			this.panelHeading.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelHeading
			// 
			this.panelHeading.AutoSize = true;
			this.panelHeading.ColumnCount = 2;
			this.panelHeading.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.panelHeading.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.panelHeading.Controls.Add(this.panelTitleLabel, 1, 0);
			this.panelHeading.Controls.Add(this.panelImage, 0, 0);
			this.panelHeading.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelHeading.Location = new System.Drawing.Point(12, 5);
			this.panelHeading.Margin = new System.Windows.Forms.Padding(0);
			this.panelHeading.Name = "panelHeading";
			this.panelHeading.RowCount = 1;
			this.panelHeading.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.panelHeading.Size = new System.Drawing.Size(401, 32);
			this.panelHeading.TabIndex = 0;
			// 
			// panelTitleLabel
			// 
			this.panelTitleLabel.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelTitleLabel.Location = new System.Drawing.Point(36, 0);
			this.panelTitleLabel.Margin = new System.Windows.Forms.Padding(4, 0, 0, 0);
			this.panelTitleLabel.Name = "panelTitleLabel";
			this.panelTitleLabel.Size = new System.Drawing.Size(365, 32);
			this.panelTitleLabel.TabIndex = 0;
			this.panelTitleLabel.Text = "[Title here]";
			this.panelTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// panelImage
			// 
			this.panelImage.Location = new System.Drawing.Point(0, 0);
			this.panelImage.Margin = new System.Windows.Forms.Padding(0);
			this.panelImage.Name = "panelImage";
			this.panelImage.Size = new System.Drawing.Size(32, 32);
			this.panelImage.TabIndex = 0;
			this.panelImage.Text = "(Img)";
			// 
			// OptionPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.Controls.Add(this.panelHeading);
			this.Name = "OptionPanel";
			this.Padding = new System.Windows.Forms.Padding(12, 5, 12, 5);
			this.Size = new System.Drawing.Size(425, 357);
			this.panelHeading.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel panelHeading;
		private System.Windows.Forms.Label panelTitleLabel;
		private System.Windows.Forms.Label panelImage;
	}
}
