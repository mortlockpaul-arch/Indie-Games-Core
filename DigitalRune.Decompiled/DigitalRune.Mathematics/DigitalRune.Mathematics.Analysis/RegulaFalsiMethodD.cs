using System;

namespace DigitalRune.Mathematics.Analysis;

/// <summary>
///       Finds roots using the regula falsi (false position) method (double-precision).
///       </summary>
public class RegulaFalsiMethodD : RootFinderD
{
	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Analysis.RegulaFalsiMethodD" /> class.
	///       </summary>
	/// <param name="function">The function f(x), which root we want to find.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="function" /> is <see langword="null" />.
	///       </exception>
	public RegulaFalsiMethodD(Func<double, double> function)
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
	protected override double FindRoot(Func<double, double> function, double x0, double x1)
	{
		NumberOfIterations = 0;
		double obj = function(x0);
		double obj2 = function(x1);
		if (Numeric.IsZero(obj, Epsilon))
		{
			while (true)
			{
				switch (6)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(RegulaFalsiMethodD.FindRoot(Func<double, double>, double, double));
					}
					return x0;
				}
			}
		}
		if (Numeric.IsZero(obj2, Epsilon))
		{
			return x1;
		}
		if (!Numeric.AreEqual(x0, x1, Epsilon))
		{
			if (!(obj * obj2 >= 0.0))
			{
				double num;
				double num2;
				if (obj < 0.0)
				{
					num = x0;
					num2 = x1;
				}
				else
				{
					num = x1;
					num2 = x0;
					MathHelper.Swap(ref obj, ref obj2);
				}
				int num3 = 0;
				while (num3 < MaxNumberOfIterations)
				{
					NumberOfIterations++;
					double num4 = num2 - num;
					double num5 = num + num4 * obj / (obj - obj2);
					double num6 = function(num5);
					if (!Numeric.IsZero(num6, Epsilon))
					{
						if (!Numeric.IsZero(num4, Epsilon))
						{
							if (num6 < 0.0)
							{
								num = num5;
								obj = num6;
							}
							else
							{
								num2 = num5;
								obj2 = num6;
							}
							num3++;
							continue;
						}
						while (true)
						{
							switch (6)
							{
							case 0:
								continue;
							}
							break;
						}
					}
					return num5;
				}
				while (true)
				{
					switch (7)
					{
					case 0:
						continue;
					}
					return double.NaN;
				}
			}
			while (true)
			{
				switch (2)
				{
				case 0:
					continue;
				}
				break;
			}
		}
		return double.NaN;
	}
}
