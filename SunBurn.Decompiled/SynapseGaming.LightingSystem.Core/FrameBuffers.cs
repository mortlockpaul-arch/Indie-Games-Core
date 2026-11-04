using System.Collections.Generic;
using _0013;
using L;
using Microsoft.Xna.Framework.Graphics;

namespace SynapseGaming.LightingSystem.Core;

/// <summary>
/// Provides automatic creation, storage, and management of shared
/// buffers (render targets) used during rendering.
///
/// These buffers include g-buffers, lighting-buffers, and post
/// processing buffers.
/// </summary>
public class FrameBuffers : IUnloadable
{
	private int fNB;

	private int fNa;

	private bool fNg;

	private DetailPreference fNf;

	private DetailPreference fN_0003;

	private IGraphicsDeviceService fNn;

	private global::L.L fN_0019;

	private SurfaceFormat[] fNL = new SurfaceFormat[6];

	private Dictionary<int, RenderTarget2D> fNv = new Dictionary<int, RenderTarget2D>(8);

	private FullFrameQuad fN_0012;

	private Dictionary<string, CustomFrameBufferCollection> fN_0015 = new Dictionary<string, CustomFrameBufferCollection>(8);

	/// <summary>
	/// Current width of the frame buffers.
	/// </summary>
	public int Width => fNB;

	/// <summary>
	/// Current height of the frame buffers.
	/// </summary>
	public int Height => fNa;

	/// <summary>
	/// Increases visual quality at the cost of performance.
	/// Generally used in visualizations, most games do not need this option.
	/// </summary>
	public DetailPreference PrecisionMode => fNf;

	/// <summary>
	/// Increases lighting quality at the cost of performance.
	/// Adds additional lighting range when using HDR.
	/// </summary>
	public DetailPreference LightingRange => fN_0003;

	/// <summary>
	/// Provides a full frame renderable quad sized specifically for the contained buffers.
	/// </summary>
	public FullFrameQuad FullFrameQuad
	{
		get
		{
			if (fN_0012 == null)
			{
				fN_0012 = new FullFrameQuad(fNn.GraphicsDevice, fNB, fNa);
			}
			return fN_0012;
		}
	}

	/// <summary>
	/// Creates a new FrameBuffers instance.
	/// </summary>
	/// <param name="graphicsdevicemanager"></param>
	/// <param name="customwidth">Custom buffer width.</param>
	/// <param name="customheight">Custom buffer height.</param>
	/// <param name="precisionmode">Increases visual quality at the cost of performance.
	/// Generally used in visualizations, most games do not need this option.</param>
	/// <param name="lightingrange">Increases lighting quality at the cost of performance.
	/// Adds additional lighting range when using HDR.</param>
	public FrameBuffers(IGraphicsDeviceService graphicsdevicemanager, int customwidth, int customheight, DetailPreference precisionmode, DetailPreference lightingrange)
	{
		fNn = graphicsdevicemanager;
		fNg = true;
		fNB = customwidth;
		fNa = customheight;
		fNf = precisionmode;
		fN_0003 = lightingrange;
		B();
	}

	/// <summary>
	/// Creates a new FrameBuffers instance.
	/// </summary>
	/// <param name="graphicsdevicemanager"></param>
	/// <param name="precisionmode">Increases visual quality at the cost of performance.
	/// Generally used in visualizations, most games do not need this option.</param>
	/// <param name="lightingrange">Increases lighting quality at the cost of performance.
	/// Adds additional lighting range when using HDR.</param>
	public FrameBuffers(IGraphicsDeviceService graphicsdevicemanager, DetailPreference precisionmode, DetailPreference lightingrange)
	{
		fNn = graphicsdevicemanager;
		fNg = false;
		fNf = precisionmode;
		fN_0003 = lightingrange;
		B();
	}

	private void B()
	{
		if (fNf == DetailPreference.High)
		{
			fNL[0] = SurfaceFormat.Vector2;
			fNL[1] = SurfaceFormat.HalfVector4;
		}
		else
		{
			fNL[0] = SurfaceFormat.HalfVector2;
			fNL[1] = SurfaceFormat.Color;
		}
		if (fN_0003 == DetailPreference.High)
		{
			fNL[2] = SurfaceFormat.HdrBlendable;
			fNL[3] = SurfaceFormat.HdrBlendable;
			fNL[4] = SurfaceFormat.HdrBlendable;
			fNL[5] = SurfaceFormat.HdrBlendable;
		}
		else
		{
			fNL[2] = SurfaceFormat.Rgba1010102;
			fNL[3] = SurfaceFormat.Rgba1010102;
			fNL[4] = SurfaceFormat.Color;
			fNL[5] = SurfaceFormat.Color;
		}
		fN_0019 = new global::L.L(fNn);
	}

	/// <summary>
	/// Gets one of the common frame buffers (only valid between
	/// calls to BeginFrameRendering and EndFrameRendering).
	/// </summary>
	/// <param name="buffertype"></param>
	/// <param name="createmissing">Determines if the buffer should be created
	/// when it does not exist, otherwise null is returned.</param>
	/// <returns></returns>
	public RenderTarget2D GetBuffer(FrameBufferType buffertype, bool createmissing)
	{
		if (fNv.TryGetValue((int)buffertype, out var value))
		{
			return value;
		}
		if (!createmissing)
		{
			return null;
		}
		GraphicsDevice graphicsDevice = fNn.GraphicsDevice;
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
		value = new RenderTarget2D(graphicsDevice, fNB, fNa, mipMap, fNL[(int)buffertype], preferredDepthFormat, preferredMultiSampleCount, RenderTargetUsage.PlatformContents);
		fNv.Add((int)buffertype, value);
		return value;
	}

	/// <summary>
	/// Gets a collection of implementation specific buffers defined and used by the caller.
	/// </summary>
	/// <param name="name">Unique name of the collection to find.</param>
	/// <param name="createmissing">Determines if the collection should be created if it does not exist.</param>
	/// <returns></returns>
	public CustomFrameBufferCollection GetCustomFrameBufferCollection(string name, bool createmissing)
	{
		if (fN_0015.TryGetValue(name, out var value))
		{
			return value;
		}
		if (!createmissing)
		{
			return null;
		}
		value = new CustomFrameBufferCollection();
		fN_0015.Add(name, value);
		return value;
	}

	/// <summary>
	/// Sets up the object prior to rendering.
	/// </summary>
	/// <param name="scenestate"></param>
	public void BeginFrameRendering(ISceneState scenestate)
	{
		if (fN_0019.Changed)
		{
			Unload();
		}
		if (!fNg)
		{
			PresentationParameters presentationParameters = fNn.GraphicsDevice.PresentationParameters;
			if (fNB != presentationParameters.BackBufferWidth || fNa != presentationParameters.BackBufferHeight)
			{
				fNB = presentationParameters.BackBufferWidth;
				fNa = presentationParameters.BackBufferHeight;
				Unload();
			}
		}
	}

	/// <summary>
	/// Finalizes rendering.
	/// </summary>
	public void EndFrameRendering()
	{
	}

	/// <summary>
	/// Disposes any graphics resource used internally by this object, and removes
	/// scene resources managed by this object. Commonly used during Game.UnloadContent.
	/// </summary>
	public void Unload()
	{
		foreach (KeyValuePair<int, RenderTarget2D> item in fNv)
		{
			item.Value.Dispose();
		}
		fNv.Clear();
		foreach (KeyValuePair<string, CustomFrameBufferCollection> item2 in fN_0015)
		{
			foreach (RenderTarget2D item3 in item2.Value)
			{
				item3.Dispose();
			}
		}
		fN_0015.Clear();
		_0013.B.nu(ref fN_0012);
	}
}
