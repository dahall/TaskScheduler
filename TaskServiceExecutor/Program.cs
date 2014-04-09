using System;
using System.Net.Mail;
using System.Reflection;
using System.Windows.Forms;

namespace TaskServiceExecutor
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static int Main(string[] args)
		{
			if (args.Length > 0 && args[0].Length == 2 && args[0][0] == '/')
			{
				switch (args[0].ToLower()[1])
				{
					case 's':
						string msg = args[1], title = null;
						if (args.Length >= 3)
							title = args[2];
						return (int)MessageBox.Show(msg, title);
					case 'e':
						if (args.Length >= 6)
						{
							string from = args[1], to = args[2], subject = args[3], body = args[4], server = args[5], cc = null, bcc = null, replyto = null;
							if (args.Length >= 7)
								replyto = args[6];
							if (args.Length >= 8)
								cc = args[7];
							if (args.Length >= 9)
								bcc = args[8];
							try
							{
								MailMessage mail = new MailMessage(from, to, subject, body);
								if (!string.IsNullOrEmpty(replyto))
									mail.ReplyTo = new MailAddress(replyto);
								if (!string.IsNullOrEmpty(cc))
									mail.CC.Add(cc);
								if (!string.IsNullOrEmpty(bcc))
									mail.Bcc.Add(bcc);
								//mail.Headers;
								//mail.Attachments;

								Uri url = new Uri(server);
								SmtpClient SmtpServer = new SmtpClient(url.AbsolutePath);
								SmtpServer.Port = url.IsDefaultPort ? 25 : url.Port;
								try
								{
									string user = url.UserInfo;
									if (!string.IsNullOrEmpty(user))
									{
										string[] parts = user.Split(':');
										SmtpServer.Credentials = new System.Net.NetworkCredential(parts.Length > 0 ? parts[0] : string.Empty, parts.Length > 1 ? parts[1] : string.Empty);
									}
								}
								catch {}
								SmtpServer.UseDefaultCredentials = (SmtpServer.Credentials == null);
								SmtpServer.EnableSsl = url.Scheme == "https";
								SmtpServer.Send(mail);

								return 0;
							}
							catch { }
							return -1;
						}
						break;
					case 'h':
						var sb = new System.Text.StringBuilder();
						sb.AppendLine("This program supports Task Scheduler actions on system prior to Windows Vista.");
						sb.AppendLine(string.Format("{0} - {1}; {2}", GetAssemblyAttribute(typeof(AssemblyCopyrightAttribute), "Copyright"), 
							GetAssemblyAttribute(typeof(AssemblyCompanyAttribute), "Company"), GetAssemblyAttribute(typeof(AssemblyDescriptionAttribute), "Description")));
						sb.AppendLine("");
						sb.AppendLine("Syntax:");
						sb.AppendLine("Show a message to the interactive user:");
						sb.AppendLine("   TaskSchedulerExecutor /s <message> [<title>]");
						sb.AppendLine("Send an email:");
						sb.AppendLine("   TaskSchedulerExecutor /e <from> <to> <subject> <body> <server> [<replyto>] [<cc>] [<bcc>]");
						sb.AppendLine("   ** <from> and <replyto> must be in the format of a single email address.");
						sb.AppendLine("   ** <to>, <cc> and <bcc> must be in the format of one or more email addresses separated by semi-colons.");
						sb.AppendLine("   ** <body> can either be simple text or encoded HTML.");
						sb.AppendLine("   ** <server> can optionally specify a port. A default of 25 is used if not.");
						sb.AppendLine("   ** Attachments and headers are not supported at this time.");
						sb.AppendLine("Help:");
						sb.AppendLine("   TaskSchedulerExecutor /h");
						sb.AppendLine("");
						MessageBox.Show(sb.ToString(), GetAssemblyAttribute(typeof(AssemblyProductAttribute), "Product"));
						break;
					default:
						break;
				}
			}
			return 0;
		}

		static string GetAssemblyAttribute(Type attr, string propName)
		{
			// Get all attributes on this assembly
			object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(attr, false);
			if (attributes.Length > 0)
			{
				// If there is a attribute with the specified property, return its value
				PropertyInfo pi = attr.GetProperty(propName);
				if (pi != null)
				{
					object val = pi.GetValue(attributes[0], null);
					if (val != null)
						return val.ToString();
				}
			}
			return string.Empty;
		}
	}
}
