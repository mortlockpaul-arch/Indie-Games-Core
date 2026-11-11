namespace SynapseGaming.LightingSystem.Collision;

public interface ICollisionMaterial
{
	int CollisionId { get; }

	float Elasticity { get; set; }

	float Friction { get; set; }
}
