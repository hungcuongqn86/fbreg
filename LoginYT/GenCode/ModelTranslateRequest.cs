using System;

namespace GenCode
{
	// Token: 0x02000018 RID: 24
	public class ModelTranslateRequest
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00007B23 File Offset: 0x00005D23
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00007B2B File Offset: 0x00005D2B
		public string text { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00007B34 File Offset: 0x00005D34
		// (set) Token: 0x06000082 RID: 130 RVA: 0x00007B3C File Offset: 0x00005D3C
		public string code { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00007B45 File Offset: 0x00005D45
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00007B4D File Offset: 0x00005D4D
		public string clientcode { get; set; }
	}
}
