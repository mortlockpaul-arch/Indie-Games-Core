using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Lights;
using _id_6;

namespace SynapseGaming.LightingSystem.Processors;

public class LightMapReader_Pro : ContentTypeReader<LightMap>
{
	protected override LightMap Read(ContentReader input, LightMap instance)
	{
		Texture2D colortexture = input.ReadObject<Texture2D>();
		Texture2D directionaltexture = input.ReadObject<Texture2D>();
		_id_6.a.L_u0005_(input);
		if (input.ReadInt32() != 1234)
		{
			throw new Exception("Error loading asset.");
		}
		return new LightMap(colortexture, directionaltexture);
	}
}
