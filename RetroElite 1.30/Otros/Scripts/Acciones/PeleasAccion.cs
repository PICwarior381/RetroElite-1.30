using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RetroElite.Otros.Game.Entidades.Manejadores.Movimientos;
using RetroElite.Otros.Mapas;
using RetroElite.Otros.Mapas.Entidades;

namespace RetroElite.Otros.Scripts.Acciones
{
	// Token: 0x02000026 RID: 38
	public class PeleasAccion : AccionesScript
	{
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00006C05 File Offset: 0x00005005
		// (set) Token: 0x06000168 RID: 360 RVA: 0x00006C0D File Offset: 0x0000500D
		public int monstruos_minimos { get; private set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00006C16 File Offset: 0x00005016
		// (set) Token: 0x0600016A RID: 362 RVA: 0x00006C1E File Offset: 0x0000501E
		public int monstruos_maximos { get; private set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00006C27 File Offset: 0x00005027
		// (set) Token: 0x0600016C RID: 364 RVA: 0x00006C2F File Offset: 0x0000502F
		public int monstruo_nivel_minimo { get; private set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00006C38 File Offset: 0x00005038
		// (set) Token: 0x0600016E RID: 366 RVA: 0x00006C40 File Offset: 0x00005040
		public int monstruo_nivel_maximo { get; private set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00006C49 File Offset: 0x00005049
		// (set) Token: 0x06000170 RID: 368 RVA: 0x00006C51 File Offset: 0x00005051
		public List<int> monstruos_prohibidos { get; private set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00006C5A File Offset: 0x0000505A
		// (set) Token: 0x06000172 RID: 370 RVA: 0x00006C62 File Offset: 0x00005062
		public List<int> monstruos_obligatorios { get; private set; }

		// Token: 0x06000173 RID: 371 RVA: 0x00006C6B File Offset: 0x0000506B
		public PeleasAccion(int _monstruos_minimos, int _monstruos_maximos, int _monstruo_nivel_minimo, int _monstruo_nivel_maximo, List<int> _monstruos_prohibidos, List<int> _monstruos_obligatorios)
		{
			this.monstruos_minimos = _monstruos_minimos;
			this.monstruos_maximos = _monstruos_maximos;
			this.monstruo_nivel_minimo = _monstruo_nivel_minimo;
			this.monstruo_nivel_maximo = _monstruo_nivel_maximo;
			this.monstruos_prohibidos = _monstruos_prohibidos;
			this.monstruos_obligatorios = _monstruos_obligatorios;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00006CA8 File Offset: 0x000050A8
		internal override Task<ResultadosAcciones> proceso(Cuenta cuenta)
		{
			Mapa mapa = cuenta.juego.mapa;
			List<Monstruos> list = mapa.get_Grupo_Monstruos(this.monstruos_minimos, this.monstruos_maximos, this.monstruo_nivel_minimo, this.monstruo_nivel_maximo, this.monstruos_prohibidos, this.monstruos_obligatorios);
			bool flag = list.Count > 0;
			if (flag)
			{
				foreach (Monstruos monstruos in list)
				{
					ResultadoMovimientos resultadoMovimientos = cuenta.juego.manejador.movimientos.get_Mover_A_Celda(monstruos.celda, new List<Celda>(), false, 0);
					Console.WriteLine(resultadoMovimientos);
					switch (resultadoMovimientos)
					{
					case ResultadoMovimientos.EXITO:
						cuenta.logger.log_Script("SCRIPT", string.Format("[MAP] Deplacement vers la cellId {0}, total de monstres: {1}, niveau du groupe: {2}", monstruos.celda.id, monstruos.get_Total_Monstruos, monstruos.get_Total_Nivel_Grupo));
						return AccionesScript.resultado_procesado;
					case ResultadoMovimientos.MISMA_CELDA:
					case ResultadoMovimientos.PATHFINDING_ERROR:
						cuenta.logger.log_Peligro("SCRIPT", "Le chemin pour aller au monstre est bloqué ...");
						continue;
					}
					return AccionesScript.resultado_fallado;
				}
			}
			return AccionesScript.resultado_hecho;
		}
	}
}
