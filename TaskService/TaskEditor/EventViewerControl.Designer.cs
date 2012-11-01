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
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.MinimumSize = new System.Drawing.Size(0, 140);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(533, 269);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.AutoScroll = true;
			this.tabPage1.AutoScrollMargin = new System.Drawing.Size(0, 5);
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
			this.tabPage1.Location = new System.Drawing.Point(4, 24);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(525, 241);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "General";
			// 
			// infoLink
			// 
			this.infoLink.AutoSize = true;
			this.infoLink.Location = new System.Drawing.Point(115, 215);
			this.infoLink.Name = "infoLink";
			this.infoLink.Size = new System.Drawing.Size(125, 15);
			this.infoLink.TabIndex = 14;
			this.infoLink.TabStop = true;
			this.infoLink.Text = "Event Log Online Help";
			this.infoLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.infoLink_LinkClicked);
			// 
			// userText
			// 
			this.userText.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.userText.Location = new System.Drawing.Point(118, 171);
			this.userText.Name = "userText";
			this.userText.ReadOnly = true;
			this.userText.Size = new System.Drawing.Size(149, 16);
			this.userText.TabIndex = 10;
			// 
			// opCodeText
			// 
			this.opCodeText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.opCodeText.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.opCodeText.Location = new System.Drawing.Point(118, 193);
			this.opCodeText.Name = "opCodeText";
			this.opCodeText.ReadOnly = true;
			this.opCodeText.Size = new System.Drawing.Size(397, 16);
			this.opCodeText.TabIndex = 12;
			// 
			// computerText
			// 
			this.computerText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.computerText.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.computerText.Location = new System.Drawing.Point(364, 171);
			this.computerText.Name = "computerText";
			this.computerText.ReadOnly = true;
			this.computerText.Size = new System.Drawing.Size(151, 16);
			this.computerText.TabIndex = 22;
			// 
			// keywordsText
			// 
			this.keywordsText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.keywordsText.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.keywordsText.Location = new System.Drawing.Point(364, 149);
			this.keywordsText.Name = "keywordsText";
			this.keywordsText.ReadOnly = true;
			this.keywordsText.Size = new System.Drawing.Size(151, 16);
			this.keywordsText.TabIndex = 20;
			// 
			// loggedText
			// 
			this.loggedText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.loggedText.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.loggedText.Location = new System.Drawing.Point(364, 105);
			this.loggedText.Name = "loggedText";
			this.loggedText.ReadOnly = true;
			this.loggedText.Size = new System.Drawing.Size(151, 16);
			this.loggedText.TabIndex = 16;
			// 
			// taskCategoryText
			// 
			this.taskCategoryText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.taskCategoryText.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.taskCategoryText.Location = new System.Drawing.Point(364, 127);
			this.taskCategoryText.Name = "taskCategoryText";
			this.taskCategoryText.ReadOnly = true;
			this.taskCategoryText.Size = new System.Drawing.Size(151, 16);
			this.taskCategoryText.TabIndex = 18;
			// 
			// logNameText
			// 
			this.logNameText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.logNameText.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.logNameText.Location = new System.Drawing.Point(118, 83);
			this.logNameText.Name = "logNameText";
			this.logNameText.ReadOnly = true;
			this.logNameText.Size = new System.Drawing.Size(397, 16);
			this.logNameText.TabIndex = 2;
			// 
			// sourceText
			// 
			this.sourceText.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.sourceText.Location = new System.Drawing.Point(118, 105);
			this.sourceText.Name = "sourceText";
			this.sourceText.ReadOnly = true;
			this.sourceText.Size = new System.Drawing.Size(149, 16);
			this.sourceText.TabIndex = 4;
			// 
			// eventIdText
			// 
			this.eventIdText.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.eventIdText.Location = new System.Drawing.Point(118, 127);
			this.eventIdText.Name = "eventIdText";
			this.eventIdText.ReadOnly = true;
			this.eventIdText.Size = new System.Drawing.Size(149, 16);
			this.eventIdText.TabIndex = 6;
			// 
			// levelText
			// 
			this.levelText.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.levelText.Location = new System.Drawing.Point(118, 149);
			this.levelText.Name = "levelText";
			this.levelText.ReadOnly = true;
			this.levelText.Size = new System.Drawing.Size(149, 16);
			this.levelText.TabIndex = 8;
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.Location = new System.Drawing.Point(273, 149);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(61, 15);
			this.label18.TabIndex = 19;
			this.label18.Text = "&Keywords:";
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(273, 105);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(50, 15);
			this.label14.TabIndex = 15;
			this.label14.Text = "Logge&d:";
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Location = new System.Drawing.Point(273, 171);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(64, 15);
			this.label17.TabIndex = 21;
			this.label17.Text = "Compute&r:";
			// 
			// label21
			// 
			this.label21.AutoSize = true;
			this.label21.Location = new System.Drawing.Point(8, 83);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(65, 15);
			this.label21.TabIndex = 1;
			this.label21.Text = "Log Na&me:";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(273, 127);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(85, 15);
			this.label13.TabIndex = 17;
			this.label13.Text = "Task Categor&y:";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(8, 105);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(46, 15);
			this.label10.TabIndex = 3;
			this.label10.Text = "&Source:";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(8, 127);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(53, 15);
			this.label9.TabIndex = 5;
			this.label9.Text = "&Event ID:";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(8, 149);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(37, 15);
			this.label6.TabIndex = 7;
			this.label6.Text = "&Level:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(8, 171);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(33, 15);
			this.label5.TabIndex = 9;
			this.label5.Text = "&User:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(8, 193);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(54, 15);
			this.label2.TabIndex = 11;
			this.label2.Text = "&OpCode:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 215);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(104, 15);
			this.label1.TabIndex = 13;
			this.label1.Text = "More &Information:";
			// 
			// detailsText
			// 
			this.detailsText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.detailsText.Location = new System.Drawing.Point(11, 11);
			this.detailsText.MinimumSize = new System.Drawing.Size(4, 50);
			this.detailsText.Multiline = true;
			this.detailsText.Name = "detailsText";
			this.detailsText.ReadOnly = true;
			this.detailsText.Size = new System.Drawing.Size(504, 56);
			this.detailsText.TabIndex = 0;
			// 
			// tabPage2
			// 
			this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
			this.tabPage2.Controls.Add(this.xmlViewRadio);
			this.tabPage2.Controls.Add(this.textViewRadio);
			this.tabPage2.Controls.Add(this.panel1);
			this.tabPage2.Location = new System.Drawing.Point(4, 24);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(525, 241);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Details";
			// 
			// xmlViewRadio
			// 
			this.xmlViewRadio.AutoSize = true;
			this.xmlViewRadio.Location = new System.Drawing.Point(134, 11);
			this.xmlViewRadio.Name = "xmlViewRadio";
			this.xmlViewRadio.Size = new System.Drawing.Size(77, 19);
			this.xmlViewRadio.TabIndex = 1;
			this.xmlViewRadio.Text = "&XML View";
			this.xmlViewRadio.UseVisualStyleBackColor = true;
			this.xmlViewRadio.CheckedChanged += new System.EventHandler(this.xmlViewRadio_CheckedChanged);
			// 
			// textViewRadio
			// 
			this.textViewRadio.AutoSize = true;
			this.textViewRadio.Checked = true;
			this.textViewRadio.Location = new System.Drawing.Point(11, 11);
			this.textViewRadio.Name = "textViewRadio";
			this.textViewRadio.Size = new System.Drawing.Size(95, 19);
			this.textViewRadio.TabIndex = 0;
			this.textViewRadio.TabStop = true;
			this.textViewRadio.Text = "Frie&ndly View";
			this.textViewRadio.UseVisualStyleBackColor = true;
			this.textViewRadio.CheckedChanged += new System.EventHandler(this.textViewRadio_CheckedChanged);
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel1.Controls.Add(this.htmlText);
			this.panel1.Location = new System.Drawing.Point(11, 37);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(504, 194);
			this.panel1.TabIndex = 2;
			// 
			// htmlText
			// 
			this.htmlText.AllowWebBrowserDrop = false;
			this.htmlText.ContextMenuStrip = this.htmlContextMenu;
			this.htmlText.Dock = System.Windows.Forms.DockStyle.Fill;
			this.htmlText.IsWebBrowserContextMenuEnabled = false;
			this.htmlText.Location = new System.Drawing.Point(0, 0);
			this.htmlText.MinimumSize = new System.Drawing.Size(20, 20);
			this.htmlText.Name = "htmlText";
			this.htmlText.ScriptErrorsSuppressed = true;
			this.htmlText.Size = new System.Drawing.Size(500, 190);
			this.htmlText.TabIndex = 0;
			this.htmlText.WebBrowserShortcutsEnabled = false;
			// 
			// htmlContextMenu
			// 
			this.htmlContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.selectAllToolStripMenuItem});
			this.htmlContextMenu.Name = "htmlContextMenu";
			this.htmlContextMenu.Size = new System.Drawing.Size(123, 48);
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
			this.copyToolStripMenuItem.Text = "&Copy";
			this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
			// 
			// selectAllToolStripMenuItem
			// 
			this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
			this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
			this.selectAllToolStripMenuItem.Text = "Select &All";
			this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
			// 
			// EventViewerControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.Controls.Add(this.tabControl1);
			this.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.Name = "EventViewerControl";
			this.Size = new System.Drawing.Size(533, 269);
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
