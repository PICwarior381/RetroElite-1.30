using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MoonSharp.Interpreter;
using RetroElite.Otros.Enums;
using RetroElite.Otros.Game.Entidades.Manejadores.Recolecciones;
using RetroElite.Otros.Game.Personaje;
using RetroElite.Otros.Mapas.Entidades;
using RetroElite.Otros.Scripts.Acciones;
using RetroElite.Otros.Scripts.Acciones.Mapas;
using RetroElite.Otros.Scripts.Acciones.Npcs;
using RetroElite.Utilidades;

namespace RetroElite.Otros.Scripts.Manejadores
{
	// Token: 0x02000015 RID: 21
	public class ManejadorAcciones : IDisposable
	{
		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060000FB RID: 251 RVA: 0x00005680 File Offset: 0x00003A80
		// (remove) Token: 0x060000FC RID: 252 RVA: 0x000056B8 File Offset: 0x00003AB8
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<bool> evento_accion_normal;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060000FD RID: 253 RVA: 0x000056F0 File Offset: 0x00003AF0
		// (remove) Token: 0x060000FE RID: 254 RVA: 0x00005728 File Offset: 0x00003B28
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<bool> evento_accion_personalizada;

		// Token: 0x060000FF RID: 255 RVA: 0x00005760 File Offset: 0x00003B60
		public ManejadorAcciones(Cuenta _cuenta, LuaManejadorScript _manejador_script)
		{
			this.cuenta = _cuenta;
			this.manejador_script = _manejador_script;
			this.fila_acciones = new ConcurrentQueue<AccionesScript>();
			this.timer_out = new TimerWrapper(60000, new TimerCallback(this.time_Out_Callback));
			PersonajeJuego personaje = this.cuenta.juego.personaje;
			this.cuenta.juego.mapa.mapa_actualizado += this.evento_Mapa_Cambiado;
			this.cuenta.juego.pelea.pelea_creada += this.get_Pelea_Creada;
			this.cuenta.juego.manejador.movimientos.movimiento_finalizado += this.evento_Movimiento_Celda;
			personaje.dialogo_npc_recibido += this.npcs_Dialogo_Recibido;
			personaje.dialogo_npc_acabado += this.npcs_Dialogo_Acabado;
			personaje.inventario.almacenamiento_abierto += this.iniciar_Almacenamiento;
			personaje.inventario.almacenamiento_cerrado += this.cerrar_Almacenamiento;
			this.cuenta.juego.manejador.recoleccion.recoleccion_iniciada += this.get_Recoleccion_Iniciada;
			this.cuenta.juego.manejador.recoleccion.recoleccion_acabada += this.get_Recoleccion_Acabada;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000058CC File Offset: 0x00003CCC
		private void evento_Mapa_Cambiado()
		{
			bool flag = !this.cuenta.script.corriendo || this.accion_actual == null;
			if (!flag)
			{
				this.mapa_cambiado = true;
				bool flag2 = !(this.accion_actual is PeleasAccion);
				if (flag2)
				{
					this.contador_peleas_mapa = 0;
				}
				bool flag3 = this.accion_actual is CambiarMapaAccion || this.accion_actual is PeleasAccion || this.accion_actual is RecoleccionAccion || this.coroutine_actual != null;
				if (flag3)
				{
					this.limpiar_Acciones();
					this.acciones_Salida(500);
				}
			}
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005970 File Offset: 0x00003D70
		private async void evento_Movimiento_Celda(bool es_correcto)
		{
			bool flag = !this.cuenta.script.corriendo;
			if (!flag)
			{
				bool flag2 = this.accion_actual is PeleasAccion;
				if (flag2)
				{
					if (es_correcto)
					{
						int delay = 0;
						while (delay < 10000 && this.cuenta.Estado_Cuenta != EstadoCuenta.LUCHANDO)
						{
							await Task.Delay(500);
							delay += 500;
						}
						if (this.cuenta.Estado_Cuenta != EstadoCuenta.LUCHANDO)
						{
							this.cuenta.logger.log_Peligro("SCRIPT", "Échec du combat, les monstres ont peut-être bougés ou déjà attaqués!");
							this.acciones_Salida(10);
						}
					}
				}
				else
				{
					MoverCeldaAccion celda = this.accion_actual as MoverCeldaAccion;
					if (celda != null)
					{
						if (es_correcto)
						{
							this.acciones_Salida(0);
						}
						else
						{
							this.cuenta.script.detener_Script("Erreur lors du deplacement a la cellule " + celda.celda_id.ToString());
						}
					}
					else if (this.accion_actual is CambiarMapaAccion && !es_correcto)
					{
						this.cuenta.script.detener_Script("Erreur lors du changement de map");
					}
					celda = null;
				}
			}
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000059B4 File Offset: 0x00003DB4
		private void get_Recoleccion_Iniciada()
		{
			bool flag = !this.cuenta.script.corriendo;
			if (!flag)
			{
				bool flag2 = this.accion_actual is RecoleccionAccion;
				if (flag2)
				{
				}
			}
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000059F0 File Offset: 0x00003DF0
		private void get_Recoleccion_Acabada(RecoleccionResultado resultado)
		{
			bool flag = !this.cuenta.script.corriendo;
			if (!flag)
			{
				bool flag2 = this.accion_actual is RecoleccionAccion;
				if (flag2)
				{
					if (resultado != RecoleccionResultado.FALLO)
					{
						this.acciones_Salida(600);
					}
					else
					{
						this.cuenta.script.detener_Script("Erreur recolte");
					}
				}
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00005A5C File Offset: 0x00003E5C
		private void get_Pelea_Creada()
		{
			bool flag = !this.cuenta.script.corriendo;
			if (!flag)
			{
				bool flag2 = this.accion_actual is PeleasAccion;
				if (flag2)
				{
					this.timer_out.Stop();
					this.contador_peleas_mapa++;
					this.contador_pelea++;
					bool flag3 = this.manejador_script.get_Global_Or<bool>("COMPTEUR_DE_MONSTRE", DataType.Boolean, false);
					if (flag3)
					{
						this.cuenta.logger.log_Script("SCRIPT", string.Format("Combat #{0}", this.contador_pelea));
					}
				}
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00005B00 File Offset: 0x00003F00
		private void npcs_Dialogo_Recibido()
		{
			bool flag = !this.cuenta.script.corriendo;
			if (!flag)
			{
				NpcBancoAccion npcBancoAccion = this.accion_actual as NpcBancoAccion;
				bool flag2 = npcBancoAccion != null;
				if (flag2)
				{
					bool flag3 = this.cuenta.Estado_Cuenta != EstadoCuenta.DIALOGANDO;
					if (!flag3)
					{
						IEnumerable<Npcs> source = this.cuenta.juego.mapa.lista_npcs();
						Npcs npcs = source.ElementAt((int)(this.cuenta.juego.personaje.hablando_npc_id * -1 - 1));
						this.cuenta.conexion.enviar_Paquete("DR" + npcs.pregunta.ToString() + "|" + npcs.respuestas[0].ToString(), true);
					}
				}
				else
				{
					bool flag4 = this.accion_actual is NpcAccion || this.accion_actual is RespuestaAccion;
					if (flag4)
					{
						this.acciones_Salida(400);
					}
				}
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00005C10 File Offset: 0x00004010
		private void npcs_Dialogo_Acabado()
		{
			bool flag = !this.cuenta.script.corriendo;
			if (!flag)
			{
				bool flag2 = this.accion_actual is RespuestaAccion || this.accion_actual is CerrarVentanaAccion;
				if (flag2)
				{
					this.acciones_Salida(200);
				}
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00005C68 File Offset: 0x00004068
		public void enqueue_Accion(AccionesScript accion, bool iniciar_dequeue_acciones = false)
		{
			this.fila_acciones.Enqueue(accion);
			if (iniciar_dequeue_acciones)
			{
				this.acciones_Salida(0);
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00005C90 File Offset: 0x00004090
		public void get_Funcion_Personalizada(DynValue coroutine)
		{
			bool flag = !this.cuenta.script.corriendo || this.coroutine_actual != null;
			if (!flag)
			{
				this.coroutine_actual = this.manejador_script.script.CreateCoroutine(coroutine);
				this.procesar_Coroutine();
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00005CE0 File Offset: 0x000040E0
		private void limpiar_Acciones()
		{
			bool flag;
			do
			{
				AccionesScript accionesScript;
				flag = this.fila_acciones.TryDequeue(out accionesScript);
			}
			while (flag);
			this.accion_actual = null;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00005D0C File Offset: 0x0000410C
		private void iniciar_Almacenamiento()
		{
			bool flag = !this.cuenta.script.corriendo;
			if (!flag)
			{
				bool flag2 = this.accion_actual is NpcBancoAccion;
				if (flag2)
				{
					this.acciones_Salida(400);
				}
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00005D54 File Offset: 0x00004154
		private void cerrar_Almacenamiento()
		{
			bool flag = !this.cuenta.script.corriendo;
			if (!flag)
			{
				bool flag2 = this.accion_actual is CerrarVentanaAccion;
				if (flag2)
				{
					this.acciones_Salida(400);
				}
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00005D9C File Offset: 0x0000419C
		private void procesar_Coroutine()
		{
			bool flag = !this.cuenta.script.corriendo;
			if (!flag)
			{
				try
				{
					DynValue dynValue = this.coroutine_actual.Coroutine.Resume();
					bool flag2 = dynValue.Type == DataType.Void;
					if (flag2)
					{
						this.acciones_Funciones_Finalizadas();
					}
				}
				catch (Exception ex)
				{
					this.cuenta.script.detener_Script(ex.ToString());
				}
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00005E1C File Offset: 0x0000421C
		private async Task procesar_Accion_Actual()
		{
			bool flag = !this.cuenta.script.corriendo;
			if (!flag)
			{
				string tipo = this.accion_actual.GetType().Name;
				switch (await this.accion_actual.proceso(this.cuenta))
				{
				case ResultadosAcciones.HECHO:
					this.acciones_Salida(100);
					break;
				case ResultadosAcciones.PROCESANDO:
					this.timer_out.Start(false);
					break;
				case ResultadosAcciones.FALLO:
					this.cuenta.logger.log_Peligro("SCRIPT", tipo + " erreur.");
					break;
				}
			}
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00005E64 File Offset: 0x00004264
		private void time_Out_Callback(object state)
		{
			bool flag = !this.cuenta.script.corriendo;
			if (!flag)
			{
				this.cuenta.logger.log_Peligro("SCRIPT", "Temps écoulé");
				this.cuenta.script.detener_Script("script");
				this.cuenta.script.activar_Script();
			}
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00005ED0 File Offset: 0x000042D0
		private void acciones_Finalizadas()
		{
			bool flag = this.mapa_cambiado;
			if (flag)
			{
				this.mapa_cambiado = false;
				Action<bool> action = this.evento_accion_normal;
				if (action != null)
				{
					action(true);
				}
			}
			else
			{
				Action<bool> action2 = this.evento_accion_normal;
				if (action2 != null)
				{
					action2(false);
				}
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00005F1C File Offset: 0x0000431C
		private void acciones_Funciones_Finalizadas()
		{
			this.coroutine_actual = null;
			bool flag = this.mapa_cambiado;
			if (flag)
			{
				this.mapa_cambiado = false;
				Action<bool> action = this.evento_accion_personalizada;
				if (action != null)
				{
					action(true);
				}
			}
			else
			{
				Action<bool> action2 = this.evento_accion_personalizada;
				if (action2 != null)
				{
					action2(false);
				}
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00005F6C File Offset: 0x0000436C
		private void acciones_Salida(int delay)
		{
			Task.Factory.StartNew<Task>(async delegate()
			{
				Cuenta cuenta = this.cuenta;
				bool flag = cuenta != null && !cuenta.script.corriendo;
				if (!flag)
				{
					bool habilitado = this.timer_out.habilitado;
					if (habilitado)
					{
						this.timer_out.Stop();
					}
					bool flag2 = delay > 0;
					if (flag2)
					{
						await Task.Delay(delay);
					}
					if (this.fila_acciones.Count > 0)
					{
						AccionesScript accion;
						if (this.fila_acciones.TryDequeue(out accion))
						{
							this.accion_actual = accion;
							await this.procesar_Accion_Actual();
						}
						accion = null;
					}
					else if (this.coroutine_actual != null)
					{
						this.procesar_Coroutine();
					}
					else
					{
						this.acciones_Finalizadas();
					}
				}
			}, TaskCreationOptions.LongRunning);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00005FA5 File Offset: 0x000043A5
		public void get_Borrar_Todo()
		{
			this.limpiar_Acciones();
			this.accion_actual = null;
			this.coroutine_actual = null;
			this.timer_out.Stop();
			this.contador_pelea = 0;
			this.contador_peleas_mapa = 0;
			this.contador_recoleccion = 0;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00005FDE File Offset: 0x000043DE
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00005FE8 File Offset: 0x000043E8
		~ManejadorAcciones()
		{
			this.Dispose(false);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00006018 File Offset: 0x00004418
		protected virtual void Dispose(bool disposing)
		{
			bool flag = !this.disposed;
			if (flag)
			{
				if (disposing)
				{
					this.timer_out.Dispose();
				}
				this.accion_actual = null;
				this.fila_acciones = null;
				this.cuenta = null;
				this.manejador_script = null;
				this.timer_out = null;
				this.disposed = true;
			}
		}

		// Token: 0x04000054 RID: 84
		private Cuenta cuenta;

		// Token: 0x04000055 RID: 85
		public LuaManejadorScript manejador_script;

		// Token: 0x04000056 RID: 86
		private ConcurrentQueue<AccionesScript> fila_acciones;

		// Token: 0x04000057 RID: 87
		private AccionesScript accion_actual;

		// Token: 0x04000058 RID: 88
		private DynValue coroutine_actual;

		// Token: 0x04000059 RID: 89
		private TimerWrapper timer_out;

		// Token: 0x0400005A RID: 90
		public int contador_pelea;

		// Token: 0x0400005B RID: 91
		public int contador_recoleccion;

		// Token: 0x0400005C RID: 92
		public int contador_peleas_mapa;

		// Token: 0x0400005D RID: 93
		private bool mapa_cambiado;

		// Token: 0x0400005E RID: 94
		private bool disposed;
	}
}
