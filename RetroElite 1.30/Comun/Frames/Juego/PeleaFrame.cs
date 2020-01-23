using System;
using System.Threading.Tasks;
using RetroElite.Comun.Frames.Transporte;
using RetroElite.Comun.Network;
using RetroElite.Otros;
using RetroElite.Otros.Mapas;
using RetroElite.Otros.Peleas.Peleadores;
using RetroElite.Utilidades.Criptografia;

namespace RetroElite.Comun.Frames.Juego
{
	// Token: 0x02000091 RID: 145
	internal class PeleaFrame : Frame
	{
		// Token: 0x060005DC RID: 1500 RVA: 0x0002680C File Offset: 0x00024C0C
		[PaqueteAtributo("GP")]
		public void get_Combate_Celdas_Posicion(ClienteTcp cliente, string paquete)
		{
			Cuenta cuenta = cliente.cuenta;
			Mapa mapa = cuenta.juego.mapa;
			string[] array = paquete.Substring(2).Split(new char[]
			{
				'|'
			});
			for (int i = 0; i < array[0].Length; i += 2)
			{
				cuenta.juego.pelea.celdas_preparacion.Add(mapa.get_Celda_Id((short)(((int)Hash.get_Hash(array[0][i]) << 6) + (int)Hash.get_Hash(array[0][i + 1]))));
			}
			bool desactivar_espectador = cuenta.pelea_extension.configuracion.desactivar_espectador;
			if (desactivar_espectador)
			{
				cliente.enviar_Paquete("fS", false);
			}
			bool puede_utilizar_dragopavo = cuenta.puede_utilizar_dragopavo;
			if (puede_utilizar_dragopavo)
			{
				bool flag = cuenta.pelea_extension.configuracion.utilizar_dragopavo && !cuenta.juego.personaje.esta_utilizando_dragopavo;
				if (flag)
				{
					cliente.enviar_Paquete("Rr", false);
					cuenta.juego.personaje.esta_utilizando_dragopavo = true;
				}
			}
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0002691C File Offset: 0x00024D1C
		[PaqueteAtributo("GICE")]
		public async Task get_Error_Cambiar_Pos_Pelea(ClienteTcp cliente, string paquete)
		{
			bool flag = cliente.cuenta.esta_luchando();
			if (flag)
			{
				await Task.Delay(150);
				cliente.enviar_Paquete("GR1", false);
			}
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00026974 File Offset: 0x00024D74
		[PaqueteAtributo("GIC")]
		public async Task get_Cambiar_Pos_Pelea(ClienteTcp cliente, string paquete)
		{
			Cuenta cuenta = cliente.cuenta;
			string[] separador_posiciones = paquete.Substring(4).Split(new char[]
			{
				'|'
			});
			Mapa mapa = cuenta.juego.mapa;
			foreach (string posicion in separador_posiciones)
			{
				int id_entidad = int.Parse(posicion.Split(new char[]
				{
					';'
				})[0]);
				short celda = short.Parse(posicion.Split(new char[]
				{
					';'
				})[1]);
				bool flag = id_entidad == cuenta.juego.personaje.id;
				if (flag)
				{
					await Task.Delay(150);
					cliente.enviar_Paquete("GR1", false);
				}
				Luchadores luchador = cuenta.juego.pelea.get_Luchador_Por_Id(id_entidad);
				if (luchador != null)
				{
					luchador.celda = mapa.get_Celda_Id(celda);
				}
				luchador = null;
				posicion = null;
			}
			string[] array = null;
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x000269CC File Offset: 0x00024DCC
		[PaqueteAtributo("GTM")]
		public void get_Combate_Info_Stats(ClienteTcp cliente, string paquete)
		{
			string[] array = paquete.Substring(4).Split(new char[]
			{
				'|'
			});
			Mapa mapa = cliente.cuenta.juego.mapa;
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(new char[]
				{
					';'
				});
				int num = int.Parse(array2[0]);
				Luchadores luchadores = cliente.cuenta.juego.pelea.get_Luchador_Por_Id(num);
				bool flag = array2.Length != 0;
				if (flag)
				{
					bool flag2 = array2[1].Equals("0");
					bool flag3 = flag2;
					if (flag3)
					{
						int vida_actual = int.Parse(array2[2]);
						byte pa = byte.Parse(array2[3]);
						byte pm = byte.Parse(array2[4]);
						short num2 = short.Parse(array2[5]);
						int vida_maxima = int.Parse(array2[7]);
						bool flag4 = num2 > 0;
						if (flag4)
						{
							byte equipo = Convert.ToByte((num > 0) ? 1 : 0);
							if (luchadores != null)
							{
								luchadores.get_Actualizar_Luchador(num, flag2, vida_actual, pa, pm, mapa.get_Celda_Id(num2), vida_maxima, equipo);
							}
						}
					}
					else if (luchadores != null)
					{
						luchadores.get_Actualizar_Luchador(num, flag2, 0, 0, 0, null, 0, 0);
					}
				}
			}
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x00026B0C File Offset: 0x00024F0C
		[PaqueteAtributo("GTR")]
		public void get_Combate_Turno_Listo(ClienteTcp cliente, string paquete)
		{
			Cuenta cuenta = cliente.cuenta;
			int num = int.Parse(paquete.Substring(3));
			bool flag = cuenta.juego.personaje.id == num;
			if (flag)
			{
				cuenta.conexion.enviar_Paquete("BD", false);
			}
			cuenta.conexion.enviar_Paquete("GT", false);
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00026B6C File Offset: 0x00024F6C
		[PaqueteAtributo("GJK")]
		public void get_Combate_Unirse_Pelea(ClienteTcp cliente, string paquete)
		{
			Cuenta cuenta = cliente.cuenta;
			string[] array = paquete.Substring(3).Split(new char[]
			{
				'|'
			});
			byte b = byte.Parse(array[0]);
			byte b2 = b;
			byte b3 = b2;
			if (b3 <= 4)
			{
				cuenta.juego.pelea.get_Combate_Creado();
			}
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x00026BC4 File Offset: 0x00024FC4
		[PaqueteAtributo("GTS")]
		public void get_Combate_Inicio_Turno(ClienteTcp cliente, string paquete)
		{
			Cuenta cuenta = cliente.cuenta;
			bool flag = int.Parse(paquete.Substring(3).Split(new char[]
			{
				'|'
			})[0]) != cuenta.juego.personaje.id || cuenta.juego.pelea.total_enemigos_vivos <= 0;
			if (!flag)
			{
				cuenta.juego.pelea.get_Turno_Iniciado();
			}
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00026C3C File Offset: 0x0002503C
		[PaqueteAtributo("GE")]
		public void get_Combate_Finalizado(ClienteTcp cliente, string paquete)
		{
			Cuenta cuenta = cliente.cuenta;
			cuenta.juego.pelea.get_Combate_Acabado();
			cliente.enviar_Paquete("GC1", false);
		}
	}
}
