using System;

namespace a;

internal class a
{
	private static byte[] fNB = new byte[8];

	internal static int s(byte[] P_0, int P_1)
	{
		fNB[3] = P_0[P_1++];
		fNB[2] = P_0[P_1++];
		fNB[1] = P_0[P_1++];
		fNB[0] = P_0[P_1++];
		return BitConverter.ToInt32(fNB, 0);
	}

	internal static uint _0011(byte[] P_0, int P_1)
	{
		fNB[3] = P_0[P_1++];
		fNB[2] = P_0[P_1++];
		fNB[1] = P_0[P_1++];
		fNB[0] = P_0[P_1++];
		return BitConverter.ToUInt32(fNB, 0);
	}

	internal static char Q(byte[] P_0, int P_1)
	{
		fNB[1] = P_0[P_1++];
		fNB[0] = P_0[P_1++];
		return BitConverter.ToChar(fNB, 0);
	}
}
