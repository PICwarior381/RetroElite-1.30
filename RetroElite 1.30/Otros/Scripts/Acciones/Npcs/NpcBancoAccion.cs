using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RetroElite.Otros.Mapas.Entidades;

namespace RetroElite.Otros.Scripts.Acciones.Npcs
{
	// Token: 0x0200002A RID: 42
	public class NpcBancoAccion : AccionesScript
	{
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00006FDD File Offset: 0x000053DD
		// (set) Token: 0x06000181 RID: 385 RVA: 0x00006FE5 File Offset: 0x000053E5
		public int npc_id { get; private set; }

		// Token: 0x06000182 RID: 386 RVA: 0x00006FEE File Offset: 0x000053EE
		public NpcBancoAccion(int _npc_id)
		{
			this.npc_id = _npc_id;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00007000 File Offset: 0x00005400
		internal override Task<ResultadosAcciones> proceso(Cuenta cuenta)
		{
			bool flag = cuenta.esta_ocupado();
			Task<ResultadosAcciones> result;
			if (flag)
			{
				result = AccionesScript.resultado_fallado;
			}
			else
			{
				IEnumerable<Npcs> source = cuenta.juego.mapa.lista_npcs();
				bool flag2 = this.npc_id < 0;
				Npcs npcs;
				if (flag2)
				{
					int num = this.npc_id * -1 - 1;
					bool flag3 = source.Count<Npcs>() <= num;
					if (flag3)
					{
						return AccionesScript.resultado_fallado;
					}
					npcs = source.ElementAt(num);
				}
				else
				{
					npcs = source.FirstOrDefault((Npcs n) => n.npc_modelo_id == this.npc_id);
				}
				bool flag4 = npcs == null;
				if (flag4)
				{
					result = AccionesScript.resultado_fallado;
				}
				else
				{
					cuenta.conexion.enviar_Paquete("DC" + npcs.id.ToString(), true);
					result = AccionesScript.resultado_procesado;
				}
			}
			return result;
		}
	}
}
