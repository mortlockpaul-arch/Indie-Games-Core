using System;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Effects.Deferred;

namespace SynapseGaming.LightingSystem.Shadows.Deferred;

/// <summary>
/// Shadow map class that implements cascading level-of-detail
/// directional shadows. Used for directional lights.
/// </summary>
public class DeferredShadowDirectionalMap : BaseShadowDirectionalMap
{
	/// <summary>
	/// Gets the effect type that performs rendering specific to the shadow
	/// mapping implementation used by this object.
	/// </summary>
	/// <returns></returns>
	protected override Type GetEffectType()
	{
		return typeof(DeferredObjectEffect);
	}

	/// <summary>
	/// Creates a new effect that performs rendering specific to the shadow
	/// mapping implementation used by this object.
	/// </summary>
	/// <returns></returns>
	protected override Effect CreateEffect()
	{
		return new DeferredObjectEffect(Device);
	}
}
