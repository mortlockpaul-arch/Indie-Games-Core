using System;
using DigitalRune.Geometry;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Physics.Constraints;

/// <summary>
///       Defines a joint that allows rotations on two axis. The joint can be used to model the
///       front wheel of a car.
///       </summary>
public class Hinge2Joint : Constraint
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
				switch (1)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(Hinge2Joint.set_AnchorPoseALocal(Pose));
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
				switch (4)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(Hinge2Joint.set_AnchorPoseBLocal(Pose));
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
				switch (4)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(Hinge2Joint.set_ErrorReduction(float));
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
			if (value == Softness)
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
					__ldtoken(Hinge2Joint.set_Softness(float));
				}
				cfc1538019fdd8122a84eb114fb0ecf14.Softness = new Vector3F(value);
				c0fabe4906bd5c392e88d5710f1032715.Softness = new Vector3F(value);
				OnChanged();
				return;
			}
		}
	}

	/// <summary>
	///       Gets or sets the minimum rotation limits about the two rotation axis.
	///       </summary>
	public Vector2F Minimum
	{
		get
		{
			return new Vector2F(c0fabe4906bd5c392e88d5710f1032715.Minimum.X, c0fabe4906bd5c392e88d5710f1032715.Minimum.Z);
		}
		set
		{
			if (value != Minimum)
			{
				c0fabe4906bd5c392e88d5710f1032715.Minimum = new Vector3F(value.X, 0f, value.Y);
				OnChanged();
			}
		}
	}

	/// <summary>
	///       Gets or sets the maximum rotation limits about the two rotation axis.
	///       </summary>
	public Vector2F Maximum
	{
		get
		{
			return new Vector2F(c0fabe4906bd5c392e88d5710f1032715.Maximum.X, c0fabe4906bd5c392e88d5710f1032715.Maximum.Z);
		}
		set
		{
			if (!(value != Maximum))
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
					__ldtoken(Hinge2Joint.set_Maximum(Vector2F));
				}
				c0fabe4906bd5c392e88d5710f1032715.Maximum = new Vector3F(value.X, 0f, value.Y);
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
			return c0fabe4906bd5c392e88d5710f1032715.Restitution.X;
		}
		set
		{
			if (value != Restitution)
			{
				c0fabe4906bd5c392e88d5710f1032715.Restitution = new Vector3F(value);
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
	///       Initializes a new instance of the <see cref="T:DigitalRune.Physics.Constraints.Hinge2Joint" /> class.
	///       </summary>
	public Hinge2Joint()
	{
		cfc1538019fdd8122a84eb114fb0ecf14 = new LinearLimit
		{
			Minimum = new Vector3F(0f, 0f, 0f),
			Maximum = new Vector3F(0f, 0f, 0f)
		};
		c0fabe4906bd5c392e88d5710f1032715 = new AngularLimit
		{
			Minimum = new Vector3F(-(float)Math.PI / 4f, 0f, float.NegativeInfinity),
			Maximum = new Vector3F((float)Math.PI / 4f, 0f, float.PositiveInfinity)
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
