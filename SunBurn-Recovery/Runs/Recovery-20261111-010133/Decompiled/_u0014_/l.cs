using System;

namespace _u0014_;

internal class l
{
	private const string l8a = "1.3.14.3.2.26";

	private static object l8_u0013_;

	static l()
	{
		l8_u0013_ = new object();
	}

	private static void lH()
	{
	}

	private static byte[] l8(long param_0)
	{
		if (param_0 > int.MaxValue || param_0 < int.MinValue)
		{
			throw new OverflowException("Part of OID doesn't fit in Int32");
		}
		long num = param_0;
		int num2 = 1;
		while (num > 127)
		{
			num >>= 7;
			num2++;
		}
		byte[] array = new byte[num2];
		for (int i = 0; i < num2; i++)
		{
			num = param_0 >> 7 * i;
			num &= 0x7F;
			if (i != 0)
			{
				num += 128;
			}
			array[num2 - i - 1] = Convert.ToByte(num);
		}
		return array;
	}

	public static byte[] EncodeOID(string str)
	{
		char[] separator = new char[1] { '.' };
		string[] array = str.Split(separator);
		if (array.Length < 2)
		{
			throw new w("OID must have at least two parts");
		}
		byte[] array2 = new byte[str.Length];
		try
		{
			byte b2 = Convert.ToByte(array[0]);
			byte b3 = Convert.ToByte(array[1]);
			array2[2] = Convert.ToByte(b2 * 40 + b3);
		}
		catch
		{
			throw new w("Invalid OID");
		}
		int num = 3;
		for (int i = 2; i < array.Length; i++)
		{
			long num2 = Convert.ToInt64(array[i]);
			if (num2 > 127)
			{
				byte[] array3 = l8(num2);
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
			throw new w("OID > 127 bytes");
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
internal class L : SystemException
{
	public L()
		: base("Error occured during a cryptographic operation.")
	{
		HResult = -2146233296;
	}

	public L(int hr)
	{
		HResult = hr;
	}

	public L(string message)
		: base(message)
	{
		HResult = -2146233296;
	}

	public L(string message, Exception inner)
		: base(message, inner)
	{
		HResult = -2146233296;
	}

	public L(string format, string insert)
		: base(string.Format(format, insert))
	{
		HResult = -2146233296;
	}
}
