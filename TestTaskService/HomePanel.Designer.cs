namespace TestTaskService
{
	partial class HomePanel
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
			this.hideDetailControl1 = new HideDetailControl();
			this.hideDetailControl2 = new HideDetailControl();
			this.SuspendLayout();
			// 
			// hideDetailControl1
			// 
			this.hideDetailControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.hideDetailControl1.BackColor = System.Drawing.Color.Transparent;
			this.hideDetailControl1.Location = new System.Drawing.Point(15, 13);
			this.hideDetailControl1.Name = "hideDetailControl1";
			this.hideDetailControl1.Size = new System.Drawing.Size(895, 115);
			this.hideDetailControl1.TabIndex = 0;
			// 
			// hideDetailControl2
			// 
			this.hideDetailControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.hideDetailControl2.BackColor = System.Drawing.Color.Transparent;
			this.hideDetailControl2.Location = new System.Drawing.Point(13, 134);
			this.hideDetailControl2.Name = "hideDetailControl2";
			this.hideDetailControl2.Size = new System.Drawing.Size(897, 246);
			this.hideDetailControl2.TabIndex = 1;
			// 
			// HomePanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.hideDetailControl2);
			this.Controls.Add(this.hideDetailControl1);
			this.Name = "HomePanel";
			this.Padding = new System.Windows.Forms.Padding(10);
			this.Size = new System.Drawing.Size(924, 697);
			this.ResumeLayout(false);

		}

		#endregion

		private HideDetailControl hideDetailControl1;
		private HideDetailControl hideDetailControl2;
	}
}
