using A;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Physics.Constraints;

/// <summary>
///       Defines a constraint between two rigid bodies.
///       </summary>
public abstract class Constraint : IConstraint
{
	private RigidBody c93efd5f1d1e0a28c4b524b761aff691c;

	private RigidBody c3fa55a45bff4b015fb13d00b2b744ed8;

	private bool caedbe52930a05951091c8dcdf6f7346c = true;

	private bool cc880fd0beeb0e0af5d51cdd0c1599bde = true;

	private Simulation c729503850b1dde68cce1c94f7a27d104;

	/// <summary>
	///       Gets or sets the first body.
	///       </summary>
	public RigidBody BodyA
	{
		get
		{
			return c93efd5f1d1e0a28c4b524b761aff691c;
		}
		set
		{
			if (c93efd5f1d1e0a28c4b524b761aff691c == value)
			{
				return;
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
					__ldtoken(Constraint.set_BodyA(RigidBody));
				}
				ca00512673b1ee337d3bdefb52376f833();
				c93efd5f1d1e0a28c4b524b761aff691c = value;
				c1a7ce16a93e81ff59b129d0ca4c9d676();
				OnChanged();
				return;
			}
		}
	}

	/// <summary>
	///       Gets or sets the second body.
	///       </summary>
	public RigidBody BodyB
	{
		get
		{
			return c3fa55a45bff4b015fb13d00b2b744ed8;
		}
		set
		{
			if (c3fa55a45bff4b015fb13d00b2b744ed8 == value)
			{
				return;
			}
			while (true)
			{
				switch (3)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(Constraint.set_BodyB(RigidBody));
				}
				ca00512673b1ee337d3bdefb52376f833();
				c3fa55a45bff4b015fb13d00b2b744ed8 = value;
				c1a7ce16a93e81ff59b129d0ca4c9d676();
				OnChanged();
				return;
			}
		}
	}

	/// <summary>
	///       Gets or sets a value indicating whether this constraint is enabled.
	///       </summary>
	public bool Enabled
	{
		get
		{
			return caedbe52930a05951091c8dcdf6f7346c;
		}
		set
		{
			if (caedbe52930a05951091c8dcdf6f7346c == value)
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
					__ldtoken(Constraint.set_Enabled(bool));
				}
				ca00512673b1ee337d3bdefb52376f833();
				caedbe52930a05951091c8dcdf6f7346c = value;
				c1a7ce16a93e81ff59b129d0ca4c9d676();
				OnChanged();
				return;
			}
		}
	}

	/// <summary>
	///       Gets or sets a value indicating whether collisions between <see cref="P:DigitalRune.Physics.Constraints.Constraint.BodyA" /> and
	///       <see cref="P:DigitalRune.Physics.Constraints.Constraint.BodyB" /> are disabled.
	///       </summary>
	public bool CollisionEnabled
	{
		get
		{
			return cc880fd0beeb0e0af5d51cdd0c1599bde;
		}
		set
		{
			if (cc880fd0beeb0e0af5d51cdd0c1599bde == value)
			{
				return;
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
					__ldtoken(Constraint.set_CollisionEnabled(bool));
				}
				ca00512673b1ee337d3bdefb52376f833();
				cc880fd0beeb0e0af5d51cdd0c1599bde = value;
				c1a7ce16a93e81ff59b129d0ca4c9d676();
				OnChanged();
				return;
			}
		}
	}

	/// <summary>
	///       Gets the simulation to which this constraint belongs.
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
			if (c729503850b1dde68cce1c94f7a27d104 != null)
			{
				while (true)
				{
					switch (6)
					{
					case 0:
						continue;
					}
					break;
				}
				if (1 == 0)
				{
					__ldtoken(Constraint.set_Simulation(Simulation));
				}
				OnRemoveFromSimulation();
			}
			c729503850b1dde68cce1c94f7a27d104 = value;
			if (c729503850b1dde68cce1c94f7a27d104 == null)
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
				OnAddToSimulation();
				return;
			}
		}
	}

	/// <summary>
	///       Gets or sets the linear constraint impulse that was applied. 
	///       </summary>
	public abstract Vector3F LinearConstraintImpulse { get; }

	/// <summary>
	///       Gets or sets the angular constraint impulse that was applied.
	///       </summary>
	public abstract Vector3F AngularConstraintImpulse { get; }

	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.Physics.Constraints.Constraint" /> class</summary>
	protected Constraint()
	{
	}

	/// <summary>
	///       Called when this constraint is added to a simulation.
	///       </summary>
	/// <exception cref="T:DigitalRune.Physics.PhysicsException">
	///   <see cref="P:DigitalRune.Physics.Constraints.Constraint.BodyA" /> or <see cref="P:DigitalRune.Physics.Constraints.Constraint.BodyB" /> is <see langword="null" />. The constraint bodies 
	///       must not be <see langword="null" /> when a constraint/joint is added to a simulation.
	///       </exception>
	protected virtual void OnAddToSimulation()
	{
		if (BodyA != null)
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
				__ldtoken(Constraint.OnAddToSimulation());
			}
			if (BodyB != null)
			{
				c1a7ce16a93e81ff59b129d0ca4c9d676();
				OnChanged();
				return;
			}
		}
		throw new PhysicsException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(118));
	}

	/// <summary>
	///       Called when this constraint is removed from a simulation.
	///       </summary>
	protected virtual void OnRemoveFromSimulation()
	{
		ca00512673b1ee337d3bdefb52376f833();
		OnChanged();
	}

	/// <summary>
	///       Called by the simulation to prepare this constraint for constraint solving for a new
	///       time step.
	///       </summary>
	/// <exception cref="T:DigitalRune.Physics.PhysicsException">
	///   <see cref="P:DigitalRune.Physics.Constraints.Constraint.BodyA" /> or <see cref="P:DigitalRune.Physics.Constraints.Constraint.BodyB" /> is <see langword="null" />.
	///       </exception>
	public void Setup()
	{
		if (BodyA != null)
		{
			if (BodyB != null)
			{
				OnSetup();
				return;
			}
			while (true)
			{
				switch (4)
				{
				case 0:
					continue;
				}
				break;
			}
			if (1 == 0)
			{
				__ldtoken(Constraint.Setup());
			}
		}
		throw new PhysicsException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(284));
	}

	/// <summary>
	///       Called when constraint should be set-up for a new time step.
	///       </summary>
	protected abstract void OnSetup();

	/// <summary>
	///       Called by the simulation to apply an impulse that satisfies the constraint.
	///       </summary>
	/// <returns>
	///   <see langword="true" /> if a constraint larger than 
	///       <see cref="P:DigitalRune.Physics.Settings.ConstraintSettings.MinConstraintImpulse" /> was applied.
	///       </returns>
	/// <exception cref="T:DigitalRune.Physics.PhysicsException">
	///   <see cref="M:DigitalRune.Physics.Constraints.Constraint.Setup" /> was not called before <see cref="M:DigitalRune.Physics.Constraints.Constraint.ApplyImpulse" />.</exception>
	public bool ApplyImpulse()
	{
		return OnApplyImpulse();
	}

	/// <summary>
	///       Called when the constraint impulse should be applied.
	///       </summary>
	/// <returns>
	///   <see langword="true" /> if a constraint larger than 
	///       <see cref="P:DigitalRune.Physics.Settings.ConstraintSettings.MinConstraintImpulse" /> was applied.
	///       </returns>
	protected abstract bool OnApplyImpulse();

	private void c1a7ce16a93e81ff59b129d0ca4c9d676()
	{
		if (CollisionEnabled)
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
				__ldtoken(Constraint.c1a7ce16a93e81ff59b129d0ca4c9d676());
			}
			if (!Enabled)
			{
				return;
			}
			while (true)
			{
				switch (6)
				{
				case 0:
					continue;
				}
				if (BodyA == null || BodyB == null || Simulation == null)
				{
					return;
				}
				while (true)
				{
					switch (2)
					{
					case 0:
						continue;
					}
					if (!(Simulation.CollisionDomain.CollisionDetection.CollisionFilter is c097cad3a52ef7e36c2bf28861167a5f8 c097cad3a52ef7e36c2bf28861167a5f9))
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
						c097cad3a52ef7e36c2bf28861167a5f9.c03d72a32723924cb5b81c585d5df7d9d(this);
						return;
					}
				}
			}
		}
	}

	private void ca00512673b1ee337d3bdefb52376f833()
	{
		if (CollisionEnabled || !Enabled || BodyA == null)
		{
			return;
		}
		while (true)
		{
			switch (3)
			{
			case 0:
				continue;
			}
			if (1 == 0)
			{
				__ldtoken(Constraint.ca00512673b1ee337d3bdefb52376f833());
			}
			if (BodyB == null || Simulation == null)
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
				if (!(Simulation.CollisionDomain.CollisionDetection.CollisionFilter is c097cad3a52ef7e36c2bf28861167a5f8 c097cad3a52ef7e36c2bf28861167a5f9))
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
					c097cad3a52ef7e36c2bf28861167a5f9.c0293645f58f4f4f88a5baaf07d85095f(this);
					return;
				}
			}
		}
	}

	/// <summary>
	///       Called when properties of this constraint were changed.
	///       </summary>
	protected virtual void OnChanged()
	{
		if (!Enabled)
		{
			return;
		}
		while (true)
		{
			switch (1)
			{
			case 0:
				continue;
			}
			if (1 == 0)
			{
				__ldtoken(Constraint.OnChanged());
			}
			if (BodyA != null)
			{
				BodyA.WakeUp();
			}
			if (BodyB != null)
			{
				BodyB.WakeUp();
			}
			return;
		}
	}
}
