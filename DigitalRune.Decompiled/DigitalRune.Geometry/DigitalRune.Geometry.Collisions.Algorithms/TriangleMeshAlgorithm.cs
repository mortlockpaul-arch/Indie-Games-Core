using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using A;
using DigitalRune.Collections;
using DigitalRune.Geometry.Meshes;
using DigitalRune.Geometry.Partitioning;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Collisions.Algorithms;

/// <summary>
///       Computes contact or closest-point information for <see cref="T:DigitalRune.Geometry.Shapes.TriangleMeshShape" /> vs. any 
///       other <see cref="T:DigitalRune.Geometry.Shapes.Shape" />.
///       </summary>
public class TriangleMeshAlgorithm : CollisionAlgorithm
{
	private class c9befaade904d8c1cd1acca5ee28bfe1c
	{
		public TriangleMeshAlgorithm cc5b0109eff96f91c3e8ea71cac49ebb7;

		public bool c0ce437d81b9e321f976ce43b3a118caa;

		public ContactSet cdaa7e85696579d3929b4007d71528264;

		public ContactSet ce8fe4f02b54149f02b6b103eb61ee46a;

		public CollisionObject c1cdf7973cd0ea83e1d5ead43c3542660;

		public c107cf2db36674a478eaf84e50d31337e c15f35ecef26809329de5b64d7ebf12e1;

		public TriangleShape c471af9d3a3379490f80b95e79689249d;

		public CollisionObject cd4591031c45f717c90e0dd16de9877cc;

		public c107cf2db36674a478eaf84e50d31337e c7124c30f3350e2e0afcd0898b7ba2f0d;

		public TriangleShape cd26f25512b9708f7257be18890c505a3;

		public float c7dee4a4bea9a7102dd7844ba3cca1e3c(int c770ce7a50191007b3e32d6b7a6e43051)
		{
			cc5b0109eff96f91c3e8ea71cac49ebb7.c9cd384ae2700ec55718aa6d97ce29739(cdaa7e85696579d3929b4007d71528264, c0ce437d81b9e321f976ce43b3a118caa, c770ce7a50191007b3e32d6b7a6e43051, CollisionQueryType.ClosestPoints, ce8fe4f02b54149f02b6b103eb61ee46a, c1cdf7973cd0ea83e1d5ead43c3542660, c15f35ecef26809329de5b64d7ebf12e1, c471af9d3a3379490f80b95e79689249d);
			return c427a5f6f67566c2dd6498fbf06616ed9();
		}

		public float c7dee4a4bea9a7102dd7844ba3cca1e3c(int c355cfe32ba0a3d5e720074f92d4ef42b, int cf578709c79e5bd453aa6f7b148b9245b)
		{
			cc5b0109eff96f91c3e8ea71cac49ebb7.cf5871a86be38372180f9b0b034bc995c(cdaa7e85696579d3929b4007d71528264, c355cfe32ba0a3d5e720074f92d4ef42b, cf578709c79e5bd453aa6f7b148b9245b, CollisionQueryType.ClosestPoints, ce8fe4f02b54149f02b6b103eb61ee46a, c1cdf7973cd0ea83e1d5ead43c3542660, c15f35ecef26809329de5b64d7ebf12e1, c471af9d3a3379490f80b95e79689249d, cd4591031c45f717c90e0dd16de9877cc, c7124c30f3350e2e0afcd0898b7ba2f0d, cd26f25512b9708f7257be18890c505a3);
			return c427a5f6f67566c2dd6498fbf06616ed9();
		}

		private float c427a5f6f67566c2dd6498fbf06616ed9()
		{
			if (cdaa7e85696579d3929b4007d71528264.HaveContact)
			{
				while (true)
				{
					switch (3)
					{
					case 0:
						break;
					default:
						if (1 == 0)
						{
							__ldtoken(c9befaade904d8c1cd1acca5ee28bfe1c.c427a5f6f67566c2dd6498fbf06616ed9());
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

	/// <summary>
	///       This value determines when contact welding should be performed.
	///       </summary>
	public static float WeldingLimit = 0.99f;

	private static readonly ResourcePool<c9befaade904d8c1cd1acca5ee28bfe1c> c5bd33de690062dc7c25dffc2027eeec9 = new ResourcePool<c9befaade904d8c1cd1acca5ee28bfe1c>(() => new c9befaade904d8c1cd1acca5ee28bfe1c(), null, (c9befaade904d8c1cd1acca5ee28bfe1c ce4b7628f9880cef87f99eae50a8132fe) =>
	{
		ce4b7628f9880cef87f99eae50a8132fe.cc5b0109eff96f91c3e8ea71cac49ebb7 = null;
		ce4b7628f9880cef87f99eae50a8132fe.cdaa7e85696579d3929b4007d71528264 = null;
		ce4b7628f9880cef87f99eae50a8132fe.c0ce437d81b9e321f976ce43b3a118caa = false;
		ce4b7628f9880cef87f99eae50a8132fe.ce8fe4f02b54149f02b6b103eb61ee46a = null;
		ce4b7628f9880cef87f99eae50a8132fe.c15f35ecef26809329de5b64d7ebf12e1 = null;
		ce4b7628f9880cef87f99eae50a8132fe.c7124c30f3350e2e0afcd0898b7ba2f0d = null;
		ce4b7628f9880cef87f99eae50a8132fe.c471af9d3a3379490f80b95e79689249d = null;
		ce4b7628f9880cef87f99eae50a8132fe.cd26f25512b9708f7257be18890c505a3 = null;
	});

	private CollisionAlgorithm c71b56267c9b9d1093856224a4b92be5c;

	[CompilerGenerated]
	private static Func<c9befaade904d8c1cd1acca5ee28bfe1c> c2cd722d7de416d829aaf20a315c6553b;

	[CompilerGenerated]
	private static Action<c9befaade904d8c1cd1acca5ee28bfe1c> c19ae5197e8f1d77c5b9a5a5a3cba33d3;

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Collisions.Algorithms.TriangleMeshAlgorithm" /> class.
	///       </summary>
	/// <param name="collisionDetection">The collision detection service.</param>
	public TriangleMeshAlgorithm(CollisionDetection collisionDetection)
		: base(collisionDetection)
	{
	}

	/// <summary>
	///       Computes the collision. - This method should only be used by 
	///       <see cref="T:DigitalRune.Geometry.Collisions.Algorithms.CollisionAlgorithm" /> instances!
	///       </summary>
	/// <param name="contactSet">The contact set.</param>
	/// <param name="type">The type of collision query.</param>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="contactSet" /> does not contain a <see cref="T:DigitalRune.Geometry.Shapes.TriangleMeshShape" />.
	///       </exception>
	public override void ComputeCollision(ContactSet contactSet, CollisionQueryType type)
	{
		CollisionObject obj = contactSet.ObjectA;
		CollisionObject obj2 = contactSet.ObjectB;
		IGeometricObject obj3 = obj.GeometricObject;
		IGeometricObject obj4 = obj2.GeometricObject;
		TriangleMeshShape obj5 = obj3.Shape as TriangleMeshShape;
		TriangleMeshShape obj6 = obj4.Shape as TriangleMeshShape;
		bool flag = false;
		if (obj5 == null)
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
				__ldtoken(TriangleMeshAlgorithm.ComputeCollision(ContactSet, CollisionQueryType));
			}
			if (obj6 == null)
			{
				throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6429), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1826));
			}
		}
		Pose obj7 = obj3.Pose;
		Pose obj8 = obj4.Pose;
		Vector3F obj9 = obj3.Scale;
		Vector3F obj10 = obj4.Scale;
		contactSet.HaveContact = false;
		CollisionObject collisionObject = cdb80ee76c96fc0751dea8d15ee69fa47.c29a5464f174eae366728cbb2edc64776.Obtain();
		CollisionObject collisionObject2 = cdb80ee76c96fc0751dea8d15ee69fa47.c29a5464f174eae366728cbb2edc64776.Obtain();
		ContactSet contactSet2 = ContactSet.Create(collisionObject, collisionObject2);
		c107cf2db36674a478eaf84e50d31337e c107cf2db36674a478eaf84e50d31337e2 = c107cf2db36674a478eaf84e50d31337e.c2d8ce8b1ccf2b905e3b18f8a5145fe8a();
		c107cf2db36674a478eaf84e50d31337e c107cf2db36674a478eaf84e50d31337e3 = c107cf2db36674a478eaf84e50d31337e.c2d8ce8b1ccf2b905e3b18f8a5145fe8a();
		TriangleShape triangleShape = cdb80ee76c96fc0751dea8d15ee69fa47.c2fabcdff37c34655c8dee4068556b530.Obtain();
		TriangleShape triangleShape2 = cdb80ee76c96fc0751dea8d15ee69fa47.c2fabcdff37c34655c8dee4068556b530.Obtain();
		try
		{
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
				if (obj5.Partition != null)
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
					if (obj6 != null && obj6.Partition != null)
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
						if (type == CollisionQueryType.ClosestPoints)
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
							if (!(obj5.Partition is ISupportClosestPointQueries<int>))
							{
								goto IL_02a9;
							}
						}
						if (type != CollisionQueryType.ClosestPoints)
						{
							IEnumerable<Pair<int>> overlaps = obj5.Partition.GetOverlaps(obj9, obj3.Pose, obj6.Partition, obj10, obj4.Pose);
							{
								foreach (Pair<int> item in overlaps)
								{
									if (type == CollisionQueryType.Boolean)
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
										if (contactSet.HaveContact)
										{
											break;
										}
									}
									cf5871a86be38372180f9b0b034bc995c(contactSet, item.First, item.Second, type, contactSet2, collisionObject, c107cf2db36674a478eaf84e50d31337e2, triangleShape, collisionObject2, c107cf2db36674a478eaf84e50d31337e3, triangleShape2);
								}
								return;
							}
						}
						c9befaade904d8c1cd1acca5ee28bfe1c c9befaade904d8c1cd1acca5ee28bfe1c2 = c5bd33de690062dc7c25dffc2027eeec9.Obtain();
						c9befaade904d8c1cd1acca5ee28bfe1c2.cc5b0109eff96f91c3e8ea71cac49ebb7 = this;
						c9befaade904d8c1cd1acca5ee28bfe1c2.c0ce437d81b9e321f976ce43b3a118caa = false;
						c9befaade904d8c1cd1acca5ee28bfe1c2.cdaa7e85696579d3929b4007d71528264 = contactSet;
						c9befaade904d8c1cd1acca5ee28bfe1c2.ce8fe4f02b54149f02b6b103eb61ee46a = contactSet2;
						c9befaade904d8c1cd1acca5ee28bfe1c2.c1cdf7973cd0ea83e1d5ead43c3542660 = collisionObject;
						c9befaade904d8c1cd1acca5ee28bfe1c2.cd4591031c45f717c90e0dd16de9877cc = collisionObject2;
						c9befaade904d8c1cd1acca5ee28bfe1c2.c15f35ecef26809329de5b64d7ebf12e1 = c107cf2db36674a478eaf84e50d31337e2;
						c9befaade904d8c1cd1acca5ee28bfe1c2.c7124c30f3350e2e0afcd0898b7ba2f0d = c107cf2db36674a478eaf84e50d31337e3;
						c9befaade904d8c1cd1acca5ee28bfe1c2.c471af9d3a3379490f80b95e79689249d = triangleShape;
						c9befaade904d8c1cd1acca5ee28bfe1c2.cd26f25512b9708f7257be18890c505a3 = triangleShape2;
						((ISupportClosestPointQueries<int>)obj5.Partition).GetClosestPointCandidates(obj9, obj3.Pose, obj6.Partition, obj10, obj4.Pose, c9befaade904d8c1cd1acca5ee28bfe1c2.c7dee4a4bea9a7102dd7844ba3cca1e3c);
						c5bd33de690062dc7c25dffc2027eeec9.Recycle(c9befaade904d8c1cd1acca5ee28bfe1c2);
						return;
					}
				}
			}
			goto IL_02a9;
			IL_02a9:
			Aabb obj11 = obj3.Aabb;
			Aabb obj12 = obj4.Aabb;
			float obj13 = obj11.Extent.LargestComponent;
			float obj14 = obj12.Extent.LargestComponent;
			if (obj5 == null)
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
				flag = true;
			}
			else if (obj6 != null)
			{
				if (obj5.Partition != null)
				{
					if (obj13 * 2f < obj14)
					{
						flag = true;
					}
				}
				else if (obj6.Partition != null)
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
					if (obj14 * 2f >= obj13)
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
						flag = true;
					}
				}
				else if (obj13 < obj14)
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
					flag = true;
				}
			}
			if (flag)
			{
				MathHelper.Swap(ref obj, ref obj2);
				MathHelper.Swap(ref obj3, ref obj4);
				MathHelper.Swap(ref obj11, ref obj12);
				MathHelper.Swap(ref obj13, ref obj14);
				MathHelper.Swap(ref obj5, ref obj6);
				MathHelper.Swap(ref obj7, ref obj8);
				MathHelper.Swap(ref obj9, ref obj10);
			}
			if (obj6 == null && type != CollisionQueryType.ClosestPoints)
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
				if (obj13 * 2f < obj14)
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
					BoxShape boxShape = cdb80ee76c96fc0751dea8d15ee69fa47.c98c3bafe0aef6f270c50db484f6cf2eb.Obtain();
					boxShape.Extent = obj11.Extent;
					c107cf2db36674a478eaf84e50d31337e2.Shape = boxShape;
					c107cf2db36674a478eaf84e50d31337e2.Scale = Vector3F.One;
					c107cf2db36674a478eaf84e50d31337e2.Pose = new Pose(obj11.Center);
					collisionObject.ceabf44cab475c8ec76f26d6553011a78(obj, c107cf2db36674a478eaf84e50d31337e2);
					contactSet2.Reset(collisionObject, obj2);
					CollisionAlgorithm collisionAlgorithm = CollisionDetection.AlgorithmMatrix[contactSet2];
					collisionAlgorithm.ComputeCollision(contactSet2, CollisionQueryType.Boolean);
					cdb80ee76c96fc0751dea8d15ee69fa47.c98c3bafe0aef6f270c50db484f6cf2eb.Recycle(boxShape);
					if (!contactSet2.HaveContact)
					{
						while (true)
						{
							switch (7)
							{
							case 0:
								break;
							default:
								contactSet.HaveContact = false;
								return;
							}
						}
					}
				}
			}
			if (obj5.Partition != null)
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
				if (type == CollisionQueryType.ClosestPoints)
				{
					if (!(obj5.Partition is ISupportClosestPointQueries<int>))
					{
						goto IL_0619;
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
				Aabb aabb = obj4.Shape.GetAabb(obj10, obj7.Inverse * obj8);
				aabb.Scale(Vector3F.One / obj9);
				if (type != CollisionQueryType.ClosestPoints)
				{
					while (true)
					{
						switch (5)
						{
						case 0:
							break;
						default:
						{
							using IEnumerator<int> enumerator2 = obj5.Partition.GetOverlaps(aabb).GetEnumerator();
							while (enumerator2.MoveNext())
							{
								int current2 = enumerator2.Current;
								if (type == CollisionQueryType.Boolean)
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
									if (contactSet.HaveContact)
									{
										return;
									}
									while (true)
									{
										switch (2)
										{
										case 0:
											continue;
										}
										break;
									}
								}
								c9cd384ae2700ec55718aa6d97ce29739(contactSet, flag, current2, type, contactSet2, collisionObject, c107cf2db36674a478eaf84e50d31337e2, triangleShape);
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
					}
				}
				if (type != CollisionQueryType.ClosestPoints)
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
					c9befaade904d8c1cd1acca5ee28bfe1c c9befaade904d8c1cd1acca5ee28bfe1c3 = c5bd33de690062dc7c25dffc2027eeec9.Obtain();
					c9befaade904d8c1cd1acca5ee28bfe1c3.cc5b0109eff96f91c3e8ea71cac49ebb7 = this;
					c9befaade904d8c1cd1acca5ee28bfe1c3.c0ce437d81b9e321f976ce43b3a118caa = flag;
					c9befaade904d8c1cd1acca5ee28bfe1c3.cdaa7e85696579d3929b4007d71528264 = contactSet;
					c9befaade904d8c1cd1acca5ee28bfe1c3.ce8fe4f02b54149f02b6b103eb61ee46a = contactSet2;
					c9befaade904d8c1cd1acca5ee28bfe1c3.c1cdf7973cd0ea83e1d5ead43c3542660 = collisionObject;
					c9befaade904d8c1cd1acca5ee28bfe1c3.cd4591031c45f717c90e0dd16de9877cc = collisionObject2;
					c9befaade904d8c1cd1acca5ee28bfe1c3.c15f35ecef26809329de5b64d7ebf12e1 = c107cf2db36674a478eaf84e50d31337e2;
					c9befaade904d8c1cd1acca5ee28bfe1c3.c7124c30f3350e2e0afcd0898b7ba2f0d = c107cf2db36674a478eaf84e50d31337e3;
					c9befaade904d8c1cd1acca5ee28bfe1c3.c471af9d3a3379490f80b95e79689249d = triangleShape;
					c9befaade904d8c1cd1acca5ee28bfe1c3.cd26f25512b9708f7257be18890c505a3 = triangleShape2;
					((ISupportClosestPointQueries<int>)obj5.Partition).GetClosestPointCandidates(aabb, float.PositiveInfinity, c9befaade904d8c1cd1acca5ee28bfe1c3.c7dee4a4bea9a7102dd7844ba3cca1e3c);
					c5bd33de690062dc7c25dffc2027eeec9.Recycle(c9befaade904d8c1cd1acca5ee28bfe1c3);
					return;
				}
			}
			goto IL_0619;
			IL_069a:
			float num;
			Vector3F minimum = obj12.Minimum - new Vector3F(num);
			Vector3F maximum = obj12.Maximum + new Vector3F(num);
			Aabb aabbA = new Aabb(minimum, maximum);
			ITriangleMesh mesh = obj5.Mesh;
			int numberOfTriangles = mesh.NumberOfTriangles;
			int num2 = 0;
			while (true)
			{
				if (num2 < numberOfTriangles)
				{
					Triangle triangle = mesh.GetTriangle(num2);
					triangleShape.Vertex0 = triangle.Vertex0 * obj9;
					triangleShape.Vertex1 = triangle.Vertex1 * obj9;
					triangleShape.Vertex2 = triangle.Vertex2 * obj9;
					if (!GeometryHelper.HaveContact(aabbA, triangleShape.GetAabb(obj7)))
					{
						goto IL_0843;
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
					c9cd384ae2700ec55718aa6d97ce29739(contactSet, flag, num2, type, contactSet2, collisionObject, c107cf2db36674a478eaf84e50d31337e2, triangleShape);
					if (contactSet.HaveContact)
					{
						if (type == CollisionQueryType.Boolean)
						{
							break;
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
					if (num > 0f)
					{
						if (contactSet.HaveContact)
						{
							goto IL_07d1;
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
					if (contactSet.Count <= 0)
					{
						goto IL_0843;
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
					if (!(0f - contactSet[contactSet.Count - 1].PenetrationDepth < num))
					{
						goto IL_0843;
					}
					goto IL_07d1;
				}
				while (true)
				{
					switch (2)
					{
					case 0:
						break;
					default:
						return;
					}
				}
				IL_07d1:
				num = ((contactSet.Count <= 0) ? 0f : Math.Max(0f, 0f - contactSet[contactSet.Count - 1].PenetrationDepth));
				aabbA.Minimum = obj12.Minimum - new Vector3F(num);
				aabbA.Maximum = obj12.Maximum + new Vector3F(num);
				goto IL_0843;
				IL_0843:
				num2++;
			}
			return;
			IL_0619:
			if (!contactSet.HaveContact)
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
				if (type == CollisionQueryType.ClosestPoints)
				{
					Vector3F vector3F = obj7.ToWorldPosition(obj3.Shape.InnerPoint * obj9);
					Vector3F vector3F2 = obj8.ToWorldPosition(obj4.Shape.InnerPoint * obj10);
					num = (vector3F2 - vector3F).Length + CollisionDetection.Epsilon;
					goto IL_069a;
				}
			}
			num = 0f;
			goto IL_069a;
		}
		finally
		{
			contactSet2.Recycle();
			cdb80ee76c96fc0751dea8d15ee69fa47.c29a5464f174eae366728cbb2edc64776.Recycle(collisionObject);
			cdb80ee76c96fc0751dea8d15ee69fa47.c29a5464f174eae366728cbb2edc64776.Recycle(collisionObject2);
			c107cf2db36674a478eaf84e50d31337e3.ca18b9b5faa9851d6355f617787bb5859();
			c107cf2db36674a478eaf84e50d31337e2.ca18b9b5faa9851d6355f617787bb5859();
			cdb80ee76c96fc0751dea8d15ee69fa47.c2fabcdff37c34655c8dee4068556b530.Recycle(triangleShape2);
			cdb80ee76c96fc0751dea8d15ee69fa47.c2fabcdff37c34655c8dee4068556b530.Recycle(triangleShape);
		}
	}

	private void c9cd384ae2700ec55718aa6d97ce29739(ContactSet cfc73391dcf53c37de15d54e2434d0199, bool cbd5f4ffb7f6caa84b20b23ef3ac88b62, int c770ce7a50191007b3e32d6b7a6e43051, CollisionQueryType cb76a1558f3778afde9d9fa562d4ee745, ContactSet c1627d7366307c41303377e394f0fa017, CollisionObject c8d3ce58b7b825133e39107118502f05a, c107cf2db36674a478eaf84e50d31337e c7c689a518e7002631d5d29f0d18011e9, TriangleShape c87da0923bb15db2ecdf504e04f63bc16)
	{
		CollisionObject collisionObject;
		if (!cbd5f4ffb7f6caa84b20b23ef3ac88b62)
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
				__ldtoken(TriangleMeshAlgorithm.c9cd384ae2700ec55718aa6d97ce29739(ContactSet, bool, int, CollisionQueryType, ContactSet, CollisionObject, c107cf2db36674a478eaf84e50d31337e, TriangleShape));
			}
			collisionObject = cfc73391dcf53c37de15d54e2434d0199.ObjectA;
		}
		else
		{
			collisionObject = cfc73391dcf53c37de15d54e2434d0199.ObjectB;
		}
		CollisionObject collisionObject2 = collisionObject;
		CollisionObject collisionObject3 = (cbd5f4ffb7f6caa84b20b23ef3ac88b62 ? cfc73391dcf53c37de15d54e2434d0199.ObjectA : cfc73391dcf53c37de15d54e2434d0199.ObjectB);
		IGeometricObject geometricObject = collisionObject2.GeometricObject;
		TriangleMeshShape triangleMeshShape = (TriangleMeshShape)geometricObject.Shape;
		Triangle triangle = triangleMeshShape.Mesh.GetTriangle(c770ce7a50191007b3e32d6b7a6e43051);
		Pose pose = geometricObject.Pose;
		Vector3F scale = geometricObject.Scale;
		CollisionAlgorithm collisionAlgorithm = CollisionDetection.AlgorithmMatrix[typeof(TriangleShape), collisionObject3.GeometricObject.Shape.GetType()];
		c87da0923bb15db2ecdf504e04f63bc16.Vertex0 = triangle.Vertex0 * scale;
		c87da0923bb15db2ecdf504e04f63bc16.Vertex1 = triangle.Vertex1 * scale;
		c87da0923bb15db2ecdf504e04f63bc16.Vertex2 = triangle.Vertex2 * scale;
		c7c689a518e7002631d5d29f0d18011e9.Shape = c87da0923bb15db2ecdf504e04f63bc16;
		c7c689a518e7002631d5d29f0d18011e9.Scale = Vector3F.One;
		c7c689a518e7002631d5d29f0d18011e9.Pose = pose;
		c8d3ce58b7b825133e39107118502f05a.ceabf44cab475c8ec76f26d6553011a78(collisionObject2, c7c689a518e7002631d5d29f0d18011e9);
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
			c1627d7366307c41303377e394f0fa017.Reset(collisionObject3, c8d3ce58b7b825133e39107118502f05a);
		}
		else
		{
			c1627d7366307c41303377e394f0fa017.Reset(c8d3ce58b7b825133e39107118502f05a, collisionObject3);
		}
		if (cb76a1558f3778afde9d9fa562d4ee745 == CollisionQueryType.Boolean)
		{
			while (true)
			{
				switch (2)
				{
				case 0:
					continue;
				}
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
		}
		c1627d7366307c41303377e394f0fa017.IsPerturbationTestAllowed = false;
		CollisionQueryType type = (cfc73391dcf53c37de15d54e2434d0199.HaveContact ? CollisionQueryType.Contacts : cb76a1558f3778afde9d9fa562d4ee745);
		collisionAlgorithm.ComputeCollision(c1627d7366307c41303377e394f0fa017, type);
		int haveContact2;
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
			haveContact2 = (c1627d7366307c41303377e394f0fa017.HaveContact ? 1 : 0);
		}
		else
		{
			haveContact2 = 1;
		}
		cfc73391dcf53c37de15d54e2434d0199.HaveContact = (byte)haveContact2 != 0;
		Vector3F vector3F3;
		float num;
		Vector3F vector3F5;
		float num2;
		if (c1627d7366307c41303377e394f0fa017.HaveContact && c1627d7366307c41303377e394f0fa017.Count > 0)
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
			if (!triangleMeshShape.IsTwoSided)
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
				Vector3F localDirection = Vector3F.Cross(triangle.Vertex1 - triangle.Vertex0, triangle.Vertex2 - triangle.Vertex0) / scale;
				Vector3F vector3F = pose.ToWorldDirection(localDirection);
				if (vector3F.TryNormalize())
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
					Vector3F vector3F2;
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
						vector3F2 = vector3F;
					}
					else
					{
						vector3F2 = -vector3F;
					}
					vector3F3 = vector3F2;
					c9ae4fc785e8cd5184185f6ee6d5a3853.cd4808b4cca49e2d5bdc52072c95767d2(c1627d7366307c41303377e394f0fa017, vector3F3, 0f - Numeric.EpsilonF);
					if (c1627d7366307c41303377e394f0fa017.Count > 0)
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
						if (triangleMeshShape.EnableContactWelding)
						{
							num = Vector3F.Dot(c1627d7366307c41303377e394f0fa017[0].Normal, vector3F3);
							if (num < WeldingLimit)
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
								Vector3F vector3F4;
								if (!cbd5f4ffb7f6caa84b20b23ef3ac88b62)
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
									vector3F4 = c1627d7366307c41303377e394f0fa017[0].PositionALocal / scale;
								}
								else
								{
									vector3F4 = c1627d7366307c41303377e394f0fa017[0].PositionBLocal / scale;
								}
								Vector3F point = vector3F4;
								GeometryHelper.GetBarycentricFromPoint(triangle, point, out var u, out var v, out var w);
								vector3F5 = vector3F;
								num2 = float.MaxValue;
								if (!(u < 0.05f) && !(v < 0.05f))
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
									if (!(w < 0.05f))
									{
										goto IL_04f6;
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
								if (u < 0.05f)
								{
									int num3 = triangleMeshShape.TriangleNeighbors[c770ce7a50191007b3e32d6b7a6e43051 * 3];
									if (num3 >= 0)
									{
										Vector3F vector3F6 = pose.ToWorldDirection(triangleMeshShape.Mesh.GetTriangle(num3).Normal / scale);
										if (vector3F6.TryNormalize())
										{
											float num4 = Vector3F.Dot(vector3F, vector3F6);
											if (num4 < num2)
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
												num2 = num4;
												vector3F5 = vector3F6;
											}
										}
									}
								}
								if (v < 0.05f)
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
									int num5 = triangleMeshShape.TriangleNeighbors[c770ce7a50191007b3e32d6b7a6e43051 * 3 + 1];
									if (num5 >= 0)
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
										Vector3F vector3F7 = pose.ToWorldDirection(triangleMeshShape.Mesh.GetTriangle(num5).Normal / scale);
										if (vector3F7.TryNormalize())
										{
											float num6 = Vector3F.Dot(vector3F, vector3F7);
											if (num6 < num2)
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
												num2 = num6;
												vector3F5 = vector3F7;
											}
										}
									}
								}
								if (w < 0.05f)
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
									int num7 = triangleMeshShape.TriangleNeighbors[c770ce7a50191007b3e32d6b7a6e43051 * 3 + 2];
									if (num7 >= 0)
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
										Vector3F vector3F8 = pose.ToWorldDirection(triangleMeshShape.Mesh.GetTriangle(num7).Normal / scale);
										if (vector3F8.TryNormalize())
										{
											float num8 = Vector3F.Dot(vector3F, vector3F8);
											if (num8 < num2)
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
												num2 = num8;
												vector3F5 = vector3F8;
											}
										}
									}
								}
								goto IL_04f6;
							}
						}
					}
				}
			}
		}
		goto IL_0738;
		IL_0609:
		Contact contact2;
		Contact contact = contact2;
		float num9;
		num = num9;
		c1627d7366307c41303377e394f0fa017[0] = contact;
		goto IL_061b;
		IL_061b:
		if (Numeric.IsLess(num, num2))
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
				Vector3F positionAWorld = contact.PositionAWorld;
				contact.Normal = vector3F5;
				Vector3F vector3F9 = positionAWorld - contact.Normal * contact.PenetrationDepth;
				contact.Position = (positionAWorld + vector3F9) / 2f;
				contact.PositionBLocal = c1627d7366307c41303377e394f0fa017.ObjectB.GeometricObject.Pose.ToLocalPosition(vector3F9);
			}
			else
			{
				Vector3F positionBWorld = contact.PositionBWorld;
				contact.Normal = -vector3F5;
				Vector3F vector3F10 = positionBWorld + contact.Normal * contact.PenetrationDepth;
				contact.Position = (vector3F10 + positionBWorld) / 2f;
				contact.PositionALocal = c1627d7366307c41303377e394f0fa017.ObjectA.GeometricObject.Pose.ToLocalPosition(vector3F10);
			}
		}
		goto IL_0738;
		IL_0738:
		if (c1627d7366307c41303377e394f0fa017.Count <= 0)
		{
			return;
		}
		while (true)
		{
			switch (2)
			{
			case 0:
				continue;
			}
			int count = c1627d7366307c41303377e394f0fa017.Count;
			for (int i = 0; i < count; i++)
			{
				Contact contact3 = c1627d7366307c41303377e394f0fa017[i];
				if (cbd5f4ffb7f6caa84b20b23ef3ac88b62)
				{
					contact3.FeatureB = c770ce7a50191007b3e32d6b7a6e43051;
				}
				else
				{
					contact3.FeatureA = c770ce7a50191007b3e32d6b7a6e43051;
				}
			}
			while (true)
			{
				switch (4)
				{
				case 0:
					continue;
				}
				c9ae4fc785e8cd5184185f6ee6d5a3853.c6aedb1aa24c5098d4ce5c849eb999287(cfc73391dcf53c37de15d54e2434d0199, c1627d7366307c41303377e394f0fa017, cb76a1558f3778afde9d9fa562d4ee745, CollisionDetection.ContactPositionTolerance);
				return;
			}
		}
		IL_04f6:
		contact = c1627d7366307c41303377e394f0fa017[0];
		if (num2 < float.MaxValue)
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
			if (Numeric.IsLess(num, num2))
			{
				foreach (Contact item in c1627d7366307c41303377e394f0fa017)
				{
					item.Recycle();
				}
				c1627d7366307c41303377e394f0fa017.Clear();
				c1627d7366307c41303377e394f0fa017.c196da5f647797a182f2b6d32727c7621 = vector3F3;
				collisionAlgorithm.ComputeCollision(c1627d7366307c41303377e394f0fa017, type);
				c1627d7366307c41303377e394f0fa017.c196da5f647797a182f2b6d32727c7621 = Vector3F.Zero;
				if (c1627d7366307c41303377e394f0fa017.Count > 0)
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
					contact2 = c1627d7366307c41303377e394f0fa017[0];
					num9 = Vector3F.Dot(contact2.Normal, vector3F3);
					if (contact2.PenetrationDepth < contact.PenetrationDepth)
					{
						goto IL_0609;
					}
					while (true)
					{
						switch (2)
						{
						case 0:
							continue;
						}
						break;
					}
					if (Numeric.IsGreaterOrEqual(num9, num2))
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
						if (contact2.PenetrationDepth < contact.PenetrationDepth + CollisionDetection.ContactPositionTolerance)
						{
							goto IL_0609;
						}
					}
				}
				goto IL_061b;
			}
		}
		goto IL_0738;
	}

	private void cf5871a86be38372180f9b0b034bc995c(ContactSet cfc73391dcf53c37de15d54e2434d0199, int c355cfe32ba0a3d5e720074f92d4ef42b, int cf578709c79e5bd453aa6f7b148b9245b, CollisionQueryType cb76a1558f3778afde9d9fa562d4ee745, ContactSet c1627d7366307c41303377e394f0fa017, CollisionObject cdd27bbfb0fc80a7fc2f2ae5e3dd16baf, c107cf2db36674a478eaf84e50d31337e c3f050a80d3dedd980def6224b64cadde, TriangleShape c2ea074299508fc69f3cf4a0aceed9832, CollisionObject c856aa57ed5367fe5c74c08b3ebd685d6, c107cf2db36674a478eaf84e50d31337e c0990781199a8d6091d996f3dc718b01f, TriangleShape c9a828ede27e753a3abac806bf177e68b)
	{
		CollisionObject objectA = cfc73391dcf53c37de15d54e2434d0199.ObjectA;
		CollisionObject objectB = cfc73391dcf53c37de15d54e2434d0199.ObjectB;
		IGeometricObject geometricObject = objectA.GeometricObject;
		IGeometricObject geometricObject2 = objectB.GeometricObject;
		TriangleMeshShape triangleMeshShape = (TriangleMeshShape)geometricObject.Shape;
		Triangle triangle = triangleMeshShape.Mesh.GetTriangle(c355cfe32ba0a3d5e720074f92d4ef42b);
		TriangleMeshShape triangleMeshShape2 = (TriangleMeshShape)geometricObject2.Shape;
		Triangle triangle2 = triangleMeshShape2.Mesh.GetTriangle(cf578709c79e5bd453aa6f7b148b9245b);
		Pose pose = geometricObject.Pose;
		Pose pose2 = geometricObject2.Pose;
		Vector3F scale = geometricObject.Scale;
		Vector3F scale2 = geometricObject2.Scale;
		if (c71b56267c9b9d1093856224a4b92be5c == null)
		{
			c71b56267c9b9d1093856224a4b92be5c = CollisionDetection.AlgorithmMatrix[typeof(TriangleShape), typeof(TriangleShape)];
		}
		c2ea074299508fc69f3cf4a0aceed9832.Vertex0 = triangle.Vertex0 * scale;
		c2ea074299508fc69f3cf4a0aceed9832.Vertex1 = triangle.Vertex1 * scale;
		c2ea074299508fc69f3cf4a0aceed9832.Vertex2 = triangle.Vertex2 * scale;
		c9a828ede27e753a3abac806bf177e68b.Vertex0 = triangle2.Vertex0 * scale2;
		c9a828ede27e753a3abac806bf177e68b.Vertex1 = triangle2.Vertex1 * scale2;
		c9a828ede27e753a3abac806bf177e68b.Vertex2 = triangle2.Vertex2 * scale2;
		c3f050a80d3dedd980def6224b64cadde.Shape = c2ea074299508fc69f3cf4a0aceed9832;
		c3f050a80d3dedd980def6224b64cadde.Scale = Vector3F.One;
		c3f050a80d3dedd980def6224b64cadde.Pose = pose;
		cdd27bbfb0fc80a7fc2f2ae5e3dd16baf.ceabf44cab475c8ec76f26d6553011a78(objectA, c3f050a80d3dedd980def6224b64cadde);
		c0990781199a8d6091d996f3dc718b01f.Shape = c9a828ede27e753a3abac806bf177e68b;
		c0990781199a8d6091d996f3dc718b01f.Scale = Vector3F.One;
		c0990781199a8d6091d996f3dc718b01f.Pose = pose2;
		c856aa57ed5367fe5c74c08b3ebd685d6.ceabf44cab475c8ec76f26d6553011a78(objectB, c0990781199a8d6091d996f3dc718b01f);
		c1627d7366307c41303377e394f0fa017.Reset(cdd27bbfb0fc80a7fc2f2ae5e3dd16baf, c856aa57ed5367fe5c74c08b3ebd685d6);
		if (cb76a1558f3778afde9d9fa562d4ee745 == CollisionQueryType.Boolean)
		{
			c71b56267c9b9d1093856224a4b92be5c.ComputeCollision(c1627d7366307c41303377e394f0fa017, CollisionQueryType.Boolean);
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
				if (1 == 0)
				{
					__ldtoken(TriangleMeshAlgorithm.cf5871a86be38372180f9b0b034bc995c(ContactSet, int, int, CollisionQueryType, ContactSet, CollisionObject, c107cf2db36674a478eaf84e50d31337e, TriangleShape, CollisionObject, c107cf2db36674a478eaf84e50d31337e, TriangleShape));
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
		CollisionQueryType type = (cfc73391dcf53c37de15d54e2434d0199.HaveContact ? CollisionQueryType.Contacts : cb76a1558f3778afde9d9fa562d4ee745);
		c71b56267c9b9d1093856224a4b92be5c.ComputeCollision(c1627d7366307c41303377e394f0fa017, type);
		cfc73391dcf53c37de15d54e2434d0199.HaveContact = cfc73391dcf53c37de15d54e2434d0199.HaveContact || c1627d7366307c41303377e394f0fa017.HaveContact;
		if (c1627d7366307c41303377e394f0fa017.HaveContact && !triangleMeshShape.IsTwoSided && !triangleMeshShape2.IsTwoSided)
		{
			Vector3F localDirection = Vector3F.Cross(triangle.Vertex1 - triangle.Vertex0, triangle.Vertex2 - triangle.Vertex0) / scale;
			Vector3F c79a3b94c816fc4e87d6348c3713a = pose.ToWorldDirection(localDirection);
			if (c79a3b94c816fc4e87d6348c3713a.TryNormalize())
			{
				c9ae4fc785e8cd5184185f6ee6d5a3853.cd4808b4cca49e2d5bdc52072c95767d2(c1627d7366307c41303377e394f0fa017, c79a3b94c816fc4e87d6348c3713a, CollisionDetection.Epsilon);
			}
		}
		if (c1627d7366307c41303377e394f0fa017.Count <= 0)
		{
			return;
		}
		int count = c1627d7366307c41303377e394f0fa017.Count;
		for (int i = 0; i < count; i++)
		{
			Contact contact = c1627d7366307c41303377e394f0fa017[i];
			contact.FeatureA = c355cfe32ba0a3d5e720074f92d4ef42b;
			contact.FeatureB = cf578709c79e5bd453aa6f7b148b9245b;
		}
		while (true)
		{
			switch (5)
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
	///       <see cref="T:DigitalRune.Geometry.Shapes.TriangleMeshShape" />.
	///       </exception>
	public override float GetTimeOfImpact(CollisionObject objectA, Pose targetPoseA, CollisionObject objectB, Pose targetPoseB, float allowedPenetration)
	{
		if (!(objectA.GeometricObject.Shape is TriangleMeshShape))
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
				__ldtoken(TriangleMeshAlgorithm.GetTimeOfImpact(CollisionObject, Pose, CollisionObject, Pose, float));
			}
			MathHelper.Swap(ref objectA, ref objectB);
			MathHelper.Swap(ref targetPoseA, ref targetPoseB);
		}
		IGeometricObject geometricObject = objectA.GeometricObject;
		IGeometricObject geometricObject2 = objectB.GeometricObject;
		if (!(geometricObject.Shape is TriangleMeshShape triangleMeshShape))
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6520));
		}
		if (objectB.GeometricObject.Shape is TriangleMeshShape)
		{
			return 1f;
		}
		ITriangleMesh mesh = triangleMeshShape.Mesh;
		Pose pose = geometricObject.Pose;
		Pose pose2 = geometricObject2.Pose;
		Vector3F scale = geometricObject.Scale;
		Vector3F scale2 = geometricObject2.Scale;
		Aabb aabb = geometricObject2.Shape.GetAabb(scale2, pose.Inverse * pose2);
		aabb.Grow(geometricObject2.Shape.GetAabb(scale2, targetPoseA.Inverse * targetPoseB));
		TriangleShape triangleShape = cdb80ee76c96fc0751dea8d15ee69fa47.c2fabcdff37c34655c8dee4068556b530.Obtain();
		c107cf2db36674a478eaf84e50d31337e c107cf2db36674a478eaf84e50d31337e2 = c107cf2db36674a478eaf84e50d31337e.c2d8ce8b1ccf2b905e3b18f8a5145fe8a();
		c107cf2db36674a478eaf84e50d31337e2.Shape = triangleShape;
		c107cf2db36674a478eaf84e50d31337e2.Scale = Vector3F.One;
		c107cf2db36674a478eaf84e50d31337e2.Pose = pose;
		CollisionObject collisionObject = cdb80ee76c96fc0751dea8d15ee69fa47.c29a5464f174eae366728cbb2edc64776.Obtain();
		collisionObject.ceabf44cab475c8ec76f26d6553011a78(objectA, c107cf2db36674a478eaf84e50d31337e2);
		CollisionAlgorithm collisionAlgorithm = CollisionDetection.AlgorithmMatrix[typeof(TriangleShape), geometricObject2.Shape.GetType()];
		float num = 1f;
		if (triangleMeshShape.Partition != null)
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
			aabb.Scale(Vector3F.One / scale);
			IEnumerator<int> enumerator = triangleMeshShape.Partition.GetOverlaps(aabb).GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					int current = enumerator.Current;
					Triangle triangle = mesh.GetTriangle(current);
					triangle.Vertex0 *= scale;
					triangle.Vertex1 *= scale;
					triangle.Vertex2 *= scale;
					triangleShape.Vertex0 = triangle.Vertex0;
					triangleShape.Vertex1 = triangle.Vertex1;
					triangleShape.Vertex2 = triangle.Vertex2;
					float timeOfImpact = collisionAlgorithm.GetTimeOfImpact(collisionObject, targetPoseA, objectB, targetPoseB, allowedPenetration);
					num = Math.Min(num, timeOfImpact);
				}
				while (true)
				{
					switch (7)
					{
					case 0:
						break;
					default:
						goto end_IL_026c;
					}
					continue;
					end_IL_026c:
					break;
				}
			}
			finally
			{
				if (enumerator != null)
				{
					while (true)
					{
						switch (6)
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
		else
		{
			int numberOfTriangles = mesh.NumberOfTriangles;
			for (int i = 0; i < numberOfTriangles; i++)
			{
				Triangle triangle2 = mesh.GetTriangle(i);
				triangle2.Vertex0 *= scale;
				triangle2.Vertex1 *= scale;
				triangle2.Vertex2 *= scale;
				if (!GeometryHelper.HaveContact(aabb, triangle2.Aabb))
				{
					continue;
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
				triangleShape.Vertex0 = triangle2.Vertex0;
				triangleShape.Vertex1 = triangle2.Vertex1;
				triangleShape.Vertex2 = triangle2.Vertex2;
				float timeOfImpact2 = collisionAlgorithm.GetTimeOfImpact(collisionObject, targetPoseA, objectB, targetPoseB, allowedPenetration);
				num = Math.Min(num, timeOfImpact2);
			}
		}
		cdb80ee76c96fc0751dea8d15ee69fa47.c29a5464f174eae366728cbb2edc64776.Recycle(collisionObject);
		c107cf2db36674a478eaf84e50d31337e2.ca18b9b5faa9851d6355f617787bb5859();
		cdb80ee76c96fc0751dea8d15ee69fa47.c2fabcdff37c34655c8dee4068556b530.Recycle(triangleShape);
		return num;
	}

	[CompilerGenerated]
	private static c9befaade904d8c1cd1acca5ee28bfe1c c4bffc38c4054efe6a3f3ff877749c085()
	{
		return new c9befaade904d8c1cd1acca5ee28bfe1c();
	}

	[CompilerGenerated]
	private static void c4c5cc4eb6785fd907a1974db5b42eeb5(c9befaade904d8c1cd1acca5ee28bfe1c ce4b7628f9880cef87f99eae50a8132fe)
	{
		ce4b7628f9880cef87f99eae50a8132fe.cc5b0109eff96f91c3e8ea71cac49ebb7 = null;
		ce4b7628f9880cef87f99eae50a8132fe.cdaa7e85696579d3929b4007d71528264 = null;
		ce4b7628f9880cef87f99eae50a8132fe.c0ce437d81b9e321f976ce43b3a118caa = false;
		ce4b7628f9880cef87f99eae50a8132fe.ce8fe4f02b54149f02b6b103eb61ee46a = null;
		ce4b7628f9880cef87f99eae50a8132fe.c15f35ecef26809329de5b64d7ebf12e1 = null;
		ce4b7628f9880cef87f99eae50a8132fe.c7124c30f3350e2e0afcd0898b7ba2f0d = null;
		ce4b7628f9880cef87f99eae50a8132fe.c471af9d3a3379490f80b95e79689249d = null;
		ce4b7628f9880cef87f99eae50a8132fe.cd26f25512b9708f7257be18890c505a3 = null;
	}
}
