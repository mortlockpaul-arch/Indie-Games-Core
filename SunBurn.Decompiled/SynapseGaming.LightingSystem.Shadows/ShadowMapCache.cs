using System;
using System.Collections.Generic;
using _9;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;

namespace SynapseGaming.LightingSystem.Shadows;

/// <summary>
/// Class that manages render target sections used for shadow mapping.
/// </summary>
public class ShadowMapCache
{
	private class nNB
	{
		public SurfaceFormat Format;

		public int TexelBytes;

		public nNB(SurfaceFormat format, int texelbytes)
		{
			Format = format;
			TexelBytes = texelbytes;
		}
	}

	internal class nNa
	{
		internal SystemStatistic fNB = SystemConsole.GetStatistic("Shadow_TotalPages", SystemStatisticCategory.Shadowing);

		internal SystemStatistic fNa = SystemConsole.GetStatistic("Shadow_TotalMemoryUsage", SystemStatisticCategory.Shadowing);

		internal SystemStatistic fNg = SystemConsole.GetStatistic("Shadow_ActivePages", SystemStatisticCategory.Shadowing);

		internal SystemStatistic fNf = SystemConsole.GetStatistic("Shadow_ActiveMemoryUsage", SystemStatisticCategory.Shadowing);
	}

	private nNB[] fNB = new nNB[5]
	{
		new nNB(SurfaceFormat.Single, 4),
		new nNB(SurfaceFormat.HalfSingle, 2),
		new nNB(SurfaceFormat.HalfVector2, 4),
		new nNB(SurfaceFormat.Single, 4),
		new nNB(SurfaceFormat.Color, 4)
	};

	private SurfaceFormat fNa = SurfaceFormat.Single;

	private int fNg;

	private int fNf;

	private int fN_0003 = 2048;

	private int fNn;

	private bool fN_0019 = true;

	private IGraphicsDeviceService fNL;

	private List<_9.a> fNv = new List<_9.a>(8);

	internal nNa fN_0012 = new nNa();

	/// <summary>
	/// Maximum amount of memory the cache is allowed to consume. This is an
	/// approximate value and the cache may use more memory in certain instances.
	/// </summary>
	public int MaxMemoryUsage => fNn;

	/// <summary>
	/// True when smaller half-float format render targets are preferred. These
	/// formats consume less memory and generally perform better, but have lower
	/// accuracy on directional lights.
	/// </summary>
	public bool PreferHalfFloatTextureFormat => fN_0019;

	/// <summary>
	/// Size in pixels of each render target (page) in the cache. For a size of 1024
	/// the actual page dimensions are 1024x1024. Small sizes can reduce performance by
	/// fragmenting the shadow maps, and reduce shadow quality by lowering the maximum
	/// resolution of each shadow map section.
	/// </summary>
	public int PageSize => fN_0003;

	/// <summary>
	/// Creates a new ShadowMapCache instance.
	/// </summary>
	/// <param name="graphicsdevicemanager"></param>
	/// <param name="pagesize">Size in pixels of each render target (page) in the cache.
	/// For a size of 1024 the actual page dimensions are 1024x1024. Small sizes can reduce
	/// performance by fragmenting the shadow maps, and reduce shadow quality by lowering
	/// the maximum resolution of each shadow map section.</param>
	/// <param name="maxmemoryusage">Maximum amount of memory the cache is allowed to consume.
	/// This is an approximate value and the cache may use more memory in certain instances.</param>
	/// <param name="preferhalffloat">True when smaller half-float format render targets are
	/// preferred. These formats consume less memory and generally perform better, but have
	/// lower accuracy on directional lights.</param>
	public ShadowMapCache(IGraphicsDeviceService graphicsdevicemanager, int pagesize, int maxmemoryusage, bool preferhalffloat)
	{
		fNL = graphicsdevicemanager;
		Resize(pagesize, maxmemoryusage, preferhalffloat);
	}

	/// <summary>
	/// Resizes shadow maps and memory usage.
	/// </summary>
	/// <param name="pagesize">Size in pixels of each render target (page) in the cache.
	/// For a size of 1024 the actual page dimensions are 1024x1024. Small sizes can reduce
	/// performance by fragmenting the shadow maps, and reduce shadow quality by lowering
	/// the maximum resolution of each shadow map section.</param>
	/// <param name="maxmemoryusage">Maximum amount of memory the cache is allowed to consume.
	/// This is an approximate value and the cache may use more memory in certain instances.</param>
	/// <param name="preferhalffloat">True when smaller half-float format render targets are
	/// preferred. These formats consume less memory and generally perform better, but have
	/// lower accuracy on directional lights.</param>
	public void Resize(int pagesize, int maxmemoryusage, bool preferhalffloat)
	{
		Unload();
		fN_0003 = pagesize;
		fNn = maxmemoryusage;
		fN_0019 = preferhalffloat;
		fNf = 0;
	}

	private void n_000F()
	{
		if (fNf > 0)
		{
			return;
		}
		GraphicsDeviceSupport graphicsDeviceSupport = LightingSystemManager.Instance.GetGraphicsDeviceSupport(fNL.GraphicsDevice);
		int num = 0;
		if (fN_0019)
		{
			num = 1;
		}
		for (int i = num; i < fNB.Length; i++)
		{
			nNB nNB2 = fNB[i];
			if (graphicsDeviceSupport.SurfaceFormat[nNB2.Format])
			{
				fNa = nNB2.Format;
				fNg = nNB2.TexelBytes;
				fNf = fN_0003 * fN_0003 * fNg;
				break;
			}
		}
		if (fNf >= 1)
		{
			return;
		}
		throw new Exception("Unable to find a valid shadow buffer render target format.");
	}

	/// <summary>
	/// Attempts to reserve the requested shadow map sections in a
	/// single render target. If successful the render target is
	/// returned, otherwise null is returned.
	/// </summary>
	/// <param name="sectionsizes"></param>
	/// <returns></returns>
	public RenderTarget2D ReserveSections(List<Rectangle> sectionsizes)
	{
		n_000F();
		int num = 0;
		foreach (Rectangle sectionsize in sectionsizes)
		{
			num += sectionsize.Width * sectionsize.Height;
		}
		if (num > fN_0003 * fN_0003)
		{
			return null;
		}
		foreach (_9.a item in fNv)
		{
			if (item.nq(sectionsizes))
			{
				nr(sectionsizes);
				return item.RenderTarget;
			}
			if (item.n_0017())
			{
				return null;
			}
		}
		if (fNv.Count > 0 && (fNv.Count + 1) * fNf > fNn)
		{
			return null;
		}
		_9.a a2 = new _9.a(fNL.GraphicsDevice, fN_0003, fNa);
		fNv.Add(a2);
		if (a2.nq(sectionsizes))
		{
			nr(sectionsizes);
			return a2.RenderTarget;
		}
		return null;
	}

	internal float n_0014()
	{
		if (fNv.Count < 1)
		{
			return 0f;
		}
		int num = 0;
		foreach (_9.a item in fNv)
		{
			if (!item.n_0017())
			{
				num++;
			}
		}
		return (float)num / (float)fNv.Count;
	}

	private void nr(List<Rectangle> P_0)
	{
		fN_0012.fNB.AccumulationValue = fNv.Count;
		fN_0012.fNa.AccumulationValue = fNv.Count * fNf;
		fN_0012.fNg.AccumulationValue = 0;
		fN_0012.fNf.AccumulationValue = 0;
		foreach (_9.a item in fNv)
		{
			if (!item.n_0017())
			{
				fN_0012.fNg.AccumulationValue++;
				fN_0012.fNf.AccumulationValue += fNf;
			}
		}
	}

	/// <summary>
	/// Clears all reserved shadow map sections, allowing the sections to be reused
	/// in future shadow maps section requests.
	/// </summary>
	public void ClearReserves()
	{
		foreach (_9.a item in fNv)
		{
			item.nG();
		}
	}

	/// <summary>
	/// Disposes any graphics resources used internally by this object, and clears
	/// all reserved shadow map sections. Commonly used during Game.UnloadContent.
	/// </summary>
	public void Unload()
	{
		foreach (_9.a item in fNv)
		{
			item.Dispose();
		}
		fNv.Clear();
	}
}
