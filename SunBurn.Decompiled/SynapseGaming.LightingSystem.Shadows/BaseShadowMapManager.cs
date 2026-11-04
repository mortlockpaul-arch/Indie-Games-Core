using System;
using System.Collections.Generic;
using _0013;
using L;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Lights;

namespace SynapseGaming.LightingSystem.Shadows;

/// <summary>
/// Base class that provides shadow map management.  Used by the forward rendering
/// ShadowMapManager and deferred rendering DeferredShadowMapManager classes.
/// </summary>
public abstract class BaseShadowMapManager : BaseShadowManager, IShadowMapVisibility, IShadowMapManager, IRenderableManager, IManagerService, IManager, IUnloadable
{
	private enum nNB
	{
		High,
		Normal,
		Low
	}

	private const int fNB = 67108864;

	private const int fNa = 2048;

	private const int fNg = 32;

	private int fNf = 40;

	private float[] fN_0003 = new float[3] { 0.2f, 0.53f, 1f };

	private bool[] fNn = new bool[3] { true, true, true };

	private bool fN_0019 = true;

	private float fNL = 1f;

	private nNB fNv = nNB.Normal;

	private float fN_0012 = 1f;

	private global::L.L fN_0015;

	private ShadowMapCache fN8;

	private L._0005<ShadowRenderTargetGroup> fNi = new L._0005<ShadowRenderTargetGroup>();

	private RenderTarget2D fN_0005;

	private static List<ShadowGroup> fNj = new List<ShadowGroup>(128);

	private static List<Rectangle> fNo = new List<Rectangle>();

	private static Dictionary<RenderTarget2D, ShadowRenderTargetGroup> fNc = new Dictionary<RenderTarget2D, ShadowRenderTargetGroup>();

	/// <summary>
	/// Gets the manager specific Type used as a unique key for storing and
	/// requesting the manager from the IManagerServiceProvider.
	/// </summary>
	public Type ManagerType => SceneInterface.ShadowMapManagerType;

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
			return fNf;
		}
		set
		{
			fNf = value;
		}
	}

	/// <summary>
	/// Determines how far from the camera a directional shadow level-of-detail (lod) will
	/// stretch before transitioning to the next lod.
	///
	/// Index 0 controls the highest level of detail, 1 controls the next highest, and so on.
	///
	/// The range is normalized relative to the environment ShadowFadeEndDistance,
	/// for instance a value of 1.0 transitions at the ShadowFadeEndDistance
	/// whereas a value of 0.25 transitions at (ShadowFadeEndDistance * 0.25).
	/// </summary>
	public float[] ShadowLODRangeHints => fN_0003;

	/// <summary>
	/// Determines if a directional shadow level-of-detail (lod) is enabled and will have its
	/// shadow map filled with an image of the scene.  Disabling unneeded lods reduces
	/// the number of rendered objects and draw calls.
	///
	/// Index 0 controls the highest level of detail, 1 controls the next highest, and so on.
	///
	/// Unlike point light shadows, directional light shadows render all of their lods
	/// every frame.  Each lod represents a different area in front of the camera with
	/// the highest lod closest to the viewer.  For some games (such as top-down
	/// perspective games) only a single lod is necessary and the rest can be disabled.
	/// </summary>
	public bool[] ShadowLODEnabled => fNn;

	/// <summary>
	/// True when smaller half-float format render targets are preferred. These
	/// formats consume less memory and generally perform better, but have lower
	/// accuracy on directional lights.
	/// </summary>
	public bool PreferHalfFloatTextureFormat
	{
		get
		{
			return fN8.PreferHalfFloatTextureFormat;
		}
		set
		{
			if (fN8.PreferHalfFloatTextureFormat != value)
			{
				fN8.Resize(fN8.PageSize, fN8.MaxMemoryUsage, value);
			}
		}
	}

	/// <summary>
	/// Maximum amount of memory the shadow map cache is allowed to consume. This is an
	/// approximate value and the cache may use more memory in certain instances.
	/// </summary>
	public int MaxMemoryUsage
	{
		get
		{
			return fN8.MaxMemoryUsage;
		}
		set
		{
			if (fN8.MaxMemoryUsage != value)
			{
				fN8.Resize(fN8.PageSize, value, fN8.PreferHalfFloatTextureFormat);
			}
		}
	}

	/// <summary>
	/// Size in pixels of each render target (page) in the cache. For a size of 1024
	/// the actual page dimensions are 1024x1024. Small sizes can reduce performance by
	/// fragmenting the shadow maps, and reduce shadow quality by lowering the maximum
	/// resolution of each shadow map section.
	/// </summary>
	public int PageSize
	{
		get
		{
			return fN8.PageSize;
		}
		set
		{
			if (fN8.PageSize != value)
			{
				fN8.Resize(value, fN8.MaxMemoryUsage, fN8.PreferHalfFloatTextureFormat);
			}
		}
	}

	private int _MaxShadowLOD => fN8.PageSize >> 1;

	/// <summary>
	/// Creates a new or cached shadow map object for this light type.
	/// </summary>
	/// <param name="shadowsource">Shadow source which uses the newly created or cached shadow map object.
	/// Provides information about how the shadow is used, such as location and the type of objects rendered
	/// to the shadow map.</param>
	/// <returns></returns>
	protected abstract IShadowMap CreateDirectionalShadowMap(IShadowSource shadowsource);

	/// <summary>
	/// Creates a new or cached shadow map object for this light type.
	/// </summary>
	/// <param name="shadowsource">Shadow source which uses the newly created or cached shadow map object.
	/// Provides information about how the shadow is used, such as location and the type of objects rendered
	/// to the shadow map.</param>
	/// <returns></returns>
	protected abstract IShadowMap CreatePointShadowMap(IShadowSource shadowsource);

	/// <summary>
	/// Creates a new or cached shadow map object for this light type.
	/// </summary>
	/// <param name="shadowsource">Shadow source which uses the newly created or cached shadow map object.
	/// Provides information about how the shadow is used, such as location and the type of objects rendered
	/// to the shadow map.</param>
	/// <returns></returns>
	protected abstract IShadowMap CreateSpotShadowMap(IShadowSource shadowsource);

	/// <summary>
	/// Creates a new BaseShadowMapManager instance.
	/// </summary>
	/// <param name="graphicsdevicemanager"></param>
	/// <param name="pagesize">Size in pixels of each render target (page) in the cache.
	/// For a size of 1024 the actual page dimensions are 1024x1024. Small sizes can reduce
	/// performance by fragmenting the shadow maps, and reduce shadow quality by lowering
	/// the maximum resolution of each shadow map section.</param>
	/// <param name="maxmemoryusage">Maximum amount of memory the cache is allowed to consume.
	/// This is an approximate value and the cache may use more memory in certain instances.</param>
	/// <param name="preferhalffloat">True when smaller half-float format render targets are
	/// preferred. These formats consume less memory and generally perform better, but have
	/// lower accuracy on directional lights.</param>
	public BaseShadowMapManager(IGraphicsDeviceService graphicsdevicemanager, int pagesize, int maxmemoryusage, bool preferhalffloat)
		: base(graphicsdevicemanager)
	{
		fN_0015 = new global::L.L(graphicsdevicemanager);
		fN8 = new ShadowMapCache(graphicsdevicemanager, pagesize, maxmemoryusage, preferhalffloat);
	}

	/// <summary>
	/// Creates a new BaseShadowMapManager instance.
	/// </summary>
	/// <param name="graphicsdevicemanager"></param>
	/// <param name="shadowmapcache"></param>
	public BaseShadowMapManager(IGraphicsDeviceService graphicsdevicemanager, ShadowMapCache shadowmapcache)
		: base(graphicsdevicemanager)
	{
		fN_0015 = new global::L.L(graphicsdevicemanager);
		fN8 = shadowmapcache;
	}

	/// <summary>
	/// Creates a new BaseShadowMapManager instance.
	/// </summary>
	/// <param name="graphicsdevicemanager"></param>
	public BaseShadowMapManager(IGraphicsDeviceService graphicsdevicemanager)
		: base(graphicsdevicemanager)
	{
		fN_0015 = new global::L.L(graphicsdevicemanager);
		fN8 = new ShadowMapCache(graphicsdevicemanager, 2048, 67108864, preferhalffloat: false);
	}

	/// <summary>
	/// Use to apply user quality and performance preferences to the resources managed by this object.
	/// </summary>
	/// <param name="preferences"></param>
	public override void ApplyPreferences(ILightingSystemPreferences preferences)
	{
		fN_0019 = preferences.ShadowDetail != DetailPreference.Off;
		fNL = MathHelper.Clamp(preferences.ShadowQuality, 0.05f, 1f);
	}

	/// <summary>
	/// Organizes the provided lights into shadow and render target groups.
	/// </summary>
	/// <param name="rendertargetgroups">Returned render target groups.</param>
	/// <param name="lights">Lights to organize.</param>
	/// <param name="usedefaultgrouping">Determines if ungrouped lights should be placed in a
	/// single default group (recommended: true for deferred rendering and false for forward).</param>
	public void BuildShadows(List<ShadowRenderTargetGroup> rendertargetgroups, List<BaseLight> lights, bool usedefaultgrouping)
	{
		GraphicsDevice graphicsDevice = GraphicsDeviceManager.GraphicsDevice;
		if (fN_0005 == null)
		{
			fN_0005 = new RenderTarget2D(graphicsDevice, 16, 16, mipMap: true, SurfaceFormat.Color, DepthFormat.None);
		}
		rendertargetgroups.Clear();
		fNj.Clear();
		fNc.Clear();
		BuildShadowGroups(fNj, lights, usedefaultgrouping);
		if (fNj.Count < 1)
		{
			return;
		}
		float num = (float)_MaxShadowLOD * fNL * fN_0012;
		foreach (ShadowGroup item2 in fNj)
		{
			RenderTarget2D renderTarget2D = fN_0005;
			if (fN_0019 && item2.ShadowSource.ShadowType != ShadowType.None)
			{
				L.c c2 = item2.ShadowSourceTypes;
				IShadowMap shadowMap;
				if (c2.fNa != null)
				{
					shadowMap = CreateSpotShadowMap(item2.ShadowSource);
				}
				else if (c2.fNB != null)
				{
					shadowMap = CreatePointShadowMap(item2.ShadowSource);
				}
				else
				{
					if (c2.fNg == null)
					{
						continue;
					}
					shadowMap = CreateDirectionalShadowMap(item2.ShadowSource);
				}
				shadowMap.Build(graphicsDevice, SceneState, item2, this, fNL);
				if (shadowMap.CustomRenderTarget != null)
				{
					renderTarget2D = shadowMap.CustomRenderTarget;
				}
				else
				{
					float num2 = 1f;
					float num3 = num * item2.ShadowSource.ShadowQuality;
					Rectangle item = default;
					do
					{
						bool flag = true;
						fNo.Clear();
						ShadowMapSurface[] surfaces = shadowMap.Surfaces;
						foreach (ShadowMapSurface shadowMapSurface in surfaces)
						{
							float num4 = num3 * num2 * shadowMapSurface.LevelOfDetail;
							num4 = L._0019.a_0011(num4);
							num4 = (float)Math.Floor(num4 * 2f) * 0.5f;
							num4 = (float)Math.Pow(2.0, num4);
							int num5 = (item.Height = (item.Width = (int)MathHelper.Clamp(num4, 32f, _MaxShadowLOD)));
							fNo.Add(item);
							if (num5 > 32)
							{
								flag = false;
							}
						}
						renderTarget2D = fN8.ReserveSections(fNo);
						if (renderTarget2D == null)
						{
							fNv = nNB.High;
						}
						if (flag)
						{
							break;
						}
						num2 *= 0.5f;
					}
					while (renderTarget2D == null);
					if (renderTarget2D == null)
					{
						continue;
					}
					for (int l = 0; l < shadowMap.Surfaces.Length; l++)
					{
						shadowMap.SetSurfaceRenderTargetLocation(l, fNo[l]);
					}
				}
				item2.Shadow = shadowMap;
			}
			if (!fNc.ContainsKey(renderTarget2D))
			{
				ShadowRenderTargetGroup shadowRenderTargetGroup = fNi.New();
				shadowRenderTargetGroup.ShadowGroups.Clear();
				shadowRenderTargetGroup.ShadowGroups.Add(item2);
				fNc.Add(renderTarget2D, shadowRenderTargetGroup);
			}
			else
			{
				fNc[renderTarget2D].ShadowGroups.Add(item2);
			}
		}
		foreach (KeyValuePair<RenderTarget2D, ShadowRenderTargetGroup> item3 in fNc)
		{
			ShadowRenderTargetGroup value = item3.Value;
			RenderTarget2D key = item3.Key;
			if (key == fN_0005)
			{
				value.Build(graphicsDevice, null);
			}
			else
			{
				value.Build(graphicsDevice, key);
			}
			rendertargetgroups.Add(value);
		}
	}

	/// <summary>
	/// Sets up frame information necessary for scene shadowing.
	/// </summary>
	public override void BeginFrameRendering(ISceneState scenestate)
	{
		if (fN_0015.Changed)
		{
			Unload();
		}
		base.BeginFrameRendering(scenestate);
	}

	/// <summary>
	/// Cleans up frame information including removing all reserved shadow maps.
	/// </summary>
	public override void EndFrameRendering()
	{
		if (fNv == nNB.High)
		{
			fN_0012 *= 0.75f;
			fNv = nNB.Normal;
		}
		else if (fN_0012 < 1f && (fNv == nNB.Low || fN8.n_0014() < 0.33f))
		{
			fN_0012 = Math.Min(fN_0012 * 1.33f, 1f);
			fNv = nNB.Low;
		}
		fN8.ClearReserves();
		fNi.FreeAllTracked();
		base.EndFrameRendering();
	}

	/// <summary>
	/// Cleans up scene information.
	/// </summary>
	public override void Clear()
	{
	}

	/// <summary>
	/// Disposes any graphics resource used internally by this object, and removes
	/// scene resources managed by this object. Commonly used during Game.UnloadContent.
	/// </summary>
	public override void Unload()
	{
		fN8.Unload();
		fNi.Unload();
		_0013.B.nu(ref fN_0005);
	}
}
