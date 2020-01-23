using System;
using System.Collections.Generic;
using RetroElite.Otros.Game.Personaje.Inventario.Enums;

namespace RetroElite.Otros.Game.Personaje.Inventario
{
	// Token: 0x02000059 RID: 89
	public static class InventarioUtiles
	{
		// Token: 0x060003E0 RID: 992 RVA: 0x0000E435 File Offset: 0x0000C835
		public static List<InventarioPosiciones> get_Posibles_Posiciones(int tipo_objeto)
		{
			return InventarioUtiles.possibles_posiciones.ContainsKey(tipo_objeto) ? InventarioUtiles.possibles_posiciones[tipo_objeto] : null;
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000E454 File Offset: 0x0000C854
		public static TipoObjetosInventario get_Objetos_Inventario(byte tipo)
		{
			switch (tipo)
			{
			case 1:
			case 2:
			case 3:
			case 4:
			case 5:
			case 6:
			case 7:
			case 8:
			case 9:
			case 10:
			case 11:
			case 16:
			case 17:
			case 18:
			case 19:
			case 20:
			case 21:
			case 22:
			case 83:
				return TipoObjetosInventario.EQUIPEMENTS;
			case 12:
			case 13:
			case 85:
			case 86:
				return TipoObjetosInventario.CONSOMMABLES;
			case 15:
			case 33:
			case 34:
			case 35:
			case 36:
			case 38:
			case 41:
			case 46:
			case 47:
			case 48:
			case 50:
			case 51:
			case 53:
			case 54:
			case 55:
			case 56:
			case 57:
			case 58:
			case 59:
			case 60:
			case 65:
			case 68:
			case 84:
			case 96:
			case 98:
			case 100:
			case 103:
			case 104:
			case 105:
			case 106:
			case 107:
			case 108:
			case 109:
			case 111:
				return TipoObjetosInventario.RESSOURCES;
			case 24:
				return TipoObjetosInventario.OBJETS_QUETE;
			}
			return TipoObjetosInventario.INCONNU;
		}

		// Token: 0x04000194 RID: 404
		private static Dictionary<int, List<InventarioPosiciones>> possibles_posiciones = new Dictionary<int, List<InventarioPosiciones>>
		{
			{
				1,
				new List<InventarioPosiciones>
				{
					InventarioPosiciones.AMULETTE
				}
			},
			{
				2,
				new List<InventarioPosiciones>
				{
					InventarioPosiciones.CAC
				}
			},
			{
				3,
				new List<InventarioPosiciones>
				{
					InventarioPosiciones.CAC
				}
			},
			{
				4,
				new List<InventarioPosiciones>
				{
					InventarioPosiciones.CAC
				}
			},
			{
				5,
				new List<InventarioPosiciones>
				{
					InventarioPosiciones.CAC
				}
			},
			{
				6,
				new List<InventarioPosiciones>
				{
					InventarioPosiciones.CAC
				}
			},
			{
				7,
				new List<InventarioPosiciones>
				{
					InventarioPosiciones.CAC
				}
			},
			{
				8,
				new List<InventarioPosiciones>
				{
					InventarioPosiciones.CAC
				}
			},
			{
				9,
				new List<InventarioPosiciones>
				{
					InventarioPosiciones.ANNEAU_1,
					InventarioPosiciones.ANNEAU_2
				}
			},
			{
				10,
				new List<InventarioPosiciones>
				{
					InventarioPosiciones.CEINTURE
				}
			},
			{
				11,
				new List<InventarioPosiciones>
				{
					InventarioPosiciones.BOTTE
				}
			},
			{
				16,
				new List<InventarioPosiciones>
				{
					InventarioPosiciones.CHAPEAU
				}
			},
			{
				17,
				new List<InventarioPosiciones>
				{
					InventarioPosiciones.CAPE
				}
			},
			{
				18,
				new List<InventarioPosiciones>
				{
					InventarioPosiciones.FAMILLIER
				}
			},
			{
				19,
				new List<InventarioPosiciones>
				{
					InventarioPosiciones.CAC
				}
			},
			{
				20,
				new List<InventarioPosiciones>
				{
					InventarioPosiciones.CAC
				}
			},
			{
				21,
				new List<InventarioPosiciones>
				{
					InventarioPosiciones.CAC
				}
			},
			{
				22,
				new List<InventarioPosiciones>
				{
					InventarioPosiciones.CAC
				}
			},
			{
				23,
				new List<InventarioPosiciones>
				{
					InventarioPosiciones.DOFUS1,
					InventarioPosiciones.DOFUS2,
					InventarioPosiciones.DOFUS3,
					InventarioPosiciones.DOFUS4,
					InventarioPosiciones.DOFUS5,
					InventarioPosiciones.DOFUS6
				}
			},
			{
				82,
				new List<InventarioPosiciones>
				{
					InventarioPosiciones.BOUCLIER
				}
			},
			{
				83,
				new List<InventarioPosiciones>
				{
					InventarioPosiciones.CAC
				}
			}
		};
	}
}
