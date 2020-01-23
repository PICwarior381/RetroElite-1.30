using System;
using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using DarkUI.Controls;
using DarkUI.Forms;
using RetroElite.Utilidades.Configuracion;

namespace RetroElite.Forms
{
	// Token: 0x02000077 RID: 119
	public partial class Opciones : DarkForm
	{
		// Token: 0x060004F1 RID: 1265 RVA: 0x00020DA4 File Offset: 0x0001F1A4
		public Opciones()
		{
			this.InitializeComponent();
			this.checkBox_mensajes_debug.Checked = GlobalConf.mostrar_mensajes_debug;
			this.textBox_ip_servidor.Text = GlobalConf.ip_conexion;
			this.textBox_puerto_servidor.Text = Convert.ToString(GlobalConf.puerto_conexion);
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00020E00 File Offset: 0x0001F200
		private void boton_opciones_guardar_Click(object sender, EventArgs e)
		{
			IPAddress ipaddress;
			bool flag = !IPAddress.TryParse(this.textBox_ip_servidor.Text, out ipaddress);
			if (flag)
			{
				this.textBox_ip_servidor.BackColor = Color.Red;
			}
			else
			{
				short num;
				bool flag2 = !short.TryParse(this.textBox_puerto_servidor.Text, out num);
				if (flag2)
				{
					this.textBox_puerto_servidor.BackColor = Color.Red;
				}
				else
				{
					GlobalConf.mostrar_mensajes_debug = this.checkBox_mensajes_debug.Checked;
					GlobalConf.ip_conexion = this.textBox_ip_servidor.Text;
					GlobalConf.puerto_conexion = short.Parse(this.textBox_puerto_servidor.Text);
					GlobalConf.guardar_Configuracion();
					base.Close();
				}
			}
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0001EA72 File Offset: 0x0001CE72
		private void checkBox_mensajes_debug_CheckedChanged(object sender, EventArgs e)
		{
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0001EA72 File Offset: 0x0001CE72
		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0001EA72 File Offset: 0x0001CE72
		private void label_ip_conexion_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0001EA72 File Offset: 0x0001CE72
		private void label_puerto_servidor_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0001EA72 File Offset: 0x0001CE72
		private void textBox_puerto_servidor_TextChanged(object sender, EventArgs e)
		{
		}
	}
}
