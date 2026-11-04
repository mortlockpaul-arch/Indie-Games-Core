using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;

namespace SynapseGaming.LightingSystem.Effects;

/// <summary>
/// Interface that provides custom effects with transparency support.
/// </summary>
public interface ITransparentEffect
{
	/// <summary>
	/// The transparency style used when rendering the effect.
	/// </summary>
	TransparencyMode TransparencyMode { get; }

	/// <summary>
	/// Used with TransparencyMode to determine the effect transparency.
	///   -For Clipped mode this value is a comparison value, where all TransparencyMap
	///    alpha values below this value are *not* rendered.
	/// </summary>
	float Transparency { get; }

	/// <summary>
	/// The texture map used for transparency (values are pulled from the alpha channel).
	/// </summary>
	Texture TransparencyMap { get; }

	/// <summary>
	/// Sets all transparency information at once.  Used to improve performance
	/// by avoiding multiple effect technique changes.
	/// </summary>
	/// <param name="mode">The transparency style used when rendering the effect.</param>
	/// <param name="transparency">Used with TransparencyMode to determine the effect transparency.
	///   -For Clipped mode this value is a comparison value, where all TransparencyMap
	///    alpha values below this value are *not* rendered.</param>
	/// <param name="map">The texture map used for transparency (values are pulled from the alpha channel).</param>
	void SetTransparencyModeAndMap(TransparencyMode mode, float transparency, Texture map);
}
