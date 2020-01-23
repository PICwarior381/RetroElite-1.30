using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace RetroElite.Properties
{
	// Token: 0x0200000D RID: 13
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.3.0.0")]
	internal sealed partial class Settings : ApplicationSettingsBase
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00003C50 File Offset: 0x00002050
		public static Settings Default
		{
			get
			{
				return Settings.defaultInstance;
			}
		}

		// Token: 0x0400001E RID: 30
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());
	}
}
