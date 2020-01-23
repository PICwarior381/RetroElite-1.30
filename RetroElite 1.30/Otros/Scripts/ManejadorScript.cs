using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MoonSharp.Interpreter;
using RetroElite.Otros.Enums;
using RetroElite.Otros.Game.Personaje;
using RetroElite.Otros.Game.Personaje.Inventario;
using RetroElite.Otros.Scripts.Acciones;
using RetroElite.Otros.Scripts.Acciones.Almacenamiento;
using RetroElite.Otros.Scripts.Acciones.Global;
using RetroElite.Otros.Scripts.Acciones.Npcs;
using RetroElite.Otros.Scripts.Api;
using RetroElite.Otros.Scripts.Banderas;
using RetroElite.Otros.Scripts.Manejadores;
using RetroElite.Properties;
using RetroElite.Utilidades.Extensiones;

namespace RetroElite.Otros.Scripts
{
	// Token: 0x02000013 RID: 19
	public class ManejadorScript : IDisposable
	{
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000BC RID: 188 RVA: 0x000040E1 File Offset: 0x000024E1
		// (set) Token: 0x060000BD RID: 189 RVA: 0x000040E9 File Offset: 0x000024E9
		public ManejadorAcciones manejar_acciones { get; private set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060000BE RID: 190 RVA: 0x000040F2 File Offset: 0x000024F2
		// (set) Token: 0x060000BF RID: 191 RVA: 0x000040FA File Offset: 0x000024FA
		public bool activado { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00004103 File Offset: 0x00002503
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x0000410B File Offset: 0x0000250B
		public bool pausado { get; private set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00004114 File Offset: 0x00002514
		public bool corriendo
		{
			get
			{
				return this.activado && !this.pausado;
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060000C3 RID: 195 RVA: 0x0000412C File Offset: 0x0000252C
		// (remove) Token: 0x060000C4 RID: 196 RVA: 0x00004164 File Offset: 0x00002564
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<string> evento_script_cargado;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060000C5 RID: 197 RVA: 0x0000419C File Offset: 0x0000259C
		// (remove) Token: 0x060000C6 RID: 198 RVA: 0x000041D4 File Offset: 0x000025D4
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action evento_script_iniciado;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060000C7 RID: 199 RVA: 0x0000420C File Offset: 0x0000260C
		// (remove) Token: 0x060000C8 RID: 200 RVA: 0x00004244 File Offset: 0x00002644
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<string> evento_script_detenido;

		// Token: 0x060000C9 RID: 201 RVA: 0x0000427C File Offset: 0x0000267C
		public ManejadorScript(Cuenta _cuenta)
		{
			this.cuenta = _cuenta;
			this.manejador_script = new LuaManejadorScript();
			this.manejar_acciones = new ManejadorAcciones(this.cuenta, this.manejador_script);
			this.banderas = new List<Bandera>();
			this.api = new API(this.cuenta, this.manejar_acciones);
			this.manejar_acciones.evento_accion_normal += this.get_Accion_Finalizada;
			this.manejar_acciones.evento_accion_personalizada += this.get_Accion_Personalizada_Finalizada;
			this.cuenta.juego.pelea.pelea_creada += this.get_Pelea_Creada;
			this.cuenta.juego.pelea.pelea_acabada += this.get_Pelea_Acabada;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00004358 File Offset: 0x00002758
		public void get_Desde_Archivo(string ruta_archivo)
		{
			bool activado = this.activado;
			if (activado)
			{
				throw new Exception("Un script est déjà en cours d'exécution.");
			}
			bool flag = !File.Exists(ruta_archivo) || !ruta_archivo.EndsWith(".lua");
			if (flag)
			{
				throw new Exception("Fichier non trouvé ou non valide.");
			}
			this.manejador_script.cargar_Desde_Archivo(ruta_archivo, new Action(this.funciones_Personalizadas));
			Action<string> action = this.evento_script_cargado;
			if (action != null)
			{
				action(Path.GetFileNameWithoutExtension(ruta_archivo));
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000043D4 File Offset: 0x000027D4
		private void funciones_Personalizadas()
		{
			this.manejador_script.Set_Global("api", this.api);
			this.manejador_script.Set_Global("personnage", this.api.personnage);
			this.manejador_script.Set_Global("message", new Action<string>(delegate(string mensaje)
			{
				this.cuenta.logger.log_Script("SCRIPT", mensaje);
			}));
			this.manejador_script.Set_Global("messageErreur", new Action<string>(delegate(string mensaje)
			{
				this.cuenta.logger.log_Error("SCRIPT", mensaje);
			}));
			this.manejador_script.Set_Global("stopScript", new Action(delegate
			{
				this.detener_Script("script");
			}));
			this.manejador_script.Set_Global("delayFuncion", new Action<int>(delegate(int ms)
			{
				this.manejar_acciones.enqueue_Accion(new DelayAccion(ms), true);
			}));
			this.manejador_script.Set_Global("Entrain de recolter", new Func<bool>(this.cuenta.esta_recolectando));
			this.manejador_script.Set_Global("Entrain de parler", new Func<bool>(this.cuenta.esta_dialogando));
			this.manejador_script.script.DoString(Resources.api_ayuda, null, null);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000044E8 File Offset: 0x000028E8
		public void activar_Script()
		{
			bool flag = this.activado || this.cuenta.esta_ocupado();
			if (!flag)
			{
				this.activado = true;
				Action action = this.evento_script_iniciado;
				if (action != null)
				{
					action();
				}
				this.estado_script = EstadoScript.DEPLACEMENT;
				this.iniciar_Script();
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0000453C File Offset: 0x0000293C
		public void detener_Script(string mensaje = "script")
		{
			bool flag = !this.activado;
			if (!flag)
			{
				this.activado = false;
				this.pausado = false;
				this.banderas.Clear();
				this.bandera_id = 0;
				this.manejar_acciones.get_Borrar_Todo();
				Action<string> action = this.evento_script_detenido;
				if (action != null)
				{
					action(mensaje);
				}
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000459B File Offset: 0x0000299B
		private void iniciar_Script()
		{
			Task.Run(async delegate()
			{
				bool flag = !this.corriendo;
				if (!flag)
				{
					try
					{
						Table mapas_dung = this.manejador_script.get_Global_Or<Table>("MAP_DONJON", DataType.Table, null);
						bool flag2 = mapas_dung != null;
						if (flag2)
						{
							IEnumerable<int> test = from m in mapas_dung.Values
							where m.Type == DataType.Number
							select m into n
							select (int)n.Number;
							bool flag3 = test.Contains(this.cuenta.juego.mapa.id);
							if (flag3)
							{
								this.es_dung = true;
							}
							test = null;
						}
						await this.aplicar_Comprobaciones();
						if (this.corriendo)
						{
							IEnumerable<Table> entradas = this.manejador_script.get_Entradas_Funciones(this.estado_script.ToString().ToLower());
							if (entradas == null)
							{
								this.detener_Script("La fonction " + this.estado_script.ToString().ToLower() + " n'est pas présente dans le script, erreur.");
							}
							else
							{
								foreach (Table entrada in entradas)
								{
									if (entrada["map"] != null)
									{
										if (this.cuenta.juego.mapa.esta_En_Mapa(entrada["map"].ToString()))
										{
											this.procesar_Entradas(entrada);
											this.procesar_Actual_Entrada(null);
											return;
										}
									}
								}
								IEnumerator<Table> enumerator = null;
								this.detener_Script("Aucun action restante à faire dans le script ..");
								mapas_dung = null;
								entradas = null;
							}
						}
					}
					catch (Exception ex)
					{
						this.cuenta.logger.log_Error("[SCRIPT]", ex.ToString());
						this.detener_Script("script");
					}
				}
			});
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000045B0 File Offset: 0x000029B0
		private async Task aplicar_Comprobaciones()
		{
			await this.verificar_Muerte();
			if (this.corriendo)
			{
				await this.get_Verificar_Script_Regeneracion();
				if (this.corriendo)
				{
					await this.get_Verificar_Regeneracion();
					if (this.corriendo)
					{
						await this.get_Verificar_Sacos();
						if (this.corriendo)
						{
							this.verificar_Maximos_Pods();
						}
					}
				}
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000045F8 File Offset: 0x000029F8
		private async Task verificar_Muerte()
		{
			bool flag = this.cuenta.juego.personaje.caracteristicas.energia_actual == 0;
			if (flag)
			{
				this.cuenta.logger.log_Script("SCRIPT", "Euh .. Tu es mort ... Go au phénix !");
				this.estado_script = EstadoScript.PHENIX;
			}
			await Task.Delay(50);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004640 File Offset: 0x00002A40
		private void verificar_Maximos_Pods()
		{
			bool flag = !this.get_Maximos_Pods();
			if (!flag)
			{
				bool flag2 = !this.es_dung && this.estado_script != EstadoScript.BANQUE;
				if (flag2)
				{
					bool flag3 = !this.corriendo;
					if (!flag3)
					{
						this.cuenta.logger.log_Script("SCRIPT", "Tu es Full POD ! Go à la banque");
						this.estado_script = EstadoScript.BANQUE;
					}
				}
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000046AC File Offset: 0x00002AAC
		private bool get_Maximos_Pods()
		{
			int num = this.manejador_script.get_Global_Or<int>("MAX_PODS", DataType.Number, 90);
			return this.cuenta.juego.personaje.inventario.porcentaje_pods >= num;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000046F4 File Offset: 0x00002AF4
		private void procesar_Entradas(Table valor)
		{
			this.banderas.Clear();
			this.bandera_id = 0;
			DynValue dynValue = valor.Get("custom");
			bool flag = !dynValue.IsNil() && dynValue.Type == DataType.Function;
			if (flag)
			{
				this.banderas.Add(new FuncionPersonalizada(dynValue));
			}
			bool flag2 = this.estado_script == EstadoScript.DEPLACEMENT;
			if (flag2)
			{
				dynValue = valor.Get("recolter");
				bool flag3 = !dynValue.IsNil() && dynValue.Type == DataType.Boolean && dynValue.Boolean;
				if (flag3)
				{
					this.banderas.Add(new RecoleccionBandera());
				}
				dynValue = valor.Get("combattre");
				bool flag4 = !dynValue.IsNil() && dynValue.Type == DataType.Boolean && dynValue.Boolean;
				if (flag4)
				{
					this.banderas.Add(new PeleaBandera());
				}
			}
			bool flag5 = this.estado_script == EstadoScript.BANQUE;
			if (flag5)
			{
				dynValue = valor.Get("npc_banque");
				bool flag6 = !dynValue.IsNil() && dynValue.Type == DataType.Boolean && dynValue.Boolean;
				if (flag6)
				{
					this.banderas.Add(new NPCBancoBandera());
				}
			}
			dynValue = valor.Get("cell");
			bool flag7 = !dynValue.IsNil() && dynValue.Type == DataType.String;
			if (flag7)
			{
				this.banderas.Add(new CambiarMapa(dynValue.String));
			}
			bool flag8 = this.banderas.Count == 0;
			if (flag8)
			{
				this.detener_Script("Plus aucune action à exécuté dans le script");
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004880 File Offset: 0x00002C80
		private void procesar_Actual_Entrada(AccionesScript tiene_accion_disponible = null)
		{
			bool flag = !this.corriendo;
			if (!flag)
			{
				Bandera bandera = this.banderas[this.bandera_id];
				Bandera bandera2 = bandera;
				Bandera bandera3 = bandera2;
				if (!(bandera3 is RecoleccionBandera))
				{
					if (!(bandera3 is PeleaBandera))
					{
						if (!(bandera3 is NPCBancoBandera))
						{
							FuncionPersonalizada funcionPersonalizada = bandera3 as FuncionPersonalizada;
							if (funcionPersonalizada == null)
							{
								CambiarMapa cambiarMapa = bandera3 as CambiarMapa;
								if (cambiarMapa != null)
								{
									this.manejar_Cambio_Mapa(cambiarMapa);
								}
							}
							else
							{
								this.manejar_acciones.get_Funcion_Personalizada(funcionPersonalizada.funcion);
							}
						}
						else
						{
							this.manejar_Npc_Banco_Bandera();
						}
					}
					else
					{
						this.manejar_Pelea_mapa(tiene_accion_disponible as PeleasAccion);
					}
				}
				else
				{
					this.manejar_Recoleccion_Bandera(tiene_accion_disponible as RecoleccionAccion);
				}
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004938 File Offset: 0x00002D38
		private void manejar_Recoleccion_Bandera(RecoleccionAccion accion_recoleccion)
		{
			RecoleccionAccion recoleccionAccion = accion_recoleccion ?? this.crear_Accion_Recoleccion();
			bool flag = recoleccionAccion == null;
			if (!flag)
			{
				bool flag2 = this.cuenta.juego.manejador.recoleccion.get_Puede_Recolectar(recoleccionAccion.elementos);
				if (flag2)
				{
					this.manejar_acciones.enqueue_Accion(recoleccionAccion, true);
				}
				else
				{
					this.procesar_Actual_Bandera();
				}
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004998 File Offset: 0x00002D98
		private RecoleccionAccion crear_Accion_Recoleccion()
		{
			Table table = this.manejador_script.get_Global_Or<Table>("RESSOURCE_RECOLTABLE", DataType.Table, null);
			List<short> list = new List<short>();
			bool flag = table != null;
			if (flag)
			{
				foreach (DynValue dynValue in table.Values)
				{
					bool flag2 = dynValue.Type != DataType.Number;
					if (!flag2)
					{
						bool flag3 = this.cuenta.juego.personaje.get_Tiene_Skill_Id((int)dynValue.Number);
						if (flag3)
						{
							list.Add((short)dynValue.Number);
						}
					}
				}
			}
			bool flag4 = list.Count == 0;
			if (flag4)
			{
				list.AddRange(this.cuenta.juego.personaje.get_Skills_Recoleccion_Disponibles());
			}
			bool flag5 = list.Count == 0;
			RecoleccionAccion result;
			if (flag5)
			{
				this.cuenta.script.detener_Script("Liste de ressources vide, ou vous n'avez pas de métiers disponibles");
				result = null;
			}
			else
			{
				result = new RecoleccionAccion(list);
			}
			return result;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004AB0 File Offset: 0x00002EB0
		private void manejar_Npc_Banco_Bandera()
		{
			this.manejar_acciones.enqueue_Accion(new NpcBancoAccion(-1), false);
			this.manejar_acciones.enqueue_Accion(new AlmacenarTodosLosObjetosAccion(), false);
			Table table = this.manejador_script.get_Global_Or<Table>("ITEM_A_RECUPERER_BANQUE", DataType.Table, null);
			bool flag = table != null;
			if (flag)
			{
				foreach (DynValue dynValue in table.Values)
				{
					bool flag2 = dynValue.Type != DataType.Table;
					if (!flag2)
					{
						DynValue dynValue2 = dynValue.Table.Get("objet");
						DynValue dynValue3 = dynValue.Table.Get("quantite");
						bool flag3 = dynValue2.IsNil() || dynValue2.Type != DataType.Number || dynValue3.IsNil() || dynValue3.Type != DataType.Number;
						if (flag3)
						{
						}
					}
				}
			}
			this.manejar_acciones.enqueue_Accion(new CerrarVentanaAccion(), true);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004BC0 File Offset: 0x00002FC0
		private void manejar_Cambio_Mapa(CambiarMapa mapa)
		{
			CambiarMapaAccion accion;
			bool flag = CambiarMapaAccion.TryParse(mapa.celda_id, out accion);
			if (flag)
			{
				this.manejar_acciones.enqueue_Accion(accion, true);
			}
			else
			{
				this.detener_Script("La cellule est invalide pour changer de map");
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00004BFC File Offset: 0x00002FFC
		private async Task get_Verificar_Sacos()
		{
			bool flag = !this.manejador_script.get_Global_Or<bool>("OUVERTURE_DES_SACS", DataType.Boolean, false);
			if (!flag)
			{
				PersonajeJuego personaje = this.cuenta.juego.personaje;
				List<ObjetosInventario> sacos = (from o in personaje.inventario.objetos
				where o.tipo == 100
				select o).ToList<ObjetosInventario>();
				bool flag2 = sacos.Count > 0;
				if (flag2)
				{
					foreach (ObjetosInventario saco in sacos)
					{
						personaje.inventario.utilizar_Objeto(saco);
						await Task.Delay(500);
						saco = null;
					}
					List<ObjetosInventario>.Enumerator enumerator = default(List<ObjetosInventario>.Enumerator);
					this.cuenta.logger.log_Script("SCRIPT", string.Format("{0} sac de ressource ouvert !", sacos.Count));
				}
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004C44 File Offset: 0x00003044
		private void manejar_Pelea_mapa(PeleasAccion pelea_accion)
		{
			PeleasAccion peleasAccion = pelea_accion ?? this.get_Crear_Pelea_Accion();
			int num = this.manejador_script.get_Global_Or<int>("NOMBRE_COMBAT_MAX_MAP", DataType.Number, -1);
			bool flag = num != -1 && this.manejar_acciones.contador_peleas_mapa >= num;
			if (flag)
			{
				this.cuenta.logger.log_Script("SCRIPT", "Limite de combat sur la map atteint");
				IEnumerable<Table> enumerable = this.manejador_script.get_Entradas_Funciones(this.estado_script.ToString().ToLower());
				foreach (Table table in enumerable)
				{
					bool flag2 = table["map"] == null;
					if (!flag2)
					{
						bool flag3 = this.cuenta.juego.mapa.esta_En_Mapa(table["map"].ToString());
						if (flag3)
						{
							DynValue dynValue = table.Get("cell");
							this.manejar_Cambio_Mapa(new CambiarMapa(dynValue.String));
							break;
						}
					}
				}
			}
			else
			{
				bool flag4 = !this.es_dung && !this.cuenta.juego.mapa.get_Puede_Luchar_Contra_Grupo_Monstruos(peleasAccion.monstruos_minimos, peleasAccion.monstruos_maximos, peleasAccion.monstruo_nivel_minimo, peleasAccion.monstruo_nivel_maximo, peleasAccion.monstruos_prohibidos, peleasAccion.monstruos_obligatorios);
				if (flag4)
				{
					this.cuenta.logger.log_Script("SCRIPT", "Aucun groupe de monstre disponible sur la map");
					this.procesar_Actual_Bandera();
				}
				else
				{
					while (this.es_dung && !this.cuenta.juego.mapa.get_Puede_Luchar_Contra_Grupo_Monstruos(peleasAccion.monstruos_minimos, peleasAccion.monstruos_maximos, peleasAccion.monstruo_nivel_minimo, peleasAccion.monstruo_nivel_maximo, peleasAccion.monstruos_prohibidos, peleasAccion.monstruos_obligatorios))
					{
						peleasAccion = this.get_Crear_Pelea_Accion();
					}
					this.manejar_acciones.enqueue_Accion(peleasAccion, true);
				}
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004E54 File Offset: 0x00003254
		private async Task get_Verificar_Regeneracion()
		{
			bool flag = this.cuenta.pelea_extension.configuracion.iniciar_regeneracion == 0;
			if (!flag)
			{
				bool flag2 = this.cuenta.pelea_extension.configuracion.detener_regeneracion <= this.cuenta.pelea_extension.configuracion.iniciar_regeneracion;
				if (!flag2)
				{
					bool flag3 = this.cuenta.juego.personaje.caracteristicas.porcentaje_vida <= (int)this.cuenta.pelea_extension.configuracion.iniciar_regeneracion;
					if (flag3)
					{
						int vida_final = (int)this.cuenta.pelea_extension.configuracion.detener_regeneracion * this.cuenta.juego.personaje.caracteristicas.vitalidad_maxima / 100;
						int vida_para_regenerar = vida_final - this.cuenta.juego.personaje.caracteristicas.vitalidad_actual;
						bool flag4 = vida_para_regenerar > 0;
						if (flag4)
						{
							int tiempo_estimado = vida_para_regenerar / 2;
							bool flag5 = this.cuenta.Estado_Cuenta != EstadoCuenta.REGENERANDO;
							if (flag5)
							{
								bool flag6 = this.cuenta.esta_ocupado();
								if (flag6)
								{
									return;
								}
								this.cuenta.conexion.enviar_Paquete("eU1", true);
							}
							this.cuenta.logger.log_Script("[SCRIPT]", string.Format("Début regénération, point de vie a récupéré : {0}, temps estimé : {1} secondes.", vida_para_regenerar, tiempo_estimado));
							int i = 0;
							while (i < tiempo_estimado && this.cuenta.juego.personaje.caracteristicas.porcentaje_vida <= (int)this.cuenta.pelea_extension.configuracion.detener_regeneracion && this.corriendo)
							{
								await Task.Delay(1000);
								i++;
							}
							if (this.corriendo)
							{
								if (this.cuenta.Estado_Cuenta == EstadoCuenta.REGENERANDO)
								{
									this.cuenta.conexion.enviar_Paquete("eU1", true);
								}
								this.cuenta.logger.log_Script("[SCRIPT]", "Regénération ok.");
							}
						}
					}
				}
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004E9C File Offset: 0x0000329C
		private async Task get_Verificar_Script_Regeneracion()
		{
			Table auto_regeneracion = this.manejador_script.get_Global_Or<Table>("AUTO_REGENERATION", DataType.Table, null);
			bool flag = auto_regeneracion == null;
			if (!flag)
			{
				PersonajeJuego personaje = this.cuenta.juego.personaje;
				int vida_minima = auto_regeneracion.get_Or("VIE_MIN", DataType.Number, 0);
				int vida_maxima = auto_regeneracion.get_Or("VIE_MAX", DataType.Number, 100);
				bool flag2 = vida_minima == 0 || personaje.caracteristicas.porcentaje_vida > vida_minima;
				if (!flag2)
				{
					int fin_vida = vida_maxima * personaje.caracteristicas.vitalidad_maxima / 100;
					int vida_para_regenerar = fin_vida - personaje.caracteristicas.vitalidad_actual;
					List<int> objetos = auto_regeneracion.Get("OBJETS").ToObject<List<int>>();
					foreach (int id_objeto in objetos)
					{
						bool flag3 = vida_para_regenerar < 20;
						if (flag3)
						{
							break;
						}
						ObjetosInventario objeto = personaje.inventario.get_Objeto_Modelo_Id(id_objeto);
						bool flag4 = objeto == null;
						if (!flag4)
						{
							bool flag5 = objeto.vida_regenerada <= 0;
							if (!flag5)
							{
								int cantidad_necesaria = (int)Math.Floor((double)vida_para_regenerar / (double)objeto.vida_regenerada);
								int cantidad_correcta = Math.Min(cantidad_necesaria, objeto.cantidad);
								for (int i = 0; i < cantidad_correcta; i++)
								{
									personaje.inventario.utilizar_Objeto(objeto);
									await Task.Delay(800);
								}
								vida_para_regenerar -= (int)objeto.vida_regenerada * cantidad_correcta;
								objeto = null;
							}
						}
					}
					List<int>.Enumerator enumerator = default(List<int>.Enumerator);
				}
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004EE4 File Offset: 0x000032E4
		private void procesar_Actual_Bandera()
		{
			bool flag = !this.corriendo;
			if (!flag)
			{
				bool flag2 = !this.es_dung && this.get_Maximos_Pods();
				if (flag2)
				{
					this.iniciar_Script();
				}
				else
				{
					Bandera bandera = this.banderas[this.bandera_id];
					Bandera bandera2 = bandera;
					if (!(bandera2 is RecoleccionBandera))
					{
						if (bandera2 is PeleaBandera)
						{
							PeleasAccion crear_Pelea_Accion = this.get_Crear_Pelea_Accion();
							bool flag3 = this.cuenta.juego.mapa.get_Puede_Luchar_Contra_Grupo_Monstruos(crear_Pelea_Accion.monstruos_minimos, crear_Pelea_Accion.monstruos_maximos, crear_Pelea_Accion.monstruo_nivel_minimo, crear_Pelea_Accion.monstruo_nivel_maximo, crear_Pelea_Accion.monstruos_prohibidos, crear_Pelea_Accion.monstruos_obligatorios);
							if (flag3)
							{
								this.procesar_Actual_Entrada(crear_Pelea_Accion);
								return;
							}
						}
					}
					else
					{
						RecoleccionAccion recoleccionAccion = this.crear_Accion_Recoleccion();
						bool flag4 = this.cuenta.juego.manejador.recoleccion.get_Puede_Recolectar(recoleccionAccion.elementos);
						if (flag4)
						{
							this.procesar_Actual_Entrada(recoleccionAccion);
							return;
						}
					}
					this.bandera_id++;
					bool flag5 = this.bandera_id == this.banderas.Count;
					if (flag5)
					{
						this.detener_Script("Plus aucune action sur le map");
					}
					else
					{
						this.procesar_Actual_Entrada(null);
					}
				}
			}
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00005024 File Offset: 0x00003424
		private PeleasAccion get_Crear_Pelea_Accion()
		{
			int monstruos_minimos = this.manejador_script.get_Global_Or<int>("MONSTRE_MIN", DataType.Number, 1);
			int monstruos_maximos = this.manejador_script.get_Global_Or<int>("MONSTRE_MAX", DataType.Number, 8);
			int monstruo_nivel_minimo = this.manejador_script.get_Global_Or<int>("LEVEL_MIN_GROUPE_MONSTRE", DataType.Number, 1);
			int monstruo_nivel_maximo = this.manejador_script.get_Global_Or<int>("LEVEL_MAX_GROUPE_MONSTRE", DataType.Number, 1000);
			List<int> list = new List<int>();
			List<int> list2 = new List<int>();
			Table table = this.manejador_script.get_Global_Or<Table>("MONSTRE_INTERDIT", DataType.Table, null);
			bool flag = table != null;
			if (flag)
			{
				foreach (DynValue dynValue in table.Values)
				{
					bool flag2 = dynValue.Type != DataType.Number;
					if (!flag2)
					{
						list.Add((int)dynValue.Number);
					}
				}
			}
			table = this.manejador_script.get_Global_Or<Table>("MONSTRE_OBLIGATOIRE", DataType.Table, null);
			bool flag3 = table != null;
			if (flag3)
			{
				foreach (DynValue dynValue2 in table.Values)
				{
					bool flag4 = dynValue2.Type != DataType.Number;
					if (!flag4)
					{
						list2.Add((int)dynValue2.Number);
					}
				}
			}
			return new PeleasAccion(monstruos_minimos, monstruos_maximos, monstruo_nivel_minimo, monstruo_nivel_maximo, list, list2);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000051B4 File Offset: 0x000035B4
		private bool verificar_Acciones_Especiales()
		{
			bool flag = this.estado_script == EstadoScript.BANQUE && !this.get_Maximos_Pods();
			bool result;
			if (flag)
			{
				this.estado_script = EstadoScript.DEPLACEMENT;
				this.iniciar_Script();
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000051F4 File Offset: 0x000035F4
		private void get_Accion_Finalizada(bool mapa_cambiado)
		{
			bool flag = this.verificar_Acciones_Especiales();
			if (!flag)
			{
				if (mapa_cambiado)
				{
					this.iniciar_Script();
				}
				else
				{
					this.procesar_Actual_Bandera();
				}
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00005224 File Offset: 0x00003624
		private void get_Accion_Personalizada_Finalizada(bool mapa_cambiado)
		{
			bool flag = this.verificar_Acciones_Especiales();
			if (!flag)
			{
				if (mapa_cambiado)
				{
					this.iniciar_Script();
				}
				else
				{
					this.procesar_Actual_Bandera();
				}
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00005254 File Offset: 0x00003654
		private void get_Pelea_Creada()
		{
			bool flag = !this.activado;
			if (!flag)
			{
				this.pausado = true;
				this.cuenta.juego.manejador.recoleccion.get_Cancelar_Interactivo();
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00005294 File Offset: 0x00003694
		private void get_Pelea_Acabada()
		{
			bool flag = !this.activado;
			if (!flag)
			{
				this.pausado = false;
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000052B9 File Offset: 0x000036B9
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000052C4 File Offset: 0x000036C4
		~ManejadorScript()
		{
			this.Dispose(false);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000052F4 File Offset: 0x000036F4
		public virtual void Dispose(bool disposing)
		{
			bool flag = this.disposed;
			if (flag)
			{
				if (disposing)
				{
					this.manejador_script.Dispose();
					this.api.Dispose();
					this.manejar_acciones.Dispose();
				}
				this.manejar_acciones = null;
				this.manejador_script = null;
				this.api = null;
				this.activado = false;
				this.pausado = false;
				this.cuenta = null;
				this.disposed = true;
			}
		}

		// Token: 0x04000044 RID: 68
		private Cuenta cuenta;

		// Token: 0x04000045 RID: 69
		private LuaManejadorScript manejador_script;

		// Token: 0x04000047 RID: 71
		private EstadoScript estado_script;

		// Token: 0x04000048 RID: 72
		private List<Bandera> banderas;

		// Token: 0x04000049 RID: 73
		private int bandera_id;

		// Token: 0x0400004A RID: 74
		private API api;

		// Token: 0x0400004B RID: 75
		private bool es_dung = false;

		// Token: 0x0400004C RID: 76
		private bool disposed;
	}
}
