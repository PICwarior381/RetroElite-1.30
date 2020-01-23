using System;
using System.Collections.Generic;
using System.Linq;
using RetroElite.Otros.Mapas;

namespace RetroElite.Otros.Game.Personaje.Hechizos
{
	// Token: 0x02000061 RID: 97
	internal class Shaper
	{
		// Token: 0x06000419 RID: 1049 RVA: 0x0000ECDC File Offset: 0x0000D0DC
		public static IEnumerable<Celda> Circulo(int x, int y, int radio_minimo, int radio_maximo, Mapa mapa)
		{
			List<Celda> list = new List<Celda>();
			bool flag = radio_minimo == 0;
			if (flag)
			{
				list.Add(mapa.get_Celda_Por_Coordenadas(x, y));
			}
			for (int i = (radio_minimo == 0) ? 1 : radio_minimo; i <= radio_maximo; i++)
			{
				for (int j = 0; j < i; j++)
				{
					int num = i - j;
					list.Add(mapa.get_Celda_Por_Coordenadas(x + j, y - num));
					list.Add(mapa.get_Celda_Por_Coordenadas(x + num, y + j));
					list.Add(mapa.get_Celda_Por_Coordenadas(x - j, y + num));
					list.Add(mapa.get_Celda_Por_Coordenadas(x - num, y - j));
				}
			}
			return from c in list
			where c != null
			select c;
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0000EDC0 File Offset: 0x0000D1C0
		public static IEnumerable<Celda> Linea(int x, int y, int radio_minimo, int radio_maximo, Mapa mapa)
		{
			List<Celda> list = new List<Celda>();
			for (int i = radio_minimo; i <= radio_maximo; i++)
			{
				list.Add(mapa.get_Celda_Por_Coordenadas(x * i, y * i));
			}
			return from c in list
			where c != null
			select c;
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0000EE24 File Offset: 0x0000D224
		public static IEnumerable<Celda> Cruz(int x, int y, int radio_minimo, int radio_maximo, Mapa mapa)
		{
			List<Celda> list = new List<Celda>();
			bool flag = radio_minimo == 0;
			if (flag)
			{
				list.Add(mapa.get_Celda_Por_Coordenadas(x, y));
			}
			for (int i = (radio_minimo == 0) ? 1 : radio_minimo; i <= radio_maximo; i++)
			{
				list.Add(mapa.get_Celda_Por_Coordenadas(x - i, y));
				list.Add(mapa.get_Celda_Por_Coordenadas(x + i, y));
				list.Add(mapa.get_Celda_Por_Coordenadas(x, y - i));
				list.Add(mapa.get_Celda_Por_Coordenadas(x, y + i));
			}
			return from c in list
			where c != null
			select c;
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000EEE0 File Offset: 0x0000D2E0
		public static IEnumerable<Celda> Anillo(int x, int y, int radio_minimo, int radio_maximo, Mapa mapa)
		{
			List<Celda> list = new List<Celda>();
			bool flag = radio_minimo == 0;
			if (flag)
			{
				list.Add(mapa.get_Celda_Por_Coordenadas(x, y));
			}
			for (int i = (radio_minimo == 0) ? 1 : radio_minimo; i <= radio_maximo; i++)
			{
				for (int j = 0; j < i; j++)
				{
					int num = i - j;
					list.Add(mapa.get_Celda_Por_Coordenadas(x + j, y - num));
					list.Add(mapa.get_Celda_Por_Coordenadas(x + num, y + j));
					list.Add(mapa.get_Celda_Por_Coordenadas(x - j, y + num));
					list.Add(mapa.get_Celda_Por_Coordenadas(x - num, y - j));
				}
			}
			return from c in list
			where c != null
			select c;
		}
	}
}
