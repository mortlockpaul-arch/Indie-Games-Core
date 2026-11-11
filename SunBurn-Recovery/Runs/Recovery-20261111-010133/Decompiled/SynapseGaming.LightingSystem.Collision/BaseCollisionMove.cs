using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using SynapseGaming.LightingSystem.Core;

namespace SynapseGaming.LightingSystem.Collision;

public abstract class BaseCollisionMove : ICollisionMove
{
	private ICollisionObject l8a;

	private ICollisionEntity l8_u0013_;

	[CompilerGenerated]
	private float l8N;

	[CompilerGenerated]
	private Vector3 l8l;

	public float Distance
	{
		[CompilerGenerated]
		get
		{
			return l8N;
		}
		[CompilerGenerated]
		protected set
		{
			l8N = value;
		}
	}

	public Vector3 Normal
	{
		[CompilerGenerated]
		get
		{
			return l8l;
		}
		[CompilerGenerated]
		protected set
		{
			l8l = value;
		}
	}

	public ICollisionObject ParentObject => l8a;

	public ICollisionEntity CollisionEntity => l8_u0013_;

	public BaseCollisionMove(ICollisionObject parent)
	{
		l8a = parent;
	}

	private bool _id_2()
	{
		if (l8_u0013_ != null && l8a.CollisionType == CollisionType.Collide)
		{
			return l8a.UpdateType == UpdateType.Automatic;
		}
		return false;
	}

	public virtual void ApplyObjectForce(Vector3 objectforce)
	{
		if (_id_2())
		{
			Vector3 worldforce = Vector3.TransformNormal(objectforce, l8a.World);
			l8_u0013_.ApplyWorldForce(ref worldforce);
		}
	}

	public virtual void ApplyObjectForce(Vector3 objectposition, Vector3 objectforce)
	{
		if (_id_2())
		{
			Matrix matrix = l8a.World;
			Vector3.Transform(ref objectposition, ref matrix, out var result);
			Vector3.TransformNormal(ref objectforce, ref matrix, out var result2);
			l8_u0013_.ApplyWorldForce(ref result, ref result2);
		}
	}

	public virtual void ApplyWorldForce(Vector3 worldforce, bool constantforce)
	{
		if (_id_2())
		{
			l8_u0013_.ApplyWorldForce(ref worldforce);
		}
	}

	public virtual void ApplyWorldForce(Vector3 worldposition, Vector3 worldforce)
	{
		if (_id_2())
		{
			l8_u0013_.ApplyWorldForce(ref worldposition, ref worldforce);
		}
	}

	public virtual void Begin()
	{
		if (l8_u0013_ != null)
		{
			if (l8_u0013_.CheckSceneObjectChanged())
			{
				l8_u0013_.SyncToPhysicsEntity();
			}
			if (l8a.UpdateType == UpdateType.Automatic)
			{
				Distance = l8_u0013_.Distance;
				Normal = l8_u0013_.Normal;
			}
		}
	}

	public virtual void End()
	{
		if (l8_u0013_ != null && l8a.UpdateType != UpdateType.None)
		{
			l8_u0013_.SyncToSceneObject();
		}
	}

	public virtual void RemoveForces()
	{
		if (l8_u0013_ != null)
		{
			l8_u0013_.RemoveForces();
		}
	}

	public virtual void OnRemovedFromManager(IManagerService manager)
	{
		if (l8_u0013_ != null)
		{
			l8_u0013_.Dispose();
			l8_u0013_ = null;
		}
	}

	public virtual void OnSubmittedToManager(IManagerService manager)
	{
		if (l8a.CollisionType != CollisionType.None)
		{
			if (l8_u0013_ != null)
			{
				l8_u0013_.Dispose();
			}
			l8_u0013_ = CreateCollisionEntity();
		}
	}

	protected abstract ICollisionEntity CreateCollisionEntity();
}
