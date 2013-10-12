namespace Microsoft.Win32.TaskScheduler
{
	partial class EventViewerControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EventViewerControl));
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.infoLink = new System.Windows.Forms.LinkLabel();
			this.userText = new System.Windows.Forms.TextBox();
			this.opCodeText = new System.Windows.Forms.TextBox();
			this.computerText = new System.Windows.Forms.TextBox();
			this.keywordsText = new System.Windows.Forms.TextBox();
			this.loggedText = new System.Windows.Forms.TextBox();
			this.taskCategoryText = new System.Windows.Forms.TextBox();
			this.logNameText = new System.Windows.Forms.TextBox();
			this.sourceText = new System.Windows.Forms.TextBox();
			this.eventIdText = new System.Windows.Forms.TextBox();
			this.levelText = new System.Windows.Forms.TextBox();
			this.label18 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.label21 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.detailsText = new System.Windows.Forms.TextBox();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.xmlViewRadio = new System.Windows.Forms.RadioButton();
			this.textViewRadio = new System.Windows.Forms.RadioButton();
			this.panel1 = new System.Windows.Forms.Panel();
			this.htmlText = new System.Windows.Forms.WebBrowser();
			this.htmlContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.htmlContextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			resources.ApplyResources(this.tabControl1, "tabControl1");
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			// 
			// tabPage1
			// 
			resources.ApplyResources(this.tabPage1, "tabPage1");
			this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
			this.tabPage1.Controls.Add(this.infoLink);
			this.tabPage1.Controls.Add(this.userText);
			this.tabPage1.Controls.Add(this.opCodeText);
			this.tabPage1.Controls.Add(this.computerText);
			this.tabPage1.Controls.Add(this.keywordsText);
			this.tabPage1.Controls.Add(this.loggedText);
			this.tabPage1.Controls.Add(this.taskCategoryText);
			this.tabPage1.Controls.Add(this.logNameText);
			this.tabPage1.Controls.Add(this.sourceText);
			this.tabPage1.Controls.Add(this.eventIdText);
			this.tabPage1.Controls.Add(this.levelText);
			this.tabPage1.Controls.Add(this.label18);
			this.tabPage1.Controls.Add(this.label14);
			this.tabPage1.Controls.Add(this.label17);
			this.tabPage1.Controls.Add(this.label21);
			this.tabPage1.Controls.Add(this.label13);
			this.tabPage1.Controls.Add(this.label10);
			this.tabPage1.Controls.Add(this.label9);
			this.tabPage1.Controls.Add(this.label6);
			this.tabPage1.Controls.Add(this.label5);
			this.tabPage1.Controls.Add(this.label2);
			this.tabPage1.Controls.Add(this.label1);
			this.tabPage1.Controls.Add(this.detailsText);
			this.tabPage1.Name = "tabPage1";
			// 
			// infoLink
			// 
			resources.ApplyResources(this.infoLink, "infoLink");
			this.infoLink.Name = "infoLink";
			this.infoLink.TabStop = true;
			this.infoLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.infoLink_LinkClicked);
			// 
			// userText
			// 
			resources.ApplyResources(this.userText, "userText");
			this.userText.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.userText.Name = "userText";
			this.userText.ReadOnly = true;
			// 
			// opCodeText
			// 
			resources.ApplyResources(this.opCodeText, "opCodeText");
			this.opCodeText.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.opCodeText.Name = "opCodeText";
			this.opCodeText.ReadOnly = true;
			// 
			// computerText
			// 
			resources.ApplyResources(this.computerText, "computerText");
			this.computerText.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.computerText.Name = "computerText";
			this.computerText.ReadOnly = true;
			// 
			// keywordsText
			// 
			resources.ApplyResources(this.keywordsText, "keywordsText");
			this.keywordsText.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.keywordsText.Name = "keywordsText";
			this.keywordsText.ReadOnly = true;
			// 
			// loggedText
			// 
			resources.ApplyResources(this.loggedText, "loggedText");
			this.loggedText.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.loggedText.Name = "loggedText";
			this.loggedText.ReadOnly = true;
			// 
			// taskCategoryText
			// 
			resources.ApplyResources(this.taskCategoryText, "taskCategoryText");
			this.taskCategoryText.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.taskCategoryText.Name = "taskCategoryText";
			this.taskCategoryText.ReadOnly = true;
			// 
			// logNameText
			// 
			resources.ApplyResources(this.logNameText, "logNameText");
			this.logNameText.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.logNameText.Name = "logNameText";
			this.logNameText.ReadOnly = true;
			// 
			// sourceText
			// 
			resources.ApplyResources(this.sourceText, "sourceText");
			this.sourceText.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.sourceText.Name = "sourceText";
			this.sourceText.ReadOnly = true;
			// 
			// eventIdText
			// 
			resources.ApplyResources(this.eventIdText, "eventIdText");
			this.eventIdText.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.eventIdText.Name = "eventIdText";
			this.eventIdText.ReadOnly = true;
			// 
			// levelText
			// 
			resources.ApplyResources(this.levelText, "levelText");
			this.levelText.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.levelText.Name = "levelText";
			this.levelText.ReadOnly = true;
			// 
			// label18
			// 
			resources.ApplyResources(this.label18, "label18");
			this.label18.Name = "label18";
			// 
			// label14
			// 
			resources.ApplyResources(this.label14, "label14");
			this.label14.Name = "label14";
			// 
			// label17
			// 
			resources.ApplyResources(this.label17, "label17");
			this.label17.Name = "label17";
			// 
			// label21
			// 
			resources.ApplyResources(this.label21, "label21");
			this.label21.Name = "label21";
			// 
			// label13
			// 
			resources.ApplyResources(this.label13, "label13");
			this.label13.Name = "label13";
			// 
			// label10
			// 
			resources.ApplyResources(this.label10, "label10");
			this.label10.Name = "label10";
			// 
			// label9
			// 
			resources.ApplyResources(this.label9, "label9");
			this.label9.Name = "label9";
			// 
			// label6
			// 
			resources.ApplyResources(this.label6, "label6");
			this.label6.Name = "label6";
			// 
			// label5
			// 
			resources.ApplyResources(this.label5, "label5");
			this.label5.Name = "label5";
			// 
			// label2
			// 
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// detailsText
			// 
			resources.ApplyResources(this.detailsText, "detailsText");
			this.detailsText.Name = "detailsText";
			this.detailsText.ReadOnly = true;
			// 
			// tabPage2
			// 
			resources.ApplyResources(this.tabPage2, "tabPage2");
			this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
			this.tabPage2.Controls.Add(this.xmlViewRadio);
			this.tabPage2.Controls.Add(this.textViewRadio);
			this.tabPage2.Controls.Add(this.panel1);
			this.tabPage2.Name = "tabPage2";
			// 
			// xmlViewRadio
			// 
			resources.ApplyResources(this.xmlViewRadio, "xmlViewRadio");
			this.xmlViewRadio.Name = "xmlViewRadio";
			this.xmlViewRadio.UseVisualStyleBackColor = true;
			this.xmlViewRadio.CheckedChanged += new System.EventHandler(this.xmlViewRadio_CheckedChanged);
			// 
			// textViewRadio
			// 
			resources.ApplyResources(this.textViewRadio, "textViewRadio");
			this.textViewRadio.Checked = true;
			this.textViewRadio.Name = "textViewRadio";
			this.textViewRadio.TabStop = true;
			this.textViewRadio.UseVisualStyleBackColor = true;
			this.textViewRadio.CheckedChanged += new System.EventHandler(this.textViewRadio_CheckedChanged);
			// 
			// panel1
			// 
			resources.ApplyResources(this.panel1, "panel1");
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel1.Controls.Add(this.htmlText);
			this.panel1.Name = "panel1";
			// 
			// htmlText
			// 
			resources.ApplyResources(this.htmlText, "htmlText");
			this.htmlText.AllowWebBrowserDrop = false;
			this.htmlText.ContextMenuStrip = this.htmlContextMenu;
			this.htmlText.IsWebBrowserContextMenuEnabled = false;
			this.htmlText.Name = "htmlText";
			this.htmlText.ScriptErrorsSuppressed = true;
			this.htmlText.WebBrowserShortcutsEnabled = false;
			// 
			// htmlContextMenu
			// 
			resources.ApplyResources(this.htmlContextMenu, "htmlContextMenu");
			this.htmlContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.selectAllToolStripMenuItem});
			this.htmlContextMenu.Name = "htmlContextMenu";
			// 
			// copyToolStripMenuItem
			// 
			resources.ApplyResources(this.copyToolStripMenuItem, "copyToolStripMenuItem");
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
			// 
			// selectAllToolStripMenuItem
			// 
			resources.ApplyResources(this.selectAllToolStripMenuItem, "selectAllToolStripMenuItem");
			this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
			this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
			// 
			// EventViewerControl
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.Controls.Add(this.tabControl1);
			this.Name = "EventViewerControl";
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.htmlContextMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.LinkLabel infoLink;
		private System.Windows.Forms.TextBox userText;
		private System.Windows.Forms.TextBox opCodeText;
		private System.Windows.Forms.TextBox computerText;
		private System.Windows.Forms.TextBox keywordsText;
		private System.Windows.Forms.TextBox loggedText;
		private System.Windows.Forms.TextBox taskCategoryText;
		private System.Windows.Forms.TextBox logNameText;
		private System.Windows.Forms.TextBox sourceText;
		private System.Windows.Forms.TextBox eventIdText;
		private System.Windows.Forms.TextBox levelText;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox detailsText;
		private System.Windows.Forms.WebBrowser htmlText;
		private System.Windows.Forms.ContextMenuStrip htmlContextMenu;
		private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
		private System.Windows.Forms.RadioButton xmlViewRadio;
		private System.Windows.Forms.RadioButton textViewRadio;
		private System.Windows.Forms.Panel panel1;
	}
}
