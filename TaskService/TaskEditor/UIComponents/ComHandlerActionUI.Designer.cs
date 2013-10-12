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
			this.comDataText = new System.Windows.Forms.TextBox();
			this.comDataLabel = new System.Windows.Forms.Label();
			this.comCLSIDText = new System.Windows.Forms.TextBox();
			this.comCLSIDLabel = new System.Windows.Forms.Label();
			this.comIntroLabel = new System.Windows.Forms.Label();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// getCLSIDButton
			// 
			resources.ApplyResources(this.getCLSIDButton, "getCLSIDButton");
			this.getCLSIDButton.Name = "getCLSIDButton";
			this.toolTip.SetToolTip(this.getCLSIDButton, resources.GetString("getCLSIDButton.ToolTip"));
			this.getCLSIDButton.UseVisualStyleBackColor = true;
			this.getCLSIDButton.Click += new System.EventHandler(this.getCLSIDButton_Click);
			// 
			// comDataText
			// 
			resources.ApplyResources(this.comDataText, "comDataText");
			this.comDataText.Name = "comDataText";
			this.toolTip.SetToolTip(this.comDataText, resources.GetString("comDataText.ToolTip"));
			// 
			// comDataLabel
			// 
			resources.ApplyResources(this.comDataLabel, "comDataLabel");
			this.comDataLabel.Name = "comDataLabel";
			this.toolTip.SetToolTip(this.comDataLabel, resources.GetString("comDataLabel.ToolTip"));
			// 
			// comCLSIDText
			// 
			resources.ApplyResources(this.comCLSIDText, "comCLSIDText");
			this.comCLSIDText.Name = "comCLSIDText";
			this.comCLSIDText.ReadOnly = true;
			this.toolTip.SetToolTip(this.comCLSIDText, resources.GetString("comCLSIDText.ToolTip"));
			// 
			// comCLSIDLabel
			// 
			resources.ApplyResources(this.comCLSIDLabel, "comCLSIDLabel");
			this.comCLSIDLabel.Name = "comCLSIDLabel";
			this.toolTip.SetToolTip(this.comCLSIDLabel, resources.GetString("comCLSIDLabel.ToolTip"));
			// 
			// comIntroLabel
			// 
			resources.ApplyResources(this.comIntroLabel, "comIntroLabel");
			this.comIntroLabel.Name = "comIntroLabel";
			this.toolTip.SetToolTip(this.comIntroLabel, resources.GetString("comIntroLabel.ToolTip"));
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
			this.toolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
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
	}
}
