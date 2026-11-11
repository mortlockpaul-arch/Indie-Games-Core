using SynapseGaming.LightingSystem.Core;

namespace SynapseGaming.LightingSystem.Collision;

public delegate void CollisionReactDelegate(IMovableObject collider, IMovableObject collidee, CollisionPoint worldcollisionpoint, ref bool collisionhandled);
