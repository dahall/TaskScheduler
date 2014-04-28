namespace Microsoft.Win32.TaskScheduler.UIComponents
{
	partial class EventTriggerUI
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EventTriggerUI));
			this.onEventBasicPanel = new System.Windows.Forms.Panel();
			this.onEventLogLabel = new System.Windows.Forms.Label();
			this.onEventIdText = new System.Windows.Forms.TextBox();
			this.onEventSourceLabel = new System.Windows.Forms.Label();
			this.onEventSourceCombo = new System.Windows.Forms.ComboBox();
			this.onEventLogCombo = new System.Windows.Forms.ComboBox();
			this.onEventIdLabel = new System.Windows.Forms.Label();
			this.onEventCustomText = new System.Windows.Forms.TextBox();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.eventCustomRadio = new System.Windows.Forms.RadioButton();
			this.eventBasicRadio = new System.Windows.Forms.RadioButton();
			this.onEventBasicPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// onEventBasicPanel
			// 
			resources.ApplyResources(this.onEventBasicPanel, "onEventBasicPanel");
			this.onEventBasicPanel.Controls.Add(this.onEventLogLabel);
			this.onEventBasicPanel.Controls.Add(this.onEventIdText);
			this.onEventBasicPanel.Controls.Add(this.onEventSourceLabel);
			this.onEventBasicPanel.Controls.Add(this.onEventSourceCombo);
			this.onEventBasicPanel.Controls.Add(this.onEventLogCombo);
			this.onEventBasicPanel.Controls.Add(this.onEventIdLabel);
			this.onEventBasicPanel.Name = "onEventBasicPanel";
			// 
			// onEventLogLabel
			// 
			resources.ApplyResources(this.onEventLogLabel, "onEventLogLabel");
			this.onEventLogLabel.Name = "onEventLogLabel";
			// 
			// onEventIdText
			// 
			resources.ApplyResources(this.onEventIdText, "onEventIdText");
			this.onEventIdText.Name = "onEventIdText";
			this.onEventIdText.TextChanged += new System.EventHandler(this.onEventIdText_TextChanged);
			this.onEventIdText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.onEventIdText_KeyPress);
			this.onEventIdText.Leave += new System.EventHandler(this.onEventTextBox_Leave);
			// 
			// onEventSourceLabel
			// 
			resources.ApplyResources(this.onEventSourceLabel, "onEventSourceLabel");
			this.onEventSourceLabel.Name = "onEventSourceLabel";
			// 
			// onEventSourceCombo
			// 
			resources.ApplyResources(this.onEventSourceCombo, "onEventSourceCombo");
			this.onEventSourceCombo.Name = "onEventSourceCombo";
			this.onEventSourceCombo.Leave += new System.EventHandler(this.onEventTextBox_Leave);
			// 
			// onEventLogCombo
			// 
			resources.ApplyResources(this.onEventLogCombo, "onEventLogCombo");
			this.onEventLogCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.onEventLogCombo.FormattingEnabled = true;
			this.onEventLogCombo.Name = "onEventLogCombo";
			this.onEventLogCombo.SelectedIndexChanged += new System.EventHandler(this.onEventLogCombo_SelectedIndexChanged);
			// 
			// onEventIdLabel
			// 
			resources.ApplyResources(this.onEventIdLabel, "onEventIdLabel");
			this.onEventIdLabel.Name = "onEventIdLabel";
			// 
			// onEventCustomText
			// 
			this.onEventCustomText.AcceptsReturn = true;
			this.onEventCustomText.AcceptsTab = true;
			resources.ApplyResources(this.onEventCustomText, "onEventCustomText");
			this.onEventCustomText.Name = "onEventCustomText";
			this.onEventCustomText.Leave += new System.EventHandler(this.onEventCustomText_Leave);
			// 
			// groupBox5
			// 
			resources.ApplyResources(this.groupBox5, "groupBox5");
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.TabStop = false;
			// 
			// eventCustomRadio
			// 
			resources.ApplyResources(this.eventCustomRadio, "eventCustomRadio");
			this.eventCustomRadio.Name = "eventCustomRadio";
			this.eventCustomRadio.UseVisualStyleBackColor = true;
			this.eventCustomRadio.CheckedChanged += new System.EventHandler(this.eventBasicRadio_CheckedChanged);
			// 
			// eventBasicRadio
			// 
			resources.ApplyResources(this.eventBasicRadio, "eventBasicRadio");
			this.eventBasicRadio.Checked = true;
			this.eventBasicRadio.Name = "eventBasicRadio";
			this.eventBasicRadio.TabStop = true;
			this.eventBasicRadio.UseVisualStyleBackColor = true;
			this.eventBasicRadio.CheckedChanged += new System.EventHandler(this.eventBasicRadio_CheckedChanged);
			// 
			// EventTriggerUI
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.onEventBasicPanel);
			this.Controls.Add(this.onEventCustomText);
			this.Controls.Add(this.groupBox5);
			this.Controls.Add(this.eventCustomRadio);
			this.Controls.Add(this.eventBasicRadio);
			this.MinimumSize = new System.Drawing.Size(516, 96);
			this.Name = "EventTriggerUI";
			this.onEventBasicPanel.ResumeLayout(false);
			this.onEventBasicPanel.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel onEventBasicPanel;
		private System.Windows.Forms.Label onEventLogLabel;
		private System.Windows.Forms.TextBox onEventIdText;
		private System.Windows.Forms.Label onEventSourceLabel;
		private System.Windows.Forms.ComboBox onEventSourceCombo;
		private System.Windows.Forms.ComboBox onEventLogCombo;
		private System.Windows.Forms.Label onEventIdLabel;
		private System.Windows.Forms.TextBox onEventCustomText;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.RadioButton eventCustomRadio;
		private System.Windows.Forms.RadioButton eventBasicRadio;
	}
}
