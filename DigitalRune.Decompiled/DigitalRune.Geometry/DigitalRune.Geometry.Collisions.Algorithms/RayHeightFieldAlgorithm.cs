using System;
using A;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Collisions.Algorithms;

/// <summary>
///       Computes contact or closest-point information for <see cref="T:DigitalRune.Geometry.Shapes.RayShape" /> vs. 
///       <see cref="T:DigitalRune.Geometry.Shapes.HeightField" />.
///       </summary>
public class RayHeightFieldAlgorithm : CollisionAlgorithm
{
	private readonly HeightFieldAlgorithm cd1019c805fe67590ca5b59b1ab476e6b;

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Collisions.Algorithms.RayHeightFieldAlgorithm" /> class.
	///       </summary>
	/// <param name="collisionDetection">The collision detection service.</param>
	public RayHeightFieldAlgorithm(CollisionDetection collisionDetection)
		: base(collisionDetection)
	{
		cd1019c805fe67590ca5b59b1ab476e6b = new HeightFieldAlgorithm(collisionDetection);
	}

	/// <summary>
	///       Computes the collision. - This method should only be used by 
	///       <see cref="T:DigitalRune.Geometry.Collisions.Algorithms.CollisionAlgorithm" /> instances!
	///       </summary>
	/// <param name="contactSet">The contact set.</param>
	/// <param name="type">The type of collision query.</param>
	public override void ComputeCollision(ContactSet contactSet, CollisionQueryType type)
	{
		if (type == CollisionQueryType.ClosestPoints)
		{
			cd1019c805fe67590ca5b59b1ab476e6b.ComputeCollision(contactSet, type);
			return;
		}
		IGeometricObject obj = contactSet.ObjectA.GeometricObject;
		IGeometricObject obj2 = contactSet.ObjectB.GeometricObject;
		bool flag = obj.Shape is RayShape;
		if (flag)
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
			if (1 == 0)
			{
				__ldtoken(RayHeightFieldAlgorithm.ComputeCollision(ContactSet, CollisionQueryType));
			}
			MathHelper.Swap(ref obj2, ref obj);
		}
		RayShape rayShape = obj2.Shape as RayShape;
		HeightField heightField = obj.Shape as HeightField;
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
			if (heightField != null)
			{
				contactSet.HaveContact = false;
				Vector3F ceabb47d7d959545d89c4300eabbdd7c = obj2.Scale;
				Pose cbc1b7e0b011ad6dcdb8beb392ec7a = obj2.Pose;
				Vector3F cd43bd3081ba2d9ab6e7120b46c165a = obj.Scale;
				Pose cbc1b7e0b011ad6dcdb8beb392ec7a2 = obj.Pose;
				if (!(cd43bd3081ba2d9ab6e7120b46c165a.X < 0f) && !(cd43bd3081ba2d9ab6e7120b46c165a.Y < 0f))
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
					if (!(cd43bd3081ba2d9ab6e7120b46c165a.Z < 0f))
					{
						Ray cde1533d396e1ad78240641e2f4146f5d = new Ray(rayShape);
						cde1533d396e1ad78240641e2f4146f5d.c22463d4a85fcdaa7d4ffbd8669084d72(ref ceabb47d7d959545d89c4300eabbdd7c);
						cde1533d396e1ad78240641e2f4146f5d.c77d310c5ae14ed7e7ed360995b1867bb(ref cbc1b7e0b011ad6dcdb8beb392ec7a);
						Ray c086c1f2c84aea1e399ca58e186e3a = cde1533d396e1ad78240641e2f4146f5d;
						c086c1f2c84aea1e399ca58e186e3a.c0d5e70cac6e675cb3df8faaefbfd7052(ref cbc1b7e0b011ad6dcdb8beb392ec7a2);
						Ray ray = c086c1f2c84aea1e399ca58e186e3a;
						Vector3F ceabb47d7d959545d89c4300eabbdd7c2 = Vector3F.One / cd43bd3081ba2d9ab6e7120b46c165a;
						ray.c22463d4a85fcdaa7d4ffbd8669084d72(ref ceabb47d7d959545d89c4300eabbdd7c2);
						float[,] array = heightField.Array;
						int length = array.GetLength(0);
						int length2 = array.GetLength(1);
						int num = length - 1;
						int num2 = length2 - 1;
						float num3 = heightField.WidthX / (float)num;
						float num4 = heightField.WidthZ / (float)num2;
						Vector3F vector3F = new Vector3F(1f / ray.Direction.X, 1f / ray.Direction.Y, 1f / ray.Direction.Z);
						Vector3F vector3F2 = ray.Origin;
						int num5;
						if (!(vector3F2.X >= 0f))
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
							num5 = -1;
						}
						else
						{
							num5 = (int)(vector3F2.X / num3);
						}
						int num6 = num5;
						if (num6 < 0)
						{
							if (ray.Direction.X <= 0f)
							{
								while (true)
								{
									switch (7)
									{
									case 0:
										break;
									default:
										return;
									}
								}
							}
							float num7 = (0f - ray.Origin.X) * vector3F.X;
							if (num7 > ray.Length)
							{
								return;
							}
							vector3F2 = ray.Origin + num7 * ray.Direction;
							num6 = 0;
						}
						else if (num6 >= num)
						{
							if (ray.Direction.X >= 0f)
							{
								return;
							}
							float num8 = (heightField.WidthX - ray.Origin.X) * vector3F.X;
							if (num8 > ray.Length)
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
							vector3F2 = ray.Origin + num8 * ray.Direction;
							num6 = num - 1;
						}
						int num9 = ((vector3F2.Z >= 0f) ? ((int)(vector3F2.Z / num4)) : (-1));
						if (num9 < 0)
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
							if (ray.Direction.Z <= 0f)
							{
								return;
							}
							float num10 = (0f - ray.Origin.Z) * vector3F.Z;
							if (num10 > ray.Length)
							{
								return;
							}
							vector3F2 = ray.Origin + num10 * ray.Direction;
							int num11;
							if (!(vector3F2.X >= 0f))
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
								num11 = -1;
							}
							else
							{
								num11 = (int)(vector3F2.X / num3);
							}
							num6 = num11;
							num9 = 0;
						}
						else if (num9 >= num2)
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
							if (ray.Direction.Z >= 0f)
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
							float num12 = (heightField.WidthZ - ray.Origin.Z) * vector3F.Z;
							if (num12 > ray.Length)
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
							vector3F2 = ray.Origin + num12 * ray.Direction;
							num6 = ((vector3F2.X >= 0f) ? ((int)(vector3F2.X / num3)) : (-1));
							num9 = num2 - 1;
						}
						if (num6 < 0 || num6 >= num || num9 < 0)
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
							if (num9 >= num2)
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
							while (true)
							{
								Triangle c082c516895a1e859067cd7bbdbe8b9e = heightField.GetTriangle(num6, num9, second: false);
								Triangle c082c516895a1e859067cd7bbdbe8b9e2 = heightField.GetTriangle(num6, num9, second: true);
								int num13 = (num9 * num + num6) * 2;
								float num14 = vector3F2.X / num3 - (float)num6;
								float num15 = vector3F2.Z / num4 - (float)num9;
								bool flag2 = num14 + num15 > 1f;
								float num16 = float.PositiveInfinity;
								if (ray.Direction.X > 0f)
								{
									num16 = ((float)(num6 + 1) * num3 - ray.Origin.X) * vector3F.X;
								}
								else if (ray.Direction.X < 0f)
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
									num16 = ((float)num6 * num3 - ray.Origin.X) * vector3F.X;
								}
								float num17 = float.PositiveInfinity;
								if (ray.Direction.Z > 0f)
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
									num17 = ((float)(num9 + 1) * num4 - ray.Origin.Z) * vector3F.Z;
								}
								else if (ray.Direction.Z < 0f)
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
									num17 = ((float)num9 * num4 - ray.Origin.Z) * vector3F.Z;
								}
								bool flag3 = false;
								Vector3F vector3F3;
								if (num16 < num17)
								{
									if (ray.Direction.X > 0f)
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
										num6++;
										if (num6 >= num)
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
											flag3 = true;
										}
									}
									else
									{
										num6--;
										if (num6 < 0)
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
											flag3 = true;
										}
									}
									if (num16 > ray.Length)
									{
										flag3 = true;
										num16 = ray.Length;
									}
									vector3F3 = ray.Origin + num16 * ray.Direction;
								}
								else
								{
									if (ray.Direction.Z > 0f)
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
										num9++;
										if (num9 >= num2)
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
											flag3 = true;
										}
									}
									else
									{
										num9--;
										if (num9 < 0)
										{
											flag3 = true;
										}
									}
									if (num17 > ray.Length)
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
										flag3 = true;
										num17 = ray.Length;
									}
									vector3F3 = ray.Origin + num17 * ray.Direction;
								}
								bool flag4 = false;
								float num18 = Math.Min(vector3F2.Y, vector3F3.Y) - CollisionDetection.Epsilon;
								int num19;
								if (c082c516895a1e859067cd7bbdbe8b9e.Vertex0.Y < num18 && c082c516895a1e859067cd7bbdbe8b9e.Vertex1.Y < num18)
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
									if (c082c516895a1e859067cd7bbdbe8b9e.Vertex2.Y < num18)
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
										num19 = ((c082c516895a1e859067cd7bbdbe8b9e2.Vertex1.Y < num18) ? 1 : 0);
										goto IL_076f;
									}
								}
								num19 = 0;
								goto IL_076f;
								IL_0864:
								contactSet.HaveContact = true;
								if (type == CollisionQueryType.Boolean)
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
								Vector3F vector3F4 = cbc1b7e0b011ad6dcdb8beb392ec7a2.ToWorldPosition(vector3F2 * cd43bd3081ba2d9ab6e7120b46c165a);
								Vector3F vector3F5 = cbc1b7e0b011ad6dcdb8beb392ec7a2.ToWorldDirection(Vector3F.UnitY);
								if (flag)
								{
									vector3F5 = -vector3F5;
								}
								float length3 = (vector3F4 - cde1533d396e1ad78240641e2f4146f5d.Origin).Length;
								Contact cf1b03e8c15642a547bc4024e27883cdf = c9ae4fc785e8cd5184185f6ee6d5a3853.c8918c90b638b2cb75bae14c2683b291d(contactSet, vector3F4, vector3F5, length3, c72057f882b90cac0b4126f84e6c9aa28: true);
								c9ae4fc785e8cd5184185f6ee6d5a3853.c6aedb1aa24c5098d4ce5c849eb999287(contactSet, cf1b03e8c15642a547bc4024e27883cdf, type, CollisionDetection.ContactPositionTolerance);
								break;
								IL_076f:
								flag4 = (byte)num19 != 0;
								if (!flag4)
								{
									bool flag5;
									if (flag2)
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
										flag5 = c7f470d0616e3ac2bc5ac9dc43e0dbb50(contactSet, flag, type, ref cde1533d396e1ad78240641e2f4146f5d, ref c086c1f2c84aea1e399ca58e186e3a, ref c082c516895a1e859067cd7bbdbe8b9e2, num13 + 1, ref cbc1b7e0b011ad6dcdb8beb392ec7a2, ref cd43bd3081ba2d9ab6e7120b46c165a);
										if (!flag5)
										{
											flag5 = c7f470d0616e3ac2bc5ac9dc43e0dbb50(contactSet, flag, type, ref cde1533d396e1ad78240641e2f4146f5d, ref c086c1f2c84aea1e399ca58e186e3a, ref c082c516895a1e859067cd7bbdbe8b9e, num13, ref cbc1b7e0b011ad6dcdb8beb392ec7a2, ref cd43bd3081ba2d9ab6e7120b46c165a);
										}
									}
									else
									{
										flag5 = c7f470d0616e3ac2bc5ac9dc43e0dbb50(contactSet, flag, type, ref cde1533d396e1ad78240641e2f4146f5d, ref c086c1f2c84aea1e399ca58e186e3a, ref c082c516895a1e859067cd7bbdbe8b9e, num13, ref cbc1b7e0b011ad6dcdb8beb392ec7a2, ref cd43bd3081ba2d9ab6e7120b46c165a);
										if (!flag5)
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
											flag5 = c7f470d0616e3ac2bc5ac9dc43e0dbb50(contactSet, flag, type, ref cde1533d396e1ad78240641e2f4146f5d, ref c086c1f2c84aea1e399ca58e186e3a, ref c082c516895a1e859067cd7bbdbe8b9e2, num13 + 1, ref cbc1b7e0b011ad6dcdb8beb392ec7a2, ref cd43bd3081ba2d9ab6e7120b46c165a);
										}
									}
									if (flag5)
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
									if (contactSet.HaveContact && type == CollisionQueryType.Boolean)
									{
										break;
									}
								}
								if (!flag4)
								{
									if (!flag2)
									{
										if (GeometryHelper.IsInFront(c082c516895a1e859067cd7bbdbe8b9e, vector3F2) < 0f)
										{
											goto IL_0864;
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
									}
									if (flag2)
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
										if (GeometryHelper.IsInFront(c082c516895a1e859067cd7bbdbe8b9e2, vector3F2) < 0f)
										{
											goto IL_0864;
										}
									}
								}
								if (flag3)
								{
									break;
								}
								vector3F2 = vector3F3;
							}
							return;
						}
					}
				}
				throw new NotSupportedException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7497));
			}
			while (true)
			{
				switch (3)
				{
				case 0:
					continue;
				}
				break;
			}
		}
		throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(10565), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1826));
	}

	private bool c7f470d0616e3ac2bc5ac9dc43e0dbb50(ContactSet cfc73391dcf53c37de15d54e2434d0199, bool cbd5f4ffb7f6caa84b20b23ef3ac88b62, CollisionQueryType cb76a1558f3778afde9d9fa562d4ee745, ref Ray cde1533d396e1ad78240641e2f4146f5d, ref Ray c086c1f2c84aea1e399ca58e186e3a920, ref Triangle c082c516895a1e859067cd7bbdbe8b9e5, int c770ce7a50191007b3e32d6b7a6e43051, ref Pose c36819baa9f54177ab4805a1ed537916a, ref Vector3F cd43bd3081ba2d9ab6e7120b46c165a99)
	{
		Vector3F vector3F = c082c516895a1e859067cd7bbdbe8b9e5.Vertex0 * cd43bd3081ba2d9ab6e7120b46c165a99;
		Vector3F vector3F2 = c082c516895a1e859067cd7bbdbe8b9e5.Vertex1 * cd43bd3081ba2d9ab6e7120b46c165a99;
		Vector3F vector3F3 = c082c516895a1e859067cd7bbdbe8b9e5.Vertex2 * cd43bd3081ba2d9ab6e7120b46c165a99;
		Vector3F vector3F4 = vector3F2 - vector3F;
		Vector3F vector3F5 = vector3F3 - vector3F;
		Vector3F vector3F6 = Vector3F.Cross(vector3F4, vector3F5);
		float num = vector3F6.Length * Numeric.EpsilonFSquared;
		Vector3F vector3F7 = c086c1f2c84aea1e399ca58e186e3a920.Direction * c086c1f2c84aea1e399ca58e186e3a920.Length;
		float num2 = 0f - Vector3F.Dot(vector3F7, vector3F6);
		if (num != 0f)
		{
			if (!Numeric.IsZero(num2, num))
			{
				Vector3F vector = c086c1f2c84aea1e399ca58e186e3a920.Origin - vector3F;
				float num3 = Vector3F.Dot(vector, vector3F6) / num2;
				if (!(num3 < 0f))
				{
					if (!(num3 > 1f))
					{
						Vector3F vector2 = Vector3F.Cross(vector, vector3F7);
						float num4 = Vector3F.Dot(vector3F5, vector2) / num2;
						float num5 = Vector3F.Dot(-vector3F4, vector2) / num2;
						if (num4 + num5 <= 1f + num && num4 >= 0f - num)
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
							if (num5 >= 0f - num)
							{
								while (true)
								{
									switch (6)
									{
									case 0:
										break;
									default:
									{
										cfc73391dcf53c37de15d54e2434d0199.HaveContact = true;
										if (cb76a1558f3778afde9d9fa562d4ee745 == CollisionQueryType.Boolean)
										{
											while (true)
											{
												switch (2)
												{
												case 0:
													break;
												default:
													return false;
												}
											}
										}
										if (num2 < 0f)
										{
											while (true)
											{
												switch (3)
												{
												case 0:
													break;
												default:
													return false;
												}
											}
										}
										float num6 = num3 * c086c1f2c84aea1e399ca58e186e3a920.Length;
										Vector3F c2fed316f5bcf4dd4ea7883930f3822f = cde1533d396e1ad78240641e2f4146f5d.Origin + cde1533d396e1ad78240641e2f4146f5d.Direction * num6;
										vector3F6 = c36819baa9f54177ab4805a1ed537916a.ToWorldDirection(vector3F6);
										vector3F6.Normalize();
										if (num2 < 0f)
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
											vector3F6 = -vector3F6;
										}
										if (cbd5f4ffb7f6caa84b20b23ef3ac88b62)
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
										Contact contact = c9ae4fc785e8cd5184185f6ee6d5a3853.c8918c90b638b2cb75bae14c2683b291d(cfc73391dcf53c37de15d54e2434d0199, c2fed316f5bcf4dd4ea7883930f3822f, vector3F6, num6, c72057f882b90cac0b4126f84e6c9aa28: true);
										if (cbd5f4ffb7f6caa84b20b23ef3ac88b62)
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
											contact.FeatureB = c770ce7a50191007b3e32d6b7a6e43051;
										}
										else
										{
											contact.FeatureA = c770ce7a50191007b3e32d6b7a6e43051;
										}
										c9ae4fc785e8cd5184185f6ee6d5a3853.c6aedb1aa24c5098d4ce5c849eb999287(cfc73391dcf53c37de15d54e2434d0199, contact, cb76a1558f3778afde9d9fa562d4ee745, CollisionDetection.ContactPositionTolerance);
										return true;
									}
									}
								}
							}
						}
						return false;
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
				return false;
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
			if (1 == 0)
			{
				__ldtoken(RayHeightFieldAlgorithm.c7f470d0616e3ac2bc5ac9dc43e0dbb50(ContactSet, bool, CollisionQueryType, ref Ray, ref Ray, ref Triangle, int, ref Pose, ref Vector3F));
			}
		}
		return false;
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
		return cd1019c805fe67590ca5b59b1ab476e6b.GetTimeOfImpact(objectA, targetPoseA, objectB, targetPoseB, allowedPenetration);
	}
}
