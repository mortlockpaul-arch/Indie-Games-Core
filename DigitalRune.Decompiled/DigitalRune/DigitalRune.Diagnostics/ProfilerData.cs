using System;
using System.Runtime.CompilerServices;

namespace DigitalRune.Diagnostics;

/// <summary>
///       Stores profiler data.
///       </summary>
public class ProfilerData : INamedObject
{
	private double c24bb09531b30a108a8b01a4e71939a25 = -1.0;

	[CompilerGenerated]
	private string c4562595c3012a609fed551ff9cc45ada;

	[CompilerGenerated]
	private int c7f00b6b375f34a64a37616c1e4df9f76;

	[CompilerGenerated]
	private double c5769353ce7565ebe28f2d915e139f5c0;

	[CompilerGenerated]
	private double c8e2764902448e2b61848f2a531929f31;

	[CompilerGenerated]
	private double c9f120e89b42321cd35c7b8980fa6f77a;

	[CompilerGenerated]
	private double c4223b553ea4671e1d6bcce009a71493a;

	/// <summary>
	///       Gets the name of the profiler data.
	///       </summary>
	public string Name
	{
		[CompilerGenerated]
		get
		{
			return c4562595c3012a609fed551ff9cc45ada;
		}
		[CompilerGenerated]
		private set
		{
			c4562595c3012a609fed551ff9cc45ada = value;
		}
	}

	/// <summary>
	///       Gets the number of measured values.
	///       </summary>
	public int Count
	{
		[CompilerGenerated]
		get
		{
			return c7f00b6b375f34a64a37616c1e4df9f76;
		}
		[CompilerGenerated]
		private set
		{
			c7f00b6b375f34a64a37616c1e4df9f76 = value;
		}
	}

	/// <summary>
	///       Gets the sum of all measured values.
	///       </summary>
	public double Sum
	{
		[CompilerGenerated]
		get
		{
			return c5769353ce7565ebe28f2d915e139f5c0;
		}
		[CompilerGenerated]
		private set
		{
			c5769353ce7565ebe28f2d915e139f5c0 = value;
		}
	}

	/// <summary>
	///       Gets the minimum of all measured values.
	///       </summary>
	public double Minimum
	{
		[CompilerGenerated]
		get
		{
			return c8e2764902448e2b61848f2a531929f31;
		}
		[CompilerGenerated]
		private set
		{
			c8e2764902448e2b61848f2a531929f31 = value;
		}
	}

	/// <summary>
	///       Gets the maximum of all measured values.
	///       </summary>
	public double Maximum
	{
		[CompilerGenerated]
		get
		{
			return c9f120e89b42321cd35c7b8980fa6f77a;
		}
		[CompilerGenerated]
		private set
		{
			c9f120e89b42321cd35c7b8980fa6f77a = value;
		}
	}

	/// <summary>
	///       Gets the average (arithmetic mean) of all measured values.
	///       </summary>
	public double Average => Sum / (double)Count;

	/// <summary>
	///       Gets the last value that was measured.
	///       </summary>
	public double Last
	{
		[CompilerGenerated]
		get
		{
			return c4223b553ea4671e1d6bcce009a71493a;
		}
		[CompilerGenerated]
		private set
		{
			c4223b553ea4671e1d6bcce009a71493a = value;
		}
	}

	internal ProfilerData(string ccfdd5487bfbd01ad9e15d54887d721b6)
	{
		Name = ccfdd5487bfbd01ad9e15d54887d721b6;
		cf06348a19a922c6295282d49671cbe77();
	}

	internal void cf06348a19a922c6295282d49671cbe77()
	{
		Count = 0;
		c24bb09531b30a108a8b01a4e71939a25 = -1.0;
		Sum = double.NaN;
		Minimum = double.NaN;
		Maximum = double.NaN;
		Last = double.NaN;
	}

	internal void c1a1ad206553a863175ada6def4da0863()
	{
		c24bb09531b30a108a8b01a4e71939a25 = Profiler.Stopwatch.Elapsed.TotalSeconds;
	}

	internal void c29a5fa2bae9d5dba9ca9f51e93213ef1()
	{
		double totalSeconds = Profiler.Stopwatch.Elapsed.TotalSeconds;
		c8c9bd9b52f15e30f961df2d97bbf4dd2(totalSeconds - c24bb09531b30a108a8b01a4e71939a25);
		c24bb09531b30a108a8b01a4e71939a25 = -1.0;
	}

	internal void c8c9bd9b52f15e30f961df2d97bbf4dd2(double c8eea6fe5a82bc3c0867f6cf9ae7c0eea)
	{
		if (Count == 0)
		{
			Sum = c8eea6fe5a82bc3c0867f6cf9ae7c0eea;
			Minimum = c8eea6fe5a82bc3c0867f6cf9ae7c0eea;
			Maximum = c8eea6fe5a82bc3c0867f6cf9ae7c0eea;
			Last = c8eea6fe5a82bc3c0867f6cf9ae7c0eea;
		}
		else
		{
			Sum += c8eea6fe5a82bc3c0867f6cf9ae7c0eea;
			Minimum = Math.Min(Minimum, c8eea6fe5a82bc3c0867f6cf9ae7c0eea);
			Maximum = Math.Max(Maximum, c8eea6fe5a82bc3c0867f6cf9ae7c0eea);
			Last = c8eea6fe5a82bc3c0867f6cf9ae7c0eea;
		}
		Count++;
	}
}
