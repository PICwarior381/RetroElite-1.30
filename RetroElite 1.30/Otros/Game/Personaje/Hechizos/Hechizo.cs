using System;
using System.Collections.Generic;

namespace RetroElite.Otros.Game.Personaje.Hechizos
{
	// Token: 0x02000063 RID: 99
	public class Hechizo
	{
		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x0000F0A8 File Offset: 0x0000D4A8
		// (set) Token: 0x06000425 RID: 1061 RVA: 0x0000F0B0 File Offset: 0x0000D4B0
		public short id { get; private set; }

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x0000F0B9 File Offset: 0x0000D4B9
		// (set) Token: 0x06000427 RID: 1063 RVA: 0x0000F0C1 File Offset: 0x0000D4C1
		public string nombre { get; private set; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x0000F0CA File Offset: 0x0000D4CA
		// (set) Token: 0x06000429 RID: 1065 RVA: 0x0000F0D2 File Offset: 0x0000D4D2
		public byte nivel { get; set; }

		// Token: 0x0600042A RID: 1066 RVA: 0x0000F0DB File Offset: 0x0000D4DB
		public Hechizo(short _id, string _nombre)
		{
			this.id = _id;
			this.nombre = _nombre;
			this.statsHechizos = new Dictionary<byte, HechizoStats>();
			Hechizo.hechizos_cargados.Add(this.id, this);
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0000F114 File Offset: 0x0000D514
		public void get_Agregar_Hechizo_Stats(byte _nivel, HechizoStats stats)
		{
			bool flag = this.statsHechizos.ContainsKey(_nivel);
			if (flag)
			{
				this.statsHechizos.Remove(_nivel);
			}
			this.statsHechizos.Add(_nivel, stats);
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000F14D File Offset: 0x0000D54D
		public HechizoStats get_Stats()
		{
			return this.statsHechizos[this.nivel];
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0000F160 File Offset: 0x0000D560
		public static Hechizo get_Hechizo(short id)
		{
			bool flag = Hechizo.hechizos_cargados.ContainsKey(id);
			Hechizo result;
			if (flag)
			{
				result = Hechizo.hechizos_cargados[id];
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x040001D3 RID: 467
		public Dictionary<byte, HechizoStats> statsHechizos;

		// Token: 0x040001D4 RID: 468
		public static Dictionary<short, Hechizo> hechizos_cargados = new Dictionary<short, Hechizo>();
	}
}
