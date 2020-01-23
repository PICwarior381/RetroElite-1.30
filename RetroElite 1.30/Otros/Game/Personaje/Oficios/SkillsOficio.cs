using System;
using RetroElite.Otros.Mapas.Interactivo;

namespace RetroElite.Otros.Game.Personaje.Oficios
{
	// Token: 0x02000057 RID: 87
	public class SkillsOficio
	{
		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x0000D8CB File Offset: 0x0000BCCB
		// (set) Token: 0x060003B1 RID: 945 RVA: 0x0000D8D3 File Offset: 0x0000BCD3
		public short id { get; private set; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x0000D8DC File Offset: 0x0000BCDC
		// (set) Token: 0x060003B3 RID: 947 RVA: 0x0000D8E4 File Offset: 0x0000BCE4
		public byte cantidad_minima { get; private set; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x0000D8ED File Offset: 0x0000BCED
		// (set) Token: 0x060003B5 RID: 949 RVA: 0x0000D8F5 File Offset: 0x0000BCF5
		public byte cantidad_maxima { get; private set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x0000D8FE File Offset: 0x0000BCFE
		// (set) Token: 0x060003B7 RID: 951 RVA: 0x0000D906 File Offset: 0x0000BD06
		public ObjetoInteractivoModelo interactivo_modelo { get; private set; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x0000D90F File Offset: 0x0000BD0F
		// (set) Token: 0x060003B9 RID: 953 RVA: 0x0000D917 File Offset: 0x0000BD17
		public bool es_craft { get; private set; } = true;

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060003BA RID: 954 RVA: 0x0000D920 File Offset: 0x0000BD20
		// (set) Token: 0x060003BB RID: 955 RVA: 0x0000D928 File Offset: 0x0000BD28
		public float tiempo { get; private set; }

		// Token: 0x060003BC RID: 956 RVA: 0x0000D934 File Offset: 0x0000BD34
		public SkillsOficio(short _id, byte _cantidad_minima, byte _cantidad_maxima, float _tiempo)
		{
			this.id = _id;
			this.cantidad_minima = _cantidad_minima;
			this.cantidad_maxima = _cantidad_maxima;
			ObjetoInteractivoModelo objetoInteractivoModelo = ObjetoInteractivoModelo.get_Modelo_Por_Habilidad(this.id);
			bool flag = objetoInteractivoModelo != null;
			if (flag)
			{
				this.interactivo_modelo = objetoInteractivoModelo;
				bool recolectable = this.interactivo_modelo.recolectable;
				if (recolectable)
				{
					this.es_craft = false;
				}
			}
			this.tiempo = _tiempo;
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000D9A6 File Offset: 0x0000BDA6
		public void set_Actualizar(short _id, byte _cantidad_minima, byte _cantidad_maxima, float _tiempo)
		{
			this.id = _id;
			this.cantidad_minima = _cantidad_minima;
			this.cantidad_maxima = _cantidad_maxima;
			this.tiempo = _tiempo;
		}
	}
}
