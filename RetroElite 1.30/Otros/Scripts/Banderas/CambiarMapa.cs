using System;

namespace RetroElite.Otros.Scripts.Banderas
{
	// Token: 0x02000017 RID: 23
	public class CambiarMapa : Bandera
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00006072 File Offset: 0x00004472
		// (set) Token: 0x06000117 RID: 279 RVA: 0x0000607A File Offset: 0x0000447A
		public string celda_id { get; private set; }

		// Token: 0x06000118 RID: 280 RVA: 0x00006083 File Offset: 0x00004483
		public CambiarMapa(string _celda_id)
		{
			this.celda_id = _celda_id;
		}
	}
}
