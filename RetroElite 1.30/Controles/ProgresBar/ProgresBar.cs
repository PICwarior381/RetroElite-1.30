using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace RetroElite.Controles.ProgresBar
{
	// Token: 0x0200007C RID: 124
	[DefaultEvent("ValueChanged")]
	internal class ProgresBar : Control
	{
		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06000523 RID: 1315 RVA: 0x00022E40 File Offset: 0x00021240
		// (remove) Token: 0x06000524 RID: 1316 RVA: 0x00022E78 File Offset: 0x00021278
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler valor_cambiado;

		// Token: 0x06000525 RID: 1317 RVA: 0x00022EB0 File Offset: 0x000212B0
		public ProgresBar()
		{
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			this.DoubleBuffered = true;
			base.Size = new Size(100, 24);
			this.color = Color.FromArgb(102, 150, 232);
			this.valor_maximo = 100;
			this.valor = 0;
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x00022F10 File Offset: 0x00021310
		// (set) Token: 0x06000527 RID: 1319 RVA: 0x00022F18 File Offset: 0x00021318
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
				base.Invalidate();
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x00022F2A File Offset: 0x0002132A
		// (set) Token: 0x06000529 RID: 1321 RVA: 0x00022F34 File Offset: 0x00021334
		public Color color_Barra
		{
			get
			{
				return this.color;
			}
			set
			{
				bool flag = this.color == value;
				if (!flag)
				{
					this.color = value;
					base.Invalidate();
				}
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x00022F62 File Offset: 0x00021362
		// (set) Token: 0x0600052B RID: 1323 RVA: 0x00022F6C File Offset: 0x0002136C
		public int valor_Maximo
		{
			get
			{
				return this.valor_maximo;
			}
			set
			{
				bool flag = this.valor_maximo == value;
				if (!flag)
				{
					this.valor_maximo = value;
					bool flag2 = this.valor > this.valor_maximo;
					if (flag2)
					{
						this.valor = this.valor_maximo;
					}
					base.Invalidate();
				}
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x00022FB5 File Offset: 0x000213B5
		// (set) Token: 0x0600052D RID: 1325 RVA: 0x00022FC0 File Offset: 0x000213C0
		public int Valor
		{
			get
			{
				return this.valor;
			}
			set
			{
				bool flag = this.valor == value;
				if (!flag)
				{
					this.valor = value;
					bool flag2 = this.valor > this.valor_maximo;
					if (flag2)
					{
						this.valor = this.valor_maximo;
					}
					else
					{
						bool flag3 = this.valor < 0;
						if (flag3)
						{
							this.valor = 0;
						}
					}
					base.Invalidate();
					EventHandler eventHandler = this.valor_cambiado;
					if (eventHandler != null)
					{
						eventHandler(this, EventArgs.Empty);
					}
				}
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600052E RID: 1326 RVA: 0x00023037 File Offset: 0x00021437
		// (set) Token: 0x0600052F RID: 1327 RVA: 0x0002303F File Offset: 0x0002143F
		public TipoProgresBar tipos_Barra
		{
			get
			{
				return this.tipo_barra;
			}
			set
			{
				this.tipo_barra = value;
				base.Invalidate();
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x00023050 File Offset: 0x00021450
		public int porcentaje
		{
			get
			{
				return (int)((double)this.Valor / (double)this.valor_Maximo * 100.0);
			}
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0002306C File Offset: 0x0002146C
		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics graphics = e.Graphics;
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
			graphics.InterpolationMode = InterpolationMode.High;
			graphics.Clear(this.BackColor);
			using (SolidBrush solidBrush = new SolidBrush(this.color_Barra))
			{
				double num = (double)(base.Width * this.porcentaje / 100);
				graphics.FillRectangle(solidBrush, 0, 0, (int)num, base.Height);
			}
			using (Pen pen = new Pen(Color.Black))
			{
				graphics.DrawLines(pen, new Point[]
				{
					new Point(0, 0),
					new Point(0, base.Height),
					new Point(base.Width, base.Height),
					new Point(base.Width, 0),
					new Point(0, 0)
				});
			}
			using (SolidBrush solidBrush2 = new SolidBrush(this.ForeColor))
			{
				SizeF sizeF = graphics.MeasureString(this.get_Texto_Barra(), this.Font);
				graphics.DrawString(this.get_Texto_Barra(), this.Font, solidBrush2, (float)(base.Width / 2) - sizeF.Width / 2f, (float)(base.Height / 2) - sizeF.Height / 2f);
			}
			base.OnPaint(e);
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00023214 File Offset: 0x00021614
		private string get_Texto_Barra()
		{
			string result;
			switch (this.tipo_barra)
			{
			case TipoProgresBar.VALOR_MAXIMO_PORCENTAJE:
				result = string.Format("{0}/{1} ({2}%)", this.valor, this.valor_maximo, this.porcentaje);
				break;
			case TipoProgresBar.VALOR_MAXIMO:
				result = string.Format("{0}/{1}", this.valor, this.valor_maximo);
				break;
			case TipoProgresBar.VALOR_PORCENTAJE:
				result = string.Format("{0} ({1}%)", this.valor, this.porcentaje);
				break;
			case TipoProgresBar.TEXTO_PORCENTAJE:
				result = string.Format("{0} ({1}%)", this.Text, this.porcentaje);
				break;
			default:
				result = string.Format("{0}%", this.porcentaje);
				break;
			}
			return result;
		}

		// Token: 0x04000345 RID: 837
		private Color color;

		// Token: 0x04000346 RID: 838
		private int valor_maximo;

		// Token: 0x04000347 RID: 839
		private int valor;

		// Token: 0x04000348 RID: 840
		private TipoProgresBar tipo_barra;
	}
}
