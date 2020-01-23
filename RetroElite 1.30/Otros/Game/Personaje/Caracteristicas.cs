using System;

namespace RetroElite.Otros.Game.Personaje
{
	// Token: 0x02000055 RID: 85
	public class Caracteristicas : IEliminable
	{
		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000373 RID: 883 RVA: 0x0000D3E9 File Offset: 0x0000B7E9
		// (set) Token: 0x06000374 RID: 884 RVA: 0x0000D3F1 File Offset: 0x0000B7F1
		public double experiencia_actual { get; set; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000375 RID: 885 RVA: 0x0000D3FA File Offset: 0x0000B7FA
		// (set) Token: 0x06000376 RID: 886 RVA: 0x0000D402 File Offset: 0x0000B802
		public double experiencia_minima_nivel { get; set; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000377 RID: 887 RVA: 0x0000D40B File Offset: 0x0000B80B
		// (set) Token: 0x06000378 RID: 888 RVA: 0x0000D413 File Offset: 0x0000B813
		public double experiencia_siguiente_nivel { get; set; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000379 RID: 889 RVA: 0x0000D41C File Offset: 0x0000B81C
		// (set) Token: 0x0600037A RID: 890 RVA: 0x0000D424 File Offset: 0x0000B824
		public int energia_actual { get; set; }

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600037B RID: 891 RVA: 0x0000D42D File Offset: 0x0000B82D
		// (set) Token: 0x0600037C RID: 892 RVA: 0x0000D435 File Offset: 0x0000B835
		public int points_sorts { get; set; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600037D RID: 893 RVA: 0x0000D43E File Offset: 0x0000B83E
		// (set) Token: 0x0600037E RID: 894 RVA: 0x0000D446 File Offset: 0x0000B846
		public int maxima_energia { get; set; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600037F RID: 895 RVA: 0x0000D44F File Offset: 0x0000B84F
		// (set) Token: 0x06000380 RID: 896 RVA: 0x0000D457 File Offset: 0x0000B857
		public int vitalidad_actual { get; set; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000381 RID: 897 RVA: 0x0000D460 File Offset: 0x0000B860
		// (set) Token: 0x06000382 RID: 898 RVA: 0x0000D468 File Offset: 0x0000B868
		public int vitalidad_maxima { get; set; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000383 RID: 899 RVA: 0x0000D471 File Offset: 0x0000B871
		// (set) Token: 0x06000384 RID: 900 RVA: 0x0000D479 File Offset: 0x0000B879
		public PersonajeStats iniciativa { get; set; }

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000385 RID: 901 RVA: 0x0000D482 File Offset: 0x0000B882
		// (set) Token: 0x06000386 RID: 902 RVA: 0x0000D48A File Offset: 0x0000B88A
		public PersonajeStats prospeccion { get; set; }

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000387 RID: 903 RVA: 0x0000D493 File Offset: 0x0000B893
		// (set) Token: 0x06000388 RID: 904 RVA: 0x0000D49B File Offset: 0x0000B89B
		public PersonajeStats puntos_accion { get; set; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000389 RID: 905 RVA: 0x0000D4A4 File Offset: 0x0000B8A4
		// (set) Token: 0x0600038A RID: 906 RVA: 0x0000D4AC File Offset: 0x0000B8AC
		public PersonajeStats puntos_movimiento { get; set; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600038B RID: 907 RVA: 0x0000D4B5 File Offset: 0x0000B8B5
		// (set) Token: 0x0600038C RID: 908 RVA: 0x0000D4BD File Offset: 0x0000B8BD
		public PersonajeStats vitalidad { get; set; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600038D RID: 909 RVA: 0x0000D4C6 File Offset: 0x0000B8C6
		// (set) Token: 0x0600038E RID: 910 RVA: 0x0000D4CE File Offset: 0x0000B8CE
		public PersonajeStats sabiduria { get; set; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600038F RID: 911 RVA: 0x0000D4D7 File Offset: 0x0000B8D7
		// (set) Token: 0x06000390 RID: 912 RVA: 0x0000D4DF File Offset: 0x0000B8DF
		public PersonajeStats fuerza { get; set; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000391 RID: 913 RVA: 0x0000D4E8 File Offset: 0x0000B8E8
		// (set) Token: 0x06000392 RID: 914 RVA: 0x0000D4F0 File Offset: 0x0000B8F0
		public PersonajeStats inteligencia { get; set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000393 RID: 915 RVA: 0x0000D4F9 File Offset: 0x0000B8F9
		// (set) Token: 0x06000394 RID: 916 RVA: 0x0000D501 File Offset: 0x0000B901
		public PersonajeStats suerte { get; set; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000395 RID: 917 RVA: 0x0000D50A File Offset: 0x0000B90A
		// (set) Token: 0x06000396 RID: 918 RVA: 0x0000D512 File Offset: 0x0000B912
		public PersonajeStats agilidad { get; set; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000397 RID: 919 RVA: 0x0000D51B File Offset: 0x0000B91B
		// (set) Token: 0x06000398 RID: 920 RVA: 0x0000D523 File Offset: 0x0000B923
		public PersonajeStats alcanze { get; set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000399 RID: 921 RVA: 0x0000D52C File Offset: 0x0000B92C
		// (set) Token: 0x0600039A RID: 922 RVA: 0x0000D534 File Offset: 0x0000B934
		public PersonajeStats criaturas_invocables { get; set; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600039B RID: 923 RVA: 0x0000D53D File Offset: 0x0000B93D
		public int porcentaje_vida
		{
			get
			{
				return (this.vitalidad_maxima == 0) ? 0 : ((int)((double)this.vitalidad_actual / (double)this.vitalidad_maxima * 100.0));
			}
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000D564 File Offset: 0x0000B964
		public Caracteristicas()
		{
			this.iniciativa = new PersonajeStats(0, 0, 0, 0);
			this.prospeccion = new PersonajeStats(0, 0, 0, 0);
			this.puntos_accion = new PersonajeStats(0, 0, 0, 0);
			this.puntos_movimiento = new PersonajeStats(0, 0, 0, 0);
			this.vitalidad = new PersonajeStats(0, 0, 0, 0);
			this.sabiduria = new PersonajeStats(0, 0, 0, 0);
			this.fuerza = new PersonajeStats(0, 0, 0, 0);
			this.inteligencia = new PersonajeStats(0, 0, 0, 0);
			this.suerte = new PersonajeStats(0, 0, 0, 0);
			this.agilidad = new PersonajeStats(0, 0, 0, 0);
			this.alcanze = new PersonajeStats(0, 0, 0, 0);
			this.criaturas_invocables = new PersonajeStats(0, 0, 0, 0);
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000D63C File Offset: 0x0000BA3C
		public void limpiar()
		{
			this.experiencia_actual = 0.0;
			this.experiencia_minima_nivel = 0.0;
			this.experiencia_siguiente_nivel = 0.0;
			this.energia_actual = 0;
			this.points_sorts = 0;
			this.maxima_energia = 0;
			this.vitalidad_actual = 0;
			this.vitalidad_maxima = 0;
			this.iniciativa.limpiar();
			this.prospeccion.limpiar();
			this.puntos_accion.limpiar();
			this.puntos_movimiento.limpiar();
			this.vitalidad.limpiar();
			this.sabiduria.limpiar();
			this.fuerza.limpiar();
			this.inteligencia.limpiar();
			this.suerte.limpiar();
			this.agilidad.limpiar();
			this.alcanze.limpiar();
			this.criaturas_invocables.limpiar();
		}
	}
}
