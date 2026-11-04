using System;

namespace DigitalRune.Mathematics.Analysis;

/// <summary>
///       Finds roots using the regula falsi (false position) method (single-precision).
///       </summary>
public class RegulaFalsiMethodF : RootFinderF
{
	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Analysis.RegulaFalsiMethodF" /> class.
	///       </summary>
	/// <param name="function">The function f(x), which root we want to find.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="function" /> is <see langword="null" />.
	///       </exception>
	public RegulaFalsiMethodF(Func<float, float> function)
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
	protected override float FindRoot(Func<float, float> function, float x0, float x1)
	{
		NumberOfIterations = 0;
		float obj = function(x0);
		float obj2 = function(x1);
		if (Numeric.IsZero(obj, Epsilon))
		{
			return x0;
		}
		if (Numeric.IsZero(obj2, Epsilon))
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
						__ldtoken(RegulaFalsiMethodF.FindRoot(Func<float, float>, float, float));
					}
					return x1;
				}
			}
		}
		if (!Numeric.AreEqual(x0, x1, Epsilon))
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
			if (!(obj * obj2 >= 0f))
			{
				float num;
				float num2;
				if (obj < 0f)
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
					float num4 = num2 - num;
					float num5 = num + num4 * obj / (obj - obj2);
					float num6 = function(num5);
					if (!Numeric.IsZero(num6, Epsilon))
					{
						if (!Numeric.IsZero(num4, Epsilon))
						{
							if (num6 < 0f)
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
							switch (4)
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
					return float.NaN;
				}
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
		return float.NaN;
	}
}
