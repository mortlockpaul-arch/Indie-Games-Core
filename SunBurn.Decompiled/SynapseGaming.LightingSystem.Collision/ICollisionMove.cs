using Microsoft.Xna.Framework;

namespace SynapseGaming.LightingSystem.Collision;

/// <summary>
/// Interface used by classes that implement collision movement
/// and applying force to a collision object.
/// </summary>
public interface ICollisionMove
{
	/// <summary>
	/// Distance the object will move this frame. Valid after calling Begin().
	/// </summary>
	float Distance { get; }

	/// <summary>
	/// Direction the object will move this frame. Valid after calling Begin().
	/// </summary>
	Vector3 Normal { get; }

	/// <summary>
	/// World bounding area the object will move to this frame.
	/// </summary>
	BoundingSphere WorldBoundingSphere { get; }

	/// <summary>
	/// World bounding area the object will move to this frame.
	/// </summary>
	BoundingBox WorldBoundingBox { get; }

	/// <summary>
	/// Applies force to the object. The total force is used to move the
	/// object during the next call to Begin().
	/// </summary>
	/// <param name="objectforce">Amount of object-space force to apply to the object.</param>
	void ApplyObjectForce(Vector3 objectforce);

	/// <summary>
	/// Applies force to the object. The total force is used to move the
	/// object during the next call to Begin().
	/// </summary>
	/// <param name="worldforce">Amount of world-space force to apply to the object.</param>
	/// <param name="constantforce">Determines if the force is from a constant
	/// source such as gravity, wind, or similar (eg: applied by the caller
	/// every frame instead of a single time).</param>
	void ApplyWorldForce(Vector3 worldforce, bool constantforce);

	/// <summary>
	/// Removes all accumulated forces acting on the object. This will halt the object
	/// movement, however future forces (such as gravity) can immediately begin acting
	/// on the object again.
	/// </summary>
	void RemoveForces();

	/// <summary>
	/// Calculates and applies the reaction force between the
	/// object and the collision surface contained in the CollisionPoint.
	/// </summary>
	/// <param name="worldcollisionpoint">Contains information about the closest collision point to the collider.</param>
	void React(CollisionPoint worldcollisionpoint);
}
