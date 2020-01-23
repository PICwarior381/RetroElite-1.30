using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RetroElite.Otros.Mapas.Entidades;

namespace RetroElite.Otros.Scripts.Acciones.Npcs
{
	// Token: 0x0200002B RID: 43
	public class RespuestaAccion : AccionesScript
	{
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000185 RID: 389 RVA: 0x000070DD File Offset: 0x000054DD
		// (set) Token: 0x06000186 RID: 390 RVA: 0x000070E5 File Offset: 0x000054E5
		public short respuesta_id { get; private set; }

		// Token: 0x06000187 RID: 391 RVA: 0x000070EE File Offset: 0x000054EE
		public RespuestaAccion(short _respuesta_id)
		{
			this.respuesta_id = _respuesta_id;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00007100 File Offset: 0x00005500
		internal override Task<ResultadosAcciones> proceso(Cuenta cuenta)
		{
			bool flag = !cuenta.esta_dialogando();
			Task<ResultadosAcciones> result;
			if (flag)
			{
				result = AccionesScript.resultado_fallado;
			}
			else
			{
				IEnumerable<Npcs> source = cuenta.juego.mapa.lista_npcs();
				Npcs npcs = source.ElementAt((int)(cuenta.juego.personaje.hablando_npc_id * -1 - 1));
				bool flag2 = npcs == null;
				if (flag2)
				{
					result = AccionesScript.resultado_fallado;
				}
				else
				{
					bool flag3 = this.respuesta_id < 0;
					if (flag3)
					{
						int num = (int)(this.respuesta_id * -1 - 1);
						bool flag4 = npcs.respuestas.Count <= num;
						if (flag4)
						{
							return AccionesScript.resultado_fallado;
						}
						this.respuesta_id = npcs.respuestas[num];
					}
					bool flag5 = !npcs.respuestas.Contains(this.respuesta_id);
					if (flag5)
					{
						result = AccionesScript.resultado_fallado;
					}
					else
					{
						cuenta.conexion.enviar_Paquete("DR" + npcs.pregunta.ToString() + "|" + this.respuesta_id.ToString(), true);
						result = AccionesScript.resultado_procesado;
					}
				}
			}
			return result;
		}
	}
}
