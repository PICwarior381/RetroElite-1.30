using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using RetroElite.Otros.Game.Personaje.Inventario.Enums;

namespace RetroElite.Otros.Game.Personaje.Inventario
{
	// Token: 0x02000058 RID: 88
	public class InventarioGeneral : IDisposable, IEliminable
	{
		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060003BE RID: 958 RVA: 0x0000D9CA File Offset: 0x0000BDCA
		// (set) Token: 0x060003BF RID: 959 RVA: 0x0000D9D2 File Offset: 0x0000BDD2
		public int kamas { get; private set; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x0000D9DB File Offset: 0x0000BDDB
		// (set) Token: 0x060003C1 RID: 961 RVA: 0x0000D9E3 File Offset: 0x0000BDE3
		public short pods_actuales { get; set; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x0000D9EC File Offset: 0x0000BDEC
		// (set) Token: 0x060003C3 RID: 963 RVA: 0x0000D9F4 File Offset: 0x0000BDF4
		public short pods_maximos { get; set; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x0000D9FD File Offset: 0x0000BDFD
		public IEnumerable<ObjetosInventario> objetos
		{
			get
			{
				return this._objetos.Values;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x0000DA0A File Offset: 0x0000BE0A
		public IEnumerable<ObjetosInventario> equipamiento
		{
			get
			{
				return from o in this.objetos
				where o.tipo_inventario == TipoObjetosInventario.EQUIPEMENTS
				select o;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x0000DA36 File Offset: 0x0000BE36
		public IEnumerable<ObjetosInventario> varios
		{
			get
			{
				return from o in this.objetos
				where o.tipo_inventario == TipoObjetosInventario.CONSOMMABLES
				select o;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x0000DA62 File Offset: 0x0000BE62
		public IEnumerable<ObjetosInventario> recursos
		{
			get
			{
				return from o in this.objetos
				where o.tipo_inventario == TipoObjetosInventario.RESSOURCES
				select o;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x0000DA8E File Offset: 0x0000BE8E
		public IEnumerable<ObjetosInventario> mision
		{
			get
			{
				return from o in this.objetos
				where o.tipo_inventario == TipoObjetosInventario.OBJETS_QUETE
				select o;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x0000DABA File Offset: 0x0000BEBA
		public int porcentaje_pods
		{
			get
			{
				return (int)((double)this.pods_actuales / (double)this.pods_maximos * 100.0);
			}
		}

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x060003CA RID: 970 RVA: 0x0000DAD8 File Offset: 0x0000BED8
		// (remove) Token: 0x060003CB RID: 971 RVA: 0x0000DB10 File Offset: 0x0000BF10
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<bool> inventario_actualizado;

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x060003CC RID: 972 RVA: 0x0000DB48 File Offset: 0x0000BF48
		// (remove) Token: 0x060003CD RID: 973 RVA: 0x0000DB80 File Offset: 0x0000BF80
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action almacenamiento_abierto;

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x060003CE RID: 974 RVA: 0x0000DBB8 File Offset: 0x0000BFB8
		// (remove) Token: 0x060003CF RID: 975 RVA: 0x0000DBF0 File Offset: 0x0000BFF0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action almacenamiento_cerrado;

		// Token: 0x060003D0 RID: 976 RVA: 0x0000DC25 File Offset: 0x0000C025
		internal InventarioGeneral(Cuenta _cuenta)
		{
			this.cuenta = _cuenta;
			this._objetos = new ConcurrentDictionary<uint, ObjetosInventario>();
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000DC44 File Offset: 0x0000C044
		public ObjetosInventario get_Objeto_Modelo_Id(int gid)
		{
			return this.objetos.FirstOrDefault((ObjetosInventario f) => f.id_modelo == gid);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000DC78 File Offset: 0x0000C078
		public ObjetosInventario get_Objeto_en_Posicion(InventarioPosiciones posicion)
		{
			return this.objetos.FirstOrDefault((ObjetosInventario o) => o.posicion == posicion);
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000DCAC File Offset: 0x0000C0AC
		public void agregar_Objetos(string paquete)
		{
			Task.Run(delegate()
			{
				foreach (string text in paquete.Split(new char[]
				{
					';'
				}))
				{
					bool flag = !string.IsNullOrEmpty(text);
					if (flag)
					{
						string[] array2 = text.Split(new char[]
						{
							'~'
						});
						uint key = Convert.ToUInt32(array2[0], 16);
						ObjetosInventario value = new ObjetosInventario(text);
						this._objetos.TryAdd(key, value);
					}
				}
			}).Wait();
			Action<bool> action = this.inventario_actualizado;
			if (action != null)
			{
				action(true);
			}
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000DCF8 File Offset: 0x0000C0F8
		public void modificar_Objetos(string paquete)
		{
			bool flag = !string.IsNullOrEmpty(paquete);
			if (flag)
			{
				string[] separador = paquete.Split(new char[]
				{
					'|'
				});
				ObjetosInventario objetosInventario = this.objetos.FirstOrDefault((ObjetosInventario f) => f.id_inventario == uint.Parse(separador[0]));
				bool flag2 = objetosInventario != null;
				if (flag2)
				{
					int cantidad = int.Parse(separador[1]);
					ObjetosInventario objetosInventario2 = objetosInventario;
					objetosInventario2.cantidad = cantidad;
					bool flag3 = this._objetos.TryUpdate(objetosInventario.id_inventario, objetosInventario2, objetosInventario);
					if (flag3)
					{
						Action<bool> action = this.inventario_actualizado;
						if (action != null)
						{
							action(true);
						}
					}
				}
			}
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000DDA0 File Offset: 0x0000C1A0
		public void eliminar_Objeto(ObjetosInventario obj, int cantidad, bool paquete_eliminar)
		{
			bool flag = obj == null;
			if (!flag)
			{
				cantidad = ((cantidad == 0) ? obj.cantidad : ((cantidad > obj.cantidad) ? obj.cantidad : cantidad));
				bool flag2 = obj.cantidad > cantidad;
				if (flag2)
				{
					obj.cantidad -= cantidad;
					this._objetos.TryUpdate(obj.id_inventario, obj, obj);
				}
				else
				{
					ObjetosInventario objetosInventario;
					this._objetos.TryRemove(obj.id_inventario, out objetosInventario);
				}
				if (paquete_eliminar)
				{
					this.cuenta.conexion.enviar_Paquete(string.Format("Od{0}|{1}", obj.id_inventario, cantidad), false);
					this.cuenta.logger.log_informacion("Inventaire", string.Format("{0} {1} enlevé(s).", cantidad, obj.nombre));
				}
				Action<bool> action = this.inventario_actualizado;
				if (action != null)
				{
					action(true);
				}
			}
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000DE9C File Offset: 0x0000C29C
		public void eliminar_Objeto(uint id_inventario, int cantidad, bool paquete_eliminar)
		{
			ObjetosInventario obj;
			bool flag = !this._objetos.TryGetValue(id_inventario, out obj);
			if (!flag)
			{
				this.eliminar_Objeto(obj, cantidad, paquete_eliminar);
			}
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000DECC File Offset: 0x0000C2CC
		public bool equipar_Objeto(ObjetosInventario objeto)
		{
			bool flag = objeto == null || objeto.cantidad == 0 || this.cuenta.esta_ocupado();
			bool result;
			if (flag)
			{
				this.cuenta.logger.log_Error("INVENTAIRE", "l'objet " + objeto.nombre + " ne peut pas être équipé");
				result = false;
			}
			else
			{
				bool flag2 = objeto.nivel > (short)this.cuenta.juego.personaje.nivel;
				if (flag2)
				{
					this.cuenta.logger.log_Error("INVENTAIRE", "Le niveau de l'objet " + objeto.nombre + " est superieur au niveau actuel.");
					result = false;
				}
				else
				{
					bool flag3 = objeto.posicion != InventarioPosiciones.PAS_EQUIPE;
					if (flag3)
					{
						this.cuenta.logger.log_Error("INVENTAIRE", "L'objet " + objeto.nombre + " est déjà équipé");
						result = false;
					}
					else
					{
						List<InventarioPosiciones> list = InventarioUtiles.get_Posibles_Posiciones((int)objeto.tipo);
						bool flag4 = list == null || list.Count == 0;
						if (flag4)
						{
							this.cuenta.logger.log_Error("INVENTAIRE", "L'objet " + objeto.nombre + " ne peut pas être équipé.");
							result = false;
						}
						else
						{
							foreach (InventarioPosiciones inventarioPosiciones in list)
							{
								bool flag5 = this.get_Objeto_en_Posicion(inventarioPosiciones) == null;
								if (flag5)
								{
									this.cuenta.conexion.enviar_Paquete("OM" + objeto.id_inventario.ToString() + "|" + ((sbyte)inventarioPosiciones).ToString(), true);
									this.cuenta.logger.log_informacion("INVENTAIRE", objeto.nombre + " équipé.");
									objeto.posicion = inventarioPosiciones;
									Action<bool> action = this.inventario_actualizado;
									if (action != null)
									{
										action(true);
									}
									return true;
								}
							}
							ObjetosInventario objetosInventario;
							bool flag6 = this._objetos.TryGetValue(this.get_Objeto_en_Posicion(list[0]).id_inventario, out objetosInventario);
							if (flag6)
							{
								objetosInventario.posicion = InventarioPosiciones.PAS_EQUIPE;
								this.cuenta.conexion.enviar_Paquete("OM" + objetosInventario.id_inventario.ToString() + "|" + -1.ToString(), false);
							}
							this.cuenta.conexion.enviar_Paquete("OM" + objeto.id_inventario.ToString() + "|" + ((sbyte)list[0]).ToString(), false);
							bool flag7 = objeto.cantidad == 1;
							if (flag7)
							{
								objeto.posicion = list[0];
							}
							this.cuenta.logger.log_informacion("INVENTAIRE", objeto.nombre + " équipé.");
							Action<bool> action2 = this.inventario_actualizado;
							if (action2 != null)
							{
								action2(true);
							}
							result = true;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000E200 File Offset: 0x0000C600
		public bool desequipar_Objeto(ObjetosInventario objeto)
		{
			bool flag = objeto == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = objeto.posicion == InventarioPosiciones.PAS_EQUIPE;
				if (flag2)
				{
					result = false;
				}
				else
				{
					this.cuenta.conexion.enviar_Paquete("OM" + objeto.id_inventario.ToString() + "|" + -1.ToString(), false);
					objeto.posicion = InventarioPosiciones.PAS_EQUIPE;
					this.cuenta.logger.log_informacion("INVENTAIRE", objeto.nombre + " déséquipé.");
					Action<bool> action = this.inventario_actualizado;
					if (action != null)
					{
						action(true);
					}
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000E2B0 File Offset: 0x0000C6B0
		public void utilizar_Objeto(ObjetosInventario objeto)
		{
			bool flag = objeto == null;
			if (!flag)
			{
				bool flag2 = objeto.cantidad == 0;
				if (flag2)
				{
					this.cuenta.logger.log_Error("INVENTAIRE", "L'objet " + objeto.nombre + " ne peut pas être utilisé, quantité insuffisante");
				}
				else
				{
					this.cuenta.conexion.enviar_Paquete("OU" + objeto.id_inventario.ToString() + "|", false);
					this.eliminar_Objeto(objeto, 1, false);
					this.cuenta.logger.log_informacion("INVENTAIRE", objeto.nombre + " utilisé.");
				}
			}
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000E365 File Offset: 0x0000C765
		public void evento_Almacenamiento_Abierto()
		{
			Action action = this.almacenamiento_abierto;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000E379 File Offset: 0x0000C779
		public void evento_Almacenamiento_Cerrado()
		{
			Action action = this.almacenamiento_cerrado;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000E38D File Offset: 0x0000C78D
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000E398 File Offset: 0x0000C798
		~InventarioGeneral()
		{
			this.Dispose(false);
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000E3C8 File Offset: 0x0000C7C8
		public void limpiar()
		{
			this.kamas = 0;
			this.pods_actuales = 0;
			this.pods_maximos = 0;
			this._objetos.Clear();
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000E3F0 File Offset: 0x0000C7F0
		public virtual void Dispose(bool disposing)
		{
			bool flag = !this.disposed;
			if (flag)
			{
				if (disposing)
				{
				}
				this._objetos.Clear();
				this._objetos = null;
				this.cuenta = null;
				this.disposed = true;
			}
		}

		// Token: 0x0400018B RID: 395
		private Cuenta cuenta;

		// Token: 0x0400018C RID: 396
		private ConcurrentDictionary<uint, ObjetosInventario> _objetos;

		// Token: 0x0400018D RID: 397
		private bool disposed;
	}
}
