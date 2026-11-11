using Microsoft.Xna.Framework;

namespace SynapseGaming.LightingSystem.Core;

public interface IBoundingVolume
{
	BoundingBox BoundingBox { get; }

	BoundingSphere BoundingSphere { get; }
}
