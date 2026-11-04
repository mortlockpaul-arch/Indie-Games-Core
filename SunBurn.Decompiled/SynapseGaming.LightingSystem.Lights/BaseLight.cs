using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;
using L;
using Microsoft.Xna.Framework;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Editor;
using SynapseGaming.LightingSystem.Serialization;
using SynapseGaming.LightingSystem.Shadows;
using W;

namespace SynapseGaming.LightingSystem.Lights;

/// <summary>
/// Abstract class that provides the base properties required for all light types.
/// </summary>
[Serializable]
public abstract class BaseLight : ILight, IMovableObject, IWorldBoundingBoxObject, IComponentObject<ILight>, IEditorCreatedObject<ILight>, IEditorObject, INamedObject, IFullSerializable, ISerializable
{
	private UpdateDelegate fNB;

	private ComponentCollection<ILight> fNa;

	private int fNg;

	private bool fNf = true;

	private UpdateType fN_0003;

	private Vector3 fNn = new Vector3(0.7f, 0.6f, 0.5f);

	private float fN_0019 = 1f;

	private string fNL = "";

	[CompilerGenerated]
	private BoundingBox fNv;

	[CompilerGenerated]
	private BoundingSphere fN_0012;

	[CompilerGenerated]
	private bool fN_0015;

	/// <summary>
	/// Turns illumination on and off without removing the light from the scene.
	/// </summary>
	public bool Enabled
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

	/// <summary>
	/// Determines if the lighting is real-time or bake-down.
	/// </summary>
	public abstract LightingType LightingType { get; set; }

	/// <summary>
	/// Direct lighting color given off by the light.
	/// </summary>
	public Vector3 DiffuseColor
	{
		get
		{
			return fNn;
		}
		set
		{
			fNn = value;
		}
	}

	/// <summary>
	/// Intensity of the light.
	/// </summary>
	public float Intensity
	{
		get
		{
			return fN_0019;
		}
		set
		{
			fN_0019 = value;
		}
	}

	/// <summary>
	/// Provides softer indirect-like illumination without "hot-spots".
	/// </summary>
	public abstract bool FillLight { get; set; }

	/// <summary>
	/// Controls how quickly lighting falls off over distance (only available in deferred rendering).
	/// Value ranges from 0.0f to 1.0f.
	/// </summary>
	public abstract float FalloffStrength { get; set; }

	/// <summary>
	/// The combined light color and intensity (provided for convenience).
	/// </summary>
	public Vector3 CompositeColorAndIntensity => fNn * fN_0019;

	/// <summary>
	/// Bounding area of the light's influence.
	/// </summary>
	public BoundingBox WorldBoundingBox
	{
		[CompilerGenerated]
		get
		{
			return fNv;
		}
		[CompilerGenerated]
		protected set
		{
			fNv = value;
		}
	}

	/// <summary>
	/// Bounding area of the light's influence.
	/// </summary>
	public BoundingSphere WorldBoundingSphere
	{
		[CompilerGenerated]
		get
		{
			return fN_0012;
		}
		[CompilerGenerated]
		protected set
		{
			fN_0012 = value;
		}
	}

	/// <summary>
	/// Shadow source the light's shadows are generated from.
	/// Allows sharing shadows between point light sources.
	/// </summary>
	public abstract IShadowSource ShadowSource { get; set; }

	/// <summary>
	/// World space transform of the light.
	/// </summary>
	public abstract Matrix World { get; set; }

	/// <summary>
	/// Unique id used to identify the object across multiple scene loads / reloads.
	/// </summary>
	public int UniqueId => fNg;

	/// <summary>
	/// Indicates the object bounding area spans the entire world and
	/// the object is always visible.
	/// </summary>
	public abstract bool InfiniteBounds { get; }

	/// <summary>
	/// Indicates the current move. This value increments each time the object
	/// is moved (when the World transform changes).
	/// </summary>
	public abstract int MoveId { get; }

	/// <summary>
	/// Determines if objects receive update events from the engine and are tracked
	/// by the scenegraph.
	///
	/// Automatic update events are necessary to be affected by gravity, for
	/// components, and for the scenegraph to track moving objects.  Objects without
	/// Automatic update events can still move, however the containing scenegraph
	/// (ObjectManager or LightManager) must be notified using Manager.Move(object).
	/// </summary>
	public UpdateType UpdateType
	{
		get
		{
			return fN_0003;
		}
		set
		{
			fN_0003 = value;
		}
	}

	/// <summary>
	/// The object's current name.
	///
	/// Important note: Name can be changed at any time, HOWEVER managers
	/// will only see the change after removing and resubmitting the object.
	/// </summary>
	public string Name
	{
		get
		{
			return fNL;
		}
		set
		{
			fNL = value;
		}
	}

	/// <summary>
	/// Notifies the editor that this object is partially controlled via code. The editor
	/// will display information to the user indicating some property values are
	/// overridden in code and changes may not take effect.
	/// </summary>
	public bool AffectedInCode
	{
		[CompilerGenerated]
		get
		{
			return fN_0015;
		}
		[CompilerGenerated]
		set
		{
			fN_0015 = value;
		}
	}

	/// <summary>
	/// Container that stores, manages, and updates the object's components.
	/// </summary>
	public ComponentCollection<ILight> Components => fNa;

	/// <summary>
	/// Event used to update the light at regular intervals. This and all
	/// events are only called on dynamic lights.
	/// </summary>
	public event UpdateDelegate UpdateEvent
	{
		add
		{
			UpdateDelegate updateDelegate = fNB;
			UpdateDelegate updateDelegate2;
			do
			{
				updateDelegate2 = updateDelegate;
				UpdateDelegate value2 = (UpdateDelegate)Delegate.Combine(updateDelegate2, value);
				updateDelegate = Interlocked.CompareExchange(ref fNB, value2, updateDelegate2);
			}
			while ((object)updateDelegate != updateDelegate2);
		}
		remove
		{
			UpdateDelegate updateDelegate = fNB;
			UpdateDelegate updateDelegate2;
			do
			{
				updateDelegate2 = updateDelegate;
				UpdateDelegate value2 = (UpdateDelegate)Delegate.Remove(updateDelegate2, value);
				updateDelegate = Interlocked.CompareExchange(ref fNB, value2, updateDelegate2);
			}
			while ((object)updateDelegate != updateDelegate2);
		}
	}

	/// <summary>
	/// Creates a new BaseLight instance.
	/// </summary>
	public BaseLight()
	{
		fNa = new ComponentCollection<ILight>(this);
		if (fNg == 0)
		{
			fNg = L._0019.@as(this);
		}
	}

	/// <summary>
	/// Updates the object using the provided game time.
	/// </summary>
	/// <param name="gametime"></param>
	public virtual void Update(GameTime gametime)
	{
		fNa.OnUpdate(gametime);
		if (fNB != null)
		{
			fNB(this, gametime);
		}
	}

	/// <summary>
	/// Returns a String that represents the current Object.
	/// </summary>
	/// <returns></returns>
	public override string ToString()
	{
		return L._0019.aG(this);
	}

	/// <summary>
	/// Deep clones the object including any contained sub-objects and components.
	/// </summary>
	/// <returns></returns>
	public virtual ILight Clone()
	{
		ILight light = Create();
		L.n.ao(this, light);
		foreach (IComponent<ILight> component in fNa.Components)
		{
			light.Components.Add(component.Clone());
		}
		return light;
	}

	/// <summary>
	/// Creates a new instance of the object type. This method assumes the type has a
	/// default constructor. If the type does not have a default constructor this method
	/// can be overridden to manually create the type.
	/// </summary>
	/// <returns></returns>
	protected virtual ILight Create()
	{
		return (ILight)Activator.CreateInstance(GetType());
	}

	/// <summary>
	/// Deserializes object data from the provided SerializationInfo.
	/// </summary>
	/// <param name="info">Contains the serialized object data.</param>
	/// <param name="context"></param>
	public virtual void SetObjectData(SerializationInfo info, StreamingContext context)
	{
		SerializationHelper.DeserializeField(ref fNg, info, "UniqueId", usedefault: false);
		SerializationHelper.DeserializeField(ref fNf, info, "Enabled", usedefault: true);
		SerializationHelper.DeserializeField(ref fNn, info, "DiffuseColor", usedefault: true);
		SerializationHelper.DeserializeField(ref fN_0019, info, "Intensity", usedefault: true);
		SerializationHelper.DeserializeField(ref fNL, info, "Name", usedefault: true);
		fN_0003 = W._0003.n8(info);
		LightingType field = LightingType;
		SerializationHelper.DeserializeEnum(ref field, info, "LightingType", isflag: true);
		LightingType = field;
		FillLight = SerializationHelper.DeserializeField<bool>(info, "FillLight");
		FalloffStrength = SerializationHelper.DeserializeField<float>(info, "FalloffStrength");
		fNa.SetObjectData(info, context);
	}

	/// <summary>
	/// Serializes object data to the provided SerializationInfo.
	/// </summary>
	/// <param name="info">SerializationInfo to store the serialized data.</param>
	/// <param name="context"></param>
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
		fNa.GetObjectData(info, context);
	}
}
