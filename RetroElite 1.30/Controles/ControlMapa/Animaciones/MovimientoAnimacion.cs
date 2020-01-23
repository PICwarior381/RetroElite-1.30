using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using RetroElite.Controles.ControlMapa.Celdas;

namespace RetroElite.Controles.ControlMapa.Animaciones
{
	// Token: 0x02000083 RID: 131
	internal class MovimientoAnimacion : IDisposable
	{
		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x00024C5D File Offset: 0x0002305D
		// (set) Token: 0x06000580 RID: 1408 RVA: 0x00024C65 File Offset: 0x00023065
		public int entidad_id { get; private set; }

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x00024C6E File Offset: 0x0002306E
		// (set) Token: 0x06000582 RID: 1410 RVA: 0x00024C76 File Offset: 0x00023076
		public List<CeldaMapa> path { get; private set; }

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000583 RID: 1411 RVA: 0x00024C7F File Offset: 0x0002307F
		// (set) Token: 0x06000584 RID: 1412 RVA: 0x00024C87 File Offset: 0x00023087
		public PointF actual_punto { get; private set; }

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000585 RID: 1413 RVA: 0x00024C90 File Offset: 0x00023090
		// (set) Token: 0x06000586 RID: 1414 RVA: 0x00024C98 File Offset: 0x00023098
		public TipoAnimaciones tipo_animacion { get; private set; }

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06000587 RID: 1415 RVA: 0x00024CA4 File Offset: 0x000230A4
		// (remove) Token: 0x06000588 RID: 1416 RVA: 0x00024CDC File Offset: 0x000230DC
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<MovimientoAnimacion> finalizado;

		// Token: 0x06000589 RID: 1417 RVA: 0x00024D14 File Offset: 0x00023114
		public MovimientoAnimacion(int _entidad_id, IEnumerable<CeldaMapa> _path, int duration, TipoAnimaciones _tipo_animacion)
		{
			this.entidad_id = _entidad_id;
			this.path = new List<CeldaMapa>(_path);
			this.tipo_animacion = _tipo_animacion;
			this.timer = new Timer(new TimerCallback(this.realizar_Animacion), null, -1, -1);
			this.iniciar_Frames();
			this.tiempo_por_frame = duration / this.frames.Count;
			this.index_frame = 0;
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00024D84 File Offset: 0x00023184
		private void iniciar_Frames()
		{
			this.frames = new List<PointF>();
			for (int i = 0; i < this.path.Count - 1; i++)
			{
				this.frames.AddRange(this.get_Punto_Entre_Dos(this.path[i].Centro, this.path[i + 1].Centro, 3));
			}
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x00024DFA File Offset: 0x000231FA
		public void iniciar()
		{
			this.actual_punto = this.frames[this.index_frame];
			this.timer.Change(this.tiempo_por_frame, this.tiempo_por_frame);
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00024E30 File Offset: 0x00023230
		private PointF[] get_Punto_Entre_Dos(PointF p1, PointF p2, int cantidad)
		{
			PointF[] array = new PointF[cantidad];
			float num = p2.Y - p1.Y;
			float num2 = p2.X - p1.X;
			double num3 = (double)(p2.Y - p1.Y) / (double)(p2.X - p1.X);
			cantidad--;
			for (double num4 = 0.0; num4 < (double)cantidad; num4 += 1.0)
			{
				double num5 = (num3 == 0.0) ? 0.0 : ((double)num * (num4 / (double)cantidad));
				double a = (num3 == 0.0) ? ((double)num2 * (num4 / (double)cantidad)) : (num5 / num3);
				array[(int)num4] = new PointF((float)Math.Round(a) + p1.X, (float)Math.Round(num5) + p1.Y);
			}
			array[cantidad] = p2;
			return array;
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00024F34 File Offset: 0x00023334
		private void realizar_Animacion(object state)
		{
			this.index_frame++;
			this.actual_punto = this.frames[this.index_frame];
			bool flag = this.index_frame == this.frames.Count - 1;
			if (flag)
			{
				this.timer.Change(-1, -1);
				Action<MovimientoAnimacion> action = this.finalizado;
				if (action != null)
				{
					action(this);
				}
			}
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00024FA4 File Offset: 0x000233A4
		public void Dispose()
		{
			this.path.Clear();
			this.timer.Dispose();
			this.path = null;
			this.timer = null;
		}

		// Token: 0x0400037E RID: 894
		private int index_frame;

		// Token: 0x0400037F RID: 895
		private int tiempo_por_frame;

		// Token: 0x04000380 RID: 896
		private Timer timer;

		// Token: 0x04000381 RID: 897
		private List<PointF> frames;
	}
}
