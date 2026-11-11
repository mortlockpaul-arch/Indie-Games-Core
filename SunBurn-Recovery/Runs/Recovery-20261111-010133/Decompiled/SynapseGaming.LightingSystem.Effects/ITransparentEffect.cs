using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;

namespace SynapseGaming.LightingSystem.Effects;

public interface ITransparentEffect
{
	TransparencyMode TransparencyMode { get; }

	float TransparencyThreshold { get; }

	Texture TransparencyMap { get; }

	void SetTransparencyModeAndMap(TransparencyMode mode, float threshold, Texture map);
}
