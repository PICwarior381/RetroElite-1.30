using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using RetroElite.Otros.Enums;
using RetroElite.Otros.Game.Personaje.Hechizos;
using RetroElite.Otros.Mapas;
using RetroElite.Otros.Peleas.Enums;
using RetroElite.Otros.Peleas.Peleadores;

namespace RetroElite.Otros.Peleas
{
	// Token: 0x02000033 RID: 51
	public class Pelea : IEliminable, IDisposable
	{
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x000076C9 File Offset: 0x00005AC9
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x000076D1 File Offset: 0x00005AD1
		public Cuenta cuenta { get; private set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x000076DA File Offset: 0x00005ADA
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x000076E2 File Offset: 0x00005AE2
		public LuchadorPersonaje jugador_luchador { get; private set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x000076EB File Offset: 0x00005AEB
		public IEnumerable<Luchadores> get_Aliados
		{
			get
			{
				return from a in this.aliados.Values
				where a.esta_vivo
				select a;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001AA RID: 426 RVA: 0x0000771C File Offset: 0x00005B1C
		public IEnumerable<Luchadores> get_Enemigos
		{
			get
			{
				return from e in this.enemigos.Values
				where e.esta_vivo
				select e;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001AB RID: 427 RVA: 0x0000774D File Offset: 0x00005B4D
		public IEnumerable<Luchadores> get_Luchadores
		{
			get
			{
				return from f in this.luchadores.Values
				where f.esta_vivo
				select f;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001AC RID: 428 RVA: 0x0000777E File Offset: 0x00005B7E
		public int total_enemigos_vivos
		{
			get
			{
				return this.get_Enemigos.Count((Luchadores f) => f.esta_vivo);
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001AD RID: 429 RVA: 0x000077AA File Offset: 0x00005BAA
		public int contador_invocaciones
		{
			get
			{
				return this.get_Luchadores.Count((Luchadores f) => f.id_invocador == this.jugador_luchador.id);
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001AE RID: 430 RVA: 0x000077C3 File Offset: 0x00005BC3
		public List<short> get_Celdas_Ocupadas
		{
			get
			{
				return (from f in this.get_Luchadores
				select f.celda.id).ToList<short>();
			}
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060001AF RID: 431 RVA: 0x000077F4 File Offset: 0x00005BF4
		// (remove) Token: 0x060001B0 RID: 432 RVA: 0x0000782C File Offset: 0x00005C2C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action pelea_creada;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060001B1 RID: 433 RVA: 0x00007864 File Offset: 0x00005C64
		// (remove) Token: 0x060001B2 RID: 434 RVA: 0x0000789C File Offset: 0x00005C9C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action pelea_acabada;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x060001B3 RID: 435 RVA: 0x000078D4 File Offset: 0x00005CD4
		// (remove) Token: 0x060001B4 RID: 436 RVA: 0x0000790C File Offset: 0x00005D0C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action turno_iniciado;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x060001B5 RID: 437 RVA: 0x00007944 File Offset: 0x00005D44
		// (remove) Token: 0x060001B6 RID: 438 RVA: 0x0000797C File Offset: 0x00005D7C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<short, bool> hechizo_lanzado;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x060001B7 RID: 439 RVA: 0x000079B4 File Offset: 0x00005DB4
		// (remove) Token: 0x060001B8 RID: 440 RVA: 0x000079EC File Offset: 0x00005DEC
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<bool> movimiento;

		// Token: 0x060001B9 RID: 441 RVA: 0x00007A24 File Offset: 0x00005E24
		public Pelea(Cuenta _cuenta)
		{
			this.cuenta = _cuenta;
			this.luchadores = new ConcurrentDictionary<int, Luchadores>();
			this.enemigos = new ConcurrentDictionary<int, Luchadores>();
			this.aliados = new ConcurrentDictionary<int, Luchadores>();
			this.hechizos_intervalo = new Dictionary<int, int>();
			this.total_hechizos_lanzados = new Dictionary<int, int>();
			this.total_hechizos_lanzados_en_celda = new Dictionary<int, Dictionary<int, int>>();
			this.celdas_preparacion = new List<Celda>();
			this.estado_pelea = 0;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00007A98 File Offset: 0x00005E98
		public async Task get_Lanzar_Hechizo(short hechizo_id, short celda_id)
		{
			bool flag = this.cuenta.Estado_Cuenta != EstadoCuenta.LUCHANDO;
			if (!flag)
			{
				await this.cuenta.conexion.enviar_Paquete_Async("GA300" + hechizo_id.ToString() + ";" + celda_id.ToString(), false);
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00007AF0 File Offset: 0x00005EF0
		public void actualizar_Hechizo_Exito(short celda_id, short hechizo_id)
		{
			Hechizo hechizo = this.cuenta.juego.personaje.get_Hechizo(hechizo_id);
			HechizoStats stats = hechizo.get_Stats();
			bool flag = stats.intervalo > 0 && !this.hechizos_intervalo.ContainsKey((int)hechizo.id);
			if (flag)
			{
				this.hechizos_intervalo.Add((int)hechizo.id, (int)stats.intervalo);
			}
			bool flag2 = !this.total_hechizos_lanzados.ContainsKey((int)hechizo.id);
			if (flag2)
			{
				this.total_hechizos_lanzados.Add((int)hechizo.id, 0);
			}
			Dictionary<int, int> dictionary = this.total_hechizos_lanzados;
			int num = (int)hechizo.id;
			int num2 = dictionary[num];
			dictionary[num] = num2 + 1;
			bool flag3 = this.total_hechizos_lanzados_en_celda.ContainsKey((int)hechizo.id);
			if (flag3)
			{
				bool flag4 = !this.total_hechizos_lanzados_en_celda[(int)hechizo.id].ContainsKey((int)celda_id);
				if (flag4)
				{
					this.total_hechizos_lanzados_en_celda[(int)hechizo.id].Add((int)celda_id, 0);
				}
				Dictionary<int, int> dictionary2 = this.total_hechizos_lanzados_en_celda[(int)hechizo.id];
				num = dictionary2[(int)celda_id];
				dictionary2[(int)celda_id] = num + 1;
			}
			else
			{
				this.total_hechizos_lanzados_en_celda.Add((int)hechizo.id, new Dictionary<int, int>
				{
					{
						(int)celda_id,
						1
					}
				});
			}
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00007C48 File Offset: 0x00006048
		public void get_Final_Turno(int id_personaje)
		{
			Luchadores luchadores = this.get_Luchador_Por_Id(id_personaje);
			bool flag = luchadores == this.jugador_luchador;
			if (flag)
			{
				this.total_hechizos_lanzados.Clear();
				this.total_hechizos_lanzados_en_celda.Clear();
				for (int i = this.hechizos_intervalo.Count - 1; i >= 0; i--)
				{
					int key = this.hechizos_intervalo.ElementAt(i).Key;
					Dictionary<int, int> dictionary = this.hechizos_intervalo;
					int key2 = key;
					int num = dictionary[key2];
					dictionary[key2] = num - 1;
					bool flag2 = this.hechizos_intervalo[key] == 0;
					if (flag2)
					{
						this.hechizos_intervalo.Remove(key);
					}
				}
			}
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00007D04 File Offset: 0x00006104
		public Luchadores get_Luchador_Por_Id(int id)
		{
			bool flag = this.jugador_luchador != null && this.jugador_luchador.id == id;
			Luchadores result;
			if (flag)
			{
				result = this.jugador_luchador;
			}
			else
			{
				Luchadores luchadores;
				bool flag2 = this.luchadores.TryGetValue(id, out luchadores);
				if (flag2)
				{
					result = luchadores;
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00007D54 File Offset: 0x00006154
		public Luchadores get_Enemigo_Mas_Debil()
		{
			int num = -1;
			Luchadores result = null;
			foreach (Luchadores luchadores in this.get_Enemigos)
			{
				bool flag = !luchadores.esta_vivo;
				if (!flag)
				{
					bool flag2 = num == -1 || luchadores.porcentaje_vida < num;
					if (flag2)
					{
						num = luchadores.porcentaje_vida;
						result = luchadores;
					}
				}
			}
			return result;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00007DDC File Offset: 0x000061DC
		public Luchadores get_Obtener_Aliado_Mas_Cercano()
		{
			int num = -1;
			Luchadores result = null;
			foreach (Luchadores luchadores in this.get_Aliados)
			{
				bool flag = !luchadores.esta_vivo;
				if (!flag)
				{
					int num2 = this.jugador_luchador.celda.get_Distancia_Entre_Dos_Casillas(luchadores.celda);
					bool flag2 = num == -1 || num2 < num;
					if (flag2)
					{
						num = num2;
						result = luchadores;
					}
				}
			}
			return result;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00007E78 File Offset: 0x00006278
		public Luchadores get_Obtener_Enemigo_Mas_Cercano()
		{
			int num = -1;
			Luchadores result = null;
			foreach (Luchadores luchadores in this.get_Enemigos)
			{
				bool flag = !luchadores.esta_vivo;
				if (!flag)
				{
					int num2 = this.jugador_luchador.celda.get_Distancia_Entre_Dos_Casillas(luchadores.celda);
					bool flag2 = num == -1 || num2 < num;
					if (flag2)
					{
						num = num2;
						result = luchadores;
					}
				}
			}
			return result;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00007F14 File Offset: 0x00006314
		public void get_Agregar_Luchador(Luchadores luchador)
		{
			bool flag = luchador.id == this.cuenta.juego.personaje.id;
			if (flag)
			{
				this.jugador_luchador = new LuchadorPersonaje(this.cuenta.juego.personaje.nombre, this.cuenta.juego.personaje.nivel, luchador);
			}
			else
			{
				bool flag2 = !this.luchadores.TryAdd(luchador.id, luchador);
				if (flag2)
				{
					luchador.get_Actualizar_Luchador(luchador.id, luchador.esta_vivo, luchador.vida_actual, luchador.pa, luchador.pm, luchador.celda, luchador.vida_maxima, luchador.equipo, luchador.id_invocador);
				}
			}
			this.get_Ordenar_Luchadores();
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00007FDC File Offset: 0x000063DC
		private void get_Ordenar_Luchadores()
		{
			bool flag = this.jugador_luchador == null;
			if (!flag)
			{
				foreach (Luchadores luchadores in this.get_Luchadores)
				{
					bool flag2 = this.aliados.ContainsKey(luchadores.id) || this.enemigos.ContainsKey(luchadores.id);
					if (!flag2)
					{
						bool flag3 = luchadores.equipo == this.jugador_luchador.equipo;
						if (flag3)
						{
							this.aliados.TryAdd(luchadores.id, luchadores);
						}
						else
						{
							this.enemigos.TryAdd(luchadores.id, luchadores);
						}
					}
				}
			}
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x000080A8 File Offset: 0x000064A8
		public short get_Celda_Mas_Cercana_O_Lejana(bool cercana, IEnumerable<Celda> celdas_posibles)
		{
			short num = -1;
			int num2 = -1;
			foreach (Celda celda in celdas_posibles)
			{
				int num3 = this.get_Distancia_Desde_Enemigo(celda);
				bool flag = num == -1 || (cercana && num3 < num2) || (!cercana && num3 > num2);
				if (flag)
				{
					num = celda.id;
					num2 = num3;
				}
			}
			return num;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00008134 File Offset: 0x00006534
		public int get_Distancia_Desde_Enemigo(Celda celda_actual)
		{
			return this.get_Enemigos.Sum((Luchadores e) => celda_actual.get_Distancia_Entre_Dos_Casillas(e.celda) - 1);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00008168 File Offset: 0x00006568
		public Luchadores get_Luchador_Esta_En_Celda(int celda_id)
		{
			LuchadorPersonaje jugador_luchador = this.jugador_luchador;
			short? num = (jugador_luchador != null) ? new short?(jugador_luchador.celda.id) : null;
			int? num2 = (num != null) ? new int?((int)num.GetValueOrDefault()) : null;
			int celda_id2 = celda_id;
			bool flag = num2.GetValueOrDefault() == celda_id2 & num2 != null;
			Luchadores result;
			if (flag)
			{
				result = this.jugador_luchador;
			}
			else
			{
				result = this.get_Luchadores.FirstOrDefault((Luchadores f) => f.esta_vivo && (int)f.celda.id == celda_id);
			}
			return result;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00008210 File Offset: 0x00006610
		public bool es_Celda_Libre(Celda celda)
		{
			return this.get_Luchador_Esta_En_Celda((int)celda.id) == null;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00008224 File Offset: 0x00006624
		public IEnumerable<Luchadores> get_Cuerpo_A_Cuerpo_Enemigo(Celda celda = null)
		{
			return from enemigo in this.get_Enemigos
			where enemigo.esta_vivo && ((celda == null) ? this.jugador_luchador.celda.get_Distancia_Entre_Dos_Casillas(enemigo.celda) : enemigo.celda.get_Distancia_Entre_Dos_Casillas(celda)) == 1
			select enemigo;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000825C File Offset: 0x0000665C
		public IEnumerable<Luchadores> get_Cuerpo_A_Cuerpo_Aliado(Celda celda = null)
		{
			return from aliado in this.get_Aliados
			where aliado.esta_vivo && ((celda == null) ? this.jugador_luchador.celda.get_Distancia_Entre_Dos_Casillas(aliado.celda) : aliado.celda.get_Distancia_Entre_Dos_Casillas(celda)) == 1
			select aliado;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00008294 File Offset: 0x00006694
		public bool esta_Cuerpo_A_Cuerpo_Con_Enemigo(Celda celda = null)
		{
			return this.get_Cuerpo_A_Cuerpo_Enemigo(celda).Count<Luchadores>() > 0;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x000082A5 File Offset: 0x000066A5
		public bool esta_Cuerpo_A_Cuerpo_Con_Aliado(Celda celda = null)
		{
			return this.get_Cuerpo_A_Cuerpo_Aliado(celda).Count<Luchadores>() > 0;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x000082B8 File Offset: 0x000066B8
		public FallosLanzandoHechizo get_Puede_Lanzar_hechizo(short hechizo_id)
		{
			Hechizo hechizo = this.cuenta.juego.personaje.get_Hechizo(hechizo_id);
			bool flag = hechizo == null;
			FallosLanzandoHechizo result;
			if (flag)
			{
				result = FallosLanzandoHechizo.DESONOCIDO;
			}
			else
			{
				HechizoStats stats = hechizo.get_Stats();
				bool flag2 = this.jugador_luchador.pa < stats.coste_pa;
				if (flag2)
				{
					result = FallosLanzandoHechizo.PUNTOS_ACCION;
				}
				else
				{
					bool flag3 = stats.lanzamientos_por_turno > 0 && this.total_hechizos_lanzados.ContainsKey((int)hechizo_id) && this.total_hechizos_lanzados[(int)hechizo_id] >= (int)stats.lanzamientos_por_turno;
					if (flag3)
					{
						result = FallosLanzandoHechizo.DEMASIADOS_LANZAMIENTOS;
					}
					else
					{
						bool flag4 = this.hechizos_intervalo.ContainsKey((int)hechizo_id);
						if (flag4)
						{
							result = FallosLanzandoHechizo.COOLDOWN;
						}
						else
						{
							bool flag5 = stats.efectos_normales.Count > 0 && stats.efectos_normales[0].id == 181 && this.contador_invocaciones >= this.cuenta.juego.personaje.caracteristicas.criaturas_invocables.total_stats;
							if (flag5)
							{
								result = FallosLanzandoHechizo.DEMASIADAS_INVOCACIONES;
							}
							else
							{
								result = FallosLanzandoHechizo.NINGUNO;
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x000083C8 File Offset: 0x000067C8
		public FallosLanzandoHechizo get_Puede_Lanzar_hechizo(short hechizo_id, Celda celda_actual, Celda celda_objetivo, Mapa mapa)
		{
			Hechizo hechizo = this.cuenta.juego.personaje.get_Hechizo(hechizo_id);
			bool flag = hechizo == null;
			FallosLanzandoHechizo result;
			if (flag)
			{
				result = FallosLanzandoHechizo.DESONOCIDO;
			}
			else
			{
				HechizoStats stats = hechizo.get_Stats();
				bool flag2 = stats.lanzamientos_por_objetivo > 0 && this.total_hechizos_lanzados_en_celda.ContainsKey((int)hechizo_id) && this.total_hechizos_lanzados_en_celda[(int)hechizo_id].ContainsKey((int)celda_objetivo.id) && this.total_hechizos_lanzados_en_celda[(int)hechizo_id][(int)celda_objetivo.id] >= (int)stats.lanzamientos_por_objetivo;
				if (flag2)
				{
					result = FallosLanzandoHechizo.DEMASIADOS_LANZAMIENTOS_POR_OBJETIVO;
				}
				else
				{
					bool flag3 = stats.es_celda_vacia && !this.es_Celda_Libre(celda_objetivo);
					if (flag3)
					{
						result = FallosLanzandoHechizo.NECESITA_CELDA_LIBRE;
					}
					else
					{
						bool flag4 = stats.es_lanzado_linea && !this.jugador_luchador.celda.get_Esta_En_Linea(celda_objetivo);
						if (flag4)
						{
							result = FallosLanzandoHechizo.NO_ESTA_EN_LINEA;
						}
						else
						{
							bool flag5 = !this.get_Rango_hechizo(celda_actual, stats, mapa).Contains(celda_objetivo.id);
							if (flag5)
							{
								result = FallosLanzandoHechizo.NO_ESTA_EN_RANGO;
							}
							else
							{
								result = FallosLanzandoHechizo.NINGUNO;
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x000084D4 File Offset: 0x000068D4
		public List<short> get_Rango_hechizo(Celda celda_personaje, HechizoStats datos_hechizo, Mapa mapa)
		{
			List<short> list = new List<short>();
			foreach (Celda celda in HechizoShape.Get_Lista_Celdas_Rango_Hechizo(celda_personaje, datos_hechizo, this.cuenta.juego.mapa, this.cuenta.juego.personaje.caracteristicas.alcanze.total_stats))
			{
				bool flag = celda == null || list.Contains(celda.id);
				if (!flag)
				{
					bool flag2 = datos_hechizo.es_celda_vacia && this.get_Celdas_Ocupadas.Contains(celda.id);
					if (!flag2)
					{
						bool flag3 = celda.tipo != TipoCelda.NO_CAMINABLE || celda.tipo != TipoCelda.OBJETO_INTERACTIVO;
						if (flag3)
						{
							list.Add(celda.id);
						}
					}
				}
			}
			bool es_lanzado_con_vision = datos_hechizo.es_lanzado_con_vision;
			if (es_lanzado_con_vision)
			{
				for (int i = list.Count - 1; i >= 0; i--)
				{
					bool flag4 = Pelea.get_Linea_Obstruida(mapa, celda_personaje, mapa.get_Celda_Id(list[i]), this.get_Celdas_Ocupadas);
					if (flag4)
					{
						list.RemoveAt(i);
					}
				}
			}
			return list;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000861C File Offset: 0x00006A1C
		public static bool get_Linea_Obstruida(Mapa mapa, Celda celda_inicial, Celda celda_destino, List<short> celdas_ocupadas)
		{
			double num = (double)celda_inicial.x + 0.5;
			double num2 = (double)celda_inicial.y + 0.5;
			double num3 = (double)celda_destino.x + 0.5;
			double num4 = (double)celda_destino.y + 0.5;
			double lastX = (double)celda_inicial.x;
			double lastY = (double)celda_inicial.y;
			bool flag = Math.Abs(num - num3) == Math.Abs(num2 - num4);
			double num5;
			double num6;
			double num7;
			int num8;
			if (flag)
			{
				num5 = Math.Abs(num - num3);
				num6 = (double)((num3 > num) ? 1 : -1);
				num7 = (double)((num4 > num2) ? 1 : -1);
				num8 = 1;
			}
			else
			{
				bool flag2 = Math.Abs(num - num3) > Math.Abs(num2 - num4);
				if (flag2)
				{
					num5 = Math.Abs(num - num3);
					num6 = (double)((num3 > num) ? 1 : -1);
					num7 = (num4 - num2) / num5;
					num7 *= 100.0;
					num7 = Math.Ceiling(num7) / 100.0;
					num8 = 2;
				}
				else
				{
					num5 = Math.Abs(num2 - num4);
					num6 = (num3 - num) / num5;
					num6 *= 100.0;
					num6 = Math.Ceiling(num6) / 100.0;
					num7 = (double)((num4 > num2) ? 1 : -1);
					num8 = 3;
				}
			}
			int num9 = Convert.ToInt32(Math.Round(Math.Floor(Convert.ToDouble(3.0 + num5 / 2.0))));
			int num10 = Convert.ToInt32(Math.Round(Math.Floor(Convert.ToDouble(97.0 - num5 / 2.0))));
			int num11 = 0;
			while ((double)num11 < num5)
			{
				double num12 = num + num6;
				double num13 = num2 + num7;
				int num14 = num8;
				int num15 = num14;
				if (num15 != 2)
				{
					if (num15 != 3)
					{
						bool flag3 = Pelea.get_Es_Celda_Obstruida(Math.Floor(num12), Math.Floor(num13), mapa, celdas_ocupadas, (int)celda_destino.id, lastX, lastY);
						if (flag3)
						{
							return true;
						}
						lastX = Math.Floor(num12);
						lastY = Math.Floor(num13);
					}
					else
					{
						double num16 = Math.Ceiling(num * 100.0 + num6 * 50.0) / 100.0;
						double num17 = Math.Floor(num * 100.0 + num6 * 150.0) / 100.0;
						double num18 = Math.Floor(Math.Abs(Math.Floor(num16) * 100.0 - num16 * 100.0)) / 100.0;
						double num19 = Math.Ceiling(Math.Abs(Math.Ceiling(num17) * 100.0 - num17 * 100.0)) / 100.0;
						double num20 = Math.Floor(num13);
						bool flag4 = Math.Floor(num16) == Math.Floor(num17);
						if (flag4)
						{
							double num21 = Math.Floor(num12);
							bool flag5 = (num16 == num21 && num17 < num21) || (num17 == num21 && num16 < num21);
							if (flag5)
							{
								num21 = Math.Ceiling(num12);
							}
							bool flag6 = Pelea.get_Es_Celda_Obstruida(num21, num20, mapa, celdas_ocupadas, (int)celda_destino.id, lastX, lastY);
							if (flag6)
							{
								return true;
							}
							lastX = num21;
							lastY = num20;
						}
						else
						{
							bool flag7 = Math.Ceiling(num16) == Math.Ceiling(num17);
							if (flag7)
							{
								double num21 = Math.Ceiling(num12);
								bool flag8 = (num16 == num21 && num17 < num21) || (num17 == num21 && num16 < num21);
								if (flag8)
								{
									num21 = Math.Floor(num12);
								}
								bool flag9 = Pelea.get_Es_Celda_Obstruida(num21, num20, mapa, celdas_ocupadas, (int)celda_destino.id, lastX, lastY);
								if (flag9)
								{
									return true;
								}
								lastX = num21;
								lastY = num20;
							}
							else
							{
								bool flag10 = Math.Floor(num18 * 100.0) <= (double)num9;
								if (flag10)
								{
									bool flag11 = Pelea.get_Es_Celda_Obstruida(Math.Floor(num17), num20, mapa, celdas_ocupadas, (int)celda_destino.id, lastX, lastY);
									if (flag11)
									{
										return true;
									}
									lastX = Math.Floor(num17);
									lastY = num20;
								}
								else
								{
									bool flag12 = Math.Floor(num19 * 100.0) >= (double)num10;
									if (flag12)
									{
										bool flag13 = Pelea.get_Es_Celda_Obstruida(Math.Floor(num16), num20, mapa, celdas_ocupadas, (int)celda_destino.id, lastX, lastY);
										if (flag13)
										{
											return true;
										}
										lastX = Math.Floor(num16);
										lastY = num20;
									}
									else
									{
										bool flag14 = Pelea.get_Es_Celda_Obstruida(Math.Floor(num16), num20, mapa, celdas_ocupadas, (int)celda_destino.id, lastX, lastY);
										if (flag14)
										{
											return true;
										}
										lastX = Math.Floor(num16);
										lastY = num20;
										bool flag15 = Pelea.get_Es_Celda_Obstruida(Math.Floor(num17), num20, mapa, celdas_ocupadas, (int)celda_destino.id, lastX, lastY);
										if (flag15)
										{
											return true;
										}
										lastX = Math.Floor(num17);
									}
								}
							}
						}
					}
				}
				else
				{
					double num22 = Math.Ceiling(num2 * 100.0 + num7 * 50.0) / 100.0;
					double num23 = Math.Floor(num2 * 100.0 + num7 * 150.0) / 100.0;
					double num24 = Math.Floor(Math.Abs(Math.Floor(num22) * 100.0 - num22 * 100.0)) / 100.0;
					double num25 = Math.Ceiling(Math.Abs(Math.Ceiling(num23) * 100.0 - num23 * 100.0)) / 100.0;
					double num21 = Math.Floor(num12);
					bool flag16 = Math.Floor(num22) == Math.Floor(num23);
					if (flag16)
					{
						double num20 = Math.Floor(num13);
						bool flag17 = (num22 == num20 && num23 < num20) || (num23 == num20 && num22 < num20);
						if (flag17)
						{
							num20 = Math.Ceiling(num13);
						}
						bool flag18 = Pelea.get_Es_Celda_Obstruida(num21, num20, mapa, celdas_ocupadas, (int)celda_destino.id, lastX, lastY);
						if (flag18)
						{
							return true;
						}
						lastX = num21;
						lastY = num20;
					}
					else
					{
						bool flag19 = Math.Ceiling(num22) == Math.Ceiling(num23);
						if (flag19)
						{
							double num20 = Math.Ceiling(num13);
							bool flag20 = (num22 == num20 && num23 < num20) || (num23 == num20 && num22 < num20);
							if (flag20)
							{
								num20 = Math.Floor(num13);
							}
							bool flag21 = Pelea.get_Es_Celda_Obstruida(num21, num20, mapa, celdas_ocupadas, (int)celda_destino.id, lastX, lastY);
							if (flag21)
							{
								return true;
							}
							lastX = num21;
							lastY = num20;
						}
						else
						{
							bool flag22 = Math.Floor(num24 * 100.0) <= (double)num9;
							if (flag22)
							{
								bool flag23 = Pelea.get_Es_Celda_Obstruida(num21, Math.Floor(num23), mapa, celdas_ocupadas, (int)celda_destino.id, lastX, lastY);
								if (flag23)
								{
									return true;
								}
								lastX = num21;
								lastY = Math.Floor(num23);
							}
							else
							{
								bool flag24 = Math.Floor(num25 * 100.0) >= (double)num10;
								if (flag24)
								{
									bool flag25 = Pelea.get_Es_Celda_Obstruida(num21, Math.Floor(num22), mapa, celdas_ocupadas, (int)celda_destino.id, lastX, lastY);
									if (flag25)
									{
										return true;
									}
									lastX = num21;
									lastY = Math.Floor(num22);
								}
								else
								{
									bool flag26 = Pelea.get_Es_Celda_Obstruida(num21, Math.Floor(num22), mapa, celdas_ocupadas, (int)celda_destino.id, lastX, lastY);
									if (flag26)
									{
										return true;
									}
									lastX = num21;
									lastY = Math.Floor(num22);
									bool flag27 = Pelea.get_Es_Celda_Obstruida(num21, Math.Floor(num23), mapa, celdas_ocupadas, (int)celda_destino.id, lastX, lastY);
									if (flag27)
									{
										return true;
									}
									lastY = Math.Floor(num23);
								}
							}
						}
					}
				}
				num = (num * 100.0 + num6 * 100.0) / 100.0;
				num2 = (num2 * 100.0 + num7 * 100.0) / 100.0;
				num11++;
			}
			return false;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00008E58 File Offset: 0x00007258
		private static bool get_Es_Celda_Obstruida(double x, double y, Mapa map, List<short> occupiedCells, int targetCellId, double lastX, double lastY)
		{
			Celda celda = map.get_Celda_Por_Coordenadas((int)x, (int)y);
			return celda.es_linea_vision || ((int)celda.id != targetCellId && occupiedCells.Contains(celda.id));
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00008E9C File Offset: 0x0000729C
		public void get_Combate_Creado()
		{
			this.cuenta.juego.personaje.timer_regeneracion.Change(-1, -1);
			this.cuenta.Estado_Cuenta = EstadoCuenta.LUCHANDO;
			Action action = this.pelea_creada;
			if (action != null)
			{
				action();
			}
			this.cuenta.logger.log_Fight("COMBAT", "Nouveau combat a commencé");
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00008F04 File Offset: 0x00007304
		public void get_Combate_Acabado()
		{
			this.limpiar();
			Action action = this.pelea_acabada;
			if (action != null)
			{
				action();
			}
			this.cuenta.Estado_Cuenta = EstadoCuenta.CONECTADO_INACTIVO;
			this.cuenta.logger.log_Fight("COMBAT", "Combat terminé");
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00008F54 File Offset: 0x00007354
		public void limpiar()
		{
			this.enemigos.Clear();
			this.aliados.Clear();
			this.luchadores.Clear();
			this.hechizos_intervalo.Clear();
			this.total_hechizos_lanzados.Clear();
			this.total_hechizos_lanzados_en_celda.Clear();
			this.celdas_preparacion.Clear();
			this.jugador_luchador = null;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00008FBE File Offset: 0x000073BE
		public void get_Turno_Iniciado()
		{
			Action action = this.turno_iniciado;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00008FD3 File Offset: 0x000073D3
		public void get_Hechizo_Lanzado(short celda_id, bool exito)
		{
			Action<short, bool> action = this.hechizo_lanzado;
			if (action != null)
			{
				action(celda_id, exito);
			}
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00008FEA File Offset: 0x000073EA
		public void get_Movimiento_Exito(bool exito)
		{
			Action<bool> action = this.movimiento;
			if (action != null)
			{
				action(exito);
			}
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00009000 File Offset: 0x00007400
		public void get_Turno_Acabado()
		{
			this.total_hechizos_lanzados.Clear();
			this.total_hechizos_lanzados_en_celda.Clear();
			for (int i = this.hechizos_intervalo.Count - 1; i >= 0; i--)
			{
				int key = this.hechizos_intervalo.ElementAt(i).Key;
				Dictionary<int, int> dictionary = this.hechizos_intervalo;
				int key2 = key;
				int num = dictionary[key2];
				dictionary[key2] = num - 1;
				bool flag = this.hechizos_intervalo[key] == 0;
				if (flag)
				{
					this.hechizos_intervalo.Remove(key);
				}
			}
			this.cuenta.logger.log_Fight("COMBAT", "Fin du tour de jeu");
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x000090B7 File Offset: 0x000074B7
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x000090C4 File Offset: 0x000074C4
		~Pelea()
		{
			this.Dispose(false);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x000090F4 File Offset: 0x000074F4
		public virtual void Dispose(bool disposing)
		{
			bool flag = !this.disposed;
			if (flag)
			{
				this.luchadores.Clear();
				this.enemigos.Clear();
				this.aliados.Clear();
				this.total_hechizos_lanzados.Clear();
				this.hechizos_intervalo.Clear();
				this.total_hechizos_lanzados_en_celda.Clear();
				this.celdas_preparacion.Clear();
				this.cuenta = null;
				this.luchadores = null;
				this.enemigos = null;
				this.aliados = null;
				this.total_hechizos_lanzados = null;
				this.hechizos_intervalo = null;
				this.total_hechizos_lanzados_en_celda = null;
				this.jugador_luchador = null;
				this.celdas_preparacion = null;
				this.disposed = true;
			}
		}

		// Token: 0x04000090 RID: 144
		private ConcurrentDictionary<int, Luchadores> luchadores;

		// Token: 0x04000091 RID: 145
		private ConcurrentDictionary<int, Luchadores> enemigos;

		// Token: 0x04000092 RID: 146
		private ConcurrentDictionary<int, Luchadores> aliados;

		// Token: 0x04000093 RID: 147
		private Dictionary<int, int> hechizos_intervalo;

		// Token: 0x04000094 RID: 148
		private Dictionary<int, int> total_hechizos_lanzados;

		// Token: 0x04000095 RID: 149
		private Dictionary<int, Dictionary<int, int>> total_hechizos_lanzados_en_celda;

		// Token: 0x04000096 RID: 150
		public List<Celda> celdas_preparacion;

		// Token: 0x04000098 RID: 152
		public byte estado_pelea;

		// Token: 0x04000099 RID: 153
		private bool disposed;
	}
}
