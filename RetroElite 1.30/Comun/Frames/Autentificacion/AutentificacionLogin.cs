using System;
using System.Text;
using RetroElite.Comun.Frames.Transporte;
using RetroElite.Comun.Network;

namespace RetroElite.Comun.Frames.Autentificacion
{
	// Token: 0x02000094 RID: 148
	internal class AutentificacionLogin : Frame
	{
		// Token: 0x06000602 RID: 1538 RVA: 0x0002767B File Offset: 0x00025A7B
		[PaqueteAtributo("AlEf")]
		public void get_Error_Datos(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.logger.log_Error("LOGIN", "Connexion rejetée. Nom de compte ou mot de passe incorrect.");
			cliente.cuenta.desconectar();
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x000276A5 File Offset: 0x00025AA5
		[PaqueteAtributo("AlEa")]
		public void get_Error_Ya_Conectado(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.logger.log_Error("LOGIN", "Déjà connecté Essaye à nouveau..");
			cliente.cuenta.desconectar();
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x000276CF File Offset: 0x00025ACF
		[PaqueteAtributo("AlEv")]
		public void get_Error_Version(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.logger.log_Error("LOGIN", "La version %1 de Dofus que vous avez installée n'est pas compatible avec ce serveur. Pour jouer, installez la version% 2.");
			cliente.cuenta.desconectar();
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x000276F9 File Offset: 0x00025AF9
		[PaqueteAtributo("AlEb")]
		public void get_Error_Baneado(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.logger.log_Error("LOGIN", "Connexion rejetée. Votre compte a été banni.");
			cliente.cuenta.desconectar();
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00027723 File Offset: 0x00025B23
		[PaqueteAtributo("AlEd")]
		public void get_Error_Conectado(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.logger.log_Error("LOGIN", "Ce compte est déjà connecté à un serveur de jeux. Veuillez réessayer.");
			cliente.cuenta.desconectar();
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00027750 File Offset: 0x00025B50
		[PaqueteAtributo("AlEk")]
		public void get_Error_Baneado_Tiempo(ClienteTcp cliente, string paquete)
		{
			string[] array = paquete.Substring(3).Split(new char[]
			{
				'|'
			});
			int num = int.Parse(array[0].Substring(1));
			int num2 = int.Parse(array[1]);
			int num3 = int.Parse(array[2]);
			StringBuilder stringBuilder = new StringBuilder().Append("Votre compte sera invalide pendant ");
			bool flag = num > 0;
			if (flag)
			{
				stringBuilder.Append(num.ToString() + " jours");
			}
			bool flag2 = num2 > 0;
			if (flag2)
			{
				stringBuilder.Append(num2.ToString() + " heures");
			}
			bool flag3 = num3 > 0;
			if (flag3)
			{
				stringBuilder.Append(num3.ToString() + " minutes");
			}
			cliente.cuenta.logger.log_Error("LOGIN", stringBuilder.ToString());
			cliente.cuenta.desconectar();
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0002783C File Offset: 0x00025C3C
		[PaqueteAtributo("AXEf")]
		public void getServerFull(ClienteTcp cliente, string paquete)
		{
			StringBuilder stringBuilder = new StringBuilder().Append("Serveur FULL ! Abonne toi :)");
			cliente.cuenta.logger.log_Error("LOGIN", stringBuilder.ToString());
		}
	}
}
