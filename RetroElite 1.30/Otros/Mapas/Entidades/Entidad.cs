using System;

namespace RetroElite.Otros.Mapas.Entidades
{
	// Token: 0x0200004E RID: 78
	public interface Entidad : IDisposable
	{
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060002D5 RID: 725
		// (set) Token: 0x060002D6 RID: 726
		int id { get; set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060002D7 RID: 727
		// (set) Token: 0x060002D8 RID: 728
		Celda celda { get; set; }
	}
}
