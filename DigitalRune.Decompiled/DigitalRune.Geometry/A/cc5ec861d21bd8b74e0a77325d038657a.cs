using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DigitalRune.Collections;
using DigitalRune.Geometry;
using DigitalRune.Geometry.Partitioning;
using DigitalRune.Geometry.Shapes;

namespace A;

internal class cc5ec861d21bd8b74e0a77325d038657a<c96c92d0bb3e5901860b75aee31593453> : BasePartition<c96c92d0bb3e5901860b75aee31593453>
{
	[CompilerGenerated]
	private sealed class ce3e7fb6c616f28903d9bee5f11f3ac67
	{
		public cc5ec861d21bd8b74e0a77325d038657a<c96c92d0bb3e5901860b75aee31593453> c1b0bbfd4af1b6a4c0c71a5dd3c509812;

		public Aabb cdc3439410d15f28489c964ee87b88fe9;

		public bool ca293765e0c2be493b5097dc252e6b893(c96c92d0bb3e5901860b75aee31593453 cf51048ba330b8d3f481ea5bc25a03eb9)
		{
			return GeometryHelper.HaveContact(c1b0bbfd4af1b6a4c0c71a5dd3c509812.GetAabbForItem(cf51048ba330b8d3f481ea5bc25a03eb9), cdc3439410d15f28489c964ee87b88fe9);
		}
	}

	protected override BasePartition<c96c92d0bb3e5901860b75aee31593453> CreateInstanceCore()
	{
		return new cc5ec861d21bd8b74e0a77325d038657a<c96c92d0bb3e5901860b75aee31593453>();
	}

	protected override void CloneCore(BasePartition<c96c92d0bb3e5901860b75aee31593453> sourcePartition)
	{
		base.CloneCore(sourcePartition);
	}

	public override IEnumerable<c96c92d0bb3e5901860b75aee31593453> GetOverlaps(Aabb aabb)
	{
		return c0e601bcddbab14811b8bae6ae4b7b274.Where((c96c92d0bb3e5901860b75aee31593453 cf51048ba330b8d3f481ea5bc25a03eb9) => GeometryHelper.HaveContact(GetAabbForItem(cf51048ba330b8d3f481ea5bc25a03eb9), aabb));
	}

	internal override void ced5f5b0bd8302f3e8ade582954f3650d(bool ce8ca0a645441ac556aaf6b4574dbbb1f, HashSet<c96c92d0bb3e5901860b75aee31593453> c049085d0fafa74bddedd7455b511e4d2, HashSet<c96c92d0bb3e5901860b75aee31593453> c2176039f062c6700652655f4b1223258, HashSet<c96c92d0bb3e5901860b75aee31593453> c7a74c15b192971583f3f676b15968063)
	{
		if (!EnableSelfOverlaps)
		{
			return;
		}
		c65379642bd949dc7e978580b9649b9bc.Clear();
		for (int i = 0; i < c0e601bcddbab14811b8bae6ae4b7b274.Count; i++)
		{
			c96c92d0bb3e5901860b75aee31593453 val = c0e601bcddbab14811b8bae6ae4b7b274[i];
			Aabb aabbA = GetAabbForItem(val);
			for (int j = i + 1; j < c0e601bcddbab14811b8bae6ae4b7b274.Count; j++)
			{
				c96c92d0bb3e5901860b75aee31593453 val2 = c0e601bcddbab14811b8bae6ae4b7b274[j];
				Aabb aabbB = GetAabbForItem(val2);
				Pair<c96c92d0bb3e5901860b75aee31593453> pair = new Pair<c96c92d0bb3e5901860b75aee31593453>(val, val2);
				if (Filter != null)
				{
					while (true)
					{
						switch (1)
						{
						case 0:
							continue;
						}
						break;
					}
					if (1 == 0)
					{
						__ldtoken(cc5ec861d21bd8b74e0a77325d038657a<>.ced5f5b0bd8302f3e8ade582954f3650d(bool, HashSet<c96c92d0bb3e5901860b75aee31593453>, HashSet<c96c92d0bb3e5901860b75aee31593453>, HashSet<c96c92d0bb3e5901860b75aee31593453>));
					}
					if (!Filter.Filter(pair))
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
				}
				if (!GeometryHelper.HaveContact(aabbA, aabbB))
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
				c65379642bd949dc7e978580b9649b9bc.Add(pair);
			}
		}
	}
}
