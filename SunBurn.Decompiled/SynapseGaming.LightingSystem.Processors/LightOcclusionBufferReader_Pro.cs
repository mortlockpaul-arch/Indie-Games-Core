using System;
using Microsoft.Xna.Framework.Content;
using SynapseGaming.LightingSystem.Lights;
using m;

namespace SynapseGaming.LightingSystem.Processors;

/// <summary />
public class LightOcclusionBufferReader_Pro : ContentTypeReader<LightOcclusionBuffer>
{
	/// <summary />
	protected override LightOcclusionBuffer Read(ContentReader input, LightOcclusionBuffer instance)
	{
		LightOcclusionBuffer lightOcclusionBuffer = new LightOcclusionBuffer();
		input.ReadInt32();
		lightOcclusionBuffer.fC(input);
		m.B._0003F(input);
		if (input.ReadInt32() != 1234)
		{
			throw new Exception("Error loading asset.");
		}
		return lightOcclusionBuffer;
	}
}
