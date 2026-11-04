using System;
using System.Collections.Generic;
using A;

namespace DigitalRune.Geometry.Meshes;

/// <summary>
///       Stores the adjacency lists for the vertices of a mesh. (For internal use only.)
///       </summary>
[Serializable]
public class VertexAdjacency
{
	/// <summary>
	///       The start indices of the adjacency list of a given vertex.
	///       </summary>
	[CLSCompliant(false)]
	public ushort[] ListIndices;

	/// <summary>
	///       The adjacency lists of all vertices stored in a single array.
	///       </summary>
	[CLSCompliant(false)]
	public ushort[] Lists;

	internal VertexAdjacency()
	{
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Meshes.VertexAdjacency" /> class.
	///       </summary>
	/// <param name="mesh">The mesh for which the adjacency information is built.</param>
	/// <exception cref="T:System.NotSupportedException">
	///       Too many vertices in convex hull. Max. 65534 vertices in convex hull are supported.
	///       </exception>
	public VertexAdjacency(DcelMesh mesh)
	{
		int count = mesh.Vertices.Count;
		if (count >= 65535)
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
						__ldtoken(VertexAdjacency._002Ector(DcelMesh));
					}
					throw new NotSupportedException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7872));
				}
			}
		}
		for (int i = 0; i < count; i++)
		{
			mesh.Vertices[i].Tag = i;
		}
		ListIndices = new ushort[count + 1];
		List<ushort> list = new List<ushort>(count * 4);
		for (int j = 0; j < count; j++)
		{
			DcelVertex dcelVertex = mesh.Vertices[j];
			ListIndices[j] = (ushort)list.Count;
			DcelEdge edge = dcelVertex.Edge;
			DcelEdge dcelEdge = edge;
			do
			{
				DcelVertex origin = dcelEdge.Next.Origin;
				int tag = origin.Tag;
				list.Add((ushort)tag);
				dcelEdge = dcelEdge.Twin.Next;
			}
			while (dcelEdge != edge);
		}
		ListIndices[count] = (ushort)list.Count;
		Lists = list.ToArray();
	}
}
