using System;

namespace DigitalRune.Geometry;

/// <summary>
///       The exception that is thrown when an error in the geometry library occurs.
///       </summary>
[Serializable]
public class GeometryException : Exception
{
	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.GeometryException" /> class.
	///       </summary>
	public GeometryException()
	{
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.GeometryException" /> class.
	///       </summary>
	/// <param name="message">The message.</param>
	public GeometryException(string message)
		: base(message)
	{
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.GeometryException" /> class.
	///       </summary>
	/// <param name="message">The message.</param>
	/// <param name="inner">The inner.</param>
	public GeometryException(string message, Exception inner)
		: base(message, inner)
	{
	}
}
