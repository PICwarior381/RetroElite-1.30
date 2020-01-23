using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DarkUI.Controls;
using RetroElite.Comun.Frames.Transporte;

namespace RetroElite.Interfaces
{
	// Token: 0x02000075 RID: 117
	public class UI_Debugger : UserControl
	{
		// Token: 0x060004D6 RID: 1238 RVA: 0x0001DBA1 File Offset: 0x0001BFA1
		public UI_Debugger()
		{
			this.InitializeComponent();
			this.lista_paquetes = new List<string>();
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0001DBC4 File Offset: 0x0001BFC4
		public void paquete_Recibido(string paquete)
		{
			this.agregar_Nuevo_Paquete(paquete, false);
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0001DBD0 File Offset: 0x0001BFD0
		public void paquete_Enviado(string paquete)
		{
			this.agregar_Nuevo_Paquete(paquete, true);
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0001DBDC File Offset: 0x0001BFDC
		private void agregar_Nuevo_Paquete(string paquete, bool enviado)
		{
			bool flag = !this.checkbox_debugger.Checked;
			if (!flag)
			{
				try
				{
					base.BeginInvoke(new Action(delegate()
					{
						bool flag2 = this.lista_paquetes.Count == 200;
						if (flag2)
						{
							this.lista_paquetes.RemoveAt(0);
							this.listView.Items.RemoveAt(0);
						}
						this.lista_paquetes.Add(paquete);
						ListViewItem listViewItem = this.listView.Items.Add(DateTime.Now.ToString("HH:mm:ss"));
						listViewItem.BackColor = (enviado ? Color.FromArgb(242, 174, 138) : Color.FromArgb(170, 196, 237));
						listViewItem.SubItems.Add(paquete);
					}));
				}
				catch
				{
				}
			}
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0001DC48 File Offset: 0x0001C048
		private void listView_SelectedIndexChanged(object sender, EventArgs e)
		{
			ListViewItem focusedItem = this.listView.FocusedItem;
			bool flag = (focusedItem != null && focusedItem.Index == -1) || this.listView.SelectedItems.Count == 0;
			if (!flag)
			{
				string text = this.lista_paquetes[this.listView.FocusedItem.Index];
				this.treeView.Nodes.Clear();
				bool flag2 = PaqueteRecibido.metodos.Count == 0;
				if (!flag2)
				{
					foreach (PaqueteDatos paqueteDatos in PaqueteRecibido.metodos)
					{
						bool flag3 = text.StartsWith(paqueteDatos.nombre_paquete);
						if (flag3)
						{
							this.treeView.Nodes.Add(paqueteDatos.nombre_paquete);
							this.treeView.Nodes[0].Nodes.Add(text.Remove(0, paqueteDatos.nombre_paquete.Length));
							this.treeView.Nodes[0].Expand();
							break;
						}
					}
				}
			}
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0001DD90 File Offset: 0x0001C190
		private void listView_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
		{
			e.Cancel = true;
			e.NewWidth = this.listView.Columns[e.ColumnIndex].Width;
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0001DDBD File Offset: 0x0001C1BD
		private void button_limpiar_logs_debugger_Click(object sender, EventArgs e)
		{
			this.lista_paquetes.Clear();
			this.listView.Items.Clear();
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0001DDE0 File Offset: 0x0001C1E0
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0001DE18 File Offset: 0x0001C218
		private void InitializeComponent()
		{
			this.splitContainer1 = new SplitContainer();
			this.tableLayoutPanel1 = new TableLayoutPanel();
			this.tableLayoutPanel2 = new TableLayoutPanel();
			this.checkbox_debugger = new DarkCheckBox();
			this.button_limpiar_logs_debugger = new DarkButton();
			this.listView = new ListView();
			this.fecha = new ColumnHeader();
			this.paquete = new ColumnHeader();
			this.treeView = new TreeView();
			((ISupportInitialize)this.splitContainer1).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			base.SuspendLayout();
			this.splitContainer1.Dock = DockStyle.Fill;
			this.splitContainer1.Location = new Point(0, 0);
			this.splitContainer1.Margin = new Padding(3, 4, 3, 4);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
			this.splitContainer1.Panel2.Controls.Add(this.treeView);
			this.splitContainer1.Size = new Size(790, 500);
			this.splitContainer1.SplitterDistance = 342;
			this.splitContainer1.SplitterWidth = 5;
			this.splitContainer1.TabIndex = 0;
			this.tableLayoutPanel1.BackColor = Color.FromArgb(65, 65, 65);
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.listView, 0, 1);
			this.tableLayoutPanel1.Dock = DockStyle.Fill;
			this.tableLayoutPanel1.Location = new Point(0, 0);
			this.tableLayoutPanel1.Margin = new Padding(3, 4, 3, 4);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 90f));
			this.tableLayoutPanel1.Size = new Size(342, 500);
			this.tableLayoutPanel1.TabIndex = 0;
			this.tableLayoutPanel2.ColumnCount = 2;
			this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25.59524f));
			this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 74.40476f));
			this.tableLayoutPanel2.Controls.Add(this.checkbox_debugger, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.button_limpiar_logs_debugger, 1, 0);
			this.tableLayoutPanel2.Dock = DockStyle.Fill;
			this.tableLayoutPanel2.Location = new Point(3, 4);
			this.tableLayoutPanel2.Margin = new Padding(3, 4, 3, 4);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
			this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 42f));
			this.tableLayoutPanel2.Size = new Size(336, 42);
			this.tableLayoutPanel2.TabIndex = 0;
			this.checkbox_debugger.AutoSize = true;
			this.checkbox_debugger.Checked = true;
			this.checkbox_debugger.CheckState = CheckState.Checked;
			this.checkbox_debugger.Dock = DockStyle.Fill;
			this.checkbox_debugger.Location = new Point(3, 4);
			this.checkbox_debugger.Margin = new Padding(3, 4, 3, 4);
			this.checkbox_debugger.Name = "checkbox_debugger";
			this.checkbox_debugger.Size = new Size(80, 34);
			this.checkbox_debugger.TabIndex = 0;
			this.checkbox_debugger.Text = "Activer";
			this.button_limpiar_logs_debugger.Dock = DockStyle.Fill;
			this.button_limpiar_logs_debugger.Location = new Point(89, 4);
			this.button_limpiar_logs_debugger.Margin = new Padding(3, 4, 3, 4);
			this.button_limpiar_logs_debugger.Name = "button_limpiar_logs_debugger";
			this.button_limpiar_logs_debugger.Padding = new Padding(5);
			this.button_limpiar_logs_debugger.Size = new Size(244, 34);
			this.button_limpiar_logs_debugger.TabIndex = 1;
			this.button_limpiar_logs_debugger.Text = "Clear";
			this.button_limpiar_logs_debugger.Click += this.button_limpiar_logs_debugger_Click;
			this.listView.BackColor = Color.FromArgb(60, 63, 65);
			this.listView.Columns.AddRange(new ColumnHeader[]
			{
				this.fecha,
				this.paquete
			});
			this.listView.ForeColor = Color.FromArgb(60, 63, 65);
			this.listView.Dock = DockStyle.Fill;
			this.listView.FullRowSelect = true;
			this.listView.HideSelection = false;
			this.listView.Location = new Point(3, 53);
			this.listView.MultiSelect = false;
			this.listView.Name = "listView";
			this.listView.Size = new Size(336, 444);
			this.listView.TabIndex = 1;
			this.listView.UseCompatibleStateImageBehavior = false;
			this.listView.View = View.Details;
			this.listView.ColumnWidthChanging += this.listView_ColumnWidthChanging;
			this.listView.SelectedIndexChanged += this.listView_SelectedIndexChanged;
			this.fecha.Text = "#";
			this.fecha.Width = 70;
			this.paquete.Text = "Packet";
			this.paquete.Width = 260;
			this.treeView.BackColor = Color.FromArgb(60, 63, 65);
			this.treeView.Dock = DockStyle.Fill;
			this.treeView.Location = new Point(0, 0);
			this.treeView.Name = "treeView";
			this.treeView.Size = new Size(443, 500);
			this.treeView.TabIndex = 0;
			base.AutoScaleDimensions = new SizeF(7f, 17f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.splitContainer1);
			this.Cursor = Cursors.Default;
			this.Font = new Font("Segoe UI", 9.75f);
			base.Margin = new Padding(3, 4, 3, 4);
			this.MinimumSize = new Size(790, 500);
			base.Name = "UI_Debugger";
			base.Size = new Size(790, 500);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((ISupportInitialize)this.splitContainer1).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x040002EC RID: 748
		private List<string> lista_paquetes;

		// Token: 0x040002ED RID: 749
		private IContainer components = null;

		// Token: 0x040002EE RID: 750
		private SplitContainer splitContainer1;

		// Token: 0x040002EF RID: 751
		private TableLayoutPanel tableLayoutPanel1;

		// Token: 0x040002F0 RID: 752
		private TableLayoutPanel tableLayoutPanel2;

		// Token: 0x040002F1 RID: 753
		private TreeView treeView;

		// Token: 0x040002F2 RID: 754
		private ListView listView;

		// Token: 0x040002F3 RID: 755
		private ColumnHeader fecha;

		// Token: 0x040002F4 RID: 756
		private ColumnHeader paquete;

		// Token: 0x040002F5 RID: 757
		private DarkCheckBox checkbox_debugger;

		// Token: 0x040002F6 RID: 758
		private DarkButton button_limpiar_logs_debugger;
	}
}
