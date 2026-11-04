using System;
using _0013;
using L;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using h;
using j;

namespace SynapseGaming.LightingSystem.Rendering;

/// <summary>
/// Adds high dynamic range (HDR) lighting to the scene. Includes HDR, tone mapping, and bloom.
/// </summary>
public class HighDynamicRangePostProcessor : BaseRenderTargetPostProcessor
{
	private const int fNB = 0;

	private const int fNa = 1;

	private const int fNg = 2;

	private const int fNf = 1;

	private DetailPreference fN_0003;

	private j._0003 fNn;

	private j.n fN_0019;

	private j._0019 fNL;

	/// <summary>
	/// Creates a HighDynamicRangePostProcessor instance.
	/// </summary>
	/// <param name="graphicsdevicemanager"></param>
	public HighDynamicRangePostProcessor(IGraphicsDeviceService graphicsdevicemanager)
		: base(graphicsdevicemanager)
	{
	}

	/// <summary>
	/// Use to apply user quality and performance preferences to the resources managed by this object.
	/// </summary>
	/// <param name="preferences"></param>
	public override void ApplyPreferences(ILightingSystemPreferences preferences)
	{
		fN_0003 = preferences.PostProcessingDetail;
	}

	/// <summary>
	/// Disposes any graphics resources used internally by this object.
	/// </summary>
	public override void Unload()
	{
		_0013.B.nu(ref fN_0019);
		_0013.B.nu(ref fNL);
		_0013.B.nu(ref fNn);
		base.Unload();
	}

	private int _0003t(int P_0)
	{
		float num = L._0019.a_0011(P_0);
		num = (float)Math.Floor(num);
		P_0 = (int)Math.Pow(2.0, num);
		return P_0;
	}

	private SurfaceFormat _00032(SurfaceFormat P_0)
	{
		if (SceneState == null || SceneState.FrameBuffers == null)
		{
			if (P_0 == SurfaceFormat.HdrBlendable)
			{
				P_0 = SurfaceFormat.HalfVector4;
			}
		}
		else
		{
			FrameBuffers frameBuffers = SceneState.FrameBuffers;
			P_0 = ((frameBuffers.LightingRange == DetailPreference.High || frameBuffers.LightingRange == DetailPreference.Medium) ? SurfaceFormat.HalfVector4 : SurfaceFormat.Color);
		}
		return P_0;
	}

	/// <summary>
	/// Applies post processing effects based on the source textures.
	/// </summary>
	public override void EndFrameRendering()
	{
		GraphicsDevice graphicsDevice = GraphicsDeviceManager.GraphicsDevice;
		graphicsDevice.BlendState = BlendState.Opaque;
		graphicsDevice.DepthStencilState = DepthStencilState.None;
		graphicsDevice.RasterizerState = RasterizerState.CullNone;
		CustomFrameBufferCollection customFrameBufferCollection = SceneState.FrameBuffers.GetCustomFrameBufferCollection("HDRBloomBuffers", createmissing: true);
		CustomFrameBufferCollection customFrameBufferCollection2 = SceneState.FrameBuffers.GetCustomFrameBufferCollection("HDRDownSampleChain", createmissing: true);
		if (fNn == null)
		{
			fNn = new j._0003(graphicsDevice);
			fN_0019 = new j.n(graphicsDevice);
			fNL = new j._0019(graphicsDevice);
		}
		if (customFrameBufferCollection2.Count <= 0)
		{
			int num = ProcessorRenderTarget.Width;
			int num2 = ProcessorRenderTarget.Height;
			SurfaceFormat preferredFormat = _00032(ProcessorRenderTarget.Format);
			while (num > 1 || num2 > 1)
			{
				num = (int)Math.Max((float)num / 2f + 0.5f, 1f);
				num2 = (int)Math.Max((float)num2 / 2f + 0.5f, 1f);
				RenderTarget2D item = new RenderTarget2D(graphicsDevice, num, num2, mipMap: false, preferredFormat, DepthFormat.None, 0, RenderTargetUsage.PlatformContents);
				customFrameBufferCollection2.Add(item);
			}
		}
		if (customFrameBufferCollection.Count <= 0)
		{
			_ = ProcessorRenderTarget.Width;
			_ = ProcessorRenderTarget.Height;
			SurfaceFormat preferredFormat2 = _00032(ProcessorRenderTarget.Format);
			customFrameBufferCollection.Add(null);
			customFrameBufferCollection.Add(null);
			customFrameBufferCollection.Add(null);
			RenderTarget2D renderTarget = (customFrameBufferCollection[0] = new RenderTarget2D(graphicsDevice, 1, 1, mipMap: false, preferredFormat2, DepthFormat.None, 0, RenderTargetUsage.PlatformContents));
			RenderTarget2D renderTarget2D2 = customFrameBufferCollection2[1];
			customFrameBufferCollection[1] = new RenderTarget2D(graphicsDevice, renderTarget2D2.Width, renderTarget2D2.Height, mipMap: false, preferredFormat2, DepthFormat.None, 0, RenderTargetUsage.PlatformContents);
			customFrameBufferCollection[2] = new RenderTarget2D(graphicsDevice, renderTarget2D2.Width, renderTarget2D2.Height, mipMap: false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.PlatformContents);
			graphicsDevice.SetRenderTarget(renderTarget);
			graphicsDevice.Clear(Color.Gray);
			graphicsDevice.SetRenderTarget(null);
		}
		float intensityBlend = (float)SceneState.GameTime.ElapsedGameTime.TotalSeconds / SceneState.Environment.DynamicRangeTransitionTime;
		ISceneEnvironment environment = SceneState.Environment;
		RenderTarget2D renderTarget2D3 = ProcessorRenderTarget;
		h._0012._0003T(graphicsDevice);
		RenderTarget2D renderTarget2D4 = customFrameBufferCollection2[customFrameBufferCollection2.Count - 1];
		RenderTarget2D renderTarget2D5 = customFrameBufferCollection[0];
		if (renderTarget2D4 == renderTarget2D5)
		{
			throw new Exception("Error intensity swap targets are the same object.");
		}
		fNn.IntensityBlend = intensityBlend;
		fNn.IntensityTexture = renderTarget2D5;
		fNn.DynamicRangeEnabled = environment.DynamicRangeEnabled;
		fNn.BloomThreshold = environment.BloomThreshold;
		fNn.BloomAmount = environment.BloomAmount;
		fNn.ExposureAmount = environment.ExposureAmount;
		fNn.TransitionMaxScale = environment.DynamicRangeTransitionMaxScale;
		fNn.TransitionMinScale = environment.DynamicRangeTransitionMinScale;
		Viewport viewport;
		for (int k = 0; k < customFrameBufferCollection2.Count; k++)
		{
			RenderTarget2D renderTarget2D6 = customFrameBufferCollection2[k];
			graphicsDevice.SetRenderTarget(renderTarget2D6);
			viewport = graphicsDevice.Viewport;
			if (k <= 0)
			{
				fNn.Mode = j._0003.nNB.ConvertData;
			}
			else if (k >= customFrameBufferCollection2.Count - 1)
			{
				fNn.Mode = j._0003.nNB.BlendIntensity;
			}
			else
			{
				fNn.Mode = j._0003.nNB.Normal;
			}
			fNn.SourceTexture = renderTarget2D3;
			fNn.CurrentTechnique.Passes[0].Apply();
			FullFrameQuad.Render(graphicsDevice, viewport.Width, viewport.Height);
			renderTarget2D3 = renderTarget2D6;
			if (!environment.DynamicRangeEnabled && k >= 1)
			{
				break;
			}
		}
		RenderTarget2D renderTarget2D7 = customFrameBufferCollection[1];
		RenderTarget2D renderTarget2D8 = customFrameBufferCollection[2];
		graphicsDevice.SetRenderTarget(renderTarget2D7);
		viewport = graphicsDevice.Viewport;
		fNL.BloomDetail = fN_0003;
		fNL.SourceTexture = customFrameBufferCollection2[1];
		fNL.BlurKernel = j._0019.HorizontalBlurKernel;
		fNL.CurrentTechnique.Passes[0].Apply();
		FullFrameQuad.Render(graphicsDevice, viewport.Width, viewport.Height);
		graphicsDevice.SetRenderTarget(renderTarget2D8);
		viewport = graphicsDevice.Viewport;
		fNL.SourceTexture = renderTarget2D7;
		fNL.BlurKernel = j._0019.VerticalKernelKernel;
		fNL.CurrentTechnique.Passes[0].Apply();
		FullFrameQuad.Render(graphicsDevice, viewport.Width, viewport.Height);
		graphicsDevice.SetRenderTarget(PreviousRenderTarget);
		graphicsDevice.Viewport = PreviousViewport;
		viewport = PreviousViewport;
		fN_0019.DynamicRangeEnabled = environment.DynamicRangeEnabled;
		fN_0019.BloomThreshold = environment.BloomThreshold;
		fN_0019.BloomAmount = environment.BloomAmount;
		fN_0019.ExposureAmount = environment.ExposureAmount;
		fN_0019.TransitionMaxScale = environment.DynamicRangeTransitionMaxScale;
		fN_0019.TransitionMinScale = environment.DynamicRangeTransitionMinScale;
		fN_0019.SetSaturationDarkenAndCinematic(environment.DynamicRangeSaturationAmount, environment.DynamicRangeDarkenAmount, environment.DynamicRangeCinematicAmount);
		fN_0019.SourceTexture = ProcessorRenderTarget;
		fN_0019.BloomTexture = renderTarget2D8;
		fN_0019.IntensityTexture = renderTarget2D3;
		fN_0019.CurrentTechnique.Passes[0].Apply();
		FullFrameQuad.Render(graphicsDevice, viewport.Width, viewport.Height);
		customFrameBufferCollection2[customFrameBufferCollection2.Count - 1] = renderTarget2D5;
		customFrameBufferCollection[0] = renderTarget2D4;
		graphicsDevice.BlendState = BlendState.Opaque;
		graphicsDevice.DepthStencilState = DepthStencilState.Default;
		graphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
	}
}
