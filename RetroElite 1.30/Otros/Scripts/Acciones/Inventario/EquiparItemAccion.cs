using System;
using System.Threading.Tasks;
using RetroElite.Otros.Game.Personaje.Inventario;

namespace RetroElite.Otros.Scripts.Acciones.Inventario
{
	// Token: 0x0200002F RID: 47
	internal class EquiparItemAccion : AccionesScript
	{
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000195 RID: 405 RVA: 0x000073AE File Offset: 0x000057AE
		// (set) Token: 0x06000196 RID: 406 RVA: 0x000073B6 File Offset: 0x000057B6
		public int modelo_id { get; private set; }

		// Token: 0x06000197 RID: 407 RVA: 0x000073BF File Offset: 0x000057BF
		public EquiparItemAccion(int _modelo_id)
		{
			this.modelo_id = _modelo_id;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x000073D4 File Offset: 0x000057D4
		internal override async Task<ResultadosAcciones> proceso(Cuenta cuenta)
		{
			ObjetosInventario objeto = cuenta.juego.personaje.inventario.get_Objeto_Modelo_Id(this.modelo_id);
			bool flag = objeto != null && cuenta.juego.personaje.inventario.equipar_Objeto(objeto);
			if (flag)
			{
				await Task.Delay(500);
			}
			return ResultadosAcciones.HECHO;
		}
	}
}
