using System.Collections.Generic;
using B;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Lights;
using SynapseGaming.LightingSystem.Rendering;

namespace SynapseGaming.LightingSystem.Shadows;

public abstract class BaseShadowManager : IRenderableManager, IManager, IUnloadable
{
	private IManagerServiceProvider l8a;

	private ISceneState l8_u0013_ = new SceneState();

	private B.h<ShadowGroup> l8N = new B.h<ShadowGroup>();

	private static ShadowSource l8l = new ShadowSource();

	private static DirectionalLight l8L = new DirectionalLight();

	private static ShadowGroup l8w = new ShadowGroup();

	private static ShadowGroup l8J = new ShadowGroup();

	private static Dictionary<IShadowSource, ShadowGroup> l8B = new Dictionary<IShadowSource, ShadowGroup>(32);

	public IManagerServiceProvider OwnerSceneInterface => l8a;

	protected ISceneState SceneState => l8_u0013_;

	public BaseShadowManager(IManagerServiceProvider sceneinterface)
	{
		l8a = sceneinterface;
	}

	public virtual void ApplyPreferences(ISystemPreferences preferences)
	{
	}

	public virtual void BeginFrameRendering(ISceneState scenestate)
	{
		SplashScreen.L_u0015_();
		l8_u0013_ = scenestate;
	}

	public virtual void EndFrameRendering()
	{
		l8N.FreeAllTracked();
	}

	protected void BuildShadowGroups(List<ShadowGroup> shadowgroups, List<BaseLight> lights, bool usedefaultgrouping)
	{
		l8B.Clear();
		l8l.ShadowType = ShadowType.None;
		l8w.Shadow = null;
		l8w.Lights.Clear();
		l8B.Add(l8l, l8w);
		l8L.ShadowType = ShadowType.None;
		l8J.Shadow = null;
		l8J.Lights.Clear();
		l8B.Add(l8L, l8J);
		int count = lights.Count;
		for (int i = 0; i < count; i++)
		{
			BaseLight baseLight = lights[i];
			if (baseLight == null)
			{
				continue;
			}
			IShadowSource shadowSource = baseLight.ShadowSource;
			if (usedefaultgrouping && (shadowSource == null || (baseLight == shadowSource && shadowSource.ShadowType == ShadowType.None)))
			{
				LightTypeCaster lightTypeCaster = OptimizationSystem.LightTypeCasters.Get(baseLight);
				if (lightTypeCaster.PointSource != null)
				{
					l8w.Lights.Add(baseLight);
				}
				else
				{
					l8J.Lights.Add(baseLight);
				}
				continue;
			}
			if (!l8B.TryGetValue(shadowSource, out var value))
			{
				value = l8N.New();
				value.Shadow = null;
				value.Lights.Clear();
				l8B.Add(shadowSource, value);
			}
			value.Lights.Add(baseLight);
		}
		if (l8J.Lights.Count <= 0)
		{
			l8B.Remove(l8L);
		}
		if (l8w.Lights.Count <= 0)
		{
			l8B.Remove(l8l);
		}
		else
		{
			l8l.Position = (l8w.Lights[0] as IPointSource).Position;
		}
		foreach (KeyValuePair<IShadowSource, ShadowGroup> item in l8B)
		{
			item.Value.Build(item.Key, l8_u0013_);
			shadowgroups.Add(item.Value);
		}
	}

	public abstract void Clear();

	public abstract void Unload();
}
