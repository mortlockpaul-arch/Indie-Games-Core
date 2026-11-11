using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SynapseGaming.LightingSystem.Core;

public struct VertexPositionNormalTextureBump : IVertexType
{
	public Vector3 Position;

	public Vector3 Normal;

	public Vector2 TextureCoordinate;

	public Vector3 Tangent;

	public Vector3 Binormal;

	public static readonly VertexElement[] VertexElements;

	public static readonly VertexDeclaration VertexDeclaration;

	public static int SizeInBytes => 56;

	VertexDeclaration IVertexType.VertexDeclaration => VertexDeclaration;

	static VertexPositionNormalTextureBump()
	{
		VertexElements = new VertexElement[5]
		{
			new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
			new VertexElement(12, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0),
			new VertexElement(24, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0),
			new VertexElement(32, VertexElementFormat.Vector3, VertexElementUsage.Tangent, 0),
			new VertexElement(44, VertexElementFormat.Vector3, VertexElementUsage.Binormal, 0)
		};
		VertexDeclaration = new VertexDeclaration(VertexElements)
		{
			Name = "VertexPositionNormalTextureBump.VertexDeclaration"
		};
	}

	public static void BuildTangentSpaceDataForTriangleList<T>(T[] indices, VertexPositionNormalTextureBump[] vertices)
	{
		for (int i = 0; i < indices.Length; i += 3)
		{
			int num = (int)Convert.ChangeType(indices[i], typeof(int), null);
			int num2 = (int)Convert.ChangeType(indices[i + 1], typeof(int), null);
			int num3 = (int)Convert.ChangeType(indices[i + 2], typeof(int), null);
			Vector2 textureCoordinate = vertices[num].TextureCoordinate;
			Vector2 textureCoordinate2 = vertices[num2].TextureCoordinate;
			Vector2 textureCoordinate3 = vertices[num3].TextureCoordinate;
			float num4 = textureCoordinate2.X - textureCoordinate.X;
			float num5 = textureCoordinate3.X - textureCoordinate.X;
			float num6 = textureCoordinate2.Y - textureCoordinate.Y;
			float num7 = textureCoordinate3.Y - textureCoordinate.Y;
			float num8 = num4 * num7 - num5 * num6;
			if (num8 != 0f)
			{
				num8 = 1f / num8;
				Vector3 position = vertices[num].Position;
				Vector3 position2 = vertices[num2].Position;
				Vector3 position3 = vertices[num3].Position;
				float num9 = position2.X - position.X;
				float num10 = position3.X - position.X;
				float num11 = position2.Y - position.Y;
				float num12 = position3.Y - position.Y;
				float num13 = position2.Z - position.Z;
				float num14 = position3.Z - position.Z;
				Vector3 vector = new Vector3((num7 * num9 - num6 * num10) * num8, (num7 * num11 - num6 * num12) * num8, (num7 * num13 - num6 * num14) * num8);
				Vector3 vector2 = new Vector3((num4 * num10 - num5 * num9) * num8, (num4 * num12 - num5 * num11) * num8, (num4 * num14 - num5 * num13) * num8);
				vertices[num].Tangent += vector;
				vertices[num2].Tangent += vector;
				vertices[num3].Tangent += vector;
				vertices[num].Binormal += vector2;
				vertices[num2].Binormal += vector2;
				vertices[num3].Binormal += vector2;
			}
		}
		for (int j = 0; j < vertices.Length; j++)
		{
			Vector3 tangent = vertices[j].Tangent;
			if (!(tangent == Vector3.Zero))
			{
				vertices[j].Tangent = Vector3.Normalize(tangent);
				vertices[j].Binormal = Vector3.Normalize(vertices[j].Binormal);
			}
		}
	}
}
