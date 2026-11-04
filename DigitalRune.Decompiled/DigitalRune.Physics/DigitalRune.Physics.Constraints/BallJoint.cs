using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Physics.Constraints;

/// <summary>
///       Defines a ball-and-socked joint.
///       </summary>
public class BallJoint : Constraint
{
	private float cba181d738bf534c2390ad3b4ddf23601;

	private Vector3F c740e97043f35961709f4f77d67471ebb;

	private Vector3F cd449d82d118d1b8cc40eef4d90bb341e;

	private Matrix33F cfb956f9f5377c9c26b4c3585c243d3c0;

	private Vector3F c6912c9bd9a7aa3408b6a81f7e4bc54df;

	private Vector3F c1c685894f57c89ddc60c97e1a0170644;

	private Vector3F cb15f5b3befd19bec2299cd680edfd098;

	private Vector3F c100e49ce7eea87b81cca30ae47e1aea5;

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
	///       Gets or sets the linear constraint impulse that was applied. 
	///       </summary>
	public override Vector3F LinearConstraintImpulse => c100e49ce7eea87b81cca30ae47e1aea5;

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
				switch (4)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(BallJoint.set_Softness(float));
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
			if (c8fac6df59597960db6cfe9088c90c9cd == value)
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
					__ldtoken(BallJoint.set_MaxForce(float));
				}
				c8fac6df59597960db6cfe9088c90c9cd = value;
				OnChanged();
				return;
			}
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.Physics.Constraints.BallJoint" /> class</summary>
	public BallJoint()
	{
	}

	/// <summary>
	///       Called when constraint should be set-up for a new time step.
	///       </summary>
	protected override void OnSetup()
	{
		cba181d738bf534c2390ad3b4ddf23601 = Simulation.Settings.Timing.FixedTimeStep;
		c740e97043f35961709f4f77d67471ebb = BodyA.Pose.ToWorldPosition(AnchorPositionALocal);
		cd449d82d118d1b8cc40eef4d90bb341e = BodyB.Pose.ToWorldPosition(AnchorPositionBLocal);
		Matrix33F matrix33F = BodyA.ComputeKMatrix(c740e97043f35961709f4f77d67471ebb) + BodyB.ComputeKMatrix(cd449d82d118d1b8cc40eef4d90bb341e);
		matrix33F.M00 += Softness / cba181d738bf534c2390ad3b4ddf23601;
		matrix33F.M11 += Softness / cba181d738bf534c2390ad3b4ddf23601;
		matrix33F.M22 += Softness / cba181d738bf534c2390ad3b4ddf23601;
		cfb956f9f5377c9c26b4c3585c243d3c0 = matrix33F.Inverse;
		Vector3F vector3F = cd449d82d118d1b8cc40eef4d90bb341e - c740e97043f35961709f4f77d67471ebb;
		float length = vector3F.Length;
		Vector3F vector3F2 = vector3F;
		vector3F2.TryNormalize();
		c6912c9bd9a7aa3408b6a81f7e4bc54df = (0f - length) * (1f / cba181d738bf534c2390ad3b4ddf23601) * ErrorReduction * vector3F2;
		if (c6912c9bd9a7aa3408b6a81f7e4bc54df.LengthSquared > Simulation.Settings.Constraints.MaxErrorCorrectionVelocitySquared)
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
				__ldtoken(BallJoint.OnSetup());
			}
			c6912c9bd9a7aa3408b6a81f7e4bc54df = (0f - Simulation.Settings.Constraints.MaxErrorCorrectionVelocity) * vector3F2;
		}
		BodyA.ApplyImpulse(-c100e49ce7eea87b81cca30ae47e1aea5, c740e97043f35961709f4f77d67471ebb);
		BodyB.ApplyImpulse(c100e49ce7eea87b81cca30ae47e1aea5, cd449d82d118d1b8cc40eef4d90bb341e);
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
		Vector3F velocityOfWorldPoint = BodyA.GetVelocityOfWorldPoint(c740e97043f35961709f4f77d67471ebb);
		Vector3F velocityOfWorldPoint2 = BodyB.GetVelocityOfWorldPoint(cd449d82d118d1b8cc40eef4d90bb341e);
		Vector3F vector3F = velocityOfWorldPoint2 - velocityOfWorldPoint;
		Vector3F vector3F2 = cfb956f9f5377c9c26b4c3585c243d3c0 * (c6912c9bd9a7aa3408b6a81f7e4bc54df - vector3F - Softness / cba181d738bf534c2390ad3b4ddf23601 * c100e49ce7eea87b81cca30ae47e1aea5);
		Vector3F vector3F3 = c100e49ce7eea87b81cca30ae47e1aea5;
		c100e49ce7eea87b81cca30ae47e1aea5 += vector3F2;
		float length = c100e49ce7eea87b81cca30ae47e1aea5.Length;
		float num = MaxForce * cba181d738bf534c2390ad3b4ddf23601;
		if (length > num)
		{
			c100e49ce7eea87b81cca30ae47e1aea5 = c100e49ce7eea87b81cca30ae47e1aea5 / length * num;
			vector3F2 = c100e49ce7eea87b81cca30ae47e1aea5 - vector3F3;
		}
		BodyA.ApplyImpulse(-vector3F2, c740e97043f35961709f4f77d67471ebb);
		BodyB.ApplyImpulse(vector3F2, cd449d82d118d1b8cc40eef4d90bb341e);
		return vector3F2.LengthSquared > Simulation.Settings.Constraints.MinConstraintImpulseSquared;
	}

	/// <summary>
	///       Called when properties of this constraint were changed.
	///       </summary>
	protected override void OnChanged()
	{
		c100e49ce7eea87b81cca30ae47e1aea5 = Vector3F.Zero;
		base.OnChanged();
	}
}
