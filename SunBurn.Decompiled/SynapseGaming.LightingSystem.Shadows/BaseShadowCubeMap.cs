using System;
using L;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Effects;
using SynapseGaming.LightingSystem.Lights;
using j;

namespace SynapseGaming.LightingSystem.Shadows;

/// <summary>
/// Shadow map class that implements cube-mapped shadows with
/// per surface level-of-detail. Used for point based lights.
/// </summary>
public abstract class BaseShadowCubeMap : BaseShadowEffectShadowMap
{
	private const int fNB = 6;

	private const int fNa = 8;

	private ShadowMapSurface[] fNg = new ShadowMapSurface[6];

	private Plane[] fNf = new Plane[6];

	private static bool fN_0003 = false;

	private static Matrix[] fNn = new Matrix[6];

	private static Plane[] fN_0019 = new Plane[6];

	/// <summary>
	/// Array of the cube-map surfaces.
	/// </summary>
	public override ShadowMapSurface[] Surfaces => fNg;

	/// <summary>
	/// Unused, this object supports render targets from the ShadowMapCache.
	/// </summary>
	public override RenderTarget2D CustomRenderTarget => null;

	/// <summary>
	/// Creates a new ShadowCubeMap instance.
	/// </summary>
	public BaseShadowCubeMap()
	{
		if (!fN_0003)
		{
			ref Matrix reference = ref fNn[0];
			reference = Matrix.CreateLookAt(Vector3.Zero, Vector3.UnitX, Vector3.UnitY);
			ref Matrix reference2 = ref fNn[1];
			reference2 = Matrix.CreateLookAt(Vector3.Zero, -Vector3.UnitX, Vector3.UnitY);
			ref Matrix reference3 = ref fNn[2];
			reference3 = Matrix.CreateLookAt(Vector3.Zero, Vector3.UnitY, Vector3.UnitZ);
			ref Matrix reference4 = ref fNn[3];
			reference4 = Matrix.CreateLookAt(Vector3.Zero, -Vector3.UnitY, Vector3.UnitZ);
			ref Matrix reference5 = ref fNn[4];
			reference5 = Matrix.CreateLookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
			ref Matrix reference6 = ref fNn[5];
			reference6 = Matrix.CreateLookAt(Vector3.Zero, -Vector3.UnitZ, Vector3.UnitY);
			for (int k = 0; k < fN_0019.Length; k++)
			{
				ref Plane reference7 = ref fN_0019[k];
				reference7 = Plane.Transform(new Plane(0f, 0f, 1f, 1f), Matrix.Invert(fNn[k]));
			}
			fN_0003 = true;
		}
		for (int l = 0; l < fNg.Length; l++)
		{
			fNg[l] = new ShadowMapSurface();
		}
		fNg[0].WorldToSurfaceView = fNn[0];
		fNg[1].WorldToSurfaceView = fNn[1];
		fNg[2].WorldToSurfaceView = fNn[2];
		fNg[3].WorldToSurfaceView = fNn[3];
		fNg[4].WorldToSurfaceView = fNn[4];
		fNg[5].WorldToSurfaceView = fNn[5];
		for (int m = 0; m < fNg.Length; m++)
		{
			ref Plane reference8 = ref fNf[m];
			reference8 = fN_0019[m];
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
		IShadowSource shadowSource = shadowgroup.ShadowSource;
		BoundingSphere boundingSphereCentered = shadowgroup.BoundingSphereCentered;
		Vector3 shadowPosition = shadowgroup.ShadowSource.ShadowPosition;
		float radius = boundingSphereCentered.Radius;
		fNg[0].nj(new Vector3(0f - shadowPosition.Z, 0f - shadowPosition.Y, shadowPosition.X));
		fNg[1].nj(new Vector3(shadowPosition.Z, 0f - shadowPosition.Y, 0f - shadowPosition.X));
		fNg[2].nj(new Vector3(0f - shadowPosition.X, 0f - shadowPosition.Z, shadowPosition.Y));
		fNg[3].nj(new Vector3(shadowPosition.X, 0f - shadowPosition.Z, 0f - shadowPosition.Y));
		fNg[4].nj(new Vector3(shadowPosition.X, 0f - shadowPosition.Y, shadowPosition.Z));
		fNg[5].nj(new Vector3(0f - shadowPosition.X, 0f - shadowPosition.Y, 0f - shadowPosition.Z));
		fNf[0].D = shadowPosition.X + radius;
		fNf[1].D = 0f - shadowPosition.X + radius;
		fNf[2].D = shadowPosition.Y + radius;
		fNf[3].D = 0f - shadowPosition.Y + radius;
		fNf[4].D = shadowPosition.Z + radius;
		fNf[5].D = 0f - shadowPosition.Z + radius;
		shadowPosition = SceneState.ViewToWorld.Translation;
		float num = 0f;
		for (int k = 0; k < fNg.Length; k++)
		{
			ShadowMapSurface shadowMapSurface = fNg[k];
			if (!shadowMapSurface.Enabled)
			{
				shadowMapSurface.LevelOfDetail = 0f;
				continue;
			}
			Plane plane = fNf[k];
			float num2 = plane.DotCoordinate(shadowPosition);
			Vector3 vector = shadowPosition - plane.Normal * num2;
			for (int l = 0; l < fNg.Length; l++)
			{
				Plane plane2 = fNf[l];
				float num3 = plane2.DotCoordinate(vector);
				if (num3 < 0f)
				{
					vector -= plane2.Normal * num3;
				}
			}
			num2 = (vector - shadowPosition).Length();
			float value = L._0019.aK(radius, num2, SceneState.Projection);
			shadowMapSurface.LevelOfDetail = MathHelper.Clamp(value, 0f, 1f);
			num = Math.Max(num, shadowMapSurface.LevelOfDetail);
		}
		if (!shadowSource.ShadowPerSurfaceLOD)
		{
			ShadowMapSurface[] array = fNg;
			foreach (ShadowMapSurface shadowMapSurface2 in array)
			{
				shadowMapSurface2.LevelOfDetail = num;
			}
		}
		if (ShadowEffect is IRenderableEffect)
		{
			(ShadowEffect as IRenderableEffect).World = Matrix.Identity;
		}
		if (ShadowEffect is IShadowGenerateEffect)
		{
			(ShadowEffect as IShadowGenerateEffect).ShadowArea = shadowgroup.BoundingSphereCentered;
		}
	}

	/// <summary>
	/// Sets the location in the shadow map render target the surface renders to.
	/// </summary>
	/// <param name="surface">Shadow map surface index.</param>
	/// <param name="location">Texel region used by the shadow map surface.</param>
	public override void SetSurfaceRenderTargetLocation(int surface, Rectangle location)
	{
		ShadowMapSurface shadowMapSurface = fNg[surface];
		shadowMapSurface.RenderTargetLocation = location;
		float num = (float)location.Width * 0.5f;
		float num2 = (float)shadowMapSurface.n_0005(8).Width * 0.5f;
		float fieldOfView = ((!(num2 > 0f)) ? MathHelper.ToRadians(90f) : ((float)Math.Atan(num / num2) * 2f));
		float num3 = 10000f;
		if (ShadowGroup.ShadowSource is IPointSource)
		{
			num3 = ShadowGroup.BoundingSphereCentered.Radius;
		}
		if (num3 <= 0f)
		{
			num3 = 1E-05f;
		}
		float nearPlaneDistance = num3 * 1E-05f;
		Matrix projection = Matrix.CreatePerspectiveFieldOfView(fieldOfView, 1f, nearPlaneDistance, num3);
		projection.M11 *= -1f;
		fNg[surface].Projection = projection;
	}

	/// <summary>
	/// Determines if the shadow map surface is visible to the provided view frustum.
	/// </summary>
	/// <param name="surface">Shadow map surface index.</param>
	/// <param name="viewfrustum"></param>
	/// <returns></returns>
	public override bool IsSurfaceVisible(int surface, BoundingFrustum viewfrustum)
	{
		ShadowMapSurface shadowMapSurface = fNg[surface];
		return shadowMapSurface.Enabled;
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
			(shadoweffect as j.g).SetShadowMapAndType(null, j.L.Point);
			return;
		}
		Texture2D shadowmap2 = shadowmap as Texture2D;
		IRenderableEffect renderableEffect = shadoweffect as IRenderableEffect;
		j.g g2 = shadoweffect as j.g;
		IShadowGenerateEffect shadowGenerateEffect = shadoweffect as IShadowGenerateEffect;
		g2.SetShadowMapAndType(shadowmap2, j.L.Point);
		renderableEffect?.SetViewAndProjection(SceneState.View, SceneState.ViewToWorld, SceneState.Projection, SceneState.ProjectionToView);
		if (shadowGenerateEffect != null)
		{
			shadowGenerateEffect.ShadowPrimaryBias = ShadowGroup.ShadowSource.ShadowPrimaryBias;
			shadowGenerateEffect.ShadowSecondaryBias = ShadowGroup.ShadowSource.ShadowSecondaryBias;
		}
		g2.ShadowArea = ShadowGroup.BoundingSphereCentered;
		g2.ShadowMapLocationAndSpan = GetPackedRenderTargetLocationAndSpan(shadowmap2, 8);
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
		ShadowMapSurface shadowMapSurface = fNg[surface];
		IRenderableEffect renderableEffect = shadoweffect as IRenderableEffect;
		j.g g2 = shadoweffect as j.g;
		IShadowGenerateEffect shadowGenerateEffect = shadoweffect as IShadowGenerateEffect;
		g2?.SetShadowMapAndType(null, j.L.Point);
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
		Device.Viewport = fNg[surface].Viewport;
	}

	/// <summary>
	/// Finalizes rendering.
	/// </summary>
	public override void EndSurfaceRendering()
	{
	}
}
