using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler.UIComponents
{
	internal partial class EventTriggerUI : UserControl, ITriggerHandler
	{
		protected bool isV2 = true;
		protected bool onAssignment = false;
		protected EventTrigger trigger;

		public event PropertyChangedEventHandler TriggerChanged;

		public EventTriggerUI()
		{
			InitializeComponent();
		}

		[Browsable(false), DefaultValue(null), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string TargetServer { get; set; }

		[Browsable(false), DefaultValue(null), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual Trigger Trigger
		{
			get { if (trigger != null) trigger.Subscription = onEventCustomText.Text; return trigger; }
			set
			{
				if (!(value is EventTrigger))
					throw new ArgumentException("Trigger property value on EventTriggerUI must be an EventTrigger.");

				onAssignment = true;
				trigger = (EventTrigger)value;

				InitializeEventLogList();
				UpdateCustomText();
				eventBasicRadio.Checked = TrySetBasic();
			}
		}

		[DefaultValue(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual bool IsV2
		{
			get { return isV2; }
			set { isV2 = value; }
		}

		public virtual bool IsTriggerValid() { return onEventCustomText.TextLength > 0; }

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.Enter" /> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		protected override void OnEnter(EventArgs e)
		{
			base.OnEnter(e);
			InitializeEventLogList();
		}

		protected virtual void OnTriggerChanged(PropertyChangedEventArgs e)
		{
			if (TriggerChanged != null)
			{
				var ev = this.TriggerChanged;
				ev(this, e);
			}
		}

		private static string GetFormattedXmlString(string xml)
		{
			if (!string.IsNullOrEmpty(xml))
			{
				var xmlDoc = new System.Xml.XmlDocument();
				xmlDoc.LoadXml(xml);
				using (var writer = new System.IO.StringWriter())
				using (var xmlW = new System.Xml.XmlTextWriter(writer))
				{
					xmlW.Formatting = System.Xml.Formatting.Indented;
					xmlDoc.WriteTo(xmlW);
					xmlW.Flush();
					return writer.ToString();
				}
			}
			return null;
		}

		private void eventBasicRadio_CheckedChanged(object sender, EventArgs e)
		{
			bool basic = eventBasicRadio.Checked || !eventCustomRadio.Checked;
			onEventBasicPanel.Visible = basic;
			onEventCustomPanel.Visible = !basic;
		}

		private int? EventId
		{
			get
			{
				int rid;
				return (int.TryParse(onEventIdText.Text, out rid) ? (int?)rid : null);
			}
			set
			{
				onEventIdText.Text = value.HasValue ? value.Value.ToString() : null;
			}
		}

		private void InitializeEventLogList()
		{
			if (onEventLogCombo.Items.Count == 0)
			{
				onEventLogCombo.Items.AddRange(SystemEventEnumerator.GetEventLogs(TargetServer));
			}
		}

		private void onEventCustomText_Leave(object sender, EventArgs e)
		{
			trigger.Subscription = onEventCustomText.TextLength > 0 ? onEventCustomText.Text : null;
			TrySetBasic();
			OnTriggerChanged(new PropertyChangedEventArgs("Trigger"));
		}

		private void onEventIdText_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
		}

		private void onEventIdText_TextChanged(object sender, EventArgs e)
		{
			long a;
			if (!long.TryParse(onEventIdText.Text, out a))
				onEventIdText.Clear();
		}

		private void onEventLogCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			onEventSourceCombo.Items.Clear();
			onEventSourceCombo.Items.AddRange(SystemEventEnumerator.GetEventProviders(TargetServer, onEventLogCombo.Text));
			onEventSourceCombo.SelectedIndex = onEventSourceCombo.Items.Count > 0 ? 0 : -1;
			UpdateCustomText();
			OnTriggerChanged(new PropertyChangedEventArgs("Trigger"));
		}

		private void onEventTextBox_Leave(object sender, EventArgs e)
		{
			if (onEventLogCombo.Text.Length > 0)
			{
				if (trigger != null)
					trigger.SetBasic(onEventLogCombo.Text, onEventSourceCombo.Text, this.EventId);
				UpdateCustomText();
				OnTriggerChanged(new PropertyChangedEventArgs("Trigger"));
			}
		}

		[DefaultValue(false)]
		bool ITriggerHandler.ShowStartBoundary
		{
			get { return false; }
			set { }
		}

		private bool TrySetBasic()
		{
			if (trigger == null)
				return true;

			string log, source; int? id;
			bool basic = trigger.GetBasic(out log, out source, out id);

			string sub = trigger.Subscription;
			if (string.IsNullOrEmpty(sub))
				basic = true;
			if (basic)
			{
				onEventLogCombo.Text = log;
				onEventSourceCombo.Text = source;
				this.EventId = id;
			}

			return basic;
		}

		private void UpdateCustomText()
		{
			string xml = trigger == null ? EventTrigger.BuildQuery(onEventLogCombo.Text, 
				onEventSourceCombo.Text.Length == 0 ? null : onEventSourceCombo.Text, this.EventId) : trigger.Subscription;
			onEventCustomText.Text = GetFormattedXmlString(xml);
		}

		private void editBtn_Click(object sender, EventArgs e)
		{
			using (var dlg = new EventActionFilterEditor(onEventCustomText.Text))
			{
				if (dlg.ShowDialog(this) == DialogResult.OK)
				{
					onEventCustomText.Text = dlg.Subscription;
					eventBasicRadio.Enabled = TrySetBasic();
				}
			}
		}
	}
}
