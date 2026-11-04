using System;

namespace DigitalRune.Mathematics.Analysis;

/// <summary>
///       Finds roots using the bisection method (double-precision).
///       </summary>
public class BisectionMethodD : RootFinderD
{
	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Analysis.BisectionMethodD" /> class.
	///       </summary>
	/// <param name="function">The function f(x), which root we want to find.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="function" /> is <see langword="null" />.
	///       </exception>
	public BisectionMethodD(Func<double, double> function)
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
		double num = function(x0);
		double num2 = function(x1);
		if (Numeric.IsZero(num, Epsilon))
		{
			return x0;
		}
		if (Numeric.IsZero(num2, Epsilon))
		{
			return x1;
		}
		if (Numeric.AreEqual(x0, x1, Epsilon) || num * num2 >= 0.0)
		{
			return double.NaN;
		}
		double num3;
		if (num < 0.0)
		{
			while (true)
			{
				switch (3)
				{
				case 0:
					continue;
				}
				break;
			}
			if (1 == 0)
			{
				__ldtoken(BisectionMethodD.FindRoot(Func<double, double>, double, double));
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
			num3 *= 0.5;
			double num5 = x0 + num3;
			num2 = function(num5);
			if (!Numeric.IsZero(num2, Epsilon))
			{
				if (!Numeric.IsZero(num3, Epsilon))
				{
					if (num2 < 0.0)
					{
						while (true)
						{
							switch (3)
							{
							case 0:
								continue;
							}
							break;
						}
						x0 = num5;
					}
					num4++;
					continue;
				}
				while (true)
				{
					switch (3)
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
			switch (6)
			{
			case 0:
				continue;
			}
			return double.NaN;
		}
	}
}
