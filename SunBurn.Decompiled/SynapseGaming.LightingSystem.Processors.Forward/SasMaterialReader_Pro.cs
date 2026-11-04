using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Effects;
using SynapseGaming.LightingSystem.Effects.Forward;
using m;

namespace SynapseGaming.LightingSystem.Processors.Forward;

/// <summary />
public class SasMaterialReader_Pro : ContentTypeReader<SasEffect>
{
	private GraphicsDevice fNB;

	private EffectData fNa;

	private Effect g_0003()
	{
		return SasEffect.fA(fNB, fNa.ByteCode, true);
	}

	/// <summary />
	protected override SasEffect Read(ContentReader input, SasEffect instance)
	{
		fNB = (input.ContentManager.ServiceProvider.GetService(typeof(IGraphicsDeviceService)) as IGraphicsDeviceService).GraphicsDevice;
		fNa = input.ReadObject<EffectData>();
		string text = input.ReadString();
		string text2 = input.ReadString();
		SasEffect sasEffect = ResourceManager.g_0003(text2, g_0003) as SasEffect;
		sasEffect.MaterialName = text;
		sasEffect.MaterialFile = text2;
		sasEffect.ProjectFile = input.ReadString();
		sasEffect.Skinned = input.ReadBoolean();
		sasEffect.Elasticity = input.ReadSingle();
		sasEffect.Friction = input.ReadSingle();
		sasEffect.EffectFile = input.ReadString();
		Dictionary<string, object> dictionary = input.ReadObject<Dictionary<string, object>>();
		Dictionary<string, Texture> dictionary2 = input.ReadObject<Dictionary<string, Texture>>();
		sasEffect.gI(dictionary2);
		sasEffect.g_000E(dictionary);
		sasEffect.gS();
		m.B._0003F(input);
		if (input.ReadInt32() != 1234)
		{
			throw new Exception("Error loading asset.");
		}
		return sasEffect;
	}
}
