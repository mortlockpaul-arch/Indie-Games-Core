using Microsoft.Xna.Framework;

namespace SynapseGaming.LightingSystem.Core;

public interface IWorldBoundingBoxObject
{
	BoundingBox WorldBoundingBox { get; }
}
