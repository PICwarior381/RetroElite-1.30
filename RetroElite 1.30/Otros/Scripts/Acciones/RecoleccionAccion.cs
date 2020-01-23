using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RetroElite.Otros.Scripts.Acciones
{
	// Token: 0x02000027 RID: 39
	internal class RecoleccionAccion : AccionesScript
	{
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00006E10 File Offset: 0x00005210
		// (set) Token: 0x06000176 RID: 374 RVA: 0x00006E18 File Offset: 0x00005218
		public List<short> elementos { get; private set; }

		// Token: 0x06000177 RID: 375 RVA: 0x00006E21 File Offset: 0x00005221
		public RecoleccionAccion(List<short> _elementos)
		{
			this.elementos = _elementos;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00006E34 File Offset: 0x00005234
		internal override Task<ResultadosAcciones> proceso(Cuenta cuenta)
		{
			bool flag = cuenta.juego.manejador.recoleccion.get_Puede_Recolectar(this.elementos);
			Task<ResultadosAcciones> result;
			if (flag)
			{
				bool flag2 = !cuenta.juego.manejador.recoleccion.get_Recolectar(this.elementos);
				if (flag2)
				{
					result = AccionesScript.resultado_fallado;
				}
				else
				{
					result = AccionesScript.resultado_procesado;
				}
			}
			else
			{
				result = AccionesScript.resultado_hecho;
			}
			return result;
		}
	}
}
