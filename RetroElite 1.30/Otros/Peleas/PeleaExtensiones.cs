using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RetroElite.Otros.Mapas;
using RetroElite.Otros.Mapas.Movimiento.Peleas;
using RetroElite.Otros.Peleas.Configuracion;
using RetroElite.Otros.Peleas.Enums;
using RetroElite.Otros.Peleas.Peleadores;

namespace RetroElite.Otros.Peleas
{
	// Token: 0x02000034 RID: 52
	public class PeleaExtensiones : IDisposable
	{
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001DB RID: 475 RVA: 0x000091C5 File Offset: 0x000075C5
		// (set) Token: 0x060001DC RID: 476 RVA: 0x000091CD File Offset: 0x000075CD
		public PeleaConf configuracion { get; set; }

		// Token: 0x060001DD RID: 477 RVA: 0x000091D8 File Offset: 0x000075D8
		public PeleaExtensiones(Cuenta _cuenta)
		{
			this.cuenta = _cuenta;
			this.configuracion = new PeleaConf(this.cuenta);
			this.manejador_hechizos = new ManejadorHechizos(this.cuenta);
			this.pelea = this.cuenta.juego.pelea;
			this.get_Eventos();
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00009234 File Offset: 0x00007634
		private void get_Eventos()
		{
			this.pelea.pelea_creada += this.get_Pelea_Creada;
			this.pelea.turno_iniciado += this.get_Pelea_Turno_iniciado;
			this.pelea.hechizo_lanzado += this.get_Procesar_Hechizo_Lanzado;
			this.pelea.movimiento += this.get_Procesar_Movimiento;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x000092A4 File Offset: 0x000076A4
		private void get_Pelea_Creada()
		{
			foreach (HechizoPelea hechizoPelea in this.configuracion.hechizos)
			{
				hechizoPelea.lanzamientos_restantes = hechizoPelea.lanzamientos_x_turno;
			}
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00009308 File Offset: 0x00007708
		private async void get_Pelea_Turno_iniciado()
		{
			this.hechizo_lanzado_index = 0;
			this.esperando_sequencia_fin = true;
			this.cuenta.logger.log_Fight("COMBAT", "Debut de votre tour de jeu");
			await Task.Delay(100);
			if (this.configuracion.hechizos.Count == 0 || !this.cuenta.juego.pelea.get_Enemigos.Any<Luchadores>() || this.cuenta.juego.personaje.caracteristicas.puntos_accion.total_stats <= 0)
			{
				await this.get_Fin_Turno();
			}
			else
			{
				await this.get_Procesar_hechizo();
			}
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00009344 File Offset: 0x00007744
		private async Task get_Procesar_hechizo()
		{
			Cuenta cuenta = this.cuenta;
			bool flag = (cuenta != null && !cuenta.esta_luchando()) || this.configuracion == null;
			if (!flag)
			{
				bool flag2 = this.hechizo_lanzado_index >= this.configuracion.hechizos.Count;
				if (flag2)
				{
					await this.get_Fin_Turno();
				}
				else
				{
					HechizoPelea hechizo_actual = this.configuracion.hechizos[this.hechizo_lanzado_index];
					if (hechizo_actual.lanzamientos_restantes == 0)
					{
						await this.get_Procesar_Siguiente_Hechizo(hechizo_actual);
					}
					else
					{
						switch (await this.manejador_hechizos.manejador_Hechizos(hechizo_actual))
						{
						case ResultadoLanzandoHechizo.LANZADO:
						{
							HechizoPelea hechizoPelea = hechizo_actual;
							hechizoPelea.lanzamientos_restantes -= 1;
							this.esperando_sequencia_fin = true;
							break;
						}
						case ResultadoLanzandoHechizo.MOVIDO:
							this.esperando_sequencia_fin = true;
							break;
						case ResultadoLanzandoHechizo.NO_LANZADO:
							await this.get_Procesar_Siguiente_Hechizo(hechizo_actual);
							break;
						}
					}
				}
			}
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000938C File Offset: 0x0000778C
		public async void get_Procesar_Hechizo_Lanzado(short celda_id, bool exito)
		{
			bool flag = this.pelea.total_enemigos_vivos == 0;
			if (!flag)
			{
				bool flag2 = !this.esperando_sequencia_fin;
				if (!flag2)
				{
					this.esperando_sequencia_fin = false;
					await Task.Delay(100);
					if (!exito)
					{
						await this.get_Procesar_Siguiente_Hechizo(this.configuracion.hechizos[this.hechizo_lanzado_index]);
					}
					else
					{
						this.pelea.actualizar_Hechizo_Exito(celda_id, this.configuracion.hechizos[this.hechizo_lanzado_index].id);
						await this.get_Procesar_hechizo();
					}
				}
			}
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x000093D8 File Offset: 0x000077D8
		public async void get_Procesar_Movimiento(bool exito)
		{
			bool flag = this.pelea.total_enemigos_vivos == 0;
			if (!flag)
			{
				bool flag2 = !this.esperando_sequencia_fin;
				if (!flag2)
				{
					this.esperando_sequencia_fin = false;
					await Task.Delay(100);
					if (!exito)
					{
						await this.get_Procesar_Siguiente_Hechizo(this.configuracion.hechizos[this.hechizo_lanzado_index]);
					}
					else
					{
						await this.get_Procesar_hechizo();
					}
				}
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000941C File Offset: 0x0000781C
		private async Task get_Procesar_Siguiente_Hechizo(HechizoPelea hechizo_actual)
		{
			Cuenta cuenta = this.cuenta;
			bool flag = cuenta != null && !cuenta.esta_luchando();
			if (!flag)
			{
				hechizo_actual.lanzamientos_restantes = hechizo_actual.lanzamientos_x_turno;
				this.hechizo_lanzado_index++;
				await Task.Delay(100);
				await this.get_Procesar_hechizo();
			}
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000946C File Offset: 0x0000786C
		private async Task get_Fin_Turno()
		{
			bool flag = !this.pelea.esta_Cuerpo_A_Cuerpo_Con_Enemigo(null) && this.configuracion.tactica == Tactica.AGRESIVA;
			if (flag)
			{
				await this.get_Mover(true, this.pelea.get_Obtener_Enemigo_Mas_Cercano());
			}
			else if (this.pelea.esta_Cuerpo_A_Cuerpo_Con_Enemigo(null) && this.configuracion.tactica == Tactica.FUGITIVA)
			{
				await this.get_Mover(false, this.pelea.get_Obtener_Enemigo_Mas_Cercano());
			}
			this.pelea.get_Turno_Acabado();
			this.cuenta.conexion.enviar_Paquete("Gt", false);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x000094B4 File Offset: 0x000078B4
		public async Task get_Mover(bool cercano, Luchadores enemigo)
		{
			KeyValuePair<short, MovimientoNodo>? nodo = null;
			Mapa mapa = this.cuenta.juego.mapa;
			int distancia = -1;
			int distancia_total = this.Get_Total_Distancia_Enemigo(this.pelea.jugador_luchador.celda);
			foreach (KeyValuePair<short, MovimientoNodo> kvp in PeleasPathfinder.get_Celdas_Accesibles(this.pelea, mapa, this.pelea.jugador_luchador.celda))
			{
				bool flag = !kvp.Value.alcanzable;
				if (!flag)
				{
					int temporal_distancia = this.Get_Total_Distancia_Enemigo(mapa.get_Celda_Id(kvp.Key));
					bool flag2 = (cercano && temporal_distancia <= distancia_total) || (!cercano && temporal_distancia >= distancia_total);
					if (flag2)
					{
						if (cercano)
						{
							nodo = new KeyValuePair<short, MovimientoNodo>?(kvp);
							distancia_total = temporal_distancia;
						}
						else
						{
							bool flag3 = kvp.Value.camino.celdas_accesibles.Count >= distancia;
							if (flag3)
							{
								nodo = new KeyValuePair<short, MovimientoNodo>?(kvp);
								distancia_total = temporal_distancia;
								distancia = kvp.Value.camino.celdas_accesibles.Count;
							}
						}
					}
					kvp = default(KeyValuePair<short, MovimientoNodo>);
				}
			}
			Dictionary<short, MovimientoNodo>.Enumerator enumerator = default(Dictionary<short, MovimientoNodo>.Enumerator);
			bool flag4 = nodo != null;
			if (flag4)
			{
				await this.cuenta.juego.manejador.movimientos.get_Mover_Celda_Pelea(nodo);
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000950C File Offset: 0x0000790C
		public int Get_Total_Distancia_Enemigo(Celda celda)
		{
			return this.cuenta.juego.pelea.get_Enemigos.Sum((Luchadores e) => e.celda.get_Distancia_Entre_Dos_Casillas(celda) - 1);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000954C File Offset: 0x0000794C
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00009558 File Offset: 0x00007958
		~PeleaExtensiones()
		{
			this.Dispose(false);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00009588 File Offset: 0x00007988
		public virtual void Dispose(bool disposing)
		{
			bool flag = !this.disposed;
			if (flag)
			{
				if (disposing)
				{
					this.configuracion.Dispose();
				}
				this.cuenta = null;
				this.disposed = true;
			}
		}

		// Token: 0x040000A0 RID: 160
		private Cuenta cuenta;

		// Token: 0x040000A1 RID: 161
		private ManejadorHechizos manejador_hechizos;

		// Token: 0x040000A2 RID: 162
		private Pelea pelea;

		// Token: 0x040000A3 RID: 163
		private int hechizo_lanzado_index;

		// Token: 0x040000A4 RID: 164
		private bool esperando_sequencia_fin;

		// Token: 0x040000A5 RID: 165
		private bool disposed;
	}
}
