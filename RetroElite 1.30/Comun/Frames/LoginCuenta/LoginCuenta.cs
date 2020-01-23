using System;
using RetroElite.Comun.Frames.Transporte;
using RetroElite.Comun.Network;
using RetroElite.Otros;
using RetroElite.Otros.Enums;
using RetroElite.Otros.Game.Servidor;
using RetroElite.Utilidades.Criptografia;

namespace RetroElite.Comun.Frames.LoginCuenta
{
	// Token: 0x02000088 RID: 136
	public class LoginCuenta : Frame
	{
		// Token: 0x060005AA RID: 1450 RVA: 0x00025870 File Offset: 0x00023C70
		[PaqueteAtributo("HC")]
		public void get_Key_BienvenidaAsync(ClienteTcp cliente, string paquete)
		{
			Cuenta cuenta = cliente.cuenta;
			cuenta.Estado_Cuenta = EstadoCuenta.CONECTANDO;
			cuenta.key_bienvenida = paquete.Substring(2);
			cliente.enviar_Paquete("1.30.0", false);
			cliente.enviar_Paquete(cliente.cuenta.configuracion.nombre_cuenta + "\n" + Hash.encriptar_Password(cliente.cuenta.configuracion.password, cliente.cuenta.key_bienvenida), false);
			cliente.enviar_Paquete("Af", false);
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x000258F7 File Offset: 0x00023CF7
		[PaqueteAtributo("Ad")]
		public void get_Apodo(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.apodo = paquete.Substring(2);
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0002590C File Offset: 0x00023D0C
		[PaqueteAtributo("Af")]
		public void get_Fila_Espera_Login(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.logger.log_informacion("File d'attente", "position " + paquete[2].ToString() + "/" + paquete[4].ToString());
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x0002595C File Offset: 0x00023D5C
		[PaqueteAtributo("AH")]
		public void get_Servidor_Estado(ClienteTcp cliente, string paquete)
		{
			Cuenta cuenta = cliente.cuenta;
			string[] array = paquete.Substring(2).Split(new char[]
			{
				'|'
			});
			ServidorJuego servidor = cuenta.juego.servidor;
			bool flag = true;
			foreach (string text in array)
			{
				string[] array3 = text.Split(new char[]
				{
					';'
				});
				int num = int.Parse(array3[0]);
				string text2 = "Issering";
				switch (num)
				{
				case 601:
					text2 = "Eratz";
					break;
				case 602:
					text2 = "Henual";
					break;
				case 603:
					text2 = "Nabur";
					break;
				case 604:
					text2 = "Arty";
					break;
				case 605:
					text2 = "Algathe";
					break;
				case 606:
					text2 = "Hogmeiser";
					break;
				case 607:
					text2 = "Droupik";
					break;
				case 608:
					text2 = "Ayuto";
					break;
				case 609:
					text2 = "Bilby";
					break;
				case 610:
					text2 = "Clustus";
					break;
				case 611:
					text2 = "Issering";
					break;
				}
				EstadosServidor estadosServidor = (EstadosServidor)byte.Parse(array3[1]);
				bool flag2 = num == cuenta.configuracion.get_Servidor_Id();
				if (flag2)
				{
					servidor.actualizar_Datos(num, text2, estadosServidor);
					cuenta.logger.log_informacion("LOGIN", string.Format("Le serveur {0} est {1}", text2, estadosServidor));
					bool flag3 = estadosServidor != EstadosServidor.CONNECTE;
					if (flag3)
					{
						flag = false;
					}
				}
			}
			Console.WriteLine("servidor.estado");
			Console.WriteLine(flag);
			bool flag4 = !flag && servidor.estado == EstadosServidor.CONNECTE;
			if (flag4)
			{
				cliente.enviar_Paquete("Ax", false);
			}
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00025B24 File Offset: 0x00023F24
		[PaqueteAtributo("AQ")]
		public void get_Pregunta_Secreta(ClienteTcp cliente, string paquete)
		{
			bool flag = cliente.cuenta.juego.servidor.estado == EstadosServidor.CONNECTE;
			if (flag)
			{
				cliente.enviar_Paquete("Ax", true);
			}
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x00025B5C File Offset: 0x00023F5C
		[PaqueteAtributo("AxK")]
		public void get_Servidores_Lista(ClienteTcp cliente, string paquete)
		{
			Cuenta cuenta = cliente.cuenta;
			string[] array = paquete.Substring(3).Split(new char[]
			{
				'|'
			});
			int num = 1;
			bool flag = false;
			while (num < array.Length && !flag)
			{
				string[] array2 = array[num].Split(new char[]
				{
					','
				});
				int num2 = int.Parse(array2[0]);
				bool flag2 = num2 == cuenta.juego.servidor.id;
				if (flag2)
				{
					bool flag3 = cuenta.juego.servidor.estado == EstadosServidor.CONNECTE;
					if (flag3)
					{
						flag = true;
						cuenta.juego.personaje.evento_Servidor_Seleccionado();
					}
					else
					{
						cuenta.logger.log_Error("LOGIN", "Serveur non accessible");
					}
				}
				num++;
			}
			bool flag4 = flag;
			if (flag4)
			{
				cliente.enviar_Paquete(string.Format("AX{0}", cuenta.juego.servidor.id), true);
			}
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x00025C60 File Offset: 0x00024060
		[PaqueteAtributo("AXK")]
		public void get_Seleccion_Servidor(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.tiquet_game = paquete.Substring(14);
			cliente.cuenta.cambiando_Al_Servidor_Juego(Hash.desencriptar_Ip(paquete.Substring(3, 8)), Hash.desencriptar_Puerto(paquete.Substring(11, 3).ToCharArray()));
		}
	}
}
