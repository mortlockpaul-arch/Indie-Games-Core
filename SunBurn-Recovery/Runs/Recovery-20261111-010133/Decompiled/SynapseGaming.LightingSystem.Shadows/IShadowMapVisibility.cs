namespace SynapseGaming.LightingSystem.Shadows;

public interface IShadowMapVisibility
{
	float[] ShadowLODRangeHints { get; }

	bool[] ShadowLODEnabled { get; }
}
