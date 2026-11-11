using System;
using Microsoft.Xna.Framework;
using SynapseGaming.LightingSystem.Collision;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Editor;

namespace SynapseGaming.LightingSystem.Components;

public interface IComponent<T> : IEditorCreatedObject<IComponent<T>>, IEditorObject, INamedObject
{
	Type ComponentType { get; }

	T ParentObject { get; set; }

	void OnInitialize();

	void OnComponentsListChanged();

	void OnMessage(IComponent<T> sender, IComponentMessage message);

	void OnAddedToParentObject();

	void OnRemovedFromParentObject();

	void OnSubmittedToManager(IManagerService manager);

	void OnRemovedFromManager(IManagerService manager);

	void OnUpdate(GameTime gametime);

	void OnCollisionReact(IMovableObject collider, IMovableObject collidee, CollisionPoint worldcollisionpoint, ref bool collisionhandled);

	void OnCollisionTrigger(IMovableObject collider, IMovableObject trigger);
}
