using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using A;

namespace DigitalRune.Physics.ForceEffects;

/// <summary>
///       Applies a force field effect to all objects in a given list.
///       </summary>
public class ListAreaOfEffect : IAreaOfEffect
{
	[CompilerGenerated]
	private IList<RigidBody> c9aba495476df432177775bd6429a7541;

	/// <summary>
	///       Gets or sets the list of rigid bodies that are affected by the force effect.
	///       </summary>
	public IList<RigidBody> RigidBodies
	{
		[CompilerGenerated]
		get
		{
			return c9aba495476df432177775bd6429a7541;
		}
		[CompilerGenerated]
		set
		{
			c9aba495476df432177775bd6429a7541 = value;
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Physics.ForceEffects.ListAreaOfEffect" /> class.
	///       </summary>
	public ListAreaOfEffect()
	{
		RigidBodies = new List<RigidBody>();
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Physics.ForceEffects.ListAreaOfEffect" /> class.
	///       </summary>
	/// <param name="rigidBodies">
	///       The list of rigid bodies. The property <see cref="P:DigitalRune.Physics.ForceEffects.ListAreaOfEffect.RigidBodies" /> is set to this list. 
	///       The list is not copied.
	///       </param>
	public ListAreaOfEffect(IList<RigidBody> rigidBodies)
	{
		RigidBodies = rigidBodies;
	}

	/// <summary>
	///       Calls <see cref="M:DigitalRune.Physics.ForceEffects.ForceField.Apply(DigitalRune.Physics.RigidBody)" /> of the given force field for all objects
	///       in the area of effect.
	///       </summary>
	/// <param name="forceField">The force field.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="forceField" /> is <see langword="null" />.
	///       </exception>
	public void Apply(ForceField forceField)
	{
		if (forceField == null)
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
						__ldtoken(ListAreaOfEffect.Apply(ForceField));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4965));
				}
			}
		}
		if (RigidBodies == null)
		{
			return;
		}
		int count = RigidBodies.Count;
		for (int i = 0; i < count; i++)
		{
			RigidBody rigidBody = RigidBodies[i];
			if (rigidBody.MotionType == MotionType.Dynamic)
			{
				forceField.Apply(rigidBody);
			}
		}
		while (true)
		{
			switch (3)
			{
			case 0:
				break;
			default:
				return;
			}
		}
	}
}
