using System;
using System.Globalization;
using System.Text;
using _0011;

namespace s;

internal sealed class B
{
	private B()
	{
	}

	private static int _0003g(byte[] P_0, int P_1)
	{
		return (P_0[P_1 + 3] << 24) | (P_0[P_1 + 2] << 16) | (P_0[P_1 + 1] << 8) | P_0[P_1];
	}

	private static uint _0003f(byte[] P_0, int P_1)
	{
		return (uint)((P_0[P_1 + 3] << 24) | (P_0[P_1 + 2] << 16) | (P_0[P_1 + 1] << 8) | P_0[P_1]);
	}

	private static byte[] _0003_0003(int P_0)
	{
		return new byte[4]
		{
			(byte)(P_0 & 0xFF),
			(byte)((P_0 >> 8) & 0xFF),
			(byte)((P_0 >> 16) & 0xFF),
			(byte)((P_0 >> 24) & 0xFF)
		};
	}

	private static byte[] _0003n(byte[] P_0)
	{
		for (int k = 0; k < P_0.Length; k++)
		{
			if (P_0[k] != 0)
			{
				byte[] array = new byte[P_0.Length - k];
				Buffer.BlockCopy(P_0, k, array, 0, array.Length);
				return array;
			}
		}
		return null;
	}

	public static _0011.a FromCapiPrivateKeyBlob(byte[] blob)
	{
		return FromCapiPrivateKeyBlob(blob, 0);
	}

	public static _0011.a FromCapiPrivateKeyBlob(byte[] blob, int offset)
	{
		if (blob == null)
		{
			throw new ArgumentNullException("blob");
		}
		if (offset >= blob.Length)
		{
			throw new ArgumentException("blob is too small.");
		}
		try
		{
			if (blob[offset] != 7 || blob[offset + 1] != 2 || blob[offset + 2] != 0 || blob[offset + 3] != 0 || _0003f(blob, offset + 8) != 843141970)
			{
				throw new _0011._0003("Invalid blob header");
			}
			int num = _0003g(blob, offset + 12);
			_0011._0005 parameters = default;
			byte[] array = new byte[4];
			Buffer.BlockCopy(blob, offset + 16, array, 0, 4);
			Array.Reverse(array);
			parameters.Exponent = _0003n(array);
			int num2 = offset + 20;
			int num3 = num >> 3;
			parameters.Modulus = new byte[num3];
			Buffer.BlockCopy(blob, num2, parameters.Modulus, 0, num3);
			Array.Reverse(parameters.Modulus);
			num2 += num3;
			int num4 = num3 >> 1;
			parameters.P = new byte[num4];
			Buffer.BlockCopy(blob, num2, parameters.P, 0, num4);
			Array.Reverse(parameters.P);
			num2 += num4;
			parameters.Q = new byte[num4];
			Buffer.BlockCopy(blob, num2, parameters.Q, 0, num4);
			Array.Reverse(parameters.Q);
			num2 += num4;
			parameters.DP = new byte[num4];
			Buffer.BlockCopy(blob, num2, parameters.DP, 0, num4);
			Array.Reverse(parameters.DP);
			num2 += num4;
			parameters.DQ = new byte[num4];
			Buffer.BlockCopy(blob, num2, parameters.DQ, 0, num4);
			Array.Reverse(parameters.DQ);
			num2 += num4;
			parameters.InverseQ = new byte[num4];
			Buffer.BlockCopy(blob, num2, parameters.InverseQ, 0, num4);
			Array.Reverse(parameters.InverseQ);
			num2 += num4;
			parameters.D = new byte[num3];
			if (num2 + num3 + offset <= blob.Length)
			{
				Buffer.BlockCopy(blob, num2, parameters.D, 0, num3);
				Array.Reverse(parameters.D);
			}
			_0011.a a2 = null;
			try
			{
				a2 = _0011.a.Create();
				a2.ImportParameters(parameters);
			}
			catch (_0011._0003)
			{
			}
			return a2;
		}
		catch (Exception inner)
		{
			throw new _0011._0003("Invalid blob.", inner);
		}
	}

	public static byte[] ToCapiPrivateKeyBlob(_0011.a rsa)
	{
		_0011._0005 obj = rsa.ExportParameters(include: true);
		int num = obj.Modulus.Length;
		byte[] array = new byte[20 + (num << 2) + (num >> 1)];
		array[0] = 7;
		array[1] = 2;
		array[5] = 36;
		array[8] = 82;
		array[9] = 83;
		array[10] = 65;
		array[11] = 50;
		byte[] array2 = _0003_0003(num << 3);
		array[12] = array2[0];
		array[13] = array2[1];
		array[14] = array2[2];
		array[15] = array2[3];
		int num2 = 16;
		int num3 = obj.Exponent.Length;
		while (num3 > 0)
		{
			array[num2++] = obj.Exponent[--num3];
		}
		num2 = 20;
		byte[] modulus = obj.Modulus;
		int num4 = modulus.Length;
		Array.Reverse(modulus, 0, num4);
		Buffer.BlockCopy(modulus, 0, array, num2, num4);
		num2 += num4;
		modulus = obj.P;
		num4 = modulus.Length;
		Array.Reverse(modulus, 0, num4);
		Buffer.BlockCopy(modulus, 0, array, num2, num4);
		num2 += num4;
		modulus = obj.Q;
		num4 = modulus.Length;
		Array.Reverse(modulus, 0, num4);
		Buffer.BlockCopy(modulus, 0, array, num2, num4);
		num2 += num4;
		modulus = obj.DP;
		num4 = modulus.Length;
		Array.Reverse(modulus, 0, num4);
		Buffer.BlockCopy(modulus, 0, array, num2, num4);
		num2 += num4;
		modulus = obj.DQ;
		num4 = modulus.Length;
		Array.Reverse(modulus, 0, num4);
		Buffer.BlockCopy(modulus, 0, array, num2, num4);
		num2 += num4;
		modulus = obj.InverseQ;
		num4 = modulus.Length;
		Array.Reverse(modulus, 0, num4);
		Buffer.BlockCopy(modulus, 0, array, num2, num4);
		num2 += num4;
		modulus = obj.D;
		num4 = modulus.Length;
		Array.Reverse(modulus, 0, num4);
		Buffer.BlockCopy(modulus, 0, array, num2, num4);
		return array;
	}

	public static _0011.a FromCapiPublicKeyBlob(byte[] blob)
	{
		return FromCapiPublicKeyBlob(blob, 0);
	}

	public static _0011.a FromCapiPublicKeyBlob(byte[] blob, int offset)
	{
		if (blob == null)
		{
			throw new ArgumentNullException("blob");
		}
		if (offset >= blob.Length)
		{
			throw new ArgumentException("blob is too small.");
		}
		try
		{
			if (blob[offset] != 6 || blob[offset + 1] != 2 || blob[offset + 2] != 0 || blob[offset + 3] != 0 || _0003f(blob, offset + 8) != 826364754)
			{
				throw new _0011._0003("Invalid blob header");
			}
			int num = _0003g(blob, offset + 12);
			_0011._0005 parameters = new _0011._0005
			{
				Exponent = new byte[3]
			};
			parameters.Exponent[0] = blob[offset + 18];
			parameters.Exponent[1] = blob[offset + 17];
			parameters.Exponent[2] = blob[offset + 16];
			int srcOffset = offset + 20;
			int num2 = num >> 3;
			parameters.Modulus = new byte[num2];
			Buffer.BlockCopy(blob, srcOffset, parameters.Modulus, 0, num2);
			Array.Reverse(parameters.Modulus);
			_0011.a a2 = null;
			try
			{
				a2 = _0011.a.Create();
				a2.ImportParameters(parameters);
			}
			catch (_0011._0003)
			{
			}
			return a2;
		}
		catch (Exception inner)
		{
			throw new _0011._0003("Invalid blob.", inner);
		}
	}

	public static byte[] ToCapiPublicKeyBlob(_0011.a rsa)
	{
		_0011._0005 obj = rsa.ExportParameters(include: false);
		int num = obj.Modulus.Length;
		byte[] array = new byte[20 + num];
		array[0] = 6;
		array[1] = 2;
		array[5] = 36;
		array[8] = 82;
		array[9] = 83;
		array[10] = 65;
		array[11] = 49;
		byte[] array2 = _0003_0003(num << 3);
		array[12] = array2[0];
		array[13] = array2[1];
		array[14] = array2[2];
		array[15] = array2[3];
		int num2 = 16;
		int num3 = obj.Exponent.Length;
		while (num3 > 0)
		{
			array[num2++] = obj.Exponent[--num3];
		}
		num2 = 20;
		byte[] modulus = obj.Modulus;
		int num4 = modulus.Length;
		Array.Reverse(modulus, 0, num4);
		Buffer.BlockCopy(modulus, 0, array, num2, num4);
		num2 += num4;
		return array;
	}

	public static _0011.a FromCapiKeyBlob(byte[] blob)
	{
		return FromCapiKeyBlob(blob, 0);
	}

	public static _0011.a FromCapiKeyBlob(byte[] blob, int offset)
	{
		if (blob == null)
		{
			throw new ArgumentNullException("blob");
		}
		if (offset >= blob.Length)
		{
			throw new ArgumentException("blob is too small.");
		}
		switch (blob[offset])
		{
		case 0:
			if (blob[offset + 12] == 6)
			{
				return FromCapiPublicKeyBlob(blob, offset + 12);
			}
			break;
		case 6:
			return FromCapiPublicKeyBlob(blob, offset);
		case 7:
			return FromCapiPrivateKeyBlob(blob, offset);
		}
		throw new _0011._0003("Unknown blob format.");
	}

	public static byte[] ToCapiKeyBlob(_0011.B keypair, bool includePrivateKey)
	{
		if (keypair == null)
		{
			throw new ArgumentNullException("keypair");
		}
		if (keypair is _0011.a)
		{
			return ToCapiKeyBlob((_0011.a)keypair, includePrivateKey);
		}
		return null;
	}

	public static byte[] ToCapiKeyBlob(_0011.a rsa, bool includePrivateKey)
	{
		if (rsa == null)
		{
			throw new ArgumentNullException("rsa");
		}
		if (includePrivateKey)
		{
			return ToCapiPrivateKeyBlob(rsa);
		}
		return ToCapiPublicKeyBlob(rsa);
	}

	public static string ToHex(byte[] input)
	{
		if (input == null)
		{
			return null;
		}
		StringBuilder stringBuilder = new StringBuilder(input.Length * 2);
		foreach (byte b in input)
		{
			stringBuilder.Append(b.ToString("X2", CultureInfo.InvariantCulture));
		}
		return stringBuilder.ToString();
	}

	private static byte _0003_0019(char P_0)
	{
		if (P_0 >= 'a' && P_0 <= 'f')
		{
			return (byte)(P_0 - 97 + 10);
		}
		if (P_0 >= 'A' && P_0 <= 'F')
		{
			return (byte)(P_0 - 65 + 10);
		}
		if (P_0 >= '0' && P_0 <= '9')
		{
			return (byte)(P_0 - 48);
		}
		throw new ArgumentException("invalid hex char");
	}

	public static byte[] FromHex(string hex)
	{
		if (hex == null)
		{
			return null;
		}
		if ((hex.Length & 1) == 1)
		{
			throw new ArgumentException("Length must be a multiple of 2");
		}
		byte[] array = new byte[hex.Length >> 1];
		int num = 0;
		int num2 = 0;
		while (num < array.Length)
		{
			array[num] = (byte)(_0003_0019(hex[num2++]) << 4);
			array[num++] += _0003_0019(hex[num2++]);
		}
		return array;
	}
}
