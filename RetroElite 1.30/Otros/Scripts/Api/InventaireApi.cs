using System;
using MoonSharp.Interpreter;
using RetroElite.Otros.Game.Personaje.Inventario;
using RetroElite.Otros.Game.Personaje.Inventario.Enums;
using RetroElite.Otros.Scripts.Acciones.Inventario;
using RetroElite.Otros.Scripts.Manejadores;

namespace RetroElite.Otros.Scripts.Api
{
	// Token: 0x0200001D RID: 29
	[MoonSharpUserData]
	public class InventaireApi : IDisposable
	{
		// Token: 0x0600012D RID: 301 RVA: 0x00006233 File Offset: 0x00004633
		public InventaireApi(Cuenta _cuenta, ManejadorAcciones _manejar_acciones)
		{
			this.cuenta = _cuenta;
			this.manejar_acciones = _manejar_acciones;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00006252 File Offset: 0x00004652
		public int pods()
		{
			return (int)this.cuenta.juego.personaje.inventario.pods_actuales;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000626E File Offset: 0x0000466E
		public int podsMax()
		{
			return (int)this.cuenta.juego.personaje.inventario.pods_maximos;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0000628A File Offset: 0x0000468A
		public int podsPourcentage()
		{
			return this.cuenta.juego.personaje.inventario.porcentaje_pods;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000062A6 File Offset: 0x000046A6
		public bool possedeEquipement(int modelo_id)
		{
			return this.cuenta.juego.personaje.inventario.get_Objeto_Modelo_Id(modelo_id) != null;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000062C8 File Offset: 0x000046C8
		public bool utiliser(int modelo_id)
		{
			ObjetosInventario objetosInventario = this.cuenta.juego.personaje.inventario.get_Objeto_Modelo_Id(modelo_id);
			bool flag = objetosInventario == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				this.manejar_acciones.enqueue_Accion(new UtilizarObjetoAccion(modelo_id), true);
				result = true;
			}
			return result;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00006318 File Offset: 0x00004718
		public bool equiper(int modelo_id)
		{
			ObjetosInventario objetosInventario = this.cuenta.juego.personaje.inventario.get_Objeto_Modelo_Id(modelo_id);
			bool flag = objetosInventario == null || objetosInventario.posicion != InventarioPosiciones.PAS_EQUIPE;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				this.manejar_acciones.enqueue_Accion(new EquiparItemAccion(modelo_id), true);
				result = true;
			}
			return result;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00006374 File Offset: 0x00004774
		~InventaireApi()
		{
			this.Dispose(false);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x000063A4 File Offset: 0x000047A4
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000063B0 File Offset: 0x000047B0
		protected virtual void Dispose(bool disposing)
		{
			bool flag = !this.disposed;
			if (flag)
			{
				this.cuenta = null;
				this.manejar_acciones = null;
				this.disposed = true;
			}
		}

		// Token: 0x04000069 RID: 105
		private Cuenta cuenta;

		// Token: 0x0400006A RID: 106
		private ManejadorAcciones manejar_acciones;

		// Token: 0x0400006B RID: 107
		private bool disposed = false;
	}
}
