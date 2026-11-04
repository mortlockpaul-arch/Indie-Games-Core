using L;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Lights;
using SynapseGaming.LightingSystem.Rendering;
using SynapseGaming.LightingSystem.Shadows;

namespace SynapseGaming.LightingSystem.Core;

/// <summary>
/// Static class used to control SunBurn's built-in optimizations.
/// </summary>
public class OptimizationSystem
{
	internal static L.s<L.o, BaseLight> fNB = new L.s<L.o, BaseLight>();

	internal static L.s<L.c, IShadowSource> fNa = new L.s<L.c, IShadowSource>();

	internal static L.s<L.d, Effect> fNg = new L.s<L.d, Effect>();

	internal static L.s<L.w, SceneEntity> fNf = new L.s<L.w, SceneEntity>();

	/// <summary>
	/// Clears the internal cache of pre-cast data types. This may become necessary when
	/// creating and removing many new objects from the scenegraph without calling Clear().
	/// </summary>
	public static void Clear()
	{
		fNB._0013();
		fNa._0013();
		fNg._0013();
		fNf._0013();
	}
}
