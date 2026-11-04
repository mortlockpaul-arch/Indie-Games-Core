using System.Runtime.CompilerServices;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Mathematics.Interpolation;

/// <summary>
///       Defines a step interpolation between two 3-dimensional points (single-precision).
///       </summary>
public class StepSegment3F : ICurve<float, Vector3F>
{
	[CompilerGenerated]
	private Vector3F c99977f6a59a72d189b9fc0d458aca839;

	[CompilerGenerated]
	private Vector3F c0b42aca19031beb107b8358dd021f2c6;

	[CompilerGenerated]
	private StepInterpolation cbb2c18333bf90550a17c8178b0400c8f;

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
	///       Gets or sets the type of step interpolation.
	///       </summary>
	public StepInterpolation StepType
	{
		[CompilerGenerated]
		get
		{
			return cbb2c18333bf90550a17c8178b0400c8f;
		}
		[CompilerGenerated]
		set
		{
			cbb2c18333bf90550a17c8178b0400c8f = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Interpolation.StepSegment3F" /> class</summary>
	public StepSegment3F()
	{
	}

	/// <summary>
	///       Computes a point on the curve.
	///       </summary>
	/// <param name="parameter">The curve parameter.</param>
	/// <returns>The curve point.</returns>
	public Vector3F GetPoint(float parameter)
	{
		return InterpolationHelper.Step(Point1, Point2, parameter, StepType);
	}

	/// <summary>
	///       Computes the tangent for a point on the curve.
	///       </summary>
	/// <param name="parameter">The curve parameter.</param>
	/// <returns>The curve tangent.</returns>
	public Vector3F GetTangent(float parameter)
	{
		return Vector3F.Zero;
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
		return 0f;
	}
}
