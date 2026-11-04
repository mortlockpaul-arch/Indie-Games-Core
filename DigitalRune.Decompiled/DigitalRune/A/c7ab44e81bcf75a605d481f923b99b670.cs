using System;
using System.Runtime.CompilerServices;
using DigitalRune;
using DigitalRune.Threading;

namespace A;

internal class c7ab44e81bcf75a605d481f923b99b670 : IWork
{
	private static readonly ResourcePool<c7ab44e81bcf75a605d481f923b99b670> c8aca4a0e3c0073ffb188fde832680587 = new ResourcePool<c7ab44e81bcf75a605d481f923b99b670>(() => new c7ab44e81bcf75a605d481f923b99b670(), null, null);

	[CompilerGenerated]
	private Action c4362892a78f23a2a970e1c993d52ce65;

	[CompilerGenerated]
	private WorkOptions c15bec2390f65d48df265c072da7a6e52;

	[CompilerGenerated]
	private static Func<c7ab44e81bcf75a605d481f923b99b670> c49253b4520c1b7e183dfcc9c426c0283;

	public Action Action
	{
		[CompilerGenerated]
		get
		{
			return c4362892a78f23a2a970e1c993d52ce65;
		}
		[CompilerGenerated]
		set
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
		set
		{
			c15bec2390f65d48df265c072da7a6e52 = value;
		}
	}

	private c7ab44e81bcf75a605d481f923b99b670()
	{
	}

	public static c7ab44e81bcf75a605d481f923b99b670 c2d8ce8b1ccf2b905e3b18f8a5145fe8a()
	{
		return c8aca4a0e3c0073ffb188fde832680587.Obtain();
	}

	public void ca18b9b5faa9851d6355f617787bb5859()
	{
		Action = null;
		c8aca4a0e3c0073ffb188fde832680587.Recycle(this);
	}

	public void DoWork()
	{
		Action();
		c8aca4a0e3c0073ffb188fde832680587.Recycle(this);
	}

	[CompilerGenerated]
	private static c7ab44e81bcf75a605d481f923b99b670 c4bffc38c4054efe6a3f3ff877749c085()
	{
		return new c7ab44e81bcf75a605d481f923b99b670();
	}
}
