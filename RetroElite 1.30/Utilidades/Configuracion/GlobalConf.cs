using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RetroElite.Utilidades.Configuracion
{
	// Token: 0x0200000A RID: 10
	internal class GlobalConf
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000028B0 File Offset: 0x00000CB0
		// (set) Token: 0x0600002B RID: 43 RVA: 0x000028B7 File Offset: 0x00000CB7
		public static bool mostrar_mensajes_debug { get; set; }

		// Token: 0x0600002C RID: 44 RVA: 0x000028BF File Offset: 0x00000CBF
		static GlobalConf()
		{
			GlobalConf.lista_cuentas = new List<CuentaConf>();
			GlobalConf.mostrar_mensajes_debug = false;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000028FC File Offset: 0x00000CFC
		public static void cargar_Todas_Cuentas()
		{
			bool flag = File.Exists(GlobalConf.ruta_archivo_cuentas);
			if (flag)
			{
				GlobalConf.lista_cuentas.Clear();
				using (BinaryReader binaryReader = new BinaryReader(File.Open(GlobalConf.ruta_archivo_cuentas, FileMode.Open)))
				{
					int num = binaryReader.ReadInt32();
					for (int i = 0; i < num; i++)
					{
						GlobalConf.lista_cuentas.Add(CuentaConf.cargar_Una_Cuenta(binaryReader));
					}
					GlobalConf.mostrar_mensajes_debug = binaryReader.ReadBoolean();
					GlobalConf.ip_conexion = binaryReader.ReadString();
					GlobalConf.puerto_conexion = binaryReader.ReadInt16();
				}
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000029A8 File Offset: 0x00000DA8
		public static void guardar_Configuracion()
		{
			using (BinaryWriter bw = new BinaryWriter(File.Open(GlobalConf.ruta_archivo_cuentas, FileMode.Create)))
			{
				bw.Write(GlobalConf.lista_cuentas.Count);
				GlobalConf.lista_cuentas.ForEach(delegate(CuentaConf a)
				{
					a.guardar_Cuenta(bw);
				});
				bw.Write(GlobalConf.mostrar_mensajes_debug);
				bw.Write(GlobalConf.ip_conexion);
				bw.Write(GlobalConf.puerto_conexion);
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002A5C File Offset: 0x00000E5C
		public static void agregar_Cuenta(string nombre_cuenta, string password, string servidor, string nombre_personaje)
		{
			GlobalConf.lista_cuentas.Add(new CuentaConf(nombre_cuenta, password, servidor, nombre_personaje));
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002A72 File Offset: 0x00000E72
		public static void eliminar_Cuenta(int cuenta_index)
		{
			GlobalConf.lista_cuentas.RemoveAt(cuenta_index);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002A80 File Offset: 0x00000E80
		public static CuentaConf get_Cuenta(string nombre_cuenta)
		{
			return GlobalConf.lista_cuentas.FirstOrDefault((CuentaConf cuenta) => cuenta.nombre_cuenta == nombre_cuenta);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002AB0 File Offset: 0x00000EB0
		public static CuentaConf get_Cuenta(int cuenta_index)
		{
			return GlobalConf.lista_cuentas.ElementAt(cuenta_index);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002ABD File Offset: 0x00000EBD
		public static List<CuentaConf> get_Lista_Cuentas()
		{
			return GlobalConf.lista_cuentas;
		}

		// Token: 0x04000013 RID: 19
		private static List<CuentaConf> lista_cuentas;

		// Token: 0x04000014 RID: 20
		private static readonly string ruta_archivo_cuentas = Path.Combine(Directory.GetCurrentDirectory(), "cuentas.bot");

		// Token: 0x04000016 RID: 22
		public static string ip_conexion = "34.251.172.139";

		// Token: 0x04000017 RID: 23
		public static short puerto_conexion = 443;
	}
}
