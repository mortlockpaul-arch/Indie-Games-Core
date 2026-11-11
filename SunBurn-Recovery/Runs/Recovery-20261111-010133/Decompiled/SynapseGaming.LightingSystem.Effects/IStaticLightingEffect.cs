using SynapseGaming.LightingSystem.Lights;

namespace SynapseGaming.LightingSystem.Effects;

public interface IStaticLightingEffect
{
	void SetStaticLighting(StaticLightingEffectMode lightingmode, LightMap lightmap, ref CompositeLighting compositelighting);

	void SetStaticLighting(StaticLightingEffectMode lightingmode, LightMap lightmap);
}
