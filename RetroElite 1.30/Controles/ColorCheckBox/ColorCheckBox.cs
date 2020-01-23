using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace RetroElite.Controles.ColorCheckBox
{
	// Token: 0x02000085 RID: 133
	public class ColorCheckBox : CheckBox
	{
		// Token: 0x0600058F RID: 1423 RVA: 0x00024FCE File Offset: 0x000233CE
		public ColorCheckBox()
		{
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x00024FE4 File Offset: 0x000233E4
		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics graphics = e.Graphics;
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
			graphics.InterpolationMode = InterpolationMode.High;
			base.OnPaint(e);
			using (SolidBrush solidBrush = new SolidBrush(this.BackColor))
			{
				graphics.FillRectangle(solidBrush, new Rectangle(0, 0, base.Width - 2, base.Height));
			}
			bool @checked = base.Checked;
			if (@checked)
			{
				using (GraphicsPath graphicsPath = new GraphicsPath())
				{
					graphicsPath.AddLines(new Point[]
					{
						new Point(2, base.Height / 2),
						new Point(base.Width / 3, base.Height - 3),
						new Point(base.Width - 2, base.Height / 3)
					});
					using (Pen pen = new Pen(Color.White, 2f))
					{
						graphics.DrawPath(pen, graphicsPath);
					}
				}
			}
			bool flag = !base.Enabled;
			if (flag)
			{
				using (SolidBrush solidBrush2 = new SolidBrush(Color.FromArgb(120, Color.Gray)))
				{
					graphics.FillRectangle(solidBrush2, 0, 0, base.Width, base.Height);
				}
			}
		}
	}
}
