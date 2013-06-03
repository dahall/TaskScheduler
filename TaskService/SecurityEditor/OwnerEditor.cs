using System;
using System.Security.Principal;
using System.Windows.Forms;

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

		public string TargetComputer { get; set; }

		private void listView1_SizeChanged(object sender, EventArgs e)
		{
			ownerListView.Columns[0].Width = ownerListView.Width;
		}

		private void otherUserButton_Click(object sender, EventArgs e)
		{
			string acctName = string.Empty, sid; bool isGroup, isService;
			if (Microsoft.Win32.TaskScheduler.NativeMethods.AccountUtils.SelectAccount(this, this.TargetComputer, ref acctName, out isGroup, out isService, out sid))
			{

			}
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			RefreshOwnerList();
		}

		private ListViewItem GetListItemForId(SecurityIdentifier securityIdentifier)
		{
			//string text = string.Format("{0} ({1}\\{0})");
			var ntAccount = securityIdentifier.Translate(typeof(NTAccount));
			bool isGroup = false;
			return new ListViewItem(ntAccount.Value, isGroup ? 1 : 0);
		}

		private void RefreshOwnerList()
		{
			ownerListView.Items.Clear();
			// TODO: Determine if there is a better way here
			ownerListView.Items.Add(GetListItemForId(WindowsIdentity.GetCurrent().User));
			ownerListView.Items.Add(GetListItemForId(new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null)));
			//ownerListView.Items.Add(string.Format("{0} ({1}\\{0})", Environment.UserName, Environment.UserDomainName), 0).Tag = ;
			//WindowsIdentity ad = new WindowsIdentity()
			ownerListView.Items.Add(string.Format("{0} ({1}\\{0})", "Administrators", Environment.MachineName), 1);
		}
	}
}
