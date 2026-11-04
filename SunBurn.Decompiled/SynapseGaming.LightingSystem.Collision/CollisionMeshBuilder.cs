using System;
using System.Collections.Generic;
using L;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Rendering;

namespace SynapseGaming.LightingSystem.Collision;

/// <summary>
/// Provides methods for generating collision meshes and mesh
/// data from renderable meshes. Also caches the generated
/// data for later requests.
/// </summary>
public class CollisionMeshBuilder
{
	private Dictionary<int, CollisionMeshData> fNB = new Dictionary<int, CollisionMeshData>(256);

	private Dictionary<int, CollisionMeshData> fNa = new Dictionary<int, CollisionMeshData>(256);

	private Dictionary<int, int> fNg = new Dictionary<int, int>(256);

	private byte[] fNf = new byte[1];

	private ushort[] fN_0003 = new ushort[1];

	private int[] fNn = new int[1];

	private List<ICollisionMaterial> fN_0019 = new List<ICollisionMaterial>(256);

	private SystemStatistic fNL = SystemConsole.GetStatistic("Collision_BuildWorldMesh", SystemStatisticCategory.Collision);

	private SystemStatistic fNv = SystemConsole.GetStatistic("Collision_BuildCollisionMesh", SystemStatisticCategory.Collision);

	/// <summary>
	/// Clears all cached mesh data.
	/// </summary>
	public void Unload()
	{
		fNB.Clear();
		fNa.Clear();
	}

	private int u(RenderableMesh P_0)
	{
		return (P_0.fNw ^ 1) + (P_0.fN_0005.GetHashCode() ^ 2) + (P_0.fN_0011 ^ 3) + (P_0.fNc ^ 4) + (P_0.fNj.GetHashCode() ^ 5) + (P_0.fNo ^ 6);
	}

	/// <summary>
	/// Calculates world-space collision mesh geometry from the
	/// mesh-space collision data in the provided renderable meshes.
	/// </summary>
	/// <param name="meshes">Renderable meshes to generate the collision mesh geometry from.</param>
	/// <param name="worldmesh">Collision mesh to fill with world-space geometry.</param>
	public void BuildWorldMesh(RenderableMeshCollection meshes, CollisionMesh worldmesh)
	{
		fNL.AccumulationValue++;
		int num = 0;
		int num2 = 0;
		for (int k = 0; k < meshes.Count; k++)
		{
			CollisionMeshData collisionMeshData = GetCollisionMeshData(meshes[k]);
			num += collisionMeshData.Vertices.Count;
			num2 += collisionMeshData.Indices.Count;
		}
		int num3 = num2 / 3;
		if (worldmesh.Vertices == null || worldmesh.Vertices.Length < num)
		{
			worldmesh.Vertices = new Vector3[num];
		}
		if (worldmesh.Indices == null || worldmesh.Indices.Length < num2)
		{
			worldmesh.Indices = new int[num2];
		}
		fN_0019.Clear();
		num = 0;
		num2 = 0;
		for (int l = 0; l < meshes.Count; l++)
		{
			RenderableMesh renderableMesh = meshes[l];
			CollisionMeshData collisionMeshData2 = GetCollisionMeshData(renderableMesh);
			ICollisionMaterial item = renderableMesh.fN_0015 as ICollisionMaterial;
			int num4 = num;
			int num5 = collisionMeshData2.Indices.Count / 3;
			bool flag = renderableMesh.fN8.Determinant() < 0f;
			for (int m = 0; m < collisionMeshData2.Vertices.Count; m++)
			{
				ref Vector3 reference = ref worldmesh.Vertices[num++];
				reference = Vector3.Transform(collisionMeshData2.Vertices[m], renderableMesh.fN8);
			}
			for (int num6 = 0; num6 < num5; num6++)
			{
				int num7 = num6 * 3;
				if (flag)
				{
					worldmesh.Indices[num2++] = collisionMeshData2.Indices[num7] + num4;
					worldmesh.Indices[num2++] = collisionMeshData2.Indices[num7 + 2] + num4;
					worldmesh.Indices[num2++] = collisionMeshData2.Indices[num7 + 1] + num4;
				}
				else
				{
					worldmesh.Indices[num2++] = collisionMeshData2.Indices[num7] + num4;
					worldmesh.Indices[num2++] = collisionMeshData2.Indices[num7 + 1] + num4;
					worldmesh.Indices[num2++] = collisionMeshData2.Indices[num7 + 2] + num4;
				}
				fN_0019.Add(item);
			}
		}
		worldmesh.Surfaces.Clear();
		int num8 = 0;
		for (int num9 = 0; num9 < num3; num9++)
		{
			int num10 = worldmesh.Indices[num8++];
			int num11 = worldmesh.Indices[num8++];
			int num12 = worldmesh.Indices[num8++];
			Vector3 vector = worldmesh.Vertices[num10];
			Vector3 vector2 = worldmesh.Vertices[num12];
			Vector3 vector3 = worldmesh.Vertices[num11];
			CollisionMesh.CollisionSurface collisionSurface = new CollisionMesh.CollisionSurface();
			collisionSurface.VertexIndex0 = num10;
			collisionSurface.VertexIndex1 = num12;
			collisionSurface.VertexIndex2 = num11;
			Plane surface = new Plane(vector, vector2, vector3);
			Vector3 point = vector - surface.Normal;
			Vector3 point2 = vector2 - surface.Normal;
			collisionSurface.Surface = surface;
			collisionSurface.Edge0 = new Plane(vector, vector2, point);
			collisionSurface.Edge1 = new Plane(vector2, vector3, point2);
			collisionSurface.Edge2 = new Plane(vector3, vector, point);
			collisionSurface.Material = fN_0019[num9];
			worldmesh.Surfaces.Add(collisionSurface);
		}
		worldmesh._0016();
	}

	private CollisionMeshData _1(RenderableMesh P_0)
	{
		int key = (P_0.fN_0005.GetHashCode() ^ 1) + (P_0.fNj.GetHashCode() ^ 2);
		if (fNa.TryGetValue(key, out var value))
		{
			return value;
		}
		value = new CollisionMeshData();
		fNa.Add(key, value);
		VertexBuffer fNj = P_0.fNj;
		IndexBuffer indexBuffer = P_0.fN_0005;
		int vertexCount = fNj.VertexCount;
		int num = vertexCount * fNj.VertexDeclaration.VertexStride;
		int indexCount = indexBuffer.IndexCount;
		if (indexBuffer.IndexElementSize == IndexElementSize.SixteenBits)
		{
			if (fN_0003.Length < indexCount)
			{
				fN_0003 = new ushort[indexCount];
			}
			indexBuffer.GetData(fN_0003, 0, indexCount);
			for (int k = 0; k < indexCount; k++)
			{
				value.Indices.Add(fN_0003[k]);
			}
		}
		else
		{
			if (fNn.Length < indexCount)
			{
				fNn = new int[indexCount];
			}
			indexBuffer.GetData(fNn, 0, indexCount);
			for (int l = 0; l < indexCount; l++)
			{
				value.Indices.Add(fNn[l]);
			}
		}
		if (fNf.Length < num)
		{
			fNf = new byte[num];
		}
		fNj.GetData(fNf, 0, num);
		int vertexStride = P_0.fNj.VertexDeclaration.VertexStride;
		for (int m = 0; m < vertexCount; m++)
		{
			int num2 = m * vertexStride;
			int num3 = 4;
			Vector3 item = new Vector3
			{
				X = BitConverter.ToSingle(fNf, num2),
				Y = BitConverter.ToSingle(fNf, num2 + num3),
				Z = BitConverter.ToSingle(fNf, num2 + num3 * 2)
			};
			value.Vertices.Add(item);
		}
		return value;
	}

	/// <summary>
	/// Builds the collision mesh data for the provided renderable
	/// mesh. If the data already exists the cached version is returned.
	/// </summary>
	/// <param name="mesh">Renderable mesh to generated the collision mesh data from.</param>
	/// <returns></returns>
	public CollisionMeshData GetCollisionMeshData(RenderableMesh mesh)
	{
		int key = u(mesh);
		if (fNB.TryGetValue(key, out var value))
		{
			return value;
		}
		fNv.AccumulationValue++;
		value = new CollisionMeshData();
		fNB.Add(key, value);
		if (mesh.fNs != PrimitiveType.TriangleList)
		{
			throw new Exception("Collideable meshes must use TriangleList primitive type.");
		}
		if (mesh.fNo != 0)
		{
			throw new Exception("Collideable meshes cannot use stream offset.");
		}
		CollisionMeshData collisionMeshData = _1(mesh);
		fNg.Clear();
		int num = mesh.fN_0011 * 3;
		for (int k = 0; k < num; k++)
		{
			int num2 = collisionMeshData.Indices[k + mesh.fNw] + mesh.fNc;
			if (fNg.ContainsKey(num2))
			{
				value.Indices.Add(fNg[num2]);
				continue;
			}
			Vector3 item = collisionMeshData.Vertices[num2];
			int count = value.Vertices.Count;
			fNg.Add(num2, count);
			value.Indices.Add(count);
			value.Vertices.Add(item);
		}
		int num3 = mesh.fN_0011;
		for (int l = 0; l < num3; l++)
		{
			int num4 = l * 3;
			Vector3 vector = value.Vertices[value.Indices[num4]];
			Vector3 vector2 = value.Vertices[value.Indices[num4 + 1]];
			Vector3 vector3 = value.Vertices[value.Indices[num4 + 2]];
			if (L._0019.ad(ref vector, ref vector2, ref vector3))
			{
				value.Indices.RemoveRange(num4, 3);
				l--;
				num3--;
			}
		}
		return value;
	}
}
