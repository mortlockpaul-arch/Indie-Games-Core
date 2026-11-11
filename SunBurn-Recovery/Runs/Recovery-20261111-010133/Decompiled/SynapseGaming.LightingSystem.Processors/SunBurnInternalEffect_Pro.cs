using System;
using Microsoft.Xna.Framework.Content;
using SynapseGaming.LightingSystem.Effects;

namespace SynapseGaming.LightingSystem.Processors;

public class SunBurnInternalEffect_Pro : ContentTypeReader<EffectData>
{
	protected override EffectData Read(ContentReader input, EffectData instance)
	{
		int count = input.ReadInt32();
		EffectData result = new EffectData(input.ReadBytes(count));
		if (input.ReadInt32() != 1234)
		{
			throw new Exception("Error loading asset.");
		}
		return result;
	}
}
