using System;
using System.Windows.Forms;

namespace TaskSchedulerConfig
{
	public partial class ProblemItem : UserControl
	{
		private bool hidden = false;

		public ProblemItem()
		{
			InitializeComponent();
		}

		public string Title
		{
			get { return title.Text; }
			set { title.Text = value; }
		}

		public string Description
		{
			get { return desc.Text; }
			set { desc.Text = value; }
		}

		public string Resolution
		{
			get { return resolution.Text; }
			set { resolution.Text = value; }
		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{
			hidden = !hidden;
			resolution.Visible = desc.Visible = !hidden;
			pictureBox1.Image = hidden ? Properties.Resources.Minus : Properties.Resources.Plus;
		}
	}
}
