using System;
using System.Runtime.CompilerServices;
using A;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Collisions.Algorithms;

/// <summary>
///       Computes contact or closest-point information for <see cref="T:DigitalRune.Geometry.Shapes.HeightField" /> vs. any other 
///       <see cref="T:DigitalRune.Geometry.Shapes.Shape" />.
///       </summary>
public class HeightFieldAlgorithm : CollisionAlgorithm
{
	/// <summary>
	///       This value determines when contact welding should be performed.
	///       </summary>
	public static float WeldingLimit = 0.999f;

	private readonly Action<ContactSet> c9e389e2926d430157c5786217c5365d6;

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Collisions.Algorithms.HeightFieldAlgorithm" /> class.
	///       </summary>
	/// <param name="collisionDetection">The collision detection service.</param>
	public HeightFieldAlgorithm(CollisionDetection collisionDetection)
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
				__ldtoken(HeightFieldAlgorithm._002Ector(CollisionDetection));
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
	///   <paramref name="contactSet" /> does not contain a <see cref="T:DigitalRune.Geometry.Shapes.HeightField" />.
	///       </exception>
	/// <exception cref="T:System.NotSupportedException">
	///   <paramref name="contactSet" /> contains a <see cref="T:DigitalRune.Geometry.Shapes.HeightField" /> with a negative scaling.
	///       Computing collisions for height fields with a negative scaling is not supported.
	///       </exception>
	public override void ComputeCollision(ContactSet contactSet, CollisionQueryType type)
	{
		CollisionObject obj = contactSet.ObjectA;
		CollisionObject obj2 = contactSet.ObjectB;
		bool flag = !(obj.GeometricObject.Shape is HeightField);
		if (flag)
		{
			MathHelper.Swap(ref obj, ref obj2);
		}
		IGeometricObject geometricObject = obj.GeometricObject;
		IGeometricObject geometricObject2 = obj2.GeometricObject;
		HeightField heightField = geometricObject.Shape as HeightField;
		Shape shape = geometricObject2.Shape;
		if (heightField == null)
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7408), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1826));
		}
		if (heightField.UseFastCollisionApproximation)
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
				__ldtoken(HeightFieldAlgorithm.ComputeCollision(ContactSet, CollisionQueryType));
			}
			if (type != CollisionQueryType.ClosestPoints)
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
				if (shape is ConvexShape c6523d38be4d4f5503102fb6ebdd2b1c)
				{
					cdd132c567958b42347a1aa048bf7a53e(contactSet, type, geometricObject, geometricObject2, heightField, c6523d38be4d4f5503102fb6ebdd2b1c, flag);
					return;
				}
			}
		}
		Vector3F scale = geometricObject.Scale;
		Vector3F scale2 = geometricObject2.Scale;
		Pose pose = geometricObject.Pose;
		Vector3F vector3F;
		int length;
		int length2;
		float num;
		float num2;
		float num3;
		Contact contact;
		if (!(scale.X < 0f))
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
			if (!(scale.Y < 0f))
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
				if (!(scale.Z < 0f))
				{
					vector3F = pose.ToWorldDirection(Vector3F.UnitY);
					float[,] array = heightField.Array;
					length = array.GetLength(0);
					length2 = array.GetLength(1);
					num = heightField.WidthX * scale.X / (float)(length - 1);
					num2 = heightField.WidthZ * scale.Z / (float)(length2 - 1);
					num3 = 0f;
					contact = null;
					if (!contactSet.HaveContact)
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
						if (type == CollisionQueryType.ClosestPoints)
						{
							contact = c28775f8cc984bc7de36ff4a5989f3409(contactSet, flag);
							if (contact.PenetrationDepth < 0f)
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
								num3 = 0f - contact.PenetrationDepth;
							}
							else
							{
								contactSet.HaveContact = true;
							}
							goto IL_0201;
						}
					}
					contactSet.HaveContact = false;
					goto IL_0201;
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
		}
		throw new NotSupportedException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7497));
		IL_0201:
		Aabb aabb = shape.GetAabb(scale2, pose.Inverse * geometricObject2.Pose);
		int val = (int)((aabb.Minimum.X - num3) / num);
		int val2 = (int)((aabb.Maximum.X + num3) / num);
		int val3 = (int)((aabb.Minimum.Z - num3) / num2);
		int val4 = (int)((aabb.Maximum.Z + num3) / num2);
		int val5 = length - 2;
		int val6 = length2 - 2;
		int num4 = Math.Max(val, 0);
		int num5 = Math.Min(val2, val5);
		int num6 = Math.Max(val3, 0);
		int num7 = Math.Min(val4, val6);
		float num8 = aabb.Maximum.Y - aabb.Minimum.Y + 2f * Math.Max(0f, heightField.GetAabb(scale, Pose.Identity).Maximum.Y - aabb.Minimum.Y);
		CollisionAlgorithm collisionAlgorithm = CollisionDetection.AlgorithmMatrix[typeof(ConvexShape), shape.GetType()];
		int count = contactSet.Count;
		TriangleShape triangleShape = cdb80ee76c96fc0751dea8d15ee69fa47.c2fabcdff37c34655c8dee4068556b530.Obtain();
		c107cf2db36674a478eaf84e50d31337e c107cf2db36674a478eaf84e50d31337e2 = c107cf2db36674a478eaf84e50d31337e.c2d8ce8b1ccf2b905e3b18f8a5145fe8a();
		c107cf2db36674a478eaf84e50d31337e2.Shape = triangleShape;
		LineSegmentShape lineSegmentShape = cdb80ee76c96fc0751dea8d15ee69fa47.c15ee94e8f68a7da40b4b9010459260d1.Obtain();
		lineSegmentShape.Start = Vector3F.Zero;
		lineSegmentShape.End = (0f - num8) * Vector3F.UnitY;
		c107cf2db36674a478eaf84e50d31337e c107cf2db36674a478eaf84e50d31337e3 = c107cf2db36674a478eaf84e50d31337e.c2d8ce8b1ccf2b905e3b18f8a5145fe8a();
		c107cf2db36674a478eaf84e50d31337e3.Shape = lineSegmentShape;
		c778cf8148fb9ff468ba20f6321de4e73 c778cf8148fb9ff468ba20f6321de4e74 = c778cf8148fb9ff468ba20f6321de4e73.c2d8ce8b1ccf2b905e3b18f8a5145fe8a();
		c778cf8148fb9ff468ba20f6321de4e74.c64743d4d4c42af8c72d03a20d80824b5 = c107cf2db36674a478eaf84e50d31337e2;
		c778cf8148fb9ff468ba20f6321de4e74.ce588e75b2b6b81bd58a657706b7a1553 = c107cf2db36674a478eaf84e50d31337e3;
		c107cf2db36674a478eaf84e50d31337e c107cf2db36674a478eaf84e50d31337e4 = c107cf2db36674a478eaf84e50d31337e.c2d8ce8b1ccf2b905e3b18f8a5145fe8a();
		c107cf2db36674a478eaf84e50d31337e4.Shape = c778cf8148fb9ff468ba20f6321de4e74;
		c107cf2db36674a478eaf84e50d31337e4.Pose = pose;
		CollisionObject collisionObject = cdb80ee76c96fc0751dea8d15ee69fa47.c29a5464f174eae366728cbb2edc64776.Obtain();
		collisionObject.ceabf44cab475c8ec76f26d6553011a78(obj, c107cf2db36674a478eaf84e50d31337e4);
		ContactSet contactSet2 = (flag ? ContactSet.Create(contactSet.ObjectA, collisionObject) : ContactSet.Create(collisionObject, contactSet.ObjectB));
		contactSet2.IsPerturbationTestAllowed = false;
		for (int num9 = num4; num9 <= num5; num9 = Math.Max(num4, num9 + 1))
		{
			for (int num10 = num6; num10 <= num7; num10 = Math.Max(num6, num10 + 1))
			{
				for (int i = 0; i < 2; i++)
				{
					Triangle triangle = heightField.GetTriangle(num9, num10, i != 0);
					triangleShape.Vertex0 = triangle.Vertex0 * scale;
					triangleShape.Vertex1 = triangle.Vertex1 * scale;
					triangleShape.Vertex2 = triangle.Vertex2 * scale;
					if (type == CollisionQueryType.Boolean)
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
						collisionAlgorithm.ComputeCollision(contactSet2, CollisionQueryType.Boolean);
						int haveContact;
						if (!contactSet.HaveContact)
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
							haveContact = (contactSet2.HaveContact ? 1 : 0);
						}
						else
						{
							haveContact = 1;
						}
						contactSet.HaveContact = (byte)haveContact != 0;
						if (contactSet.HaveContact)
						{
							num5 = -1;
							num7 = -1;
							break;
						}
						continue;
					}
					int num11;
					if (!contactSet.HaveContact)
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
						num11 = (int)type;
					}
					else
					{
						num11 = 1;
					}
					CollisionQueryType type2 = (CollisionQueryType)num11;
					collisionAlgorithm.ComputeCollision(contactSet2, type2);
					Vector3F localDirection;
					Vector3F vector2;
					if (contactSet2.HaveContact)
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
						contactSet.HaveContact = true;
						if (contactSet2.Count > 0)
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
							localDirection = triangle.Normal / scale;
							localDirection = pose.ToWorldDirection(localDirection);
							localDirection.TryNormalize();
							Vector3F vector = (flag ? (-contactSet2[0].Normal) : contactSet2[0].Normal);
							if (Vector3F.Dot(vector, localDirection) < WeldingLimit)
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
								vector2 = localDirection;
								Vector3F vector3F2;
								if (!flag)
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
									vector3F2 = contactSet2[0].PositionALocal / scale;
								}
								else
								{
									vector3F2 = contactSet2[0].PositionBLocal / scale;
								}
								Vector3F point = vector3F2;
								GeometryHelper.GetBarycentricFromPoint(triangle, point, out var u, out var v, out var w);
								if (!(u < 0.05f) && !(v < 0.05f))
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
									if (!(w < 0.05f))
									{
										goto IL_081c;
									}
								}
								if (i == 0)
								{
									if (u < v)
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
										if (u < w)
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
											vector2 = pose.ToWorldDirection(heightField.GetTriangle(num9, num10, second: true).Normal / scale);
											vector2.TryNormalize();
											goto IL_081c;
										}
									}
									if (v < w)
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
										if (num10 > 0)
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
											vector2 = pose.ToWorldDirection(heightField.GetTriangle(num9, num10 - 1, second: true).Normal / scale);
											vector2.TryNormalize();
										}
									}
									else if (num9 > 0)
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
										vector2 = pose.ToWorldDirection(heightField.GetTriangle(num9 - 1, num10, second: true).Normal / scale);
										vector2.TryNormalize();
									}
								}
								else if (u < v && u < w)
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
									if (num9 + 2 < heightField.Array.GetLength(0))
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
										vector2 = pose.ToWorldDirection(heightField.GetTriangle(num9 + 1, num10, second: false).Normal / scale);
										vector2.TryNormalize();
									}
								}
								else if (v < w)
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
									vector2 = pose.ToWorldDirection(heightField.GetTriangle(num9, num10, second: false).Normal / scale);
									vector2.TryNormalize();
								}
								else if (num10 + 2 < heightField.Array.GetLength(1))
								{
									vector2 = pose.ToWorldDirection(heightField.GetTriangle(num9, num10 + 1, second: true).Normal / scale);
									vector2.TryNormalize();
								}
								goto IL_081c;
							}
						}
					}
					goto IL_0901;
					IL_0901:
					if (contactSet2.Count <= 0)
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
					float num12 = 0f - contactSet2[0].PenetrationDepth;
					int count2 = contactSet2.Count;
					for (int j = 0; j < count2; j++)
					{
						Contact contact2 = contactSet2[j];
						int num13 = (num10 * (length - 1) + num9) * 2 + i;
						if (flag)
						{
							contact2.FeatureB = num13;
						}
						else
						{
							contact2.FeatureA = num13;
						}
					}
					c9ae4fc785e8cd5184185f6ee6d5a3853.c6aedb1aa24c5098d4ce5c849eb999287(contactSet, contactSet2, type, CollisionDetection.ContactPositionTolerance);
					if (!(num3 > 0f) || (!contactSet.HaveContact && !(num12 < num3)))
					{
						continue;
					}
					if (contactSet.HaveContact)
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
						num3 = 0f;
					}
					else
					{
						num3 = Math.Max(0f, num12);
					}
					val = (int)((aabb.Minimum.X - num3) / num);
					val2 = (int)((aabb.Maximum.X + num3) / num);
					val3 = (int)((aabb.Minimum.Z - num3) / num2);
					val4 = (int)((aabb.Maximum.Z + num3) / num2);
					num4 = Math.Max(num4, val);
					num5 = Math.Min(val2, val5);
					num6 = Math.Max(num6, val3);
					num7 = Math.Min(val4, val6);
					continue;
					IL_081c:
					float c815b55ebf8d6dfe015af8f31bf69cc = Vector3F.Dot(vector2, localDirection) - CollisionDetection.Epsilon;
					cd4808b4cca49e2d5bdc52072c95767d2(flag, contactSet2, localDirection, c815b55ebf8d6dfe015af8f31bf69cc);
					if (contactSet2.Count == 0 && contactSet.Count == 0)
					{
						contactSet2.c196da5f647797a182f2b6d32727c7621 = (flag ? (-vector3F) : vector3F);
						collisionAlgorithm.ComputeCollision(contactSet2, CollisionQueryType.Contacts);
						contactSet2.c196da5f647797a182f2b6d32727c7621 = Vector3F.Zero;
						cd4808b4cca49e2d5bdc52072c95767d2(flag, contactSet2, localDirection, c815b55ebf8d6dfe015af8f31bf69cc);
					}
					if (contactSet2.Count == 0)
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
						if (contactSet.Count == 0)
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
							if (Vector3F.Dot(vector3F, localDirection) < WeldingLimit)
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
								contactSet2.c196da5f647797a182f2b6d32727c7621 = (flag ? (-localDirection) : localDirection);
								collisionAlgorithm.ComputeCollision(contactSet2, CollisionQueryType.Contacts);
								contactSet2.c196da5f647797a182f2b6d32727c7621 = Vector3F.Zero;
								cd4808b4cca49e2d5bdc52072c95767d2(flag, contactSet2, localDirection, c815b55ebf8d6dfe015af8f31bf69cc);
							}
						}
					}
					goto IL_0901;
				}
			}
			while (true)
			{
				switch (4)
				{
				case 0:
					break;
				default:
					goto end_IL_0a7d;
				}
				continue;
				end_IL_0a7d:
				break;
			}
		}
		while (true)
		{
			switch (5)
			{
			case 0:
				continue;
			}
			contactSet2.Recycle();
			cdb80ee76c96fc0751dea8d15ee69fa47.c29a5464f174eae366728cbb2edc64776.Recycle(collisionObject);
			c107cf2db36674a478eaf84e50d31337e4.ca18b9b5faa9851d6355f617787bb5859();
			c778cf8148fb9ff468ba20f6321de4e74.ca18b9b5faa9851d6355f617787bb5859();
			c107cf2db36674a478eaf84e50d31337e3.ca18b9b5faa9851d6355f617787bb5859();
			cdb80ee76c96fc0751dea8d15ee69fa47.c15ee94e8f68a7da40b4b9010459260d1.Recycle(lineSegmentShape);
			c107cf2db36674a478eaf84e50d31337e2.ca18b9b5faa9851d6355f617787bb5859();
			cdb80ee76c96fc0751dea8d15ee69fa47.c2fabcdff37c34655c8dee4068556b530.Recycle(triangleShape);
			if (contactSet.Count == 0)
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
				if (contactSet.HaveContact)
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
					if (type == CollisionQueryType.Contacts)
					{
						goto IL_0b2a;
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
				}
				if (type == CollisionQueryType.ClosestPoints)
				{
					goto IL_0b2a;
				}
			}
			goto IL_0b53;
			IL_0b53:
			if (!CollisionDetection.FullContactSetPerFrame)
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
				if (type == CollisionQueryType.Contacts && count == 0 && contactSet.Count > 0 && contactSet.Count < 4)
				{
					c9ae4fc785e8cd5184185f6ee6d5a3853.c4ed493b7ff558f5648011f2cd6007480(CollisionDetection, contactSet, !flag, c9e389e2926d430157c5786217c5365d6);
				}
				return;
			}
			IL_0b2a:
			Contact cf1b03e8c15642a547bc4024e27883cdf = contact ?? c28775f8cc984bc7de36ff4a5989f3409(contactSet, flag);
			c9ae4fc785e8cd5184185f6ee6d5a3853.c6aedb1aa24c5098d4ce5c849eb999287(contactSet, cf1b03e8c15642a547bc4024e27883cdf, type, CollisionDetection.ContactPositionTolerance);
			goto IL_0b53;
		}
	}

	private static void cd4808b4cca49e2d5bdc52072c95767d2(bool cbd5f4ffb7f6caa84b20b23ef3ac88b62, ContactSet c1627d7366307c41303377e394f0fa017, Vector3F c6afb464d0782c8459b520b0623be5eda, float c815b55ebf8d6dfe015af8f31bf69cc43)
	{
		if (c1627d7366307c41303377e394f0fa017.Count <= 0)
		{
			return;
		}
		if (!cbd5f4ffb7f6caa84b20b23ef3ac88b62)
		{
			if (!(Vector3F.Dot(c1627d7366307c41303377e394f0fa017[0].Normal, c6afb464d0782c8459b520b0623be5eda) < c815b55ebf8d6dfe015af8f31bf69cc43))
			{
				return;
			}
			foreach (Contact item in c1627d7366307c41303377e394f0fa017)
			{
				item.Recycle();
			}
			c1627d7366307c41303377e394f0fa017.Clear();
		}
		else
		{
			if (!(Vector3F.Dot(-c1627d7366307c41303377e394f0fa017[0].Normal, c6afb464d0782c8459b520b0623be5eda) < c815b55ebf8d6dfe015af8f31bf69cc43))
			{
				return;
			}
			foreach (Contact item2 in c1627d7366307c41303377e394f0fa017)
			{
				item2.Recycle();
			}
			c1627d7366307c41303377e394f0fa017.Clear();
		}
	}

	private static Contact c28775f8cc984bc7de36ff4a5989f3409(ContactSet cfc73391dcf53c37de15d54e2434d0199, bool cbd5f4ffb7f6caa84b20b23ef3ac88b62)
	{
		IGeometricObject obj = cfc73391dcf53c37de15d54e2434d0199.ObjectA.GeometricObject;
		IGeometricObject obj2 = cfc73391dcf53c37de15d54e2434d0199.ObjectB.GeometricObject;
		if (cbd5f4ffb7f6caa84b20b23ef3ac88b62)
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
				__ldtoken(HeightFieldAlgorithm.c28775f8cc984bc7de36ff4a5989f3409(ContactSet, bool));
			}
			MathHelper.Swap(ref obj, ref obj2);
		}
		HeightField heightField = (HeightField)obj.Shape;
		Shape shape = obj2.Shape;
		Vector3F scale = obj.Scale;
		Vector3F scale2 = obj2.Scale;
		Pose pose = obj.Pose;
		Pose pose2 = obj2.Pose;
		Vector3F vector3F = pose.ToWorldDirection(Vector3F.UnitY);
		Vector3F localPosition = ((!(shape is ConvexShape convexShape)) ? (shape.InnerPoint * scale2) : convexShape.GetSupportPoint(pose2.ToLocalDirection(-vector3F), scale2));
		Vector3F vector3F2 = pose2.ToWorldPosition(localPosition);
		Vector3F vector3F3 = pose.ToLocalPosition(vector3F2);
		float num = MathHelper.Clamp(vector3F3.X, 0f, heightField.WidthX * scale.X);
		float num2 = MathHelper.Clamp(vector3F3.Z, 0f, heightField.WidthZ * scale.Z);
		float y = heightField.GetHeight(num / scale.X, num2 / scale.Z) * scale.Y;
		Vector3F localPosition2 = new Vector3F(num, y, num2);
		Vector3F vector3F4 = pose.ToWorldPosition(localPosition2);
		Vector3F c2fed316f5bcf4dd4ea7883930f3822f = (vector3F4 + vector3F2) / 2f;
		float num3 = (vector3F4 - vector3F2).Length;
		bool flag = localPosition2.Y >= vector3F3.Y;
		Vector3F vector3F5 = vector3F4 - vector3F2;
		if (!vector3F5.TryNormalize())
		{
			vector3F5 = vector3F;
		}
		if (cbd5f4ffb7f6caa84b20b23ef3ac88b62)
		{
			vector3F5 = -vector3F5;
		}
		bool c72057f882b90cac0b4126f84e6c9aa = flag && shape is RayShape;
		if (!flag)
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
			vector3F5 = -vector3F5;
			num3 = 0f - num3;
		}
		return c9ae4fc785e8cd5184185f6ee6d5a3853.c8918c90b638b2cb75bae14c2683b291d(cfc73391dcf53c37de15d54e2434d0199, c2fed316f5bcf4dd4ea7883930f3822f, vector3F5, num3, c72057f882b90cac0b4126f84e6c9aa);
	}

	/// <summary>
	///       Gets the time of impact between two moving objects.
	///       </summary>
	/// <param name="objectA">The object A.</param>
	/// <param name="targetPoseA">The target pose of A.</param>
	/// <param name="objectB">The object B.</param>
	/// <param name="targetPoseB">The target pose of B.</param>
	/// <param name="allowedPenetration">The allowed penetration. A positive allowed penetration value makes sure that the objects
	///       have a measurable contact at the time of impact.</param>
	/// <returns>The time of impact in the range [0, 1].</returns>
	/// <exception cref="T:System.ArgumentException">
	///       Neither <paramref name="objectA" /> nor <paramref name="objectB" /> contains a 
	///       <see cref="T:DigitalRune.Geometry.Shapes.HeightField" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="objectA" /> or <paramref name="objectB" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.NotSupportedException">
	///   <paramref name="objectA" /> or <paramref name="objectB" /> contains a 
	///       <see cref="T:DigitalRune.Geometry.Shapes.HeightField" /> with a negative scaling. Computing collisions for height fields 
	///       with a negative scaling is not supported.
	///       </exception>
	public override float GetTimeOfImpact(CollisionObject objectA, Pose targetPoseA, CollisionObject objectB, Pose targetPoseB, float allowedPenetration)
	{
		if (!(objectA.GeometricObject.Shape is HeightField))
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
				__ldtoken(HeightFieldAlgorithm.GetTimeOfImpact(CollisionObject, Pose, CollisionObject, Pose, float));
			}
			MathHelper.Swap(ref objectA, ref objectB);
			MathHelper.Swap(ref targetPoseA, ref targetPoseB);
		}
		IGeometricObject geometricObject = objectA.GeometricObject;
		IGeometricObject geometricObject2 = objectB.GeometricObject;
		if (!(geometricObject.Shape is HeightField heightField))
		{
			while (true)
			{
				switch (2)
				{
				case 0:
					break;
				default:
					throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7659));
				}
			}
		}
		if (objectB.GeometricObject.Shape is HeightField)
		{
			while (true)
			{
				switch (4)
				{
				case 0:
					break;
				default:
					return 1f;
				}
			}
		}
		Pose pose = geometricObject.Pose;
		Pose pose2 = geometricObject2.Pose;
		Vector3F scale = geometricObject.Scale;
		Vector3F scale2 = geometricObject2.Scale;
		if (!(scale.X < 0f) && !(scale.Y < 0f))
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
			if (!(scale.Z < 0f))
			{
				Aabb aabb = geometricObject2.Shape.GetAabb(scale2, pose.Inverse * pose2);
				aabb.Grow(geometricObject2.Shape.GetAabb(scale2, targetPoseA.Inverse * targetPoseB));
				TriangleShape triangleShape = cdb80ee76c96fc0751dea8d15ee69fa47.c2fabcdff37c34655c8dee4068556b530.Obtain();
				c107cf2db36674a478eaf84e50d31337e c107cf2db36674a478eaf84e50d31337e2 = c107cf2db36674a478eaf84e50d31337e.c2d8ce8b1ccf2b905e3b18f8a5145fe8a();
				c107cf2db36674a478eaf84e50d31337e2.Shape = triangleShape;
				c107cf2db36674a478eaf84e50d31337e2.Scale = Vector3F.One;
				c107cf2db36674a478eaf84e50d31337e2.Pose = pose;
				CollisionObject collisionObject = cdb80ee76c96fc0751dea8d15ee69fa47.c29a5464f174eae366728cbb2edc64776.Obtain();
				collisionObject.ceabf44cab475c8ec76f26d6553011a78(objectA, c107cf2db36674a478eaf84e50d31337e2);
				CollisionAlgorithm collisionAlgorithm = CollisionDetection.AlgorithmMatrix[typeof(TriangleShape), geometricObject2.Shape.GetType()];
				float[,] array = heightField.Array;
				int length = array.GetLength(0);
				int length2 = array.GetLength(1);
				float num = heightField.WidthX * scale.X / (float)(length - 1);
				float num2 = heightField.WidthZ * scale.Z / (float)(length2 - 1);
				int val = (int)(aabb.Minimum.X / num);
				int val2 = (int)(aabb.Maximum.X / num);
				int val3 = (int)(aabb.Minimum.Z / num2);
				int val4 = (int)(aabb.Maximum.Z / num2);
				int val5 = length - 2;
				int val6 = length2 - 2;
				int num3 = Math.Max(val, 0);
				int num4 = Math.Min(val2, val5);
				int num5 = Math.Max(val3, 0);
				int num6 = Math.Min(val4, val6);
				float num7 = 1f;
				for (int num8 = num3; num8 <= num4; num8 = Math.Max(num3, num8 + 1))
				{
					for (int num9 = num5; num9 <= num6; num9 = Math.Max(num5, num9 + 1))
					{
						for (int i = 0; i < 2; i++)
						{
							Triangle triangle = heightField.GetTriangle(num8, num9, i != 0);
							triangle.Vertex0 *= scale;
							triangle.Vertex1 *= scale;
							triangle.Vertex2 *= scale;
							if (!GeometryHelper.HaveContact(aabb, triangle.Aabb))
							{
								continue;
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
							triangleShape.Vertex0 = triangle.Vertex0;
							triangleShape.Vertex1 = triangle.Vertex1;
							triangleShape.Vertex2 = triangle.Vertex2;
							float timeOfImpact = collisionAlgorithm.GetTimeOfImpact(collisionObject, targetPoseA, objectB, targetPoseB, allowedPenetration);
							num7 = Math.Min(num7, timeOfImpact);
						}
						while (true)
						{
							switch (4)
							{
							case 0:
								break;
							default:
								goto end_IL_0387;
							}
							continue;
							end_IL_0387:
							break;
						}
					}
					while (true)
					{
						switch (3)
						{
						case 0:
							break;
						default:
							goto end_IL_03a7;
						}
						continue;
						end_IL_03a7:
						break;
					}
				}
				while (true)
				{
					switch (5)
					{
					case 0:
						continue;
					}
					cdb80ee76c96fc0751dea8d15ee69fa47.c29a5464f174eae366728cbb2edc64776.Recycle(collisionObject);
					c107cf2db36674a478eaf84e50d31337e2.ca18b9b5faa9851d6355f617787bb5859();
					cdb80ee76c96fc0751dea8d15ee69fa47.c2fabcdff37c34655c8dee4068556b530.Recycle(triangleShape);
					return num7;
				}
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
		throw new NotSupportedException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7497));
	}

	private void cdd132c567958b42347a1aa048bf7a53e(ContactSet cfc73391dcf53c37de15d54e2434d0199, CollisionQueryType cb76a1558f3778afde9d9fa562d4ee745, IGeometricObject cd3e1592c51343934e58671e0ed6fcd31, IGeometricObject ce9a427c60c2f05e1978dc539198893cd, HeightField c3836ed0129b8af477695c9fffe562952, ConvexShape c6523d38be4d4f5503102fb6ebdd2b1c2, bool cbd5f4ffb7f6caa84b20b23ef3ac88b62)
	{
		cfc73391dcf53c37de15d54e2434d0199.HaveContact = false;
		Pose pose = cd3e1592c51343934e58671e0ed6fcd31.Pose;
		Vector3F scale = cd3e1592c51343934e58671e0ed6fcd31.Scale;
		if (!(scale.X < 0f))
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
				__ldtoken(HeightFieldAlgorithm.cdd132c567958b42347a1aa048bf7a53e(ContactSet, CollisionQueryType, IGeometricObject, IGeometricObject, HeightField, ConvexShape, bool));
			}
			if (!(scale.Y < 0f))
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
				if (!(scale.Z < 0f))
				{
					Pose pose2 = ce9a427c60c2f05e1978dc539198893cd.Pose;
					Vector3F scale2 = ce9a427c60c2f05e1978dc539198893cd.Scale;
					Vector3F worldPosition = pose2.ToWorldPosition(c6523d38be4d4f5503102fb6ebdd2b1c2.InnerPoint * scale2);
					worldPosition = pose.ToLocalPosition(worldPosition);
					float num = worldPosition.X / scale.X;
					float num2 = worldPosition.Z / scale.Z;
					if (num < 0f)
					{
						return;
					}
					while (true)
					{
						switch (4)
						{
						case 0:
							continue;
						}
						if (num > c3836ed0129b8af477695c9fffe562952.WidthX || num2 < 0f)
						{
							return;
						}
						if (num2 > c3836ed0129b8af477695c9fffe562952.WidthZ)
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
						ca6905e8d2f7056e25e40451585e99ba4(c3836ed0129b8af477695c9fffe562952, num, num2, out var c19632431916ea40db7b768daafd2c05f, out var c79a3b94c816fc4e87d6348c3713a, out var c5a3c99435afad706eab8cfc388981e);
						c19632431916ea40db7b768daafd2c05f *= scale.Y;
						c79a3b94c816fc4e87d6348c3713a /= scale;
						c79a3b94c816fc4e87d6348c3713a.Normalize();
						c79a3b94c816fc4e87d6348c3713a = pose.ToWorldDirection(c79a3b94c816fc4e87d6348c3713a);
						Vector3F vector3F = pose2.ToLocalDirection(c79a3b94c816fc4e87d6348c3713a);
						Vector3F localPosition = new Vector3F(worldPosition.X, c19632431916ea40db7b768daafd2c05f, worldPosition.Z);
						localPosition = pose.ToWorldPosition(localPosition);
						localPosition = pose2.ToLocalPosition(localPosition);
						Vector3F supportPoint = c6523d38be4d4f5503102fb6ebdd2b1c2.GetSupportPoint(-vector3F, scale2);
						float num3 = Vector3F.Dot(localPosition - supportPoint, vector3F);
						if (num3 < 0f)
						{
							return;
						}
						cfc73391dcf53c37de15d54e2434d0199.HaveContact = true;
						if (cb76a1558f3778afde9d9fa562d4ee745 == CollisionQueryType.Boolean)
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
						Vector3F c2fed316f5bcf4dd4ea7883930f3822f = pose2.ToWorldPosition(supportPoint + vector3F * (num3 / 2f));
						if (cbd5f4ffb7f6caa84b20b23ef3ac88b62)
						{
							c79a3b94c816fc4e87d6348c3713a = -c79a3b94c816fc4e87d6348c3713a;
						}
						Contact contact = c9ae4fc785e8cd5184185f6ee6d5a3853.c8918c90b638b2cb75bae14c2683b291d(cfc73391dcf53c37de15d54e2434d0199, c2fed316f5bcf4dd4ea7883930f3822f, c79a3b94c816fc4e87d6348c3713a, num3, c72057f882b90cac0b4126f84e6c9aa28: false);
						if (cbd5f4ffb7f6caa84b20b23ef3ac88b62)
						{
							contact.FeatureB = c5a3c99435afad706eab8cfc388981e;
						}
						else
						{
							contact.FeatureA = c5a3c99435afad706eab8cfc388981e;
						}
						c9ae4fc785e8cd5184185f6ee6d5a3853.c6aedb1aa24c5098d4ce5c849eb999287(cfc73391dcf53c37de15d54e2434d0199, contact, cb76a1558f3778afde9d9fa562d4ee745, CollisionDetection.ContactPositionTolerance);
						if (!CollisionDetection.FullContactSetPerFrame || cfc73391dcf53c37de15d54e2434d0199.Count >= 3)
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
							Vector3F orthonormal = vector3F.Orthonormal1;
							Vector3F orthonormal2 = vector3F.Orthonormal2;
							for (int i = 0; i < 4; i++)
							{
								supportPoint = c6523d38be4d4f5503102fb6ebdd2b1c2.GetSupportPoint(i switch
								{
									0 => -vector3F + orthonormal, 
									1 => -vector3F - orthonormal, 
									2 => -vector3F + orthonormal2, 
									_ => -vector3F - orthonormal2, 
								}, scale2);
								num3 = Vector3F.Dot(localPosition - supportPoint, vector3F);
								if (num3 >= 0f)
								{
									c2fed316f5bcf4dd4ea7883930f3822f = pose2.ToWorldPosition(supportPoint + vector3F * (num3 / 2f));
									contact = c9ae4fc785e8cd5184185f6ee6d5a3853.c8918c90b638b2cb75bae14c2683b291d(cfc73391dcf53c37de15d54e2434d0199, c2fed316f5bcf4dd4ea7883930f3822f, c79a3b94c816fc4e87d6348c3713a, num3, c72057f882b90cac0b4126f84e6c9aa28: false);
									c9ae4fc785e8cd5184185f6ee6d5a3853.c6aedb1aa24c5098d4ce5c849eb999287(cfc73391dcf53c37de15d54e2434d0199, contact, cb76a1558f3778afde9d9fa562d4ee745, CollisionDetection.ContactPositionTolerance);
								}
							}
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
					}
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
		}
		throw new NotSupportedException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7497));
	}

	private static void ca6905e8d2f7056e25e40451585e99ba4(HeightField c3836ed0129b8af477695c9fffe562952, float c5078b1ca2bd86333c65cb625b996b618, float c65450856399ed37fbf50b0d9cd3565da, out float c19632431916ea40db7b768daafd2c05f, out Vector3F c79a3b94c816fc4e87d6348c3713a1943, out int c5a3c99435afad706eab8cfc388981e35)
	{
		int length = c3836ed0129b8af477695c9fffe562952.Array.GetLength(0);
		int length2 = c3836ed0129b8af477695c9fffe562952.Array.GetLength(1);
		float num = c3836ed0129b8af477695c9fffe562952.WidthX / (float)(length - 1);
		float num2 = c3836ed0129b8af477695c9fffe562952.WidthZ / (float)(length2 - 1);
		int num3 = Math.Min((int)(c5078b1ca2bd86333c65cb625b996b618 / num), length - 2);
		int num4 = Math.Min((int)(c65450856399ed37fbf50b0d9cd3565da / num2), length2 - 2);
		float num5 = c5078b1ca2bd86333c65cb625b996b618 / num - (float)num3;
		float num6 = c65450856399ed37fbf50b0d9cd3565da / num2 - (float)num4;
		bool flag = num5 + num6 > 1f;
		Triangle triangle = c3836ed0129b8af477695c9fffe562952.GetTriangle(num3, num4, flag);
		float y = triangle.Vertex0.Y;
		float y2 = triangle.Vertex1.Y;
		float y3 = triangle.Vertex2.Y;
		triangle.Vertex0.Y = 0f;
		triangle.Vertex1.Y = 0f;
		triangle.Vertex2.Y = 0f;
		GeometryHelper.GetBarycentricFromPoint(triangle, new Vector3F(c5078b1ca2bd86333c65cb625b996b618, 0f, c65450856399ed37fbf50b0d9cd3565da), out var u, out var v, out var w);
		c5a3c99435afad706eab8cfc388981e35 = (num4 * (length - 1) + num3) * 2;
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
				__ldtoken(HeightFieldAlgorithm.ca6905e8d2f7056e25e40451585e99ba4(HeightField, float, float, ref float, ref Vector3F, ref int));
			}
			c5a3c99435afad706eab8cfc388981e35++;
		}
		c19632431916ea40db7b768daafd2c05f = u * y + v * y2 + w * y3;
		triangle.Vertex0.Y = y;
		triangle.Vertex1.Y = y2;
		triangle.Vertex2.Y = y3;
		c79a3b94c816fc4e87d6348c3713a1943 = Vector3F.Cross(triangle.Vertex1 - triangle.Vertex0, triangle.Vertex2 - triangle.Vertex0);
	}

	[CompilerGenerated]
	private void cde6b723243877cf27157a6099d285c48(ContactSet cfc73391dcf53c37de15d54e2434d0199)
	{
		ComputeCollision(cfc73391dcf53c37de15d54e2434d0199, CollisionQueryType.Contacts);
	}
}
