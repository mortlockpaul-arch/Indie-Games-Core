using System;
using System.Collections.Generic;
using B;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace x;

internal class _u0005_
{
	internal const int l8a = 1024;

	internal const int l8_u0013_ = 4;

	internal const int l8N = 6;

	private Vector2[] l8l = new Vector2[4]
	{
		new Vector2(0f, 0f),
		new Vector2(1f, 0f),
		new Vector2(0f, 1f),
		new Vector2(1f, 1f)
	};

	private Effect l8L;

	private GraphicsDevice l8w;

	private int l8J;

	private v[] l8B = new v[4096];

	private Y l8s;

	private List<Y> l8h = new List<Y>(32);

	private global::B._u0002_<Y> l8_u0002_;

	private static ushort[] l8v;

	internal Effect Effect => l8L;

	internal List<Y> Buffers => l8h;

	static _u0005_()
	{
		l8v = new ushort[6144];
		int num = 0;
		for (int i = 0; i < 1024; i++)
		{
			int num2 = i * 4;
			l8v[num++] = (ushort)num2;
			l8v[num++] = (ushort)(num2 + 1);
			l8v[num++] = (ushort)(num2 + 2);
			l8v[num++] = (ushort)(num2 + 2);
			l8v[num++] = (ushort)(num2 + 1);
			l8v[num++] = (ushort)(num2 + 3);
		}
	}

	internal _u0005_(GraphicsDevice param_0, global::B._u0002_<Y> param_1, Effect param_2)
	{
		l8w = param_0;
		l8_u0002_ = param_1;
		l8L = param_2;
		for (int i = 0; i < l8B.Length; i++)
		{
			l8B[i].Normal = new Vector3(0f, 0f, -1f);
		}
	}

	internal unsafe void _u0006_(ref Vector2 param_0, ref Vector2 param_1, float param_2, ref Vector2 param_3, ref Vector2 param_4, ref Vector2 param_5, float param_6)
	{
		if (l8s == null || l8J >= 1024)
		{
			L_u0001_();
			L9();
		}
		int num = l8J * 4;
		if (num + 4 > l8B.Length)
		{
			throw new Exception("Unable to build sprite, vertex array to small for all vertices.");
		}
		bool flag = param_2 != 0f;
		float num2;
		float num3;
		if (flag)
		{
			num2 = (float)Math.Sin(param_2);
			num3 = (float)Math.Cos(param_2);
		}
		else
		{
			num2 = 0f;
			num3 = 1f;
		}
		fixed (v* ptr = &l8B[num])
		{
			fixed (Vector2* ptr2 = l8l)
			{
				v* ptr3 = ptr;
				Vector2* ptr4 = ptr2;
				float num4 = 0.5f - param_3.X;
				float num5 = 0.5f - param_3.Y;
				for (int i = 0; i < 4; i++)
				{
					ptr3->TextureCoordinate.X = ptr4->X * param_4.X + param_5.X;
					ptr3->TextureCoordinate.Y = ptr4->Y * param_4.Y + param_5.Y;
					float num6 = (ptr4->X - num4) * param_0.X;
					float num7 = (ptr4->Y - num5) * param_0.Y;
					if (flag)
					{
						float num8 = num6 * num3 - num7 * num2;
						num7 = num6 * num2 + num7 * num3;
						num6 = num8;
					}
					ptr3->Position.X = num6 + param_1.X;
					ptr3->Position.Y = num7 + param_1.Y;
					ptr3->Position.Z = param_6;
					ptr3->Binormal.X = 0f - num2;
					ptr3->Binormal.Y = num3;
					ptr3->Tangent.X = num3;
					ptr3->Tangent.Y = num2;
					ptr3++;
					ptr4++;
				}
			}
		}
		l8J++;
	}

	internal void L_u0001_()
	{
		if (l8s != null && l8J >= 1)
		{
			l8s.L_u0001_(l8w, l8B, l8v, l8J);
		}
	}

	private void L9()
	{
		l8s = l8_u0002_.New();
		l8h.Add(l8s);
		l8J = 0;
	}

	internal void _id_3()
	{
		foreach (Y item in l8h)
		{
			l8_u0002_.Free(item);
		}
		l8s = null;
		l8h.Clear();
		l8J = 0;
	}
}
