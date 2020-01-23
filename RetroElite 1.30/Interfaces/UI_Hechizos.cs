using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using RetroElite.Otros;
using RetroElite.Otros.Game.Personaje.Hechizos;

namespace RetroElite.Interfaces
{
	// Token: 0x0200006E RID: 110
	public class UI_Hechizos : UserControl
	{
		// Token: 0x0600046E RID: 1134 RVA: 0x0001042A File Offset: 0x0000E82A
		public UI_Hechizos()
		{
			this.InitializeComponent();
			UI_Hechizos.set_DoubleBuffered(this.dataGridView_hechizos);
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0001044E File Offset: 0x0000E84E
		public void set_Cuenta(Cuenta _cuenta)
		{
			this.cuenta = _cuenta;
			this.cuenta.juego.personaje.hechizos_actualizados += this.actualizar_Agregar_Lista_Hechizos;
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0001047C File Offset: 0x0000E87C
		private void dataGridView_hechizos_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			bool flag = this.dataGridView_hechizos.CurrentCell.ColumnIndex.Equals(3) && e.RowIndex != -1;
			if (flag)
			{
				bool flag2 = this.dataGridView_hechizos.CurrentCell != null && this.dataGridView_hechizos.CurrentCell.Value != null;
				if (flag2)
				{
					DialogResult dialogResult = MessageBox.Show("Ve?ux-tu augmenter ce sort ?", "RetroElite", MessageBoxButtons.YesNo);
					bool flag3 = dialogResult == DialogResult.Yes;
					if (flag3)
					{
						string text = this.dataGridView_hechizos.CurrentRow.Cells["id"].Value.ToString();
						this.cuenta.conexion.enviar_Paquete("SB" + text, false);
						this.cuenta.conexion.enviar_Paquete("BD", false);
						this.actualizar_Agregar_Lista_Hechizos(text);
					}
				}
			}
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0001056A File Offset: 0x0000E96A
		private void actualizar_Agregar_Lista_Hechizos()
		{
			this.dataGridView_hechizos.BeginInvoke(new Action(delegate()
			{
				this.dataGridView_hechizos.Rows.Clear();
				foreach (Hechizo hechizo in this.cuenta.juego.personaje.hechizos.Values)
				{
					this.dataGridView_hechizos.Rows.Add(new object[]
					{
						hechizo.id,
						hechizo.nombre,
						hechizo.nivel,
						(hechizo.nivel == 7 || hechizo.id == 0) ? "-" : "Monter le sort"
					});
				}
			}));
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00010588 File Offset: 0x0000E988
		private void actualizar_Agregar_Lista_Hechizos(string idS = null)
		{
			this.dataGridView_hechizos.BeginInvoke(new Action(delegate()
			{
				this.dataGridView_hechizos.Rows.Clear();
				foreach (Hechizo hechizo in this.cuenta.juego.personaje.hechizos.Values)
				{
					bool flag = idS != null && int.Parse(idS).Equals((int)hechizo.id);
					if (flag)
					{
						int num = (int)(hechizo.nivel + 1);
						this.dataGridView_hechizos.Rows.Add(new object[]
						{
							hechizo.id,
							hechizo.nombre,
							num,
							(hechizo.nivel == 7 || hechizo.id == 0) ? "-" : "Monter le sort"
						});
					}
					else
					{
						this.dataGridView_hechizos.Rows.Add(new object[]
						{
							hechizo.id,
							hechizo.nombre,
							hechizo.nivel,
							(hechizo.nivel == 7 || hechizo.id == 0) ? "-" : "Monter le sort"
						});
					}
				}
			}));
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x000105C4 File Offset: 0x0000E9C4
		public static void set_DoubleBuffered(Control control)
		{
			typeof(Control).InvokeMember("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.SetProperty, null, control, new object[]
			{
				true
			});
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x000105FC File Offset: 0x0000E9FC
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00010634 File Offset: 0x0000EA34
		private void InitializeComponent()
		{
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
			this.dataGridView_hechizos = new DataGridView();
			this.id = new DataGridViewTextBoxColumn();
			this.nombre = new DataGridViewTextBoxColumn();
			this.nivel = new DataGridViewTextBoxColumn();
			this.accion = new DataGridViewTextBoxColumn();
			((ISupportInitialize)this.dataGridView_hechizos).BeginInit();
			base.SuspendLayout();
			this.dataGridView_hechizos.AllowUserToAddRows = false;
			this.dataGridView_hechizos.AllowUserToDeleteRows = false;
			this.dataGridView_hechizos.AllowUserToOrderColumns = true;
			this.dataGridView_hechizos.BackgroundColor = Color.FromArgb(60, 63, 65);
			this.dataGridView_hechizos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView_hechizos.Columns.AddRange(new DataGridViewColumn[]
			{
				this.id,
				this.nombre,
				this.nivel,
				this.accion
			});
			this.dataGridView_hechizos.Dock = DockStyle.Fill;
			this.dataGridView_hechizos.Location = new Point(0, 0);
			this.dataGridView_hechizos.Margin = new Padding(3, 4, 3, 4);
			this.dataGridView_hechizos.MultiSelect = false;
			this.dataGridView_hechizos.Name = "dataGridView_hechizos";
			this.dataGridView_hechizos.ReadOnly = true;
			this.dataGridView_hechizos.RowHeadersVisible = false;
			this.dataGridView_hechizos.RowHeadersWidth = 51;
			this.dataGridView_hechizos.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			this.dataGridView_hechizos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView_hechizos.Size = new Size(790, 500);
			this.dataGridView_hechizos.TabIndex = 0;
			this.dataGridView_hechizos.CellContentClick += this.dataGridView_hechizos_CellContentClick;
			dataGridViewCellStyle.BackColor = Color.FromArgb(60, 63, 65);
			this.id.DefaultCellStyle = dataGridViewCellStyle;
			this.id.HeaderText = "Id";
			this.id.MinimumWidth = 100;
			this.id.Name = "id";
			this.id.ReadOnly = true;
			this.id.Width = 112;
			dataGridViewCellStyle2.BackColor = Color.FromArgb(60, 63, 65);
			dataGridViewCellStyle2.ForeColor = Color.White;
			this.nombre.DefaultCellStyle = dataGridViewCellStyle2;
			this.nombre.FillWeight = 300f;
			this.nombre.HeaderText = "Nom";
			this.nombre.MinimumWidth = 300;
			this.nombre.Name = "nombre";
			this.nombre.ReadOnly = true;
			this.nombre.Width = 338;
			dataGridViewCellStyle3.BackColor = Color.FromArgb(60, 63, 65);
			dataGridViewCellStyle3.ForeColor = Color.White;
			this.nivel.DefaultCellStyle = dataGridViewCellStyle3;
			this.nivel.HeaderText = "Niveau";
			this.nivel.MinimumWidth = 6;
			this.nivel.Name = "nivel";
			this.nivel.ReadOnly = true;
			this.nivel.Width = 112;
			dataGridViewCellStyle4.BackColor = Color.FromArgb(60, 63, 65);
			dataGridViewCellStyle4.ForeColor = Color.White;
			this.accion.DefaultCellStyle = dataGridViewCellStyle4;
			this.accion.FillWeight = 200f;
			this.accion.HeaderText = "Action";
			this.accion.MinimumWidth = 200;
			this.accion.Name = "accion";
			this.accion.ReadOnly = true;
			this.accion.Width = 225;
			base.AutoScaleDimensions = new SizeF(9f, 21f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.dataGridView_hechizos);
			this.Font = new Font("Segoe UI", 9.75f);
			base.Margin = new Padding(3, 4, 3, 4);
			base.Name = "UI_Hechizos";
			base.Size = new Size(790, 500);
			((ISupportInitialize)this.dataGridView_hechizos).EndInit();
			base.ResumeLayout(false);
		}

		// Token: 0x04000208 RID: 520
		private Cuenta cuenta;

		// Token: 0x04000209 RID: 521
		private IContainer components = null;

		// Token: 0x0400020A RID: 522
		private DataGridView dataGridView_hechizos;

		// Token: 0x0400020B RID: 523
		private DataGridViewTextBoxColumn id;

		// Token: 0x0400020C RID: 524
		private DataGridViewTextBoxColumn nombre;

		// Token: 0x0400020D RID: 525
		private DataGridViewTextBoxColumn nivel;

		// Token: 0x0400020E RID: 526
		private DataGridViewTextBoxColumn accion;
	}
}
