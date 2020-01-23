using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DarkUI.Controls;
using RetroElite.Controles.ColorCheckBox;
using RetroElite.Controles.ProgresBar;
using RetroElite.Forms;
using RetroElite.Otros;
using RetroElite.Otros.Enums;
using RetroElite.Otros.Game.Personaje;
using RetroElite.Properties;
using RetroElite.Utilidades.Extensiones;
using RetroElite.Utilidades.Logs;

namespace RetroElite.Interfaces
{
	// Token: 0x02000074 RID: 116
	public class UI_Principal : UserControl
	{
		// Token: 0x060004B3 RID: 1203 RVA: 0x0001A962 File Offset: 0x00018D62
		public UI_Principal(Cuenta _cuenta)
		{
			this.InitializeComponent();
			this.cuenta = _cuenta;
			this.nombre_cuenta = this.cuenta.configuracion.nombre_cuenta;
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0001A998 File Offset: 0x00018D98
		private void UI_Principal_Load(object sender, EventArgs e)
		{
			this.desconectarOconectarToolStripMenuItem.Text = "Connexion";
			this.escribir_mensaje(string.Concat(new string[]
			{
				"[",
				DateTime.Now.ToString("HH:mm:ss"),
				"] -> [INFORMATION] RetroElite, version : ",
				Application.ProductVersion,
				" alpha"
			}), "F3FF00");
			this.cuenta.evento_estado_cuenta += this.eventos_Estados_Cuenta;
			this.cuenta.cuenta_desconectada += this.desconectar_Cuenta;
			this.cuenta.logger.log_evento += delegate(LogMensajes mensaje, string color)
			{
				this.escribir_mensaje(mensaje.ToString(), color);
			};
			this.cuenta.script.evento_script_cargado += this.evento_Scripts_Cargado;
			this.cuenta.script.evento_script_iniciado += this.evento_Scripts_Iniciado;
			this.cuenta.script.evento_script_detenido += this.evento_Scripts_Detenido;
			this.cuenta.juego.personaje.caracteristicas_actualizadas += this.caracteristicas_Actualizadas;
			this.cuenta.juego.personaje.pods_actualizados += this.pods_Actualizados;
			this.cuenta.juego.personaje.servidor_seleccionado += this.servidor_Seleccionado;
			this.cuenta.juego.personaje.personaje_seleccionado += this.personaje_Seleccionado;
			bool tiene_grupo = this.cuenta.tiene_grupo;
			if (tiene_grupo)
			{
				this.escribir_mensaje("[" + DateTime.Now.ToString("HH:mm:ss") + "] -> Chef de groupe : " + this.cuenta.grupo.lider.configuracion.nombre_cuenta, LogTipos.ERROR.ToString("X"));
			}
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0001AB94 File Offset: 0x00018F94
		private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool flag = Principal.cuentas_cargadas.ContainsKey(this.nombre_cuenta);
			if (flag)
			{
				bool flag2 = this.cuenta.tiene_grupo && this.cuenta.es_lider_grupo;
				if (flag2)
				{
					this.cuenta.grupo.desconectar_Cuentas();
				}
				else
				{
					bool tiene_grupo = this.cuenta.tiene_grupo;
					if (tiene_grupo)
					{
						this.cuenta.grupo.eliminar_Miembro(this.cuenta);
					}
				}
				this.cuenta.Dispose();
				Principal.cuentas_cargadas[this.nombre_cuenta].contenido.Dispose();
				Principal.cuentas_cargadas.Remove(this.nombre_cuenta);
			}
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0001AC4C File Offset: 0x0001904C
		private void cambiar_Tab_Imagen(Image image)
		{
			bool flag = Principal.cuentas_cargadas.ContainsKey(this.nombre_cuenta);
			if (flag)
			{
				Principal.cuentas_cargadas[this.nombre_cuenta].cabezera.propiedad_Imagen = image;
			}
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0001AC8A File Offset: 0x0001908A
		private void button_limpiar_consola_Click(object sender, EventArgs e)
		{
			this.textbox_logs.Clear();
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0001AC98 File Offset: 0x00019098
		private void desconectarToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool flag = this.desconectarOconectarToolStripMenuItem.Text.Equals("Connexion");
			if (flag)
			{
				while (this.tabControl_principal.TabPages.Count > 2)
				{
					this.tabControl_principal.TabPages.RemoveAt(2);
				}
				this.cuenta.conectar();
				this.cuenta.conexion.paquete_recibido += this.debugger.paquete_Recibido;
				this.cuenta.conexion.paquete_enviado += this.debugger.paquete_Enviado;
				this.cuenta.conexion.socket_informacion += new Action<string>(this.get_Mensajes_Socket_Informacion);
				this.desconectarOconectarToolStripMenuItem.Text = "Deconnexion";
			}
			else
			{
				bool flag2 = this.desconectarOconectarToolStripMenuItem.Text.Equals("Deconnexion");
				if (flag2)
				{
					this.cuenta.desconectar();
				}
			}
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0001AD94 File Offset: 0x00019194
		private void desconectar_Cuenta()
		{
			bool flag = !base.IsHandleCreated;
			if (!flag)
			{
				base.BeginInvoke(new Action(delegate()
				{
					this.cambiar_Todos_Controles_Chat(false);
					for (int i = 2; i < this.tabControl_principal.TabPages.Count; i++)
					{
						this.tabControl_principal.TabPages[i].Enabled = false;
					}
					this.desconectarOconectarToolStripMenuItem.Text = "Connexion";
				}));
			}
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0001ADC4 File Offset: 0x000191C4
		private void cambiar_Todos_Controles_Chat(bool estado)
		{
			base.BeginInvoke(new Action(delegate()
			{
				this.canal_informaciones.Enabled = estado;
				this.canal_general.Enabled = estado;
				this.canal_privado.Enabled = estado;
				this.canal_gremio.Enabled = estado;
				this.canal_alineamiento.Enabled = estado;
				this.canal_reclutamiento.Enabled = estado;
				this.canal_comercio.Enabled = estado;
				this.canal_incarnam.Enabled = estado;
				this.comboBox_lista_canales.Enabled = estado;
				this.textBox_enviar_consola.Enabled = estado;
				this.cargarScriptToolStripMenuItem.Enabled = estado;
			}));
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0001ADFC File Offset: 0x000191FC
		private void eventos_Estados_Cuenta()
		{
			EstadoCuenta estado_Cuenta = this.cuenta.Estado_Cuenta;
			EstadoCuenta estadoCuenta = estado_Cuenta;
			if (estadoCuenta != EstadoCuenta.CONECTANDO)
			{
				if (estadoCuenta != EstadoCuenta.DESCONECTADO)
				{
					this.cambiar_Tab_Imagen(Resources.circulo_verde);
				}
				else
				{
					this.cambiar_Tab_Imagen(Resources.circulo_rojo);
				}
			}
			else
			{
				this.cambiar_Tab_Imagen(Resources.circulo_naranja);
			}
			bool flag = this.cuenta != null && Principal.cuentas_cargadas.ContainsKey(this.nombre_cuenta);
			if (flag)
			{
				Principal.cuentas_cargadas[this.nombre_cuenta].cabezera.propiedad_Estado = this.cuenta.Estado_Cuenta.cadena_Amigable();
			}
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0001AE98 File Offset: 0x00019298
		private void agregar_Tab_Pagina(string nombre, UserControl control, int imagen_index)
		{
			Console.WriteLine("Action --> " + nombre);
			this.tabControl_principal.BeginInvoke(new Action(delegate()
			{
				control.Dock = DockStyle.Fill;
				TabPage tabPage = new TabPage(nombre)
				{
					ImageIndex = imagen_index
				};
				tabPage.Controls.Add(control);
				bool flag = !this.tabControl_principal.TabPages.Contains(tabPage);
				if (flag)
				{
					this.tabControl_principal.TabPages.Add(tabPage);
				}
			}));
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0001AEF6 File Offset: 0x000192F6
		private void cargar_Canales_Chat()
		{
			base.BeginInvoke(new Action(delegate()
			{
				this.canal_informaciones.Checked = this.cuenta.juego.personaje.canales.Contains("i");
				this.canal_general.Checked = this.cuenta.juego.personaje.canales.Contains("*");
				this.canal_privado.Checked = this.cuenta.juego.personaje.canales.Contains("#");
				this.canal_gremio.Checked = this.cuenta.juego.personaje.canales.Contains("%");
				this.canal_alineamiento.Checked = this.cuenta.juego.personaje.canales.Contains("!");
				this.canal_reclutamiento.Checked = this.cuenta.juego.personaje.canales.Contains("?");
				this.canal_comercio.Checked = this.cuenta.juego.personaje.canales.Contains(":");
				this.canal_incarnam.Checked = this.cuenta.juego.personaje.canales.Contains("^");
				this.comboBox_lista_canales.SelectedIndex = 0;
			}));
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0001AF0C File Offset: 0x0001930C
		private void canal_Chat_Click(object sender, EventArgs e)
		{
			Cuenta cuenta = this.cuenta;
			bool flag;
			if (cuenta == null || cuenta.Estado_Cuenta != EstadoCuenta.DESCONECTADO)
			{
				Cuenta cuenta2 = this.cuenta;
				flag = (cuenta2 == null || cuenta2.Estado_Cuenta > EstadoCuenta.CONECTANDO);
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			if (flag2)
			{
				string[] array = new string[]
				{
					"i",
					"*",
					"#$p",
					"%",
					"!",
					"?",
					":",
					"^"
				};
				CheckBox checkBox = sender as CheckBox;
				this.cuenta.conexion.enviar_Paquete((checkBox.Checked ? "cC+" : "cC-") + array[checkBox.TabIndex], false);
			}
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0001AFD8 File Offset: 0x000193D8
		private void textBox_enviar_consola_KeyDown(object sender, KeyEventArgs e)
		{
			bool flag = e.KeyCode == Keys.Return && this.textBox_enviar_consola.TextLength > 0 && this.textBox_enviar_consola.TextLength < 255;
			if (flag)
			{
				string text = this.textBox_enviar_consola.Text.ToUpper();
				string text2 = text;
				if (text2 != null)
				{
					if (text2 == "/MAPID")
					{
						this.escribir_mensaje(this.cuenta.juego.mapa.id.ToString(), "0040FF");
						goto IL_232;
					}
					if (text2 == "/CELLID")
					{
						this.escribir_mensaje(this.cuenta.juego.personaje.celda.id.ToString(), "0040FF");
						goto IL_232;
					}
					if (text2 == "/PING")
					{
						bool flag2 = this.cuenta.conexion != null;
						if (flag2)
						{
							this.cuenta.conexion.enviar_Paquete("ping", true);
						}
						else
						{
							this.escribir_mensaje("Vous n'êtes pas connecté ...", "0040FF");
						}
						goto IL_232;
					}
				}
				switch (this.comboBox_lista_canales.SelectedIndex)
				{
				case 0:
					this.cuenta.conexion.enviar_Paquete("BM*|" + this.textBox_enviar_consola.Text + "|", true);
					break;
				case 1:
					this.cuenta.conexion.enviar_Paquete("BM?|" + this.textBox_enviar_consola.Text + "|", true);
					break;
				case 2:
					this.cuenta.conexion.enviar_Paquete("BM:|" + this.textBox_enviar_consola.Text + "|", true);
					break;
				case 3:
					this.cuenta.conexion.enviar_Paquete(string.Concat(new string[]
					{
						"BM",
						this.textBox_nombre_privado.Text,
						"|",
						this.textBox_enviar_consola.Text,
						"|"
					}), true);
					break;
				}
				IL_232:
				e.Handled = true;
				e.SuppressKeyPress = true;
				this.textBox_nombre_privado.Clear();
				this.textBox_enviar_consola.Clear();
			}
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0001B240 File Offset: 0x00019640
		private void comboBox_lista_canales_Valor_Cambiado(object sender, EventArgs e)
		{
			ComboBox comboBox = sender as ComboBox;
			this.textBox_nombre_privado.Enabled = (comboBox.SelectedIndex == 3);
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0001B26C File Offset: 0x0001966C
		private void cargarScriptToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				using (OpenFileDialog openFileDialog = new OpenFileDialog())
				{
					openFileDialog.Title = "Selectionner le script à charger";
					openFileDialog.Filter = "Extension (.lua) | *.lua";
					bool flag = openFileDialog.ShowDialog() == DialogResult.OK;
					if (flag)
					{
						this.cuenta.script.get_Desde_Archivo(openFileDialog.FileName);
					}
				}
			}
			catch (Exception ex)
			{
				this.cuenta.logger.log_Error("SCRIPT", ex.Message);
			}
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0001B310 File Offset: 0x00019710
		private void caracteristicas_Actualizadas()
		{
			base.BeginInvoke(new Action(delegate()
			{
				PersonajeJuego personaje = this.cuenta.juego.personaje;
				this.progresBar_vitalidad.valor_Maximo = personaje.caracteristicas.vitalidad_maxima;
				this.progresBar_vitalidad.Valor = personaje.caracteristicas.vitalidad_actual;
				this.progresBar_energia.valor_Maximo = personaje.caracteristicas.maxima_energia;
				this.progresBar_energia.Valor = personaje.caracteristicas.energia_actual;
				this.progresBar_experiencia.Text = personaje.nivel.ToString();
				this.progresBar_energia.TextChanged += this.progresBar_energia_TextChanged;
				this.progresBar_experiencia.Valor = personaje.porcentaje_experiencia;
				this.label_kamas_principal.Text = personaje.kamas.ToString("0,0");
			}));
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0001B326 File Offset: 0x00019726
		private void progresBar_energia_TextChanged(object sender, EventArgs e)
		{
			PersonajeJuego personaje = this.cuenta.juego.personaje;
			personaje.nivel += 1;
			this.caracteristicas_Actualizadas();
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0001B34F File Offset: 0x0001974F
		private void pods_Actualizados()
		{
			base.BeginInvoke(new Action(delegate()
			{
				this.progresBar_pods.valor_Maximo = (int)this.cuenta.juego.personaje.inventario.pods_maximos;
				this.progresBar_pods.Valor = (int)this.cuenta.juego.personaje.inventario.pods_actuales;
			}));
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0001B365 File Offset: 0x00019765
		private void servidor_Seleccionado()
		{
			this.agregar_Tab_Pagina("Personnage", new UI_Personaje(this.cuenta), 2);
			this.agregar_Tab_Pagina("Inventaire", new UI_Inventario(this.cuenta), 3);
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0001B398 File Offset: 0x00019798
		private void personaje_Seleccionado()
		{
			this.cuenta.pelea_extension.configuracion.cargar();
			Console.WriteLine("Personnage select ...");
			this.agregar_Tab_Pagina("Map", new UI_Mapa(this.cuenta), 4);
			this.agregar_Tab_Pagina("Combat", new UI_Pelea(this.cuenta), 5);
			this.cambiar_Todos_Controles_Chat(true);
			this.cargar_Canales_Chat();
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0001B408 File Offset: 0x00019808
		private void get_Mensajes_Socket_Informacion(object error)
		{
			this.escribir_mensaje("[" + DateTime.Now.ToString("HH:mm:ss") + "] [Connexion] " + ((error != null) ? error.ToString() : null), LogTipos.PELIGRO.ToString("X"));
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0001B460 File Offset: 0x00019860
		private void escribir_mensaje(string mensaje, string color)
		{
			bool flag = !base.IsHandleCreated;
			if (!flag)
			{
				this.textbox_logs.BeginInvoke(new Action(delegate()
				{
					this.textbox_logs.Select(this.textbox_logs.TextLength, 0);
					this.textbox_logs.SelectionColor = ColorTranslator.FromHtml("#" + color);
					this.textbox_logs.AppendText(mensaje + Environment.NewLine);
					this.textbox_logs.ScrollToCaret();
				}));
			}
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0001B4B0 File Offset: 0x000198B0
		private void iniciarScriptToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool flag = !this.cuenta.script.activado;
			if (flag)
			{
				this.cuenta.script.activar_Script();
			}
			else
			{
				this.cuenta.script.detener_Script("script");
			}
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0001B500 File Offset: 0x00019900
		private void evento_Scripts_Cargado(string nombre)
		{
			this.cuenta.logger.log_Script("SCRIPT", "'" + nombre + "' chargé.");
			base.BeginInvoke(new Action(delegate()
			{
				this.NomScript.Text = (((nombre.Length > 16) ? nombre.Substring(0, 16) : nombre) ?? "");
				this.iniciarScriptToolStripMenuItem.Enabled = true;
			}));
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0001B560 File Offset: 0x00019960
		private void evento_Scripts_Iniciado()
		{
			this.cuenta.logger.log_Script("SCRIPT", "Initialisation");
			base.BeginInvoke(new Action(delegate()
			{
				this.cargarScriptToolStripMenuItem.Enabled = false;
				this.iniciarScriptToolStripMenuItem.Image = Resources.boton_stop;
			}));
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0001B594 File Offset: 0x00019994
		private void evento_Scripts_Detenido(string motivo)
		{
			bool flag = string.IsNullOrEmpty(motivo);
			if (flag)
			{
				this.cuenta.logger.log_Script("SCRIPT", "STOP");
			}
			else
			{
				this.cuenta.logger.log_Script("SCRIPT", "STOP " + motivo);
			}
			base.BeginInvoke(new Action(delegate()
			{
				this.iniciarScriptToolStripMenuItem.Image = Resources.boton_play;
				this.cargarScriptToolStripMenuItem.Enabled = true;
				this.NomScript.Text = "Script fini";
			}));
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x0001B600 File Offset: 0x00019A00
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0001B638 File Offset: 0x00019A38
		private void InitializeComponent()
		{
			this.components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(UI_Principal));
			this.menuStrip1 = new MenuStrip();
			this.desconectarOconectarToolStripMenuItem = new ToolStripMenuItem();
			this.eliminarToolStripMenuItem = new ToolStripMenuItem();
			this.cargarScriptToolStripMenuItem = new ToolStripMenuItem();
			this.iniciarScriptToolStripMenuItem = new ToolStripMenuItem();
			this.NomScript = new ToolStripMenuItem();
			this.tabControl_principal = new TabControl();
			this.tabPage_consola = new TabPage();
			this.tableLayout_Canales = new TableLayoutPanel();
			this.canal_incarnam = new ColorCheckBox();
			this.canal_informaciones = new ColorCheckBox();
			this.canal_comercio = new ColorCheckBox();
			this.canal_alineamiento = new ColorCheckBox();
			this.canal_reclutamiento = new ColorCheckBox();
			this.canal_gremio = new ColorCheckBox();
			this.canal_privado = new ColorCheckBox();
			this.canal_general = new ColorCheckBox();
			this.textbox_logs = new RichTextBox();
			this.tableLayoutPanel1 = new TableLayoutPanel();
			this.textBox_nombre_privado = new DarkTextBox();
			this.comboBox_lista_canales = new ComboBox();
			this.button_limpiar_consola = new Button();
			this.textBox_enviar_consola = new DarkTextBox();
			this.tabPage2 = new TabPage();
			this.debugger = new UI_Debugger();
			this.lista_imagenes = new ImageList(this.components);
			this.tableLayoutPanel4 = new TableLayoutPanel();
			this.pictureBox1 = new PictureBox();
			this.pictureBox2 = new PictureBox();
			this.pictureBox3 = new PictureBox();
			this.pictureBox4 = new PictureBox();
			this.pictureBox5 = new PictureBox();
			this.progresBar_vitalidad = new ProgresBar();
			this.progresBar_energia = new ProgresBar();
			this.progresBar_experiencia = new ProgresBar();
			this.progresBar_pods = new ProgresBar();
			this.label_kamas_principal = new Label();
			this.ScriptTituloStripMenuItem = new Label();
			this.menuStrip1.SuspendLayout();
			this.tabControl_principal.SuspendLayout();
			this.tabPage_consola.SuspendLayout();
			this.tableLayout_Canales.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tableLayoutPanel4.SuspendLayout();
			((ISupportInitialize)this.pictureBox1).BeginInit();
			((ISupportInitialize)this.pictureBox2).BeginInit();
			((ISupportInitialize)this.pictureBox3).BeginInit();
			((ISupportInitialize)this.pictureBox4).BeginInit();
			((ISupportInitialize)this.pictureBox5).BeginInit();
			base.SuspendLayout();
			this.menuStrip1.BackColor = Color.FromArgb(60, 63, 65);
			this.menuStrip1.ImageScalingSize = new Size(20, 20);
			this.menuStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.desconectarOconectarToolStripMenuItem,
				this.eliminarToolStripMenuItem,
				this.cargarScriptToolStripMenuItem,
				this.iniciarScriptToolStripMenuItem,
				this.NomScript
			});
			this.menuStrip1.Location = new Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Padding = new Padding(7, 3, 0, 3);
			this.menuStrip1.Size = new Size(1005, 30);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			this.desconectarOconectarToolStripMenuItem.BackColor = Color.FromArgb(60, 63, 65);
			this.desconectarOconectarToolStripMenuItem.ForeColor = Color.Gainsboro;
			this.desconectarOconectarToolStripMenuItem.Image = (Image)componentResourceManager.GetObject("desconectarOconectarToolStripMenuItem.Image");
			this.desconectarOconectarToolStripMenuItem.Name = "desconectarOconectarToolStripMenuItem";
			this.desconectarOconectarToolStripMenuItem.Size = new Size(130, 24);
			this.desconectarOconectarToolStripMenuItem.Text = "Deconnexion";
			this.desconectarOconectarToolStripMenuItem.Click += this.desconectarToolStripMenuItem_Click;
			this.eliminarToolStripMenuItem.BackColor = Color.FromArgb(60, 63, 65);
			this.eliminarToolStripMenuItem.ForeColor = Color.Gainsboro;
			this.eliminarToolStripMenuItem.Image = (Image)componentResourceManager.GetObject("eliminarToolStripMenuItem.Image");
			this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
			this.eliminarToolStripMenuItem.Size = new Size(91, 24);
			this.eliminarToolStripMenuItem.Text = "Enlever";
			this.eliminarToolStripMenuItem.Click += this.eliminarToolStripMenuItem_Click;
			this.cargarScriptToolStripMenuItem.BackColor = Color.FromArgb(60, 63, 65);
			this.cargarScriptToolStripMenuItem.Enabled = false;
			this.cargarScriptToolStripMenuItem.ForeColor = Color.Gainsboro;
			this.cargarScriptToolStripMenuItem.Image = Resources.documento_azul;
			this.cargarScriptToolStripMenuItem.Name = "cargarScriptToolStripMenuItem";
			this.cargarScriptToolStripMenuItem.Size = new Size(155, 24);
			this.cargarScriptToolStripMenuItem.Text = "Charger un script";
			this.cargarScriptToolStripMenuItem.Click += this.cargarScriptToolStripMenuItem_Click;
			this.iniciarScriptToolStripMenuItem.Enabled = false;
			this.iniciarScriptToolStripMenuItem.ForeColor = Color.FromArgb(60, 63, 65);
			this.iniciarScriptToolStripMenuItem.Image = Resources.boton_play;
			this.iniciarScriptToolStripMenuItem.Name = "iniciarScriptToolStripMenuItem";
			this.iniciarScriptToolStripMenuItem.Size = new Size(34, 24);
			this.iniciarScriptToolStripMenuItem.Click += this.iniciarScriptToolStripMenuItem_Click;
			this.NomScript.ForeColor = Color.CornflowerBlue;
			this.NomScript.Name = "NomScript";
			this.NomScript.Size = new Size(14, 24);
			this.tabControl_principal.Controls.Add(this.tabPage_consola);
			this.tabControl_principal.Controls.Add(this.tabPage2);
			this.tabControl_principal.Dock = DockStyle.Fill;
			this.tabControl_principal.ImageList = this.lista_imagenes;
			this.tabControl_principal.ItemSize = new Size(67, 26);
			this.tabControl_principal.Location = new Point(0, 30);
			this.tabControl_principal.Name = "tabControl_principal";
			this.tabControl_principal.SelectedIndex = 0;
			this.tabControl_principal.Size = new Size(1005, 690);
			this.tabControl_principal.TabIndex = 7;
			this.tabPage_consola.BackColor = Color.FromArgb(60, 63, 65);
			this.tabPage_consola.Controls.Add(this.tableLayout_Canales);
			this.tabPage_consola.Controls.Add(this.textbox_logs);
			this.tabPage_consola.Controls.Add(this.tableLayoutPanel1);
			this.tabPage_consola.ImageIndex = 0;
			this.tabPage_consola.Location = new Point(4, 30);
			this.tabPage_consola.Name = "tabPage_consola";
			this.tabPage_consola.Padding = new Padding(3);
			this.tabPage_consola.Size = new Size(997, 656);
			this.tabPage_consola.TabIndex = 0;
			this.tabPage_consola.Text = "Console";
			this.tableLayout_Canales.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.tableLayout_Canales.ColumnCount = 1;
			this.tableLayout_Canales.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayout_Canales.Controls.Add(this.canal_incarnam, 0, 7);
			this.tableLayout_Canales.Controls.Add(this.canal_informaciones, 0, 0);
			this.tableLayout_Canales.Controls.Add(this.canal_comercio, 0, 6);
			this.tableLayout_Canales.Controls.Add(this.canal_alineamiento, 0, 4);
			this.tableLayout_Canales.Controls.Add(this.canal_reclutamiento, 0, 5);
			this.tableLayout_Canales.Controls.Add(this.canal_gremio, 0, 3);
			this.tableLayout_Canales.Controls.Add(this.canal_privado, 0, 2);
			this.tableLayout_Canales.Controls.Add(this.canal_general, 0, 1);
			this.tableLayout_Canales.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
			this.tableLayout_Canales.Location = new Point(969, 366);
			this.tableLayout_Canales.Name = "tableLayout_Canales";
			this.tableLayout_Canales.RowCount = 8;
			this.tableLayout_Canales.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5f));
			this.tableLayout_Canales.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5f));
			this.tableLayout_Canales.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5f));
			this.tableLayout_Canales.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5f));
			this.tableLayout_Canales.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5f));
			this.tableLayout_Canales.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5f));
			this.tableLayout_Canales.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5f));
			this.tableLayout_Canales.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5f));
			this.tableLayout_Canales.Size = new Size(22, 256);
			this.tableLayout_Canales.TabIndex = 0;
			this.canal_incarnam.AutoSize = true;
			this.canal_incarnam.BackColor = Color.Blue;
			this.canal_incarnam.Dock = DockStyle.Bottom;
			this.canal_incarnam.Enabled = false;
			this.canal_incarnam.ForeColor = Color.Black;
			this.canal_incarnam.Location = new Point(3, 236);
			this.canal_incarnam.Name = "canal_incarnam";
			this.canal_incarnam.Size = new Size(16, 17);
			this.canal_incarnam.TabIndex = 7;
			this.canal_incarnam.UseVisualStyleBackColor = false;
			this.canal_incarnam.Click += this.canal_Chat_Click;
			this.canal_informaciones.AutoSize = true;
			this.canal_informaciones.BackColor = Color.Green;
			this.canal_informaciones.Dock = DockStyle.Bottom;
			this.canal_informaciones.Enabled = false;
			this.canal_informaciones.ForeColor = Color.Black;
			this.canal_informaciones.Location = new Point(3, 12);
			this.canal_informaciones.Name = "canal_informaciones";
			this.canal_informaciones.Size = new Size(16, 17);
			this.canal_informaciones.TabIndex = 0;
			this.canal_informaciones.UseVisualStyleBackColor = false;
			this.canal_informaciones.Click += this.canal_Chat_Click;
			this.canal_comercio.AutoSize = true;
			this.canal_comercio.BackColor = Color.FromArgb(96, 62, 28);
			this.canal_comercio.Dock = DockStyle.Bottom;
			this.canal_comercio.Enabled = false;
			this.canal_comercio.ForeColor = Color.Black;
			this.canal_comercio.Location = new Point(3, 204);
			this.canal_comercio.Name = "canal_comercio";
			this.canal_comercio.Size = new Size(16, 17);
			this.canal_comercio.TabIndex = 6;
			this.canal_comercio.UseVisualStyleBackColor = false;
			this.canal_comercio.Click += this.canal_Chat_Click;
			this.canal_alineamiento.AutoSize = true;
			this.canal_alineamiento.BackColor = Color.FromArgb(255, 146, 69);
			this.canal_alineamiento.Dock = DockStyle.Bottom;
			this.canal_alineamiento.Enabled = false;
			this.canal_alineamiento.ForeColor = Color.Black;
			this.canal_alineamiento.Location = new Point(3, 140);
			this.canal_alineamiento.Name = "canal_alineamiento";
			this.canal_alineamiento.Size = new Size(16, 17);
			this.canal_alineamiento.TabIndex = 4;
			this.canal_alineamiento.UseVisualStyleBackColor = false;
			this.canal_alineamiento.Click += this.canal_Chat_Click;
			this.canal_reclutamiento.AutoSize = true;
			this.canal_reclutamiento.BackColor = Color.FromArgb(135, 133, 135);
			this.canal_reclutamiento.Dock = DockStyle.Bottom;
			this.canal_reclutamiento.Enabled = false;
			this.canal_reclutamiento.ForeColor = Color.Black;
			this.canal_reclutamiento.Location = new Point(3, 172);
			this.canal_reclutamiento.Name = "canal_reclutamiento";
			this.canal_reclutamiento.Size = new Size(16, 17);
			this.canal_reclutamiento.TabIndex = 5;
			this.canal_reclutamiento.UseVisualStyleBackColor = false;
			this.canal_reclutamiento.Click += this.canal_Chat_Click;
			this.canal_gremio.AutoSize = true;
			this.canal_gremio.BackColor = Color.FromArgb(103, 48, 160);
			this.canal_gremio.Dock = DockStyle.Bottom;
			this.canal_gremio.Enabled = false;
			this.canal_gremio.ForeColor = Color.Black;
			this.canal_gremio.Location = new Point(3, 108);
			this.canal_gremio.Name = "canal_gremio";
			this.canal_gremio.Size = new Size(16, 17);
			this.canal_gremio.TabIndex = 3;
			this.canal_gremio.UseVisualStyleBackColor = false;
			this.canal_gremio.Click += this.canal_Chat_Click;
			this.canal_privado.AutoSize = true;
			this.canal_privado.BackColor = Color.FromArgb(1, 112, 196);
			this.canal_privado.Dock = DockStyle.Bottom;
			this.canal_privado.Enabled = false;
			this.canal_privado.ForeColor = Color.Black;
			this.canal_privado.Location = new Point(3, 76);
			this.canal_privado.Name = "canal_privado";
			this.canal_privado.Size = new Size(16, 17);
			this.canal_privado.TabIndex = 2;
			this.canal_privado.UseVisualStyleBackColor = false;
			this.canal_privado.Click += this.canal_Chat_Click;
			this.canal_general.AutoSize = true;
			this.canal_general.BackColor = Color.Black;
			this.canal_general.Dock = DockStyle.Bottom;
			this.canal_general.Enabled = false;
			this.canal_general.ForeColor = Color.Black;
			this.canal_general.Location = new Point(3, 44);
			this.canal_general.Name = "canal_general";
			this.canal_general.Size = new Size(16, 17);
			this.canal_general.TabIndex = 1;
			this.canal_general.UseVisualStyleBackColor = false;
			this.canal_general.Click += this.canal_Chat_Click;
			this.textbox_logs.BackColor = Color.FromArgb(60, 63, 65);
			this.textbox_logs.BorderStyle = BorderStyle.None;
			this.textbox_logs.Dock = DockStyle.Fill;
			this.textbox_logs.ForeColor = Color.Gainsboro;
			this.textbox_logs.Location = new Point(3, 3);
			this.textbox_logs.MaxLength = 200;
			this.textbox_logs.Name = "textbox_logs";
			this.textbox_logs.Size = new Size(991, 625);
			this.textbox_logs.TabIndex = 5;
			this.textbox_logs.Text = "";
			this.tableLayoutPanel1.ColumnCount = 4;
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18.76676f));
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 21.31367f));
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 59.91957f));
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 79f));
			this.tableLayoutPanel1.Controls.Add(this.textBox_nombre_privado, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.comboBox_lista_canales, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.button_limpiar_consola, 3, 0);
			this.tableLayoutPanel1.Controls.Add(this.textBox_enviar_consola, 2, 0);
			this.tableLayoutPanel1.Dock = DockStyle.Bottom;
			this.tableLayoutPanel1.Location = new Point(3, 628);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel1.Size = new Size(991, 25);
			this.tableLayoutPanel1.TabIndex = 0;
			this.textBox_nombre_privado.BackColor = Color.FromArgb(69, 73, 74);
			this.textBox_nombre_privado.BorderStyle = BorderStyle.FixedSingle;
			this.textBox_nombre_privado.Dock = DockStyle.Fill;
			this.textBox_nombre_privado.Enabled = false;
			this.textBox_nombre_privado.ForeColor = Color.FromArgb(220, 220, 220);
			this.textBox_nombre_privado.ImeMode = ImeMode.NoControl;
			this.textBox_nombre_privado.Location = new Point(174, 3);
			this.textBox_nombre_privado.MaxLength = 80;
			this.textBox_nombre_privado.Name = "textBox_nombre_privado";
			this.textBox_nombre_privado.Size = new Size(188, 29);
			this.textBox_nombre_privado.TabIndex = 3;
			this.comboBox_lista_canales.BackColor = Color.FromArgb(60, 63, 65);
			this.comboBox_lista_canales.Dock = DockStyle.Fill;
			this.comboBox_lista_canales.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_lista_canales.Enabled = false;
			this.comboBox_lista_canales.FlatStyle = FlatStyle.Flat;
			this.comboBox_lista_canales.ForeColor = Color.Cyan;
			this.comboBox_lista_canales.FormattingEnabled = true;
			this.comboBox_lista_canales.Items.AddRange(new object[]
			{
				"General",
				"Recrutement",
				"Commerce",
				"MP"
			});
			this.comboBox_lista_canales.Location = new Point(3, 3);
			this.comboBox_lista_canales.Name = "comboBox_lista_canales";
			this.comboBox_lista_canales.Size = new Size(165, 29);
			this.comboBox_lista_canales.TabIndex = 2;
			this.comboBox_lista_canales.SelectedIndexChanged += this.comboBox_lista_canales_Valor_Cambiado;
			this.button_limpiar_consola.Dock = DockStyle.Fill;
			this.button_limpiar_consola.FlatStyle = FlatStyle.Flat;
			this.button_limpiar_consola.Image = (Image)componentResourceManager.GetObject("button_limpiar_consola.Image");
			this.button_limpiar_consola.Location = new Point(914, 3);
			this.button_limpiar_consola.Name = "button_limpiar_consola";
			this.button_limpiar_consola.Size = new Size(74, 19);
			this.button_limpiar_consola.TabIndex = 1;
			this.button_limpiar_consola.UseVisualStyleBackColor = true;
			this.button_limpiar_consola.Click += this.button_limpiar_consola_Click;
			this.textBox_enviar_consola.BackColor = Color.FromArgb(69, 73, 74);
			this.textBox_enviar_consola.BorderStyle = BorderStyle.FixedSingle;
			this.textBox_enviar_consola.Dock = DockStyle.Fill;
			this.textBox_enviar_consola.Enabled = false;
			this.textBox_enviar_consola.ForeColor = Color.FromArgb(220, 220, 220);
			this.textBox_enviar_consola.ImeMode = ImeMode.NoControl;
			this.textBox_enviar_consola.Location = new Point(368, 3);
			this.textBox_enviar_consola.MaxLength = 80;
			this.textBox_enviar_consola.Name = "textBox_enviar_consola";
			this.textBox_enviar_consola.Size = new Size(540, 29);
			this.textBox_enviar_consola.TabIndex = 0;
			this.textBox_enviar_consola.KeyDown += this.textBox_enviar_consola_KeyDown;
			this.tabPage2.Controls.Add(this.debugger);
			this.tabPage2.ImageIndex = 1;
			this.tabPage2.Location = new Point(4, 30);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new Padding(3);
			this.tabPage2.Size = new Size(997, 656);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Debugger";
			this.tabPage2.UseVisualStyleBackColor = true;
			this.debugger.Cursor = Cursors.Default;
			this.debugger.Dock = DockStyle.Fill;
			this.debugger.Font = new Font("Segoe UI", 9.75f);
			this.debugger.Location = new Point(3, 3);
			this.debugger.Margin = new Padding(3, 4, 3, 4);
			this.debugger.MinimumSize = new Size(790, 500);
			this.debugger.Name = "debugger";
			this.debugger.Size = new Size(991, 650);
			this.debugger.TabIndex = 0;
			this.lista_imagenes.ImageStream = (ImageListStreamer)componentResourceManager.GetObject("lista_imagenes.ImageStream");
			this.lista_imagenes.TransparentColor = Color.Transparent;
			this.lista_imagenes.Images.SetKeyName(0, "terminal.png");
			this.lista_imagenes.Images.SetKeyName(1, "debugger.png");
			this.lista_imagenes.Images.SetKeyName(2, "personaje.png");
			this.lista_imagenes.Images.SetKeyName(3, "inventario.png");
			this.lista_imagenes.Images.SetKeyName(4, "mapa.png");
			this.lista_imagenes.Images.SetKeyName(5, "pelea.png");
			this.lista_imagenes.Images.SetKeyName(6, "bolsa_dinero.png");
			this.lista_imagenes.Images.SetKeyName(7, "ajustes.png");
			this.tableLayoutPanel4.BackColor = Color.FromArgb(60, 63, 65);
			this.tableLayoutPanel4.ColumnCount = 10;
			this.tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5.19867f));
			this.tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.80491f));
			this.tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5.197404f));
			this.tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.8017f));
			this.tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5.197404f));
			this.tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.8017f));
			this.tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5.197404f));
			this.tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.8017f));
			this.tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5.197404f));
			this.tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.8017f));
			this.tableLayoutPanel4.Controls.Add(this.pictureBox1, 0, 0);
			this.tableLayoutPanel4.Controls.Add(this.pictureBox2, 2, 0);
			this.tableLayoutPanel4.Controls.Add(this.pictureBox3, 6, 0);
			this.tableLayoutPanel4.Controls.Add(this.pictureBox4, 8, 0);
			this.tableLayoutPanel4.Controls.Add(this.pictureBox5, 4, 0);
			this.tableLayoutPanel4.Controls.Add(this.progresBar_vitalidad, 1, 0);
			this.tableLayoutPanel4.Controls.Add(this.progresBar_energia, 3, 0);
			this.tableLayoutPanel4.Controls.Add(this.progresBar_experiencia, 5, 0);
			this.tableLayoutPanel4.Controls.Add(this.progresBar_pods, 7, 0);
			this.tableLayoutPanel4.Controls.Add(this.label_kamas_principal, 9, 0);
			this.tableLayoutPanel4.Dock = DockStyle.Bottom;
			this.tableLayoutPanel4.Location = new Point(0, 720);
			this.tableLayoutPanel4.Name = "tableLayoutPanel4";
			this.tableLayoutPanel4.RowCount = 1;
			this.tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel4.Size = new Size(1005, 35);
			this.tableLayoutPanel4.TabIndex = 1;
			this.pictureBox1.Dock = DockStyle.Fill;
			this.pictureBox1.Image = (Image)componentResourceManager.GetObject("pictureBox1.Image");
			this.pictureBox1.Location = new Point(3, 3);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new Size(46, 29);
			this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.pictureBox2.Dock = DockStyle.Fill;
			this.pictureBox2.Image = (Image)componentResourceManager.GetObject("pictureBox2.Image");
			this.pictureBox2.Location = new Point(203, 3);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new Size(46, 29);
			this.pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
			this.pictureBox2.TabIndex = 1;
			this.pictureBox2.TabStop = false;
			this.pictureBox3.Dock = DockStyle.Fill;
			this.pictureBox3.Image = (Image)componentResourceManager.GetObject("pictureBox3.Image");
			this.pictureBox3.Location = new Point(603, 3);
			this.pictureBox3.Name = "pictureBox3";
			this.pictureBox3.Size = new Size(46, 29);
			this.pictureBox3.SizeMode = PictureBoxSizeMode.CenterImage;
			this.pictureBox3.TabIndex = 2;
			this.pictureBox3.TabStop = false;
			this.pictureBox4.Dock = DockStyle.Fill;
			this.pictureBox4.Image = (Image)componentResourceManager.GetObject("pictureBox4.Image");
			this.pictureBox4.Location = new Point(803, 3);
			this.pictureBox4.Name = "pictureBox4";
			this.pictureBox4.Size = new Size(46, 29);
			this.pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
			this.pictureBox4.TabIndex = 3;
			this.pictureBox4.TabStop = false;
			this.pictureBox5.Dock = DockStyle.Fill;
			this.pictureBox5.Image = Resources.experiencia;
			this.pictureBox5.Location = new Point(403, 3);
			this.pictureBox5.Name = "pictureBox5";
			this.pictureBox5.Size = new Size(46, 29);
			this.pictureBox5.SizeMode = PictureBoxSizeMode.CenterImage;
			this.pictureBox5.TabIndex = 4;
			this.pictureBox5.TabStop = false;
			this.progresBar_vitalidad.color_Barra = Color.FromArgb(102, 150, 232);
			this.progresBar_vitalidad.Dock = DockStyle.Fill;
			this.progresBar_vitalidad.ForeColor = Color.Gainsboro;
			this.progresBar_vitalidad.Location = new Point(55, 3);
			this.progresBar_vitalidad.Name = "progresBar_vitalidad";
			this.progresBar_vitalidad.Size = new Size(142, 29);
			this.progresBar_vitalidad.TabIndex = 5;
			this.progresBar_vitalidad.tipos_Barra = TipoProgresBar.VALOR_MAXIMO_PORCENTAJE;
			this.progresBar_vitalidad.Valor = 0;
			this.progresBar_vitalidad.valor_Maximo = 100;
			this.progresBar_energia.color_Barra = Color.FromArgb(102, 150, 232);
			this.progresBar_energia.Dock = DockStyle.Fill;
			this.progresBar_energia.ForeColor = Color.Gainsboro;
			this.progresBar_energia.Location = new Point(255, 3);
			this.progresBar_energia.Name = "progresBar_energia";
			this.progresBar_energia.Size = new Size(142, 29);
			this.progresBar_energia.TabIndex = 6;
			this.progresBar_energia.tipos_Barra = TipoProgresBar.VALOR_MAXIMO;
			this.progresBar_energia.Valor = 0;
			this.progresBar_energia.valor_Maximo = 10000;
			this.progresBar_experiencia.color_Barra = Color.FromArgb(102, 150, 232);
			this.progresBar_experiencia.Dock = DockStyle.Fill;
			this.progresBar_experiencia.ForeColor = Color.Cyan;
			this.progresBar_experiencia.Location = new Point(455, 3);
			this.progresBar_experiencia.Name = "progresBar_experiencia";
			this.progresBar_experiencia.Size = new Size(142, 29);
			this.progresBar_experiencia.TabIndex = 7;
			this.progresBar_experiencia.Text = "0";
			this.progresBar_experiencia.tipos_Barra = TipoProgresBar.TEXTO_PORCENTAJE;
			this.progresBar_experiencia.Valor = 0;
			this.progresBar_experiencia.valor_Maximo = 100;
			this.progresBar_pods.color_Barra = Color.FromArgb(102, 150, 232);
			this.progresBar_pods.Dock = DockStyle.Fill;
			this.progresBar_pods.ForeColor = Color.Gainsboro;
			this.progresBar_pods.Location = new Point(655, 3);
			this.progresBar_pods.Name = "progresBar_pods";
			this.progresBar_pods.Size = new Size(142, 29);
			this.progresBar_pods.TabIndex = 8;
			this.progresBar_pods.tipos_Barra = TipoProgresBar.VALOR_MAXIMO_PORCENTAJE;
			this.progresBar_pods.Valor = 0;
			this.progresBar_pods.valor_Maximo = 100;
			this.label_kamas_principal.AutoSize = true;
			this.label_kamas_principal.BackColor = Color.FromArgb(60, 63, 65);
			this.label_kamas_principal.Dock = DockStyle.Fill;
			this.label_kamas_principal.Font = new Font("Segoe UI", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label_kamas_principal.ForeColor = Color.Gold;
			this.label_kamas_principal.Location = new Point(855, 0);
			this.label_kamas_principal.Name = "label_kamas_principal";
			this.label_kamas_principal.Size = new Size(147, 35);
			this.label_kamas_principal.TabIndex = 9;
			this.label_kamas_principal.TextAlign = ContentAlignment.MiddleRight;
			this.ScriptTituloStripMenuItem.AutoSize = true;
			this.ScriptTituloStripMenuItem.Location = new Point(361, 13);
			this.ScriptTituloStripMenuItem.Name = "ScriptTituloStripMenuItem";
			this.ScriptTituloStripMenuItem.Size = new Size(0, 23);
			this.ScriptTituloStripMenuItem.TabIndex = 2;
			base.AutoScaleDimensions = new SizeF(9f, 21f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.ScriptTituloStripMenuItem);
			base.Controls.Add(this.tabControl_principal);
			base.Controls.Add(this.tableLayoutPanel4);
			base.Controls.Add(this.menuStrip1);
			this.Font = new Font("Segoe UI", 9.75f);
			base.Margin = new Padding(3, 4, 3, 4);
			base.Name = "UI_Principal";
			base.Size = new Size(1005, 755);
			base.Load += this.UI_Principal_Load;
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.tabControl_principal.ResumeLayout(false);
			this.tabPage_consola.ResumeLayout(false);
			this.tableLayout_Canales.ResumeLayout(false);
			this.tableLayout_Canales.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tableLayoutPanel4.ResumeLayout(false);
			this.tableLayoutPanel4.PerformLayout();
			((ISupportInitialize)this.pictureBox1).EndInit();
			((ISupportInitialize)this.pictureBox2).EndInit();
			((ISupportInitialize)this.pictureBox3).EndInit();
			((ISupportInitialize)this.pictureBox4).EndInit();
			((ISupportInitialize)this.pictureBox5).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040002C3 RID: 707
		private Cuenta cuenta;

		// Token: 0x040002C4 RID: 708
		private string nombre_cuenta;

		// Token: 0x040002C5 RID: 709
		private IContainer components = null;

		// Token: 0x040002C6 RID: 710
		private MenuStrip menuStrip1;

		// Token: 0x040002C7 RID: 711
		private ToolStripMenuItem desconectarOconectarToolStripMenuItem;

		// Token: 0x040002C8 RID: 712
		private ToolStripMenuItem eliminarToolStripMenuItem;

		// Token: 0x040002C9 RID: 713
		private TabControl tabControl_principal;

		// Token: 0x040002CA RID: 714
		private TabPage tabPage_consola;

		// Token: 0x040002CB RID: 715
		private TabPage tabPage2;

		// Token: 0x040002CC RID: 716
		private TableLayoutPanel tableLayout_Canales;

		// Token: 0x040002CD RID: 717
		private ColorCheckBox canal_informaciones;

		// Token: 0x040002CE RID: 718
		private ColorCheckBox canal_comercio;

		// Token: 0x040002CF RID: 719
		private ColorCheckBox canal_alineamiento;

		// Token: 0x040002D0 RID: 720
		private ColorCheckBox canal_reclutamiento;

		// Token: 0x040002D1 RID: 721
		private ColorCheckBox canal_gremio;

		// Token: 0x040002D2 RID: 722
		private ColorCheckBox canal_privado;

		// Token: 0x040002D3 RID: 723
		private TableLayoutPanel tableLayoutPanel1;

		// Token: 0x040002D4 RID: 724
		private Button button_limpiar_consola;

		// Token: 0x040002D5 RID: 725
		private RichTextBox textbox_logs;

		// Token: 0x040002D6 RID: 726
		private TableLayoutPanel tableLayoutPanel4;

		// Token: 0x040002D7 RID: 727
		private PictureBox pictureBox1;

		// Token: 0x040002D8 RID: 728
		private PictureBox pictureBox2;

		// Token: 0x040002D9 RID: 729
		private PictureBox pictureBox3;

		// Token: 0x040002DA RID: 730
		private PictureBox pictureBox4;

		// Token: 0x040002DB RID: 731
		private PictureBox pictureBox5;

		// Token: 0x040002DC RID: 732
		private ProgresBar progresBar_vitalidad;

		// Token: 0x040002DD RID: 733
		private ProgresBar progresBar_energia;

		// Token: 0x040002DE RID: 734
		private ProgresBar progresBar_experiencia;

		// Token: 0x040002DF RID: 735
		private ProgresBar progresBar_pods;

		// Token: 0x040002E0 RID: 736
		private UI_Debugger debugger;

		// Token: 0x040002E1 RID: 737
		private ImageList lista_imagenes;

		// Token: 0x040002E2 RID: 738
		private ColorCheckBox canal_incarnam;

		// Token: 0x040002E3 RID: 739
		private ColorCheckBox canal_general;

		// Token: 0x040002E4 RID: 740
		private Label label_kamas_principal;

		// Token: 0x040002E5 RID: 741
		private ToolStripMenuItem cargarScriptToolStripMenuItem;

		// Token: 0x040002E6 RID: 742
		private ToolStripMenuItem iniciarScriptToolStripMenuItem;

		// Token: 0x040002E7 RID: 743
		private ComboBox comboBox_lista_canales;

		// Token: 0x040002E8 RID: 744
		private Label ScriptTituloStripMenuItem;

		// Token: 0x040002E9 RID: 745
		private ToolStripMenuItem NomScript;

		// Token: 0x040002EA RID: 746
		private DarkTextBox textBox_enviar_consola;

		// Token: 0x040002EB RID: 747
		private DarkTextBox textBox_nombre_privado;
	}
}
