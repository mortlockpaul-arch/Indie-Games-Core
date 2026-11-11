using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Collision;
using SynapseGaming.LightingSystem.Components;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Editor;
using SynapseGaming.LightingSystem.Lights;
using SynapseGaming.LightingSystem.Serialization;
using X;
using a;

namespace SynapseGaming.LightingSystem.Rendering;

[Serializable]
[EditorCreatedObject]
public class SceneObject : SceneEntity, ISceneObject, ICollisionObject, ISceneEntity, IMovableObject, IWorldBoundingBoxObject, IComponentObject<ISceneEntity>, IEditorCreatedObject<ISceneEntity>, IEditorObject, INamedObject, IEditorRenderableObject
{
	private CollisionReactDelegate l8a;

	private CollisionTriggerDelegate l8_u0013_;

	private Matrix[] l8N;

	private bool l8l = true;

	private bool l8L = true;

	private bool l8w = true;

	private ObjectVisibility l8J = ObjectVisibility.RenderedAndCastShadows;

	private ModelAsset l8B = ModelAsset.Empty;

	private bool l8s = true;

	private CollisionType l8h;

	private ICollisionMove l8_u0002_;

	private bool l8v;

	private ICollisionMaterial l8Y;

	private float l8_u0005_ = 1f;

	private StaticLightingType l8b;

	private LightMapSize l8I = LightMapSize.Size128x128;

	private Vector3 l85;

	private bool l8W;

	private bool l8Z = true;

	private string l8T = "";

	private RenderableMeshCollection l8_u0014_;

	private List<RenderableMesh> l8E = new List<RenderableMesh>(16);

	[EditorProperty(true, Description = "Model File", HorizontalAlignment = true, MajorGrouping = 1, MinorGrouping = 2, ToolTipText = "")]
	public ModelAsset ModelAsset
	{
		get
		{
			return l8B;
		}
		set
		{
			if (value != null)
			{
				l8B = value;
			}
			else
			{
				l8B = ModelAsset.Empty;
			}
			Ld();
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

	[EditorProperty(true, Description = "Affected By Gravity", MajorGrouping = 6, MinorGrouping = 1, ToolTipText = "")]
	public bool AffectedByGravity
	{
		get
		{
			return l8s;
		}
		set
		{
			l8s = value;
			_CollisionId++;
			if (!l8s && l8_u0002_ != null)
			{
				l8_u0002_.RemoveForces();
			}
		}
	}

	[EditorProperty(true, Description = "Collision Type", HorizontalAlignment = true, MajorGrouping = 5, MinorGrouping = 1, ToolTipText = "")]
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

	public ICollisionMaterial DefaultCollisionMaterial
	{
		get
		{
			return l8Y;
		}
		set
		{
			l8Y = value;
			l8v = true;
			_CollisionId++;
		}
	}

	[EditorProperty(true, Description = "Mass", HorizontalAlignment = true, MajorGrouping = 6, MinorGrouping = 2, ToolTipText = "")]
	[EditorNumberPadOptions(3, 0.001, 10000.0, 0.1)]
	public float Mass
	{
		get
		{
			return l8_u0005_;
		}
		set
		{
			l8_u0005_ = value;
			_CollisionId++;
		}
	}

	[EditorProperty(true, Description = "Static Lighting Type", HorizontalAlignment = true, MajorGrouping = 4, MinorGrouping = 2, ToolTipText = "")]
	public StaticLightingType StaticLightingType
	{
		get
		{
			return l8b;
		}
		set
		{
			if (!l8W && value == StaticLightingType.BakedDown)
			{
				l8b = StaticLightingType.None;
				return;
			}
			if (l8b != value)
			{
				l8Z = true;
			}
			l8b = value;
		}
	}

	public LightMapSize LightMapSize
	{
		get
		{
			return l8I;
		}
		set
		{
			l8I = value;
		}
	}

	[EditorProperty(true, Description = "Custom Static Lighting", HorizontalAlignment = true, MajorGrouping = 4, MinorGrouping = 3, ToolTipText = "", ControlType = ControlType.ColorSelection)]
	public Vector3 CustomStaticLightingColor
	{
		get
		{
			return l85;
		}
		set
		{
			l85 = value;
		}
	}

	[EditorProperty(false)]
	public bool CanLightMap
	{
		get
		{
			return l8W;
		}
		private set
		{
			l8W = value;
			if (!l8W && l8b == StaticLightingType.BakedDown)
			{
				StaticLightingType = StaticLightingType.None;
			}
		}
	}

	[EditorProperty(false)]
	public bool Valid
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

	[EditorTextBoxOptions(true, Width = 165)]
	[EditorProperty(true, Description = "Possible Errors", HorizontalAlignment = true, MajorGrouping = 5, MinorGrouping = 3, ToolTipText = "")]
	public string RenderingErrors
	{
		get
		{
			return l8T;
		}
		set
		{
			l8T = value;
		}
	}

	public Matrix[] SkinBones
	{
		get
		{
			return l8N;
		}
		set
		{
			l8N = value;
		}
	}

	[EditorProperty(true, Description = "Visibility", HorizontalAlignment = true, MajorGrouping = 4, MinorGrouping = 1, ToolTipText = "")]
	[EditorDropDownOptions(165)]
	public ObjectVisibility Visibility
	{
		get
		{
			return l8J;
		}
		set
		{
			l8J = value;
			l8l = (l8J & ObjectVisibility.CastShadows) != 0;
			l8L = (l8J & ObjectVisibility.Rendered) != 0;
			l8w = (l8J & ObjectVisibility.RenderedInEditor) != 0 || (l8J & ObjectVisibility.Rendered) != 0;
		}
	}

	public bool CastShadows => l8l;

	public bool Visible => l8L;

	public bool VisibleInEditor => l8w;

	public RenderableMeshCollection RenderableMeshes => l8_u0014_;

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

	public SceneObject()
		: base("", infinitebounds: false)
	{
	}

	public SceneObject(string name, bool infinitebounds)
		: base(name, infinitebounds)
	{
	}

	public SceneObject(MeshData meshdata)
		: this(meshdata, "")
	{
	}

	public SceneObject(MeshData meshdata, string name)
		: base(name, meshdata.InfiniteBounds)
	{
		RenderableMesh renderableMesh = new RenderableMesh();
		renderableMesh.Build(this, meshdata.Effect, meshdata.MeshToObject, meshdata.ObjectSpaceBoundingSphere, meshdata.ObjectSpaceBoundingBox, meshdata.IndexBuffer, meshdata.VertexBuffer, 0, PrimitiveType.TriangleList, meshdata.PrimitiveCount, 0, meshdata.VertexCount, 0, detectskinningandlightmapping: true);
		Add(renderableMesh);
	}

	public SceneObject(Effect effect, BoundingSphere meshboundingsphere, BoundingBox meshboundingbox, Matrix objectspace, VertexBuffer vertexbuffer, PrimitiveType primitivetype, int primitivecount, int vertexstart, int vertexstreamoffset)
		: this("", infinitebounds: false, effect, meshboundingsphere, meshboundingbox, objectspace, null, vertexbuffer, 0, primitivetype, primitivecount, 0, 0, vertexstreamoffset)
	{
	}

	public SceneObject(string name, bool infinitebounds, Effect effect, BoundingSphere meshboundingsphere, BoundingBox meshboundingbox, Matrix objectspace, VertexBuffer vertexbuffer, PrimitiveType primitivetype, int primitivecount, int vertexstart, int vertexstreamoffset)
		: this(name, infinitebounds, effect, meshboundingsphere, meshboundingbox, objectspace, null, vertexbuffer, 0, primitivetype, primitivecount, 0, 0, vertexstreamoffset)
	{
	}

	public SceneObject(Effect effect, BoundingSphere meshboundingsphere, BoundingBox meshboundingbox, Matrix objectspace, IndexBuffer indexbuffer, VertexBuffer vertexbuffer, int indexstart, PrimitiveType primitivetype, int primitivecount, int vertexbase, int vertexcount, int vertexstreamoffset)
		: this("", infinitebounds: false, effect, meshboundingsphere, meshboundingbox, objectspace, indexbuffer, vertexbuffer, indexstart, primitivetype, primitivecount, vertexbase, vertexcount, vertexstreamoffset)
	{
	}

	public SceneObject(string name, bool infinitebounds, Effect effect, BoundingSphere meshboundingsphere, BoundingBox meshboundingbox, Matrix objectspace, IndexBuffer indexbuffer, VertexBuffer vertexbuffer, int indexstart, PrimitiveType primitivetype, int primitivecount, int vertexbase, int vertexcount, int vertexstreamoffset)
		: base(name, infinitebounds)
	{
		RenderableMesh renderableMesh = new RenderableMesh();
		renderableMesh.Build(this, effect, objectspace, meshboundingsphere, meshboundingbox, indexbuffer, vertexbuffer, indexstart, primitivetype, primitivecount, vertexbase, vertexcount, vertexstreamoffset, detectskinningandlightmapping: true);
		Add(renderableMesh);
	}

	public SceneObject(Model model)
		: this(model, model.Root.Name)
	{
	}

	public SceneObject(ModelMesh mesh)
		: this(mesh, mesh.ParentBone.Name)
	{
	}

	public SceneObject(Model model, string name)
		: base(name, infinitebounds: false)
	{
		for (int i = 0; i < model.Meshes.Count; i++)
		{
			AddModelMesh(model.Meshes[i], null);
		}
	}

	public SceneObject(ModelMesh mesh, string name)
		: base(name, infinitebounds: false)
	{
		AddModelMesh(mesh, null);
	}

	public SceneObject(Model model, Effect overrideeffect, string name)
		: base(name, infinitebounds: false)
	{
		for (int i = 0; i < model.Meshes.Count; i++)
		{
			AddModelMesh(model.Meshes[i], overrideeffect);
		}
	}

	public SceneObject(ModelMesh mesh, Effect overrideeffect, string name)
		: base(name, infinitebounds: false)
	{
		AddModelMesh(mesh, overrideeffect);
	}

	protected override void Init(string name, bool infinitebounds)
	{
		l8_u0014_ = new RenderableMeshCollection(l8E);
		base.Init(name, infinitebounds);
	}

	public override ISceneEntity Clone()
	{
		ISceneEntity sceneEntity = base.Clone();
		ISceneObject sceneObject = sceneEntity as ISceneObject;
		if (l8B != null && l8B != ModelAsset.Empty && l8B.Asset != null)
		{
			sceneObject.ModelAsset = l8B;
		}
		else if (sceneObject is SceneObject sceneObject2)
		{
			for (int i = 0; i < l8_u0014_.Count; i++)
			{
				sceneObject2.Add(l8_u0014_[i].Clone());
			}
		}
		sceneObject.StaticLightingType = StaticLightingType;
		return sceneEntity;
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

	public virtual void OnCollisionReact(IMovableObject collider, IMovableObject collidee, CollisionPoint worldcollisionpoint, ref bool collisionhandled)
	{
		_Components.OnCollisionReact(collider, collidee, worldcollisionpoint, ref collisionhandled);
		if (l8a != null)
		{
			l8a(collider, collidee, worldcollisionpoint, ref collisionhandled);
		}
	}

	public virtual void OnCollisionTrigger(IMovableObject collider, IMovableObject trigger)
	{
		_Components.OnCollisionTrigger(collider, trigger);
		if (l8_u0013_ != null)
		{
			l8_u0013_(collider, trigger);
		}
	}

	private void Ld()
	{
		Clear();
		Model asset = l8B.Asset;
		if (asset == null)
		{
			return;
		}
		string modelMeshName = l8B.ModelMeshName;
		bool flag = !string.IsNullOrEmpty(modelMeshName);
		for (int i = 0; i < asset.Meshes.Count; i++)
		{
			ModelMesh modelMesh = asset.Meshes[i];
			if (!flag || (!string.IsNullOrEmpty(modelMesh.Name) && !(modelMesh.Name != modelMeshName)))
			{
				AddModelMesh(modelMesh, null);
				if (flag)
				{
					break;
				}
			}
		}
		ContentRepository contentRepository = ContentRepository.Find(l8B.ContentRepositoryName);
		if (contentRepository != null)
		{
			for (int j = 0; j < l8_u0014_.Count; j++)
			{
				contentRepository.LoadLightMap(l8_u0014_[j]);
			}
		}
		Matrix world = World;
		Matrix worldtoobj = WorldToObject;
		CalculateBounds();
		UpdateWorldAndWorldToObject(ref world, ref worldtoobj);
	}

	public void Add(RenderableMesh mesh)
	{
		l8E.Add(mesh);
		mesh.SetWorldAndWorldToObject(World, WorldToObject);
		RebuildMeshInfo();
		CalculateBounds();
	}

	public void Remove(RenderableMesh mesh)
	{
		l8E.Remove(mesh);
		CalculateBounds();
		RebuildMeshInfo();
	}

	public void Clear()
	{
		l8E.Clear();
		l8Y = null;
		l8v = false;
		CalculateBounds();
		RebuildMeshInfo();
	}

	protected virtual void RebuildMeshInfo()
	{
		if (!l8v)
		{
			l8Y = null;
		}
		CanLightMap = true;
		for (int i = 0; i < l8_u0014_.Count; i++)
		{
			RenderableMesh renderableMesh = l8_u0014_[i];
			if (l8Y == null)
			{
				l8Y = renderableMesh.l8_u0002_ as ICollisionMaterial;
			}
			if (!renderableMesh.l8l)
			{
				CanLightMap = false;
				break;
			}
		}
	}

	protected override void CalculateObjectBounds(ref BoundingBox objectboundingbox, ref BoundingSphere objectboundingsphere)
	{
		if (InfiniteBounds)
		{
			base.CalculateObjectBounds(ref objectboundingbox, ref objectboundingsphere);
			return;
		}
		if (l8_u0014_.Count <= 0)
		{
			objectboundingbox = new BoundingBox(-Vector3.One, Vector3.One);
			objectboundingsphere = new BoundingSphere(Vector3.One, 1f);
			return;
		}
		RenderableMesh renderableMesh = l8_u0014_[0];
		objectboundingbox = CoreHelper.TransformBoundingBox(renderableMesh.l8B, renderableMesh.l8L);
		objectboundingsphere = CoreHelper.TransformBoundingSphereSlow(renderableMesh.l8J, renderableMesh.l8L);
		for (int i = 1; i < l8_u0014_.Count; i++)
		{
			renderableMesh = l8_u0014_[i];
			BoundingBox additional = CoreHelper.TransformBoundingBox(renderableMesh.l8B, renderableMesh.l8L);
			BoundingSphere additional2 = CoreHelper.TransformBoundingSphereSlow(renderableMesh.l8J, renderableMesh.l8L);
			objectboundingbox = BoundingBox.CreateMerged(objectboundingbox, additional);
			objectboundingsphere = BoundingSphere.CreateMerged(objectboundingsphere, additional2);
		}
	}

	protected override void CalculateWorldBounds(ref BoundingBox worldboundingbox, ref BoundingSphere worldboundingsphere, bool alreadymoved)
	{
		if (HullType == HullType.Mesh && UpdateType == UpdateType.Automatic)
		{
			HullType = HullType.Box;
		}
		base.CalculateWorldBounds(ref worldboundingbox, ref worldboundingsphere, alreadymoved);
		if (HullType == HullType.Mesh && !InfiniteBounds)
		{
			BoundingBox boundingBox = worldboundingbox;
			float num = worldboundingsphere.Radius * 2f;
			Vector3 vector = worldboundingbox.Max - worldboundingbox.Min;
			if (vector.X < num || vector.Y < num || vector.Z < num)
			{
				worldboundingsphere = BoundingSphere.CreateFromBoundingBox(worldboundingbox);
			}
			else
			{
				worldboundingbox = BoundingBox.CreateFromSphere(worldboundingsphere);
			}
			if (!alreadymoved && !boundingBox.Equals(worldboundingbox))
			{
				MoveId++;
			}
		}
	}

	public void AddModelMesh(ModelMesh mesh, Effect overrideeffect)
	{
		int hash = a.l.I(mesh.Name);
		for (int i = 0; i < mesh.MeshParts.Count; i++)
		{
			ModelMeshPart modelMeshPart = mesh.MeshParts[i];
			Effect overrideeffect2 = modelMeshPart.Effect;
			if (overrideeffect != null)
			{
				overrideeffect2 = overrideeffect;
			}
			RenderableMesh renderableMesh = new RenderableMesh();
			renderableMesh.Build(this, mesh, modelMeshPart, overrideeffect2);
			renderableMesh.l8N = CoreHelper.GetHashCode(UniqueId, hash);
			Add(renderableMesh);
		}
	}

	protected override void UpdateWorldAndWorldToObject(ref Matrix world, ref Matrix worldtoobj)
	{
		for (int i = 0; i < l8_u0014_.Count; i++)
		{
			l8_u0014_[i].SetWorldAndWorldToObject(ref world, ref worldtoobj);
		}
		base.UpdateWorldAndWorldToObject(ref world, ref worldtoobj);
	}

	public override string ToString()
	{
		return CoreHelper.GetDisplayName(this);
	}

	public static void CreateMeshBasedObjectsFromModel(Model model, IList<SceneObject> returnobjects)
	{
		for (int i = 0; i < model.Meshes.Count; i++)
		{
			returnobjects.Add(new SceneObject(model.Meshes[i]));
		}
	}

	public override void SetObjectData(SerializationInfo info, StreamingContext context)
	{
		_id_7(info);
		string field = string.Empty;
		string field2 = string.Empty;
		string field3 = string.Empty;
		SerializationHelper.DeserializeField(ref field2, info, "ModelFile", usedefault: true);
		SerializationHelper.DeserializeField(ref field3, info, "ModelMeshName", usedefault: true);
		SerializationHelper.DeserializeField(ref field, info, "ContentRepositoryName", usedefault: true);
		l8b = X.L.LV(info);
		SerializationHelper.DeserializeField(ref l85, info, "CustomStaticLightingColor", usedefault: true);
		SerializationHelper.DeserializeEnum(ref l8I, info, "LightMapSize", isflag: true);
		SerializationHelper.DeserializeEnum(ref l8J, info, "Visibility", isflag: false);
		SerializationHelper.DeserializeField(ref l8_u0005_, info, "Mass", usedefault: true);
		SerializationHelper.DeserializeField(ref l8s, info, "AffectedByGravity", usedefault: true);
		SerializationHelper.DeserializeEnum(ref l8h, info, "CollisionType", isflag: false);
		Visibility = l8J;
		ModelAsset = new ModelAsset(field, field2, field3);
		Components.SetObjectData(info, context);
	}

	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
	public override void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		base.GetObjectData(info, context);
		info.AddValue("ModelFile", ModelAsset.SourceAssetFilePath);
		info.AddValue("ModelMeshName", ModelAsset.ModelMeshName);
		info.AddValue("ContentRepositoryName", ModelAsset.ContentRepositoryName);
		info.AddValue("StaticLightingType", l8b);
		info.AddValue("CustomStaticLightingColor", l85);
		info.AddValue("LightMapSize", l8I);
		info.AddValue("Visibility", l8J);
		info.AddValue("Mass", l8_u0005_);
		info.AddValue("AffectedByGravity", l8s);
		info.AddValue("CollisionType", l8h);
	}
}
