using Microsoft.Xna.Framework;

namespace SynapseGaming.LightingSystem.Lights;

public interface IDirectionalSource
{
	Vector3 Direction { get; set; }
}
