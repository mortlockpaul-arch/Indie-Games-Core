using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Rendering;
using SynapseGaming.LightingSystem.Shadows;
using W;
using _u0004_;
using b;
using x;
using y;

namespace SynapseGaming.LightingSystem.Lights;

public class LightManager : BaseLightManager<ILightManager>, ILightManager, IWorldRenderableManager, IRenderableManager, IUpdatableManager, ILightQuery, IQuery<BaseLight>, IManagerService, IManager, IUnloadable, ISubmit<BaseLight>, ISubmit<IScene>
{
	private int l8a = 30;

	private int l8_u0013_ = 8;

	private bool l8N;

	private b.h l8l;

	private W._u0013_ l8L;

	private DetailPreference l8w;

	private List<BaseLight> l8J = new List<BaseLight>(64);

	public override Type ManagerType => SceneInterface.LightManagerType;

	public override int ManagerProcessOrder
	{
		get
		{
			return l8a;
		}
		set
		{
			l8a = value;
		}
	}

	public int VolumeLightSliceCount
	{
		get
		{
			return l8_u0013_;
		}
		set
		{
			l8_u0013_ = value;
			lO();
		}
	}

	public bool BillboardVolumeLightSlices
	{
		get
		{
			return l8N;
		}
		set
		{
			l8N = value;
		}
	}

	public LightManager(IManagerServiceProvider sceneinterface, BoundingBox worldboundingbox, int worldtreemaxdepth)
		: base(sceneinterface, worldboundingbox, worldtreemaxdepth)
	{
	}

	public LightManager(IManagerServiceProvider sceneinterface)
		: base(sceneinterface)
	{
	}

	public override void ApplyPreferences(ISystemPreferences preferences)
	{
		l8w = preferences.LightingDetail;
		base.ApplyPreferences(preferences);
	}

	private void lO()
	{
		y.a.w_u0005_(ref l8l);
		y.a.w_u0005_(ref l8L);
	}

	public void RenderVolumeLights()
	{
		l8J.Clear();
		Find(l8J, SceneState.ViewFrustum, ObjectFilter.EnabledDynamicAndStatic);
		RenderVolumeLights(l8J);
	}

	public void RenderVolumeLights(List<BaseLight> volumelights)
	{
		if (l8w != DetailPreference.High)
		{
			return;
		}
		GraphicsDevice graphicsDevice = SunBurnCoreSystem.Instance.GraphicsDeviceManager.GraphicsDevice;
		if (l8l == null)
		{
			l8l = new b.h(graphicsDevice);
			l8L = new W._u0013_(graphicsDevice, l8_u0013_);
		}
		graphicsDevice.BlendState = _u0004_.a.l8a;
		graphicsDevice.RasterizerState = RasterizerState.CullNone;
		graphicsDevice.DepthStencilState = DepthStencilState.DepthRead;
		graphicsDevice.SetVertexBuffer(l8L.VertexBuffer);
		x.h.LO(graphicsDevice);
		l8l.View = SceneState.View;
		l8l.Projection = SceneState.Projection;
		l8l.SetDepthRange(SceneState.ProjectionToView);
		FrameBuffers frameBuffers = SceneState.FrameBuffers;
		RenderTarget2D buffer = frameBuffers.GetBuffer(FrameBufferType.DeferredDepthAndSpecularPower, createmissing: false);
		if (buffer != null)
		{
			l8l.SceneDepthMap = buffer;
		}
		else
		{
			l8l.SceneDepthMap = null;
		}
		EffectPass effectPass = l8l.CurrentTechnique.Passes[0];
		foreach (BaseLight volumelight in volumelights)
		{
			if (volumelight == null)
			{
				continue;
			}
			LightTypeCaster lightTypeCaster = OptimizationSystem.LightTypeCasters.Get(volumelight);
			if (lightTypeCaster.SpotSource == null)
			{
				continue;
			}
			ISpotSource spotSource = lightTypeCaster.SpotSource;
			if (!(spotSource.Volume <= 0f))
			{
				if (!l8N)
				{
					l8l.World = volumelight.World;
				}
				else
				{
					Matrix matrix = Matrix.Invert(volumelight.World);
					Vector3 param_ = Vector3.Transform(SceneState.ViewToWorld.Translation, matrix);
					Matrix world = l_u000F_(Vector3.Zero, param_, Vector3.Backward) * Matrix.CreateRotationZ(MathHelper.ToRadians(45f)) * volumelight.World;
					l8l.World = world;
				}
				l8l.Color = volumelight.CompositeColorAndIntensity / l8_u0013_ * spotSource.Volume;
				l8l.SetAngleAndRadius(spotSource.Angle, spotSource.Radius);
				effectPass.Apply();
				l8L.S();
			}
		}
		graphicsDevice.BlendState = BlendState.Opaque;
		graphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
		graphicsDevice.DepthStencilState = DepthStencilState.Default;
	}

	private Matrix l_u000F_(Vector3 param_0, Vector3 param_1, Vector3 param_2)
	{
		Matrix result = default;
		Vector3 vector = new Vector3
		{
			X = param_0.X - param_1.X,
			Y = param_0.Y - param_1.Y,
			Z = param_0.Z - param_1.Z
		};
		float num = vector.LengthSquared();
		if (num < 0.0001f)
		{
			vector = Vector3.Forward;
		}
		else
		{
			Vector3.Multiply(ref vector, 1f / (float)Math.Sqrt(num), out vector);
		}
		Vector3 vector2 = param_2;
		Vector3.Dot(ref param_2, ref vector, out var result2);
		Vector3 result3;
		Vector3 result4;
		if (Math.Abs(result2) > 0.9982547f)
		{
			result2 = param_2.X * Vector3.Forward.X + param_2.Y * Vector3.Forward.Y + param_2.Z * Vector3.Forward.Z;
			result3 = ((Math.Abs(result2) > 0.9982547f) ? Vector3.Right : Vector3.Forward);
			Vector3.Cross(ref param_2, ref result3, out result4);
			result4.Normalize();
			Vector3.Cross(ref result4, ref param_2, out result3);
			result3.Normalize();
		}
		else
		{
			Vector3.Cross(ref param_2, ref vector, out result4);
			result4.Normalize();
			Vector3.Cross(ref result4, ref vector2, out result3);
			result3.Normalize();
		}
		result.M11 = result4.X;
		result.M12 = result4.Y;
		result.M13 = result4.Z;
		result.M14 = 0f;
		result.M21 = result3.X;
		result.M22 = result3.Y;
		result.M23 = result3.Z;
		result.M24 = 0f;
		result.M31 = vector2.X;
		result.M32 = vector2.Y;
		result.M33 = vector2.Z;
		result.M34 = 0f;
		result.M41 = param_0.X;
		result.M42 = param_0.Y;
		result.M43 = param_0.Z;
		result.M44 = 1f;
		return result;
	}

	public void SubmitStaticDirectionalLight(Vector3 diffusecolor, Vector3 direction, float intensity, ShadowType shadowtype, float shadowquality, float shadowprimarybias, float shadowsecondarybias)
	{
		DirectionalLight directionalLight = new DirectionalLight();
		directionalLight.DiffuseColor = diffusecolor;
		directionalLight.Intensity = intensity;
		directionalLight.Direction = direction;
		directionalLight.ShadowType = shadowtype;
		directionalLight.ShadowQuality = shadowquality;
		directionalLight.ShadowPrimaryBias = shadowprimarybias;
		directionalLight.ShadowSecondaryBias = shadowsecondarybias;
		Submit(directionalLight);
	}

	public void SubmitStaticPointLight(Vector3 diffusecolor, Vector3 position, float intensity, float radius, bool filllight, float falloffstrength, ShadowType shadowtype, float shadowquality, float shadowprimarybias, float shadowsecondarybias, IShadowSource shadowsource)
	{
		PointLight pointLight = new PointLight();
		pointLight.DiffuseColor = diffusecolor;
		pointLight.Position = position;
		pointLight.Intensity = intensity;
		pointLight.Radius = radius;
		pointLight.FillLight = filllight;
		pointLight.FalloffStrength = falloffstrength;
		pointLight.ShadowType = shadowtype;
		pointLight.ShadowQuality = shadowquality;
		pointLight.ShadowPrimaryBias = shadowprimarybias;
		pointLight.ShadowSecondaryBias = shadowsecondarybias;
		if (shadowsource != null)
		{
			pointLight.ShadowSource = shadowsource;
		}
		else
		{
			pointLight.ShadowSource = pointLight;
		}
		Submit(pointLight);
	}

	public void SubmitStaticSpotLight(Vector3 diffusecolor, Vector3 position, float intensity, float radius, Vector3 direction, float angle, bool filllight, float falloffstrength, ShadowType shadowtype, float shadowquality, float shadowprimarybias, float shadowsecondarybias, IShadowSource shadowsource)
	{
		SpotLight spotLight = new SpotLight();
		spotLight.DiffuseColor = diffusecolor;
		spotLight.Position = position;
		spotLight.Intensity = intensity;
		spotLight.Radius = radius;
		spotLight.Direction = direction;
		spotLight.Angle = angle;
		spotLight.FillLight = filllight;
		spotLight.FalloffStrength = falloffstrength;
		spotLight.ShadowType = shadowtype;
		spotLight.ShadowQuality = shadowquality;
		spotLight.ShadowPrimaryBias = shadowprimarybias;
		spotLight.ShadowSecondaryBias = shadowsecondarybias;
		if (shadowsource != null)
		{
			spotLight.ShadowSource = shadowsource;
		}
		else
		{
			spotLight.ShadowSource = spotLight;
		}
		Submit(spotLight);
	}

	public void SubmitStaticAmbientLight(Vector3 diffusecolor, float intensity)
	{
		AmbientLight ambientLight = new AmbientLight();
		ambientLight.DiffuseColor = diffusecolor;
		ambientLight.Intensity = intensity;
		Submit(ambientLight);
	}

	public override void Clear()
	{
		base.Clear();
		OptimizationSystem.Clear();
	}

	public override void Unload()
	{
		lO();
		base.Unload();
	}
}
