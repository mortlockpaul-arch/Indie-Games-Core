using System;
using Microsoft.Xna.Framework.Content;
using SynapseGaming.LightingSystem.Lights;
using _id_6;

namespace SynapseGaming.LightingSystem.Processors;

public class LightOcclusionBufferReader_Pro : ContentTypeReader<LightOcclusionBuffer>
{
	protected override LightOcclusionBuffer Read(ContentReader input, LightOcclusionBuffer instance)
	{
		LightOcclusionBuffer lightOcclusionBuffer = new LightOcclusionBuffer();
		input.ReadInt32();
		lightOcclusionBuffer._u0013__u0014_(input);
		_id_6.a.L_u0005_(input);
		if (input.ReadInt32() != 1234)
		{
			throw new Exception("Error loading asset.");
		}
		return lightOcclusionBuffer;
	}
}
