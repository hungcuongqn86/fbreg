using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Keys = OpenQA.Selenium.Keys;
using System.IO.Compression;

namespace GenCode
{
	// Token: 0x02000003 RID: 3
	public partial class ManageChannel : Form
	{
		// Token: 0x06000008 RID: 8
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

		// Token: 0x06000009 RID: 9 RVA: 0x00002194 File Offset: 0x00000394
		public ManageChannel()
		{
			this.InitializeComponent();
			string _json = File.ReadAllText("keyword.json");
			this._listKeyword = JsonConvert.DeserializeObject<List<string>>(_json);
			this.SubscribeGlobal();
			base.FormClosing += new FormClosingEventHandler(this.Main_Closing);
			bool flag = !File.Exists(this.firefoxPath);
			if (flag)
			{
				this.firefoxPath = "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe";
			}
			this.getIP();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000227C File Offset: 0x0000047C
		public async void getIP()
		{
			try
			{
				HttpClient _client = new HttpClient();
				string text = await _client.GetStringAsync("https://whoer.net/");
				string _rs = text;
				text = null;
				Regex ip = new Regex("\\b\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\b");
				MatchCollection result = ip.Matches(_rs);
				this.IP = ((!result[0].ToString().Trim().Equals("")) ? result[0].ToString().Trim() : this.IP);
				_client = null;
				_rs = null;
				ip = null;
				result = null;
			}
			catch (Exception ex)
			{
				this.log(ex);
			}
			this.log("Your IP: " + this.IP);
			this._folderPath = Path.Combine("DATA", "Dropbox", "change", this.IP + "_" + DateTime.Now.ToString("yyyy-dd-MM HH-mm-ss"));
			if (!Directory.Exists(this._folderPath))
			{
				Directory.CreateDirectory(this._folderPath);
				this.log("Created " + this._folderPath);
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000022B8 File Offset: 0x000004B8
		private void Main_Closing(object sender, CancelEventArgs e)
		{
			this.Unsubscribe();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000022C2 File Offset: 0x000004C2
		private void SubscribeApplication()
		{
			this.Unsubscribe();
			this.Subscribe(Hook.AppEvents());
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000022D8 File Offset: 0x000004D8
		private void SubscribeGlobal()
		{
			this.Unsubscribe();
			this.Subscribe(Hook.GlobalEvents());
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022EE File Offset: 0x000004EE
		private void Subscribe(IKeyboardMouseEvents events)
		{
			this.m_Events = events;
			this.m_Events.KeyUp += this.OnKeyUp;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002310 File Offset: 0x00000510
		private void Unsubscribe()
		{
			bool flag = this.m_Events == null;
			if (!flag)
			{
				this.m_Events.KeyUp -= this.OnKeyUp;
				this.m_Events.Dispose();
				this.m_Events = null;
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002358 File Offset: 0x00000558
		private void OnKeyUp(object sender, KeyEventArgs e)
		{
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000235C File Offset: 0x0000055C
		private void lbData_SelectedIndexChanged(object sender, EventArgs e)
		{
			ManageChannel.TmpDisplay _item = ((ListBox)sender).SelectedItem as ManageChannel.TmpDisplay;
			bool flag = _item != null;
			if (flag)
			{
				Console.WriteLine(_item.Value);
				this.currentValue = _item.Value;
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000239D File Offset: 0x0000059D
		private void appendLn(RichTextBox tb, string text)
		{
			tb.AppendText(text + "\n");
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000023B4 File Offset: 0x000005B4
		private string randomNumber(int length)
		{
			string s = "";
			for (int i = 0; i < length; i++)
			{
				s += new Random(Guid.NewGuid().GetHashCode()).Next(0, 10).ToString();
			}
			return s;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002414 File Offset: 0x00000614
		private string randomProjectId()
		{
			string _id = string.Concat(new string[]
			{
				this._listKeyword[new Random(Guid.NewGuid().GetHashCode()).Next(0, this._listKeyword.Count)],
				"-",
				this._listKeyword[new Random(Guid.NewGuid().GetHashCode()).Next(0, this._listKeyword.Count)],
				"-",
				this.randomNumber(6)
			});
			return _id.Replace(" ", "");
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000024CC File Offset: 0x000006CC
		private string randomMailKhoiPhuc()
		{
			string _id = this._listKeyword[new Random(Guid.NewGuid().GetHashCode()).Next(0, this._listKeyword.Count)] + this._listKeyword[new Random(Guid.NewGuid().GetHashCode()).Next(0, this._listKeyword.Count)] + this.randomNumber(5);
			_id = _id.Replace(" ", "");
			return _id + "@yahoo.com";
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002574 File Offset: 0x00000774
		private string randomGmail()
		{
			string _id = this._listKeyword[new Random(Guid.NewGuid().GetHashCode()).Next(0, this._listKeyword.Count)] + this._listKeyword[new Random(Guid.NewGuid().GetHashCode()).Next(0, this._listKeyword.Count)] + this.randomNumber(5);
			_id = _id.Replace(" ", "");
			return _id + "@gmail.com";
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000261C File Offset: 0x0000081C
		private string randomEmail()
		{
			string _id = this._listKeyword[new Random(Guid.NewGuid().GetHashCode()).Next(0, this._listKeyword.Count)] + this._listKeyword[new Random(Guid.NewGuid().GetHashCode()).Next(0, this._listKeyword.Count)] + this.randomNumber(5);
			_id = _id.Replace(" ", "");
			return _id + "@fairocketsmail.com";
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000026C4 File Offset: 0x000008C4
		private void log(string s)
		{
			TextBoxUtils.Instance.AppendRichTextLn(this, this.tbInfo, s);
			bool flag = Directory.Exists(this._folderPath);
			if (flag)
			{
				for (;;)
				{
					try
					{
						File.AppendAllText(Path.Combine(this._folderPath, "log.txt"), s + "\n");
						break;
					}
					catch
					{
					}
					Thread.Sleep(500);
				}
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002744 File Offset: 0x00000944
		private void log(Exception ex)
		{
			TextBoxUtils.Instance.AppendRichTextLn(this, this.tbInfo, ex.Message + "\n" + ex.StackTrace);
			bool flag = Directory.Exists(this._folderPath);
			if (flag)
			{
				File.AppendAllText(Path.Combine(this._folderPath, "log.txt"), ex.Message + "\n" + ex.StackTrace + "\n");
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000027BC File Offset: 0x000009BC
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

		// Token: 0x0600001B RID: 27 RVA: 0x0000282C File Offset: 0x00000A2C
		private string genNumber(int length)
		{
			string _s = "";
			for (int i = 0; i < length; i++)
			{
				_s += new Random().Next(0, 9);
			}
			return _s;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002870 File Offset: 0x00000A70
		private void logs(string s)
		{
			TextBoxUtils.Instance.AppendRichTextLn(this, this.tbInfo, s);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002888 File Offset: 0x00000A88
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

		// Token: 0x0600001E RID: 30 RVA: 0x000028C0 File Offset: 0x00000AC0
		private void fillInput(IWebElement _ele, string s)
		{
			_ele.Click();
			_ele.SendKeys(OpenQA.Selenium.Keys.Control + "a");
			_ele.SendKeys("\b");
			_ele.SendKeys(s);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000028F4 File Offset: 0x00000AF4
		private void initBrowserForCreateGmail()
		{
			bool flag = this._driver != null;
			if (flag)
			{
				this._driver.Quit();
			}
			bool _checkChromeDrive = this.checkAndKillChromeDriverProcess();
			while (!_checkChromeDrive)
			{
				_checkChromeDrive = this.checkAndKillChromeDriverProcess();
				Thread.Sleep(1000);
			}
			bool _checkChrome = this.checkAndKillChromeProcess();
			while (!_checkChrome)
			{
				_checkChrome = this.checkAndKillChromeProcess();
				Thread.Sleep(1000);
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

		// Token: 0x06000020 RID: 32 RVA: 0x00002A78 File Offset: 0x00000C78
		private void initBrowserIphoneX()
		{
			bool flag = this._driver != null;
			if (flag)
			{
				this._driver.Quit();
			}
			bool _checkChromeDrive = this.checkAndKillChromeDriverProcess();
			while (!_checkChromeDrive)
			{
				_checkChromeDrive = this.checkAndKillChromeDriverProcess();
				Thread.Sleep(1000);
			}
			bool _checkChrome = this.checkAndKillChromeProcess();
			while (!_checkChrome)
			{
				_checkChrome = this.checkAndKillChromeProcess();
				Thread.Sleep(1000);
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
			driverService.Port = 2255;
			this._driver = new ChromeDriver(driverService, options);
			this._driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 20);
			this._driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60.0);
			this._driver.Manage().Window.Size = new Size(400, 900);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002C4C File Offset: 0x00000E4C
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

		// Token: 0x06000022 RID: 34 RVA: 0x00002C94 File Offset: 0x00000E94
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

		// Token: 0x06000023 RID: 35 RVA: 0x00002D68 File Offset: 0x00000F68
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

		// Token: 0x06000024 RID: 36 RVA: 0x00002DE0 File Offset: 0x00000FE0
		public string ToString(string text)
		{
			byte[] key = new byte[]
			{
				36,
				4,
				247,
				192,
				185,
				124,
				51,
				249,
				23,
				87,
				177,
				137,
				236,
				22,
				169,
				129,
				50,
				234,
				67,
				180,
				249,
				178,
				12,
				16,
				33,
				237,
				193,
				23,
				83,
				49,
				18,
				83
			};
			byte[] _byte = ManageChannel.hihi(text, key);
			return Convert.ToBase64String(_byte);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002E14 File Offset: 0x00001014
		private static byte[] hihi(string plainText, byte[] Key)
		{
			byte[] IV;
			byte[] encrypted;
			using (Aes aesAlg = Aes.Create())
			{
				aesAlg.Key = Key;
				aesAlg.GenerateIV();
				IV = aesAlg.IV;
				aesAlg.Mode = CipherMode.CBC;
				ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
				using (MemoryStream msEncrypt = new MemoryStream())
				{
					using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
					{
						using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
						{
							swEncrypt.Write(plainText);
						}
						encrypted = msEncrypt.ToArray();
					}
				}
			}
			byte[] combinedIvCt = new byte[IV.Length + encrypted.Length];
			Array.Copy(IV, 0, combinedIvCt, 0, IV.Length);
			Array.Copy(encrypted, 0, combinedIvCt, IV.Length, encrypted.Length);
			return combinedIvCt;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002F30 File Offset: 0x00001130
		private static string DecryptStringFromBytes_Aes(byte[] cipherTextCombined, byte[] Key)
		{
			string plaintext = null;
			using (Aes aesAlg = Aes.Create())
			{
				aesAlg.Key = Key;
				byte[] IV = new byte[aesAlg.BlockSize / 8];
				byte[] cipherText = new byte[cipherTextCombined.Length - IV.Length];
				Array.Copy(cipherTextCombined, IV, IV.Length);
				Array.Copy(cipherTextCombined, IV.Length, cipherText, 0, cipherText.Length);
				aesAlg.IV = IV;
				aesAlg.Mode = CipherMode.CBC;
				ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
				using (MemoryStream msDecrypt = new MemoryStream(cipherText))
				{
					using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
					{
						using (StreamReader srDecrypt = new StreamReader(csDecrypt))
						{
							plaintext = srDecrypt.ReadToEnd();
						}
					}
				}
			}
			return plaintext;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000304C File Offset: 0x0000124C
		private void checkAndStartDropbox()
		{
			Process[] pname = Process.GetProcessesByName("Dropbox");
			bool flag = pname.Length == 0;
			if (flag)
			{
				string _path = "C:\\Program Files (x86)\\Dropbox\\Client\\Dropbox.exe";
				bool flag2 = File.Exists(_path);
				if (flag2)
				{
					Process.Start(_path);
				}
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000308C File Offset: 0x0000128C
		private bool checkAndKillChromeProcess()
		{
			Process[] pname = Process.GetProcessesByName("chrome");
			bool flag = pname.Length != 0;
			bool result;
			if (flag)
			{
				for (int i = 0; i < pname.Length; i++)
				{
					try
					{
						pname[i].CloseMainWindow();
						pname[i].Kill();
						pname[i].WaitForExit();
					}
					catch
					{
					}
				}
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003104 File Offset: 0x00001304
		private bool checkAndKillChromeDriverProcess()
		{
			Process[] pname = Process.GetProcessesByName("chromedriver");
			bool flag = pname.Length != 0;
			bool result;
			if (flag)
			{
				for (int i = 0; i < pname.Length; i++)
				{
					try
					{
						pname[i].CloseMainWindow();
						pname[i].Kill();
						pname[i].WaitForExit();
					}
					catch
					{
					}
				}
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000317C File Offset: 0x0000137C
		private bool checkVPN()
		{
			bool isNetworkAvailable = NetworkInterface.GetIsNetworkAvailable();
			if (isNetworkAvailable)
			{
				NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
				foreach (NetworkInterface Interface in interfaces)
				{
					bool flag = Interface.OperationalStatus == OperationalStatus.Up;
					if (flag)
					{
						bool flag2 = Interface.NetworkInterfaceType == NetworkInterfaceType.Ppp && Interface.NetworkInterfaceType != NetworkInterfaceType.Loopback;
						if (flag2)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000031F4 File Offset: 0x000013F4
		private string decode(string _s)
		{
			string result;
			try
			{
				byte[] key = new byte[]
				{
					36,
					4,
					247,
					192,
					185,
					124,
					51,
					249,
					23,
					87,
					177,
					137,
					236,
					22,
					169,
					129,
					50,
					234,
					67,
					180,
					249,
					178,
					12,
					16,
					33,
					237,
					193,
					23,
					83,
					49,
					18,
					83
				};
				byte[] _decode = Convert.FromBase64String(_s);
				string _rs = ManageChannel.DecryptStringFromBytes_Aes(_decode, key);
				result = _rs;
			}
			catch
			{
				result = "null";
			}
			return result;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003244 File Offset: 0x00001444
		public async Task<string> getIP2()
		{
			try
			{
				HttpClient _client = new HttpClient();
				string text = await _client.GetStringAsync("https://whoer.net/");
				string _rs = text;
				text = null;
				Regex ip = new Regex("\\b\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\b");
				MatchCollection result = ip.Matches(_rs);
				return (!result[0].ToString().Trim().Equals("")) ? result[0].ToString().Trim() : "127.0.0.1";
			}
			catch (Exception ex)
			{
			}
			return null;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000328C File Offset: 0x0000148C
		private void pasteInput(IWebElement _ele, string s)
		{
			bool flag = this.isValid(_ele);
			if (flag)
			{
				_ele.Click();
				_ele.SendKeys(Keys.Control + "a");
				_ele.SendKeys("\b");
				_ele.SendKeys(s);
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002358 File Offset: 0x00000558
		private void ManageChannel_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000032D8 File Offset: 0x000014D8
		private void btOpenDirect_Click(object sender, EventArgs e)
		{
			Process.Start(Directory.GetCurrentDirectory());
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000032E6 File Offset: 0x000014E6
		private void btOpenCurrentDir2_Click(object sender, EventArgs e)
		{
			Process.Start(this._folderPath);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000032F8 File Offset: 0x000014F8
		private async Task<int> regClone()
		{
			int _status = 0;
			await Task.Run(async delegate()
			{
				try
				{
					this.log("--------------------------------");
					this.initBrowserForCreateGmail();
					this.log("Start load page");
					this._driver.Navigate().GoToUrl("https://facebook.com");
					Thread.Sleep(2000);
					this.log("Find cookie banner");
					IWebElement _acceptBt = this.getElement(By.CssSelector("button[data-testid='cookie-policy-banner-accept']"));
					bool flag = this.isValid(_acceptBt);
					if (flag)
					{
						_acceptBt.Click();
						Thread.Sleep(2000);
					}
					this.log("Find Locate Link");
					ReadOnlyCollection<IWebElement> _locateList = this._driver.FindElements(By.CssSelector("a[href*='facebook.com/']"));
					bool flag2 = _locateList.Count > 1;
					if (flag2)
					{
						string _url = _locateList[1].GetAttribute("href");
						this.log(_url);
						this._driver.Navigate().GoToUrl(_url);
						Thread.Sleep(2000);
						_url = null;
					}
					this.log("Find Register Button");
					IWebElement _regBt = this.getElement(By.CssSelector("a[data-testid='open-registration-form-button']"));
					bool flag3 = this.isValid(_regBt);
					if (flag3)
					{
						_regBt.Click();
						Thread.Sleep(2000);
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
						IWebElement _lName = this.getElement(By.CssSelector("input[name='lastname']"));
						if (this.isValid(_lName))
						{
							this.fillInput(_lName, _lastName);
							IWebElement _fName = this.getElement(By.CssSelector("input[name='firstname']"));
							if (this.isValid(_fName))
							{
								this.fillInput(_fName, _firstName);
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
							IWebElement _submitBt = this.getElement(By.CssSelector("button[name='websubmit']"));
							if (this.isValid(_submitBt))
							{
								_submitBt.Click();
								Thread.Sleep(20000);
								if (!this._driver.Url.Contains("https://www.facebook.com/checkpoint"))
								{
									IWebElement _btConfirmResend = this.getElement(By.CssSelector("a[href*='/confirm/resend_code']"));
									if (this.isValid(_btConfirmResend))
									{
										_btConfirmResend.Click();
										Thread.Sleep(2000);
										_btConfirmResend = this.getElement(By.CssSelector("a[href*='/change_contactpoint'][rel='dialog-post']"));
										if (this.isValid(_btConfirmResend))
										{
											_btConfirmResend.Click();
											Thread.Sleep(2000);
											IWebElement _inputNewEmail = this.getElement(By.CssSelector("input[name='contactpoint']"));
											if (this.isValid(_inputNewEmail))
											{
												this.fillInput(_inputNewEmail, _mailKhoiPhuc);
												Thread.Sleep(3000);
												ReadOnlyCollection<IWebElement> _btSubmitNewEmail = this._driver.FindElements(By.CssSelector("button[type='submit']"));
												bool _isClickSubmit = false;
												if (_btSubmitNewEmail.Count > 0)
												{
													if (this.isValid(_btSubmitNewEmail[_btSubmitNewEmail.Count - 1]))
													{
														_btSubmitNewEmail[_btSubmitNewEmail.Count - 1].Click();
														_isClickSubmit = true;
														Thread.Sleep(5000);
													}
												}
												if (_isClickSubmit)
												{
													if (!this._driver.Url.Contains("https://www.facebook.com/checkpoint"))
													{
														string text2 = await this.getCode2(_mailKhoiPhuc.Replace("@fairocketsmail.com", ""));
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
																		bool flag4 = await this.createPage(_tmpCoookie, _tmpDataAll);
																		bool _rsCreatePage = flag4;
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
																		File.AppendAllText("data_log.txt", _tmpData + "\n");
																		_tmpCoookie = null;
																		_tmpData = null;
																	}
																	else
																	{
																		_status = -1;
																		this.log("check point");
																	}
																}
																_btConfirmCode = null;
															}
															_inputCode = null;
														}
														else
														{
															this.log("Get Code Error");
														}
														_code = null;
													}
													else
													{
														_status = -1;
														this.log("check point");
													}
												}
												_btSubmitNewEmail = null;
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
								}
							}
							_fName = null;
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
						_lName = null;
					}
					_acceptBt = null;
					_locateList = null;
					_regBt = null;
				}
				catch (Exception ex)
				{
					this.log(ex);
				}
			});
			return _status;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00003340 File Offset: 0x00001540
		private void killChrome()
		{
			bool _checkChromeDrive = this.checkAndKillChromeDriverProcess();
			while (!_checkChromeDrive)
			{
				_checkChromeDrive = this.checkAndKillChromeDriverProcess();
				Thread.Sleep(1000);
			}
			bool _checkChrome = this.checkAndKillChromeProcess();
			while (!_checkChrome)
			{
				_checkChrome = this.checkAndKillChromeProcess();
				Thread.Sleep(1000);
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003398 File Offset: 0x00001598
		private void LogDelegate(string _s)
		{
			this.log(_s);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000033A4 File Offset: 0x000015A4
		private void button3_Click(object sender, EventArgs e)
		{
			// this.killChrome();
			int _numberThread = (int)this.tbNumberThread.Value;
			for (int i = 0; i < _numberThread; i++)
			{
				string _threadName = i.ToString().Clone().ToString();
				new Thread(async delegate()
				{
					RegClone _reg = new RegClone();
					_reg._listKeyword = this._listKeyword;
					_reg._ThreadName = (_threadName ?? "");
					_reg._closeChrome = this._closeChrome;
					RegClone regClone = _reg;
					regClone._logDelegate = (RegClone.LogDelegate)Delegate.Combine(regClone._logDelegate, new RegClone.LogDelegate(this.LogDelegate));
					string chrome = DateTime.Now.ToString("MM_dd_yyyy_HH_mm_ss") + "_" + _reg._ThreadName;
					initBrowse(chrome);
					await _reg.regClone(chrome);
				}).Start();
			}
			// this.log("All Done!");
		}

		private void initBrowse(string viaName)
		{
			try
			{
				string m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
				string folderPath = m_exePath + "\\via";
				bool exists = System.IO.Directory.Exists(folderPath);
				if (!exists)
					System.IO.Directory.CreateDirectory(folderPath);

				string viaPath = folderPath + "\\" + viaName;
				// this.autoRun = new AutoRun(viaPath + "\\GoogleChromePortable");
				// this.autoRun.viaAccess(viaArr);

				bool viaExists = System.IO.Directory.Exists(viaPath);
				if (!viaExists)
				{
					string portablePath = m_exePath + "\\portable\\GoogleChromePortable.zip";
					bool portableExists = File.Exists(portablePath);
					if (!portableExists)
					{
						MessageBox.Show("Chưa có bộ cài portable, liên hệ cuongnh!", "Thông báo!", MessageBoxButtons.OK);
						return;
					}
					// Create via portable
					System.IO.Directory.CreateDirectory(viaPath);
					ZipFile.ExtractToDirectory(portablePath, viaPath);
				}
				else
				{
					MessageBox.Show("Via đã tồn tại!", "Thông báo!", MessageBoxButtons.OK);
					return;
				}
			}
			catch (Exception error)
			{
				MessageBox.Show(error.Message.ToString());
				MessageBox.Show("Add via không thành công!", "Thông báo!", MessageBoxButtons.OK);
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003424 File Offset: 0x00001624
		private async Task<string> getCode(string _email)
		{
			try
			{
				ServicePointManager.Expect100Continue = true;
				ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
				int _count = 0;
				string _code = "";
				string _tmpCode;
				for (;;)
				{
					await Task.Delay(20000);
					this.log("Start retry code " + _count);
					HttpClient _client = new HttpClient();
					await _client.GetStringAsync("https://mailgen.biz/mailbox/" + _email);
					string _tmpInfo = "{\"key\":\"344cba3d678ddf5c1d2c517304b5734bcaabd840\",\"sid\":null,\"vid\":null,\"domain\":\"mailgen.biz\",\"pageUrl\":\"https://mailgen.biz/mailbox/" + _email + "\",\"referer\":\"\",\"screenWidth\":1920,\"screenHeight\":344,\"internalProps\":{\"version\":\"396de4c1f2\"}}";
					_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					HttpResponseMessage httpResponseMessage = await _client.PostAsync("https://manager.eu.smartlook.cloud/rec/check", new StringContent(_tmpInfo, Encoding.UTF8, "application/json"));
					string text = await _client.GetStringAsync("https://mailgen.biz/mail/fetch");
					string _rs = text;
					text = null;
					string str = _rs;
					string str2 = await _client.GetStringAsync("https://mailgen.biz/mail/fetch?new=true");
					_rs = str + str2;
					str = null;
					str2 = null;
					_tmpCode = Regex.Match(_rs, "FB[-][0-9]+").ToString();
					if (!string.IsNullOrEmpty(_tmpCode))
					{
						break;
					}
					_count++;
					if (_count >= 50)
					{
						goto Block_3;
					}
					_client = null;
					_tmpInfo = null;
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

		// Token: 0x06000036 RID: 54 RVA: 0x00003474 File Offset: 0x00001674
		private async Task<string> getCode2(string _email)
		{
			try
			{
				ServicePointManager.Expect100Continue = true;
				ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
				int _count = 0;
				string _code = "";
				string _tmpCode;
				for (;;)
				{
					await Task.Delay(20000);
					this.log("Start retry code " + _count);
					HttpClient _client = new HttpClient(new HttpClientHandler
					{
						UseCookies = false
					});
					_client.DefaultRequestHeaders.Add("Cookie", "_ga=GA1.2.61770657.1607270767; _gid=GA1.2.1123466691.1607270767; __gads=ID=a94e27823af01929-224a441f08c50006:T=1607270768:RT=1607270768:S=ALNI_Mbp8D2iSYvRQYtco_GgwN6Nq2Askg; embx=%5B%22tec313213afdsafsa%40fairocketsmail.com%22%2C%22tecopa4324321%40fairocketsmail.com%22%5D; _gat=1; surl=fairocketsmail.com/" + _email);
					string text = await _client.GetStringAsync("https://vi.emailfake.com/channel5/");
					string _rs = text;
					text = null;
					_tmpCode = Regex.Match(_rs, "FB[-][0-9]+").ToString();
					if (!string.IsNullOrEmpty(_tmpCode))
					{
						break;
					}
					_count++;
					if (_count >= 50)
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

		// Token: 0x06000037 RID: 55 RVA: 0x000034C4 File Offset: 0x000016C4
		private async Task<bool> createPage(string _tmpCookie, string _dataAll)
		{
			bool _rsCreatePage = false;
			await Task.Run(delegate()
			{
				try
				{
					this.initBrowserIphoneX();
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
							bool flag = _x.Length > 1;
							if (flag)
							{
								bool flag2 = _x.Length == 3;
								if (flag2)
								{
									_x[1] = _x[1] + "=" + _x[2];
								}
								bool flag3 = !string.IsNullOrEmpty(_x[0].Trim());
								if (flag3)
								{
									this._driver.Manage().Cookies.AddCookie(new OpenQA.Selenium.Cookie(_x[0], _x[1], ".facebook.com", "/", new DateTime?(DateTime.Now.AddDays(400.0))));
								}
							}
						}
						catch (Exception ex)
						{
							this.log(ex);
						}
					}
					this._driver.Navigate().GoToUrl("https://www.facebook.com");
					Thread.Sleep(3000);
					IWebElement _btskipPhone = this.getElement(By.Id("nux-nav-button"));
					bool flag4 = this.isValid(_btskipPhone);
					if (flag4)
					{
						_btskipPhone.Click();
						Thread.Sleep(3000);
						_btskipPhone = this.getElement(By.Id("nux-nav-button"));
						bool flag5 = this.isValid(_btskipPhone);
						if (flag5)
						{
							_btskipPhone.Click();
							Thread.Sleep(3000);
							_btskipPhone = this.getElement(By.Id("nux-nav-button"));
							bool flag6 = this.isValid(_btskipPhone);
							if (flag6)
							{
								_btskipPhone.Click();
								Thread.Sleep(3000);
							}
						}
					}
					IWebElement _btBookmark = this.getElement(By.Id("bookmarks_jewel"));
					bool flag7 = this.isValid(_btBookmark);
					if (flag7)
					{
						_btBookmark.Click();
						Thread.Sleep(1000);
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
								Thread.Sleep(5000);
								_tmpCount++;
								bool flag9 = _tmpCount > 5;
								if (flag9)
								{
									break;
								}
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
								IWebElement _btCreate = this.getElement(By.CssSelector("a[href*='/pages/creation_flow']"));
								bool flag13 = this.isValid(_btCreate);
								if (flag13)
								{
									_btCreate.Click();
									Thread.Sleep(2000);
									IWebElement _inputPageName = this.getElement(By.CssSelector("input[data-sigil='page_creation_name_input']"));
									bool flag14 = this.isValid(_inputPageName);
									if (flag14)
									{
										string _rNamePage = this._listKeyword[new Random(Guid.NewGuid().GetHashCode()).Next(0, this._listKeyword.Count)] + " " + this.randomNumber(2);
										this.fillInput(_inputPageName, _rNamePage);
										IWebElement _submitBt = this.getElement(By.CssSelector("button[type='submit']"));
										bool flag15 = this.isValid(_submitBt);
										if (flag15)
										{
											_submitBt.Click();
											Thread.Sleep(2000);
											SelectElement _selectCategory = new SelectElement(this.getElement(By.CssSelector("select[name='super_category_selector']")));
											_selectCategory.SelectByIndex(new Random().Next(2, _selectCategory.Options.Count));
											_selectCategory = new SelectElement(this.getElement(By.CssSelector("select[name='sub_category_selector']")));
											_selectCategory.SelectByIndex(new Random().Next(2, _selectCategory.Options.Count));
											_submitBt = this.getElement(By.CssSelector("button[type='submit']"));
											bool flag16 = this.isValid(_submitBt);
											if (flag16)
											{
												_submitBt.Click();
												Thread.Sleep(10000);
												IWebElement _btNext = this.getElement(By.CssSelector("a[href*='/pages/creation_flow/?step=profile_pic']"));
												bool flag17 = this.isValid(_btNext);
												if (flag17)
												{
													_btNext.Click();
												}
												Thread.Sleep(2000);
												_btNext = this.getElement(By.CssSelector("a[href*='/pages/creation_flow/?step=cover_photo']"));
												bool flag18 = this.isValid(_btNext);
												if (flag18)
												{
													_btNext.Click();
												}
												Thread.Sleep(5000);
												ReadOnlyCollection<IWebElement> _btSubmits = this._driver.FindElements(By.CssSelector("button[type='submit']"));
												bool flag19 = _btSubmits.Count > 1;
												if (flag19)
												{
													_btSubmits[1].Click();
													IWebElement _btAds = this.getElement(By.CssSelector("a[href*='https://m.facebook.com/ads/create/choose_objective']"));
													bool flag20 = this.isValid(_btAds);
													if (flag20)
													{
														_btAds.Click();
														Thread.Sleep(2000);
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
															Thread.Sleep(2000);
															this._driver.Navigate().GoToUrl("https://m.facebook.com/certification/nondiscrimination/");
															Thread.Sleep(3000);
															IWebElement _btSubmit = this.getElement(By.CssSelector("button[use='primary'][type='submit']"));
															bool flag22 = this.isValid(_btSubmit);
															if (flag22)
															{
																_btSubmit.Click();
																Thread.Sleep(1000);
																_btSubmit.Click();
																Thread.Sleep(3000);
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

		// Token: 0x06000038 RID: 56 RVA: 0x0000351C File Offset: 0x0000171C
		private async void button4_Click(object sender, EventArgs e)
		{
			await this.createPage("xs=37%3AD3C-d2nKnzJ5lg%3A2%3A1607277357%3A-1%3A-1; c_user=100058943328201; datr=AhvNX7C4NEDFGwp58i-gdv43; locale=el_GR; sb=DRvNXyNwrx5dgyXiAbn3orws; spin=r.1003057891_b.trunk_t.1607277362_s.1_v.2_; fr=1BqSeNynS641dwxGZ.AWXQOga5OUEWWEarDq1WtYsAYNs.BfzRsN.wG.AAA.0.0.BfzRso.AWV0kI7Ql_w; ", "");
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003568 File Offset: 0x00001768
		private async void button5_Click(object sender, EventArgs e)
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
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000035B4 File Offset: 0x000017B4
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
														File.AppendAllText("bank_fail.txt", _dataAll + "\n");
														this.log("NOT OK");
													}
													else
													{
														File.AppendAllText("bank_ok.txt", _dataAll + "\n");
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

		// Token: 0x0600003B RID: 59 RVA: 0x0000360C File Offset: 0x0000180C
		private async void button1_Click(object sender, EventArgs e)
		{
			
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003658 File Offset: 0x00001858
		private async void button2_Click(object sender, EventArgs e)
		{
			ServicePointManager.Expect100Continue = true;
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
			string text = await new HttpClient(new HttpClientHandler
			{
				UseCookies = false
			})
			{
				DefaultRequestHeaders = 
				{
					{
						"Cookie",
						"_ga=GA1.2.61770657.1607270767; _gid=GA1.2.1123466691.1607270767; __gads=ID=a94e27823af01929-224a441f08c50006:T=1607270768:RT=1607270768:S=ALNI_Mbp8D2iSYvRQYtco_GgwN6Nq2Askg; embx=%5B%22tec313213afdsafsa%40fairocketsmail.com%22%2C%22tecopa4324321%40fairocketsmail.com%22%5D; _gat=1; surl=fairocketsmail.com/davidjame"
					}
				}
			}.GetStringAsync("https://vi.emailfake.com/channel5/");
			string _rs = text;
			text = null;
			this.log(_rs);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000036A2 File Offset: 0x000018A2
		private void cbCloseChrome_CheckedChanged(object sender, EventArgs e)
		{
			this._closeChrome = this.cbCloseChrome.Checked;
		}

		// Token: 0x04000003 RID: 3
		private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

		// Token: 0x04000004 RID: 4
		private const uint SWP_NOSIZE = 1U;

		// Token: 0x04000005 RID: 5
		private const uint SWP_NOMOVE = 2U;

		// Token: 0x04000006 RID: 6
		private const uint TOPMOST_FLAGS = 3U;

		// Token: 0x04000007 RID: 7
		public string _instance = "";

		// Token: 0x04000008 RID: 8
		public string _templace = "";

		// Token: 0x04000009 RID: 9
		public Hashtable _state = null;

		// Token: 0x0400000A RID: 10
		private List<string> _listKeyword;

		// Token: 0x0400000B RID: 11
		private string currentValue = "";

		// Token: 0x0400000C RID: 12
		private List<ManageChannel.TmpDisplay> _listDisplay = new List<ManageChannel.TmpDisplay>();

		// Token: 0x0400000D RID: 13
		private IKeyboardMouseEvents m_Events;

		// Token: 0x0400000E RID: 14
		private string firefoxPath = "C:\\Program Files (x86)\\Google\\Chrome\\Application\\chrome.exe";

		// Token: 0x0400000F RID: 15
		private string IP = "127.0.0.1";

		// Token: 0x04000010 RID: 16
		private string _folderPath = "";

		// Token: 0x04000011 RID: 17
		private IWebDriver _driver = null;

		// Token: 0x04000012 RID: 18
		private string _ip = "";

		// Token: 0x04000013 RID: 19
		private bool _closeChrome = true;

		// Token: 0x02000004 RID: 4
		public class TmpDisplay
		{
			// Token: 0x17000003 RID: 3
			// (get) Token: 0x06000041 RID: 65 RVA: 0x00003EBE File Offset: 0x000020BE
			// (set) Token: 0x06000042 RID: 66 RVA: 0x00003EC6 File Offset: 0x000020C6
			public string Name { get; set; }

			// Token: 0x17000004 RID: 4
			// (get) Token: 0x06000043 RID: 67 RVA: 0x00003ECF File Offset: 0x000020CF
			// (set) Token: 0x06000044 RID: 68 RVA: 0x00003ED7 File Offset: 0x000020D7
			public string Value { get; set; }
		}
	}
}
