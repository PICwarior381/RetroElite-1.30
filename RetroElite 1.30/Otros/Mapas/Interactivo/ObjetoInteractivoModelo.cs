using System;
using System.Collections.Generic;
using System.Linq;

namespace RetroElite.Otros.Mapas.Interactivo
{
	// Token: 0x0200004D RID: 77
	public class ObjetoInteractivoModelo
	{
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x0000BC02 File Offset: 0x0000A002
		// (set) Token: 0x060002C7 RID: 711 RVA: 0x0000BC0A File Offset: 0x0000A00A
		public short[] gfxs { get; private set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x0000BC13 File Offset: 0x0000A013
		// (set) Token: 0x060002C9 RID: 713 RVA: 0x0000BC1B File Offset: 0x0000A01B
		public bool caminable { get; private set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060002CA RID: 714 RVA: 0x0000BC24 File Offset: 0x0000A024
		// (set) Token: 0x060002CB RID: 715 RVA: 0x0000BC2C File Offset: 0x0000A02C
		public short[] habilidades { get; private set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060002CC RID: 716 RVA: 0x0000BC35 File Offset: 0x0000A035
		// (set) Token: 0x060002CD RID: 717 RVA: 0x0000BC3D File Offset: 0x0000A03D
		public string nombre { get; private set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060002CE RID: 718 RVA: 0x0000BC46 File Offset: 0x0000A046
		// (set) Token: 0x060002CF RID: 719 RVA: 0x0000BC4E File Offset: 0x0000A04E
		public bool recolectable { get; private set; }

		// Token: 0x060002D0 RID: 720 RVA: 0x0000BC58 File Offset: 0x0000A058
		public ObjetoInteractivoModelo(string _nombre, string _gfx, bool _caminable, string _habilidades, bool _recolectable)
		{
			this.nombre = _nombre;
			bool flag = !_gfx.Equals("-1") && !string.IsNullOrEmpty(_gfx);
			if (flag)
			{
				string[] array = _gfx.Split(new char[]
				{
					','
				});
				this.gfxs = new short[array.Length];
				byte b = 0;
				while ((int)b < this.gfxs.Length)
				{
					this.gfxs[(int)b] = short.Parse(array[(int)b]);
					b += 1;
				}
			}
			this.caminable = _caminable;
			bool flag2 = !_habilidades.Equals("-1") && !string.IsNullOrEmpty(_habilidades);
			if (flag2)
			{
				string[] array2 = _habilidades.Split(new char[]
				{
					','
				});
				this.habilidades = new short[array2.Length];
				byte b2 = 0;
				while ((int)b2 < this.habilidades.Length)
				{
					this.habilidades[(int)b2] = short.Parse(array2[(int)b2]);
					b2 += 1;
				}
			}
			this.recolectable = _recolectable;
			ObjetoInteractivoModelo.interactivos_modelo_cargados.Add(this);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000BD74 File Offset: 0x0000A174
		public static ObjetoInteractivoModelo get_Modelo_Por_Gfx(short gfx_id)
		{
			foreach (ObjetoInteractivoModelo objetoInteractivoModelo in ObjetoInteractivoModelo.interactivos_modelo_cargados)
			{
				bool flag = objetoInteractivoModelo.gfxs.Contains(gfx_id);
				if (flag)
				{
					return objetoInteractivoModelo;
				}
			}
			return null;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000BDE0 File Offset: 0x0000A1E0
		public static ObjetoInteractivoModelo get_Modelo_Por_Habilidad(short habilidad_id)
		{
			IEnumerable<ObjetoInteractivoModelo> enumerable = from i in ObjetoInteractivoModelo.interactivos_modelo_cargados
			where i.habilidades != null
			select i;
			foreach (ObjetoInteractivoModelo objetoInteractivoModelo in enumerable)
			{
				bool flag = objetoInteractivoModelo.habilidades.Contains(habilidad_id);
				if (flag)
				{
					return objetoInteractivoModelo;
				}
			}
			return null;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000BE6C File Offset: 0x0000A26C
		public static List<ObjetoInteractivoModelo> get_Interactivos_Modelos_Cargados()
		{
			return ObjetoInteractivoModelo.interactivos_modelo_cargados;
		}

		// Token: 0x0400012F RID: 303
		private static List<ObjetoInteractivoModelo> interactivos_modelo_cargados = new List<ObjetoInteractivoModelo>();
	}
}
