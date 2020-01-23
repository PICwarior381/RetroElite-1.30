using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RetroElite.Otros.Enums;
using RetroElite.Otros.Game.Personaje.Hechizos;
using RetroElite.Otros.Game.Personaje.Inventario;
using RetroElite.Otros.Game.Personaje.Oficios;
using RetroElite.Otros.Mapas;
using RetroElite.Otros.Mapas.Entidades;

namespace RetroElite.Otros.Game.Personaje
{
	// Token: 0x02000053 RID: 83
	public class PersonajeJuego : Entidad, IDisposable, IEliminable
	{
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000315 RID: 789 RVA: 0x0000C3FF File Offset: 0x0000A7FF
		// (set) Token: 0x06000316 RID: 790 RVA: 0x0000C407 File Offset: 0x0000A807
		public int id { get; set; } = 0;

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000317 RID: 791 RVA: 0x0000C410 File Offset: 0x0000A810
		// (set) Token: 0x06000318 RID: 792 RVA: 0x0000C418 File Offset: 0x0000A818
		public string nombre { get; set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000319 RID: 793 RVA: 0x0000C421 File Offset: 0x0000A821
		// (set) Token: 0x0600031A RID: 794 RVA: 0x0000C429 File Offset: 0x0000A829
		public byte nivel { get; set; } = 0;

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600031B RID: 795 RVA: 0x0000C432 File Offset: 0x0000A832
		// (set) Token: 0x0600031C RID: 796 RVA: 0x0000C43A File Offset: 0x0000A83A
		public byte sexo { get; set; } = 0;

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600031D RID: 797 RVA: 0x0000C443 File Offset: 0x0000A843
		// (set) Token: 0x0600031E RID: 798 RVA: 0x0000C44B File Offset: 0x0000A84B
		public byte raza_id { get; set; } = 0;

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600031F RID: 799 RVA: 0x0000C454 File Offset: 0x0000A854
		// (set) Token: 0x06000320 RID: 800 RVA: 0x0000C45C File Offset: 0x0000A85C
		public InventarioGeneral inventario { get; private set; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000321 RID: 801 RVA: 0x0000C465 File Offset: 0x0000A865
		// (set) Token: 0x06000322 RID: 802 RVA: 0x0000C46D File Offset: 0x0000A86D
		public int puntos_caracteristicas { get; set; } = 0;

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000323 RID: 803 RVA: 0x0000C476 File Offset: 0x0000A876
		// (set) Token: 0x06000324 RID: 804 RVA: 0x0000C47E File Offset: 0x0000A87E
		public int points_sorts { get; set; } = 0;

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000325 RID: 805 RVA: 0x0000C487 File Offset: 0x0000A887
		// (set) Token: 0x06000326 RID: 806 RVA: 0x0000C48F File Offset: 0x0000A88F
		public int kamas { get; private set; } = 0;

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000327 RID: 807 RVA: 0x0000C498 File Offset: 0x0000A898
		// (set) Token: 0x06000328 RID: 808 RVA: 0x0000C4A0 File Offset: 0x0000A8A0
		public Caracteristicas caracteristicas { get; set; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000329 RID: 809 RVA: 0x0000C4A9 File Offset: 0x0000A8A9
		// (set) Token: 0x0600032A RID: 810 RVA: 0x0000C4B1 File Offset: 0x0000A8B1
		public Dictionary<short, Hechizo> hechizos { get; set; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600032B RID: 811 RVA: 0x0000C4BA File Offset: 0x0000A8BA
		// (set) Token: 0x0600032C RID: 812 RVA: 0x0000C4C2 File Offset: 0x0000A8C2
		public List<Oficio> oficios { get; private set; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600032D RID: 813 RVA: 0x0000C4CB File Offset: 0x0000A8CB
		// (set) Token: 0x0600032E RID: 814 RVA: 0x0000C4D3 File Offset: 0x0000A8D3
		public Timer timer_regeneracion { get; private set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600032F RID: 815 RVA: 0x0000C4DC File Offset: 0x0000A8DC
		// (set) Token: 0x06000330 RID: 816 RVA: 0x0000C4E4 File Offset: 0x0000A8E4
		public Timer timer_afk { get; private set; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000331 RID: 817 RVA: 0x0000C4ED File Offset: 0x0000A8ED
		// (set) Token: 0x06000332 RID: 818 RVA: 0x0000C4F5 File Offset: 0x0000A8F5
		public string canales { get; set; } = string.Empty;

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000333 RID: 819 RVA: 0x0000C4FE File Offset: 0x0000A8FE
		// (set) Token: 0x06000334 RID: 820 RVA: 0x0000C506 File Offset: 0x0000A906
		public Celda celda { get; set; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000335 RID: 821 RVA: 0x0000C50F File Offset: 0x0000A90F
		// (set) Token: 0x06000336 RID: 822 RVA: 0x0000C517 File Offset: 0x0000A917
		public bool en_grupo { get; set; } = false;

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000337 RID: 823 RVA: 0x0000C520 File Offset: 0x0000A920
		// (set) Token: 0x06000338 RID: 824 RVA: 0x0000C528 File Offset: 0x0000A928
		public bool esta_utilizando_dragopavo { get; set; } = false;

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000339 RID: 825 RVA: 0x0000C531 File Offset: 0x0000A931
		// (set) Token: 0x0600033A RID: 826 RVA: 0x0000C539 File Offset: 0x0000A939
		public sbyte hablando_npc_id { get; set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600033B RID: 827 RVA: 0x0000C542 File Offset: 0x0000A942
		public int porcentaje_experiencia
		{
			get
			{
				return (int)((this.caracteristicas.experiencia_actual - this.caracteristicas.experiencia_minima_nivel) / (this.caracteristicas.experiencia_siguiente_nivel - this.caracteristicas.experiencia_minima_nivel) * 100.0);
			}
		}

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x0600033C RID: 828 RVA: 0x0000C580 File Offset: 0x0000A980
		// (remove) Token: 0x0600033D RID: 829 RVA: 0x0000C5B8 File Offset: 0x0000A9B8
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action servidor_seleccionado;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x0600033E RID: 830 RVA: 0x0000C5F0 File Offset: 0x0000A9F0
		// (remove) Token: 0x0600033F RID: 831 RVA: 0x0000C628 File Offset: 0x0000AA28
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action personaje_seleccionado;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06000340 RID: 832 RVA: 0x0000C660 File Offset: 0x0000AA60
		// (remove) Token: 0x06000341 RID: 833 RVA: 0x0000C698 File Offset: 0x0000AA98
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action caracteristicas_actualizadas;

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06000342 RID: 834 RVA: 0x0000C6D0 File Offset: 0x0000AAD0
		// (remove) Token: 0x06000343 RID: 835 RVA: 0x0000C708 File Offset: 0x0000AB08
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action pods_actualizados;

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000344 RID: 836 RVA: 0x0000C740 File Offset: 0x0000AB40
		// (remove) Token: 0x06000345 RID: 837 RVA: 0x0000C778 File Offset: 0x0000AB78
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action hechizos_actualizados;

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06000346 RID: 838 RVA: 0x0000C7B0 File Offset: 0x0000ABB0
		// (remove) Token: 0x06000347 RID: 839 RVA: 0x0000C7E8 File Offset: 0x0000ABE8
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action oficios_actualizados;

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06000348 RID: 840 RVA: 0x0000C820 File Offset: 0x0000AC20
		// (remove) Token: 0x06000349 RID: 841 RVA: 0x0000C858 File Offset: 0x0000AC58
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action dialogo_npc_recibido;

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x0600034A RID: 842 RVA: 0x0000C890 File Offset: 0x0000AC90
		// (remove) Token: 0x0600034B RID: 843 RVA: 0x0000C8C8 File Offset: 0x0000ACC8
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action dialogo_npc_acabado;

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x0600034C RID: 844 RVA: 0x0000C900 File Offset: 0x0000AD00
		// (remove) Token: 0x0600034D RID: 845 RVA: 0x0000C938 File Offset: 0x0000AD38
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<List<Celda>> movimiento_pathfinding_minimapa;

		// Token: 0x0600034E RID: 846 RVA: 0x0000C970 File Offset: 0x0000AD70
		public PersonajeJuego(Cuenta _cuenta)
		{
			this.cuenta = _cuenta;
			this.timer_regeneracion = new Timer(new TimerCallback(this.regeneracion_TimerCallback), null, -1, -1);
			this.timer_afk = new Timer(new TimerCallback(this.anti_Afk), null, -1, -1);
			this.inventario = new InventarioGeneral(this.cuenta);
			this.caracteristicas = new Caracteristicas();
			this.hechizos = new Dictionary<short, Hechizo>();
			this.oficios = new List<Oficio>();
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000CA42 File Offset: 0x0000AE42
		public void set_Datos_Personaje(int _id, string _nombre_personaje, byte _nivel, byte _sexo, byte _raza_id)
		{
			this.id = _id;
			this.nombre = _nombre_personaje;
			this.nivel = _nivel;
			this.sexo = _sexo;
			this.raza_id = _raza_id;
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000CA70 File Offset: 0x0000AE70
		public void agregar_Canal_Personaje(string cadena_canales)
		{
			bool flag = cadena_canales.Length <= 1;
			if (flag)
			{
				this.canales += cadena_canales;
			}
			else
			{
				this.canales = cadena_canales;
			}
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000CAAB File Offset: 0x0000AEAB
		public void eliminar_Canal_Personaje(string simbolo_canal)
		{
			this.canales = this.canales.Replace(simbolo_canal, string.Empty);
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000CAC5 File Offset: 0x0000AEC5
		public void evento_Pods_Actualizados()
		{
			Action action = this.pods_actualizados;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000CAD9 File Offset: 0x0000AED9
		public void evento_Servidor_Seleccionado()
		{
			Action action = this.servidor_seleccionado;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000CAED File Offset: 0x0000AEED
		public void evento_Personaje_Seleccionado()
		{
			Action action = this.personaje_seleccionado;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000CB01 File Offset: 0x0000AF01
		public void evento_Personaje_Pathfinding_Minimapa(List<Celda> lista)
		{
			Action<List<Celda>> action = this.movimiento_pathfinding_minimapa;
			if (action != null)
			{
				action(lista);
			}
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000CB16 File Offset: 0x0000AF16
		public void evento_Oficios_Actualizados()
		{
			Action action = this.oficios_actualizados;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000CB2A File Offset: 0x0000AF2A
		public void evento_Dialogo_Recibido()
		{
			Action action = this.dialogo_npc_recibido;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000CB3E File Offset: 0x0000AF3E
		public void evento_Dialogo_Acabado()
		{
			Action action = this.dialogo_npc_acabado;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000CB54 File Offset: 0x0000AF54
		public async void up_StatsAsync(string field, int count)
		{
			bool flag = !this.cuenta.esta_luchando();
			if (flag)
			{
				bool flag2 = field.Equals("force");
				if (flag2)
				{
					this.cuenta.conexion.enviar_Paquete("AB10", false);
					this.cuenta.conexion.enviar_Paquete("BD", false);
				}
				else
				{
					bool flag3 = field.Equals("vita");
					if (flag3)
					{
						this.cuenta.conexion.enviar_Paquete("AB11", false);
						this.cuenta.conexion.enviar_Paquete("BD", false);
					}
					else
					{
						bool flag4 = field.Equals("sagesse");
						if (flag4)
						{
							this.cuenta.conexion.enviar_Paquete("AB12", false);
							this.cuenta.conexion.enviar_Paquete("BD", false);
						}
						else
						{
							bool flag5 = field.Equals("chance");
							if (flag5)
							{
								this.cuenta.conexion.enviar_Paquete("AB13", false);
								this.cuenta.conexion.enviar_Paquete("BD", false);
							}
							else
							{
								bool flag6 = field.Equals("agi");
								if (flag6)
								{
									this.cuenta.conexion.enviar_Paquete("AB14", false);
									this.cuenta.conexion.enviar_Paquete("BD", false);
								}
								else
								{
									bool flag7 = field.Equals("intel");
									if (flag7)
									{
										this.cuenta.conexion.enviar_Paquete("AB15", false);
										this.cuenta.conexion.enviar_Paquete("BD", false);
									}
								}
							}
						}
					}
				}
				await Task.Delay(100);
				Action action = this.caracteristicas_actualizadas;
				if (action != null)
				{
					action();
				}
				await Task.Delay(100);
			}
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000CBA0 File Offset: 0x0000AFA0
		public void actualizar_Caracteristicas(string paquete)
		{
			string[] array = paquete.Substring(2).Split(new char[]
			{
				'|'
			});
			string[] array2 = array[0].Split(new char[]
			{
				','
			});
			this.caracteristicas.experiencia_actual = double.Parse(array2[0]);
			this.caracteristicas.experiencia_minima_nivel = double.Parse(array2[1]);
			this.caracteristicas.experiencia_siguiente_nivel = double.Parse(array2[2]);
			this.kamas = int.Parse(array[1]);
			this.puntos_caracteristicas = int.Parse(array[2]);
			this.points_sorts = int.Parse(array[3]);
			array2 = array[5].Split(new char[]
			{
				','
			});
			this.caracteristicas.vitalidad_actual = int.Parse(array2[0]);
			this.caracteristicas.vitalidad_maxima = int.Parse(array2[1]);
			array2 = array[6].Split(new char[]
			{
				','
			});
			this.caracteristicas.energia_actual = int.Parse(array2[0]);
			this.caracteristicas.maxima_energia = int.Parse(array2[1]);
			bool flag = this.caracteristicas.iniciativa != null;
			if (flag)
			{
				this.caracteristicas.iniciativa.base_personaje = int.Parse(array[7]);
			}
			else
			{
				this.caracteristicas.iniciativa = new PersonajeStats(int.Parse(array[7]));
			}
			bool flag2 = this.caracteristicas.prospeccion != null;
			if (flag2)
			{
				this.caracteristicas.prospeccion.base_personaje = int.Parse(array[8]);
			}
			else
			{
				this.caracteristicas.prospeccion = new PersonajeStats(int.Parse(array[8]));
			}
			for (int i = 9; i <= 18; i++)
			{
				array2 = array[i].Split(new char[]
				{
					','
				});
				int base_personaje = int.Parse(array2[0]);
				int equipamiento = int.Parse(array2[1]);
				int dones = int.Parse(array2[2]);
				int boost = int.Parse(array2[3]);
				switch (i)
				{
				case 9:
					this.caracteristicas.puntos_accion.actualizar_Stats(base_personaje, equipamiento, dones, boost);
					break;
				case 10:
					this.caracteristicas.puntos_movimiento.actualizar_Stats(base_personaje, equipamiento, dones, boost);
					break;
				case 11:
					this.caracteristicas.fuerza.actualizar_Stats(base_personaje, equipamiento, dones, boost);
					break;
				case 12:
					this.caracteristicas.vitalidad.actualizar_Stats(base_personaje, equipamiento, dones, boost);
					break;
				case 13:
					this.caracteristicas.sabiduria.actualizar_Stats(base_personaje, equipamiento, dones, boost);
					break;
				case 14:
					this.caracteristicas.suerte.actualizar_Stats(base_personaje, equipamiento, dones, boost);
					break;
				case 15:
					this.caracteristicas.agilidad.actualizar_Stats(base_personaje, equipamiento, dones, boost);
					break;
				case 16:
					this.caracteristicas.inteligencia.actualizar_Stats(base_personaje, equipamiento, dones, boost);
					break;
				case 17:
					this.caracteristicas.alcanze.actualizar_Stats(base_personaje, equipamiento, dones, boost);
					break;
				case 18:
					this.caracteristicas.criaturas_invocables.actualizar_Stats(base_personaje, equipamiento, dones, boost);
					break;
				}
			}
			Action action = this.caracteristicas_actualizadas;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000CF18 File Offset: 0x0000B318
		public void actualizar_Hechizos(string paquete)
		{
			this.hechizos.Clear();
			string[] array = paquete.Split(new char[]
			{
				';'
			});
			for (int i = 0; i < array.Length - 1; i++)
			{
				string[] array2 = array[i].Split(new char[]
				{
					'~'
				});
				short num = short.Parse(array2[0]);
				Hechizo hechizo = Hechizo.get_Hechizo(num);
				bool flag = hechizo != null;
				if (flag)
				{
					hechizo.nivel = byte.Parse((array2[1] == null) ? "0" : array2[1]);
					this.hechizos.Add(num, hechizo);
				}
			}
			this.hechizos_actualizados();
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000CFC8 File Offset: 0x0000B3C8
		private void regeneracion_TimerCallback(object state)
		{
			try
			{
				Caracteristicas caracteristicas = this.caracteristicas;
				int? num = (caracteristicas != null) ? new int?(caracteristicas.vitalidad_actual) : null;
				Caracteristicas caracteristicas2 = this.caracteristicas;
				int? num2 = (caracteristicas2 != null) ? new int?(caracteristicas2.vitalidad_maxima) : null;
				bool flag = num.GetValueOrDefault() >= num2.GetValueOrDefault() & (num != null & num2 != null);
				if (flag)
				{
					this.timer_regeneracion.Change(-1, -1);
				}
				else
				{
					Caracteristicas caracteristicas3 = this.caracteristicas;
					int vitalidad_actual = caracteristicas3.vitalidad_actual;
					caracteristicas3.vitalidad_actual = vitalidad_actual + 1;
					Action action = this.caracteristicas_actualizadas;
					if (action != null)
					{
						action();
					}
				}
			}
			catch (Exception arg)
			{
				this.cuenta.logger.log_Error("TIMER-REGENERANDO", string.Format("ERROR: {0}", arg));
			}
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000D0B4 File Offset: 0x0000B4B4
		private void anti_Afk(object state)
		{
			try
			{
				bool flag = this.cuenta.Estado_Cuenta != EstadoCuenta.DESCONECTADO;
				if (flag)
				{
					this.cuenta.conexion.enviar_Paquete("ping", false);
				}
			}
			catch (Exception arg)
			{
				this.cuenta.logger.log_Error("TIMER-ANTIAFK", string.Format("ERROR: {0}", arg));
			}
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000D128 File Offset: 0x0000B528
		public Hechizo get_Hechizo(short id)
		{
			return this.hechizos[id];
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000D138 File Offset: 0x0000B538
		public bool get_Tiene_Skill_Id(int id)
		{
			Func<SkillsOficio, bool> <>9__1;
			return this.oficios.FirstOrDefault(delegate(Oficio j)
			{
				IEnumerable<SkillsOficio> skills = j.skills;
				Func<SkillsOficio, bool> predicate;
				if ((predicate = <>9__1) == null)
				{
					predicate = (<>9__1 = ((SkillsOficio s) => (int)s.id == id));
				}
				return skills.FirstOrDefault(predicate) != null;
			}) != null;
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000D16C File Offset: 0x0000B56C
		public IEnumerable<SkillsOficio> get_Skills_Disponibles()
		{
			return this.oficios.SelectMany((Oficio oficio) => from skill in oficio.skills
			select skill);
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000D198 File Offset: 0x0000B598
		public IEnumerable<short> get_Skills_Recoleccion_Disponibles()
		{
			return this.oficios.SelectMany((Oficio oficio) => from skill in oficio.skills
			where !skill.es_craft
			select skill.id);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000D1C4 File Offset: 0x0000B5C4
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000D1D0 File Offset: 0x0000B5D0
		~PersonajeJuego()
		{
			this.Dispose(false);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000D200 File Offset: 0x0000B600
		public void limpiar()
		{
			this.id = 0;
			this.hechizos.Clear();
			this.oficios.Clear();
			this.inventario.limpiar();
			this.caracteristicas.limpiar();
			this.timer_regeneracion.Change(-1, -1);
			this.timer_afk.Change(-1, -1);
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000D264 File Offset: 0x0000B664
		public virtual void Dispose(bool disposing)
		{
			bool flag = !this.disposed;
			if (flag)
			{
				if (disposing)
				{
					this.inventario.Dispose();
					this.timer_regeneracion.Dispose();
					this.timer_afk.Dispose();
				}
				this.hechizos = null;
				this.caracteristicas = null;
				this.nombre = null;
				this.inventario = null;
				this.timer_regeneracion = null;
				this.timer_afk = null;
				this.disposed = true;
			}
		}

		// Token: 0x0400014D RID: 333
		private Cuenta cuenta;

		// Token: 0x0400015A RID: 346
		private bool disposed;
	}
}
