using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using RetroElite.Comun.Network;

namespace RetroElite.Comun.Frames.Transporte
{
	// Token: 0x0200008B RID: 139
	public static class PaqueteRecibido
	{
		// Token: 0x060005BA RID: 1466 RVA: 0x00025D1C File Offset: 0x0002411C
		public static void Inicializar()
		{
			Assembly assembly = typeof(Frame).GetTypeInfo().Assembly;
			foreach (MethodInfo methodInfo in from m in assembly.GetTypes().SelectMany((Type x) => x.GetMethods())
			where m.GetCustomAttributes(typeof(PaqueteAtributo), false).Length != 0
			select m)
			{
				PaqueteAtributo paqueteAtributo = methodInfo.GetCustomAttributes(typeof(PaqueteAtributo), true)[0] as PaqueteAtributo;
				Type type = Type.GetType(methodInfo.DeclaringType.FullName);
				object instancia = Activator.CreateInstance(type, null);
				PaqueteRecibido.metodos.Add(new PaqueteDatos(instancia, paqueteAtributo.paquete, methodInfo));
			}
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x00025E14 File Offset: 0x00024214
		public static void Recibir(ClienteTcp cliente, string paquete)
		{
			PaqueteDatos paqueteDatos = PaqueteRecibido.metodos.Find((PaqueteDatos m) => paquete.StartsWith(m.nombre_paquete));
			bool flag = paqueteDatos != null;
			if (flag)
			{
				paqueteDatos.informacion.Invoke(paqueteDatos.instancia, new object[]
				{
					cliente,
					paquete
				});
			}
		}

		// Token: 0x04000396 RID: 918
		public static readonly List<PaqueteDatos> metodos = new List<PaqueteDatos>();
	}
}
