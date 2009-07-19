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
			this.SuspendLayout();
			// 
			// taskPropertiesControl1
			// 
			this.taskPropertiesControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.taskPropertiesControl1.Editable = false;
			this.taskPropertiesControl1.Location = new System.Drawing.Point(0, 0);
			this.taskPropertiesControl1.MinimumSize = new System.Drawing.Size(533, 375);
			this.taskPropertiesControl1.Name = "taskPropertiesControl1";
			this.taskPropertiesControl1.Size = new System.Drawing.Size(539, 375);
			this.taskPropertiesControl1.TabIndex = 0;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(539, 374);
			this.Controls.Add(this.taskPropertiesControl1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);

		}

		#endregion

		public Microsoft.Win32.TaskScheduler.TaskPropertiesControl taskPropertiesControl1;

	}
}