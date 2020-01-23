using System;
using MoonSharp.Interpreter;
using RetroElite.Otros.Scripts.Manejadores;

namespace RetroElite.Otros.Scripts.Api
{
	// Token: 0x0200001C RID: 28
	[MoonSharpUserData]
	public class API : IDisposable
	{
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600011F RID: 287 RVA: 0x000060B6 File Offset: 0x000044B6
		// (set) Token: 0x06000120 RID: 288 RVA: 0x000060BE File Offset: 0x000044BE
		public InventaireApi inventaire { get; private set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000121 RID: 289 RVA: 0x000060C7 File Offset: 0x000044C7
		// (set) Token: 0x06000122 RID: 290 RVA: 0x000060CF File Offset: 0x000044CF
		public PersonnageApi personnage { get; private set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000123 RID: 291 RVA: 0x000060D8 File Offset: 0x000044D8
		// (set) Token: 0x06000124 RID: 292 RVA: 0x000060E0 File Offset: 0x000044E0
		public MapApi map { get; private set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000125 RID: 293 RVA: 0x000060E9 File Offset: 0x000044E9
		// (set) Token: 0x06000126 RID: 294 RVA: 0x000060F1 File Offset: 0x000044F1
		public NpcAPI npc { get; private set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000127 RID: 295 RVA: 0x000060FA File Offset: 0x000044FA
		// (set) Token: 0x06000128 RID: 296 RVA: 0x00006102 File Offset: 0x00004502
		public CombatApi combat { get; private set; }

		// Token: 0x06000129 RID: 297 RVA: 0x0000610C File Offset: 0x0000450C
		public API(Cuenta cuenta, ManejadorAcciones manejar_acciones)
		{
			this.inventaire = new InventaireApi(cuenta, manejar_acciones);
			this.personnage = new PersonnageApi(cuenta);
			this.map = new MapApi(cuenta, manejar_acciones);
			this.npc = new NpcAPI(cuenta, manejar_acciones);
			this.combat = new CombatApi(cuenta, manejar_acciones);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00006168 File Offset: 0x00004568
		~API()
		{
			this.Dispose(false);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00006198 File Offset: 0x00004598
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000061A4 File Offset: 0x000045A4
		protected virtual void Dispose(bool disposing)
		{
			bool flag = !this.disposed;
			if (flag)
			{
				if (disposing)
				{
					this.inventaire.Dispose();
					this.personnage.Dispose();
					this.map.Dispose();
					this.npc.Dispose();
					this.combat.Dispose();
				}
				this.inventaire = null;
				this.personnage = null;
				this.map = null;
				this.npc = null;
				this.combat = null;
				this.disposed = true;
			}
		}

		// Token: 0x04000068 RID: 104
		private bool disposed;
	}
}
