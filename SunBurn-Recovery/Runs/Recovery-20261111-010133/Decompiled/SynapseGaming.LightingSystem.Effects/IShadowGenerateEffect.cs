using Microsoft.Xna.Framework;

namespace SynapseGaming.LightingSystem.Effects;

public interface IShadowGenerateEffect
{
	bool SupportsShadowGeneration { get; }

	float ShadowPrimaryBias { get; set; }

	float ShadowSecondaryBias { get; set; }

	BoundingSphere ShadowArea { set; }

	void SetCameraView(Matrix view, Matrix viewtoworld);
}
