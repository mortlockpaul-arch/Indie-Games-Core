using System;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Mathematics.Interpolation;

/// <summary>
///       Defines a waypoint of a 2-dimensional path (singe-precision).
///       </summary>
[Serializable]
public class PathKey2F : CurveKey<float, Vector2F>
{
	private float _parameter;

	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Interpolation.PathKey2F" /> class</summary>
	public PathKey2F()
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
