using System;

namespace RetroElite.Otros.Game.Personaje
{
	// Token: 0x02000054 RID: 84
	public class PersonajeStats : IEliminable
	{
		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000366 RID: 870 RVA: 0x0000D2E3 File Offset: 0x0000B6E3
		// (set) Token: 0x06000367 RID: 871 RVA: 0x0000D2EB File Offset: 0x0000B6EB
		public int base_personaje { get; set; } = 0;

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000368 RID: 872 RVA: 0x0000D2F4 File Offset: 0x0000B6F4
		// (set) Token: 0x06000369 RID: 873 RVA: 0x0000D2FC File Offset: 0x0000B6FC
		public int equipamiento { get; set; } = 0;

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600036A RID: 874 RVA: 0x0000D305 File Offset: 0x0000B705
		// (set) Token: 0x0600036B RID: 875 RVA: 0x0000D30D File Offset: 0x0000B70D
		public int dones { get; set; } = 0;

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600036C RID: 876 RVA: 0x0000D316 File Offset: 0x0000B716
		// (set) Token: 0x0600036D RID: 877 RVA: 0x0000D31E File Offset: 0x0000B71E
		public int boost { get; set; } = 0;

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600036E RID: 878 RVA: 0x0000D327 File Offset: 0x0000B727
		public int total_stats
		{
			get
			{
				return this.base_personaje + this.equipamiento + this.dones + this.boost;
			}
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000D344 File Offset: 0x0000B744
		public PersonajeStats(int _base_personaje)
		{
			this.base_personaje = _base_personaje;
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000D371 File Offset: 0x0000B771
		public PersonajeStats(int _base_personaje, int _equipamiento, int _dones, int _boost)
		{
			this.actualizar_Stats(_base_personaje, _equipamiento, _dones, _boost);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000D3A2 File Offset: 0x0000B7A2
		public void actualizar_Stats(int _base_personaje, int _equipamiento, int _dones, int _boost)
		{
			this.base_personaje = _base_personaje;
			this.equipamiento = _equipamiento;
			this.dones = _dones;
			this.boost = _boost;
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000D3C6 File Offset: 0x0000B7C6
		public void limpiar()
		{
			this.base_personaje = 0;
			this.equipamiento = 0;
			this.dones = 0;
			this.boost = 0;
		}
	}
}
