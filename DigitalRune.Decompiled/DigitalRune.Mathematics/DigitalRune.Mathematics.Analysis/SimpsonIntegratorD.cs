using System;
using A;

namespace DigitalRune.Mathematics.Analysis;

/// <summary>
///       Performs numerical integration using the <i>Simpson's rule</i> (double-precision).
///       </summary>
public class SimpsonIntegratorD : IntegratorD
{
	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Analysis.SimpsonIntegratorD" /> class</summary>
	public SimpsonIntegratorD()
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
		double num = 0.0;
		double num2 = 0.0;
		double value = 0.0;
		for (int i = 0; i < MaxNumberOfIterations; i++)
		{
			NumberOfIterations++;
			double num3 = TrapezoidalIntegratorD.c5bbe3eb60cb3da596d645a2a5154dfc8(function, lowerBound, upperBound, num2, i + 1);
			num = (4.0 * num3 - num2) / 3.0;
			if (NumberOfIterations >= MinNumberOfIterations)
			{
				while (true)
				{
					switch (7)
					{
					case 0:
						continue;
					}
					break;
				}
				if (1 == 0)
				{
					__ldtoken(SimpsonIntegratorD.Integrate(Func<double, double>, double, double));
				}
				if (Numeric.AreEqual(num, value, Epsilon))
				{
					return num;
				}
			}
			value = num;
			num2 = num3;
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
}
