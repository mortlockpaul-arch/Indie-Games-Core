using System;
using System.Runtime.CompilerServices;
using DigitalRune;

namespace A;

internal class cc308d98a84a8563af043b321600339a2<c96c92d0bb3e5901860b75aee31593453>
{
	private static readonly ResourcePool<cc308d98a84a8563af043b321600339a2<c96c92d0bb3e5901860b75aee31593453>> c8aca4a0e3c0073ffb188fde832680587;

	[CompilerGenerated]
	private Func<c96c92d0bb3e5901860b75aee31593453, c96c92d0bb3e5901860b75aee31593453, float> c93116eca98fbd029925ed11a7e1dccaf;

	[CompilerGenerated]
	private c96c92d0bb3e5901860b75aee31593453 c902ff321a0f3b86bc2e1f440fc4250df;

	[CompilerGenerated]
	private static Func<cc308d98a84a8563af043b321600339a2<c96c92d0bb3e5901860b75aee31593453>> c49253b4520c1b7e183dfcc9c426c0283;

	public Func<c96c92d0bb3e5901860b75aee31593453, c96c92d0bb3e5901860b75aee31593453, float> OriginalCallback
	{
		[CompilerGenerated]
		get
		{
			return c93116eca98fbd029925ed11a7e1dccaf;
		}
		[CompilerGenerated]
		set
		{
			c93116eca98fbd029925ed11a7e1dccaf = value;
		}
	}

	public c96c92d0bb3e5901860b75aee31593453 Item
	{
		[CompilerGenerated]
		get
		{
			return c902ff321a0f3b86bc2e1f440fc4250df;
		}
		[CompilerGenerated]
		set
		{
			c902ff321a0f3b86bc2e1f440fc4250df = value;
		}
	}

	private cc308d98a84a8563af043b321600339a2()
	{
	}

	static cc308d98a84a8563af043b321600339a2()
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
				__ldtoken(cc308d98a84a8563af043b321600339a2<>._002Ecctor());
			}
			c49253b4520c1b7e183dfcc9c426c0283 = () => new cc308d98a84a8563af043b321600339a2<c96c92d0bb3e5901860b75aee31593453>();
		}
		c8aca4a0e3c0073ffb188fde832680587 = new ResourcePool<cc308d98a84a8563af043b321600339a2<c96c92d0bb3e5901860b75aee31593453>>(c49253b4520c1b7e183dfcc9c426c0283, null, null);
	}

	public static cc308d98a84a8563af043b321600339a2<c96c92d0bb3e5901860b75aee31593453> c2d8ce8b1ccf2b905e3b18f8a5145fe8a()
	{
		return c8aca4a0e3c0073ffb188fde832680587.Obtain();
	}

	public void ca18b9b5faa9851d6355f617787bb5859()
	{
		OriginalCallback = null;
		Item = default;
		c8aca4a0e3c0073ffb188fde832680587.Recycle(this);
	}

	public float c6516654248581f959063ea51406f708b(c96c92d0bb3e5901860b75aee31593453 c61ae56037f91c064782b7ed1f01aa199)
	{
		return OriginalCallback(Item, c61ae56037f91c064782b7ed1f01aa199);
	}

	[CompilerGenerated]
	private static cc308d98a84a8563af043b321600339a2<c96c92d0bb3e5901860b75aee31593453> c4bffc38c4054efe6a3f3ff877749c085()
	{
		return new cc308d98a84a8563af043b321600339a2<c96c92d0bb3e5901860b75aee31593453>();
	}
}
