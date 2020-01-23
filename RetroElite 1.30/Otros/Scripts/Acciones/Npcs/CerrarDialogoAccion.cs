using System;
using System.Threading.Tasks;

namespace RetroElite.Otros.Scripts.Acciones.Npcs
{
	// Token: 0x02000028 RID: 40
	internal class CerrarDialogoAccion : AccionesScript
	{
		// Token: 0x06000179 RID: 377 RVA: 0x00006EA0 File Offset: 0x000052A0
		internal override Task<ResultadosAcciones> proceso(Cuenta cuenta)
		{
			bool flag = cuenta.esta_dialogando();
			Task<ResultadosAcciones> result;
			if (flag)
			{
				cuenta.conexion.enviar_Paquete("DV", true);
				result = AccionesScript.resultado_procesado;
			}
			else
			{
				result = AccionesScript.resultado_hecho;
			}
			return result;
		}
	}
}
