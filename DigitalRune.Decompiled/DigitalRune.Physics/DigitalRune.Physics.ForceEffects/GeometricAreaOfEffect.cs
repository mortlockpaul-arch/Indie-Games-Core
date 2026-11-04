using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using A;
using DigitalRune.Geometry.Collisions;

namespace DigitalRune.Physics.ForceEffects;

/// <summary>
///       Applies a force field effect to all rigid bodies that touch a certain 
///       <see cref="P:DigitalRune.Physics.ForceEffects.GeometricAreaOfEffect.CollisionObject" />.
///       </summary>
public class GeometricAreaOfEffect : IAreaOfEffect
{
	[CompilerGenerated]
	private CollisionObject c9f70e8b23bf60595b29e28c6f9beb85d;

	/// <summary>
	///       Gets or sets the collision object that defines the area of effect.
	///       </summary>
	public CollisionObject CollisionObject
	{
		[CompilerGenerated]
		get
		{
			return c9f70e8b23bf60595b29e28c6f9beb85d;
		}
		[CompilerGenerated]
		set
		{
			c9f70e8b23bf60595b29e28c6f9beb85d = value;
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Physics.ForceEffects.GeometricAreaOfEffect" /> class.
	///       </summary>
	public GeometricAreaOfEffect()
	{
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Physics.ForceEffects.GeometricAreaOfEffect" /> class.
	///       </summary>
	/// <param name="collisionObject">The collision object.</param>
	public GeometricAreaOfEffect(CollisionObject collisionObject)
	{
		CollisionObject = collisionObject;
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
				switch (3)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(GeometricAreaOfEffect.Apply(ForceField));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4965));
				}
			}
		}
		if (forceField.Simulation == null)
		{
			while (true)
			{
				switch (5)
				{
				case 0:
					break;
				default:
					return;
				}
			}
		}
		if (CollisionObject == null || CollisionObject.Domain != forceField.Simulation.CollisionDomain)
		{
			return;
		}
		while (true)
		{
			switch (7)
			{
			case 0:
				continue;
			}
			IEnumerable<CollisionObject> contactObjects = forceField.Simulation.CollisionDomain.GetContactObjects(CollisionObject);
			IEnumerator<CollisionObject> enumerator = contactObjects.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					CollisionObject current = enumerator.Current;
					if (!(current.GeometricObject is RigidBody { MotionType: MotionType.Dynamic } rigidBody))
					{
						continue;
					}
					while (true)
					{
						switch (2)
						{
						case 0:
							continue;
						}
						break;
					}
					forceField.Apply(rigidBody);
				}
				return;
			}
			finally
			{
				if (enumerator != null)
				{
					while (true)
					{
						switch (2)
						{
						case 0:
							continue;
						}
						enumerator.Dispose();
						break;
					}
				}
			}
		}
	}
}
