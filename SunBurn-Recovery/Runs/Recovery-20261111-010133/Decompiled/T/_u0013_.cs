using System;
using E;
using _u0014_;

namespace T;

internal sealed class _u0013_
{
	private static byte[] l8a = new byte[20]
	{
		218, 57, 163, 238, 94, 107, 75, 13, 50, 85,
		191, 239, 149, 96, 24, 144, 175, 216, 7, 9
	};

	private _u0013_()
	{
	}

	private static bool lg(byte[] param_0, byte[] param_1)
	{
		bool flag = param_0.Length == param_1.Length;
		if (flag)
		{
			for (int i = 0; i < param_0.Length; i++)
			{
				if (param_0[i] != param_1[i])
				{
					return false;
				}
			}
		}
		return flag;
	}

	private static byte[] l_u0006_(byte[] param_0, byte[] param_1)
	{
		byte[] array = new byte[param_0.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = (byte)(param_0[i] ^ param_1[i]);
		}
		return array;
	}

	private static byte[] le(_u0014_.B param_0)
	{
		if (param_0 is _u0014_.I)
		{
			return l8a;
		}
		return param_0.ComputeHash((byte[])null);
	}

	public static byte[] I2OSP(int x, int size)
	{
		byte[] bytes = BitConverter.GetBytes(x);
		return I2OSP(bytes, size);
	}

	public static byte[] I2OSP(byte[] x, int size)
	{
		byte[] array = new byte[size];
		Buffer.BlockCopy(x, 0, array, array.Length - x.Length, x.Length);
		return array;
	}

	public static byte[] OS2IP(byte[] x)
	{
		int num = 0;
		while (x[num++] == 0 && num < x.Length)
		{
		}
		num--;
		if (num > 0)
		{
			byte[] array = new byte[x.Length - num];
			Buffer.BlockCopy(x, num, array, 0, array.Length);
			return array;
		}
		return x;
	}

	public static byte[] RSAEP(_u0014_._u0013_ rsa, byte[] m)
	{
		return rsa.EncryptValue(m);
	}

	public static byte[] RSADP(_u0014_._u0013_ rsa, byte[] c)
	{
		return rsa.DecryptValue(c);
	}

	public static byte[] RSASP1(_u0014_._u0013_ rsa, byte[] m)
	{
		return rsa.DecryptValue(m);
	}

	public static byte[] RSAVP1(_u0014_._u0013_ rsa, byte[] s)
	{
		return rsa.EncryptValue(s);
	}

	public static byte[] Encrypt_OAEP(_u0014_._u0013_ rsa, _u0014_.B hash, _u0014_.h rng, byte[] M)
	{
		int num = rsa.KeySize / 8;
		int num2 = hash.HashSize / 8;
		if (M.Length > num - 2 * num2 - 2)
		{
			throw new _u0014_.L("message too long");
		}
		byte[] array = le(hash);
		int num3 = num - M.Length - 2 * num2 - 2;
		byte[] array2 = new byte[array.Length + num3 + 1 + M.Length];
		Buffer.BlockCopy(array, 0, array2, 0, array.Length);
		array2[array.Length + num3] = 1;
		Buffer.BlockCopy(M, 0, array2, array2.Length - M.Length, M.Length);
		byte[] array3 = new byte[num2];
		rng.GetBytes(array3);
		byte[] param_ = MGF1(hash, array3, num - num2 - 1);
		byte[] array4 = l_u0006_(array2, param_);
		byte[] param_2 = MGF1(hash, array4, num2);
		byte[] array5 = l_u0006_(array3, param_2);
		byte[] array6 = new byte[array5.Length + array4.Length + 1];
		Buffer.BlockCopy(array5, 0, array6, 1, array5.Length);
		Buffer.BlockCopy(array4, 0, array6, array5.Length + 1, array4.Length);
		byte[] m = OS2IP(array6);
		byte[] x = RSAEP(rsa, m);
		return I2OSP(x, num);
	}

	public static byte[] Decrypt_OAEP(_u0014_._u0013_ rsa, _u0014_.B hash, byte[] C)
	{
		int num = rsa.KeySize / 8;
		int num2 = hash.HashSize / 8;
		if (num < 2 * num2 + 2 || C.Length != num)
		{
			throw new _u0014_.L("decryption error");
		}
		byte[] c = OS2IP(C);
		byte[] x = RSADP(rsa, c);
		byte[] array = I2OSP(x, num);
		byte[] array2 = new byte[num2];
		Buffer.BlockCopy(array, 1, array2, 0, array2.Length);
		byte[] array3 = new byte[num - num2 - 1];
		Buffer.BlockCopy(array, array.Length - array3.Length, array3, 0, array3.Length);
		byte[] param_ = MGF1(hash, array3, num2);
		byte[] mgfSeed = l_u0006_(array2, param_);
		byte[] param_2 = MGF1(hash, mgfSeed, num - num2 - 1);
		byte[] array4 = l_u0006_(array3, param_2);
		byte[] array5 = le(hash);
		byte[] array6 = new byte[array5.Length];
		Buffer.BlockCopy(array4, 0, array6, 0, array6.Length);
		bool flag = lg(array5, array6);
		int i;
		for (i = array5.Length; array4[i] == 0; i++)
		{
		}
		int num3 = array4.Length - i - 1;
		byte[] array7 = new byte[num3];
		Buffer.BlockCopy(array4, i + 1, array7, 0, num3);
		if (array[0] != 0 || !flag || array4[i] != 1)
		{
			return null;
		}
		return array7;
	}

	public static byte[] Encrypt_v15(_u0014_._u0013_ rsa, _u0014_.h rng, byte[] M)
	{
		int num = rsa.KeySize / 8;
		if (M.Length > num - 11)
		{
			throw new _u0014_.L("message too long");
		}
		int num2 = Math.Max(8, num - M.Length - 3);
		byte[] array = new byte[num2];
		rng.GetNonZeroBytes(array);
		byte[] array2 = new byte[num];
		array2[1] = 2;
		Buffer.BlockCopy(array, 0, array2, 2, num2);
		Buffer.BlockCopy(M, 0, array2, num - M.Length, M.Length);
		byte[] m = OS2IP(array2);
		byte[] x = RSAEP(rsa, m);
		return I2OSP(x, num);
	}

	public static byte[] Decrypt_v15(_u0014_._u0013_ rsa, byte[] C)
	{
		int num = rsa.KeySize >> 3;
		if (num < 11 || C.Length > num)
		{
			throw new _u0014_.L("decryption error");
		}
		byte[] c = OS2IP(C);
		byte[] x = RSADP(rsa, c);
		byte[] array = I2OSP(x, num);
		if (array[0] != 0 || array[1] != 2)
		{
			return null;
		}
		int i;
		for (i = 10; array[i] != 0 && i < array.Length; i++)
		{
		}
		if (array[i] != 0)
		{
			return null;
		}
		i++;
		byte[] array2 = new byte[array.Length - i];
		Buffer.BlockCopy(array, i, array2, 0, array2.Length);
		return array2;
	}

	public static byte[] Sign_v15(_u0014_._u0013_ rsa, _u0014_.B hash, byte[] hashValue)
	{
		int num = rsa.KeySize >> 3;
		byte[] x = Encode_v15(hash, hashValue, num);
		byte[] m = OS2IP(x);
		byte[] x2 = RSASP1(rsa, m);
		return I2OSP(x2, num);
	}

	public static bool Verify_v15(_u0014_._u0013_ rsa, _u0014_.B hash, byte[] hashValue, byte[] signature)
	{
		return Verify_v15(rsa, hash, hashValue, signature, tryNonStandardEncoding: false);
	}

	public static bool Verify_v15(_u0014_._u0013_ rsa, _u0014_.B hash, byte[] hashValue, byte[] signature, bool tryNonStandardEncoding)
	{
		int num = rsa.KeySize >> 3;
		byte[] array = OS2IP(signature);
		byte[] x = RSAVP1(rsa, array);
		byte[] array2 = I2OSP(x, num);
		byte[] param_ = Encode_v15(hash, hashValue, num);
		bool flag = lg(param_, array2);
		if (flag || !tryNonStandardEncoding)
		{
			return flag;
		}
		if (array2[0] != 0 || array2[1] != 1)
		{
			return false;
		}
		int i;
		for (i = 2; i < array2.Length - hashValue.Length - 1; i++)
		{
			if (array2[i] != byte.MaxValue)
			{
				return false;
			}
		}
		if (array2[i++] != 0)
		{
			return false;
		}
		byte[] array3 = new byte[hashValue.Length];
		Buffer.BlockCopy(array2, i, array3, 0, array3.Length);
		return lg(array3, hashValue);
	}

	public static byte[] Encode_v15(_u0014_.B hash, byte[] hashValue, int emLength)
	{
		if (hashValue.Length != hash.HashSize >> 3)
		{
			throw new _u0014_.L("bad hash length for " + hash.ToString());
		}
		byte[] array = null;
		string text = _u0014_.l.MapNameToOID(hash.ToString());
		if (text != null)
		{
			E.a a2 = new E.a(48);
			a2.Add(new E.a(_u0014_.l.EncodeOID(text)));
			a2.Add(new E.a(5));
			E.a asn = new E.a(4, hashValue);
			E.a a3 = new E.a(48);
			a3.Add(a2);
			a3.Add(asn);
			array = a3.GetBytes();
		}
		else
		{
			array = hashValue;
		}
		Buffer.BlockCopy(hashValue, 0, array, array.Length - hashValue.Length, hashValue.Length);
		int num = Math.Max(8, emLength - array.Length - 3);
		byte[] array2 = new byte[num + array.Length + 3];
		array2[1] = 1;
		for (int i = 2; i < num + 2; i++)
		{
			array2[i] = byte.MaxValue;
		}
		Buffer.BlockCopy(array, 0, array2, num + 3, array.Length);
		return array2;
	}

	public static byte[] MGF1(_u0014_.B hash, byte[] mgfSeed, int maskLen)
	{
		if (maskLen < 0)
		{
			throw new OverflowException();
		}
		int num = mgfSeed.Length;
		int num2 = hash.HashSize >> 3;
		int num3 = maskLen / num2;
		if (maskLen % num2 != 0)
		{
			num3++;
		}
		byte[] array = new byte[num3 * num2];
		byte[] array2 = new byte[num + 4];
		int num4 = 0;
		for (int i = 0; i < num3; i++)
		{
			byte[] src = I2OSP(i, 4);
			Buffer.BlockCopy(mgfSeed, 0, array2, 0, num);
			Buffer.BlockCopy(src, 0, array2, num, 4);
			byte[] src2 = hash.ComputeHash(array2);
			Buffer.BlockCopy(src2, 0, array, num4, num2);
			num4 += num;
		}
		byte[] array3 = new byte[maskLen];
		Buffer.BlockCopy(array, 0, array3, 0, maskLen);
		return array3;
	}
}
