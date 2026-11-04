namespace SynapseGaming.LightingSystem.Core;

/// <summary>
/// Defines the transparency mode when rendering materials and effects.
/// </summary>
public enum TransparencyMode
{
	/// <summary>
	/// No transparency - the material is solid.
	/// </summary>
	None,
	/// <summary>
	/// Clipped transparency - the material is only rendered where its diffuse
	/// map alpha value is greater than the reference value.
	/// </summary>
	Clip
}
