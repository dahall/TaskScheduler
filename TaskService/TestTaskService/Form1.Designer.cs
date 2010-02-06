namespace TestTaskService
{
	partial class Form1
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
			this.taskPropertiesControl1 = new Microsoft.Win32.TaskScheduler.TaskPropertiesControl();
			this.cancelBtn = new System.Windows.Forms.Button();
			this.okBtn = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// taskPropertiesControl1
			// 
			this.taskPropertiesControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.taskPropertiesControl1.Location = new System.Drawing.Point(12, 12);
			this.taskPropertiesControl1.MinimumSize = new System.Drawing.Size(622, 433);
			this.taskPropertiesControl1.Name = "taskPropertiesControl1";
			this.taskPropertiesControl1.Size = new System.Drawing.Size(625, 433);
			this.taskPropertiesControl1.TabIndex = 0;
			// 
			// cancelBtn
			// 
			this.cancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelBtn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.cancelBtn.Location = new System.Drawing.Point(550, 453);
			this.cancelBtn.Name = "cancelBtn";
			this.cancelBtn.Size = new System.Drawing.Size(87, 27);
			this.cancelBtn.TabIndex = 7;
			this.cancelBtn.Text = "Cancel";
			this.cancelBtn.UseVisualStyleBackColor = true;
			// 
			// okBtn
			// 
			this.okBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okBtn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.okBtn.Location = new System.Drawing.Point(455, 453);
			this.okBtn.Name = "okBtn";
			this.okBtn.Size = new System.Drawing.Size(87, 27);
			this.okBtn.TabIndex = 6;
			this.okBtn.Text = "OK";
			this.okBtn.UseVisualStyleBackColor = true;
			this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
			// 
			// Form1
			// 
			this.AcceptButton = this.okBtn;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelBtn;
			this.ClientSize = new System.Drawing.Size(651, 493);
			this.Controls.Add(this.cancelBtn);
			this.Controls.Add(this.okBtn);
			this.Controls.Add(this.taskPropertiesControl1);
			this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "Form1";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Task Properties";
			this.ResumeLayout(false);

		}

		#endregion

		public Microsoft.Win32.TaskScheduler.TaskPropertiesControl taskPropertiesControl1;
		private System.Windows.Forms.Button cancelBtn;
		private System.Windows.Forms.Button okBtn;

	}
}