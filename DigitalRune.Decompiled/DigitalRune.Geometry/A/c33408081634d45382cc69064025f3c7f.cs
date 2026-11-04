using System;
using System.Collections.Generic;
using DigitalRune.Geometry;
using DigitalRune.Geometry.Collisions;
using DigitalRune.Geometry.Collisions.Algorithms;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;

namespace A;

internal static class c33408081634d45382cc69064025f3c7f
{
	private const int ccc81a89e5837c5d9e080ee22b5a22784 = 64;

	internal static float ca9ba452be1650d033669f0368c6e76e7(CollisionObject c6e4150c82a8b60982e648f0411087959, Pose ce62c464030ecc0a6d92c1401aa8953ba, CollisionObject cb9065353c960dfabb2c6812b8400ab32, Pose c01837a72cf7ae926712c07d07b9ad285, float cf7e17649ec54ab6d9098dda36235a236)
	{
		if (c6e4150c82a8b60982e648f0411087959 == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1796));
		}
		if (cb9065353c960dfabb2c6812b8400ab32 == null)
		{
			while (true)
			{
				switch (6)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(c33408081634d45382cc69064025f3c7f.ca9ba452be1650d033669f0368c6e76e7(CollisionObject, Pose, CollisionObject, Pose, float));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1811));
				}
			}
		}
		IGeometricObject geometricObject = c6e4150c82a8b60982e648f0411087959.GeometricObject;
		IGeometricObject geometricObject2 = cb9065353c960dfabb2c6812b8400ab32.GeometricObject;
		ConvexShape convexShape = geometricObject.Shape as ConvexShape;
		ConvexShape convexShape2 = geometricObject2.Shape as ConvexShape;
		if (convexShape != null)
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
			if (convexShape2 != null)
			{
				Pose pose = geometricObject.Pose;
				Pose pose2 = geometricObject2.Pose;
				Vector3F vector3F = ce62c464030ecc0a6d92c1401aa8953ba.Position - pose.Position;
				Vector3F vector3F2 = c01837a72cf7ae926712c07d07b9ad285.Position - pose2.Position;
				Vector3F vector3F3 = vector3F - vector3F2;
				float length = vector3F3.Length;
				if (Numeric.IsZero(length))
				{
					return 1f;
				}
				cc49dcd41ee82253a805139c508e0926f cc49dcd41ee82253a805139c508e0926f2 = cc49dcd41ee82253a805139c508e0926f.c2d8ce8b1ccf2b905e3b18f8a5145fe8a();
				try
				{
					Vector3F scale = geometricObject.Scale;
					Vector3F scale2 = geometricObject2.Scale;
					Pose pose3 = pose;
					Pose pose4 = pose2;
					Vector3F vector3F4 = vector3F3;
					float num = 0f;
					Vector3F vector3F5 = pose3.ToWorldPosition(convexShape.GetSupportPoint(pose3.ToLocalDirection(-vector3F4), scale));
					Vector3F vector3F6 = pose4.ToWorldPosition(convexShape2.GetSupportPoint(pose4.ToLocalDirection(vector3F4), scale2));
					Vector3F vector3F7 = vector3F5 - vector3F6;
					float num2 = vector3F7.LengthSquared;
					int num3 = 0;
					float num6;
					for (; num2 > Numeric.EpsilonF; num2 = num6)
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
						if (num3 >= 64)
						{
							break;
						}
						num3++;
						Vector3F vector3F8 = vector3F7;
						vector3F8.TryNormalize();
						Vector3F vector3F9 = pose3.ToLocalDirection(-vector3F8);
						Vector3F vector3F10 = pose4.ToLocalDirection(vector3F8);
						vector3F5 = pose3.ToWorldPosition(convexShape.GetSupportPoint(vector3F9, scale) - 0.5f * vector3F9 * cf7e17649ec54ab6d9098dda36235a236);
						vector3F6 = pose4.ToWorldPosition(convexShape2.GetSupportPoint(vector3F10, scale2) - 0.5f * vector3F10 * cf7e17649ec54ab6d9098dda36235a236);
						Vector3F vector3F11 = vector3F5 - vector3F6;
						float num4 = Vector3F.Dot(vector3F7, vector3F11);
						if (num4 > 0f)
						{
							float num5 = Vector3F.Dot(vector3F7, vector3F4);
							if (num5 >= 0f - Numeric.EpsilonF)
							{
								return 1f;
							}
							num -= num4 / num5;
							pose3.Position = pose.Position + num * (ce62c464030ecc0a6d92c1401aa8953ba.Position - pose.Position);
							pose4.Position = pose2.Position + num * (c01837a72cf7ae926712c07d07b9ad285.Position - pose2.Position);
						}
						if (!cc49dcd41ee82253a805139c508e0926f2.ce475b78d124a2feef9cd2aab44c161c4(vector3F11))
						{
							cc49dcd41ee82253a805139c508e0926f2.c03d72a32723924cb5b81c585d5df7d9d(vector3F11, vector3F5, vector3F6);
						}
						cc49dcd41ee82253a805139c508e0926f2.c322ef1ce205c6dbcf3a93ad899943435();
						vector3F7 = cc49dcd41ee82253a805139c508e0926f2.c596154f5b0c243d05e4fa792fd81b4bb;
						if (cc49dcd41ee82253a805139c508e0926f2.IsValid)
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
							if (!cc49dcd41ee82253a805139c508e0926f2.c8cfa49e753c046fa77edb85a6fcf2975)
							{
								num6 = vector3F7.LengthSquared;
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
						}
						num6 = 0f;
					}
					if (0f < num && num <= 1f)
					{
						return num;
					}
				}
				finally
				{
					cc49dcd41ee82253a805139c508e0926f2.ca18b9b5faa9851d6355f617787bb5859();
				}
				return 1f;
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
		throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4953));
	}

	internal static float c6b01c99c514aea8ee7f1a805664d5e98(CollisionObject c6e4150c82a8b60982e648f0411087959, Pose ce62c464030ecc0a6d92c1401aa8953ba, CollisionObject cb9065353c960dfabb2c6812b8400ab32, Pose c01837a72cf7ae926712c07d07b9ad285, float cf7e17649ec54ab6d9098dda36235a236, CollisionDetection c201f75b262cf6dc9b7933bb9ccf0d4f0)
	{
		if (c6e4150c82a8b60982e648f0411087959 == null)
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
						__ldtoken(c33408081634d45382cc69064025f3c7f.c6b01c99c514aea8ee7f1a805664d5e98(CollisionObject, Pose, CollisionObject, Pose, float, CollisionDetection));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1796));
				}
			}
		}
		if (cb9065353c960dfabb2c6812b8400ab32 == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1811));
		}
		if (c201f75b262cf6dc9b7933bb9ccf0d4f0 == null)
		{
			while (true)
			{
				switch (1)
				{
				case 0:
					break;
				default:
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2553));
				}
			}
		}
		IGeometricObject geometricObject = c6e4150c82a8b60982e648f0411087959.GeometricObject;
		IGeometricObject geometricObject2 = cb9065353c960dfabb2c6812b8400ab32.GeometricObject;
		Pose pose = geometricObject.Pose;
		Pose pose2 = geometricObject2.Pose;
		Vector3F vector3F = ce62c464030ecc0a6d92c1401aa8953ba.Position - pose.Position;
		Vector3F vector3F2 = c01837a72cf7ae926712c07d07b9ad285.Position - pose2.Position;
		Vector3F vector = vector3F - vector3F2;
		if (Numeric.IsZero(vector.Length))
		{
			return 1f;
		}
		CollisionAlgorithm collisionAlgorithm = c201f75b262cf6dc9b7933bb9ccf0d4f0.AlgorithmMatrix[c6e4150c82a8b60982e648f0411087959, cb9065353c960dfabb2c6812b8400ab32];
		c107cf2db36674a478eaf84e50d31337e c107cf2db36674a478eaf84e50d31337e2 = c107cf2db36674a478eaf84e50d31337e.c2d8ce8b1ccf2b905e3b18f8a5145fe8a();
		c107cf2db36674a478eaf84e50d31337e2.Shape = geometricObject.Shape;
		c107cf2db36674a478eaf84e50d31337e2.Scale = geometricObject.Scale;
		c107cf2db36674a478eaf84e50d31337e2.Pose = pose;
		c107cf2db36674a478eaf84e50d31337e c107cf2db36674a478eaf84e50d31337e3 = c107cf2db36674a478eaf84e50d31337e.c2d8ce8b1ccf2b905e3b18f8a5145fe8a();
		c107cf2db36674a478eaf84e50d31337e3.Shape = geometricObject2.Shape;
		c107cf2db36674a478eaf84e50d31337e3.Scale = geometricObject2.Scale;
		c107cf2db36674a478eaf84e50d31337e3.Pose = pose2;
		CollisionObject collisionObject = cdb80ee76c96fc0751dea8d15ee69fa47.c29a5464f174eae366728cbb2edc64776.Obtain();
		collisionObject.ceabf44cab475c8ec76f26d6553011a78(c6e4150c82a8b60982e648f0411087959, c107cf2db36674a478eaf84e50d31337e2);
		CollisionObject collisionObject2 = cdb80ee76c96fc0751dea8d15ee69fa47.c29a5464f174eae366728cbb2edc64776.Obtain();
		collisionObject2.ceabf44cab475c8ec76f26d6553011a78(cb9065353c960dfabb2c6812b8400ab32, c107cf2db36674a478eaf84e50d31337e3);
		ContactSet contactSet = ContactSet.Create(collisionObject, collisionObject2);
		try
		{
			collisionAlgorithm.UpdateClosestPoints(contactSet, 0f);
			if (contactSet.Count < 0)
			{
				while (true)
				{
					switch (5)
					{
					case 0:
						continue;
					}
					return 1f;
				}
			}
			Vector3F normal = contactSet[0].Normal;
			float num = 0f - contactSet[0].PenetrationDepth;
			float num2 = 0f;
			float num3 = 0f;
			for (int i = 0; i < 64; i++)
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
				if (num > 0f)
				{
					float num4 = Vector3F.Dot(vector, normal);
					if (Numeric.IsLess(num4, 0f))
					{
						break;
					}
					float num5 = (num + cf7e17649ec54ab6d9098dda36235a236) / num4;
					num2 += num5;
					if (num2 < 0f || num2 > 1f || num2 <= num3)
					{
						break;
					}
					Vector3F position = pose.Position + num2 * (ce62c464030ecc0a6d92c1401aa8953ba.Position - pose.Position);
					c107cf2db36674a478eaf84e50d31337e2.Pose = new Pose(position, pose.Orientation);
					Vector3F position2 = pose2.Position + num2 * (c01837a72cf7ae926712c07d07b9ad285.Position - pose2.Position);
					c107cf2db36674a478eaf84e50d31337e3.Pose = new Pose(position2, pose2.Orientation);
					collisionAlgorithm.UpdateClosestPoints(contactSet, 0f);
					if (contactSet.Count == 0)
					{
						break;
					}
					normal = contactSet[0].Normal;
					num = 0f - contactSet[0].PenetrationDepth;
					num3 = num2;
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
				break;
			}
			if (contactSet.HaveContact)
			{
				while (true)
				{
					switch (4)
					{
					case 0:
						continue;
					}
					if (!(num2 > 0f))
					{
						break;
					}
					while (true)
					{
						switch (5)
						{
						case 0:
							continue;
						}
						if (!(num2 < 1f) || contactSet.Count <= 0)
						{
							break;
						}
						while (true)
						{
							switch (2)
							{
							case 0:
								continue;
							}
							return num2;
						}
					}
					break;
				}
			}
		}
		finally
		{
			using (List<Contact>.Enumerator enumerator = contactSet.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Contact current = enumerator.Current;
					current.Recycle();
				}
				while (true)
				{
					switch (2)
					{
					case 0:
						break;
					default:
						goto end_IL_0386;
					}
					continue;
					end_IL_0386:
					break;
				}
			}
			contactSet.Recycle();
			cdb80ee76c96fc0751dea8d15ee69fa47.c29a5464f174eae366728cbb2edc64776.Recycle(collisionObject);
			cdb80ee76c96fc0751dea8d15ee69fa47.c29a5464f174eae366728cbb2edc64776.Recycle(collisionObject2);
			c107cf2db36674a478eaf84e50d31337e2.ca18b9b5faa9851d6355f617787bb5859();
			c107cf2db36674a478eaf84e50d31337e3.ca18b9b5faa9851d6355f617787bb5859();
		}
		return 1f;
	}

	internal static void ca3ab0237d3e1ea1bc3c635f35068a66a(IGeometricObject cb68f9e7d713a301d7f5d4ed772a8666f, out float c62a8ac8a1ea6107858bdc026334c8108, out Vector3F cf94628f848fdb1b294ef4b3736d0f3bc)
	{
		Aabb aabb = cb68f9e7d713a301d7f5d4ed772a8666f.Aabb;
		cf94628f848fdb1b294ef4b3736d0f3bc = aabb.Center;
		c62a8ac8a1ea6107858bdc026334c8108 = aabb.Extent.Length * 0.5f;
	}

	internal static float c24bac5b4e8c5b2971d30aa656e0fd884(IGeometricObject cb68f9e7d713a301d7f5d4ed772a8666f)
	{
		ca3ab0237d3e1ea1bc3c635f35068a66a(cb68f9e7d713a301d7f5d4ed772a8666f, out var c62a8ac8a1ea6107858bdc026334c, out var cf94628f848fdb1b294ef4b3736d0f3bc);
		return (cf94628f848fdb1b294ef4b3736d0f3bc - cb68f9e7d713a301d7f5d4ed772a8666f.Pose.Position).Length + c62a8ac8a1ea6107858bdc026334c;
	}

	internal static float c90c3411c13dad6b165001e39c0f00faf(CollisionObject c6e4150c82a8b60982e648f0411087959, Pose ce62c464030ecc0a6d92c1401aa8953ba, CollisionObject cb9065353c960dfabb2c6812b8400ab32, Pose c01837a72cf7ae926712c07d07b9ad285, float cc932046c8062860a7ef859243edc98b5, CollisionDetection c201f75b262cf6dc9b7933bb9ccf0d4f0)
	{
		if (c6e4150c82a8b60982e648f0411087959 == null)
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
						__ldtoken(c33408081634d45382cc69064025f3c7f.c90c3411c13dad6b165001e39c0f00faf(CollisionObject, Pose, CollisionObject, Pose, float, CollisionDetection));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1796));
				}
			}
		}
		if (cb9065353c960dfabb2c6812b8400ab32 == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1811));
		}
		if (c201f75b262cf6dc9b7933bb9ccf0d4f0 == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2553));
		}
		IGeometricObject geometricObject = c6e4150c82a8b60982e648f0411087959.GeometricObject;
		IGeometricObject geometricObject2 = cb9065353c960dfabb2c6812b8400ab32.GeometricObject;
		Pose pose = geometricObject.Pose;
		Pose pose2 = geometricObject2.Pose;
		QuaternionF quaternionF = QuaternionF.CreateRotation(ce62c464030ecc0a6d92c1401aa8953ba.Orientation * pose.Orientation.Transposed);
		float angle = quaternionF.Angle;
		Vector3F vector3F;
		if (Numeric.AreEqual(quaternionF.W, 1f))
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
			vector3F = Vector3F.UnitX;
		}
		else
		{
			vector3F = quaternionF.Axis;
		}
		Vector3F vector3F2 = vector3F;
		QuaternionF quaternionF2 = QuaternionF.CreateRotation(c01837a72cf7ae926712c07d07b9ad285.Orientation * pose2.Orientation.Transposed);
		float angle2 = quaternionF2.Angle;
		Vector3F vector3F3 = ((!Numeric.AreEqual(quaternionF2.W, 1f)) ? quaternionF2.Axis : Vector3F.UnitX);
		float num = c24bac5b4e8c5b2971d30aa656e0fd884(geometricObject);
		float num2 = c24bac5b4e8c5b2971d30aa656e0fd884(geometricObject2);
		float num3 = angle * num + angle2 * num2;
		Vector3F vector3F4 = ce62c464030ecc0a6d92c1401aa8953ba.Position - pose.Position;
		Vector3F vector3F5 = c01837a72cf7ae926712c07d07b9ad285.Position - pose2.Position;
		Vector3F vector = vector3F4 - vector3F5;
		if (Numeric.IsZero(vector.Length + num3))
		{
			return 1f;
		}
		CollisionAlgorithm collisionAlgorithm = c201f75b262cf6dc9b7933bb9ccf0d4f0.AlgorithmMatrix[c6e4150c82a8b60982e648f0411087959, cb9065353c960dfabb2c6812b8400ab32];
		c107cf2db36674a478eaf84e50d31337e c107cf2db36674a478eaf84e50d31337e2 = c107cf2db36674a478eaf84e50d31337e.c2d8ce8b1ccf2b905e3b18f8a5145fe8a();
		c107cf2db36674a478eaf84e50d31337e2.Shape = geometricObject.Shape;
		c107cf2db36674a478eaf84e50d31337e2.Scale = geometricObject.Scale;
		c107cf2db36674a478eaf84e50d31337e2.Pose = pose;
		c107cf2db36674a478eaf84e50d31337e c107cf2db36674a478eaf84e50d31337e3 = c107cf2db36674a478eaf84e50d31337e.c2d8ce8b1ccf2b905e3b18f8a5145fe8a();
		c107cf2db36674a478eaf84e50d31337e3.Shape = geometricObject2.Shape;
		c107cf2db36674a478eaf84e50d31337e3.Scale = geometricObject2.Scale;
		c107cf2db36674a478eaf84e50d31337e3.Pose = pose2;
		CollisionObject collisionObject = cdb80ee76c96fc0751dea8d15ee69fa47.c29a5464f174eae366728cbb2edc64776.Obtain();
		collisionObject.ceabf44cab475c8ec76f26d6553011a78(c6e4150c82a8b60982e648f0411087959, c107cf2db36674a478eaf84e50d31337e2);
		CollisionObject collisionObject2 = cdb80ee76c96fc0751dea8d15ee69fa47.c29a5464f174eae366728cbb2edc64776.Obtain();
		collisionObject2.ceabf44cab475c8ec76f26d6553011a78(cb9065353c960dfabb2c6812b8400ab32, c107cf2db36674a478eaf84e50d31337e3);
		ContactSet contactSet = ContactSet.Create(collisionObject, collisionObject2);
		try
		{
			collisionAlgorithm.UpdateClosestPoints(contactSet, 0f);
			if (contactSet.Count < 0)
			{
				return 1f;
			}
			Vector3F normal = contactSet[0].Normal;
			float num4 = 0f - contactSet[0].PenetrationDepth;
			float num5 = 0f;
			float num6 = 0f;
			for (int i = 0; i < 64; i++)
			{
				if (num4 > 0f)
				{
					float num7 = Vector3F.Dot(vector, normal);
					num3 = Vector3F.Cross(normal, vector3F2).Length * angle * num + Vector3F.Cross(normal, vector3F3).Length * angle2 * num2;
					float num8 = num7 + num3;
					if (Numeric.IsLess(num8, 0f))
					{
						break;
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
					float num9 = (num4 + cc932046c8062860a7ef859243edc98b5) / num8;
					num5 += num9;
					if (num5 < 0f || num5 > 1f || num5 <= num6)
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
					Vector3F position = pose.Position + num5 * (ce62c464030ecc0a6d92c1401aa8953ba.Position - pose.Position);
					Matrix33F matrix33F = Matrix33F.CreateRotation(vector3F2, num5 * angle);
					c107cf2db36674a478eaf84e50d31337e2.Pose = new Pose(position, matrix33F * pose.Orientation);
					Vector3F position2 = pose2.Position + num5 * (c01837a72cf7ae926712c07d07b9ad285.Position - pose2.Position);
					Matrix33F matrix33F2 = Matrix33F.CreateRotation(vector3F3, num5 * angle2);
					c107cf2db36674a478eaf84e50d31337e3.Pose = new Pose(position2, matrix33F2 * pose2.Orientation);
					collisionAlgorithm.UpdateClosestPoints(contactSet, 0f);
					if (contactSet.Count == 0)
					{
						break;
					}
					normal = contactSet[0].Normal;
					num4 = 0f - contactSet[0].PenetrationDepth;
					num6 = num5;
					continue;
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
				break;
			}
			if (contactSet.HaveContact && num5 > 0f)
			{
				while (true)
				{
					switch (4)
					{
					case 0:
						continue;
					}
					if (num5 < 1f && contactSet.Count > 0)
					{
						return num5;
					}
					break;
				}
			}
		}
		finally
		{
			using (List<Contact>.Enumerator enumerator = contactSet.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Contact current = enumerator.Current;
					current.Recycle();
				}
				while (true)
				{
					switch (1)
					{
					case 0:
						break;
					default:
						goto end_IL_04a8;
					}
					continue;
					end_IL_04a8:
					break;
				}
			}
			contactSet.Recycle();
			cdb80ee76c96fc0751dea8d15ee69fa47.c29a5464f174eae366728cbb2edc64776.Recycle(collisionObject);
			cdb80ee76c96fc0751dea8d15ee69fa47.c29a5464f174eae366728cbb2edc64776.Recycle(collisionObject2);
			c107cf2db36674a478eaf84e50d31337e2.ca18b9b5faa9851d6355f617787bb5859();
			c107cf2db36674a478eaf84e50d31337e3.ca18b9b5faa9851d6355f617787bb5859();
		}
		return 1f;
	}
}
