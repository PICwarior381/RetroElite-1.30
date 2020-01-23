using System;
using System.Reflection;

namespace RetroElite.Comun.Frames.Transporte
{
	// Token: 0x0200008A RID: 138
	public class PaqueteDatos
	{
		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x00025CC7 File Offset: 0x000240C7
		// (set) Token: 0x060005B4 RID: 1460 RVA: 0x00025CCF File Offset: 0x000240CF
		public object instancia { get; set; }

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x00025CD8 File Offset: 0x000240D8
		// (set) Token: 0x060005B6 RID: 1462 RVA: 0x00025CE0 File Offset: 0x000240E0
		public string nombre_paquete { get; set; }

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x00025CE9 File Offset: 0x000240E9
		// (set) Token: 0x060005B8 RID: 1464 RVA: 0x00025CF1 File Offset: 0x000240F1
		public MethodInfo informacion { get; set; }

		// Token: 0x060005B9 RID: 1465 RVA: 0x00025CFA File Offset: 0x000240FA
		public PaqueteDatos(object _instancia, string _nombre_paquete, MethodInfo _informacion)
		{
			this.instancia = _instancia;
			this.nombre_paquete = _nombre_paquete;
			this.informacion = _informacion;
		}
	}
}
