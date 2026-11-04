using System;

namespace DigitalRune.Mathematics.Interpolation;

/// <summary>
///       Defines a curve key (control point) of a piecewise curve.
///       </summary>
/// <typeparam name="TParam">
///       The type of the curve parameter (usually <see cref="T:System.Single" /> or <see cref="T:System.Double" />).
///       </typeparam>
/// <typeparam name="TPoint">
///       The type of the curve points (such as <see cref="T:DigitalRune.Mathematics.Algebra.Vector2F" />, <see cref="T:DigitalRune.Mathematics.Algebra.Vector3F" />, etc.).
///       </typeparam>
/// <typeparam name="TCurveKey">
///       The type of the curve key. (A type derived from <see cref="T:DigitalRune.Mathematics.Interpolation.CurveKey`2" />.)
///       </typeparam>
[Serializable]
public abstract class CurveKey<TParam, TPoint>
{
	/// <summary>
	///       Gets or sets a value that defines where this curve key is positioned on the curve.
	///       </summary>
	public TParam Parameter
	{
		get
		{
			return GetParameter();
		}
		set
		{
			SetParameter(value);
		}
	}

	/// <summary>
	///       Gets or sets the curve point for this curve key.
	///       </summary>
	public TPoint Point { get; set; }

	/// <summary>
	///       Gets or sets the incoming tangent or the control point before this curve key.
	///       </summary>
	public TPoint TangentIn { get; set; }

	/// <summary>
	///       Gets or sets the outgoing tangent or the control point after this curve key.
	///       </summary>
	public TPoint TangentOut { get; set; }

	/// <summary>
	///       Gets or sets the type of interpolation (the spline type) used for the current curve segment.
	///       between this key and the next key.
	///       </summary>
	public SplineInterpolation Interpolation { get; set; }

	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Interpolation.CurveKey`2" /> class</summary>
	protected CurveKey()
	{
	}

	/// <summary>
	///       Gets the parameter.
	///       </summary>
	/// <returns>The parameter.</returns>
	protected abstract TParam GetParameter();

	/// <summary>
	///       Sets the parameter.
	///       </summary>
	/// <param name="value">The parameter</param>
	protected abstract void SetParameter(TParam value);
}
