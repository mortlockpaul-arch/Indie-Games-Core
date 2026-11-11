using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Effects;
using y;

namespace B
{
	internal class N : IDisposable
	{
		private GraphicsDevice l8a;

		private BasicEffect l8_u0013_;

		private VertexBuffer l8N;

		private IndexBuffer l8l;

		private static Vector3[] l8L = new Vector3[8]
		{
			new Vector3(1f, 1f, 1f),
			new Vector3(0f, 1f, 1f),
			new Vector3(1f, 0f, 1f),
			new Vector3(0f, 0f, 1f),
			new Vector3(1f, 1f, 0f),
			new Vector3(0f, 1f, 0f),
			new Vector3(1f, 0f, 0f),
			new Vector3(0f, 0f, 0f)
		};

		private static int[] l8w = new int[24]
		{
			1, 0, 3, 2, 4, 5, 6, 7, 0, 4,
			2, 6, 5, 1, 7, 3, 0, 1, 4, 5,
			3, 2, 7, 6
		};

		internal BasicEffect DefaultEffect => l8_u0013_;

		internal N(GraphicsDevice param_0)
		{
			l8a = param_0;
			l8_u0013_ = new SunBurnBasicEffect(l8a);
			l8_u0013_.TextureEnabled = false;
			l8_u0013_.VertexColorEnabled = false;
			l8_u0013_.PreferPerPixelLighting = false;
			l8_u0013_.LightingEnabled = false;
			l8_u0013_.FogEnabled = false;
			l8_u0013_.SpecularColor = Vector3.Zero;
			VertexPositionTexture[] array = new VertexPositionTexture[24];
			short[] array2 = new short[36];
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < 6; i++)
			{
				Vector3 position = l8L[l8w[num]];
				Vector3 position2 = l8L[l8w[num + 1]];
				Vector3 position3 = l8L[l8w[num + 2]];
				Vector3 position4 = l8L[l8w[num + 3]];
				array[num].Position = position;
				array[num + 1].Position = position2;
				array[num + 2].Position = position3;
				array[num + 3].Position = position4;
				array2[num2++] = (byte)num;
				array2[num2++] = (byte)(num + 1);
				array2[num2++] = (byte)(num + 2);
				array2[num2++] = (byte)(num + 3);
				array2[num2++] = (byte)(num + 2);
				array2[num2++] = (byte)(num + 1);
				num += 4;
			}
			l8N = new VertexBuffer(l8a, typeof(VertexPositionTexture), array.Length, BufferUsage.WriteOnly);
			l8N.SetData(array);
			l8l = new IndexBuffer(l8a, typeof(short), array2.Length, BufferUsage.WriteOnly);
			l8l.SetData(array2);
		}

		internal Matrix _u0018_(BoundingBox param_0)
		{
			Matrix result = Matrix.CreateScale(param_0.Max - param_0.Min);
			result.Translation = param_0.Min;
			return result;
		}

		internal void S()
		{
			l8a.SetVertexBuffer(l8N);
			l8a.Indices = l8l;
			l8a.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 24, 0, 12);
		}

		public void Dispose()
		{
			y.a.w_u0005_(ref l8_u0013_);
			y.a.w_u0005_(ref l8N);
			y.a.w_u0005_(ref l8l);
		}
	}
}
namespace b
{
	internal interface N
	{
		TextureCube ShadowFaceMap { get; set; }

		TextureCube ShadowCoordMap { get; set; }

		Texture2D ShadowMap { get; }

		BoundingSphere ShadowArea { set; }

		Vector4 ShadowViewDistance { get; set; }

		Vector4[] ShadowMapLocationAndSpan { set; }

		Matrix[] ShadowViewProjection { set; }

		DetailPreference EffectDetail { get; set; }

		void SetShadowMapAndType(Texture2D shadowmap, B type);
	}
}
