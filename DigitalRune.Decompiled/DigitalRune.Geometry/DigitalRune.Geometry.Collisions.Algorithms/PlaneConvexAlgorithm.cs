using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using A;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Collisions.Algorithms;

/// <summary>
///       Computes contact or closest-point information for <see cref="T:DigitalRune.Geometry.Shapes.PlaneShape" /> vs. 
///       <see cref="T:DigitalRune.Geometry.Shapes.ConvexShape" />s.
///       </summary>
public class PlaneConvexAlgorithm : CollisionAlgorithm
{
	private readonly Action<ContactSet> c9e389e2926d430157c5786217c5365d6;

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Collisions.Algorithms.PlaneConvexAlgorithm" /> class.
	///       </summary>
	/// <param name="collisionDetection">The collision detection service.</param>
	public PlaneConvexAlgorithm(CollisionDetection collisionDetection)
	{
		Action<ContactSet> action = null;
		base._002Ector(collisionDetection);
		if (action == null)
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
				__ldtoken(PlaneConvexAlgorithm._002Ector(CollisionDetection));
			}
			action = (ContactSet cfc73391dcf53c37de15d54e2434d0199) =>
			{
				ComputeCollision(cfc73391dcf53c37de15d54e2434d0199, CollisionQueryType.Contacts);
			};
		}
		c9e389e2926d430157c5786217c5365d6 = action;
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
				switch (7)
				{
				case 0:
					continue;
				}
				break;
			}
			if (1 == 0)
			{
				__ldtoken(PlaneConvexAlgorithm.ComputeCollision(ContactSet, CollisionQueryType));
			}
			MathHelper.Swap(ref obj, ref obj2);
		}
		PlaneShape planeShape = obj.Shape as PlaneShape;
		ConvexShape convexShape = obj2.Shape as ConvexShape;
		if (planeShape != null)
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
			if (convexShape != null)
			{
				Vector3F ceabb47d7d959545d89c4300eabbdd7c = obj.Scale;
				Vector3F scale = obj2.Scale;
				Pose cbc1b7e0b011ad6dcdb8beb392ec7a = obj.Pose;
				Pose pose = obj2.Pose;
				Plane plane = new Plane(planeShape);
				plane.c22463d4a85fcdaa7d4ffbd8669084d72(ref ceabb47d7d959545d89c4300eabbdd7c);
				plane.c77d310c5ae14ed7e7ed360995b1867bb(ref cbc1b7e0b011ad6dcdb8beb392ec7a);
				Vector3F vector3F = pose.ToLocalDirection(plane.Normal);
				Vector3F supportPoint = convexShape.GetSupportPoint(-vector3F, scale);
				Vector3F vector3F2 = pose.ToWorldPosition(supportPoint);
				float num = Vector3F.Dot(vector3F2, plane.Normal);
				float num2 = plane.DistanceFromOrigin - num;
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
					}
					Vector3F c2fed316f5bcf4dd4ea7883930f3822f = vector3F2 + plane.Normal * (num2 / 2f);
					Vector3F vector3F3;
					if (!flag)
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
						vector3F3 = plane.Normal;
					}
					else
					{
						vector3F3 = -plane.Normal;
					}
					Vector3F c79a3b94c816fc4e87d6348c3713a = vector3F3;
					Contact cf1b03e8c15642a547bc4024e27883cdf = c9ae4fc785e8cd5184185f6ee6d5a3853.c8918c90b638b2cb75bae14c2683b291d(contactSet, c2fed316f5bcf4dd4ea7883930f3822f, c79a3b94c816fc4e87d6348c3713a, num2, c72057f882b90cac0b4126f84e6c9aa28: false);
					c9ae4fc785e8cd5184185f6ee6d5a3853.c6aedb1aa24c5098d4ce5c849eb999287(contactSet, cf1b03e8c15642a547bc4024e27883cdf, type, CollisionDetection.ContactPositionTolerance);
					if (!CollisionDetection.FullContactSetPerFrame)
					{
						return;
					}
					while (true)
					{
						switch (3)
						{
						case 0:
							continue;
						}
						if (type != CollisionQueryType.Contacts || contactSet.Count <= 0 || contactSet.Count >= 4)
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
							IList<Vector3F> list = null;
							if (convexShape is ConvexHullOfPoints)
							{
								ConvexHullOfPoints convexHullOfPoints = (ConvexHullOfPoints)convexShape;
								list = convexHullOfPoints.Points;
							}
							else if (convexShape is ConvexPolyhedron)
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
								ConvexPolyhedron convexPolyhedron = (ConvexPolyhedron)convexShape;
								list = convexPolyhedron.Vertices;
							}
							if (list != null && list.Count <= 8)
							{
								while (true)
								{
									switch (2)
									{
									case 0:
										break;
									default:
									{
										for (int i = 0; i < list.Count; i++)
										{
											Vector3F vector3F4 = list[i];
											Vector3F vector3F5 = vector3F4 * scale;
											if (vector3F5 != supportPoint)
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
												Vector3F vector3F6 = pose.ToWorldPosition(vector3F5);
												num = Vector3F.Dot(vector3F6, plane.Normal);
												num2 = plane.DistanceFromOrigin - num;
												if (num2 >= 0f)
												{
													c2fed316f5bcf4dd4ea7883930f3822f = vector3F6 + plane.Normal * (num2 / 2f);
													c79a3b94c816fc4e87d6348c3713a = (flag ? (-plane.Normal) : plane.Normal);
													cf1b03e8c15642a547bc4024e27883cdf = c9ae4fc785e8cd5184185f6ee6d5a3853.c8918c90b638b2cb75bae14c2683b291d(contactSet, c2fed316f5bcf4dd4ea7883930f3822f, c79a3b94c816fc4e87d6348c3713a, num2, c72057f882b90cac0b4126f84e6c9aa28: false);
													c9ae4fc785e8cd5184185f6ee6d5a3853.c6aedb1aa24c5098d4ce5c849eb999287(contactSet, cf1b03e8c15642a547bc4024e27883cdf, type, CollisionDetection.ContactPositionTolerance);
												}
											}
										}
										return;
									}
									}
								}
							}
							c9ae4fc785e8cd5184185f6ee6d5a3853.c4ed493b7ff558f5648011f2cd6007480(CollisionDetection, contactSet, !flag, c9e389e2926d430157c5786217c5365d6);
							return;
						}
					}
				}
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
		throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(11391), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1826));
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
			while (true)
			{
				switch (4)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(PlaneConvexAlgorithm.GetTimeOfImpact(CollisionObject, Pose, CollisionObject, Pose, float));
					}
					return c33408081634d45382cc69064025f3c7f.c90c3411c13dad6b165001e39c0f00faf(objectA, targetPoseA, objectB, targetPoseB, allowedPenetration, CollisionDetection);
				}
			}
		}
		return c33408081634d45382cc69064025f3c7f.c6b01c99c514aea8ee7f1a805664d5e98(objectA, targetPoseA, objectB, targetPoseB, allowedPenetration, CollisionDetection);
	}

	[CompilerGenerated]
	private void cde6b723243877cf27157a6099d285c48(ContactSet cfc73391dcf53c37de15d54e2434d0199)
	{
		ComputeCollision(cfc73391dcf53c37de15d54e2434d0199, CollisionQueryType.Contacts);
	}
}
