using System;
using RetroElite.Comun.Frames.Transporte;
using RetroElite.Comun.Network;

namespace RetroElite.Comun.Frames.Juego
{
	// Token: 0x0200008C RID: 140
	internal class AutentificacionJuego : Frame
	{
		// Token: 0x060005BD RID: 1469 RVA: 0x00025E7F File Offset: 0x0002427F
		[PaqueteAtributo("M030")]
		public void get_Error_Streaming(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.logger.log_Error("Login", "Connexion rejetée. Vous ne pouvez pas vous authentifier pour ce serveur car votre connexion a expiré. Assurez-vous de couper les téléchargements, ainsi que la musique ou les vidéos en streaming, afin d'améliorer la qualité et la vitesse de votre connexion.");
			cliente.cuenta.desconectar();
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00025EA9 File Offset: 0x000242A9
		[PaqueteAtributo("M031")]
		public void get_Error_Red(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.logger.log_Error("Login", "Connexion rejetée. Le serveur de jeu n'a pas reçu les informations d'authentification nécessaires après votre identification. Veuillez réessayer et, si le problème persiste, contactez votre administrateur réseau ou votre serveur d'accès Internet. Il s'agit d'un problème de nouvelle adresse en raison d'une mauvaise configuration DNS.");
			cliente.cuenta.desconectar();
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00025ED3 File Offset: 0x000242D3
		[PaqueteAtributo("M032")]
		public void get_Error_Flood_Conexion(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.logger.log_Error("Login", "Pour éviter de gêner les autres joueurs, attendez %1 secondes avant de vous reconnecter.");
			cliente.cuenta.desconectar();
		}
	}
}
