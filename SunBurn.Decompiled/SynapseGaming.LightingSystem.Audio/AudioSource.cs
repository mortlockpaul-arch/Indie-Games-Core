using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Threading;
using L;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Editor;
using SynapseGaming.LightingSystem.Lights;
using SynapseGaming.LightingSystem.Rendering;
using SynapseGaming.LightingSystem.Serialization;

namespace SynapseGaming.LightingSystem.Audio;

/// <summary>
/// Provides an audio emitter which is capable of emitting 3D sound from a specific
/// location, or ambient sound heard equally from everywhere in the scene.
/// </summary>
[Serializable]
[EditorCreatedObject]
public class AudioSource : IAudioSource, ISceneEntity, IMovableObject, IWorldBoundingBoxObject, IComponentObject<ISceneEntity>, IEditorCreatedObject<ISceneEntity>, IEditorObject, INamedObject, IEditorRenderableObject, IPointSource, IFullSerializable, ISerializable
{
	private UpdateDelegate fNB;

	private bool fNa = true;

	private bool fNg = true;

	private int fNf;

	private int fN_0003;

	private float fNn = 1f;

	private float fN_0019 = 1f;

	private string fNL = string.Empty;

	private UpdateType fNv;

	private AudioType fN_0012;

	private Matrix fN_0015 = Matrix.Identity;

	private BoundingBox fN8;

	private BoundingSphere fNi;

	private BoundingBox fN_0005;

	private BoundingSphere fNj;

	private SoundEffectAsset fNo = SoundEffectAsset.Empty;

	private ComponentCollection<ISceneEntity> fNc;

	private static Vector3[] fNd = new Vector3[7];

	private static readonly Vector3[] fNw = new Vector3[7]
	{
		new Vector3(0.577f, 0.577f, 0.577f),
		new Vector3(0.577f, -0.577f, 0.577f),
		new Vector3(0.577f, 0.577f, -0.577f),
		new Vector3(0.577f, -0.577f, -0.577f),
		new Vector3(1f, 0f, 0f),
		new Vector3(0f, 1f, 0f),
		new Vector3(0f, 0f, 1f)
	};

	[CompilerGenerated]
	private AudioState fNs;

	[CompilerGenerated]
	private SoundEffect fN_0011;

	[CompilerGenerated]
	private bool fNQ;

	[CompilerGenerated]
	private bool fNG;

	/// <summary>
	/// Determines if the sound will repeat after completing.
	/// </summary>
	[EditorProperty(true, Description = "Looping Sound", HorizontalAlignment = true, MajorGrouping = 4, MinorGrouping = 1, ToolTipText = "")]
	public bool Loop
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
	/// Determines how loud the sound is.
	/// </summary>
	[EditorProperty(true, Description = "Volume", HorizontalAlignment = true, MajorGrouping = 5, MinorGrouping = 1, ToolTipText = "")]
	[EditorNumberPadOptions(2, 0.0, 1.0, 0.05)]
	public float Volume
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
	/// Determines how the sound changes in relationship to the viewer. Ambient sounds
	/// are heard equally from everywhere in the scene, whereas 3D sounds are relative
	/// to the viewer / listener.
	/// </summary>
	[EditorProperty(true, Description = "Audio Type", HorizontalAlignment = true, MajorGrouping = 4, MinorGrouping = 3, ToolTipText = "")]
	public AudioType AudioType
	{
		get
		{
			return fN_0012;
		}
		set
		{
			fN_0012 = value;
			UpdateBounds();
		}
	}

	/// <summary>
	/// Determines if the sound automatically begins playing when the emitter is loaded
	/// as part of a scene. If the sound is not automatically played it will need to be triggered
	/// using the Play() method.
	/// </summary>
	[EditorProperty(true, Description = "Play When Loaded", HorizontalAlignment = true, MajorGrouping = 4, MinorGrouping = 2, ToolTipText = "")]
	public bool PlayWhenLoaded
	{
		get
		{
			return fNg;
		}
		set
		{
			fNg = value;
			if (fNg)
			{
				AudioState = AudioState.Playing;
			}
			else
			{
				AudioState = AudioState.Stopped;
			}
		}
	}

	/// <summary>
	/// Determines if the sound is currently playing.
	/// </summary>
	[EditorProperty(false)]
	public AudioState AudioState
	{
		[CompilerGenerated]
		get
		{
			return fNs;
		}
		[CompilerGenerated]
		set
		{
			fNs = value;
		}
	}

	/// <summary>
	/// The SoundEffect used by the emitter to play sounds. This is either
	/// the sound loaded by the SoundEffectAsset or the sound passed into the constructor
	/// depending on how the object was initialized.
	/// </summary>
	[EditorProperty(false)]
	public SoundEffect SoundEffect
	{
		[CompilerGenerated]
		get
		{
			return fN_0011;
		}
		[CompilerGenerated]
		private set
		{
			fN_0011 = value;
		}
	}

	/// <summary>
	/// Provides direct access to the repository name, file name, and sound
	/// the object was created from. Only valid for serialized objects
	/// created via the SunBurn editor.
	/// </summary>
	[EditorProperty(true, Description = "Sound Effect", HorizontalAlignment = true, MajorGrouping = 1, MinorGrouping = 2, ToolTipText = "")]
	public SoundEffectAsset SoundEffectAsset
	{
		get
		{
			return fNo;
		}
		set
		{
			if (value != null)
			{
				fNo = value;
			}
			else
			{
				fNo = SoundEffectAsset.Empty;
			}
			SoundEffect = fNo.Asset;
		}
	}

	/// <summary>
	/// Position in world space of the source.
	/// </summary>
	[EditorProperty(false)]
	public Vector3 Position
	{
		get
		{
			return World.Translation;
		}
		set
		{
			Matrix world = World;
			world.Translation = value;
			SetWorldAndWorldToObject(world, Matrix.Identity);
		}
	}

	/// <summary>
	/// Maximum distance in world space of the source's influence.
	/// </summary>
	[EditorProperty(true, Description = "Sound Radius", HorizontalAlignment = true, MajorGrouping = 5, MinorGrouping = 2, ToolTipText = "")]
	[EditorNumberPadOptions(2, 0.0, 2147483647.0, 0.25)]
	public float Radius
	{
		get
		{
			return fNn;
		}
		set
		{
			if (fNn != value)
			{
				fNn = value;
				UpdateBounds();
			}
		}
	}

	/// <summary>
	/// Object bounding area of the source's influence.
	/// </summary>
	public BoundingBox ObjectBoundingBox => fN8;

	/// <summary>
	/// Object bounding area of the source's influence.
	/// </summary>
	public BoundingSphere ObjectBoundingSphere => fNi;

	/// <summary>
	/// World bounding area of the source's influence.
	/// </summary>
	public BoundingBox WorldBoundingBox => fN_0005;

	/// <summary>
	/// World bounding area of the source's influence.
	/// </summary>
	public BoundingSphere WorldBoundingSphere => fNj;

	/// <summary>
	/// World space transform of the source.
	/// </summary>
	public Matrix World
	{
		get
		{
			return fN_0015;
		}
		set
		{
			SetWorldAndWorldToObject(value, Matrix.Identity);
		}
	}

	/// <summary>
	/// Indicates the object bounding area spans the entire world and
	/// the object is always visible.
	/// </summary>
	[EditorProperty(false)]
	public bool InfiniteBounds
	{
		[CompilerGenerated]
		get
		{
			return fNQ;
		}
		[CompilerGenerated]
		private set
		{
			fNQ = value;
		}
	}

	/// <summary>
	/// Indicates the current move. This value increments each time the object
	/// is moved (when the World transform changes).
	/// </summary>
	[EditorProperty(false)]
	public int MoveId => fNf;

	/// <summary>
	/// Unique id used to identify the object across multiple scene loads / reloads.
	/// </summary>
	[EditorProperty(false)]
	public int UniqueId => fN_0003;

	/// <summary>
	/// Determines the bounds used for emitter culling (always returns HullType.Box).
	/// </summary>
	[EditorProperty(false)]
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

	/// <summary>
	/// Determines if objects receive update events from the engine and are tracked
	/// by the scenegraph.
	///
	/// Automatic update events are necessary to be affected by gravity, for
	/// components, and for the scenegraph to track moving objects.  Objects without
	/// Automatic update events can still move, however the containing scenegraph
	/// (ObjectManager or LightManager) must be notified using Manager.Move(object).
	/// </summary>
	[EditorProperty(true, Description = "Receives Updates", HorizontalAlignment = true, MajorGrouping = 2, MinorGrouping = 1, ControlType = ControlType.CheckBox)]
	[EditorCheckboxOptions(true)]
	public UpdateType UpdateType
	{
		get
		{
			return fNv;
		}
		set
		{
			fNv = value;
		}
	}

	/// <summary>
	/// The object's current name.
	///
	/// Important note: Name can be changed at any time, HOWEVER managers
	/// will only see the change after removing and resubmitting the object.
	/// </summary>
	[EditorProperty(true, Description = "Name", HorizontalAlignment = true, MajorGrouping = 1, MinorGrouping = 1)]
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
	[EditorProperty(false)]
	public bool AffectedInCode
	{
		[CompilerGenerated]
		get
		{
			return fNG;
		}
		[CompilerGenerated]
		set
		{
			fNG = value;
		}
	}

	/// <summary>
	/// Container that stores, manages, and updates the object's components.
	/// </summary>
	public ComponentCollection<ISceneEntity> Components => fNc;

	/// <summary>
	/// Event used to update the source at regular intervals. This and all
	/// events are only called on automatic source.
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
	/// Creates a new AudioSource instance.
	/// </summary>
	public AudioSource()
		: this(null)
	{
		AudioState = AudioState.Playing;
	}

	/// <summary>
	/// Creates a new AudioSource instance.
	/// </summary>
	/// <param name="sound">The SoundEffect used by the emitter to play sounds.</param>
	public AudioSource(SoundEffect sound)
	{
		SoundEffect = sound;
		AudioState = AudioState.Stopped;
		fNc = new ComponentCollection<ISceneEntity>(this);
		if (fN_0003 == 0)
		{
			fN_0003 = L._0019.@as(this);
		}
		UpdateBounds();
	}

	/// <summary>
	/// Updates the object using the provided game time.
	/// </summary>
	/// <param name="gametime"></param>
	public virtual void Update(GameTime gametime)
	{
		fNc.OnUpdate(gametime);
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
		if (!fN_0015.Equals(world))
		{
			fN_0015 = world;
			UpdateBounds();
		}
	}

	/// <summary>
	/// Recalculates the emitter bounds based on the audio type, position, and radius.
	/// </summary>
	protected virtual void UpdateBounds()
	{
		if (InfiniteBounds = fN_0012 == AudioType.Ambient)
		{
			float num = 3.4028235E+37f;
			fN8 = new BoundingBox(new Vector3(0f - num), new Vector3(num));
			fNi = new BoundingSphere(Vector3.Zero, num);
			fN_0005 = fN8;
			fNj = fNi;
		}
		else
		{
			Vector3 vector = new Vector3(fNn, fNn, fNn);
			Vector3 translation = fN_0015.Translation;
			fN8 = new BoundingBox(-vector, vector);
			fNi = new BoundingSphere(Vector3.Zero, fNn);
			fN_0005 = new BoundingBox(translation - vector, translation + vector);
			fNj = new BoundingSphere(fN_0015.Translation, fNn);
		}
		fNf++;
	}

	/// <summary>
	/// Starts playing the contained sound from the beginning.
	/// </summary>
	public void Play()
	{
		AudioState = AudioState.Playing;
	}

	/// <summary>
	/// Stops playing the contained sound.
	/// </summary>
	public void Stop()
	{
		AudioState = AudioState.Stopped;
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
	public virtual ISceneEntity Clone()
	{
		ISceneEntity sceneEntity = Create();
		L.n.ao(this, sceneEntity);
		foreach (IComponent<ISceneEntity> component in fNc.Components)
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

	/// <summary>
	/// Deserializes object data from the provided SerializationInfo.
	/// </summary>
	/// <param name="info">Contains the serialized object data.</param>
	/// <param name="context"></param>
	public void SetObjectData(SerializationInfo info, StreamingContext context)
	{
		SerializationHelper.DeserializeField(ref fN_0003, info, "UniqueId", usedefault: false);
		SerializationHelper.DeserializeField(ref fNL, info, "Name", usedefault: true);
		SerializationHelper.DeserializeField(ref fNa, info, "Loop", usedefault: true);
		SerializationHelper.DeserializeField(ref fNg, info, "PlayWhenLoaded", usedefault: true);
		SerializationHelper.DeserializeField(ref fN_0019, info, "Volume", usedefault: true);
		SerializationHelper.DeserializeField(ref fNn, info, "Radius", usedefault: true);
		SerializationHelper.DeserializeEnum(ref fN_0012, info, "AudioType", isflag: false);
		SerializationHelper.DeserializeEnum(ref fNv, info, "UpdateType", isflag: false);
		SerializationHelper.DeserializeField(ref fN_0015, info, "World", usedefault: true);
		string field = string.Empty;
		string field2 = string.Empty;
		SerializationHelper.DeserializeField(ref field, info, "ContentRepositoryName", usedefault: true);
		SerializationHelper.DeserializeField(ref field2, info, "SoundEffectFile", usedefault: true);
		SoundEffectAsset = new SoundEffectAsset(field, field2);
		UpdateBounds();
		fNc.SetObjectData(info, context);
		if (fNg)
		{
			AudioState = AudioState.Playing;
		}
	}

	/// <summary>
	/// Serializes object data to the provided SerializationInfo.
	/// </summary>
	/// <param name="info">SerializationInfo to store the serialized data.</param>
	/// <param name="context"></param>
	public void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		SerializationHelper.SerializeFieldOrEnum(ref fN_0003, info, "UniqueId");
		SerializationHelper.SerializeFieldOrEnum(ref fNL, info, "Name");
		SerializationHelper.SerializeFieldOrEnum(ref fNa, info, "Loop");
		SerializationHelper.SerializeFieldOrEnum(ref fNg, info, "PlayWhenLoaded");
		SerializationHelper.SerializeFieldOrEnum(ref fN_0019, info, "Volume");
		SerializationHelper.SerializeFieldOrEnum(ref fNn, info, "Radius");
		SerializationHelper.SerializeFieldOrEnum(ref fN_0012, info, "AudioType");
		SerializationHelper.SerializeFieldOrEnum(ref fNv, info, "UpdateType");
		SerializationHelper.SerializeFieldOrEnum(ref fN_0015, info, "World");
		string field = fNo.ContentRepositoryName;
		string field2 = fNo.SourceAssetFilePath;
		SerializationHelper.SerializeFieldOrEnum(ref field, info, "ContentRepositoryName");
		SerializationHelper.SerializeFieldOrEnum(ref field2, info, "SoundEffectFile");
		fNc.GetObjectData(info, context);
	}
}
