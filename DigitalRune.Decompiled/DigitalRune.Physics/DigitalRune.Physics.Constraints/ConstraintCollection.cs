using System;
using A;
using DigitalRune.Collections;

namespace DigitalRune.Physics.Constraints;

/// <summary>
///       Stores the <see cref="T:DigitalRune.Physics.Constraints.Constraint" /> objects of a simulation.
///       </summary>
public class ConstraintCollection : NotifyingCollection<Constraint>
{
	internal ConstraintCollection()
		: base(false, true)
	{
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
	protected override void InsertItem(int index, Constraint item)
	{
		if (item != null)
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
				__ldtoken(ConstraintCollection.InsertItem(int, Constraint));
			}
			if (item.Simulation != null)
			{
				throw new InvalidOperationException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5759));
			}
		}
		base.InsertItem(index, item);
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
	protected override void SetItem(int index, Constraint item)
	{
		if (item != null && item.Simulation != null)
		{
			throw new InvalidOperationException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5759));
		}
		base.SetItem(index, item);
	}
}
