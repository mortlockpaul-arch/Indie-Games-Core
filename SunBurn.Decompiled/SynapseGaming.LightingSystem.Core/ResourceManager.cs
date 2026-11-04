using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace SynapseGaming.LightingSystem.Core;

/// <summary>
/// Can be assigned ownership of disposable and unloadable resources, automatically
/// freeing them when the scene is unloaded.
/// </summary>
public class ResourceManager : IResourceManager, IManagerService, IManager, IUnloadable
{
	internal delegate Effect nNB();

	private static bool fNB = true;

	private static Dictionary<string, Effect> fNa = new Dictionary<string, Effect>();

	private static Dictionary<Type, Effect> fNg = new Dictionary<Type, Effect>();

	private int fNf = 500;

	private Dictionary<IDisposable, int> fN_0003 = new Dictionary<IDisposable, int>(32);

	private Dictionary<IUnloadable, int> fNn = new Dictionary<IUnloadable, int>(32);

	/// <summary>
	/// Determines if material effects loaded from the content pipeline are shared
	/// based on the source material file.
	///
	/// As an example models and effects that load the material "Materials\Rock.mat"
	/// will all share a single reference to the same "Materials\Rock.mat" material effect.
	/// Modifying properties on the effect will change the material properties for
	/// all models and effects referencing it.
	///
	/// Disabling shared materials emulates the behavior of SunBurn prior to version 2.0.13
	/// and standard XNA, where loading multiple models will create multiple unique copies
	/// of the same material effect.
	/// </summary>
	public static bool ShareMaterialsBetweenModels
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
	/// Gets the manager specific Type used as a unique key for storing and
	/// requesting the manager from the IManagerServiceProvider.
	/// </summary>
	public Type ManagerType => SceneInterface.ResourceManagerType;

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

	internal static Effect g_0003(Type P_0, nNB P_1)
	{
		if (fNg.TryGetValue(P_0, out var value) && !value.IsDisposed)
		{
			return value;
		}
		value = P_1();
		fNg[P_0] = value;
		return value;
	}

	internal static Effect g_0003(string P_0, nNB P_1)
	{
		if (!fNB)
		{
			return P_1();
		}
		if (fNa.TryGetValue(P_0, out var value) && !value.IsDisposed)
		{
			return value;
		}
		value = P_1();
		fNa[P_0] = value;
		return value;
	}

	/// <summary>
	/// Unused.
	/// </summary>
	/// <param name="preferences"></param>
	public void ApplyPreferences(ILightingSystemPreferences preferences)
	{
	}

	/// <summary>
	/// Assigns ownership of the resource to the resource manager, this means the manager
	/// will handle disposing and removing (IDisposable), or unloading (IUnloadable) the
	/// resource when the scene is unloaded (when [manager].Unload() is called).
	/// </summary>
	/// <param name="resource"></param>
	public void AssignOwnership(IDisposable resource)
	{
		if (!fN_0003.ContainsKey(resource))
		{
			fN_0003.Add(resource, 0);
		}
	}

	/// <summary>
	/// Assigns ownership of the resource to the resource manager, this means the manager
	/// will handle disposing and removing (IDisposable), or unloading (IUnloadable) the
	/// resource when the scene is unloaded (when [manager].Unload() is called).
	/// </summary>
	/// <param name="resource"></param>
	public void AssignOwnership(IUnloadable resource)
	{
		if (!fNn.ContainsKey(resource))
		{
			fNn.Add(resource, 0);
		}
	}

	/// <summary>
	/// Unused. Resources assigned to the manager are not removed until
	/// they are disposed (during the Unload method).
	/// </summary>
	public void Clear()
	{
	}

	/// <summary>
	/// Disposes and removes all IDisposable resources. Unloads but
	/// continues tracking IUnloadable resources.
	///
	/// Commonly used during Game.UnloadContent.
	/// </summary>
	public void Unload()
	{
		foreach (KeyValuePair<IDisposable, int> item in fN_0003)
		{
			item.Key.Dispose();
		}
		foreach (KeyValuePair<IUnloadable, int> item2 in fNn)
		{
			item2.Key.Unload();
		}
		fN_0003.Clear();
		fNa.Clear();
		fNg.Clear();
	}
}
