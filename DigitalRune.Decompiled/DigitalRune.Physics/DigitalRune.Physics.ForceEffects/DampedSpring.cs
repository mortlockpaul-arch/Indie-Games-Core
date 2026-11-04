using System.Runtime.CompilerServices;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Physics.ForceEffects;

/// <summary>
///       Connects to rigid bodies with a damped spring.
///       </summary>
public class DampedSpring : ForceEffect
{
	[CompilerGenerated]
	private RigidBody ca2c2623ac90d2f94949a3d8866847b57;

	[CompilerGenerated]
	private RigidBody cf36548e635e5cc1183bb0e3024c7123b;

	[CompilerGenerated]
	private float c8f29d37d441bd9e83843d838e4c726b4;

	[CompilerGenerated]
	private float c48d29a58571e20b96fbf85ba73e2f9de;

	[CompilerGenerated]
	private Vector3F c85ce3433ac5ea4f5d375e0c064299d81;

	[CompilerGenerated]
	private Vector3F c160ab8b1d9a3c74bc2c191b2a2e0b685;

	[CompilerGenerated]
	private float c89ee6d3cffcfb8089261ab560c605de8;

	/// <summary>
	///       Gets or sets the first rigid body.
	///       </summary>
	public RigidBody BodyA
	{
		[CompilerGenerated]
		get
		{
			return ca2c2623ac90d2f94949a3d8866847b57;
		}
		[CompilerGenerated]
		set
		{
			ca2c2623ac90d2f94949a3d8866847b57 = value;
		}
	}

	/// <summary>
	///       Gets or sets the second rigid body.
	///       </summary>
	public RigidBody BodyB
	{
		[CompilerGenerated]
		get
		{
			return cf36548e635e5cc1183bb0e3024c7123b;
		}
		[CompilerGenerated]
		set
		{
			cf36548e635e5cc1183bb0e3024c7123b = value;
		}
	}

	/// <summary>
	///       Gets or sets the damping constant.
	///       </summary>
	public float DampingConstant
	{
		[CompilerGenerated]
		get
		{
			return c8f29d37d441bd9e83843d838e4c726b4;
		}
		[CompilerGenerated]
		set
		{
			c8f29d37d441bd9e83843d838e4c726b4 = value;
		}
	}

	/// <summary>
	///       Gets or sets the rest length of the spring.
	///       </summary>
	public float Length
	{
		[CompilerGenerated]
		get
		{
			return c48d29a58571e20b96fbf85ba73e2f9de;
		}
		[CompilerGenerated]
		set
		{
			c48d29a58571e20b96fbf85ba73e2f9de = value;
		}
	}

	/// <summary>
	///       Gets or sets the position where the spring is attached to the first body (in local space of
	///       the first body).
	///       </summary>
	public Vector3F AttachmentPositionALocal
	{
		[CompilerGenerated]
		get
		{
			return c85ce3433ac5ea4f5d375e0c064299d81;
		}
		[CompilerGenerated]
		set
		{
			c85ce3433ac5ea4f5d375e0c064299d81 = value;
		}
	}

	/// <summary>
	///       Gets or sets the position where the spring is attached to the second body (in local space of
	///       the second body).
	///       </summary>
	public Vector3F AttachmentPositionBLocal
	{
		[CompilerGenerated]
		get
		{
			return c160ab8b1d9a3c74bc2c191b2a2e0b685;
		}
		[CompilerGenerated]
		set
		{
			c160ab8b1d9a3c74bc2c191b2a2e0b685 = value;
		}
	}

	/// <summary>
	///       Gets or sets the spring constant.
	///       </summary>
	public float SpringConstant
	{
		[CompilerGenerated]
		get
		{
			return c89ee6d3cffcfb8089261ab560c605de8;
		}
		[CompilerGenerated]
		set
		{
			c89ee6d3cffcfb8089261ab560c605de8 = value;
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Physics.ForceEffects.DampedSpring" /> class.
	///       </summary>
	public DampedSpring()
	{
		DampingConstant = 1.6f;
		SpringConstant = 2f;
	}

	/// <summary>
	///       Called when the simulation wants this force effect to apply forces to rigid bodies.
	///       </summary>
	protected override void OnApply()
	{
		Vector3F vector3F;
		if (BodyA == null)
		{
			while (true)
			{
				switch (4)
				{
				case 0:
					continue;
				}
				break;
			}
			if (1 == 0)
			{
				__ldtoken(DampedSpring.OnApply());
			}
			vector3F = AttachmentPositionALocal;
		}
		else
		{
			vector3F = BodyA.Pose.ToWorldPosition(AttachmentPositionALocal);
		}
		Vector3F vector3F2 = vector3F;
		Vector3F vector3F3 = ((BodyA != null) ? BodyA.GetVelocityOfLocalPoint(AttachmentPositionALocal) : Vector3F.Zero);
		Vector3F vector3F4;
		if (BodyB == null)
		{
			while (true)
			{
				switch (4)
				{
				case 0:
					continue;
				}
				break;
			}
			vector3F4 = AttachmentPositionBLocal;
		}
		else
		{
			vector3F4 = BodyB.Pose.ToWorldPosition(AttachmentPositionBLocal);
		}
		Vector3F vector3F5 = vector3F4;
		Vector3F vector3F6 = ((BodyB != null) ? BodyB.GetVelocityOfLocalPoint(AttachmentPositionBLocal) : Vector3F.Zero);
		Vector3F vector3F7 = vector3F5 - vector3F2;
		float length = vector3F7.Length;
		if (!vector3F7.TryNormalize())
		{
			while (true)
			{
				switch (2)
				{
				case 0:
					continue;
				}
				break;
			}
			vector3F7 = Vector3F.UnitY;
		}
		Vector3F vector3F8 = SpringConstant * (length - Length) * vector3F7;
		Vector3F vector = vector3F3 - vector3F6;
		vector3F8 += (0f - DampingConstant) * Vector3F.Dot(vector, vector3F7) * vector3F7;
		if (BodyA != null)
		{
			while (true)
			{
				switch (2)
				{
				case 0:
					continue;
				}
				break;
			}
			if (BodyA.MotionType == MotionType.Dynamic)
			{
				ForceEffect.AddForce(BodyA, vector3F8, vector3F2);
			}
		}
		if (BodyB == null)
		{
			return;
		}
		while (true)
		{
			switch (1)
			{
			case 0:
				continue;
			}
			if (BodyB.MotionType != MotionType.Dynamic)
			{
				return;
			}
			while (true)
			{
				switch (1)
				{
				case 0:
					continue;
				}
				ForceEffect.AddForce(BodyB, -vector3F8, vector3F5);
				return;
			}
		}
	}
}
