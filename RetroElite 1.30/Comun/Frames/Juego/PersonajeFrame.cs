using System;
using System.Collections.Generic;
using System.Linq;
using RetroElite.Comun.Frames.Transporte;
using RetroElite.Comun.Network;
using RetroElite.Otros;
using RetroElite.Otros.Enums;
using RetroElite.Otros.Game.Personaje;
using RetroElite.Otros.Game.Personaje.Oficios;
using RetroElite.Otros.Mapas.Entidades;

namespace RetroElite.Comun.Frames.Juego
{
	// Token: 0x02000092 RID: 146
	internal class PersonajeFrame : Frame
	{
		// Token: 0x060005E5 RID: 1509 RVA: 0x00026C6F File Offset: 0x0002506F
		[PaqueteAtributo("As")]
		public void get_Stats_Actualizados(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.juego.personaje.actualizar_Caracteristicas(paquete);
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00026C88 File Offset: 0x00025088
		[PaqueteAtributo("PIK")]
		public void get_Peticion_Grupo(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.logger.log_informacion("Grupo", "Nouvelle invitation de groupe de personnages: " + paquete.Substring(3).Split(new char[]
			{
				'|'
			})[0]);
			cliente.enviar_Paquete("PR", false);
			cliente.cuenta.logger.log_informacion("Grupo", "Invitation rejeté");
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00026CF8 File Offset: 0x000250F8
		[PaqueteAtributo("SL")]
		public void get_Lista_Hechizos(ClienteTcp cliente, string paquete)
		{
			bool flag = !paquete[2].Equals('o');
			if (flag)
			{
				cliente.cuenta.juego.personaje.actualizar_Hechizos(paquete.Substring(2));
			}
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x00026D3C File Offset: 0x0002513C
		[PaqueteAtributo("Ow")]
		public void get_Actualizacion_Pods(ClienteTcp cliente, string paquete)
		{
			string[] array = paquete.Substring(2).Split(new char[]
			{
				'|'
			});
			short pods_actuales = short.Parse(array[0]);
			short pods_maximos = short.Parse(array[1]);
			PersonajeJuego personaje = cliente.cuenta.juego.personaje;
			personaje.inventario.pods_actuales = pods_actuales;
			personaje.inventario.pods_maximos = pods_maximos;
			cliente.cuenta.juego.personaje.evento_Pods_Actualizados();
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00026DB8 File Offset: 0x000251B8
		[PaqueteAtributo("DV")]
		public void get_Cerrar_Dialogo(ClienteTcp cliente, string paquete)
		{
			Cuenta cuenta = cliente.cuenta;
			EstadoCuenta estado_Cuenta = cuenta.Estado_Cuenta;
			EstadoCuenta estadoCuenta = estado_Cuenta;
			if (estadoCuenta != EstadoCuenta.DIALOGANDO)
			{
				if (estadoCuenta == EstadoCuenta.ALMACENAMIENTO)
				{
					cuenta.juego.personaje.inventario.evento_Almacenamiento_Abierto();
				}
			}
			else
			{
				IEnumerable<Npcs> source = cuenta.juego.mapa.lista_npcs();
				Npcs npcs = source.ElementAt((int)(cuenta.juego.personaje.hablando_npc_id * -1 - 1));
				npcs.respuestas.Clear();
				npcs.respuestas = null;
				cuenta.Estado_Cuenta = EstadoCuenta.CONECTADO_INACTIVO;
				cuenta.juego.personaje.evento_Dialogo_Acabado();
			}
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00026E58 File Offset: 0x00025258
		[PaqueteAtributo("EV")]
		public void get_Ventana_Cerrada(ClienteTcp cliente, string paquete)
		{
			Cuenta cuenta = cliente.cuenta;
			bool flag = cuenta.Estado_Cuenta == EstadoCuenta.ALMACENAMIENTO;
			if (flag)
			{
				cuenta.Estado_Cuenta = EstadoCuenta.CONECTADO_INACTIVO;
				cuenta.juego.personaje.inventario.evento_Almacenamiento_Cerrado();
			}
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00026E9C File Offset: 0x0002529C
		[PaqueteAtributo("JS")]
		public void get_Skills_Oficio(ClienteTcp cliente, string paquete)
		{
			PersonajeJuego personaje = cliente.cuenta.juego.personaje;
			short id_oficio;
			short id_skill;
			Predicate<Oficio> <>9__0;
			Predicate<SkillsOficio> <>9__1;
			foreach (string text in paquete.Substring(3).Split(new char[]
			{
				'|'
			}))
			{
				id_oficio = short.Parse(text.Split(new char[]
				{
					';'
				})[0]);
				List<Oficio> oficios = personaje.oficios;
				Predicate<Oficio> match;
				if ((match = <>9__0) == null)
				{
					match = (<>9__0 = ((Oficio x) => x.id == (int)id_oficio));
				}
				Oficio oficio = oficios.Find(match);
				bool flag = oficio == null;
				if (flag)
				{
					oficio = new Oficio((int)id_oficio);
					personaje.oficios.Add(oficio);
				}
				foreach (string text2 in text.Split(new char[]
				{
					';'
				})[1].Split(new char[]
				{
					','
				}))
				{
					string[] array3 = text2.Split(new char[]
					{
						'~'
					});
					id_skill = short.Parse(array3[0]);
					byte cantidad_minima = byte.Parse(array3[1]);
					byte cantidad_maxima = byte.Parse(array3[2]);
					float tiempo = float.Parse(array3[4]);
					List<SkillsOficio> skills = oficio.skills;
					Predicate<SkillsOficio> match2;
					if ((match2 = <>9__1) == null)
					{
						match2 = (<>9__1 = ((SkillsOficio actividad) => actividad.id == id_skill));
					}
					SkillsOficio skillsOficio = skills.Find(match2);
					bool flag2 = skillsOficio != null;
					if (flag2)
					{
						skillsOficio.set_Actualizar(id_skill, cantidad_minima, cantidad_maxima, tiempo);
					}
					else
					{
						oficio.skills.Add(new SkillsOficio(id_skill, cantidad_minima, cantidad_maxima, tiempo));
					}
				}
			}
			personaje.evento_Oficios_Actualizados();
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x0002706C File Offset: 0x0002546C
		[PaqueteAtributo("JX")]
		public void get_Experiencia_Oficio(ClienteTcp cliente, string paquete)
		{
			string[] array = paquete.Substring(3).Split(new char[]
			{
				'|'
			});
			PersonajeJuego personaje = cliente.cuenta.juego.personaje;
			short id;
			Predicate<Oficio> <>9__0;
			foreach (string text in array)
			{
				id = short.Parse(text.Split(new char[]
				{
					';'
				})[0]);
				byte b = byte.Parse(text.Split(new char[]
				{
					';'
				})[1]);
				uint experiencia_base = uint.Parse(text.Split(new char[]
				{
					';'
				})[2]);
				uint experiencia_actual = uint.Parse(text.Split(new char[]
				{
					';'
				})[3]);
				bool flag = b < 100;
				uint experiencia_siguiente_nivel;
				if (flag)
				{
					experiencia_siguiente_nivel = uint.Parse(text.Split(new char[]
					{
						';'
					})[4]);
				}
				else
				{
					experiencia_siguiente_nivel = 0U;
				}
				List<Oficio> oficios = personaje.oficios;
				Predicate<Oficio> match;
				if ((match = <>9__0) == null)
				{
					match = (<>9__0 = ((Oficio x) => x.id == (int)id));
				}
				oficios.Find(match).set_Actualizar_Oficio(b, experiencia_base, experiencia_actual, experiencia_siguiente_nivel);
			}
			personaje.evento_Oficios_Actualizados();
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x000271AA File Offset: 0x000255AA
		[PaqueteAtributo("Re")]
		public void get_Datos_Montura(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.puede_utilizar_dragopavo = true;
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x000271B8 File Offset: 0x000255B8
		[PaqueteAtributo("OAKO")]
		public void get_Aparecer_Objeto(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.juego.personaje.inventario.agregar_Objetos(paquete.Substring(4));
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x000271DC File Offset: 0x000255DC
		[PaqueteAtributo("OR")]
		public void get_Eliminar_Objeto(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.juego.personaje.inventario.eliminar_Objeto(uint.Parse(paquete.Substring(2)), 1, false);
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x00027207 File Offset: 0x00025607
		[PaqueteAtributo("OQ")]
		public void get_Modificar_Cantidad_Objeto(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.juego.personaje.inventario.modificar_Objetos(paquete.Substring(2));
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0002722B File Offset: 0x0002562B
		[PaqueteAtributo("ECK")]
		public void get_Intercambio_Ventana_Abierta(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.Estado_Cuenta = EstadoCuenta.ALMACENAMIENTO;
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0002723A File Offset: 0x0002563A
		[PaqueteAtributo("PCK")]
		public void get_Grupo_Aceptado(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.juego.personaje.en_grupo = true;
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0002723A File Offset: 0x0002563A
		[PaqueteAtributo("PV")]
		public void get_Grupo_Abandonado(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.juego.personaje.en_grupo = true;
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00027253 File Offset: 0x00025653
		[PaqueteAtributo("ERK")]
		public void get_Peticion_Intercambio(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.logger.log_informacion("INFORMACIÓN", "Invitation d'échange reçue");
			cliente.enviar_Paquete("EV", true);
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x00027280 File Offset: 0x00025680
		[PaqueteAtributo("ILS")]
		public void get_Tiempo_Regenerado(ClienteTcp cliente, string paquete)
		{
			paquete = paquete.Substring(3);
			int num = int.Parse(paquete);
			Cuenta cuenta = cliente.cuenta;
			PersonajeJuego personaje = cuenta.juego.personaje;
			personaje.timer_regeneracion.Change(-1, -1);
			personaje.timer_regeneracion.Change(num, num);
			cuenta.logger.log_informacion("DOFUS", string.Format("Votre personnage récupère 1 pdv toutes les {0} secondes", num / 1000));
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x000272F4 File Offset: 0x000256F4
		[PaqueteAtributo("ILF")]
		public void get_Cantidad_Vida_Regenerada(ClienteTcp cliente, string paquete)
		{
			paquete = paquete.Substring(3);
			int num = int.Parse(paquete);
			Cuenta cuenta = cliente.cuenta;
			PersonajeJuego personaje = cuenta.juego.personaje;
			personaje.caracteristicas.vitalidad_actual += num;
			cuenta.logger.log_informacion("DOFUS", string.Format("Vous avez récupéré {0} points de vie", num));
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x0002735C File Offset: 0x0002575C
		[PaqueteAtributo("eUK")]
		public void get_Emote_Recibido(ClienteTcp cliente, string paquete)
		{
			string[] array = paquete.Substring(3).Split(new char[]
			{
				'|'
			});
			int num = int.Parse(array[0]);
			int num2 = int.Parse(array[1]);
			Cuenta cuenta = cliente.cuenta;
			bool flag = cuenta.juego.personaje.id != num;
			if (!flag)
			{
				bool flag2 = num2 == 1 && cuenta.Estado_Cuenta != EstadoCuenta.REGENERANDO;
				if (flag2)
				{
					cuenta.Estado_Cuenta = EstadoCuenta.REGENERANDO;
				}
				else
				{
					bool flag3 = num2 == 0 && cuenta.Estado_Cuenta == EstadoCuenta.REGENERANDO;
					if (flag3)
					{
						cuenta.Estado_Cuenta = EstadoCuenta.CONECTADO_INACTIVO;
					}
				}
			}
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x000273FC File Offset: 0x000257FC
		[PaqueteAtributo("Bp")]
		public void get_Ping_Promedio(ClienteTcp cliente, string paquete)
		{
			cliente.enviar_Paquete(string.Format("Bp{0}|{1}|50", cliente.get_Promedio_Pings(), cliente.get_Total_Pings()), false);
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00027426 File Offset: 0x00025826
		[PaqueteAtributo("pong")]
		public void get_Ping_Pong(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.logger.log_informacion("DOFUS", string.Format("Ping: {0} ms", cliente.get_Actual_Ping()));
		}
	}
}
