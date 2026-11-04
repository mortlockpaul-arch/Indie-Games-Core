using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using A;

namespace DigitalRune.Diagnostics;

/// <summary>
///        Provides support for simple, non-hierarchical profiling.
///        </summary>
public static class Profiler
{
	[CompilerGenerated]
	private sealed class c81332e61de7cb6ffb3429f92d6203bc1 : IEnumerable<ProfilerDataCollection>, IEnumerator<ProfilerDataCollection>, IEnumerable, IEnumerator, IDisposable
	{
		private ProfilerDataCollection cd179d7f18da070d164af6afcb0af45b5;

		private int c94f69126adc708dfc5f5fd5b63c52835;

		private int c0e8a058e779916e7715d7616fb0da9e7;

		public KeyValuePair<int, ProfilerDataCollection> c0d0e4640568eb5e07c8fe18580d4cbca;

		public c70a9f1f6302abae8209fa3bd3ad7722e<int, ProfilerDataCollection>.c27bac404a3ce140128437624b882afb4 c81bc24b01874c03464f508899a23b019;

		ProfilerDataCollection IEnumerator<ProfilerDataCollection>.Current
		{
			[DebuggerHidden]
			get
			{
				return cd179d7f18da070d164af6afcb0af45b5;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return cd179d7f18da070d164af6afcb0af45b5;
			}
		}

		[DebuggerHidden]
		public c81332e61de7cb6ffb3429f92d6203bc1(int c94f69126adc708dfc5f5fd5b63c52835)
		{
			this.c94f69126adc708dfc5f5fd5b63c52835 = c94f69126adc708dfc5f5fd5b63c52835;
			c0e8a058e779916e7715d7616fb0da9e7 = Thread.CurrentThread.ManagedThreadId;
		}

		[DebuggerHidden]
		private IEnumerator<ProfilerDataCollection> c40f7b58a7f3848047e1d829b0ec245ac()
		{
			if (Thread.CurrentThread.ManagedThreadId == c0e8a058e779916e7715d7616fb0da9e7 && c94f69126adc708dfc5f5fd5b63c52835 == -2)
			{
				while (true)
				{
					switch (3)
					{
					case 0:
						break;
					default:
						if (1 == 0)
						{
							__ldtoken(c81332e61de7cb6ffb3429f92d6203bc1.c40f7b58a7f3848047e1d829b0ec245ac());
						}
						c94f69126adc708dfc5f5fd5b63c52835 = 0;
						return this;
					}
				}
			}
			return new c81332e61de7cb6ffb3429f92d6203bc1(0);
		}

		IEnumerator<ProfilerDataCollection> IEnumerable<ProfilerDataCollection>.GetEnumerator()
		{
			//ILSpy generated this explicit interface implementation from .override directive in c40f7b58a7f3848047e1d829b0ec245ac
			return this.c40f7b58a7f3848047e1d829b0ec245ac();
		}

		[DebuggerHidden]
		private IEnumerator c63811cf6ff5c0b86c54fd5037c6af6ad()
		{
			return c40f7b58a7f3848047e1d829b0ec245ac();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			//ILSpy generated this explicit interface implementation from .override directive in c63811cf6ff5c0b86c54fd5037c6af6ad
			return this.c63811cf6ff5c0b86c54fd5037c6af6ad();
		}

		private bool MoveNext()
		{
			try
			{
				switch (c94f69126adc708dfc5f5fd5b63c52835)
				{
				case 0:
					c94f69126adc708dfc5f5fd5b63c52835 = -1;
					c81bc24b01874c03464f508899a23b019 = c66a6780d080aaa5c45e744fd4c2f6619.c7b92ede539d48fe07a43280283af7eb4();
					c94f69126adc708dfc5f5fd5b63c52835 = 1;
					goto IL_0079;
				case 2:
					{
						c94f69126adc708dfc5f5fd5b63c52835 = 1;
						goto IL_0079;
					}
					IL_0079:
					if (c81bc24b01874c03464f508899a23b019.MoveNext())
					{
						c0d0e4640568eb5e07c8fe18580d4cbca = c81bc24b01874c03464f508899a23b019.Current;
						KeyValuePair<int, ProfilerDataCollection> keyValuePair = c0d0e4640568eb5e07c8fe18580d4cbca;
						cd179d7f18da070d164af6afcb0af45b5 = keyValuePair.Value;
						c94f69126adc708dfc5f5fd5b63c52835 = 2;
						return true;
					}
					while (true)
					{
						switch (2)
						{
						case 0:
							continue;
						}
						break;
					}
					if (1 == 0)
					{
						__ldtoken(c81332e61de7cb6ffb3429f92d6203bc1.MoveNext());
					}
					c6b3af2365d3902564afbc42014f1c625();
					break;
				}
				return false;
			}
			catch
			{
				//try-fault
				c087ddb9300ad4d53672b7a60e6d76672();
				throw;
			}
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		private void c9106c2889b15ecfa28d3af82494ec7bc()
		{
			throw new NotSupportedException();
		}

		void IEnumerator.Reset()
		{
			//ILSpy generated this explicit interface implementation from .override directive in c9106c2889b15ecfa28d3af82494ec7bc
			this.c9106c2889b15ecfa28d3af82494ec7bc();
		}

		private void c087ddb9300ad4d53672b7a60e6d76672()
		{
			switch (c94f69126adc708dfc5f5fd5b63c52835)
			{
			case 1:
			case 2:
				try
				{
					break;
				}
				finally
				{
					c6b3af2365d3902564afbc42014f1c625();
				}
			}
		}

		void IDisposable.Dispose()
		{
			//ILSpy generated this explicit interface implementation from .override directive in c087ddb9300ad4d53672b7a60e6d76672
			this.c087ddb9300ad4d53672b7a60e6d76672();
		}

		private void c6b3af2365d3902564afbc42014f1c625()
		{
			c94f69126adc708dfc5f5fd5b63c52835 = -1;
			((IDisposable)c81bc24b01874c03464f508899a23b019/*cast due to constrained. prefix*/).Dispose();
		}
	}

	private static c70a9f1f6302abae8209fa3bd3ad7722e<int, ProfilerDataCollection> c66a6780d080aaa5c45e744fd4c2f6619;

	[CompilerGenerated]
	private static Dictionary<string, ccbeb88146471c547bb7ba81c8a7966e0> cae8531c14029bb2fa97eadae2f684848;

	[CompilerGenerated]
	private static Stopwatch c6aa62560ef8f06d1f3eb231ec15d1693;

	/// <summary>
	///       Gets the profiler data for each thread.
	///       </summary>
	public static IEnumerable<ProfilerDataCollection> Data
	{
		get
		{
			using c70a9f1f6302abae8209fa3bd3ad7722e<int, ProfilerDataCollection>.c27bac404a3ce140128437624b882afb4 c27bac404a3ce140128437624b882afb = c66a6780d080aaa5c45e744fd4c2f6619.c7b92ede539d48fe07a43280283af7eb4();
			while (c27bac404a3ce140128437624b882afb.MoveNext())
			{
				KeyValuePair<int, ProfilerDataCollection> current = c27bac404a3ce140128437624b882afb.Current;
				KeyValuePair<int, ProfilerDataCollection> keyValuePair = current;
				yield return keyValuePair.Value;
			}
			while (true)
			{
				switch (2)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(c81332e61de7cb6ffb3429f92d6203bc1.MoveNext());
				}
				yield break;
			}
		}
	}

	internal static Dictionary<string, ccbeb88146471c547bb7ba81c8a7966e0> Formats
	{
		[CompilerGenerated]
		get
		{
			return cae8531c14029bb2fa97eadae2f684848;
		}
		[CompilerGenerated]
		private set
		{
			cae8531c14029bb2fa97eadae2f684848 = value;
		}
	}

	internal static Stopwatch Stopwatch
	{
		[CompilerGenerated]
		get
		{
			return c6aa62560ef8f06d1f3eb231ec15d1693;
		}
		[CompilerGenerated]
		private set
		{
			c6aa62560ef8f06d1f3eb231ec15d1693 = value;
		}
	}

	static Profiler()
	{
		ClearAll();
		Stopwatch = Stopwatch.StartNew();
		Formats = new Dictionary<string, ccbeb88146471c547bb7ba81c8a7966e0>();
	}

	/// <summary>
	///       Removes all profiler data.
	///       </summary>
	public static void ClearAll()
	{
		c66a6780d080aaa5c45e744fd4c2f6619 = new c70a9f1f6302abae8209fa3bd3ad7722e<int, ProfilerDataCollection>(Environment.ProcessorCount);
	}

	/// <summary>
	///       Gets the <see cref="T:DigitalRune.Diagnostics.ProfilerDataCollection" /> for the current thread.
	///       </summary>
	/// <returns>
	///       The <see cref="T:DigitalRune.Diagnostics.ProfilerDataCollection" /> for the current thread.
	///       </returns>
	public static ProfilerDataCollection Get()
	{
		return Get(Thread.CurrentThread);
	}

	/// <summary>
	///       Gets the <see cref="T:DigitalRune.Diagnostics.ProfilerDataCollection" /> for the specified thread.
	///       </summary>
	/// <param name="thread">The thread.</param>
	/// <returns>
	///       The <see cref="T:DigitalRune.Diagnostics.ProfilerDataCollection" /> for the specified thread.
	///       </returns>
	public static ProfilerDataCollection Get(Thread thread)
	{
		if (!c66a6780d080aaa5c45e744fd4c2f6619.cfa34cf3f48196497030d8ee48d8e4741(thread.ManagedThreadId, out var c64f632f47ec42f89161439dfec6e213c))
		{
			while (true)
			{
				switch (3)
				{
				case 0:
					continue;
				}
				break;
			}
			if (1 == 0)
			{
				__ldtoken(Profiler.Get(Thread));
			}
			c64f632f47ec42f89161439dfec6e213c = new ProfilerDataCollection(thread);
			c66a6780d080aaa5c45e744fd4c2f6619.c03d72a32723924cb5b81c585d5df7d9d(thread.ManagedThreadId, c64f632f47ec42f89161439dfec6e213c);
		}
		return c64f632f47ec42f89161439dfec6e213c;
	}

	/// <summary>
	///       Gets the <see cref="T:DigitalRune.Diagnostics.ProfilerData" /> with the given name for the current thread.
	///       </summary>
	/// <param name="name">The name of the profiler data.</param>
	/// <returns>
	///       The <see cref="T:DigitalRune.Diagnostics.ProfilerData" /> with the given name for the current thread.
	///       </returns>
	public static ProfilerData Get(string name)
	{
		ProfilerDataCollection profilerDataCollection = Get();
		if (!profilerDataCollection.TryGet(name, out var value))
		{
			while (true)
			{
				switch (7)
				{
				case 0:
					continue;
				}
				break;
			}
			if (1 == 0)
			{
				__ldtoken(Profiler.Get(string));
			}
			value = new ProfilerData(name);
			profilerDataCollection.Add(value);
		}
		return value;
	}

	/// <summary>
	///       Resets all <see cref="T:DigitalRune.Diagnostics.ProfilerData" /> for all threads.
	///       </summary>
	[Conditional("DIGITALRUNE_PROFILE")]
	public static void ResetAll()
	{
		using c70a9f1f6302abae8209fa3bd3ad7722e<int, ProfilerDataCollection>.c27bac404a3ce140128437624b882afb4 c27bac404a3ce140128437624b882afb = c66a6780d080aaa5c45e744fd4c2f6619.c7b92ede539d48fe07a43280283af7eb4();
		while (c27bac404a3ce140128437624b882afb.MoveNext())
		{
			c27bac404a3ce140128437624b882afb.Current.Value.cf06348a19a922c6295282d49671cbe77();
		}
	}

	/// <summary>
	///       Resets the <see cref="T:DigitalRune.Diagnostics.ProfilerData" /> with the given name for all threads.
	///       </summary>
	/// <param name="name">The name.</param>
	[Conditional("DIGITALRUNE_PROFILE")]
	public static void ResetAll(string name)
	{
		using c70a9f1f6302abae8209fa3bd3ad7722e<int, ProfilerDataCollection>.c27bac404a3ce140128437624b882afb4 c27bac404a3ce140128437624b882afb = c66a6780d080aaa5c45e744fd4c2f6619.c7b92ede539d48fe07a43280283af7eb4();
		while (c27bac404a3ce140128437624b882afb.MoveNext())
		{
			using List<ProfilerData>.Enumerator enumerator = c27bac404a3ce140128437624b882afb.Current.Value.GetEnumerator();
			while (enumerator.MoveNext())
			{
				ProfilerData current = enumerator.Current;
				if (current.Name == name)
				{
					current.cf06348a19a922c6295282d49671cbe77();
				}
			}
			while (true)
			{
				switch (6)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(Profiler.ResetAll(string));
				}
				break;
			}
		}
		while (true)
		{
			switch (4)
			{
			case 0:
				break;
			default:
				return;
			}
		}
	}

	/// <summary>
	///       Resets all <see cref="T:DigitalRune.Diagnostics.ProfilerData" /> for the current thread.
	///       </summary>
	[Conditional("DIGITALRUNE_PROFILE")]
	public static void Reset()
	{
	}

	/// <summary>
	///       Resets all <see cref="T:DigitalRune.Diagnostics.ProfilerData" /> for the given thread.
	///       </summary>
	/// <param name="thread">The thread.</param>
	[Conditional("DIGITALRUNE_PROFILE")]
	public static void Reset(Thread thread)
	{
		Get(thread).cf06348a19a922c6295282d49671cbe77();
	}

	/// <summary>
	///       Resets the <see cref="T:DigitalRune.Diagnostics.ProfilerData" /> with the given name (only for the current thread).
	///       </summary>
	/// <param name="name">The name of the profiler data.</param>
	[Conditional("DIGITALRUNE_PROFILE")]
	public static void Reset(string name)
	{
		Get(name).cf06348a19a922c6295282d49671cbe77();
	}

	/// <summary>
	///       Starts time measurement for the <see cref="T:DigitalRune.Diagnostics.ProfilerData" /> with the given name.
	///       </summary>
	/// <param name="name">The name of the <see cref="T:DigitalRune.Diagnostics.ProfilerData" />.</param>
	[Conditional("DIGITALRUNE_PROFILE")]
	public static void Start(string name)
	{
		ProfilerData profilerData = Get(name);
		profilerData.c1a1ad206553a863175ada6def4da0863();
	}

	/// <summary>
	///       Stops time measurement for the <see cref="T:DigitalRune.Diagnostics.ProfilerData" /> with the given name and
	///       records the elapsed time in seconds.
	///       </summary>
	/// <param name="name">The name of the <see cref="T:DigitalRune.Diagnostics.ProfilerData" />.</param>
	[Conditional("DIGITALRUNE_PROFILE")]
	public static void Stop(string name)
	{
		Get(name).c29a5fa2bae9d5dba9ca9f51e93213ef1();
	}

	/// <summary>
	///       Adds the value to the <see cref="T:DigitalRune.Diagnostics.ProfilerData" /> with the given name.
	///       </summary>
	/// <param name="name">The name.</param>
	/// <param name="value">The value.</param>
	[Conditional("DIGITALRUNE_PROFILE")]
	public static void AddValue(string name, double value)
	{
		Get(name).c8c9bd9b52f15e30f961df2d97bbf4dd2(value);
	}

	/// <summary>
	///       Sets the formatting data for <see cref="T:DigitalRune.Diagnostics.ProfilerData" />.
	///       </summary>
	/// <param name="name">The name of the <see cref="T:DigitalRune.Diagnostics.ProfilerData" />.</param>
	/// <param name="scale">The scale.</param>
	/// <param name="description">The description.</param>
	public static void SetFormat(string name, double scale, string description)
	{
		lock (((ICollection)Formats).SyncRoot)
		{
			if (!Formats.TryGetValue(name, out var value))
			{
				while (true)
				{
					switch (3)
					{
					case 0:
						continue;
					}
					break;
				}
				if (1 == 0)
				{
					__ldtoken(Profiler.SetFormat(string, double, string));
				}
				value = new ccbeb88146471c547bb7ba81c8a7966e0();
				Formats.Add(name, value);
			}
			value.Scale = scale;
			value.Description = description;
		}
	}

	/// <summary>
	///       Returns a string that contains all collected profiler data (for all threads). 
	///       </summary>
	/// <returns>
	///       A string containing all collected profiler data (for all threads). The string contains
	///       a table for each thread.
	///       </returns>
	public static string DumpAll()
	{
		StringBuilder stringBuilder = new StringBuilder();
		bool flag = false;
		using (c70a9f1f6302abae8209fa3bd3ad7722e<int, ProfilerDataCollection>.c27bac404a3ce140128437624b882afb4 c27bac404a3ce140128437624b882afb = c66a6780d080aaa5c45e744fd4c2f6619.c7b92ede539d48fe07a43280283af7eb4())
		{
			while (c27bac404a3ce140128437624b882afb.MoveNext())
			{
				KeyValuePair<int, ProfilerDataCollection> current = c27bac404a3ce140128437624b882afb.Current;
				if (flag)
				{
					stringBuilder.AppendLine();
				}
				flag = true;
				stringBuilder.Append(current.Value.ceeb78ad5f7e390f791bacde4dfd07c42());
			}
		}
		return stringBuilder.ToString();
	}

	/// <summary>
	///       Returns a string that contains a table with all <see cref="T:DigitalRune.Diagnostics.ProfilerData" /> instances
	///       for the current thread.
	///       </summary>
	/// <returns>
	///       A string containing a table with all profiler data values for the current thread.
	///       </returns>
	public static string Dump()
	{
		return Get().ceeb78ad5f7e390f791bacde4dfd07c42();
	}

	/// <summary>
	///       Returns a string that contains a table with all <see cref="T:DigitalRune.Diagnostics.ProfilerData" /> instances
	///       for the given thread.
	///       </summary>
	/// <param name="thread">The thread.</param>
	/// <returns>
	///       A string containing a table with all profiler data values for the given thread.
	///       </returns>
	public static string Dump(Thread thread)
	{
		return Get(thread).ceeb78ad5f7e390f791bacde4dfd07c42();
	}
}
