using System;
using System.Threading;

namespace y;

internal class _u0013_
{
	private int l8a;

	internal _u0013_()
	{
		l8a = Thread.CurrentThread.ManagedThreadId;
	}

	internal void wb()
	{
		if (l8a != Thread.CurrentThread.ManagedThreadId)
		{
			throw new Exception("Calls to object cannot be made from threads other than the one that created it.");
		}
	}
}
