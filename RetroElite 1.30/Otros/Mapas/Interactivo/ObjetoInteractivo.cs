using System;

namespace RetroElite.Otros.Mapas.Interactivo
{
	// Token: 0x0200004C RID: 76
	public class ObjetoInteractivo
	{
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060002BD RID: 701 RVA: 0x0000BB6C File Offset: 0x00009F6C
		// (set) Token: 0x060002BE RID: 702 RVA: 0x0000BB74 File Offset: 0x00009F74
		public short gfx { get; private set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0000BB7D File Offset: 0x00009F7D
		// (set) Token: 0x060002C0 RID: 704 RVA: 0x0000BB85 File Offset: 0x00009F85
		public Celda celda { get; private set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x0000BB8E File Offset: 0x00009F8E
		// (set) Token: 0x060002C2 RID: 706 RVA: 0x0000BB96 File Offset: 0x00009F96
		public ObjetoInteractivoModelo modelo { get; private set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000BB9F File Offset: 0x00009F9F
		// (set) Token: 0x060002C4 RID: 708 RVA: 0x0000BBA7 File Offset: 0x00009FA7
		public bool es_utilizable { get; set; } = false;

		// Token: 0x060002C5 RID: 709 RVA: 0x0000BBB0 File Offset: 0x00009FB0
		public ObjetoInteractivo(short _gfx, Celda _celda)
		{
			this.gfx = _gfx;
			this.celda = _celda;
			ObjetoInteractivoModelo objetoInteractivoModelo = ObjetoInteractivoModelo.get_Modelo_Por_Gfx(this.gfx);
			bool flag = objetoInteractivoModelo != null;
			if (flag)
			{
				this.modelo = objetoInteractivoModelo;
				this.es_utilizable = true;
			}
		}
	}
}
