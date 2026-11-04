using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Effects;
using SynapseGaming.LightingSystem.Effects.Deferred;
using m;

namespace SynapseGaming.LightingSystem.Processors.Deferred;

/// <summary />
public class DeferredSasMaterialReader_Pro : ContentTypeReader<DeferredSasEffect>
{
	private GraphicsDevice fNB;

	private EffectData fNa;

	private Effect g_0003()
	{
		return new DeferredSasEffect(fNB, fNa.ByteCode, true);
	}

	/// <summary />
	protected override DeferredSasEffect Read(ContentReader input, DeferredSasEffect instance)
	{
		fNB = (input.ContentManager.ServiceProvider.GetService(typeof(IGraphicsDeviceService)) as IGraphicsDeviceService).GraphicsDevice;
		fNa = input.ReadObject<EffectData>();
		string text = input.ReadString();
		string text2 = input.ReadString();
		DeferredSasEffect deferredSasEffect = ResourceManager.g_0003(text2, g_0003) as DeferredSasEffect;
		deferredSasEffect.MaterialName = text;
		deferredSasEffect.MaterialFile = text2;
		deferredSasEffect.ProjectFile = input.ReadString();
		deferredSasEffect.Skinned = input.ReadBoolean();
		deferredSasEffect.Elasticity = input.ReadSingle();
		deferredSasEffect.Friction = input.ReadSingle();
		deferredSasEffect.EffectFile = input.ReadString();
		Dictionary<string, object> dictionary = input.ReadObject<Dictionary<string, object>>();
		Dictionary<string, Texture> dictionary2 = input.ReadObject<Dictionary<string, Texture>>();
		foreach (KeyValuePair<string, Texture> item in dictionary2)
		{
			deferredSasEffect.SetTexture(item.Key, item.Value);
		}
		deferredSasEffect.g_000E(dictionary);
		deferredSasEffect.gS();
		m.B._0003F(input);
		if (input.ReadInt32() != 1234)
		{
			throw new Exception("Error loading asset.");
		}
		return deferredSasEffect;
	}
}
