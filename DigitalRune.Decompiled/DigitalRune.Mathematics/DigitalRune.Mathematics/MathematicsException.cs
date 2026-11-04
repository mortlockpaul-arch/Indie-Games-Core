using System;

namespace DigitalRune.Mathematics;

/// <summary>
///       The exception that is thrown when a general error in the mathematics library occurs.
///       </summary>
[Serializable]
public class MathematicsException : Exception
{
	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.MathematicsException" /> class.
	///       </summary>
	public MathematicsException()
	{
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.MathematicsException" /> class.
	///       </summary>
	/// <param name="message">The exception message.</param>
	public MathematicsException(string message)
		: base(message)
	{
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.MathematicsException" /> class.
	///       </summary>
	/// <param name="message">The message.</param>
	/// <param name="innerException">The inner exception.</param>
	public MathematicsException(string message, Exception innerException)
		: base(message, innerException)
	{
	}
}
