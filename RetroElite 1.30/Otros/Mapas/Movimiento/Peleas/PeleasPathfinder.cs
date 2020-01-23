using System;
using System.Collections.Generic;
using System.Linq;
using RetroElite.Otros.Peleas;
using RetroElite.Otros.Peleas.Peleadores;

namespace RetroElite.Otros.Mapas.Movimiento.Peleas
{
	// Token: 0x02000048 RID: 72
	internal class PeleasPathfinder
	{
		// Token: 0x060002A5 RID: 677 RVA: 0x0000B090 File Offset: 0x00009490
		public static PeleaCamino get_Path_Pelea(short celda_actual, short celda_objetivo, Dictionary<short, MovimientoNodo> celdas)
		{
			bool flag = !celdas.ContainsKey(celda_objetivo);
			PeleaCamino result;
			if (flag)
			{
				result = null;
			}
			else
			{
				short num = celda_objetivo;
				List<short> list = new List<short>();
				List<short> list2 = new List<short>();
				Dictionary<short, int> dictionary = new Dictionary<short, int>();
				Dictionary<short, int> dictionary2 = new Dictionary<short, int>();
				byte b = 0;
				while (num != celda_actual)
				{
					MovimientoNodo movimientoNodo = celdas[num];
					bool alcanzable = movimientoNodo.alcanzable;
					if (alcanzable)
					{
						list.Insert(0, num);
						dictionary.Add(num, (int)b);
					}
					else
					{
						list2.Insert(0, num);
						dictionary2.Add(num, (int)b);
					}
					num = movimientoNodo.celda_inicial;
					b += 1;
				}
				result = new PeleaCamino
				{
					celdas_accesibles = list,
					celdas_inalcanzables = list2,
					mapa_celdas_alcanzables = dictionary,
					mapa_celdas_inalcanzable = dictionary2
				};
			}
			return result;
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000B164 File Offset: 0x00009564
		public static Dictionary<short, MovimientoNodo> get_Celdas_Accesibles(Pelea pelea, Mapa mapa, Celda celda_actual)
		{
			Dictionary<short, MovimientoNodo> dictionary = new Dictionary<short, MovimientoNodo>();
			bool flag = pelea.jugador_luchador.pm <= 0;
			Dictionary<short, MovimientoNodo> result;
			if (flag)
			{
				result = dictionary;
			}
			else
			{
				short pm = (short)pelea.jugador_luchador.pm;
				List<NodoPelea> list = new List<NodoPelea>();
				Dictionary<short, NodoPelea> dictionary2 = new Dictionary<short, NodoPelea>();
				NodoPelea nodoPelea = new NodoPelea(celda_actual, (int)pm, (int)pelea.jugador_luchador.pa, 1);
				list.Add(nodoPelea);
				dictionary2[celda_actual.id] = nodoPelea;
				while (list.Count > 0)
				{
					NodoPelea nodoPelea2 = list.Last<NodoPelea>();
					list.Remove(nodoPelea2);
					Celda celda = nodoPelea2.celda;
					List<Celda> adyecentes = PeleasPathfinder.get_Celdas_Adyecentes(celda, mapa.celdas);
					int i = 0;
					Func<Luchadores, bool> <>9__0;
					while (i < adyecentes.Count)
					{
						IEnumerable<Luchadores> get_Luchadores = pelea.get_Luchadores;
						Func<Luchadores, bool> predicate;
						if ((predicate = <>9__0) == null)
						{
							predicate = (<>9__0 = delegate(Luchadores f)
							{
								int id = (int)f.celda.id;
								Celda celda2 = adyecentes[i];
								short? num3 = (celda2 != null) ? new short?(celda2.id) : null;
								int? num4 = (num3 != null) ? new int?((int)num3.GetValueOrDefault()) : null;
								return id == num4.GetValueOrDefault() & num4 != null;
							});
						}
						Luchadores luchadores = get_Luchadores.FirstOrDefault(predicate);
						bool flag2 = adyecentes[i] != null && luchadores == null;
						if (flag2)
						{
							int j = i;
							i = j + 1;
						}
						else
						{
							adyecentes.RemoveAt(i);
						}
					}
					int num = nodoPelea2.pm_disponible - 1;
					int pa_disponible = nodoPelea2.pa_disponible;
					int distancia = nodoPelea2.distancia + 1;
					bool alcanzable = num >= 0;
					i = 0;
					while (i < adyecentes.Count)
					{
						bool flag3 = dictionary2.ContainsKey(adyecentes[i].id);
						if (!flag3)
						{
							goto IL_20C;
						}
						NodoPelea nodoPelea3 = dictionary2[adyecentes[i].id];
						bool flag4 = nodoPelea3.pm_disponible > num;
						if (!flag4)
						{
							bool flag5 = nodoPelea3.pm_disponible == num && nodoPelea3.pm_disponible >= pa_disponible;
							if (!flag5)
							{
								goto IL_20C;
							}
						}
						IL_2BA:
						int j = i;
						i = j + 1;
						continue;
						IL_20C:
						bool flag6 = !adyecentes[i].es_Caminable();
						if (flag6)
						{
							goto IL_2BA;
						}
						dictionary[adyecentes[i].id] = new MovimientoNodo(celda.id, alcanzable);
						nodoPelea = new NodoPelea(adyecentes[i], num, pa_disponible, distancia);
						dictionary2[adyecentes[i].id] = nodoPelea;
						bool flag7 = nodoPelea2.distancia < (int)pm;
						if (flag7)
						{
							list.Add(nodoPelea);
						}
						goto IL_2BA;
					}
				}
				foreach (short num2 in dictionary.Keys)
				{
					dictionary[num2].camino = PeleasPathfinder.get_Path_Pelea(celda_actual.id, num2, dictionary);
				}
				result = dictionary;
			}
			return result;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000B4D8 File Offset: 0x000098D8
		public static List<Celda> get_Celdas_Adyecentes(Celda nodo, Celda[] mapa_celdas)
		{
			List<Celda> list = new List<Celda>();
			Celda celda = mapa_celdas.FirstOrDefault((Celda nodec) => nodec.x == nodo.x + 1 && nodec.y == nodo.y);
			Celda celda2 = mapa_celdas.FirstOrDefault((Celda nodec) => nodec.x == nodo.x - 1 && nodec.y == nodo.y);
			Celda celda3 = mapa_celdas.FirstOrDefault((Celda nodec) => nodec.x == nodo.x && nodec.y == nodo.y + 1);
			Celda celda4 = mapa_celdas.FirstOrDefault((Celda nodec) => nodec.x == nodo.x && nodec.y == nodo.y - 1);
			bool flag = celda != null;
			if (flag)
			{
				list.Add(celda);
			}
			bool flag2 = celda2 != null;
			if (flag2)
			{
				list.Add(celda2);
			}
			bool flag3 = celda3 != null;
			if (flag3)
			{
				list.Add(celda3);
			}
			bool flag4 = celda4 != null;
			if (flag4)
			{
				list.Add(celda4);
			}
			return list;
		}
	}
}
