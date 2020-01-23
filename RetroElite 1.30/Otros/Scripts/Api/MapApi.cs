using System;
using MoonSharp.Interpreter;
using RetroElite.Otros.Scripts.Acciones;
using RetroElite.Otros.Scripts.Acciones.Mapas;
using RetroElite.Otros.Scripts.Manejadores;

namespace RetroElite.Otros.Scripts.Api
{
	// Token: 0x0200001E RID: 30
	[MoonSharpUserData]
	public class MapApi : IDisposable
	{
		// Token: 0x06000137 RID: 311 RVA: 0x000063E2 File Offset: 0x000047E2
		public MapApi(Cuenta _cuenta, ManejadorAcciones _manejador_acciones)
		{
			this.cuenta = _cuenta;
			this.manejador_acciones = _manejador_acciones;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00006404 File Offset: 0x00004804
		public bool changerMap(string posicion)
		{
			bool flag = this.cuenta.esta_ocupado();
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				CambiarMapaAccion accion;
				bool flag2 = !CambiarMapaAccion.TryParse(posicion, out accion);
				if (flag2)
				{
					this.cuenta.logger.log_Error("MapApi", "Erreur changement de map " + posicion);
					result = false;
				}
				else
				{
					this.manejador_acciones.enqueue_Accion(accion, true);
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00006470 File Offset: 0x00004870
		public bool bougerALaCellule(short celda_id)
		{
			bool flag = celda_id < 0 || (int)celda_id > this.cuenta.juego.mapa.celdas.Length;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = !this.cuenta.juego.mapa.get_Celda_Id(celda_id).es_Caminable() || this.cuenta.juego.mapa.get_Celda_Id(celda_id).es_linea_vision;
				if (flag2)
				{
					result = false;
				}
				else
				{
					this.manejador_acciones.enqueue_Accion(new MoverCeldaAccion(celda_id), true);
					result = true;
				}
			}
			return result;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00006502 File Offset: 0x00004902
		public bool surLaCell(short celda_id)
		{
			return this.cuenta.juego.personaje.celda.id == celda_id;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00006521 File Offset: 0x00004921
		public bool surLaMap(string coordenadas)
		{
			return this.cuenta.juego.mapa.esta_En_Mapa(coordenadas);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000653C File Offset: 0x0000493C
		public string ouJeSuis()
		{
			return this.cuenta.juego.mapa.id.ToString();
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00006566 File Offset: 0x00004966
		public string maPos()
		{
			return this.cuenta.juego.mapa.coordenadas;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00006580 File Offset: 0x00004980
		~MapApi()
		{
			this.Dispose(false);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x000065B0 File Offset: 0x000049B0
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x000065BC File Offset: 0x000049BC
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

		// Token: 0x0400006C RID: 108
		private Cuenta cuenta;

		// Token: 0x0400006D RID: 109
		private ManejadorAcciones manejador_acciones;

		// Token: 0x0400006E RID: 110
		private bool disposed = false;
	}
}
