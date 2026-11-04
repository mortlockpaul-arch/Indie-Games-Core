using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using A;

namespace DigitalRune.Collections;

/// <summary>
///       Stores objects by their names.
///       </summary>
/// <typeparam name="T">
///       The type of the objects. Must be derived from <see cref="T:DigitalRune.INamedObject" />.
///       </typeparam>
public class NamedObjectCollection<T> : KeyedCollection<string, T> where T : INamedObject
{
	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Collections.NamedObjectCollection`1" /> class.
	///       </summary>
	public NamedObjectCollection()
		: base((IEqualityComparer<string>)StringComparer.Ordinal)
	{
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Collections.NamedObjectCollection`1" /> class using the
	///       given comparer.
	///       </summary>
	/// <param name="comparer">The comparer that compares whether two names are equal.</param>
	public NamedObjectCollection(IEqualityComparer<string> comparer)
		: base(comparer)
	{
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Collections.NamedObjectCollection`1" /> class that uses the 
	///       specified equality comparer and creates a lookup dictionary when the specified threshold is 
	///       exceeded. 
	///       </summary>
	/// <param name="comparer">The comparer that compares whether two names are equal.</param>
	/// <param name="dictionaryCreationThreshold">
	///       The number of elements the collection can hold without creating a lookup dictionary 
	///       (0 creates the lookup dictionary when the first item is added), or –1 to specify that a 
	///       lookup dictionary is never created. 
	///       </param>
	public NamedObjectCollection(IEqualityComparer<string> comparer, int dictionaryCreationThreshold)
		: base(comparer, dictionaryCreationThreshold)
	{
	}

	/// <summary>
	///       Returns an enumerator that iterates through the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" />. 
	///       </summary>
	/// <returns>
	///       An <see cref="T:System.Collections.Generic.List`1.Enumerator" /> for <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" />.
	///       </returns>
	public new List<T>.Enumerator GetEnumerator()
	{
		return ((List<T>)Items).GetEnumerator();
	}

	/// <summary>
	///       Adds the elements of the specified collection to the end of the 
	///       <see cref="T:DigitalRune.Collections.NamedObjectCollection`1" />. 
	///       </summary>
	/// <param name="collection">
	///       The collection whose elements should be added to the end of the 
	///       <see cref="T:DigitalRune.Collections.NamedObjectCollection`1" /></param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="collection" /> is <see langword="null" />.
	///       </exception>
	public void AddRange(IEnumerable<T> collection)
	{
		if (collection == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1764));
		}
		foreach (T item in collection)
		{
			Add(item);
		}
	}

	/// <summary>
	///       When implemented in a derived class, extracts the key from the specified element.
	///       </summary>
	/// <param name="item">The element from which to extract the key.</param>
	/// <returns>The key for the specified element.</returns>
	protected override string GetKeyForItem(T item)
	{
		return item.Name;
	}

	/// <summary>
	///       Gets the object associated with the specified key.
	///       </summary>
	/// <param name="key">The name of the element to get.</param>
	/// <param name="value">
	///       When this method returns, the object associated with the specified key, if 
	///       the key is found; otherwise, the default value for the type of the value 
	///       parameter. (This parameter is passed uninitialized.)
	///       </param>
	/// <returns>
	///   <see langword="true" /> if the <see cref="T:DigitalRune.Collections.NamedObjectCollection`1" /> contains 
	///       an element with the specified key; otherwise, <see langword="false" />.
	///       </returns>
	public bool TryGet(string key, out T value)
	{
		if (Dictionary == null)
		{
			using (List<T>.Enumerator enumerator = GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					T current = enumerator.Current;
					if (Comparer.Equals(current.Name, key))
					{
						value = current;
						return true;
					}
				}
			}
			value = default;
			return false;
		}
		return Dictionary.TryGetValue(key, out value);
	}

	/// <summary>
	///       Gets the object associated with the specified key. The object needs to be of a
	///       certain type.
	///       </summary>
	/// <param name="key">The name of the element to get.</param>
	/// <param name="value">
	///       The object associated with the specified key, if the key is found and the 
	///       object is of type <typeparamref name="TExpected" />; otherwise, the 
	///       default value for <typeparamref name="TExpected" /> is returned. (This 
	///       parameter is passed uninitialized.)
	///       </param>
	/// <typeparam name="TExpected">The expected type of the object.</typeparam>
	/// <returns>
	///   <see langword="true" /> if the <see cref="T:DigitalRune.Collections.NamedObjectCollection`1" /> contains an element 
	///       with the specified key which is of the expected type; otherwise, <see langword="false" />.
	///       </returns>
	public bool TryGet<TExpected>(string key, out TExpected value) where TExpected : T
	{
		value = default;
		if (Dictionary != null)
		{
			if (Dictionary.TryGetValue(key, out var value2) && value2 is TExpected)
			{
				value = (TExpected)(object)value2;
				return true;
			}
			return false;
		}
		using (List<T>.Enumerator enumerator = GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				T current = enumerator.Current;
				if (Comparer.Equals(current.Name, key))
				{
					if (current is TExpected)
					{
						value = (TExpected)(object)current;
						return true;
					}
					return false;
				}
			}
		}
		return false;
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
		T item = Items[oldIndex];
		Items.RemoveAt(oldIndex);
		Items.Insert(newIndex, item);
	}
}
