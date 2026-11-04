using System;
using System.Runtime.CompilerServices;

namespace DigitalRune.Mathematics.Interpolation;

/// <summary>
///       Defines a single segment of a 1-dimensional cubic Bézier spline (single-precision).
///       </summary>
public class BezierSegment1F : ICurve<float, float>
{
	[CompilerGenerated]
	private float c99977f6a59a72d189b9fc0d458aca839;

	[CompilerGenerated]
	private float c0b42aca19031beb107b8358dd021f2c6;

	[CompilerGenerated]
	private float cdf8e117d50bd618cd9226a8d360e28f1;

	[CompilerGenerated]
	private float cb548d667c99edc6b3cb6ca2bf0a13438;

	/// <summary>
	///       Gets or sets the start point.
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
	///       Gets or sets the end point.
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
	///       Gets or sets the first control point.
	///       </summary>
	public float ControlPoint1
	{
		[CompilerGenerated]
		get
		{
			return cdf8e117d50bd618cd9226a8d360e28f1;
		}
		[CompilerGenerated]
		set
		{
			cdf8e117d50bd618cd9226a8d360e28f1 = value;
		}
	}

	/// <summary>
	///       Gets or sets the second control point.
	///       </summary>
	public float ControlPoint2
	{
		[CompilerGenerated]
		get
		{
			return cb548d667c99edc6b3cb6ca2bf0a13438;
		}
		[CompilerGenerated]
		set
		{
			cb548d667c99edc6b3cb6ca2bf0a13438 = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Interpolation.BezierSegment1F" /> class</summary>
	public BezierSegment1F()
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
		float num3 = 1f - parameter;
		float num4 = num3 * num3;
		float num5 = num4 * num3;
		return num5 * Point1 + 3f * parameter * num4 * ControlPoint1 + 3f * num * num3 * ControlPoint2 + num2 * Point2;
	}

	/// <summary>
	///       Computes the tangent for a point on the curve.
	///       </summary>
	/// <param name="parameter">The curve parameter.</param>
	/// <returns>The curve tangent.</returns>
	public float GetTangent(float parameter)
	{
		float num = parameter * parameter;
		return (-3f + 6f * parameter - 3f * num) * Point1 + (3f - 12f * parameter + 9f * num) * ControlPoint1 + (6f * parameter - 9f * num) * ControlPoint2 + 3f * num * Point2;
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
	///       The approximated length of the curve in the given interval. For some curves the length is 
	///       computed with an iterative algorithm. The iterations end when the 
	///       <paramref name="maxNumberOfIterations" /> were performed, or when the 
	///       <paramref name="tolerance" /> criterion is met - whichever comes first.
	///       </returns>
	public float GetLength(float start, float end, int maxNumberOfIterations, float tolerance)
	{
		return Math.Abs(GetPoint(end) - GetPoint(start));
	}
}
