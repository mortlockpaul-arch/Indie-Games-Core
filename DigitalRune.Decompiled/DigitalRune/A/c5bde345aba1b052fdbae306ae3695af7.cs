using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DigitalRune;
using DigitalRune.Threading;

namespace A;

internal class c5bde345aba1b052fdbae306ae3695af7<c96c92d0bb3e5901860b75aee31593453> : IWork
{
	private static ResourcePool<c5bde345aba1b052fdbae306ae3695af7<c96c92d0bb3e5901860b75aee31593453>> c8aca4a0e3c0073ffb188fde832680587 = new ResourcePool<c5bde345aba1b052fdbae306ae3695af7<c96c92d0bb3e5901860b75aee31593453>>(() => new c5bde345aba1b052fdbae306ae3695af7<c96c92d0bb3e5901860b75aee31593453>(), null, null);

	private IEnumerator<c96c92d0bb3e5901860b75aee31593453> c8e8eedc3a921a9dc10958e22537bbcb1;

	private volatile bool c683625e99c1f2978760c25e42b1c4497;

	private readonly object c9eec6abc67031b77e3eb4373d31bc2b5 = new object();

	[CompilerGenerated]
	private Action<c96c92d0bb3e5901860b75aee31593453> c4362892a78f23a2a970e1c993d52ce65;

	[CompilerGenerated]
	private WorkOptions c15bec2390f65d48df265c072da7a6e52;

	[CompilerGenerated]
	private static Func<c5bde345aba1b052fdbae306ae3695af7<c96c92d0bb3e5901860b75aee31593453>> c2cd722d7de416d829aaf20a315c6553b;

	public Action<c96c92d0bb3e5901860b75aee31593453> Action
	{
		[CompilerGenerated]
		get
		{
			return c4362892a78f23a2a970e1c993d52ce65;
		}
		[CompilerGenerated]
		private set
		{
			c4362892a78f23a2a970e1c993d52ce65 = value;
		}
	}

	public WorkOptions Options
	{
		[CompilerGenerated]
		get
		{
			return c15bec2390f65d48df265c072da7a6e52;
		}
		[CompilerGenerated]
		private set
		{
			c15bec2390f65d48df265c072da7a6e52 = value;
		}
	}

	private c5bde345aba1b052fdbae306ae3695af7()
	{
		Options = new WorkOptions
		{
			MaximumThreads = int.MaxValue
		};
	}

	public static c5bde345aba1b052fdbae306ae3695af7<c96c92d0bb3e5901860b75aee31593453> c2d8ce8b1ccf2b905e3b18f8a5145fe8a()
	{
		return c8aca4a0e3c0073ffb188fde832680587.Obtain();
	}

	public void ca18b9b5faa9851d6355f617787bb5859()
	{
		Action = null;
		c8aca4a0e3c0073ffb188fde832680587.Recycle(this);
	}

	public void cb6e3dcfcb9d1bf53e27a1c4bc4921edb(Action<c96c92d0bb3e5901860b75aee31593453> c7ec5510c81d856b30fa7939fc87e254e, IEnumerator<c96c92d0bb3e5901860b75aee31593453> c0ed8d052981f9d1ca0aacf2e8e1e3c12)
	{
		Action = c7ec5510c81d856b30fa7939fc87e254e;
		c8e8eedc3a921a9dc10958e22537bbcb1 = c0ed8d052981f9d1ca0aacf2e8e1e3c12;
		c683625e99c1f2978760c25e42b1c4497 = true;
	}

	public void DoWork()
	{
		c96c92d0bb3e5901860b75aee31593453 obj = default;
		while (c683625e99c1f2978760c25e42b1c4497)
		{
			bool flag = false;
			lock (c9eec6abc67031b77e3eb4373d31bc2b5)
			{
				c683625e99c1f2978760c25e42b1c4497 = c8e8eedc3a921a9dc10958e22537bbcb1.MoveNext();
				if (c683625e99c1f2978760c25e42b1c4497)
				{
					obj = c8e8eedc3a921a9dc10958e22537bbcb1.Current;
					flag = true;
				}
			}
			if (flag)
			{
				Action(obj);
			}
		}
	}

	[CompilerGenerated]
	private static c5bde345aba1b052fdbae306ae3695af7<c96c92d0bb3e5901860b75aee31593453> c4c5cc4eb6785fd907a1974db5b42eeb5()
	{
		return new c5bde345aba1b052fdbae306ae3695af7<c96c92d0bb3e5901860b75aee31593453>();
	}
}
