using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SynapseGaming.LightingSystem.Core;

/// <summary>
///
/// </summary>
public class LightingSystemPerformance
{
	/// <summary>
	///
	/// </summary>
	public class TimeTracker
	{
		private Stopwatch fNB = new Stopwatch();

		private SystemStatistic fNa;

		internal float TotalMilliseconds => (float)fNB.Elapsed.TotalMilliseconds;

		internal bool IsRunning => fNB.IsRunning;

		/// <summary>
		///
		/// </summary>
		/// <param name="name"></param>
		public TimeTracker(string name)
		{
			fNa = SystemConsole.GetStatistic(name, SystemStatisticCategory.Performance);
		}

		/// <summary>
		///
		/// </summary>
		[Conditional("ENABLE_TIMETRACKER")]
		public void Begin()
		{
			fNB.Start();
		}

		/// <summary>
		///
		/// </summary>
		[Conditional("ENABLE_TIMETRACKER")]
		public void End()
		{
			fNB.Stop();
			fNa.AccumulationValue = (int)fNB.ElapsedMilliseconds;
		}

		/// <summary>
		///
		/// </summary>
		[Conditional("ENABLE_TIMETRACKER")]
		public void Reset()
		{
			fNB.Reset();
		}
	}

	private static Dictionary<string, TimeTracker> fNB = new Dictionary<string, TimeTracker>(64);

	internal static Dictionary<string, TimeTracker> TimeTrackers => fNB;

	/// <summary>
	///
	/// </summary>
	/// <param name="codearea"></param>
	/// <returns></returns>
	public static TimeTracker Begin(string codearea)
	{
		return null;
	}

	/// <summary>
	///
	/// </summary>
	[Conditional("ENABLE_TIMETRACKER")]
	public static void Reset()
	{
		foreach (KeyValuePair<string, TimeTracker> item in fNB)
		{
			if (item.Value.IsRunning)
			{
				throw new Exception("TimeTracker not properly ended.");
			}
		}
	}

	/// <summary>
	///
	/// </summary>
	/// <returns></returns>
	public static string Dump()
	{
		return "";
	}
}
