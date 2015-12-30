namespace Microsoft.Win32.TaskScheduler
{
	partial class TaskEditDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskEditDialog));
			this.taskPropertiesControl1 = new Microsoft.Win32.TaskScheduler.TaskPropertiesControl();
			this.cancelBtn = new System.Windows.Forms.Button();
			this.okBtn = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// taskPropertiesControl1
			// 
			resources.ApplyResources(this.taskPropertiesControl1, "taskPropertiesControl1");
			this.taskPropertiesControl1.Name = "taskPropertiesControl1";
			this.taskPropertiesControl1.ComponentError += new System.EventHandler<Microsoft.Win32.TaskScheduler.TaskPropertiesControl.ComponentErrorEventArgs>(this.taskPropertiesControl1_ComponentError);
			// 
			// cancelBtn
			// 
			resources.ApplyResources(this.cancelBtn, "cancelBtn");
			this.cancelBtn.CausesValidation = false;
			this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelBtn.Name = "cancelBtn";
			this.cancelBtn.UseVisualStyleBackColor = true;
			// 
			// okBtn
			// 
			resources.ApplyResources(this.okBtn, "okBtn");
			this.okBtn.Name = "okBtn";
			this.okBtn.UseVisualStyleBackColor = true;
			this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
			// 
			// TaskEditDialog
			// 
			this.AcceptButton = this.okBtn;
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelBtn;
			this.Controls.Add(this.cancelBtn);
			this.Controls.Add(this.okBtn);
			this.Controls.Add(this.taskPropertiesControl1);
			this.MaximizeBox = false;
			this.Name = "TaskEditDialog";
			this.ShowIcon = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.ResumeLayout(false);

		}

		#endregion

		private Microsoft.Win32.TaskScheduler.TaskPropertiesControl taskPropertiesControl1;
		private System.Windows.Forms.Button cancelBtn;
		private System.Windows.Forms.Button okBtn;

	}
}