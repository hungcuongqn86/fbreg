using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace GenCode.Properties
{
	// Token: 0x0200003A RID: 58
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
	[CompilerGenerated]
	internal sealed partial class Settings : ApplicationSettingsBase
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000119 RID: 281 RVA: 0x0000CD34 File Offset: 0x0000AF34
		public static Settings Default
		{
			get
			{
				return Settings.defaultInstance;
			}
		}

		// Token: 0x040001E5 RID: 485
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());
	}
}
