using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using A;
using DigitalRune.Collections;
using DigitalRune.Geometry.Partitioning;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Collisions.Algorithms;

/// <summary>
///       Computes contact or closest-point information for <see cref="T:DigitalRune.Geometry.Shapes.CompositeShape" /> vs. any other 
///       <see cref="T:DigitalRune.Geometry.Shapes.Shape" />.
///       </summary>
public class CompositeShapeAlgorithm : CollisionAlgorithm
{
	private class c21a5460512652ac21db0a5bed9a5508d
	{
		public CompositeShapeAlgorithm cc5b0109eff96f91c3e8ea71cac49ebb7;

		public bool c0ce437d81b9e321f976ce43b3a118caa;

		public ContactSet cdaa7e85696579d3929b4007d71528264;

		public ContactSet ce8fe4f02b54149f02b6b103eb61ee46a;

		public CollisionObject c1cdf7973cd0ea83e1d5ead43c3542660;

		public c107cf2db36674a478eaf84e50d31337e c15f35ecef26809329de5b64d7ebf12e1;

		public CollisionObject cd4591031c45f717c90e0dd16de9877cc;

		public c107cf2db36674a478eaf84e50d31337e c7124c30f3350e2e0afcd0898b7ba2f0d;

		public float c7dee4a4bea9a7102dd7844ba3cca1e3c(int ceb3d8673e21ca555c3fff5f70ca388ad)
		{
			cc5b0109eff96f91c3e8ea71cac49ebb7.cb144978528da5703ffab741a9f4a9bb8(cdaa7e85696579d3929b4007d71528264, c0ce437d81b9e321f976ce43b3a118caa, ceb3d8673e21ca555c3fff5f70ca388ad, CollisionQueryType.ClosestPoints, ce8fe4f02b54149f02b6b103eb61ee46a, c1cdf7973cd0ea83e1d5ead43c3542660, c15f35ecef26809329de5b64d7ebf12e1);
			return c427a5f6f67566c2dd6498fbf06616ed9();
		}

		public float c7dee4a4bea9a7102dd7844ba3cca1e3c(int cf53c8d691780da94eed9938488bb832e, int c5b872e494f2bcc2bf71ea1c564ad0987)
		{
			cc5b0109eff96f91c3e8ea71cac49ebb7.cb5374e9391c732548d9275f22dbd7d3d(cdaa7e85696579d3929b4007d71528264, cf53c8d691780da94eed9938488bb832e, c5b872e494f2bcc2bf71ea1c564ad0987, CollisionQueryType.ClosestPoints, ce8fe4f02b54149f02b6b103eb61ee46a, c1cdf7973cd0ea83e1d5ead43c3542660, c15f35ecef26809329de5b64d7ebf12e1, cd4591031c45f717c90e0dd16de9877cc, c7124c30f3350e2e0afcd0898b7ba2f0d);
			return c427a5f6f67566c2dd6498fbf06616ed9();
		}

		private float c427a5f6f67566c2dd6498fbf06616ed9()
		{
			if (cdaa7e85696579d3929b4007d71528264.HaveContact)
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
							__ldtoken(c21a5460512652ac21db0a5bed9a5508d.c427a5f6f67566c2dd6498fbf06616ed9());
						}
						return 0f;
					}
				}
			}
			if (cdaa7e85696579d3929b4007d71528264.Count > 0)
			{
				float penetrationDepth = cdaa7e85696579d3929b4007d71528264[cdaa7e85696579d3929b4007d71528264.Count - 1].PenetrationDepth;
				return penetrationDepth * penetrationDepth;
			}
			return float.PositiveInfinity;
		}
	}

	private static readonly ResourcePool<c21a5460512652ac21db0a5bed9a5508d> ce3416855d494370018a0f8b71b6eac9c;

	[CompilerGenerated]
	private static Func<IGeometricObject, bool> c2cd722d7de416d829aaf20a315c6553b;

	[CompilerGenerated]
	private static Func<IGeometricObject, bool> c19ae5197e8f1d77c5b9a5a5a3cba33d3;

	[CompilerGenerated]
	private static Func<c21a5460512652ac21db0a5bed9a5508d> c83ce51cca2c9a5b9ef62a19a0014722a;

	[CompilerGenerated]
	private static Action<c21a5460512652ac21db0a5bed9a5508d> c56450011eff7620c267636fce2acb8c4;

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Collisions.Algorithms.CompositeShapeAlgorithm" /> class.
	///       </summary>
	/// <param name="collisionDetection">The collision detection service.</param>
	public CompositeShapeAlgorithm(CollisionDetection collisionDetection)
		: base(collisionDetection)
	{
	}

	static CompositeShapeAlgorithm()
	{
		if (c83ce51cca2c9a5b9ef62a19a0014722a == null)
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
				__ldtoken(CompositeShapeAlgorithm._002Ecctor());
			}
			c83ce51cca2c9a5b9ef62a19a0014722a = () => new c21a5460512652ac21db0a5bed9a5508d();
		}
		Func<c21a5460512652ac21db0a5bed9a5508d> create = c83ce51cca2c9a5b9ef62a19a0014722a;
		if (c56450011eff7620c267636fce2acb8c4 == null)
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
			c56450011eff7620c267636fce2acb8c4 = (c21a5460512652ac21db0a5bed9a5508d ce4b7628f9880cef87f99eae50a8132fe) =>
			{
				ce4b7628f9880cef87f99eae50a8132fe.cc5b0109eff96f91c3e8ea71cac49ebb7 = null;
				ce4b7628f9880cef87f99eae50a8132fe.cdaa7e85696579d3929b4007d71528264 = null;
				ce4b7628f9880cef87f99eae50a8132fe.c0ce437d81b9e321f976ce43b3a118caa = false;
				ce4b7628f9880cef87f99eae50a8132fe.ce8fe4f02b54149f02b6b103eb61ee46a = null;
				ce4b7628f9880cef87f99eae50a8132fe.c15f35ecef26809329de5b64d7ebf12e1 = null;
				ce4b7628f9880cef87f99eae50a8132fe.c7124c30f3350e2e0afcd0898b7ba2f0d = null;
			};
		}
		ce3416855d494370018a0f8b71b6eac9c = new ResourcePool<c21a5460512652ac21db0a5bed9a5508d>(create, null, c56450011eff7620c267636fce2acb8c4);
	}

	/// <summary>
	///       Computes the collision. - This method should only be used by 
	///       <see cref="T:DigitalRune.Geometry.Collisions.Algorithms.CollisionAlgorithm" /> instances!
	///       </summary>
	/// <param name="contactSet">The contact set.</param>
	/// <param name="type">The type of collision query.</param>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="contactSet" /> does not contain a <see cref="T:DigitalRune.Geometry.Shapes.CompositeShape" />.
	///       </exception>
	/// <exception cref="T:System.NotSupportedException">
	///   <paramref name="contactSet" /> contains a <see cref="T:DigitalRune.Geometry.Shapes.CompositeShape" /> with a non-uniform
	///       scaling. One of its children has a local rotation. Computing collisions for composite shapes 
	///       with non-uniform scaling and rotated children is not supported.
	///       </exception>
	public override void ComputeCollision(ContactSet contactSet, CollisionQueryType type)
	{
		CollisionObject obj = contactSet.ObjectA;
		CollisionObject obj2 = contactSet.ObjectB;
		IGeometricObject obj3 = obj.GeometricObject;
		IGeometricObject obj4 = obj2.GeometricObject;
		CompositeShape obj5 = obj3.Shape as CompositeShape;
		CompositeShape obj6 = obj4.Shape as CompositeShape;
		bool flag = false;
		if (obj5 == null)
		{
			flag = true;
		}
		else if (obj5.Partition == null && obj6 != null)
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
			if (1 == 0)
			{
				__ldtoken(CompositeShapeAlgorithm.ComputeCollision(ContactSet, CollisionQueryType));
			}
			if (obj6.Partition != null)
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
				flag = true;
			}
		}
		if (flag)
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
			MathHelper.Swap(ref obj, ref obj2);
			MathHelper.Swap(ref obj3, ref obj4);
			MathHelper.Swap(ref obj5, ref obj6);
		}
		if (obj5 == null)
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6591), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1826));
		}
		contactSet.HaveContact = false;
		Vector3F scale = obj3.Scale;
		Vector3F scale2 = obj4.Scale;
		if (obj5 != null)
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
			if (scale.X == scale.Y)
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
				if (scale.Y == scale.Z)
				{
					goto IL_0170;
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
			if (obj5.Children.Any((IGeometricObject cec3b7e534d1dd4ef5c11099cf1a04b42) => cec3b7e534d1dd4ef5c11099cf1a04b42.Pose.HasRotation))
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
		goto IL_0170;
		IL_0170:
		if (obj6 != null)
		{
			if (scale2.X == scale2.Y)
			{
				if (scale2.Y == scale2.Z)
				{
					goto IL_01dd;
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
			if (obj6.Children.Any((IGeometricObject cec3b7e534d1dd4ef5c11099cf1a04b42) => cec3b7e534d1dd4ef5c11099cf1a04b42.Pose.HasRotation))
			{
				throw new NotSupportedException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6686));
			}
		}
		goto IL_01dd;
		IL_01dd:
		CollisionObject collisionObject = cdb80ee76c96fc0751dea8d15ee69fa47.c29a5464f174eae366728cbb2edc64776.Obtain();
		CollisionObject collisionObject2 = cdb80ee76c96fc0751dea8d15ee69fa47.c29a5464f174eae366728cbb2edc64776.Obtain();
		ContactSet contactSet2 = ContactSet.Create(collisionObject, collisionObject2);
		c107cf2db36674a478eaf84e50d31337e c107cf2db36674a478eaf84e50d31337e2 = c107cf2db36674a478eaf84e50d31337e.c2d8ce8b1ccf2b905e3b18f8a5145fe8a();
		c107cf2db36674a478eaf84e50d31337e c107cf2db36674a478eaf84e50d31337e3 = c107cf2db36674a478eaf84e50d31337e.c2d8ce8b1ccf2b905e3b18f8a5145fe8a();
		try
		{
			if (obj5.Partition != null)
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
				if (type == CollisionQueryType.ClosestPoints)
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
					if (!(obj5.Partition is ISupportClosestPointQueries<int>))
					{
						goto IL_04f4;
					}
				}
				if (obj6 != null)
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
					if (obj6.Partition != null)
					{
						if (type != CollisionQueryType.ClosestPoints)
						{
							IEnumerable<Pair<int>> overlaps = obj5.Partition.GetOverlaps(scale, obj3.Pose, obj6.Partition, scale2, obj4.Pose);
							using IEnumerator<Pair<int>> enumerator = overlaps.GetEnumerator();
							while (enumerator.MoveNext())
							{
								Pair<int> current = enumerator.Current;
								if (type != CollisionQueryType.Boolean || !contactSet.HaveContact)
								{
									cb5374e9391c732548d9275f22dbd7d3d(contactSet, current.First, current.Second, type, contactSet2, collisionObject, c107cf2db36674a478eaf84e50d31337e2, collisionObject2, c107cf2db36674a478eaf84e50d31337e3);
									continue;
								}
								return;
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
						c21a5460512652ac21db0a5bed9a5508d c21a5460512652ac21db0a5bed9a5508d2 = ce3416855d494370018a0f8b71b6eac9c.Obtain();
						c21a5460512652ac21db0a5bed9a5508d2.cc5b0109eff96f91c3e8ea71cac49ebb7 = this;
						c21a5460512652ac21db0a5bed9a5508d2.c0ce437d81b9e321f976ce43b3a118caa = false;
						c21a5460512652ac21db0a5bed9a5508d2.cdaa7e85696579d3929b4007d71528264 = contactSet;
						c21a5460512652ac21db0a5bed9a5508d2.c1cdf7973cd0ea83e1d5ead43c3542660 = collisionObject;
						c21a5460512652ac21db0a5bed9a5508d2.cd4591031c45f717c90e0dd16de9877cc = collisionObject2;
						c21a5460512652ac21db0a5bed9a5508d2.c15f35ecef26809329de5b64d7ebf12e1 = c107cf2db36674a478eaf84e50d31337e2;
						c21a5460512652ac21db0a5bed9a5508d2.c7124c30f3350e2e0afcd0898b7ba2f0d = c107cf2db36674a478eaf84e50d31337e3;
						c21a5460512652ac21db0a5bed9a5508d2.ce8fe4f02b54149f02b6b103eb61ee46a = contactSet2;
						((ISupportClosestPointQueries<int>)obj5.Partition).GetClosestPointCandidates(scale, obj3.Pose, obj6.Partition, scale2, obj4.Pose, c21a5460512652ac21db0a5bed9a5508d2.c7dee4a4bea9a7102dd7844ba3cca1e3c);
						ce3416855d494370018a0f8b71b6eac9c.Recycle(c21a5460512652ac21db0a5bed9a5508d2);
						return;
					}
				}
				Aabb aabb = obj4.Shape.GetAabb(scale2, obj3.Pose.Inverse * obj4.Pose);
				aabb.Scale(Vector3F.One / scale);
				if (type != CollisionQueryType.ClosestPoints)
				{
					while (true)
					{
						switch (3)
						{
						case 0:
							break;
						default:
						{
							using IEnumerator<int> enumerator2 = obj5.Partition.GetOverlaps(aabb).GetEnumerator();
							while (enumerator2.MoveNext())
							{
								int current2 = enumerator2.Current;
								if (type == CollisionQueryType.Boolean && contactSet.HaveContact)
								{
									return;
								}
								cb144978528da5703ffab741a9f4a9bb8(contactSet, flag, current2, type, contactSet2, collisionObject, c107cf2db36674a478eaf84e50d31337e2);
							}
							while (true)
							{
								switch (3)
								{
								case 0:
									break;
								default:
									return;
								}
							}
						}
						}
					}
				}
				if (type == CollisionQueryType.ClosestPoints)
				{
					c21a5460512652ac21db0a5bed9a5508d c21a5460512652ac21db0a5bed9a5508d3 = ce3416855d494370018a0f8b71b6eac9c.Obtain();
					c21a5460512652ac21db0a5bed9a5508d3.cc5b0109eff96f91c3e8ea71cac49ebb7 = this;
					c21a5460512652ac21db0a5bed9a5508d3.c0ce437d81b9e321f976ce43b3a118caa = flag;
					c21a5460512652ac21db0a5bed9a5508d3.cdaa7e85696579d3929b4007d71528264 = contactSet;
					c21a5460512652ac21db0a5bed9a5508d3.c1cdf7973cd0ea83e1d5ead43c3542660 = collisionObject;
					c21a5460512652ac21db0a5bed9a5508d3.cd4591031c45f717c90e0dd16de9877cc = collisionObject2;
					c21a5460512652ac21db0a5bed9a5508d3.c15f35ecef26809329de5b64d7ebf12e1 = c107cf2db36674a478eaf84e50d31337e2;
					c21a5460512652ac21db0a5bed9a5508d3.c7124c30f3350e2e0afcd0898b7ba2f0d = c107cf2db36674a478eaf84e50d31337e3;
					c21a5460512652ac21db0a5bed9a5508d3.ce8fe4f02b54149f02b6b103eb61ee46a = contactSet2;
					((ISupportClosestPointQueries<int>)obj5.Partition).GetClosestPointCandidates(aabb, float.PositiveInfinity, c21a5460512652ac21db0a5bed9a5508d3.c7dee4a4bea9a7102dd7844ba3cca1e3c);
					ce3416855d494370018a0f8b71b6eac9c.Recycle(c21a5460512652ac21db0a5bed9a5508d3);
				}
				return;
			}
			goto IL_04f4;
			IL_04f4:
			Aabb aabb2 = obj4.Shape.GetAabb(scale2, obj3.Pose.Inverse * obj4.Pose);
			aabb2.Scale(Vector3F.One / scale);
			int count = obj5.Children.Count;
			int num = 0;
			while (true)
			{
				if (num < count)
				{
					IGeometricObject geometricObject = obj5.Children[num];
					if (type != CollisionQueryType.ClosestPoints || contactSet.HaveContact)
					{
						if (!GeometryHelper.HaveContact(aabb2, geometricObject.Shape.GetAabb(geometricObject.Scale, geometricObject.Pose)))
						{
							goto IL_05c4;
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
					cb144978528da5703ffab741a9f4a9bb8(contactSet, flag, num, type, contactSet2, collisionObject, c107cf2db36674a478eaf84e50d31337e2);
					if (contactSet.HaveContact)
					{
						if (type == CollisionQueryType.Boolean)
						{
							break;
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
					goto IL_05c4;
				}
				while (true)
				{
					switch (6)
					{
					case 0:
						break;
					default:
						return;
					}
				}
				IL_05c4:
				num++;
			}
		}
		finally
		{
			contactSet2.Recycle();
			cdb80ee76c96fc0751dea8d15ee69fa47.c29a5464f174eae366728cbb2edc64776.Recycle(collisionObject);
			cdb80ee76c96fc0751dea8d15ee69fa47.c29a5464f174eae366728cbb2edc64776.Recycle(collisionObject2);
			c107cf2db36674a478eaf84e50d31337e3.ca18b9b5faa9851d6355f617787bb5859();
			c107cf2db36674a478eaf84e50d31337e2.ca18b9b5faa9851d6355f617787bb5859();
		}
	}

	private void cb144978528da5703ffab741a9f4a9bb8(ContactSet cfc73391dcf53c37de15d54e2434d0199, bool cbd5f4ffb7f6caa84b20b23ef3ac88b62, int ceb3d8673e21ca555c3fff5f70ca388ad, CollisionQueryType cb76a1558f3778afde9d9fa562d4ee745, ContactSet c1627d7366307c41303377e394f0fa017, CollisionObject c8d3ce58b7b825133e39107118502f05a, c107cf2db36674a478eaf84e50d31337e c7c689a518e7002631d5d29f0d18011e9)
	{
		CollisionObject collisionObject = (cbd5f4ffb7f6caa84b20b23ef3ac88b62 ? cfc73391dcf53c37de15d54e2434d0199.ObjectB : cfc73391dcf53c37de15d54e2434d0199.ObjectA);
		CollisionObject collisionObject2 = (cbd5f4ffb7f6caa84b20b23ef3ac88b62 ? cfc73391dcf53c37de15d54e2434d0199.ObjectA : cfc73391dcf53c37de15d54e2434d0199.ObjectB);
		IGeometricObject geometricObject = collisionObject.GeometricObject;
		IGeometricObject geometricObject2 = collisionObject2.GeometricObject;
		Vector3F scale = geometricObject.Scale;
		IGeometricObject geometricObject3 = ((CompositeShape)geometricObject.Shape).Children[ceb3d8673e21ca555c3fff5f70ca388ad];
		CollisionAlgorithm collisionAlgorithm = CollisionDetection.AlgorithmMatrix[geometricObject3, geometricObject2];
		Pose pose = geometricObject3.Pose;
		pose.Position *= scale;
		c7c689a518e7002631d5d29f0d18011e9.Pose = geometricObject.Pose * pose;
		c7c689a518e7002631d5d29f0d18011e9.Shape = geometricObject3.Shape;
		c7c689a518e7002631d5d29f0d18011e9.Scale = scale * geometricObject3.Scale;
		c8d3ce58b7b825133e39107118502f05a.ceabf44cab475c8ec76f26d6553011a78(collisionObject, c7c689a518e7002631d5d29f0d18011e9);
		if (cbd5f4ffb7f6caa84b20b23ef3ac88b62)
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
				__ldtoken(CompositeShapeAlgorithm.cb144978528da5703ffab741a9f4a9bb8(ContactSet, bool, int, CollisionQueryType, ContactSet, CollisionObject, c107cf2db36674a478eaf84e50d31337e));
			}
			c1627d7366307c41303377e394f0fa017.Reset(collisionObject2, c8d3ce58b7b825133e39107118502f05a);
		}
		else
		{
			c1627d7366307c41303377e394f0fa017.Reset(c8d3ce58b7b825133e39107118502f05a, collisionObject2);
		}
		if (cb76a1558f3778afde9d9fa562d4ee745 == CollisionQueryType.Boolean)
		{
			collisionAlgorithm.ComputeCollision(c1627d7366307c41303377e394f0fa017, CollisionQueryType.Boolean);
			cfc73391dcf53c37de15d54e2434d0199.HaveContact = cfc73391dcf53c37de15d54e2434d0199.HaveContact || c1627d7366307c41303377e394f0fa017.HaveContact;
			return;
		}
		c1627d7366307c41303377e394f0fa017.IsPerturbationTestAllowed = false;
		int num;
		if (!cfc73391dcf53c37de15d54e2434d0199.HaveContact)
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
			num = (int)cb76a1558f3778afde9d9fa562d4ee745;
		}
		else
		{
			num = 1;
		}
		CollisionQueryType type = (CollisionQueryType)num;
		collisionAlgorithm.ComputeCollision(c1627d7366307c41303377e394f0fa017, type);
		int haveContact;
		if (!cfc73391dcf53c37de15d54e2434d0199.HaveContact)
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
			haveContact = (c1627d7366307c41303377e394f0fa017.HaveContact ? 1 : 0);
		}
		else
		{
			haveContact = 1;
		}
		cfc73391dcf53c37de15d54e2434d0199.HaveContact = (byte)haveContact != 0;
		int count = c1627d7366307c41303377e394f0fa017.Count;
		for (int i = 0; i < count; i++)
		{
			Contact contact = c1627d7366307c41303377e394f0fa017[i];
			if (cbd5f4ffb7f6caa84b20b23ef3ac88b62)
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
				contact.PositionBLocal = pose.ToWorldPosition(contact.PositionBLocal);
				contact.FeatureB = ceb3d8673e21ca555c3fff5f70ca388ad;
			}
			else
			{
				contact.PositionALocal = pose.ToWorldPosition(contact.PositionALocal);
				contact.FeatureA = ceb3d8673e21ca555c3fff5f70ca388ad;
			}
		}
		while (true)
		{
			switch (2)
			{
			case 0:
				continue;
			}
			c9ae4fc785e8cd5184185f6ee6d5a3853.c6aedb1aa24c5098d4ce5c849eb999287(cfc73391dcf53c37de15d54e2434d0199, c1627d7366307c41303377e394f0fa017, cb76a1558f3778afde9d9fa562d4ee745, CollisionDetection.ContactPositionTolerance);
			return;
		}
	}

	private void cb5374e9391c732548d9275f22dbd7d3d(ContactSet cfc73391dcf53c37de15d54e2434d0199, int cf53c8d691780da94eed9938488bb832e, int c5b872e494f2bcc2bf71ea1c564ad0987, CollisionQueryType cb76a1558f3778afde9d9fa562d4ee745, ContactSet c1627d7366307c41303377e394f0fa017, CollisionObject cdd27bbfb0fc80a7fc2f2ae5e3dd16baf, c107cf2db36674a478eaf84e50d31337e c3f050a80d3dedd980def6224b64cadde, CollisionObject c856aa57ed5367fe5c74c08b3ebd685d6, c107cf2db36674a478eaf84e50d31337e c0990781199a8d6091d996f3dc718b01f)
	{
		CollisionObject objectA = cfc73391dcf53c37de15d54e2434d0199.ObjectA;
		CollisionObject objectB = cfc73391dcf53c37de15d54e2434d0199.ObjectB;
		IGeometricObject geometricObject = objectA.GeometricObject;
		IGeometricObject geometricObject2 = objectB.GeometricObject;
		CompositeShape compositeShape = (CompositeShape)geometricObject.Shape;
		CompositeShape compositeShape2 = (CompositeShape)geometricObject2.Shape;
		Vector3F scale = geometricObject.Scale;
		Vector3F scale2 = geometricObject2.Scale;
		IGeometricObject geometricObject3 = compositeShape.Children[cf53c8d691780da94eed9938488bb832e];
		IGeometricObject geometricObject4 = compositeShape2.Children[c5b872e494f2bcc2bf71ea1c564ad0987];
		CollisionAlgorithm collisionAlgorithm = CollisionDetection.AlgorithmMatrix[geometricObject3, geometricObject4];
		Pose pose = geometricObject3.Pose;
		pose.Position *= scale;
		c3f050a80d3dedd980def6224b64cadde.Pose = geometricObject.Pose * pose;
		c3f050a80d3dedd980def6224b64cadde.Shape = geometricObject3.Shape;
		c3f050a80d3dedd980def6224b64cadde.Scale = scale * geometricObject3.Scale;
		cdd27bbfb0fc80a7fc2f2ae5e3dd16baf.ceabf44cab475c8ec76f26d6553011a78(objectA, c3f050a80d3dedd980def6224b64cadde);
		Pose pose2 = geometricObject4.Pose;
		pose2.Position *= scale2;
		c0990781199a8d6091d996f3dc718b01f.Pose = geometricObject2.Pose * pose2;
		c0990781199a8d6091d996f3dc718b01f.Shape = geometricObject4.Shape;
		c0990781199a8d6091d996f3dc718b01f.Scale = scale2 * geometricObject4.Scale;
		c856aa57ed5367fe5c74c08b3ebd685d6.ceabf44cab475c8ec76f26d6553011a78(objectB, c0990781199a8d6091d996f3dc718b01f);
		c1627d7366307c41303377e394f0fa017.Reset(cdd27bbfb0fc80a7fc2f2ae5e3dd16baf, c856aa57ed5367fe5c74c08b3ebd685d6);
		if (cb76a1558f3778afde9d9fa562d4ee745 == CollisionQueryType.Boolean)
		{
			while (true)
			{
				switch (5)
				{
				case 0:
					break;
				default:
				{
					if (1 == 0)
					{
						__ldtoken(CompositeShapeAlgorithm.cb5374e9391c732548d9275f22dbd7d3d(ContactSet, int, int, CollisionQueryType, ContactSet, CollisionObject, c107cf2db36674a478eaf84e50d31337e, CollisionObject, c107cf2db36674a478eaf84e50d31337e));
					}
					collisionAlgorithm.ComputeCollision(c1627d7366307c41303377e394f0fa017, CollisionQueryType.Boolean);
					int haveContact;
					if (!cfc73391dcf53c37de15d54e2434d0199.HaveContact)
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
						haveContact = (c1627d7366307c41303377e394f0fa017.HaveContact ? 1 : 0);
					}
					else
					{
						haveContact = 1;
					}
					cfc73391dcf53c37de15d54e2434d0199.HaveContact = (byte)haveContact != 0;
					return;
				}
				}
			}
		}
		c1627d7366307c41303377e394f0fa017.IsPerturbationTestAllowed = false;
		int num;
		if (!cfc73391dcf53c37de15d54e2434d0199.HaveContact)
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
			num = (int)cb76a1558f3778afde9d9fa562d4ee745;
		}
		else
		{
			num = 1;
		}
		CollisionQueryType type = (CollisionQueryType)num;
		collisionAlgorithm.ComputeCollision(c1627d7366307c41303377e394f0fa017, type);
		int haveContact2;
		if (!cfc73391dcf53c37de15d54e2434d0199.HaveContact)
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
			haveContact2 = (c1627d7366307c41303377e394f0fa017.HaveContact ? 1 : 0);
		}
		else
		{
			haveContact2 = 1;
		}
		cfc73391dcf53c37de15d54e2434d0199.HaveContact = (byte)haveContact2 != 0;
		int count = c1627d7366307c41303377e394f0fa017.Count;
		for (int i = 0; i < count; i++)
		{
			Contact contact = c1627d7366307c41303377e394f0fa017[i];
			contact.PositionALocal = pose.ToWorldPosition(contact.PositionALocal);
			contact.FeatureA = cf53c8d691780da94eed9938488bb832e;
			contact.PositionBLocal = pose2.ToWorldPosition(contact.PositionBLocal);
			contact.FeatureB = c5b872e494f2bcc2bf71ea1c564ad0987;
		}
		while (true)
		{
			switch (1)
			{
			case 0:
				continue;
			}
			c9ae4fc785e8cd5184185f6ee6d5a3853.c6aedb1aa24c5098d4ce5c849eb999287(cfc73391dcf53c37de15d54e2434d0199, c1627d7366307c41303377e394f0fa017, cb76a1558f3778afde9d9fa562d4ee745, CollisionDetection.ContactPositionTolerance);
			return;
		}
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
	/// <exception cref="T:System.ArgumentException">
	///       Neither <paramref name="objectA" /> nor <paramref name="objectB" /> is a 
	///       <see cref="T:DigitalRune.Geometry.Shapes.CompositeShape" />.
	///       </exception>
	public override float GetTimeOfImpact(CollisionObject objectA, Pose targetPoseA, CollisionObject objectB, Pose targetPoseB, float allowedPenetration)
	{
		if (!(objectB.GeometricObject.Shape is CompositeShape))
		{
			MathHelper.Swap(ref objectA, ref objectB);
			MathHelper.Swap(ref targetPoseA, ref targetPoseB);
		}
		if (!(objectB.GeometricObject.Shape is CompositeShape compositeShape))
		{
			while (true)
			{
				switch (7)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(CompositeShapeAlgorithm.GetTimeOfImpact(CollisionObject, Pose, CollisionObject, Pose, float));
					}
					throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6898));
				}
			}
		}
		IGeometricObject geometricObject = objectB.GeometricObject;
		Pose pose = geometricObject.Pose;
		Vector3F scale = geometricObject.Scale;
		float num = 1f;
		c107cf2db36674a478eaf84e50d31337e c107cf2db36674a478eaf84e50d31337e2 = c107cf2db36674a478eaf84e50d31337e.c2d8ce8b1ccf2b905e3b18f8a5145fe8a();
		CollisionObject collisionObject = cdb80ee76c96fc0751dea8d15ee69fa47.c29a5464f174eae366728cbb2edc64776.Obtain();
		int count = compositeShape.Children.Count;
		for (int i = 0; i < count; i++)
		{
			IGeometricObject geometricObject2 = compositeShape.Children[i];
			Pose pose2 = geometricObject2.Pose;
			pose2.Position *= scale;
			c107cf2db36674a478eaf84e50d31337e2.Shape = geometricObject2.Shape;
			c107cf2db36674a478eaf84e50d31337e2.Scale = scale * geometricObject2.Scale;
			c107cf2db36674a478eaf84e50d31337e2.Pose = pose * pose2;
			collisionObject.ceabf44cab475c8ec76f26d6553011a78(objectB, c107cf2db36674a478eaf84e50d31337e2);
			CollisionAlgorithm collisionAlgorithm = CollisionDetection.AlgorithmMatrix[objectA, collisionObject];
			float timeOfImpact = collisionAlgorithm.GetTimeOfImpact(objectA, targetPoseA, collisionObject, targetPoseB * pose2, allowedPenetration);
			num = Math.Min(num, timeOfImpact);
		}
		while (true)
		{
			switch (1)
			{
			case 0:
				continue;
			}
			cdb80ee76c96fc0751dea8d15ee69fa47.c29a5464f174eae366728cbb2edc64776.Recycle(collisionObject);
			c107cf2db36674a478eaf84e50d31337e2.ca18b9b5faa9851d6355f617787bb5859();
			return num;
		}
	}

	[CompilerGenerated]
	private static bool c7917d0fa24e68f0e91d6caf4324b9300(IGeometricObject cec3b7e534d1dd4ef5c11099cf1a04b42)
	{
		return cec3b7e534d1dd4ef5c11099cf1a04b42.Pose.HasRotation;
	}

	[CompilerGenerated]
	private static bool c6b9b0c017bad0c7209be9bb04ea8f70c(IGeometricObject cec3b7e534d1dd4ef5c11099cf1a04b42)
	{
		return cec3b7e534d1dd4ef5c11099cf1a04b42.Pose.HasRotation;
	}

	[CompilerGenerated]
	private static c21a5460512652ac21db0a5bed9a5508d cabc7d9463a3aca5199cf3ee0115097e1()
	{
		return new c21a5460512652ac21db0a5bed9a5508d();
	}

	[CompilerGenerated]
	private static void c7e37b87d8de7f0a82661edb092462f99(c21a5460512652ac21db0a5bed9a5508d ce4b7628f9880cef87f99eae50a8132fe)
	{
		ce4b7628f9880cef87f99eae50a8132fe.cc5b0109eff96f91c3e8ea71cac49ebb7 = null;
		ce4b7628f9880cef87f99eae50a8132fe.cdaa7e85696579d3929b4007d71528264 = null;
		ce4b7628f9880cef87f99eae50a8132fe.c0ce437d81b9e321f976ce43b3a118caa = false;
		ce4b7628f9880cef87f99eae50a8132fe.ce8fe4f02b54149f02b6b103eb61ee46a = null;
		ce4b7628f9880cef87f99eae50a8132fe.c15f35ecef26809329de5b64d7ebf12e1 = null;
		ce4b7628f9880cef87f99eae50a8132fe.c7124c30f3350e2e0afcd0898b7ba2f0d = null;
	}
}
