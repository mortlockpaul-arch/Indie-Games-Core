using System.Runtime.CompilerServices;
using L;
using Microsoft.Xna.Framework.Graphics;
using SynapseGaming.LightingSystem.Collision;

namespace SynapseGaming.LightingSystem.Effects;

/// <summary>
/// Used to fix issues with XNA 4.0 effects.
/// </summary>
public class SunBurnSkinnedEffect : SkinnedEffect, ICollisionMaterial, IExtendedXNAEffect
{
	[CompilerGenerated]
	private bool fNB;

	[CompilerGenerated]
	private float fNa;

	[CompilerGenerated]
	private float fNg;

	/// <summary>
	/// Surfaces rendered with the effect should be visible from both sides.
	/// </summary>
	public bool DoubleSided
	{
		[CompilerGenerated]
		get
		{
			return fNB;
		}
		[CompilerGenerated]
		set
		{
			fNB = value;
		}
	}

	/// <summary>
	/// Amount material absorbs impact force.
	/// </summary>
	public float Elasticity
	{
		[CompilerGenerated]
		get
		{
			return fNa;
		}
		[CompilerGenerated]
		set
		{
			fNa = value;
		}
	}

	/// <summary>
	/// Amount material resists objects moving across its surface.
	/// </summary>
	public float Friction
	{
		[CompilerGenerated]
		get
		{
			return fNg;
		}
		[CompilerGenerated]
		set
		{
			fNg = value;
		}
	}

	/// <summary>
	/// Creates a SunBurnSkinnedEffect instance.
	/// </summary>
	public SunBurnSkinnedEffect(GraphicsDevice device)
		: base(device)
	{
	}

	/// <summary>
	/// Creates a clone of the current effect instance.
	/// </summary>
	public override Effect Clone()
	{
		Effect effect = new SunBurnSkinnedEffect(GraphicsDevice);
		L.n.ao(this, effect);
		return effect;
	}
}
