namespace SynapseGaming.LightingSystem.Effects;

/// <summary>
/// Interface that provides basic material properties to XNA built-in effects.
/// </summary>
public interface IExtendedXNAEffect
{
	/// <summary>
	/// Surfaces rendered with the effect should be visible from both sides.
	/// </summary>
	bool DoubleSided { get; set; }
}
