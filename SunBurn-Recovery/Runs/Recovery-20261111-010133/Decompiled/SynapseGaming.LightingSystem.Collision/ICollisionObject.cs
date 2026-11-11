using Microsoft.Xna.Framework;
using SynapseGaming.LightingSystem.Components;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Editor;
using SynapseGaming.LightingSystem.Rendering;

namespace SynapseGaming.LightingSystem.Collision;

public interface ICollisionObject : ISceneEntity, IMovableObject, IWorldBoundingBoxObject, IComponentObject<ISceneEntity>, IEditorCreatedObject<ISceneEntity>, IEditorObject, INamedObject, IEditorRenderableObject
{
	int CollisionId { get; }

	bool AffectedByGravity { get; set; }

	CollisionType CollisionType { get; set; }

	ICollisionMove CollisionMove { get; set; }

	ICollisionMaterial DefaultCollisionMaterial { get; set; }

	float Mass { get; set; }

	Matrix WorldToObject { get; }

	event CollisionReactDelegate CollisionReactEvent;

	event CollisionTriggerDelegate CollisionTriggerEvent;

	void OnCollisionReact(IMovableObject collider, IMovableObject collidee, CollisionPoint worldcollisionpoint, ref bool collisionhandled);

	void OnCollisionTrigger(IMovableObject collider, IMovableObject trigger);
}
