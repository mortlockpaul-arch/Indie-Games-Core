using System;
using _u0013_;

namespace N;

internal class a
{
	private _u0013_.a l8a;

	internal a(_u0013_.a param_0)
	{
		l8a = param_0;
	}

	internal void f(byte[] param_0, out string param_1, out uint param_2, out uint param_3, out DateTime param_4)
	{
		byte[] array = l8a.W(param_0);
		if (array.Length <= 8)
		{
			param_1 = "";
			param_2 = 0u;
			param_3 = 0u;
			param_4 = DateTime.Now;
			return;
		}
		param_1 = "";
		byte[] array2 = new byte[2];
		for (int i = 0; i < array.Length - 16; i++)
		{
			array2[0] = array[i];
			array2[1] = 0;
			param_1 += global::_u0013_._u0013_.E(array2, 0);
		}
		param_4 = DateTime.Now;
		param_2 = global::_u0013_._u0013_._u0014_(array, array.Length - 8);
		param_3 = global::_u0013_._u0013_._u0014_(array, array.Length - 4);
	}
}
