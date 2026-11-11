using System;
using System.Collections.Generic;
using System.Diagnostics;
using B;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Editor;
using SynapseGaming.LightingSystem.Lights;
using SynapseGaming.LightingSystem.Shadows;
using _u0019_;

namespace SynapseGaming.LightingSystem.Rendering;

public abstract class BaseRenderManager : IRenderManager, IRenderableManager, IManagerService, IManager, IUnloadable
{
	internal enum w8a
	{
		Solid,
		TransparentSingleSided,
		TransparentDoubleSided
	}

	internal class w8_u0013_
	{
		internal SystemStatistic l8a = SystemConsole.GetStatistic("Renderer_PolysRendered", SystemStatisticCategory.Rendering);

		internal SystemStatistic l8_u0013_ = SystemConsole.GetStatistic("Renderer_SceneObjectsRendered", SystemStatisticCategory.Rendering);

		internal SystemStatistic l8N = SystemConsole.GetStatistic("Renderer_MeshesRendered", SystemStatisticCategory.Rendering);

		internal SystemStatistic l8l = SystemConsole.GetStatistic("Renderer_Batches", SystemStatisticCategory.Rendering);

		internal SystemStatistic l8L = SystemConsole.GetStatistic("Renderer_BatchPasses", SystemStatisticCategory.Rendering);

		internal SystemStatistic l8w = SystemConsole.GetStatistic("Renderer_BatchCullModeChanges", SystemStatisticCategory.Rendering);

		internal SystemStatistic l8J = SystemConsole.GetStatistic("Renderer_BatchEffectCommitChanges", SystemStatisticCategory.Rendering);

		internal SystemStatistic l8B = SystemConsole.GetStatistic("Light_LightsRendered", SystemStatisticCategory.Lighting);

		internal SystemStatistic l8s = SystemConsole.GetStatistic("Light_LightsRenderedAsGroup", SystemStatisticCategory.Lighting);

		internal SystemStatistic l8h = SystemConsole.GetStatistic("Shadow_ShadowGroupsRendered", SystemStatisticCategory.Shadowing);

		internal SystemStatistic l8_u0002_ = SystemConsole.GetStatistic("Shadow_ShadowMapPagesProcessed", SystemStatisticCategory.Shadowing);

		internal SystemStatistic l8v = SystemConsole.GetStatistic("Shadow_ShadowMapsProcessed", SystemStatisticCategory.Shadowing);

		internal SystemStatistic l8Y = SystemConsole.GetStatistic("Shadow_ShadowMapFacesProcessed", SystemStatisticCategory.Shadowing);

		internal SystemStatistic l8_u0005_ = SystemConsole.GetStatistic("Shadow_ShadowMapFacesFilled", SystemStatisticCategory.Shadowing);
	}

	protected const float _CompositeLightingBlendAmount = 0.25f;

	private int l8a = 60;

	private DetailPreference l8_u0013_ = DetailPreference.Medium;

	private DetailPreference l8N;

	protected bool _AvoidXNAGarbageCollectionMemoryLeak;

	private bool l8l;

	private bool l8L = true;

	private FillMode l8w;

	private ISceneState l8J;

	private int l8B = 4;

	private TextureFilter l8s;

	private IManagerServiceProvider l8h;

	private TransparencyRenderNodeSorter l8_u0002_ = new TransparencyRenderNodeSorter();

	private B.h<RenderableMeshTransparencyRenderNode> l8v = new B.h<RenderableMeshTransparencyRenderNode>();

	private B.h<VolumeLightTransparencyRenderNode> l8Y = new B.h<VolumeLightTransparencyRenderNode>();

	private ShadowRenderTargetGroup l8_u0005_ = new ShadowRenderTargetGroup();

	private _u0019_.a l8b;

	private List<ShadowRenderTargetGroup> l8I = new List<ShadowRenderTargetGroup>();

	private List<BaseLight> l85 = new List<BaseLight>();

	private List<BaseLight> l8W = new List<BaseLight>();

	private AmbientLight l8Z = new AmbientLight();

	internal w8_u0013_ l8T = new w8_u0013_();

	public Type ManagerType => SceneInterface.RenderManagerType;

	public int ManagerProcessOrder
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

	public IManagerServiceProvider OwnerSceneInterface => l8h;

	public bool HideXNARuntimeExceptions
	{
		get
		{
			return l8l;
		}
		set
		{
			l8l = value;
		}
	}

	public bool AvoidXNAGarbageCollectionMemoryLeak
	{
		get
		{
			return _AvoidXNAGarbageCollectionMemoryLeak;
		}
		set
		{
			_AvoidXNAGarbageCollectionMemoryLeak = value;
		}
	}

	public bool ClearBackBufferEnabled
	{
		get
		{
			return l8L;
		}
		set
		{
			l8L = value;
		}
	}

	public FillMode RenderFillMode
	{
		get
		{
			return l8w;
		}
		set
		{
			l8w = value;
		}
	}

	public AmbientLight FrameAmbientLight => l8Z;

	public List<BaseLight> FrameLights => l85;

	public List<ShadowRenderTargetGroup> FrameShadowRenderTargetGroups => l8I;

	protected int MaxAnisotropy => l8B;

	protected ISceneState SceneState => l8J;

	protected DetailPreference ShadowDetail => l8_u0013_;

	protected DetailPreference EffectDetail => l8N;

	protected TextureFilter Filter => l8s;

	protected List<BaseLight> FrameAmbientLights => l8W;

	protected IManagerServiceProvider ServiceProvider => l8h;

	protected TransparencyRenderNodeSorter TransparencyRenderNodes => l8_u0002_;

	public BaseRenderManager(IManagerServiceProvider sceneinterface)
	{
		l8h = sceneinterface;
		l8b = new _u0019_.a(sceneinterface);
	}

	public virtual void ApplyPreferences(ISystemPreferences preferences)
	{
		switch (preferences.TextureSampling)
		{
		case SamplingPreference.Point:
			l8s = TextureFilter.PointMipLinear;
			break;
		case SamplingPreference.Bilinear:
			l8s = TextureFilter.LinearMipPoint;
			break;
		case SamplingPreference.Trilinear:
			l8s = TextureFilter.Linear;
			break;
		case SamplingPreference.Anisotropic:
			l8s = TextureFilter.Anisotropic;
			break;
		}
		l8B = preferences.MaxAnisotropy;
		l8_u0013_ = preferences.ShadowDetail;
		l8N = preferences.EffectDetail;
		l8b.ApplyPreferences(preferences);
	}

	internal abstract void r4_u0015__u0011_1(List<RenderableMesh> param_0, bool param_1);

	protected abstract bool CanBatchTransparencies(ISceneState scenestate);

	protected virtual void ExtractAndFilterRenderableMeshes(List<SceneEntity> entities, ILightManager lightmanager, bool editorisopen, List<RenderableMesh> rendermeshes, Dictionary<object, CompositeLighting> renderingcompositelightingcache, Dictionary<object, CompositeLighting> transparentcompositelightingcache)
	{
		LightingSystemPerformance.Begin("BaseRenderManager.ExtractAndFilterRenderableMeshes");
		bool flag = lightmanager != null;
		Vector3 value = l8J.ViewToWorld.Translation;
		for (int i = 0; i < entities.Count; i++)
		{
			SceneEntity sceneEntity = entities[i];
			if (sceneEntity == null)
			{
				continue;
			}
			SceneEntityTypeCaster sceneEntityTypeCaster = OptimizationSystem.SceneEntityTypeCasters.Get(sceneEntity);
			ISceneObject sceneObject = sceneEntityTypeCaster.SceneObject;
			if (sceneObject == null || (!sceneObject.Visible && (!sceneObject.VisibleInEditor || !editorisopen)))
			{
				continue;
			}
			BoundingSphere worldBoundingSphere = sceneObject.WorldBoundingSphere;
			float num = l8J.Environment.VisibleDistance + worldBoundingSphere.Radius;
			float num2 = num * num;
			Vector3.DistanceSquared(ref value, ref worldBoundingSphere.Center, out var result);
			if (result > num2)
			{
				continue;
			}
			ushort num3 = 0;
			if (num2 > 0f)
			{
				num3 = (ushort)(result / num2 * 65535f);
			}
			l8T.l8_u0013_.AccumulationValue++;
			bool flag2 = false;
			bool flag3 = false;
			for (int j = 0; j < sceneObject.RenderableMeshes.Count; j++)
			{
				RenderableMesh renderableMesh = sceneObject.RenderableMeshes[j];
				if (renderableMesh.l8_u0002_ != null)
				{
					EffectTypeCaster effectTypeCaster = OptimizationSystem.EffectTypeCasters.Get(renderableMesh.l8_u0002_);
					if (effectTypeCaster.BaseSasEffect != null)
					{
						effectTypeCaster.BaseSasEffect.GameTime = l8J.GameTime;
					}
					if (effectTypeCaster.TransparentEffect != null && (effectTypeCaster.TransparentEffect.TransparencyMode == TransparencyMode.Blend || effectTypeCaster.TransparentEffect.TransparencyMode == TransparencyMode.Additive))
					{
						RenderableMeshTransparencyRenderNode renderableMeshTransparencyRenderNode = l8v.New();
						renderableMeshTransparencyRenderNode.Build(this, l8J, renderableMesh);
						l8_u0002_.Add(renderableMeshTransparencyRenderNode);
						flag3 = true;
					}
					else
					{
						ushort num4 = (ushort)(renderableMesh.l8Z >> 16);
						ushort num5 = (ushort)renderableMesh.l8Z;
						int num6 = (num5 ^ 1) + (num4 ^ 2) << 16;
						renderableMesh.l8T = num3 | num6;
						rendermeshes.Add(renderableMesh);
						flag2 = true;
					}
				}
			}
			if (!flag)
			{
				continue;
			}
			StaticLightingType staticLightingType = sceneObject.StaticLightingType;
			if (flag2 && (staticLightingType == StaticLightingType.Composite || staticLightingType == StaticLightingType.Custom) && !renderingcompositelightingcache.ContainsKey(sceneObject))
			{
				CompositeLighting compositelighting = default;
				BoundingBox worldbounds = sceneObject.WorldBoundingBox;
				lightmanager.GetCompositeLighting(ref worldbounds, 0.25f, LightingType.BakedDown, out compositelighting);
				if (staticLightingType == StaticLightingType.Custom)
				{
					float num7 = MathHelper.Clamp(0.25f, 0f, 1f);
					float num8 = 1f - num7;
					Vector3 vector = sceneObject.CustomStaticLightingColor * 2f;
					compositelighting.AmbientColor = vector * num7;
					compositelighting.DiffuseColor = vector * num8;
				}
				renderingcompositelightingcache.Add(sceneObject, compositelighting);
			}
			if (flag3 && !transparentcompositelightingcache.ContainsKey(sceneObject))
			{
				CompositeLighting compositelighting2 = default;
				StaticLightingType staticLightingType2 = staticLightingType;
				LightingType lightingtype = ((staticLightingType2 == StaticLightingType.BakedDown) ? LightingType.RealTime : (LightingType.RealTime | LightingType.BakedDown));
				BoundingBox worldbounds2 = sceneObject.WorldBoundingBox;
				lightmanager.GetCompositeLighting(ref worldbounds2, 0.25f, lightingtype, out compositelighting2);
				if (staticLightingType == StaticLightingType.Custom)
				{
					float num9 = MathHelper.Clamp(0.25f, 0f, 1f);
					float num10 = 1f - num9;
					Vector3 vector2 = sceneObject.CustomStaticLightingColor * 2f;
					compositelighting2.AmbientColor = vector2 * num9;
					compositelighting2.DiffuseColor = vector2 * num10;
				}
				transparentcompositelightingcache.Add(sceneObject, compositelighting2);
			}
		}
	}

	protected virtual void OnXNARuntimeException(Exception e, string contextmessage)
	{
		if (l8l || !Debugger.IsAttached || SunBurnEditor.EditorAttachedStatic)
		{
			return;
		}
		string text = $"XNA raised the following runtime exception: \"{e.Message}\". To ignore future errors disable HideXNARuntimeExceptions on the RenderManager.";
		if (!string.IsNullOrEmpty(contextmessage))
		{
			text = $"{contextmessage} {text}";
		}
		throw new Exception(text, e);
	}

	protected void GetDefaultShadows(List<ShadowRenderTargetGroup> rendertargetgroups, List<BaseLight> lights)
	{
		l8_u0005_.ShadowGroups.Clear();
		l8b.BuildShadowGroups(l8_u0005_.ShadowGroups, lights, usedefaultgrouping: true);
		l8_u0005_.Build(null);
		rendertargetgroups.Add(l8_u0005_);
	}

	protected abstract void BuildShadowMaps(List<ShadowRenderTargetGroup> shadowrendertargetgroups);

	public virtual void BeginFrameRendering(ISceneState scenestate)
	{
		LightingSystemPerformance.Begin("BaseRenderManager.BeginFrameRendering");
		SplashScreen.L_u0015_();
		l8J = scenestate;
		GraphicsDevice graphicsDevice = SunBurnCoreSystem.Instance.GraphicsDeviceManager.GraphicsDevice;
		SunBurnCoreSystem instance = SunBurnCoreSystem.Instance;
		instance._u0013_M(scenestate.FrameBuffers);
		GraphicsDeviceSupport graphicsDeviceSupport = instance.GetGraphicsDeviceSupport();
		l8B = Math.Max(Math.Min(l8B, graphicsDeviceSupport.MaxAnisotropy), 1);
		for (int i = 0; i < 8; i++)
		{
			graphicsDevice.SamplerStates[i] = SamplerState.AnisotropicWrap;
		}
		l8b.BeginFrameRendering(scenestate);
		l85.Clear();
		l8W.Clear();
		ILightManager lightManager = (ILightManager)l8h.GetManager(SceneInterface.LightManagerType, required: false);
		if (lightManager == null)
		{
			l8Z.DiffuseColor = new Vector3(1f, 0.9f, 0.8f);
			l8Z.Intensity = 0.25f;
		}
		else
		{
			lightManager.Find(l85, l8J.ViewFrustum, ObjectFilter.EnabledDynamicAndStatic);
			l8Z.DiffuseColor = Vector3.Zero;
			l8Z.Intensity = 1f;
			int num = 0;
			float num2 = 0f;
			Matrix viewToWorld = scenestate.ViewToWorld;
			for (int j = 0; j < l85.Count; j++)
			{
				BaseLight baseLight = l85[j];
				if (baseLight == null)
				{
					continue;
				}
				LightTypeCaster lightTypeCaster = OptimizationSystem.LightTypeCasters.Get(baseLight);
				if (lightTypeCaster.SpotSource != null && lightTypeCaster.SpotSource.Volume > 0f)
				{
					VolumeLightTransparencyRenderNode volumeLightTransparencyRenderNode = l8Y.New();
					volumeLightTransparencyRenderNode.Build(lightManager, l8J, baseLight);
					l8_u0002_.Add(volumeLightTransparencyRenderNode);
				}
				if (baseLight.LightingType != LightingType.RealTime)
				{
					l85[j] = null;
				}
				else if (lightTypeCaster.AmbientSource != null)
				{
					num2 += lightTypeCaster.AmbientSource.Depth;
					num++;
					l8Z.DiffuseColor += baseLight.CompositeColorAndIntensity;
					l85[j] = null;
				}
				else if (lightTypeCaster.PointSource != null)
				{
					IPointSource pointSource = lightTypeCaster.PointSource;
					float num3 = Vector3.DistanceSquared(viewToWorld.Translation, pointSource.Position);
					float num4 = SceneState.Environment.VisibleDistance + pointSource.Radius;
					if (num3 > num4 * num4)
					{
						l85[j] = null;
					}
				}
			}
			if (num > 0)
			{
				l8Z.Depth = num2 / (float)num;
			}
			else
			{
				l8Z.Depth = 0.1f;
			}
		}
		l8W.Add(l8Z);
	}

	public abstract void Render();

	public virtual void EndFrameRendering()
	{
		LightingSystemPerformance.Begin("BaseRenderManager.EndFrameRendering");
		GraphicsDevice graphicsDevice = SunBurnCoreSystem.Instance.GraphicsDeviceManager.GraphicsDevice;
		LightingSystemPerformance.Begin("BaseRenderManager.EndFrameRendering (transparency nodes)");
		l8_u0002_.RenderBatches(l8J, CanBatchTransparencies(l8J));
		l8_u0002_.Clear();
		l8v.FreeAllTracked();
		l8Y.FreeAllTracked();
		l8b.EndFrameRendering();
		graphicsDevice.BlendState = BlendState.Opaque;
		graphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
		graphicsDevice.DepthStencilState = DepthStencilState.Default;
	}

	public virtual void Clear()
	{
		l8b.Clear();
		OptimizationSystem.Clear();
	}

	public virtual void Unload()
	{
		Clear();
		l8b.Unload();
	}
}
