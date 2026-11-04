using System;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Mathematics.Interpolation;

/// <summary>
///       Defines a control point on a <see cref="T:DigitalRune.Mathematics.Interpolation.Curve2F" /> (single-precision).
///       </summary>
[Serializable]
public class CurveKey2F : CurveKey<float, Vector2F>
{
	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Interpolation.CurveKey2F" /> class</summary>
	public CurveKey2F()
	{
	}

	/// <summary>
	///       Gets the parameter.
	///       </summary>
	/// <returns>The parameter.</returns>
	protected override float GetParameter()
	{
		return Point.X;
	}

	/// <summary>
	///       Sets the parameter.
	///       </summary>
	/// <param name="value">The parameter</param>
	protected override void SetParameter(float value)
	{
		Point = new Vector2F(value, Point.Y);
	}
}
