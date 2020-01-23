using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace RetroElite.Controles.ControlMapa.Celdas
{
	// Token: 0x02000082 RID: 130
	public class CeldaMapa
	{
		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x00024574 File Offset: 0x00022974
		// (set) Token: 0x0600056C RID: 1388 RVA: 0x0002457C File Offset: 0x0002297C
		public CeldaEstado estado { get; set; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600056D RID: 1389 RVA: 0x00024585 File Offset: 0x00022985
		// (set) Token: 0x0600056E RID: 1390 RVA: 0x0002458D File Offset: 0x0002298D
		public Brush CustomBrush { get; set; }

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x00024596 File Offset: 0x00022996
		// (set) Token: 0x06000570 RID: 1392 RVA: 0x0002459E File Offset: 0x0002299E
		public Pen CustomBorderPen { get; set; }

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x000245A7 File Offset: 0x000229A7
		// (set) Token: 0x06000572 RID: 1394 RVA: 0x000245AF File Offset: 0x000229AF
		public Pen MouseOverPen { get; set; }

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x000245B8 File Offset: 0x000229B8
		// (set) Token: 0x06000574 RID: 1396 RVA: 0x000245C0 File Offset: 0x000229C0
		public Rectangle Rectangulo { get; private set; }

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x000245CC File Offset: 0x000229CC
		public Point Centro
		{
			get
			{
				return new Point((this.Puntos[0].X + this.Puntos[2].X) / 2, (this.Puntos[1].Y + this.Puntos[3].Y) / 2);
			}
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00024628 File Offset: 0x00022A28
		public CeldaMapa(short _id)
		{
			this.id = _id;
			this.estado = CeldaEstado.NO_CAMINABLE;
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x00024641 File Offset: 0x00022A41
		// (set) Token: 0x06000578 RID: 1400 RVA: 0x00024649 File Offset: 0x00022A49
		public Point[] Puntos
		{
			get
			{
				return this.mapa_puntos;
			}
			set
			{
				this.mapa_puntos = value;
				this.RefreshBounds();
			}
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0002465C File Offset: 0x00022A5C
		public void RefreshBounds()
		{
			int num = this.Puntos.Min((Point entry) => entry.X);
			int num2 = this.Puntos.Min((Point entry) => entry.Y);
			int width = this.Puntos.Max((Point entry) => entry.X) - num;
			int height = this.Puntos.Max((Point entry) => entry.Y) - num2;
			this.Rectangulo = new Rectangle(num, num2, width, height);
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0002472C File Offset: 0x00022B2C
		public void dibujar_Color(Graphics g, Color borderColor, Color? fillingColor)
		{
			using (GraphicsPath graphicsPath = new GraphicsPath())
			{
				graphicsPath.AddLines(this.Puntos);
				bool flag = fillingColor != null;
				if (flag)
				{
					using (SolidBrush solidBrush = new SolidBrush(fillingColor.Value))
					{
						g.FillPath(solidBrush, graphicsPath);
					}
				}
				using (Pen pen = new Pen(borderColor))
				{
					g.DrawPath(pen, graphicsPath);
				}
			}
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x000247D8 File Offset: 0x00022BD8
		public virtual void dibujar_Celda_Id(ControlMapa parent, Graphics g)
		{
			StringFormat format = new StringFormat
			{
				Alignment = StringAlignment.Center,
				LineAlignment = StringAlignment.Center
			};
			g.DrawString(this.id.ToString(), parent.Font, Brushes.Black, new RectangleF((float)this.Rectangulo.X, (float)this.Rectangulo.Y, (float)this.Rectangulo.Width, (float)this.Rectangulo.Height), format);
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0002485C File Offset: 0x00022C5C
		public void dibujar_FillPie(Graphics g, Color color, float size)
		{
			using (SolidBrush solidBrush = new SolidBrush(color))
			{
				g.FillPie(solidBrush, (float)this.Puntos[1].X - size / 2f, (float)this.Puntos[1].Y + 4.2f, size, size, 0f, 360f);
			}
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x000248D8 File Offset: 0x00022CD8
		public void dibujar_Obstaculo(Graphics g, Color borderColor, Color fillingColor)
		{
			using (GraphicsPath graphicsPath = new GraphicsPath())
			{
				graphicsPath.AddLines(new PointF[]
				{
					new PointF((float)this.Puntos[0].X, (float)(this.Puntos[0].Y - 10)),
					new PointF((float)this.Puntos[1].X, (float)(this.Puntos[1].Y - 10)),
					new PointF((float)this.Puntos[2].X, (float)(this.Puntos[2].Y - 10)),
					new PointF((float)this.Puntos[3].X, (float)(this.Puntos[3].Y - 10)),
					new PointF((float)this.Puntos[0].X, (float)(this.Puntos[0].Y - 10))
				});
				graphicsPath.AddLines(new PointF[]
				{
					new PointF((float)this.Puntos[0].X, (float)(this.Puntos[0].Y - 10)),
					new PointF((float)this.Puntos[3].X, (float)(this.Puntos[3].Y - 10)),
					this.Puntos[3],
					this.Puntos[0],
					new PointF((float)this.Puntos[0].X, (float)(this.Puntos[0].Y - 10))
				});
				graphicsPath.AddLines(new PointF[]
				{
					new PointF((float)this.Puntos[3].X, (float)(this.Puntos[3].Y - 10)),
					new PointF((float)this.Puntos[2].X, (float)(this.Puntos[2].Y - 10)),
					this.Puntos[2],
					this.Puntos[3],
					new PointF((float)this.Puntos[3].X, (float)(this.Puntos[3].Y - 10))
				});
				using (SolidBrush solidBrush = new SolidBrush(fillingColor))
				{
					g.FillPath(solidBrush, graphicsPath);
				}
				using (Pen pen = new Pen(borderColor))
				{
					g.DrawPath(pen, graphicsPath);
				}
			}
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00024C3C File Offset: 0x0002303C
		public bool esta_En_Rectangulo(RectangleF rectangulo)
		{
			return this.Rectangulo.IntersectsWith(Rectangle.Ceiling(rectangulo));
		}

		// Token: 0x04000373 RID: 883
		public short id;

		// Token: 0x04000374 RID: 884
		private Point[] mapa_puntos;
	}
}
