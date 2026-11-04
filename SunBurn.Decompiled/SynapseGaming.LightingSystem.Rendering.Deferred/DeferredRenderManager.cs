using System;
using System.Collections.Generic;
using _0013;
using L;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using R;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Editor;
using SynapseGaming.LightingSystem.Effects;
using SynapseGaming.LightingSystem.Effects.Deferred;
using SynapseGaming.LightingSystem.Lights;
using SynapseGaming.LightingSystem.Shadows;
using SynapseGaming.LightingSystem.Shadows.Forward;
using h;
using j;
using o;
using v;

namespace SynapseGaming.LightingSystem.Rendering.Deferred;

/// <summary>
/// Provides a complete deferred renderer.
/// </summary>
public class DeferredRenderManager : BaseRenderManager
{
	private const float fNB = 1.2f;

	private Color fNa = new Color(0, 0, 0, 0);

	private Matrix fNg = Matrix.Identity;

	private bool fNf;

	private bool fN_0003 = true;

	private float fNn;

	private bool fN_0019;

	private BasicEffect fNL;

	private o.a fNv;

	private DeferredObjectEffect fN_0012;

	private o.B fN_0015;

	private Texture2D fN8;

	private L.g fNi;

	private OcclusionQueryHelper<ShadowGroup> fN_0005;

	private Model fNj;

	private List<BaseLight> fNo = new List<BaseLight>(4);

	private bool[] fNc = new bool[6];

	private new List<SceneEntity> fNd = new List<SceneEntity>();

	private List<RenderableMesh> fNw = new List<RenderableMesh>();

	private List<h._0019> fNs = new List<h._0019>();

	private List<h._0019> fN_0011 = new List<h._0019>();

	private CompositeLighting fNQ = default;

	private Dictionary<object, CompositeLighting> fNG = new Dictionary<object, CompositeLighting>(32);

	private h._0012 fN_0017 = new h._0012();

	private h._0015 fNm = new h._0015();

	private List<RenderableMesh> fNh = new List<RenderableMesh>();

	private List<h._0019> fNR = new List<h._0019>();

	private RenderTargetBinding[] fNW = new RenderTargetBinding[2];

	private static h._0003 fN9 = new h._0003();

	private static h.v fN_0013 = new h.v();

	/// <summary>
	/// Enables occlusion querying to reduce light rendering. Helps performance with complex indoor
	/// scenes using lots of lights. May hurt performance in scenes where most tested lights are visible.
	/// </summary>
	public bool OcclusionQueryEnabled
	{
		get
		{
			return fNf;
		}
		set
		{
			fNf = value;
		}
	}

	/// <summary>
	/// Enables z-fill optimization to reduce bandwidth and fill rate consumption in complex
	/// scenes.  May hurt performance in high polygon scenes.
	/// </summary>
	public bool DepthFillOptimizationEnabled
	{
		get
		{
			return fN_0003;
		}
		set
		{
			fN_0003 = value;
		}
	}

	/// <summary>
	/// Shows light rendering debug information by rendering a constant red color where shadow groups are rendered.
	/// </summary>
	public float LightGroupDebugAmount
	{
		get
		{
			return fNn;
		}
		set
		{
			fNn = value;
		}
	}

	/// <summary>
	/// Creates a new DeferredRenderManager instance.  Automatically creates an internal LightManager and StencilShadowManager.
	/// </summary>
	/// <param name="graphicsdevicemanager"></param>
	/// <param name="sceneinterface">Service provider used to access all other manager services in this scene.</param>
	public DeferredRenderManager(IGraphicsDeviceService graphicsdevicemanager, IManagerServiceProvider sceneinterface)
		: base(graphicsdevicemanager, sceneinterface)
	{
		B();
	}

	private void B()
	{
		BaseLightManager.MaxLightsPerGroup = o.a.MaxLightSourcesInternal;
	}

	/// <summary>
	/// Use to apply user quality and performance preferences to the resources managed by this object.
	/// </summary>
	/// <param name="preferences"></param>
	public override void ApplyPreferences(ILightingSystemPreferences preferences)
	{
		base.ApplyPreferences(preferences);
	}

	/// <summary>
	/// Builds all object batches, shadow maps, and cached information before rendering.
	/// Any object added to the RenderManager after this call will not be visible during the frame.
	/// </summary>
	/// <param name="scenestate"></param>
	public override void BeginFrameRendering(ISceneState scenestate)
	{
		LightingSystemPerformance.Begin("RenderManager.BeginFrameRendering");
		GraphicsDevice graphicsDevice = GraphicsDeviceManager.GraphicsDevice;
		FrameBuffers frameBuffers = scenestate.FrameBuffers;
		if (fNv == null)
		{
			fNv = new o.a(graphicsDevice);
			fN_0015 = new o.B(graphicsDevice);
			fNL = new BasicEffect(graphicsDevice);
			fN8 = LightingSystemManager.Instance.aP("White");
			fN_0012 = new DeferredObjectEffect(graphicsDevice);
			fNi = new L.g(graphicsDevice);
			fN_0005 = new OcclusionQueryHelper<ShadowGroup>(graphicsDevice, writingtofloattarget: true);
			fNj = LightingSystemManager.Instance.aU("FullSphere");
		}
		fN_0017._00034();
		fNm._00034();
		base.BeginFrameRendering(scenestate);
		LightingSystemPerformance.Begin("RenderManager.BeginFrameRendering (find objects)");
		fNd.Clear();
		fNw.Clear();
		fNG.Clear();
		IObjectManager objectManager = (IObjectManager)ServiceProvider.GetManager(SceneInterface.ObjectManagerType, required: false);
		ILightManager lightManager = (ILightManager)ServiceProvider.GetManager(SceneInterface.LightManagerType, required: false);
		objectManager?.Find(fNd, SceneState.ViewFrustum, ObjectFilter.DynamicAndStatic);
		LightingSystemPerformance.Begin("RenderManager.BeginFrameRendering (filter objects)");
		bool flag = lightManager != null;
		for (int k = 0; k < fNd.Count; k++)
		{
			SceneEntity sceneEntity = fNd[k];
			if (sceneEntity == null)
			{
				continue;
			}
			L.w w2 = OptimizationSystem.fNf.g_0011(sceneEntity);
			ISceneObject sceneObject = w2.fNB;
			if (sceneObject == null || (!sceneObject.Visible && (!sceneObject.VisibleInEditor || !fN_0019)))
			{
				continue;
			}
			float num = Vector3.DistanceSquared(SceneState.ViewToWorld.Translation, sceneObject.WorldBoundingSphere.Center);
			float num2 = SceneState.Environment.VisibleDistance + sceneObject.WorldBoundingSphere.Radius;
			if (num > num2 * num2)
			{
				continue;
			}
			base.fNd.fNa.AccumulationValue++;
			if (flag && sceneObject.StaticLightingType == StaticLightingType.Composite && !fNG.ContainsKey(sceneEntity))
			{
				fNG.Add(sceneEntity, lightManager.GetCompositeLighting(sceneObject.WorldBoundingBox, 0.25f, LightingType.BakedDown));
			}
			for (int l = 0; l < sceneObject.RenderableMeshes.Count; l++)
			{
				RenderableMesh renderableMesh = sceneObject.RenderableMeshes[l];
				if (renderableMesh.fN_0015 != null)
				{
					if (renderableMesh.fN_0015 is BaseSasBindEffect baseSasBindEffect)
					{
						baseSasBindEffect.GameTime = scenestate.GameTime;
					}
					fNw.Add(renderableMesh);
				}
			}
		}
		LightingSystemPerformance.Begin("RenderManager.BeginFrameRendering (sort and batch)");
		fNw.Sort(fN9);
		fN_0013._0003E(SceneState.View, SceneState.ViewToWorld, SceneState.Projection, SceneState.ProjectionToView, fNs, fNw, h.g.LightingEffect | h.g.BasicEffect_Lighting | h.g.BasicEffect_NonLighting | h.g.MiscEffect);
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
		ILightMapManager lightMapManager = (ILightMapManager)ServiceProvider.GetManager(SceneInterface.LightMapManagerType, required: false);
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
		fN_0017._00034();
		fNm._00034();
		graphicsDevice.BlendState = BlendState.Opaque;
		graphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
		graphicsDevice.DepthStencilState = DepthStencilState.Default;
		ref RenderTargetBinding reference = ref fNW[0];
		reference = new RenderTargetBinding(buffer);
		ref RenderTargetBinding reference2 = ref fNW[1];
		reference2 = new RenderTargetBinding(buffer2);
		graphicsDevice.SetRenderTargets(fNW);
		graphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, fNa, 1f, 0);
		if (fN_0003 || fNf)
		{
			fN_0013._0003Y(fN_0011, fNw, false, fN_0019);
			graphicsDevice.BlendState = R.B.fNa;
			_0003_0004(fN_0011, null, lightMapManager, false, DeferredEffectOutput.Depth, false, RenderFillMode);
			graphicsDevice.DepthStencilState = DepthStencilState.DepthRead;
			if (fNf)
			{
				h._0012._0003T(graphicsDevice);
				fN_0005.Clear();
				foreach (ShadowRenderTargetGroup item in frameShadowRenderTargetGroups)
				{
					foreach (ShadowGroup shadowGroup in item.ShadowGroups)
					{
						fN_0005.SubmitObject(shadowGroup, shadowGroup.BoundingBox);
					}
				}
				fN_0005.RunOcclusionQuery(SceneState, 1.2f);
				fNm._00034();
			}
			fN_0017._00034();
			graphicsDevice.BlendState = BlendState.Opaque;
		}
		_0003_0004(fNs, null, lightMapManager, false, DeferredEffectOutput.GBuffer, false, RenderFillMode);
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
			ref RenderTargetBinding reference3 = ref fNW[0];
			reference3 = new RenderTargetBinding(buffer3);
			ref RenderTargetBinding reference4 = ref fNW[1];
			reference4 = new RenderTargetBinding(buffer4);
			graphicsDevice.SetRenderTargets(fNW);
		}
		else
		{
			graphicsDevice.SetRenderTarget(buffer3);
		}
		graphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, fNa, 1f, 0);
		Texture2D texture2D = buffer;
		Texture2D texture2D2 = buffer2;
		_0003K(texture2D, texture2D2);
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

	/// <summary>
	/// Generates shadow maps for the provided shadow render groups. Override this
	/// method to customize shadow map generation.
	/// </summary>
	/// <param name="shadowrendertargetgroups">Shadow render groups to generate shadow maps for.</param>
	protected override void BuildShadowMaps(List<ShadowRenderTargetGroup> shadowrendertargetgroups)
	{
		IObjectManager objectManager = (IObjectManager)ServiceProvider.GetManager(SceneInterface.ObjectManagerType, required: false);
		IAvatarManager avatarManager = (IAvatarManager)ServiceProvider.GetManager(SceneInterface.AvatarManagerType, required: false);
		ILightMapManager lightMapManager = (ILightMapManager)ServiceProvider.GetManager(SceneInterface.LightMapManagerType, required: false);
		fN_0017._00034();
		foreach (ShadowRenderTargetGroup shadowrendertargetgroup in shadowrendertargetgroups)
		{
			if (!shadowrendertargetgroup.HasShadows() || shadowrendertargetgroup.ContentsAreValid)
			{
				continue;
			}
			base.fNd.fN_0015.AccumulationValue++;
			shadowrendertargetgroup.Begin();
			foreach (ShadowGroup shadowGroup in shadowrendertargetgroup.ShadowGroups)
			{
				if (shadowGroup.Shadow is IShadowMap shadowMap && (!fNf || fN_0005.IsObjectVisible(shadowGroup)))
				{
					_0003J(shadowGroup, shadowMap, objectManager, avatarManager, lightMapManager);
					shadowMap.ContentsAreValid = true;
				}
			}
			shadowrendertargetgroup.End();
		}
	}

	/// <summary>
	/// Renders the scene.
	/// </summary>
	public override void Render()
	{
		LightingSystemPerformance.Begin("RenderManager.Render");
		GraphicsDevice graphicsDevice = GraphicsDeviceManager.GraphicsDevice;
		FrameBuffers frameBuffers = SceneState.FrameBuffers;
		RenderTarget2D buffer = frameBuffers.GetBuffer(FrameBufferType.DeferredLightingDiffuse, createmissing: true);
		RenderTarget2D buffer2 = frameBuffers.GetBuffer(FrameBufferType.DeferredLightingSpecular, createmissing: true);
		ILightMapManager lightMapManager = (ILightMapManager)ServiceProvider.GetManager(SceneInterface.LightMapManagerType, required: false);
		graphicsDevice.BlendState = BlendState.Opaque;
		graphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
		graphicsDevice.DepthStencilState = DepthStencilState.Default;
		Texture2D sceneLightingDiffuseMap = buffer;
		Texture2D sceneLightingSpecularMap = null;
		if (EffectDetail != DetailPreference.Off)
		{
			sceneLightingSpecularMap = buffer2;
		}
		Vector3 down = Vector3.Down;
		ISceneState sceneState = SceneState;
		ISceneEnvironment environment = sceneState.Environment;
		foreach (h._0019 fN in fNs)
		{
			L.d d2 = OptimizationSystem.fNg.g_0011(fN.fNn);
			if (d2.fNL != null)
			{
				IDeferredObjectEffect deferredObjectEffect = d2.fNL;
				deferredObjectEffect.SceneLightingDiffuseMap = sceneLightingDiffuseMap;
				deferredObjectEffect.SceneLightingSpecularMap = sceneLightingSpecularMap;
				deferredObjectEffect.SetAmbientLighting(FrameAmbientLight, down);
				deferredObjectEffect.FogEnabled = environment.FogEnabled;
				if (environment.FogEnabled)
				{
					deferredObjectEffect.FogStartDistance = environment.FogStartDistance;
					deferredObjectEffect.FogEndDistance = environment.FogEndDistance;
					deferredObjectEffect.FogColor = environment.FogColor;
				}
				if (d2.fNB != null)
				{
					d2.fNB.SetViewAndProjection(sceneState.View, sceneState.ViewToWorld, sceneState.Projection, sceneState.ProjectionToView);
				}
			}
		}
		fN_0017._00034();
		fNm._00034();
		if (fN_0003)
		{
			graphicsDevice.BlendState = R.B.fNa;
			_0003_0004(fN_0011, null, lightMapManager, false, DeferredEffectOutput.Depth, false, RenderFillMode);
			graphicsDevice.BlendState = BlendState.Opaque;
			graphicsDevice.DepthStencilState = DepthStencilState.DepthRead;
			fN_0017._00034();
		}
		_0003_0004(fNs, null, lightMapManager, false, DeferredEffectOutput.Final, false, RenderFillMode);
		graphicsDevice.BlendState = BlendState.Opaque;
		graphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
		graphicsDevice.DepthStencilState = DepthStencilState.Default;
	}

	/// <summary>
	/// Finalizes rendering and cleans up frame information including removing all frame lifespan objects.
	/// </summary>
	public override void EndFrameRendering()
	{
		LightingSystemPerformance.Begin("RenderManager.EndFrameRendering");
		ISceneState sceneState = SceneState;
		base.EndFrameRendering();
		foreach (SceneEntity item in fNd)
		{
			item.RenderCustomPass(sceneState);
		}
		((ILightManager)ServiceProvider.GetManager(SceneInterface.LightManagerType, required: false))?.RenderVolumeLights();
		fN_0013._0013();
	}

	/// <summary>
	/// Removes all scene objects and cleans up scene information.
	/// </summary>
	public override void Clear()
	{
		base.Clear();
	}

	/// <summary>
	/// Unloads all scene and device specific data.  Must be called
	/// when the device is reset (during Game.UnloadGraphicsContent()).
	/// </summary>
	public override void Unload()
	{
		base.Unload();
		fN8 = null;
		_0013.B.nu(ref fNv);
		_0013.B.nu(ref fN_0012);
		_0013.B.nu(ref fN_0015);
		_0013.B.nu(ref fNi);
		_0013.B.nu(ref fN_0005);
	}

	private void _0003K(Texture2D P_0, Texture2D P_1)
	{
		LightingSystemPerformance.Begin("RenderManager.RenderLighting");
		GraphicsDevice graphicsDevice = GraphicsDeviceManager.GraphicsDevice;
		List<ShadowRenderTargetGroup> frameShadowRenderTargetGroups = FrameShadowRenderTargetGroups;
		graphicsDevice.BlendState = BlendState.Opaque;
		graphicsDevice.RasterizerState = RasterizerState.CullNone;
		graphicsDevice.DepthStencilState = DepthStencilState.Default;
		fN_0017._0003z(graphicsDevice, SamplerState.PointClamp);
		fN_0017._00034();
		fN_0015.SceneDepthMap = P_0;
		fN_0015.SetViewAndProjection(SceneState.View, SceneState.ViewToWorld, SceneState.Projection, SceneState.ProjectionToView);
		SceneState.FrameBuffers.FullFrameQuad.Render(fN_0015);
		graphicsDevice.BlendState = R.B.fNB;
		graphicsDevice.RasterizerState = RasterizerState.CullClockwise;
		graphicsDevice.DepthStencilState = DepthStencilState.DepthRead;
		fNv.SceneDepthMap = P_0;
		fNv.SceneNormalSpecularMap = P_1;
		fNv.EffectDetail = EffectDetail;
		fNv.ShadowDetail = ShadowDetail;
		fNv.LightGroupDebugAmount = fNn;
		fNv.SetViewAndProjection(SceneState.View, SceneState.ViewToWorld, SceneState.Projection, SceneState.ProjectionToView);
		fNv.CurrentTechnique.Passes[0].Apply();
		foreach (ShadowRenderTargetGroup item in frameShadowRenderTargetGroups)
		{
			foreach (ShadowGroup shadowGroup in item.ShadowGroups)
			{
				if (shadowGroup.Lights.Count < 1 || (fNf && !fN_0005.IsObjectVisible(shadowGroup)))
				{
					continue;
				}
				IShadowMap shadowMap = shadowGroup.Shadow as IShadowMap;
				IShadowSource shadowSource = shadowGroup.ShadowSource;
				bool flag = shadowSource is IPointSource;
				bool flag2 = !flag && shadowSource is IDirectionalSource;
				if (shadowMap != null)
				{
					shadowMap.BeginRendering(item.RenderTarget, fNv);
				}
				else if (flag)
				{
					fNv.SetShadowMapAndType(null, j.L.Point);
				}
				else if (flag2)
				{
					fNv.SetShadowMapAndType(null, j.L.Directional);
				}
				if (flag)
				{
					if (shadowSource.ShadowRenderLightsTogether)
					{
						_0003M(shadowGroup, false);
					}
					else
					{
						_0003_0006(shadowGroup);
					}
				}
				else if (flag2)
				{
					_0003M(shadowGroup, true);
				}
				shadowMap?.EndRendering();
				base.fNd.fN_0012.AccumulationValue++;
			}
		}
		graphicsDevice.DepthStencilState = DepthStencilState.None;
	}

	private void _0003M(ShadowGroup P_0, bool P_1)
	{
		LightingSystemPerformance.Begin("RenderManager.RenderLightingShadowGroupAsGroup");
		GraphicsDevice graphicsDevice = GraphicsDeviceManager.GraphicsDevice;
		FullFrameQuad fullFrameQuad = SceneState.FrameBuffers.FullFrameQuad;
		bool invertedWindings = SceneState.InvertedWindings;
		EffectPass effectPass = fNv.CurrentTechnique.Passes[0];
		if (!P_1)
		{
			BoundingSphere worldClippingSphere = BoundingSphere.CreateFromBoundingBox(P_0.BoundingBox);
			Matrix world = fNi.e(P_0.BoundingBox);
			fNv.WorldClippingSphere = worldClippingSphere;
			fNv.SetWorldAndWorldToObject(ref world, ref fNg);
			if (L._0019.a9(P_0.BoundingBox, 1.2f).Intersects(SceneState.ViewFrustum.Near) == PlaneIntersectionType.Intersecting)
			{
				if (invertedWindings)
				{
					graphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
				}
				else
				{
					graphicsDevice.RasterizerState = RasterizerState.CullClockwise;
				}
				graphicsDevice.DepthStencilState = R.B.fN_0019;
			}
			else
			{
				if (invertedWindings)
				{
					graphicsDevice.RasterizerState = RasterizerState.CullClockwise;
				}
				else
				{
					graphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
				}
				graphicsDevice.DepthStencilState = R.B.fNL;
			}
		}
		else
		{
			fNv.SetWorldAndWorldToObject(ref fNg, ref fNg);
			graphicsDevice.RasterizerState = RasterizerState.CullNone;
			graphicsDevice.DepthStencilState = R.B.fNv;
		}
		int num = 0;
		while (num < P_0.Lights.Count)
		{
			int num2 = Math.Min(fNv.MaxLightSources, P_0.Lights.Count - num);
			int num3 = num + num2;
			fNo.Clear();
			for (int k = num; k < num3; k++)
			{
				fNo.Add(P_0.Lights[k]);
				base.fNd.fNL.AccumulationValue++;
			}
			num = num3;
			fNv.LightSources = fNo;
			effectPass.Apply();
			if (!P_1)
			{
				fNi._5();
			}
			else
			{
				fullFrameQuad.Render();
			}
			base.fNd.fNv.AccumulationValue++;
		}
	}

	private void _0003_0006(ShadowGroup P_0)
	{
		LightingSystemPerformance.Begin("RenderManager.RenderLightingShadowGroupAsLights");
		GraphicsDevice graphicsDevice = GraphicsDeviceManager.GraphicsDevice;
		bool invertedWindings = SceneState.InvertedWindings;
		Plane near = SceneState.ViewFrustum.Near;
		EffectPass effectPass = fNv.CurrentTechnique.Passes[0];
		ModelMesh modelMesh = fNj.Meshes[0];
		ModelMeshPart modelMeshPart = modelMesh.MeshParts[0];
		bool flag = false;
		graphicsDevice.Indices = modelMeshPart.IndexBuffer;
		graphicsDevice.SetVertexBuffer(modelMeshPart.VertexBuffer, modelMeshPart.VertexOffset);
		List<BaseLight> lights = P_0.Lights;
		int count = lights.Count;
		for (int k = 0; k < count; k++)
		{
			BaseLight baseLight = lights[k];
			L.o o2 = OptimizationSystem.fNB.g_0011(baseLight);
			fNo.Clear();
			fNo.Add(baseLight);
			fNv.LightSources = fNo;
			if (o2.fNa != null)
			{
				BoundingBox worldBoundingBox = baseLight.WorldBoundingBox;
				BoundingSphere worldClippingSphere = BoundingSphere.CreateFromBoundingBox(worldBoundingBox);
				if (L._0019.a9(worldBoundingBox, 1.2f).Intersects(near) == PlaneIntersectionType.Intersecting)
				{
					if (invertedWindings)
					{
						graphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
					}
					else
					{
						graphicsDevice.RasterizerState = RasterizerState.CullClockwise;
					}
					graphicsDevice.DepthStencilState = R.B.fN_0019;
				}
				else
				{
					if (invertedWindings)
					{
						graphicsDevice.RasterizerState = RasterizerState.CullClockwise;
					}
					else
					{
						graphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
					}
					graphicsDevice.DepthStencilState = R.B.fNL;
				}
				Matrix world = fNi.e(worldBoundingBox);
				Matrix worldtoobj = Matrix.Identity;
				fNv.WorldClippingSphere = worldClippingSphere;
				fNv.SetWorldAndWorldToObject(ref world, ref worldtoobj);
				effectPass.Apply();
				fNi._5();
				flag = true;
			}
			else
			{
				BoundingSphere worldBoundingSphere = baseLight.WorldBoundingSphere;
				BoundingSphere sphere = L._0019.a_0013(worldBoundingSphere, 1.2f);
				if (near.Intersects(sphere) == PlaneIntersectionType.Intersecting)
				{
					if (invertedWindings)
					{
						graphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
					}
					else
					{
						graphicsDevice.RasterizerState = RasterizerState.CullClockwise;
					}
					graphicsDevice.DepthStencilState = R.B.fN_0019;
				}
				else
				{
					if (invertedWindings)
					{
						graphicsDevice.RasterizerState = RasterizerState.CullClockwise;
					}
					else
					{
						graphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
					}
					graphicsDevice.DepthStencilState = R.B.fNL;
				}
				Matrix world2 = Matrix.CreateScale(worldBoundingSphere.Radius) * Matrix.CreateTranslation(worldBoundingSphere.Center);
				Matrix worldtoobj2 = Matrix.Identity;
				fNv.WorldClippingSphere = worldBoundingSphere;
				fNv.SetWorldAndWorldToObject(ref world2, ref worldtoobj2);
				effectPass.Apply();
				if (flag)
				{
					graphicsDevice.Indices = modelMeshPart.IndexBuffer;
					graphicsDevice.SetVertexBuffer(modelMeshPart.VertexBuffer, modelMeshPart.VertexOffset);
					flag = false;
				}
				graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, modelMeshPart.NumVertices, modelMeshPart.StartIndex, modelMeshPart.PrimitiveCount);
			}
			base.fNd.fNL.AccumulationValue++;
			base.fNd.fNv.AccumulationValue++;
		}
	}

	private void _0003J(ShadowGroup P_0, IShadowMap P_1, IObjectManager P_2, IAvatarManager P_3, ILightMapManager P_4)
	{
		base.fNd.fN8.AccumulationValue++;
		fNh.Clear();
		P_2.Find(objectfilter: (P_0.ShadowSource.ShadowType != ShadowType.AllObjects) ? ObjectFilter.Static : ObjectFilter.DynamicAndStatic, foundobjects: fNh, worldbounds: P_0.BoundingBox);
		fNh.Sort(fN9);
		fN_0013._0003Y(fNR, fNh, false, fN_0019);
		P_3?.BeginShadowGroupRendering(P_0);
		_ = P_0.ShadowSource.ShadowPosition;
		fN_0012.EffectDetail = EffectDetail;
		fN_0012.ShadowArea = P_0.BoundingSphereCentered;
		for (int k = 0; k < P_1.Surfaces.Length; k++)
		{
			ShadowMapSurface shadowMapSurface = P_1.Surfaces[k];
			base.fNd.fNi.AccumulationValue++;
			if (!P_1.IsSurfaceVisible(k, SceneState.ViewFrustum))
			{
				continue;
			}
			P_1.BeginSurfaceRendering(k, fN_0012);
			LightingSystemPerformance.Begin("RenderManager.BuildShadowMaps (object filter loop)");
			h.L._00035(fNh, shadowMapSurface.Frustum);
			foreach (h._0019 item in fNR)
			{
				if (!item.fNB)
				{
					continue;
				}
				L.d d2 = null;
				if (!item.fN_0003)
				{
					L.d d3 = OptimizationSystem.fNg.g_0011(item.fNn);
					L.d d4 = OptimizationSystem.fNg.g_0011(fN_0012);
					EffectHelper.f1(d3, d4);
					fN_0012.Skinned = item.fNa;
					d2 = d4;
				}
				else
				{
					L.d d5 = OptimizationSystem.fNg.g_0011(item.fNn);
					IRenderableEffect renderableEffect = d5.fNB;
					IShadowGenerateEffect shadowGenerateEffect = d5.fNv;
					if (renderableEffect == null || shadowGenerateEffect == null)
					{
						continue;
					}
					renderableEffect.View = shadowMapSurface.WorldToSurfaceView;
					renderableEffect.Projection = shadowMapSurface.Projection;
					renderableEffect.EffectDetail = EffectDetail;
					shadowGenerateEffect.ShadowPrimaryBias = P_0.ShadowSource.ShadowPrimaryBias;
					shadowGenerateEffect.ShadowSecondaryBias = P_0.ShadowSource.ShadowSecondaryBias;
					shadowGenerateEffect.ShadowArea = P_0.BoundingSphereCentered;
					shadowGenerateEffect.SetCameraView(SceneState.View, SceneState.ViewToWorld);
					if (d5.fN8 != null)
					{
						d5.fN8.GameTime = SceneState.GameTime;
					}
					d2 = d5;
				}
				_00033(item.Objects, d2, P_4, true, DeferredEffectOutput.ShadowDepth, true, true, FillMode.Solid);
			}
			if (P_3 != null && P_3.RenderToShadowMapSurface(P_0, shadowMapSurface, fN_0012))
			{
				fNm._00034();
				fN_0017._00034();
			}
			P_1.EndSurfaceRendering();
			base.fNd.fN_0005.AccumulationValue++;
		}
		P_3?.EndShadowGroupRendering(P_0);
	}

	private void _0003_0004(List<h._0019> P_0, ShadowGroup P_1, ILightMapManager P_2, bool P_3, DeferredEffectOutput P_4, bool P_5, FillMode P_6)
	{
		foreach (h._0019 item in P_0)
		{
			if (!P_3 || item.fNB)
			{
				L.d d2 = OptimizationSystem.fNg.g_0011(item.fNn);
				if (d2.fNB != null)
				{
					d2.fNB.EffectDetail = EffectDetail;
				}
				if (P_1 != null && d2.fNv != null)
				{
					IShadowGenerateEffect shadowGenerateEffect = d2.fNv;
					shadowGenerateEffect.ShadowPrimaryBias = P_1.ShadowSource.ShadowPrimaryBias;
					shadowGenerateEffect.ShadowSecondaryBias = P_1.ShadowSource.ShadowSecondaryBias;
					shadowGenerateEffect.ShadowArea = P_1.BoundingSphereCentered;
				}
				bool flag = P_4 != DeferredEffectOutput.ShadowDepth || d2.fN_0003 == null || d2.fN_0003.TransparencyMode == TransparencyMode.Clip;
				_00033(item.Objects, d2, P_2, P_3, P_4, flag, P_5, P_6);
			}
		}
	}

	private void _00033(List<RenderableMesh> P_0, L.d P_1, ILightMapManager P_2, bool P_3, DeferredEffectOutput P_4, bool P_5, bool P_6, FillMode P_7)
	{
		if (P_0.Count < 1)
		{
			return;
		}
		LightingSystemPerformance.Begin("RenderManager.RenderObjectBatch");
		if (P_1.fNL != null)
		{
			P_1.fNL.DeferredEffectOutput = P_4;
			if (P_3)
			{
				bool flag = false;
				foreach (RenderableMesh item in P_0)
				{
					if (item.fNG && item.fNa.Valid)
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
			if (P_1.fNB != null)
			{
				flag2 = P_1.fNB.DoubleSided;
			}
			GraphicsDevice graphicsDevice = GraphicsDeviceManager.GraphicsDevice;
			CullMode cullMode = CullMode.CullCounterClockwiseFace;
			if (SceneState.InvertedWindings)
			{
				P_6 = !P_6;
			}
			R.B._0003l(graphicsDevice, P_7, cullMode, P_6, flag2, false);
			SamplerState samplerState = null;
			if (P_5)
			{
				if (P_1.fN_0019 != null)
				{
					IAddressableEffect addressableEffect = P_1.fN_0019;
					samplerState = fN_0017._0003_0010(graphicsDevice, addressableEffect.AddressModeU, addressableEffect.AddressModeV, addressableEffect.AddressModeW, Filter, MaxAnisotropy);
				}
				else
				{
					samplerState = fN_0017._0003_0010(graphicsDevice, TextureAddressMode.Wrap, TextureAddressMode.Wrap, TextureAddressMode.Wrap, Filter, MaxAnisotropy);
				}
			}
			else
			{
				fN_0017._0003z(graphicsDevice, SamplerState.PointClamp);
			}
			EffectPassCollection passes = P_1.fNi.CurrentTechnique.Passes;
			base.fNd.fNf.AccumulationValue++;
			base.fNd.fN_0003.AccumulationValue += passes.Count;
			ISkinnedEffect skinnedEffect = P_1.fNg;
			IRenderableEffect renderableEffect = P_1.fNB;
			IStaticLightingEffect staticLightingEffect = P_1.fNf;
			BaseRenderableEffect baseRenderableEffect = P_1.fN_0012;
			_ = P_1.fN_0015;
			if (P_1.fN8 != null || P_1.fNn != null)
			{
				fN_0017._00034();
			}
			bool flag3 = true;
			bool flag4 = P_4 == DeferredEffectOutput.Final && staticLightingEffect != null && P_2 != null;
			for (int k = 0; k < passes.Count; k++)
			{
				EffectPass effectPass = passes[k];
				effectPass.Apply();
				foreach (RenderableMesh item2 in P_0)
				{
					if (item2 == null || (P_3 && !item2.fNG) || !item2.fNa.Valid)
					{
						continue;
					}
					bool flag5 = false;
					if (flag4)
					{
						StaticLightingType staticLightingType = item2.fNa.StaticLightingType;
						if (staticLightingType != StaticLightingType.Composite || !fNG.TryGetValue(item2.fNa, out var value))
						{
							value = fNQ;
						}
						if (staticLightingType == StaticLightingType.BakedDown)
						{
							staticLightingEffect.SetStaticLighting(P_2.GetLightMap(item2), ref value);
						}
						else
						{
							staticLightingEffect.SetStaticLighting(null, ref value);
						}
						flag5 = true;
					}
					if (skinnedEffect != null)
					{
						skinnedEffect.SkinBones = item2.fNa.SkinBones;
						flag5 = true;
					}
					if (renderableEffect != null)
					{
						renderableEffect.SetWorldAndWorldToObject(ref item2.fN8, ref item2.fNi);
						flag5 = true;
					}
					if (baseRenderableEffect != null)
					{
						flag5 = baseRenderableEffect.UpdatedByBatch;
						baseRenderableEffect.UpdatedByBatch = false;
					}
					if (flag5)
					{
						effectPass.Apply();
						base.fNd.fN_0019.AccumulationValue++;
					}
					if (!flag2 && cullMode != item2.fNQ)
					{
						cullMode = item2.fNQ;
						R.B._0003l(graphicsDevice, P_7, cullMode, P_6, flag2, false);
						base.fNd.fNn.AccumulationValue++;
					}
					fNm.h(graphicsDevice, item2);
					try
					{
						if (flag3 && samplerState != null)
						{
							fN_0017._0003z(graphicsDevice, samplerState);
						}
						if (item2.fN_0005 == null)
						{
							graphicsDevice.DrawPrimitives(item2.fNs, item2.fNw, item2.fN_0011);
						}
						else
						{
							graphicsDevice.DrawIndexedPrimitives(item2.fNs, item2.fNc, 0, item2.fNd, item2.fNw, item2.fN_0011);
						}
					}
					catch (Exception ex)
					{
						item2.fNa.Valid = false;
						item2.fNa.RenderingErrors = ex.Message;
						LightingSystemEditor.gJ(item2.fNa, v.g.Error);
						OnXNARuntimeException(ex, $"Unable to render object '{item2.fNa.Name}'.");
					}
					flag3 = false;
					base.fNd.fNg.AccumulationValue++;
					base.fNd.fNB.AccumulationValue += item2.fN_0011;
				}
			}
			if (P_1.fN8 != null || P_1.fNn != null)
			{
				fN_0017._00034();
			}
			return;
		}
		string text = string.Empty;
		if (P_0[0].fNa != null)
		{
			text = P_0[0].fNa.Name;
		}
		throw new Exception("Deferred rendering does not support non-deferred effects (SceneObject '" + text + "'). Make sure effects derive from IDeferredObjectEffect or model processors are set to a deferred processor.");
	}
}
