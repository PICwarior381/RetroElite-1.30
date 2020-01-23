using System;

namespace RetroElite.Otros.Game.Personaje.Hechizos
{
	// Token: 0x0200005D RID: 93
	public class HechizoEfecto
	{
		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x0000EB18 File Offset: 0x0000CF18
		// (set) Token: 0x060003F9 RID: 1017 RVA: 0x0000EB20 File Offset: 0x0000CF20
		public int id { get; set; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x0000EB29 File Offset: 0x0000CF29
		// (set) Token: 0x060003FB RID: 1019 RVA: 0x0000EB31 File Offset: 0x0000CF31
		public Zonas zona_efecto { get; set; }

		// Token: 0x060003FC RID: 1020 RVA: 0x0000EB3A File Offset: 0x0000CF3A
		public HechizoEfecto(int _id, Zonas zona)
		{
			this.id = _id;
			this.zona_efecto = zona;
		}
	}
}
