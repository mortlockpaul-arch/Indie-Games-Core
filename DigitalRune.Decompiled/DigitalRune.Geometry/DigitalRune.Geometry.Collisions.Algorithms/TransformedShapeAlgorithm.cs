using System;
using A;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Collisions.Algorithms;

/// <summary>
///       Computes contact or closest-point information for <see cref="T:DigitalRune.Geometry.Shapes.TransformedShape" /> vs. any other 
///       <see cref="T:DigitalRune.Geometry.Shapes.Shape" />.
///       </summary>
public class TransformedShapeAlgorithm : CollisionAlgorithm
{
	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Collisions.Algorithms.TransformedShapeAlgorithm" /> class.
	///       </summary>
	/// <param name="collisionDetection">The collision detection service.</param>
	public TransformedShapeAlgorithm(CollisionDetection collisionDetection)
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
	///   <paramref name="contactSet" /> does not contain a <see cref="T:DigitalRune.Geometry.Shapes.TransformedShape" />.
	///       </exception>
	/// <exception cref="T:System.NotSupportedException">
	///   <paramref name="contactSet" /> contains a <see cref="T:DigitalRune.Geometry.Shapes.TransformedShape" /> with a local 
	///       rotation and a non-uniform scaling. Computing collisions for transformed shapes with local 
	///       rotations and non-uniform scaling is not supported.
	///       </exception>
	public override void ComputeCollision(ContactSet contactSet, CollisionQueryType type)
	{
		CollisionObject obj = contactSet.ObjectA;
		CollisionObject obj2 = contactSet.ObjectB;
		bool flag = !(obj2.GeometricObject.Shape is TransformedShape);
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
			if (1 == 0)
			{
				__ldtoken(TransformedShapeAlgorithm.ComputeCollision(ContactSet, CollisionQueryType));
			}
			MathHelper.Swap(ref obj, ref obj2);
		}
		IGeometricObject geometricObject = obj2.GeometricObject;
		if (!(geometricObject.Shape is TransformedShape transformedShape))
		{
			while (true)
			{
				switch (7)
				{
				case 0:
					continue;
				}
				throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(15781), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1826));
			}
		}
		Pose pose = geometricObject.Pose;
		Vector3F scale = geometricObject.Scale;
		if (scale.X == scale.Y)
		{
			if (scale.Y == scale.Z)
			{
				goto IL_00eb;
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
		if (pose.HasRotation)
		{
			while (true)
			{
				switch (4)
				{
				case 0:
					break;
				default:
					throw new NotSupportedException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(15880));
				}
			}
		}
		goto IL_00eb;
		IL_00eb:
		IGeometricObject child = transformedShape.Child;
		Pose pose2 = child.Pose;
		pose2.Position *= scale;
		c107cf2db36674a478eaf84e50d31337e c107cf2db36674a478eaf84e50d31337e2 = c107cf2db36674a478eaf84e50d31337e.c2d8ce8b1ccf2b905e3b18f8a5145fe8a();
		c107cf2db36674a478eaf84e50d31337e2.Shape = child.Shape;
		c107cf2db36674a478eaf84e50d31337e2.Scale = scale * child.Scale;
		c107cf2db36674a478eaf84e50d31337e2.Pose = pose * pose2;
		CollisionObject collisionObject = cdb80ee76c96fc0751dea8d15ee69fa47.c29a5464f174eae366728cbb2edc64776.Obtain();
		collisionObject.ceabf44cab475c8ec76f26d6553011a78(obj2, c107cf2db36674a478eaf84e50d31337e2);
		ContactSet contactSet2 = (flag ? ContactSet.Create(collisionObject, obj) : ContactSet.Create(obj, collisionObject));
		contactSet2.IsPerturbationTestAllowed = contactSet.IsPerturbationTestAllowed;
		CollisionAlgorithm collisionAlgorithm = CollisionDetection.AlgorithmMatrix[obj, collisionObject];
		collisionAlgorithm.ComputeCollision(contactSet2, type);
		if (contactSet2.HaveContact)
		{
			contactSet.HaveContact = true;
		}
		int count = contactSet2.Count;
		for (int i = 0; i < count; i++)
		{
			Contact contact = contactSet2[i];
			if (flag)
			{
				contact.PositionALocal = pose2.ToWorldPosition(contact.PositionALocal);
			}
			else
			{
				contact.PositionBLocal = pose2.ToWorldPosition(contact.PositionBLocal);
			}
		}
		c9ae4fc785e8cd5184185f6ee6d5a3853.c6aedb1aa24c5098d4ce5c849eb999287(contactSet, contactSet2, type, CollisionDetection.ContactPositionTolerance);
		contactSet2.Recycle();
		cdb80ee76c96fc0751dea8d15ee69fa47.c29a5464f174eae366728cbb2edc64776.Recycle(collisionObject);
		c107cf2db36674a478eaf84e50d31337e2.ca18b9b5faa9851d6355f617787bb5859();
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
	///       <see cref="T:DigitalRune.Geometry.Shapes.TransformedShape" />.
	///       </exception>
	public override float GetTimeOfImpact(CollisionObject objectA, Pose targetPoseA, CollisionObject objectB, Pose targetPoseB, float allowedPenetration)
	{
		if (!(objectB.GeometricObject.Shape is TransformedShape))
		{
			MathHelper.Swap(ref objectA, ref objectB);
			MathHelper.Swap(ref targetPoseA, ref targetPoseB);
		}
		IGeometricObject geometricObject = objectB.GeometricObject;
		if (!(geometricObject.Shape is TransformedShape transformedShape))
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
						__ldtoken(TransformedShapeAlgorithm.GetTimeOfImpact(CollisionObject, Pose, CollisionObject, Pose, float));
					}
					throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(16094));
				}
			}
		}
		Pose pose = geometricObject.Pose;
		Vector3F scale = geometricObject.Scale;
		IGeometricObject child = transformedShape.Child;
		Pose pose2 = child.Pose;
		pose2.Position *= scale;
		c107cf2db36674a478eaf84e50d31337e c107cf2db36674a478eaf84e50d31337e2 = c107cf2db36674a478eaf84e50d31337e.c2d8ce8b1ccf2b905e3b18f8a5145fe8a();
		c107cf2db36674a478eaf84e50d31337e2.Shape = child.Shape;
		c107cf2db36674a478eaf84e50d31337e2.Scale = scale * child.Scale;
		c107cf2db36674a478eaf84e50d31337e2.Pose = pose * pose2;
		CollisionObject collisionObject = cdb80ee76c96fc0751dea8d15ee69fa47.c29a5464f174eae366728cbb2edc64776.Obtain();
		collisionObject.ceabf44cab475c8ec76f26d6553011a78(objectB, c107cf2db36674a478eaf84e50d31337e2);
		CollisionAlgorithm collisionAlgorithm = CollisionDetection.AlgorithmMatrix[objectA, collisionObject];
		float timeOfImpact = collisionAlgorithm.GetTimeOfImpact(objectA, targetPoseA, collisionObject, targetPoseB * pose2, allowedPenetration);
		cdb80ee76c96fc0751dea8d15ee69fa47.c29a5464f174eae366728cbb2edc64776.Recycle(collisionObject);
		c107cf2db36674a478eaf84e50d31337e2.ca18b9b5faa9851d6355f617787bb5859();
		return timeOfImpact;
	}
}
