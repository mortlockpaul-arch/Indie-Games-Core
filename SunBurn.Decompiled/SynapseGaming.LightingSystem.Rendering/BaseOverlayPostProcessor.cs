using SynapseGaming.LightingSystem.Core;

namespace SynapseGaming.LightingSystem.Rendering;

/// <summary>
/// Post-processor base class used for processors that do not use their own
/// render targets, and instead use data from the previous processor to
/// alter the current render target (the target owned by the next processor
/// down the chain).
/// </summary>
public abstract class BaseOverlayPostProcessor : IPostProcessor
{
	private ISceneState fNB;

	/// <summary>
	/// The current SceneState used by this object.
	/// </summary>
	protected ISceneState SceneState => fNB;

	/// <summary>
	/// Use to apply user quality and performance preferences to the resources managed by this object.
	/// </summary>
	/// <param name="preferences"></param>
	public abstract void ApplyPreferences(ILightingSystemPreferences preferences);

	/// <summary>
	/// Sets up the object prior to rendering.
	/// </summary>
	/// <param name="scenestate"></param>
	/// <param name="rendertargetindex"></param>
	/// <returns>Returns true if the processor reserves a render target from the frame buffers.</returns>
	public bool BeginFrameRendering(ISceneState scenestate, int rendertargetindex)
	{
		fNB = scenestate;
		return false;
	}

	/// <summary>
	/// Applies post processing effects based on the source textures.
	/// </summary>
	public virtual void EndFrameRendering()
	{
	}

	/// <summary>
	/// Disposes any graphics resources used internally by this object.
	/// </summary>
	public virtual void Unload()
	{
	}
}
