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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel1.SuspendLayout();
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
			this.tableLayoutPanel1.SetColumnSpan(this.execDirText, 2);
			this.execDirText.Name = "execDirText";
			// 
			// execArgText
			// 
			resources.ApplyResources(this.execArgText, "execArgText");
			this.tableLayoutPanel1.SetColumnSpan(this.execArgText, 2);
			this.execArgText.Name = "execArgText";
			// 
			// execProgText
			// 
			resources.ApplyResources(this.execProgText, "execProgText");
			this.execProgText.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.execProgText.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
			this.tableLayoutPanel1.SetColumnSpan(this.execProgText, 2);
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
			this.tableLayoutPanel1.SetColumnSpan(this.execProgLabel, 3);
			this.execProgLabel.Name = "execProgLabel";
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// tableLayoutPanel1
			// 
			resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
			this.tableLayoutPanel1.Controls.Add(this.execProgLabel, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.execDirText, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.execProgBrowseBtn, 2, 1);
			this.tableLayoutPanel1.Controls.Add(this.execArgText, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.execProgText, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.execDirLabel, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.execArgLabel, 0, 2);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			// 
			// ExecActionUI
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "ExecActionUI";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
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
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
	}
}
