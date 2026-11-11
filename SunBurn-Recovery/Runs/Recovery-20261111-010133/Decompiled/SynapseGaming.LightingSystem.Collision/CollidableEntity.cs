using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Threading;
using Microsoft.Xna.Framework;
using SynapseGaming.LightingSystem.Components;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Editor;
using SynapseGaming.LightingSystem.Rendering;
using SynapseGaming.LightingSystem.Serialization;

namespace SynapseGaming.LightingSystem.Collision;

[Serializable]
[EditorObject(true)]
[EditorCreatedObject]
public class CollidableEntity : SceneEntity, ICollisionObject, ISceneEntity, IMovableObject, IWorldBoundingBoxObject, IComponentObject<ISceneEntity>, IEditorCreatedObject<ISceneEntity>, IEditorObject, INamedObject, IEditorRenderableObject, ICollisionMaterial
{
	private CollisionReactDelegate l8a;

	private CollisionTriggerDelegate l8_u0013_;

	private bool l8N;

	private float l8l;

	private float l8L;

	private float l8w;

	private float l8J;

	private float l8B;

	private float l8s;

	private CollisionType l8h;

	private ICollisionMove l8_u0002_;

	private static Vector3[] l8v = new Vector3[8];

	[CompilerGenerated]
	private ICollisionMaterial l8Y;

	[EditorProperty(true, HorizontalAlignment = true, MajorGrouping = 6, MinorGrouping = 1)]
	public float Width
	{
		get
		{
			return l8l;
		}
		set
		{
			l8l = value;
			CalculateBounds();
		}
	}

	[EditorProperty(true, HorizontalAlignment = true, MajorGrouping = 6, MinorGrouping = 2)]
	public float Height
	{
		get
		{
			return l8L;
		}
		set
		{
			l8L = value;
			CalculateBounds();
		}
	}

	[EditorProperty(true, HorizontalAlignment = true, MajorGrouping = 6, MinorGrouping = 3)]
	public float Depth
	{
		get
		{
			return l8w;
		}
		set
		{
			l8w = value;
			CalculateBounds();
		}
	}

	[EditorProperty(true, HorizontalAlignment = true, MajorGrouping = 7, MinorGrouping = 3)]
	public float Elasticity
	{
		get
		{
			return l8B;
		}
		set
		{
			l8B = value;
			_CollisionId++;
		}
	}

	[EditorProperty(true, HorizontalAlignment = true, MajorGrouping = 7, MinorGrouping = 2)]
	public float Friction
	{
		get
		{
			return l8J;
		}
		set
		{
			l8J = value;
			_CollisionId++;
		}
	}

	[EditorProperty(true, Description = "Affected By Gravity", MajorGrouping = 5, MinorGrouping = 1)]
	public bool AffectedByGravity
	{
		get
		{
			return l8N;
		}
		set
		{
			l8N = value;
			_CollisionId++;
			if (!l8N && l8_u0002_ != null)
			{
				l8_u0002_.RemoveForces();
			}
		}
	}

	public int CollisionId
	{
		get
		{
			return _CollisionId;
		}
		set
		{
			_CollisionId = value;
		}
	}

	public ICollisionMove CollisionMove
	{
		get
		{
			return l8_u0002_;
		}
		set
		{
			if (value == l8_u0002_)
			{
				return;
			}
			if (l8_u0002_ != null)
			{
				foreach (KeyValuePair<Type, IManagerService> item in ContainingManagers.Items)
				{
					l8_u0002_.OnRemovedFromManager(item.Value);
				}
			}
			l8_u0002_ = value;
			if (value == null)
			{
				return;
			}
			foreach (KeyValuePair<Type, IManagerService> item2 in ContainingManagers.Items)
			{
				value.OnSubmittedToManager(item2.Value);
			}
		}
	}

	[EditorProperty(true, Description = "Collision Type", HorizontalAlignment = true, MajorGrouping = 5, MinorGrouping = 3)]
	public CollisionType CollisionType
	{
		get
		{
			return l8h;
		}
		set
		{
			l8h = value;
			_CollisionId++;
		}
	}

	public ICollisionMaterial DefaultCollisionMaterial
	{
		[CompilerGenerated]
		get
		{
			return l8Y;
		}
		[CompilerGenerated]
		set
		{
			l8Y = value;
		}
	}

	[EditorNumberPadOptions(3, 0.001, 10000.0, 0.1)]
	[EditorProperty(true, Description = "Mass", HorizontalAlignment = true, MajorGrouping = 7, MinorGrouping = 1)]
	public float Mass
	{
		get
		{
			return l8s;
		}
		set
		{
			l8s = value;
			_CollisionId++;
		}
	}

	public event CollisionReactDelegate CollisionReactEvent
	{
		add
		{
			CollisionReactDelegate collisionReactDelegate = l8a;
			CollisionReactDelegate collisionReactDelegate2;
			do
			{
				collisionReactDelegate2 = collisionReactDelegate;
				CollisionReactDelegate value2 = (CollisionReactDelegate)Delegate.Combine(collisionReactDelegate2, value);
				collisionReactDelegate = Interlocked.CompareExchange(ref l8a, value2, collisionReactDelegate2);
			}
			while ((object)collisionReactDelegate != collisionReactDelegate2);
		}
		remove
		{
			CollisionReactDelegate collisionReactDelegate = l8a;
			CollisionReactDelegate collisionReactDelegate2;
			do
			{
				collisionReactDelegate2 = collisionReactDelegate;
				CollisionReactDelegate value2 = (CollisionReactDelegate)Delegate.Remove(collisionReactDelegate2, value);
				collisionReactDelegate = Interlocked.CompareExchange(ref l8a, value2, collisionReactDelegate2);
			}
			while ((object)collisionReactDelegate != collisionReactDelegate2);
		}
	}

	public event CollisionTriggerDelegate CollisionTriggerEvent
	{
		add
		{
			CollisionTriggerDelegate collisionTriggerDelegate = l8_u0013_;
			CollisionTriggerDelegate collisionTriggerDelegate2;
			do
			{
				collisionTriggerDelegate2 = collisionTriggerDelegate;
				CollisionTriggerDelegate value2 = (CollisionTriggerDelegate)Delegate.Combine(collisionTriggerDelegate2, value);
				collisionTriggerDelegate = Interlocked.CompareExchange(ref l8_u0013_, value2, collisionTriggerDelegate2);
			}
			while ((object)collisionTriggerDelegate != collisionTriggerDelegate2);
		}
		remove
		{
			CollisionTriggerDelegate collisionTriggerDelegate = l8_u0013_;
			CollisionTriggerDelegate collisionTriggerDelegate2;
			do
			{
				collisionTriggerDelegate2 = collisionTriggerDelegate;
				CollisionTriggerDelegate value2 = (CollisionTriggerDelegate)Delegate.Remove(collisionTriggerDelegate2, value);
				collisionTriggerDelegate = Interlocked.CompareExchange(ref l8_u0013_, value2, collisionTriggerDelegate2);
			}
			while ((object)collisionTriggerDelegate != collisionTriggerDelegate2);
		}
	}

	public CollidableEntity()
	{
		CollisionType = CollisionType.Collide;
		Mass = 0f;
		AffectedByGravity = false;
		DefaultCollisionMaterial = this;
		Elasticity = 0.5f;
		Friction = 0.25f;
		Width = 1f;
		Height = 1f;
		Depth = 1f;
	}

	protected override void CalculateObjectBounds(ref BoundingBox objectboundingbox, ref BoundingSphere objectboundingsphere)
	{
		Vector3 vector = new Vector3(l8l * 0.5f, l8L * 0.5f, l8w * 0.5f);
		objectboundingbox = new BoundingBox(-vector, vector);
		BoundingSphere.CreateFromBoundingBox(ref objectboundingbox, out objectboundingsphere);
	}

	public override void OnSubmittedToManager(IManagerService manager)
	{
		base.OnSubmittedToManager(manager);
		if (l8_u0002_ != null)
		{
			l8_u0002_.OnSubmittedToManager(manager);
		}
	}

	public override void OnRemovedFromManager(IManagerService manager)
	{
		base.OnRemovedFromManager(manager);
		if (l8_u0002_ != null)
		{
			l8_u0002_.OnRemovedFromManager(manager);
		}
	}

	public void OnCollisionReact(IMovableObject collider, IMovableObject collidee, CollisionPoint worldcollisionpoint, ref bool collisionhandled)
	{
		if (l8a != null)
		{
			l8a(collider, collidee, worldcollisionpoint, ref collisionhandled);
		}
		Components.OnCollisionReact(collider, collidee, worldcollisionpoint, ref collisionhandled);
	}

	public void OnCollisionTrigger(IMovableObject collider, IMovableObject trigger)
	{
		if (l8_u0013_ != null)
		{
			l8_u0013_(collider, trigger);
		}
		Components.OnCollisionTrigger(collider, trigger);
	}

	public override void RenderEditorIcon(ISceneState scenestate, BoundingBoxRenderHelper renderhelper, bool highlighted, bool selected, bool sceneoccludedpass)
	{
		base.RenderEditorIcon(scenestate, renderhelper, highlighted, selected, sceneoccludedpass);
		if (sceneoccludedpass)
		{
			Matrix world = World;
			ObjectBoundingBox.GetCorners(l8v);
			for (int i = 0; i < 8; i++)
			{
				ref Vector3 reference = ref l8v[i];
				reference = Vector3.Transform(l8v[i], world);
			}
			Color mediumSlateBlue = Color.MediumSlateBlue;
			renderhelper.Submit(l8v[0], l8v[4], mediumSlateBlue);
			renderhelper.Submit(l8v[1], l8v[5], mediumSlateBlue);
			renderhelper.Submit(l8v[2], l8v[6], mediumSlateBlue);
			renderhelper.Submit(l8v[3], l8v[7], mediumSlateBlue);
			renderhelper.Submit(l8v[0], l8v[1], mediumSlateBlue);
			renderhelper.Submit(l8v[1], l8v[2], mediumSlateBlue);
			renderhelper.Submit(l8v[2], l8v[3], mediumSlateBlue);
			renderhelper.Submit(l8v[3], l8v[0], mediumSlateBlue);
			renderhelper.Submit(l8v[4], l8v[5], mediumSlateBlue);
			renderhelper.Submit(l8v[5], l8v[6], mediumSlateBlue);
			renderhelper.Submit(l8v[6], l8v[7], mediumSlateBlue);
			renderhelper.Submit(l8v[7], l8v[4], mediumSlateBlue);
		}
	}

	public override void SetObjectData(SerializationInfo info, StreamingContext context)
	{
		SerializationHelper.DeserializeField(ref l8l, info, "Width", usedefault: true);
		SerializationHelper.DeserializeField(ref l8L, info, "Height", usedefault: true);
		SerializationHelper.DeserializeField(ref l8w, info, "Depth", usedefault: true);
		SerializationHelper.DeserializeField(ref l8B, info, "Elasticity", usedefault: true);
		SerializationHelper.DeserializeField(ref l8J, info, "Friction", usedefault: true);
		SerializationHelper.DeserializeField(ref l8s, info, "Mass", usedefault: true);
		SerializationHelper.DeserializeField(ref l8N, info, "AffectedByGravity", usedefault: true);
		SerializationHelper.DeserializeEnum(ref l8h, info, "CollisionType", isflag: false);
		_CollisionId++;
		CalculateBounds();
		base.SetObjectData(info, context);
	}

	public override void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		base.GetObjectData(info, context);
		SerializationHelper.SerializeFieldOrEnum(ref l8l, info, "Width");
		SerializationHelper.SerializeFieldOrEnum(ref l8L, info, "Height");
		SerializationHelper.SerializeFieldOrEnum(ref l8w, info, "Depth");
		SerializationHelper.SerializeFieldOrEnum(ref l8B, info, "Elasticity");
		SerializationHelper.SerializeFieldOrEnum(ref l8J, info, "Friction");
		SerializationHelper.SerializeFieldOrEnum(ref l8s, info, "Mass");
		SerializationHelper.SerializeFieldOrEnum(ref l8N, info, "AffectedByGravity");
		SerializationHelper.SerializeFieldOrEnum(ref l8h, info, "CollisionType");
	}
}
