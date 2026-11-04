using System;
using System.Threading;
using DigitalRune.Collections;

namespace DigitalRune.Geometry.Partitioning;

/// <summary>
///       Filters item pairs with the help of a user-defined callback method.
///       </summary>
/// <typeparam name="T">The type of the items.</typeparam>
public class DelegatePairFilter<T> : IPairFilter<T>
{
	private Func<Pair<T>, bool> c6df08418a9b49678ce4ec9907246da5a;

	private EventHandler<EventArgs> ce35cebb10815eb5ffb8dca1b5d73af8c;

	/// <summary>
	///       Gets or sets the filter callback.
	///       </summary>
	public Func<Pair<T>, bool> FilterCallback
	{
		get
		{
			return c6df08418a9b49678ce4ec9907246da5a;
		}
		set
		{
			if (!(c6df08418a9b49678ce4ec9907246da5a != value))
			{
				return;
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
					__ldtoken(DelegatePairFilter<>.set_FilterCallback(Func<Pair<T>, bool>));
				}
				c6df08418a9b49678ce4ec9907246da5a = value;
				RaiseChanged();
				return;
			}
		}
	}

	/// <summary>
	///       Occurs when the filter rules were changed.
	///       </summary>
	public event EventHandler<EventArgs> Changed
	{
		add
		{
			EventHandler<EventArgs> eventHandler = ce35cebb10815eb5ffb8dca1b5d73af8c;
			EventHandler<EventArgs> eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler<EventArgs> value2 = (EventHandler<EventArgs>)Delegate.Combine(eventHandler2, value);
				eventHandler = Interlocked.CompareExchange(ref ce35cebb10815eb5ffb8dca1b5d73af8c, value2, eventHandler2);
			}
			while ((object)eventHandler != eventHandler2);
		}
		remove
		{
			EventHandler<EventArgs> eventHandler = ce35cebb10815eb5ffb8dca1b5d73af8c;
			EventHandler<EventArgs> eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler<EventArgs> value2 = (EventHandler<EventArgs>)Delegate.Remove(eventHandler2, value);
				eventHandler = Interlocked.CompareExchange(ref ce35cebb10815eb5ffb8dca1b5d73af8c, value2, eventHandler2);
			}
			while ((object)eventHandler != eventHandler2);
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Partitioning.DelegatePairFilter`1" /> class.
	///       </summary>
	/// <param name="filterCallback">The filter callback (can be <see langword="null" />).</param>
	public DelegatePairFilter(Func<Pair<T>, bool> filterCallback)
	{
		c6df08418a9b49678ce4ec9907246da5a = filterCallback;
	}

	/// <summary>
	///       Filters the specified item pair.
	///       </summary>
	/// <param name="pair">The pair.</param>
	/// <returns>
	///   <see langword="true" /> if the pair should be processed (pair is accepted); otherwise,
	///       <see langword="false" /> if the pair should not be processed (pair is rejected).
	///       </returns>
	public bool Filter(Pair<T> pair)
	{
		if (FilterCallback != null)
		{
			return FilterCallback(pair);
		}
		return true;
	}

	/// <summary>
	///       Raises the <see cref="E:DigitalRune.Geometry.Partitioning.DelegatePairFilter`1.Changed" /> event.
	///       </summary>
	public void RaiseChanged()
	{
		OnChanged(EventArgs.Empty);
	}

	/// <summary>
	///       Raises the <see cref="E:DigitalRune.Geometry.Partitioning.DelegatePairFilter`1.Changed" /> event.
	///       </summary>
	/// <param name="eventArgs">
	///   <see cref="T:System.EventArgs" /> object that provides the arguments for the event.
	///       </param>
	protected virtual void OnChanged(EventArgs eventArgs)
	{
		ce35cebb10815eb5ffb8dca1b5d73af8c?.Invoke(this, eventArgs);
	}
}
