using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;

namespace SynapseGaming.LightingSystem.Rendering;

public abstract class BaseRenderTargetPostProcessor : IPostProcessor
{
	private IGraphicsDeviceService l8a;

	private RenderTarget2D l8_u0013_;

	private RenderTarget2D l8N;

	private RenderTargetCube l8l;

	private CubeMapFace l8L;

	private Viewport l8w;

	private ISceneState l8J;

	protected IGraphicsDeviceService GraphicsDeviceManager => l8a;

	protected RenderTarget2D ProcessorRenderTarget => l8_u0013_;

	protected RenderTarget2D PreviousRenderTarget => l8N;

	protected RenderTargetCube PreviousRenderTargetCube => l8l;

	protected CubeMapFace PreviousRenderTargetCubeFace => l8L;

	protected Viewport PreviousViewport => l8w;

	protected ISceneState SceneState => l8J;

	public BaseRenderTargetPostProcessor()
	{
		l8a = SunBurnCoreSystem.Instance.GraphicsDeviceManager;
	}

	public abstract void ApplyPreferences(ISystemPreferences preferences);

	public bool BeginFrameRendering(ISceneState scenestate, int rendertargetindex)
	{
		GraphicsDevice graphicsDevice = l8a.GraphicsDevice;
		if ((rendertargetindex & 1) == 0)
		{
			l8_u0013_ = scenestate.FrameBuffers.GetBuffer(FrameBufferType.PostProcessing1, createmissing: true);
		}
		else
		{
			l8_u0013_ = scenestate.FrameBuffers.GetBuffer(FrameBufferType.PostProcessing2, createmissing: true);
		}
		l8w = graphicsDevice.Viewport;
		l8N = null;
		l8l = null;
		RenderTargetBinding[] renderTargets = graphicsDevice.GetRenderTargets();
		if (renderTargets.Length > 0)
		{
			RenderTargetBinding renderTargetBinding = renderTargets[0];
			l8N = renderTargetBinding.RenderTarget as RenderTarget2D;
			l8l = renderTargetBinding.RenderTarget as RenderTargetCube;
			l8L = renderTargetBinding.CubeMapFace;
		}
		graphicsDevice.SetRenderTarget(l8_u0013_);
		l8J = scenestate;
		return true;
	}

	public virtual void EndFrameRendering()
	{
		GraphicsDevice graphicsDevice = l8a.GraphicsDevice;
		if (l8l != null)
		{
			graphicsDevice.SetRenderTarget(l8l, l8L);
		}
		else
		{
			graphicsDevice.SetRenderTarget(l8N);
		}
		graphicsDevice.Viewport = l8w;
	}

	public virtual void Unload()
	{
		l8_u0013_ = null;
		l8N = null;
	}
}
