using System;
using A;

namespace DigitalRune.Mathematics.Analysis;

/// <summary>
///       Performs numerical integration using the <i>Romberg's method</i> (single-precision).
///       </summary>
public class RombergIntegratorF : IntegratorF
{
	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Analysis.RombergIntegratorF" /> class</summary>
	public RombergIntegratorF()
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
			while (true)
			{
				switch (3)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(RombergIntegratorF.Integrate(Func<float, float>, float, float));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(366));
				}
			}
		}
		if (lowerBound == upperBound)
		{
			return 0f;
		}
		float[,] array = new float[MaxNumberOfIterations + 1, MaxNumberOfIterations + 1];
		float num = upperBound - lowerBound;
		float num2 = function(lowerBound);
		float num3 = function(upperBound);
		array[0, 0] = num / 2f * (num2 + num3);
		int num4 = 1;
		while (num4 <= MaxNumberOfIterations)
		{
			NumberOfIterations++;
			float num5 = 0f;
			int num6 = (int)Math.Pow(2.0, num4);
			for (int i = 1; i < num6; i++)
			{
				num5 += function(lowerBound + (float)i * num / (float)num6);
			}
			while (true)
			{
				switch (5)
				{
				case 0:
					continue;
				}
				array[num4, 0] = num / (float)(2 * num6) * (num2 + num3 + 2f * num5);
				for (int j = 1; j <= num4; j++)
				{
					int num7 = num4 - j;
					array[num7, j] = (float)((Math.Pow(4.0, j) * (double)array[num7 + 1, j - 1] - (double)array[num7, j - 1]) / (Math.Pow(4.0, j) - 1.0));
				}
				if (NumberOfIterations >= MinNumberOfIterations)
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
					if (Numeric.AreEqual(array[0, num4], array[0, num4 - 1], Epsilon))
					{
						return array[0, num4];
					}
				}
				num4++;
				break;
			}
		}
		while (true)
		{
			switch (1)
			{
			case 0:
				continue;
			}
			return array[0, num4 - 1];
		}
	}
}
