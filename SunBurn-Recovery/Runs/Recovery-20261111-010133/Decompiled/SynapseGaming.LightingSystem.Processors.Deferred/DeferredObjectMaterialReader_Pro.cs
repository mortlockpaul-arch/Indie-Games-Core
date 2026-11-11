using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Core;
using SynapseGaming.LightingSystem.Effects.Deferred;
using _id_6;

namespace SynapseGaming.LightingSystem.Processors.Deferred;

public class DeferredObjectMaterialReader_Pro : ContentTypeReader<DeferredObjectEffect>
{
	private GraphicsDevice l8a;

	private Effect _u0013__u0004_()
	{
		return new DeferredObjectEffect(l8a);
	}

	protected override DeferredObjectEffect Read(ContentReader input, DeferredObjectEffect instance)
	{
		l8a = (input.ContentManager.ServiceProvider.GetService(typeof(IGraphicsDeviceService)) as IGraphicsDeviceService).GraphicsDevice;
		string param_ = input.ReadString();
		string param_2 = input.ReadString();
		DeferredObjectEffect deferredObjectEffect = ResourceManager._u0013__u0004_(param_2, _u0013__u0004_) as DeferredObjectEffect;
		deferredObjectEffect.MaterialName = param_;
		deferredObjectEffect.MaterialFile = param_2;
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
		deferredObjectEffect.ParallaxMapTexture = CoreHelper._u0013_a(l8a, input.ReadExternalReference<Texture2D>());
		deferredObjectEffect.Skinned = input.ReadBoolean();
		deferredObjectEffect.DoubleSided = input.ReadBoolean();
		TransparencyMode mode = (TransparencyMode)input.ReadInt32();
		float threshold = input.ReadSingle();
		deferredObjectEffect.SetTransparencyModeAndMap(mode, threshold, deferredObjectEffect.DiffuseMapTexture);
		deferredObjectEffect.SpecularPower = input.ReadSingle();
		deferredObjectEffect.SpecularAmount = input.ReadSingle();
		deferredObjectEffect.FresnelReflectBias = input.ReadSingle();
		deferredObjectEffect.FresnelReflectOffset = input.ReadSingle();
		deferredObjectEffect.FresnelMicrofacetDistribution = input.ReadSingle();
		deferredObjectEffect.ParallaxScale = input.ReadSingle();
		deferredObjectEffect.ParallaxOffset = input.ReadSingle();
		Vector4 vector = input.ReadVector4();
		deferredObjectEffect.DiffuseColor = new Vector3(vector.X, vector.Y, vector.Z);
		deferredObjectEffect.TransparencyAmount = vector.W;
		vector = input.ReadVector4();
		deferredObjectEffect.EmissiveColor = new Vector3(vector.X, vector.Y, vector.Z);
		deferredObjectEffect.Elasticity = input.ReadSingle();
		deferredObjectEffect.Friction = input.ReadSingle();
		deferredObjectEffect.AddressModeU = (TextureAddressMode)input.ReadInt32();
		deferredObjectEffect.AddressModeV = (TextureAddressMode)input.ReadInt32();
		deferredObjectEffect.AddressModeW = (TextureAddressMode)input.ReadInt32();
		_id_6.a.L_u0005_(input);
		if (input.ReadInt32() != 1234)
		{
			throw new Exception("Error loading asset.");
		}
		return deferredObjectEffect;
	}
}
