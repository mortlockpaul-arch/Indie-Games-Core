using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;

namespace SynapseGaming.LightingSystem.Rendering;

/// <summary>
/// Post-processor base class used for processors that contain their own
/// render targets. These processors do *not* render directly to their own
/// targets, instead the previous processor or the main scene rendering
/// code (if no previous processor exists) renders to the target.  The current
/// processor then uses a texture containing the image of it's target to
/// apply visual effects to the next processor's render target.
///
/// Processors derived from this class are responsible for:
///   -switching to the ProcessorRenderTarget on BeginFrameRendering (done in the base method)
///   -switching to the PreviousRenderTarget and applying processed image data
///    from ProcessorRenderTarget in EndFrameRendering
///    (this modifies the image data for the next processor in the chain)
/// </summary>
public abstract class BaseRenderTargetPostProcessor : IPostProcessor
{
	private IGraphicsDeviceService fNB;

	private RenderTarget2D fNa;

	private RenderTarget2D fNg;

	private Viewport fNf;

	private ISceneState fN_0003;

	/// <summary>
	/// The current GraphicsDeviceManager used by this object.
	/// </summary>
	protected IGraphicsDeviceService GraphicsDeviceManager => fNB;

	/// <summary>
	/// The processor's render target used for applying visual effects.
	/// </summary>
	protected RenderTarget2D ProcessorRenderTarget => fNa;

	/// <summary>
	/// The render target occupying slot 0 in the device (either the next post processor's render
	/// target or the back buffer) prior to making the processor's render target the current target.
	/// </summary>
	protected RenderTarget2D PreviousRenderTarget => fNg;

	/// <summary>
	/// The viewport in use by the device (either the next post processor's render
	/// target or the back buffer) prior to making the processor's viewport the current viewport.
	/// </summary>
	protected Viewport PreviousViewport => fNf;

	/// <summary>
	/// The current SceneState used by this object.
	/// </summary>
	protected ISceneState SceneState => fN_0003;

	/// <summary>
	/// Creates a BaseRenderTargetPostProcessor instance.
	/// </summary>
	/// <param name="graphicsdevicemanager"></param>
	public BaseRenderTargetPostProcessor(IGraphicsDeviceService graphicsdevicemanager)
	{
		fNB = graphicsdevicemanager;
	}

	/// <summary>
	/// Use to apply user quality and performance preferences to the resources managed by this object.
	/// </summary>
	/// <param name="preferences"></param>
	public abstract void ApplyPreferences(ILightingSystemPreferences preferences);

	/// <summary>
	/// Sets up the object prior to rendering. This base implementation automatically switches the current
	/// device render target to the processor's render target.
	/// </summary>
	/// <param name="scenestate"></param>
	/// <param name="rendertargetindex"></param>
	/// <returns>Returns true if the processor reserves a render target from the frame buffers.</returns>
	public bool BeginFrameRendering(ISceneState scenestate, int rendertargetindex)
	{
		GraphicsDevice graphicsDevice = fNB.GraphicsDevice;
		if ((rendertargetindex & 1) == 0)
		{
			fNa = scenestate.FrameBuffers.GetBuffer(FrameBufferType.PostProcessing1, createmissing: true);
		}
		else
		{
			fNa = scenestate.FrameBuffers.GetBuffer(FrameBufferType.PostProcessing2, createmissing: true);
		}
		fNf = graphicsDevice.Viewport;
		RenderTargetBinding[] renderTargets = graphicsDevice.GetRenderTargets();
		if (renderTargets.Length > 0)
		{
			fNg = (RenderTarget2D)renderTargets[0].RenderTarget;
		}
		else
		{
			fNg = null;
		}
		graphicsDevice.SetRenderTarget(fNa);
		fN_0003 = scenestate;
		return true;
	}

	/// <summary>
	/// Applies post processing effects based on the source textures. This base implementation automatically
	/// sets the current device render target back to the previous target and returns a texture of the processor's render target data.
	/// </summary>
	public virtual void EndFrameRendering()
	{
		GraphicsDevice graphicsDevice = fNB.GraphicsDevice;
		graphicsDevice.SetRenderTarget(fNg);
		graphicsDevice.Viewport = fNf;
	}

	/// <summary>
	/// Disposes any graphics resources used internally by this object.
	/// </summary>
	public virtual void Unload()
	{
		fNa = null;
		fNg = null;
	}
}
