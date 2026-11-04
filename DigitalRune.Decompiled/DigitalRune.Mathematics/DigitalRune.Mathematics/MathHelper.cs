using System;

namespace DigitalRune.Mathematics;

/// <summary>
///       Provides useful mathematical algorithms and functions.
///       </summary>
public static class MathHelper
{
	/// <summary>
	///       Clamps the specified value.
	///       </summary>
	/// <param name="value">The value which should be clamped.</param>
	/// <param name="min">The min limit.</param>
	/// <param name="max">The max limit.</param>
	/// <typeparam name="T">The type of the value.</typeparam>
	/// <returns>
	///   <paramref name="value" /> clamped to the interval
	///       [<paramref name="min" />, <paramref name="max" />].
	///       </returns>
	public static T Clamp<T>(T value, T min, T max) where T : IComparable<T>
	{
		if (min.CompareTo(max) > 0)
		{
			T val = max;
			max = min;
			min = val;
		}
		T other = min;
		if (value.CompareTo(other) < 0)
		{
			value = min;
		}
		else if (value.CompareTo(max) > 0)
		{
			value = max;
		}
		return value;
	}

	/// <summary>
	///       Computes Sqrt(a*a + b*b) without underflow/overflow (single-precision).
	///       </summary>
	/// <param name="cathetusA">Cathetus a.</param>
	/// <param name="cathetusB">Cathetus b.</param>
	/// <returns>The hypotenuse c, which is Sqrt(a*a + b*b).</returns>
	public static float Hypotenuse(float cathetusA, float cathetusB)
	{
		float result = 0f;
		if (Math.Abs(cathetusA) > Math.Abs(cathetusB))
		{
			result = cathetusB / cathetusA;
			result = (float)((double)Math.Abs(cathetusA) * Math.Sqrt(1f + result * result));
		}
		else if (cathetusB != 0f)
		{
			result = cathetusA / cathetusB;
			result = (float)((double)Math.Abs(cathetusB) * Math.Sqrt(1f + result * result));
		}
		return result;
	}

	/// <summary>
	///       Computes Sqrt(a*a + b*b) without underflow/overflow (double-precision).
	///       </summary>
	/// <param name="cathetusA">Cathetus a.</param>
	/// <param name="cathetusB">Cathetus b.</param>
	/// <returns>The hypotenuse c, which is Sqrt(a*a + b*b).</returns>
	public static double Hypotenuse(double cathetusA, double cathetusB)
	{
		double result = 0.0;
		if (Math.Abs(cathetusA) > Math.Abs(cathetusB))
		{
			result = cathetusB / cathetusA;
			result = Math.Abs(cathetusA) * Math.Sqrt(1.0 + result * result);
		}
		else if (cathetusB != 0.0)
		{
			result = cathetusA / cathetusB;
			result = Math.Abs(cathetusB) * Math.Sqrt(1.0 + result * result);
		}
		return result;
	}

	/// <summary>
	///       Swaps the content of two variables.
	///       </summary>
	/// <param name="obj1">First variable.</param>
	/// <param name="obj2">Second variable.</param>
	/// <typeparam name="T">The type of the objects.</typeparam>
	public static void Swap<T>(ref T obj1, ref T obj2)
	{
		T val = obj1;
		obj1 = obj2;
		obj2 = val;
	}

	/// <summary>
	///       Converts an angle value from degrees to radians (single-precision).
	///       </summary>
	/// <param name="degree">The angle in degrees.</param>
	/// <returns>The angle in radians.</returns>
	public static float ToRadians(float degree)
	{
		return degree * (float)Math.PI / 180f;
	}

	/// <summary>
	///       Converts an angle value from degrees to radians (double-precision).
	///       </summary>
	/// <param name="degree">The angle in degrees.</param>
	/// <returns>The angle in radians.</returns>
	public static double ToRadians(double degree)
	{
		return degree * Math.PI / 180.0;
	}

	/// <summary>
	///       Converts an angle value from radians to degrees (single-precision).
	///       </summary>
	/// <param name="radians">The angle in radians.</param>
	/// <returns>The angle in degrees.</returns>
	public static float ToDegrees(float radians)
	{
		return radians * 180f * (1f / (float)Math.PI);
	}

	/// <summary>
	///       Converts an angle value from radians to degrees (double-precision).
	///       </summary>
	/// <param name="radians">The angle in radians.</param>
	/// <returns>The angle in degrees.</returns>
	public static double ToDegrees(double radians)
	{
		return radians * 180.0 * (1.0 / Math.PI);
	}

	/// <summary>
	///       Returns the largest non-negative integer x such that 2<sup>x</sup> ≤ <paramref name="value" />.
	///       </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	///       The largest non-negative integer x such that 2<sup>x</sup> ≤ <paramref name="value" />.
	///       Exception: If <paramref name="value" /> is 0 then 0 is returned.
	///       </returns>
	[CLSCompliant(false)]
	public static uint Log2LessOrEqual(uint value)
	{
		uint num;
		uint num2;
		if (value >= 65536)
		{
			num = 16u;
			num2 = 16777216u;
		}
		else
		{
			num = 0u;
			num2 = 256u;
		}
		if (value >= num2)
		{
			num += 8;
			num2 <<= 4;
		}
		else
		{
			num2 >>= 4;
		}
		if (value >= num2)
		{
			num += 4;
			num2 <<= 2;
		}
		else
		{
			num2 >>= 2;
		}
		if (value >= num2)
		{
			num += 2;
			num2 <<= 1;
		}
		else
		{
			num2 >>= 1;
		}
		if (value >= num2)
		{
			num++;
		}
		return num;
	}

	/// <summary>
	///       Returns the smallest non-negative integer x such that 2<sup>x</sup> ≥ <paramref name="value" />.
	///       </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	///       The smallest non-negative integer x such that 2<sup>x</sup> ≥ <paramref name="value" />.
	///       Exception: If <paramref name="value" /> is 0, 0 is returned.
	///       </returns>
	[CLSCompliant(false)]
	public static uint Log2GreaterOrEqual(uint value)
	{
		if (value > 2147483648u)
		{
			return 32u;
		}
		uint num;
		uint num2;
		if (value > 32768)
		{
			num = 16u;
			num2 = 8388608u;
		}
		else
		{
			num = 0u;
			num2 = 128u;
		}
		if (value > num2)
		{
			num += 8;
			num2 <<= 4;
		}
		else
		{
			num2 >>= 4;
		}
		if (value > num2)
		{
			num += 4;
			num2 <<= 2;
		}
		else
		{
			num2 >>= 2;
		}
		if (value > num2)
		{
			num += 2;
			num2 <<= 1;
		}
		else
		{
			num2 >>= 1;
		}
		if (value > num2)
		{
			num++;
		}
		return num;
	}

	/// <summary>
	///       Creates the smallest bitmask that is greater than or equal to the given value.
	///       </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	///       A bitmask where the left bits are 0 and the right bits are 1. The value of the bitmask
	///       is ≥ <paramref name="value" />.
	///       </returns>
	[CLSCompliant(false)]
	public static uint Bitmask(uint value)
	{
		value |= value >> 1;
		value |= value >> 2;
		value |= value >> 4;
		value |= value >> 8;
		value |= value >> 16;
		return value;
	}

	/// <summary>
	///       Computes the Gaussian function y = k * e^( -(x-μ)<sup>2</sup>/(2σ<sup>2</sup>) 
	///       (single precision).
	///       </summary>
	/// <param name="x">The argument x.</param>
	/// <param name="coefficient">The coefficient k.</param>
	/// <param name="expectedValue">The expected value μ.</param>
	/// <param name="standardDeviation">The standard deviation σ.</param>
	/// <returns>The height of the Gaussian bell curve at x.</returns>
	public static float Gaussian(float x, float coefficient, float expectedValue, float standardDeviation)
	{
		float num = x - expectedValue;
		return coefficient * (float)Math.Exp((0f - num) * num / (2f * standardDeviation * standardDeviation));
	}

	/// <summary>
	///   <summary>
	///       Computes the Gaussian function y = k * e^( -(x-μ)<sup>2</sup>/(2σ<sup>2</sup>) 
	///       (double-precision).
	///       </summary>
	/// </summary>
	/// <param name="x">The argument x.</param>
	/// <param name="coefficient">The coefficient k.</param>
	/// <param name="expectedValue">The expected value μ.</param>
	/// <param name="standardDeviation">The standard deviation σ.</param>
	/// <returns>The height of the Gaussian bell curve at x.</returns>
	public static double Gaussian(double x, double coefficient, double expectedValue, double standardDeviation)
	{
		double num = x - expectedValue;
		return coefficient * Math.Exp((0.0 - num) * num / (2.0 * standardDeviation * standardDeviation));
	}
}
