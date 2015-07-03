using MapleLauncher.IO;
using MapleLauncher.Net;
using MapleLauncher.Utilities;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Windows.Forms;

namespace MapleLauncher
{
	public partial class frmMain : Form
	{
		public static frmMain Instance { get; private set; }

		public Config Config { get; private set; }
		public Acceptor Acceptor { get; private set; }

		public frmMain()
		{
			MapleKeys.Initialize();

			this.InitializeComponent();

			this.Acceptor = new Acceptor(8484);
			this.Acceptor.OnClientAccepted = OnClientAccepted;

			frmMain.Instance = this;
		}

		private void LoadConfig()
		{
			string json = File.ReadAllText(Program.ConfigName);
			this.Config = JsonUtilities.Deserialize<Config>(json);
		}

		private void SaveConfig()
		{
			string json = JsonUtilities.Serialize(this.Config);
			File.WriteAllText(Program.ConfigName, json);
		}

		public void Listen()
		{
			this.Acceptor.Start();

			this.InformStatus("Waiting for MapleStory...");
		}

		private void Minimize()
		{
			this.notifyIcon.Visible = true;
			this.notifyIcon.BalloonTipTitle = Program.Name + " is here!";
			this.notifyIcon.BalloonTipText = "Right click to see more!";
			this.notifyIcon.ShowBalloonTip(500);

			this.Hide();
		}

		private void Unmimize()
		{
			this.notifyIcon.Visible = false;

			this.Show();

			this.WindowState = FormWindowState.Normal;
		}

		private void OnClientAccepted(Socket socket)
		{
			this.Minimize();

			new ClientSession(socket);
		}

		public byte[] GetHashPacket()
		{
			using (OutPacket outPacket = new OutPacket(0x0999))
			{
				foreach (string path in Directory.GetFiles(this.Config.MapleStoryPath))
				{
					string extension = Path.GetExtension(path);
					string name = Path.GetFileNameWithoutExtension(path);

					if (extension.ToLower().Contains("wz"))
					{
						outPacket.WriteString(name);
						outPacket.WriteString(HashUtilities.GetMD5HashFromFile(path));
					}
				}

				return outPacket.ToArray();
			}
		}

		public void InformStatus(string text, params object[] args)
		{
			if (InvokeRequired)
			{
				Invoke(new MethodInvoker(() => InformStatus(text, args)));

				return;
			}

			text = string.Format(text, args);

			lblStatus.Text = text;
		}

		#region Events
		private void frmMain_Load(object sender, EventArgs e)
		{
			if (!File.Exists(Program.ConfigName))
			{
				MessageBox.Show(string.Format("Unable to locate configuration file '{0}'. \nPlease contact your server administrator.", Program.ConfigName), Program.Name, MessageBoxButtons.OK, MessageBoxIcon.Error);

				Application.Exit();
			}

			this.Text = Program.Name;

			this.LoadConfig();

			if (Program.MaskIP)
			{
				NetworkUtilities.MaskIP(Program.MaskedIP);
			}

			this.Listen();
		}

		private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.SaveConfig();

			NetworkUtilities.UnmaskIP(Program.MaskedIP);
		}

		private void btnWebsite_Click(object sender, EventArgs e)
		{
			Process.Start(Program.Website);
		}

		private void btnForums_Click(object sender, EventArgs e)
		{
			Process.Start(Program.Forums);
		}

		private void btnQuit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void btnStart_Click(object sender, EventArgs e)
		{
			string path = Path.Combine(this.Config.MapleStoryPath, Program.ClientName);

			if (!File.Exists(path))
			{
				MessageBox.Show(string.Format("Unable to locate '{0}'.", path), Program.Name, MessageBoxButtons.OK, MessageBoxIcon.Error);

				return;
			}

			Process.Start(path);
		}

		private void frmMain_Resize(object sender, EventArgs e)
		{
			if (this.WindowState == FormWindowState.Minimized)
			{
				this.Minimize();
			}
			else if (WindowState == FormWindowState.Normal)
			{
				this.Unmimize();
			}
		}

		private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			this.Unmimize();
		}
		#endregion
	}
}
