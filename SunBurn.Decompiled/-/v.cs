namespace _0011;

internal sealed class v
{
	private int fNB;

	private int fNa;

	private int fNg;

	public int MaxSize => fNB;

	public int MinSize => fNa;

	public int SkipSize => fNg;

	public v(int minSize, int maxSize, int skipSize)
	{
		fNB = maxSize;
		fNa = minSize;
		fNg = skipSize;
	}

	internal bool _0003R(int P_0)
	{
		int num = P_0 - MinSize;
		bool flag = num >= 0 && P_0 <= MaxSize;
		if (SkipSize != 0)
		{
			if (flag)
			{
				return num % SkipSize == 0;
			}
			return false;
		}
		return flag;
	}

	internal static bool _0003W(v[] P_0, int P_1)
	{
		foreach (v v2 in P_0)
		{
			if (v2._0003R(P_1))
			{
				return true;
			}
		}
		return false;
	}
}
