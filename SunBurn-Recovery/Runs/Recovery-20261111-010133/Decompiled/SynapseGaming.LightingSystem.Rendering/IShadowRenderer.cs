using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Shadows;

namespace SynapseGaming.LightingSystem.Rendering;

public interface IShadowRenderer
{
	void BeginShadowGroupRendering(ShadowGroup shadowgroup);

	bool RenderToShadowMapSurface(ShadowGroup shadowgroup, ShadowMapSurface surface, Effect shadoweffect);

	void EndShadowGroupRendering(ShadowGroup shadowgroup);
}
