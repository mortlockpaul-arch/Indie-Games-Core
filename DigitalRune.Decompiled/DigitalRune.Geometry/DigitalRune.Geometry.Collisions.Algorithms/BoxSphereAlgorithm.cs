using System;
using A;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Collisions.Algorithms;

/// <summary>
///       Computes contact or closest-point information for <see cref="T:DigitalRune.Geometry.Shapes.BoxShape" /> vs. 
///       <see cref="T:DigitalRune.Geometry.Shapes.SphereShape" />.
///       </summary>
public class BoxSphereAlgorithm : CollisionAlgorithm
{
	private CollisionAlgorithm ce4ac7c01f9cf66b62482860397c715f6;

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Collisions.Algorithms.BoxSphereAlgorithm" /> class.
	///       </summary>
	/// <param name="collisionDetection">The collision detection service.</param>
	public BoxSphereAlgorithm(CollisionDetection collisionDetection)
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
	///   <paramref name="contactSet" /> does not contain a <see cref="T:DigitalRune.Geometry.Shapes.BoxShape" /> and a 
	///       <see cref="T:DigitalRune.Geometry.Shapes.SphereShape" />.
	///       </exception>
	public override void ComputeCollision(ContactSet contactSet, CollisionQueryType type)
	{
		IGeometricObject obj = contactSet.ObjectA.GeometricObject;
		IGeometricObject obj2 = contactSet.ObjectB.GeometricObject;
		bool flag = obj2.Shape is BoxShape;
		if (flag)
		{
			MathHelper.Swap(ref obj, ref obj2);
		}
		BoxShape boxShape = obj.Shape as BoxShape;
		SphereShape sphereShape = obj2.Shape as SphereShape;
		float num;
		Pose pose;
		Vector3F position;
		bool flag2;
		Vector3F zero;
		Vector3F vector3F5;
		if (boxShape != null)
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
				__ldtoken(BoxSphereAlgorithm.ComputeCollision(ContactSet, CollisionQueryType));
			}
			if (sphereShape != null)
			{
				Vector3F vector3F = Vector3F.Absolute(obj.Scale);
				Vector3F vector3F2 = Vector3F.Absolute(obj2.Scale);
				if (vector3F2.X != vector3F2.Y || vector3F2.Y != vector3F2.Z)
				{
					if (ce4ac7c01f9cf66b62482860397c715f6 == null)
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
						ce4ac7c01f9cf66b62482860397c715f6 = CollisionDetection.AlgorithmMatrix[typeof(BoxShape), typeof(ConvexShape)];
					}
					ce4ac7c01f9cf66b62482860397c715f6.ComputeCollision(contactSet, type);
					return;
				}
				Vector3F vector3F3 = boxShape.Extent * vector3F;
				num = sphereShape.Radius * vector3F2.X;
				pose = obj.Pose;
				position = obj2.Pose.Position;
				Vector3F vector3F4 = pose.ToLocalPosition(position);
				zero = Vector3F.Zero;
				flag2 = true;
				vector3F5 = Vector3F.Zero;
				Vector3F vector3F6 = 0.5f * vector3F3;
				if (vector3F4.X < 0f - vector3F6.X)
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
					zero.X = 0f - vector3F6.X;
					flag2 = false;
				}
				else if (vector3F4.X > vector3F6.X)
				{
					zero.X = vector3F6.X;
					flag2 = false;
				}
				else
				{
					zero.X = vector3F4.X;
				}
				if (vector3F4.Y < 0f - vector3F6.Y)
				{
					zero.Y = 0f - vector3F6.Y;
					flag2 = false;
				}
				else if (vector3F4.Y > vector3F6.Y)
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
					zero.Y = vector3F6.Y;
					flag2 = false;
				}
				else
				{
					zero.Y = vector3F4.Y;
				}
				if (vector3F4.Z < 0f - vector3F6.Z)
				{
					zero.Z = 0f - vector3F6.Z;
					flag2 = false;
				}
				else if (vector3F4.Z > vector3F6.Z)
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
					zero.Z = vector3F6.Z;
					flag2 = false;
				}
				else
				{
					zero.Z = vector3F4.Z;
				}
				if (!flag2)
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
					if (!(vector3F4 - zero).IsNumericallyZero)
					{
						goto IL_0467;
					}
				}
				Vector3F vector3F7 = vector3F6 - Vector3F.Absolute(vector3F4);
				if (vector3F7.X <= vector3F7.Y)
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
					if (vector3F7.X <= vector3F7.Z)
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
						bool flag3 = vector3F4.X > 0f;
						float x;
						if (!flag3)
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
							x = 0f - vector3F6.X;
						}
						else
						{
							x = vector3F6.X;
						}
						zero.X = x;
						if (Numeric.IsZero(vector3F7.X))
						{
							Vector3F vector3F8;
							if (!flag3)
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
								vector3F8 = -Vector3F.UnitX;
							}
							else
							{
								vector3F8 = Vector3F.UnitX;
							}
							vector3F5 = vector3F8;
						}
						goto IL_0467;
					}
				}
				if (vector3F7.Y <= vector3F7.X && vector3F7.Y <= vector3F7.Z)
				{
					bool flag4 = vector3F4.Y > 0f;
					zero.Y = (flag4 ? vector3F6.Y : (0f - vector3F6.Y));
					if (Numeric.IsZero(vector3F7.Y))
					{
						Vector3F vector3F9;
						if (!flag4)
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
							vector3F9 = -Vector3F.UnitY;
						}
						else
						{
							vector3F9 = Vector3F.UnitY;
						}
						vector3F5 = vector3F9;
					}
				}
				else
				{
					bool flag5 = vector3F4.Z > 0f;
					float z;
					if (!flag5)
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
						z = 0f - vector3F6.Z;
					}
					else
					{
						z = vector3F6.Z;
					}
					zero.Z = z;
					if (Numeric.IsZero(vector3F7.Z))
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
						vector3F5 = (flag5 ? Vector3F.UnitZ : (-Vector3F.UnitZ));
					}
				}
				goto IL_0467;
			}
		}
		throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(13364), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1826));
		IL_0467:
		zero = pose.ToWorldPosition(zero);
		Vector3F vector3F10 = zero - position;
		float num2;
		if (!flag2)
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
			num2 = num - vector3F10.Length;
		}
		else
		{
			num2 = num + vector3F10.Length;
		}
		float num3 = num2;
		contactSet.HaveContact = num3 >= 0f;
		if (type == CollisionQueryType.Boolean)
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
			if (type == CollisionQueryType.Contacts && !contactSet.HaveContact)
			{
				while (true)
				{
					switch (1)
					{
					case 0:
						break;
					default:
						return;
					}
				}
			}
			if (vector3F5 == Vector3F.Zero)
			{
				Vector3F vector3F11;
				if (!flag2)
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
					vector3F11 = -vector3F10;
				}
				else
				{
					vector3F11 = vector3F10;
				}
				vector3F5 = vector3F11;
				vector3F5.Normalize();
			}
			else
			{
				vector3F5 = pose.ToWorldDirection(vector3F5);
			}
			Vector3F c2fed316f5bcf4dd4ea7883930f3822f = zero - vector3F5 * (num3 / 2f);
			if (flag)
			{
				vector3F5 = -vector3F5;
			}
			Contact cf1b03e8c15642a547bc4024e27883cdf = c9ae4fc785e8cd5184185f6ee6d5a3853.c8918c90b638b2cb75bae14c2683b291d(contactSet, c2fed316f5bcf4dd4ea7883930f3822f, vector3F5, num3, c72057f882b90cac0b4126f84e6c9aa28: false);
			c9ae4fc785e8cd5184185f6ee6d5a3853.c6aedb1aa24c5098d4ce5c849eb999287(contactSet, cf1b03e8c15642a547bc4024e27883cdf, type, CollisionDetection.ContactPositionTolerance);
			return;
		}
	}
}
