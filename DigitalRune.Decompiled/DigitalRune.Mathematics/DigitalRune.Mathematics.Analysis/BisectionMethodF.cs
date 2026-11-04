using System;

namespace DigitalRune.Mathematics.Analysis;

/// <summary>
///       Finds roots using the bisection method (single-precision).
///       </summary>
public class BisectionMethodF : RootFinderF
{
	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Analysis.BisectionMethodF" /> class.
	///       </summary>
	/// <param name="function">The function f(x), which root we want to find.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="function" /> is <see langword="null" />.
	///       </exception>
	public BisectionMethodF(Func<float, float> function)
		: base(function)
	{
	}

	/// <summary>
	///       Finds the root of the given function.
	///       </summary>
	/// <param name="function">The function f.</param>
	/// <param name="x0">
	///       An x value such that the root lies between <paramref name="x0" /> and <paramref name="x1" />.
	///       </param>
	/// <param name="x1">
	///       An x value such that the root lies between <paramref name="x0" /> and <paramref name="x1" />.
	///       </param>
	/// <returns>
	///       The x value such that <i>f(x) = 0</i>; or <i>NaN</i> if no root is found.
	///       </returns>
	protected override float FindRoot(Func<float, float> function, float x0, float x1)
	{
		NumberOfIterations = 0;
		float num = function(x0);
		float num2 = function(x1);
		if (Numeric.IsZero(num, Epsilon))
		{
			while (true)
			{
				switch (3)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(BisectionMethodF.FindRoot(Func<float, float>, float, float));
					}
					return x0;
				}
			}
		}
		if (Numeric.IsZero(num2, Epsilon))
		{
			return x1;
		}
		if (!Numeric.AreEqual(x0, x1, Epsilon))
		{
			while (true)
			{
				switch (1)
				{
				case 0:
					continue;
				}
				break;
			}
			if (!(num * num2 >= 0f))
			{
				float num3;
				if (num < 0f)
				{
					while (true)
					{
						switch (5)
						{
						case 0:
							continue;
						}
						break;
					}
					num3 = x1 - x0;
				}
				else
				{
					num3 = x0 - x1;
					x0 = x1;
				}
				int num4 = 0;
				while (num4 < MaxNumberOfIterations)
				{
					NumberOfIterations++;
					num3 *= 0.5f;
					float num5 = x0 + num3;
					num2 = function(num5);
					if (!Numeric.IsZero(num2, Epsilon))
					{
						while (true)
						{
							switch (2)
							{
							case 0:
								continue;
							}
							break;
						}
						if (!Numeric.IsZero(num3, Epsilon))
						{
							if (num2 < 0f)
							{
								x0 = num5;
							}
							num4++;
							continue;
						}
					}
					return num5;
				}
				while (true)
				{
					switch (4)
					{
					case 0:
						continue;
					}
					return float.NaN;
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
		return float.NaN;
	}
}
