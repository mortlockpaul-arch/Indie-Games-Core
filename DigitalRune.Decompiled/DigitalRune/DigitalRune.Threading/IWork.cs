namespace DigitalRune.Threading;

/// <summary>
///       Represents a piece of work which can be executed in parallel.
///       </summary>
public interface IWork
{
	/// <summary>
	///       Gets the options specifying how this work may be executed.
	///       </summary>
	WorkOptions Options { get; }

	/// <summary>
	///       Executes the work.
	///       </summary>
	void DoWork();
}
