using System;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Effects.Deferred;

namespace SynapseGaming.LightingSystem.Shadows.Deferred;

public class DeferredShadowCubeMap : BaseShadowCubeMap
{
	protected override Type GetEffectType()
	{
		return typeof(DeferredObjectEffect);
	}

	protected override Effect CreateEffect()
	{
		return new DeferredObjectEffect(Device);
	}
}
