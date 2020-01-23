using System;
using System.Collections.Generic;
using System.Linq;
using RetroElite.Comun.Frames.Transporte;
using RetroElite.Comun.Network;
using RetroElite.Otros;
using RetroElite.Otros.Enums;
using RetroElite.Otros.Mapas.Entidades;

namespace RetroElite.Comun.Frames.Juego
{
	// Token: 0x02000090 RID: 144
	internal class NPCFrame : Frame
	{
		// Token: 0x060005D9 RID: 1497 RVA: 0x000266C4 File Offset: 0x00024AC4
		[PaqueteAtributo("DCK")]
		public void get_Dialogo_Creado(ClienteTcp cliente, string paquete)
		{
			Cuenta cuenta = cliente.cuenta;
			cuenta.Estado_Cuenta = EstadoCuenta.DIALOGANDO;
			cuenta.juego.personaje.hablando_npc_id = sbyte.Parse(paquete.Substring(3));
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x00026700 File Offset: 0x00024B00
		[PaqueteAtributo("DQ")]
		public void get_Lista_Respuestas(ClienteTcp cliente, string paquete)
		{
			Cuenta cuenta = cliente.cuenta;
			bool flag = !cuenta.esta_dialogando();
			if (!flag)
			{
				IEnumerable<Npcs> source = cuenta.juego.mapa.lista_npcs();
				Npcs npcs = source.ElementAt((int)(cuenta.juego.personaje.hablando_npc_id * -1 - 1));
				bool flag2 = npcs != null;
				if (flag2)
				{
					string[] array = paquete.Substring(2).Split(new char[]
					{
						'|'
					});
					string[] array2 = array[1].Split(new char[]
					{
						';'
					});
					npcs.pregunta = short.Parse(array[0].Split(new char[]
					{
						';'
					})[0]);
					npcs.respuestas = new List<short>(array2.Count<string>());
					foreach (string s in array2)
					{
						npcs.respuestas.Add(short.Parse(s));
					}
					cuenta.juego.personaje.evento_Dialogo_Recibido();
				}
			}
		}
	}
}
