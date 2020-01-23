using System;
using System.Collections.Generic;

namespace RetroElite.Otros.Game.Personaje.Hechizos
{
	// Token: 0x0200005F RID: 95
	public class HechizoStats
	{
		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x0000EBB9 File Offset: 0x0000CFB9
		// (set) Token: 0x06000400 RID: 1024 RVA: 0x0000EBC1 File Offset: 0x0000CFC1
		public byte coste_pa { get; set; }

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x0000EBCA File Offset: 0x0000CFCA
		// (set) Token: 0x06000402 RID: 1026 RVA: 0x0000EBD2 File Offset: 0x0000CFD2
		public byte alcanze_minimo { get; set; }

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x0000EBDB File Offset: 0x0000CFDB
		// (set) Token: 0x06000404 RID: 1028 RVA: 0x0000EBE3 File Offset: 0x0000CFE3
		public byte alcanze_maximo { get; set; }

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x0000EBEC File Offset: 0x0000CFEC
		// (set) Token: 0x06000406 RID: 1030 RVA: 0x0000EBF4 File Offset: 0x0000CFF4
		public bool es_lanzado_linea { get; set; }

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x0000EBFD File Offset: 0x0000CFFD
		// (set) Token: 0x06000408 RID: 1032 RVA: 0x0000EC05 File Offset: 0x0000D005
		public bool es_lanzado_con_vision { get; set; }

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x0000EC0E File Offset: 0x0000D00E
		// (set) Token: 0x0600040A RID: 1034 RVA: 0x0000EC16 File Offset: 0x0000D016
		public bool es_celda_vacia { get; set; }

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x0000EC1F File Offset: 0x0000D01F
		// (set) Token: 0x0600040C RID: 1036 RVA: 0x0000EC27 File Offset: 0x0000D027
		public bool es_alcanze_modificable { get; set; }

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x0000EC30 File Offset: 0x0000D030
		// (set) Token: 0x0600040E RID: 1038 RVA: 0x0000EC38 File Offset: 0x0000D038
		public byte lanzamientos_por_turno { get; set; }

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x0000EC41 File Offset: 0x0000D041
		// (set) Token: 0x06000410 RID: 1040 RVA: 0x0000EC49 File Offset: 0x0000D049
		public byte lanzamientos_por_objetivo { get; set; }

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x0000EC52 File Offset: 0x0000D052
		// (set) Token: 0x06000412 RID: 1042 RVA: 0x0000EC5A File Offset: 0x0000D05A
		public byte intervalo { get; set; }

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x0000EC63 File Offset: 0x0000D063
		// (set) Token: 0x06000414 RID: 1044 RVA: 0x0000EC6B File Offset: 0x0000D06B
		public List<HechizoEfecto> efectos_normales { get; private set; }

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x0000EC74 File Offset: 0x0000D074
		// (set) Token: 0x06000416 RID: 1046 RVA: 0x0000EC7C File Offset: 0x0000D07C
		public List<HechizoEfecto> efectos_criticos { get; private set; }

		// Token: 0x06000417 RID: 1047 RVA: 0x0000EC85 File Offset: 0x0000D085
		public HechizoStats()
		{
			this.efectos_normales = new List<HechizoEfecto>();
			this.efectos_criticos = new List<HechizoEfecto>();
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000ECA8 File Offset: 0x0000D0A8
		public void agregar_efecto(HechizoEfecto effect, bool es_critico)
		{
			bool flag = !es_critico;
			if (flag)
			{
				this.efectos_normales.Add(effect);
			}
			else
			{
				this.efectos_criticos.Add(effect);
			}
		}
	}
}
