using System;
using System.Runtime.CompilerServices;

namespace DigitalRune.Mathematics.Interpolation;

/// <summary>
///       Defines a single segment of a 1-dimensional cubic Cardinal spline (single-precision).
///       </summary>
public class CardinalSegment1F : ICurve<float, float>
{
	[CompilerGenerated]
	private float c99977f6a59a72d189b9fc0d458aca839;

	[CompilerGenerated]
	private float c0b42aca19031beb107b8358dd021f2c6;

	[CompilerGenerated]
	private float cef39567e827b1e2e96dc03e9a98c8c6e;

	[CompilerGenerated]
	private float ce03bf79825fa8aa2e40810bcb43e63d7;

	[CompilerGenerated]
	private float c5e6190807bfd72e57b22c1ca98b7400f;

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

	/// <summary>
	///       Gets or sets the tension constant.
	///       </summary>
	public float Tension
	{
		[CompilerGenerated]
		get
		{
			return c5e6190807bfd72e57b22c1ca98b7400f;
		}
		[CompilerGenerated]
		set
		{
			c5e6190807bfd72e57b22c1ca98b7400f = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Interpolation.CardinalSegment1F" /> class</summary>
	public CardinalSegment1F()
	{
	}

	/// <summary>
	///       Computes a point on the curve.
	///       </summary>
	/// <param name="parameter">The curve parameter.</param>
	/// <returns>The curve point.</returns>
	public float GetPoint(float parameter)
	{
		float num = 0.5f * (1f - Tension);
		float num2 = parameter * parameter;
		float num3 = num2 * parameter;
		return (2f * num3 - 3f * num2 + 1f) * Point2 + (num3 - 2f * num2 + parameter) * num * (Point3 - Point1) + (-2f * num3 + 3f * num2) * Point3 + (num3 - num2) * num * (Point4 - Point2);
	}

	/// <summary>
	///       Computes the tangent for a point on the curve.
	///       </summary>
	/// <param name="parameter">The curve parameter.</param>
	/// <returns>The curve tangent.</returns>
	public float GetTangent(float parameter)
	{
		float num = 0.5f * (1f - Tension);
		float num2 = parameter * parameter;
		return (6f * num2 - 6f * parameter) * Point2 + (3f * num2 - 4f * parameter + 1f) * num * (Point3 - Point1) + (-6f * num2 + 6f * parameter) * Point3 + (3f * num2 - 2f * parameter) * num * (Point4 - Point2);
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
