using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DarkUI.Controls;
using DarkUI.Forms;
using Microsoft.VisualBasic;
using RetroElite.Properties;
using RetroElite.Utilidades.Configuracion;

namespace RetroElite.Forms
{
	// Token: 0x02000076 RID: 118
	public partial class GestionCuentas : DarkForm
	{
		// Token: 0x060004DF RID: 1247 RVA: 0x0001E611 File Offset: 0x0001CA11
		public GestionCuentas()
		{
			this.InitializeComponent();
			this.cuentas_cargadas = new List<CuentaConf>();
			this.comboBox_Servidor.SelectedIndex = 0;
			this.cargar_Cuentas_Lista();
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0001E648 File Offset: 0x0001CA48
		private void cargar_Cuentas_Lista()
		{
			this.listViewCuentas.Items.Clear();
			GlobalConf.get_Lista_Cuentas().ForEach(delegate(CuentaConf x)
			{
				bool flag = !Principal.cuentas_cargadas.ContainsKey(x.nombre_cuenta);
				if (flag)
				{
					this.listViewCuentas.Items.Add(x.nombre_cuenta).SubItems.AddRange(new string[]
					{
						x.servidor,
						x.nombre_personaje
					});
				}
			});
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0001E674 File Offset: 0x0001CA74
		private void boton_Agregar_Cuenta_Click(object sender, EventArgs e)
		{
			bool flag = GlobalConf.get_Cuenta(this.textBox_Nombre_Cuenta.Text) != null && GlobalConf.mostrar_mensajes_debug;
			if (flag)
			{
				MessageBox.Show("Il existe déjà un compte avec ce nom de compte", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			else
			{
				bool tiene_errores = false;
				Action<TextBox> <>9__1;
				this.tableLayoutPanel6.Controls.OfType<TableLayoutPanel>().ToList<TableLayoutPanel>().ForEach(delegate(TableLayoutPanel panel)
				{
					List<TextBox> list = panel.Controls.OfType<TextBox>().ToList<TextBox>();
					Action<TextBox> action;
					if ((action = <>9__1) == null)
					{
						action = (<>9__1 = delegate(TextBox textbox)
						{
							Console.WriteLine("TextBox: " + textbox.Name);
							bool flag3 = textbox.Name != "textBox_nombre_personaje";
							if (flag3)
							{
								bool flag4 = string.IsNullOrEmpty(textbox.Text) || textbox.Text.Split(new char[0]).Length > 1;
								if (flag4)
								{
									textbox.BackColor = Color.Red;
									tiene_errores = true;
								}
								else
								{
									textbox.BackColor = Color.White;
								}
							}
						});
					}
					list.ForEach(action);
				});
				bool flag2 = !tiene_errores;
				if (flag2)
				{
					GlobalConf.agregar_Cuenta(this.textBox_Nombre_Cuenta.Text, this.textBox_Password.Text, this.comboBox_Servidor.SelectedItem.ToString(), this.textBox_nombre_personaje.Text);
					this.cargar_Cuentas_Lista();
					this.textBox_Nombre_Cuenta.Clear();
					this.textBox_Password.Clear();
					this.textBox_nombre_personaje.Clear();
					bool @checked = this.checkBox_Agregar_Retroceder.Checked;
					if (@checked)
					{
						this.tabControlPrincipalCuentas.SelectedIndex = 0;
					}
					GlobalConf.guardar_Configuracion();
				}
			}
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0001E783 File Offset: 0x0001CB83
		private void listViewCuentas_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
		{
			e.Cancel = true;
			e.NewWidth = this.listViewCuentas.Columns[e.ColumnIndex].Width;
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0001E7B0 File Offset: 0x0001CBB0
		private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool flag = this.listViewCuentas.SelectedItems.Count > 0 && this.listViewCuentas.FocusedItem != null;
			if (flag)
			{
				foreach (object obj in this.listViewCuentas.SelectedItems)
				{
					ListViewItem listViewItem = (ListViewItem)obj;
					GlobalConf.eliminar_Cuenta(listViewItem.Index);
					listViewItem.Remove();
				}
				GlobalConf.guardar_Configuracion();
				this.cargar_Cuentas_Lista();
			}
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0001E858 File Offset: 0x0001CC58
		private void conectarToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool flag = this.listViewCuentas.SelectedItems.Count > 0 && this.listViewCuentas.FocusedItem != null;
			if (flag)
			{
				using (IEnumerator enumerator = this.listViewCuentas.SelectedItems.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ListViewItem cuenta = (ListViewItem)enumerator.Current;
						this.cuentas_cargadas.Add(GlobalConf.get_Lista_Cuentas().FirstOrDefault((CuentaConf f) => f.nombre_cuenta == cuenta.Text));
					}
				}
				base.DialogResult = DialogResult.OK;
				base.Close();
			}
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x0001E918 File Offset: 0x0001CD18
		public List<CuentaConf> get_Cuentas_Cargadas()
		{
			return this.cuentas_cargadas;
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0001E920 File Offset: 0x0001CD20
		private void listViewCuentas_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			this.conectarToolStripMenuItem.PerformClick();
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0001E930 File Offset: 0x0001CD30
		private void modificar_Cuenta(object sender, EventArgs e)
		{
			bool flag = this.listViewCuentas.SelectedItems.Count == 1 && this.listViewCuentas.FocusedItem != null;
			if (flag)
			{
				CuentaConf cuentaConf = GlobalConf.get_Cuenta(this.listViewCuentas.SelectedItems[0].Index);
				string text = sender.ToString();
				string text2 = text;
				if (text2 != null)
				{
					if (text2 == "Compte")
					{
						string text3 = Interaction.InputBox("Renseigne le nouveau compte", "Modifier compte", cuentaConf.nombre_cuenta, -1, -1);
						bool flag2 = !string.IsNullOrEmpty(text3) || text3.Split(new char[0]).Length == 0;
						if (flag2)
						{
							cuentaConf.nombre_cuenta = text3;
						}
						goto IL_127;
					}
					if (text2 == "Mot de passe")
					{
						string text4 = Interaction.InputBox("Renseigne le mot de passe", "Modifier mdp", cuentaConf.password, -1, -1);
						bool flag3 = !string.IsNullOrEmpty(text4) || text4.Split(new char[0]).Length == 0;
						if (flag3)
						{
							cuentaConf.password = text4;
						}
						goto IL_127;
					}
				}
				string nombre_personaje = Interaction.InputBox("Change le nom du personnage a connecté", "Modifier le nom du personnage", cuentaConf.nombre_personaje, -1, -1);
				cuentaConf.nombre_personaje = nombre_personaje;
				IL_127:
				GlobalConf.guardar_Configuracion();
				this.cargar_Cuentas_Lista();
			}
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0001EA72 File Offset: 0x0001CE72
		private void GestionCuentas_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0001EA72 File Offset: 0x0001CE72
		private void label1_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0001EA72 File Offset: 0x0001CE72
		private void listViewCuentas_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0001EA72 File Offset: 0x0001CE72
		private void pictureBox_informacion_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0001EA72 File Offset: 0x0001CE72
		private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
		{
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0001EA78 File Offset: 0x0001CE78
		private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
		{
			TabPage tabPage = this.tabControlPrincipalCuentas.TabPages[e.Index];
			Rectangle tabRect = this.tabControlPrincipalCuentas.GetTabRect(e.Index);
			SolidBrush solidBrush = new SolidBrush(Color.FromArgb(220, 220, 220));
			SolidBrush solidBrush2 = new SolidBrush(Color.FromArgb(60, 63, 65));
			StringFormat stringFormat = new StringFormat();
			stringFormat.Alignment = StringAlignment.Center;
			stringFormat.LineAlignment = StringAlignment.Center;
			bool flag = Convert.ToBoolean(e.State & DrawItemState.Selected);
			if (flag)
			{
				solidBrush.Color = Color.FromArgb(60, 63, 65);
				solidBrush2.Color = Color.FromArgb(220, 220, 220);
				tabRect.Inflate(2, 2);
			}
			e.Graphics.FillRectangle(solidBrush, tabRect);
			e.Graphics.DrawString(tabPage.Text, e.Font, solidBrush2, tabRect, stringFormat);
			e.Graphics.ResetTransform();
			solidBrush.Dispose();
			solidBrush2.Dispose();
		}

		// Token: 0x040002F7 RID: 759
		private List<CuentaConf> cuentas_cargadas;
	}
}
