using System.Collections.Generic;
using System.Threading;
using DigitalRune.Threading;

namespace A;

internal class cff580634e72e633642f7196e22f3eb60
{
	private static readonly Stack<cff580634e72e633642f7196e22f3eb60> cf9fc3b081e2edbf4e90ab5dc8f1a99f3 = new Stack<cff580634e72e633642f7196e22f3eb60>();

	private readonly Thread cd3505178d02288a09139a8df5058d36f;

	private readonly AutoResetEvent c5942ead04373b9074312701ddcbb6c94;

	private Task c4b759cf0f2d3c6a630def65811944ebc;

	private cff580634e72e633642f7196e22f3eb60()
	{
		c5942ead04373b9074312701ddcbb6c94 = new AutoResetEvent(initialState: false);
		cd3505178d02288a09139a8df5058d36f = new Thread(c2d0da763c84ff0800aff0b907caf95ae);
		cd3505178d02288a09139a8df5058d36f.IsBackground = true;
		cd3505178d02288a09139a8df5058d36f.Start();
	}

	private void c2d0da763c84ff0800aff0b907caf95ae()
	{
		while (true)
		{
			c5942ead04373b9074312701ddcbb6c94.WaitOne();
			c4b759cf0f2d3c6a630def65811944ebc.cbca98f742f4d19c763f8dabed3e87233();
			lock (cf9fc3b081e2edbf4e90ab5dc8f1a99f3)
			{
				cf9fc3b081e2edbf4e90ab5dc8f1a99f3.Push(this);
			}
		}
	}

	private void c1a1ad206553a863175ada6def4da0863(Task c48534c252a39ff483baefaab1792a5cf)
	{
		c4b759cf0f2d3c6a630def65811944ebc = c48534c252a39ff483baefaab1792a5cf;
		c5942ead04373b9074312701ddcbb6c94.Set();
	}

	public static void c60f2d76c17ab5bddce54f7edea984a95(Task c48534c252a39ff483baefaab1792a5cf)
	{
		cff580634e72e633642f7196e22f3eb60 cff580634e72e633642f7196e22f3eb61 = null;
		if (cf9fc3b081e2edbf4e90ab5dc8f1a99f3.Count > 0)
		{
			lock (cf9fc3b081e2edbf4e90ab5dc8f1a99f3)
			{
				if (cf9fc3b081e2edbf4e90ab5dc8f1a99f3.Count > 0)
				{
					cff580634e72e633642f7196e22f3eb61 = cf9fc3b081e2edbf4e90ab5dc8f1a99f3.Pop();
				}
			}
		}
		if (cff580634e72e633642f7196e22f3eb61 == null)
		{
			cff580634e72e633642f7196e22f3eb61 = new cff580634e72e633642f7196e22f3eb60();
		}
		cff580634e72e633642f7196e22f3eb61.c1a1ad206553a863175ada6def4da0863(c48534c252a39ff483baefaab1792a5cf);
	}
}
