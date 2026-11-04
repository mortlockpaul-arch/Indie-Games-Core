using SynapseGaming.LightingSystem.Collision;
using SynapseGaming.LightingSystem.Rendering;

namespace L;

internal class w : global::L.j<SceneEntity>
{
	internal ISceneObject fNB;

	internal ICollisionObject fNa;

	public void Set(SceneEntity entity)
	{
		fNB = entity as ISceneObject;
		fNa = entity as ICollisionObject;
	}
}
