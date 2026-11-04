using System.Threading;

namespace A;

internal class c6e7042cdccec6ea73fbbeffdd84220b7<c96c92d0bb3e5901860b75aee31593453>
{
	private const int c066540c84f40bb90c4a43541c16ae3cc = 32;

	private c96c92d0bb3e5901860b75aee31593453[] c37adb342465be20dcf2c430c7d6aeb68 = new c96c92d0bb3e5901860b75aee31593453[32];

	private int c7d7586a296bbccbae5d5134cf5967ddd = 31;

	private volatile int c376c82fe96a3b4db4f9c7da8116f196c;

	private volatile int cc75bf899386174bb3455b4b7901a8367;

	private readonly object c397c0e7c306d313f8008e7bc5ca4f9ed = new object();

	public bool ce4e3bcec46c899e1009a33b2202cde07 => c376c82fe96a3b4db4f9c7da8116f196c >= cc75bf899386174bb3455b4b7901a8367;

	public int ca2ab9bfbe566d4bbb2f79385a1dc216b => cc75bf899386174bb3455b4b7901a8367 - c376c82fe96a3b4db4f9c7da8116f196c;

	public void cc33e88ad9074c1994dd5af333440ba4b(c96c92d0bb3e5901860b75aee31593453 cf51048ba330b8d3f481ea5bc25a03eb9)
	{
		int num = cc75bf899386174bb3455b4b7901a8367;
		if (num < c376c82fe96a3b4db4f9c7da8116f196c + c7d7586a296bbccbae5d5134cf5967ddd)
		{
			c37adb342465be20dcf2c430c7d6aeb68[num & c7d7586a296bbccbae5d5134cf5967ddd] = cf51048ba330b8d3f481ea5bc25a03eb9;
			cc75bf899386174bb3455b4b7901a8367 = num + 1;
			return;
		}
		lock (c397c0e7c306d313f8008e7bc5ca4f9ed)
		{
			int num2 = c376c82fe96a3b4db4f9c7da8116f196c;
			int num3 = cc75bf899386174bb3455b4b7901a8367 - c376c82fe96a3b4db4f9c7da8116f196c;
			if (num3 >= c7d7586a296bbccbae5d5134cf5967ddd)
			{
				c96c92d0bb3e5901860b75aee31593453[] array = new c96c92d0bb3e5901860b75aee31593453[c37adb342465be20dcf2c430c7d6aeb68.Length << 1];
				for (int i = 0; i < num3; i++)
				{
					array[i] = c37adb342465be20dcf2c430c7d6aeb68[(i + num2) & c7d7586a296bbccbae5d5134cf5967ddd];
				}
				c37adb342465be20dcf2c430c7d6aeb68 = array;
				c376c82fe96a3b4db4f9c7da8116f196c = 0;
				num = (cc75bf899386174bb3455b4b7901a8367 = num3);
				c7d7586a296bbccbae5d5134cf5967ddd = (c7d7586a296bbccbae5d5134cf5967ddd << 1) | 1;
			}
			c37adb342465be20dcf2c430c7d6aeb68[num & c7d7586a296bbccbae5d5134cf5967ddd] = cf51048ba330b8d3f481ea5bc25a03eb9;
			cc75bf899386174bb3455b4b7901a8367 = num + 1;
		}
	}

	public bool c86e260139666e87e89bb356c82b180e4(ref c96c92d0bb3e5901860b75aee31593453 cf51048ba330b8d3f481ea5bc25a03eb9)
	{
		int num = cc75bf899386174bb3455b4b7901a8367;
		if (c376c82fe96a3b4db4f9c7da8116f196c >= num)
		{
			return false;
		}
		num--;
		Interlocked.Exchange(ref cc75bf899386174bb3455b4b7901a8367, num);
		if (c376c82fe96a3b4db4f9c7da8116f196c <= num)
		{
			cf51048ba330b8d3f481ea5bc25a03eb9 = c37adb342465be20dcf2c430c7d6aeb68[num & c7d7586a296bbccbae5d5134cf5967ddd];
			return true;
		}
		lock (c397c0e7c306d313f8008e7bc5ca4f9ed)
		{
			if (c376c82fe96a3b4db4f9c7da8116f196c <= num)
			{
				cf51048ba330b8d3f481ea5bc25a03eb9 = c37adb342465be20dcf2c430c7d6aeb68[num & c7d7586a296bbccbae5d5134cf5967ddd];
				return true;
			}
			cc75bf899386174bb3455b4b7901a8367 = num + 1;
			return false;
		}
	}

	public bool c8866ed16e0b3f3dff2e9c86a99de5823(ref c96c92d0bb3e5901860b75aee31593453 cf51048ba330b8d3f481ea5bc25a03eb9, int cdc0078dd5089488339e8f77b3f19eeba)
	{
		bool flag = false;
		try
		{
			flag = Monitor.TryEnter(c397c0e7c306d313f8008e7bc5ca4f9ed, cdc0078dd5089488339e8f77b3f19eeba);
			if (flag)
			{
				int num = c376c82fe96a3b4db4f9c7da8116f196c;
				Interlocked.Exchange(ref c376c82fe96a3b4db4f9c7da8116f196c, num + 1);
				if (num < cc75bf899386174bb3455b4b7901a8367)
				{
					cf51048ba330b8d3f481ea5bc25a03eb9 = c37adb342465be20dcf2c430c7d6aeb68[num & c7d7586a296bbccbae5d5134cf5967ddd];
					return true;
				}
				c376c82fe96a3b4db4f9c7da8116f196c = num;
				return false;
			}
		}
		finally
		{
			if (flag)
			{
				Monitor.Exit(c397c0e7c306d313f8008e7bc5ca4f9ed);
			}
		}
		return false;
	}
}
