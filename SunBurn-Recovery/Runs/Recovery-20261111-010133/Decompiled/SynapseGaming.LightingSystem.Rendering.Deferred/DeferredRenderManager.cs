using System;
using System.Collections.Generic;
using B;
using I;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Editor;
using SynapseGaming.LightingSystem.Effects;
using SynapseGaming.LightingSystem.Effects.Deferred;
using SynapseGaming.LightingSystem.Lights;
using SynapseGaming.LightingSystem.Shadows;
using SynapseGaming.LightingSystem.Shadows.Forward;
using _u0004_;
using b;
using s;
using x;
using y;

namespace SynapseGaming.LightingSystem.Rendering.Deferred;

public class DeferredRenderManager : BaseRenderManager
{
	private const float l8a = 1.2f;

	private Color l8_u0013_ = new Color(0, 0, 0, 0);

	private Matrix l8N = Matrix.Identity;

	private bool l8l;

	private bool l8L = true;

	private float l8w;

	private bool l8J;

	private BasicEffect l8B;

	private I._u0013_ l8s;

	private DeferredObjectEffect l8h;

	private I.a l8_u0002_;

	private Texture2D l8v;

	private Texture2D l8Y;

	private B.N l8_u0005_;

	private OcclusionQueryHelper<ShadowGroup> l8b;

	private Model l8I;

	private List<BaseLight> l85 = new List<BaseLight>(4);

	private bool[] l8W = new bool[6];

	private List<SceneEntity> l8Z = new List<SceneEntity>();

	private new List<RenderableMesh> l8T = new List<RenderableMesh>();

	private List<x.J> l8_u0014_ = new List<x.J>();

	private List<x.J> l8E = new List<x.J>();

	private CompositeLighting l8f = default;

	private Dictionary<object, CompositeLighting> l8_u0016_ = new Dictionary<object, CompositeLighting>(32);

	private Dictionary<object, CompositeLighting> l86 = new Dictionary<object, CompositeLighting>(32);

	private x.h l8x = new x.h();

	private x._u0002_ l8_u0004_ = new x._u0002_();

	private List<RenderableMesh> l8X = new List<RenderableMesh>();

	private List<x.J> l8_u0019_ = new List<x.J>();

	private RenderTargetBinding[] l8y = new RenderTargetBinding[2];

	private static x.w l83 = new x.w();

	private static x.L l8j = new x.L();

	private static x.s l8p = new x.s();

	public bool OcclusionQueryEnabled
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

	public bool DepthFillOptimizationEnabled
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

	public float LightGroupDebugAmount
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

	public DeferredRenderManager(IManagerServiceProvider sceneinterface)
		: base(sceneinterface)
	{
		a();
	}

	private void a()
	{
		SunBurnCoreSystem.MaxLightsPerGroup = I._u0013_.MaxLightSourcesInternal;
	}

	public override void ApplyPreferences(ISystemPreferences preferences)
	{
		base.ApplyPreferences(preferences);
	}

	public override void BeginFrameRendering(ISceneState scenestate)
	{
		LightingSystemPerformance.Begin("RenderManager.BeginFrameRendering");
		GraphicsDevice graphicsDevice = SunBurnCoreSystem.Instance.GraphicsDeviceManager.GraphicsDevice;
		FrameBuffers frameBuffers = scenestate.FrameBuffers;
		if (l8s == null)
		{
			l8s = new I._u0013_(graphicsDevice);
			l8_u0002_ = new I.a(graphicsDevice);
			l8B = new BasicEffect(graphicsDevice);
			l8v = SunBurnCoreSystem.Instance._u0013_S("White");
			l8Y = SunBurnCoreSystem.Instance._u0013_S("Black");
			l8h = new DeferredObjectEffect(graphicsDevice);
			l8_u0005_ = new B.N(graphicsDevice);
			l8b = new OcclusionQueryHelper<ShadowGroup>(graphicsDevice, writingtofloattarget: true);
			l8I = SunBurnCoreSystem.Instance._u0013__u0018_("FullSphere");
		}
		l8x.L_u0012_();
		l8_u0004_.L_u0012_();
		base.BeginFrameRendering(scenestate);
		LightingSystemPerformance.Begin("RenderManager.BeginFrameRendering (find objects)");
		l8Z.Clear();
		l8T.Clear();
		l8_u0016_.Clear();
		l86.Clear();
		IObjectManager objectManager = (IObjectManager)ServiceProvider.GetManager(SceneInterface.ObjectManagerType, required: false);
		ILightManager lightmanager = (ILightManager)ServiceProvider.GetManager(SceneInterface.LightManagerType, required: false);
		objectManager?.Find(l8Z, SceneState.ViewFrustum, ObjectFilter.DynamicAndStatic);
		LightingSystemPerformance.Begin("RenderManager.BeginFrameRendering (filter objects)");
		ExtractAndFilterRenderableMeshes(l8Z, lightmanager, l8J, l8T, l8_u0016_, l86);
		LightingSystemPerformance.Begin("RenderManager.BeginFrameRendering (sort and batch)");
		l8T.Sort(l83);
		l8p.L7(SceneState.View, SceneState.ViewToWorld, SceneState.Projection, SceneState.ProjectionToView, l8_u0014_, l8T, x.N.LightingEffect | x.N.BasicEffect_Lighting | x.N.BasicEffect_NonLighting | x.N.MiscEffect);
		LightingSystemPerformance.Begin("RenderManager.BeginFrameRendering (shadow group)");
		List<ShadowRenderTargetGroup> frameShadowRenderTargetGroups = FrameShadowRenderTargetGroups;
		frameShadowRenderTargetGroups.Clear();
		IShadowMapManager shadowMapManager = (IShadowMapManager)ServiceProvider.GetManager(SceneInterface.ShadowMapManagerType, required: false);
		if (shadowMapManager == null)
		{
			GetDefaultShadows(frameShadowRenderTargetGroups, FrameLights);
		}
		else
		{
			if (shadowMapManager is ShadowMapManager)
			{
				throw new Exception("Cannot use a forward shadow map manager with a deferred render manager. Please switch to a deferred shadow map manager.");
			}
			shadowMapManager.BuildShadows(frameShadowRenderTargetGroups, FrameLights, usedefaultgrouping: true);
		}
		LightingSystemPerformance.Begin("RenderManager.BeginFrameRendering (gbuffers)");
		ILightMapManager param_ = (ILightMapManager)ServiceProvider.GetManager(SceneInterface.LightMapManagerType, required: false);
		Viewport viewport = graphicsDevice.Viewport;
		RenderTargetBinding[] renderTargets = graphicsDevice.GetRenderTargets();
		BlendState blendState = graphicsDevice.BlendState;
		RasterizerState rasterizerState = graphicsDevice.RasterizerState;
		DepthStencilState depthStencilState = graphicsDevice.DepthStencilState;
		if (blendState == null)
		{
			blendState = BlendState.Opaque;
		}
		if (rasterizerState == null)
		{
			rasterizerState = RasterizerState.CullCounterClockwise;
		}
		if (depthStencilState == null)
		{
			depthStencilState = DepthStencilState.Default;
		}
		RenderTarget2D buffer = frameBuffers.GetBuffer(FrameBufferType.DeferredDepthAndSpecularPower, createmissing: true);
		RenderTarget2D buffer2 = frameBuffers.GetBuffer(FrameBufferType.DeferredNormalViewSpaceAndSpecular, createmissing: true);
		RenderTarget2D buffer3 = frameBuffers.GetBuffer(FrameBufferType.DeferredLightingDiffuse, createmissing: true);
		RenderTarget2D buffer4 = frameBuffers.GetBuffer(FrameBufferType.DeferredLightingSpecular, createmissing: true);
		l8x.L_u0012_();
		l8_u0004_.L_u0012_();
		graphicsDevice.BlendState = BlendState.Opaque;
		graphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
		graphicsDevice.DepthStencilState = DepthStencilState.Default;
		ref RenderTargetBinding reference = ref l8y[0];
		reference = new RenderTargetBinding(buffer);
		ref RenderTargetBinding reference2 = ref l8y[1];
		reference2 = new RenderTargetBinding(buffer2);
		graphicsDevice.SetRenderTargets(l8y);
		graphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, l8_u0013_, 1f, 0);
		if (l8L || l8l)
		{
			l8p.LQ(l8E, l8T, param_2: false, l8J);
			graphicsDevice.BlendState = _u0004_.a.l8_u0013_;
			L_u0016_(graphicsDevice, l8E, null, param_, param_4: false, DeferredEffectOutput.Depth, param_6: false, RenderFillMode);
			graphicsDevice.DepthStencilState = DepthStencilState.DepthRead;
			if (l8l)
			{
				x.h.LO(graphicsDevice);
				l8b.Clear();
				foreach (ShadowRenderTargetGroup item in frameShadowRenderTargetGroups)
				{
					foreach (ShadowGroup shadowGroup in item.ShadowGroups)
					{
						l8b.SubmitObject(shadowGroup, shadowGroup.BoundingBox);
					}
				}
				l8b.RunOcclusionQuery(SceneState, 1.2f);
				l8_u0004_.L_u0012_();
			}
			l8x.L_u0012_();
			graphicsDevice.BlendState = BlendState.Opaque;
		}
		L_u0016_(graphicsDevice, l8_u0014_, null, param_, param_4: false, DeferredEffectOutput.GBuffer, param_6: false, RenderFillMode);
		LightingSystemPerformance.Begin("RenderManager.BeginFrameRendering (shadows)");
		if (ShadowDetail != DetailPreference.Off && objectManager != null)
		{
			graphicsDevice.SetRenderTarget(null);
			graphicsDevice.BlendState = BlendState.Opaque;
			graphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
			graphicsDevice.DepthStencilState = DepthStencilState.Default;
			BuildShadowMaps(frameShadowRenderTargetGroups);
		}
		LightingSystemPerformance.Begin("RenderManager.BeginFrameRendering (lighting)");
		graphicsDevice.BlendState = BlendState.Opaque;
		graphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
		graphicsDevice.DepthStencilState = DepthStencilState.DepthRead;
		if (EffectDetail != DetailPreference.Off)
		{
			ref RenderTargetBinding reference3 = ref l8y[0];
			reference3 = new RenderTargetBinding(buffer3);
			ref RenderTargetBinding reference4 = ref l8y[1];
			reference4 = new RenderTargetBinding(buffer4);
			graphicsDevice.SetRenderTargets(l8y);
		}
		else
		{
			graphicsDevice.SetRenderTarget(buffer3);
		}
		graphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, l8_u0013_, 1f, 0);
		Texture2D param_2 = buffer;
		Texture2D param_3 = buffer2;
		LT(graphicsDevice, param_2, param_3);
		graphicsDevice.SetRenderTargets(renderTargets);
		graphicsDevice.Viewport = viewport;
		graphicsDevice.BlendState = blendState;
		graphicsDevice.RasterizerState = rasterizerState;
		graphicsDevice.DepthStencilState = depthStencilState;
		if (ClearBackBufferEnabled)
		{
			graphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, new Color(SceneState.Environment.FogColor), 1f, 0);
		}
	}

	protected override void BuildShadowMaps(List<ShadowRenderTargetGroup> shadowrendertargetgroups)
	{
		GraphicsDevice graphicsDevice = SunBurnCoreSystem.Instance.GraphicsDeviceManager.GraphicsDevice;
		IObjectManager param_ = (IObjectManager)ServiceProvider.GetManager(SceneInterface.ObjectManagerType, required: false);
		IAvatarManager param_2 = (IAvatarManager)ServiceProvider.GetManager(SceneInterface.AvatarManagerType, required: false);
		ILightMapManager param_3 = (ILightMapManager)ServiceProvider.GetManager(SceneInterface.LightMapManagerType, required: false);
		l8x.L_u0012_();
		foreach (ShadowRenderTargetGroup shadowrendertargetgroup in shadowrendertargetgroups)
		{
			if (!shadowrendertargetgroup.HasShadows() || shadowrendertargetgroup.ContentsAreValid)
			{
				continue;
			}
			base.l8T.l8_u0002_.AccumulationValue++;
			shadowrendertargetgroup.Begin();
			foreach (ShadowGroup shadowGroup in shadowrendertargetgroup.ShadowGroups)
			{
				IShadowMap shadow = shadowGroup.Shadow;
				if (shadow != null && (!l8l || l8b.IsObjectVisible(shadowGroup)))
				{
					Lf(graphicsDevice, shadowGroup, shadow, param_, param_2, param_3);
					shadow.ContentsAreValid = true;
				}
			}
			shadowrendertargetgroup.End();
		}
	}

	public override void Render()
	{
		LightingSystemPerformance.Begin("RenderManager.Render");
		GraphicsDevice graphicsDevice = SunBurnCoreSystem.Instance.GraphicsDeviceManager.GraphicsDevice;
		FrameBuffers frameBuffers = SceneState.FrameBuffers;
		RenderTarget2D buffer = frameBuffers.GetBuffer(FrameBufferType.DeferredLightingDiffuse, createmissing: true);
		RenderTarget2D buffer2 = frameBuffers.GetBuffer(FrameBufferType.DeferredLightingSpecular, createmissing: true);
		ILightMapManager param_ = (ILightMapManager)ServiceProvider.GetManager(SceneInterface.LightMapManagerType, required: false);
		graphicsDevice.BlendState = BlendState.Opaque;
		graphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
		graphicsDevice.DepthStencilState = DepthStencilState.Default;
		Texture2D param_2 = buffer;
		Texture2D param_3 = l8Y;
		if (EffectDetail != DetailPreference.Off)
		{
			param_3 = buffer2;
		}
		ISceneState sceneState = SceneState;
		ISceneEnvironment environment = sceneState.Environment;
		foreach (x.J item in l8_u0014_)
		{
			EffectTypeCaster param_4 = OptimizationSystem.EffectTypeCasters.Get(item.l8w);
			LZ(sceneState, environment, param_4, param_2, param_3);
		}
		l8x.L_u0012_();
		l8_u0004_.L_u0012_();
		if (l8L)
		{
			graphicsDevice.BlendState = _u0004_.a.l8_u0013_;
			L_u0016_(graphicsDevice, l8E, null, param_, param_4: false, DeferredEffectOutput.Depth, param_6: false, RenderFillMode);
			graphicsDevice.BlendState = BlendState.Opaque;
			graphicsDevice.DepthStencilState = DepthStencilState.DepthRead;
			l8x.L_u0012_();
		}
		L_u0016_(graphicsDevice, l8_u0014_, null, param_, param_4: false, DeferredEffectOutput.Final, param_6: false, RenderFillMode);
		graphicsDevice.BlendState = BlendState.Opaque;
		graphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
		graphicsDevice.DepthStencilState = DepthStencilState.Default;
	}

	public override void EndFrameRendering()
	{
		LightingSystemPerformance.Begin("RenderManager.EndFrameRendering");
		ISceneState sceneState = SceneState;
		foreach (SceneEntity item in l8Z)
		{
			item.RenderCustomPass(sceneState);
		}
		base.EndFrameRendering();
		l8p._id_3();
	}

	public override void Clear()
	{
		base.Clear();
	}

	public override void Unload()
	{
		base.Unload();
		l8v = null;
		l8Y = null;
		y.a.w_u0005_(ref l8s);
		y.a.w_u0005_(ref l8h);
		y.a.w_u0005_(ref l8_u0002_);
		y.a.w_u0005_(ref l8_u0005_);
		y.a.w_u0005_(ref l8b);
	}

	private void LZ(ISceneState param_0, ISceneEnvironment param_1, EffectTypeCaster param_2, Texture2D param_3, Texture2D param_4)
	{
		if (param_2.DeferredObjectEffect != null)
		{
			IDeferredObjectEffect deferredObjectEffect = param_2.DeferredObjectEffect;
			deferredObjectEffect.SceneLightingDiffuseMap = param_3;
			deferredObjectEffect.SceneLightingSpecularMap = param_4;
			deferredObjectEffect.SetAmbientLighting(FrameAmbientLight, Vector3.Down);
			deferredObjectEffect.FogEnabled = param_1.FogEnabled;
			if (param_1.FogEnabled)
			{
				deferredObjectEffect.FogStartDistance = param_1.FogStartDistance;
				deferredObjectEffect.FogEndDistance = param_1.FogEndDistance;
				deferredObjectEffect.FogColor = param_1.FogColor;
			}
			if (param_2.RenderableEffect != null)
			{
				param_2.RenderableEffect.SetViewAndProjection(param_0.View, param_0.ViewToWorld, param_0.Projection, param_0.ProjectionToView);
			}
		}
	}

	protected override bool CanBatchTransparencies(ISceneState scenestate)
	{
		return true;
	}

	internal override void r4_u0015__u0011_1(List<RenderableMesh> param_0, bool param_1)
	{
		if (param_0.Count > 0)
		{
			ISceneState sceneState = SceneState;
			ISceneEnvironment environment = sceneState.Environment;
			ILightMapManager param_2 = (ILightMapManager)ServiceProvider.GetManager(SceneInterface.LightMapManagerType, required: false);
			GraphicsDevice graphicsDevice = SunBurnCoreSystem.Instance.GraphicsDeviceManager.GraphicsDevice;
			if (param_1)
			{
				l8x.L_u0012_();
				l8_u0004_.L_u0012_();
			}
			EffectTypeCaster effectTypeCaster = OptimizationSystem.EffectTypeCasters.Get(param_0[0].Effect);
			LZ(sceneState, environment, effectTypeCaster, l8Y, l8Y);
			graphicsDevice.BlendState = BlendState.AlphaBlend;
			graphicsDevice.DepthStencilState = DepthStencilState.DepthRead;
			w8a param_3 = ((effectTypeCaster.RenderableEffect == null || !effectTypeCaster.RenderableEffect.DoubleSided) ? w8a.TransparentSingleSided : w8a.TransparentDoubleSided);
			L6(graphicsDevice, param_0, effectTypeCaster, param_2, param_4: false, DeferredEffectOutput.Final, param_6: true, param_7: false, param_3, RenderFillMode);
		}
	}

	private void LT(GraphicsDevice param_0, Texture2D param_1, Texture2D param_2)
	{
		LightingSystemPerformance.Begin("RenderManager.RenderLighting");
		List<ShadowRenderTargetGroup> frameShadowRenderTargetGroups = FrameShadowRenderTargetGroups;
		LightingSystemPerformance.Begin("RenderManager.RenderLighting (depth buffer replace)");
		param_0.BlendState = BlendState.Opaque;
		param_0.RasterizerState = RasterizerState.CullNone;
		param_0.DepthStencilState = DepthStencilState.Default;
		l8x.L_u000F_(param_0);
		l8x.L_u0012_();
		l8_u0002_.SceneDepthMap = param_1;
		l8_u0002_.SetViewAndProjection(SceneState.View, SceneState.ViewToWorld, SceneState.Projection, SceneState.ProjectionToView);
		SceneState.FrameBuffers.FullFrameQuad.Render(l8_u0002_);
		param_0.BlendState = _u0004_.a.l8a;
		param_0.RasterizerState = RasterizerState.CullClockwise;
		param_0.DepthStencilState = DepthStencilState.DepthRead;
		l8s.SceneDepthMap = param_1;
		l8s.SceneNormalSpecularMap = param_2;
		l8s.EffectDetail = EffectDetail;
		l8s.ShadowDetail = ShadowDetail;
		l8s.LightGroupDebugAmount = l8w;
		l8s.SetViewAndProjection(SceneState.View, SceneState.ViewToWorld, SceneState.Projection, SceneState.ProjectionToView);
		l8s.CurrentTechnique.Passes[0].Apply();
		foreach (ShadowRenderTargetGroup item in frameShadowRenderTargetGroups)
		{
			foreach (ShadowGroup shadowGroup in item.ShadowGroups)
			{
				if (shadowGroup.Lights.Count < 1 || (l8l && !l8b.IsObjectVisible(shadowGroup)))
				{
					continue;
				}
				IShadowMap shadow = shadowGroup.Shadow;
				ShadowSourceTypeCaster shadowSourceTypeCaster = shadowGroup.ShadowSourceTypes;
				bool flag = shadowSourceTypeCaster.PointSource != null;
				bool flag2 = !flag && shadowSourceTypeCaster.DirectionalSource != null;
				if (shadow != null)
				{
					shadow.BeginRendering(item.RenderTarget, l8s);
				}
				else if (flag)
				{
					l8s.SetShadowMapAndType(null, b.B.Point);
				}
				else if (flag2)
				{
					l8s.SetShadowMapAndType(null, b.B.Directional);
				}
				if (flag)
				{
					if (shadowGroup.ShadowSource.ShadowRenderLightsTogether)
					{
						L_u0014_(param_0, shadowGroup, param_2: false);
					}
					else
					{
						LE(param_0, shadowGroup);
					}
				}
				else if (flag2)
				{
					L_u0014_(param_0, shadowGroup, param_2: true);
				}
				shadow?.EndRendering();
				base.l8T.l8h.AccumulationValue++;
			}
		}
		param_0.DepthStencilState = DepthStencilState.None;
	}

	private void L_u0014_(GraphicsDevice param_0, ShadowGroup param_1, bool param_2)
	{
		LightingSystemPerformance.Begin("RenderManager.RenderLightingShadowGroupAsGroup");
		FullFrameQuad fullFrameQuad = SceneState.FrameBuffers.FullFrameQuad;
		bool invertedWindings = SceneState.InvertedWindings;
		EffectPass effectPass = l8s.CurrentTechnique.Passes[0];
		if (!param_2)
		{
			BoundingSphere worldClippingSphere = BoundingSphere.CreateFromBoundingBox(param_1.BoundingBox);
			Matrix world = l8_u0005_._u0018_(param_1.BoundingBox);
			l8s.WorldClippingSphere = worldClippingSphere;
			l8s.SetWorldAndWorldToObject(ref world, ref l8N);
			if (CoreHelper.PaddedBoundingBox(param_1.BoundingBox, 1.2f).Intersects(SceneState.ViewFrustum.Near) == PlaneIntersectionType.Intersecting)
			{
				if (invertedWindings)
				{
					param_0.RasterizerState = RasterizerState.CullCounterClockwise;
				}
				else
				{
					param_0.RasterizerState = RasterizerState.CullClockwise;
				}
				param_0.DepthStencilState = _u0004_.a.l8J;
			}
			else
			{
				if (invertedWindings)
				{
					param_0.RasterizerState = RasterizerState.CullClockwise;
				}
				else
				{
					param_0.RasterizerState = RasterizerState.CullCounterClockwise;
				}
				param_0.DepthStencilState = _u0004_.a.l8B;
			}
		}
		else
		{
			l8s.SetWorldAndWorldToObject(ref l8N, ref l8N);
			param_0.RasterizerState = RasterizerState.CullNone;
			param_0.DepthStencilState = _u0004_.a.l8s;
		}
		int num = 0;
		while (num < param_1.Lights.Count)
		{
			int num2 = Math.Min(l8s.MaxLightSources, param_1.Lights.Count - num);
			int num3 = num + num2;
			l85.Clear();
			for (int i = num; i < num3; i++)
			{
				l85.Add(param_1.Lights[i]);
				base.l8T.l8B.AccumulationValue++;
			}
			num = num3;
			l8s.LightSources = l85;
			effectPass.Apply();
			if (!param_2)
			{
				l8_u0005_.S();
			}
			else
			{
				fullFrameQuad.Render();
			}
			base.l8T.l8s.AccumulationValue++;
		}
	}

	private void LE(GraphicsDevice param_0, ShadowGroup param_1)
	{
		LightingSystemPerformance.Begin("RenderManager.RenderLightingShadowGroupAsLights");
		bool invertedWindings = SceneState.InvertedWindings;
		Plane near = SceneState.ViewFrustum.Near;
		EffectPass effectPass = l8s.CurrentTechnique.Passes[0];
		ModelMesh modelMesh = l8I.Meshes[0];
		ModelMeshPart modelMeshPart = modelMesh.MeshParts[0];
		bool flag = false;
		param_0.Indices = modelMeshPart.IndexBuffer;
		param_0.SetVertexBuffer(modelMeshPart.VertexBuffer, modelMeshPart.VertexOffset);
		List<BaseLight> lights = param_1.Lights;
		int count = lights.Count;
		for (int i = 0; i < count; i++)
		{
			BaseLight baseLight = lights[i];
			LightTypeCaster lightTypeCaster = OptimizationSystem.LightTypeCasters.Get(baseLight);
			l85.Clear();
			l85.Add(baseLight);
			l8s.LightSources = l85;
			if (lightTypeCaster.SpotSource != null)
			{
				BoundingBox worldBoundingBox = baseLight.WorldBoundingBox;
				BoundingSphere worldClippingSphere = BoundingSphere.CreateFromBoundingBox(worldBoundingBox);
				if (CoreHelper.PaddedBoundingBox(worldBoundingBox, 1.2f).Intersects(near) == PlaneIntersectionType.Intersecting)
				{
					if (invertedWindings)
					{
						param_0.RasterizerState = RasterizerState.CullCounterClockwise;
					}
					else
					{
						param_0.RasterizerState = RasterizerState.CullClockwise;
					}
					param_0.DepthStencilState = _u0004_.a.l8J;
				}
				else
				{
					if (invertedWindings)
					{
						param_0.RasterizerState = RasterizerState.CullClockwise;
					}
					else
					{
						param_0.RasterizerState = RasterizerState.CullCounterClockwise;
					}
					param_0.DepthStencilState = _u0004_.a.l8B;
				}
				Matrix world = l8_u0005_._u0018_(worldBoundingBox);
				Matrix worldtoobj = Matrix.Identity;
				l8s.WorldClippingSphere = worldClippingSphere;
				l8s.SetWorldAndWorldToObject(ref world, ref worldtoobj);
				effectPass.Apply();
				l8_u0005_.S();
				flag = true;
			}
			else
			{
				BoundingSphere worldBoundingSphere = baseLight.WorldBoundingSphere;
				BoundingSphere sphere = CoreHelper.PaddedBoundingSphere(worldBoundingSphere, 1.2f);
				if (near.Intersects(sphere) == PlaneIntersectionType.Intersecting)
				{
					if (invertedWindings)
					{
						param_0.RasterizerState = RasterizerState.CullCounterClockwise;
					}
					else
					{
						param_0.RasterizerState = RasterizerState.CullClockwise;
					}
					param_0.DepthStencilState = _u0004_.a.l8J;
				}
				else
				{
					if (invertedWindings)
					{
						param_0.RasterizerState = RasterizerState.CullClockwise;
					}
					else
					{
						param_0.RasterizerState = RasterizerState.CullCounterClockwise;
					}
					param_0.DepthStencilState = _u0004_.a.l8B;
				}
				Matrix world2 = Matrix.CreateScale(worldBoundingSphere.Radius) * Matrix.CreateTranslation(worldBoundingSphere.Center);
				Matrix worldtoobj2 = Matrix.Identity;
				l8s.WorldClippingSphere = worldBoundingSphere;
				l8s.SetWorldAndWorldToObject(ref world2, ref worldtoobj2);
				effectPass.Apply();
				if (flag)
				{
					param_0.Indices = modelMeshPart.IndexBuffer;
					param_0.SetVertexBuffer(modelMeshPart.VertexBuffer, modelMeshPart.VertexOffset);
					flag = false;
				}
				param_0.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, modelMeshPart.NumVertices, modelMeshPart.StartIndex, modelMeshPart.PrimitiveCount);
			}
			base.l8T.l8B.AccumulationValue++;
			base.l8T.l8s.AccumulationValue++;
		}
	}

	private void Lf(GraphicsDevice param_0, ShadowGroup param_1, IShadowMap param_2, IObjectManager param_3, IAvatarManager param_4, ILightMapManager param_5)
	{
		LightingSystemPerformance.Begin("RenderManager.RenderShadowMaps");
		base.l8T.l8v.AccumulationValue++;
		LightingSystemPerformance.Begin("RenderManager.BuildShadowMaps (find and batch)");
		l8X.Clear();
		param_3.Find(objectfilter: (param_1.ShadowSource.ShadowType != ShadowType.AllObjects) ? ObjectFilter.Static : ObjectFilter.DynamicAndStatic, foundobjects: l8X, worldbounds: param_1.BoundingBox);
		l8X.Sort(l8j);
		l8p.LQ(l8_u0019_, l8X, param_2: false, l8J);
		param_4?.BeginShadowGroupRendering(param_1);
		_ = param_1.ShadowSource.ShadowPosition;
		l8h.EffectDetail = EffectDetail;
		l8h.ShadowArea = param_1.BoundingSphereCentered;
		for (int i = 0; i < param_2.Surfaces.Length; i++)
		{
			ShadowMapSurface shadowMapSurface = param_2.Surfaces[i];
			base.l8T.l8Y.AccumulationValue++;
			if (!param_2.IsSurfaceVisible(i, SceneState.ViewFrustum))
			{
				continue;
			}
			param_2.BeginSurfaceRendering(i, l8h);
			LightingSystemPerformance.Begin("RenderManager.BuildShadowMaps (object filter loop)");
			x.B.L2(l8X, shadowMapSurface.Frustum);
			foreach (x.J item in l8_u0019_)
			{
				if (!item.l8a)
				{
					continue;
				}
				EffectTypeCaster effectTypeCaster = null;
				if (!item.l8L)
				{
					EffectTypeCaster param_6 = OptimizationSystem.EffectTypeCasters.Get(item.l8w);
					EffectTypeCaster effectTypeCaster2 = OptimizationSystem.EffectTypeCasters.Get(l8h);
					EffectHelper.lv(param_6, effectTypeCaster2);
					l8h.Skinned = item.l8_u0013_;
					effectTypeCaster = effectTypeCaster2;
				}
				else
				{
					EffectTypeCaster effectTypeCaster3 = OptimizationSystem.EffectTypeCasters.Get(item.l8w);
					IRenderableEffect renderableEffect = effectTypeCaster3.RenderableEffect;
					IShadowGenerateEffect shadowGenerateEffect = effectTypeCaster3.ShadowGenerateEffect;
					if (renderableEffect == null || shadowGenerateEffect == null)
					{
						continue;
					}
					renderableEffect.View = shadowMapSurface.WorldToSurfaceView;
					renderableEffect.Projection = shadowMapSurface.Projection;
					renderableEffect.EffectDetail = EffectDetail;
					shadowGenerateEffect.ShadowPrimaryBias = param_1.ShadowSource.ShadowPrimaryBias;
					shadowGenerateEffect.ShadowSecondaryBias = param_1.ShadowSource.ShadowSecondaryBias;
					shadowGenerateEffect.ShadowArea = param_1.BoundingSphereCentered;
					shadowGenerateEffect.SetCameraView(SceneState.View, SceneState.ViewToWorld);
					if (effectTypeCaster3.BaseSasEffect != null)
					{
						effectTypeCaster3.BaseSasEffect.GameTime = SceneState.GameTime;
					}
					effectTypeCaster = effectTypeCaster3;
				}
				L6(param_0, item.Objects, effectTypeCaster, param_5, param_4: true, DeferredEffectOutput.ShadowDepth, param_6: true, param_7: true, w8a.Solid, FillMode.Solid);
			}
			if (param_4 != null && param_4.RenderToShadowMapSurface(param_1, shadowMapSurface, l8h))
			{
				l8_u0004_.L_u0012_();
				l8x.L_u0012_();
			}
			param_2.EndSurfaceRendering();
			base.l8T.l8_u0005_.AccumulationValue++;
		}
		param_4?.EndShadowGroupRendering(param_1);
	}

	private void L_u0016_(GraphicsDevice param_0, List<x.J> param_1, ShadowGroup param_2, ILightMapManager param_3, bool param_4, DeferredEffectOutput param_5, bool param_6, FillMode param_7)
	{
		foreach (x.J item in param_1)
		{
			if (!param_4 || item.l8a)
			{
				EffectTypeCaster effectTypeCaster = OptimizationSystem.EffectTypeCasters.Get(item.l8w);
				if (effectTypeCaster.RenderableEffect != null)
				{
					effectTypeCaster.RenderableEffect.EffectDetail = EffectDetail;
				}
				if (param_2 != null && effectTypeCaster.ShadowGenerateEffect != null)
				{
					IShadowGenerateEffect shadowGenerateEffect = effectTypeCaster.ShadowGenerateEffect;
					shadowGenerateEffect.ShadowPrimaryBias = param_2.ShadowSource.ShadowPrimaryBias;
					shadowGenerateEffect.ShadowSecondaryBias = param_2.ShadowSource.ShadowSecondaryBias;
					shadowGenerateEffect.ShadowArea = param_2.BoundingSphereCentered;
				}
				L6(param_6: param_5 != DeferredEffectOutput.ShadowDepth || effectTypeCaster.TransparentEffect == null || effectTypeCaster.TransparentEffect.TransparencyMode != TransparencyMode.None, param_0: param_0, param_1: item.Objects, param_2: effectTypeCaster, param_3: param_3, param_4: param_4, param_5: param_5, param_7: param_6, param_8: w8a.Solid, param_9: param_7);
			}
		}
	}

	private void L6(GraphicsDevice param_0, List<RenderableMesh> param_1, EffectTypeCaster param_2, ILightMapManager param_3, bool param_4, DeferredEffectOutput param_5, bool param_6, bool param_7, w8a param_8, FillMode param_9)
	{
		if (param_1.Count < 1)
		{
			return;
		}
		LightingSystemPerformance.Begin("RenderManager.RenderObjectBatch");
		if (param_2.DeferredObjectEffect != null)
		{
			param_2.DeferredObjectEffect.DeferredEffectOutput = param_5;
			if (param_4)
			{
				bool flag = false;
				foreach (RenderableMesh item in param_1)
				{
					if (item.l85 && item.l8_u0013_.Valid)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return;
				}
			}
			bool flag2 = false;
			CullMode cullMode = CullMode.CullCounterClockwiseFace;
			if (SceneState.InvertedWindings)
			{
				param_7 = !param_7;
			}
			bool flag3 = param_8 == w8a.TransparentDoubleSided;
			if (flag3)
			{
				_u0004_.a.Lx(param_0, param_9, cullMode, !param_7, param_4: false, param_5: false);
			}
			else
			{
				if (param_2.RenderableEffect != null && param_8 == w8a.Solid)
				{
					flag2 = param_2.RenderableEffect.DoubleSided;
				}
				_u0004_.a.Lx(param_0, param_9, cullMode, param_7, flag2, param_5: false);
			}
			SamplerState samplerState = null;
			if (param_6)
			{
				if (param_2.AddressableEffect != null)
				{
					IAddressableEffect addressableEffect = param_2.AddressableEffect;
					samplerState = l8x.Lq(param_0, addressableEffect.AddressModeU, addressableEffect.AddressModeV, addressableEffect.AddressModeW, Filter, MaxAnisotropy);
				}
				else
				{
					samplerState = l8x.Lq(param_0, TextureAddressMode.Wrap, TextureAddressMode.Wrap, TextureAddressMode.Wrap, Filter, MaxAnisotropy);
				}
			}
			else
			{
				l8x.L_u000F_(param_0);
			}
			EffectPassCollection passes = param_2.Effect.CurrentTechnique.Passes;
			base.l8T.l8l.AccumulationValue++;
			base.l8T.l8L.AccumulationValue += passes.Count;
			ISkinnedEffect skinnedEffect = param_2.SkinnedEffect;
			IRenderableEffect renderableEffect = param_2.RenderableEffect;
			IStaticLightingEffect staticLightingEffect = param_2.StaticLightingEffect;
			BaseRenderableEffect baseRenderableEffect = param_2.BaseRenderableEffect;
			_ = param_2.BaseMaterialEffect;
			if (param_2.BaseSasEffect != null || param_2.TerrainEffect != null)
			{
				l8x.L_u0012_();
			}
			bool flag4 = true;
			bool flag5 = param_5 == DeferredEffectOutput.Final && staticLightingEffect != null && param_3 != null;
			Dictionary<object, CompositeLighting> dictionary = ((param_8 == w8a.Solid) ? l8_u0016_ : l86);
			for (int i = 0; i < passes.Count; i++)
			{
				EffectPass effectPass = passes[i];
				effectPass.Apply();
				foreach (RenderableMesh item2 in param_1)
				{
					if (item2 == null || (param_4 && !item2.l85) || !item2.l8_u0013_.Valid)
					{
						continue;
					}
					bool flag6 = false;
					if (flag5)
					{
						StaticLightingType staticLightingType = item2.l8_u0013_.StaticLightingType;
						if (staticLightingType == StaticLightingType.Composite || staticLightingType == StaticLightingType.Custom || param_8 != w8a.Solid)
						{
							if (!dictionary.TryGetValue(item2.l8_u0013_, out var value))
							{
								value = l8f;
							}
							if (staticLightingType == StaticLightingType.BakedDown)
							{
								staticLightingEffect.SetStaticLighting(StaticLightingEffectMode.BakedDownAndComposite, param_3.GetLightMap(item2), ref value);
							}
							else
							{
								staticLightingEffect.SetStaticLighting(StaticLightingEffectMode.Composite, null, ref value);
							}
						}
						else if (staticLightingType == StaticLightingType.BakedDown)
						{
							staticLightingEffect.SetStaticLighting(StaticLightingEffectMode.BakedDown, param_3.GetLightMap(item2));
						}
						else
						{
							staticLightingEffect.SetStaticLighting(StaticLightingEffectMode.Ambient, null);
						}
						flag6 = true;
					}
					if (skinnedEffect != null)
					{
						skinnedEffect.SkinBones = item2.l8_u0013_.SkinBones;
						flag6 = true;
					}
					if (renderableEffect != null)
					{
						renderableEffect.SetWorldAndWorldToObject(ref item2.l8v, ref item2.l8Y);
						flag6 = true;
					}
					if (baseRenderableEffect != null)
					{
						flag6 = baseRenderableEffect.UpdatedByBatch;
						baseRenderableEffect.UpdatedByBatch = false;
					}
					if (flag6)
					{
						effectPass.Apply();
						base.l8T.l8J.AccumulationValue++;
					}
					if (!flag2 && cullMode != item2.l8I)
					{
						cullMode = item2.l8I;
						if (flag3)
						{
							_u0004_.a.Lx(param_0, param_9, cullMode, !param_7, param_4: false, param_5: false);
						}
						else
						{
							_u0004_.a.Lx(param_0, param_9, cullMode, param_7, flag2, param_5: false);
						}
						base.l8T.l8w.AccumulationValue++;
					}
					l8_u0004_._u0004_(param_0, item2);
					try
					{
						if (flag4 && samplerState != null)
						{
							l8x.LF(param_0, samplerState, _AvoidXNAGarbageCollectionMemoryLeak);
						}
						if (item2.Index.BufferIndex.l8a == null)
						{
							param_0.DrawPrimitives(item2.l8b, item2.Index.l8_u0013_, item2.Index.l8a);
						}
						else
						{
							param_0.DrawIndexedPrimitives(item2.l8b, item2.Index.l8N, 0, item2.l8_u0005_, item2.Index.l8_u0013_, item2.Index.l8a);
						}
						if (flag3)
						{
							_u0004_.a.Lx(param_0, param_9, cullMode, param_7, param_4: false, param_5: false);
							cullMode = CullMode.None;
							if (item2.Index.BufferIndex.l8a == null)
							{
								param_0.DrawPrimitives(item2.l8b, item2.Index.l8_u0013_, item2.Index.l8a);
							}
							else
							{
								param_0.DrawIndexedPrimitives(item2.l8b, item2.Index.l8N, 0, item2.l8_u0005_, item2.Index.l8_u0013_, item2.Index.l8a);
							}
						}
					}
					catch (Exception ex)
					{
						item2.l8_u0013_.Valid = false;
						item2.l8_u0013_.RenderingErrors = ex.Message;
						SunBurnEditor.NZ(item2.l8_u0013_, s.N.Error);
						OnXNARuntimeException(ex, $"Unable to render object '{item2.l8_u0013_.Name}'.");
					}
					flag4 = false;
					base.l8T.l8N.AccumulationValue++;
					base.l8T.l8a.AccumulationValue += item2.Index.l8a;
				}
			}
			if (param_2.BaseSasEffect != null || param_2.TerrainEffect != null)
			{
				l8x.L_u0012_();
			}
			return;
		}
		string text = string.Empty;
		if (param_1[0].l8_u0013_ != null)
		{
			text = param_1[0].l8_u0013_.Name;
		}
		throw new Exception("Deferred rendering does not support non-deferred effects (SceneObject '" + text + "'). Make sure effects derive from IDeferredObjectEffect or model processors are set to a deferred processor.");
	}
}
