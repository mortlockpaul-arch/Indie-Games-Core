using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace x;

internal class Y : IDisposable
{
	private BoundingBox l8a = default;

	private int l8_u0013_;

	private IndexBuffer l8N;

	private VertexBuffer l8l;

	internal BoundingBox ObjectBoundingBox => l8a;

	internal int VertexCount => l8_u0013_;

	internal IndexBuffer IndexBuffer => l8N;

	internal VertexBuffer VertexBuffer => l8l;

	internal unsafe void L_u0001_(GraphicsDevice param_0, v[] param_1, ushort[] param_2, int param_3)
	{
		if (l8l == null)
		{
			l8l = new VertexBuffer(param_0, typeof(v), 4096, BufferUsage.None);
			l8N = new IndexBuffer(param_0, typeof(ushort), 6144, BufferUsage.None);
		}
		fixed (v* ptr = param_1)
		{
			fixed (Vector3* max = &l8a.Max)
			{
				fixed (Vector3* min = &l8a.Min)
				{
					int num = param_3 * 4;
					v* ptr2 = ptr;
					min->X = (max->X = ptr2->Position.X);
					min->Y = (max->Y = ptr2->Position.Y);
					min->Z = (max->Z = ptr2->Position.Z);
					ptr2++;
					for (int i = 1; i < num; i++)
					{
						if (ptr2->Position.X > max->X)
						{
							max->X = ptr2->Position.X;
						}
						else if (ptr2->Position.X < min->X)
						{
							min->X = ptr2->Position.X;
						}
						if (ptr2->Position.Y > max->Y)
						{
							max->Y = ptr2->Position.Y;
						}
						else if (ptr2->Position.Y < min->Y)
						{
							min->Y = ptr2->Position.Y;
						}
						if (ptr2->Position.Z > max->Z)
						{
							max->Z = ptr2->Position.Z;
						}
						else if (ptr2->Position.Z < min->Z)
						{
							min->Z = ptr2->Position.Z;
						}
						ptr2++;
					}
				}
			}
		}
		param_0.Indices = null;
		param_0.SetVertexBuffer(null);
		l8_u0013_ = param_3 * 4;
		l8l.SetData(param_1, 0, l8_u0013_);
		l8N.SetData(param_2, 0, param_3 * 6);
		param_3 = 0;
	}

	public void Dispose()
	{
		if (l8N != null)
		{
			l8N.Dispose();
			l8N = null;
		}
		if (l8l != null)
		{
			l8l.Dispose();
			l8l = null;
		}
	}
}
