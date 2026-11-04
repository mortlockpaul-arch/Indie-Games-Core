using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Threading;
using A;

namespace DigitalRune.Collections;

/// <summary>
///       Represents collection of objects that sends notifications (events) when the collection is 
///       modified.
///       </summary>
/// <typeparam name="T">The type of elements in the collection.</typeparam>
public class NotifyingCollection<T> : Collection<T>
{
	private bool c53353887105e37579dd9feacc25a1884;

	private EventHandler<CollectionChangedEventArgs<T>> cda7ce06a2ebed16de8fc3925490e31ac;

	[CompilerGenerated]
	private bool c87fa73c374468098d355162d059959f5;

	[CompilerGenerated]
	private bool c0e7cc43acc62bdfabd01aa76420a2d67;

	/// <summary>
	///       Gets or sets a value indicating whether <see langword="null" /> items are allowed 
	///       in the collection.
	///       </summary>
	public bool AllowNull
	{
		[CompilerGenerated]
		get
		{
			return c87fa73c374468098d355162d059959f5;
		}
		[CompilerGenerated]
		private set
		{
			c87fa73c374468098d355162d059959f5 = value;
		}
	}

	/// <summary>
	///       Gets or sets a value indicating whether duplicate items are allowed in 
	///       the collection.
	///       </summary>
	public bool AllowDuplicates
	{
		[CompilerGenerated]
		get
		{
			return c0e7cc43acc62bdfabd01aa76420a2d67;
		}
		[CompilerGenerated]
		private set
		{
			c0e7cc43acc62bdfabd01aa76420a2d67 = value;
		}
	}

	/// <summary>
	///       Event raised after items were added, replaced or removed.
	///       </summary>
	public event EventHandler<CollectionChangedEventArgs<T>> CollectionChanged
	{
		add
		{
			EventHandler<CollectionChangedEventArgs<T>> eventHandler = cda7ce06a2ebed16de8fc3925490e31ac;
			EventHandler<CollectionChangedEventArgs<T>> eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler<CollectionChangedEventArgs<T>> value2 = (EventHandler<CollectionChangedEventArgs<T>>)Delegate.Combine(eventHandler2, value);
				eventHandler = Interlocked.CompareExchange(ref cda7ce06a2ebed16de8fc3925490e31ac, value2, eventHandler2);
			}
			while ((object)eventHandler != eventHandler2);
		}
		remove
		{
			EventHandler<CollectionChangedEventArgs<T>> eventHandler = cda7ce06a2ebed16de8fc3925490e31ac;
			EventHandler<CollectionChangedEventArgs<T>> eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler<CollectionChangedEventArgs<T>> value2 = (EventHandler<CollectionChangedEventArgs<T>>)Delegate.Remove(eventHandler2, value);
				eventHandler = Interlocked.CompareExchange(ref cda7ce06a2ebed16de8fc3925490e31ac, value2, eventHandler2);
			}
			while ((object)eventHandler != eventHandler2);
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Collections.NotifyingCollection`1" /> class.
	///       </summary>
	public NotifyingCollection()
	{
		AllowNull = true;
		AllowDuplicates = true;
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Collections.NotifyingCollection`1" /> class with the given
	///       settings.
	///       </summary>
	/// <param name="allowNull">
	///       If set to <see langword="true" /> null items are allowed.
	///       </param>
	/// <param name="allowDuplicates">
	///       If set to <see langword="true" /> duplicate items are allowed.
	///       </param>
	public NotifyingCollection(bool allowNull, bool allowDuplicates)
	{
		AllowNull = allowNull;
		AllowDuplicates = allowDuplicates;
	}

	/// <summary>
	///       Removes all elements from the <see cref="T:System.Collections.ObjectModel.Collection`1" />.
	///       </summary>
	protected override void ClearItems()
	{
		if (c53353887105e37579dd9feacc25a1884)
		{
			base.ClearItems();
			return;
		}
		CollectionChangedEventArgs<T> e = CollectionChangedEventArgs<T>.Create();
		try
		{
			e.Action = CollectionChangedAction.Clear;
			e.OldItemsIndex = 0;
			for (int i = 0; i < Count; i++)
			{
				e.OldItems.Add(Items[i]);
			}
			base.ClearItems();
			OnCollectionChanged(e);
		}
		finally
		{
			e.Recycle();
		}
	}

	/// <summary>
	///       Inserts an element into the <see cref="T:System.Collections.ObjectModel.Collection`1" /> at the specified index.
	///       </summary>
	/// <param name="index">
	///       The zero-based index at which <paramref name="item" /> should be inserted.
	///       </param>
	/// <param name="item">The object to insert.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is less than zero or greater than 
	///       <see cref="P:System.Collections.ObjectModel.Collection`1.Count" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="item" /> is <see langword="null" />. The collection does not allow 
	///       <see langword="null" /> values. See <see cref="P:DigitalRune.Collections.NotifyingCollection`1.AllowNull" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="item" /> is already contained in the collection. The collection does not 
	///       allow duplicate items. See <see cref="P:DigitalRune.Collections.NotifyingCollection`1.AllowDuplicates" />.
	///       </exception>
	protected override void InsertItem(int index, T item)
	{
		if (!AllowNull && object.ReferenceEquals(item, null))
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2312));
		}
		if (!AllowDuplicates && Contains(item))
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2703));
		}
		base.InsertItem(index, item);
		if (c53353887105e37579dd9feacc25a1884)
		{
			return;
		}
		CollectionChangedEventArgs<T> e = CollectionChangedEventArgs<T>.Create();
		try
		{
			e.Action = CollectionChangedAction.Add;
			e.NewItems.Add(item);
			e.NewItemsIndex = index;
			OnCollectionChanged(e);
		}
		finally
		{
			e.Recycle();
		}
	}

	/// <summary>
	///       Removes the element at the specified index of the <see cref="T:System.Collections.ObjectModel.Collection`1" />.
	///       </summary>
	/// <param name="index">The zero-based index of the element to remove.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is less than zero or <paramref name="index" /> is equal to or 
	///       greater than <see cref="P:System.Collections.ObjectModel.Collection`1.Count" />.
	///       </exception>
	protected override void RemoveItem(int index)
	{
		if (c53353887105e37579dd9feacc25a1884)
		{
			base.RemoveItem(index);
			return;
		}
		CollectionChangedEventArgs<T> e = CollectionChangedEventArgs<T>.Create();
		try
		{
			e.Action = CollectionChangedAction.Remove;
			e.OldItems.Add(Items[index]);
			e.OldItemsIndex = index;
			base.RemoveItem(index);
			OnCollectionChanged(e);
		}
		finally
		{
			e.Recycle();
		}
	}

	/// <summary>
	///       Replaces the element at the specified index.
	///       </summary>
	/// <param name="index">The zero-based index of the element to replace.</param>
	/// <param name="item">The new value for the element at the specified index.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is less than zero or is greater than 
	///       <see cref="P:System.Collections.ObjectModel.Collection`1.Count" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="item" /> is <see langword="null" />. The collection does not allow 
	///       <see langword="null" /> values. See <see cref="P:DigitalRune.Collections.NotifyingCollection`1.AllowNull" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="item" /> is already contained in the collection. The collection does not 
	///       allow duplicate items. See <see cref="P:DigitalRune.Collections.NotifyingCollection`1.AllowDuplicates" />.
	///       </exception>
	protected override void SetItem(int index, T item)
	{
		T val = Items[index];
		if (object.ReferenceEquals(item, val))
		{
			return;
		}
		if (!AllowNull && object.ReferenceEquals(item, null))
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2312));
		}
		if (!AllowDuplicates && Contains(item))
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2703));
		}
		if (c53353887105e37579dd9feacc25a1884)
		{
			base.SetItem(index, item);
			return;
		}
		CollectionChangedEventArgs<T> e = CollectionChangedEventArgs<T>.Create();
		try
		{
			e.Action = CollectionChangedAction.Replace;
			e.NewItems.Add(item);
			e.NewItemsIndex = index;
			e.OldItems.Add(val);
			e.OldItemsIndex = index;
			base.SetItem(index, item);
			OnCollectionChanged(e);
		}
		finally
		{
			e.Recycle();
		}
	}

	/// <summary>
	///       Raises the <see cref="E:DigitalRune.Collections.NotifyingCollection`1.CollectionChanged" /> event.
	///       </summary>
	/// <param name="eventArgs">
	///   <see cref="T:DigitalRune.Collections.CollectionChangedEventArgs`1" /> object that provides the arguments for the
	///       event.
	///       </param>
	protected virtual void OnCollectionChanged(CollectionChangedEventArgs<T> eventArgs)
	{
		cda7ce06a2ebed16de8fc3925490e31ac?.Invoke(this, eventArgs);
	}

	/// <summary>
	///       Returns an enumerator that iterates through the <see cref="T:DigitalRune.Collections.NotifyingCollection`1" />. 
	///       </summary>
	/// <returns>
	///       An <see cref="T:System.Collections.Generic.List`1.Enumerator" /> for <see cref="T:DigitalRune.Collections.NotifyingCollection`1" />.
	///       </returns>
	public new List<T>.Enumerator GetEnumerator()
	{
		return ((List<T>)Items).GetEnumerator();
	}

	/// <summary>
	///       Adds the elements of the specified collection to the end of the 
	///       <see cref="T:DigitalRune.Collections.NotifyingCollection`1" />. 
	///       </summary>
	/// <param name="collection">
	///       The collection whose elements should be added to the end of the 
	///       <see cref="T:DigitalRune.Collections.NotifyingCollection`1" />. The collection itself cannot be 
	///       <see langword="null" />, but it can contain elements that are <see langword="null" />, if type 
	///       <typeparamref name="T" /> is a reference type and <see cref="P:DigitalRune.Collections.NotifyingCollection`1.AllowNull" /> is set to 
	///       <see langword="true" />. 
	///       </param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="collection" /> is <see langword="null" />.
	///       </exception>
	public void AddRange(IEnumerable<T> collection)
	{
		if (collection == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1764));
		}
		CollectionChangedEventArgs<T> e = CollectionChangedEventArgs<T>.Create();
		try
		{
			e.Action = CollectionChangedAction.Add;
			e.NewItemsIndex = Items.Count;
			c53353887105e37579dd9feacc25a1884 = true;
			foreach (T item in collection)
			{
				Add(item);
				e.NewItems.Add(item);
			}
			c53353887105e37579dd9feacc25a1884 = false;
			if (e.NewItems.Count > 0)
			{
				OnCollectionChanged(e);
			}
		}
		finally
		{
			c53353887105e37579dd9feacc25a1884 = false;
			e.Recycle();
		}
	}

	/// <summary>
	///       Inserts the elements of a collection into the <see cref="T:DigitalRune.Collections.NotifyingCollection`1" /> at the
	///       specified index.
	///       </summary>
	/// <param name="index">
	///       The zero-based index at which the new elements should be inserted.
	///       </param>
	/// <param name="collection">
	///       The collection whose elements should be inserted into the 
	///       <see cref="T:DigitalRune.Collections.NotifyingCollection`1" />. The collection itself cannot be 
	///       <see langword="null" />, but it can contain elements that are <see langword="null" />, if type 
	///       <typeparamref name="T" /> is a reference type and <see cref="P:DigitalRune.Collections.NotifyingCollection`1.AllowNull" /> is set to 
	///       <see langword="true" />.
	///       </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is less than 0 or greater than <see cref="P:System.Collections.ObjectModel.Collection`1.Count" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="collection" /> is <see langword="null" />.
	///       </exception>
	public void InsertRange(int index, IEnumerable<T> collection)
	{
		if (collection == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1764));
		}
		CollectionChangedEventArgs<T> e = CollectionChangedEventArgs<T>.Create();
		try
		{
			e.Action = CollectionChangedAction.Add;
			e.NewItemsIndex = index;
			c53353887105e37579dd9feacc25a1884 = true;
			foreach (T item in collection)
			{
				Insert(index, item);
				index++;
				e.NewItems.Add(item);
			}
			c53353887105e37579dd9feacc25a1884 = false;
			if (e.NewItems.Count > 0)
			{
				OnCollectionChanged(e);
			}
		}
		finally
		{
			c53353887105e37579dd9feacc25a1884 = false;
			e.Recycle();
		}
	}

	/// <summary>
	///       Removes a range of elements from the <see cref="T:DigitalRune.Collections.NotifyingCollection`1" />. 
	///       </summary>
	/// <param name="index">
	///       The zero-based starting index of the range of elements to remove.
	///       </param>
	/// <param name="count">
	///       The number of elements to remove. 
	///       </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is less than 0.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="count" /> is less than 0.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="index" /> and <paramref name="count" /> do not denote a valid range of 
	///       elements in the <see cref="T:DigitalRune.Collections.NotifyingCollection`1" />.
	///       </exception>
	public void RemoveRange(int index, int count)
	{
		if (index < 0)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2804), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2815));
		}
		if (count < 0)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2924), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2935));
		}
		if (index + count > Count)
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3073));
		}
		if (count == 0)
		{
			return;
		}
		CollectionChangedEventArgs<T> e = CollectionChangedEventArgs<T>.Create();
		try
		{
			e.Action = CollectionChangedAction.Remove;
			e.OldItemsIndex = index;
			c53353887105e37579dd9feacc25a1884 = true;
			for (int i = 0; i < count; i++)
			{
				e.OldItems.Add(Items[index]);
				Items.RemoveAt(index);
			}
			c53353887105e37579dd9feacc25a1884 = false;
			OnCollectionChanged(e);
		}
		finally
		{
			c53353887105e37579dd9feacc25a1884 = false;
			e.Recycle();
		}
	}

	/// <summary>
	///       Moves the item at the specified index to a new location in the collection. 
	///       </summary>
	/// <param name="oldIndex">
	///       The zero-based index specifying the location of the item to be moved. 
	///       </param>
	/// <param name="newIndex">
	///       The zero-based index specifying the new location of the item.
	///       </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="oldIndex" /> or <paramref name="newIndex" /> is out of range.
	///       </exception>
	public void Move(int oldIndex, int newIndex)
	{
		if (oldIndex < 0 || oldIndex >= Count)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1785));
		}
		if (newIndex < 0 || newIndex >= Count)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1785));
		}
		if (oldIndex != newIndex)
		{
			MoveItem(oldIndex, newIndex);
		}
	}

	/// <summary>
	///       Moves the item at the specified index to a new location in the collection.
	///       </summary>
	/// <param name="oldIndex">
	///       The zero-based index specifying the location of the item to be moved. 
	///       </param>
	/// <param name="newIndex">
	///       The zero-based index specifying the new location of the item.
	///       </param>
	protected virtual void MoveItem(int oldIndex, int newIndex)
	{
		CollectionChangedEventArgs<T> e = CollectionChangedEventArgs<T>.Create();
		try
		{
			T item = Items[oldIndex];
			Items.RemoveAt(oldIndex);
			Items.Insert(newIndex, item);
			e.Action = CollectionChangedAction.Move;
			e.NewItemsIndex = newIndex;
			e.OldItemsIndex = oldIndex;
			e.OldItems.Add(item);
			OnCollectionChanged(e);
		}
		finally
		{
			e.Recycle();
		}
	}
}
