using System;
using System.Collections.Generic;
using System.Threading;
using A;
using DigitalRune.Collections;
using DigitalRune.Geometry.Partitioning;

namespace DigitalRune.Physics;

/// <summary>
///       Defines whether collision response between rigid bodies is enabled or disabled.
///       </summary>
public class CollisionResponseFilter : IPairFilter<RigidBody>
{
	private readonly HashSet<Pair<RigidBody>> c4eb715eb425c941ae5877e4e8431b961 = new HashSet<Pair<RigidBody>>();

	private EventHandler<EventArgs> ce35cebb10815eb5ffb8dca1b5d73af8c;

	/// <summary>
	///       Occurs when the filter rules have changed.
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
			while (true)
			{
				switch (1)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(CollisionResponseFilter.add_Changed(EventHandler<EventArgs>));
				}
				return;
			}
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
			while (true)
			{
				switch (7)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(CollisionResponseFilter.remove_Changed(EventHandler<EventArgs>));
				}
				return;
			}
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.Physics.CollisionResponseFilter" /> class</summary>
	public CollisionResponseFilter()
	{
	}

	/// <summary>
	///       Resets this filter. Collision response will be enabled for all pairs of rigid bodies.
	///       </summary>
	public void Reset()
	{
		c4eb715eb425c941ae5877e4e8431b961.Clear();
		OnChanged(EventArgs.Empty);
	}

	/// <summary>
	///       Enables or disables collision response between the given rigid bodies.
	///       </summary>
	/// <param name="bodyA">The first rigid bodies.</param>
	/// <param name="bodyB">The second rigid bodies.</param>
	/// <param name="responseEnabled">
	///       If set to <see langword="true" /> the collision response between <paramref name="bodyA" /> and 
	///       <paramref name="bodyB" /> is enabled. Use <see langword="false" /> to disable the collision
	///       response.
	///       </param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="bodyA" /> or <paramref name="bodyB" /> is <see langword="null" />.
	///       </exception>
	public void Set(RigidBody bodyA, RigidBody bodyB, bool responseEnabled)
	{
		if (bodyA == null)
		{
			while (true)
			{
				switch (6)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(CollisionResponseFilter.Set(RigidBody, RigidBody, bool));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1729));
				}
			}
		}
		if (bodyB == null)
		{
			while (true)
			{
				switch (3)
				{
				case 0:
					break;
				default:
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1740));
				}
			}
		}
		Pair<RigidBody> element = new Pair<RigidBody>(bodyA, bodyB);
		if (responseEnabled)
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
			c4eb715eb425c941ae5877e4e8431b961.Remove(element);
		}
		else
		{
			c4eb715eb425c941ae5877e4e8431b961.Add(element);
		}
		OnChanged(EventArgs.Empty);
	}

	/// <summary>
	///       Returns <see langword="true" /> if collision response is enabled for the given pair.
	///       </summary>
	/// <param name="pair">The pair of rigid bodies.</param>
	/// <returns>
	///   <see langword="true" /> if collision response is enabled; otherwise, <see langword="false" />.
	///       </returns>
	public bool Filter(Pair<RigidBody> pair)
	{
		return !c4eb715eb425c941ae5877e4e8431b961.Contains(pair);
	}

	/// <summary>
	///       Raises the <see cref="E:DigitalRune.Physics.CollisionResponseFilter.Changed" /> event.
	///       </summary>
	/// <param name="eventArgs">
	///   <see cref="T:System.EventArgs" /> object that provides the arguments for the event.
	///       </param>
	protected virtual void OnChanged(EventArgs eventArgs)
	{
		EventHandler<EventArgs> eventHandler = ce35cebb10815eb5ffb8dca1b5d73af8c;
		if (eventHandler == null)
		{
			return;
		}
		while (true)
		{
			switch (3)
			{
			case 0:
				continue;
			}
			if (1 == 0)
			{
				__ldtoken(CollisionResponseFilter.OnChanged(EventArgs));
			}
			eventHandler(this, eventArgs);
			return;
		}
	}
}
