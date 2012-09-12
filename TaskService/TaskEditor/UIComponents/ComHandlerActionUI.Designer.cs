namespace Microsoft.Win32.TaskScheduler.UIComponents
{
	partial class ComHandlerActionUI
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
			this.components = new System.ComponentModel.Container();
			this.getCLSIDButton = new System.Windows.Forms.Button();
			this.comDataText = new System.Windows.Forms.TextBox();
			this.comDataLabel = new System.Windows.Forms.Label();
			this.comCLSIDText = new System.Windows.Forms.TextBox();
			this.comCLSIDLabel = new System.Windows.Forms.Label();
			this.comIntroLabel = new System.Windows.Forms.Label();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// getCLSIDButton
			// 
			this.getCLSIDButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.getCLSIDButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.getCLSIDButton.Location = new System.Drawing.Point(407, 25);
			this.getCLSIDButton.Name = "getCLSIDButton";
			this.getCLSIDButton.Size = new System.Drawing.Size(33, 25);
			this.getCLSIDButton.TabIndex = 11;
			this.getCLSIDButton.Text = "...";
			this.getCLSIDButton.UseVisualStyleBackColor = true;
			this.getCLSIDButton.Click += new System.EventHandler(this.getCLSIDButton_Click);
			// 
			// comDataText
			// 
			this.comDataText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comDataText.Location = new System.Drawing.Point(82, 55);
			this.comDataText.Name = "comDataText";
			this.comDataText.Size = new System.Drawing.Size(357, 23);
			this.comDataText.TabIndex = 10;
			// 
			// comDataLabel
			// 
			this.comDataLabel.AutoSize = true;
			this.comDataLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.comDataLabel.Location = new System.Drawing.Point(-3, 58);
			this.comDataLabel.Name = "comDataLabel";
			this.comDataLabel.Size = new System.Drawing.Size(34, 15);
			this.comDataLabel.TabIndex = 9;
			this.comDataLabel.Text = "Data:";
			// 
			// comCLSIDText
			// 
			this.comCLSIDText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comCLSIDText.Location = new System.Drawing.Point(82, 26);
			this.comCLSIDText.MinimumSize = new System.Drawing.Size(220, 4);
			this.comCLSIDText.Name = "comCLSIDText";
			this.comCLSIDText.ReadOnly = true;
			this.comCLSIDText.Size = new System.Drawing.Size(319, 23);
			this.comCLSIDText.TabIndex = 8;
			// 
			// comCLSIDLabel
			// 
			this.comCLSIDLabel.AutoSize = true;
			this.comCLSIDLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.comCLSIDLabel.Location = new System.Drawing.Point(-3, 29);
			this.comCLSIDLabel.Name = "comCLSIDLabel";
			this.comCLSIDLabel.Size = new System.Drawing.Size(76, 15);
			this.comCLSIDLabel.TabIndex = 7;
			this.comCLSIDLabel.Text = "COM Object:";
			// 
			// comIntroLabel
			// 
			this.comIntroLabel.AutoSize = true;
			this.comIntroLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.comIntroLabel.Location = new System.Drawing.Point(-3, 0);
			this.comIntroLabel.Name = "comIntroLabel";
			this.comIntroLabel.Size = new System.Drawing.Size(220, 15);
			this.comIntroLabel.TabIndex = 6;
			this.comIntroLabel.Text = "This action runs a custom COM handler.";
			// 
			// ComHandlerActionUI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.comDataText);
			this.Controls.Add(this.getCLSIDButton);
			this.Controls.Add(this.comDataLabel);
			this.Controls.Add(this.comIntroLabel);
			this.Controls.Add(this.comCLSIDLabel);
			this.Controls.Add(this.comCLSIDText);
			this.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.MinimumSize = new System.Drawing.Size(343, 89);
			this.Name = "ComHandlerActionUI";
			this.Size = new System.Drawing.Size(439, 89);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button getCLSIDButton;
		private System.Windows.Forms.TextBox comDataText;
		private System.Windows.Forms.Label comDataLabel;
		private System.Windows.Forms.TextBox comCLSIDText;
		private System.Windows.Forms.Label comCLSIDLabel;
		private System.Windows.Forms.Label comIntroLabel;
		private System.Windows.Forms.ToolTip toolTip;
	}
}
