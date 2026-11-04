using System;
using A;

namespace DigitalRune.Mathematics.Analysis;

/// <summary>
///       Performs numerical integration using the <i>Simpson's rule</i> (single-precision).
///       </summary>
public class SimpsonIntegratorF : IntegratorF
{
	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Analysis.SimpsonIntegratorF" /> class</summary>
	public SimpsonIntegratorF()
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
	public override float Integrate(Func<float, float> function, float lowerBound, float upperBound)
	{
		NumberOfIterations = 0;
		if (function == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(366));
		}
		float num = 0f;
		float num2 = 0f;
		float value = 0f;
		for (int i = 0; i < MaxNumberOfIterations; i++)
		{
			NumberOfIterations++;
			float num3 = TrapezoidalIntegratorF.c5bbe3eb60cb3da596d645a2a5154dfc8(function, lowerBound, upperBound, num2, i + 1);
			num = (4f * num3 - num2) / 3f;
			if (NumberOfIterations >= MinNumberOfIterations)
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
				if (1 == 0)
				{
					__ldtoken(SimpsonIntegratorF.Integrate(Func<float, float>, float, float));
				}
				if (Numeric.AreEqual(num, value, Epsilon))
				{
					return num;
				}
			}
			value = num;
			num2 = num3;
		}
		return num;
	}
}
