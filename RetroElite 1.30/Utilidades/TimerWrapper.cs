using System;
using System.Threading;

namespace RetroElite.Utilidades
{
	// Token: 0x02000003 RID: 3
	internal class TimerWrapper : IDisposable
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020BF File Offset: 0x000004BF
		// (set) Token: 0x06000003 RID: 3 RVA: 0x000020C7 File Offset: 0x000004C7
		public bool habilitado { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020D0 File Offset: 0x000004D0
		// (set) Token: 0x06000005 RID: 5 RVA: 0x000020D8 File Offset: 0x000004D8
		public int intervalo { get; set; }

		// Token: 0x06000006 RID: 6 RVA: 0x000020E1 File Offset: 0x000004E1
		public TimerWrapper(int _intervalo, TimerCallback callback)
		{
			this.intervalo = _intervalo;
			this.timer = new Timer(callback, null, -1, -1);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002104 File Offset: 0x00000504
		public void Start(bool inmediatamente = false)
		{
			bool habilitado = this.habilitado;
			if (!habilitado)
			{
				this.habilitado = true;
				this.timer.Change(inmediatamente ? 0 : this.intervalo, this.intervalo);
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002144 File Offset: 0x00000544
		public void Stop()
		{
			bool flag = !this.habilitado;
			if (!flag)
			{
				this.habilitado = false;
				Timer timer = this.timer;
				if (timer != null)
				{
					timer.Change(-1, -1);
				}
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000217D File Offset: 0x0000057D
		public void Dispose()
		{
			Timer timer = this.timer;
			if (timer != null)
			{
				timer.Dispose();
			}
			this.timer = null;
		}

		// Token: 0x04000001 RID: 1
		private Timer timer;
	}
}
