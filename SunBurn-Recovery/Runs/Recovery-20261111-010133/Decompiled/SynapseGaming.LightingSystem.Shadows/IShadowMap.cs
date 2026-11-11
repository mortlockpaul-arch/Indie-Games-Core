using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;

namespace SynapseGaming.LightingSystem.Shadows;

public interface IShadowMap : IDisposable
{
	bool ContentsAreValid { get; set; }

	ShadowMapSurface[] Surfaces { get; }

	RenderTarget2D CustomRenderTarget { get; }

	Effect ShadowEffect { get; }

	void Build(GraphicsDevice device, ISceneState scenestate, ShadowGroup shadowgroup, IShadowMapVisibility shadowvisibility, float shadowquality);

	void BeginRendering(Texture shadowmap);

	void BeginRendering(Texture shadowmap, Effect shadoweffect);

	void EndRendering();

	bool IsSurfaceVisible(int surface, BoundingFrustum viewfrustum);

	void SetSurfaceRenderTargetLocation(int surface, Rectangle location);

	void BeginSurfaceRendering(int surface);

	void BeginSurfaceRendering(int surface, Effect shadoweffect);

	void EndSurfaceRendering();
}
