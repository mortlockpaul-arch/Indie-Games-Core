using System;
using System.Runtime.CompilerServices;
using A;

namespace DigitalRune.Physics.ForceEffects;

/// <summary>
///       Applies a force field effect to all <see cref="P:DigitalRune.Physics.Simulation.RigidBodies" /> of the 
///       <see cref="T:DigitalRune.Physics.Simulation" />. (An optional predicate can be used to exclude certain objects.)
///       </summary>
public class GlobalAreaOfEffect : IAreaOfEffect
{
	[CompilerGenerated]
	private Predicate<RigidBody> cb299806e19708bf27aa0445b253b9d9b;

	/// <summary>
	///       Gets or sets the predicate that can be used to exclude rigid bodies from the area of effect.
	///       </summary>
	public Predicate<RigidBody> Exclude
	{
		[CompilerGenerated]
		get
		{
			return cb299806e19708bf27aa0445b253b9d9b;
		}
		[CompilerGenerated]
		set
		{
			cb299806e19708bf27aa0445b253b9d9b = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.Physics.ForceEffects.GlobalAreaOfEffect" /> class</summary>
	public GlobalAreaOfEffect()
	{
	}

	/// <summary>
	///       Calls <see cref="M:DigitalRune.Physics.ForceEffects.ForceField.Apply(DigitalRune.Physics.RigidBody)" /> of the given force field for all objects in 
	///       the area of effect.
	///       </summary>
	/// <param name="forceField">The force field.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="forceField" /> is <see langword="null" />.
	///       </exception>
	public void Apply(ForceField forceField)
	{
		if (forceField == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4965));
		}
		Simulation simulation = forceField.Simulation;
		if (simulation == null)
		{
			return;
		}
		while (true)
		{
			switch (5)
			{
			case 0:
				continue;
			}
			if (1 == 0)
			{
				__ldtoken(GlobalAreaOfEffect.Apply(ForceField));
			}
			int count = simulation.RigidBodies.Count;
			for (int i = 0; i < count; i++)
			{
				RigidBody rigidBody = simulation.RigidBodies[i];
				if (rigidBody.MotionType == MotionType.Dynamic && (Exclude == null || !Exclude(rigidBody)))
				{
					forceField.Apply(rigidBody);
				}
			}
			while (true)
			{
				switch (1)
				{
				case 0:
					break;
				default:
					return;
				}
			}
		}
	}
}
