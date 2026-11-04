using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using L;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Editor;

namespace SynapseGaming.LightingSystem.Rendering;

/// <summary>
/// Avatar implementation that provides properties necessary for avatar
/// rendering.
/// </summary>
public class Avatar : IAvatar, ISceneEntity, IMovableObject, IWorldBoundingBoxObject, IComponentObject<ISceneEntity>, IEditorCreatedObject<ISceneEntity>, IEditorObject, INamedObject, IEditorRenderableObject
{
	private const int fNB = 71;

	private UpdateDelegate fNa;

	private int fNg;

	private string fNf = "";

	private UpdateType fN_0003;

	private Matrix fNn;

	private BoundingSphere fN_0019;

	private BoundingBox fNL;

	private BoundingBox fNv;

	private BoundingBox fN_0012;

	private BoundingSphere fN_0015;

	private BoundingBox fN8;

	private IList<Matrix> fNi;

	private AvatarExpression fN_0005;

	private AvatarRenderer fNj;

	private AvatarDescription fNo;

	private ObjectVisibility fNc = ObjectVisibility.RenderedAndCastShadows;

	private static List<Matrix> fNd;

	private ComponentCollection<ISceneEntity> fNw;

	[CompilerGenerated]
	private bool fNs;

	/// <summary>
	/// Unique id used to identify the object across multiple scene loads / reloads.
	/// </summary>
	public int UniqueId => 0;

	/// <summary>
	/// Indicates the object bounding area spans the entire world and
	/// the object is always visible.
	/// </summary>
	public bool InfiniteBounds => false;

	/// <summary>
	/// Indicates the current move. This value increments each time the object
	/// is moved (when the World transform changes).
	/// </summary>
	public int MoveId => fNg;

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
			return fNf;
		}
		set
		{
			fNf = value;
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
	/// World space transform of the object.
	/// </summary>
	public Matrix World
	{
		get
		{
			return fNn;
		}
		set
		{
			if (!fNn.Equals(value))
			{
				fNn = value;
				fNg++;
				_0003A();
			}
		}
	}

	/// <summary>
	/// Array of bone transforms for the skeleton's current pose. The matrix index is the
	/// same as the bone order used by the avatar.
	/// </summary>
	public IList<Matrix> SkinBones
	{
		get
		{
			return fNi;
		}
		set
		{
			fNi = value;
		}
	}

	/// <summary>
	/// The current avatar facial expression.
	/// </summary>
	public AvatarExpression Expression
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
	/// Defines how the avatar is rendered.
	///
	/// This enumeration is a Flag, which allows combining multiple values using the
	/// Logical OR operator (example: "ObjectVisibility.Rendered | ObjectVisibility.CastShadows",
	/// both renders the avatar and casts shadows from it).
	/// </summary>
	public ObjectVisibility Visibility
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
	/// Determines the bounds used in object culling and collision.
	/// </summary>
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
	/// Object space bounding area of the object.
	/// </summary>
	public BoundingSphere ObjectBoundingSphere => fN_0019;

	/// <summary>
	/// Object space bounding area of the object.
	/// </summary>
	public BoundingBox ObjectBoundingBox => fNL;

	/// <summary>
	/// World space bounding area of the object.
	/// </summary>
	public BoundingBox WorldBoundingBox => fN_0012;

	/// <summary>
	/// World space bounding area of the object.
	/// </summary>
	public BoundingSphere WorldBoundingSphere => fN_0015;

	/// <summary>
	/// Extended world space bounding area of the object. This area is roughly twice the size
	/// to accommodate avatar animations that fall outside the normal bounds.
	/// </summary>
	public BoundingBox WorldBoundingBoxProxy => fN8;

	/// <summary>
	/// AvatarRenderer used to render the avatar.
	/// </summary>
	public AvatarRenderer Renderer => fNj;

	/// <summary>
	/// Description of the avatar size, clothing, features, and more.
	/// </summary>
	public AvatarDescription Description => fNo;

	/// <summary>
	/// Determines if the avatar casts shadows base on the current ObjectVisibility options.
	/// </summary>
	public bool CastShadows => (fNc & ObjectVisibility.CastShadows) != 0;

	/// <summary>
	/// Determines if the avatar is visible base on the current ObjectVisibility options.
	/// </summary>
	public bool Visible => (fNc & ObjectVisibility.Rendered) != 0;

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
			return fNs;
		}
		[CompilerGenerated]
		set
		{
			fNs = value;
		}
	}

	/// <summary>
	/// Container that stores, manages, and updates the object's components.
	/// </summary>
	public ComponentCollection<ISceneEntity> Components => fNw;

	/// <summary>
	/// Event used to update the object at regular intervals. This and all
	/// events are only called on dynamic objects.
	/// </summary>
	public event UpdateDelegate UpdateEvent
	{
		add
		{
			UpdateDelegate updateDelegate = fNa;
			UpdateDelegate updateDelegate2;
			do
			{
				updateDelegate2 = updateDelegate;
				UpdateDelegate value2 = (UpdateDelegate)Delegate.Combine(updateDelegate2, value);
				updateDelegate = Interlocked.CompareExchange(ref fNa, value2, updateDelegate2);
			}
			while ((object)updateDelegate != updateDelegate2);
		}
		remove
		{
			UpdateDelegate updateDelegate = fNa;
			UpdateDelegate updateDelegate2;
			do
			{
				updateDelegate2 = updateDelegate;
				UpdateDelegate value2 = (UpdateDelegate)Delegate.Remove(updateDelegate2, value);
				updateDelegate = Interlocked.CompareExchange(ref fNa, value2, updateDelegate2);
			}
			while ((object)updateDelegate != updateDelegate2);
		}
	}

	static Avatar()
	{
		fNd = new List<Matrix>();
		for (int k = 0; k < 71; k++)
		{
			fNd.Add(Matrix.Identity);
		}
	}

	/// <summary>
	/// Creates a new Avatar instance.
	/// </summary>
	/// <param name="avatarrenderer">AvatarRenderer used to render the avatar.</param>
	/// <param name="description">Description of the avatar size, clothing, features, and more.</param>
	public Avatar(AvatarRenderer avatarrenderer, AvatarDescription description)
	{
		fNw = new ComponentCollection<ISceneEntity>(this);
		fNi = fNd;
		fNn = Matrix.Identity;
		SetRendererAndDescription(avatarrenderer, description);
		_0003A();
	}

	/// <summary>
	/// Changes both the renderer and description used by the avatar.
	/// </summary>
	/// <param name="avatarrenderer">AvatarRenderer used to render the avatar.</param>
	/// <param name="description">Description of the avatar size, clothing, features, and more.</param>
	public void SetRendererAndDescription(AvatarRenderer avatarrenderer, AvatarDescription description)
	{
		fNj = avatarrenderer;
		fNo = description;
		fNL = new BoundingBox(new Vector3(-0.5f, 0f, -0.5f), new Vector3(0.5f, fNo.Height, 0.5f));
		fN_0019 = BoundingSphere.CreateFromBoundingBox(fNL);
		fNv = L._0019.ah(fNL, Matrix.CreateScale(2f));
		_0003A();
	}

	/// <summary>
	/// Deep clones the object including any contained sub-objects and components.
	/// </summary>
	/// <returns></returns>
	public virtual ISceneEntity Clone()
	{
		Avatar avatar = new Avatar(fNj, fNo);
		L.n.ao(this, avatar);
		foreach (IComponent<ISceneEntity> component in fNw.Components)
		{
			avatar.Components.Add(component.Clone());
		}
		return avatar;
	}

	/// <summary>
	/// Updates the object using the provided game time.
	/// </summary>
	/// <param name="gametime"></param>
	public virtual void Update(GameTime gametime)
	{
		fNw.OnUpdate(gametime);
		if (fNa != null)
		{
			fNa(this, gametime);
		}
	}

	/// <summary>
	/// Sets both the avatar bone transforms and expression using an AvatarAnimation object.
	/// </summary>
	/// <param name="animation"></param>
	public void ApplyAnimation(IAvatarAnimation animation)
	{
		fNi = animation.BoneTransforms;
		fN_0005 = animation.Expression;
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
		World = world;
	}

	private void _0003A()
	{
		fN_0012 = L._0019.ah(fNL, fNn);
		fN_0015 = BoundingSphere.CreateFromBoundingBox(fN_0012);
		fN8 = L._0019.ah(fNv, fNn);
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
}
