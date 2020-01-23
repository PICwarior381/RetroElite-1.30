using System;

namespace RetroElite.Otros.Mapas.Movimiento.Peleas
{
	// Token: 0x02000045 RID: 69
	public class MovimientoNodo
	{
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600028C RID: 652 RVA: 0x0000AF8F File Offset: 0x0000938F
		// (set) Token: 0x0600028D RID: 653 RVA: 0x0000AF97 File Offset: 0x00009397
		public short celda_inicial { get; private set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600028E RID: 654 RVA: 0x0000AFA0 File Offset: 0x000093A0
		// (set) Token: 0x0600028F RID: 655 RVA: 0x0000AFA8 File Offset: 0x000093A8
		public bool alcanzable { get; private set; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000290 RID: 656 RVA: 0x0000AFB1 File Offset: 0x000093B1
		// (set) Token: 0x06000291 RID: 657 RVA: 0x0000AFB9 File Offset: 0x000093B9
		public PeleaCamino camino { get; set; }

		// Token: 0x06000292 RID: 658 RVA: 0x0000AFC2 File Offset: 0x000093C2
		public MovimientoNodo(short _celda_inicial, bool _alcanzable)
		{
			this.celda_inicial = _celda_inicial;
			this.alcanzable = _alcanzable;
		}
	}
}
