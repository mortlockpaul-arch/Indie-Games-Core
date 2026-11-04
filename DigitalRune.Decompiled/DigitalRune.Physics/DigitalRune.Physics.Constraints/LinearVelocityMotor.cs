using System;
using A;
using DigitalRune.Geometry;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Physics.Constraints;

/// <summary>
///       Defines a motor that controls the relative linear velocity of two constrained bodies.
///       </summary>
public class LinearVelocityMotor : Constraint
{
	private Vector3F c0fa68ea4eef5266cec95704c95b87eed;

	private Vector3F c3d39387af73715d5f4670ac095fd9f56;

	private readonly ce427b5a6ee07208e331688e3ede5e795[] c719741ad5138547824c9ab491e7ec0f5 = new ce427b5a6ee07208e331688e3ede5e795[3]
	{
		new ce427b5a6ee07208e331688e3ede5e795(),
		new ce427b5a6ee07208e331688e3ede5e795(),
		new ce427b5a6ee07208e331688e3ede5e795()
	};

	private Vector3F c1c685894f57c89ddc60c97e1a0170644;

	private Vector3F cb15f5b3befd19bec2299cd680edfd098;

	private Vector3F c056b8e5b873532e4a85f0ca976c51496 = Vector3F.UnitX;

	private float c6912c9bd9a7aa3408b6a81f7e4bc54df;

	private float c2899dbccd71e3358c2300c70e7b778ee;

	private float c8fac6df59597960db6cfe9088c90c9cd = float.PositiveInfinity;

	private bool cb3f4a1b2df9f50aff66ac36c7b361271;

	/// <summary>
	///       Gets or sets the constraint anchor position on <see cref="P:DigitalRune.Physics.Constraints.Constraint.BodyA" /> in local space 
	///       of <see cref="P:DigitalRune.Physics.Constraints.Constraint.BodyA" />.
	///       </summary>
	public Vector3F AnchorPositionALocal
	{
		get
		{
			return c1c685894f57c89ddc60c97e1a0170644;
		}
		set
		{
			if (c1c685894f57c89ddc60c97e1a0170644 != value)
			{
				c1c685894f57c89ddc60c97e1a0170644 = value;
				OnChanged();
			}
		}
	}

	/// <summary>
	///       Gets or sets the constraint anchor position on <see cref="P:DigitalRune.Physics.Constraints.Constraint.BodyB" /> in local space
	///       of <see cref="P:DigitalRune.Physics.Constraints.Constraint.BodyB" />.
	///       </summary>
	public Vector3F AnchorPositionBLocal
	{
		get
		{
			return cb15f5b3befd19bec2299cd680edfd098;
		}
		set
		{
			if (!(cb15f5b3befd19bec2299cd680edfd098 != value))
			{
				return;
			}
			while (true)
			{
				switch (2)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(LinearVelocityMotor.set_AnchorPositionBLocal(Vector3F));
				}
				cb15f5b3befd19bec2299cd680edfd098 = value;
				OnChanged();
				return;
			}
		}
	}

	/// <summary>
	///       Gets or sets the constraint axis that is fixed on <see cref="P:DigitalRune.Physics.Constraints.Constraint.BodyA" /> in local 
	///       space of <see cref="P:DigitalRune.Physics.Constraints.Constraint.BodyA" />.
	///       </summary>
	public Vector3F AxisALocal
	{
		get
		{
			return c056b8e5b873532e4a85f0ca976c51496;
		}
		set
		{
			if (!(c056b8e5b873532e4a85f0ca976c51496 != value))
			{
				return;
			}
			c056b8e5b873532e4a85f0ca976c51496 = value;
			if (!c056b8e5b873532e4a85f0ca976c51496.TryNormalize())
			{
				while (true)
				{
					switch (6)
					{
					case 0:
						continue;
					}
					break;
				}
				if (1 == 0)
				{
					__ldtoken(LinearVelocityMotor.set_AxisALocal(Vector3F));
				}
				c056b8e5b873532e4a85f0ca976c51496 = Vector3F.Zero;
			}
			OnChanged();
		}
	}

	/// <summary>
	///       Gets or sets the target linear velocity in the direction of the constraint axis
	///       (<see cref="P:DigitalRune.Physics.Constraints.LinearVelocityMotor.AxisALocal" />).
	///       </summary>
	public float TargetVelocity
	{
		get
		{
			return c6912c9bd9a7aa3408b6a81f7e4bc54df;
		}
		set
		{
			if (c6912c9bd9a7aa3408b6a81f7e4bc54df != value)
			{
				c6912c9bd9a7aa3408b6a81f7e4bc54df = value;
				OnChanged();
			}
		}
	}

	/// <summary>
	///       Gets or sets the softness.
	///       </summary>
	public float Softness
	{
		get
		{
			return c2899dbccd71e3358c2300c70e7b778ee;
		}
		set
		{
			if (c2899dbccd71e3358c2300c70e7b778ee != value)
			{
				c2899dbccd71e3358c2300c70e7b778ee = value;
				OnChanged();
			}
		}
	}

	/// <summary>
	///       Gets or sets the maximal force that is applied by this motor.
	///       </summary>
	public float MaxForce
	{
		get
		{
			return c8fac6df59597960db6cfe9088c90c9cd;
		}
		set
		{
			if (c8fac6df59597960db6cfe9088c90c9cd == value)
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
					__ldtoken(LinearVelocityMotor.set_MaxForce(float));
				}
				c8fac6df59597960db6cfe9088c90c9cd = value;
				OnChanged();
				return;
			}
		}
	}

	/// <summary>
	///       Gets or sets the linear constraint impulse that was applied. 
	///       </summary>
	public override Vector3F LinearConstraintImpulse => c719741ad5138547824c9ab491e7ec0f5[0].cc01f881484f44c63c9c69258954b12e0 * c719741ad5138547824c9ab491e7ec0f5[0].cd113dd0023606dbb59275f5f206680fc + c719741ad5138547824c9ab491e7ec0f5[1].cc01f881484f44c63c9c69258954b12e0 * c719741ad5138547824c9ab491e7ec0f5[1].cd113dd0023606dbb59275f5f206680fc + c719741ad5138547824c9ab491e7ec0f5[2].cc01f881484f44c63c9c69258954b12e0 * c719741ad5138547824c9ab491e7ec0f5[2].cd113dd0023606dbb59275f5f206680fc;

	/// <summary>
	///       Gets or sets the angular constraint impulse that was applied.
	///       </summary>
	public override Vector3F AngularConstraintImpulse => Vector3F.Zero;

	/// <summary>
	///       Gets or sets a value indicating whether the motor applies forces only on a single axis.
	///       </summary>
	public bool UseSingleAxisMode
	{
		get
		{
			return cb3f4a1b2df9f50aff66ac36c7b361271;
		}
		set
		{
			if (cb3f4a1b2df9f50aff66ac36c7b361271 == value)
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
				if (1 == 0)
				{
					__ldtoken(LinearVelocityMotor.set_UseSingleAxisMode(bool));
				}
				cb3f4a1b2df9f50aff66ac36c7b361271 = value;
				OnChanged();
				return;
			}
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.Physics.Constraints.LinearVelocityMotor" /> class</summary>
	public LinearVelocityMotor()
	{
	}

	/// <summary>
	///       Called when constraint should be set-up for a new time step.
	///       </summary>
	protected override void OnSetup()
	{
		Pose pose = BodyA.Pose;
		Vector3F vector3F = BodyB.Pose.ToWorldPosition(AnchorPositionBLocal);
		Matrix33F orientation = pose.Orientation;
		Vector3F cafdcbeab03ee2149d6d7ea18ab = pose.Position - BodyA.PoseCenterOfMass.Position;
		Vector3F cc9ff7c641ebd61e37502a93d3820c2e = vector3F - BodyB.PoseCenterOfMass.Position;
		float fixedTimeStep = Simulation.Settings.Timing.FixedTimeStep;
		Vector3F vector3F2 = BodyA.Pose.Orientation * AxisALocal;
		if (!UseSingleAxisMode)
		{
			Vector3F vector3F3 = vector3F2 * TargetVelocity;
			c3c8a85b9ce08fec192d949648775562c(0, vector3F3.X, orientation.GetColumn(0), cafdcbeab03ee2149d6d7ea18ab, cc9ff7c641ebd61e37502a93d3820c2e, fixedTimeStep);
			c3c8a85b9ce08fec192d949648775562c(1, vector3F3.Y, orientation.GetColumn(1), cafdcbeab03ee2149d6d7ea18ab, cc9ff7c641ebd61e37502a93d3820c2e, fixedTimeStep);
			c3c8a85b9ce08fec192d949648775562c(2, vector3F3.Z, orientation.GetColumn(2), cafdcbeab03ee2149d6d7ea18ab, cc9ff7c641ebd61e37502a93d3820c2e, fixedTimeStep);
		}
		else if (vector3F2.IsNumericallyZero)
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
				__ldtoken(LinearVelocityMotor.OnSetup());
			}
			c0fa68ea4eef5266cec95704c95b87eed[0] = 0f;
		}
		else
		{
			c3c8a85b9ce08fec192d949648775562c(0, TargetVelocity, vector3F2, cafdcbeab03ee2149d6d7ea18ab, cc9ff7c641ebd61e37502a93d3820c2e, fixedTimeStep);
		}
		c719741ad5138547824c9ab491e7ec0f5[0].cc01f881484f44c63c9c69258954b12e0 = 0f;
		c719741ad5138547824c9ab491e7ec0f5[1].cc01f881484f44c63c9c69258954b12e0 = 0f;
		c719741ad5138547824c9ab491e7ec0f5[2].cc01f881484f44c63c9c69258954b12e0 = 0f;
	}

	private void c3c8a85b9ce08fec192d949648775562c(int c88a85a6fc8e0a66e19d046d4d51cd3f1, float c156bf6650c5cf7eafcfa926c80419c15, Vector3F c7d96080eb2f6c52630b4332d78a61022, Vector3F cafdcbeab03ee2149d6d7ea18ab700176, Vector3F cc9ff7c641ebd61e37502a93d3820c2e8, float ce8c71427a0828475a969c745c8335cde)
	{
		ce427b5a6ee07208e331688e3ede5e795 ce427b5a6ee07208e331688e3ede5e796 = c719741ad5138547824c9ab491e7ec0f5[c88a85a6fc8e0a66e19d046d4d51cd3f1];
		ce427b5a6ee07208e331688e3ede5e796.ceef50b9f5f0910e0d3cdffa0ab9e67bf = c156bf6650c5cf7eafcfa926c80419c15;
		float num = MaxForce * ce8c71427a0828475a969c745c8335cde;
		c0fa68ea4eef5266cec95704c95b87eed[c88a85a6fc8e0a66e19d046d4d51cd3f1] = 0f - num;
		c3d39387af73715d5f4670ac095fd9f56[c88a85a6fc8e0a66e19d046d4d51cd3f1] = num;
		ce427b5a6ee07208e331688e3ede5e796.ced8746c2a7c3e88fd7de830408ee6551 = Softness / ce8c71427a0828475a969c745c8335cde;
		ce427b5a6ee07208e331688e3ede5e796.cb6e3dcfcb9d1bf53e27a1c4bc4921edb(BodyA, BodyB, -c7d96080eb2f6c52630b4332d78a61022, -Vector3F.Cross(cafdcbeab03ee2149d6d7ea18ab700176, c7d96080eb2f6c52630b4332d78a61022), c7d96080eb2f6c52630b4332d78a61022, Vector3F.Cross(cc9ff7c641ebd61e37502a93d3820c2e8, c7d96080eb2f6c52630b4332d78a61022));
	}

	/// <summary>
	///       Called when the constraint impulse should be applied.
	///       </summary>
	/// <returns>
	///   <see langword="true" /> if a constraint larger than 
	///       <see cref="P:DigitalRune.Physics.Settings.ConstraintSettings.MinConstraintImpulse" /> was applied.
	///       </returns>
	protected override bool OnApplyImpulse()
	{
		if (!UseSingleAxisMode)
		{
			while (true)
			{
				switch (7)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(LinearVelocityMotor.OnApplyImpulse());
					}
					return new Vector3F
					{
						X = c0004f123fef2ac0bfc76ea9ed6e45574(0),
						Y = c0004f123fef2ac0bfc76ea9ed6e45574(1),
						Z = c0004f123fef2ac0bfc76ea9ed6e45574(2)
					}.LengthSquared > Simulation.Settings.Constraints.MinConstraintImpulseSquared;
				}
			}
		}
		float value = c0004f123fef2ac0bfc76ea9ed6e45574(0);
		return Math.Abs(value) > Simulation.Settings.Constraints.MinConstraintImpulse;
	}

	private float c0004f123fef2ac0bfc76ea9ed6e45574(int c88a85a6fc8e0a66e19d046d4d51cd3f1)
	{
		if (c0fa68ea4eef5266cec95704c95b87eed[c88a85a6fc8e0a66e19d046d4d51cd3f1] != 0f)
		{
			ce427b5a6ee07208e331688e3ede5e795 ce427b5a6ee07208e331688e3ede5e796 = c719741ad5138547824c9ab491e7ec0f5[c88a85a6fc8e0a66e19d046d4d51cd3f1];
			float c3e548b3010de6dec5144f5f94f3c = ce427b5a6ee07208e331688e3ede5e796.c1d2b8cb986f37f24e96af6bf3f5b2694(BodyA, BodyB);
			return ce427b5a6ee07208e331688e3ede5e796.c5ff4a78f1a80b7e61a2f125dd4c0d54f(BodyA, BodyB, c3e548b3010de6dec5144f5f94f3c, c0fa68ea4eef5266cec95704c95b87eed[c88a85a6fc8e0a66e19d046d4d51cd3f1], c3d39387af73715d5f4670ac095fd9f56[c88a85a6fc8e0a66e19d046d4d51cd3f1]);
		}
		return 0f;
	}

	/// <summary>
	///       Called when properties of this constraint were changed.
	///       </summary>
	protected override void OnChanged()
	{
		c719741ad5138547824c9ab491e7ec0f5[0].cc01f881484f44c63c9c69258954b12e0 = 0f;
		c719741ad5138547824c9ab491e7ec0f5[1].cc01f881484f44c63c9c69258954b12e0 = 0f;
		c719741ad5138547824c9ab491e7ec0f5[2].cc01f881484f44c63c9c69258954b12e0 = 0f;
		base.OnChanged();
	}
}
