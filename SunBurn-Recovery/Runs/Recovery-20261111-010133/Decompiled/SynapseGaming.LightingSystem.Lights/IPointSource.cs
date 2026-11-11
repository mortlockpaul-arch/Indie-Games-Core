using Microsoft.Xna.Framework;

namespace SynapseGaming.LightingSystem.Lights;

public interface IPointSource
{
	Vector3 Position { get; set; }

	float Radius { get; set; }
}
