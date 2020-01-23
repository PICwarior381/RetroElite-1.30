using System;
using System.Threading.Tasks;

namespace RetroElite.Otros.Scripts.Acciones
{
	// Token: 0x02000025 RID: 37
	internal class CerrarVentanaAccion : AccionesScript
	{
		// Token: 0x06000165 RID: 357 RVA: 0x00006BC0 File Offset: 0x00004FC0
		internal override Task<ResultadosAcciones> proceso(Cuenta cuenta)
		{
			bool flag = cuenta.esta_dialogando();
			Task<ResultadosAcciones> result;
			if (flag)
			{
				cuenta.conexion.enviar_Paquete("EV", false);
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
