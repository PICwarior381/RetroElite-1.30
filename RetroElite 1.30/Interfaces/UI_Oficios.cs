using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using RetroElite.Otros;
using RetroElite.Otros.Game.Personaje.Oficios;

namespace RetroElite.Interfaces
{
	// Token: 0x02000071 RID: 113
	public class UI_Oficios : UserControl
	{
		// Token: 0x0600048B RID: 1163 RVA: 0x000130CE File Offset: 0x000114CE
		public UI_Oficios()
		{
			this.InitializeComponent();
			UI_Oficios.set_DoubleBuffered(this.dataGridView_oficios);
			UI_Oficios.set_DoubleBuffered(this.dataGridView_skills);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x000130FE File Offset: 0x000114FE
		public void set_Cuenta(Cuenta _cuenta)
		{
			this.cuenta = _cuenta;
			this.cuenta.juego.personaje.oficios_actualizados += this.personaje_Oficios_Actualizados;
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0001312A File Offset: 0x0001152A
		private void personaje_Oficios_Actualizados()
		{
			base.BeginInvoke(new Action(delegate()
			{
				this.dataGridView_oficios.Rows.Clear();
				foreach (Oficio oficio in this.cuenta.juego.personaje.oficios)
				{
					this.dataGridView_oficios.Rows.Add(new object[]
					{
						oficio.id,
						oficio.nombre,
						oficio.nivel,
						oficio.experiencia_actual.ToString() + "/" + oficio.experiencia_siguiente_nivel.ToString(),
						oficio.get_Experiencia_Porcentaje.ToString() + "%"
					});
				}
				this.dataGridView_skills.Rows.Clear();
				foreach (SkillsOficio skillsOficio in this.cuenta.juego.personaje.get_Skills_Disponibles())
				{
					this.dataGridView_skills.Rows.Add(new object[]
					{
						skillsOficio.id,
						skillsOficio.interactivo_modelo.nombre,
						skillsOficio.cantidad_minima,
						skillsOficio.cantidad_maxima,
						skillsOficio.es_craft ? (skillsOficio.tiempo.ToString() + "%") : skillsOficio.tiempo.ToString()
					});
				}
			}));
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00013140 File Offset: 0x00011540
		public static void set_DoubleBuffered(Control control)
		{
			typeof(Control).InvokeMember("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.SetProperty, null, control, new object[]
			{
				true
			});
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00013178 File Offset: 0x00011578
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x000131B0 File Offset: 0x000115B0
		private void InitializeComponent()
		{
			this.dataGridView_skills = new DataGridView();
			this.dataGridView_oficios = new DataGridView();
			this.Id = new DataGridViewTextBoxColumn();
			this.Nombre = new DataGridViewTextBoxColumn();
			this.Nivel = new DataGridViewTextBoxColumn();
			this.Experiencia = new DataGridViewTextBoxColumn();
			this.porcentaje = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
			this.cantidad_minima = new DataGridViewTextBoxColumn();
			this.cantidad_maxima = new DataGridViewTextBoxColumn();
			this.tiempo = new DataGridViewTextBoxColumn();
			((ISupportInitialize)this.dataGridView_skills).BeginInit();
			((ISupportInitialize)this.dataGridView_oficios).BeginInit();
			base.SuspendLayout();
			this.dataGridView_skills.AllowUserToAddRows = false;
			this.dataGridView_skills.AllowUserToDeleteRows = false;
			this.dataGridView_skills.AllowUserToOrderColumns = true;
			this.dataGridView_skills.AllowUserToResizeColumns = false;
			this.dataGridView_skills.AllowUserToResizeRows = false;
			this.dataGridView_skills.BackgroundColor = Color.FromArgb(60, 63, 65);
			this.dataGridView_skills.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView_skills.Columns.AddRange(new DataGridViewColumn[]
			{
				this.dataGridViewTextBoxColumn1,
				this.dataGridViewTextBoxColumn2,
				this.cantidad_minima,
				this.cantidad_maxima,
				this.tiempo
			});
			this.dataGridView_skills.Cursor = Cursors.Default;
			this.dataGridView_skills.Dock = DockStyle.Bottom;
			this.dataGridView_skills.Location = new Point(0, 261);
			this.dataGridView_skills.MultiSelect = false;
			this.dataGridView_skills.Name = "dataGridView_skills";
			this.dataGridView_skills.ReadOnly = true;
			this.dataGridView_skills.RowHeadersVisible = false;
			this.dataGridView_skills.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView_skills.Size = new Size(790, 239);
			this.dataGridView_skills.TabIndex = 3;
			this.dataGridView_oficios.AllowUserToAddRows = false;
			this.dataGridView_oficios.AllowUserToDeleteRows = false;
			this.dataGridView_oficios.AllowUserToOrderColumns = true;
			this.dataGridView_oficios.AllowUserToResizeColumns = false;
			this.dataGridView_oficios.AllowUserToResizeRows = false;
			this.dataGridView_oficios.BackgroundColor = Color.FromArgb(60, 63, 65);
			this.dataGridView_oficios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView_oficios.Columns.AddRange(new DataGridViewColumn[]
			{
				this.Id,
				this.Nombre,
				this.Nivel,
				this.Experiencia,
				this.porcentaje
			});
			this.dataGridView_oficios.Cursor = Cursors.Default;
			this.dataGridView_oficios.Dock = DockStyle.Fill;
			this.dataGridView_oficios.Location = new Point(0, 0);
			this.dataGridView_oficios.Name = "dataGridView_oficios";
			this.dataGridView_oficios.ReadOnly = true;
			this.dataGridView_oficios.RowHeadersVisible = false;
			this.dataGridView_oficios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView_oficios.Size = new Size(790, 261);
			this.dataGridView_oficios.TabIndex = 4;
			this.Id.FillWeight = 88.94537f;
			this.Id.HeaderText = "ID";
			this.Id.Name = "Id";
			this.Id.ReadOnly = true;
			this.Nombre.FillWeight = 187.5193f;
			this.Nombre.HeaderText = "Nom";
			this.Nombre.MinimumWidth = 200;
			this.Nombre.Name = "Nombre";
			this.Nombre.ReadOnly = true;
			this.Nombre.Width = 211;
			this.Nivel.FillWeight = 102.1648f;
			this.Nivel.HeaderText = "Niveau";
			this.Nivel.Name = "Nivel";
			this.Nivel.ReadOnly = true;
			this.Nivel.Width = 115;
			this.Experiencia.FillWeight = 210.5987f;
			this.Experiencia.HeaderText = "XP";
			this.Experiencia.MinimumWidth = 200;
			this.Experiencia.Name = "Experiencia";
			this.Experiencia.ReadOnly = true;
			this.Experiencia.Width = 236;
			this.porcentaje.FillWeight = 110.7719f;
			this.porcentaje.HeaderText = "%";
			this.porcentaje.Name = "porcentaje";
			this.porcentaje.ReadOnly = true;
			this.porcentaje.Width = 125;
			this.dataGridViewTextBoxColumn1.FillWeight = 73.69759f;
			this.dataGridViewTextBoxColumn1.HeaderText = "ID";
			this.dataGridViewTextBoxColumn1.MinimumWidth = 100;
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.ReadOnly = true;
			this.dataGridViewTextBoxColumn2.FillWeight = 144.6247f;
			this.dataGridViewTextBoxColumn2.HeaderText = "Nom";
			this.dataGridViewTextBoxColumn2.MinimumWidth = 180;
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.dataGridViewTextBoxColumn2.ReadOnly = true;
			this.dataGridViewTextBoxColumn2.Width = 196;
			this.cantidad_minima.FillWeight = 118.0339f;
			this.cantidad_minima.HeaderText = "Quantite Min";
			this.cantidad_minima.MinimumWidth = 130;
			this.cantidad_minima.Name = "cantidad_minima";
			this.cantidad_minima.ReadOnly = true;
			this.cantidad_minima.Width = 160;
			this.cantidad_maxima.FillWeight = 123.9451f;
			this.cantidad_maxima.HeaderText = "Quantite max";
			this.cantidad_maxima.MinimumWidth = 135;
			this.cantidad_maxima.Name = "cantidad_maxima";
			this.cantidad_maxima.ReadOnly = true;
			this.cantidad_maxima.Width = 169;
			this.tiempo.FillWeight = 119.6988f;
			this.tiempo.HeaderText = "Temps / %";
			this.tiempo.MinimumWidth = 130;
			this.tiempo.Name = "tiempo";
			this.tiempo.ReadOnly = true;
			this.tiempo.Width = 162;
			base.AutoScaleDimensions = new SizeF(7f, 17f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.dataGridView_oficios);
			base.Controls.Add(this.dataGridView_skills);
			this.Font = new Font("Segoe UI", 9.75f);
			base.Margin = new Padding(3, 4, 3, 4);
			base.Name = "UI_Oficios";
			base.Size = new Size(790, 500);
			((ISupportInitialize)this.dataGridView_skills).EndInit();
			((ISupportInitialize)this.dataGridView_oficios).EndInit();
			base.ResumeLayout(false);
		}

		// Token: 0x0400023B RID: 571
		private Cuenta cuenta;

		// Token: 0x0400023C RID: 572
		private IContainer components = null;

		// Token: 0x0400023D RID: 573
		private DataGridView dataGridView_skills;

		// Token: 0x0400023E RID: 574
		private DataGridView dataGridView_oficios;

		// Token: 0x0400023F RID: 575
		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

		// Token: 0x04000240 RID: 576
		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

		// Token: 0x04000241 RID: 577
		private DataGridViewTextBoxColumn cantidad_minima;

		// Token: 0x04000242 RID: 578
		private DataGridViewTextBoxColumn cantidad_maxima;

		// Token: 0x04000243 RID: 579
		private DataGridViewTextBoxColumn tiempo;

		// Token: 0x04000244 RID: 580
		private DataGridViewTextBoxColumn Id;

		// Token: 0x04000245 RID: 581
		private DataGridViewTextBoxColumn Nombre;

		// Token: 0x04000246 RID: 582
		private DataGridViewTextBoxColumn Nivel;

		// Token: 0x04000247 RID: 583
		private DataGridViewTextBoxColumn Experiencia;

		// Token: 0x04000248 RID: 584
		private DataGridViewTextBoxColumn porcentaje;
	}
}
