using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using A;
using DigitalRune.Collections;
using DigitalRune.Geometry.Partitioning;

namespace DigitalRune.Geometry.Collisions;

/// <summary>
///       Provides methods and settings for collision detection.
///       </summary>
public class CollisionDetection
{
	private float ca664d4b0657a48e9c40b0e7522e53c8e;

	private float c8b99649fe8ac20b87a444ad033b2d940;

	private IPairFilter<CollisionObject> cd6e99021f04d0afc8a8f1ac5f90fa9be;

	private EventHandler m_cfb5e1c5b2b7d7015e55d4c648b31473c;

	[CompilerGenerated]
	private CollisionAlgorithmMatrix c32c0397ae30e03a1296441f1a675fc87;

	[CompilerGenerated]
	private ContinuousCollisionDetectionMode ce30f6666a6867c8d9c15f001e5c4fb0f;

	[CompilerGenerated]
	private IContactFilter c57a73afbccc5e2751d2d45b9f13fe667;

	[CompilerGenerated]
	private bool c57682a46fffd7b50b672eba0052f8729;

	/// <summary>
	///       Gets the collision algorithm matrix.
	///       </summary>
	public CollisionAlgorithmMatrix AlgorithmMatrix
	{
		[CompilerGenerated]
		get
		{
			return c32c0397ae30e03a1296441f1a675fc87;
		}
		[CompilerGenerated]
		private set
		{
			c32c0397ae30e03a1296441f1a675fc87 = value;
		}
	}

	/// <summary>
	///       Gets or sets the contact position tolerance.
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is negative.
	///       </exception>
	public float ContactPositionTolerance
	{
		get
		{
			return ca664d4b0657a48e9c40b0e7522e53c8e;
		}
		set
		{
			if (value < 0f)
			{
				while (true)
				{
					switch (2)
					{
					case 0:
						break;
					default:
						if (1 == 0)
						{
							__ldtoken(CollisionDetection.set_ContactPositionTolerance(float));
						}
						throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1040), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(12060));
					}
				}
			}
			ca664d4b0657a48e9c40b0e7522e53c8e = value;
		}
	}

	/// <summary>
	///       Gets or sets the mode of the continuous collision detection.
	///       </summary>
	public ContinuousCollisionDetectionMode ContinuousCollisionDetectionMode
	{
		[CompilerGenerated]
		get
		{
			return ce30f6666a6867c8d9c15f001e5c4fb0f;
		}
		[CompilerGenerated]
		set
		{
			ce30f6666a6867c8d9c15f001e5c4fb0f = value;
		}
	}

	/// <summary>
	///       Gets or sets the collision epsilon (numerical tolerance value).
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is negative.
	///       </exception>
	public float Epsilon
	{
		get
		{
			return c8b99649fe8ac20b87a444ad033b2d940;
		}
		set
		{
			if (value < 0f)
			{
				throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1040), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(12185));
			}
			c8b99649fe8ac20b87a444ad033b2d940 = value;
		}
	}

	/// <summary>
	///       Gets or sets the collision filter for contact queries.
	///       </summary>
	public IPairFilter<CollisionObject> CollisionFilter
	{
		get
		{
			return cd6e99021f04d0afc8a8f1ac5f90fa9be;
		}
		set
		{
			if (cd6e99021f04d0afc8a8f1ac5f90fa9be == value)
			{
				while (true)
				{
					switch (6)
					{
					case 0:
						break;
					default:
						if (1 == 0)
						{
							__ldtoken(CollisionDetection.set_CollisionFilter(IPairFilter<CollisionObject>));
						}
						return;
					}
				}
			}
			if (cd6e99021f04d0afc8a8f1ac5f90fa9be != null)
			{
				cd6e99021f04d0afc8a8f1ac5f90fa9be.Changed -= c58a5fa51280a5beb38f93c24fd9567fd;
			}
			cd6e99021f04d0afc8a8f1ac5f90fa9be = value;
			if (cd6e99021f04d0afc8a8f1ac5f90fa9be != null)
			{
				cd6e99021f04d0afc8a8f1ac5f90fa9be.Changed += c58a5fa51280a5beb38f93c24fd9567fd;
			}
			c58a5fa51280a5beb38f93c24fd9567fd(this, EventArgs.Empty);
		}
	}

	/// <summary>
	///       Gets or sets the contact filter.
	///       </summary>
	public IContactFilter ContactFilter
	{
		[CompilerGenerated]
		get
		{
			return c57a73afbccc5e2751d2d45b9f13fe667;
		}
		[CompilerGenerated]
		set
		{
			c57a73afbccc5e2751d2d45b9f13fe667 = value;
		}
	}

	/// <summary>
	///       Experimental: Gets or sets a value indicating whether the full contact set should be
	///       found per frame.
	///       </summary>
	public bool FullContactSetPerFrame
	{
		[CompilerGenerated]
		get
		{
			return c57682a46fffd7b50b672eba0052f8729;
		}
		[CompilerGenerated]
		set
		{
			c57682a46fffd7b50b672eba0052f8729 = value;
		}
	}

	internal event EventHandler cfb5e1c5b2b7d7015e55d4c648b31473c
	{
		add
		{
			EventHandler eventHandler = this.m_cfb5e1c5b2b7d7015e55d4c648b31473c;
			EventHandler eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler value2 = (EventHandler)Delegate.Combine(eventHandler2, value);
				eventHandler = Interlocked.CompareExchange(ref this.m_cfb5e1c5b2b7d7015e55d4c648b31473c, value2, eventHandler2);
			}
			while ((object)eventHandler != eventHandler2);
			while (true)
			{
				switch (5)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(CollisionDetection.add_cfb5e1c5b2b7d7015e55d4c648b31473c(EventHandler));
				}
				return;
			}
		}
		remove
		{
			EventHandler eventHandler = this.m_cfb5e1c5b2b7d7015e55d4c648b31473c;
			EventHandler eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler value2 = (EventHandler)Delegate.Remove(eventHandler2, value);
				eventHandler = Interlocked.CompareExchange(ref this.m_cfb5e1c5b2b7d7015e55d4c648b31473c, value2, eventHandler2);
			}
			while ((object)eventHandler != eventHandler2);
		}
	}

	static CollisionDetection()
	{
		TypeInitializer.Initialize(ccb48cf63175e6724337dcebf74566228.ce9f9bc9ecd5ed400a711abaa33452c25);
		TypeInitializer.CreateInstance(12, ccb48cf63175e6724337dcebf74566228.ccda5e1cdd8faa90b24f6bc28c9af6b28);
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Collisions.CollisionDetection" /> class.
	///       </summary>
	public CollisionDetection()
	{
		Epsilon = 0.001f;
		ContactPositionTolerance = 0.01f;
		ContactFilter = new ContactReducer();
		ContinuousCollisionDetectionMode = ContinuousCollisionDetectionMode.Linear;
		AlgorithmMatrix = new CollisionAlgorithmMatrix(this);
	}

	/// <summary>
	///       Returns <see langword="true" /> if the axis-aligned bounding boxes (AABBs) of two
	///       <see cref="T:DigitalRune.Geometry.Collisions.CollisionObject" />s are in contact.
	///       </summary>
	/// <param name="objectA">The first collision object.</param>
	/// <param name="objectB">The second collision object.</param>
	/// <returns>
	///   <see langword="true" /> if the objects' AABBs are touching or intersecting; otherwise
	///       <see langword="false" />.
	///       </returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="objectA" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="objectB" /> is <see langword="null" />.
	///       </exception>
	public bool HaveAabbContact(CollisionObject objectA, CollisionObject objectB)
	{
		if (objectA == null)
		{
			while (true)
			{
				switch (2)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(CollisionDetection.HaveAabbContact(CollisionObject, CollisionObject));
				}
				throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1796));
			}
		}
		if (objectB == null)
		{
			while (true)
			{
				switch (5)
				{
				case 0:
					continue;
				}
				throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1811));
			}
		}
		if (objectA.Enabled && objectB.Enabled)
		{
			if (CollisionFilter != null)
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
				if (!CollisionFilter.Filter(new Pair<CollisionObject>(objectA, objectB)))
				{
					while (true)
					{
						switch (1)
						{
						case 0:
							continue;
						}
						break;
					}
					goto IL_008b;
				}
			}
			return GeometryHelper.HaveContact(objectA.GeometricObject.Aabb, objectB.GeometricObject.Aabb);
		}
		goto IL_008b;
		IL_008b:
		return false;
	}

	/// <summary>
	///       Returns <see langword="true" /> if two <see cref="T:DigitalRune.Geometry.Collisions.CollisionObject" />s are in contact.
	///       </summary>
	/// <param name="objectA">The first collision object.</param>
	/// <param name="objectB">The second collision object.</param>
	/// <returns>
	///   <see langword="true" /> if the object are touching or intersecting; otherwise 
	///       <see langword="false" />.
	///       </returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="objectA" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="objectB" /> is <see langword="null" />.
	///       </exception>
	public bool HaveContact(CollisionObject objectA, CollisionObject objectB)
	{
		if (!HaveAabbContact(objectA, objectB))
		{
			return false;
		}
		return AlgorithmMatrix[objectA, objectB].HaveContact(objectA, objectB);
	}

	/// <summary>
	///       Computes the closest points between two <see cref="T:DigitalRune.Geometry.Collisions.CollisionObject" />s.
	///       </summary>
	/// <param name="objectA">The first collision object.</param>
	/// <param name="objectB">The second collision object.</param>
	/// <returns>
	///       The <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" /> with the closest-point information. The 
	///       <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" />will have exactly 1 <see cref="T:DigitalRune.Geometry.Collisions.Contact" /> (describing the
	///       closest-point pair).
	///       </returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="objectA" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="objectB" /> is <see langword="null" />.
	///       </exception>
	public ContactSet GetClosestPoints(CollisionObject objectA, CollisionObject objectB)
	{
		if (objectA == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1796));
		}
		if (objectB == null)
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
						__ldtoken(CollisionDetection.GetClosestPoints(CollisionObject, CollisionObject));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1811));
				}
			}
		}
		return AlgorithmMatrix[objectA, objectB].GetClosestPoints(objectA, objectB);
	}

	/// <summary>
	///       Computes the contacts between two <see cref="T:DigitalRune.Geometry.Collisions.CollisionObject" />s.
	///       </summary>
	/// <param name="objectA">The first collision object.</param>
	/// <param name="objectB">The second collision object.</param>
	/// <returns>
	///       A <see cref="T:DigitalRune.Geometry.Collisions.ContactSet" /> describing the contact information if <paramref name="objectA" />
	///       and <paramref name="objectB" /> are intersecting; otherwise, <see langword="null" /> if the
	///       objects are separated.
	///       </returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="objectA" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="objectB" /> is <see langword="null" />.
	///       </exception>
	public ContactSet GetContacts(CollisionObject objectA, CollisionObject objectB)
	{
		if (!HaveAabbContact(objectA, objectB))
		{
			return null;
		}
		ContactSet contacts = AlgorithmMatrix[objectA, objectB].GetContacts(objectA, objectB);
		if (contacts.HaveContact)
		{
			while (true)
			{
				switch (2)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(CollisionDetection.GetContacts(CollisionObject, CollisionObject));
					}
					return contacts;
				}
			}
		}
		contacts.Recycle();
		return null;
	}

	/// <summary>
	///       Gets the time of impact between two moving objects.
	///       </summary>
	/// <param name="objectA">The object A.</param>
	/// <param name="targetPoseA">The target pose of A.</param>
	/// <param name="objectB">The object B.</param>
	/// <param name="targetPoseB">The target pose of B.</param>
	/// <param name="allowedPenetration">
	///       The allowed penetration. A positive allowed penetration value makes sure that the objects 
	///       have a measurable contact at the time of impact.
	///       </param>
	/// <returns>The time of impact in the range [0, 1].</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="objectA" /> or <paramref name="objectB" /> is <see langword="null" />.
	///       </exception>
	public float GetTimeOfImpact(CollisionObject objectA, Pose targetPoseA, CollisionObject objectB, Pose targetPoseB, float allowedPenetration)
	{
		if (objectA == null)
		{
			while (true)
			{
				switch (6)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(CollisionDetection.GetTimeOfImpact(CollisionObject, Pose, CollisionObject, Pose, float));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1796));
				}
			}
		}
		if (objectB == null)
		{
			while (true)
			{
				switch (7)
				{
				case 0:
					break;
				default:
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1811));
				}
			}
		}
		return AlgorithmMatrix[objectA, objectB].GetTimeOfImpact(objectA, targetPoseA, objectB, targetPoseB, allowedPenetration);
	}

	/// <summary>
	///       Updates the closest-point information in the given contact set.
	///       </summary>
	/// <param name="contactSet">
	///       The contact set containing the last known closest-point information.
	///       </param>
	/// <param name="deltaTime">
	///       The time step size in seconds. (The elapsed simulation time since 
	///       <see cref="M:DigitalRune.Geometry.Collisions.CollisionDetection.UpdateClosestPoints(DigitalRune.Geometry.Collisions.ContactSet,System.Single)" /> or <see cref="M:DigitalRune.Geometry.Collisions.CollisionDetection.UpdateContacts(DigitalRune.Geometry.Collisions.ContactSet,System.Single)" /> was last called for this
	///       contact set.)
	///       </param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="contactSet" /> is <see langword="null" />.
	///       </exception>
	public void UpdateClosestPoints(ContactSet contactSet, float deltaTime)
	{
		if (contactSet == null)
		{
			while (true)
			{
				switch (2)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(CollisionDetection.UpdateClosestPoints(ContactSet, float));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1826));
				}
			}
		}
		AlgorithmMatrix[contactSet].UpdateClosestPoints(contactSet, deltaTime);
	}

	/// <summary>
	///       Updates the contact information in the given contact set.
	///       </summary>
	/// <param name="contactSet">The contact set containing the last known contacts.</param>
	/// <param name="deltaTime">
	///       The time step size in seconds. (The elapsed simulation time since 
	///       <see cref="M:DigitalRune.Geometry.Collisions.CollisionDetection.UpdateClosestPoints(DigitalRune.Geometry.Collisions.ContactSet,System.Single)" /> or <see cref="M:DigitalRune.Geometry.Collisions.CollisionDetection.UpdateContacts(DigitalRune.Geometry.Collisions.ContactSet,System.Single)" /> was last called for this
	///       contact set.)
	///       </param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="contactSet" /> is <see langword="null" />.
	///       </exception>
	public void UpdateContacts(ContactSet contactSet, float deltaTime)
	{
		if (contactSet == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1826));
		}
		if (HaveAabbContact(contactSet.ObjectA, contactSet.ObjectB))
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
						__ldtoken(CollisionDetection.UpdateContacts(ContactSet, float));
					}
					AlgorithmMatrix[contactSet].UpdateContacts(contactSet, deltaTime);
					return;
				}
			}
		}
		using (List<Contact>.Enumerator enumerator = contactSet.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				Contact current = enumerator.Current;
				current.Recycle();
			}
			while (true)
			{
				switch (2)
				{
				case 0:
					break;
				default:
					goto end_IL_007c;
				}
				continue;
				end_IL_007c:
				break;
			}
		}
		contactSet.Clear();
		contactSet.HaveContact = false;
	}

	internal void c58a5fa51280a5beb38f93c24fd9567fd(object c807f682152464d11dde83a885bf87124, EventArgs c7fefeb91d4632901bd52a1f245b70db1)
	{
		EventHandler eventHandler = this.m_cfb5e1c5b2b7d7015e55d4c648b31473c;
		if (eventHandler == null)
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
				__ldtoken(CollisionDetection.c58a5fa51280a5beb38f93c24fd9567fd(object, EventArgs));
			}
			eventHandler(this, c7fefeb91d4632901bd52a1f245b70db1);
			return;
		}
	}
}
