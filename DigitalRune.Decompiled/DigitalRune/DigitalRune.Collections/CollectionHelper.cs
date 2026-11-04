using System;
using System.Collections.Generic;
using A;

namespace DigitalRune.Collections;

/// <summary>
///       Provides extension methods for working with collections.
///       </summary>
public static class CollectionHelper
{
	/// <summary>
	///        Adds the specified items to the <see cref="T:System.Collections.Generic.ICollection`1" />. 
	///        </summary>
	/// <param name="collection">The collection to which the items should be added.</param>
	/// <param name="items">TThe items to be added.</param>
	/// <typeparam name="T">The type of items in the collection.</typeparam>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="collection" /> is <see langword="null" />.
	///        </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="items" /> is <see langword="null" />.
	///        </exception>
	public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
	{
		if (collection == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1764));
		}
		if (items == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5333));
		}
		IEnumerator<T> enumerator = items.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				T current = enumerator.Current;
				collection.Add(current);
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
					__ldtoken(CollectionHelper.AddRange<T>(ICollection<T>, IEnumerable<T>));
				}
				return;
			}
		}
		finally
		{
			if (enumerator != null)
			{
				while (true)
				{
					switch (1)
					{
					case 0:
						continue;
					}
					enumerator.Dispose();
					break;
				}
			}
		}
	}
}
