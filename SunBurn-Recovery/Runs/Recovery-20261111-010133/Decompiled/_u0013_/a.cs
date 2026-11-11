using System;
using System.Collections.Generic;
using _u0014_;

namespace _u0013_;

internal class a
{
	private _u0014_.v l8a = new _u0014_.v();

	private _u0014_.v l8_u0013_ = new _u0014_.v();

	private _u0014_.Z l8N = new _u0014_.Z();

	private List<byte[]> l8l = new List<byte[]>();

	internal a(byte[] param_0, byte[] param_1)
	{
		l8_u0013_.PersistKeyInCsp = false;
		l8_u0013_.ImportCspBlob(param_0);
		l8a.PersistKeyInCsp = false;
		l8a.ImportCspBlob(param_1);
	}

	internal byte[] W(byte[] param_0)
	{
		int num = _u0013_.T(param_0, 0);
		byte[] array = new byte[num];
		byte[] array2 = new byte[param_0.Length - (num + 4)];
		Array.Copy(param_0, 4, array, 0, array.Length);
		Array.Copy(param_0, num + 4, array2, 0, array2.Length);
		byte[] rgbHash = l8N.ComputeHash(array2);
		if (!l8a.VerifyHash(rgbHash, _u0014_.l.MapNameToOID("SHA1"), array))
		{
			return null;
		}
		return Z(array2);
	}

	private byte[] Z(byte[] param_0)
	{
		int num = l8_u0013_.KeySize / 8;
		byte[] array = new byte[num];
		int num2 = 0;
		int num3 = 0;
		l8l.Clear();
		while (num2 < param_0.Length)
		{
			int num4 = Math.Min(num, param_0.Length - num2);
			byte[] array2 = array;
			if (num4 < array2.Length)
			{
				array2 = new byte[num4];
			}
			Array.Copy(param_0, num2, array2, 0, num4);
			byte[] array3 = l8_u0013_.Decrypt(array2, fOAEP: true);
			num2 += num4;
			num3 += array3.Length;
			l8l.Add(array3);
		}
		byte[] array4 = new byte[num3];
		num2 = 0;
		foreach (byte[] item in l8l)
		{
			item.CopyTo(array4, num2);
			num2 += item.Length;
		}
		return array4;
	}
}
