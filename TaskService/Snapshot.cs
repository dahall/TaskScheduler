#if !(NET20 || NET35 || NET40)
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>Abstract class representing a secured item for storage in a <see cref="TaskSchedulerSnapshot"/>.</summary>
	public abstract class SnapshotItem
	{
		/// <summary>Initializes a new instance of the <see cref="SnapshotItem"/> class.</summary>
		/// <param name="path">The path to the item.</param>
		/// <param name="sddl">The SDDL for the item.</param>
		internal SnapshotItem(string path, string sddl) { Path = path; Sddl = sddl; }

		/// <summary>Gets the path to the item.</summary>
		/// <value>The path to the item.</value>
		public string Path { get; private set; }

		/// <summary>Gets the SDDL for the item.</summary>
		/// <value>The SDDL for the item.</value>
		public string Sddl { get; private set; }
	}

	/// <summary>Represents a <see cref="TaskFolder"/> instance and captures its name and security.</summary>
	public sealed class TaskFolderSnapshot : SnapshotItem
	{
		/// <summary>Initializes a new instance of the <see cref="TaskFolderSnapshot"/> class.</summary>
		/// <param name="path">The path to the item.</param>
		/// <param name="sddl">The SDDL for the item.</param>
		internal TaskFolderSnapshot(string path, string sddl) : base(path, sddl) { }
	}

	/// <summary>
	/// Represents all the information about the tasks and folders from a <see cref="TaskService"/> instance that can be used to reconstitute tasks and folders
	/// on the same or different systems. <note>This class and related classes are only available under the .NET 4.5.2 build and later .NET versions due to
	/// dependencies on threading and compressed (zip) files.</note>
	/// </summary>
	public sealed class TaskSchedulerSnapshot : IXmlSerializable
	{
		private const string hdrfile = ".metadata.xml";
		private List<SnapshotItem> items;

		/// <summary>Creates a new instance of <see cref="TaskSchedulerSnapshot"/> from an existing snapshot.</summary>
		/// <param name="path">The zip file snapshot created by the <see cref="Create(TaskService,string)"/> method.</param>
		public TaskSchedulerSnapshot(string path)
		{
			if (path == null) throw new ArgumentNullException(nameof(path));
			if (!File.Exists(path)) throw new FileNotFoundException("Invalid file location.", nameof(path));
			try
			{
				using (var zip = ZipFile.OpenRead(path))
					zip.GetEntry(hdrfile);
			}
			catch
			{
				throw new InvalidOperationException("Invalid file format.");
			}
			Path = path;
		}

		internal TaskSchedulerSnapshot() { }

		/// <summary>
		/// Gets a list of <see cref="TaskSnapshot"/> and <see cref="TaskFolderSnapshot"/> instances the represent the tasks and folders from a Task Scheduler instance.
		/// </summary>
		public List<SnapshotItem> Items
		{
			get => items ?? (items = (Path == null ? new List<SnapshotItem>() : GetArchiveItems(Path)));
			internal set => items = value;
		}

		/// <summary>Gets the path of the file based snapshot.</summary>
		public string Path { get; private set; }

		/// <summary>Gets the machine name of the server from which the snapshot was taken.</summary>
		/// <value>The target server name.</value>
		public string TargetServer { get; private set; }

		/// <summary>Gets the UTC time stamp for when the snapshot was taken.</summary>
		/// <value>The time stamp.</value>
		public DateTime TimeStamp { get; private set; } = DateTime.UtcNow;

		/// <summary>
		/// Creates a compressed zip file that contains all the information accessible to the user from the <see cref="TaskService"/> instance necessary to
		/// reconstitute its tasks and folders. <note>This method can take many seconds to execute. It is recommended to call the asynchronous
		/// version.</note><note type="warning">This method will execute without error even if the user does not have permissions to see all tasks and folders.
		/// It is imperative that the developer ensures that the user has Administrator or equivalent rights before calling this method.</note>
		/// </summary>
		/// <param name="ts">The <see cref="TaskService"/> from which to pull the tasks and folders.</param>
		/// <param name="path">The output zip file in which to place the snapshot information.</param>
		/// <returns>A <see cref="TaskSchedulerSnapshot"/> instance with the contents of the specified Task Scheduler connection.</returns>
		public static TaskSchedulerSnapshot Create(TaskService ts, string path)
		{
			var c = new System.Threading.CancellationTokenSource();
			return InternalCreate(ts.Token, path, c.Token, null);
		}

		/// <summary>
		/// Creates a compressed zip file that contains all the information accessible to the user from the <see cref="TaskService"/> instance necessary to
		/// reconstitute its tasks and folders. <note type="warning">This method will execute without error even if the user does not have permissions to see all
		/// tasks and folders. It is imperative that the developer ensures that the user has Administrator or equivalent rights before calling this method.</note>
		/// </summary>
		/// <param name="tsToken">The <see cref="TaskService.ConnectionToken"/> from which to pull the tasks and folders.</param>
		/// <param name="path">The output zip file in which to place the snapshot information.</param>
		/// <param name="cancelToken">A cancellation token to use to cancel this asynchronous operation.</param>
		/// <param name="progress">An optional <see cref="IProgress{T}"/> instance to use to report progress of the asynchronous operation.</param>
		/// <returns>An asynchronous <see cref="TaskSchedulerSnapshot"/> instance with the contents of the specified Task Scheduler connection.</returns>
		public static async System.Threading.Tasks.Task<TaskSchedulerSnapshot> Create(TaskService.ConnectionToken tsToken, string path, System.Threading.CancellationToken cancelToken, IProgress<Tuple<int, string>> progress)
		{
			return await System.Threading.Tasks.Task.Run(() => InternalCreate(tsToken, path, cancelToken, progress), cancelToken);
		}

		/// <summary>Opens an existing snapshot and returns a new instance of <see cref="TaskSchedulerSnapshot"/>.</summary>
		/// <param name="path">The zip file snapshot created by the <see cref="Create(TaskService,string)"/> method.</param>
		/// <returns>A <see cref="TaskSchedulerSnapshot"/> instance with the contents of the specified snapshot file.</returns>
		public static TaskSchedulerSnapshot Open(string path) => new TaskSchedulerSnapshot(path);

		/// <summary>Register a list of snapshot items (tasks and folders) into the specified Task Scheduler.</summary>
		/// <param name="tsToken">The <see cref="TaskService.ConnectionToken"/> into which the tasks and folders are registered.</param>
		/// <param name="itemPaths">
		/// The list of paths representing the tasks and folders from this snapshot that should be registered on the <see cref="TaskService"/> instance.
		/// </param>
		/// <param name="applyAccessRights">
		/// If <c>true</c>, takes the access rights from the snapshot item and applies it to both new and existing tasks and folders.
		/// </param>
		/// <param name="overwriteExisting">
		/// If <c>true</c>, overwrite any existing tasks and folders found in the target Task Scheduler that match the path of the snapshot item.
		/// </param>
		/// <param name="passwords">
		/// Lookup table for password. Provide pairs of the user/group account name and the associated passwords for any task that requires a password.
		/// </param>
		/// <param name="cancelToken">A cancellation token to use to cancel this asynchronous operation.</param>
		/// <param name="progress">An optional <see cref="IProgress{T}"/> instance to use to report progress of the asynchronous operation.</param>
		/// <returns>An asynchronous <see cref="Task"/> instance.</returns>
		public async System.Threading.Tasks.Task Restore(TaskService.ConnectionToken tsToken, IEnumerable<string> itemPaths, bool applyAccessRights, bool overwriteExisting, IDictionary<string, string> passwords, System.Threading.CancellationToken cancelToken, IProgress<Tuple<int, string>> progress)
		{
			if (itemPaths == null) throw new ArgumentNullException(nameof(itemPaths));
			var items = Items.Where(i => i is TaskSnapshot).Join(itemPaths, a => a.Path, b => b, (a, b) => a).ToList();
			if (items.Count != itemPaths.Count()) throw new ArgumentException($"Unable to locate matching tasks to all values of {nameof(itemPaths)}.", nameof(itemPaths));
			await System.Threading.Tasks.Task.Run(() => InternalRestore(tsToken, items, applyAccessRights, overwriteExisting, passwords, cancelToken, progress));
		}

		/// <summary>Register a list of snapshot items (tasks and folders) into the specified Task Scheduler.</summary>
		/// <param name="tsToken">The <see cref="TaskService.ConnectionToken"/> into which the tasks and folders are registered.</param>
		/// <param name="items">
		/// The list of <see cref="SnapshotItem"/> instances representing the tasks and folders from this snapshot that should be registered on the
		/// <see cref="TaskService"/> instance.
		/// </param>
		/// <param name="applyAccessRights">
		/// If <c>true</c>, takes the access rights from the snapshot item and applies it to both new and existing tasks and folders.
		/// </param>
		/// <param name="overwriteExisting">
		/// If <c>true</c>, overwrite any existing tasks and folders found in the target Task Scheduler that match the path of the snapshot item.
		/// </param>
		/// <param name="passwords">
		/// Lookup table for password. Provide pairs of the user/group account name and the associated passwords for any task that requires a password.
		/// </param>
		/// <param name="cancelToken">A cancellation token to use to cancel this asynchronous operation.</param>
		/// <param name="progress">An optional <see cref="IProgress{T}"/> instance to use to report progress of the asynchronous operation.</param>
		/// <returns>An asynchronous <see cref="Task"/> instance.</returns>
		public async System.Threading.Tasks.Task Restore(TaskService.ConnectionToken tsToken, ICollection<SnapshotItem> items, bool applyAccessRights, bool overwriteExisting, IDictionary<string, string> passwords, System.Threading.CancellationToken cancelToken, IProgress<Tuple<int, string>> progress)
		{
			if (items == null) throw new ArgumentNullException(nameof(items));
			await System.Threading.Tasks.Task.Run(() => InternalRestore(tsToken, items, applyAccessRights, overwriteExisting, passwords, cancelToken, progress));
		}

		/// <summary>Register a list of snapshot items (tasks and folders) into the specified Task Scheduler.</summary>
		/// <param name="ts">The <see cref="TaskService"/> into which the tasks and folders are registered.</param>
		/// <param name="items">
		/// The list of <see cref="SnapshotItem"/> instances representing the tasks and folders from this snapshot that should be registered on the
		/// <see cref="TaskService"/> instance.
		/// </param>
		/// <param name="applyAccessRights">
		/// If <c>true</c>, takes the access rights from the snapshot item and applies it to both new and existing tasks and folders.
		/// </param>
		/// <param name="overwriteExisting">
		/// If <c>true</c>, overwrite any existing tasks and folders found in the target Task Scheduler that match the path of the snapshot item.
		/// </param>
		/// <param name="passwords">
		/// Lookup table for password. Provide pairs of the user/group account name and the associated passwords for any task that requires a password.
		/// </param>
		public void Restore(TaskService ts, ICollection<SnapshotItem> items, bool applyAccessRights, bool overwriteExisting, IDictionary<string, string> passwords)
		{
			var c = new System.Threading.CancellationTokenSource();
			InternalRestore(ts.Token, items, applyAccessRights, overwriteExisting, passwords, c.Token, null);
		}

		XmlSchema IXmlSerializable.GetSchema() => null;

		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			reader.MoveToContent();
			if (DateTime.TryParseExact(reader.GetAttribute("timestamp"), "o", CultureInfo.CurrentCulture.DateTimeFormat, DateTimeStyles.AssumeUniversal, out DateTime dt))
				TimeStamp = dt;
			TargetServer = reader.GetAttribute("machine");
			reader.GetAttribute("version");
			while (reader.Read())
			{
				if (reader.Name == "Task")
				{
					var tsnap = new TaskSnapshot(reader.GetAttribute("path"), reader.GetAttribute("sddl"), true);
					var e = reader.GetAttribute("enabled");
					if (e != null && e == "false") tsnap.Enabled = false;
					Items.Add(tsnap);
				}
				else if (reader.Name == "TaskFolder")
					Items.Add(new TaskFolderSnapshot(reader.GetAttribute("path"), reader.GetAttribute("sddl")));
			}
		}

		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			writer.WriteAttributeString("timestamp", TimeStamp.ToString("o"));
			if (TargetServer != null)
				writer.WriteAttributeString("machine", TargetServer);
			writer.WriteAttributeString("version", "1.0");
			foreach (var i in items)
			{
				if (i is TaskSnapshot t)
				{
					writer.WriteStartElement("Task");
					writer.WriteAttributeString("path", t.Path);
					writer.WriteAttributeString("sddl", t.Sddl);
					if (!t.Enabled)
						writer.WriteAttributeString("enabled", "false");
					writer.WriteEndElement();
				}
				else if (i is TaskFolderSnapshot f)
				{
					writer.WriteStartElement("TaskFolder");
					writer.WriteAttributeString("path", f.Path);
					writer.WriteAttributeString("sddl", f.Sddl);
					writer.WriteEndElement();
				}
			}
		}

		private static List<SnapshotItem> GetArchiveItems(string archiveFile)
		{
			TaskSchedulerSnapshot ret = null;
			using (var zip = ZipFile.OpenRead(archiveFile))
			{
				using (var hdrStr = zip.GetEntry(hdrfile).Open())
					ret = new XmlSerializer(typeof(TaskSchedulerSnapshot)).Deserialize(hdrStr) as TaskSchedulerSnapshot;
				if (ret == null) return null;
				for (int i = 0; i < ret.Items.Count; i++)
				{
					if (ret.Items[i] is TaskSnapshot t)
					{
						var xml = zip.GetEntry(t.Path.TrimStart('\\') + ".xml");
						using (var str = new StreamReader(xml.Open(), Encoding.UTF8))
							t.TaskDefinitionXml = str.ReadToEnd();
					}
				}
			}
			return ret?.Items;
		}

		private static TaskSchedulerSnapshot InternalCreate(TaskService.ConnectionToken token, string path, System.Threading.CancellationToken cancelToken, IProgress<Tuple<int, string>> progress)
		{
			var ts = TaskService.CreateFromToken(token);
			const SecurityInfos siall = SecurityInfos.DiscretionaryAcl | SecurityInfos.SystemAcl | SecurityInfos.Group | SecurityInfos.Owner;
			if (File.Exists(path)) throw new ArgumentException("Output file already exists.", nameof(path));
			int i = 0, count = 0;
			using (var zipstr = new FileStream(path, FileMode.CreateNew))
			using (var zip = new ZipArchive(zipstr, ZipArchiveMode.Create))
			{
				GetCount(ts.RootFolder);
				var snapshot = new TaskSchedulerSnapshot { TargetServer = ts.TargetServer ?? Environment.MachineName };
				GetContents(ts.RootFolder, snapshot.Items, zip);
				snapshot.Items.Sort((t1, t2) => String.Compare(t1.Path, t2.Path, StringComparison.InvariantCultureIgnoreCase));
				using (var hdr = new StreamWriter(zip.CreateEntry(hdrfile).Open(), Encoding.UTF8))
					new XmlSerializer(snapshot.GetType()).Serialize(hdr, snapshot);
				snapshot.Path = path;
				return snapshot;
			}

			void GetCount(TaskFolder f)
			{
				count += f.Tasks.Count;
				foreach (var sf in f.SubFolders)
				{
					count++;
					GetCount(sf);
				}
			}

			void GetContents(TaskFolder f, List<SnapshotItem> list, ZipArchive zip)
			{
				cancelToken.ThrowIfCancellationRequested();
				foreach (var t in f.Tasks)
				{
					list.Add(new TaskSnapshot(t.Path, t.GetSecurityDescriptorSddlForm(siall), t.Enabled, t.Xml));
					using (var wr = new StreamWriter(zip.CreateEntry(t.Path.TrimStart('\\') + ".xml").Open(), Encoding.Unicode))
						wr.Write(t.Xml);
					cancelToken.ThrowIfCancellationRequested();
					progress?.Report(new Tuple<int, string>(++i * 100 / count, t.Path));
				}
				foreach (var sf in f.SubFolders)
				{
					list.Add(new TaskFolderSnapshot(sf.Path, sf.GetSecurityDescriptorSddlForm(siall)));
					zip.CreateEntry(sf.Path.TrimStart('\\') + "\\");
					GetContents(sf, list, zip);
					cancelToken.ThrowIfCancellationRequested();
					progress?.Report(new Tuple<int, string>(++i * 100 / count, sf.Path));
				}
			}
		}

		private void InternalRestore(TaskService.ConnectionToken token, ICollection<SnapshotItem> items, bool applyAccessRights, bool overwriteExisting, IDictionary<string, string> passwords, System.Threading.CancellationToken cancelToken, IProgress<Tuple<int, string>> progress)
		{
			var ts = TaskService.CreateFromToken(token);
			var i = 0;
			progress?.Report(new Tuple<int, string>(0, ""));
			foreach (var item in items)
			{
				cancelToken.ThrowIfCancellationRequested();
				if (item is TaskSnapshot t)
				{
					var td = ts.NewTask();
					td.XmlText = t.TaskDefinitionXml;
					var pwd = td.Principal.RequiresPassword() ? passwords?[td.Principal.ToString()] : null;
					var st = ts.GetTask(t.Path);
					if (st == null)
					{
						CreateTask(t, td, pwd);
					}
					else if (overwriteExisting)
					{
						st = st.Folder.RegisterTaskDefinition(System.IO.Path.GetFileName(t.Path), td, TaskCreation.CreateOrUpdate, td.Principal.ToString(), pwd, td.Principal.LogonType, applyAccessRights ? t.Sddl : null);
						if (!t.Enabled) st.Enabled = false;
					}
				}
				else if (item is TaskFolderSnapshot f)
				{
					var sf = ts.GetFolder(f.Path);
					if (sf == null)
						sf = EnsureFolder(f.Path);
					else if (overwriteExisting && applyAccessRights)
						sf.SetSecurityDescriptorSddlForm(f.Sddl);
				}
				progress?.Report(new Tuple<int, string>(++i * 100 / items.Count, item.Path));
			}
			progress?.Report(new Tuple<int, string>(100, ""));

			void CreateTask(TaskSnapshot task, TaskDefinition td, string password)
			{
				var fpath = System.IO.Path.GetDirectoryName(task.Path);
				var fld = EnsureFolder(fpath);
				var t = fld.RegisterTaskDefinition(System.IO.Path.GetFileName(task.Path), td, TaskCreation.CreateOrUpdate, td.Principal.ToString(), password, td.Principal.LogonType, applyAccessRights ? task.Sddl : null);
				if (!task.Enabled) t.Enabled = false;
			}

			TaskFolder EnsureFolder(string fpath)
			{
				if (!overwriteExisting)
				{
					var f = ts.GetFolder(fpath);
					if (f != null) return f;
				}
				var flds = fpath.Split(new[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
				var fld = ts.RootFolder;
				var sfpath = "";
				for (var j = 0; j < flds.Length; j++)
				{
					sfpath += "\\" + flds[j];
					var sf = fld.SubFolders.Exists(flds[j]) ? fld.SubFolders[flds[j]] : null;
					if (sf == null)
						sf = fld.CreateFolder(flds[j], GetFolderSddl(sfpath), false);
					else if (overwriteExisting)
						sf.SetSecurityDescriptorSddlForm(GetFolderSddl(sfpath));
					fld = sf;
				}
				return fld;
			}

			string GetFolderSddl(string path) => Items?.Find(tci => String.Equals(tci.Path, path, StringComparison.OrdinalIgnoreCase)).Sddl;
		}
	}

	/// <summary>Represents a <see cref="Task"/> instance and captures its details.</summary>
	public sealed class TaskSnapshot : SnapshotItem
	{
		/// <summary>Initializes a new instance of the <see cref="TaskSnapshot"/> class.</summary>
		/// <param name="path">The path to the item.</param>
		/// <param name="sddl">The SDDL for the item.</param>
		/// <param name="enabled">If set to <c>true</c> task is enabled.</param>
		/// <param name="xml">The XML for the <see cref="TaskDefinition"/>.</param>
		internal TaskSnapshot(string path, string sddl, bool enabled, string xml = null) : base(path, sddl) { Enabled = enabled; TaskDefinitionXml = xml; }

		/// <summary>Gets a value indicating whether th <see cref="Task"/> is enabled.</summary>
		/// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
		public bool Enabled { get; internal set; } = true;

		/// <summary>Gets the <see cref="TaskDefinition"/> XML.</summary>
		/// <value>The <see cref="TaskDefinition"/> XML.</value>
		public string TaskDefinitionXml { get; internal set; }
	}
}
#endif