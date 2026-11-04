using System;
using A;

namespace DigitalRune.Mathematics.Analysis;

/// <summary>
///       Performs numerical integration using the <i>extended trapezoidal rule</i> (double-precision).
///       </summary>
public class TrapezoidalIntegratorD : IntegratorD
{
	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Analysis.TrapezoidalIntegratorD" /> class</summary>
	public TrapezoidalIntegratorD()
	{
	}

	internal static double c5bbe3eb60cb3da596d645a2a5154dfc8(Func<double, double> c82d1df3580e4faabc557f40ec1c0a401, double c9a7ffb2bfe3b48e77127456ae6530353, double cfbe71c4d7e602f3d262fcb4c1896627c, double c6ddc2c4df50fff2f0ca5dccccf4b5b4b, int c9e5863c174840ce096a45496fda8518c)
	{
		if (c9e5863c174840ce096a45496fda8518c == 1)
		{
			return 0.5 * (cfbe71c4d7e602f3d262fcb4c1896627c - c9a7ffb2bfe3b48e77127456ae6530353) * (c82d1df3580e4faabc557f40ec1c0a401(c9a7ffb2bfe3b48e77127456ae6530353) + c82d1df3580e4faabc557f40ec1c0a401(cfbe71c4d7e602f3d262fcb4c1896627c));
		}
		int num = 1;
		for (int i = 1; i < c9e5863c174840ce096a45496fda8518c - 1; i++)
		{
			num <<= 1;
		}
		while (true)
		{
			switch (1)
			{
			case 0:
				continue;
			}
			if (1 == 0)
			{
				__ldtoken(TrapezoidalIntegratorD.c5bbe3eb60cb3da596d645a2a5154dfc8(Func<double, double>, double, double, double, int));
			}
			double num2 = (cfbe71c4d7e602f3d262fcb4c1896627c - c9a7ffb2bfe3b48e77127456ae6530353) / (double)num;
			double num3 = c9a7ffb2bfe3b48e77127456ae6530353 + 0.5 * num2;
			double num4 = 0.0;
			int num5 = 0;
			while (num5 < num)
			{
				num4 += c82d1df3580e4faabc557f40ec1c0a401(num3);
				num5++;
				num3 += num2;
			}
			while (true)
			{
				switch (7)
				{
				case 0:
					continue;
				}
				return 0.5 * (c6ddc2c4df50fff2f0ca5dccccf4b5b4b + (cfbe71c4d7e602f3d262fcb4c1896627c - c9a7ffb2bfe3b48e77127456ae6530353) * num4 / (double)num);
			}
		}
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
			while (true)
			{
				switch (6)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(TrapezoidalIntegratorD.Integrate(Func<double, double>, double, double));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(366));
				}
			}
		}
		double num = 0.0;
		double num2 = 0.0;
		for (int i = 0; i < MaxNumberOfIterations; i++)
		{
			NumberOfIterations++;
			num = c5bbe3eb60cb3da596d645a2a5154dfc8(function, lowerBound, upperBound, num2, i + 1);
			if (NumberOfIterations >= MinNumberOfIterations && Numeric.AreEqual(num, num2, Epsilon))
			{
				while (true)
				{
					switch (2)
					{
					case 0:
						break;
					default:
						return num;
					}
				}
			}
			num2 = num;
		}
		while (true)
		{
			switch (1)
			{
			case 0:
				continue;
			}
			return num;
		}
	}
}
