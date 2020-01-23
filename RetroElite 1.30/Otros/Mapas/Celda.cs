using System;
using System.Linq;
using RetroElite.Otros.Mapas.Interactivo;

namespace RetroElite.Otros.Mapas
{
	// Token: 0x02000041 RID: 65
	public class Celda
	{
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600023F RID: 575 RVA: 0x0000A06E File Offset: 0x0000846E
		// (set) Token: 0x06000240 RID: 576 RVA: 0x0000A076 File Offset: 0x00008476
		public short id { get; private set; } = 0;

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000241 RID: 577 RVA: 0x0000A07F File Offset: 0x0000847F
		// (set) Token: 0x06000242 RID: 578 RVA: 0x0000A087 File Offset: 0x00008487
		public bool activa { get; private set; } = false;

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0000A090 File Offset: 0x00008490
		// (set) Token: 0x06000244 RID: 580 RVA: 0x0000A098 File Offset: 0x00008498
		public TipoCelda tipo { get; private set; } = TipoCelda.NO_CAMINABLE;

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000245 RID: 581 RVA: 0x0000A0A1 File Offset: 0x000084A1
		// (set) Token: 0x06000246 RID: 582 RVA: 0x0000A0A9 File Offset: 0x000084A9
		public bool es_linea_vision { get; private set; } = false;

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0000A0B2 File Offset: 0x000084B2
		// (set) Token: 0x06000248 RID: 584 RVA: 0x0000A0BA File Offset: 0x000084BA
		public byte layer_ground_nivel { get; private set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0000A0C3 File Offset: 0x000084C3
		// (set) Token: 0x0600024A RID: 586 RVA: 0x0000A0CB File Offset: 0x000084CB
		public byte layer_ground_slope { get; private set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600024B RID: 587 RVA: 0x0000A0D4 File Offset: 0x000084D4
		// (set) Token: 0x0600024C RID: 588 RVA: 0x0000A0DC File Offset: 0x000084DC
		public short layer_object_1_num { get; private set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600024D RID: 589 RVA: 0x0000A0E5 File Offset: 0x000084E5
		// (set) Token: 0x0600024E RID: 590 RVA: 0x0000A0ED File Offset: 0x000084ED
		public short layer_object_2_num { get; private set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600024F RID: 591 RVA: 0x0000A0F6 File Offset: 0x000084F6
		// (set) Token: 0x06000250 RID: 592 RVA: 0x0000A0FE File Offset: 0x000084FE
		public ObjetoInteractivo objeto_interactivo { get; private set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000251 RID: 593 RVA: 0x0000A107 File Offset: 0x00008507
		// (set) Token: 0x06000252 RID: 594 RVA: 0x0000A10F File Offset: 0x0000850F
		public int x { get; private set; } = 0;

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000253 RID: 595 RVA: 0x0000A118 File Offset: 0x00008518
		// (set) Token: 0x06000254 RID: 596 RVA: 0x0000A120 File Offset: 0x00008520
		public int y { get; private set; } = 0;

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000255 RID: 597 RVA: 0x0000A129 File Offset: 0x00008529
		// (set) Token: 0x06000256 RID: 598 RVA: 0x0000A131 File Offset: 0x00008531
		public int coste_h { get; set; } = 0;

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000257 RID: 599 RVA: 0x0000A13A File Offset: 0x0000853A
		// (set) Token: 0x06000258 RID: 600 RVA: 0x0000A142 File Offset: 0x00008542
		public int coste_g { get; set; } = 0;

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000259 RID: 601 RVA: 0x0000A14B File Offset: 0x0000854B
		// (set) Token: 0x0600025A RID: 602 RVA: 0x0000A153 File Offset: 0x00008553
		public int coste_f { get; set; } = 0;

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600025B RID: 603 RVA: 0x0000A15C File Offset: 0x0000855C
		// (set) Token: 0x0600025C RID: 604 RVA: 0x0000A164 File Offset: 0x00008564
		public Celda nodo_padre { get; set; } = null;

		// Token: 0x0600025D RID: 605 RVA: 0x0000A170 File Offset: 0x00008570
		public Celda(short _id, bool esta_activa, TipoCelda _tipo, bool _es_linea_vision, byte _nivel, byte _slope, short _objeto_interactivo_id, short _layer_object_1_num, short _layer_object_2_num, Mapa _mapa)
		{
			this.id = _id;
			this.activa = esta_activa;
			this.tipo = _tipo;
			this.layer_object_1_num = _layer_object_1_num;
			this.layer_object_2_num = _layer_object_2_num;
			this.es_linea_vision = _es_linea_vision;
			this.layer_ground_nivel = _nivel;
			this.layer_ground_slope = _slope;
			bool flag = _objeto_interactivo_id != -1;
			if (flag)
			{
				this.objeto_interactivo = new ObjetoInteractivo(_objeto_interactivo_id, this);
				_mapa.interactivos.TryAdd((int)this.id, this.objeto_interactivo);
			}
			byte anchura = _mapa.anchura;
			int num = (int)(this.id / (short)(anchura * 2 - 1));
			int num2 = (int)this.id - num * (int)(anchura * 2 - 1);
			int num3 = num2 % (int)anchura;
			this.y = num - num3;
			this.x = ((int)this.id - (int)(anchura - 1) * this.y) / (int)anchura;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000A294 File Offset: 0x00008694
		public int get_Distancia_Entre_Dos_Casillas(Celda destino)
		{
			return Math.Abs(this.x - destino.x) + Math.Abs(this.y - destino.y);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000A2BB File Offset: 0x000086BB
		public bool get_Esta_En_Linea(Celda destino)
		{
			return this.x == destino.x || this.y == destino.y;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000A2DC File Offset: 0x000086DC
		public char get_Direccion_Char(Celda celda)
		{
			bool flag = this.x == celda.x;
			char result;
			if (flag)
			{
				result = ((celda.y < this.y) ? 'd' : 'h');
			}
			else
			{
				bool flag2 = this.y == celda.y;
				if (flag2)
				{
					result = ((celda.x < this.x) ? 'b' : 'f');
				}
				else
				{
					bool flag3 = this.x > celda.x;
					if (flag3)
					{
						result = ((this.y > celda.y) ? 'c' : 'a');
					}
					else
					{
						bool flag4 = this.x < celda.x;
						if (!flag4)
						{
							throw new Exception("Error direccion no encontrada");
						}
						result = ((this.y < celda.y) ? 'g' : 'e');
					}
				}
			}
			return result;
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000A39F File Offset: 0x0000879F
		public bool es_Teleport()
		{
			return Celda.texturas_teleport.Contains((int)this.layer_object_1_num) || Celda.texturas_teleport.Contains((int)this.layer_object_2_num);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000A3C6 File Offset: 0x000087C6
		public bool es_Interactivo()
		{
			return this.tipo == TipoCelda.OBJETO_INTERACTIVO || this.objeto_interactivo != null;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000A3DD File Offset: 0x000087DD
		public bool es_Caminable()
		{
			return this.activa && this.tipo != TipoCelda.NO_CAMINABLE && !this.es_Interactivo_Caminable();
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000A3FB File Offset: 0x000087FB
		public bool es_Interactivo_Caminable()
		{
			return this.tipo == TipoCelda.OBJETO_INTERACTIVO || (this.objeto_interactivo != null && !this.objeto_interactivo.modelo.caminable);
		}

		// Token: 0x040000FA RID: 250
		public static readonly int[] texturas_teleport = new int[]
		{
			1030,
			1029,
			1764,
			2298,
			745
		};
	}
}
