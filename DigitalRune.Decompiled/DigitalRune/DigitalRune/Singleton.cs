using System;
using A;

namespace DigitalRune;

/// <summary>
///       Base class for types to restrict instantiation to one object ("singleton pattern").
///       </summary>
/// <typeparam name="T">The type of the object that is instantiated.</typeparam>
public abstract class Singleton<T> where T : Singleton<T>, new()
{
	private static readonly object c5547948d65d53162d7c76e0c961f6fb7 = new object();

	private static T cbe529900b7e6790f68661f58d209dfb1;

	/// <summary>
	///       Gets the singleton of type <typeparamref name="T" />.
	///       </summary>
	public static T Instance
	{
		get
		{
			if (cbe529900b7e6790f68661f58d209dfb1 == null)
			{
				lock (c5547948d65d53162d7c76e0c961f6fb7)
				{
					if (cbe529900b7e6790f68661f58d209dfb1 == null)
					{
						while (true)
						{
							switch (7)
							{
							case 0:
								continue;
							}
							if (1 == 0)
							{
								__ldtoken(Singleton<>.get_Instance());
							}
							cbe529900b7e6790f68661f58d209dfb1 = new T();
							break;
						}
					}
				}
			}
			return cbe529900b7e6790f68661f58d209dfb1;
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Singleton`1" /> class.
	///       </summary>
	/// <exception cref="T:System.InvalidOperationException">
	///       Constructor is called several times, but <see cref="T:DigitalRune.Singleton`1" /> can only be instantiated 
	///       once.
	///       </exception>
	/// <exception cref="T:System.InvalidOperationException">
	///       The class which derives from <see cref="T:DigitalRune.Singleton`1" /> is not of type 
	///       <typeparamref name="T" />.
	///       </exception>
	protected Singleton()
	{
		if (cbe529900b7e6790f68661f58d209dfb1 != null)
		{
			while (true)
			{
				switch (3)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(Singleton<>._002Ector());
					}
					throw new InvalidOperationException(string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5722), typeof(T)));
				}
			}
		}
		cbe529900b7e6790f68661f58d209dfb1 = this as T;
		if (cbe529900b7e6790f68661f58d209dfb1 == null)
		{
			throw new InvalidOperationException(string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5809), GetType()));
		}
	}
}
