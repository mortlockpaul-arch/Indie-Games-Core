using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using DigitalRune.Physics.Constraints;

namespace DigitalRune.Physics;

/// <summary>
///       Describes a collection of rigid bodies and constraints that can be simulated independently.
///       </summary>
public class SimulationIsland
{
	private static readonly ResourcePool<SimulationIsland> c8aca4a0e3c0073ffb188fde832680587;

	private ReadOnlyCollection<Constraint> c719741ad5138547824c9ab491e7ec0f5;

	private ReadOnlyCollection<ContactConstraint> c3ecaf6cc88e7c4be42a38eb49c03e95c;

	private ReadOnlyCollection<RigidBody> cc9e68e8fb12b31d452f1103c0626e067;

	[CompilerGenerated]
	private Simulation cbebb653f415fd36f75adc579c8bca56d;

	[CompilerGenerated]
	private List<Constraint> cec0404f4852f2ecdb1e25398b78ede43;

	[CompilerGenerated]
	private List<ContactConstraint> c1e3485a64e2456dcc08c42b4a7865ae9;

	[CompilerGenerated]
	private List<RigidBody> c6c006e41800e3016be5690bfa80c3340;

	[CompilerGenerated]
	private Random c96a07709765f7dba3b33fbba799c4598;

	[CompilerGenerated]
	private static Func<SimulationIsland> c49253b4520c1b7e183dfcc9c426c0283;

	internal Simulation Simulation
	{
		[CompilerGenerated]
		get
		{
			return cbebb653f415fd36f75adc579c8bca56d;
		}
		[CompilerGenerated]
		set
		{
			cbebb653f415fd36f75adc579c8bca56d = value;
		}
	}

	/// <summary>
	///       Gets the constraints of this island.
	///       </summary>
	public ReadOnlyCollection<Constraint> Constraints
	{
		get
		{
			if (c719741ad5138547824c9ab491e7ec0f5 == null)
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
					__ldtoken(SimulationIsland.get_Constraints());
				}
				c719741ad5138547824c9ab491e7ec0f5 = ConstraintsInternal.AsReadOnly();
			}
			return c719741ad5138547824c9ab491e7ec0f5;
		}
	}

	/// <summary>
	///       Gets the contact constraints of this island.
	///       </summary>
	public ReadOnlyCollection<ContactConstraint> ContactConstraints
	{
		get
		{
			if (c3ecaf6cc88e7c4be42a38eb49c03e95c == null)
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
					__ldtoken(SimulationIsland.get_ContactConstraints());
				}
				c3ecaf6cc88e7c4be42a38eb49c03e95c = ContactConstraintsInternal.AsReadOnly();
			}
			return c3ecaf6cc88e7c4be42a38eb49c03e95c;
		}
	}

	/// <summary>
	///       Gets the rigid bodies of this island.
	///       </summary>
	public ReadOnlyCollection<RigidBody> RigidBodies
	{
		get
		{
			if (cc9e68e8fb12b31d452f1103c0626e067 == null)
			{
				cc9e68e8fb12b31d452f1103c0626e067 = RigidBodiesInternal.AsReadOnly();
			}
			return cc9e68e8fb12b31d452f1103c0626e067;
		}
	}

	internal List<Constraint> ConstraintsInternal
	{
		[CompilerGenerated]
		get
		{
			return cec0404f4852f2ecdb1e25398b78ede43;
		}
		[CompilerGenerated]
		private set
		{
			cec0404f4852f2ecdb1e25398b78ede43 = value;
		}
	}

	internal List<ContactConstraint> ContactConstraintsInternal
	{
		[CompilerGenerated]
		get
		{
			return c1e3485a64e2456dcc08c42b4a7865ae9;
		}
		[CompilerGenerated]
		private set
		{
			c1e3485a64e2456dcc08c42b4a7865ae9 = value;
		}
	}

	internal List<RigidBody> RigidBodiesInternal
	{
		[CompilerGenerated]
		get
		{
			return c6c006e41800e3016be5690bfa80c3340;
		}
		[CompilerGenerated]
		private set
		{
			c6c006e41800e3016be5690bfa80c3340 = value;
		}
	}

	internal Random Random
	{
		[CompilerGenerated]
		get
		{
			return c96a07709765f7dba3b33fbba799c4598;
		}
		[CompilerGenerated]
		set
		{
			c96a07709765f7dba3b33fbba799c4598 = value;
		}
	}

	private SimulationIsland()
	{
		ConstraintsInternal = new List<Constraint>();
		ContactConstraintsInternal = new List<ContactConstraint>();
		RigidBodiesInternal = new List<RigidBody>();
	}

	static SimulationIsland()
	{
		if (c49253b4520c1b7e183dfcc9c426c0283 == null)
		{
			while (true)
			{
				switch (7)
				{
				case 0:
					continue;
				}
				break;
			}
			if (1 == 0)
			{
				__ldtoken(SimulationIsland._002Ecctor());
			}
			c49253b4520c1b7e183dfcc9c426c0283 = () => new SimulationIsland();
		}
		c8aca4a0e3c0073ffb188fde832680587 = new ResourcePool<SimulationIsland>(c49253b4520c1b7e183dfcc9c426c0283, null, null);
	}

	internal static SimulationIsland c2d8ce8b1ccf2b905e3b18f8a5145fe8a()
	{
		return c8aca4a0e3c0073ffb188fde832680587.Obtain();
	}

	internal void ca18b9b5faa9851d6355f617787bb5859()
	{
		c0a4aead06b14dcb81618057a59d4a433();
		c8aca4a0e3c0073ffb188fde832680587.Recycle(this);
	}

	internal void c0a4aead06b14dcb81618057a59d4a433()
	{
		Simulation = null;
		ConstraintsInternal.Clear();
		ContactConstraintsInternal.Clear();
		RigidBodiesInternal.Clear();
	}

	internal bool cd467b98690cc77fb7e4140a129f87542()
	{
		bool flag = true;
		bool flag2 = false;
		int count = ContactConstraintsInternal.Count;
		for (int i = 0; i < count; i++)
		{
			ContactConstraint contactConstraint = ContactConstraintsInternal[i];
			RigidBody bodyA = contactConstraint.BodyA;
			RigidBody bodyB = contactConstraint.BodyB;
			if (bodyA.MotionType != MotionType.Dynamic)
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
				if (1 == 0)
				{
					__ldtoken(SimulationIsland.cd467b98690cc77fb7e4140a129f87542());
				}
				if (bodyA.IsSleeping)
				{
					goto IL_008c;
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
			}
			if (bodyB.MotionType != MotionType.Dynamic)
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
				if (bodyB.IsSleeping)
				{
					goto IL_008c;
				}
			}
			goto IL_008e;
			IL_008c:
			flag2 = true;
			goto IL_008e;
			IL_008e:
			if (bodyA != null && bodyA.MotionType == MotionType.Kinematic)
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
				if (!bodyA.c0d4caf414e813f4e1505938c05c05689)
				{
					if (bodyB == null || bodyB.MotionType != MotionType.Dynamic)
					{
						continue;
					}
					while (true)
					{
						switch (7)
						{
						case 0:
							continue;
						}
						break;
					}
					flag = false;
					break;
				}
			}
			if (bodyB == null || bodyB.MotionType != MotionType.Kinematic)
			{
				continue;
			}
			while (true)
			{
				switch (6)
				{
				case 0:
					continue;
				}
				break;
			}
			if (bodyB.c0d4caf414e813f4e1505938c05c05689)
			{
				continue;
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
			if (bodyA != null && bodyA.MotionType == MotionType.Dynamic)
			{
				flag = false;
				break;
			}
		}
		int count2 = ConstraintsInternal.Count;
		for (int j = 0; j < count2; j++)
		{
			Constraint constraint = ConstraintsInternal[j];
			RigidBody bodyA2 = constraint.BodyA;
			RigidBody bodyB2 = constraint.BodyB;
			if (bodyA2 != null && bodyA2.MotionType == MotionType.Kinematic)
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
				if (!bodyA2.c0d4caf414e813f4e1505938c05c05689)
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
					if (bodyB2 != null && bodyB2.MotionType == MotionType.Dynamic)
					{
						flag = false;
						break;
					}
					continue;
				}
			}
			if (bodyB2 == null || bodyB2.MotionType != MotionType.Kinematic)
			{
				continue;
			}
			while (true)
			{
				switch (3)
				{
				case 0:
					continue;
				}
				break;
			}
			if (bodyB2.c0d4caf414e813f4e1505938c05c05689)
			{
				continue;
			}
			while (true)
			{
				switch (1)
				{
				case 0:
					continue;
				}
				break;
			}
			if (bodyA2 == null)
			{
				continue;
			}
			while (true)
			{
				switch (3)
				{
				case 0:
					continue;
				}
				break;
			}
			if (bodyA2.MotionType != MotionType.Dynamic)
			{
				continue;
			}
			while (true)
			{
				switch (6)
				{
				case 0:
					continue;
				}
				break;
			}
			flag = false;
			break;
		}
		int count3 = RigidBodiesInternal.Count;
		if (flag)
		{
			for (int k = 0; k < count3; k++)
			{
				RigidBody rigidBody = RigidBodiesInternal[k];
				if (rigidBody.c0d4caf414e813f4e1505938c05c05689)
				{
					continue;
				}
				while (true)
				{
					switch (3)
					{
					case 0:
						continue;
					}
					break;
				}
				flag = false;
				break;
			}
		}
		if (RigidBodiesInternal[0].IslandTouchesImmovable && !flag2)
		{
			for (int l = 0; l < count3; l++)
			{
				RigidBody rigidBody2 = RigidBodiesInternal[l];
				rigidBody2.WakeUp();
			}
			while (true)
			{
				switch (5)
				{
				case 0:
					continue;
				}
				break;
			}
		}
		else if (!flag)
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
			for (int m = 0; m < count3; m++)
			{
				RigidBody rigidBody3 = RigidBodiesInternal[m];
				if (rigidBody3.IsSleeping)
				{
					rigidBody3.c4ada8b8e9f664f77c14206a9c1cb6a17(Simulation.Settings.Timing.FixedTimeStep * 1f);
				}
			}
		}
		for (int n = 0; n < count3; n++)
		{
			RigidBody rigidBody4 = RigidBodiesInternal[n];
			rigidBody4.IslandTouchesImmovable = flag2;
		}
		while (true)
		{
			switch (2)
			{
			case 0:
				continue;
			}
			return flag;
		}
	}

	[CompilerGenerated]
	private static SimulationIsland c4bffc38c4054efe6a3f3ff877749c085()
	{
		return new SimulationIsland();
	}
}
