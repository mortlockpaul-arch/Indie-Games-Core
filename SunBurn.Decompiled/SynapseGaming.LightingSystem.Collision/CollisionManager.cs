using System;
using System.Collections.Generic;
using _0003;
using L;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Rendering;

namespace SynapseGaming.LightingSystem.Collision;

/// <summary>
/// Provides a built-in collision and lightweight physics implementation.
/// </summary>
public class CollisionManager : ICollisionManager, IManagerService, IRenderableManager, IUpdatableManager, IManager, IUnloadable
{
	private const double fNB = 0.0167;

	private const double fNa = 0.04;

	private int fNg = 10;

	private static CollisionMeshBuilder fNf = new CollisionMeshBuilder();

	private bool fN_0003;

	private float fNn;

	private double fN_0019;

	private IManagerServiceProvider fNL;

	private IGraphicsDeviceService fNv;

	private List<SceneEntity> fN_0012 = new List<SceneEntity>(128);

	private CollisionPoint fN_0015 = new CollisionPoint();

	private List<SceneEntity> fN8 = new List<SceneEntity>(16);

	private SystemStatistic fNi = SystemConsole.GetStatistic("Collision_UpdateCalls", SystemStatisticCategory.Collision);

	private SystemStatistic fN_0005 = SystemConsole.GetStatistic("Collision_FindFastCalls", SystemStatisticCategory.Collision);

	private SystemStatistic fNj = SystemConsole.GetStatistic("Collision_SphereContainCalls", SystemStatisticCategory.Collision);

	private SystemStatistic fNo = SystemConsole.GetStatistic("Collision_BoxContainCalls", SystemStatisticCategory.Collision);

	private SystemStatistic fNc = SystemConsole.GetStatistic("Collision_Collisions", SystemStatisticCategory.Collision);

	/// <summary>
	/// Gets the manager specific Type used as a unique key for storing and
	/// requesting the manager from the IManagerServiceProvider.
	/// </summary>
	public Type ManagerType => SceneInterface.CollisionManagerType;

	/// <summary>
	/// Sets the order this manager is processed relative to other managers
	/// in the IManagerServiceProvider. Managers with lower processing order
	/// values are processed first.
	///
	/// In the case of BeginFrameRendering and EndFrameRendering, BeginFrameRendering
	/// is processed in the normal order (lowest order value to highest), however
	/// EndFrameRendering is processed in reverse order (highest to lowest) to ensure
	/// the first manager begun is the last one ended (FILO).
	/// </summary>
	public int ManagerProcessOrder
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
	/// Helper used to construct and cache collision meshes.
	/// </summary>
	public static CollisionMeshBuilder MeshBuilder => fNf;

	/// <summary>
	/// The current GraphicsDeviceManager used by this object.
	/// </summary>
	public IGraphicsDeviceService GraphicsDeviceManager => fNv;

	/// <summary>
	/// Creates a new CollisionManager instance.
	/// </summary>
	/// <param name="graphicsdevicemanager"></param>
	/// <param name="sceneinterface">Service provider used to access all other manager services in this scene.</param>
	public CollisionManager(IGraphicsDeviceService graphicsdevicemanager, IManagerServiceProvider sceneinterface)
	{
		fNL = sceneinterface;
		fNv = graphicsdevicemanager;
	}

	/// <summary>
	/// Disposes any graphics resource used internally by this object, and removes
	/// scene resources managed by this object. Commonly used during Game.UnloadContent.
	/// </summary>
	public virtual void Unload()
	{
		fNf.Unload();
		Clear();
	}

	/// <summary>
	/// Updates all object movement, applies forces, and calculates collisions.
	///
	/// For any calculations to apply BeginFrameRendering() must be called. This prevents
	/// games from slowing down due to receiving several Update() calls per rendered frame.
	/// </summary>
	/// <param name="gametime"></param>
	public virtual void Update(GameTime gametime)
	{
		fN_0019 += gametime.ElapsedGameTime.TotalSeconds;
		if (!fN_0003 || fN_0019 < 0.0167)
		{
			return;
		}
		double num = fN_0019;
		if (num > 0.04)
		{
			num = 0.04;
		}
		double num2 = num / 0.0167;
		if (num2 <= 0.0)
		{
			return;
		}
		fN_0019 = 0.0;
		fNi.AccumulationValue++;
		fN_0003 = false;
		IObjectManager objectManager = (IObjectManager)fNL.GetManager(SceneInterface.ObjectManagerType, required: false);
		if (objectManager == null)
		{
			return;
		}
		LightingSystemPerformance.Begin("CollisionManager.Update");
		fN_0012.Clear();
		objectManager.Find(fN_0012, ObjectFilter.Dynamic | ObjectFilter.Enabled);
		Vector3 worldforce = new Vector3(0f, 0f - L._0019.aw(fNn, num), 0f);
		foreach (SceneEntity item in fN_0012)
		{
			L.w w2 = OptimizationSystem.fNf.g_0011(item);
			ICollisionObject collisionObject = w2.fNa;
			if (collisionObject != null && collisionObject.CollisionType != CollisionType.None && collisionObject.CollisionMove is CollisionMove collisionMove)
			{
				if (collisionObject.AffectedByGravity)
				{
					collisionMove.ApplyWorldForce(worldforce, constantforce: true);
				}
				collisionMove.Begin((float)num2);
			}
		}
		foreach (SceneEntity item2 in fN_0012)
		{
			L.w w3 = OptimizationSystem.fNf.g_0011(item2);
			ICollisionObject collisionObject2 = w3.fNa;
			if (collisionObject2 != null && collisionObject2.CollisionType != CollisionType.None && collisionObject2.CollisionMove is CollisionMove { Distance: not (<=0f) } collisionMove2)
			{
				fN_0015.Clear();
				MoveObject(collisionObject2, objectManager, fN_0015);
				collisionMove2.End(fN_0015);
				if (fN_0015.ContactTime < 1f)
				{
					fNc.AccumulationValue++;
				}
			}
		}
	}

	/// <summary>
	/// Sets up the object prior to rendering.
	/// </summary>
	/// <param name="scenestate"></param>
	public virtual void BeginFrameRendering(ISceneState scenestate)
	{
		fN_0003 = true;
		fNn = scenestate.Environment.Gravity;
	}

	/// <summary>
	/// Finalizes rendering.
	/// </summary>
	public virtual void EndFrameRendering()
	{
	}

	/// <summary>
	/// Simulates the object move, checking for collisions along the way.
	/// </summary>
	/// <param name="movingobj">Object to move (collider).</param>
	/// <param name="objectmanager"></param>
	/// <param name="worldcollisionpoint">Filled with collidee information during the call.</param>
	protected virtual void MoveObject(ICollisionObject movingobj, IObjectManager objectmanager, CollisionPoint worldcollisionpoint)
	{
		LightingSystemPerformance.Begin("CollisionManager.MoveObject (find)");
		if (!(movingobj.CollisionMove is CollisionMove { WorldSweepBoundingBox: var box }))
		{
			return;
		}
		BoundingSphere.CreateFromBoundingBox(ref box, out var result);
		fN8.Clear();
		fN_0005.AccumulationValue++;
		objectmanager.Find(fN8, box, ObjectFilter.All);
		LightingSystemPerformance.Begin("CollisionManager.MoveObject (test)");
		foreach (SceneEntity item in fN8)
		{
			L.w w2 = OptimizationSystem.fNf.g_0011(item);
			ICollisionObject collisionObject = w2.fNa;
			if (collisionObject == null || collisionObject == movingobj || collisionObject.CollisionType == CollisionType.None)
			{
				continue;
			}
			ICollisionMove collisionMove2 = collisionObject.CollisionMove;
			fNj.AccumulationValue++;
			collisionMove2.WorldBoundingSphere.Contains(ref result, out var result2);
			if (result2 != ContainmentType.Disjoint)
			{
				fNo.AccumulationValue++;
				collisionMove2.WorldBoundingBox.Contains(ref box, out result2);
				if (result2 != ContainmentType.Disjoint)
				{
					Collide(movingobj, collisionObject, worldcollisionpoint);
				}
			}
		}
	}

	/// <summary>
	/// Tests for collision between two objects.  Assumes CollisionMove.Begin()
	/// is already called on the moving object.
	/// </summary>
	/// <param name="movingobj">Moving / collider object.</param>
	/// <param name="staticobj">Potential collidee.</param>
	/// <param name="worldcollisionpoint">Filled with collidee information during the call.</param>
	public virtual void Collide(ICollisionObject movingobj, ICollisionObject staticobj, CollisionPoint worldcollisionpoint)
	{
		LightingSystemPerformance.Begin("CollisionManager.Collide");
		if (movingobj.HullType == HullType.Sphere)
		{
			if (staticobj.HullType == HullType.Sphere)
			{
				_0003.g.J(movingobj, staticobj, worldcollisionpoint);
			}
			else if (staticobj.HullType == HullType.Box)
			{
				_0003.g._0006(movingobj, staticobj, worldcollisionpoint);
			}
			else if (staticobj.HullType == HullType.Mesh)
			{
				_0003.g._0004(movingobj, staticobj, worldcollisionpoint);
			}
		}
		else if (movingobj.HullType == HullType.Box)
		{
			if (staticobj.HullType == HullType.Sphere)
			{
				_0003.a._0(movingobj, staticobj, worldcollisionpoint);
			}
			else if (staticobj.HullType == HullType.Box)
			{
				_0003.a.K(movingobj, staticobj, worldcollisionpoint);
			}
			else if (staticobj.HullType == HullType.Mesh)
			{
				_0003.a.M(movingobj, staticobj, worldcollisionpoint);
			}
		}
	}

	/// <summary>
	/// Removes resources managed by this object. Commonly used while clearing the scene.
	/// </summary>
	public virtual void Clear()
	{
	}

	/// <summary>
	/// Use to apply user quality and performance preferences to the resources managed by this object.
	/// </summary>
	/// <param name="preferences"></param>
	public virtual void ApplyPreferences(ILightingSystemPreferences preferences)
	{
	}
}
