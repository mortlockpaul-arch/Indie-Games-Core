using System;
using L;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Effects.Deferred;
using m;

namespace SynapseGaming.LightingSystem.Processors.Deferred;

/// <summary />
public class DeferredObjectMaterialReader_Pro : ContentTypeReader<DeferredObjectEffect>
{
	private GraphicsDevice fNB;

	private Effect g_0003()
	{
		return new DeferredObjectEffect(fNB);
	}

	/// <summary />
	protected override DeferredObjectEffect Read(ContentReader input, DeferredObjectEffect instance)
	{
		fNB = (input.ContentManager.ServiceProvider.GetService(typeof(IGraphicsDeviceService)) as IGraphicsDeviceService).GraphicsDevice;
		string text = input.ReadString();
		string text2 = input.ReadString();
		DeferredObjectEffect deferredObjectEffect = ResourceManager.g_0003(text2, g_0003) as DeferredObjectEffect;
		deferredObjectEffect.MaterialName = text;
		deferredObjectEffect.MaterialFile = text2;
		deferredObjectEffect.ProjectFile = input.ReadString();
		deferredObjectEffect.DiffuseMapFile = input.ReadString();
		deferredObjectEffect.DiffuseMapTexture = input.ReadExternalReference<Texture2D>();
		deferredObjectEffect.EmissiveMapFile = input.ReadString();
		deferredObjectEffect.EmissiveMapTexture = input.ReadExternalReference<Texture2D>();
		deferredObjectEffect.NormalMapFile = input.ReadString();
		deferredObjectEffect.NormalMapTexture = input.ReadExternalReference<Texture2D>();
		deferredObjectEffect.SpecularColorMapFile = input.ReadString();
		deferredObjectEffect.SpecularColorMapTexture = input.ReadExternalReference<Texture2D>();
		deferredObjectEffect.ParallaxMapFile = input.ReadString();
		deferredObjectEffect.ParallaxMapTexture = L._0019.a3(fNB, input.ReadExternalReference<Texture2D>());
		deferredObjectEffect.Skinned = input.ReadBoolean();
		deferredObjectEffect.DoubleSided = input.ReadBoolean();
		TransparencyMode mode = (TransparencyMode)input.ReadInt32();
		float transparency = input.ReadSingle();
		deferredObjectEffect.SetTransparencyModeAndMap(mode, transparency, deferredObjectEffect.DiffuseMapTexture);
		deferredObjectEffect.SpecularPower = input.ReadSingle();
		deferredObjectEffect.SpecularAmount = input.ReadSingle();
		deferredObjectEffect.FresnelReflectBias = input.ReadSingle();
		deferredObjectEffect.FresnelReflectOffset = input.ReadSingle();
		deferredObjectEffect.FresnelMicrofacetDistribution = input.ReadSingle();
		deferredObjectEffect.ParallaxScale = input.ReadSingle();
		deferredObjectEffect.ParallaxOffset = input.ReadSingle();
		Vector4 vector = input.ReadVector4();
		deferredObjectEffect.DiffuseColor = new Vector3(vector.X, vector.Y, vector.Z);
		vector = input.ReadVector4();
		deferredObjectEffect.EmissiveColor = new Vector3(vector.X, vector.Y, vector.Z);
		deferredObjectEffect.Elasticity = input.ReadSingle();
		deferredObjectEffect.Friction = input.ReadSingle();
		deferredObjectEffect.AddressModeU = (TextureAddressMode)input.ReadInt32();
		deferredObjectEffect.AddressModeV = (TextureAddressMode)input.ReadInt32();
		deferredObjectEffect.AddressModeW = (TextureAddressMode)input.ReadInt32();
		m.B._0003F(input);
		if (input.ReadInt32() != 1234)
		{
			throw new Exception("Error loading asset.");
		}
		return deferredObjectEffect;
	}
}
