using System;
using A;

namespace DigitalRune.Text;

/// <summary>
///       Contains additional string methods.
///       </summary>
public static class StringHelper
{
	/// <summary>
	///       Splits a string that has an integer number as suffix (e.g. "text123") into its components.
	///       </summary>
	/// <param name="str">
	///       A string that has an positive integer number as suffix. The suffix is optional. Examples: "text", 
	///       "text123".
	///       </param>
	/// <param name="text">
	///       The text before the number suffix. "" if <paramref name="str" /> is <see langword="null" /> or
	///       empty.
	///       </param>
	/// <param name="number">
	///       The positive integer number. -1 if <paramref name="str" /> does not contain a suffix.
	///       </param>
	public static void SplitTextAndNumber(this string str, out string text, out int number)
	{
		if (string.IsNullOrEmpty(str))
		{
			text = string.Empty;
			number = -1;
			return;
		}
		if (!char.IsDigit(str, str.Length - 1))
		{
			text = str;
			number = -1;
			return;
		}
		int num = str.Length - 1;
		while (num > 0)
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
			if (1 == 0)
			{
				__ldtoken(StringHelper.SplitTextAndNumber(string, ref string, ref int));
			}
			if (!char.IsLetter(str[num - 1]))
			{
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
			break;
		}
		number = int.Parse(str.Substring(num));
		text = str.Substring(0, num);
	}

	/// <summary>
	///       Computes a value that indicates the match between two strings.
	///       </summary>
	/// <param name="stringA">The first string.</param>
	/// <param name="stringB">The second string.</param>
	/// <returns>
	///       If the strings have nothing in common 0 is returned. If the strings are identical 1 is
	///       returned. Otherwise a value between 0 and 1 is returned that indicates how similar the
	///       strings are.
	///       </returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="stringA" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="stringB" /> is <see langword="null" />.
	///       </exception>
	public static float ComputeMatch(string stringA, string stringB)
	{
		if (stringA == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5692));
		}
		if (stringB == null)
		{
			while (true)
			{
				switch (1)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(StringHelper.ComputeMatch(string, string));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5707));
				}
			}
		}
		int num = Math.Max(stringA.Length, stringB.Length);
		int num2 = 0;
		int num3 = 0;
		float num4 = 0f;
		while (num2 < stringA.Length)
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
			if (num3 < stringB.Length)
			{
				if (stringA[num2] == stringB[num3])
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
					num4 += 1f / (float)num;
					num2++;
					num3++;
					continue;
				}
				if (char.ToLower(stringA[num2]) == char.ToLower(stringB[num3]))
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
					num4 += 0.9f / (float)num;
					num2++;
					num3++;
					continue;
				}
				int num5 = stringA.Length;
				int num6 = stringB.Length;
				int num7 = int.MaxValue;
				int num8 = 0;
				int num9 = 0;
				for (int i = num2; i < stringA.Length; i++)
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
					if (num8 + num9 < num7)
					{
						for (int j = num3; j < stringB.Length; j++)
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
							if (num8 + num9 >= num7)
							{
								break;
							}
							if (char.ToLower(stringA[i]) == char.ToLower(stringB[j]))
							{
								int num10 = num8 + num9;
								if (num10 < num7)
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
									num7 = num10;
									num5 = i;
									num6 = j;
								}
							}
							num9++;
						}
						num8++;
						num9 = 0;
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
					break;
				}
				num2 = num5;
				num3 = num6;
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
			break;
		}
		if (num4 < 0.01f)
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
			num4 = 0f;
		}
		else if (num4 > 0.99f)
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
			num4 = 1f;
		}
		return num4;
	}
}
