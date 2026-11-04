using System;
using A;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Physics.Constraints;

/// <summary>
///       Defines a constraint that restricts rotational movement. This constraint is configurable
///       to create custom joints.
///       </summary>
public class AngularLimit : Constraint
{
	private readonly LimitState[] c6a2fa7a2595bdda502a7657db30fd6b2 = new LimitState[3];

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

	private Vector3F c2cae79528c2f6469234ffa89a595401d;

	private Vector3F c0b8d7516b73b12bd4b0f02823ac9f8b7;

	private Vector3F c7b0155e0d6ae0f40ef24d79ecda3d967 = new Vector3F(0.2f);

	private Vector3F c2899dbccd71e3358c2300c70e7b778ee;

	private Vector3F c3029b3eb4d165025b85c5363274411cc;

	private Vector3F c8fac6df59597960db6cfe9088c90c9cd = new Vector3F(float.PositiveInfinity);

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
				switch (5)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(AngularLimit.set_AnchorOrientationBLocal(Matrix33F));
				}
				c77021cfbaf0decc953df6cd7c2ec54f5 = value;
				OnChanged();
				return;
			}
		}
	}

	/// <summary>
	///       Gets or sets the minimum movement limit on the three constraint axes (in radians).
	///       </summary>
	public Vector3F Minimum
	{
		get
		{
			return c2cae79528c2f6469234ffa89a595401d;
		}
		set
		{
			if (!(c2cae79528c2f6469234ffa89a595401d != value))
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
					__ldtoken(AngularLimit.set_Minimum(Vector3F));
				}
				c2cae79528c2f6469234ffa89a595401d = value;
				OnChanged();
				return;
			}
		}
	}

	/// <summary>
	///       Gets or sets the maximum movement limit on the three constraint axes (in radians).
	///       </summary>
	public Vector3F Maximum
	{
		get
		{
			return c0b8d7516b73b12bd4b0f02823ac9f8b7;
		}
		set
		{
			if (!(c0b8d7516b73b12bd4b0f02823ac9f8b7 != value))
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
					__ldtoken(AngularLimit.set_Maximum(Vector3F));
				}
				c0b8d7516b73b12bd4b0f02823ac9f8b7 = value;
				OnChanged();
				return;
			}
		}
	}

	/// <summary>
	///       Gets or sets the error reduction parameter.
	///       </summary>
	public Vector3F ErrorReduction
	{
		get
		{
			return c7b0155e0d6ae0f40ef24d79ecda3d967;
		}
		set
		{
			if (!(c7b0155e0d6ae0f40ef24d79ecda3d967 != value))
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
					__ldtoken(AngularLimit.set_ErrorReduction(Vector3F));
				}
				c7b0155e0d6ae0f40ef24d79ecda3d967 = value;
				OnChanged();
				return;
			}
		}
	}

	/// <summary>
	///       Gets or sets the softness.
	///       </summary>
	public Vector3F Softness
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
	///       Gets or sets the coefficients of restitution.
	///       </summary>
	public Vector3F Restitution
	{
		get
		{
			return c3029b3eb4d165025b85c5363274411cc;
		}
		set
		{
			if (c3029b3eb4d165025b85c5363274411cc != value)
			{
				c3029b3eb4d165025b85c5363274411cc = value;
				OnChanged();
			}
		}
	}

	/// <summary>
	///       Gets or sets the maximal forces for the three rotational constraints.
	///       </summary>
	public Vector3F MaxForce
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
	public override Vector3F LinearConstraintImpulse => Vector3F.Zero;

	/// <summary>
	///       Gets or sets the angular constraint impulse that was applied.
	///       </summary>
	public override Vector3F AngularConstraintImpulse => c719741ad5138547824c9ab491e7ec0f5[0].cc01f881484f44c63c9c69258954b12e0 * c719741ad5138547824c9ab491e7ec0f5[0].c270614fc716c17fabf0b32db3d42da8d + c719741ad5138547824c9ab491e7ec0f5[1].cc01f881484f44c63c9c69258954b12e0 * c719741ad5138547824c9ab491e7ec0f5[1].c270614fc716c17fabf0b32db3d42da8d + c719741ad5138547824c9ab491e7ec0f5[2].cc01f881484f44c63c9c69258954b12e0 * c719741ad5138547824c9ab491e7ec0f5[2].c270614fc716c17fabf0b32db3d42da8d;

	/// <summary>
	///       Gets the relative rotations about the constraint axes (the three Euler angles).
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
			Matrix33F matrix33F = BodyA.Pose.Orientation * AnchorOrientationALocal;
			Matrix33F matrix33F2 = BodyB.Pose.Orientation * AnchorOrientationBLocal;
			Matrix33F rotation = matrix33F.Transposed * matrix33F2;
			return ConstraintHelper.GetEulerAngles(rotation);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.Physics.Constraints.AngularLimit" /> class</summary>
	public AngularLimit()
	{
	}

	/// <summary>
	///       Gets the state of an angular limit about a certain axis.
	///       </summary>
	/// <param name="index">
	///       The index of the limit axis. (0 = first rotation axis, 1 = second rotation axis, 
	///       2 = third rotation axis)
	///       </param>
	/// <returns>
	///       The limit state on the given axis.
	///       </returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is not in the range [0, 2].
	///       </exception>
	public LimitState GetLimitState(int index)
	{
		if (index >= 0)
		{
			while (true)
			{
				switch (1)
				{
				case 0:
					continue;
				}
				break;
			}
			if (1 == 0)
			{
				__ldtoken(AngularLimit.GetLimitState(int));
			}
			if (index <= 2)
			{
				return c6a2fa7a2595bdda502a7657db30fd6b2[index];
			}
			while (true)
			{
				switch (7)
				{
				case 0:
					continue;
				}
				break;
			}
		}
		throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1423), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1434));
	}

	/// <summary>
	///       Called when constraint should be set-up for a new time step.
	///       </summary>
	protected override void OnSetup()
	{
		Matrix33F matrix33F = BodyA.Pose.Orientation * AnchorOrientationALocal;
		Matrix33F matrix33F2 = BodyB.Pose.Orientation * AnchorOrientationBLocal;
		Matrix33F rotation = matrix33F.Transposed * matrix33F2;
		Vector3F eulerAngles = ConstraintHelper.GetEulerAngles(rotation);
		Vector3F column = matrix33F.GetColumn(0);
		Vector3F column2 = matrix33F2.GetColumn(2);
		Vector3F vector3F = Vector3F.Cross(column2, column);
		Vector3F c7d96080eb2f6c52630b4332d78a = Vector3F.Cross(vector3F, column2);
		Vector3F c7d96080eb2f6c52630b4332d78a2 = Vector3F.Cross(column, vector3F);
		LimitState c9498271b0ba9856118ef0ba50f3a = c6a2fa7a2595bdda502a7657db30fd6b2[0];
		LimitState c9498271b0ba9856118ef0ba50f3a2 = c6a2fa7a2595bdda502a7657db30fd6b2[1];
		LimitState c9498271b0ba9856118ef0ba50f3a3 = c6a2fa7a2595bdda502a7657db30fd6b2[2];
		c3c8a85b9ce08fec192d949648775562c(0, eulerAngles.X, c7d96080eb2f6c52630b4332d78a);
		c3c8a85b9ce08fec192d949648775562c(1, eulerAngles.Y, vector3F);
		c3c8a85b9ce08fec192d949648775562c(2, eulerAngles.Z, c7d96080eb2f6c52630b4332d78a2);
		c1b70fe54ea8e2058c44156c348d51664(0, c9498271b0ba9856118ef0ba50f3a);
		c1b70fe54ea8e2058c44156c348d51664(1, c9498271b0ba9856118ef0ba50f3a2);
		c1b70fe54ea8e2058c44156c348d51664(2, c9498271b0ba9856118ef0ba50f3a3);
	}

	private void c3c8a85b9ce08fec192d949648775562c(int c88a85a6fc8e0a66e19d046d4d51cd3f1, float c2fed316f5bcf4dd4ea7883930f3822f9, Vector3F c7d96080eb2f6c52630b4332d78a61022)
	{
		if (c7d96080eb2f6c52630b4332d78a61022.IsNumericallyZero)
		{
			while (true)
			{
				switch (1)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(AngularLimit.c3c8a85b9ce08fec192d949648775562c(int, float, Vector3F));
					}
					c6a2fa7a2595bdda502a7657db30fd6b2[c88a85a6fc8e0a66e19d046d4d51cd3f1] = LimitState.Inactive;
					return;
				}
			}
		}
		ce427b5a6ee07208e331688e3ede5e795 ce427b5a6ee07208e331688e3ede5e796 = c719741ad5138547824c9ab491e7ec0f5[c88a85a6fc8e0a66e19d046d4d51cd3f1];
		float num = Minimum[c88a85a6fc8e0a66e19d046d4d51cd3f1];
		float num2 = Maximum[c88a85a6fc8e0a66e19d046d4d51cd3f1];
		Simulation simulation = Simulation;
		float fixedTimeStep = simulation.Settings.Timing.FixedTimeStep;
		if (num > num2)
		{
			c6a2fa7a2595bdda502a7657db30fd6b2[c88a85a6fc8e0a66e19d046d4d51cd3f1] = LimitState.Inactive;
			return;
		}
		if (Numeric.AreEqual(num, num2))
		{
			while (true)
			{
				switch (7)
				{
				case 0:
					continue;
				}
				break;
			}
			c6a2fa7a2595bdda502a7657db30fd6b2[c88a85a6fc8e0a66e19d046d4d51cd3f1] = LimitState.Locked;
		}
		else if (c2fed316f5bcf4dd4ea7883930f3822f9 <= num)
		{
			while (true)
			{
				switch (5)
				{
				case 0:
					continue;
				}
				break;
			}
			c6a2fa7a2595bdda502a7657db30fd6b2[c88a85a6fc8e0a66e19d046d4d51cd3f1] = LimitState.Min;
		}
		else
		{
			if (!(c2fed316f5bcf4dd4ea7883930f3822f9 >= num2))
			{
				c6a2fa7a2595bdda502a7657db30fd6b2[c88a85a6fc8e0a66e19d046d4d51cd3f1] = LimitState.Inactive;
				return;
			}
			while (true)
			{
				switch (5)
				{
				case 0:
					continue;
				}
				break;
			}
			c6a2fa7a2595bdda502a7657db30fd6b2[c88a85a6fc8e0a66e19d046d4d51cd3f1] = LimitState.Max;
		}
		float num3 = 0f;
		float num4 = simulation.Settings.Constraints.AllowedAngularDeviation;
		if (c6a2fa7a2595bdda502a7657db30fd6b2[c88a85a6fc8e0a66e19d046d4d51cd3f1] == LimitState.Locked)
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
			num4 = 0f;
		}
		if (c2fed316f5bcf4dd4ea7883930f3822f9 > num2 + num4)
		{
			num3 = num2 - c2fed316f5bcf4dd4ea7883930f3822f9 + num4;
		}
		else if (c2fed316f5bcf4dd4ea7883930f3822f9 < num - num4)
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
			num3 = num - c2fed316f5bcf4dd4ea7883930f3822f9 - num4;
		}
		float value = num3 * ErrorReduction[c88a85a6fc8e0a66e19d046d4d51cd3f1] / fixedTimeStep;
		float maxErrorCorrectionVelocity = simulation.Settings.Constraints.MaxErrorCorrectionVelocity;
		value = MathHelper.Clamp(value, 0f - maxErrorCorrectionVelocity, maxErrorCorrectionVelocity);
		float num5 = Restitution[c88a85a6fc8e0a66e19d046d4d51cd3f1];
		if (num5 > simulation.Settings.Constraints.RestitutionThreshold)
		{
			while (true)
			{
				switch (1)
				{
				case 0:
					continue;
				}
				break;
			}
			float num6 = ce427b5a6ee07208e331688e3ede5e796.c1d2b8cb986f37f24e96af6bf3f5b2694(BodyA, BodyB);
			if (c6a2fa7a2595bdda502a7657db30fd6b2[c88a85a6fc8e0a66e19d046d4d51cd3f1] == LimitState.Min)
			{
				while (true)
				{
					switch (7)
					{
					case 0:
						continue;
					}
					break;
				}
				if (num6 < 0f - Simulation.Settings.Constraints.RestingVelocityLimit)
				{
					while (true)
					{
						switch (7)
						{
						case 0:
							continue;
						}
						break;
					}
					value = Math.Max(value, (0f - num6) * num5);
				}
			}
			else if (c6a2fa7a2595bdda502a7657db30fd6b2[c88a85a6fc8e0a66e19d046d4d51cd3f1] == LimitState.Max)
			{
				while (true)
				{
					switch (7)
					{
					case 0:
						continue;
					}
					break;
				}
				if (num6 > Simulation.Settings.Constraints.RestingVelocityLimit)
				{
					value = Math.Min(value, (0f - num6) * num5);
				}
			}
		}
		ce427b5a6ee07208e331688e3ede5e796.ceef50b9f5f0910e0d3cdffa0ab9e67bf = value;
		float num7 = MaxForce[c88a85a6fc8e0a66e19d046d4d51cd3f1] * fixedTimeStep;
		if (c6a2fa7a2595bdda502a7657db30fd6b2[c88a85a6fc8e0a66e19d046d4d51cd3f1] == LimitState.Min)
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
			c0fa68ea4eef5266cec95704c95b87eed[c88a85a6fc8e0a66e19d046d4d51cd3f1] = 0f;
			c3d39387af73715d5f4670ac095fd9f56[c88a85a6fc8e0a66e19d046d4d51cd3f1] = num7;
		}
		else if (c6a2fa7a2595bdda502a7657db30fd6b2[c88a85a6fc8e0a66e19d046d4d51cd3f1] == LimitState.Max)
		{
			c0fa68ea4eef5266cec95704c95b87eed[c88a85a6fc8e0a66e19d046d4d51cd3f1] = 0f - num7;
			c3d39387af73715d5f4670ac095fd9f56[c88a85a6fc8e0a66e19d046d4d51cd3f1] = 0f;
		}
		else
		{
			c0fa68ea4eef5266cec95704c95b87eed[c88a85a6fc8e0a66e19d046d4d51cd3f1] = 0f - num7;
			c3d39387af73715d5f4670ac095fd9f56[c88a85a6fc8e0a66e19d046d4d51cd3f1] = num7;
		}
		ce427b5a6ee07208e331688e3ede5e796.ced8746c2a7c3e88fd7de830408ee6551 = Softness[c88a85a6fc8e0a66e19d046d4d51cd3f1] / fixedTimeStep;
		ce427b5a6ee07208e331688e3ede5e796.cb6e3dcfcb9d1bf53e27a1c4bc4921edb(BodyA, BodyB, Vector3F.Zero, -c7d96080eb2f6c52630b4332d78a61022, Vector3F.Zero, c7d96080eb2f6c52630b4332d78a61022);
	}

	private void c1b70fe54ea8e2058c44156c348d51664(int c88a85a6fc8e0a66e19d046d4d51cd3f1, LimitState c9498271b0ba9856118ef0ba50f3a8449)
	{
		ce427b5a6ee07208e331688e3ede5e795 ce427b5a6ee07208e331688e3ede5e796 = c719741ad5138547824c9ab491e7ec0f5[c88a85a6fc8e0a66e19d046d4d51cd3f1];
		if (c9498271b0ba9856118ef0ba50f3a8449 != LimitState.Inactive)
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
				__ldtoken(AngularLimit.c1b70fe54ea8e2058c44156c348d51664(int, LimitState));
			}
			if (c9498271b0ba9856118ef0ba50f3a8449 == c6a2fa7a2595bdda502a7657db30fd6b2[c88a85a6fc8e0a66e19d046d4d51cd3f1])
			{
				ce427b5a6ee07208e331688e3ede5e796.c1b70fe54ea8e2058c44156c348d51664(BodyA, BodyB);
				return;
			}
		}
		if (ce427b5a6ee07208e331688e3ede5e796 == null)
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
			ce427b5a6ee07208e331688e3ede5e796.cc01f881484f44c63c9c69258954b12e0 = 0f;
			return;
		}
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
		if (c6a2fa7a2595bdda502a7657db30fd6b2[c88a85a6fc8e0a66e19d046d4d51cd3f1] != LimitState.Inactive)
		{
			while (true)
			{
				switch (5)
				{
				case 0:
					break;
				default:
				{
					if (1 == 0)
					{
						__ldtoken(AngularLimit.c0004f123fef2ac0bfc76ea9ed6e45574(int));
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
