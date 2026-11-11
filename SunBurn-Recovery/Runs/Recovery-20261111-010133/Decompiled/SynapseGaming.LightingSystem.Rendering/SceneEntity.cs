using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;
using B;
using Microsoft.Xna.Framework;
using SynapseGaming.LightingSystem.Components;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Editor;
using SynapseGaming.LightingSystem.Serialization;
using X;

namespace SynapseGaming.LightingSystem.Rendering;

[Serializable]
[EditorCreatedObject]
public class SceneEntity : ISceneEntity, IMovableObject, IWorldBoundingBoxObject, IComponentObject<ISceneEntity>, IEditorCreatedObject<ISceneEntity>, IEditorObject, INamedObject, IEditorRenderableObject, IFullSerializable, ISerializable
{
	private UpdateDelegate l8a;

	private SubmitRemoveManagerDelegate l8_u0013_;

	private SubmitRemoveManagerDelegate l8N;

	private TypeDictionary<IManagerService> l8l = new TypeDictionary<IManagerService>();

	private int l8L;

	private UpdateType l8w;

	private Matrix l8J = Matrix.Identity;

	private Matrix l8B = Matrix.Identity;

	private BoundingBox l8s = default;

	private BoundingSphere l8h = default;

	private int l8_u0002_;

	private bool l8v;

	private string l8Y;

	protected int _CollisionId;

	private HullType l8_u0005_ = HullType.Box;

	private BoundingSphere l8b;

	private BoundingBox l8I;

	protected ComponentCollection<ISceneEntity> _Components;

	[CompilerGenerated]
	private bool l85;

	public TypeDictionary<IManagerService> ContainingManagers => l8l;

	[EditorProperty(false)]
	public int UniqueId
	{
		get
		{
			return l8_u0002_;
		}
		set
		{
			l8_u0002_ = value;
		}
	}

	[EditorProperty(false)]
	public Matrix World
	{
		get
		{
			return l8J;
		}
		set
		{
			if (!l8J.Equals(value))
			{
				SetWorldAndWorldToObject(value, Matrix.Invert(value));
			}
		}
	}

	public Matrix WorldToObject => l8B;

	public bool InfiniteBounds => l8v;

	[EditorProperty(false)]
	public int MoveId
	{
		get
		{
			return l8L;
		}
		set
		{
			l8L = value;
		}
	}

	[EditorProperty(true, Description = "Receives Updates", HorizontalAlignment = true, MajorGrouping = 2, MinorGrouping = 1, ToolTipText = "", ControlType = ControlType.CheckBox)]
	[EditorCheckboxOptions(true)]
	public UpdateType UpdateType
	{
		get
		{
			return l8w;
		}
		set
		{
			l8w = value;
		}
	}

	public BoundingSphere ObjectBoundingSphere => l8b;

	public BoundingBox ObjectBoundingBox => l8I;

	public BoundingBox WorldBoundingBox => l8s;

	public BoundingSphere WorldBoundingSphere => l8h;

	[EditorProperty(true, Description = "Name", HorizontalAlignment = true, MajorGrouping = 1, MinorGrouping = 1, ToolTipText = "")]
	public string Name
	{
		get
		{
			return l8Y;
		}
		set
		{
			l8Y = value;
		}
	}

	[EditorProperty(true, Description = "Hull Type", HorizontalAlignment = true, MajorGrouping = 5, MinorGrouping = 2, ToolTipText = "")]
	public HullType HullType
	{
		get
		{
			return l8_u0005_;
		}
		set
		{
			if (l8_u0005_ != value)
			{
				l8_u0005_ = value;
				_CollisionId++;
				CalculateWorldBounds(ref l8s, ref l8h, alreadymoved: false);
			}
		}
	}

	[EditorProperty(false)]
	public bool AffectedInCode
	{
		[CompilerGenerated]
		get
		{
			return l85;
		}
		[CompilerGenerated]
		set
		{
			l85 = value;
		}
	}

	public ComponentCollection<ISceneEntity> Components => _Components;

	public event UpdateDelegate UpdateEvent
	{
		add
		{
			UpdateDelegate updateDelegate = l8a;
			UpdateDelegate updateDelegate2;
			do
			{
				updateDelegate2 = updateDelegate;
				UpdateDelegate value2 = (UpdateDelegate)Delegate.Combine(updateDelegate2, value);
				updateDelegate = Interlocked.CompareExchange(ref l8a, value2, updateDelegate2);
			}
			while ((object)updateDelegate != updateDelegate2);
		}
		remove
		{
			UpdateDelegate updateDelegate = l8a;
			UpdateDelegate updateDelegate2;
			do
			{
				updateDelegate2 = updateDelegate;
				UpdateDelegate value2 = (UpdateDelegate)Delegate.Remove(updateDelegate2, value);
				updateDelegate = Interlocked.CompareExchange(ref l8a, value2, updateDelegate2);
			}
			while ((object)updateDelegate != updateDelegate2);
		}
	}

	public event SubmitRemoveManagerDelegate SubmittedToManagerEvent
	{
		add
		{
			SubmitRemoveManagerDelegate submitRemoveManagerDelegate = l8_u0013_;
			SubmitRemoveManagerDelegate submitRemoveManagerDelegate2;
			do
			{
				submitRemoveManagerDelegate2 = submitRemoveManagerDelegate;
				SubmitRemoveManagerDelegate value2 = (SubmitRemoveManagerDelegate)Delegate.Combine(submitRemoveManagerDelegate2, value);
				submitRemoveManagerDelegate = Interlocked.CompareExchange(ref l8_u0013_, value2, submitRemoveManagerDelegate2);
			}
			while ((object)submitRemoveManagerDelegate != submitRemoveManagerDelegate2);
		}
		remove
		{
			SubmitRemoveManagerDelegate submitRemoveManagerDelegate = l8_u0013_;
			SubmitRemoveManagerDelegate submitRemoveManagerDelegate2;
			do
			{
				submitRemoveManagerDelegate2 = submitRemoveManagerDelegate;
				SubmitRemoveManagerDelegate value2 = (SubmitRemoveManagerDelegate)Delegate.Remove(submitRemoveManagerDelegate2, value);
				submitRemoveManagerDelegate = Interlocked.CompareExchange(ref l8_u0013_, value2, submitRemoveManagerDelegate2);
			}
			while ((object)submitRemoveManagerDelegate != submitRemoveManagerDelegate2);
		}
	}

	public event SubmitRemoveManagerDelegate RemovedFromManagerEvent
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

	public SceneEntity(string name, bool infinitebounds)
	{
		Init(name, infinitebounds);
	}

	public SceneEntity()
	{
		Init("", infinitebounds: false);
	}

	protected virtual void Init(string name, bool infinitebounds)
	{
		_Components = new ComponentCollection<ISceneEntity>(this);
		if (l8_u0002_ == 0)
		{
			l8_u0002_ = CoreHelper.GetUniqueId(this);
		}
		l8v = infinitebounds;
		if (!string.IsNullOrEmpty(name))
		{
			l8Y = name;
		}
		else
		{
			l8Y = string.Empty;
		}
		CalculateBounds();
	}

	public virtual ISceneEntity Clone()
	{
		ISceneEntity sceneEntity = Create();
		if (l8v && sceneEntity is SceneEntity sceneEntity2)
		{
			sceneEntity2.l8v = l8v;
			sceneEntity2.CalculateBounds();
		}
		B.L._u0013_B(this, sceneEntity);
		foreach (IComponent<ISceneEntity> component in _Components.Components)
		{
			sceneEntity.Components.Add(component.Clone());
		}
		return sceneEntity;
	}

	protected virtual ISceneEntity Create()
	{
		return (ISceneEntity)Activator.CreateInstance(GetType());
	}

	public virtual void Update(GameTime gametime)
	{
		_Components.OnUpdate(gametime);
		if (l8a != null)
		{
			l8a(this, gametime);
		}
	}

	public virtual void OnSubmittedToManager(IManagerService manager)
	{
		l8l.Add(manager.ManagerType, manager);
		_Components.OnSubmittedToManager(manager);
		if (l8_u0013_ != null)
		{
			l8_u0013_(manager);
		}
	}

	public virtual void OnRemovedFromManager(IManagerService manager)
	{
		_Components.OnRemovedFromManager(manager);
		if (l8N != null)
		{
			l8N(manager);
		}
		l8l.Remove(manager.ManagerType);
	}

	public virtual void OnCreatedInEditor()
	{
	}

	public void SetWorldAndWorldToObject(Matrix world, Matrix worldtoobj)
	{
		if (!l8J.Equals(world))
		{
			UpdateWorldAndWorldToObject(ref world, ref worldtoobj);
		}
	}

	protected virtual void UpdateWorldAndWorldToObject(ref Matrix world, ref Matrix worldtoobj)
	{
		l8J = world;
		l8B = worldtoobj;
		l8L++;
		CalculateWorldBounds(ref l8s, ref l8h, alreadymoved: true);
	}

	public void CalculateBounds()
	{
		CalculateObjectBounds(ref l8I, ref l8b);
		CalculateWorldBounds(ref l8s, ref l8h, alreadymoved: false);
	}

	protected virtual void CalculateObjectBounds(ref BoundingBox objectboundingbox, ref BoundingSphere objectboundingsphere)
	{
		if (l8v)
		{
			float num = 3.4028235E+37f;
			objectboundingbox = new BoundingBox(new Vector3(0f - num), new Vector3(num));
			objectboundingsphere = new BoundingSphere(Vector3.Zero, num);
		}
		else
		{
			objectboundingbox = new BoundingBox(-Vector3.One, Vector3.One);
			objectboundingsphere = new BoundingSphere(Vector3.Zero, 1f);
		}
	}

	protected virtual void CalculateWorldBounds(ref BoundingBox worldboundingbox, ref BoundingSphere worldboundingsphere, bool alreadymoved)
	{
		if (l8v)
		{
			float num = 3.4028235E+37f;
			worldboundingbox = new BoundingBox(new Vector3(0f - num), new Vector3(num));
			worldboundingsphere = new BoundingSphere(Vector3.Zero, num);
			return;
		}
		BoundingBox boundingBox = worldboundingbox;
		if (l8_u0005_ == HullType.Box)
		{
			worldboundingbox = CoreHelper.TransformBoundingBox(l8I, l8J);
			BoundingSphere.CreateFromBoundingBox(ref worldboundingbox, out worldboundingsphere);
		}
		else if (l8_u0005_ == HullType.Sphere)
		{
			CoreHelper.TransformBoundingSphere(ref l8b, ref l8J, out worldboundingsphere);
			BoundingBox.CreateFromSphere(ref worldboundingsphere, out worldboundingbox);
		}
		else
		{
			CoreHelper.TransformBoundingSphere(ref l8b, ref l8J, out worldboundingsphere);
			worldboundingbox = CoreHelper.TransformBoundingBox(l8I, l8J);
		}
		if (!alreadymoved && !boundingBox.Equals(worldboundingbox))
		{
			l8L++;
		}
	}

	public virtual void RenderCustomPass(ISceneState scenestate)
	{
	}

	public virtual void RenderEditorIcon(ISceneState scenestate, BoundingBoxRenderHelper renderhelper, bool highlighted, bool selected, bool sceneoccludedpass)
	{
	}

	internal void _id_7(SerializationInfo param_0)
	{
		SerializationHelper.DeserializeField(ref l8v, param_0, "InfiniteBounds", usedefault: true);
		SerializationHelper.DeserializeField(ref l8Y, param_0, "Name", usedefault: true);
		SerializationHelper.DeserializeField(ref l8_u0002_, param_0, "UniqueId", usedefault: false);
		SerializationHelper.DeserializeEnum(ref l8_u0005_, param_0, "HullType", isflag: false);
		l8w = X.L.L_u0010_(param_0);
		Matrix field = default;
		SerializationHelper.DeserializeField(ref field, param_0, "World", usedefault: true);
		Init(l8Y, l8v);
		World = field;
	}

	public virtual void SetObjectData(SerializationInfo info, StreamingContext context)
	{
		_id_7(info);
		_Components.SetObjectData(info, context);
	}

	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
	public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("InfiniteBounds", l8v);
		info.AddValue("Name", l8Y);
		info.AddValue("UpdateType", l8w);
		info.AddValue("UniqueId", l8_u0002_);
		info.AddValue("World", l8J);
		info.AddValue("HullType", l8_u0005_);
		_Components.GetObjectData(info, context);
	}
}
