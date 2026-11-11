using SynapseGaming.LightingSystem.Core;

namespace SynapseGaming.LightingSystem.Editor;

public interface IEditorRenderableObject
{
	void RenderEditorIcon(ISceneState scenestate, BoundingBoxRenderHelper renderhelper, bool highlighted, bool selected, bool sceneoccludedpass);
}
