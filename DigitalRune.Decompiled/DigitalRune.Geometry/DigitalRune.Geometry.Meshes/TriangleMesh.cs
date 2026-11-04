using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using A;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DigitalRune.Geometry.Meshes;

/// <summary>
///       Provides a simple <see cref="T:DigitalRune.Geometry.Meshes.ITriangleMesh" /> implementation using vertex and index lists.
///       </summary>
[Serializable]
public sealed class TriangleMesh : ITriangleMesh, ICloneable
{
	private class cc467e46599906a5d357036985a0eca2f
	{
		public Vector3F cf2e6e952fbc0f199d7bf1b90ba27a6c4;

		public int c1eb951f3760e81021c2a54e06f304fef;

		public float c4be90e89fe880ace69ff02dc7bc1ee11;

		public int cd95fe24a51d1343917f4177daca707ea;

		public static int ca71a451bb5f70e92e8bebb9bb71cfb56(cc467e46599906a5d357036985a0eca2f cbe6829ca974177ec6282270ed6d4e5c9, cc467e46599906a5d357036985a0eca2f cc1f9d7339a612e6210b583889d64d189)
		{
			if (cbe6829ca974177ec6282270ed6d4e5c9.c4be90e89fe880ace69ff02dc7bc1ee11 < cc1f9d7339a612e6210b583889d64d189.c4be90e89fe880ace69ff02dc7bc1ee11)
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
							__ldtoken(cc467e46599906a5d357036985a0eca2f.ca71a451bb5f70e92e8bebb9bb71cfb56(cc467e46599906a5d357036985a0eca2f, cc467e46599906a5d357036985a0eca2f));
						}
						return -1;
					}
				}
			}
			if (cbe6829ca974177ec6282270ed6d4e5c9.c4be90e89fe880ace69ff02dc7bc1ee11 > cc1f9d7339a612e6210b583889d64d189.c4be90e89fe880ace69ff02dc7bc1ee11)
			{
				return 1;
			}
			return 0;
		}

		public static int c2aa25107eaee0b8064508925dd8d300b(cc467e46599906a5d357036985a0eca2f cbe6829ca974177ec6282270ed6d4e5c9, cc467e46599906a5d357036985a0eca2f cc1f9d7339a612e6210b583889d64d189)
		{
			if (cbe6829ca974177ec6282270ed6d4e5c9.c1eb951f3760e81021c2a54e06f304fef < cc1f9d7339a612e6210b583889d64d189.c1eb951f3760e81021c2a54e06f304fef)
			{
				return -1;
			}
			if (cbe6829ca974177ec6282270ed6d4e5c9.c1eb951f3760e81021c2a54e06f304fef > cc1f9d7339a612e6210b583889d64d189.c1eb951f3760e81021c2a54e06f304fef)
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
							__ldtoken(cc467e46599906a5d357036985a0eca2f.c2aa25107eaee0b8064508925dd8d300b(cc467e46599906a5d357036985a0eca2f, cc467e46599906a5d357036985a0eca2f));
						}
						return 1;
					}
				}
			}
			return 0;
		}
	}

	/// <summary>
	///       Gets the number of triangles.
	///       </summary>
	public int NumberOfTriangles
	{
		get
		{
			if (Indices == null)
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
							__ldtoken(TriangleMesh.get_NumberOfTriangles());
						}
						return 0;
					}
				}
			}
			return Indices.Count / 3;
		}
	}

	/// <summary>
	///       Gets or sets the vertices.
	///       </summary>
	public List<Vector3F> Vertices { get; set; }

	/// <summary>
	///       Gets or sets the indices.
	///       </summary>
	public List<int> Indices { get; set; }

	/// <summary>
	///       Gets or sets custom information.
	///       </summary>
	public object Tag { get; set; }

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Meshes.TriangleMesh" /> class.
	///       </summary>
	public TriangleMesh()
	{
		Vertices = new List<Vector3F>();
		Indices = new List<int>();
	}

	private object c48423685eed44c8f6f4708513cf05f2b()
	{
		return Clone();
	}

	object ICloneable.Clone()
	{
		//ILSpy generated this explicit interface implementation from .override directive in c48423685eed44c8f6f4708513cf05f2b
		return this.c48423685eed44c8f6f4708513cf05f2b();
	}

	/// <summary>
	///       Creates a new <see cref="T:DigitalRune.Geometry.Meshes.TriangleMesh" /> that is a copy of the current instance.
	///       </summary>
	/// <returns>
	///       A new <see cref="T:DigitalRune.Geometry.Meshes.TriangleMesh" /> that is a copy of the current instance.
	///       </returns>
	public TriangleMesh Clone()
	{
		TriangleMesh triangleMesh = new TriangleMesh();
		if (Indices != null)
		{
			using List<int>.Enumerator enumerator = Indices.GetEnumerator();
			while (enumerator.MoveNext())
			{
				int current = enumerator.Current;
				triangleMesh.Indices.Add(current);
			}
			while (true)
			{
				switch (2)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(TriangleMesh.Clone());
				}
				break;
			}
		}
		if (Vertices != null)
		{
			foreach (Vector3F vertex in Vertices)
			{
				triangleMesh.Vertices.Add(vertex);
			}
		}
		if (Tag != null)
		{
			if (Tag is ICloneable)
			{
				triangleMesh.Tag = ((ICloneable)Tag).Clone();
			}
			else
			{
				triangleMesh.Tag = Tag;
			}
		}
		return triangleMesh;
	}

	/// <summary>
	///       Adds the triangles of the specified mesh (without vertex welding).
	///       </summary>
	/// <param name="mesh">The mesh.</param>
	public void Add(ITriangleMesh mesh)
	{
		Add(mesh, weldVerticesBruteForce: false);
	}

	/// <summary>
	///       Adds the triangles of the specified mesh.
	///       </summary>
	/// <param name="mesh">The mesh.</param>
	/// <param name="weldVerticesBruteForce">
	///       If set to <see langword="true" />, vertex welding is performed. A brute-force method is used
	///       for welding which can be very slow for large triangle meshes. For large meshes it is 
	///       recommended to call <see cref="M:DigitalRune.Geometry.Meshes.TriangleMesh.WeldVertices" /> after all triangles have been added.
	///       </param>
	public void Add(ITriangleMesh mesh, bool weldVerticesBruteForce)
	{
		if (mesh is TriangleMesh triangleMesh)
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
				__ldtoken(TriangleMesh.Add(ITriangleMesh, bool));
			}
			if (!weldVerticesBruteForce)
			{
				if (triangleMesh.Vertices == null)
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
				if (triangleMesh.Indices == null)
				{
					return;
				}
				if (Vertices == null)
				{
					Vertices = new List<Vector3F>(triangleMesh.Vertices.Count);
				}
				int count = triangleMesh.Indices.Count;
				if (Indices == null)
				{
					Indices = new List<int>(count);
				}
				int count2 = Vertices.Count;
				Vertices.AddRange(triangleMesh.Vertices);
				for (int i = 0; i < count; i++)
				{
					Indices.Add(triangleMesh.Indices[i] + count2);
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
		int numberOfTriangles = mesh.NumberOfTriangles;
		for (int j = 0; j < numberOfTriangles; j++)
		{
			Add(mesh.GetTriangle(j), weldVerticesBruteForce);
		}
	}

	/// <summary>
	///       Adds the triangle (without vertex welding).
	///       </summary>
	/// <param name="triangle">The triangle.</param>
	public void Add(Triangle triangle)
	{
		Add(triangle, weldVerticesBruteForce: false, Numeric.EpsilonF, removeDegenerateTriangles: false);
	}

	/// <summary>
	///       Adds the triangle.
	///       </summary>
	/// <param name="triangle">The triangle.</param>
	/// <param name="weldVerticesBruteForce">
	///       If set to <see langword="true" />, vertex welding is performed. A brute-force method is used
	///       for welding which can be very slow for large triangle meshes. For large meshes it is 
	///       recommended to call <see cref="M:DigitalRune.Geometry.Meshes.TriangleMesh.WeldVertices" /> after all triangles have been added.
	///       </param>
	public void Add(Triangle triangle, bool weldVerticesBruteForce)
	{
		Add(triangle, weldVerticesBruteForce, Numeric.EpsilonF, removeDegenerateTriangles: false);
	}

	/// <summary>
	///       Adds the triangle.
	///       </summary>
	/// <param name="triangle">The triangle.</param>
	/// <param name="weldVerticesBruteForce">
	///       If set to <see langword="true" />, vertex welding is performed. A brute-force method is used
	///       for welding which can be very slow for large triangle meshes. For large meshes it is 
	///       recommended to call <see cref="M:DigitalRune.Geometry.Meshes.TriangleMesh.WeldVertices" /> after all triangles have been added.
	///       </param>
	/// <param name="vertexPositionTolerance">
	///       The vertex position tolerance. If the vertex positions are within this range, they are 
	///       treated as identical vertices. This value is used for vertex welding and to decide
	///       if a triangle is degenerate. 
	///       </param>
	/// <param name="removeDegenerateTriangles">
	///       If set to <see langword="true" /> degenerate triangles will not be added to the mesh.
	///       </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="vertexPositionTolerance" /> is negative or 0.
	///       </exception>
	public void Add(Triangle triangle, bool weldVerticesBruteForce, float vertexPositionTolerance, bool removeDegenerateTriangles)
	{
		if (vertexPositionTolerance <= 0f)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(10674), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(10721));
		}
		if (removeDegenerateTriangles)
		{
			if (Vector3F.AreNumericallyEqual(triangle.Vertex0, triangle.Vertex1, vertexPositionTolerance))
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
				break;
			}
			if (1 == 0)
			{
				__ldtoken(TriangleMesh.Add(Triangle, bool, float, bool));
			}
			if (Vector3F.AreNumericallyEqual(triangle.Vertex0, triangle.Vertex2, vertexPositionTolerance))
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
				break;
			}
			if (Vector3F.AreNumericallyEqual(triangle.Vertex1, triangle.Vertex2, vertexPositionTolerance))
			{
				return;
			}
		}
		if (Vertices == null)
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
			Vertices = new List<Vector3F>();
		}
		if (Indices == null)
		{
			Indices = new List<int>();
		}
		int num = Vertices.Count;
		if (!weldVerticesBruteForce)
		{
			Vertices.Add(triangle.Vertex0);
			Indices.Add(num);
			num++;
			Vertices.Add(triangle.Vertex1);
			Indices.Add(num);
			num++;
			Vertices.Add(triangle.Vertex2);
			Indices.Add(num);
			return;
		}
		for (int i = 0; i < 3; i++)
		{
			Vector3F vector3F = triangle[i];
			int num2 = num;
			for (int j = 0; j < num; j++)
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
				if (num2 != num)
				{
					break;
				}
				if (!Vector3F.AreNumericallyEqual(Vertices[j], vector3F, vertexPositionTolerance))
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
				num2 = j;
			}
			if (num2 == num)
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
				Vertices.Add(vector3F);
				num++;
			}
			Indices.Add(num2);
		}
	}

	/// <summary>
	///       Removes duplicate vertices.
	///       </summary>
	/// <returns>
	///       The number of removed vertices.
	///       </returns>
	public int WeldVertices()
	{
		return WeldVertices(Numeric.EpsilonF);
	}

	/// <summary>
	///       Removes duplicate vertices.
	///       </summary>
	/// <param name="vertexPositionTolerance">
	///       The vertex position tolerance. If the distance between two vertices is less than this value,
	///       the vertices are merged.
	///       </param>
	/// <returns>The number of removed vertices.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="vertexPositionTolerance" /> is negative or 0.
	///       </exception>
	public int WeldVertices(float vertexPositionTolerance)
	{
		if (Vertices != null)
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
			if (1 == 0)
			{
				__ldtoken(TriangleMesh.WeldVertices(float));
			}
			if (Indices != null)
			{
				if (vertexPositionTolerance <= 0f)
				{
					while (true)
					{
						switch (3)
						{
						case 0:
							break;
						default:
							throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(10674), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(10816));
						}
					}
				}
				int count = Vertices.Count;
				if (count <= 1)
				{
					while (true)
					{
						switch (6)
						{
						case 0:
							break;
						default:
							return 0;
						}
					}
				}
				cc467e46599906a5d357036985a0eca2f[] array = new cc467e46599906a5d357036985a0eca2f[count];
				for (int i = 0; i < count; i++)
				{
					Vector3F cf2e6e952fbc0f199d7bf1b90ba27a6c = Vertices[i];
					array[i] = new cc467e46599906a5d357036985a0eca2f
					{
						cf2e6e952fbc0f199d7bf1b90ba27a6c4 = cf2e6e952fbc0f199d7bf1b90ba27a6c,
						c1eb951f3760e81021c2a54e06f304fef = i,
						c4be90e89fe880ace69ff02dc7bc1ee11 = Math.Abs(cf2e6e952fbc0f199d7bf1b90ba27a6c.X) + Math.Abs(cf2e6e952fbc0f199d7bf1b90ba27a6c.Y) + Math.Abs(cf2e6e952fbc0f199d7bf1b90ba27a6c.Z),
						cd95fe24a51d1343917f4177daca707ea = -1
					};
				}
				while (true)
				{
					switch (7)
					{
					case 0:
						continue;
					}
					Array.Sort(array, cc467e46599906a5d357036985a0eca2f.ca71a451bb5f70e92e8bebb9bb71cfb56);
					int num = 0;
					for (int j = 0; j < count; j++)
					{
						cc467e46599906a5d357036985a0eca2f cc467e46599906a5d357036985a0eca2f2 = array[j];
						if (cc467e46599906a5d357036985a0eca2f2.cd95fe24a51d1343917f4177daca707ea >= 0)
						{
							continue;
						}
						int num2 = j + 1;
						while (true)
						{
							if (num2 < count)
							{
								cc467e46599906a5d357036985a0eca2f cc467e46599906a5d357036985a0eca2f3 = array[num2];
								if (cc467e46599906a5d357036985a0eca2f3.c4be90e89fe880ace69ff02dc7bc1ee11 - cc467e46599906a5d357036985a0eca2f2.c4be90e89fe880ace69ff02dc7bc1ee11 > 3f * vertexPositionTolerance)
								{
									break;
								}
								if (Vector3F.AreNumericallyEqual(cc467e46599906a5d357036985a0eca2f2.cf2e6e952fbc0f199d7bf1b90ba27a6c4, cc467e46599906a5d357036985a0eca2f3.cf2e6e952fbc0f199d7bf1b90ba27a6c4, vertexPositionTolerance))
								{
									num++;
									cc467e46599906a5d357036985a0eca2f3.cd95fe24a51d1343917f4177daca707ea = cc467e46599906a5d357036985a0eca2f2.c1eb951f3760e81021c2a54e06f304fef;
								}
								num2++;
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
							break;
						}
					}
					while (true)
					{
						switch (6)
						{
						case 0:
							continue;
						}
						if (num == 0)
						{
							return 0;
						}
						Array.Sort(array, cc467e46599906a5d357036985a0eca2f.c2aa25107eaee0b8064508925dd8d300b);
						Vertices.Clear();
						int[] array2 = new int[count];
						for (int k = 0; k < count; k++)
						{
							if (array[k].cd95fe24a51d1343917f4177daca707ea < 0)
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
								array2[k] = Vertices.Count;
								Vertices.Add(array[k].cf2e6e952fbc0f199d7bf1b90ba27a6c4);
							}
							else
							{
								array2[k] = -1;
							}
						}
						while (true)
						{
							switch (5)
							{
							case 0:
								continue;
							}
							for (int l = 0; l < count; l++)
							{
								if (array[l].cd95fe24a51d1343917f4177daca707ea < 0)
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
								array2[l] = array2[array[l].cd95fe24a51d1343917f4177daca707ea];
							}
							int count2 = Indices.Count;
							for (int m = 0; m < count2; m++)
							{
								Indices[m] = array2[Indices[m]];
							}
							while (true)
							{
								switch (5)
								{
								case 0:
									continue;
								}
								return num;
							}
						}
					}
				}
			}
		}
		return 0;
	}

	/// <summary>
	///       Gets the triangle with the given index.
	///       </summary>
	/// <param name="index">The index.</param>
	/// <returns>The triangle with the given index.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is out of range.
	///       </exception>
	/// <exception cref="T:DigitalRune.Geometry.GeometryException">
	///       Either <see cref="P:DigitalRune.Geometry.Meshes.TriangleMesh.Vertices" /> or <see cref="P:DigitalRune.Geometry.Meshes.TriangleMesh.Indices" /> is <see langword="null" />.
	///       </exception>
	public Triangle GetTriangle(int index)
	{
		if (Vertices == null || Indices == null)
		{
			throw new GeometryException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(10919));
		}
		if (index >= 0)
		{
			if (index < NumberOfTriangles * 3)
			{
				index *= 3;
				return new Triangle(Vertices[Indices[index]], Vertices[Indices[index + 1]], Vertices[Indices[index + 2]]);
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
			if (1 == 0)
			{
				__ldtoken(TriangleMesh.GetTriangle(int));
			}
		}
		throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(549), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(11032));
	}

	/// <summary>
	///       Transforms all vertices by the given matrix.
	///       </summary>
	/// <param name="matrix">The transformation matrix.</param>
	public void Transform(Matrix44F matrix)
	{
		if (Vertices != null)
		{
			int count = Vertices.Count;
			for (int i = 0; i < count; i++)
			{
				Vertices[i] = matrix.TransformPosition(Vertices[i]);
			}
		}
	}

	/// <summary>
	///       Changes the winding order of all triangles.
	///       </summary>
	public void ReverseWindingOrder()
	{
		for (int i = 0; i < NumberOfTriangles; i++)
		{
			int value = Indices[i * 3 + 1];
			Indices[i * 3 + 1] = Indices[i * 3 + 2];
			Indices[i * 3 + 2] = value;
		}
	}

	/// <summary>
	///       Computes the mesh normals.
	///       </summary>
	/// <param name="useWeightedAverage">
	///       If set to <see langword="true" /> the influence of each triangle normal is weighted by the 
	///       triangle area; otherwise, all triangle normals have the same weight.
	///       </param>
	/// <param name="angleLimit">
	///       The angle limit in radians. Normals are only merged if the angle between the triangle
	///       normals is equal to or less than the angle limit. Set this value to -1 to disable the angle
	///       limit. 
	///       </param>
	/// <returns>
	///       If no angle limit is used (angle limit is -1), an array with one normal per vertex is 
	///       returned. If an angle limit is used, an array with one normal per index is used.
	///       </returns>
	public Vector3F[] ComputeNormals(bool useWeightedAverage, float angleLimit)
	{
		if (Vertices != null)
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
				__ldtoken(TriangleMesh.ComputeNormals(bool, float));
			}
			if (Indices != null)
			{
				if (angleLimit < 0f)
				{
					while (true)
					{
						switch (3)
						{
						case 0:
							break;
						default:
							return cb85edd303d8997bff76d35e1a9e9cd94(useWeightedAverage);
						}
					}
				}
				return cbc31b23b267df2a12d4d598a0b121ebb(angleLimit, useWeightedAverage);
			}
		}
		return null;
	}

	private Vector3F[] cb85edd303d8997bff76d35e1a9e9cd94(bool c085f5dd9b04388874d36710213b32d00)
	{
		int count = Vertices.Count;
		int numberOfTriangles = NumberOfTriangles;
		Vector3F[] array = new Vector3F[count];
		for (int i = 0; i < numberOfTriangles; i++)
		{
			int num = Indices[i * 3];
			int num2 = Indices[i * 3 + 1];
			int num3 = Indices[i * 3 + 2];
			Vector3F vector3F = Vertices[num];
			Vector3F vector3F2 = Vertices[num2];
			Vector3F vector3F3 = Vertices[num3];
			Vector3F vector3F4 = Vector3F.Cross(vector3F2 - vector3F, vector3F3 - vector3F);
			if (!c085f5dd9b04388874d36710213b32d00)
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
					__ldtoken(TriangleMesh.cb85edd303d8997bff76d35e1a9e9cd94(bool));
				}
				if (!vector3F4.TryNormalize())
				{
					continue;
				}
			}
			array[num] += vector3F4;
			array[num2] += vector3F4;
			array[num3] += vector3F4;
		}
		for (int j = 0; j < count; j++)
		{
			Vector3F vector3F5 = array[j];
			if (vector3F5.TryNormalize())
			{
				array[j] = vector3F5;
				continue;
			}
			ref Vector3F reference = ref array[j];
			reference = Vector3F.UnitY;
		}
		return array;
	}

	private Vector3F[] cbc31b23b267df2a12d4d598a0b121ebb(float c20e9c2db5c70b7a6459c52a6024b7897, bool c085f5dd9b04388874d36710213b32d00)
	{
		int count = Vertices.Count;
		int numberOfTriangles = NumberOfTriangles;
		Vector4F[] array = new Vector4F[numberOfTriangles];
		List<Vector4F>[] array2 = new List<Vector4F>[count];
		for (int i = 0; i < count; i++)
		{
			array2[i] = new List<Vector4F>(6);
		}
		while (true)
		{
			switch (7)
			{
			case 0:
				continue;
			}
			if (1 == 0)
			{
				__ldtoken(TriangleMesh.cbc31b23b267df2a12d4d598a0b121ebb(float, bool));
			}
			for (int j = 0; j < numberOfTriangles; j++)
			{
				int num = Indices[j * 3];
				int num2 = Indices[j * 3 + 1];
				int num3 = Indices[j * 3 + 2];
				Vector3F vector3F = Vertices[num];
				Vector3F vector3F2 = Vertices[num2];
				Vector3F vector3F3 = Vertices[num3];
				Vector3F vector3F4 = Vector3F.Cross(vector3F2 - vector3F, vector3F3 - vector3F);
				float lengthSquared = vector3F4.LengthSquared;
				if (Numeric.IsZero(lengthSquared, Numeric.EpsilonFSquared))
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
				float num4 = (float)Math.Sqrt(lengthSquared);
				Vector4F vector4F = new Vector4F(vector3F4 / num4, num4);
				array[j] = vector4F;
				array2[num].Add(vector4F);
				array2[num2].Add(vector4F);
				array2[num3].Add(vector4F);
			}
			float num5 = (float)Math.Cos(c20e9c2db5c70b7a6459c52a6024b7897);
			Vector3F[] array3 = new Vector3F[numberOfTriangles * 3];
			for (int k = 0; k < numberOfTriangles; k++)
			{
				Vector4F vector4F2 = array[k];
				Vector3F xYZ = vector4F2.XYZ;
				if (vector4F2.W == 0f)
				{
					ref Vector3F reference = ref array3[k * 3];
					reference = Vector3F.UnitY;
					ref Vector3F reference2 = ref array3[k * 3 + 1];
					reference2 = Vector3F.UnitY;
					ref Vector3F reference3 = ref array3[k * 3 + 2];
					reference3 = Vector3F.UnitY;
					continue;
				}
				for (int l = 0; l < 3; l++)
				{
					int num6 = k * 3 + l;
					int num7 = Indices[num6];
					Vector3F vector3F5 = default;
					using (List<Vector4F>.Enumerator enumerator = array2[num7].GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Vector4F current = enumerator.Current;
							Vector3F xYZ2 = current.XYZ;
							float w = current.W;
							if (!(Vector3F.Dot(xYZ, xYZ2) >= num5))
							{
								continue;
							}
							if (c085f5dd9b04388874d36710213b32d00)
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
								vector3F5 += xYZ2 * w;
							}
							else
							{
								vector3F5 += xYZ2;
							}
						}
						while (true)
						{
							switch (2)
							{
							case 0:
								break;
							default:
								goto end_IL_0285;
							}
							continue;
							end_IL_0285:
							break;
						}
					}
					vector3F5.Normalize();
					array3[num6] = vector3F5;
				}
			}
			return array3;
		}
	}

	/// <summary>
	///       Creates a triangle mesh from an XNA <see cref="T:Microsoft.Xna.Framework.Graphics.Model" />. (Only available in the XNA-compatible build.)
	///       </summary>
	/// <param name="model">The XNA model.</param>
	/// <returns>
	///       This method is only available in XNA-compatible builds of DigitalRune.Geometry.dll.
	///       </returns>
	public static TriangleMesh FromModel(Model model)
	{
		TriangleMesh triangleMesh = new TriangleMesh();
		foreach (ModelMesh mesh in model.Meshes)
		{
			Matrix matrix = c5987ec826e2dfd92062b67f34689c5c0(mesh.ParentBone);
			using ModelMeshPartCollection.Enumerator enumerator2 = mesh.MeshParts.GetEnumerator();
			while (enumerator2.MoveNext())
			{
				ModelMeshPart current2 = enumerator2.Current;
				VertexDeclaration vertexDeclaration = current2.VertexBuffer.VertexDeclaration;
				VertexElement[] vertexElements = vertexDeclaration.GetVertexElements();
				VertexElement vertexElement = vertexElements.First((VertexElement c4496d1e28885071f753d5d8c6ed007b6) => c4496d1e28885071f753d5d8c6ed007b6.VertexElementUsage == VertexElementUsage.Position);
				if (vertexElement.VertexElementFormat != VertexElementFormat.Vector3)
				{
					throw new NotSupportedException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(11085));
				}
				Vector3[] array = new Vector3[current2.NumVertices];
				current2.VertexBuffer.GetData(current2.VertexOffset * vertexDeclaration.VertexStride + vertexElement.Offset, array, 0, current2.NumVertices, vertexDeclaration.VertexStride);
				for (int num = 0; num < array.Length; num++)
				{
					ref Vector3 reference = ref array[num];
					reference = Vector3.Transform(array[num], matrix);
				}
				int num2 = ((current2.IndexBuffer.IndexElementSize == IndexElementSize.SixteenBits) ? 2 : 4);
				if (num2 != 2)
				{
					while (true)
					{
						switch (5)
						{
						case 0:
							break;
						default:
							if (1 == 0)
							{
								__ldtoken(TriangleMesh.FromModel(Model));
							}
							throw new NotSupportedException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(11221));
						}
					}
				}
				short[] array2 = new short[current2.PrimitiveCount * 3];
				current2.IndexBuffer.GetData(current2.StartIndex * 2, array2, 0, current2.PrimitiveCount * 3);
				int count = triangleMesh.Vertices.Count;
				Vector3[] array3 = array;
				foreach (Vector3 vector in array3)
				{
					triangleMesh.Vertices.Add((Vector3F)vector);
				}
				for (int num4 = 0; num4 < current2.PrimitiveCount; num4++)
				{
					int item = array2[num4 * 3] + count;
					int item2 = array2[num4 * 3 + 1] + count;
					int item3 = array2[num4 * 3 + 2] + count;
					triangleMesh.Indices.Add(item);
					triangleMesh.Indices.Add(item3);
					triangleMesh.Indices.Add(item2);
				}
			}
			while (true)
			{
				switch (2)
				{
				case 0:
					break;
				default:
					goto end_IL_0257;
				}
				continue;
				end_IL_0257:
				break;
			}
		}
		return triangleMesh;
	}

	private static Matrix c5987ec826e2dfd92062b67f34689c5c0(ModelBone cb6efe6730f002c6bb606c5d503800d42)
	{
		if (cb6efe6730f002c6bb606c5d503800d42 == null)
		{
			return Matrix.Identity;
		}
		return cb6efe6730f002c6bb606c5d503800d42.Transform * c5987ec826e2dfd92062b67f34689c5c0(cb6efe6730f002c6bb606c5d503800d42.Parent);
	}

	[CompilerGenerated]
	private static bool c1e727118355174d71b9ae8a1ad8a4483(VertexElement c4496d1e28885071f753d5d8c6ed007b6)
	{
		return c4496d1e28885071f753d5d8c6ed007b6.VertexElementUsage == VertexElementUsage.Position;
	}
}
