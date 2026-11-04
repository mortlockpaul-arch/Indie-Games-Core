using System;
using System.Globalization;
using System.Text;
using A;

namespace DigitalRune.Text;

/// <summary>
///       Static class for string builder extension methods.
///       </summary>
public static class StringBuilderExtensions
{
	private static readonly int[] ce52bc0a84d05efa936040a4f4535d503 = CultureInfo.CurrentCulture.NumberFormat.NumberGroupSizes;

	private static readonly char[] c413ee16b1b1ca0c2a5460ac5b70b0690 = new char[32];

	/// <summary>
	///       Converts an integer number to a string and adds it to string builder.
	///       </summary>
	/// <param name="builder">The string builder.</param>
	/// <param name="number">The number.</param>
	public static void AppendNumber(this StringBuilder builder, int number)
	{
		cd8819c8ab3a9678b5cacf4ac31be8966(builder, number, 0, AppendNumberOptions.None);
	}

	/// <summary>
	///       Converts an integer number to a string and adds it to the string builder.
	///       </summary>
	/// <param name="builder">The string builder.</param>
	/// <param name="number">The number.</param>
	/// <param name="options">The format options.</param>
	public static void AppendNumber(this StringBuilder builder, int number, AppendNumberOptions options)
	{
		cd8819c8ab3a9678b5cacf4ac31be8966(builder, number, 0, options);
	}

	/// <summary>
	///       Converts a float number to a string and adds it to the string builder.
	///       </summary>
	/// <param name="builder">The string builder.</param>
	/// <param name="number">The number.</param>
	public static void AppendNumber(this StringBuilder builder, float number)
	{
		builder.AppendNumber(number, 2, AppendNumberOptions.None);
	}

	/// <summary>
	///       Converts a float number to a string and adds it to the string builder.
	///       </summary>
	/// <param name="builder">The string builder.</param>
	/// <param name="number">The number.</param>
	/// <param name="options">The format options.</param>
	public static void AppendNumber(this StringBuilder builder, float number, AppendNumberOptions options)
	{
		builder.AppendNumber(number, 2, options);
	}

	/// <summary>
	///       Converts a float number to a string and adds it to the string builder.
	///       </summary>
	/// <param name="builder">The string builder.</param>
	/// <param name="number">The number.</param>
	/// <param name="decimalCount">The number of decimal digits to show.</param>
	/// <param name="options">The format options.</param>
	public static void AppendNumber(this StringBuilder builder, float number, int decimalCount, AppendNumberOptions options)
	{
		if (float.IsNaN(number))
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
						__ldtoken(StringBuilderExtensions.AppendNumber(StringBuilder, float, int, AppendNumberOptions));
					}
					builder.Append(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7226));
					return;
				}
			}
		}
		if (float.IsNegativeInfinity(number))
		{
			builder.Append(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7233));
			return;
		}
		if (float.IsPositiveInfinity(number))
		{
			while (true)
			{
				switch (1)
				{
				case 0:
					break;
				default:
					builder.Append(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7252));
					return;
				}
			}
		}
		try
		{
			long cdc3a0259a5e35752fa6f4ae5fd6a959f = checked((long)((double)number * Math.Pow(10.0, decimalCount) + 0.5));
			cd8819c8ab3a9678b5cacf4ac31be8966(builder, cdc3a0259a5e35752fa6f4ae5fd6a959f, decimalCount, options);
		}
		catch (Exception)
		{
			builder.Append(number);
		}
	}

	private static void cd8819c8ab3a9678b5cacf4ac31be8966(StringBuilder c52661634ae10fef5f320aa618b22f78d, long cdc3a0259a5e35752fa6f4ae5fd6a959f, int c3d027711e324f8f09ea7ea56c1e7dd26, AppendNumberOptions ca0e514da4bfe5dfa81019b470469da21)
	{
		NumberFormatInfo numberFormat = CultureInfo.CurrentCulture.NumberFormat;
		int num = c413ee16b1b1ca0c2a5460ac5b70b0690.Length;
		int num2 = num - c3d027711e324f8f09ea7ea56c1e7dd26;
		if (num2 == num)
		{
			num2 = num + 1;
		}
		int num3 = 0;
		int num4 = ce52bc0a84d05efa936040a4f4535d503[num3] + c3d027711e324f8f09ea7ea56c1e7dd26;
		bool flag = (ca0e514da4bfe5dfa81019b470469da21 & AppendNumberOptions.NumberGroup) != 0;
		bool flag2 = (ca0e514da4bfe5dfa81019b470469da21 & AppendNumberOptions.PositiveSign) != 0;
		bool flag3 = cdc3a0259a5e35752fa6f4ae5fd6a959f < 0;
		cdc3a0259a5e35752fa6f4ae5fd6a959f = Math.Abs(cdc3a0259a5e35752fa6f4ae5fd6a959f);
		while (true)
		{
			if (num == num2)
			{
				while (true)
				{
					switch (2)
					{
					case 0:
						continue;
					}
					break;
				}
				if (1 == 0)
				{
					__ldtoken(StringBuilderExtensions.cd8819c8ab3a9678b5cacf4ac31be8966(StringBuilder, long, int, AppendNumberOptions));
				}
				c413ee16b1b1ca0c2a5460ac5b70b0690[--num] = numberFormat.NumberDecimalSeparator[0];
			}
			if (--num4 < 0 && flag)
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
				c413ee16b1b1ca0c2a5460ac5b70b0690[--num] = numberFormat.NumberGroupSeparator[0];
				if (num3 < ce52bc0a84d05efa936040a4f4535d503.Length - 1)
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
					num3++;
				}
				num4 = ce52bc0a84d05efa936040a4f4535d503[num3] - 1;
			}
			c413ee16b1b1ca0c2a5460ac5b70b0690[--num] = (char)(48 + cdc3a0259a5e35752fa6f4ae5fd6a959f % 10);
			cdc3a0259a5e35752fa6f4ae5fd6a959f /= 10;
			if (cdc3a0259a5e35752fa6f4ae5fd6a959f > 0)
			{
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
			if (num2 > num)
			{
				break;
			}
		}
		if (flag3)
		{
			c413ee16b1b1ca0c2a5460ac5b70b0690[--num] = numberFormat.NegativeSign[0];
		}
		else if (flag2)
		{
			c413ee16b1b1ca0c2a5460ac5b70b0690[--num] = numberFormat.PositiveSign[0];
		}
		c52661634ae10fef5f320aa618b22f78d.Append(c413ee16b1b1ca0c2a5460ac5b70b0690, num, c413ee16b1b1ca0c2a5460ac5b70b0690.Length - num);
	}

	public static void Clear(this StringBuilder builder)
	{
		builder.Remove(0, builder.Length);
	}
}
