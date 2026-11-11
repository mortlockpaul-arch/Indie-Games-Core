using Microsoft.Xna.Framework;

namespace SynapseGaming.LightingSystem.Shadows;

public interface IShadowSource
{
	ShadowType ShadowType { get; set; }

	Vector3 ShadowPosition { get; }

	float ShadowQuality { get; set; }

	float ShadowPrimaryBias { get; set; }

	float ShadowSecondaryBias { get; set; }

	bool ShadowPerSurfaceLOD { get; set; }

	bool ShadowRenderLightsTogether { get; }

	Matrix World { get; set; }

	int GetShadowSourceHashCode();
}
