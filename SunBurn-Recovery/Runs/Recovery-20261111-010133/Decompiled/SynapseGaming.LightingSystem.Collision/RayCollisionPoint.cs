using Microsoft.Xna.Framework;

namespace SynapseGaming.LightingSystem.Collision;

public struct RayCollisionPoint
{
	public float ContactTime;

	public Vector3 ContactPoint;

	public Vector3 SurfaceNormal;

	public ICollisionObject ContactObject;

	public ICollisionMaterial Material;
}
