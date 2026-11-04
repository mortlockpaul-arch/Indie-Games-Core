using System;
using System.Collections.Generic;
using System.Linq;
using A;
using DigitalRune.Collections;

namespace DigitalRune;

/// <summary>
///       Manages a pool of reusable items (base implementation).
///       </summary>
public abstract class ResourcePool
{
	private static readonly WeakCollection<ResourcePool> cc1e906066d65de2622e7400d261d0cb1 = new WeakCollection<ResourcePool>();

	private static bool caedbe52930a05951091c8dcdf6f7346c = true;

	/// <summary>
	///       Gets or sets a value indicating whether resource pooling is enabled.
	///       </summary>
	public static bool Enabled
	{
		get
		{
			return caedbe52930a05951091c8dcdf6f7346c;
		}
		set
		{
			if (caedbe52930a05951091c8dcdf6f7346c == value)
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
					__ldtoken(ResourcePool.set_Enabled(bool));
				}
				ClearAll();
				caedbe52930a05951091c8dcdf6f7346c = value;
				return;
			}
		}
	}

	/// <summary>
	///       Gets collection of all active <see cref="T:DigitalRune.ResourcePool" />s.
	///       </summary>
	public static IEnumerable<ResourcePool> Pools
	{
		get
		{
			lock (cc1e906066d65de2622e7400d261d0cb1)
			{
				return cc1e906066d65de2622e7400d261d0cb1.ToArray();
			}
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.ResourcePool" /> class</summary>
	protected ResourcePool()
	{
	}

	internal static void c23f143076f759fdfeffbea623e15f36c(ResourcePool ce41a65171dc6bb022d76c107ecba9b79)
	{
		lock (cc1e906066d65de2622e7400d261d0cb1)
		{
			cc1e906066d65de2622e7400d261d0cb1.Add(ce41a65171dc6bb022d76c107ecba9b79);
		}
	}

	/// <summary>
	///       Clears all resource pools.
	///       </summary>
	public static void ClearAll()
	{
		lock (cc1e906066d65de2622e7400d261d0cb1)
		{
			foreach (ResourcePool item in (IEnumerable<ResourcePool>)cc1e906066d65de2622e7400d261d0cb1)
			{
				item.Clear();
			}
		}
	}

	/// <summary>
	///       Removes all items from the resource pool.
	///       </summary>
	public abstract void Clear();
}
/// <summary>
///       Manages a pool of typed, reusable items.
///       </summary>
/// <typeparam name="T">The type of the items.</typeparam>
public class ResourcePool<T> : ResourcePool where T : class
{
	private readonly caa5af503c382cc00f4f9ae5f1e1cf59c<T> ce28be6a6bb7190fa7286682159dc87de;

	private readonly Func<T> cd4c71713eabd5d76a01b6703c56030e7;

	private readonly Action<T> ce3be715fe33d7eb6fc84598e5f847351;

	private readonly Action<T> c6ecb3f6a58a8c8dafb77263bb7afbf00;

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.ResourcePool`1" /> class with the given
	///       un-/initialize methods.
	///       </summary>
	/// <param name="create">
	///       The function that creates a new item of type <typeparamref name="T" />.
	///       </param>
	/// <param name="initialize">
	///       The method that is executed on an item when it is obtained from the pool - can be
	///       <see langword="null" />.
	///       </param>
	/// <param name="uninitialize">
	///       The method that is executed on an item when it is recycled - can be <see langword="null" />.
	///       </param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="create" /> is <see langword="null" />.
	///       </exception>
	public ResourcePool(Func<T> create, Action<T> initialize, Action<T> uninitialize)
	{
		if (create == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1013));
		}
		cd4c71713eabd5d76a01b6703c56030e7 = create;
		ce3be715fe33d7eb6fc84598e5f847351 = initialize;
		c6ecb3f6a58a8c8dafb77263bb7afbf00 = uninitialize;
		ce28be6a6bb7190fa7286682159dc87de = new caa5af503c382cc00f4f9ae5f1e1cf59c<T>();
		ResourcePool.c23f143076f759fdfeffbea623e15f36c(this);
	}

	/// <summary>
	///       Removes all items from the resource pool.
	///       </summary>
	public override void Clear()
	{
		if (ce28be6a6bb7190fa7286682159dc87de.ca2ab9bfbe566d4bbb2f79385a1dc216b > 0)
		{
			lock (ce28be6a6bb7190fa7286682159dc87de)
			{
				ce28be6a6bb7190fa7286682159dc87de.c0a4aead06b14dcb81618057a59d4a433();
			}
		}
	}

	/// <summary>
	///       Obtains a new item by reusing an instance from the pool or by creating a new instance if 
	///       necessary.
	///       </summary>
	/// <returns>The item.</returns>
	public T Obtain()
	{
		T val = null;
		if (ResourcePool.Enabled)
		{
			lock (ce28be6a6bb7190fa7286682159dc87de)
			{
				val = ce28be6a6bb7190fa7286682159dc87de.c8291bb0ce023b5713dd6741dca85a2cc();
			}
		}
		if (val == null)
		{
			val = cd4c71713eabd5d76a01b6703c56030e7();
		}
		if (ce3be715fe33d7eb6fc84598e5f847351 != null)
		{
			ce3be715fe33d7eb6fc84598e5f847351(val);
		}
		return val;
	}

	/// <summary>
	///       Recycles the given item and places it back in the pool for future reuse.
	///       </summary>
	/// <param name="item">The item to be recycled.</param>
	public void Recycle(T item)
	{
		if (c6ecb3f6a58a8c8dafb77263bb7afbf00 != null)
		{
			c6ecb3f6a58a8c8dafb77263bb7afbf00(item);
		}
		if (ResourcePool.Enabled)
		{
			lock (ce28be6a6bb7190fa7286682159dc87de)
			{
				ce28be6a6bb7190fa7286682159dc87de.c22e6a1d6a12c03e9bfc60b749df7f18b(item);
			}
		}
	}
}
