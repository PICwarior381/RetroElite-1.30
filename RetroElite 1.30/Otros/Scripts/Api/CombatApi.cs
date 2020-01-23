using System;
using System.Collections.Generic;
using MoonSharp.Interpreter;
using RetroElite.Otros.Scripts.Acciones;
using RetroElite.Otros.Scripts.Manejadores;

namespace RetroElite.Otros.Scripts.Api
{
	// Token: 0x02000020 RID: 32
	[MoonSharpUserData]
	public class CombatApi : IDisposable
	{
		// Token: 0x06000149 RID: 329 RVA: 0x0000678E File Offset: 0x00004B8E
		public CombatApi(Cuenta _cuenta, ManejadorAcciones _manejador_acciones)
		{
			this.cuenta = _cuenta;
			this.manejador_acciones = _manejador_acciones;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x000067AD File Offset: 0x00004BAD
		public bool peutCombattre(int monstruos_minimos = 1, int monstruos_maximos = 8, int nivel_minimo = 1, int nivel_maximo = 1000, List<int> monstruos_prohibidos = null, List<int> monstruos_obligatorios = null)
		{
			return this.cuenta.juego.mapa.get_Puede_Luchar_Contra_Grupo_Monstruos(monstruos_minimos, monstruos_maximos, nivel_minimo, nivel_maximo, monstruos_prohibidos, monstruos_obligatorios);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x000067D0 File Offset: 0x00004BD0
		public bool combattre(int monstruos_minimos = 1, int monstruos_maximos = 8, int nivel_minimo = 1, int nivel_maximo = 1000, List<int> monstruos_prohibidos = null, List<int> monstruos_obligatorios = null)
		{
			bool flag = this.peutCombattre(monstruos_minimos, monstruos_maximos, nivel_minimo, nivel_maximo, monstruos_prohibidos, monstruos_obligatorios);
			bool result;
			if (flag)
			{
				this.manejador_acciones.enqueue_Accion(new PeleasAccion(monstruos_minimos, monstruos_maximos, nivel_minimo, nivel_maximo, monstruos_prohibidos, monstruos_obligatorios), true);
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00006818 File Offset: 0x00004C18
		~CombatApi()
		{
			this.Dispose(false);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00006848 File Offset: 0x00004C48
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00006854 File Offset: 0x00004C54
		protected virtual void Dispose(bool disposing)
		{
			bool flag = !this.disposed;
			if (flag)
			{
				this.cuenta = null;
				this.manejador_acciones = null;
				this.disposed = true;
			}
		}

		// Token: 0x04000072 RID: 114
		private Cuenta cuenta;

		// Token: 0x04000073 RID: 115
		private ManejadorAcciones manejador_acciones;

		// Token: 0x04000074 RID: 116
		private bool disposed = false;
	}
}
