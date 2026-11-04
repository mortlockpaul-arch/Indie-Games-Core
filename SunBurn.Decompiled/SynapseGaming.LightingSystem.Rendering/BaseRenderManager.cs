using System;
using System.Collections.Generic;
using System.Diagnostics;
using _9;
using L;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Editor;
using SynapseGaming.LightingSystem.Lights;
using SynapseGaming.LightingSystem.Shadows;

namespace SynapseGaming.LightingSystem.Rendering;

/// <summary>
/// Base class that provides basic render management.  Used by the forward rendering
/// RenderManager and deferred rendering DeferredRenderManager classes.
/// </summary>
public abstract class BaseRenderManager : IRenderManager, IRenderableManager, IManagerService, IManager, IUnloadable
{
	internal class nNB
	{
		internal SystemStatistic fNB = SystemConsole.GetStatistic("Renderer_PolysRendered", SystemStatisticCategory.Rendering);

		internal SystemStatistic fNa = SystemConsole.GetStatistic("Renderer_SceneObjectsRendered", SystemStatisticCategory.Rendering);

		internal SystemStatistic fNg = SystemConsole.GetStatistic("Renderer_MeshesRendered", SystemStatisticCategory.Rendering);

		internal SystemStatistic fNf = SystemConsole.GetStatistic("Renderer_Batches", SystemStatisticCategory.Rendering);

		internal SystemStatistic fN_0003 = SystemConsole.GetStatistic("Renderer_BatchPasses", SystemStatisticCategory.Rendering);

		internal SystemStatistic fNn = SystemConsole.GetStatistic("Renderer_BatchCullModeChanges", SystemStatisticCategory.Rendering);

		internal SystemStatistic fN_0019 = SystemConsole.GetStatistic("Renderer_BatchEffectCommitChanges", SystemStatisticCategory.Rendering);

		internal SystemStatistic fNL = SystemConsole.GetStatistic("Light_LightsRendered", SystemStatisticCategory.Lighting);

		internal SystemStatistic fNv = SystemConsole.GetStatistic("Light_LightsRenderedAsGroup", SystemStatisticCategory.Lighting);

		internal SystemStatistic fN_0012 = SystemConsole.GetStatistic("Shadow_ShadowGroupsRendered", SystemStatisticCategory.Shadowing);

		internal SystemStatistic fN_0015 = SystemConsole.GetStatistic("Shadow_ShadowMapPagesProcessed", SystemStatisticCategory.Shadowing);

		internal SystemStatistic fN8 = SystemConsole.GetStatistic("Shadow_ShadowMapsProcessed", SystemStatisticCategory.Shadowing);

		internal SystemStatistic fNi = SystemConsole.GetStatistic("Shadow_ShadowMapFacesProcessed", SystemStatisticCategory.Shadowing);

		internal SystemStatistic fN_0005 = SystemConsole.GetStatistic("Shadow_ShadowMapFacesFilled", SystemStatisticCategory.Shadowing);
	}

	/// <summary />
	protected const float _CompositeLightingBlendAmount = 0.25f;

	private int fNB = 60;

	private DetailPreference fNa = DetailPreference.Medium;

	private DetailPreference fNg;

	private IGraphicsDeviceService fNf;

	private bool fN_0003;

	private bool fNn = true;

	private FillMode fN_0019;

	private ISceneState fNL;

	private int fNv = 4;

	private TextureFilter fN_0012;

	private IManagerServiceProvider fN_0015;

	private ShadowRenderTargetGroup fN8 = new ShadowRenderTargetGroup();

	private _9.B fNi;

	private List<ShadowRenderTargetGroup> fN_0005 = new List<ShadowRenderTargetGroup>();

	private List<BaseLight> fNj = new List<BaseLight>();

	private List<BaseLight> fNo = new List<BaseLight>();

	private AmbientLight fNc = new AmbientLight();

	internal nNB fNd = new nNB();

	/// <summary>
	/// Gets the manager specific Type used as a unique key for storing and
	/// requesting the manager from the IManagerServiceProvider.
	/// </summary>
	public Type ManagerType => SceneInterface.RenderManagerType;

	/// <summary>
	/// Sets the order this manager is processed relative to other managers
	/// in the IManagerServiceProvider. Managers with lower processing order
	/// values are processed first.
	///
	/// In the case of BeginFrameRendering and EndFrameRendering, BeginFrameRendering
	/// is processed in the normal order (lowest order value to highest), however
	/// EndFrameRendering is processed in reverse order (highest to lowest) to ensure
	/// the first manager begun is the last one ended (FILO).
	/// </summary>
	public int ManagerProcessOrder
	{
		get
		{
			return fNB;
		}
		set
		{
			fNB = value;
		}
	}

	/// <summary>
	/// The current GraphicsDeviceManager used by this object.
	/// </summary>
	public IGraphicsDeviceService GraphicsDeviceManager => fNf;

	/// <summary>
	/// Determines if XNA rendering exceptions are quietly caught
	/// or allowed to bubble-up to the debugger.
	///
	/// Note: exceptions are always hidden while the editor is
	/// open and when the game is not being debugged.
	/// </summary>
	public bool HideXNARuntimeExceptions
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
	/// Enables clearing the back buffer during rendering.
	/// Disabling allows custom rendering (such as skybox)
	/// prior to calling RenderManager.Render().
	/// </summary>
	public bool ClearBackBufferEnabled
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
	/// Changes the render fill mode allowing solid and wireframe rendering.
	/// </summary>
	public FillMode RenderFillMode
	{
		get
		{
			return fN_0019;
		}
		set
		{
			fN_0019 = value;
		}
	}

	/// <summary>
	/// Current composite ambient light (combination of all scene ambient lights)
	/// provided by the LightManager (only valid between calls to
	/// BeginFrameRendering and EndFrameRendering).
	/// </summary>
	public AmbientLight FrameAmbientLight => fNc;

	/// <summary>
	/// Current scene lights provided by the LightManager (only valid between
	/// calls to BeginFrameRendering and EndFrameRendering).
	/// </summary>
	public List<BaseLight> FrameLights => fNj;

	/// <summary>
	/// Current scene shadow maps provided by the ShadowManager and
	/// filled by this render manager (only valid between calls to
	/// BeginFrameRendering and EndFrameRendering).
	///
	/// See the Custom Renderer project template for an example of
	/// how to use ShadowRenderTargetGroup and the contained
	/// shadow maps to render shadows onto the scene.
	/// </summary>
	public List<ShadowRenderTargetGroup> FrameShadowRenderTargetGroups => fN_0005;

	/// <summary>
	/// Determines the current rendering quality based on the user preferences provided to ApplyPreferences.
	/// </summary>
	protected int MaxAnisotropy => fNv;

	/// <summary>
	/// Current scene state information provided to BeginFrameRendering (only valid between calls to BeginFrameRendering and EndFrameRendering).
	/// </summary>
	protected ISceneState SceneState => fNL;

	/// <summary>
	/// Determines the current rendering quality based on the user preferences provided to ApplyPreferences.
	/// </summary>
	protected DetailPreference ShadowDetail => fNa;

	/// <summary>
	/// Determines the current rendering quality based on the user preferences provided to ApplyPreferences.
	/// </summary>
	protected DetailPreference EffectDetail => fNg;

	/// <summary>
	/// Determines the current rendering quality based on the user preferences provided to ApplyPreferences.
	/// </summary>
	protected TextureFilter Filter => fN_0012;

	/// <summary>
	/// Current ambient lights provided by the LightManager (only valid between calls to BeginFrameRendering and EndFrameRendering).
	/// </summary>
	protected List<BaseLight> FrameAmbientLights => fNo;

	/// <summary>
	/// Service provider used to access all other manager services in this scene. Allows querying
	/// objects through the IObjectManager manager interface, querying lights through the ILightManager manager
	/// interface, and more.
	/// </summary>
	protected IManagerServiceProvider ServiceProvider => fN_0015;

	/// <summary>
	/// Creates a new BaseRenderManager instance.
	/// </summary>
	/// <param name="graphicsdevicemanager"></param>
	/// <param name="sceneinterface">Service provider used to access all other manager services in this scene.</param>
	public BaseRenderManager(IGraphicsDeviceService graphicsdevicemanager, IManagerServiceProvider sceneinterface)
	{
		fNf = graphicsdevicemanager;
		fN_0015 = sceneinterface;
		fNi = new _9.B(graphicsdevicemanager);
	}

	/// <summary>
	/// Use to apply user quality and performance preferences to the resources managed by this object.
	/// </summary>
	/// <param name="preferences"></param>
	public virtual void ApplyPreferences(ILightingSystemPreferences preferences)
	{
		switch (preferences.TextureSampling)
		{
		case SamplingPreference.Point:
			fN_0012 = TextureFilter.PointMipLinear;
			break;
		case SamplingPreference.Bilinear:
			fN_0012 = TextureFilter.LinearMipPoint;
			break;
		case SamplingPreference.Trilinear:
			fN_0012 = TextureFilter.Linear;
			break;
		case SamplingPreference.Anisotropic:
			fN_0012 = TextureFilter.Anisotropic;
			break;
		}
		fNv = preferences.MaxAnisotropy;
		fNa = preferences.ShadowDetail;
		fNg = preferences.EffectDetail;
		fNi.ApplyPreferences(preferences);
	}

	/// <summary />
	/// <param name="e"></param>
	/// <param name="contextmessage"></param>
	protected virtual void OnXNARuntimeException(Exception e, string contextmessage)
	{
		if (fN_0003 || !Debugger.IsAttached || LightingSystemEditor.EditorAttachedStatic)
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

	/// <summary>
	/// Provides a default set of shadow groups when no IShadowMapManager manager service is available.
	/// </summary>
	/// <param name="rendertargetgroups">Returned render target groups.</param>
	/// <param name="lights">Source lights to create groups for.</param>
	protected void GetDefaultShadows(List<ShadowRenderTargetGroup> rendertargetgroups, List<BaseLight> lights)
	{
		fN8.ShadowGroups.Clear();
		fNi.BuildShadowGroups(fN8.ShadowGroups, lights, usedefaultgrouping: true);
		fN8.Build(GraphicsDeviceManager.GraphicsDevice, null);
		rendertargetgroups.Add(fN8);
	}

	/// <summary>
	/// Generates shadow maps for the provided shadow render groups. Override this
	/// method to customize shadow map generation.
	/// </summary>
	/// <param name="shadowrendertargetgroups">Shadow render groups to generate shadow maps for.</param>
	protected abstract void BuildShadowMaps(List<ShadowRenderTargetGroup> shadowrendertargetgroups);

	/// <summary>
	/// Builds all object batches, shadow maps, and cached information before rendering.
	/// Any object added to the RenderManager after this call will not be visible during the frame.
	/// </summary>
	/// <param name="scenestate"></param>
	public virtual void BeginFrameRendering(ISceneState scenestate)
	{
		LightingSystemPerformance.Begin("BaseRenderManager.BeginFrameRendering");
		SplashScreen._0003k();
		fNL = scenestate;
		GraphicsDevice graphicsDevice = GraphicsDeviceManager.GraphicsDevice;
		LightingSystemManager.Instance.aZ(graphicsDevice, scenestate.FrameBuffers);
		GraphicsDeviceSupport graphicsDeviceSupport = LightingSystemManager.Instance.GetGraphicsDeviceSupport(graphicsDevice);
		fNv = Math.Max(Math.Min(fNv, graphicsDeviceSupport.MaxAnisotropy), 1);
		for (int k = 0; k < 8; k++)
		{
			graphicsDevice.SamplerStates[k] = SamplerState.AnisotropicWrap;
		}
		fNi.BeginFrameRendering(scenestate);
		fNj.Clear();
		fNo.Clear();
		ILightManager lightManager = (ILightManager)fN_0015.GetManager(SceneInterface.LightManagerType, required: false);
		if (lightManager == null)
		{
			fNc.DiffuseColor = new Vector3(1f, 0.9f, 0.8f);
			fNc.Intensity = 0.25f;
		}
		else
		{
			lightManager.Find(fNj, fNL.ViewFrustum, ObjectFilter.EnabledDynamicAndStatic);
			fNc.DiffuseColor = Vector3.Zero;
			fNc.Intensity = 1f;
			int num = 0;
			float num2 = 0f;
			Matrix viewToWorld = scenestate.ViewToWorld;
			for (int l = 0; l < fNj.Count; l++)
			{
				BaseLight baseLight = fNj[l];
				if (baseLight == null)
				{
					continue;
				}
				if (baseLight.LightingType != LightingType.RealTime)
				{
					fNj[l] = null;
					continue;
				}
				L.o o2 = OptimizationSystem.fNB.g_0011(baseLight);
				if (o2.fNf != null)
				{
					num2 += o2.fNf.Depth;
					num++;
					fNc.DiffuseColor += baseLight.CompositeColorAndIntensity;
					fNj[l] = null;
				}
				else if (o2.fNB != null)
				{
					IPointSource pointSource = o2.fNB;
					float num3 = Vector3.DistanceSquared(viewToWorld.Translation, pointSource.Position);
					float num4 = SceneState.Environment.VisibleDistance + pointSource.Radius;
					if (num3 > num4 * num4)
					{
						fNj[l] = null;
					}
				}
			}
			if (num > 0)
			{
				fNc.Depth = num2 / (float)num;
			}
			else
			{
				fNc.Depth = 0.1f;
			}
		}
		fNo.Add(fNc);
	}

	/// <summary>
	/// Renders the scene.
	/// </summary>
	public abstract void Render();

	/// <summary>
	/// Finalizes rendering and cleans up frame information including removing all frame lifespan objects.
	/// </summary>
	public virtual void EndFrameRendering()
	{
		fNi.EndFrameRendering();
	}

	/// <summary>
	/// Removes all scene objects and cleans up scene information.
	/// </summary>
	public virtual void Clear()
	{
		fNi.Clear();
		OptimizationSystem.Clear();
	}

	/// <summary>
	/// Unloads all scene and device specific data.  Must be called
	/// when the device is reset (during Game.UnloadGraphicsContent()).
	/// </summary>
	public virtual void Unload()
	{
		Clear();
		fNi.Unload();
	}
}
