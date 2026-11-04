using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;
using L;
using Microsoft.Xna.Framework;
using SynapseGaming.LightingSystem.Collision;
using SynapseGaming.LightingSystem.Serialization;

namespace SynapseGaming.LightingSystem.Core;

/// <summary>
/// Container that stores, manages, and updates object components.
/// </summary>
/// <typeparam name="T">Type of the parent class or interface
/// that contains the components. This strongly types the
/// components ensuring only components of the correct
/// type can be assigned.
///
/// For instance all classes and objects that derive from SceneEntity
/// use the ISceneEntity type allowing them to share components.
///
/// However lights use the ILight type to ensure entity components
/// cannot accidently be assigned to them, and vice versa.</typeparam>
[Serializable]
public class ComponentCollection<T> : IGroup<IComponent<T>>, IFullSerializable, ISerializable
{
	private T fNB;

	private List<IComponent<T>> fNa = new List<IComponent<T>>();

	private Dictionary<Type, IComponent<T>> fNg = new Dictionary<Type, IComponent<T>>();

	private IList<IComponent<T>> fNf;

	/// <summary>
	/// Read only list of all contained components.
	/// </summary>
	public IList<IComponent<T>> Components => fNf;

	/// <summary>
	/// Parent object which the components control and interact with.
	/// </summary>
	public T ParentObject
	{
		get
		{
			return fNB;
		}
		set
		{
			fNB = value;
			Y();
		}
	}

	/// <summary>
	/// Creates a new ComponentCollection instance.
	/// </summary>
	public ComponentCollection()
		: this(default(T))
	{
	}

	/// <summary>
	/// Creates a new ComponentCollection instance.
	/// </summary>
	/// <param name="parentobject">Parent object which the components
	/// control and interact with.</param>
	public ComponentCollection(T parentobject)
	{
		fNf = fNa.AsReadOnly();
		fNB = parentobject;
	}

	private void Y()
	{
		foreach (IComponent<T> item in fNa)
		{
			item.ParentObject = fNB;
		}
	}

	private void _4()
	{
		fNg.Clear();
		foreach (IComponent<T> item in fNa)
		{
			fNg.Add(item.ComponentType, item);
		}
		foreach (IComponent<T> item2 in fNa)
		{
			item2.OnComponentsListChanged();
		}
	}

	/// <summary>
	/// Adds a component that will control or interact with the parent
	/// object. Also adds any dependent components not in the container.
	/// </summary>
	/// <param name="component"></param>
	public void AddWithDependencies(IComponent<T> component)
	{
		List<Type> dependencies = ComponentDependencyCache.GetDependencies(component.GetType());
		for (int num = dependencies.Count - 1; num >= 0; num--)
		{
			Type type = dependencies[num];
			if (!fNg.ContainsKey(type) && Activator.CreateInstance(type, new object[1] { true }) is IComponent<T> component2 && !fNg.ContainsKey(component2.ComponentType))
			{
				Add(component2);
			}
		}
		Add(component);
	}

	/// <summary>
	/// Adds a component that will control or interact with the parent object.
	/// </summary>
	/// <param name="component"></param>
	public void Add(IComponent<T> component)
	{
		this.RemoveComponentInternal(component.ComponentType);
		fNa.Add(component);
		_4();
		component.ParentObject = fNB;
	}

	/// <summary>
	/// Inserts a component at the specified index that will control or interact with the parent object.
	/// </summary>
	/// <param name="index"></param>
	/// <param name="component"></param>
	public void Insert(int index, IComponent<T> component)
	{
		this.RemoveComponentInternal(component.ComponentType);
		fNa.Insert(index, component);
		_4();
		component.ParentObject = fNB;
	}

	/// <summary>
	/// Inserts a component after a component of the specified type,
	/// that will control or interact with the parent object.
	///
	/// Used to ensure a component of the specified type will be processed
	/// before the inserted component.
	/// </summary>
	/// <param name="component"></param>
	/// <param name="othercomponenttype">Component type to insert the component after.</param>
	public void InsertAfter(IComponent<T> component, Type othercomponenttype)
	{
		this.RemoveComponentInternal(component.ComponentType);
		int count = fNa.Count;
		int num = count;
		for (int k = 0; k < count; k++)
		{
			IComponent<T> component2 = fNa[k];
			if ((object)component2.ComponentType == othercomponenttype)
			{
				num = k + 1;
				break;
			}
		}
		if (num <= count)
		{
			fNa.Insert(num, component);
		}
		else
		{
			fNa.Add(component);
		}
		_4();
		component.ParentObject = fNB;
	}

	/// <summary>
	/// Clears all components from the container.
	/// </summary>
	public void Clear()
	{
		fNa.Clear();
		_4();
	}

	/// <summary>
	/// Removes the component at the specified index from the container.
	/// </summary>
	/// <param name="index"></param>
	public void RemoveAt(int index)
	{
		fNa.RemoveAt(index);
		_4();
	}

	/// <summary>
	/// Removes a component of the specified type from the container.
	///
	/// Must be the same type provided in IComponent.ComponentType.
	/// </summary>
	/// <typeparam name="TComponentType"></typeparam>
	public void Remove<TComponentType>()
	{
		Remove(typeof(TComponentType));
	}

	/// <summary>
	/// Removes a specific component from the container.
	/// </summary>
	/// <param name="component"></param>
	public void Remove(IComponent<T> component)
	{
		Remove(component.ComponentType);
	}

	/// <summary>
	/// Removes a component of the specified type from the container.
	///
	/// Must be the same type provided in IComponent.ComponentType.
	/// </summary>
	/// <param name="componenttype"></param>
	public void Remove(Type componenttype)
	{
		if (this.RemoveComponentInternal(componenttype))
		{
			_4();
		}
	}

	private bool RemoveComponentInternal(Type P_0)
	{
		IComponent<T> component = GetComponent(P_0, required: false);
		if (component == null)
		{
			return false;
		}
		fNa.Remove(component);
		return true;
	}

	/// <summary>
	/// Gets a component of the specified type from the container.
	///
	/// Must be the same type provided in IComponent.ComponentType.
	/// </summary>
	/// <typeparam name="TComponentType"></typeparam>
	/// <param name="required">Determines if the component is required.
	/// If it is and the component doesn't exist the method will throw an exception.</param>
	/// <returns></returns>
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

	/// <summary>
	/// Gets a component of the specified type from the container.
	///
	/// Must be the same type provided in IComponent.ComponentType.
	/// </summary>
	/// <param name="componenttype"></param>
	/// <param name="required">Determines if the component is required.
	/// If it is and the component doesn't exist the method will throw an exception.</param>
	/// <returns></returns>
	public IComponent<T> GetComponent(Type componenttype, bool required)
	{
		if (fNg.TryGetValue(componenttype, out var value))
		{
			return value;
		}
		if (!required)
		{
			return null;
		}
		throw new Exception("Component container does not contain a component assigned to the '" + componenttype.Name + "' type.");
	}

	/// <summary>
	/// Sends an inter-component message to the parent object's components.
	/// </summary>
	/// <param name="sender">Sender component.</param>
	/// <param name="message">Message data.</param>
	public void SendMessage(IComponent<T> sender, IComponentMessage message)
	{
		int count = fNa.Count;
		for (int k = 0; k < count; k++)
		{
			IComponent<T> component = fNa[k];
			if (component != sender)
			{
				component.OnMessage(sender, message);
			}
		}
	}

	/// <summary>
	/// Event called when the parent object is updated during the game update loop.
	/// </summary>
	/// <param name="gametime"></param>
	public void OnUpdate(GameTime gametime)
	{
		int count = fNa.Count;
		for (int k = 0; k < count; k++)
		{
			fNa[k].OnUpdate(gametime);
		}
	}

	/// <summary>
	/// Event called when the parent object collides with another object.
	/// </summary>
	/// <param name="collider">The moving object.</param>
	/// <param name="collidee">The object hit by the moving object.</param>
	/// <param name="worldcollisionpoint">Contains information about the closest collision point to the collider.</param>
	/// <param name="collisionhandled">Determines if the collision was handled by a prior component.
	/// If this value is true do NOT process any collision reaction code. If the component processes
	/// collision reaction code set this value to true to avoid another component or SunBurn's built-in
	/// reaction code from processing.</param>
	public void OnCollisionReact(IMovableObject collider, IMovableObject collidee, CollisionPoint worldcollisionpoint, ref bool collisionhandled)
	{
		int count = fNa.Count;
		for (int k = 0; k < count; k++)
		{
			fNa[k].OnCollisionReact(collider, collidee, worldcollisionpoint, ref collisionhandled);
		}
	}

	/// <summary>
	/// Event called when the parent object collides with another object, but only
	/// when the parent's CollisionType is set to Trigger.
	///
	/// The component can then apply custom trigger code like damage, apply force, and more.
	/// </summary>
	/// <param name="collider">The moving object.</param>
	/// <param name="trigger">The trigger hit by the moving object.</param>
	public void OnCollisionTrigger(IMovableObject collider, IMovableObject trigger)
	{
		int count = fNa.Count;
		for (int k = 0; k < count; k++)
		{
			fNa[k].OnCollisionTrigger(collider, trigger);
		}
	}

	/// <summary>
	/// Deserializes object data from the provided SerializationInfo.
	/// </summary>
	/// <param name="info">Contains the serialized object data.</param>
	/// <param name="context"></param>
	public void SetObjectData(SerializationInfo info, StreamingContext context)
	{
		fNa.Clear();
		List<IComponent<T>> field = null;
		SerializationHelper.DeserializeField(ref field, info, L.f.fNB, usedefault: false);
		if (field != null)
		{
			fNa.AddRange(field);
		}
		_4();
		Y();
	}

	/// <summary>
	/// Serializes object data to the provided SerializationInfo.
	/// </summary>
	/// <param name="info">SerializationInfo to store the serialized data.</param>
	/// <param name="context"></param>
	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
	public void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue(L.f.fNB, fNa);
	}
}

