using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using A;
using DigitalRune.Collections;
using DigitalRune.Physics.Constraints;

namespace DigitalRune.Physics;

/// <summary>
///       Manages <see cref="T:DigitalRune.Physics.SimulationIsland" />s of a <see cref="T:DigitalRune.Physics.Simulation" />.
///       </summary>
public class SimulationIslandManager : IComparer<SimulationIsland>
{
	private readonly Simulation c729503850b1dde68cce1c94f7a27d104;

	private readonly c3813009c1171253be56c785c7a4646a7 c9c2fbfc9d7bfa5d50ed9550ec54a82ce;

	private readonly List<c972bb04a7023e8f3ae8a26639588d00f> c3060fa8260bf134d1d1c6814910a3d2c = new List<c972bb04a7023e8f3ae8a26639588d00f>(256);

	private ReadOnlyCollection<SimulationIsland> c9530387e19be8ac237d8cd8a22a47da2;

	[CompilerGenerated]
	private List<SimulationIsland> c6214d09b545e1942a2366c3841bf6a3a;

	[CompilerGenerated]
	private List<Pair<RigidBody>> c1d7304fc5afe354457c70fa66bc4731f;

	/// <summary>
	///       Gets the simulation islands.
	///       </summary>
	public ReadOnlyCollection<SimulationIsland> Islands
	{
		get
		{
			if (c9530387e19be8ac237d8cd8a22a47da2 == null)
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
					__ldtoken(SimulationIslandManager.get_Islands());
				}
				c9530387e19be8ac237d8cd8a22a47da2 = IslandsInternal.AsReadOnly();
			}
			return c9530387e19be8ac237d8cd8a22a47da2;
		}
	}

	internal List<SimulationIsland> IslandsInternal
	{
		[CompilerGenerated]
		get
		{
			return c6214d09b545e1942a2366c3841bf6a3a;
		}
		[CompilerGenerated]
		private set
		{
			c6214d09b545e1942a2366c3841bf6a3a = value;
		}
	}

	internal List<Pair<RigidBody>> ContactSetLinks
	{
		[CompilerGenerated]
		get
		{
			return c1d7304fc5afe354457c70fa66bc4731f;
		}
		[CompilerGenerated]
		private set
		{
			c1d7304fc5afe354457c70fa66bc4731f = value;
		}
	}

	static SimulationIslandManager()
	{
		TypeInitializer.Initialize(c31a405ff5d24b3c4be569f1e85a6f73f.ce9f9bc9ecd5ed400a711abaa33452c25);
		TypeInitializer.CreateInstance(13, c31a405ff5d24b3c4be569f1e85a6f73f.ccda5e1cdd8faa90b24f6bc28c9af6b28);
	}

	internal SimulationIslandManager(Simulation cc597c252189878e9fdf2cba65ddefbe4)
	{
		if (cc597c252189878e9fdf2cba65ddefbe4 == null)
		{
			while (true)
			{
				switch (5)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(SimulationIslandManager._002Ector(Simulation));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2825));
				}
			}
		}
		c729503850b1dde68cce1c94f7a27d104 = cc597c252189878e9fdf2cba65ddefbe4;
		c9c2fbfc9d7bfa5d50ed9550ec54a82ce = new c3813009c1171253be56c785c7a4646a7();
		IslandsInternal = new List<SimulationIsland>();
		ContactSetLinks = new List<Pair<RigidBody>>();
	}

	internal void c322ef1ce205c6dbcf3a93ad899943435()
	{
		int count = c729503850b1dde68cce1c94f7a27d104.RigidBodies.Count;
		int count2 = IslandsInternal.Count;
		for (int i = 0; i < count2; i++)
		{
			IslandsInternal[i].ca18b9b5faa9851d6355f617787bb5859();
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
				__ldtoken(SimulationIslandManager.c322ef1ce205c6dbcf3a93ad899943435());
			}
			IslandsInternal.Clear();
			c9c2fbfc9d7bfa5d50ed9550ec54a82ce.cf06348a19a922c6295282d49671cbe77(count);
			for (int j = 0; j < count; j++)
			{
				c729503850b1dde68cce1c94f7a27d104.RigidBodies[j].IslandId = j;
			}
			int count3 = c729503850b1dde68cce1c94f7a27d104.c32f28f83e5bad489c86f1585da1c621f.Count;
			int count4 = ContactSetLinks.Count;
			for (int k = 0; k < count4; k++)
			{
				Pair<RigidBody> pair = ContactSetLinks[k];
				RigidBody first = pair.First;
				RigidBody second = pair.Second;
				if (first.MotionType == MotionType.Dynamic && second.MotionType == MotionType.Dynamic)
				{
					c9c2fbfc9d7bfa5d50ed9550ec54a82ce.c9d6febe2f42deb115290757a0772d244(first.IslandId, second.IslandId);
				}
			}
			int count5 = c729503850b1dde68cce1c94f7a27d104.Constraints.Count;
			for (int l = 0; l < count5; l++)
			{
				Constraint constraint = c729503850b1dde68cce1c94f7a27d104.Constraints[l];
				RigidBody bodyA = constraint.BodyA;
				RigidBody bodyB = constraint.BodyB;
				if (!constraint.Enabled)
				{
					continue;
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
				if (bodyA.MotionType != MotionType.Dynamic)
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
				if (bodyB.MotionType != MotionType.Dynamic)
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
				c9c2fbfc9d7bfa5d50ed9550ec54a82ce.c9d6febe2f42deb115290757a0772d244(bodyA.IslandId, bodyB.IslandId);
			}
			while (true)
			{
				switch (3)
				{
				case 0:
					continue;
				}
				for (int m = 0; m < count; m++)
				{
					RigidBody rigidBody = c729503850b1dde68cce1c94f7a27d104.RigidBodies[m];
					if (rigidBody.MotionType == MotionType.Dynamic)
					{
						rigidBody.IslandId = c9c2fbfc9d7bfa5d50ed9550ec54a82ce.cce0f90a395df190a07fc3d56126999ae(rigidBody.IslandId);
					}
					else
					{
						rigidBody.IslandId = -1;
					}
				}
				while (true)
				{
					switch (7)
					{
					case 0:
						continue;
					}
					c3060fa8260bf134d1d1c6814910a3d2c.Clear();
					for (int n = 0; n < count; n++)
					{
						c3060fa8260bf134d1d1c6814910a3d2c.Add(new c972bb04a7023e8f3ae8a26639588d00f(c729503850b1dde68cce1c94f7a27d104.RigidBodies[n].IslandId, n));
					}
					c3060fa8260bf134d1d1c6814910a3d2c.Sort();
					count2 = 0;
					int num = -1;
					SimulationIsland simulationIsland = null;
					for (int num2 = 0; num2 < count; num2++)
					{
						c972bb04a7023e8f3ae8a26639588d00f c972bb04a7023e8f3ae8a26639588d00f2 = c3060fa8260bf134d1d1c6814910a3d2c[num2];
						int c4b2dab96343831e7df85372853e51f0e = c972bb04a7023e8f3ae8a26639588d00f2.c4b2dab96343831e7df85372853e51f0e;
						RigidBody rigidBody2 = c729503850b1dde68cce1c94f7a27d104.RigidBodies[c4b2dab96343831e7df85372853e51f0e];
						if (rigidBody2.IslandId == -1)
						{
							continue;
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
						if (c972bb04a7023e8f3ae8a26639588d00f2.cb4ba6e6e9c6a11b6723dbb2c70241460 != num)
						{
							simulationIsland = SimulationIsland.c2d8ce8b1ccf2b905e3b18f8a5145fe8a();
							simulationIsland.Simulation = c729503850b1dde68cce1c94f7a27d104;
							num = c972bb04a7023e8f3ae8a26639588d00f2.cb4ba6e6e9c6a11b6723dbb2c70241460;
							IslandsInternal.Add(simulationIsland);
							count2++;
						}
						rigidBody2.IslandId = count2 - 1;
						simulationIsland.RigidBodiesInternal.Add(rigidBody2);
					}
					for (int num3 = 0; num3 < count3; num3++)
					{
						ContactConstraint contactConstraint = c729503850b1dde68cce1c94f7a27d104.c32f28f83e5bad489c86f1585da1c621f[num3];
						int num4 = ce5afd0e6bed231cbb28684fe95c5add1(contactConstraint);
						if (num4 < 0)
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
						IslandsInternal[num4].ContactConstraintsInternal.Add(contactConstraint);
					}
					for (int num5 = 0; num5 < count5; num5++)
					{
						Constraint constraint2 = c729503850b1dde68cce1c94f7a27d104.Constraints[num5];
						if (!constraint2.Enabled)
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
						int num6 = ce5afd0e6bed231cbb28684fe95c5add1(constraint2);
						if (num6 < 0)
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
						IslandsInternal[num6].ConstraintsInternal.Add(constraint2);
					}
					if (!c729503850b1dde68cce1c94f7a27d104.Settings.EnableMultithreading)
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
						IslandsInternal.Sort(this);
						return;
					}
				}
			}
		}
	}

	private static int ce5afd0e6bed231cbb28684fe95c5add1(IConstraint c05b65a62b8880bec8655000b1122cd7f)
	{
		RigidBody bodyA = c05b65a62b8880bec8655000b1122cd7f.BodyA;
		if (bodyA.IslandId >= 0)
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
						__ldtoken(SimulationIslandManager.ce5afd0e6bed231cbb28684fe95c5add1(IConstraint));
					}
					return bodyA.IslandId;
				}
			}
		}
		return c05b65a62b8880bec8655000b1122cd7f.BodyB.IslandId;
	}

	private int cd5266e31e2b67953ddbb7776e1832e35(SimulationIsland c53c34f98e81f56eec1feba2e1612161e, SimulationIsland c7348308c73f921441266c677fe295f0b)
	{
		int num = c53c34f98e81f56eec1feba2e1612161e.ConstraintsInternal.Count + c53c34f98e81f56eec1feba2e1612161e.ContactConstraintsInternal.Count;
		int num2 = c7348308c73f921441266c677fe295f0b.ConstraintsInternal.Count + c7348308c73f921441266c677fe295f0b.ContactConstraintsInternal.Count;
		return num2 - num;
	}

	int IComparer<SimulationIsland>.Compare(SimulationIsland c53c34f98e81f56eec1feba2e1612161e, SimulationIsland c7348308c73f921441266c677fe295f0b)
	{
		//ILSpy generated this explicit interface implementation from .override directive in cd5266e31e2b67953ddbb7776e1832e35
		return this.cd5266e31e2b67953ddbb7776e1832e35(c53c34f98e81f56eec1feba2e1612161e, c7348308c73f921441266c677fe295f0b);
	}
}
