using Microsoft.Xna.Framework.Graphics;

namespace SynapseGaming.LightingSystem.Effects.Forward;

public class SasEffect : BaseSasEffect
{
	internal SasEffect(GraphicsDevice param_0, byte[] param_1, bool param_2)
		: base(param_0, param_1, param_2)
	{
		EffectByteCode = param_1;
	}

	protected override Effect Create()
	{
		return new SasEffect(GraphicsDevice, EffectByteCode, param_2: true);
	}

	internal static SasEffect l5(GraphicsDevice param_0, byte[] param_1, bool param_2)
	{
		SasLightingEffect sasLightingEffect = new SasLightingEffect(param_0, param_1, param_2);
		if (sasLightingEffect.MaxLightSources > 0)
		{
			return sasLightingEffect;
		}
		sasLightingEffect.Dispose();
		return new SasEffect(param_0, param_1, param_2);
	}
}
