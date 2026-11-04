using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace DigitalRune.Diagnostics;

/// <summary>
///       Defines a node in the hierarchical measurements of a <see cref="T:DigitalRune.Diagnostics.HierarchicalProfiler" />.
///       </summary>
[DebuggerDisplay("{Name}")]
public class HierarchicalProfilerNode : INamedObject
{
	[CompilerGenerated]
	private sealed class c1adb63a174ef5981cddc74d2b1e38876 : IEnumerable<HierarchicalProfilerNode>, IEnumerator<HierarchicalProfilerNode>, IEnumerable, IEnumerator, IDisposable
	{
		private HierarchicalProfilerNode cd179d7f18da070d164af6afcb0af45b5;

		private int c94f69126adc708dfc5f5fd5b63c52835;

		private int c0e8a058e779916e7715d7616fb0da9e7;

		public HierarchicalProfilerNode c1b0bbfd4af1b6a4c0c71a5dd3c509812;

		public HierarchicalProfilerNode c7892aa75333c99ab47fcbf679be2fbb2;

		HierarchicalProfilerNode IEnumerator<HierarchicalProfilerNode>.Current
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
		public c1adb63a174ef5981cddc74d2b1e38876(int c94f69126adc708dfc5f5fd5b63c52835)
		{
			this.c94f69126adc708dfc5f5fd5b63c52835 = c94f69126adc708dfc5f5fd5b63c52835;
			c0e8a058e779916e7715d7616fb0da9e7 = Thread.CurrentThread.ManagedThreadId;
		}

		[DebuggerHidden]
		private IEnumerator<HierarchicalProfilerNode> ce2a24fe13038bbf6da8ee2f187add819()
		{
			c1adb63a174ef5981cddc74d2b1e38876 result;
			if (Thread.CurrentThread.ManagedThreadId == c0e8a058e779916e7715d7616fb0da9e7 && c94f69126adc708dfc5f5fd5b63c52835 == -2)
			{
				c94f69126adc708dfc5f5fd5b63c52835 = 0;
				result = this;
			}
			else
			{
				result = new c1adb63a174ef5981cddc74d2b1e38876(0)
				{
					c1b0bbfd4af1b6a4c0c71a5dd3c509812 = c1b0bbfd4af1b6a4c0c71a5dd3c509812
				};
			}
			return result;
		}

		IEnumerator<HierarchicalProfilerNode> IEnumerable<HierarchicalProfilerNode>.GetEnumerator()
		{
			//ILSpy generated this explicit interface implementation from .override directive in ce2a24fe13038bbf6da8ee2f187add819
			return this.ce2a24fe13038bbf6da8ee2f187add819();
		}

		[DebuggerHidden]
		private IEnumerator c63811cf6ff5c0b86c54fd5037c6af6ad()
		{
			return ce2a24fe13038bbf6da8ee2f187add819();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			//ILSpy generated this explicit interface implementation from .override directive in c63811cf6ff5c0b86c54fd5037c6af6ad
			return this.c63811cf6ff5c0b86c54fd5037c6af6ad();
		}

		private bool MoveNext()
		{
			switch (c94f69126adc708dfc5f5fd5b63c52835)
			{
			case 0:
				c94f69126adc708dfc5f5fd5b63c52835 = -1;
				c7892aa75333c99ab47fcbf679be2fbb2 = c1b0bbfd4af1b6a4c0c71a5dd3c509812.FirstChild;
				goto IL_0060;
			case 1:
				{
					c94f69126adc708dfc5f5fd5b63c52835 = -1;
					c7892aa75333c99ab47fcbf679be2fbb2 = c7892aa75333c99ab47fcbf679be2fbb2.NextSibling;
					goto IL_0060;
				}
				IL_0060:
				if (c7892aa75333c99ab47fcbf679be2fbb2 != null)
				{
					cd179d7f18da070d164af6afcb0af45b5 = c7892aa75333c99ab47fcbf679be2fbb2;
					c94f69126adc708dfc5f5fd5b63c52835 = 1;
					return true;
				}
				while (true)
				{
					switch (5)
					{
					case 0:
						continue;
					}
					break;
				}
				if (1 == 0)
				{
					__ldtoken(c1adb63a174ef5981cddc74d2b1e38876.MoveNext());
				}
				break;
			}
			return false;
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
		}

		void IDisposable.Dispose()
		{
			//ILSpy generated this explicit interface implementation from .override directive in c087ddb9300ad4d53672b7a60e6d76672
			this.c087ddb9300ad4d53672b7a60e6d76672();
		}
	}

	private double c24bb09531b30a108a8b01a4e71939a25;

	private int c863d17fa565459f6793acf67b88596e4;

	[CompilerGenerated]
	private string c4562595c3012a609fed551ff9cc45ada;

	[CompilerGenerated]
	private HierarchicalProfiler c0a1fd548ac77b299aac932d00f1d0e2c;

	[CompilerGenerated]
	private HierarchicalProfilerNode c68564c291a2eb3cc110bf32b8735d0fa;

	[CompilerGenerated]
	private HierarchicalProfilerNode c1d374c50fd99a98de5afe7f558b4d284;

	[CompilerGenerated]
	private HierarchicalProfilerNode cd07ecd9e7a2b3ad02a772e5e4e34eedf;

	[CompilerGenerated]
	private HierarchicalProfilerNode c663a2b8c215282cbe097a4b085cb6463;

	[CompilerGenerated]
	private int c7f00b6b375f34a64a37616c1e4df9f76;

	[CompilerGenerated]
	private double c5769353ce7565ebe28f2d915e139f5c0;

	[CompilerGenerated]
	private double c8e2764902448e2b61848f2a531929f31;

	[CompilerGenerated]
	private double c9f120e89b42321cd35c7b8980fa6f77a;

	/// <summary>
	///       Gets the name of this node.
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
	///       Gets the profiler.
	///       </summary>
	public HierarchicalProfiler Profiler
	{
		[CompilerGenerated]
		get
		{
			return c0a1fd548ac77b299aac932d00f1d0e2c;
		}
		[CompilerGenerated]
		private set
		{
			c0a1fd548ac77b299aac932d00f1d0e2c = value;
		}
	}

	/// <summary>
	///       Gets the parent node.
	///       </summary>
	public HierarchicalProfilerNode Parent
	{
		[CompilerGenerated]
		get
		{
			return c68564c291a2eb3cc110bf32b8735d0fa;
		}
		[CompilerGenerated]
		private set
		{
			c68564c291a2eb3cc110bf32b8735d0fa = value;
		}
	}

	private HierarchicalProfilerNode FirstChild
	{
		[CompilerGenerated]
		get
		{
			return c1d374c50fd99a98de5afe7f558b4d284;
		}
		[CompilerGenerated]
		set
		{
			c1d374c50fd99a98de5afe7f558b4d284 = value;
		}
	}

	private HierarchicalProfilerNode LastChild
	{
		[CompilerGenerated]
		get
		{
			return cd07ecd9e7a2b3ad02a772e5e4e34eedf;
		}
		[CompilerGenerated]
		set
		{
			cd07ecd9e7a2b3ad02a772e5e4e34eedf = value;
		}
	}

	private HierarchicalProfilerNode NextSibling
	{
		[CompilerGenerated]
		get
		{
			return c663a2b8c215282cbe097a4b085cb6463;
		}
		[CompilerGenerated]
		set
		{
			c663a2b8c215282cbe097a4b085cb6463 = value;
		}
	}

	/// <summary>
	///       Gets the child nodes.
	///       </summary>
	public IEnumerable<HierarchicalProfilerNode> Children
	{
		get
		{
			for (HierarchicalProfilerNode hierarchicalProfilerNode = FirstChild; hierarchicalProfilerNode != null; hierarchicalProfilerNode = hierarchicalProfilerNode.NextSibling)
			{
				yield return hierarchicalProfilerNode;
			}
			while (true)
			{
				switch (5)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(c1adb63a174ef5981cddc74d2b1e38876.MoveNext());
				}
				yield break;
			}
		}
	}

	/// <summary>
	///       Gets how often this node was called.
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
	///       Gets the total accumulated time of this node.
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
	///       Gets the minimum time of all non-recursive calls.
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
	///       Gets the maximum time of all non-recursive calls.
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
	///       Gets the average (arithmetic mean) time of all non-recursive calls.
	///       </summary>
	public double Average => Sum / (double)Count;

	internal HierarchicalProfilerNode(string ccfdd5487bfbd01ad9e15d54887d721b6, HierarchicalProfiler c8656e59fc565e9d97f9a6daf70194927)
	{
		Name = ccfdd5487bfbd01ad9e15d54887d721b6;
		Profiler = c8656e59fc565e9d97f9a6daf70194927;
		cf06348a19a922c6295282d49671cbe77();
	}

	internal HierarchicalProfilerNode(string ccfdd5487bfbd01ad9e15d54887d721b6, HierarchicalProfilerNode c1f50f2047925b46636270e9827930b10)
	{
		Name = ccfdd5487bfbd01ad9e15d54887d721b6;
		Profiler = c1f50f2047925b46636270e9827930b10.Profiler;
		Parent = c1f50f2047925b46636270e9827930b10;
		cf06348a19a922c6295282d49671cbe77();
	}

	internal void cf06348a19a922c6295282d49671cbe77()
	{
		Count = 0;
		Sum = 0.0;
		Minimum = double.NaN;
		Maximum = double.NaN;
		if (FirstChild != null)
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
				__ldtoken(HierarchicalProfilerNode.cf06348a19a922c6295282d49671cbe77());
			}
			FirstChild.cf06348a19a922c6295282d49671cbe77();
		}
		if (NextSibling != null)
		{
			NextSibling.cf06348a19a922c6295282d49671cbe77();
		}
	}

	internal void c91885c4415e5ce9d09690f58c8b97d8e()
	{
		Count++;
		if (c863d17fa565459f6793acf67b88596e4 == 0)
		{
			c24bb09531b30a108a8b01a4e71939a25 = Profiler.ElapsedTime;
		}
		c863d17fa565459f6793acf67b88596e4++;
	}

	internal bool c870bb8f5a0d4ae57b4286b4492eb6d2a()
	{
		c863d17fa565459f6793acf67b88596e4--;
		if (c863d17fa565459f6793acf67b88596e4 == 0)
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
			if (1 == 0)
			{
				__ldtoken(HierarchicalProfilerNode.c870bb8f5a0d4ae57b4286b4492eb6d2a());
			}
			double elapsedTime = Profiler.ElapsedTime;
			double num = elapsedTime - c24bb09531b30a108a8b01a4e71939a25;
			Sum += elapsedTime - c24bb09531b30a108a8b01a4e71939a25;
			if (Count == 1)
			{
				while (true)
				{
					switch (5)
					{
					case 0:
						continue;
					}
					break;
				}
				Minimum = num;
				Maximum = num;
			}
			else
			{
				Minimum = Math.Min(num, Minimum);
				Maximum = Math.Max(num, Maximum);
			}
		}
		return c863d17fa565459f6793acf67b88596e4 == 0;
	}

	internal HierarchicalProfilerNode c7193e6a9a834d6ea48b30ce7304d7c63(string ccfde6d20aa08fa0249e0b39a32e9d978)
	{
		for (HierarchicalProfilerNode hierarchicalProfilerNode = FirstChild; hierarchicalProfilerNode != null; hierarchicalProfilerNode = hierarchicalProfilerNode.NextSibling)
		{
			if (hierarchicalProfilerNode.Name == ccfde6d20aa08fa0249e0b39a32e9d978)
			{
				return hierarchicalProfilerNode;
			}
		}
		HierarchicalProfilerNode hierarchicalProfilerNode2 = new HierarchicalProfilerNode(ccfde6d20aa08fa0249e0b39a32e9d978, this);
		if (LastChild == null)
		{
			FirstChild = hierarchicalProfilerNode2;
			LastChild = hierarchicalProfilerNode2;
		}
		else
		{
			LastChild.NextSibling = hierarchicalProfilerNode2;
			LastChild = hierarchicalProfilerNode2;
		}
		return hierarchicalProfilerNode2;
	}
}
