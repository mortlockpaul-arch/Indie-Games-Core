using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using y;

namespace SynapseGaming.LightingSystem.Shadows;

public abstract class BaseShadowEffectShadowMap : BaseShadowMap
{
	private Effect l8a;

	private Vector4[] l8_u0013_ = new Vector4[6];

	private Matrix[] l8N = new Matrix[6];

	public override Effect ShadowEffect => l8a;

	protected abstract Type GetEffectType();

	protected abstract Effect CreateEffect();

	public override void Build(GraphicsDevice device, ISceneState scenestate, ShadowGroup shadowgroup, IShadowMapVisibility shadowvisibility, float shadowquality)
	{
		base.Build(device, scenestate, shadowgroup, shadowvisibility, shadowquality);
		if (l8a == null)
		{
			l8a = ResourceManager._u0013__u0004_(GetEffectType(), CreateEffect);
		}
	}

	public override void Dispose()
	{
		y.a.w_u0005_(ref l8a);
		base.Dispose();
	}

	protected Vector4[] GetPackedRenderTargetLocationAndSpan(Texture2D shadowmap, int padding)
	{
		Vector4 vector = new Vector4(1f / (float)shadowmap.Width, 1f / (float)shadowmap.Height, 1f / (float)shadowmap.Width, 1f / (float)shadowmap.Height);
		for (int i = 0; i < Surfaces.Length; i++)
		{
			Rectangle rectangle = Surfaces[i].L_u0011_(padding);
			ref Vector4 reference = ref l8_u0013_[i];
			reference = new Vector4(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height) * vector;
		}
		return l8_u0013_;
	}

	protected Matrix[] GetPackedSurfaceViewProjection()
	{
		for (int i = 0; i < Surfaces.Length; i++)
		{
			ref Matrix reference = ref l8N[i];
			reference = Surfaces[i].WorldToSurfaceView * Surfaces[i].Projection;
		}
		return l8N;
	}
}
