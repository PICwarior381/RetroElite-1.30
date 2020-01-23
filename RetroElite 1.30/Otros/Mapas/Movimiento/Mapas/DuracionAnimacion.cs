using System;

namespace RetroElite.Otros.Mapas.Movimiento.Mapas
{
	// Token: 0x02000049 RID: 73
	internal class DuracionAnimacion
	{
		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000B59A File Offset: 0x0000999A
		// (set) Token: 0x060002AA RID: 682 RVA: 0x0000B5A2 File Offset: 0x000099A2
		public int lineal { get; private set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0000B5AB File Offset: 0x000099AB
		// (set) Token: 0x060002AC RID: 684 RVA: 0x0000B5B3 File Offset: 0x000099B3
		public int horizontal { get; private set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000B5BC File Offset: 0x000099BC
		// (set) Token: 0x060002AE RID: 686 RVA: 0x0000B5C4 File Offset: 0x000099C4
		public int vertical { get; private set; }

		// Token: 0x060002AF RID: 687 RVA: 0x0000B5CD File Offset: 0x000099CD
		public DuracionAnimacion(int _lineal, int _horizontal, int _vertical)
		{
			this.lineal = _lineal;
			this.horizontal = _horizontal;
			this.vertical = _vertical;
		}
	}
}
