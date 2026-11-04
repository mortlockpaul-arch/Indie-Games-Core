using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Physics.ForceEffects;

/// <summary>
///       Applies a force onto rigid bodies.
///       </summary>
public abstract class ForceEffect
{
	private Simulation c729503850b1dde68cce1c94f7a27d104;

	private bool caedbe52930a05951091c8dcdf6f7346c;

	/// <summary>
	///       Gets the simulation to which this force effect belongs.
	///       </summary>
	public Simulation Simulation
	{
		get
		{
			return c729503850b1dde68cce1c94f7a27d104;
		}
		internal set
		{
			if (c729503850b1dde68cce1c94f7a27d104 == value)
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
				if (1 == 0)
				{
					__ldtoken(ForceEffect.set_Simulation(Simulation));
				}
				if (c729503850b1dde68cce1c94f7a27d104 != null)
				{
					OnRemoveFromSimulation();
				}
				c729503850b1dde68cce1c94f7a27d104 = value;
				if (c729503850b1dde68cce1c94f7a27d104 != null)
				{
					OnAddToSimulation();
				}
				return;
			}
		}
	}

	/// <summary>
	///       Gets or sets a value indicating whether this <see cref="T:DigitalRune.Physics.ForceEffects.ForceEffect" /> is enabled.
	///       </summary>
	public bool Enabled
	{
		get
		{
			return caedbe52930a05951091c8dcdf6f7346c;
		}
		set
		{
			if (value != caedbe52930a05951091c8dcdf6f7346c)
			{
				caedbe52930a05951091c8dcdf6f7346c = value;
				if (caedbe52930a05951091c8dcdf6f7346c)
				{
					OnEnabled();
				}
				else
				{
					OnDisabled();
				}
			}
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Physics.ForceEffects.ForceEffect" /> class.
	///       </summary>
	protected ForceEffect()
	{
		Enabled = true;
	}

	internal void c1a3cd64f0234e04728b4ac42f0cab1e4()
	{
		if (Enabled)
		{
			OnApply();
		}
	}

	/// <summary>
	///       Called when the simulation wants this force effect to apply forces to rigid bodies.
	///       </summary>
	protected abstract void OnApply();

	/// <summary>
	///       Called when this force effect was enabled.
	///       </summary>
	protected virtual void OnEnabled()
	{
	}

	/// <summary>
	///       Called when this force effect was disabled.
	///       </summary>
	protected virtual void OnDisabled()
	{
	}

	/// <summary>
	///       Called when this force effect is added to a simulation.
	///       </summary>
	protected virtual void OnAddToSimulation()
	{
	}

	/// <summary>
	///       Called when this force effect is removed from a simulation.
	///       </summary>
	protected virtual void OnRemoveFromSimulation()
	{
	}

	/// <summary>
	///       Applies a force to the rigid body.
	///       </summary>
	/// <param name="body">The rigid body.</param>
	/// <param name="forceWorld">The force in world space.</param>
	/// <param name="positionWorld">
	///       The world space position where the force is a applied on the body.
	///       </param>
	protected static void AddForce(RigidBody body, Vector3F forceWorld, Vector3F positionWorld)
	{
		Vector3F vector = positionWorld - body.PoseCenterOfMass.Position;
		body.c9db2f3d2ea57a42c0954897116e7e018 += forceWorld;
		body.c8cc59972c02a657a040f0f4333ff4c3a += Vector3F.Cross(vector, forceWorld);
	}

	/// <summary>
	///       Applies a force to the rigid body at the center of mass.
	///       </summary>
	/// <param name="body">The rigid body.</param>
	/// <param name="forceWorld">The force in world space.</param>
	protected static void AddForce(RigidBody body, Vector3F forceWorld)
	{
		body.c9db2f3d2ea57a42c0954897116e7e018 += forceWorld;
	}

	/// <summary>
	///       Applies a torque to the rigid body at the center of mass.
	///       </summary>
	/// <param name="body">The rigid body.</param>
	/// <param name="torqueWorld">The torque in world space.</param>
	protected static void AddTorque(RigidBody body, Vector3F torqueWorld)
	{
		body.c8cc59972c02a657a040f0f4333ff4c3a += torqueWorld;
	}
}
