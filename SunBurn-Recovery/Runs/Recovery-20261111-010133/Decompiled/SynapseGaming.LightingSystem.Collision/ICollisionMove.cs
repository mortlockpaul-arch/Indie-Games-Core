using Microsoft.Xna.Framework;
using SynapseGaming.LightingSystem.Core;

namespace SynapseGaming.LightingSystem.Collision;

public interface ICollisionMove
{
	float Distance { get; }

	Vector3 Normal { get; }

	void ApplyObjectForce(Vector3 objectforce);

	void ApplyObjectForce(Vector3 objectposition, Vector3 objectforce);

	void ApplyWorldForce(Vector3 worldforce, bool constantforce);

	void ApplyWorldForce(Vector3 worldposition, Vector3 worldforce);

	void RemoveForces();

	void OnSubmittedToManager(IManagerService manager);

	void OnRemovedFromManager(IManagerService manager);
}
