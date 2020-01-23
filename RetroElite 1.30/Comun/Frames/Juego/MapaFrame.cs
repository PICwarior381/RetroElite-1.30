using System;
using System.Threading.Tasks;
using RetroElite.Comun.Frames.Transporte;
using RetroElite.Comun.Network;
using RetroElite.Otros;
using RetroElite.Otros.Enums;
using RetroElite.Otros.Game.Entidades.Manejadores.Recolecciones;
using RetroElite.Otros.Game.Personaje;
using RetroElite.Otros.Mapas;
using RetroElite.Otros.Mapas.Entidades;
using RetroElite.Otros.Peleas;
using RetroElite.Otros.Peleas.Enums;
using RetroElite.Otros.Peleas.Peleadores;
using RetroElite.Utilidades.Configuracion;
using RetroElite.Utilidades.Criptografia;

namespace RetroElite.Comun.Frames.Juego
{
	// Token: 0x0200008F RID: 143
	internal class MapaFrame : Frame
	{
		// Token: 0x060005D0 RID: 1488 RVA: 0x000263E8 File Offset: 0x000247E8
		[PaqueteAtributo("GM")]
		public async Task get_Movimientos_Personajes(ClienteTcp cliente, string paquete)
		{
			Cuenta cuenta = cliente.cuenta;
			string[] separador_jugadores = paquete.Substring(3).Split(new char[]
			{
				'|'
			});
			int num;
			for (int i = 0; i < separador_jugadores.Length; i = num)
			{
				string _loc6 = separador_jugadores[i];
				bool flag = _loc6.Length != 0;
				if (flag)
				{
					string[] informaciones = _loc6.Substring(1).Split(new char[]
					{
						';'
					});
					bool flag2 = _loc6[0].Equals('+');
					if (flag2)
					{
						Celda celda = cuenta.juego.mapa.get_Celda_Id(short.Parse(informaciones[0]));
						Pelea pelea = cuenta.juego.pelea;
						int id = int.Parse(informaciones[3]);
						string nombre_template = informaciones[4];
						string tipo = informaciones[5];
						bool flag3 = tipo.Contains(",");
						if (flag3)
						{
							tipo = tipo.Split(new char[]
							{
								','
							})[0];
						}
						string[] templates;
						string[] niveles;
						Monstruos monstruo;
						switch (int.Parse(tipo))
						{
						case -10:
						case -9:
						case -8:
						case -7:
						case -6:
						case -5:
							break;
						case -4:
							cuenta.juego.mapa.entidades.TryAdd(id, new Npcs(id, int.Parse(nombre_template), celda));
							break;
						case -3:
							templates = nombre_template.Split(new char[]
							{
								','
							});
							niveles = informaciones[7].Split(new char[]
							{
								','
							});
							monstruo = new Monstruos(id, int.Parse(templates[0]), celda, int.Parse(niveles[0]));
							monstruo.lider_grupo = monstruo;
							for (int j = 1; j < templates.Length; j = num)
							{
								monstruo.moobs_dentro_grupo.Add(new Monstruos(id, int.Parse(templates[j]), celda, int.Parse(niveles[j])));
								num = j + 1;
							}
							cuenta.juego.mapa.entidades.TryAdd(id, monstruo);
							break;
						case -2:
						case -1:
						{
							bool flag4 = cuenta.Estado_Cuenta == EstadoCuenta.LUCHANDO;
							if (flag4)
							{
								int vida = int.Parse(informaciones[12]);
								byte pa = byte.Parse(informaciones[13]);
								byte pm = byte.Parse(informaciones[14]);
								byte equipo = byte.Parse(informaciones[15]);
								pelea.get_Agregar_Luchador(new Luchadores(id, true, vida, pa, pm, celda, vida, equipo));
							}
							break;
						}
						default:
						{
							bool flag5 = cuenta.Estado_Cuenta != EstadoCuenta.LUCHANDO;
							if (flag5)
							{
								bool flag6 = cuenta.juego.personaje.id != id;
								if (flag6)
								{
									cuenta.juego.mapa.entidades.TryAdd(id, new Personajes(id, nombre_template, byte.Parse(informaciones[7].ToString()), celda));
								}
								else
								{
									cuenta.juego.personaje.celda = celda;
								}
							}
							else
							{
								int vida2 = int.Parse(informaciones[14]);
								byte pa2 = byte.Parse(informaciones[15]);
								byte pm2 = byte.Parse(informaciones[16]);
								byte equipo2 = byte.Parse(informaciones[24]);
								pelea.get_Agregar_Luchador(new Luchadores(id, true, vida2, pa2, pm2, celda, vida2, equipo2));
								bool flag7 = cuenta.juego.personaje.id == id && cuenta.pelea_extension.configuracion.posicionamiento != PosicionamientoInicioPelea.INMOVIL;
								if (flag7)
								{
									await Task.Delay(300);
									short celda_posicion = pelea.get_Celda_Mas_Cercana_O_Lejana(cuenta.pelea_extension.configuracion.posicionamiento == PosicionamientoInicioPelea.CERCA_DE_ENEMIGOS, pelea.celdas_preparacion);
									if (celda_posicion != celda.id)
									{
										cuenta.conexion.enviar_Paquete("Gp" + celda_posicion.ToString(), true);
									}
									else
									{
										cuenta.conexion.enviar_Paquete("GR1", false);
									}
								}
								else if (cuenta.juego.personaje.id == id)
								{
									await Task.Delay(300);
									cuenta.conexion.enviar_Paquete("GR1", false);
								}
							}
							break;
						}
						}
						templates = null;
						niveles = null;
						monstruo = null;
						celda = null;
						pelea = null;
					}
					else if (_loc6[0].Equals('-'))
					{
						if (cuenta.Estado_Cuenta != EstadoCuenta.LUCHANDO)
						{
							int id2 = int.Parse(_loc6.Substring(1));
							Entidad entidad;
							cuenta.juego.mapa.entidades.TryRemove(id2, out entidad);
							entidad = null;
						}
					}
				}
				num = i + 1;
			}
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00026440 File Offset: 0x00024840
		[PaqueteAtributo("GAF")]
		public void get_Finalizar_Accion(ClienteTcp cliente, string paquete)
		{
			string[] array = paquete.Substring(3).Split(new char[]
			{
				'|'
			});
			cliente.cuenta.conexion.enviar_Paquete("GKK" + array[0], false);
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x00026488 File Offset: 0x00024888
		[PaqueteAtributo("GAS")]
		public async Task get_Inicio_Accion(ClienteTcp cliente, string paquete)
		{
			await Task.Delay(200);
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x000264E0 File Offset: 0x000248E0
		[PaqueteAtributo("GA")]
		public async Task get_Iniciar_Accion(ClienteTcp cliente, string paquete)
		{
			string[] separador = paquete.Substring(2).Split(new char[]
			{
				';'
			});
			int id_accion = int.Parse(separador[1]);
			Cuenta cuenta = cliente.cuenta;
			PersonajeJuego personaje = cuenta.juego.personaje;
			bool flag = id_accion > 0;
			if (flag)
			{
				int id_entidad = int.Parse(separador[2]);
				Mapa mapa = cuenta.juego.mapa;
				Pelea pelea = cuenta.juego.pelea;
				int num = id_accion;
				int num2 = num;
				int num3 = num2;
				Celda celda;
				Luchadores luchador;
				if (num3 <= 151)
				{
					if (num3 <= 102)
					{
						switch (num3)
						{
						case 1:
						{
							celda = mapa.get_Celda_Id(Hash.get_Celda_Id_Desde_hash(separador[3].Substring(separador[3].Length - 2)));
							bool flag2 = !cuenta.esta_luchando();
							if (flag2)
							{
								bool flag3 = id_entidad == personaje.id && celda.id > 0 && personaje.celda.id != celda.id;
								if (flag3)
								{
									byte tipo_gkk_movimiento = byte.Parse(separador[0]);
									await cuenta.juego.manejador.movimientos.evento_Movimiento_Finalizado(celda, tipo_gkk_movimiento, true);
								}
								else
								{
									Entidad entidad;
									if (mapa.entidades.TryGetValue(id_entidad, out entidad))
									{
										entidad.celda = celda;
										if (GlobalConf.mostrar_mensajes_debug)
										{
											cuenta.logger.log_informacion("DEBUG", "Detectado movimiento de una entidad a la casilla: " + celda.id.ToString());
										}
									}
									entidad = null;
								}
								mapa.evento_Entidad_Actualizada();
							}
							else
							{
								luchador = pelea.get_Luchador_Por_Id(id_entidad);
								if (luchador != null)
								{
									luchador.celda = celda;
									if (luchador.id == personaje.id)
									{
										byte tipo_gkk_movimiento = byte.Parse(separador[0]);
										await Task.Delay(400 + 100 * personaje.celda.get_Distancia_Entre_Dos_Casillas(celda));
										cuenta.conexion.enviar_Paquete("GKK" + tipo_gkk_movimiento.ToString(), false);
									}
								}
							}
							break;
						}
						case 2:
						case 3:
							break;
						case 4:
							separador = separador[3].Split(new char[]
							{
								','
							});
							celda = mapa.get_Celda_Id(short.Parse(separador[1]));
							if (!cuenta.esta_luchando() && id_entidad == personaje.id && celda.id > 0 && personaje.celda.id != celda.id)
							{
								personaje.celda = celda;
								await Task.Delay(150);
								cuenta.conexion.enviar_Paquete("GKK1", false);
								mapa.evento_Entidad_Actualizada();
								cuenta.juego.manejador.movimientos.movimiento_Actualizado(true);
							}
							break;
						case 5:
							if (cuenta.esta_luchando())
							{
								separador = separador[3].Split(new char[]
								{
									','
								});
								luchador = pelea.get_Luchador_Por_Id(int.Parse(separador[0]));
								if (luchador != null)
								{
									luchador.celda = mapa.get_Celda_Id(short.Parse(separador[1]));
								}
							}
							break;
						default:
							if (num3 == 102)
							{
								if (cuenta.esta_luchando())
								{
									luchador = pelea.get_Luchador_Por_Id(id_entidad);
									byte pa_utilizados = byte.Parse(separador[3].Split(new char[]
									{
										','
									})[1].Substring(1));
									if (luchador != null)
									{
										Luchadores luchadores = luchador;
										luchadores.pa -= pa_utilizados;
									}
								}
							}
							break;
						}
					}
					else if (num3 != 103)
					{
						if (num3 != 129)
						{
							if (num3 == 151)
							{
								if (cuenta.esta_luchando())
								{
									luchador = pelea.get_Luchador_Por_Id(id_entidad);
									if (luchador != null && luchador.id == personaje.id)
									{
										cuenta.logger.log_Error("INFORMATION", "No es posible realizar esta acción por culpa de un obstáculo invisible.");
										pelea.get_Hechizo_Lanzado(short.Parse(separador[3]), false);
									}
								}
							}
						}
						else if (cuenta.esta_luchando())
						{
							luchador = pelea.get_Luchador_Por_Id(id_entidad);
							byte pm_utilizados = byte.Parse(separador[3].Split(new char[]
							{
								','
							})[1].Substring(1));
							if (luchador != null)
							{
								Luchadores luchadores2 = luchador;
								luchadores2.pm -= pm_utilizados;
							}
							if (luchador.id == personaje.id)
							{
								pelea.get_Movimiento_Exito(true);
							}
						}
					}
					else if (cuenta.esta_luchando())
					{
						int id_muerto = int.Parse(separador[3]);
						luchador = pelea.get_Luchador_Por_Id(id_muerto);
						if (luchador != null)
						{
							luchador.esta_vivo = false;
						}
					}
				}
				else if (num3 <= 300)
				{
					if (num3 != 181)
					{
						if (num3 == 300)
						{
							if (cuenta.esta_luchando() && id_entidad == cuenta.juego.personaje.id)
							{
								short celda_id_lanzado = short.Parse(separador[3].Split(new char[]
								{
									','
								})[1]);
								pelea.get_Hechizo_Lanzado(celda_id_lanzado, true);
							}
						}
					}
					else
					{
						celda = mapa.get_Celda_Id(short.Parse(separador[3].Substring(1)));
						short id_luchador = short.Parse(separador[6]);
						short vida = short.Parse(separador[15]);
						byte pa = byte.Parse(separador[16]);
						byte pm = byte.Parse(separador[17]);
						byte equipo = byte.Parse(separador[25]);
						pelea.get_Agregar_Luchador(new Luchadores((int)id_luchador, true, (int)vida, pa, pm, celda, (int)vida, equipo, id_entidad));
					}
				}
				else if (num3 != 302)
				{
					if (num3 != 501)
					{
						if (num3 == 900)
						{
							cuenta.conexion.enviar_Paquete("GA902" + id_entidad.ToString(), true);
							cuenta.logger.log_informacion("INFORMATION", "Desafio del personaje id: " + id_entidad.ToString() + " cancelado");
						}
					}
					else
					{
						int tiempo_recoleccion = int.Parse(separador[3].Split(new char[]
						{
							','
						})[1]);
						celda = mapa.get_Celda_Id(short.Parse(separador[3].Split(new char[]
						{
							','
						})[0]));
						byte tipo_gkk_recoleccion = byte.Parse(separador[0]);
						await cuenta.juego.manejador.recoleccion.evento_Recoleccion_Iniciada(id_entidad, tiempo_recoleccion, celda.id, tipo_gkk_recoleccion);
					}
				}
				else if (cuenta.esta_luchando() && id_entidad == cuenta.juego.personaje.id)
				{
					pelea.get_Hechizo_Lanzado(0, false);
				}
				celda = null;
				luchador = null;
				mapa = null;
				pelea = null;
			}
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00026538 File Offset: 0x00024938
		[PaqueteAtributo("GDF")]
		public void get_Estado_Interactivo(ClienteTcp cliente, string paquete)
		{
			foreach (string text in paquete.Substring(4).Split(new char[]
			{
				'|'
			}))
			{
				string[] array2 = text.Split(new char[]
				{
					';'
				});
				Cuenta cuenta = cliente.cuenta;
				short num = short.Parse(array2[0]);
				switch (byte.Parse(array2[1]))
				{
				case 2:
					cuenta.juego.mapa.interactivos[(int)num].es_utilizable = false;
					break;
				case 3:
				{
					cuenta.juego.mapa.interactivos[(int)num].es_utilizable = false;
					bool flag = cuenta.esta_recolectando();
					if (flag)
					{
						cuenta.juego.manejador.recoleccion.evento_Recoleccion_Acabada(RecoleccionResultado.RECOLECTADO, num);
					}
					else
					{
						cuenta.juego.manejador.recoleccion.evento_Recoleccion_Acabada(RecoleccionResultado.ROBADO, num);
					}
					break;
				}
				case 4:
					cuenta.juego.mapa.interactivos[(int)num].es_utilizable = false;
					break;
				}
			}
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00026672 File Offset: 0x00024A72
		[PaqueteAtributo("GDM")]
		public void get_Nuevo_Mapa(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.juego.mapa.get_Actualizar_Mapa(paquete.Substring(4));
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x00026691 File Offset: 0x00024A91
		[PaqueteAtributo("GDK")]
		public void get_Mapa_Cambiado(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.juego.mapa.get_Evento_Mapa_Cambiado();
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x000266A9 File Offset: 0x00024AA9
		[PaqueteAtributo("GV")]
		public void get_Reiniciar_Pantalla(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.conexion.enviar_Paquete("GC1", false);
		}
	}
}
