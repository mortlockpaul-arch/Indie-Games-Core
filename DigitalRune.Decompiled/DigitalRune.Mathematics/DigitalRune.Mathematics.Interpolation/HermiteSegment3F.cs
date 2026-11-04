using System;
using System.Runtime.CompilerServices;
using DigitalRune.Mathematics.Algebra;
using DigitalRune.Mathematics.Analysis;

namespace DigitalRune.Mathematics.Interpolation;

/// <summary>
///       Defines a single segment of a 3-dimensional cubic Hermite spline (single-precision).
///       </summary>
public class HermiteSegment3F : ICurve<float, Vector3F>
{
	[CompilerGenerated]
	private Vector3F c99977f6a59a72d189b9fc0d458aca839;

	[CompilerGenerated]
	private Vector3F c0b42aca19031beb107b8358dd021f2c6;

	[CompilerGenerated]
	private Vector3F cf22edfc606aec1095c054b7f49776f83;

	[CompilerGenerated]
	private Vector3F c269ea886a47c07692c7420b34bbea992;

	/// <summary>
	///       Gets or sets the start point.
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
	///       Gets or sets the end point.
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
	///       Gets or sets the tangent at <see cref="P:DigitalRune.Mathematics.Interpolation.HermiteSegment3F.Point1" />.
	///       </summary>
	public Vector3F Tangent1
	{
		[CompilerGenerated]
		get
		{
			return cf22edfc606aec1095c054b7f49776f83;
		}
		[CompilerGenerated]
		set
		{
			cf22edfc606aec1095c054b7f49776f83 = value;
		}
	}

	/// <summary>
	///       Gets or sets the tangent at <see cref="P:DigitalRune.Mathematics.Interpolation.HermiteSegment3F.Point2" />.
	///       </summary>
	public Vector3F Tangent2
	{
		[CompilerGenerated]
		get
		{
			return c269ea886a47c07692c7420b34bbea992;
		}
		[CompilerGenerated]
		set
		{
			c269ea886a47c07692c7420b34bbea992 = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Interpolation.HermiteSegment3F" /> class</summary>
	public HermiteSegment3F()
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
		return (2f * num2 - 3f * num + 1f) * Point1 + (num2 - 2f * num + parameter) * Tangent1 + (-2f * num2 + 3f * num) * Point2 + (num2 - num) * Tangent2;
	}

	/// <summary>
	///       Computes the tangent for a point on the curve.
	///       </summary>
	/// <param name="parameter">The curve parameter.</param>
	/// <returns>The curve tangent.</returns>
	public Vector3F GetTangent(float parameter)
	{
		float num = parameter * parameter;
		return (6f * num - 6f * parameter) * Point1 + (3f * num - 4f * parameter + 1f) * Tangent1 + (-6f * num + 6f * parameter) * Point2 + (3f * num - 2f * parameter) * Tangent2;
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
