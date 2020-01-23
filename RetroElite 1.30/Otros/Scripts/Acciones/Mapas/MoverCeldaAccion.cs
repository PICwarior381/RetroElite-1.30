using System;
using System.Threading.Tasks;
using RetroElite.Otros.Game.Entidades.Manejadores.Movimientos;
using RetroElite.Otros.Mapas;

namespace RetroElite.Otros.Scripts.Acciones.Mapas
{
	// Token: 0x0200002C RID: 44
	public class MoverCeldaAccion : AccionesScript
	{
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000189 RID: 393 RVA: 0x0000721D File Offset: 0x0000561D
		// (set) Token: 0x0600018A RID: 394 RVA: 0x00007225 File Offset: 0x00005625
		public short celda_id { get; private set; }

		// Token: 0x0600018B RID: 395 RVA: 0x0000722E File Offset: 0x0000562E
		public MoverCeldaAccion(short _celda_id)
		{
			this.celda_id = _celda_id;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00007240 File Offset: 0x00005640
		internal override Task<ResultadosAcciones> proceso(Cuenta cuenta)
		{
			Mapa mapa = cuenta.juego.mapa;
			Celda celda = mapa.get_Celda_Id(this.celda_id);
			bool flag = celda == null;
			Task<ResultadosAcciones> result;
			if (flag)
			{
				result = AccionesScript.resultado_fallado;
			}
			else
			{
				ResultadoMovimientos resultadoMovimientos = cuenta.juego.manejador.movimientos.get_Mover_A_Celda(celda, cuenta.juego.mapa.celdas_ocupadas(), false, 0);
				ResultadoMovimientos resultadoMovimientos2 = resultadoMovimientos;
				if (resultadoMovimientos2 != ResultadoMovimientos.EXITO)
				{
					if (resultadoMovimientos2 != ResultadoMovimientos.MISMA_CELDA)
					{
						result = AccionesScript.resultado_fallado;
					}
					else
					{
						result = AccionesScript.resultado_hecho;
					}
				}
				else
				{
					result = AccionesScript.resultado_procesado;
				}
			}
			return result;
		}
	}
}
