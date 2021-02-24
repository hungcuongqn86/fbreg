using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

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

		private void QuitDriver(string chromept)
		{
			this._driver.Quit();
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00009278 File Offset: 0x00007478
		private async Task<bool> createPage(string _tmpCookie, string _dataAll)
		{
			bool _rsCreatePage = false;
			await Task.Run(delegate()
			{
				try
				{
					this._driver.Navigate().GoToUrl("https://m.facebook.com");
					WaitAjaxLoading(By.Id("nux-nav-button"), 3);
					Delay(1000);
					IWebElement _btskipPhone = this.getElement(By.Id("nux-nav-button"));
					bool flag4 = this.isValid(_btskipPhone);
					if (flag4)
					{
						_btskipPhone.Click();
						WaitAjaxLoading(By.Id("nux-nav-button"), 3);
						Delay(1000);
						_btskipPhone = this.getElement(By.Id("nux-nav-button"));
						bool flag5 = this.isValid(_btskipPhone);
						if (flag5)
						{
							_btskipPhone.Click();
							WaitAjaxLoading(By.Id("nux-nav-button"), 3);
							Delay(1000);
							_btskipPhone = this.getElement(By.Id("nux-nav-button"));
							bool flag6 = this.isValid(_btskipPhone);
							if (flag6)
							{
								_btskipPhone.Click();
							}
						}
					}

					WaitAjaxLoading(By.Id("bookmarks_jewel"), 8);
					Delay(1000);
					IWebElement _btBookmark = this.getElement(By.Id("bookmarks_jewel"));
					bool flag7 = this.isValid(_btBookmark);
					if (flag7)
					{
						_btBookmark.Click();

						WaitAjaxLoading(By.CssSelector("a[href*='nt_launchpoint_redesign']"), 8);
						Delay(1000);
						IWebElement _pageBt = this.getElement(By.CssSelector("a[href*='nt_launchpoint_redesign']"));
						bool flag8 = this.isValid(_pageBt);
						if (flag8)
						{
							_pageBt.Click();
							int _tmpCount = 0;
							bool _isClickBt = false;
							ReadOnlyCollection<IWebElement> _listBt;
							for (;;)
							{
								_tmpCount++;
								bool flag9 = _tmpCount > 5;
								if (flag9)
								{
									break;
								}
								WaitAjaxLoading(By.CssSelector("div[data-nt='NT:BUTTON_2']"), 10);
								Delay(1000);
								_listBt = this._driver.FindElements(By.CssSelector("div[data-nt='NT:BUTTON_2']"));
								bool flag10 = _listBt.Count > 0;
								if (flag10)
								{
									bool flag11 = this.isValid(_listBt[0]);
									if (flag11)
									{
										goto Block_11;
									}
								}
							}
							goto IL_326;
							Block_11:
							_listBt[0].Click();
							_isClickBt = true;
							IL_326:
							bool flag12 = _isClickBt;
							if (flag12)
							{
								WaitAjaxLoading(By.CssSelector("a[href*='/pages/creation_flow']"), 10);
								Delay(1000);
								IWebElement _btCreate = this.getElement(By.CssSelector("a[href*='/pages/creation_flow']"));
								bool flag13 = this.isValid(_btCreate);
								if (flag13)
								{
									_btCreate.Click();

									WaitAjaxLoading(By.CssSelector("input[data-sigil='page_creation_name_input']"), 10);
									Delay(1000);
									IWebElement _inputPageName = this.getElement(By.CssSelector("input[data-sigil='page_creation_name_input']"));
									bool flag14 = this.isValid(_inputPageName);
									if (flag14)
									{
										string _rNamePage = this._listKeyword[new Random(Guid.NewGuid().GetHashCode()).Next(0, this._listKeyword.Count)] + " " + this.randomNumber(2);
										this.fillInput(_inputPageName, _rNamePage);

										WaitAjaxLoading(By.CssSelector("button[type='submit']"), 3);
										Delay(500);
										IWebElement _submitBt = this.getElement(By.CssSelector("button[type='submit']"));
										bool flag15 = this.isValid(_submitBt);
										if (flag15)
										{
											_submitBt.Click();

											WaitAjaxLoading(By.CssSelector("select[name='super_category_selector']"), 10);
											Delay(1000);
											SelectElement _selectCategory = new SelectElement(this.getElement(By.CssSelector("select[name='super_category_selector']")));
											_selectCategory.SelectByIndex(new Random().Next(2, _selectCategory.Options.Count));
											_selectCategory = new SelectElement(this.getElement(By.CssSelector("select[name='sub_category_selector']")));
											_selectCategory.SelectByIndex(new Random().Next(2, _selectCategory.Options.Count));

											WaitAjaxLoading(By.CssSelector("button[type='submit']"), 3);
											Delay(500);
											_submitBt = this.getElement(By.CssSelector("button[type='submit']"));
											bool flag16 = this.isValid(_submitBt);
											if (flag16)
											{
												_submitBt.Click();

												WaitAjaxLoading(By.CssSelector("a[href*='/pages/creation_flow/?step=profile_pic']"), 20);
												Delay(1000);
												IWebElement _btNext = this.getElement(By.CssSelector("a[href*='/pages/creation_flow/?step=profile_pic']"));
												bool flag17 = this.isValid(_btNext);
												if (flag17)
												{
													_btNext.Click();
												}

												WaitAjaxLoading(By.CssSelector("a[href*='/pages/creation_flow/?step=cover_photo']"), 5);
												Delay(1000);
												_btNext = this.getElement(By.CssSelector("a[href*='/pages/creation_flow/?step=cover_photo']"));
												bool flag18 = this.isValid(_btNext);
												if (flag18)
												{
													_btNext.Click();
												}

												WaitAjaxLoading(By.CssSelector("button[type='submit']"), 8);
												Delay(1000);
												ReadOnlyCollection<IWebElement> _btSubmits = this._driver.FindElements(By.CssSelector("button[type='submit']"));
												bool flag19 = _btSubmits.Count > 1;
												if (flag19)
												{
													_btSubmits[1].Click();

													WaitAjaxLoading(By.CssSelector("a[href*='https://m.facebook.com/ads/create/choose_objective']"), 10);
													Delay(1000);
													IWebElement _btAds = this.getElement(By.CssSelector("a[href*='https://m.facebook.com/ads/create/choose_objective']"));
													bool flag20 = this.isValid(_btAds);
													if (flag20)
													{
														_btAds.Click();

														WaitAjaxLoading(By.CssSelector("a[href*='product=boosted_pagelike']"), 8);
														Delay(1000);
														IWebElement _pageLike = this.getElement(By.CssSelector("a[href*='product=boosted_pagelike']"));
														bool flag21 = _pageLike != null;
														if (flag21)
														{
															try
															{
																_pageLike.Click();
															}
															catch
															{
															}


															Thread.Sleep(3000);
															this._driver.Navigate().GoToUrl("https://m.facebook.com/certification/nondiscrimination/");

															WaitAjaxLoading(By.CssSelector("button[use='primary'][type='submit']"), 5);
															Delay(1000);
															IWebElement _btSubmit = this.getElement(By.CssSelector("button[use='primary'][type='submit']"));
															bool flag22 = this.isValid(_btSubmit);
															if (flag22)
															{
																_btSubmit.Click();
																Delay(1000);
																_btSubmit.Click();
																Delay(3000);
																_rsCreatePage = true;
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
				catch (Exception ex2)
				{
					this.log(ex2);
				}
			});
			return _rsCreatePage;
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



		// Token: 0x060000BD RID: 189 RVA: 0x000094F8 File Offset: 0x000076F8
		public async Task<int> regClone(string chrome)
		{
			int _status = 0;
			await Task.Run(async delegate()
			{
				try
				{
					this.log("--------------------------------");
					// this.initBrowserForCreateGmail();
					this.initChromePortable(chrome);
					this.log("Start load page");
					this._driver.Navigate().GoToUrl("https://facebook.com");
					Thread.Sleep(1000);
					bool flag = this._driver.PageSource.Contains("This site can’t be reached");
					if (flag)
					{
						_status = -2;
						this.log("Die Socks");
					}
					else
					{
						this.log("Find cookie banner");
						WaitAjaxLoading(By.CssSelector("button[data-testid='cookie-policy-banner-accept']"));
						IWebElement _acceptBt = this.getElement(By.CssSelector("button[data-testid='cookie-policy-banner-accept']"));
						bool flag2 = this.isValid(_acceptBt);
						if (flag2)
						{
							_acceptBt.Click();
						}
						this.log("Find Locate Link");
						WaitAjaxLoading(By.CssSelector("a[href*='facebook.com/']"));
						ReadOnlyCollection<IWebElement> _locateList = this._driver.FindElements(By.CssSelector("a[href*='facebook.com/']"));
						bool flag3 = _locateList.Count > 1;
						if (flag3)
						{
							string _url = _locateList[1].GetAttribute("href");
							this.log(_url);
							this._driver.Navigate().GoToUrl(_url);
							_url = null;
						}
						this.log("Find Register Button");
						WaitAjaxLoading(By.CssSelector("a[data-testid='open-registration-form-button']"));
						IWebElement _regBt = this.getElement(By.CssSelector("a[data-testid='open-registration-form-button']"));
						bool flag4 = this.isValid(_regBt);
						if (flag4)
						{
							_regBt.Click();
							
							this.log("Generate Infomation");
							HttpClient _client = new HttpClient();
							string text = await _client.GetStringAsync("https://fake-it.ws/de/");
							string _rs = text;
							text = null;
							string _tmpName = Regex.Match(_rs, "row..Name[<][/]th[>].{10,200}[/]span", RegexOptions.Singleline).ToString();
							_tmpName = _tmpName.Replace("row\">Name</th>", "").Trim().Replace("<td class=\"copy\"><span data-toggle=\"tooltip\" data-placement=\"top\" title=\"Click To Copy\">", "").Replace("</span", "");
							this.log("Name: " + _tmpName);
							string[] _tmpNameArr = _tmpName.Split(new char[]
							{
								' '
							});
							string _firstName = "";
							string _lastName = "";
							if (_tmpNameArr.Length > 1)
							{
								_firstName = _tmpNameArr[0];
								_lastName = _tmpNameArr[1];
							}
							else
							{
								_firstName = "Marion";
								_lastName = "Geyer";
							}
							string _randomEmail = this.randomGmail();
							string _passAcc = "F0KHFSa" + new Random(Guid.NewGuid().GetHashCode()).Next(10000, 99999);
							string _mailKhoiPhuc = this.randomEmail();
							string _tmpDataAll = string.Concat(new string[]
							{
								_randomEmail,
								"\t",
								_passAcc,
								"\t",
								_mailKhoiPhuc
							});
							this.log(_tmpDataAll);

							this.log("Fill Name");
							WaitAjaxLoading(By.CssSelector("input[name='firstname']"), 10);
							IWebElement _fName = this.getElement(By.CssSelector("input[name='firstname']"));
							if (this.isValid(_fName))
							{
								this.fillInput(_fName, _firstName);

								IWebElement _lName = this.getElement(By.CssSelector("input[name='lastname']"));
								if (this.isValid(_lName))
								{
									this.fillInput(_lName, _lastName);
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
								this.log("Choose gender");
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
								this.log("Find submit");
								IWebElement _submitBt = this.getElement(By.CssSelector("button[name='websubmit']"));
								if (this.isValid(_submitBt))
								{
									this.log("Click submit");
									_submitBt.Click();
									WaitAjaxLoading(By.CssSelector("a[href*='/confirm/resend_code']"), 15);

									if (!this._driver.Url.Contains("https://www.facebook.com/checkpoint"))
									{
										Delay(1000);
										IWebElement _btConfirmResend = this.getElement(By.CssSelector("a[href*='/confirm/resend_code']"));
										if (!this.isValid(_btConfirmResend))
										{
											_submitBt = this.getElement(By.CssSelector("button[name='websubmit']"));
											if (this.isValid(_submitBt))
                                            {
												Thread.Sleep(120000);
												_submitBt.Click();
												WaitAjaxLoading(By.CssSelector("a[href*='/confirm/resend_code']"), 10);
												Delay(1000);
												_btConfirmResend = this.getElement(By.CssSelector("a[href*='/confirm/resend_code']"));
											}
										}

										if (this.isValid(_btConfirmResend))
										{
											_btConfirmResend.Click();
											WaitAjaxLoading(By.CssSelector("a[href*='/change_contactpoint'][rel='dialog-post']"), 10);
											Delay(500);
											_btConfirmResend = this.getElement(By.CssSelector("a[href*='/change_contactpoint'][rel='dialog-post']"));
											if (this.isValid(_btConfirmResend))
											{
												_btConfirmResend.Click();
												this.log("Change new email");
												WaitAjaxLoading(By.CssSelector("input[name='contactpoint']"), 10);
												Delay(1000);
												IWebElement _inputNewEmail = this.getElement(By.CssSelector("input[name='contactpoint']"));
												if (this.isValid(_inputNewEmail))
												{
													this.fillInput(_inputNewEmail, _mailKhoiPhuc);
													this.log("Submit change Email");

													WaitAjaxLoading(By.XPath("//div[contains(@class, 'iOverlayFooter')]/button[@type='submit']"), 3);
													Delay(1000);
													ReadOnlyCollection<IWebElement> _btSubmitNewEmail = this._driver.FindElements(By.XPath("//div[contains(@class, 'iOverlayFooter')]/button[@type='submit']"));
													bool _isClickSubmit = false;
													if (_btSubmitNewEmail.Count > 0)
													{
														if (this.isValid(_btSubmitNewEmail[0]))
														{
															_btSubmitNewEmail[0].Click();
															_isClickSubmit = true;
														}
													}
													else
													{
														this.log("Not found submit new email");
													}
													if (_isClickSubmit)
													{
														Delay(5000);
														if (!this._driver.Url.Contains("https://www.facebook.com/checkpoint"))
														{
															string text2 = await this.getCode2(_mailKhoiPhuc.Replace("@fairocketsmail.com", ""));
                                                            if (string.IsNullOrEmpty(text2))
                                                            {
																// change mail
																this.log("Change new email lan 2-->");
																IWebElement _btConfirmResend1 = this.getElement(By.CssSelector("a[href*='/confirm/resend_code']"));
																if (this.isValid(_btConfirmResend1))
                                                                {
																	_btConfirmResend1.Click();
																	WaitAjaxLoading(By.CssSelector("a[href*='/change_contactpoint'][rel='dialog-post']"), 10);
																	_btConfirmResend1 = this.getElement(By.CssSelector("a[href*='/change_contactpoint'][rel='dialog-post']"));
																	if (this.isValid(_btConfirmResend1))
                                                                    {
																		_btConfirmResend1.Click();
																		this.log("Change new email");
																		WaitAjaxLoading(By.CssSelector("input[name='contactpoint']"), 10);
																		_inputNewEmail = this.getElement(By.CssSelector("input[name='contactpoint']"));
																		_mailKhoiPhuc = this.randomEmail();
																		_tmpDataAll = string.Concat(new string[]
																		{
																			_randomEmail,
																			"\t",
																			_passAcc,
																			"\t",
																			_mailKhoiPhuc
																		});
																		this.log(_tmpDataAll);
																		this.fillInput(_inputNewEmail, _mailKhoiPhuc);
																		
																		this.log("Submit change Email");
																		WaitAjaxLoading(By.XPath("//div[contains(@class, 'iOverlayFooter')]/button[@type='submit']"), 3);
																		Delay(1000);
																		_btSubmitNewEmail = this._driver.FindElements(By.XPath("//div[contains(@class, 'iOverlayFooter')]/button[@type='submit']"));
																		if (_btSubmitNewEmail.Count > 0)
																		{
																			if (this.isValid(_btSubmitNewEmail[0]))
																			{
																				_btSubmitNewEmail[0].Click();
																				Delay(5000);
																			}
																		}
																		else
																		{
																			this.log("Not found submit new email");
																		}

																		text2 = await this.getCode2(_mailKhoiPhuc.Replace("@fairocketsmail.com", ""));
																	}
																}
															}

															string _code = text2;
															text2 = null;
															if (!string.IsNullOrEmpty(_code))
															{
																this.log(_code);
																IWebElement _inputCode = this.getElement(By.CssSelector("input[name='code']"));
																if (this.isValid(_inputCode))
																{
																	this.fillInput(_inputCode, _code);
																	IWebElement _btConfirmCode = this.getElement(By.CssSelector("button[name='confirm']"));
																	if (this.isValid(_btConfirmCode))
																	{
																		_btConfirmCode.Click();
																		Thread.Sleep(10000);
																		if (!this._driver.Url.Contains("https://www.facebook.com/checkpoint"))
																		{
																			string _tmpCoookie = "";
																			foreach (OpenQA.Selenium.Cookie _c in this._driver.Manage().Cookies.AllCookies)
																			{
																				_tmpCoookie = string.Concat(new string[]
																				{
																					_tmpCoookie,
																					_c.Name,
																					"=",
																					_c.Value,
																					"; "
																				});
																				// _c = null;
																			}
																			IEnumerator<OpenQA.Selenium.Cookie> enumerator = null;
																			this.log(_tmpCoookie);
																			bool flag5 = await this.createPage(_tmpCoookie, _tmpDataAll);
																			bool _rsCreatePage = flag5;
																			string _tmpData = string.Concat(new string[]
																			{
																				_tmpDataAll,
																				"\t",
																				_tmpCoookie,
																				"\tPageCreate: ",
																				_rsCreatePage.ToString()
																			});
																			this.log("OK->\t" + _tmpData);
																			_status = 1;
																			for (;;)
																			{
																				try
																				{
																					File.AppendAllText("data_log.txt", _tmpData + "\n");
																					break;
																				}
																				catch
																				{
																				}
																				Thread.Sleep(500);
																			}
																			_tmpCoookie = null;
																			_tmpData = null;
																		}
																		else
																		{
																			_status = -1;
																			this.log("check point");
																			QuitDriver(chrome);
																		}
																	}
																	_btConfirmCode = null;
																}
																_inputCode = null;
															}
															else
															{
																this.log("Get Code Error");
																// QuitDriver(chrome);
															}
															_code = null;
														}
														else
														{
															_status = -1;
															this.log("check point");
															QuitDriver(chrome);
														}
													}
													_btSubmitNewEmail = null;
												}
												else
												{
													this.log("Not found new email");
												}
												_inputNewEmail = null;
											}
										}
										_btConfirmResend = null;
									}
									else
									{
										_status = -1;
										this.log("check point");
										QuitDriver(chrome);
									}
								}
								_lName = null;
								_email = null;
								_email2 = null;
								_pass = null;
								_selectDay = null;
								_random = null;
								_selectMonth = null;
								_selectYear = null;
								_gender = null;
								_submitBt = null;
							}
							_client = null;
							_rs = null;
							_tmpName = null;
							_tmpNameArr = null;
							_firstName = null;
							_lastName = null;
							_randomEmail = null;
							_passAcc = null;
							_mailKhoiPhuc = null;
							_tmpDataAll = null;
							_fName = null;
						}
						_acceptBt = null;
						_locateList = null;
						_regBt = null;
					}
				}
				catch (Exception ex)
				{
					this.log(ex);
				}
				this.log("End");
				/*try
				{
					if (this._closeChrome && this._driver != null)
					{
						this._driver.Quit();
					}
				}
				catch
				{
				}*/
			});
			return _status;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00009540 File Offset: 0x00007740
		private async Task<string> getCode2(string _email)
		{
			try
			{
				ServicePointManager.Expect100Continue = true;
				ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
				int _count = 0;
				string _code = "";
				string[] _tmpEmail = _email.Split(new char[]
				{
					'@'
				});
				string _tmpCode;
				for (;;)
				{
					await Task.Delay(10000);
					this.log("Start retry code " + _count);
					HttpClient _client = new HttpClient(new HttpClientHandler
					{
						UseCookies = false
					});
					_client.DefaultRequestHeaders.Add("Cookie", "_ga=GA1.2.61770657.1607270767; _gid=GA1.2.1123466691.1607270767; __gads=ID=a94e27823af01929-224a441f08c50006:T=1607270768:RT=1607270768:S=ALNI_Mbp8D2iSYvRQYtco_GgwN6Nq2Askg; embx=%5B%22tec313213afdsafsa%40fairocketsmail.com%22%2C%22tecopa4324321%40fairocketsmail.com%22%5D; _gat=1; surl=" + _tmpEmail[1] + "/" + _tmpEmail[0]);
					string text = await _client.GetStringAsync("https://vi.emailfake.com/channel5/");
					string _rs = text;
					text = null;
					_tmpCode = Regex.Match(_rs, "FB[-][0-9]+").ToString();
					if (!string.IsNullOrEmpty(_tmpCode))
					{
						break;
					}
					_count++;
					if (_count >= 5)
					{
						goto Block_3;
					}
					_client = null;
					_rs = null;
					_tmpCode = null;
				}
				_code = _tmpCode.Replace("FB-", "");
				Block_3:
				this.log("Code: " + _code);
				return _code;
			}
			catch (Exception ex)
			{
				this.log(ex);
			}
			return null;
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

		// Token: 0x060000C1 RID: 193 RVA: 0x00009638 File Offset: 0x00007838
		private async Task addBank(string _tmpCookie, string _dataAll)
		{
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
					this.initBrowserForCreateGmail();
					this._driver.Navigate().GoToUrl("https://www.facebook.com");
					string[] _listCookie = _tmpCookie.Split(new char[]
					{
						';'
					});
					this._driver.Manage().Cookies.DeleteAllCookies();
					foreach (string _c in _listCookie)
					{
						try
						{
							string[] _x = _c.Trim().Split(new char[]
							{
								'='
							});
							if (_x.Length > 1)
							{
								if (_x.Length == 3)
								{
									_x[1] = _x[1] + "=" + _x[2];
								}
								if (!string.IsNullOrEmpty(_x[0].Trim()))
								{
									this._driver.Manage().Cookies.AddCookie(new OpenQA.Selenium.Cookie(_x[0], _x[1], ".facebook.com", "/", new DateTime?(DateTime.Now.AddDays(400.0))));
								}
							}
							_x = null;
						}
						catch (Exception ex)
						{
							this.log(ex);
						}
						// _c = null;
					}
					string[] array = null;
					this._driver.Navigate().GoToUrl("https://www.facebook.com/ads/manager/account_settings/account_billing");
					Thread.Sleep(5000);
					IWebElement _btAddBank = this.getElement(By.CssSelector("button[type='button'][aria-disabled='false'][style*='background-color: rgba(0, 0, 0, 0.05)']"));
					if (this.isValid(_btAddBank))
					{
						_btAddBank.Click();
						Thread.Sleep(15000);
						ReadOnlyCollection<IWebElement> _listRadio = this._driver.FindElements(By.CssSelector("div[role='radio']"));
						if (_listRadio.Count > 2)
						{
							_listRadio[2].Click();
							Thread.Sleep(1000);
							ReadOnlyCollection<IWebElement> _listButtons = this._driver.FindElements(By.CssSelector("div[role='button'][tabindex='0']"));
							if (_listButtons.Count > 1)
							{
								if (this.isValid(_listButtons[_listButtons.Count - 2]))
								{
									_listButtons[_listButtons.Count - 2].Click();
									Thread.Sleep(3000);
									ReadOnlyCollection<IWebElement> _listInputs = this._driver.FindElements(By.CssSelector("input[type='text'][dir='auto']"));
									this.fillInput(_listInputs[0], _tmpName);
									Thread.Sleep(2000);
									this.fillInput(_listInputs[1], _iban);
									Thread.Sleep(2000);
									this.fillInput(_listInputs[2], _BIC);
									Thread.Sleep(2000);
									this.fillInput(_listInputs[3], _address);
									Thread.Sleep(2000);
									this.fillInput(_listInputs[4], _city);
									Thread.Sleep(2000);
									this.fillInput(_listInputs[5], _postCode);
									Thread.Sleep(2000);
									IWebElement _checkTerm = this.getElement(By.CssSelector("input[type='checkbox']"));
									if (_checkTerm != null)
									{
										_checkTerm.Click();
									}
									_listButtons = this._driver.FindElements(By.CssSelector("div[role='button'][tabindex='0']"));
									if (_listButtons.Count > 1)
									{
										if (this.isValid(_listButtons[_listButtons.Count - 2]))
										{
											_listButtons[_listButtons.Count - 2].Click();
											Thread.Sleep(10000);
											_listButtons = this._driver.FindElements(By.CssSelector("div[role='button'][tabindex='0']"));
											if (_listButtons.Count > 0)
											{
												if (this.isValid(_listButtons[_listButtons.Count - 1]))
												{
													_listButtons[_listButtons.Count - 1].Click();
													Thread.Sleep(5000);
													this._driver.Navigate().Refresh();
													Thread.Sleep(5000);
													IWebElement _btCheck = this.getElement(By.CssSelector("button[type='button'][aria-disabled='true']"));
													if (_btCheck != null)
													{
														File.WriteAllText("bank_fail.txt", _dataAll + "\n");
														this.log("NOT OK");
													}
													else
													{
														File.WriteAllText("bank_ok.txt", _dataAll + "\n");
														this.log("OK");
													}
													_btCheck = null;
												}
											}
										}
									}
									_listInputs = null;
									_checkTerm = null;
								}
							}
							_listButtons = null;
						}
						_listRadio = null;
					}
					_client = null;
					_rs = null;
					_tmpName = null;
					_address = null;
					_city = null;
					_postCode = null;
					_iban = null;
					_BIC = null;
					_listCookie = null;
					_btAddBank = null;
				}
				catch (Exception ex2)
				{
					this.log(ex2);
				}
			});
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
			_ele.SendKeys(Keys.Control + "a");
			_ele.SendKeys("\b");
			_ele.SendKeys(s);
		}

		private void initChromePortable(string chrome)
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

			this._driver = new ChromeDriver(driverService, options, TimeSpan.FromMinutes(3.0));
			this._driver.Manage().Window.Size = new Size(1486, 800);
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
