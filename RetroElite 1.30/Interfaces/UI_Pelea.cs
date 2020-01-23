using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DarkUI.Controls;
using RetroElite.Otros;
using RetroElite.Otros.Game.Entidades.Manejadores.Movimientos;
using RetroElite.Otros.Game.Personaje.Hechizos;
using RetroElite.Otros.Mapas;
using RetroElite.Otros.Mapas.Entidades;
using RetroElite.Otros.Peleas.Configuracion;
using RetroElite.Otros.Peleas.Enums;
using RetroElite.Properties;

namespace RetroElite.Interfaces
{
	// Token: 0x02000072 RID: 114
	public class UI_Pelea : UserControl
	{
		// Token: 0x06000492 RID: 1170 RVA: 0x00013AEC File Offset: 0x00011EEC
		public UI_Pelea(Cuenta _cuenta)
		{
			this.InitializeComponent();
			this.cuenta = _cuenta;
			this.refrescar_Lista_Hechizos();
			this.cuenta.juego.personaje.hechizos_actualizados += this.actualizar_Agregar_Lista_Hechizos;
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00013B40 File Offset: 0x00011F40
		private void UI_Pelea_Load(object sender, EventArgs e)
		{
			this.comboBox_focus_hechizo.SelectedIndex = 0;
			this.checkbox_espectadores.Checked = this.cuenta.pelea_extension.configuracion.desactivar_espectador;
			bool puede_utilizar_dragopavo = this.cuenta.puede_utilizar_dragopavo;
			if (puede_utilizar_dragopavo)
			{
				this.checkBox_utilizar_dragopavo.Checked = this.cuenta.pelea_extension.configuracion.utilizar_dragopavo;
			}
			else
			{
				this.checkBox_utilizar_dragopavo.Enabled = false;
			}
			this.comboBox_lista_tactica.SelectedIndex = (int)((byte)this.cuenta.pelea_extension.configuracion.tactica);
			this.comboBox_lista_posicionamiento.SelectedIndex = (int)((byte)this.cuenta.pelea_extension.configuracion.posicionamiento);
			this.comboBox_modo_lanzamiento.SelectedIndex = 0;
			this.numericUp_regeneracion1.Value = this.cuenta.pelea_extension.configuracion.iniciar_regeneracion;
			this.numericUp_regeneracion2.Value = this.cuenta.pelea_extension.configuracion.detener_regeneracion;
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00013C58 File Offset: 0x00012058
		private void actualizar_Agregar_Lista_Hechizos()
		{
			try
			{
				this.comboBox_lista_hechizos.DisplayMember = "nombre";
				this.comboBox_lista_hechizos.ValueMember = "id";
				this.comboBox_lista_hechizos.DataSource = this.cuenta.juego.personaje.hechizos.Values.ToList<Hechizo>();
				this.comboBox_lista_hechizos.SelectedIndex = 0;
			}
			catch (Exception ex)
			{
				this.cuenta.logger.log_Error("SORTS", "Erreur sort manquant");
			}
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00013CF4 File Offset: 0x000120F4
		private void button_agregar_hechizo_Click(object sender, EventArgs e)
		{
			Hechizo hechizo = this.comboBox_lista_hechizos.SelectedItem as Hechizo;
			this.cuenta.pelea_extension.configuracion.hechizos.Add(new HechizoPelea(hechizo.id, hechizo.nombre, (HechizoFocus)this.comboBox_focus_hechizo.SelectedIndex, (MetodoLanzamiento)this.comboBox_modo_lanzamiento.SelectedIndex, Convert.ToByte(this.numeric_lanzamientos_turno.Value)));
			this.cuenta.pelea_extension.configuracion.guardar();
			this.refrescar_Lista_Hechizos();
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00013D84 File Offset: 0x00012184
		private void refrescar_Lista_Hechizos()
		{
			this.listView_hechizos_pelea.Items.Clear();
			foreach (HechizoPelea hechizoPelea in this.cuenta.pelea_extension.configuracion.hechizos)
			{
				this.listView_hechizos_pelea.Items.Add(hechizoPelea.id.ToString()).SubItems.AddRange(new string[]
				{
					hechizoPelea.nombre,
					hechizoPelea.focus.ToString(),
					hechizoPelea.lanzamientos_x_turno.ToString(),
					hechizoPelea.metodo_lanzamiento.ToString()
				});
			}
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00013E74 File Offset: 0x00012274
		private void button_subir_hechizo_Click(object sender, EventArgs e)
		{
			bool flag = this.listView_hechizos_pelea.FocusedItem == null || this.listView_hechizos_pelea.FocusedItem.Index == 0;
			if (!flag)
			{
				List<HechizoPelea> hechizos = this.cuenta.pelea_extension.configuracion.hechizos;
				HechizoPelea value = hechizos[this.listView_hechizos_pelea.FocusedItem.Index - 1];
				hechizos[this.listView_hechizos_pelea.FocusedItem.Index - 1] = hechizos[this.listView_hechizos_pelea.FocusedItem.Index];
				hechizos[this.listView_hechizos_pelea.FocusedItem.Index] = value;
				this.cuenta.pelea_extension.configuracion.guardar();
				this.refrescar_Lista_Hechizos();
			}
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00013F44 File Offset: 0x00012344
		private void button_bajar_hechizo_Click(object sender, EventArgs e)
		{
			bool flag = this.listView_hechizos_pelea.FocusedItem == null || this.listView_hechizos_pelea.FocusedItem.Index == 0;
			if (!flag)
			{
				List<HechizoPelea> hechizos = this.cuenta.pelea_extension.configuracion.hechizos;
				HechizoPelea value = hechizos[this.listView_hechizos_pelea.FocusedItem.Index + 1];
				hechizos[this.listView_hechizos_pelea.FocusedItem.Index + 1] = hechizos[this.listView_hechizos_pelea.FocusedItem.Index];
				hechizos[this.listView_hechizos_pelea.FocusedItem.Index] = value;
				this.cuenta.pelea_extension.configuracion.guardar();
				this.refrescar_Lista_Hechizos();
			}
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00014014 File Offset: 0x00012414
		private void button1_Click(object sender, EventArgs e)
		{
			Mapa mapa = this.cuenta.juego.mapa;
			List<Monstruos> list = this.cuenta.juego.mapa.lista_monstruos();
			bool flag = list.Count > 0;
			if (flag)
			{
				Celda celda = this.cuenta.juego.personaje.celda;
				Celda celda2 = list[0].celda;
				bool flag2 = celda.id != celda2.id & celda2.id > 0;
				if (flag2)
				{
					this.cuenta.logger.log_Fight("COMBAT", "Monstre rencontré à la cellule " + celda2.id.ToString());
					ResultadoMovimientos resultadoMovimientos = this.cuenta.juego.manejador.movimientos.get_Mover_A_Celda(celda2, new List<Celda>(), false, 0);
					ResultadoMovimientos resultadoMovimientos2 = resultadoMovimientos;
					if (resultadoMovimientos2 != ResultadoMovimientos.EXITO)
					{
						if (resultadoMovimientos2 - ResultadoMovimientos.MISMA_CELDA <= 2)
						{
							this.cuenta.logger.log_Error("COMBAT", "Le monstre n'est pas sur la cellule ...");
						}
					}
					else
					{
						this.cuenta.logger.log_Fight("COMBAT", "Recherche d'un combat ...");
					}
				}
			}
			else
			{
				this.cuenta.logger.log_Error("MONSTRE", "Aucun monstre disponible sur la map");
			}
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00014166 File Offset: 0x00012566
		private void checkbox_espectadores_CheckedChanged(object sender, EventArgs e)
		{
			this.cuenta.pelea_extension.configuracion.desactivar_espectador = this.checkbox_espectadores.Checked;
			this.cuenta.pelea_extension.configuracion.guardar();
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x000141A0 File Offset: 0x000125A0
		private void checkBox_utilizar_dragopavo_CheckedChanged(object sender, EventArgs e)
		{
			this.cuenta.pelea_extension.configuracion.utilizar_dragopavo = this.checkBox_utilizar_dragopavo.Checked;
			this.cuenta.pelea_extension.configuracion.guardar();
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x000141DA File Offset: 0x000125DA
		private void comboBox_lista_tactica_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.cuenta.pelea_extension.configuracion.tactica = (Tactica)this.comboBox_lista_tactica.SelectedIndex;
			this.cuenta.pelea_extension.configuracion.guardar();
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00014214 File Offset: 0x00012614
		private void button_eliminar_hechizo_Click(object sender, EventArgs e)
		{
			bool flag = this.listView_hechizos_pelea.FocusedItem == null;
			if (!flag)
			{
				this.cuenta.pelea_extension.configuracion.hechizos.RemoveAt(this.listView_hechizos_pelea.FocusedItem.Index);
				this.cuenta.pelea_extension.configuracion.guardar();
				this.refrescar_Lista_Hechizos();
			}
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0001427E File Offset: 0x0001267E
		private void comboBox_lista_posicionamiento_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.cuenta.pelea_extension.configuracion.posicionamiento = (PosicionamientoInicioPelea)this.comboBox_lista_posicionamiento.SelectedIndex;
			this.cuenta.pelea_extension.configuracion.guardar();
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x000142B8 File Offset: 0x000126B8
		private void NumericUp_regeneracion1_ValueChanged(object sender, EventArgs e)
		{
			this.cuenta.pelea_extension.configuracion.iniciar_regeneracion = (byte)this.numericUp_regeneracion1.Value;
			this.cuenta.pelea_extension.configuracion.guardar();
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x000142F7 File Offset: 0x000126F7
		private void NumericUp_regeneracion2_ValueChanged(object sender, EventArgs e)
		{
			this.cuenta.pelea_extension.configuracion.detener_regeneracion = (byte)this.numericUp_regeneracion2.Value;
			this.cuenta.pelea_extension.configuracion.guardar();
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00014338 File Offset: 0x00012738
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00014370 File Offset: 0x00012770
		private void InitializeComponent()
		{
			this.components = new Container();
			ListViewItem listViewItem = new ListViewItem("");
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(UI_Pelea));
			this.tabControl1 = new TabControl();
			this.tabPage_general_pelea = new TabPage();
			this.tableLayoutPanel1 = new TableLayoutPanel();
			this.groupBox3 = new GroupBox();
			this.tableLayoutPanel3 = new TableLayoutPanel();
			this.label2 = new Label();
			this.comboBox_lista_tactica = new ComboBox();
			this.tableLayoutPanel5 = new TableLayoutPanel();
			this.checkBox1 = new CheckBox();
			this.checkBox_utilizar_dragopavo = new CheckBox();
			this.checkbox_espectadores = new CheckBox();
			this.groupBox_preparacion = new GroupBox();
			this.tableLayoutPanel2 = new TableLayoutPanel();
			this.comboBox_lista_posicionamiento = new ComboBox();
			this.label1 = new Label();
			this.groupBox2 = new GroupBox();
			this.tableLayoutPanel4 = new TableLayoutPanel();
			this.numericUp_regeneracion2 = new NumericUpDown();
			this.label_mensaje_regeneracion = new Label();
			this.label_mensaje_regeneracion_1 = new Label();
			this.numericUp_regeneracion1 = new NumericUpDown();
			this.panel_informacion_regeneracion = new Panel();
			this.label_informacion_regeneracion = new Label();
			this.pictureBox1 = new PictureBox();
			this.button1 = new DarkButton();
			this.tabPage_hechizos_pelea = new TabPage();
			this.tableLayoutPanel6 = new TableLayoutPanel();
			this.listView_hechizos_pelea = new ListView();
			this.id = new ColumnHeader();
			this.nombre = new ColumnHeader();
			this.focus = new ColumnHeader();
			this.n_lanzamientos = new ColumnHeader();
			this.lanzamiento = new ColumnHeader();
			this.tableLayoutPanel8 = new TableLayoutPanel();
			this.button_subir_hechizo = new DarkButton();
			this.button_informacion_hechizo = new DarkButton();
			this.button_bajar_hechizo = new DarkButton();
			this.button_eliminar_hechizo = new DarkButton();
			this.groupBox_agregar_hechizo = new GroupBox();
			this.tableLayoutPanel9 = new TableLayoutPanel();
			this.comboBox_modo_lanzamiento = new ComboBox();
			this.label3 = new Label();
			this.label6 = new Label();
			this.label5 = new Label();
			this.comboBox_lista_hechizos = new ComboBox();
			this.comboBox_focus_hechizo = new ComboBox();
			this.label7 = new Label();
			this.numeric_lanzamientos_turno = new NumericUpDown();
			this.button_agregar_hechizo = new DarkButton();
			this.lista_imagenes = new ImageList(this.components);
			this.tabControl1.SuspendLayout();
			this.tabPage_general_pelea.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.tableLayoutPanel5.SuspendLayout();
			this.groupBox_preparacion.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.tableLayoutPanel4.SuspendLayout();
			((ISupportInitialize)this.numericUp_regeneracion2).BeginInit();
			((ISupportInitialize)this.numericUp_regeneracion1).BeginInit();
			this.panel_informacion_regeneracion.SuspendLayout();
			((ISupportInitialize)this.pictureBox1).BeginInit();
			this.tabPage_hechizos_pelea.SuspendLayout();
			this.tableLayoutPanel6.SuspendLayout();
			this.tableLayoutPanel8.SuspendLayout();
			this.groupBox_agregar_hechizo.SuspendLayout();
			this.tableLayoutPanel9.SuspendLayout();
			((ISupportInitialize)this.numeric_lanzamientos_turno).BeginInit();
			base.SuspendLayout();
			this.tabControl1.Controls.Add(this.tabPage_general_pelea);
			this.tabControl1.Controls.Add(this.tabPage_hechizos_pelea);
			this.tabControl1.Dock = DockStyle.Fill;
			this.tabControl1.ImageList = this.lista_imagenes;
			this.tabControl1.ItemSize = new Size(67, 26);
			this.tabControl1.Location = new Point(0, 0);
			this.tabControl1.Margin = new Padding(3, 4, 3, 4);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new Size(750, 580);
			this.tabControl1.TabIndex = 0;
			this.tabPage_general_pelea.Controls.Add(this.tableLayoutPanel1);
			this.tabPage_general_pelea.Controls.Add(this.button1);
			this.tabPage_general_pelea.ImageIndex = 0;
			this.tabPage_general_pelea.Location = new Point(4, 30);
			this.tabPage_general_pelea.Margin = new Padding(3, 4, 3, 4);
			this.tabPage_general_pelea.Name = "tabPage_general_pelea";
			this.tabPage_general_pelea.Padding = new Padding(3, 4, 3, 4);
			this.tabPage_general_pelea.Size = new Size(742, 546);
			this.tabPage_general_pelea.TabIndex = 0;
			this.tabPage_general_pelea.Text = "General";
			this.tabPage_general_pelea.UseVisualStyleBackColor = true;
			this.tableLayoutPanel1.BackColor = Color.FromArgb(60, 63, 65);
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel1.Controls.Add(this.groupBox3, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.groupBox_preparacion, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 2);
			this.tableLayoutPanel1.Dock = DockStyle.Fill;
			this.tableLayoutPanel1.Location = new Point(3, 4);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25.6f));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 40.8f));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333f));
			this.tableLayoutPanel1.Size = new Size(736, 500);
			this.tableLayoutPanel1.TabIndex = 0;
			this.groupBox3.BackColor = Color.FromArgb(60, 63, 65);
			this.groupBox3.Controls.Add(this.tableLayoutPanel3);
			this.groupBox3.Controls.Add(this.tableLayoutPanel5);
			this.groupBox3.Dock = DockStyle.Fill;
			this.groupBox3.ForeColor = Color.Gainsboro;
			this.groupBox3.Location = new Point(3, 131);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new Size(730, 198);
			this.groupBox3.TabIndex = 2;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Pendant le combat";
			this.tableLayoutPanel3.ColumnCount = 2;
			this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.87845f));
			this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 88.12154f));
			this.tableLayoutPanel3.Controls.Add(this.label2, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.comboBox_lista_tactica, 1, 0);
			this.tableLayoutPanel3.Dock = DockStyle.Fill;
			this.tableLayoutPanel3.Location = new Point(3, 25);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 1;
			this.tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
			this.tableLayoutPanel3.Size = new Size(724, 123);
			this.tableLayoutPanel3.TabIndex = 1;
			this.label2.AutoSize = true;
			this.label2.Dock = DockStyle.Fill;
			this.label2.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
			this.label2.Location = new Point(3, 0);
			this.label2.Name = "label2";
			this.label2.Size = new Size(79, 123);
			this.label2.TabIndex = 0;
			this.label2.Text = "Tactique";
			this.comboBox_lista_tactica.Dock = DockStyle.Fill;
			this.comboBox_lista_tactica.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_lista_tactica.FormattingEnabled = true;
			this.comboBox_lista_tactica.Items.AddRange(new object[]
			{
				"Agressif",
				"Passif",
				"Fuyard"
			});
			this.comboBox_lista_tactica.Location = new Point(88, 3);
			this.comboBox_lista_tactica.Name = "comboBox_lista_tactica";
			this.comboBox_lista_tactica.Size = new Size(633, 29);
			this.comboBox_lista_tactica.TabIndex = 1;
			this.comboBox_lista_tactica.SelectedIndexChanged += this.comboBox_lista_tactica_SelectedIndexChanged;
			this.tableLayoutPanel5.ColumnCount = 3;
			this.tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
			this.tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
			this.tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
			this.tableLayoutPanel5.Controls.Add(this.checkBox1, 0, 0);
			this.tableLayoutPanel5.Controls.Add(this.checkBox_utilizar_dragopavo, 2, 0);
			this.tableLayoutPanel5.Controls.Add(this.checkbox_espectadores, 1, 0);
			this.tableLayoutPanel5.Dock = DockStyle.Bottom;
			this.tableLayoutPanel5.Location = new Point(3, 148);
			this.tableLayoutPanel5.Name = "tableLayoutPanel5";
			this.tableLayoutPanel5.RowCount = 1;
			this.tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel5.Size = new Size(724, 47);
			this.tableLayoutPanel5.TabIndex = 0;
			this.checkBox1.AutoSize = true;
			this.checkBox1.CheckAlign = ContentAlignment.TopLeft;
			this.checkBox1.Dock = DockStyle.Fill;
			this.checkBox1.Font = new Font("Segoe UI", 12f, FontStyle.Bold);
			this.checkBox1.ImageAlign = ContentAlignment.TopLeft;
			this.checkBox1.Location = new Point(3, 3);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new Size(235, 41);
			this.checkBox1.TabIndex = 2;
			this.checkBox1.Text = "Verouiller le combat";
			this.checkBox1.TextAlign = ContentAlignment.MiddleCenter;
			this.checkBox1.UseVisualStyleBackColor = true;
			this.checkBox_utilizar_dragopavo.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.checkBox_utilizar_dragopavo.AutoSize = true;
			this.checkBox_utilizar_dragopavo.CheckAlign = ContentAlignment.TopLeft;
			this.checkBox_utilizar_dragopavo.Font = new Font("Segoe UI", 12f, FontStyle.Bold);
			this.checkBox_utilizar_dragopavo.ImageAlign = ContentAlignment.TopLeft;
			this.checkBox_utilizar_dragopavo.Location = new Point(485, 3);
			this.checkBox_utilizar_dragopavo.Name = "checkBox_utilizar_dragopavo";
			this.checkBox_utilizar_dragopavo.Size = new Size(236, 41);
			this.checkBox_utilizar_dragopavo.TabIndex = 1;
			this.checkBox_utilizar_dragopavo.Text = "Utiliser la DD";
			this.checkBox_utilizar_dragopavo.TextAlign = ContentAlignment.MiddleCenter;
			this.checkBox_utilizar_dragopavo.UseVisualStyleBackColor = true;
			this.checkBox_utilizar_dragopavo.CheckedChanged += this.checkBox_utilizar_dragopavo_CheckedChanged;
			this.checkbox_espectadores.AutoSize = true;
			this.checkbox_espectadores.CheckAlign = ContentAlignment.TopLeft;
			this.checkbox_espectadores.Dock = DockStyle.Fill;
			this.checkbox_espectadores.Font = new Font("Segoe UI", 12f, FontStyle.Bold);
			this.checkbox_espectadores.ImageAlign = ContentAlignment.TopLeft;
			this.checkbox_espectadores.Location = new Point(244, 3);
			this.checkbox_espectadores.Name = "checkbox_espectadores";
			this.checkbox_espectadores.Size = new Size(235, 41);
			this.checkbox_espectadores.TabIndex = 0;
			this.checkbox_espectadores.Text = "Enlever spectateur";
			this.checkbox_espectadores.TextAlign = ContentAlignment.MiddleCenter;
			this.checkbox_espectadores.UseVisualStyleBackColor = true;
			this.checkbox_espectadores.CheckedChanged += this.checkbox_espectadores_CheckedChanged;
			this.groupBox_preparacion.BackColor = Color.FromArgb(60, 63, 65);
			this.groupBox_preparacion.Controls.Add(this.tableLayoutPanel2);
			this.groupBox_preparacion.Dock = DockStyle.Fill;
			this.groupBox_preparacion.ForeColor = Color.Gainsboro;
			this.groupBox_preparacion.Location = new Point(3, 3);
			this.groupBox_preparacion.Name = "groupBox_preparacion";
			this.groupBox_preparacion.Size = new Size(730, 122);
			this.groupBox_preparacion.TabIndex = 0;
			this.groupBox_preparacion.TabStop = false;
			this.groupBox_preparacion.Text = "Preparaton";
			this.tableLayoutPanel2.ColumnCount = 2;
			this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 46.8232f));
			this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 53.1768f));
			this.tableLayoutPanel2.Controls.Add(this.comboBox_lista_posicionamiento, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel2.Dock = DockStyle.Fill;
			this.tableLayoutPanel2.Location = new Point(3, 25);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
			this.tableLayoutPanel2.Size = new Size(724, 94);
			this.tableLayoutPanel2.TabIndex = 0;
			this.comboBox_lista_posicionamiento.Dock = DockStyle.Fill;
			this.comboBox_lista_posicionamiento.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_lista_posicionamiento.FormattingEnabled = true;
			this.comboBox_lista_posicionamiento.Items.AddRange(new object[]
			{
				"Loin des ennemis",
				"Proche des ennemis",
				"Aucun mouvement"
			});
			this.comboBox_lista_posicionamiento.Location = new Point(341, 3);
			this.comboBox_lista_posicionamiento.Name = "comboBox_lista_posicionamiento";
			this.comboBox_lista_posicionamiento.Size = new Size(380, 29);
			this.comboBox_lista_posicionamiento.TabIndex = 2;
			this.comboBox_lista_posicionamiento.SelectedIndexChanged += this.comboBox_lista_posicionamiento_SelectedIndexChanged;
			this.label1.AutoSize = true;
			this.label1.Dock = DockStyle.Fill;
			this.label1.Font = new Font("Segoe UI", 12f, FontStyle.Bold);
			this.label1.Location = new Point(3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new Size(332, 94);
			this.label1.TabIndex = 1;
			this.label1.Text = "Positionnement au début du combat";
			this.groupBox2.BackColor = Color.FromArgb(60, 63, 65);
			this.groupBox2.Controls.Add(this.tableLayoutPanel4);
			this.groupBox2.Controls.Add(this.panel_informacion_regeneracion);
			this.groupBox2.Dock = DockStyle.Fill;
			this.groupBox2.ForeColor = Color.Gainsboro;
			this.groupBox2.Location = new Point(3, 335);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new Size(730, 162);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Regeneration";
			this.tableLayoutPanel4.ColumnCount = 4;
			this.tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34.11602f));
			this.tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 29.55801f));
			this.tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 8.011049f));
			this.tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28.1768f));
			this.tableLayoutPanel4.Controls.Add(this.numericUp_regeneracion2, 3, 0);
			this.tableLayoutPanel4.Controls.Add(this.label_mensaje_regeneracion, 0, 0);
			this.tableLayoutPanel4.Controls.Add(this.label_mensaje_regeneracion_1, 2, 0);
			this.tableLayoutPanel4.Controls.Add(this.numericUp_regeneracion1, 1, 0);
			this.tableLayoutPanel4.Dock = DockStyle.Fill;
			this.tableLayoutPanel4.Location = new Point(3, 25);
			this.tableLayoutPanel4.Name = "tableLayoutPanel4";
			this.tableLayoutPanel4.RowCount = 1;
			this.tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel4.Size = new Size(724, 59);
			this.tableLayoutPanel4.TabIndex = 1;
			this.numericUp_regeneracion2.Dock = DockStyle.Fill;
			this.numericUp_regeneracion2.Location = new Point(522, 3);
			this.numericUp_regeneracion2.Name = "numericUp_regeneracion2";
			this.numericUp_regeneracion2.Size = new Size(199, 29);
			this.numericUp_regeneracion2.TabIndex = 3;
			NumericUpDown numericUpDown = this.numericUp_regeneracion2;
			int[] array = new int[4];
			array[0] = 100;
			numericUpDown.Value = new decimal(array);
			this.numericUp_regeneracion2.ValueChanged += this.NumericUp_regeneracion2_ValueChanged;
			this.label_mensaje_regeneracion.Dock = DockStyle.Fill;
			this.label_mensaje_regeneracion.Location = new Point(3, 0);
			this.label_mensaje_regeneracion.Name = "label_mensaje_regeneracion";
			this.label_mensaje_regeneracion.Size = new Size(241, 59);
			this.label_mensaje_regeneracion.TabIndex = 0;
			this.label_mensaje_regeneracion.Text = "Regénérer si la vie est >= à";
			this.label_mensaje_regeneracion_1.Dock = DockStyle.Fill;
			this.label_mensaje_regeneracion_1.Location = new Point(464, 0);
			this.label_mensaje_regeneracion_1.Name = "label_mensaje_regeneracion_1";
			this.label_mensaje_regeneracion_1.Size = new Size(52, 59);
			this.label_mensaje_regeneracion_1.TabIndex = 2;
			this.label_mensaje_regeneracion_1.Text = "à";
			this.numericUp_regeneracion1.AutoSize = true;
			this.numericUp_regeneracion1.Dock = DockStyle.Fill;
			this.numericUp_regeneracion1.Location = new Point(250, 3);
			NumericUpDown numericUpDown2 = this.numericUp_regeneracion1;
			int[] array2 = new int[4];
			array2[0] = 99;
			numericUpDown2.Maximum = new decimal(array2);
			this.numericUp_regeneracion1.Name = "numericUp_regeneracion1";
			this.numericUp_regeneracion1.Size = new Size(208, 29);
			this.numericUp_regeneracion1.TabIndex = 1;
			NumericUpDown numericUpDown3 = this.numericUp_regeneracion1;
			int[] array3 = new int[4];
			array3[0] = 50;
			numericUpDown3.Value = new decimal(array3);
			this.numericUp_regeneracion1.ValueChanged += this.NumericUp_regeneracion1_ValueChanged;
			this.panel_informacion_regeneracion.Controls.Add(this.label_informacion_regeneracion);
			this.panel_informacion_regeneracion.Controls.Add(this.pictureBox1);
			this.panel_informacion_regeneracion.Dock = DockStyle.Bottom;
			this.panel_informacion_regeneracion.Location = new Point(3, 84);
			this.panel_informacion_regeneracion.Name = "panel_informacion_regeneracion";
			this.panel_informacion_regeneracion.Size = new Size(724, 75);
			this.panel_informacion_regeneracion.TabIndex = 0;
			this.label_informacion_regeneracion.Dock = DockStyle.Fill;
			this.label_informacion_regeneracion.Location = new Point(25, 0);
			this.label_informacion_regeneracion.Name = "label_informacion_regeneracion";
			this.label_informacion_regeneracion.Size = new Size(699, 75);
			this.label_informacion_regeneracion.TabIndex = 9;
			this.label_informacion_regeneracion.Text = "Définissez la premiere valeur sur 0 pour désactiver la regénération";
			this.label_informacion_regeneracion.TextAlign = ContentAlignment.MiddleCenter;
			this.pictureBox1.Dock = DockStyle.Left;
			this.pictureBox1.Image = Resources.informacion;
			this.pictureBox1.Location = new Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new Size(25, 75);
			this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 8;
			this.pictureBox1.TabStop = false;
			this.button1.Dock = DockStyle.Bottom;
			this.button1.Location = new Point(3, 504);
			this.button1.Name = "button1";
			this.button1.Padding = new Padding(5);
			this.button1.Size = new Size(736, 38);
			this.button1.TabIndex = 1;
			this.button1.Text = "Commencer à combattre";
			this.button1.Click += this.button1_Click;
			this.tabPage_hechizos_pelea.BackColor = Color.FromArgb(60, 63, 65);
			this.tabPage_hechizos_pelea.Controls.Add(this.tableLayoutPanel6);
			this.tabPage_hechizos_pelea.Controls.Add(this.groupBox_agregar_hechizo);
			this.tabPage_hechizos_pelea.Controls.Add(this.button_agregar_hechizo);
			this.tabPage_hechizos_pelea.ImageIndex = 1;
			this.tabPage_hechizos_pelea.Location = new Point(4, 30);
			this.tabPage_hechizos_pelea.Margin = new Padding(3, 4, 3, 4);
			this.tabPage_hechizos_pelea.Name = "tabPage_hechizos_pelea";
			this.tabPage_hechizos_pelea.Padding = new Padding(3, 4, 3, 4);
			this.tabPage_hechizos_pelea.Size = new Size(742, 546);
			this.tabPage_hechizos_pelea.TabIndex = 1;
			this.tabPage_hechizos_pelea.Text = "Sorts";
			this.tableLayoutPanel6.ColumnCount = 2;
			this.tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 92.63302f));
			this.tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 7.366985f));
			this.tableLayoutPanel6.Controls.Add(this.listView_hechizos_pelea, 0, 0);
			this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel8, 1, 0);
			this.tableLayoutPanel6.Dock = DockStyle.Fill;
			this.tableLayoutPanel6.Location = new Point(3, 4);
			this.tableLayoutPanel6.Name = "tableLayoutPanel6";
			this.tableLayoutPanel6.RowCount = 1;
			this.tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
			this.tableLayoutPanel6.Size = new Size(736, 355);
			this.tableLayoutPanel6.TabIndex = 4;
			this.listView_hechizos_pelea.BackColor = Color.FromArgb(60, 63, 65);
			this.listView_hechizos_pelea.Columns.AddRange(new ColumnHeader[]
			{
				this.id,
				this.nombre,
				this.focus,
				this.n_lanzamientos,
				this.lanzamiento
			});
			this.listView_hechizos_pelea.Dock = DockStyle.Fill;
			this.listView_hechizos_pelea.ForeColor = Color.Gainsboro;
			this.listView_hechizos_pelea.FullRowSelect = true;
			this.listView_hechizos_pelea.HideSelection = false;
			this.listView_hechizos_pelea.Items.AddRange(new ListViewItem[]
			{
				listViewItem
			});
			this.listView_hechizos_pelea.Location = new Point(3, 3);
			this.listView_hechizos_pelea.Name = "listView_hechizos_pelea";
			this.listView_hechizos_pelea.Size = new Size(675, 349);
			this.listView_hechizos_pelea.TabIndex = 0;
			this.listView_hechizos_pelea.UseCompatibleStateImageBehavior = false;
			this.listView_hechizos_pelea.View = View.Details;
			this.id.Text = "ID";
			this.id.Width = 58;
			this.nombre.Text = "Nombre";
			this.nombre.Width = 108;
			this.focus.Text = "Focus";
			this.focus.Width = 97;
			this.n_lanzamientos.Text = "Lancement par tour";
			this.n_lanzamientos.Width = 184;
			this.lanzamiento.Text = "Lancer";
			this.lanzamiento.Width = 136;
			this.tableLayoutPanel8.ColumnCount = 1;
			this.tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel8.Controls.Add(this.button_subir_hechizo, 0, 0);
			this.tableLayoutPanel8.Controls.Add(this.button_informacion_hechizo, 0, 3);
			this.tableLayoutPanel8.Controls.Add(this.button_bajar_hechizo, 0, 1);
			this.tableLayoutPanel8.Controls.Add(this.button_eliminar_hechizo, 0, 2);
			this.tableLayoutPanel8.Dock = DockStyle.Fill;
			this.tableLayoutPanel8.Location = new Point(684, 3);
			this.tableLayoutPanel8.Name = "tableLayoutPanel8";
			this.tableLayoutPanel8.RowCount = 4;
			this.tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 25f));
			this.tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 25f));
			this.tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 25f));
			this.tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 25f));
			this.tableLayoutPanel8.Size = new Size(49, 349);
			this.tableLayoutPanel8.TabIndex = 1;
			this.button_subir_hechizo.Dock = DockStyle.Fill;
			this.button_subir_hechizo.Image = Resources.flecha_arriba;
			this.button_subir_hechizo.Location = new Point(3, 3);
			this.button_subir_hechizo.Name = "button_subir_hechizo";
			this.button_subir_hechizo.Padding = new Padding(5);
			this.button_subir_hechizo.Size = new Size(43, 81);
			this.button_subir_hechizo.TabIndex = 0;
			this.button_subir_hechizo.Click += this.button_subir_hechizo_Click;
			this.button_informacion_hechizo.Dock = DockStyle.Fill;
			this.button_informacion_hechizo.Image = Resources.informacion;
			this.button_informacion_hechizo.Location = new Point(3, 264);
			this.button_informacion_hechizo.Name = "button_informacion_hechizo";
			this.button_informacion_hechizo.Padding = new Padding(5);
			this.button_informacion_hechizo.Size = new Size(43, 82);
			this.button_informacion_hechizo.TabIndex = 3;
			this.button_bajar_hechizo.Dock = DockStyle.Fill;
			this.button_bajar_hechizo.Image = Resources.flecha_abajo;
			this.button_bajar_hechizo.Location = new Point(3, 90);
			this.button_bajar_hechizo.Name = "button_bajar_hechizo";
			this.button_bajar_hechizo.Padding = new Padding(5);
			this.button_bajar_hechizo.Size = new Size(43, 81);
			this.button_bajar_hechizo.TabIndex = 1;
			this.button_bajar_hechizo.Click += this.button_bajar_hechizo_Click;
			this.button_eliminar_hechizo.Dock = DockStyle.Fill;
			this.button_eliminar_hechizo.Image = Resources.cruz_roja_pequeña;
			this.button_eliminar_hechizo.Location = new Point(3, 177);
			this.button_eliminar_hechizo.Name = "button_eliminar_hechizo";
			this.button_eliminar_hechizo.Padding = new Padding(5);
			this.button_eliminar_hechizo.Size = new Size(43, 81);
			this.button_eliminar_hechizo.TabIndex = 2;
			this.button_eliminar_hechizo.Click += this.button_eliminar_hechizo_Click;
			this.groupBox_agregar_hechizo.BackColor = Color.FromArgb(60, 63, 65);
			this.groupBox_agregar_hechizo.Controls.Add(this.tableLayoutPanel9);
			this.groupBox_agregar_hechizo.Dock = DockStyle.Bottom;
			this.groupBox_agregar_hechizo.ForeColor = Color.Gainsboro;
			this.groupBox_agregar_hechizo.Location = new Point(3, 359);
			this.groupBox_agregar_hechizo.Name = "groupBox_agregar_hechizo";
			this.groupBox_agregar_hechizo.Size = new Size(736, 152);
			this.groupBox_agregar_hechizo.TabIndex = 1;
			this.groupBox_agregar_hechizo.TabStop = false;
			this.groupBox_agregar_hechizo.Text = "Gestion sorts";
			this.tableLayoutPanel9.ColumnCount = 2;
			this.tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 44.25727f));
			this.tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55.74273f));
			this.tableLayoutPanel9.Controls.Add(this.comboBox_modo_lanzamiento, 1, 3);
			this.tableLayoutPanel9.Controls.Add(this.label3, 0, 3);
			this.tableLayoutPanel9.Controls.Add(this.label6, 0, 1);
			this.tableLayoutPanel9.Controls.Add(this.label5, 0, 0);
			this.tableLayoutPanel9.Controls.Add(this.comboBox_lista_hechizos, 1, 0);
			this.tableLayoutPanel9.Controls.Add(this.comboBox_focus_hechizo, 1, 1);
			this.tableLayoutPanel9.Controls.Add(this.label7, 0, 2);
			this.tableLayoutPanel9.Controls.Add(this.numeric_lanzamientos_turno, 1, 2);
			this.tableLayoutPanel9.Dock = DockStyle.Fill;
			this.tableLayoutPanel9.Location = new Point(3, 25);
			this.tableLayoutPanel9.Name = "tableLayoutPanel9";
			this.tableLayoutPanel9.RowCount = 4;
			this.tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Percent, 25f));
			this.tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Percent, 25f));
			this.tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Percent, 25f));
			this.tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Percent, 25f));
			this.tableLayoutPanel9.Size = new Size(730, 124);
			this.tableLayoutPanel9.TabIndex = 2;
			this.comboBox_modo_lanzamiento.BackColor = Color.FromArgb(60, 63, 65);
			this.comboBox_modo_lanzamiento.Dock = DockStyle.Fill;
			this.comboBox_modo_lanzamiento.FlatStyle = FlatStyle.Flat;
			this.comboBox_modo_lanzamiento.ForeColor = Color.Gainsboro;
			this.comboBox_modo_lanzamiento.FormattingEnabled = true;
			this.comboBox_modo_lanzamiento.Items.AddRange(new object[]
			{
				"CAC",
				"DISTANCE",
				"LES DEUX"
			});
			this.comboBox_modo_lanzamiento.Location = new Point(326, 96);
			this.comboBox_modo_lanzamiento.Name = "comboBox_modo_lanzamiento";
			this.comboBox_modo_lanzamiento.Size = new Size(401, 29);
			this.comboBox_modo_lanzamiento.TabIndex = 8;
			this.label3.AutoSize = true;
			this.label3.Dock = DockStyle.Fill;
			this.label3.Font = new Font("Segoe UI", 12f, FontStyle.Bold);
			this.label3.ImageAlign = ContentAlignment.MiddleLeft;
			this.label3.Location = new Point(3, 93);
			this.label3.Name = "label3";
			this.label3.Size = new Size(317, 31);
			this.label3.TabIndex = 7;
			this.label3.Text = "Tactique utilisation";
			this.label6.AutoSize = true;
			this.label6.Dock = DockStyle.Fill;
			this.label6.Font = new Font("Segoe UI", 12f, FontStyle.Bold);
			this.label6.Location = new Point(3, 31);
			this.label6.Name = "label6";
			this.label6.Size = new Size(317, 31);
			this.label6.TabIndex = 3;
			this.label6.Text = "Focus";
			this.label5.AutoSize = true;
			this.label5.Dock = DockStyle.Fill;
			this.label5.Font = new Font("Segoe UI", 12f, FontStyle.Bold);
			this.label5.Location = new Point(3, 0);
			this.label5.Name = "label5";
			this.label5.Size = new Size(317, 31);
			this.label5.TabIndex = 0;
			this.label5.Text = "Sort ";
			this.comboBox_lista_hechizos.BackColor = Color.FromArgb(60, 63, 65);
			this.comboBox_lista_hechizos.Dock = DockStyle.Fill;
			this.comboBox_lista_hechizos.FlatStyle = FlatStyle.Flat;
			this.comboBox_lista_hechizos.ForeColor = Color.Gainsboro;
			this.comboBox_lista_hechizos.FormattingEnabled = true;
			this.comboBox_lista_hechizos.Location = new Point(326, 3);
			this.comboBox_lista_hechizos.Name = "comboBox_lista_hechizos";
			this.comboBox_lista_hechizos.Size = new Size(401, 29);
			this.comboBox_lista_hechizos.TabIndex = 1;
			this.comboBox_focus_hechizo.BackColor = Color.FromArgb(60, 63, 65);
			this.comboBox_focus_hechizo.Dock = DockStyle.Fill;
			this.comboBox_focus_hechizo.FlatStyle = FlatStyle.Flat;
			this.comboBox_focus_hechizo.ForeColor = Color.Gainsboro;
			this.comboBox_focus_hechizo.FormattingEnabled = true;
			this.comboBox_focus_hechizo.Items.AddRange(new object[]
			{
				"Ennemie",
				"Alliée",
				"Sois-même",
				"Cellule vide"
			});
			this.comboBox_focus_hechizo.Location = new Point(326, 34);
			this.comboBox_focus_hechizo.Name = "comboBox_focus_hechizo";
			this.comboBox_focus_hechizo.Size = new Size(401, 29);
			this.comboBox_focus_hechizo.TabIndex = 4;
			this.label7.AutoSize = true;
			this.label7.Dock = DockStyle.Fill;
			this.label7.Font = new Font("Segoe UI", 12f, FontStyle.Bold);
			this.label7.ImageAlign = ContentAlignment.MiddleLeft;
			this.label7.Location = new Point(3, 62);
			this.label7.Name = "label7";
			this.label7.Size = new Size(317, 31);
			this.label7.TabIndex = 5;
			this.label7.Text = "Utilisation par tour";
			this.numeric_lanzamientos_turno.BackColor = Color.FromArgb(60, 63, 65);
			this.numeric_lanzamientos_turno.BorderStyle = BorderStyle.FixedSingle;
			this.numeric_lanzamientos_turno.Dock = DockStyle.Fill;
			this.numeric_lanzamientos_turno.ForeColor = Color.Gainsboro;
			this.numeric_lanzamientos_turno.Location = new Point(326, 65);
			NumericUpDown numericUpDown4 = this.numeric_lanzamientos_turno;
			int[] array4 = new int[4];
			array4[0] = 10;
			numericUpDown4.Maximum = new decimal(array4);
			this.numeric_lanzamientos_turno.Name = "numeric_lanzamientos_turno";
			this.numeric_lanzamientos_turno.Size = new Size(401, 29);
			this.numeric_lanzamientos_turno.TabIndex = 6;
			NumericUpDown numericUpDown5 = this.numeric_lanzamientos_turno;
			int[] array5 = new int[4];
			array5[0] = 1;
			numericUpDown5.Value = new decimal(array5);
			this.button_agregar_hechizo.Dock = DockStyle.Bottom;
			this.button_agregar_hechizo.Location = new Point(3, 511);
			this.button_agregar_hechizo.Name = "button_agregar_hechizo";
			this.button_agregar_hechizo.Padding = new Padding(5);
			this.button_agregar_hechizo.Size = new Size(736, 31);
			this.button_agregar_hechizo.TabIndex = 1;
			this.button_agregar_hechizo.Text = "Ajouter un sort";
			this.button_agregar_hechizo.Click += this.button_agregar_hechizo_Click;
			this.lista_imagenes.ImageStream = (ImageListStreamer)componentResourceManager.GetObject("lista_imagenes.ImageStream");
			this.lista_imagenes.TransparentColor = Color.Transparent;
			this.lista_imagenes.Images.SetKeyName(0, "1 - Home24.png");
			this.lista_imagenes.Images.SetKeyName(1, "magic32.png");
			base.AutoScaleDimensions = new SizeF(9f, 21f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.tabControl1);
			this.Font = new Font("Segoe UI", 9.75f);
			base.Margin = new Padding(3, 4, 3, 4);
			base.Name = "UI_Pelea";
			base.Size = new Size(750, 580);
			base.Load += this.UI_Pelea_Load;
			this.tabControl1.ResumeLayout(false);
			this.tabPage_general_pelea.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			this.tableLayoutPanel5.ResumeLayout(false);
			this.tableLayoutPanel5.PerformLayout();
			this.groupBox_preparacion.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.tableLayoutPanel4.ResumeLayout(false);
			this.tableLayoutPanel4.PerformLayout();
			((ISupportInitialize)this.numericUp_regeneracion2).EndInit();
			((ISupportInitialize)this.numericUp_regeneracion1).EndInit();
			this.panel_informacion_regeneracion.ResumeLayout(false);
			((ISupportInitialize)this.pictureBox1).EndInit();
			this.tabPage_hechizos_pelea.ResumeLayout(false);
			this.tableLayoutPanel6.ResumeLayout(false);
			this.tableLayoutPanel8.ResumeLayout(false);
			this.groupBox_agregar_hechizo.ResumeLayout(false);
			this.tableLayoutPanel9.ResumeLayout(false);
			this.tableLayoutPanel9.PerformLayout();
			((ISupportInitialize)this.numeric_lanzamientos_turno).EndInit();
			base.ResumeLayout(false);
		}

		// Token: 0x04000249 RID: 585
		private Cuenta cuenta;

		// Token: 0x0400024A RID: 586
		private IContainer components = null;

		// Token: 0x0400024B RID: 587
		private TabControl tabControl1;

		// Token: 0x0400024C RID: 588
		private TabPage tabPage_general_pelea;

		// Token: 0x0400024D RID: 589
		private TabPage tabPage_hechizos_pelea;

		// Token: 0x0400024E RID: 590
		private ImageList lista_imagenes;

		// Token: 0x0400024F RID: 591
		private TableLayoutPanel tableLayoutPanel1;

		// Token: 0x04000250 RID: 592
		private GroupBox groupBox_preparacion;

		// Token: 0x04000251 RID: 593
		private Label label1;

		// Token: 0x04000252 RID: 594
		private ComboBox comboBox_lista_posicionamiento;

		// Token: 0x04000253 RID: 595
		private Label label2;

		// Token: 0x04000254 RID: 596
		private ComboBox comboBox_lista_tactica;

		// Token: 0x04000255 RID: 597
		private ListView listView_hechizos_pelea;

		// Token: 0x04000256 RID: 598
		private GroupBox groupBox_agregar_hechizo;

		// Token: 0x04000257 RID: 599
		private TableLayoutPanel tableLayoutPanel9;

		// Token: 0x04000258 RID: 600
		private Label label5;

		// Token: 0x04000259 RID: 601
		private ComboBox comboBox_lista_hechizos;

		// Token: 0x0400025A RID: 602
		private Label label6;

		// Token: 0x0400025B RID: 603
		private ComboBox comboBox_focus_hechizo;

		// Token: 0x0400025C RID: 604
		private ColumnHeader id;

		// Token: 0x0400025D RID: 605
		private ColumnHeader nombre;

		// Token: 0x0400025E RID: 606
		private ColumnHeader focus;

		// Token: 0x0400025F RID: 607
		private Label label7;

		// Token: 0x04000260 RID: 608
		private NumericUpDown numeric_lanzamientos_turno;

		// Token: 0x04000261 RID: 609
		private ColumnHeader n_lanzamientos;

		// Token: 0x04000262 RID: 610
		private CheckBox checkbox_espectadores;

		// Token: 0x04000263 RID: 611
		private CheckBox checkBox_utilizar_dragopavo;

		// Token: 0x04000264 RID: 612
		private ColumnHeader lanzamiento;

		// Token: 0x04000265 RID: 613
		private TableLayoutPanel tableLayoutPanel8;

		// Token: 0x04000266 RID: 614
		private GroupBox groupBox2;

		// Token: 0x04000267 RID: 615
		private Label label_mensaje_regeneracion;

		// Token: 0x04000268 RID: 616
		private PictureBox pictureBox1;

		// Token: 0x04000269 RID: 617
		private NumericUpDown numericUp_regeneracion1;

		// Token: 0x0400026A RID: 618
		private Label label_mensaje_regeneracion_1;

		// Token: 0x0400026B RID: 619
		private NumericUpDown numericUp_regeneracion2;

		// Token: 0x0400026C RID: 620
		private Label label_informacion_regeneracion;

		// Token: 0x0400026D RID: 621
		private ComboBox comboBox_modo_lanzamiento;

		// Token: 0x0400026E RID: 622
		private Label label3;

		// Token: 0x0400026F RID: 623
		private GroupBox groupBox3;

		// Token: 0x04000270 RID: 624
		private TableLayoutPanel tableLayoutPanel5;

		// Token: 0x04000271 RID: 625
		private TableLayoutPanel tableLayoutPanel2;

		// Token: 0x04000272 RID: 626
		private TableLayoutPanel tableLayoutPanel3;

		// Token: 0x04000273 RID: 627
		private Panel panel_informacion_regeneracion;

		// Token: 0x04000274 RID: 628
		private TableLayoutPanel tableLayoutPanel4;

		// Token: 0x04000275 RID: 629
		private CheckBox checkBox1;

		// Token: 0x04000276 RID: 630
		private TableLayoutPanel tableLayoutPanel6;

		// Token: 0x04000277 RID: 631
		private DarkButton button1;

		// Token: 0x04000278 RID: 632
		private DarkButton button_agregar_hechizo;

		// Token: 0x04000279 RID: 633
		private DarkButton button_subir_hechizo;

		// Token: 0x0400027A RID: 634
		private DarkButton button_bajar_hechizo;

		// Token: 0x0400027B RID: 635
		private DarkButton button_eliminar_hechizo;

		// Token: 0x0400027C RID: 636
		private DarkButton button_informacion_hechizo;
	}
}
