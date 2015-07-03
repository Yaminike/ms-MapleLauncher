using System.Diagnostics;

namespace MapleLauncher.Utilities
{
	internal static class NetworkUtilities
	{
		public static void MaskIP(string ip)
		{
			string command = string.Format("netsh int ip add addr 1 {0} mask=255.255.255.255", ip);

			Process process = new Process();
			ProcessStartInfo startInfo = new ProcessStartInfo();

			startInfo.WindowStyle = ProcessWindowStyle.Hidden;
			startInfo.FileName = "cmd.exe";
			startInfo.Arguments = command;

			process.StartInfo = startInfo;
			process.Start();

			frmMain.Instance.InformStatus("Succesfully masked IP Address '{0}'.", ip);
		}

		public static void UnmaskIP(string ip)
		{
			string command = string.Format("netsh int ip delete addr 1 {0}", ip);

			Process process = new Process();
			ProcessStartInfo startInfo = new ProcessStartInfo();

			startInfo.WindowStyle = ProcessWindowStyle.Hidden;
			startInfo.FileName = "cmd.exe";
			startInfo.Arguments = command;

			process.StartInfo = startInfo;
			process.Start();

			frmMain.Instance.InformStatus("Succesfully unmasked IP Address '{0}'.", ip);
		}
	}
}
