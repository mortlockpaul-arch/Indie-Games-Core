using System;

namespace DigitalRune.Mathematics.Analysis;

/// <summary>
///       Finds roots using an improved Newton-Raphson method (double-precision).
///       </summary>
public class ImprovedNewtonRaphsonMethodD : NewtonRaphsonMethodD
{
	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Analysis.ImprovedNewtonRaphsonMethodD" /> class.
	///       </summary>
	/// <param name="function">The function f(x), which root we want to find.</param>
	/// <param name="derivative">The function f'(x), which computes the derivative.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="function" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="derivative" /> is <see langword="null" />.
	///       </exception>
	public ImprovedNewtonRaphsonMethodD(Func<double, double> function, Func<double, double> derivative)
		: base(function, derivative)
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
			while (true)
			{
				switch (6)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(ImprovedNewtonRaphsonMethodD.FindRoot(Func<double, double>, double, double));
					}
					return x1;
				}
			}
		}
		if (Numeric.AreEqual(x0, x1, Epsilon) || num * num2 >= 0.0)
		{
			return double.NaN;
		}
		double num3;
		double num4;
		if (num < 0.0)
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
			num3 = x0;
			num4 = x1;
		}
		else
		{
			num3 = x1;
			num4 = x0;
		}
		double num5 = (x0 + x1) / 2.0;
		double num6 = Math.Abs(x1 - x0);
		double num7 = num6;
		double num8 = function(num5);
		if (Numeric.IsZero(num8, Epsilon) || Numeric.IsZero(num7, Epsilon))
		{
			return num5;
		}
		for (int num9 = 0; num9 < MaxNumberOfIterations; num9++)
		{
			NumberOfIterations++;
			double num10 = Derivative(num5);
			if (!(((num5 - num4) * num10 - num8) * ((num5 - num3) * num10 - num8) > 0.0))
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
				if (!(Math.Abs(2.0 * num8) > Math.Abs(num6 * num10)))
				{
					num6 = num7;
					num7 = num8 / num10;
					num5 -= num7;
					goto IL_018c;
				}
			}
			num6 = num7;
			num7 = (num4 - num3) / 2.0;
			num5 = num3 + num7;
			goto IL_018c;
			IL_018c:
			num8 = function(num5);
			if (Numeric.IsZero(num8, Epsilon) || Numeric.IsZero(num7, Epsilon))
			{
				return num5;
			}
			if (num8 < 0.0)
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
				num3 = num5;
			}
			else
			{
				num4 = num5;
			}
		}
		return double.NaN;
	}
}
