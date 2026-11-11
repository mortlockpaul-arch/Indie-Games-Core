using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Effects.Forward;
using _id_6;

namespace SynapseGaming.LightingSystem.Processors.Forward;

public class LightingMaterialReader_Pro : ContentTypeReader<LightingEffect>
{
	private GraphicsDevice l8a;

	private Effect _u0013__u0004_()
	{
		return new LightingEffect(l8a);
	}

	protected override LightingEffect Read(ContentReader input, LightingEffect instance)
	{
		l8a = (input.ContentManager.ServiceProvider.GetService(typeof(IGraphicsDeviceService)) as IGraphicsDeviceService).GraphicsDevice;
		string param_ = input.ReadString();
		string param_2 = input.ReadString();
		LightingEffect lightingEffect = ResourceManager._u0013__u0004_(param_2, _u0013__u0004_) as LightingEffect;
		lightingEffect.MaterialName = param_;
		lightingEffect.MaterialFile = param_2;
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
		lightingEffect.ParallaxMapTexture = CoreHelper._u0013_a(l8a, input.ReadExternalReference<Texture2D>());
		lightingEffect.Skinned = input.ReadBoolean();
		lightingEffect.DoubleSided = input.ReadBoolean();
		TransparencyMode mode = (TransparencyMode)input.ReadInt32();
		float threshold = input.ReadSingle();
		lightingEffect.SetTransparencyModeAndMap(mode, threshold, lightingEffect.DiffuseMapTexture);
		lightingEffect.SpecularPower = input.ReadSingle();
		lightingEffect.SpecularAmount = input.ReadSingle();
		lightingEffect.FresnelReflectBias = input.ReadSingle();
		lightingEffect.FresnelReflectOffset = input.ReadSingle();
		lightingEffect.FresnelMicrofacetDistribution = input.ReadSingle();
		lightingEffect.ParallaxScale = input.ReadSingle();
		lightingEffect.ParallaxOffset = input.ReadSingle();
		Vector4 vector = input.ReadVector4();
		lightingEffect.DiffuseColor = new Vector3(vector.X, vector.Y, vector.Z);
		lightingEffect.TransparencyAmount = vector.W;
		vector = input.ReadVector4();
		lightingEffect.EmissiveColor = new Vector3(vector.X, vector.Y, vector.Z);
		lightingEffect.Elasticity = input.ReadSingle();
		lightingEffect.Friction = input.ReadSingle();
		lightingEffect.AddressModeU = (TextureAddressMode)input.ReadInt32();
		lightingEffect.AddressModeV = (TextureAddressMode)input.ReadInt32();
		lightingEffect.AddressModeW = (TextureAddressMode)input.ReadInt32();
		_id_6.a.L_u0005_(input);
		if (input.ReadInt32() != 1234)
		{
			throw new Exception("Error loading asset.");
		}
		return lightingEffect;
	}
}
