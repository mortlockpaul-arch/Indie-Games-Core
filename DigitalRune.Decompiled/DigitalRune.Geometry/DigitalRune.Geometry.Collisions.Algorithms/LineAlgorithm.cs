using System;
using A;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Collisions.Algorithms;

/// <summary>
///       Computes collision queries between <see cref="T:DigitalRune.Geometry.Shapes.LineShape" />s and other primitives.
///       </summary>
public class LineAlgorithm : CollisionAlgorithm
{
	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Collisions.Algorithms.LineAlgorithm" /> class.
	///       </summary>
	/// <param name="collisionDetection">The collision detection service.</param>
	public LineAlgorithm(CollisionDetection collisionDetection)
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
	///   <paramref name="contactSet" /> does not contain a <see cref="T:DigitalRune.Geometry.Shapes.Line" />.
	///       </exception>
	public override void ComputeCollision(ContactSet contactSet, CollisionQueryType type)
	{
		bool flag = contactSet.ObjectA.GeometricObject.Shape is LineShape;
		bool flag2 = contactSet.ObjectB.GeometricObject.Shape is LineShape;
		if (flag && flag2)
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
						__ldtoken(LineAlgorithm.ComputeCollision(ContactSet, CollisionQueryType));
					}
					c7533ad7e06f56b5111c881a2873e823d(contactSet, type);
					return;
				}
			}
		}
		if (!flag)
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
			if (!flag2)
			{
				throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(13213), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1826));
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
		c56e4286e53f8edb2f96eb5370eb3b3bd(contactSet, type, flag);
	}

	private void c7533ad7e06f56b5111c881a2873e823d(ContactSet cfc73391dcf53c37de15d54e2434d0199, CollisionQueryType cb76a1558f3778afde9d9fa562d4ee745)
	{
		IGeometricObject geometricObject = cfc73391dcf53c37de15d54e2434d0199.ObjectA.GeometricObject;
		IGeometricObject geometricObject2 = cfc73391dcf53c37de15d54e2434d0199.ObjectB.GeometricObject;
		Vector3F ceabb47d7d959545d89c4300eabbdd7c = geometricObject.Scale;
		Vector3F ceabb47d7d959545d89c4300eabbdd7c2 = geometricObject2.Scale;
		Pose cbc1b7e0b011ad6dcdb8beb392ec7a = geometricObject.Pose;
		Pose cbc1b7e0b011ad6dcdb8beb392ec7a2 = geometricObject2.Pose;
		Line line = new Line((LineShape)geometricObject.Shape);
		line.c22463d4a85fcdaa7d4ffbd8669084d72(ref ceabb47d7d959545d89c4300eabbdd7c);
		line.c77d310c5ae14ed7e7ed360995b1867bb(ref cbc1b7e0b011ad6dcdb8beb392ec7a);
		Line line2 = new Line((LineShape)geometricObject2.Shape);
		line2.c22463d4a85fcdaa7d4ffbd8669084d72(ref ceabb47d7d959545d89c4300eabbdd7c2);
		line2.c77d310c5ae14ed7e7ed360995b1867bb(ref cbc1b7e0b011ad6dcdb8beb392ec7a2);
		cfc73391dcf53c37de15d54e2434d0199.HaveContact = GeometryHelper.GetClosestPoints(line, line2, out var point, out var point2);
		switch (cb76a1558f3778afde9d9fa562d4ee745)
		{
		case CollisionQueryType.Contacts:
			if (cfc73391dcf53c37de15d54e2434d0199.HaveContact)
			{
				break;
			}
			while (true)
			{
				switch (4)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(LineAlgorithm.c7533ad7e06f56b5111c881a2873e823d(ContactSet, CollisionQueryType));
				}
				return;
			}
		case CollisionQueryType.Boolean:
			return;
		}
		Vector3F c2fed316f5bcf4dd4ea7883930f3822f = (point + point2) / 2f;
		Vector3F c79a3b94c816fc4e87d6348c3713a = point2 - point;
		float length = c79a3b94c816fc4e87d6348c3713a.Length;
		if (Numeric.IsZero(length))
		{
			c79a3b94c816fc4e87d6348c3713a = Vector3F.Cross(line.Direction, line2.Direction);
			if (!c79a3b94c816fc4e87d6348c3713a.TryNormalize())
			{
				c79a3b94c816fc4e87d6348c3713a = Vector3F.UnitY;
			}
		}
		else
		{
			c79a3b94c816fc4e87d6348c3713a /= length;
		}
		Contact cf1b03e8c15642a547bc4024e27883cdf = c9ae4fc785e8cd5184185f6ee6d5a3853.c8918c90b638b2cb75bae14c2683b291d(cfc73391dcf53c37de15d54e2434d0199, c2fed316f5bcf4dd4ea7883930f3822f, c79a3b94c816fc4e87d6348c3713a, 0f - length, c72057f882b90cac0b4126f84e6c9aa28: false);
		c9ae4fc785e8cd5184185f6ee6d5a3853.c6aedb1aa24c5098d4ce5c849eb999287(cfc73391dcf53c37de15d54e2434d0199, cf1b03e8c15642a547bc4024e27883cdf, cb76a1558f3778afde9d9fa562d4ee745, CollisionDetection.ContactPositionTolerance);
	}

	private void c56e4286e53f8edb2f96eb5370eb3b3bd(ContactSet cfc73391dcf53c37de15d54e2434d0199, CollisionQueryType cb76a1558f3778afde9d9fa562d4ee745, bool cfc613d0fdb526a9d6a645e6c61773e10)
	{
		CollisionObject objectA = cfc73391dcf53c37de15d54e2434d0199.ObjectA;
		CollisionObject objectB = cfc73391dcf53c37de15d54e2434d0199.ObjectB;
		IGeometricObject geometricObject = objectA.GeometricObject;
		IGeometricObject geometricObject2 = objectB.GeometricObject;
		Shape shape = geometricObject.Shape;
		Shape shape2 = geometricObject2.Shape;
		CollisionObject c9bdbd655e343282211edce8c6b867eab;
		IGeometricObject geometricObject3;
		LineShape lineShape;
		IGeometricObject geometricObject4;
		Shape shapeB;
		if (cfc613d0fdb526a9d6a645e6c61773e10)
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
				__ldtoken(LineAlgorithm.c56e4286e53f8edb2f96eb5370eb3b3bd(ContactSet, CollisionQueryType, bool));
			}
			c9bdbd655e343282211edce8c6b867eab = objectA;
			geometricObject3 = geometricObject;
			lineShape = (LineShape)shape;
			geometricObject4 = geometricObject2;
			shapeB = shape2;
		}
		else
		{
			c9bdbd655e343282211edce8c6b867eab = objectB;
			geometricObject3 = geometricObject2;
			lineShape = (LineShape)shape2;
			geometricObject4 = geometricObject;
			shapeB = shape;
		}
		Line line = new Line(lineShape);
		Vector3F ceabb47d7d959545d89c4300eabbdd7c = geometricObject3.Scale;
		line.c22463d4a85fcdaa7d4ffbd8669084d72(ref ceabb47d7d959545d89c4300eabbdd7c);
		Aabb aabb = geometricObject4.Aabb;
		Vector3F worldPosition = (aabb.Minimum + aabb.Maximum) / 2f;
		float length = (aabb.Maximum - aabb.Minimum).Length;
		Pose pose = geometricObject3.Pose;
		GeometryHelper.GetClosestPoint(line, pose.ToLocalPosition(worldPosition), out var closestPointOnLine);
		LineSegmentShape lineSegmentShape = cdb80ee76c96fc0751dea8d15ee69fa47.c15ee94e8f68a7da40b4b9010459260d1.Obtain();
		lineSegmentShape.Start = closestPointOnLine - line.Direction * length;
		lineSegmentShape.End = closestPointOnLine + line.Direction * length;
		c107cf2db36674a478eaf84e50d31337e c107cf2db36674a478eaf84e50d31337e2 = c107cf2db36674a478eaf84e50d31337e.c2d8ce8b1ccf2b905e3b18f8a5145fe8a();
		c107cf2db36674a478eaf84e50d31337e2.Shape = lineSegmentShape;
		c107cf2db36674a478eaf84e50d31337e2.Scale = Vector3F.One;
		c107cf2db36674a478eaf84e50d31337e2.Pose = pose;
		CollisionObject collisionObject = cdb80ee76c96fc0751dea8d15ee69fa47.c29a5464f174eae366728cbb2edc64776.Obtain();
		collisionObject.ceabf44cab475c8ec76f26d6553011a78(c9bdbd655e343282211edce8c6b867eab, c107cf2db36674a478eaf84e50d31337e2);
		ContactSet contactSet;
		if (!cfc613d0fdb526a9d6a645e6c61773e10)
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
			contactSet = ContactSet.Create(objectA, collisionObject);
		}
		else
		{
			contactSet = ContactSet.Create(collisionObject, objectB);
		}
		ContactSet contactSet2 = contactSet;
		contactSet2.IsPerturbationTestAllowed = cfc73391dcf53c37de15d54e2434d0199.IsPerturbationTestAllowed;
		CollisionAlgorithm collisionAlgorithm = CollisionDetection.AlgorithmMatrix[lineSegmentShape, shapeB];
		Vector3F vector3F = geometricObject.Pose.ToWorldPosition(shape.InnerPoint * geometricObject.Scale);
		Vector3F vector3F2 = geometricObject2.Pose.ToWorldPosition(shape2.InnerPoint * geometricObject2.Scale);
		Vector3F vector3F3 = vector3F2 - vector3F;
		vector3F3 -= Vector3F.ProjectTo(vector3F3, pose.ToWorldDirection(lineShape.Direction));
		if (!vector3F3.TryNormalize())
		{
			vector3F3 = lineShape.Direction.Orthonormal1;
		}
		contactSet2.c196da5f647797a182f2b6d32727c7621 = vector3F3;
		collisionAlgorithm.ComputeCollision(contactSet2, cb76a1558f3778afde9d9fa562d4ee745);
		if (contactSet2.HaveContact)
		{
			cfc73391dcf53c37de15d54e2434d0199.HaveContact = true;
		}
		c9ae4fc785e8cd5184185f6ee6d5a3853.c6aedb1aa24c5098d4ce5c849eb999287(cfc73391dcf53c37de15d54e2434d0199, contactSet2, cb76a1558f3778afde9d9fa562d4ee745, CollisionDetection.ContactPositionTolerance);
		contactSet2.Recycle();
		cdb80ee76c96fc0751dea8d15ee69fa47.c29a5464f174eae366728cbb2edc64776.Recycle(collisionObject);
		c107cf2db36674a478eaf84e50d31337e2.ca18b9b5faa9851d6355f617787bb5859();
		cdb80ee76c96fc0751dea8d15ee69fa47.c15ee94e8f68a7da40b4b9010459260d1.Recycle(lineSegmentShape);
	}
}
