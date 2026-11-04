using SynapseGaming.LightingSystem.Core;

namespace SynapseGaming.LightingSystem.Rendering;

/// <summary>
/// Interface used by objects that perform post render visual processing.
/// </summary>
public interface IPostProcessor
{
	/// <summary>
	/// Use to apply user quality and performance preferences to the resources managed by this object.
	/// </summary>
	/// <param name="preferences"></param>
	void ApplyPreferences(ILightingSystemPreferences preferences);

	/// <summary>
	/// Sets up the object prior to rendering.
	/// </summary>
	/// <param name="scenestate"></param>
	/// <param name="rendertargetindex"></param>
	/// <returns>Returns true if the processor reserves a render target from the frame buffers.</returns>
	bool BeginFrameRendering(ISceneState scenestate, int rendertargetindex);

	/// <summary>
	/// Applies post processing effects based on the source textures.
	/// </summary>
	void EndFrameRendering();

	/// <summary>
	/// Disposes any graphics resources used internally by this object.
	/// </summary>
	void Unload();
}
