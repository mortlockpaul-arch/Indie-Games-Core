using System;

namespace _u0013_;

internal class _u0013_
{
	private static byte[] l8a = new byte[8];

	internal static int T(byte[] param_0, int param_1)
	{
		l8a[3] = param_0[param_1++];
		l8a[2] = param_0[param_1++];
		l8a[1] = param_0[param_1++];
		l8a[0] = param_0[param_1++];
		return BitConverter.ToInt32(l8a, 0);
	}

	internal static uint _u0014_(byte[] param_0, int param_1)
	{
		l8a[3] = param_0[param_1++];
		l8a[2] = param_0[param_1++];
		l8a[1] = param_0[param_1++];
		l8a[0] = param_0[param_1++];
		return BitConverter.ToUInt32(l8a, 0);
	}

	internal static char E(byte[] param_0, int param_1)
	{
		l8a[1] = param_0[param_1++];
		l8a[0] = param_0[param_1++];
		return BitConverter.ToChar(l8a, 0);
	}
}
