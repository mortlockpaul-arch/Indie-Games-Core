using System;

namespace DigitalRune.Mathematics.Interpolation;

/// <summary>
///       Represents a curve that is defined by piecewise interpolation of curve keys (control points).
///       (Single-precision)
///       </summary>
/// <typeparam name="TPoint">
///       The type of the curve points (such as <see cref="T:DigitalRune.Mathematics.Algebra.Vector2F" />, <see cref="T:DigitalRune.Mathematics.Algebra.Vector3F" />, etc.).
///       </typeparam>
/// <typeparam name="TCurveKey">
///       The type of the curve key. (A type derived from <see cref="T:DigitalRune.Mathematics.Interpolation.CurveKey`2" />.)
///       </typeparam>
/// <typeparam name="TParam">
///       The type of the curve parameter (usually <see cref="T:System.Single" /> or <see cref="T:System.Double" />).
///       </typeparam>
[Serializable]
public abstract class PiecewiseCurveF<TPoint, TCurveKey> : PiecewiseCurve<float, TPoint, TCurveKey> where TCurveKey : CurveKey<float, TPoint>
{
	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Interpolation.PiecewiseCurveF`2" /> class</summary>
	protected PiecewiseCurveF()
	{
	}

	/// <summary>
	///       Gets the index of the curve key <i>before</i> or at the given parameter value.
	///       </summary>
	/// <param name="parameter">The parameter value.</param>
	/// <returns>
	///       The index of the curve key or <c>-1</c> if no suitable curve key exists.
	///       </returns>
	public override int GetKeyIndex(float parameter)
	{
		int count = Count;
		if (count == 0)
		{
			while (true)
			{
				switch (2)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(PiecewiseCurveF<, >.GetKeyIndex(float));
					}
					return -1;
				}
			}
		}
		parameter = LoopParameter(parameter);
		float value = parameter;
		TCurveKey val = Items[0];
		if (Numeric.IsLess(value, val.Parameter))
		{
			return -1;
		}
		float value2 = parameter;
		TCurveKey val2 = Items[count - 1];
		if (Numeric.IsGreater(value2, val2.Parameter))
		{
			while (true)
			{
				switch (5)
				{
				case 0:
					break;
				default:
					return count - 1;
				}
			}
		}
		for (int i = 1; i < count; i++)
		{
			TCurveKey val3 = Items[i];
			if (!(val3.Parameter > parameter))
			{
				continue;
			}
			while (true)
			{
				switch (6)
				{
				case 0:
					continue;
				}
				return i - 1;
			}
		}
		return count - 1;
	}

	/// <summary>
	///       Determines whether the given parameter corresponds to a mirrored oscillation loop.
	///       </summary>
	/// <param name="parameter">The parameter value.</param>
	/// <returns>
	///   <see langword="true" /> if the parameter is in a mirrored oscillation loop; otherwise, 
	///       <see langword="false" />.
	///       </returns>
	public override bool IsInMirroredOscillation(float parameter)
	{
		int count = Count;
		if (count == 0)
		{
			while (true)
			{
				switch (5)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(PiecewiseCurveF<, >.IsInMirroredOscillation(float));
					}
					return false;
				}
			}
		}
		TCurveKey val = Items[0];
		float parameter2 = val.Parameter;
		TCurveKey val2 = Items[count - 1];
		float parameter3 = val2.Parameter;
		float num = parameter3 - parameter2;
		if (Numeric.IsZero(num))
		{
			return false;
		}
		if (parameter < parameter2)
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
			if (PreLoop == CurveLoopType.Oscillate)
			{
				return (int)((parameter2 - parameter) / num) % 2 == 0;
			}
		}
		if (parameter > parameter3 && PostLoop == CurveLoopType.Oscillate)
		{
			return (int)((parameter - parameter3) / num) % 2 == 0;
		}
		return false;
	}

	/// <summary>
	///       Handles pre- and post-looping by changing the given parameter so that it lies on the curve.
	///       </summary>
	/// <param name="parameter">The parameter value.</param>
	/// <returns>The modified parameter value.</returns>
	public override float LoopParameter(float parameter)
	{
		int count = Count;
		if (count == 0)
		{
			return parameter;
		}
		TCurveKey val = Items[0];
		TCurveKey val2 = Items[count - 1];
		float parameter2 = val.Parameter;
		float parameter3 = val2.Parameter;
		float num = parameter3 - parameter2;
		if (parameter < parameter2)
		{
			if (PreLoop == CurveLoopType.Linear)
			{
				while (true)
				{
					switch (2)
					{
					case 0:
						break;
					default:
						if (1 == 0)
						{
							__ldtoken(PiecewiseCurveF<, >.LoopParameter(float));
						}
						return parameter;
					}
				}
			}
			if (PreLoop != CurveLoopType.Constant)
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
				if (count != 1)
				{
					if (Numeric.IsZero(num))
					{
						return parameter2;
					}
					int num2 = (int)((parameter2 - parameter) / num);
					if (PreLoop != CurveLoopType.Cycle)
					{
						if (PreLoop != CurveLoopType.CycleOffset)
						{
							if (num2 % 2 == 0)
							{
								return parameter2 + parameter2 - (parameter + num * (float)num2);
							}
							return parameter + num * (float)(num2 + 1);
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
					return parameter + num * (float)(num2 + 1);
				}
			}
			return parameter2;
		}
		if (parameter > parameter3)
		{
			if (PostLoop == CurveLoopType.Linear)
			{
				return parameter;
			}
			if (PostLoop != CurveLoopType.Constant)
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
				if (count != 1)
				{
					if (Numeric.IsZero(num))
					{
						return parameter2;
					}
					int num3 = (int)((parameter - parameter3) / num);
					if (PostLoop != CurveLoopType.Cycle)
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
						if (PostLoop != CurveLoopType.CycleOffset)
						{
							if (num3 % 2 == 0)
							{
								while (true)
								{
									switch (4)
									{
									case 0:
										break;
									default:
										return parameter3 - (parameter - num * (float)num3 - parameter3);
									}
								}
							}
							return parameter - num * (float)(num3 + 1);
						}
					}
					return parameter - num * (float)(num3 + 1);
				}
			}
			return parameter3;
		}
		return parameter;
	}
}
