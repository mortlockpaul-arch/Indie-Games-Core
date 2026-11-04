using System.Runtime.CompilerServices;

namespace DigitalRune.Physics.ForceEffects;

/// <summary>
///       Applies a force effect to all bodies in the <see cref="P:DigitalRune.Physics.ForceEffects.ForceField.AreaOfEffect" /> individually.
///       </summary>
public abstract class ForceField : ForceEffect
{
	[CompilerGenerated]
	private IAreaOfEffect c0505240ed6f0a5fb20e4b87f9d340a4f;

	/// <summary>
	///       Gets or sets the area of effect.
	///       </summary>
	public IAreaOfEffect AreaOfEffect
	{
		[CompilerGenerated]
		get
		{
			return c0505240ed6f0a5fb20e4b87f9d340a4f;
		}
		[CompilerGenerated]
		set
		{
			c0505240ed6f0a5fb20e4b87f9d340a4f = value;
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Physics.ForceEffects.ForceField" /> class.
	///       </summary>
	protected ForceField()
		: this(new GlobalAreaOfEffect())
	{
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Physics.ForceEffects.ForceField" /> class.
	///       </summary>
	/// <param name="areaOfEffect">The area of effect.</param>
	protected ForceField(IAreaOfEffect areaOfEffect)
	{
		AreaOfEffect = areaOfEffect;
	}

	/// <summary>
	///       Called when the simulation wants this force effect to apply forces to rigid bodies.
	///       </summary>
	protected override void OnApply()
	{
		if (AreaOfEffect == null)
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
				__ldtoken(ForceField.OnApply());
			}
			AreaOfEffect.Apply(this);
			return;
		}
	}

	/// <summary>
	///       Applies the force effect to the specified body.
	///       </summary>
	/// <param name="body">The rigid body.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="body" /> is <see langword="null" />.
	///       </exception>
	public abstract void Apply(RigidBody body);
}
