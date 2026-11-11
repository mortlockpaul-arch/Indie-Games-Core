namespace SynapseGaming.LightingSystem.Core;

public interface IRenderableManager : IManager, IUnloadable
{
	void BeginFrameRendering(ISceneState scenestate);

	void EndFrameRendering();
}
