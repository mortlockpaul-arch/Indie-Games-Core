using System.Collections.Generic;
using B;
using Microsoft.Xna.Framework.Graphics;
using y;

namespace SynapseGaming.LightingSystem.Core;

public class FrameBuffers : IUnloadable
{
	private int l8a;

	private int l8_u0013_;

	private bool l8N;

	private DetailPreference l8l;

	private DetailPreference l8L;

	private IGraphicsDeviceService l8w;

	private B.w l8J;

	private SurfaceFormat[] l8B = new SurfaceFormat[6];

	private Dictionary<int, RenderTarget2D> l8s = new Dictionary<int, RenderTarget2D>(8);

	private FullFrameQuad l8h;

	private Dictionary<string, CustomFrameBufferCollection> l8_u0002_ = new Dictionary<string, CustomFrameBufferCollection>(8);

	public int Width => l8a;

	public int Height => l8_u0013_;

	public DetailPreference PrecisionMode => l8l;

	public DetailPreference LightingRange => l8L;

	public FullFrameQuad FullFrameQuad
	{
		get
		{
			if (l8h == null)
			{
				l8h = new FullFrameQuad(l8w.GraphicsDevice, l8a, l8_u0013_);
			}
			return l8h;
		}
	}

	public FrameBuffers(int customwidth, int customheight, DetailPreference precisionmode, DetailPreference lightingrange)
	{
		l8w = SunBurnCoreSystem.Instance.GraphicsDeviceManager;
		l8N = true;
		l8a = customwidth;
		l8_u0013_ = customheight;
		l8l = precisionmode;
		l8L = lightingrange;
		a();
	}

	public FrameBuffers(DetailPreference precisionmode, DetailPreference lightingrange)
	{
		l8w = SunBurnCoreSystem.Instance.GraphicsDeviceManager;
		l8N = false;
		l8l = precisionmode;
		l8L = lightingrange;
		a();
	}

	private void a()
	{
		if (l8l == DetailPreference.High)
		{
			l8B[0] = SurfaceFormat.Vector2;
			l8B[1] = SurfaceFormat.HalfVector4;
		}
		else
		{
			l8B[0] = SurfaceFormat.HalfVector2;
			l8B[1] = SurfaceFormat.Color;
		}
		if (l8L == DetailPreference.High)
		{
			l8B[2] = SurfaceFormat.HdrBlendable;
			l8B[3] = SurfaceFormat.HdrBlendable;
			l8B[4] = SurfaceFormat.HdrBlendable;
			l8B[5] = SurfaceFormat.HdrBlendable;
		}
		else
		{
			l8B[2] = SurfaceFormat.Rgba1010102;
			l8B[3] = SurfaceFormat.Rgba1010102;
			l8B[4] = SurfaceFormat.Color;
			l8B[5] = SurfaceFormat.Color;
		}
		l8J = new B.w();
	}

	public RenderTarget2D GetBuffer(FrameBufferType buffertype, bool createmissing)
	{
		if (l8s.TryGetValue((int)buffertype, out var value))
		{
			return value;
		}
		if (!createmissing)
		{
			return null;
		}
		GraphicsDevice graphicsDevice = l8w.GraphicsDevice;
		DepthFormat preferredDepthFormat = DepthFormat.None;
		if (buffertype == FrameBufferType.DeferredDepthAndSpecularPower || buffertype == FrameBufferType.DeferredLightingDiffuse)
		{
			preferredDepthFormat = DepthFormat.Depth24Stencil8;
		}
		bool mipMap = false;
		int preferredMultiSampleCount = 0;
		if (buffertype == FrameBufferType.PostProcessing1 || buffertype == FrameBufferType.PostProcessing2)
		{
			preferredDepthFormat = DepthFormat.Depth24Stencil8;
			preferredMultiSampleCount = ((graphicsDevice.PresentationParameters.MultiSampleCount > 0) ? 2 : 0);
		}
		value = new RenderTarget2D(graphicsDevice, l8a, l8_u0013_, mipMap, l8B[(int)buffertype], preferredDepthFormat, preferredMultiSampleCount, RenderTargetUsage.PlatformContents);
		l8s.Add((int)buffertype, value);
		return value;
	}

	public CustomFrameBufferCollection GetCustomFrameBufferCollection(string name, bool createmissing)
	{
		if (l8_u0002_.TryGetValue(name, out var value))
		{
			return value;
		}
		if (!createmissing)
		{
			return null;
		}
		value = new CustomFrameBufferCollection();
		l8_u0002_.Add(name, value);
		return value;
	}

	public void BeginFrameRendering(ISceneState scenestate)
	{
		if (l8J.Changed)
		{
			Unload();
		}
		if (!l8N)
		{
			PresentationParameters presentationParameters = l8w.GraphicsDevice.PresentationParameters;
			if (l8a != presentationParameters.BackBufferWidth || l8_u0013_ != presentationParameters.BackBufferHeight)
			{
				l8a = presentationParameters.BackBufferWidth;
				l8_u0013_ = presentationParameters.BackBufferHeight;
				Unload();
			}
		}
	}

	public void EndFrameRendering()
	{
	}

	public void Unload()
	{
		foreach (KeyValuePair<int, RenderTarget2D> l in l8s)
		{
			l.Value.Dispose();
		}
		l8s.Clear();
		foreach (KeyValuePair<string, CustomFrameBufferCollection> item in l8_u0002_)
		{
			foreach (RenderTarget2D item2 in item.Value)
			{
				item2.Dispose();
			}
		}
		l8_u0002_.Clear();
		y.a.w_u0005_(ref l8h);
	}
}
