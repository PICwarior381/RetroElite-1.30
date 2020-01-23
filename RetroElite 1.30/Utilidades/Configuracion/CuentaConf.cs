using System;
using System.IO;

namespace RetroElite.Utilidades.Configuracion
{
	// Token: 0x0200000B RID: 11
	public class CuentaConf
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002AC4 File Offset: 0x00000EC4
		// (set) Token: 0x06000036 RID: 54 RVA: 0x00002ACC File Offset: 0x00000ECC
		public string nombre_cuenta { get; set; } = string.Empty;

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002AD5 File Offset: 0x00000ED5
		// (set) Token: 0x06000038 RID: 56 RVA: 0x00002ADD File Offset: 0x00000EDD
		public string password { get; set; } = string.Empty;

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002AE6 File Offset: 0x00000EE6
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00002AEE File Offset: 0x00000EEE
		public string servidor { get; set; } = string.Empty;

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002AF7 File Offset: 0x00000EF7
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00002AFF File Offset: 0x00000EFF
		public string nombre_personaje { get; set; } = string.Empty;

		// Token: 0x0600003D RID: 61 RVA: 0x00002B08 File Offset: 0x00000F08
		public CuentaConf(string _nombre_cuenta, string _password, string _servidor, string _nombre_personaje)
		{
			this.nombre_cuenta = _nombre_cuenta;
			this.password = _password;
			this.servidor = _servidor;
			this.nombre_personaje = _nombre_personaje;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002B6A File Offset: 0x00000F6A
		public void guardar_Cuenta(BinaryWriter bw)
		{
			bw.Write(this.nombre_cuenta);
			bw.Write(this.password);
			bw.Write(this.servidor);
			bw.Write(this.nombre_personaje);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002BA4 File Offset: 0x00000FA4
		public static CuentaConf cargar_Una_Cuenta(BinaryReader br)
		{
			CuentaConf result;
			try
			{
				result = new CuentaConf(br.ReadString(), br.ReadString(), br.ReadString(), br.ReadString());
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002BEC File Offset: 0x00000FEC
		public int get_Servidor_Id()
		{
			int result = 601;
			string servidor = this.servidor;
			string text = servidor;
			if (text != null)
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 703811073U)
				{
					if (num <= 19890187U)
					{
						if (num != 15800944U)
						{
							if (num == 19890187U)
							{
								if (text == "Eratz")
								{
									result = 601;
								}
							}
						}
						else if (text == "Hogmeiser")
						{
							result = 606;
						}
					}
					else if (num != 562859689U)
					{
						if (num != 624135955U)
						{
							if (num == 703811073U)
							{
								if (text == "Droupik")
								{
									result = 607;
								}
							}
						}
						else if (text == "Ayuto")
						{
							result = 608;
						}
					}
					else if (text == "Arty")
					{
						result = 604;
					}
				}
				else if (num <= 3739496927U)
				{
					if (num != 1380765114U)
					{
						if (num != 3450482321U)
						{
							if (num == 3739496927U)
							{
								if (text == "Algathe")
								{
									result = 605;
								}
							}
						}
						else if (text == "Nabur")
						{
							result = 603;
						}
					}
					else if (text == "Henual")
					{
						result = 602;
					}
				}
				else if (num != 3878991803U)
				{
					if (num != 4014150122U)
					{
						if (num == 4274870901U)
						{
							if (text == "Bilby")
							{
								result = 609;
							}
						}
					}
					else if (text == "Clustus")
					{
						result = 610;
					}
				}
				else if (text == "Issering")
				{
					result = 611;
				}
			}
			return result;
		}
	}
}
