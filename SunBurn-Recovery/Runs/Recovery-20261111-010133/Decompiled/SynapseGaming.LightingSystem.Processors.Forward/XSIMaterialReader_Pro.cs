using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Effects;
using SynapseGaming.LightingSystem.Effects.Forward;
using _id_6;

namespace SynapseGaming.LightingSystem.Processors.Forward;

public class XSIMaterialReader_Pro : ContentTypeReader<XSIEffect>
{
	protected override XSIEffect Read(ContentReader input, XSIEffect instance)
	{
		IGraphicsDeviceService graphicsDeviceService = (IGraphicsDeviceService)input.ContentManager.ServiceProvider.GetService(typeof(IGraphicsDeviceService));
		bool skinned = input.ReadBoolean();
		EffectData effectData = input.ReadObject<EffectData>();
		XSIEffect xSIEffect = new XSIEffect(graphicsDeviceService.GraphicsDevice, effectData.ByteCode);
		xSIEffect.Skinned = skinned;
		_id_6.a.L_u0005_(input);
		return xSIEffect;
	}
}
