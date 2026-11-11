using Microsoft.Xna.Framework;
using SynapseGaming.LightingSystem.Components;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Editor;

namespace SynapseGaming.LightingSystem.Rendering;

public interface ISceneEntity : IMovableObject, IWorldBoundingBoxObject, IComponentObject<ISceneEntity>, IEditorCreatedObject<ISceneEntity>, IEditorObject, INamedObject, IEditorRenderableObject
{
	HullType HullType { get; set; }

	BoundingSphere ObjectBoundingSphere { get; }

	BoundingBox ObjectBoundingBox { get; }

	BoundingSphere WorldBoundingSphere { get; }

	void SetWorldAndWorldToObject(Matrix world, Matrix worldtoobj);

	void RenderCustomPass(ISceneState scenestate);
}
