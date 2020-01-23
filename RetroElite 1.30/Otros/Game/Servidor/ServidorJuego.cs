using System;
using RetroElite.Otros.Enums;

namespace RetroElite.Otros.Game.Servidor
{
	// Token: 0x0200006D RID: 109
	public class ServidorJuego : IEliminable, IDisposable
	{
		// Token: 0x06000468 RID: 1128 RVA: 0x0001036A File Offset: 0x0000E76A
		public ServidorJuego()
		{
			this.actualizar_Datos(0, "UNDEFINED", EstadosServidor.OFF);
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00010388 File Offset: 0x0000E788
		public void actualizar_Datos(int _id, string _nombre, EstadosServidor _estado)
		{
			this.id = _id;
			this.nombre = _nombre;
			this.estado = _estado;
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x000103A0 File Offset: 0x0000E7A0
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x000103AC File Offset: 0x0000E7AC
		~ServidorJuego()
		{
			this.Dispose(false);
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x000103DC File Offset: 0x0000E7DC
		public void limpiar()
		{
			this.id = 0;
			this.nombre = null;
			this.estado = EstadosServidor.OFF;
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x000103F4 File Offset: 0x0000E7F4
		protected virtual void Dispose(bool disposing)
		{
			bool flag = this.disposed;
			if (!flag)
			{
				this.id = 0;
				this.nombre = null;
				this.estado = EstadosServidor.OFF;
				this.disposed = true;
			}
		}

		// Token: 0x04000204 RID: 516
		public int id;

		// Token: 0x04000205 RID: 517
		public string nombre;

		// Token: 0x04000206 RID: 518
		public EstadosServidor estado;

		// Token: 0x04000207 RID: 519
		private bool disposed = false;
	}
}
