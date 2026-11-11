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
using SynapseGaming.LightingSystem.Shadows;
using X;

namespace SynapseGaming.LightingSystem.Lights;

[Serializable]
public abstract class BaseLight : ILight, IMovableObject, IWorldBoundingBoxObject, IComponentObject<ILight>, IEditorCreatedObject<ILight>, IEditorObject, INamedObject, IFullSerializable, ISerializable
{
	private UpdateDelegate l8a;

	private SubmitRemoveManagerDelegate l8_u0013_;

	private SubmitRemoveManagerDelegate l8N;

	private ComponentCollection<ILight> l8l;

	private TypeDictionary<IManagerService> l8L = new TypeDictionary<IManagerService>();

	private int l8w;

	private bool l8J = true;

	private UpdateType l8B;

	private Vector3 l8s = new Vector3(0.7f, 0.6f, 0.5f);

	private float l8h = 1f;

	private string l8_u0002_ = "";

	[CompilerGenerated]
	private BoundingBox l8v;

	[CompilerGenerated]
	private BoundingSphere l8Y;

	[CompilerGenerated]
	private bool l8_u0005_;

	public TypeDictionary<IManagerService> ContainingManagers => l8L;

	public bool Enabled
	{
		get
		{
			return l8J;
		}
		set
		{
			l8J = value;
		}
	}

	public abstract LightingType LightingType { get; set; }

	public Vector3 DiffuseColor
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

	public float Intensity
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

	public abstract bool FillLight { get; set; }

	public abstract float FalloffStrength { get; set; }

	public Vector3 CompositeColorAndIntensity => l8s * l8h;

	public BoundingBox WorldBoundingBox
	{
		[CompilerGenerated]
		get
		{
			return l8v;
		}
		[CompilerGenerated]
		protected set
		{
			l8v = value;
		}
	}

	public BoundingSphere WorldBoundingSphere
	{
		[CompilerGenerated]
		get
		{
			return l8Y;
		}
		[CompilerGenerated]
		protected set
		{
			l8Y = value;
		}
	}

	public abstract IShadowSource ShadowSource { get; set; }

	public abstract Matrix World { get; set; }

	public int UniqueId => l8w;

	public abstract bool InfiniteBounds { get; }

	public abstract int MoveId { get; }

	public UpdateType UpdateType
	{
		get
		{
			return l8B;
		}
		set
		{
			l8B = value;
		}
	}

	public string Name
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

	public bool AffectedInCode
	{
		[CompilerGenerated]
		get
		{
			return l8_u0005_;
		}
		[CompilerGenerated]
		set
		{
			l8_u0005_ = value;
		}
	}

	public ComponentCollection<ILight> Components => l8l;

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

	public BaseLight()
	{
		l8l = new ComponentCollection<ILight>(this);
		if (l8w == 0)
		{
			l8w = CoreHelper.GetUniqueId(this);
		}
	}

	public virtual void Update(GameTime gametime)
	{
		l8l.OnUpdate(gametime);
		if (l8a != null)
		{
			l8a(this, gametime);
		}
	}

	public virtual void OnSubmittedToManager(IManagerService manager)
	{
		l8L.Add(manager.ManagerType, manager);
		l8l.OnSubmittedToManager(manager);
		if (l8_u0013_ != null)
		{
			l8_u0013_(manager);
		}
	}

	public virtual void OnRemovedFromManager(IManagerService manager)
	{
		l8l.OnRemovedFromManager(manager);
		if (l8N != null)
		{
			l8N(manager);
		}
		l8L.Remove(manager.ManagerType);
	}

	public virtual void OnCreatedInEditor()
	{
	}

	public override string ToString()
	{
		return CoreHelper.GetDisplayName(this);
	}

	public virtual ILight Clone()
	{
		ILight light = Create();
		B.L._u0013_B(this, light);
		foreach (IComponent<ILight> component in l8l.Components)
		{
			light.Components.Add(component.Clone());
		}
		return light;
	}

	protected virtual ILight Create()
	{
		return (ILight)Activator.CreateInstance(GetType());
	}

	public virtual void SetObjectData(SerializationInfo info, StreamingContext context)
	{
		SerializationHelper.DeserializeField(ref l8w, info, "UniqueId", usedefault: false);
		SerializationHelper.DeserializeField(ref l8J, info, "Enabled", usedefault: true);
		SerializationHelper.DeserializeField(ref l8s, info, "DiffuseColor", usedefault: true);
		SerializationHelper.DeserializeField(ref l8h, info, "Intensity", usedefault: true);
		SerializationHelper.DeserializeField(ref l8_u0002_, info, "Name", usedefault: true);
		l8B = X.L.L_u0010_(info);
		LightingType field = LightingType;
		SerializationHelper.DeserializeEnum(ref field, info, "LightingType", isflag: true);
		LightingType = field;
		FillLight = SerializationHelper.DeserializeField<bool>(info, "FillLight");
		FalloffStrength = SerializationHelper.DeserializeField<float>(info, "FalloffStrength");
		l8l.SetObjectData(info, context);
	}

	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
	public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("UniqueId", UniqueId);
		info.AddValue("Name", Name);
		info.AddValue("UpdateType", UpdateType);
		info.AddValue("Enabled", Enabled);
		info.AddValue("LightingType", LightingType);
		info.AddValue("DiffuseColor", DiffuseColor);
		info.AddValue("Intensity", Intensity);
		info.AddValue("FillLight", FillLight);
		info.AddValue("FalloffStrength", FalloffStrength);
		l8l.GetObjectData(info, context);
	}
}
