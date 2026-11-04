using System;
using A;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Collisions.Algorithms;

/// <summary>
///       Computes contact or closest-point information for <see cref="T:DigitalRune.Geometry.Shapes.RayShape" /> vs. <see cref="T:DigitalRune.Geometry.Shapes.PlaneShape" />.
///       </summary>
public class PlaneRayAlgorithm : CollisionAlgorithm
{
	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Collisions.Algorithms.PlaneRayAlgorithm" /> class.
	///       </summary>
	/// <param name="collisionDetection">The collision detection service.</param>
	public PlaneRayAlgorithm(CollisionDetection collisionDetection)
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
	///   <paramref name="contactSet" /> does not contain a <see cref="T:DigitalRune.Geometry.Shapes.PlaneShape" /> and a 
	///       <see cref="T:DigitalRune.Geometry.Shapes.RayShape" />.
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
		RayShape rayShape = obj2.Shape as RayShape;
		if (planeShape != null)
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
				__ldtoken(PlaneRayAlgorithm.ComputeCollision(ContactSet, CollisionQueryType));
			}
			if (rayShape != null)
			{
				Vector3F ceabb47d7d959545d89c4300eabbdd7c = obj.Scale;
				Vector3F ceabb47d7d959545d89c4300eabbdd7c2 = obj2.Scale;
				Pose cbc1b7e0b011ad6dcdb8beb392ec7a = obj2.Pose;
				Pose cbc1b7e0b011ad6dcdb8beb392ec7a2 = obj.Pose;
				Plane plane = new Plane(planeShape);
				plane.c22463d4a85fcdaa7d4ffbd8669084d72(ref ceabb47d7d959545d89c4300eabbdd7c);
				Ray ray = new Ray(rayShape);
				ray.c22463d4a85fcdaa7d4ffbd8669084d72(ref ceabb47d7d959545d89c4300eabbdd7c2);
				ray.c77d310c5ae14ed7e7ed360995b1867bb(ref cbc1b7e0b011ad6dcdb8beb392ec7a);
				ray.c0d5e70cac6e675cb3df8faaefbfd7052(ref cbc1b7e0b011ad6dcdb8beb392ec7a2);
				LineSegment lineSegment = new LineSegment
				{
					Start = ray.Origin,
					End = ray.Origin + ray.Direction * ray.Length
				};
				Vector3F pointOnLineSegment = Vector3F.Zero;
				Vector3F pointOnPlane;
				if (Vector3F.Dot(lineSegment.Start, plane.Normal) <= plane.DistanceFromOrigin)
				{
					pointOnPlane = lineSegment.Start;
					contactSet.HaveContact = true;
				}
				else
				{
					contactSet.HaveContact = GeometryHelper.GetClosestPoints(plane, lineSegment, out pointOnPlane, out pointOnLineSegment);
				}
				if (type == CollisionQueryType.Boolean)
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
					if (type == CollisionQueryType.Contacts && !contactSet.HaveContact)
					{
						return;
					}
					Vector3F c2fed316f5bcf4dd4ea7883930f3822f;
					float cea91322cfd30caa062e8bdf867e4b;
					if (contactSet.HaveContact)
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
						c2fed316f5bcf4dd4ea7883930f3822f = cbc1b7e0b011ad6dcdb8beb392ec7a2.ToWorldPosition(pointOnPlane);
						cea91322cfd30caa062e8bdf867e4b = (pointOnPlane - lineSegment.Start).Length;
					}
					else
					{
						c2fed316f5bcf4dd4ea7883930f3822f = cbc1b7e0b011ad6dcdb8beb392ec7a2.ToWorldPosition((pointOnLineSegment + pointOnPlane) / 2f);
						cea91322cfd30caa062e8bdf867e4b = 0f - (pointOnPlane - pointOnLineSegment).Length;
					}
					Vector3F vector3F = cbc1b7e0b011ad6dcdb8beb392ec7a2.ToWorldDirection(plane.Normal);
					if (flag)
					{
						vector3F = -vector3F;
					}
					Contact cf1b03e8c15642a547bc4024e27883cdf = c9ae4fc785e8cd5184185f6ee6d5a3853.c8918c90b638b2cb75bae14c2683b291d(contactSet, c2fed316f5bcf4dd4ea7883930f3822f, vector3F, cea91322cfd30caa062e8bdf867e4b, contactSet.HaveContact);
					c9ae4fc785e8cd5184185f6ee6d5a3853.c6aedb1aa24c5098d4ce5c849eb999287(contactSet, cf1b03e8c15642a547bc4024e27883cdf, type, CollisionDetection.ContactPositionTolerance);
					return;
				}
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
		throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(17935), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1826));
	}
}
