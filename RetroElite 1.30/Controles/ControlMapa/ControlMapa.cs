using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Timers;
using System.Windows.Forms;
using RetroElite.Controles.ControlMapa.Animaciones;
using RetroElite.Controles.ControlMapa.Celdas;
using RetroElite.Otros;
using RetroElite.Otros.Mapas;
using RetroElite.Otros.Mapas.Entidades;

namespace RetroElite.Controles.ControlMapa
{
	// Token: 0x02000080 RID: 128
	[Serializable]
	public class ControlMapa : UserControl
	{
		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x00023310 File Offset: 0x00021710
		// (set) Token: 0x06000535 RID: 1333 RVA: 0x00023318 File Offset: 0x00021718
		public byte mapa_altura { get; set; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x00023321 File Offset: 0x00021721
		// (set) Token: 0x06000537 RID: 1335 RVA: 0x00023329 File Offset: 0x00021729
		public byte mapa_anchura { get; set; }

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x06000538 RID: 1336 RVA: 0x00023334 File Offset: 0x00021734
		// (remove) Token: 0x06000539 RID: 1337 RVA: 0x0002336C File Offset: 0x0002176C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event ControlMapa.CellClickedHandler clic_celda;

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x0600053A RID: 1338 RVA: 0x000233A4 File Offset: 0x000217A4
		// (remove) Token: 0x0600053B RID: 1339 RVA: 0x000233DC File Offset: 0x000217DC
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<CeldaMapa, CeldaMapa> clic_celda_terminado;

		// Token: 0x0600053C RID: 1340 RVA: 0x00023414 File Offset: 0x00021814
		public ControlMapa()
		{
			this.DoubleBuffered = true;
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			this.tipo_calidad = CalidadMapa.MEDIA;
			this.mapa_altura = 17;
			this.mapa_anchura = 15;
			this.TraceOnOver = false;
			this.ColorCeldaInactiva = Color.DarkGray;
			this.ColorCeldaActiva = Color.Gray;
			this.mostrar_animaciones = true;
			this.animaciones = new ConcurrentDictionary<int, MovimientoAnimacion>();
			this.animaciones_timer = new System.Timers.Timer(80.0);
			this.animaciones_timer.Elapsed += this.animacion_Finalizada;
			this.set_Celda_Numero();
			this.dibujar_Cuadricula();
			this.InitializeComponent();
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x000234D1 File Offset: 0x000218D1
		protected void OnCellClicked(CeldaMapa cell, MouseButtons buttons, bool abajo)
		{
			ControlMapa.CellClickedHandler cellClickedHandler = this.clic_celda;
			if (cellClickedHandler != null)
			{
				cellClickedHandler(cell, buttons, abajo);
			}
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x000234E8 File Offset: 0x000218E8
		protected void OnCellOver(CeldaMapa cell, CeldaMapa last)
		{
			Action<CeldaMapa, CeldaMapa> action = this.clic_celda_terminado;
			if (action != null)
			{
				action(cell, last);
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x000234FE File Offset: 0x000218FE
		// (set) Token: 0x06000540 RID: 1344 RVA: 0x00023508 File Offset: 0x00021908
		public bool Mostrar_Animaciones
		{
			get
			{
				return this.mostrar_animaciones;
			}
			set
			{
				this.mostrar_animaciones = value;
				bool flag = this.mostrar_animaciones;
				if (flag)
				{
					this.animaciones_timer.Start();
				}
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x00023533 File Offset: 0x00021933
		// (set) Token: 0x06000542 RID: 1346 RVA: 0x0002353B File Offset: 0x0002193B
		public bool Mostrar_Celdas_Id
		{
			get
			{
				return this.mostrar_celdas;
			}
			set
			{
				this.mostrar_celdas = value;
				base.Invalidate();
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x0002354C File Offset: 0x0002194C
		// (set) Token: 0x06000544 RID: 1348 RVA: 0x00023554 File Offset: 0x00021954
		[Browsable(false)]
		public int RealCellHeight { get; private set; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x0002355D File Offset: 0x0002195D
		// (set) Token: 0x06000546 RID: 1350 RVA: 0x00023565 File Offset: 0x00021965
		[Browsable(false)]
		public int RealCellWidth { get; private set; }

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x0002356E File Offset: 0x0002196E
		// (set) Token: 0x06000548 RID: 1352 RVA: 0x00023576 File Offset: 0x00021976
		public Color ColorCeldaInactiva { get; set; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x0002357F File Offset: 0x0002197F
		// (set) Token: 0x0600054A RID: 1354 RVA: 0x00023587 File Offset: 0x00021987
		public Color ColorCeldaActiva { get; set; }

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x00023590 File Offset: 0x00021990
		// (set) Token: 0x0600054C RID: 1356 RVA: 0x00023598 File Offset: 0x00021998
		public bool TraceOnOver { get; set; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x000235A1 File Offset: 0x000219A1
		// (set) Token: 0x0600054E RID: 1358 RVA: 0x000235A9 File Offset: 0x000219A9
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public CeldaMapa CurrentCellOver { get; set; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x000235B2 File Offset: 0x000219B2
		// (set) Token: 0x06000550 RID: 1360 RVA: 0x000235BA File Offset: 0x000219BA
		public Color BorderColorOnOver { get; set; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x000235C3 File Offset: 0x000219C3
		// (set) Token: 0x06000552 RID: 1362 RVA: 0x000235CB File Offset: 0x000219CB
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public CeldaMapa[] celdas { get; set; }

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x000235D4 File Offset: 0x000219D4
		// (set) Token: 0x06000554 RID: 1364 RVA: 0x000235DC File Offset: 0x000219DC
		public CalidadMapa TipoCalidad
		{
			get
			{
				return this.tipo_calidad;
			}
			set
			{
				this.tipo_calidad = value;
				base.Invalidate();
			}
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x000235ED File Offset: 0x000219ED
		public void set_Cuenta(Cuenta _cuenta)
		{
			this.cuenta = _cuenta;
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x000235F8 File Offset: 0x000219F8
		private void aplicar_Calidad_Mapa(Graphics g)
		{
			switch (this.tipo_calidad)
			{
			case CalidadMapa.BAJA:
				g.CompositingMode = CompositingMode.SourceOver;
				g.CompositingQuality = CompositingQuality.HighSpeed;
				g.InterpolationMode = InterpolationMode.Low;
				g.SmoothingMode = SmoothingMode.HighSpeed;
				break;
			case CalidadMapa.MEDIA:
				g.CompositingMode = CompositingMode.SourceOver;
				g.CompositingQuality = CompositingQuality.GammaCorrected;
				g.InterpolationMode = InterpolationMode.High;
				g.SmoothingMode = SmoothingMode.AntiAlias;
				break;
			case CalidadMapa.ALTA:
				g.CompositingMode = CompositingMode.SourceOver;
				g.CompositingQuality = CompositingQuality.HighQuality;
				g.InterpolationMode = InterpolationMode.HighQualityBicubic;
				g.SmoothingMode = SmoothingMode.HighQuality;
				break;
			}
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0002368C File Offset: 0x00021A8C
		public void set_Celda_Numero()
		{
			this.celdas = new CeldaMapa[(int)(2 * this.mapa_altura * this.mapa_anchura)];
			short num = 0;
			for (int i = 0; i < (int)this.mapa_altura; i++)
			{
				for (int j = 0; j < (int)(this.mapa_anchura * 2); j++)
				{
					short num2 = num;
					num = num2 + 1;
					CeldaMapa celdaMapa = new CeldaMapa(num2);
					this.celdas[(int)celdaMapa.id] = celdaMapa;
				}
			}
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00023708 File Offset: 0x00021B08
		private double get_Maximo_Escalado()
		{
			double val = (double)base.Width / (double)(this.mapa_anchura + 1);
			double num = (double)base.Height / (double)(this.mapa_altura + 1);
			return Math.Min(num * 2.0, val);
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00023754 File Offset: 0x00021B54
		public void dibujar_Cuadricula()
		{
			int num = 0;
			double maximo_Escalado = this.get_Maximo_Escalado();
			double num2 = Math.Ceiling(maximo_Escalado / 2.0);
			int num3 = Convert.ToInt32(((double)base.Width - ((double)this.mapa_anchura + 0.5) * maximo_Escalado) / 2.0);
			int num4 = Convert.ToInt32(((double)base.Height - ((double)this.mapa_altura + 0.5) * num2) / 2.0);
			double num5 = num2 / 2.0;
			double num6 = maximo_Escalado / 2.0;
			for (int i = 0; i <= (int)(2 * this.mapa_altura - 1); i++)
			{
				bool flag = i % 2 == 0;
				if (flag)
				{
					for (int j = 0; j <= (int)(this.mapa_anchura - 1); j++)
					{
						Point point = new Point(Convert.ToInt32((double)num3 + (double)j * maximo_Escalado), Convert.ToInt32((double)num4 + (double)i * num5 + num5));
						Point point2 = new Point(Convert.ToInt32((double)num3 + (double)j * maximo_Escalado + num6), Convert.ToInt32((double)num4 + (double)i * num5));
						Point point3 = new Point(Convert.ToInt32((double)num3 + (double)j * maximo_Escalado + maximo_Escalado), Convert.ToInt32((double)num4 + (double)i * num5 + num5));
						Point point4 = new Point(Convert.ToInt32((double)num3 + (double)j * maximo_Escalado + num6), Convert.ToInt32((double)num4 + (double)i * num5 + num2));
						this.celdas[num++].Puntos = new Point[]
						{
							point,
							point2,
							point3,
							point4
						};
					}
				}
				else
				{
					for (int k = 0; k <= (int)(this.mapa_anchura - 2); k++)
					{
						Point point5 = new Point(Convert.ToInt32((double)num3 + (double)k * maximo_Escalado + num6), Convert.ToInt32((double)num4 + (double)i * num5 + num5));
						Point point6 = new Point(Convert.ToInt32((double)num3 + (double)k * maximo_Escalado + maximo_Escalado), Convert.ToInt32((double)num4 + (double)i * num5));
						Point point7 = new Point(Convert.ToInt32((double)num3 + (double)k * maximo_Escalado + maximo_Escalado + num6), Convert.ToInt32((double)num4 + (double)i * num5 + num5));
						Point point8 = new Point(Convert.ToInt32((double)num3 + (double)k * maximo_Escalado + maximo_Escalado), Convert.ToInt32((double)num4 + (double)i * num5 + num2));
						this.celdas[num++].Puntos = new Point[]
						{
							point5,
							point6,
							point7,
							point8
						};
					}
				}
			}
			this.RealCellHeight = (int)num2;
			this.RealCellWidth = (int)maximo_Escalado;
			base.Invalidate();
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00023A50 File Offset: 0x00021E50
		public void dibujar_Celdas(Graphics g)
		{
			this.aplicar_Calidad_Mapa(g);
			g.Clear(this.BackColor);
			CeldaMapa[] celdas = this.celdas;
			for (int i = 0; i < celdas.Length; i++)
			{
				CeldaMapa celda = celdas[i];
				bool flag = celda.esta_En_Rectangulo(g.ClipBounds);
				if (flag)
				{
					switch (celda.estado)
					{
					case CeldaEstado.CAMINABLE:
					{
						celda.dibujar_Color(g, Color.Gray, new Color?(Color.White));
						bool flag2 = this.mostrar_celdas;
						if (flag2)
						{
							celda.dibujar_Celda_Id(this, g);
						}
						break;
					}
					case CeldaEstado.NO_CAMINABLE:
					case CeldaEstado.PELEA_EQUIPO_AZUL:
					case CeldaEstado.PELEA_EQUIPO_ROJO:
						goto IL_15C;
					case CeldaEstado.CELDA_TELEPORT:
						celda.dibujar_Color(g, Color.Gray, new Color?(Color.Orange));
						celda.dibujar_Celda_Id(this, g);
						break;
					case CeldaEstado.OBJETO_INTERACTIVO:
						celda.dibujar_Color(g, Color.LightGoldenrodYellow, new Color?(Color.LightGoldenrodYellow));
						celda.dibujar_Celda_Id(this, g);
						break;
					case CeldaEstado.OBSTACULO:
					{
						bool flag3 = this.mostrar_celdas;
						if (flag3)
						{
							celda.dibujar_Celda_Id(this, g);
						}
						else
						{
							celda.dibujar_Obstaculo(g, Color.Gray, Color.FromArgb(60, 60, 60));
						}
						break;
					}
					default:
						goto IL_15C;
					}
					IL_17A:
					bool flag4 = this.cuenta != null;
					if (flag4)
					{
						bool flag5 = celda.id == this.cuenta.juego.personaje.celda.id && !this.animaciones.ContainsKey(this.cuenta.juego.personaje.id);
						if (flag5)
						{
							celda.dibujar_FillPie(g, Color.Blue, (float)(this.RealCellHeight / 2));
						}
						else
						{
							bool flag6 = (from m in this.cuenta.juego.mapa.entidades.Values
							where m is Monstruos
							select m).FirstOrDefault((Entidad m) => m.celda.id == celda.id && !this.animaciones.ContainsKey(m.id)) != null;
							if (flag6)
							{
								celda.dibujar_FillPie(g, Color.DarkRed, (float)(this.RealCellHeight / 2));
							}
							else
							{
								bool flag7 = (from n in this.cuenta.juego.mapa.entidades.Values
								where n is Npcs
								select n).FirstOrDefault((Entidad n) => n.celda.id == celda.id && !this.animaciones.ContainsKey(n.id)) != null;
								if (flag7)
								{
									celda.dibujar_FillPie(g, Color.FromArgb(179, 120, 211), (float)(this.RealCellHeight / 2));
								}
								else
								{
									bool flag8 = (from p in this.cuenta.juego.mapa.entidades.Values
									where p is Personajes
									select p).FirstOrDefault((Entidad p) => p.celda.id == celda.id && !this.animaciones.ContainsKey(p.id)) != null;
									if (flag8)
									{
										celda.dibujar_FillPie(g, Color.FromArgb(81, 113, 202), (float)(this.RealCellHeight / 2));
									}
								}
							}
						}
					}
					goto IL_377;
					IL_15C:
					celda.dibujar_Color(g, Color.Gray, new Color?(Color.DarkGray));
					goto IL_17A;
				}
				IL_377:
				this.dibujar_Animaciones(g);
			}
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00023DEC File Offset: 0x000221EC
		public void agregar_Animacion(int id, List<Celda> path, int duracion, TipoAnimaciones actor)
		{
			bool flag = path.Count < 2 || !this.mostrar_animaciones;
			if (!flag)
			{
				bool flag2 = this.animaciones.ContainsKey(id);
				if (flag2)
				{
					this.animacion_Finalizada(this.animaciones[id]);
				}
				MovimientoAnimacion movimientoAnimacion = new MovimientoAnimacion(id, from f in path
				select this.celdas[(int)f.id], duracion, actor);
				movimientoAnimacion.finalizado += this.animacion_Finalizada;
				this.animaciones.TryAdd(id, movimientoAnimacion);
				movimientoAnimacion.iniciar();
			}
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00023E7C File Offset: 0x0002227C
		private void animacion_Finalizada(MovimientoAnimacion animacion)
		{
			animacion.finalizado -= this.animacion_Finalizada;
			MovimientoAnimacion movimientoAnimacion;
			this.animaciones.TryRemove(animacion.entidad_id, out movimientoAnimacion);
			animacion.Dispose();
			base.Invalidate();
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00023EC0 File Offset: 0x000222C0
		private void dibujar_Animaciones(Graphics g)
		{
			foreach (MovimientoAnimacion movimientoAnimacion in this.animaciones.Values)
			{
				bool flag = movimientoAnimacion.path == null;
				if (!flag)
				{
					using (SolidBrush solidBrush = new SolidBrush(this.get_Animacion_Color(movimientoAnimacion)))
					{
						g.FillPie(solidBrush, movimientoAnimacion.actual_punto.X - (float)(this.RealCellHeight / 2 / 2), movimientoAnimacion.actual_punto.Y - (float)(this.RealCellHeight / 2 / 2), (float)(this.RealCellHeight / 2), (float)(this.RealCellHeight / 2), 0f, 360f);
					}
				}
			}
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00023FA8 File Offset: 0x000223A8
		private void animacion_Finalizada(object sender, ElapsedEventArgs e)
		{
			bool flag = this.animaciones.Count > 0;
			if (flag)
			{
				base.Invalidate();
			}
			else
			{
				bool flag2 = !this.mostrar_animaciones;
				if (flag2)
				{
					this.animaciones_timer.Stop();
				}
			}
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00023FF0 File Offset: 0x000223F0
		private Color get_Animacion_Color(MovimientoAnimacion animacion)
		{
			TipoAnimaciones tipo_animacion = animacion.tipo_animacion;
			TipoAnimaciones tipoAnimaciones = tipo_animacion;
			Color result;
			if (tipoAnimaciones != TipoAnimaciones.PERSONAJE)
			{
				if (tipoAnimaciones != TipoAnimaciones.GRUPO_MONSTRUOS)
				{
					result = Color.FromArgb(81, 113, 202);
				}
				else
				{
					result = Color.DarkRed;
				}
			}
			else
			{
				result = Color.Blue;
			}
			return result;
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00024034 File Offset: 0x00022434
		public void refrescar_Mapa()
		{
			bool flag = this.cuenta.juego.mapa == null;
			if (!flag)
			{
				this.animaciones.Clear();
				this.animaciones_timer.Stop();
				Celda[] celdas = this.cuenta.juego.mapa.celdas;
				foreach (Celda celda in celdas)
				{
					this.celdas[(int)celda.id].estado = CeldaEstado.NO_CAMINABLE;
					bool flag2 = celda.es_Caminable();
					if (flag2)
					{
						this.celdas[(int)celda.id].estado = CeldaEstado.CAMINABLE;
					}
					bool es_linea_vision = celda.es_linea_vision;
					if (es_linea_vision)
					{
						this.celdas[(int)celda.id].estado = CeldaEstado.OBSTACULO;
					}
					bool flag3 = celda.es_Teleport();
					if (flag3)
					{
						this.celdas[(int)celda.id].estado = CeldaEstado.CELDA_TELEPORT;
					}
					bool flag4 = celda.es_Interactivo();
					if (flag4)
					{
						this.celdas[(int)celda.id].estado = CeldaEstado.OBJETO_INTERACTIVO;
					}
				}
				this.animaciones_timer.Start();
				base.Invalidate();
			}
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0002415A File Offset: 0x0002255A
		protected override void OnPaint(PaintEventArgs e)
		{
			this.dibujar_Celdas(e.Graphics);
			base.OnPaint(e);
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x00024172 File Offset: 0x00022572
		protected override void OnResize(EventArgs e)
		{
			this.dibujar_Cuadricula();
			base.OnResize(e);
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x00024184 File Offset: 0x00022584
		protected override void OnMouseMove(MouseEventArgs e)
		{
			bool flag = this.mapa_raton_abajo;
			if (flag)
			{
				CeldaMapa celdaMapa = this.get_Celda(e.Location);
				bool flag2 = this.celda_retenida != null && this.celda_retenida != celdaMapa;
				if (flag2)
				{
					this.OnCellClicked(this.celda_retenida, e.Button, true);
					this.celda_retenida = celdaMapa;
				}
				bool flag3 = celdaMapa != null;
				if (flag3)
				{
					this.OnCellClicked(celdaMapa, e.Button, true);
				}
			}
			bool traceOnOver = this.TraceOnOver;
			if (traceOnOver)
			{
				CeldaMapa celdaMapa2 = this.get_Celda(e.Location);
				Rectangle rectangle = Rectangle.Empty;
				CeldaMapa last = null;
				bool flag4 = this.CurrentCellOver != null && this.CurrentCellOver != celdaMapa2;
				if (flag4)
				{
					this.CurrentCellOver.MouseOverPen = null;
					rectangle = this.CurrentCellOver.Rectangulo;
					last = this.CurrentCellOver;
				}
				bool flag5 = celdaMapa2 != null;
				if (flag5)
				{
					celdaMapa2.MouseOverPen = new Pen(this.BorderColorOnOver, 1f);
					rectangle = ((rectangle != Rectangle.Empty) ? Rectangle.Union(rectangle, celdaMapa2.Rectangulo) : celdaMapa2.Rectangulo);
					this.CurrentCellOver = celdaMapa2;
				}
				this.OnCellOver(celdaMapa2, last);
				bool flag6 = rectangle != Rectangle.Empty;
				if (flag6)
				{
					base.Invalidate(rectangle);
				}
			}
			base.OnMouseMove(e);
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x000242EC File Offset: 0x000226EC
		protected override void OnMouseDown(MouseEventArgs e)
		{
			CeldaMapa celdaMapa = this.get_Celda(e.Location);
			bool flag = celdaMapa != null;
			if (flag)
			{
				this.celda_retenida = (this.celda_abajo = celdaMapa);
			}
			this.mapa_raton_abajo = true;
			base.OnMouseDown(e);
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00024330 File Offset: 0x00022730
		protected override void OnMouseUp(MouseEventArgs e)
		{
			this.mapa_raton_abajo = false;
			CeldaMapa celdaMapa = this.get_Celda(e.Location);
			bool flag = this.celda_retenida != null;
			if (flag)
			{
				this.OnCellClicked(this.celda_retenida, e.Button, celdaMapa != this.celda_abajo);
				this.celda_retenida = null;
			}
			base.OnMouseUp(e);
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00024390 File Offset: 0x00022790
		public CeldaMapa get_Celda(Point p)
		{
			return this.celdas.FirstOrDefault((CeldaMapa cell) => cell.esta_En_Rectangulo(new Rectangle(p.X - this.RealCellWidth, p.Y - this.RealCellHeight, this.RealCellWidth, this.RealCellHeight)) && ControlMapa.PointInPoly(cell.Puntos, p));
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x000243C8 File Offset: 0x000227C8
		public static bool PointInPoly(Point[] poly, Point p)
		{
			bool flag = false;
			bool flag2 = poly.Length < 3;
			bool result;
			if (flag2)
			{
				result = false;
			}
			else
			{
				int num = poly[poly.Length - 1].X;
				int num2 = poly[poly.Length - 1].Y;
				foreach (Point point in poly)
				{
					int x = point.X;
					int y = point.Y;
					bool flag3 = x > num;
					int num3;
					int num4;
					int num5;
					int num6;
					if (flag3)
					{
						num3 = num;
						num4 = x;
						num5 = num2;
						num6 = y;
					}
					else
					{
						num3 = x;
						num4 = num;
						num5 = y;
						num6 = num2;
					}
					bool flag4 = x < p.X == p.X <= num && ((long)p.Y - (long)num5) * (long)(num4 - num3) < ((long)num6 - (long)num5) * (long)(p.X - num3);
					if (flag4)
					{
						flag = !flag;
					}
					num = x;
					num2 = y;
				}
				result = flag;
			}
			return result;
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x000244D0 File Offset: 0x000228D0
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00024508 File Offset: 0x00022908
		private void InitializeComponent()
		{
			base.SuspendLayout();
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Name = "Mapa";
			base.Size = new Size(523, 347);
			base.ResumeLayout(false);
		}

		// Token: 0x04000356 RID: 854
		private CalidadMapa tipo_calidad;

		// Token: 0x04000357 RID: 855
		private bool mapa_raton_abajo;

		// Token: 0x04000358 RID: 856
		private CeldaMapa celda_retenida;

		// Token: 0x04000359 RID: 857
		private CeldaMapa celda_abajo;

		// Token: 0x0400035A RID: 858
		private Cuenta cuenta;

		// Token: 0x0400035B RID: 859
		private ConcurrentDictionary<int, MovimientoAnimacion> animaciones;

		// Token: 0x0400035C RID: 860
		private System.Timers.Timer animaciones_timer;

		// Token: 0x0400035D RID: 861
		private bool mostrar_animaciones;

		// Token: 0x0400035E RID: 862
		private bool mostrar_celdas;

		// Token: 0x04000369 RID: 873
		private IContainer components = null;

		// Token: 0x020000E1 RID: 225
		// (Invoke) Token: 0x060006F3 RID: 1779
		public delegate void CellClickedHandler(CeldaMapa celda, MouseButtons botones, bool abajo);
	}
}
