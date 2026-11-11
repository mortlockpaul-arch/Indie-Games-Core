using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Editor;
using SynapseGaming.LightingSystem.Effects;
using SynapseGaming.LightingSystem.Effects.Forward;
using SynapseGaming.LightingSystem.Lights;
using SynapseGaming.LightingSystem.Shadows;
using SynapseGaming.LightingSystem.Shadows.Deferred;
using _id_5;
using _u0004_;
using s;
using x;

namespace SynapseGaming.LightingSystem.Rendering.Forward;

public class RenderManager : BaseRenderManager
{
	private bool l8a;

	private bool l8_u0013_ = true;

	private bool l8N;

	private int l8l = 1;

	private FogEffect l8L;

	private List<SceneEntity> l8w = new List<SceneEntity>();

	private List<RenderableMesh> l8J = new List<RenderableMesh>();

	private List<x.J> l8B = new List<x.J>();

	private List<x.J> l8s = new List<x.J>();

	private List<x.J> l8h = new List<x.J>();

	private CompositeLighting l8_u0002_ = default;

	private List<BaseLight> l8v = new List<BaseLight>();

	private Dictionary<object, CompositeLighting> l8Y = new Dictionary<object, CompositeLighting>(32);

	private Dictionary<object, CompositeLighting> l8_u0005_ = new Dictionary<object, CompositeLighting>(32);

	private x.h l8b = new x.h();

	private x._u0002_ l8I = new x._u0002_();

	private List<BaseLight> l85 = new List<BaseLight>();

	private List<RenderableMesh> l8W = new List<RenderableMesh>();

	private List<x.J> l8Z = new List<x.J>();

	private new List<x.J> l8T = new List<x.J>();

	private List<x.J> l8_u0014_ = new List<x.J>();

	private static x.w l8E = new x.w();

	private static x.L l8f = new x.L();

	private static x.s l8_u0016_ = new x.s();

	public bool MultiPassEdgeCleanupEnabled
	{
		get
		{
			return l8_u0013_;
		}
		set
		{
			l8_u0013_ = value;
		}
	}

	public RenderManager(IManagerServiceProvider sceneinterface)
		: base(sceneinterface)
	{
		AmbientLight item = new AmbientLight
		{
			DiffuseColor = Vector3.Zero
		};
		l8v.Add(item);
	}

	public override void BeginFrameRendering(ISceneState scenestate)
	{
		LightingSystemPerformance.Begin("RenderManager.BeginFrameRendering");
		l8b.L_u0012_();
		l8I.L_u0012_();
		base.BeginFrameRendering(scenestate);
		_ = scenestate.ViewToWorld.Translation;
		GraphicsDevice graphicsDevice = SunBurnCoreSystem.Instance.GraphicsDeviceManager.GraphicsDevice;
		RasterizerState rasterizerState = graphicsDevice.RasterizerState;
		if (rasterizerState == null)
		{
			rasterizerState = RasterizerState.CullCounterClockwise;
		}
		l8N = false;
		if (l8_u0013_)
		{
			RenderTargetBinding[] renderTargets = graphicsDevice.GetRenderTargets();
			if (renderTargets.Length > 0)
			{
				if (renderTargets[0].RenderTarget is RenderTarget2D renderTarget2D)
				{
					l8N = renderTarget2D.DepthStencilFormat == DepthFormat.Depth24Stencil8;
				}
			}
			else
			{
				l8N = graphicsDevice.PresentationParameters.DepthStencilFormat == DepthFormat.Depth24Stencil8;
			}
		}
		if (l8L == null)
		{
			l8L = new FogEffect(graphicsDevice);
		}
		LightingSystemPerformance.Begin("RenderManager.BeginFrameRendering (find objects)");
		l8w.Clear();
		l8J.Clear();
		l8Y.Clear();
		l8_u0005_.Clear();
		IObjectManager objectManager = (IObjectManager)ServiceProvider.GetManager(SceneInterface.ObjectManagerType, required: false);
		ILightManager lightmanager = (ILightManager)ServiceProvider.GetManager(SceneInterface.LightManagerType, required: false);
		objectManager?.Find(l8w, SceneState.ViewFrustum, ObjectFilter.DynamicAndStatic);
		LightingSystemPerformance.Begin("RenderManager.BeginFrameRendering (filter objects)");
		ExtractAndFilterRenderableMeshes(l8w, lightmanager, l8a, l8J, l8Y, l8_u0005_);
		LightingSystemPerformance.Begin("RenderManager.BeginFrameRendering (sort and batch)");
		l8J.Sort(l8E);
		l8_u0016_.L7(SceneState.View, SceneState.ViewToWorld, SceneState.Projection, SceneState.ProjectionToView, l8B, l8J, x.N.LightingEffect | x.N.BasicEffect_Lighting | x.N.BasicEffect_NonLighting | x.N.MiscEffect);
		l8_u0016_.L7(SceneState.View, SceneState.ViewToWorld, SceneState.Projection, SceneState.ProjectionToView, l8s, l8J, x.N.LightingEffect);
		l8_u0016_.L7(SceneState.View, SceneState.ViewToWorld, SceneState.Projection, SceneState.ProjectionToView, l8h, l8J, x.N.LightingEffect | x.N.BasicEffect_Lighting);
		LightingSystemPerformance.Begin("RenderManager.BeginFrameRendering (shadows)");
		List<ShadowRenderTargetGroup> frameShadowRenderTargetGroups = FrameShadowRenderTargetGroups;
		frameShadowRenderTargetGroups.Clear();
		IShadowMapManager shadowMapManager = (IShadowMapManager)ServiceProvider.GetManager(SceneInterface.ShadowMapManagerType, required: false);
		if (shadowMapManager == null)
		{
			GetDefaultShadows(frameShadowRenderTargetGroups, FrameLights);
		}
		else
		{
			if (shadowMapManager is DeferredShadowMapManager)
			{
				throw new Exception("Cannot use a deferred shadow map manager with a forward render manager. Please switch to a forward shadow map manager.");
			}
			shadowMapManager.BuildShadows(frameShadowRenderTargetGroups, FrameLights, usedefaultgrouping: false);
		}
		if (ShadowDetail != DetailPreference.Off && objectManager != null)
		{
			graphicsDevice.BlendState = BlendState.Opaque;
			graphicsDevice.DepthStencilState = DepthStencilState.Default;
			BuildShadowMaps(frameShadowRenderTargetGroups);
		}
		if (ClearBackBufferEnabled)
		{
			LightingSystemPerformance.Begin("RenderManager.BeginFrameRendering (clear backbuffer)");
			Color color = new Color(scenestate.Environment.FogColor);
			ClearOptions clearOptions = ClearOptions.Target | ClearOptions.DepthBuffer;
			if (l8_u0013_ && l8N)
			{
				clearOptions |= ClearOptions.Stencil;
			}
			graphicsDevice.Clear(clearOptions, color, 1f, 0);
		}
		graphicsDevice.RasterizerState = rasterizerState;
	}

	protected override void BuildShadowMaps(List<ShadowRenderTargetGroup> shadowrendertargetgroups)
	{
		GraphicsDevice graphicsDevice = SunBurnCoreSystem.Instance.GraphicsDeviceManager.GraphicsDevice;
		IObjectManager objectManager = (IObjectManager)ServiceProvider.GetManager(SceneInterface.ObjectManagerType, required: false);
		IAvatarManager avatarManager = (IAvatarManager)ServiceProvider.GetManager(SceneInterface.AvatarManagerType, required: false);
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
				if (shadow == null)
				{
					continue;
				}
				base.l8T.l8v.AccumulationValue++;
				l8W.Clear();
				objectManager.Find(objectfilter: (shadowGroup.ShadowSource.ShadowType != ShadowType.AllObjects) ? ObjectFilter.Static : ObjectFilter.DynamicAndStatic, foundobjects: l8W, worldbounds: shadowGroup.BoundingBox);
				l8W.Sort(l8f);
				l8_u0016_.LQ(l8Z, l8W, param_2: false, l8a);
				avatarManager?.BeginShadowGroupRendering(shadowGroup);
				_ = shadowGroup.ShadowSource.ShadowPosition;
				for (int i = 0; i < shadow.Surfaces.Length; i++)
				{
					ShadowMapSurface shadowMapSurface = shadow.Surfaces[i];
					base.l8T.l8Y.AccumulationValue++;
					if (!shadow.IsSurfaceVisible(i, SceneState.ViewFrustum))
					{
						continue;
					}
					shadow.BeginSurfaceRendering(i);
					LightingSystemPerformance.Begin("RenderManager.BuildShadowMaps (object filter loop)");
					x.B.L2(l8W, shadowMapSurface.Frustum);
					foreach (x.J item in l8Z)
					{
						if (item.l8a)
						{
							EffectTypeCaster effectTypeCaster = OptimizationSystem.EffectTypeCasters.Get(item.l8w);
							EffectTypeCaster effectTypeCaster2 = OptimizationSystem.EffectTypeCasters.Get(shadow.ShadowEffect);
							EffectHelper.lv(effectTypeCaster, effectTypeCaster2);
							if (effectTypeCaster2.SkinnedEffect != null)
							{
								effectTypeCaster2.SkinnedEffect.Skinned = item.l8_u0013_;
							}
							L6(graphicsDevice, item.Objects, effectTypeCaster2, null, param_4: true, param_5: true, param_6: true, w8a.Solid, FillMode.Solid, param_9: false, param_10: false, effectTypeCaster.TerrainEffect != null);
						}
					}
					if (avatarManager != null && avatarManager.RenderToShadowMapSurface(shadowGroup, shadowMapSurface, shadow.ShadowEffect))
					{
						l8I.L_u0012_();
						l8b.L_u0012_();
					}
					shadow.EndSurfaceRendering();
					base.l8T.l8_u0005_.AccumulationValue++;
				}
				avatarManager?.EndShadowGroupRendering(shadowGroup);
				shadow.ContentsAreValid = true;
			}
			shadowrendertargetgroup.End();
		}
	}

	public override void Render()
	{
		if (SceneState == null)
		{
			return;
		}
		LightingSystemPerformance.Begin("RenderManager.Render");
		GraphicsDevice graphicsDevice = SunBurnCoreSystem.Instance.GraphicsDeviceManager.GraphicsDevice;
		ILightMapManager param_ = (ILightMapManager)ServiceProvider.GetManager(SceneInterface.LightMapManagerType, required: false);
		l8b.L_u0012_();
		l8I.L_u0012_();
		graphicsDevice.BlendState = BlendState.Opaque;
		graphicsDevice.DepthStencilState = DepthStencilState.Default;
		RasterizerState rasterizerState = graphicsDevice.RasterizerState;
		if (rasterizerState == null)
		{
			rasterizerState = RasterizerState.CullCounterClockwise;
		}
		ISceneState sceneState = SceneState;
		foreach (x.J item in l8B)
		{
			EffectTypeCaster effectTypeCaster = OptimizationSystem.EffectTypeCasters.Get(item.l8w);
			if (effectTypeCaster.RenderableEffect != null)
			{
				effectTypeCaster.RenderableEffect.SetViewAndProjection(sceneState.View, sceneState.ViewToWorld, sceneState.Projection, sceneState.ProjectionToView);
			}
			else if (effectTypeCaster.EffectMatrices != null)
			{
				effectTypeCaster.EffectMatrices.View = sceneState.View;
				effectTypeCaster.EffectMatrices.Projection = sceneState.Projection;
			}
		}
		LightingSystemPerformance.Begin("RenderManager.Render (ambient)");
		L_u0016_(graphicsDevice, l8B, FrameAmbientLights, param_, param_4: false, RenderFillMode, param_6: false, param_7: false);
		graphicsDevice.DepthStencilState = _u0004_.a.l8w;
		LightingSystemPerformance.Begin("RenderManager.Render (lighting loop)");
		List<ShadowRenderTargetGroup> frameShadowRenderTargetGroups = FrameShadowRenderTargetGroups;
		foreach (ShadowRenderTargetGroup item2 in frameShadowRenderTargetGroups)
		{
			foreach (ShadowGroup shadowGroup in item2.ShadowGroups)
			{
				L3(graphicsDevice, item2, shadowGroup);
			}
		}
		if (SceneState.Environment.FogEnabled)
		{
			LightingSystemPerformance.Begin("RenderManager.Render (fog)");
			EffectTypeCaster effectTypeCaster2 = OptimizationSystem.EffectTypeCasters.Get(l8L);
			l8L.SetViewAndProjection(SceneState.View, SceneState.ViewToWorld, SceneState.Projection, SceneState.ProjectionToView);
			graphicsDevice.BlendState = BlendState.NonPremultiplied;
			graphicsDevice.DepthStencilState = _u0004_.a.l8w;
			l8L.StartDistance = SceneState.Environment.FogStartDistance;
			l8L.EndDistance = SceneState.Environment.FogEndDistance;
			l8L.Color = SceneState.Environment.FogColor;
			l8_u0016_.LQ(l8_u0014_, l8J, param_2: false, l8a);
			foreach (x.J item3 in l8_u0014_)
			{
				EffectTypeCaster effectTypeCaster3 = OptimizationSystem.EffectTypeCasters.Get(item3.l8w);
				EffectHelper.lv(effectTypeCaster3, effectTypeCaster2);
				l8L.Skinned = item3.l8_u0013_;
				L6(graphicsDevice, item3.Objects, effectTypeCaster2, null, param_4: false, param_5: true, param_6: false, w8a.Solid, RenderFillMode, param_9: false, param_10: false, effectTypeCaster3.TerrainEffect != null);
			}
		}
		graphicsDevice.BlendState = BlendState.Opaque;
		graphicsDevice.DepthStencilState = DepthStencilState.Default;
		graphicsDevice.RasterizerState = rasterizerState;
	}

	public override void EndFrameRendering()
	{
		LightingSystemPerformance.Begin("RenderManager.EndFrameRendering");
		ISceneState sceneState = SceneState;
		foreach (SceneEntity item in l8w)
		{
			item.RenderCustomPass(sceneState);
		}
		base.EndFrameRendering();
		l8_u0016_._id_3();
	}

	public override void Unload()
	{
		if (l8L != null)
		{
			l8L.Dispose();
			l8L = null;
		}
		base.Unload();
	}

	protected override bool CanBatchTransparencies(ISceneState scenestate)
	{
		if (scenestate.Environment.FogEnabled)
		{
			return false;
		}
		return true;
	}

	internal override void r4_u0015__u0011_1(List<RenderableMesh> param_0, bool param_1)
	{
		if (param_0.Count <= 0)
		{
			return;
		}
		ISceneState sceneState = SceneState;
		_ = sceneState.Environment;
		ILightMapManager param_2 = (ILightMapManager)ServiceProvider.GetManager(SceneInterface.LightMapManagerType, required: false);
		GraphicsDevice graphicsDevice = SunBurnCoreSystem.Instance.GraphicsDeviceManager.GraphicsDevice;
		EffectTypeCaster effectTypeCaster = OptimizationSystem.EffectTypeCasters.Get(param_0[0].Effect);
		w8a param_3 = w8a.TransparentDoubleSided;
		if (effectTypeCaster.EffectMatrices != null)
		{
			effectTypeCaster.EffectMatrices.View = sceneState.View;
			effectTypeCaster.EffectMatrices.Projection = sceneState.Projection;
			graphicsDevice.BlendState = BlendState.NonPremultiplied;
		}
		if (effectTypeCaster.RenderableEffect != null)
		{
			effectTypeCaster.RenderableEffect.SetViewAndProjection(sceneState.View, sceneState.ViewToWorld, sceneState.Projection, sceneState.ProjectionToView);
			param_3 = ((effectTypeCaster.RenderableEffect == null || !effectTypeCaster.RenderableEffect.DoubleSided) ? w8a.TransparentSingleSided : w8a.TransparentDoubleSided);
			if (effectTypeCaster.TransparentEffect != null && effectTypeCaster.TransparentEffect.TransparencyMode == TransparencyMode.Additive)
			{
				graphicsDevice.BlendState = BlendState.Additive;
			}
			else
			{
				graphicsDevice.BlendState = BlendState.NonPremultiplied;
			}
		}
		if (effectTypeCaster.LightingEffect != null)
		{
			effectTypeCaster.LightingEffect.LightSources = l8v;
		}
		graphicsDevice.DepthStencilState = DepthStencilState.Default;
		if (param_1)
		{
			l8b.L_u0012_();
			l8I.L_u0012_();
		}
		L6(graphicsDevice, param_0, effectTypeCaster, param_2, param_4: false, param_5: true, param_6: false, param_3, RenderFillMode, param_9: false, param_10: false, param_11: false);
		if (SceneState.Environment.FogEnabled)
		{
			EffectTypeCaster effectTypeCaster2 = OptimizationSystem.EffectTypeCasters.Get(l8L);
			l8L.SetViewAndProjection(SceneState.View, SceneState.ViewToWorld, SceneState.Projection, SceneState.ProjectionToView);
			graphicsDevice.BlendState = BlendState.NonPremultiplied;
			graphicsDevice.DepthStencilState = _u0004_.a.l8w;
			l8L.StartDistance = SceneState.Environment.FogStartDistance;
			l8L.EndDistance = SceneState.Environment.FogEndDistance;
			l8L.Color = SceneState.Environment.FogColor;
			EffectHelper.lv(effectTypeCaster, effectTypeCaster2);
			if (effectTypeCaster.SkinnedEffect != null)
			{
				l8L.Skinned = effectTypeCaster.SkinnedEffect.Skinned;
			}
			else
			{
				l8L.Skinned = false;
			}
			L6(graphicsDevice, param_0, effectTypeCaster2, null, param_4: false, param_5: true, param_6: false, param_3, RenderFillMode, param_9: false, param_10: false, effectTypeCaster.TerrainEffect != null);
		}
	}

	private void L3(GraphicsDevice param_0, ShadowRenderTargetGroup param_1, ShadowGroup param_2)
	{
		if (param_2.Lights.Count < 1 || l8J.Count < 1)
		{
			return;
		}
		base.l8T.l8h.AccumulationValue++;
		base.l8T.l8B.AccumulationValue += param_2.Lights.Count;
		bool flag = param_2.ShadowSourceTypes.PointSource != null;
		List<x.J> list = (flag ? l8s : l8h);
		LightingSystemPerformance.Begin("RenderManager.RenderShadowGroup (object filter loop)");
		foreach (x.J item in list)
		{
			if (flag)
			{
				item.l8a = x.B.L_u0017_(item.Objects, param_2.BoundingBox);
				continue;
			}
			item.l8a = true;
			foreach (RenderableMesh item2 in item.Objects)
			{
				item2.l85 = true;
			}
		}
		if (flag)
		{
			Rectangle screenArea = CoreHelper.GetScreenArea(param_2.BoundingBox, param_0.Viewport, SceneState.ViewProjection, SceneState.ViewToWorld);
			if ((float)screenArea.Width <= 0f || (float)screenArea.Height <= 0f)
			{
				return;
			}
			param_0.ScissorRectangle = screenArea;
		}
		if (ShadowDetail != DetailPreference.Off)
		{
			IShadowMap shadow = param_2.Shadow;
			if (shadow != null && shadow.ShadowEffect is IRenderableEffect)
			{
				l8T.Clear();
				l8_u0016_.LQ(l8T, l8J, param_2: true, l8a);
				param_0.BlendState = _u0004_.a.l8N;
				param_0.DepthStencilState = _u0004_.a.l8w;
				foreach (x.J item3 in l8T)
				{
					if (item3.l8a)
					{
						EffectTypeCaster effectTypeCaster = OptimizationSystem.EffectTypeCasters.Get(item3.l8w);
						EffectTypeCaster effectTypeCaster2 = OptimizationSystem.EffectTypeCasters.Get(shadow.ShadowEffect);
						EffectHelper.lv(effectTypeCaster, effectTypeCaster2);
						Lj(param_0, param_1, param_2, shadow, effectTypeCaster2, item3, flag, effectTypeCaster.TerrainEffect != null);
					}
				}
				param_0.BlendState = _u0004_.a.l8l;
			}
			else
			{
				param_0.BlendState = _u0004_.a.l8a;
			}
		}
		else
		{
			param_0.BlendState = _u0004_.a.l8a;
		}
		if (param_2.Lights.Count == 1 && l8_u0013_ && l8N)
		{
			param_0.DepthStencilState = _u0004_.a.l8h;
			param_0.ReferenceStencil = l8l;
			l8l++;
			if (l8l > 250)
			{
				l8l = 1;
			}
		}
		else
		{
			param_0.DepthStencilState = _u0004_.a.l8w;
		}
		L_u0016_(param_0, list, param_2.Lights, null, param_4: true, RenderFillMode, flag, param_7: false);
	}

	private void Lj(GraphicsDevice param_0, ShadowRenderTargetGroup param_1, ShadowGroup param_2, IShadowMap param_3, EffectTypeCaster param_4, x.J param_5, bool param_6, bool param_7)
	{
		if (param_5.Objects.Count >= 1 && param_2.Lights.Count >= 1 && param_3 != null)
		{
			ISkinnedEffect skinnedEffect = param_4.SkinnedEffect;
			if (param_4.Effect is _id_5.a a2)
			{
				a2.EffectDetail = ShadowDetail;
			}
			param_3.BeginRendering(param_1.RenderTarget);
			if (skinnedEffect != null)
			{
				skinnedEffect.Skinned = param_5.l8_u0013_;
			}
			L6(param_0, param_5.Objects, param_4, null, param_4: false, param_5: true, param_6: false, w8a.Solid, RenderFillMode, param_6, param_10: true, param_7);
			param_3.EndRendering();
		}
	}

	private void L_u0016_(GraphicsDevice param_0, List<x.J> param_1, List<BaseLight> param_2, ILightMapManager param_3, bool param_4, FillMode param_5, bool param_6, bool param_7)
	{
		LightingSystemPerformance.Begin("RenderManager.RenderObjectBatches");
		foreach (x.J item in param_1)
		{
			if (param_4 && !item.l8a)
			{
				continue;
			}
			EffectTypeCaster effectTypeCaster = OptimizationSystem.EffectTypeCasters.Get(item.l8w);
			bool param_8 = effectTypeCaster.TerrainEffect != null;
			if (effectTypeCaster.RenderableEffect != null)
			{
				effectTypeCaster.RenderableEffect.EffectDetail = EffectDetail;
			}
			else
			{
				IEffectLights effectLights = effectTypeCaster.EffectLights;
				if (effectLights != null && effectLights.LightingEnabled && param_2.Count > 0)
				{
					BaseLight baseLight = param_2[0];
					LightTypeCaster lightTypeCaster = OptimizationSystem.LightTypeCasters.Get(baseLight);
					if (lightTypeCaster.DirectionalSource != null && lightTypeCaster.SpotSource == null)
					{
						effectLights.AmbientLightColor = default;
						effectLights.DirectionalLight0.Enabled = true;
						effectLights.DirectionalLight0.DiffuseColor = baseLight.CompositeColorAndIntensity;
						effectLights.DirectionalLight0.Direction = lightTypeCaster.DirectionalSource.Direction;
						effectLights.DirectionalLight1.Enabled = false;
						effectLights.DirectionalLight2.Enabled = false;
					}
					else
					{
						if (lightTypeCaster.AmbientSource == null)
						{
							throw new ArgumentException("BasicEffect / IEffectLights can only render directional lights.");
						}
						effectLights.AmbientLightColor = baseLight.CompositeColorAndIntensity;
						effectLights.DirectionalLight0.Enabled = false;
						effectLights.DirectionalLight1.Enabled = false;
						effectLights.DirectionalLight2.Enabled = false;
					}
				}
			}
			_ = item.l8w;
			ILightingEffect lightingEffect = effectTypeCaster.LightingEffect;
			if (param_2.Count > 0 && lightingEffect != null)
			{
				int maxLightSources = lightingEffect.MaxLightSources;
				if (param_2.Count > maxLightSources)
				{
					l85.Clear();
					for (int i = 0; i < param_2.Count; i++)
					{
						l85.Add(param_2[i]);
						if (l85.Count >= maxLightSources || i + 1 >= param_2.Count)
						{
							lightingEffect.LightSources = l85;
							L6(param_0, item.Objects, effectTypeCaster, param_3, param_4, param_5: true, param_6: false, w8a.Solid, param_5, param_6, param_7, param_8);
							l85.Clear();
						}
					}
				}
				else
				{
					lightingEffect.LightSources = param_2;
					L6(param_0, item.Objects, effectTypeCaster, param_3, param_4, param_5: true, param_6: false, w8a.Solid, param_5, param_6, param_7, param_8);
				}
			}
			else
			{
				L6(param_0, item.Objects, effectTypeCaster, param_3, param_4, param_5: true, param_6: false, w8a.Solid, param_5, param_6, param_7, param_8);
			}
		}
	}

	private void L6(GraphicsDevice param_0, List<RenderableMesh> param_1, EffectTypeCaster param_2, ILightMapManager param_3, bool param_4, bool param_5, bool param_6, w8a param_7, FillMode param_8, bool param_9, bool param_10, bool param_11)
	{
		if (param_1.Count < 1)
		{
			return;
		}
		LightingSystemPerformance.Begin("RenderManager.RenderObjectBatch");
		if (param_2.DeferredObjectEffect != null)
		{
			string text = string.Empty;
			if (param_1[0].l8_u0013_ != null)
			{
				text = param_1[0].l8_u0013_.Name;
			}
			throw new Exception("Forward rendering does not support deferred effects (SceneObject '" + text + "'). Make sure model processors are set to a non-deferred processor.");
		}
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
			param_6 = !param_6;
		}
		bool flag3 = param_7 == w8a.TransparentDoubleSided;
		if (flag3)
		{
			_u0004_.a.Lx(param_0, param_8, cullMode, !param_6, param_4: false, param_5: false);
		}
		else
		{
			if (param_2.RenderableEffect != null && param_7 == w8a.Solid)
			{
				flag2 = param_2.RenderableEffect.DoubleSided;
			}
			_u0004_.a.Lx(param_0, param_8, cullMode, param_6, flag2, param_5: false);
		}
		SamplerState samplerState = null;
		if (param_5 && !param_10)
		{
			if (param_2.AddressableEffect != null)
			{
				IAddressableEffect addressableEffect = param_2.AddressableEffect;
				samplerState = l8b.Lq(param_0, addressableEffect.AddressModeU, addressableEffect.AddressModeV, addressableEffect.AddressModeW, Filter, MaxAnisotropy);
			}
			else
			{
				samplerState = l8b.Lq(param_0, TextureAddressMode.Wrap, TextureAddressMode.Wrap, TextureAddressMode.Wrap, Filter, MaxAnisotropy);
			}
		}
		else
		{
			l8b.L_u000F_(param_0);
		}
		EffectPassCollection passes = param_2.Effect.CurrentTechnique.Passes;
		base.l8T.l8l.AccumulationValue++;
		base.l8T.l8L.AccumulationValue += passes.Count;
		IEffectMatrices effectMatrices = param_2.EffectMatrices;
		IEffectLights effectLights = param_2.EffectLights;
		ISkinnedEffect skinnedEffect = param_2.SkinnedEffect;
		IRenderableEffect renderableEffect = param_2.RenderableEffect;
		BaseRenderableEffect baseRenderableEffect = param_2.BaseRenderableEffect;
		IStaticLightingEffect staticLightingEffect = param_2.StaticLightingEffect;
		if (param_2.BaseSasEffect != null || param_11)
		{
			l8b.L_u0012_();
		}
		bool flag4 = true;
		bool flag5 = staticLightingEffect != null || effectLights != null;
		Dictionary<object, CompositeLighting> dictionary = ((param_7 == w8a.Solid) ? l8Y : l8_u0005_);
		for (int i = 0; i < passes.Count; i++)
		{
			EffectPass effectPass = passes[i];
			foreach (RenderableMesh item2 in param_1)
			{
				if (item2 == null || (param_4 && !item2.l85) || !item2.l8_u0013_.Valid)
				{
					continue;
				}
				bool flag6 = false;
				if (flag5)
				{
					CompositeLighting value;
					if (staticLightingEffect != null)
					{
						StaticLightingType staticLightingType = item2.l8_u0013_.StaticLightingType;
						if (staticLightingType == StaticLightingType.Composite || staticLightingType == StaticLightingType.Custom || param_7 != w8a.Solid)
						{
							if (!dictionary.TryGetValue(item2.l8_u0013_, out value))
							{
								value = l8_u0002_;
							}
							if (staticLightingType == StaticLightingType.BakedDown && param_3 != null)
							{
								staticLightingEffect.SetStaticLighting(StaticLightingEffectMode.BakedDownAndComposite, param_3.GetLightMap(item2), ref value);
							}
							else
							{
								staticLightingEffect.SetStaticLighting(StaticLightingEffectMode.Composite, null, ref value);
							}
						}
						else if (staticLightingType == StaticLightingType.BakedDown && param_3 != null)
						{
							staticLightingEffect.SetStaticLighting(StaticLightingEffectMode.BakedDown, param_3.GetLightMap(item2));
						}
						else
						{
							staticLightingEffect.SetStaticLighting(StaticLightingEffectMode.Ambient, null);
						}
					}
					else if (effectLights != null)
					{
						if (!dictionary.TryGetValue(item2.l8_u0013_, out value))
						{
							value = l8_u0002_;
						}
						effectLights.AmbientLightColor = value.AmbientColor;
						effectLights.DirectionalLight0.DiffuseColor = value.DiffuseColor;
						effectLights.DirectionalLight0.Direction = value.Direction;
					}
					flag6 = true;
				}
				if (effectMatrices != null)
				{
					effectMatrices.World = item2.l8v;
					flag6 = true;
				}
				else
				{
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
				}
				if (flag6 || flag4)
				{
					effectPass.Apply();
					base.l8T.l8J.AccumulationValue++;
				}
				if (!flag2 && cullMode != item2.l8I)
				{
					cullMode = item2.l8I;
					if (flag3)
					{
						_u0004_.a.Lx(param_0, param_8, cullMode, !param_6, param_4: false, param_5: false);
					}
					else
					{
						_u0004_.a.Lx(param_0, param_8, cullMode, param_6, flag2, param_9);
					}
					base.l8T.l8w.AccumulationValue++;
				}
				l8I._u0004_(param_0, item2);
				try
				{
					if (flag4 && samplerState != null)
					{
						l8b.LF(param_0, samplerState, _AvoidXNAGarbageCollectionMemoryLeak);
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
						_u0004_.a.Lx(param_0, param_8, cullMode, param_6, param_4: false, param_5: false);
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
		if (param_2.BaseSasEffect != null || param_11)
		{
			l8b.L_u0012_();
		}
	}
}
