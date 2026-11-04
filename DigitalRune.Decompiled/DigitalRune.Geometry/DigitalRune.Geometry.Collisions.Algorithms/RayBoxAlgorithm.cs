using System;
using A;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Collisions.Algorithms;

/// <summary>
///       Computes contact or closest-point information for <see cref="T:DigitalRune.Geometry.Shapes.RayShape" /> vs. 
///       <see cref="T:DigitalRune.Geometry.Shapes.BoxShape" />.
///       </summary>
public class RayBoxAlgorithm : CollisionAlgorithm
{
	private readonly Gjk c27f40a10674f00f388cbda4adc56f518;

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Collisions.Algorithms.RayBoxAlgorithm" /> class.
	///       </summary>
	/// <param name="collisionDetection">The collision detection service.</param>
	public RayBoxAlgorithm(CollisionDetection collisionDetection)
		: base(collisionDetection)
	{
		c27f40a10674f00f388cbda4adc56f518 = new Gjk(collisionDetection);
	}

	/// <summary>
	///       Computes the collision. - This method should only be used by 
	///       <see cref="T:DigitalRune.Geometry.Collisions.Algorithms.CollisionAlgorithm" /> instances!
	///       </summary>
	/// <param name="contactSet">The contact set.</param>
	/// <param name="type">The type of collision query.</param>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="contactSet" /> does not contain a <see cref="T:DigitalRune.Geometry.Shapes.RayShape" /> and a 
	///       <see cref="T:DigitalRune.Geometry.Shapes.BoxShape" />.
	///       </exception>
	public override void ComputeCollision(ContactSet contactSet, CollisionQueryType type)
	{
		IGeometricObject obj = contactSet.ObjectA.GeometricObject;
		IGeometricObject obj2 = contactSet.ObjectB.GeometricObject;
		bool flag = obj2.Shape is RayShape;
		if (flag)
		{
			MathHelper.Swap(ref obj, ref obj2);
		}
		RayShape rayShape = obj.Shape as RayShape;
		BoxShape boxShape = obj2.Shape as BoxShape;
		if (rayShape != null)
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
				__ldtoken(RayBoxAlgorithm.ComputeCollision(ContactSet, CollisionQueryType));
			}
			if (boxShape != null)
			{
				if (type == CollisionQueryType.ClosestPoints)
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
					c27f40a10674f00f388cbda4adc56f518.ComputeCollision(contactSet, type);
					if (!contactSet.HaveContact)
					{
						while (true)
						{
							switch (5)
							{
							case 0:
								break;
							default:
								return;
							}
						}
					}
					foreach (Contact item in contactSet)
					{
						item.Recycle();
					}
					contactSet.Clear();
				}
				contactSet.HaveContact = false;
				Vector3F ceabb47d7d959545d89c4300eabbdd7c = obj.Scale;
				Vector3F vector3F = Vector3F.Absolute(obj2.Scale);
				Pose cbc1b7e0b011ad6dcdb8beb392ec7a = obj.Pose;
				Pose cbc1b7e0b011ad6dcdb8beb392ec7a2 = obj2.Pose;
				Vector3F vector3F2 = boxShape.Extent * vector3F;
				Ray ray = new Ray(rayShape);
				ray.c22463d4a85fcdaa7d4ffbd8669084d72(ref ceabb47d7d959545d89c4300eabbdd7c);
				ray.c77d310c5ae14ed7e7ed360995b1867bb(ref cbc1b7e0b011ad6dcdb8beb392ec7a);
				Ray ray2 = ray;
				ray2.c0d5e70cac6e675cb3df8faaefbfd7052(ref cbc1b7e0b011ad6dcdb8beb392ec7a2);
				uint num = GeometryHelper.c028a07785704edad752d31ddd84e5429(vector3F2, ray2.Origin);
				uint num2 = GeometryHelper.c028a07785704edad752d31ddd84e5429(vector3F2, ray2.Origin + ray2.Direction * ray2.Length);
				if ((num & num2) != 0)
				{
					while (true)
					{
						switch (5)
						{
						case 0:
							break;
						default:
							return;
						}
					}
				}
				float num3 = 0f;
				float num4 = 1f;
				uint num5 = 1u;
				Vector3F vector3F3 = ray2.Direction * ray2.Length;
				Vector3F vector3F4 = 0.5f * vector3F2;
				Vector3F vector3F5 = Vector3F.Zero;
				for (int i = 0; i < 3; i++)
				{
					if ((num & num5) != 0)
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
						float num6 = (0f - ray2.Origin[i] - vector3F4[i]) / vector3F3[i];
						if (num3 < num6)
						{
							num3 = num6;
							vector3F5 = new Vector3F { [i] = 1f };
						}
					}
					else if ((num2 & num5) != 0)
					{
						float num7 = (0f - ray2.Origin[i] - vector3F4[i]) / vector3F3[i];
						if (num4 > num7)
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
							num4 = num7;
						}
					}
					num5 <<= 1;
					if ((num & num5) != 0)
					{
						float num8 = (0f - ray2.Origin[i] + vector3F4[i]) / vector3F3[i];
						if (num3 < num8)
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
							num3 = num8;
							vector3F5 = new Vector3F { [i] = -1f };
						}
					}
					else if ((num2 & num5) != 0)
					{
						float num9 = (0f - ray2.Origin[i] + vector3F4[i]) / vector3F3[i];
						if (num4 > num9)
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
							num4 = num9;
						}
					}
					num5 <<= 1;
				}
				if (!(num3 <= num4))
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
					contactSet.HaveContact = true;
					if (type == CollisionQueryType.Boolean)
					{
						return;
					}
					float num10 = num3 * ray2.Length;
					if (vector3F5 == Vector3F.Zero)
					{
						vector3F5 = Vector3F.UnitX;
					}
					Vector3F c2fed316f5bcf4dd4ea7883930f3822f = ray.Origin + ray.Direction * num10;
					vector3F5 = cbc1b7e0b011ad6dcdb8beb392ec7a2.ToWorldDirection(vector3F5);
					if (flag)
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
						vector3F5 = -vector3F5;
					}
					Contact cf1b03e8c15642a547bc4024e27883cdf = c9ae4fc785e8cd5184185f6ee6d5a3853.c8918c90b638b2cb75bae14c2683b291d(contactSet, c2fed316f5bcf4dd4ea7883930f3822f, vector3F5, num10, c72057f882b90cac0b4126f84e6c9aa28: true);
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
		throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(13461), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1826));
	}
}
