using System;
using System.Collections.Generic;

namespace RetroElite.Otros.Grupos
{
	// Token: 0x0200003F RID: 63
	public class Agrupamiento : IDisposable
	{
		// Token: 0x0600022C RID: 556 RVA: 0x00009C27 File Offset: 0x00008027
		public Agrupamiento(Grupo _grupo)
		{
			this.grupo = _grupo;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00009C37 File Offset: 0x00008037
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00009C44 File Offset: 0x00008044
		~Agrupamiento()
		{
			this.Dispose(false);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00009C74 File Offset: 0x00008074
		protected virtual void Dispose(bool disposing)
		{
			bool flag = !this.disposed;
			if (flag)
			{
				this.grupo = null;
				List<Cuenta> list = this.miembros_perdidos;
				if (list != null)
				{
					list.Clear();
				}
				this.miembros_perdidos = null;
				this.disposed = true;
			}
		}

		// Token: 0x040000E3 RID: 227
		private Grupo grupo;

		// Token: 0x040000E4 RID: 228
		private List<Cuenta> miembros_perdidos;

		// Token: 0x040000E5 RID: 229
		private bool disposed;
	}
}
