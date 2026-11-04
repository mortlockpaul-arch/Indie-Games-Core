using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Lights;

namespace SynapseGaming.LightingSystem.Effects;

/// <summary>
/// Interface that provides custom effects with static lighting support.
/// </summary>
public interface IStaticLightingEffect
{
	/// <summary>
	/// Sets the static lighting applied to the effect during rendering.
	/// </summary>
	/// <param name="lightmap">Light map texture containing static
	/// lighting. If texture is not null the texture will override
	/// the provided composite lighting.</param>
	/// <param name="compositelighting">Composite lighting containing
	/// static lighting.</param>
	void SetStaticLighting(Texture2D lightmap, ref CompositeLighting compositelighting);
}
