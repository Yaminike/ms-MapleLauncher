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
			if (inPacket.OperationCode == Program.ServerIP)
			{
				ushort status = inPacket.ReadUShort();
				ushort status2 = inPacket.ReadUShort();
				byte[] ip = inPacket.ReadBytes(4);
				ushort port = inPacket.ReadUShort();
				byte[] leftover = inPacket.ReadLeftoverBytes();

				Program.IP = string.Format("{0}.{1}.{2}.{3}", ip[0], ip[1], ip[2], ip[3]);
				Program.Port = port;

				using (OutPacket outPacket = new OutPacket(Program.ServerIP))
				{
					outPacket.WriteUShort(status);
					outPacket.WriteUShort(status2);
					outPacket.WriteBytes(127, 0, 0, 1);
					outPacket.WriteUShort(8484);
					outPacket.WriteBytes(leftover);

					if (ClientSession.Instance != null)
					{
						ClientSession.Instance.Send(outPacket.ToArray());
					}
				}
			}
			else
			{
				using (OutPacket outPacket = new OutPacket(inPacket))
				{
					if (ClientSession.Instance != null)
					{
						ClientSession.Instance.Send(outPacket.ToArray());
					}
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
