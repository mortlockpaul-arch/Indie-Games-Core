using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SynapseGaming.LightingSystem.Core;

public class LightingSystemPerformance
{
	public class TimeTracker
	{
		private Stopwatch l8a = new Stopwatch();

		private SystemStatistic l8_u0013_;

		internal float TotalMilliseconds => (float)l8a.Elapsed.TotalMilliseconds;

		internal bool IsRunning => l8a.IsRunning;

		public TimeTracker(string name)
		{
			l8_u0013_ = SystemConsole.GetStatistic(name, SystemStatisticCategory.Performance);
		}

		[Conditional("ENABLE_TIMETRACKER")]
		public void Begin()
		{
			l8a.Start();
		}

		[Conditional("ENABLE_TIMETRACKER")]
		public void End()
		{
			l8a.Stop();
			l8_u0013_.AccumulationValue = (int)(l8a.Elapsed.TotalMilliseconds * 1000.0);
		}

		[Conditional("ENABLE_TIMETRACKER")]
		public void Reset()
		{
			l8a.Reset();
		}
	}

	private static Dictionary<string, TimeTracker> l8a = new Dictionary<string, TimeTracker>(64);

	internal static Dictionary<string, TimeTracker> TimeTrackers => l8a;

	public static TimeTracker Begin(string codearea)
	{
		return null;
	}

	[Conditional("ENABLE_TIMETRACKER")]
	public static void Reset()
	{
		foreach (KeyValuePair<string, TimeTracker> item in l8a)
		{
			if (item.Value.IsRunning)
			{
				throw new Exception("TimeTracker not properly ended.");
			}
		}
	}

	public static string Dump()
	{
		return "";
	}
}
