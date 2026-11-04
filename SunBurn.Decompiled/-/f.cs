using System;
using System.Collections.Generic;

namespace _0019
{
	internal class f : List<g>
	{
		public B GetNamedItem(string name)
		{
			using (Enumerator enumerator = GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					B current = enumerator.Current;
					if (current.Name == name)
					{
						return current;
					}
				}
			}
			return null;
		}
	}
}
namespace _0011
{
	internal class f
	{
		private const string fNB = "1.3.14.3.2.26";

		private static object fNa;

		static f()
		{
			fNa = new object();
		}

		private static void _0003m()
		{
		}

		private static byte[] _0003h(long P_0)
		{
			if (P_0 > int.MaxValue || P_0 < int.MinValue)
			{
				throw new OverflowException("Part of OID doesn't fit in Int32");
			}
			long num = P_0;
			int num2 = 1;
			while (num > 127)
			{
				num >>= 7;
				num2++;
			}
			byte[] array = new byte[num2];
			for (int k = 0; k < num2; k++)
			{
				num = P_0 >> 7 * k;
				num &= 0x7F;
				if (k != 0)
				{
					num += 128;
				}
				array[num2 - k - 1] = Convert.ToByte(num);
			}
			return array;
		}

		public static byte[] EncodeOID(string str)
		{
			char[] separator = new char[1] { '.' };
			string[] array = str.Split(separator);
			if (array.Length < 2)
			{
				throw new n("OID must have at least two parts");
			}
			byte[] array2 = new byte[str.Length];
			try
			{
				byte b = Convert.ToByte(array[0]);
				byte b2 = Convert.ToByte(array[1]);
				array2[2] = Convert.ToByte(b * 40 + b2);
			}
			catch
			{
				throw new n("Invalid OID");
			}
			int num = 3;
			for (int k = 2; k < array.Length; k++)
			{
				long num2 = Convert.ToInt64(array[k]);
				if (num2 > 127)
				{
					byte[] array3 = _0003h(num2);
					Buffer.BlockCopy(array3, 0, array2, num, array3.Length);
					num += array3.Length;
				}
				else
				{
					array2[num++] = Convert.ToByte(num2);
				}
			}
			int num3 = 2;
			byte[] array4 = new byte[num];
			array4[0] = 6;
			if (num > 127)
			{
				throw new n("OID > 127 bytes");
			}
			array4[1] = Convert.ToByte(num - 2);
			Buffer.BlockCopy(array2, num3, array4, num3, num - num3);
			return array4;
		}

		public static string MapNameToOID(string name)
		{
			return "1.3.14.3.2.26";
		}
	}
}
