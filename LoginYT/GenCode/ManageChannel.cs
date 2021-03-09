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
using System.Configuration;
using Firebase.Database;
using Firebase.Database.Query;

namespace GenCode
{
	// Token: 0x02000003 RID: 3
	public partial class ManageChannel : Form
	{
		// Token: 0x06000008 RID: 8
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

		private FirebaseClient firebase;
		private bool useapi = false;
		private bool autosharetovia = false;
		private bool addbanking = false;

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
				// this.log("Created " + this._folderPath);
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
			this.StartPosition = FormStartPosition.CenterScreen;
			this.log("Version 2.0.8");
			StartFirebase();
		}

		private bool StartFirebase()
        {
            bool result;
            try
            {
                string FirebaseUrl = ConfigurationManager.AppSettings["FirebaseUrl"];
                string FirebaseSecretKey = ConfigurationManager.AppSettings["FirebaseSecretKey"];

                firebase = new FirebaseClient(FirebaseUrl, new FirebaseOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(FirebaseSecretKey)
                });
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
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

		private void killChromePortable()
		{
			try
			{
				string m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
				string folderPath = m_exePath + "\\via";
				bool exists = System.IO.Directory.Exists(folderPath);
                if (exists)
                {
					DirectoryInfo directory = new DirectoryInfo(folderPath);
					foreach (DirectoryInfo dir in directory.EnumerateDirectories())
					{
						dir.Delete(true);
					}
				}
			}
			catch (Exception ex)
			{
				this.log(ex);
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
			if (this.textBox8.Text.Trim().Length == 0)
			{
				MessageBox.Show("Phải nhập id VIA nhận!", "Thông báo!", MessageBoxButtons.OK);
				return;
			}

			this.log("Kill Chrome!");
			this.killChrome();

			this.log("Delete ChromePortable!");
			this.killChromePortable();

			this.log("New session!");
			useapi = this.checkBox1.Checked;
			autosharetovia = this.checkBox2.Checked;
			addbanking = this.checkBox3.Checked;
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
					_reg.initChromePortable(chrome);
					bool regres = await _reg.RegFaceBook();
                    if (regres)
                    {
                        if (addbanking)
                        {
							bool bank = await _reg.addBank();
							if (bank)
							{
								if (autosharetovia)
								{
									if (!String.IsNullOrEmpty(_reg._clone_uid))
									{
										string viaid = this.textBox8.Text.Trim();
										bool addfriendrq = await friendRequestAsync("https://www.facebook.com/" + _reg._clone_uid, viaid);
										if (addfriendrq)
										{
											bool agreeF = await _reg.agreeFriends();
											if (agreeF)
											{
												// share to via
												this.log("share to via!");
												bool share = await _reg.shareAdsToVia(viaid);
												// Share BM
												if (share)
												{
													await shareBmAsync(_reg._ads_id, viaid);
												}
											}
										}
									}
								}
							}
                        }
                        else
                        {
							if (autosharetovia)
							{
								if (!String.IsNullOrEmpty(_reg._clone_uid))
								{
									string viaid = this.textBox8.Text.Trim();
									bool addfriendrq = await friendRequestAsync("https://www.facebook.com/" + _reg._clone_uid, viaid);
									if (addfriendrq)
									{
										bool agreeF = await _reg.agreeFriends();
										if (agreeF)
										{
											// share to via
											this.log("share to via!");
											bool share = await _reg.shareAdsToVia(viaid);
											// Share BM
											if (share)
											{
												await shareBmAsync(_reg._ads_id, viaid);
											}
										}
									}
								}
							}
						}
                    }
                    else
                    {
						_reg.QuitDriver();
					}
				}).Start();
			}
		}

		private async Task<bool> friendRequestAsync(string cloneLink, string viaId)
		{
			try
			{
				this.log("Yêu cầu kết bạn: " + cloneLink);
				FireBaseMessage message = new FireBaseMessage();
				int Timestamp = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
				message.Id = Timestamp.ToString();
				message.Profile = cloneLink;
				message.Type = 1;
				await firebase.Child("share/" + viaId).Child(message.Id).PutAsync(message);
				this.log("Gửi yêu cầu kết bạn thành công!");
				return true;
			}
			catch (Exception error)
			{
				this.log(error);
				return false;
			}
		}

		private async Task<bool> shareBmAsync(string adsId, string viaId)
		{
			try
			{
				this.log("Share to BM: " + adsId);
				FireBaseMessage message = new FireBaseMessage();
				int Timestamp = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
				message.Id = Timestamp.ToString();
				message.Profile = adsId;
				message.Type = 2;
				await firebase.Child("share/" + viaId).Child(message.Id).PutAsync(message);
				this.log("Share Ads thành công!");
				return true;
			}
			catch (Exception error)
			{
				this.log(error);
				return false;
			}
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
		private bool _closeChrome = false;

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

        private void button4_Click(object sender, EventArgs e)
        {
			if (this.textBox3.Text.Trim().Length == 0)
			{
				MessageBox.Show("Phải nhập BM Id!", "Thông báo!", MessageBoxButtons.OK);
				return;
			}

			if (this.textBox4.Text.Trim().Length == 0)
			{
				MessageBox.Show("Phải nhập BM Token!", "Thông báo!", MessageBoxButtons.OK);
				return;
			}

			if (this.textBox5.Text.Trim().Length == 0)
			{
				MessageBox.Show("Phải nhập clone_uid!", "Thông báo!", MessageBoxButtons.OK);
				return;
			}

			if (this.textBox6.Text.Trim().Length == 0)
			{
				MessageBox.Show("Phải nhập clone_adsID!", "Thông báo!", MessageBoxButtons.OK);
				return;
			}

			if (this.textBox7.Text.Trim().Length == 0)
			{
				MessageBox.Show("Phải nhập clone_cookie!", "Thông báo!", MessageBoxButtons.OK);
				return;
			}

			RegClone _reg = new RegClone();
			_reg._listKeyword = this._listKeyword;
			_reg._ThreadName = "df";
			_reg._closeChrome = this._closeChrome;
			_reg.bm_id = this.textBox3.Text.Trim();
			_reg.bm_token = this.textBox4.Text.Trim();
			RegClone regClone = _reg;
			regClone._logDelegate = (RegClone.LogDelegate)Delegate.Combine(regClone._logDelegate, new RegClone.LogDelegate(this.LogDelegate));
			_reg.shareAds(this.textBox5.Text.Trim(), this.textBox6.Text.Trim(), this.textBox7.Text.Trim());
		}

        private async void button1_Click_1(object sender, EventArgs e)
        {
			if (this.textBox8.Text.Trim().Length == 0)
			{
				MessageBox.Show("Phải nhập id VIA nhận!", "Thông báo!", MessageBoxButtons.OK);
				return;
			}

			if (this.textBox1.Text.Trim().Length == 0)
			{
				MessageBox.Show("Phải nhập link facebook cá nhân!", "Thông báo!", MessageBoxButtons.OK);
				return;
			}

			bool addfriendrq = await friendRequestAsync(this.textBox1.Text.Trim(), this.textBox8.Text.Trim());
            if (!addfriendrq)
            {
				this.log("Gửi yêu cầu kết bạn KHÔNG thành công!");
			}
		}

        private async void button2_Click_1(object sender, EventArgs e)
        {
			if (this.textBox2.Text.Trim().Length == 0)
			{
				MessageBox.Show("Phải nhập Ads ID!", "Thông báo!", MessageBoxButtons.OK);
				return;
			}

			if (this.textBox8.Text.Trim().Length == 0)
			{
				MessageBox.Show("Phải nhập id VIA nhận!", "Thông báo!", MessageBoxButtons.OK);
				return;
			}

			bool addfriendrq = await shareBmAsync(this.textBox2.Text.Trim(), this.textBox8.Text.Trim());
			if (!addfriendrq)
			{
				this.log("Share Ads KHÔNG thành công!");
			}
		}

        private void button5_Click(object sender, EventArgs e)
        {
			this.log("Kill Chrome!");
			this.killChrome();

			this.log("Delete ChromePortable!");
			this.killChromePortable();

			this.log("New session!");
			useapi = this.checkBox1.Checked;
			int _numberThread = (int)this.tbNumberThread.Value;
			for (int i = 0; i < _numberThread; i++)
			{
				string _threadName = i.ToString().Clone().ToString();
				new Thread(async delegate ()
				{
					RegClone _reg = new RegClone();
					_reg._listKeyword = this._listKeyword;
					_reg._ThreadName = (_threadName ?? "");
					_reg._closeChrome = this._closeChrome;
					RegClone regClone = _reg;
					regClone._logDelegate = (RegClone.LogDelegate)Delegate.Combine(regClone._logDelegate, new RegClone.LogDelegate(this.LogDelegate));
					string chrome = DateTime.Now.ToString("MM_dd_yyyy_HH_mm_ss") + "_" + _reg._ThreadName;
					initBrowse(chrome);
					_reg.initChromePortable(chrome);
					await _reg.RegFaceBook(useapi);
				}).Start();
			}
		}
	}
}
