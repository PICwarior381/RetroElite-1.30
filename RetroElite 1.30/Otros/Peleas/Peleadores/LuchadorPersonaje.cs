using System;

namespace RetroElite.Otros.Peleas.Peleadores
{
	// Token: 0x02000036 RID: 54
	public class LuchadorPersonaje : Luchadores
	{
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000202 RID: 514 RVA: 0x00009747 File Offset: 0x00007B47
		// (set) Token: 0x06000203 RID: 515 RVA: 0x0000974F File Offset: 0x00007B4F
		public string nombre { get; private set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000204 RID: 516 RVA: 0x00009758 File Offset: 0x00007B58
		// (set) Token: 0x06000205 RID: 517 RVA: 0x00009760 File Offset: 0x00007B60
		public byte nivel { get; private set; }

		// Token: 0x06000206 RID: 518 RVA: 0x0000976C File Offset: 0x00007B6C
		public LuchadorPersonaje(string _nombre, byte _nivel, Luchadores luchador) : base(luchador.id, luchador.esta_vivo, luchador.vida_actual, luchador.pa, luchador.pm, luchador.celda, luchador.vida_maxima, luchador.equipo)
		{
			this.nombre = _nombre;
			this.nivel = _nivel;
		}
	}
}
