using System;
using A;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Collisions.Algorithms;

/// <summary>
///       Computes contact or closest-point information for <see cref="T:DigitalRune.Geometry.Shapes.PlaneShape" /> vs. 
///       <see cref="T:DigitalRune.Geometry.Shapes.BoxShape" />s.
///       </summary>
public class PlaneBoxAlgorithm : CollisionAlgorithm
{
	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Collisions.Algorithms.PlaneBoxAlgorithm" /> class.
	///       </summary>
	/// <param name="collisionDetection">The collision detection service.</param>
	public PlaneBoxAlgorithm(CollisionDetection collisionDetection)
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
	///       <see cref="T:DigitalRune.Geometry.Shapes.ConvexShape" />.
	///       </exception>
	public override void ComputeCollision(ContactSet contactSet, CollisionQueryType type)
	{
		IGeometricObject obj = contactSet.ObjectA.GeometricObject;
		IGeometricObject obj2 = contactSet.ObjectB.GeometricObject;
		bool flag = obj2.Shape is PlaneShape;
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
				__ldtoken(PlaneBoxAlgorithm.ComputeCollision(ContactSet, CollisionQueryType));
			}
			MathHelper.Swap(ref obj, ref obj2);
		}
		PlaneShape planeShape = obj.Shape as PlaneShape;
		BoxShape boxShape = obj2.Shape as BoxShape;
		if (planeShape != null)
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
			if (boxShape != null)
			{
				Vector3F ceabb47d7d959545d89c4300eabbdd7c = obj.Scale;
				Vector3F scale = obj2.Scale;
				Pose cbc1b7e0b011ad6dcdb8beb392ec7a = obj.Pose;
				Pose c3ab2e3d1ed639649b20cea4faebefade = obj2.Pose;
				Plane c546c04f26f2e15538a50655eb6b77b = new Plane(planeShape);
				c546c04f26f2e15538a50655eb6b77b.c22463d4a85fcdaa7d4ffbd8669084d72(ref ceabb47d7d959545d89c4300eabbdd7c);
				c546c04f26f2e15538a50655eb6b77b.c77d310c5ae14ed7e7ed360995b1867bb(ref cbc1b7e0b011ad6dcdb8beb392ec7a);
				Vector3F vector3F = c3ab2e3d1ed639649b20cea4faebefade.ToLocalDirection(c546c04f26f2e15538a50655eb6b77b.Normal);
				Vector3F supportPoint = boxShape.GetSupportPoint(-vector3F, scale);
				Vector3F c017396742a4443621f7602d4362206cd = c3ab2e3d1ed639649b20cea4faebefade.ToWorldPosition(supportPoint);
				float num = Vector3F.Dot(c017396742a4443621f7602d4362206cd, c546c04f26f2e15538a50655eb6b77b.Normal);
				float num2 = c546c04f26f2e15538a50655eb6b77b.DistanceFromOrigin - num;
				contactSet.HaveContact = num2 >= 0f;
				if (type == CollisionQueryType.Boolean)
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
					if (type == CollisionQueryType.Contacts && !contactSet.HaveContact)
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
					if (!CollisionDetection.FullContactSetPerFrame || type == CollisionQueryType.ClosestPoints)
					{
						c7f470d0616e3ac2bc5ac9dc43e0dbb50(ref c017396742a4443621f7602d4362206cd, ref c546c04f26f2e15538a50655eb6b77b, num2, flag, contactSet, type);
						return;
					}
					Vector3F vector3F2 = boxShape.Extent * scale * 0.5f;
					c2d3ae6f8d12573b47db0eec5b5372186(ref c546c04f26f2e15538a50655eb6b77b, new Vector3F(0f - vector3F2.X, 0f - vector3F2.Y, 0f - vector3F2.Z), ref c3ab2e3d1ed639649b20cea4faebefade, flag, contactSet);
					c2d3ae6f8d12573b47db0eec5b5372186(ref c546c04f26f2e15538a50655eb6b77b, new Vector3F(0f - vector3F2.X, 0f - vector3F2.Y, vector3F2.Z), ref c3ab2e3d1ed639649b20cea4faebefade, flag, contactSet);
					c2d3ae6f8d12573b47db0eec5b5372186(ref c546c04f26f2e15538a50655eb6b77b, new Vector3F(0f - vector3F2.X, vector3F2.Y, 0f - vector3F2.Z), ref c3ab2e3d1ed639649b20cea4faebefade, flag, contactSet);
					c2d3ae6f8d12573b47db0eec5b5372186(ref c546c04f26f2e15538a50655eb6b77b, new Vector3F(0f - vector3F2.X, vector3F2.Y, vector3F2.Z), ref c3ab2e3d1ed639649b20cea4faebefade, flag, contactSet);
					c2d3ae6f8d12573b47db0eec5b5372186(ref c546c04f26f2e15538a50655eb6b77b, new Vector3F(vector3F2.X, 0f - vector3F2.Y, 0f - vector3F2.Z), ref c3ab2e3d1ed639649b20cea4faebefade, flag, contactSet);
					c2d3ae6f8d12573b47db0eec5b5372186(ref c546c04f26f2e15538a50655eb6b77b, new Vector3F(vector3F2.X, 0f - vector3F2.Y, vector3F2.Z), ref c3ab2e3d1ed639649b20cea4faebefade, flag, contactSet);
					c2d3ae6f8d12573b47db0eec5b5372186(ref c546c04f26f2e15538a50655eb6b77b, new Vector3F(vector3F2.X, vector3F2.Y, 0f - vector3F2.Z), ref c3ab2e3d1ed639649b20cea4faebefade, flag, contactSet);
					c2d3ae6f8d12573b47db0eec5b5372186(ref c546c04f26f2e15538a50655eb6b77b, new Vector3F(vector3F2.X, vector3F2.Y, vector3F2.Z), ref c3ab2e3d1ed639649b20cea4faebefade, flag, contactSet);
					return;
				}
			}
		}
		throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9486), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1826));
	}

	private void c2d3ae6f8d12573b47db0eec5b5372186(ref Plane c546c04f26f2e15538a50655eb6b77b96, Vector3F ce367f48e054c0cf811b40a3176520c66, ref Pose c3ab2e3d1ed639649b20cea4faebefade, bool cbd5f4ffb7f6caa84b20b23ef3ac88b62, ContactSet cfc73391dcf53c37de15d54e2434d0199)
	{
		Vector3F c017396742a4443621f7602d4362206cd = c3ab2e3d1ed639649b20cea4faebefade.ToWorldPosition(ce367f48e054c0cf811b40a3176520c66);
		float num = Vector3F.Dot(c017396742a4443621f7602d4362206cd, c546c04f26f2e15538a50655eb6b77b96.Normal);
		float num2 = c546c04f26f2e15538a50655eb6b77b96.DistanceFromOrigin - num;
		if (num2 > 0f)
		{
			c7f470d0616e3ac2bc5ac9dc43e0dbb50(ref c017396742a4443621f7602d4362206cd, ref c546c04f26f2e15538a50655eb6b77b96, num2, cbd5f4ffb7f6caa84b20b23ef3ac88b62, cfc73391dcf53c37de15d54e2434d0199, CollisionQueryType.Contacts);
		}
	}

	private void c7f470d0616e3ac2bc5ac9dc43e0dbb50(ref Vector3F c017396742a4443621f7602d4362206cd, ref Plane c546c04f26f2e15538a50655eb6b77b96, float cea91322cfd30caa062e8bdf867e4b412, bool cbd5f4ffb7f6caa84b20b23ef3ac88b62, ContactSet cfc73391dcf53c37de15d54e2434d0199, CollisionQueryType cb76a1558f3778afde9d9fa562d4ee745)
	{
		Vector3F c2fed316f5bcf4dd4ea7883930f3822f = c017396742a4443621f7602d4362206cd + c546c04f26f2e15538a50655eb6b77b96.Normal * (cea91322cfd30caa062e8bdf867e4b412 / 2f);
		Vector3F c79a3b94c816fc4e87d6348c3713a = (cbd5f4ffb7f6caa84b20b23ef3ac88b62 ? (-c546c04f26f2e15538a50655eb6b77b96.Normal) : c546c04f26f2e15538a50655eb6b77b96.Normal);
		Contact cf1b03e8c15642a547bc4024e27883cdf = c9ae4fc785e8cd5184185f6ee6d5a3853.c8918c90b638b2cb75bae14c2683b291d(cfc73391dcf53c37de15d54e2434d0199, c2fed316f5bcf4dd4ea7883930f3822f, c79a3b94c816fc4e87d6348c3713a, cea91322cfd30caa062e8bdf867e4b412, c72057f882b90cac0b4126f84e6c9aa28: false);
		c9ae4fc785e8cd5184185f6ee6d5a3853.c6aedb1aa24c5098d4ce5c849eb999287(cfc73391dcf53c37de15d54e2434d0199, cf1b03e8c15642a547bc4024e27883cdf, cb76a1558f3778afde9d9fa562d4ee745, CollisionDetection.ContactPositionTolerance);
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
		if (CollisionDetection.ContinuousCollisionDetectionMode == ContinuousCollisionDetectionMode.Full)
		{
			return c33408081634d45382cc69064025f3c7f.c90c3411c13dad6b165001e39c0f00faf(objectA, targetPoseA, objectB, targetPoseB, allowedPenetration, CollisionDetection);
		}
		return c33408081634d45382cc69064025f3c7f.c6b01c99c514aea8ee7f1a805664d5e98(objectA, targetPoseA, objectB, targetPoseB, allowedPenetration, CollisionDetection);
	}
}
