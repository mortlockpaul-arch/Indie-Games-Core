using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

/// <summary>
/// Acts as both a service provider and component manager for a scene.
///
/// As a service provider its contained manager services can be requested by type and
/// accessed through common interfaces, making both using the built-in managers and
/// writing custom replacement managers easy.
///
/// As a component manager all contained manager services automatically receive calls
/// to BeginFrameRendering, EndFrameRendering, Update, and more, allowing custom managers
/// to be plugged in and run with out writing any additional code to specifically handle them.
/// </summary>
public class SceneInterface : IManagerServiceProvider, IRenderableManager, IUpdatableManager, IManager, IUnloadable
{
	internal class nNB : IComparer<IManagerService>
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

	internal class nNa : IComparer<IRenderableManager>
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

	internal class nNg : IComparer<IUpdatableManager>
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

	private ISceneState fNB;

	private IGraphicsDeviceService fNa;

	private Dictionary<Type, IManagerService> fNg = new Dictionary<Type, IManagerService>(16);

	private List<IManagerService> fNf = new List<IManagerService>(16);

	private List<IUpdatableManager> fN_0003 = new List<IUpdatableManager>(16);

	private List<IRenderableManager> fNn = new List<IRenderableManager>(16);

	private static nNB fN_0019 = new nNB();

	private static nNa fNL = new nNa();

	private static nNg fNv = new nNg();

	[CompilerGenerated]
	private static SceneInterface fN_0012;

	[CompilerGenerated]
	private IResourceManager fN_0015;

	[CompilerGenerated]
	private IObjectManager fN8;

	[CompilerGenerated]
	private IRenderManager fNi;

	[CompilerGenerated]
	private ILightManager fN_0005;

	[CompilerGenerated]
	private IAudioManager fNj;

	[CompilerGenerated]
	private LightingSystemEditor fNo;

	[CompilerGenerated]
	private IShadowMapManager fNc;

	[CompilerGenerated]
	private IPostProcessManager fNd;

	[CompilerGenerated]
	private IAvatarManager fNw;

	[CompilerGenerated]
	private ILightMapManager fNs;

	[CompilerGenerated]
	private ICollisionManager fN_0011;

	[CompilerGenerated]
	private bool fNQ;

	[CompilerGenerated]
	private bool fNG;

	[CompilerGenerated]
	private SystemStatisticCategory fN_0017;

	/// <summary>
	/// The current GraphicsDeviceManager used by this object.
	/// </summary>
	public IGraphicsDeviceService GraphicsDeviceManager => fNa;

	/// <summary>
	/// Provides access to the current active SceneInterface. A SceneInterface becomes
	/// active when ApplyPreferences(), Update(), Clear(), Unload(),
	/// BeginFrameRendering(), or EndFrameRendering() is called.
	/// </summary>
	public static SceneInterface ActiveSceneInterface
	{
		[CompilerGenerated]
		get
		{
			return fN_0012;
		}
		[CompilerGenerated]
		private set
		{
			fN_0012 = value;
		}
	}

	/// <summary>
	/// Provides convenient access to the ResourceManager manager service contained in the provider.
	///
	/// Note: this property will be null if no manager service of this type is contained in the provider.
	/// </summary>
	public IResourceManager ResourceManager
	{
		[CompilerGenerated]
		get
		{
			return fN_0015;
		}
		[CompilerGenerated]
		private set
		{
			fN_0015 = value;
		}
	}

	/// <summary>
	/// Provides convenient access to the ObjectManager manager service contained in the provider.
	///
	/// Note: this property will be null if no manager service of this type is contained in the provider.
	/// </summary>
	public IObjectManager ObjectManager
	{
		[CompilerGenerated]
		get
		{
			return fN8;
		}
		[CompilerGenerated]
		private set
		{
			fN8 = value;
		}
	}

	/// <summary>
	/// Provides convenient access to the RenderManager manager service contained in the provider.
	///
	/// Note: this property will be null if no manager service of this type is contained in the provider.
	/// </summary>
	public IRenderManager RenderManager
	{
		[CompilerGenerated]
		get
		{
			return fNi;
		}
		[CompilerGenerated]
		private set
		{
			fNi = value;
		}
	}

	/// <summary>
	/// Provides convenient access to the LightManager manager service contained in the provider.
	///
	/// Note: this property will be null if no manager service of this type is contained in the provider.
	/// </summary>
	public ILightManager LightManager
	{
		[CompilerGenerated]
		get
		{
			return fN_0005;
		}
		[CompilerGenerated]
		private set
		{
			fN_0005 = value;
		}
	}

	/// <summary>
	/// Provides convenient access to the AudioManager manager service contained in the provider.
	///
	/// Note: this property will be null if no manager service of this type is contained in the provider.
	/// </summary>
	public IAudioManager AudioManager
	{
		[CompilerGenerated]
		get
		{
			return fNj;
		}
		[CompilerGenerated]
		private set
		{
			fNj = value;
		}
	}

	/// <summary>
	/// Provides convenient access to the LightingSystemEditor service contained in the provider.
	///
	/// Note: this property will be null if no manager service of this type is contained in the provider.
	/// </summary>
	public LightingSystemEditor Editor
	{
		[CompilerGenerated]
		get
		{
			return fNo;
		}
		[CompilerGenerated]
		private set
		{
			fNo = value;
		}
	}

	/// <summary>
	/// Provides convenient access to the ShadowMapManager manager service contained in the provider.
	///
	/// Note: this property will be null if no manager service of this type is contained in the provider.
	/// </summary>
	public IShadowMapManager ShadowMapManager
	{
		[CompilerGenerated]
		get
		{
			return fNc;
		}
		[CompilerGenerated]
		private set
		{
			fNc = value;
		}
	}

	/// <summary>
	/// Provides convenient access to the PostProcessManager manager service contained in the provider.
	///
	/// Note: this property will be null if no manager service of this type is contained in the provider.
	/// </summary>
	public IPostProcessManager PostProcessManager
	{
		[CompilerGenerated]
		get
		{
			return fNd;
		}
		[CompilerGenerated]
		private set
		{
			fNd = value;
		}
	}

	/// <summary>
	/// Provides convenient access to the AvatarManager manager service contained in the provider.
	///
	/// Note: this property will be null if no manager service of this type is contained in the provider.
	/// </summary>
	public IAvatarManager AvatarManager
	{
		[CompilerGenerated]
		get
		{
			return fNw;
		}
		[CompilerGenerated]
		private set
		{
			fNw = value;
		}
	}

	/// <summary>
	/// Provides convenient access to the LightMapManager manager service contained in the provider.
	///
	/// Note: this property will be null if no manager service of this type is contained in the provider.
	/// </summary>
	public ILightMapManager LightMapManager
	{
		[CompilerGenerated]
		get
		{
			return fNs;
		}
		[CompilerGenerated]
		private set
		{
			fNs = value;
		}
	}

	/// <summary>
	/// Provides convenient access to the CollisionManager manager service contained in the provider.
	///
	/// Note: this property will be null if no manager service of this type is contained in the provider.
	/// </summary>
	public ICollisionManager CollisionManager
	{
		[CompilerGenerated]
		get
		{
			return fN_0011;
		}
		[CompilerGenerated]
		private set
		{
			fN_0011 = value;
		}
	}

	/// <summary>
	/// Manager type used to retrieve the IResourceManager manager service.  Use this
	/// type when creating a custom manager that replaces the built-in manager.
	/// </summary>
	public static Type ResourceManagerType => typeof(IResourceManager);

	/// <summary>
	/// Manager type used to retrieve the IObjectManager manager service.  Use this
	/// type when creating a custom manager that replaces the built-in manager.
	/// </summary>
	public static Type ObjectManagerType => typeof(IObjectManager);

	/// <summary>
	/// Manager type used to retrieve the IRenderManager manager service.  Use this
	/// type when creating a custom manager that replaces the built-in manager.
	/// </summary>
	public static Type RenderManagerType => typeof(IRenderManager);

	/// <summary>
	/// Manager type used to retrieve the ILightManager manager service.  Use this
	/// type when creating a custom manager that replaces the built-in manager.
	/// </summary>
	public static Type LightManagerType => typeof(ILightManager);

	/// <summary>
	/// Manager type used to retrieve the IAudioManager manager service.  Use this
	/// type when creating a custom manager that replaces the built-in manager.
	/// </summary>
	public static Type AudioManagerType => typeof(IAudioManager);

	/// <summary>
	/// Manager type used to retrieve the IShadowMapManager manager service.  Use this
	/// type when creating a custom manager that replaces the built-in manager.
	/// </summary>
	public static Type ShadowMapManagerType => typeof(IShadowMapManager);

	/// <summary>
	/// Manager type used to retrieve the IPostProcessManager manager service.  Use this
	/// type when creating a custom manager that replaces the built-in manager.
	/// </summary>
	public static Type PostProcessManagerType => typeof(IPostProcessManager);

	/// <summary>
	/// Manager type used to retrieve the LightingSystemEditor service.
	/// </summary>
	public static Type EditorType => typeof(LightingSystemEditor);

	/// <summary>
	/// Manager type used to retrieve the IAvatarManager manager service.  Use this
	/// type when creating a custom manager that replaces the built-in manager.
	/// </summary>
	public static Type AvatarManagerType => typeof(IAvatarManager);

	/// <summary>
	/// Manager type used to retrieve the ILightMapManager manager service.  Use this
	/// type when creating a custom manager that replaces the built-in manager.
	/// </summary>
	public static Type LightMapManagerType => typeof(ILightMapManager);

	/// <summary>
	/// Manager type used to retrieve the ICollisionManager manager service.  Use this
	/// type when creating a custom manager that replaces the built-in manager.
	/// </summary>
	public static Type CollisionManagerType => typeof(ICollisionManager);

	/// <summary>
	/// Enables on-screen statistics.
	/// </summary>
	public bool ShowStatistics
	{
		[CompilerGenerated]
		get
		{
			return fNQ;
		}
		[CompilerGenerated]
		set
		{
			fNQ = value;
		}
	}

	/// <summary>
	/// Enables on-screen console messages. The console is not displayed if no messages exist.
	/// </summary>
	public bool ShowConsole
	{
		[CompilerGenerated]
		get
		{
			return fNG;
		}
		[CompilerGenerated]
		set
		{
			fNG = value;
		}
	}

	/// <summary>
	/// Categories used when rendering on-screen statistics.
	/// </summary>
	public SystemStatisticCategory StatisticCategories
	{
		[CompilerGenerated]
		get
		{
			return fN_0017;
		}
		[CompilerGenerated]
		set
		{
			fN_0017 = value;
		}
	}

	/// <summary>
	/// Creates a new SceneInterface instance.
	/// </summary>
	/// <param name="graphicsdevicemanager"></param>
	public SceneInterface(IGraphicsDeviceService graphicsdevicemanager)
	{
		fNa = graphicsdevicemanager;
	}

	/// <summary>
	/// Creates and adds a default set of manager services. This makes
	/// initializing the SceneInterface easier.
	///
	/// Depending on the creation options provided the following manager
	/// services will be created:
	///
	/// Always
	///     -ResourceManager
	///     -ObjectManager
	///     -LightManager
	///     -AvatarManager
	///     -PostProcessManager
	///     -CollisionManager
	///     -LightMapManager
	///     -LightingSystemEditor
	///
	/// Forward rendering
	///     -RenderManager
	///     -LightManager
	///
	/// Deferred rednering
	///     -DeferredRenderManager
	///     -DeferredShadowMapManager
	/// </summary>
	/// <param name="usedeferredrendering"></param>
	public void CreateDefaultManagers(bool usedeferredrendering)
	{
		Unload();
		AddManager(new ResourceManager());
		AddManager(new ObjectManager(fNa));
		AddManager(new LightManager(fNa, this));
		AddManager(new LightMapManager(fNa, this));
		AddManager(new CollisionManager(fNa, this));
		AddManager(new AudioManager(fNa));
		if (fNa is GraphicsDeviceManager)
		{
			AddManager(new LightingSystemEditor(fNa, this));
		}
		if (usedeferredrendering)
		{
			AddManager(new DeferredRenderManager(fNa, this));
			AddManager(new DeferredShadowMapManager(fNa));
		}
		else
		{
			AddManager(new RenderManager(fNa, this));
			AddManager(new ShadowMapManager(fNa));
		}
		AddManager(new AvatarManager(fNa, this));
		AddManager(new PostProcessManager(fNa));
	}

	/// <summary>
	/// Adds a manager service to the provider.
	/// </summary>
	/// <param name="manager"></param>
	public virtual void AddManager(IManagerService manager)
	{
		RemoveManager(manager);
		if (!fNg.ContainsKey(manager.ManagerType))
		{
			fNg.Add(manager.ManagerType, manager);
			ResortServices();
			gs();
		}
	}

	/// <summary>
	/// Removes a manager service from the provider.
	/// </summary>
	/// <param name="manager"></param>
	public virtual void RemoveManager(IManagerService manager)
	{
		if (fNg.ContainsKey(manager.ManagerType))
		{
			fNg.Remove(manager.ManagerType);
			ResortServices();
			gs();
		}
	}

	private void gs()
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
		Editor = GetManager(EditorType, required: false) as LightingSystemEditor;
	}

	/// <summary>
	/// Resorts the contained manager services.
	///
	/// Providers should automatically resort when manager services
	/// are added and removed, however manual resorting is necessary
	/// if a manager service's ManagerProcessOrder property changes
	/// after being added to the provider.
	/// </summary>
	public virtual void ResortServices()
	{
		fNf.Clear();
		fNn.Clear();
		fN_0003.Clear();
		foreach (KeyValuePair<Type, IManagerService> item in fNg)
		{
			IManagerService value = item.Value;
			fNf.Add(value);
			if (value is IRenderableManager)
			{
				fNn.Add(value as IRenderableManager);
			}
			if (value is IUpdatableManager)
			{
				fN_0003.Add(value as IUpdatableManager);
			}
		}
		fNf.Sort(fN_0019);
		fNn.Sort(fNL);
		fN_0003.Sort(fNv);
	}

	/// <summary>
	/// Retrieves a manager service by type from the provider.
	/// </summary>
	/// <typeparam name="T">Type used by the manager as a unique
	/// identifying key (IManagerService.ManagerType).</typeparam>
	/// <param name="required">Determines whether an exception should
	/// be thrown if the manager is not found.</param>
	/// <returns></returns>
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

	/// <summary>
	/// Retrieves a manager service by type from the provider.
	/// </summary>
	/// <param name="managertype">Type used by the manager as a unique
	/// identifying key (IManagerService.ManagerType).</param>
	/// <param name="required">Determines whether an exception should
	/// be thrown if the manager is not found.</param>
	/// <returns></returns>
	public IManagerService GetManager(Type managertype, bool required)
	{
		fNg.TryGetValue(managertype, out var value);
		if (value == null && required)
		{
			throw new Exception("Service manager does not contain a service assigned to the '" + managertype.Name + "' type.");
		}
		return value;
	}

	/// <summary>
	/// Retrieves all manager services from the provider.
	/// </summary>
	/// <param name="managers">List used to store manager services.</param>
	public void GetManagers(List<IManagerService> managers)
	{
		foreach (KeyValuePair<Type, IManagerService> item in fNg)
		{
			managers.Add(item.Value);
		}
	}

	/// <summary>
	/// Use to apply user quality and performance preferences to the
	/// contained manager services.
	/// </summary>
	/// <param name="preferences"></param>
	public virtual void ApplyPreferences(ILightingSystemPreferences preferences)
	{
		ActiveSceneInterface = this;
		foreach (IManagerService item in fNf)
		{
			item.ApplyPreferences(preferences);
		}
	}

	/// <summary>
	/// Submits a scene to the contained manager services.
	/// </summary>
	/// <param name="scene"></param>
	public virtual void Submit(IScene scene)
	{
		ActiveSceneInterface = this;
		foreach (IManagerService item in fNf)
		{
			if (item is ISubmit<IScene> submit)
			{
				submit.Submit(scene);
			}
		}
	}

	/// <summary>
	/// Removes a scene from the contained manager services.
	/// </summary>
	/// <param name="scene"></param>
	public virtual void Remove(IScene scene)
	{
		ActiveSceneInterface = this;
		foreach (IManagerService item in fNf)
		{
			if (item is ISubmit<IScene> submit)
			{
				submit.Remove(scene);
			}
		}
	}

	/// <summary>
	/// Removes resources managed by the contained manager services.
	/// Commonly used while clearing the scene.
	///
	/// Note: this does not remove the contained manager services.
	/// </summary>
	public virtual void Clear()
	{
		ActiveSceneInterface = this;
		foreach (IManagerService item in fNf)
		{
			item.Clear();
		}
		OptimizationSystem.Clear();
	}

	/// <summary>
	/// Disposes any graphics resources used internally by the
	/// contained manager services, and removes scene resources
	/// managed by them. Commonly used during Game.UnloadContent.
	///
	/// Note: this does not remove the contained manager services.
	/// </summary>
	public virtual void Unload()
	{
		ActiveSceneInterface = this;
		foreach (IManagerService item in fNf)
		{
			item.Unload();
		}
		OptimizationSystem.Clear();
	}

	/// <summary>
	/// Updates the contained manager services and their managed resources.
	/// </summary>
	/// <param name="gameTime"></param>
	public virtual void Update(GameTime gameTime)
	{
		ActiveSceneInterface = this;
		LightingSystemPerformance.Begin("SceneInterface.Update");
		foreach (IUpdatableManager item in fN_0003)
		{
			item.Update(gameTime);
		}
	}

	/// <summary>
	/// Sets up the contained manager services prior to rendering (used for forward rendering).
	/// </summary>
	/// <param name="scenestate"></param>
	public virtual void BeginFrameRendering(ISceneState scenestate)
	{
		ActiveSceneInterface = this;
		LightingSystemPerformance.Begin("SceneInterface.BeginFrameRendering");
		fNB = scenestate;
		foreach (IRenderableManager item in fNn)
		{
			item.BeginFrameRendering(scenestate);
		}
	}

	/// <summary>
	/// Finalizes rendering on the contained manager services.
	/// </summary>
	public virtual void EndFrameRendering()
	{
		ActiveSceneInterface = this;
		LightingSystemPerformance.Begin("SceneInterface.EndFrameRendering");
		for (int num = fNn.Count - 1; num >= 0; num--)
		{
			fNn[num].EndFrameRendering();
		}
		SystemConsole.Apply();
		LightingSystemPerformance.Begin("SceneInterface.EndFrameRendering (stats)");
		if ((ShowConsole || ShowStatistics) && fNB != null && fNB.RenderingToScreen)
		{
			SystemConsole.Render(fNa.GraphicsDevice, StatisticCategories, ShowStatistics, ShowConsole, new Vector2(20f), Vector2.One, Color.White, fNB.GameTime);
		}
	}
}
