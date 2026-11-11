using Microsoft.Xna.Framework;
using SynapseGaming.LightingSystem.Core;

namespace SynapseGaming.LightingSystem.Effects;

public interface IRenderableEffect
{
	Matrix World { get; set; }

	Matrix View { get; set; }

	Matrix Projection { get; set; }

	bool DoubleSided { get; set; }

	DetailPreference EffectDetail { get; set; }

	void SetWorldAndWorldToObject(ref Matrix world, ref Matrix worldtoobj);

	void SetViewAndProjection(Matrix view, Matrix viewtoworld, Matrix projection, Matrix projectiontoview);
}
