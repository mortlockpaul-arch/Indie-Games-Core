using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using A;
using DigitalRune.Geometry.Shapes;

namespace DigitalRune.Geometry.Collisions.Algorithms;

/// <summary>
///       A collision algorithm computes contact information or closest-point information for 
///       collision objects with certain shapes.
///       </summary>
public abstract class CollisionAlgorithm
{
	[CompilerGenerated]
	private CollisionDetection ce318f76508d8601dc346daf3ba322cfa;

	[CompilerGenerated]
	private static Func<Contact, bool> c19ae5197e8f1d77c5b9a5a5a3cba33d3;

	[CompilerGenerated]
	private static Func<Contact, bool> c70e908f904d35f6bba1c2f6562a9abdb;

	[CompilerGenerated]
	private static Func<Contact, bool> c422f799ed4ec89e25dd968008fe98786;

	/// <summary>
	///       Gets the collision detection service.
	///       </summary>
	public CollisionDetection CollisionDetection
	{
		[CompilerGenerated]
		get
		{
			return ce318f76508d8601dc346daf3ba322cfa;
		}
		[CompilerGenerated]
		private set
		{
			ce318f76508d8601dc346daf3ba322cfa = value;
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Collisions.Algorithms.CollisionAlgorithm" /> class.
	///       </summary>
	/// <param name="collisionDetection">The collision detection service.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="collisionDetection" /> is <see langword="null" />.
	///       </exception>
	protected CollisionAlgorithm(CollisionDetection collisionDetection)
	{
		if (collisionDetection == null)
		{
			while (true)
			{
				switch (1)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(CollisionAlgorithm._002Ector(CollisionDetection));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2553));
				}
			}
		}
		CollisionDetection = collisionDetection;
	}

	/// <summary>
	///       Gets the closest points of two collision objects.
	///       </summary>
	/// <param name="objectA">The collision object A.</param>
	/// <param name="objectB">The collision object B.</param>
	/// <returns>The contact set with the closest points.</returns>
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
				switch (6)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(CollisionAlgorithm.GetClosestPoints(CollisionObject, CollisionObject));
				}
				throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1811));
			}
		}
		ContactSet contactSet = ContactSet.Create(objectA, objectB);
		ComputeCollision(contactSet, CollisionQueryType.ClosestPoints);
		if (!objectA.cedb7bba503d90238311e100ff7d9069a)
		{
			if (!objectB.cedb7bba503d90238311e100ff7d9069a)
			{
				c9ae4fc785e8cd5184185f6ee6d5a3853.c7ea5a400e98aa00477faf1c0240ebaec(contactSet);
				goto IL_0079;
			}
			while (true)
			{
				switch (4)
				{
				case 0:
					continue;
				}
				break;
			}
		}
		c9ae4fc785e8cd5184185f6ee6d5a3853.c8312ce88b68411fc3377bc0e963b6c59(contactSet);
		goto IL_0079;
		IL_0079:
		return contactSet;
	}

	/// <summary>
	///       Gets the contact information of two possibly touching collision objects.
	///       </summary>
	/// <param name="objectA">The collision object A.</param>
	/// <param name="objectB">The collision object B.</param>
	/// <returns>The contact set.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="objectA" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="objectB" /> is <see langword="null" />.
	///       </exception>
	public ContactSet GetContacts(CollisionObject objectA, CollisionObject objectB)
	{
		if (objectA == null)
		{
			while (true)
			{
				switch (6)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(CollisionAlgorithm.GetContacts(CollisionObject, CollisionObject));
				}
				throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1796));
			}
		}
		if (objectB == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1811));
		}
		bool flag = objectA.Type == CollisionObjectType.Trigger || objectB.Type == CollisionObjectType.Trigger;
		if (!objectA.c0dbe012fb6e35eff096504ebd112177b)
		{
			while (true)
			{
				switch (6)
				{
				case 0:
					continue;
				}
				break;
			}
			if (!objectB.c0dbe012fb6e35eff096504ebd112177b)
			{
				goto IL_0073;
			}
		}
		flag = false;
		goto IL_0073;
		IL_0073:
		ContactSet contactSet = ContactSet.Create(objectA, objectB);
		if (flag)
		{
			while (true)
			{
				switch (2)
				{
				case 0:
					continue;
				}
				break;
			}
			ComputeCollision(contactSet, CollisionQueryType.Boolean);
		}
		else
		{
			ComputeCollision(contactSet, CollisionQueryType.Contacts);
		}
		if (!objectA.cedb7bba503d90238311e100ff7d9069a)
		{
			if (!objectB.cedb7bba503d90238311e100ff7d9069a)
			{
				goto IL_00bc;
			}
			while (true)
			{
				switch (3)
				{
				case 0:
					continue;
				}
				break;
			}
		}
		c9ae4fc785e8cd5184185f6ee6d5a3853.c8312ce88b68411fc3377bc0e963b6c59(contactSet);
		goto IL_00bc;
		IL_00bc:
		if (CollisionDetection.ContactFilter != null)
		{
			while (true)
			{
				switch (2)
				{
				case 0:
					continue;
				}
				break;
			}
			CollisionDetection.ContactFilter.Filter(contactSet);
		}
		return contactSet;
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
	public virtual float GetTimeOfImpact(CollisionObject objectA, Pose targetPoseA, CollisionObject objectB, Pose targetPoseB, float allowedPenetration)
	{
		if (objectA == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1796));
		}
		if (objectB == null)
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
						__ldtoken(CollisionAlgorithm.GetTimeOfImpact(CollisionObject, Pose, CollisionObject, Pose, float));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1811));
				}
			}
		}
		if (objectA.GeometricObject.Shape is ConvexShape)
		{
			while (true)
			{
				switch (7)
				{
				case 0:
					continue;
				}
				break;
			}
			if (objectB.GeometricObject.Shape is ConvexShape)
			{
				if (CollisionDetection.ContinuousCollisionDetectionMode == ContinuousCollisionDetectionMode.Full)
				{
					return c33408081634d45382cc69064025f3c7f.c90c3411c13dad6b165001e39c0f00faf(objectA, targetPoseA, objectB, targetPoseB, allowedPenetration, CollisionDetection);
				}
				return c33408081634d45382cc69064025f3c7f.ca9ba452be1650d033669f0368c6e76e7(objectA, targetPoseA, objectB, targetPoseB, allowedPenetration);
			}
		}
		return 1f;
	}

	/// <summary>
	///       Determines whether two collision objects are in contact.
	///       </summary>
	/// <param name="objectA">The collision object A.</param>
	/// <param name="objectB">The collision object B.</param>
	/// <returns>
	///   <see langword="true" /> if the objects are touching (or penetrating); otherwise 
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
		if (objectA == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1796));
		}
		if (objectB == null)
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
						__ldtoken(CollisionAlgorithm.HaveContact(CollisionObject, CollisionObject));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1811));
				}
			}
		}
		ContactSet contactSet = ContactSet.Create(objectA, objectB);
		ComputeCollision(contactSet, CollisionQueryType.Boolean);
		bool haveContact = contactSet.HaveContact;
		contactSet.Recycle();
		return haveContact;
	}

	/// <summary>
	///       Performs a collision query to update the closest-point information in the contact set.
	///       </summary>
	/// <param name="contactSet">The contact set.</param>
	/// <param name="deltaTime">
	///       The time step size in seconds. (The elapsed simulation time since the contact set was
	///       updated the last time.)
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
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(CollisionAlgorithm.UpdateClosestPoints(ContactSet, float));
				}
				throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1826));
			}
		}
		c9ae4fc785e8cd5184185f6ee6d5a3853.c4428c2d517a1c32aa278a32dae4fe2d3(contactSet, deltaTime, CollisionDetection.ContactPositionTolerance);
		ComputeCollision(contactSet, CollisionQueryType.ClosestPoints);
		if (!contactSet.ObjectA.cedb7bba503d90238311e100ff7d9069a)
		{
			while (true)
			{
				switch (7)
				{
				case 0:
					continue;
				}
				break;
			}
			if (!contactSet.ObjectB.cedb7bba503d90238311e100ff7d9069a)
			{
				c9ae4fc785e8cd5184185f6ee6d5a3853.c7ea5a400e98aa00477faf1c0240ebaec(contactSet);
				goto IL_007a;
			}
		}
		c9ae4fc785e8cd5184185f6ee6d5a3853.c8312ce88b68411fc3377bc0e963b6c59(contactSet);
		goto IL_007a;
		IL_007a:
		contactSet.IsValid = true;
	}

	/// <summary>
	///       Performs a collision query to update the contact information in the contact set.
	///       </summary>
	/// <param name="contactSet">The contact set.</param>
	/// <param name="deltaTime">
	///       The time step size in seconds. (The elapsed simulation time since the contact set was
	///       updated the last time.)
	///       </param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="contactSet" /> is <see langword="null" />.
	///       </exception>
	public void UpdateContacts(ContactSet contactSet, float deltaTime)
	{
		if (contactSet == null)
		{
			while (true)
			{
				switch (7)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(CollisionAlgorithm.UpdateContacts(ContactSet, float));
				}
				throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1826));
			}
		}
		c9ae4fc785e8cd5184185f6ee6d5a3853.c85fd3b007f86fc234e44b356b56c836c(contactSet);
		c9ae4fc785e8cd5184185f6ee6d5a3853.c4428c2d517a1c32aa278a32dae4fe2d3(contactSet, deltaTime, CollisionDetection.ContactPositionTolerance);
		bool flag = contactSet.ObjectA.Type == CollisionObjectType.Trigger || contactSet.ObjectB.Type == CollisionObjectType.Trigger;
		if (contactSet.ObjectA.c0dbe012fb6e35eff096504ebd112177b || contactSet.ObjectB.c0dbe012fb6e35eff096504ebd112177b)
		{
			flag = false;
		}
		if (flag)
		{
			if (contactSet.HaveContact)
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
				if (contactSet.Count != 0)
				{
					goto IL_00b3;
				}
			}
			ComputeCollision(contactSet, CollisionQueryType.Boolean);
		}
		else
		{
			ComputeCollision(contactSet, CollisionQueryType.Contacts);
		}
		goto IL_00b3;
		IL_015d:
		contactSet.IsValid = true;
		return;
		IL_00b3:
		if (!contactSet.HaveContact)
		{
			using (List<Contact>.Enumerator enumerator = contactSet.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Contact current = enumerator.Current;
					current.Recycle();
				}
				while (true)
				{
					switch (6)
					{
					case 0:
						break;
					default:
						goto end_IL_00df;
					}
					continue;
					end_IL_00df:
					break;
				}
			}
			contactSet.Clear();
			goto IL_015d;
		}
		if (!contactSet.ObjectA.cedb7bba503d90238311e100ff7d9069a)
		{
			if (!contactSet.ObjectB.cedb7bba503d90238311e100ff7d9069a)
			{
				goto IL_0131;
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
		}
		c9ae4fc785e8cd5184185f6ee6d5a3853.c8312ce88b68411fc3377bc0e963b6c59(contactSet);
		goto IL_0131;
		IL_0131:
		if (CollisionDetection.ContactFilter != null)
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
			CollisionDetection.ContactFilter.Filter(contactSet);
		}
		goto IL_015d;
	}

	/// <summary>
	///       Computes the collision. - This method should only be used by 
	///       <see cref="T:DigitalRune.Geometry.Collisions.Algorithms.CollisionAlgorithm" /> instances!
	///       </summary>
	/// <param name="contactSet">The contact set.</param>
	/// <param name="type">The type of collision query.</param>
	public abstract void ComputeCollision(ContactSet contactSet, CollisionQueryType type);

	[Conditional("DEBUG")]
	private static void c22863b1b8520f400dee017d7b6e02552(ContactSet cfc73391dcf53c37de15d54e2434d0199, bool cca55282db198cb321cede718c2ef549a)
	{
		if (cca55282db198cb321cede718c2ef549a && cfc73391dcf53c37de15d54e2434d0199.Count > 1)
		{
			throw new GeometryException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1847));
		}
		if (cfc73391dcf53c37de15d54e2434d0199.HaveContact)
		{
			while (true)
			{
				switch (6)
				{
				case 0:
					continue;
				}
				break;
			}
			if (1 == 0)
			{
				__ldtoken(CollisionAlgorithm.c22863b1b8520f400dee017d7b6e02552(ContactSet, bool));
			}
			if (cfc73391dcf53c37de15d54e2434d0199.Any((Contact c4858ff102f4ef50097c5a533c1478c21) => c4858ff102f4ef50097c5a533c1478c21.PenetrationDepth < 0f))
			{
				throw new GeometryException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1964));
			}
			if (!(cfc73391dcf53c37de15d54e2434d0199.ObjectA.GeometricObject.Shape is RayShape))
			{
				while (true)
				{
					switch (5)
					{
					case 0:
						continue;
					}
					break;
				}
				if (!(cfc73391dcf53c37de15d54e2434d0199.ObjectB.GeometricObject.Shape is RayShape))
				{
					goto IL_0123;
				}
				while (true)
				{
					switch (3)
					{
					case 0:
						continue;
					}
					break;
				}
			}
			if (cfc73391dcf53c37de15d54e2434d0199.Count > 1)
			{
				throw new GeometryException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2102));
			}
			if (c70e908f904d35f6bba1c2f6562a9abdb == null)
			{
				while (true)
				{
					switch (2)
					{
					case 0:
						continue;
					}
					break;
				}
				c70e908f904d35f6bba1c2f6562a9abdb = (Contact c4858ff102f4ef50097c5a533c1478c21) => !c4858ff102f4ef50097c5a533c1478c21.IsRayHit;
			}
			if (cfc73391dcf53c37de15d54e2434d0199.Any(c70e908f904d35f6bba1c2f6562a9abdb))
			{
				while (true)
				{
					switch (6)
					{
					case 0:
						break;
					default:
						throw new GeometryException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2197));
					}
				}
			}
		}
		goto IL_0123;
		IL_0123:
		if (!cca55282db198cb321cede718c2ef549a && !cfc73391dcf53c37de15d54e2434d0199.HaveContact && cfc73391dcf53c37de15d54e2434d0199.Count > 0)
		{
			while (true)
			{
				switch (5)
				{
				case 0:
					break;
				default:
					throw new GeometryException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2266));
				}
			}
		}
		if (cfc73391dcf53c37de15d54e2434d0199.HaveContact)
		{
			return;
		}
		while (true)
		{
			switch (5)
			{
			case 0:
				continue;
			}
			if (c422f799ed4ec89e25dd968008fe98786 == null)
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
				c422f799ed4ec89e25dd968008fe98786 = (Contact c4858ff102f4ef50097c5a533c1478c21) => c4858ff102f4ef50097c5a533c1478c21.PenetrationDepth >= 0f;
			}
			if (!cfc73391dcf53c37de15d54e2434d0199.Any(c422f799ed4ec89e25dd968008fe98786))
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
				throw new GeometryException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2391));
			}
		}
	}

	[CompilerGenerated]
	private static bool c03bc13715efca6a36d96fc39c99600a7(Contact c4858ff102f4ef50097c5a533c1478c21)
	{
		return c4858ff102f4ef50097c5a533c1478c21.PenetrationDepth < 0f;
	}

	[CompilerGenerated]
	private static bool ccf0c55995bf559db63d95a1bca12c125(Contact c4858ff102f4ef50097c5a533c1478c21)
	{
		return !c4858ff102f4ef50097c5a533c1478c21.IsRayHit;
	}

	[CompilerGenerated]
	private static bool cae814e62e46cfd7b5fa17caa5159f425(Contact c4858ff102f4ef50097c5a533c1478c21)
	{
		return c4858ff102f4ef50097c5a533c1478c21.PenetrationDepth >= 0f;
	}
}
