using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace h;

internal class i : IDisposable
{
	private BoundingBox fNB = default;

	private int fNa;

	private IndexBuffer fNg;

	private VertexBuffer fNf;

	internal BoundingBox ObjectBoundingBox => fNB;

	internal int VertexCount => fNa;

	internal IndexBuffer IndexBuffer => fNg;

	internal VertexBuffer VertexBuffer => fNf;

	internal unsafe void _0003b(GraphicsDevice P_0, _8[] P_1, ushort[] P_2, int P_3)
	{
		if (fNf == null)
		{
			fNf = new VertexBuffer(P_0, typeof(_8), 4096, BufferUsage.None);
			fNg = new IndexBuffer(P_0, typeof(ushort), 6144, BufferUsage.None);
		}
		fixed (_8* ptr = P_1)
		{
			fixed (Vector3* max = &fNB.Max)
			{
				fixed (Vector3* min = &fNB.Min)
				{
					int num = P_3 * 4;
					_8* ptr2 = ptr;
					min->X = (max->X = ptr2->Position.X);
					min->Y = (max->Y = ptr2->Position.Y);
					max->Z = 1f;
					min->Z = 0f;
					ptr2++;
					for (int j = 1; j < num; j++)
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
						ptr2++;
					}
				}
			}
		}
		P_0.Indices = null;
		P_0.SetVertexBuffer(null);
		fNa = P_3 * 4;
		fNf.SetData(P_1, 0, fNa);
		fNg.SetData(P_2, 0, P_3 * 6);
		P_3 = 0;
	}

	public void Dispose()
	{
		if (fNg != null)
		{
			fNg.Dispose();
			fNg = null;
		}
		if (fNf != null)
		{
			fNf.Dispose();
			fNf = null;
		}
	}
}
