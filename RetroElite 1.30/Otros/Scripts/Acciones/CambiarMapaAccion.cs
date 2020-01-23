using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using RetroElite.Otros.Game.Entidades.Manejadores.Movimientos;
using RetroElite.Otros.Mapas;
using RetroElite.Utilidades.Criptografia;

namespace RetroElite.Otros.Scripts.Acciones
{
	// Token: 0x02000024 RID: 36
	public class CambiarMapaAccion : AccionesScript
	{
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600015C RID: 348 RVA: 0x0000697B File Offset: 0x00004D7B
		// (set) Token: 0x0600015D RID: 349 RVA: 0x00006983 File Offset: 0x00004D83
		public MapaTeleportCeldas direccion { get; private set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600015E RID: 350 RVA: 0x0000698C File Offset: 0x00004D8C
		// (set) Token: 0x0600015F RID: 351 RVA: 0x00006994 File Offset: 0x00004D94
		public short celda_id { get; private set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000160 RID: 352 RVA: 0x0000699D File Offset: 0x00004D9D
		public bool celda_especifica
		{
			get
			{
				return this.direccion == MapaTeleportCeldas.AUCUN && this.celda_id != -1;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000161 RID: 353 RVA: 0x000069B6 File Offset: 0x00004DB6
		public bool direccion_especifica
		{
			get
			{
				return this.direccion != MapaTeleportCeldas.AUCUN && this.celda_id == -1;
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000069CC File Offset: 0x00004DCC
		public CambiarMapaAccion(MapaTeleportCeldas _direccion, short _celda_id)
		{
			this.direccion = _direccion;
			this.celda_id = _celda_id;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x000069E8 File Offset: 0x00004DE8
		internal override Task<ResultadosAcciones> proceso(Cuenta cuenta)
		{
			bool celda_especifica = this.celda_especifica;
			if (celda_especifica)
			{
				Celda celda = cuenta.juego.mapa.get_Celda_Id(this.celda_id);
				bool flag = !cuenta.juego.manejador.movimientos.get_Cambiar_Mapa(this.direccion, celda);
				if (flag)
				{
					return AccionesScript.resultado_fallado;
				}
			}
			else
			{
				bool direccion_especifica = this.direccion_especifica;
				if (direccion_especifica)
				{
					bool flag2 = !cuenta.juego.manejador.movimientos.get_Cambiar_Mapa(this.direccion);
					if (flag2)
					{
						return AccionesScript.resultado_fallado;
					}
				}
			}
			return AccionesScript.resultado_procesado;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00006A8C File Offset: 0x00004E8C
		public static bool TryParse(string texto, out CambiarMapaAccion accion)
		{
			string[] array = texto.Split(new char[]
			{
				'|'
			});
			string input = array[Randomize.get_Random(0, array.Length)];
			Match match = Regex.Match(input, "(?<direction>haut|droite|bas|gauche)\\((?<cell>\\d{1,3})\\)");
			bool success = match.Success;
			bool result;
			if (success)
			{
				accion = new CambiarMapaAccion((MapaTeleportCeldas)Enum.Parse(typeof(MapaTeleportCeldas), match.Groups["direction"].Value, true), short.Parse(match.Groups["celda"].Value));
				result = true;
			}
			else
			{
				match = Regex.Match(input, "(?<direction>haut|droite|bas|gauche)");
				bool success2 = match.Success;
				if (success2)
				{
					accion = new CambiarMapaAccion((MapaTeleportCeldas)Enum.Parse(typeof(MapaTeleportCeldas), match.Groups["direction"].Value, true), -1);
					result = true;
				}
				else
				{
					match = Regex.Match(input, "(?<cell>\\d{1,3})");
					bool success3 = match.Success;
					if (success3)
					{
						accion = new CambiarMapaAccion(MapaTeleportCeldas.AUCUN, short.Parse(match.Groups["cell"].Value));
						result = true;
					}
					else
					{
						accion = null;
						result = false;
					}
				}
			}
			return result;
		}
	}
}
