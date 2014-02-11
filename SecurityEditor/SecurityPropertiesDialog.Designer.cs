namespace SecurityEditor
{
	partial class SecurityPropertiesDialog
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
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.secTabPage = new System.Windows.Forms.TabPage();
			this.secProps = new SecurityEditor.SecurityProperties();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.applyBtn = new System.Windows.Forms.Button();
			this.cancelBtn = new System.Windows.Forms.Button();
			this.okBtn = new System.Windows.Forms.Button();
			this.tabControl1.SuspendLayout();
			this.secTabPage.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.secTabPage);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(7, 7);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(430, 492);
			this.tabControl1.TabIndex = 1;
			// 
			// secTabPage
			// 
			this.secTabPage.Controls.Add(this.secProps);
			this.secTabPage.Location = new System.Drawing.Point(4, 22);
			this.secTabPage.Name = "secTabPage";
			this.secTabPage.Padding = new System.Windows.Forms.Padding(7);
			this.secTabPage.Size = new System.Drawing.Size(422, 466);
			this.secTabPage.TabIndex = 1;
			this.secTabPage.Text = "Security";
			this.secTabPage.UseVisualStyleBackColor = true;
			// 
			// secProps
			// 
			this.secProps.Dock = System.Windows.Forms.DockStyle.Fill;
			this.secProps.Location = new System.Drawing.Point(7, 7);
			this.secProps.Name = "secProps";
			this.secProps.Size = new System.Drawing.Size(408, 452);
			this.secProps.TabIndex = 0;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.applyBtn, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.cancelBtn, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.okBtn, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(7, 499);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(430, 29);
			this.tableLayoutPanel1.TabIndex = 2;
			// 
			// applyBtn
			// 
			this.applyBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.applyBtn.Enabled = false;
			this.applyBtn.Location = new System.Drawing.Point(354, 7);
			this.applyBtn.Margin = new System.Windows.Forms.Padding(6, 7, 1, 0);
			this.applyBtn.Name = "applyBtn";
			this.applyBtn.Size = new System.Drawing.Size(75, 22);
			this.applyBtn.TabIndex = 2;
			this.applyBtn.Text = "&Apply";
			this.applyBtn.UseVisualStyleBackColor = true;
			this.applyBtn.Click += new System.EventHandler(this.applyBtn_Click);
			// 
			// cancelBtn
			// 
			this.cancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelBtn.Location = new System.Drawing.Point(273, 7);
			this.cancelBtn.Margin = new System.Windows.Forms.Padding(6, 7, 0, 0);
			this.cancelBtn.Name = "cancelBtn";
			this.cancelBtn.Size = new System.Drawing.Size(75, 22);
			this.cancelBtn.TabIndex = 1;
			this.cancelBtn.Text = "Cancel";
			this.cancelBtn.UseVisualStyleBackColor = true;
			this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
			// 
			// okBtn
			// 
			this.okBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.okBtn.Location = new System.Drawing.Point(192, 7);
			this.okBtn.Margin = new System.Windows.Forms.Padding(6, 7, 0, 0);
			this.okBtn.Name = "okBtn";
			this.okBtn.Size = new System.Drawing.Size(75, 22);
			this.okBtn.TabIndex = 0;
			this.okBtn.Text = "OK";
			this.okBtn.UseVisualStyleBackColor = true;
			this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
			// 
			// SecurityPropertiesDialog
			// 
			this.AcceptButton = this.okBtn;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelBtn;
			this.ClientSize = new System.Drawing.Size(444, 535);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.tableLayoutPanel1);
			this.MaximumSize = new System.Drawing.Size(460, 574);
			this.MinimumSize = new System.Drawing.Size(376, 458);
			this.Name = "SecurityPropertiesDialog";
			this.Padding = new System.Windows.Forms.Padding(7);
			this.Text = "SecurityPropertiesDialog";
			this.tabControl1.ResumeLayout(false);
			this.secTabPage.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage secTabPage;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Button applyBtn;
		private System.Windows.Forms.Button cancelBtn;
		private System.Windows.Forms.Button okBtn;
		internal SecurityProperties secProps;
	}
}