namespace _u0014_;

internal sealed class s
{
	private int l8a;

	private int l8_u0013_;

	private int l8N;

	public int MaxSize => l8a;

	public int MinSize => l8_u0013_;

	public int SkipSize => l8N;

	public s(int minSize, int maxSize, int skipSize)
	{
		l8a = maxSize;
		l8_u0013_ = minSize;
		l8N = skipSize;
	}

	internal bool La(int param_0)
	{
		int num = param_0 - MinSize;
		bool flag = num >= 0 && param_0 <= MaxSize;
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

	internal static bool L_u0013_(s[] param_0, int param_1)
	{
		foreach (s s2 in param_0)
		{
			if (s2.La(param_1))
			{
				return true;
			}
		}
		return false;
	}
}
