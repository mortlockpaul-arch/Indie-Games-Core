using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Effects;

namespace L;

internal class d : global::L.j<Effect>
{
	internal IRenderableEffect fNB;

	internal ILightingEffect fNa;

	internal ISkinnedEffect fNg;

	internal IStaticLightingEffect fNf;

	internal ITransparentEffect fN_0003;

	internal ITerrainEffect fNn;

	internal IAddressableEffect fN_0019;

	internal IDeferredObjectEffect fNL;

	internal IShadowGenerateEffect fNv;

	internal BaseRenderableEffect fN_0012;

	internal BaseMaterialEffect fN_0015;

	internal BaseSasEffect fN8;

	internal Effect fNi;

	internal IEffectFog fN_0005;

	internal IEffectLights fNj;

	internal IEffectMatrices fNo;

	public void Set(Effect effect)
	{
		fNB = effect as IRenderableEffect;
		fNa = effect as ILightingEffect;
		fNg = effect as ISkinnedEffect;
		fNf = effect as IStaticLightingEffect;
		fN_0003 = effect as ITransparentEffect;
		fNn = effect as ITerrainEffect;
		fN_0019 = effect as IAddressableEffect;
		fNL = effect as IDeferredObjectEffect;
		fNv = effect as IShadowGenerateEffect;
		fN_0012 = effect as BaseRenderableEffect;
		fN_0015 = effect as BaseMaterialEffect;
		fN8 = effect as BaseSasEffect;
		fNi = effect;
		fN_0005 = effect as IEffectFog;
		fNj = effect as IEffectLights;
		fNo = effect as IEffectMatrices;
	}
}
