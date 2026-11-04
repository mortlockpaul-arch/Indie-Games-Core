using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;

namespace d;

internal class a : IDisposable
{
	private GraphicsDevice fNB;

	private static List<VertexPositionNormalTextureBump> fNa = new List<VertexPositionNormalTextureBump>(64);

	[CompilerGenerated]
	private int fNg;

	[CompilerGenerated]
	private VertexBuffer fNf;

	internal int PrimitiveCount
	{
		[CompilerGenerated]
		get
		{
			return fNg;
		}
		[CompilerGenerated]
		private set
		{
			fNg = value;
		}
	}

	internal VertexBuffer VertexBuffer
	{
		[CompilerGenerated]
		get
		{
			return fNf;
		}
		[CompilerGenerated]
		private set
		{
			fNf = value;
		}
	}

	internal a(GraphicsDevice P_0, int P_1)
	{
		fNB = P_0;
		fNa.Clear();
		float num = 1f;
		float num2 = num / (float)P_1;
		float num3 = num2;
		float num4 = MathHelper.ToRadians(90f);
		for (int i = 0; i < P_1 - 1; i++)
		{
			float num5 = num3 / num;
			float num6 = (float)Math.Cos(num4 * num5);
			fN(fNa, num3, num6 * num, num, 1f - num5);
			num3 += num2;
		}
		f_0018(fNa, Matrix.CreateRotationZ(MathHelper.ToRadians(180f)));
		fN(fNa, 0f, num, num, 1f);
		f_0018(fNa, Matrix.CreateRotationZ(MathHelper.ToRadians(90f)));
		VertexPositionNormalTextureBump[] array = fNa.ToArray();
		PrimitiveCount = array.Length / 3;
		VertexBuffer = new VertexBuffer(P_0, typeof(VertexPositionNormalTextureBump), array.Length, BufferUsage.WriteOnly);
		VertexBuffer.SetData(array);
	}

	internal void _5()
	{
		fNB.DrawPrimitives(PrimitiveType.TriangleList, 0, PrimitiveCount);
	}

	public void Dispose()
	{
		PrimitiveCount = 0;
		fNB = null;
		VertexBuffer.Dispose();
	}

	private void f_0018(List<VertexPositionNormalTextureBump> P_0, Matrix P_1)
	{
		int count = P_0.Count;
		for (int i = 0; i < count; i++)
		{
			VertexPositionNormalTextureBump item = P_0[i];
			item.Normal = Vector3.TransformNormal(item.Normal, P_1);
			item.Position = Vector3.Transform(item.Position, P_1);
			P_0.Add(item);
		}
	}

	private void fN(List<VertexPositionNormalTextureBump> P_0, float P_1, float P_2, float P_3, float P_4)
	{
		VertexPositionNormalTextureBump item = default;
		Vector3 vector = new Vector3(P_2, P_1, 0f - P_3);
		Vector3 vector2 = new Vector3(0f - P_2, P_1, 0f - P_3);
		Vector3 normal = new Plane(Vector3.Zero, vector, vector2).Normal;
		item.Position = Vector3.Zero;
		item.TextureCoordinate = Vector2.Zero;
		item.Normal = normal;
		item.Tangent = Vector3.One * P_4;
		P_0.Add(item);
		item.Position = vector;
		item.TextureCoordinate = new Vector2(1f, 0f);
		item.Normal = normal;
		item.Tangent = Vector3.Zero;
		P_0.Add(item);
		item.Position = vector2;
		item.TextureCoordinate = new Vector2(0f, 1f);
		item.Normal = normal;
		item.Tangent = Vector3.Zero;
		P_0.Add(item);
	}
}
