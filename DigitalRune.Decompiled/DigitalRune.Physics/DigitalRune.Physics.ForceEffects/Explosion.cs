using System;
using System.Runtime.CompilerServices;
using A;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Physics.ForceEffects;

/// <summary>
///       Applies an explosion force for a short duration.
///       </summary>
public class Explosion : ForceField
{
	[CompilerGenerated]
	private TimeSpan c8d797d4f9ccb16dfcb62d12942181081;

	[CompilerGenerated]
	private TimeSpan c7ab67e26570b012da2ab1571ff50ad74;

	[CompilerGenerated]
	private float c07893f403a9dde3d3027e4f1287e20f8;

	[CompilerGenerated]
	private Vector3F cfad2b109ba2c6f891737b1ad92b7eb51;

	[CompilerGenerated]
	private float ccfda1a8375cef2e900151590eece129b;

	/// <summary>
	///       Gets or sets the duration of the explosion.
	///       </summary>
	public TimeSpan Duration
	{
		[CompilerGenerated]
		get
		{
			return c8d797d4f9ccb16dfcb62d12942181081;
		}
		[CompilerGenerated]
		set
		{
			c8d797d4f9ccb16dfcb62d12942181081 = value;
		}
	}

	private TimeSpan EndTime
	{
		[CompilerGenerated]
		get
		{
			return c7ab67e26570b012da2ab1571ff50ad74;
		}
		[CompilerGenerated]
		set
		{
			c7ab67e26570b012da2ab1571ff50ad74 = value;
		}
	}

	/// <summary>
	///       Gets or sets the force magnitude of the explosion.
	///       </summary>
	public float Force
	{
		[CompilerGenerated]
		get
		{
			return c07893f403a9dde3d3027e4f1287e20f8;
		}
		[CompilerGenerated]
		set
		{
			c07893f403a9dde3d3027e4f1287e20f8 = value;
		}
	}

	/// <summary>
	///       Gets or sets the position of the center of the explosion.
	///       </summary>
	public Vector3F Position
	{
		[CompilerGenerated]
		get
		{
			return cfad2b109ba2c6f891737b1ad92b7eb51;
		}
		[CompilerGenerated]
		set
		{
			cfad2b109ba2c6f891737b1ad92b7eb51 = value;
		}
	}

	/// <summary>
	///       Gets or sets the explosion radius.
	///       </summary>
	public float Radius
	{
		[CompilerGenerated]
		get
		{
			return ccfda1a8375cef2e900151590eece129b;
		}
		[CompilerGenerated]
		set
		{
			ccfda1a8375cef2e900151590eece129b = value;
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Physics.ForceEffects.Explosion" /> class.
	///       </summary>
	public Explosion()
	{
		cfcf5000392556fa5f33e81db7674f9ed();
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Physics.ForceEffects.Explosion" /> class.
	///       </summary>
	/// <param name="areaOfEffect">The area of effect.</param>
	public Explosion(IAreaOfEffect areaOfEffect)
		: base(areaOfEffect)
	{
		cfcf5000392556fa5f33e81db7674f9ed();
	}

	private void cfcf5000392556fa5f33e81db7674f9ed()
	{
		Duration = TimeSpan.FromSeconds(0.03);
		Force = 100000f;
		EndTime = TimeSpan.MinValue;
		Radius = 5f;
	}

	/// <summary>
	///       Called when this force effect is added to a simulation.
	///       </summary>
	protected override void OnAddToSimulation()
	{
		base.OnAddToSimulation();
		if (!(EndTime == TimeSpan.MinValue))
		{
			return;
		}
		while (true)
		{
			switch (4)
			{
			case 0:
				continue;
			}
			if (1 == 0)
			{
				__ldtoken(Explosion.OnAddToSimulation());
			}
			EndTime = Simulation.Time + Duration;
			return;
		}
	}

	/// <summary>
	///       Called when the simulation wants this force effect to apply forces to rigid bodies.
	///       </summary>
	protected override void OnApply()
	{
		if (EndTime <= Simulation.Time)
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
						__ldtoken(Explosion.OnApply());
					}
					Simulation.ForceEffects.Remove(this);
					return;
				}
			}
		}
		AreaOfEffect.Apply(this);
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
			while (true)
			{
				switch (1)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(Explosion.Apply(RigidBody));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2846), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2855));
				}
			}
		}
		Vector3F vector3F = body.PoseCenterOfMass.Position - Position;
		float lengthSquared = vector3F.LengthSquared;
		float num = Radius * Radius;
		if (lengthSquared > num)
		{
			return;
		}
		float num2 = 1f - lengthSquared / num;
		if (!vector3F.TryNormalize())
		{
			while (true)
			{
				switch (4)
				{
				case 0:
					continue;
				}
				break;
			}
			vector3F = Vector3F.UnitY;
		}
		Vector3F forceWorld = Force * num2 * vector3F;
		ForceEffect.AddForce(body, forceWorld);
	}
}
