using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using xNet;

namespace GenCode
{
	// Token: 0x02000025 RID: 37
	public class RegClone
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x000091D3 File Offset: 0x000073D3
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x000091DB File Offset: 0x000073DB
		public List<string> _listKeyword { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x000091E4 File Offset: 0x000073E4
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x000091EC File Offset: 0x000073EC
		public bool _closeChrome { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x000091F5 File Offset: 0x000073F5
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x000091FD File Offset: 0x000073FD
		public RegClone.LogDelegate _logDelegate { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00009206 File Offset: 0x00007406
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x0000920E File Offset: 0x0000740E
		public string _ThreadName { get; set; }
		public string _clone_uid { get; set; }

		// Token: 0x060000B8 RID: 184 RVA: 0x00009218 File Offset: 0x00007418
		private string randomNumber(int length)
		{
			string s = "";
			for (int i = 0; i < length; i++)
			{
				s += new Random(Guid.NewGuid().GetHashCode()).Next(0, 10).ToString();
			}
			return s;
		}


		public string bm_id = "";
		public string bm_token = "";

		// Token: 0x0400005B RID: 91
		private string _userAgent = "Mozilla/5.0 (iPhone; CPU iPhone OS 13_3_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/13.0.5 Mobile/15E148 Snapchat/10.77.5.59 (like Safari/604.1)";

		// Token: 0x0400005C RID: 92
		private string _userAgent2 = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36";

		// Token: 0x0400005D RID: 93
		private string _contentType = "application/x-www-form-urlencoded";

		private void Waitf2AjaxLoading(By byFinter1, By byFinter2, int time = 3)
		{
			WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(time));
			Func<IWebDriver, bool> waitLoading = new Func<IWebDriver, bool>((IWebDriver Web) =>
			{
				try
				{
					ReadOnlyCollection<IWebElement> alertE = Web.FindElements(byFinter1);
					if (alertE.Count > 0)
					{
						return true;
					}
					else
					{
						alertE = Web.FindElements(byFinter2);
						if (alertE.Count > 0)
						{
							return true;
                        }
                        else
                        {
							return false;
						}
					}
				}
				catch
				{
					return false;
				}
			});

			try
			{
				wait.Until(waitLoading);
			}
			catch { }
		}

		private void WaitAjaxLoading(By byFinter, int time = 3)
		{
			WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(time));
			Func<IWebDriver, bool> waitLoading = new Func<IWebDriver, bool>((IWebDriver Web) =>
			{
				try
				{
					ReadOnlyCollection<IWebElement> alertE = Web.FindElements(byFinter);
					if (alertE.Count > 0)
					{
						return true;
					}
					else
					{
						return false;
					}
				}
				catch
				{
					return false;
				}
			});

			try
			{
				wait.Until(waitLoading);
			}
			catch { }
		}

		private static void Delay(int Time_delay)
		{
			int i = 0;
			System.Timers.Timer _delayTimer = new System.Timers.Timer();
			_delayTimer.Interval = Time_delay;
			_delayTimer.AutoReset = false; //so that it only calls the method once
			_delayTimer.Elapsed += (s, args) => i = 1;
			_delayTimer.Start();
			while (i == 0) { };
		}

		private void WaitLoading()
		{
			// wait loading
			WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
			Func<IWebDriver, bool> waitLoading = new Func<IWebDriver, bool>((IWebDriver Web) =>
			{
				try
				{
					IWebElement alertE = Web.FindElement(By.Id("abccuongnh"));
					return false;
				}
				catch
				{
					return true;
				}
			});

			try
			{
				wait.Until(waitLoading);
			}
			catch { }
		}

		public void QuitDriver()
		{
			try
			{
				this._driver.Quit();
			}
			catch (Exception ex)
			{
				this.log(ex);
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000092D0 File Offset: 0x000074D0
		private string randomMailKhoiPhuc()
		{
			string _id = this._listKeyword[new Random(Guid.NewGuid().GetHashCode()).Next(0, this._listKeyword.Count)] + this._listKeyword[new Random(Guid.NewGuid().GetHashCode()).Next(0, this._listKeyword.Count)] + this.randomNumber(5);
			_id = _id.Replace(" ", "");
			return _id + "@yahoo.com";
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00009378 File Offset: 0x00007578
		private string randomGmail()
		{
			string _id = this._listKeyword[new Random(Guid.NewGuid().GetHashCode()).Next(0, this._listKeyword.Count)] + this._listKeyword[new Random(Guid.NewGuid().GetHashCode()).Next(0, this._listKeyword.Count)] + this.randomNumber(5);
			_id = _id.Replace(" ", "");
			return _id + "@gmail.com";
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00009420 File Offset: 0x00007620
		private string randomEmail()
		{
			string _id = this._listKeyword[new Random(Guid.NewGuid().GetHashCode()).Next(0, this._listKeyword.Count)] + this._listKeyword[new Random(Guid.NewGuid().GetHashCode()).Next(0, this._listKeyword.Count)] + this.randomNumber(5);
			_id = _id.Replace(" ", "");
			return _id + "@" + this._listDomain[new Random(Guid.NewGuid().GetHashCode()).Next(0, this._listDomain.Length - 1)];
		}

		private string getTempEmailCom()
		{
			try
			{
				this._driver.SwitchTo().Window(this.emailTabHandle);
				WaitLoading();
				Delay(1000);
                OpenQA.Selenium.Cookie cookie1 = this._driver.Manage().Cookies.GetCookieNamed("email");
                if (cookie1 is null)
                {
					this._driver.Navigate().Refresh();
					WaitLoading();
					Delay(1000);
					cookie1 = this._driver.Manage().Cookies.GetCookieNamed("email");
				}

				if (cookie1 is null)
				{
					this._driver.Navigate().Refresh();
					WaitLoading();
					Delay(1000);
					cookie1 = this._driver.Manage().Cookies.GetCookieNamed("email");
				}

				this._driver.SwitchTo().Window(this.regTabHandle);
				return Uri.UnescapeDataString(cookie1.Value);
			}
			catch (Exception ex)
			{
				this.log(ex);
				this._driver.SwitchTo().Window(this.regTabHandle);
				return null;
			}
		}

		private string getTempEmailComCode()
		{
			string _id = this._listKeyword[new Random(Guid.NewGuid().GetHashCode()).Next(0, this._listKeyword.Count)] + this._listKeyword[new Random(Guid.NewGuid().GetHashCode()).Next(0, this._listKeyword.Count)] + this.randomNumber(5);
			_id = _id.Replace(" ", "");
			return _id + "@" + this._listDomain[new Random(Guid.NewGuid().GetHashCode()).Next(0, this._listDomain.Length - 1)];
		}

		private string newTempEmailCom()
		{
			try
			{
				this._driver.SwitchTo().Window(this.emailTabHandle);
				WaitLoading();
				WaitAjaxLoading(By.Id("click-to-delete"), 5);
				Delay(1000);
				IWebElement _btDelete = this.getElement(By.Id("click-to-delete"));
				if (this.isValid(_btDelete))
				{
					_btDelete.Click();
					Delay(3000);
					WaitLoading();
				}
                else
                {
					this._driver.SwitchTo().Window(this.regTabHandle);
					return null;
				}

				OpenQA.Selenium.Cookie cookie1 = this._driver.Manage().Cookies.GetCookieNamed("email");
				if (cookie1 != null)
				{
					this._driver.SwitchTo().Window(this.regTabHandle);
					return Uri.UnescapeDataString(cookie1.Value);
				}
				this._driver.SwitchTo().Window(this.regTabHandle);
				return null;
			}
			catch (Exception ex)
			{
				this.log(ex);
				this._driver.SwitchTo().Window(this.regTabHandle);
				return null;
			}
		}

		private bool changeEmail(string newmail)
		{
			try
            {
				WaitAjaxLoading(By.XPath("//div[contains(@class, 'iOverlayFooter')]/a[contains(@class, 'ayerCancel')]"), 3);
				Delay(1000);
				IWebElement _btCancel = this.getElement(By.XPath("//div[contains(@class, 'iOverlayFooter')]/a[contains(@class, 'ayerCancel')]"));
				if (this.isValid(_btCancel))
				{
					_btCancel.Click();
				}

				WaitAjaxLoading(By.CssSelector("a[href*='/confirm/resend_code']"), 15);
				Delay(1000);
				IWebElement _btConfirmResend = this.getElement(By.CssSelector("a[href*='/confirm/resend_code']"));
				if (!this.isValid(_btConfirmResend))
                {
					return false;
				}
				_btConfirmResend.Click();

				WaitAjaxLoading(By.CssSelector("a[href*='/change_contactpoint'][rel='dialog-post']"), 10);
				Delay(500);
				_btConfirmResend = this.getElement(By.CssSelector("a[href*='/change_contactpoint'][rel='dialog-post']"));
				if (!this.isValid(_btConfirmResend))
				{
					return false;
				}
				_btConfirmResend.Click();

				WaitAjaxLoading(By.CssSelector("input[name='contactpoint']"), 10);
				Delay(1000);
				IWebElement _inputNewEmail = this.getElement(By.CssSelector("input[name='contactpoint']"));
				if (!this.isValid(_inputNewEmail))
				{
					return false;
				}

				this.fillInput(_inputNewEmail, newmail);

				// Submit
				WaitAjaxLoading(By.XPath("//div[contains(@class, 'iOverlayFooter')]/button[@type='submit']"));
				Delay(1000);
				ReadOnlyCollection<IWebElement> _btSubmitNewEmail = this._driver.FindElements(By.XPath("//div[contains(@class, 'iOverlayFooter')]/button[@type='submit']"));
				if (_btSubmitNewEmail.Count > 0)
				{
					if (this.isValid(_btSubmitNewEmail[0]))
					{
						_btSubmitNewEmail[0].Click();
						Delay(1000);
					}
				}

				// <a class="autofocus _9l2h  layerCancel _4jy0 _4jy3 _4jy1 _51sy selected _42ft" action="cancel" role="button" href="#" tabindex="0">Schließen</a>
				// Resupmit
				string confirmTag = "//a[contains(@class, 'layerCancel') and contains(@class, 'selected') and @role='button' and @action='cancel']";
				WaitAjaxLoading(By.XPath(confirmTag));
				Delay(1000);
				ReadOnlyCollection<IWebElement> _btSubmitConfirm = this._driver.FindElements(By.XPath(confirmTag));
				if (_btSubmitConfirm.Count > 0)
				{
					if (this.isValid(_btSubmitConfirm[0]))
					{
						_btSubmitConfirm[0].Click();
						Delay(1000);

						WaitAjaxLoading(By.XPath("//div[contains(@class, 'iOverlayFooter')]/button[@type='submit']"));
						Delay(1000);
						_btSubmitNewEmail = this._driver.FindElements(By.XPath("//div[contains(@class, 'iOverlayFooter')]/button[@type='submit']"));
						if (_btSubmitNewEmail.Count > 0)
						{
							if (this.isValid(_btSubmitNewEmail[0]))
							{
								_btSubmitNewEmail[0].Click();
								Delay(1000);
							}
						}
					}
				}

				return true;
            } catch
            {
				return false;
            }
		}

		private async Task<string[]> GenFakeInfoAsync()
		{
			try
			{
				HttpClient _client = new HttpClient();
				string text = await _client.GetStringAsync("https://fake-it.ws/de/");
				string _rs = text;
				text = null;
				string _tmpName = Regex.Match(_rs, "row..Name[<][/]th[>].{10,200}[/]span", RegexOptions.Singleline).ToString();
				_tmpName = _tmpName.Replace("row\">Name</th>", "").Trim().Replace("<td class=\"copy\"><span data-toggle=\"tooltip\" data-placement=\"top\" title=\"Click To Copy\">", "").Replace("</span", "");
				string[] _tmpNameArr = _tmpName.Split(new char[]
				{
								' '
				});
				return _tmpNameArr;
			}
			catch (Exception ex)
			{
				this.log(ex);
				return null;
			}
		}

		public async Task<bool> RegFaceBook()
		{
			bool _status = false;
			await Task.Run(async delegate ()
			{
				try
				{
					this._driver.Navigate().GoToUrl("https://facebook.com");
					((IJavaScriptExecutor)this._driver).ExecuteScript("window.open();");
					Delay(500);

					ReadOnlyCollection<string> tabs = this._driver.WindowHandles;
					if (tabs.Count < 2)
					{
						this.log("Die Tab!");
						this.log("End!");
						return;
					}

					this.regTabHandle = tabs[0];
					this.emailTabHandle = tabs[1];

					this._driver.SwitchTo().Window(this.emailTabHandle);
					this._driver.Navigate().GoToUrl("https://temp-mail.org/en/");
					this._driver.SwitchTo().Window(this.regTabHandle);

					WaitLoading();
					Delay(500);
					bool flag = this._driver.PageSource.Contains("This site can’t be reached");
					if (flag)
					{
						this.log("Die Socks");
						this.log("End!");
						return;
					}

					// cookiebanner
					string cookieBanner = "button[data-cookiebanner='accept_button']";
					WaitAjaxLoading(By.CssSelector(cookieBanner));
					Delay(500);
					IWebElement _acceptBt = this.getElement(By.CssSelector(cookieBanner));
					bool flag2 = this.isValid(_acceptBt);
					if (flag2)
					{
						_acceptBt.Click();
					}

					// this.log("Find Locate Link");
					WaitAjaxLoading(By.CssSelector("a[href*='facebook.com/']"));
					Delay(1000);
					ReadOnlyCollection<IWebElement> _locateList = this._driver.FindElements(By.CssSelector("a[href*='facebook.com/']"));
                    if (_locateList.Count < 1)
                    {
						this.log("Find Locate Link --- False!");
						this.log("End!");
						return;
					}
					string _url = _locateList[1].GetAttribute("href");
					this._driver.Navigate().GoToUrl(_url);

					// Find registration button
					string btnreg = "a[data-testid='open-registration-form-button']";
					WaitAjaxLoading(By.CssSelector(btnreg));
					Delay(1000);
					IWebElement _regBt = this.getElement(By.CssSelector(btnreg));
					if (!this.isValid(_regBt))
					{
						this.log("Find registration button --- False!");
						this.log("End!");
						return;
					}
					Actions actions = new Actions(this._driver);
					actions.MoveToElement(_regBt);
					Delay(500);
					actions.Perform();
					_regBt.Click();

					// Gen Fake Info
					string[] _tmpNameArr = await GenFakeInfoAsync();
                    if (_tmpNameArr is null)
                    {
						this.log("Gen Fake Info --- False!");
						this.log("End!");
						return;
					}

					string _randomEmail = this.randomGmail();
					string _passAcc = "F0KHFSa" + new Random(Guid.NewGuid().GetHashCode()).Next(10000, 99999);
					string _mailKhoiPhuc = this.getTempEmailCom();

					WaitLoading();
					WaitAjaxLoading(By.CssSelector("input[name='firstname']"), 60);
					Delay(1000);

					IWebElement _fName = this.getElement(By.CssSelector("input[name='firstname']"));
					if (!this.isValid(_fName))
					{
						this._driver.Navigate().Refresh();
						WaitLoading();
						WaitAjaxLoading(By.CssSelector("input[name='firstname']"), 60);
						Delay(1000);
						_fName = this.getElement(By.CssSelector("input[name='firstname']"));
					}

					if (!this.isValid(_fName))
                    {
						this.log("Firstname --- False!");
						this.log("End!");
						return;
					}

					this.fillInput(_fName, _tmpNameArr[0]);

					IWebElement _lName = this.getElement(By.CssSelector("input[name='lastname']"));
					if (this.isValid(_lName))
					{
						this.fillInput(_lName, _tmpNameArr[1]);
					}
					IWebElement _email = this.getElement(By.CssSelector("input[name='reg_email__']"));
					if (this.isValid(_email))
					{
						this.fillInput(_email, _randomEmail);
					}
					IWebElement _email2 = this.getElement(By.CssSelector("input[name='reg_email_confirmation__']"));
					if (this.isValid(_email2))
					{
						this.fillInput(_email2, _randomEmail);
					}
					IWebElement _pass = this.getElement(By.CssSelector("input[name='reg_passwd__']"));
					if (this.isValid(_pass))
					{
						this.fillInput(_pass, _passAcc);
					}
					SelectElement _selectDay = new SelectElement(this.getElement(By.Id("day")));
					Random _random = new Random(Guid.NewGuid().GetHashCode());
					_selectDay.SelectByIndex(_random.Next(1, 20));
					SelectElement _selectMonth = new SelectElement(this.getElement(By.Id("month")));
					_selectMonth.SelectByValue(_random.Next(1, 12).ToString());
					SelectElement _selectYear = new SelectElement(this.getElement(By.Id("year")));
					_selectYear.SelectByValue(_random.Next(1970, 1996).ToString());
					// this.log("Choose gender");
					ReadOnlyCollection<IWebElement> _gender = this._driver.FindElements(By.CssSelector("input[name='sex']"));
					if (_gender.Count > 1)
					{
						int _ranPer = _random.Next(0, 100);
						if (_ranPer % 2 == 0)
						{
							_gender[0].Click();
						}
						else
						{
							_gender[1].Click();
						}
					}

					// this.log("Find submit");
					IWebElement _submitBt = this.getElement(By.CssSelector("button[name='websubmit']"));
					if (!this.isValid(_submitBt))
                    {
						this.log("Find submit --- False!");
						this.log("End!");
						return;
					}
					_submitBt.Click();

					WaitLoading();
					Delay(500);
					if (this._driver.Url.Contains("https://www.facebook.com/checkpoint"))
					{
						this.log("Check point!");
						this.log("End!");
						return;
					}

					// Find submit change mail!
					WaitAjaxLoading(By.CssSelector("a[href*='/confirm/resend_code']"), 15);
					Delay(1000);
					IWebElement _btConfirmResend = this.getElement(By.CssSelector("a[href*='/confirm/resend_code']"));
					if (!this.isValid(_btConfirmResend))
					{
						_submitBt = this.getElement(By.CssSelector("button[name='websubmit']"));
						if (this.isValid(_submitBt))
						{
							Delay(120000);
							_submitBt.Click();
							WaitAjaxLoading(By.CssSelector("a[href*='/confirm/resend_code']"), 10);
							Delay(1000);
							_btConfirmResend = this.getElement(By.CssSelector("a[href*='/confirm/resend_code']"));
						}
					}

					WaitLoading();
					Delay(500);
					if (this._driver.Url.Contains("https://www.facebook.com/checkpoint"))
					{
						this.log("Check point!");
						this.log("End!");
						return;
					}

					if (!this.isValid(_btConfirmResend))
					{
						this.log("Not reg submit!");
						this.log("End!");
						return;
					}

					WaitLoading();
					Delay(500);
					if (this._driver.Url.Contains("https://www.facebook.com/checkpoint"))
					{
						this.log("Check point!");
						this.log("End!");
						return;
					}

					string secCode = "";
					this.log("Change email---");
					if (this.changeEmail(_mailKhoiPhuc))
					{
						Delay(1000);
						WaitLoading();
						if (this._driver.Url.Contains("https://www.facebook.com/checkpoint"))
						{
							this.log("Check point!");
							this.log("End!");
							return;
						}
						secCode = await this.getSecurityCode();
					}

					WaitLoading();
					Delay(500);
					if (this._driver.Url.Contains("https://www.facebook.com/checkpoint"))
					{
						this.log("Check point!");
						this.log("End!");
						return;
					}

					if (string.IsNullOrEmpty(secCode))
					{
						this.log("Get Auth Code ---- false!");
						this.log("End!");
						return;
					}

					IWebElement _inputCode = this.getElement(By.CssSelector("input[name='code']"));
					if (!this.isValid(_inputCode))
                    {
						this.log("Find input code ---- false!");
						this.log("End!");
						return;
					}

					this.fillInput(_inputCode, secCode);
					IWebElement _btConfirmCode = this.getElement(By.CssSelector("button[name='confirm']"));
					if (!this.isValid(_btConfirmCode))
					{
						this.log("Find Btn Confirm Code ---- false!");
						this.log("End!");
						return;
					}

					_btConfirmCode.Click();
					Delay(10000);
					if (this._driver.Url.Contains("https://www.facebook.com/checkpoint"))
					{
						this.log("Check point!");
						this.log("End!");
						return;
					}

					string _tmpCoookie = "";
					foreach (OpenQA.Selenium.Cookie _c in this._driver.Manage().Cookies.AllCookies)
					{
                        if (_c.Name == "c_user")
                        {
							_clone_uid = _c.Value;
						}
						_tmpCoookie = string.Concat(new string[] {_tmpCoookie, _c.Name, "=", _c.Value, "; "});
					}

                    // Vươt han che
                    if (_clone_uid != "")
                    {
						await this.OverLimit(_clone_uid, _tmpCoookie);
					}
					this.log("=====================================");
					this.log("-------------------------------------");
					this.log("Email: " + _randomEmail);
					this.log("Pass: " + _passAcc);
					this.log("Email khôi phục: " + _mailKhoiPhuc);
					this.log("Cookie: " + _tmpCoookie);
					this.log("-------------------------------------");
					this.log("=====================================");
					_status = true;
					return;
				}
				catch (Exception ex)
				{
					this.log(ex);
					this.log("End!");
					return;
				}
			});

			return _status;
		}

		public Task<bool> OverLimit(string clone_uid, string cookie)
		{
			try
			{
				this.log("Vượt hạn chế ...");
				string url = "https://m.facebook.com/";
				string data = this.GetData(null, url, cookie, this._userAgent);
				string value = Regex.Match(data, "\"token\":\"(.*?)\"").Groups[1].Value;
				string value2 = Regex.Match(data, "\"token\":\"(.*?)\"").NextMatch().Groups[1].Value;

				string text5 = "HTCFunny " + new Random().Next(100000, 999999).ToString();
				url = "https://m.facebook.com/pages/create/edit_name/";
				string data2 = string.Concat(new string[]
				{
						"page_name=",
						text5,
						"&m_sess=&fb_dtsg=",
						value,
						"&jazoest=22057&__dyn=1KQdAGm1gwHwh8-F42mml3onwSz8do98nwgU2owSwMxW0Horx64o720SUhwem0iy1gCwSxu0BU3JxO1ZxObwro1YUWUS0KU2Swk888C0NE2owZwaO2W0D81ao5G0zE5W0KE&__csr=&__req=2&__a=AYkpm3rAlv05MLP3ZtwecOT5Of275v7lUPwe5Gn_SmZJ8zHSj2Y-6vUOE2PO98Sn9PZE-w-MRx3UHpZCxEquI1cI813lsEPbCw3RnCrAH5TrzQ&__user=",
						clone_uid
				});

				string value3 = Regex.Match(this.PostData(null, url, data2, this._contentType, this._userAgent, cookie), "draftID\":(.*?),").Groups[1].Value;
				if (string.IsNullOrEmpty(value3))
				{
					this.log("Lỗi xử lý hạn chế ...");
					return Task.FromResult(false);
				}

				url = "https://m.facebook.com/pages/create/edit_category/";
				data2 = string.Concat(new string[]
				{
						"page_category=2903&ref_type=bookmark&draft_id=",
						value3,
						"&m_sess=&fb_dtsg=",
						value,
						"&jazoest=22054&__dyn=1KQdAGm1gwHwh8-F42mml3onwSz8do98nwgU2owSwMxW0Horx64o720SUhwem0iy1gCwSxu0BU3JxO1ZxObwro1YUWUS0KU2Swk888C0NE2owZwaO2W0D81ao5G0zE5W0KE&__csr=&__req=6&__a=AYnWsPV52kVUq1FSUGdWInyc8GdPY2rOHb6aQcAJnvZKwK0RW8fqMwyjxmjW4IE6wkNOLLzs9z9yWyXCrVN0NT1XAlXQYxznK0yzjteD2qQ0_Q&__user=",
						clone_uid
				});

				string value4 = Regex.Match(this.PostData(null, url, data2, this._contentType, this._userAgent, cookie), "pageID\":(.*?)}").Groups[1].Value;
				
				url = string.Concat(new string[]
				{
						"https://m.facebook.com/pages/boosted_component_v2/view/?entry_point=promote_action_button&page_id=",
						value4,
						"&product=boosted_pagelike&ref=bookmarks&__m_async_page__=&__big_pipe_on__=&m_sess=&fb_dtsg_ag=",
						value2,
						"&jazoest=28254&__dyn=1KQEGhpoO13xu4UpwGzWAg8-ml3kdy8qjxqcyoaU98nwCxyUcodUbEdEc8uK1lwZxm6Uhx6484-fz83ex65of85610wxw9e3C3y1gCwSxu0BU3JxO1ZxObwro7ifw5lxyeKdwGgaE6i2CE29wk888C0NE2oCwSwaOfxW0D86i0DU985G0zE5W0KE&__csr=&__req=n&__a=AYl9OAYB07bUn_I9AAOqNWG6b-Gyd9l7RxzndO2OZGwdVeJeviykO6nUcCZ0JxtRJ2c2WJrlZFPAxAQX9WOJ6WCaABMqPyo_ZygdtXOrdutaIg&__user=",
						clone_uid
				});
				// this.GetData(null, url, cookie, this._userAgent);
				this._driver.Navigate().GoToUrl(url);
				WaitLoading();
				this.log("Hoàn thành vượt hạn chế!");
				return Task.FromResult(true);
			}
			catch (Exception ex)
			{
				this.log(ex);
				return Task.FromResult(false);
			}
		}

		private string GetData(HttpRequest http, string url, string cookie = null, string userArgent = "")
		{
			string result;
			try
			{
				if (http == null)
				{
					http = new HttpRequest
					{
						Cookies = new CookieDictionary(false)
					};
				}
				if (!string.IsNullOrEmpty(cookie))
				{
					this.AddCookie(http, cookie);
				}
				if (!string.IsNullOrEmpty(userArgent))
				{
					http.UserAgent = userArgent;
				}
				else
				{
					http.UserAgent = this._userAgent;
				}
				result = http.Get(url, null).ToString();
			}
			catch
			{
				result = "";
			}
			return result;
		}

		private void AddCookie(HttpRequest http, string cookie)
		{
			string[] array = cookie.Split(new char[]
			{
				';'
			});
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(new char[]
				{
					'='
				});
				if (array2.Count<string>() > 1)
				{
					try
					{
						http.Cookies.Add(array2[0], array2[1]);
					}
					catch
					{
					}
				}
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00005A28 File Offset: 0x00003C28
		private string PostData(HttpRequest http, string url, string data = null, string contentType = null, string userArgent = "", string cookie = null)
		{
			string result;
			try
			{
				if (http == null)
				{
					http = new HttpRequest
					{
						Cookies = new CookieDictionary(false)
					};
				}
				if (!string.IsNullOrEmpty(cookie))
				{
					this.AddCookie(http, cookie);
				}
				if (string.IsNullOrEmpty(userArgent))
				{
					http.UserAgent = this._userAgent;
				}
				else
				{
					http.UserAgent = userArgent;
				}
				result = http.Post(url, data, contentType).ToString();
			}
			catch
			{
				result = "";
			}
			return result;
		}

		public Task<bool> shareAds(string clone_uid, string clone_adsID, string clone_cookie)
        {
			try
			{
				this.log("Lấy thông tin ads...");
				string text = clone_uid; // this.data_clone.Rows[index].Cells["clone_uid"].Value.ToString();
				string text4 = clone_adsID; // this.data_clone.Rows[index].Cells["clone_adsID"].Value.ToString();
				string text3 = clone_cookie; // this.data_clone.Rows[index].Cells["clone_cookie"].Value.ToString();

				string url = "https://m.facebook.com/";
				string data = this.GetData(null, url, text3, this._userAgent);
				string value = Regex.Match(data, "\"token\":\"(.*?)\"").Groups[1].Value;
				string value2 = Regex.Match(data, "\"token\":\"(.*?)\"").NextMatch().Groups[1].Value;

				url = "https://www.facebook.com/ads/manager/account_settings/information";
				if (!string.IsNullOrEmpty(text4))
				{
					url = "https://www.facebook.com/ads/manager/account_settings/information/?act=" + text4;
				}
				string data3 = this.GetData(null, url, text3, this._userAgent2);

				string text6 = Regex.Match(data3, "accountID:\"(.*?)\",accountName:\"(.*?)\"").Groups[1].Value;
				string value5 = Regex.Match(data3, "accountID:\"(.*?)\",accountName:\"(.*?)\"").Groups[2].Value;
				string value6 = Regex.Match(data3, "access_token:\"(.*?)\"").Groups[1].Value;


				this.log("Gửi lời mời...");
				url = string.Concat(new string[]
				{
						"https://graph.facebook.com/v7.0/",
						this.bm_id,
						"/client_ad_accounts?access_token=",
						this.bm_token,
						"&__cppo=1"
				});

				string data2 = "_reqName=object%3Abrand%2Fclient_ad_accounts&_reqSrc=AdAccountActions.brands&access_type=AGENCY&adaccount_id=act_" + text6 + "&locale=vi_VN&method=post&permitted_roles=%5B%5D&permitted_tasks=%5B%22ADVERTISE%22%2C%22ANALYZE%22%2C%22DRAFT%22%2C%22MANAGE%22%5D&pretty=0&suppress_http_code=1&xref=f25bf97b92021a";
				this.PostData(null, url, data2, this._contentType, this._userAgent, text3);

				this.log("Chấp nhận lời mời...");
				url = "https://www.facebook.com/ads/manager/agency_permission_requests_getter/?ad_account_id=" + text6;
				data2 = string.Concat(new string[]
				{
						"__user=",
						text,
						"&__a=1&__dyn=7xeUmBz8aolJ28S2qq7E-8mA5FaDJ4WqwOwCwgEpyA4WCHxC49pEG48coG49UKbigC6UnGiidBxa7GzUK3G4Wxa6US2SaAUgS4UgwxAzUO486C6EC8yEScx60DUcEiyEjCx65EJ0Bxq22q3KUnyFEdUmKFpobQUTwMQmqEjwznBwRyXxK9z9p87zxil1ap12U98lwWxecAwTK6pox0g8lUScyobo43xu7o4q2ycwgHAhUuyUlxeawywWjy8gK4EG11DwFg94bxRoCiewzwAwRyUgyU-1iwnHxJxK48cohAy88rxiezUuxe1dx-q4VEhwbSi2-fzobEaUiwBxe3mbxu3ydDxG2G4UOcwzxG9GdxS48bE4q8w&__csr=&__req=e&__beoa=0&__pc=PHASED%3Apowereditor_pkg&dpr=1&__ccg=MODERATE&__rev=1003171746&__s=942s7c%3A1e99qr%3A0z43po&__hsi=6916515816308183139-0&__comet_req=0&fb_dtsg=",
						value,
						"&jazoest=22044&__spin_r=1003171746&__spin_b=trunk&__spin_t=1610376829"
				});
				string value7 = Regex.Match(this.PostData(null, url, data2, this._contentType, this._userAgent2, text3), "ad_market_id=(.*?)&").Groups[1].Value;
				HttpRequest http = new HttpRequest
				{
					KeepAlive = true,
					Cookies = new CookieDictionary(false)
				};
				url = string.Concat(new string[]
				{
						"https://www.facebook.com/adaccount/agency/accept_reject_dialog/?ad_market_id=",
						value7,
						"&agency_id=",
						this.bm_id,
						"&fb_dtsg_ag=",
						value2,
						"&__user=",
						text,
						"&__a=1&__dyn=7xeUmBz8aolJ28S2qq7E-8mA5FaDJ4WqwOwCwgEpyA4WCHxC49pEG48coG49UKbigC6UnGiidBxa7GzUK3G4Wxa6US2SaAUgS4UgwxAzUO486C6EC8yEScx60DUcEiyEjCx65EJ0Bxq22q3KUnyFEdUmKFpobQUTwMQmqEjwznBwRyXxK9z9p87zxil1ap12U98lwWxecAwTK6pox0g8lUScyobo43xu7o4q2ycwgHAhUuyUlxeawywWjy8gK4EG11DwFg94bxRoCiewzwAwRyUgyU-1iwnHxJxK48cohAy88rxiezUuxe1dx-q4VE3fAwLzUS2W2K4EqyEjwRyUnwUzpUqwGxecz88Uqyqzotx22W16y8&__csr=&__req=x&__beoa=0&__pc=PHASED%3Apowereditor_pkg&dpr=1&__ccg=EXCELLENT&__rev=1003171746&__s=3pdtbb%3Ai0fgh7%3Amzdffu&__hsi=6916524890961458823-0&__comet_req=0&jazoest=27654&__spin_r=1003171746&__spin_b=trunk&__spin_t=1610378942"
				});
				string data4 = this.GetData(http, url, text3, this._userAgent2);
				string value8 = Regex.Match(data4, "ext=(.*?)&amp;hash=(.*?)\\\\").Groups[2].Value;
				string value9 = Regex.Match(data4, "ext=(.*?)&amp;hash=(.*?)\\\\").Groups[1].Value;
				url = string.Concat(new string[]
				{
						"https://www.facebook.com/adaccount/agency/request/accept_reject/?ad_market_id=",
						value7,
						"&agency_id=",
						this.bm_id,
						"&operation=0&ext=",
						value9,
						"&hash=",
						value8,
						"&fb_dtsg_ag=",
						value2,
						"&__user=",
						text,
						"&__a=1&__dyn=7xeUmBz8aolJ28S2qq7E-8mA5FaDJ4WqwOwCwgEpyA4WCHxC49pEG48coG49UKbigC6UnGiidBxa7GzUK3G4Wxa6US2SaAUgS4UgwxAzUO486C6EC8yEScx60DUcEiyEjCx65EJ0Bxq22q3KUnyFEdUmKFpobQUTwMQmqEjwznBwRyXxK9z9p87zxil1ap12U98lwWxecAwTK6pox0g8lUScyobo43xu7o4q2ycwgHAhUuyUlxeawywWjy8gK4EG11DwFg94bxRoCiewzwAwRyUgyU-1iwnHxJxK48cohAy88rxiezUuxe1dx-q4VEhwbSi2-fzobEaUiwBxe3mbxu3ydDxG2G4UOcwzxG9GdxS48bE4q8w&__csr=&__req=16&__beoa=0&__pc=PHASED%3Apowereditor_pkg&dpr=1&__ccg=MODERATE&__rev=1003171746&__s=0hndtg%3A1e99qr%3A0z43po&__hsi=6916515816308183139-0&__comet_req=0&jazoest=27566&__spin_r=1003171746&__spin_b=trunk&__spin_t=1610376829"
				});
				this.GetData(http, url, text3, this._userAgent2);
				url = "https://graph.facebook.com/v7.0/" + this.bm_id + "/client_ad_accounts?fields=id&limit=9999&access_token=" + this.bm_token;
				if (!Regex.Match(this.GetData(null, url, text3, this._userAgent), text6).Success)
				{
					url = string.Concat(new string[]
					{
							"https://graph.facebook.com/v7.0/",
							this.bm_id,
							"/adaccounts?access_token=",
							this.bm_token,
							"&__cppo=1"
					});
					data2 = "_reqName=object%3Abrand%2Fadaccounts&_reqSrc=AdAccountActions.brands&adaccount_id=act_" + text6 + "&locale=vi_VN&method=delete&pretty=0&suppress_http_code=1&xref=f124cfc137c91c8";
					this.PostData(null, url, data2, this._contentType, this._userAgent, text3);
					this.log("Share lỗi");
					return Task.FromResult(false);
				}
				this.log("Share thành công");
				return Task.FromResult(true);
			}
			catch (Exception ex)
			{
				this.log(ex);
				return Task.FromResult(false);
			}
		}

		private async Task<string> getSecurityCode()
		{
			try
			{
				string _tmpCode;
				this._driver.SwitchTo().Window(this.emailTabHandle);
				WaitLoading();
				Delay(1000);
				int _count = 0;
				string _code = "";

				for (; ; )
				{
					await Task.Delay(10000);
					this.log("Requet code -  " + _count);
					WaitAjaxLoading(By.CssSelector("div[class*='tm-content']"), 20);
					await Task.Delay(1000);
					ReadOnlyCollection<IWebElement> tmcontent = this._driver.FindElements(By.CssSelector("div[class*='tm-content']"));
                    if (tmcontent.Count > 0 )
                    {
						if (this.isValid(tmcontent[0]))
                        {
							Actions actions = new Actions(this._driver);
							actions.MoveToElement(tmcontent[0]);
							await Task.Delay(3000);
							actions.Perform();
						}
					}

					WaitAjaxLoading(By.CssSelector("div[class*='beforeContentBannerBl']"), 20);
					await Task.Delay(1000);
					tmcontent = this._driver.FindElements(By.CssSelector("div[class*='beforeContentBannerBl']"));
					if (tmcontent.Count > 0)
					{
						if (this.isValid(tmcontent[0]))
						{
							Actions actions = new Actions(this._driver);
							actions.MoveToElement(tmcontent[0]);
							await Task.Delay(3000);
							actions.Perform();
						}
					}

					string pageSource = this._driver.PageSource;
					_tmpCode = Regex.Match(pageSource, "FB[-][0-9]+").ToString();
					if (!string.IsNullOrEmpty(_tmpCode))
					{
						break;
                    }
                    else
                    {
						this._driver.Navigate().Refresh();
						WaitLoading();
					}

					_count++;
					if (_count >= 5)
					{
						goto Block_3;
					}
					_tmpCode = null;
				}
				_code = _tmpCode.Replace("FB-", "");
				Block_3:
				this.log("Code: " + _code);
				this._driver.SwitchTo().Window(this.regTabHandle);
				return _code;
			}
			catch (Exception ex)
			{
				this.log(ex);
				this._driver.SwitchTo().Window(this.regTabHandle);
				return null;
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00009590 File Offset: 0x00007790
		private void log(string s)
		{
			bool flag = this._logDelegate != null;
			if (flag)
			{
				this._logDelegate("Thread " + this._ThreadName + "->" + s);
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000095D0 File Offset: 0x000077D0
		private void log(Exception ex)
		{
			bool flag = this._logDelegate != null;
			if (flag)
			{
				this._logDelegate(string.Concat(new string[]
				{
					"Thread ",
					this._ThreadName,
					"->",
					ex.Message,
					"\n",
					ex.StackTrace
				}));
			}
		}

		private void clearWebField(IWebElement element)
		{
			while (!element.GetAttribute("value").Equals(""))
			{
				element.SendKeys(OpenQA.Selenium.Keys.Backspace);
			}
		}

		public async Task<bool> addBank()
		{
			bool res = false;
			await Task.Run(async delegate()
			{
				try
				{
					HttpClient _client = new HttpClient();
					string text = await _client.GetStringAsync("https://fake-it.ws/de/");
					string _rs = text;
					text = null;
					string _tmpName = Regex.Match(_rs, "row..Name[<][/]th[>].{10,200}[/]span", RegexOptions.Singleline).ToString();
					_tmpName = _tmpName.Replace("row\">Name</th>", "").Trim().Replace("<td class=\"copy\"><span data-toggle=\"tooltip\" data-placement=\"top\" title=\"Click To Copy\">", "").Replace("</span", "");
					this.log(_tmpName);
					string _address = Regex.Match(_rs, "row..Address[<][/]th[>].{10,200}[/]span", RegexOptions.Singleline).ToString();
					_address = _address.Replace("row\">Address</th>", "").Trim().Replace("<td class=\"copy\"><span data-toggle=\"tooltip\" data-placement=\"top\" title=\"Click To Copy\">", "").Replace("</span", "");
					this.log(_address);
					string _city = Regex.Match(_rs, "row..City[<][/]th[>].{10,200}[/]span", RegexOptions.Singleline).ToString();
					_city = _city.Replace("row\">City</th>", "").Trim().Replace("<td class=\"copy\"><span data-toggle=\"tooltip\" data-placement=\"top\" title=\"Click To Copy\">", "").Replace("</span", "");
					this.log(_city);
					string _postCode = Regex.Match(_rs, "row..Postcode[<][/]th[>].{10,200}[/]span", RegexOptions.Singleline).ToString();
					_postCode = _postCode.Replace("row\">Postcode</th>", "").Trim().Replace("<td class=\"copy\"><span data-toggle=\"tooltip\" data-placement=\"top\" title=\"Click To Copy\">", "").Replace("</span", "");
					this.log(_postCode);
					string _iban = Regex.Match(_rs, "id..iban.{10,200}[/]span", RegexOptions.Singleline).ToString();
					_iban = _iban.Replace("</span></span", "").Trim().Replace("id=\"iban\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"Click To Copy\">", "");
					this.log(_iban);
					string _BIC = Regex.Match(_rs, "row..BIC[<][/]th[>].{10,200}[/]span", RegexOptions.Singleline).ToString();
					_BIC = _BIC.Replace("row\">BIC</th>", "").Trim().Replace("<td class=\"copy\"><span data-toggle=\"tooltip\" data-placement=\"top\" title=\"Click To Copy\">", "").Replace("</span", "");
					this.log(_BIC);

					this._driver.Navigate().GoToUrl("https://www.facebook.com/ads/manager/account_settings/account_billing");
					Delay(5000);
					WaitLoading();

					IWebElement _btAddStart = this.getElement(By.CssSelector("button[type='button'][aria-disabled='false'][style*='background-color: rgb(24, 119, 242)']"));
					if (this.isValid(_btAddStart))
                    {
						_btAddStart.Click();
						Delay(1000);
					}
					WaitLoading();
					string addPaymentDivName = "//div[@role='button']/div/div/span[contains(@class, '66pz984')]";
					WaitAjaxLoading(By.XPath(addPaymentDivName), 300);
					Delay(1000);

					IWebElement addPamt = null;
					ReadOnlyCollection<IWebElement> addPaymentDev = this._driver.FindElements(By.XPath(addPaymentDivName));
					if (addPaymentDev.Count < 1)
					{
						this.log("Not Add Payment button!");
						return;
					}

					addPamt = addPaymentDev[0];
					if (!this.isValid(addPamt))
					{
						this.log("Add Payment Btn Valid!");
						return;
					}

					addPamt.Click();
					Delay(1000);
					WaitLoading();
					string addPaymentRadName = "//div[@role='button']/div/div/div/div/i[contains(@class, 'x_18fa0a')]";
					WaitAjaxLoading(By.XPath(addPaymentRadName), 30);
					Delay(1000);
					ReadOnlyCollection<IWebElement> _listRadio = this._driver.FindElements(By.XPath(addPaymentRadName));
					if (_listRadio.Count < 1)
                    {
						this.log("Not Banking Option!");
						return;
					}

					string btnNext1Name = "//div[@role='button' and contains(@class, 's1i5eluu')]/div/div/span[contains(@class, 'bwm1u5wc')]";
					WaitAjaxLoading(By.XPath(btnNext1Name));
					Delay(1000);
					ReadOnlyCollection<IWebElement> _listButtons = this._driver.FindElements(By.XPath(btnNext1Name));
					if (_listButtons.Count < 1)
                    {
						this.log("Not Banking Option Submit!");
						return;
					}

					IWebElement bankingopt = _listRadio[0].FindElement(By.XPath("..")).FindElement(By.XPath("..")).FindElement(By.XPath("..")).FindElement(By.XPath(".."));
					if (!this.isValid(bankingopt))
					{
						this.log("Delay Banking Option Valid...!");
						Delay(3000);
						if (!this.isValid(bankingopt))
						{
							this.log("Delay Banking Option Valid...!");
							Delay(3000);
							if (!this.isValid(bankingopt))
							{
								this.log("Delay Banking Option Valid...!");
								Delay(3000);
							}
						}
					}
					if (!this.isValid(bankingopt))
					{
						this.log("Banking Option Valid!");
						return;
					}
					bankingopt.Click();
					Delay(1000);
					_listButtons[0].FindElement(By.XPath("..")).FindElement(By.XPath("..")).FindElement(By.XPath("..")).Click();

					Delay(1000);
					WaitLoading();
					string inputTagName = "//input[@type='text']";
					WaitAjaxLoading(By.XPath(inputTagName));
					ReadOnlyCollection<IWebElement> inputs = this._driver.FindElements(By.XPath(inputTagName));
					if (inputs.Count > 5)
					{
						inputs[0].Click();
						inputs[0].SendKeys(_tmpName);
						Delay(500);
						inputs[1].Click();
						inputs[1].SendKeys(_iban);
						Delay(500);
						inputs[2].Click();
						inputs[2].SendKeys(_BIC);
						Delay(500);
						inputs[3].Click();
						inputs[3].SendKeys(_address);
						Delay(500);
						inputs[4].Click();
						inputs[4].SendKeys(_city);
						Delay(500);
						inputs[5].Click();
						inputs[5].SendKeys(_postCode);
						Delay(500);
						// checkbox
						string inputAccountName = "//input[@type='checkbox']";
						WaitAjaxLoading(By.XPath(inputAccountName));
						Delay(500);
						IWebElement input = this._driver.FindElement(By.XPath(inputAccountName));
						input.Click();
                    }
                    else
                    {
						this.log("Not Banking Form!");
						return;
					}

					string btnNext2Name = btnNext1Name;
					WaitAjaxLoading(By.XPath(btnNext2Name));
					Delay(500);
					_listButtons = this._driver.FindElements(By.XPath(btnNext2Name));
					if (_listButtons.Count < 1)
					{
						this.log("Not Banking Form Submit!");
						return;
					}

					if (!this.isValid(_listButtons[0]))
                    {
						this.log("Banking Form Submit Btn Valid!");
						return;
					}

					_listButtons[0].FindElement(By.XPath("..")).FindElement(By.XPath("..")).FindElement(By.XPath("..")).Click();

					// if hrsnp83r
					string errorAlert = "//div[contains(@class, 'hrsnp83r')]";
					WaitAjaxLoading(By.XPath(errorAlert), 5);
					ReadOnlyCollection<IWebElement> _listAlerts = this._driver.FindElements(By.XPath(errorAlert));
					if (_listAlerts.Count > 0)
					{
						inputs[5].Click();
						clearWebField(inputs[5]);
						inputs[5].SendKeys("1111");
						Delay(500);
						_listButtons[0].FindElement(By.XPath("..")).FindElement(By.XPath("..")).FindElement(By.XPath("..")).Click();
					}

					// Check complate
					string cplAlert = "//i[contains(@class, 'p_79DaOjpeIbN')]";
					WaitAjaxLoading(By.XPath(cplAlert), 20);
					ReadOnlyCollection<IWebElement> _cplAlert = this._driver.FindElements(By.XPath(cplAlert));
					Delay(1000);
					WaitLoading();
					this._driver.Navigate().Refresh();
					WaitLoading();
					Waitf2AjaxLoading(By.CssSelector("button[type='button'][aria-disabled='true']"), By.CssSelector("div[role='button'][aria-disabled='true']"), 60);
					Delay(1000);

					// check res
					string resCheck = "//div[contains(@class, 'o1teu6h')]/div/div/span[contains(@class, '66pz984')]";
					WaitAjaxLoading(By.XPath(resCheck), 8);
					ReadOnlyCollection<IWebElement> _btCheck = this._driver.FindElements(By.XPath(resCheck));
                    if (_btCheck.Count > 0)
                    {
						string bankIcon = "//i[contains(@class, 'x_18fa0a')]";
						WaitAjaxLoading(By.XPath(bankIcon));
						Delay(1000);
						ReadOnlyCollection<IWebElement> _bankIcon = this._driver.FindElements(By.XPath(bankIcon));
						if (_bankIcon.Count > 0)
						{
							this.log("BANK OK!");
							res = true;
							return;
						}
					}
					return;
				}
				catch (Exception ex2)
				{
					this.log(ex2);
					return;
				}
			});

			this.log("End banking!");
			return res;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00009690 File Offset: 0x00007890
		private IWebElement getElement(By by)
		{
			IWebElement result;
			try
			{
				int _count = 0;
				bool isValidData = false;
				IWebElement _ele = null;
				while (!isValidData)
				{
					_count++;
					bool flag = _count > 3;
					if (flag)
					{
						break;
					}
					_ele = this._driver.FindElement(by);
					isValidData = this.isValid(_ele);
					Thread.Sleep(1000);
				}
				result = _ele;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00009700 File Offset: 0x00007900
		private string genNumber(int length)
		{
			string _s = "";
			for (int i = 0; i < length; i++)
			{
				_s += new Random().Next(0, 9);
			}
			return _s;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00002358 File Offset: 0x00000558
		private void logs(string s)
		{
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00009744 File Offset: 0x00007944
		private IWebElement findById(IWebDriver _d, string id)
		{
			IWebElement result;
			try
			{
				result = _d.FindElement(By.Id(id));
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000028C0 File Offset: 0x00000AC0
		private void fillInput(IWebElement _ele, string s)
		{
			_ele.Click();
			_ele.SendKeys(OpenQA.Selenium.Keys.Control + "a");
			_ele.SendKeys("\b");
			_ele.SendKeys(s);
		}

		public void initChromePortable(string chrome)
		{
			try
			{
				bool flag = this._driver != null;
				if (flag)
				{
					this._driver.Quit();
				}

				string m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
				string folderPath = m_exePath + "\\via";
				string viaPath = folderPath + "\\" + chrome + "\\GoogleChromePortable";


				var options = new ChromeOptions();
				options.BinaryLocation = viaPath + "\\App\\Chrome-bin\\chrome.exe";
				StringBuilder builder = new StringBuilder(viaPath);
				builder.Replace("\\", "/");
				options.AddArgument("--user-data-dir=" + builder.ToString() + "/Data/profile");
				options.AddArgument("profile-directory=Default");
				// options.AddArgument("--no-sandbox");
				// options.AddArgument("--start-maximized");
				// options.AddArgument("--headless");

				var driverService = ChromeDriverService.CreateDefaultService();
				driverService.HideCommandPromptWindow = true;

				this._driver = new ChromeDriver(driverService, options, TimeSpan.FromMinutes(15.0));
				this._driver.Manage().Window.Size = new Size(600, 600);
			}
			catch
			{
				this.log("initChromePortable --- False!");
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0000977C File Offset: 0x0000797C
		private void initBrowserForCreateGmail()
		{
			bool flag = this._driver != null;
			if (flag)
			{
				this._driver.Quit();
			}
			ChromeOptions options = new ChromeOptions();
			options.AddArgument("--silent");
			options.AddArgument("--disable-infobars");
			options.AddArgument("disable-infobars");
			options.AddArgument("--disable-extensions");
			options.AddArgument("--incognito");
			options.AddArgument("--disable-plugins-discovery");
			options.BinaryLocation = Path.Combine("bin", "Application", "chrome.exe");
			options.AddArgument("user-agent=Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36");
			ChromeDriverService driverService = ChromeDriverService.CreateDefaultService("driver");
			driverService.HideCommandPromptWindow = true;
			this._driver = new ChromeDriver(driverService, options, TimeSpan.FromMinutes(3.0));
			this._driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 30);
			this._driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(180.0);
			this._driver.Manage().Window.Size = new Size(1280, 720);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000098B0 File Offset: 0x00007AB0
		private void initBrowserIphoneX()
		{
			bool flag = this._driver != null;
			if (flag)
			{
				// this._driver.Quit();
			}
			ChromeOptions options = new ChromeOptions();
			options.AddArgument("--silent");
			options.AddArgument("--disable-infobars");
			options.AddArgument("disable-infobars");
			options.AddArgument("--disable-extensions");
			options.AddArgument("--incognito");
			options.AddArgument("--disable-plugins-discovery");
			options.EnableMobileEmulation(new ChromeMobileEmulationDeviceSettings
			{
				Width = 375L,
				Height = 812L,
				PixelRatio = 3.0,
				UserAgent = "Mozilla/5.0 (iPhone; CPU iPhone OS 13_2_3 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/13.0.3 Mobile/15E148 Safari/604.1",
				EnableTouchEvents = true
			});
			options.BinaryLocation = Path.Combine("bin", "Application", "chrome.exe");
			options.AddArgument("user-agent=Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36");
			ChromeDriverService driverService = ChromeDriverService.CreateDefaultService("driver");
			driverService.HideCommandPromptWindow = true;
			this._driver = new ChromeDriver(driverService, options, TimeSpan.FromMinutes(3.0));
			this._driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 20);
			this._driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(120.0);
			this._driver.Manage().Window.Size = new Size(400, 900);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00009A30 File Offset: 0x00007C30
		private bool isValid(IWebElement _ele)
		{
			bool result;
			try
			{
				bool flag = _ele == null;
				if (flag)
				{
					result = false;
				}
				else
				{
					result = (_ele.Displayed && _ele.Enabled);
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00009A78 File Offset: 0x00007C78
		private IWebElement findElementByCode(string code, string text, bool hasPartOfText)
		{
			IWebElement result;
			try
			{
				ReadOnlyCollection<IWebElement> _eles = this._driver.FindElements(By.CssSelector(code));
				bool flag = _eles.Count >= 1 && text == null;
				if (flag)
				{
					result = _eles[0];
				}
				else
				{
					foreach (IWebElement _item in _eles)
					{
						if (hasPartOfText)
						{
							bool flag2 = _item.Text.Contains(text);
							if (flag2)
							{
								return _item;
							}
						}
						else
						{
							bool flag3 = _item.Text.Trim().Equals(text);
							if (flag3)
							{
								return _item;
							}
						}
					}
					result = null;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00009B4C File Offset: 0x00007D4C
		private string genPassword(int length)
		{
			string x = "abcdefghijklmnopqrstuvwxyz1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ!@#$%^&*()_";
			string _s = "";
			for (int i = 0; i < length - 1; i++)
			{
				Random _r = new Random(Guid.NewGuid().GetHashCode());
				_s += x[_r.Next(0, x.Length)].ToString();
			}
			return "Mango12345";
		}

		// Token: 0x04000165 RID: 357
		private string[] _listDomain = new string[]
		{
			"gddao.com",
			"blogemail.com",
			"huntarapp.com",
			"aircraftdictionary.com",
			"avissena.com",
			"xdatelocal.com",
			"emvil.com",
			"cent23.com",
			"walmartshops.com",
			"cokils.com",
			"hatberkshire.com",
			"avelani.com",
			"ch13sfv.com",
			"civoo.com",
			"blogtribe.com"
		};

		// Token: 0x04000166 RID: 358
		private IWebDriver _driver = null;
		private string regTabHandle = null;
		private string emailTabHandle = null;

		// Token: 0x02000026 RID: 38
		// (Invoke) Token: 0x060000CE RID: 206
		public delegate void LogDelegate(string s);

		// Token: 0x02000027 RID: 39
		public class TmpDisplay
		{
			// Token: 0x17000010 RID: 16
			// (get) Token: 0x060000D1 RID: 209 RVA: 0x00009C6A File Offset: 0x00007E6A
			// (set) Token: 0x060000D2 RID: 210 RVA: 0x00009C72 File Offset: 0x00007E72
			public string Name { get; set; }

			// Token: 0x17000011 RID: 17
			// (get) Token: 0x060000D3 RID: 211 RVA: 0x00009C7B File Offset: 0x00007E7B
			// (set) Token: 0x060000D4 RID: 212 RVA: 0x00009C83 File Offset: 0x00007E83
			public string Value { get; set; }
		}
	}
}
