namespace TaskSchedulerConfig
{
	partial class Main
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
			this.localConfigList = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.cancelBtn = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			this.fixItemBtn = new System.Windows.Forms.Button();
			this.fixAllBtn = new System.Windows.Forms.Button();
			this.retestBtn = new System.Windows.Forms.Button();
			this.panel2 = new System.Windows.Forms.Panel();
			this.flowLayoutPanel1.SuspendLayout();
			this.flowLayoutPanel2.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// localConfigList
			// 
			this.localConfigList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
			this.localConfigList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.localConfigList.FullRowSelect = true;
			this.localConfigList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.localConfigList.Location = new System.Drawing.Point(10, 10);
			this.localConfigList.MultiSelect = false;
			this.localConfigList.Name = "localConfigList";
			this.localConfigList.Size = new System.Drawing.Size(444, 262);
			this.localConfigList.SmallImageList = this.imageList1;
			this.localConfigList.TabIndex = 0;
			this.localConfigList.UseCompatibleStateImageBehavior = false;
			this.localConfigList.View = System.Windows.Forms.View.Details;
			this.localConfigList.SelectedIndexChanged += new System.EventHandler(this.localConfigList_SelectedIndexChanged);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Width = 440;
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "109_AllAnnotations_Error_16x16_72.png");
			this.imageList1.Images.SetKeyName(1, "109_AllAnnotations_Default_16x16_72.png");
			this.imageList1.Images.SetKeyName(2, "109_AllAnnotations_Info_16x16_72.png");
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.AutoSize = true;
			this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flowLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
			this.flowLayoutPanel1.Controls.Add(this.cancelBtn);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 283);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(549, 43);
			this.flowLayoutPanel1.TabIndex = 1;
			// 
			// cancelBtn
			// 
			this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelBtn.Location = new System.Drawing.Point(464, 10);
			this.cancelBtn.Margin = new System.Windows.Forms.Padding(10);
			this.cancelBtn.Name = "cancelBtn";
			this.cancelBtn.Size = new System.Drawing.Size(75, 23);
			this.cancelBtn.TabIndex = 0;
			this.cancelBtn.Text = "Close";
			this.cancelBtn.UseVisualStyleBackColor = true;
			this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 282);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(549, 1);
			this.panel1.TabIndex = 2;
			// 
			// flowLayoutPanel2
			// 
			this.flowLayoutPanel2.AutoSize = true;
			this.flowLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flowLayoutPanel2.Controls.Add(this.fixItemBtn);
			this.flowLayoutPanel2.Controls.Add(this.fixAllBtn);
			this.flowLayoutPanel2.Controls.Add(this.retestBtn);
			this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Right;
			this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowLayoutPanel2.Location = new System.Drawing.Point(454, 0);
			this.flowLayoutPanel2.Name = "flowLayoutPanel2";
			this.flowLayoutPanel2.Size = new System.Drawing.Size(95, 282);
			this.flowLayoutPanel2.TabIndex = 3;
			// 
			// fixItemBtn
			// 
			this.fixItemBtn.Enabled = false;
			this.fixItemBtn.Location = new System.Drawing.Point(10, 10);
			this.fixItemBtn.Margin = new System.Windows.Forms.Padding(10, 10, 10, 7);
			this.fixItemBtn.Name = "fixItemBtn";
			this.fixItemBtn.Size = new System.Drawing.Size(75, 23);
			this.fixItemBtn.TabIndex = 0;
			this.fixItemBtn.Text = "Fix";
			this.fixItemBtn.UseVisualStyleBackColor = true;
			this.fixItemBtn.Click += new System.EventHandler(this.fixItemBtn_Click);
			// 
			// fixAllBtn
			// 
			this.fixAllBtn.Enabled = false;
			this.fixAllBtn.Location = new System.Drawing.Point(10, 40);
			this.fixAllBtn.Margin = new System.Windows.Forms.Padding(10, 0, 10, 7);
			this.fixAllBtn.Name = "fixAllBtn";
			this.fixAllBtn.Size = new System.Drawing.Size(75, 23);
			this.fixAllBtn.TabIndex = 1;
			this.fixAllBtn.Text = "Fix All";
			this.fixAllBtn.UseVisualStyleBackColor = true;
			this.fixAllBtn.Click += new System.EventHandler(this.fixAllBtn_Click);
			// 
			// retestBtn
			// 
			this.retestBtn.Location = new System.Drawing.Point(10, 70);
			this.retestBtn.Margin = new System.Windows.Forms.Padding(10, 0, 10, 10);
			this.retestBtn.Name = "retestBtn";
			this.retestBtn.Size = new System.Drawing.Size(75, 23);
			this.retestBtn.TabIndex = 2;
			this.retestBtn.Text = "Retest";
			this.retestBtn.UseVisualStyleBackColor = true;
			this.retestBtn.Click += new System.EventHandler(this.retestBtn_Click);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.localConfigList);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Padding = new System.Windows.Forms.Padding(10, 10, 0, 10);
			this.panel2.Size = new System.Drawing.Size(454, 282);
			this.panel2.TabIndex = 4;
			// 
			// Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.CancelButton = this.cancelBtn;
			this.ClientSize = new System.Drawing.Size(549, 326);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.flowLayoutPanel2);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.flowLayoutPanel1);
			this.Name = "Main";
			this.Text = "Task Scheduler Configuration";
			this.Load += new System.EventHandler(this.Main_Load);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel2.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView localConfigList;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Button cancelBtn;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
		private System.Windows.Forms.Button fixItemBtn;
		private System.Windows.Forms.Button fixAllBtn;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button retestBtn;
	}
}