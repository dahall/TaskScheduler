namespace Microsoft.Win32.TaskScheduler
{
	partial class TimeSpanPicker
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TimeSpanPicker));
			this.comboBoxTimeSpan = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// comboBoxTimeSpan
			// 
			this.comboBoxTimeSpan.AutoCompleteCustomSource.AddRange(new string[] {
            resources.GetString("comboBoxTimeSpan.AutoCompleteCustomSource"),
            resources.GetString("comboBoxTimeSpan.AutoCompleteCustomSource1"),
            resources.GetString("comboBoxTimeSpan.AutoCompleteCustomSource2")});
			this.comboBoxTimeSpan.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.comboBoxTimeSpan.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			resources.ApplyResources(this.comboBoxTimeSpan, "comboBoxTimeSpan");
			this.comboBoxTimeSpan.FormattingEnabled = true;
			this.comboBoxTimeSpan.Name = "comboBoxTimeSpan";
			this.comboBoxTimeSpan.Leave += new System.EventHandler(this.comboBoxTimeSpan_Leave);
			this.comboBoxTimeSpan.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.comboBoxTimeSpan_Format);
			// 
			// TimeSpanPicker
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.comboBoxTimeSpan);
			this.Name = "TimeSpanPicker";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ComboBox comboBoxTimeSpan;
	}
}
