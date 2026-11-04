using System;
using System.Collections.Generic;
using L;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Rendering;

namespace h;

internal class _0003 : IComparer<RenderableMesh>
{
	public int Compare(RenderableMesh a, RenderableMesh b)
	{
		if (a == b)
		{
			return 0;
		}
		if (a == null)
		{
			return -1;
		}
		if (b == null)
		{
			return 1;
		}
		return a.fNm - b.fNm;
	}
}
internal class _0019
{
	internal bool fNB = true;

	internal bool fNa;

	internal bool fNg;

	internal bool fNf;

	internal bool fN_0003;

	internal Effect fNn;

	private List<RenderableMesh> fN_0019 = new List<RenderableMesh>(32);

	internal List<RenderableMesh> Objects => fN_0019;

	internal void _0013()
	{
		fNB = true;
		fNa = false;
		fNg = false;
		fNf = false;
		fN_0003 = false;
		fNn = null;
		fN_0019.Clear();
	}
}
internal class _0012
{
	protected class nNB
	{
		internal SystemStatistic fNB = SystemConsole.GetStatistic("Renderer_BatchSamplerChanges", SystemStatisticCategory.Rendering);
	}

	protected nNB Statistics = new nNB();

	private bool fNB = true;

	private SamplerState fNa;

	private static Dictionary<int, SamplerState> fNg = new Dictionary<int, SamplerState>(16);

	internal void _00034()
	{
		fNB = true;
	}

	internal static void _0003T(GraphicsDevice P_0)
	{
		for (int k = 0; k < 16; k++)
		{
			P_0.SamplerStates[k] = SamplerState.PointClamp;
		}
		for (int l = 0; l < 2; l++)
		{
			P_0.VertexSamplerStates[l] = SamplerState.PointWrap;
		}
	}

	internal void _0003z(GraphicsDevice P_0, SamplerState P_1)
	{
		if (!fNB && fNa == P_1)
		{
			return;
		}
		Statistics.fNB.AccumulationValue++;
		fNB = false;
		fNa = P_1;
		for (int k = 0; k < 16; k++)
		{
			Texture texture = P_0.Textures[k];
			if (texture != null && texture.Format >= SurfaceFormat.Single)
			{
				P_0.SamplerStates[k] = SamplerState.PointClamp;
			}
			else
			{
				P_0.SamplerStates[k] = P_1;
			}
		}
		for (int l = 0; l < 2; l++)
		{
			P_0.VertexSamplerStates[l] = SamplerState.PointWrap;
		}
	}

	internal SamplerState _0003_0010(GraphicsDevice P_0, TextureAddressMode P_1, TextureAddressMode P_2, TextureAddressMode P_3, TextureFilter P_4, int P_5)
	{
		int key = (int)(P_1 + ((int)P_2 << 2) + ((int)P_3 << 4) + ((int)P_4 << 6) + (P_5 << 10));
		if (!fNg.TryGetValue(key, out var value))
		{
			value = new SamplerState();
			value.AddressU = P_1;
			value.AddressV = P_2;
			value.AddressW = P_3;
			value.Filter = P_4;
			value.MaxAnisotropy = P_5;
			fNg.Add(key, value);
		}
		return value;
	}
}
internal class _0015
{
	protected class nNB
	{
		public SystemStatistic BatchVertexBufferChanges = SystemConsole.GetStatistic("Renderer_BatchVertexBufferChanges", SystemStatisticCategory.Rendering);
	}

	protected nNB Statistics = new nNB();

	private RenderableMesh fNB;

	internal void _00034()
	{
		fNB = null;
	}

	internal void h(GraphicsDevice P_0, RenderableMesh P_1)
	{
		if (fNB == null || fNB.fNj != P_1.fNj || fNB.fNo != P_1.fNo)
		{
			P_0.SetVertexBuffer(P_1.fNj, P_1.fNo);
			Statistics.BatchVertexBufferChanges.AccumulationValue++;
		}
		if (fNB == null || fNB.fN_0005 != P_1.fN_0005)
		{
			P_0.Indices = P_1.fN_0005;
		}
		fNB = P_1;
	}
}
internal class _0005
{
	internal const int fNB = 1024;

	internal const int fNa = 4;

	internal const int fNg = 6;

	private Vector2[] fNf = new Vector2[4]
	{
		new Vector2(0f, 0f),
		new Vector2(1f, 0f),
		new Vector2(0f, 1f),
		new Vector2(1f, 1f)
	};

	private Effect fN_0003;

	private GraphicsDevice fNn;

	private int fN_0019;

	private _8[] fNL = new _8[4096];

	private i fNv;

	private List<i> fN_0012 = new List<i>(32);

	private global::L._0005<i> fN_0015;

	private static ushort[] fN8;

	internal Effect Effect => fN_0003;

	internal List<i> Buffers => fN_0012;

	static _0005()
	{
		fN8 = new ushort[6144];
		int num = 0;
		for (int k = 0; k < 1024; k++)
		{
			int num2 = k * 4;
			fN8[num++] = (ushort)num2;
			fN8[num++] = (ushort)(num2 + 1);
			fN8[num++] = (ushort)(num2 + 2);
			fN8[num++] = (ushort)(num2 + 2);
			fN8[num++] = (ushort)(num2 + 1);
			fN8[num++] = (ushort)(num2 + 3);
		}
	}

	internal _0005(GraphicsDevice P_0, global::L._0005<i> P_1, Effect P_2)
	{
		fNn = P_0;
		fN_0015 = P_1;
		fN_0003 = P_2;
		for (int k = 0; k < fNL.Length; k++)
		{
			fNL[k].Normal = new Vector3(0f, 0f, -1f);
		}
	}

	internal unsafe void k(ref Vector2 P_0, ref Vector2 P_1, float P_2, ref Vector2 P_3, ref Vector2 P_4, ref Vector2 P_5, float P_6)
	{
		if (fNv == null || fN_0019 >= 1024)
		{
			_0003b();
			_0003_0018();
		}
		int num = fN_0019 * 4;
		if (num + 4 > fNL.Length)
		{
			throw new Exception("Unable to build sprite, vertex array to small for all vertices.");
		}
		bool flag = P_2 != 0f;
		float num2;
		float num3;
		if (flag)
		{
			num2 = (float)Math.Sin(P_2);
			num3 = (float)Math.Cos(P_2);
		}
		else
		{
			num2 = 0f;
			num3 = 1f;
		}
		fixed (_8* ptr = &fNL[num])
		{
			fixed (Vector2* ptr2 = fNf)
			{
				_8* ptr3 = ptr;
				Vector2* ptr4 = ptr2;
				float num4 = 0.5f - P_3.X;
				float num5 = 0.5f - P_3.Y;
				for (int k = 0; k < 4; k++)
				{
					ptr3->TextureCoordinate.X = ptr4->X * P_4.X + P_5.X;
					ptr3->TextureCoordinate.Y = ptr4->Y * P_4.Y + P_5.Y;
					float num6 = (ptr4->X - num4) * P_0.X;
					float num7 = (ptr4->Y - num5) * P_0.Y;
					if (flag)
					{
						float num8 = num6 * num3 - num7 * num2;
						num7 = num6 * num2 + num7 * num3;
						num6 = num8;
					}
					ptr3->Position.X = num6 + P_1.X;
					ptr3->Position.Y = num7 + P_1.Y;
					ptr3->Position.Z = P_6;
					ptr3->Binormal.X = 0f - num2;
					ptr3->Binormal.Y = num3;
					ptr3->Tangent.X = num3;
					ptr3->Tangent.Y = num2;
					ptr3++;
					ptr4++;
				}
			}
		}
		fN_0019++;
	}

	internal void _0003b()
	{
		if (fNv != null && fN_0019 >= 1)
		{
			fNv._0003b(fNn, fNL, fN8, fN_0019);
		}
	}

	private void _0003_0018()
	{
		fNv = fN_0015.New();
		fN_0012.Add(fNv);
		fN_0019 = 0;
	}

	internal void _0013()
	{
		foreach (i item in fN_0012)
		{
			fN_0015.Free(item);
		}
		fNv = null;
		fN_0012.Clear();
		fN_0019 = 0;
	}
}
