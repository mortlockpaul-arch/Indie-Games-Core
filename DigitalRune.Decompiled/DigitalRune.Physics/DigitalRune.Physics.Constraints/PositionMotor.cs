using System;
using A;
using DigitalRune.Geometry;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Physics.Constraints;

/// <summary>
///       Defines a motor that controls the linear distance of two constrained bodies.
///       </summary>
public class PositionMotor : Constraint
{
	private Vector3F c0fa68ea4eef5266cec95704c95b87eed;

	private Vector3F c3d39387af73715d5f4670ac095fd9f56;

	private readonly ce427b5a6ee07208e331688e3ede5e795[] c719741ad5138547824c9ab491e7ec0f5 = new ce427b5a6ee07208e331688e3ede5e795[3]
	{
		new ce427b5a6ee07208e331688e3ede5e795(),
		new ce427b5a6ee07208e331688e3ede5e795(),
		new ce427b5a6ee07208e331688e3ede5e795()
	};

	private Pose cdb8a4163cdae1c67c11ab887d0afd5c9 = Pose.Identity;

	private Vector3F cb15f5b3befd19bec2299cd680edfd098;

	private Vector3F cb04112d260f514e4d7c37ba110432075;

	private float c6e73e2b321632ffda3c817af4598a6af = 6000f;

	private float c789a67a7250bbf53bdfef25b80f0ed2b = 900f;

	private float c8fac6df59597960db6cfe9088c90c9cd = float.PositiveInfinity;

	private bool cb3f4a1b2df9f50aff66ac36c7b361271;

	private float c8e23b9e9bcef99c776b900fc3710e9a6 = float.PositiveInfinity;

	/// <summary>
	///       Gets or sets the constraint anchor pose on <see cref="P:DigitalRune.Physics.Constraints.Constraint.BodyA" /> in local space of 
	///       <see cref="P:DigitalRune.Physics.Constraints.Constraint.BodyA" />.
	///       </summary>
	public Pose AnchorPoseALocal
	{
		get
		{
			return cdb8a4163cdae1c67c11ab887d0afd5c9;
		}
		set
		{
			if (cdb8a4163cdae1c67c11ab887d0afd5c9 != value)
			{
				cdb8a4163cdae1c67c11ab887d0afd5c9 = value;
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
			if (cb15f5b3befd19bec2299cd680edfd098 != value)
			{
				cb15f5b3befd19bec2299cd680edfd098 = value;
				OnChanged();
			}
		}
	}

	/// <summary>
	///       Gets or sets the target position of the <see cref="P:DigitalRune.Physics.Constraints.PositionMotor.AnchorPositionBLocal" /> relative to 
	///       <see cref="P:DigitalRune.Physics.Constraints.PositionMotor.AnchorPoseALocal" /></summary>
	public Vector3F TargetPosition
	{
		get
		{
			return cb04112d260f514e4d7c37ba110432075;
		}
		set
		{
			if (!(cb04112d260f514e4d7c37ba110432075 != value))
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
					__ldtoken(PositionMotor.set_TargetPosition(Vector3F));
				}
				cb04112d260f514e4d7c37ba110432075 = value;
				OnChanged();
				return;
			}
		}
	}

	/// <summary>
	///       Gets or sets the spring constant.
	///       </summary>
	public float SpringConstant
	{
		get
		{
			return c6e73e2b321632ffda3c817af4598a6af;
		}
		set
		{
			if (c6e73e2b321632ffda3c817af4598a6af == value)
			{
				return;
			}
			while (true)
			{
				switch (5)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(PositionMotor.set_SpringConstant(float));
				}
				c6e73e2b321632ffda3c817af4598a6af = value;
				OnChanged();
				return;
			}
		}
	}

	/// <summary>
	///       Gets or sets the damping constant.
	///       </summary>
	public float DampingConstant
	{
		get
		{
			return c789a67a7250bbf53bdfef25b80f0ed2b;
		}
		set
		{
			if (c789a67a7250bbf53bdfef25b80f0ed2b == value)
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
					__ldtoken(PositionMotor.set_DampingConstant(float));
				}
				c789a67a7250bbf53bdfef25b80f0ed2b = value;
				OnChanged();
				return;
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
			if (c8fac6df59597960db6cfe9088c90c9cd != value)
			{
				c8fac6df59597960db6cfe9088c90c9cd = value;
				OnChanged();
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
	///       Gets the relative position of the constraint anchor on <see cref="P:DigitalRune.Physics.Constraints.Constraint.BodyB" />
	///       relative to the constraint anchor on <see cref="P:DigitalRune.Physics.Constraints.Constraint.BodyA" />.
	///       </summary>
	/// <exception cref="T:DigitalRune.Physics.PhysicsException">
	///   <see cref="P:DigitalRune.Physics.Constraints.Constraint.BodyA" /> or <see cref="P:DigitalRune.Physics.Constraints.Constraint.BodyB" /> is <see langword="null" />.
	///       </exception>
	public Vector3F RelativePosition
	{
		get
		{
			if (BodyA == null)
			{
				throw new PhysicsException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1329));
			}
			if (BodyB == null)
			{
				throw new PhysicsException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1376));
			}
			Pose pose = BodyA.Pose * AnchorPoseALocal;
			Vector3F worldPosition = BodyB.Pose.ToWorldPosition(AnchorPositionBLocal);
			return pose.ToLocalPosition(worldPosition);
		}
	}

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
				switch (5)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(PositionMotor.set_UseSingleAxisMode(bool));
				}
				cb3f4a1b2df9f50aff66ac36c7b361271 = value;
				OnChanged();
				return;
			}
		}
	}

	/// <summary>
	///       Gets or sets the maximal velocity.
	///       </summary>
	public float MaxVelocity
	{
		get
		{
			return c8e23b9e9bcef99c776b900fc3710e9a6;
		}
		set
		{
			if (c8e23b9e9bcef99c776b900fc3710e9a6 != value)
			{
				c8e23b9e9bcef99c776b900fc3710e9a6 = value;
				OnChanged();
			}
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.Physics.Constraints.PositionMotor" /> class</summary>
	public PositionMotor()
	{
	}

	/// <summary>
	///       Called when constraint should be set-up for a new time step.
	///       </summary>
	protected override void OnSetup()
	{
		Pose pose = BodyA.Pose * AnchorPoseALocal;
		Vector3F vector3F = BodyB.Pose.ToWorldPosition(AnchorPositionBLocal);
		Vector3F vector3F2 = pose.ToLocalPosition(vector3F);
		Matrix33F orientation = pose.Orientation;
		Vector3F cafdcbeab03ee2149d6d7ea18ab = pose.Position - BodyA.PoseCenterOfMass.Position;
		Vector3F cc9ff7c641ebd61e37502a93d3820c2e = vector3F - BodyB.PoseCenterOfMass.Position;
		float fixedTimeStep = Simulation.Settings.Timing.FixedTimeStep;
		float c75186be1308be18e3492ddaaeb = ConstraintHelper.ComputeErrorReduction(fixedTimeStep, SpringConstant, DampingConstant);
		float cad6d615cea81a5446de3403a944a2d = ConstraintHelper.ComputeSoftness(fixedTimeStep, SpringConstant, DampingConstant);
		if (!UseSingleAxisMode)
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
			if (1 == 0)
			{
				__ldtoken(PositionMotor.OnSetup());
			}
			c3c8a85b9ce08fec192d949648775562c(0, vector3F2.X, TargetPosition.X, orientation.GetColumn(0), cafdcbeab03ee2149d6d7ea18ab, cc9ff7c641ebd61e37502a93d3820c2e, fixedTimeStep, c75186be1308be18e3492ddaaeb, cad6d615cea81a5446de3403a944a2d);
			c3c8a85b9ce08fec192d949648775562c(1, vector3F2.Y, TargetPosition.Y, orientation.GetColumn(1), cafdcbeab03ee2149d6d7ea18ab, cc9ff7c641ebd61e37502a93d3820c2e, fixedTimeStep, c75186be1308be18e3492ddaaeb, cad6d615cea81a5446de3403a944a2d);
			c3c8a85b9ce08fec192d949648775562c(2, vector3F2.Z, TargetPosition.Z, orientation.GetColumn(2), cafdcbeab03ee2149d6d7ea18ab, cc9ff7c641ebd61e37502a93d3820c2e, fixedTimeStep, c75186be1308be18e3492ddaaeb, cad6d615cea81a5446de3403a944a2d);
		}
		else
		{
			Vector3F c7d96080eb2f6c52630b4332d78a = TargetPosition - vector3F2;
			float length = c7d96080eb2f6c52630b4332d78a.Length;
			if (Numeric.IsZero(length))
			{
				c7d96080eb2f6c52630b4332d78a = Vector3F.UnitX;
			}
			else
			{
				c7d96080eb2f6c52630b4332d78a.Normalize();
			}
			c3c8a85b9ce08fec192d949648775562c(0, 0f - length, 0f, c7d96080eb2f6c52630b4332d78a, cafdcbeab03ee2149d6d7ea18ab, cc9ff7c641ebd61e37502a93d3820c2e, fixedTimeStep, c75186be1308be18e3492ddaaeb, cad6d615cea81a5446de3403a944a2d);
		}
		c719741ad5138547824c9ab491e7ec0f5[0].cc01f881484f44c63c9c69258954b12e0 = 0f;
		c719741ad5138547824c9ab491e7ec0f5[1].cc01f881484f44c63c9c69258954b12e0 = 0f;
		c719741ad5138547824c9ab491e7ec0f5[2].cc01f881484f44c63c9c69258954b12e0 = 0f;
	}

	private void c3c8a85b9ce08fec192d949648775562c(int c88a85a6fc8e0a66e19d046d4d51cd3f1, float c2fed316f5bcf4dd4ea7883930f3822f9, float c5b2fb281067a7be3836f467e6f81767c, Vector3F c7d96080eb2f6c52630b4332d78a61022, Vector3F cafdcbeab03ee2149d6d7ea18ab700176, Vector3F cc9ff7c641ebd61e37502a93d3820c2e8, float ce8c71427a0828475a969c745c8335cde, float c75186be1308be18e3492ddaaeb084262, float cad6d615cea81a5446de3403a944a2d81)
	{
		ce427b5a6ee07208e331688e3ede5e795 ce427b5a6ee07208e331688e3ede5e796 = c719741ad5138547824c9ab491e7ec0f5[c88a85a6fc8e0a66e19d046d4d51cd3f1];
		Simulation simulation = Simulation;
		float num = c5b2fb281067a7be3836f467e6f81767c - c2fed316f5bcf4dd4ea7883930f3822f9;
		if (Numeric.IsZero(num))
		{
			c0fa68ea4eef5266cec95704c95b87eed[c88a85a6fc8e0a66e19d046d4d51cd3f1] = 0f;
			c3d39387af73715d5f4670ac095fd9f56[c88a85a6fc8e0a66e19d046d4d51cd3f1] = 0f;
			ce427b5a6ee07208e331688e3ede5e796.cc01f881484f44c63c9c69258954b12e0 = 0f;
			return;
		}
		float num2 = num / ce8c71427a0828475a969c745c8335cde;
		float value = num2 * c75186be1308be18e3492ddaaeb084262;
		float maxErrorCorrectionVelocity = simulation.Settings.Constraints.MaxErrorCorrectionVelocity;
		value = MathHelper.Clamp(value, 0f - maxErrorCorrectionVelocity, maxErrorCorrectionVelocity);
		value = MathHelper.Clamp(value, 0f - MaxVelocity, MaxVelocity);
		ce427b5a6ee07208e331688e3ede5e796.ceef50b9f5f0910e0d3cdffa0ab9e67bf = value;
		float num3 = MaxForce * ce8c71427a0828475a969c745c8335cde;
		c0fa68ea4eef5266cec95704c95b87eed[c88a85a6fc8e0a66e19d046d4d51cd3f1] = 0f - num3;
		c3d39387af73715d5f4670ac095fd9f56[c88a85a6fc8e0a66e19d046d4d51cd3f1] = num3;
		ce427b5a6ee07208e331688e3ede5e796.ced8746c2a7c3e88fd7de830408ee6551 = cad6d615cea81a5446de3403a944a2d81 / ce8c71427a0828475a969c745c8335cde;
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
			Vector3F vector3F = new Vector3F
			{
				X = c0004f123fef2ac0bfc76ea9ed6e45574(0),
				Y = c0004f123fef2ac0bfc76ea9ed6e45574(1),
				Z = c0004f123fef2ac0bfc76ea9ed6e45574(2)
			};
			return vector3F.LengthSquared > Simulation.Settings.Constraints.MinConstraintImpulseSquared;
		}
		float value = c0004f123fef2ac0bfc76ea9ed6e45574(0);
		return Math.Abs(value) > Simulation.Settings.Constraints.MinConstraintImpulse;
	}

	private float c0004f123fef2ac0bfc76ea9ed6e45574(int c88a85a6fc8e0a66e19d046d4d51cd3f1)
	{
		if (c0fa68ea4eef5266cec95704c95b87eed[c88a85a6fc8e0a66e19d046d4d51cd3f1] != 0f)
		{
			while (true)
			{
				switch (7)
				{
				case 0:
					break;
				default:
				{
					if (1 == 0)
					{
						__ldtoken(PositionMotor.c0004f123fef2ac0bfc76ea9ed6e45574(int));
					}
					ce427b5a6ee07208e331688e3ede5e795 ce427b5a6ee07208e331688e3ede5e796 = c719741ad5138547824c9ab491e7ec0f5[c88a85a6fc8e0a66e19d046d4d51cd3f1];
					float c3e548b3010de6dec5144f5f94f3c = ce427b5a6ee07208e331688e3ede5e796.c1d2b8cb986f37f24e96af6bf3f5b2694(BodyA, BodyB);
					return ce427b5a6ee07208e331688e3ede5e796.c5ff4a78f1a80b7e61a2f125dd4c0d54f(BodyA, BodyB, c3e548b3010de6dec5144f5f94f3c, c0fa68ea4eef5266cec95704c95b87eed[c88a85a6fc8e0a66e19d046d4d51cd3f1], c3d39387af73715d5f4670ac095fd9f56[c88a85a6fc8e0a66e19d046d4d51cd3f1]);
				}
				}
			}
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
