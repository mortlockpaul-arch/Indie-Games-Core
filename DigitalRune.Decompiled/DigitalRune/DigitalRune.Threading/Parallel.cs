using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using A;

namespace DigitalRune.Threading;

/// <summary>
///       Provides support for parallel execution of tasks.
///       </summary>
public static class Parallel
{
	private static readonly ResourcePool<List<Task>> ce378f72feffbd62a9361ca38fa472c10;

	private static readonly List<c3d37694395193af2b89c32818efb5679> ccabcb1d76bd861f8c6017b19364f8348;

	private static int[] c4f509d5c55f0a9e70e36aaef29604073;

	private static ITaskScheduler c5ff39d39633371910a19c587706712e8;

	[CompilerGenerated]
	private static Func<int, bool> c19ae5197e8f1d77c5b9a5a5a3cba33d3;

	[CompilerGenerated]
	private static Func<int, bool> c70e908f904d35f6bba1c2f6562a9abdb;

	[CompilerGenerated]
	private static Func<int, bool> c422f799ed4ec89e25dd968008fe98786;

	[CompilerGenerated]
	private static Func<List<Task>> c73f073436b4c66eae523762159132e8d;

	[CompilerGenerated]
	private static Action<List<Task>> c52f93b6e16dc37ce8233ad11ec6b3be7;

	/// <summary>
	///       Gets or sets the processor affinity of the worker threads.
	///       </summary>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="value" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///       The specified array is empty or contains invalid values.
	///       </exception>
	public static int[] ProcessorAffinity
	{
		get
		{
			return c4f509d5c55f0a9e70e36aaef29604073;
		}
		set
		{
			if (value == null)
			{
				while (true)
				{
					switch (1)
					{
					case 0:
						break;
					default:
						if (1 == 0)
						{
							__ldtoken(Parallel.set_ProcessorAffinity(int[]));
						}
						throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(588));
					}
				}
			}
			if (value.Length < 1)
			{
				while (true)
				{
					switch (4)
					{
					case 0:
						break;
					default:
						throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3359), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(588));
					}
				}
			}
			if (value.Any((int c7dad665870c39748dfa1df28b07cc92d) => c7dad665870c39748dfa1df28b07cc92d < 0))
			{
				while (true)
				{
					switch (1)
					{
					case 0:
						break;
					default:
						throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3486), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(588));
					}
				}
			}
			if (c70e908f904d35f6bba1c2f6562a9abdb == null)
			{
				while (true)
				{
					switch (7)
					{
					case 0:
						continue;
					}
					break;
				}
				c70e908f904d35f6bba1c2f6562a9abdb = (int c7dad665870c39748dfa1df28b07cc92d) => c7dad665870c39748dfa1df28b07cc92d == 0 || c7dad665870c39748dfa1df28b07cc92d == 2;
			}
			if (value.Any(c70e908f904d35f6bba1c2f6562a9abdb))
			{
				throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3575), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(588));
			}
			if (value.Any((int c7dad665870c39748dfa1df28b07cc92d) => c7dad665870c39748dfa1df28b07cc92d > 5))
			{
				while (true)
				{
					switch (6)
					{
					case 0:
						break;
					default:
						throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3731), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(588));
					}
				}
			}
			c4f509d5c55f0a9e70e36aaef29604073 = value;
		}
	}

	/// <summary>
	///       Gets or sets the task scheduler.
	///       </summary>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="value" /> is <see langword="null" />.
	///       </exception>
	public static ITaskScheduler Scheduler
	{
		get
		{
			if (c5ff39d39633371910a19c587706712e8 == null)
			{
				ITaskScheduler value = new WorkStealingScheduler();
				Interlocked.CompareExchange(ref c5ff39d39633371910a19c587706712e8, value, null);
			}
			return c5ff39d39633371910a19c587706712e8;
		}
		set
		{
			if (value == null)
			{
				throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(588));
			}
			Interlocked.Exchange(ref c5ff39d39633371910a19c587706712e8, value);
		}
	}

	static Parallel()
	{
		if (c73f073436b4c66eae523762159132e8d == null)
		{
			while (true)
			{
				switch (7)
				{
				case 0:
					continue;
				}
				break;
			}
			if (1 == 0)
			{
				__ldtoken(Parallel._002Ecctor());
			}
			c73f073436b4c66eae523762159132e8d = () => new List<Task>();
		}
		Func<List<Task>> create = c73f073436b4c66eae523762159132e8d;
		if (c52f93b6e16dc37ce8233ad11ec6b3be7 == null)
		{
			while (true)
			{
				switch (4)
				{
				case 0:
					continue;
				}
				break;
			}
			c52f93b6e16dc37ce8233ad11ec6b3be7 = (List<Task> c9cc70b23e19e1a842fac3e03e9b3105f) =>
			{
				c9cc70b23e19e1a842fac3e03e9b3105f.Clear();
			};
		}
		ce378f72feffbd62a9361ca38fa472c10 = new ResourcePool<List<Task>>(create, null, c52f93b6e16dc37ce8233ad11ec6b3be7);
		ccabcb1d76bd861f8c6017b19364f8348 = new List<c3d37694395193af2b89c32818efb5679>();
		c4f509d5c55f0a9e70e36aaef29604073 = new int[4] { 3, 4, 5, 1 };
	}

	/// <summary>
	///       Executes all task callbacks on a single thread.
	///       </summary>
	public static void RunCallbacks()
	{
		lock (c3d37694395193af2b89c32818efb5679.ce756549f02d68f19cc5220f66c9420e6)
		{
			ccabcb1d76bd861f8c6017b19364f8348.AddRange(c3d37694395193af2b89c32818efb5679.ce756549f02d68f19cc5220f66c9420e6);
			c3d37694395193af2b89c32818efb5679.ce756549f02d68f19cc5220f66c9420e6.Clear();
		}
		int count = ccabcb1d76bd861f8c6017b19364f8348.Count;
		for (int i = 0; i < count; i++)
		{
			c3d37694395193af2b89c32818efb5679 c3d37694395193af2b89c32818efb5680 = ccabcb1d76bd861f8c6017b19364f8348[i];
			c3d37694395193af2b89c32818efb5680.Callback();
			c3d37694395193af2b89c32818efb5680.Callback = null;
			c3d37694395193af2b89c32818efb5680.ca18b9b5faa9851d6355f617787bb5859();
		}
		while (true)
		{
			switch (4)
			{
			case 0:
				continue;
			}
			if (1 == 0)
			{
				__ldtoken(Parallel.RunCallbacks());
			}
			ccabcb1d76bd861f8c6017b19364f8348.Clear();
			return;
		}
	}

	/// <summary>
	///       Starts a task in a secondary worker thread. Intended for long running, blocking work such as
	///       I/O.
	///       </summary>
	/// <param name="work">The work to execute.</param>
	/// <returns>A task which represents the asynchronous operation.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="work" /> is <see langword="null" />.
	///       </exception>
	public static Task StartBackground(IWork work)
	{
		return StartBackground(work, null);
	}

	/// <summary>
	///       Starts a task in a secondary worker thread. Intended for long running, blocking work such as
	///       I/O.
	///       </summary>
	/// <param name="work">The work to execute.</param>
	/// <param name="completionCallback">
	///       A method which will be called in <see cref="M:DigitalRune.Threading.Parallel.RunCallbacks" /> once this task has completed.
	///       </param>
	/// <returns>A task which represents the asynchronous operation.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="work" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///       Invalid number of maximum threads set in <see cref="P:DigitalRune.Threading.IWork.Options" />.
	///       </exception>
	public static Task StartBackground(IWork work, Action completionCallback)
	{
		if (work == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3844));
		}
		if (work.Options.MaximumThreads < 1)
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3853));
		}
		c3d37694395193af2b89c32818efb5679 c3d37694395193af2b89c32818efb5680 = c3d37694395193af2b89c32818efb5679.c2d8ce8b1ccf2b905e3b18f8a5145fe8a();
		c3d37694395193af2b89c32818efb5680.Callback = completionCallback;
		Task task = c3d37694395193af2b89c32818efb5680.cf953e1a8d2f36ad649d3d153db383d06(work);
		cff580634e72e633642f7196e22f3eb60.c60f2d76c17ab5bddce54f7edea984a95(task);
		return task;
	}

	/// <summary>
	///       Starts a task in a secondary worker thread. Intended for long running, blocking work such as
	///       I/O.
	///       </summary>
	/// <param name="action">The work to execute.</param>
	/// <returns>A task which represents the asynchronous operation.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="action" /> is <see langword="null" />.
	///       </exception>
	public static Task StartBackground(Action action)
	{
		return StartBackground(action, null);
	}

	/// <summary>
	///       Starts a task in a secondary worker thread. Intended for long running, blocking, work
	///       such as I/O.
	///       </summary>
	/// <param name="action">The work to execute.</param>
	/// <param name="completionCallback">
	///       A method which will be called in <see cref="M:DigitalRune.Threading.Parallel.RunCallbacks" /> once this task has completed.
	///       </param>
	/// <returns>A task which represents the asynchronous operation.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="action" /> is <see langword="null" />.
	///       </exception>
	public static Task StartBackground(Action action, Action completionCallback)
	{
		if (action == null)
		{
			while (true)
			{
				switch (2)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(Parallel.StartBackground(Action, Action));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3958));
				}
			}
		}
		c7ab44e81bcf75a605d481f923b99b670 c7ab44e81bcf75a605d481f923b99b671 = c7ab44e81bcf75a605d481f923b99b670.c2d8ce8b1ccf2b905e3b18f8a5145fe8a();
		c7ab44e81bcf75a605d481f923b99b671.Action = action;
		c7ab44e81bcf75a605d481f923b99b671.Options = WorkOptions.Default;
		return StartBackground(c7ab44e81bcf75a605d481f923b99b671, completionCallback);
	}

	/// <summary>
	///       Creates and starts a task to execute the given work.
	///       </summary>
	/// <param name="work">The work to execute in parallel.</param>
	/// <returns>A task which represents the asynchronous operation.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="work" /> is <see langword="null" />.
	///       </exception>
	public static Task Start(IWork work)
	{
		return Start(work, null);
	}

	/// <summary>
	///       Creates and starts a task to execute the given work.
	///       </summary>
	/// <param name="work">The work to execute in parallel.</param>
	/// <param name="completionCallback">
	///       A method which will be called in <see cref="M:DigitalRune.Threading.Parallel.RunCallbacks" /> once this task has completed.
	///       </param>
	/// <returns>A task which represents the asynchronous operation.</returns>
	/// <exception cref="T:System.ArgumentException">
	///       Invalid number of maximum threads set in <see cref="P:DigitalRune.Threading.IWork.Options" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="work" /> is <see langword="null" />.
	///       </exception>
	public static Task Start(IWork work, Action completionCallback)
	{
		if (work == null)
		{
			while (true)
			{
				switch (4)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(Parallel.Start(IWork, Action));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3844));
				}
			}
		}
		if (work.Options.MaximumThreads < 1)
		{
			while (true)
			{
				switch (2)
				{
				case 0:
					break;
				default:
					throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3853));
				}
			}
		}
		c3d37694395193af2b89c32818efb5679 c3d37694395193af2b89c32818efb5680 = c3d37694395193af2b89c32818efb5679.c2d8ce8b1ccf2b905e3b18f8a5145fe8a();
		c3d37694395193af2b89c32818efb5680.Callback = completionCallback;
		Task task = c3d37694395193af2b89c32818efb5680.cf953e1a8d2f36ad649d3d153db383d06(work);
		Scheduler.Schedule(task);
		return task;
	}

	/// <summary>
	///       Creates and starts a task to execute the given work.
	///       </summary>
	/// <param name="action">The work to execute in parallel.</param>
	/// <returns>A task which represents the asynchronous operation.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="action" /> is <see langword="null" />.
	///       </exception>
	public static Task Start(Action action)
	{
		return Start(action, null);
	}

	/// <summary>
	///       Creates and starts a task to execute the given work.
	///       </summary>
	/// <param name="action">The work to execute in parallel.</param>
	/// <param name="completionCallback">
	///       A method which will be called in <see cref="M:DigitalRune.Threading.Parallel.RunCallbacks" /> once this task has completed.
	///       </param>
	/// <returns>A task which represents the asynchronous operation.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="action" /> is <see langword="null" />.
	///       </exception>
	public static Task Start(Action action, Action completionCallback)
	{
		return Start(action, WorkOptions.Default, completionCallback);
	}

	/// <summary>
	///       Creates and starts a task to execute the given work.
	///       </summary>
	/// <param name="action">The work to execute in parallel.</param>
	/// <param name="options">The work options to use with this action.</param>
	/// <returns>A task which represents the asynchronous operation.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="action" /> is <see langword="null" />.
	///       </exception>
	public static Task Start(Action action, WorkOptions options)
	{
		return Start(action, options, null);
	}

	/// <summary>
	///       Creates and starts a task to execute the given work.
	///       </summary>
	/// <param name="action">The work to execute in parallel.</param>
	/// <param name="options">The work options to use with this action.</param>
	/// <param name="completionCallback">
	///       A method which will be called in <see cref="M:DigitalRune.Threading.Parallel.RunCallbacks" /> once this task has completed.
	///       </param>
	/// <returns>A task which represents the asynchronous operation.</returns>
	/// <exception cref="T:System.ArgumentException">
	///       Invalid number of maximum threads set in <see cref="P:DigitalRune.Threading.IWork.Options" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="action" /> is <see langword="null" />.
	///       </exception>
	public static Task Start(Action action, WorkOptions options, Action completionCallback)
	{
		if (options.MaximumThreads < 1)
		{
			while (true)
			{
				switch (4)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(Parallel.Start(Action, WorkOptions, Action));
					}
					throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3971), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4062));
				}
			}
		}
		c7ab44e81bcf75a605d481f923b99b670 c7ab44e81bcf75a605d481f923b99b671 = c7ab44e81bcf75a605d481f923b99b670.c2d8ce8b1ccf2b905e3b18f8a5145fe8a();
		c7ab44e81bcf75a605d481f923b99b671.Action = action;
		c7ab44e81bcf75a605d481f923b99b671.Options = options;
		return Start(c7ab44e81bcf75a605d481f923b99b671, completionCallback);
	}

	/// <summary>
	///       Creates an starts a task which executes the given function and stores the result for later 
	///       retrieval.
	///       </summary>
	/// <param name="function">The function to execute in parallel.</param>
	/// <typeparam name="T">The type of result.</typeparam>
	/// <returns>A <see cref="T:DigitalRune.Threading.Task`1" /> which stores the result of the function.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="function" /> is <see langword="null" />.
	///       </exception>
	public static Task<T> Start<T>(Func<T> function)
	{
		return Start(function, null);
	}

	/// <summary>
	///       Creates an starts a task which executes the given function and stores the result for later 
	///       retrieval.
	///       </summary>
	/// <param name="function">The function to execute in parallel.</param>
	/// <param name="completionCallback">
	///       A method which will be called in <see cref="M:DigitalRune.Threading.Parallel.RunCallbacks" /> once this task has completed.
	///       </param>
	/// <typeparam name="T">The type of result the function returns.</typeparam>
	/// <returns>A <see cref="T:DigitalRune.Threading.Task`1" /> which stores the result of the function.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="function" /> is <see langword="null" />.
	///       </exception>
	public static Task<T> Start<T>(Func<T> function, Action completionCallback)
	{
		return Start(function, WorkOptions.Default, completionCallback);
	}

	/// <summary>
	///       Creates an starts a task which executes the given function and stores the result for later 
	///       retrieval.
	///       </summary>
	/// <param name="function">The function to execute in parallel.</param>
	/// <param name="options">The work options to use with this action.</param>
	/// <typeparam name="T">The type of result the function returns.</typeparam>
	/// <returns>A <see cref="T:DigitalRune.Threading.Task`1" /> which stores the result of the function.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="function" /> is <see langword="null" />.
	///       </exception>
	public static Task<T> Start<T>(Func<T> function, WorkOptions options)
	{
		return Start(function, options, null);
	}

	/// <summary>
	///       Creates an starts a task which executes the given function and stores the result for later 
	///       retrieval.
	///       </summary>
	/// <param name="function">The function to execute in parallel.</param>
	/// <param name="options">The work options to use with this action.</param>
	/// <param name="completionCallback">
	///       A method which will be called in <see cref="M:DigitalRune.Threading.Parallel.RunCallbacks" /> once this task has completed.
	///       </param>
	/// <typeparam name="T">The type of result the function returns.</typeparam>
	/// <returns>A <see cref="T:DigitalRune.Threading.Task`1" /> which stores the result of the function.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="function" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///       Invalid number of maximum threads set in <see cref="P:DigitalRune.Threading.IWork.Options" />.
	///       </exception>
	public static Task<T> Start<T>(Func<T> function, WorkOptions options, Action completionCallback)
	{
		if (options.MaximumThreads < 1)
		{
			while (true)
			{
				switch (5)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(Parallel.Start<T>(Func<T>, WorkOptions, Action));
					}
					throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3971), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4062));
				}
			}
		}
		cdb8b25ba004a852efdf9dc56da187a20<T> cdb8b25ba004a852efdf9dc56da187a21 = cdb8b25ba004a852efdf9dc56da187a20<T>.c2d8ce8b1ccf2b905e3b18f8a5145fe8a();
		cdb8b25ba004a852efdf9dc56da187a21.Function = function;
		cdb8b25ba004a852efdf9dc56da187a21.Options = options;
		Task c65546a8d53dfc41c7ac1e13c9746114b = Start(cdb8b25ba004a852efdf9dc56da187a21, completionCallback);
		return new Task<T>(c65546a8d53dfc41c7ac1e13c9746114b, cdb8b25ba004a852efdf9dc56da187a21);
	}

	/// <summary>
	///       Executes the given work items potentially in parallel with each other.
	///       This method will block until all work is completed.
	///       </summary>
	/// <param name="a">Work to execute.</param>
	/// <param name="b">Work to execute.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///       Either <paramref name="a" /> or <paramref name="b" /> is <see langword="null" />.
	///       </exception>
	public static void Do(IWork a, IWork b)
	{
		Task task = Start(b);
		a.DoWork();
		task.Wait();
	}

	/// <summary>
	///       Executes the given work items potentially in parallel with each other.
	///       This method will block until all work is completed.
	///       </summary>
	/// <param name="work">The work to execute.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///       One of the parameters is <see langword="null" />.
	///       </exception>
	public static void Do(params IWork[] work)
	{
		List<Task> list = ce378f72feffbd62a9361ca38fa472c10.Obtain();
		int num = work.Length;
		for (int i = 0; i < num; i++)
		{
			list.Add(Start(work[i]));
		}
		while (true)
		{
			switch (2)
			{
			case 0:
				continue;
			}
			if (1 == 0)
			{
				__ldtoken(Parallel.Do(IWork[]));
			}
			for (int j = 0; j < num; j++)
			{
				list[j].Wait();
			}
			ce378f72feffbd62a9361ca38fa472c10.Recycle(list);
			return;
		}
	}

	/// <summary>
	///       Executes the given work items potentially in parallel with each other.
	///       This method will block until all work is completed.
	///       </summary>
	/// <param name="action1">The first piece of work to execute.</param>
	/// <param name="action2">The second piece of work to execute.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///       Either <paramref name="action1" /> or <paramref name="action2" /> is <see langword="null" />.
	///       </exception>
	public static void Do(Action action1, Action action2)
	{
		c7ab44e81bcf75a605d481f923b99b670 c7ab44e81bcf75a605d481f923b99b671 = c7ab44e81bcf75a605d481f923b99b670.c2d8ce8b1ccf2b905e3b18f8a5145fe8a();
		c7ab44e81bcf75a605d481f923b99b671.Action = action2;
		c7ab44e81bcf75a605d481f923b99b671.Options = WorkOptions.Default;
		Task task = Start(c7ab44e81bcf75a605d481f923b99b671);
		action1();
		task.Wait();
	}

	/// <summary>
	///       Executes the given work items potentially in parallel with each other.
	///       This method will block until all work is completed.
	///       </summary>
	/// <param name="actions">The work to execute.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///       One of the parameters is <see langword="null" />.
	///       </exception>
	public static void Do(params Action[] actions)
	{
		List<Task> list = ce378f72feffbd62a9361ca38fa472c10.Obtain();
		int num = actions.Length;
		for (int i = 0; i < num; i++)
		{
			c7ab44e81bcf75a605d481f923b99b670 c7ab44e81bcf75a605d481f923b99b671 = c7ab44e81bcf75a605d481f923b99b670.c2d8ce8b1ccf2b905e3b18f8a5145fe8a();
			c7ab44e81bcf75a605d481f923b99b671.Action = actions[i];
			c7ab44e81bcf75a605d481f923b99b671.Options = WorkOptions.Default;
			list.Add(Start(c7ab44e81bcf75a605d481f923b99b671));
		}
		while (true)
		{
			switch (6)
			{
			case 0:
				continue;
			}
			if (1 == 0)
			{
				__ldtoken(Parallel.Do(Action[]));
			}
			for (int j = 0; j < num; j++)
			{
				list[j].Wait();
			}
			while (true)
			{
				switch (2)
				{
				case 0:
					continue;
				}
				ce378f72feffbd62a9361ca38fa472c10.Recycle(list);
				return;
			}
		}
	}

	/// <summary>
	///       Executes a for loop where each iteration can potentially occur in parallel with others.
	///       </summary>
	/// <param name="startInclusive">The index (inclusive) at which to start iterating.</param>
	/// <param name="endExclusive">The index (exclusive) at which to end iterating.</param>
	/// <param name="body">
	///       The method to execute at each iteration. The current index is supplied as the parameter.
	///       </param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="body" /> is <see langword="null" />.
	///       </exception>
	public static void For(int startInclusive, int endExclusive, Action<int> body)
	{
		For(startInclusive, endExclusive, body, 1);
	}

	/// <summary>
	///       Executes a for loop where each iteration can potentially occur in parallel with others. 
	///       </summary>
	/// <param name="startInclusive">The index (inclusive) at which to start iterating.</param>
	/// <param name="endExclusive">The index (exclusive) at which to end iterating.</param>
	/// <param name="body">
	///       The method to execute at each iteration. The current index is supplied as the parameter.
	///       </param>
	/// <param name="stride">The number of iterations that each processor takes at a time.</param>
	public static void For(int startInclusive, int endExclusive, Action<int> body, int stride)
	{
		c7aa9c62bb0a67c16dc365891d9fd830d c7aa9c62bb0a67c16dc365891d9fd830d2 = c7aa9c62bb0a67c16dc365891d9fd830d.c2d8ce8b1ccf2b905e3b18f8a5145fe8a();
		c7aa9c62bb0a67c16dc365891d9fd830d2.cb6e3dcfcb9d1bf53e27a1c4bc4921edb(body, startInclusive, endExclusive, stride);
		Start(c7aa9c62bb0a67c16dc365891d9fd830d2).Wait();
		c7aa9c62bb0a67c16dc365891d9fd830d2.ca18b9b5faa9851d6355f617787bb5859();
	}

	/// <summary>
	///       Executes a for-each loop where each iteration can potentially occur in parallel with others.
	///       </summary>
	/// <param name="collection">The enumerable data source.</param>
	/// <param name="action">
	///       The method to execute at each iteration. The item to process is supplied as the parameter.
	///       </param>
	/// <typeparam name="T">The type of item to iterate over.</typeparam>
	/// <exception cref="T:System.ArgumentNullException">
	///       Either <paramref name="collection" /> or <paramref name="action" /> is <see langword="null" />.
	///       </exception>
	public static void ForEach<T>(IEnumerable<T> collection, Action<T> action)
	{
		c5bde345aba1b052fdbae306ae3695af7<T> c5bde345aba1b052fdbae306ae3695af8 = c5bde345aba1b052fdbae306ae3695af7<T>.c2d8ce8b1ccf2b905e3b18f8a5145fe8a();
		c5bde345aba1b052fdbae306ae3695af8.cb6e3dcfcb9d1bf53e27a1c4bc4921edb(action, collection.GetEnumerator());
		Start(c5bde345aba1b052fdbae306ae3695af8).Wait();
		c5bde345aba1b052fdbae306ae3695af8.ca18b9b5faa9851d6355f617787bb5859();
	}

	[CompilerGenerated]
	private static bool cd4c03acd195179a5b0e65b13645371e8(int c7dad665870c39748dfa1df28b07cc92d)
	{
		return c7dad665870c39748dfa1df28b07cc92d < 0;
	}

	[CompilerGenerated]
	private static bool cbcbc74e41c65f6f6aca2963cf59bb845(int c7dad665870c39748dfa1df28b07cc92d)
	{
		if (c7dad665870c39748dfa1df28b07cc92d != 0)
		{
			return c7dad665870c39748dfa1df28b07cc92d == 2;
		}
		return true;
	}

	[CompilerGenerated]
	private static bool cdf91ab56b47fe0d9d3909060efc6c70d(int c7dad665870c39748dfa1df28b07cc92d)
	{
		return c7dad665870c39748dfa1df28b07cc92d > 5;
	}

	[CompilerGenerated]
	private static List<Task> c8a4e56c8333d2ebdef66dc66117c4d95()
	{
		return new List<Task>();
	}

	[CompilerGenerated]
	private static void cfae703c96caae33f63a5e69930b6e549(List<Task> c9cc70b23e19e1a842fac3e03e9b3105f)
	{
		c9cc70b23e19e1a842fac3e03e9b3105f.Clear();
	}
}
