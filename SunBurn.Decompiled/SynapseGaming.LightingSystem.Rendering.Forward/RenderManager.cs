using System;
using System.Collections.Generic;
using L;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using R;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Editor;
using SynapseGaming.LightingSystem.Effects;
using SynapseGaming.LightingSystem.Effects.Forward;
using SynapseGaming.LightingSystem.Lights;
using SynapseGaming.LightingSystem.Shadows;
using SynapseGaming.LightingSystem.Shadows.Deferred;
using c;
using h;
using v;

namespace SynapseGaming.LightingSystem.Rendering.Forward;

/// <summary>
/// Provides a complete forward renderer.
/// </summary>
public class RenderManager : BaseRenderManager
{
	private bool fNB;

	private bool fNa = true;

	private bool fNg;

	private int fNf = 1;

	private FogEffect fN_0003;

	private List<SceneEntity> fNn = new List<SceneEntity>();

	private List<RenderableMesh> fN_0019 = new List<RenderableMesh>();

	private List<h._0019> fNL = new List<h._0019>();

	private List<h._0019> fNv = new List<h._0019>();

	private List<h._0019> fN_0012 = new List<h._0019>();

	private CompositeLighting fN_0015 = default;

	private Dictionary<ISceneObject, CompositeLighting> fN8 = new Dictionary<ISceneObject, CompositeLighting>(32);

	private h._0012 fNi = new h._0012();

	private h._0015 fN_0005 = new h._0015();

	private List<BaseLight> fNj = new List<BaseLight>();

	private List<RenderableMesh> fNo = new List<RenderableMesh>();

	private List<h._0019> fNc = new List<h._0019>();

	private new List<h._0019> fNd = new List<h._0019>();

	private List<h._0019> fNw = new List<h._0019>();

	private static h.n fNs = new h.n();

	private static h._0003 fN_0011 = new h._0003();

	private static h.v fNQ = new h.v();

	/// <summary>
	/// Cleans up shimmering effects on object edges. Requires a
	/// depth buffer format that supports stencil tests. Improper
	/// depth buffer formats will disable the feature.
	/// </summary>
	public bool MultiPassEdgeCleanupEnabled
	{
		get
		{
			return fNa;
		}
		set
		{
			fNa = value;
		}
	}

	/// <summary>
	/// Creates a new RenderManager instance.
	/// </summary>
	/// <param name="graphicsdevicemanager"></param>
	/// <param name="sceneinterface">Service provider used to access all other manager services in this scene.</param>
	public RenderManager(IGraphicsDeviceService graphicsdevicemanager, IManagerServiceProvider sceneinterface)
		: base(graphicsdevicemanager, sceneinterface)
	{
	}

	/// <summary>
	/// Builds all object batches, shadow maps, and cached information before rendering.
	/// Any object added to the RenderManager after this call will not be visible during the frame.
	/// </summary>
	/// <param name="scenestate"></param>
	public override void BeginFrameRendering(ISceneState scenestate)
	{
		LightingSystemPerformance.Begin("RenderManager.BeginFrameRendering");
		fNi._00034();
		fN_0005._00034();
		base.BeginFrameRendering(scenestate);
		Vector3 value = scenestate.ViewToWorld.Translation;
		GraphicsDevice graphicsDevice = GraphicsDeviceManager.GraphicsDevice;
		RasterizerState rasterizerState = graphicsDevice.RasterizerState;
		if (rasterizerState == null)
		{
			rasterizerState = RasterizerState.CullCounterClockwise;
		}
		fNg = false;
		if (fNa)
		{
			RenderTargetBinding[] renderTargets = graphicsDevice.GetRenderTargets();
			if (renderTargets.Length > 0)
			{
				if (renderTargets[0].RenderTarget is RenderTarget2D renderTarget2D)
				{
					fNg = renderTarget2D.DepthStencilFormat == DepthFormat.Depth24Stencil8;
				}
			}
			else
			{
				fNg = graphicsDevice.PresentationParameters.DepthStencilFormat == DepthFormat.Depth24Stencil8;
			}
		}
		if (fN_0003 == null)
		{
			fN_0003 = new FogEffect(graphicsDevice);
		}
		LightingSystemPerformance.Begin("RenderManager.BeginFrameRendering (find objects)");
		fNn.Clear();
		fN_0019.Clear();
		fN8.Clear();
		IObjectManager objectManager = (IObjectManager)ServiceProvider.GetManager(SceneInterface.ObjectManagerType, required: false);
		ILightManager lightManager = (ILightManager)ServiceProvider.GetManager(SceneInterface.LightManagerType, required: false);
		objectManager?.Find(fNn, SceneState.ViewFrustum, ObjectFilter.DynamicAndStatic);
		LightingSystemPerformance.Begin("RenderManager.BeginFrameRendering (filter objects)");
		bool flag = lightManager != null;
		for (int k = 0; k < fNn.Count; k++)
		{
			SceneEntity sceneEntity = fNn[k];
			if (sceneEntity == null)
			{
				continue;
			}
			L.w w2 = OptimizationSystem.fNf.g_0011(sceneEntity);
			ISceneObject sceneObject = w2.fNB;
			if (sceneObject == null || (!sceneObject.Visible && (!sceneObject.VisibleInEditor || !fNB)))
			{
				continue;
			}
			BoundingSphere worldBoundingSphere = sceneObject.WorldBoundingSphere;
			float num = SceneState.Environment.VisibleDistance + worldBoundingSphere.Radius;
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
			base.fNd.fNa.AccumulationValue++;
			if (flag && sceneObject.StaticLightingType == StaticLightingType.Composite && !fN8.ContainsKey(sceneObject))
			{
				BoundingBox worldbounds = sceneObject.WorldBoundingBox;
				CompositeLighting compositelighting = default;
				lightManager.GetCompositeLighting(ref worldbounds, 0.25f, LightingType.BakedDown, out compositelighting);
				fN8.Add(sceneObject, compositelighting);
			}
			for (int l = 0; l < sceneObject.RenderableMeshes.Count; l++)
			{
				RenderableMesh renderableMesh = sceneObject.RenderableMeshes[l];
				if (renderableMesh.fN_0015 != null)
				{
					ushort num4 = (ushort)(renderableMesh.fNm >> 16);
					ushort num5 = (ushort)renderableMesh.fNm;
					int num6 = (num5 ^ 1) + (num4 ^ 2) << 16;
					renderableMesh.fNh = num3 | num6;
					if (renderableMesh.fN_0015 is BaseSasBindEffect baseSasBindEffect)
					{
						baseSasBindEffect.GameTime = scenestate.GameTime;
					}
					fN_0019.Add(renderableMesh);
				}
			}
		}
		LightingSystemPerformance.Begin("RenderManager.BeginFrameRendering (sort and batch)");
		fN_0019.Sort(fNs);
		fNQ._0003E(SceneState.View, SceneState.ViewToWorld, SceneState.Projection, SceneState.ProjectionToView, fNL, fN_0019, h.g.LightingEffect | h.g.BasicEffect_Lighting | h.g.BasicEffect_NonLighting | h.g.MiscEffect);
		fNQ._0003E(SceneState.View, SceneState.ViewToWorld, SceneState.Projection, SceneState.ProjectionToView, fNv, fN_0019, h.g.LightingEffect);
		fNQ._0003E(SceneState.View, SceneState.ViewToWorld, SceneState.Projection, SceneState.ProjectionToView, fN_0012, fN_0019, h.g.LightingEffect | h.g.BasicEffect_Lighting);
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
			if (fNa && fNg)
			{
				clearOptions |= ClearOptions.Stencil;
			}
			graphicsDevice.Clear(clearOptions, color, 1f, 0);
		}
		graphicsDevice.RasterizerState = rasterizerState;
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
				if (!(shadowGroup.Shadow is IShadowMap shadowMap))
				{
					continue;
				}
				base.fNd.fN8.AccumulationValue++;
				fNo.Clear();
				objectManager.Find(objectfilter: (shadowGroup.ShadowSource.ShadowType != ShadowType.AllObjects) ? ObjectFilter.Static : ObjectFilter.DynamicAndStatic, foundobjects: fNo, worldbounds: shadowGroup.BoundingBox);
				fNo.Sort(fN_0011);
				fNQ._0003Y(fNc, fNo, false, fNB);
				avatarManager?.BeginShadowGroupRendering(shadowGroup);
				_ = shadowGroup.ShadowSource.ShadowPosition;
				for (int k = 0; k < shadowMap.Surfaces.Length; k++)
				{
					ShadowMapSurface shadowMapSurface = shadowMap.Surfaces[k];
					base.fNd.fNi.AccumulationValue++;
					if (!shadowMap.IsSurfaceVisible(k, SceneState.ViewFrustum))
					{
						continue;
					}
					shadowMap.BeginSurfaceRendering(k);
					LightingSystemPerformance.Begin("RenderManager.BuildShadowMaps (object filter loop)");
					h.L._00035(fNo, shadowMapSurface.Frustum);
					foreach (h._0019 item in fNc)
					{
						if (item.fNB)
						{
							L.d d2 = OptimizationSystem.fNg.g_0011(item.fNn);
							L.d d3 = OptimizationSystem.fNg.g_0011(shadowMap.ShadowEffect);
							EffectHelper.f1(d2, d3);
							if (d3.fNg != null)
							{
								d3.fNg.Skinned = item.fNa;
							}
							_00033(item.Objects, d3, null, true, true, true, FillMode.Solid, false, false, false, d2.fNn != null);
						}
					}
					if (avatarManager != null && avatarManager.RenderToShadowMapSurface(shadowGroup, shadowMapSurface, shadowMap.ShadowEffect))
					{
						fN_0005._00034();
						fNi._00034();
					}
					shadowMap.EndSurfaceRendering();
					base.fNd.fN_0005.AccumulationValue++;
				}
				avatarManager?.EndShadowGroupRendering(shadowGroup);
				shadowMap.ContentsAreValid = true;
			}
			shadowrendertargetgroup.End();
		}
	}

	/// <summary>
	/// Renders the scene.
	/// </summary>
	public override void Render()
	{
		if (SceneState == null)
		{
			return;
		}
		LightingSystemPerformance.Begin("RenderManager.Render");
		GraphicsDevice graphicsDevice = GraphicsDeviceManager.GraphicsDevice;
		ILightMapManager lightMapManager = (ILightMapManager)ServiceProvider.GetManager(SceneInterface.LightMapManagerType, required: false);
		fNi._00034();
		fN_0005._00034();
		graphicsDevice.BlendState = BlendState.Opaque;
		graphicsDevice.DepthStencilState = DepthStencilState.Default;
		RasterizerState rasterizerState = graphicsDevice.RasterizerState;
		if (rasterizerState == null)
		{
			rasterizerState = RasterizerState.CullCounterClockwise;
		}
		ISceneState sceneState = SceneState;
		foreach (h._0019 item in fNL)
		{
			L.d d2 = OptimizationSystem.fNg.g_0011(item.fNn);
			if (d2.fNB != null)
			{
				d2.fNB.SetViewAndProjection(sceneState.View, sceneState.ViewToWorld, sceneState.Projection, sceneState.ProjectionToView);
			}
			else if (d2.fNo != null)
			{
				d2.fNo.View = sceneState.View;
				d2.fNo.Projection = sceneState.Projection;
			}
		}
		LightingSystemPerformance.Begin("RenderManager.Render (ambient)");
		_0003_0004(fNL, FrameAmbientLights, lightMapManager, false, RenderFillMode, false, true, false);
		graphicsDevice.DepthStencilState = R.B.fNn;
		LightingSystemPerformance.Begin("RenderManager.Render (lighting loop)");
		List<ShadowRenderTargetGroup> frameShadowRenderTargetGroups = FrameShadowRenderTargetGroups;
		foreach (ShadowRenderTargetGroup item2 in frameShadowRenderTargetGroups)
		{
			foreach (ShadowGroup shadowGroup in item2.ShadowGroups)
			{
				_0003Z(item2, shadowGroup);
			}
		}
		if (SceneState.Environment.FogEnabled)
		{
			LightingSystemPerformance.Begin("RenderManager.Render (fog)");
			L.d d3 = OptimizationSystem.fNg.g_0011(fN_0003);
			fN_0003.SetViewAndProjection(SceneState.View, SceneState.ViewToWorld, SceneState.Projection, SceneState.ProjectionToView);
			graphicsDevice.BlendState = BlendState.NonPremultiplied;
			graphicsDevice.DepthStencilState = R.B.fNn;
			fN_0003.StartDistance = SceneState.Environment.FogStartDistance;
			fN_0003.EndDistance = SceneState.Environment.FogEndDistance;
			fN_0003.Color = SceneState.Environment.FogColor;
			fNQ._0003Y(fNw, fN_0019, false, fNB);
			foreach (h._0019 item3 in fNw)
			{
				L.d d4 = OptimizationSystem.fNg.g_0011(item3.fNn);
				EffectHelper.f1(d4, d3);
				fN_0003.Skinned = item3.fNa;
				_00033(item3.Objects, d3, null, false, true, false, RenderFillMode, false, false, false, d4.fNn != null);
			}
		}
		graphicsDevice.BlendState = BlendState.Opaque;
		graphicsDevice.DepthStencilState = DepthStencilState.Default;
		graphicsDevice.RasterizerState = rasterizerState;
	}

	/// <summary>
	/// Finalizes rendering and cleans up frame information including removing all frame lifespan objects.
	/// </summary>
	public override void EndFrameRendering()
	{
		LightingSystemPerformance.Begin("RenderManager.EndFrameRendering");
		ISceneState sceneState = SceneState;
		base.EndFrameRendering();
		foreach (SceneEntity item in fNn)
		{
			item.RenderCustomPass(sceneState);
		}
		((ILightManager)ServiceProvider.GetManager(SceneInterface.LightManagerType, required: false))?.RenderVolumeLights();
		fNQ._0013();
	}

	/// <summary>
	/// Unloads all scene and device specific data.  Must be called
	/// when the device is reset (during Game.UnloadGraphicsContent()).
	/// </summary>
	public override void Unload()
	{
		if (fN_0003 != null)
		{
			fN_0003.Dispose();
			fN_0003 = null;
		}
		base.Unload();
	}

	private void _0003Z(ShadowRenderTargetGroup P_0, ShadowGroup P_1)
	{
		if (P_1.Lights.Count < 1 || fN_0019.Count < 1)
		{
			return;
		}
		base.fNd.fN_0012.AccumulationValue++;
		base.fNd.fNL.AccumulationValue += P_1.Lights.Count;
		bool flag = P_1.ShadowSourceTypes.fNB != null;
		GraphicsDevice graphicsDevice = GraphicsDeviceManager.GraphicsDevice;
		List<h._0019> list = (flag ? fNv : fN_0012);
		LightingSystemPerformance.Begin("RenderManager.RenderShadowGroup (object filter loop)");
		foreach (h._0019 item in list)
		{
			if (flag)
			{
				item.fNB = h.L._0003e(item.Objects, P_1.BoundingBox);
				continue;
			}
			item.fNB = true;
			foreach (RenderableMesh item2 in item.Objects)
			{
				item2.fNG = true;
			}
		}
		if (flag)
		{
			Rectangle scissorRectangle = L._0019.a_0004(P_1.BoundingBox, graphicsDevice.Viewport, SceneState.ViewProjection, SceneState.ViewToWorld);
			if ((float)scissorRectangle.Width <= 0f || (float)scissorRectangle.Height <= 0f)
			{
				return;
			}
			graphicsDevice.ScissorRectangle = scissorRectangle;
		}
		if (ShadowDetail != DetailPreference.Off)
		{
			if (P_1.Shadow is IShadowMap shadowMap && shadowMap.ShadowEffect is IRenderableEffect)
			{
				fNd.Clear();
				fNQ._0003Y(fNd, fN_0019, true, fNB);
				graphicsDevice.BlendState = R.B.fNg;
				graphicsDevice.DepthStencilState = R.B.fNn;
				foreach (h._0019 item3 in fNd)
				{
					if (item3.fNB)
					{
						L.d d2 = OptimizationSystem.fNg.g_0011(item3.fNn);
						L.d d3 = OptimizationSystem.fNg.g_0011(shadowMap.ShadowEffect);
						EffectHelper.f1(d2, d3);
						_0003D(P_0, P_1, shadowMap, d3, item3, flag, d2.fNn != null);
					}
				}
				graphicsDevice.BlendState = R.B.fNf;
			}
			else
			{
				graphicsDevice.BlendState = R.B.fNB;
			}
		}
		else
		{
			graphicsDevice.BlendState = R.B.fNB;
		}
		if (P_1.Lights.Count == 1 && fNa && fNg)
		{
			graphicsDevice.DepthStencilState = R.B.fN_0012;
			graphicsDevice.ReferenceStencil = fNf;
			fNf++;
			if (fNf > 250)
			{
				fNf = 1;
			}
		}
		else
		{
			graphicsDevice.DepthStencilState = R.B.fNn;
		}
		_0003_0004(list, P_1.Lights, null, true, RenderFillMode, flag, false, false);
	}

	private void _0003D(ShadowRenderTargetGroup P_0, ShadowGroup P_1, IShadowMap P_2, L.d P_3, h._0019 P_4, bool P_5, bool P_6)
	{
		if (P_4.Objects.Count >= 1 && P_1.Lights.Count >= 1 && P_2 != null)
		{
			_ = GraphicsDeviceManager.GraphicsDevice;
			ISkinnedEffect skinnedEffect = P_3.fNg;
			if (P_3.fNi is c.B b)
			{
				b.EffectDetail = ShadowDetail;
			}
			P_2.BeginRendering(P_0.RenderTarget);
			if (skinnedEffect != null)
			{
				skinnedEffect.Skinned = P_4.fNa;
			}
			_00033(P_4.Objects, P_3, null, false, true, false, RenderFillMode, P_5, false, true, P_6);
			P_2.EndRendering();
		}
	}

	private void _0003_0004(List<h._0019> P_0, List<BaseLight> P_1, ILightMapManager P_2, bool P_3, FillMode P_4, bool P_5, bool P_6, bool P_7)
	{
		LightingSystemPerformance.Begin("RenderManager.RenderObjectBatches");
		foreach (h._0019 item in P_0)
		{
			if (P_3 && !item.fNB)
			{
				continue;
			}
			L.d d2 = OptimizationSystem.fNg.g_0011(item.fNn);
			bool flag = d2.fNn != null;
			if (d2.fNB != null)
			{
				d2.fNB.EffectDetail = EffectDetail;
			}
			else
			{
				IEffectLights effectLights = d2.fNj;
				if (effectLights != null && effectLights.LightingEnabled && P_1.Count > 0)
				{
					BaseLight baseLight = P_1[0];
					L.o o2 = OptimizationSystem.fNB.g_0011(baseLight);
					if (o2.fNg != null && o2.fNa == null)
					{
						effectLights.AmbientLightColor = default;
						effectLights.DirectionalLight0.Enabled = true;
						effectLights.DirectionalLight0.DiffuseColor = baseLight.CompositeColorAndIntensity;
						effectLights.DirectionalLight0.Direction = o2.fNg.Direction;
						effectLights.DirectionalLight1.Enabled = false;
						effectLights.DirectionalLight2.Enabled = false;
					}
					else
					{
						if (o2.fNf == null)
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
			_ = item.fNn;
			ILightingEffect lightingEffect = d2.fNa;
			if (P_1.Count > 0 && lightingEffect != null)
			{
				int maxLightSources = lightingEffect.MaxLightSources;
				if (P_1.Count > maxLightSources)
				{
					fNj.Clear();
					for (int k = 0; k < P_1.Count; k++)
					{
						fNj.Add(P_1[k]);
						if (fNj.Count >= maxLightSources || k + 1 >= P_1.Count)
						{
							lightingEffect.LightSources = fNj;
							_00033(item.Objects, d2, P_2, P_3, true, false, P_4, P_5, P_6, P_7, flag);
							fNj.Clear();
						}
					}
				}
				else
				{
					lightingEffect.LightSources = P_1;
					_00033(item.Objects, d2, P_2, P_3, true, false, P_4, P_5, P_6, P_7, flag);
				}
			}
			else
			{
				_00033(item.Objects, d2, P_2, P_3, true, false, P_4, P_5, P_6, P_7, flag);
			}
		}
	}

	private void _00033(List<RenderableMesh> P_0, L.d P_1, ILightMapManager P_2, bool P_3, bool P_4, bool P_5, FillMode P_6, bool P_7, bool P_8, bool P_9, bool P_10)
	{
		if (P_0.Count < 1)
		{
			return;
		}
		LightingSystemPerformance.Begin("RenderManager.RenderObjectBatch");
		if (P_1.fNL != null)
		{
			string text = string.Empty;
			if (P_0[0].fNa != null)
			{
				text = P_0[0].fNa.Name;
			}
			throw new Exception("Forward rendering does not support deferred effects (SceneObject '" + text + "'). Make sure model processors are set to a non-deferred processor.");
		}
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
			P_5 = !P_5;
		}
		R.B._0003l(graphicsDevice, P_6, cullMode, P_5, flag2, P_7);
		SamplerState samplerState = null;
		if (P_4 && !P_9)
		{
			if (P_1.fN_0019 != null)
			{
				IAddressableEffect addressableEffect = P_1.fN_0019;
				samplerState = fNi._0003_0010(graphicsDevice, addressableEffect.AddressModeU, addressableEffect.AddressModeV, addressableEffect.AddressModeW, Filter, MaxAnisotropy);
			}
			else
			{
				samplerState = fNi._0003_0010(graphicsDevice, TextureAddressMode.Wrap, TextureAddressMode.Wrap, TextureAddressMode.Wrap, Filter, MaxAnisotropy);
			}
		}
		else
		{
			fNi._0003z(graphicsDevice, SamplerState.PointClamp);
		}
		EffectPassCollection passes = P_1.fNi.CurrentTechnique.Passes;
		base.fNd.fNf.AccumulationValue++;
		base.fNd.fN_0003.AccumulationValue += passes.Count;
		IEffectMatrices effectMatrices = P_1.fNo;
		IEffectLights effectLights = P_1.fNj;
		ISkinnedEffect skinnedEffect = P_1.fNg;
		IRenderableEffect renderableEffect = P_1.fNB;
		BaseRenderableEffect baseRenderableEffect = P_1.fN_0012;
		IStaticLightingEffect staticLightingEffect = P_1.fNf;
		if (P_1.fN8 != null || P_10)
		{
			fNi._00034();
		}
		bool flag3 = true;
		for (int k = 0; k < passes.Count; k++)
		{
			EffectPass effectPass = passes[k];
			foreach (RenderableMesh item2 in P_0)
			{
				if (item2 == null || (P_3 && !item2.fNG) || !item2.fNa.Valid)
				{
					continue;
				}
				bool flag4 = false;
				if (P_8)
				{
					StaticLightingType staticLightingType = item2.fNa.StaticLightingType;
					if (staticLightingType != StaticLightingType.Composite || !fN8.TryGetValue(item2.fNa, out var value))
					{
						value = fN_0015;
					}
					if (staticLightingEffect != null && P_2 != null)
					{
						if (staticLightingType == StaticLightingType.BakedDown)
						{
							staticLightingEffect.SetStaticLighting(P_2.GetLightMap(item2), ref value);
						}
						else
						{
							staticLightingEffect.SetStaticLighting(null, ref value);
						}
					}
					else if (effectLights != null)
					{
						effectLights.AmbientLightColor = value.AmbientColor;
						effectLights.DirectionalLight0.DiffuseColor = value.DiffuseColor;
						effectLights.DirectionalLight0.Direction = value.Direction;
					}
					flag4 = true;
				}
				if (effectMatrices != null)
				{
					effectMatrices.World = item2.fN8;
					flag4 = true;
				}
				else
				{
					if (skinnedEffect != null)
					{
						skinnedEffect.SkinBones = item2.fNa.SkinBones;
						flag4 = true;
					}
					if (renderableEffect != null)
					{
						renderableEffect.SetWorldAndWorldToObject(ref item2.fN8, ref item2.fNi);
						flag4 = true;
					}
					if (baseRenderableEffect != null)
					{
						flag4 = baseRenderableEffect.UpdatedByBatch;
						baseRenderableEffect.UpdatedByBatch = false;
					}
				}
				if (flag4 || flag3)
				{
					effectPass.Apply();
					base.fNd.fN_0019.AccumulationValue++;
				}
				if (!flag2 && cullMode != item2.fNQ)
				{
					cullMode = item2.fNQ;
					R.B._0003l(graphicsDevice, P_6, cullMode, P_5, flag2, P_7);
					base.fNd.fNn.AccumulationValue++;
				}
				fN_0005.h(graphicsDevice, item2);
				try
				{
					if (flag3 && samplerState != null)
					{
						fNi._0003z(graphicsDevice, samplerState);
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
		if (P_1.fN8 != null || P_10)
		{
			fNi._00034();
		}
	}
}
