using System;
using DigitalRune.Mathematics.Statistics;
using DigitalRune.Physics;
using DigitalRune.Physics.Constraints;

namespace A;

internal class c02168253bcb64d2a565d402ed87663bb : c2943f95551cd5c00e1ad7d7f91414782
{
	public c02168253bcb64d2a565d402ed87663bb(Simulation cc597c252189878e9fdf2cba65ddefbe4)
		: base(cc597c252189878e9fdf2cba65ddefbe4)
	{
	}

	public override void cb71790430f9d613551af34a891399e09(SimulationIsland cabc32bb1fe9456232d11a3a7adad3555, float ce8c71427a0828475a969c745c8335cde)
	{
		int count = cabc32bb1fe9456232d11a3a7adad3555.ContactConstraintsInternal.Count;
		int count2 = cabc32bb1fe9456232d11a3a7adad3555.ConstraintsInternal.Count;
		if (count == 0)
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
				__ldtoken(c02168253bcb64d2a565d402ed87663bb.cb71790430f9d613551af34a891399e09(SimulationIsland, float));
			}
			if (count2 == 0)
			{
				return;
			}
		}
		float num;
		if (cabc32bb1fe9456232d11a3a7adad3555.RigidBodies.Count >= 6)
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
			num = Simulation.Settings.Constraints.StackingTolerance;
		}
		else
		{
			num = 0f;
		}
		float stackingTolerance = num;
		for (int i = 0; i < count; i++)
		{
			ContactConstraint contactConstraint = cabc32bb1fe9456232d11a3a7adad3555.ContactConstraintsInternal[i];
			contactConstraint.StackingTolerance = stackingTolerance;
			contactConstraint.Setup();
		}
		while (true)
		{
			switch (5)
			{
			case 0:
				continue;
			}
			for (int j = 0; j < count2; j++)
			{
				Constraint constraint = cabc32bb1fe9456232d11a3a7adad3555.ConstraintsInternal[j];
				constraint.Setup();
			}
			cd7a5fd3b8b9c5ca6465ff67f8dd4e160(cabc32bb1fe9456232d11a3a7adad3555);
			int numberOfConstraintIterations = Simulation.Settings.Constraints.NumberOfConstraintIterations;
			int num2 = 0;
			while (num2 < numberOfConstraintIterations)
			{
				bool flag = false;
				for (int k = 0; k < count; k++)
				{
					ContactConstraint contactConstraint2 = cabc32bb1fe9456232d11a3a7adad3555.ContactConstraintsInternal[k];
					bool flag2 = contactConstraint2.ApplyImpulse();
					int num3;
					if (!flag)
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
						num3 = (flag2 ? 1 : 0);
					}
					else
					{
						num3 = 1;
					}
					flag = (byte)num3 != 0;
				}
				for (int l = 0; l < count2; l++)
				{
					Constraint constraint2 = cabc32bb1fe9456232d11a3a7adad3555.ConstraintsInternal[l];
					bool flag3 = constraint2.ApplyImpulse();
					int num4;
					if (!flag)
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
						num4 = (flag3 ? 1 : 0);
					}
					else
					{
						num4 = 1;
					}
					flag = (byte)num4 != 0;
				}
				while (true)
				{
					switch (4)
					{
					case 0:
						continue;
					}
					if (!flag)
					{
						while (true)
						{
							switch (6)
							{
							case 0:
								break;
							default:
								return;
							}
						}
					}
					num2++;
					break;
				}
			}
			return;
		}
	}

	private void cd7a5fd3b8b9c5ca6465ff67f8dd4e160(SimulationIsland cabc32bb1fe9456232d11a3a7adad3555)
	{
		if (!Simulation.Settings.Constraints.RandomizeConstraints)
		{
			return;
		}
		Random random = cabc32bb1fe9456232d11a3a7adad3555.Random;
		if (random == null)
		{
			random = (cabc32bb1fe9456232d11a3a7adad3555.Random = new Random(55711));
		}
		for (int num = cabc32bb1fe9456232d11a3a7adad3555.ContactConstraintsInternal.Count - 1; num >= 1; num--)
		{
			ContactConstraint value = cabc32bb1fe9456232d11a3a7adad3555.ContactConstraintsInternal[num];
			int index = random.NextInteger(0, num);
			cabc32bb1fe9456232d11a3a7adad3555.ContactConstraintsInternal[num] = cabc32bb1fe9456232d11a3a7adad3555.ContactConstraintsInternal[index];
			cabc32bb1fe9456232d11a3a7adad3555.ContactConstraintsInternal[index] = value;
		}
		for (int num2 = cabc32bb1fe9456232d11a3a7adad3555.ConstraintsInternal.Count - 1; num2 >= 1; num2--)
		{
			Constraint value2 = cabc32bb1fe9456232d11a3a7adad3555.ConstraintsInternal[num2];
			int index2 = random.NextInteger(0, num2);
			cabc32bb1fe9456232d11a3a7adad3555.ConstraintsInternal[num2] = cabc32bb1fe9456232d11a3a7adad3555.ConstraintsInternal[index2];
			cabc32bb1fe9456232d11a3a7adad3555.ConstraintsInternal[index2] = value2;
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
				__ldtoken(c02168253bcb64d2a565d402ed87663bb.cd7a5fd3b8b9c5ca6465ff67f8dd4e160(SimulationIsland));
			}
			return;
		}
	}
}
