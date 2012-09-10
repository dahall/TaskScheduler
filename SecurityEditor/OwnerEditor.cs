using System;
using System.Windows.Forms;
using System.Security.Principal;

namespace SecurityEditor
{
	internal partial class OwnerEditor : UserControl
	{
		private string objName;
		private IdentityReference sid;

		public OwnerEditor()
		{
			InitializeComponent();
			objNameText.BackColor = this.BackColor;
		}

		public string ObjectName
		{
			get { return objName; }
			set { objName = value; objNameText.Text = objName; }
		}

		public IdentityReference Identity
		{
			get { return sid; }
			set { sid = value; }
		}

		private void listView1_SizeChanged(object sender, EventArgs e)
		{
			ownerListView.Columns[0].Width = ownerListView.Width;
		}
	}
}
