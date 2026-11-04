using System;
using System.Collections.Generic;
using A;
using DigitalRune.Collections;

namespace DigitalRune.Geometry.Collisions;

/// <summary>
///       Represents a collection of <see cref="T:DigitalRune.Geometry.Collisions.CollisionObject" />s.
///       </summary>
public class CollisionObjectCollection : NotifyingCollection<CollisionObject>
{
	private Dictionary<IGeometricObject, CollisionObject> c9d114a40d03a68e3c66b174103781938;

	private bool cb14bff04474b3915263eb6637c2bb520;

	/// <summary>
	///       Gets or sets a value indicating whether the internal lookup table is enabled.
	///       </summary>
	public bool EnableLookupTable
	{
		get
		{
			return cb14bff04474b3915263eb6637c2bb520;
		}
		set
		{
			if (cb14bff04474b3915263eb6637c2bb520 == value)
			{
				return;
			}
			cb14bff04474b3915263eb6637c2bb520 = value;
			if (cb14bff04474b3915263eb6637c2bb520)
			{
				c9d114a40d03a68e3c66b174103781938 = new Dictionary<IGeometricObject, CollisionObject>();
				using List<CollisionObject>.Enumerator enumerator = GetEnumerator();
				while (enumerator.MoveNext())
				{
					CollisionObject current = enumerator.Current;
					c9d114a40d03a68e3c66b174103781938.Add(current.GeometricObject, current);
				}
				return;
			}
			c9d114a40d03a68e3c66b174103781938 = null;
		}
	}

	internal CollisionObjectCollection()
		: base(false, true)
	{
	}

	/// <summary>
	///       Removes all elements from the <see cref="T:System.Collections.ObjectModel.Collection`1" />.
	///       </summary>
	protected override void ClearItems()
	{
		base.ClearItems();
		if (cb14bff04474b3915263eb6637c2bb520)
		{
			c9d114a40d03a68e3c66b174103781938.Clear();
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
	protected override void InsertItem(int index, CollisionObject item)
	{
		if (item != null && item.Domain != null)
		{
			throw new InvalidOperationException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(16331));
		}
		base.InsertItem(index, item);
		if (cb14bff04474b3915263eb6637c2bb520)
		{
			c9d114a40d03a68e3c66b174103781938[item.GeometricObject] = item;
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
		if (cb14bff04474b3915263eb6637c2bb520)
		{
			CollisionObject collisionObject = base[index];
			c9d114a40d03a68e3c66b174103781938.Remove(collisionObject.GeometricObject);
		}
		base.RemoveItem(index);
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
	protected override void SetItem(int index, CollisionObject item)
	{
		if (item != null && item.Domain != null)
		{
			throw new InvalidOperationException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(16331));
		}
		CollisionObject collisionObject = base[index];
		base.SetItem(index, item);
		if (cb14bff04474b3915263eb6637c2bb520)
		{
			c9d114a40d03a68e3c66b174103781938[item.GeometricObject] = item;
			c9d114a40d03a68e3c66b174103781938.Remove(collisionObject.GeometricObject);
		}
	}

	/// <summary>
	///       Gets the collision object for the specified geometric object.
	///       </summary>
	/// <param name="geometricObject">The <see cref="T:DigitalRune.Geometry.IGeometricObject" />.</param>
	/// <returns>
	///       The <see cref="T:DigitalRune.Geometry.Collisions.CollisionObject" /> of <paramref name="geometricObject" />, or 
	///       <see langword="null" /> if the collision domain does not contain a 
	///       <see cref="T:DigitalRune.Geometry.Collisions.CollisionObject" /> for <paramref name="geometricObject" />.
	///       </returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="geometricObject" /> is <see langword="true" />.
	///       </exception>
	public CollisionObject Get(IGeometricObject geometricObject)
	{
		if (geometricObject == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(16509));
		}
		if (cb14bff04474b3915263eb6637c2bb520)
		{
			c9d114a40d03a68e3c66b174103781938.TryGetValue(geometricObject, out var value);
			return value;
		}
		using (List<CollisionObject>.Enumerator enumerator = GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				CollisionObject current = enumerator.Current;
				if (current.GeometricObject == geometricObject)
				{
					return current;
				}
			}
		}
		return null;
	}
}
