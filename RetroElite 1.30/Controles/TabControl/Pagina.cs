using System;
using System.Drawing;
using System.Windows.Forms;

namespace RetroElite.Controles.TabControl
{
	// Token: 0x0200007B RID: 123
	public class Pagina
	{
		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x00022DB5 File Offset: 0x000211B5
		// (set) Token: 0x0600051F RID: 1311 RVA: 0x00022DBD File Offset: 0x000211BD
		public Cabezera cabezera { get; private set; }

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x00022DC6 File Offset: 0x000211C6
		// (set) Token: 0x06000521 RID: 1313 RVA: 0x00022DCE File Offset: 0x000211CE
		public Panel contenido { get; private set; }

		// Token: 0x06000522 RID: 1314 RVA: 0x00022DD8 File Offset: 0x000211D8
		public Pagina(string nuevo_titulo, int anchura_cabezera)
		{
			this.cabezera = new Cabezera
			{
				propiedad_Cuenta = nuevo_titulo,
				Size = new Size(anchura_cabezera, 40),
				Margin = new Padding(2, 0, 2, 10)
			};
			this.contenido = new Panel
			{
				Dock = DockStyle.Fill,
				Visible = false
			};
		}
	}
}
