using SynapseGaming.LightingSystem.Rendering;

namespace SynapseGaming.LightingSystem.Core;

public class SceneObjectTypeCaster : ITypeCaster<ISceneObject>
{
	public SpriteContainer SpriteContainer;

	public void Set(ISceneObject entity)
	{
		SpriteContainer = entity as SpriteContainer;
	}
}
