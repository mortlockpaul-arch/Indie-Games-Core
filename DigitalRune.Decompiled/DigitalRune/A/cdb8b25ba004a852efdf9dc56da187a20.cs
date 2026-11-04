using System;
using System.Runtime.CompilerServices;
using DigitalRune;
using DigitalRune.Threading;

namespace A;

internal class cdb8b25ba004a852efdf9dc56da187a20<c96c92d0bb3e5901860b75aee31593453> : IWork
{
	private static readonly ResourcePool<cdb8b25ba004a852efdf9dc56da187a20<c96c92d0bb3e5901860b75aee31593453>> c8aca4a0e3c0073ffb188fde832680587 = new ResourcePool<cdb8b25ba004a852efdf9dc56da187a20<c96c92d0bb3e5901860b75aee31593453>>(() => new cdb8b25ba004a852efdf9dc56da187a20<c96c92d0bb3e5901860b75aee31593453>(), null, null);

	[CompilerGenerated]
	private int c81e01eb94ded2de1d28abbfff38dcf9e;

	[CompilerGenerated]
	private WorkOptions c15bec2390f65d48df265c072da7a6e52;

	[CompilerGenerated]
	private Func<c96c92d0bb3e5901860b75aee31593453> c80ff9ef51b94af980a78c423a644d183;

	[CompilerGenerated]
	private c96c92d0bb3e5901860b75aee31593453 cc24185cdf3f632292d776522eef90d9a;

	[CompilerGenerated]
	private static Func<cdb8b25ba004a852efdf9dc56da187a20<c96c92d0bb3e5901860b75aee31593453>> c49253b4520c1b7e183dfcc9c426c0283;

	public int ID
	{
		[CompilerGenerated]
		get
		{
			return c81e01eb94ded2de1d28abbfff38dcf9e;
		}
		[CompilerGenerated]
		private set
		{
			c81e01eb94ded2de1d28abbfff38dcf9e = value;
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

	public Func<c96c92d0bb3e5901860b75aee31593453> Function
	{
		[CompilerGenerated]
		get
		{
			return c80ff9ef51b94af980a78c423a644d183;
		}
		[CompilerGenerated]
		set
		{
			c80ff9ef51b94af980a78c423a644d183 = value;
		}
	}

	public c96c92d0bb3e5901860b75aee31593453 Result
	{
		[CompilerGenerated]
		get
		{
			return cc24185cdf3f632292d776522eef90d9a;
		}
		[CompilerGenerated]
		set
		{
			cc24185cdf3f632292d776522eef90d9a = value;
		}
	}

	private cdb8b25ba004a852efdf9dc56da187a20()
	{
	}

	public static cdb8b25ba004a852efdf9dc56da187a20<c96c92d0bb3e5901860b75aee31593453> c2d8ce8b1ccf2b905e3b18f8a5145fe8a()
	{
		return c8aca4a0e3c0073ffb188fde832680587.Obtain();
	}

	public void ca18b9b5faa9851d6355f617787bb5859()
	{
		if (ID < int.MaxValue)
		{
			ID++;
			Function = null;
			Result = default;
			c8aca4a0e3c0073ffb188fde832680587.Recycle(this);
		}
	}

	public void DoWork()
	{
		Result = Function();
	}

	[CompilerGenerated]
	private static cdb8b25ba004a852efdf9dc56da187a20<c96c92d0bb3e5901860b75aee31593453> c4bffc38c4054efe6a3f3ff877749c085()
	{
		return new cdb8b25ba004a852efdf9dc56da187a20<c96c92d0bb3e5901860b75aee31593453>();
	}
}
