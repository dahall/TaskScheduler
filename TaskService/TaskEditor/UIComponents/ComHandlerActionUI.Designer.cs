namespace Microsoft.Win32.TaskScheduler.UIComponents
{
	partial class ComHandlerActionUI
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ComHandlerActionUI));
			this.getCLSIDButton = new System.Windows.Forms.Button();
			this.clsidMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.lookupCLSIDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.manuallyEnterCLSIDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.comDataText = new System.Windows.Forms.TextBox();
			this.comDataLabel = new System.Windows.Forms.Label();
			this.comCLSIDText = new System.Windows.Forms.TextBox();
			this.comCLSIDLabel = new System.Windows.Forms.Label();
			this.comIntroLabel = new System.Windows.Forms.Label();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
			this.clsidMenuStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
			this.SuspendLayout();
			// 
			// getCLSIDButton
			// 
			resources.ApplyResources(this.getCLSIDButton, "getCLSIDButton");
			this.getCLSIDButton.CausesValidation = false;
			this.getCLSIDButton.ContextMenuStrip = this.clsidMenuStrip;
			this.getCLSIDButton.Name = "getCLSIDButton";
			this.getCLSIDButton.UseVisualStyleBackColor = true;
			this.getCLSIDButton.Click += new System.EventHandler(this.getCLSIDButton_Click);
			// 
			// clsidMenuStrip
			// 
			this.clsidMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lookupCLSIDToolStripMenuItem,
            this.manuallyEnterCLSIDToolStripMenuItem});
			this.clsidMenuStrip.Name = "clsidMenuStrip";
			this.clsidMenuStrip.ShowImageMargin = false;
			resources.ApplyResources(this.clsidMenuStrip, "clsidMenuStrip");
			// 
			// lookupCLSIDToolStripMenuItem
			// 
			this.lookupCLSIDToolStripMenuItem.Name = "lookupCLSIDToolStripMenuItem";
			resources.ApplyResources(this.lookupCLSIDToolStripMenuItem, "lookupCLSIDToolStripMenuItem");
			this.lookupCLSIDToolStripMenuItem.Click += new System.EventHandler(this.lookupCLSIDToolStripMenuItem_Click);
			// 
			// manuallyEnterCLSIDToolStripMenuItem
			// 
			this.manuallyEnterCLSIDToolStripMenuItem.Name = "manuallyEnterCLSIDToolStripMenuItem";
			resources.ApplyResources(this.manuallyEnterCLSIDToolStripMenuItem, "manuallyEnterCLSIDToolStripMenuItem");
			this.manuallyEnterCLSIDToolStripMenuItem.Click += new System.EventHandler(this.manuallyEnterCLSIDToolStripMenuItem_Click);
			// 
			// comDataText
			// 
			resources.ApplyResources(this.comDataText, "comDataText");
			this.comDataText.Name = "comDataText";
			// 
			// comDataLabel
			// 
			resources.ApplyResources(this.comDataLabel, "comDataLabel");
			this.comDataLabel.Name = "comDataLabel";
			// 
			// comCLSIDText
			// 
			resources.ApplyResources(this.comCLSIDText, "comCLSIDText");
			this.comCLSIDText.Name = "comCLSIDText";
			this.comCLSIDText.ReadOnly = true;
			this.comCLSIDText.Validating += new System.ComponentModel.CancelEventHandler(this.comCLSIDText_Validating);
			// 
			// comCLSIDLabel
			// 
			resources.ApplyResources(this.comCLSIDLabel, "comCLSIDLabel");
			this.comCLSIDLabel.Name = "comCLSIDLabel";
			// 
			// comIntroLabel
			// 
			resources.ApplyResources(this.comIntroLabel, "comIntroLabel");
			this.comIntroLabel.Name = "comIntroLabel";
			// 
			// errorProvider
			// 
			this.errorProvider.ContainerControl = this;
			// 
			// ComHandlerActionUI
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.comDataText);
			this.Controls.Add(this.getCLSIDButton);
			this.Controls.Add(this.comDataLabel);
			this.Controls.Add(this.comIntroLabel);
			this.Controls.Add(this.comCLSIDLabel);
			this.Controls.Add(this.comCLSIDText);
			this.MinimumSize = new System.Drawing.Size(343, 89);
			this.Name = "ComHandlerActionUI";
			this.clsidMenuStrip.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button getCLSIDButton;
		private System.Windows.Forms.TextBox comDataText;
		private System.Windows.Forms.Label comDataLabel;
		private System.Windows.Forms.TextBox comCLSIDText;
		private System.Windows.Forms.Label comCLSIDLabel;
		private System.Windows.Forms.Label comIntroLabel;
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.ContextMenuStrip clsidMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem lookupCLSIDToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem manuallyEnterCLSIDToolStripMenuItem;
		private System.Windows.Forms.ErrorProvider errorProvider;
	}
}
