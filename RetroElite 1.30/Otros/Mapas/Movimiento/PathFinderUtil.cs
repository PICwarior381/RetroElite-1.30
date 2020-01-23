using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RetroElite.Otros.Mapas.Movimiento.Mapas;
using RetroElite.Utilidades.Criptografia;

namespace RetroElite.Otros.Mapas.Movimiento
{
	// Token: 0x02000044 RID: 68
	internal class PathFinderUtil
	{
		// Token: 0x06000288 RID: 648 RVA: 0x0000ACDC File Offset: 0x000090DC
		public static int get_Tiempo_Desplazamiento_Mapa(Celda celda_actual, List<Celda> celdas_camino, bool con_montura = false)
		{
			int num = 20;
			DuracionAnimacion duracionAnimacion;
			if (con_montura)
			{
				duracionAnimacion = PathFinderUtil.tiempo_tipo_animacion[TipoAnimacion.MONTURA];
			}
			else
			{
				duracionAnimacion = ((celdas_camino.Count > 6) ? PathFinderUtil.tiempo_tipo_animacion[TipoAnimacion.CORRIENDO] : PathFinderUtil.tiempo_tipo_animacion[TipoAnimacion.CAMINANDO]);
			}
			for (int i = 1; i < celdas_camino.Count; i++)
			{
				Celda celda = celdas_camino[i];
				bool flag = celda_actual.y == celda.y;
				if (flag)
				{
					num += duracionAnimacion.horizontal;
				}
				else
				{
					bool flag2 = celda_actual.x == celda.y;
					if (flag2)
					{
						num += duracionAnimacion.vertical;
					}
					else
					{
						num += duracionAnimacion.lineal;
					}
				}
				bool flag3 = celda_actual.layer_ground_nivel < celda.layer_ground_nivel;
				if (flag3)
				{
					num += 100;
				}
				else
				{
					bool flag4 = celda.layer_ground_nivel > celda_actual.layer_ground_nivel;
					if (flag4)
					{
						num -= 100;
					}
					else
					{
						bool flag5 = celda_actual.layer_ground_slope != celda.layer_ground_slope;
						if (flag5)
						{
							bool flag6 = celda_actual.layer_ground_slope == 1;
							if (flag6)
							{
								num += 100;
							}
							else
							{
								bool flag7 = celda.layer_ground_slope == 1;
								if (flag7)
								{
									num -= 100;
								}
							}
						}
					}
				}
				celda_actual = celda;
			}
			return num;
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000AE18 File Offset: 0x00009218
		public static string get_Pathfinding_Limpio(List<Celda> camino)
		{
			Celda celda = camino.Last<Celda>();
			bool flag = camino.Count <= 2;
			string result;
			if (flag)
			{
				result = celda.get_Direccion_Char(camino.First<Celda>()).ToString() + Hash.get_Celda_Char(celda.id);
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				char c = camino[1].get_Direccion_Char(camino.First<Celda>());
				for (int i = 2; i < camino.Count; i++)
				{
					Celda celda2 = camino[i];
					Celda celda3 = camino[i - 1];
					char c2 = celda2.get_Direccion_Char(celda3);
					bool flag2 = c != c2;
					if (flag2)
					{
						stringBuilder.Append(c);
						stringBuilder.Append(Hash.get_Celda_Char(celda3.id));
						c = c2;
					}
				}
				stringBuilder.Append(c);
				stringBuilder.Append(Hash.get_Celda_Char(celda.id));
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x0400010F RID: 271
		private static readonly Dictionary<TipoAnimacion, DuracionAnimacion> tiempo_tipo_animacion = new Dictionary<TipoAnimacion, DuracionAnimacion>
		{
			{
				TipoAnimacion.MONTURA,
				new DuracionAnimacion(135, 200, 120)
			},
			{
				TipoAnimacion.CORRIENDO,
				new DuracionAnimacion(170, 255, 150)
			},
			{
				TipoAnimacion.CAMINANDO,
				new DuracionAnimacion(480, 510, 425)
			},
			{
				TipoAnimacion.FANTASMA,
				new DuracionAnimacion(57, 85, 50)
			}
		};
	}
}
