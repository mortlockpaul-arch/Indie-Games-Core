using System;
using System.Collections.Generic;
using DigitalRune.Geometry;
using DigitalRune.Geometry.Meshes;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;

namespace A;

internal class c24f3b4ab921762a6a531fa3822e2f73d
{
	private readonly float cbff03fdc16c1391e13a6980f66b56c22;

	private readonly int cba0f9cd75936246c87ef5131610ec89e;

	private cdfb5b8962b4040fceeeba9fbddd27e32 c7c9af702452497cba0063de94eb6e293;

	private DcelMesh c3fc7f41529b83687d34e0f8dafef96ca;

	private List<DcelFace> c58a8479b538d5e70ccdab0acab84c5b3;

	private List<DcelEdge> c61060d1dad052b00fa5d0e1da87d4d4a;

	private bool cecb2d3a074cdf2a0e4fbf9e1718317eb;

	public c24f3b4ab921762a6a531fa3822e2f73d(int cc17ebb89ee6963d9338138084546c231, float cf8b5bc8a36f10d0f983ce93df3592fec)
	{
		cbff03fdc16c1391e13a6980f66b56c22 = cf8b5bc8a36f10d0f983ce93df3592fec;
		cba0f9cd75936246c87ef5131610ec89e = cc17ebb89ee6963d9338138084546c231;
	}

	public DcelMesh c2d8ce8b1ccf2b905e3b18f8a5145fe8a(IEnumerable<Vector3F> c2ebf3a3e77e8b4507a45357157f26fae)
	{
		c7c9af702452497cba0063de94eb6e293 = cdfb5b8962b4040fceeeba9fbddd27e32.c5e575b1e405b2dad96e3ae6b5455f3c9;
		c3fc7f41529b83687d34e0f8dafef96ca = new DcelMesh();
		c58a8479b538d5e70ccdab0acab84c5b3 = new List<DcelFace>();
		c61060d1dad052b00fa5d0e1da87d4d4a = new List<DcelEdge>();
		List<Vector3F> list = new List<Vector3F>(c2ebf3a3e77e8b4507a45357157f26fae);
		if (list.Count == 0)
		{
			while (true)
			{
				switch (1)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(c24f3b4ab921762a6a531fa3822e2f73d.c2d8ce8b1ccf2b905e3b18f8a5145fe8a(IEnumerable<Vector3F>));
					}
					return null;
				}
			}
		}
		Aabb aabb = new Aabb(list[0], list[0]);
		using (List<Vector3F>.Enumerator enumerator = list.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				Vector3F current = enumerator.Current;
				aabb.Grow(current);
			}
			while (true)
			{
				switch (7)
				{
				case 0:
					break;
				default:
					goto end_IL_008c;
				}
				continue;
				end_IL_008c:
				break;
			}
		}
		Vector3F center = aabb.Center;
		float num = Numeric.EpsilonF * (1f + aabb.Extent.Length);
		List<Vector3F> list2 = new List<Vector3F>();
		for (int i = 0; i < list.Count; i++)
		{
			Vector3F vector3F = list[i];
			bool flag = true;
			for (int j = 0; j < list2.Count; j++)
			{
				Vector3F vector3F2 = list2[j];
				if (!Vector3F.AreNumericallyEqual(vector3F, vector3F2, num))
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
				if ((vector3F - center).LengthSquared > (vector3F2 - center).LengthSquared)
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
					list2[j] = vector3F;
				}
				flag = false;
				break;
			}
			if (flag)
			{
				list2.Add(vector3F);
			}
		}
		while (true)
		{
			switch (5)
			{
			case 0:
				continue;
			}
			list = list2;
			if (list.Count >= 4)
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
				Vector3F vector3F3 = list[0];
				int index = 0;
				for (int k = 1; k < list.Count; k++)
				{
					Vector3F vector3F4 = list[k];
					if (vector3F4.X < vector3F3.X)
					{
						vector3F3 = vector3F4;
						index = k;
					}
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
				list[index] = list[0];
				list[0] = vector3F3;
				Vector3F vector3F5 = list[0];
				int index2 = 0;
				for (int l = 1; l < list.Count; l++)
				{
					Vector3F vector3F6 = list[l];
					if (!(vector3F6.X > vector3F5.X))
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
					vector3F5 = vector3F6;
					index2 = l;
				}
				list[index2] = list[1];
				list[1] = vector3F5;
				Vector3F direction = vector3F5 - vector3F3;
				if (!direction.TryNormalize())
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
					cecb2d3a074cdf2a0e4fbf9e1718317eb = true;
				}
				Line c2217c4fd9f0103c2f9b3dc23c71674b = new Line(vector3F3, direction);
				Vector3F vector3F7 = list[0];
				int num2 = 0;
				float num3 = cd24efc61a620a1e69744c247fec8bdf9(c2217c4fd9f0103c2f9b3dc23c71674b, vector3F7);
				for (int m = 1; m < list.Count; m++)
				{
					Vector3F vector3F8 = list[m];
					float num4 = cd24efc61a620a1e69744c247fec8bdf9(c2217c4fd9f0103c2f9b3dc23c71674b, vector3F8);
					if (!(num4 > num3))
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
					num3 = num4;
					vector3F7 = vector3F8;
					num2 = m;
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
				if (num2 > 2)
				{
					list[num2] = list[2];
					list[2] = vector3F7;
				}
				Triangle triangle = new Triangle(vector3F3, vector3F5, vector3F7);
				Vector3F normal = triangle.Normal;
				Vector3F vector3F9 = list[0];
				int num5 = 0;
				float num6 = Math.Abs(Vector3F.Dot(normal, vector3F9 - triangle.Vertex0));
				for (int n = 1; n < list.Count; n++)
				{
					Vector3F vector3F10 = list[n];
					float num7 = Math.Abs(Vector3F.Dot(normal, vector3F10 - triangle.Vertex0));
					if (!(num7 > num6))
					{
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
					num6 = num7;
					vector3F9 = vector3F10;
					num5 = n;
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
				if (num5 > 3)
				{
					list[num5] = list[3];
					list[3] = vector3F9;
				}
				if (num6 < num)
				{
					cecb2d3a074cdf2a0e4fbf9e1718317eb = true;
				}
			}
			foreach (Vector3F item2 in list)
			{
				c28f483887e74e354e355f6b3d3fdc6bd(item2);
			}
			if (c3fc7f41529b83687d34e0f8dafef96ca.Faces.Count != 2)
			{
				if (!Numeric.IsZero(cbff03fdc16c1391e13a6980f66b56c22) || c3fc7f41529b83687d34e0f8dafef96ca.Vertices.Count >= cba0f9cd75936246c87ef5131610ec89e)
				{
					List<Plane> list3 = new List<Plane>();
					foreach (DcelFace face in c3fc7f41529b83687d34e0f8dafef96ca.Faces)
					{
						Vector3F c92b2ed2c76c9d5b2e6f8466991b83c = face.c92b2ed2c76c9d5b2e6f8466991b83c86;
						if (!c92b2ed2c76c9d5b2e6f8466991b83c.TryNormalize())
						{
							continue;
						}
						Plane item = new Plane(c92b2ed2c76c9d5b2e6f8466991b83c, face.Boundary.Origin.Position);
						item.DistanceFromOrigin += cbff03fdc16c1391e13a6980f66b56c22;
						bool flag2 = false;
						for (int num8 = 0; num8 < list3.Count; num8++)
						{
							Plane plane = list3[num8];
							if (!Numeric.AreEqual(Vector3F.Dot(item.Normal, plane.Normal), 1f))
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
							list3[num8] = new Plane(item.Normal, Math.Max(item.DistanceFromOrigin, plane.DistanceFromOrigin));
							flag2 = true;
							break;
						}
						if (flag2)
						{
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
						list3.Add(item);
					}
					c3fc7f41529b83687d34e0f8dafef96ca = DcelMesh.CreateCube();
					IEnumerator<DcelVertex> enumerator4 = c3fc7f41529b83687d34e0f8dafef96ca.Vertices.GetEnumerator();
					try
					{
						while (enumerator4.MoveNext())
						{
							DcelVertex current4 = enumerator4.Current;
							float x = ((current4.Position.X < 0f) ? aabb.Minimum.X : aabb.Maximum.X);
							float y = ((current4.Position.Y < 0f) ? aabb.Minimum.Y : aabb.Maximum.Y);
							float z;
							if (!(current4.Position.Z < 0f))
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
								z = aabb.Maximum.Z;
							}
							else
							{
								z = aabb.Minimum.Z;
							}
							current4.Position = new Vector3F(x, y, z);
						}
					}
					finally
					{
						if (enumerator4 != null)
						{
							while (true)
							{
								switch (4)
								{
								case 0:
									continue;
								}
								enumerator4.Dispose();
								break;
							}
						}
					}
					for (int num9 = 0; num9 < list3.Count; num9++)
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
						if (c3fc7f41529b83687d34e0f8dafef96ca.Vertices.Count < cba0f9cd75936246c87ef5131610ec89e)
						{
							int num10 = c10b0ddf48d151a4741bb0495fe610d87(list3, c3fc7f41529b83687d34e0f8dafef96ca);
							if (num10 < 0)
							{
								break;
							}
							Plane plane2 = list3[num10];
							list3.RemoveAt(num10);
							bool flag3 = false;
							using (List<Vector3F>.Enumerator enumerator5 = list.GetEnumerator())
							{
								while (true)
								{
									if (enumerator5.MoveNext())
									{
										Vector3F current5 = enumerator5.Current;
										float num11 = Vector3F.Dot(current5, plane2.Normal) - (plane2.DistanceFromOrigin - cbff03fdc16c1391e13a6980f66b56c22);
										if (!(num11 > num))
										{
											continue;
										}
										while (true)
										{
											switch (6)
											{
											case 0:
												continue;
											}
											flag3 = true;
											break;
										}
										break;
									}
									while (true)
									{
										switch (5)
										{
										case 0:
											break;
										default:
											goto end_IL_0734;
										}
										continue;
										end_IL_0734:
										break;
									}
									break;
								}
							}
							if (!flag3)
							{
								c3fc7f41529b83687d34e0f8dafef96ca.CutConvex(plane2);
							}
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
					return c3fc7f41529b83687d34e0f8dafef96ca;
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
			return c3fc7f41529b83687d34e0f8dafef96ca;
		}
	}

	public void c28f483887e74e354e355f6b3d3fdc6bd(Vector3F cfef46e983928cc4e6880c424d1468b93)
	{
		switch (c7c9af702452497cba0063de94eb6e293)
		{
		case cdfb5b8962b4040fceeeba9fbddd27e32.c5e575b1e405b2dad96e3ae6b5455f3c9:
			c82763820f8abb5d0a8b8893423d87e6f(cfef46e983928cc4e6880c424d1468b93);
			break;
		case cdfb5b8962b4040fceeeba9fbddd27e32.c874f23cc1913405a1cf8ac84ab912400:
			cfbd065c218684fb84849fe26af8828d0(cfef46e983928cc4e6880c424d1468b93);
			break;
		case cdfb5b8962b4040fceeeba9fbddd27e32.c55e22ed7eb8ba14c65211f35868a204e:
			cae71fb917d09d10f1098c844daf3310b(cfef46e983928cc4e6880c424d1468b93);
			break;
		case cdfb5b8962b4040fceeeba9fbddd27e32.ca224286fc54b8fdffc0cd437cbf90856:
			c2b517c30c4e40d24582eb1e674b179d4(cfef46e983928cc4e6880c424d1468b93);
			break;
		default:
			c4f4ad3f8298cc36b3175a665b5e3a553(cfef46e983928cc4e6880c424d1468b93);
			break;
		}
	}

	private void c82763820f8abb5d0a8b8893423d87e6f(Vector3F cfef46e983928cc4e6880c424d1468b93)
	{
		c3fc7f41529b83687d34e0f8dafef96ca.Vertex = new DcelVertex(cfef46e983928cc4e6880c424d1468b93, null);
		c3fc7f41529b83687d34e0f8dafef96ca.Dirty = true;
		c7c9af702452497cba0063de94eb6e293 = cdfb5b8962b4040fceeeba9fbddd27e32.c874f23cc1913405a1cf8ac84ab912400;
	}

	private void cfbd065c218684fb84849fe26af8828d0(Vector3F cfef46e983928cc4e6880c424d1468b93)
	{
		if (!Vector3F.AreNumericallyEqual(c3fc7f41529b83687d34e0f8dafef96ca.Vertex.Position, cfef46e983928cc4e6880c424d1468b93))
		{
			DcelVertex dcelVertex = new DcelVertex(cfef46e983928cc4e6880c424d1468b93, null);
			DcelEdge dcelEdge = new DcelEdge();
			DcelEdge dcelEdge2 = new DcelEdge();
			dcelEdge.Origin = c3fc7f41529b83687d34e0f8dafef96ca.Vertex;
			dcelEdge2.Origin = dcelVertex;
			dcelEdge.Twin = dcelEdge2;
			dcelEdge2.Twin = dcelEdge;
			c3fc7f41529b83687d34e0f8dafef96ca.Vertex.Edge = dcelEdge;
			dcelVertex.Edge = dcelEdge2;
			c3fc7f41529b83687d34e0f8dafef96ca.Dirty = true;
			c7c9af702452497cba0063de94eb6e293 = cdfb5b8962b4040fceeeba9fbddd27e32.c55e22ed7eb8ba14c65211f35868a204e;
		}
	}

	private void cae71fb917d09d10f1098c844daf3310b(Vector3F cfef46e983928cc4e6880c424d1468b93)
	{
		if (!Vector3F.AreNumericallyEqual(cfef46e983928cc4e6880c424d1468b93, c3fc7f41529b83687d34e0f8dafef96ca.Vertex.Position) && !Vector3F.AreNumericallyEqual(cfef46e983928cc4e6880c424d1468b93, c3fc7f41529b83687d34e0f8dafef96ca.Vertex.Edge.Twin.Origin.Position))
		{
			DcelEdge edge = c3fc7f41529b83687d34e0f8dafef96ca.Vertex.Edge;
			switch (DcelMesh.cf8c8fb520854eb9b380072d6514b548f(cfef46e983928cc4e6880c424d1468b93, c3fc7f41529b83687d34e0f8dafef96ca.Vertex.Edge))
			{
			case c3bf6880584bac8647ae5b3c0b6ef826a.c0277d06f7b510805fdeb5fcc22f3e783:
				edge.Origin.Position = cfef46e983928cc4e6880c424d1468b93;
				c3fc7f41529b83687d34e0f8dafef96ca.Dirty = true;
				return;
			case c3bf6880584bac8647ae5b3c0b6ef826a.c80b2e4b967d0b0bceebb2e229d32e970:
				edge.Twin.Origin.Position = cfef46e983928cc4e6880c424d1468b93;
				c3fc7f41529b83687d34e0f8dafef96ca.Dirty = true;
				return;
			case c3bf6880584bac8647ae5b3c0b6ef826a.c0daadf83b0eef7c5bbddd30f172eeeb3:
				return;
			}
			c7c9af702452497cba0063de94eb6e293 = cdfb5b8962b4040fceeeba9fbddd27e32.ca224286fc54b8fdffc0cd437cbf90856;
			DcelVertex vertex = c3fc7f41529b83687d34e0f8dafef96ca.Vertex;
			DcelVertex origin = edge.Twin.Origin;
			DcelVertex dcelVertex = new DcelVertex(cfef46e983928cc4e6880c424d1468b93, null);
			DcelEdge dcelEdge = edge;
			DcelEdge twin = dcelEdge.Twin;
			DcelEdge dcelEdge2 = new DcelEdge();
			DcelEdge dcelEdge3 = new DcelEdge();
			DcelEdge dcelEdge4 = new DcelEdge();
			DcelEdge dcelEdge5 = new DcelEdge();
			DcelFace dcelFace = new DcelFace();
			DcelFace dcelFace2 = new DcelFace();
			dcelEdge.Next = dcelEdge2;
			dcelEdge.Previous = dcelEdge5;
			twin.Next = dcelEdge4;
			twin.Previous = dcelEdge3;
			dcelVertex.Edge = dcelEdge5;
			dcelEdge2.Origin = origin;
			dcelEdge3.Origin = dcelVertex;
			dcelEdge5.Origin = dcelVertex;
			dcelEdge4.Origin = vertex;
			dcelEdge2.Twin = dcelEdge3;
			dcelEdge3.Twin = dcelEdge2;
			dcelEdge5.Twin = dcelEdge4;
			dcelEdge4.Twin = dcelEdge5;
			dcelEdge2.Previous = dcelEdge;
			dcelEdge3.Previous = dcelEdge4;
			dcelEdge5.Previous = dcelEdge2;
			dcelEdge4.Previous = twin;
			dcelEdge2.Next = dcelEdge5;
			dcelEdge3.Next = twin;
			dcelEdge5.Next = dcelEdge;
			dcelEdge4.Next = dcelEdge3;
			dcelFace.Boundary = dcelEdge;
			dcelFace2.Boundary = dcelEdge3;
			DcelFace dcelFace3 = (dcelEdge5.Face = dcelFace);
			DcelFace face = (dcelEdge2.Face = dcelFace3);
			dcelEdge.Face = face;
			DcelFace dcelFace6 = (dcelEdge4.Face = dcelFace2);
			DcelFace face2 = (twin.Face = dcelFace6);
			dcelEdge3.Face = face2;
			c58a8479b538d5e70ccdab0acab84c5b3.Add(dcelFace);
			c58a8479b538d5e70ccdab0acab84c5b3.Add(dcelFace2);
			c3fc7f41529b83687d34e0f8dafef96ca.Dirty = true;
		}
	}

	private void c2b517c30c4e40d24582eb1e674b179d4(Vector3F cfef46e983928cc4e6880c424d1468b93)
	{
		DcelFace face = c3fc7f41529b83687d34e0f8dafef96ca.Vertex.Edge.Face;
		DcelFace face2 = face.Boundary.Twin.Face;
		if (!cecb2d3a074cdf2a0e4fbf9e1718317eb)
		{
			switch (DcelMesh.cf8c8fb520854eb9b380072d6514b548f(cfef46e983928cc4e6880c424d1468b93, face))
			{
			case c3bf6880584bac8647ae5b3c0b6ef826a.cdbbdc5140026b87b9842a38afa9c326c:
				c1b8697c081217005f83e063486edd2b5(face, cfef46e983928cc4e6880c424d1468b93);
				return;
			case c3bf6880584bac8647ae5b3c0b6ef826a.ccc99115cbab7b1097821c06452ce7c72:
				c1b8697c081217005f83e063486edd2b5(face2, cfef46e983928cc4e6880c424d1468b93);
				return;
			}
		}
		cecb2d3a074cdf2a0e4fbf9e1718317eb = true;
		Vector3F cbc85e5134c93a5ab94922ecd0cf4f = face.c92b2ed2c76c9d5b2e6f8466991b83c86 / face.c92b2ed2c76c9d5b2e6f8466991b83c86.Length;
		DcelEdge dcelEdge = c3fc7f41529b83687d34e0f8dafef96ca.Vertex.Edge;
		DcelEdge dcelEdge2 = null;
		while (true)
		{
			c3bf6880584bac8647ae5b3c0b6ef826a c3bf6880584bac8647ae5b3c0b6ef826a2 = DcelMesh.cf8c8fb520854eb9b380072d6514b548f(cfef46e983928cc4e6880c424d1468b93, dcelEdge, cbc85e5134c93a5ab94922ecd0cf4f);
			if (c3bf6880584bac8647ae5b3c0b6ef826a2 != c3bf6880584bac8647ae5b3c0b6ef826a.cdbbdc5140026b87b9842a38afa9c326c && c3bf6880584bac8647ae5b3c0b6ef826a2 != c3bf6880584bac8647ae5b3c0b6ef826a.c0277d06f7b510805fdeb5fcc22f3e783)
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
					__ldtoken(c24f3b4ab921762a6a531fa3822e2f73d.c2b517c30c4e40d24582eb1e674b179d4(Vector3F));
				}
				if (c3bf6880584bac8647ae5b3c0b6ef826a2 != c3bf6880584bac8647ae5b3c0b6ef826a.c80b2e4b967d0b0bceebb2e229d32e970)
				{
					dcelEdge = dcelEdge.Next;
					if (dcelEdge != c3fc7f41529b83687d34e0f8dafef96ca.Vertex.Edge)
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
					break;
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
			dcelEdge2 = dcelEdge;
			break;
		}
		if (dcelEdge2 == null)
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
		DcelVertex dcelVertex = null;
		DcelVertex dcelVertex2 = null;
		dcelEdge = dcelEdge2.Next;
		while (true)
		{
			c3bf6880584bac8647ae5b3c0b6ef826a c3bf6880584bac8647ae5b3c0b6ef826a3 = DcelMesh.cf8c8fb520854eb9b380072d6514b548f(cfef46e983928cc4e6880c424d1468b93, dcelEdge, cbc85e5134c93a5ab94922ecd0cf4f);
			if (c3bf6880584bac8647ae5b3c0b6ef826a3 == c3bf6880584bac8647ae5b3c0b6ef826a.ccc99115cbab7b1097821c06452ce7c72)
			{
				dcelVertex2 = dcelEdge.Origin;
				break;
			}
			dcelEdge = dcelEdge.Next;
			if (dcelEdge != dcelEdge2.Next)
			{
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
		if (dcelVertex2 == null)
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
		dcelEdge = dcelEdge2.Previous;
		while (true)
		{
			c3bf6880584bac8647ae5b3c0b6ef826a c3bf6880584bac8647ae5b3c0b6ef826a4 = DcelMesh.cf8c8fb520854eb9b380072d6514b548f(cfef46e983928cc4e6880c424d1468b93, dcelEdge, cbc85e5134c93a5ab94922ecd0cf4f);
			if (c3bf6880584bac8647ae5b3c0b6ef826a4 == c3bf6880584bac8647ae5b3c0b6ef826a.ccc99115cbab7b1097821c06452ce7c72)
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
				dcelVertex = dcelEdge.Next.Origin;
				break;
			}
			dcelEdge = dcelEdge.Previous;
			if (dcelEdge != dcelEdge2.Previous)
			{
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
		if (dcelVertex == null)
		{
			return;
		}
		DcelEdge edge = dcelVertex.Edge;
		if (edge.Face != face)
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
			edge = dcelVertex.Edge.Previous.Twin;
		}
		DcelVertex dcelVertex3 = new DcelVertex(cfef46e983928cc4e6880c424d1468b93, null);
		dcelVertex3.Edge = new DcelEdge();
		DcelEdge dcelEdge3 = new DcelEdge();
		DcelEdge dcelEdge4 = new DcelEdge();
		DcelEdge edge2 = dcelVertex3.Edge;
		DcelEdge dcelEdge5 = new DcelEdge();
		dcelEdge3.Origin = dcelVertex;
		dcelEdge3.Face = face;
		dcelEdge3.Next = edge2;
		dcelEdge3.Twin = dcelEdge4;
		dcelEdge4.Origin = dcelVertex3;
		dcelEdge4.Face = face2;
		dcelEdge4.Previous = dcelEdge5;
		dcelEdge4.Twin = dcelEdge3;
		edge2.Origin = dcelVertex3;
		edge2.Face = face;
		edge2.Previous = dcelEdge3;
		edge2.Twin = dcelEdge5;
		dcelEdge5.Origin = dcelVertex2;
		dcelEdge5.Face = face2;
		dcelEdge5.Next = dcelEdge4;
		dcelEdge5.Twin = edge2;
		dcelEdge3.Previous = ((dcelVertex.Edge.Face == face) ? dcelVertex.Edge.Previous : dcelVertex.Edge.Twin);
		DcelEdge next;
		if (dcelVertex2.Edge.Face != face)
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
			next = dcelVertex2.Edge.Twin.Next;
		}
		else
		{
			next = dcelVertex2.Edge;
		}
		edge2.Next = next;
		dcelEdge4.Next = dcelEdge3.Previous.Twin;
		dcelEdge5.Previous = edge2.Next.Twin;
		face.Boundary = dcelEdge;
		face2.Boundary = dcelEdge.Twin;
		dcelVertex.Edge = dcelEdge3;
		dcelVertex2.Edge = edge2.Next;
		dcelEdge3.Previous.Next = dcelEdge3;
		dcelEdge4.Next.Previous = dcelEdge4;
		edge2.Next.Previous = edge2;
		dcelEdge5.Previous.Next = dcelEdge5;
		c3fc7f41529b83687d34e0f8dafef96ca.Vertex = dcelVertex;
		c3fc7f41529b83687d34e0f8dafef96ca.Dirty = true;
	}

	private void c1b8697c081217005f83e063486edd2b5(DcelFace c0b89ebd3492c28b5af64c64c59f1627f, Vector3F cfef46e983928cc4e6880c424d1468b93)
	{
		c58a8479b538d5e70ccdab0acab84c5b3.Remove(c0b89ebd3492c28b5af64c64c59f1627f);
		DcelVertex dcelVertex = new DcelVertex(cfef46e983928cc4e6880c424d1468b93, null);
		DcelEdge dcelEdge = c0b89ebd3492c28b5af64c64c59f1627f.Boundary;
		DcelEdge dcelEdge2 = null;
		do
		{
			DcelFace dcelFace = new DcelFace();
			dcelFace.Boundary = dcelEdge;
			dcelEdge.Face = dcelFace;
			c58a8479b538d5e70ccdab0acab84c5b3.Add(dcelFace);
			dcelEdge.Next = new DcelEdge();
			dcelEdge.Previous = new DcelEdge();
			dcelEdge.Next.Face = dcelFace;
			dcelEdge.Next.Origin = dcelEdge.Twin.Origin;
			dcelEdge.Next.Previous = dcelEdge;
			dcelEdge.Next.Next = dcelEdge.Previous;
			dcelEdge.Previous.Face = dcelFace;
			dcelEdge.Previous.Origin = dcelVertex;
			dcelEdge.Previous.Previous = dcelEdge.Next;
			dcelEdge.Previous.Next = dcelEdge;
			if (dcelEdge2 != null)
			{
				dcelEdge2.Next.Twin = dcelEdge.Previous;
				dcelEdge.Previous.Twin = dcelEdge2.Next;
			}
			dcelEdge2 = dcelEdge;
			dcelEdge = dcelEdge.Twin.Previous.Twin;
		}
		while (dcelEdge.Face == c0b89ebd3492c28b5af64c64c59f1627f);
		while (true)
		{
			switch (4)
			{
			case 0:
				continue;
			}
			if (1 == 0)
			{
				__ldtoken(c24f3b4ab921762a6a531fa3822e2f73d.c1b8697c081217005f83e063486edd2b5(DcelFace, Vector3F));
			}
			dcelEdge2.Next.Twin = dcelEdge.Previous;
			dcelEdge.Previous.Twin = dcelEdge2.Next;
			dcelVertex.Edge = dcelEdge.Previous;
			c3fc7f41529b83687d34e0f8dafef96ca.Dirty = true;
			c7c9af702452497cba0063de94eb6e293 = cdfb5b8962b4040fceeeba9fbddd27e32.cfc563710ce52622b1f3698cb93277585;
			return;
		}
	}

	private bool c4f4ad3f8298cc36b3175a665b5e3a553(Vector3F cfef46e983928cc4e6880c424d1468b93)
	{
		DcelFace dcelFace = null;
		int count = c58a8479b538d5e70ccdab0acab84c5b3.Count;
		for (int i = 0; i < count; i++)
		{
			DcelFace dcelFace2 = c58a8479b538d5e70ccdab0acab84c5b3[i];
			if (DcelMesh.cf8c8fb520854eb9b380072d6514b548f(cfef46e983928cc4e6880c424d1468b93, dcelFace2) != c3bf6880584bac8647ae5b3c0b6ef826a.cdbbdc5140026b87b9842a38afa9c326c)
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
				__ldtoken(c24f3b4ab921762a6a531fa3822e2f73d.c4f4ad3f8298cc36b3175a665b5e3a553(Vector3F));
			}
			dcelFace = dcelFace2;
			break;
		}
		if (dcelFace == null)
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
		c61060d1dad052b00fa5d0e1da87d4d4a.Clear();
		Stack<DcelFace> stack = new Stack<DcelFace>();
		stack.Push(dcelFace);
		dcelFace.Tag = 1;
		c58a8479b538d5e70ccdab0acab84c5b3.Remove(dcelFace);
		double num = double.PositiveInfinity;
		DcelEdge dcelEdge = null;
		while (stack.Count > 0)
		{
			dcelFace = stack.Pop();
			DcelEdge[] array = new DcelEdge[3];
			array[0] = dcelFace.Boundary;
			array[1] = array[0].Next;
			array[2] = array[1].Next;
			for (int j = 0; j < 3; j++)
			{
				DcelFace face = array[j].Twin.Face;
				if (face == null)
				{
					continue;
				}
				DcelMesh.cf8c8fb520854eb9b380072d6514b548f(cfef46e983928cc4e6880c424d1468b93, face, out var caf24d26da4b746e8b133be35f8167c);
				if (caf24d26da4b746e8b133be35f8167c >= 0f)
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
					if (face.Tag == 1)
					{
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
					c58a8479b538d5e70ccdab0acab84c5b3.Remove(face);
					stack.Push(face);
					face.Tag = 1;
					continue;
				}
				if (num > (double)caf24d26da4b746e8b133be35f8167c)
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
					dcelEdge = array[j];
					num = caf24d26da4b746e8b133be35f8167c;
				}
				array[j].Tag = 1;
				c61060d1dad052b00fa5d0e1da87d4d4a.Add(array[j]);
			}
		}
		while (true)
		{
			switch (2)
			{
			case 0:
				continue;
			}
			if (dcelEdge == null)
			{
				while (true)
				{
					switch (7)
					{
					case 0:
						break;
					default:
						throw new GeometryException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(17002));
					}
				}
			}
			DcelVertex dcelVertex = new DcelVertex(cfef46e983928cc4e6880c424d1468b93, null);
			DcelEdge dcelEdge2 = dcelEdge;
			DcelEdge dcelEdge3 = null;
			do
			{
				dcelEdge2.Tag = 2;
				dcelEdge2.Origin.Edge = dcelEdge2;
				DcelFace dcelFace3 = new DcelFace();
				dcelFace3.Boundary = dcelEdge2;
				dcelEdge2.Face = dcelFace3;
				c58a8479b538d5e70ccdab0acab84c5b3.Add(dcelFace3);
				dcelEdge2.Next = new DcelEdge();
				dcelEdge2.Previous = new DcelEdge();
				c61060d1dad052b00fa5d0e1da87d4d4a.Add(dcelEdge2.Next);
				c61060d1dad052b00fa5d0e1da87d4d4a.Add(dcelEdge2.Previous);
				dcelEdge2.Next.Face = dcelFace3;
				dcelEdge2.Next.Origin = dcelEdge2.Twin.Origin;
				dcelEdge2.Next.Previous = dcelEdge2;
				dcelEdge2.Next.Next = dcelEdge2.Previous;
				dcelEdge2.Next.Tag = 2;
				dcelEdge2.Previous.Face = dcelFace3;
				dcelEdge2.Previous.Origin = dcelVertex;
				dcelEdge2.Previous.Previous = dcelEdge2.Next;
				dcelEdge2.Previous.Next = dcelEdge2;
				dcelEdge2.Previous.Tag = 2;
				if (dcelEdge3 != null)
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
					dcelEdge3.Next.Twin = dcelEdge2.Previous;
					dcelEdge2.Previous.Twin = dcelEdge3.Next;
				}
				dcelEdge3 = dcelEdge2;
				dcelEdge2 = dcelEdge2.Twin.Previous.Twin;
				int num2 = 0;
				while (dcelEdge2.Tag == 0)
				{
					dcelEdge2 = dcelEdge2.Previous.Twin;
					num2++;
					if (num2 != 100)
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
						throw new GeometryException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(17002));
					}
				}
				while (true)
				{
					switch (1)
					{
					case 0:
						break;
					default:
						goto end_IL_03a3;
					}
					continue;
					end_IL_03a3:
					break;
				}
			}
			while (dcelEdge2.Tag == 1);
			dcelEdge3.Next.Twin = dcelEdge2.Previous;
			dcelEdge2.Previous.Twin = dcelEdge3.Next;
			dcelVertex.Edge = dcelEdge.Previous;
			c3fc7f41529b83687d34e0f8dafef96ca.Vertex = dcelEdge.Origin;
			int count2 = c61060d1dad052b00fa5d0e1da87d4d4a.Count;
			for (int k = 0; k < count2; k++)
			{
				c61060d1dad052b00fa5d0e1da87d4d4a[k].Tag = 0;
			}
			c3fc7f41529b83687d34e0f8dafef96ca.Dirty = true;
			return true;
		}
	}

	private static int c10b0ddf48d151a4741bb0495fe610d87(List<Plane> ce847552b1af64ebc2badfe5f10f37d03, DcelMesh cf1674ba51e57b7e621e83565f0591384)
	{
		float num = 0f;
		int result = -1;
		for (int i = 0; i < ce847552b1af64ebc2badfe5f10f37d03.Count; i++)
		{
			Plane plane = ce847552b1af64ebc2badfe5f10f37d03[i];
			float num2 = 0f;
			float num3 = 0f;
			IEnumerator<DcelVertex> enumerator = cf1674ba51e57b7e621e83565f0591384.Vertices.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					DcelVertex current = enumerator.Current;
					float val = Vector3F.Dot(current.Position, plane.Normal) - plane.DistanceFromOrigin;
					num2 = Math.Min(val, num2);
					num3 = Math.Max(val, num3);
				}
				while (true)
				{
					switch (6)
					{
					case 0:
						continue;
					}
					if (1 == 0)
					{
						__ldtoken(c24f3b4ab921762a6a531fa3822e2f73d.c10b0ddf48d151a4741bb0495fe610d87(List<Plane>, DcelMesh));
					}
					break;
				}
			}
			finally
			{
				if (enumerator != null)
				{
					while (true)
					{
						switch (3)
						{
						case 0:
							continue;
						}
						enumerator.Dispose();
						break;
					}
				}
			}
			float num4 = num3 - num2;
			float num5 = num3 / num4;
			if (!(num5 > num))
			{
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
			num = num5;
			result = i;
		}
		while (true)
		{
			switch (7)
			{
			case 0:
				continue;
			}
			return result;
		}
	}

	private static float cd24efc61a620a1e69744c247fec8bdf9(Line c2217c4fd9f0103c2f9b3dc23c71674b5, Vector3F cfef46e983928cc4e6880c424d1468b93)
	{
		Vector3F vector3F = cfef46e983928cc4e6880c424d1468b93 - c2217c4fd9f0103c2f9b3dc23c71674b5.PointOnLine;
		return (vector3F - Vector3F.ProjectTo(vector3F, c2217c4fd9f0103c2f9b3dc23c71674b5.Direction)).LengthSquared;
	}

	internal void c0cb1d93ea51a9bbf37a29ce659e737b7()
	{
		for (int i = 0; i < c58a8479b538d5e70ccdab0acab84c5b3.Count; i++)
		{
			DcelFace dcelFace = c58a8479b538d5e70ccdab0acab84c5b3[i];
			if (!Numeric.IsZero(dcelFace.c92b2ed2c76c9d5b2e6f8466991b83c86.Length))
			{
				continue;
			}
			DcelEdge boundary = dcelFace.Boundary;
			DcelEdge dcelEdge = boundary;
			float num = dcelEdge.c8c9313fee1ca2682f0170d92769928f1;
			boundary = boundary.Next;
			float c8c9313fee1ca2682f0170d92769928f = boundary.c8c9313fee1ca2682f0170d92769928f1;
			if (num < c8c9313fee1ca2682f0170d92769928f)
			{
				dcelEdge = boundary;
				num = c8c9313fee1ca2682f0170d92769928f;
			}
			boundary = boundary.Next;
			c8c9313fee1ca2682f0170d92769928f = boundary.c8c9313fee1ca2682f0170d92769928f1;
			if (num < c8c9313fee1ca2682f0170d92769928f)
			{
				dcelEdge = boundary;
				num = c8c9313fee1ca2682f0170d92769928f;
			}
			DcelEdge next = dcelEdge.Next;
			DcelEdge next2 = next.Next;
			DcelFace face = dcelEdge.Twin.Face;
			DcelEdge next3 = dcelEdge.Twin.Next;
			DcelEdge next4 = next3.Next;
			if (face == null)
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
				__ldtoken(c24f3b4ab921762a6a531fa3822e2f73d.c0cb1d93ea51a9bbf37a29ce659e737b7());
			}
			if (Numeric.IsZero(face.c92b2ed2c76c9d5b2e6f8466991b83c86.Length))
			{
				c58a8479b538d5e70ccdab0acab84c5b3.Remove(dcelFace);
				c58a8479b538d5e70ccdab0acab84c5b3.Remove(face);
				dcelEdge.Origin.Edge = next2.Twin;
				next.Origin.Edge = next4.Twin;
				next2.Origin.Edge = next.Twin;
				next.Twin.Twin = next4.Twin;
				next4.Twin.Twin = next.Twin;
				next2.Twin.Twin = next3.Twin;
				next3.Twin.Twin = next2.Twin;
				for (boundary = next4; boundary != next4; boundary = boundary.Previous.Twin)
				{
					boundary.Origin = next2.Origin;
				}
			}
			else
			{
				next3.Origin.Edge = next3;
				next.Origin.Edge = next;
				dcelEdge.Origin = next4.Origin;
				dcelEdge.Twin.Origin = next2.Origin;
				dcelFace.Boundary = dcelEdge;
				next2.Face = dcelFace;
				next3.Face = dcelFace;
				face.Boundary = dcelEdge.Twin;
				dcelEdge.Twin.Face = face;
				next.Face = face;
				next3.Next = dcelEdge;
				dcelEdge.Next = next2;
				next2.Next = next3;
				next3.Previous = next2;
				next2.Previous = dcelEdge;
				dcelEdge.Previous = next3;
				next4.Next = next;
				next.Next = dcelEdge.Twin;
				dcelEdge.Twin.Next = next4;
				next4.Previous = dcelEdge.Twin;
				dcelEdge.Twin.Previous = next;
				next.Previous = next4;
			}
			i = -1;
		}
	}
}
