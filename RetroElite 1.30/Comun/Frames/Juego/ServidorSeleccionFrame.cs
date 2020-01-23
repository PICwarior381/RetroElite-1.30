using System;
using RetroElite.Comun.Frames.Transporte;
using RetroElite.Comun.Network;
using RetroElite.Otros;
using RetroElite.Otros.Enums;

namespace RetroElite.Comun.Frames.Juego
{
	// Token: 0x02000093 RID: 147
	internal class ServidorSeleccionFrame : Frame
	{
		// Token: 0x060005FB RID: 1531 RVA: 0x00027453 File Offset: 0x00025853
		[PaqueteAtributo("HG")]
		public void bienvenida_Juego(ClienteTcp cliente, string paquete)
		{
			cliente.enviar_Paquete("AT" + cliente.cuenta.tiquet_game, false);
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00027472 File Offset: 0x00025872
		[PaqueteAtributo("ATK0")]
		public void resultado_Servidor_Seleccion(ClienteTcp cliente, string paquete)
		{
			cliente.enviar_Paquete("Ak0", false);
			cliente.enviar_Paquete("AV", false);
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x0002748F File Offset: 0x0002588F
		[PaqueteAtributo("AV0")]
		public void lista_Personajes(ClienteTcp cliente, string paquete)
		{
			cliente.enviar_Paquete("Ages", false);
			cliente.enviar_Paquete("AL", false);
			cliente.enviar_Paquete("Af", false);
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x000274BC File Offset: 0x000258BC
		[PaqueteAtributo("ALK")]
		public void seleccionar_Personaje(ClienteTcp cliente, string paquete)
		{
			Cuenta cuenta = cliente.cuenta;
			string[] array = paquete.Substring(3).Split(new char[]
			{
				'|'
			});
			int num = 2;
			bool flag = false;
			while (num < array.Length && !flag)
			{
				string[] array2 = array[num].Split(new char[]
				{
					';'
				});
				int num2 = int.Parse(array2[0]);
				string text = array2[1];
				bool flag2 = text.ToLower().Equals(cuenta.configuracion.nombre_personaje.ToLower()) || string.IsNullOrEmpty(cuenta.configuracion.nombre_personaje);
				if (flag2)
				{
					cliente.enviar_Paquete("AS" + num2.ToString(), true);
					flag = true;
				}
				num++;
			}
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00027588 File Offset: 0x00025988
		[PaqueteAtributo("BT")]
		public void get_Tiempo_Servidor(ClienteTcp cliente, string paquete)
		{
			cliente.enviar_Paquete("GI", false);
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00027598 File Offset: 0x00025998
		[PaqueteAtributo("ASK")]
		public void personaje_Seleccionado(ClienteTcp cliente, string paquete)
		{
			Cuenta cuenta = cliente.cuenta;
			string[] array = paquete.Substring(4).Split(new char[]
			{
				'|'
			});
			int id = int.Parse(array[0]);
			string nombre_personaje = array[1];
			byte nivel = byte.Parse(array[2]);
			byte raza_id = byte.Parse(array[3]);
			byte sexo = byte.Parse(array[4]);
			cuenta.juego.personaje.set_Datos_Personaje(id, nombre_personaje, nivel, sexo, raza_id);
			cuenta.juego.personaje.inventario.agregar_Objetos(array[9]);
			cliente.enviar_Paquete("GC1", false);
			cliente.enviar_Paquete("BYA", false);
			cuenta.juego.personaje.evento_Personaje_Seleccionado();
			cuenta.juego.personaje.timer_afk.Change(1200000, 1200000);
			cliente.cuenta.Estado_Cuenta = EstadoCuenta.CONECTADO_INACTIVO;
		}
	}
}
