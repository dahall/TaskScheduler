namespace Microsoft.Win32.TaskScheduler.OptionPanels
{
	partial class GeneralOptionPanel
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GeneralOptionPanel));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.optionPanelHeaderLabel2 = new Microsoft.Win32.TaskScheduler.OptionPanels.OptionPanelHeaderLabel();
			this.taskRegDocText = new System.Windows.Forms.TextBox();
			this.taskRegDocLabel = new System.Windows.Forms.Label();
			this.taskRegVersionLabel = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.taskAuthorText = new System.Windows.Forms.TextBox();
			this.taskRegSourceLabel = new System.Windows.Forms.Label();
			this.taskRegURILabel = new System.Windows.Forms.Label();
			this.taskRegVersionText = new System.Windows.Forms.TextBox();
			this.taskRegURIText = new System.Windows.Forms.TextBox();
			this.taskRegSourceText = new System.Windows.Forms.TextBox();
			this.optionPanelHeaderLabel1 = new Microsoft.Win32.TaskScheduler.OptionPanels.OptionPanelHeaderLabel();
			this.label2 = new System.Windows.Forms.Label();
			this.taskDescText = new System.Windows.Forms.TextBox();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.taskEnabledCheck = new System.Windows.Forms.CheckBox();
			this.taskHiddenCheck = new System.Windows.Forms.CheckBox();
			this.helpProvider = new System.Windows.Forms.HelpProvider();
			this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
			this.taskUseUnifiedSchedulingEngineCheck = new System.Windows.Forms.CheckBox();
			this.taskVolatileCheck = new System.Windows.Forms.CheckBox();
			this.tableLayoutPanel1.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
			this.tableLayoutPanel1.Controls.Add(this.taskRegDocText, 1, 10);
			this.tableLayoutPanel1.Controls.Add(this.taskDescText, 1, 6);
			this.tableLayoutPanel1.Controls.Add(this.taskRegVersionText, 1, 9);
			this.tableLayoutPanel1.Controls.Add(this.taskUseUnifiedSchedulingEngineCheck, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.taskRegURIText, 1, 8);
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 6);
			this.tableLayoutPanel1.Controls.Add(this.taskRegSourceText, 1, 7);
			this.tableLayoutPanel1.Controls.Add(this.taskAuthorText, 1, 5);
			this.tableLayoutPanel1.Controls.Add(this.taskRegDocLabel, 0, 10);
			this.tableLayoutPanel1.Controls.Add(this.optionPanelHeaderLabel2, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.taskRegVersionLabel, 0, 9);
			this.tableLayoutPanel1.Controls.Add(this.taskVolatileCheck, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.taskRegURILabel, 0, 8);
			this.tableLayoutPanel1.Controls.Add(this.taskRegSourceLabel, 0, 7);
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.label3, 0, 5);
			this.tableLayoutPanel1.Controls.Add(this.optionPanelHeaderLabel1, 0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			// 
			// optionPanelHeaderLabel2
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.optionPanelHeaderLabel2, 2);
			resources.ApplyResources(this.optionPanelHeaderLabel2, "optionPanelHeaderLabel2");
			this.optionPanelHeaderLabel2.Name = "optionPanelHeaderLabel2";
			// 
			// taskRegDocText
			// 
			resources.ApplyResources(this.taskRegDocText, "taskRegDocText");
			this.taskRegDocText.Name = "taskRegDocText";
			this.taskRegDocText.Leave += new System.EventHandler(this.taskRegDocText_Leave);
			// 
			// taskRegDocLabel
			// 
			resources.ApplyResources(this.taskRegDocLabel, "taskRegDocLabel");
			this.taskRegDocLabel.Name = "taskRegDocLabel";
			// 
			// taskRegVersionLabel
			// 
			resources.ApplyResources(this.taskRegVersionLabel, "taskRegVersionLabel");
			this.taskRegVersionLabel.Name = "taskRegVersionLabel";
			// 
			// label3
			// 
			resources.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			// 
			// taskAuthorText
			// 
			resources.ApplyResources(this.taskAuthorText, "taskAuthorText");
			this.taskAuthorText.Name = "taskAuthorText";
			this.taskAuthorText.ReadOnly = true;
			// 
			// taskRegSourceLabel
			// 
			resources.ApplyResources(this.taskRegSourceLabel, "taskRegSourceLabel");
			this.taskRegSourceLabel.Name = "taskRegSourceLabel";
			// 
			// taskRegURILabel
			// 
			resources.ApplyResources(this.taskRegURILabel, "taskRegURILabel");
			this.taskRegURILabel.Name = "taskRegURILabel";
			// 
			// taskRegVersionText
			// 
			resources.ApplyResources(this.taskRegVersionText, "taskRegVersionText");
			this.taskRegVersionText.Name = "taskRegVersionText";
			this.taskRegVersionText.Validating += new System.ComponentModel.CancelEventHandler(this.taskRegVersionText_Validating);
			this.taskRegVersionText.Validated += new System.EventHandler(this.taskRegVersionText_Validated);
			// 
			// taskRegURIText
			// 
			resources.ApplyResources(this.taskRegURIText, "taskRegURIText");
			this.taskRegURIText.Name = "taskRegURIText";
			this.taskRegURIText.Validating += new System.ComponentModel.CancelEventHandler(this.taskRegURIText_Validating);
			this.taskRegURIText.Validated += new System.EventHandler(this.taskRegURIText_Validated);
			// 
			// taskRegSourceText
			// 
			resources.ApplyResources(this.taskRegSourceText, "taskRegSourceText");
			this.taskRegSourceText.Name = "taskRegSourceText";
			this.taskRegSourceText.Leave += new System.EventHandler(this.taskRegSourceText_Leave);
			// 
			// optionPanelHeaderLabel1
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.optionPanelHeaderLabel1, 2);
			resources.ApplyResources(this.optionPanelHeaderLabel1, "optionPanelHeaderLabel1");
			this.optionPanelHeaderLabel1.Name = "optionPanelHeaderLabel1";
			// 
			// label2
			// 
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			// 
			// taskDescText
			// 
			resources.ApplyResources(this.taskDescText, "taskDescText");
			this.taskDescText.Name = "taskDescText";
			this.taskDescText.Leave += new System.EventHandler(this.taskDescText_Leave);
			// 
			// flowLayoutPanel1
			// 
			resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
			this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel1, 2);
			this.flowLayoutPanel1.Controls.Add(this.taskEnabledCheck);
			this.flowLayoutPanel1.Controls.Add(this.taskHiddenCheck);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			// 
			// taskEnabledCheck
			// 
			resources.ApplyResources(this.taskEnabledCheck, "taskEnabledCheck");
			this.taskEnabledCheck.Name = "taskEnabledCheck";
			this.taskEnabledCheck.UseVisualStyleBackColor = true;
			this.taskEnabledCheck.CheckedChanged += new System.EventHandler(this.taskEnabledCheck_CheckedChanged);
			// 
			// taskHiddenCheck
			// 
			resources.ApplyResources(this.taskHiddenCheck, "taskHiddenCheck");
			this.taskHiddenCheck.Name = "taskHiddenCheck";
			this.taskHiddenCheck.UseVisualStyleBackColor = true;
			this.taskHiddenCheck.CheckedChanged += new System.EventHandler(this.taskHiddenCheck_CheckedChanged);
			// 
			// errorProvider
			// 
			this.errorProvider.ContainerControl = this;
			// 
			// taskUseUnifiedSchedulingEngineCheck
			// 
			resources.ApplyResources(this.taskUseUnifiedSchedulingEngineCheck, "taskUseUnifiedSchedulingEngineCheck");
			this.tableLayoutPanel1.SetColumnSpan(this.taskUseUnifiedSchedulingEngineCheck, 2);
			this.taskUseUnifiedSchedulingEngineCheck.Name = "taskUseUnifiedSchedulingEngineCheck";
			this.helpProvider.SetShowHelp(this.taskUseUnifiedSchedulingEngineCheck, ((bool)(resources.GetObject("taskUseUnifiedSchedulingEngineCheck.ShowHelp"))));
			this.taskUseUnifiedSchedulingEngineCheck.UseVisualStyleBackColor = true;
			// 
			// taskVolatileCheck
			// 
			resources.ApplyResources(this.taskVolatileCheck, "taskVolatileCheck");
			this.tableLayoutPanel1.SetColumnSpan(this.taskVolatileCheck, 2);
			this.taskVolatileCheck.Name = "taskVolatileCheck";
			this.taskVolatileCheck.UseVisualStyleBackColor = true;
			// 
			// GeneralOptionPanel
			// 
			resources.ApplyResources(this, "$this");
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "GeneralOptionPanel";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private OptionPanelHeaderLabel optionPanelHeaderLabel1;
		private OptionPanelHeaderLabel optionPanelHeaderLabel2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox taskAuthorText;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox taskDescText;
		private System.Windows.Forms.Label taskRegVersionLabel;
		private System.Windows.Forms.Label taskRegSourceLabel;
		private System.Windows.Forms.Label taskRegURILabel;
		private System.Windows.Forms.TextBox taskRegVersionText;
		private System.Windows.Forms.TextBox taskRegURIText;
		private System.Windows.Forms.TextBox taskRegSourceText;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.CheckBox taskEnabledCheck;
		private System.Windows.Forms.CheckBox taskHiddenCheck;
		private System.Windows.Forms.Label taskRegDocLabel;
		private System.Windows.Forms.TextBox taskRegDocText;
		private System.Windows.Forms.CheckBox taskUseUnifiedSchedulingEngineCheck;
		private System.Windows.Forms.HelpProvider helpProvider;
		private System.Windows.Forms.ErrorProvider errorProvider;
		private System.Windows.Forms.CheckBox taskVolatileCheck;
	}
}
