using System;
using A;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Physics.Constraints;

/// <summary>
///       Defines a twist and swing limits to limit rotations.
///       </summary>
public class TwistSwingLimit : Constraint
{
	private readonly LimitState[] c6a2fa7a2595bdda502a7657db30fd6b2 = new LimitState[2];

	private Vector2F c0fa68ea4eef5266cec95704c95b87eed;

	private Vector2F c3d39387af73715d5f4670ac095fd9f56;

	private readonly ce427b5a6ee07208e331688e3ede5e795[] c719741ad5138547824c9ab491e7ec0f5 = new ce427b5a6ee07208e331688e3ede5e795[2]
	{
		new ce427b5a6ee07208e331688e3ede5e795(),
		new ce427b5a6ee07208e331688e3ede5e795()
	};

	private Matrix33F cca115fae1c884ef1dec0a6234424f3b1 = Matrix33F.Identity;

	private Matrix33F c77021cfbaf0decc953df6cd7c2ec54f5 = Matrix33F.Identity;

	private Vector3F c2cae79528c2f6469234ffa89a595401d = new Vector3F(0f, -(float)Math.PI / 4f, -(float)Math.PI / 4f);

	private Vector3F c0b8d7516b73b12bd4b0f02823ac9f8b7 = new Vector3F(0f, (float)Math.PI / 4f, (float)Math.PI / 4f);

	private float c7b0155e0d6ae0f40ef24d79ecda3d967 = 0.2f;

	private float c2899dbccd71e3358c2300c70e7b778ee;

	private Vector2F c3029b3eb4d165025b85c5363274411cc;

	private Vector2F c8fac6df59597960db6cfe9088c90c9cd = new Vector2F(float.PositiveInfinity);

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
			if (!(cca115fae1c884ef1dec0a6234424f3b1 != value))
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
					__ldtoken(TwistSwingLimit.set_AnchorOrientationALocal(Matrix33F));
				}
				cca115fae1c884ef1dec0a6234424f3b1 = value;
				OnChanged();
				return;
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
			if (c77021cfbaf0decc953df6cd7c2ec54f5 != value)
			{
				c77021cfbaf0decc953df6cd7c2ec54f5 = value;
				OnChanged();
			}
		}
	}

	/// <summary>
	///       Gets or sets the minimum movement limit on the twist and swing axes.
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///       A minimum swing limit is positive.
	///       </exception>
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
				switch (4)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(TwistSwingLimit.set_Minimum(Vector3F));
				}
				if (!(value.Y > 0f))
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
					if (!(value.Z > 0f))
					{
						c2cae79528c2f6469234ffa89a595401d = value;
						OnChanged();
						return;
					}
				}
				throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(351));
			}
		}
	}

	/// <summary>
	///       Gets or sets the maximum movement limit on the twist and swing axes.
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///       A maximum swing limit is negative.
	///       </exception>
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
				switch (3)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(TwistSwingLimit.set_Maximum(Vector3F));
				}
				if (!(value.Y < 0f))
				{
					if (!(value.Z < 0f))
					{
						c0b8d7516b73b12bd4b0f02823ac9f8b7 = value;
						OnChanged();
						return;
					}
					while (true)
					{
						switch (2)
						{
						case 0:
							continue;
						}
						break;
					}
				}
				throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(458));
			}
		}
	}

	/// <summary>
	///       Gets or sets the error reduction parameter.
	///       </summary>
	public float ErrorReduction
	{
		get
		{
			return c7b0155e0d6ae0f40ef24d79ecda3d967;
		}
		set
		{
			if (c7b0155e0d6ae0f40ef24d79ecda3d967 != value)
			{
				c7b0155e0d6ae0f40ef24d79ecda3d967 = value;
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
	///       Gets or sets the coefficient of restitution for limits.
	///       </summary>
	public Vector2F Restitution
	{
		get
		{
			return c3029b3eb4d165025b85c5363274411cc;
		}
		set
		{
			if (!(c3029b3eb4d165025b85c5363274411cc != value))
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
					__ldtoken(TwistSwingLimit.set_Restitution(Vector2F));
				}
				c3029b3eb4d165025b85c5363274411cc = value;
				OnChanged();
				return;
			}
		}
	}

	/// <summary>
	///       Gets or sets the maximal forces that are applied by this constraint.
	///       </summary>
	public Vector2F MaxForce
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
	public override Vector3F AngularConstraintImpulse => c719741ad5138547824c9ab491e7ec0f5[0].cc01f881484f44c63c9c69258954b12e0 * c719741ad5138547824c9ab491e7ec0f5[0].c270614fc716c17fabf0b32db3d42da8d + c719741ad5138547824c9ab491e7ec0f5[1].cc01f881484f44c63c9c69258954b12e0 * c719741ad5138547824c9ab491e7ec0f5[1].c270614fc716c17fabf0b32db3d42da8d;

	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.Physics.Constraints.TwistSwingLimit" /> class</summary>
	public TwistSwingLimit()
	{
	}

	/// <summary>
	///       Called when constraint should be set-up for a new time step.
	///       </summary>
	protected override void OnSetup()
	{
		Matrix33F matrix = BodyA.Pose.Orientation * AnchorOrientationALocal;
		Matrix33F matrix33F = BodyB.Pose.Orientation * AnchorOrientationBLocal;
		QuaternionF quaternionF = QuaternionF.CreateRotation(matrix33F * matrix.Transposed);
		Vector3F column = matrix.GetColumn(0);
		Vector3F column2 = matrix.GetColumn(1);
		Vector3F column3 = matrix33F.GetColumn(0);
		QuaternionF quaternionF2 = QuaternionF.CreateRotation(column, column3);
		Vector3F c7d96080eb2f6c52630b4332d78a = new Vector3F(quaternionF2.X, quaternionF2.Y, quaternionF2.Z);
		if (!c7d96080eb2f6c52630b4332d78a.TryNormalize())
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
				__ldtoken(TwistSwingLimit.OnSetup());
			}
			c7d96080eb2f6c52630b4332d78a = column2;
		}
		float angle = quaternionF2.Angle;
		Vector3F vector3F = Matrix33F.MultiplyTransposed(matrix, column3);
		float y = vector3F.Y;
		float z = vector3F.Z;
		float num = ((y > 0f) ? Maximum.Z : (0f - Minimum.Z));
		float num2;
		if (!(z > 0f))
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
			num2 = Maximum.Y;
		}
		else
		{
			num2 = 0f - Minimum.Y;
		}
		float num3 = num2;
		float num4 = num3;
		if (!Numeric.IsZero(y))
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
			float num5 = z / y;
			float num6 = num5 * num5;
			float num7 = num * num;
			float num8 = num3 * num3;
			num4 = (float)Math.Sqrt((1f + num6) / (1f / num7 + num6 / num8));
		}
		QuaternionF quaternionF3 = quaternionF2.Conjugated * quaternionF;
		quaternionF3.Normalize();
		float num9 = quaternionF3.Angle;
		if (num9 > (float)Math.PI)
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
			num9 = 0f - ((float)Math.PI * 2f - num9);
		}
		Vector3F vector = new Vector3F(quaternionF3.X, quaternionF3.Y, quaternionF3.Z);
		if (Vector3F.Dot(vector, column) < 0f)
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
			num9 = 0f - num9;
		}
		LimitState c9498271b0ba9856118ef0ba50f3a = c6a2fa7a2595bdda502a7657db30fd6b2[0];
		LimitState c9498271b0ba9856118ef0ba50f3a2 = c6a2fa7a2595bdda502a7657db30fd6b2[1];
		c3c8a85b9ce08fec192d949648775562c(0, num9, column3, Minimum[0], Maximum[0]);
		c3c8a85b9ce08fec192d949648775562c(1, angle, c7d96080eb2f6c52630b4332d78a, 0f - num4, num4);
		c1b70fe54ea8e2058c44156c348d51664(0, c9498271b0ba9856118ef0ba50f3a);
		c1b70fe54ea8e2058c44156c348d51664(1, c9498271b0ba9856118ef0ba50f3a2);
	}

	private void c3c8a85b9ce08fec192d949648775562c(int c88a85a6fc8e0a66e19d046d4d51cd3f1, float c2fed316f5bcf4dd4ea7883930f3822f9, Vector3F c7d96080eb2f6c52630b4332d78a61022, float caaf95081cf14ad7794bfd88499640354, float ce19571f79859f441a405d0d1ee01f53e)
	{
		ce427b5a6ee07208e331688e3ede5e795 ce427b5a6ee07208e331688e3ede5e796 = c719741ad5138547824c9ab491e7ec0f5[c88a85a6fc8e0a66e19d046d4d51cd3f1];
		Simulation simulation = Simulation;
		float fixedTimeStep = simulation.Settings.Timing.FixedTimeStep;
		if (caaf95081cf14ad7794bfd88499640354 > ce19571f79859f441a405d0d1ee01f53e)
		{
			while (true)
			{
				switch (6)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(TwistSwingLimit.c3c8a85b9ce08fec192d949648775562c(int, float, Vector3F, float, float));
					}
					c6a2fa7a2595bdda502a7657db30fd6b2[c88a85a6fc8e0a66e19d046d4d51cd3f1] = LimitState.Inactive;
					return;
				}
			}
		}
		if (Numeric.AreEqual(caaf95081cf14ad7794bfd88499640354, ce19571f79859f441a405d0d1ee01f53e))
		{
			c6a2fa7a2595bdda502a7657db30fd6b2[c88a85a6fc8e0a66e19d046d4d51cd3f1] = LimitState.Locked;
		}
		else if (c2fed316f5bcf4dd4ea7883930f3822f9 <= caaf95081cf14ad7794bfd88499640354)
		{
			c6a2fa7a2595bdda502a7657db30fd6b2[c88a85a6fc8e0a66e19d046d4d51cd3f1] = LimitState.Min;
		}
		else
		{
			if (!(c2fed316f5bcf4dd4ea7883930f3822f9 >= ce19571f79859f441a405d0d1ee01f53e))
			{
				c6a2fa7a2595bdda502a7657db30fd6b2[c88a85a6fc8e0a66e19d046d4d51cd3f1] = LimitState.Inactive;
				return;
			}
			c6a2fa7a2595bdda502a7657db30fd6b2[c88a85a6fc8e0a66e19d046d4d51cd3f1] = LimitState.Max;
		}
		float num = 0f;
		float allowedAngularDeviation = simulation.Settings.Constraints.AllowedAngularDeviation;
		if (c2fed316f5bcf4dd4ea7883930f3822f9 > ce19571f79859f441a405d0d1ee01f53e + allowedAngularDeviation)
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
			num = ce19571f79859f441a405d0d1ee01f53e - c2fed316f5bcf4dd4ea7883930f3822f9 + allowedAngularDeviation;
		}
		else if (c2fed316f5bcf4dd4ea7883930f3822f9 < caaf95081cf14ad7794bfd88499640354 - allowedAngularDeviation)
		{
			num = caaf95081cf14ad7794bfd88499640354 - c2fed316f5bcf4dd4ea7883930f3822f9 - allowedAngularDeviation;
		}
		float value = num * ErrorReduction / fixedTimeStep;
		float maxErrorCorrectionVelocity = simulation.Settings.Constraints.MaxErrorCorrectionVelocity;
		value = MathHelper.Clamp(value, 0f - maxErrorCorrectionVelocity, maxErrorCorrectionVelocity);
		float num2 = Restitution[c88a85a6fc8e0a66e19d046d4d51cd3f1];
		if (num2 > simulation.Settings.Constraints.RestitutionThreshold)
		{
			float num3 = ce427b5a6ee07208e331688e3ede5e796.c1d2b8cb986f37f24e96af6bf3f5b2694(BodyA, BodyB);
			if (c6a2fa7a2595bdda502a7657db30fd6b2[c88a85a6fc8e0a66e19d046d4d51cd3f1] == LimitState.Min)
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
				if (num3 < 0f - Simulation.Settings.Constraints.RestingVelocityLimit)
				{
					value = Math.Max(value, (0f - num3) * num2);
				}
			}
			else if (c6a2fa7a2595bdda502a7657db30fd6b2[c88a85a6fc8e0a66e19d046d4d51cd3f1] == LimitState.Max)
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
				if (num3 > Simulation.Settings.Constraints.RestingVelocityLimit)
				{
					value = Math.Min(value, (0f - num3) * num2);
				}
			}
		}
		ce427b5a6ee07208e331688e3ede5e796.ceef50b9f5f0910e0d3cdffa0ab9e67bf = value;
		float num4 = MaxForce[c88a85a6fc8e0a66e19d046d4d51cd3f1] * fixedTimeStep;
		if (c6a2fa7a2595bdda502a7657db30fd6b2[c88a85a6fc8e0a66e19d046d4d51cd3f1] == LimitState.Min)
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
			c0fa68ea4eef5266cec95704c95b87eed[c88a85a6fc8e0a66e19d046d4d51cd3f1] = 0f;
			c3d39387af73715d5f4670ac095fd9f56[c88a85a6fc8e0a66e19d046d4d51cd3f1] = num4;
		}
		else if (c6a2fa7a2595bdda502a7657db30fd6b2[c88a85a6fc8e0a66e19d046d4d51cd3f1] == LimitState.Max)
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
			c0fa68ea4eef5266cec95704c95b87eed[c88a85a6fc8e0a66e19d046d4d51cd3f1] = 0f - num4;
			c3d39387af73715d5f4670ac095fd9f56[c88a85a6fc8e0a66e19d046d4d51cd3f1] = 0f;
		}
		else
		{
			c0fa68ea4eef5266cec95704c95b87eed[c88a85a6fc8e0a66e19d046d4d51cd3f1] = 0f - num4;
			c3d39387af73715d5f4670ac095fd9f56[c88a85a6fc8e0a66e19d046d4d51cd3f1] = num4;
		}
		ce427b5a6ee07208e331688e3ede5e796.ced8746c2a7c3e88fd7de830408ee6551 = Softness / fixedTimeStep;
		ce427b5a6ee07208e331688e3ede5e796.cb6e3dcfcb9d1bf53e27a1c4bc4921edb(BodyA, BodyB, Vector3F.Zero, -c7d96080eb2f6c52630b4332d78a61022, Vector3F.Zero, c7d96080eb2f6c52630b4332d78a61022);
	}

	private void c1b70fe54ea8e2058c44156c348d51664(int c88a85a6fc8e0a66e19d046d4d51cd3f1, LimitState c9498271b0ba9856118ef0ba50f3a8449)
	{
		ce427b5a6ee07208e331688e3ede5e795 ce427b5a6ee07208e331688e3ede5e796 = c719741ad5138547824c9ab491e7ec0f5[c88a85a6fc8e0a66e19d046d4d51cd3f1];
		if (c9498271b0ba9856118ef0ba50f3a8449 != LimitState.Inactive && c9498271b0ba9856118ef0ba50f3a8449 == c6a2fa7a2595bdda502a7657db30fd6b2[c88a85a6fc8e0a66e19d046d4d51cd3f1])
		{
			while (true)
			{
				switch (2)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(TwistSwingLimit.c1b70fe54ea8e2058c44156c348d51664(int, LimitState));
					}
					ce427b5a6ee07208e331688e3ede5e796.c1b70fe54ea8e2058c44156c348d51664(BodyA, BodyB);
					return;
				}
			}
		}
		if (ce427b5a6ee07208e331688e3ede5e796 == null)
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
		float value = c0004f123fef2ac0bfc76ea9ed6e45574(0);
		float value2 = c0004f123fef2ac0bfc76ea9ed6e45574(1);
		if (!(Math.Abs(value) > Simulation.Settings.Constraints.MinConstraintImpulse))
		{
			while (true)
			{
				switch (5)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(TwistSwingLimit.OnApplyImpulse());
					}
					return Math.Abs(value2) > Simulation.Settings.Constraints.MinConstraintImpulse;
				}
			}
		}
		return true;
	}

	private float c0004f123fef2ac0bfc76ea9ed6e45574(int c88a85a6fc8e0a66e19d046d4d51cd3f1)
	{
		if (c6a2fa7a2595bdda502a7657db30fd6b2[c88a85a6fc8e0a66e19d046d4d51cd3f1] != LimitState.Inactive)
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
						__ldtoken(TwistSwingLimit.c0004f123fef2ac0bfc76ea9ed6e45574(int));
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
		base.OnChanged();
	}

	/// <summary>
	///       Gets a point on the swing limit cone (for debug visualization).
	///       </summary>
	/// <param name="angle">The angle about the twist axis in radians.</param>
	/// <param name="coneTip">The position of the tip of the cone.</param>
	/// <param name="distanceFromTip">The distance from tip.</param>
	/// <returns>
	///       A point that lies on the cone. The distance to the tip of the cone is <paramref name="distanceFromTip" />.
	///       </returns>
	public Vector3F GetPointOnCone(float angle, Vector3F coneTip, float distanceFromTip)
	{
		float num = (float)Math.Cos(angle);
		float num2 = (float)Math.Sin(angle);
		float num3;
		if (!(num > 0f))
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
				__ldtoken(TwistSwingLimit.GetPointOnCone(float, Vector3F, float));
			}
			num3 = 0f - Minimum.Z;
		}
		else
		{
			num3 = Maximum.Z;
		}
		float num4 = num3;
		float num5;
		if (!(num2 > 0f))
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
			num5 = Maximum.Y;
		}
		else
		{
			num5 = 0f - Minimum.Y;
		}
		float num6 = num5;
		float angle2 = num6;
		if (!Numeric.IsZero(num))
		{
			float num7 = num2 / num;
			float num8 = num7 * num7;
			float num9 = num4 * num4;
			float num10 = num6 * num6;
			angle2 = (float)Math.Sqrt((1f + num8) / (1f / num9 + num8 / num10));
		}
		Vector3F axis = new Vector3F(0f, 0f - num2, num);
		Vector3F vector3F = QuaternionF.CreateRotation(axis, angle2).Rotate(new Vector3F(distanceFromTip, 0f, 0f));
		Vector3F vector3F2 = AnchorOrientationALocal * vector3F;
		if (BodyA != null)
		{
			Vector3F vector3F3 = BodyA.Pose.ToWorldDirection(vector3F2);
			return vector3F3 + coneTip;
		}
		return vector3F2 + coneTip;
	}
}
