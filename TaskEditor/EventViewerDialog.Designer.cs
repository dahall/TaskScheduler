namespace Microsoft.Win32.TaskScheduler
{
	partial class EventViewerDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EventViewerDialog));
			this.eventViewerControl1 = new Microsoft.Win32.TaskScheduler.EventViewerControl();
			this.copyBtn = new System.Windows.Forms.Button();
			this.closeBtn = new System.Windows.Forms.Button();
			this.prevBtn = new System.Windows.Forms.Button();
			this.nextBtn = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// eventViewerControl1
			// 
			this.eventViewerControl1.ActiveTab = Microsoft.Win32.TaskScheduler.EventViewerControl.EventViewerActiveTab.General;
			this.eventViewerControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.eventViewerControl1.BackColor = System.Drawing.SystemColors.Control;
			this.eventViewerControl1.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.eventViewerControl1.Location = new System.Drawing.Point(12, 12);
			this.eventViewerControl1.Name = "eventViewerControl1";
			this.eventViewerControl1.Size = new System.Drawing.Size(551, 335);
			this.eventViewerControl1.TabIndex = 0;
			this.eventViewerControl1.TaskEvent = null;
			// 
			// copyBtn
			// 
			this.copyBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.copyBtn.Location = new System.Drawing.Point(12, 359);
			this.copyBtn.Name = "copyBtn";
			this.copyBtn.Size = new System.Drawing.Size(87, 27);
			this.copyBtn.TabIndex = 1;
			this.copyBtn.Text = "Co&py";
			this.copyBtn.UseVisualStyleBackColor = true;
			this.copyBtn.Click += new System.EventHandler(this.copyBtn_Click);
			// 
			// closeBtn
			// 
			this.closeBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.closeBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.closeBtn.Location = new System.Drawing.Point(513, 359);
			this.closeBtn.Name = "closeBtn";
			this.closeBtn.Size = new System.Drawing.Size(87, 27);
			this.closeBtn.TabIndex = 1;
			this.closeBtn.Text = "&Close";
			this.closeBtn.UseVisualStyleBackColor = true;
			this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
			// 
			// prevBtn
			// 
			this.prevBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.prevBtn.Enabled = false;
			this.prevBtn.Image = ((System.Drawing.Image)(resources.GetObject("prevBtn.Image")));
			this.prevBtn.Location = new System.Drawing.Point(574, 155);
			this.prevBtn.Name = "prevBtn";
			this.prevBtn.Size = new System.Drawing.Size(26, 26);
			this.prevBtn.TabIndex = 2;
			this.prevBtn.UseVisualStyleBackColor = true;
			this.prevBtn.Visible = false;
			this.prevBtn.Click += new System.EventHandler(this.prevBtn_Click);
			// 
			// nextBtn
			// 
			this.nextBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.nextBtn.Enabled = false;
			this.nextBtn.Image = ((System.Drawing.Image)(resources.GetObject("nextBtn.Image")));
			this.nextBtn.Location = new System.Drawing.Point(574, 188);
			this.nextBtn.Name = "nextBtn";
			this.nextBtn.Size = new System.Drawing.Size(26, 26);
			this.nextBtn.TabIndex = 2;
			this.nextBtn.UseVisualStyleBackColor = true;
			this.nextBtn.Visible = false;
			this.nextBtn.Click += new System.EventHandler(this.nextBtn_Click);
			// 
			// EventViewerDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.closeBtn;
			this.ClientSize = new System.Drawing.Size(612, 398);
			this.Controls.Add(this.nextBtn);
			this.Controls.Add(this.prevBtn);
			this.Controls.Add(this.closeBtn);
			this.Controls.Add(this.copyBtn);
			this.Controls.Add(this.eventViewerControl1);
			this.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.Name = "EventViewerDialog";
			this.Text = "EventViewDialog";
			this.ResumeLayout(false);

		}

		#endregion

		private EventViewerControl eventViewerControl1;
		private System.Windows.Forms.Button copyBtn;
		private System.Windows.Forms.Button closeBtn;
		private System.Windows.Forms.Button prevBtn;
		private System.Windows.Forms.Button nextBtn;
	}
}