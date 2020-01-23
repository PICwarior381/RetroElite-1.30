using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace RetroElite.Controles.TabControl
{
	// Token: 0x02000079 RID: 121
	public class Cabezera : Control
	{
		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x00022393 File Offset: 0x00020793
		// (set) Token: 0x06000508 RID: 1288 RVA: 0x0002239B File Offset: 0x0002079B
		public string propiedad_Cuenta
		{
			get
			{
				return this.cuenta;
			}
			set
			{
				this.cuenta = value;
				base.Invalidate();
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x000223AC File Offset: 0x000207AC
		// (set) Token: 0x0600050A RID: 1290 RVA: 0x000223B4 File Offset: 0x000207B4
		public string propiedad_Estado
		{
			get
			{
				return this.estado;
			}
			set
			{
				this.estado = value;
				base.Invalidate();
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x000223C5 File Offset: 0x000207C5
		// (set) Token: 0x0600050C RID: 1292 RVA: 0x000223CD File Offset: 0x000207CD
		public string propiedad_Grupo
		{
			get
			{
				return this.grupo;
			}
			set
			{
				this.grupo = value;
				base.Invalidate();
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x000223DE File Offset: 0x000207DE
		// (set) Token: 0x0600050E RID: 1294 RVA: 0x000223E6 File Offset: 0x000207E6
		public Image propiedad_Imagen
		{
			get
			{
				return this.imagen;
			}
			set
			{
				this.imagen = value;
				base.Invalidate();
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600050F RID: 1295 RVA: 0x000223F7 File Offset: 0x000207F7
		// (set) Token: 0x06000510 RID: 1296 RVA: 0x000223FF File Offset: 0x000207FF
		public bool propiedad_Esta_Seleccionada
		{
			get
			{
				return this.esta_seleccionada;
			}
			set
			{
				this.esta_seleccionada = value;
				base.Invalidate();
			}
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00022410 File Offset: 0x00020810
		public Cabezera()
		{
			this.DoubleBuffered = true;
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.FixedHeight | ControlStyles.OptimizedDoubleBuffer, true);
			this.Cursor = Cursors.Hand;
			base.Size = new Size(150, 40);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00022450 File Offset: 0x00020850
		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics graphics = e.Graphics;
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
			graphics.InterpolationMode = InterpolationMode.High;
			base.OnPaint(e);
			Rectangle rect = new Rectangle(0, 0, 0, base.Height);
			using (SolidBrush solidBrush = new SolidBrush(this.esta_seleccionada ? Color.FromArgb(65, 65, 65) : Control.DefaultBackColor))
			{
				graphics.FillRectangle(solidBrush, rect);
			}
			bool flag = this.imagen != null;
			if (flag)
			{
				graphics.DrawImage(this.imagen, new Rectangle(8, 8, 25, 25));
				rect.X += 40;
			}
			Font font = new Font(this.Font.FontFamily, this.Font.Size - 0.6f);
			bool flag2 = !string.IsNullOrEmpty(this.cuenta) && !string.IsNullOrEmpty(this.estado) && !string.IsNullOrEmpty(this.grupo);
			if (flag2)
			{
				SizeF sizeF = graphics.MeasureString(this.cuenta, font);
				SizeF sizeF2 = graphics.MeasureString(this.estado, font);
				SizeF sizeF3 = graphics.MeasureString(this.grupo, font);
				graphics.DrawString(char.ToUpper(this.cuenta[0]).ToString() + this.cuenta.Substring(1), font, Brushes.FloralWhite, (float)rect.X, 25f - (sizeF.Height + sizeF2.Height + sizeF3.Height) / 2f);
				graphics.DrawString("Etat : " + this.estado, font, Brushes.FloralWhite, (float)rect.X, 20f - (sizeF.Height + sizeF2.Height + sizeF3.Height) / 2f + sizeF.Height);
				graphics.DrawString("Action : " + this.grupo, font, Brushes.FloralWhite, (float)rect.X, 15f - (sizeF.Height + sizeF2.Height + sizeF3.Height) / 2f + sizeF.Height + sizeF2.Height);
			}
			else
			{
				bool flag3 = !string.IsNullOrEmpty(this.cuenta);
				if (flag3)
				{
					SizeF sizeF4 = graphics.MeasureString(this.cuenta, font);
					graphics.DrawString(this.cuenta, font, Brushes.Black, (float)rect.X, 25f - sizeF4.Height / 2f);
				}
			}
		}

		// Token: 0x04000337 RID: 823
		public string cuenta;

		// Token: 0x04000338 RID: 824
		public string estado;

		// Token: 0x04000339 RID: 825
		public string grupo;

		// Token: 0x0400033A RID: 826
		public Image imagen;

		// Token: 0x0400033B RID: 827
		public bool esta_seleccionada;
	}
}
