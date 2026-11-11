using System;
using Microsoft.Xna.Framework;

namespace SynapseGaming.LightingSystem.Collision;

public interface ICollisionEntity : IDisposable
{
	float Distance { get; }

	Vector3 Normal { get; }

	ICollisionObject Object { get; }

	bool CheckSceneObjectChanged();

	void ApplyWorldForce(ref Vector3 worldforce);

	void ApplyWorldForce(ref Vector3 worldposition, ref Vector3 worldforce);

	void RemoveForces();

	void SyncToPhysicsEntity();

	void SyncToSceneObject();
}
