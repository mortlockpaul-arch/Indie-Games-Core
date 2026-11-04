using System;
using A;

namespace DigitalRune.Threading;

/// <summary>
///       Occurs when an unhandled exception is thrown within a <see cref="T:DigitalRune.Threading.Task" />.
///       </summary>
[Serializable]
public class TaskException : Exception
{
	/// <summary>
	///       Gets an array containing any unhandled exceptions that were thrown by the task.
	///       </summary>
	public Exception[] InnerExceptions { get; private set; }

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Threading.TaskException" /> class.
	///       </summary>
	/// <param name="inner">The unhandled exceptions thrown by the task.</param>
	public TaskException(Exception[] inner)
		: base(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5395), null)
	{
		InnerExceptions = inner;
	}
}
