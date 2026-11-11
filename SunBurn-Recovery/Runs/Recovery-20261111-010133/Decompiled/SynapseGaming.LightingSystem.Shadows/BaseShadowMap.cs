using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;

namespace SynapseGaming.LightingSystem.Shadows;

public abstract class BaseShadowMap : IShadowMap, IDisposable
{
	private GraphicsDevice l8a;

	private ISceneState l8_u0013_;

	private ShadowGroup l8N;

	public virtual bool ContentsAreValid
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	public abstract ShadowMapSurface[] Surfaces { get; }

	public abstract Effect ShadowEffect { get; }

	public abstract RenderTarget2D CustomRenderTarget { get; }

	protected GraphicsDevice Device => l8a;

	protected ISceneState SceneState => l8_u0013_;

	protected ShadowGroup ShadowGroup => l8N;

	public virtual void Build(GraphicsDevice device, ISceneState scenestate, ShadowGroup shadowgroup, IShadowMapVisibility shadowvisibility, float shadowquality)
	{
		l8a = device;
		l8_u0013_ = scenestate;
		l8N = shadowgroup;
	}

	public virtual void Dispose()
	{
		l8a = null;
		l8N = null;
	}

	public abstract void BeginRendering(Texture shadowmap);

	public abstract void BeginRendering(Texture shadowmap, Effect shadoweffect);

	public abstract void EndRendering();

	public abstract bool IsSurfaceVisible(int surface, BoundingFrustum viewfrustum);

	public abstract void SetSurfaceRenderTargetLocation(int surface, Rectangle location);

	public abstract void BeginSurfaceRendering(int surface);

	public abstract void BeginSurfaceRendering(int surface, Effect shadoweffect);

	public abstract void EndSurfaceRendering();
}
