using System;
using System.Runtime.CompilerServices;
using DigitalRune.Mathematics.Algebra;
using DigitalRune.Mathematics.Analysis;

namespace DigitalRune.Mathematics.Interpolation;

/// <summary>
///       Defines a single segment of a 3-dimensional cubic B-spline (single-precision).
///       </summary>
public class BSplineSegment3F : ICurve<float, Vector3F>
{
	[CompilerGenerated]
	private Vector3F c99977f6a59a72d189b9fc0d458aca839;

	[CompilerGenerated]
	private Vector3F c0b42aca19031beb107b8358dd021f2c6;

	[CompilerGenerated]
	private Vector3F cef39567e827b1e2e96dc03e9a98c8c6e;

	[CompilerGenerated]
	private Vector3F ce03bf79825fa8aa2e40810bcb43e63d7;

	/// <summary>
	///       Gets or sets the previous point.
	///       </summary>
	public Vector3F Point1
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
	public Vector3F Point2
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
	public Vector3F Point3
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
	public Vector3F Point4
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

	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Interpolation.BSplineSegment3F" /> class</summary>
	public BSplineSegment3F()
	{
	}

	/// <summary>
	///       Computes a point on the curve.
	///       </summary>
	/// <param name="parameter">The curve parameter.</param>
	/// <returns>The curve point.</returns>
	public Vector3F GetPoint(float parameter)
	{
		float num = parameter * parameter;
		float num2 = num * parameter;
		return ((0f - num2 + 3f * num - 3f * parameter + 1f) * Point1 + (3f * num2 - 6f * num + 4f) * Point2 + (-3f * num2 + 3f * num + 3f * parameter + 1f) * Point3 + num2 * Point4) / 6f;
	}

	/// <summary>
	///       Computes the tangent for a point on the curve.
	///       </summary>
	/// <param name="parameter">The curve parameter.</param>
	/// <returns>The curve tangent.</returns>
	public Vector3F GetTangent(float parameter)
	{
		float num = parameter * parameter;
		return ((-3f * num + 6f * parameter - 3f) * Point1 + (9f * num - 12f * parameter) * Point2 + (-9f * num + 6f * parameter + 3f) * Point3 + 3f * num * Point4) / 6f;
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
		RombergIntegratorF rombergIntegratorF = new RombergIntegratorF();
		rombergIntegratorF.Epsilon = tolerance;
		rombergIntegratorF.MaxNumberOfIterations = maxNumberOfIterations;
		rombergIntegratorF.MinNumberOfIterations = 2;
		RombergIntegratorF rombergIntegratorF2 = rombergIntegratorF;
		return Math.Abs(rombergIntegratorF2.Integrate(ce56b3acb59a9ed8922b13ec1b5eef043, start, end));
	}

	private float ce56b3acb59a9ed8922b13ec1b5eef043(float c5078b1ca2bd86333c65cb625b996b618)
	{
		return GetTangent(c5078b1ca2bd86333c65cb625b996b618).Length;
	}
}
