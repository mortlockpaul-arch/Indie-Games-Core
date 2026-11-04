using System;
using A;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Physics.Constraints;

/// <summary>
///       Defines a constraint that limits the distance of two points.
///       </summary>
public class DistanceLimit : Constraint
{
	private float cba181d738bf534c2390ad3b4ddf23601;

	private Vector3F c9ab589fe156dc19766969bc9aed9fff3;

	private Vector3F cc5a1873e70892f05d48169557677fda4;

	private Vector3F c2fbf8505fe64d1c145fd61f5436e170f;

	private bool c480672ed4b1d025de68294f42d6826c5;

	private bool c16098ee0fd2c52e89cb05f1bcd26d068;

	private readonly ce427b5a6ee07208e331688e3ede5e795 c0ae11529b11ec697e2efa369199cfe54 = new ce427b5a6ee07208e331688e3ede5e795();

	private Vector3F c1c685894f57c89ddc60c97e1a0170644;

	private Vector3F cb15f5b3befd19bec2299cd680edfd098;

	private float ca9850964576d733755d4c5f612ccb226;

	private float c38409572742088a1bd2170f343c36bc1 = 1f;

	private float c7b0155e0d6ae0f40ef24d79ecda3d967 = 0.2f;

	private float c2899dbccd71e3358c2300c70e7b778ee;

	private float c8fac6df59597960db6cfe9088c90c9cd = float.PositiveInfinity;

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
			c1c685894f57c89ddc60c97e1a0170644 = value;
			OnChanged();
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
			cb15f5b3befd19bec2299cd680edfd098 = value;
			OnChanged();
		}
	}

	/// <summary>
	///       Gets or sets the minimal allowed distance between the constraint anchor points.
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is negative.
	///       </exception>
	public float MinDistance
	{
		get
		{
			return ca9850964576d733755d4c5f612ccb226;
		}
		set
		{
			if (value < 0f)
			{
				while (true)
				{
					switch (3)
					{
					case 0:
						break;
					default:
						if (1 == 0)
						{
							__ldtoken(DistanceLimit.set_MinDistance(float));
						}
						throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4986));
					}
				}
			}
			ca9850964576d733755d4c5f612ccb226 = value;
			OnChanged();
		}
	}

	/// <summary>
	///       Gets or sets the maximal allowed distance between the constraint anchor points.
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is negative.
	///       </exception>
	public float MaxDistance
	{
		get
		{
			return c38409572742088a1bd2170f343c36bc1;
		}
		set
		{
			if (value < 0f)
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
							__ldtoken(DistanceLimit.set_MaxDistance(float));
						}
						throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5055));
					}
				}
			}
			c38409572742088a1bd2170f343c36bc1 = value;
			OnChanged();
		}
	}

	/// <summary>
	///       Gets or sets the linear constraint impulse that was applied. 
	///       </summary>
	public override Vector3F LinearConstraintImpulse => c0ae11529b11ec697e2efa369199cfe54.cc01f881484f44c63c9c69258954b12e0 * c0ae11529b11ec697e2efa369199cfe54.cd113dd0023606dbb59275f5f206680fc;

	/// <summary>
	///       Gets or sets the angular constraint impulse that was applied.
	///       </summary>
	public override Vector3F AngularConstraintImpulse => Vector3F.Zero;

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
			c7b0155e0d6ae0f40ef24d79ecda3d967 = value;
			OnChanged();
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
			if (c2899dbccd71e3358c2300c70e7b778ee == value)
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
					__ldtoken(DistanceLimit.set_Softness(float));
				}
				c2899dbccd71e3358c2300c70e7b778ee = value;
				OnChanged();
				return;
			}
		}
	}

	/// <summary>
	///       Gets or sets the maximal force that is applied by this constraint.
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

	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.Physics.Constraints.DistanceLimit" /> class</summary>
	public DistanceLimit()
	{
	}

	/// <summary>
	///       Called when constraint should be set-up for a new time step.
	///       </summary>
	protected override void OnSetup()
	{
		cba181d738bf534c2390ad3b4ddf23601 = Simulation.Settings.Timing.FixedTimeStep;
		Vector3F vector3F = BodyA.Pose.ToWorldPosition(AnchorPositionALocal);
		Vector3F vector3F2 = BodyB.Pose.ToWorldPosition(AnchorPositionBLocal);
		c2fbf8505fe64d1c145fd61f5436e170f = vector3F2 - vector3F;
		float length = c2fbf8505fe64d1c145fd61f5436e170f.Length;
		c480672ed4b1d025de68294f42d6826c5 = length < MinDistance && !Numeric.IsZero(length);
		c16098ee0fd2c52e89cb05f1bcd26d068 = length > MaxDistance;
		if (!c480672ed4b1d025de68294f42d6826c5 && !c16098ee0fd2c52e89cb05f1bcd26d068)
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
						__ldtoken(DistanceLimit.OnSetup());
					}
					c0ae11529b11ec697e2efa369199cfe54.cc01f881484f44c63c9c69258954b12e0 = 0f;
					return;
				}
			}
		}
		c2fbf8505fe64d1c145fd61f5436e170f.TryNormalize();
		c9ab589fe156dc19766969bc9aed9fff3 = vector3F - BodyA.PoseCenterOfMass.Position;
		cc5a1873e70892f05d48169557677fda4 = vector3F2 - BodyB.PoseCenterOfMass.Position;
		float num;
		if (!c480672ed4b1d025de68294f42d6826c5)
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
			num = MaxDistance - length;
		}
		else
		{
			num = MinDistance - length;
		}
		float num2 = num;
		c0ae11529b11ec697e2efa369199cfe54.ceef50b9f5f0910e0d3cdffa0ab9e67bf = num2 * ErrorReduction / cba181d738bf534c2390ad3b4ddf23601;
		float maxErrorCorrectionVelocity = Simulation.Settings.Constraints.MaxErrorCorrectionVelocity;
		c0ae11529b11ec697e2efa369199cfe54.ceef50b9f5f0910e0d3cdffa0ab9e67bf = MathHelper.Clamp(c0ae11529b11ec697e2efa369199cfe54.ceef50b9f5f0910e0d3cdffa0ab9e67bf, 0f - maxErrorCorrectionVelocity, maxErrorCorrectionVelocity);
		c0ae11529b11ec697e2efa369199cfe54.ced8746c2a7c3e88fd7de830408ee6551 = Softness / cba181d738bf534c2390ad3b4ddf23601;
		c0ae11529b11ec697e2efa369199cfe54.cb6e3dcfcb9d1bf53e27a1c4bc4921edb(BodyA, BodyB, -c2fbf8505fe64d1c145fd61f5436e170f, -Vector3F.Cross(c9ab589fe156dc19766969bc9aed9fff3, c2fbf8505fe64d1c145fd61f5436e170f), c2fbf8505fe64d1c145fd61f5436e170f, Vector3F.Cross(cc5a1873e70892f05d48169557677fda4, c2fbf8505fe64d1c145fd61f5436e170f));
		c0ae11529b11ec697e2efa369199cfe54.cc01f881484f44c63c9c69258954b12e0 = 0f;
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
		if (!c480672ed4b1d025de68294f42d6826c5 && !c16098ee0fd2c52e89cb05f1bcd26d068)
		{
			return false;
		}
		float c3e548b3010de6dec5144f5f94f3c = c0ae11529b11ec697e2efa369199cfe54.c1d2b8cb986f37f24e96af6bf3f5b2694(BodyA, BodyB);
		float num = MaxForce * cba181d738bf534c2390ad3b4ddf23601;
		float num2;
		if (!c16098ee0fd2c52e89cb05f1bcd26d068)
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
				__ldtoken(DistanceLimit.OnApplyImpulse());
			}
			num2 = 0f;
		}
		else
		{
			num2 = 0f - num;
		}
		float c17c3c23a0a9a4f253767cc120f675e = num2;
		float num3;
		if (!c480672ed4b1d025de68294f42d6826c5)
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
			num3 = 0f;
		}
		else
		{
			num3 = num;
		}
		float c04a7381cf16e4e31af82211a35f9f4e = num3;
		float value = c0ae11529b11ec697e2efa369199cfe54.c5ff4a78f1a80b7e61a2f125dd4c0d54f(BodyA, BodyB, c3e548b3010de6dec5144f5f94f3c, c17c3c23a0a9a4f253767cc120f675e, c04a7381cf16e4e31af82211a35f9f4e);
		return Math.Abs(value) > Simulation.Settings.Constraints.MinConstraintImpulse;
	}

	/// <summary>
	///       Called when properties of this constraint were changed.
	///       </summary>
	protected override void OnChanged()
	{
		c0ae11529b11ec697e2efa369199cfe54.cc01f881484f44c63c9c69258954b12e0 = 0f;
		base.OnChanged();
	}
}
