using System;
using System.Collections.Generic;
using _0013;
using L;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;

namespace _9;

internal class a : IDisposable
{
	private class nNB : IComparer<nNa>
	{
		public int Compare(nNa a, nNa b)
		{
			return b.Section.Height - a.Section.Height;
		}
	}

	private struct nNa
	{
		public int Index;

		public Rectangle Section;
	}

	private L._8<int, Rectangle> fNB = new L._8<int, Rectangle>(128u);

	private RenderTarget2D fNa;

	private Rectangle[] fNg = new Rectangle[2];

	private int fNf;

	private int fN_0003;

	private List<Rectangle> fNn = new List<Rectangle>(8);

	private List<Rectangle> fN_0019 = new List<Rectangle>(8);

	private nNB fNL = new nNB();

	private static List<nNa> fNv = new List<nNa>();

	internal RenderTarget2D RenderTarget => fNa;

	internal a(GraphicsDevice P_0, int P_1, SurfaceFormat P_2)
	{
		fNa = new RenderTarget2D(P_0, P_1, P_1, mipMap: false, P_2, DepthFormat.Depth24Stencil8, 0, LightingSystemManager.Instance.GetBestRenderTargetUsage());
		int num = P_1 / 2;
		ref Rectangle reference = ref fNg[0];
		reference = new Rectangle(0, 0, num, P_1);
		ref Rectangle reference2 = ref fNg[1];
		reference2 = new Rectangle(num, 0, num, P_1);
		fNf = P_1 * P_1;
		nW(fNg[0]);
		nW(fNg[1]);
		h();
	}

	internal void nG()
	{
		fNB._0013();
		nW(fNg[0]);
		nW(fNg[1]);
		h();
	}

	public void Dispose()
	{
		nG();
		_0013.B.nu(ref fNa);
	}

	internal bool n_0017()
	{
		return nh() >= fNf;
	}

	private int nm(Rectangle P_0)
	{
		return Math.Min(P_0.Width, P_0.Height);
	}

	internal int nh()
	{
		int num = 0;
		for (L._8<int, Rectangle> obj = fNB.a_0001(); obj != null; obj = obj.fNg)
		{
			foreach (Rectangle item in obj.fN_0003)
			{
				num += item.Width * item.Height;
			}
		}
		return num;
	}

	private void h()
	{
		fNn.Clear();
		fN_0019.Clear();
		fN_0003 = nh();
	}

	private void nR()
	{
		foreach (Rectangle item in fN_0019)
		{
			n9(item);
		}
		foreach (Rectangle item2 in fNn)
		{
			int num = nm(item2);
			L._8<int, Rectangle> obj = fNB.a_0018(num, (uint)num);
			obj.fN_0003.Remove(item2);
		}
		fNn.Clear();
		fN_0019.Clear();
		int num2 = nh();
		if (num2 != fN_0003)
		{
			throw new Exception("Unable to rollback shadow cache data.");
		}
	}

	private void nW(Rectangle P_0)
	{
		if (P_0.Width >= 1 && P_0.Height >= 1)
		{
			n9(P_0);
			fNn.Add(P_0);
		}
	}

	private void n9(Rectangle P_0)
	{
		if (P_0.Width >= 1 && P_0.Height >= 1)
		{
			int num = nm(P_0);
			L._8<int, Rectangle> obj = fNB.a_0018(num, (uint)num);
			obj.fN_0003.Add(P_0);
		}
	}

	private bool n_0013(int P_0, ref Rectangle P_1)
	{
		if (P_0 < 1)
		{
			return false;
		}
		for (L._8<int, Rectangle> obj = fNB.a_0018(P_0, (uint)P_0); obj != null; obj = obj.fNg)
		{
			for (int k = 0; k < obj.fN_0003.Count; k++)
			{
				Rectangle item = obj.fN_0003[k];
				if (item.Width >= P_0)
				{
					obj.fN_0003.RemoveAt(k);
					fN_0019.Add(item);
					Rectangle rectangle = default;
					Rectangle rectangle2 = default;
					rectangle.X = item.X + P_0;
					rectangle.Y = item.Y;
					rectangle.Width = item.Width - P_0;
					rectangle.Height = P_0;
					nW(rectangle);
					rectangle2.X = item.X;
					rectangle2.Y = item.Y + P_0;
					rectangle2.Width = item.Width;
					rectangle2.Height = item.Height - P_0;
					nW(rectangle2);
					P_1.X = item.X;
					P_1.Y = item.Y;
					P_1.Width = P_0;
					P_1.Height = P_0;
					return true;
				}
			}
		}
		return false;
	}

	internal bool nq(List<Rectangle> P_0)
	{
		fNv.Clear();
		for (int k = 0; k < P_0.Count; k++)
		{
			nNa item = new nNa
			{
				Index = k,
				Section = P_0[k]
			};
			fNv.Add(item);
		}
		fNv.Sort(fNL);
		for (int l = 0; l < fNv.Count; l++)
		{
			nNa nNa2 = fNv[l];
			if (n_0013(nNa2.Section.Height, ref nNa2.Section))
			{
				P_0[nNa2.Index] = nNa2.Section;
				continue;
			}
			nR();
			return false;
		}
		h();
		return true;
	}
}
