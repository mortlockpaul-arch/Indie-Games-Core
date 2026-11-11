using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Effects;
using SynapseGaming.LightingSystem.Effects.Forward;
using _id_6;

namespace SynapseGaming.LightingSystem.Processors.Forward;

public class SasMaterialReader_Pro : ContentTypeReader<SasEffect>
{
	private GraphicsDevice l8a;

	private EffectData l8_u0013_;

	private Effect _u0013__u0004_()
	{
		return SasEffect.l5(l8a, l8_u0013_.ByteCode, param_2: true);
	}

	protected override SasEffect Read(ContentReader input, SasEffect instance)
	{
		l8a = (input.ContentManager.ServiceProvider.GetService(typeof(IGraphicsDeviceService)) as IGraphicsDeviceService).GraphicsDevice;
		l8_u0013_ = input.ReadObject<EffectData>();
		string param_ = input.ReadString();
		string param_2 = input.ReadString();
		SasEffect sasEffect = ResourceManager._u0013__u0004_(param_2, _u0013__u0004_) as SasEffect;
		sasEffect.MaterialName = param_;
		sasEffect.MaterialFile = param_2;
		sasEffect.ProjectFile = input.ReadString();
		sasEffect.Skinned = input.ReadBoolean();
		sasEffect.Elasticity = input.ReadSingle();
		sasEffect.Friction = input.ReadSingle();
		sasEffect.EffectFile = input.ReadString();
		Dictionary<string, object> param_3 = input.ReadObject<Dictionary<string, object>>();
		Dictionary<string, Texture> param_4 = input.ReadObject<Dictionary<string, Texture>>();
		sasEffect.NO(param_4);
		sasEffect.N_u000F_(param_3);
		sasEffect.N_u0012_();
		_id_6.a.L_u0005_(input);
		if (input.ReadInt32() != 1234)
		{
			throw new Exception("Error loading asset.");
		}
		return sasEffect;
	}
}
