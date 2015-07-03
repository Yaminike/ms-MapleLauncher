namespace MapleLauncher
{
	partial class frmMain
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.btnStart = new System.Windows.Forms.PictureBox();
			this.pbLogo = new System.Windows.Forms.PictureBox();
			this.notifyIconMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.btnWebsite = new System.Windows.Forms.ToolStripMenuItem();
			this.btnForums = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.btnQuit = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.btnStart)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
			this.notifyIconMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// notifyIcon
			// 
			this.notifyIcon.ContextMenuStrip = this.notifyIconMenu;
			this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
			this.notifyIcon.Text = "notifyIcon1";
			this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
			// 
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
			this.statusStrip.Location = new System.Drawing.Point(0, 140);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(454, 22);
			this.statusStrip.TabIndex = 2;
			this.statusStrip.Text = "statusStrip1";
			// 
			// lblStatus
			// 
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(29, 17);
			this.lblStatus.Text = "Idle.";
			// 
			// btnStart
			// 
			this.btnStart.Image = global::MapleLauncher.Properties.Resources.start;
			this.btnStart.Location = new System.Drawing.Point(315, 23);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(117, 76);
			this.btnStart.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.btnStart.TabIndex = 3;
			this.btnStart.TabStop = false;
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// pbLogo
			// 
			this.pbLogo.Image = global::MapleLauncher.Properties.Resources.mercedes_skill_build_guide_maplestory;
			this.pbLogo.Location = new System.Drawing.Point(0, 0);
			this.pbLogo.Name = "pbLogo";
			this.pbLogo.Size = new System.Drawing.Size(500, 150);
			this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pbLogo.TabIndex = 0;
			this.pbLogo.TabStop = false;
			// 
			// notifyIconMenu
			// 
			this.notifyIconMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnWebsite,
            this.btnForums,
            this.toolStripSeparator1,
            this.btnQuit});
			this.notifyIconMenu.Name = "notifyIconMenu";
			this.notifyIconMenu.Size = new System.Drawing.Size(153, 98);
			// 
			// btnWebsite
			// 
			this.btnWebsite.Name = "btnWebsite";
			this.btnWebsite.Size = new System.Drawing.Size(152, 22);
			this.btnWebsite.Text = "Website";
			this.btnWebsite.Click += new System.EventHandler(this.btnWebsite_Click);
			// 
			// btnForums
			// 
			this.btnForums.Name = "btnForums";
			this.btnForums.Size = new System.Drawing.Size(152, 22);
			this.btnForums.Text = "Forums";
			this.btnForums.Click += new System.EventHandler(this.btnForums_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
			// 
			// btnQuit
			// 
			this.btnQuit.Name = "btnQuit";
			this.btnQuit.Size = new System.Drawing.Size(152, 22);
			this.btnQuit.Text = "Quit";
			this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(454, 162);
			this.Controls.Add(this.btnStart);
			this.Controls.Add(this.statusStrip);
			this.Controls.Add(this.pbLogo);
			this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.Name = "frmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "MapleLauncher";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.Resize += new System.EventHandler(this.frmMain_Resize);
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.btnStart)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
			this.notifyIconMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pbLogo;
		private System.Windows.Forms.NotifyIcon notifyIcon;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripStatusLabel lblStatus;
		private System.Windows.Forms.PictureBox btnStart;
		private System.Windows.Forms.ContextMenuStrip notifyIconMenu;
		private System.Windows.Forms.ToolStripMenuItem btnWebsite;
		private System.Windows.Forms.ToolStripMenuItem btnForums;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem btnQuit;
	}
}

