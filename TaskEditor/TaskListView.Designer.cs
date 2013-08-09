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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskListView));
			this.listView1 = new System.Windows.Forms.ListView();
			this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.triggers = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.nextRunTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lastRunTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lastRunResult = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.author = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.created = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.smallImageList = new System.Windows.Forms.ImageList(this.components);
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
			resources.ApplyResources(this.listView1, "listView1");
			this.listView1.FullRowSelect = true;
			this.listView1.HideSelection = false;
			this.listView1.MultiSelect = false;
			this.listView1.Name = "listView1";
			this.listView1.ShowGroups = false;
			this.listView1.SmallImageList = this.smallImageList;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
			this.listView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseClick);
			// 
			// name
			// 
			resources.ApplyResources(this.name, "name");
			// 
			// status
			// 
			resources.ApplyResources(this.status, "status");
			// 
			// triggers
			// 
			resources.ApplyResources(this.triggers, "triggers");
			// 
			// nextRunTime
			// 
			resources.ApplyResources(this.nextRunTime, "nextRunTime");
			// 
			// lastRunTime
			// 
			resources.ApplyResources(this.lastRunTime, "lastRunTime");
			// 
			// lastRunResult
			// 
			resources.ApplyResources(this.lastRunResult, "lastRunResult");
			// 
			// author
			// 
			resources.ApplyResources(this.author, "author");
			// 
			// created
			// 
			resources.ApplyResources(this.created, "created");
			// 
			// smallImageList
			// 
			this.smallImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			resources.ApplyResources(this.smallImageList, "smallImageList");
			this.smallImageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// TaskListView
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.listView1);
			this.Name = "TaskListView";
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
		private System.Windows.Forms.ImageList smallImageList;
	}
}
