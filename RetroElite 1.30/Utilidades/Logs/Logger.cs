using System;
using System.Diagnostics;
using RetroElite.Utilidades.Configuracion;

namespace RetroElite.Utilidades.Logs
{
	// Token: 0x02000004 RID: 4
	public class Logger
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600000A RID: 10 RVA: 0x0000219C File Offset: 0x0000059C
		// (remove) Token: 0x0600000B RID: 11 RVA: 0x000021D4 File Offset: 0x000005D4
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<LogMensajes, string> log_evento;

		// Token: 0x0600000C RID: 12 RVA: 0x0000220C File Offset: 0x0000060C
		private void log_Final(string referencia, string mensaje, string color, Exception ex = null)
		{
			try
			{
				LogMensajes arg = new LogMensajes(referencia, mensaje, ex);
				Action<LogMensajes, string> action = this.log_evento;
				if (action != null)
				{
					action(arg, color);
				}
			}
			catch (Exception ex2)
			{
				this.log_Final("LOGGER", "Se produjo una excepción al activar el evento registrado.", LogTipos.ERROR, ex2);
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002268 File Offset: 0x00000668
		private void log_Final(string referencia, string mensaje, LogTipos color, Exception ex = null)
		{
			bool flag = color == LogTipos.DEBUG && !GlobalConf.mostrar_mensajes_debug;
			if (!flag)
			{
				int num = (int)color;
				this.log_Final(referencia, mensaje, num.ToString("X"), ex);
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022A8 File Offset: 0x000006A8
		public void log_Error(string referencia, string mensaje)
		{
			this.log_Final(referencia, mensaje, "e74c3c", null);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022B9 File Offset: 0x000006B9
		public void log_Peligro(string referencia, string mensaje)
		{
			this.log_Final(referencia, mensaje, LogTipos.PELIGRO, null);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022CA File Offset: 0x000006CA
		public void log_informacion(string referencia, string mensaje)
		{
			this.log_Final(referencia, mensaje, "9b59b6", null);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022DB File Offset: 0x000006DB
		public void log_Fight(string referencia, string mensaje)
		{
			this.log_Final(referencia, mensaje, "2ecc71", null);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022EC File Offset: 0x000006EC
		public void log_Script(string referencia, string mensaje)
		{
			this.log_Final(referencia, mensaje, "3498db", null);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022FD File Offset: 0x000006FD
		public void log_normal(string referencia, string mensaje)
		{
			this.log_Final(referencia, mensaje, "1E90FF", null);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000230E File Offset: 0x0000070E
		public void log_privado(string referencia, string mensaje)
		{
			this.log_Final(referencia, mensaje, "f1c40f", null);
		}
	}
}
