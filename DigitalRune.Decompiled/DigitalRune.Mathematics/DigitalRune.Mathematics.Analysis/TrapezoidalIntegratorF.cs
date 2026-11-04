using System;
using A;

namespace DigitalRune.Mathematics.Analysis;

/// <summary>
///       Performs numerical integration using the <i>extended trapezoidal rule</i> (single-precision).
///       </summary>
public class TrapezoidalIntegratorF : IntegratorF
{
	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Analysis.TrapezoidalIntegratorF" /> class</summary>
	public TrapezoidalIntegratorF()
	{
	}

	internal static float c5bbe3eb60cb3da596d645a2a5154dfc8(Func<float, float> c82d1df3580e4faabc557f40ec1c0a401, float c9a7ffb2bfe3b48e77127456ae6530353, float cfbe71c4d7e602f3d262fcb4c1896627c, float c6ddc2c4df50fff2f0ca5dccccf4b5b4b, int c9e5863c174840ce096a45496fda8518c)
	{
		if (c9e5863c174840ce096a45496fda8518c == 1)
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
						__ldtoken(TrapezoidalIntegratorF.c5bbe3eb60cb3da596d645a2a5154dfc8(Func<float, float>, float, float, float, int));
					}
					return 0.5f * (cfbe71c4d7e602f3d262fcb4c1896627c - c9a7ffb2bfe3b48e77127456ae6530353) * (c82d1df3580e4faabc557f40ec1c0a401(c9a7ffb2bfe3b48e77127456ae6530353) + c82d1df3580e4faabc557f40ec1c0a401(cfbe71c4d7e602f3d262fcb4c1896627c));
				}
			}
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
			float num2 = (cfbe71c4d7e602f3d262fcb4c1896627c - c9a7ffb2bfe3b48e77127456ae6530353) / (float)num;
			float num3 = c9a7ffb2bfe3b48e77127456ae6530353 + 0.5f * num2;
			float num4 = 0f;
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
				return 0.5f * (c6ddc2c4df50fff2f0ca5dccccf4b5b4b + (cfbe71c4d7e602f3d262fcb4c1896627c - c9a7ffb2bfe3b48e77127456ae6530353) * num4 / (float)num);
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
	public override float Integrate(Func<float, float> function, float lowerBound, float upperBound)
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
						__ldtoken(TrapezoidalIntegratorF.Integrate(Func<float, float>, float, float));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(366));
				}
			}
		}
		float num = 0f;
		float num2 = 0f;
		for (int i = 0; i < MaxNumberOfIterations; i++)
		{
			NumberOfIterations++;
			num = c5bbe3eb60cb3da596d645a2a5154dfc8(function, lowerBound, upperBound, num2, i + 1);
			if (NumberOfIterations >= MinNumberOfIterations && Numeric.AreEqual(num, num2, Epsilon))
			{
				while (true)
				{
					switch (7)
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
		return num;
	}
}
