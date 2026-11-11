using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Effects;
using b;

namespace SynapseGaming.LightingSystem.Core;

public class EffectTypeCaster : ITypeCaster<Effect>
{
	public IRenderableEffect RenderableEffect;

	public ILightingEffect LightingEffect;

	public ISkinnedEffect SkinnedEffect;

	public IStaticLightingEffect StaticLightingEffect;

	public ITransparentEffect TransparentEffect;

	public ITerrainEffect TerrainEffect;

	public IAddressableEffect AddressableEffect;

	internal b.N l8a;

	public IShadowGenerateEffect ShadowGenerateEffect;

	public IDeferredObjectEffect DeferredObjectEffect;

	public BaseRenderableEffect BaseRenderableEffect;

	public BaseMaterialEffect BaseMaterialEffect;

	public BaseSasEffect BaseSasEffect;

	public Effect Effect;

	public IEffectFog EffectFog;

	public IEffectLights EffectLights;

	public IEffectMatrices EffectMatrices;

	public void Set(Effect effect)
	{
		RenderableEffect = effect as IRenderableEffect;
		LightingEffect = effect as ILightingEffect;
		SkinnedEffect = effect as ISkinnedEffect;
		StaticLightingEffect = effect as IStaticLightingEffect;
		TransparentEffect = effect as ITransparentEffect;
		TerrainEffect = effect as ITerrainEffect;
		AddressableEffect = effect as IAddressableEffect;
		l8a = effect as b.N;
		ShadowGenerateEffect = effect as IShadowGenerateEffect;
		DeferredObjectEffect = effect as IDeferredObjectEffect;
		BaseRenderableEffect = effect as BaseRenderableEffect;
		BaseMaterialEffect = effect as BaseMaterialEffect;
		BaseSasEffect = effect as BaseSasEffect;
		Effect = effect;
		EffectFog = effect as IEffectFog;
		EffectLights = effect as IEffectLights;
		EffectMatrices = effect as IEffectMatrices;
	}
}
