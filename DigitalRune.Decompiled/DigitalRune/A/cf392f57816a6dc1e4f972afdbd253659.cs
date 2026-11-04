using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace A;

internal class cf392f57816a6dc1e4f972afdbd253659
{
	internal const char c236912e468eb6f39af7ee59183004304 = '`';

	internal const char cfb5b42aa24c4633e5ae9e0d94e7fbe57 = '~';

	internal const string cabdfca56eecea3239b38960057988c2f = "!";

	internal const string c7b23a6a523e36cdd2811da8e8bc9945a = "~!";

	private static readonly string c4576e9f7171140cd2c0561ba54b62688 = c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(10023);

	internal static string cff5b1897f51cbdb93bc719ae4daff3a7 = c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(10155);

	internal static string cd8477039016c102e931fa71ca518ff80 = c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(10220);

	internal static string c3108a583b9306b5486931a27412f3ebc = c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(10223);

	internal static string c86b6737c9c8de83d71d5a8ff45d17342(string cdb0b3f04d7042a8d672da77421cc7b9e)
	{
		return cdb0b3f04d7042a8d672da77421cc7b9e.Replace('\\', Path.DirectorySeparatorChar);
	}

	internal static string caa2df1972a7f6c1a1f47051483c02eea(string c6a9aadf14c7ffd765c6ad1c29b0b732c)
	{
		StringBuilder stringBuilder = new StringBuilder(c6a9aadf14c7ffd765c6ad1c29b0b732c.Length + 8);
		stringBuilder.Append(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9926));
		for (int i = 0; i < c6a9aadf14c7ffd765c6ad1c29b0b732c.Length; i++)
		{
			char c = c6a9aadf14c7ffd765c6ad1c29b0b732c[i];
			char c2 = c;
			if (c2 != '*')
			{
				while (true)
				{
					switch (3)
					{
					case 0:
						continue;
					}
					break;
				}
				if (1 == 0)
				{
					__ldtoken(cf392f57816a6dc1e4f972afdbd253659.caa2df1972a7f6c1a1f47051483c02eea(string));
				}
				if (c2 != '?')
				{
					while (true)
					{
						switch (5)
						{
						case 0:
							continue;
						}
						break;
					}
					if (c2 != '\\')
					{
						while (true)
						{
							switch (6)
							{
							case 0:
								continue;
							}
							break;
						}
						stringBuilder.Append(Regex.Escape(c6a9aadf14c7ffd765c6ad1c29b0b732c[i].ToString()));
					}
					else if (i < c6a9aadf14c7ffd765c6ad1c29b0b732c.Length - 1)
					{
						stringBuilder.Append(Regex.Escape(c6a9aadf14c7ffd765c6ad1c29b0b732c[++i].ToString()));
					}
				}
				else
				{
					stringBuilder.Append(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1613));
				}
			}
			else
			{
				stringBuilder.Append(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9929));
			}
		}
		while (true)
		{
			switch (4)
			{
			case 0:
				continue;
			}
			stringBuilder.Append(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9934));
			return stringBuilder.ToString();
		}
	}

	internal static bool c352aa377b2bb1402ecb10043063d52f1(string c178caa51c848fc2a47f1589dec7f06ca)
	{
		if (c178caa51c848fc2a47f1589dec7f06ca != null)
		{
			while (true)
			{
				switch (5)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(cf392f57816a6dc1e4f972afdbd253659.c352aa377b2bb1402ecb10043063d52f1(string));
					}
					return c178caa51c848fc2a47f1589dec7f06ca.Length == 0;
				}
			}
		}
		return true;
	}

	internal static byte[] c44469f2afc881769047175903945c5ea(byte[] caea7c8eeab7dec52f3eabd5619c94714)
	{
		if (caea7c8eeab7dec52f3eabd5619c94714 == null)
		{
			while (true)
			{
				switch (4)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(cf392f57816a6dc1e4f972afdbd253659.c44469f2afc881769047175903945c5ea(byte[]));
					}
					return null;
				}
			}
		}
		byte[] array = new byte[caea7c8eeab7dec52f3eabd5619c94714.Length];
		caea7c8eeab7dec52f3eabd5619c94714.CopyTo(array, 0);
		return array;
	}

	internal static bool c9a7d079446f5d74a7dc7c66bef0dd89c(byte[] c63b39959d0967016174a65b9ded457eb)
	{
		if (c63b39959d0967016174a65b9ded457eb == null)
		{
			return true;
		}
		for (int i = 0; i < c63b39959d0967016174a65b9ded457eb.Length; i++)
		{
			if (c63b39959d0967016174a65b9ded457eb[i] == 0)
			{
				continue;
			}
			while (true)
			{
				switch (2)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(cf392f57816a6dc1e4f972afdbd253659.c9a7d079446f5d74a7dc7c66bef0dd89c(byte[]));
				}
				return false;
			}
		}
		return true;
	}

	internal static byte[] c92efce0a250a5bfff5b5d3cce1662d0b(byte[] caea7c8eeab7dec52f3eabd5619c94714)
	{
		if (caea7c8eeab7dec52f3eabd5619c94714 == null)
		{
			return null;
		}
		int num = caea7c8eeab7dec52f3eabd5619c94714.Length - 1;
		while (true)
		{
			if (num >= 0)
			{
				if (caea7c8eeab7dec52f3eabd5619c94714[num] != 0)
				{
					break;
				}
				num--;
				continue;
			}
			while (true)
			{
				switch (1)
				{
				case 0:
					continue;
				}
				break;
			}
			if (1 == 0)
			{
				__ldtoken(cf392f57816a6dc1e4f972afdbd253659.c92efce0a250a5bfff5b5d3cce1662d0b(byte[]));
			}
			break;
		}
		if (num == -1)
		{
			return null;
		}
		byte[] array = new byte[num + 1];
		for (num = 0; num < array.Length; num++)
		{
			array[num] = caea7c8eeab7dec52f3eabd5619c94714[num];
		}
		while (true)
		{
			switch (3)
			{
			case 0:
				continue;
			}
			return array;
		}
	}

	internal static c5d83b9a67cd1031a61b8188f33357ed7 c7d4e527207ffc8d45c4ceddfc39fe1d9(byte[] c55c90b3a326fb3b42bbd371d20bfc90a)
	{
		uint num = 0u;
		for (int i = 0; i < 4; i++)
		{
			if (i < c55c90b3a326fb3b42bbd371d20bfc90a.Length)
			{
				num |= (uint)(c55c90b3a326fb3b42bbd371d20bfc90a[i] << 8 * i);
				continue;
			}
			while (true)
			{
				switch (6)
				{
				case 0:
					continue;
				}
				break;
			}
			if (1 == 0)
			{
				__ldtoken(cf392f57816a6dc1e4f972afdbd253659.c7d4e527207ffc8d45c4ceddfc39fe1d9(byte[]));
			}
			break;
		}
		return (c5d83b9a67cd1031a61b8188f33357ed7)(num & 0x7FFFFFFF);
	}

	internal static void c4d5a63370f9a3c80fc8bc102279cf00a(string c049a5392ad3e954c892c056bde2579ac)
	{
	}

	internal static bool c352aa377b2bb1402ecb10043063d52f1(Array c896199c1a94c4753bb4783ca04e801ab)
	{
		if (c896199c1a94c4753bb4783ca04e801ab != null)
		{
			return c896199c1a94c4753bb4783ca04e801ab.Length == 0;
		}
		return true;
	}

	internal static string c05c66ad2b870d39fc1dd7d071e522ad4(string c178caa51c848fc2a47f1589dec7f06ca)
	{
		return c05c66ad2b870d39fc1dd7d071e522ad4(c178caa51c848fc2a47f1589dec7f06ca, c7c6533c5dc5555fc964fbca37534601c: false);
	}

	internal static string c05c66ad2b870d39fc1dd7d071e522ad4(string c178caa51c848fc2a47f1589dec7f06ca, bool c7c6533c5dc5555fc964fbca37534601c)
	{
		try
		{
			if (c178caa51c848fc2a47f1589dec7f06ca == null)
			{
				return string.Empty;
			}
			byte[] array = ce46fa3377ed2e123f161eedd9455d8b2.ce5f0615654bd80e731ced18842900ade(c178caa51c848fc2a47f1589dec7f06ca, 32771u);
			if (c352aa377b2bb1402ecb10043063d52f1(array))
			{
				while (true)
				{
					switch (7)
					{
					case 0:
						break;
					default:
						if (1 == 0)
						{
							__ldtoken(cf392f57816a6dc1e4f972afdbd253659.c05c66ad2b870d39fc1dd7d071e522ad4(string, bool));
						}
						return null;
					}
				}
			}
			if (c7c6533c5dc5555fc964fbca37534601c)
			{
				Array.Reverse(array);
			}
			string text = new Guid(array).ToString(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9937));
			return text.Replace(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9940), array[0] % 9 + c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9940));
		}
		catch
		{
		}
		return string.Empty;
	}

	internal static string c76f074b1e125ebc4c29a47aa25855d1a(string[] c2974d6571d334ea408d4f793d752d9a9, string[] c1481d31912f7f316f8a54b5312b38bb9)
	{
		string empty = string.Empty;
		StringBuilder stringBuilder = new StringBuilder();
		try
		{
			for (int i = 0; i < c2974d6571d334ea408d4f793d752d9a9.Length; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9943));
				}
				stringBuilder.Append(c2974d6571d334ea408d4f793d752d9a9[i]);
				stringBuilder.Append(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9946));
				stringBuilder.Append(cfbe9a07847976d701e5b63bb7d84ff4e(c1481d31912f7f316f8a54b5312b38bb9[i]));
			}
			return stringBuilder.ToString();
		}
		catch
		{
			return string.Empty;
		}
	}

	internal static string c3fcae289752394af27980438cbe13a99(string ccc1029455150b8fa7c3de95a87f4a442, string cb9b46fead1ceee4ef4cdb4f14fe79477)
	{
		string text = ccc1029455150b8fa7c3de95a87f4a442;
		if (text.EndsWith(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1723)))
		{
			return text + cb9b46fead1ceee4ef4cdb4f14fe79477;
		}
		return text + c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1723) + cb9b46fead1ceee4ef4cdb4f14fe79477;
	}

	internal static string c652b883c92da9d038a7639bfd11eee94(string c178caa51c848fc2a47f1589dec7f06ca)
	{
		StringBuilder stringBuilder = new StringBuilder(c178caa51c848fc2a47f1589dec7f06ca.Length);
		foreach (char value in c178caa51c848fc2a47f1589dec7f06ca)
		{
			if (c4576e9f7171140cd2c0561ba54b62688.IndexOf(value) != -1)
			{
				stringBuilder.Append(value);
			}
		}
		return stringBuilder.ToString();
	}

	internal static byte[] c0bbae15145e91cbda560736d56f7f842(string c178caa51c848fc2a47f1589dec7f06ca)
	{
		if (c178caa51c848fc2a47f1589dec7f06ca != null)
		{
			if (c178caa51c848fc2a47f1589dec7f06ca.Length != 0)
			{
				try
				{
					c178caa51c848fc2a47f1589dec7f06ca = c178caa51c848fc2a47f1589dec7f06ca.Replace(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7712), string.Empty);
					uint num = 0u;
					int num2 = 0;
					byte[] array = new byte[c178caa51c848fc2a47f1589dec7f06ca.Length * 5 / 8];
					int num3 = 0;
					string text = c178caa51c848fc2a47f1589dec7f06ca;
					foreach (char value in text)
					{
						int num4 = cff5b1897f51cbdb93bc719ae4daff3a7.IndexOf(value);
						if (num4 == -1)
						{
							while (true)
							{
								switch (2)
								{
								case 0:
									break;
								default:
									throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9949));
								}
							}
						}
						num |= (uint)(num4 << num2);
						num2 += 5;
						if (num2 >= 8)
						{
							num2 -= 8;
							array[num3++] = (byte)(num & 0xFF);
							num >>= 8;
						}
					}
					while (true)
					{
						switch (4)
						{
						case 0:
							continue;
						}
						return array;
					}
				}
				catch
				{
				}
				return null;
			}
			while (true)
			{
				switch (4)
				{
				case 0:
					continue;
				}
				break;
			}
			if (1 == 0)
			{
				__ldtoken(cf392f57816a6dc1e4f972afdbd253659.c0bbae15145e91cbda560736d56f7f842(string));
			}
		}
		return null;
	}

	internal static void cda5da55414bacea1d1bb122b40dc28cd(byte[] c64f632f47ec42f89161439dfec6e213c)
	{
		for (int i = 0; i < c64f632f47ec42f89161439dfec6e213c.Length; i++)
		{
			c64f632f47ec42f89161439dfec6e213c[i] = (byte)(~c64f632f47ec42f89161439dfec6e213c[i]);
		}
	}

	[ComVisible(false)]
	internal static string[] c270cafdbb64f9447dd033594c9174760(string c178caa51c848fc2a47f1589dec7f06ca, string[] cfb33acf04215dc914ae35d02a227fe7a)
	{
		if (cfb33acf04215dc914ae35d02a227fe7a != null)
		{
			if (cfb33acf04215dc914ae35d02a227fe7a.Length != 0)
			{
				int[] c314052a860260161112274aaba2edb5a = new int[c178caa51c848fc2a47f1589dec7f06ca.Length];
				int[] c158b230e26d3d01adadf87075d2d09fa = new int[c178caa51c848fc2a47f1589dec7f06ca.Length];
				int num = cd732bbc490534524d2450e71ba6668c4(c178caa51c848fc2a47f1589dec7f06ca, cfb33acf04215dc914ae35d02a227fe7a, ref c314052a860260161112274aaba2edb5a, ref c158b230e26d3d01adadf87075d2d09fa);
				if (num == 0)
				{
					return new string[1] { c178caa51c848fc2a47f1589dec7f06ca };
				}
				return cc8979d21076e000abcb003c682c5c834(c178caa51c848fc2a47f1589dec7f06ca, c314052a860260161112274aaba2edb5a, c158b230e26d3d01adadf87075d2d09fa, num, int.MaxValue);
			}
			while (true)
			{
				switch (3)
				{
				case 0:
					continue;
				}
				break;
			}
			if (1 == 0)
			{
				__ldtoken(cf392f57816a6dc1e4f972afdbd253659.c270cafdbb64f9447dd033594c9174760(string, string[]));
			}
		}
		return c178caa51c848fc2a47f1589dec7f06ca.Split(null);
	}

	internal static int cd732bbc490534524d2450e71ba6668c4(string c178caa51c848fc2a47f1589dec7f06ca, string[] c51b85b5c8cbf48aba38eb69eff0b629f, ref int[] c314052a860260161112274aaba2edb5a, ref int[] c158b230e26d3d01adadf87075d2d09fa)
	{
		int num = 0;
		int num2 = c314052a860260161112274aaba2edb5a.Length;
		for (int i = 0; i < c178caa51c848fc2a47f1589dec7f06ca.Length; i++)
		{
			while (true)
			{
				switch (3)
				{
				case 0:
					continue;
				}
				break;
			}
			if (num < num2)
			{
				foreach (string text in c51b85b5c8cbf48aba38eb69eff0b629f)
				{
					if (c352aa377b2bb1402ecb10043063d52f1(text))
					{
						continue;
					}
					int length = text.Length;
					if (c178caa51c848fc2a47f1589dec7f06ca[i] != text[0] || length > c178caa51c848fc2a47f1589dec7f06ca.Length - i)
					{
						continue;
					}
					while (true)
					{
						switch (4)
						{
						case 0:
							continue;
						}
						break;
					}
					if (1 == 0)
					{
						__ldtoken(cf392f57816a6dc1e4f972afdbd253659.cd732bbc490534524d2450e71ba6668c4(string, string[], ref int[], ref int[]));
					}
					if (length != 1)
					{
						while (true)
						{
							switch (1)
							{
							case 0:
								continue;
							}
							break;
						}
						if (string.CompareOrdinal(c178caa51c848fc2a47f1589dec7f06ca, i, text, 0, length) != 0)
						{
							continue;
						}
						while (true)
						{
							switch (5)
							{
							case 0:
								continue;
							}
							break;
						}
					}
					c314052a860260161112274aaba2edb5a[num] = i;
					c158b230e26d3d01adadf87075d2d09fa[num] = length;
					num++;
					i += length - 1;
					break;
				}
				continue;
			}
			while (true)
			{
				switch (3)
				{
				case 0:
					continue;
				}
				break;
			}
			break;
		}
		return num;
	}

	internal static string[] cc8979d21076e000abcb003c682c5c834(string c178caa51c848fc2a47f1589dec7f06ca, int[] c314052a860260161112274aaba2edb5a, int[] c158b230e26d3d01adadf87075d2d09fa, int caa6400129e257ce6e025f7886c784064, int c943673bf4a019321f5f0f6d2dfda2f94)
	{
		int num = 0;
		int num2 = 0;
		c943673bf4a019321f5f0f6d2dfda2f94--;
		int num3 = ((caa6400129e257ce6e025f7886c784064 < c943673bf4a019321f5f0f6d2dfda2f94) ? caa6400129e257ce6e025f7886c784064 : c943673bf4a019321f5f0f6d2dfda2f94);
		string[] array = new string[num3 + 1];
		for (int i = 0; i < num3; i++)
		{
			if (num < c178caa51c848fc2a47f1589dec7f06ca.Length)
			{
				array[num2++] = c178caa51c848fc2a47f1589dec7f06ca.Substring(num, c314052a860260161112274aaba2edb5a[i] - num);
				num = c314052a860260161112274aaba2edb5a[i] + ((c158b230e26d3d01adadf87075d2d09fa == null) ? 1 : c158b230e26d3d01adadf87075d2d09fa[i]);
				continue;
			}
			while (true)
			{
				switch (7)
				{
				case 0:
					continue;
				}
				break;
			}
			if (1 == 0)
			{
				__ldtoken(cf392f57816a6dc1e4f972afdbd253659.cc8979d21076e000abcb003c682c5c834(string, int[], int[], int, int));
			}
			break;
		}
		if (num < c178caa51c848fc2a47f1589dec7f06ca.Length && num3 >= 0)
		{
			while (true)
			{
				switch (3)
				{
				case 0:
					break;
				default:
					array[num2] = c178caa51c848fc2a47f1589dec7f06ca.Substring(num);
					return array;
				}
			}
		}
		if (num2 == num3)
		{
			while (true)
			{
				switch (1)
				{
				case 0:
					continue;
				}
				break;
			}
			array[num2] = string.Empty;
		}
		return array;
	}

	internal static string cbb20da9675067bc12f19be38016fe1f3(byte[] c63b39959d0967016174a65b9ded457eb)
	{
		if (c63b39959d0967016174a65b9ded457eb != null)
		{
			while (true)
			{
				switch (5)
				{
				case 0:
					continue;
				}
				break;
			}
			if (1 == 0)
			{
				__ldtoken(cf392f57816a6dc1e4f972afdbd253659.cbb20da9675067bc12f19be38016fe1f3(byte[]));
			}
			if (c63b39959d0967016174a65b9ded457eb.Length != 0)
			{
				string text = string.Empty;
				uint num = 0u;
				uint num2 = 0u;
				uint num3 = 0u;
				int num4 = 0;
				int num5 = 0;
				while (num5 < c63b39959d0967016174a65b9ded457eb.Length)
				{
					num = c63b39959d0967016174a65b9ded457eb[num5];
					num >>= num4;
					num &= 0x1F;
					num2 = 0u;
					if (num4 - 3 > 0)
					{
						while (true)
						{
							switch (4)
							{
							case 0:
								continue;
							}
							break;
						}
						int num6;
						if (num5 != c63b39959d0967016174a65b9ded457eb.Length - 1)
						{
							while (true)
							{
								switch (7)
								{
								case 0:
									continue;
								}
								break;
							}
							num6 = c63b39959d0967016174a65b9ded457eb[num5 + 1];
						}
						else
						{
							num6 = 0;
						}
						num2 = (uint)num6;
						num2 &= (uint)((1 << num4 - 3) - 1);
						num2 <<= 8 - num4;
					}
					num3 = num + num2;
					text += cff5b1897f51cbdb93bc719ae4daff3a7[(int)num3];
					num4 += 5;
					if (num4 < 8)
					{
						continue;
					}
					while (true)
					{
						switch (3)
						{
						case 0:
							continue;
						}
						break;
					}
					num5++;
					num4 -= 8;
				}
				while (true)
				{
					switch (1)
					{
					case 0:
						continue;
					}
					return text;
				}
			}
			while (true)
			{
				switch (3)
				{
				case 0:
					continue;
				}
				break;
			}
		}
		return string.Empty;
	}

	internal static string cfbe9a07847976d701e5b63bb7d84ff4e(string c178caa51c848fc2a47f1589dec7f06ca)
	{
		if (c178caa51c848fc2a47f1589dec7f06ca == null)
		{
			while (true)
			{
				switch (6)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(cf392f57816a6dc1e4f972afdbd253659.cfbe9a07847976d701e5b63bb7d84ff4e(string));
					}
					return null;
				}
			}
		}
		return cfbe9a07847976d701e5b63bb7d84ff4e(c178caa51c848fc2a47f1589dec7f06ca, Encoding.UTF8);
	}

	internal static string cfbe9a07847976d701e5b63bb7d84ff4e(string c178caa51c848fc2a47f1589dec7f06ca, Encoding c4496d1e28885071f753d5d8c6ed007b6)
	{
		if (c178caa51c848fc2a47f1589dec7f06ca == null)
		{
			while (true)
			{
				switch (5)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(cf392f57816a6dc1e4f972afdbd253659.cfbe9a07847976d701e5b63bb7d84ff4e(string, Encoding));
					}
					return null;
				}
			}
		}
		byte[] array = c4fa132a56ea9b9ac706da14567b72f45(c178caa51c848fc2a47f1589dec7f06ca, c4496d1e28885071f753d5d8c6ed007b6);
		return Encoding.UTF8.GetString(array, 0, array.Length);
	}

	internal static byte[] c4fa132a56ea9b9ac706da14567b72f45(string c178caa51c848fc2a47f1589dec7f06ca, Encoding c4496d1e28885071f753d5d8c6ed007b6)
	{
		if (c178caa51c848fc2a47f1589dec7f06ca == null)
		{
			while (true)
			{
				switch (3)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(cf392f57816a6dc1e4f972afdbd253659.c4fa132a56ea9b9ac706da14567b72f45(string, Encoding));
					}
					return null;
				}
			}
		}
		byte[] bytes = c4496d1e28885071f753d5d8c6ed007b6.GetBytes(c178caa51c848fc2a47f1589dec7f06ca);
		return c6006d547ac777fc4b49d86db3a23fb8c(bytes, 0, bytes.Length, c4e5da4524231d79bd68674719edff6ea: false);
	}

	private static byte[] c6006d547ac777fc4b49d86db3a23fb8c(byte[] ccc469fdc94161fb1dd5588ee3ba07644, int c39291411c9363c8c148a1afc376604a8, int c943673bf4a019321f5f0f6d2dfda2f94, bool c4e5da4524231d79bd68674719edff6ea)
	{
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < c943673bf4a019321f5f0f6d2dfda2f94; i++)
		{
			char c = (char)ccc469fdc94161fb1dd5588ee3ba07644[c39291411c9363c8c148a1afc376604a8 + i];
			if (c == ' ')
			{
				while (true)
				{
					switch (7)
					{
					case 0:
						continue;
					}
					break;
				}
				if (1 == 0)
				{
					__ldtoken(cf392f57816a6dc1e4f972afdbd253659.c6006d547ac777fc4b49d86db3a23fb8c(byte[], int, int, bool));
				}
				num++;
			}
			else
			{
				if (c2953fb2f43be1ce1c1550e8ea52bf775(c))
				{
					continue;
				}
				while (true)
				{
					switch (5)
					{
					case 0:
						continue;
					}
					break;
				}
				num2++;
			}
		}
		while (true)
		{
			switch (1)
			{
			case 0:
				continue;
			}
			if (!c4e5da4524231d79bd68674719edff6ea)
			{
				while (true)
				{
					switch (1)
					{
					case 0:
						continue;
					}
					break;
				}
				if (num == 0 && num2 == 0)
				{
					while (true)
					{
						switch (5)
						{
						case 0:
							break;
						default:
							return ccc469fdc94161fb1dd5588ee3ba07644;
						}
					}
				}
			}
			byte[] array = new byte[c943673bf4a019321f5f0f6d2dfda2f94 + num2 * 2];
			int num3 = 0;
			for (int j = 0; j < c943673bf4a019321f5f0f6d2dfda2f94; j++)
			{
				byte b = ccc469fdc94161fb1dd5588ee3ba07644[c39291411c9363c8c148a1afc376604a8 + j];
				char c2 = (char)b;
				if (c2953fb2f43be1ce1c1550e8ea52bf775(c2))
				{
					array[num3++] = b;
					continue;
				}
				if (c2 == ' ')
				{
					array[num3++] = 43;
					continue;
				}
				array[num3++] = 37;
				array[num3++] = (byte)cadf8676ee4004763782fd75293fe4c8c((b >> 4) & 0xF);
				array[num3++] = (byte)cadf8676ee4004763782fd75293fe4c8c(b & 0xF);
			}
			return array;
		}
	}

	internal static char cadf8676ee4004763782fd75293fe4c8c(int cad632b6eef0c46e2f07e5911266d468e)
	{
		if (cad632b6eef0c46e2f07e5911266d468e <= 9)
		{
			while (true)
			{
				switch (6)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(cf392f57816a6dc1e4f972afdbd253659.cadf8676ee4004763782fd75293fe4c8c(int));
					}
					return (char)(cad632b6eef0c46e2f07e5911266d468e + 48);
				}
			}
		}
		return (char)(cad632b6eef0c46e2f07e5911266d468e - 10 + 97);
	}

	internal static bool c2953fb2f43be1ce1c1550e8ea52bf775(char c00ce235f41d33fdcb1ad86b02e9443b3)
	{
		if ((c00ce235f41d33fdcb1ad86b02e9443b3 < 'a' || c00ce235f41d33fdcb1ad86b02e9443b3 > 'z') && (c00ce235f41d33fdcb1ad86b02e9443b3 < 'A' || c00ce235f41d33fdcb1ad86b02e9443b3 > 'Z'))
		{
			if (c00ce235f41d33fdcb1ad86b02e9443b3 >= '0')
			{
				while (true)
				{
					switch (3)
					{
					case 0:
						continue;
					}
					break;
				}
				if (1 == 0)
				{
					__ldtoken(cf392f57816a6dc1e4f972afdbd253659.c2953fb2f43be1ce1c1550e8ea52bf775(char));
				}
				if (c00ce235f41d33fdcb1ad86b02e9443b3 <= '9')
				{
					goto IL_0031;
				}
			}
			switch (c00ce235f41d33fdcb1ad86b02e9443b3)
			{
			case '_':
				while (true)
				{
					switch (5)
					{
					case 0:
						continue;
					}
					break;
				}
				goto case '!';
			case '!':
			case '\'':
			case '(':
			case ')':
			case '*':
			case '-':
			case '.':
				return true;
			default:
				return false;
			}
		}
		goto IL_0031;
		IL_0031:
		return true;
	}

	internal static string c14d37aa51c856288573eaee4cc09009d(byte[] c805350637a63929ca01460e84a8a8dbd)
	{
		if (c805350637a63929ca01460e84a8a8dbd != null)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < c805350637a63929ca01460e84a8a8dbd.Length; i++)
			{
				stringBuilder.Append(c805350637a63929ca01460e84a8a8dbd[i].ToString(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(10018)));
			}
			while (true)
			{
				switch (6)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(cf392f57816a6dc1e4f972afdbd253659.c14d37aa51c856288573eaee4cc09009d(byte[]));
				}
				return stringBuilder.ToString();
			}
		}
		return string.Empty;
	}

	internal static byte[] cb97a711af3b89552ee0f7b0cf0c0a4d5(string c805350637a63929ca01460e84a8a8dbd)
	{
		byte[] array = new byte[c805350637a63929ca01460e84a8a8dbd.Length / 2];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = byte.Parse(c805350637a63929ca01460e84a8a8dbd.Substring(i * 2, 2), NumberStyles.HexNumber);
		}
		return array;
	}

	internal static bool ce0f1db791659db34570eda261419cc2d(byte[] c402104de3808c009827870e3f63b2f4f, int c0466871c5205eb4657c8ed0a32ff90b9, byte[] cee6a0d6b3eb0adfaf4691515790cd8df, int c4dd115646f36e33eec724f44f90e038a, int cc9318c57b86104b1057fdbd227eb8df4)
	{
		try
		{
			for (int i = 0; i < cc9318c57b86104b1057fdbd227eb8df4; i++)
			{
				if (c402104de3808c009827870e3f63b2f4f[c0466871c5205eb4657c8ed0a32ff90b9 + i] == cee6a0d6b3eb0adfaf4691515790cd8df[c4dd115646f36e33eec724f44f90e038a + i])
				{
					continue;
				}
				while (true)
				{
					switch (3)
					{
					case 0:
						continue;
					}
					if (1 == 0)
					{
						__ldtoken(cf392f57816a6dc1e4f972afdbd253659.ce0f1db791659db34570eda261419cc2d(byte[], int, byte[], int, int));
					}
					return false;
				}
			}
			while (true)
			{
				switch (6)
				{
				case 0:
					continue;
				}
				return true;
			}
		}
		catch
		{
		}
		return false;
	}

	internal static byte[] c249c0850a76c7825792850012b561887(byte[][] cb318f21e496717ff40f4715b560de507)
	{
		int num = 0;
		foreach (byte[] array in cb318f21e496717ff40f4715b560de507)
		{
			num += array.Length;
		}
		byte[] array2 = new byte[num];
		int num2 = 0;
		foreach (byte[] array3 in cb318f21e496717ff40f4715b560de507)
		{
			array3.CopyTo(array2, num2);
			num2 += array3.Length;
		}
		return array2;
	}

	internal static byte[] c0f1e206cfb9cb8bf94687932cf1edbe7(byte[] c63b39959d0967016174a65b9ded457eb)
	{
		byte[] array = new byte[c63b39959d0967016174a65b9ded457eb.Length / 2];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = c63b39959d0967016174a65b9ded457eb[i * 2];
		}
		return array;
	}

	internal static string ceb9d502fa8f46b3c7d576612d6cd871c(string c178caa51c848fc2a47f1589dec7f06ca, string cdc2bf1b8def17e956d10584ae25352c2, int cf5d28afb792fc719850d20d1018cb3f0)
	{
		int i = 0;
		string text = string.Empty;
		for (; i < c178caa51c848fc2a47f1589dec7f06ca.Length; i += cf5d28afb792fc719850d20d1018cb3f0)
		{
			text = text + c178caa51c848fc2a47f1589dec7f06ca.Substring(i, Math.Min(cf5d28afb792fc719850d20d1018cb3f0, c178caa51c848fc2a47f1589dec7f06ca.Length - i)) + cdc2bf1b8def17e956d10584ae25352c2;
		}
		if (text.EndsWith(cdc2bf1b8def17e956d10584ae25352c2))
		{
			while (true)
			{
				switch (7)
				{
				case 0:
					continue;
				}
				break;
			}
			if (1 == 0)
			{
				__ldtoken(cf392f57816a6dc1e4f972afdbd253659.ceb9d502fa8f46b3c7d576612d6cd871c(string, string, int));
			}
			text = text.Substring(0, text.Length - cdc2bf1b8def17e956d10584ae25352c2.Length);
		}
		return text;
	}

	internal static void cb546c497dbff3ffb19c9f530625da985(byte[] caea7c8eeab7dec52f3eabd5619c94714, out byte[] cf5018842beadb7c1014ddb906c7cea5b, out byte[] c7158250eecfdda62371de793c6bc3092, int c39291411c9363c8c148a1afc376604a8)
	{
		cf5018842beadb7c1014ddb906c7cea5b = new byte[c39291411c9363c8c148a1afc376604a8];
		Array.Copy(caea7c8eeab7dec52f3eabd5619c94714, 0, cf5018842beadb7c1014ddb906c7cea5b, 0, c39291411c9363c8c148a1afc376604a8);
		c7158250eecfdda62371de793c6bc3092 = new byte[caea7c8eeab7dec52f3eabd5619c94714.Length - c39291411c9363c8c148a1afc376604a8];
		Array.Copy(caea7c8eeab7dec52f3eabd5619c94714, c39291411c9363c8c148a1afc376604a8, c7158250eecfdda62371de793c6bc3092, 0, c7158250eecfdda62371de793c6bc3092.Length);
	}

	internal static string c81d237d8616a284940335922a9ce2c08(string c178caa51c848fc2a47f1589dec7f06ca)
	{
		if (c178caa51c848fc2a47f1589dec7f06ca.IndexOf(cd8477039016c102e931fa71ca518ff80) != -1)
		{
			return c178caa51c848fc2a47f1589dec7f06ca.Replace(cd8477039016c102e931fa71ca518ff80, c3108a583b9306b5486931a27412f3ebc);
		}
		return c178caa51c848fc2a47f1589dec7f06ca;
	}

	internal static string c88f7306a46baef4e5b876de6a844d292(string c178caa51c848fc2a47f1589dec7f06ca)
	{
		return c178caa51c848fc2a47f1589dec7f06ca.Replace(c3108a583b9306b5486931a27412f3ebc, cd8477039016c102e931fa71ca518ff80);
	}

	internal static void c4346c72b67fc2930303dcca1c2e13fef(byte[] c63b39959d0967016174a65b9ded457eb)
	{
		for (int i = 0; i < c63b39959d0967016174a65b9ded457eb.Length; i++)
		{
			c63b39959d0967016174a65b9ded457eb[i] = (byte)(~c63b39959d0967016174a65b9ded457eb[i]);
		}
	}
}
