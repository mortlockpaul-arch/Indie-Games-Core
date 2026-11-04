using System;
using System.Reflection;
using A;

namespace DigitalRune;

/// <summary>
///       Represents a <see cref="P:DigitalRune.WeakDelegate.Delegate" /> that stores the target object as a weak reference.
///       </summary>
public class WeakDelegate
{
	private readonly c7180016b8fd0464565a072d2ef58eb1d cd16f846fdcd3098b4a1a80ba92556ea3;

	/// <summary>
	///       Gets the weak reference of the target object.
	///       </summary>
	public WeakReference TargetReference => cd16f846fdcd3098b4a1a80ba92556ea3.c4bbdd2ec94609ed106e71dd6f5679d67;

	/// <summary>
	///       Gets the metadata of the delegate method.
	///       </summary>
	public MethodInfo MethodInfo => cd16f846fdcd3098b4a1a80ba92556ea3.c398ca7cf438dac411558bed96056a177;

	/// <summary>
	///       Gets the type of delegate.
	///       </summary>
	public Type DelegateType => cd16f846fdcd3098b4a1a80ba92556ea3.cae41735bf8b6e5574819efa591cd7d8d;

	/// <summary>
	///       Gets the <see cref="T:System.Delegate" /> stored by the current <see cref="T:DigitalRune.WeakDelegate" /> 
	///       object.
	///       </summary>
	public Delegate Delegate => cd16f846fdcd3098b4a1a80ba92556ea3.c200fbad0dca0e5295345b463dafbe426;

	/// <summary>
	///       Gets a value indicating whether the <see cref="T:System.Delegate" /> referenced by this 
	///       <see cref="T:DigitalRune.WeakDelegate" /> has been garbage collected.
	///       </summary>
	public bool IsAlive => cd16f846fdcd3098b4a1a80ba92556ea3.ceb345e599e75ceeff1e14b0ac2cf0104;

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.WeakDelegate" /> class.
	///       </summary>
	/// <param name="delegate">
	///       The original <see cref="T:System.Delegate" /> to create a weak reference for.
	///       </param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="delegate" /> is <see langword="null" />.
	///       </exception>
	public WeakDelegate(Delegate @delegate)
	{
		cd16f846fdcd3098b4a1a80ba92556ea3 = new c7180016b8fd0464565a072d2ef58eb1d(@delegate);
	}

	/// <summary>
	///       Invokes the stored <see cref="T:System.Delegate" /> with the given arguments.
	///       </summary>
	/// <param name="args">The arguments.</param>
	public void Invoke(params object[] args)
	{
		cd16f846fdcd3098b4a1a80ba92556ea3.c0a27cdd8cef5186286b408128a586c9d(args);
	}
}
/// <summary>
///       Represents a <see cref="T:System.Delegate" /> of a specific type that stores the target object as a weak 
///       reference.
///       </summary>
/// <typeparam name="T">The type of delegate.</typeparam>
public class WeakDelegate<T> : WeakDelegate where T : class
{
	static WeakDelegate()
	{
		if (typeof(T).IsSubclassOf(typeof(Delegate)))
		{
			return;
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
				__ldtoken(WeakDelegate<>._002Ecctor());
			}
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5344));
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.WeakDelegate`1" /> class.
	///       </summary>
	/// <param name="target">
	///       The original <see cref="T:System.Delegate" /> to create a weak reference for.
	///       </param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="target" /> is <see langword="null" />.
	///       </exception>
	public WeakDelegate(T target)
		: base((Delegate)(object)target)
	{
	}
}
