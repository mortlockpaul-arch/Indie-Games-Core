using System;
using System.Runtime.InteropServices;
using A;

namespace DigitalRune.Mathematics;

/// <summary>
///       Provides methods for robust floating-point comparisons (for <see langword="float" /> and 
///       <see langword="double" />).
///       </summary>
public static class Numeric
{
	[StructLayout(LayoutKind.Explicit)]
	private struct cdc702daf118f928bc1ca019d4a5e8e09
	{
		[FieldOffset(0)]
		internal float c4a214070176e4ad77db7dd0d5a86a33b;

		[FieldOffset(0)]
		internal uint cda050f670cde7f4d2b300f365a3e3ee0;
	}

	[StructLayout(LayoutKind.Explicit)]
	private struct cc2728d8647d0c51b2198683956d8c66f
	{
		[FieldOffset(0)]
		internal double c26eec981304bf4453dbb7cf4dffc8851;

		[FieldOffset(0)]
		internal ulong cda050f670cde7f4d2b300f365a3e3ee0;
	}

	private static float c8ee234cbd2898574d8933416e4f6b621 = 1E-05f;

	private static float c852684101b0fc0acf3d84674ab75eaaa = 9.9999994E-11f;

	private static double c5b7b1e874dc25fca0cd2506d5fce143e = 1E-12;

	private static double c16fa0ef2b18889ee9c2292badb204e8a = 1E-24;

	/// <summary>
	///       Gets or sets the tolerance value used for comparison of <see langword="float" /> values.
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is negative or 0.
	///       </exception>
	public static float EpsilonF
	{
		get
		{
			return c8ee234cbd2898574d8933416e4f6b621;
		}
		set
		{
			if (value <= 0f)
			{
				throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(588), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(599));
			}
			c8ee234cbd2898574d8933416e4f6b621 = value;
			c852684101b0fc0acf3d84674ab75eaaa = value * value;
		}
	}

	/// <summary>
	///       Gets the squared tolerance value used for comparison of <see langword="float" /> values.
	///       (<see cref="P:DigitalRune.Mathematics.Numeric.EpsilonF" /> * <see cref="P:DigitalRune.Mathematics.Numeric.EpsilonF" />).
	///       </summary>
	public static float EpsilonFSquared => c852684101b0fc0acf3d84674ab75eaaa;

	/// <summary>
	///       Gets or sets the tolerance value used for comparison of <see langword="double" /> values.
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is negative or 0.
	///       </exception>
	public static double EpsilonD
	{
		get
		{
			return c5b7b1e874dc25fca0cd2506d5fce143e;
		}
		set
		{
			if (value <= 0.0)
			{
				throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(588), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(686));
			}
			c5b7b1e874dc25fca0cd2506d5fce143e = value;
			c16fa0ef2b18889ee9c2292badb204e8a = value * value;
		}
	}

	/// <summary>
	///       Gets the squared tolerance value used for comparison of <see langword="double" /> values.
	///       (<see cref="P:DigitalRune.Mathematics.Numeric.EpsilonD" /> * <see cref="P:DigitalRune.Mathematics.Numeric.EpsilonD" />).
	///       </summary>
	public static double EpsilonDSquared => c16fa0ef2b18889ee9c2292badb204e8a;

	/// <summary>
	///       Determines whether two values are equal (regarding the tolerance <see cref="P:DigitalRune.Mathematics.Numeric.EpsilonF" />).
	///       </summary>
	/// <param name="value1">The first value.</param>
	/// <param name="value2">The second value.</param>
	/// <returns>
	///   <see langword="true" /> if the specified values are equal (within the tolerance); 
	///       otherwise, <see langword="false" />.
	///       </returns>
	public static bool AreEqual(float value1, float value2)
	{
		if (value1 == value2)
		{
			return true;
		}
		float num = c8ee234cbd2898574d8933416e4f6b621 * (Math.Abs(value1) + Math.Abs(value2) + 1f);
		float num2 = value1 - value2;
		if (0f - num < num2)
		{
			return num2 < num;
		}
		return false;
	}

	/// <summary>
	///       Determines whether two values are equal (regarding the tolerance <see cref="P:DigitalRune.Mathematics.Numeric.EpsilonD" />).
	///       </summary>
	/// <param name="value1">The first value.</param>
	/// <param name="value2">The second value.</param>
	/// <returns>
	///   <see langword="true" /> if the specified values are equal (within the tolerance); 
	///       otherwise, <see langword="false" />.
	///       </returns>
	public static bool AreEqual(double value1, double value2)
	{
		if (value1 == value2)
		{
			return true;
		}
		double num = c5b7b1e874dc25fca0cd2506d5fce143e * (Math.Abs(value1) + Math.Abs(value2) + 1.0);
		double num2 = value1 - value2;
		if (0.0 - num < num2)
		{
			return num2 < num;
		}
		return false;
	}

	/// <summary>
	///       Determines whether two values are equal (regarding a specific tolerance).
	///       </summary>
	/// <param name="value1">The first value.</param>
	/// <param name="value2">The second value.</param>
	/// <param name="epsilon">The tolerance value.</param>
	/// <returns>
	///   <see langword="true" /> if the specified values are equal (within the tolerance); 
	///       otherwise, <see langword="false" />.
	///       </returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="epsilon" /> is negative or 0.
	///       </exception>
	public static bool AreEqual(float value1, float value2, float epsilon)
	{
		if (epsilon <= 0f)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(789), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(804));
		}
		if (value1 == value2)
		{
			return true;
		}
		float num = value1 - value2;
		if (0f - epsilon < num)
		{
			return num < epsilon;
		}
		return false;
	}

	/// <summary>
	///       Determines whether two values are equal (regarding a specific tolerance).
	///       </summary>
	/// <param name="value1">The first value.</param>
	/// <param name="value2">The second value.</param>
	/// <param name="epsilon">The tolerance value.</param>
	/// <returns>
	///   <see langword="true" /> if the specified values are equal (within the tolerance); 
	///       otherwise, <see langword="false" />.
	///       </returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="epsilon" /> is negative or 0.
	///       </exception>
	public static bool AreEqual(double value1, double value2, double epsilon)
	{
		if (epsilon <= 0.0)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(789), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(804));
		}
		if (value1 == value2)
		{
			return true;
		}
		double num = value1 - value2;
		if (0.0 - epsilon < num)
		{
			return num < epsilon;
		}
		return false;
	}

	/// <summary>
	///       Determines whether a value is less than another value (regarding the tolerance 
	///       <see cref="P:DigitalRune.Mathematics.Numeric.EpsilonF" />).
	///       </summary>
	/// <param name="value1">The first value.</param>
	/// <param name="value2">The second value.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="value1" /> &lt; <paramref name="value2" /> and 
	///       the difference between <paramref name="value1" /> and <paramref name="value2" /> is greater 
	///       than or equal to the epsilon tolerance; otherwise, <see langword="false" />.
	///       </returns>
	public static bool IsLess(float value1, float value2)
	{
		if (value1 < value2)
		{
			return !AreEqual(value1, value2);
		}
		return false;
	}

	/// <summary>
	///       Determines whether a value is less than another value (regarding the specified tolerance).
	///       </summary>
	/// <param name="value1">The first value.</param>
	/// <param name="value2">The second value.</param>
	/// <param name="epsilon">The tolerance value.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="value1" /> &lt; <paramref name="value2" /> and 
	///       the difference between <paramref name="value1" /> and <paramref name="value2" /> is greater 
	///       than or equal to the epsilon tolerance; otherwise, <see langword="false" />.
	///       </returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="epsilon" /> is negative or 0.
	///       </exception>
	public static bool IsLess(float value1, float value2, float epsilon)
	{
		if (value1 < value2)
		{
			return !AreEqual(value1, value2, epsilon);
		}
		return false;
	}

	/// <summary>
	///       Determines whether a value is less than another value (regarding the tolerance 
	///       <see cref="P:DigitalRune.Mathematics.Numeric.EpsilonD" />).
	///       </summary>
	/// <param name="value1">The first value.</param>
	/// <param name="value2">The second value.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="value1" /> &lt; <paramref name="value2" /> and 
	///       the difference between <paramref name="value1" /> and <paramref name="value2" /> is greater 
	///       than or equal to the epsilon tolerance; otherwise, <see langword="false" />.
	///       </returns>
	public static bool IsLess(double value1, double value2)
	{
		if (value1 < value2)
		{
			return !AreEqual(value1, value2);
		}
		return false;
	}

	/// <summary>
	///       Determines whether a value is less than another value (regarding the specified tolerance).
	///       </summary>
	/// <param name="value1">The first value.</param>
	/// <param name="value2">The second value.</param>
	/// <param name="epsilon">The tolerance value.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="value1" /> &lt; <paramref name="value2" /> and 
	///       the difference between <paramref name="value1" /> and <paramref name="value2" /> is greater 
	///       than or equal to the epsilon tolerance; otherwise, <see langword="false" />.
	///       </returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="epsilon" /> is negative or 0.
	///       </exception>
	public static bool IsLess(double value1, double value2, double epsilon)
	{
		if (value1 < value2)
		{
			return !AreEqual(value1, value2, epsilon);
		}
		return false;
	}

	/// <summary>
	///       Determines whether a value is less than or equal to another value (regarding the tolerance 
	///       <see cref="P:DigitalRune.Mathematics.Numeric.EpsilonF" />).
	///       </summary>
	/// <param name="value1">The first value.</param>
	/// <param name="value2">The second value.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="value1" /> ≤ <paramref name="value2" /> or their 
	///       difference is less than the epsilon tolerance; otherwise, <see langword="false" />.
	///       </returns>
	public static bool IsLessOrEqual(float value1, float value2)
	{
		if (!(value1 < value2))
		{
			return AreEqual(value1, value2);
		}
		return true;
	}

	/// <summary>
	///       Determines whether a value is less than or equal to another value (regarding the specified tolerance).
	///       </summary>
	/// <param name="value1">The first value.</param>
	/// <param name="value2">The second value.</param>
	/// <param name="epsilon">The tolerance value.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="value1" /> ≤ <paramref name="value2" /> or their 
	///       difference is less than the epsilon tolerance; otherwise, <see langword="false" />.
	///       </returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="epsilon" /> is negative or 0.
	///       </exception>
	public static bool IsLessOrEqual(float value1, float value2, float epsilon)
	{
		if (!(value1 < value2))
		{
			return AreEqual(value1, value2, epsilon);
		}
		return true;
	}

	/// <summary>
	///       Determines whether a value is less than or equal to another value (regarding the tolerance 
	///       <see cref="P:DigitalRune.Mathematics.Numeric.EpsilonD" />).
	///       </summary>
	/// <param name="value1">The first value.</param>
	/// <param name="value2">The second value.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="value1" /> ≤ <paramref name="value2" /> or their 
	///       difference is less than the epsilon tolerance; otherwise, <see langword="false" />.
	///       </returns>
	public static bool IsLessOrEqual(double value1, double value2)
	{
		if (!(value1 < value2))
		{
			return AreEqual(value1, value2);
		}
		return true;
	}

	/// <summary>
	///       Determines whether a value is less than or equal to another value (regarding the specified 
	///       tolerance).
	///       </summary>
	/// <param name="value1">The first value.</param>
	/// <param name="value2">The second value.</param>
	/// <param name="epsilon">The tolerance value.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="value1" /> ≤ <paramref name="value2" /> or their 
	///       difference is less than the epsilon tolerance; otherwise, <see langword="false" />.
	///       </returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="epsilon" /> is negative or 0.
	///       </exception>
	public static bool IsLessOrEqual(double value1, double value2, double epsilon)
	{
		if (!(value1 < value2))
		{
			return AreEqual(value1, value2, epsilon);
		}
		return true;
	}

	/// <summary>
	///       Determines whether a value is greater than another value (regarding the tolerance 
	///       <see cref="P:DigitalRune.Mathematics.Numeric.EpsilonF" />).
	///       </summary>
	/// <param name="value1">The first value.</param>
	/// <param name="value2">The second value.</param>
	/// <returns>
	///   <see langword="true" /> if the difference between <paramref name="value1" /> and 
	///       <paramref name="value2" /> is greater than or equal to the epsilon tolerance and 
	///       <paramref name="value1" /> &gt; <paramref name="value2" />; 
	///       otherwise, <see langword="false" />.
	///       </returns>
	public static bool IsGreater(float value1, float value2)
	{
		if (value1 > value2)
		{
			return !AreEqual(value1, value2);
		}
		return false;
	}

	/// <summary>
	///       Determines whether a value is greater than another value (regarding the specified tolerance).
	///       </summary>
	/// <param name="value1">The first value.</param>
	/// <param name="value2">The second value.</param>
	/// <param name="epsilon">The tolerance value.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="value1" /> &gt; <paramref name="value2" /> and 
	///       the difference between <paramref name="value1" /> and <paramref name="value2" /> is greater 
	///       than or equal to the epsilon tolerance; 
	///       otherwise, <see langword="false" />.
	///       </returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="epsilon" /> is negative or 0.
	///       </exception>
	public static bool IsGreater(float value1, float value2, float epsilon)
	{
		if (value1 > value2)
		{
			return !AreEqual(value1, value2, epsilon);
		}
		return false;
	}

	/// <summary>
	///       Determines whether a value is greater than another value (regarding the tolerance 
	///       <see cref="P:DigitalRune.Mathematics.Numeric.EpsilonD" />).
	///       </summary>
	/// <param name="value1">The first value.</param>
	/// <param name="value2">The second value.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="value1" /> &gt; <paramref name="value2" /> and 
	///       the difference between <paramref name="value1" /> and <paramref name="value2" /> is greater 
	///       than or equal to the epsilon tolerance; 
	///       otherwise, <see langword="false" />.
	///       </returns>
	public static bool IsGreater(double value1, double value2)
	{
		if (value1 > value2)
		{
			return !AreEqual(value1, value2);
		}
		return false;
	}

	/// <summary>
	///       Determines whether a value is greater than another value (regarding the specified tolerance).
	///       </summary>
	/// <param name="value1">The first value.</param>
	/// <param name="value2">The second value.</param>
	/// <param name="epsilon">The tolerance value.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="value1" /> &gt; <paramref name="value2" /> and 
	///       the difference between <paramref name="value1" /> and <paramref name="value2" /> is greater 
	///       than or equal to the epsilon tolerance; 
	///       otherwise, <see langword="false" />.
	///       </returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="epsilon" /> is negative or 0.
	///       </exception>
	public static bool IsGreater(double value1, double value2, double epsilon)
	{
		if (value1 > value2)
		{
			return !AreEqual(value1, value2, epsilon);
		}
		return false;
	}

	/// <summary>
	///       Determines whether a value is greater than or equal to another value (regarding the tolerance 
	///       <see cref="P:DigitalRune.Mathematics.Numeric.EpsilonF" />).
	///       </summary>
	/// <param name="value1">The first value.</param>
	/// <param name="value2">The second value.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="value1" /> ≥ <paramref name="value2" /> or their 
	///       difference is less than the epsilon tolerance; otherwise, <see langword="false" />.
	///       </returns>
	public static bool IsGreaterOrEqual(float value1, float value2)
	{
		if (!(value1 > value2))
		{
			return AreEqual(value1, value2);
		}
		return true;
	}

	/// <summary>
	///       Determines whether a value is greater than or equal to another value (regarding the 
	///       specified tolerance).
	///       </summary>
	/// <param name="value1">The first value.</param>
	/// <param name="value2">The second value.</param>
	/// <param name="epsilon">The tolerance value.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="value1" /> ≥ <paramref name="value2" /> or their 
	///       difference is less than the epsilon tolerance; otherwise, <see langword="false" />.
	///       </returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="epsilon" /> is negative or 0.
	///       </exception>
	public static bool IsGreaterOrEqual(float value1, float value2, float epsilon)
	{
		if (!(value1 > value2))
		{
			return AreEqual(value1, value2, epsilon);
		}
		return true;
	}

	/// <summary>
	///       Determines whether a value is greater than or equal to another value (regarding the tolerance 
	///       <see cref="P:DigitalRune.Mathematics.Numeric.EpsilonD" />).
	///       </summary>
	/// <param name="value1">The first value.</param>
	/// <param name="value2">The second value.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="value1" /> ≥ <paramref name="value2" /> or their 
	///       difference is less than the epsilon tolerance; otherwise, <see langword="false" />.
	///       </returns>
	public static bool IsGreaterOrEqual(double value1, double value2)
	{
		if (!(value1 > value2))
		{
			return AreEqual(value1, value2);
		}
		return true;
	}

	/// <summary>
	///       Determines whether a value is greater than or equal to another value (regarding the 
	///       specified tolerance).
	///       </summary>
	/// <param name="value1">The first value.</param>
	/// <param name="value2">The second value.</param>
	/// <param name="epsilon">The tolerance value.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="value1" /> ≥ <paramref name="value2" /> or their 
	///       difference is less than the epsilon tolerance; otherwise, <see langword="false" />.
	///       </returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="epsilon" /> is negative or 0.
	///       </exception>
	public static bool IsGreaterOrEqual(double value1, double value2, double epsilon)
	{
		if (!(value1 > value2))
		{
			return AreEqual(value1, value2, epsilon);
		}
		return true;
	}

	/// <summary>
	///       Clamps near-zero values to zero.
	///       </summary>
	/// <param name="value">The value.</param>
	/// <returns>0 if the value is nearly zero (within the tolerance <see cref="P:DigitalRune.Mathematics.Numeric.EpsilonF" />) 
	///       or the original value otherwise.</returns>
	public static float ClampToZero(float value)
	{
		if (!IsZero(value))
		{
			return value;
		}
		return 0f;
	}

	/// <summary>
	///       Clamps near-zero values to zero.
	///       </summary>
	/// <param name="value">The value.</param>
	/// <param name="epsilon">The tolerance value.</param>
	/// <returns>
	///       0 if the value is nearly zero (within the tolerance <paramref name="epsilon" />)
	///       or the original value otherwise.
	///       </returns>
	public static float ClampToZero(float value, float epsilon)
	{
		if (!IsZero(value, epsilon))
		{
			return value;
		}
		return 0f;
	}

	/// <summary>
	///       Clamps near-zero values to zero.
	///       </summary>
	/// <param name="value">The value.</param>
	/// <returns>0 if the value is nearly zero (within the tolerance <see cref="P:DigitalRune.Mathematics.Numeric.EpsilonF" />) 
	///       or the original value otherwise.</returns>
	public static double ClampToZero(double value)
	{
		if (!IsZero(value))
		{
			return value;
		}
		return 0.0;
	}

	/// <summary>
	///       Clamps near-zero values to zero.
	///       </summary>
	/// <param name="value">The value.</param>
	/// <param name="epsilon">The tolerance value.</param>
	/// <returns>
	///       0 if the value is nearly zero (within the tolerance <paramref name="epsilon" />)
	///       or the original value otherwise.
	///       </returns>
	public static double ClampToZero(double value, double epsilon)
	{
		if (!IsZero(value, epsilon))
		{
			return value;
		}
		return 0.0;
	}

	/// <summary>
	///       Determines whether a value is zero (regarding the tolerance <see cref="P:DigitalRune.Mathematics.Numeric.EpsilonF" />).
	///       </summary>
	/// <param name="value">The value to test.</param>
	/// <returns>
	///   <see langword="true" /> if the specified value is zero (within the tolerance); 
	///       otherwise, <see langword="false" />.
	///       </returns>
	public static bool IsZero(float value)
	{
		if (0f - c8ee234cbd2898574d8933416e4f6b621 < value)
		{
			return value < c8ee234cbd2898574d8933416e4f6b621;
		}
		return false;
	}

	/// <summary>
	///       Determines whether a value is zero (regarding the tolerance 
	///       <see cref="P:DigitalRune.Mathematics.Numeric.EpsilonD" />).
	///       </summary>
	/// <param name="value">The value to test.</param>
	/// <returns>
	///   <see langword="true" /> if the specified value is zero (within the tolerance); 
	///       otherwise, <see langword="false" />.
	///       </returns>
	public static bool IsZero(double value)
	{
		if (0.0 - c5b7b1e874dc25fca0cd2506d5fce143e < value)
		{
			return value < c5b7b1e874dc25fca0cd2506d5fce143e;
		}
		return false;
	}

	/// <summary>
	///       Determines whether a value is zero (regarding a specific tolerance).
	///       </summary>
	/// <param name="value">The value to test.</param>
	/// <param name="epsilon">The tolerance value.</param>
	/// <returns>
	///   <see langword="true" /> if the specified value is zero (within the tolerance); 
	///       otherwise, <see langword="false" />.
	///       </returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="epsilon" /> is negative or 0.
	///       </exception>
	public static bool IsZero(float value, float epsilon)
	{
		if (epsilon <= 0f)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(789), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(804));
		}
		if (0f - epsilon < value)
		{
			return value < epsilon;
		}
		return false;
	}

	/// <summary>
	///       Determines whether a value is zero (regarding a specific tolerance).
	///       </summary>
	/// <param name="value">The value to test.</param>
	/// <param name="epsilon">The tolerance value.</param>
	/// <returns>
	///   <see langword="true" /> if the specified value is zero (within the tolerance); 
	///       otherwise, <see langword="false" />.
	///       </returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="epsilon" /> is negative or 0.
	///       </exception>
	public static bool IsZero(double value, double epsilon)
	{
		if (epsilon <= 0.0)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(789), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(804));
		}
		if (0.0 - epsilon < value)
		{
			return value < epsilon;
		}
		return false;
	}

	/// <summary>
	///       Compares two float values (regarding the tolerance <see cref="P:DigitalRune.Mathematics.Numeric.EpsilonF" />).
	///       </summary>
	/// <param name="value1">The first value.</param>
	/// <param name="value2">The second value.</param>
	/// <returns>
	///   <c>-1</c> if value1 is less than value2, <c>+1</c> if value1 is greater than value2, and 
	///       <c>0</c> if value1 is equal to value2 (within the tolerance <see cref="P:DigitalRune.Mathematics.Numeric.EpsilonF" />).
	///       </returns>
	public static int Compare(float value1, float value2)
	{
		if (AreEqual(value1, value2))
		{
			return 0;
		}
		if (!(value1 < value2))
		{
			return 1;
		}
		return -1;
	}

	/// <summary>
	///       Compares two double values (regarding the tolerance <see cref="P:DigitalRune.Mathematics.Numeric.EpsilonD" />).
	///       </summary>
	/// <param name="value1">The first value.</param>
	/// <param name="value2">The second value.</param>
	/// <returns>
	///   <c>-1</c> if value1 is less than value2, <c>+1</c> if value1 is greater than value2, and 
	///       <c>0</c> if value1 is equal to value2 (within the tolerance <see cref="P:DigitalRune.Mathematics.Numeric.EpsilonD" />).
	///       </returns>
	public static int Compare(double value1, double value2)
	{
		if (AreEqual(value1, value2))
		{
			return 0;
		}
		if (!(value1 < value2))
		{
			return 1;
		}
		return -1;
	}

	/// <summary>
	///       Compares two float values (regarding a specific tolerance).
	///       </summary>
	/// <param name="value1">The first value.</param>
	/// <param name="value2">The second value.</param>
	/// <param name="epsilon">The tolerance value for equality.</param>
	/// <returns>
	///   <c>-1</c> if value1 is less than value2, <c>+1</c> if value1 is greater than value2, and 
	///       <c>0</c> if value1 is equal to value2 (within the tolerance <paramref name="epsilon" />).
	///       </returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="epsilon" /> is negative or 0.
	///       </exception>
	public static int Compare(float value1, float value2, float epsilon)
	{
		if (AreEqual(value1, value2, epsilon))
		{
			return 0;
		}
		if (!(value1 < value2))
		{
			return 1;
		}
		return -1;
	}

	/// <summary>
	///       Compares two double values (regarding a specific tolerance).
	///       </summary>
	/// <param name="value1">The first value.</param>
	/// <param name="value2">The second value.</param>
	/// <param name="epsilon">The tolerance value for equality.</param>
	/// <returns>
	///   <c>-1</c> if value1 is less than value2, <c>+1</c> if value1 is greater than value2, and 
	///       <c>0</c> if value1 is equal to value2 (within the tolerance <paramref name="epsilon" />).
	///       </returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="epsilon" /> is negative or 0.
	///       </exception>
	public static int Compare(double value1, double value2, double epsilon)
	{
		if (AreEqual(value1, value2, epsilon))
		{
			return 0;
		}
		if (!(value1 < value2))
		{
			return 1;
		}
		return -1;
	}

	/// <summary>
	///       Determines whether the specified value is finite or <see cref="F:System.Single.NaN" />.
	///       </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="value" /> is finite or <see cref="F:System.Single.NaN" />; 
	///       otherwise, <see langword="false" />.
	///       </returns>
	public static bool IsFiniteOrNaN(float value)
	{
		return !float.IsInfinity(value);
	}

	/// <summary>
	///       Determines whether the specified value is finite or <see cref="F:System.Double.NaN" />.
	///       </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="value" /> is finite or <see cref="F:System.Double.NaN" />; 
	///       otherwise, <see langword="false" />.
	///       </returns>
	public static bool IsFiniteOrNaN(double value)
	{
		return !double.IsInfinity(value);
	}

	/// <summary>
	///       Determines whether the specified value is finite.
	///       </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="value" /> is finite; otherwise, 
	///       <see langword="false" />.
	///       </returns>
	public static bool IsFinite(float value)
	{
		if (!IsNaN(value))
		{
			return !float.IsInfinity(value);
		}
		return false;
	}

	/// <summary>
	///       Determines whether the specified value is finite.
	///       </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="value" /> is finite; otherwise, 
	///       <see langword="false" />.
	///       </returns>
	public static bool IsFinite(double value)
	{
		if (!IsNaN(value))
		{
			return !double.IsInfinity(value);
		}
		return false;
	}

	/// <summary>
	///       Determines whether the specified value is positive.
	///       </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="value" /> is positive; otherwise, 
	///       <see langword="false" />.
	///       </returns>
	public static bool IsPositive(float value)
	{
		return 0f < value;
	}

	/// <summary>
	///       Determines whether the specified value is positive.
	///       </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="value" /> is positive; otherwise, 
	///       <see langword="false" />.
	///       </returns>
	public static bool IsPositive(double value)
	{
		return 0.0 < value;
	}

	/// <summary>
	///       Determines whether the specified value is negative.
	///       </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="value" /> is negative; otherwise, 
	///       <see langword="false" />.
	///       </returns>
	public static bool IsNegative(float value)
	{
		return value < 0f;
	}

	/// <summary>
	///       Determines whether the specified value is negative.
	///       </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="value" /> is negative; otherwise, 
	///       <see langword="false" />.
	///       </returns>
	public static bool IsNegative(double value)
	{
		return value < 0.0;
	}

	/// <summary>
	///       Determines whether the specified value is positive finite.
	///       </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="value" /> is a positive finite number; otherwise, 
	///       <see langword="false" />.
	///       </returns>
	public static bool IsPositiveFinite(float value)
	{
		if (0f < value)
		{
			return value != float.PositiveInfinity;
		}
		return false;
	}

	/// <summary>
	///       Determines whether the specified value is positive finite.
	///       </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="value" /> is a positive finite number; otherwise, 
	///       <see langword="false" />.
	///       </returns>
	public static bool IsPositiveFinite(double value)
	{
		if (0.0 < value)
		{
			return value != double.PositiveInfinity;
		}
		return false;
	}

	/// <summary>
	///       Determines whether the specified value is negative finite.
	///       </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="value" /> is a negative finite number; otherwise, 
	///       <see langword="false" />.
	///       </returns>
	public static bool IsNegativeFinite(float value)
	{
		if (value < 0f)
		{
			return value != float.NegativeInfinity;
		}
		return false;
	}

	/// <summary>
	///       Determines whether the specified value is negative finite.
	///       </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="value" /> is a negative finite number; otherwise, 
	///       <see langword="false" />.
	///       </returns>
	public static bool IsNegativeFinite(double value)
	{
		if (value < 0.0)
		{
			return value != double.NegativeInfinity;
		}
		return false;
	}

	/// <summary>
	///       Determines whether the specified value is 0 or positive finite.
	///       </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="value" /> is 0 or a positive finite number; 
	///       otherwise, <see langword="false" />.
	///       </returns>
	public static bool IsZeroOrPositiveFinite(float value)
	{
		if (0f <= value)
		{
			return value != float.PositiveInfinity;
		}
		return false;
	}

	/// <summary>
	///       Determines whether the specified value is 0 or positive finite.
	///       </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="value" /> is 0 or a positive finite number; 
	///       otherwise, <see langword="false" />.
	///       </returns>
	public static bool IsZeroOrPositiveFinite(double value)
	{
		if (0.0 <= value)
		{
			return value != double.PositiveInfinity;
		}
		return false;
	}

	/// <summary>
	///       Determines whether the specified value is 0 or negative finite.
	///       </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="value" /> is 0 or a negative finite number; 
	///       otherwise, <see langword="false" />.
	///       </returns>
	public static bool IsZeroOrNegativeFinite(float value)
	{
		if (value <= 0f)
		{
			return value != float.NegativeInfinity;
		}
		return false;
	}

	/// <summary>
	///       Determines whether the specified value is 0 or negative finite.
	///       </summary>
	/// <param name="value">The value.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="value" /> is 0 or a negative finite number; 
	///       otherwise, <see langword="false" />.
	///       </returns>
	public static bool IsZeroOrNegativeFinite(double value)
	{
		if (value <= 0.0)
		{
			return value != double.NegativeInfinity;
		}
		return false;
	}

	/// <summary>
	///       Returns a value indicating whether the specified number evaluates to not a number 
	///       (<see cref="F:System.Double.NaN" />).
	///       </summary>
	/// <param name="f">A double-precision floating-point number.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="f" /> evaluates to not a number 
	///       (<see cref="F:System.Double.NaN" />); otherwise, <see langword="false" />.
	///       </returns>
	public static bool IsNaN(float f)
	{
		cdc702daf118f928bc1ca019d4a5e8e09 cdc702daf118f928bc1ca019d4a5e8e10 = new cdc702daf118f928bc1ca019d4a5e8e09
		{
			c4a214070176e4ad77db7dd0d5a86a33b = f
		};
		uint num = cdc702daf118f928bc1ca019d4a5e8e10.cda050f670cde7f4d2b300f365a3e3ee0 & 0x7F800000;
		uint num2 = cdc702daf118f928bc1ca019d4a5e8e10.cda050f670cde7f4d2b300f365a3e3ee0 & 0x7FFFFF;
		if (num == 2139095040)
		{
			return num2 != 0;
		}
		return false;
	}

	/// <summary>
	///       Returns a value indicating whether the specified number evaluates to not a number 
	///       (<see cref="F:System.Double.NaN" />).
	///       </summary>
	/// <param name="d">A double-precision floating-point number.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="d" /> evaluates to not a number 
	///       (<see cref="F:System.Double.NaN" />); otherwise, <see langword="false" />.
	///       </returns>
	public static bool IsNaN(double d)
	{
		cc2728d8647d0c51b2198683956d8c66f cc2728d8647d0c51b2198683956d8c66f2 = new cc2728d8647d0c51b2198683956d8c66f
		{
			c26eec981304bf4453dbb7cf4dffc8851 = d
		};
		ulong num = cc2728d8647d0c51b2198683956d8c66f2.cda050f670cde7f4d2b300f365a3e3ee0 & 0x7FF0000000000000L;
		ulong num2 = cc2728d8647d0c51b2198683956d8c66f2.cda050f670cde7f4d2b300f365a3e3ee0 & 0xFFFFFFFFFFFFFL;
		if (num == 9218868437227405312L)
		{
			return num2 != 0;
		}
		return false;
	}
}
