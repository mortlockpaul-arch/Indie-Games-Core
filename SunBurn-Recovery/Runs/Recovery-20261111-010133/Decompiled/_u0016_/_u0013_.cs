using System;
using System.Collections;
using System.Globalization;

namespace _u0016_;

[Serializable]
internal sealed class _u0013_ : IComparer
{
	public static readonly _u0013_ Default = new _u0013_();

	internal static readonly _u0013_ l8a = new _u0013_(CultureInfo.InvariantCulture);

	private CompareInfo l8_u0013_;

	private _u0013_()
	{
	}

	internal _u0013_(CultureInfo param_0)
	{
		if (param_0 == null)
		{
			throw new ArgumentNullException("culture");
		}
		l8_u0013_ = param_0.CompareInfo;
	}

	public int Compare(object a, object b)
	{
		if (a == b)
		{
			return 0;
		}
		if (a == null)
		{
			return -1;
		}
		if (b == null)
		{
			return 1;
		}
		if (l8_u0013_ != null)
		{
			string text = a as string;
			string text2 = b as string;
			if (text != null && text2 != null)
			{
				return l8_u0013_.Compare(text, text2);
			}
		}
		if (a is IComparable)
		{
			return (a as IComparable).CompareTo(b);
		}
		if (b is IComparable)
		{
			return -(b as IComparable).CompareTo(a);
		}
		throw new ArgumentException("Neither 'a' nor 'b' implements IComparable.");
	}
}
