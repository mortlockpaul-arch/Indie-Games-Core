using A;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Physics.Constraints;

/// <summary>
///       Defines a motor that controls the relative orientation of two constrained bodies using Euler
///       angle.
///       </summary>
public class EulerMotor : Constraint
{
	private Vector3F c0fa68ea4eef5266cec95704c95b87eed;

	private Vector3F c3d39387af73715d5f4670ac095fd9f56;

	private readonly ce427b5a6ee07208e331688e3ede5e795[] c719741ad5138547824c9ab491e7ec0f5 = new ce427b5a6ee07208e331688e3ede5e795[3]
	{
		new ce427b5a6ee07208e331688e3ede5e795(),
		new ce427b5a6ee07208e331688e3ede5e795(),
		new ce427b5a6ee07208e331688e3ede5e795()
	};

	private Matrix33F cca115fae1c884ef1dec0a6234424f3b1 = Matrix33F.Identity;

	private Matrix33F c77021cfbaf0decc953df6cd7c2ec54f5 = Matrix33F.Identity;

	private Vector3F c04b21dd5c2c70a9648b9fca4f7a88301;

	private float c6e73e2b321632ffda3c817af4598a6af = 6000f;

	private float c789a67a7250bbf53bdfef25b80f0ed2b = 900f;

	private float c8fac6df59597960db6cfe9088c90c9cd = float.PositiveInfinity;

	private float c8e23b9e9bcef99c776b900fc3710e9a6 = float.PositiveInfinity;

	/// <summary>
	///       Gets or sets the constraint anchor orientation on <see cref="P:DigitalRune.Physics.Constraints.Constraint.BodyA" /> in local 
	///       space of <see cref="P:DigitalRune.Physics.Constraints.Constraint.BodyA" />.
	///       </summary>
	public Matrix33F AnchorOrientationALocal
	{
		get
		{
			return cca115fae1c884ef1dec0a6234424f3b1;
		}
		set
		{
			if (cca115fae1c884ef1dec0a6234424f3b1 != value)
			{
				cca115fae1c884ef1dec0a6234424f3b1 = value;
				OnChanged();
			}
		}
	}

	/// <summary>
	///       Gets or sets the constraint anchor orientation on <see cref="P:DigitalRune.Physics.Constraints.Constraint.BodyB" /> in local 
	///       space of <see cref="P:DigitalRune.Physics.Constraints.Constraint.BodyB" />.
	///       </summary>
	public Matrix33F AnchorOrientationBLocal
	{
		get
		{
			return c77021cfbaf0decc953df6cd7c2ec54f5;
		}
		set
		{
			if (!(c77021cfbaf0decc953df6cd7c2ec54f5 != value))
			{
				return;
			}
			while (true)
			{
				switch (7)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(EulerMotor.set_AnchorOrientationBLocal(Matrix33F));
				}
				c77021cfbaf0decc953df6cd7c2ec54f5 = value;
				OnChanged();
				return;
			}
		}
	}

	/// <summary>
	///       Gets or sets the target angles.
	///       </summary>
	public Vector3F TargetAngles
	{
		get
		{
			return c04b21dd5c2c70a9648b9fca4f7a88301;
		}
		set
		{
			if (!(c04b21dd5c2c70a9648b9fca4f7a88301 != value))
			{
				return;
			}
			while (true)
			{
				switch (3)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(EulerMotor.set_TargetAngles(Vector3F));
				}
				c04b21dd5c2c70a9648b9fca4f7a88301 = value;
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
				switch (3)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(EulerMotor.set_SpringConstant(float));
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
			if (c789a67a7250bbf53bdfef25b80f0ed2b != value)
			{
				c789a67a7250bbf53bdfef25b80f0ed2b = value;
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
				switch (6)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(EulerMotor.set_MaxForce(float));
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
	public override Vector3F LinearConstraintImpulse => Vector3F.Zero;

	/// <summary>
	///       Gets or sets the angular constraint impulse that was applied.
	///       </summary>
	public override Vector3F AngularConstraintImpulse => c719741ad5138547824c9ab491e7ec0f5[0].cc01f881484f44c63c9c69258954b12e0 * c719741ad5138547824c9ab491e7ec0f5[0].c270614fc716c17fabf0b32db3d42da8d + c719741ad5138547824c9ab491e7ec0f5[1].cc01f881484f44c63c9c69258954b12e0 * c719741ad5138547824c9ab491e7ec0f5[1].c270614fc716c17fabf0b32db3d42da8d + c719741ad5138547824c9ab491e7ec0f5[2].cc01f881484f44c63c9c69258954b12e0 * c719741ad5138547824c9ab491e7ec0f5[2].c270614fc716c17fabf0b32db3d42da8d;

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
			if (c8e23b9e9bcef99c776b900fc3710e9a6 == value)
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
					__ldtoken(EulerMotor.set_MaxVelocity(float));
				}
				c8e23b9e9bcef99c776b900fc3710e9a6 = value;
				OnChanged();
				return;
			}
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.Physics.Constraints.EulerMotor" /> class</summary>
	public EulerMotor()
	{
	}

	/// <summary>
	///       Called when constraint should be set-up for a new time step.
	///       </summary>
	protected override void OnSetup()
	{
		float fixedTimeStep = Simulation.Settings.Timing.FixedTimeStep;
		float c75186be1308be18e3492ddaaeb = ConstraintHelper.ComputeErrorReduction(fixedTimeStep, SpringConstant, DampingConstant);
		float cad6d615cea81a5446de3403a944a2d = ConstraintHelper.ComputeSoftness(fixedTimeStep, SpringConstant, DampingConstant);
		Matrix33F matrix33F = BodyA.Pose.Orientation * AnchorOrientationALocal;
		Matrix33F matrix33F2 = BodyB.Pose.Orientation * AnchorOrientationBLocal;
		Matrix33F rotation = matrix33F.Transposed * matrix33F2;
		Vector3F eulerAngles = ConstraintHelper.GetEulerAngles(rotation);
		Vector3F column = matrix33F.GetColumn(0);
		Vector3F column2 = matrix33F2.GetColumn(2);
		Vector3F vector3F = Vector3F.Cross(column2, column);
		Vector3F c7d96080eb2f6c52630b4332d78a = Vector3F.Cross(vector3F, column2);
		Vector3F c7d96080eb2f6c52630b4332d78a2 = Vector3F.Cross(column, vector3F);
		c3c8a85b9ce08fec192d949648775562c(0, eulerAngles[0], TargetAngles[0], c7d96080eb2f6c52630b4332d78a, fixedTimeStep, c75186be1308be18e3492ddaaeb, cad6d615cea81a5446de3403a944a2d);
		c3c8a85b9ce08fec192d949648775562c(1, eulerAngles[1], TargetAngles[1], vector3F, fixedTimeStep, c75186be1308be18e3492ddaaeb, cad6d615cea81a5446de3403a944a2d);
		c3c8a85b9ce08fec192d949648775562c(2, eulerAngles[2], TargetAngles[2], c7d96080eb2f6c52630b4332d78a2, fixedTimeStep, c75186be1308be18e3492ddaaeb, cad6d615cea81a5446de3403a944a2d);
		c719741ad5138547824c9ab491e7ec0f5[0].cc01f881484f44c63c9c69258954b12e0 = 0f;
		c719741ad5138547824c9ab491e7ec0f5[1].cc01f881484f44c63c9c69258954b12e0 = 0f;
		c719741ad5138547824c9ab491e7ec0f5[2].cc01f881484f44c63c9c69258954b12e0 = 0f;
	}

	private void c3c8a85b9ce08fec192d949648775562c(int c88a85a6fc8e0a66e19d046d4d51cd3f1, float c8d9caeda1af0d36487d44ed990d972c6, float c510690569eeb8a78baa3b9a162a5523f, Vector3F c7d96080eb2f6c52630b4332d78a61022, float ce8c71427a0828475a969c745c8335cde, float c75186be1308be18e3492ddaaeb084262, float cad6d615cea81a5446de3403a944a2d81)
	{
		ce427b5a6ee07208e331688e3ede5e795 ce427b5a6ee07208e331688e3ede5e796 = c719741ad5138547824c9ab491e7ec0f5[c88a85a6fc8e0a66e19d046d4d51cd3f1];
		Simulation simulation = Simulation;
		float num = c510690569eeb8a78baa3b9a162a5523f - c8d9caeda1af0d36487d44ed990d972c6;
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
		ce427b5a6ee07208e331688e3ede5e796.cb6e3dcfcb9d1bf53e27a1c4bc4921edb(BodyA, BodyB, Vector3F.Zero, -c7d96080eb2f6c52630b4332d78a61022, Vector3F.Zero, c7d96080eb2f6c52630b4332d78a61022);
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
		Vector3F vector3F = new Vector3F
		{
			X = c0004f123fef2ac0bfc76ea9ed6e45574(0),
			Y = c0004f123fef2ac0bfc76ea9ed6e45574(1),
			Z = c0004f123fef2ac0bfc76ea9ed6e45574(2)
		};
		return vector3F.LengthSquared > Simulation.Settings.Constraints.MinConstraintImpulseSquared;
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
