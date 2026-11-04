using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using A;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Meshes;

/// <summary>
///       A mesh represented by a Doubly-Connected Edge List (DCEL).
///       </summary>
public class DcelMesh
{
	[CompilerGenerated]
	private sealed class ca7cac95670275e69eec0aac7599f9b0c
	{
		public int cd43c6f3133f0f2ab513e7cd8fc70d1cc;

		public bool c6ed08d87baa673367937701ae8c94518(DcelVertex c017396742a4443621f7602d4362206cd)
		{
			return c017396742a4443621f7602d4362206cd.Tag == cd43c6f3133f0f2ab513e7cd8fc70d1cc;
		}

		public bool c25b579b7c41ef3d53ee7b940fb8d868e(DcelEdge c58db163daa8ede8b1e0c6c619a868d58)
		{
			return c58db163daa8ede8b1e0c6c619a868d58.Tag == cd43c6f3133f0f2ab513e7cd8fc70d1cc;
		}

		public bool c5ff8c81687c4e7b6be8b6346c5af6949(DcelFace c0b89ebd3492c28b5af64c64c59f1627f)
		{
			return c0b89ebd3492c28b5af64c64c59f1627f.Tag == cd43c6f3133f0f2ab513e7cd8fc70d1cc;
		}
	}

	private bool c421cab13099813f33cdb5ce5b075b221 = true;

	private List<DcelEdge> c7893e0b992303cc211862f59e171df8c;

	private List<DcelFace> c58a8479b538d5e70ccdab0acab84c5b3;

	private List<DcelVertex> c045d147d00d3ef3c235dc7bc888c7464;

	[CompilerGenerated]
	private DcelVertex c8c28d6745b08976215fcd5ecf57cfe6c;

	[CompilerGenerated]
	private static Func<DcelFace, bool> c56450011eff7620c267636fce2acb8c4;

	[CompilerGenerated]
	private static Func<DcelEdge, bool> c73f073436b4c66eae523762159132e8d;

	[CompilerGenerated]
	private static Func<DcelFace, bool> c8dfeca5d3887b369bda6427cfb2884e2;

	[CompilerGenerated]
	private static Func<DcelEdge, bool> cd338e6ceee0d962a13e4d9184081f6a1;

	[CompilerGenerated]
	private static Func<DcelVertex, bool> c8a588e6b444b42cda78d231b5321c4e3;

	[CompilerGenerated]
	private static Func<DcelEdge, bool> c1459c5013a2ce52f5e51691d02f23b59;

	[CompilerGenerated]
	private static Func<DcelFace, bool> c7f3b3f1e9cf6d09064bbf5a07433eced;

	/// <summary>
	///       Gets or sets a value indicating whether this <see cref="T:DigitalRune.Geometry.Meshes.DcelMesh" /> is dirty.
	///       </summary>
	public bool Dirty
	{
		get
		{
			return c421cab13099813f33cdb5ce5b075b221;
		}
		set
		{
			c421cab13099813f33cdb5ce5b075b221 = value;
		}
	}

	/// <summary>
	///       Gets the edges (as a read-only list).
	///       </summary>
	public ReadOnlyCollection<DcelEdge> Edges
	{
		get
		{
			cddc0e99ba4cceafb96748578af2570d4();
			return c7893e0b992303cc211862f59e171df8c.AsReadOnly();
		}
	}

	/// <summary>
	///       Gets the faces (as a read-only list).
	///       </summary>
	public ReadOnlyCollection<DcelFace> Faces
	{
		get
		{
			cddc0e99ba4cceafb96748578af2570d4();
			return c58a8479b538d5e70ccdab0acab84c5b3.AsReadOnly();
		}
	}

	/// <summary>
	///       Gets or sets the vertex.
	///       </summary>
	public DcelVertex Vertex
	{
		[CompilerGenerated]
		get
		{
			return c8c28d6745b08976215fcd5ecf57cfe6c;
		}
		[CompilerGenerated]
		set
		{
			c8c28d6745b08976215fcd5ecf57cfe6c = value;
		}
	}

	/// <summary>
	///       Gets the vertices (as a read-only list).
	///       </summary>
	public ReadOnlyCollection<DcelVertex> Vertices
	{
		get
		{
			cddc0e99ba4cceafb96748578af2570d4();
			return c045d147d00d3ef3c235dc7bc888c7464.AsReadOnly();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Meshes.DcelMesh" /> class</summary>
	public DcelMesh()
	{
	}

	/// <summary>
	///       Gets a value indicating whether the tags of all components in the mesh are equal to the
	///       given tag value.
	///       </summary>
	/// <param name="tag">The reference tag value.</param>
	/// <returns>
	///   <see langword="true" /> if the tags of all components are equal to <paramref name="tag" />;
	///       otherwise, <see langword="false" />.
	///       </returns>
	public bool AreTagsEqualTo(int tag)
	{
		if (Vertices.All((DcelVertex c017396742a4443621f7602d4362206cd) => c017396742a4443621f7602d4362206cd.Tag == tag))
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
				__ldtoken(DcelMesh.AreTagsEqualTo(int));
			}
			if (Edges.All((DcelEdge c58db163daa8ede8b1e0c6c619a868d58) => c58db163daa8ede8b1e0c6c619a868d58.Tag == tag))
			{
				while (true)
				{
					switch (7)
					{
					case 0:
						break;
					default:
						return Faces.All((DcelFace c0b89ebd3492c28b5af64c64c59f1627f) => c0b89ebd3492c28b5af64c64c59f1627f.Tag == tag);
					}
				}
			}
		}
		return false;
	}

	/// <summary>
	///       Determines whether the specified point is contained in the mesh. (This method assumes that
	///       the mesh is a convex polyhedron.)
	///       </summary>
	/// <param name="point">The point.</param>
	/// <param name="epsilon">
	///       The epsilon tolerance. A point counts as "contained" if the distance to the mesh surface
	///       is less than this value. Use a small positive value, e.g. 0.001f, for numerical robustness.
	///       </param>
	/// <returns>
	///   <see langword="true" /> if the specified point is contained; otherwise, <see langword="false" />.
	///       (The result is undefined if the mesh is not a convex polyhedron.)
	///       </returns>
	public bool Contains(Vector3F point, float epsilon)
	{
		IEnumerator<DcelFace> enumerator = Faces.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				DcelFace current = enumerator.Current;
				Vector3F c92b2ed2c76c9d5b2e6f8466991b83c = current.c92b2ed2c76c9d5b2e6f8466991b83c86;
				float length = c92b2ed2c76c9d5b2e6f8466991b83c.Length;
				if (Numeric.IsZero(length))
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
				if (1 == 0)
				{
					__ldtoken(DcelMesh.Contains(Vector3F, float));
				}
				c92b2ed2c76c9d5b2e6f8466991b83c /= length;
				Plane plane = new Plane(c92b2ed2c76c9d5b2e6f8466991b83c, current.Boundary.Origin.Position);
				float num = Vector3F.Dot(point, plane.Normal) - plane.DistanceFromOrigin;
				if (num > epsilon * (1f + length + (point - current.Boundary.Origin.Position).Length))
				{
					return false;
				}
			}
		}
		finally
		{
			if (enumerator != null)
			{
				while (true)
				{
					switch (4)
					{
					case 0:
						continue;
					}
					enumerator.Dispose();
					break;
				}
			}
		}
		return true;
	}

	/// <summary>
	///       Determines whether this mesh is a closed mesh.
	///       </summary>
	/// <returns>
	///   <see langword="true" /> if this instance is a closed mesh; otherwise, <see langword="false" />.
	///       </returns>
	public bool IsClosed()
	{
		if (Vertex == null)
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
						__ldtoken(DcelMesh.IsClosed());
					}
					return false;
				}
			}
		}
		if (Faces.Count == 0)
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
			if (Edges.Count <= 2)
			{
				while (true)
				{
					switch (2)
					{
					case 0:
						break;
					default:
						return true;
					}
				}
			}
		}
		ReadOnlyCollection<DcelFace> faces = Faces;
		if (c56450011eff7620c267636fce2acb8c4 == null)
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
			c56450011eff7620c267636fce2acb8c4 = (DcelFace c4416e2ec407f91e8cc3086dd695bb8c5) => c4416e2ec407f91e8cc3086dd695bb8c5.Holes == null;
		}
		if (faces.All(c56450011eff7620c267636fce2acb8c4))
		{
			ReadOnlyCollection<DcelEdge> edges = Edges;
			if (c73f073436b4c66eae523762159132e8d == null)
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
				c73f073436b4c66eae523762159132e8d = (DcelEdge c4496d1e28885071f753d5d8c6ed007b6) =>
				{
					if (c4496d1e28885071f753d5d8c6ed007b6.Face != null)
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
						if (1 == 0)
						{
							__ldtoken(DcelMesh.c08a141fcfb76458077614b37378a4896(DcelEdge));
						}
						if (c4496d1e28885071f753d5d8c6ed007b6.Twin != null)
						{
							while (true)
							{
								switch (7)
								{
								case 0:
									break;
								default:
									return c4496d1e28885071f753d5d8c6ed007b6.Twin.Face != null;
								}
							}
						}
					}
					return false;
				};
			}
			return edges.All(c73f073436b4c66eae523762159132e8d);
		}
		return false;
	}

	/// <summary>
	///       Determines whether this mesh is a convex mesh.
	///       </summary>
	/// <returns>
	///   <see langword="true" /> if this mesh is a convex mesh; otherwise, <see langword="false" />.
	///       </returns>
	public bool IsConvex()
	{
		return IsConvex(0.001f);
	}

	/// <summary>
	///       Determines whether this mesh is a convex mesh using a specific tolerance.
	///       </summary>
	/// <param name="epsilon">
	///       The epsilon tolerance value. Numerical errors within this tolerance are accepted. This value 
	///       is automatically scaled with the mesh size. Recommended values are 0.001 or 0.0001. 
	///       </param>
	/// <returns>
	///   <see langword="true" /> if this mesh is a convex mesh; otherwise, <see langword="false" />.
	///       </returns>
	public bool IsConvex(float epsilon)
	{
		if (!IsClosed())
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
						__ldtoken(DcelMesh.IsConvex(float));
					}
					return false;
				}
			}
		}
		cddc0e99ba4cceafb96748578af2570d4();
		if (Faces.Count <= 2)
		{
			while (true)
			{
				switch (1)
				{
				case 0:
					break;
				default:
					return true;
				}
			}
		}
		try
		{
			IEnumerator<DcelFace> enumerator = Faces.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					DcelFace current = enumerator.Current;
					if (current.InternalTag == 1)
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
					current.InternalTag = 1;
					Vector3F c92b2ed2c76c9d5b2e6f8466991b83c = current.c92b2ed2c76c9d5b2e6f8466991b83c86;
					float length = c92b2ed2c76c9d5b2e6f8466991b83c.Length;
					if (Numeric.IsZero(length))
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
					c92b2ed2c76c9d5b2e6f8466991b83c /= length;
					Plane plane = new Plane(c92b2ed2c76c9d5b2e6f8466991b83c, current.Boundary.Origin.Position);
					DcelEdge dcelEdge = current.Boundary;
					dcelEdge.InternalTag = 1;
					do
					{
						DcelFace face = dcelEdge.Twin.Face;
						if (face.InternalTag != 1)
						{
							DcelEdge dcelEdge2 = face.Boundary;
							dcelEdge2.InternalTag = 1;
							do
							{
								float value = Vector3F.Dot(dcelEdge2.Origin.Position, plane.Normal) - plane.DistanceFromOrigin;
								if (Numeric.IsGreater(value, 0f, epsilon * (1f + length + (dcelEdge2.Origin.Position - dcelEdge.Origin.Position).Length)))
								{
									return false;
								}
								dcelEdge2 = dcelEdge2.Next;
							}
							while (dcelEdge2.InternalTag != 1);
						}
						dcelEdge = dcelEdge.Next;
					}
					while (dcelEdge != current.Boundary);
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
		}
		finally
		{
			c6f4c2e0978e773986f805e780946fe21();
		}
		return true;
	}

	/// <summary>
	///       Determines whether the mesh is a valid triangle mesh.
	///       </summary>
	/// <returns>
	///   <see langword="true" /> if the mesh is a valid triangle mesh; otherwise, 
	///       <see langword="false" />.
	///       </returns>
	public bool IsTriangleMesh()
	{
		if (Vertices.Count > 0)
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
			if (1 == 0)
			{
				__ldtoken(DcelMesh.IsTriangleMesh());
			}
			if (Faces.Count == 0)
			{
				return false;
			}
		}
		if (Faces.All((DcelFace c4416e2ec407f91e8cc3086dd695bb8c5) => c4416e2ec407f91e8cc3086dd695bb8c5.Holes == null))
		{
			while (true)
			{
				switch (4)
				{
				case 0:
					break;
				default:
				{
					ReadOnlyCollection<DcelEdge> edges = Edges;
					if (cd338e6ceee0d962a13e4d9184081f6a1 == null)
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
						cd338e6ceee0d962a13e4d9184081f6a1 = (DcelEdge c4496d1e28885071f753d5d8c6ed007b6) =>
						{
							if (c4496d1e28885071f753d5d8c6ed007b6.Next != null && c4496d1e28885071f753d5d8c6ed007b6.Next.Next == c4496d1e28885071f753d5d8c6ed007b6.Previous)
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
											__ldtoken(DcelMesh.cd28dbb98588f0534f4bada0bb26db36b(DcelEdge));
										}
										return c4496d1e28885071f753d5d8c6ed007b6.Next != c4496d1e28885071f753d5d8c6ed007b6.Previous;
									}
								}
							}
							return false;
						};
					}
					return edges.All(cd338e6ceee0d962a13e4d9184081f6a1);
				}
				}
			}
		}
		return false;
	}

	internal bool c9e173cd82e394258ca88b02f2c3f96b7()
	{
		if (Faces.Count == 2)
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
				__ldtoken(DcelMesh.c9e173cd82e394258ca88b02f2c3f96b7());
			}
			if (Edges.Count == Vertices.Count * 2)
			{
				return IsClosed();
			}
		}
		return false;
	}

	/// <summary>
	///       Determines whether this instance is a valid mesh.
	///       </summary>
	/// <returns>
	///   <see langword="true" /> if this instance is valid; otherwise, <see langword="false" />.
	///       </returns>
	public bool IsValid()
	{
		string errorDescription;
		return IsValid(out errorDescription);
	}

	/// <summary>
	///       Determines whether this instance is a valid mesh and returns an error description.
	///       </summary>
	/// <param name="errorDescription">
	///       The error description or <see langword="null" /> if the mesh is valid.
	///       </param>
	/// <returns>
	///   <see langword="true" /> if this instance is valid; otherwise, <see langword="false" />.
	///       </returns>
	public bool IsValid(out string errorDescription)
	{
		IEnumerator<DcelFace> enumerator = Faces.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				DcelFace current = enumerator.Current;
				if (!c8e71ef62104b5216a597dbb6b08779f3(current, out errorDescription))
				{
					return false;
				}
			}
		}
		finally
		{
			if (enumerator != null)
			{
				while (true)
				{
					switch (6)
					{
					case 0:
						continue;
					}
					if (1 == 0)
					{
						__ldtoken(DcelMesh.IsValid(ref string));
					}
					enumerator.Dispose();
					break;
				}
			}
		}
		foreach (DcelEdge edge in Edges)
		{
			if (edge.Twin == null)
			{
				while (true)
				{
					switch (4)
					{
					case 0:
						break;
					default:
						errorDescription = c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2870);
						return false;
					}
				}
			}
			if (edge.Twin.Twin != edge)
			{
				errorDescription = c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2915);
				return false;
			}
			if (edge.Twin.Face != null)
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
				if (edge.Twin.Face == edge.Face)
				{
					while (true)
					{
						switch (3)
						{
						case 0:
							break;
						default:
							errorDescription = c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2990);
							return false;
						}
					}
				}
			}
			if (edge.Twin.Next != null)
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
				if (edge.Twin.Next.Origin != edge.Origin)
				{
					errorDescription = c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3081);
					return false;
				}
			}
			if (edge.Next != null && edge.Next.Previous != edge)
			{
				while (true)
				{
					switch (7)
					{
					case 0:
						break;
					default:
						errorDescription = c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3186);
						return false;
					}
				}
			}
			if (edge.Origin != null)
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
				errorDescription = c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3267);
				return false;
			}
		}
		using (IEnumerator<DcelVertex> enumerator3 = Vertices.GetEnumerator())
		{
			while (enumerator3.MoveNext())
			{
				DcelVertex current3 = enumerator3.Current;
				if (current3 != Vertex)
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
					if (current3.Edge == null)
					{
						errorDescription = c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3308);
						return false;
					}
				}
				if (current3 == Vertex || current3.Edge.Origin == current3)
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
					errorDescription = c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3349);
					return false;
				}
			}
			while (true)
			{
				switch (2)
				{
				case 0:
					break;
				default:
					goto end_IL_0259;
				}
				continue;
				end_IL_0259:
				break;
			}
		}
		errorDescription = null;
		return true;
	}

	private bool c8e71ef62104b5216a597dbb6b08779f3(DcelFace c0b89ebd3492c28b5af64c64c59f1627f, out string c001cfcb60b05471b18ab1e40b01b1028)
	{
		DcelEdge boundary = c0b89ebd3492c28b5af64c64c59f1627f.Boundary;
		if (boundary == null)
		{
			c001cfcb60b05471b18ab1e40b01b1028 = c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3438);
			return false;
		}
		if (boundary.Face != c0b89ebd3492c28b5af64c64c59f1627f)
		{
			c001cfcb60b05471b18ab1e40b01b1028 = c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3483);
			return false;
		}
		if (boundary.Next == boundary)
		{
			c001cfcb60b05471b18ab1e40b01b1028 = c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3580);
			return false;
		}
		try
		{
			boundary.InternalTag = 1;
			DcelEdge next;
			for (next = boundary.Next; next != boundary; next = next.Next)
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
				if (next != null)
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
					if (next.InternalTag == 1)
					{
						break;
					}
				}
				if (next == null)
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
								__ldtoken(DcelMesh.c8e71ef62104b5216a597dbb6b08779f3(DcelFace, ref string));
							}
							c001cfcb60b05471b18ab1e40b01b1028 = c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3633);
							return false;
						}
					}
				}
				if (next.Face != c0b89ebd3492c28b5af64c64c59f1627f)
				{
					c001cfcb60b05471b18ab1e40b01b1028 = c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3483);
					return false;
				}
				next.InternalTag = 1;
			}
			if (next != boundary)
			{
				while (true)
				{
					switch (4)
					{
					case 0:
						break;
					default:
						c001cfcb60b05471b18ab1e40b01b1028 = c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3726);
						return false;
					}
				}
			}
		}
		finally
		{
			c6f4c2e0978e773986f805e780946fe21();
		}
		c001cfcb60b05471b18ab1e40b01b1028 = null;
		return true;
	}

	/// <summary>
	///       Converts the given <see cref="T:DigitalRune.Geometry.Meshes.ITriangleMesh" /> to a <see cref="T:DigitalRune.Geometry.Meshes.DcelMesh" />.
	///       </summary>
	/// <param name="mesh">The triangle mesh.</param>
	/// <returns>
	///       The <see cref="T:DigitalRune.Geometry.Meshes.DcelMesh" />.
	///       </returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="mesh" /> is <see langword="null" />.
	///       </exception>
	public static DcelMesh FromTriangleMesh(ITriangleMesh mesh)
	{
		if (mesh == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3787));
		}
		TriangleMesh triangleMesh = new TriangleMesh();
		triangleMesh.Add(mesh, weldVerticesBruteForce: false);
		triangleMesh.WeldVertices();
		return FromTriangleMesh(triangleMesh);
	}

	/// <summary>
	///       Converts the given <see cref="T:DigitalRune.Geometry.Meshes.ITriangleMesh" /> to a <see cref="T:DigitalRune.Geometry.Meshes.DcelMesh" />.
	///       </summary>
	/// <param name="mesh">The triangle mesh.</param>
	/// <returns>
	///       The <see cref="T:DigitalRune.Geometry.Meshes.DcelMesh" />.
	///       </returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="mesh" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="mesh" /> has no vertices or vertex indices.
	///       </exception>
	/// <exception cref="T:System.NotSupportedException">
	///   <paramref name="mesh" /> consists of several unconnected components or sub-meshes.
	///       </exception>
	public static DcelMesh FromTriangleMesh(TriangleMesh mesh)
	{
		if (mesh == null)
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
						__ldtoken(DcelMesh.FromTriangleMesh(TriangleMesh));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3787));
				}
			}
		}
		if (mesh.Vertices == null || mesh.Indices == null)
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3796));
		}
		int count = mesh.Vertices.Count;
		List<DcelVertex> list = new List<DcelVertex>(count);
		using (List<Vector3F>.Enumerator enumerator = mesh.Vertices.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				Vector3F current = enumerator.Current;
				list.Add(new DcelVertex(current, null));
			}
			while (true)
			{
				switch (4)
				{
				case 0:
					break;
				default:
					goto end_IL_0094;
				}
				continue;
				end_IL_0094:
				break;
			}
		}
		for (int i = 0; i < count; i++)
		{
			for (int j = i + 1; j < count; j++)
			{
				if (Vector3F.AreNumericallyEqual(list[i].Position, list[j].Position))
				{
					list[i] = list[j];
				}
			}
		}
		List<DcelEdge> list2 = new List<DcelEdge>(mesh.NumberOfTriangles * 3 * 2);
		List<DcelFace> list3 = new List<DcelFace>(mesh.NumberOfTriangles);
		for (int k = 0; k < mesh.NumberOfTriangles; k++)
		{
			int index = mesh.Indices[k * 3];
			int index2 = mesh.Indices[k * 3 + 1];
			int index3 = mesh.Indices[k * 3 + 2];
			DcelVertex dcelVertex = list[index];
			DcelVertex dcelVertex2 = list[index2];
			DcelVertex dcelVertex3 = list[index3];
			DcelEdge dcelEdge = new DcelEdge();
			DcelEdge dcelEdge2 = new DcelEdge();
			DcelEdge dcelEdge3 = new DcelEdge();
			DcelFace dcelFace = new DcelFace();
			dcelFace.Boundary = dcelEdge;
			dcelVertex.Edge = dcelEdge;
			dcelVertex2.Edge = dcelEdge2;
			dcelVertex3.Edge = dcelEdge3;
			dcelEdge.Face = dcelFace;
			dcelEdge.Origin = dcelVertex;
			dcelEdge.Next = dcelEdge2;
			dcelEdge.Previous = dcelEdge3;
			dcelEdge2.Face = dcelFace;
			dcelEdge2.Origin = dcelVertex2;
			dcelEdge2.Next = dcelEdge3;
			dcelEdge2.Previous = dcelEdge;
			dcelEdge3.Face = dcelFace;
			dcelEdge3.Origin = dcelVertex3;
			dcelEdge3.Next = dcelEdge;
			dcelEdge3.Previous = dcelEdge2;
			list2.Add(dcelEdge);
			list2.Add(dcelEdge2);
			list2.Add(dcelEdge3);
			list3.Add(dcelFace);
		}
		while (true)
		{
			switch (6)
			{
			case 0:
				continue;
			}
			for (int l = 0; l < list3.Count; l++)
			{
				DcelFace dcelFace2 = list3[l];
				DcelEdge boundary = dcelFace2.Boundary;
				DcelEdge next = boundary.Next;
				DcelEdge next2 = next.Next;
				for (int m = l + 1; m < list3.Count; m++)
				{
					DcelFace dcelFace3 = list3[m];
					DcelEdge boundary2 = dcelFace3.Boundary;
					DcelEdge next3 = boundary2.Next;
					DcelEdge next4 = next3.Next;
					c7908406ea2d1f414ba6c1f33e7b6e1cf(boundary, boundary2);
					c7908406ea2d1f414ba6c1f33e7b6e1cf(boundary, next3);
					c7908406ea2d1f414ba6c1f33e7b6e1cf(boundary, next4);
					c7908406ea2d1f414ba6c1f33e7b6e1cf(next, boundary2);
					c7908406ea2d1f414ba6c1f33e7b6e1cf(next, next3);
					c7908406ea2d1f414ba6c1f33e7b6e1cf(next, next4);
					c7908406ea2d1f414ba6c1f33e7b6e1cf(next2, boundary2);
					c7908406ea2d1f414ba6c1f33e7b6e1cf(next2, next3);
					c7908406ea2d1f414ba6c1f33e7b6e1cf(next2, next4);
				}
				while (true)
				{
					switch (5)
					{
					case 0:
						break;
					default:
						goto end_IL_0345;
					}
					continue;
					end_IL_0345:
					break;
				}
			}
			while (true)
			{
				switch (4)
				{
				case 0:
					continue;
				}
				DcelEdge[] array = list2.ToArray();
				foreach (DcelEdge dcelEdge4 in array)
				{
					if (dcelEdge4.Twin != null)
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
					DcelEdge dcelEdge5 = new DcelEdge();
					dcelEdge5.Origin = dcelEdge4.Next.Origin;
					dcelEdge5.Twin = dcelEdge4;
					dcelEdge4.Twin = dcelEdge5;
					list2.Add(dcelEdge5);
				}
				foreach (DcelEdge item in list2)
				{
					if (item.Previous != null)
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
					_ = item.Origin;
					DcelEdge next5 = item.Twin.Next;
					while (next5.Twin.Next != null)
					{
						next5 = next5.Twin.Next;
					}
					DcelEdge twin = next5.Twin;
					twin.Next = item;
					item.Previous = twin;
				}
				if (list.Count > 0)
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
					c6e426ad7f89865844dbe4dc8babf26af(list[0], 1);
					if (!list.Any((DcelVertex caa9d7ab80719ca5895d6816e6deee0d6) => caa9d7ab80719ca5895d6816e6deee0d6.Tag != 1))
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
						if (c1459c5013a2ce52f5e51691d02f23b59 == null)
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
							c1459c5013a2ce52f5e51691d02f23b59 = (DcelEdge c4496d1e28885071f753d5d8c6ed007b6) => c4496d1e28885071f753d5d8c6ed007b6.Tag != 1;
						}
						if (!list2.Any(c1459c5013a2ce52f5e51691d02f23b59))
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
							if (c7f3b3f1e9cf6d09064bbf5a07433eced == null)
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
								c7f3b3f1e9cf6d09064bbf5a07433eced = (DcelFace c4416e2ec407f91e8cc3086dd695bb8c5) => c4416e2ec407f91e8cc3086dd695bb8c5.Tag != 1;
							}
							if (!list3.Any(c7f3b3f1e9cf6d09064bbf5a07433eced))
							{
								goto IL_0558;
							}
						}
					}
					throw new NotSupportedException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3887));
				}
				goto IL_0558;
				IL_0558:
				DcelMesh dcelMesh = new DcelMesh();
				dcelMesh.Vertex = list.FirstOrDefault();
				DcelMesh dcelMesh2 = dcelMesh;
				dcelMesh2.ResetTags();
				return dcelMesh2;
			}
		}
	}

	private static void c7908406ea2d1f414ba6c1f33e7b6e1cf(DcelEdge c0d5aa50012d63e3b953c40c74a77a847, DcelEdge ce3b4455f98c1c5b76a65600c92b5e649)
	{
		DcelVertex origin = c0d5aa50012d63e3b953c40c74a77a847.Origin;
		DcelVertex origin2 = c0d5aa50012d63e3b953c40c74a77a847.Next.Origin;
		DcelVertex origin3 = ce3b4455f98c1c5b76a65600c92b5e649.Origin;
		DcelVertex origin4 = ce3b4455f98c1c5b76a65600c92b5e649.Next.Origin;
		if (origin != origin4)
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
			if (1 == 0)
			{
				__ldtoken(DcelMesh.c7908406ea2d1f414ba6c1f33e7b6e1cf(DcelEdge, DcelEdge));
			}
			if (origin2 == origin3)
			{
				c0d5aa50012d63e3b953c40c74a77a847.Twin = ce3b4455f98c1c5b76a65600c92b5e649;
				ce3b4455f98c1c5b76a65600c92b5e649.Twin = c0d5aa50012d63e3b953c40c74a77a847;
			}
			return;
		}
	}

	/// <summary>
	///       Converts this mesh to a <see cref="T:DigitalRune.Geometry.Meshes.TriangleMesh" />.
	///       </summary>
	/// <returns>A triangle mesh that represents the same mesh.</returns>
	/// <exception cref="T:DigitalRune.Geometry.GeometryException">The DCEL mesh is invalid.</exception>
	public TriangleMesh ToTriangleMesh()
	{
		TriangleMesh triangleMesh = new TriangleMesh();
		cddc0e99ba4cceafb96748578af2570d4();
		int count = Vertices.Count;
		int count2 = Faces.Count;
		for (int i = 0; i < count; i++)
		{
			DcelVertex dcelVertex = Vertices[i];
			dcelVertex.InternalTag = i;
			triangleMesh.Vertices.Add(dcelVertex.Position);
		}
		for (int j = 0; j < count2; j++)
		{
			DcelFace dcelFace = Faces[j];
			DcelEdge boundary = dcelFace.Boundary;
			DcelEdge next = boundary.Next;
			boundary.InternalTag = 1;
			next.InternalTag = 1;
			DcelVertex origin = boundary.Origin;
			DcelVertex dcelVertex2 = next.Origin;
			next = next.Next;
			while (next != boundary)
			{
				if (next != null)
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
					if (1 == 0)
					{
						__ldtoken(DcelMesh.ToTriangleMesh());
					}
					if (next.InternalTag != 1)
					{
						next.InternalTag = 1;
						DcelVertex origin2 = next.Origin;
						triangleMesh.Indices.Add(origin.InternalTag);
						triangleMesh.Indices.Add(dcelVertex2.InternalTag);
						triangleMesh.Indices.Add(origin2.InternalTag);
						dcelVertex2 = origin2;
						next = next.Next;
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
				throw new GeometryException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4039));
			}
			while (true)
			{
				switch (2)
				{
				case 0:
					break;
				default:
					goto end_IL_015d;
				}
				continue;
				end_IL_015d:
				break;
			}
		}
		while (true)
		{
			switch (4)
			{
			case 0:
				continue;
			}
			c6f4c2e0978e773986f805e780946fe21();
			return triangleMesh;
		}
	}

	internal static c3bf6880584bac8647ae5b3c0b6ef826a cf8c8fb520854eb9b380072d6514b548f(Vector3F cfef46e983928cc4e6880c424d1468b93, DcelEdge c58db163daa8ede8b1e0c6c619a868d58)
	{
		Vector3F position = c58db163daa8ede8b1e0c6c619a868d58.Origin.Position;
		Vector3F position2 = c58db163daa8ede8b1e0c6c619a868d58.Twin.Origin.Position;
		Vector3F vector3F = position2 - position;
		float lengthSquared = vector3F.LengthSquared;
		Vector3F vector3F2 = cfef46e983928cc4e6880c424d1468b93 - position;
		float num = Vector3F.Dot(vector3F2, vector3F);
		if ((vector3F2 - num / lengthSquared * vector3F).LengthSquared > Numeric.EpsilonF * (1f + lengthSquared))
		{
			return c3bf6880584bac8647ae5b3c0b6ef826a.c68d54e3c96af7b3d61e452cc6d786698;
		}
		if (num < (0f - Numeric.EpsilonF) * lengthSquared)
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
						__ldtoken(DcelMesh.cf8c8fb520854eb9b380072d6514b548f(Vector3F, DcelEdge));
					}
					return c3bf6880584bac8647ae5b3c0b6ef826a.c0277d06f7b510805fdeb5fcc22f3e783;
				}
			}
		}
		if (num > (1f + Numeric.EpsilonF) * lengthSquared)
		{
			return c3bf6880584bac8647ae5b3c0b6ef826a.c80b2e4b967d0b0bceebb2e229d32e970;
		}
		return c3bf6880584bac8647ae5b3c0b6ef826a.c0daadf83b0eef7c5bbddd30f172eeeb3;
	}

	internal static c3bf6880584bac8647ae5b3c0b6ef826a cf8c8fb520854eb9b380072d6514b548f(Vector3F cfef46e983928cc4e6880c424d1468b93, DcelFace c0b89ebd3492c28b5af64c64c59f1627f)
	{
		float caf24d26da4b746e8b133be35f8167c;
		return cf8c8fb520854eb9b380072d6514b548f(cfef46e983928cc4e6880c424d1468b93, c0b89ebd3492c28b5af64c64c59f1627f, out caf24d26da4b746e8b133be35f8167c);
	}

	internal static c3bf6880584bac8647ae5b3c0b6ef826a cf8c8fb520854eb9b380072d6514b548f(Vector3F cfef46e983928cc4e6880c424d1468b93, DcelFace c0b89ebd3492c28b5af64c64c59f1627f, out float caf24d26da4b746e8b133be35f8167c77)
	{
		Vector3F position = c0b89ebd3492c28b5af64c64c59f1627f.Boundary.Origin.Position;
		Vector3F c92b2ed2c76c9d5b2e6f8466991b83c = c0b89ebd3492c28b5af64c64c59f1627f.c92b2ed2c76c9d5b2e6f8466991b83c86;
		Vector3F vector = cfef46e983928cc4e6880c424d1468b93 - position;
		float num = (caf24d26da4b746e8b133be35f8167c77 = Vector3F.Dot(vector, c92b2ed2c76c9d5b2e6f8466991b83c));
		if (num * num > Numeric.EpsilonF * c92b2ed2c76c9d5b2e6f8466991b83c.LengthSquared * vector.LengthSquared)
		{
			if (num > 0f)
			{
				return c3bf6880584bac8647ae5b3c0b6ef826a.cdbbdc5140026b87b9842a38afa9c326c;
			}
			if (num < 0f)
			{
				while (true)
				{
					switch (4)
					{
					case 0:
						break;
					default:
						if (1 == 0)
						{
							__ldtoken(DcelMesh.cf8c8fb520854eb9b380072d6514b548f(Vector3F, DcelFace, ref float));
						}
						return c3bf6880584bac8647ae5b3c0b6ef826a.ccc99115cbab7b1097821c06452ce7c72;
					}
				}
			}
		}
		return c3bf6880584bac8647ae5b3c0b6ef826a.cc8c5a3df3e75781aa127c9f3b880f875;
	}

	internal static c3bf6880584bac8647ae5b3c0b6ef826a cf8c8fb520854eb9b380072d6514b548f(Vector3F cfef46e983928cc4e6880c424d1468b93, DcelEdge c58db163daa8ede8b1e0c6c619a868d58, Vector3F cbc85e5134c93a5ab94922ecd0cf4f915)
	{
		Vector3F position = c58db163daa8ede8b1e0c6c619a868d58.Origin.Position;
		Vector3F position2 = c58db163daa8ede8b1e0c6c619a868d58.Twin.Origin.Position;
		Vector3F vector = position2 - position;
		float lengthSquared = vector.LengthSquared;
		Vector3F vector2 = Vector3F.Cross(vector, cbc85e5134c93a5ab94922ecd0cf4f915);
		float lengthSquared2 = vector2.LengthSquared;
		Vector3F vector3F = cfef46e983928cc4e6880c424d1468b93 - position;
		float lengthSquared3 = vector3F.LengthSquared;
		float num = Vector3F.Dot(vector3F, vector2);
		if (num * num > Numeric.EpsilonF * lengthSquared2 * lengthSquared3)
		{
			if (num > 0f)
			{
				while (true)
				{
					switch (5)
					{
					case 0:
						continue;
					}
					if (1 == 0)
					{
						__ldtoken(DcelMesh.cf8c8fb520854eb9b380072d6514b548f(Vector3F, DcelEdge, Vector3F));
					}
					return c3bf6880584bac8647ae5b3c0b6ef826a.cdbbdc5140026b87b9842a38afa9c326c;
				}
			}
			if (num < 0f)
			{
				return c3bf6880584bac8647ae5b3c0b6ef826a.ccc99115cbab7b1097821c06452ce7c72;
			}
		}
		num = Vector3F.Dot(vector, vector3F);
		if (num < (0f - Numeric.EpsilonF) * lengthSquared)
		{
			while (true)
			{
				switch (3)
				{
				case 0:
					break;
				default:
					return c3bf6880584bac8647ae5b3c0b6ef826a.c0277d06f7b510805fdeb5fcc22f3e783;
				}
			}
		}
		if (num > (1f + Numeric.EpsilonF) * lengthSquared)
		{
			while (true)
			{
				switch (2)
				{
				case 0:
					break;
				default:
					return c3bf6880584bac8647ae5b3c0b6ef826a.c80b2e4b967d0b0bceebb2e229d32e970;
				}
			}
		}
		return c3bf6880584bac8647ae5b3c0b6ef826a.c0daadf83b0eef7c5bbddd30f172eeeb3;
	}

	private static void cb821e82336fab7b24992f89fc2294205<c96c92d0bb3e5901860b75aee31593453>(ref List<c96c92d0bb3e5901860b75aee31593453> c9cc70b23e19e1a842fac3e03e9b3105f)
	{
		if (c9cc70b23e19e1a842fac3e03e9b3105f == null)
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
						__ldtoken(DcelMesh.cb821e82336fab7b24992f89fc2294205<c96c92d0bb3e5901860b75aee31593453>(ref List<c96c92d0bb3e5901860b75aee31593453>));
					}
					c9cc70b23e19e1a842fac3e03e9b3105f = new List<c96c92d0bb3e5901860b75aee31593453>();
					return;
				}
			}
		}
		c9cc70b23e19e1a842fac3e03e9b3105f.Clear();
	}

	private void cddc0e99ba4cceafb96748578af2570d4()
	{
		if (!Dirty)
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
				__ldtoken(DcelMesh.cddc0e99ba4cceafb96748578af2570d4());
			}
			if (c045d147d00d3ef3c235dc7bc888c7464 != null && c7893e0b992303cc211862f59e171df8c != null)
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
				if (c58a8479b538d5e70ccdab0acab84c5b3 != null)
				{
					return;
				}
			}
		}
		Dirty = false;
		cb821e82336fab7b24992f89fc2294205(ref c045d147d00d3ef3c235dc7bc888c7464);
		cb821e82336fab7b24992f89fc2294205(ref c7893e0b992303cc211862f59e171df8c);
		cb821e82336fab7b24992f89fc2294205(ref c58a8479b538d5e70ccdab0acab84c5b3);
		if (Vertex == null)
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
			c045d147d00d3ef3c235dc7bc888c7464.Add(Vertex);
			Vertex.InternalTag = 1;
			cb0c7ae4ecf198040cfc4d848a260187d(Vertex.Edge);
			return;
		}
	}

	private void cb0c7ae4ecf198040cfc4d848a260187d(DcelEdge c58db163daa8ede8b1e0c6c619a868d58)
	{
		if (c58db163daa8ede8b1e0c6c619a868d58 == null)
		{
			while (true)
			{
				switch (4)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(DcelMesh.cb0c7ae4ecf198040cfc4d848a260187d(DcelEdge));
					}
					return;
				}
			}
		}
		Stack<DcelEdge> stack = new Stack<DcelEdge>();
		stack.Push(c58db163daa8ede8b1e0c6c619a868d58);
		while (stack.Count > 0)
		{
			c58db163daa8ede8b1e0c6c619a868d58 = stack.Pop();
			if (c58db163daa8ede8b1e0c6c619a868d58.InternalTag == 1)
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
			c58db163daa8ede8b1e0c6c619a868d58.InternalTag = 1;
			c7893e0b992303cc211862f59e171df8c.Add(c58db163daa8ede8b1e0c6c619a868d58);
			if (c58db163daa8ede8b1e0c6c619a868d58.Origin != null)
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
				if (c58db163daa8ede8b1e0c6c619a868d58.Origin.InternalTag != 1)
				{
					c58db163daa8ede8b1e0c6c619a868d58.Origin.InternalTag = 1;
					c045d147d00d3ef3c235dc7bc888c7464.Add(c58db163daa8ede8b1e0c6c619a868d58.Origin);
				}
			}
			if (c58db163daa8ede8b1e0c6c619a868d58.Face != null)
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
				if (c58db163daa8ede8b1e0c6c619a868d58.Face.InternalTag != 1)
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
					c58db163daa8ede8b1e0c6c619a868d58.Face.InternalTag = 1;
					c58a8479b538d5e70ccdab0acab84c5b3.Add(c58db163daa8ede8b1e0c6c619a868d58.Face);
					c2ac70623a32c8e1a5b576d05c6138ad3(c58db163daa8ede8b1e0c6c619a868d58.Face.Boundary, stack, 1);
					if (c58db163daa8ede8b1e0c6c619a868d58.Face.Holes != null)
					{
						for (int i = 0; i < c58db163daa8ede8b1e0c6c619a868d58.Face.Holes.Count; i++)
						{
							c2ac70623a32c8e1a5b576d05c6138ad3(c58db163daa8ede8b1e0c6c619a868d58.Face.Holes[i], stack, 1);
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
			}
			c2ac70623a32c8e1a5b576d05c6138ad3(c58db163daa8ede8b1e0c6c619a868d58.Next, stack, 1);
			c2ac70623a32c8e1a5b576d05c6138ad3(c58db163daa8ede8b1e0c6c619a868d58.Previous, stack, 1);
			c2ac70623a32c8e1a5b576d05c6138ad3(c58db163daa8ede8b1e0c6c619a868d58.Twin, stack, 1);
		}
		while (true)
		{
			switch (1)
			{
			case 0:
				continue;
			}
			c6f4c2e0978e773986f805e780946fe21();
			return;
		}
	}

	/// <summary>
	///       Resets the tags in the DCEL data. (Tags are set to 0.)
	///       </summary>
	public void ResetTags()
	{
		cddc0e99ba4cceafb96748578af2570d4();
		int count = c045d147d00d3ef3c235dc7bc888c7464.Count;
		for (int i = 0; i < count; i++)
		{
			c045d147d00d3ef3c235dc7bc888c7464[i].Tag = 0;
		}
		while (true)
		{
			switch (4)
			{
			case 0:
				continue;
			}
			if (1 == 0)
			{
				__ldtoken(DcelMesh.ResetTags());
			}
			int count2 = c7893e0b992303cc211862f59e171df8c.Count;
			for (int j = 0; j < count2; j++)
			{
				c7893e0b992303cc211862f59e171df8c[j].Tag = 0;
			}
			while (true)
			{
				switch (7)
				{
				case 0:
					continue;
				}
				int count3 = c58a8479b538d5e70ccdab0acab84c5b3.Count;
				for (int k = 0; k < count3; k++)
				{
					c58a8479b538d5e70ccdab0acab84c5b3[k].Tag = 0;
				}
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
		}
	}

	private void c6f4c2e0978e773986f805e780946fe21()
	{
		int count = c045d147d00d3ef3c235dc7bc888c7464.Count;
		for (int i = 0; i < count; i++)
		{
			c045d147d00d3ef3c235dc7bc888c7464[i].InternalTag = 0;
		}
		while (true)
		{
			switch (5)
			{
			case 0:
				continue;
			}
			if (1 == 0)
			{
				__ldtoken(DcelMesh.c6f4c2e0978e773986f805e780946fe21());
			}
			int count2 = c7893e0b992303cc211862f59e171df8c.Count;
			for (int j = 0; j < count2; j++)
			{
				c7893e0b992303cc211862f59e171df8c[j].InternalTag = 0;
			}
			int count3 = c58a8479b538d5e70ccdab0acab84c5b3.Count;
			for (int k = 0; k < count3; k++)
			{
				c58a8479b538d5e70ccdab0acab84c5b3[k].InternalTag = 0;
			}
			return;
		}
	}

	/// <summary>
	///       Creates a mesh for unit cube.
	///       </summary>
	/// <returns>The DCEL mesh that represent a unit cube.</returns>
	public static DcelMesh CreateCube()
	{
		DcelVertex dcelVertex = new DcelVertex(new Vector3F(-1f, -1f, -1f), null);
		DcelVertex dcelVertex2 = new DcelVertex(new Vector3F(1f, -1f, -1f), null);
		DcelVertex dcelVertex3 = new DcelVertex(new Vector3F(-1f, 1f, -1f), null);
		DcelVertex dcelVertex4 = new DcelVertex(new Vector3F(1f, 1f, -1f), null);
		DcelVertex dcelVertex5 = new DcelVertex(new Vector3F(-1f, -1f, 1f), null);
		DcelVertex dcelVertex6 = new DcelVertex(new Vector3F(1f, -1f, 1f), null);
		DcelVertex dcelVertex7 = new DcelVertex(new Vector3F(-1f, 1f, 1f), null);
		DcelVertex dcelVertex8 = new DcelVertex(new Vector3F(1f, 1f, 1f), null);
		DcelFace dcelFace = new DcelFace();
		DcelFace dcelFace2 = new DcelFace();
		DcelFace dcelFace3 = new DcelFace();
		DcelFace dcelFace4 = new DcelFace();
		DcelFace dcelFace5 = new DcelFace();
		DcelFace dcelFace6 = new DcelFace();
		DcelEdge dcelEdge = new DcelEdge();
		dcelEdge.Origin = dcelVertex;
		dcelEdge.Face = dcelFace4;
		DcelEdge dcelEdge2 = dcelEdge;
		DcelEdge dcelEdge3 = new DcelEdge();
		dcelEdge3.Origin = dcelVertex2;
		dcelEdge3.Face = dcelFace2;
		DcelEdge dcelEdge4 = dcelEdge3;
		DcelEdge dcelEdge5 = new DcelEdge();
		dcelEdge5.Origin = dcelVertex2;
		dcelEdge5.Face = dcelFace6;
		DcelEdge dcelEdge6 = dcelEdge5;
		DcelEdge dcelEdge7 = new DcelEdge();
		dcelEdge7.Origin = dcelVertex4;
		dcelEdge7.Face = dcelFace2;
		DcelEdge dcelEdge8 = dcelEdge7;
		DcelEdge dcelEdge9 = new DcelEdge();
		dcelEdge9.Origin = dcelVertex3;
		dcelEdge9.Face = dcelFace2;
		DcelEdge dcelEdge10 = dcelEdge9;
		DcelEdge dcelEdge11 = new DcelEdge();
		dcelEdge11.Origin = dcelVertex4;
		dcelEdge11.Face = dcelFace3;
		DcelEdge dcelEdge12 = dcelEdge11;
		DcelEdge dcelEdge13 = new DcelEdge();
		dcelEdge13.Origin = dcelVertex;
		dcelEdge13.Face = dcelFace2;
		DcelEdge dcelEdge14 = dcelEdge13;
		DcelEdge dcelEdge15 = new DcelEdge();
		dcelEdge15.Origin = dcelVertex3;
		dcelEdge15.Face = dcelFace5;
		DcelEdge dcelEdge16 = dcelEdge15;
		DcelEdge dcelEdge17 = new DcelEdge();
		dcelEdge17.Origin = dcelVertex3;
		dcelEdge17.Face = dcelFace3;
		DcelEdge dcelEdge18 = dcelEdge17;
		DcelEdge dcelEdge19 = new DcelEdge();
		dcelEdge19.Origin = dcelVertex7;
		dcelEdge19.Face = dcelFace5;
		DcelEdge dcelEdge20 = dcelEdge19;
		DcelEdge dcelEdge21 = new DcelEdge();
		dcelEdge21.Origin = dcelVertex4;
		dcelEdge21.Face = dcelFace6;
		DcelEdge dcelEdge22 = dcelEdge21;
		DcelEdge dcelEdge23 = new DcelEdge();
		dcelEdge23.Origin = dcelVertex8;
		dcelEdge23.Face = dcelFace3;
		DcelEdge dcelEdge24 = dcelEdge23;
		DcelEdge dcelEdge25 = new DcelEdge();
		dcelEdge25.Origin = dcelVertex;
		dcelEdge25.Face = dcelFace5;
		DcelEdge dcelEdge26 = dcelEdge25;
		DcelEdge dcelEdge27 = new DcelEdge();
		dcelEdge27.Origin = dcelVertex5;
		dcelEdge27.Face = dcelFace4;
		DcelEdge dcelEdge28 = dcelEdge27;
		DcelEdge dcelEdge29 = new DcelEdge();
		dcelEdge29.Origin = dcelVertex2;
		dcelEdge29.Face = dcelFace4;
		DcelEdge dcelEdge30 = dcelEdge29;
		DcelEdge dcelEdge31 = new DcelEdge();
		dcelEdge31.Origin = dcelVertex6;
		dcelEdge31.Face = dcelFace6;
		DcelEdge dcelEdge32 = dcelEdge31;
		DcelEdge dcelEdge33 = new DcelEdge();
		dcelEdge33.Origin = dcelVertex5;
		dcelEdge33.Face = dcelFace;
		DcelEdge dcelEdge34 = dcelEdge33;
		DcelEdge dcelEdge35 = new DcelEdge();
		dcelEdge35.Origin = dcelVertex6;
		dcelEdge35.Face = dcelFace4;
		DcelEdge dcelEdge36 = dcelEdge35;
		DcelEdge dcelEdge37 = new DcelEdge();
		dcelEdge37.Origin = dcelVertex6;
		dcelEdge37.Face = dcelFace;
		DcelEdge dcelEdge38 = dcelEdge37;
		DcelEdge dcelEdge39 = new DcelEdge();
		dcelEdge39.Origin = dcelVertex8;
		dcelEdge39.Face = dcelFace6;
		DcelEdge dcelEdge40 = dcelEdge39;
		DcelEdge dcelEdge41 = new DcelEdge();
		dcelEdge41.Origin = dcelVertex5;
		dcelEdge41.Face = dcelFace5;
		DcelEdge dcelEdge42 = dcelEdge41;
		DcelEdge dcelEdge43 = new DcelEdge();
		dcelEdge43.Origin = dcelVertex7;
		dcelEdge43.Face = dcelFace;
		DcelEdge dcelEdge44 = dcelEdge43;
		DcelEdge dcelEdge45 = new DcelEdge();
		dcelEdge45.Origin = dcelVertex7;
		dcelEdge45.Face = dcelFace3;
		DcelEdge dcelEdge46 = dcelEdge45;
		DcelEdge dcelEdge47 = new DcelEdge();
		dcelEdge47.Origin = dcelVertex8;
		dcelEdge47.Face = dcelFace;
		DcelEdge dcelEdge48 = dcelEdge47;
		dcelVertex.Edge = dcelEdge2;
		dcelVertex2.Edge = dcelEdge4;
		dcelVertex3.Edge = dcelEdge16;
		dcelVertex4.Edge = dcelEdge8;
		dcelVertex5.Edge = dcelEdge28;
		dcelVertex6.Edge = dcelEdge38;
		dcelVertex7.Edge = dcelEdge46;
		dcelVertex8.Edge = dcelEdge48;
		dcelFace.Boundary = dcelEdge38;
		dcelFace2.Boundary = dcelEdge4;
		dcelFace5.Boundary = dcelEdge20;
		dcelFace6.Boundary = dcelEdge32;
		dcelFace3.Boundary = dcelEdge46;
		dcelFace4.Boundary = dcelEdge2;
		dcelEdge2.Twin = dcelEdge4;
		dcelEdge4.Twin = dcelEdge2;
		dcelEdge6.Twin = dcelEdge8;
		dcelEdge8.Twin = dcelEdge6;
		dcelEdge10.Twin = dcelEdge12;
		dcelEdge12.Twin = dcelEdge10;
		dcelEdge14.Twin = dcelEdge16;
		dcelEdge16.Twin = dcelEdge14;
		dcelEdge18.Twin = dcelEdge20;
		dcelEdge20.Twin = dcelEdge18;
		dcelEdge22.Twin = dcelEdge24;
		dcelEdge24.Twin = dcelEdge22;
		dcelEdge26.Twin = dcelEdge28;
		dcelEdge28.Twin = dcelEdge26;
		dcelEdge30.Twin = dcelEdge32;
		dcelEdge32.Twin = dcelEdge30;
		dcelEdge34.Twin = dcelEdge36;
		dcelEdge36.Twin = dcelEdge34;
		dcelEdge38.Twin = dcelEdge40;
		dcelEdge40.Twin = dcelEdge38;
		dcelEdge42.Twin = dcelEdge44;
		dcelEdge44.Twin = dcelEdge42;
		dcelEdge46.Twin = dcelEdge48;
		dcelEdge48.Twin = dcelEdge46;
		dcelEdge4.Next = dcelEdge14;
		dcelEdge14.Next = dcelEdge10;
		dcelEdge10.Next = dcelEdge8;
		dcelEdge8.Next = dcelEdge4;
		dcelEdge14.Previous = dcelEdge4;
		dcelEdge10.Previous = dcelEdge14;
		dcelEdge8.Previous = dcelEdge10;
		dcelEdge4.Previous = dcelEdge8;
		dcelEdge34.Next = dcelEdge38;
		dcelEdge38.Next = dcelEdge48;
		dcelEdge48.Next = dcelEdge44;
		dcelEdge44.Next = dcelEdge34;
		dcelEdge38.Previous = dcelEdge34;
		dcelEdge48.Previous = dcelEdge38;
		dcelEdge44.Previous = dcelEdge48;
		dcelEdge34.Previous = dcelEdge44;
		dcelEdge20.Next = dcelEdge16;
		dcelEdge16.Next = dcelEdge26;
		dcelEdge26.Next = dcelEdge42;
		dcelEdge42.Next = dcelEdge20;
		dcelEdge16.Previous = dcelEdge20;
		dcelEdge26.Previous = dcelEdge16;
		dcelEdge42.Previous = dcelEdge26;
		dcelEdge20.Previous = dcelEdge42;
		dcelEdge32.Next = dcelEdge6;
		dcelEdge6.Next = dcelEdge22;
		dcelEdge22.Next = dcelEdge40;
		dcelEdge40.Next = dcelEdge32;
		dcelEdge6.Previous = dcelEdge32;
		dcelEdge22.Previous = dcelEdge6;
		dcelEdge40.Previous = dcelEdge22;
		dcelEdge32.Previous = dcelEdge40;
		dcelEdge46.Next = dcelEdge24;
		dcelEdge24.Next = dcelEdge12;
		dcelEdge12.Next = dcelEdge18;
		dcelEdge18.Next = dcelEdge46;
		dcelEdge24.Previous = dcelEdge46;
		dcelEdge12.Previous = dcelEdge24;
		dcelEdge18.Previous = dcelEdge12;
		dcelEdge46.Previous = dcelEdge18;
		dcelEdge2.Next = dcelEdge30;
		dcelEdge30.Next = dcelEdge36;
		dcelEdge36.Next = dcelEdge28;
		dcelEdge28.Next = dcelEdge2;
		dcelEdge30.Previous = dcelEdge2;
		dcelEdge36.Previous = dcelEdge30;
		dcelEdge28.Previous = dcelEdge36;
		dcelEdge2.Previous = dcelEdge28;
		DcelMesh dcelMesh = new DcelMesh();
		dcelMesh.Vertex = dcelVertex;
		return dcelMesh;
	}

	/// <summary>
	///       Gets the axis-aligned bounding box of this mesh.
	///       </summary>
	/// <returns>
	///       The AABB of this mesh.
	///       </returns>
	public Aabb GetAabb()
	{
		if (Vertex == null)
		{
			return default;
		}
		Aabb result = new Aabb(Vertex.Position, Vertex.Position);
		IEnumerator<DcelVertex> enumerator = Vertices.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				DcelVertex current = enumerator.Current;
				result.Grow(current.Position);
			}
			return result;
		}
		finally
		{
			if (enumerator != null)
			{
				while (true)
				{
					switch (4)
					{
					case 0:
						continue;
					}
					if (1 == 0)
					{
						__ldtoken(DcelMesh.GetAabb());
					}
					enumerator.Dispose();
					break;
				}
			}
		}
	}

	internal void c91f82a72aa7891ee41e7f6ab5c0fa478()
	{
		cddc0e99ba4cceafb96748578af2570d4();
		using (List<DcelEdge>.Enumerator enumerator = c7893e0b992303cc211862f59e171df8c.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				DcelEdge current = enumerator.Current;
				if (current.InternalTag == 1)
				{
					continue;
				}
				DcelEdge twin = current.Twin;
				DcelFace face = current.Face;
				DcelFace face2 = twin.Face;
				if (face == null)
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
					break;
				}
				if (1 == 0)
				{
					__ldtoken(DcelMesh.c91f82a72aa7891ee41e7f6ab5c0fa478());
				}
				if (face2 == null)
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
				Vector3F c92b2ed2c76c9d5b2e6f8466991b83c = face.c92b2ed2c76c9d5b2e6f8466991b83c86;
				Vector3F c92b2ed2c76c9d5b2e6f8466991b83c2 = face2.c92b2ed2c76c9d5b2e6f8466991b83c86;
				float value = Vector3F.Dot(c92b2ed2c76c9d5b2e6f8466991b83c, c92b2ed2c76c9d5b2e6f8466991b83c2) / (c92b2ed2c76c9d5b2e6f8466991b83c.Length * c92b2ed2c76c9d5b2e6f8466991b83c2.Length);
				if (Numeric.AreEqual(value, 1f))
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
					DcelVertex origin = current.Origin;
					DcelVertex origin2 = twin.Origin;
					DcelEdge previous = current.Previous;
					DcelEdge next = twin.Next;
					DcelEdge previous2 = twin.Previous;
					DcelEdge next2 = current.Next;
					origin.Edge = next;
					origin2.Edge = next2;
					previous.Next = next;
					next.Previous = previous;
					previous2.Next = next2;
					next2.Previous = previous2;
					DcelEdge dcelEdge = next;
					do
					{
						dcelEdge.Face = face;
						dcelEdge = dcelEdge.Next;
					}
					while (dcelEdge != next);
					while (true)
					{
						switch (7)
						{
						case 0:
							continue;
						}
						break;
					}
					Dirty = true;
				}
				current.InternalTag = 1;
				twin.InternalTag = 1;
			}
			while (true)
			{
				switch (1)
				{
				case 0:
					break;
				default:
					goto end_IL_0172;
				}
				continue;
				end_IL_0172:
				break;
			}
		}
		ResetTags();
	}

	/// <summary>
	///       Cuts mesh with a plane.
	///       </summary>
	/// <param name="plane">The plane.</param>
	/// <returns>
	///       The mesh where the parts above the plane are removed. A new face is inserted at the cut.
	///       </returns>
	public bool CutConvex(Plane plane)
	{
		Aabb aabb = GetAabb();
		float num = Numeric.EpsilonF * (1f + aabb.Extent.Length);
		float num2 = float.PositiveInfinity;
		float num3 = float.NegativeInfinity;
		IEnumerator<DcelVertex> enumerator = Vertices.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				DcelVertex current = enumerator.Current;
				float num4 = Vector3F.Dot(current.Position, plane.Normal) - plane.DistanceFromOrigin;
				current.UserData = num4;
				num2 = Math.Min(num4, num2);
				num3 = Math.Max(num4, num3);
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
					__ldtoken(DcelMesh.CutConvex(Plane));
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
					switch (5)
					{
					case 0:
						continue;
					}
					enumerator.Dispose();
					break;
				}
			}
		}
		if (num2 > 0f - num)
		{
			Vertex = null;
			Dirty = true;
			return true;
		}
		if (num3 < num)
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
		DcelEdge dcelEdge = null;
		using (IEnumerator<DcelEdge> enumerator2 = Edges.GetEnumerator())
		{
			while (true)
			{
				if (enumerator2.MoveNext())
				{
					DcelEdge current2 = enumerator2.Current;
					float num5 = (float)current2.Origin.UserData;
					float num6 = (float)current2.Twin.Origin.UserData;
					if (num5 < 0f - num && num6 > 0f - num)
					{
						dcelEdge = current2;
						break;
					}
					continue;
				}
				while (true)
				{
					switch (2)
					{
					case 0:
						break;
					default:
						goto end_IL_015f;
					}
					continue;
					end_IL_015f:
					break;
				}
				break;
			}
		}
		DcelFace dcelFace = new DcelFace();
		DcelEdge dcelEdge2 = dcelEdge;
		DcelVertex dcelVertex = new DcelVertex();
		dcelVertex.Position = c1f11196d4aecb64c80b5e97b9eeb85ac(dcelEdge2);
		dcelVertex.Edge = dcelEdge.Twin;
		while (true)
		{
			DcelEdge next = dcelEdge2.Next;
			while ((float)next.Twin.Origin.UserData > 0f - num)
			{
				next = next.Next;
			}
			while (true)
			{
				switch (6)
				{
				case 0:
					continue;
				}
				DcelEdge dcelEdge3 = new DcelEdge();
				DcelEdge dcelEdge4 = new DcelEdge();
				dcelEdge2.Next = dcelEdge3;
				dcelEdge3.Face = dcelEdge2.Face;
				dcelEdge3.Origin = dcelVertex;
				dcelEdge3.Previous = dcelEdge2;
				dcelEdge3.Next = next;
				dcelEdge3.Twin = dcelEdge4;
				if (next.Twin != dcelEdge)
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
					dcelVertex = new DcelVertex();
					dcelVertex.Position = c1f11196d4aecb64c80b5e97b9eeb85ac(next);
					dcelVertex.Edge = next;
				}
				else
				{
					dcelVertex = dcelEdge.Next.Origin;
				}
				dcelEdge4.Face = dcelFace;
				dcelEdge4.Origin = dcelVertex;
				dcelEdge4.Twin = dcelEdge3;
				if (dcelFace.Boundary == null)
				{
					dcelFace.Boundary = dcelEdge4;
				}
				else
				{
					dcelEdge4.Next = dcelEdge2.Twin.Previous.Twin;
					dcelEdge4.Next.Previous = dcelEdge4;
				}
				next.Origin = dcelVertex;
				next.Previous = dcelEdge3;
				dcelEdge2.Face.Boundary = dcelEdge2;
				dcelEdge2 = next.Twin;
				if (dcelEdge2 != dcelEdge)
				{
					break;
				}
				while (true)
				{
					switch (7)
					{
					case 0:
						continue;
					}
					DcelEdge boundary = dcelFace.Boundary;
					DcelEdge dcelEdge5 = (boundary.Next = dcelEdge.Twin.Previous.Twin);
					dcelEdge5.Previous = boundary;
					Vertex = dcelVertex;
					Dirty = true;
					using (IEnumerator<DcelVertex> enumerator3 = Vertices.GetEnumerator())
					{
						while (enumerator3.MoveNext())
						{
							DcelVertex current3 = enumerator3.Current;
							current3.UserData = null;
						}
						while (true)
						{
							switch (1)
							{
							case 0:
								break;
							default:
								goto end_IL_038a;
							}
							continue;
							end_IL_038a:
							break;
						}
					}
					return true;
				}
			}
		}
	}

	private static Vector3F c1f11196d4aecb64c80b5e97b9eeb85ac(DcelEdge c58db163daa8ede8b1e0c6c619a868d58)
	{
		DcelVertex origin = c58db163daa8ede8b1e0c6c619a868d58.Origin;
		float value = (float)origin.UserData;
		DcelVertex origin2 = c58db163daa8ede8b1e0c6c619a868d58.Twin.Origin;
		float value2 = (float)origin2.UserData;
		float num = Math.Abs(value) / (Math.Abs(value) + Math.Abs(value2));
		return origin.Position * (1f - num) + origin2.Position * num;
	}

	private static void c6e426ad7f89865844dbe4dc8babf26af(DcelVertex c017396742a4443621f7602d4362206cd, int cd43c6f3133f0f2ab513e7cd8fc70d1cc)
	{
		if (c017396742a4443621f7602d4362206cd == null)
		{
			return;
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
				__ldtoken(DcelMesh.c6e426ad7f89865844dbe4dc8babf26af(DcelVertex, int));
			}
			if (c017396742a4443621f7602d4362206cd.Tag != cd43c6f3133f0f2ab513e7cd8fc70d1cc)
			{
				c017396742a4443621f7602d4362206cd.Tag = cd43c6f3133f0f2ab513e7cd8fc70d1cc;
				c6e426ad7f89865844dbe4dc8babf26af(c017396742a4443621f7602d4362206cd.Edge, cd43c6f3133f0f2ab513e7cd8fc70d1cc);
			}
			return;
		}
	}

	private static void c6e426ad7f89865844dbe4dc8babf26af(DcelEdge c58db163daa8ede8b1e0c6c619a868d58, int cd43c6f3133f0f2ab513e7cd8fc70d1cc)
	{
		if (c58db163daa8ede8b1e0c6c619a868d58 == null)
		{
			return;
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
				__ldtoken(DcelMesh.c6e426ad7f89865844dbe4dc8babf26af(DcelEdge, int));
			}
			if (c58db163daa8ede8b1e0c6c619a868d58.Tag == cd43c6f3133f0f2ab513e7cd8fc70d1cc)
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
			Stack<DcelEdge> stack = new Stack<DcelEdge>();
			stack.Push(c58db163daa8ede8b1e0c6c619a868d58);
			while (stack.Count > 0)
			{
				c58db163daa8ede8b1e0c6c619a868d58 = stack.Pop();
				if (c58db163daa8ede8b1e0c6c619a868d58.Tag == cd43c6f3133f0f2ab513e7cd8fc70d1cc)
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
				c58db163daa8ede8b1e0c6c619a868d58.Tag = cd43c6f3133f0f2ab513e7cd8fc70d1cc;
				if (c58db163daa8ede8b1e0c6c619a868d58.Origin != null)
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
					c58db163daa8ede8b1e0c6c619a868d58.Origin.Tag = 1;
				}
				if (c58db163daa8ede8b1e0c6c619a868d58.Face != null && c58db163daa8ede8b1e0c6c619a868d58.Face.Tag != cd43c6f3133f0f2ab513e7cd8fc70d1cc)
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
					c58db163daa8ede8b1e0c6c619a868d58.Face.Tag = cd43c6f3133f0f2ab513e7cd8fc70d1cc;
					ceaba6466c6ba954a7854de373f7f63e2(c58db163daa8ede8b1e0c6c619a868d58.Face.Boundary, stack, cd43c6f3133f0f2ab513e7cd8fc70d1cc);
					if (c58db163daa8ede8b1e0c6c619a868d58.Face.Holes != null)
					{
						for (int i = 0; i < c58db163daa8ede8b1e0c6c619a868d58.Face.Holes.Count; i++)
						{
							ceaba6466c6ba954a7854de373f7f63e2(c58db163daa8ede8b1e0c6c619a868d58.Face.Holes[i], stack, cd43c6f3133f0f2ab513e7cd8fc70d1cc);
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
				ceaba6466c6ba954a7854de373f7f63e2(c58db163daa8ede8b1e0c6c619a868d58.Next, stack, 1);
				ceaba6466c6ba954a7854de373f7f63e2(c58db163daa8ede8b1e0c6c619a868d58.Previous, stack, 1);
				ceaba6466c6ba954a7854de373f7f63e2(c58db163daa8ede8b1e0c6c619a868d58.Twin, stack, 1);
			}
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
	}

	private static void ceaba6466c6ba954a7854de373f7f63e2(DcelEdge c58db163daa8ede8b1e0c6c619a868d58, Stack<DcelEdge> c63511248710b2604ad15f716f6701d87, int cd43c6f3133f0f2ab513e7cd8fc70d1cc)
	{
		if (c58db163daa8ede8b1e0c6c619a868d58 != null && c58db163daa8ede8b1e0c6c619a868d58.Tag != cd43c6f3133f0f2ab513e7cd8fc70d1cc)
		{
			c63511248710b2604ad15f716f6701d87.Push(c58db163daa8ede8b1e0c6c619a868d58);
		}
	}

	private static void c2ac70623a32c8e1a5b576d05c6138ad3(DcelEdge c58db163daa8ede8b1e0c6c619a868d58, Stack<DcelEdge> c63511248710b2604ad15f716f6701d87, int ccfc05b318ad3d731f6133387622bcd04)
	{
		if (c58db163daa8ede8b1e0c6c619a868d58 == null)
		{
			return;
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
				__ldtoken(DcelMesh.c2ac70623a32c8e1a5b576d05c6138ad3(DcelEdge, Stack<DcelEdge>, int));
			}
			if (c58db163daa8ede8b1e0c6c619a868d58.InternalTag != ccfc05b318ad3d731f6133387622bcd04)
			{
				c63511248710b2604ad15f716f6701d87.Push(c58db163daa8ede8b1e0c6c619a868d58);
			}
			return;
		}
	}

	private static void c6e426ad7f89865844dbe4dc8babf26af(DcelFace c0b89ebd3492c28b5af64c64c59f1627f, int cd43c6f3133f0f2ab513e7cd8fc70d1cc)
	{
		if (c0b89ebd3492c28b5af64c64c59f1627f == null)
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
			if (1 == 0)
			{
				__ldtoken(DcelMesh.c6e426ad7f89865844dbe4dc8babf26af(DcelFace, int));
			}
			if (c0b89ebd3492c28b5af64c64c59f1627f.Tag == cd43c6f3133f0f2ab513e7cd8fc70d1cc)
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
				c0b89ebd3492c28b5af64c64c59f1627f.Tag = cd43c6f3133f0f2ab513e7cd8fc70d1cc;
				c6e426ad7f89865844dbe4dc8babf26af(c0b89ebd3492c28b5af64c64c59f1627f.Boundary, cd43c6f3133f0f2ab513e7cd8fc70d1cc);
				if (c0b89ebd3492c28b5af64c64c59f1627f.Holes == null)
				{
					return;
				}
				using List<DcelEdge>.Enumerator enumerator = c0b89ebd3492c28b5af64c64c59f1627f.Holes.GetEnumerator();
				while (enumerator.MoveNext())
				{
					DcelEdge current = enumerator.Current;
					c6e426ad7f89865844dbe4dc8babf26af(current, cd43c6f3133f0f2ab513e7cd8fc70d1cc);
				}
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
		}
	}

	[CompilerGenerated]
	private static bool cee5df6b11812ab10f93f92d3d51d6d8b(DcelFace c4416e2ec407f91e8cc3086dd695bb8c5)
	{
		return c4416e2ec407f91e8cc3086dd695bb8c5.Holes == null;
	}

	[CompilerGenerated]
	private static bool c08a141fcfb76458077614b37378a4896(DcelEdge c4496d1e28885071f753d5d8c6ed007b6)
	{
		if (c4496d1e28885071f753d5d8c6ed007b6.Face != null)
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
			if (1 == 0)
			{
				__ldtoken(DcelMesh.c08a141fcfb76458077614b37378a4896(DcelEdge));
			}
			if (c4496d1e28885071f753d5d8c6ed007b6.Twin != null)
			{
				while (true)
				{
					switch (7)
					{
					case 0:
						break;
					default:
						return c4496d1e28885071f753d5d8c6ed007b6.Twin.Face != null;
					}
				}
			}
		}
		return false;
	}

	[CompilerGenerated]
	private static bool ce73655a81757cd28403254a153ebc4fd(DcelFace c4416e2ec407f91e8cc3086dd695bb8c5)
	{
		return c4416e2ec407f91e8cc3086dd695bb8c5.Holes == null;
	}

	[CompilerGenerated]
	private static bool cd28dbb98588f0534f4bada0bb26db36b(DcelEdge c4496d1e28885071f753d5d8c6ed007b6)
	{
		if (c4496d1e28885071f753d5d8c6ed007b6.Next != null && c4496d1e28885071f753d5d8c6ed007b6.Next.Next == c4496d1e28885071f753d5d8c6ed007b6.Previous)
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
						__ldtoken(DcelMesh.cd28dbb98588f0534f4bada0bb26db36b(DcelEdge));
					}
					return c4496d1e28885071f753d5d8c6ed007b6.Next != c4496d1e28885071f753d5d8c6ed007b6.Previous;
				}
			}
		}
		return false;
	}

	[CompilerGenerated]
	private static bool cc1ae76212f138d97ca5cddaae7b38443(DcelVertex caa9d7ab80719ca5895d6816e6deee0d6)
	{
		return caa9d7ab80719ca5895d6816e6deee0d6.Tag != 1;
	}

	[CompilerGenerated]
	private static bool cf515f4bc015f97ba87d8a695d2839f4f(DcelEdge c4496d1e28885071f753d5d8c6ed007b6)
	{
		return c4496d1e28885071f753d5d8c6ed007b6.Tag != 1;
	}

	[CompilerGenerated]
	private static bool c9d675f95d2a30f2bafa0fbe9c1e61942(DcelFace c4416e2ec407f91e8cc3086dd695bb8c5)
	{
		return c4416e2ec407f91e8cc3086dd695bb8c5.Tag != 1;
	}
}
