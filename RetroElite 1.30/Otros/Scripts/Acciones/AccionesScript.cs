using System;
using System.Threading.Tasks;

namespace RetroElite.Otros.Scripts.Acciones
{
	// Token: 0x02000022 RID: 34
	public abstract class AccionesScript
	{
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00006963 File Offset: 0x00004D63
		protected static Task<ResultadosAcciones> resultado_hecho
		{
			get
			{
				return Task.FromResult<ResultadosAcciones>(ResultadosAcciones.HECHO);
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000158 RID: 344 RVA: 0x0000696B File Offset: 0x00004D6B
		protected static Task<ResultadosAcciones> resultado_procesado
		{
			get
			{
				return Task.FromResult<ResultadosAcciones>(ResultadosAcciones.PROCESANDO);
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00006973 File Offset: 0x00004D73
		protected static Task<ResultadosAcciones> resultado_fallado
		{
			get
			{
				return Task.FromResult<ResultadosAcciones>(ResultadosAcciones.FALLO);
			}
		}

		// Token: 0x0600015A RID: 346
		internal abstract Task<ResultadosAcciones> proceso(Cuenta cuenta);
	}
}
