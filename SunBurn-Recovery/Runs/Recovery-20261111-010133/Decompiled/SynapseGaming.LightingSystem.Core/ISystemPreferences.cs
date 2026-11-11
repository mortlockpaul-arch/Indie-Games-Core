namespace SynapseGaming.LightingSystem.Core;

public interface ISystemPreferences
{
	SamplingPreference TextureSampling { get; }

	int MaxAnisotropy { get; }

	DetailPreference ShadowDetail { get; }

	float ShadowQuality { get; }

	DetailPreference EffectDetail { get; }

	DetailPreference LightingDetail { get; }

	DetailPreference PostProcessingDetail { get; }
}
