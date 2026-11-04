using System;
using System.Collections.Generic;
using DigitalRune.Geometry;
using DigitalRune.Geometry.Collisions;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;

namespace A;

internal static class c9ae4fc785e8cd5184185f6ee6d5a3853
{
	public static Contact c8918c90b638b2cb75bae14c2683b291d(CollisionObject c6e4150c82a8b60982e648f0411087959, CollisionObject cb9065353c960dfabb2c6812b8400ab32, Vector3F c2fed316f5bcf4dd4ea7883930f3822f9, Vector3F c79a3b94c816fc4e87d6348c3713a1943, float cea91322cfd30caa062e8bdf867e4b412, bool c72057f882b90cac0b4126f84e6c9aa28)
	{
		Contact contact = Contact.Create();
		contact.Position = c2fed316f5bcf4dd4ea7883930f3822f9;
		contact.Normal = c79a3b94c816fc4e87d6348c3713a1943;
		contact.PenetrationDepth = cea91322cfd30caa062e8bdf867e4b412;
		contact.IsRayHit = c72057f882b90cac0b4126f84e6c9aa28;
		if (c72057f882b90cac0b4126f84e6c9aa28)
		{
			contact.PositionALocal = c6e4150c82a8b60982e648f0411087959.GeometricObject.Pose.ToLocalPosition(c2fed316f5bcf4dd4ea7883930f3822f9);
			contact.PositionBLocal = cb9065353c960dfabb2c6812b8400ab32.GeometricObject.Pose.ToLocalPosition(c2fed316f5bcf4dd4ea7883930f3822f9);
		}
		else
		{
			float num = cea91322cfd30caa062e8bdf867e4b412 / 2f;
			Vector3F vector3F = default;
			vector3F.X = c79a3b94c816fc4e87d6348c3713a1943.X * num;
			vector3F.Y = c79a3b94c816fc4e87d6348c3713a1943.Y * num;
			vector3F.Z = c79a3b94c816fc4e87d6348c3713a1943.Z * num;
			Pose pose = c6e4150c82a8b60982e648f0411087959.GeometricObject.Pose;
			Vector3F vector3F2 = default;
			vector3F2.X = c2fed316f5bcf4dd4ea7883930f3822f9.X + vector3F.X - pose.Position.X;
			vector3F2.Y = c2fed316f5bcf4dd4ea7883930f3822f9.Y + vector3F.Y - pose.Position.Y;
			vector3F2.Z = c2fed316f5bcf4dd4ea7883930f3822f9.Z + vector3F.Z - pose.Position.Z;
			Vector3F positionALocal = default;
			positionALocal.X = pose.Orientation.M00 * vector3F2.X + pose.Orientation.M10 * vector3F2.Y + pose.Orientation.M20 * vector3F2.Z;
			positionALocal.Y = pose.Orientation.M01 * vector3F2.X + pose.Orientation.M11 * vector3F2.Y + pose.Orientation.M21 * vector3F2.Z;
			positionALocal.Z = pose.Orientation.M02 * vector3F2.X + pose.Orientation.M12 * vector3F2.Y + pose.Orientation.M22 * vector3F2.Z;
			contact.PositionALocal = positionALocal;
			Pose pose2 = cb9065353c960dfabb2c6812b8400ab32.GeometricObject.Pose;
			Vector3F vector3F3 = default;
			vector3F3.X = c2fed316f5bcf4dd4ea7883930f3822f9.X - vector3F.X - pose2.Position.X;
			vector3F3.Y = c2fed316f5bcf4dd4ea7883930f3822f9.Y - vector3F.Y - pose2.Position.Y;
			vector3F3.Z = c2fed316f5bcf4dd4ea7883930f3822f9.Z - vector3F.Z - pose2.Position.Z;
			Vector3F positionBLocal = default;
			positionBLocal.X = pose2.Orientation.M00 * vector3F3.X + pose2.Orientation.M10 * vector3F3.Y + pose2.Orientation.M20 * vector3F3.Z;
			positionBLocal.Y = pose2.Orientation.M01 * vector3F3.X + pose2.Orientation.M11 * vector3F3.Y + pose2.Orientation.M21 * vector3F3.Z;
			positionBLocal.Z = pose2.Orientation.M02 * vector3F3.X + pose2.Orientation.M12 * vector3F3.Y + pose2.Orientation.M22 * vector3F3.Z;
			contact.PositionBLocal = positionBLocal;
		}
		return contact;
	}

	public static Contact c8918c90b638b2cb75bae14c2683b291d(ContactSet cfc73391dcf53c37de15d54e2434d0199, Vector3F c2fed316f5bcf4dd4ea7883930f3822f9, Vector3F c79a3b94c816fc4e87d6348c3713a1943, float cea91322cfd30caa062e8bdf867e4b412, bool c72057f882b90cac0b4126f84e6c9aa28)
	{
		return c8918c90b638b2cb75bae14c2683b291d(cfc73391dcf53c37de15d54e2434d0199.ObjectA, cfc73391dcf53c37de15d54e2434d0199.ObjectB, c2fed316f5bcf4dd4ea7883930f3822f9, c79a3b94c816fc4e87d6348c3713a1943, cea91322cfd30caa062e8bdf867e4b412, c72057f882b90cac0b4126f84e6c9aa28);
	}

	public static void c6aedb1aa24c5098d4ce5c849eb999287(ContactSet cfc73391dcf53c37de15d54e2434d0199, Contact cf1b03e8c15642a547bc4024e27883cdf, CollisionQueryType cb76a1558f3778afde9d9fa562d4ee745, float caf99328b31a375f0ddcb0eabd44f28ad)
	{
		if (cb76a1558f3778afde9d9fa562d4ee745 == CollisionQueryType.Contacts && cf1b03e8c15642a547bc4024e27883cdf.PenetrationDepth < 0f)
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
						__ldtoken(c9ae4fc785e8cd5184185f6ee6d5a3853.c6aedb1aa24c5098d4ce5c849eb999287(ContactSet, Contact, CollisionQueryType, float));
					}
					return;
				}
			}
		}
		if (cfc73391dcf53c37de15d54e2434d0199.Count == 0)
		{
			cfc73391dcf53c37de15d54e2434d0199.Add(cf1b03e8c15642a547bc4024e27883cdf);
			return;
		}
		if (cae137e1bc07a163993c4d262aa15bfdf(cfc73391dcf53c37de15d54e2434d0199, cf1b03e8c15642a547bc4024e27883cdf, caf99328b31a375f0ddcb0eabd44f28ad, cc99f063007543d0681282f3bc53054a1: true))
		{
			while (true)
			{
				switch (3)
				{
				case 0:
					break;
				default:
					cf1b03e8c15642a547bc4024e27883cdf.Recycle();
					return;
				}
			}
		}
		cfc73391dcf53c37de15d54e2434d0199.Add(cf1b03e8c15642a547bc4024e27883cdf);
	}

	public static void c6aedb1aa24c5098d4ce5c849eb999287(ContactSet cb6dfa807d7708d9673e5bf87e4bd0447, ContactSet c350086766c30a6e00ffacd8ad464ebbf, CollisionQueryType cb76a1558f3778afde9d9fa562d4ee745, float caf99328b31a375f0ddcb0eabd44f28ad)
	{
		int count = c350086766c30a6e00ffacd8ad464ebbf.Count;
		for (int i = 0; i < count; i++)
		{
			c6aedb1aa24c5098d4ce5c849eb999287(cb6dfa807d7708d9673e5bf87e4bd0447, c350086766c30a6e00ffacd8ad464ebbf[i], cb76a1558f3778afde9d9fa562d4ee745, caf99328b31a375f0ddcb0eabd44f28ad);
		}
		c350086766c30a6e00ffacd8ad464ebbf.Clear();
	}

	private static bool cae137e1bc07a163993c4d262aa15bfdf(ContactSet cfc73391dcf53c37de15d54e2434d0199, Contact c4858ff102f4ef50097c5a533c1478c21, float caf99328b31a375f0ddcb0eabd44f28ad, bool cc99f063007543d0681282f3bc53054a1)
	{
		int num = -1;
		bool flag = cfc73391dcf53c37de15d54e2434d0199.ObjectA.GeometricObject.Shape is RayShape || cfc73391dcf53c37de15d54e2434d0199.ObjectB.GeometricObject.Shape is RayShape;
		float num2 = caf99328b31a375f0ddcb0eabd44f28ad + Numeric.EpsilonF;
		float num3 = num2 * num2;
		int count = cfc73391dcf53c37de15d54e2434d0199.Count;
		for (int i = 0; i < count; i++)
		{
			Contact contact = cfc73391dcf53c37de15d54e2434d0199[i];
			if (c4858ff102f4ef50097c5a533c1478c21.FeatureA != contact.FeatureA)
			{
				continue;
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
			if (1 == 0)
			{
				__ldtoken(c9ae4fc785e8cd5184185f6ee6d5a3853.cae137e1bc07a163993c4d262aa15bfdf(ContactSet, Contact, float, bool));
			}
			if (c4858ff102f4ef50097c5a533c1478c21.FeatureB != contact.FeatureB)
			{
				continue;
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
			if (flag)
			{
				num = i;
				break;
			}
			float lengthSquared = (contact.Position - c4858ff102f4ef50097c5a533c1478c21.Position).LengthSquared;
			if (!(lengthSquared < num3))
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
			num3 = lengthSquared;
			num = i;
		}
		if (num >= 0)
		{
			if (cc99f063007543d0681282f3bc53054a1)
			{
				Contact contact2 = cfc73391dcf53c37de15d54e2434d0199[num];
				contact2.IsRayHit = c4858ff102f4ef50097c5a533c1478c21.IsRayHit;
				contact2.PositionALocal = c4858ff102f4ef50097c5a533c1478c21.PositionALocal;
				contact2.PositionBLocal = c4858ff102f4ef50097c5a533c1478c21.PositionBLocal;
				contact2.Normal = c4858ff102f4ef50097c5a533c1478c21.Normal;
				contact2.PenetrationDepth = c4858ff102f4ef50097c5a533c1478c21.PenetrationDepth;
				contact2.Position = c4858ff102f4ef50097c5a533c1478c21.Position;
			}
			return true;
		}
		return false;
	}

	public static void c7ea5a400e98aa00477faf1c0240ebaec(ContactSet cfc73391dcf53c37de15d54e2434d0199)
	{
		int count = cfc73391dcf53c37de15d54e2434d0199.Count;
		if (count > 1)
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
				__ldtoken(c9ae4fc785e8cd5184185f6ee6d5a3853.c7ea5a400e98aa00477faf1c0240ebaec(ContactSet));
			}
			Contact contact = cfc73391dcf53c37de15d54e2434d0199[0];
			for (int i = 1; i < count; i++)
			{
				if (cfc73391dcf53c37de15d54e2434d0199[i].PenetrationDepth > contact.PenetrationDepth)
				{
					contact = cfc73391dcf53c37de15d54e2434d0199[i];
				}
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
			using (List<Contact>.Enumerator enumerator = cfc73391dcf53c37de15d54e2434d0199.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Contact current = enumerator.Current;
					if (current != contact)
					{
						current.Recycle();
					}
				}
				while (true)
				{
					switch (5)
					{
					case 0:
						break;
					default:
						goto end_IL_0088;
					}
					continue;
					end_IL_0088:
					break;
				}
			}
			cfc73391dcf53c37de15d54e2434d0199.Clear();
			cfc73391dcf53c37de15d54e2434d0199.Add(contact);
		}
		if (!cfc73391dcf53c37de15d54e2434d0199.HaveContact || cfc73391dcf53c37de15d54e2434d0199.Count <= 0)
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
			if (!(cfc73391dcf53c37de15d54e2434d0199[0].PenetrationDepth < 0f))
			{
				return;
			}
			while (true)
			{
				switch (6)
				{
				case 0:
					continue;
				}
				cfc73391dcf53c37de15d54e2434d0199[0].Recycle();
				cfc73391dcf53c37de15d54e2434d0199.Clear();
				return;
			}
		}
	}

	public static void c8312ce88b68411fc3377bc0e963b6c59(ContactSet cfc73391dcf53c37de15d54e2434d0199)
	{
		bool haveContact = cfc73391dcf53c37de15d54e2434d0199.HaveContact;
		float num;
		if (!haveContact)
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
				__ldtoken(c9ae4fc785e8cd5184185f6ee6d5a3853.c8312ce88b68411fc3377bc0e963b6c59(ContactSet));
			}
			num = float.NegativeInfinity;
		}
		else
		{
			num = float.PositiveInfinity;
		}
		float num2 = num;
		Contact contact = null;
		int count = cfc73391dcf53c37de15d54e2434d0199.Count;
		for (int i = 0; i < count; i++)
		{
			Contact contact2 = cfc73391dcf53c37de15d54e2434d0199[i];
			float penetrationDepth = contact2.PenetrationDepth;
			if (haveContact)
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
				if (penetrationDepth >= 0f && penetrationDepth < num2)
				{
					contact = contact2;
					num2 = penetrationDepth;
				}
			}
			else
			{
				if (!(penetrationDepth > num2))
				{
					continue;
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
				contact = contact2;
				num2 = penetrationDepth;
			}
		}
		foreach (Contact item in cfc73391dcf53c37de15d54e2434d0199)
		{
			if (item == contact)
			{
				continue;
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
			item.Recycle();
		}
		cfc73391dcf53c37de15d54e2434d0199.Clear();
		if (contact == null)
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
			cfc73391dcf53c37de15d54e2434d0199.Add(contact);
			return;
		}
	}

	public static void cd4808b4cca49e2d5bdc52072c95767d2(ContactSet cfc73391dcf53c37de15d54e2434d0199, Vector3F c79a3b94c816fc4e87d6348c3713a1943, float c0fb26312bc7c0e36c0408bd1b7b51981)
	{
		for (int num = cfc73391dcf53c37de15d54e2434d0199.Count - 1; num >= 0; num--)
		{
			Contact contact = cfc73391dcf53c37de15d54e2434d0199[num];
			Vector3F normal = contact.Normal;
			float num2 = normal.X * c79a3b94c816fc4e87d6348c3713a1943.X + normal.Y * c79a3b94c816fc4e87d6348c3713a1943.Y + normal.Z * c79a3b94c816fc4e87d6348c3713a1943.Z;
			if (num2 < c0fb26312bc7c0e36c0408bd1b7b51981)
			{
				cfc73391dcf53c37de15d54e2434d0199.RemoveAt(num);
				contact.Recycle();
			}
		}
	}

	public static void c85fd3b007f86fc234e44b356b56c836c(ContactSet cfc73391dcf53c37de15d54e2434d0199)
	{
		for (int num = cfc73391dcf53c37de15d54e2434d0199.Count - 1; num >= 0; num--)
		{
			Contact contact = cfc73391dcf53c37de15d54e2434d0199[num];
			if (contact.PenetrationDepth < 0f)
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
					__ldtoken(c9ae4fc785e8cd5184185f6ee6d5a3853.c85fd3b007f86fc234e44b356b56c836c(ContactSet));
				}
				cfc73391dcf53c37de15d54e2434d0199.RemoveAt(num);
				contact.Recycle();
			}
		}
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

	public static void c4428c2d517a1c32aa278a32dae4fe2d3(ContactSet cfc73391dcf53c37de15d54e2434d0199, float ce8c71427a0828475a969c745c8335cde, float caf99328b31a375f0ddcb0eabd44f28ad)
	{
		int count = cfc73391dcf53c37de15d54e2434d0199.Count;
		if (count == 0)
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
						__ldtoken(c9ae4fc785e8cd5184185f6ee6d5a3853.c4428c2d517a1c32aa278a32dae4fe2d3(ContactSet, float, float));
					}
					return;
				}
			}
		}
		if (!cfc73391dcf53c37de15d54e2434d0199.ObjectA.Changed)
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
			if (!cfc73391dcf53c37de15d54e2434d0199.ObjectB.Changed)
			{
				for (int i = 0; i < count; i++)
				{
					cfc73391dcf53c37de15d54e2434d0199[i].Lifetime += ce8c71427a0828475a969c745c8335cde;
				}
				return;
			}
		}
		for (int num = count - 1; num >= 0; num--)
		{
			Contact contact = cfc73391dcf53c37de15d54e2434d0199[num];
			contact.Lifetime += ce8c71427a0828475a969c745c8335cde;
			if (c875c055980891761315762ca11b32256(cfc73391dcf53c37de15d54e2434d0199, contact, caf99328b31a375f0ddcb0eabd44f28ad))
			{
				cfc73391dcf53c37de15d54e2434d0199.RemoveAt(num);
				contact.Recycle();
			}
		}
	}

	private static bool c875c055980891761315762ca11b32256(ContactSet cfc73391dcf53c37de15d54e2434d0199, Contact c4858ff102f4ef50097c5a533c1478c21, float caf99328b31a375f0ddcb0eabd44f28ad)
	{
		Pose pose = cfc73391dcf53c37de15d54e2434d0199.ObjectA.GeometricObject.Pose;
		Pose pose2 = cfc73391dcf53c37de15d54e2434d0199.ObjectB.GeometricObject.Pose;
		Vector3F positionALocal = c4858ff102f4ef50097c5a533c1478c21.PositionALocal;
		Vector3F vector3F = default;
		vector3F.X = pose.Orientation.M00 * positionALocal.X + pose.Orientation.M01 * positionALocal.Y + pose.Orientation.M02 * positionALocal.Z + pose.Position.X;
		vector3F.Y = pose.Orientation.M10 * positionALocal.X + pose.Orientation.M11 * positionALocal.Y + pose.Orientation.M12 * positionALocal.Z + pose.Position.Y;
		vector3F.Z = pose.Orientation.M20 * positionALocal.X + pose.Orientation.M21 * positionALocal.Y + pose.Orientation.M22 * positionALocal.Z + pose.Position.Z;
		Vector3F positionBLocal = c4858ff102f4ef50097c5a533c1478c21.PositionBLocal;
		Vector3F vector3F2 = default;
		vector3F2.X = pose2.Orientation.M00 * positionBLocal.X + pose2.Orientation.M01 * positionBLocal.Y + pose2.Orientation.M02 * positionBLocal.Z + pose2.Position.X;
		vector3F2.Y = pose2.Orientation.M10 * positionBLocal.X + pose2.Orientation.M11 * positionBLocal.Y + pose2.Orientation.M12 * positionBLocal.Z + pose2.Position.Y;
		vector3F2.Z = pose2.Orientation.M20 * positionBLocal.X + pose2.Orientation.M21 * positionBLocal.Y + pose2.Orientation.M22 * positionBLocal.Z + pose2.Position.Z;
		c4858ff102f4ef50097c5a533c1478c21.Position = (vector3F + vector3F2) / 2f;
		if (c4858ff102f4ef50097c5a533c1478c21.PenetrationDepth >= 0f)
		{
			Vector3F vector3F3 = vector3F - vector3F2;
			if (!c4858ff102f4ef50097c5a533c1478c21.IsRayHit)
			{
				Vector3F normal = c4858ff102f4ef50097c5a533c1478c21.Normal;
				c4858ff102f4ef50097c5a533c1478c21.PenetrationDepth = vector3F3.X * normal.X + vector3F3.Y * normal.Y + vector3F3.Z * normal.Z;
			}
			else
			{
				RayShape rayShape = cfc73391dcf53c37de15d54e2434d0199.ObjectA.GeometricObject.Shape as RayShape;
				float x = cfc73391dcf53c37de15d54e2434d0199.ObjectA.GeometricObject.Scale.X;
				Vector3F vector3F4;
				if (rayShape != null)
				{
					vector3F4 = pose.ToLocalPosition(c4858ff102f4ef50097c5a533c1478c21.Position);
				}
				else
				{
					rayShape = cfc73391dcf53c37de15d54e2434d0199.ObjectB.GeometricObject.Shape as RayShape;
					x = cfc73391dcf53c37de15d54e2434d0199.ObjectB.GeometricObject.Scale.X;
					vector3F4 = pose2.ToLocalPosition(c4858ff102f4ef50097c5a533c1478c21.Position);
				}
				if (rayShape != null)
				{
					c4858ff102f4ef50097c5a533c1478c21.PenetrationDepth = Vector3F.Dot(vector3F4 - rayShape.Origin * x, rayShape.Direction);
					if (c4858ff102f4ef50097c5a533c1478c21.PenetrationDepth < 0f || c4858ff102f4ef50097c5a533c1478c21.PenetrationDepth > rayShape.Length * x)
					{
						return true;
					}
				}
			}
			if (c4858ff102f4ef50097c5a533c1478c21.PenetrationDepth < 0f)
			{
				return true;
			}
			float num = ((!c4858ff102f4ef50097c5a533c1478c21.IsRayHit) ? (vector3F3 - c4858ff102f4ef50097c5a533c1478c21.Normal * c4858ff102f4ef50097c5a533c1478c21.PenetrationDepth).LengthSquared : vector3F3.LengthSquared);
			return num > caf99328b31a375f0ddcb0eabd44f28ad * caf99328b31a375f0ddcb0eabd44f28ad;
		}
		Vector3F vector3F5 = vector3F2 - vector3F;
		c4858ff102f4ef50097c5a533c1478c21.PenetrationDepth = 0f - vector3F5.Length;
		if (Numeric.IsZero(c4858ff102f4ef50097c5a533c1478c21.PenetrationDepth))
		{
			return true;
		}
		c4858ff102f4ef50097c5a533c1478c21.Normal = vector3F5.Normalized;
		return false;
	}

	public static ContactSetCollection c89266090115e45c8d3b565d082daebcb(this IEnumerable<ContactSet> cbc664476e10ebecf958eb9fa7e209e05)
	{
		if (cbc664476e10ebecf958eb9fa7e209e05 is ContactSetCollection result)
		{
			while (true)
			{
				switch (6)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(c9ae4fc785e8cd5184185f6ee6d5a3853.c89266090115e45c8d3b565d082daebcb(IEnumerable<ContactSet>));
				}
				return result;
			}
		}
		return new ContactSetCollection(cbc664476e10ebecf958eb9fa7e209e05);
	}

	internal static void c4ed493b7ff558f5648011f2cd6007480(CollisionDetection c201f75b262cf6dc9b7933bb9ccf0d4f0, ContactSet cfc73391dcf53c37de15d54e2434d0199, bool c2eeb659e2bb86ef30af0a88192f0796c, Action<ContactSet> c05af26f718f1ace38c2c5ccfb47c122f)
	{
		if (!cfc73391dcf53c37de15d54e2434d0199.HaveContact || cfc73391dcf53c37de15d54e2434d0199.Count == 0 || cfc73391dcf53c37de15d54e2434d0199.Count >= 4 || !cfc73391dcf53c37de15d54e2434d0199.IsPerturbationTestAllowed)
		{
			return;
		}
		CollisionObject collisionObject = (c2eeb659e2bb86ef30af0a88192f0796c ? cfc73391dcf53c37de15d54e2434d0199.ObjectB : cfc73391dcf53c37de15d54e2434d0199.ObjectA);
		IGeometricObject geometricObject = collisionObject.GeometricObject;
		Pose pose = geometricObject.Pose;
		Vector3F vector3F = cfc73391dcf53c37de15d54e2434d0199[0].Normal;
		if (!c2eeb659e2bb86ef30af0a88192f0796c)
		{
			vector3F = -vector3F;
		}
		Vector3F position = cfc73391dcf53c37de15d54e2434d0199[0].Position;
		Vector3F vector = position - pose.Position;
		float length = geometricObject.Aabb.Extent.Length;
		float num = c201f75b262cf6dc9b7933bb9ccf0d4f0.ContactPositionTolerance / length;
		Vector3F vector3F2 = Vector3F.Cross(vector3F, vector);
		if (vector3F2.IsNumericallyZero)
		{
			return;
		}
		Vector3F axis = pose.ToLocalDirection(vector3F2);
		Matrix33F matrix33F = Matrix33F.CreateRotation(axis, 0f - num);
		c107cf2db36674a478eaf84e50d31337e c107cf2db36674a478eaf84e50d31337e2 = c107cf2db36674a478eaf84e50d31337e.c2d8ce8b1ccf2b905e3b18f8a5145fe8a();
		c107cf2db36674a478eaf84e50d31337e2.Shape = geometricObject.Shape;
		c107cf2db36674a478eaf84e50d31337e2.Scale = geometricObject.Scale;
		c107cf2db36674a478eaf84e50d31337e2.Pose = new Pose(pose.Position, pose.Orientation * matrix33F);
		CollisionObject collisionObject2 = cdb80ee76c96fc0751dea8d15ee69fa47.c29a5464f174eae366728cbb2edc64776.Obtain();
		collisionObject2.ceabf44cab475c8ec76f26d6553011a78(collisionObject, c107cf2db36674a478eaf84e50d31337e2);
		ContactSet contactSet = (c2eeb659e2bb86ef30af0a88192f0796c ? ContactSet.Create(cfc73391dcf53c37de15d54e2434d0199.ObjectA, collisionObject2) : ContactSet.Create(collisionObject2, cfc73391dcf53c37de15d54e2434d0199.ObjectB));
		contactSet.IsPerturbationTestAllowed = false;
		contactSet.c196da5f647797a182f2b6d32727c7621 = cfc73391dcf53c37de15d54e2434d0199.c196da5f647797a182f2b6d32727c7621;
		c05af26f718f1ace38c2c5ccfb47c122f(contactSet);
		if (contactSet.Count > 0)
		{
			Vector3F worldDirection = Vector3F.Cross(vector3F2, vector3F);
			Vector3F axis2 = pose.ToLocalDirection(worldDirection);
			Matrix33F matrix33F2 = Matrix33F.CreateRotation(axis2, 0f - num);
			c107cf2db36674a478eaf84e50d31337e2.Pose = new Pose(pose.Position, pose.Orientation * matrix33F2);
			c05af26f718f1ace38c2c5ccfb47c122f(contactSet);
			matrix33F2.Transpose();
			c107cf2db36674a478eaf84e50d31337e2.Pose = new Pose(pose.Position, pose.Orientation * matrix33F2);
			c05af26f718f1ace38c2c5ccfb47c122f(contactSet);
		}
		contactSet.HaveContact = true;
		c4428c2d517a1c32aa278a32dae4fe2d3(contactSet, 0f, c201f75b262cf6dc9b7933bb9ccf0d4f0.ContactPositionTolerance);
		foreach (Contact item in contactSet)
		{
			if (cae137e1bc07a163993c4d262aa15bfdf(cfc73391dcf53c37de15d54e2434d0199, item, c201f75b262cf6dc9b7933bb9ccf0d4f0.ContactPositionTolerance, cc99f063007543d0681282f3bc53054a1: false))
			{
				item.Recycle();
			}
			else
			{
				cfc73391dcf53c37de15d54e2434d0199.Add(item);
			}
		}
		contactSet.Recycle();
		cdb80ee76c96fc0751dea8d15ee69fa47.c29a5464f174eae366728cbb2edc64776.Recycle(collisionObject2);
		c107cf2db36674a478eaf84e50d31337e2.ca18b9b5faa9851d6355f617787bb5859();
	}
}
