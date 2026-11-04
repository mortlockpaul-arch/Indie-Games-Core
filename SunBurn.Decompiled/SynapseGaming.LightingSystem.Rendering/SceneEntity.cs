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
using W;

namespace SynapseGaming.LightingSystem.Rendering;

/// <summary>
/// Scene entity implementation used to derive custom entities and scene objects.
/// </summary>
[Serializable]
[EditorCreatedObject]
public class SceneEntity : ISceneEntity, IMovableObject, IWorldBoundingBoxObject, IComponentObject<ISceneEntity>, IEditorCreatedObject<ISceneEntity>, IEditorObject, INamedObject, IEditorRenderableObject, IFullSerializable, ISerializable
{
	private UpdateDelegate fNB;

	private int fNa;

	private UpdateType fNg;

	private Matrix fNf = Matrix.Identity;

	private Matrix fN_0003 = Matrix.Identity;

	private BoundingBox fNn = default;

	private BoundingSphere fN_0019 = default;

	private int fNL;

	private bool fNv;

	private string fN_0012;

	private HullType fN_0015 = HullType.Box;

	private BoundingSphere fN8;

	private BoundingBox fNi;

	/// <summary />
	protected ComponentCollection<ISceneEntity> _Components;

	[CompilerGenerated]
	private bool fN_0005;

	/// <summary>
	/// Unique id used to identify the object across multiple scene loads / reloads.
	/// </summary>
	[EditorProperty(false)]
	public int UniqueId
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
	/// World space transform of the object.
	/// </summary>
	[EditorProperty(false)]
	public Matrix World
	{
		get
		{
			return fNf;
		}
		set
		{
			if (!fNf.Equals(value))
			{
				SetWorldAndWorldToObject(value, Matrix.Invert(value));
			}
		}
	}

	/// <summary>
	/// Inverse world space transform of the object.
	/// </summary>
	public Matrix WorldToObject => fN_0003;

	/// <summary>
	/// Indicates the object bounding area spans the entire world and
	/// the object is always visible.
	/// </summary>
	public bool InfiniteBounds => fNv;

	/// <summary>
	/// Indicates the current move. This value increments each time the object
	/// is moved (when the World transform changes).
	/// </summary>
	[EditorProperty(false)]
	public int MoveId
	{
		get
		{
			return fNa;
		}
		set
		{
			fNa = value;
		}
	}

	/// <summary>
	/// Determines if objects receive update events from the engine and are tracked
	/// by the scenegraph.
	///
	/// Automatic update events are necessary to be affected by gravity, for
	/// components, and for the scenegraph to track moving objects.  Objects without
	/// Automatic update events can still move, however the containing scenegraph
	/// (ObjectManager or LightManager) must be notified using Manager.Move(object).
	/// </summary>
	[EditorProperty(true, Description = "Receives Updates", HorizontalAlignment = true, MajorGrouping = 2, MinorGrouping = 1, ToolTipText = "", ControlType = ControlType.CheckBox)]
	[EditorCheckboxOptions(true)]
	public UpdateType UpdateType
	{
		get
		{
			return fNg;
		}
		set
		{
			fNg = value;
		}
	}

	/// <summary>
	/// Object space bounding area of the object.
	/// </summary>
	public BoundingSphere ObjectBoundingSphere => fN8;

	/// <summary>
	/// Object space bounding area of the object.
	/// </summary>
	public BoundingBox ObjectBoundingBox => fNi;

	/// <summary>
	/// World space bounding area of the object.
	/// </summary>
	public BoundingBox WorldBoundingBox => fNn;

	/// <summary>
	/// World space bounding area of the object.
	/// </summary>
	public BoundingSphere WorldBoundingSphere => fN_0019;

	/// <summary>
	/// The object's current name.
	///
	/// Important note: Name can be changed at any time, HOWEVER managers
	/// will only see the change after removing and resubmitting the object.
	/// </summary>
	[EditorProperty(true, Description = "Name", HorizontalAlignment = true, MajorGrouping = 1, MinorGrouping = 1, ToolTipText = "")]
	public string Name
	{
		get
		{
			return fN_0012;
		}
		set
		{
			fN_0012 = value;
		}
	}

	/// <summary>
	/// Determines the bounds used in object culling and collision.
	/// </summary>
	[EditorProperty(true, Description = "Hull Type", HorizontalAlignment = true, MajorGrouping = 5, MinorGrouping = 2, ToolTipText = "")]
	public HullType HullType
	{
		get
		{
			return fN_0015;
		}
		set
		{
			if (fN_0015 != value)
			{
				fN_0015 = value;
				CalculateWorldBounds(ref fNn, ref fN_0019, alreadymoved: false);
			}
		}
	}

	/// <summary>
	/// Notifies the editor that this object is partially controlled via code. The editor
	/// will display information to the user indicating some property values are
	/// overridden in code and changes may not take effect.
	/// </summary>
	[EditorProperty(false)]
	public bool AffectedInCode
	{
		[CompilerGenerated]
		get
		{
			return fN_0005;
		}
		[CompilerGenerated]
		set
		{
			fN_0005 = value;
		}
	}

	/// <summary>
	/// Container that stores, manages, and updates the object's components.
	/// </summary>
	public ComponentCollection<ISceneEntity> Components => _Components;

	/// <summary>
	/// Event used to update the object at regular intervals. This and all
	/// events are only called on dynamic objects.
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
	/// Creates a new SceneEntity instance.
	/// </summary>
	/// <param name="name">Custom name for the object.</param>
	/// <param name="infinitebounds">Indicates the object bounding area spans the entire world and
	/// the object is always visible.</param>
	public SceneEntity(string name, bool infinitebounds)
	{
		Init(name, infinitebounds);
	}

	/// <summary>
	/// Creates a new SceneEntity instance.
	/// </summary>
	public SceneEntity()
	{
		Init("", infinitebounds: false);
	}

	/// <summary>
	/// Initializes the object to default values.
	/// </summary>
	/// <param name="name">Custom name for the object.</param>
	/// <param name="infinitebounds">Indicates the object bounding area spans the entire world and
	/// the object is always visible.</param>
	protected virtual void Init(string name, bool infinitebounds)
	{
		_Components = new ComponentCollection<ISceneEntity>(this);
		if (fNL == 0)
		{
			fNL = L._0019.@as(this);
		}
		fNv = infinitebounds;
		if (!string.IsNullOrEmpty(name))
		{
			fN_0012 = name;
		}
		else
		{
			fN_0012 = string.Empty;
		}
		CalculateBounds();
	}

	/// <summary>
	/// Deep clones the object including any contained sub-objects and components.
	/// </summary>
	/// <returns></returns>
	public virtual ISceneEntity Clone()
	{
		ISceneEntity sceneEntity = Create();
		L.n.ao(this, sceneEntity);
		foreach (IComponent<ISceneEntity> component in _Components.Components)
		{
			sceneEntity.Components.Add(component.Clone());
		}
		return sceneEntity;
	}

	/// <summary>
	/// Creates a new instance of the object type. This method assumes the type has a
	/// default constructor. If the type does not have a default constructor this method
	/// can be overridden to manually create the type.
	/// </summary>
	/// <returns></returns>
	protected virtual ISceneEntity Create()
	{
		return (ISceneEntity)Activator.CreateInstance(GetType());
	}

	/// <summary>
	/// Updates the object using the provided game time.
	/// </summary>
	/// <param name="gametime"></param>
	public virtual void Update(GameTime gametime)
	{
		_Components.OnUpdate(gametime);
		if (fNB != null)
		{
			fNB(this, gametime);
		}
	}

	/// <summary>
	/// Sets both the world and inverse world matrices.  Used to improve
	/// performance when the world matrix is set, by providing a cached
	/// or precalculated inverse matrix with the world matrix.
	/// </summary>
	/// <param name="world">World space transform of the object.</param>
	/// <param name="worldtoobj">Inverse world space transform of the object.</param>
	public void SetWorldAndWorldToObject(Matrix world, Matrix worldtoobj)
	{
		if (!fNf.Equals(world))
		{
			UpdateWorldAndWorldToObject(ref world, ref worldtoobj);
		}
	}

	/// <summary>
	/// Updates the object world space and inverse world space transforms.
	/// Override to perform custom code when the world transform changes.
	/// </summary>
	/// <param name="world">World space transform.</param>
	/// <param name="worldtoobj">Inverse world space transform.</param>
	protected virtual void UpdateWorldAndWorldToObject(ref Matrix world, ref Matrix worldtoobj)
	{
		fNf = world;
		fN_0003 = worldtoobj;
		fNa++;
		CalculateWorldBounds(ref fNn, ref fN_0019, alreadymoved: true);
	}

	/// <summary>
	/// Recalculates the object bounding area based on all contained meshes.
	///
	/// Calling this method may become necessary if a mesh bounding area is
	/// altered after being added to the object.
	/// </summary>
	public void CalculateBounds()
	{
		CalculateObjectBounds(ref fNi, ref fN8);
		CalculateWorldBounds(ref fNn, ref fN_0019, alreadymoved: false);
	}

	/// <summary>
	/// Calculates the object bounds.
	/// </summary>
	/// <param name="objectboundingbox">Object bounds to update.</param>
	/// <param name="objectboundingsphere">Object bounds to update.</param>
	protected virtual void CalculateObjectBounds(ref BoundingBox objectboundingbox, ref BoundingSphere objectboundingsphere)
	{
		if (fNv)
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

	/// <summary>
	/// Updates the object world bounds based on the current world transform and object space bounds.
	///
	/// NOTE: when implementing custom bounds ensure the hull type (box or sphere) is completely
	/// enclosed by the other bounds type. For instance if the hull type is Box then the bounding
	/// sphere should completely contain the bounding box, and vice-versa. This is critical for
	/// correct collision.
	/// </summary>
	/// <param name="worldboundingbox">World bounds to update.</param>
	/// <param name="worldboundingsphere">World bounds to update.</param>
	/// <param name="alreadymoved">Indicates the object move id is already updated.</param>
	protected virtual void CalculateWorldBounds(ref BoundingBox worldboundingbox, ref BoundingSphere worldboundingsphere, bool alreadymoved)
	{
		if (fNv)
		{
			float num = 3.4028235E+37f;
			worldboundingbox = new BoundingBox(new Vector3(0f - num), new Vector3(num));
			worldboundingsphere = new BoundingSphere(Vector3.Zero, num);
			return;
		}
		BoundingBox boundingBox = worldboundingbox;
		if (fN_0015 == HullType.Box)
		{
			worldboundingbox = L._0019.ah(fNi, fNf);
			BoundingSphere.CreateFromBoundingBox(ref worldboundingbox, out worldboundingsphere);
		}
		else if (fN_0015 == HullType.Sphere)
		{
			L._0019.aW(ref fN8, ref fNf, out worldboundingsphere);
			BoundingBox.CreateFromSphere(ref worldboundingsphere, out worldboundingbox);
		}
		else
		{
			L._0019.aW(ref fN8, ref fNf, out worldboundingsphere);
			worldboundingbox = L._0019.ah(fNi, fNf);
		}
		if (!alreadymoved && !boundingBox.Equals(worldboundingbox))
		{
			fNa++;
		}
	}

	/// <summary>
	/// Implements a custom rendering pass. The pass occurs after scene rendering completes, but before post processing.
	/// </summary>
	/// <param name="scenestate">Current state used to render the scene.</param>
	public virtual void RenderCustomPass(ISceneState scenestate)
	{
	}

	/// <summary>
	/// Implements rendering of in-editor icons and helpers.
	///
	/// This method is called twice per-frame: once with scene depth clipping enable, and once with it disabled.
	/// </summary>
	/// <param name="scenestate">Current state used to render the scene.</param>
	/// <param name="renderhelper">Helper used to draw lines associated with the object. Only calling Submit() is
	/// supported in this method, using other methods may affect rendering of lines drawn by other objects.</param>
	/// <param name="highlighted">Indicates if the object is currently highlighted by the editor.</param>
	/// <param name="selected">Indicates if the object is currently selected by the editor.</param>
	/// <param name="sceneoccludedpass">Indicates if the current rendering pass depth clips with the scene.
	/// If so rendered icons and helpers are occluded by scene objects.</param>
	public virtual void RenderEditorIcon(ISceneState scenestate, BoundingBoxRenderHelper renderhelper, bool highlighted, bool selected, bool sceneoccludedpass)
	{
	}

	internal void _0003S(SerializationInfo P_0)
	{
		SerializationHelper.DeserializeField(ref fNv, P_0, "InfiniteBounds", usedefault: true);
		SerializationHelper.DeserializeField(ref fN_0012, P_0, "Name", usedefault: true);
		SerializationHelper.DeserializeField(ref fNL, P_0, "UniqueId", usedefault: false);
		SerializationHelper.DeserializeEnum(ref fN_0015, P_0, "HullType", isflag: false);
		fNg = W._0003.n8(P_0);
		Matrix field = default;
		SerializationHelper.DeserializeField(ref field, P_0, "World", usedefault: true);
		Init(fN_0012, fNv);
		World = field;
	}

	/// <summary>
	/// Deserializes object data from the provided SerializationInfo.
	/// </summary>
	/// <param name="info">Contains the serialized object data.</param>
	/// <param name="context"></param>
	public virtual void SetObjectData(SerializationInfo info, StreamingContext context)
	{
		_0003S(info);
		_Components.SetObjectData(info, context);
	}

	/// <summary>
	/// Serializes object data to the provided SerializationInfo.
	/// </summary>
	/// <param name="info">SerializationInfo to store the serialized data.</param>
	/// <param name="context"></param>
	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
	public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("InfiniteBounds", fNv);
		info.AddValue("Name", fN_0012);
		info.AddValue("UpdateType", fNg);
		info.AddValue("UniqueId", fNL);
		info.AddValue("World", fNf);
		info.AddValue("HullType", fN_0015);
		_Components.GetObjectData(info, context);
	}
}
