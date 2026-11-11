using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Effects;
using b;

namespace I;

internal class a : BaseRenderableEffect, b._u0013_
{
	private Texture2D l8a;

	private EffectParameter l8_u0013_;

	[CompilerGenerated]
	private bool l8N;

	public Texture2D SceneDepthMap
	{
		get
		{
			return l8a;
		}
		set
		{
			EffectHelper.lh(value, ref l8a, ref l8_u0013_);
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

	public override bool DoubleSided
	{
		[CompilerGenerated]
		get
		{
			return l8N;
		}
		[CompilerGenerated]
		set
		{
			l8N = value;
		}
	}

	protected override void SetTechnique()
	{
	}

	public a(GraphicsDevice graphicsdevice)
		: base(graphicsdevice, "DeferredDepthEffect")
	{
		l8_u0013_ = Parameters["_SceneDepthMap"];
	}

	protected override Effect Create()
	{
		return new a(GraphicsDevice);
	}
}
