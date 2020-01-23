using System;
using System.Threading.Tasks;

namespace RetroElite.Otros.Scripts.Acciones.Global
{
	// Token: 0x0200002D RID: 45
	public class DelayAccion : AccionesScript
	{
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600018D RID: 397 RVA: 0x000072CB File Offset: 0x000056CB
		// (set) Token: 0x0600018E RID: 398 RVA: 0x000072D3 File Offset: 0x000056D3
		public int milisegundos { get; private set; }

		// Token: 0x0600018F RID: 399 RVA: 0x000072DC File Offset: 0x000056DC
		public DelayAccion(int ms)
		{
			this.milisegundos = ms;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x000072F0 File Offset: 0x000056F0
		internal override async Task<ResultadosAcciones> proceso(Cuenta cuenta)
		{
			await Task.Delay(this.milisegundos);
			return ResultadosAcciones.HECHO;
		}
	}
}
