using System;
using System.Collections.Generic;

namespace RetroElite.Otros.Mapas.Entidades
{
	// Token: 0x02000050 RID: 80
	public class Npcs : Entidad, IDisposable
	{
		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060002EC RID: 748 RVA: 0x0000C054 File Offset: 0x0000A454
		// (set) Token: 0x060002ED RID: 749 RVA: 0x0000C05C File Offset: 0x0000A45C
		public int id { get; set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000C065 File Offset: 0x0000A465
		// (set) Token: 0x060002EF RID: 751 RVA: 0x0000C06D File Offset: 0x0000A46D
		public int npc_modelo_id { get; private set; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000C076 File Offset: 0x0000A476
		// (set) Token: 0x060002F1 RID: 753 RVA: 0x0000C07E File Offset: 0x0000A47E
		public Celda celda { get; set; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x0000C087 File Offset: 0x0000A487
		// (set) Token: 0x060002F3 RID: 755 RVA: 0x0000C08F File Offset: 0x0000A48F
		public short pregunta { get; set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0000C098 File Offset: 0x0000A498
		// (set) Token: 0x060002F5 RID: 757 RVA: 0x0000C0A0 File Offset: 0x0000A4A0
		public List<short> respuestas { get; set; }

		// Token: 0x060002F6 RID: 758 RVA: 0x0000C0A9 File Offset: 0x0000A4A9
		public Npcs(int _id, int _npc_modelo_id, Celda _celda)
		{
			this.id = _id;
			this.npc_modelo_id = _npc_modelo_id;
			this.celda = _celda;
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000C0CC File Offset: 0x0000A4CC
		~Npcs()
		{
			this.Dispose(false);
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000C0FC File Offset: 0x0000A4FC
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000C108 File Offset: 0x0000A508
		public virtual void Dispose(bool disposing)
		{
			bool flag = !this.disposed;
			if (flag)
			{
				List<short> respuestas = this.respuestas;
				if (respuestas != null)
				{
					respuestas.Clear();
				}
				this.respuestas = null;
				this.celda = null;
				this.disposed = true;
			}
		}

		// Token: 0x0400013C RID: 316
		private bool disposed;
	}
}
