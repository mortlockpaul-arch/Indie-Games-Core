using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using b;
using x;
using y;

namespace SynapseGaming.LightingSystem.Rendering;

public class HighDynamicRangePostProcessor : BaseRenderTargetPostProcessor
{
	private const int l8a = 0;

	private const int l8_u0013_ = 1;

	private const int l8N = 2;

	private const int l8l = 1;

	private DetailPreference l8L;

	private b.L l8w;

	private b.w l8J;

	private b.J l8B;

	public override void ApplyPreferences(ISystemPreferences preferences)
	{
		l8L = preferences.PostProcessingDetail;
	}

	public override void Unload()
	{
		y.a.w_u0005_(ref l8J);
		y.a.w_u0005_(ref l8B);
		y.a.w_u0005_(ref l8w);
		base.Unload();
	}

	private int L_u0004_(int param_0)
	{
		float num = CoreHelper.Log2(param_0);
		num = (float)Math.Floor(num);
		param_0 = (int)Math.Pow(2.0, num);
		return param_0;
	}

	private SurfaceFormat LX(SurfaceFormat param_0)
	{
		if (SceneState == null || SceneState.FrameBuffers == null)
		{
			if (param_0 == SurfaceFormat.HdrBlendable)
			{
				param_0 = SurfaceFormat.HalfVector4;
			}
		}
		else
		{
			FrameBuffers frameBuffers = SceneState.FrameBuffers;
			param_0 = ((frameBuffers.LightingRange == DetailPreference.High || frameBuffers.LightingRange == DetailPreference.Medium) ? SurfaceFormat.HalfVector4 : SurfaceFormat.Color);
		}
		return param_0;
	}

	public override void EndFrameRendering()
	{
		GraphicsDevice graphicsDevice = GraphicsDeviceManager.GraphicsDevice;
		graphicsDevice.BlendState = BlendState.Opaque;
		graphicsDevice.DepthStencilState = DepthStencilState.None;
		graphicsDevice.RasterizerState = RasterizerState.CullNone;
		CustomFrameBufferCollection customFrameBufferCollection = SceneState.FrameBuffers.GetCustomFrameBufferCollection("HDRBloomBuffers", createmissing: true);
		CustomFrameBufferCollection customFrameBufferCollection2 = SceneState.FrameBuffers.GetCustomFrameBufferCollection("HDRDownSampleChain", createmissing: true);
		if (l8w == null)
		{
			l8w = new b.L(graphicsDevice);
			l8J = new b.w(graphicsDevice);
			l8B = new b.J(graphicsDevice);
		}
		if (customFrameBufferCollection2.Count <= 0)
		{
			int num = ProcessorRenderTarget.Width;
			int num2 = ProcessorRenderTarget.Height;
			SurfaceFormat preferredFormat = LX(ProcessorRenderTarget.Format);
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
			SurfaceFormat preferredFormat2 = LX(ProcessorRenderTarget.Format);
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
		x.h.LO(graphicsDevice);
		RenderTarget2D renderTarget2D4 = customFrameBufferCollection2[customFrameBufferCollection2.Count - 1];
		RenderTarget2D renderTarget2D5 = customFrameBufferCollection[0];
		if (renderTarget2D4 == renderTarget2D5)
		{
			throw new Exception("Error intensity swap targets are the same object.");
		}
		l8w.IntensityBlend = intensityBlend;
		l8w.IntensityTexture = renderTarget2D5;
		l8w.DynamicRangeEnabled = environment.DynamicRangeEnabled;
		l8w.BloomThreshold = environment.BloomThreshold;
		l8w.BloomAmount = environment.BloomAmount;
		l8w.ExposureAmount = environment.ExposureAmount;
		l8w.TransitionMaxScale = environment.DynamicRangeTransitionMaxScale;
		l8w.TransitionMinScale = environment.DynamicRangeTransitionMinScale;
		Viewport viewport;
		for (int i = 0; i < customFrameBufferCollection2.Count; i++)
		{
			RenderTarget2D renderTarget2D6 = customFrameBufferCollection2[i];
			graphicsDevice.SetRenderTarget(renderTarget2D6);
			viewport = graphicsDevice.Viewport;
			if (i <= 0)
			{
				l8w.Mode = b.L.w8a.ConvertData;
			}
			else if (i >= customFrameBufferCollection2.Count - 1)
			{
				l8w.Mode = b.L.w8a.BlendIntensity;
			}
			else
			{
				l8w.Mode = b.L.w8a.Normal;
			}
			l8w.SourceTexture = renderTarget2D3;
			l8w.CurrentTechnique.Passes[0].Apply();
			FullFrameQuad.Render(graphicsDevice, viewport.Width, viewport.Height);
			renderTarget2D3 = renderTarget2D6;
			if (!environment.DynamicRangeEnabled && i >= 1)
			{
				break;
			}
		}
		RenderTarget2D renderTarget2D7 = customFrameBufferCollection[1];
		RenderTarget2D renderTarget2D8 = customFrameBufferCollection[2];
		graphicsDevice.SetRenderTarget(renderTarget2D7);
		viewport = graphicsDevice.Viewport;
		l8B.BloomDetail = l8L;
		l8B.SourceTexture = customFrameBufferCollection2[1];
		l8B.BlurKernel = b.J.HorizontalBlurKernel;
		l8B.CurrentTechnique.Passes[0].Apply();
		FullFrameQuad.Render(graphicsDevice, viewport.Width, viewport.Height);
		graphicsDevice.SetRenderTarget(renderTarget2D8);
		viewport = graphicsDevice.Viewport;
		l8B.SourceTexture = renderTarget2D7;
		l8B.BlurKernel = b.J.VerticalKernelKernel;
		l8B.CurrentTechnique.Passes[0].Apply();
		FullFrameQuad.Render(graphicsDevice, viewport.Width, viewport.Height);
		if (PreviousRenderTargetCube != null)
		{
			graphicsDevice.SetRenderTarget(PreviousRenderTargetCube, PreviousRenderTargetCubeFace);
		}
		else
		{
			graphicsDevice.SetRenderTarget(PreviousRenderTarget);
		}
		graphicsDevice.Viewport = PreviousViewport;
		viewport = PreviousViewport;
		l8J.DynamicRangeEnabled = environment.DynamicRangeEnabled;
		l8J.BloomThreshold = environment.BloomThreshold;
		l8J.BloomAmount = environment.BloomAmount;
		l8J.ExposureAmount = environment.ExposureAmount;
		l8J.TransitionMaxScale = environment.DynamicRangeTransitionMaxScale;
		l8J.TransitionMinScale = environment.DynamicRangeTransitionMinScale;
		l8J.SetSaturationDarkenAndCinematic(environment.DynamicRangeSaturationAmount, environment.DynamicRangeDarkenAmount, environment.DynamicRangeCinematicAmount);
		l8J.SourceTexture = ProcessorRenderTarget;
		l8J.BloomTexture = renderTarget2D8;
		l8J.IntensityTexture = renderTarget2D3;
		l8J.CurrentTechnique.Passes[0].Apply();
		FullFrameQuad.Render(graphicsDevice, viewport.Width, viewport.Height);
		customFrameBufferCollection2[customFrameBufferCollection2.Count - 1] = renderTarget2D5;
		customFrameBufferCollection[0] = renderTarget2D4;
		graphicsDevice.BlendState = BlendState.Opaque;
		graphicsDevice.DepthStencilState = DepthStencilState.Default;
		graphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
	}
}
