using System;
using A;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Collisions.Algorithms;

/// <summary>
///       Computes contact or closest-point information for <see cref="T:DigitalRune.Geometry.Shapes.RayShape" /> vs. 
///       <see cref="T:DigitalRune.Geometry.Shapes.ConvexShape" />.
///       </summary>
public class RayConvexAlgorithm : CollisionAlgorithm
{
	private const int ccc81a89e5837c5d9e080ee22b5a22784 = 32;

	private readonly Gjk c27f40a10674f00f388cbda4adc56f518;

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Collisions.Algorithms.RayConvexAlgorithm" /> class.
	///       </summary>
	/// <param name="collisionDetection">The collision detection service.</param>
	public RayConvexAlgorithm(CollisionDetection collisionDetection)
		: base(collisionDetection)
	{
		c27f40a10674f00f388cbda4adc56f518 = new Gjk(CollisionDetection);
	}

	/// <summary>
	///       Computes the collision. - This method should only be used by 
	///       <see cref="T:DigitalRune.Geometry.Collisions.Algorithms.CollisionAlgorithm" /> instances!
	///       </summary>
	/// <param name="contactSet">The contact set.</param>
	/// <param name="type">The type of collision query.</param>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="contactSet" /> does not contain a <see cref="T:DigitalRune.Geometry.Shapes.RayShape" /> and a 
	///       <see cref="T:DigitalRune.Geometry.Shapes.ConvexShape" />.
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
		ConvexShape convexShape = obj2.Shape as ConvexShape;
		if (rayShape != null)
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
				__ldtoken(RayConvexAlgorithm.ComputeCollision(ContactSet, CollisionQueryType));
			}
			if (convexShape != null)
			{
				if (type == CollisionQueryType.ClosestPoints)
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
					c27f40a10674f00f388cbda4adc56f518.ComputeCollision(contactSet, type);
					if (!contactSet.HaveContact)
					{
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
					foreach (Contact item in contactSet)
					{
						item.Recycle();
					}
					contactSet.Clear();
				}
				contactSet.HaveContact = false;
				Vector3F ceabb47d7d959545d89c4300eabbdd7c = obj.Scale;
				Vector3F scale = obj2.Scale;
				Pose cbc1b7e0b011ad6dcdb8beb392ec7a = obj2.Pose;
				Pose cbc1b7e0b011ad6dcdb8beb392ec7a2 = obj.Pose;
				Ray ray = new Ray(rayShape);
				ray.c22463d4a85fcdaa7d4ffbd8669084d72(ref ceabb47d7d959545d89c4300eabbdd7c);
				ray.c77d310c5ae14ed7e7ed360995b1867bb(ref cbc1b7e0b011ad6dcdb8beb392ec7a2);
				Ray ray2 = ray;
				ray2.c0d5e70cac6e675cb3df8faaefbfd7052(ref cbc1b7e0b011ad6dcdb8beb392ec7a);
				cc49dcd41ee82253a805139c508e0926f cc49dcd41ee82253a805139c508e0926f2 = cc49dcd41ee82253a805139c508e0926f.c2d8ce8b1ccf2b905e3b18f8a5145fe8a();
				try
				{
					Vector3F origin = ray2.Origin;
					Vector3F vector3F = ray2.Direction * ray2.Length;
					float num = 0f;
					Vector3F vector3F2 = origin;
					Vector3F localDirection = default;
					Vector3F vector3F3 = vector3F2 - convexShape.GetSupportPoint(ray2.Direction, scale);
					float num2 = vector3F3.LengthSquared;
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
						if (num3 < 32)
						{
							num3++;
							Vector3F supportPoint = convexShape.GetSupportPoint(vector3F3, scale);
							Vector3F vector3F4 = vector3F2 - supportPoint;
							float num4 = Vector3F.Dot(vector3F3, vector3F4);
							if (num4 > 0f)
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
								float num5 = Vector3F.Dot(vector3F3, vector3F);
								if (num5 >= 0f)
								{
									return;
								}
								num -= num4 / num5;
								vector3F2 = origin + num * vector3F;
								cc49dcd41ee82253a805139c508e0926f2.c0a4aead06b14dcb81618057a59d4a433();
								vector3F4 = vector3F2 - supportPoint;
								localDirection = vector3F3;
							}
							cc49dcd41ee82253a805139c508e0926f2.c03d72a32723924cb5b81c585d5df7d9d(vector3F4, vector3F2, supportPoint);
							cc49dcd41ee82253a805139c508e0926f2.c322ef1ce205c6dbcf3a93ad899943435();
							vector3F3 = cc49dcd41ee82253a805139c508e0926f2.c596154f5b0c243d05e4fa792fd81b4bb;
							if (cc49dcd41ee82253a805139c508e0926f2.IsValid)
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
								if (!cc49dcd41ee82253a805139c508e0926f2.c8cfa49e753c046fa77edb85a6fcf2975)
								{
									num6 = vector3F3.LengthSquared;
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
							}
							num6 = 0f;
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
						break;
					}
					int haveContact;
					if (0f <= num)
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
						haveContact = ((num <= 1f) ? 1 : 0);
					}
					else
					{
						haveContact = 0;
					}
					contactSet.HaveContact = (byte)haveContact != 0;
					if (type == CollisionQueryType.Boolean)
					{
						return;
					}
					if (type == CollisionQueryType.Contacts)
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
						if (!contactSet.HaveContact)
						{
							return;
						}
					}
					float num7 = num * ray2.Length;
					Vector3F c2fed316f5bcf4dd4ea7883930f3822f = ray.Origin + ray.Direction * num7;
					localDirection = cbc1b7e0b011ad6dcdb8beb392ec7a.ToWorldDirection(localDirection);
					if (!localDirection.TryNormalize())
					{
						localDirection = Vector3F.UnitY;
					}
					if (flag)
					{
						localDirection = -localDirection;
					}
					Contact cf1b03e8c15642a547bc4024e27883cdf = c9ae4fc785e8cd5184185f6ee6d5a3853.c8918c90b638b2cb75bae14c2683b291d(contactSet, c2fed316f5bcf4dd4ea7883930f3822f, -localDirection, num7, c72057f882b90cac0b4126f84e6c9aa28: true);
					c9ae4fc785e8cd5184185f6ee6d5a3853.c6aedb1aa24c5098d4ce5c849eb999287(contactSet, cf1b03e8c15642a547bc4024e27883cdf, type, CollisionDetection.ContactPositionTolerance);
					return;
				}
				finally
				{
					cc49dcd41ee82253a805139c508e0926f2.ca18b9b5faa9851d6355f617787bb5859();
				}
			}
		}
		throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8904), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1826));
	}
}
