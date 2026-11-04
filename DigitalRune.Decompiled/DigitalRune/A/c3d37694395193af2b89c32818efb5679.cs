using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using DigitalRune;
using DigitalRune.Threading;

namespace A;

internal class c3d37694395193af2b89c32818efb5679
{
	private static readonly Stack<Task> ca672644a1aa5266639889b17690b0cf5 = new Stack<Task>();

	private static readonly object cf6926c8f9726e12f8eb856ac7915863b = new object();

	private static Task? c7708496db46bf53039a3ee68d45d9667;

	private static List<c3d37694395193af2b89c32818efb5679> cd7014727aa9a1e0c4f469884e944b67d;

	private List<Exception> c29c2bbc1f51460d1f7f153fa0bc27981;

	private readonly c70a9f1f6302abae8209fa3bd3ad7722e<int, Exception[]> c8f7de0dfe524133ef8e6c60f848cadf1;

	private readonly ManualResetEvent c5942ead04373b9074312701ddcbb6c94;

	private IWork c4b759cf0f2d3c6a630def65811944ebc;

	private volatile int c46f31c93ce550226aa64672f11907be5;

	private volatile int ce89303509da7816247dea434adba3cb0;

	private readonly List<Task> c6cd7e82e7300a1cee50f2816a6a48ae2;

	private volatile int cb3531e955f3b3b4848c43bfc17884a22;

	private readonly object c26485af1f7944575e9b8affb6720dfb4 = new object();

	private static readonly ResourcePool<c3d37694395193af2b89c32818efb5679> c8aca4a0e3c0073ffb188fde832680587 = new ResourcePool<c3d37694395193af2b89c32818efb5679>(() => new c3d37694395193af2b89c32818efb5679(), null, null);

	private static readonly c70a9f1f6302abae8209fa3bd3ad7722e<Thread, Stack<Task>> c198332ebfca029d87d6b6c361875a566 = new c70a9f1f6302abae8209fa3bd3ad7722e<Thread, Stack<Task>>(Environment.ProcessorCount);

	[CompilerGenerated]
	private Action ce62a30e96ab20b739a3345abdc510ba9;

	[CompilerGenerated]
	private static Func<c3d37694395193af2b89c32818efb5679> c49253b4520c1b7e183dfcc9c426c0283;

	internal static Task? cb0a359a7d5efad04120d80f3bb03d9f1
	{
		get
		{
			bool flag = false;
			try
			{
				flag = Monitor.TryEnter(cf6926c8f9726e12f8eb856ac7915863b);
				if (flag)
				{
					return c7708496db46bf53039a3ee68d45d9667;
				}
				return null;
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(cf6926c8f9726e12f8eb856ac7915863b);
				}
			}
		}
		set
		{
			lock (cf6926c8f9726e12f8eb856ac7915863b)
			{
				ca672644a1aa5266639889b17690b0cf5.Push(value.Value);
				c7708496db46bf53039a3ee68d45d9667 = value.Value;
			}
		}
	}

	internal static List<c3d37694395193af2b89c32818efb5679> ce756549f02d68f19cc5220f66c9420e6
	{
		get
		{
			if (cd7014727aa9a1e0c4f469884e944b67d == null)
			{
				List<c3d37694395193af2b89c32818efb5679> value = new List<c3d37694395193af2b89c32818efb5679>();
				Interlocked.CompareExchange(ref cd7014727aa9a1e0c4f469884e944b67d, value, null);
			}
			return cd7014727aa9a1e0c4f469884e944b67d;
		}
	}

	public int cc9f1f02819bbde2fce810c68e3ce2c95 => c46f31c93ce550226aa64672f11907be5;

	public c70a9f1f6302abae8209fa3bd3ad7722e<int, Exception[]> cf5b9d9552c08f2515ce5863da923e6a9 => c8f7de0dfe524133ef8e6c60f848cadf1;

	public IWork cc2353fa93de25abd681a15853d70f006 => c4b759cf0f2d3c6a630def65811944ebc;

	public Action Callback
	{
		[CompilerGenerated]
		get
		{
			return ce62a30e96ab20b739a3345abdc510ba9;
		}
		[CompilerGenerated]
		set
		{
			ce62a30e96ab20b739a3345abdc510ba9 = value;
		}
	}

	public static Task? ca0c69eef9d50097b82adb19ee5a85f93
	{
		get
		{
			if (c198332ebfca029d87d6b6c361875a566.cfa34cf3f48196497030d8ee48d8e4741(Thread.CurrentThread, out var c64f632f47ec42f89161439dfec6e213c) && c64f632f47ec42f89161439dfec6e213c.Count > 0)
			{
				return c64f632f47ec42f89161439dfec6e213c.Peek();
			}
			return null;
		}
	}

	private c3d37694395193af2b89c32818efb5679()
	{
		c5942ead04373b9074312701ddcbb6c94 = new ManualResetEvent(initialState: true);
		c8f7de0dfe524133ef8e6c60f848cadf1 = new c70a9f1f6302abae8209fa3bd3ad7722e<int, Exception[]>(1);
		c6cd7e82e7300a1cee50f2816a6a48ae2 = new List<Task>();
	}

	internal static void c3d7ba883d287ddb53de46e1983290f62(Task? c65546a8d53dfc41c7ac1e13c9746114b)
	{
		if (!c7708496db46bf53039a3ee68d45d9667.HasValue)
		{
			return;
		}
		if (!c65546a8d53dfc41c7ac1e13c9746114b.HasValue)
		{
			lock (cf6926c8f9726e12f8eb856ac7915863b)
			{
				ca672644a1aa5266639889b17690b0cf5.Clear();
				c7708496db46bf53039a3ee68d45d9667 = null;
				return;
			}
		}
		bool flag = false;
		try
		{
			flag = Monitor.TryEnter(cf6926c8f9726e12f8eb856ac7915863b);
			if (flag && ca672644a1aa5266639889b17690b0cf5.Count > 0)
			{
				Task task = ca672644a1aa5266639889b17690b0cf5.Peek();
				if (task.ID == c65546a8d53dfc41c7ac1e13c9746114b.Value.ID && task.Item == c65546a8d53dfc41c7ac1e13c9746114b.Value.Item)
				{
					ca672644a1aa5266639889b17690b0cf5.Pop();
				}
				if (ca672644a1aa5266639889b17690b0cf5.Count > 0)
				{
					c7708496db46bf53039a3ee68d45d9667 = ca672644a1aa5266639889b17690b0cf5.Peek();
				}
				else
				{
					c7708496db46bf53039a3ee68d45d9667 = null;
				}
			}
		}
		finally
		{
			if (flag)
			{
				Monitor.Exit(cf6926c8f9726e12f8eb856ac7915863b);
			}
		}
	}

	public static c3d37694395193af2b89c32818efb5679 c2d8ce8b1ccf2b905e3b18f8a5145fe8a()
	{
		return c8aca4a0e3c0073ffb188fde832680587.Obtain();
	}

	public Task cf953e1a8d2f36ad649d3d153db383d06(IWork c48534c252a39ff483baefaab1792a5cf)
	{
		c4b759cf0f2d3c6a630def65811944ebc = c48534c252a39ff483baefaab1792a5cf;
		c5942ead04373b9074312701ddcbb6c94.Reset();
		c6cd7e82e7300a1cee50f2816a6a48ae2.Clear();
		c29c2bbc1f51460d1f7f153fa0bc27981 = null;
		Task task = new Task(this);
		Task? task2 = ca0c69eef9d50097b82adb19ee5a85f93;
		if (task2.HasValue && task2.Value.Item == this)
		{
			throw new Exception(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(879));
		}
		if (!c48534c252a39ff483baefaab1792a5cf.Options.DetachFromParent && task2.HasValue)
		{
			task2.Value.Item.ce0e14bf3cccb72269976b77b5a5358a8(task);
		}
		return task;
	}

	public bool cbca98f742f4d19c763f8dabed3e87233(int cc4980351641fde3eb0017719c9b71323)
	{
		lock (c26485af1f7944575e9b8affb6720dfb4)
		{
			if (cc4980351641fde3eb0017719c9b71323 < c46f31c93ce550226aa64672f11907be5)
			{
				return true;
			}
			if (ce89303509da7816247dea434adba3cb0 == c4b759cf0f2d3c6a630def65811944ebc.Options.MaximumThreads)
			{
				return false;
			}
			ce89303509da7816247dea434adba3cb0++;
		}
		if (!c198332ebfca029d87d6b6c361875a566.cfa34cf3f48196497030d8ee48d8e4741(Thread.CurrentThread, out var c64f632f47ec42f89161439dfec6e213c))
		{
			c64f632f47ec42f89161439dfec6e213c = new Stack<Task>();
			c198332ebfca029d87d6b6c361875a566.c03d72a32723924cb5b81c585d5df7d9d(Thread.CurrentThread, c64f632f47ec42f89161439dfec6e213c);
		}
		c64f632f47ec42f89161439dfec6e213c.Push(new Task(this));
		try
		{
			c4b759cf0f2d3c6a630def65811944ebc.DoWork();
		}
		catch (Exception item)
		{
			if (c29c2bbc1f51460d1f7f153fa0bc27981 == null)
			{
				List<Exception> value = new List<Exception>();
				Interlocked.CompareExchange(ref c29c2bbc1f51460d1f7f153fa0bc27981, value, null);
			}
			lock (c29c2bbc1f51460d1f7f153fa0bc27981)
			{
				c29c2bbc1f51460d1f7f153fa0bc27981.Add(item);
			}
		}
		c64f632f47ec42f89161439dfec6e213c.Pop();
		lock (c26485af1f7944575e9b8affb6720dfb4)
		{
			ce89303509da7816247dea434adba3cb0--;
			if (ce89303509da7816247dea434adba3cb0 == 0)
			{
				if (c29c2bbc1f51460d1f7f153fa0bc27981 != null)
				{
					c8f7de0dfe524133ef8e6c60f848cadf1.c03d72a32723924cb5b81c585d5df7d9d(c46f31c93ce550226aa64672f11907be5, c29c2bbc1f51460d1f7f153fa0bc27981.ToArray());
				}
				foreach (Task item2 in c6cd7e82e7300a1cee50f2816a6a48ae2)
				{
					item2.Wait();
				}
				c46f31c93ce550226aa64672f11907be5++;
				c5942ead04373b9074312701ddcbb6c94.Set();
				while (cb3531e955f3b3b4848c43bfc17884a22 > 0)
				{
				}
				if (Callback == null)
				{
					ca18b9b5faa9851d6355f617787bb5859();
				}
				else
				{
					lock (ce756549f02d68f19cc5220f66c9420e6)
					{
						ce756549f02d68f19cc5220f66c9420e6.Add(this);
					}
				}
				return true;
			}
			return false;
		}
	}

	public void ca18b9b5faa9851d6355f617787bb5859()
	{
		if (c46f31c93ce550226aa64672f11907be5 < int.MaxValue && c29c2bbc1f51460d1f7f153fa0bc27981 == null)
		{
			Callback = null;
			c8aca4a0e3c0073ffb188fde832680587.Recycle(this);
		}
	}

	public void c334ab49796744460921f9725866ddf75(int c7dad665870c39748dfa1df28b07cc92d)
	{
		c7389e2f3ab5e5630fbe533818bdd7f36(c7dad665870c39748dfa1df28b07cc92d);
		if (c8f7de0dfe524133ef8e6c60f848cadf1.cfa34cf3f48196497030d8ee48d8e4741(c7dad665870c39748dfa1df28b07cc92d, out var c64f632f47ec42f89161439dfec6e213c))
		{
			throw new TaskException(c64f632f47ec42f89161439dfec6e213c);
		}
	}

	private void c7389e2f3ab5e5630fbe533818bdd7f36(int c7dad665870c39748dfa1df28b07cc92d)
	{
		if (c46f31c93ce550226aa64672f11907be5 != c7dad665870c39748dfa1df28b07cc92d || cbca98f742f4d19c763f8dabed3e87233(c7dad665870c39748dfa1df28b07cc92d))
		{
			return;
		}
		try
		{
			Interlocked.Increment(ref cb3531e955f3b3b4848c43bfc17884a22);
			while (c46f31c93ce550226aa64672f11907be5 == c7dad665870c39748dfa1df28b07cc92d)
			{
				c5942ead04373b9074312701ddcbb6c94.WaitOne();
			}
		}
		finally
		{
			Interlocked.Decrement(ref cb3531e955f3b3b4848c43bfc17884a22);
		}
	}

	public void ce0e14bf3cccb72269976b77b5a5358a8(Task cf51048ba330b8d3f481ea5bc25a03eb9)
	{
		lock (c26485af1f7944575e9b8affb6720dfb4)
		{
			c6cd7e82e7300a1cee50f2816a6a48ae2.Add(cf51048ba330b8d3f481ea5bc25a03eb9);
		}
	}

	[CompilerGenerated]
	private static c3d37694395193af2b89c32818efb5679 c4bffc38c4054efe6a3f3ff877749c085()
	{
		return new c3d37694395193af2b89c32818efb5679();
	}
}
