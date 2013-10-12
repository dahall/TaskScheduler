namespace Microsoft.Win32.TaskScheduler.UIComponents
{
	partial class ExecActionUI
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExecActionUI));
			this.execProgBrowseBtn = new System.Windows.Forms.Button();
			this.execDirText = new System.Windows.Forms.TextBox();
			this.execArgText = new System.Windows.Forms.TextBox();
			this.execProgText = new System.Windows.Forms.TextBox();
			this.execDirLabel = new System.Windows.Forms.Label();
			this.execArgLabel = new System.Windows.Forms.Label();
			this.execProgLabel = new System.Windows.Forms.Label();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.SuspendLayout();
			// 
			// execProgBrowseBtn
			// 
			resources.ApplyResources(this.execProgBrowseBtn, "execProgBrowseBtn");
			this.execProgBrowseBtn.Name = "execProgBrowseBtn";
			this.execProgBrowseBtn.UseVisualStyleBackColor = true;
			this.execProgBrowseBtn.Click += new System.EventHandler(this.execProgBrowseBtn_Click);
			// 
			// execDirText
			// 
			resources.ApplyResources(this.execDirText, "execDirText");
			this.execDirText.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.execDirText.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
			this.execDirText.Name = "execDirText";
			// 
			// execArgText
			// 
			resources.ApplyResources(this.execArgText, "execArgText");
			this.execArgText.Name = "execArgText";
			// 
			// execProgText
			// 
			resources.ApplyResources(this.execProgText, "execProgText");
			this.execProgText.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.execProgText.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
			this.execProgText.Name = "execProgText";
			this.execProgText.TextChanged += new System.EventHandler(this.execProgText_TextChanged);
			// 
			// execDirLabel
			// 
			resources.ApplyResources(this.execDirLabel, "execDirLabel");
			this.execDirLabel.Name = "execDirLabel";
			// 
			// execArgLabel
			// 
			resources.ApplyResources(this.execArgLabel, "execArgLabel");
			this.execArgLabel.Name = "execArgLabel";
			// 
			// execProgLabel
			// 
			resources.ApplyResources(this.execProgLabel, "execProgLabel");
			this.execProgLabel.Name = "execProgLabel";
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			resources.ApplyResources(this.openFileDialog1, "openFileDialog1");
			// 
			// ExecActionUI
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.execProgBrowseBtn);
			this.Controls.Add(this.execDirText);
			this.Controls.Add(this.execArgText);
			this.Controls.Add(this.execProgText);
			this.Controls.Add(this.execDirLabel);
			this.Controls.Add(this.execArgLabel);
			this.Controls.Add(this.execProgLabel);
			this.Name = "ExecActionUI";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button execProgBrowseBtn;
		private System.Windows.Forms.TextBox execDirText;
		private System.Windows.Forms.TextBox execArgText;
		private System.Windows.Forms.TextBox execProgText;
		private System.Windows.Forms.Label execDirLabel;
		private System.Windows.Forms.Label execArgLabel;
		private System.Windows.Forms.Label execProgLabel;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;

	}
}
