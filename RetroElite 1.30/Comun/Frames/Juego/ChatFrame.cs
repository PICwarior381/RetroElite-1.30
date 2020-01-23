using System;
using RetroElite.Comun.Frames.Transporte;
using RetroElite.Comun.Network;

namespace RetroElite.Comun.Frames.Juego
{
	// Token: 0x0200008D RID: 141
	internal class ChatFrame : Frame
	{
		// Token: 0x060005C1 RID: 1473 RVA: 0x00025EFD File Offset: 0x000242FD
		[PaqueteAtributo("cC+")]
		public void get_Agregar_Canal(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.juego.personaje.agregar_Canal_Personaje(paquete.Substring(3));
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x00025F1C File Offset: 0x0002431C
		[PaqueteAtributo("cC-")]
		public void get_Eliminar_Canal(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.juego.personaje.eliminar_Canal_Personaje(paquete.Substring(3));
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00025F3C File Offset: 0x0002433C
		[PaqueteAtributo("cMK")]
		public void get_Mensajes_Chat(ClienteTcp cliente, string paquete)
		{
			string[] array = paquete.Substring(3).Split(new char[]
			{
				'|'
			});
			string text = string.Empty;
			string text2 = array[0];
			string text3 = text2;
			if (text3 != null)
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text3);
				if (num <= 973910158U)
				{
					if (num <= 554469683U)
					{
						if (num != 537692064U)
						{
							if (num == 554469683U)
							{
								if (text3 == "$")
								{
									text = "Groupe";
									goto IL_20D;
								}
							}
						}
						else if (text3 == "%")
						{
							text = "Guilde";
							goto IL_20D;
						}
					}
					else if (num != 638357778U)
					{
						if (num == 973910158U)
						{
							if (text3 == "?")
							{
								text = "Recrutement";
								goto IL_20D;
							}
						}
					}
					else if (text3 == "#")
					{
						text = "Equipe";
						goto IL_20D;
					}
				}
				else if (num <= 3272340793U)
				{
					if (num != 1057798253U)
					{
						if (num == 3272340793U)
						{
							if (text3 == "F")
							{
								cliente.cuenta.logger.log_privado("MP", array[2] + ": " + array[3]);
								goto IL_20D;
							}
						}
					}
					else if (text3 == ":")
					{
						text = "Commerce";
						goto IL_20D;
					}
				}
				else if (num != 3507227459U)
				{
					if (num != 3675003649U)
					{
						if (num == 3960223172U)
						{
							if (text3 == "i")
							{
								text = "Information";
								goto IL_20D;
							}
						}
					}
					else if (text3 == "^")
					{
						text = "Incarnam";
						goto IL_20D;
					}
				}
				else if (text3 == "T")
				{
					cliente.cuenta.logger.log_privado("MP Envoyé ", array[2] + ": " + array[3]);
					goto IL_20D;
				}
			}
			text = "Général";
			IL_20D:
			bool flag = !text.Equals(string.Empty);
			if (flag)
			{
				cliente.cuenta.logger.log_normal(text, array[2] + ": " + array[3]);
			}
		}
	}
}
