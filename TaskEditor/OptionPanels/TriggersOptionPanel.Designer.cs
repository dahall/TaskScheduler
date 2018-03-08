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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TriggersOptionPanel));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.optionPanelHeaderLabel1 = new Microsoft.Win32.TaskScheduler.OptionPanels.OptionPanelHeaderLabel();
			this.triggerCollectionUI1 = new Microsoft.Win32.TaskScheduler.UIComponents.TriggerCollectionUI();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
			this.tableLayoutPanel1.Controls.Add(this.optionPanelHeaderLabel1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.triggerCollectionUI1, 0, 1);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			// 
			// optionPanelHeaderLabel1
			// 
			resources.ApplyResources(this.optionPanelHeaderLabel1, "optionPanelHeaderLabel1");
			this.optionPanelHeaderLabel1.Name = "optionPanelHeaderLabel1";
			// 
			// triggerCollectionUI1
			// 
			resources.ApplyResources(this.triggerCollectionUI1, "triggerCollectionUI1");
			this.triggerCollectionUI1.Name = "triggerCollectionUI1";
			this.triggerCollectionUI1.UseModernUI = true;
			// 
			// TriggersOptionPanel
			// 
			resources.ApplyResources(this, "$this");
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "TriggersOptionPanel";
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
