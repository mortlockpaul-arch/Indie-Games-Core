using System;
using System.Runtime.CompilerServices;

namespace DigitalRune.Mathematics.Interpolation;

/// <summary>
///       Defines a single segment of a 1-dimensional cubic Catmull-Rom spline (single-precision).
///       </summary>
public class CatmullRomSegment1F : ICurve<float, float>
{
	[CompilerGenerated]
	private float c99977f6a59a72d189b9fc0d458aca839;

	[CompilerGenerated]
	private float c0b42aca19031beb107b8358dd021f2c6;

	[CompilerGenerated]
	private float cef39567e827b1e2e96dc03e9a98c8c6e;

	[CompilerGenerated]
	private float ce03bf79825fa8aa2e40810bcb43e63d7;

	/// <summary>
	///       Gets or sets the previous point.
	///       </summary>
	public float Point1
	{
		[CompilerGenerated]
		get
		{
			return c99977f6a59a72d189b9fc0d458aca839;
		}
		[CompilerGenerated]
		set
		{
			c99977f6a59a72d189b9fc0d458aca839 = value;
		}
	}

	/// <summary>
	///       Gets or sets the start point.
	///       </summary>
	public float Point2
	{
		[CompilerGenerated]
		get
		{
			return c0b42aca19031beb107b8358dd021f2c6;
		}
		[CompilerGenerated]
		set
		{
			c0b42aca19031beb107b8358dd021f2c6 = value;
		}
	}

	/// <summary>
	///       Gets or sets the end point.
	///       </summary>
	public float Point3
	{
		[CompilerGenerated]
		get
		{
			return cef39567e827b1e2e96dc03e9a98c8c6e;
		}
		[CompilerGenerated]
		set
		{
			cef39567e827b1e2e96dc03e9a98c8c6e = value;
		}
	}

	/// <summary>
	///       Gets or sets the subsequent point.
	///       </summary>
	public float Point4
	{
		[CompilerGenerated]
		get
		{
			return ce03bf79825fa8aa2e40810bcb43e63d7;
		}
		[CompilerGenerated]
		set
		{
			ce03bf79825fa8aa2e40810bcb43e63d7 = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Interpolation.CatmullRomSegment1F" /> class</summary>
	public CatmullRomSegment1F()
	{
	}

	/// <summary>
	///       Computes a point on the curve.
	///       </summary>
	/// <param name="parameter">The curve parameter.</param>
	/// <returns>The curve point.</returns>
	public float GetPoint(float parameter)
	{
		float num = parameter * parameter;
		float num2 = num * parameter;
		return ((0f - num2 + 2f * num - parameter) * Point1 + (3f * num2 - 5f * num + 2f) * Point2 + (-3f * num2 + 4f * num + parameter) * Point3 + (num2 - num) * Point4) / 2f;
	}

	/// <summary>
	///       Computes the tangent for a point on the curve.
	///       </summary>
	/// <param name="parameter">The curve parameter.</param>
	/// <returns>The curve tangent.</returns>
	public float GetTangent(float parameter)
	{
		float num = parameter * parameter;
		return ((-3f * num + 4f * parameter - 1f) * Point1 + (9f * num - 10f * parameter) * Point2 + (-9f * num + 8f * parameter + 1f) * Point3 + (3f * num - 2f * parameter) * Point4) / 2f;
	}

	/// <summary>
	///       Computes the approximated length of the curve for the parameter interval
	///       [<paramref name="start" />, <paramref name="end" />].
	///       </summary>
	/// <param name="start">The parameter value of the start position.</param>
	/// <param name="end">The parameter value of the end position.</param>
	/// <param name="maxNumberOfIterations">
	///       The maximum number of iterations which are taken to compute the length.
	///       </param>
	/// <param name="tolerance">
	///       The tolerance value. This method will return an approximation of the precise length.
	///       The absolute error will be less than this tolerance.
	///       </param>
	/// <returns>
	///       The approximated length of the curve in the given interval.
	///       </returns>
	public float GetLength(float start, float end, int maxNumberOfIterations, float tolerance)
	{
		return Math.Abs(GetPoint(end) - GetPoint(start));
	}
}
