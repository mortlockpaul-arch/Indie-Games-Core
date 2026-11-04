using System;
using A;
using DigitalRune.Geometry;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Physics.Constraints;

/// <summary>
///       Defines a constraint that forces one body to move on a line that is fixed on the other body.
///       </summary>
public class PointOnLineConstraint : Constraint
{
	private readonly LinearLimit cfc1538019fdd8122a84eb114fb0ecf14;

	/// <summary>
	///       Gets or sets the line axis that is fixed on <see cref="P:DigitalRune.Physics.Constraints.Constraint.BodyA" /> in local space
	///       of <see cref="P:DigitalRune.Physics.Constraints.Constraint.BodyA" />.
	///       </summary>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="value" /> is a zero vector.
	///       </exception>
	public Vector3F AxisALocal
	{
		get
		{
			return cfc1538019fdd8122a84eb114fb0ecf14.AnchorPoseALocal.Orientation.GetColumn(0);
		}
		set
		{
			if (value != AxisALocal)
			{
				if (!value.TryNormalize())
				{
					throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(743));
				}
				Pose anchorPoseALocal = new Pose(AnchorPositionALocal);
				anchorPoseALocal.Orientation.SetColumn(0, value);
				anchorPoseALocal.Orientation.SetColumn(1, value.Orthonormal1);
				anchorPoseALocal.Orientation.SetColumn(2, value.Orthonormal2);
				cfc1538019fdd8122a84eb114fb0ecf14.AnchorPoseALocal = anchorPoseALocal;
				OnChanged();
			}
		}
	}

	/// <summary>
	///       Gets or sets the constraint anchor position on <see cref="P:DigitalRune.Physics.Constraints.Constraint.BodyA" /> in local space
	///       of <see cref="P:DigitalRune.Physics.Constraints.Constraint.BodyA" />.
	///       </summary>
	public Vector3F AnchorPositionALocal
	{
		get
		{
			return cfc1538019fdd8122a84eb114fb0ecf14.AnchorPoseALocal.Position;
		}
		set
		{
			if (value != AnchorPositionALocal)
			{
				cfc1538019fdd8122a84eb114fb0ecf14.AnchorPoseALocal = new Pose(value, cfc1538019fdd8122a84eb114fb0ecf14.AnchorPoseALocal.Orientation);
				OnChanged();
			}
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
			return cfc1538019fdd8122a84eb114fb0ecf14.AnchorPositionBLocal;
		}
		set
		{
			if (!(value != AnchorPositionBLocal))
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
					__ldtoken(PointOnLineConstraint.set_AnchorPositionBLocal(Vector3F));
				}
				cfc1538019fdd8122a84eb114fb0ecf14.AnchorPositionBLocal = value;
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
			if (value != ErrorReduction)
			{
				cfc1538019fdd8122a84eb114fb0ecf14.ErrorReduction = new Vector3F(value);
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
			if (value != Softness)
			{
				cfc1538019fdd8122a84eb114fb0ecf14.Softness = new Vector3F(value);
				OnChanged();
			}
		}
	}

	/// <summary>
	///       Gets or sets the minimum movement limit on the line.
	///       </summary>
	public float Minimum
	{
		get
		{
			return cfc1538019fdd8122a84eb114fb0ecf14.Minimum.X;
		}
		set
		{
			Vector3F minimum = cfc1538019fdd8122a84eb114fb0ecf14.Minimum;
			if (value == minimum.X)
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
					__ldtoken(PointOnLineConstraint.set_Minimum(float));
				}
				cfc1538019fdd8122a84eb114fb0ecf14.Minimum = new Vector3F(value, minimum.Y, minimum.Z);
				OnChanged();
				return;
			}
		}
	}

	/// <summary>
	///       Gets or sets the maximum movement limit on the line.
	///       </summary>
	public float Maximum
	{
		get
		{
			return cfc1538019fdd8122a84eb114fb0ecf14.Maximum.X;
		}
		set
		{
			Vector3F maximum = cfc1538019fdd8122a84eb114fb0ecf14.Maximum;
			if (value == maximum.X)
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
					__ldtoken(PointOnLineConstraint.set_Maximum(float));
				}
				cfc1538019fdd8122a84eb114fb0ecf14.Maximum = new Vector3F(value, maximum.Y, maximum.Z);
				OnChanged();
				return;
			}
		}
	}

	/// <summary>
	///       Gets or sets the coefficient of restitution for limits.
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
				switch (6)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(PointOnLineConstraint.set_Restitution(float));
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
					__ldtoken(PointOnLineConstraint.set_MaxForce(float));
				}
				cfc1538019fdd8122a84eb114fb0ecf14.MaxForce = new Vector3F(value);
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
	public override Vector3F AngularConstraintImpulse => Vector3F.Zero;

	/// <summary>
	///       Gets the position on the line axis relative to <see cref="P:DigitalRune.Physics.Constraints.PointOnLineConstraint.AnchorPositionALocal" />.
	///       </summary>
	public float RelativePosition => cfc1538019fdd8122a84eb114fb0ecf14.RelativePosition.X;

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Physics.Constraints.PointOnLineConstraint" /> class.
	///       </summary>
	public PointOnLineConstraint()
	{
		cfc1538019fdd8122a84eb114fb0ecf14 = new LinearLimit
		{
			Minimum = new Vector3F(float.NegativeInfinity, 0f, 0f),
			Maximum = new Vector3F(float.PositiveInfinity, 0f, 0f)
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
		base.OnAddToSimulation();
	}

	/// <summary>
	///       Called when this constraint is removed from a simulation.
	///       </summary>
	protected override void OnRemoveFromSimulation()
	{
		cfc1538019fdd8122a84eb114fb0ecf14.Simulation = null;
		base.OnRemoveFromSimulation();
	}

	/// <summary>
	///       Called when constraint should be set-up for a new time step.
	///       </summary>
	protected override void OnSetup()
	{
		cfc1538019fdd8122a84eb114fb0ecf14.Setup();
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
		return cfc1538019fdd8122a84eb114fb0ecf14.ApplyImpulse();
	}

	/// <summary>
	///       Called when properties of this constraint were changed.
	///       </summary>
	protected override void OnChanged()
	{
		cfc1538019fdd8122a84eb114fb0ecf14.BodyA = BodyA;
		cfc1538019fdd8122a84eb114fb0ecf14.BodyB = BodyB;
		base.OnChanged();
	}
}
