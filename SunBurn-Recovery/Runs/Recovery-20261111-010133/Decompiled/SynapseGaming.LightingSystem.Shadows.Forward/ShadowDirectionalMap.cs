using System;
using Microsoft.Xna.Framework.Graphics;
using _id_5;

namespace SynapseGaming.LightingSystem.Shadows.Forward;

public class ShadowDirectionalMap : BaseShadowDirectionalMap
{
	protected override Type GetEffectType()
	{
		return typeof(_id_5.a);
	}

	protected override Effect CreateEffect()
	{
		return new _id_5.a(Device);
	}
}
