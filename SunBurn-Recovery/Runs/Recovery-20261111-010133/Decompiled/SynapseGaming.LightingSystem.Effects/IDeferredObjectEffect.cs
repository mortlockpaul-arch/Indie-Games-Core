using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Lights;

namespace SynapseGaming.LightingSystem.Effects;

public interface IDeferredObjectEffect
{
	DeferredEffectOutput DeferredEffectOutput { get; set; }

	Texture2D SceneLightingDiffuseMap { get; set; }

	Texture2D SceneLightingSpecularMap { get; set; }

	DetailPreference EffectDetail { get; set; }

	bool FogEnabled { get; set; }

	float FogStartDistance { get; set; }

	float FogEndDistance { get; set; }

	Vector3 FogColor { get; set; }

	void SetAmbientLighting(IAmbientSource light, Vector3 directionhint);
}
