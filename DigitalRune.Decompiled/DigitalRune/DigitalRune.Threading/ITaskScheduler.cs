namespace DigitalRune.Threading;

/// <summary>
///       Defines a task scheduler.
///       </summary>
public interface ITaskScheduler
{
	/// <summary>
	///       Schedules a task for execution.
	///       </summary>
	/// <param name="task">The task to schedule.</param>
	void Schedule(Task task);
}
