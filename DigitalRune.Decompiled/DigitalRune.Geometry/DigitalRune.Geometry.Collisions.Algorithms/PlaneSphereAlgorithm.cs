using System;
using A;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Collisions.Algorithms;

/// <summary>
///       Computes contact or closest-point information for <see cref="T:DigitalRune.Geometry.Shapes.SphereShape" /> vs. 
///       <see cref="T:DigitalRune.Geometry.Shapes.PlaneShape" />.
///       </summary>
public class PlaneSphereAlgorithm : CollisionAlgorithm
{
	private CollisionAlgorithm ce4ac7c01f9cf66b62482860397c715f6;

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Collisions.Algorithms.PlaneSphereAlgorithm" /> class.
	///       </summary>
	/// <param name="collisionDetection">The collision detection service.</param>
	public PlaneSphereAlgorithm(CollisionDetection collisionDetection)
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
	///   <paramref name="contactSet" /> does not contain a <see cref="T:DigitalRune.Geometry.Shapes.PlaneShape" /> and 
	///       <see cref="T:DigitalRune.Geometry.Shapes.SphereShape" />.
	///       </exception>
	public override void ComputeCollision(ContactSet contactSet, CollisionQueryType type)
	{
		IGeometricObject obj = contactSet.ObjectA.GeometricObject;
		IGeometricObject obj2 = contactSet.ObjectB.GeometricObject;
		bool flag = obj2.Shape is PlaneShape;
		if (flag)
		{
			MathHelper.Swap(ref obj, ref obj2);
		}
		PlaneShape planeShape = obj.Shape as PlaneShape;
		SphereShape sphereShape = obj2.Shape as SphereShape;
		if (planeShape != null)
		{
			if (sphereShape != null)
			{
				Vector3F ceabb47d7d959545d89c4300eabbdd7c = obj.Scale;
				Vector3F vector3F = Vector3F.Absolute(obj2.Scale);
				if (vector3F.X == vector3F.Y)
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
					if (vector3F.Y == vector3F.Z)
					{
						Pose cbc1b7e0b011ad6dcdb8beb392ec7a = obj.Pose;
						Pose pose = obj2.Pose;
						Plane plane = new Plane(planeShape);
						plane.c22463d4a85fcdaa7d4ffbd8669084d72(ref ceabb47d7d959545d89c4300eabbdd7c);
						plane.c77d310c5ae14ed7e7ed360995b1867bb(ref cbc1b7e0b011ad6dcdb8beb392ec7a);
						float num = sphereShape.Radius * vector3F.X;
						Vector3F position = pose.Position;
						float num2 = Vector3F.Dot(position, plane.Normal) - num - plane.DistanceFromOrigin;
						float num3 = 0f - num2;
						contactSet.HaveContact = num3 >= 0f;
						if (type == CollisionQueryType.Boolean)
						{
							return;
						}
						if (type == CollisionQueryType.Contacts)
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
							if (!contactSet.HaveContact)
							{
								while (true)
								{
									switch (4)
									{
									case 0:
										break;
									default:
										return;
									}
								}
							}
						}
						Vector3F c2fed316f5bcf4dd4ea7883930f3822f = position - plane.Normal * (num - num3 / 2f);
						Vector3F c79a3b94c816fc4e87d6348c3713a = (flag ? (-plane.Normal) : plane.Normal);
						Contact cf1b03e8c15642a547bc4024e27883cdf = c9ae4fc785e8cd5184185f6ee6d5a3853.c8918c90b638b2cb75bae14c2683b291d(contactSet, c2fed316f5bcf4dd4ea7883930f3822f, c79a3b94c816fc4e87d6348c3713a, num3, c72057f882b90cac0b4126f84e6c9aa28: false);
						c9ae4fc785e8cd5184185f6ee6d5a3853.c6aedb1aa24c5098d4ce5c849eb999287(contactSet, cf1b03e8c15642a547bc4024e27883cdf, type, CollisionDetection.ContactPositionTolerance);
						return;
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
				if (ce4ac7c01f9cf66b62482860397c715f6 == null)
				{
					ce4ac7c01f9cf66b62482860397c715f6 = CollisionDetection.AlgorithmMatrix[typeof(PlaneShape), typeof(ConvexShape)];
				}
				ce4ac7c01f9cf66b62482860397c715f6.ComputeCollision(contactSet, type);
				return;
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
			if (1 == 0)
			{
				__ldtoken(PlaneSphereAlgorithm.ComputeCollision(ContactSet, CollisionQueryType));
			}
		}
		throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(11290), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1826));
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
		return c33408081634d45382cc69064025f3c7f.c6b01c99c514aea8ee7f1a805664d5e98(objectA, targetPoseA, objectB, targetPoseB, allowedPenetration, CollisionDetection);
	}
}
