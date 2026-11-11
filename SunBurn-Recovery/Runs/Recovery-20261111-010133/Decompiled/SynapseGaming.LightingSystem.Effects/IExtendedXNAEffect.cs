using SynapseGaming.LightingSystem.Collision;
using SynapseGaming.LightingSystem.Core;

namespace SynapseGaming.LightingSystem.Effects;

public interface IExtendedXNAEffect : ICollisionMaterial
{
	bool DoubleSided { get; set; }

	TransparencyMode TransparencyMode { get; set; }
}
