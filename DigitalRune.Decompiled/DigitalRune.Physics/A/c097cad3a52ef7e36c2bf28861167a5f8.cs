using System;
using System.Collections.Generic;
using DigitalRune;
using DigitalRune.Collections;
using DigitalRune.Geometry.Collisions;
using DigitalRune.Physics.Constraints;

namespace A;

internal class c097cad3a52ef7e36c2bf28861167a5f8 : CollisionFilter
{
	private readonly Dictionary<Pair<CollisionObject>, List<Constraint>> c719741ad5138547824c9ab491e7ec0f5 = new Dictionary<Pair<CollisionObject>, List<Constraint>>();

	public c097cad3a52ef7e36c2bf28861167a5f8()
	{
	}

	public c097cad3a52ef7e36c2bf28861167a5f8(int c9ebf0ced010fdcda39d87c2c818032c8)
		: base(c9ebf0ced010fdcda39d87c2c818032c8)
	{
	}

	internal void c03d72a32723924cb5b81c585d5df7d9d(Constraint c05b65a62b8880bec8655000b1122cd7f)
	{
		Pair<CollisionObject> key = new Pair<CollisionObject>(c05b65a62b8880bec8655000b1122cd7f.BodyA.CollisionObject, c05b65a62b8880bec8655000b1122cd7f.BodyB.CollisionObject);
		if (!c719741ad5138547824c9ab491e7ec0f5.TryGetValue(key, out var value))
		{
			value = ResourcePools<Constraint>.Lists.Obtain();
			c719741ad5138547824c9ab491e7ec0f5[key] = value;
		}
		if (!value.Contains(c05b65a62b8880bec8655000b1122cd7f))
		{
			value.Add(c05b65a62b8880bec8655000b1122cd7f);
			OnChanged(EventArgs.Empty);
		}
	}

	internal void c0293645f58f4f4f88a5baaf07d85095f(Constraint c05b65a62b8880bec8655000b1122cd7f)
	{
		Pair<CollisionObject> key = new Pair<CollisionObject>(c05b65a62b8880bec8655000b1122cd7f.BodyA.CollisionObject, c05b65a62b8880bec8655000b1122cd7f.BodyB.CollisionObject);
		List<Constraint> list = c719741ad5138547824c9ab491e7ec0f5[key];
		if (list.Count == 1)
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
				__ldtoken(c097cad3a52ef7e36c2bf28861167a5f8.c0293645f58f4f4f88a5baaf07d85095f(Constraint));
			}
			ResourcePools<Constraint>.Lists.Recycle(list);
			c719741ad5138547824c9ab491e7ec0f5.Remove(key);
		}
		else
		{
			list.Remove(c05b65a62b8880bec8655000b1122cd7f);
		}
		OnChanged(EventArgs.Empty);
	}

	public override void Reset()
	{
		using (Dictionary<Pair<CollisionObject>, List<Constraint>>.ValueCollection.Enumerator enumerator = c719741ad5138547824c9ab491e7ec0f5.Values.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				List<Constraint> current = enumerator.Current;
				ResourcePools<Constraint>.Lists.Recycle(current);
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
					__ldtoken(c097cad3a52ef7e36c2bf28861167a5f8.Reset());
				}
				break;
			}
		}
		c719741ad5138547824c9ab491e7ec0f5.Clear();
		base.Reset();
	}

	public override bool Filter(Pair<CollisionObject> pair)
	{
		if (c719741ad5138547824c9ab491e7ec0f5.ContainsKey(pair))
		{
			return false;
		}
		return base.Filter(pair);
	}
}
