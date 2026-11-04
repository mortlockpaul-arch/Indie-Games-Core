using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using A;
using DigitalRune.Collections;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Partitioning;

/// <summary>
///       Sorts items using the Sweep and Prune method.
///       </summary>
/// <typeparam name="T">The type of items in this spatial partition.</typeparam>
public class SweepAndPruneSpace<T> : BasePartition<T>, cfd5199210e5d1b1eef9639935dc7441a<T>
{
	private class cfbc4257db264a188db6749e63a329fa3
	{
		public c05acc98681ffb369c7a2f2dc25eecbc9 c84a1a15da09156b3c3f1ddaff5c98db8;

		public float cf2e6e952fbc0f199d7bf1b90ba27a6c4;

		public bool c87543cfcfbeafa2cfc037bcd3deec37d;
	}

	private class c05acc98681ffb369c7a2f2dc25eecbc9
	{
		public T c652f269a40a2519b8658ffbade581b4e;

		public Aabb c3e263e990bc8d6c3a2d636ac7a19c6c6;

		public readonly int[] c8e7c602b39e5913ed6d4fe7be0c61af1 = new int[3];

		public readonly int[] c8e160838a44263b61963f6ea6eee1a9a = new int[3];
	}

	private class c0708461e5bffa577325436ba24c2186b : KeyedCollection<T, c05acc98681ffb369c7a2f2dc25eecbc9>
	{
		protected override T GetKeyForItem(c05acc98681ffb369c7a2f2dc25eecbc9 info)
		{
			return info.c652f269a40a2519b8658ffbade581b4e;
		}

		public c05acc98681ffb369c7a2f2dc25eecbc9 c63830c034a936b954f36284b38a8762c(T cf51048ba330b8d3f481ea5bc25a03eb9)
		{
			Dictionary.TryGetValue(cf51048ba330b8d3f481ea5bc25a03eb9, out var value);
			return value;
		}
	}

	[CompilerGenerated]
	private sealed class cda085b1fdec44c24961d688a690d894e
	{
		public SweepAndPruneSpace<T> c1b0bbfd4af1b6a4c0c71a5dd3c509812;

		public T cf51048ba330b8d3f481ea5bc25a03eb9;

		public bool c4bbcee76d72035b01eb6c1d9080ada41(Pair<T> c22655ba082a86d172e436abc6a4fdf11)
		{
			if (!BasePartition<T>.cbbc6d542116e0e180b1f11cadb4505d2.Equals(c22655ba082a86d172e436abc6a4fdf11.First, cf51048ba330b8d3f481ea5bc25a03eb9))
			{
				while (true)
				{
					switch (7)
					{
					case 0:
						break;
					default:
						if (1 == 0)
						{
							__ldtoken(SweepAndPruneSpace<>.cda085b1fdec44c24961d688a690d894e.c4bbcee76d72035b01eb6c1d9080ada41(Pair<T>));
						}
						return BasePartition<T>.cbbc6d542116e0e180b1f11cadb4505d2.Equals(c22655ba082a86d172e436abc6a4fdf11.Second, cf51048ba330b8d3f481ea5bc25a03eb9);
					}
				}
			}
			return true;
		}
	}

	[CompilerGenerated]
	private sealed class cae8db3b4354abc44f6539bbbdfe8b3a5 : IEnumerable<T>, IEnumerator<T>, IEnumerable, IEnumerator, IDisposable
	{
		private T cd179d7f18da070d164af6afcb0af45b5;

		private int c94f69126adc708dfc5f5fd5b63c52835;

		private int c0e8a058e779916e7715d7616fb0da9e7;

		public SweepAndPruneSpace<T> c1b0bbfd4af1b6a4c0c71a5dd3c509812;

		public Aabb cdc3439410d15f28489c964ee87b88fe9;

		public Aabb cc3ae004bdecc8a5e2b7f6f0efd06d1d5;

		public HashSet<c05acc98681ffb369c7a2f2dc25eecbc9> c7915817c30f1818b6749374d314ab606;

		public List<cfbc4257db264a188db6749e63a329fa3> ca81ee6d02f462924ea06dcaf8e9253b2;

		public int c675a10d4b664460617f6035e170a5d80;

		public c05acc98681ffb369c7a2f2dc25eecbc9 ccbd944440cba13268aed98ba2dd9c925;

		public Dictionary<c05acc98681ffb369c7a2f2dc25eecbc9, bool>.KeyCollection.Enumerator c8d63344aa4f76bfdcbbb972020e18599;

		T IEnumerator<T>.Current
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
		public cae8db3b4354abc44f6539bbbdfe8b3a5(int c94f69126adc708dfc5f5fd5b63c52835)
		{
			this.c94f69126adc708dfc5f5fd5b63c52835 = c94f69126adc708dfc5f5fd5b63c52835;
			c0e8a058e779916e7715d7616fb0da9e7 = Thread.CurrentThread.ManagedThreadId;
		}

		[DebuggerHidden]
		private IEnumerator<T> c0843ff4e059c4450bad00d5ba0b6ef48()
		{
			cae8db3b4354abc44f6539bbbdfe8b3a5 cae8db3b4354abc44f6539bbbdfe8b3a6;
			if (Thread.CurrentThread.ManagedThreadId == c0e8a058e779916e7715d7616fb0da9e7 && c94f69126adc708dfc5f5fd5b63c52835 == -2)
			{
				c94f69126adc708dfc5f5fd5b63c52835 = 0;
				cae8db3b4354abc44f6539bbbdfe8b3a6 = this;
			}
			else
			{
				cae8db3b4354abc44f6539bbbdfe8b3a6 = new cae8db3b4354abc44f6539bbbdfe8b3a5(0)
				{
					c1b0bbfd4af1b6a4c0c71a5dd3c509812 = c1b0bbfd4af1b6a4c0c71a5dd3c509812
				};
			}
			cae8db3b4354abc44f6539bbbdfe8b3a6.cdc3439410d15f28489c964ee87b88fe9 = cc3ae004bdecc8a5e2b7f6f0efd06d1d5;
			return cae8db3b4354abc44f6539bbbdfe8b3a6;
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			//ILSpy generated this explicit interface implementation from .override directive in c0843ff4e059c4450bad00d5ba0b6ef48
			return this.c0843ff4e059c4450bad00d5ba0b6ef48();
		}

		[DebuggerHidden]
		private IEnumerator c63811cf6ff5c0b86c54fd5037c6af6ad()
		{
			return c0843ff4e059c4450bad00d5ba0b6ef48();
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
				{
					c94f69126adc708dfc5f5fd5b63c52835 = -1;
					c1b0bbfd4af1b6a4c0c71a5dd3c509812.Update(forceRebuild: false);
					c7915817c30f1818b6749374d314ab606 = ResourcePools<c05acc98681ffb369c7a2f2dc25eecbc9>.HashSets.Obtain();
					ca81ee6d02f462924ea06dcaf8e9253b2 = c1b0bbfd4af1b6a4c0c71a5dd3c509812.c7893e0b992303cc211862f59e171df8c[0];
					c675a10d4b664460617f6035e170a5d80 = ca81ee6d02f462924ea06dcaf8e9253b2.Count;
					int num = 0;
					while (true)
					{
						if (num < c675a10d4b664460617f6035e170a5d80)
						{
							cfbc4257db264a188db6749e63a329fa3 cfbc4257db264a188db6749e63a329fa4 = ca81ee6d02f462924ea06dcaf8e9253b2[num];
							if (!(cfbc4257db264a188db6749e63a329fa4.cf2e6e952fbc0f199d7bf1b90ba27a6c4 <= cdc3439410d15f28489c964ee87b88fe9.Maximum.X))
							{
								break;
							}
							if (!cfbc4257db264a188db6749e63a329fa4.c87543cfcfbeafa2cfc037bcd3deec37d)
							{
								c7915817c30f1818b6749374d314ab606.Add(cfbc4257db264a188db6749e63a329fa4.c84a1a15da09156b3c3f1ddaff5c98db8);
							}
							num++;
							continue;
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
							__ldtoken(SweepAndPruneSpace<>.cae8db3b4354abc44f6539bbbdfe8b3a5.MoveNext());
						}
						break;
					}
					for (int i = 0; i < c675a10d4b664460617f6035e170a5d80; i++)
					{
						cfbc4257db264a188db6749e63a329fa3 cfbc4257db264a188db6749e63a329fa5 = ca81ee6d02f462924ea06dcaf8e9253b2[i];
						if (!(cfbc4257db264a188db6749e63a329fa5.cf2e6e952fbc0f199d7bf1b90ba27a6c4 < cdc3439410d15f28489c964ee87b88fe9.Minimum.X))
						{
							break;
						}
						if (cfbc4257db264a188db6749e63a329fa5.c87543cfcfbeafa2cfc037bcd3deec37d)
						{
							c7915817c30f1818b6749374d314ab606.Remove(cfbc4257db264a188db6749e63a329fa5.c84a1a15da09156b3c3f1ddaff5c98db8);
						}
					}
					c8d63344aa4f76bfdcbbb972020e18599 = c7915817c30f1818b6749374d314ab606.GetEnumerator();
					c94f69126adc708dfc5f5fd5b63c52835 = 1;
					goto IL_0193;
				}
				case 2:
					{
						c94f69126adc708dfc5f5fd5b63c52835 = 1;
						goto IL_0193;
					}
					IL_0193:
					while (c8d63344aa4f76bfdcbbb972020e18599.MoveNext())
					{
						ccbd944440cba13268aed98ba2dd9c925 = c8d63344aa4f76bfdcbbb972020e18599.Current;
						if (!GeometryHelper.HaveContact(ccbd944440cba13268aed98ba2dd9c925.c3e263e990bc8d6c3a2d636ac7a19c6c6, cdc3439410d15f28489c964ee87b88fe9))
						{
							continue;
						}
						while (true)
						{
							switch (7)
							{
							case 0:
								continue;
							}
							cd179d7f18da070d164af6afcb0af45b5 = ccbd944440cba13268aed98ba2dd9c925.c652f269a40a2519b8658ffbade581b4e;
							c94f69126adc708dfc5f5fd5b63c52835 = 2;
							return true;
						}
					}
					cd65081c53ab454b2313d9fa45d40e6c7();
					ResourcePools<c05acc98681ffb369c7a2f2dc25eecbc9>.HashSets.Recycle(c7915817c30f1818b6749374d314ab606);
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
					cd65081c53ab454b2313d9fa45d40e6c7();
				}
			}
		}

		void IDisposable.Dispose()
		{
			//ILSpy generated this explicit interface implementation from .override directive in c087ddb9300ad4d53672b7a60e6d76672
			this.c087ddb9300ad4d53672b7a60e6d76672();
		}

		private void cd65081c53ab454b2313d9fa45d40e6c7()
		{
			c94f69126adc708dfc5f5fd5b63c52835 = -1;
			((IDisposable)c8d63344aa4f76bfdcbbb972020e18599/*cast due to constrained. prefix*/).Dispose();
		}
	}

	private readonly List<cfbc4257db264a188db6749e63a329fa3>[] c7893e0b992303cc211862f59e171df8c = new List<cfbc4257db264a188db6749e63a329fa3>[3];

	private readonly c0708461e5bffa577325436ba24c2186b c793c0734613f80f6ea486c3ca032bd9b = new c0708461e5bffa577325436ba24c2186b();

	private Action c67aea54c00ac9f3ac1c8d80a4e82d790;

	private Action<Pair<T>> ca8d890cde636b6ebce382a10f98e9c38;

	private Action<Pair<T>> cc44219e487b1b4f0955a098cd151c228;

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Partitioning.SweepAndPruneSpace`1" /> class.
	///       </summary>
	public SweepAndPruneSpace()
	{
		c7893e0b992303cc211862f59e171df8c[0] = new List<cfbc4257db264a188db6749e63a329fa3>();
		c7893e0b992303cc211862f59e171df8c[1] = new List<cfbc4257db264a188db6749e63a329fa3>();
		c7893e0b992303cc211862f59e171df8c[2] = new List<cfbc4257db264a188db6749e63a329fa3>();
		EnableSelfOverlaps = true;
	}

	/// <summary>
	///       When implemented in a derived class, creates a new instance of the 
	///       <see cref="T:DigitalRune.Geometry.Partitioning.BasePartition`1" /> derived class.
	///       </summary>
	/// <returns>The new instance.</returns>
	protected override BasePartition<T> CreateInstanceCore()
	{
		return new SweepAndPruneSpace<T>();
	}

	/// <summary>
	///       Makes the instance a clone (deep copy) of the specified <see cref="T:DigitalRune.Geometry.Partitioning.BasePartition`1" />.
	///       </summary>
	/// <param name="sourcePartition">The object to clone.</param>
	protected override void CloneCore(BasePartition<T> sourcePartition)
	{
		base.CloneCore(sourcePartition);
	}

	private void c86698794e5e9a8321531715311634590(Action c396bb112e0fdbfde851a9969f046ad26, Action<Pair<T>> c281d0cce0effc56da972ee16fd23facb, Action<Pair<T>> ce16ddb7282b206e64eb95448188da85a)
	{
		c67aea54c00ac9f3ac1c8d80a4e82d790 = c396bb112e0fdbfde851a9969f046ad26;
		ca8d890cde636b6ebce382a10f98e9c38 = c281d0cce0effc56da972ee16fd23facb;
		cc44219e487b1b4f0955a098cd151c228 = ce16ddb7282b206e64eb95448188da85a;
	}

	void cfd5199210e5d1b1eef9639935dc7441a<T>.c1bdb53c8c6209d3e6b68cb29eb6c1773(Action c396bb112e0fdbfde851a9969f046ad26, Action<Pair<T>> c281d0cce0effc56da972ee16fd23facb, Action<Pair<T>> ce16ddb7282b206e64eb95448188da85a)
	{
		//ILSpy generated this explicit interface implementation from .override directive in c86698794e5e9a8321531715311634590
		this.c86698794e5e9a8321531715311634590(c396bb112e0fdbfde851a9969f046ad26, c281d0cce0effc56da972ee16fd23facb, ce16ddb7282b206e64eb95448188da85a);
	}

	private void c44268eb3786a9543c8a20edb3af55cc0()
	{
		c67aea54c00ac9f3ac1c8d80a4e82d790 = null;
		ca8d890cde636b6ebce382a10f98e9c38 = null;
		cc44219e487b1b4f0955a098cd151c228 = null;
	}

	void cfd5199210e5d1b1eef9639935dc7441a<T>.c706091e93d24d199da97520bab9f558d()
	{
		//ILSpy generated this explicit interface implementation from .override directive in c44268eb3786a9543c8a20edb3af55cc0
		this.c44268eb3786a9543c8a20edb3af55cc0();
	}

	internal override void ced5f5b0bd8302f3e8ade582954f3650d(bool ce8ca0a645441ac556aaf6b4574dbbb1f, HashSet<T> c049085d0fafa74bddedd7455b511e4d2, HashSet<T> c2176039f062c6700652655f4b1223258, HashSet<T> c7a74c15b192971583f3f676b15968063)
	{
		if (ce8ca0a645441ac556aaf6b4574dbbb1f)
		{
			c7893e0b992303cc211862f59e171df8c[0].Clear();
			c7893e0b992303cc211862f59e171df8c[1].Clear();
			c7893e0b992303cc211862f59e171df8c[2].Clear();
			c793c0734613f80f6ea486c3ca032bd9b.Clear();
			if (EnableSelfOverlaps)
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
					__ldtoken(SweepAndPruneSpace<>.ced5f5b0bd8302f3e8ade582954f3650d(bool, HashSet<T>, HashSet<T>, HashSet<T>));
				}
				c65379642bd949dc7e978580b9649b9bc.Clear();
			}
			if (c67aea54c00ac9f3ac1c8d80a4e82d790 != null)
			{
				c67aea54c00ac9f3ac1c8d80a4e82d790();
			}
			using List<T>.Enumerator enumerator = c0e601bcddbab14811b8bae6ae4b7b274.GetEnumerator();
			while (enumerator.MoveNext())
			{
				T current = enumerator.Current;
				cc7b924660207920f3939360f0e32c2a0(current);
			}
			while (true)
			{
				switch (6)
				{
				case 0:
					break;
				default:
					goto end_IL_00a0;
				}
				continue;
				end_IL_00a0:
				break;
			}
		}
		else
		{
			using (Dictionary<T, bool>.KeyCollection.Enumerator enumerator2 = c2176039f062c6700652655f4b1223258.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					T current2 = enumerator2.Current;
					c96c6536fde380c341fb4f2176a06cc89(current2);
				}
				while (true)
				{
					switch (1)
					{
					case 0:
						break;
					default:
						goto end_IL_00df;
					}
					continue;
					end_IL_00df:
					break;
				}
			}
			if (c7a74c15b192971583f3f676b15968063 == null)
			{
				IEnumerator<c05acc98681ffb369c7a2f2dc25eecbc9> enumerator3 = c793c0734613f80f6ea486c3ca032bd9b.GetEnumerator();
				try
				{
					while (enumerator3.MoveNext())
					{
						c05acc98681ffb369c7a2f2dc25eecbc9 current3 = enumerator3.Current;
						c335e424f4dfe70b435f8ac60623c8c99(current3);
					}
					while (true)
					{
						switch (2)
						{
						case 0:
							break;
						default:
							goto end_IL_0128;
						}
						continue;
						end_IL_0128:
						break;
					}
				}
				finally
				{
					if (enumerator3 != null)
					{
						while (true)
						{
							switch (4)
							{
							case 0:
								continue;
							}
							enumerator3.Dispose();
							break;
						}
					}
				}
			}
			else
			{
				foreach (T item in c7a74c15b192971583f3f676b15968063)
				{
					c335e424f4dfe70b435f8ac60623c8c99(item);
				}
			}
			foreach (T item2 in c049085d0fafa74bddedd7455b511e4d2)
			{
				cc7b924660207920f3939360f0e32c2a0(item2);
			}
		}
		c527912fe7c25ec4be62695147b7edc1d();
	}

	private void cc7b924660207920f3939360f0e32c2a0(T cf51048ba330b8d3f481ea5bc25a03eb9)
	{
		Aabb c3e263e990bc8d6c3a2d636ac7a19c6c = GetAabbForItem(cf51048ba330b8d3f481ea5bc25a03eb9);
		c05acc98681ffb369c7a2f2dc25eecbc9 c05acc98681ffb369c7a2f2dc25eecbc10 = new c05acc98681ffb369c7a2f2dc25eecbc9();
		c05acc98681ffb369c7a2f2dc25eecbc10.c3e263e990bc8d6c3a2d636ac7a19c6c6 = c3e263e990bc8d6c3a2d636ac7a19c6c;
		c05acc98681ffb369c7a2f2dc25eecbc10.c652f269a40a2519b8658ffbade581b4e = cf51048ba330b8d3f481ea5bc25a03eb9;
		c05acc98681ffb369c7a2f2dc25eecbc9 c05acc98681ffb369c7a2f2dc25eecbc11 = c05acc98681ffb369c7a2f2dc25eecbc10;
		c793c0734613f80f6ea486c3ca032bd9b.Add(c05acc98681ffb369c7a2f2dc25eecbc11);
		for (int i = 0; i < 3; i++)
		{
			cfbc4257db264a188db6749e63a329fa3 cfbc4257db264a188db6749e63a329fa4 = new cfbc4257db264a188db6749e63a329fa3();
			cfbc4257db264a188db6749e63a329fa4.c84a1a15da09156b3c3f1ddaff5c98db8 = c05acc98681ffb369c7a2f2dc25eecbc11;
			cfbc4257db264a188db6749e63a329fa4.c87543cfcfbeafa2cfc037bcd3deec37d = false;
			cfbc4257db264a188db6749e63a329fa4.cf2e6e952fbc0f199d7bf1b90ba27a6c4 = c3e263e990bc8d6c3a2d636ac7a19c6c.Minimum[i];
			cfbc4257db264a188db6749e63a329fa3 item = cfbc4257db264a188db6749e63a329fa4;
			cfbc4257db264a188db6749e63a329fa3 cfbc4257db264a188db6749e63a329fa5 = new cfbc4257db264a188db6749e63a329fa3();
			cfbc4257db264a188db6749e63a329fa5.c84a1a15da09156b3c3f1ddaff5c98db8 = c05acc98681ffb369c7a2f2dc25eecbc11;
			cfbc4257db264a188db6749e63a329fa5.c87543cfcfbeafa2cfc037bcd3deec37d = true;
			cfbc4257db264a188db6749e63a329fa5.cf2e6e952fbc0f199d7bf1b90ba27a6c4 = c3e263e990bc8d6c3a2d636ac7a19c6c.Maximum[i];
			cfbc4257db264a188db6749e63a329fa3 item2 = cfbc4257db264a188db6749e63a329fa5;
			List<cfbc4257db264a188db6749e63a329fa3> list = c7893e0b992303cc211862f59e171df8c[i];
			c05acc98681ffb369c7a2f2dc25eecbc11.c8e7c602b39e5913ed6d4fe7be0c61af1[i] = list.Count;
			list.Add(item);
			c05acc98681ffb369c7a2f2dc25eecbc11.c8e160838a44263b61963f6ea6eee1a9a[i] = list.Count;
			list.Add(item2);
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
				__ldtoken(SweepAndPruneSpace<>.cc7b924660207920f3939360f0e32c2a0(T));
			}
			for (int j = 0; j < 3; j++)
			{
				List<cfbc4257db264a188db6749e63a329fa3> c2fd81d8e0cf197328b96d69860348fc = c7893e0b992303cc211862f59e171df8c[j];
				cd3a14146354fc666c748b364a5a9c6d9(c2fd81d8e0cf197328b96d69860348fc, j, c05acc98681ffb369c7a2f2dc25eecbc11.c8e7c602b39e5913ed6d4fe7be0c61af1[j]);
				ca9b9171fe71a764dd05df59e15655295(c2fd81d8e0cf197328b96d69860348fc, j, c05acc98681ffb369c7a2f2dc25eecbc11.c8e160838a44263b61963f6ea6eee1a9a[j]);
			}
			return;
		}
	}

	private void c335e424f4dfe70b435f8ac60623c8c99(T cf51048ba330b8d3f481ea5bc25a03eb9)
	{
		c335e424f4dfe70b435f8ac60623c8c99(c793c0734613f80f6ea486c3ca032bd9b.c63830c034a936b954f36284b38a8762c(cf51048ba330b8d3f481ea5bc25a03eb9));
	}

	private void c335e424f4dfe70b435f8ac60623c8c99(c05acc98681ffb369c7a2f2dc25eecbc9 c733bdcb005d0bf86335efb5d967d7ba8)
	{
		Aabb aabb = (c733bdcb005d0bf86335efb5d967d7ba8.c3e263e990bc8d6c3a2d636ac7a19c6c6 = GetAabbForItem(c733bdcb005d0bf86335efb5d967d7ba8.c652f269a40a2519b8658ffbade581b4e));
		for (int i = 0; i < 3; i++)
		{
			List<cfbc4257db264a188db6749e63a329fa3> list = c7893e0b992303cc211862f59e171df8c[i];
			float num = aabb.Minimum[i];
			float num2 = aabb.Maximum[i];
			int num3 = c733bdcb005d0bf86335efb5d967d7ba8.c8e7c602b39e5913ed6d4fe7be0c61af1[i];
			int num4 = c733bdcb005d0bf86335efb5d967d7ba8.c8e160838a44263b61963f6ea6eee1a9a[i];
			cfbc4257db264a188db6749e63a329fa3 cfbc4257db264a188db6749e63a329fa4 = list[num3];
			float num5 = num - cfbc4257db264a188db6749e63a329fa4.cf2e6e952fbc0f199d7bf1b90ba27a6c4;
			cfbc4257db264a188db6749e63a329fa4.cf2e6e952fbc0f199d7bf1b90ba27a6c4 = num;
			cfbc4257db264a188db6749e63a329fa3 cfbc4257db264a188db6749e63a329fa5 = list[num4];
			float num6 = num2 - cfbc4257db264a188db6749e63a329fa5.cf2e6e952fbc0f199d7bf1b90ba27a6c4;
			cfbc4257db264a188db6749e63a329fa5.cf2e6e952fbc0f199d7bf1b90ba27a6c4 = num2;
			if (num5 < 0f)
			{
				cd3a14146354fc666c748b364a5a9c6d9(list, i, num3);
			}
			if (num6 > 0f)
			{
				c57d6f0ce9556054e08f4023432a94819(list, i, num4);
			}
			if (num5 > 0f)
			{
				cf3a7a37835e8bb2d35f02a72772011a8(list, i, num3);
			}
			if (num6 < 0f)
			{
				ca9b9171fe71a764dd05df59e15655295(list, i, num4);
			}
		}
	}

	private void c96c6536fde380c341fb4f2176a06cc89(T cf51048ba330b8d3f481ea5bc25a03eb9)
	{
		c05acc98681ffb369c7a2f2dc25eecbc9 c05acc98681ffb369c7a2f2dc25eecbc10 = c793c0734613f80f6ea486c3ca032bd9b.c63830c034a936b954f36284b38a8762c(cf51048ba330b8d3f481ea5bc25a03eb9);
		if (c05acc98681ffb369c7a2f2dc25eecbc10 == null)
		{
			while (true)
			{
				switch (1)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(SweepAndPruneSpace<>.c96c6536fde380c341fb4f2176a06cc89(T));
					}
					return;
				}
			}
		}
		c793c0734613f80f6ea486c3ca032bd9b.Remove(cf51048ba330b8d3f481ea5bc25a03eb9);
		for (int i = 0; i < 3; i++)
		{
			int num = c05acc98681ffb369c7a2f2dc25eecbc10.c8e7c602b39e5913ed6d4fe7be0c61af1[i];
			int num2 = c05acc98681ffb369c7a2f2dc25eecbc10.c8e160838a44263b61963f6ea6eee1a9a[i];
			List<cfbc4257db264a188db6749e63a329fa3> list = c7893e0b992303cc211862f59e171df8c[i];
			int count = list.Count;
			for (int j = num + 1; j < count; j++)
			{
				int num3;
				if (j <= num2)
				{
					while (true)
					{
						switch (4)
						{
						case 0:
							continue;
						}
						break;
					}
					num3 = 1;
				}
				else
				{
					num3 = 2;
				}
				int num4 = num3;
				c05acc98681ffb369c7a2f2dc25eecbc9 c84a1a15da09156b3c3f1ddaff5c98db = list[j].c84a1a15da09156b3c3f1ddaff5c98db8;
				if (!list[j].c87543cfcfbeafa2cfc037bcd3deec37d)
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
					c84a1a15da09156b3c3f1ddaff5c98db.c8e7c602b39e5913ed6d4fe7be0c61af1[i] = j - num4;
				}
				else
				{
					c84a1a15da09156b3c3f1ddaff5c98db.c8e160838a44263b61963f6ea6eee1a9a[i] = j - num4;
				}
			}
			list.RemoveAt(num2);
			list.RemoveAt(num);
		}
		if (c65379642bd949dc7e978580b9649b9bc == null)
		{
			return;
		}
		if (cc44219e487b1b4f0955a098cd151c228 != null)
		{
			foreach (Pair<T> item in c65379642bd949dc7e978580b9649b9bc)
			{
				if (!BasePartition<T>.cbbc6d542116e0e180b1f11cadb4505d2.Equals(item.First, cf51048ba330b8d3f481ea5bc25a03eb9))
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
					if (!BasePartition<T>.cbbc6d542116e0e180b1f11cadb4505d2.Equals(item.Second, cf51048ba330b8d3f481ea5bc25a03eb9))
					{
						continue;
					}
				}
				cc44219e487b1b4f0955a098cd151c228(item);
			}
		}
		c65379642bd949dc7e978580b9649b9bc.RemoveWhere((Pair<T> c22655ba082a86d172e436abc6a4fdf11) =>
		{
			if (!BasePartition<T>.cbbc6d542116e0e180b1f11cadb4505d2.Equals(c22655ba082a86d172e436abc6a4fdf11.First, cf51048ba330b8d3f481ea5bc25a03eb9))
			{
				while (true)
				{
					switch (7)
					{
					case 0:
						break;
					default:
						if (1 == 0)
						{
							__ldtoken(SweepAndPruneSpace<>.cda085b1fdec44c24961d688a690d894e.c4bbcee76d72035b01eb6c1d9080ada41(Pair<T>));
						}
						return BasePartition<T>.cbbc6d542116e0e180b1f11cadb4505d2.Equals(c22655ba082a86d172e436abc6a4fdf11.Second, cf51048ba330b8d3f481ea5bc25a03eb9);
					}
				}
			}
			return true;
		});
	}

	private void c527912fe7c25ec4be62695147b7edc1d()
	{
		if (Count == 0)
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
						__ldtoken(SweepAndPruneSpace<>.c527912fe7c25ec4be62695147b7edc1d());
					}
					Aabb = default;
					return;
				}
			}
		}
		Vector3F minimum = new Vector3F(c7893e0b992303cc211862f59e171df8c[0][0].cf2e6e952fbc0f199d7bf1b90ba27a6c4, c7893e0b992303cc211862f59e171df8c[1][0].cf2e6e952fbc0f199d7bf1b90ba27a6c4, c7893e0b992303cc211862f59e171df8c[2][0].cf2e6e952fbc0f199d7bf1b90ba27a6c4);
		int index = c7893e0b992303cc211862f59e171df8c[0].Count - 1;
		Vector3F maximum = new Vector3F(c7893e0b992303cc211862f59e171df8c[0][index].cf2e6e952fbc0f199d7bf1b90ba27a6c4, c7893e0b992303cc211862f59e171df8c[1][index].cf2e6e952fbc0f199d7bf1b90ba27a6c4, c7893e0b992303cc211862f59e171df8c[2][index].cf2e6e952fbc0f199d7bf1b90ba27a6c4);
		Aabb = new Aabb(minimum, maximum);
	}

	private void cd3a14146354fc666c748b364a5a9c6d9(List<cfbc4257db264a188db6749e63a329fa3> c2fd81d8e0cf197328b96d69860348fc1, int c1d763b6f1f2abba260ee7d980bdd0363, int c22f583a23dce6edd96e3e7edb520f2c1)
	{
		if (c22f583a23dce6edd96e3e7edb520f2c1 == 0)
		{
			return;
		}
		cfbc4257db264a188db6749e63a329fa3 cfbc4257db264a188db6749e63a329fa4 = c2fd81d8e0cf197328b96d69860348fc1[c22f583a23dce6edd96e3e7edb520f2c1];
		c05acc98681ffb369c7a2f2dc25eecbc9 c84a1a15da09156b3c3f1ddaff5c98db = cfbc4257db264a188db6749e63a329fa4.c84a1a15da09156b3c3f1ddaff5c98db8;
		int num = c22f583a23dce6edd96e3e7edb520f2c1 - 1;
		cfbc4257db264a188db6749e63a329fa3 cfbc4257db264a188db6749e63a329fa5 = c2fd81d8e0cf197328b96d69860348fc1[num];
		while (cfbc4257db264a188db6749e63a329fa4.cf2e6e952fbc0f199d7bf1b90ba27a6c4 <= cfbc4257db264a188db6749e63a329fa5.cf2e6e952fbc0f199d7bf1b90ba27a6c4)
		{
			c05acc98681ffb369c7a2f2dc25eecbc9 c84a1a15da09156b3c3f1ddaff5c98db2 = cfbc4257db264a188db6749e63a329fa5.c84a1a15da09156b3c3f1ddaff5c98db8;
			if (cfbc4257db264a188db6749e63a329fa5.c87543cfcfbeafa2cfc037bcd3deec37d)
			{
				Pair<T> pair = new Pair<T>(c84a1a15da09156b3c3f1ddaff5c98db.c652f269a40a2519b8658ffbade581b4e, c84a1a15da09156b3c3f1ddaff5c98db2.c652f269a40a2519b8658ffbade581b4e);
				if (EnableSelfOverlaps && c42523ca66e5579767d13d5135685b497(c84a1a15da09156b3c3f1ddaff5c98db, c84a1a15da09156b3c3f1ddaff5c98db2, c1d763b6f1f2abba260ee7d980bdd0363) && (Filter == null || Filter.Filter(pair)))
				{
					c65379642bd949dc7e978580b9649b9bc.Add(pair);
					if (ca8d890cde636b6ebce382a10f98e9c38 != null)
					{
						ca8d890cde636b6ebce382a10f98e9c38(pair);
					}
				}
				c84a1a15da09156b3c3f1ddaff5c98db2.c8e160838a44263b61963f6ea6eee1a9a[c1d763b6f1f2abba260ee7d980bdd0363] = c22f583a23dce6edd96e3e7edb520f2c1;
			}
			else
			{
				c84a1a15da09156b3c3f1ddaff5c98db2.c8e7c602b39e5913ed6d4fe7be0c61af1[c1d763b6f1f2abba260ee7d980bdd0363] = c22f583a23dce6edd96e3e7edb520f2c1;
			}
			c84a1a15da09156b3c3f1ddaff5c98db.c8e7c602b39e5913ed6d4fe7be0c61af1[c1d763b6f1f2abba260ee7d980bdd0363] = num;
			c2fd81d8e0cf197328b96d69860348fc1[c22f583a23dce6edd96e3e7edb520f2c1] = cfbc4257db264a188db6749e63a329fa5;
			c2fd81d8e0cf197328b96d69860348fc1[num] = cfbc4257db264a188db6749e63a329fa4;
			c22f583a23dce6edd96e3e7edb520f2c1--;
			if (c22f583a23dce6edd96e3e7edb520f2c1 == 0)
			{
				break;
			}
			num--;
			cfbc4257db264a188db6749e63a329fa5 = c2fd81d8e0cf197328b96d69860348fc1[num];
		}
	}

	private void cf3a7a37835e8bb2d35f02a72772011a8(List<cfbc4257db264a188db6749e63a329fa3> c2fd81d8e0cf197328b96d69860348fc1, int c1d763b6f1f2abba260ee7d980bdd0363, int c22f583a23dce6edd96e3e7edb520f2c1)
	{
		cfbc4257db264a188db6749e63a329fa3 cfbc4257db264a188db6749e63a329fa4 = c2fd81d8e0cf197328b96d69860348fc1[c22f583a23dce6edd96e3e7edb520f2c1];
		c05acc98681ffb369c7a2f2dc25eecbc9 c84a1a15da09156b3c3f1ddaff5c98db = cfbc4257db264a188db6749e63a329fa4.c84a1a15da09156b3c3f1ddaff5c98db8;
		int num = c22f583a23dce6edd96e3e7edb520f2c1 + 1;
		cfbc4257db264a188db6749e63a329fa3 cfbc4257db264a188db6749e63a329fa5 = c2fd81d8e0cf197328b96d69860348fc1[num];
		while (cfbc4257db264a188db6749e63a329fa4.cf2e6e952fbc0f199d7bf1b90ba27a6c4 > cfbc4257db264a188db6749e63a329fa5.cf2e6e952fbc0f199d7bf1b90ba27a6c4)
		{
			c05acc98681ffb369c7a2f2dc25eecbc9 c84a1a15da09156b3c3f1ddaff5c98db2 = cfbc4257db264a188db6749e63a329fa5.c84a1a15da09156b3c3f1ddaff5c98db8;
			if (cfbc4257db264a188db6749e63a329fa5.c87543cfcfbeafa2cfc037bcd3deec37d)
			{
				Pair<T> pair = new Pair<T>(c84a1a15da09156b3c3f1ddaff5c98db.c652f269a40a2519b8658ffbade581b4e, c84a1a15da09156b3c3f1ddaff5c98db2.c652f269a40a2519b8658ffbade581b4e);
				if (c65379642bd949dc7e978580b9649b9bc != null)
				{
					c65379642bd949dc7e978580b9649b9bc.Remove(pair);
					if (cc44219e487b1b4f0955a098cd151c228 != null)
					{
						cc44219e487b1b4f0955a098cd151c228(pair);
					}
				}
				c84a1a15da09156b3c3f1ddaff5c98db2.c8e160838a44263b61963f6ea6eee1a9a[c1d763b6f1f2abba260ee7d980bdd0363] = c22f583a23dce6edd96e3e7edb520f2c1;
			}
			else
			{
				c84a1a15da09156b3c3f1ddaff5c98db2.c8e7c602b39e5913ed6d4fe7be0c61af1[c1d763b6f1f2abba260ee7d980bdd0363] = c22f583a23dce6edd96e3e7edb520f2c1;
			}
			c84a1a15da09156b3c3f1ddaff5c98db.c8e7c602b39e5913ed6d4fe7be0c61af1[c1d763b6f1f2abba260ee7d980bdd0363] = num;
			c2fd81d8e0cf197328b96d69860348fc1[c22f583a23dce6edd96e3e7edb520f2c1] = cfbc4257db264a188db6749e63a329fa5;
			c2fd81d8e0cf197328b96d69860348fc1[num] = cfbc4257db264a188db6749e63a329fa4;
			c22f583a23dce6edd96e3e7edb520f2c1++;
			num++;
			cfbc4257db264a188db6749e63a329fa5 = c2fd81d8e0cf197328b96d69860348fc1[num];
		}
	}

	private void c57d6f0ce9556054e08f4023432a94819(List<cfbc4257db264a188db6749e63a329fa3> c2fd81d8e0cf197328b96d69860348fc1, int c1d763b6f1f2abba260ee7d980bdd0363, int c22f583a23dce6edd96e3e7edb520f2c1)
	{
		int num = c2fd81d8e0cf197328b96d69860348fc1.Count - 1;
		if (c22f583a23dce6edd96e3e7edb520f2c1 == num)
		{
			return;
		}
		cfbc4257db264a188db6749e63a329fa3 cfbc4257db264a188db6749e63a329fa4 = c2fd81d8e0cf197328b96d69860348fc1[c22f583a23dce6edd96e3e7edb520f2c1];
		c05acc98681ffb369c7a2f2dc25eecbc9 c84a1a15da09156b3c3f1ddaff5c98db = cfbc4257db264a188db6749e63a329fa4.c84a1a15da09156b3c3f1ddaff5c98db8;
		int num2 = c22f583a23dce6edd96e3e7edb520f2c1 + 1;
		cfbc4257db264a188db6749e63a329fa3 cfbc4257db264a188db6749e63a329fa5 = c2fd81d8e0cf197328b96d69860348fc1[num2];
		while (cfbc4257db264a188db6749e63a329fa4.cf2e6e952fbc0f199d7bf1b90ba27a6c4 >= cfbc4257db264a188db6749e63a329fa5.cf2e6e952fbc0f199d7bf1b90ba27a6c4)
		{
			c05acc98681ffb369c7a2f2dc25eecbc9 c84a1a15da09156b3c3f1ddaff5c98db2 = cfbc4257db264a188db6749e63a329fa5.c84a1a15da09156b3c3f1ddaff5c98db8;
			if (!cfbc4257db264a188db6749e63a329fa5.c87543cfcfbeafa2cfc037bcd3deec37d)
			{
				Pair<T> pair = new Pair<T>(c84a1a15da09156b3c3f1ddaff5c98db.c652f269a40a2519b8658ffbade581b4e, c84a1a15da09156b3c3f1ddaff5c98db2.c652f269a40a2519b8658ffbade581b4e);
				if (EnableSelfOverlaps && c42523ca66e5579767d13d5135685b497(c84a1a15da09156b3c3f1ddaff5c98db, c84a1a15da09156b3c3f1ddaff5c98db2, c1d763b6f1f2abba260ee7d980bdd0363) && (Filter == null || Filter.Filter(pair)))
				{
					c65379642bd949dc7e978580b9649b9bc.Add(pair);
					if (ca8d890cde636b6ebce382a10f98e9c38 != null)
					{
						ca8d890cde636b6ebce382a10f98e9c38(pair);
					}
				}
				c84a1a15da09156b3c3f1ddaff5c98db2.c8e7c602b39e5913ed6d4fe7be0c61af1[c1d763b6f1f2abba260ee7d980bdd0363] = c22f583a23dce6edd96e3e7edb520f2c1;
			}
			else
			{
				c84a1a15da09156b3c3f1ddaff5c98db2.c8e160838a44263b61963f6ea6eee1a9a[c1d763b6f1f2abba260ee7d980bdd0363] = c22f583a23dce6edd96e3e7edb520f2c1;
			}
			c84a1a15da09156b3c3f1ddaff5c98db.c8e160838a44263b61963f6ea6eee1a9a[c1d763b6f1f2abba260ee7d980bdd0363] = num2;
			c2fd81d8e0cf197328b96d69860348fc1[c22f583a23dce6edd96e3e7edb520f2c1] = cfbc4257db264a188db6749e63a329fa5;
			c2fd81d8e0cf197328b96d69860348fc1[num2] = cfbc4257db264a188db6749e63a329fa4;
			c22f583a23dce6edd96e3e7edb520f2c1++;
			if (c22f583a23dce6edd96e3e7edb520f2c1 == num)
			{
				break;
			}
			num2++;
			cfbc4257db264a188db6749e63a329fa5 = c2fd81d8e0cf197328b96d69860348fc1[num2];
		}
	}

	private void ca9b9171fe71a764dd05df59e15655295(List<cfbc4257db264a188db6749e63a329fa3> c2fd81d8e0cf197328b96d69860348fc1, int c1d763b6f1f2abba260ee7d980bdd0363, int c22f583a23dce6edd96e3e7edb520f2c1)
	{
		cfbc4257db264a188db6749e63a329fa3 cfbc4257db264a188db6749e63a329fa4 = c2fd81d8e0cf197328b96d69860348fc1[c22f583a23dce6edd96e3e7edb520f2c1];
		c05acc98681ffb369c7a2f2dc25eecbc9 c84a1a15da09156b3c3f1ddaff5c98db = cfbc4257db264a188db6749e63a329fa4.c84a1a15da09156b3c3f1ddaff5c98db8;
		int num = c22f583a23dce6edd96e3e7edb520f2c1 - 1;
		cfbc4257db264a188db6749e63a329fa3 cfbc4257db264a188db6749e63a329fa5 = c2fd81d8e0cf197328b96d69860348fc1[num];
		while (cfbc4257db264a188db6749e63a329fa4.cf2e6e952fbc0f199d7bf1b90ba27a6c4 < cfbc4257db264a188db6749e63a329fa5.cf2e6e952fbc0f199d7bf1b90ba27a6c4)
		{
			c05acc98681ffb369c7a2f2dc25eecbc9 c84a1a15da09156b3c3f1ddaff5c98db2 = cfbc4257db264a188db6749e63a329fa5.c84a1a15da09156b3c3f1ddaff5c98db8;
			if (!cfbc4257db264a188db6749e63a329fa5.c87543cfcfbeafa2cfc037bcd3deec37d)
			{
				Pair<T> pair = new Pair<T>(c84a1a15da09156b3c3f1ddaff5c98db.c652f269a40a2519b8658ffbade581b4e, c84a1a15da09156b3c3f1ddaff5c98db2.c652f269a40a2519b8658ffbade581b4e);
				if (c65379642bd949dc7e978580b9649b9bc != null)
				{
					c65379642bd949dc7e978580b9649b9bc.Remove(pair);
					if (cc44219e487b1b4f0955a098cd151c228 != null)
					{
						cc44219e487b1b4f0955a098cd151c228(pair);
					}
				}
				c84a1a15da09156b3c3f1ddaff5c98db2.c8e7c602b39e5913ed6d4fe7be0c61af1[c1d763b6f1f2abba260ee7d980bdd0363] = c22f583a23dce6edd96e3e7edb520f2c1;
			}
			else
			{
				c84a1a15da09156b3c3f1ddaff5c98db2.c8e160838a44263b61963f6ea6eee1a9a[c1d763b6f1f2abba260ee7d980bdd0363] = c22f583a23dce6edd96e3e7edb520f2c1;
			}
			c84a1a15da09156b3c3f1ddaff5c98db.c8e160838a44263b61963f6ea6eee1a9a[c1d763b6f1f2abba260ee7d980bdd0363] = num;
			c2fd81d8e0cf197328b96d69860348fc1[c22f583a23dce6edd96e3e7edb520f2c1] = cfbc4257db264a188db6749e63a329fa5;
			c2fd81d8e0cf197328b96d69860348fc1[num] = cfbc4257db264a188db6749e63a329fa4;
			c22f583a23dce6edd96e3e7edb520f2c1--;
			num--;
			cfbc4257db264a188db6749e63a329fa5 = c2fd81d8e0cf197328b96d69860348fc1[num];
		}
	}

	private static bool c42523ca66e5579767d13d5135685b497(c05acc98681ffb369c7a2f2dc25eecbc9 c5badf6413d4f2550b6d3170b5572f2f8, c05acc98681ffb369c7a2f2dc25eecbc9 cf3c9b54e2cb8f63defc2a0aa8cf3ea11, int c2217359fa8e1732778709099a220a9ec)
	{
		if (c2217359fa8e1732778709099a220a9ec != 0 && (c5badf6413d4f2550b6d3170b5572f2f8.c8e160838a44263b61963f6ea6eee1a9a[0] < cf3c9b54e2cb8f63defc2a0aa8cf3ea11.c8e7c602b39e5913ed6d4fe7be0c61af1[0] || c5badf6413d4f2550b6d3170b5572f2f8.c8e7c602b39e5913ed6d4fe7be0c61af1[0] > cf3c9b54e2cb8f63defc2a0aa8cf3ea11.c8e160838a44263b61963f6ea6eee1a9a[0]))
		{
			return false;
		}
		if (c2217359fa8e1732778709099a220a9ec != 1 && (c5badf6413d4f2550b6d3170b5572f2f8.c8e160838a44263b61963f6ea6eee1a9a[1] < cf3c9b54e2cb8f63defc2a0aa8cf3ea11.c8e7c602b39e5913ed6d4fe7be0c61af1[1] || c5badf6413d4f2550b6d3170b5572f2f8.c8e7c602b39e5913ed6d4fe7be0c61af1[1] > cf3c9b54e2cb8f63defc2a0aa8cf3ea11.c8e160838a44263b61963f6ea6eee1a9a[1]))
		{
			return false;
		}
		if (c2217359fa8e1732778709099a220a9ec != 2 && (c5badf6413d4f2550b6d3170b5572f2f8.c8e160838a44263b61963f6ea6eee1a9a[2] < cf3c9b54e2cb8f63defc2a0aa8cf3ea11.c8e7c602b39e5913ed6d4fe7be0c61af1[2] || c5badf6413d4f2550b6d3170b5572f2f8.c8e7c602b39e5913ed6d4fe7be0c61af1[2] > cf3c9b54e2cb8f63defc2a0aa8cf3ea11.c8e160838a44263b61963f6ea6eee1a9a[2]))
		{
			return false;
		}
		return true;
	}

	/// <summary>
	///       Gets the items that touch the given AABB.
	///       </summary>
	/// <param name="aabb">The axis-aligned bounding box.</param>
	/// <returns>All items that touch the given AABB.</returns>
	public override IEnumerable<T> GetOverlaps(Aabb aabb)
	{
		Update(forceRebuild: false);
		HashSet<c05acc98681ffb369c7a2f2dc25eecbc9> hashSet = ResourcePools<c05acc98681ffb369c7a2f2dc25eecbc9>.HashSets.Obtain();
		List<cfbc4257db264a188db6749e63a329fa3> list = c7893e0b992303cc211862f59e171df8c[0];
		int count = list.Count;
		int num = 0;
		while (true)
		{
			if (num < count)
			{
				cfbc4257db264a188db6749e63a329fa3 cfbc4257db264a188db6749e63a329fa4 = list[num];
				if (!(cfbc4257db264a188db6749e63a329fa4.cf2e6e952fbc0f199d7bf1b90ba27a6c4 <= aabb.Maximum.X))
				{
					break;
				}
				if (!cfbc4257db264a188db6749e63a329fa4.c87543cfcfbeafa2cfc037bcd3deec37d)
				{
					hashSet.Add(cfbc4257db264a188db6749e63a329fa4.c84a1a15da09156b3c3f1ddaff5c98db8);
				}
				num++;
				continue;
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
				__ldtoken(SweepAndPruneSpace<>.cae8db3b4354abc44f6539bbbdfe8b3a5.MoveNext());
			}
			break;
		}
		for (int i = 0; i < count; i++)
		{
			cfbc4257db264a188db6749e63a329fa3 cfbc4257db264a188db6749e63a329fa5 = list[i];
			if (!(cfbc4257db264a188db6749e63a329fa5.cf2e6e952fbc0f199d7bf1b90ba27a6c4 < aabb.Minimum.X))
			{
				break;
			}
			if (cfbc4257db264a188db6749e63a329fa5.c87543cfcfbeafa2cfc037bcd3deec37d)
			{
				hashSet.Remove(cfbc4257db264a188db6749e63a329fa5.c84a1a15da09156b3c3f1ddaff5c98db8);
			}
		}
		foreach (c05acc98681ffb369c7a2f2dc25eecbc9 item in hashSet)
		{
			if (!GeometryHelper.HaveContact(item.c3e263e990bc8d6c3a2d636ac7a19c6c6, aabb))
			{
				continue;
			}
			while (true)
			{
				switch (7)
				{
				case 0:
					continue;
				}
				break;
			}
			yield return item.c652f269a40a2519b8658ffbade581b4e;
		}
		ResourcePools<c05acc98681ffb369c7a2f2dc25eecbc9>.HashSets.Recycle(hashSet);
	}
}
