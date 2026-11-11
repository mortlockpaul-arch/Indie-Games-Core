using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Threading;
using B;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using SynapseGaming.LightingSystem.Components;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Editor;
using SynapseGaming.LightingSystem.Lights;
using SynapseGaming.LightingSystem.Rendering;
using SynapseGaming.LightingSystem.Serialization;

namespace SynapseGaming.LightingSystem.Audio;

[Serializable]
[EditorCreatedObject]
public class AudioSource : IAudioSource, ISceneEntity, IMovableObject, IWorldBoundingBoxObject, IComponentObject<ISceneEntity>, IEditorCreatedObject<ISceneEntity>, IEditorObject, INamedObject, IEditorRenderableObject, IPointSource, IFullSerializable, ISerializable
{
	private UpdateDelegate l8a;

	private SubmitRemoveManagerDelegate l8_u0013_;

	private SubmitRemoveManagerDelegate l8N;

	private TypeDictionary<IManagerService> l8l = new TypeDictionary<IManagerService>();

	private bool l8L = true;

	private bool l8w = true;

	private int l8J;

	private int l8B;

	private float l8s = 1f;

	private float l8h = 1f;

	private float l8_u0002_ = 1f;

	private string l8v = string.Empty;

	private UpdateType l8Y;

	private AudioType l8_u0005_;

	private Matrix l8b = Matrix.Identity;

	private BoundingBox l8I;

	private BoundingSphere l85;

	private BoundingBox l8W;

	private BoundingSphere l8Z;

	private SoundEffectAsset l8T = SoundEffectAsset.Empty;

	private ComponentCollection<ISceneEntity> l8_u0014_;

	private static Vector3[] l8E = new Vector3[7];

	private static readonly Vector3[] l8f = new Vector3[7]
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
	private AudioState l8_u0016_;

	[CompilerGenerated]
	private SoundEffect l86;

	[CompilerGenerated]
	private bool l8x;

	[CompilerGenerated]
	private bool l8_u0004_;

	public TypeDictionary<IManagerService> ContainingManagers => l8l;

	[EditorProperty(true, Description = "Looping Sound", HorizontalAlignment = true, MajorGrouping = 4, MinorGrouping = 1, ToolTipText = "")]
	public bool Loop
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

	[EditorProperty(true, Description = "Volume", HorizontalAlignment = true, MajorGrouping = 5, MinorGrouping = 1, ToolTipText = "")]
	[EditorNumberPadOptions(2, 0.0, 1.0, 0.05)]
	public float Volume
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

	[EditorProperty(true, Description = "Doppler Scale", HorizontalAlignment = true, MajorGrouping = 5, MinorGrouping = 3, ToolTipText = "")]
	[EditorNumberPadOptions(2, 0.0, 10000.0, 0.05)]
	public float DopplerScale
	{
		get
		{
			return l8_u0002_;
		}
		set
		{
			l8_u0002_ = Math.Max(value, 0f);
		}
	}

	[EditorProperty(true, Description = "Audio Type", HorizontalAlignment = true, MajorGrouping = 4, MinorGrouping = 3, ToolTipText = "")]
	public AudioType AudioType
	{
		get
		{
			return l8_u0005_;
		}
		set
		{
			l8_u0005_ = value;
			UpdateBounds();
		}
	}

	[EditorProperty(true, Description = "Play When Loaded", HorizontalAlignment = true, MajorGrouping = 4, MinorGrouping = 2, ToolTipText = "")]
	public bool PlayWhenLoaded
	{
		get
		{
			return l8w;
		}
		set
		{
			l8w = value;
			if (l8w)
			{
				AudioState = AudioState.Playing;
			}
			else
			{
				AudioState = AudioState.Stopped;
			}
		}
	}

	[EditorProperty(false)]
	public AudioState AudioState
	{
		[CompilerGenerated]
		get
		{
			return l8_u0016_;
		}
		[CompilerGenerated]
		set
		{
			l8_u0016_ = value;
		}
	}

	[EditorProperty(false)]
	public SoundEffect SoundEffect
	{
		[CompilerGenerated]
		get
		{
			return l86;
		}
		[CompilerGenerated]
		private set
		{
			l86 = value;
		}
	}

	[EditorProperty(true, Description = "Sound Effect", HorizontalAlignment = true, MajorGrouping = 1, MinorGrouping = 2, ToolTipText = "")]
	public SoundEffectAsset SoundEffectAsset
	{
		get
		{
			return l8T;
		}
		set
		{
			if (value != null)
			{
				l8T = value;
			}
			else
			{
				l8T = SoundEffectAsset.Empty;
			}
			SoundEffect = l8T.Asset;
		}
	}

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

	[EditorNumberPadOptions(2, 0.0, 2147483647.0, 0.25)]
	[EditorProperty(true, Description = "Sound Radius", HorizontalAlignment = true, MajorGrouping = 5, MinorGrouping = 2, ToolTipText = "")]
	public float Radius
	{
		get
		{
			return l8s;
		}
		set
		{
			if (l8s != value)
			{
				l8s = value;
				UpdateBounds();
			}
		}
	}

	public BoundingBox ObjectBoundingBox => l8I;

	public BoundingSphere ObjectBoundingSphere => l85;

	public BoundingBox WorldBoundingBox => l8W;

	public BoundingSphere WorldBoundingSphere => l8Z;

	public Matrix World
	{
		get
		{
			return l8b;
		}
		set
		{
			SetWorldAndWorldToObject(value, Matrix.Identity);
		}
	}

	[EditorProperty(false)]
	public bool InfiniteBounds
	{
		[CompilerGenerated]
		get
		{
			return l8x;
		}
		[CompilerGenerated]
		private set
		{
			l8x = value;
		}
	}

	[EditorProperty(false)]
	public int MoveId => l8J;

	[EditorProperty(false)]
	public int UniqueId => l8B;

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

	[EditorProperty(true, Description = "Receives Updates", HorizontalAlignment = true, MajorGrouping = 2, MinorGrouping = 1, ControlType = ControlType.CheckBox)]
	[EditorCheckboxOptions(true)]
	public UpdateType UpdateType
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

	[EditorProperty(true, Description = "Name", HorizontalAlignment = true, MajorGrouping = 1, MinorGrouping = 1)]
	public string Name
	{
		get
		{
			return l8v;
		}
		set
		{
			l8v = value;
		}
	}

	[EditorProperty(false)]
	public bool AffectedInCode
	{
		[CompilerGenerated]
		get
		{
			return l8_u0004_;
		}
		[CompilerGenerated]
		set
		{
			l8_u0004_ = value;
		}
	}

	public ComponentCollection<ISceneEntity> Components => l8_u0014_;

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

	public AudioSource()
		: this(null)
	{
		AudioState = AudioState.Playing;
	}

	public AudioSource(SoundEffect sound)
	{
		SoundEffect = sound;
		AudioState = AudioState.Stopped;
		l8_u0014_ = new ComponentCollection<ISceneEntity>(this);
		if (l8B == 0)
		{
			l8B = CoreHelper.GetUniqueId(this);
		}
		UpdateBounds();
	}

	public virtual void Update(GameTime gametime)
	{
		l8_u0014_.OnUpdate(gametime);
		if (l8a != null)
		{
			l8a(this, gametime);
		}
	}

	public virtual void OnSubmittedToManager(IManagerService manager)
	{
		l8l.Add(manager.ManagerType, manager);
		l8_u0014_.OnSubmittedToManager(manager);
		if (l8_u0013_ != null)
		{
			l8_u0013_(manager);
		}
	}

	public virtual void OnRemovedFromManager(IManagerService manager)
	{
		l8_u0014_.OnRemovedFromManager(manager);
		if (l8N != null)
		{
			l8N(manager);
		}
		l8l.Remove(manager.ManagerType);
	}

	public virtual void OnCreatedInEditor()
	{
	}

	public virtual void SetWorldAndWorldToObject(Matrix world, Matrix worldtoobj)
	{
		if (!l8b.Equals(world))
		{
			l8b = world;
			UpdateBounds();
		}
	}

	protected virtual void UpdateBounds()
	{
		if (InfiniteBounds = l8_u0005_ == AudioType.Ambient)
		{
			float num = 3.4028235E+37f;
			l8I = new BoundingBox(new Vector3(0f - num), new Vector3(num));
			l85 = new BoundingSphere(Vector3.Zero, num);
			l8W = l8I;
			l8Z = l85;
		}
		else
		{
			Vector3 vector = new Vector3(l8s, l8s, l8s);
			Vector3 translation = l8b.Translation;
			l8I = new BoundingBox(-vector, vector);
			l85 = new BoundingSphere(Vector3.Zero, l8s);
			l8W = new BoundingBox(translation - vector, translation + vector);
			l8Z = new BoundingSphere(l8b.Translation, l8s);
		}
		l8J++;
	}

	public virtual void Play()
	{
		AudioState = AudioState.Playing;
	}

	public virtual void Stop()
	{
		AudioState = AudioState.Stopped;
	}

	public override string ToString()
	{
		return CoreHelper.GetDisplayName(this);
	}

	public virtual ISceneEntity Clone()
	{
		ISceneEntity sceneEntity = Create();
		B.L._u0013_B(this, sceneEntity);
		foreach (IComponent<ISceneEntity> component in l8_u0014_.Components)
		{
			sceneEntity.Components.Add(component.Clone());
		}
		return sceneEntity;
	}

	protected virtual ISceneEntity Create()
	{
		return (ISceneEntity)Activator.CreateInstance(GetType());
	}

	public virtual void RenderCustomPass(ISceneState scenestate)
	{
	}

	public virtual void RenderEditorIcon(ISceneState scenestate, BoundingBoxRenderHelper renderhelper, bool highlighted, bool selected, bool sceneoccludedpass)
	{
	}

	public virtual void SetObjectData(SerializationInfo info, StreamingContext context)
	{
		SerializationHelper.DeserializeField(ref l8B, info, "UniqueId", usedefault: false);
		SerializationHelper.DeserializeField(ref l8v, info, "Name", usedefault: true);
		SerializationHelper.DeserializeField(ref l8L, info, "Loop", usedefault: true);
		SerializationHelper.DeserializeField(ref l8w, info, "PlayWhenLoaded", usedefault: true);
		SerializationHelper.DeserializeField(ref l8h, info, "Volume", usedefault: true);
		SerializationHelper.DeserializeField(ref l8s, info, "Radius", usedefault: true);
		SerializationHelper.DeserializeField(ref l8_u0002_, info, "DopplerScale", usedefault: true);
		SerializationHelper.DeserializeEnum(ref l8_u0005_, info, "AudioType", isflag: false);
		SerializationHelper.DeserializeEnum(ref l8Y, info, "UpdateType", isflag: false);
		SerializationHelper.DeserializeField(ref l8b, info, "World", usedefault: true);
		string field = string.Empty;
		string field2 = string.Empty;
		SerializationHelper.DeserializeField(ref field, info, "ContentRepositoryName", usedefault: true);
		SerializationHelper.DeserializeField(ref field2, info, "SoundEffectFile", usedefault: true);
		SoundEffectAsset = new SoundEffectAsset(field, field2);
		UpdateBounds();
		l8_u0014_.SetObjectData(info, context);
		if (l8w)
		{
			AudioState = AudioState.Playing;
		}
	}

	public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		SerializationHelper.SerializeFieldOrEnum(ref l8B, info, "UniqueId");
		SerializationHelper.SerializeFieldOrEnum(ref l8v, info, "Name");
		SerializationHelper.SerializeFieldOrEnum(ref l8L, info, "Loop");
		SerializationHelper.SerializeFieldOrEnum(ref l8w, info, "PlayWhenLoaded");
		SerializationHelper.SerializeFieldOrEnum(ref l8h, info, "Volume");
		SerializationHelper.SerializeFieldOrEnum(ref l8s, info, "Radius");
		SerializationHelper.SerializeFieldOrEnum(ref l8_u0002_, info, "DopplerScale");
		SerializationHelper.SerializeFieldOrEnum(ref l8_u0005_, info, "AudioType");
		SerializationHelper.SerializeFieldOrEnum(ref l8Y, info, "UpdateType");
		SerializationHelper.SerializeFieldOrEnum(ref l8b, info, "World");
		string field = l8T.ContentRepositoryName;
		string field2 = l8T.SourceAssetFilePath;
		SerializationHelper.SerializeFieldOrEnum(ref field, info, "ContentRepositoryName");
		SerializationHelper.SerializeFieldOrEnum(ref field2, info, "SoundEffectFile");
		l8_u0014_.GetObjectData(info, context);
	}
}
