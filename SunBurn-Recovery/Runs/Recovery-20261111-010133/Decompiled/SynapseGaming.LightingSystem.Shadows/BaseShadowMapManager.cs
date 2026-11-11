using System;
using System.Collections.Generic;
using B;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Lights;
using y;

namespace SynapseGaming.LightingSystem.Shadows;

public abstract class BaseShadowMapManager : BaseShadowManager, IShadowMapVisibility, IShadowMapManager, IRenderableManager, IManagerService, IManager, IUnloadable
{
	private enum w8a
	{
		High,
		Normal,
		Low
	}

	private const int l8a = 67108864;

	private const int l8_u0013_ = 2048;

	private const int l8N = 32;

	private int l8l = 40;

	private float[] l8L = new float[3] { 0.2f, 0.53f, 1f };

	private bool[] l8w = new bool[3] { true, true, true };

	private bool l8J = true;

	private float l8B = 1f;

	private w8a l8s = w8a.Normal;

	private float l8h = 1f;

	private B.w l8_u0002_;

	private ShadowMapCache l8v;

	private B._u0002_<ShadowRenderTargetGroup> l8Y = new B._u0002_<ShadowRenderTargetGroup>();

	private RenderTarget2D l8_u0005_;

	private static List<ShadowGroup> l8b = new List<ShadowGroup>(128);

	private static List<Rectangle> l8I = new List<Rectangle>();

	private static Dictionary<RenderTarget2D, ShadowRenderTargetGroup> l85 = new Dictionary<RenderTarget2D, ShadowRenderTargetGroup>();

	public Type ManagerType => SceneInterface.ShadowMapManagerType;

	public int ManagerProcessOrder
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

	public float[] ShadowLODRangeHints => l8L;

	public bool[] ShadowLODEnabled => l8w;

	public bool PreferHalfFloatTextureFormat
	{
		get
		{
			return l8v.PreferHalfFloatTextureFormat;
		}
		set
		{
			if (l8v.PreferHalfFloatTextureFormat != value)
			{
				l8v.Resize(l8v.PageSize, l8v.MaxMemoryUsage, value);
			}
		}
	}

	public int MaxMemoryUsage
	{
		get
		{
			return l8v.MaxMemoryUsage;
		}
		set
		{
			if (l8v.MaxMemoryUsage != value)
			{
				l8v.Resize(l8v.PageSize, value, l8v.PreferHalfFloatTextureFormat);
			}
		}
	}

	public int PageSize
	{
		get
		{
			return l8v.PageSize;
		}
		set
		{
			if (l8v.PageSize != value)
			{
				l8v.Resize(value, l8v.MaxMemoryUsage, l8v.PreferHalfFloatTextureFormat);
			}
		}
	}

	private int _MaxShadowLOD => l8v.PageSize >> 1;

	protected abstract IShadowMap CreateDirectionalShadowMap(IShadowSource shadowsource);

	protected abstract IShadowMap CreatePointShadowMap(IShadowSource shadowsource);

	protected abstract IShadowMap CreateSpotShadowMap(IShadowSource shadowsource);

	public BaseShadowMapManager(IManagerServiceProvider sceneinterface, int pagesize, int maxmemoryusage, bool preferhalffloat)
		: base(sceneinterface)
	{
		l8_u0002_ = new B.w();
		l8v = new ShadowMapCache(pagesize, maxmemoryusage, preferhalffloat);
	}

	public BaseShadowMapManager(IManagerServiceProvider sceneinterface, ShadowMapCache shadowmapcache)
		: base(sceneinterface)
	{
		l8_u0002_ = new B.w();
		l8v = shadowmapcache;
	}

	public BaseShadowMapManager(IManagerServiceProvider sceneinterface)
		: base(sceneinterface)
	{
		l8_u0002_ = new B.w();
		l8v = new ShadowMapCache(2048, 67108864, preferhalffloat: false);
	}

	public override void ApplyPreferences(ISystemPreferences preferences)
	{
		l8J = preferences.ShadowDetail != DetailPreference.Off;
		l8B = MathHelper.Clamp(preferences.ShadowQuality, 0.05f, 1f);
	}

	public void BuildShadows(List<ShadowRenderTargetGroup> rendertargetgroups, List<BaseLight> lights, bool usedefaultgrouping)
	{
		GraphicsDevice graphicsDevice = SunBurnCoreSystem.Instance.GraphicsDeviceManager.GraphicsDevice;
		if (l8_u0005_ == null)
		{
			l8_u0005_ = new RenderTarget2D(graphicsDevice, 16, 16, mipMap: true, SurfaceFormat.Color, DepthFormat.None);
		}
		rendertargetgroups.Clear();
		l8b.Clear();
		l85.Clear();
		BuildShadowGroups(l8b, lights, usedefaultgrouping);
		if (l8b.Count < 1)
		{
			return;
		}
		float num = (float)_MaxShadowLOD * l8B * l8h;
		foreach (ShadowGroup item2 in l8b)
		{
			RenderTarget2D renderTarget2D = l8_u0005_;
			if (l8J && item2.ShadowSource.ShadowType != ShadowType.None)
			{
				ShadowSourceTypeCaster shadowSourceTypeCaster = item2.ShadowSourceTypes;
				IShadowMap shadowMap;
				if (shadowSourceTypeCaster.SpotSource != null)
				{
					shadowMap = CreateSpotShadowMap(item2.ShadowSource);
				}
				else if (shadowSourceTypeCaster.PointSource != null)
				{
					shadowMap = CreatePointShadowMap(item2.ShadowSource);
				}
				else
				{
					if (shadowSourceTypeCaster.DirectionalSource == null)
					{
						continue;
					}
					shadowMap = CreateDirectionalShadowMap(item2.ShadowSource);
				}
				shadowMap.Build(graphicsDevice, SceneState, item2, this, l8B);
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
						l8I.Clear();
						ShadowMapSurface[] surfaces = shadowMap.Surfaces;
						foreach (ShadowMapSurface shadowMapSurface in surfaces)
						{
							float num4 = num3 * num2 * shadowMapSurface.LevelOfDetail;
							num4 = CoreHelper.Log2(num4);
							num4 = (float)Math.Floor(num4 * 2f) * 0.5f;
							num4 = (float)Math.Pow(2.0, num4);
							int num5 = (item.Height = (item.Width = (int)MathHelper.Clamp(num4, 32f, _MaxShadowLOD)));
							l8I.Add(item);
							if (num5 > 32)
							{
								flag = false;
							}
						}
						renderTarget2D = l8v.ReserveSections(l8I);
						if (renderTarget2D == null)
						{
							l8s = w8a.High;
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
					for (int j = 0; j < shadowMap.Surfaces.Length; j++)
					{
						shadowMap.SetSurfaceRenderTargetLocation(j, l8I[j]);
					}
				}
				item2.Shadow = shadowMap;
			}
			if (!l85.ContainsKey(renderTarget2D))
			{
				ShadowRenderTargetGroup shadowRenderTargetGroup = l8Y.New();
				shadowRenderTargetGroup.ShadowGroups.Clear();
				shadowRenderTargetGroup.ShadowGroups.Add(item2);
				l85.Add(renderTarget2D, shadowRenderTargetGroup);
			}
			else
			{
				l85[renderTarget2D].ShadowGroups.Add(item2);
			}
		}
		foreach (KeyValuePair<RenderTarget2D, ShadowRenderTargetGroup> item3 in l85)
		{
			ShadowRenderTargetGroup value = item3.Value;
			RenderTarget2D key = item3.Key;
			if (key == l8_u0005_)
			{
				value.Build(null);
			}
			else
			{
				value.Build(key);
			}
			rendertargetgroups.Add(value);
		}
	}

	public override void BeginFrameRendering(ISceneState scenestate)
	{
		if (l8_u0002_.Changed)
		{
			Unload();
		}
		base.BeginFrameRendering(scenestate);
	}

	public override void EndFrameRendering()
	{
		if (l8s == w8a.High)
		{
			l8h *= 0.75f;
			l8s = w8a.Normal;
		}
		else if (l8h < 1f && (l8s == w8a.Low || l8v.w_u0002_() < 0.33f))
		{
			l8h = Math.Min(l8h * 1.33f, 1f);
			l8s = w8a.Low;
		}
		l8v.ClearReserves();
		l8Y.FreeAllTracked();
		base.EndFrameRendering();
	}

	public override void Clear()
	{
	}

	public override void Unload()
	{
		l8v.Unload();
		l8Y.Unload();
		y.a.w_u0005_(ref l8_u0005_);
	}
}
