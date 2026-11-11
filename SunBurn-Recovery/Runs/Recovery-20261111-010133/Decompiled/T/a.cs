using System;
using System.Globalization;
using System.Text;
using _u0014_;

namespace T;

internal sealed class a
{
	private a()
	{
	}

	private static int lG(byte[] param_0, int param_1)
	{
		return (param_0[param_1 + 3] << 24) | (param_0[param_1 + 2] << 16) | (param_0[param_1 + 1] << 8) | param_0[param_1];
	}

	private static uint lu(byte[] param_0, int param_1)
	{
		return (uint)((param_0[param_1 + 3] << 24) | (param_0[param_1 + 2] << 16) | (param_0[param_1 + 1] << 8) | param_0[param_1]);
	}

	private static byte[] lt(int param_0)
	{
		return new byte[4]
		{
			(byte)(param_0 & 0xFF),
			(byte)((param_0 >> 8) & 0xFF),
			(byte)((param_0 >> 16) & 0xFF),
			(byte)((param_0 >> 24) & 0xFF)
		};
	}

	private static byte[] l4(byte[] param_0)
	{
		for (int i = 0; i < param_0.Length; i++)
		{
			if (param_0[i] != 0)
			{
				byte[] array = new byte[param_0.Length - i];
				Buffer.BlockCopy(param_0, i, array, 0, array.Length);
				return array;
			}
		}
		return null;
	}

	public static _u0014_._u0013_ FromCapiPrivateKeyBlob(byte[] blob)
	{
		return FromCapiPrivateKeyBlob(blob, 0);
	}

	public static _u0014_._u0013_ FromCapiPrivateKeyBlob(byte[] blob, int offset)
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
			if (blob[offset] != 7 || blob[offset + 1] != 2 || blob[offset + 2] != 0 || blob[offset + 3] != 0 || lu(blob, offset + 8) != 843141970)
			{
				throw new _u0014_.L("Invalid blob header");
			}
			int num = lG(blob, offset + 12);
			_u0014_._u0005_ parameters = default;
			byte[] array = new byte[4];
			Buffer.BlockCopy(blob, offset + 16, array, 0, 4);
			Array.Reverse(array);
			parameters.Exponent = l4(array);
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
			_u0014_._u0013_ u0013_ = null;
			try
			{
				u0013_ = _u0014_._u0013_.Create();
				u0013_.ImportParameters(parameters);
			}
			catch (_u0014_.L)
			{
			}
			return u0013_;
		}
		catch (Exception inner)
		{
			throw new _u0014_.L("Invalid blob.", inner);
		}
	}

	public static byte[] ToCapiPrivateKeyBlob(_u0014_._u0013_ rsa)
	{
		_u0014_._u0005_ u0005_ = rsa.ExportParameters(include: true);
		int num = u0005_.Modulus.Length;
		byte[] array = new byte[20 + (num << 2) + (num >> 1)];
		array[0] = 7;
		array[1] = 2;
		array[5] = 36;
		array[8] = 82;
		array[9] = 83;
		array[10] = 65;
		array[11] = 50;
		byte[] array2 = lt(num << 3);
		array[12] = array2[0];
		array[13] = array2[1];
		array[14] = array2[2];
		array[15] = array2[3];
		int num2 = 16;
		int num3 = u0005_.Exponent.Length;
		while (num3 > 0)
		{
			array[num2++] = u0005_.Exponent[--num3];
		}
		num2 = 20;
		byte[] modulus = u0005_.Modulus;
		int num4 = modulus.Length;
		Array.Reverse(modulus, 0, num4);
		Buffer.BlockCopy(modulus, 0, array, num2, num4);
		num2 += num4;
		modulus = u0005_.P;
		num4 = modulus.Length;
		Array.Reverse(modulus, 0, num4);
		Buffer.BlockCopy(modulus, 0, array, num2, num4);
		num2 += num4;
		modulus = u0005_.Q;
		num4 = modulus.Length;
		Array.Reverse(modulus, 0, num4);
		Buffer.BlockCopy(modulus, 0, array, num2, num4);
		num2 += num4;
		modulus = u0005_.DP;
		num4 = modulus.Length;
		Array.Reverse(modulus, 0, num4);
		Buffer.BlockCopy(modulus, 0, array, num2, num4);
		num2 += num4;
		modulus = u0005_.DQ;
		num4 = modulus.Length;
		Array.Reverse(modulus, 0, num4);
		Buffer.BlockCopy(modulus, 0, array, num2, num4);
		num2 += num4;
		modulus = u0005_.InverseQ;
		num4 = modulus.Length;
		Array.Reverse(modulus, 0, num4);
		Buffer.BlockCopy(modulus, 0, array, num2, num4);
		num2 += num4;
		modulus = u0005_.D;
		num4 = modulus.Length;
		Array.Reverse(modulus, 0, num4);
		Buffer.BlockCopy(modulus, 0, array, num2, num4);
		return array;
	}

	public static _u0014_._u0013_ FromCapiPublicKeyBlob(byte[] blob)
	{
		return FromCapiPublicKeyBlob(blob, 0);
	}

	public static _u0014_._u0013_ FromCapiPublicKeyBlob(byte[] blob, int offset)
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
			if (blob[offset] != 6 || blob[offset + 1] != 2 || blob[offset + 2] != 0 || blob[offset + 3] != 0 || lu(blob, offset + 8) != 826364754)
			{
				throw new _u0014_.L("Invalid blob header");
			}
			int num = lG(blob, offset + 12);
			_u0014_._u0005_ parameters = new _u0014_._u0005_
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
			_u0014_._u0013_ u0013_ = null;
			try
			{
				u0013_ = _u0014_._u0013_.Create();
				u0013_.ImportParameters(parameters);
			}
			catch (_u0014_.L)
			{
			}
			return u0013_;
		}
		catch (Exception inner)
		{
			throw new _u0014_.L("Invalid blob.", inner);
		}
	}

	public static byte[] ToCapiPublicKeyBlob(_u0014_._u0013_ rsa)
	{
		_u0014_._u0005_ u0005_ = rsa.ExportParameters(include: false);
		int num = u0005_.Modulus.Length;
		byte[] array = new byte[20 + num];
		array[0] = 6;
		array[1] = 2;
		array[5] = 36;
		array[8] = 82;
		array[9] = 83;
		array[10] = 65;
		array[11] = 49;
		byte[] array2 = lt(num << 3);
		array[12] = array2[0];
		array[13] = array2[1];
		array[14] = array2[2];
		array[15] = array2[3];
		int num2 = 16;
		int num3 = u0005_.Exponent.Length;
		while (num3 > 0)
		{
			array[num2++] = u0005_.Exponent[--num3];
		}
		num2 = 20;
		byte[] modulus = u0005_.Modulus;
		int num4 = modulus.Length;
		Array.Reverse(modulus, 0, num4);
		Buffer.BlockCopy(modulus, 0, array, num2, num4);
		num2 += num4;
		return array;
	}

	public static _u0014_._u0013_ FromCapiKeyBlob(byte[] blob)
	{
		return FromCapiKeyBlob(blob, 0);
	}

	public static _u0014_._u0013_ FromCapiKeyBlob(byte[] blob, int offset)
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
		throw new _u0014_.L("Unknown blob format.");
	}

	public static byte[] ToCapiKeyBlob(_u0014_.a keypair, bool includePrivateKey)
	{
		if (keypair == null)
		{
			throw new ArgumentNullException("keypair");
		}
		if (keypair is _u0014_._u0013_)
		{
			return ToCapiKeyBlob((_u0014_._u0013_)keypair, includePrivateKey);
		}
		return null;
	}

	public static byte[] ToCapiKeyBlob(_u0014_._u0013_ rsa, bool includePrivateKey)
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
		foreach (byte b2 in input)
		{
			stringBuilder.Append(b2.ToString("X2", CultureInfo.InvariantCulture));
		}
		return stringBuilder.ToString();
	}

	private static byte lA(char param_0)
	{
		if (param_0 >= 'a' && param_0 <= 'f')
		{
			return (byte)(param_0 - 97 + 10);
		}
		if (param_0 >= 'A' && param_0 <= 'F')
		{
			return (byte)(param_0 - 65 + 10);
		}
		if (param_0 >= '0' && param_0 <= '9')
		{
			return (byte)(param_0 - 48);
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
			array[num] = (byte)(lA(hex[num2++]) << 4);
			array[num++] += lA(hex[num2++]);
		}
		return array;
	}
}
