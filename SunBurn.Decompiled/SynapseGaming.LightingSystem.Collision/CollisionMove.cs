using System;
using Microsoft.Xna.Framework;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Rendering;

namespace SynapseGaming.LightingSystem.Collision;

/// <summary>
/// Provides the built-in implement of object collision movement
/// and applying force.
/// </summary>
public class CollisionMove : ICollisionMove
{
	private CollisionMesh fNB;

	private ICollisionObject fNa;

	private bool fNg = true;

	private bool fNf;

	private int fN_0003 = -1;

	private Plane fNn;

	private float fN_0019;

	private Vector3 fNL;

	private Vector3 fNv;

	private Vector3 fN_0012;

	private Vector3 fN_0015;

	private Matrix fN8;

	private BoundingSphere fNi;

	private BoundingBox fN_0005;

	private BoundingBox fNj;

	public bool SlidingEnabled
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
	/// Determines if the object is currently contacting another object or surface.
	/// </summary>
	public bool OnSurface => fNf;

	public Plane CollisionSurface => fNn;

	/// <summary>
	/// Distance the object will move this frame. Valid after calling Begin().
	/// </summary>
	public float Distance => fN_0019;

	/// <summary>
	/// Direction the object will move this frame. Valid after calling Begin().
	/// </summary>
	public Vector3 Normal => fN_0015;

	/// <summary>
	/// Amount of force applied to the object this frame from constant
	/// sources such as gravity, wind, or similar.
	///
	/// The amount of force applied to the object is affected by the
	/// collision time period (see GetForce() for details).
	/// </summary>
	public Vector3 ForceConstant
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
	/// Amount of force applied to the object this frame from impulse
	/// sources such as impacts, exlposions, and similar.
	///
	/// The amount of force applied to the object is NOT affected by the
	/// collision time period (see GetForce() for details).
	/// </summary>
	public Vector3 ForceImpulse
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
	/// Amount of force carried over into the next frame due to collisions.
	/// </summary>
	public Vector3 CarryOver
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
	/// World bounding area the object will move to this frame.
	/// </summary>
	public BoundingSphere WorldBoundingSphere => fNi;

	/// <summary>
	/// World bounding area the object will move to this frame.
	/// </summary>
	public BoundingBox WorldBoundingBox => fN_0005;

	/// <summary>
	/// Total world bounding area of the object movement. This includes
	/// both the current and next world bounding areas.
	/// </summary>
	public BoundingBox WorldSweepBoundingBox => fNj;

	/// <summary>
	/// Access to the world space geometry used for collision.
	/// </summary>
	public CollisionMesh WorldCollisionMesh
	{
		get
		{
			int moveId = fNa.MoveId;
			if (fN_0003 == moveId)
			{
				return fNB;
			}
			if (fNB == null)
			{
				fNB = new CollisionMesh();
			}
			if (fNa is ISceneObject sceneObject)
			{
				CollisionManager.MeshBuilder.BuildWorldMesh(sceneObject.RenderableMeshes, fNB);
			}
			fN_0003 = moveId;
			return fNB;
		}
	}

	/// <summary>
	/// Creates a new CollisionMove instance.
	/// </summary>
	/// <param name="parent">Collision object to move.</param>
	public CollisionMove(ICollisionObject parent)
	{
		fNa = parent;
	}

	/// <summary>
	/// Applies force to the object. The total force is used to move the
	/// object during the next call to Begin().
	/// </summary>
	/// <param name="objectforce">Amount of object-space force to apply to the object.</param>
	public void ApplyObjectForce(Vector3 objectforce)
	{
		Matrix matrix = fNa.World;
		Vector3.TransformNormal(ref objectforce, ref matrix, out var result);
		fNv.X += result.X;
		fNv.Y += result.Y;
		fNv.Z += result.Z;
	}

	/// <summary>
	/// Applies force to the object. The total force is used to move the
	/// object during the next call to Begin().
	/// </summary>
	/// <param name="worldforce">Amount of world-space force to apply to the object.</param>
	/// <param name="constantforce">Determines if the force is from a constant
	/// source such as gravity, wind, or similar (eg: applied by the caller
	/// every frame instead of a single time).</param>
	public void ApplyWorldForce(Vector3 worldforce, bool constantforce)
	{
		if (constantforce)
		{
			fNL.X += worldforce.X;
			fNL.Y += worldforce.Y;
			fNL.Z += worldforce.Z;
		}
		else
		{
			fNv.X += worldforce.X;
			fNv.Y += worldforce.Y;
			fNv.Z += worldforce.Z;
		}
	}

	/// <summary>
	/// Removes all accumulated forces acting on the object. This will halt the object
	/// movement, however future forces (such as gravity) can immediately begin acting
	/// on the object again.
	/// </summary>
	public void RemoveForces()
	{
		fN_0012 = (fNL = (fNv = Vector3.Zero));
	}

	/// <summary>
	/// Gets the cumulative amount of force affecting the object for the time period.
	/// </summary>
	/// <param name="timeperiod">Normalized time period from 0.0 (start of the
	/// movement this frame) to 1.0 (end of the movement this frame).</param>
	/// <returns></returns>
	public Vector3 GetForce(float timeperiod)
	{
		return fNL * timeperiod + fNv;
	}

	/// <summary>
	/// Prepares the object for movement this frame. Also calculates related
	/// volumes for collision detection.
	/// </summary>
	/// <param name="numberofvirtualupdates">Number of virtual frames that elapsed
	/// since the last call to Begin(). Allows collisions to be calculated without
	/// using a fixed time step.</param>
	public void Begin(float numberofvirtualupdates)
	{
		Vector3 vector = (fNL + fNv) * numberofvirtualupdates;
		if (numberofvirtualupdates > 0f)
		{
			vector += fN_0012;
			fN_0012 = Vector3.Zero;
		}
		fN_0019 = vector.Length();
		if (fN_0019 <= 0f)
		{
			fN_0015 = Vector3.Zero;
		}
		else
		{
			float num = 1f / fN_0019;
			fN_0015 = vector * num;
		}
		fNi = fNa.WorldBoundingSphere;
		fNi.Center.X += vector.X;
		fNi.Center.Y += vector.Y;
		fNi.Center.Z += vector.Z;
		BoundingBox additional = (fN_0005 = fNa.WorldBoundingBox);
		fN_0005.Min.X += vector.X;
		fN_0005.Min.Y += vector.Y;
		fN_0005.Min.Z += vector.Z;
		fN_0005.Max.X += vector.X;
		fN_0005.Max.Y += vector.Y;
		fN_0005.Max.Z += vector.Z;
		BoundingBox.CreateMerged(ref fN_0005, ref additional, out fNj);
		fN8 = fNa.World;
		fN8.Translation += vector;
	}

	/// <summary>
	/// Finishes the object move and changes the object position to the specified
	/// world collision point.
	/// </summary>
	/// <param name="worldcollisionpoint">Contains information about the closest
	/// collision point to the object.</param>
	public void End(CollisionPoint worldcollisionpoint)
	{
		ICollisionObject collisionObject = fNa;
		foreach (ICollisionObject trigger in worldcollisionpoint.Triggers)
		{
			collisionObject.OnCollisionTrigger(collisionObject, trigger);
			trigger.OnCollisionTrigger(collisionObject, trigger);
		}
		if (fN_0019 <= 0f)
		{
			V();
		}
		else if (worldcollisionpoint.ContactTime < 1f)
		{
			Matrix world = fNa.World;
			world.Translation += fN_0015 * (fN_0019 * worldcollisionpoint.ContactTime);
			collisionObject.React(worldcollisionpoint);
			collisionObject.SetWorldAndWorldToObject(world, Matrix.Invert(world));
			fNf = true;
			fNn.Normal = worldcollisionpoint.SurfaceNormal;
			fNn.D = 0f - Vector3.Dot(worldcollisionpoint.SurfaceNormal, worldcollisionpoint.ContactPoint);
			V();
		}
		else
		{
			collisionObject.World = fN8;
			fNf = false;
			V();
		}
	}

	private void V()
	{
		fNv += fNL;
		fNL = Vector3.Zero;
	}

	/// <summary>
	/// Calculates and applies the reaction force between the
	/// object and the collision surface contained in the CollisionPoint.
	/// </summary>
	/// <param name="worldcollisionpoint">Contains information about the closest collision point to the collider.</param>
	public virtual void React(CollisionPoint worldcollisionpoint)
	{
		ICollisionObject contactObject = worldcollisionpoint.ContactObject;
		float mass = fNa.Mass;
		float num = 0.5f;
		float num2 = mass + contactObject.Mass;
		if (num2 > 0f)
		{
			num = mass / num2;
		}
		ICollisionMaterial defaultCollisionMaterial = fNa.DefaultCollisionMaterial;
		float num3 = ((defaultCollisionMaterial == null) ? 0.75f : (1f - defaultCollisionMaterial.Elasticity));
		if (worldcollisionpoint.Material != null)
		{
			num3 *= 1f - worldcollisionpoint.Material.Elasticity;
		}
		Vector3 vector = fN_0015;
		Vector3 vector2 = worldcollisionpoint.SurfaceNormal;
		Vector3.Dot(ref vector, ref vector2, out var result);
		result = (1f - Math.Abs(result)) * 2f - 1f;
		result = MathHelper.Clamp(result, 0f, 1f);
		float num4 = result * 0.01f;
		if (!fNg)
		{
			vector -= vector2 * num4 * 10f;
		}
		else if (num4 > 0f)
		{
			vector -= vector2 * num4;
			float num5 = 0.8f;
			float num6 = 2f;
			float num7 = worldcollisionpoint.ContactTime - num5;
			num7 *= num6;
			num7 = MathHelper.Clamp(1f - num7, 0f, 1f);
			num3 = MathHelper.Lerp(num3, 1f, result * num7);
		}
		Vector3 force = GetForce(worldcollisionpoint.ContactTime);
		Vector3.Reflect(ref vector, ref vector2, out var result2);
		Vector3 vector3 = result2 * force.Length() * num3;
		fN_0012 = result2 * (fN_0019 * num3 * (1f - worldcollisionpoint.ContactTime));
		if (contactObject.UpdateType == UpdateType.None)
		{
			fNv = vector3;
			fNL = Vector3.Zero;
			return;
		}
		Vector3 vector4 = vector3 * (1f - num);
		Vector3 vector5 = vector3 * num;
		fNv = vector4;
		fNL = Vector3.Zero;
		contactObject.CollisionMove.ApplyWorldForce(-vector5, constantforce: false);
	}
}
