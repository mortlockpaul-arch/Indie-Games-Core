using System;
using A;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Collisions.Algorithms;

/// <summary>
///       Computes contact or closest-point information for <see cref="T:DigitalRune.Geometry.Shapes.RayShape" /> vs. 
///       <see cref="T:DigitalRune.Geometry.Shapes.TriangleShape" />.
///       </summary>
public class RayTriangleAlgorithm : CollisionAlgorithm
{
	private readonly Gjk c27f40a10674f00f388cbda4adc56f518;

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Collisions.Algorithms.RayTriangleAlgorithm" /> class.
	///       </summary>
	/// <param name="collisionDetection">The collision detection service.</param>
	public RayTriangleAlgorithm(CollisionDetection collisionDetection)
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
	///       <see cref="T:DigitalRune.Geometry.Shapes.Triangle" />.
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
		TriangleShape triangleShape = obj2.Shape as TriangleShape;
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
				__ldtoken(RayTriangleAlgorithm.ComputeCollision(ContactSet, CollisionQueryType));
			}
			if (triangleShape != null)
			{
				Vector3F ceabb47d7d959545d89c4300eabbdd7c = obj.Scale;
				Vector3F scale = obj2.Scale;
				Pose cbc1b7e0b011ad6dcdb8beb392ec7a = obj.Pose;
				Pose cbc1b7e0b011ad6dcdb8beb392ec7a2 = obj2.Pose;
				Vector3F vector3F = triangleShape.Vertex0 * scale;
				Vector3F vector3F2 = triangleShape.Vertex1 * scale;
				Vector3F vector3F3 = triangleShape.Vertex2 * scale;
				Ray ray = new Ray(rayShape);
				ray.c22463d4a85fcdaa7d4ffbd8669084d72(ref ceabb47d7d959545d89c4300eabbdd7c);
				ray.c77d310c5ae14ed7e7ed360995b1867bb(ref cbc1b7e0b011ad6dcdb8beb392ec7a);
				Ray ray2 = ray;
				ray2.c0d5e70cac6e675cb3df8faaefbfd7052(ref cbc1b7e0b011ad6dcdb8beb392ec7a2);
				Vector3F vector3F4 = vector3F2 - vector3F;
				Vector3F vector3F5 = vector3F3 - vector3F;
				Vector3F vector3F6 = Vector3F.Cross(vector3F4, vector3F5);
				float num = vector3F6.Length * Numeric.EpsilonFSquared;
				Vector3F vector3F7 = ray2.Direction * ray2.Length;
				float num2 = 0f - Vector3F.Dot(vector3F7, vector3F6);
				if (num == 0f || Numeric.IsZero(num2, num))
				{
					if (type != CollisionQueryType.Contacts)
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
						switch (type)
						{
						case CollisionQueryType.Boolean:
							break;
						case CollisionQueryType.ClosestPoints:
							ce8940e67bedd22b9570162e0f28c0a3c(contactSet, ray.Origin);
							return;
						default:
							return;
						}
					}
					contactSet.HaveContact = false;
					return;
				}
				Vector3F vector = ray2.Origin - vector3F;
				float num3 = Vector3F.Dot(vector, vector3F6) / num2;
				contactSet.HaveContact = false;
				if (!(num3 < 0f))
				{
					if (!(num3 > 1f))
					{
						Vector3F vector2 = Vector3F.Cross(vector, vector3F7);
						float num4 = Vector3F.Dot(vector3F5, vector2) / num2;
						float num5 = Vector3F.Dot(-vector3F4, vector2) / num2;
						if (num4 + num5 <= 1f + num)
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
							if (num4 >= 0f - num)
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
								if (num5 >= 0f - num)
								{
									while (true)
									{
										switch (2)
										{
										case 0:
											break;
										default:
											contactSet.HaveContact = true;
											if (type != CollisionQueryType.Boolean)
											{
												float num6 = num3 * ray2.Length;
												Vector3F c2fed316f5bcf4dd4ea7883930f3822f = ray.Origin + ray.Direction * num6;
												vector3F6 = cbc1b7e0b011ad6dcdb8beb392ec7a2.ToWorldDirection(vector3F6);
												vector3F6.Normalize();
												if (num2 > 0f)
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
													vector3F6 = -vector3F6;
												}
												if (flag)
												{
													vector3F6 = -vector3F6;
												}
												Contact cf1b03e8c15642a547bc4024e27883cdf = c9ae4fc785e8cd5184185f6ee6d5a3853.c8918c90b638b2cb75bae14c2683b291d(contactSet, c2fed316f5bcf4dd4ea7883930f3822f, vector3F6, num6, c72057f882b90cac0b4126f84e6c9aa28: true);
												c9ae4fc785e8cd5184185f6ee6d5a3853.c6aedb1aa24c5098d4ce5c849eb999287(contactSet, cf1b03e8c15642a547bc4024e27883cdf, type, CollisionDetection.ContactPositionTolerance);
											}
											return;
										}
									}
								}
							}
						}
						if (type == CollisionQueryType.ClosestPoints)
						{
							ce8940e67bedd22b9570162e0f28c0a3c(contactSet, ray.Origin);
						}
						return;
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
				}
				if (type == CollisionQueryType.ClosestPoints)
				{
					ce8940e67bedd22b9570162e0f28c0a3c(contactSet, ray.Origin);
				}
				return;
			}
		}
		throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6973), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1826));
	}

	private void ce8940e67bedd22b9570162e0f28c0a3c(ContactSet cfc73391dcf53c37de15d54e2434d0199, Vector3F c8c96fa1a638120437ed15707b0fadc79)
	{
		c27f40a10674f00f388cbda4adc56f518.ComputeCollision(cfc73391dcf53c37de15d54e2434d0199, CollisionQueryType.ClosestPoints);
		if (!cfc73391dcf53c37de15d54e2434d0199.HaveContact)
		{
			return;
		}
		int count = cfc73391dcf53c37de15d54e2434d0199.Count;
		for (int i = 0; i < count; i++)
		{
			Contact contact = cfc73391dcf53c37de15d54e2434d0199[i];
			if (contact.IsRayHit)
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
			if (1 == 0)
			{
				__ldtoken(RayTriangleAlgorithm.ce8940e67bedd22b9570162e0f28c0a3c(ContactSet, Vector3F));
			}
			if (contact.PenetrationDepth >= 0f)
			{
				contact.PenetrationDepth = (contact.Position - c8c96fa1a638120437ed15707b0fadc79).Length;
				contact.IsRayHit = true;
			}
		}
	}
}
