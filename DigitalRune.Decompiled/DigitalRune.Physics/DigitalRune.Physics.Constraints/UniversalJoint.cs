using System;
using DigitalRune.Geometry;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Physics.Constraints;

/// <summary>
///       Defines a joint that allows rotations about two axes.
///       </summary>
public class UniversalJoint : Constraint
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
			if (value != AnchorPoseALocal)
			{
				cfc1538019fdd8122a84eb114fb0ecf14.AnchorPoseALocal = value;
				Matrix33F anchorOrientationALocal = default;
				anchorOrientationALocal.SetColumn(0, value.Orientation.GetColumn(0));
				anchorOrientationALocal.SetColumn(1, value.Orientation.GetColumn(2));
				anchorOrientationALocal.SetColumn(2, -value.Orientation.GetColumn(1));
				c0fabe4906bd5c392e88d5710f1032715.AnchorOrientationALocal = anchorOrientationALocal;
				OnChanged();
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
			Matrix33F orientation = default;
			orientation.SetColumn(0, c0fabe4906bd5c392e88d5710f1032715.AnchorOrientationBLocal.GetColumn(0));
			orientation.SetColumn(1, -c0fabe4906bd5c392e88d5710f1032715.AnchorOrientationBLocal.GetColumn(2));
			orientation.SetColumn(2, c0fabe4906bd5c392e88d5710f1032715.AnchorOrientationBLocal.GetColumn(1));
			return new Pose(cfc1538019fdd8122a84eb114fb0ecf14.AnchorPositionBLocal, orientation);
		}
		set
		{
			if (value != AnchorPoseBLocal)
			{
				cfc1538019fdd8122a84eb114fb0ecf14.AnchorPositionBLocal = value.Position;
				Matrix33F anchorOrientationBLocal = default;
				anchorOrientationBLocal.SetColumn(0, value.Orientation.GetColumn(0));
				anchorOrientationBLocal.SetColumn(1, value.Orientation.GetColumn(2));
				anchorOrientationBLocal.SetColumn(2, -value.Orientation.GetColumn(1));
				c0fabe4906bd5c392e88d5710f1032715.AnchorOrientationBLocal = anchorOrientationBLocal;
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
			return cfc1538019fdd8122a84eb114fb0ecf14.ErrorReduction.X;
		}
		set
		{
			if (value != ErrorReduction)
			{
				cfc1538019fdd8122a84eb114fb0ecf14.ErrorReduction = new Vector3F(value);
				c0fabe4906bd5c392e88d5710f1032715.ErrorReduction = new Vector3F(value);
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
				switch (4)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(UniversalJoint.set_Softness(float));
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
			return new Vector2F(c0fabe4906bd5c392e88d5710f1032715.Minimum.X, 0f - c0fabe4906bd5c392e88d5710f1032715.Maximum.Z);
		}
		set
		{
			if (!(value != Minimum))
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
					__ldtoken(UniversalJoint.set_Minimum(Vector2F));
				}
				c0fabe4906bd5c392e88d5710f1032715.Minimum = new Vector3F(value.X, 0f, c0fabe4906bd5c392e88d5710f1032715.Minimum.Z);
				c0fabe4906bd5c392e88d5710f1032715.Maximum = new Vector3F(c0fabe4906bd5c392e88d5710f1032715.Maximum.X, 0f, 0f - value.Y);
				OnChanged();
				return;
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
			return new Vector2F(c0fabe4906bd5c392e88d5710f1032715.Maximum.X, 0f - c0fabe4906bd5c392e88d5710f1032715.Minimum.Z);
		}
		set
		{
			if (value != Maximum)
			{
				c0fabe4906bd5c392e88d5710f1032715.Minimum = new Vector3F(c0fabe4906bd5c392e88d5710f1032715.Minimum.X, 0f, 0f - value.Y);
				c0fabe4906bd5c392e88d5710f1032715.Maximum = new Vector3F(value.X, 0f, c0fabe4906bd5c392e88d5710f1032715.Maximum.Z);
				OnChanged();
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
			if (value == Restitution)
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
					__ldtoken(UniversalJoint.set_Restitution(float));
				}
				c0fabe4906bd5c392e88d5710f1032715.Restitution = new Vector3F(value);
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
			if (value == Restitution)
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
					__ldtoken(UniversalJoint.set_MaxForce(float));
				}
				cfc1538019fdd8122a84eb114fb0ecf14.MaxForce = new Vector3F(value);
				c0fabe4906bd5c392e88d5710f1032715.MaxForce = new Vector3F(value);
				OnChanged();
				return;
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
	///       Initializes a new instance of the <see cref="T:DigitalRune.Physics.Constraints.UniversalJoint" /> class.
	///       </summary>
	public UniversalJoint()
	{
		cfc1538019fdd8122a84eb114fb0ecf14 = new LinearLimit
		{
			Minimum = new Vector3F(0f, 0f, 0f),
			Maximum = new Vector3F(0f, 0f, 0f)
		};
		c0fabe4906bd5c392e88d5710f1032715 = new AngularLimit
		{
			Minimum = new Vector3F(-(float)Math.PI / 4f, 0f, -(float)Math.PI / 4f),
			Maximum = new Vector3F((float)Math.PI / 4f, 0f, (float)Math.PI / 4f)
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
		int result;
		if (!c0fabe4906bd5c392e88d5710f1032715.ApplyImpulse())
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
				__ldtoken(UniversalJoint.OnApplyImpulse());
			}
			result = (flag ? 1 : 0);
		}
		else
		{
			result = 1;
		}
		return (byte)result != 0;
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
