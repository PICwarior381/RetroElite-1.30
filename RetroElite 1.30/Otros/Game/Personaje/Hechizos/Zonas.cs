using System;
using RetroElite.Utilidades.Criptografia;

namespace RetroElite.Otros.Game.Personaje.Hechizos
{
	// Token: 0x02000062 RID: 98
	public class Zonas
	{
		// Token: 0x17000150 RID: 336
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x0000EFC1 File Offset: 0x0000D3C1
		// (set) Token: 0x0600041F RID: 1055 RVA: 0x0000EFC9 File Offset: 0x0000D3C9
		public HechizoZona tipo { get; set; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x0000EFD2 File Offset: 0x0000D3D2
		// (set) Token: 0x06000421 RID: 1057 RVA: 0x0000EFDA File Offset: 0x0000D3DA
		public int tamano { get; set; }

		// Token: 0x06000422 RID: 1058 RVA: 0x0000EFE3 File Offset: 0x0000D3E3
		public Zonas(HechizoZona _tipo, int _tamano)
		{
			this.tipo = _tipo;
			this.tamano = _tamano;
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0000F000 File Offset: 0x0000D400
		public static Zonas Parse(string str)
		{
			bool flag = str.Length != 2;
			if (flag)
			{
				throw new ArgumentException("zona invalida");
			}
			char c = str[0];
			char c2 = c;
			HechizoZona tipo;
			if (c2 != 'C')
			{
				switch (c2)
				{
				case 'L':
					tipo = HechizoZona.LINEA;
					goto IL_83;
				case 'M':
				case 'N':
				case 'Q':
				case 'S':
					break;
				case 'O':
					tipo = HechizoZona.ANILLO;
					goto IL_83;
				case 'P':
					tipo = HechizoZona.SOLO;
					goto IL_83;
				case 'R':
					tipo = HechizoZona.RECTANGULO;
					goto IL_83;
				case 'T':
					tipo = HechizoZona.TLINEA;
					goto IL_83;
				default:
					if (c2 == 'X')
					{
						tipo = HechizoZona.CRUZADO;
						goto IL_83;
					}
					break;
				}
				tipo = HechizoZona.SOLO;
			}
			else
			{
				tipo = HechizoZona.CIRCULO;
			}
			IL_83:
			return new Zonas(tipo, (int)Hash.get_Hash(str[1]));
		}
	}
}
