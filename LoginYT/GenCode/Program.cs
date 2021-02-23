using System;
using System.Windows.Forms;

namespace GenCode
{
	// Token: 0x02000024 RID: 36
	internal static class Program
	{
		// Token: 0x060000AF RID: 175 RVA: 0x000091B8 File Offset: 0x000073B8
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new ManageChannel());
		}
	}
}
