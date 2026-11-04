using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using A;
using DigitalRune.Geometry.Collisions.Algorithms;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Collisions;

/// <summary>
///       A collection of <see cref="T:DigitalRune.Geometry.Collisions.Contact" />s that describe the contact points or closest points
///       between to collision objects.
///       </summary>
public class ContactSet : ICollection<Contact>, IEnumerable<Contact>, ICollection, IEnumerable, IList<Contact>
{
	private static readonly c107cf2db36674a478eaf84e50d31337e c1b97f8ade7576cb1dbaf95642e216415 = c107cf2db36674a478eaf84e50d31337e.c2d8ce8b1ccf2b905e3b18f8a5145fe8a();

	private static readonly CollisionObject cd6e38a4032e49762f9dfd932b33937cc = new CollisionObject(c1b97f8ade7576cb1dbaf95642e216415);

	private static readonly CollisionObject c157ecb28e011e2ac5c9b038e1f15f2b7 = new CollisionObject(c1b97f8ade7576cb1dbaf95642e216415);

	private static readonly ResourcePool<ContactSet> c8aca4a0e3c0073ffb188fde832680587 = new ResourcePool<ContactSet>(() => new ContactSet(), null, null);

	private List<Contact> c9d16faceeb0058b07b0e5f0c16bb5d30;

	private Vector3F c0ffdbf65ac5ec2b92af8780bdfd85d3a;

	internal int c55dbd29b4a90a73bb93fd94db8e41505;

	internal CollisionAlgorithm cc5b0109eff96f91c3e8ea71cac49ebb7;

	internal bool cff390782086d4775a28c5af1a8ec7667;

	[CompilerGenerated]
	private bool ce9ade838242f7ae1d3b19a66da86b1c1;

	[CompilerGenerated]
	private CollisionObject c0011e0b67ea915e2c324fb0c692753a3;

	[CompilerGenerated]
	private CollisionObject c757f286cc55d16436490cf3fc3d8f8da;

	[CompilerGenerated]
	private bool cfec7c798a134bce22733508865c6ef8a;

	[CompilerGenerated]
	private bool cf32dd2a8c0433f0aaba17a4455620434;

	[CompilerGenerated]
	private static Func<ContactSet> c49253b4520c1b7e183dfcc9c426c0283;

	/// <summary>
	///       Gets the number of <see cref="T:DigitalRune.Geometry.Collisions.Contact" />s contained in the <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" />.
	///       </summary>
	public int Count
	{
		get
		{
			if (c9d16faceeb0058b07b0e5f0c16bb5d30 != null)
			{
				return c9d16faceeb0058b07b0e5f0c16bb5d30.Count;
			}
			return 0;
		}
	}

	/// <summary>
	///       Gets or sets the <see cref="T:DigitalRune.Geometry.Collisions.Contact" /> at the specified index.
	///       </summary>
	/// <param name="index">The zero-based index of the contact to get or set.</param>
	/// <exception cref="T:System.NullReferenceException">
	///       This <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" /> is empty.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is less than 0 or equal to or greater than <see cref="P:DigitalRune.Geometry.Collisions.ContactSet.Count" />.
	///       </exception>
	public Contact this[int index]
	{
		get
		{
			return c9d16faceeb0058b07b0e5f0c16bb5d30[index];
		}
		set
		{
			c9d16faceeb0058b07b0e5f0c16bb5d30[index] = value;
		}
	}

	private object c9be56348c75e8eb93f4195f512c32f99
	{
		get
		{
			c08c869dd9b7be760010c5602ad867605();
			return ((ICollection)c9d16faceeb0058b07b0e5f0c16bb5d30).SyncRoot;
		}
	}

	private bool cb0b0651e317ddd8a13a67ef2b6b20484 => false;

	private bool ccea4f3d1c448b23b1c3c1542eb8327b1 => false;

	/// <summary>
	///       Gets or sets a value indicating whether <see cref="P:DigitalRune.Geometry.Collisions.ContactSet.ObjectA" /> 
	///       and <see cref="P:DigitalRune.Geometry.Collisions.ContactSet.ObjectB" /> are in contact.
	///       </summary>
	public bool HaveContact
	{
		[CompilerGenerated]
		get
		{
			return ce9ade838242f7ae1d3b19a66da86b1c1;
		}
		[CompilerGenerated]
		set
		{
			ce9ade838242f7ae1d3b19a66da86b1c1 = value;
		}
	}

	/// <summary>
	///       Gets collision object A.
	///       </summary>
	public CollisionObject ObjectA
	{
		[CompilerGenerated]
		get
		{
			return c0011e0b67ea915e2c324fb0c692753a3;
		}
		[CompilerGenerated]
		private set
		{
			c0011e0b67ea915e2c324fb0c692753a3 = value;
		}
	}

	/// <summary>
	///       Gets collision object B.
	///       </summary>
	public CollisionObject ObjectB
	{
		[CompilerGenerated]
		get
		{
			return c757f286cc55d16436490cf3fc3d8f8da;
		}
		[CompilerGenerated]
		private set
		{
			c757f286cc55d16436490cf3fc3d8f8da = value;
		}
	}

	/// <summary>
	///       Gets a copy of the contact set where <see cref="P:DigitalRune.Geometry.Collisions.ContactSet.ObjectA" /> and <see cref="P:DigitalRune.Geometry.Collisions.ContactSet.ObjectB" />
	///       are swapped.
	///       </summary>
	public ContactSet Swapped
	{
		get
		{
			ContactSet contactSet = Create(ObjectB, ObjectA);
			contactSet.c196da5f647797a182f2b6d32727c7621 = c196da5f647797a182f2b6d32727c7621;
			contactSet.HaveContact = HaveContact;
			contactSet.IsPerturbationTestAllowed = IsPerturbationTestAllowed;
			contactSet.IsValid = IsValid;
			int count = Count;
			for (int i = 0; i < count; i++)
			{
				contactSet.Add(c9d16faceeb0058b07b0e5f0c16bb5d30[i].Swapped);
			}
			return contactSet;
		}
	}

	internal bool IsValid
	{
		[CompilerGenerated]
		get
		{
			return cfec7c798a134bce22733508865c6ef8a;
		}
		[CompilerGenerated]
		set
		{
			cfec7c798a134bce22733508865c6ef8a = value;
		}
	}

	internal Vector3F c196da5f647797a182f2b6d32727c7621
	{
		get
		{
			return c0ffdbf65ac5ec2b92af8780bdfd85d3a;
		}
		set
		{
			if (value != Vector3F.Zero && !value.IsNumericallyNormalized)
			{
				throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(14051), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1040));
			}
			c0ffdbf65ac5ec2b92af8780bdfd85d3a = value;
		}
	}

	internal bool c27a62576bc69b101acc0b063aca9c47b => c0ffdbf65ac5ec2b92af8780bdfd85d3a != Vector3F.Zero;

	internal bool IsPerturbationTestAllowed
	{
		[CompilerGenerated]
		get
		{
			return cf32dd2a8c0433f0aaba17a4455620434;
		}
		[CompilerGenerated]
		set
		{
			cf32dd2a8c0433f0aaba17a4455620434 = value;
		}
	}

	private ContactSet()
	{
	}

	/// <summary>
	///       Creates an instance of the <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" /> class. (This method 
	///       reuses a previously recycled instance or allocates a new instance if necessary.)
	///       </summary>
	/// <param name="objectA">The object A.</param>
	/// <param name="objectB">The object B.</param>
	/// <returns>A new or reusable instance of the <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" /> class.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="objectA" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="objectB" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="objectA" /> and <paramref name="objectB" /> are the same.
	///       </exception>
	public static ContactSet Create(CollisionObject objectA, CollisionObject objectB)
	{
		if (objectA == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1796));
		}
		if (objectB == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1811));
		}
		if (objectA == objectB)
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(14124));
		}
		ContactSet contactSet = c8aca4a0e3c0073ffb188fde832680587.Obtain();
		contactSet.ObjectA = objectA;
		contactSet.ObjectB = objectB;
		contactSet.IsValid = true;
		contactSet.IsPerturbationTestAllowed = true;
		contactSet.c55dbd29b4a90a73bb93fd94db8e41505 = -1;
		return contactSet;
	}

	public void Recycle()
	{
		Reset(cd6e38a4032e49762f9dfd932b33937cc, c157ecb28e011e2ac5c9b038e1f15f2b7);
		c8aca4a0e3c0073ffb188fde832680587.Recycle(this);
	}

	[Obsolete("The static method Recycle() is obsolete. Use the instance method instead.")]
	public static void Recycle(ContactSet contactSet)
	{
		contactSet.Reset(cd6e38a4032e49762f9dfd932b33937cc, c157ecb28e011e2ac5c9b038e1f15f2b7);
		c8aca4a0e3c0073ffb188fde832680587.Recycle(contactSet);
	}

	private void c08c869dd9b7be760010c5602ad867605()
	{
		if (c9d16faceeb0058b07b0e5f0c16bb5d30 == null)
		{
			c9d16faceeb0058b07b0e5f0c16bb5d30 = new List<Contact>();
		}
	}

	/// <summary>
	///       Resets this contact set to default values. 
	///       </summary>
	/// <param name="objectA">The first collision object.</param>
	/// <param name="objectB">The second collision object.</param>
	public void Reset(CollisionObject objectA, CollisionObject objectB)
	{
		ObjectA = objectA;
		ObjectB = objectB;
		IsValid = true;
		c196da5f647797a182f2b6d32727c7621 = Vector3F.Zero;
		HaveContact = false;
		IsPerturbationTestAllowed = true;
		c55dbd29b4a90a73bb93fd94db8e41505 = -1;
		cc5b0109eff96f91c3e8ea71cac49ebb7 = null;
		if (c9d16faceeb0058b07b0e5f0c16bb5d30 != null)
		{
			Clear();
		}
	}

	/// <summary>
	///       Adds a <see cref="T:DigitalRune.Geometry.Collisions.Contact" /> to the end of the <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" />.
	///       </summary>
	/// <param name="contact">
	///       The contact to add to the <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" />.
	///       </param>
	public void Add(Contact contact)
	{
		c08c869dd9b7be760010c5602ad867605();
		c9d16faceeb0058b07b0e5f0c16bb5d30.Add(contact);
	}

	/// <summary>
	///       Returns a read-only <see cref="T:System.Collections.Generic.IList`1" /> wrapper for the current <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" />.
	///       </summary>
	/// <returns>
	///       A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> that acts as a read-only wrapper around the current
	///       <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" />. 
	///       </returns>
	public ReadOnlyCollection<Contact> AsReadOnly()
	{
		c08c869dd9b7be760010c5602ad867605();
		return c9d16faceeb0058b07b0e5f0c16bb5d30.AsReadOnly();
	}

	/// <summary>
	///       Removes all <see cref="T:DigitalRune.Geometry.Collisions.Contact" />s from the <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" />.
	///       </summary>
	public void Clear()
	{
		if (c9d16faceeb0058b07b0e5f0c16bb5d30 != null)
		{
			c9d16faceeb0058b07b0e5f0c16bb5d30.Clear();
		}
	}

	/// <summary>
	///       Determines whether the <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" /> contains a specific <see cref="T:DigitalRune.Geometry.Collisions.Contact" />.
	///       </summary>
	/// <param name="contact">The contact to locate in the <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" />.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="contact" /> is found in the 
	///       <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" />; otherwise, <see langword="false" />.
	///       </returns>
	public bool Contains(Contact contact)
	{
		if (c9d16faceeb0058b07b0e5f0c16bb5d30 != null)
		{
			return c9d16faceeb0058b07b0e5f0c16bb5d30.Contains(contact);
		}
		return false;
	}

	/// <summary>
	///       Copies the <see cref="T:DigitalRune.Geometry.Collisions.Contact" />s of the <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" /> to an <see cref="T:System.Array" />,
	///       starting at a particular <see cref="T:System.Array" /> index.
	///       </summary>
	/// <param name="array">
	///       The one-dimensional <see cref="T:System.Array" /> that is the destination of the 
	///       <see cref="T:DigitalRune.Geometry.Collisions.Contact" />s copied from this <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" />. The <see cref="T:System.Array" />
	///       must have zero-based indexing.
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
	///       or greater than the length of <paramref name="array" />. Or the number of contacts in the 
	///       source <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" /> is greater than the available space from 
	///       <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />. Or 
	///       type <see cref="T:DigitalRune.Geometry.Collisions.Contact" /> cannot be cast automatically to the type of the destination 
	///       <paramref name="array" />.
	///       </exception>
	public void CopyTo(Contact[] array, int arrayIndex)
	{
		if (c9d16faceeb0058b07b0e5f0c16bb5d30 != null)
		{
			c9d16faceeb0058b07b0e5f0c16bb5d30.CopyTo(array, arrayIndex);
		}
	}

	private void c44d0c5f4266290decda264ead4da31d9(Array c6278a2c6debbe0cf2d35fcb5ff5fd0d5, int c88a85a6fc8e0a66e19d046d4d51cd3f1)
	{
		if (c9d16faceeb0058b07b0e5f0c16bb5d30 != null)
		{
			((ICollection)c9d16faceeb0058b07b0e5f0c16bb5d30).CopyTo(c6278a2c6debbe0cf2d35fcb5ff5fd0d5, c88a85a6fc8e0a66e19d046d4d51cd3f1);
		}
	}

	void ICollection.CopyTo(Array c6278a2c6debbe0cf2d35fcb5ff5fd0d5, int c88a85a6fc8e0a66e19d046d4d51cd3f1)
	{
		//ILSpy generated this explicit interface implementation from .override directive in c44d0c5f4266290decda264ead4da31d9
		this.c44d0c5f4266290decda264ead4da31d9(c6278a2c6debbe0cf2d35fcb5ff5fd0d5, c88a85a6fc8e0a66e19d046d4d51cd3f1);
	}

	private IEnumerator<Contact> c81302dcb6e2b37eff517413822894cf6()
	{
		return GetEnumerator();
	}

	IEnumerator<Contact> IEnumerable<Contact>.GetEnumerator()
	{
		//ILSpy generated this explicit interface implementation from .override directive in c81302dcb6e2b37eff517413822894cf6
		return this.c81302dcb6e2b37eff517413822894cf6();
	}

	private IEnumerator c63811cf6ff5c0b86c54fd5037c6af6ad()
	{
		return GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		//ILSpy generated this explicit interface implementation from .override directive in c63811cf6ff5c0b86c54fd5037c6af6ad
		return this.c63811cf6ff5c0b86c54fd5037c6af6ad();
	}

	/// <summary>
	///       Returns an enumerator that iterates through the <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" />. 
	///       </summary>
	/// <returns>
	///       An <see cref="T:System.Collections.Generic.List`1.Enumerator" /> for <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" />.
	///       </returns>
	public List<Contact>.Enumerator GetEnumerator()
	{
		if (c9d16faceeb0058b07b0e5f0c16bb5d30 == null)
		{
			c9d16faceeb0058b07b0e5f0c16bb5d30 = new List<Contact>();
		}
		return c9d16faceeb0058b07b0e5f0c16bb5d30.GetEnumerator();
	}

	/// <summary>
	///       Determines the index of a specific <see cref="T:DigitalRune.Geometry.Collisions.Contact" /> in the <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" />.
	///       </summary>
	/// <param name="contact">The contact to locate in the <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" />.</param>
	/// <returns>
	///       The index of <paramref name="contact" /> if found in the contact set; otherwise, -1.
	///       </returns>
	public int IndexOf(Contact contact)
	{
		if (c9d16faceeb0058b07b0e5f0c16bb5d30 != null)
		{
			return c9d16faceeb0058b07b0e5f0c16bb5d30.IndexOf(contact);
		}
		return -1;
	}

	/// <summary>
	///       Inserts an <see cref="T:DigitalRune.Geometry.Collisions.Contact" /> in the <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" /> at the specified index.
	///       </summary>
	/// <param name="index">
	///       The zero-based index at which <paramref name="contact" /> should be inserted.
	///       </param>
	/// <param name="contact">The contact to insert into the <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" />.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is not a valid index in the <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" />.
	///       </exception>
	public void Insert(int index, Contact contact)
	{
		if (c9d16faceeb0058b07b0e5f0c16bb5d30 == null)
		{
			if (index != 0)
			{
				throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(549), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(14251));
			}
			c9d16faceeb0058b07b0e5f0c16bb5d30 = new List<Contact>();
		}
		c9d16faceeb0058b07b0e5f0c16bb5d30.Insert(index, contact);
	}

	/// <summary>
	///       Removes the first occurrence of a specific <see cref="T:DigitalRune.Geometry.Collisions.Contact" /> from the 
	///       <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" />.
	///       </summary>
	/// <param name="contact">The contact to remove from the <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" />.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="contact" /> was successfully removed from the 
	///       <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" />; otherwise, <see langword="false" />. This method also returns 
	///       <see langword="false" /> if <paramref name="contact" /> is not found in the original 
	///       <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" />.
	///       </returns>
	public bool Remove(Contact contact)
	{
		if (c9d16faceeb0058b07b0e5f0c16bb5d30 == null)
		{
			return false;
		}
		return c9d16faceeb0058b07b0e5f0c16bb5d30.Remove(contact);
	}

	/// <summary>
	///       Removes the <see cref="T:DigitalRune.Geometry.Collisions.Contact" /> at the specified index.
	///       </summary>
	/// <param name="index">The zero-based index of the contact to remove.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is not a valid index in the <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" />.
	///       </exception>
	public void RemoveAt(int index)
	{
		if (c9d16faceeb0058b07b0e5f0c16bb5d30 == null)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(549), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(14251));
		}
		c9d16faceeb0058b07b0e5f0c16bb5d30.RemoveAt(index);
	}

	/// <summary>
	///       Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
	///       </summary>
	/// <returns>
	///       A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
	///       </returns>
	public override string ToString()
	{
		return string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(14344), ObjectA.GeometricObject, ObjectB.GeometricObject, Count);
	}

	[CompilerGenerated]
	private static ContactSet c4bffc38c4054efe6a3f3ff877749c085()
	{
		return new ContactSet();
	}
}
