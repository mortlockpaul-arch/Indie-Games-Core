using System;
using System.Linq;
using System.Runtime.CompilerServices;
using DigitalRune.Geometry;
using DigitalRune.Geometry.Collisions;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Physics.Specialized;

/// <summary>
///       Represents a single wheel of a <see cref="P:DigitalRune.Physics.Specialized.Wheel.Vehicle" />.
///       </summary>
public class Wheel
{
	private RigidBody c0e67846c640e0e5e5031d64fdf7c95bb;

	private Vehicle cab9fa13df545c61d1fc7f3e85e7a9816;

	private float c877163febefd3e6c27333c7b0285fa0b;

	private float c9e16c795435c3bc8854e7915ccdb5282;

	private Vector3F ce0e4385114c5600316445524084b0a64;

	[CompilerGenerated]
	private float c20bb90b02a5bc4f38c4a6f1c87f56024;

	[CompilerGenerated]
	private float c935e04c113ed24ae13a0b2729a39280d;

	[CompilerGenerated]
	private float c6973bc489876e44ab8dab244e2feef14;

	[CompilerGenerated]
	private float cb56d26d9c78b2cf26d77397660024434;

	[CompilerGenerated]
	private float c16e03535cb48a2c89194dc08bbe90a0b;

	[CompilerGenerated]
	private float c2f7747d07caa7de4d1c92c80dc3dabad;

	[CompilerGenerated]
	private float c4db9d9088185cc1d20b706655492b978;

	[CompilerGenerated]
	private float c5c5d10ef1fc43aa6cca448cc523c2685;

	[CompilerGenerated]
	private float ccb432e2e416652736b56e61f6b9d1185;

	[CompilerGenerated]
	private float cf8f023f5a94cb9df97390e51ac6bba71;

	[CompilerGenerated]
	private RayShape c86618a43d81402e0ba56377d10b6f6ee;

	[CompilerGenerated]
	private GeometricObject c4a14464875454ca734e27308b0c4a9f9;

	[CompilerGenerated]
	private CollisionObject c9f70e8b23bf60595b29e28c6f9beb85d;

	[CompilerGenerated]
	private Vector3F c1abee3261f6a8e43ef18d87c75c7c426;

	[CompilerGenerated]
	private Vector3F cf73d01ed20bb4d43c47c0ed3456e9702;

	[CompilerGenerated]
	private bool c4306177e6ad447930bb4488180771ec6;

	[CompilerGenerated]
	private float cd82ace8508b6f74e0c528e55f88f2c43;

	[CompilerGenerated]
	private float c2c31ace903b0977a4f4e258749073b7c;

	[CompilerGenerated]
	private RigidBody c9832b1259af077901a4fa5d9b7c5b376;

	[CompilerGenerated]
	private float c23fe913c7aea4402d130eecd3d1f91f3;

	[CompilerGenerated]
	private float c962ab887c0bd52f2acc37940b31dd014;

	[CompilerGenerated]
	private float cdab79cb007a65dcc3b21ec9ec8564750;

	/// <summary>
	///       Gets (or sets) the vehicle to which the wheel is attached.
	///       </summary>
	public Vehicle Vehicle
	{
		get
		{
			return cab9fa13df545c61d1fc7f3e85e7a9816;
		}
		internal set
		{
			cab9fa13df545c61d1fc7f3e85e7a9816 = value;
			c7dff647aa7bb9df13d5440c878eb477d();
		}
	}

	/// <summary>
	///       Gets or sets the radius of the wheel. 
	///       </summary>
	public float Radius
	{
		get
		{
			return c877163febefd3e6c27333c7b0285fa0b;
		}
		set
		{
			if (c877163febefd3e6c27333c7b0285fa0b == value)
			{
				while (true)
				{
					switch (4)
					{
					case 0:
						break;
					default:
						if (1 == 0)
						{
							__ldtoken(Wheel.set_Radius(float));
						}
						return;
					}
				}
			}
			c877163febefd3e6c27333c7b0285fa0b = value;
			if (Ray != null)
			{
				Ray.Length = c877163febefd3e6c27333c7b0285fa0b + SuspensionRestLength;
			}
		}
	}

	/// <summary>
	///       Gets or sets the rest length of the suspension.
	///       </summary>
	public float SuspensionRestLength
	{
		get
		{
			return c9e16c795435c3bc8854e7915ccdb5282;
		}
		set
		{
			if (c9e16c795435c3bc8854e7915ccdb5282 == value)
			{
				return;
			}
			c9e16c795435c3bc8854e7915ccdb5282 = value;
			if (Ray == null)
			{
				return;
			}
			while (true)
			{
				switch (6)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(Wheel.set_SuspensionRestLength(float));
				}
				Ray.Length = Radius + c9e16c795435c3bc8854e7915ccdb5282;
				return;
			}
		}
	}

	/// <summary>
	///       Gets or sets the suspension stiffness.
	///       </summary>
	public float SuspensionStiffness
	{
		[CompilerGenerated]
		get
		{
			return c20bb90b02a5bc4f38c4a6f1c87f56024;
		}
		[CompilerGenerated]
		set
		{
			c20bb90b02a5bc4f38c4a6f1c87f56024 = value;
		}
	}

	/// <summary>
	///       Gets or sets the suspension damping during relaxation.
	///       </summary>
	public float SuspensionRelaxationDamping
	{
		[CompilerGenerated]
		get
		{
			return c935e04c113ed24ae13a0b2729a39280d;
		}
		[CompilerGenerated]
		set
		{
			c935e04c113ed24ae13a0b2729a39280d = value;
		}
	}

	/// <summary>
	///       Gets or sets the suspension damping during compression.
	///       </summary>
	public float SuspensionCompressionDamping
	{
		[CompilerGenerated]
		get
		{
			return c6973bc489876e44ab8dab244e2feef14;
		}
		[CompilerGenerated]
		set
		{
			c6973bc489876e44ab8dab244e2feef14 = value;
		}
	}

	/// <summary>
	///       Gets or sets the max suspension force.
	///       </summary>
	public float MaxSuspensionForce
	{
		[CompilerGenerated]
		get
		{
			return cb56d26d9c78b2cf26d77397660024434;
		}
		[CompilerGenerated]
		set
		{
			cb56d26d9c78b2cf26d77397660024434 = value;
		}
	}

	/// <summary>
	///       Gets or sets the rolling friction force.
	///       </summary>
	public float RollingFrictionForce
	{
		[CompilerGenerated]
		get
		{
			return c16e03535cb48a2c89194dc08bbe90a0b;
		}
		[CompilerGenerated]
		set
		{
			c16e03535cb48a2c89194dc08bbe90a0b = value;
		}
	}

	/// <summary>
	///       Gets or sets the friction.
	///       </summary>
	public float Friction
	{
		[CompilerGenerated]
		get
		{
			return c2f7747d07caa7de4d1c92c80dc3dabad;
		}
		[CompilerGenerated]
		set
		{
			c2f7747d07caa7de4d1c92c80dc3dabad = value;
		}
	}

	/// <summary>
	///       Gets or sets the roll reduction.
	///       </summary>
	public float RollReduction
	{
		[CompilerGenerated]
		get
		{
			return c4db9d9088185cc1d20b706655492b978;
		}
		[CompilerGenerated]
		set
		{
			c4db9d9088185cc1d20b706655492b978 = value;
		}
	}

	/// <summary>
	///       Gets or sets the wheel offset.
	///       </summary>
	public Vector3F Offset
	{
		get
		{
			return ce0e4385114c5600316445524084b0a64;
		}
		set
		{
			ce0e4385114c5600316445524084b0a64 = value;
			if (c0e67846c640e0e5e5031d64fdf7c95bb == null)
			{
				return;
			}
			while (true)
			{
				switch (4)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(Wheel.set_Offset(Vector3F));
				}
				c3c12ccb0e0feb2f78a298059c5a1223b(null, null);
				return;
			}
		}
	}

	/// <summary>
	///       Gets or sets the steering angle.
	///       </summary>
	public float SteeringAngle
	{
		[CompilerGenerated]
		get
		{
			return c5c5d10ef1fc43aa6cca448cc523c2685;
		}
		[CompilerGenerated]
		set
		{
			c5c5d10ef1fc43aa6cca448cc523c2685 = value;
		}
	}

	/// <summary>
	///       Gets or sets the motor force.
	///       </summary>
	public float MotorForce
	{
		[CompilerGenerated]
		get
		{
			return ccb432e2e416652736b56e61f6b9d1185;
		}
		[CompilerGenerated]
		set
		{
			ccb432e2e416652736b56e61f6b9d1185 = value;
		}
	}

	/// <summary>
	///       Gets or sets the brake force.
	///       </summary>
	public float BrakeForce
	{
		[CompilerGenerated]
		get
		{
			return cf8f023f5a94cb9df97390e51ac6bba71;
		}
		[CompilerGenerated]
		set
		{
			cf8f023f5a94cb9df97390e51ac6bba71 = value;
		}
	}

	/// <summary>
	///       Gets the ray.
	///       </summary>
	public RayShape Ray
	{
		[CompilerGenerated]
		get
		{
			return c86618a43d81402e0ba56377d10b6f6ee;
		}
		[CompilerGenerated]
		private set
		{
			c86618a43d81402e0ba56377d10b6f6ee = value;
		}
	}

	/// <summary>
	///       Gets the geometric object that defines the pose of the ray.
	///       </summary>
	public GeometricObject GeometricObject
	{
		[CompilerGenerated]
		get
		{
			return c4a14464875454ca734e27308b0c4a9f9;
		}
		[CompilerGenerated]
		private set
		{
			c4a14464875454ca734e27308b0c4a9f9 = value;
		}
	}

	/// <summary>
	///       Gets the collision object that defines collision detection behavior of the ray.
	///       </summary>
	public CollisionObject CollisionObject
	{
		[CompilerGenerated]
		get
		{
			return c9f70e8b23bf60595b29e28c6f9beb85d;
		}
		[CompilerGenerated]
		private set
		{
			c9f70e8b23bf60595b29e28c6f9beb85d = value;
		}
	}

	internal bool c8bdc38c9ecd02f560f18d792aa723d13
	{
		get
		{
			return CollisionObject.Domain != null;
		}
		set
		{
			if (c8bdc38c9ecd02f560f18d792aa723d13 != value)
			{
				if (value)
				{
					Vehicle.Simulation.CollisionDomain.CollisionObjects.Add(CollisionObject);
					CollisionFilter collisionFilter = (CollisionFilter)Vehicle.Simulation.CollisionDomain.CollisionDetection.CollisionFilter;
					collisionFilter.Set(Vehicle.Chassis.CollisionObject, CollisionObject, collisionsEnabled: false);
				}
				else
				{
					CollisionFilter collisionFilter2 = (CollisionFilter)Vehicle.Simulation.CollisionDomain.CollisionDetection.CollisionFilter;
					collisionFilter2.Set(Vehicle.Chassis.CollisionObject, CollisionObject, collisionsEnabled: true);
					Vehicle.Simulation.CollisionDomain.CollisionObjects.Remove(CollisionObject);
				}
			}
		}
	}

	/// <summary>
	///       Gets the position where the wheel touches the ground.
	///       </summary>
	public Vector3F GroundPosition
	{
		[CompilerGenerated]
		get
		{
			return c1abee3261f6a8e43ef18d87c75c7c426;
		}
		[CompilerGenerated]
		private set
		{
			c1abee3261f6a8e43ef18d87c75c7c426 = value;
		}
	}

	/// <summary>
	///       Gets the normal vector of the ground at the <see cref="P:DigitalRune.Physics.Specialized.Wheel.GroundPosition" />.
	///       </summary>
	public Vector3F GroundNormal
	{
		[CompilerGenerated]
		get
		{
			return cf73d01ed20bb4d43c47c0ed3456e9702;
		}
		[CompilerGenerated]
		private set
		{
			cf73d01ed20bb4d43c47c0ed3456e9702 = value;
		}
	}

	/// <summary>
	///       Gets a value indicating whether this wheel has ground contact.
	///       </summary>
	public bool HasGroundContact
	{
		[CompilerGenerated]
		get
		{
			return c4306177e6ad447930bb4488180771ec6;
		}
		[CompilerGenerated]
		private set
		{
			c4306177e6ad447930bb4488180771ec6 = value;
		}
	}

	/// <summary>
	///       Gets the the current suspension length.
	///       </summary>
	public float SuspensionLength
	{
		[CompilerGenerated]
		get
		{
			return cd82ace8508b6f74e0c528e55f88f2c43;
		}
		[CompilerGenerated]
		set
		{
			cd82ace8508b6f74e0c528e55f88f2c43 = value;
		}
	}

	internal float PreviousSuspensionLength
	{
		[CompilerGenerated]
		get
		{
			return c2c31ace903b0977a4f4e258749073b7c;
		}
		[CompilerGenerated]
		set
		{
			c2c31ace903b0977a4f4e258749073b7c = value;
		}
	}

	/// <summary>
	///       Gets the touched body (= the ground object).
	///       </summary>
	public RigidBody TouchedBody
	{
		[CompilerGenerated]
		get
		{
			return c9832b1259af077901a4fa5d9b7c5b376;
		}
		[CompilerGenerated]
		private set
		{
			c9832b1259af077901a4fa5d9b7c5b376 = value;
		}
	}

	/// <summary>
	///       Gets the pose of the wheel in world space.
	///       </summary>
	public Pose Pose
	{
		get
		{
			if (c0e67846c640e0e5e5031d64fdf7c95bb == null)
			{
				while (true)
				{
					switch (4)
					{
					case 0:
						break;
					default:
						if (1 == 0)
						{
							__ldtoken(Wheel.get_Pose());
						}
						return Pose.Identity;
					}
				}
			}
			Pose pose = c0e67846c640e0e5e5031d64fdf7c95bb.Pose;
			Vector3F vector3F = pose.Position + pose.ToWorldDirection(Offset);
			Vector3F position = vector3F + SuspensionLength * pose.ToWorldDirection(-Vector3F.UnitY);
			Matrix33F orientation = pose.Orientation * Matrix33F.CreateRotationY(SteeringAngle) * Matrix33F.CreateRotationX(0f - RotationAngle);
			return new Pose(position, orientation);
		}
	}

	/// <summary>
	///       Gets or sets the angular velocity of the wheel about the rotation axis.
	///       </summary>
	public float AngularVelocity
	{
		[CompilerGenerated]
		get
		{
			return c23fe913c7aea4402d130eecd3d1f91f3;
		}
		[CompilerGenerated]
		internal set
		{
			c23fe913c7aea4402d130eecd3d1f91f3 = value;
		}
	}

	/// <summary>
	///       Gets or sets the rotation angle about the rolling axis.
	///       </summary>
	public float RotationAngle
	{
		[CompilerGenerated]
		get
		{
			return c962ab887c0bd52f2acc37940b31dd014;
		}
		[CompilerGenerated]
		internal set
		{
			c962ab887c0bd52f2acc37940b31dd014 = value;
		}
	}

	/// <summary>
	///       Gets or sets the skid energy.
	///       </summary>
	public float SkidEnergy
	{
		[CompilerGenerated]
		get
		{
			return cdab79cb007a65dcc3b21ec9ec8564750;
		}
		[CompilerGenerated]
		internal set
		{
			cdab79cb007a65dcc3b21ec9ec8564750 = value;
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Physics.Specialized.Wheel" /> class.
	///       </summary>
	public Wheel()
	{
		Radius = 0.4f;
		SuspensionRestLength = 0.6f;
		SuspensionLength = SuspensionRestLength;
		PreviousSuspensionLength = SuspensionRestLength;
		SuspensionStiffness = 20f;
		SuspensionCompressionDamping = 4f;
		SuspensionRelaxationDamping = 3f;
		MaxSuspensionForce = 6000f;
		RollingFrictionForce = 500f;
		Friction = 0.9f;
		RollReduction = 0.3f;
		Vector3F zero = Vector3F.Zero;
		Vector3F direction = -Vector3F.UnitY;
		float length = Radius + SuspensionRestLength;
		Ray = new RayShape(zero, direction, length)
		{
			StopsAtFirstHit = true
		};
		GeometricObject = new GeometricObject(Ray);
		CollisionObject = new CollisionObject(GeometricObject);
	}

	internal void c7dff647aa7bb9df13d5440c878eb477d()
	{
		if (c0e67846c640e0e5e5031d64fdf7c95bb != null)
		{
			while (true)
			{
				switch (3)
				{
				case 0:
					continue;
				}
				break;
			}
			if (1 == 0)
			{
				__ldtoken(Wheel.c7dff647aa7bb9df13d5440c878eb477d());
			}
			c0e67846c640e0e5e5031d64fdf7c95bb.PoseChanged -= c3c12ccb0e0feb2f78a298059c5a1223b;
		}
		c0e67846c640e0e5e5031d64fdf7c95bb = Vehicle.Chassis;
		if (c0e67846c640e0e5e5031d64fdf7c95bb == null)
		{
			return;
		}
		while (true)
		{
			switch (4)
			{
			case 0:
				continue;
			}
			c0e67846c640e0e5e5031d64fdf7c95bb.PoseChanged += c3c12ccb0e0feb2f78a298059c5a1223b;
			c3c12ccb0e0feb2f78a298059c5a1223b(null, null);
			return;
		}
	}

	private void c3c12ccb0e0feb2f78a298059c5a1223b(object c807f682152464d11dde83a885bf87124, EventArgs c7fefeb91d4632901bd52a1f245b70db1)
	{
		Pose pose = c0e67846c640e0e5e5031d64fdf7c95bb.Pose;
		Vector3F position = pose.Position + pose.ToWorldDirection(Offset);
		GeometricObject.Pose = new Pose(position, pose.Orientation);
	}

	internal void c12c24226dfa6f48adef0c6b19cc54b9c()
	{
		ContactSet contactSet = CollisionObject.Domain.GetContacts(CollisionObject).FirstOrDefault();
		if (contactSet != null && contactSet.Count > 0)
		{
			Contact contact = contactSet[0];
			HasGroundContact = true;
			GroundPosition = contactSet[0].Position;
			GroundNormal = ((CollisionObject == contactSet.ObjectA) ? (-contact.Normal) : contact.Normal);
			IGeometricObject geometricObject;
			if (CollisionObject != contactSet.ObjectA)
			{
				while (true)
				{
					switch (3)
					{
					case 0:
						continue;
					}
					break;
				}
				if (1 == 0)
				{
					__ldtoken(Wheel.c12c24226dfa6f48adef0c6b19cc54b9c());
				}
				geometricObject = contactSet.ObjectA.GeometricObject;
			}
			else
			{
				geometricObject = contactSet.ObjectB.GeometricObject;
			}
			TouchedBody = geometricObject as RigidBody;
			float penetrationDepth = contact.PenetrationDepth;
			SuspensionLength = Math.Max(penetrationDepth - Radius, Radius);
		}
		else
		{
			HasGroundContact = false;
			TouchedBody = null;
			SuspensionLength = SuspensionRestLength;
		}
	}
}
