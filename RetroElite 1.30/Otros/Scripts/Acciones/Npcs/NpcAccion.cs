using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RetroElite.Otros.Mapas.Entidades;

namespace RetroElite.Otros.Scripts.Acciones.Npcs
{
	// Token: 0x02000029 RID: 41
	public class NpcAccion : AccionesScript
	{
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00006EDC File Offset: 0x000052DC
		// (set) Token: 0x0600017C RID: 380 RVA: 0x00006EE4 File Offset: 0x000052E4
		public int npc_id { get; private set; }

		// Token: 0x0600017D RID: 381 RVA: 0x00006EED File Offset: 0x000052ED
		public NpcAccion(int _npc_id)
		{
			this.npc_id = _npc_id;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00006F00 File Offset: 0x00005300
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
