using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;

namespace w
{
	internal class _u0013_ : List<a>
	{
	}
}
namespace W
{
	internal class _u0013_ : IDisposable
	{
		private GraphicsDevice l8a;

		private static List<VertexPositionNormalTextureBump> l8_u0013_ = new List<VertexPositionNormalTextureBump>(64);

		[CompilerGenerated]
		private int l8N;

		[CompilerGenerated]
		private VertexBuffer l8l;

		internal int PrimitiveCount
		{
			[CompilerGenerated]
			get
			{
				return l8N;
			}
			[CompilerGenerated]
			private set
			{
				l8N = value;
			}
		}

		internal VertexBuffer VertexBuffer
		{
			[CompilerGenerated]
			get
			{
				return l8l;
			}
			[CompilerGenerated]
			private set
			{
				l8l = value;
			}
		}

		internal _u0013_(GraphicsDevice param_0, int param_1)
		{
			l8a = param_0;
			l8_u0013_.Clear();
			float num = 1f;
			float num2 = num / (float)param_1;
			float num3 = num2;
			float num4 = MathHelper.ToRadians(90f);
			for (int i = 0; i < param_1 - 1; i++)
			{
				float num5 = num3 / num;
				float num6 = (float)Math.Cos(num4 * num5);
				l_u0018_(l8_u0013_, num3, num6 * num, num, 1f - num5);
				num3 += num2;
			}
			l_u0001_(l8_u0013_, Matrix.CreateRotationZ(MathHelper.ToRadians(180f)));
			l_u0018_(l8_u0013_, 0f, num, num, 1f);
			l_u0001_(l8_u0013_, Matrix.CreateRotationZ(MathHelper.ToRadians(90f)));
			VertexPositionNormalTextureBump[] array = l8_u0013_.ToArray();
			PrimitiveCount = array.Length / 3;
			VertexBuffer = new VertexBuffer(param_0, typeof(VertexPositionNormalTextureBump), array.Length, BufferUsage.WriteOnly);
			VertexBuffer.SetData(array);
		}

		internal void S()
		{
			l8a.DrawPrimitives(PrimitiveType.TriangleList, 0, PrimitiveCount);
		}

		public void Dispose()
		{
			PrimitiveCount = 0;
			l8a = null;
			VertexBuffer.Dispose();
		}

		private void l_u0001_(List<VertexPositionNormalTextureBump> param_0, Matrix param_1)
		{
			int count = param_0.Count;
			for (int i = 0; i < count; i++)
			{
				VertexPositionNormalTextureBump item = param_0[i];
				item.Normal = Vector3.TransformNormal(item.Normal, param_1);
				item.Position = Vector3.Transform(item.Position, param_1);
				param_0.Add(item);
			}
		}

		private void l_u0018_(List<VertexPositionNormalTextureBump> param_0, float param_1, float param_2, float param_3, float param_4)
		{
			VertexPositionNormalTextureBump item = default;
			Vector3 vector = new Vector3(param_2, param_1, 0f - param_3);
			Vector3 vector2 = new Vector3(0f - param_2, param_1, 0f - param_3);
			Vector3 normal = new Plane(Vector3.Zero, vector, vector2).Normal;
			item.Position = Vector3.Zero;
			item.TextureCoordinate = Vector2.Zero;
			item.Normal = normal;
			item.Tangent = Vector3.One * param_4;
			param_0.Add(item);
			item.Position = vector;
			item.TextureCoordinate = new Vector2(1f, 0f);
			item.Normal = normal;
			item.Tangent = Vector3.Zero;
			param_0.Add(item);
			item.Position = vector2;
			item.TextureCoordinate = new Vector2(0f, 1f);
			item.Normal = normal;
			item.Tangent = Vector3.Zero;
			param_0.Add(item);
		}
	}
}
