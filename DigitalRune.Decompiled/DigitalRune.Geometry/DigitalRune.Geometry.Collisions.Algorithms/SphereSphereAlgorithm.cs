using System;
using A;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Collisions.Algorithms;

/// <summary>
///       Computes contact or closest-point information for two <see cref="T:DigitalRune.Geometry.Shapes.SphereShape" />s.
///       </summary>
public class SphereSphereAlgorithm : CollisionAlgorithm
{
	private CollisionAlgorithm ce4ac7c01f9cf66b62482860397c715f6;

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Collisions.Algorithms.SphereSphereAlgorithm" /> class.
	///       </summary>
	/// <param name="collisionDetection">The collision detection service.</param>
	public SphereSphereAlgorithm(CollisionDetection collisionDetection)
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
	///   <paramref name="contactSet" /> does not contain two <see cref="T:DigitalRune.Geometry.Shapes.SphereShape" /> shapes.
	///       </exception>
	public override void ComputeCollision(ContactSet contactSet, CollisionQueryType type)
	{
		IGeometricObject geometricObject = contactSet.ObjectA.GeometricObject;
		IGeometricObject geometricObject2 = contactSet.ObjectB.GeometricObject;
		SphereShape sphereShape = geometricObject.Shape as SphereShape;
		SphereShape sphereShape2 = geometricObject2.Shape as SphereShape;
		if (sphereShape != null)
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
				__ldtoken(SphereSphereAlgorithm.ComputeCollision(ContactSet, CollisionQueryType));
			}
			if (sphereShape2 != null)
			{
				Vector3F vector3F = Vector3F.Absolute(geometricObject.Scale);
				Vector3F vector3F2 = Vector3F.Absolute(geometricObject2.Scale);
				if (vector3F.X == vector3F.Y && vector3F.Y == vector3F.Z)
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
					if (vector3F2.X == vector3F2.Y && vector3F2.Y == vector3F2.Z)
					{
						float num = sphereShape.Radius * vector3F.X;
						float num2 = sphereShape2.Radius * vector3F2.X;
						Vector3F position = geometricObject.Pose.Position;
						Vector3F position2 = geometricObject2.Pose.Position;
						Vector3F vector3F3 = position2 - position;
						float length = vector3F3.Length;
						float num3 = num + num2 - length;
						contactSet.HaveContact = num3 >= 0f;
						if (type == CollisionQueryType.Boolean)
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
							if (type == CollisionQueryType.Contacts)
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
								if (!contactSet.HaveContact)
								{
									return;
								}
							}
							Vector3F vector3F4 = ((!Numeric.IsZero(length)) ? vector3F3.Normalized : Vector3F.UnitY);
							Vector3F c2fed316f5bcf4dd4ea7883930f3822f = position + vector3F4 * (num - num3 / 2f);
							Contact cf1b03e8c15642a547bc4024e27883cdf = c9ae4fc785e8cd5184185f6ee6d5a3853.c8918c90b638b2cb75bae14c2683b291d(contactSet, c2fed316f5bcf4dd4ea7883930f3822f, vector3F4, num3, c72057f882b90cac0b4126f84e6c9aa28: false);
							c9ae4fc785e8cd5184185f6ee6d5a3853.c6aedb1aa24c5098d4ce5c849eb999287(contactSet, cf1b03e8c15642a547bc4024e27883cdf, type, CollisionDetection.ContactPositionTolerance);
							return;
						}
					}
				}
				if (ce4ac7c01f9cf66b62482860397c715f6 == null)
				{
					ce4ac7c01f9cf66b62482860397c715f6 = CollisionDetection.AlgorithmMatrix[typeof(ConvexShape), typeof(ConvexShape)];
				}
				ce4ac7c01f9cf66b62482860397c715f6.ComputeCollision(contactSet, type);
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
		throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(17848), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1826));
	}
}
