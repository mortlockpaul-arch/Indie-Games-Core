using System;
using System.Threading;

namespace A;

internal struct c1895de254ebbe9cb91fe0418007c7c26
{
	private Thread c0f0a483db7eb46fd79347bd275665950;

	private int c90ea6721c7045e0069157ccc78be534f;

	public void ca45e58c22f5934bf30e51f80b31fc58b()
	{
		Thread currentThread = Thread.CurrentThread;
		if (c0f0a483db7eb46fd79347bd275665950 == currentThread)
		{
			Interlocked.Increment(ref c90ea6721c7045e0069157ccc78be534f);
			return;
		}
		while (Interlocked.CompareExchange(ref c0f0a483db7eb46fd79347bd275665950, currentThread, null) != null)
		{
		}
		Interlocked.Increment(ref c90ea6721c7045e0069157ccc78be534f);
	}

	public bool c7384fbd4429e574a96cccd37d8d295b4()
	{
		Thread currentThread = Thread.CurrentThread;
		if (c0f0a483db7eb46fd79347bd275665950 == currentThread)
		{
			Interlocked.Increment(ref c90ea6721c7045e0069157ccc78be534f);
			return true;
		}
		bool flag = Interlocked.CompareExchange(ref c0f0a483db7eb46fd79347bd275665950, currentThread, null) == null;
		if (flag)
		{
			Interlocked.Increment(ref c90ea6721c7045e0069157ccc78be534f);
		}
		return flag;
	}

	public bool c7384fbd4429e574a96cccd37d8d295b4(TimeSpan ca8b4c99949728727abaec0bbac23d77c)
	{
		DateTime now = DateTime.Now;
		Thread currentThread = Thread.CurrentThread;
		if (c0f0a483db7eb46fd79347bd275665950 == currentThread)
		{
			Interlocked.Increment(ref c90ea6721c7045e0069157ccc78be534f);
			return true;
		}
		while (Interlocked.CompareExchange(ref c0f0a483db7eb46fd79347bd275665950, currentThread, null) != null)
		{
			if (DateTime.Now - now > ca8b4c99949728727abaec0bbac23d77c)
			{
				return false;
			}
		}
		Interlocked.Increment(ref c90ea6721c7045e0069157ccc78be534f);
		return true;
	}

	public void c2e949f29d1f93161447a38cd6f450556()
	{
		Thread currentThread = Thread.CurrentThread;
		if (currentThread == c0f0a483db7eb46fd79347bd275665950)
		{
			Interlocked.Decrement(ref c90ea6721c7045e0069157ccc78be534f);
			if (c90ea6721c7045e0069157ccc78be534f == 0)
			{
				c0f0a483db7eb46fd79347bd275665950 = null;
			}
			return;
		}
		throw new InvalidOperationException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(442));
	}
}
