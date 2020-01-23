using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RetroElite.Otros.Game.Personaje.Hechizos;
using RetroElite.Otros.Mapas;
using RetroElite.Otros.Mapas.Movimiento.Peleas;
using RetroElite.Otros.Peleas.Configuracion;
using RetroElite.Otros.Peleas.Enums;
using RetroElite.Otros.Peleas.Peleadores;

namespace RetroElite.Otros.Peleas
{
	// Token: 0x02000032 RID: 50
	public class ManejadorHechizos : IDisposable
	{
		// Token: 0x0600019C RID: 412 RVA: 0x00007472 File Offset: 0x00005872
		public ManejadorHechizos(Cuenta _cuenta)
		{
			this.cuenta = _cuenta;
			this.mapa = this.cuenta.juego.mapa;
			this.pelea = this.cuenta.juego.pelea;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x000074B0 File Offset: 0x000058B0
		public async Task<ResultadoLanzandoHechizo> manejador_Hechizos(HechizoPelea hechizo)
		{
			bool flag = hechizo.focus == HechizoFocus.CELL_VIDE;
			ResultadoLanzandoHechizo result;
			if (flag)
			{
				ResultadoLanzandoHechizo resultadoLanzandoHechizo = await this.lanzar_Hechizo_Celda_Vacia(hechizo);
				result = resultadoLanzandoHechizo;
			}
			else if (hechizo.metodo_lanzamiento == MetodoLanzamiento.CAC_ET_DISTANCE)
			{
				ResultadoLanzandoHechizo resultadoLanzandoHechizo2 = await this.get_Lanzar_Hechizo_Simple(hechizo);
				result = resultadoLanzandoHechizo2;
			}
			else if (hechizo.metodo_lanzamiento == MetodoLanzamiento.DISTANCE && !this.cuenta.juego.pelea.esta_Cuerpo_A_Cuerpo_Con_Enemigo(null))
			{
				ResultadoLanzandoHechizo resultadoLanzandoHechizo3 = await this.get_Lanzar_Hechizo_Simple(hechizo);
				result = resultadoLanzandoHechizo3;
			}
			else if (hechizo.metodo_lanzamiento == MetodoLanzamiento.CAC && this.cuenta.juego.pelea.esta_Cuerpo_A_Cuerpo_Con_Enemigo(null))
			{
				ResultadoLanzandoHechizo resultadoLanzandoHechizo4 = await this.get_Lanzar_Hechizo_Simple(hechizo);
				result = resultadoLanzandoHechizo4;
			}
			else if (hechizo.metodo_lanzamiento == MetodoLanzamiento.CAC && !this.cuenta.juego.pelea.esta_Cuerpo_A_Cuerpo_Con_Enemigo(null))
			{
				ResultadoLanzandoHechizo resultadoLanzandoHechizo5 = await this.get_Mover_Lanzar_hechizo_Simple(hechizo, this.get_Objetivo_Mas_Cercano(hechizo));
				result = resultadoLanzandoHechizo5;
			}
			else
			{
				result = ResultadoLanzandoHechizo.NO_LANZADO;
			}
			return result;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00007500 File Offset: 0x00005900
		private async Task<ResultadoLanzandoHechizo> get_Lanzar_Hechizo_Simple(HechizoPelea hechizo)
		{
			bool flag = this.pelea.get_Puede_Lanzar_hechizo(hechizo.id) > FallosLanzandoHechizo.NINGUNO;
			ResultadoLanzandoHechizo result;
			if (flag)
			{
				result = ResultadoLanzandoHechizo.NO_LANZADO;
			}
			else
			{
				Luchadores enemigo = this.get_Objetivo_Mas_Cercano(hechizo);
				bool flag2 = enemigo != null;
				if (flag2)
				{
					FallosLanzandoHechizo resultado = this.pelea.get_Puede_Lanzar_hechizo(hechizo.id, this.pelea.jugador_luchador.celda, enemigo.celda, this.mapa);
					bool flag3 = resultado == FallosLanzandoHechizo.NINGUNO;
					if (flag3)
					{
						this.cuenta.logger.log_Fight("COMBAT", "Lancement du sort " + hechizo.nombre + " ...");
						await this.pelea.get_Lanzar_Hechizo(hechizo.id, enemigo.celda.id);
						return ResultadoLanzandoHechizo.LANZADO;
					}
					if (resultado == FallosLanzandoHechizo.NO_ESTA_EN_RANGO)
					{
						this.cuenta.logger.log_Fight("COMBAT", "Lancement du sort " + hechizo.nombre + " ...");
						return await this.get_Mover_Lanzar_hechizo_Simple(hechizo, enemigo);
					}
				}
				else if (hechizo.focus == HechizoFocus.CELL_VIDE)
				{
					this.cuenta.logger.log_Fight("COMBAT", "Lancement du sort " + hechizo.nombre + " ...");
					return await this.lanzar_Hechizo_Celda_Vacia(hechizo);
				}
				result = ResultadoLanzandoHechizo.NO_LANZADO;
			}
			return result;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00007550 File Offset: 0x00005950
		private async Task<ResultadoLanzandoHechizo> get_Mover_Lanzar_hechizo_Simple(HechizoPelea hechizo_pelea, Luchadores enemigo)
		{
			KeyValuePair<short, MovimientoNodo>? nodo = null;
			int pm_utilizados = 99;
			foreach (KeyValuePair<short, MovimientoNodo> movimiento in PeleasPathfinder.get_Celdas_Accesibles(this.pelea, this.mapa, this.pelea.jugador_luchador.celda))
			{
				bool flag = !movimiento.Value.alcanzable;
				if (!flag)
				{
					bool flag2 = hechizo_pelea.metodo_lanzamiento == MetodoLanzamiento.CAC && !this.pelea.esta_Cuerpo_A_Cuerpo_Con_Aliado(this.mapa.get_Celda_Id(movimiento.Key));
					if (!flag2)
					{
						bool flag3 = this.pelea.get_Puede_Lanzar_hechizo(hechizo_pelea.id, this.mapa.get_Celda_Id(movimiento.Key), enemigo.celda, this.mapa) > FallosLanzandoHechizo.NINGUNO;
						if (!flag3)
						{
							bool flag4 = movimiento.Value.camino.celdas_accesibles.Count <= pm_utilizados;
							if (flag4)
							{
								nodo = new KeyValuePair<short, MovimientoNodo>?(movimiento);
								pm_utilizados = movimiento.Value.camino.celdas_accesibles.Count;
							}
							movimiento = default(KeyValuePair<short, MovimientoNodo>);
						}
					}
				}
			}
			Dictionary<short, MovimientoNodo>.Enumerator enumerator = default(Dictionary<short, MovimientoNodo>.Enumerator);
			bool flag5 = nodo != null;
			ResultadoLanzandoHechizo result;
			if (flag5)
			{
				await this.cuenta.juego.manejador.movimientos.get_Mover_Celda_Pelea(nodo);
				result = ResultadoLanzandoHechizo.MOVIDO;
			}
			else
			{
				result = ResultadoLanzandoHechizo.NO_LANZADO;
			}
			return result;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x000075A8 File Offset: 0x000059A8
		private async Task<ResultadoLanzandoHechizo> lanzar_Hechizo_Celda_Vacia(HechizoPelea hechizo_pelea)
		{
			bool flag = this.pelea.get_Puede_Lanzar_hechizo(hechizo_pelea.id) > FallosLanzandoHechizo.NINGUNO;
			ResultadoLanzandoHechizo result;
			if (flag)
			{
				result = ResultadoLanzandoHechizo.NO_LANZADO;
			}
			else
			{
				bool flag2 = hechizo_pelea.focus == HechizoFocus.CELL_VIDE && this.pelea.get_Cuerpo_A_Cuerpo_Enemigo(null).Count<Luchadores>() == 4;
				if (flag2)
				{
					result = ResultadoLanzandoHechizo.NO_LANZADO;
				}
				else
				{
					Hechizo hechizo = this.cuenta.juego.personaje.get_Hechizo(hechizo_pelea.id);
					HechizoStats datos_hechizo = hechizo.get_Stats();
					List<short> rangos_disponibles = this.pelea.get_Rango_hechizo(this.pelea.jugador_luchador.celda, datos_hechizo, this.mapa);
					foreach (short rango in rangos_disponibles)
					{
						bool flag3 = this.pelea.get_Puede_Lanzar_hechizo(hechizo_pelea.id, this.pelea.jugador_luchador.celda, this.mapa.get_Celda_Id(rango), this.mapa) == FallosLanzandoHechizo.NINGUNO;
						if (flag3)
						{
							bool flag4 = hechizo_pelea.metodo_lanzamiento == MetodoLanzamiento.CAC || (hechizo_pelea.metodo_lanzamiento == MetodoLanzamiento.CAC_ET_DISTANCE && this.mapa.get_Celda_Id(rango).get_Distancia_Entre_Dos_Casillas(this.pelea.jugador_luchador.celda) != 1);
							if (!flag4)
							{
								await this.pelea.get_Lanzar_Hechizo(hechizo_pelea.id, rango);
								return ResultadoLanzandoHechizo.LANZADO;
							}
						}
					}
					List<short>.Enumerator enumerator = default(List<short>.Enumerator);
					result = ResultadoLanzandoHechizo.NO_LANZADO;
				}
			}
			return result;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x000075F8 File Offset: 0x000059F8
		private Luchadores get_Objetivo_Mas_Cercano(HechizoPelea hechizo)
		{
			bool flag = hechizo.focus == HechizoFocus.SOIS_MEME;
			Luchadores result;
			if (flag)
			{
				result = this.pelea.jugador_luchador;
			}
			else
			{
				bool flag2 = hechizo.focus == HechizoFocus.CELL_VIDE;
				if (flag2)
				{
					result = null;
				}
				else
				{
					result = ((hechizo.focus == HechizoFocus.ENNEMIE) ? this.pelea.get_Obtener_Enemigo_Mas_Cercano() : this.pelea.get_Obtener_Aliado_Mas_Cercano());
				}
			}
			return result;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00007656 File Offset: 0x00005A56
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00007660 File Offset: 0x00005A60
		~ManejadorHechizos()
		{
			this.Dispose(false);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00007690 File Offset: 0x00005A90
		public virtual void Dispose(bool disposing)
		{
			bool flag = !this.disposed;
			if (flag)
			{
				this.cuenta = null;
				this.mapa = null;
				this.pelea = null;
				this.disposed = true;
			}
		}

		// Token: 0x0400008B RID: 139
		private Cuenta cuenta;

		// Token: 0x0400008C RID: 140
		private Mapa mapa;

		// Token: 0x0400008D RID: 141
		private Pelea pelea;

		// Token: 0x0400008E RID: 142
		private bool disposed;
	}
}
