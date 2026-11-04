using DigitalRune.Geometry;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Physics.Constraints;

/// <summary>
///       Defines a prismatic slider joint.
///       </summary>
public class PrismaticJoint : Constraint
{
	private readonly LinearLimit cfc1538019fdd8122a84eb114fb0ecf14;

	private readonly AngularLimit c0fabe4906bd5c392e88d5710f1032715;

	/// <summary>
	///       Gets or sets the constraint anchor pose on <see cref="P:DigitalRune.Physics.Constraints.Constraint.BodyA" /> in local space of 
	///       <see cref="P:DigitalRune.Physics.Constraints.Constraint.BodyA" />.
	///       </summary>
	public Pose AnchorPoseALocal
	{
		get
		{
			return cfc1538019fdd8122a84eb114fb0ecf14.AnchorPoseALocal;
		}
		set
		{
			if (!(value != AnchorPoseALocal))
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
					__ldtoken(PrismaticJoint.set_AnchorPoseALocal(Pose));
				}
				cfc1538019fdd8122a84eb114fb0ecf14.AnchorPoseALocal = value;
				c0fabe4906bd5c392e88d5710f1032715.AnchorOrientationALocal = value.Orientation;
				OnChanged();
				return;
			}
		}
	}

	/// <summary>
	///       Gets or sets the constraint anchor pose on <see cref="P:DigitalRune.Physics.Constraints.Constraint.BodyB" /> in local space of 
	///       <see cref="P:DigitalRune.Physics.Constraints.Constraint.BodyB" />.
	///       </summary>
	public Pose AnchorPoseBLocal
	{
		get
		{
			return new Pose(cfc1538019fdd8122a84eb114fb0ecf14.AnchorPositionBLocal, c0fabe4906bd5c392e88d5710f1032715.AnchorOrientationBLocal);
		}
		set
		{
			if (!(value != AnchorPoseBLocal))
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
					__ldtoken(PrismaticJoint.set_AnchorPoseBLocal(Pose));
				}
				cfc1538019fdd8122a84eb114fb0ecf14.AnchorPositionBLocal = value.Position;
				c0fabe4906bd5c392e88d5710f1032715.AnchorOrientationBLocal = value.Orientation;
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
			return cfc1538019fdd8122a84eb114fb0ecf14.ErrorReduction.X;
		}
		set
		{
			if (value == ErrorReduction)
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
					__ldtoken(PrismaticJoint.set_ErrorReduction(float));
				}
				cfc1538019fdd8122a84eb114fb0ecf14.ErrorReduction = new Vector3F(value);
				c0fabe4906bd5c392e88d5710f1032715.ErrorReduction = new Vector3F(value);
				OnChanged();
				return;
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
			return cfc1538019fdd8122a84eb114fb0ecf14.Softness.X;
		}
		set
		{
			if (value != Softness)
			{
				cfc1538019fdd8122a84eb114fb0ecf14.Softness = new Vector3F(value);
				c0fabe4906bd5c392e88d5710f1032715.Softness = new Vector3F(value);
				OnChanged();
			}
		}
	}

	/// <summary>
	///       Gets or sets the minimum movement limit on the slider axis.
	///       </summary>
	public float Minimum
	{
		get
		{
			return cfc1538019fdd8122a84eb114fb0ecf14.Minimum.X;
		}
		set
		{
			if (value == Minimum)
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
					__ldtoken(PrismaticJoint.set_Minimum(float));
				}
				cfc1538019fdd8122a84eb114fb0ecf14.Minimum = new Vector3F(value, 0f, 0f);
				OnChanged();
				return;
			}
		}
	}

	/// <summary>
	///       Gets or sets the maximum movement limit on the slider axis.
	///       </summary>
	public float Maximum
	{
		get
		{
			return cfc1538019fdd8122a84eb114fb0ecf14.Maximum.X;
		}
		set
		{
			if (value == Maximum)
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
					__ldtoken(PrismaticJoint.set_Maximum(float));
				}
				cfc1538019fdd8122a84eb114fb0ecf14.Maximum = new Vector3F(value, 0f, 0f);
				OnChanged();
				return;
			}
		}
	}

	/// <summary>
	///       Gets or sets the coefficient of restitution for slider limits.
	///       </summary>
	public float Restitution
	{
		get
		{
			return cfc1538019fdd8122a84eb114fb0ecf14.Restitution.X;
		}
		set
		{
			if (value == Restitution)
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
					__ldtoken(PrismaticJoint.set_Restitution(float));
				}
				cfc1538019fdd8122a84eb114fb0ecf14.Restitution = new Vector3F(value);
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
			return cfc1538019fdd8122a84eb114fb0ecf14.MaxForce.X;
		}
		set
		{
			if (value != Restitution)
			{
				cfc1538019fdd8122a84eb114fb0ecf14.MaxForce = new Vector3F(value);
				c0fabe4906bd5c392e88d5710f1032715.MaxForce = new Vector3F(value);
				OnChanged();
			}
		}
	}

	/// <summary>
	///       Gets or sets the linear constraint impulse that was applied. 
	///       </summary>
	public override Vector3F LinearConstraintImpulse => cfc1538019fdd8122a84eb114fb0ecf14.LinearConstraintImpulse;

	/// <summary>
	///       Gets or sets the angular constraint impulse that was applied.
	///       </summary>
	public override Vector3F AngularConstraintImpulse => c0fabe4906bd5c392e88d5710f1032715.AngularConstraintImpulse;

	/// <summary>
	///       Gets the relative position on the slider axis.
	///       </summary>
	public float RelativePosition => cfc1538019fdd8122a84eb114fb0ecf14.RelativePosition.X;

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Physics.Constraints.PrismaticJoint" /> class.
	///       </summary>
	public PrismaticJoint()
	{
		cfc1538019fdd8122a84eb114fb0ecf14 = new LinearLimit
		{
			Minimum = new Vector3F(float.NegativeInfinity, 0f, 0f),
			Maximum = new Vector3F(float.PositiveInfinity, 0f, 0f)
		};
		c0fabe4906bd5c392e88d5710f1032715 = new AngularLimit
		{
			Minimum = new Vector3F(0f, 0f, 0f),
			Maximum = new Vector3F(0f, 0f, 0f)
		};
	}

	/// <summary>
	///       Called when this constraint is added to a simulation.
	///       </summary>
	/// <exception cref="T:DigitalRune.Physics.PhysicsException">
	///   <see cref="P:DigitalRune.Physics.Constraints.Constraint.BodyA" /> or <see cref="P:DigitalRune.Physics.Constraints.Constraint.BodyB" /> is <see langword="null" />. The constraint bodies 
	///       must not be <see langword="null" /> when a constraint/joint is added to a simulation.
	///       </exception>
	protected override void OnAddToSimulation()
	{
		cfc1538019fdd8122a84eb114fb0ecf14.Simulation = Simulation;
		c0fabe4906bd5c392e88d5710f1032715.Simulation = Simulation;
		base.OnAddToSimulation();
	}

	/// <summary>
	///       Called when this constraint is removed from a simulation.
	///       </summary>
	protected override void OnRemoveFromSimulation()
	{
		cfc1538019fdd8122a84eb114fb0ecf14.Simulation = null;
		c0fabe4906bd5c392e88d5710f1032715.Simulation = null;
		base.OnRemoveFromSimulation();
	}

	/// <summary>
	///       Called when constraint should be set-up for a new time step.
	///       </summary>
	protected override void OnSetup()
	{
		cfc1538019fdd8122a84eb114fb0ecf14.Setup();
		c0fabe4906bd5c392e88d5710f1032715.Setup();
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
		bool flag = cfc1538019fdd8122a84eb114fb0ecf14.ApplyImpulse();
		return c0fabe4906bd5c392e88d5710f1032715.ApplyImpulse() || flag;
	}

	/// <summary>
	///       Called when properties of this constraint were changed.
	///       </summary>
	protected override void OnChanged()
	{
		cfc1538019fdd8122a84eb114fb0ecf14.BodyA = BodyA;
		cfc1538019fdd8122a84eb114fb0ecf14.BodyB = BodyB;
		c0fabe4906bd5c392e88d5710f1032715.BodyA = BodyA;
		c0fabe4906bd5c392e88d5710f1032715.BodyB = BodyB;
		base.OnChanged();
	}
}
