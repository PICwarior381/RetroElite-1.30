using System;
using RetroElite.Otros.Mapas;

namespace RetroElite.Otros.Peleas.Peleadores
{
	// Token: 0x02000035 RID: 53
	public class Luchadores
	{
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001EB RID: 491 RVA: 0x000095C6 File Offset: 0x000079C6
		// (set) Token: 0x060001EC RID: 492 RVA: 0x000095CE File Offset: 0x000079CE
		public int id { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001ED RID: 493 RVA: 0x000095D7 File Offset: 0x000079D7
		// (set) Token: 0x060001EE RID: 494 RVA: 0x000095DF File Offset: 0x000079DF
		public Celda celda { get; set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001EF RID: 495 RVA: 0x000095E8 File Offset: 0x000079E8
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x000095F0 File Offset: 0x000079F0
		public byte equipo { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x000095F9 File Offset: 0x000079F9
		// (set) Token: 0x060001F2 RID: 498 RVA: 0x00009601 File Offset: 0x00007A01
		public bool esta_vivo { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000960A File Offset: 0x00007A0A
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x00009612 File Offset: 0x00007A12
		public int vida_actual { get; set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x0000961B File Offset: 0x00007A1B
		// (set) Token: 0x060001F6 RID: 502 RVA: 0x00009623 File Offset: 0x00007A23
		public int vida_maxima { get; set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x0000962C File Offset: 0x00007A2C
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x00009634 File Offset: 0x00007A34
		public byte pa { get; set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x0000963D File Offset: 0x00007A3D
		// (set) Token: 0x060001FA RID: 506 RVA: 0x00009645 File Offset: 0x00007A45
		public byte pm { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001FB RID: 507 RVA: 0x0000964E File Offset: 0x00007A4E
		// (set) Token: 0x060001FC RID: 508 RVA: 0x00009656 File Offset: 0x00007A56
		public int id_invocador { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001FD RID: 509 RVA: 0x0000965F File Offset: 0x00007A5F
		public int porcentaje_vida
		{
			get
			{
				return (int)((double)this.vida_actual / (double)this.vida_maxima) / 100;
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00009674 File Offset: 0x00007A74
		public Luchadores(int _id, bool _esta_vivo, int _vida_actual, byte _pa, byte _pm, Celda _celda, int _vida_maxima, byte _equipo)
		{
			this.get_Actualizar_Luchador(_id, _esta_vivo, _vida_actual, _pa, _pm, _celda, _vida_maxima, _equipo);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000969C File Offset: 0x00007A9C
		public Luchadores(int _id, bool _esta_vivo, int _vida_actual, byte _pa, byte _pm, Celda _celda, int _vida_maxima, byte _equipo, int _id_invocador)
		{
			this.get_Actualizar_Luchador(_id, _esta_vivo, _vida_actual, _pa, _pm, _celda, _vida_maxima, _equipo, _id_invocador);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x000096C8 File Offset: 0x00007AC8
		public void get_Actualizar_Luchador(int _id, bool _esta_vivo, int _vida_actual, byte _pa, byte _pm, Celda _celda_id, int _vida_maxima, byte _equipo)
		{
			this.id = _id;
			this.esta_vivo = _esta_vivo;
			this.vida_actual = _vida_actual;
			this.pa = _pa;
			this.pm = _pm;
			this.celda = _celda_id;
			this.vida_maxima = _vida_maxima;
			this.equipo = _equipo;
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000971C File Offset: 0x00007B1C
		public void get_Actualizar_Luchador(int _id, bool _esta_vivo, int _vida_actual, byte _pa, byte _pm, Celda _celda, int _vida_maxima, byte _equipo, int _id_invocador)
		{
			this.get_Actualizar_Luchador(_id, _esta_vivo, _vida_actual, _pa, _pm, _celda, _vida_maxima, _equipo);
			this.id_invocador = _id_invocador;
		}
	}
}
