using System;
using System.Collections.Generic;
using A;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Collisions.Algorithms;

/// <summary>
///       Computes contact or closest-point information for <see cref="T:DigitalRune.Geometry.Shapes.BoxShape" /> vs. <see cref="T:DigitalRune.Geometry.Shapes.BoxShape" />.
///       </summary>
public class BoxBoxAlgorithm : CollisionAlgorithm
{
	private const int cab924df9c3b683576918872903234210 = 4;

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Collisions.Algorithms.BoxBoxAlgorithm" /> class.
	///       </summary>
	/// <param name="collisionDetection">The collision detection service.</param>
	public BoxBoxAlgorithm(CollisionDetection collisionDetection)
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
	///   <paramref name="contactSet" /> does not contain two <see cref="T:DigitalRune.Geometry.Shapes.BoxShape" />es.
	///       </exception>
	/// <exception cref="T:DigitalRune.Geometry.GeometryException">
	///   <paramref name="type" /> is set to <see cref="F:DigitalRune.Geometry.Collisions.CollisionQueryType.ClosestPoints" />. This 
	///       collision algorithm cannot handle closest-point queries. Use <see cref="T:DigitalRune.Geometry.Collisions.Algorithms.Gjk" /> instead.
	///       </exception>
	public override void ComputeCollision(ContactSet contactSet, CollisionQueryType type)
	{
		if (type == CollisionQueryType.ClosestPoints)
		{
			throw new GeometryException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(18030));
		}
		CollisionObject objectA = contactSet.ObjectA;
		CollisionObject objectB = contactSet.ObjectB;
		IGeometricObject geometricObject = objectA.GeometricObject;
		IGeometricObject geometricObject2 = objectB.GeometricObject;
		BoxShape boxShape = geometricObject.Shape as BoxShape;
		BoxShape boxShape2 = geometricObject2.Shape as BoxShape;
		if (boxShape == null || boxShape2 == null)
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(18188), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1826));
		}
		Vector3F vector3F = Vector3F.Absolute(geometricObject.Scale);
		Vector3F vector3F2 = Vector3F.Absolute(geometricObject2.Scale);
		Pose pose = geometricObject.Pose;
		Pose pose2 = geometricObject2.Pose;
		Vector3F vector3F3 = pose.ToLocalPosition(pose2.Position);
		Matrix33F matrix33F = pose.Orientation.Transposed * pose2.Orientation;
		Matrix33F matrix33F2 = Matrix33F.Absolute(matrix33F);
		Vector3F vector3F4 = 0.5f * boxShape.Extent * vector3F;
		Vector3F vector3F5 = 0.5f * boxShape2.Extent * vector3F2;
		float num = float.PositiveInfinity;
		int num2 = 0;
		Vector3F vector3F6 = Vector3F.UnitX;
		bool flag = false;
		contactSet.HaveContact = false;
		float num3 = Math.Abs(vector3F3.X) - (vector3F4.X + vector3F5.X * matrix33F2.M00 + vector3F5.Y * matrix33F2.M01 + vector3F5.Z * matrix33F2.M02);
		if (num3 > 0f)
		{
			return;
		}
		if (type != CollisionQueryType.Boolean && 0f - num3 < num)
		{
			vector3F6 = Vector3F.UnitX;
			num = 0f - num3;
			flag = vector3F3.X < 0f;
			num2 = 1;
		}
		num3 = Math.Abs(vector3F3.Y) - (vector3F4.Y + vector3F5.X * matrix33F2.M10 + vector3F5.Y * matrix33F2.M11 + vector3F5.Z * matrix33F2.M12);
		if (num3 > 0f)
		{
			return;
		}
		if (type != CollisionQueryType.Boolean && 0f - num3 < num)
		{
			vector3F6 = Vector3F.UnitY;
			num = 0f - num3;
			flag = vector3F3.Y < 0f;
			num2 = 2;
		}
		num3 = Math.Abs(vector3F3.Z) - (vector3F4.Z + vector3F5.X * matrix33F2.M20 + vector3F5.Y * matrix33F2.M21 + vector3F5.Z * matrix33F2.M22);
		if (num3 > 0f)
		{
			return;
		}
		if (type != CollisionQueryType.Boolean && 0f - num3 < num)
		{
			vector3F6 = Vector3F.UnitZ;
			num = 0f - num3;
			flag = vector3F3.Z < 0f;
			num2 = 3;
		}
		float num4 = vector3F3.X * matrix33F.M00 + vector3F3.Y * matrix33F.M10 + vector3F3.Z * matrix33F.M20;
		num3 = Math.Abs(num4) - (vector3F5.X + vector3F4.X * matrix33F2.M00 + vector3F4.Y * matrix33F2.M10 + vector3F4.Z * matrix33F2.M20);
		if (num3 > 0f)
		{
			return;
		}
		if (type != CollisionQueryType.Boolean && 0f - num3 < num)
		{
			vector3F6 = matrix33F.GetColumn(0);
			num = 0f - num3;
			flag = num4 < 0f;
			num2 = 4;
		}
		num4 = vector3F3.X * matrix33F.M01 + vector3F3.Y * matrix33F.M11 + vector3F3.Z * matrix33F.M21;
		num3 = Math.Abs(num4) - (vector3F5.Y + vector3F4.X * matrix33F2.M01 + vector3F4.Y * matrix33F2.M11 + vector3F4.Z * matrix33F2.M21);
		if (num3 > 0f)
		{
			return;
		}
		if (type != CollisionQueryType.Boolean && 0f - num3 < num)
		{
			vector3F6 = matrix33F.GetColumn(1);
			num = 0f - num3;
			flag = num4 < 0f;
			num2 = 5;
		}
		num4 = vector3F3.X * matrix33F.M02 + vector3F3.Y * matrix33F.M12 + vector3F3.Z * matrix33F.M22;
		num3 = Math.Abs(num4) - (vector3F5.Z + vector3F4.X * matrix33F2.M02 + vector3F4.Y * matrix33F2.M12 + vector3F4.Z * matrix33F2.M22);
		if (num3 > 0f)
		{
			return;
		}
		if (type != CollisionQueryType.Boolean && 0f - num3 < num)
		{
			vector3F6 = matrix33F.GetColumn(2);
			num = 0f - num3;
			flag = num4 < 0f;
			num2 = 6;
		}
		num4 = vector3F3.Z * matrix33F.M10 - vector3F3.Y * matrix33F.M20;
		num3 = Math.Abs(num4) - (vector3F4.Y * matrix33F2.M20 + vector3F4.Z * matrix33F2.M10 + vector3F5.Y * matrix33F2.M02 + vector3F5.Z * matrix33F2.M01);
		if (num3 > 0f)
		{
			return;
		}
		if (type != CollisionQueryType.Boolean)
		{
			Vector3F vector3F7 = new Vector3F(0f, 0f - matrix33F.M20, matrix33F.M10);
			float length = vector3F7.Length;
			num3 /= length;
			if (0f - num3 < num)
			{
				vector3F6 = vector3F7 / length;
				num = 0f - num3;
				flag = num4 < 0f;
				num2 = 7;
			}
		}
		num4 = vector3F3.Z * matrix33F.M11 - vector3F3.Y * matrix33F.M21;
		num3 = Math.Abs(num4) - (vector3F4.Y * matrix33F2.M21 + vector3F4.Z * matrix33F2.M11 + vector3F5.X * matrix33F2.M02 + vector3F5.Z * matrix33F2.M00);
		if (num3 > 0f)
		{
			return;
		}
		if (type != CollisionQueryType.Boolean)
		{
			Vector3F vector3F7 = new Vector3F(0f, 0f - matrix33F.M21, matrix33F.M11);
			float length = vector3F7.Length;
			num3 /= length;
			if (0f - num3 < num)
			{
				vector3F6 = vector3F7 / length;
				num = 0f - num3;
				flag = num4 < 0f;
				num2 = 8;
			}
		}
		num4 = vector3F3.Z * matrix33F.M12 - vector3F3.Y * matrix33F.M22;
		num3 = Math.Abs(num4) - (vector3F4.Y * matrix33F2.M22 + vector3F4.Z * matrix33F2.M12 + vector3F5.X * matrix33F2.M01 + vector3F5.Y * matrix33F2.M00);
		if (num3 > 0f)
		{
			return;
		}
		if (type != CollisionQueryType.Boolean)
		{
			Vector3F vector3F7 = new Vector3F(0f, 0f - matrix33F.M22, matrix33F.M12);
			float length = vector3F7.Length;
			num3 /= length;
			if (0f - num3 < num)
			{
				vector3F6 = vector3F7 / length;
				num = 0f - num3;
				flag = num4 < 0f;
				num2 = 9;
			}
		}
		num4 = vector3F3.X * matrix33F.M20 - vector3F3.Z * matrix33F.M00;
		num3 = Math.Abs(num4) - (vector3F4.X * matrix33F2.M20 + vector3F4.Z * matrix33F2.M00 + vector3F5.Y * matrix33F2.M12 + vector3F5.Z * matrix33F2.M11);
		if (num3 > 0f)
		{
			return;
		}
		if (type != CollisionQueryType.Boolean)
		{
			Vector3F vector3F7 = new Vector3F(matrix33F.M20, 0f, 0f - matrix33F.M00);
			float length = vector3F7.Length;
			num3 /= length;
			if (0f - num3 < num)
			{
				vector3F6 = vector3F7 / length;
				num = 0f - num3;
				flag = num4 < 0f;
				num2 = 10;
			}
		}
		num4 = vector3F3.X * matrix33F.M21 - vector3F3.Z * matrix33F.M01;
		num3 = Math.Abs(num4) - (vector3F4.X * matrix33F2.M21 + vector3F4.Z * matrix33F2.M01 + vector3F5.X * matrix33F2.M12 + vector3F5.Z * matrix33F2.M10);
		if (num3 > 0f)
		{
			return;
		}
		if (type != CollisionQueryType.Boolean)
		{
			Vector3F vector3F7 = new Vector3F(matrix33F.M21, 0f, 0f - matrix33F.M01);
			float length = vector3F7.Length;
			num3 /= length;
			if (0f - num3 < num)
			{
				vector3F6 = vector3F7 / length;
				num = 0f - num3;
				flag = num4 < 0f;
				num2 = 11;
			}
		}
		num4 = vector3F3.X * matrix33F.M22 - vector3F3.Z * matrix33F.M02;
		num3 = Math.Abs(num4) - (vector3F4.X * matrix33F2.M22 + vector3F4.Z * matrix33F2.M02 + vector3F5.X * matrix33F2.M11 + vector3F5.Y * matrix33F2.M10);
		if (num3 > 0f)
		{
			return;
		}
		if (type != CollisionQueryType.Boolean)
		{
			Vector3F vector3F7 = new Vector3F(matrix33F.M22, 0f, 0f - matrix33F.M02);
			float length = vector3F7.Length;
			num3 /= length;
			if (0f - num3 < num)
			{
				vector3F6 = vector3F7 / length;
				num = 0f - num3;
				flag = num4 < 0f;
				num2 = 12;
			}
		}
		num4 = vector3F3.Y * matrix33F.M00 - vector3F3.X * matrix33F.M10;
		num3 = Math.Abs(num4) - (vector3F4.X * matrix33F2.M10 + vector3F4.Y * matrix33F2.M00 + vector3F5.Y * matrix33F2.M22 + vector3F5.Z * matrix33F2.M21);
		if (num3 > 0f)
		{
			return;
		}
		if (type != CollisionQueryType.Boolean)
		{
			Vector3F vector3F7 = new Vector3F(0f - matrix33F.M10, matrix33F.M00, 0f);
			float length = vector3F7.Length;
			num3 /= length;
			if (0f - num3 < num)
			{
				vector3F6 = vector3F7 / length;
				num = 0f - num3;
				flag = num4 < 0f;
				num2 = 13;
			}
		}
		num4 = vector3F3.Y * matrix33F.M01 - vector3F3.X * matrix33F.M11;
		num3 = Math.Abs(num4) - (vector3F4.X * matrix33F2.M11 + vector3F4.Y * matrix33F2.M01 + vector3F5.X * matrix33F2.M22 + vector3F5.Z * matrix33F2.M20);
		if (num3 > 0f)
		{
			return;
		}
		if (type != CollisionQueryType.Boolean)
		{
			Vector3F vector3F7 = new Vector3F(0f - matrix33F.M11, matrix33F.M01, 0f);
			float length = vector3F7.Length;
			num3 /= length;
			if (0f - num3 < num)
			{
				vector3F6 = vector3F7 / length;
				num = 0f - num3;
				flag = num4 < 0f;
				num2 = 14;
			}
		}
		num4 = vector3F3.Y * matrix33F.M02 - vector3F3.X * matrix33F.M12;
		num3 = Math.Abs(num4) - (vector3F4.X * matrix33F2.M12 + vector3F4.Y * matrix33F2.M02 + vector3F5.X * matrix33F2.M21 + vector3F5.Y * matrix33F2.M20);
		if (num3 > 0f)
		{
			return;
		}
		if (type != CollisionQueryType.Boolean)
		{
			Vector3F vector3F7 = new Vector3F(0f - matrix33F.M12, matrix33F.M02, 0f);
			float length = vector3F7.Length;
			num3 /= length;
			if (0f - num3 < num)
			{
				vector3F6 = vector3F7 / length;
				num = 0f - num3;
				flag = num4 < 0f;
				num2 = 15;
			}
		}
		contactSet.HaveContact = true;
		if (type == CollisionQueryType.Boolean)
		{
			return;
		}
		if (flag)
		{
			vector3F6 = -vector3F6;
		}
		Vector3F vector3F8 = pose.ToWorldDirection(vector3F6);
		if (num2 > 6)
		{
			LineSegment segment = boxShape.c9fec5a4b39e0cab537d06f7eb7d3cb90((num2 - 7) / 3, vector3F6, vector3F);
			LineSegment segment2 = boxShape2.c9fec5a4b39e0cab537d06f7eb7d3cb90((num2 - 7) % 3, Matrix33F.MultiplyTransposed(matrix33F, -vector3F6), vector3F2);
			segment2.Start = matrix33F * segment2.Start + vector3F3;
			segment2.End = matrix33F * segment2.End + vector3F3;
			GeometryHelper.GetClosestPoints(segment, segment2, out var point, out var _);
			point -= vector3F6 * (num / 2f);
			point = pose.ToWorldPosition(point);
			Contact cf1b03e8c15642a547bc4024e27883cdf = c9ae4fc785e8cd5184185f6ee6d5a3853.c8918c90b638b2cb75bae14c2683b291d(contactSet, point, vector3F8, num, c72057f882b90cac0b4126f84e6c9aa28: false);
			c9ae4fc785e8cd5184185f6ee6d5a3853.c6aedb1aa24c5098d4ce5c849eb999287(contactSet, cf1b03e8c15642a547bc4024e27883cdf, type, CollisionDetection.ContactPositionTolerance);
		}
		else
		{
			if (1 > num2 || num2 > 6)
			{
				return;
			}
			Pose pose3;
			Pose pose4;
			Vector3F vector3F9;
			Vector3F vector3F10;
			if (num2 <= 3)
			{
				pose3 = pose;
				pose4 = pose2;
				vector3F9 = vector3F4;
				vector3F10 = vector3F5;
				flag = false;
			}
			else
			{
				pose3 = pose2;
				pose4 = pose;
				vector3F9 = vector3F5;
				vector3F10 = vector3F4;
				vector3F8 = -vector3F8;
				flag = true;
			}
			Vector3F vector3F11 = pose4.ToLocalDirection(vector3F8);
			Vector3F vector3F12 = vector3F11;
			vector3F12.Absolute();
			Vector2F vector2F = default;
			float num5;
			Vector3F column;
			Vector3F column2;
			Vector3F column3;
			float num6;
			switch (Vector3F.Absolute(vector3F11).IndexOfLargestComponent)
			{
			case 0:
				vector2F.X = vector3F10.Y;
				vector2F.Y = vector3F10.Z;
				num5 = vector3F10.X;
				column = pose4.Orientation.GetColumn(0);
				column2 = pose4.Orientation.GetColumn(1);
				column3 = pose4.Orientation.GetColumn(2);
				num6 = vector3F11.X;
				break;
			case 1:
				vector2F.X = vector3F10.X;
				vector2F.Y = vector3F10.Z;
				num5 = vector3F10.Y;
				column = pose4.Orientation.GetColumn(1);
				column2 = pose4.Orientation.GetColumn(0);
				column3 = pose4.Orientation.GetColumn(2);
				num6 = vector3F11.Y;
				break;
			default:
				vector2F.X = vector3F10.X;
				vector2F.Y = vector3F10.Y;
				num5 = vector3F10.Z;
				column = pose4.Orientation.GetColumn(2);
				column2 = pose4.Orientation.GetColumn(0);
				column3 = pose4.Orientation.GetColumn(1);
				num6 = vector3F11.Z;
				break;
			}
			float num7 = ((num6 < 0f) ? num5 : (0f - num5));
			Vector3F vector3F13 = column * num7 + pose4.Position - pose3.Position;
			Vector2F c72a1596dd0ddcc910cb662ae6c2352b = default;
			float num8;
			Vector3F column4;
			Vector3F column5;
			switch (num2)
			{
			case 1:
			case 4:
				c72a1596dd0ddcc910cb662ae6c2352b.X = vector3F9.Y;
				c72a1596dd0ddcc910cb662ae6c2352b.Y = vector3F9.Z;
				num8 = vector3F9.X;
				column4 = pose3.Orientation.GetColumn(1);
				column5 = pose3.Orientation.GetColumn(2);
				break;
			case 2:
			case 5:
				c72a1596dd0ddcc910cb662ae6c2352b.X = vector3F9.X;
				c72a1596dd0ddcc910cb662ae6c2352b.Y = vector3F9.Z;
				num8 = vector3F9.Y;
				column4 = pose3.Orientation.GetColumn(0);
				column5 = pose3.Orientation.GetColumn(2);
				break;
			default:
				c72a1596dd0ddcc910cb662ae6c2352b.X = vector3F9.X;
				c72a1596dd0ddcc910cb662ae6c2352b.Y = vector3F9.Y;
				num8 = vector3F9.Z;
				column4 = pose3.Orientation.GetColumn(0);
				column5 = pose3.Orientation.GetColumn(1);
				break;
			}
			Vector2F vector2F2 = default;
			vector2F2.X = vector3F13.X * column4.X + vector3F13.Y * column4.Y + vector3F13.Z * column4.Z;
			vector2F2.Y = vector3F13.X * column5.X + vector3F13.Y * column5.Y + vector3F13.Z * column5.Z;
			Matrix22F matrix22F = default;
			matrix22F.M00 = column4.X * column2.X + column4.Y * column2.Y + column4.Z * column2.Z;
			matrix22F.M01 = column4.X * column3.X + column4.Y * column3.Y + column4.Z * column3.Z;
			matrix22F.M10 = column5.X * column2.X + column5.Y * column2.Y + column5.Z * column2.Z;
			matrix22F.M11 = column5.X * column3.X + column5.Y * column3.Y + column5.Z * column3.Z;
			float num9 = matrix22F.M00 * vector2F.X;
			float num10 = matrix22F.M01 * vector2F.Y;
			float num11 = matrix22F.M10 * vector2F.X;
			float num12 = matrix22F.M11 * vector2F.Y;
			List<Vector2F> list = ResourcePools<Vector2F>.Lists.Obtain();
			list.Add(new Vector2F(vector2F2.X - num9 - num10, vector2F2.Y - num11 - num12));
			list.Add(new Vector2F(vector2F2.X + num9 - num10, vector2F2.Y + num11 - num12));
			list.Add(new Vector2F(vector2F2.X + num9 + num10, vector2F2.Y + num11 + num12));
			list.Add(new Vector2F(vector2F2.X - num9 + num10, vector2F2.Y - num11 + num12));
			List<Vector2F> list2 = c924a28462d7cebf2fa40e9daf5302c85(c72a1596dd0ddcc910cb662ae6c2352b, list);
			int count = list2.Count;
			List<Vector3F> list3 = ResourcePools<Vector3F>.Lists.Obtain();
			List<float> list4 = ResourcePools<float>.Lists.Obtain();
			Matrix22F inverse = matrix22F.Inverse;
			Vector3F item = default;
			for (int num13 = count - 1; num13 >= 0; num13--)
			{
				Vector2F vector2F3 = list2[num13];
				Vector2F vector2F4 = inverse * (vector2F3 - vector2F2);
				item.X = column2.X * vector2F4.X + column3.X * vector2F4.Y + vector3F13.X;
				item.Y = column2.Y * vector2F4.X + column3.Y * vector2F4.Y + vector3F13.Y;
				item.Z = column2.Z * vector2F4.X + column3.Z * vector2F4.Y + vector3F13.Z;
				float num14 = num8 - (vector3F8.X * item.X + vector3F8.Y * item.Y + vector3F8.Z * item.Z);
				if (num14 >= 0f)
				{
					list3.Add(item);
					list4.Add(num14);
				}
				else
				{
					list2.RemoveAt(num13);
				}
			}
			count = list3.Count;
			if (count == 0)
			{
				return;
			}
			vector3F6 = (flag ? (-vector3F8) : vector3F8);
			if (count <= 4)
			{
				for (int i = 0; i < count; i++)
				{
					float num15 = list4[i];
					Vector3F c2fed316f5bcf4dd4ea7883930f3822f = list3[i] + pose3.Position + vector3F8 * (num15 / 2f);
					Contact cf1b03e8c15642a547bc4024e27883cdf2 = c9ae4fc785e8cd5184185f6ee6d5a3853.c8918c90b638b2cb75bae14c2683b291d(contactSet, c2fed316f5bcf4dd4ea7883930f3822f, vector3F6, num15, c72057f882b90cac0b4126f84e6c9aa28: false);
					c9ae4fc785e8cd5184185f6ee6d5a3853.c6aedb1aa24c5098d4ce5c849eb999287(contactSet, cf1b03e8c15642a547bc4024e27883cdf2, type, CollisionDetection.ContactPositionTolerance);
				}
			}
			else
			{
				int cf0de4337e134e1d42a86ae78e02eea = 0;
				float num16 = list4[0];
				for (int j = 1; j < count; j++)
				{
					float num17 = list4[j];
					if (num17 > num16)
					{
						num16 = num17;
						cf0de4337e134e1d42a86ae78e02eea = j;
					}
				}
				List<int> list5 = cc4a650c1b510f5c746afed11e5444a57(list2, cf0de4337e134e1d42a86ae78e02eea, 4);
				count = list5.Count;
				for (int k = 0; k < count; k++)
				{
					int index = list5[k];
					float num18 = list4[index];
					Vector3F c2fed316f5bcf4dd4ea7883930f3822f2 = list3[index] + pose3.Position + vector3F8 * (num18 / 2f);
					Contact cf1b03e8c15642a547bc4024e27883cdf3 = c9ae4fc785e8cd5184185f6ee6d5a3853.c8918c90b638b2cb75bae14c2683b291d(contactSet, c2fed316f5bcf4dd4ea7883930f3822f2, vector3F6, list4[index], c72057f882b90cac0b4126f84e6c9aa28: false);
					c9ae4fc785e8cd5184185f6ee6d5a3853.c6aedb1aa24c5098d4ce5c849eb999287(contactSet, cf1b03e8c15642a547bc4024e27883cdf3, type, CollisionDetection.ContactPositionTolerance);
				}
				ResourcePools<int>.Lists.Recycle(list5);
			}
			ResourcePools<Vector2F>.Lists.Recycle(list2);
			ResourcePools<Vector3F>.Lists.Recycle(list3);
			ResourcePools<float>.Lists.Recycle(list4);
		}
	}

	private static List<Vector2F> c924a28462d7cebf2fa40e9daf5302c85(Vector2F c72a1596dd0ddcc910cb662ae6c2352b0, List<Vector2F> c65e58a5c74957dbc4f0392180d6998f4)
	{
		List<Vector2F> obj = c65e58a5c74957dbc4f0392180d6998f4;
		List<Vector2F> obj2 = ResourcePools<Vector2F>.Lists.Obtain();
		int count = obj.Count;
		for (int i = 0; i < count; i++)
		{
			float x = c72a1596dd0ddcc910cb662ae6c2352b0.X;
			Vector2F item = obj[i];
			if (item.X <= x)
			{
				obj2.Add(item);
			}
			Vector2F vector2F = ((i < count - 1) ? obj[i + 1] : obj[0]);
			if ((item.X < x && vector2F.X > x) || (item.X > x && vector2F.X < x))
			{
				obj2.Add(new Vector2F(x, item.Y + (x - item.X) * (vector2F.Y - item.Y) / (vector2F.X - item.X)));
			}
		}
		MathHelper.Swap(ref obj, ref obj2);
		count = obj.Count;
		obj2.Clear();
		for (int j = 0; j < count; j++)
		{
			float num = 0f - c72a1596dd0ddcc910cb662ae6c2352b0.X;
			Vector2F item2 = obj[j];
			if (num <= item2.X)
			{
				obj2.Add(item2);
			}
			Vector2F vector2F2 = ((j < count - 1) ? obj[j + 1] : obj[0]);
			if ((item2.X < num && vector2F2.X > num) || (item2.X > num && vector2F2.X < num))
			{
				obj2.Add(new Vector2F(num, item2.Y + (num - item2.X) * (vector2F2.Y - item2.Y) / (vector2F2.X - item2.X)));
			}
		}
		MathHelper.Swap(ref obj, ref obj2);
		count = obj.Count;
		obj2.Clear();
		for (int k = 0; k < count; k++)
		{
			float y = c72a1596dd0ddcc910cb662ae6c2352b0.Y;
			Vector2F item3 = obj[k];
			if (item3.Y <= y)
			{
				obj2.Add(item3);
			}
			Vector2F vector2F3 = ((k < count - 1) ? obj[k + 1] : obj[0]);
			if ((item3.Y < y && vector2F3.Y > y) || (item3.Y > y && vector2F3.Y < y))
			{
				obj2.Add(new Vector2F(item3.X + (y - item3.Y) * (vector2F3.X - item3.X) / (vector2F3.Y - item3.Y), y));
			}
		}
		MathHelper.Swap(ref obj, ref obj2);
		count = obj.Count;
		obj2.Clear();
		for (int l = 0; l < count; l++)
		{
			float num2 = 0f - c72a1596dd0ddcc910cb662ae6c2352b0.Y;
			Vector2F item4 = obj[l];
			if (num2 <= item4.Y)
			{
				obj2.Add(item4);
			}
			Vector2F vector2F4 = ((l < count - 1) ? obj[l + 1] : obj[0]);
			if ((item4.Y < num2 && vector2F4.Y > num2) || (item4.Y > num2 && vector2F4.Y < num2))
			{
				obj2.Add(new Vector2F(item4.X + (num2 - item4.Y) * (vector2F4.X - item4.X) / (vector2F4.Y - item4.Y), num2));
			}
		}
		ResourcePools<Vector2F>.Lists.Recycle(obj);
		return obj2;
	}

	private static Vector2F c71d31484263ce41f6c34f6ba299fc651(Vector2F c00d0ff00811e265cb49e4fb94c2935c8, Vector2F cdea40b3fcaaecfce91785c0d7700b038, float ced3a2c195df015dba1080d5376df75ba)
	{
		return new Vector2F(ced3a2c195df015dba1080d5376df75ba, c00d0ff00811e265cb49e4fb94c2935c8.Y + (ced3a2c195df015dba1080d5376df75ba - c00d0ff00811e265cb49e4fb94c2935c8.X) * (cdea40b3fcaaecfce91785c0d7700b038.Y - c00d0ff00811e265cb49e4fb94c2935c8.Y) / (cdea40b3fcaaecfce91785c0d7700b038.X - c00d0ff00811e265cb49e4fb94c2935c8.X));
	}

	private static Vector2F c3d189fe8ae157ea74e0ba1790c71487f(Vector2F c00d0ff00811e265cb49e4fb94c2935c8, Vector2F cdea40b3fcaaecfce91785c0d7700b038, float c2d827ceb89b50db33cdf25887a12ad0f)
	{
		return new Vector2F(c00d0ff00811e265cb49e4fb94c2935c8.X + (c2d827ceb89b50db33cdf25887a12ad0f - c00d0ff00811e265cb49e4fb94c2935c8.Y) * (cdea40b3fcaaecfce91785c0d7700b038.X - c00d0ff00811e265cb49e4fb94c2935c8.X) / (cdea40b3fcaaecfce91785c0d7700b038.Y - c00d0ff00811e265cb49e4fb94c2935c8.Y), c2d827ceb89b50db33cdf25887a12ad0f);
	}

	private static List<int> cc4a650c1b510f5c746afed11e5444a57(List<Vector2F> c2ebf3a3e77e8b4507a45357157f26fae, int cf0de4337e134e1d42a86ae78e02eea10, int cea41f78e112f48ba366a3dc0addcf141)
	{
		int count = c2ebf3a3e77e8b4507a45357157f26fae.Count;
		Vector2F vector2F;
		switch (count)
		{
		case 1:
			vector2F = c2ebf3a3e77e8b4507a45357157f26fae[0];
			break;
		case 2:
			vector2F = 0.5f * (c2ebf3a3e77e8b4507a45357157f26fae[0] + c2ebf3a3e77e8b4507a45357157f26fae[1]);
			break;
		default:
		{
			vector2F = default;
			float num = 0f;
			Vector2F vector2F2;
			Vector2F vector2F3;
			float num2;
			for (int i = 0; i < count - 1; i++)
			{
				vector2F2 = c2ebf3a3e77e8b4507a45357157f26fae[i];
				vector2F3 = c2ebf3a3e77e8b4507a45357157f26fae[i + 1];
				num2 = vector2F2.X * vector2F3.Y - vector2F3.X * vector2F2.Y;
				num += num2;
				vector2F += (vector2F2 + vector2F3) * num2;
			}
			vector2F2 = c2ebf3a3e77e8b4507a45357157f26fae[count - 1];
			vector2F3 = c2ebf3a3e77e8b4507a45357157f26fae[0];
			num2 = vector2F2.X * vector2F3.Y - vector2F3.X * vector2F2.Y;
			num += num2;
			vector2F += (vector2F2 + vector2F3) * num2;
			if (!Numeric.IsZero(num))
			{
				vector2F *= 1f / (3f * num);
				break;
			}
			vector2F = Vector2F.Zero;
			for (int j = 0; j < count; j++)
			{
				vector2F += c2ebf3a3e77e8b4507a45357157f26fae[j];
			}
			vector2F /= (float)count;
			break;
		}
		}
		float[] array = cb06b21e3dce409f51454b820e86a8560<float>.c73844b59cabb234138404883e6fe5975.Obtain();
		for (int k = 0; k < count; k++)
		{
			Vector2F vector2F4 = c2ebf3a3e77e8b4507a45357157f26fae[k];
			array[k] = (float)Math.Atan2(vector2F4.Y - vector2F.Y, vector2F4.X - vector2F.X);
		}
		float num3 = array[cf0de4337e134e1d42a86ae78e02eea10];
		List<int> list = ResourcePools<int>.Lists.Obtain();
		bool[] array2 = cb06b21e3dce409f51454b820e86a8560<bool>.c73844b59cabb234138404883e6fe5975.Obtain();
		for (int l = 0; l < count; l++)
		{
			array2[l] = true;
		}
		array2[cf0de4337e134e1d42a86ae78e02eea10] = false;
		list.Add(cf0de4337e134e1d42a86ae78e02eea10);
		for (int m = 1; m < cea41f78e112f48ba366a3dc0addcf141; m++)
		{
			float num4 = num3 + (float)m * ((float)Math.PI * 2f) / (float)cea41f78e112f48ba366a3dc0addcf141;
			if (num4 > (float)Math.PI)
			{
				num4 -= (float)Math.PI * 2f;
			}
			float num5 = float.MaxValue;
			int num6 = -1;
			for (int n = 0; n < count; n++)
			{
				if (array2[n])
				{
					float num7 = Math.Abs(array[n] - num4);
					if (num7 > (float)Math.PI)
					{
						num7 = (float)Math.PI * 2f - num7;
					}
					if (num7 < num5)
					{
						num5 = num7;
						num6 = n;
					}
				}
			}
			if (num6 >= 0)
			{
				array2[num6] = false;
				list.Add(num6);
			}
		}
		cb06b21e3dce409f51454b820e86a8560<bool>.c73844b59cabb234138404883e6fe5975.Recycle(array2);
		cb06b21e3dce409f51454b820e86a8560<float>.c73844b59cabb234138404883e6fe5975.Recycle(array);
		return list;
	}
}
