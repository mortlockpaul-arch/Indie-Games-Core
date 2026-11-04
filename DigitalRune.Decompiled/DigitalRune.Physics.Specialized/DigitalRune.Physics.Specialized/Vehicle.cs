using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using A;
using DigitalRune.Collections;
using DigitalRune.Mathematics;

namespace DigitalRune.Physics.Specialized;

/// <summary>
///       Represents a simulated vehicle.
///       </summary>
public class Vehicle
{
	private readonly c474bd0c7094acd3af328887ce9866162 cdce9fc77873c3d41a418cf6852418351;

	private RigidBody c0e67846c640e0e5e5031d64fdf7c95bb;

	[CompilerGenerated]
	private Simulation cbebb653f415fd36f75adc579c8bca56d;

	[CompilerGenerated]
	private NotifyingCollection<Wheel> c4a8f32d6b0360c3e7f0104a5f516561f;

	/// <summary>
	///       Gets or sets the simulation.
	///       </summary>
	public Simulation Simulation
	{
		[CompilerGenerated]
		get
		{
			return cbebb653f415fd36f75adc579c8bca56d;
		}
		[CompilerGenerated]
		private set
		{
			cbebb653f415fd36f75adc579c8bca56d = value;
		}
	}

	/// <summary>
	///       Gets or sets the chassis.
	///       </summary>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="value" /> is <see langword="null" />.
	///       </exception>
	public RigidBody Chassis
	{
		get
		{
			return c0e67846c640e0e5e5031d64fdf7c95bb;
		}
		set
		{
			if (value == null)
			{
				throw new ArgumentNullException();
			}
			c0e67846c640e0e5e5031d64fdf7c95bb = value;
			foreach (Wheel wheel in Wheels)
			{
				wheel.c7dff647aa7bb9df13d5440c878eb477d();
			}
		}
	}

	/// <summary>
	///       Gets or sets a value indicating whether this vehicle is enabled.
	///       </summary>
	public bool Enabled
	{
		get
		{
			return Chassis.Simulation != null;
		}
		set
		{
			if (Enabled == value)
			{
				while (true)
				{
					switch (7)
					{
					case 0:
						break;
					default:
						if (1 == 0)
						{
							__ldtoken(Vehicle.set_Enabled(bool));
						}
						return;
					}
				}
			}
			if (value)
			{
				while (true)
				{
					switch (7)
					{
					case 0:
						break;
					default:
						Simulation.ForceEffects.Add(cdce9fc77873c3d41a418cf6852418351);
						Simulation.RigidBodies.Add(Chassis);
						{
							foreach (Wheel wheel in Wheels)
							{
								wheel.c8bdc38c9ecd02f560f18d792aa723d13 = true;
							}
							return;
						}
					}
				}
			}
			foreach (Wheel wheel2 in Wheels)
			{
				wheel2.c8bdc38c9ecd02f560f18d792aa723d13 = false;
			}
			Simulation.RigidBodies.Remove(Chassis);
			Simulation.ForceEffects.Remove(cdce9fc77873c3d41a418cf6852418351);
		}
	}

	/// <summary>
	///       Gets the wheels.
	///       </summary>
	public NotifyingCollection<Wheel> Wheels
	{
		[CompilerGenerated]
		get
		{
			return c4a8f32d6b0360c3e7f0104a5f516561f;
		}
		[CompilerGenerated]
		private set
		{
			c4a8f32d6b0360c3e7f0104a5f516561f = value;
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Physics.Specialized.Vehicle" /> class.
	///       </summary>
	/// <param name="simulation">The simulation.</param>
	/// <param name="chassis">The rigid body for the chassis.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="simulation" /> or <paramref name="chassis" /> is <see langword="null" />.
	///       </exception>
	public Vehicle(Simulation simulation, RigidBody chassis)
	{
		if (simulation == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(421));
		}
		if (chassis == null)
		{
			while (true)
			{
				switch (6)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(Vehicle._002Ector(Simulation, RigidBody));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(823));
				}
			}
		}
		Simulation = simulation;
		c0e67846c640e0e5e5031d64fdf7c95bb = chassis;
		cdce9fc77873c3d41a418cf6852418351 = new c474bd0c7094acd3af328887ce9866162(this);
		Wheels = new NotifyingCollection<Wheel>(allowNull: false, allowDuplicates: false);
		Wheels.CollectionChanged += c7ed6fb89559cedc9a133de75af160402;
	}

	private void c7ed6fb89559cedc9a133de75af160402(object c807f682152464d11dde83a885bf87124, CollectionChangedEventArgs<Wheel> c7fefeb91d4632901bd52a1f245b70db1)
	{
		if (c7fefeb91d4632901bd52a1f245b70db1.Action == CollectionChangedAction.Move)
		{
			return;
		}
		using (List<Wheel>.Enumerator enumerator = c7fefeb91d4632901bd52a1f245b70db1.OldItems.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				Wheel current = enumerator.Current;
				current.c8bdc38c9ecd02f560f18d792aa723d13 = false;
				current.Vehicle = null;
			}
			while (true)
			{
				switch (2)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(Vehicle.c7ed6fb89559cedc9a133de75af160402(object, CollectionChangedEventArgs<Wheel>));
				}
				break;
			}
		}
		foreach (Wheel newItem in c7fefeb91d4632901bd52a1f245b70db1.NewItems)
		{
			if (newItem.Vehicle != null)
			{
				while (true)
				{
					switch (3)
					{
					case 0:
						break;
					default:
						throw new InvalidOperationException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(587));
					}
				}
			}
			newItem.Vehicle = this;
			newItem.c8bdc38c9ecd02f560f18d792aa723d13 = Enabled;
		}
	}

	/// <summary>
	///       Sets the steering angles for a standard 4 wheel car.
	///       </summary>
	/// <param name="steeringAngle">The steering angle.</param>
	/// <param name="frontLeft">The front left wheel.</param>
	/// <param name="frontRight">The front right wheel.</param>
	/// <param name="backLeft">The back left wheel.</param>
	/// <param name="backRight">The back right wheel.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="frontLeft" />, <paramref name="frontRight" />, <paramref name="backLeft" />, or
	///       <paramref name="backRight" /> is <see langword="null" />.
	///       </exception>
	public static void SetCarSteeringAngle(float steeringAngle, Wheel frontLeft, Wheel frontRight, Wheel backLeft, Wheel backRight)
	{
		if (frontLeft == null)
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
						__ldtoken(Vehicle.SetCarSteeringAngle(float, Wheel, Wheel, Wheel, Wheel));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(747));
				}
			}
		}
		if (frontRight == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(766));
		}
		if (backLeft == null)
		{
			while (true)
			{
				switch (4)
				{
				case 0:
					break;
				default:
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(787));
				}
			}
		}
		if (backRight == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(804));
		}
		backLeft.SteeringAngle = 0f;
		backRight.SteeringAngle = 0f;
		if (Numeric.IsZero(steeringAngle))
		{
			while (true)
			{
				switch (1)
				{
				case 0:
					break;
				default:
					frontLeft.SteeringAngle = 0f;
					frontRight.SteeringAngle = 0f;
					return;
				}
			}
		}
		Wheel wheel;
		Wheel wheel2;
		if (steeringAngle > 0f)
		{
			wheel = frontLeft;
			wheel2 = frontRight;
		}
		else
		{
			wheel = frontRight;
			wheel2 = frontLeft;
		}
		wheel.SteeringAngle = steeringAngle;
		float num = backLeft.Offset.Z - frontLeft.Offset.Z;
		float num2 = frontRight.Offset.X - frontLeft.Offset.X;
		float num3 = Math.Abs(steeringAngle);
		float num4 = (float)Math.Atan2(num, (double)num / Math.Tan(num3) + (double)num2);
		wheel2.SteeringAngle = (float)Math.Sign(steeringAngle) * num4;
	}
}
