using MapleLauncher.IO;
using System;
using System.Windows.Forms;

namespace MapleLauncher
{
	public partial class frmSettings : Form
	{
		public frmSettings()
		{
			InitializeComponent();
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			frmMain.Instance.Config.MapleStoryPath = txtMapleStoryPath.Text;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void btnBrowse_Click(object sender, EventArgs e)
		{
			using (FolderBrowserDialog fbd = new FolderBrowserDialog())
			{
				if (fbd.ShowDialog() == DialogResult.OK)
				{
					txtMapleStoryPath.Text = fbd.SelectedPath;
				}
			}
		}
	}
}
