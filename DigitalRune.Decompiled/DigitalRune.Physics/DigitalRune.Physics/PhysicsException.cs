using System;

namespace DigitalRune.Physics;

/// <summary>
///       The exception that is thrown when a general error in the physics library occurs.
///       </summary>
[Serializable]
public class PhysicsException : Exception
{
	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Physics.PhysicsException" /> class.
	///       </summary>
	public PhysicsException()
	{
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Physics.PhysicsException" /> class.
	///       </summary>
	/// <param name="message">The exception message.</param>
	public PhysicsException(string message)
		: base(message)
	{
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Physics.PhysicsException" /> class.
	///       </summary>
	/// <param name="message">The message.</param>
	/// <param name="innerException">The inner exception.</param>
	public PhysicsException(string message, Exception innerException)
		: base(message, innerException)
	{
	}
}
