using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GenCode
{
	// Token: 0x02000019 RID: 25
	internal class HttpClientHelper
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00007B58 File Offset: 0x00005D58
		public static HttpClientHelper Instance
		{
			get
			{
				HttpClientHelper._instance = (HttpClientHelper._instance ?? new HttpClientHelper());
				return HttpClientHelper._instance;
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00007B82 File Offset: 0x00005D82
		public HttpClientHelper()
		{
			this.initHttpClient();
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00007B93 File Offset: 0x00005D93
		private void initHttpClient()
		{
			this.client = new HttpClient();
			this.client.DefaultRequestHeaders.Remove("User-Agent");
			this.client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.103 Safari/537.36");
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00007BD4 File Offset: 0x00005DD4
		public async Task<string> getFromStream(Stream response)
		{
			string text = await Task.Run<string>(delegate()
			{
				StreamReader reader = new StreamReader(response);
				string result = "";
				while (!reader.EndOfStream)
				{
					result += reader.ReadLine();
				}
				return result;
			});
			string x = text;
			text = null;
			return x;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00007C24 File Offset: 0x00005E24
		public async Task<string> getUrl(string url)
		{
			this.initHttpClient();
			Stream stream = await this.client.GetStreamAsync(url);
			Stream st = stream;
			stream = null;
			string text = await this.getFromStream(st);
			string rs = text;
			text = null;
			return rs;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00007C74 File Offset: 0x00005E74
		public async Task<List<string>> getAllServer()
		{
			List<string> result;
			try
			{
				this.initHttpClient();
				Stream stream = await this.client.GetStreamAsync("http://52.206.191.94/static/update/hmaserver.txt");
				Stream st = stream;
				stream = null;
				string text = await this.getFromStream(st);
				string rs = text;
				text = null;
				if (!string.IsNullOrEmpty(rs))
				{
					string[] _listServer = rs.Split(new char[]
					{
						';'
					});
					result = _listServer.ToList<string>();
				}
				else
				{
					result = null;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00007CBC File Offset: 0x00005EBC
		public async Task<string> getAllAcc()
		{
			string result;
			try
			{
				this.initHttpClient();
				Stream stream = await this.client.GetStreamAsync("http://52.206.191.94/static/update/acc.txt");
				Stream st = stream;
				stream = null;
				string text = await this.getFromStream(st);
				string rs = text;
				text = null;
				if (!string.IsNullOrEmpty(rs))
				{
					string _listServer = rs.Replace(";", "\n");
					result = _listServer;
				}
				else
				{
					result = null;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00007D04 File Offset: 0x00005F04
		public async Task<JObject> addVerify(string _info)
		{
			JObject result;
			try
			{
				this.initHttpClient();
				string postBody = JsonConvert.SerializeObject(new JObject
				{
					{
						"info",
						_info
					}
				});
				this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpResponseMessage httpResponseMessage = await this.client.PostAsync("http://34.230.39.127:6969/v1/addverify", new StringContent(postBody, Encoding.UTF8, "application/json"));
				HttpResponseMessage wcfResponse = httpResponseMessage;
				httpResponseMessage = null;
				string text = await wcfResponse.Content.ReadAsStringAsync();
				string rs = text;
				text = null;
				JObject _rsApi = JsonConvert.DeserializeObject<JObject>(rs);
				result = _rsApi;
			}
			catch (Exception ex)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00007D54 File Offset: 0x00005F54
		public async Task<JObject> addChannel(string _info)
		{
			JObject result;
			try
			{
				this.initHttpClient();
				string postBody = JsonConvert.SerializeObject(new JObject
				{
					{
						"info",
						_info
					}
				});
				this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpResponseMessage httpResponseMessage = await this.client.PostAsync("http://34.230.39.127:6969/v1/validatev2", new StringContent(postBody, Encoding.UTF8, "application/json"));
				HttpResponseMessage wcfResponse = httpResponseMessage;
				httpResponseMessage = null;
				string text = await wcfResponse.Content.ReadAsStringAsync();
				string rs = text;
				text = null;
				JObject _rsApi = JsonConvert.DeserializeObject<JObject>(rs);
				result = _rsApi;
			}
			catch (Exception ex)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00007DA4 File Offset: 0x00005FA4
		public async Task<JObject> renew(string _info)
		{
			JObject result;
			try
			{
				this.initHttpClient();
				string _tmpInfo = "{\"username\":\"" + _info + "\",\"password\":\"Trieu2019\",\"days\":\"30\",\"adminpass\":\"ngaymai123\"}";
				this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpResponseMessage httpResponseMessage = await this.client.PostAsync("http://chdmovie.com/v1/unfreeze", new StringContent(_tmpInfo, Encoding.UTF8, "application/json"));
				HttpResponseMessage wcfResponse = httpResponseMessage;
				httpResponseMessage = null;
				string text = await wcfResponse.Content.ReadAsStringAsync();
				string rs = text;
				text = null;
				JObject _rsApi = JsonConvert.DeserializeObject<JObject>(rs);
				result = _rsApi;
			}
			catch (Exception ex)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00007DF4 File Offset: 0x00005FF4
		public async Task<JObject> register(string _info)
		{
			JObject result;
			try
			{
				this.initHttpClient();
				string _tmpInfo = string.Concat(new string[]
				{
					"{\"username\":\"",
					_info,
					"\",\"password\":\"",
					_info,
					"\",\"days\":\"30\",\"adminpass\":\"ngaymai123\"}"
				});
				this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpResponseMessage httpResponseMessage = await this.client.PostAsync("http://chdmovie.com/v1/register", new StringContent(_tmpInfo, Encoding.UTF8, "application/json"));
				HttpResponseMessage wcfResponse = httpResponseMessage;
				httpResponseMessage = null;
				string text = await wcfResponse.Content.ReadAsStringAsync();
				string rs = text;
				text = null;
				JObject _rsApi = JsonConvert.DeserializeObject<JObject>(rs);
				result = _rsApi;
			}
			catch (Exception ex)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00007E44 File Offset: 0x00006044
		public async Task<JObject> addVerify2(string _info)
		{
			JObject result;
			try
			{
				this.initHttpClient();
				string postBody = JsonConvert.SerializeObject(new JObject
				{
					{
						"info",
						_info
					}
				});
				this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpResponseMessage httpResponseMessage = await this.client.PostAsync("http://34.230.39.127:6969/v1/validatev2", new StringContent(postBody, Encoding.UTF8, "application/json"));
				HttpResponseMessage wcfResponse = httpResponseMessage;
				httpResponseMessage = null;
				string text = await wcfResponse.Content.ReadAsStringAsync();
				string rs = text;
				text = null;
				JObject _rsApi = JsonConvert.DeserializeObject<JObject>(rs);
				result = _rsApi;
			}
			catch (Exception ex)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x040000F3 RID: 243
		private static HttpClientHelper _instance;

		// Token: 0x040000F4 RID: 244
		private HttpClient client;
	}
}
