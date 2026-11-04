using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Effects;
using j;

namespace o;

internal class B : BaseRenderableEffect, j.a
{
	private Texture2D fNB;

	private EffectParameter fNa;

	public Texture2D SceneDepthMap
	{
		get
		{
			return fNB;
		}
		set
		{
			EffectHelper.fO(value, ref fNB, ref fNa);
		}
	}

	public Texture2D SceneNormalSpecularMap
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	protected override void SetTechnique()
	{
	}

	public B(GraphicsDevice graphicsdevice)
		: base(graphicsdevice, "DeferredDepthEffect")
	{
		fNa = Parameters["_SceneDepthMap"];
	}

	protected override Effect Create()
	{
		return new B(GraphicsDevice);
	}
}
