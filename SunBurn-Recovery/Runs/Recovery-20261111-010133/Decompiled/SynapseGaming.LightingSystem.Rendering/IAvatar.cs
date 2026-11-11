using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using SynapseGaming.LightingSystem.Components;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Editor;

namespace SynapseGaming.LightingSystem.Rendering;

public interface IAvatar : ISceneEntity, IMovableObject, IWorldBoundingBoxObject, IComponentObject<ISceneEntity>, IEditorCreatedObject<ISceneEntity>, IEditorObject, INamedObject, IEditorRenderableObject
{
	BoundingBox WorldBoundingBoxProxy { get; }

	IList<Matrix> SkinBones { get; set; }

	AvatarExpression Expression { get; set; }

	AvatarRenderer Renderer { get; }

	AvatarDescription Description { get; }

	ObjectVisibility Visibility { get; set; }

	bool CastShadows { get; }

	bool Visible { get; }

	void ApplyAnimation(IAvatarAnimation animation);
}
