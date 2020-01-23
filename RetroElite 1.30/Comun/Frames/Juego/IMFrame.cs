using System;
using RetroElite.Comun.Frames.Transporte;
using RetroElite.Comun.Network;

namespace RetroElite.Comun.Frames.Juego
{
	// Token: 0x0200008E RID: 142
	internal class IMFrame : Frame
	{
		// Token: 0x060005C5 RID: 1477 RVA: 0x0002618C File Offset: 0x0002458C
		[PaqueteAtributo("Im189")]
		public void get_Mensaje_Bienvenida_Dofus(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.logger.log_Error("DOFUS", "Bienvenue sur DOFUS, le monde des douze! Attention: Il est interdit de communiquer votre compte utilisateur et votre mot de passe.");
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x000261A9 File Offset: 0x000245A9
		[PaqueteAtributo("Im039")]
		public void get_Pelea_Espectador_Desactivado(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.logger.log_Fight("COMBAT", "Le mode de visualisation est désactivé.");
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x000261C6 File Offset: 0x000245C6
		[PaqueteAtributo("Im040")]
		public void get_Pelea_Espectador_Activado(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.logger.log_Fight("COMBAT", "Le mode spectateur est activé.");
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x000261E4 File Offset: 0x000245E4
		[PaqueteAtributo("Im0152")]
		public void get_Mensaje_Ultima_Conexion_IP(ClienteTcp cliente, string paquete)
		{
			string text = paquete.Substring(3).Split(new char[]
			{
				';'
			})[1];
			cliente.cuenta.logger.log_informacion("DOFUS", string.Concat(new string[]
			{
				"Dernière connexion à votre compte effectuée le ",
				text.Split(new char[]
				{
					'~'
				})[0],
				"/",
				text.Split(new char[]
				{
					'~'
				})[1],
				"/",
				text.Split(new char[]
				{
					'~'
				})[2],
				" à ",
				text.Split(new char[]
				{
					'~'
				})[3],
				":",
				text.Split(new char[]
				{
					'~'
				})[4],
				" avec l'ip ",
				text.Split(new char[]
				{
					'~'
				})[5]
			}));
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x000262E5 File Offset: 0x000246E5
		[PaqueteAtributo("Im0153")]
		public void get_Mensaje_Nueva_Conexion_IP(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.logger.log_informacion("DOFUS", "Ton ip actuel est " + paquete.Substring(3).Split(new char[]
			{
				';'
			})[1]);
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00026320 File Offset: 0x00024720
		[PaqueteAtributo("Im020")]
		public void get_Mensaje_Abrir_Cofre_Perder_Kamas(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.logger.log_informacion("DOFUS", "Tu dois donner " + paquete.Split(new char[]
			{
				';'
			})[1] + " kamas accéder à ce coffre.");
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x0002635A File Offset: 0x0002475A
		[PaqueteAtributo("Im025")]
		public void get_Mensaje_Mascota_Feliz(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.logger.log_informacion("DOFUS", "Votre animal de compagnie est très heureux de vous revoir!");
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x00026377 File Offset: 0x00024777
		[PaqueteAtributo("Im0157")]
		public void get_Mensaje_Error_Chat_Difusion(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.logger.log_informacion("DOFUS", "Vous devez être abonnée" + paquete.Split(new char[]
			{
				';'
			})[1]);
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x000263AC File Offset: 0x000247AC
		[PaqueteAtributo("Im037")]
		public void get_Mensaje_Modo_Away_Dofus(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.logger.log_informacion("DOFUS", "A partir de maintenant, vous serez considéré comme absent.");
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x000263C9 File Offset: 0x000247C9
		[PaqueteAtributo("Im112")]
		public void get_Mensaje_Pods_Llenos(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.logger.log_Error("DOFUS", "Vous êtes trop chargé. impossible de ce déplacer.");
		}
	}
}
