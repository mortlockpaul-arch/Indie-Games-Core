using SynapseGaming.LightingSystem.Collision;
using SynapseGaming.LightingSystem.Rendering;

namespace SynapseGaming.LightingSystem.Core;

public class SceneEntityTypeCaster : ITypeCaster<SceneEntity>
{
	public ISceneObject SceneObject;

	public ICollisionObject CollisionObject;

	public void Set(SceneEntity entity)
	{
		SceneObject = entity as ISceneObject;
		CollisionObject = entity as ICollisionObject;
	}
}
