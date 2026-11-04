using System.Collections.Generic;
using System.Runtime.CompilerServices;
using A;

namespace DigitalRune.Threading;

/// <summary>
///       A task scheduler that supports "work stealing" to balance tasks across multiple worker 
///       threads.
///       </summary>
public class WorkStealingScheduler : ITaskScheduler
{
	private readonly Queue<Task> c4dfcbc23665dbf014b820c370f9e6893;

	[CompilerGenerated]
	private List<cb1d8c1f6621ea94d9e1756e141e27094> c27ad1e4266c5d0a1db4fb88a9269dadb;

	internal List<cb1d8c1f6621ea94d9e1756e141e27094> Workers
	{
		[CompilerGenerated]
		get
		{
			return c27ad1e4266c5d0a1db4fb88a9269dadb;
		}
		[CompilerGenerated]
		private set
		{
			c27ad1e4266c5d0a1db4fb88a9269dadb = value;
		}
	}

	/// <summary>
	///       Creates a new instance of the <see cref="T:DigitalRune.Threading.WorkStealingScheduler" /> class.
	///       </summary>
	public WorkStealingScheduler()
		: this(3)
	{
	}

	/// <summary>
	///       Creates a new instance of the <see cref="T:DigitalRune.Threading.WorkStealingScheduler" /> class.
	///       </summary>
	/// <param name="numThreads">The number of threads to create.</param>
	public WorkStealingScheduler(int numThreads)
	{
		c4dfcbc23665dbf014b820c370f9e6893 = new Queue<Task>();
		Workers = new List<cb1d8c1f6621ea94d9e1756e141e27094>(numThreads);
		for (int i = 0; i < numThreads; i++)
		{
			Workers.Add(new cb1d8c1f6621ea94d9e1756e141e27094(this, i));
		}
		for (int j = 0; j < numThreads; j++)
		{
			Workers[j].c1a1ad206553a863175ada6def4da0863();
		}
	}

	internal bool c0dec5417fc0d42950205fe7afacd87d4(ref Task c65546a8d53dfc41c7ac1e13c9746114b)
	{
		if (c4dfcbc23665dbf014b820c370f9e6893.Count == 0)
		{
			return false;
		}
		lock (c4dfcbc23665dbf014b820c370f9e6893)
		{
			if (c4dfcbc23665dbf014b820c370f9e6893.Count > 0)
			{
				c65546a8d53dfc41c7ac1e13c9746114b = c4dfcbc23665dbf014b820c370f9e6893.Dequeue();
				return true;
			}
			return false;
		}
	}

	/// <summary>
	///       Schedules a task for execution.
	///       </summary>
	/// <param name="task">The task to schedule.</param>
	public void Schedule(Task task)
	{
		int maximumThreads = task.Item.cc2353fa93de25abd681a15853d70f006.Options.MaximumThreads;
		cb1d8c1f6621ea94d9e1756e141e27094 c69766249e84c6705ec5d3193204d0e = cb1d8c1f6621ea94d9e1756e141e27094.c69766249e84c6705ec5d3193204d0e92;
		if (!task.Item.cc2353fa93de25abd681a15853d70f006.Options.QueueFIFO && c69766249e84c6705ec5d3193204d0e != null)
		{
			c69766249e84c6705ec5d3193204d0e.c42af5e0c989cbf8f2e017442f350ccbc(task);
		}
		else
		{
			lock (c4dfcbc23665dbf014b820c370f9e6893)
			{
				c4dfcbc23665dbf014b820c370f9e6893.Enqueue(task);
			}
		}
		if (maximumThreads > 1)
		{
			c3d37694395193af2b89c32818efb5679.cb0a359a7d5efad04120d80f3bb03d9f1 = task;
		}
		int count = Workers.Count;
		for (int i = 0; i < count; i++)
		{
			Workers[i].Gate.Set();
		}
	}
}
