using CSUACSelfElevation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TaskSchedulerConfig
{
	public partial class WizardForm : Form
	{
		private bool autofix;
		private bool canElevate;
		private bool isElevated;
		private Diagnostics diags;

		public WizardForm()
		{
			InitializeComponent();
			canElevate = UACHandler.IsUserInAdminGroup() && UACHandler.IsUacEnabled();
			isElevated = canElevate && UACHandler.IsRunAsAdmin();
		}

		private enum ScanState { Privileges, Firewall, Services, Complete };

		private void WizardForm_Load(object sender, EventArgs e)
		{
		}

		private void wizardControl1_Cancelling(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (localScanner.IsBusy) localScanner.CancelAsync();
			if (remoteScanner.IsBusy) remoteScanner.CancelAsync();
		}

		// ********* INTRO Page *************
		private void intro_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
		{
			runAsAdminPrompt.Visible = canElevate && !isElevated;
		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			UACHandler.RestartElevated();
		}

		private void introWizPg_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
		{
			autofix = autoRepairCheck.Checked;
		}

		// ********* DETECT Page *************
		private void scanLocal_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
		{
			diags = new Diagnostics(null);
			localScanner.RunWorkerAsync();
		}

		// ********* NO ERROR Page *************
		private void connRemoteBtn_Click(object sender, EventArgs e)
		{
			wizardControl1.NextPage(selectRemoteWizPg);
		}

		private void explOptionsBtn_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("http://taskscheduler.codeplex.com/wikipage?title=TaskSecurity");
		}

		private void closeBtn_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void showLocalResults_HelpClicked(object sender, EventArgs e)
		{
			wizardControl1.NextPage(reportWizPg);
		}

		// ********* ERROR Page *************
		private void completeWithProbWizPg_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
		{
			// Fill out list box based on diagnostics
			var items = new List<Control>();
			foreach (var diag in diags.Issues)
				items.Add(MakeStatusItem(diag));
			if (autofix && items.Count > 0)
				items.Insert(0, MakeTitleItem("Issues found"));
			issueList.Controls.Clear();
			items.Reverse();
			if (items.Count > 0)
				issueList.Controls.AddRange(items.ToArray());
			else
				wizardControl1.NextPage(completeNoProbWizPg);
		}

		private Control MakeStatusItem(Diagnostics.Diagnostic diag, bool indented = true)
		{
			var status = ItemStatus.Detected;
			if (diag.HasIssue.HasValue && !diag.HasIssue.Value)
				status = ItemStatus.Checked;
			else
			{
				if (diag.Resolved.HasValue)
				{
					if (diag.Resolved.Value)
						status = ItemStatus.Fixed;
				}
				else
				{
					if (diag.Resolution.RequiresConsent || diag.Resolution.RequiresElevation)
						status = ItemStatus.NotFixed;
					else
						status = ItemStatus.None;
				}
			}
			return MakeStatusItem(diag.Name, diag.Description, status, indented);
		}

		// ********* REPORT Page *************
		private void reportWizPg_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
		{
			var items = new List<Control>();
			foreach (var diag in diags.Issues)
				items.Add(MakeReportItem(diag));
			if (items.Count > 0)
				items.Insert(0, MakeTitleItem("Issues found"));
			int iPos = items.Count;
			foreach (var diag in diags.NonIsseus)
				items.Add(MakeStatusItem(diag, false));
			if (items.Count > iPos)
				items.Insert(iPos, MakeTitleItem("\r\nIssues checked"));
			localConfigList.Controls.Clear();
			items.Reverse();
			if (items.Count > 0)
				localConfigList.Controls.AddRange(items.ToArray());
		}

		private Control MakeReportItem(Diagnostics.Diagnostic diag)
		{
			var c = MakeStatusItem(diag, false) as TableLayoutPanel;
			c.RowCount = 2;
			c.RowStyles.Add(new RowStyle(SizeType.AutoSize));
			c.Controls.Add(MakeLabel(diag.Resolution.Name, diag.Resolution.Description, true), 0, 1);
			string resText;
			bool canRun = false;
			if (diag.Resolved.HasValue)
				resText = diag.Resolved.Value ? "Completed" : "Issue not present";
			else
			{
				resText = diag.Resolution.RequiresConsent ? "Requires consent" : "Not run";
				if (diag.Resolution.RequiresConsent && (!diag.Resolution.RequiresElevation || isElevated))
					canRun = true;
			}
			c.Controls.Add(MakeLabel(resText, null, false, false, true), 1, 1);
			if (canRun)
				c.Controls.Add(MakeIcon(Properties.Resources.Run, "Click here to run the repair", diag), 2, 1);
			return c;
		}

		// ********* REMOTE SERVER Page *************
		private void selectRemote_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
		{
			remoteScanner.RunWorkerAsync();
		}

		// ********* SCANNER METHODS *************
		private void localScanner_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			object defParam = null;
			e.Result = diags;
			var rp = new EventHandler<Diagnostics.ShowMessageEventArgs>((o, m) => localScanner.ReportProgress(0, m.Message));
			diags.ShowMessage += rp;
			foreach (var d in diags)
			{
				if (d.Condition(defParam))
				{
					if (!d.RequiresElevation || isElevated)
					{
						try { d.HasIssue = d.Troubleshooter(defParam); } catch (Exception ex) { e.Result = ex; throw; }
						if (d.HasIssue.Value && autofix && !d.Resolution.RequiresConsent)
						{
							if (!d.Resolution.RequiresElevation || isElevated)
								try { d.Resolution.Resolver(defParam); d.Resolved = !d.Troubleshooter(defParam); } catch { }
						}
					}
				}
			}
			diags.ShowMessage -= rp;
		}

		private void localScanner_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
		{
			scanLocalStatusLabel.Text = e.UserState?.ToString();
			if (e.ProgressPercentage == 100)
				wizardControl1.NextPage();
		}

		private void localScanner_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			if (!e.Cancelled)
			{
				if (e.Error != null)
				{
					localResultLabel.Text = "An error occurred while troubleshooting. Error: " + e.Error.ToString();
					wizardControl1.NextPage(completeWithProbWizPg);
				}
				else
				{
					int i = 0; foreach (var d in diags.Issues) i++;
					wizardControl1.NextPage(i == 0 ? completeNoProbWizPg : completeWithProbWizPg);
				}
			}
		}

		private void remoteScanner_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
		{
		}

		private void remoteScanner_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
		{

		}

		private void remoteScanner_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{

		}

		/*private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
		{
			((sender as ListBox)?.Items[e.Index] as LBItem)?.DrawItem(e);
		}

		private void listBox1_MeasureItem(object sender, MeasureItemEventArgs e)
		{
			var lb = sender as ListBox;
			(lb?.Items[e.Index] as LBItem)?.MeasureItem(e, lb?.Font);
		}*/

		private Control MakeTitleItem(string text, string tooltip = null)
		{
			var c = MakeTableItem(1, 1);
			c.Controls.Add(MakeLabel(text, tooltip, false, true, false), 0, 0);
			return c;
		}

		private enum ItemStatus { None, Fixed, Detected, NotFixed, Checked }

		private Control MakeStatusItem(string text, string tooltip, ItemStatus status, bool indented = false)
		{
			var c = MakeTableItem(1, 3);
			c.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 100);
			c.ColumnStyles[2] = new ColumnStyle(SizeType.AutoSize);
			c.Controls.Add(MakeLabel(text, tooltip, indented), 0, 0);
			c.Controls.Add(MakeStatusLabel(status), 1, 0);
			c.Controls.Add(MakeStatusIcon(status), 2, 0);
			return c;
		}

		private Control MakeStatusIcon(ItemStatus status)
		{
			Image img = Properties.Resources.NotFixed;
			if (status == ItemStatus.Checked || status == ItemStatus.Fixed)
				img = Properties.Resources.Fixed;
			if (status == ItemStatus.Detected)
				img = Properties.Resources.Detected;
			return MakeIcon(img);
		}

		private Control MakeStatusLabel(ItemStatus status)
		{
			string text;
			switch (status)
			{
				case ItemStatus.Fixed:
					text = "Fixed"; break;
				case ItemStatus.Detected:
					text = "Detected"; break;
				case ItemStatus.NotFixed:
					text = "Not fixed"; break;
				case ItemStatus.Checked:
					text = "Checked"; break;
				default:
					text = "Unknown"; break;
			}
			return MakeLabel(text);
		}

		private Label MakeLabel(string text, string tooltip = null, bool indented = false, bool bold = false, bool ital = false)
		{
			var fs = FontStyle.Regular;
			if (bold) fs |= FontStyle.Bold;
			if (ital) fs |= FontStyle.Italic;
			var l = new Label
			{
				Anchor = AnchorStyles.Left | AnchorStyles.Right,
				AutoSize = true,
				Font = new Font(wizardControl1.Font, fs),
				Margin = new Padding(0),
				Padding = new Padding(indented ? 12 : 0, 2, 0, 2),
				Text = text,
			};
			if (tooltip != null)
				toolTip1.SetToolTip(l, tooltip);
			return l;
		}

		private TableLayoutPanel MakeTableItem(int rows = 1, int cols = 1)
		{
			var c = new TableLayoutPanel
			{
				AutoSize = true,
				AutoSizeMode = AutoSizeMode.GrowAndShrink,
				ColumnCount = cols,
				Dock = DockStyle.Top,
				Margin = new Padding(0),
				RowCount = rows,
			};
			for (int i = 0; i < cols; i++)
				c.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			for (int i = 0; i < rows; i++)
				c.RowStyles.Add(new RowStyle(SizeType.AutoSize));
			return c;
		}

		private PictureBox MakeIcon(Image image, string tooltip = null, Diagnostics.Diagnostic diag = null)
		{
			var p = new PictureBox
			{
				Anchor = AnchorStyles.Right,
				Image = image,
				Margin = new Padding(4, 2, 10, 2),
				Size = new Size(16, 16),
				TabStop = false,
			};
			if (tooltip != null)
				toolTip1.SetToolTip(p, tooltip);
			if (diag?.Resolution.Resolver != null)
				p.Click += (s, e) => PromptAndRun(s, diag);
			return p;
		}

		private void PromptAndRun(object sender, Diagnostics.Diagnostic diag)
		{
			var p = sender as PictureBox;
			if (p?.Tag == null && diag.Resolution.Resolver != null && MessageBox.Show(this, $"Execute the following patch?\n\n{diag.Resolution.Name}", "Apply Fix", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
			{
				diag.Resolution.Resolver?.Invoke(null);
				if (!diag.Troubleshooter(null))
				{
					if (p != null)
					{
						p.Image = Properties.Resources.Fixed;
						p.Tag = 0;
						var t = p.Parent as TableLayoutPanel;
						if (t != null)
						{
							var l = t.GetControlFromPosition(1, 1) as Label;
							if (l != null)
								l.Text = "Fixed";
						}
					}
				}
			}
		}

		/*private abstract class LBItem
		{
			public const int lrPad = 10;
			public const int tbPad = 2;
			protected const int stW = 80;

			protected string Text;
			public string Tooltip;
			public abstract void DrawItem(DrawItemEventArgs e);
			public abstract void MeasureItem(MeasureItemEventArgs e, Font font);
		}

		private class LBTitleItem : LBItem
		{
			public LBTitleItem(string text) { Text = text; }
			public override void MeasureItem(MeasureItemEventArgs e, Font font)
			{
				Size sz = new Size(e.ItemWidth, e.ItemHeight);
				using (var f = new Font(font, FontStyle.Bold))
					e.ItemHeight = TextRenderer.MeasureText(e.Graphics, Text, f, sz, TextFormatFlags.SingleLine).Height + (tbPad * 2);
			}

			public override void DrawItem(DrawItemEventArgs e)
			{
				e.Graphics.FillRectangle(SystemBrushes.Window, e.Bounds);
				using (var f = new Font(e.Font, FontStyle.Bold))
					TextRenderer.DrawText(e.Graphics, Text, f, e.Bounds, SystemColors.WindowText, TextFormatFlags.SingleLine | TextFormatFlags.VerticalCenter);
			}
		}

		private class LBStatusItem : LBItem
		{
			private enum LBItemStatus { None, Fixed, Detected, NotFixed, Checked }

			private LBItemStatus Status;
			private bool indentLeft;

			public LBStatusItem(Diagnostics.Diagnostic diag, bool leftIndent = true)
			{
				Text = diag.Name;
				Status = LBItemStatus.Detected;
				if (diag.HasIssue.HasValue && !diag.HasIssue.Value)
					Status = LBItemStatus.Checked;
				else
				{
					if (diag.Resolved.HasValue)
					{
						if (diag.Resolved.Value)
							Status = LBItemStatus.Fixed;
					}
					else
					{
						if (diag.Resolution.RequiresConsent || diag.Resolution.RequiresElevation)
							Status = LBItemStatus.NotFixed;
						else
							Status = LBItemStatus.None;
					}
				}
				indentLeft = leftIndent;
			}

			public override void MeasureItem(MeasureItemEventArgs e, Font font)
			{
				Size sz = new Size(e.ItemWidth, e.ItemHeight);
				e.ItemHeight = Math.Max(TextRenderer.MeasureText(e.Graphics, Text, font, sz, TextFormatFlags.SingleLine).Height, this.Image.Height) + (tbPad * 2);
			}

			public override void DrawItem(DrawItemEventArgs e)
			{
				int imgW = this.Image.Width;
				int imgX = e.Bounds.Right - lrPad - imgW;
				e.Graphics.FillRectangle(SystemBrushes.Window, e.Bounds);
				e.Graphics.DrawImage(this.Image, new Rectangle(imgX, e.Bounds.Y + (e.Bounds.Height - this.Image.Height) / 2, imgW, imgW));
				Rectangle stBounds = new Rectangle(e.Bounds.Right - (lrPad * 2) - imgW - stW, e.Bounds.Y, stW, e.Bounds.Height);
				TextRenderer.DrawText(e.Graphics, StatusText, e.Font, stBounds, SystemColors.WindowText, TextFormatFlags.SingleLine | TextFormatFlags.VerticalCenter);
				Rectangle tBounds = indentLeft ? Rectangle.Inflate(e.Bounds, -lrPad, 0) : new Rectangle(e.Bounds.Location, new Size(e.Bounds.Width - lrPad, e.Bounds.Height));
				tBounds.Width -= (lrPad * 2 + imgW + stW);
				TextRenderer.DrawText(e.Graphics, Text, e.Font, tBounds, SystemColors.WindowText, TextFormatFlags.SingleLine | TextFormatFlags.VerticalCenter);
			}

			private string StatusText
			{
				get
				{
					switch (Status)
					{
						case LBItemStatus.Fixed:
							return "Fixed";
						case LBItemStatus.Detected:
							return "Detected";
						case LBItemStatus.NotFixed:
							return "Not fixed";
						case LBItemStatus.Checked:
							return "Checked";
						default:
							return "Unknown";
					}
				}
			}

			private Image Image
			{
				get
				{
					if (Status == LBItemStatus.Checked || Status == LBItemStatus.Fixed)
						return Properties.Resources.Fixed;
					if (Status == LBItemStatus.Detected)
						return Properties.Resources.Detected;
					return Properties.Resources.NotFixed;
				}
			}
		}

		private class LBResolutionItem : LBItem
		{
			public enum LBResStatus { NotRun, Completed, NotPresent, ReqConsent }

			private LBResStatus Status;

			public LBResolutionItem(Diagnostics.Diagnostic diag)
			{
				Text = diag.Resolution.Name;
				if (diag.Resolved.HasValue)
					Status = diag.Resolved.Value ? LBResStatus.Completed : LBResStatus.NotPresent;
				else
					Status = diag.Resolution.RequiresConsent ? LBResStatus.ReqConsent : LBResStatus.NotRun;
			}

			public override void MeasureItem(MeasureItemEventArgs e, Font font)
			{
				Size sz = new Size(e.ItemWidth, e.ItemHeight);
				e.ItemHeight = TextRenderer.MeasureText(e.Graphics, Text, font, sz, TextFormatFlags.SingleLine).Height + tbPad;
			}

			public override void DrawItem(DrawItemEventArgs e)
			{
				int imgW = 16;
				int imgX = e.Bounds.Right - lrPad - imgW;
				e.Graphics.FillRectangle(SystemBrushes.Window, e.Bounds);
				Rectangle stBounds = new Rectangle(e.Bounds.Right - (lrPad * 2) - imgW - stW, e.Bounds.Y, stW + lrPad + imgW, e.Bounds.Height);
				using (Font f2 = new Font(e.Font, FontStyle.Italic))
					TextRenderer.DrawText(e.Graphics, StatusText, f2, stBounds, SystemColors.WindowText, TextFormatFlags.SingleLine);
				Rectangle tBounds = new Rectangle(e.Bounds.X + lrPad * 2, e.Bounds.Y, e.Bounds.Width - (lrPad * 2) - stBounds.Width, e.Bounds.Height);
				TextRenderer.DrawText(e.Graphics, Text, e.Font, tBounds, SystemColors.WindowText, TextFormatFlags.SingleLine | TextFormatFlags.Top);
			}

			private string StatusText
			{
				get
				{
					switch (Status)
					{
						case LBResStatus.Completed:
							return "Completed";
						case LBResStatus.NotPresent:
							return "Issue not present";
						case LBResStatus.ReqConsent:
							return "Requires consent";
						default:
							return "Not run";
					}
				}
			}
		}

		private class LBRepairConfirmItem : LBItem
		{
			protected string TroubleText;
			protected string RepairText;
			public bool RepairChecked = true;
			public bool Collapsed = false;

			public LBRepairConfirmItem(Diagnostics.Diagnostic diag)
			{
				Text = diag.Name;
				TroubleText = diag.Description;
				RepairText = diag.Resolution.Name;
			}

			public override void MeasureItem(MeasureItemEventArgs e, Font font)
			{
				var m1 = new MeasureItemEventArgs(e.Graphics, e.Index, e.ItemHeight);
				//TroubleLine.MeasureItem(m1, font);
				var m2 = new MeasureItemEventArgs(e.Graphics, e.Index, e.ItemHeight);
				//RepairLine.MeasureItem(m2, font);
				e.ItemHeight = m1.ItemHeight + m2.ItemHeight;
			}

			public override void DrawItem(DrawItemEventArgs e)
			{
				var m1 = new MeasureItemEventArgs(e.Graphics, e.Index, e.Bounds.Height);
				//TroubleLine.MeasureItem(m1, e.Font);
				var r = e.Bounds; r.Height = m1.ItemHeight;
				//TroubleLine.DrawItem(new DrawItemEventArgs(e.Graphics, e.Font, r, e.Index, e.State, e.ForeColor, e.BackColor));
				r.Y += m1.ItemHeight; r.Height = e.Bounds.Height - m1.ItemHeight;
				//RepairLine.DrawItem(new DrawItemEventArgs(e.Graphics, e.Font, r, e.Index, e.State, e.ForeColor, e.BackColor));
			}
		}

		private class LBReportItem : LBItem
		{
			protected LBStatusItem TroubleLine;
			protected LBResolutionItem RepairLine;

			public LBReportItem(Diagnostics.Diagnostic diag)
			{
				TroubleLine = new LBStatusItem(diag, false);
				RepairLine = new LBResolutionItem(diag);
			}
			public override void MeasureItem(MeasureItemEventArgs e, Font font)
			{
				var m1 = new MeasureItemEventArgs(e.Graphics, e.Index, e.ItemHeight);
				TroubleLine.MeasureItem(m1, font);
				var m2 = new MeasureItemEventArgs(e.Graphics, e.Index, e.ItemHeight);
				RepairLine.MeasureItem(m2, font);
				e.ItemHeight = m1.ItemHeight + m2.ItemHeight;
			}
			public override void DrawItem(DrawItemEventArgs e)
			{
				var m1 = new MeasureItemEventArgs(e.Graphics, e.Index, e.Bounds.Height);
				TroubleLine.MeasureItem(m1, e.Font);
				var r = e.Bounds; r.Height = m1.ItemHeight;
				TroubleLine.DrawItem(new DrawItemEventArgs(e.Graphics, e.Font, r, e.Index, e.State, e.ForeColor, e.BackColor));
				r.Y += m1.ItemHeight; r.Height = e.Bounds.Height - m1.ItemHeight;
				RepairLine.DrawItem(new DrawItemEventArgs(e.Graphics, e.Font, r, e.Index, e.State, e.ForeColor, e.BackColor));
			}
		}*/
	}
}
