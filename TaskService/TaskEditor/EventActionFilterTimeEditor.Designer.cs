namespace Microsoft.Win32.TaskScheduler
{
	partial class EventActionFilterTimeEditor
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EventActionFilterTimeEditor));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.cancelButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.fromDatePicker = new Microsoft.Win32.TaskScheduler.FullDateTimePicker();
			this.toDatePicker = new Microsoft.Win32.TaskScheduler.FullDateTimePicker();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.fromCombo = new System.Windows.Forms.ComboBox();
			this.toCombo = new System.Windows.Forms.ComboBox();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
			this.tableLayoutPanel1.Controls.Add(this.cancelButton, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.okButton, 1, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			// 
			// cancelButton
			// 
			resources.ApplyResources(this.cancelButton, "cancelButton");
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// okButton
			// 
			resources.ApplyResources(this.okButton, "okButton");
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Name = "okButton";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// fromDatePicker
			// 
			resources.ApplyResources(this.fromDatePicker, "fromDatePicker");
			this.fromDatePicker.Name = "fromDatePicker";
			// 
			// toDatePicker
			// 
			resources.ApplyResources(this.toDatePicker, "toDatePicker");
			this.toDatePicker.Name = "toDatePicker";
			// 
			// tableLayoutPanel2
			// 
			resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
			this.tableLayoutPanel2.Controls.Add(this.fromDatePicker, 2, 1);
			this.tableLayoutPanel2.Controls.Add(this.toDatePicker, 2, 2);
			this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.label2, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.label3, 0, 2);
			this.tableLayoutPanel2.Controls.Add(this.fromCombo, 1, 1);
			this.tableLayoutPanel2.Controls.Add(this.toCombo, 1, 2);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.tableLayoutPanel2.SetColumnSpan(this.label1, 3);
			this.label1.Name = "label1";
			// 
			// label2
			// 
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			// 
			// label3
			// 
			resources.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			// 
			// fromCombo
			// 
			resources.ApplyResources(this.fromCombo, "fromCombo");
			this.fromCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.fromCombo.FormattingEnabled = true;
			this.fromCombo.Items.AddRange(new object[] {
            resources.GetString("fromCombo.Items"),
            resources.GetString("fromCombo.Items1")});
			this.fromCombo.Name = "fromCombo";
			this.fromCombo.SelectedIndexChanged += new System.EventHandler(this.fromCombo_SelectedIndexChanged);
			// 
			// toCombo
			// 
			resources.ApplyResources(this.toCombo, "toCombo");
			this.toCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.toCombo.FormattingEnabled = true;
			this.toCombo.Items.AddRange(new object[] {
            resources.GetString("toCombo.Items"),
            resources.GetString("toCombo.Items1")});
			this.toCombo.Name = "toCombo";
			this.toCombo.SelectedIndexChanged += new System.EventHandler(this.toCombo_SelectedIndexChanged);
			// 
			// EventActionFilterTimeEditor
			// 
			this.AcceptButton = this.okButton;
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.Controls.Add(this.tableLayoutPanel2);
			this.Controls.Add(this.tableLayoutPanel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "EventActionFilterTimeEditor";
			this.ShowInTaskbar = false;
			this.Load += new System.EventHandler(this.EventActionFilterTimeEditor_Load);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button okButton;
		private FullDateTimePicker fromDatePicker;
		private FullDateTimePicker toDatePicker;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox fromCombo;
		private System.Windows.Forms.ComboBox toCombo;
	}
}