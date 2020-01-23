using System;

namespace RetroElite.Comun.Frames.Transporte
{
	// Token: 0x02000089 RID: 137
	internal class PaqueteAtributo : Attribute
	{
		// Token: 0x060005B2 RID: 1458 RVA: 0x00025CB7 File Offset: 0x000240B7
		public PaqueteAtributo(string _paquete)
		{
			this.paquete = _paquete;
		}

		// Token: 0x04000392 RID: 914
		public string paquete;
	}
}
