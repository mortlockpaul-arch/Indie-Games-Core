using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Rendering;

namespace SynapseGaming.LightingSystem.Core;

public static class GeometryExtractionHelper
{
	private static Dictionary<RenderableMesh.ComparisonIndex, GeometryData> l8a = new Dictionary<RenderableMesh.ComparisonIndex, GeometryData>(256);

	private static Dictionary<RenderableMesh.ComparisonIndex.BufferComparisonIndex, GeometryData> l8_u0013_ = new Dictionary<RenderableMesh.ComparisonIndex.BufferComparisonIndex, GeometryData>(256);

	private static byte[] l8N = new byte[1];

	private static ushort[] l8l = new ushort[1];

	private static int[] l8L = new int[1];

	private static Dictionary<int, int> l8w = new Dictionary<int, int>(256);

	private static SystemStatistic l8J = SystemConsole.GetStatistic("GeometryExtraction_GetRawBufferData", SystemStatisticCategory.SceneGraph);

	private static SystemStatistic l8B = SystemConsole.GetStatistic("GeometryExtraction_GetMeshData", SystemStatisticCategory.SceneGraph);

	public static void Clear()
	{
		l8a.Clear();
		l8_u0013_.Clear();
	}

	public static GeometryData GetRawBufferData(RenderableMesh.ComparisonIndex.BufferComparisonIndex buffercomparisonindex)
	{
		if (l8_u0013_.TryGetValue(buffercomparisonindex, out var value))
		{
			return value;
		}
		l8J.AccumulationValue++;
		value = new GeometryData();
		l8_u0013_.Add(buffercomparisonindex, value);
		VertexBuffer vertexBuffer = buffercomparisonindex.l8_u0013_;
		IndexBuffer indexBuffer = buffercomparisonindex.l8a;
		int vertexCount = vertexBuffer.VertexCount;
		int num = vertexCount * vertexBuffer.VertexDeclaration.VertexStride;
		int indexCount = indexBuffer.IndexCount;
		if (indexBuffer.IndexElementSize == IndexElementSize.SixteenBits)
		{
			if (l8l.Length < indexCount)
			{
				l8l = new ushort[indexCount];
			}
			indexBuffer.GetData(l8l, 0, indexCount);
			for (int i = 0; i < indexCount; i++)
			{
				value.Indices.Add(l8l[i]);
			}
		}
		else
		{
			if (l8L.Length < indexCount)
			{
				l8L = new int[indexCount];
			}
			indexBuffer.GetData(l8L, 0, indexCount);
			for (int j = 0; j < indexCount; j++)
			{
				value.Indices.Add(l8L[j]);
			}
		}
		if (l8N.Length < num)
		{
			l8N = new byte[num];
		}
		vertexBuffer.GetData(l8N, 0, num);
		int vertexStride = vertexBuffer.VertexDeclaration.VertexStride;
		for (int k = 0; k < vertexCount; k++)
		{
			int num2 = k * vertexStride;
			int num3 = 4;
			Vector3 item = new Vector3
			{
				X = BitConverter.ToSingle(l8N, num2),
				Y = BitConverter.ToSingle(l8N, num2 + num3),
				Z = BitConverter.ToSingle(l8N, num2 + num3 * 2)
			};
			value.Vertices.Add(item);
		}
		return value;
	}

	public static GeometryData GetMeshData(RenderableMesh mesh)
	{
		if (l8a.TryGetValue(mesh.Index, out var value))
		{
			return value;
		}
		l8B.AccumulationValue++;
		value = new GeometryData();
		l8a.Add(mesh.Index, value);
		if (mesh.PrimitiveType != PrimitiveType.TriangleList)
		{
			throw new Exception("Collideable meshes must use TriangleList primitive type.");
		}
		if (mesh.VertexStreamOffset != 0)
		{
			throw new Exception("Collideable meshes cannot use stream offset.");
		}
		GeometryData rawBufferData = GetRawBufferData(mesh.Index.BufferIndex);
		l8w.Clear();
		int num = mesh.PrimitiveCount * 3;
		int elementStart = mesh.ElementStart;
		int vertexBase = mesh.VertexBase;
		for (int i = 0; i < num; i++)
		{
			int num2 = rawBufferData.Indices[i + elementStart] + vertexBase;
			if (l8w.ContainsKey(num2))
			{
				value.Indices.Add(l8w[num2]);
				continue;
			}
			Vector3 item = rawBufferData.Vertices[num2];
			int count = value.Vertices.Count;
			l8w.Add(num2, count);
			value.Indices.Add(count);
			value.Vertices.Add(item);
		}
		int num3 = mesh.PrimitiveCount;
		for (int j = 0; j < num3; j++)
		{
			int num4 = j * 3;
			Vector3 a = value.Vertices[value.Indices[num4]];
			Vector3 b = value.Vertices[value.Indices[num4 + 1]];
			Vector3 c = value.Vertices[value.Indices[num4 + 2]];
			if (CoreHelper.IsDegenerate(ref a, ref b, ref c))
			{
				value.Indices.RemoveRange(num4, 3);
				j--;
				num3--;
			}
		}
		return value;
	}
}
