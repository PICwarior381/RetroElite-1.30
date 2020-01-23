using System;

namespace RetroElite.Otros.Mapas.Movimiento.Peleas
{
	// Token: 0x02000046 RID: 70
	public class NodoPelea
	{
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000293 RID: 659 RVA: 0x0000AFDC File Offset: 0x000093DC
		// (set) Token: 0x06000294 RID: 660 RVA: 0x0000AFE4 File Offset: 0x000093E4
		public Celda celda { get; private set; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0000AFED File Offset: 0x000093ED
		// (set) Token: 0x06000296 RID: 662 RVA: 0x0000AFF5 File Offset: 0x000093F5
		public int pm_disponible { get; private set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000297 RID: 663 RVA: 0x0000AFFE File Offset: 0x000093FE
		// (set) Token: 0x06000298 RID: 664 RVA: 0x0000B006 File Offset: 0x00009406
		public int pa_disponible { get; private set; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000299 RID: 665 RVA: 0x0000B00F File Offset: 0x0000940F
		// (set) Token: 0x0600029A RID: 666 RVA: 0x0000B017 File Offset: 0x00009417
		public int distancia { get; private set; }

		// Token: 0x0600029B RID: 667 RVA: 0x0000B020 File Offset: 0x00009420
		public NodoPelea(Celda _celda, int _pm_disponible, int _pa_disponible, int _distancia)
		{
			this.celda = _celda;
			this.pm_disponible = _pm_disponible;
			this.pa_disponible = _pa_disponible;
			this.distancia = _distancia;
		}
	}
}
