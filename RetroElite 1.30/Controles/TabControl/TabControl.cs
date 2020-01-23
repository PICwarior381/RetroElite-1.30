using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using RetroElite.Controles.LayoutPanel;

namespace RetroElite.Controles.TabControl
{
	// Token: 0x0200007A RID: 122
	public class TabControl : UserControl
	{
		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000513 RID: 1299 RVA: 0x00022704 File Offset: 0x00020B04
		public List<string> titulos_paginas
		{
			get
			{
				return this.paginas.Keys.ToList<string>();
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x00022716 File Offset: 0x00020B16
		public Pagina pagina_seleccionada
		{
			get
			{
				return (this.nombre_pagina_seleccionada == null) ? null : (this.paginas.ContainsKey(this.nombre_pagina_seleccionada) ? this.paginas[this.nombre_pagina_seleccionada] : null);
			}
		}

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06000515 RID: 1301 RVA: 0x0002274C File Offset: 0x00020B4C
		// (remove) Token: 0x06000516 RID: 1302 RVA: 0x00022784 File Offset: 0x00020B84
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler pagina_cambiada;

		// Token: 0x06000517 RID: 1303 RVA: 0x000227B9 File Offset: 0x00020BB9
		public TabControl()
		{
			this.InitializeComponent();
			this.anchura_cabezera = 164;
			this.paginas = new Dictionary<string, Pagina>();
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x000227E8 File Offset: 0x00020BE8
		public Pagina agregar_Nueva_Pagina(string titulo)
		{
			bool flag = string.IsNullOrEmpty(titulo);
			if (flag)
			{
				throw new ArgumentNullException("Nombre de la cuenta vacía");
			}
			bool flag2 = this.paginas.ContainsKey(titulo);
			if (flag2)
			{
				throw new InvalidOperationException("Ya existe una cuenta cargada con este nombre");
			}
			bool flag3 = this.panelCabezeraCuentas.Controls.Count > 0;
			if (flag3)
			{
				this.panelCabezeraCuentas.Controls[this.panelCabezeraCuentas.Controls.Count - 1].Margin = new Padding(2, 0, 2, 0);
			}
			Pagina pagina = new Pagina(titulo, this.anchura_cabezera);
			this.paginas.Add(titulo, pagina);
			pagina.cabezera.Click += delegate(object s, EventArgs e)
			{
				this.seleccionar_Pagina((s as Cabezera).propiedad_Cuenta);
			};
			pagina.contenido.Disposed += delegate(object s, EventArgs e)
			{
				this.eliminar_Pagina(pagina.cabezera.propiedad_Cuenta);
			};
			this.panelCabezeraCuentas.Controls.Add(pagina.cabezera);
			this.panelContenidoCuenta.Controls.Add(pagina.contenido);
			this.ajustar_Cabezera_Anchura();
			this.seleccionar_Pagina(titulo);
			return pagina;
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00022930 File Offset: 0x00020D30
		public void eliminar_Pagina(string titulo)
		{
			bool flag = this.paginas.ContainsKey(titulo);
			if (flag)
			{
				Pagina pagina = this.paginas[titulo];
				this.panelCabezeraCuentas.Controls.Remove(pagina.cabezera);
				this.panelContenidoCuenta.Controls.Remove(pagina.contenido);
				pagina.cabezera.Dispose();
				pagina.contenido.Dispose();
				this.paginas.Remove(titulo);
				bool flag2 = this.nombre_pagina_seleccionada == titulo;
				if (flag2)
				{
					this.nombre_pagina_seleccionada = null;
					bool flag3 = this.paginas.Count > 0;
					if (flag3)
					{
						this.seleccionar_Pagina(this.titulos_paginas[0]);
					}
				}
				this.ajustar_Cabezera_Anchura();
				GC.Collect();
			}
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00022A08 File Offset: 0x00020E08
		private void ajustar_Cabezera_Anchura()
		{
			bool flag = this.anchura_cabezera == 164 && this.panelCabezeraCuentas.VerticalScroll.Visible;
			if (flag)
			{
				this.anchura_cabezera = 150;
				this.panelCabezeraCuentas.SuspendLayout();
				for (int i = 0; i < this.panelCabezeraCuentas.Controls.Count; i++)
				{
					this.panelCabezeraCuentas.Controls[i].Size = new Size(this.anchura_cabezera, 40);
				}
				this.panelCabezeraCuentas.ResumeLayout();
			}
			else
			{
				bool flag2 = this.anchura_cabezera == 150 && !this.panelCabezeraCuentas.VerticalScroll.Visible;
				if (flag2)
				{
					this.anchura_cabezera = 164;
					this.panelCabezeraCuentas.SuspendLayout();
					for (int j = 0; j < this.panelCabezeraCuentas.Controls.Count; j++)
					{
						this.panelCabezeraCuentas.Controls[j].Size = new Size(this.anchura_cabezera, 40);
					}
					this.panelCabezeraCuentas.ResumeLayout();
				}
			}
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00022B44 File Offset: 0x00020F44
		public void seleccionar_Pagina(string title)
		{
			bool flag = this.nombre_pagina_seleccionada != title;
			if (flag)
			{
				bool flag2 = this.paginas.ContainsKey(title);
				if (!flag2)
				{
					throw new InvalidOperationException("No se puede seleccionar una página que no existe.");
				}
				bool flag3 = this.nombre_pagina_seleccionada != null && this.paginas.ContainsKey(this.nombre_pagina_seleccionada);
				if (flag3)
				{
					Pagina pagina = this.paginas[this.nombre_pagina_seleccionada];
					pagina.cabezera.propiedad_Esta_Seleccionada = false;
					pagina.contenido.Visible = false;
				}
				this.nombre_pagina_seleccionada = title;
				this.pagina_seleccionada.cabezera.propiedad_Esta_Seleccionada = true;
				this.pagina_seleccionada.contenido.Visible = true;
				EventHandler eventHandler = this.pagina_cambiada;
				if (eventHandler != null)
				{
					eventHandler(this.paginas[this.nombre_pagina_seleccionada], EventArgs.Empty);
				}
			}
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00022C30 File Offset: 0x00021030
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00022C68 File Offset: 0x00021068
		private void InitializeComponent()
		{
			this.panelCabezeraCuentas = new FlowLayoutPanelBuffered();
			this.panelContenidoCuenta = new Panel();
			base.SuspendLayout();
			this.panelCabezeraCuentas.Dock = DockStyle.Left;
			this.panelCabezeraCuentas.Location = new Point(0, 0);
			this.panelCabezeraCuentas.Name = "panelCabezeraCuentas";
			this.panelCabezeraCuentas.Size = new Size(174, 540);
			this.panelCabezeraCuentas.TabIndex = 0;
			this.panelContenidoCuenta.Dock = DockStyle.Fill;
			this.panelContenidoCuenta.Location = new Point(174, 0);
			this.panelContenidoCuenta.Name = "panelContenidoCuenta";
			this.panelContenidoCuenta.Size = new Size(734, 540);
			this.panelContenidoCuenta.TabIndex = 0;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.panelContenidoCuenta);
			base.Controls.Add(this.panelCabezeraCuentas);
			base.Name = "TabControl_Horizontal";
			base.Size = new Size(908, 540);
			base.ResumeLayout(false);
		}

		// Token: 0x0400033C RID: 828
		private int anchura_cabezera;

		// Token: 0x0400033D RID: 829
		private Dictionary<string, Pagina> paginas;

		// Token: 0x0400033E RID: 830
		private string nombre_pagina_seleccionada;

		// Token: 0x04000340 RID: 832
		private IContainer components = null;

		// Token: 0x04000341 RID: 833
		private FlowLayoutPanelBuffered panelCabezeraCuentas;

		// Token: 0x04000342 RID: 834
		private Panel panelContenidoCuenta;
	}
}
