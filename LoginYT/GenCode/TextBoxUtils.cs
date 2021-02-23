using System;
using System.Windows.Forms;

namespace GenCode
{
	// Token: 0x02000034 RID: 52
	internal class TextBoxUtils
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000FA RID: 250 RVA: 0x0000C9F4 File Offset: 0x0000ABF4
		public static TextBoxUtils Instance
		{
			get
			{
				TextBoxUtils._instance = (TextBoxUtils._instance ?? new TextBoxUtils());
				return TextBoxUtils._instance;
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000CA20 File Offset: 0x0000AC20
		public void AppendRichText(Control ctl, RichTextBox tb, string text)
		{
			try
			{
				bool invokeRequired = tb.InvokeRequired;
				if (invokeRequired)
				{
					TextBoxUtils.SetTextCallback d = new TextBoxUtils.SetTextCallback(this.AppendRichText);
					ctl.Invoke(d, new object[]
					{
						ctl,
						tb,
						text
					});
				}
				else
				{
					tb.AppendText(text);
					this.setFocusEndTextbox(tb);
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000CA90 File Offset: 0x0000AC90
		public void AppendRichText2(Control ctl, RichTextBox tb, string text)
		{
			try
			{
				bool invokeRequired = tb.InvokeRequired;
				if (invokeRequired)
				{
					TextBoxUtils.SetTextCallback d = new TextBoxUtils.SetTextCallback(this.AppendRichText2);
					ctl.Invoke(d, new object[]
					{
						ctl,
						tb,
						text
					});
				}
				else
				{
					bool flag = tb.Text.Length > 20000;
					if (flag)
					{
						tb.Text = "";
					}
					tb.AppendText(text);
					this.setFocusEndTextbox(tb);
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000CB24 File Offset: 0x0000AD24
		public void AppendRichTextLn(Control ctl, RichTextBox tb, string text)
		{
			this.AppendRichText(ctl, tb, text + "\n");
		}

		// Token: 0x060000FE RID: 254 RVA: 0x0000CB3B File Offset: 0x0000AD3B
		public void AppendRichTextLn2(Control ctl, RichTextBox tb, string text)
		{
			this.AppendRichText2(ctl, tb, text + "\n");
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0000CB54 File Offset: 0x0000AD54
		public void SetButtonText(Control ctl, Button tb, string text)
		{
			try
			{
				bool invokeRequired = tb.InvokeRequired;
				if (invokeRequired)
				{
					TextBoxUtils.SetButtonCallback d = new TextBoxUtils.SetButtonCallback(this.SetButtonText);
					ctl.Invoke(d, new object[]
					{
						ctl,
						tb,
						text
					});
				}
				else
				{
					tb.Text = text;
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0000CBBC File Offset: 0x0000ADBC
		public void AppendText(Control ctl, TextBox tb, string text)
		{
			try
			{
				bool invokeRequired = tb.InvokeRequired;
				if (invokeRequired)
				{
					TextBoxUtils.SetTextBoxCallback d = new TextBoxUtils.SetTextBoxCallback(this.AppendText);
					ctl.Invoke(d, new object[]
					{
						ctl,
						tb,
						text
					});
				}
				else
				{
					tb.AppendText(text);
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000CC24 File Offset: 0x0000AE24
		public void AppendTextLn(Control ctl, TextBox tb, string text)
		{
			this.AppendText(ctl, tb, text + "\n");
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0000CC3C File Offset: 0x0000AE3C
		public void SetLabelText(Control ctl, Label tb, string text)
		{
			try
			{
				bool invokeRequired = tb.InvokeRequired;
				if (invokeRequired)
				{
					TextBoxUtils.SetLabelCallback d = new TextBoxUtils.SetLabelCallback(this.SetLabelText);
					ctl.Invoke(d, new object[]
					{
						ctl,
						tb,
						text
					});
				}
				else
				{
					tb.Text = text;
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000103 RID: 259 RVA: 0x0000CCA4 File Offset: 0x0000AEA4
		public void setFocusEndTextbox(RichTextBox _tb)
		{
			_tb.SelectionStart = _tb.Text.Length;
			_tb.ScrollToCaret();
		}

		// Token: 0x040001E2 RID: 482
		private static TextBoxUtils _instance;

		// Token: 0x02000035 RID: 53
		// (Invoke) Token: 0x06000106 RID: 262
		private delegate void SetTextCallback(Control ctl, RichTextBox tb, string text);

		// Token: 0x02000036 RID: 54
		// (Invoke) Token: 0x0600010A RID: 266
		private delegate void SetTextBoxCallback(Control ctl, TextBox tb, string text);

		// Token: 0x02000037 RID: 55
		// (Invoke) Token: 0x0600010E RID: 270
		private delegate void SetButtonCallback(Control ctl, Button tb, string text);

		// Token: 0x02000038 RID: 56
		// (Invoke) Token: 0x06000112 RID: 274
		private delegate void SetLabelCallback(Control ctl, Label tb, string text);
	}
}
