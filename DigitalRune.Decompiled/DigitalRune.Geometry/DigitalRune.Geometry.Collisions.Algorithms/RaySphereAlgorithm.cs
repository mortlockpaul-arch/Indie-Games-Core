using System;
using A;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Collisions.Algorithms;

/// <summary>
///       Computes contact or closest-point information for <see cref="T:DigitalRune.Geometry.Shapes.RayShape" /> vs. 
///       <see cref="T:DigitalRune.Geometry.Shapes.SphereShape" />.
///       </summary>
public class RaySphereAlgorithm : CollisionAlgorithm
{
	private CollisionAlgorithm ce4ac7c01f9cf66b62482860397c715f6;

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Collisions.Algorithms.RaySphereAlgorithm" /> class.
	///       </summary>
	/// <param name="collisionDetection">The collision detection service.</param>
	public RaySphereAlgorithm(CollisionDetection collisionDetection)
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
	///   <paramref name="contactSet" /> does not contain a <see cref="T:DigitalRune.Geometry.Shapes.RayShape" /> and a 
	///       <see cref="T:DigitalRune.Geometry.Shapes.SphereShape" />.
	///       </exception>
	public override void ComputeCollision(ContactSet contactSet, CollisionQueryType type)
	{
		IGeometricObject obj = contactSet.ObjectA.GeometricObject;
		IGeometricObject obj2 = contactSet.ObjectB.GeometricObject;
		bool flag = obj2.Shape is RayShape;
		if (flag)
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
				__ldtoken(RaySphereAlgorithm.ComputeCollision(ContactSet, CollisionQueryType));
			}
			MathHelper.Swap(ref obj, ref obj2);
		}
		RayShape rayShape = obj.Shape as RayShape;
		SphereShape sphereShape = obj2.Shape as SphereShape;
		Pose cbc1b7e0b011ad6dcdb8beb392ec7a2;
		Ray ray;
		float num2;
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
			if (sphereShape != null)
			{
				Vector3F ceabb47d7d959545d89c4300eabbdd7c = obj.Scale;
				Vector3F vector3F = Vector3F.Absolute(obj2.Scale);
				Pose cbc1b7e0b011ad6dcdb8beb392ec7a = obj.Pose;
				cbc1b7e0b011ad6dcdb8beb392ec7a2 = obj2.Pose;
				if (vector3F.X != vector3F.Y || vector3F.Y != vector3F.Z)
				{
					if (ce4ac7c01f9cf66b62482860397c715f6 == null)
					{
						ce4ac7c01f9cf66b62482860397c715f6 = CollisionDetection.AlgorithmMatrix[typeof(RayShape), typeof(ConvexShape)];
					}
					ce4ac7c01f9cf66b62482860397c715f6.ComputeCollision(contactSet, type);
					return;
				}
				ray = new Ray(rayShape);
				ray.c22463d4a85fcdaa7d4ffbd8669084d72(ref ceabb47d7d959545d89c4300eabbdd7c);
				ray.c77d310c5ae14ed7e7ed360995b1867bb(ref cbc1b7e0b011ad6dcdb8beb392ec7a);
				float num = sphereShape.Radius * vector3F.X;
				num2 = num * num;
				if (type != CollisionQueryType.ClosestPoints)
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
					if (type != CollisionQueryType.Boolean)
					{
						goto IL_02a3;
					}
				}
				LineSegment lineSegment = new LineSegment(ray.Origin, ray.Origin + ray.Direction * ray.Length);
				Vector3F position = cbc1b7e0b011ad6dcdb8beb392ec7a2.Position;
				GeometryHelper.GetClosestPoints(lineSegment, position, out var closestPointOnLineSegment);
				Vector3F vector3F2 = position - closestPointOnLineSegment;
				float lengthSquared = vector3F2.LengthSquared;
				float num3 = num2 - lengthSquared;
				contactSet.HaveContact = num3 >= 0f;
				if (type == CollisionQueryType.Boolean)
				{
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
				}
				if (num3 <= 0f)
				{
					while (true)
					{
						switch (6)
						{
						case 0:
							break;
						default:
						{
							Vector3F c2fed316f5bcf4dd4ea7883930f3822f;
							if (vector3F2.TryNormalize())
							{
								Vector3F vector3F3 = position - vector3F2 * num;
								c2fed316f5bcf4dd4ea7883930f3822f = (vector3F3 + closestPointOnLineSegment) / 2f;
							}
							else
							{
								c2fed316f5bcf4dd4ea7883930f3822f = closestPointOnLineSegment;
								vector3F2 = Vector3F.UnitY;
							}
							if (flag)
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
								vector3F2 = -vector3F2;
							}
							float cea91322cfd30caa062e8bdf867e4b = 0f - ((float)Math.Sqrt(lengthSquared) - num);
							Contact cf1b03e8c15642a547bc4024e27883cdf = c9ae4fc785e8cd5184185f6ee6d5a3853.c8918c90b638b2cb75bae14c2683b291d(contactSet, c2fed316f5bcf4dd4ea7883930f3822f, vector3F2, cea91322cfd30caa062e8bdf867e4b, contactSet.HaveContact);
							c9ae4fc785e8cd5184185f6ee6d5a3853.c6aedb1aa24c5098d4ce5c849eb999287(contactSet, cf1b03e8c15642a547bc4024e27883cdf, type, CollisionDetection.ContactPositionTolerance);
							return;
						}
						}
					}
				}
				goto IL_02a3;
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
		}
		throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8807), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1826));
		IL_02a3:
		contactSet.HaveContact = false;
		Ray ray2 = ray;
		ray2.c0d5e70cac6e675cb3df8faaefbfd7052(ref cbc1b7e0b011ad6dcdb8beb392ec7a2);
		Vector3F origin = ray2.Origin;
		Vector3F direction = ray2.Direction;
		Vector3F vector3F4 = direction * ray2.Length;
		float num4 = ray2.Length * ray2.Length;
		float num5 = 0f - Vector3F.Dot(origin, vector3F4);
		float num6 = num5 * num5 - num4 * (origin.LengthSquared - num2);
		if (!(num6 >= 0f))
		{
			return;
		}
		float num7 = (float)Math.Sqrt(num6);
		float num8 = num5 + num7;
		if (!(num8 >= 0f))
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
			float num9 = (num5 - num7) / num4;
			if (!(num9 <= 1f))
			{
				return;
			}
			contactSet.HaveContact = true;
			float num10;
			Vector3F localDirection;
			if (num9 > 0f)
			{
				num10 = num9 * ray2.Length;
				localDirection = -(origin + vector3F4 * num9);
			}
			else
			{
				num10 = 0f;
				localDirection = -origin;
			}
			Vector3F c2fed316f5bcf4dd4ea7883930f3822f2 = ray.Origin + ray.Direction * num10;
			localDirection = cbc1b7e0b011ad6dcdb8beb392ec7a2.ToWorldDirection(localDirection);
			if (!localDirection.TryNormalize())
			{
				localDirection = Vector3F.UnitY;
			}
			if (flag)
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
				localDirection = -localDirection;
			}
			Contact cf1b03e8c15642a547bc4024e27883cdf2 = c9ae4fc785e8cd5184185f6ee6d5a3853.c8918c90b638b2cb75bae14c2683b291d(contactSet, c2fed316f5bcf4dd4ea7883930f3822f2, localDirection, num10, c72057f882b90cac0b4126f84e6c9aa28: true);
			c9ae4fc785e8cd5184185f6ee6d5a3853.c6aedb1aa24c5098d4ce5c849eb999287(contactSet, cf1b03e8c15642a547bc4024e27883cdf2, type, CollisionDetection.ContactPositionTolerance);
			return;
		}
	}
}
