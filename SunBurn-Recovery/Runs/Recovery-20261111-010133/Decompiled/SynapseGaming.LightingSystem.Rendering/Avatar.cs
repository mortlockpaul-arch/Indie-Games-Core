using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using B;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using SynapseGaming.LightingSystem.Collision;
using SynapseGaming.LightingSystem.Components;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Editor;

namespace SynapseGaming.LightingSystem.Rendering;

public class Avatar : IAvatar, ICollisionObject, ISceneEntity, IMovableObject, IWorldBoundingBoxObject, IComponentObject<ISceneEntity>, IEditorCreatedObject<ISceneEntity>, IEditorObject, INamedObject, IEditorRenderableObject, ICollisionMaterial
{
	private const int l8a = 71;

	private UpdateDelegate l8_u0013_;

	private SubmitRemoveManagerDelegate l8N;

	private SubmitRemoveManagerDelegate l8l;

	private CollisionReactDelegate l8L;

	private CollisionTriggerDelegate l8w;

	private TypeDictionary<IManagerService> l8J = new TypeDictionary<IManagerService>();

	private int l8B;

	private string l8s = "";

	private UpdateType l8h;

	private Matrix l8_u0002_;

	private BoundingSphere l8v;

	private BoundingBox l8Y;

	private BoundingBox l8_u0005_;

	private BoundingBox l8b;

	private BoundingSphere l8I;

	private BoundingBox l85;

	private IList<Matrix> l8W;

	private AvatarExpression l8Z;

	private AvatarRenderer l8T;

	private AvatarDescription l8_u0014_;

	private ObjectVisibility l8E = ObjectVisibility.RenderedAndCastShadows;

	private int l8f;

	private bool l8_u0016_;

	private float l86;

	private float l8x;

	private float l8_u0004_;

	private CollisionType l8X;

	private ICollisionMove l8_u0019_;

	private static List<Matrix> l8y;

	private ComponentCollection<ISceneEntity> l83;

	[CompilerGenerated]
	private Matrix l8j;

	[CompilerGenerated]
	private bool l8p;

	[CompilerGenerated]
	private ICollisionMaterial l8z;

	public TypeDictionary<IManagerService> ContainingManagers => l8J;

	public int UniqueId => 0;

	public bool InfiniteBounds => false;

	public int MoveId => l8B;

	public string Name
	{
		get
		{
			return l8s;
		}
		set
		{
			l8s = value;
		}
	}

	public UpdateType UpdateType
	{
		get
		{
			return l8h;
		}
		set
		{
			l8h = value;
		}
	}

	public Matrix World
	{
		get
		{
			return l8_u0002_;
		}
		set
		{
			if (!l8_u0002_.Equals(value))
			{
				l8_u0002_ = value;
				l8B++;
				WorldToObject = Matrix.Invert(value);
				L5();
			}
		}
	}

	public Matrix WorldToObject
	{
		[CompilerGenerated]
		get
		{
			return l8j;
		}
		[CompilerGenerated]
		private set
		{
			l8j = value;
		}
	}

	public IList<Matrix> SkinBones
	{
		get
		{
			return l8W;
		}
		set
		{
			l8W = value;
		}
	}

	public AvatarExpression Expression
	{
		get
		{
			return l8Z;
		}
		set
		{
			l8Z = value;
		}
	}

	public ObjectVisibility Visibility
	{
		get
		{
			return l8E;
		}
		set
		{
			l8E = value;
		}
	}

	public HullType HullType
	{
		get
		{
			return HullType.Box;
		}
		set
		{
		}
	}

	public BoundingSphere ObjectBoundingSphere => l8v;

	public BoundingBox ObjectBoundingBox => l8Y;

	public BoundingBox WorldBoundingBox => l8b;

	public BoundingSphere WorldBoundingSphere => l8I;

	public BoundingBox WorldBoundingBoxProxy => l85;

	public AvatarRenderer Renderer => l8T;

	public AvatarDescription Description => l8_u0014_;

	public bool CastShadows => (l8E & ObjectVisibility.CastShadows) != 0;

	public bool Visible => (l8E & ObjectVisibility.Rendered) != 0;

	public bool AffectedInCode
	{
		[CompilerGenerated]
		get
		{
			return l8p;
		}
		[CompilerGenerated]
		set
		{
			l8p = value;
		}
	}

	public ComponentCollection<ISceneEntity> Components => l83;

	public float Elasticity
	{
		get
		{
			return l8x;
		}
		set
		{
			l8x = value;
			l8f++;
		}
	}

	public float Friction
	{
		get
		{
			return l86;
		}
		set
		{
			l86 = value;
			l8f++;
		}
	}

	public bool AffectedByGravity
	{
		get
		{
			return l8_u0016_;
		}
		set
		{
			l8_u0016_ = value;
			l8f++;
			if (!l8_u0016_ && l8_u0019_ != null)
			{
				l8_u0019_.RemoveForces();
			}
		}
	}

	public int CollisionId
	{
		get
		{
			return l8f;
		}
		set
		{
			l8f = value;
		}
	}

	public ICollisionMove CollisionMove
	{
		get
		{
			return l8_u0019_;
		}
		set
		{
			if (value == l8_u0019_)
			{
				return;
			}
			if (l8_u0019_ != null)
			{
				foreach (KeyValuePair<Type, IManagerService> item in ContainingManagers.Items)
				{
					l8_u0019_.OnRemovedFromManager(item.Value);
				}
			}
			l8_u0019_ = value;
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

	public CollisionType CollisionType
	{
		get
		{
			return l8X;
		}
		set
		{
			l8X = value;
			l8f++;
		}
	}

	public ICollisionMaterial DefaultCollisionMaterial
	{
		[CompilerGenerated]
		get
		{
			return l8z;
		}
		[CompilerGenerated]
		set
		{
			l8z = value;
		}
	}

	public float Mass
	{
		get
		{
			return l8_u0004_;
		}
		set
		{
			l8_u0004_ = value;
			l8f++;
		}
	}

	public event UpdateDelegate UpdateEvent
	{
		add
		{
			UpdateDelegate updateDelegate = l8_u0013_;
			UpdateDelegate updateDelegate2;
			do
			{
				updateDelegate2 = updateDelegate;
				UpdateDelegate value2 = (UpdateDelegate)Delegate.Combine(updateDelegate2, value);
				updateDelegate = Interlocked.CompareExchange(ref l8_u0013_, value2, updateDelegate2);
			}
			while ((object)updateDelegate != updateDelegate2);
		}
		remove
		{
			UpdateDelegate updateDelegate = l8_u0013_;
			UpdateDelegate updateDelegate2;
			do
			{
				updateDelegate2 = updateDelegate;
				UpdateDelegate value2 = (UpdateDelegate)Delegate.Remove(updateDelegate2, value);
				updateDelegate = Interlocked.CompareExchange(ref l8_u0013_, value2, updateDelegate2);
			}
			while ((object)updateDelegate != updateDelegate2);
		}
	}

	public event SubmitRemoveManagerDelegate SubmittedToManagerEvent
	{
		add
		{
			SubmitRemoveManagerDelegate submitRemoveManagerDelegate = l8N;
			SubmitRemoveManagerDelegate submitRemoveManagerDelegate2;
			do
			{
				submitRemoveManagerDelegate2 = submitRemoveManagerDelegate;
				SubmitRemoveManagerDelegate value2 = (SubmitRemoveManagerDelegate)Delegate.Combine(submitRemoveManagerDelegate2, value);
				submitRemoveManagerDelegate = Interlocked.CompareExchange(ref l8N, value2, submitRemoveManagerDelegate2);
			}
			while ((object)submitRemoveManagerDelegate != submitRemoveManagerDelegate2);
		}
		remove
		{
			SubmitRemoveManagerDelegate submitRemoveManagerDelegate = l8N;
			SubmitRemoveManagerDelegate submitRemoveManagerDelegate2;
			do
			{
				submitRemoveManagerDelegate2 = submitRemoveManagerDelegate;
				SubmitRemoveManagerDelegate value2 = (SubmitRemoveManagerDelegate)Delegate.Remove(submitRemoveManagerDelegate2, value);
				submitRemoveManagerDelegate = Interlocked.CompareExchange(ref l8N, value2, submitRemoveManagerDelegate2);
			}
			while ((object)submitRemoveManagerDelegate != submitRemoveManagerDelegate2);
		}
	}

	public event SubmitRemoveManagerDelegate RemovedFromManagerEvent
	{
		add
		{
			SubmitRemoveManagerDelegate submitRemoveManagerDelegate = l8l;
			SubmitRemoveManagerDelegate submitRemoveManagerDelegate2;
			do
			{
				submitRemoveManagerDelegate2 = submitRemoveManagerDelegate;
				SubmitRemoveManagerDelegate value2 = (SubmitRemoveManagerDelegate)Delegate.Combine(submitRemoveManagerDelegate2, value);
				submitRemoveManagerDelegate = Interlocked.CompareExchange(ref l8l, value2, submitRemoveManagerDelegate2);
			}
			while ((object)submitRemoveManagerDelegate != submitRemoveManagerDelegate2);
		}
		remove
		{
			SubmitRemoveManagerDelegate submitRemoveManagerDelegate = l8l;
			SubmitRemoveManagerDelegate submitRemoveManagerDelegate2;
			do
			{
				submitRemoveManagerDelegate2 = submitRemoveManagerDelegate;
				SubmitRemoveManagerDelegate value2 = (SubmitRemoveManagerDelegate)Delegate.Remove(submitRemoveManagerDelegate2, value);
				submitRemoveManagerDelegate = Interlocked.CompareExchange(ref l8l, value2, submitRemoveManagerDelegate2);
			}
			while ((object)submitRemoveManagerDelegate != submitRemoveManagerDelegate2);
		}
	}

	public event CollisionReactDelegate CollisionReactEvent
	{
		add
		{
			CollisionReactDelegate collisionReactDelegate = l8L;
			CollisionReactDelegate collisionReactDelegate2;
			do
			{
				collisionReactDelegate2 = collisionReactDelegate;
				CollisionReactDelegate value2 = (CollisionReactDelegate)Delegate.Combine(collisionReactDelegate2, value);
				collisionReactDelegate = Interlocked.CompareExchange(ref l8L, value2, collisionReactDelegate2);
			}
			while ((object)collisionReactDelegate != collisionReactDelegate2);
		}
		remove
		{
			CollisionReactDelegate collisionReactDelegate = l8L;
			CollisionReactDelegate collisionReactDelegate2;
			do
			{
				collisionReactDelegate2 = collisionReactDelegate;
				CollisionReactDelegate value2 = (CollisionReactDelegate)Delegate.Remove(collisionReactDelegate2, value);
				collisionReactDelegate = Interlocked.CompareExchange(ref l8L, value2, collisionReactDelegate2);
			}
			while ((object)collisionReactDelegate != collisionReactDelegate2);
		}
	}

	public event CollisionTriggerDelegate CollisionTriggerEvent
	{
		add
		{
			CollisionTriggerDelegate collisionTriggerDelegate = l8w;
			CollisionTriggerDelegate collisionTriggerDelegate2;
			do
			{
				collisionTriggerDelegate2 = collisionTriggerDelegate;
				CollisionTriggerDelegate value2 = (CollisionTriggerDelegate)Delegate.Combine(collisionTriggerDelegate2, value);
				collisionTriggerDelegate = Interlocked.CompareExchange(ref l8w, value2, collisionTriggerDelegate2);
			}
			while ((object)collisionTriggerDelegate != collisionTriggerDelegate2);
		}
		remove
		{
			CollisionTriggerDelegate collisionTriggerDelegate = l8w;
			CollisionTriggerDelegate collisionTriggerDelegate2;
			do
			{
				collisionTriggerDelegate2 = collisionTriggerDelegate;
				CollisionTriggerDelegate value2 = (CollisionTriggerDelegate)Delegate.Remove(collisionTriggerDelegate2, value);
				collisionTriggerDelegate = Interlocked.CompareExchange(ref l8w, value2, collisionTriggerDelegate2);
			}
			while ((object)collisionTriggerDelegate != collisionTriggerDelegate2);
		}
	}

	static Avatar()
	{
		l8y = new List<Matrix>();
		for (int i = 0; i < 71; i++)
		{
			l8y.Add(Matrix.Identity);
		}
	}

	public Avatar(AvatarRenderer avatarrenderer, AvatarDescription description)
	{
		l83 = new ComponentCollection<ISceneEntity>(this);
		l8W = l8y;
		l8_u0002_ = Matrix.Identity;
		DefaultCollisionMaterial = this;
		Elasticity = 0.5f;
		Friction = 0.25f;
		SetRendererAndDescription(avatarrenderer, description);
		L5();
	}

	public void SetRendererAndDescription(AvatarRenderer avatarrenderer, AvatarDescription description)
	{
		l8T = avatarrenderer;
		l8_u0014_ = description;
		l8Y = new BoundingBox(new Vector3(-0.5f, 0f, -0.5f), new Vector3(0.5f, l8_u0014_.Height, 0.5f));
		l8v = BoundingSphere.CreateFromBoundingBox(l8Y);
		l8_u0005_ = CoreHelper.TransformBoundingBox(l8Y, Matrix.CreateScale(2f));
		L5();
	}

	public virtual ISceneEntity Clone()
	{
		Avatar avatar = new Avatar(l8T, l8_u0014_);
		B.L._u0013_B(this, avatar);
		foreach (IComponent<ISceneEntity> component in l83.Components)
		{
			avatar.Components.Add(component.Clone());
		}
		return avatar;
	}

	public virtual void Update(GameTime gametime)
	{
		l83.OnUpdate(gametime);
		if (l8_u0013_ != null)
		{
			l8_u0013_(this, gametime);
		}
	}

	public virtual void OnSubmittedToManager(IManagerService manager)
	{
		l8J.Add(manager.ManagerType, manager);
		l83.OnSubmittedToManager(manager);
		if (l8_u0019_ != null)
		{
			l8_u0019_.OnSubmittedToManager(manager);
		}
		if (l8N != null)
		{
			l8N(manager);
		}
	}

	public virtual void OnRemovedFromManager(IManagerService manager)
	{
		l83.OnRemovedFromManager(manager);
		if (l8_u0019_ != null)
		{
			l8_u0019_.OnRemovedFromManager(manager);
		}
		if (l8l != null)
		{
			l8l(manager);
		}
		l8J.Remove(manager.ManagerType);
	}

	public virtual void OnCreatedInEditor()
	{
	}

	public void OnCollisionReact(IMovableObject collider, IMovableObject collidee, CollisionPoint worldcollisionpoint, ref bool collisionhandled)
	{
		if (l8L != null)
		{
			l8L(collider, collidee, worldcollisionpoint, ref collisionhandled);
		}
		Components.OnCollisionReact(collider, collidee, worldcollisionpoint, ref collisionhandled);
	}

	public void OnCollisionTrigger(IMovableObject collider, IMovableObject trigger)
	{
		if (l8w != null)
		{
			l8w(collider, trigger);
		}
		Components.OnCollisionTrigger(collider, trigger);
	}

	public void ApplyAnimation(IAvatarAnimation animation)
	{
		l8W = animation.BoneTransforms;
		l8Z = animation.Expression;
	}

	public void SetWorldAndWorldToObject(Matrix world, Matrix worldtoobj)
	{
		World = world;
	}

	private void L5()
	{
		l8b = CoreHelper.TransformBoundingBox(l8Y, l8_u0002_);
		l8I = BoundingSphere.CreateFromBoundingBox(l8b);
		l85 = CoreHelper.TransformBoundingBox(l8_u0005_, l8_u0002_);
	}

	public virtual void RenderCustomPass(ISceneState scenestate)
	{
	}

	public virtual void RenderEditorIcon(ISceneState scenestate, BoundingBoxRenderHelper renderhelper, bool highlighted, bool selected, bool sceneoccludedpass)
	{
	}
}
