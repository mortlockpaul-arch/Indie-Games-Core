using Microsoft.Xna.Framework.Graphics;

namespace SynapseGaming.LightingSystem.Effects;

public interface IAddressableEffect
{
	TextureAddressMode AddressModeU { get; set; }

	TextureAddressMode AddressModeV { get; set; }

	TextureAddressMode AddressModeW { get; set; }
}
