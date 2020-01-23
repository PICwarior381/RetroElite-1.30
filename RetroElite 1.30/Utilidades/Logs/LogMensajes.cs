using System;
using System.Text;

namespace RetroElite.Utilidades.Logs
{
	// Token: 0x02000005 RID: 5
	public class LogMensajes
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002328 File Offset: 0x00000728
		public string referencia { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002330 File Offset: 0x00000730
		public string mensaje { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002338 File Offset: 0x00000738
		public Exception exception { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002340 File Offset: 0x00000740
		public bool es_Exception
		{
			get
			{
				return this.exception != null;
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000234B File Offset: 0x0000074B
		public LogMensajes(string _referencia, string _mensaje, Exception _exception)
		{
			this.referencia = _referencia;
			this.mensaje = _mensaje;
			this.exception = _exception;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000236C File Offset: 0x0000076C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = string.IsNullOrEmpty(this.referencia) ? "" : ("[" + this.referencia + "]");
			stringBuilder.Append(string.Concat(new string[]
			{
				"[",
				DateTime.Now.ToString("HH:mm:ss"),
				"] ",
				text,
				" ",
				this.mensaje
			}));
			bool es_Exception = this.es_Exception;
			if (es_Exception)
			{
				stringBuilder.Append(string.Format("{0}- Exception ocurrida: {1}", Environment.NewLine, this.exception));
			}
			return stringBuilder.ToString();
		}
	}
}
