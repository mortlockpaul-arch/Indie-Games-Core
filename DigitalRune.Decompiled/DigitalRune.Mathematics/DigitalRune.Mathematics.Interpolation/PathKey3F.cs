using System;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Mathematics.Interpolation;

/// <summary>
///       Defines a waypoint of a 3-dimensional path (singe-precision).
///       </summary>
[Serializable]
public class PathKey3F : CurveKey<float, Vector3F>
{
	private float _parameter;

	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Interpolation.PathKey3F" /> class</summary>
	public PathKey3F()
	{
	}

	/// <summary>
	///       Gets the parameter.
	///       </summary>
	/// <returns>The parameter.</returns>
	protected override float GetParameter()
	{
		return _parameter;
	}

	/// <summary>
	///       Sets the parameter.
	///       </summary>
	/// <param name="value">The parameter</param>
	protected override void SetParameter(float value)
	{
		_parameter = value;
	}
}
