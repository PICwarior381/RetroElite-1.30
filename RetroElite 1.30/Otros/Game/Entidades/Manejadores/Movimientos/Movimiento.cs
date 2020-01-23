using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using RetroElite.Otros.Enums;
using RetroElite.Otros.Game.Personaje;
using RetroElite.Otros.Mapas;
using RetroElite.Otros.Mapas.Movimiento;
using RetroElite.Otros.Mapas.Movimiento.Mapas;
using RetroElite.Otros.Mapas.Movimiento.Peleas;
using RetroElite.Utilidades.Criptografia;

namespace RetroElite.Otros.Game.Entidades.Manejadores.Movimientos
{
	// Token: 0x02000069 RID: 105
	public class Movimiento : IDisposable
	{
		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06000450 RID: 1104 RVA: 0x0000FB04 File Offset: 0x0000DF04
		// (remove) Token: 0x06000451 RID: 1105 RVA: 0x0000FB3C File Offset: 0x0000DF3C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<bool> movimiento_finalizado;

		// Token: 0x06000452 RID: 1106 RVA: 0x0000FB74 File Offset: 0x0000DF74
		public Movimiento(Cuenta _cuenta, Mapa _mapa, PersonajeJuego _personaje)
		{
			this.cuenta = _cuenta;
			this.personaje = _personaje;
			this.mapa = _mapa;
			this.pathfinder = new Pathfinder();
			this.mapa.mapa_actualizado += this.evento_Mapa_Actualizado;
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0000FBC4 File Offset: 0x0000DFC4
		public bool get_Puede_Cambiar_Mapa(MapaTeleportCeldas direccion, Celda celda)
		{
			Console.WriteLine("get puede cambiar Map");
			Console.WriteLine(direccion);
			bool result;
			switch (direccion)
			{
			case MapaTeleportCeldas.HAUT:
				result = (celda.y < 0 && celda.x - Math.Abs(celda.y) == 1);
				break;
			case MapaTeleportCeldas.DROITE:
				result = (celda.x - 27 == celda.y);
				break;
			case MapaTeleportCeldas.BAS:
				result = (celda.x + celda.y == 31);
				break;
			case MapaTeleportCeldas.GAUCHE:
				result = (celda.x - 1 == celda.y);
				break;
			default:
				result = true;
				break;
			}
			return result;
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0000FC6C File Offset: 0x0000E06C
		public bool get_Cambiar_Mapa(MapaTeleportCeldas direccion, Celda celda)
		{
			bool flag = this.cuenta.esta_ocupado() || this.personaje.inventario.porcentaje_pods >= 100;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = this.get_Puede_Cambiar_Mapa(direccion, celda);
				bool flag3 = !flag2;
				result = (!flag3 && this.get_Mover_Para_Cambiar_mapa(celda));
			}
			return result;
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0000FCCC File Offset: 0x0000E0CC
		public bool get_Cambiar_Mapa(MapaTeleportCeldas direccion)
		{
			bool flag = this.cuenta.esta_ocupado();
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				List<Celda> list = (from celda in this.cuenta.juego.mapa.celdas
				where celda.tipo == TipoCelda.CELDA_TELEPORT
				select celda).ToList<Celda>();
				Console.WriteLine("celdas_teleport.count");
				Console.WriteLine(list.Count);
				Console.WriteLine(direccion.ToString());
				while (list.Count > 0)
				{
					Celda celda2 = list[Randomize.get_Random(0, list.Count)];
					bool flag2 = this.get_Cambiar_Mapa(direccion, celda2);
					if (flag2)
					{
						return true;
					}
					list.Remove(celda2);
				}
				this.cuenta.logger.log_Peligro("MOUVEMENTS", "Aucune cellule cible n'a été trouvée, utilisez une autre méthode (gauche,droite,bas,haut) ou cellID");
				result = true;
			}
			return result;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0000FDE0 File Offset: 0x0000E1E0
		public ResultadoMovimientos get_Mover_A_Celda(Celda celda_destino, List<Celda> celdas_no_permitidas, bool detener_delante = false, byte distancia_detener = 0)
		{
			Console.WriteLine("get_Mover_A_Celda");
			bool flag = celda_destino.id < 0 || (int)celda_destino.id > this.mapa.celdas.Length;
			ResultadoMovimientos result;
			if (flag)
			{
				result = ResultadoMovimientos.FALLO;
			}
			else
			{
				bool flag2 = this.cuenta.esta_ocupado() || this.actual_path != null || this.personaje.inventario.porcentaje_pods >= 100;
				if (flag2)
				{
					result = ResultadoMovimientos.FALLO;
				}
				else
				{
					bool flag3 = celda_destino.id == this.personaje.celda.id;
					if (flag3)
					{
						result = ResultadoMovimientos.MISMA_CELDA;
					}
					else
					{
						bool flag4 = celda_destino.tipo == TipoCelda.NO_CAMINABLE && celda_destino.objeto_interactivo == null;
						if (flag4)
						{
							result = ResultadoMovimientos.FALLO;
						}
						else
						{
							bool flag5 = celda_destino.tipo == TipoCelda.OBJETO_INTERACTIVO && celda_destino.objeto_interactivo == null;
							if (flag5)
							{
								result = ResultadoMovimientos.FALLO;
							}
							else
							{
								List<Celda> list = this.pathfinder.get_Path(this.personaje.celda, celda_destino, celdas_no_permitidas, detener_delante, distancia_detener);
								bool flag6 = list == null || list.Count == 0;
								if (flag6)
								{
									result = ResultadoMovimientos.PATHFINDING_ERROR;
								}
								else
								{
									bool flag7 = !detener_delante && list.Last<Celda>().id != celda_destino.id;
									if (flag7)
									{
										result = ResultadoMovimientos.PATHFINDING_ERROR;
									}
									else
									{
										bool flag8 = detener_delante && list.Count == 1 && list[0].id == this.personaje.celda.id;
										if (flag8)
										{
											result = ResultadoMovimientos.MISMA_CELDA;
										}
										else
										{
											bool flag9 = detener_delante && list.Count == 2 && list[0].id == this.personaje.celda.id && list[1].id == celda_destino.id;
											if (flag9)
											{
												result = ResultadoMovimientos.MISMA_CELDA;
											}
											else
											{
												this.actual_path = list;
												this.enviar_Paquete_Movimiento();
												result = ResultadoMovimientos.EXITO;
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0000FFC0 File Offset: 0x0000E3C0
		public async Task get_Mover_Celda_Pelea(KeyValuePair<short, MovimientoNodo>? nodo)
		{
			bool flag = !this.cuenta.esta_luchando();
			if (!flag)
			{
				bool flag2 = nodo == null || nodo.Value.Value.camino.celdas_accesibles.Count == 0;
				if (!flag2)
				{
					bool flag3 = nodo.Value.Key == this.cuenta.juego.pelea.jugador_luchador.celda.id;
					if (!flag3)
					{
						nodo.Value.Value.camino.celdas_accesibles.Insert(0, this.cuenta.juego.pelea.jugador_luchador.celda.id);
						List<Celda> lista_celdas = (from c in nodo.Value.Value.camino.celdas_accesibles
						select this.mapa.get_Celda_Id(c)).ToList<Celda>();
						await this.cuenta.conexion.enviar_Paquete_Async("GA001" + PathFinderUtil.get_Pathfinding_Limpio(lista_celdas), false);
						this.personaje.evento_Personaje_Pathfinding_Minimapa(lista_celdas);
					}
				}
			}
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00010010 File Offset: 0x0000E410
		private bool get_Mover_Para_Cambiar_mapa(Celda celda)
		{
			ResultadoMovimientos resultadoMovimientos = this.get_Mover_A_Celda(celda, this.mapa.celdas_ocupadas(), false, 0);
			Console.WriteLine("resultado");
			Console.WriteLine(resultadoMovimientos);
			ResultadoMovimientos resultadoMovimientos2 = resultadoMovimientos;
			ResultadoMovimientos resultadoMovimientos3 = resultadoMovimientos2;
			bool result;
			if (resultadoMovimientos3 != ResultadoMovimientos.EXITO)
			{
				this.cuenta.logger.log_Error("MOUVEMENTS", string.Format("Erreur: essaye cette CELL_ID {0} pour changer de map", celda.id));
				result = false;
			}
			else
			{
				this.cuenta.logger.log_informacion("MOUVEMENTS", string.Format("Map actuel: {0} GO a la celllule {1} pour changer de map", this.mapa.id, celda.id));
				result = true;
			}
			return result;
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x000100C4 File Offset: 0x0000E4C4
		private void enviar_Paquete_Movimiento()
		{
			bool flag = this.cuenta.Estado_Cuenta == EstadoCuenta.REGENERANDO;
			if (flag)
			{
				this.cuenta.conexion.enviar_Paquete("eU1", true);
			}
			string str = PathFinderUtil.get_Pathfinding_Limpio(this.actual_path);
			this.cuenta.conexion.enviar_Paquete("GA001" + str, true);
			this.personaje.evento_Personaje_Pathfinding_Minimapa(this.actual_path);
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00010138 File Offset: 0x0000E538
		public async Task evento_Movimiento_Finalizado(Celda celda_destino, byte tipo_gkk, bool correcto)
		{
			this.cuenta.Estado_Cuenta = EstadoCuenta.MOVIMIENTO;
			if (correcto)
			{
				await Task.Delay(PathFinderUtil.get_Tiempo_Desplazamiento_Mapa(this.personaje.celda, this.actual_path, this.personaje.esta_utilizando_dragopavo));
				if (this.cuenta == null || this.cuenta.Estado_Cuenta == EstadoCuenta.DESCONECTADO)
				{
					return;
				}
				this.cuenta.conexion.enviar_Paquete("GKK" + tipo_gkk.ToString(), false);
				this.personaje.celda = celda_destino;
			}
			this.actual_path = null;
			this.cuenta.Estado_Cuenta = EstadoCuenta.CONECTADO_INACTIVO;
			Action<bool> action = this.movimiento_finalizado;
			if (action != null)
			{
				action(correcto);
			}
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00010194 File Offset: 0x0000E594
		private void evento_Mapa_Actualizado()
		{
			this.pathfinder.set_Mapa(this.cuenta.juego.mapa);
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x000101B2 File Offset: 0x0000E5B2
		public void movimiento_Actualizado(bool estado)
		{
			Action<bool> action = this.movimiento_finalizado;
			if (action != null)
			{
				action(estado);
			}
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x000101C8 File Offset: 0x0000E5C8
		~Movimiento()
		{
			this.Dispose(false);
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x000101F8 File Offset: 0x0000E5F8
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00010202 File Offset: 0x0000E602
		public void limpiar()
		{
			this.actual_path = null;
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0001020C File Offset: 0x0000E60C
		protected virtual void Dispose(bool disposing)
		{
			bool flag = !this.disposed;
			if (flag)
			{
				if (disposing)
				{
					this.pathfinder.Dispose();
				}
				List<Celda> list = this.actual_path;
				if (list != null)
				{
					list.Clear();
				}
				this.actual_path = null;
				this.pathfinder = null;
				this.cuenta = null;
				this.personaje = null;
				this.disposed = true;
			}
		}

		// Token: 0x040001EE RID: 494
		private Cuenta cuenta;

		// Token: 0x040001EF RID: 495
		private PersonajeJuego personaje;

		// Token: 0x040001F0 RID: 496
		private Mapa mapa;

		// Token: 0x040001F1 RID: 497
		private Pathfinder pathfinder;

		// Token: 0x040001F2 RID: 498
		public List<Celda> actual_path;

		// Token: 0x040001F4 RID: 500
		private bool disposed;
	}
}
