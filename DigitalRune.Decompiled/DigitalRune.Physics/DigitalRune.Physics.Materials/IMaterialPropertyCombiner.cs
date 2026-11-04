namespace DigitalRune.Physics.Materials;

/// <summary>
///       Computes the material properties for two materials in contact.
///       </summary>
public interface IMaterialPropertyCombiner
{
	/// <summary>
	///       Computes the combined friction coefficient.
	///       </summary>
	/// <param name="frictionA">The first friction coefficient.</param>
	/// <param name="frictionB">The second friction coefficient.</param>
	/// <returns>
	///       The combined friction coefficient.
	///       </returns>
	float CombineFriction(float frictionA, float frictionB);

	/// <summary>
	///       Computes the combined coefficient of restitution.
	///       </summary>
	/// <param name="restitutionA">The first coefficient of restitution.</param>
	/// <param name="restitutionB">The second coefficient of restitution.</param>
	/// <returns>
	///       The combined coefficient of restitution.
	///       </returns>
	float CombineRestitution(float restitutionA, float restitutionB);
}
