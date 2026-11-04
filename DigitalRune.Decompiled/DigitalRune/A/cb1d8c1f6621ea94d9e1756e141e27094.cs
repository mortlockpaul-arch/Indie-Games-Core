using System;
using System.Runtime.CompilerServices;
using System.Threading;
using DigitalRune.Threading;

namespace A;

internal class cb1d8c1f6621ea94d9e1756e141e27094
{
	private static readonly c70a9f1f6302abae8209fa3bd3ad7722e<Thread, cb1d8c1f6621ea94d9e1756e141e27094> c92fd4f32079ac4a5b455a105cef7eb4c = new c70a9f1f6302abae8209fa3bd3ad7722e<Thread, cb1d8c1f6621ea94d9e1756e141e27094>(Environment.ProcessorCount);

	private static int c63feac06efb4d991c4195b78156f1a22;

	private readonly Thread cd3505178d02288a09139a8df5058d36f;

	private readonly c6e7042cdccec6ea73fbbeffdd84220b7<Task> c4dfcbc23665dbf014b820c370f9e6893;

	private readonly WorkStealingScheduler c5ff39d39633371910a19c587706712e8;

	[CompilerGenerated]
	private AutoResetEvent cbbd7b9e5c9ca4347a0103c68628961b5;

	public AutoResetEvent Gate
	{
		[CompilerGenerated]
		get
		{
			return cbbd7b9e5c9ca4347a0103c68628961b5;
		}
		[CompilerGenerated]
		private set
		{
			cbbd7b9e5c9ca4347a0103c68628961b5 = value;
		}
	}

	public static cb1d8c1f6621ea94d9e1756e141e27094 c69766249e84c6705ec5d3193204d0e92
	{
		get
		{
			Thread currentThread = Thread.CurrentThread;
			if (c92fd4f32079ac4a5b455a105cef7eb4c.cfa34cf3f48196497030d8ee48d8e4741(currentThread, out var c64f632f47ec42f89161439dfec6e213c))
			{
				return c64f632f47ec42f89161439dfec6e213c;
			}
			return null;
		}
	}

	public cb1d8c1f6621ea94d9e1756e141e27094(WorkStealingScheduler c5b4a87154c130968d216096e61ed0809, int c88a85a6fc8e0a66e19d046d4d51cd3f1)
	{
		cd3505178d02288a09139a8df5058d36f = new Thread(cc2353fa93de25abd681a15853d70f006)
		{
			Name = c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(3223) + c88a85a6fc8e0a66e19d046d4d51cd3f1,
			IsBackground = true
		};
		c4dfcbc23665dbf014b820c370f9e6893 = new c6e7042cdccec6ea73fbbeffdd84220b7<Task>();
		c5ff39d39633371910a19c587706712e8 = c5b4a87154c130968d216096e61ed0809;
		Gate = new AutoResetEvent(initialState: false);
		c92fd4f32079ac4a5b455a105cef7eb4c.c03d72a32723924cb5b81c585d5df7d9d(cd3505178d02288a09139a8df5058d36f, this);
	}

	public void c1a1ad206553a863175ada6def4da0863()
	{
		cd3505178d02288a09139a8df5058d36f.Start();
	}

	public void c42af5e0c989cbf8f2e017442f350ccbc(Task c65546a8d53dfc41c7ac1e13c9746114b)
	{
		c4dfcbc23665dbf014b820c370f9e6893.cc33e88ad9074c1994dd5af333440ba4b(c65546a8d53dfc41c7ac1e13c9746114b);
	}

	private void cc2353fa93de25abd681a15853d70f006()
	{
		int num = Interlocked.Increment(ref c63feac06efb4d991c4195b78156f1a22) - 1;
		int num2 = Parallel.ProcessorAffinity[num % Parallel.ProcessorAffinity.Length];
		Thread.CurrentThread.SetProcessorAffinity(new int[1] { num2 });
		Task cf51048ba330b8d3f481ea5bc25a03eb = default;
		while (true)
		{
			if (c4dfcbc23665dbf014b820c370f9e6893.c86e260139666e87e89bb356c82b180e4(ref cf51048ba330b8d3f481ea5bc25a03eb))
			{
				cf51048ba330b8d3f481ea5bc25a03eb.cbca98f742f4d19c763f8dabed3e87233();
			}
			else
			{
				c7291b213b5bbc5af4c0d77ff4ca8b791();
			}
		}
	}

	private void c7291b213b5bbc5af4c0d77ff4ca8b791()
	{
		Task c65546a8d53dfc41c7ac1e13c9746114b = default;
		bool flag = false;
		do
		{
			if (c5ff39d39633371910a19c587706712e8.c0dec5417fc0d42950205fe7afacd87d4(ref c65546a8d53dfc41c7ac1e13c9746114b))
			{
				flag = true;
				break;
			}
			Task? cb0a359a7d5efad04120d80f3bb03d9f = c3d37694395193af2b89c32818efb5679.cb0a359a7d5efad04120d80f3bb03d9f1;
			if (cb0a359a7d5efad04120d80f3bb03d9f.HasValue)
			{
				cb0a359a7d5efad04120d80f3bb03d9f.Value.cbca98f742f4d19c763f8dabed3e87233();
				c3d37694395193af2b89c32818efb5679.c3d7ba883d287ddb53de46e1983290f62(cb0a359a7d5efad04120d80f3bb03d9f);
				continue;
			}
			int count = c5ff39d39633371910a19c587706712e8.Workers.Count;
			for (int i = 0; i < count; i++)
			{
				cb1d8c1f6621ea94d9e1756e141e27094 cb1d8c1f6621ea94d9e1756e141e27095 = c5ff39d39633371910a19c587706712e8.Workers[i];
				if (cb1d8c1f6621ea94d9e1756e141e27095 != this && cb1d8c1f6621ea94d9e1756e141e27095.c4dfcbc23665dbf014b820c370f9e6893.c8866ed16e0b3f3dff2e9c86a99de5823(ref c65546a8d53dfc41c7ac1e13c9746114b, 0))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				Gate.WaitOne();
			}
		}
		while (!flag);
		c4dfcbc23665dbf014b820c370f9e6893.cc33e88ad9074c1994dd5af333440ba4b(c65546a8d53dfc41c7ac1e13c9746114b);
	}
}
