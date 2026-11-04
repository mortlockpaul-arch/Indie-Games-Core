using System;
using _0011;
using Q;

namespace s;

internal sealed class a
{
	private static byte[] fNB = new byte[20]
	{
		218, 57, 163, 238, 94, 107, 75, 13, 50, 85,
		191, 239, 149, 96, 24, 144, 175, 216, 7, 9
	};

	private a()
	{
	}

	private static bool _0003L(byte[] P_0, byte[] P_1)
	{
		bool flag = P_0.Length == P_1.Length;
		if (flag)
		{
			for (int k = 0; k < P_0.Length; k++)
			{
				if (P_0[k] != P_1[k])
				{
					return false;
				}
			}
		}
		return flag;
	}

	private static byte[] _0003v(byte[] P_0, byte[] P_1)
	{
		byte[] array = new byte[P_0.Length];
		for (int k = 0; k < array.Length; k++)
		{
			array[k] = (byte)(P_0[k] ^ P_1[k]);
		}
		return array;
	}

	private static byte[] _0003_0012(_0011.L P_0)
	{
		if (P_0 is _0011.o)
		{
			return fNB;
		}
		return P_0.ComputeHash((byte[])null);
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

	public static byte[] RSAEP(_0011.a rsa, byte[] m)
	{
		return rsa.EncryptValue(m);
	}

	public static byte[] RSADP(_0011.a rsa, byte[] c)
	{
		return rsa.DecryptValue(c);
	}

	public static byte[] RSASP1(_0011.a rsa, byte[] m)
	{
		return rsa.DecryptValue(m);
	}

	public static byte[] RSAVP1(_0011.a rsa, byte[] s)
	{
		return rsa.EncryptValue(s);
	}

	public static byte[] Encrypt_OAEP(_0011.a rsa, _0011.L hash, _0011._0012 rng, byte[] M)
	{
		int num = rsa.KeySize / 8;
		int num2 = hash.HashSize / 8;
		if (M.Length > num - 2 * num2 - 2)
		{
			throw new _0011._0003("message too long");
		}
		byte[] array = _0003_0012(hash);
		int num3 = num - M.Length - 2 * num2 - 2;
		byte[] array2 = new byte[array.Length + num3 + 1 + M.Length];
		Buffer.BlockCopy(array, 0, array2, 0, array.Length);
		array2[array.Length + num3] = 1;
		Buffer.BlockCopy(M, 0, array2, array2.Length - M.Length, M.Length);
		byte[] array3 = new byte[num2];
		rng.GetBytes(array3);
		byte[] array4 = MGF1(hash, array3, num - num2 - 1);
		byte[] array5 = _0003v(array2, array4);
		byte[] array6 = MGF1(hash, array5, num2);
		byte[] array7 = _0003v(array3, array6);
		byte[] array8 = new byte[array7.Length + array5.Length + 1];
		Buffer.BlockCopy(array7, 0, array8, 1, array7.Length);
		Buffer.BlockCopy(array5, 0, array8, array7.Length + 1, array5.Length);
		byte[] m = OS2IP(array8);
		byte[] x = RSAEP(rsa, m);
		return I2OSP(x, num);
	}

	public static byte[] Decrypt_OAEP(_0011.a rsa, _0011.L hash, byte[] C)
	{
		int num = rsa.KeySize / 8;
		int num2 = hash.HashSize / 8;
		if (num < 2 * num2 + 2 || C.Length != num)
		{
			throw new _0011._0003("decryption error");
		}
		byte[] array = OS2IP(C);
		byte[] x = RSADP(rsa, array);
		byte[] array2 = I2OSP(x, num);
		byte[] array3 = new byte[num2];
		Buffer.BlockCopy(array2, 1, array3, 0, array3.Length);
		byte[] array4 = new byte[num - num2 - 1];
		Buffer.BlockCopy(array2, array2.Length - array4.Length, array4, 0, array4.Length);
		byte[] array5 = MGF1(hash, array4, num2);
		byte[] mgfSeed = _0003v(array3, array5);
		byte[] array6 = MGF1(hash, mgfSeed, num - num2 - 1);
		byte[] array7 = _0003v(array4, array6);
		byte[] array8 = _0003_0012(hash);
		byte[] array9 = new byte[array8.Length];
		Buffer.BlockCopy(array7, 0, array9, 0, array9.Length);
		bool flag = _0003L(array8, array9);
		int k;
		for (k = array8.Length; array7[k] == 0; k++)
		{
		}
		int num3 = array7.Length - k - 1;
		byte[] array10 = new byte[num3];
		Buffer.BlockCopy(array7, k + 1, array10, 0, num3);
		if (array2[0] != 0 || !flag || array7[k] != 1)
		{
			return null;
		}
		return array10;
	}

	public static byte[] Encrypt_v15(_0011.a rsa, _0011._0012 rng, byte[] M)
	{
		int num = rsa.KeySize / 8;
		if (M.Length > num - 11)
		{
			throw new _0011._0003("message too long");
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

	public static byte[] Decrypt_v15(_0011.a rsa, byte[] C)
	{
		int num = rsa.KeySize >> 3;
		if (num < 11 || C.Length > num)
		{
			throw new _0011._0003("decryption error");
		}
		byte[] array = OS2IP(C);
		byte[] x = RSADP(rsa, array);
		byte[] array2 = I2OSP(x, num);
		if (array2[0] != 0 || array2[1] != 2)
		{
			return null;
		}
		int k;
		for (k = 10; array2[k] != 0 && k < array2.Length; k++)
		{
		}
		if (array2[k] != 0)
		{
			return null;
		}
		k++;
		byte[] array3 = new byte[array2.Length - k];
		Buffer.BlockCopy(array2, k, array3, 0, array3.Length);
		return array3;
	}

	public static byte[] Sign_v15(_0011.a rsa, _0011.L hash, byte[] hashValue)
	{
		int num = rsa.KeySize >> 3;
		byte[] x = Encode_v15(hash, hashValue, num);
		byte[] m = OS2IP(x);
		byte[] x2 = RSASP1(rsa, m);
		return I2OSP(x2, num);
	}

	public static bool Verify_v15(_0011.a rsa, _0011.L hash, byte[] hashValue, byte[] signature)
	{
		return Verify_v15(rsa, hash, hashValue, signature, tryNonStandardEncoding: false);
	}

	public static bool Verify_v15(_0011.a rsa, _0011.L hash, byte[] hashValue, byte[] signature, bool tryNonStandardEncoding)
	{
		int num = rsa.KeySize >> 3;
		byte[] s = OS2IP(signature);
		byte[] x = RSAVP1(rsa, s);
		byte[] array = I2OSP(x, num);
		byte[] array2 = Encode_v15(hash, hashValue, num);
		bool flag = _0003L(array2, array);
		if (flag || !tryNonStandardEncoding)
		{
			return flag;
		}
		if (array[0] != 0 || array[1] != 1)
		{
			return false;
		}
		int k;
		for (k = 2; k < array.Length - hashValue.Length - 1; k++)
		{
			if (array[k] != byte.MaxValue)
			{
				return false;
			}
		}
		if (array[k++] != 0)
		{
			return false;
		}
		byte[] array3 = new byte[hashValue.Length];
		Buffer.BlockCopy(array, k, array3, 0, array3.Length);
		return _0003L(array3, hashValue);
	}

	public static byte[] Encode_v15(_0011.L hash, byte[] hashValue, int emLength)
	{
		if (hashValue.Length != hash.HashSize >> 3)
		{
			throw new _0011._0003("bad hash length for " + hash.ToString());
		}
		byte[] array = null;
		string text = _0011.f.MapNameToOID(hash.ToString());
		if (text != null)
		{
			Q.B b = new Q.B(48);
			b.Add(new Q.B(_0011.f.EncodeOID(text)));
			b.Add(new Q.B(5));
			Q.B asn = new Q.B(4, hashValue);
			Q.B b2 = new Q.B(48);
			b2.Add(b);
			b2.Add(asn);
			array = b2.GetBytes();
		}
		else
		{
			array = hashValue;
		}
		Buffer.BlockCopy(hashValue, 0, array, array.Length - hashValue.Length, hashValue.Length);
		int num = Math.Max(8, emLength - array.Length - 3);
		byte[] array2 = new byte[num + array.Length + 3];
		array2[1] = 1;
		for (int k = 2; k < num + 2; k++)
		{
			array2[k] = byte.MaxValue;
		}
		Buffer.BlockCopy(array, 0, array2, num + 3, array.Length);
		return array2;
	}

	public static byte[] MGF1(_0011.L hash, byte[] mgfSeed, int maskLen)
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
		for (int k = 0; k < num3; k++)
		{
			byte[] src = I2OSP(k, 4);
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
