using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using RetroElite.Otros;
using RetroElite.Otros.Game.Personaje.Inventario;
using RetroElite.Otros.Game.Personaje.Inventario.Enums;

namespace RetroElite.Interfaces
{
	// Token: 0x0200006F RID: 111
	public class UI_Inventario : UserControl
	{
		// Token: 0x06000477 RID: 1143 RVA: 0x00010B54 File Offset: 0x0000EF54
		public UI_Inventario(Cuenta _cuenta)
		{
			this.InitializeComponent();
			UI_Inventario.set_DoubleBuffered(this.dataGridView_equipamientos);
			UI_Inventario.set_DoubleBuffered(this.dataGridView_varios);
			UI_Inventario.set_DoubleBuffered(this.dataGridView_recursos);
			this.cuenta = _cuenta;
			this.cuenta.juego.personaje.inventario.inventario_actualizado += this.actualizar_Inventario;
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00010BD0 File Offset: 0x0000EFD0
		private void actualizar_Inventario(bool objetos_actualizados)
		{
			bool flag = !objetos_actualizados;
			if (!flag)
			{
				Task.Run(delegate()
				{
					bool flag2 = !base.IsHandleCreated;
					if (!flag2)
					{
						base.BeginInvoke(new Action(delegate()
						{
							this.dataGridView_equipamientos.Rows.Clear();
							foreach (ObjetosInventario objetosInventario in this.cuenta.juego.personaje.inventario.equipamiento)
							{
								this.dataGridView_equipamientos.Rows.Add(new object[]
								{
									objetosInventario.id_inventario,
									objetosInventario.id_modelo,
									objetosInventario.nombre,
									objetosInventario.cantidad,
									objetosInventario.posicion,
									(objetosInventario.posicion == InventarioPosiciones.PAS_EQUIPE) ? "Equiper" : "Desequiper",
									"Supprimer"
								});
							}
							this.dataGridView_varios.Rows.Clear();
							foreach (ObjetosInventario objetosInventario2 in this.cuenta.juego.personaje.inventario.varios)
							{
								this.dataGridView_varios.Rows.Add(new object[]
								{
									objetosInventario2.id_inventario,
									objetosInventario2.id_modelo,
									objetosInventario2.nombre,
									objetosInventario2.cantidad,
									objetosInventario2.pods,
									"Supprimer"
								});
							}
							this.dataGridView_recursos.Rows.Clear();
							foreach (ObjetosInventario objetosInventario3 in this.cuenta.juego.personaje.inventario.recursos)
							{
								this.dataGridView_recursos.Rows.Add(new object[]
								{
									objetosInventario3.id_inventario,
									objetosInventario3.id_modelo,
									objetosInventario3.nombre,
									objetosInventario3.cantidad,
									objetosInventario3.pods,
									"Supprimer"
								});
							}
							this.dataGridView_mision.Rows.Clear();
							foreach (ObjetosInventario objetosInventario4 in this.cuenta.juego.personaje.inventario.mision)
							{
								this.dataGridView_mision.Rows.Add(new object[]
								{
									objetosInventario4.id_inventario,
									objetosInventario4.id_modelo,
									objetosInventario4.nombre,
									objetosInventario4.cantidad
								});
							}
						}));
					}
				});
			}
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00010BFC File Offset: 0x0000EFFC
		private void dataGridView_equipamientos_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			bool flag = e.ColumnIndex < 5;
			if (!flag)
			{
				ObjetosInventario objetosInventario = this.cuenta.juego.personaje.inventario.equipamiento.ElementAt(e.RowIndex);
				string text = this.dataGridView_equipamientos.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
				string text2 = text;
				string text3 = text2;
				if (text3 != null)
				{
					if (!(text3 == "Equiper"))
					{
						if (!(text3 == "Desequiper"))
						{
							if (text3 == "Supprimer")
							{
								bool flag2 = MessageBox.Show("Tu es sur de supprimer ? " + objetosInventario.nombre + "?", "Supprimer un objet", MessageBoxButtons.YesNo) == DialogResult.Yes;
								if (flag2)
								{
									this.cuenta.juego.personaje.inventario.eliminar_Objeto(objetosInventario, 1, true);
								}
							}
						}
						else
						{
							this.cuenta.juego.personaje.inventario.desequipar_Objeto(objetosInventario);
						}
					}
					else
					{
						this.cuenta.juego.personaje.inventario.equipar_Objeto(objetosInventario);
					}
				}
			}
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00010D30 File Offset: 0x0000F130
		private void dataGridView_recursos_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			bool flag = e.ColumnIndex < 5;
			if (!flag)
			{
				ObjetosInventario objetosInventario = this.cuenta.juego.personaje.inventario.recursos.ElementAt(e.RowIndex);
				string text = this.dataGridView_recursos.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
				int cantidad;
				bool flag2 = !int.TryParse(Interaction.InputBox(string.Concat(new string[]
				{
					"Réduire la quantité de ",
					text.ToLower(),
					" de l'objet ",
					objetosInventario.nombre,
					" (0 = tout):"
				}), text, "1", -1, -1), out cantidad);
				if (!flag2)
				{
					string text2 = text;
					string text3 = text2;
					if (text3 != null)
					{
						if (text3 == "Supprimer")
						{
							this.cuenta.juego.personaje.inventario.eliminar_Objeto(objetosInventario, cantidad, true);
						}
					}
				}
			}
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00010E38 File Offset: 0x0000F238
		public static void set_DoubleBuffered(Control control)
		{
			typeof(Control).InvokeMember("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.SetProperty, null, control, new object[]
			{
				true
			});
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00010E70 File Offset: 0x0000F270
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00010EA8 File Offset: 0x0000F2A8
		private void InitializeComponent()
		{
			this.components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(UI_Inventario));
			this.tabControl_mision = new TabControl();
			this.tabPage_equipamiento = new TabPage();
			this.dataGridView_equipamientos = new DataGridView();
			this.tabPage_varios = new TabPage();
			this.dataGridView_varios = new DataGridView();
			this.tabPage_recursos = new TabPage();
			this.dataGridView_recursos = new DataGridView();
			this.tabPage_mision = new TabPage();
			this.dataGridView_mision = new DataGridView();
			this.imageList1 = new ImageList(this.components);
			this.id_inventario_varios = new DataGridViewTextBoxColumn();
			this.id_modelo_varios = new DataGridViewTextBoxColumn();
			this.nombre_varios = new DataGridViewTextBoxColumn();
			this.cantidad_varios = new DataGridViewTextBoxColumn();
			this.pods_varios = new DataGridViewTextBoxColumn();
			this.eliminar_varios = new DataGridViewTextBoxColumn();
			this.id_Inventario_equipamiento = new DataGridViewTextBoxColumn();
			this.id_modelo_equipamiento = new DataGridViewTextBoxColumn();
			this.nombre_equipamiento = new DataGridViewTextBoxColumn();
			this.cantidad_equipamiento = new DataGridViewTextBoxColumn();
			this.posicion_equipamiento = new DataGridViewTextBoxColumn();
			this.accion_equipamiento = new DataGridViewTextBoxColumn();
			this.eliminar_equipamiento = new DataGridViewTextBoxColumn();
			this.id_inventario_recursos = new DataGridViewTextBoxColumn();
			this.id_modelo_recursos = new DataGridViewTextBoxColumn();
			this.nombre_recursos = new DataGridViewTextBoxColumn();
			this.cantidad_recursos = new DataGridViewTextBoxColumn();
			this.Pods_recursos = new DataGridViewTextBoxColumn();
			this.eliminar_recursos = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
			this.tabControl_mision.SuspendLayout();
			this.tabPage_equipamiento.SuspendLayout();
			((ISupportInitialize)this.dataGridView_equipamientos).BeginInit();
			this.tabPage_varios.SuspendLayout();
			((ISupportInitialize)this.dataGridView_varios).BeginInit();
			this.tabPage_recursos.SuspendLayout();
			((ISupportInitialize)this.dataGridView_recursos).BeginInit();
			this.tabPage_mision.SuspendLayout();
			((ISupportInitialize)this.dataGridView_mision).BeginInit();
			base.SuspendLayout();
			this.tabControl_mision.Controls.Add(this.tabPage_equipamiento);
			this.tabControl_mision.Controls.Add(this.tabPage_varios);
			this.tabControl_mision.Controls.Add(this.tabPage_recursos);
			this.tabControl_mision.Controls.Add(this.tabPage_mision);
			this.tabControl_mision.Cursor = Cursors.Default;
			this.tabControl_mision.Dock = DockStyle.Fill;
			this.tabControl_mision.Font = new Font("Segoe UI", 9.75f);
			this.tabControl_mision.ImageList = this.imageList1;
			this.tabControl_mision.ItemSize = new Size(67, 26);
			this.tabControl_mision.Location = new Point(0, 0);
			this.tabControl_mision.Name = "tabControl_mision";
			this.tabControl_mision.SelectedIndex = 0;
			this.tabControl_mision.Size = new Size(790, 500);
			this.tabControl_mision.TabIndex = 0;
			this.tabPage_equipamiento.Controls.Add(this.dataGridView_equipamientos);
			this.tabPage_equipamiento.ImageIndex = 0;
			this.tabPage_equipamiento.Location = new Point(4, 30);
			this.tabPage_equipamiento.Name = "tabPage_equipamiento";
			this.tabPage_equipamiento.Padding = new Padding(3);
			this.tabPage_equipamiento.Size = new Size(782, 466);
			this.tabPage_equipamiento.TabIndex = 0;
			this.tabPage_equipamiento.Text = "Equipement";
			this.tabPage_equipamiento.UseVisualStyleBackColor = true;
			this.dataGridView_equipamientos.AllowUserToAddRows = false;
			this.dataGridView_equipamientos.AllowUserToDeleteRows = false;
			this.dataGridView_equipamientos.AllowUserToOrderColumns = true;
			this.dataGridView_equipamientos.AllowUserToResizeColumns = false;
			this.dataGridView_equipamientos.AllowUserToResizeRows = false;
			this.dataGridView_equipamientos.BackgroundColor = Color.FromArgb(60, 63, 65);
			this.dataGridView_equipamientos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dataGridView_equipamientos.Columns.AddRange(new DataGridViewColumn[]
			{
				this.id_Inventario_equipamiento,
				this.id_modelo_equipamiento,
				this.nombre_equipamiento,
				this.cantidad_equipamiento,
				this.posicion_equipamiento,
				this.accion_equipamiento,
				this.eliminar_equipamiento
			});
			this.dataGridView_equipamientos.Cursor = Cursors.Default;
			this.dataGridView_equipamientos.Dock = DockStyle.Fill;
			this.dataGridView_equipamientos.Location = new Point(3, 3);
			this.dataGridView_equipamientos.Name = "dataGridView_equipamientos";
			this.dataGridView_equipamientos.ReadOnly = true;
			this.dataGridView_equipamientos.RowHeadersVisible = false;
			this.dataGridView_equipamientos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView_equipamientos.Size = new Size(776, 460);
			this.dataGridView_equipamientos.TabIndex = 0;
			this.dataGridView_equipamientos.CellContentClick += this.dataGridView_equipamientos_CellContentClick;
			this.tabPage_varios.Controls.Add(this.dataGridView_varios);
			this.tabPage_varios.ImageIndex = 1;
			this.tabPage_varios.Location = new Point(4, 30);
			this.tabPage_varios.Name = "tabPage_varios";
			this.tabPage_varios.Padding = new Padding(3);
			this.tabPage_varios.Size = new Size(782, 466);
			this.tabPage_varios.TabIndex = 1;
			this.tabPage_varios.Text = "Consomable";
			this.tabPage_varios.UseVisualStyleBackColor = true;
			this.dataGridView_varios.AllowUserToAddRows = false;
			this.dataGridView_varios.AllowUserToDeleteRows = false;
			this.dataGridView_varios.AllowUserToOrderColumns = true;
			this.dataGridView_varios.AllowUserToResizeColumns = false;
			this.dataGridView_varios.AllowUserToResizeRows = false;
			this.dataGridView_varios.BackgroundColor = Color.FromArgb(60, 63, 65);
			this.dataGridView_varios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView_varios.Columns.AddRange(new DataGridViewColumn[]
			{
				this.id_inventario_varios,
				this.id_modelo_varios,
				this.nombre_varios,
				this.cantidad_varios,
				this.pods_varios,
				this.eliminar_varios
			});
			this.dataGridView_varios.Cursor = Cursors.Default;
			this.dataGridView_varios.Dock = DockStyle.Fill;
			this.dataGridView_varios.Location = new Point(3, 3);
			this.dataGridView_varios.Name = "dataGridView_varios";
			this.dataGridView_varios.ReadOnly = true;
			this.dataGridView_varios.RowHeadersVisible = false;
			this.dataGridView_varios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView_varios.Size = new Size(776, 460);
			this.dataGridView_varios.TabIndex = 2;
			this.tabPage_recursos.Controls.Add(this.dataGridView_recursos);
			this.tabPage_recursos.ImageIndex = 2;
			this.tabPage_recursos.Location = new Point(4, 30);
			this.tabPage_recursos.Name = "tabPage_recursos";
			this.tabPage_recursos.Padding = new Padding(3);
			this.tabPage_recursos.Size = new Size(782, 466);
			this.tabPage_recursos.TabIndex = 2;
			this.tabPage_recursos.Text = "Ressource";
			this.tabPage_recursos.UseVisualStyleBackColor = true;
			this.dataGridView_recursos.AllowUserToAddRows = false;
			this.dataGridView_recursos.AllowUserToDeleteRows = false;
			this.dataGridView_recursos.AllowUserToOrderColumns = true;
			this.dataGridView_recursos.AllowUserToResizeColumns = false;
			this.dataGridView_recursos.AllowUserToResizeRows = false;
			this.dataGridView_recursos.BackgroundColor = Color.FromArgb(60, 63, 65);
			this.dataGridView_recursos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView_recursos.Columns.AddRange(new DataGridViewColumn[]
			{
				this.id_inventario_recursos,
				this.id_modelo_recursos,
				this.nombre_recursos,
				this.cantidad_recursos,
				this.Pods_recursos,
				this.eliminar_recursos
			});
			this.dataGridView_recursos.Cursor = Cursors.Default;
			this.dataGridView_recursos.Dock = DockStyle.Fill;
			this.dataGridView_recursos.Location = new Point(3, 3);
			this.dataGridView_recursos.Name = "dataGridView_recursos";
			this.dataGridView_recursos.ReadOnly = true;
			this.dataGridView_recursos.RowHeadersVisible = false;
			this.dataGridView_recursos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView_recursos.Size = new Size(776, 460);
			this.dataGridView_recursos.TabIndex = 1;
			this.dataGridView_recursos.CellContentClick += this.dataGridView_recursos_CellContentClick;
			this.tabPage_mision.Controls.Add(this.dataGridView_mision);
			this.tabPage_mision.ImageIndex = 3;
			this.tabPage_mision.Location = new Point(4, 30);
			this.tabPage_mision.Name = "tabPage_mision";
			this.tabPage_mision.Padding = new Padding(3);
			this.tabPage_mision.Size = new Size(782, 466);
			this.tabPage_mision.TabIndex = 3;
			this.tabPage_mision.Text = "Objet quete";
			this.tabPage_mision.UseVisualStyleBackColor = true;
			this.dataGridView_mision.AllowUserToAddRows = false;
			this.dataGridView_mision.AllowUserToDeleteRows = false;
			this.dataGridView_mision.AllowUserToOrderColumns = true;
			this.dataGridView_mision.AllowUserToResizeColumns = false;
			this.dataGridView_mision.AllowUserToResizeRows = false;
			this.dataGridView_mision.BackgroundColor = Color.FromArgb(60, 63, 65);
			this.dataGridView_mision.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView_mision.Columns.AddRange(new DataGridViewColumn[]
			{
				this.dataGridViewTextBoxColumn1,
				this.dataGridViewTextBoxColumn2,
				this.dataGridViewTextBoxColumn3,
				this.dataGridViewTextBoxColumn4
			});
			this.dataGridView_mision.Cursor = Cursors.Default;
			this.dataGridView_mision.Dock = DockStyle.Fill;
			this.dataGridView_mision.Location = new Point(3, 3);
			this.dataGridView_mision.Name = "dataGridView_mision";
			this.dataGridView_mision.ReadOnly = true;
			this.dataGridView_mision.RowHeadersVisible = false;
			this.dataGridView_mision.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView_mision.Size = new Size(776, 460);
			this.dataGridView_mision.TabIndex = 2;
			this.imageList1.ImageStream = (ImageListStreamer)componentResourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "espada.png");
			this.imageList1.Images.SetKeyName(1, "pocion.png");
			this.imageList1.Images.SetKeyName(2, "recursos.png");
			this.imageList1.Images.SetKeyName(3, "llave.png");
			this.id_inventario_varios.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.id_inventario_varios.HeaderText = "Id";
			this.id_inventario_varios.MinimumWidth = 110;
			this.id_inventario_varios.Name = "id_inventario_varios";
			this.id_inventario_varios.ReadOnly = true;
			this.id_inventario_varios.Width = 110;
			this.id_modelo_varios.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.id_modelo_varios.HeaderText = "ItemId";
			this.id_modelo_varios.MinimumWidth = 100;
			this.id_modelo_varios.Name = "id_modelo_varios";
			this.id_modelo_varios.ReadOnly = true;
			this.nombre_varios.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.nombre_varios.HeaderText = "Nom";
			this.nombre_varios.MinimumWidth = 100;
			this.nombre_varios.Name = "nombre_varios";
			this.nombre_varios.ReadOnly = true;
			this.cantidad_varios.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.cantidad_varios.HeaderText = "Quantite";
			this.cantidad_varios.MinimumWidth = 100;
			this.cantidad_varios.Name = "cantidad_varios";
			this.cantidad_varios.ReadOnly = true;
			this.pods_varios.HeaderText = "Pods";
			this.pods_varios.Name = "pods_varios";
			this.pods_varios.ReadOnly = true;
			this.eliminar_varios.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.eliminar_varios.HeaderText = "Enlever";
			this.eliminar_varios.MinimumWidth = 100;
			this.eliminar_varios.Name = "eliminar_varios";
			this.eliminar_varios.ReadOnly = true;
			this.id_Inventario_equipamiento.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.id_Inventario_equipamiento.HeaderText = "Id";
			this.id_Inventario_equipamiento.MinimumWidth = 110;
			this.id_Inventario_equipamiento.Name = "id_Inventario_equipamiento";
			this.id_Inventario_equipamiento.ReadOnly = true;
			this.id_Inventario_equipamiento.Width = 110;
			this.id_modelo_equipamiento.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.id_modelo_equipamiento.FillWeight = 90f;
			this.id_modelo_equipamiento.HeaderText = "ItemId";
			this.id_modelo_equipamiento.MinimumWidth = 90;
			this.id_modelo_equipamiento.Name = "id_modelo_equipamiento";
			this.id_modelo_equipamiento.ReadOnly = true;
			this.id_modelo_equipamiento.Width = 90;
			this.nombre_equipamiento.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.nombre_equipamiento.HeaderText = "Nom";
			this.nombre_equipamiento.MinimumWidth = 120;
			this.nombre_equipamiento.Name = "nombre_equipamiento";
			this.nombre_equipamiento.ReadOnly = true;
			this.nombre_equipamiento.Width = 120;
			this.cantidad_equipamiento.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.cantidad_equipamiento.FillWeight = 80f;
			this.cantidad_equipamiento.HeaderText = "Quantite";
			this.cantidad_equipamiento.MinimumWidth = 80;
			this.cantidad_equipamiento.Name = "cantidad_equipamiento";
			this.cantidad_equipamiento.ReadOnly = true;
			this.cantidad_equipamiento.Width = 82;
			this.posicion_equipamiento.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.posicion_equipamiento.HeaderText = "Emplacement";
			this.posicion_equipamiento.MinimumWidth = 100;
			this.posicion_equipamiento.Name = "posicion_equipamiento";
			this.posicion_equipamiento.ReadOnly = true;
			this.posicion_equipamiento.Width = 111;
			this.accion_equipamiento.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.accion_equipamiento.HeaderText = "Action";
			this.accion_equipamiento.MaxInputLength = 50;
			this.accion_equipamiento.MinimumWidth = 100;
			this.accion_equipamiento.Name = "accion_equipamiento";
			this.accion_equipamiento.ReadOnly = true;
			this.eliminar_equipamiento.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.eliminar_equipamiento.HeaderText = "Enlever";
			this.eliminar_equipamiento.MinimumWidth = 100;
			this.eliminar_equipamiento.Name = "eliminar_equipamiento";
			this.eliminar_equipamiento.ReadOnly = true;
			this.id_inventario_recursos.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.id_inventario_recursos.HeaderText = "Id";
			this.id_inventario_recursos.MinimumWidth = 110;
			this.id_inventario_recursos.Name = "id_inventario_recursos";
			this.id_inventario_recursos.ReadOnly = true;
			this.id_inventario_recursos.Width = 110;
			this.id_modelo_recursos.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.id_modelo_recursos.HeaderText = "ItemId";
			this.id_modelo_recursos.MinimumWidth = 100;
			this.id_modelo_recursos.Name = "id_modelo_recursos";
			this.id_modelo_recursos.ReadOnly = true;
			this.nombre_recursos.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.nombre_recursos.HeaderText = "Nom";
			this.nombre_recursos.MinimumWidth = 100;
			this.nombre_recursos.Name = "nombre_recursos";
			this.nombre_recursos.ReadOnly = true;
			this.cantidad_recursos.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.cantidad_recursos.HeaderText = "Quantite";
			this.cantidad_recursos.MinimumWidth = 100;
			this.cantidad_recursos.Name = "cantidad_recursos";
			this.cantidad_recursos.ReadOnly = true;
			this.Pods_recursos.HeaderText = "Pods";
			this.Pods_recursos.Name = "Pods_recursos";
			this.Pods_recursos.ReadOnly = true;
			this.eliminar_recursos.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.eliminar_recursos.HeaderText = "Enlever";
			this.eliminar_recursos.MinimumWidth = 100;
			this.eliminar_recursos.Name = "eliminar_recursos";
			this.eliminar_recursos.ReadOnly = true;
			this.dataGridViewTextBoxColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.dataGridViewTextBoxColumn1.HeaderText = "Id";
			this.dataGridViewTextBoxColumn1.MinimumWidth = 110;
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.ReadOnly = true;
			this.dataGridViewTextBoxColumn1.Width = 110;
			this.dataGridViewTextBoxColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.dataGridViewTextBoxColumn2.HeaderText = "ItemId";
			this.dataGridViewTextBoxColumn2.MinimumWidth = 100;
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.dataGridViewTextBoxColumn2.ReadOnly = true;
			this.dataGridViewTextBoxColumn3.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.dataGridViewTextBoxColumn3.HeaderText = "Nom";
			this.dataGridViewTextBoxColumn3.MinimumWidth = 100;
			this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			this.dataGridViewTextBoxColumn3.ReadOnly = true;
			this.dataGridViewTextBoxColumn4.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.dataGridViewTextBoxColumn4.HeaderText = "Quantite";
			this.dataGridViewTextBoxColumn4.MaxInputLength = 200;
			this.dataGridViewTextBoxColumn4.MinimumWidth = 100;
			this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
			this.dataGridViewTextBoxColumn4.ReadOnly = true;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.tabControl_mision);
			base.Name = "UI_Inventario";
			base.Size = new Size(790, 500);
			this.tabControl_mision.ResumeLayout(false);
			this.tabPage_equipamiento.ResumeLayout(false);
			((ISupportInitialize)this.dataGridView_equipamientos).EndInit();
			this.tabPage_varios.ResumeLayout(false);
			((ISupportInitialize)this.dataGridView_varios).EndInit();
			this.tabPage_recursos.ResumeLayout(false);
			((ISupportInitialize)this.dataGridView_recursos).EndInit();
			this.tabPage_mision.ResumeLayout(false);
			((ISupportInitialize)this.dataGridView_mision).EndInit();
			base.ResumeLayout(false);
		}

		// Token: 0x0400020F RID: 527
		private Cuenta cuenta = null;

		// Token: 0x04000210 RID: 528
		private IContainer components = null;

		// Token: 0x04000211 RID: 529
		private TabControl tabControl_mision;

		// Token: 0x04000212 RID: 530
		private TabPage tabPage_equipamiento;

		// Token: 0x04000213 RID: 531
		private TabPage tabPage_varios;

		// Token: 0x04000214 RID: 532
		private ImageList imageList1;

		// Token: 0x04000215 RID: 533
		private TabPage tabPage_recursos;

		// Token: 0x04000216 RID: 534
		private TabPage tabPage_mision;

		// Token: 0x04000217 RID: 535
		private DataGridView dataGridView_equipamientos;

		// Token: 0x04000218 RID: 536
		private DataGridView dataGridView_recursos;

		// Token: 0x04000219 RID: 537
		private DataGridView dataGridView_varios;

		// Token: 0x0400021A RID: 538
		private DataGridView dataGridView_mision;

		// Token: 0x0400021B RID: 539
		private DataGridViewTextBoxColumn id_Inventario_equipamiento;

		// Token: 0x0400021C RID: 540
		private DataGridViewTextBoxColumn id_modelo_equipamiento;

		// Token: 0x0400021D RID: 541
		private DataGridViewTextBoxColumn nombre_equipamiento;

		// Token: 0x0400021E RID: 542
		private DataGridViewTextBoxColumn cantidad_equipamiento;

		// Token: 0x0400021F RID: 543
		private DataGridViewTextBoxColumn posicion_equipamiento;

		// Token: 0x04000220 RID: 544
		private DataGridViewTextBoxColumn accion_equipamiento;

		// Token: 0x04000221 RID: 545
		private DataGridViewTextBoxColumn eliminar_equipamiento;

		// Token: 0x04000222 RID: 546
		private DataGridViewTextBoxColumn id_inventario_varios;

		// Token: 0x04000223 RID: 547
		private DataGridViewTextBoxColumn id_modelo_varios;

		// Token: 0x04000224 RID: 548
		private DataGridViewTextBoxColumn nombre_varios;

		// Token: 0x04000225 RID: 549
		private DataGridViewTextBoxColumn cantidad_varios;

		// Token: 0x04000226 RID: 550
		private DataGridViewTextBoxColumn pods_varios;

		// Token: 0x04000227 RID: 551
		private DataGridViewTextBoxColumn eliminar_varios;

		// Token: 0x04000228 RID: 552
		private DataGridViewTextBoxColumn id_inventario_recursos;

		// Token: 0x04000229 RID: 553
		private DataGridViewTextBoxColumn id_modelo_recursos;

		// Token: 0x0400022A RID: 554
		private DataGridViewTextBoxColumn nombre_recursos;

		// Token: 0x0400022B RID: 555
		private DataGridViewTextBoxColumn cantidad_recursos;

		// Token: 0x0400022C RID: 556
		private DataGridViewTextBoxColumn Pods_recursos;

		// Token: 0x0400022D RID: 557
		private DataGridViewTextBoxColumn eliminar_recursos;

		// Token: 0x0400022E RID: 558
		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

		// Token: 0x0400022F RID: 559
		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

		// Token: 0x04000230 RID: 560
		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

		// Token: 0x04000231 RID: 561
		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
	}
}
