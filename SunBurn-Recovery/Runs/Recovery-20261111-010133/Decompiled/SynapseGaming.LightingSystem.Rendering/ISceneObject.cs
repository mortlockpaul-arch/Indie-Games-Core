using Microsoft.Xna.Framework;
using SynapseGaming.LightingSystem.Components;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Editor;
using SynapseGaming.LightingSystem.Lights;

namespace SynapseGaming.LightingSystem.Rendering;

public interface ISceneObject : ISceneEntity, IMovableObject, IWorldBoundingBoxObject, IComponentObject<ISceneEntity>, IEditorCreatedObject<ISceneEntity>, IEditorObject, INamedObject, IEditorRenderableObject
{
	ModelAsset ModelAsset { get; set; }

	StaticLightingType StaticLightingType { get; set; }

	LightMapSize LightMapSize { get; set; }

	Vector3 CustomStaticLightingColor { get; set; }

	bool CanLightMap { get; }

	bool Valid { get; set; }

	string RenderingErrors { get; set; }

	bool CastShadows { get; }

	bool Visible { get; }

	bool VisibleInEditor { get; }

	ObjectVisibility Visibility { get; set; }

	Matrix[] SkinBones { get; set; }

	RenderableMeshCollection RenderableMeshes { get; }
}
