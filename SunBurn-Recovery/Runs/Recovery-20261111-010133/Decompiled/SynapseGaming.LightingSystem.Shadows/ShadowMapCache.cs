using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using _u0019_;

namespace SynapseGaming.LightingSystem.Shadows;

public class ShadowMapCache
{
	private class w8a
	{
		public SurfaceFormat Format;

		public int TexelBytes;

		public w8a(SurfaceFormat format, int texelbytes)
		{
			Format = format;
			TexelBytes = texelbytes;
		}
	}

	internal class w8_u0013_
	{
		internal SystemStatistic l8a = SystemConsole.GetStatistic("Shadow_TotalPages", SystemStatisticCategory.Shadowing);

		internal SystemStatistic l8_u0013_ = SystemConsole.GetStatistic("Shadow_TotalMemoryUsage", SystemStatisticCategory.Shadowing);

		internal SystemStatistic l8N = SystemConsole.GetStatistic("Shadow_ActivePages", SystemStatisticCategory.Shadowing);

		internal SystemStatistic l8l = SystemConsole.GetStatistic("Shadow_ActiveMemoryUsage", SystemStatisticCategory.Shadowing);
	}

	private w8a[] l8a = new w8a[5]
	{
		new w8a(SurfaceFormat.Single, 4),
		new w8a(SurfaceFormat.HalfSingle, 2),
		new w8a(SurfaceFormat.HalfVector2, 4),
		new w8a(SurfaceFormat.Single, 4),
		new w8a(SurfaceFormat.Color, 4)
	};

	private SurfaceFormat l8_u0013_ = SurfaceFormat.Single;

	private int l8N;

	private int l8l;

	private int l8L = 2048;

	private int l8w;

	private bool l8J = true;

	private IGraphicsDeviceService l8B;

	private List<_u0019_._u0013_> l8s = new List<_u0019_._u0013_>(8);

	internal w8_u0013_ l8h = new w8_u0013_();

	public int MaxMemoryUsage => l8w;

	public bool PreferHalfFloatTextureFormat => l8J;

	public int PageSize => l8L;

	public ShadowMapCache(int pagesize, int maxmemoryusage, bool preferhalffloat)
	{
		l8B = SunBurnCoreSystem.Instance.GraphicsDeviceManager;
		Resize(pagesize, maxmemoryusage, preferhalffloat);
	}

	public void Resize(int pagesize, int maxmemoryusage, bool preferhalffloat)
	{
		Unload();
		l8L = pagesize;
		l8w = maxmemoryusage;
		l8J = preferhalffloat;
		l8l = 0;
	}

	private void wh()
	{
		if (l8l > 0)
		{
			return;
		}
		GraphicsDeviceSupport graphicsDeviceSupport = SunBurnCoreSystem.Instance.GetGraphicsDeviceSupport();
		int num = 0;
		if (l8J)
		{
			num = 1;
		}
		for (int i = num; i < l8a.Length; i++)
		{
			w8a w8a2 = l8a[i];
			if (graphicsDeviceSupport.SurfaceFormat[w8a2.Format])
			{
				l8_u0013_ = w8a2.Format;
				l8N = w8a2.TexelBytes;
				l8l = l8L * l8L * l8N;
				break;
			}
		}
		if (l8l >= 1)
		{
			return;
		}
		throw new Exception("Unable to find a valid shadow buffer render target format.");
	}

	public RenderTarget2D ReserveSections(List<Rectangle> sectionsizes)
	{
		wh();
		int num = 0;
		foreach (Rectangle sectionsize in sectionsizes)
		{
			num += sectionsize.Width * sectionsize.Height;
		}
		if (num > l8L * l8L)
		{
			return null;
		}
		foreach (_u0019_._u0013_ l in l8s)
		{
			if (l.ws(sectionsizes))
			{
				wv(sectionsizes);
				return l.RenderTarget;
			}
			if (l.w_u0013_())
			{
				return null;
			}
		}
		if (l8s.Count > 0 && (l8s.Count + 1) * l8l > l8w)
		{
			return null;
		}
		_u0019_._u0013_ u0013_ = new _u0019_._u0013_(l8B.GraphicsDevice, l8L, l8_u0013_);
		l8s.Add(u0013_);
		if (u0013_.ws(sectionsizes))
		{
			wv(sectionsizes);
			return u0013_.RenderTarget;
		}
		return null;
	}

	internal float w_u0002_()
	{
		if (l8s.Count < 1)
		{
			return 0f;
		}
		int num = 0;
		foreach (_u0019_._u0013_ l in l8s)
		{
			if (!l.w_u0013_())
			{
				num++;
			}
		}
		return (float)num / (float)l8s.Count;
	}

	private void wv(List<Rectangle> param_0)
	{
		l8h.l8a.AccumulationValue = l8s.Count;
		l8h.l8_u0013_.AccumulationValue = l8s.Count * l8l;
		l8h.l8N.AccumulationValue = 0;
		l8h.l8l.AccumulationValue = 0;
		foreach (_u0019_._u0013_ l in l8s)
		{
			if (!l.w_u0013_())
			{
				l8h.l8N.AccumulationValue++;
				l8h.l8l.AccumulationValue += l8l;
			}
		}
	}

	public void ClearReserves()
	{
		foreach (_u0019_._u0013_ l in l8s)
		{
			l.wa();
		}
	}

	public void Unload()
	{
		foreach (_u0019_._u0013_ l in l8s)
		{
			l.Dispose();
		}
		l8s.Clear();
	}
}
