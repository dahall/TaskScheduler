namespace Microsoft.Win32.TaskScheduler
{
	partial class TaskRunTimesDialog
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
			this.cancelButton = new System.Windows.Forms.Button();
			this.taskRunTimesControl1 = new Microsoft.Win32.TaskScheduler.TaskRunTimesControl();
			((System.ComponentModel.ISupportInitialize)(this.taskRunTimesControl1)).BeginInit();
			this.SuspendLayout();
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(182, 259);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 9;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// taskRunTimesControl1
			// 
			this.taskRunTimesControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.taskRunTimesControl1.Location = new System.Drawing.Point(12, 12);
			this.taskRunTimesControl1.MinimumSize = new System.Drawing.Size(244, 185);
			this.taskRunTimesControl1.Name = "taskRunTimesControl1";
			this.taskRunTimesControl1.Size = new System.Drawing.Size(245, 241);
			this.taskRunTimesControl1.TabIndex = 10;
			this.taskRunTimesControl1.Task = null;
			// 
			// TaskRunTimesDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(269, 294);
			this.Controls.Add(this.taskRunTimesControl1);
			this.Controls.Add(this.cancelButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "TaskRunTimesDialog";
			this.Text = "Task Run Times";
			((System.ComponentModel.ISupportInitialize)(this.taskRunTimesControl1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button cancelButton;
		private TaskRunTimesControl taskRunTimesControl1;
	}
}