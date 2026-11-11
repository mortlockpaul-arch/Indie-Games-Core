using Microsoft.Xna.Framework;

namespace SynapseGaming.LightingSystem.Effects;

public interface ISkinnedEffect
{
	Matrix[] SkinBones { get; set; }

	bool Skinned { get; set; }
}
