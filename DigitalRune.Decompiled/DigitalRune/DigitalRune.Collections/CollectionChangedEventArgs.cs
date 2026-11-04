using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace DigitalRune.Collections;

/// <summary>
///       Provides data for the <see cref="E:DigitalRune.Collections.NotifyingCollection`1.CollectionChanged" /> event.
///       </summary>
/// <typeparam name="T">The type of elements in the collection.</typeparam>
public class CollectionChangedEventArgs<T> : EventArgs
{
	private static readonly ResourcePool<CollectionChangedEventArgs<T>> c8aca4a0e3c0073ffb188fde832680587;

	[CompilerGenerated]
	private CollectionChangedAction c4362892a78f23a2a970e1c993d52ce65;

	[CompilerGenerated]
	private int ca06daf5077d611830972665523b5a94d;

	[CompilerGenerated]
	private int c7a34702afe9f38c6f671183c9999385e;

	[CompilerGenerated]
	private List<T> c2739b4a7da054da40dff36eb0b679f46;

	[CompilerGenerated]
	private List<T> cb4a1138e7bee858e4d1ebc1d0717d0cd;

	[CompilerGenerated]
	private static Func<CollectionChangedEventArgs<T>> c49253b4520c1b7e183dfcc9c426c0283;

	/// <summary>
	///       Gets or sets the action that caused the event.
	///       </summary>
	public CollectionChangedAction Action
	{
		[CompilerGenerated]
		get
		{
			return c4362892a78f23a2a970e1c993d52ce65;
		}
		[CompilerGenerated]
		set
		{
			c4362892a78f23a2a970e1c993d52ce65 = value;
		}
	}

	/// <summary>
	///       Gets or sets the index of the first new item.
	///       </summary>
	public int NewItemsIndex
	{
		[CompilerGenerated]
		get
		{
			return ca06daf5077d611830972665523b5a94d;
		}
		[CompilerGenerated]
		set
		{
			ca06daf5077d611830972665523b5a94d = value;
		}
	}

	/// <summary>
	///       Gets or sets the index where the first old item was placed in the collection.
	///       </summary>
	public int OldItemsIndex
	{
		[CompilerGenerated]
		get
		{
			return c7a34702afe9f38c6f671183c9999385e;
		}
		[CompilerGenerated]
		set
		{
			c7a34702afe9f38c6f671183c9999385e = value;
		}
	}

	/// <summary>
	///       Gets the new items that were involved in the change.
	///       </summary>
	public List<T> NewItems
	{
		[CompilerGenerated]
		get
		{
			return c2739b4a7da054da40dff36eb0b679f46;
		}
		[CompilerGenerated]
		private set
		{
			c2739b4a7da054da40dff36eb0b679f46 = value;
		}
	}

	/// <summary>
	///       Gets the items affected by a replace, remove or move action.
	///       </summary>
	public List<T> OldItems
	{
		[CompilerGenerated]
		get
		{
			return cb4a1138e7bee858e4d1ebc1d0717d0cd;
		}
		[CompilerGenerated]
		private set
		{
			cb4a1138e7bee858e4d1ebc1d0717d0cd = value;
		}
	}

	private CollectionChangedEventArgs()
	{
		NewItemsIndex = -1;
		NewItems = new List<T>();
		OldItemsIndex = -1;
		OldItems = new List<T>();
	}

	static CollectionChangedEventArgs()
	{
		if (c49253b4520c1b7e183dfcc9c426c0283 == null)
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
			if (1 == 0)
			{
				__ldtoken(CollectionChangedEventArgs<>._002Ecctor());
			}
			c49253b4520c1b7e183dfcc9c426c0283 = () => new CollectionChangedEventArgs<T>();
		}
		c8aca4a0e3c0073ffb188fde832680587 = new ResourcePool<CollectionChangedEventArgs<T>>(c49253b4520c1b7e183dfcc9c426c0283, null, null);
	}

	/// <summary>
	///       Creates an instance of the <see cref="T:DigitalRune.Collections.CollectionChangedEventArgs`1" /> class. (This method 
	///       reuses a previously recycled instance or allocates a new instance if necessary.)
	///       </summary>
	/// <returns>
	///       A new or reusable instance of the <see cref="T:DigitalRune.Collections.CollectionChangedEventArgs`1" /> class.
	///       </returns>
	public static CollectionChangedEventArgs<T> Create()
	{
		return c8aca4a0e3c0073ffb188fde832680587.Obtain();
	}

	/// <summary>
	///       Recycles this instance of the <see cref="T:DigitalRune.Collections.CollectionChangedEventArgs`1" /> class.
	///       </summary>
	public void Recycle()
	{
		Action = CollectionChangedAction.Add;
		NewItemsIndex = -1;
		NewItems.Clear();
		OldItemsIndex = -1;
		OldItems.Clear();
		c8aca4a0e3c0073ffb188fde832680587.Recycle(this);
	}

	[CompilerGenerated]
	private static CollectionChangedEventArgs<T> c4bffc38c4054efe6a3f3ff877749c085()
	{
		return new CollectionChangedEventArgs<T>();
	}
}
