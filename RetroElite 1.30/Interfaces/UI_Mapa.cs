using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using RetroElite.Controles.ControlMapa;
using RetroElite.Controles.ControlMapa.Animaciones;
using RetroElite.Controles.ControlMapa.Celdas;
using RetroElite.Otros;
using RetroElite.Otros.Game.Entidades.Manejadores.Movimientos;
using RetroElite.Otros.Mapas;
using RetroElite.Otros.Mapas.Movimiento;

namespace RetroElite.Interfaces
{
	// Token: 0x02000070 RID: 112
	public class UI_Mapa : UserControl
	{
		// Token: 0x06000480 RID: 1152 RVA: 0x00012558 File Offset: 0x00010958
		public UI_Mapa(Cuenta _cuenta)
		{
			this.InitializeComponent();
			this.cuenta = _cuenta;
			this.control_mapa.set_Cuenta(this.cuenta);
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00012590 File Offset: 0x00010990
		private void UI_Mapa_Load(object sender, EventArgs e)
		{
			this.comboBox_calidad_minimapa.SelectedIndex = (int)((byte)this.control_mapa.TipoCalidad);
			this.control_mapa.clic_celda += this.mapa_Control_Celda_Clic;
			this.cuenta.juego.mapa.mapa_actualizado += this.get_Eventos_Mapa_Cambiado;
			this.cuenta.juego.mapa.entidades_actualizadas += delegate()
			{
				this.control_mapa.Invalidate();
			};
			this.cuenta.juego.personaje.movimiento_pathfinding_minimapa += this.get_Dibujar_Pathfinding;
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00012634 File Offset: 0x00010A34
		private void get_Eventos_Mapa_Cambiado()
		{
			Mapa mapa = this.cuenta.juego.mapa;
			byte mapa_anchura = this.control_mapa.mapa_anchura;
			byte mapa_altura = this.control_mapa.mapa_altura;
			byte anchura = mapa.anchura;
			byte altura = mapa.altura;
			bool flag = mapa_anchura != anchura || mapa_altura != altura;
			if (flag)
			{
				this.control_mapa.mapa_anchura = anchura;
				this.control_mapa.mapa_altura = altura;
				this.control_mapa.set_Celda_Numero();
				this.control_mapa.dibujar_Cuadricula();
			}
			base.BeginInvoke(new Action(delegate()
			{
				this.label_mapa_id.Text = "MAP ID: [" + mapa.id.ToString() + "]";
			}));
			this.control_mapa.refrescar_Mapa();
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00012700 File Offset: 0x00010B00
		private void mapa_Control_Celda_Clic(CeldaMapa celda, MouseButtons botones, bool abajo)
		{
			Mapa mapa = this.cuenta.juego.mapa;
			Celda celda2 = this.cuenta.juego.personaje.celda;
			Celda celda3 = mapa.get_Celda_Id(celda.id);
			bool flag = botones == MouseButtons.Left && celda2.id != 0 && celda3.id != 0 && !abajo;
			if (flag)
			{
				ResultadoMovimientos resultadoMovimientos = this.cuenta.juego.manejador.movimientos.get_Mover_A_Celda(celda3, mapa.celdas_ocupadas(), false, 0);
				ResultadoMovimientos resultadoMovimientos2 = resultadoMovimientos;
				if (resultadoMovimientos2 != ResultadoMovimientos.EXITO)
				{
					if (resultadoMovimientos2 != ResultadoMovimientos.MISMA_CELDA)
					{
						this.cuenta.logger.log_Error("MAP", string.Format("Erreur lors du déplacement du personnage sur la cellule.: {0}", celda3.id));
					}
					else
					{
						this.cuenta.logger.log_Error("MAP", "Déjà un joueur sur la cell-ID");
					}
				}
				else
				{
					this.cuenta.logger.log_informacion("MAP", string.Format("Personnage déplacé à la cellule: {0}", celda3.id));
				}
			}
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00012818 File Offset: 0x00010C18
		private void get_Dibujar_Pathfinding(List<Celda> lista_celdas)
		{
			Task.Run(delegate()
			{
				this.control_mapa.agregar_Animacion(this.cuenta.juego.personaje.id, lista_celdas, PathFinderUtil.get_Tiempo_Desplazamiento_Mapa(lista_celdas.First<Celda>(), lista_celdas, false), TipoAnimaciones.PERSONAJE);
			});
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0001284B File Offset: 0x00010C4B
		private void comboBox_calidad_minimapa_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.control_mapa.TipoCalidad = (CalidadMapa)this.comboBox_calidad_minimapa.SelectedIndex;
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00012864 File Offset: 0x00010C64
		private void checkBox_animaciones_CheckedChanged(object sender, EventArgs e)
		{
			this.control_mapa.Mostrar_Animaciones = this.checkBox_animaciones.Checked;
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0001287D File Offset: 0x00010C7D
		private void checkBox_mostrar_celdas_CheckedChanged(object sender, EventArgs e)
		{
			this.control_mapa.Mostrar_Celdas_Id = this.checkBox_mostrar_celdas.Checked;
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00012898 File Offset: 0x00010C98
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x000128D0 File Offset: 0x00010CD0
		private void InitializeComponent()
		{
			this.tableLayoutPanel1 = new TableLayoutPanel();
			this.tableLayoutPanel3 = new TableLayoutPanel();
			this.label_mapa_id = new Label();
			this.checkBox_mostrar_celdas = new CheckBox();
			this.checkBox_animaciones = new CheckBox();
			this.comboBox_calidad_minimapa = new ComboBox();
			this.control_mapa = new ControlMapa();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			base.SuspendLayout();
			this.tableLayoutPanel1.BackColor = Color.FromArgb(60, 63, 65);
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.control_mapa, 0, 0);
			this.tableLayoutPanel1.Dock = DockStyle.Fill;
			this.tableLayoutPanel1.Location = new Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 92f));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 8f));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
			this.tableLayoutPanel1.Size = new Size(811, 514);
			this.tableLayoutPanel1.TabIndex = 1;
			this.tableLayoutPanel3.ColumnCount = 4;
			this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
			this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
			this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
			this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
			this.tableLayoutPanel3.Controls.Add(this.label_mapa_id, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.checkBox_mostrar_celdas, 1, 0);
			this.tableLayoutPanel3.Controls.Add(this.checkBox_animaciones, 2, 0);
			this.tableLayoutPanel3.Controls.Add(this.comboBox_calidad_minimapa, 3, 0);
			this.tableLayoutPanel3.Dock = DockStyle.Fill;
			this.tableLayoutPanel3.Location = new Point(3, 475);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 1;
			this.tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 36f));
			this.tableLayoutPanel3.Size = new Size(805, 36);
			this.tableLayoutPanel3.TabIndex = 0;
			this.label_mapa_id.Dock = DockStyle.Fill;
			this.label_mapa_id.Font = new Font("Segoe UI", 9.75f);
			this.label_mapa_id.ForeColor = Color.Coral;
			this.label_mapa_id.Location = new Point(3, 0);
			this.label_mapa_id.Name = "label_mapa_id";
			this.label_mapa_id.Size = new Size(195, 36);
			this.label_mapa_id.TabIndex = 0;
			this.label_mapa_id.Text = "MAP ID :";
			this.label_mapa_id.TextAlign = ContentAlignment.MiddleLeft;
			this.checkBox_mostrar_celdas.Dock = DockStyle.Fill;
			this.checkBox_mostrar_celdas.Font = new Font("Segoe UI", 9.75f);
			this.checkBox_mostrar_celdas.ForeColor = Color.CornflowerBlue;
			this.checkBox_mostrar_celdas.Location = new Point(204, 3);
			this.checkBox_mostrar_celdas.Name = "checkBox_mostrar_celdas";
			this.checkBox_mostrar_celdas.Size = new Size(195, 30);
			this.checkBox_mostrar_celdas.TabIndex = 1;
			this.checkBox_mostrar_celdas.Text = "Voir cell ID";
			this.checkBox_mostrar_celdas.UseVisualStyleBackColor = true;
			this.checkBox_mostrar_celdas.CheckedChanged += this.checkBox_mostrar_celdas_CheckedChanged;
			this.checkBox_animaciones.Checked = true;
			this.checkBox_animaciones.CheckState = CheckState.Checked;
			this.checkBox_animaciones.Dock = DockStyle.Fill;
			this.checkBox_animaciones.Font = new Font("Segoe UI", 9.75f);
			this.checkBox_animaciones.ForeColor = Color.CornflowerBlue;
			this.checkBox_animaciones.Location = new Point(405, 3);
			this.checkBox_animaciones.Name = "checkBox_animaciones";
			this.checkBox_animaciones.Size = new Size(195, 30);
			this.checkBox_animaciones.TabIndex = 2;
			this.checkBox_animaciones.Text = "Voir les anim";
			this.checkBox_animaciones.UseVisualStyleBackColor = true;
			this.checkBox_animaciones.CheckedChanged += this.checkBox_animaciones_CheckedChanged;
			this.comboBox_calidad_minimapa.BackColor = Color.FromArgb(60, 63, 65);
			this.comboBox_calidad_minimapa.Dock = DockStyle.Fill;
			this.comboBox_calidad_minimapa.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_calidad_minimapa.FlatStyle = FlatStyle.Flat;
			this.comboBox_calidad_minimapa.Font = new Font("Segoe UI", 9.75f);
			this.comboBox_calidad_minimapa.ForeColor = Color.Gainsboro;
			this.comboBox_calidad_minimapa.FormattingEnabled = true;
			this.comboBox_calidad_minimapa.Items.AddRange(new object[]
			{
				"Qualité faible",
				"Qualité moyenne",
				"HD"
			});
			this.comboBox_calidad_minimapa.Location = new Point(606, 3);
			this.comboBox_calidad_minimapa.Name = "comboBox_calidad_minimapa";
			this.comboBox_calidad_minimapa.Size = new Size(196, 25);
			this.comboBox_calidad_minimapa.TabIndex = 3;
			this.comboBox_calidad_minimapa.SelectedIndexChanged += this.comboBox_calidad_minimapa_SelectedIndexChanged;
			this.control_mapa.BackColor = Color.FromArgb(65, 65, 65);
			this.control_mapa.BorderColorOnOver = Color.Empty;
			this.control_mapa.ColorCeldaActiva = Color.Gray;
			this.control_mapa.ColorCeldaInactiva = Color.DarkGray;
			this.control_mapa.Dock = DockStyle.Fill;
			this.control_mapa.Location = new Point(4, 4);
			this.control_mapa.mapa_altura = 17;
			this.control_mapa.mapa_anchura = 15;
			this.control_mapa.Margin = new Padding(4);
			this.control_mapa.Mostrar_Animaciones = true;
			this.control_mapa.Mostrar_Celdas_Id = false;
			this.control_mapa.Name = "control_mapa";
			this.control_mapa.Size = new Size(803, 464);
			this.control_mapa.TabIndex = 1;
			this.control_mapa.TipoCalidad = CalidadMapa.MEDIA;
			this.control_mapa.TraceOnOver = false;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.tableLayoutPanel1);
			base.Name = "UI_Mapa";
			base.Size = new Size(811, 514);
			base.Load += this.UI_Mapa_Load;
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel3.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		// Token: 0x04000232 RID: 562
		private Cuenta cuenta = null;

		// Token: 0x04000233 RID: 563
		private IContainer components = null;

		// Token: 0x04000234 RID: 564
		private TableLayoutPanel tableLayoutPanel1;

		// Token: 0x04000235 RID: 565
		private TableLayoutPanel tableLayoutPanel3;

		// Token: 0x04000236 RID: 566
		private Label label_mapa_id;

		// Token: 0x04000237 RID: 567
		private CheckBox checkBox_mostrar_celdas;

		// Token: 0x04000238 RID: 568
		private CheckBox checkBox_animaciones;

		// Token: 0x04000239 RID: 569
		private ComboBox comboBox_calidad_minimapa;

		// Token: 0x0400023A RID: 570
		private ControlMapa control_mapa;
	}
}
