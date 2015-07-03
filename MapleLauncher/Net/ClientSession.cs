using System.Net.Sockets;
using System.Windows.Forms;

namespace MapleLauncher.Net
{
	public sealed class ClientSession : Session
	{
		public static ClientSession Instance { get; private set; }

		public ClientSession(Socket socket) 
			: base(socket)
		{
			var server = new ServerSession(Program.IP, Program.Port);

			if (!server.Connect())
			{
				frmMain.Instance.InformStatus("Unable to connect to server.");

				MessageBox.Show("Unable to connect to server. \nPlease try again later.", Program.Name, MessageBoxButtons.OK, MessageBoxIcon.Error);

				Application.Exit();
			}
		}

		public override void Dispatch(InPacket inPacket)
		{
			using (OutPacket outPacket = new OutPacket(inPacket))
			{
				if (ServerSession.Instance != null)
				{
					ServerSession.Instance.Send(outPacket.ToArray());
				}
			}
		}

		public override void Terminate()
		{
			if (ServerSession.Instance != null)
			{
				ServerSession.Instance.Disconnect();
			}

			ClientSession.Instance = null;

			frmMain.Instance.Listen();
		}
	}
}
