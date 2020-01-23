using System;
using System.Collections.Generic;
using System.Linq;

namespace RetroElite.Otros.Mapas.Entidades
{
	// Token: 0x0200004F RID: 79
	public class Monstruos : Entidad, IDisposable
	{
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x0000BE7F File Offset: 0x0000A27F
		// (set) Token: 0x060002DA RID: 730 RVA: 0x0000BE87 File Offset: 0x0000A287
		public int id { get; set; } = 0;

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060002DB RID: 731 RVA: 0x0000BE90 File Offset: 0x0000A290
		// (set) Token: 0x060002DC RID: 732 RVA: 0x0000BE98 File Offset: 0x0000A298
		public int template_id { get; set; } = 0;

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060002DD RID: 733 RVA: 0x0000BEA1 File Offset: 0x0000A2A1
		// (set) Token: 0x060002DE RID: 734 RVA: 0x0000BEA9 File Offset: 0x0000A2A9
		public Celda celda { get; set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060002DF RID: 735 RVA: 0x0000BEB2 File Offset: 0x0000A2B2
		// (set) Token: 0x060002E0 RID: 736 RVA: 0x0000BEBA File Offset: 0x0000A2BA
		public int nivel { get; set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x0000BEC3 File Offset: 0x0000A2C3
		// (set) Token: 0x060002E2 RID: 738 RVA: 0x0000BECB File Offset: 0x0000A2CB
		public List<Monstruos> moobs_dentro_grupo { get; set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x0000BED4 File Offset: 0x0000A2D4
		// (set) Token: 0x060002E4 RID: 740 RVA: 0x0000BEDC File Offset: 0x0000A2DC
		public Monstruos lider_grupo { get; set; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x0000BEE5 File Offset: 0x0000A2E5
		public int get_Total_Monstruos
		{
			get
			{
				return this.moobs_dentro_grupo.Count + 1;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x0000BEF4 File Offset: 0x0000A2F4
		public int get_Total_Nivel_Grupo
		{
			get
			{
				return this.lider_grupo.nivel + this.moobs_dentro_grupo.Sum((Monstruos f) => f.nivel);
			}
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000BF2C File Offset: 0x0000A32C
		public Monstruos(int _id, int _template, Celda _celda, int _nivel)
		{
			this.id = _id;
			this.template_id = _template;
			this.celda = _celda;
			this.moobs_dentro_grupo = new List<Monstruos>();
			this.nivel = _nivel;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000BF7C File Offset: 0x0000A37C
		public bool get_Contiene_Monstruo(int id)
		{
			bool flag = this.lider_grupo.template_id == id;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				for (int i = 0; i < this.moobs_dentro_grupo.Count; i++)
				{
					bool flag2 = this.moobs_dentro_grupo[i].template_id == id;
					if (flag2)
					{
						return true;
					}
				}
				result = false;
			}
			return result;
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000BFDF File Offset: 0x0000A3DF
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000BFEC File Offset: 0x0000A3EC
		~Monstruos()
		{
			this.Dispose(false);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000C01C File Offset: 0x0000A41C
		public virtual void Dispose(bool disposing)
		{
			bool flag = !this.disposed;
			if (flag)
			{
				this.moobs_dentro_grupo.Clear();
				this.moobs_dentro_grupo = null;
				this.disposed = true;
			}
		}

		// Token: 0x04000136 RID: 310
		private bool disposed;
	}
}
