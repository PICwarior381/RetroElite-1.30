using System;

namespace RetroElite.Otros.Mapas.Entidades
{
	// Token: 0x02000051 RID: 81
	public class Personajes : Entidad, IDisposable
	{
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060002FA RID: 762 RVA: 0x0000C14E File Offset: 0x0000A54E
		// (set) Token: 0x060002FB RID: 763 RVA: 0x0000C156 File Offset: 0x0000A556
		public int id { get; set; } = 0;

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060002FC RID: 764 RVA: 0x0000C15F File Offset: 0x0000A55F
		// (set) Token: 0x060002FD RID: 765 RVA: 0x0000C167 File Offset: 0x0000A567
		public string nombre { get; set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060002FE RID: 766 RVA: 0x0000C170 File Offset: 0x0000A570
		// (set) Token: 0x060002FF RID: 767 RVA: 0x0000C178 File Offset: 0x0000A578
		public byte sexo { get; set; } = 0;

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000300 RID: 768 RVA: 0x0000C181 File Offset: 0x0000A581
		// (set) Token: 0x06000301 RID: 769 RVA: 0x0000C189 File Offset: 0x0000A589
		public Celda celda { get; set; }

		// Token: 0x06000302 RID: 770 RVA: 0x0000C192 File Offset: 0x0000A592
		public Personajes(int _id, string _nombre_personaje, byte _sexo, Celda _celda)
		{
			this.id = _id;
			this.nombre = _nombre_personaje;
			this.sexo = _sexo;
			this.celda = _celda;
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000C1CC File Offset: 0x0000A5CC
		~Personajes()
		{
			this.Dispose(false);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000C1FC File Offset: 0x0000A5FC
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000C208 File Offset: 0x0000A608
		public virtual void Dispose(bool disposing)
		{
			bool flag = !this.disposed;
			if (flag)
			{
				this.celda = null;
				this.disposed = true;
			}
		}

		// Token: 0x04000141 RID: 321
		private bool disposed;
	}
}
