using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using RetroElite.Properties;

namespace RetroElite.Otros.Game.Personaje.Oficios
{
	// Token: 0x02000056 RID: 86
	public class Oficio
	{
		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600039E RID: 926 RVA: 0x0000D732 File Offset: 0x0000BB32
		// (set) Token: 0x0600039F RID: 927 RVA: 0x0000D73A File Offset: 0x0000BB3A
		public int id { get; private set; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x0000D743 File Offset: 0x0000BB43
		// (set) Token: 0x060003A1 RID: 929 RVA: 0x0000D74B File Offset: 0x0000BB4B
		public byte nivel { get; set; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x0000D754 File Offset: 0x0000BB54
		// (set) Token: 0x060003A3 RID: 931 RVA: 0x0000D75C File Offset: 0x0000BB5C
		public string nombre { get; private set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x0000D765 File Offset: 0x0000BB65
		// (set) Token: 0x060003A5 RID: 933 RVA: 0x0000D76D File Offset: 0x0000BB6D
		public uint experiencia_base { get; private set; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x0000D776 File Offset: 0x0000BB76
		// (set) Token: 0x060003A7 RID: 935 RVA: 0x0000D77E File Offset: 0x0000BB7E
		public uint experiencia_actual { get; private set; }

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x0000D787 File Offset: 0x0000BB87
		// (set) Token: 0x060003A9 RID: 937 RVA: 0x0000D78F File Offset: 0x0000BB8F
		public uint experiencia_siguiente_nivel { get; private set; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060003AA RID: 938 RVA: 0x0000D798 File Offset: 0x0000BB98
		// (set) Token: 0x060003AB RID: 939 RVA: 0x0000D7A0 File Offset: 0x0000BBA0
		public List<SkillsOficio> skills { get; private set; }

		// Token: 0x060003AC RID: 940 RVA: 0x0000D7A9 File Offset: 0x0000BBA9
		public Oficio(int _id)
		{
			this.id = _id;
			this.nombre = this.get_Nombre_Oficio(this.id);
			this.skills = new List<SkillsOficio>();
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000D7DC File Offset: 0x0000BBDC
		private string get_Nombre_Oficio(int id_oficio)
		{
			return (from e in (from e in XElement.Parse(Resources.oficios).Elements("OFICIO")
			where int.Parse(e.Element("id").Value) == id_oficio
			select e).Elements("nombre")
			select e.Value).FirstOrDefault<string>();
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060003AE RID: 942 RVA: 0x0000D858 File Offset: 0x0000BC58
		public double get_Experiencia_Porcentaje
		{
			get
			{
				return (this.experiencia_actual == 0U) ? 0.0 : Math.Round((this.experiencia_actual - this.experiencia_base) / (this.experiencia_siguiente_nivel - this.experiencia_base) * 100.0, 2);
			}
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000D8A7 File Offset: 0x0000BCA7
		public void set_Actualizar_Oficio(byte _nivel, uint _experiencia_base, uint _experiencia_actual, uint _experiencia_siguiente_nivel)
		{
			this.nivel = _nivel;
			this.experiencia_base = _experiencia_base;
			this.experiencia_actual = _experiencia_actual;
			this.experiencia_siguiente_nivel = _experiencia_siguiente_nivel;
		}
	}
}
