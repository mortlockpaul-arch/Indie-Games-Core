using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using A;
using DigitalRune.Collections;

namespace DigitalRune.Geometry.Collisions;

/// <summary>
///       A collection of <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" />s.
///       </summary>
public class ContactSetCollection : IEnumerable<ContactSet>, IEnumerable, ICollection<ContactSet>
{
	[CompilerGenerated]
	private sealed class c8f9f85b2c9f18090d584a9f6deeac458 : IEnumerable<ContactSet>, IEnumerable, IEnumerator<ContactSet>, IEnumerator, IDisposable
	{
		private ContactSet cd179d7f18da070d164af6afcb0af45b5;

		private int c94f69126adc708dfc5f5fd5b63c52835;

		private int c0e8a058e779916e7715d7616fb0da9e7;

		public ContactSetCollection c1b0bbfd4af1b6a4c0c71a5dd3c509812;

		public CollisionObject c9bdbd655e343282211edce8c6b867eab;

		public CollisionObject c9932429c8066f0d42d6974e6af4e7c30;

		public ContactSet c88932e1efcae73c62cf59e238c17f5b9;

		public Dictionary<Pair<CollisionObject>, ContactSet>.ValueCollection.Enumerator c81bc24b01874c03464f508899a23b019;

		ContactSet IEnumerator<ContactSet>.Current
		{
			[DebuggerHidden]
			get
			{
				return cd179d7f18da070d164af6afcb0af45b5;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return cd179d7f18da070d164af6afcb0af45b5;
			}
		}

		[DebuggerHidden]
		public c8f9f85b2c9f18090d584a9f6deeac458(int c94f69126adc708dfc5f5fd5b63c52835)
		{
			this.c94f69126adc708dfc5f5fd5b63c52835 = c94f69126adc708dfc5f5fd5b63c52835;
			c0e8a058e779916e7715d7616fb0da9e7 = Thread.CurrentThread.ManagedThreadId;
		}

		[DebuggerHidden]
		private IEnumerator<ContactSet> ce20363940a22b917704bd730db1c37e8()
		{
			c8f9f85b2c9f18090d584a9f6deeac458 c8f9f85b2c9f18090d584a9f6deeac459;
			if (Thread.CurrentThread.ManagedThreadId == c0e8a058e779916e7715d7616fb0da9e7)
			{
				while (true)
				{
					switch (3)
					{
					case 0:
						continue;
					}
					break;
				}
				if (1 == 0)
				{
					__ldtoken(c8f9f85b2c9f18090d584a9f6deeac458.ce20363940a22b917704bd730db1c37e8());
				}
				if (c94f69126adc708dfc5f5fd5b63c52835 == -2)
				{
					c94f69126adc708dfc5f5fd5b63c52835 = 0;
					c8f9f85b2c9f18090d584a9f6deeac459 = this;
					goto IL_004f;
				}
			}
			c8f9f85b2c9f18090d584a9f6deeac459 = new c8f9f85b2c9f18090d584a9f6deeac458(0)
			{
				c1b0bbfd4af1b6a4c0c71a5dd3c509812 = c1b0bbfd4af1b6a4c0c71a5dd3c509812
			};
			goto IL_004f;
			IL_004f:
			c8f9f85b2c9f18090d584a9f6deeac459.c9bdbd655e343282211edce8c6b867eab = c9932429c8066f0d42d6974e6af4e7c30;
			return c8f9f85b2c9f18090d584a9f6deeac459;
		}

		IEnumerator<ContactSet> IEnumerable<ContactSet>.GetEnumerator()
		{
			//ILSpy generated this explicit interface implementation from .override directive in ce20363940a22b917704bd730db1c37e8
			return this.ce20363940a22b917704bd730db1c37e8();
		}

		[DebuggerHidden]
		private IEnumerator c63811cf6ff5c0b86c54fd5037c6af6ad()
		{
			return ce20363940a22b917704bd730db1c37e8();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			//ILSpy generated this explicit interface implementation from .override directive in c63811cf6ff5c0b86c54fd5037c6af6ad
			return this.c63811cf6ff5c0b86c54fd5037c6af6ad();
		}

		private bool MoveNext()
		{
			try
			{
				switch (c94f69126adc708dfc5f5fd5b63c52835)
				{
				case 0:
					c94f69126adc708dfc5f5fd5b63c52835 = -1;
					c81bc24b01874c03464f508899a23b019 = c1b0bbfd4af1b6a4c0c71a5dd3c509812.c48a7f6cf912d8b6cada5392ccc2f9c56.Values.GetEnumerator();
					c94f69126adc708dfc5f5fd5b63c52835 = 1;
					goto IL_00b7;
				case 2:
					{
						c94f69126adc708dfc5f5fd5b63c52835 = 1;
						goto IL_00b7;
					}
					IL_00b7:
					while (c81bc24b01874c03464f508899a23b019.MoveNext())
					{
						c88932e1efcae73c62cf59e238c17f5b9 = c81bc24b01874c03464f508899a23b019.Current;
						if (c88932e1efcae73c62cf59e238c17f5b9.ObjectA != c9bdbd655e343282211edce8c6b867eab)
						{
							while (true)
							{
								switch (4)
								{
								case 0:
									continue;
								}
								if (1 == 0)
								{
									__ldtoken(c8f9f85b2c9f18090d584a9f6deeac458.MoveNext());
								}
								if (c88932e1efcae73c62cf59e238c17f5b9.ObjectB != c9bdbd655e343282211edce8c6b867eab)
								{
									break;
								}
								goto IL_0099;
							}
							continue;
						}
						goto IL_0099;
						IL_0099:
						cd179d7f18da070d164af6afcb0af45b5 = c88932e1efcae73c62cf59e238c17f5b9;
						c94f69126adc708dfc5f5fd5b63c52835 = 2;
						return true;
					}
					c6b3af2365d3902564afbc42014f1c625();
					break;
				}
				return false;
			}
			catch
			{
				//try-fault
				c087ddb9300ad4d53672b7a60e6d76672();
				throw;
			}
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		private void c9106c2889b15ecfa28d3af82494ec7bc()
		{
			throw new NotSupportedException();
		}

		void IEnumerator.Reset()
		{
			//ILSpy generated this explicit interface implementation from .override directive in c9106c2889b15ecfa28d3af82494ec7bc
			this.c9106c2889b15ecfa28d3af82494ec7bc();
		}

		private void c087ddb9300ad4d53672b7a60e6d76672()
		{
			switch (c94f69126adc708dfc5f5fd5b63c52835)
			{
			case 1:
			case 2:
				try
				{
					break;
				}
				finally
				{
					c6b3af2365d3902564afbc42014f1c625();
				}
			}
		}

		void IDisposable.Dispose()
		{
			//ILSpy generated this explicit interface implementation from .override directive in c087ddb9300ad4d53672b7a60e6d76672
			this.c087ddb9300ad4d53672b7a60e6d76672();
		}

		private void c6b3af2365d3902564afbc42014f1c625()
		{
			c94f69126adc708dfc5f5fd5b63c52835 = -1;
			((IDisposable)c81bc24b01874c03464f508899a23b019/*cast due to constrained. prefix*/).Dispose();
		}
	}

	private readonly Dictionary<Pair<CollisionObject>, ContactSet> c48a7f6cf912d8b6cada5392ccc2f9c56 = new Dictionary<Pair<CollisionObject>, ContactSet>();

	/// <summary>
	///       Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.
	///       </summary>
	public int Count => c48a7f6cf912d8b6cada5392ccc2f9c56.Count;

	/// <summary>
	///       Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.
	///       </summary>
	public bool IsReadOnly => false;

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Collisions.ContactSetCollection" /> class.
	///       </summary>
	public ContactSetCollection()
	{
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Collisions.ContactSetCollection" /> class with the given
	///       contact sets.
	///       </summary>
	/// <param name="contactSets">
	///       The contact sets which are initially added to the collection.
	///       </param>
	public ContactSetCollection(IEnumerable<ContactSet> contactSets)
	{
		if (contactSets == null)
		{
			return;
		}
		foreach (ContactSet contactSet in contactSets)
		{
			c48a7f6cf912d8b6cada5392ccc2f9c56.Add(new Pair<CollisionObject>(contactSet.ObjectA, contactSet.ObjectB), contactSet);
		}
	}

	/// <summary>
	///       Adds the specified contact set.
	///       </summary>
	/// <param name="contactSet">The contact set.</param>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="contactSet" /> is already contained in the collection.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="contactSet" /> is <see langword="null" />.
	///       </exception>
	public void Add(ContactSet contactSet)
	{
		if (contactSet == null)
		{
			throw new ArgumentNullException();
		}
		c48a7f6cf912d8b6cada5392ccc2f9c56.Add(new Pair<CollisionObject>(contactSet.ObjectA, contactSet.ObjectB), contactSet);
	}

	internal void c03d72a32723924cb5b81c585d5df7d9d(Pair<CollisionObject> c3cdd6711e3e96d722a86b2bfd486c923, ContactSet cfc73391dcf53c37de15d54e2434d0199)
	{
		c48a7f6cf912d8b6cada5392ccc2f9c56.Add(c3cdd6711e3e96d722a86b2bfd486c923, cfc73391dcf53c37de15d54e2434d0199);
	}

	/// <summary>
	///       Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1" />.
	///       </summary>
	public void Clear()
	{
		c48a7f6cf912d8b6cada5392ccc2f9c56.Clear();
	}

	/// <summary>
	///       Determines whether the collection contains a contact set for the given pair of 
	///       <see cref="T:DigitalRune.Geometry.Collisions.CollisionObject" />s.
	///       </summary>
	/// <param name="objectA">The first collision object.</param>
	/// <param name="objectB">The second collision object.</param>
	/// <returns>
	///   <see langword="true" /> if the collection contains a contact set with contacts between 
	///       <paramref name="objectA" /> and <paramref name="objectB" />; otherwise, 
	///       <see langword="false" />.
	///       </returns>
	public bool Contains(CollisionObject objectA, CollisionObject objectB)
	{
		if (objectA == null || objectB == null)
		{
			return false;
		}
		return c48a7f6cf912d8b6cada5392ccc2f9c56.ContainsKey(new Pair<CollisionObject>(objectA, objectB));
	}

	/// <summary>
	///       Determines whether the collection contains a contact set for the given pair of
	///       <see cref="T:DigitalRune.Geometry.Collisions.CollisionObject" />s.
	///       </summary>
	/// <param name="collisionObjectPair">The collision object pair.</param>
	/// <returns>
	///   <see langword="true" /> if the collection contains a contact set with contacts between
	///       the given pair of objects; otherwise, <see langword="false" />.
	///       </returns>
	public bool Contains(Pair<CollisionObject> collisionObjectPair)
	{
		return c48a7f6cf912d8b6cada5392ccc2f9c56.ContainsKey(collisionObjectPair);
	}

	/// <summary>
	///       Gets the contact set with the contacts between two <see cref="T:DigitalRune.Geometry.Collisions.CollisionObject" />s.
	///       </summary>
	/// <param name="objectA">The first collision object.</param>
	/// <param name="objectB">The second collision object.</param>
	/// <returns>
	///   <para>
	///       A <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" /> with contacts between <paramref name="objectA" /> and 
	///       <paramref name="objectB" />. The collision objects in the returned <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" />
	///       can be swapped! See <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" /> for more information on <i>swapped contact 
	///       sets</i>.
	///       </para>
	///   <para>
	///       If the collection does not contain a suitable <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" />, 
	///       <see langword="null" /> is returned. 
	///       </para>
	/// </returns>
	public ContactSet GetContacts(CollisionObject objectA, CollisionObject objectB)
	{
		if (objectA == null || objectB == null)
		{
			return null;
		}
		Pair<CollisionObject> key = new Pair<CollisionObject>(objectA, objectB);
		c48a7f6cf912d8b6cada5392ccc2f9c56.TryGetValue(key, out var value);
		return value;
	}

	/// <summary>
	///       Gets the contact set with the contacts between two <see cref="T:DigitalRune.Geometry.Collisions.CollisionObject" />s.
	///       </summary>
	/// <param name="collisionObjectPair">The collision object pair.</param>
	/// <returns>
	///   <para>
	///       A <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" /> with contacts between the given pair of objects. 
	///       The collision objects in the returned <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" />
	///       can be swapped! See <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" /> for more information on <i>swapped contact
	///       sets</i>.
	///       </para>
	///   <para>
	///       If the collection does not contain a suitable <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" />,
	///       <see langword="null" /> is returned.
	///       </para>
	/// </returns>
	public ContactSet GetContacts(Pair<CollisionObject> collisionObjectPair)
	{
		c48a7f6cf912d8b6cada5392ccc2f9c56.TryGetValue(collisionObjectPair, out var value);
		return value;
	}

	/// <summary>
	///       Gets the <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" />s for a given <see cref="T:DigitalRune.Geometry.Collisions.CollisionObject" />.
	///       </summary>
	/// <param name="collisionObject">The collision object.</param>
	/// <returns>
	///       All <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" />s where <paramref name="collisionObject" /> is involved.
	///       </returns>
	public IEnumerable<ContactSet> GetContacts(CollisionObject collisionObject)
	{
		foreach (ContactSet value in c48a7f6cf912d8b6cada5392ccc2f9c56.Values)
		{
			if (value.ObjectA != collisionObject)
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
					__ldtoken(c8f9f85b2c9f18090d584a9f6deeac458.MoveNext());
				}
				if (value.ObjectB != collisionObject)
				{
					continue;
				}
			}
			yield return value;
		}
	}

	/// <summary>
	///       Removes the <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" />s for the given collision object.
	///       </summary>
	/// <param name="collisionObject">The collision object.</param>
	/// <returns>
	///   <see langword="true" /> if an item was successfully removed; otherwise, 
	///       <see langword="false" />. This method also returns <see langword="false" /> if item is not 
	///       found.
	///       </returns>
	public bool Remove(CollisionObject collisionObject)
	{
		List<Pair<CollisionObject>> list = ResourcePools<Pair<CollisionObject>>.Lists.Obtain();
		foreach (KeyValuePair<Pair<CollisionObject>, ContactSet> item in c48a7f6cf912d8b6cada5392ccc2f9c56)
		{
			if (item.Value.ObjectA == collisionObject || item.Value.ObjectB == collisionObject)
			{
				list.Add(item.Key);
			}
		}
		foreach (Pair<CollisionObject> item2 in list)
		{
			c48a7f6cf912d8b6cada5392ccc2f9c56.Remove(item2);
		}
		bool result = list.Count > 0;
		ResourcePools<Pair<CollisionObject>>.Lists.Recycle(list);
		return result;
	}

	/// <summary>
	///       Removes the <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" /> for the given pair of <see cref="T:DigitalRune.Geometry.Collisions.CollisionObject" />s.
	///       </summary>
	/// <param name="objectA">The first collision object.</param>
	/// <param name="objectB">The second collision object.</param>
	/// <returns>
	///   <see langword="true" /> if item was successfully removed; otherwise, <see langword="false" />. 
	///       This method also returns <see langword="false" /> if item is not found.
	///       </returns>
	public bool Remove(CollisionObject objectA, CollisionObject objectB)
	{
		if (objectA == null || objectB == null)
		{
			return false;
		}
		return c48a7f6cf912d8b6cada5392ccc2f9c56.Remove(new Pair<CollisionObject>(objectA, objectB));
	}

	/// <summary>
	///       Removes the <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" /> for the given pair of <see cref="T:DigitalRune.Geometry.Collisions.CollisionObject" />s.
	///       </summary>
	/// <param name="collisionObjectPair">The collision object pair.</param>
	/// <returns>
	///   <see langword="true" /> if item was successfully removed; otherwise, <see langword="false" />.
	///       This method also returns <see langword="false" /> if item is not found.
	///       </returns>
	public bool Remove(Pair<CollisionObject> collisionObjectPair)
	{
		return c48a7f6cf912d8b6cada5392ccc2f9c56.Remove(collisionObjectPair);
	}

	/// <summary>
	///       Copies the contact sets of the collection to a new array.
	///       </summary>
	/// <returns>
	///       An array containing the contact sets.
	///       </returns>
	public ContactSet[] ToArray()
	{
		ContactSet[] array = new ContactSet[c48a7f6cf912d8b6cada5392ccc2f9c56.Count];
		c48a7f6cf912d8b6cada5392ccc2f9c56.Values.CopyTo(array, 0);
		return array;
	}

	/// <summary>
	///       Gets the <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" /> for the given pair of <see cref="T:DigitalRune.Geometry.Collisions.CollisionObject" />s.
	///       </summary>
	/// <param name="objectA">The first collision object.</param>
	/// <param name="objectB">The second collision object.</param>
	/// <param name="contactSet">
	///       When this method returns, contains the contact set with contacts between 
	///       <paramref name="objectA" /> and <paramref name="objectB" />, if such a contact set exists
	///       in the collection - otherwise, <see langword="null" />.
	///       </param>
	/// <returns>
	///   <see langword="true" /> if the <see cref="T:DigitalRune.Geometry.Collisions.ContactSetCollection" /> contains a <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" />
	///       for the specified pair of <see cref="T:DigitalRune.Geometry.Collisions.CollisionObject" />s.
	///       </returns>
	public bool TryGet(CollisionObject objectA, CollisionObject objectB, out ContactSet contactSet)
	{
		if (objectA == null || objectB == null)
		{
			contactSet = null;
			return false;
		}
		return c48a7f6cf912d8b6cada5392ccc2f9c56.TryGetValue(new Pair<CollisionObject>(objectA, objectB), out contactSet);
	}

	/// <summary>
	///       Gets the <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" /> for the given pair of <see cref="T:DigitalRune.Geometry.Collisions.CollisionObject" />s.
	///       </summary>
	/// <param name="collisionObjectPair">The collision object pair.</param>
	/// <param name="contactSet">
	///       When this method returns, contains the contact set with contacts between the given 
	///       collision object pair, if such a contact set exists
	///       in the collection - otherwise, <see langword="null" />.
	///       </param>
	/// <returns>
	///   <see langword="true" /> if the <see cref="T:DigitalRune.Geometry.Collisions.ContactSetCollection" /> contains a <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" />
	///       for the specified pair of <see cref="T:DigitalRune.Geometry.Collisions.CollisionObject" />s.
	///       </returns>
	public bool TryGet(Pair<CollisionObject> collisionObjectPair, out ContactSet contactSet)
	{
		return c48a7f6cf912d8b6cada5392ccc2f9c56.TryGetValue(collisionObjectPair, out contactSet);
	}

	/// <summary>
	///       Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
	///       </summary>
	/// <returns>
	///       A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
	///       </returns>
	public override string ToString()
	{
		return string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(15714), c48a7f6cf912d8b6cada5392ccc2f9c56.Count);
	}

	/// <summary>
	///       Determines whether the collection contains a specific <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" />.
	///       </summary>
	/// <param name="item">The <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" /> to locate in the collection.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="item" /> is found; otherwise, 
	///       <see langword="false" />.
	///       </returns>
	public bool Contains(ContactSet item)
	{
		if (item == null)
		{
			return false;
		}
		if (TryGet(item.ObjectA, item.ObjectB, out var contactSet))
		{
			return item == contactSet;
		}
		return false;
	}

	/// <summary>
	///       Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1" /> to an <see cref="T:System.Array" />, starting 
	///       at a particular <see cref="T:System.Array" /> index.
	///       </summary>
	/// <param name="array">
	///       The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from 
	///       <see cref="T:System.Collections.Generic.ICollection`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.
	///       </param>
	/// <param name="arrayIndex">
	///       The zero-based index in <paramref name="array" /> at which copying begins.
	///       </param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="array" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="arrayIndex" /> is less than 0.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="array" /> is multidimensional. Or <paramref name="arrayIndex" /> is equal to 
	///       or greater than the length of <paramref name="array" />. Or the number of elements in the 
	///       source <see cref="T:System.Collections.Generic.ICollection`1" /> is greater than the available space from 
	///       <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />.
	///       </exception>
	public void CopyTo(ContactSet[] array, int arrayIndex)
	{
		c48a7f6cf912d8b6cada5392ccc2f9c56.Values.CopyTo(array, arrayIndex);
	}

	/// <summary>
	///       Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.
	///       </summary>
	/// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
	/// <returns>
	///   <see langword="true" /> if item was successfully removed from the 
	///       <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, <see langword="false" />. This method also returns 
	///       <see langword="false" /> if item is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.
	///       </returns>
	public bool Remove(ContactSet item)
	{
		if (item == null)
		{
			return false;
		}
		return c48a7f6cf912d8b6cada5392ccc2f9c56.Remove(new Pair<CollisionObject>(item.ObjectA, item.ObjectB));
	}

	/// <summary>
	///       Returns an enumerator that iterates through the collection.
	///       </summary>
	/// <returns>
	///       A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
	///       </returns>
	public Dictionary<Pair<CollisionObject>, ContactSet>.ValueCollection.Enumerator GetEnumerator()
	{
		return c48a7f6cf912d8b6cada5392ccc2f9c56.Values.GetEnumerator();
	}

	private IEnumerator<ContactSet> ce20363940a22b917704bd730db1c37e8()
	{
		return c48a7f6cf912d8b6cada5392ccc2f9c56.Values.GetEnumerator();
	}

	IEnumerator<ContactSet> IEnumerable<ContactSet>.GetEnumerator()
	{
		//ILSpy generated this explicit interface implementation from .override directive in ce20363940a22b917704bd730db1c37e8
		return this.ce20363940a22b917704bd730db1c37e8();
	}

	private IEnumerator c63811cf6ff5c0b86c54fd5037c6af6ad()
	{
		return c48a7f6cf912d8b6cada5392ccc2f9c56.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		//ILSpy generated this explicit interface implementation from .override directive in c63811cf6ff5c0b86c54fd5037c6af6ad
		return this.c63811cf6ff5c0b86c54fd5037c6af6ad();
	}
}
