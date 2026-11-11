using System.Runtime.CompilerServices;
using B;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Collision;
using SynapseGaming.LightingSystem.Core;

namespace SynapseGaming.LightingSystem.Effects;

public class SunBurnAlphaTestEffect : AlphaTestEffect, IExtendedXNAEffect, ICollisionMaterial
{
	private int l8a;

	private float l8_u0013_;

	private float l8N;

	[CompilerGenerated]
	private bool l8l;

	[CompilerGenerated]
	private TransparencyMode l8L;

	public bool DoubleSided
	{
		[CompilerGenerated]
		get
		{
			return l8l;
		}
		[CompilerGenerated]
		set
		{
			l8l = value;
		}
	}

	public TransparencyMode TransparencyMode
	{
		[CompilerGenerated]
		get
		{
			return l8L;
		}
		[CompilerGenerated]
		set
		{
			l8L = value;
		}
	}

	public float Elasticity
	{
		get
		{
			return l8_u0013_;
		}
		set
		{
			l8_u0013_ = value;
			l8a++;
		}
	}

	public float Friction
	{
		get
		{
			return l8N;
		}
		set
		{
			l8N = value;
			l8a++;
		}
	}

	public int CollisionId => l8a;

	public SunBurnAlphaTestEffect(GraphicsDevice device)
		: base(device)
	{
	}

	public override Effect Clone()
	{
		Effect effect = new SunBurnAlphaTestEffect(GraphicsDevice);
		B.L._u0013_B(this, effect);
		return effect;
	}
}
