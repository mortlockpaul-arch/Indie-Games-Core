using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SynapseGaming.LightingSystem.Rendering;

namespace SynapseGaming.LightingSystem.Core;

public abstract class BaseObjectGraphManager<T, TManagerServiceType> : ObjectGraph<T>, IManagerService, IManager, IUnloadable, ISubmit<IScene> where T : IMovableObject
{
	private IManagerServiceProvider l8a;

	private List<IScene> l8_u0013_ = new List<IScene>(16);

	public abstract Type ManagerType { get; }

	public abstract int ManagerProcessOrder { get; set; }

	public IManagerServiceProvider OwnerSceneInterface => l8a;

	public BaseObjectGraphManager(IManagerServiceProvider sceneinterface, BoundingBox worldboundingbox, int worldtreemaxdepth)
		: base(worldboundingbox, worldtreemaxdepth)
	{
		l8a = sceneinterface;
	}

	public BaseObjectGraphManager(IManagerServiceProvider sceneinterface)
	{
		l8a = sceneinterface;
	}

	public virtual void ApplyPreferences(ISystemPreferences preferences)
	{
	}

	public virtual void Submit(IScene scene)
	{
		l8_u0013_.Add(scene);
		scene.SetContainingManager<TManagerServiceType>(this);
		scene.Apply();
	}

	public virtual void Move(IScene scene)
	{
	}

	public virtual void Remove(IScene scene)
	{
		if (l8_u0013_.Remove(scene))
		{
			scene.SetContainingManager<TManagerServiceType>(null);
		}
	}

	public override void Submit(T obj)
	{
		base.Submit(obj);
		obj.OnSubmittedToManager(this);
	}

	public override void Remove(T obj)
	{
		base.Remove(obj);
		obj.OnRemovedFromManager(this);
	}

	public override void Clear()
	{
		while (l8_u0013_.Count > 0)
		{
			Remove(l8_u0013_[0]);
		}
		l8_u0013_.Clear();
		foreach (KeyValuePair<T, int> allObject in ObjectIndex.AllObjects)
		{
			allObject.Key.OnRemovedFromManager(this);
		}
		base.Clear();
	}

	public virtual void Unload()
	{
		Clear();
	}
}
