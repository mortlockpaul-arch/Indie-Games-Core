using System;
using A;

namespace DigitalRune;

/// <summary>
///       Exception that is thrown when a service is not found in the <see cref="T:System.IServiceProvider" />.
///       </summary>
[Serializable]
public class ServiceNotFoundException : Exception
{
	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.ServiceNotFoundException" /> class.
	///       </summary>
	public ServiceNotFoundException()
	{
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.ServiceNotFoundException" /> class.
	///       </summary>
	/// <param name="serviceType">Type of the service.</param>
	public ServiceNotFoundException(Type serviceType)
		: base(string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7271), serviceType.Name))
	{
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.ServiceNotFoundException" /> class.
	///       </summary>
	/// <param name="message">The message.</param>
	public ServiceNotFoundException(string message)
		: base(message)
	{
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.ServiceNotFoundException" /> class.
	///       </summary>
	/// <param name="message">The message.</param>
	/// <param name="inner">The inner exception.</param>
	public ServiceNotFoundException(string message, Exception inner)
		: base(message, inner)
	{
	}
}
