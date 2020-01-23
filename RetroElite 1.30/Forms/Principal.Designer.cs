namespace RetroElite.Forms
{
	// Token: 0x02000078 RID: 120
	public partial class Principal : global::DarkUI.Forms.DarkForm
	{
		// Token: 0x06000505 RID: 1285 RVA: 0x00021CE0 File Offset: 0x000200E0
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x00021D18 File Offset: 0x00020118
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::RetroElite.Forms.Principal));
			this.menuSuperiorPrincipal = new global::System.Windows.Forms.MenuStrip();
			this.gestionDeCuentasToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem4 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.statusStripInferiorPrincipal = new global::System.Windows.Forms.StatusStrip();
			this.tabControlCuentas = new global::RetroElite.Controles.TabControl.TabControl();
			this.menuSuperiorPrincipal.SuspendLayout();
			base.SuspendLayout();
			this.menuSuperiorPrincipal.AutoSize = false;
			this.menuSuperiorPrincipal.BackColor = global::System.Drawing.Color.FromArgb(65, 65, 65);
			this.menuSuperiorPrincipal.ImageScalingSize = new global::System.Drawing.Size(20, 20);
			this.menuSuperiorPrincipal.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.gestionDeCuentasToolStripMenuItem,
				this.toolStripMenuItem1,
				this.toolStripMenuItem2,
				this.toolStripMenuItem3,
				this.toolStripMenuItem4
			});
			this.menuSuperiorPrincipal.Location = new global::System.Drawing.Point(0, 0);
			this.menuSuperiorPrincipal.Name = "menuSuperiorPrincipal";
			this.menuSuperiorPrincipal.Padding = new global::System.Windows.Forms.Padding(7, 2, 0, 2);
			this.menuSuperiorPrincipal.Size = new global::System.Drawing.Size(1271, 46);
			this.menuSuperiorPrincipal.TabIndex = 0;
			this.menuSuperiorPrincipal.Text = "menuSuperiorPrincipal";
			this.gestionDeCuentasToolStripMenuItem.ForeColor = global::System.Drawing.Color.FloralWhite;
			this.gestionDeCuentasToolStripMenuItem.Image = global::RetroElite.Properties.Resources.gestion_cuentas;
			this.gestionDeCuentasToolStripMenuItem.Name = "gestionDeCuentasToolStripMenuItem";
			this.gestionDeCuentasToolStripMenuItem.Size = new global::System.Drawing.Size(169, 42);
			this.gestionDeCuentasToolStripMenuItem.Text = "Gestion de compte";
			this.gestionDeCuentasToolStripMenuItem.Click += new global::System.EventHandler(this.gestionDeCuentasToolStripMenuItem_Click);
			this.toolStripMenuItem1.ForeColor = global::System.Drawing.Color.FloralWhite;
			this.toolStripMenuItem1.Image = global::RetroElite.Properties.Resources.discord_logo;
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new global::System.Drawing.Size(94, 42);
			this.toolStripMenuItem1.Text = "Discord";
			this.toolStripMenuItem1.Click += new global::System.EventHandler(this.toolStripMenuItem1_Click);
			this.toolStripMenuItem2.ForeColor = global::System.Drawing.Color.FloralWhite;
			this.toolStripMenuItem2.Image = global::RetroElite.Properties.Resources.favicon;
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new global::System.Drawing.Size(90, 42);
			this.toolStripMenuItem2.Text = "Barbok";
			this.toolStripMenuItem2.Click += new global::System.EventHandler(this.toolStripMenuItem2_Click);
			this.toolStripMenuItem3.ForeColor = global::System.Drawing.Color.Gainsboro;
			this.toolStripMenuItem3.Image = global::RetroElite.Properties.Resources.boton_ajustes;
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new global::System.Drawing.Size(112, 42);
			this.toolStripMenuItem3.Text = "DofusTool";
			this.toolStripMenuItem3.Click += new global::System.EventHandler(this.toolStripMenuItem3_Click);
			this.toolStripMenuItem4.ForeColor = global::System.Drawing.Color.FloralWhite;
			this.toolStripMenuItem4.Image = global::RetroElite.Properties.Resources.favicon__1_;
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new global::System.Drawing.Size(126, 42);
			this.toolStripMenuItem4.Text = "Forum dofus";
			this.toolStripMenuItem4.Click += new global::System.EventHandler(this.toolStripMenuItem4_Click);
			this.statusStripInferiorPrincipal.BackColor = global::System.Drawing.Color.FromArgb(60, 63, 65);
			this.statusStripInferiorPrincipal.ImageScalingSize = new global::System.Drawing.Size(20, 20);
			this.statusStripInferiorPrincipal.Location = new global::System.Drawing.Point(0, 711);
			this.statusStripInferiorPrincipal.Name = "statusStripInferiorPrincipal";
			this.statusStripInferiorPrincipal.Padding = new global::System.Windows.Forms.Padding(1, 0, 16, 0);
			this.statusStripInferiorPrincipal.Size = new global::System.Drawing.Size(1271, 22);
			this.statusStripInferiorPrincipal.TabIndex = 1;
			this.statusStripInferiorPrincipal.Text = "statusStripInferiorPrincipal";
			this.statusStripInferiorPrincipal.ItemClicked += new global::System.Windows.Forms.ToolStripItemClickedEventHandler(this.statusStripInferiorPrincipal_ItemClicked);
			this.tabControlCuentas.AutoSize = true;
			this.tabControlCuentas.BackColor = global::System.Drawing.Color.FromArgb(60, 63, 65);
			this.tabControlCuentas.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.tabControlCuentas.Font = new global::System.Drawing.Font("Segoe UI", 9.75f);
			this.tabControlCuentas.Location = new global::System.Drawing.Point(0, 46);
			this.tabControlCuentas.Margin = new global::System.Windows.Forms.Padding(3, 5, 3, 5);
			this.tabControlCuentas.Name = "tabControlCuentas";
			this.tabControlCuentas.Size = new global::System.Drawing.Size(1271, 665);
			this.tabControlCuentas.TabIndex = 2;
			this.tabControlCuentas.Load += new global::System.EventHandler(this.tabControlCuentas_Load);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(9f, 18f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = global::System.Drawing.SystemColors.ControlDark;
			this.BackgroundImage = global::RetroElite.Properties.Resources.wp3076909;
			this.BackgroundImageLayout = global::System.Windows.Forms.ImageLayout.Stretch;
			base.ClientSize = new global::System.Drawing.Size(1271, 733);
			base.Controls.Add(this.tabControlCuentas);
			base.Controls.Add(this.statusStripInferiorPrincipal);
			base.Controls.Add(this.menuSuperiorPrincipal);
			base.FlatBorder = true;
			this.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.25f);
			this.ForeColor = global::System.Drawing.Color.FromArgb(192, 0, 0);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.MainMenuStrip = this.menuSuperiorPrincipal;
			this.MinimumSize = new global::System.Drawing.Size(1159, 772);
			base.Name = "Principal";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "RetroElite 1.30";
			base.Load += new global::System.EventHandler(this.Principal_Load);
			this.menuSuperiorPrincipal.ResumeLayout(false);
			this.menuSuperiorPrincipal.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400032E RID: 814
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x0400032F RID: 815
		private global::System.Windows.Forms.MenuStrip menuSuperiorPrincipal;

		// Token: 0x04000330 RID: 816
		private global::System.Windows.Forms.StatusStrip statusStripInferiorPrincipal;

		// Token: 0x04000331 RID: 817
		private global::RetroElite.Controles.TabControl.TabControl tabControlCuentas;

		// Token: 0x04000332 RID: 818
		private global::System.Windows.Forms.ToolStripMenuItem gestionDeCuentasToolStripMenuItem;

		// Token: 0x04000333 RID: 819
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;

		// Token: 0x04000334 RID: 820
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;

		// Token: 0x04000335 RID: 821
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;

		// Token: 0x04000336 RID: 822
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
	}
}
