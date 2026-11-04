using System;
using System.Runtime.CompilerServices;
using A;

namespace DigitalRune.Physics.ForceEffects;

/// <summary>
///       Applies a damping force (viscous drag).
///       </summary>
public class Damping : ForceField
{
	[CompilerGenerated]
	private float c7845e29061576370b40496129fa33a0d;

	[CompilerGenerated]
	private float ca5be6d2ab265828e2c38ac8511da2da8;

	/// <summary>
	///       The linear damping coefficient.
	///       </summary>
	public float LinearDamping
	{
		[CompilerGenerated]
		get
		{
			return c7845e29061576370b40496129fa33a0d;
		}
		[CompilerGenerated]
		set
		{
			c7845e29061576370b40496129fa33a0d = value;
		}
	}

	/// <summary>
	///       The angular damping coefficient.
	///       </summary>
	public float AngularDamping
	{
		[CompilerGenerated]
		get
		{
			return ca5be6d2ab265828e2c38ac8511da2da8;
		}
		[CompilerGenerated]
		set
		{
			ca5be6d2ab265828e2c38ac8511da2da8 = value;
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Physics.ForceEffects.Damping" /> class.
	///       </summary>
	public Damping()
	{
		cfcf5000392556fa5f33e81db7674f9ed();
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Physics.ForceEffects.Damping" /> class.
	///       </summary>
	/// <param name="areaOfEffect">The area of effect.</param>
	public Damping(IAreaOfEffect areaOfEffect)
		: base(areaOfEffect)
	{
		cfcf5000392556fa5f33e81db7674f9ed();
	}

	private void cfcf5000392556fa5f33e81db7674f9ed()
	{
		LinearDamping = 0.02f;
		AngularDamping = 0.2f;
	}

	/// <summary>
	///       Applies the force effect to the specified body.
	///       </summary>
	/// <param name="body">The rigid body.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="body" /> is <see langword="null" />.
	///       </exception>
	public override void Apply(RigidBody body)
	{
		if (body == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2846), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2855));
		}
		if (body.IsSleeping)
		{
			while (true)
			{
				switch (2)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(Damping.Apply(RigidBody));
					}
					return;
				}
			}
		}
		float fixedTimeStep = Simulation.Settings.Timing.FixedTimeStep;
		body.LinearVelocity = (1f - LinearDamping * fixedTimeStep) * body.LinearVelocity;
		body.AngularVelocity = (1f - AngularDamping * fixedTimeStep) * body.AngularVelocity;
	}
}
