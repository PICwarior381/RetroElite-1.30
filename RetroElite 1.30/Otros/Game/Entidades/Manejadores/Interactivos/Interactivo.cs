using System;
using System.Diagnostics;
using RetroElite.Otros.Game.Entidades.Manejadores.Movimientos;
using RetroElite.Otros.Mapas.Interactivo;

namespace RetroElite.Otros.Game.Entidades.Manejadores.Interactivos
{
	// Token: 0x0200006C RID: 108
	public class Interactivo : IDisposable
	{
		// Token: 0x14000020 RID: 32
		// (add) Token: 0x06000462 RID: 1122 RVA: 0x00010280 File Offset: 0x0000E680
		// (remove) Token: 0x06000463 RID: 1123 RVA: 0x000102B8 File Offset: 0x0000E6B8
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<bool> fin_interactivo;

		// Token: 0x06000464 RID: 1124 RVA: 0x000102ED File Offset: 0x0000E6ED
		public Interactivo(Cuenta _cuenta, Movimiento movimiento)
		{
			this.cuenta = _cuenta;
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x000102FE File Offset: 0x0000E6FE
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x00010308 File Offset: 0x0000E708
		~Interactivo()
		{
			this.Dispose(false);
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00010338 File Offset: 0x0000E738
		protected virtual void Dispose(bool disposing)
		{
			bool flag = !this.disposed;
			if (flag)
			{
				this.interactivo_utilizado = null;
				this.cuenta = null;
				this.disposed = true;
			}
		}

		// Token: 0x04000200 RID: 512
		private Cuenta cuenta;

		// Token: 0x04000201 RID: 513
		private ObjetoInteractivo interactivo_utilizado;

		// Token: 0x04000203 RID: 515
		private bool disposed;
	}
}
