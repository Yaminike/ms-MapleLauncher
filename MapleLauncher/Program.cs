using System;
using System.Windows.Forms;

namespace MapleLauncher
{
	public enum Localisation : byte
	{
		Global = 8
	}

	internal static class Program
	{
		public const string Name = "MapleLauncher";
		public const string IP = "127.0.0.1";
		public const ushort Port = 8484;

		public const bool MaskIP = true;
		public const string MaskedIP = "8.31.99.141";

		public const string ClientName = "MapleStory.exe";

		public const string Website = "http://www.RaGEZONE.com/";
		public const string Forums = "http://forum.RaGEZONE.com/f425/";

		public const string ConfigName = "config.json";

		public const bool UseMapleEncryption = true;

		private static void Main()
		{
			AppDomain.CurrentDomain.UnhandledException += Program.OnUnhandledException;

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new frmMain());
		}

		private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			MessageBox.Show("An unknown error has occurred: \n", e.ExceptionObject.ToString());
		}
	}
}
