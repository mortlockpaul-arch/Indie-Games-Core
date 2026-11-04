using System;
using System.Collections.Generic;
using _0011;

namespace a;

internal class B
{
	private _0011._8 fNB = new _0011._8();

	private _0011._8 fNa = new _0011._8();

	private _0011.w fNg = new _0011.w();

	private List<byte[]> fNf = new List<byte[]>();

	internal B(byte[] P_0, byte[] P_1)
	{
		fNa.PersistKeyInCsp = false;
		fNa.ImportCspBlob(P_0);
		fNB.PersistKeyInCsp = false;
		fNB.ImportCspBlob(P_1);
	}

	internal byte[] d(byte[] P_0)
	{
		int num = a.s(P_0, 0);
		byte[] array = new byte[num];
		byte[] array2 = new byte[P_0.Length - (num + 4)];
		Array.Copy(P_0, 4, array, 0, array.Length);
		Array.Copy(P_0, num + 4, array2, 0, array2.Length);
		byte[] rgbHash = fNg.ComputeHash(array2);
		if (!fNB.VerifyHash(rgbHash, _0011.f.MapNameToOID("SHA1"), array))
		{
			return null;
		}
		return w(array2);
	}

	private byte[] w(byte[] P_0)
	{
		int num = fNa.KeySize / 8;
		byte[] array = new byte[num];
		int num2 = 0;
		int num3 = 0;
		fNf.Clear();
		while (num2 < P_0.Length)
		{
			int num4 = Math.Min(num, P_0.Length - num2);
			byte[] array2 = array;
			if (num4 < array2.Length)
			{
				array2 = new byte[num4];
			}
			Array.Copy(P_0, num2, array2, 0, num4);
			byte[] array3 = fNa.Decrypt(array2, fOAEP: true);
			num2 += num4;
			num3 += array3.Length;
			fNf.Add(array3);
		}
		byte[] array4 = new byte[num3];
		num2 = 0;
		foreach (byte[] item in fNf)
		{
			item.CopyTo(array4, num2);
			num2 += item.Length;
		}
		return array4;
	}
}
