using System;
using MoonSharp.Interpreter;

namespace RetroElite.Otros.Scripts.Banderas
{
	// Token: 0x02000018 RID: 24
	public class FuncionPersonalizada : Bandera
	{
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00006094 File Offset: 0x00004494
		// (set) Token: 0x0600011A RID: 282 RVA: 0x0000609C File Offset: 0x0000449C
		public DynValue funcion { get; private set; }

		// Token: 0x0600011B RID: 283 RVA: 0x000060A5 File Offset: 0x000044A5
		public FuncionPersonalizada(DynValue _funcion)
		{
			this.funcion = _funcion;
		}
	}
}
