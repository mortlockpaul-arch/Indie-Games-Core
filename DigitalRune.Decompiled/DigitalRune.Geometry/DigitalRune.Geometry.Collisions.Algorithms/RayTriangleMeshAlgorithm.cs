using System;
using System.Collections.Generic;
using A;
using DigitalRune.Geometry.Meshes;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Collisions.Algorithms;

/// <summary>
///       Computes contact or closest-point information for <see cref="T:DigitalRune.Geometry.Shapes.RayShape" /> vs. 
///       <see cref="T:DigitalRune.Geometry.Shapes.TriangleMeshShape" />.
///       </summary>
public class RayTriangleMeshAlgorithm : CollisionAlgorithm
{
	private readonly TriangleMeshAlgorithm cb45f82b55dd4e4f2e5c1f145f15b72fb;

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Collisions.Algorithms.RayTriangleMeshAlgorithm" /> class.
	///       </summary>
	/// <param name="collisionDetection">The collision detection service.</param>
	public RayTriangleMeshAlgorithm(CollisionDetection collisionDetection)
		: base(collisionDetection)
	{
		cb45f82b55dd4e4f2e5c1f145f15b72fb = new TriangleMeshAlgorithm(collisionDetection);
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
			while (true)
			{
				switch (4)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(RayTriangleMeshAlgorithm.ComputeCollision(ContactSet, CollisionQueryType));
					}
					cb45f82b55dd4e4f2e5c1f145f15b72fb.ComputeCollision(contactSet, type);
					return;
				}
			}
		}
		IGeometricObject obj = contactSet.ObjectA.GeometricObject;
		IGeometricObject obj2 = contactSet.ObjectB.GeometricObject;
		bool flag = obj.Shape is RayShape;
		if (flag)
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
			MathHelper.Swap(ref obj2, ref obj);
		}
		RayShape rayShape = obj2.Shape as RayShape;
		TriangleMeshShape triangleMeshShape = obj.Shape as TriangleMeshShape;
		if (rayShape != null)
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
			if (triangleMeshShape != null)
			{
				contactSet.HaveContact = false;
				Vector3F ceabb47d7d959545d89c4300eabbdd7c = obj2.Scale;
				Pose cbc1b7e0b011ad6dcdb8beb392ec7a = obj2.Pose;
				Vector3F cd43bd3081ba2d9ab6e7120b46c165a = obj.Scale;
				Pose cbc1b7e0b011ad6dcdb8beb392ec7a2 = obj.Pose;
				Ray cde1533d396e1ad78240641e2f4146f5d = new Ray(rayShape);
				cde1533d396e1ad78240641e2f4146f5d.c22463d4a85fcdaa7d4ffbd8669084d72(ref ceabb47d7d959545d89c4300eabbdd7c);
				cde1533d396e1ad78240641e2f4146f5d.c77d310c5ae14ed7e7ed360995b1867bb(ref cbc1b7e0b011ad6dcdb8beb392ec7a);
				Ray c2d138deffd3c2b2bbcd462f78120966a = cde1533d396e1ad78240641e2f4146f5d;
				c2d138deffd3c2b2bbcd462f78120966a.c0d5e70cac6e675cb3df8faaefbfd7052(ref cbc1b7e0b011ad6dcdb8beb392ec7a2);
				Ray ray = c2d138deffd3c2b2bbcd462f78120966a;
				Vector3F ceabb47d7d959545d89c4300eabbdd7c2 = Vector3F.One / cd43bd3081ba2d9ab6e7120b46c165a;
				ray.c22463d4a85fcdaa7d4ffbd8669084d72(ref ceabb47d7d959545d89c4300eabbdd7c2);
				ITriangleMesh mesh = triangleMeshShape.Mesh;
				bool isTwoSided = triangleMeshShape.IsTwoSided;
				if (triangleMeshShape.Partition != null)
				{
					IEnumerator<int> enumerator = triangleMeshShape.Partition.GetOverlaps(ray).GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							int current = enumerator.Current;
							Triangle c082c516895a1e859067cd7bbdbe8b9e = mesh.GetTriangle(current);
							c7f470d0616e3ac2bc5ac9dc43e0dbb50(contactSet, flag, type, ref cde1533d396e1ad78240641e2f4146f5d, ref c2d138deffd3c2b2bbcd462f78120966a, ref c082c516895a1e859067cd7bbdbe8b9e, current, ref cbc1b7e0b011ad6dcdb8beb392ec7a2, ref cd43bd3081ba2d9ab6e7120b46c165a, isTwoSided);
							if (type != CollisionQueryType.Boolean)
							{
								continue;
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
							if (contactSet.HaveContact)
							{
								break;
							}
						}
						return;
					}
					finally
					{
						if (enumerator != null)
						{
							while (true)
							{
								switch (3)
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
				Vector3F c3ec982f192efbba86e42a1cea5c = new Vector3F(1f / ray.Direction.X, 1f / ray.Direction.Y, 1f / ray.Direction.Z);
				float c912da9d2d82e2a6523be60260abdfc = Numeric.EpsilonF * (1f + obj.Aabb.Extent.Length);
				int numberOfTriangles = mesh.NumberOfTriangles;
				for (int i = 0; i < numberOfTriangles; i++)
				{
					Triangle c082c516895a1e859067cd7bbdbe8b9e2 = mesh.GetTriangle(i);
					if (GeometryHelper.cdd6d17271fec83883494ba8e43eede6a(c082c516895a1e859067cd7bbdbe8b9e2.Aabb, ray.Origin, c3ec982f192efbba86e42a1cea5c, ray.Length, c912da9d2d82e2a6523be60260abdfc))
					{
						c7f470d0616e3ac2bc5ac9dc43e0dbb50(contactSet, flag, type, ref cde1533d396e1ad78240641e2f4146f5d, ref c2d138deffd3c2b2bbcd462f78120966a, ref c082c516895a1e859067cd7bbdbe8b9e2, i, ref cbc1b7e0b011ad6dcdb8beb392ec7a2, ref cd43bd3081ba2d9ab6e7120b46c165a, isTwoSided);
						if (contactSet.HaveContact && type == CollisionQueryType.Boolean)
						{
							break;
						}
					}
				}
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
		throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2590), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1826));
	}

	private void c7f470d0616e3ac2bc5ac9dc43e0dbb50(ContactSet cfc73391dcf53c37de15d54e2434d0199, bool cbd5f4ffb7f6caa84b20b23ef3ac88b62, CollisionQueryType cb76a1558f3778afde9d9fa562d4ee745, ref Ray cde1533d396e1ad78240641e2f4146f5d, ref Ray c2d138deffd3c2b2bbcd462f78120966a, ref Triangle c082c516895a1e859067cd7bbdbe8b9e5, int c770ce7a50191007b3e32d6b7a6e43051, ref Pose c36819baa9f54177ab4805a1ed537916a, ref Vector3F cd43bd3081ba2d9ab6e7120b46c165a99, bool c88ffc1fce30da02a48d92f0d498b3793)
	{
		Vector3F vector3F = c082c516895a1e859067cd7bbdbe8b9e5.Vertex0 * cd43bd3081ba2d9ab6e7120b46c165a99;
		Vector3F vector3F2 = c082c516895a1e859067cd7bbdbe8b9e5.Vertex1 * cd43bd3081ba2d9ab6e7120b46c165a99;
		Vector3F vector3F3 = c082c516895a1e859067cd7bbdbe8b9e5.Vertex2 * cd43bd3081ba2d9ab6e7120b46c165a99;
		Vector3F vector3F4 = vector3F2 - vector3F;
		Vector3F vector3F5 = vector3F3 - vector3F;
		Vector3F vector3F6 = Vector3F.Cross(vector3F4, vector3F5);
		float num = vector3F6.Length * Numeric.EpsilonFSquared;
		Vector3F vector3F7 = c2d138deffd3c2b2bbcd462f78120966a.Direction * c2d138deffd3c2b2bbcd462f78120966a.Length;
		float num2 = 0f - Vector3F.Dot(vector3F7, vector3F6);
		if (num == 0f || Numeric.IsZero(num2, num))
		{
			return;
		}
		Vector3F vector = c2d138deffd3c2b2bbcd462f78120966a.Origin - vector3F;
		float num3 = Vector3F.Dot(vector, vector3F6) / num2;
		if (num3 < 0f)
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
			if (1 == 0)
			{
				__ldtoken(RayTriangleMeshAlgorithm.c7f470d0616e3ac2bc5ac9dc43e0dbb50(ContactSet, bool, CollisionQueryType, ref Ray, ref Ray, ref Triangle, int, ref Pose, ref Vector3F, bool));
			}
			if (num3 > 1f)
			{
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
			}
			Vector3F vector2 = Vector3F.Cross(vector, vector3F7);
			float num4 = Vector3F.Dot(vector3F5, vector2) / num2;
			float num5 = Vector3F.Dot(-vector3F4, vector2) / num2;
			if (!(num4 + num5 <= 1f + num) || !(num4 >= 0f - num) || !(num5 >= 0f - num))
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
				cfc73391dcf53c37de15d54e2434d0199.HaveContact = true;
				if (cb76a1558f3778afde9d9fa562d4ee745 == CollisionQueryType.Boolean)
				{
					return;
				}
				if (num2 < 0f)
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
					if (!c88ffc1fce30da02a48d92f0d498b3793)
					{
						while (true)
						{
							switch (1)
							{
							case 0:
								break;
							default:
								return;
							}
						}
					}
				}
				float num6 = num3 * c2d138deffd3c2b2bbcd462f78120966a.Length;
				Vector3F c2fed316f5bcf4dd4ea7883930f3822f = cde1533d396e1ad78240641e2f4146f5d.Origin + cde1533d396e1ad78240641e2f4146f5d.Direction * num6;
				vector3F6 = c36819baa9f54177ab4805a1ed537916a.ToWorldDirection(vector3F6);
				vector3F6.Normalize();
				if (num2 < 0f)
				{
					vector3F6 = -vector3F6;
				}
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
					vector3F6 = -vector3F6;
				}
				Contact contact = c9ae4fc785e8cd5184185f6ee6d5a3853.c8918c90b638b2cb75bae14c2683b291d(cfc73391dcf53c37de15d54e2434d0199, c2fed316f5bcf4dd4ea7883930f3822f, vector3F6, num6, c72057f882b90cac0b4126f84e6c9aa28: true);
				if (cbd5f4ffb7f6caa84b20b23ef3ac88b62)
				{
					contact.FeatureB = c770ce7a50191007b3e32d6b7a6e43051;
				}
				else
				{
					contact.FeatureA = c770ce7a50191007b3e32d6b7a6e43051;
				}
				c9ae4fc785e8cd5184185f6ee6d5a3853.c6aedb1aa24c5098d4ce5c849eb999287(cfc73391dcf53c37de15d54e2434d0199, contact, cb76a1558f3778afde9d9fa562d4ee745, CollisionDetection.ContactPositionTolerance);
				return;
			}
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
	public override float GetTimeOfImpact(CollisionObject objectA, Pose targetPoseA, CollisionObject objectB, Pose targetPoseB, float allowedPenetration)
	{
		return cb45f82b55dd4e4f2e5c1f145f15b72fb.GetTimeOfImpact(objectA, targetPoseA, objectB, targetPoseB, allowedPenetration);
	}
}
