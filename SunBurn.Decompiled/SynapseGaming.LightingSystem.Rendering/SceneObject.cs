using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;
using B;
using L;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Collision;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Editor;
using SynapseGaming.LightingSystem.Serialization;
using W;

namespace SynapseGaming.LightingSystem.Rendering;

/// <summary>
/// Scene object implementation that uses XNA Models, SunBurn MeshData,
/// and raw vertex / index buffers as a source.
/// </summary>
[Serializable]
[EditorCreatedObject]
public class SceneObject : SceneEntity, ISceneObject, ICollisionObject, ISceneEntity, IMovableObject, IWorldBoundingBoxObject, IComponentObject<ISceneEntity>, IEditorCreatedObject<ISceneEntity>, IEditorObject, INamedObject, IEditorRenderableObject
{
	private CollisionReactDelegate fNB;

	private CollisionTriggerDelegate fNa;

	private Matrix[] fNg;

	private bool fNf = true;

	private bool fN_0003 = true;

	private bool fNn = true;

	private ObjectVisibility fN_0019 = ObjectVisibility.RenderedAndCastShadows;

	private ModelAsset fNL = ModelAsset.Empty;

	private bool fNv = true;

	private CollisionType fN_0012;

	private ICollisionMove fN_0015;

	private bool fN8;

	private ICollisionMaterial fNi;

	private float fN_0005 = 1f;

	private StaticLightingType fNj;

	private bool fNo;

	private bool fNc = true;

	private string fNd = "";

	private RenderableMeshCollection fNw;

	private List<RenderableMesh> fNs = new List<RenderableMesh>(16);

	/// <summary>
	/// Provides direct access to the repository name, file name, and model
	/// the scene object was created from. Only valid for serialized scene objects
	/// created via the SunBurn editor.
	/// </summary>
	[EditorProperty(true, Description = "Model File", HorizontalAlignment = true, MajorGrouping = 1, MinorGrouping = 2, ToolTipText = "")]
	public ModelAsset ModelAsset
	{
		get
		{
			return fNL;
		}
		set
		{
			if (value != null)
			{
				fNL = value;
			}
			else
			{
				fNL = ModelAsset.Empty;
			}
			_0003X();
		}
	}

	/// <summary>
	/// Determines if gravity will cause the object to fall. For an object to be affected
	/// by gravity its UpdateType must be Automatic and CollisionType must be Collide.
	/// </summary>
	[EditorProperty(true, Description = "Affected By Gravity", MajorGrouping = 6, MinorGrouping = 1, ToolTipText = "")]
	public bool AffectedByGravity
	{
		get
		{
			return fNv;
		}
		set
		{
			fNv = value;
			if (!fNv && fN_0015 != null)
			{
				fN_0015.RemoveForces();
			}
		}
	}

	/// <summary>
	/// Determines how an object interacts with the scene.
	/// </summary>
	[EditorProperty(true, Description = "Collision Type", HorizontalAlignment = true, MajorGrouping = 5, MinorGrouping = 1, ToolTipText = "")]
	public CollisionType CollisionType
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
	/// Move helper used by this object to determine its momentum, next location, and sweep volume.
	/// </summary>
	public ICollisionMove CollisionMove
	{
		get
		{
			return fN_0015;
		}
		set
		{
			fN_0015 = value;
		}
	}

	/// <summary>
	/// Default material used when collision surface does not implement material info.
	/// </summary>
	public ICollisionMaterial DefaultCollisionMaterial
	{
		get
		{
			return fNi;
		}
		set
		{
			fNi = value;
			fN8 = true;
		}
	}

	/// <summary>
	/// Mass of the object.
	/// </summary>
	[EditorNumberPadOptions(3, 0.001, 10000.0, 0.1)]
	[EditorProperty(true, Description = "Mass", HorizontalAlignment = true, MajorGrouping = 6, MinorGrouping = 2, ToolTipText = "")]
	public float Mass
	{
		get
		{
			return fN_0005;
		}
		set
		{
			fN_0005 = value;
		}
	}

	/// <summary>
	/// Determines if an object uses light mapping, approximate lighting, or no lighting
	/// to receive illumination from BakedDown light sources.
	/// </summary>
	[EditorProperty(true, Description = "Static Lighting Type", HorizontalAlignment = true, MajorGrouping = 4, MinorGrouping = 1, ToolTipText = "")]
	public StaticLightingType StaticLightingType
	{
		get
		{
			return fNj;
		}
		set
		{
			if (!fNo && value == StaticLightingType.BakedDown)
			{
				fNj = StaticLightingType.None;
				return;
			}
			if (fNj != value)
			{
				fNc = true;
			}
			fNj = value;
		}
	}

	/// <summary>
	/// Indicates the object's meshes are capable of using light maps.
	/// </summary>
	[EditorProperty(false)]
	public bool CanLightMap
	{
		get
		{
			return fNo;
		}
		private set
		{
			fNo = value;
			if (!fNo && fNj == StaticLightingType.BakedDown)
			{
				StaticLightingType = StaticLightingType.None;
			}
		}
	}

	/// <summary>
	/// Indicates the object is rendering without errors.
	/// </summary>
	[EditorProperty(false)]
	public bool Valid
	{
		get
		{
			return fNc;
		}
		set
		{
			fNc = value;
		}
	}

	/// <summary>
	/// Contains any errors that occurred during rendering.
	/// </summary>
	[EditorProperty(true, Description = "Possible Errors", HorizontalAlignment = true, MajorGrouping = 4, MinorGrouping = 3, ToolTipText = "")]
	[EditorTextBoxOptions(true, Width = 165)]
	public string RenderingErrors
	{
		get
		{
			return fNd;
		}
		set
		{
			fNd = value;
		}
	}

	/// <summary>
	/// Array of bone transforms used to form the skeleton's current pose. The array
	/// index of a bone matrix should match the vertex buffer bone index.
	/// </summary>
	public Matrix[] SkinBones
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
	/// Defines how the object is rendered.
	///
	/// This enumeration is a Flag, which allows combining multiple values using the
	/// Logical OR operator (example: "ObjectVisibility.Rendered | ObjectVisibility.CastShadows",
	/// both renders the object and casts shadows from it).
	/// </summary>
	[EditorProperty(true, Description = "Visibility", HorizontalAlignment = true, MajorGrouping = 4, MinorGrouping = 2, ToolTipText = "")]
	[EditorDropDownOptions(165)]
	public ObjectVisibility Visibility
	{
		get
		{
			return fN_0019;
		}
		set
		{
			fN_0019 = value;
			fNf = (fN_0019 & ObjectVisibility.CastShadows) != 0;
			fN_0003 = (fN_0019 & ObjectVisibility.Rendered) != 0;
			fNn = (fN_0019 & ObjectVisibility.RenderedInEditor) != 0 || (fN_0019 & ObjectVisibility.Rendered) != 0;
		}
	}

	/// <summary>
	/// Determines if the object casts shadows based on the current ObjectVisibility options.
	/// </summary>
	public bool CastShadows => fNf;

	/// <summary>
	/// Determines if the object is visible based on the current ObjectVisibility options.
	/// </summary>
	public bool Visible => fN_0003;

	/// <summary>
	/// Determines if the object is visible in the editor based on the current ObjectVisibility options.
	/// </summary>
	public bool VisibleInEditor => fNn;

	/// <summary>
	/// Collection of the object's internal mesh parts.
	/// </summary>
	public RenderableMeshCollection RenderableMeshes => fNw;

	/// <summary>
	/// Event used to detect when the object collides with another object, or to
	/// override the default reaction behavior between objects.
	/// </summary>
	public event CollisionReactDelegate CollisionReactEvent
	{
		add
		{
			CollisionReactDelegate collisionReactDelegate = fNB;
			CollisionReactDelegate collisionReactDelegate2;
			do
			{
				collisionReactDelegate2 = collisionReactDelegate;
				CollisionReactDelegate value2 = (CollisionReactDelegate)Delegate.Combine(collisionReactDelegate2, value);
				collisionReactDelegate = Interlocked.CompareExchange(ref fNB, value2, collisionReactDelegate2);
			}
			while ((object)collisionReactDelegate != collisionReactDelegate2);
		}
		remove
		{
			CollisionReactDelegate collisionReactDelegate = fNB;
			CollisionReactDelegate collisionReactDelegate2;
			do
			{
				collisionReactDelegate2 = collisionReactDelegate;
				CollisionReactDelegate value2 = (CollisionReactDelegate)Delegate.Remove(collisionReactDelegate2, value);
				collisionReactDelegate = Interlocked.CompareExchange(ref fNB, value2, collisionReactDelegate2);
			}
			while ((object)collisionReactDelegate != collisionReactDelegate2);
		}
	}

	/// <summary>
	/// Event used to detect when another object collides with this object, but only
	/// when this object's CollisionType is set to Trigger.
	///
	/// The event handler can then apply custom trigger code like damage, apply force, and more.
	/// </summary>
	public event CollisionTriggerDelegate CollisionTriggerEvent
	{
		add
		{
			CollisionTriggerDelegate collisionTriggerDelegate = fNa;
			CollisionTriggerDelegate collisionTriggerDelegate2;
			do
			{
				collisionTriggerDelegate2 = collisionTriggerDelegate;
				CollisionTriggerDelegate value2 = (CollisionTriggerDelegate)Delegate.Combine(collisionTriggerDelegate2, value);
				collisionTriggerDelegate = Interlocked.CompareExchange(ref fNa, value2, collisionTriggerDelegate2);
			}
			while ((object)collisionTriggerDelegate != collisionTriggerDelegate2);
		}
		remove
		{
			CollisionTriggerDelegate collisionTriggerDelegate = fNa;
			CollisionTriggerDelegate collisionTriggerDelegate2;
			do
			{
				collisionTriggerDelegate2 = collisionTriggerDelegate;
				CollisionTriggerDelegate value2 = (CollisionTriggerDelegate)Delegate.Remove(collisionTriggerDelegate2, value);
				collisionTriggerDelegate = Interlocked.CompareExchange(ref fNa, value2, collisionTriggerDelegate2);
			}
			while ((object)collisionTriggerDelegate != collisionTriggerDelegate2);
		}
	}

	/// <summary>
	/// Default constructor for derived classes that implement their own mesh creation.
	/// </summary>
	public SceneObject()
		: base("", infinitebounds: false)
	{
	}

	/// <summary>
	/// Creates a new SceneObject instance.
	/// </summary>
	/// <param name="name">Custom name for the object.</param>
	/// <param name="infinitebounds">Indicates the object bounding area spans the entire world and
	/// the object is always visible.</param>
	public SceneObject(string name, bool infinitebounds)
		: base(name, infinitebounds)
	{
	}

	/// <summary>
	/// Creates a new SceneObject from mesh data.
	/// </summary>
	/// <param name="meshdata"></param>
	public SceneObject(MeshData meshdata)
		: this(meshdata, "")
	{
	}

	/// <summary>
	/// Creates a new SceneObject from mesh data.
	/// </summary>
	/// <param name="meshdata"></param>
	/// <param name="name">Custom name for the object.</param>
	public SceneObject(MeshData meshdata, string name)
		: base(name, meshdata.InfiniteBounds)
	{
		RenderableMesh renderableMesh = new RenderableMesh();
		renderableMesh.Build(this, meshdata.Effect, meshdata.MeshToObject, meshdata.ObjectSpaceBoundingSphere, BoundingBox.CreateFromSphere(meshdata.ObjectSpaceBoundingSphere), meshdata.IndexBuffer, meshdata.VertexBuffer, 0, PrimitiveType.TriangleList, meshdata.PrimitiveCount, 0, meshdata.VertexCount, 0, detectskinningandlightmapping: true);
		Add(renderableMesh);
	}

	/// <summary>
	/// Creates a new SceneObject from a user defined vertex buffer.
	/// </summary>
	/// <param name="effect">Effect applied to the mesh during rendering.</param>
	/// <param name="meshboundingsphere">Smallest mesh space bounding sphere that
	/// completely encloses the object.</param>
	/// <param name="meshboundingbox">Smallest mesh space bounding box that
	/// completely encloses the object.</param>
	/// <param name="vertexbuffer">VertexBuffer that contains the mesh geometry.</param>
	/// <param name="vertexstart">Index into the vertex buffer that mesh geometry begins.</param>
	/// <param name="primitivetype">Primitive format the mesh geometry is stored in.</param>
	/// <param name="primitivecount">Number of primitives in the mesh geometry.</param>
	/// <param name="vertexstreamoffset">Offset in bytes from the beginning of the vertex
	/// buffer to start reading data.</param>
	/// <param name="objectspace">Mesh object-space matrix.</param>
	public SceneObject(Effect effect, BoundingSphere meshboundingsphere, BoundingBox meshboundingbox, Matrix objectspace, VertexBuffer vertexbuffer, PrimitiveType primitivetype, int primitivecount, int vertexstart, int vertexstreamoffset)
		: this("", infinitebounds: false, effect, meshboundingsphere, meshboundingbox, objectspace, null, vertexbuffer, 0, primitivetype, primitivecount, 0, 0, vertexstreamoffset)
	{
	}

	/// <summary>
	/// Creates a new SceneObject from a user defined vertex buffer.
	/// </summary>
	/// <param name="name">Custom name for the object.</param>
	/// <param name="infinitebounds">Determines if the object spans an infinite bounding volume.</param>
	/// <param name="effect">Effect applied to the mesh during rendering.</param>
	/// <param name="meshboundingsphere">Smallest mesh space bounding sphere that
	/// completely encloses the object.</param>
	/// <param name="meshboundingbox">Smallest mesh space bounding box that
	/// completely encloses the object.</param>
	/// <param name="vertexbuffer">VertexBuffer that contains the mesh geometry.</param>
	/// <param name="vertexstart">Index into the vertex buffer that mesh geometry begins.</param>
	/// <param name="primitivetype">Primitive format the mesh geometry is stored in.</param>
	/// <param name="primitivecount">Number of primitives in the mesh geometry.</param>
	/// <param name="vertexstreamoffset">Offset in bytes from the beginning of the vertex
	/// buffer to start reading data.</param>
	/// <param name="objectspace">Mesh object-space matrix.</param>
	public SceneObject(string name, bool infinitebounds, Effect effect, BoundingSphere meshboundingsphere, BoundingBox meshboundingbox, Matrix objectspace, VertexBuffer vertexbuffer, PrimitiveType primitivetype, int primitivecount, int vertexstart, int vertexstreamoffset)
		: this(name, infinitebounds, effect, meshboundingsphere, meshboundingbox, objectspace, null, vertexbuffer, 0, primitivetype, primitivecount, 0, 0, vertexstreamoffset)
	{
	}

	/// <summary>
	/// Creates a new SceneObject from a user defined vertex and index buffer.
	/// </summary>
	/// <param name="effect">Effect applied to the mesh during rendering.</param>
	/// <param name="meshboundingsphere">Smallest mesh space bounding sphere that
	/// completely encloses the object.</param>
	/// <param name="meshboundingbox">Smallest mesh space bounding box that
	/// completely encloses the object.</param>
	/// <param name="indexbuffer">IndexBuffer that contains the mesh geometry.</param>
	/// <param name="vertexbuffer">VertexBuffer that contains the mesh geometry.</param>
	/// <param name="indexstart">Index into the index buffer that mesh geometry begins.</param>
	/// <param name="primitivetype">Primitive format the mesh geometry is stored in.</param>
	/// <param name="primitivecount">Number of primitives in the mesh geometry.</param>
	/// <param name="vertexbase">Offset added to each index in the index buffer during rendering.</param>
	/// <param name="vertexcount">Number of vertices in the vertex buffer range required to
	/// draw the mesh.  For instance, a quad rendering vertices at indices (2, 5, 6, 9) requires
	/// a vertex buffer range of 8 vertices (vertices 2 – 9 inclusive).</param>
	/// <param name="vertexstreamoffset">Offset in bytes from the beginning of the vertex
	/// buffer to start reading data.</param>
	/// <param name="objectspace">Mesh object-space matrix.</param>
	public SceneObject(Effect effect, BoundingSphere meshboundingsphere, BoundingBox meshboundingbox, Matrix objectspace, IndexBuffer indexbuffer, VertexBuffer vertexbuffer, int indexstart, PrimitiveType primitivetype, int primitivecount, int vertexbase, int vertexcount, int vertexstreamoffset)
		: this("", infinitebounds: false, effect, meshboundingsphere, meshboundingbox, objectspace, indexbuffer, vertexbuffer, indexstart, primitivetype, primitivecount, vertexbase, vertexcount, vertexstreamoffset)
	{
	}

	/// <summary>
	/// Creates a new SceneObject from a user defined vertex and index buffer.
	/// </summary>
	/// <param name="name">Custom name for the object.</param>
	/// <param name="infinitebounds">Determines if the object spans an infinite bounding volume.</param>
	/// <param name="effect">Effect applied to the mesh during rendering.</param>
	/// <param name="meshboundingsphere">Smallest mesh space bounding sphere that
	/// completely encloses the object.</param>
	/// <param name="meshboundingbox">Smallest mesh space bounding box that
	/// completely encloses the object.</param>
	/// <param name="indexbuffer">IndexBuffer that contains the mesh geometry.</param>
	/// <param name="vertexbuffer">VertexBuffer that contains the mesh geometry.</param>
	/// <param name="indexstart">Index into the index buffer that mesh geometry begins.</param>
	/// <param name="primitivetype">Primitive format the mesh geometry is stored in.</param>
	/// <param name="primitivecount">Number of primitives in the mesh geometry.</param>
	/// <param name="vertexbase">Offset added to each index in the index buffer during rendering.</param>
	/// <param name="vertexcount">Number of vertices in the vertex buffer range required to
	/// draw the mesh.  For instance, a quad rendering vertices at indices (2, 5, 6, 9) requires
	/// a vertex buffer range of 8 vertices (vertices 2 – 9 inclusive).</param>
	/// <param name="vertexstreamoffset">Offset in bytes from the beginning of the vertex
	/// buffer to start reading data.</param>
	/// <param name="objectspace">Mesh object-space matrix.</param>
	public SceneObject(string name, bool infinitebounds, Effect effect, BoundingSphere meshboundingsphere, BoundingBox meshboundingbox, Matrix objectspace, IndexBuffer indexbuffer, VertexBuffer vertexbuffer, int indexstart, PrimitiveType primitivetype, int primitivecount, int vertexbase, int vertexcount, int vertexstreamoffset)
		: base(name, infinitebounds)
	{
		RenderableMesh renderableMesh = new RenderableMesh();
		renderableMesh.Build(this, effect, objectspace, meshboundingsphere, meshboundingbox, indexbuffer, vertexbuffer, indexstart, primitivetype, primitivecount, vertexbase, vertexcount, vertexstreamoffset, detectskinningandlightmapping: true);
		Add(renderableMesh);
	}

	/// <summary>
	/// Creates a new SceneObject constructing RenderableMeshes
	/// from all ModelMeshes within the provided Model.
	/// </summary>
	/// <param name="model"></param>
	public SceneObject(Model model)
		: this(model, model.Root.Name)
	{
	}

	/// <summary>
	/// Creates a new SceneObject constructing RenderableMeshes
	/// from the provided ModelMesh.
	/// </summary>
	/// <param name="mesh"></param>
	public SceneObject(ModelMesh mesh)
		: this(mesh, mesh.ParentBone.Name)
	{
	}

	/// <summary>
	/// Creates a new SceneObject constructing RenderableMeshes
	/// from all ModelMeshes within the provided Model.
	/// </summary>
	/// <param name="model"></param>
	/// <param name="name">Custom name for the object.</param>
	public SceneObject(Model model, string name)
		: base(name, infinitebounds: false)
	{
		for (int k = 0; k < model.Meshes.Count; k++)
		{
			AddModelMesh(model.Meshes[k], null);
		}
	}

	/// <summary>
	/// Creates a new SceneObject constructing RenderableMeshes
	/// from the provided ModelMesh.
	/// </summary>
	/// <param name="mesh"></param>
	/// <param name="name">Custom name for the object.</param>
	public SceneObject(ModelMesh mesh, string name)
		: base(name, infinitebounds: false)
	{
		AddModelMesh(mesh, null);
	}

	/// <summary>
	/// Creates a new SceneObject constructing RenderableMeshes
	/// from all ModelMeshes within the provided Model.
	/// </summary>
	/// <param name="model"></param>
	/// <param name="overrideeffect">User defined effect used to render the object.</param>
	/// <param name="name">Custom name for the object.</param>
	public SceneObject(Model model, Effect overrideeffect, string name)
		: base(name, infinitebounds: false)
	{
		for (int k = 0; k < model.Meshes.Count; k++)
		{
			AddModelMesh(model.Meshes[k], overrideeffect);
		}
	}

	/// <summary>
	/// Creates a new SceneObject constructing RenderableMeshes
	/// from the provided ModelMesh.
	/// </summary>
	/// <param name="mesh"></param>
	/// <param name="overrideeffect">User defined effect used to render the object.</param>
	/// <param name="name">Custom name for the object.</param>
	public SceneObject(ModelMesh mesh, Effect overrideeffect, string name)
		: base(name, infinitebounds: false)
	{
		AddModelMesh(mesh, overrideeffect);
	}

	/// <summary>
	/// Initializes the object to default values.
	/// </summary>
	/// <param name="name">Custom name for the object.</param>
	/// <param name="infinitebounds">Indicates the object bounding area spans the entire world and
	/// the object is always visible.</param>
	protected override void Init(string name, bool infinitebounds)
	{
		fNw = new RenderableMeshCollection(fNs);
		fN_0015 = new CollisionMove(this);
		base.Init(name, infinitebounds);
	}

	/// <summary>
	/// Deep clones the object including any contained sub-objects and components.
	/// </summary>
	/// <returns></returns>
	public override ISceneEntity Clone()
	{
		ISceneEntity sceneEntity = base.Clone();
		ISceneObject sceneObject = sceneEntity as ISceneObject;
		sceneObject.ModelAsset = fNL;
		sceneObject.StaticLightingType = StaticLightingType;
		return sceneEntity;
	}

	/// <summary>
	/// Used to trigger the CollisionReactEvent event when two objects collide.
	/// </summary>
	/// <param name="collider">The moving object.</param>
	/// <param name="collidee">The object hit by the moving object.</param>
	/// <param name="worldcollisionpoint">Contains information about the closest collision point to the collider.</param>
	/// <param name="collisionhandled">Determines if the collision was handled by a prior event hander.
	/// If this value is true do NOT process any collision reaction code. If the event handler processes
	/// collision reaction code set this value to true to avoid another handler or SunBurn's built-in
	/// reaction code from processing.</param>
	public void OnCollisionReact(IMovableObject collider, IMovableObject collidee, CollisionPoint worldcollisionpoint, ref bool collisionhandled)
	{
		_Components.OnCollisionReact(collider, collidee, worldcollisionpoint, ref collisionhandled);
		if (fNB != null)
		{
			fNB(collider, collidee, worldcollisionpoint, ref collisionhandled);
		}
	}

	/// <summary>
	/// Used to trigger the CollisionTriggerEvent event when an object passes through or overlaps a trigger.
	/// </summary>
	/// <param name="collider">The moving object.</param>
	/// <param name="trigger">The trigger hit by the moving object.</param>
	public void OnCollisionTrigger(IMovableObject collider, IMovableObject trigger)
	{
		_Components.OnCollisionTrigger(collider, trigger);
		if (fNa != null)
		{
			fNa(collider, trigger);
		}
	}

	/// <summary>
	/// Calculates and applies the reaction force between the
	/// object and the collision surface contained in the CollisionPoint.
	/// </summary>
	/// <param name="worldcollisionpoint">Contains information about the closest collision point to the collider.</param>
	public virtual void React(CollisionPoint worldcollisionpoint)
	{
		ICollisionObject contactObject = worldcollisionpoint.ContactObject;
		bool collisionhandled = false;
		if (fN_0012 == CollisionType.Collide && contactObject.CollisionType == CollisionType.Collide)
		{
			OnCollisionReact(this, contactObject, worldcollisionpoint, ref collisionhandled);
			contactObject.OnCollisionReact(this, contactObject, worldcollisionpoint, ref collisionhandled);
			if (!collisionhandled && fN_0015 != null)
			{
				fN_0015.React(worldcollisionpoint);
			}
		}
	}

	private void _0003X()
	{
		Clear();
		Model asset = fNL.Asset;
		if (asset == null)
		{
			return;
		}
		string modelMeshName = fNL.ModelMeshName;
		bool flag = !string.IsNullOrEmpty(modelMeshName);
		for (int k = 0; k < asset.Meshes.Count; k++)
		{
			ModelMesh modelMesh = asset.Meshes[k];
			if (!flag || (!string.IsNullOrEmpty(modelMesh.Name) && !(modelMesh.Name != modelMeshName)))
			{
				AddModelMesh(modelMesh, null);
				if (flag)
				{
					break;
				}
			}
		}
		ContentRepository contentRepository = ContentRepository.Find(fNL.ContentRepositoryName);
		if (contentRepository != null)
		{
			for (int l = 0; l < fNw.Count; l++)
			{
				contentRepository.LoadLightMap(fNw[l]);
			}
		}
		Matrix world = World;
		Matrix worldtoobj = WorldToObject;
		CalculateBounds();
		UpdateWorldAndWorldToObject(ref world, ref worldtoobj);
	}

	/// <summary>
	/// Adds a mesh to this object. Automatically recalculates the object bounds.
	/// </summary>
	/// <param name="mesh"></param>
	public void Add(RenderableMesh mesh)
	{
		fNs.Add(mesh);
		mesh.SetWorldAndWorldToObject(World, WorldToObject);
		RebuildMeshInfo();
		CalculateBounds();
	}

	/// <summary>
	/// Removes a mesh from this object. Automatically recalculates the object bounds.
	/// </summary>
	/// <param name="mesh"></param>
	public void Remove(RenderableMesh mesh)
	{
		fNs.Remove(mesh);
		CalculateBounds();
		RebuildMeshInfo();
	}

	/// <summary>
	/// Removes all meshes from this object.
	/// </summary>
	public void Clear()
	{
		fNs.Clear();
		fNi = null;
		fN8 = false;
		CalculateBounds();
		RebuildMeshInfo();
	}

	/// <summary>
	/// Called when the mesh list changes.
	/// </summary>
	protected virtual void RebuildMeshInfo()
	{
		if (!fN8)
		{
			fNi = null;
		}
		CanLightMap = true;
		for (int k = 0; k < fNw.Count; k++)
		{
			RenderableMesh renderableMesh = fNw[k];
			if (fNi == null)
			{
				fNi = renderableMesh.fN_0015 as ICollisionMaterial;
			}
			if (!renderableMesh.fNf)
			{
				CanLightMap = false;
				break;
			}
		}
	}

	/// <summary>
	/// Calculates the object bounds.
	/// </summary>
	/// <param name="objectboundingbox">Object bounds to update.</param>
	/// <param name="objectboundingsphere">Object bounds to update.</param>
	protected override void CalculateObjectBounds(ref BoundingBox objectboundingbox, ref BoundingSphere objectboundingsphere)
	{
		if (InfiniteBounds)
		{
			base.CalculateObjectBounds(ref objectboundingbox, ref objectboundingsphere);
			return;
		}
		if (fNw.Count <= 0)
		{
			objectboundingbox = new BoundingBox(-Vector3.One, Vector3.One);
			objectboundingsphere = new BoundingSphere(Vector3.One, 1f);
			return;
		}
		RenderableMesh renderableMesh = fNw[0];
		objectboundingbox = L._0019.ah(renderableMesh.fNL, renderableMesh.fN_0003);
		objectboundingsphere = L._0019.aR(renderableMesh.fN_0019, renderableMesh.fN_0003);
		for (int k = 1; k < fNw.Count; k++)
		{
			renderableMesh = fNw[k];
			BoundingBox additional = L._0019.ah(renderableMesh.fNL, renderableMesh.fN_0003);
			BoundingSphere additional2 = L._0019.aR(renderableMesh.fN_0019, renderableMesh.fN_0003);
			objectboundingbox = BoundingBox.CreateMerged(objectboundingbox, additional);
			objectboundingsphere = BoundingSphere.CreateMerged(objectboundingsphere, additional2);
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
		if (fN_0015 is CollisionMove collisionMove)
		{
			collisionMove.Begin(0f);
		}
	}

	/// <summary>
	/// Converts a ModelMesh into RenderableMeshes and adds them
	/// to this object. Automatically recalculates the object bounds.
	/// </summary>
	/// <param name="mesh"></param>
	/// <param name="overrideeffect">User defined effect used to render
	/// the object. If null the effects contained in the ModelMesh are used.</param>
	public void AddModelMesh(ModelMesh mesh, Effect overrideeffect)
	{
		int num = B.f.o(mesh.Name);
		for (int k = 0; k < mesh.MeshParts.Count; k++)
		{
			ModelMeshPart modelMeshPart = mesh.MeshParts[k];
			Effect overrideeffect2 = modelMeshPart.Effect;
			if (overrideeffect != null)
			{
				overrideeffect2 = overrideeffect;
			}
			RenderableMesh renderableMesh = new RenderableMesh();
			renderableMesh.Build(this, mesh, modelMeshPart, overrideeffect2);
			renderableMesh.fNg = L._0019.au(UniqueId, num);
			Add(renderableMesh);
		}
	}

	/// <summary>
	/// Updates the object world space and inverse world space transforms.
	/// Override to perform custom code when the world transform changes.
	/// </summary>
	/// <param name="world">World space transform.</param>
	/// <param name="worldtoobj">Inverse world space transform.</param>
	protected override void UpdateWorldAndWorldToObject(ref Matrix world, ref Matrix worldtoobj)
	{
		for (int k = 0; k < fNw.Count; k++)
		{
			fNw[k].SetWorldAndWorldToObject(ref world, ref worldtoobj);
		}
		base.UpdateWorldAndWorldToObject(ref world, ref worldtoobj);
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
	/// Helper method that creates a new SceneObject for each
	/// ModelMesh in the provided Model.
	/// </summary>
	/// <param name="model">Source Model object.</param>
	/// <param name="returnobjects">List used to store the created SceneObject objects.</param>
	public static void CreateMeshBasedObjectsFromModel(Model model, IList<SceneObject> returnobjects)
	{
		for (int k = 0; k < model.Meshes.Count; k++)
		{
			returnobjects.Add(new SceneObject(model.Meshes[k]));
		}
	}

	/// <summary>
	/// Deserializes object data from the provided SerializationInfo.
	/// </summary>
	/// <param name="info">Contains the serialized object data.</param>
	/// <param name="context"></param>
	public override void SetObjectData(SerializationInfo info, StreamingContext context)
	{
		_0003S(info);
		string field = string.Empty;
		string field2 = string.Empty;
		string field3 = string.Empty;
		SerializationHelper.DeserializeField(ref field2, info, "ModelFile", usedefault: true);
		SerializationHelper.DeserializeField(ref field3, info, "ModelMeshName", usedefault: true);
		SerializationHelper.DeserializeField(ref field, info, "ContentRepositoryName", usedefault: true);
		fNj = W._0003.ni(info);
		SerializationHelper.DeserializeEnum(ref fN_0019, info, "Visibility", isflag: false);
		SerializationHelper.DeserializeField(ref fN_0005, info, "Mass", usedefault: true);
		SerializationHelper.DeserializeField(ref fNv, info, "AffectedByGravity", usedefault: true);
		SerializationHelper.DeserializeEnum(ref fN_0012, info, "CollisionType", isflag: false);
		Visibility = fN_0019;
		ModelAsset = new ModelAsset(field, field2, field3);
		Components.SetObjectData(info, context);
	}

	/// <summary>
	/// Serializes object data to the provided SerializationInfo.
	/// </summary>
	/// <param name="info">SerializationInfo to store the serialized data.</param>
	/// <param name="context"></param>
	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
	public override void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		base.GetObjectData(info, context);
		info.AddValue("ModelFile", ModelAsset.SourceAssetFilePath);
		info.AddValue("ModelMeshName", ModelAsset.ModelMeshName);
		info.AddValue("ContentRepositoryName", ModelAsset.ContentRepositoryName);
		info.AddValue("StaticLightingType", fNj);
		info.AddValue("Visibility", fN_0019);
		info.AddValue("Mass", fN_0005);
		info.AddValue("AffectedByGravity", fNv);
		info.AddValue("CollisionType", fN_0012);
	}
}
