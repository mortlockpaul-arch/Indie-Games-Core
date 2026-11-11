using SynapseGaming.LightingSystem.Core;

namespace SynapseGaming.LightingSystem.Rendering;

public interface IPostProcessor
{
	void ApplyPreferences(ISystemPreferences preferences);

	bool BeginFrameRendering(ISceneState scenestate, int rendertargetindex);

	void EndFrameRendering();

	void Unload();
}
