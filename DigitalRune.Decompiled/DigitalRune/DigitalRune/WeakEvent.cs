using System;
using System.Reflection;
using A;

namespace DigitalRune;

/// <summary>
///       Represents an event that stores the target objects as weak 
///       references.
///       </summary>
/// <typeparam name="T">
///       The type of event handler. Must be of type <see cref="T:System.EventHandler" /> or 
///       <see cref="T:System.EventHandler`1" />.
///       </typeparam>
public sealed class WeakEvent<T> : WeakMulticastDelegate<T> where T : class
{
	static WeakEvent()
	{
		MethodInfo method = typeof(T).GetMethod(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(18));
		if ((object)method == null || method.GetParameters().Length != 2)
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(31));
		}
		ParameterInfo parameterInfo = method.GetParameters()[0];
		if ((object)parameterInfo.ParameterType != typeof(object))
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
						__ldtoken(WeakEvent<>._002Ecctor());
					}
					throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(122));
				}
			}
		}
		ParameterInfo parameterInfo2 = method.GetParameters()[1];
		if (!typeof(EventArgs).IsAssignableFrom(parameterInfo2.ParameterType))
		{
			while (true)
			{
				switch (1)
				{
				case 0:
					break;
				default:
					throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(229));
				}
			}
		}
		if ((object)method.ReturnType != typeof(void))
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(365));
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.WeakEvent`1" /> class</summary>
	public WeakEvent()
	{
	}

	/// <summary>
	///       Raises the event.
	///       </summary>
	/// <param name="sender">The sender.</param>
	/// <param name="eventArgs">
	///       The <see cref="T:System.EventArgs" /> instance containing the event data.
	///       </param>
	public void Invoke(object sender, EventArgs eventArgs)
	{
		Invoke(sender, (object)eventArgs);
	}
}
