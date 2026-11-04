using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using A;
using DigitalRune.Collections;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Collisions.Algorithms;

/// <summary>
///       Computes contact or closest-point information for <see cref="T:DigitalRune.Geometry.Shapes.RayShape" /> vs. 
///       <see cref="T:DigitalRune.Geometry.Shapes.CompositeShape" />.
///       </summary>
public class RayCompositeAlgorithm : CollisionAlgorithm
{
	private CompositeShapeAlgorithm c4b38c33f832e334dc4346170faa25f3c;

	[CompilerGenerated]
	private static Func<IGeometricObject, bool> c49253b4520c1b7e183dfcc9c426c0283;

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Collisions.Algorithms.RayCompositeAlgorithm" /> class.
	///       </summary>
	/// <param name="collisionDetection">The collision detection service.</param>
	public RayCompositeAlgorithm(CollisionDetection collisionDetection)
		: base(collisionDetection)
	{
		c4b38c33f832e334dc4346170faa25f3c = new CompositeShapeAlgorithm(collisionDetection);
	}

	/// <summary>
	///       Computes the collision. - This method should only be used by 
	///       <see cref="T:DigitalRune.Geometry.Collisions.Algorithms.CollisionAlgorithm" /> instances!
	///       </summary>
	/// <param name="contactSet">The contact set.</param>
	/// <param name="type">The type of collision query.</param>
	public override void ComputeCollision(ContactSet contactSet, CollisionQueryType type)
	{
		if (type == CollisionQueryType.ClosestPoints)
		{
			c4b38c33f832e334dc4346170faa25f3c.ComputeCollision(contactSet, type);
			return;
		}
		IGeometricObject obj = contactSet.ObjectA.GeometricObject;
		IGeometricObject obj2 = contactSet.ObjectB.GeometricObject;
		bool flag = obj.Shape is RayShape;
		if (flag)
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
				__ldtoken(RayCompositeAlgorithm.ComputeCollision(ContactSet, CollisionQueryType));
			}
			MathHelper.Swap(ref obj2, ref obj);
		}
		RayShape rayShape = obj2.Shape as RayShape;
		CompositeShape compositeShape = obj.Shape as CompositeShape;
		Vector3F ceabb47d7d959545d89c4300eabbdd7c;
		Pose cbc1b7e0b011ad6dcdb8beb392ec7a;
		Vector3F scale;
		Pose cbc1b7e0b011ad6dcdb8beb392ec7a2;
		if (rayShape != null)
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
			if (compositeShape != null)
			{
				contactSet.HaveContact = false;
				ceabb47d7d959545d89c4300eabbdd7c = obj2.Scale;
				cbc1b7e0b011ad6dcdb8beb392ec7a = obj2.Pose;
				scale = obj.Scale;
				cbc1b7e0b011ad6dcdb8beb392ec7a2 = obj.Pose;
				if (compositeShape != null)
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
					if (scale.X == scale.Y)
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
						if (scale.Y == scale.Z)
						{
							goto IL_0165;
						}
						while (true)
						{
							switch (5)
							{
							case 0:
								continue;
							}
							break;
						}
					}
					NotifyingCollection<IGeometricObject> children = compositeShape.Children;
					if (c49253b4520c1b7e183dfcc9c426c0283 == null)
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
						c49253b4520c1b7e183dfcc9c426c0283 = (IGeometricObject cec3b7e534d1dd4ef5c11099cf1a04b42) => cec3b7e534d1dd4ef5c11099cf1a04b42.Pose.HasRotation;
					}
					if (children.Any(c49253b4520c1b7e183dfcc9c426c0283))
					{
						while (true)
						{
							switch (5)
							{
							case 0:
								break;
							default:
								throw new NotSupportedException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6686));
							}
						}
					}
				}
				goto IL_0165;
			}
		}
		throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9267), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1826));
		IL_0165:
		CollisionObject collisionObject = cdb80ee76c96fc0751dea8d15ee69fa47.c29a5464f174eae366728cbb2edc64776.Obtain();
		c107cf2db36674a478eaf84e50d31337e c107cf2db36674a478eaf84e50d31337e2 = c107cf2db36674a478eaf84e50d31337e.c2d8ce8b1ccf2b905e3b18f8a5145fe8a();
		ContactSet contactSet2 = ContactSet.Create(collisionObject, contactSet.ObjectB);
		Ray ray = new Ray(rayShape);
		ray.c22463d4a85fcdaa7d4ffbd8669084d72(ref ceabb47d7d959545d89c4300eabbdd7c);
		ray.c77d310c5ae14ed7e7ed360995b1867bb(ref cbc1b7e0b011ad6dcdb8beb392ec7a);
		Ray ray2 = ray;
		ray2.c0d5e70cac6e675cb3df8faaefbfd7052(ref cbc1b7e0b011ad6dcdb8beb392ec7a2);
		Vector3F ceabb47d7d959545d89c4300eabbdd7c2 = Vector3F.One / scale;
		ray2.c22463d4a85fcdaa7d4ffbd8669084d72(ref ceabb47d7d959545d89c4300eabbdd7c2);
		try
		{
			if (compositeShape.Partition != null)
			{
				IEnumerator<int> enumerator = compositeShape.Partition.GetOverlaps(ray2).GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						int current = enumerator.Current;
						if (type == CollisionQueryType.Boolean)
						{
							if (contactSet.HaveContact)
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
								break;
							}
						}
						cb144978528da5703ffab741a9f4a9bb8(contactSet, flag, current, type, contactSet2, collisionObject, c107cf2db36674a478eaf84e50d31337e2);
					}
					while (true)
					{
						switch (5)
						{
						case 0:
							break;
						default:
							return;
						}
					}
				}
				finally
				{
					if (enumerator != null)
					{
						while (true)
						{
							switch (7)
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
			Vector3F c3ec982f192efbba86e42a1cea5c = new Vector3F(1f / ray2.Direction.X, 1f / ray2.Direction.Y, 1f / ray2.Direction.Z);
			float c912da9d2d82e2a6523be60260abdfc = Numeric.EpsilonF * (1f + obj.Aabb.Extent.Length);
			int count = compositeShape.Children.Count;
			for (int num = 0; num < count; num++)
			{
				IGeometricObject geometricObject = compositeShape.Children[num];
				if (!GeometryHelper.cdd6d17271fec83883494ba8e43eede6a(geometricObject.Shape.GetAabb(geometricObject.Scale, geometricObject.Pose), ray2.Origin, c3ec982f192efbba86e42a1cea5c, ray2.Length, c912da9d2d82e2a6523be60260abdfc))
				{
					continue;
				}
				while (true)
				{
					switch (6)
					{
					case 0:
						continue;
					}
					break;
				}
				cb144978528da5703ffab741a9f4a9bb8(contactSet, flag, num, type, contactSet2, collisionObject, c107cf2db36674a478eaf84e50d31337e2);
				if (!contactSet.HaveContact)
				{
					continue;
				}
				if (type == CollisionQueryType.Boolean)
				{
					break;
				}
				while (true)
				{
					switch (1)
					{
					case 0:
						continue;
					}
					break;
				}
			}
		}
		finally
		{
			contactSet2.Recycle();
			cdb80ee76c96fc0751dea8d15ee69fa47.c29a5464f174eae366728cbb2edc64776.Recycle(collisionObject);
			c107cf2db36674a478eaf84e50d31337e2.ca18b9b5faa9851d6355f617787bb5859();
		}
	}

	private void cb144978528da5703ffab741a9f4a9bb8(ContactSet cfc73391dcf53c37de15d54e2434d0199, bool cbd5f4ffb7f6caa84b20b23ef3ac88b62, int ceb3d8673e21ca555c3fff5f70ca388ad, CollisionQueryType cb76a1558f3778afde9d9fa562d4ee745, ContactSet c1627d7366307c41303377e394f0fa017, CollisionObject c8d3ce58b7b825133e39107118502f05a, c107cf2db36674a478eaf84e50d31337e c7c689a518e7002631d5d29f0d18011e9)
	{
		CollisionObject collisionObject;
		if (!cbd5f4ffb7f6caa84b20b23ef3ac88b62)
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
			if (1 == 0)
			{
				__ldtoken(RayCompositeAlgorithm.cb144978528da5703ffab741a9f4a9bb8(ContactSet, bool, int, CollisionQueryType, ContactSet, CollisionObject, c107cf2db36674a478eaf84e50d31337e));
			}
			collisionObject = cfc73391dcf53c37de15d54e2434d0199.ObjectA;
		}
		else
		{
			collisionObject = cfc73391dcf53c37de15d54e2434d0199.ObjectB;
		}
		CollisionObject collisionObject2 = collisionObject;
		CollisionObject collisionObject3;
		if (!cbd5f4ffb7f6caa84b20b23ef3ac88b62)
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
			collisionObject3 = cfc73391dcf53c37de15d54e2434d0199.ObjectB;
		}
		else
		{
			collisionObject3 = cfc73391dcf53c37de15d54e2434d0199.ObjectA;
		}
		CollisionObject collisionObject4 = collisionObject3;
		IGeometricObject geometricObject = collisionObject2.GeometricObject;
		IGeometricObject geometricObject2 = collisionObject4.GeometricObject;
		Vector3F scale = geometricObject.Scale;
		IGeometricObject geometricObject3 = ((CompositeShape)geometricObject.Shape).Children[ceb3d8673e21ca555c3fff5f70ca388ad];
		CollisionAlgorithm collisionAlgorithm = CollisionDetection.AlgorithmMatrix[geometricObject3, geometricObject2];
		Pose pose = geometricObject3.Pose;
		pose.Position *= scale;
		c7c689a518e7002631d5d29f0d18011e9.Pose = geometricObject.Pose * pose;
		c7c689a518e7002631d5d29f0d18011e9.Shape = geometricObject3.Shape;
		c7c689a518e7002631d5d29f0d18011e9.Scale = scale * geometricObject3.Scale;
		c8d3ce58b7b825133e39107118502f05a.ceabf44cab475c8ec76f26d6553011a78(collisionObject2, c7c689a518e7002631d5d29f0d18011e9);
		if (cbd5f4ffb7f6caa84b20b23ef3ac88b62)
		{
			c1627d7366307c41303377e394f0fa017.Reset(collisionObject4, c8d3ce58b7b825133e39107118502f05a);
		}
		else
		{
			c1627d7366307c41303377e394f0fa017.Reset(c8d3ce58b7b825133e39107118502f05a, collisionObject4);
		}
		if (cb76a1558f3778afde9d9fa562d4ee745 == CollisionQueryType.Boolean)
		{
			collisionAlgorithm.ComputeCollision(c1627d7366307c41303377e394f0fa017, CollisionQueryType.Boolean);
			int haveContact;
			if (!cfc73391dcf53c37de15d54e2434d0199.HaveContact)
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
				haveContact = (c1627d7366307c41303377e394f0fa017.HaveContact ? 1 : 0);
			}
			else
			{
				haveContact = 1;
			}
			cfc73391dcf53c37de15d54e2434d0199.HaveContact = (byte)haveContact != 0;
			return;
		}
		c1627d7366307c41303377e394f0fa017.IsPerturbationTestAllowed = false;
		int num;
		if (!cfc73391dcf53c37de15d54e2434d0199.HaveContact)
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
			num = (int)cb76a1558f3778afde9d9fa562d4ee745;
		}
		else
		{
			num = 1;
		}
		CollisionQueryType type = (CollisionQueryType)num;
		collisionAlgorithm.ComputeCollision(c1627d7366307c41303377e394f0fa017, type);
		cfc73391dcf53c37de15d54e2434d0199.HaveContact = cfc73391dcf53c37de15d54e2434d0199.HaveContact || c1627d7366307c41303377e394f0fa017.HaveContact;
		int count = c1627d7366307c41303377e394f0fa017.Count;
		for (int i = 0; i < count; i++)
		{
			Contact contact = c1627d7366307c41303377e394f0fa017[i];
			if (cbd5f4ffb7f6caa84b20b23ef3ac88b62)
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
				contact.PositionBLocal = pose.ToWorldPosition(contact.PositionBLocal);
				contact.FeatureB = ceb3d8673e21ca555c3fff5f70ca388ad;
			}
			else
			{
				contact.PositionALocal = pose.ToWorldPosition(contact.PositionALocal);
				contact.FeatureA = ceb3d8673e21ca555c3fff5f70ca388ad;
			}
		}
		c9ae4fc785e8cd5184185f6ee6d5a3853.c6aedb1aa24c5098d4ce5c849eb999287(cfc73391dcf53c37de15d54e2434d0199, c1627d7366307c41303377e394f0fa017, cb76a1558f3778afde9d9fa562d4ee745, CollisionDetection.ContactPositionTolerance);
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
	public override float GetTimeOfImpact(CollisionObject objectA, Pose targetPoseA, CollisionObject objectB, Pose targetPoseB, float allowedPenetration)
	{
		return c4b38c33f832e334dc4346170faa25f3c.GetTimeOfImpact(objectA, targetPoseA, objectB, targetPoseB, allowedPenetration);
	}

	[CompilerGenerated]
	private static bool c7917d0fa24e68f0e91d6caf4324b9300(IGeometricObject cec3b7e534d1dd4ef5c11099cf1a04b42)
	{
		return cec3b7e534d1dd4ef5c11099cf1a04b42.Pose.HasRotation;
	}
}
