using System;

namespace GenCode
{
	// Token: 0x02000017 RID: 23
	public class ModelTranslateResponse
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00007AF0 File Offset: 0x00005CF0
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00007AF8 File Offset: 0x00005CF8
		public int status { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00007B01 File Offset: 0x00005D01
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00007B09 File Offset: 0x00005D09
		public string data { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00007B12 File Offset: 0x00005D12
		// (set) Token: 0x0600007D RID: 125 RVA: 0x00007B1A File Offset: 0x00005D1A
		public string from { get; set; }
	}
}
