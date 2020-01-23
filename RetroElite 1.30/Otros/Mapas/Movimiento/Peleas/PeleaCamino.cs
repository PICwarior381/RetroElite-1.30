using System;
using System.Collections.Generic;

namespace RetroElite.Otros.Mapas.Movimiento.Peleas
{
	// Token: 0x02000047 RID: 71
	public class PeleaCamino
	{
		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0000B04B File Offset: 0x0000944B
		// (set) Token: 0x0600029D RID: 669 RVA: 0x0000B053 File Offset: 0x00009453
		public List<short> celdas_accesibles { get; set; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600029E RID: 670 RVA: 0x0000B05C File Offset: 0x0000945C
		// (set) Token: 0x0600029F RID: 671 RVA: 0x0000B064 File Offset: 0x00009464
		public List<short> celdas_inalcanzables { get; set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x0000B06D File Offset: 0x0000946D
		// (set) Token: 0x060002A1 RID: 673 RVA: 0x0000B075 File Offset: 0x00009475
		public Dictionary<short, int> mapa_celdas_alcanzables { get; set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000B07E File Offset: 0x0000947E
		// (set) Token: 0x060002A3 RID: 675 RVA: 0x0000B086 File Offset: 0x00009486
		public Dictionary<short, int> mapa_celdas_inalcanzable { get; set; }
	}
}
