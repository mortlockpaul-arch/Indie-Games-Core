using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using SynapseGaming.LightingSystem.Audio;
using SynapseGaming.LightingSystem.Collision;
using SynapseGaming.LightingSystem.Editor;
using SynapseGaming.LightingSystem.Lights;
using SynapseGaming.LightingSystem.Rendering;
using SynapseGaming.LightingSystem.Rendering.Deferred;
using SynapseGaming.LightingSystem.Rendering.Forward;
using SynapseGaming.LightingSystem.Shadows;
using SynapseGaming.LightingSystem.Shadows.Deferred;
using SynapseGaming.LightingSystem.Shadows.Forward;

namespace SynapseGaming.LightingSystem.Core;

public class SceneInterface : IManagerServiceProvider
{
	internal class w8a : IComparer<IManagerService>
	{
		public int Compare(IManagerService a, IManagerService b)
		{
			if (a == null)
			{
				return -1;
			}
			if (b == null)
			{
				return 1;
			}
			return a.ManagerProcessOrder - b.ManagerProcessOrder;
		}
	}

	internal class w8_u0013_ : IComparer<IRenderableManager>
	{
		public int Compare(IRenderableManager a, IRenderableManager b)
		{
			if (a == null)
			{
				return -1;
			}
			if (b == null)
			{
				return 1;
			}
			return (a as IManagerService).ManagerProcessOrder - (b as IManagerService).ManagerProcessOrder;
		}
	}

	internal class w8N : IComparer<IUpdatableManager>
	{
		public int Compare(IUpdatableManager a, IUpdatableManager b)
		{
			if (a == null)
			{
				return -1;
			}
			if (b == null)
			{
				return 1;
			}
			return (a as IManagerService).ManagerProcessOrder - (b as IManagerService).ManagerProcessOrder;
		}
	}

	private ISceneState l8a;

	private Dictionary<Type, IManagerService> l8_u0013_ = new Dictionary<Type, IManagerService>(16);

	private List<IManagerService> l8N = new List<IManagerService>(16);

	private List<IUpdatableManager> l8l = new List<IUpdatableManager>(16);

	private List<IRenderableManager> l8L = new List<IRenderableManager>(16);

	private static w8a l8w = new w8a();

	private static w8_u0013_ l8J = new w8_u0013_();

	private static w8N l8B = new w8N();

	[CompilerGenerated]
	private static SceneInterface l8s;

	[CompilerGenerated]
	private IResourceManager l8h;

	[CompilerGenerated]
	private IObjectManager l8_u0002_;

	[CompilerGenerated]
	private IRenderManager l8v;

	[CompilerGenerated]
	private ILightManager l8Y;

	[CompilerGenerated]
	private IAudioManager l8_u0005_;

	[CompilerGenerated]
	private SunBurnEditor l8b;

	[CompilerGenerated]
	private IShadowMapManager l8I;

	[CompilerGenerated]
	private IPostProcessManager l85;

	[CompilerGenerated]
	private IAvatarManager l8W;

	[CompilerGenerated]
	private ILightMapManager l8Z;

	[CompilerGenerated]
	private ICollisionManager l8T;

	[CompilerGenerated]
	private bool l8_u0014_;

	[CompilerGenerated]
	private bool l8E;

	[CompilerGenerated]
	private SystemStatisticCategory l8f;

	public static SceneInterface ActiveSceneInterface
	{
		[CompilerGenerated]
		get
		{
			return l8s;
		}
		[CompilerGenerated]
		private set
		{
			l8s = value;
		}
	}

	public IResourceManager ResourceManager
	{
		[CompilerGenerated]
		get
		{
			return l8h;
		}
		[CompilerGenerated]
		private set
		{
			l8h = value;
		}
	}

	public IObjectManager ObjectManager
	{
		[CompilerGenerated]
		get
		{
			return l8_u0002_;
		}
		[CompilerGenerated]
		private set
		{
			l8_u0002_ = value;
		}
	}

	public IRenderManager RenderManager
	{
		[CompilerGenerated]
		get
		{
			return l8v;
		}
		[CompilerGenerated]
		private set
		{
			l8v = value;
		}
	}

	public ILightManager LightManager
	{
		[CompilerGenerated]
		get
		{
			return l8Y;
		}
		[CompilerGenerated]
		private set
		{
			l8Y = value;
		}
	}

	public IAudioManager AudioManager
	{
		[CompilerGenerated]
		get
		{
			return l8_u0005_;
		}
		[CompilerGenerated]
		private set
		{
			l8_u0005_ = value;
		}
	}

	public SunBurnEditor Editor
	{
		[CompilerGenerated]
		get
		{
			return l8b;
		}
		[CompilerGenerated]
		private set
		{
			l8b = value;
		}
	}

	public IShadowMapManager ShadowMapManager
	{
		[CompilerGenerated]
		get
		{
			return l8I;
		}
		[CompilerGenerated]
		private set
		{
			l8I = value;
		}
	}

	public IPostProcessManager PostProcessManager
	{
		[CompilerGenerated]
		get
		{
			return l85;
		}
		[CompilerGenerated]
		private set
		{
			l85 = value;
		}
	}

	public IAvatarManager AvatarManager
	{
		[CompilerGenerated]
		get
		{
			return l8W;
		}
		[CompilerGenerated]
		private set
		{
			l8W = value;
		}
	}

	public ILightMapManager LightMapManager
	{
		[CompilerGenerated]
		get
		{
			return l8Z;
		}
		[CompilerGenerated]
		private set
		{
			l8Z = value;
		}
	}

	public ICollisionManager CollisionManager
	{
		[CompilerGenerated]
		get
		{
			return l8T;
		}
		[CompilerGenerated]
		private set
		{
			l8T = value;
		}
	}

	public static Type ResourceManagerType => typeof(IResourceManager);

	public static Type ObjectManagerType => typeof(IObjectManager);

	public static Type RenderManagerType => typeof(IRenderManager);

	public static Type LightManagerType => typeof(ILightManager);

	public static Type AudioManagerType => typeof(IAudioManager);

	public static Type ShadowMapManagerType => typeof(IShadowMapManager);

	public static Type PostProcessManagerType => typeof(IPostProcessManager);

	public static Type EditorType => typeof(SunBurnEditor);

	public static Type AvatarManagerType => typeof(IAvatarManager);

	public static Type LightMapManagerType => typeof(ILightMapManager);

	public static Type CollisionManagerType => typeof(ICollisionManager);

	public bool ShowStatistics
	{
		[CompilerGenerated]
		get
		{
			return l8_u0014_;
		}
		[CompilerGenerated]
		set
		{
			l8_u0014_ = value;
		}
	}

	public bool ShowConsole
	{
		[CompilerGenerated]
		get
		{
			return l8E;
		}
		[CompilerGenerated]
		set
		{
			l8E = value;
		}
	}

	public SystemStatisticCategory StatisticCategories
	{
		[CompilerGenerated]
		get
		{
			return l8f;
		}
		[CompilerGenerated]
		set
		{
			l8f = value;
		}
	}

	public void CreateDefaultManagers(RenderingSystemType renderingsystemtype, bool includeautoloadedplugins)
	{
		Unload();
		AddManager(new ResourceManager(this));
		AddManager(new ObjectManager(this));
		AddManager(new LightManager(this));
		AddManager(new LightMapManager(this));
		AddManager(new AudioManager(this));
		if (SunBurnCoreSystem.Instance.GraphicsDeviceManager is GraphicsDeviceManager)
		{
			AddManager(new SunBurnEditor(this));
		}
		if (renderingsystemtype == RenderingSystemType.Deferred)
		{
			AddManager(new DeferredRenderManager(this));
			AddManager(new DeferredShadowMapManager(this));
		}
		else
		{
			AddManager(new RenderManager(this));
			AddManager(new ShadowMapManager(this));
		}
		AddManager(new AvatarManager(this));
		AddManager(new PostProcessManager(this));
		SunBurnCoreSystem.Instance.InitializeSceneInterfaceWithLoadedPlugins(this, includeautoloadedplugins);
	}

	public virtual void AddManager(IManagerService manager)
	{
		RemoveManager(manager);
		if (!l8_u0013_.ContainsKey(manager.ManagerType))
		{
			l8_u0013_.Add(manager.ManagerType, manager);
			ResortServices();
			_u0013__u000F_();
		}
	}

	public virtual void RemoveManager(IManagerService manager)
	{
		if (l8_u0013_.ContainsKey(manager.ManagerType))
		{
			l8_u0013_.Remove(manager.ManagerType);
			ResortServices();
			_u0013__u000F_();
		}
	}

	private void _u0013__u000F_()
	{
		ResourceManager = GetManager(ResourceManagerType, required: false) as IResourceManager;
		ObjectManager = GetManager(ObjectManagerType, required: false) as IObjectManager;
		RenderManager = GetManager(RenderManagerType, required: false) as IRenderManager;
		LightManager = GetManager(LightManagerType, required: false) as ILightManager;
		AudioManager = GetManager(AudioManagerType, required: false) as IAudioManager;
		ShadowMapManager = GetManager(ShadowMapManagerType, required: false) as IShadowMapManager;
		AvatarManager = GetManager(AvatarManagerType, required: false) as IAvatarManager;
		PostProcessManager = GetManager(PostProcessManagerType, required: false) as IPostProcessManager;
		LightMapManager = GetManager(LightMapManagerType, required: false) as ILightMapManager;
		CollisionManager = GetManager(CollisionManagerType, required: false) as ICollisionManager;
		Editor = GetManager(EditorType, required: false) as SunBurnEditor;
	}

	public virtual void ResortServices()
	{
		l8N.Clear();
		l8L.Clear();
		l8l.Clear();
		foreach (KeyValuePair<Type, IManagerService> item in l8_u0013_)
		{
			IManagerService value = item.Value;
			l8N.Add(value);
			if (value is IRenderableManager)
			{
				l8L.Add(value as IRenderableManager);
			}
			if (value is IUpdatableManager)
			{
				l8l.Add(value as IUpdatableManager);
			}
		}
		l8N.Sort(l8w);
		l8L.Sort(l8J);
		l8l.Sort(l8B);
	}

	public virtual T GetManager<T>(bool required) where T : class
	{
		Type typeFromHandle = typeof(T);
		T val = GetManager(typeFromHandle, required) as T;
		if (val == null && required)
		{
			throw new Exception("Service manager does not contain a service assigned to the '" + typeFromHandle.Name + "' type.");
		}
		return val;
	}

	public IManagerService GetManager(Type managertype, bool required)
	{
		l8_u0013_.TryGetValue(managertype, out var value);
		if (value == null && required)
		{
			throw new Exception("Service manager does not contain a service assigned to the '" + managertype.Name + "' type.");
		}
		return value;
	}

	public void GetManagers(List<IManagerService> managers)
	{
		foreach (KeyValuePair<Type, IManagerService> item in l8_u0013_)
		{
			managers.Add(item.Value);
		}
	}

	public virtual void ApplyPreferences(ISystemPreferences preferences)
	{
		ActiveSceneInterface = this;
		foreach (IManagerService item in l8N)
		{
			item.ApplyPreferences(preferences);
		}
	}

	public virtual void Submit(IScene scene)
	{
		ActiveSceneInterface = this;
		foreach (IManagerService item in l8N)
		{
			if (item is ISubmit<IScene> submit)
			{
				submit.Submit(scene);
			}
		}
	}

	public virtual void Remove(IScene scene)
	{
		ActiveSceneInterface = this;
		foreach (IManagerService item in l8N)
		{
			if (item is ISubmit<IScene> submit)
			{
				submit.Remove(scene);
			}
		}
	}

	public virtual void Clear()
	{
		ActiveSceneInterface = this;
		foreach (IManagerService item in l8N)
		{
			item.Clear();
		}
		OptimizationSystem.Clear();
		GeometryExtractionHelper.Clear();
	}

	public virtual void Unload()
	{
		ActiveSceneInterface = this;
		foreach (IManagerService item in l8N)
		{
			item.Unload();
		}
		OptimizationSystem.Clear();
		GeometryExtractionHelper.Clear();
	}

	public virtual void Update(GameTime gameTime)
	{
		ActiveSceneInterface = this;
		LightingSystemPerformance.Begin("SceneInterface.Update");
		foreach (IUpdatableManager item in l8l)
		{
			item.Update(gameTime);
		}
	}

	public virtual void BeginFrameRendering(ISceneState scenestate)
	{
		ActiveSceneInterface = this;
		LightingSystemPerformance.Begin("SceneInterface.BeginFrameRendering");
		l8a = scenestate;
		foreach (IRenderableManager item in l8L)
		{
			item.BeginFrameRendering(scenestate);
		}
	}

	public virtual void EndFrameRendering()
	{
		ActiveSceneInterface = this;
		LightingSystemPerformance.Begin("SceneInterface.EndFrameRendering");
		for (int num = l8L.Count - 1; num >= 0; num--)
		{
			l8L[num].EndFrameRendering();
		}
		SystemConsole.Apply();
		LightingSystemPerformance.Begin("SceneInterface.EndFrameRendering (stats)");
		if ((ShowConsole || ShowStatistics) && l8a != null && l8a.RenderingToScreen)
		{
			SystemConsole.Render(StatisticCategories, ShowStatistics, ShowConsole, new Vector2(20f), Vector2.One, Color.White, l8a.GameTime);
		}
	}
}
