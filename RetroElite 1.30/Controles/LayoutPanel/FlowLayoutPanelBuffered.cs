using System;
using System.Windows.Forms;

namespace RetroElite.Controles.LayoutPanel
{
	// Token: 0x0200007E RID: 126
	internal class FlowLayoutPanelBuffered : FlowLayoutPanel
	{
		// Token: 0x06000533 RID: 1331 RVA: 0x000232F1 File Offset: 0x000216F1
		public FlowLayoutPanelBuffered()
		{
			this.DoubleBuffered = true;
			base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
		}
	}
}
