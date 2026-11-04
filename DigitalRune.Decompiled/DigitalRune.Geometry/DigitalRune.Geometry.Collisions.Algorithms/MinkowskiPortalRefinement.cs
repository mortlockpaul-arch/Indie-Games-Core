using System;
using System.Runtime.CompilerServices;
using A;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Collisions.Algorithms;

/// <summary>
///       The Minkowski Portal Refinement (MPR) algorithm for computing contacts between convex objects.
///       </summary>
public class MinkowskiPortalRefinement : CollisionAlgorithm
{
	private class ccb684145acdb14e6f082cbb9752cc53b
	{
		public Func<CollisionQueryType, ContactSet, Vector3F, Vector3F> ca3b37f0ec68bcfb53081f228d2907dd7;

		public Vector3F c2ff167fe185c10d3ae8de2d4859563e5;

		public readonly Action<ContactSet> ca9797c36f4af3fe2f0281606f33f3813;

		public ccb684145acdb14e6f082cbb9752cc53b()
		{
			Action<ContactSet> action = null;
			base._002Ector();
			if (action == null)
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
					__ldtoken(ccb684145acdb14e6f082cbb9752cc53b._002Ector());
				}
				action = (ContactSet cfc73391dcf53c37de15d54e2434d0199) =>
				{
					ca3b37f0ec68bcfb53081f228d2907dd7(CollisionQueryType.Contacts, cfc73391dcf53c37de15d54e2434d0199, c2ff167fe185c10d3ae8de2d4859563e5);
				};
			}
			ca9797c36f4af3fe2f0281606f33f3813 = action;
		}

		[CompilerGenerated]
		private void ccf6134e3b9d563d5b24933d5574bd94e(ContactSet cfc73391dcf53c37de15d54e2434d0199)
		{
			ca3b37f0ec68bcfb53081f228d2907dd7(CollisionQueryType.Contacts, cfc73391dcf53c37de15d54e2434d0199, c2ff167fe185c10d3ae8de2d4859563e5);
		}
	}

	private static readonly ResourcePool<ccb684145acdb14e6f082cbb9752cc53b> ce2240b1f45401358b6651ae1e84d6f3b;

	private readonly Func<CollisionQueryType, ContactSet, Vector3F, Vector3F> cc19056ed368ad5324e57700d67f48bc5;

	[CompilerGenerated]
	private static Func<ccb684145acdb14e6f082cbb9752cc53b> c2cd722d7de416d829aaf20a315c6553b;

	[CompilerGenerated]
	private static Action<ccb684145acdb14e6f082cbb9752cc53b> c19ae5197e8f1d77c5b9a5a5a3cba33d3;

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Collisions.Algorithms.MinkowskiPortalRefinement" /> class.
	///       </summary>
	/// <param name="collisionDetection">The collision detection service.</param>
	public MinkowskiPortalRefinement(CollisionDetection collisionDetection)
		: base(collisionDetection)
	{
		cc19056ed368ad5324e57700d67f48bc5 = cac867e234462f5e3ea88d21c80a14aa2;
	}

	static MinkowskiPortalRefinement()
	{
		if (c2cd722d7de416d829aaf20a315c6553b == null)
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
				__ldtoken(MinkowskiPortalRefinement._002Ecctor());
			}
			c2cd722d7de416d829aaf20a315c6553b = () => new ccb684145acdb14e6f082cbb9752cc53b();
		}
		ce2240b1f45401358b6651ae1e84d6f3b = new ResourcePool<ccb684145acdb14e6f082cbb9752cc53b>(c2cd722d7de416d829aaf20a315c6553b, null, (ccb684145acdb14e6f082cbb9752cc53b cb319138669e7653ae142770a40971439) =>
		{
			cb319138669e7653ae142770a40971439.ca3b37f0ec68bcfb53081f228d2907dd7 = null;
		});
	}

	/// <summary>
	///       Computes the collision. - This method should only be used by 
	///       <see cref="T:DigitalRune.Geometry.Collisions.Algorithms.CollisionAlgorithm" /> instances!
	///       </summary>
	/// <param name="contactSet">The contact set.</param>
	/// <param name="type">The type of collision query.</param>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="contactSet" /> does not contain two <see cref="T:DigitalRune.Geometry.Shapes.ConvexShape" />s.
	///       </exception>
	/// <exception cref="T:DigitalRune.Geometry.GeometryException">
	///   <paramref name="type" /> is set to <see cref="F:DigitalRune.Geometry.Collisions.CollisionQueryType.ClosestPoints" />. This 
	///       collision algorithm cannot handle closest-point queries. Use <see cref="T:DigitalRune.Geometry.Collisions.Algorithms.Gjk" /> instead.
	///       </exception>
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
						__ldtoken(MinkowskiPortalRefinement.ComputeCollision(ContactSet, CollisionQueryType));
					}
					throw new GeometryException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(15512));
				}
			}
		}
		CollisionObject objectA = contactSet.ObjectA;
		IGeometricObject geometricObject = objectA.GeometricObject;
		ConvexShape convexShape = geometricObject.Shape as ConvexShape;
		Vector3F scale = geometricObject.Scale;
		Pose pose = geometricObject.Pose;
		CollisionObject objectB = contactSet.ObjectB;
		IGeometricObject geometricObject2 = objectB.GeometricObject;
		ConvexShape convexShape2 = geometricObject2.Shape as ConvexShape;
		Vector3F scale2 = geometricObject2.Scale;
		Pose pose2 = geometricObject2.Pose;
		if (convexShape != null)
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
			if (convexShape2 != null)
			{
				contactSet.HaveContact = false;
				Vector3F c196da5f647797a182f2b6d32727c;
				if (contactSet.c27a62576bc69b101acc0b063aca9c47b)
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
					if (type == CollisionQueryType.Contacts)
					{
						c196da5f647797a182f2b6d32727c = contactSet.c196da5f647797a182f2b6d32727c7621;
						cac867e234462f5e3ea88d21c80a14aa2(type, contactSet, c196da5f647797a182f2b6d32727c);
						return;
					}
				}
				Vector3F vector3F = pose.ToWorldPosition(convexShape.InnerPoint * scale);
				Vector3F vector3F2 = pose2.ToWorldPosition(convexShape2.InnerPoint * scale2);
				c196da5f647797a182f2b6d32727c = vector3F2 - vector3F;
				if (c196da5f647797a182f2b6d32727c.IsNumericallyZero)
				{
					contactSet.HaveContact = true;
					if (type == CollisionQueryType.Boolean)
					{
						while (true)
						{
							switch (3)
							{
							case 0:
								break;
							default:
								return;
							}
						}
					}
					c196da5f647797a182f2b6d32727c.X = CollisionDetection.Epsilon / 10f;
				}
				int num = 0;
				ContactSet contactSet2 = ContactSet.Create(objectA, objectB);
				contactSet2.IsPerturbationTestAllowed = contactSet.IsPerturbationTestAllowed;
				contactSet2.c196da5f647797a182f2b6d32727c7621 = contactSet.c196da5f647797a182f2b6d32727c7621;
				Contact contact = null;
				Vector3F vector;
				do
				{
					vector = c196da5f647797a182f2b6d32727c;
					if (num == 0)
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
						vector.TryNormalize();
					}
					contactSet2.Clear();
					contactSet2.HaveContact = false;
					c196da5f647797a182f2b6d32727c = -cac867e234462f5e3ea88d21c80a14aa2(type, contactSet2, c196da5f647797a182f2b6d32727c);
					if (contactSet2.Count > 0)
					{
						Contact contact2 = contactSet2[0];
						if (contact != null && contact.PenetrationDepth < contact2.PenetrationDepth)
						{
							contact2.Recycle();
							contactSet2[0] = contact;
							break;
						}
						if (contact2 != contact)
						{
							if (contact != null)
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
								contact.Recycle();
							}
							contact = contact2;
						}
					}
					num++;
					if (!contactSet2.HaveContact)
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
						break;
					}
					if (num >= 10)
					{
						break;
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
					if (!(c196da5f647797a182f2b6d32727c != Vector3F.Zero))
					{
						break;
					}
					while (true)
					{
						switch (6)
						{
						case 0:
							break;
						default:
							goto end_IL_0271;
						}
						continue;
						end_IL_0271:
						break;
					}
				}
				while (!Vector3F.AreNumericallyEqual(-c196da5f647797a182f2b6d32727c, vector, CollisionDetection.Epsilon));
				if (contactSet2.Count > 0 && contactSet2[0] != contact)
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
					contact.Recycle();
					contact = null;
				}
				if (CollisionDetection.FullContactSetPerFrame)
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
					if (type == CollisionQueryType.Contacts && contactSet2.Count > 0)
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
						if (contactSet.Count < 3)
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
							ccb684145acdb14e6f082cbb9752cc53b ccb684145acdb14e6f082cbb9752cc53b2 = ce2240b1f45401358b6651ae1e84d6f3b.Obtain();
							ccb684145acdb14e6f082cbb9752cc53b2.ca3b37f0ec68bcfb53081f228d2907dd7 = cc19056ed368ad5324e57700d67f48bc5;
							ccb684145acdb14e6f082cbb9752cc53b2.c2ff167fe185c10d3ae8de2d4859563e5 = contactSet2[0].Normal;
							c9ae4fc785e8cd5184185f6ee6d5a3853.c4ed493b7ff558f5648011f2cd6007480(CollisionDetection, contactSet2, c2eeb659e2bb86ef30af0a88192f0796c: true, ccb684145acdb14e6f082cbb9752cc53b2.ca9797c36f4af3fe2f0281606f33f3813);
							ce2240b1f45401358b6651ae1e84d6f3b.Recycle(ccb684145acdb14e6f082cbb9752cc53b2);
						}
					}
				}
				contactSet.HaveContact = contactSet2.HaveContact;
				c9ae4fc785e8cd5184185f6ee6d5a3853.c6aedb1aa24c5098d4ce5c849eb999287(contactSet, contactSet2, type, CollisionDetection.ContactPositionTolerance);
				contactSet2.Recycle();
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
		throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(15627), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1826));
	}

	private Vector3F cac867e234462f5e3ea88d21c80a14aa2(CollisionQueryType cb76a1558f3778afde9d9fa562d4ee745, ContactSet cfc73391dcf53c37de15d54e2434d0199, Vector3F cbe6829ca974177ec6282270ed6d4e5c9)
	{
		int num = 0;
		CollisionObject objectA = cfc73391dcf53c37de15d54e2434d0199.ObjectA;
		IGeometricObject geometricObject = objectA.GeometricObject;
		ConvexShape convexShape = (ConvexShape)geometricObject.Shape;
		Vector3F scale = geometricObject.Scale;
		Pose pose = geometricObject.Pose;
		CollisionObject objectB = cfc73391dcf53c37de15d54e2434d0199.ObjectB;
		IGeometricObject geometricObject2 = objectB.GeometricObject;
		ConvexShape convexShape2 = (ConvexShape)geometricObject2.Shape;
		Vector3F scale2 = geometricObject2.Scale;
		Pose pose2 = geometricObject2.Pose;
		Matrix33F transposed = pose.Orientation.Transposed;
		Matrix33F transposed2 = pose2.Orientation.Transposed;
		Vector3F vector3F = -cbe6829ca974177ec6282270ed6d4e5c9;
		Vector3F obj = pose.ToWorldPosition(convexShape.GetSupportPoint(transposed * -vector3F, scale));
		Vector3F obj2 = pose2.ToWorldPosition(convexShape2.GetSupportPoint(transposed2 * vector3F, scale2));
		Vector3F obj3 = obj2 - obj;
		if (Vector3F.Dot(obj3, vector3F) < 0f)
		{
			return Vector3F.Zero;
		}
		vector3F = Vector3F.Cross(obj3, cbe6829ca974177ec6282270ed6d4e5c9);
		if (vector3F.IsNumericallyZero)
		{
			cfc73391dcf53c37de15d54e2434d0199.HaveContact = true;
			if (cb76a1558f3778afde9d9fa562d4ee745 == CollisionQueryType.Boolean)
			{
				return Vector3F.Zero;
			}
			Vector3F c79a3b94c816fc4e87d6348c3713a = cbe6829ca974177ec6282270ed6d4e5c9 - obj3;
			if (!c79a3b94c816fc4e87d6348c3713a.TryNormalize())
			{
				c79a3b94c816fc4e87d6348c3713a = Vector3F.UnitY;
			}
			Vector3F c2fed316f5bcf4dd4ea7883930f3822f = (obj + obj2) / 2f;
			float length = obj3.Length;
			Contact cf1b03e8c15642a547bc4024e27883cdf = c9ae4fc785e8cd5184185f6ee6d5a3853.c8918c90b638b2cb75bae14c2683b291d(cfc73391dcf53c37de15d54e2434d0199, c2fed316f5bcf4dd4ea7883930f3822f, c79a3b94c816fc4e87d6348c3713a, length, c72057f882b90cac0b4126f84e6c9aa28: false);
			c9ae4fc785e8cd5184185f6ee6d5a3853.c6aedb1aa24c5098d4ce5c849eb999287(cfc73391dcf53c37de15d54e2434d0199, cf1b03e8c15642a547bc4024e27883cdf, cb76a1558f3778afde9d9fa562d4ee745, CollisionDetection.ContactPositionTolerance);
			return Vector3F.Zero;
		}
		Vector3F obj4 = pose.ToWorldPosition(convexShape.GetSupportPoint(transposed * -vector3F, scale));
		Vector3F obj5 = pose2.ToWorldPosition(convexShape2.GetSupportPoint(transposed2 * vector3F, scale2));
		Vector3F obj6 = obj5 - obj4;
		if (Vector3F.Dot(obj6, vector3F) < 0f)
		{
			return Vector3F.Zero;
		}
		vector3F = Vector3F.Cross(obj3 - cbe6829ca974177ec6282270ed6d4e5c9, obj6 - cbe6829ca974177ec6282270ed6d4e5c9);
		if (Vector3F.Dot(vector3F, cbe6829ca974177ec6282270ed6d4e5c9) > 0f)
		{
			MathHelper.Swap(ref obj3, ref obj6);
			MathHelper.Swap(ref obj, ref obj4);
			MathHelper.Swap(ref obj2, ref obj5);
			vector3F = -vector3F;
		}
		if (vector3F.IsNumericallyZero)
		{
			return Vector3F.Zero;
		}
		Vector3F direction = default;
		Vector3F vector3F2 = default;
		Vector3F direction2 = default;
		Vector3F vector3F3 = default;
		Vector3F vector3F4;
		Vector3F vector3F5 = default;
		Vector3F vector3F6 = default;
		Vector3F vector3F7 = default;
		Vector3F vector3F8 = default;
		while (true)
		{
			num++;
			if (num > 100)
			{
				return Vector3F.Zero;
			}
			direction.X = 0f - (transposed.M00 * vector3F.X + transposed.M01 * vector3F.Y + transposed.M02 * vector3F.Z);
			direction.Y = 0f - (transposed.M10 * vector3F.X + transposed.M11 * vector3F.Y + transposed.M12 * vector3F.Z);
			direction.Z = 0f - (transposed.M20 * vector3F.X + transposed.M21 * vector3F.Y + transposed.M22 * vector3F.Z);
			Vector3F supportPoint = convexShape.GetSupportPoint(direction, scale);
			vector3F2.X = pose.Orientation.M00 * supportPoint.X + pose.Orientation.M01 * supportPoint.Y + pose.Orientation.M02 * supportPoint.Z + pose.Position.X;
			vector3F2.Y = pose.Orientation.M10 * supportPoint.X + pose.Orientation.M11 * supportPoint.Y + pose.Orientation.M12 * supportPoint.Z + pose.Position.Y;
			vector3F2.Z = pose.Orientation.M20 * supportPoint.X + pose.Orientation.M21 * supportPoint.Y + pose.Orientation.M22 * supportPoint.Z + pose.Position.Z;
			direction2.X = transposed2.M00 * vector3F.X + transposed2.M01 * vector3F.Y + transposed2.M02 * vector3F.Z;
			direction2.Y = transposed2.M10 * vector3F.X + transposed2.M11 * vector3F.Y + transposed2.M12 * vector3F.Z;
			direction2.Z = transposed2.M20 * vector3F.X + transposed2.M21 * vector3F.Y + transposed2.M22 * vector3F.Z;
			Vector3F supportPoint2 = convexShape2.GetSupportPoint(direction2, scale2);
			vector3F3.X = pose2.Orientation.M00 * supportPoint2.X + pose2.Orientation.M01 * supportPoint2.Y + pose2.Orientation.M02 * supportPoint2.Z + pose2.Position.X;
			vector3F3.Y = pose2.Orientation.M10 * supportPoint2.X + pose2.Orientation.M11 * supportPoint2.Y + pose2.Orientation.M12 * supportPoint2.Z + pose2.Position.Y;
			vector3F3.Z = pose2.Orientation.M20 * supportPoint2.X + pose2.Orientation.M21 * supportPoint2.Y + pose2.Orientation.M22 * supportPoint2.Z + pose2.Position.Z;
			vector3F4 = vector3F3 - vector3F2;
			if (vector3F4.X * vector3F.X + vector3F4.Y * vector3F.Y + vector3F4.Z * vector3F.Z < 0f)
			{
				return Vector3F.Zero;
			}
			if ((obj3.Y * vector3F4.Z - obj3.Z * vector3F4.Y) * cbe6829ca974177ec6282270ed6d4e5c9.X + (obj3.Z * vector3F4.X - obj3.X * vector3F4.Z) * cbe6829ca974177ec6282270ed6d4e5c9.Y + (obj3.X * vector3F4.Y - obj3.Y * vector3F4.X) * cbe6829ca974177ec6282270ed6d4e5c9.Z < 0f)
			{
				obj6 = vector3F4;
				obj4 = vector3F2;
				obj5 = vector3F3;
				vector3F5.X = obj3.X - cbe6829ca974177ec6282270ed6d4e5c9.X;
				vector3F5.Y = obj3.Y - cbe6829ca974177ec6282270ed6d4e5c9.Y;
				vector3F5.Z = obj3.Z - cbe6829ca974177ec6282270ed6d4e5c9.Z;
				vector3F6.X = vector3F4.X - cbe6829ca974177ec6282270ed6d4e5c9.X;
				vector3F6.Y = vector3F4.Y - cbe6829ca974177ec6282270ed6d4e5c9.Y;
				vector3F6.Z = vector3F4.Z - cbe6829ca974177ec6282270ed6d4e5c9.Z;
				vector3F.X = vector3F5.Y * vector3F6.Z - vector3F5.Z * vector3F6.Y;
				vector3F.Y = vector3F5.Z * vector3F6.X - vector3F5.X * vector3F6.Z;
				vector3F.Z = vector3F5.X * vector3F6.Y - vector3F5.Y * vector3F6.X;
				continue;
			}
			if (!((vector3F4.Y * obj6.Z - vector3F4.Z * obj6.Y) * cbe6829ca974177ec6282270ed6d4e5c9.X + (vector3F4.Z * obj6.X - vector3F4.X * obj6.Z) * cbe6829ca974177ec6282270ed6d4e5c9.Y + (vector3F4.X * obj6.Y - vector3F4.Y * obj6.X) * cbe6829ca974177ec6282270ed6d4e5c9.Z < 0f))
			{
				break;
			}
			obj3 = vector3F4;
			obj = vector3F2;
			obj2 = vector3F3;
			vector3F7.X = vector3F4.X - cbe6829ca974177ec6282270ed6d4e5c9.X;
			vector3F7.Y = vector3F4.Y - cbe6829ca974177ec6282270ed6d4e5c9.Y;
			vector3F7.Z = vector3F4.Z - cbe6829ca974177ec6282270ed6d4e5c9.Z;
			vector3F8.X = obj6.X - cbe6829ca974177ec6282270ed6d4e5c9.X;
			vector3F8.Y = obj6.Y - cbe6829ca974177ec6282270ed6d4e5c9.Y;
			vector3F8.Z = obj6.Z - cbe6829ca974177ec6282270ed6d4e5c9.Z;
			vector3F.X = vector3F7.Y * vector3F8.Z - vector3F7.Z * vector3F8.Y;
			vector3F.Y = vector3F7.Z * vector3F8.X - vector3F7.X * vector3F8.Z;
			vector3F.Z = vector3F7.X * vector3F8.Y - vector3F7.Y * vector3F8.X;
		}
		Vector3F vector3F9 = default;
		Vector3F vector3F10 = default;
		Vector3F direction3 = default;
		Vector3F vector3F14 = default;
		Vector3F direction4 = default;
		Vector3F vector3F15 = default;
		Vector3F vector3F17 = default;
		while (true)
		{
			num++;
			Vector3F vector = vector3F;
			vector3F9.X = obj6.X - obj3.X;
			vector3F9.Y = obj6.Y - obj3.Y;
			vector3F9.Z = obj6.Z - obj3.Z;
			vector3F10.X = vector3F4.X - obj3.X;
			vector3F10.Y = vector3F4.Y - obj3.Y;
			vector3F10.Z = vector3F4.Z - obj3.Z;
			vector3F.X = vector3F9.Y * vector3F10.Z - vector3F9.Z * vector3F10.Y;
			vector3F.Y = vector3F9.Z * vector3F10.X - vector3F9.X * vector3F10.Z;
			vector3F.Z = vector3F9.X * vector3F10.Y - vector3F9.Y * vector3F10.X;
			float lengthSquared = vector3F.LengthSquared;
			if (lengthSquared < Numeric.EpsilonFSquared)
			{
				cfc73391dcf53c37de15d54e2434d0199.HaveContact = true;
				if (cb76a1558f3778afde9d9fa562d4ee745 == CollisionQueryType.Boolean)
				{
					return Vector3F.Zero;
				}
				GeometryHelper.GetClosestPoint(new Triangle(obj3, obj6, vector3F4), Vector3F.Zero, out var u, out var v, out var w);
				Vector3F vector3F11 = u * obj3 + v * obj6 + w * vector3F4;
				Vector3F vector3F12 = u * obj + v * obj4 + w * vector3F2;
				Vector3F vector3F13 = u * obj2 + v * obj5 + w * vector3F3;
				Vector3F c79a3b94c816fc4e87d6348c3713a2 = vector3F12 - vector3F13;
				if (!c79a3b94c816fc4e87d6348c3713a2.TryNormalize())
				{
					c79a3b94c816fc4e87d6348c3713a2 = ((!cfc73391dcf53c37de15d54e2434d0199.c27a62576bc69b101acc0b063aca9c47b) ? Vector3F.UnitY : cfc73391dcf53c37de15d54e2434d0199.c196da5f647797a182f2b6d32727c7621);
				}
				Vector3F c2fed316f5bcf4dd4ea7883930f3822f2 = (vector3F12 + vector3F13) / 2f;
				float length2 = vector3F11.Length;
				Contact cf1b03e8c15642a547bc4024e27883cdf2 = c9ae4fc785e8cd5184185f6ee6d5a3853.c8918c90b638b2cb75bae14c2683b291d(cfc73391dcf53c37de15d54e2434d0199, c2fed316f5bcf4dd4ea7883930f3822f2, c79a3b94c816fc4e87d6348c3713a2, length2, c72057f882b90cac0b4126f84e6c9aa28: false);
				c9ae4fc785e8cd5184185f6ee6d5a3853.c6aedb1aa24c5098d4ce5c849eb999287(cfc73391dcf53c37de15d54e2434d0199, cf1b03e8c15642a547bc4024e27883cdf2, cb76a1558f3778afde9d9fa562d4ee745, CollisionDetection.ContactPositionTolerance);
				return Vector3F.Zero;
			}
			float num2 = (float)Math.Sqrt(lengthSquared);
			float num3 = 1f / num2;
			vector3F.X *= num3;
			vector3F.Y *= num3;
			vector3F.Z *= num3;
			if (!cfc73391dcf53c37de15d54e2434d0199.HaveContact && obj3.X * vector3F.X + obj3.Y * vector3F.Y + obj3.Z * vector3F.Z >= 0f)
			{
				cfc73391dcf53c37de15d54e2434d0199.HaveContact = true;
				if (cb76a1558f3778afde9d9fa562d4ee745 == CollisionQueryType.Boolean)
				{
					return Vector3F.Zero;
				}
			}
			direction3.X = 0f - (transposed.M00 * vector3F.X + transposed.M01 * vector3F.Y + transposed.M02 * vector3F.Z);
			direction3.Y = 0f - (transposed.M10 * vector3F.X + transposed.M11 * vector3F.Y + transposed.M12 * vector3F.Z);
			direction3.Z = 0f - (transposed.M20 * vector3F.X + transposed.M21 * vector3F.Y + transposed.M22 * vector3F.Z);
			Vector3F supportPoint3 = convexShape.GetSupportPoint(direction3, scale);
			vector3F14.X = pose.Orientation.M00 * supportPoint3.X + pose.Orientation.M01 * supportPoint3.Y + pose.Orientation.M02 * supportPoint3.Z + pose.Position.X;
			vector3F14.Y = pose.Orientation.M10 * supportPoint3.X + pose.Orientation.M11 * supportPoint3.Y + pose.Orientation.M12 * supportPoint3.Z + pose.Position.Y;
			vector3F14.Z = pose.Orientation.M20 * supportPoint3.X + pose.Orientation.M21 * supportPoint3.Y + pose.Orientation.M22 * supportPoint3.Z + pose.Position.Z;
			direction4.X = transposed2.M00 * vector3F.X + transposed2.M01 * vector3F.Y + transposed2.M02 * vector3F.Z;
			direction4.Y = transposed2.M10 * vector3F.X + transposed2.M11 * vector3F.Y + transposed2.M12 * vector3F.Z;
			direction4.Z = transposed2.M20 * vector3F.X + transposed2.M21 * vector3F.Y + transposed2.M22 * vector3F.Z;
			Vector3F supportPoint4 = convexShape2.GetSupportPoint(direction4, scale2);
			vector3F15.X = pose2.Orientation.M00 * supportPoint4.X + pose2.Orientation.M01 * supportPoint4.Y + pose2.Orientation.M02 * supportPoint4.Z + pose2.Position.X;
			vector3F15.Y = pose2.Orientation.M10 * supportPoint4.X + pose2.Orientation.M11 * supportPoint4.Y + pose2.Orientation.M12 * supportPoint4.Z + pose2.Position.Y;
			vector3F15.Z = pose2.Orientation.M20 * supportPoint4.X + pose2.Orientation.M21 * supportPoint4.Y + pose2.Orientation.M22 * supportPoint4.Z + pose2.Position.Z;
			Vector3F vector3F16 = vector3F15 - vector3F14;
			if (!cfc73391dcf53c37de15d54e2434d0199.HaveContact && vector3F16.X * vector3F.X + vector3F16.Y * vector3F.Y + vector3F16.Z * vector3F.Z < 0f)
			{
				return Vector3F.Zero;
			}
			if ((vector3F16.X - vector3F4.X) * vector3F.X + (vector3F16.Y - vector3F4.Y) * vector3F.Y + (vector3F16.Z - vector3F4.Z) * vector3F.Z <= CollisionDetection.Epsilon || Vector3F.AreNumericallyEqual(vector3F, vector) || num >= 100)
			{
				break;
			}
			vector3F17.X = vector3F16.Y * cbe6829ca974177ec6282270ed6d4e5c9.Z - vector3F16.Z * cbe6829ca974177ec6282270ed6d4e5c9.Y;
			vector3F17.Y = vector3F16.Z * cbe6829ca974177ec6282270ed6d4e5c9.X - vector3F16.X * cbe6829ca974177ec6282270ed6d4e5c9.Z;
			vector3F17.Z = vector3F16.X * cbe6829ca974177ec6282270ed6d4e5c9.Y - vector3F16.Y * cbe6829ca974177ec6282270ed6d4e5c9.X;
			if (obj3.X * vector3F17.X + obj3.Y * vector3F17.Y + obj3.Z * vector3F17.Z > 0f)
			{
				if (obj6.X * vector3F17.X + obj6.Y * vector3F17.Y + obj6.Z * vector3F17.Z > 0f)
				{
					obj3 = vector3F16;
					obj = vector3F14;
					obj2 = vector3F15;
				}
				else
				{
					vector3F4 = vector3F16;
					vector3F2 = vector3F14;
					vector3F3 = vector3F15;
				}
			}
			else if (vector3F4.X * vector3F17.X + vector3F4.Y * vector3F17.Y + vector3F4.Z * vector3F17.Z > 0f)
			{
				obj6 = vector3F16;
				obj4 = vector3F14;
				obj5 = vector3F15;
			}
			else
			{
				obj3 = vector3F16;
				obj = vector3F14;
				obj2 = vector3F15;
			}
		}
		if (!cfc73391dcf53c37de15d54e2434d0199.HaveContact)
		{
			return Vector3F.Zero;
		}
		if (cb76a1558f3778afde9d9fa562d4ee745 == CollisionQueryType.Boolean)
		{
			return Vector3F.Zero;
		}
		GeometryHelper.GetClosestPoint(new Triangle(obj3, obj6, vector3F4), Vector3F.Zero, out var u2, out var v2, out var w2);
		Vector3F vector3F18 = u2 * obj3 + v2 * obj6 + w2 * vector3F4;
		Vector3F vector3F19 = u2 * obj + v2 * obj4 + w2 * vector3F2;
		Vector3F vector3F20 = u2 * obj2 + v2 * obj5 + w2 * vector3F3;
		Vector3F c79a3b94c816fc4e87d6348c3713a3 = vector3F19 - vector3F20;
		if (!c79a3b94c816fc4e87d6348c3713a3.TryNormalize())
		{
			c79a3b94c816fc4e87d6348c3713a3 = -vector3F;
		}
		Vector3F c2fed316f5bcf4dd4ea7883930f3822f3 = (vector3F19 + vector3F20) / 2f;
		float length3 = vector3F18.Length;
		Contact cf1b03e8c15642a547bc4024e27883cdf3 = c9ae4fc785e8cd5184185f6ee6d5a3853.c8918c90b638b2cb75bae14c2683b291d(cfc73391dcf53c37de15d54e2434d0199, c2fed316f5bcf4dd4ea7883930f3822f3, c79a3b94c816fc4e87d6348c3713a3, length3, c72057f882b90cac0b4126f84e6c9aa28: false);
		c9ae4fc785e8cd5184185f6ee6d5a3853.c6aedb1aa24c5098d4ce5c849eb999287(cfc73391dcf53c37de15d54e2434d0199, cf1b03e8c15642a547bc4024e27883cdf3, cb76a1558f3778afde9d9fa562d4ee745, CollisionDetection.ContactPositionTolerance);
		if (u2 == 0f || v2 == 0f || w2 == 0f || u2 == 1f || v2 == 1f || w2 == 1f)
		{
			return vector3F;
		}
		return Vector3F.Zero;
	}

	[CompilerGenerated]
	private static ccb684145acdb14e6f082cbb9752cc53b c4bffc38c4054efe6a3f3ff877749c085()
	{
		return new ccb684145acdb14e6f082cbb9752cc53b();
	}

	[CompilerGenerated]
	private static void c4c5cc4eb6785fd907a1974db5b42eeb5(ccb684145acdb14e6f082cbb9752cc53b cb319138669e7653ae142770a40971439)
	{
		cb319138669e7653ae142770a40971439.ca3b37f0ec68bcfb53081f228d2907dd7 = null;
	}
}
