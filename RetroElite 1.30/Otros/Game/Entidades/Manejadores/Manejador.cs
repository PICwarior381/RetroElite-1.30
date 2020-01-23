using System;
using RetroElite.Otros.Game.Entidades.Manejadores.Movimientos;
using RetroElite.Otros.Game.Entidades.Manejadores.Recolecciones;
using RetroElite.Otros.Game.Personaje;
using RetroElite.Otros.Mapas;

namespace RetroElite.Otros.Game.Entidades.Manejadores
{
	// Token: 0x02000064 RID: 100
	public class Manejador : IEliminable, IDisposable
	{
		// Token: 0x17000155 RID: 341
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x0000F19D File Offset: 0x0000D59D
		// (set) Token: 0x06000430 RID: 1072 RVA: 0x0000F1A5 File Offset: 0x0000D5A5
		public Movimiento movimientos { get; private set; }

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x0000F1AE File Offset: 0x0000D5AE
		// (set) Token: 0x06000432 RID: 1074 RVA: 0x0000F1B6 File Offset: 0x0000D5B6
		public Recoleccion recoleccion { get; private set; }

		// Token: 0x06000433 RID: 1075 RVA: 0x0000F1BF File Offset: 0x0000D5BF
		public Manejador(Cuenta cuenta, Mapa mapa, PersonajeJuego personaje)
		{
			this.movimientos = new Movimiento(cuenta, mapa, personaje);
			this.recoleccion = new Recoleccion(cuenta, this.movimientos, mapa);
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0000F1EC File Offset: 0x0000D5EC
		public void limpiar()
		{
			this.movimientos.limpiar();
			this.recoleccion.limpiar();
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0000F208 File Offset: 0x0000D608
		~Manejador()
		{
			this.Dispose(false);
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0000F238 File Offset: 0x0000D638
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0000F244 File Offset: 0x0000D644
		protected virtual void Dispose(bool disposing)
		{
			bool flag = this.disposed;
			if (!flag)
			{
				if (disposing)
				{
					this.movimientos.Dispose();
				}
				this.movimientos = null;
				this.disposed = true;
			}
		}

		// Token: 0x040001D7 RID: 471
		private bool disposed;
	}
}
