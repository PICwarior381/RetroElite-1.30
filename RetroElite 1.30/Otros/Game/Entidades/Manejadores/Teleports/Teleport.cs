using System;
using System.Diagnostics;

namespace RetroElite.Otros.Game.Entidades.Manejadores.Teleports
{
	// Token: 0x02000065 RID: 101
	public class Teleport
	{
		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06000438 RID: 1080 RVA: 0x0000F280 File Offset: 0x0000D680
		// (remove) Token: 0x06000439 RID: 1081 RVA: 0x0000F2B8 File Offset: 0x0000D6B8
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<bool> fin_teleport;
	}
}
