namespace DigitalRune.Physics.ForceEffects;

/// <summary>
///       Defines the area of effect of a <see cref="T:DigitalRune.Physics.ForceEffects.ForceField" />. Only rigid bodies in the area of
///       effect are subject to forces.
///       </summary>
public interface IAreaOfEffect
{
	/// <summary>
	///       Calls <see cref="M:DigitalRune.Physics.ForceEffects.ForceField.Apply(DigitalRune.Physics.RigidBody)" /> of the given force field for all objects
	///       in the area of effect.
	///       </summary>
	/// <param name="forceField">The force field.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="forceField" /> is <see langword="null" />.
	///       </exception>
	void Apply(ForceField forceField);
}
