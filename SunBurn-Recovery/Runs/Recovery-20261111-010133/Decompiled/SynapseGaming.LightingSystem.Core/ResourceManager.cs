using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SynapseGaming.LightingSystem.Core;

public class ResourceManager : IResourceManager, IManagerService, IUpdatableManager, IManager, IUnloadable
{
	internal delegate Effect w8a();

	private static bool l8a = true;

	private static Dictionary<string, Effect> l8_u0013_ = new Dictionary<string, Effect>();

	private static Dictionary<Type, Effect> l8N = new Dictionary<Type, Effect>();

	private int l8l = 500;

	private IManagerServiceProvider l8L;

	private Dictionary<IDisposable, WeakReference> l8w = new Dictionary<IDisposable, WeakReference>(32);

	private Dictionary<IUnloadable, int> l8J = new Dictionary<IUnloadable, int>(32);

	private List<IDisposable> l8B = new List<IDisposable>(32);

	public static bool ShareMaterialsBetweenModels
	{
		get
		{
			return l8a;
		}
		set
		{
			l8a = value;
		}
	}

	public Type ManagerType => SceneInterface.ResourceManagerType;

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

	public IManagerServiceProvider OwnerSceneInterface => l8L;

	internal static Effect _u0013__u0004_(Type param_0, w8a param_1)
	{
		if (l8N.TryGetValue(param_0, out var value) && !value.IsDisposed)
		{
			return value;
		}
		value = param_1();
		l8N[param_0] = value;
		return value;
	}

	internal static Effect _u0013__u0004_(string param_0, w8a param_1)
	{
		if (!l8a)
		{
			return param_1();
		}
		if (l8_u0013_.TryGetValue(param_0, out var value) && !value.IsDisposed)
		{
			return value;
		}
		value = param_1();
		l8_u0013_[param_0] = value;
		return value;
	}

	public ResourceManager(IManagerServiceProvider sceneinterface)
	{
		l8L = sceneinterface;
	}

	public void ApplyPreferences(ISystemPreferences preferences)
	{
	}

	public void AssignOwnership(IDisposable resource)
	{
		l8w[resource] = null;
	}

	public void AssignOwnership(IUnloadable resource)
	{
		l8J[resource] = 0;
	}

	public void LinkOwnership(IDisposable resource, object linkedobject)
	{
		l8w[resource] = new WeakReference(linkedobject);
	}

	public void Update(GameTime gametime)
	{
		l8B.Clear();
		foreach (KeyValuePair<IDisposable, WeakReference> item in l8w)
		{
			WeakReference value = item.Value;
			if (value != null && value.Target == null)
			{
				l8B.Add(item.Key);
			}
		}
		foreach (IDisposable item2 in l8B)
		{
			item2.Dispose();
			l8w.Remove(item2);
		}
	}

	public void Clear()
	{
	}

	public void Unload()
	{
		foreach (KeyValuePair<IDisposable, WeakReference> item in l8w)
		{
			item.Key.Dispose();
		}
		foreach (KeyValuePair<IUnloadable, int> item2 in l8J)
		{
			item2.Key.Unload();
		}
		l8w.Clear();
		l8_u0013_.Clear();
		l8N.Clear();
	}
}
