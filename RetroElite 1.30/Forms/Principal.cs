using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DarkUI.Forms;
using RetroElite.Controles.TabControl;
using RetroElite.Interfaces;
using RetroElite.Otros;
using RetroElite.Otros.Grupos;
using RetroElite.Properties;
using RetroElite.Utilidades.Configuracion;

namespace RetroElite.Forms
{
	// Token: 0x02000078 RID: 120
	public partial class Principal : DarkForm
	{
		// Token: 0x060004FA RID: 1274 RVA: 0x00021A21 File Offset: 0x0001FE21
		public Principal()
		{
			this.InitializeComponent();
			Principal.cuentas_cargadas = new Dictionary<string, Pagina>();
			Directory.CreateDirectory("map");
			Directory.CreateDirectory("item");
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00021A5C File Offset: 0x0001FE5C
		private void gestionDeCuentasToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.tabControlCuentas.Visible = true;
			using (GestionCuentas gestionCuentas = new GestionCuentas())
			{
				bool flag = gestionCuentas.ShowDialog() == DialogResult.OK;
				if (flag)
				{
					List<CuentaConf> cuentas_Cargadas = gestionCuentas.get_Cuentas_Cargadas();
					bool flag2 = cuentas_Cargadas.Count < 2;
					if (flag2)
					{
						CuentaConf cuentaConf = cuentas_Cargadas[0];
						Principal.cuentas_cargadas.Add(cuentaConf.nombre_cuenta, this.agregar_Nueva_Tab_Pagina(cuentaConf.nombre_cuenta, new UI_Principal(new Cuenta(cuentaConf)), "Aucun"));
					}
					else
					{
						CuentaConf cuentaConf2 = cuentas_Cargadas.First<CuentaConf>();
						Cuenta cuenta = new Cuenta(cuentaConf2);
						Grupo grupo = new Grupo(cuenta);
						Principal.cuentas_cargadas.Add(cuentaConf2.nombre_cuenta, this.agregar_Nueva_Tab_Pagina(cuentaConf2.nombre_cuenta, new UI_Principal(cuenta), cuentaConf2.nombre_cuenta));
						cuentas_Cargadas.Remove(cuentaConf2);
						foreach (CuentaConf cuentaConf3 in cuentas_Cargadas)
						{
							Cuenta cuenta2 = new Cuenta(cuentaConf3);
							grupo.agregar_Miembro(cuenta2);
							Principal.cuentas_cargadas.Add(cuentaConf3.nombre_cuenta, this.agregar_Nueva_Tab_Pagina(cuentaConf3.nombre_cuenta, new UI_Principal(cuenta2), grupo.lider.configuracion.nombre_cuenta));
						}
					}
				}
			}
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00021BF4 File Offset: 0x0001FFF4
		private Pagina agregar_Nueva_Tab_Pagina(string titulo, UserControl control, string nombre_grupo)
		{
			Pagina pagina = this.tabControlCuentas.agregar_Nueva_Pagina(titulo);
			pagina.cabezera.propiedad_Imagen = Resources.circulo_rojo;
			pagina.cabezera.propiedad_Estado = "Deconnecté";
			pagina.cabezera.propiedad_Grupo = nombre_grupo;
			pagina.contenido.Controls.Add(control);
			control.Dock = DockStyle.Fill;
			return pagina;
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00021C60 File Offset: 0x00020060
		private void opcionesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (Opciones opciones = new Opciones())
			{
				opciones.ShowDialog();
			}
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x00021C98 File Offset: 0x00020098
		private void tabControlCuentas_Load(object sender, EventArgs e)
		{
			this.tabControlCuentas.Visible = false;
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0001EA72 File Offset: 0x0001CE72
		private void Principal_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0001EA72 File Offset: 0x0001CE72
		private void statusStripInferiorPrincipal_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00021CA8 File Offset: 0x000200A8
		private void toolStripMenuItem1_Click(object sender, EventArgs e)
		{
			Process.Start("https://discord.gg/u3HCkrF");
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00021CB6 File Offset: 0x000200B6
		private void toolStripMenuItem2_Click(object sender, EventArgs e)
		{
			Process.Start("https://barbok.eratz.fr/");
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00021CC4 File Offset: 0x000200C4
		private void toolStripMenuItem3_Click(object sender, EventArgs e)
		{
			Process.Start("http://www.dofus.tools/");
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00021CD2 File Offset: 0x000200D2
		private void toolStripMenuItem4_Click(object sender, EventArgs e)
		{
			Process.Start("https://www.dofus.com/fr/forum/1580-serveurs-dofus-retro");
		}

		// Token: 0x0400032D RID: 813
		public static Dictionary<string, Pagina> cuentas_cargadas;
	}
}
