using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using A;

namespace DigitalRune.Diagnostics;

/// <summary>
///        Provides support for hierarchical profiling.
///        </summary>
public class HierarchicalProfiler : INamedObject
{
	[CompilerGenerated]
	private string c4562595c3012a609fed551ff9cc45ada;

	[CompilerGenerated]
	private HierarchicalProfilerNode c5c9f60e12b71259c8000542f62c7158e;

	[CompilerGenerated]
	private HierarchicalProfilerNode c84a660e5bac19f2202f8f44f93275b83;

	[CompilerGenerated]
	private Stopwatch c6aa62560ef8f06d1f3eb231ec15d1693;

	[CompilerGenerated]
	private int c8e16bd9ae7e4df58fc13e27a0e5d60e3;

	/// <summary>
	///       Gets the name of this profiler.
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
	///       Gets the root node.
	///       </summary>
	public HierarchicalProfilerNode Root
	{
		[CompilerGenerated]
		get
		{
			return c5c9f60e12b71259c8000542f62c7158e;
		}
		[CompilerGenerated]
		private set
		{
			c5c9f60e12b71259c8000542f62c7158e = value;
		}
	}

	private HierarchicalProfilerNode CurrentNode
	{
		[CompilerGenerated]
		get
		{
			return c84a660e5bac19f2202f8f44f93275b83;
		}
		[CompilerGenerated]
		set
		{
			c84a660e5bac19f2202f8f44f93275b83 = value;
		}
	}

	internal Stopwatch Stopwatch
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

	/// <summary>
	///       Gets the number of frames.
	///       </summary>
	public int NumberOfFrames
	{
		[CompilerGenerated]
		get
		{
			return c8e16bd9ae7e4df58fc13e27a0e5d60e3;
		}
		[CompilerGenerated]
		private set
		{
			c8e16bd9ae7e4df58fc13e27a0e5d60e3 = value;
		}
	}

	/// <summary>
	///       Gets the elapsed time since the creation of this instance or the last <see cref="M:DigitalRune.Diagnostics.HierarchicalProfiler.Reset" />.
	///       </summary>
	public double ElapsedTime => Stopwatch.Elapsed.TotalSeconds;

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Diagnostics.HierarchicalProfiler" /> class.
	///       </summary>
	/// <param name="name">The name of this profiler.</param>
	public HierarchicalProfiler(string name)
	{
		Name = name;
		Root = new HierarchicalProfilerNode(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1755), this);
		CurrentNode = Root;
		Stopwatch = Stopwatch.StartNew();
	}

	/// <summary>
	///       Resets all timing data.
	///       </summary>
	[Conditional("DIGITALRUNE_PROFILE")]
	public void Reset()
	{
		Root.cf06348a19a922c6295282d49671cbe77();
		NumberOfFrames = 0;
		Stopwatch.Reset();
		Stopwatch.Start();
	}

	/// <summary>
	///       Starts time measurement for a node.
	///       </summary>
	/// <param name="nodeName">The name of the node.</param>
	[Conditional("DIGITALRUNE_PROFILE")]
	public void Start(string nodeName)
	{
		if (nodeName != CurrentNode.Name)
		{
			CurrentNode = CurrentNode.c7193e6a9a834d6ea48b30ce7304d7c63(nodeName);
		}
		CurrentNode.c91885c4415e5ce9d09690f58c8b97d8e();
	}

	/// <summary>
	///       Stops time measurement for a node.
	///       </summary>
	[Conditional("DIGITALRUNE_PROFILE")]
	public void Stop()
	{
		if (CurrentNode.c870bb8f5a0d4ae57b4286b4492eb6d2a())
		{
			CurrentNode = CurrentNode.Parent;
		}
	}

	/// <summary>
	///       Must be called when a new frame begins.
	///       </summary>
	[Conditional("DIGITALRUNE_PROFILE")]
	public void NewFrame()
	{
		NumberOfFrames++;
	}

	/// <summary>
	///       Dumps the profiled data of the given node.
	///       </summary>
	/// <param name="node">The node.</param>
	/// <param name="maxLevelIncluded">
	///       The maximal level included relative to the given node. 0 means, no child data is included.
	///       1 means, one level of children are included.
	///       </param>
	/// <returns>
	///       A string representing the profiled data.
	///       </returns>
	public string Dump(HierarchicalProfilerNode node, int maxLevelIncluded)
	{
		if (maxLevelIncluded < 0)
		{
			while (true)
			{
				switch (6)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(HierarchicalProfiler.Dump(HierarchicalProfilerNode, int));
					}
					return string.Empty;
				}
			}
		}
		StringBuilder stringBuilder = new StringBuilder();
		double num;
		if (node != Root)
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
			num = node.Sum;
		}
		else
		{
			num = ElapsedTime;
		}
		double num2 = num;
		stringBuilder.Append(Name);
		stringBuilder.Append(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1613));
		stringBuilder.Append(node.Name);
		stringBuilder.Append(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1616));
		stringBuilder.Append(NumberOfFrames);
		stringBuilder.Append(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1619));
		c38c6866c47707644f78d963ddc21597f(stringBuilder, num2 / (double)NumberOfFrames);
		stringBuilder.Append(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1636));
		c38c6866c47707644f78d963ddc21597f(stringBuilder, num2);
		stringBuilder.Append(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1651));
		stringBuilder.AppendLine();
		ceeb78ad5f7e390f791bacde4dfd07c42(stringBuilder, node, 1, maxLevelIncluded, num2);
		return stringBuilder.ToString();
	}

	private void ceeb78ad5f7e390f791bacde4dfd07c42(StringBuilder c522b64d400efb0e3fff1037ad5f6909e, HierarchicalProfilerNode c7f587bd57d47897172f47483e6bcca68, int c872e485cc0199002177139fd85d681e5, int cab486a14f73c785778847e8483a5e07d, double cd0695ea2274be6e3131be4518772a845)
	{
		if (c872e485cc0199002177139fd85d681e5 > cab486a14f73c785778847e8483a5e07d)
		{
			while (true)
			{
				switch (5)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(HierarchicalProfiler.ceeb78ad5f7e390f791bacde4dfd07c42(StringBuilder, HierarchicalProfilerNode, int, int, double));
					}
					return;
				}
			}
		}
		double num = 0.0;
		int num2 = 0;
		IEnumerator<HierarchicalProfilerNode> enumerator = c7f587bd57d47897172f47483e6bcca68.Children.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				HierarchicalProfilerNode current = enumerator.Current;
				num2++;
				num += current.Sum;
				for (int i = 0; i < c872e485cc0199002177139fd85d681e5; i++)
				{
					c522b64d400efb0e3fff1037ad5f6909e.Append(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1664));
				}
				while (true)
				{
					switch (3)
					{
					case 0:
						break;
					default:
						goto end_IL_006f;
					}
					continue;
					end_IL_006f:
					break;
				}
				c522b64d400efb0e3fff1037ad5f6909e.Append(current.Name);
				c522b64d400efb0e3fff1037ad5f6909e.Append(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1616));
				c522b64d400efb0e3fff1037ad5f6909e.Append((current.Sum / cd0695ea2274be6e3131be4518772a845 * 100.0).ToString(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1669), CultureInfo.InvariantCulture));
				c522b64d400efb0e3fff1037ad5f6909e.Append(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1674));
				c38c6866c47707644f78d963ddc21597f(c522b64d400efb0e3fff1037ad5f6909e, current.Sum / (double)NumberOfFrames);
				c522b64d400efb0e3fff1037ad5f6909e.Append(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1636));
				c522b64d400efb0e3fff1037ad5f6909e.Append(((double)current.Count / (double)NumberOfFrames).ToString(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1679), CultureInfo.InvariantCulture));
				c522b64d400efb0e3fff1037ad5f6909e.Append(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1694));
				c38c6866c47707644f78d963ddc21597f(c522b64d400efb0e3fff1037ad5f6909e, current.Minimum);
				c522b64d400efb0e3fff1037ad5f6909e.Append(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1723));
				c38c6866c47707644f78d963ddc21597f(c522b64d400efb0e3fff1037ad5f6909e, current.Average);
				c522b64d400efb0e3fff1037ad5f6909e.Append(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1723));
				c38c6866c47707644f78d963ddc21597f(c522b64d400efb0e3fff1037ad5f6909e, current.Maximum);
				c522b64d400efb0e3fff1037ad5f6909e.AppendLine(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1726));
				ceeb78ad5f7e390f791bacde4dfd07c42(c522b64d400efb0e3fff1037ad5f6909e, current, c872e485cc0199002177139fd85d681e5 + 1, cab486a14f73c785778847e8483a5e07d, cd0695ea2274be6e3131be4518772a845);
			}
			while (true)
			{
				switch (3)
				{
				case 0:
					break;
				default:
					goto end_IL_01d0;
				}
				continue;
				end_IL_01d0:
				break;
			}
		}
		finally
		{
			if (enumerator != null)
			{
				while (true)
				{
					switch (6)
					{
					case 0:
						continue;
					}
					enumerator.Dispose();
					break;
				}
			}
		}
		if (num2 <= 0)
		{
			return;
		}
		for (int j = 0; j < c872e485cc0199002177139fd85d681e5; j++)
		{
			c522b64d400efb0e3fff1037ad5f6909e.Append(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1664));
		}
		while (true)
		{
			switch (5)
			{
			case 0:
				continue;
			}
			c522b64d400efb0e3fff1037ad5f6909e.Append(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1729));
			double num3;
			if (c7f587bd57d47897172f47483e6bcca68 != Root)
			{
				while (true)
				{
					switch (6)
					{
					case 0:
						continue;
					}
					break;
				}
				num3 = c7f587bd57d47897172f47483e6bcca68.Sum;
			}
			else
			{
				num3 = ElapsedTime;
			}
			double num4 = num3 - num;
			c522b64d400efb0e3fff1037ad5f6909e.Append((num4 / cd0695ea2274be6e3131be4518772a845 * 100.0).ToString(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1669), CultureInfo.InvariantCulture));
			c522b64d400efb0e3fff1037ad5f6909e.Append(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1674));
			c38c6866c47707644f78d963ddc21597f(c522b64d400efb0e3fff1037ad5f6909e, num4);
			c522b64d400efb0e3fff1037ad5f6909e.Append(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1616));
			c38c6866c47707644f78d963ddc21597f(c522b64d400efb0e3fff1037ad5f6909e, num4 / (double)NumberOfFrames);
			c522b64d400efb0e3fff1037ad5f6909e.Append(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1636));
			c522b64d400efb0e3fff1037ad5f6909e.AppendLine();
			return;
		}
	}

	private static void c38c6866c47707644f78d963ddc21597f(StringBuilder c522b64d400efb0e3fff1037ad5f6909e, double c8d21c67f611263cfcdab19599e225772)
	{
		if ((int)c8d21c67f611263cfcdab19599e225772 > 0)
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
						__ldtoken(HierarchicalProfiler.c38c6866c47707644f78d963ddc21597f(StringBuilder, double));
					}
					c522b64d400efb0e3fff1037ad5f6909e.Append(c8d21c67f611263cfcdab19599e225772.ToString(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1679), CultureInfo.InvariantCulture));
					c522b64d400efb0e3fff1037ad5f6909e.Append(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1742));
					return;
				}
			}
		}
		if (c8d21c67f611263cfcdab19599e225772 > 0.001)
		{
			while (true)
			{
				switch (6)
				{
				case 0:
					break;
				default:
					c522b64d400efb0e3fff1037ad5f6909e.Append((c8d21c67f611263cfcdab19599e225772 * 1000.0).ToString(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1679), CultureInfo.InvariantCulture));
					c522b64d400efb0e3fff1037ad5f6909e.Append(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1745));
					return;
				}
			}
		}
		c522b64d400efb0e3fff1037ad5f6909e.Append((c8d21c67f611263cfcdab19599e225772 * 1000000.0).ToString(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1679), CultureInfo.InvariantCulture));
		c522b64d400efb0e3fff1037ad5f6909e.Append(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1750));
	}
}
