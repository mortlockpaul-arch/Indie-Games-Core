using System;
using A;

namespace DigitalRune.Mathematics.Analysis;

/// <summary>
///       Performs numerical integration using the <i>Romberg's method</i> (double-precision).
///       </summary>
public class RombergIntegratorD : IntegratorD
{
	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Analysis.RombergIntegratorD" /> class</summary>
	public RombergIntegratorD()
	{
	}

	/// <summary>
	///       Integrates the specified function within the given interval.
	///       </summary>
	/// <param name="function">The function.</param>
	/// <param name="lowerBound">The lower bound.</param>
	/// <param name="upperBound">The upper bound.</param>
	/// <returns>
	///       The integral of the given function over the interval 
	///       [<paramref name="lowerBound" />, <paramref name="upperBound" />].
	///       </returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="function" /> is <see langword="null" />.
	///       </exception>
	public override double Integrate(Func<double, double> function, double lowerBound, double upperBound)
	{
		NumberOfIterations = 0;
		if (function == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(366));
		}
		if (lowerBound == upperBound)
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
						__ldtoken(RombergIntegratorD.Integrate(Func<double, double>, double, double));
					}
					return 0.0;
				}
			}
		}
		double[,] array = new double[MaxNumberOfIterations + 1, MaxNumberOfIterations + 1];
		double num = upperBound - lowerBound;
		double num2 = function(lowerBound);
		double num3 = function(upperBound);
		array[0, 0] = num / 2.0 * (num2 + num3);
		int num4 = 1;
		while (num4 <= MaxNumberOfIterations)
		{
			NumberOfIterations++;
			double num5 = 0.0;
			int num6 = (int)Math.Pow(2.0, num4);
			for (int i = 1; i < num6; i++)
			{
				num5 += function(lowerBound + (double)i * num / (double)num6);
			}
			while (true)
			{
				switch (3)
				{
				case 0:
					continue;
				}
				array[num4, 0] = num / (double)(2 * num6) * (num2 + num3 + 2.0 * num5);
				for (int j = 1; j <= num4; j++)
				{
					int num7 = num4 - j;
					array[num7, j] = (Math.Pow(4.0, j) * array[num7 + 1, j - 1] - array[num7, j - 1]) / (Math.Pow(4.0, j) - 1.0);
				}
				if (NumberOfIterations >= MinNumberOfIterations)
				{
					while (true)
					{
						switch (6)
						{
						case 0:
							continue;
						}
						break;
					}
					if (Numeric.AreEqual(array[0, num4], array[0, num4 - 1], Epsilon))
					{
						return array[0, num4];
					}
				}
				num4++;
				break;
			}
		}
		return array[0, num4 - 1];
	}
}
