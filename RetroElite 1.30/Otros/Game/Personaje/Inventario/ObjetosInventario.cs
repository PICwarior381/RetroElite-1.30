using System;
using System.IO;
using System.Xml.Linq;
using RetroElite.Otros.Game.Personaje.Inventario.Enums;

namespace RetroElite.Otros.Game.Personaje.Inventario
{
	// Token: 0x0200005A RID: 90
	public class ObjetosInventario
	{
		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x0000E858 File Offset: 0x0000CC58
		// (set) Token: 0x060003E4 RID: 996 RVA: 0x0000E860 File Offset: 0x0000CC60
		public uint id_inventario { get; private set; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x0000E869 File Offset: 0x0000CC69
		// (set) Token: 0x060003E6 RID: 998 RVA: 0x0000E871 File Offset: 0x0000CC71
		public int id_modelo { get; private set; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x0000E87A File Offset: 0x0000CC7A
		// (set) Token: 0x060003E8 RID: 1000 RVA: 0x0000E882 File Offset: 0x0000CC82
		public string nombre { get; private set; } = "Desconocido";

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0000E88B File Offset: 0x0000CC8B
		// (set) Token: 0x060003EA RID: 1002 RVA: 0x0000E893 File Offset: 0x0000CC93
		public int cantidad { get; set; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x0000E89C File Offset: 0x0000CC9C
		// (set) Token: 0x060003EC RID: 1004 RVA: 0x0000E8A4 File Offset: 0x0000CCA4
		public InventarioPosiciones posicion { get; set; } = InventarioPosiciones.PAS_EQUIPE;

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x0000E8AD File Offset: 0x0000CCAD
		// (set) Token: 0x060003EE RID: 1006 RVA: 0x0000E8B5 File Offset: 0x0000CCB5
		public short pods { get; private set; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x0000E8BE File Offset: 0x0000CCBE
		// (set) Token: 0x060003F0 RID: 1008 RVA: 0x0000E8C6 File Offset: 0x0000CCC6
		public short nivel { get; private set; } = 0;

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x0000E8CF File Offset: 0x0000CCCF
		// (set) Token: 0x060003F2 RID: 1010 RVA: 0x0000E8D7 File Offset: 0x0000CCD7
		public byte tipo { get; private set; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x0000E8E0 File Offset: 0x0000CCE0
		public short vida_regenerada { get; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x0000E8E8 File Offset: 0x0000CCE8
		// (set) Token: 0x060003F5 RID: 1013 RVA: 0x0000E8F0 File Offset: 0x0000CCF0
		public TipoObjetosInventario tipo_inventario { get; private set; } = TipoObjetosInventario.INCONNU;

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000E8FC File Offset: 0x0000CCFC
		public ObjetosInventario(string paquete)
		{
			string[] array = paquete.Split(new char[]
			{
				'~'
			});
			this.id_inventario = Convert.ToUInt32(array[0], 16);
			this.id_modelo = Convert.ToInt32(array[1], 16);
			this.cantidad = Convert.ToInt32(array[2], 16);
			bool flag = !string.IsNullOrEmpty(array[3]);
			if (flag)
			{
				this.posicion = (InventarioPosiciones)Convert.ToSByte(array[3], 16);
			}
			string[] array2 = array[4].Split(new char[]
			{
				','
			});
			foreach (string text in array2)
			{
				string[] array4 = text.Split(new char[]
				{
					'#'
				});
				string value = array4[0];
				bool flag2 = string.IsNullOrEmpty(value);
				if (!flag2)
				{
					int num = Convert.ToInt32(value, 16);
					bool flag3 = num == 110;
					if (flag3)
					{
						this.vida_regenerada = Convert.ToInt16(array4[1], 16);
					}
				}
			}
			FileInfo fileInfo = new FileInfo("items/" + this.id_modelo.ToString() + ".xml");
			bool exists = fileInfo.Exists;
			if (exists)
			{
				this.archivo_objeto = XElement.Load(fileInfo.FullName);
				this.nombre = this.archivo_objeto.Element("NOMBRE").Value;
				this.pods = short.Parse(this.archivo_objeto.Element("PODS").Value);
				this.tipo = byte.Parse(this.archivo_objeto.Element("TIPO").Value);
				this.nivel = short.Parse(this.archivo_objeto.Element("NIVEL").Value);
				this.tipo_inventario = InventarioUtiles.get_Objetos_Inventario(this.tipo);
				this.archivo_objeto = null;
			}
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000EB0D File Offset: 0x0000CF0D
		public bool objeto_esta_equipado()
		{
			return this.posicion > InventarioPosiciones.PAS_EQUIPE;
		}

		// Token: 0x0400019F RID: 415
		private readonly XElement archivo_objeto;
	}
}
