using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Costura
{
	// Token: 0x0200003D RID: 61
	[CompilerGenerated]
	internal static class AssemblyLoader
	{
		// Token: 0x0600011C RID: 284 RVA: 0x0000CD6A File Offset: 0x0000AF6A
		private static string CultureToString(CultureInfo culture)
		{
			if (culture == null)
			{
				return "";
			}
			return culture.Name;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000CD7C File Offset: 0x0000AF7C
		private static Assembly ReadExistingAssembly(AssemblyName name)
		{
			foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				AssemblyName name2 = assembly.GetName();
				if (string.Equals(name2.Name, name.Name, StringComparison.InvariantCultureIgnoreCase) && string.Equals(AssemblyLoader.CultureToString(name2.CultureInfo), AssemblyLoader.CultureToString(name.CultureInfo), StringComparison.InvariantCultureIgnoreCase))
				{
					return assembly;
				}
			}
			return null;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000CDE4 File Offset: 0x0000AFE4
		private static void CopyTo(Stream source, Stream destination)
		{
			byte[] array = new byte[81920];
			int count;
			while ((count = source.Read(array, 0, array.Length)) != 0)
			{
				destination.Write(array, 0, count);
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000CE18 File Offset: 0x0000B018
		private static Stream LoadStream(string fullname)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			if (fullname.EndsWith(".compressed"))
			{
				using (Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(fullname))
				{
					using (DeflateStream deflateStream = new DeflateStream(manifestResourceStream, CompressionMode.Decompress))
					{
						MemoryStream memoryStream = new MemoryStream();
						AssemblyLoader.CopyTo(deflateStream, memoryStream);
						memoryStream.Position = 0L;
						return memoryStream;
					}
				}
			}
			return executingAssembly.GetManifestResourceStream(fullname);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000CE9C File Offset: 0x0000B09C
		private static Stream LoadStream(Dictionary<string, string> resourceNames, string name)
		{
			string fullname;
			if (resourceNames.TryGetValue(name, out fullname))
			{
				return AssemblyLoader.LoadStream(fullname);
			}
			return null;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000CEBC File Offset: 0x0000B0BC
		private static byte[] ReadStream(Stream stream)
		{
			byte[] array = new byte[stream.Length];
			stream.Read(array, 0, array.Length);
			return array;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000CEE4 File Offset: 0x0000B0E4
		private static Assembly ReadFromEmbeddedResources(Dictionary<string, string> assemblyNames, Dictionary<string, string> symbolNames, AssemblyName requestedAssemblyName)
		{
			string text = requestedAssemblyName.Name.ToLowerInvariant();
			if (requestedAssemblyName.CultureInfo != null && !string.IsNullOrEmpty(requestedAssemblyName.CultureInfo.Name))
			{
				text = string.Format("{0}.{1}", requestedAssemblyName.CultureInfo.Name, text);
			}
			byte[] rawAssembly;
			using (Stream stream = AssemblyLoader.LoadStream(assemblyNames, text))
			{
				if (stream == null)
				{
					return null;
				}
				rawAssembly = AssemblyLoader.ReadStream(stream);
			}
			using (Stream stream2 = AssemblyLoader.LoadStream(symbolNames, text))
			{
				if (stream2 != null)
				{
					byte[] rawSymbolStore = AssemblyLoader.ReadStream(stream2);
					return Assembly.Load(rawAssembly, rawSymbolStore);
				}
			}
			return Assembly.Load(rawAssembly);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0000CFA4 File Offset: 0x0000B1A4
		public static Assembly ResolveAssembly(object sender, ResolveEventArgs e)
		{
			object obj = AssemblyLoader.nullCacheLock;
			lock (obj)
			{
				if (AssemblyLoader.nullCache.ContainsKey(e.Name))
				{
					return null;
				}
			}
			AssemblyName assemblyName = new AssemblyName(e.Name);
			Assembly assembly = AssemblyLoader.ReadExistingAssembly(assemblyName);
			if (assembly != null)
			{
				return assembly;
			}
			assembly = AssemblyLoader.ReadFromEmbeddedResources(AssemblyLoader.assemblyNames, AssemblyLoader.symbolNames, assemblyName);
			if (assembly == null)
			{
				obj = AssemblyLoader.nullCacheLock;
				lock (obj)
				{
					AssemblyLoader.nullCache[e.Name] = true;
				}
				if (assemblyName.Flags == AssemblyNameFlags.Retargetable)
				{
					assembly = Assembly.Load(assemblyName);
				}
			}
			return assembly;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000D064 File Offset: 0x0000B264
		// Note: this type is marked as 'beforefieldinit'.
		static AssemblyLoader()
		{
			AssemblyLoader.assemblyNames.Add("csquery", "costura.csquery.dll.compressed");
			AssemblyLoader.assemblyNames.Add("gma.system.mousekeyhook", "costura.gma.system.mousekeyhook.dll.compressed");
			AssemblyLoader.assemblyNames.Add("newtonsoft.json", "costura.newtonsoft.json.dll.compressed");
			AssemblyLoader.assemblyNames.Add("supersocket.clientengine", "costura.supersocket.clientengine.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.collections.specialized", "costura.system.collections.specialized.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.io", "costura.system.io.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.linq", "costura.system.linq.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.net.nameresolution", "costura.system.net.nameresolution.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.net.security", "costura.system.net.security.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.net.sockets", "costura.system.net.sockets.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.runtime", "costura.system.runtime.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.runtime.extensions", "costura.system.runtime.extensions.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.runtime.interopservices", "costura.system.runtime.interopservices.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.security.cryptography.algorithms", "costura.system.security.cryptography.algorithms.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.security.cryptography.encoding", "costura.system.security.cryptography.encoding.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.security.cryptography.primitives", "costura.system.security.cryptography.primitives.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.security.cryptography.x509certificates", "costura.system.security.cryptography.x509certificates.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.text.regularexpressions", "costura.system.text.regularexpressions.dll.compressed");
			AssemblyLoader.assemblyNames.Add("webdriver", "costura.webdriver.dll.compressed");
			AssemblyLoader.assemblyNames.Add("webdriver.support", "costura.webdriver.support.dll.compressed");
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000D22F File Offset: 0x0000B42F
		public static void Attach()
		{
			if (Interlocked.Exchange(ref AssemblyLoader.isAttached, 1) == 1)
			{
				return;
			}
			AppDomain.CurrentDomain.AssemblyResolve += AssemblyLoader.ResolveAssembly;
		}

		// Token: 0x040001E7 RID: 487
		private static readonly object nullCacheLock = new object();

		// Token: 0x040001E8 RID: 488
		private static readonly Dictionary<string, bool> nullCache = new Dictionary<string, bool>();

		// Token: 0x040001E9 RID: 489
		private static readonly Dictionary<string, string> assemblyNames = new Dictionary<string, string>();

		// Token: 0x040001EA RID: 490
		private static readonly Dictionary<string, string> symbolNames = new Dictionary<string, string>();

		// Token: 0x040001EB RID: 491
		private static int isAttached = 0;
	}
}
