using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace GenCode
{
	// Token: 0x02000031 RID: 49
	internal static class ShuffleHelper
	{
		// Token: 0x060000EE RID: 238 RVA: 0x0000C7CC File Offset: 0x0000A9CC
		public static void Shuffle<T>(this IList<T> list)
		{
			int i = list.Count;
			while (i > 1)
			{
				i--;
				int j = ShuffleHelper.rng.Next(i + 1);
				T value = list[j];
				list[j] = list[i];
				list[i] = value;
			}
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000C820 File Offset: 0x0000AA20
		public static IEnumerable<string> CustomSort(this IEnumerable<string> list)
		{
			/*int maxLen = (from s in list
			select s.Length).Max();
			MatchEvaluator <>9__2;
			return from x in list.Select(delegate(string s)
			{
				string pattern = "(\\d+)|(\\D+)";
				MatchEvaluator evaluator;
				if ((evaluator = <>9__2) == null)
				{
					evaluator = (<>9__2 = ((Match m) => m.Value.PadLeft(maxLen, char.IsDigit(m.Value[0]) ? ' ' : char.MaxValue)));
				}
				return new
				{
					OrgStr = s,
					SortStr = Regex.Replace(s, pattern, evaluator)
				};
			})
			orderby x.SortStr
			select x.OrgStr;*/
			return list;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0000C8C4 File Offset: 0x0000AAC4
		public static string Replace(this string str, string oldValue, string newValue, StringComparison comparisonType)
		{
			newValue = (newValue ?? string.Empty);
			bool flag = string.IsNullOrEmpty(str) || string.IsNullOrEmpty(oldValue) || oldValue.Equals(newValue, comparisonType);
			string result;
			if (flag)
			{
				result = str;
			}
			else
			{
				int foundAt;
				while ((foundAt = str.IndexOf(oldValue, 0, comparisonType)) != -1)
				{
					str = str.Remove(foundAt, oldValue.Length).Insert(foundAt, newValue);
				}
				result = str;
			}
			return result;
		}

		// Token: 0x040001DB RID: 475
		private static Random rng = new Random(Guid.NewGuid().GetHashCode());
	}
}
