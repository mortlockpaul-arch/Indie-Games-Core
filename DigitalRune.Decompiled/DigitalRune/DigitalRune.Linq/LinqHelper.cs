using System;
using System.Collections.Generic;
using A;

namespace DigitalRune.Linq;

/// <summary>
///       Provides new extension methods for LINQ.
///       </summary>
public static class LinqHelper
{
	/// <summary>
	///       Performs the given action on each element in a sequence.
	///       </summary>
	/// <param name="source">
	///       A sequence that contains the elements on which to perform <paramref name="action" />.
	///       </param>
	/// <param name="action">The action to execute on each element.</param>
	/// <typeparam name="T">The type of elements.</typeparam>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="action" /> is <see langword="null" />.
	///       </exception>
	public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
	{
		if (action == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3958));
		}
		if (source == null)
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
				__ldtoken(LinqHelper.ForEach<T>(IEnumerable<T>, Action<T>));
			}
			using IEnumerator<T> enumerator = source.GetEnumerator();
			while (enumerator.MoveNext())
			{
				T current = enumerator.Current;
				action(current);
			}
			while (true)
			{
				switch (7)
				{
				case 0:
					break;
				default:
					return;
				}
			}
		}
	}

	/// <summary>
	///       Returns the index of the first element in a sequence that satisfies the specified condition.
	///       </summary>
	/// <param name="source">A sequence of elements.</param>
	/// <param name="predicate">A predicate to test each element.</param>
	/// <typeparam name="T">The type of elements.</typeparam>
	/// <returns>
	///       The zero-based index of the first element in the sequence that passed test; -1 if no element
	///       passed the test.
	///       </returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="predicate" /> is <see langword="null" />.
	///       </exception>
	public static int IndexOf<T>(this IEnumerable<T> source, Predicate<T> predicate)
	{
		if (predicate == null)
		{
			while (true)
			{
				switch (4)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(LinqHelper.IndexOf<T>(IEnumerable<T>, Predicate<T>));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6831));
				}
			}
		}
		if (source != null)
		{
			int num = 0;
			foreach (T item in source)
			{
				if (predicate(item))
				{
					return num;
				}
				num++;
			}
		}
		return -1;
	}

	/// <summary>
	///       Returns an <see cref="T:System.Collections.Generic.IEnumerable`1" /> that returns a single element.
	///       </summary>
	/// <param name="value">The first and only element in the sequence.</param>
	/// <typeparam name="T">The type of the element.</typeparam>
	/// <returns>
	///       An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that returns a single element.
	///       </returns>
	public static IEnumerable<T> Sequence<T>(T value)
	{
		yield return value;
	}
}
