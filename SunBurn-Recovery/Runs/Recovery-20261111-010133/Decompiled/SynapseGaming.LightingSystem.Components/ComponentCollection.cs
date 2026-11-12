using SynapseGaming.LightingSystem.Components;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;
using J;
using Microsoft.Xna.Framework;
using SynapseGaming.LightingSystem.Collision;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Serialization;

namespace SynapseGaming.LightingSystem.Core;

[Serializable]
public class ComponentCollection<T> : IGroup<IComponent<T>>, IFullSerializable, ISerializable where T : class
{
	private T l8a;

	private List<IComponent<T>> l8_u0013_ = new List<IComponent<T>>();

	private Dictionary<Type, IComponent<T>> l8N = new Dictionary<Type, IComponent<T>>();

	private IList<IComponent<T>> l8l;

	public IList<IComponent<T>> Components => l8l;

	public T ParentObject
	{
		get
		{
			return l8a;
		}
		set
		{
			l8a = value;
			O();
		}
	}

	public ComponentCollection()
		: this((T)null)
	{
	}

	public ComponentCollection(T parentobject)
	{
		l8l = l8_u0013_.AsReadOnly();
		l8a = parentobject;
	}

	private void O()
	{
		foreach (IComponent<T> item in l8_u0013_)
		{
			item.ParentObject = l8a;
		}
	}

	private void _u000F_()
	{
		l8N.Clear();
		foreach (IComponent<T> item in l8_u0013_)
		{
			l8N.Add(item.ComponentType, item);
		}
		foreach (IComponent<T> item2 in l8_u0013_)
		{
			item2.OnComponentsListChanged();
		}
	}

	public void AddWithDependencies(IComponent<T> component)
	{
		List<Type> dependencies = ComponentDependencyCache.GetDependencies(component.GetType());
		for (int num = dependencies.Count - 1; num >= 0; num--)
		{
			Type type = dependencies[num];
			if (!l8N.ContainsKey(type) && Activator.CreateInstance(type, new object[1] { true }) is IComponent<T> component2 && !l8N.ContainsKey(component2.ComponentType))
			{
				Add(component2);
			}
		}
		Add(component);
	}

	public void Add(IComponent<T> component)
	{
		F(component.ComponentType);
		l8_u0013_.Add(component);
		_u000F_();
		component.ParentObject = l8a;
	}

	public void Insert(int index, IComponent<T> component)
	{
		F(component.ComponentType);
		l8_u0013_.Insert(index, component);
		_u000F_();
		component.ParentObject = l8a;
	}

	public void InsertAfter(IComponent<T> component, Type othercomponenttype)
	{
		F(component.ComponentType);
		int count = l8_u0013_.Count;
		int num = count;
		for (int i = 0; i < count; i++)
		{
			IComponent<T> component2 = l8_u0013_[i];
			if ((object)component2.ComponentType == othercomponenttype)
			{
				num = i + 1;
				break;
			}
		}
		if (num <= count)
		{
			l8_u0013_.Insert(num, component);
		}
		else
		{
			l8_u0013_.Add(component);
		}
		_u000F_();
		component.ParentObject = l8a;
	}

	public void Clear()
	{
		foreach (IComponent<T> item in l8_u0013_)
		{
			item.ParentObject = null;
		}
		l8_u0013_.Clear();
		_u000F_();
	}

	public void RemoveAt(int index)
	{
		l8_u0013_[index].ParentObject = null;
		l8_u0013_.RemoveAt(index);
		_u000F_();
	}

	public void Remove<TComponentType>()
	{
		Remove(typeof(TComponentType));
	}

	public void Remove(IComponent<T> component)
	{
		Remove(component.ComponentType);
	}

	public void Remove(Type componenttype)
	{
		if (F(componenttype))
		{
			_u000F_();
		}
	}

	private bool F(Type param_0)
	{
		IComponent<T> component = GetComponent(param_0, required: false);
		if (component == null)
		{
			return false;
		}
		component.ParentObject = null;
		l8_u0013_.Remove(component);
		return true;
	}

	public TComponentType GetComponent<TComponentType>(bool required)
	{
		Type typeFromHandle = typeof(TComponentType);
		TComponentType val = (TComponentType)GetComponent(typeFromHandle, required);
		if (val == null && required)
		{
			throw new Exception("Component container does not contain a component assigned to the '" + typeFromHandle.Name + "' type.");
		}
		return val;
	}

	public IComponent<T> GetComponent(Type componenttype, bool required)
	{
		if (l8N.TryGetValue(componenttype, out var value))
		{
			return value;
		}
		if (!required)
		{
			return null;
		}
		throw new Exception("Component container does not contain a component assigned to the '" + componenttype.Name + "' type.");
	}

	public void SendMessage(IComponent<T> sender, IComponentMessage message)
	{
		int count = l8_u0013_.Count;
		for (int i = 0; i < count; i++)
		{
			IComponent<T> component = l8_u0013_[i];
			if (component != sender)
			{
				component.OnMessage(sender, message);
			}
		}
	}

	public void OnSubmittedToManager(IManagerService manager)
	{
		foreach (IComponent<T> item in l8_u0013_)
		{
			item.OnSubmittedToManager(manager);
		}
	}

	public void OnRemovedFromManager(IManagerService manager)
	{
		foreach (IComponent<T> item in l8_u0013_)
		{
			item.OnRemovedFromManager(manager);
		}
	}

	public void OnUpdate(GameTime gametime)
	{
		int count = l8_u0013_.Count;
		for (int i = 0; i < count; i++)
		{
			l8_u0013_[i].OnUpdate(gametime);
		}
	}

	public void OnCollisionReact(IMovableObject collider, IMovableObject collidee, CollisionPoint worldcollisionpoint, ref bool collisionhandled)
	{
		int count = l8_u0013_.Count;
		for (int i = 0; i < count; i++)
		{
			l8_u0013_[i].OnCollisionReact(collider, collidee, worldcollisionpoint, ref collisionhandled);
		}
	}

	public void OnCollisionTrigger(IMovableObject collider, IMovableObject trigger)
	{
		int count = l8_u0013_.Count;
		for (int i = 0; i < count; i++)
		{
			l8_u0013_[i].OnCollisionTrigger(collider, trigger);
		}
	}

	public void SetObjectData(SerializationInfo info, StreamingContext context)
	{
		l8_u0013_.Clear();
		List<IComponent<T>> field = null;
		SerializationHelper.DeserializeField(ref field, info, J.a.l8a, usedefault: false);
		if (field != null)
		{
			l8_u0013_.AddRange(field);
		}
		_u000F_();
		O();
	}

	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
	public void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue(J.a.l8a, l8_u0013_);
	}
}
