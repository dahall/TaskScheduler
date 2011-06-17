namespace Microsoft.Win32.TaskScheduler
{
	partial class TaskListView
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
			this.listView1 = new System.Windows.Forms.ListView();
			this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.triggers = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.nextRunTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lastRunTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lastRunResult = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.author = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.created = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.AllowColumnReorder = true;
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.name,
            this.status,
            this.triggers,
            this.nextRunTime,
            this.lastRunTime,
            this.lastRunResult,
            this.author,
            this.created});
			this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView1.FullRowSelect = true;
			this.listView1.HideSelection = false;
			this.listView1.Location = new System.Drawing.Point(0, 0);
			this.listView1.MultiSelect = false;
			this.listView1.Name = "listView1";
			this.listView1.ShowGroups = false;
			this.listView1.Size = new System.Drawing.Size(1035, 232);
			this.listView1.TabIndex = 0;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
			// 
			// name
			// 
			this.name.Text = "Name";
			this.name.Width = 84;
			// 
			// status
			// 
			this.status.Text = "Status";
			this.status.Width = 50;
			// 
			// triggers
			// 
			this.triggers.Text = "Triggers";
			this.triggers.Width = 384;
			// 
			// nextRunTime
			// 
			this.nextRunTime.Text = "Next Run Time";
			this.nextRunTime.Width = 107;
			// 
			// lastRunTime
			// 
			this.lastRunTime.Text = "Last Run Time";
			this.lastRunTime.Width = 112;
			// 
			// lastRunResult
			// 
			this.lastRunResult.Text = "Last Run Result";
			this.lastRunResult.Width = 132;
			// 
			// author
			// 
			this.author.Text = "Author";
			this.author.Width = 77;
			// 
			// created
			// 
			this.created.Text = "Created";
			this.created.Width = 84;
			// 
			// TaskListView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.listView1);
			this.Name = "TaskListView";
			this.Size = new System.Drawing.Size(1035, 232);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader name;
		private System.Windows.Forms.ColumnHeader status;
		private System.Windows.Forms.ColumnHeader triggers;
		private System.Windows.Forms.ColumnHeader nextRunTime;
		private System.Windows.Forms.ColumnHeader lastRunTime;
		private System.Windows.Forms.ColumnHeader lastRunResult;
		private System.Windows.Forms.ColumnHeader author;
		private System.Windows.Forms.ColumnHeader created;
	}
}
