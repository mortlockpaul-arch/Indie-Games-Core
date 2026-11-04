using System;
using L;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Effects.Forward;
using m;

namespace SynapseGaming.LightingSystem.Processors.Forward;

/// <summary />
public class LightingMaterialReader_Pro : ContentTypeReader<LightingEffect>
{
	private GraphicsDevice fNB;

	private Effect g_0003()
	{
		return new LightingEffect(fNB);
	}

	/// <summary />
	protected override LightingEffect Read(ContentReader input, LightingEffect instance)
	{
		fNB = (input.ContentManager.ServiceProvider.GetService(typeof(IGraphicsDeviceService)) as IGraphicsDeviceService).GraphicsDevice;
		string text = input.ReadString();
		string text2 = input.ReadString();
		LightingEffect lightingEffect = ResourceManager.g_0003(text2, g_0003) as LightingEffect;
		lightingEffect.MaterialName = text;
		lightingEffect.MaterialFile = text2;
		lightingEffect.ProjectFile = input.ReadString();
		lightingEffect.DiffuseMapFile = input.ReadString();
		lightingEffect.DiffuseMapTexture = input.ReadExternalReference<Texture2D>();
		lightingEffect.EmissiveMapFile = input.ReadString();
		lightingEffect.EmissiveMapTexture = input.ReadExternalReference<Texture2D>();
		lightingEffect.NormalMapFile = input.ReadString();
		lightingEffect.NormalMapTexture = input.ReadExternalReference<Texture2D>();
		lightingEffect.SpecularColorMapFile = input.ReadString();
		lightingEffect.SpecularColorMapTexture = input.ReadExternalReference<Texture2D>();
		lightingEffect.ParallaxMapFile = input.ReadString();
		lightingEffect.ParallaxMapTexture = L._0019.a3(fNB, input.ReadExternalReference<Texture2D>());
		lightingEffect.Skinned = input.ReadBoolean();
		lightingEffect.DoubleSided = input.ReadBoolean();
		TransparencyMode mode = (TransparencyMode)input.ReadInt32();
		float transparency = input.ReadSingle();
		lightingEffect.SetTransparencyModeAndMap(mode, transparency, lightingEffect.DiffuseMapTexture);
		lightingEffect.SpecularPower = input.ReadSingle();
		lightingEffect.SpecularAmount = input.ReadSingle();
		lightingEffect.FresnelReflectBias = input.ReadSingle();
		lightingEffect.FresnelReflectOffset = input.ReadSingle();
		lightingEffect.FresnelMicrofacetDistribution = input.ReadSingle();
		lightingEffect.ParallaxScale = input.ReadSingle();
		lightingEffect.ParallaxOffset = input.ReadSingle();
		Vector4 vector = input.ReadVector4();
		lightingEffect.DiffuseColor = new Vector3(vector.X, vector.Y, vector.Z);
		vector = input.ReadVector4();
		lightingEffect.EmissiveColor = new Vector3(vector.X, vector.Y, vector.Z);
		lightingEffect.Elasticity = input.ReadSingle();
		lightingEffect.Friction = input.ReadSingle();
		lightingEffect.AddressModeU = (TextureAddressMode)input.ReadInt32();
		lightingEffect.AddressModeV = (TextureAddressMode)input.ReadInt32();
		lightingEffect.AddressModeW = (TextureAddressMode)input.ReadInt32();
		m.B._0003F(input);
		if (input.ReadInt32() != 1234)
		{
			throw new Exception("Error loading asset.");
		}
		return lightingEffect;
	}
}
