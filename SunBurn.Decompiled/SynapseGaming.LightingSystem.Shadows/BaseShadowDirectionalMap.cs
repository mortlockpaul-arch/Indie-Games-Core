using System;
using L;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Effects;
using j;

namespace SynapseGaming.LightingSystem.Shadows;

/// <summary>
/// Shadow map class that implements cascading level-of-detail
/// directional shadows. Used for directional lights.
/// </summary>
public abstract class BaseShadowDirectionalMap : BaseShadowEffectShadowMap
{
	private const int fNB = 3;

	private float[] fNa = new float[4];

	private float fNg = 250f;

	private float fNf = 300f;

	private float fN_0003 = 300f;

	private Vector4 fNn;

	private ShadowMapSurface[] fN_0019 = new ShadowMapSurface[3];

	private BoundingFrustum fNL = new BoundingFrustum(Matrix.Identity);

	private Vector3[] fNv = new Vector3[8];

	/// <summary>
	/// Array of the level-of-detail surfaces.
	/// </summary>
	public override ShadowMapSurface[] Surfaces => fN_0019;

	/// <summary>
	/// Unused, this object supports render targets from the ShadowMapCache.
	/// </summary>
	public override RenderTarget2D CustomRenderTarget => null;

	/// <summary>
	/// Creates a new ShadowDirectionalMap instance.
	/// </summary>
	public BaseShadowDirectionalMap()
	{
		for (int k = 0; k < fN_0019.Length; k++)
		{
			fN_0019[k] = new ShadowMapSurface();
		}
	}

	/// <summary>
	/// Builds the shadow map information based on the provided scene state and shadow
	/// group, visibility, and quality.
	/// </summary>
	/// <param name="device"></param>
	/// <param name="scenestate"></param>
	/// <param name="shadowgroup">Shadow group used as the source for the shadow map.</param>
	/// <param name="shadowvisibility"></param>
	/// <param name="shadowquality">Shadow quality from 1.0 (highest) to 0.0 (lowest).</param>
	public override void Build(GraphicsDevice device, ISceneState scenestate, ShadowGroup shadowgroup, IShadowMapVisibility shadowvisibility, float shadowquality)
	{
		base.Build(device, scenestate, shadowgroup, shadowvisibility, shadowquality);
		fNg = scenestate.Environment.ShadowFadeStartDistance;
		fNf = scenestate.Environment.ShadowFadeEndDistance;
		fN_0003 = scenestate.Environment.ShadowCasterDistance;
		fNn.W = scenestate.Environment.ShadowFadeStartDistance;
		int num = Math.Min(fNa.Length - 1, shadowvisibility.ShadowLODRangeHints.Length);
		for (int k = 0; k < num; k++)
		{
			fNa[k + 1] = shadowvisibility.ShadowLODRangeHints[k];
		}
		bool[] shadowLODEnabled = shadowvisibility.ShadowLODEnabled;
		for (int l = 0; l < fN_0019.Length; l++)
		{
			ShadowMapSurface shadowMapSurface = fN_0019[l];
			if (shadowLODEnabled.Length > l && !shadowLODEnabled[l])
			{
				shadowMapSurface.Enabled = false;
				shadowMapSurface.LevelOfDetail = 0f;
			}
			else
			{
				shadowMapSurface.Enabled = true;
				shadowMapSurface.LevelOfDetail = 1f;
			}
		}
	}

	private BoundingBox no(Vector3[] P_0, Matrix P_1)
	{
		if (P_0.Length < 1)
		{
			return default;
		}
		Vector3 vector = Vector3.Transform(P_0[0], P_1);
		BoundingBox result = new BoundingBox(vector, vector);
		for (int k = 1; k < P_0.Length; k++)
		{
			vector = Vector3.Transform(P_0[k], P_1);
			result.Min = Vector3.Min(result.Min, vector);
			result.Max = Vector3.Max(result.Max, vector);
		}
		return result;
	}

	/// <summary>
	/// Sets the location in the shadow map render target the surface renders to.
	/// </summary>
	/// <param name="surface">Shadow map surface index.</param>
	/// <param name="location">Texel region used by the shadow map surface.</param>
	public override void SetSurfaceRenderTargetLocation(int surface, Rectangle location)
	{
		IShadowSource shadowSource = ShadowGroup.ShadowSource;
		ShadowMapSurface shadowMapSurface = fN_0019[surface];
		shadowMapSurface.RenderTargetLocation = location;
		float num = fNf * fNa[surface];
		float num2 = fNf * fNa[surface + 1];
		switch (surface)
		{
		case 0:
			fNn.X = num2;
			break;
		case 1:
			fNn.Y = num2;
			break;
		default:
			fNn.Z = num2;
			break;
		}
		fNL.Matrix = SceneState.ProjectionNonOblique;
		fNL.GetCorners(fNv);
		Plane plane = new Plane(0f, 0f, 1f, num);
		Plane plane2 = new Plane(0f, 0f, 1f, num2);
		Vector3 vector = default;
		for (int k = 0; k < 4; k++)
		{
			Vector3 vector2 = fNv[k];
			Vector3 vector3 = fNv[k + 4];
			if (L._0019.F(vector2, vector3, plane, ref vector))
			{
				fNv[k] = vector;
			}
			if (L._0019.F(vector2, vector3, plane2, ref vector))
			{
				fNv[k + 4] = vector;
			}
		}
		Vector3 position = fNv[0];
		for (int l = 1; l < 8; l++)
		{
			position += fNv[l];
		}
		position /= 8f;
		Matrix viewToWorld = SceneState.ViewToWorld;
		Vector3 vector4 = Vector3.Transform(position, viewToWorld);
		float num3 = fN_0003;
		Vector3 vector5 = vector4 - shadowSource.World.Forward * num3;
		Matrix matrix = Matrix.CreateTranslation(vector5);
		Matrix matrix2 = Matrix.Invert(matrix) * Matrix.Invert(shadowSource.World);
		Matrix matrix3 = viewToWorld * matrix2;
		for (int m = 0; m < 8; m++)
		{
			ref Vector3 reference = ref fNv[m];
			reference = Vector3.Transform(fNv[m], matrix3);
		}
		float num4 = Math.Max(Vector3.Distance(fNv[0], fNv[2]), Vector3.Distance(fNv[0], fNv[6]));
		L._0019.a_000F(fNv);
		shadowMapSurface.WorldToSurfaceView = matrix2;
		shadowMapSurface.Projection = Matrix.CreateOrthographic(num4, num4, num3 * 0.25f, num3 * 1.75f) * Matrix.CreateScale(-1f, 1f, 1f);
		int width = location.Width;
		Vector4 vector6 = Vector4.Transform(new Vector4(vector5, 1f), shadowMapSurface.Frustum.Matrix);
		vector6 = (vector6 + Vector4.One) * 0.5f;
		vector6 *= new Vector4(width);
		Vector4 vector7 = Vector4.Transform(new Vector4(Vector3.Zero, 1f), shadowMapSurface.Frustum.Matrix);
		vector7 = (vector7 + Vector4.One) * 0.5f;
		vector7 *= new Vector4(width);
		vector6.X += vector7.X % 1f;
		vector6.Y += vector7.Y % 1f;
		vector6 /= new Vector4(width);
		vector6 = vector6 * 2f - Vector4.One;
		vector6 = Vector4.Transform(vector6, Matrix.Invert(shadowMapSurface.Frustum.Matrix));
		Matrix matrix4 = Matrix.Invert(matrix2);
		matrix4.Translation = new Vector3(vector6.X, vector6.Y, vector6.Z);
		shadowMapSurface.WorldToSurfaceView = Matrix.Invert(matrix4);
	}

	/// <summary>
	/// Determines if the shadow map surface is visible to the provided view frustum.
	/// </summary>
	/// <param name="surface">Shadow map surface index.</param>
	/// <param name="viewfrustum"></param>
	/// <returns></returns>
	public override bool IsSurfaceVisible(int surface, BoundingFrustum viewfrustum)
	{
		return fN_0019[surface].Enabled;
	}

	private int nc(float P_0, int P_1, int P_2)
	{
		float num = P_0 * 0.5f + 0.5f;
		return (int)MathHelper.Clamp(num * (float)P_1 + (float)P_2, 0f, P_1);
	}

	/// <summary>
	/// Sets up the shadow map for rendering shadows to the scene.
	/// </summary>
	/// <param name="shadowmap"></param>
	public override void BeginRendering(Texture shadowmap)
	{
		BeginRendering(shadowmap, ShadowEffect);
	}

	/// <summary>
	/// Sets up the shadow map for rendering shadows to the scene.
	/// </summary>
	/// <param name="shadowmap"></param>
	/// <param name="shadoweffect">Custom shadow effect used in rendering.</param>
	public override void BeginRendering(Texture shadowmap, Effect shadoweffect)
	{
		if (!(shadowmap is Texture2D))
		{
			(shadoweffect as j.g).SetShadowMapAndType(null, j.L.Directional);
			return;
		}
		Texture2D shadowmap2 = shadowmap as Texture2D;
		IRenderableEffect renderableEffect = shadoweffect as IRenderableEffect;
		j.g g2 = shadoweffect as j.g;
		IShadowGenerateEffect shadowGenerateEffect = shadoweffect as IShadowGenerateEffect;
		g2.SetShadowMapAndType(shadowmap2, j.L.Directional);
		g2.ShadowViewDistance = fNn;
		renderableEffect?.SetViewAndProjection(SceneState.View, SceneState.ViewToWorld, SceneState.Projection, SceneState.ProjectionToView);
		if (shadowGenerateEffect != null)
		{
			shadowGenerateEffect.ShadowPrimaryBias = ShadowGroup.ShadowSource.ShadowPrimaryBias;
			shadowGenerateEffect.ShadowSecondaryBias = ShadowGroup.ShadowSource.ShadowSecondaryBias;
		}
		g2.ShadowArea = ShadowGroup.BoundingSphereCentered;
		g2.ShadowMapLocationAndSpan = GetPackedRenderTargetLocationAndSpan(shadowmap2, 0);
		g2.ShadowViewProjection = GetPackedSurfaceViewProjection();
	}

	/// <summary>
	/// Finalizes rendering.
	/// </summary>
	public override void EndRendering()
	{
	}

	/// <summary>
	/// Sets up the shadow map surface for generating the shadow map depth buffer.
	/// </summary>
	/// <param name="surface">Shadow map surface index.</param>
	public override void BeginSurfaceRendering(int surface)
	{
		BeginSurfaceRendering(surface, ShadowEffect);
	}

	/// <summary>
	/// Sets up the shadow map surface for generating the shadow map depth buffer.
	/// </summary>
	/// <param name="surface">Shadow map surface index.</param>
	/// <param name="shadoweffect">Custom shadow effect used in rendering.</param>
	public override void BeginSurfaceRendering(int surface, Effect shadoweffect)
	{
		ShadowMapSurface shadowMapSurface = fN_0019[surface];
		IRenderableEffect renderableEffect = shadoweffect as IRenderableEffect;
		j.g g2 = shadoweffect as j.g;
		IShadowGenerateEffect shadowGenerateEffect = shadoweffect as IShadowGenerateEffect;
		g2?.SetShadowMapAndType(null, j.L.Directional);
		renderableEffect?.SetViewAndProjection(shadowMapSurface.WorldToSurfaceView, Matrix.Identity, shadowMapSurface.Projection, SceneState.ProjectionToView);
		if (shadowGenerateEffect != null)
		{
			shadowGenerateEffect.ShadowPrimaryBias = ShadowGroup.ShadowSource.ShadowPrimaryBias;
			shadowGenerateEffect.ShadowSecondaryBias = ShadowGroup.ShadowSource.ShadowSecondaryBias;
			shadowGenerateEffect.ShadowArea = ShadowGroup.BoundingSphereCentered;
			shadowGenerateEffect.SetCameraView(SceneState.View, SceneState.ViewToWorld);
		}
		else if (g2 != null)
		{
			g2.ShadowArea = ShadowGroup.BoundingSphereCentered;
		}
		Device.Viewport = fN_0019[surface].Viewport;
	}

	/// <summary>
	/// Finalizes rendering.
	/// </summary>
	public override void EndSurfaceRendering()
	{
	}
}
