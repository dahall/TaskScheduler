namespace Microsoft.Win32.TaskScheduler
{
	partial class FullDateTimePicker
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
			this.dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
			this.tableLayoutPanelFullDateTime = new System.Windows.Forms.TableLayoutPanel();
			this.dateTimePickerTime = new System.Windows.Forms.DateTimePicker();
			this.utcCheckBox = new System.Windows.Forms.CheckBox();
			this.tableLayoutPanelFullDateTime.SuspendLayout();
			this.SuspendLayout();
			// 
			// dateTimePickerDate
			// 
			this.dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePickerDate.Location = new System.Drawing.Point(0, 0);
			this.dateTimePickerDate.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.dateTimePickerDate.Name = "dateTimePickerDate";
			this.dateTimePickerDate.Size = new System.Drawing.Size(100, 20);
			this.dateTimePickerDate.TabIndex = 0;
			// 
			// tableLayoutPanelFullDateTime
			// 
			this.tableLayoutPanelFullDateTime.AutoSize = true;
			this.tableLayoutPanelFullDateTime.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanelFullDateTime.ColumnCount = 3;
			this.tableLayoutPanelFullDateTime.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanelFullDateTime.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanelFullDateTime.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanelFullDateTime.Controls.Add(this.dateTimePickerDate, 0, 0);
			this.tableLayoutPanelFullDateTime.Controls.Add(this.dateTimePickerTime, 1, 0);
			this.tableLayoutPanelFullDateTime.Controls.Add(this.utcCheckBox, 2, 0);
			this.tableLayoutPanelFullDateTime.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanelFullDateTime.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanelFullDateTime.Name = "tableLayoutPanelFullDateTime";
			this.tableLayoutPanelFullDateTime.RowCount = 1;
			this.tableLayoutPanelFullDateTime.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelFullDateTime.Size = new System.Drawing.Size(383, 20);
			this.tableLayoutPanelFullDateTime.TabIndex = 1;
			// 
			// dateTimePickerTime
			// 
			this.dateTimePickerTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			this.dateTimePickerTime.Location = new System.Drawing.Point(106, 0);
			this.dateTimePickerTime.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.dateTimePickerTime.Name = "dateTimePickerTime";
			this.dateTimePickerTime.ShowUpDown = true;
			this.dateTimePickerTime.Size = new System.Drawing.Size(100, 20);
			this.dateTimePickerTime.TabIndex = 1;
			// 
			// utcCheckBox
			// 
			this.utcCheckBox.AutoSize = true;
			this.utcCheckBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.utcCheckBox.Location = new System.Drawing.Point(212, 2);
			this.utcCheckBox.Margin = new System.Windows.Forms.Padding(6, 2, 0, 0);
			this.utcCheckBox.Name = "utcCheckBox";
			this.utcCheckBox.Size = new System.Drawing.Size(171, 17);
			this.utcCheckBox.TabIndex = 3;
			this.utcCheckBox.Text = "Synchronize across time zones";
			this.utcCheckBox.UseVisualStyleBackColor = true;
			// 
			// FullDateTimePicker
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Controls.Add(this.tableLayoutPanelFullDateTime);
			this.Name = "FullDateTimePicker";
			this.Size = new System.Drawing.Size(383, 20);
			this.Load += new System.EventHandler(this.FullDateTimePicker_Load);
			this.RightToLeftChanged += new System.EventHandler(this.FullDateTimePicker_RightToLeftChanged);
			this.tableLayoutPanelFullDateTime.ResumeLayout(false);
			this.tableLayoutPanelFullDateTime.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DateTimePicker dateTimePickerDate;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanelFullDateTime;
		private System.Windows.Forms.DateTimePicker dateTimePickerTime;
		private System.Windows.Forms.CheckBox utcCheckBox;
	}
}
