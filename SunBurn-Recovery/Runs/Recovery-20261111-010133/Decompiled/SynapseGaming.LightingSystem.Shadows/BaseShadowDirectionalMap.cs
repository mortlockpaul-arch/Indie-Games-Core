using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Effects;
using b;

namespace SynapseGaming.LightingSystem.Shadows;

public abstract class BaseShadowDirectionalMap : BaseShadowEffectShadowMap
{
	private const int l8a = 3;

	private float[] l8_u0013_ = new float[4];

	private float l8N = 250f;

	private float l8l = 300f;

	private float l8L = 300f;

	private Vector4 l8w;

	private ShadowMapSurface[] l8J = new ShadowMapSurface[3];

	private BoundingFrustum l8B = new BoundingFrustum(Matrix.Identity);

	private Vector3[] l8s = new Vector3[8];

	public override ShadowMapSurface[] Surfaces => l8J;

	public override RenderTarget2D CustomRenderTarget => null;

	public BaseShadowDirectionalMap()
	{
		for (int i = 0; i < l8J.Length; i++)
		{
			l8J[i] = new ShadowMapSurface();
		}
	}

	public override void Build(GraphicsDevice device, ISceneState scenestate, ShadowGroup shadowgroup, IShadowMapVisibility shadowvisibility, float shadowquality)
	{
		base.Build(device, scenestate, shadowgroup, shadowvisibility, shadowquality);
		l8N = scenestate.Environment.ShadowFadeStartDistance;
		l8l = scenestate.Environment.ShadowFadeEndDistance;
		l8L = scenestate.Environment.ShadowCasterDistance;
		l8w.W = scenestate.Environment.ShadowFadeStartDistance;
		int num = Math.Min(l8_u0013_.Length - 1, shadowvisibility.ShadowLODRangeHints.Length);
		for (int i = 0; i < num; i++)
		{
			l8_u0013_[i + 1] = shadowvisibility.ShadowLODRangeHints[i];
		}
		bool[] shadowLODEnabled = shadowvisibility.ShadowLODEnabled;
		for (int j = 0; j < l8J.Length; j++)
		{
			ShadowMapSurface shadowMapSurface = l8J[j];
			if (shadowLODEnabled.Length > j && !shadowLODEnabled[j])
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

	private BoundingBox Lm(Vector3[] param_0, Matrix param_1)
	{
		if (param_0.Length < 1)
		{
			return default;
		}
		Vector3 vector = Vector3.Transform(param_0[0], param_1);
		BoundingBox result = new BoundingBox(vector, vector);
		for (int i = 1; i < param_0.Length; i++)
		{
			vector = Vector3.Transform(param_0[i], param_1);
			result.Min = Vector3.Min(result.Min, vector);
			result.Max = Vector3.Max(result.Max, vector);
		}
		return result;
	}

	public override void SetSurfaceRenderTargetLocation(int surface, Rectangle location)
	{
		IShadowSource shadowSource = ShadowGroup.ShadowSource;
		ShadowMapSurface shadowMapSurface = l8J[surface];
		shadowMapSurface.RenderTargetLocation = location;
		float d = l8l * l8_u0013_[surface];
		float num = l8l * l8_u0013_[surface + 1];
		switch (surface)
		{
		case 0:
			l8w.X = num;
			break;
		case 1:
			l8w.Y = num;
			break;
		default:
			l8w.Z = num;
			break;
		}
		l8B.Matrix = SceneState.ProjectionNonOblique;
		l8B.GetCorners(l8s);
		Plane plane = new Plane(0f, 0f, 1f, d);
		Plane plane2 = new Plane(0f, 0f, 1f, num);
		Vector3 intersectionpoint = default;
		for (int i = 0; i < 4; i++)
		{
			Vector3 start = l8s[i];
			Vector3 end = l8s[i + 4];
			if (CoreHelper.Intersects(start, end, plane, ref intersectionpoint))
			{
				l8s[i] = intersectionpoint;
			}
			if (CoreHelper.Intersects(start, end, plane2, ref intersectionpoint))
			{
				l8s[i + 4] = intersectionpoint;
			}
		}
		Vector3 position = l8s[0];
		for (int j = 1; j < 8; j++)
		{
			position += l8s[j];
		}
		position /= 8f;
		Matrix viewToWorld = SceneState.ViewToWorld;
		Vector3 vector = Vector3.Transform(position, viewToWorld);
		float num2 = l8L;
		Vector3 vector2 = vector - shadowSource.World.Forward * num2;
		Matrix matrix = Matrix.CreateTranslation(vector2);
		Matrix matrix2 = Matrix.Invert(matrix) * Matrix.Invert(shadowSource.World);
		Matrix matrix3 = viewToWorld * matrix2;
		for (int k = 0; k < 8; k++)
		{
			ref Vector3 reference = ref l8s[k];
			reference = Vector3.Transform(l8s[k], matrix3);
		}
		float num3 = Math.Max(Vector3.Distance(l8s[0], l8s[2]), Vector3.Distance(l8s[0], l8s[6]));
		CoreHelper.CreateBoundingBoxFromPoints(l8s);
		shadowMapSurface.WorldToSurfaceView = matrix2;
		shadowMapSurface.Projection = Matrix.CreateOrthographic(num3, num3, num2 * 0.25f, num2 * 1.75f) * Matrix.CreateScale(-1f, 1f, 1f);
		int width = location.Width;
		Vector4 vector3 = Vector4.Transform(new Vector4(vector2, 1f), shadowMapSurface.Frustum.Matrix);
		vector3 = (vector3 + Vector4.One) * 0.5f;
		vector3 *= new Vector4(width);
		Vector4 vector4 = Vector4.Transform(new Vector4(Vector3.Zero, 1f), shadowMapSurface.Frustum.Matrix);
		vector4 = (vector4 + Vector4.One) * 0.5f;
		vector4 *= new Vector4(width);
		vector3.X += vector4.X % 1f;
		vector3.Y += vector4.Y % 1f;
		vector3 /= new Vector4(width);
		vector3 = vector3 * 2f - Vector4.One;
		vector3 = Vector4.Transform(vector3, Matrix.Invert(shadowMapSurface.Frustum.Matrix));
		Matrix matrix4 = Matrix.Invert(matrix2);
		matrix4.Translation = new Vector3(vector3.X, vector3.Y, vector3.Z);
		shadowMapSurface.WorldToSurfaceView = Matrix.Invert(matrix4);
	}

	public override bool IsSurfaceVisible(int surface, BoundingFrustum viewfrustum)
	{
		return l8J[surface].Enabled;
	}

	private int Ln(float param_0, int param_1, int param_2)
	{
		float num = param_0 * 0.5f + 0.5f;
		return (int)MathHelper.Clamp(num * (float)param_1 + (float)param_2, 0f, param_1);
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
			effectTypeCaster.l8a.SetShadowMapAndType(null, b.B.Directional);
			return;
		}
		IRenderableEffect renderableEffect = effectTypeCaster.RenderableEffect;
		b.N n = effectTypeCaster.l8a;
		IShadowGenerateEffect shadowGenerateEffect = effectTypeCaster.ShadowGenerateEffect;
		n.SetShadowMapAndType(shadowmap2, b.B.Directional);
		n.ShadowViewDistance = l8w;
		renderableEffect?.SetViewAndProjection(SceneState.View, SceneState.ViewToWorld, SceneState.Projection, SceneState.ProjectionToView);
		if (shadowGenerateEffect != null)
		{
			shadowGenerateEffect.ShadowPrimaryBias = ShadowGroup.ShadowSource.ShadowPrimaryBias;
			shadowGenerateEffect.ShadowSecondaryBias = ShadowGroup.ShadowSource.ShadowSecondaryBias;
		}
		n.ShadowArea = ShadowGroup.BoundingSphereCentered;
		n.ShadowMapLocationAndSpan = GetPackedRenderTargetLocationAndSpan(shadowmap2, 0);
		n.ShadowViewProjection = GetPackedSurfaceViewProjection();
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
		ShadowMapSurface shadowMapSurface = l8J[surface];
		IRenderableEffect renderableEffect = effectTypeCaster.RenderableEffect;
		b.N n = effectTypeCaster.l8a;
		IShadowGenerateEffect shadowGenerateEffect = effectTypeCaster.ShadowGenerateEffect;
		n?.SetShadowMapAndType(null, b.B.Directional);
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
		Device.Viewport = l8J[surface].Viewport;
	}

	public override void EndSurfaceRendering()
	{
	}
}
