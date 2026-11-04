using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using A;
using DigitalRune.Collections;

namespace DigitalRune.Diagnostics;

/// <summary>
///       Stores a collection of <see cref="T:DigitalRune.Diagnostics.ProfilerData" /> instances for one thread.
///       </summary>
public class ProfilerDataCollection : NamedObjectCollection<ProfilerData>
{
	[CompilerGenerated]
	private string c63d8e2ce020ba789d629a6e6f5e6de7a;

	[CompilerGenerated]
	private int c16d4f001383655a6145d22042e5bebac;

	/// <summary>
	///       Gets or sets the name of the thread.
	///       </summary>
	public string ThreadName
	{
		[CompilerGenerated]
		get
		{
			return c63d8e2ce020ba789d629a6e6f5e6de7a;
		}
		[CompilerGenerated]
		private set
		{
			c63d8e2ce020ba789d629a6e6f5e6de7a = value;
		}
	}

	/// <summary>
	///       Gets or sets the thread ID.
	///       </summary>
	public int ThreadId
	{
		[CompilerGenerated]
		get
		{
			return c16d4f001383655a6145d22042e5bebac;
		}
		[CompilerGenerated]
		private set
		{
			c16d4f001383655a6145d22042e5bebac = value;
		}
	}

	internal ProfilerDataCollection(Thread c4caade7babc0a987b1624380304e6432)
	{
		ThreadName = c4caade7babc0a987b1624380304e6432.Name;
		ThreadId = c4caade7babc0a987b1624380304e6432.ManagedThreadId;
	}

	internal void cf06348a19a922c6295282d49671cbe77()
	{
		using List<ProfilerData>.Enumerator enumerator = GetEnumerator();
		while (enumerator.MoveNext())
		{
			ProfilerData current = enumerator.Current;
			current.cf06348a19a922c6295282d49671cbe77();
		}
		while (true)
		{
			switch (1)
			{
			case 0:
				continue;
			}
			if (1 == 0)
			{
				__ldtoken(ProfilerDataCollection.cf06348a19a922c6295282d49671cbe77());
			}
			return;
		}
	}

	internal string ceeb78ad5f7e390f791bacde4dfd07c42()
	{
		lock (((ICollection)Profiler.Formats).SyncRoot)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2340), ThreadName, ThreadId));
			stringBuilder.Append(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2379));
			stringBuilder.Append(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2541));
			using (List<ProfilerData>.Enumerator enumerator = GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ProfilerData current = enumerator.Current;
					if (!Profiler.Formats.TryGetValue(current.Name, out var value))
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
							__ldtoken(ProfilerDataCollection.ceeb78ad5f7e390f791bacde4dfd07c42());
						}
						value = ccbeb88146471c547bb7ba81c8a7966e0.cd7a78bd43074e324847007105e636193;
					}
					cd7bc605dc4b7818d989af390742d0976.cf7baa9f5bbe8a05752da252932c1c321(stringBuilder, current.Name);
					cd7bc605dc4b7818d989af390742d0976.cf7baa9f5bbe8a05752da252932c1c321(stringBuilder, current.Count);
					stringBuilder.Append(' ');
					cd7bc605dc4b7818d989af390742d0976.cf7baa9f5bbe8a05752da252932c1c321(stringBuilder, current.Sum * value.Scale);
					stringBuilder.Append(' ');
					cd7bc605dc4b7818d989af390742d0976.cf7baa9f5bbe8a05752da252932c1c321(stringBuilder, current.Minimum * value.Scale);
					stringBuilder.Append(' ');
					cd7bc605dc4b7818d989af390742d0976.cf7baa9f5bbe8a05752da252932c1c321(stringBuilder, current.Average * value.Scale);
					stringBuilder.Append(' ');
					cd7bc605dc4b7818d989af390742d0976.cf7baa9f5bbe8a05752da252932c1c321(stringBuilder, current.Maximum * value.Scale);
					stringBuilder.Append(' ');
					stringBuilder.Append(value.Description);
					stringBuilder.AppendLine();
				}
				while (true)
				{
					switch (2)
					{
					case 0:
						break;
					default:
						goto end_IL_0178;
					}
					continue;
					end_IL_0178:
					break;
				}
			}
			return stringBuilder.ToString();
		}
	}
}
