using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Effects;
using SynapseGaming.LightingSystem.Lights;
using b;

namespace SynapseGaming.LightingSystem.Shadows;

public abstract class BaseShadowCubeMap : BaseShadowEffectShadowMap
{
	private const int l8a = 6;

	private const int l8_u0013_ = 8;

	private ShadowMapSurface[] l8N = new ShadowMapSurface[6];

	private Plane[] l8l = new Plane[6];

	private static bool l8L = false;

	private static Matrix[] l8w = new Matrix[6];

	private static Plane[] l8J = new Plane[6];

	public override ShadowMapSurface[] Surfaces => l8N;

	public override RenderTarget2D CustomRenderTarget => null;

	public BaseShadowCubeMap()
	{
		if (!l8L)
		{
			ref Matrix reference = ref l8w[0];
			reference = Matrix.CreateLookAt(Vector3.Zero, Vector3.UnitX, Vector3.UnitY);
			ref Matrix reference2 = ref l8w[1];
			reference2 = Matrix.CreateLookAt(Vector3.Zero, -Vector3.UnitX, Vector3.UnitY);
			ref Matrix reference3 = ref l8w[2];
			reference3 = Matrix.CreateLookAt(Vector3.Zero, Vector3.UnitY, Vector3.UnitZ);
			ref Matrix reference4 = ref l8w[3];
			reference4 = Matrix.CreateLookAt(Vector3.Zero, -Vector3.UnitY, Vector3.UnitZ);
			ref Matrix reference5 = ref l8w[4];
			reference5 = Matrix.CreateLookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
			ref Matrix reference6 = ref l8w[5];
			reference6 = Matrix.CreateLookAt(Vector3.Zero, -Vector3.UnitZ, Vector3.UnitY);
			for (int i = 0; i < l8J.Length; i++)
			{
				ref Plane reference7 = ref l8J[i];
				reference7 = Plane.Transform(new Plane(0f, 0f, 1f, 1f), Matrix.Invert(l8w[i]));
			}
			l8L = true;
		}
		for (int j = 0; j < l8N.Length; j++)
		{
			l8N[j] = new ShadowMapSurface();
		}
		l8N[0].WorldToSurfaceView = l8w[0];
		l8N[1].WorldToSurfaceView = l8w[1];
		l8N[2].WorldToSurfaceView = l8w[2];
		l8N[3].WorldToSurfaceView = l8w[3];
		l8N[4].WorldToSurfaceView = l8w[4];
		l8N[5].WorldToSurfaceView = l8w[5];
		for (int k = 0; k < l8N.Length; k++)
		{
			ref Plane reference8 = ref l8l[k];
			reference8 = l8J[k];
		}
	}

	public override void Build(GraphicsDevice device, ISceneState scenestate, ShadowGroup shadowgroup, IShadowMapVisibility shadowvisibility, float shadowquality)
	{
		base.Build(device, scenestate, shadowgroup, shadowvisibility, shadowquality);
		IShadowSource shadowSource = shadowgroup.ShadowSource;
		BoundingSphere boundingSphereCentered = shadowgroup.BoundingSphereCentered;
		Vector3 shadowPosition = shadowgroup.ShadowSource.ShadowPosition;
		float radius = boundingSphereCentered.Radius;
		l8N[0].L_u0003_(new Vector3(0f - shadowPosition.Z, 0f - shadowPosition.Y, shadowPosition.X));
		l8N[1].L_u0003_(new Vector3(shadowPosition.Z, 0f - shadowPosition.Y, 0f - shadowPosition.X));
		l8N[2].L_u0003_(new Vector3(0f - shadowPosition.X, 0f - shadowPosition.Z, shadowPosition.Y));
		l8N[3].L_u0003_(new Vector3(shadowPosition.X, 0f - shadowPosition.Z, 0f - shadowPosition.Y));
		l8N[4].L_u0003_(new Vector3(shadowPosition.X, 0f - shadowPosition.Y, shadowPosition.Z));
		l8N[5].L_u0003_(new Vector3(0f - shadowPosition.X, 0f - shadowPosition.Y, 0f - shadowPosition.Z));
		l8l[0].D = shadowPosition.X + radius;
		l8l[1].D = 0f - shadowPosition.X + radius;
		l8l[2].D = shadowPosition.Y + radius;
		l8l[3].D = 0f - shadowPosition.Y + radius;
		l8l[4].D = shadowPosition.Z + radius;
		l8l[5].D = 0f - shadowPosition.Z + radius;
		shadowPosition = SceneState.ViewToWorld.Translation;
		float num = 0f;
		for (int i = 0; i < l8N.Length; i++)
		{
			ShadowMapSurface shadowMapSurface = l8N[i];
			if (!shadowMapSurface.Enabled)
			{
				shadowMapSurface.LevelOfDetail = 0f;
				continue;
			}
			Plane plane = l8l[i];
			float num2 = plane.DotCoordinate(shadowPosition);
			Vector3 vector = shadowPosition - plane.Normal * num2;
			for (int j = 0; j < l8N.Length; j++)
			{
				Plane plane2 = l8l[j];
				float num3 = plane2.DotCoordinate(vector);
				if (num3 < 0f)
				{
					vector -= plane2.Normal * num3;
				}
			}
			num2 = (vector - shadowPosition).Length();
			float screenSize = CoreHelper.GetScreenSize(radius, num2, SceneState.Projection);
			shadowMapSurface.LevelOfDetail = MathHelper.Clamp(screenSize, 0f, 1f);
			num = Math.Max(num, shadowMapSurface.LevelOfDetail);
		}
		if (!shadowSource.ShadowPerSurfaceLOD)
		{
			ShadowMapSurface[] array = l8N;
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

	public override void SetSurfaceRenderTargetLocation(int surface, Rectangle location)
	{
		ShadowMapSurface shadowMapSurface = l8N[surface];
		shadowMapSurface.RenderTargetLocation = location;
		float num = (float)location.Width * 0.5f;
		float num2 = (float)shadowMapSurface.L_u0011_(8).Width * 0.5f;
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
		l8N[surface].Projection = projection;
	}

	public override bool IsSurfaceVisible(int surface, BoundingFrustum viewfrustum)
	{
		ShadowMapSurface shadowMapSurface = l8N[surface];
		return shadowMapSurface.Enabled;
	}

	public override void BeginRendering(Texture shadowmap)
	{
		BeginRendering(shadowmap, ShadowEffect);
	}

	public override void BeginRendering(Texture shadowmap, Effect shadoweffect)
	{
		EffectTypeCaster effectTypeCaster = OptimizationSystem.EffectTypeCasters.Get(shadoweffect);
		if (!(shadowmap is Texture2D shadowmap2))
		{
			effectTypeCaster.l8a.SetShadowMapAndType(null, b.B.Point);
			return;
		}
		IRenderableEffect renderableEffect = effectTypeCaster.RenderableEffect;
		b.N n = effectTypeCaster.l8a;
		IShadowGenerateEffect shadowGenerateEffect = effectTypeCaster.ShadowGenerateEffect;
		n.SetShadowMapAndType(shadowmap2, b.B.Point);
		renderableEffect?.SetViewAndProjection(SceneState.View, SceneState.ViewToWorld, SceneState.Projection, SceneState.ProjectionToView);
		if (shadowGenerateEffect != null)
		{
			shadowGenerateEffect.ShadowPrimaryBias = ShadowGroup.ShadowSource.ShadowPrimaryBias;
			shadowGenerateEffect.ShadowSecondaryBias = ShadowGroup.ShadowSource.ShadowSecondaryBias;
		}
		n.ShadowArea = ShadowGroup.BoundingSphereCentered;
		n.ShadowMapLocationAndSpan = GetPackedRenderTargetLocationAndSpan(shadowmap2, 8);
	}

	public override void EndRendering()
	{
	}

	public override void BeginSurfaceRendering(int surface)
	{
		BeginSurfaceRendering(surface, ShadowEffect);
	}

	public override void BeginSurfaceRendering(int surface, Effect shadoweffect)
	{
		EffectTypeCaster effectTypeCaster = OptimizationSystem.EffectTypeCasters.Get(shadoweffect);
		ShadowMapSurface shadowMapSurface = l8N[surface];
		IRenderableEffect renderableEffect = effectTypeCaster.RenderableEffect;
		b.N n = effectTypeCaster.l8a;
		IShadowGenerateEffect shadowGenerateEffect = effectTypeCaster.ShadowGenerateEffect;
		n?.SetShadowMapAndType(null, b.B.Point);
		renderableEffect?.SetViewAndProjection(shadowMapSurface.WorldToSurfaceView, Matrix.Identity, shadowMapSurface.Projection, SceneState.ProjectionToView);
		if (shadowGenerateEffect != null)
		{
			shadowGenerateEffect.ShadowPrimaryBias = ShadowGroup.ShadowSource.ShadowPrimaryBias;
			shadowGenerateEffect.ShadowSecondaryBias = ShadowGroup.ShadowSource.ShadowSecondaryBias;
			shadowGenerateEffect.ShadowArea = ShadowGroup.BoundingSphereCentered;
			shadowGenerateEffect.SetCameraView(SceneState.View, SceneState.ViewToWorld);
		}
		else if (n != null)
		{
			n.ShadowArea = ShadowGroup.BoundingSphereCentered;
		}
		Device.Viewport = l8N[surface].Viewport;
	}

	public override void EndSurfaceRendering()
	{
	}
}
