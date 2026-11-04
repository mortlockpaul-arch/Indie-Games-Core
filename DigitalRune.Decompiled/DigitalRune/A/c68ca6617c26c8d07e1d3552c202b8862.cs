using System;

namespace A;

internal class c68ca6617c26c8d07e1d3552c202b8862
{
	private static long c0702c0de268216c03be67b1a8534c354(byte[] c8eea6fe5a82bc3c0867f6cf9ae7c0eea, int cd1692db43d70a38d5bb9ccc9014c59bb, int ccd96373c362dad39976cd467c20ce8f8)
	{
		if (c8eea6fe5a82bc3c0867f6cf9ae7c0eea == null)
		{
			while (true)
			{
				switch (7)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(c68ca6617c26c8d07e1d3552c202b8862.c0702c0de268216c03be67b1a8534c354(byte[], int, int));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(588));
				}
			}
		}
		if (cd1692db43d70a38d5bb9ccc9014c59bb >= 0)
		{
			if (cd1692db43d70a38d5bb9ccc9014c59bb <= c8eea6fe5a82bc3c0867f6cf9ae7c0eea.Length - ccd96373c362dad39976cd467c20ce8f8)
			{
				long num = 0L;
				for (int i = 0; i < ccd96373c362dad39976cd467c20ce8f8; i++)
				{
					num = (num << 8) | c8eea6fe5a82bc3c0867f6cf9ae7c0eea[cd1692db43d70a38d5bb9ccc9014c59bb + ccd96373c362dad39976cd467c20ce8f8 - 1 - i];
				}
				while (true)
				{
					switch (6)
					{
					case 0:
						continue;
					}
					return num;
				}
			}
			while (true)
			{
				switch (4)
				{
				case 0:
					continue;
				}
				break;
			}
		}
		throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9905));
	}

	public static int c3034c4efa5344a42739fa4fc8bd89d42(byte[] c8eea6fe5a82bc3c0867f6cf9ae7c0eea, int cd1692db43d70a38d5bb9ccc9014c59bb)
	{
		return (int)c0702c0de268216c03be67b1a8534c354(c8eea6fe5a82bc3c0867f6cf9ae7c0eea, cd1692db43d70a38d5bb9ccc9014c59bb, 4);
	}

	public static short ccf0b10fedac007c4f73ef13a38a96900(byte[] c8eea6fe5a82bc3c0867f6cf9ae7c0eea, int cd1692db43d70a38d5bb9ccc9014c59bb)
	{
		return (short)c0702c0de268216c03be67b1a8534c354(c8eea6fe5a82bc3c0867f6cf9ae7c0eea, cd1692db43d70a38d5bb9ccc9014c59bb, 2);
	}

	public static long c9ac46ea7d74cb191a46b6d985934d751(byte[] c8eea6fe5a82bc3c0867f6cf9ae7c0eea, int cd1692db43d70a38d5bb9ccc9014c59bb)
	{
		return c0702c0de268216c03be67b1a8534c354(c8eea6fe5a82bc3c0867f6cf9ae7c0eea, cd1692db43d70a38d5bb9ccc9014c59bb, 8);
	}

	internal static ushort cfa9dd51ad15fd65e5a0f1934fb5c2cd9(byte[] c8eea6fe5a82bc3c0867f6cf9ae7c0eea, int cd1692db43d70a38d5bb9ccc9014c59bb)
	{
		return (ushort)c0702c0de268216c03be67b1a8534c354(c8eea6fe5a82bc3c0867f6cf9ae7c0eea, cd1692db43d70a38d5bb9ccc9014c59bb, 2);
	}

	internal static uint cc9fae6c3f4a76df47fa6ca59142d897d(byte[] c8eea6fe5a82bc3c0867f6cf9ae7c0eea, int cd1692db43d70a38d5bb9ccc9014c59bb)
	{
		return (uint)c0702c0de268216c03be67b1a8534c354(c8eea6fe5a82bc3c0867f6cf9ae7c0eea, cd1692db43d70a38d5bb9ccc9014c59bb, 4);
	}
}
