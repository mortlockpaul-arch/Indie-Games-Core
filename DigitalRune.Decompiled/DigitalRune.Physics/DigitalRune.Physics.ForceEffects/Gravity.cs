using System;
using System.Collections.Generic;
using A;
using DigitalRune.Geometry.Collisions;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Physics.ForceEffects;

/// <summary>
///       Applies a gravity force to rigid bodies.
///       </summary>
public class Gravity : ForceField
{
	private Vector3F c98d8ed452c78aec1045b65483a09b90c;

	private Vector3F ce7492b093cd43724390fea15aadc8e59;

	/// <summary>
	///       Gets or sets the gravity acceleration vector.
	///       </summary>
	public Vector3F Acceleration
	{
		get
		{
			return ce7492b093cd43724390fea15aadc8e59;
		}
		set
		{
			ce7492b093cd43724390fea15aadc8e59 = value;
			c98d8ed452c78aec1045b65483a09b90c = ce7492b093cd43724390fea15aadc8e59;
			c98d8ed452c78aec1045b65483a09b90c.TryNormalize();
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Physics.ForceEffects.Gravity" /> class.
	///       </summary>
	public Gravity()
	{
		cfcf5000392556fa5f33e81db7674f9ed();
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Physics.ForceEffects.Gravity" /> class.
	///       </summary>
	/// <param name="areaOfEffect">The area of effect.</param>
	public Gravity(IAreaOfEffect areaOfEffect)
		: base(areaOfEffect)
	{
		cfcf5000392556fa5f33e81db7674f9ed();
	}

	private void cfcf5000392556fa5f33e81db7674f9ed()
	{
		Acceleration = new Vector3F(0f, -9.81f, 0f);
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
		bool flag = false;
		float stackingFactor = Simulation.Settings.Constraints.StackingFactor;
		if (stackingFactor > 0f)
		{
			while (true)
			{
				switch (3)
				{
				case 0:
					continue;
				}
				break;
			}
			if (1 == 0)
			{
				__ldtoken(Gravity.Apply(RigidBody));
			}
			if (body.IslandId >= 0 && body.IslandId < Simulation.IslandManager.IslandsInternal.Count)
			{
				while (true)
				{
					switch (5)
					{
					case 0:
						continue;
					}
					break;
				}
				if (Simulation.IslandManager.IslandsInternal[body.IslandId].RigidBodiesInternal.Count > 5)
				{
					while (true)
					{
						switch (2)
						{
						case 0:
							continue;
						}
						break;
					}
					IEnumerable<ContactSet> contacts = Simulation.CollisionDomain.GetContacts(body.CollisionObject);
					IEnumerator<ContactSet> enumerator = contacts.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							ContactSet current = enumerator.Current;
							Vector3F vector = ((current.ObjectA == body.CollisionObject) ? c98d8ed452c78aec1045b65483a09b90c : (-c98d8ed452c78aec1045b65483a09b90c));
							foreach (Contact item in current)
							{
								if (Vector3F.Dot(item.Normal, vector) <= 0.999f)
								{
									while (true)
									{
										switch (4)
										{
										case 0:
											continue;
										}
										flag = false;
										break;
									}
									break;
								}
								flag = true;
							}
							if (flag)
							{
								break;
							}
						}
					}
					finally
					{
						if (enumerator != null)
						{
							while (true)
							{
								switch (4)
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
		if (!flag)
		{
			ForceEffect.AddForce(body, Acceleration * body.MassFrame.Mass);
		}
		else
		{
			ForceEffect.AddForce(body, Acceleration * body.MassFrame.Mass / (stackingFactor + 1f));
		}
	}
}
