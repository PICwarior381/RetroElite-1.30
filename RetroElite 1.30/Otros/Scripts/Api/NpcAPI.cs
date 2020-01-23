using System;
using System.Linq;
using MoonSharp.Interpreter;
using RetroElite.Otros.Mapas.Entidades;
using RetroElite.Otros.Scripts.Acciones.Npcs;
using RetroElite.Otros.Scripts.Manejadores;

namespace RetroElite.Otros.Scripts.Api
{
	// Token: 0x0200001F RID: 31
	[MoonSharpUserData]
	public class NpcAPI : IDisposable
	{
		// Token: 0x06000141 RID: 321 RVA: 0x000065EE File Offset: 0x000049EE
		public NpcAPI(Cuenta _cuenta, ManejadorAcciones _manejador_acciones)
		{
			this.cuenta = _cuenta;
			this.manejador_acciones = _manejador_acciones;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00006608 File Offset: 0x00004A08
		public bool npcBanque(int npc_id)
		{
			bool flag = npc_id > 0 && this.cuenta.juego.mapa.lista_npcs().FirstOrDefault((Npcs n) => n.npc_modelo_id == npc_id) == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				this.manejador_acciones.enqueue_Accion(new NpcBancoAccion(npc_id), true);
				result = true;
			}
			return result;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00006680 File Offset: 0x00004A80
		public bool parlerNpc(int npc_id)
		{
			bool flag = npc_id > 0 && this.cuenta.juego.mapa.lista_npcs().FirstOrDefault((Npcs n) => n.npc_modelo_id == npc_id) == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				this.manejador_acciones.enqueue_Accion(new NpcAccion(npc_id), true);
				result = true;
			}
			return result;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x000066F5 File Offset: 0x00004AF5
		public void repondre(short respuesta_id)
		{
			this.manejador_acciones.enqueue_Accion(new RespuestaAccion(respuesta_id), true);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000670A File Offset: 0x00004B0A
		public void fermerDialogue(short respuesta_id)
		{
			this.manejador_acciones.enqueue_Accion(new CerrarDialogoAccion(), true);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00006720 File Offset: 0x00004B20
		~NpcAPI()
		{
			this.Dispose(false);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00006750 File Offset: 0x00004B50
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000675C File Offset: 0x00004B5C
		protected virtual void Dispose(bool disposing)
		{
			bool flag = !this.disposed;
			if (flag)
			{
				this.cuenta = null;
				this.manejador_acciones = null;
				this.disposed = true;
			}
		}

		// Token: 0x0400006F RID: 111
		private Cuenta cuenta;

		// Token: 0x04000070 RID: 112
		private ManejadorAcciones manejador_acciones;

		// Token: 0x04000071 RID: 113
		private bool disposed;
	}
}
