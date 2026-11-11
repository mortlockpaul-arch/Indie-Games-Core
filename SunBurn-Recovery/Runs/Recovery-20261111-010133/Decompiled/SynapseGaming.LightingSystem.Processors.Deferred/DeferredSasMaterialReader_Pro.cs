using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Effects;
using SynapseGaming.LightingSystem.Effects.Deferred;
using _id_6;

namespace SynapseGaming.LightingSystem.Processors.Deferred;

public class DeferredSasMaterialReader_Pro : ContentTypeReader<DeferredSasEffect>
{
	private GraphicsDevice l8a;

	private EffectData l8_u0013_;

	private Effect _u0013__u0004_()
	{
		return new DeferredSasEffect(l8a, l8_u0013_.ByteCode, param_2: true);
	}

	protected override DeferredSasEffect Read(ContentReader input, DeferredSasEffect instance)
	{
		l8a = (input.ContentManager.ServiceProvider.GetService(typeof(IGraphicsDeviceService)) as IGraphicsDeviceService).GraphicsDevice;
		l8_u0013_ = input.ReadObject<EffectData>();
		string param_ = input.ReadString();
		string param_2 = input.ReadString();
		DeferredSasEffect deferredSasEffect = ResourceManager._u0013__u0004_(param_2, _u0013__u0004_) as DeferredSasEffect;
		deferredSasEffect.MaterialName = param_;
		deferredSasEffect.MaterialFile = param_2;
		deferredSasEffect.ProjectFile = input.ReadString();
		deferredSasEffect.Skinned = input.ReadBoolean();
		deferredSasEffect.Elasticity = input.ReadSingle();
		deferredSasEffect.Friction = input.ReadSingle();
		deferredSasEffect.EffectFile = input.ReadString();
		Dictionary<string, object> param_3 = input.ReadObject<Dictionary<string, object>>();
		Dictionary<string, Texture> dictionary = input.ReadObject<Dictionary<string, Texture>>();
		foreach (KeyValuePair<string, Texture> item in dictionary)
		{
			deferredSasEffect.SetTexture(item.Key, item.Value);
		}
		deferredSasEffect.N_u000F_(param_3);
		deferredSasEffect.N_u0012_();
		_id_6.a.L_u0005_(input);
		if (input.ReadInt32() != 1234)
		{
			throw new Exception("Error loading asset.");
		}
		return deferredSasEffect;
	}
}
