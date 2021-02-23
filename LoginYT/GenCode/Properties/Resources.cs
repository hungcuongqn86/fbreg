using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace GenCode.Properties
{
	// Token: 0x02000039 RID: 57
	[CompilerGenerated]
	[DebuggerNonUserCode]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	internal class Resources
	{
		// Token: 0x06000115 RID: 277 RVA: 0x0000CCC0 File Offset: 0x0000AEC0
		internal Resources()
		{
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000116 RID: 278 RVA: 0x0000CCCC File Offset: 0x0000AECC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				bool flag = Resources.resourceMan == null;
				if (flag)
				{
					ResourceManager temp = new ResourceManager("GenCode.Properties.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = temp;
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000117 RID: 279 RVA: 0x0000CD14 File Offset: 0x0000AF14
		// (set) Token: 0x06000118 RID: 280 RVA: 0x0000CD2B File Offset: 0x0000AF2B
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x040001E3 RID: 483
		private static ResourceManager resourceMan;

		// Token: 0x040001E4 RID: 484
		private static CultureInfo resourceCulture;
	}
}
