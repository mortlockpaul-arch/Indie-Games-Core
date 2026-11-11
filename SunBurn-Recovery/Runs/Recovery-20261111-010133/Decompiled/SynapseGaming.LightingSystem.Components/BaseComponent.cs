using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using B;
using Microsoft.Xna.Framework;
using SynapseGaming.LightingSystem.Collision;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Editor;
using SynapseGaming.LightingSystem.Rendering;

namespace SynapseGaming.LightingSystem.Components;

[Serializable]
public class BaseComponent<T> : IComponent<T>, IEditorCreatedObject<IComponent<T>>, IEditorObject, INamedObject where T : class, IComponentObject<T>
{
	private T l8a;

	[CompilerGenerated]
	private bool l8_u0013_;

	[CompilerGenerated]
	private string l8N;

	public virtual Type ComponentType => GetType();

	[EditorProperty(false)]
	public bool AffectedInCode
	{
		[CompilerGenerated]
		get
		{
			return l8_u0013_;
		}
		[CompilerGenerated]
		set
		{
			l8_u0013_ = value;
		}
	}

	[EditorProperty(false)]
	public string Name
	{
		[CompilerGenerated]
		get
		{
			return l8N;
		}
		[CompilerGenerated]
		set
		{
			l8N = value;
		}
	}

	public T ParentObject
	{
		get
		{
			return l8a;
		}
		set
		{
			if (value == l8a)
			{
				return;
			}
			if (l8a != null)
			{
				if (l8a is ISceneEntity sceneEntity)
				{
					foreach (KeyValuePair<Type, IManagerService> item in sceneEntity.ContainingManagers.Items)
					{
						OnRemovedFromManager(item.Value);
					}
				}
				OnRemovedFromParentObject();
			}
			l8a = value;
			if (value == null)
			{
				return;
			}
			OnInitialize();
			OnAddedToParentObject();
			if (!(value is ISceneEntity sceneEntity2))
			{
				return;
			}
			foreach (KeyValuePair<Type, IManagerService> item2 in sceneEntity2.ContainingManagers.Items)
			{
				OnSubmittedToManager(item2.Value);
			}
		}
	}

	public virtual IComponent<T> Clone()
	{
		IComponent<T> component = Create();
		B.L._u0013_B(this, component);
		return component;
	}

	protected virtual IComponent<T> Create()
	{
		return (IComponent<T>)Activator.CreateInstance(GetType());
	}

	public void SendMessage()
	{
		SendMessage(null);
	}

	public void SendMessage(IComponentMessage message)
	{
		l8a.Components.SendMessage(this, message);
	}

	public virtual void OnInitialize()
	{
	}

	public virtual void OnComponentsListChanged()
	{
	}

	public virtual void OnMessage(IComponent<T> sender, IComponentMessage message)
	{
	}

	public virtual void OnAddedToParentObject()
	{
	}

	public virtual void OnRemovedFromParentObject()
	{
	}

	public virtual void OnCreatedInEditor()
	{
	}

	public virtual void OnSubmittedToManager(IManagerService manager)
	{
	}

	public virtual void OnRemovedFromManager(IManagerService manager)
	{
	}

	public virtual void OnUpdate(GameTime gametime)
	{
	}

	public virtual void OnCollisionReact(IMovableObject collider, IMovableObject collidee, CollisionPoint worldcollisionpoint, ref bool collisionhandled)
	{
	}

	public virtual void OnCollisionTrigger(IMovableObject collider, IMovableObject trigger)
	{
	}
}
