using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using A;
using DigitalRune.Collections;
using DigitalRune.Geometry.Partitioning;

namespace DigitalRune.Geometry.Collisions;

/// <summary>
///       A basic collision filter supporting pairwise filtering and collision groups.
///       </summary>
public class CollisionFilter : IPairFilter<CollisionObject>
{
	private readonly bool[] c3bc44d90a10de60ea78a1c55d1a0daf5;

	private readonly HashSet<Pair<CollisionObject>> c4eb715eb425c941ae5877e4e8431b961 = new HashSet<Pair<CollisionObject>>();

	private readonly bool[,] cea144607eef57041e71b27824fe22f8d;

	private EventHandler<EventArgs> ce35cebb10815eb5ffb8dca1b5d73af8c;

	[CompilerGenerated]
	private int c992834636215c1fb5901a03c864d68f9;

	/// <summary>
	///       The maximum number of supported collision groups.
	///       </summary>
	public int MaxNumberOfGroups
	{
		[CompilerGenerated]
		get
		{
			return c992834636215c1fb5901a03c864d68f9;
		}
		[CompilerGenerated]
		private set
		{
			c992834636215c1fb5901a03c864d68f9 = value;
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
			while (true)
			{
				switch (5)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(CollisionFilter.add_Changed(EventHandler<EventArgs>));
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
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Collisions.CollisionFilter" /> class for max. 32 
	///       different collision groups.
	///       </summary>
	public CollisionFilter()
		: this(32)
	{
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Collisions.CollisionFilter" /> class for the given
	///       max. number of collision groups.
	///       </summary>
	/// <param name="maxNumberOfGroups">
	///       The maximum number of groups (see <see cref="P:DigitalRune.Geometry.Collisions.CollisionFilter.MaxNumberOfGroups" />).
	///       </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="maxNumberOfGroups" /> is negative.
	///       </exception>
	public CollisionFilter(int maxNumberOfGroups)
	{
		if (maxNumberOfGroups < 0)
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
						__ldtoken(CollisionFilter._002Ector(int));
					}
					throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4267), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4302));
				}
			}
		}
		MaxNumberOfGroups = maxNumberOfGroups;
		c3bc44d90a10de60ea78a1c55d1a0daf5 = new bool[MaxNumberOfGroups];
		cea144607eef57041e71b27824fe22f8d = new bool[MaxNumberOfGroups, MaxNumberOfGroups];
		Reset();
	}

	/// <summary>
	///       Resets this filter. All collisions will be enabled.
	///       </summary>
	public virtual void Reset()
	{
		c4eb715eb425c941ae5877e4e8431b961.Clear();
		for (int i = 0; i < MaxNumberOfGroups; i++)
		{
			c3bc44d90a10de60ea78a1c55d1a0daf5[i] = true;
		}
		while (true)
		{
			switch (1)
			{
			case 0:
				continue;
			}
			if (1 == 0)
			{
				__ldtoken(CollisionFilter.Reset());
			}
			for (int j = 0; j < MaxNumberOfGroups; j++)
			{
				for (int k = 0; k < MaxNumberOfGroups; k++)
				{
					cea144607eef57041e71b27824fe22f8d[j, k] = true;
				}
			}
			while (true)
			{
				switch (3)
				{
				case 0:
					continue;
				}
				OnChanged(EventArgs.Empty);
				return;
			}
		}
	}

	/// <summary>
	///       Enables or disables collisions between the given <see cref="T:DigitalRune.Geometry.Collisions.CollisionObject" />s.
	///       </summary>
	/// <param name="objectA">The first collision object.</param>
	/// <param name="objectB">The second collision object.</param>
	/// <param name="collisionsEnabled">
	///       If set to <see langword="true" /> collisions between <paramref name="objectA" /> and 
	///       <paramref name="objectB" /> are enabled. Use <see langword="false" /> to disable collisions.
	///       </param>
	public void Set(CollisionObject objectA, CollisionObject objectB, bool collisionsEnabled)
	{
		if (!collisionsEnabled)
		{
			c4eb715eb425c941ae5877e4e8431b961.Add(new Pair<CollisionObject>(objectA, objectB));
		}
		else
		{
			c4eb715eb425c941ae5877e4e8431b961.Remove(new Pair<CollisionObject>(objectA, objectB));
		}
		OnChanged(EventArgs.Empty);
	}

	/// <summary>
	///       Enables or disables collisions with the given collision group.
	///       </summary>
	/// <param name="collisionGroup">The collision group.</param>
	/// <param name="collisionsEnabled">
	///       If set to <see langword="true" /> collisions for object in the given group are enabled. Use 
	///       <see langword="false" /> to disable collisions.
	///       </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="collisionGroup" /> is out of range.
	///       </exception>
	public void Set(int collisionGroup, bool collisionsEnabled)
	{
		if (collisionGroup >= 0)
		{
			if (collisionGroup <= MaxNumberOfGroups - 1)
			{
				c3bc44d90a10de60ea78a1c55d1a0daf5[collisionGroup] = collisionsEnabled;
				OnChanged(EventArgs.Empty);
				return;
			}
			while (true)
			{
				switch (6)
				{
				case 0:
					continue;
				}
				break;
			}
			if (1 == 0)
			{
				__ldtoken(CollisionFilter.Set(int, bool));
			}
		}
		throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4082), string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4111), MaxNumberOfGroups - 1));
	}

	/// <summary>
	///       Enabled or disables collisions between a pair of collision groups.
	///       </summary>
	/// <param name="groupA">The first collision group.</param>
	/// <param name="groupB">The second collision group.</param>
	/// <param name="collisionsEnabled">
	///       if set to <see langword="true" /> collisions between objects in <paramref name="groupA" /> and 
	///       objects in <paramref name="groupB" /> are enabled. Use <see langword="false" /> to disable 
	///       collisions.
	///       </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="groupA" /> is out of range.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="groupB" /> is out of range.
	///       </exception>
	public void Set(int groupA, int groupB, bool collisionsEnabled)
	{
		if (groupA >= 0)
		{
			while (true)
			{
				switch (6)
				{
				case 0:
					continue;
				}
				break;
			}
			if (1 == 0)
			{
				__ldtoken(CollisionFilter.Set(int, int, bool));
			}
			if (groupA <= MaxNumberOfGroups - 1)
			{
				if (groupB >= 0)
				{
					while (true)
					{
						switch (6)
						{
						case 0:
							continue;
						}
						break;
					}
					if (groupB <= MaxNumberOfGroups - 1)
					{
						if (groupA < groupB)
						{
							cea144607eef57041e71b27824fe22f8d[groupA, groupB] = collisionsEnabled;
						}
						else
						{
							cea144607eef57041e71b27824fe22f8d[groupB, groupA] = collisionsEnabled;
						}
						OnChanged(EventArgs.Empty);
						return;
					}
				}
				throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4254), string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4111), MaxNumberOfGroups - 1));
			}
			while (true)
			{
				switch (5)
				{
				case 0:
					continue;
				}
				break;
			}
		}
		throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4241), string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4111), MaxNumberOfGroups - 1));
	}

	/// <summary>
	///       Returns <see langword="true" /> if collisions between two <see cref="T:DigitalRune.Geometry.Collisions.CollisionObject" />s are
	///       enabled (without testing collision groups).
	///       </summary>
	/// <param name="objectA">The first collision object.</param>
	/// <param name="objectB">The second collision object.</param>
	/// <returns>
	///   <see langword="true" /> if collisions between the given <see cref="T:DigitalRune.Geometry.Collisions.CollisionObject" /> pair 
	///       are enabled; otherwise <see langword="false" />.
	///       </returns>
	public bool Get(CollisionObject objectA, CollisionObject objectB)
	{
		return !c4eb715eb425c941ae5877e4e8431b961.Contains(new Pair<CollisionObject>(objectA, objectB));
	}

	/// <summary>
	///       Returns <see langword="true" /> if collisions with the given collision group are enabled.
	///       </summary>
	/// <param name="collisionGroup">The collision group.</param>
	/// <returns>
	///   <see langword="true" /> if collisions with the given collision group are enabled; otherwise 
	///       <see langword="false" />.
	///       </returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="collisionGroup" /> is out of range.
	///       </exception>
	public bool Get(int collisionGroup)
	{
		if (collisionGroup < 0 || collisionGroup > MaxNumberOfGroups - 1)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4082), string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4111), MaxNumberOfGroups - 1));
		}
		return c3bc44d90a10de60ea78a1c55d1a0daf5[collisionGroup];
	}

	/// <summary>
	///       Returns <see langword="true" /> if collisions between two collision groups are enabled.
	///       </summary>
	/// <param name="groupA">The first collision group.</param>
	/// <param name="groupB">The second collision group.</param>
	/// <returns>
	///   <see langword="true" /> if collisions with the between <paramref name="groupA" /> and 
	///       <paramref name="groupB" />are enabled; otherwise <see langword="false" />.
	///       </returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="groupA" /> is out of range.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="groupB" /> is out of range.
	///       </exception>
	public bool Get(int groupA, int groupB)
	{
		if (groupA >= 0)
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
				__ldtoken(CollisionFilter.Get(int, int));
			}
			if (groupA <= MaxNumberOfGroups - 1)
			{
				if (groupB >= 0)
				{
					if (groupB <= MaxNumberOfGroups - 1)
					{
						if (groupA < groupB)
						{
							return cea144607eef57041e71b27824fe22f8d[groupA, groupB];
						}
						return cea144607eef57041e71b27824fe22f8d[groupB, groupA];
					}
					while (true)
					{
						switch (4)
						{
						case 0:
							continue;
						}
						break;
					}
				}
				throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4254), string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4111), MaxNumberOfGroups - 1));
			}
		}
		throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4241), string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4111), MaxNumberOfGroups - 1));
	}

	/// <summary>
	///       Determines whether the given <see cref="T:DigitalRune.Geometry.Collisions.CollisionObject" />s can collide.
	///       </summary>
	/// <param name="pair">The pair of collision objects.</param>
	/// <returns>
	///   <see langword="true" /> if the pair of collision objects can collide; otherwise, 
	///       <see langword="false" /> if the objects cannot collide.
	///       </returns>
	public virtual bool Filter(Pair<CollisionObject> pair)
	{
		if (Get(pair.First.CollisionGroup) && Get(pair.Second.CollisionGroup) && Get(pair.First.CollisionGroup, pair.Second.CollisionGroup))
		{
			return Get(pair.First, pair.Second);
		}
		return false;
	}

	/// <summary>
	///       Raises the <see cref="E:DigitalRune.Geometry.Collisions.CollisionFilter.Changed" /> event.
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
			switch (7)
			{
			case 0:
				continue;
			}
			if (1 == 0)
			{
				__ldtoken(CollisionFilter.OnChanged(EventArgs));
			}
			eventHandler(this, eventArgs);
			return;
		}
	}
}
