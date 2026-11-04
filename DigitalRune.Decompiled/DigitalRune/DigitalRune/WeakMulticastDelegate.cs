using System;
using System.Collections.Generic;
using A;

namespace DigitalRune;

/// <summary>
///       Represents a <see cref="T:System.MulticastDelegate" /> that stores the target objects as weak 
///       references.
///       </summary>
public class WeakMulticastDelegate
{
	private readonly List<c7180016b8fd0464565a072d2ef58eb1d> c4ff860504c1da0757f5bf226fe4a5680 = new List<c7180016b8fd0464565a072d2ef58eb1d>();

	/// <summary>
	///       Gets the number of live delegates in the collection.
	///       </summary>
	public int Count
	{
		get
		{
			c652127f486d3fe0c1cc1e63428d89611();
			return c4ff860504c1da0757f5bf226fe4a5680.Count;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.WeakMulticastDelegate" /> class</summary>
	public WeakMulticastDelegate()
	{
	}

	/// <summary>
	///       Adds a new <see cref="T:System.Delegate" /> to the <see cref="T:DigitalRune.WeakMulticastDelegate" />.
	///       </summary>
	/// <param name="delegate">The new <see cref="T:System.Delegate" />.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="delegate" /> is <see langword="null" />.
	///       </exception>
	public void Add(Delegate @delegate)
	{
		if ((object)@delegate == null)
		{
			while (true)
			{
				switch (7)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(WeakMulticastDelegate.Add(Delegate));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1));
				}
			}
		}
		if (c4ff860504c1da0757f5bf226fe4a5680.Count == c4ff860504c1da0757f5bf226fe4a5680.Capacity)
		{
			c652127f486d3fe0c1cc1e63428d89611();
		}
		c4ff860504c1da0757f5bf226fe4a5680.Add(new c7180016b8fd0464565a072d2ef58eb1d(@delegate));
	}

	/// <summary>
	///       Removes a <see cref="T:System.Delegate" /> from the <see cref="T:DigitalRune.WeakMulticastDelegate" />.
	///       </summary>
	/// <param name="delegate">The <see cref="T:System.Delegate" /> to remove.</param>
	public void Remove(Delegate @delegate)
	{
		for (int num = c4ff860504c1da0757f5bf226fe4a5680.Count - 1; num >= 0; num--)
		{
			c7180016b8fd0464565a072d2ef58eb1d c7180016b8fd0464565a072d2ef58eb1d2 = c4ff860504c1da0757f5bf226fe4a5680[num];
			if (c7180016b8fd0464565a072d2ef58eb1d2.c4bbdd2ec94609ed106e71dd6f5679d67 != null)
			{
				while (true)
				{
					switch (2)
					{
					case 0:
						continue;
					}
					break;
				}
				if (1 == 0)
				{
					__ldtoken(WeakMulticastDelegate.Remove(Delegate));
				}
				object target = c7180016b8fd0464565a072d2ef58eb1d2.c4bbdd2ec94609ed106e71dd6f5679d67.Target;
				if (target == null)
				{
					c4ff860504c1da0757f5bf226fe4a5680.RemoveAt(num);
				}
				else if (target == @delegate.Target)
				{
					while (true)
					{
						switch (2)
						{
						case 0:
							continue;
						}
						break;
					}
					if ((object)c7180016b8fd0464565a072d2ef58eb1d2.c398ca7cf438dac411558bed96056a177 == @delegate.Method)
					{
						c4ff860504c1da0757f5bf226fe4a5680.RemoveAt(num);
						break;
					}
				}
			}
			else if (@delegate.Target == null)
			{
				while (true)
				{
					switch (3)
					{
					case 0:
						continue;
					}
					break;
				}
				if ((object)c7180016b8fd0464565a072d2ef58eb1d2.c398ca7cf438dac411558bed96056a177 == @delegate.Method)
				{
					c4ff860504c1da0757f5bf226fe4a5680.RemoveAt(num);
					break;
				}
			}
		}
	}

	/// <summary>
	///       Invokes the stored <see cref="T:System.Delegate" />s with the given arguments.
	///       </summary>
	/// <param name="args">The arguments.</param>
	public void Invoke(params object[] args)
	{
		c652127f486d3fe0c1cc1e63428d89611();
		c7180016b8fd0464565a072d2ef58eb1d[] array = c4ff860504c1da0757f5bf226fe4a5680.ToArray();
		foreach (c7180016b8fd0464565a072d2ef58eb1d c7180016b8fd0464565a072d2ef58eb1d2 in array)
		{
			c7180016b8fd0464565a072d2ef58eb1d2.c0a27cdd8cef5186286b408128a586c9d(args);
		}
		while (true)
		{
			switch (5)
			{
			case 0:
				continue;
			}
			if (1 == 0)
			{
				__ldtoken(WeakMulticastDelegate.Invoke(object[]));
			}
			return;
		}
	}

	private void c652127f486d3fe0c1cc1e63428d89611()
	{
		for (int num = c4ff860504c1da0757f5bf226fe4a5680.Count - 1; num >= 0; num--)
		{
			if (!c4ff860504c1da0757f5bf226fe4a5680[num].ceb345e599e75ceeff1e14b0ac2cf0104)
			{
				while (true)
				{
					switch (3)
					{
					case 0:
						continue;
					}
					break;
				}
				if (1 == 0)
				{
					__ldtoken(WeakMulticastDelegate.c652127f486d3fe0c1cc1e63428d89611());
				}
				c4ff860504c1da0757f5bf226fe4a5680.RemoveAt(num);
			}
		}
		while (true)
		{
			switch (6)
			{
			case 0:
				break;
			default:
				return;
			}
		}
	}
}
/// <summary>
///       Represents a <see cref="T:System.MulticastDelegate" /> that stores the target objects as weak 
///       references.
///       </summary>
/// <typeparam name="T">The type of delegate.</typeparam>
public class WeakMulticastDelegate<T> : WeakMulticastDelegate where T : class
{
	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.WeakMulticastDelegate`1" /> class</summary>
	public WeakMulticastDelegate()
	{
	}

	/// <summary>
	///       Adds a new <see cref="T:System.Delegate" /> of a given type to the 
	///       <see cref="T:DigitalRune.WeakMulticastDelegate`1" />.
	///       </summary>
	/// <param name="delegate">The new <see cref="T:System.Delegate" />.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="delegate" /> is <see langword="null" />.
	///       </exception>
	public void Add(T @delegate)
	{
		Add((Delegate)(object)@delegate);
	}

	/// <summary>
	///       Removes a <see cref="T:System.Delegate" /> of a given type from the 
	///       <see cref="T:DigitalRune.WeakMulticastDelegate`1" />.
	///       </summary>
	/// <param name="delegate">The <see cref="T:System.Delegate" /> to remove.</param>
	public void Remove(T @delegate)
	{
		Remove((Delegate)(object)@delegate);
	}
}
