using System;
using System.Threading.Tasks;
using RetroElite.Otros.Game.Personaje.Inventario;

namespace RetroElite.Otros.Scripts.Acciones.Inventario
{
	// Token: 0x0200002E RID: 46
	public class UtilizarObjetoAccion : AccionesScript
	{
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000191 RID: 401 RVA: 0x0000733E File Offset: 0x0000573E
		// (set) Token: 0x06000192 RID: 402 RVA: 0x00007346 File Offset: 0x00005746
		public int modelo_id { get; private set; }

		// Token: 0x06000193 RID: 403 RVA: 0x0000734F File Offset: 0x0000574F
		public UtilizarObjetoAccion(int _modelo_id)
		{
			this.modelo_id = _modelo_id;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00007360 File Offset: 0x00005760
		internal override async Task<ResultadosAcciones> proceso(Cuenta cuenta)
		{
			ObjetosInventario objeto = cuenta.juego.personaje.inventario.get_Objeto_Modelo_Id(this.modelo_id);
			bool flag = objeto != null;
			if (flag)
			{
				cuenta.juego.personaje.inventario.utilizar_Objeto(objeto);
				await Task.Delay(800);
			}
			return ResultadosAcciones.HECHO;
		}
	}
}
