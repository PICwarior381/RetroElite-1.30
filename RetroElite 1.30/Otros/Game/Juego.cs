using System;
using RetroElite.Otros.Game.Entidades.Manejadores;
using RetroElite.Otros.Game.Personaje;
using RetroElite.Otros.Game.Servidor;
using RetroElite.Otros.Mapas;
using RetroElite.Otros.Peleas;

namespace RetroElite.Otros.Game
{
	// Token: 0x02000052 RID: 82
	public class Juego : IEliminable, IDisposable
	{
		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000306 RID: 774 RVA: 0x0000C234 File Offset: 0x0000A634
		// (set) Token: 0x06000307 RID: 775 RVA: 0x0000C23C File Offset: 0x0000A63C
		public ServidorJuego servidor { get; private set; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000308 RID: 776 RVA: 0x0000C245 File Offset: 0x0000A645
		// (set) Token: 0x06000309 RID: 777 RVA: 0x0000C24D File Offset: 0x0000A64D
		public Mapa mapa { get; private set; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600030A RID: 778 RVA: 0x0000C256 File Offset: 0x0000A656
		// (set) Token: 0x0600030B RID: 779 RVA: 0x0000C25E File Offset: 0x0000A65E
		public PersonajeJuego personaje { get; private set; }

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600030C RID: 780 RVA: 0x0000C267 File Offset: 0x0000A667
		// (set) Token: 0x0600030D RID: 781 RVA: 0x0000C26F File Offset: 0x0000A66F
		public Manejador manejador { get; private set; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600030E RID: 782 RVA: 0x0000C278 File Offset: 0x0000A678
		// (set) Token: 0x0600030F RID: 783 RVA: 0x0000C280 File Offset: 0x0000A680
		public Pelea pelea { get; private set; }

		// Token: 0x06000310 RID: 784 RVA: 0x0000C28C File Offset: 0x0000A68C
		internal Juego(Cuenta cuenta)
		{
			this.servidor = new ServidorJuego();
			this.mapa = new Mapa();
			this.personaje = new PersonajeJuego(cuenta);
			this.manejador = new Manejador(cuenta, this.mapa, this.personaje);
			this.pelea = new Pelea(cuenta);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000C2F4 File Offset: 0x0000A6F4
		~Juego()
		{
			this.Dispose(false);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000C324 File Offset: 0x0000A724
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000C32E File Offset: 0x0000A72E
		public void limpiar()
		{
			this.mapa.limpiar();
			this.manejador.limpiar();
			this.pelea.limpiar();
			this.personaje.limpiar();
			this.servidor.limpiar();
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000C370 File Offset: 0x0000A770
		protected virtual void Dispose(bool disposing)
		{
			bool flag = !this.disposed;
			if (flag)
			{
				if (disposing)
				{
					this.mapa.Dispose();
					this.personaje.Dispose();
					this.manejador.Dispose();
					this.pelea.Dispose();
					this.servidor.Dispose();
				}
				this.servidor = null;
				this.mapa = null;
				this.personaje = null;
				this.manejador = null;
				this.pelea = null;
				this.disposed = true;
			}
		}

		// Token: 0x04000147 RID: 327
		private bool disposed = false;
	}
}
