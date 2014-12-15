namespace Microsoft.Win32.TaskScheduler.OptionPanels
{
	partial class TriggersOptionPanel
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.optionPanelHeaderLabel1 = new Microsoft.Win32.TaskScheduler.OptionPanels.OptionPanelHeaderLabel();
			this.triggerCollectionUI1 = new Microsoft.Win32.TaskScheduler.UIComponents.TriggerCollectionUI();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.optionPanelHeaderLabel1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.triggerCollectionUI1, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(330, 262);
			this.tableLayoutPanel1.TabIndex = 1;
			// 
			// optionPanelHeaderLabel1
			// 
			this.optionPanelHeaderLabel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.optionPanelHeaderLabel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			this.optionPanelHeaderLabel1.Location = new System.Drawing.Point(0, 0);
			this.optionPanelHeaderLabel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
			this.optionPanelHeaderLabel1.Name = "optionPanelHeaderLabel1";
			this.optionPanelHeaderLabel1.Size = new System.Drawing.Size(330, 23);
			this.optionPanelHeaderLabel1.TabIndex = 0;
			this.optionPanelHeaderLabel1.Text = "Add, remove and change triggers";
			// 
			// triggerCollectionUI1
			// 
			this.triggerCollectionUI1.Dock = System.Windows.Forms.DockStyle.Top;
			this.triggerCollectionUI1.Location = new System.Drawing.Point(0, 29);
			this.triggerCollectionUI1.Margin = new System.Windows.Forms.Padding(0);
			this.triggerCollectionUI1.Name = "triggerCollectionUI1";
			this.triggerCollectionUI1.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
			this.triggerCollectionUI1.Size = new System.Drawing.Size(330, 233);
			this.triggerCollectionUI1.TabIndex = 1;
			this.triggerCollectionUI1.UseModernUI = true;
			// 
			// TriggersOptionPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "TriggersOptionPanel";
			this.Size = new System.Drawing.Size(330, 412);
			this.Title = "Trigger Options";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private OptionPanelHeaderLabel optionPanelHeaderLabel1;
		private UIComponents.TriggerCollectionUI triggerCollectionUI1;
	}
}
