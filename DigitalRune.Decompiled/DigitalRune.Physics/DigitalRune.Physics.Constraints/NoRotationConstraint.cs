using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Physics.Constraints;

/// <summary>
///       Defines a constraint that fixes the relative orientation of two bodies.
///       </summary>
public class NoRotationConstraint : Constraint
{
	private readonly AngularLimit c0fabe4906bd5c392e88d5710f1032715;

	/// <summary>
	///       Gets or sets the constraint anchor orientation on <see cref="P:DigitalRune.Physics.Constraints.Constraint.BodyA" /> in local 
	///       space of <see cref="P:DigitalRune.Physics.Constraints.Constraint.BodyA" />.
	///       </summary>
	public Matrix33F AnchorOrientationALocal
	{
		get
		{
			return c0fabe4906bd5c392e88d5710f1032715.AnchorOrientationALocal;
		}
		set
		{
			if (value != AnchorOrientationALocal)
			{
				c0fabe4906bd5c392e88d5710f1032715.AnchorOrientationALocal = value;
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
			return c0fabe4906bd5c392e88d5710f1032715.AnchorOrientationBLocal;
		}
		set
		{
			if (value != AnchorOrientationBLocal)
			{
				c0fabe4906bd5c392e88d5710f1032715.AnchorOrientationBLocal = value;
				OnChanged();
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
			return c0fabe4906bd5c392e88d5710f1032715.ErrorReduction.X;
		}
		set
		{
			if (value == ErrorReduction)
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
					__ldtoken(NoRotationConstraint.set_ErrorReduction(float));
				}
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
			return c0fabe4906bd5c392e88d5710f1032715.Softness.X;
		}
		set
		{
			if (value != Softness)
			{
				c0fabe4906bd5c392e88d5710f1032715.Softness = new Vector3F(value);
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
			return c0fabe4906bd5c392e88d5710f1032715.MaxForce.X;
		}
		set
		{
			if (value == MaxForce)
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
					__ldtoken(NoRotationConstraint.set_MaxForce(float));
				}
				c0fabe4906bd5c392e88d5710f1032715.MaxForce = new Vector3F(value);
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
	public override Vector3F AngularConstraintImpulse => c0fabe4906bd5c392e88d5710f1032715.AngularConstraintImpulse;

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Physics.Constraints.NoRotationConstraint" /> class.
	///       </summary>
	public NoRotationConstraint()
	{
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
		c0fabe4906bd5c392e88d5710f1032715.Simulation = Simulation;
		base.OnAddToSimulation();
	}

	/// <summary>
	///       Called when this constraint is removed from a simulation.
	///       </summary>
	protected override void OnRemoveFromSimulation()
	{
		c0fabe4906bd5c392e88d5710f1032715.Simulation = null;
		base.OnRemoveFromSimulation();
	}

	/// <summary>
	///       Called when constraint should be set-up for a new time step.
	///       </summary>
	protected override void OnSetup()
	{
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
		return c0fabe4906bd5c392e88d5710f1032715.ApplyImpulse();
	}

	/// <summary>
	///       Called when properties of this constraint were changed.
	///       </summary>
	protected override void OnChanged()
	{
		c0fabe4906bd5c392e88d5710f1032715.BodyA = BodyA;
		c0fabe4906bd5c392e88d5710f1032715.BodyB = BodyB;
		base.OnChanged();
	}
}
