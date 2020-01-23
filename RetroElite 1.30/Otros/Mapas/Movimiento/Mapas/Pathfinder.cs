using System;
using System.Collections.Generic;
using System.Linq;

namespace RetroElite.Otros.Mapas.Movimiento.Mapas
{
	// Token: 0x0200004A RID: 74
	public class Pathfinder : IDisposable
	{
		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x0000B5EF File Offset: 0x000099EF
		// (set) Token: 0x060002B1 RID: 689 RVA: 0x0000B5F7 File Offset: 0x000099F7
		private Celda[] celdas { get; set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000B600 File Offset: 0x00009A00
		// (set) Token: 0x060002B3 RID: 691 RVA: 0x0000B608 File Offset: 0x00009A08
		private Mapa mapa { get; set; }

		// Token: 0x060002B4 RID: 692 RVA: 0x0000B611 File Offset: 0x00009A11
		public void set_Mapa(Mapa _mapa)
		{
			this.mapa = _mapa;
			this.celdas = this.mapa.celdas;
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000B630 File Offset: 0x00009A30
		public List<Celda> get_Path(Celda celda_inicio, Celda celda_final, List<Celda> celdas_no_permitidas, bool detener_delante, byte distancia_detener)
		{
			bool flag = celda_inicio == null || celda_final == null;
			List<Celda> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				List<Celda> list = new List<Celda>
				{
					celda_inicio
				};
				bool flag2 = celdas_no_permitidas.Contains(celda_final);
				if (flag2)
				{
					celdas_no_permitidas.Remove(celda_final);
				}
				while (list.Count > 0)
				{
					int index = 0;
					for (int i = 1; i < list.Count; i++)
					{
						bool flag3 = list[i].coste_f < list[index].coste_f;
						if (flag3)
						{
							index = i;
						}
						bool flag4 = list[i].coste_f != list[index].coste_f;
						if (!flag4)
						{
							bool flag5 = list[i].coste_g > list[index].coste_g;
							if (flag5)
							{
								index = i;
							}
							bool flag6 = list[i].coste_g == list[index].coste_g;
							if (flag6)
							{
								index = i;
							}
							bool flag7 = list[i].coste_g == list[index].coste_g;
							if (flag7)
							{
								index = i;
							}
						}
					}
					Celda celda = list[index];
					bool flag8 = detener_delante && this.get_Distancia_Nodos(celda, celda_final) <= (int)distancia_detener && !celda_final.es_Caminable();
					if (flag8)
					{
						return this.get_Camino_Retroceso(celda_inicio, celda);
					}
					bool flag9 = celda == celda_final;
					if (flag9)
					{
						return this.get_Camino_Retroceso(celda_inicio, celda_final);
					}
					list.Remove(celda);
					celdas_no_permitidas.Add(celda);
					foreach (Celda celda2 in this.get_Celdas_Adyecentes(celda))
					{
						bool flag10 = celdas_no_permitidas.Contains(celda2) || !celda2.es_Caminable();
						if (!flag10)
						{
							bool flag11 = celda2.es_Teleport() && celda2 != celda_final;
							if (!flag11)
							{
								int num = celda.coste_g + this.get_Distancia_Nodos(celda2, celda);
								bool flag12 = !list.Contains(celda2);
								if (flag12)
								{
									list.Add(celda2);
								}
								else
								{
									bool flag13 = num >= celda2.coste_g;
									if (flag13)
									{
										continue;
									}
								}
								celda2.coste_g = num;
								celda2.coste_h = this.get_Distancia_Nodos(celda2, celda_final);
								celda2.coste_f = celda2.coste_g + celda2.coste_h;
								celda2.nodo_padre = celda;
							}
						}
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000B8F0 File Offset: 0x00009CF0
		private List<Celda> get_Camino_Retroceso(Celda nodo_inicial, Celda nodo_final)
		{
			Celda celda = nodo_final;
			List<Celda> list = new List<Celda>();
			while (celda != nodo_inicial)
			{
				list.Add(celda);
				celda = celda.nodo_padre;
			}
			list.Add(nodo_inicial);
			list.Reverse();
			return list;
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000B938 File Offset: 0x00009D38
		public List<Celda> get_Celdas_Adyecentes(Celda nodo)
		{
			List<Celda> list = new List<Celda>();
			Celda celda = this.celdas.FirstOrDefault((Celda nodec) => nodec.x == nodo.x + 1 && nodec.y == nodo.y);
			Celda celda2 = this.celdas.FirstOrDefault((Celda nodec) => nodec.x == nodo.x - 1 && nodec.y == nodo.y);
			Celda celda3 = this.celdas.FirstOrDefault((Celda nodec) => nodec.x == nodo.x && nodec.y == nodo.y + 1);
			Celda celda4 = this.celdas.FirstOrDefault((Celda nodec) => nodec.x == nodo.x && nodec.y == nodo.y - 1);
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
			Celda celda5 = this.celdas.FirstOrDefault((Celda nodec) => nodec.x == nodo.x - 1 && nodec.y == nodo.y - 1);
			Celda celda6 = this.celdas.FirstOrDefault((Celda nodec) => nodec.x == nodo.x + 1 && nodec.y == nodo.y + 1);
			Celda celda7 = this.celdas.FirstOrDefault((Celda nodec) => nodec.x == nodo.x - 1 && nodec.y == nodo.y + 1);
			Celda celda8 = this.celdas.FirstOrDefault((Celda nodec) => nodec.x == nodo.x + 1 && nodec.y == nodo.y - 1);
			bool flag5 = celda5 != null;
			if (flag5)
			{
				list.Add(celda5);
			}
			bool flag6 = celda8 != null;
			if (flag6)
			{
				list.Add(celda8);
			}
			bool flag7 = celda6 != null;
			if (flag7)
			{
				list.Add(celda6);
			}
			bool flag8 = celda7 != null;
			if (flag8)
			{
				list.Add(celda7);
			}
			return list;
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000BAC2 File Offset: 0x00009EC2
		private int get_Distancia_Nodos(Celda a, Celda b)
		{
			return (a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000BAFB File Offset: 0x00009EFB
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000BB08 File Offset: 0x00009F08
		~Pathfinder()
		{
			this.Dispose(false);
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000BB38 File Offset: 0x00009F38
		protected virtual void Dispose(bool disposing)
		{
			bool flag = !this.disposed;
			if (flag)
			{
				this.celdas = null;
				this.mapa = null;
				this.disposed = true;
			}
		}

		// Token: 0x04000120 RID: 288
		private bool disposed;
	}
}
