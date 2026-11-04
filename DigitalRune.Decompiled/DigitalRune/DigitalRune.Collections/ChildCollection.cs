using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using A;

namespace DigitalRune.Collections;

/// <summary>
///       Represents a collection of children.
///       </summary>
/// <typeparam name="TParent">The type of the parent object.</typeparam>
/// <typeparam name="TChild">The type of the child object.</typeparam>
public abstract class ChildCollection<TParent, TChild> : Collection<TChild> where TParent : class where TChild : class
{
	private readonly TParent cb27463d15b7dad14abc090ec73e9118b;

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Collections.ChildCollection`2" /> class.
	///       </summary>
	/// <param name="parent">The parent object that owns this collection.</param>
	protected ChildCollection(TParent parent)
	{
		cb27463d15b7dad14abc090ec73e9118b = parent;
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Collections.ChildCollection`2" /> class that
	///       has the specified initial capacity.
	///       </summary>
	/// <param name="parent">The parent object that owns this collection.</param>
	/// <param name="capacity">
	///       The number of elements that the new list can initially store.
	///       </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="capacity" /> is less than 0.
	///       </exception>
	protected ChildCollection(TParent parent, int capacity)
		: base((IList<TChild>)new List<TChild>(capacity))
	{
		cb27463d15b7dad14abc090ec73e9118b = parent;
	}

	/// <summary>
	///       Returns an enumerator that iterates through the <see cref="T:DigitalRune.Collections.ChildCollection`2" />. 
	///       </summary>
	/// <returns>
	///       An <see cref="T:System.Collections.Generic.List`1.Enumerator" /> for <see cref="T:DigitalRune.Collections.ChildCollection`2" />.
	///       </returns>
	public new List<TChild>.Enumerator GetEnumerator()
	{
		return ((List<TChild>)Items).GetEnumerator();
	}

	/// <summary>
	///       Removes all elements from the collection. 
	///       </summary>
	protected override void ClearItems()
	{
		foreach (TChild item in Items)
		{
			SetParent(item, null);
		}
		base.ClearItems();
	}

	/// <summary>
	///       Inserts an element into the collection at the specified index.
	///       </summary>
	/// <param name="index">
	///       The zero-based index at which <paramref name="child" /> should be inserted.
	///       </param>
	/// <param name="child">The object to insert.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="child" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.InvalidOperationException">
	///       Cannot insert node into collection - <paramref name="child" /> is already in this collection
	///       or child of another object.
	///       </exception>
	protected override void InsertItem(int index, TChild child)
	{
		if (child == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5913), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5924));
		}
		TParent parent = GetParent(child);
		if (parent != null)
		{
			if (parent == cb27463d15b7dad14abc090ec73e9118b)
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
							__ldtoken(ChildCollection<, >.InsertItem(int, TChild));
						}
						throw new InvalidOperationException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6021));
					}
				}
			}
			throw new InvalidOperationException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6193));
		}
		base.InsertItem(index, child);
		SetParent(child, cb27463d15b7dad14abc090ec73e9118b);
	}

	/// <summary>
	///       Removes the element at the specified index of the collection. 
	///       </summary>
	/// <param name="index">The zero-based index of the element to remove.</param>
	protected override void RemoveItem(int index)
	{
		TChild child = Items[index];
		base.RemoveItem(index);
		SetParent(child, null);
	}

	/// <summary>
	///       Replaces the element at the specified index.
	///       </summary>
	/// <param name="index">The zero-based index of the element to replace.</param>
	/// <param name="child">The new value for the element at the specified index.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="child" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.InvalidOperationException">
	///       Cannot insert node into collection - <paramref name="child" /> is already in this collection
	///       or child of another object.
	///       </exception>
	protected override void SetItem(int index, TChild child)
	{
		if (child == null)
		{
			while (true)
			{
				switch (5)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(ChildCollection<, >.SetItem(int, TChild));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5913), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5924));
				}
			}
		}
		TChild val = Items[index];
		if (object.ReferenceEquals(child, val))
		{
			return;
		}
		while (true)
		{
			switch (1)
			{
			case 0:
				continue;
			}
			TParent parent = GetParent(child);
			if (parent != null)
			{
				if (parent == cb27463d15b7dad14abc090ec73e9118b)
				{
					while (true)
					{
						switch (3)
						{
						case 0:
							break;
						default:
							throw new InvalidOperationException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6021));
						}
					}
				}
				throw new InvalidOperationException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6193));
			}
			base.SetItem(index, child);
			SetParent(val, null);
			SetParent(child, cb27463d15b7dad14abc090ec73e9118b);
			return;
		}
	}

	/// <summary>
	///       Gets the parent of an object.
	///       </summary>
	/// <param name="child">The child object.</param>
	/// <returns>The parent of <paramref name="child" />.</returns>
	protected abstract TParent GetParent(TChild child);

	/// <summary>
	///       Sets the parent of the given object.
	///       </summary>
	/// <param name="parent">The parent to set.</param>
	/// <param name="child">The child object.</param>
	protected abstract void SetParent(TChild child, TParent parent);

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
		if (newIndex >= 0)
		{
			if (newIndex < Count)
			{
				if (oldIndex != newIndex)
				{
					MoveItem(oldIndex, newIndex);
				}
				return;
			}
			while (true)
			{
				switch (7)
				{
				case 0:
					continue;
				}
				break;
			}
			if (1 == 0)
			{
				__ldtoken(ChildCollection<, >.Move(int, int));
			}
		}
		throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1785));
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
		TChild item = Items[oldIndex];
		Items.RemoveAt(oldIndex);
		Items.Insert(newIndex, item);
	}
}
