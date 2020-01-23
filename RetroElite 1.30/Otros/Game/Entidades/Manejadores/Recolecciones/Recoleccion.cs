using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MoonSharp.Interpreter;
using RetroElite.Otros.Enums;
using RetroElite.Otros.Game.Entidades.Manejadores.Movimientos;
using RetroElite.Otros.Game.Personaje;
using RetroElite.Otros.Game.Personaje.Inventario;
using RetroElite.Otros.Game.Personaje.Inventario.Enums;
using RetroElite.Otros.Mapas;
using RetroElite.Otros.Mapas.Interactivo;
using RetroElite.Otros.Mapas.Movimiento.Mapas;

namespace RetroElite.Otros.Game.Entidades.Manejadores.Recolecciones
{
	// Token: 0x02000067 RID: 103
	public class Recoleccion : IDisposable
	{
		// Token: 0x1400001D RID: 29
		// (add) Token: 0x0600043B RID: 1083 RVA: 0x0000F2F0 File Offset: 0x0000D6F0
		// (remove) Token: 0x0600043C RID: 1084 RVA: 0x0000F328 File Offset: 0x0000D728
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action recoleccion_iniciada;

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x0600043D RID: 1085 RVA: 0x0000F360 File Offset: 0x0000D760
		// (remove) Token: 0x0600043E RID: 1086 RVA: 0x0000F398 File Offset: 0x0000D798
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<RecoleccionResultado> recoleccion_acabada;

		// Token: 0x0600043F RID: 1087 RVA: 0x0000F3D0 File Offset: 0x0000D7D0
		public Recoleccion(Cuenta _cuenta, Movimiento movimientos, Mapa _mapa)
		{
			this.cuenta = _cuenta;
			this.interactivos_no_utilizables = new List<int>();
			this.pathfinder = new Pathfinder();
			this.mapa = _mapa;
			movimientos.movimiento_finalizado += this.get_Movimiento_Finalizado;
			this.mapa.mapa_actualizado += this.evento_Mapa_Actualizado;
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0000F43B File Offset: 0x0000D83B
		public bool get_Puede_Recolectar(List<short> elementos_recolectables)
		{
			return this.get_Interactivos_Utilizables(elementos_recolectables).Count > 0;
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0000F44C File Offset: 0x0000D84C
		public void get_Cancelar_Interactivo()
		{
			this.interactivo_recolectando = null;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0000F458 File Offset: 0x0000D858
		public bool get_Recolectar(List<short> elementos)
		{
			bool flag = this.cuenta.esta_ocupado() || this.interactivo_recolectando != null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				foreach (KeyValuePair<short, ObjetoInteractivo> interactivo in this.get_Interactivos_Utilizables(elementos))
				{
					bool flag2 = this.get_Intentar_Mover_Interactivo(interactivo);
					if (flag2)
					{
						return true;
					}
				}
				this.cuenta.logger.log_Peligro("inventaire", "Aucun objet trouvé");
				result = false;
			}
			return result;
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0000F4FC File Offset: 0x0000D8FC
		private Dictionary<short, ObjetoInteractivo> get_Interactivos_Utilizables(List<short> elementos_ids)
		{
			Dictionary<short, ObjetoInteractivo> dictionary = new Dictionary<short, ObjetoInteractivo>();
			PersonajeJuego personaje = this.cuenta.juego.personaje;
			ObjetosInventario objetosInventario = personaje.inventario.get_Objeto_en_Posicion(InventarioPosiciones.CAC);
			byte b = 1;
			bool flag = false;
			bool flag2 = objetosInventario != null;
			if (flag2)
			{
				b = this.get_Distancia_herramienta(objetosInventario.id_modelo);
				flag = Recoleccion.herramientas_pescar.Contains(objetosInventario.id_modelo);
			}
			foreach (ObjetoInteractivo objetoInteractivo in this.mapa.interactivos.Values)
			{
				bool flag3 = !objetoInteractivo.es_utilizable || !objetoInteractivo.modelo.recolectable;
				if (!flag3)
				{
					List<Celda> list = this.pathfinder.get_Path(personaje.celda, objetoInteractivo.celda, this.mapa.celdas_ocupadas(), true, b);
					bool flag4 = list == null || list.Count == 0;
					if (!flag4)
					{
						foreach (short item in objetoInteractivo.modelo.habilidades)
						{
							bool flag5 = !elementos_ids.Contains(item);
							if (!flag5)
							{
								bool flag6 = !flag && list.Last<Celda>().get_Distancia_Entre_Dos_Casillas(objetoInteractivo.celda) > 1;
								if (!flag6)
								{
									bool flag7 = flag && list.Last<Celda>().get_Distancia_Entre_Dos_Casillas(objetoInteractivo.celda) > (int)b;
									if (!flag7)
									{
										dictionary.Add(objetoInteractivo.celda.id, objetoInteractivo);
									}
								}
							}
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0000F6D0 File Offset: 0x0000DAD0
		private bool get_Intentar_Mover_Interactivo(KeyValuePair<short, ObjetoInteractivo> interactivo)
		{
			this.interactivo_recolectando = interactivo.Value;
			byte distancia_detener = 1;
			ObjetosInventario objetosInventario = this.cuenta.juego.personaje.inventario.get_Objeto_en_Posicion(InventarioPosiciones.CAC);
			bool flag = objetosInventario != null;
			if (flag)
			{
				distancia_detener = this.get_Distancia_herramienta(objetosInventario.id_modelo);
			}
			ResultadoMovimientos resultadoMovimientos = this.cuenta.juego.manejador.movimientos.get_Mover_A_Celda(this.interactivo_recolectando.celda, this.mapa.celdas_ocupadas(), true, distancia_detener);
			ResultadoMovimientos resultadoMovimientos2 = resultadoMovimientos;
			bool result;
			if (resultadoMovimientos2 > ResultadoMovimientos.MISMA_CELDA)
			{
				this.get_Cancelar_Interactivo();
				result = false;
			}
			else
			{
				this.get_Intentar_Recolectar_Interactivo();
				result = true;
			}
			return result;
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0000F778 File Offset: 0x0000DB78
		private void get_Intentar_Recolectar_Interactivo()
		{
			bool flag = !this.robado;
			if (flag)
			{
				foreach (short value in this.interactivo_recolectando.modelo.habilidades)
				{
					bool flag2 = this.cuenta.juego.personaje.get_Skills_Recoleccion_Disponibles().Contains(value);
					if (flag2)
					{
						this.cuenta.conexion.enviar_Paquete("GA500" + this.interactivo_recolectando.celda.id.ToString() + ";" + value.ToString(), false);
					}
				}
			}
			else
			{
				this.evento_Recoleccion_Acabada(RecoleccionResultado.ROBADO, this.interactivo_recolectando.celda.id);
			}
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0000F83C File Offset: 0x0000DC3C
		private void get_Movimiento_Finalizado(bool correcto)
		{
			bool flag = this.interactivo_recolectando == null;
			if (!flag)
			{
				bool flag2 = !correcto && this.cuenta.juego.manejador.movimientos.actual_path != null;
				if (flag2)
				{
					this.evento_Recoleccion_Acabada(RecoleccionResultado.FALLO, this.interactivo_recolectando.celda.id);
				}
			}
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0000F898 File Offset: 0x0000DC98
		public async Task evento_Recoleccion_Iniciada(int id_personaje, int tiempo_delay, short celda_id, byte tipo_gkk)
		{
			bool flag = this.interactivo_recolectando == null || this.interactivo_recolectando.celda.id != celda_id;
			if (!flag)
			{
				bool flag2 = this.cuenta.juego.personaje.id != id_personaje;
				if (flag2)
				{
					this.cuenta.logger.log_informacion("INFORMATION", "Un personnage a volé votre ressource ... mais t'inquiète on va en trouver une autre ");
				}
				this.cuenta.Estado_Cuenta = EstadoCuenta.RECOLECTANDO;
				Action action = this.recoleccion_iniciada;
				if (action != null)
				{
					action();
				}
				this.contador_recoleccion++;
				bool flag3 = this.cuenta.script.manejar_acciones.manejador_script.get_Global_Or<bool>("COMPTEUR_DE_RECOLTE", DataType.Boolean, false);
				if (flag3)
				{
					this.cuenta.logger.log_Script("SCRIPT", string.Format("Recolte #{0}", this.contador_recoleccion));
				}
				await Task.Delay(tiempo_delay);
				this.cuenta.conexion.enviar_Paquete("GKK" + tipo_gkk.ToString(), false);
			}
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0000F8FC File Offset: 0x0000DCFC
		public void evento_Recoleccion_Acabada(RecoleccionResultado resultado, short celda_id)
		{
			bool flag = this.interactivo_recolectando == null || this.interactivo_recolectando.celda.id != celda_id;
			if (!flag)
			{
				this.robado = false;
				this.interactivo_recolectando = null;
				this.cuenta.Estado_Cuenta = EstadoCuenta.CONECTADO_INACTIVO;
				Action<RecoleccionResultado> action = this.recoleccion_acabada;
				if (action != null)
				{
					action(resultado);
				}
			}
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0000F95F File Offset: 0x0000DD5F
		private void evento_Mapa_Actualizado()
		{
			this.pathfinder.set_Mapa(this.cuenta.juego.mapa);
			this.interactivos_no_utilizables.Clear();
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0000F98C File Offset: 0x0000DD8C
		public byte get_Distancia_herramienta(int id_objeto)
		{
			if (id_objeto <= 2188)
			{
				if (id_objeto != 596)
				{
					switch (id_objeto)
					{
					case 1860:
					case 1861:
						return 8;
					case 1862:
					case 1863:
						return 6;
					case 1864:
					case 1865:
						return 4;
					case 1866:
						return 3;
					case 1867:
						break;
					case 1868:
						return 7;
					default:
						if (id_objeto != 2188)
						{
							goto IL_92;
						}
						break;
					}
					return 5;
				}
			}
			else
			{
				if (id_objeto == 2366)
				{
					return 9;
				}
				if (id_objeto != 6661 && id_objeto != 8541)
				{
					goto IL_92;
				}
			}
			return 2;
			IL_92:
			return 1;
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0000FA30 File Offset: 0x0000DE30
		public void limpiar()
		{
			this.interactivo_recolectando = null;
			this.interactivos_no_utilizables.Clear();
			this.robado = false;
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0000FA50 File Offset: 0x0000DE50
		~Recoleccion()
		{
			this.Dispose(false);
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0000FA80 File Offset: 0x0000DE80
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0000FA8C File Offset: 0x0000DE8C
		protected virtual void Dispose(bool disposing)
		{
			bool flag = !this.disposed;
			if (flag)
			{
				if (disposing)
				{
					this.pathfinder.Dispose();
				}
				this.interactivos_no_utilizables.Clear();
				this.interactivos_no_utilizables = null;
				this.interactivo_recolectando = null;
				this.pathfinder = null;
				this.cuenta = null;
				this.disposed = true;
			}
		}

		// Token: 0x040001DD RID: 477
		private Cuenta cuenta;

		// Token: 0x040001DE RID: 478
		private Mapa mapa;

		// Token: 0x040001DF RID: 479
		public ObjetoInteractivo interactivo_recolectando;

		// Token: 0x040001E0 RID: 480
		private List<int> interactivos_no_utilizables;

		// Token: 0x040001E1 RID: 481
		private bool robado;

		// Token: 0x040001E2 RID: 482
		private Pathfinder pathfinder;

		// Token: 0x040001E3 RID: 483
		private bool disposed;

		// Token: 0x040001E6 RID: 486
		public static readonly int[] herramientas_pescar = new int[]
		{
			8541,
			6661,
			596,
			1866,
			1865,
			1864,
			1867,
			2188,
			1863,
			1862,
			1868,
			1861,
			1860,
			2366
		};

		// Token: 0x040001E7 RID: 487
		private int contador_recoleccion = 0;
	}
}
