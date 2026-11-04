using System;

namespace DigitalRune.Mathematics.Analysis;

/// <summary>
///       Finds roots using an improved Newton-Raphson method (single-precision).
///       </summary>
public class ImprovedNewtonRaphsonMethodF : NewtonRaphsonMethodF
{
	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Analysis.ImprovedNewtonRaphsonMethodF" /> class.
	///       </summary>
	/// <param name="function">The function f(x), which root we want to find.</param>
	/// <param name="derivative">The function f'(x), which computes the derivative.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="function" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="derivative" /> is <see langword="null" />.
	///       </exception>
	public ImprovedNewtonRaphsonMethodF(Func<float, float> function, Func<float, float> derivative)
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
	protected override float FindRoot(Func<float, float> function, float x0, float x1)
	{
		NumberOfIterations = 0;
		float num = function(x0);
		float num2 = function(x1);
		if (Numeric.IsZero(num, Epsilon))
		{
			while (true)
			{
				switch (7)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(ImprovedNewtonRaphsonMethodF.FindRoot(Func<float, float>, float, float));
					}
					return x0;
				}
			}
		}
		if (Numeric.IsZero(num2, Epsilon))
		{
			while (true)
			{
				switch (3)
				{
				case 0:
					break;
				default:
					return x1;
				}
			}
		}
		if (!Numeric.AreEqual(x0, x1, Epsilon))
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
			if (!(num * num2 >= 0f))
			{
				float num3;
				float num4;
				if (num < 0f)
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
				float num5 = (x0 + x1) / 2f;
				float num6 = Math.Abs(x1 - x0);
				float num7 = num6;
				float num8 = function(num5);
				if (!Numeric.IsZero(num8, Epsilon))
				{
					if (!Numeric.IsZero(num7, Epsilon))
					{
						for (int i = 0; i < MaxNumberOfIterations; i++)
						{
							NumberOfIterations++;
							float num9 = Derivative(num5);
							if (((num5 - num4) * num9 - num8) * ((num5 - num3) * num9 - num8) > 0f || Math.Abs(2f * num8) > Math.Abs(num6 * num9))
							{
								num6 = num7;
								num7 = (num4 - num3) / 2f;
								num5 = num3 + num7;
							}
							else
							{
								num6 = num7;
								num7 = num8 / num9;
								num5 -= num7;
							}
							num8 = function(num5);
							if (Numeric.IsZero(num8, Epsilon) || Numeric.IsZero(num7, Epsilon))
							{
								return num5;
							}
							if (num8 < 0f)
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
								num3 = num5;
							}
							else
							{
								num4 = num5;
							}
						}
						return float.NaN;
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
		}
		return float.NaN;
	}
}
