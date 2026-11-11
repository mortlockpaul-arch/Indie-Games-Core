using System;
using System.Collections.Generic;
using B;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using y;

namespace _u0019_;

internal class _u0013_ : IDisposable
{
	private class w8a : IComparer<w8_u0013_>
	{
		public int Compare(w8_u0013_ a, w8_u0013_ b)
		{
			return b.Section.Height - a.Section.Height;
		}
	}

	private struct w8_u0013_
	{
		public int Index;

		public Rectangle Section;
	}

	private global::B.B<int, Rectangle> l8a = new global::B.B<int, Rectangle>(128u);

	private RenderTarget2D l8_u0013_;

	private Rectangle[] l8N = new Rectangle[2];

	private int l8l;

	private int l8L;

	private List<Rectangle> l8w = new List<Rectangle>(8);

	private List<Rectangle> l8J = new List<Rectangle>(8);

	private w8a l8B = new w8a();

	private static List<w8_u0013_> l8s = new List<w8_u0013_>();

	internal RenderTarget2D RenderTarget => l8_u0013_;

	internal _u0013_(GraphicsDevice param_0, int param_1, SurfaceFormat param_2)
	{
		l8_u0013_ = new RenderTarget2D(param_0, param_1, param_1, mipMap: false, param_2, DepthFormat.Depth24Stencil8, 0, SunBurnCoreSystem.Instance.GetBestRenderTargetUsage());
		int num = param_1 / 2;
		ref Rectangle reference = ref l8N[0];
		reference = new Rectangle(0, 0, num, param_1);
		ref Rectangle reference2 = ref l8N[1];
		reference2 = new Rectangle(num, 0, num, param_1);
		l8l = param_1 * param_1;
		ww(l8N[0]);
		ww(l8N[1]);
		_u0004_();
	}

	internal void wa()
	{
		l8a._id_3();
		ww(l8N[0]);
		ww(l8N[1]);
		_u0004_();
	}

	public void Dispose()
	{
		wa();
		y.a.w_u0005_(ref l8_u0013_);
	}

	internal bool w_u0013_()
	{
		return wl() >= l8l;
	}

	private int wN(Rectangle param_0)
	{
		return Math.Min(param_0.Width, param_0.Height);
	}

	internal int wl()
	{
		int num = 0;
		for (global::B.B<int, Rectangle> b = l8a._u0013_v(); b != null; b = b.l8N)
		{
			foreach (Rectangle item in b.l8L)
			{
				num += item.Width * item.Height;
			}
		}
		return num;
	}

	private void _u0004_()
	{
		l8w.Clear();
		l8J.Clear();
		l8L = wl();
	}

	private void wL()
	{
		foreach (Rectangle item in l8J)
		{
			wJ(item);
		}
		foreach (Rectangle item2 in l8w)
		{
			int num = wN(item2);
			global::B.B<int, Rectangle> b = l8a._u0013__u0005_(num, (uint)num);
			b.l8L.Remove(item2);
		}
		l8w.Clear();
		l8J.Clear();
		int num2 = wl();
		if (num2 != l8L)
		{
			throw new Exception("Unable to rollback shadow cache data.");
		}
	}

	private void ww(Rectangle param_0)
	{
		if (param_0.Width >= 1 && param_0.Height >= 1)
		{
			wJ(param_0);
			l8w.Add(param_0);
		}
	}

	private void wJ(Rectangle param_0)
	{
		if (param_0.Width >= 1 && param_0.Height >= 1)
		{
			int num = wN(param_0);
			global::B.B<int, Rectangle> b = l8a._u0013__u0005_(num, (uint)num);
			b.l8L.Add(param_0);
		}
	}

	private bool wB(int param_0, ref Rectangle param_1)
	{
		if (param_0 < 1)
		{
			return false;
		}
		for (global::B.B<int, Rectangle> b = l8a._u0013__u0005_(param_0, (uint)param_0); b != null; b = b.l8N)
		{
			for (int i = 0; i < b.l8L.Count; i++)
			{
				Rectangle item = b.l8L[i];
				if (item.Width >= param_0)
				{
					b.l8L.RemoveAt(i);
					l8J.Add(item);
					Rectangle param_2 = default;
					Rectangle param_3 = default;
					param_2.X = item.X + param_0;
					param_2.Y = item.Y;
					param_2.Width = item.Width - param_0;
					param_2.Height = param_0;
					ww(param_2);
					param_3.X = item.X;
					param_3.Y = item.Y + param_0;
					param_3.Width = item.Width;
					param_3.Height = item.Height - param_0;
					ww(param_3);
					param_1.X = item.X;
					param_1.Y = item.Y;
					param_1.Width = param_0;
					param_1.Height = param_0;
					return true;
				}
			}
		}
		return false;
	}

	internal bool ws(List<Rectangle> param_0)
	{
		l8s.Clear();
		for (int i = 0; i < param_0.Count; i++)
		{
			w8_u0013_ item = new w8_u0013_
			{
				Index = i,
				Section = param_0[i]
			};
			l8s.Add(item);
		}
		l8s.Sort(l8B);
		for (int j = 0; j < l8s.Count; j++)
		{
			w8_u0013_ w8_u0013_2 = l8s[j];
			if (wB(w8_u0013_2.Section.Height, ref w8_u0013_2.Section))
			{
				param_0[w8_u0013_2.Index] = w8_u0013_2.Section;
				continue;
			}
			wL();
			return false;
		}
		_u0004_();
		return true;
	}
}
