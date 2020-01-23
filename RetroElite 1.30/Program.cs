using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using RetroElite.Comun.Frames.Transporte;
using RetroElite.Forms;
using RetroElite.Otros.Game.Personaje.Hechizos;
using RetroElite.Otros.Mapas.Interactivo;
using RetroElite.Otros.Scripts.Manejadores;
using RetroElite.Properties;
using RetroElite.Utilidades.Configuracion;

namespace RetroElite
{
	// Token: 0x02000002 RID: 2
	internal static class Program
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000450
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Task.Run(delegate()
			{
				GlobalConf.cargar_Todas_Cuentas();
				LuaManejadorScript.inicializar_Funciones();
				XElement.Parse(Resources.interactivos).Descendants("SKILL").ToList<XElement>().ForEach(delegate(XElement i)
				{
					new ObjetoInteractivoModelo(i.Element("nombre").Value, i.Element("gfx").Value, bool.Parse(i.Element("caminable").Value), i.Element("habilidades").Value, bool.Parse(i.Element("recolectable").Value));
				});
				PaqueteRecibido.Inicializar();
			}).ContinueWith(delegate(Task t)
			{
				XElement.Parse(Resources.hechizos).Descendants("HECHIZO").ToList<XElement>().ForEach(delegate(XElement mapa)
				{
					Hechizo hechizo = new Hechizo(short.Parse(mapa.Attribute("ID").Value), mapa.Element("NOMBRE").Value);
					mapa.Descendants("NIVEL").ToList<XElement>().ForEach(delegate(XElement stats)
					{
						HechizoStats hechizo_stats = new HechizoStats();
						hechizo_stats.coste_pa = byte.Parse(stats.Attribute("COSTE_PA").Value);
						hechizo_stats.alcanze_minimo = byte.Parse(stats.Attribute("RANGO_MINIMO").Value);
						hechizo_stats.alcanze_maximo = byte.Parse(stats.Attribute("RANGO_MAXIMO").Value);
						hechizo_stats.es_lanzado_linea = bool.Parse(stats.Attribute("LANZ_EN_LINEA").Value);
						hechizo_stats.es_lanzado_con_vision = bool.Parse(stats.Attribute("NECESITA_VISION").Value);
						hechizo_stats.es_celda_vacia = bool.Parse(stats.Attribute("NECESITA_CELDA_LIBRE").Value);
						hechizo_stats.es_alcanze_modificable = bool.Parse(stats.Attribute("RANGO_MODIFICABLE").Value);
						hechizo_stats.lanzamientos_por_turno = byte.Parse(stats.Attribute("MAX_LANZ_POR_TURNO").Value);
						hechizo_stats.lanzamientos_por_objetivo = byte.Parse(stats.Attribute("MAX_LANZ_POR_OBJETIVO").Value);
						hechizo_stats.intervalo = byte.Parse(stats.Attribute("COOLDOWN").Value);
						stats.Descendants("EFECTO").ToList<XElement>().ForEach(delegate(XElement efecto)
						{
							hechizo_stats.agregar_efecto(new HechizoEfecto(int.Parse(efecto.Attribute("TIPO").Value), Zonas.Parse(efecto.Attribute("ZONA").Value)), bool.Parse(efecto.Attribute("ES_CRITICO").Value));
						});
						hechizo.get_Agregar_Hechizo_Stats(byte.Parse(stats.Attribute("NIVEL").Value), hechizo_stats);
					});
				});
			}).Wait();
			Principal principal = new Principal();
			principal.ShowDialog();
		}
	}
}
