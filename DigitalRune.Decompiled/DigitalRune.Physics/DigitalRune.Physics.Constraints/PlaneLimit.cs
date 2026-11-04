using System;
using A;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Physics.Constraints;

/// <summary>
///       Defines a limit plane and a limit point that must stay in front of the plane.
///       </summary>
public class PlaneLimit : Constraint
{
	private bool cd2ba92bdd69584b115c87b910eeea261;

	private readonly ce427b5a6ee07208e331688e3ede5e795 c0ae11529b11ec697e2efa369199cfe54 = new ce427b5a6ee07208e331688e3ede5e795();

	private Plane c977a96600d6c2331b4a50a1111909316 = new Plane(Vector3F.UnitY, 0f);

	private Vector3F cb15f5b3befd19bec2299cd680edfd098;

	private float c7b0155e0d6ae0f40ef24d79ecda3d967 = 0.2f;

	private float c2899dbccd71e3358c2300c70e7b778ee;

	private float c3029b3eb4d165025b85c5363274411cc;

	private float c8fac6df59597960db6cfe9088c90c9cd = float.PositiveInfinity;

	/// <summary>
	///       Gets or sets the constraint plane that this is fixed on <see cref="P:DigitalRune.Physics.Constraints.Constraint.BodyA" /> 
	///       (defined in local space of <see cref="P:DigitalRune.Physics.Constraints.Constraint.BodyA" />).
	///       </summary>
	public Plane PlaneALocal
	{
		get
		{
			return c977a96600d6c2331b4a50a1111909316;
		}
		set
		{
			c977a96600d6c2331b4a50a1111909316 = value;
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
			if (!(cb15f5b3befd19bec2299cd680edfd098 != value))
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
					__ldtoken(PlaneLimit.set_AnchorPositionBLocal(Vector3F));
				}
				cb15f5b3befd19bec2299cd680edfd098 = value;
				OnChanged();
				return;
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
			if (c2899dbccd71e3358c2300c70e7b778ee == value)
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
					__ldtoken(PlaneLimit.set_Softness(float));
				}
				c2899dbccd71e3358c2300c70e7b778ee = value;
				OnChanged();
				return;
			}
		}
	}

	/// <summary>
	///       Gets or sets the coefficient of restitution.
	///       </summary>
	public float Restitution
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

	/// <summary>
	///       Gets or sets the constraint impulse that was applied. 
	///       </summary>
	public Vector3F ConstraintImpulse => c0ae11529b11ec697e2efa369199cfe54.cc01f881484f44c63c9c69258954b12e0 * c0ae11529b11ec697e2efa369199cfe54.cd113dd0023606dbb59275f5f206680fc;

	/// <summary>
	///       Gets or sets the linear constraint impulse that was applied. 
	///       </summary>
	public override Vector3F LinearConstraintImpulse => c0ae11529b11ec697e2efa369199cfe54.cc01f881484f44c63c9c69258954b12e0 * c0ae11529b11ec697e2efa369199cfe54.cd113dd0023606dbb59275f5f206680fc;

	/// <summary>
	///       Gets or sets the angular constraint impulse that was applied.
	///       </summary>
	public override Vector3F AngularConstraintImpulse => Vector3F.Zero;

	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.Physics.Constraints.PlaneLimit" /> class</summary>
	public PlaneLimit()
	{
	}

	/// <summary>
	///       Called when constraint should be set-up for a new time step.
	///       </summary>
	protected override void OnSetup()
	{
		Plane planeALocal = PlaneALocal;
		Vector3F vector3F = BodyA.Pose.ToWorldDirection(planeALocal.Normal);
		Vector3F vector = BodyA.Pose.Position + vector3F * planeALocal.DistanceFromOrigin;
		float num = Vector3F.Dot(vector, vector3F);
		Vector3F vector3F2 = BodyB.Pose.ToWorldPosition(AnchorPositionBLocal);
		float num2 = Vector3F.Dot(vector3F2, vector3F) - num;
		float num3 = 0f - num2;
		Vector3F vector2 = vector3F2 - BodyA.PoseCenterOfMass.Position;
		Vector3F vector3 = vector3F2 - BodyB.PoseCenterOfMass.Position;
		bool flag = cd2ba92bdd69584b115c87b910eeea261;
		if (num2 <= 0f)
		{
			cd2ba92bdd69584b115c87b910eeea261 = true;
			Simulation simulation = Simulation;
			float fixedTimeStep = simulation.Settings.Timing.FixedTimeStep;
			float value = num3 * ErrorReduction / fixedTimeStep;
			float maxErrorCorrectionVelocity = simulation.Settings.Constraints.MaxErrorCorrectionVelocity;
			value = MathHelper.Clamp(value, 0f - maxErrorCorrectionVelocity, maxErrorCorrectionVelocity);
			if (Restitution > simulation.Settings.Constraints.RestitutionThreshold)
			{
				float num4 = c0ae11529b11ec697e2efa369199cfe54.c1d2b8cb986f37f24e96af6bf3f5b2694(BodyA, BodyB);
				if (num4 < 0f - Simulation.Settings.Constraints.RestingVelocityLimit)
				{
					value = Math.Max(value, (0f - num4) * Restitution);
				}
			}
			c0ae11529b11ec697e2efa369199cfe54.ceef50b9f5f0910e0d3cdffa0ab9e67bf = value;
			c0ae11529b11ec697e2efa369199cfe54.ced8746c2a7c3e88fd7de830408ee6551 = Softness / fixedTimeStep;
			c0ae11529b11ec697e2efa369199cfe54.cb6e3dcfcb9d1bf53e27a1c4bc4921edb(BodyA, BodyB, -vector3F, -Vector3F.Cross(vector2, vector3F), vector3F, Vector3F.Cross(vector3, vector3F));
		}
		else
		{
			cd2ba92bdd69584b115c87b910eeea261 = false;
		}
		if (flag && cd2ba92bdd69584b115c87b910eeea261)
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
						__ldtoken(PlaneLimit.OnSetup());
					}
					c0ae11529b11ec697e2efa369199cfe54.c1b70fe54ea8e2058c44156c348d51664(BodyA, BodyB);
					return;
				}
			}
		}
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
		if (!cd2ba92bdd69584b115c87b910eeea261)
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
						__ldtoken(PlaneLimit.OnApplyImpulse());
					}
					return false;
				}
			}
		}
		float c3e548b3010de6dec5144f5f94f3c = c0ae11529b11ec697e2efa369199cfe54.c1d2b8cb986f37f24e96af6bf3f5b2694(BodyA, BodyB);
		float value = c0ae11529b11ec697e2efa369199cfe54.c5ff4a78f1a80b7e61a2f125dd4c0d54f(BodyA, BodyB, c3e548b3010de6dec5144f5f94f3c, 0f, MaxForce);
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
