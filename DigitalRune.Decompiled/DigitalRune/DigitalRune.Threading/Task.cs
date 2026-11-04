using System;
using System.Runtime.CompilerServices;
using A;

namespace DigitalRune.Threading;

/// <summary>
///       Represents an asynchronous operation that can return a value.
///       </summary>
/// <typeparam name="T">The type of result produced by the asynchronous operation.</typeparam>
public struct Task<T>
{
	private Task c12e24e4b2235005e480c6c09bdf729c2;

	private cdb8b25ba004a852efdf9dc56da187a20<T> c4b759cf0f2d3c6a630def65811944ebc;

	private readonly int cffc844a590a55f61a9ca9f513339533b;

	/// <summary>
	///       Gets a value which indicates if this task has completed.
	///       </summary>
	public bool IsComplete => c12e24e4b2235005e480c6c09bdf729c2.IsComplete;

	/// <summary>
	///       Gets an array containing any exceptions thrown by this task.
	///       </summary>
	public Exception[] Exceptions => c12e24e4b2235005e480c6c09bdf729c2.Exceptions;

	internal Task(Task c65546a8d53dfc41c7ac1e13c9746114b, cdb8b25ba004a852efdf9dc56da187a20<T> c48534c252a39ff483baefaab1792a5cf)
	{
		c12e24e4b2235005e480c6c09bdf729c2 = c65546a8d53dfc41c7ac1e13c9746114b;
		c4b759cf0f2d3c6a630def65811944ebc = c48534c252a39ff483baefaab1792a5cf;
		cffc844a590a55f61a9ca9f513339533b = c48534c252a39ff483baefaab1792a5cf.ID;
	}

	/// <summary>
	///       Gets the result. (Blocks the calling thread until the asynchronous operation has completed 
	///       execution. This can only be called once!)
	///       </summary>
	/// <returns>The result of the asynchronous operation.</returns>
	/// <exception cref="T:DigitalRune.Threading.TaskException">
	///       The task or a child task has thrown an exception.
	///       </exception>
	/// <exception cref="T:System.InvalidOperationException">
	///       The result of the <see cref="T:DigitalRune.Threading.Task`1" /> has already been retrieved. The method 
	///       <see cref="M:DigitalRune.Threading.Task`1.GetResult" /> can only be called once.
	///       </exception>
	public T GetResult()
	{
		if (c4b759cf0f2d3c6a630def65811944ebc == null || c4b759cf0f2d3c6a630def65811944ebc.ID != cffc844a590a55f61a9ca9f513339533b)
		{
			throw new InvalidOperationException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3256));
		}
		c12e24e4b2235005e480c6c09bdf729c2.Wait();
		T result = c4b759cf0f2d3c6a630def65811944ebc.Result;
		c4b759cf0f2d3c6a630def65811944ebc.ca18b9b5faa9851d6355f617787bb5859();
		c4b759cf0f2d3c6a630def65811944ebc = null;
		return result;
	}
}
/// <summary>
///       Represents an asynchronous operation.
///       </summary>
public struct Task
{
	private readonly bool cd26ad71a5b28e38742793e526bd6c17f;

	[CompilerGenerated]
	private c3d37694395193af2b89c32818efb5679 c902ff321a0f3b86bc2e1f440fc4250df;

	[CompilerGenerated]
	private int c81e01eb94ded2de1d28abbfff38dcf9e;

	internal c3d37694395193af2b89c32818efb5679 Item
	{
		[CompilerGenerated]
		get
		{
			return c902ff321a0f3b86bc2e1f440fc4250df;
		}
		[CompilerGenerated]
		private set
		{
			c902ff321a0f3b86bc2e1f440fc4250df = value;
		}
	}

	internal int ID
	{
		[CompilerGenerated]
		get
		{
			return c81e01eb94ded2de1d28abbfff38dcf9e;
		}
		[CompilerGenerated]
		private set
		{
			c81e01eb94ded2de1d28abbfff38dcf9e = value;
		}
	}

	/// <summary>
	///       Gets a value indicating whether the task has completed.
	///       </summary>
	public bool IsComplete
	{
		get
		{
			if (cd26ad71a5b28e38742793e526bd6c17f)
			{
				return Item.cc9f1f02819bbde2fce810c68e3ce2c95 != ID;
			}
			return true;
		}
	}

	/// <summary>
	///       Gets an array containing any exceptions thrown by this task.
	///       </summary>
	public Exception[] Exceptions
	{
		get
		{
			if (cd26ad71a5b28e38742793e526bd6c17f && IsComplete)
			{
				Item.cf5b9d9552c08f2515ce5863da923e6a9.cfa34cf3f48196497030d8ee48d8e4741(ID, out var c64f632f47ec42f89161439dfec6e213c);
				return c64f632f47ec42f89161439dfec6e213c;
			}
			return null;
		}
	}

	internal Task(c3d37694395193af2b89c32818efb5679 cf51048ba330b8d3f481ea5bc25a03eb9)
	{
		this = default;
		ID = cf51048ba330b8d3f481ea5bc25a03eb9.cc9f1f02819bbde2fce810c68e3ce2c95;
		Item = cf51048ba330b8d3f481ea5bc25a03eb9;
		cd26ad71a5b28e38742793e526bd6c17f = true;
	}

	/// <summary>
	///       Waits for the task to complete execution.
	///       </summary>
	/// <exception cref="T:DigitalRune.Threading.TaskException">
	///       The task or a child task has thrown an exception.
	///       </exception>
	/// <exception cref="T:System.InvalidOperationException">
	///       The method is called from within the same task. A task cannot wait on itself.
	///       </exception>
	public void Wait()
	{
		if (cd26ad71a5b28e38742793e526bd6c17f)
		{
			Task? ca0c69eef9d50097b82adb19ee5a85f = c3d37694395193af2b89c32818efb5679.ca0c69eef9d50097b82adb19ee5a85f93;
			if (ca0c69eef9d50097b82adb19ee5a85f.HasValue && ca0c69eef9d50097b82adb19ee5a85f.Value.Item == Item && ca0c69eef9d50097b82adb19ee5a85f.Value.ID == ID)
			{
				throw new InvalidOperationException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6559));
			}
			Item.c334ab49796744460921f9725866ddf75(ID);
		}
	}

	internal void cbca98f742f4d19c763f8dabed3e87233()
	{
		if (cd26ad71a5b28e38742793e526bd6c17f)
		{
			Item.cbca98f742f4d19c763f8dabed3e87233(ID);
		}
	}
}
