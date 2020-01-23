using System;
using System.Collections.Generic;
using RetroElite.Otros.Mapas;

namespace RetroElite.Otros.Game.Personaje.Hechizos
{
	// Token: 0x0200005E RID: 94
	public class HechizoShape
	{
		// Token: 0x060003FD RID: 1021 RVA: 0x0000EB54 File Offset: 0x0000CF54
		public static IEnumerable<Celda> Get_Lista_Celdas_Rango_Hechizo(Celda celda, HechizoStats spellLevel, Mapa mapa, int rango_adicional = 0)
		{
			int radio_maximo = (int)spellLevel.alcanze_maximo + (spellLevel.es_alcanze_modificable ? rango_adicional : 0);
			bool es_lanzado_linea = spellLevel.es_lanzado_linea;
			IEnumerable<Celda> result;
			if (es_lanzado_linea)
			{
				result = Shaper.Cruz(celda.x, celda.y, (int)spellLevel.alcanze_minimo, radio_maximo, mapa);
			}
			else
			{
				result = Shaper.Anillo(celda.x, celda.y, (int)spellLevel.alcanze_minimo, radio_maximo, mapa);
			}
			return result;
		}
	}
}
