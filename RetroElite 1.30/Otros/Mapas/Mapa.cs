using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using RetroElite.Otros.Mapas.Entidades;
using RetroElite.Otros.Mapas.Interactivo;
using RetroElite.Utilidades.Criptografia;

namespace RetroElite.Otros.Mapas
{
	// Token: 0x02000042 RID: 66
	public class Mapa : IEliminable, IDisposable
	{
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000266 RID: 614 RVA: 0x0000A43F File Offset: 0x0000883F
		// (set) Token: 0x06000267 RID: 615 RVA: 0x0000A447 File Offset: 0x00008847
		public int id { get; set; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000268 RID: 616 RVA: 0x0000A450 File Offset: 0x00008850
		// (set) Token: 0x06000269 RID: 617 RVA: 0x0000A458 File Offset: 0x00008858
		public byte anchura { get; set; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600026A RID: 618 RVA: 0x0000A461 File Offset: 0x00008861
		// (set) Token: 0x0600026B RID: 619 RVA: 0x0000A469 File Offset: 0x00008869
		public byte altura { get; set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0000A472 File Offset: 0x00008872
		// (set) Token: 0x0600026D RID: 621 RVA: 0x0000A47A File Offset: 0x0000887A
		public sbyte x { get; set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000A483 File Offset: 0x00008883
		// (set) Token: 0x0600026F RID: 623 RVA: 0x0000A48B File Offset: 0x0000888B
		public sbyte y { get; set; }

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06000270 RID: 624 RVA: 0x0000A494 File Offset: 0x00008894
		// (remove) Token: 0x06000271 RID: 625 RVA: 0x0000A4CC File Offset: 0x000088CC
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action mapa_actualizado;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000272 RID: 626 RVA: 0x0000A504 File Offset: 0x00008904
		// (remove) Token: 0x06000273 RID: 627 RVA: 0x0000A53C File Offset: 0x0000893C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action entidades_actualizadas;

		// Token: 0x06000274 RID: 628 RVA: 0x0000A571 File Offset: 0x00008971
		public Mapa()
		{
			this.entidades = new ConcurrentDictionary<int, Entidad>();
			this.interactivos = new ConcurrentDictionary<int, ObjetoInteractivo>();
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000A598 File Offset: 0x00008998
		public void get_Actualizar_Mapa(string paquete)
		{
			this.entidades.Clear();
			this.interactivos.Clear();
			string[] array = paquete.Split(new char[]
			{
				'|'
			});
			this.id = int.Parse(array[0]);
			Console.WriteLine(this.id.ToString());
			FileInfo fileInfo = new FileInfo("mapas/" + this.id.ToString() + ".xml");
			bool exists = fileInfo.Exists;
			if (exists)
			{
				Console.WriteLine("FileExist");
				XElement archivo_mapa = XElement.Load(fileInfo.FullName);
				this.anchura = byte.Parse(archivo_mapa.Element("ANCHURA").Value);
				this.altura = byte.Parse(archivo_mapa.Element("ALTURA").Value);
				this.x = sbyte.Parse(archivo_mapa.Element("X").Value);
				this.y = sbyte.Parse(archivo_mapa.Element("Y").Value);
				Task.Run(delegate()
				{
					this.descomprimir_mapa(archivo_mapa.Element("MAPA_DATA").Value);
				}).Wait();
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000276 RID: 630 RVA: 0x0000A705 File Offset: 0x00008B05
		public string coordenadas
		{
			get
			{
				return string.Format("[{0},{1}]", this.x, this.y);
			}
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000A727 File Offset: 0x00008B27
		public Celda get_Celda_Id(short celda_id)
		{
			return this.celdas[(int)celda_id];
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000A734 File Offset: 0x00008B34
		public bool esta_En_Mapa(string _coordenadas)
		{
			return _coordenadas == this.id.ToString() || _coordenadas == this.coordenadas;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000A768 File Offset: 0x00008B68
		public Celda get_Celda_Por_Coordenadas(int x, int y)
		{
			return this.celdas.FirstOrDefault((Celda celda) => celda.x == x && celda.y == y);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000A7A0 File Offset: 0x00008BA0
		public bool get_Puede_Luchar_Contra_Grupo_Monstruos(int monstruos_minimos, int monstruos_maximos, int nivel_minimo, int nivel_maximo, List<int> monstruos_prohibidos, List<int> monstruos_obligatorios)
		{
			return this.get_Grupo_Monstruos(monstruos_minimos, monstruos_maximos, nivel_minimo, nivel_maximo, monstruos_prohibidos, monstruos_obligatorios).Count > 0;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000A7B9 File Offset: 0x00008BB9
		public List<Celda> celdas_ocupadas()
		{
			return (from c in this.entidades.Values
			select c.celda).ToList<Celda>();
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000A7F0 File Offset: 0x00008BF0
		public List<Npcs> lista_npcs()
		{
			return (from n in this.entidades.Values
			where n is Npcs
			select n as Npcs).ToList<Npcs>();
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000A858 File Offset: 0x00008C58
		public List<Monstruos> lista_monstruos()
		{
			return (from n in this.entidades.Values
			where n is Monstruos
			select n as Monstruos).ToList<Monstruos>();
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000A8C0 File Offset: 0x00008CC0
		public List<Personajes> lista_personajes()
		{
			return (from n in this.entidades.Values
			where n is Personajes
			select n as Personajes).ToList<Personajes>();
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000A928 File Offset: 0x00008D28
		public List<Monstruos> get_Grupo_Monstruos(int monstruos_minimos, int monstruos_maximos, int nivel_minimo, int nivel_maximo, List<int> monstruos_prohibidos, List<int> monstruos_obligatorios)
		{
			List<Monstruos> list = new List<Monstruos>();
			foreach (Monstruos monstruos in this.lista_monstruos())
			{
				bool flag = monstruos.get_Total_Monstruos < monstruos_minimos || monstruos.get_Total_Monstruos > monstruos_maximos;
				if (!flag)
				{
					bool flag2 = monstruos.get_Total_Nivel_Grupo < nivel_minimo || monstruos.get_Total_Nivel_Grupo > nivel_maximo;
					if (!flag2)
					{
						bool flag3 = monstruos.celda.tipo == TipoCelda.CELDA_TELEPORT;
						if (!flag3)
						{
							bool flag4 = true;
							bool flag5 = monstruos_prohibidos != null;
							if (flag5)
							{
								for (int i = 0; i < monstruos_prohibidos.Count; i++)
								{
									bool flag6 = monstruos.get_Contiene_Monstruo(monstruos_prohibidos[i]);
									if (flag6)
									{
										flag4 = false;
										break;
									}
								}
							}
							bool flag7 = monstruos_obligatorios != null && flag4;
							if (flag7)
							{
								for (int j = 0; j < monstruos_obligatorios.Count; j++)
								{
									bool flag8 = !monstruos.get_Contiene_Monstruo(monstruos_obligatorios[j]);
									if (flag8)
									{
										flag4 = false;
										break;
									}
								}
							}
							bool flag9 = flag4;
							if (flag9)
							{
								list.Add(monstruos);
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000AA94 File Offset: 0x00008E94
		public void get_Evento_Mapa_Cambiado()
		{
			Action action = this.mapa_actualizado;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000AAA8 File Offset: 0x00008EA8
		public void evento_Entidad_Actualizada()
		{
			Action action = this.entidades_actualizadas;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000AABC File Offset: 0x00008EBC
		public void descomprimir_mapa(string mapa_data)
		{
			this.celdas = new Celda[mapa_data.Length / 10];
			for (int i = 0; i < mapa_data.Length; i += 10)
			{
				string celda_data = mapa_data.Substring(i, 10);
				this.celdas[i / 10] = this.descompimir_Celda(celda_data, Convert.ToInt16(i / 10));
			}
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000AB1C File Offset: 0x00008F1C
		public Celda descompimir_Celda(string celda_data, short id_celda)
		{
			byte[] array = new byte[celda_data.Length];
			for (int i = 0; i < celda_data.Length; i++)
			{
				array[i] = Convert.ToByte(Hash.get_Hash(celda_data[i]));
			}
			TipoCelda tipo = (TipoCelda)((array[2] & 56) >> 3);
			bool esta_activa = (array[0] & 32) >> 5 != 0;
			bool es_linea_vision = (array[0] & 1) != 1;
			bool flag = (array[7] & 2) >> 1 != 0;
			short num = Convert.ToInt16(((int)(array[0] & 2) << 12) + ((int)(array[7] & 1) << 12) + ((int)array[8] << 6) + (int)array[9]);
			short layer_object_1_num = Convert.ToInt16(((int)(array[0] & 4) << 11) + ((int)(array[4] & 1) << 12) + ((int)array[5] << 6) + (int)array[6]);
			byte nivel = Convert.ToByte((int)(array[1] & 15));
			byte slope = Convert.ToByte((array[4] & 60) >> 2);
			return new Celda(id_celda, esta_activa, tipo, es_linea_vision, nivel, slope, flag ? num : Convert.ToInt16(-1), layer_object_1_num, num, this);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000AC1B File Offset: 0x0000901B
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000AC28 File Offset: 0x00009028
		~Mapa()
		{
			this.Dispose(false);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000AC58 File Offset: 0x00009058
		public void limpiar()
		{
			this.id = 0;
			this.x = 0;
			this.y = 0;
			this.entidades.Clear();
			this.interactivos.Clear();
			this.celdas = null;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000AC94 File Offset: 0x00009094
		protected virtual void Dispose(bool disposing)
		{
			bool flag = this.disposed;
			if (!flag)
			{
				this.entidades.Clear();
				this.interactivos.Clear();
				this.celdas = null;
				this.entidades = null;
				this.disposed = true;
			}
		}

		// Token: 0x04000100 RID: 256
		public Celda[] celdas;

		// Token: 0x04000101 RID: 257
		public ConcurrentDictionary<int, Entidad> entidades;

		// Token: 0x04000102 RID: 258
		public ConcurrentDictionary<int, ObjetoInteractivo> interactivos;

		// Token: 0x04000105 RID: 261
		private bool disposed = false;
	}
}
