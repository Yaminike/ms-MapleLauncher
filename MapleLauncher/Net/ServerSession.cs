namespace MapleLauncher.Net
{
	public sealed class ServerSession : Session
	{
		public static ServerSession Instance { get; private set; }

		public ServerSession(string ip, ushort port)
			: base(ip, port)
		{
			ServerSession.Instance = this;
		}

		public override void OnHandshake(ServerInfo info)
		{
			if (ClientSession.Instance != null)
			{
				ClientSession.Instance.Handshake(info);
			}

			this.Send(frmMain.Instance.GetHashPacket());
		}

		public override void Dispatch(InPacket inPacket)
		{
			using (OutPacket outPacket = new OutPacket(inPacket))
			{
				if (ClientSession.Instance != null)
				{
					ClientSession.Instance.Send(outPacket.ToArray());
				}
			}
		}

		public override void Terminate()
		{
			ServerSession.Instance = null;

			if (ClientSession.Instance != null)
			{
				ClientSession.Instance.Disconnect();
			}
		}
	}
}
