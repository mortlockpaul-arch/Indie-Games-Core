using System;
using System.Runtime.CompilerServices;
using System.Threading;
using DigitalRune;
using DigitalRune.Threading;

namespace A;

internal class c7aa9c62bb0a67c16dc365891d9fd830d : IWork
{
	private static readonly ResourcePool<c7aa9c62bb0a67c16dc365891d9fd830d> c8aca4a0e3c0073ffb188fde832680587 = new ResourcePool<c7aa9c62bb0a67c16dc365891d9fd830d>(() => new c7aa9c62bb0a67c16dc365891d9fd830d(), null, null);

	private int c69889266b4638bd1b42bfce250e9714e;

	private int c6190c24c75a0d046f6534476b87fd90d;

	private volatile int c2a3d603b430e66707dc56b455d6355a5;

	[CompilerGenerated]
	private Action<int> c4362892a78f23a2a970e1c993d52ce65;

	[CompilerGenerated]
	private WorkOptions c15bec2390f65d48df265c072da7a6e52;

	[CompilerGenerated]
	private static Func<c7aa9c62bb0a67c16dc365891d9fd830d> c2cd722d7de416d829aaf20a315c6553b;

	public Action<int> Action
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

	private c7aa9c62bb0a67c16dc365891d9fd830d()
	{
		Options = new WorkOptions
		{
			MaximumThreads = int.MaxValue
		};
	}

	public static c7aa9c62bb0a67c16dc365891d9fd830d c2d8ce8b1ccf2b905e3b18f8a5145fe8a()
	{
		return c8aca4a0e3c0073ffb188fde832680587.Obtain();
	}

	public void ca18b9b5faa9851d6355f617787bb5859()
	{
		Action = null;
		c8aca4a0e3c0073ffb188fde832680587.Recycle(this);
	}

	public void cb6e3dcfcb9d1bf53e27a1c4bc4921edb(Action<int> c7ec5510c81d856b30fa7939fc87e254e, int c50aacda77af97976e9cd34f620b03392, int c77ca4b555f195f4c9f88a69cf897ab3f, int cf5605c7bb517d6b0560c748edcb9cccd)
	{
		Action = c7ec5510c81d856b30fa7939fc87e254e;
		c2a3d603b430e66707dc56b455d6355a5 = c50aacda77af97976e9cd34f620b03392;
		c69889266b4638bd1b42bfce250e9714e = c77ca4b555f195f4c9f88a69cf897ab3f;
		c6190c24c75a0d046f6534476b87fd90d = cf5605c7bb517d6b0560c748edcb9cccd;
	}

	public void DoWork()
	{
		for (int num = c7357fb76001b06b71419cea7b4886f62(); num < c69889266b4638bd1b42bfce250e9714e; num = c7357fb76001b06b71419cea7b4886f62())
		{
			int num2 = Math.Min(num + c6190c24c75a0d046f6534476b87fd90d, c69889266b4638bd1b42bfce250e9714e);
			for (int i = num; i < num2; i++)
			{
				Action(i);
			}
		}
	}

	private int c7357fb76001b06b71419cea7b4886f62()
	{
		int num;
		do
		{
			num = c2a3d603b430e66707dc56b455d6355a5;
		}
		while (Interlocked.CompareExchange(ref c2a3d603b430e66707dc56b455d6355a5, num + c6190c24c75a0d046f6534476b87fd90d, num) != num);
		return num;
	}

	[CompilerGenerated]
	private static c7aa9c62bb0a67c16dc365891d9fd830d c4c5cc4eb6785fd907a1974db5b42eeb5()
	{
		return new c7aa9c62bb0a67c16dc365891d9fd830d();
	}
}
