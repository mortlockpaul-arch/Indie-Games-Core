namespace DigitalRune.Mathematics.Interpolation;

/// <summary>
///       The type of spline interpolation used to interpolate between two curve keys.
///       </summary>
public enum SplineInterpolation
{
	/// <summary>
	///       Linear interpolation (LERP).
	///       </summary>
	Linear,
	/// <summary>
	///       Step interpolation using "left steps". See <see cref="T:DigitalRune.Mathematics.Interpolation.StepInterpolation" />.
	///       </summary>
	StepLeft,
	/// <summary>
	///       Step interpolation using "centered steps". See <see cref="T:DigitalRune.Mathematics.Interpolation.StepInterpolation" />.
	///       </summary>
	StepCentered,
	/// <summary>
	///       Step interpolation using "right steps". See <see cref="T:DigitalRune.Mathematics.Interpolation.StepInterpolation" />.
	///       </summary>
	StepRight,
	/// <summary>
	///       Interpolation using a cubic Bézier spline.
	///       </summary>
	Bezier,
	/// <summary>
	///       Interpolation using a cubic B-spline.
	///       </summary>
	BSpline,
	/// <summary>
	///       Interpolation using a cubic Hermite spline.
	///       </summary>
	Hermite,
	/// <summary>
	///       Interpolation using a Catmull-Rom spline.
	///       </summary>
	CatmullRom
}
