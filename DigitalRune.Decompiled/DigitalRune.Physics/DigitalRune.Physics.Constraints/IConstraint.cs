using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Physics.Constraints;

/// <summary>
///       Defines a constraint between two bodies.
///       </summary>
public interface IConstraint
{
	/// <summary>
	///       Gets the first body.
	///       </summary>
	RigidBody BodyA { get; }

	/// <summary>
	///       Gets the second body.
	///       </summary>
	RigidBody BodyB { get; }

	/// <summary>
	///       Gets a value indicating whether this constraint is enabled.
	///       </summary>
	bool Enabled { get; }

	/// <summary>
	///       Gets a value indicating whether collisions between <see cref="P:DigitalRune.Physics.Constraints.IConstraint.BodyA" /> and 
	///       <see cref="P:DigitalRune.Physics.Constraints.IConstraint.BodyB" /> are disabled.
	///       </summary>
	bool CollisionEnabled { get; }

	/// <summary>
	///       Gets the simulation to which this constraint belongs.
	///       </summary>
	Simulation Simulation { get; }

	/// <summary>
	///       Gets or sets the linear constraint impulse that was applied. 
	///       </summary>
	Vector3F LinearConstraintImpulse { get; }

	/// <summary>
	///       Gets or sets the angular constraint impulse that was applied.
	///       </summary>
	Vector3F AngularConstraintImpulse { get; }

	/// <summary>
	///       Called by the simulation to prepare this constraint for constraint solving for a new
	///       time step.
	///       </summary>
	void Setup();

	/// <summary>
	///       Called by the simulation to apply an impulse that satisfies the constraint.
	///       </summary>
	/// <returns>
	///   <see langword="true" /> if a constraint larger than 
	///       <see cref="P:DigitalRune.Physics.Settings.ConstraintSettings.MinConstraintImpulse" /> was applied.
	///       </returns>
	bool ApplyImpulse();
}
