using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using A;
using DigitalRune.Collections;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Partitioning;

/// <summary>
///       Represents a dynamic bounding volume tree using axis-aligned bounding boxes (AABBs).
///       </summary>
/// <typeparam name="T">The type of item in the spatial partition.</typeparam>
public class DynamicAabbTree<T> : BasePartition<T>, ISupportClosestPointQueries<T>
{
	private class c07a248008e037e41899c8b004ace7a50 : cbe5649b18b8b0caa00be9ad49188b193<T>
	{
		public Aabb c3e263e990bc8d6c3a2d636ac7a19c6c6;

		public c07a248008e037e41899c8b004ace7a50 c551fa69e7c0ec492ab91daa4199210a2;

		public c07a248008e037e41899c8b004ace7a50 c6d3fb34101b9e1ce92055c2914d2d1de;

		public c07a248008e037e41899c8b004ace7a50 c2d9d869d0c8fd5fb55f452375f580a4a;

		public T c652f269a40a2519b8658ffbade581b4e;

		public bool cfd73e16b19bb8bde65db991a787eb5ad
		{
			get
			{
				if (c6d3fb34101b9e1ce92055c2914d2d1de == null)
				{
					return c2d9d869d0c8fd5fb55f452375f580a4a == null;
				}
				return false;
			}
		}

		private Aabb c10119f0a23100d7faaf2cedf57dea5c7
		{
			get
			{
				return c3e263e990bc8d6c3a2d636ac7a19c6c6;
			}
			set
			{
				c3e263e990bc8d6c3a2d636ac7a19c6c6 = value;
			}
		}

		private cbe5649b18b8b0caa00be9ad49188b193<T> c615388a537d74cc58753af96745b8dde
		{
			get
			{
				return c6d3fb34101b9e1ce92055c2914d2d1de;
			}
			set
			{
				if (c6d3fb34101b9e1ce92055c2914d2d1de != null)
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
					if (1 == 0)
					{
						__ldtoken(DynamicAabbTree<>.c07a248008e037e41899c8b004ace7a50.set_c615388a537d74cc58753af96745b8dde(cbe5649b18b8b0caa00be9ad49188b193<T>));
					}
					c6d3fb34101b9e1ce92055c2914d2d1de.c551fa69e7c0ec492ab91daa4199210a2 = null;
				}
				c6d3fb34101b9e1ce92055c2914d2d1de = (c07a248008e037e41899c8b004ace7a50)value;
				if (c6d3fb34101b9e1ce92055c2914d2d1de == null)
				{
					return;
				}
				while (true)
				{
					switch (7)
					{
					case 0:
						continue;
					}
					c6d3fb34101b9e1ce92055c2914d2d1de.c551fa69e7c0ec492ab91daa4199210a2 = this;
					return;
				}
			}
		}

		private cbe5649b18b8b0caa00be9ad49188b193<T> c7dce2df744f119546642da2929ddcf76
		{
			get
			{
				return c2d9d869d0c8fd5fb55f452375f580a4a;
			}
			set
			{
				if (c2d9d869d0c8fd5fb55f452375f580a4a != null)
				{
					c2d9d869d0c8fd5fb55f452375f580a4a.c551fa69e7c0ec492ab91daa4199210a2 = null;
				}
				c2d9d869d0c8fd5fb55f452375f580a4a = (c07a248008e037e41899c8b004ace7a50)value;
				if (c2d9d869d0c8fd5fb55f452375f580a4a == null)
				{
					return;
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
						__ldtoken(DynamicAabbTree<>.c07a248008e037e41899c8b004ace7a50.set_c7dce2df744f119546642da2929ddcf76(cbe5649b18b8b0caa00be9ad49188b193<T>));
					}
					c2d9d869d0c8fd5fb55f452375f580a4a.c551fa69e7c0ec492ab91daa4199210a2 = this;
					return;
				}
			}
		}

		private cbe5649b18b8b0caa00be9ad49188b193<T> cfff16699b0ff91e60fab908ef0f2ea0b => c551fa69e7c0ec492ab91daa4199210a2;

		private T c477d5bc87b57a983b9d9da4eab43eb34 => c652f269a40a2519b8658ffbade581b4e;
	}

	private class c2313695eacdcb5ef30a3559ae721d868 : KeyedCollection<T, c07a248008e037e41899c8b004ace7a50>
	{
		protected override T GetKeyForItem(c07a248008e037e41899c8b004ace7a50 node)
		{
			return node.c652f269a40a2519b8658ffbade581b4e;
		}

		public c07a248008e037e41899c8b004ace7a50 c63830c034a936b954f36284b38a8762c(T cf51048ba330b8d3f481ea5bc25a03eb9)
		{
			Dictionary.TryGetValue(cf51048ba330b8d3f481ea5bc25a03eb9, out var value);
			return value;
		}
	}

	[CompilerGenerated]
	private sealed class ce338abf3716189437408a0ba88dc0602 : IEnumerable<Pair<T>>, IEnumerator<Pair<T>>, IEnumerable, IEnumerator, IDisposable
	{
		private Pair<T> cd179d7f18da070d164af6afcb0af45b5;

		private int c94f69126adc708dfc5f5fd5b63c52835;

		private int c0e8a058e779916e7715d7616fb0da9e7;

		public DynamicAabbTree<T> c1b0bbfd4af1b6a4c0c71a5dd3c509812;

		public ISpatialPartition<T> cfd8883387afb9cc25a2ac9150de0f66b;

		public ISpatialPartition<T> cd1939b65df62ed5f7a2112466dabd8f0;

		public c07a248008e037e41899c8b004ace7a50 c564ec4f122ba9bfda482d136b8ad95fb;

		public IEnumerable<T> cd436e162e5616ea58af135ffa3719edb;

		public T c80fb2194a6d53503cd4f599dd44c69d1;

		public Pair<T> c58fd89563c384b40d219c2f8aa3f3ae2;

		public IEnumerator<c07a248008e037e41899c8b004ace7a50> c9e7d5643c48037153a5a2f3c7387b95c;

		public IEnumerator<T> cc6ce579f31a652efe49f41376cab173c;

		Pair<T> IEnumerator<Pair<T>>.Current
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
		public ce338abf3716189437408a0ba88dc0602(int c94f69126adc708dfc5f5fd5b63c52835)
		{
			this.c94f69126adc708dfc5f5fd5b63c52835 = c94f69126adc708dfc5f5fd5b63c52835;
			c0e8a058e779916e7715d7616fb0da9e7 = Thread.CurrentThread.ManagedThreadId;
		}

		[DebuggerHidden]
		private IEnumerator<Pair<T>> c4bd63b962f346f189f5c6a4ad2e4ce8d()
		{
			ce338abf3716189437408a0ba88dc0602 ce338abf3716189437408a0ba88dc603;
			if (Thread.CurrentThread.ManagedThreadId == c0e8a058e779916e7715d7616fb0da9e7 && c94f69126adc708dfc5f5fd5b63c52835 == -2)
			{
				c94f69126adc708dfc5f5fd5b63c52835 = 0;
				ce338abf3716189437408a0ba88dc603 = this;
			}
			else
			{
				ce338abf3716189437408a0ba88dc603 = new ce338abf3716189437408a0ba88dc0602(0)
				{
					c1b0bbfd4af1b6a4c0c71a5dd3c509812 = c1b0bbfd4af1b6a4c0c71a5dd3c509812
				};
			}
			ce338abf3716189437408a0ba88dc603.cfd8883387afb9cc25a2ac9150de0f66b = cd1939b65df62ed5f7a2112466dabd8f0;
			return ce338abf3716189437408a0ba88dc603;
		}

		IEnumerator<Pair<T>> IEnumerable<Pair<T>>.GetEnumerator()
		{
			//ILSpy generated this explicit interface implementation from .override directive in c4bd63b962f346f189f5c6a4ad2e4ce8d
			return this.c4bd63b962f346f189f5c6a4ad2e4ce8d();
		}

		[DebuggerHidden]
		private IEnumerator c63811cf6ff5c0b86c54fd5037c6af6ad()
		{
			return c4bd63b962f346f189f5c6a4ad2e4ce8d();
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
				int num = c94f69126adc708dfc5f5fd5b63c52835;
				if (num != 0)
				{
					if (num != 3)
					{
						goto IL_013e;
					}
					c94f69126adc708dfc5f5fd5b63c52835 = 2;
					goto IL_0112;
				}
				c94f69126adc708dfc5f5fd5b63c52835 = -1;
				c9e7d5643c48037153a5a2f3c7387b95c = c1b0bbfd4af1b6a4c0c71a5dd3c509812.c4d76d858205a75f799980f42e41cbe5b(cfd8883387afb9cc25a2ac9150de0f66b.Aabb).GetEnumerator();
				c94f69126adc708dfc5f5fd5b63c52835 = 1;
				goto IL_0128;
				IL_013e:
				return false;
				IL_0112:
				while (cc6ce579f31a652efe49f41376cab173c.MoveNext())
				{
					c80fb2194a6d53503cd4f599dd44c69d1 = cc6ce579f31a652efe49f41376cab173c.Current;
					ref Pair<T> reference = ref c58fd89563c384b40d219c2f8aa3f3ae2;
					reference = new Pair<T>(c564ec4f122ba9bfda482d136b8ad95fb.c652f269a40a2519b8658ffbade581b4e, c80fb2194a6d53503cd4f599dd44c69d1);
					if (c1b0bbfd4af1b6a4c0c71a5dd3c509812.Filter == null || c1b0bbfd4af1b6a4c0c71a5dd3c509812.Filter.Filter(c58fd89563c384b40d219c2f8aa3f3ae2))
					{
						cd179d7f18da070d164af6afcb0af45b5 = c58fd89563c384b40d219c2f8aa3f3ae2;
						c94f69126adc708dfc5f5fd5b63c52835 = 3;
						return true;
					}
				}
				ca437c2283e35f857fc3187a89ada035d();
				goto IL_0128;
				IL_0128:
				if (c9e7d5643c48037153a5a2f3c7387b95c.MoveNext())
				{
					c564ec4f122ba9bfda482d136b8ad95fb = c9e7d5643c48037153a5a2f3c7387b95c.Current;
					cd436e162e5616ea58af135ffa3719edb = cfd8883387afb9cc25a2ac9150de0f66b.GetOverlaps(c564ec4f122ba9bfda482d136b8ad95fb.c3e263e990bc8d6c3a2d636ac7a19c6c6);
					cc6ce579f31a652efe49f41376cab173c = cd436e162e5616ea58af135ffa3719edb.GetEnumerator();
					c94f69126adc708dfc5f5fd5b63c52835 = 2;
					goto IL_0112;
				}
				ca0a60671becd1e0cef8a51aa281f5da6();
				goto IL_013e;
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
			case 3:
				try
				{
					switch (c94f69126adc708dfc5f5fd5b63c52835)
					{
					case 2:
					case 3:
						try
						{
							break;
						}
						finally
						{
							ca437c2283e35f857fc3187a89ada035d();
						}
					}
					break;
				}
				finally
				{
					ca0a60671becd1e0cef8a51aa281f5da6();
				}
			}
		}

		void IDisposable.Dispose()
		{
			//ILSpy generated this explicit interface implementation from .override directive in c087ddb9300ad4d53672b7a60e6d76672
			this.c087ddb9300ad4d53672b7a60e6d76672();
		}

		private void ca0a60671becd1e0cef8a51aa281f5da6()
		{
			c94f69126adc708dfc5f5fd5b63c52835 = -1;
			if (c9e7d5643c48037153a5a2f3c7387b95c == null)
			{
				return;
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
					__ldtoken(DynamicAabbTree<>.ce338abf3716189437408a0ba88dc0602.ca0a60671becd1e0cef8a51aa281f5da6());
				}
				c9e7d5643c48037153a5a2f3c7387b95c.Dispose();
				return;
			}
		}

		private void ca437c2283e35f857fc3187a89ada035d()
		{
			c94f69126adc708dfc5f5fd5b63c52835 = 1;
			if (cc6ce579f31a652efe49f41376cab173c != null)
			{
				cc6ce579f31a652efe49f41376cab173c.Dispose();
			}
		}
	}

	[CompilerGenerated]
	private sealed class c4d1a8923eecf70cd450199c7f8b8684a : IEnumerable<Pair<T>>, IEnumerator<Pair<T>>, IEnumerable, IEnumerator, IDisposable
	{
		private Pair<T> cd179d7f18da070d164af6afcb0af45b5;

		private int c94f69126adc708dfc5f5fd5b63c52835;

		private int c0e8a058e779916e7715d7616fb0da9e7;

		public DynamicAabbTree<T> c1b0bbfd4af1b6a4c0c71a5dd3c509812;

		public DynamicAabbTree<T> ca9802b6266a4b983f751fb2d730561ca;

		public DynamicAabbTree<T> c6b71d229c51c908f777d8166aeaf9947;

		public Stack<Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50>> ce03673dd66404a7b3f8ce7639922d00f;

		public Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50> c4284b4528675b5f3bce341762b870cf8;

		public c07a248008e037e41899c8b004ace7a50 caf909efb99bcf423703f8cdd3c621ce7;

		public c07a248008e037e41899c8b004ace7a50 c3fbc46bcbed8f7a043f1a9517d931d9f;

		public Pair<T> c894694434cc2c8a99a07d1d1c79d6883;

		Pair<T> IEnumerator<Pair<T>>.Current
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
		public c4d1a8923eecf70cd450199c7f8b8684a(int c94f69126adc708dfc5f5fd5b63c52835)
		{
			this.c94f69126adc708dfc5f5fd5b63c52835 = c94f69126adc708dfc5f5fd5b63c52835;
			c0e8a058e779916e7715d7616fb0da9e7 = Thread.CurrentThread.ManagedThreadId;
		}

		[DebuggerHidden]
		private IEnumerator<Pair<T>> c4bd63b962f346f189f5c6a4ad2e4ce8d()
		{
			c4d1a8923eecf70cd450199c7f8b8684a c4d1a8923eecf70cd450199c7f8b8684a2;
			if (Thread.CurrentThread.ManagedThreadId == c0e8a058e779916e7715d7616fb0da9e7)
			{
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
					__ldtoken(DynamicAabbTree<>.c4d1a8923eecf70cd450199c7f8b8684a.c4bd63b962f346f189f5c6a4ad2e4ce8d());
				}
				if (c94f69126adc708dfc5f5fd5b63c52835 == -2)
				{
					c94f69126adc708dfc5f5fd5b63c52835 = 0;
					c4d1a8923eecf70cd450199c7f8b8684a2 = this;
					goto IL_004d;
				}
			}
			c4d1a8923eecf70cd450199c7f8b8684a2 = new c4d1a8923eecf70cd450199c7f8b8684a(0)
			{
				c1b0bbfd4af1b6a4c0c71a5dd3c509812 = c1b0bbfd4af1b6a4c0c71a5dd3c509812
			};
			goto IL_004d;
			IL_004d:
			c4d1a8923eecf70cd450199c7f8b8684a2.ca9802b6266a4b983f751fb2d730561ca = c6b71d229c51c908f777d8166aeaf9947;
			return c4d1a8923eecf70cd450199c7f8b8684a2;
		}

		IEnumerator<Pair<T>> IEnumerable<Pair<T>>.GetEnumerator()
		{
			//ILSpy generated this explicit interface implementation from .override directive in c4bd63b962f346f189f5c6a4ad2e4ce8d
			return this.c4bd63b962f346f189f5c6a4ad2e4ce8d();
		}

		[DebuggerHidden]
		private IEnumerator c63811cf6ff5c0b86c54fd5037c6af6ad()
		{
			return c4bd63b962f346f189f5c6a4ad2e4ce8d();
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
				ce03673dd66404a7b3f8ce7639922d00f = ResourcePools<Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50>>.Stacks.Obtain();
				ce03673dd66404a7b3f8ce7639922d00f.Push(new Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50>(c1b0bbfd4af1b6a4c0c71a5dd3c509812.c3cb2c0edc4d55dd1410e3483d1ef60d4, ca9802b6266a4b983f751fb2d730561ca.c3cb2c0edc4d55dd1410e3483d1ef60d4));
				goto IL_032c;
			case 1:
				{
					c94f69126adc708dfc5f5fd5b63c52835 = -1;
					goto IL_032c;
				}
				IL_032c:
				while (ce03673dd66404a7b3f8ce7639922d00f.Count > 0)
				{
					c4284b4528675b5f3bce341762b870cf8 = ce03673dd66404a7b3f8ce7639922d00f.Pop();
					caf909efb99bcf423703f8cdd3c621ce7 = c4284b4528675b5f3bce341762b870cf8.First;
					c3fbc46bcbed8f7a043f1a9517d931d9f = c4284b4528675b5f3bce341762b870cf8.Second;
					if (caf909efb99bcf423703f8cdd3c621ce7 == c3fbc46bcbed8f7a043f1a9517d931d9f)
					{
						if (!caf909efb99bcf423703f8cdd3c621ce7.cfd73e16b19bb8bde65db991a787eb5ad)
						{
							ce03673dd66404a7b3f8ce7639922d00f.Push(new Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50>(caf909efb99bcf423703f8cdd3c621ce7.c2d9d869d0c8fd5fb55f452375f580a4a, caf909efb99bcf423703f8cdd3c621ce7.c2d9d869d0c8fd5fb55f452375f580a4a));
							ce03673dd66404a7b3f8ce7639922d00f.Push(new Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50>(caf909efb99bcf423703f8cdd3c621ce7.c6d3fb34101b9e1ce92055c2914d2d1de, caf909efb99bcf423703f8cdd3c621ce7.c2d9d869d0c8fd5fb55f452375f580a4a));
							ce03673dd66404a7b3f8ce7639922d00f.Push(new Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50>(caf909efb99bcf423703f8cdd3c621ce7.c6d3fb34101b9e1ce92055c2914d2d1de, caf909efb99bcf423703f8cdd3c621ce7.c6d3fb34101b9e1ce92055c2914d2d1de));
						}
					}
					else
					{
						if (!GeometryHelper.HaveContact(caf909efb99bcf423703f8cdd3c621ce7.c3e263e990bc8d6c3a2d636ac7a19c6c6, c3fbc46bcbed8f7a043f1a9517d931d9f.c3e263e990bc8d6c3a2d636ac7a19c6c6))
						{
							continue;
						}
						if (!caf909efb99bcf423703f8cdd3c621ce7.cfd73e16b19bb8bde65db991a787eb5ad)
						{
							if (!c3fbc46bcbed8f7a043f1a9517d931d9f.cfd73e16b19bb8bde65db991a787eb5ad)
							{
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
									__ldtoken(DynamicAabbTree<>.c4d1a8923eecf70cd450199c7f8b8684a.MoveNext());
								}
								ce03673dd66404a7b3f8ce7639922d00f.Push(new Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50>(caf909efb99bcf423703f8cdd3c621ce7.c2d9d869d0c8fd5fb55f452375f580a4a, c3fbc46bcbed8f7a043f1a9517d931d9f.c2d9d869d0c8fd5fb55f452375f580a4a));
								ce03673dd66404a7b3f8ce7639922d00f.Push(new Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50>(caf909efb99bcf423703f8cdd3c621ce7.c6d3fb34101b9e1ce92055c2914d2d1de, c3fbc46bcbed8f7a043f1a9517d931d9f.c2d9d869d0c8fd5fb55f452375f580a4a));
								ce03673dd66404a7b3f8ce7639922d00f.Push(new Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50>(caf909efb99bcf423703f8cdd3c621ce7.c2d9d869d0c8fd5fb55f452375f580a4a, c3fbc46bcbed8f7a043f1a9517d931d9f.c6d3fb34101b9e1ce92055c2914d2d1de));
								ce03673dd66404a7b3f8ce7639922d00f.Push(new Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50>(caf909efb99bcf423703f8cdd3c621ce7.c6d3fb34101b9e1ce92055c2914d2d1de, c3fbc46bcbed8f7a043f1a9517d931d9f.c6d3fb34101b9e1ce92055c2914d2d1de));
							}
							else
							{
								ce03673dd66404a7b3f8ce7639922d00f.Push(new Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50>(caf909efb99bcf423703f8cdd3c621ce7.c2d9d869d0c8fd5fb55f452375f580a4a, c3fbc46bcbed8f7a043f1a9517d931d9f));
								ce03673dd66404a7b3f8ce7639922d00f.Push(new Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50>(caf909efb99bcf423703f8cdd3c621ce7.c6d3fb34101b9e1ce92055c2914d2d1de, c3fbc46bcbed8f7a043f1a9517d931d9f));
							}
						}
						else if (!c3fbc46bcbed8f7a043f1a9517d931d9f.cfd73e16b19bb8bde65db991a787eb5ad)
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
							ce03673dd66404a7b3f8ce7639922d00f.Push(new Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50>(caf909efb99bcf423703f8cdd3c621ce7, c3fbc46bcbed8f7a043f1a9517d931d9f.c2d9d869d0c8fd5fb55f452375f580a4a));
							ce03673dd66404a7b3f8ce7639922d00f.Push(new Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50>(caf909efb99bcf423703f8cdd3c621ce7, c3fbc46bcbed8f7a043f1a9517d931d9f.c6d3fb34101b9e1ce92055c2914d2d1de));
						}
						else
						{
							ref Pair<T> reference = ref c894694434cc2c8a99a07d1d1c79d6883;
							reference = new Pair<T>(caf909efb99bcf423703f8cdd3c621ce7.c652f269a40a2519b8658ffbade581b4e, c3fbc46bcbed8f7a043f1a9517d931d9f.c652f269a40a2519b8658ffbade581b4e);
							if (c1b0bbfd4af1b6a4c0c71a5dd3c509812.Filter == null || c1b0bbfd4af1b6a4c0c71a5dd3c509812.Filter.Filter(c894694434cc2c8a99a07d1d1c79d6883))
							{
								cd179d7f18da070d164af6afcb0af45b5 = c894694434cc2c8a99a07d1d1c79d6883;
								c94f69126adc708dfc5f5fd5b63c52835 = 1;
								return true;
							}
						}
					}
				}
				while (true)
				{
					switch (6)
					{
					case 0:
						continue;
					}
					break;
				}
				ResourcePools<Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50>>.Stacks.Recycle(ce03673dd66404a7b3f8ce7639922d00f);
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

	[CompilerGenerated]
	private sealed class caca1bf20c97f469f311050b95e74ccd9 : IEnumerable<Pair<T>>, IEnumerator<Pair<T>>, IEnumerable, IEnumerator, IDisposable
	{
		private Pair<T> cd179d7f18da070d164af6afcb0af45b5;

		private int c94f69126adc708dfc5f5fd5b63c52835;

		private int c0e8a058e779916e7715d7616fb0da9e7;

		public DynamicAabbTree<T> c1b0bbfd4af1b6a4c0c71a5dd3c509812;

		public Vector3F ceabb47d7d959545d89c4300eabbdd7c2;

		public Vector3F c6f203721b629fec4c93cc63ff1172fc4;

		public ISpatialPartition<T> cfd8883387afb9cc25a2ac9150de0f66b;

		public ISpatialPartition<T> cd1939b65df62ed5f7a2112466dabd8f0;

		public Vector3F c1b4918d9d3cc94171c67339d1fc78b0f;

		public Vector3F cfd54ba1e25c70994847ab953f2228207;

		public Pose cd2abebf7ff6ec7d035c28a04faaa459b;

		public Pose c1c245528512f0ae5611e6e833a70158f;

		public Vector3F c5279e86c7170b16a26e0353879e2a1da;

		public Vector3F cdf467a98a98be94f79096be05a4cf352;

		public Pose c3abd454245895686438ef05ea6522677;

		public Pose cac2ea64f102d27596905e2b220bff881;

		public Aabb c09001c24f4c62ae4d590f9cbf93cf7f0;

		public c07a248008e037e41899c8b004ace7a50 c733442a0edfb185129ce9c019282191b;

		public Aabb c716ca7212944f8e17038e6e1452c5c70;

		public T c48988385aa2d683624d9802f508c1e31;

		public Pair<T> c8dacf44be5d7eb2dc701fa6dc59cf633;

		public IEnumerator<c07a248008e037e41899c8b004ace7a50> c353f6c7b770f28567f411fa063daec5c;

		public IEnumerator<T> c843969e5123cee8af26eb8da07f3ce40;

		Pair<T> IEnumerator<Pair<T>>.Current
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
		public caca1bf20c97f469f311050b95e74ccd9(int c94f69126adc708dfc5f5fd5b63c52835)
		{
			this.c94f69126adc708dfc5f5fd5b63c52835 = c94f69126adc708dfc5f5fd5b63c52835;
			c0e8a058e779916e7715d7616fb0da9e7 = Thread.CurrentThread.ManagedThreadId;
		}

		[DebuggerHidden]
		private IEnumerator<Pair<T>> c4bd63b962f346f189f5c6a4ad2e4ce8d()
		{
			caca1bf20c97f469f311050b95e74ccd9 caca1bf20c97f469f311050b95e74ccd10;
			if (Thread.CurrentThread.ManagedThreadId == c0e8a058e779916e7715d7616fb0da9e7)
			{
				while (true)
				{
					switch (1)
					{
					case 0:
						continue;
					}
					break;
				}
				if (1 == 0)
				{
					__ldtoken(DynamicAabbTree<>.caca1bf20c97f469f311050b95e74ccd9.c4bd63b962f346f189f5c6a4ad2e4ce8d());
				}
				if (c94f69126adc708dfc5f5fd5b63c52835 == -2)
				{
					while (true)
					{
						switch (2)
						{
						case 0:
							continue;
						}
						break;
					}
					c94f69126adc708dfc5f5fd5b63c52835 = 0;
					caca1bf20c97f469f311050b95e74ccd10 = this;
					goto IL_0059;
				}
			}
			caca1bf20c97f469f311050b95e74ccd10 = new caca1bf20c97f469f311050b95e74ccd9(0)
			{
				c1b0bbfd4af1b6a4c0c71a5dd3c509812 = c1b0bbfd4af1b6a4c0c71a5dd3c509812
			};
			goto IL_0059;
			IL_0059:
			caca1bf20c97f469f311050b95e74ccd10.ceabb47d7d959545d89c4300eabbdd7c2 = c6f203721b629fec4c93cc63ff1172fc4;
			caca1bf20c97f469f311050b95e74ccd10.cfd8883387afb9cc25a2ac9150de0f66b = cd1939b65df62ed5f7a2112466dabd8f0;
			caca1bf20c97f469f311050b95e74ccd10.c1b4918d9d3cc94171c67339d1fc78b0f = cfd54ba1e25c70994847ab953f2228207;
			caca1bf20c97f469f311050b95e74ccd10.cd2abebf7ff6ec7d035c28a04faaa459b = c1c245528512f0ae5611e6e833a70158f;
			return caca1bf20c97f469f311050b95e74ccd10;
		}

		IEnumerator<Pair<T>> IEnumerable<Pair<T>>.GetEnumerator()
		{
			//ILSpy generated this explicit interface implementation from .override directive in c4bd63b962f346f189f5c6a4ad2e4ce8d
			return this.c4bd63b962f346f189f5c6a4ad2e4ce8d();
		}

		[DebuggerHidden]
		private IEnumerator c63811cf6ff5c0b86c54fd5037c6af6ad()
		{
			return c4bd63b962f346f189f5c6a4ad2e4ce8d();
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
				int num = c94f69126adc708dfc5f5fd5b63c52835;
				if (num != 0)
				{
					while (true)
					{
						switch (1)
						{
						case 0:
							continue;
						}
						break;
					}
					if (1 == 0)
					{
						__ldtoken(DynamicAabbTree<>.caca1bf20c97f469f311050b95e74ccd9.MoveNext());
					}
					if (num != 3)
					{
						goto IL_0216;
					}
					c94f69126adc708dfc5f5fd5b63c52835 = 2;
					goto IL_01dc;
				}
				c94f69126adc708dfc5f5fd5b63c52835 = -1;
				c5279e86c7170b16a26e0353879e2a1da = Vector3F.One / ceabb47d7d959545d89c4300eabbdd7c2;
				cdf467a98a98be94f79096be05a4cf352 = Vector3F.One / c1b4918d9d3cc94171c67339d1fc78b0f;
				c3abd454245895686438ef05ea6522677 = cd2abebf7ff6ec7d035c28a04faaa459b;
				cac2ea64f102d27596905e2b220bff881 = cd2abebf7ff6ec7d035c28a04faaa459b.Inverse;
				c09001c24f4c62ae4d590f9cbf93cf7f0 = cfd8883387afb9cc25a2ac9150de0f66b.Aabb;
				c09001c24f4c62ae4d590f9cbf93cf7f0 = c09001c24f4c62ae4d590f9cbf93cf7f0.GetAabb(c1b4918d9d3cc94171c67339d1fc78b0f, c3abd454245895686438ef05ea6522677);
				c09001c24f4c62ae4d590f9cbf93cf7f0.Scale(c5279e86c7170b16a26e0353879e2a1da);
				c353f6c7b770f28567f411fa063daec5c = c1b0bbfd4af1b6a4c0c71a5dd3c509812.c4d76d858205a75f799980f42e41cbe5b(c09001c24f4c62ae4d590f9cbf93cf7f0).GetEnumerator();
				c94f69126adc708dfc5f5fd5b63c52835 = 1;
				goto IL_01fe;
				IL_01dc:
				while (c843969e5123cee8af26eb8da07f3ce40.MoveNext())
				{
					c48988385aa2d683624d9802f508c1e31 = c843969e5123cee8af26eb8da07f3ce40.Current;
					ref Pair<T> reference = ref c8dacf44be5d7eb2dc701fa6dc59cf633;
					reference = new Pair<T>(c733442a0edfb185129ce9c019282191b.c652f269a40a2519b8658ffbade581b4e, c48988385aa2d683624d9802f508c1e31);
					if (c1b0bbfd4af1b6a4c0c71a5dd3c509812.Filter != null)
					{
						while (true)
						{
							switch (2)
							{
							case 0:
								break;
							default:
								goto end_IL_0198;
							}
							continue;
							end_IL_0198:
							break;
						}
						if (!c1b0bbfd4af1b6a4c0c71a5dd3c509812.Filter.Filter(c8dacf44be5d7eb2dc701fa6dc59cf633))
						{
							continue;
						}
					}
					cd179d7f18da070d164af6afcb0af45b5 = c8dacf44be5d7eb2dc701fa6dc59cf633;
					c94f69126adc708dfc5f5fd5b63c52835 = 3;
					return true;
				}
				while (true)
				{
					switch (6)
					{
					case 0:
						continue;
					}
					break;
				}
				ca405a97f680015612a585ae4a9ed48be();
				goto IL_01fe;
				IL_0216:
				return false;
				IL_01fe:
				if (c353f6c7b770f28567f411fa063daec5c.MoveNext())
				{
					c733442a0edfb185129ce9c019282191b = c353f6c7b770f28567f411fa063daec5c.Current;
					c716ca7212944f8e17038e6e1452c5c70 = c733442a0edfb185129ce9c019282191b.c3e263e990bc8d6c3a2d636ac7a19c6c6.GetAabb(ceabb47d7d959545d89c4300eabbdd7c2, cac2ea64f102d27596905e2b220bff881);
					c716ca7212944f8e17038e6e1452c5c70.Scale(cdf467a98a98be94f79096be05a4cf352);
					c843969e5123cee8af26eb8da07f3ce40 = cfd8883387afb9cc25a2ac9150de0f66b.GetOverlaps(c716ca7212944f8e17038e6e1452c5c70).GetEnumerator();
					c94f69126adc708dfc5f5fd5b63c52835 = 2;
					goto IL_01dc;
				}
				c2290ec7e707de69340902b58848608a4();
				goto IL_0216;
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
			case 3:
				try
				{
					switch (c94f69126adc708dfc5f5fd5b63c52835)
					{
					case 2:
					case 3:
						try
						{
							break;
						}
						finally
						{
							ca405a97f680015612a585ae4a9ed48be();
						}
					}
					break;
				}
				finally
				{
					c2290ec7e707de69340902b58848608a4();
				}
			}
		}

		void IDisposable.Dispose()
		{
			//ILSpy generated this explicit interface implementation from .override directive in c087ddb9300ad4d53672b7a60e6d76672
			this.c087ddb9300ad4d53672b7a60e6d76672();
		}

		private void c2290ec7e707de69340902b58848608a4()
		{
			c94f69126adc708dfc5f5fd5b63c52835 = -1;
			if (c353f6c7b770f28567f411fa063daec5c == null)
			{
				return;
			}
			while (true)
			{
				switch (3)
				{
				case 0:
					continue;
				}
				if (1 == 0)
				{
					__ldtoken(DynamicAabbTree<>.caca1bf20c97f469f311050b95e74ccd9.c2290ec7e707de69340902b58848608a4());
				}
				c353f6c7b770f28567f411fa063daec5c.Dispose();
				return;
			}
		}

		private void ca405a97f680015612a585ae4a9ed48be()
		{
			c94f69126adc708dfc5f5fd5b63c52835 = 1;
			if (c843969e5123cee8af26eb8da07f3ce40 != null)
			{
				c843969e5123cee8af26eb8da07f3ce40.Dispose();
			}
		}
	}

	[CompilerGenerated]
	private sealed class c895d3751afb2627cd9239876fd166931 : IEnumerable<Pair<T>>, IEnumerator<Pair<T>>, IEnumerable, IEnumerator, IDisposable
	{
		private Pair<T> cd179d7f18da070d164af6afcb0af45b5;

		private int c94f69126adc708dfc5f5fd5b63c52835;

		private int c0e8a058e779916e7715d7616fb0da9e7;

		public DynamicAabbTree<T> c1b0bbfd4af1b6a4c0c71a5dd3c509812;

		public Vector3F ceabb47d7d959545d89c4300eabbdd7c2;

		public Vector3F c6f203721b629fec4c93cc63ff1172fc4;

		public DynamicAabbTree<T> ca9802b6266a4b983f751fb2d730561ca;

		public DynamicAabbTree<T> c6b71d229c51c908f777d8166aeaf9947;

		public Vector3F c1b4918d9d3cc94171c67339d1fc78b0f;

		public Vector3F cfd54ba1e25c70994847ab953f2228207;

		public Pose cd2abebf7ff6ec7d035c28a04faaa459b;

		public Pose c1c245528512f0ae5611e6e833a70158f;

		public Vector3F c78147cb3bdd4c4e4d334576ba3800f77;

		public Vector3F c8edeb5267f2c6d035291dad8f399a355;

		public Pose cddf4ef6a08101a45ae492b544e9becb2;

		public Stack<Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50>> c3da89fb1f00a3dce09e8cc25f08fa870;

		public Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50> ccdd24831a50d21a8c0c84ef43810efec;

		public c07a248008e037e41899c8b004ace7a50 c6ac31ebfa0ff25688a99389e8ca6e254;

		public c07a248008e037e41899c8b004ace7a50 c905ab1506f87f2a1d316df63de9fbc0d;

		public Pair<T> c16fdd38f63c45543e86673b29ccff3df;

		Pair<T> IEnumerator<Pair<T>>.Current
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
		public c895d3751afb2627cd9239876fd166931(int c94f69126adc708dfc5f5fd5b63c52835)
		{
			this.c94f69126adc708dfc5f5fd5b63c52835 = c94f69126adc708dfc5f5fd5b63c52835;
			c0e8a058e779916e7715d7616fb0da9e7 = Thread.CurrentThread.ManagedThreadId;
		}

		[DebuggerHidden]
		private IEnumerator<Pair<T>> c4bd63b962f346f189f5c6a4ad2e4ce8d()
		{
			c895d3751afb2627cd9239876fd166931 c895d3751afb2627cd9239876fd166932;
			if (Thread.CurrentThread.ManagedThreadId == c0e8a058e779916e7715d7616fb0da9e7 && c94f69126adc708dfc5f5fd5b63c52835 == -2)
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
					__ldtoken(DynamicAabbTree<>.c895d3751afb2627cd9239876fd166931.c4bd63b962f346f189f5c6a4ad2e4ce8d());
				}
				c94f69126adc708dfc5f5fd5b63c52835 = 0;
				c895d3751afb2627cd9239876fd166932 = this;
			}
			else
			{
				c895d3751afb2627cd9239876fd166932 = new c895d3751afb2627cd9239876fd166931(0)
				{
					c1b0bbfd4af1b6a4c0c71a5dd3c509812 = c1b0bbfd4af1b6a4c0c71a5dd3c509812
				};
			}
			c895d3751afb2627cd9239876fd166932.ceabb47d7d959545d89c4300eabbdd7c2 = c6f203721b629fec4c93cc63ff1172fc4;
			c895d3751afb2627cd9239876fd166932.ca9802b6266a4b983f751fb2d730561ca = c6b71d229c51c908f777d8166aeaf9947;
			c895d3751afb2627cd9239876fd166932.c1b4918d9d3cc94171c67339d1fc78b0f = cfd54ba1e25c70994847ab953f2228207;
			c895d3751afb2627cd9239876fd166932.cd2abebf7ff6ec7d035c28a04faaa459b = c1c245528512f0ae5611e6e833a70158f;
			return c895d3751afb2627cd9239876fd166932;
		}

		IEnumerator<Pair<T>> IEnumerable<Pair<T>>.GetEnumerator()
		{
			//ILSpy generated this explicit interface implementation from .override directive in c4bd63b962f346f189f5c6a4ad2e4ce8d
			return this.c4bd63b962f346f189f5c6a4ad2e4ce8d();
		}

		[DebuggerHidden]
		private IEnumerator c63811cf6ff5c0b86c54fd5037c6af6ad()
		{
			return c4bd63b962f346f189f5c6a4ad2e4ce8d();
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
				c78147cb3bdd4c4e4d334576ba3800f77 = ceabb47d7d959545d89c4300eabbdd7c2;
				c8edeb5267f2c6d035291dad8f399a355 = c1b4918d9d3cc94171c67339d1fc78b0f;
				cddf4ef6a08101a45ae492b544e9becb2 = cd2abebf7ff6ec7d035c28a04faaa459b;
				c3da89fb1f00a3dce09e8cc25f08fa870 = ResourcePools<Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50>>.Stacks.Obtain();
				c3da89fb1f00a3dce09e8cc25f08fa870.Push(new Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50>(c1b0bbfd4af1b6a4c0c71a5dd3c509812.c3cb2c0edc4d55dd1410e3483d1ef60d4, ca9802b6266a4b983f751fb2d730561ca.c3cb2c0edc4d55dd1410e3483d1ef60d4));
				goto IL_02c0;
			case 1:
				{
					c94f69126adc708dfc5f5fd5b63c52835 = -1;
					goto IL_02c0;
				}
				IL_02c0:
				while (c3da89fb1f00a3dce09e8cc25f08fa870.Count > 0)
				{
					ccdd24831a50d21a8c0c84ef43810efec = c3da89fb1f00a3dce09e8cc25f08fa870.Pop();
					c6ac31ebfa0ff25688a99389e8ca6e254 = ccdd24831a50d21a8c0c84ef43810efec.First;
					c905ab1506f87f2a1d316df63de9fbc0d = ccdd24831a50d21a8c0c84ef43810efec.Second;
					if (!DynamicAabbTree<T>.cd0cfe897426aab73c90b8217df390607(c6ac31ebfa0ff25688a99389e8ca6e254, c78147cb3bdd4c4e4d334576ba3800f77, c905ab1506f87f2a1d316df63de9fbc0d, c8edeb5267f2c6d035291dad8f399a355, cddf4ef6a08101a45ae492b544e9becb2))
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
					if (1 == 0)
					{
						__ldtoken(DynamicAabbTree<>.c895d3751afb2627cd9239876fd166931.MoveNext());
					}
					if (!c6ac31ebfa0ff25688a99389e8ca6e254.cfd73e16b19bb8bde65db991a787eb5ad)
					{
						if (!c905ab1506f87f2a1d316df63de9fbc0d.cfd73e16b19bb8bde65db991a787eb5ad)
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
							c3da89fb1f00a3dce09e8cc25f08fa870.Push(new Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50>(c6ac31ebfa0ff25688a99389e8ca6e254.c2d9d869d0c8fd5fb55f452375f580a4a, c905ab1506f87f2a1d316df63de9fbc0d.c2d9d869d0c8fd5fb55f452375f580a4a));
							c3da89fb1f00a3dce09e8cc25f08fa870.Push(new Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50>(c6ac31ebfa0ff25688a99389e8ca6e254.c6d3fb34101b9e1ce92055c2914d2d1de, c905ab1506f87f2a1d316df63de9fbc0d.c2d9d869d0c8fd5fb55f452375f580a4a));
							c3da89fb1f00a3dce09e8cc25f08fa870.Push(new Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50>(c6ac31ebfa0ff25688a99389e8ca6e254.c2d9d869d0c8fd5fb55f452375f580a4a, c905ab1506f87f2a1d316df63de9fbc0d.c6d3fb34101b9e1ce92055c2914d2d1de));
							c3da89fb1f00a3dce09e8cc25f08fa870.Push(new Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50>(c6ac31ebfa0ff25688a99389e8ca6e254.c6d3fb34101b9e1ce92055c2914d2d1de, c905ab1506f87f2a1d316df63de9fbc0d.c6d3fb34101b9e1ce92055c2914d2d1de));
						}
						else
						{
							c3da89fb1f00a3dce09e8cc25f08fa870.Push(new Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50>(c6ac31ebfa0ff25688a99389e8ca6e254.c2d9d869d0c8fd5fb55f452375f580a4a, c905ab1506f87f2a1d316df63de9fbc0d));
							c3da89fb1f00a3dce09e8cc25f08fa870.Push(new Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50>(c6ac31ebfa0ff25688a99389e8ca6e254.c6d3fb34101b9e1ce92055c2914d2d1de, c905ab1506f87f2a1d316df63de9fbc0d));
						}
					}
					else if (!c905ab1506f87f2a1d316df63de9fbc0d.cfd73e16b19bb8bde65db991a787eb5ad)
					{
						c3da89fb1f00a3dce09e8cc25f08fa870.Push(new Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50>(c6ac31ebfa0ff25688a99389e8ca6e254, c905ab1506f87f2a1d316df63de9fbc0d.c2d9d869d0c8fd5fb55f452375f580a4a));
						c3da89fb1f00a3dce09e8cc25f08fa870.Push(new Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50>(c6ac31ebfa0ff25688a99389e8ca6e254, c905ab1506f87f2a1d316df63de9fbc0d.c6d3fb34101b9e1ce92055c2914d2d1de));
					}
					else
					{
						ref Pair<T> reference = ref c16fdd38f63c45543e86673b29ccff3df;
						reference = new Pair<T>(c6ac31ebfa0ff25688a99389e8ca6e254.c652f269a40a2519b8658ffbade581b4e, c905ab1506f87f2a1d316df63de9fbc0d.c652f269a40a2519b8658ffbade581b4e);
						if (c1b0bbfd4af1b6a4c0c71a5dd3c509812.Filter == null || c1b0bbfd4af1b6a4c0c71a5dd3c509812.Filter.Filter(c16fdd38f63c45543e86673b29ccff3df))
						{
							cd179d7f18da070d164af6afcb0af45b5 = c16fdd38f63c45543e86673b29ccff3df;
							c94f69126adc708dfc5f5fd5b63c52835 = 1;
							return true;
						}
					}
				}
				while (true)
				{
					switch (4)
					{
					case 0:
						continue;
					}
					break;
				}
				ResourcePools<Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50>>.Stacks.Recycle(c3da89fb1f00a3dce09e8cc25f08fa870);
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

	[CompilerGenerated]
	private sealed class c7d96806d88c569e449caf11ddcd2f6dc : IEnumerable<T>, IEnumerator<T>, IEnumerable, IEnumerator, IDisposable
	{
		private T cd179d7f18da070d164af6afcb0af45b5;

		private int c94f69126adc708dfc5f5fd5b63c52835;

		private int c0e8a058e779916e7715d7616fb0da9e7;

		public DynamicAabbTree<T> c1b0bbfd4af1b6a4c0c71a5dd3c509812;

		public Aabb cdc3439410d15f28489c964ee87b88fe9;

		public Aabb cc3ae004bdecc8a5e2b7f6f0efd06d1d5;

		public Stack<c07a248008e037e41899c8b004ace7a50> c60ec67b86496bf391379bed071fa4321;

		public c07a248008e037e41899c8b004ace7a50 c0e81df5692cc091fa6cc2f3286324026;

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
		public c7d96806d88c569e449caf11ddcd2f6dc(int c94f69126adc708dfc5f5fd5b63c52835)
		{
			this.c94f69126adc708dfc5f5fd5b63c52835 = c94f69126adc708dfc5f5fd5b63c52835;
			c0e8a058e779916e7715d7616fb0da9e7 = Thread.CurrentThread.ManagedThreadId;
		}

		[DebuggerHidden]
		private IEnumerator<T> c0843ff4e059c4450bad00d5ba0b6ef48()
		{
			c7d96806d88c569e449caf11ddcd2f6dc c7d96806d88c569e449caf11ddcd2f6dc2;
			if (Thread.CurrentThread.ManagedThreadId == c0e8a058e779916e7715d7616fb0da9e7)
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
				if (1 == 0)
				{
					__ldtoken(DynamicAabbTree<>.c7d96806d88c569e449caf11ddcd2f6dc.c0843ff4e059c4450bad00d5ba0b6ef48());
				}
				if (c94f69126adc708dfc5f5fd5b63c52835 == -2)
				{
					c94f69126adc708dfc5f5fd5b63c52835 = 0;
					c7d96806d88c569e449caf11ddcd2f6dc2 = this;
					goto IL_0051;
				}
			}
			c7d96806d88c569e449caf11ddcd2f6dc2 = new c7d96806d88c569e449caf11ddcd2f6dc(0)
			{
				c1b0bbfd4af1b6a4c0c71a5dd3c509812 = c1b0bbfd4af1b6a4c0c71a5dd3c509812
			};
			goto IL_0051;
			IL_0051:
			c7d96806d88c569e449caf11ddcd2f6dc2.cdc3439410d15f28489c964ee87b88fe9 = cc3ae004bdecc8a5e2b7f6f0efd06d1d5;
			return c7d96806d88c569e449caf11ddcd2f6dc2;
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
			switch (c94f69126adc708dfc5f5fd5b63c52835)
			{
			case 0:
				c94f69126adc708dfc5f5fd5b63c52835 = -1;
				c1b0bbfd4af1b6a4c0c71a5dd3c509812.Update(forceRebuild: false);
				if (c1b0bbfd4af1b6a4c0c71a5dd3c509812.c3cb2c0edc4d55dd1410e3483d1ef60d4 == null)
				{
					break;
				}
				c60ec67b86496bf391379bed071fa4321 = ResourcePools<c07a248008e037e41899c8b004ace7a50>.Stacks.Obtain();
				c60ec67b86496bf391379bed071fa4321.Push(c1b0bbfd4af1b6a4c0c71a5dd3c509812.c3cb2c0edc4d55dd1410e3483d1ef60d4);
				goto IL_0106;
			case 1:
				{
					c94f69126adc708dfc5f5fd5b63c52835 = -1;
					goto IL_0106;
				}
				IL_0106:
				while (c60ec67b86496bf391379bed071fa4321.Count > 0)
				{
					c0e81df5692cc091fa6cc2f3286324026 = c60ec67b86496bf391379bed071fa4321.Pop();
					if (!GeometryHelper.HaveContact(c0e81df5692cc091fa6cc2f3286324026.c3e263e990bc8d6c3a2d636ac7a19c6c6, cdc3439410d15f28489c964ee87b88fe9))
					{
						continue;
					}
					if (c0e81df5692cc091fa6cc2f3286324026.cfd73e16b19bb8bde65db991a787eb5ad)
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
									__ldtoken(DynamicAabbTree<>.c7d96806d88c569e449caf11ddcd2f6dc.MoveNext());
								}
								cd179d7f18da070d164af6afcb0af45b5 = c0e81df5692cc091fa6cc2f3286324026.c652f269a40a2519b8658ffbade581b4e;
								c94f69126adc708dfc5f5fd5b63c52835 = 1;
								return true;
							}
						}
					}
					c60ec67b86496bf391379bed071fa4321.Push(c0e81df5692cc091fa6cc2f3286324026.c2d9d869d0c8fd5fb55f452375f580a4a);
					c60ec67b86496bf391379bed071fa4321.Push(c0e81df5692cc091fa6cc2f3286324026.c6d3fb34101b9e1ce92055c2914d2d1de);
				}
				ResourcePools<c07a248008e037e41899c8b004ace7a50>.Stacks.Recycle(c60ec67b86496bf391379bed071fa4321);
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

	[CompilerGenerated]
	private sealed class c1e9acb92d100bc5baf1f66966856de30 : IEnumerable<T>, IEnumerator<T>, IEnumerable, IEnumerator, IDisposable
	{
		private T cd179d7f18da070d164af6afcb0af45b5;

		private int c94f69126adc708dfc5f5fd5b63c52835;

		private int c0e8a058e779916e7715d7616fb0da9e7;

		public DynamicAabbTree<T> c1b0bbfd4af1b6a4c0c71a5dd3c509812;

		public c07a248008e037e41899c8b004ace7a50 ce9beaab8ed4de70dd0e96a36b4c42105;

		public c07a248008e037e41899c8b004ace7a50 ca8b7e7cbb5469712cd98441c75a65ad8;

		public Stack<c07a248008e037e41899c8b004ace7a50> c6b873fb12adcd56b541329d39efa6daa;

		public c07a248008e037e41899c8b004ace7a50 c572cf7344a04b7a54f8f90272f29db08;

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
		public c1e9acb92d100bc5baf1f66966856de30(int c94f69126adc708dfc5f5fd5b63c52835)
		{
			this.c94f69126adc708dfc5f5fd5b63c52835 = c94f69126adc708dfc5f5fd5b63c52835;
			c0e8a058e779916e7715d7616fb0da9e7 = Thread.CurrentThread.ManagedThreadId;
		}

		[DebuggerHidden]
		private IEnumerator<T> c0843ff4e059c4450bad00d5ba0b6ef48()
		{
			c1e9acb92d100bc5baf1f66966856de30 c1e9acb92d100bc5baf1f66966856de31;
			if (Thread.CurrentThread.ManagedThreadId == c0e8a058e779916e7715d7616fb0da9e7 && c94f69126adc708dfc5f5fd5b63c52835 == -2)
			{
				c94f69126adc708dfc5f5fd5b63c52835 = 0;
				c1e9acb92d100bc5baf1f66966856de31 = this;
			}
			else
			{
				c1e9acb92d100bc5baf1f66966856de31 = new c1e9acb92d100bc5baf1f66966856de30(0)
				{
					c1b0bbfd4af1b6a4c0c71a5dd3c509812 = c1b0bbfd4af1b6a4c0c71a5dd3c509812
				};
			}
			c1e9acb92d100bc5baf1f66966856de31.ce9beaab8ed4de70dd0e96a36b4c42105 = ca8b7e7cbb5469712cd98441c75a65ad8;
			return c1e9acb92d100bc5baf1f66966856de31;
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
			switch (c94f69126adc708dfc5f5fd5b63c52835)
			{
			case 0:
				c94f69126adc708dfc5f5fd5b63c52835 = -1;
				c1b0bbfd4af1b6a4c0c71a5dd3c509812.Update(forceRebuild: false);
				if (c1b0bbfd4af1b6a4c0c71a5dd3c509812.c3cb2c0edc4d55dd1410e3483d1ef60d4 == null)
				{
					break;
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
					__ldtoken(DynamicAabbTree<>.c1e9acb92d100bc5baf1f66966856de30.MoveNext());
				}
				c6b873fb12adcd56b541329d39efa6daa = ResourcePools<c07a248008e037e41899c8b004ace7a50>.Stacks.Obtain();
				c6b873fb12adcd56b541329d39efa6daa.Push(c1b0bbfd4af1b6a4c0c71a5dd3c509812.c3cb2c0edc4d55dd1410e3483d1ef60d4);
				goto IL_0128;
			case 1:
				{
					c94f69126adc708dfc5f5fd5b63c52835 = -1;
					goto IL_0128;
				}
				IL_0128:
				while (c6b873fb12adcd56b541329d39efa6daa.Count > 0)
				{
					c572cf7344a04b7a54f8f90272f29db08 = c6b873fb12adcd56b541329d39efa6daa.Pop();
					if (c572cf7344a04b7a54f8f90272f29db08 == ce9beaab8ed4de70dd0e96a36b4c42105 || !GeometryHelper.HaveContact(c572cf7344a04b7a54f8f90272f29db08.c3e263e990bc8d6c3a2d636ac7a19c6c6, ce9beaab8ed4de70dd0e96a36b4c42105.c3e263e990bc8d6c3a2d636ac7a19c6c6))
					{
						continue;
					}
					if (c572cf7344a04b7a54f8f90272f29db08.cfd73e16b19bb8bde65db991a787eb5ad)
					{
						while (true)
						{
							switch (3)
							{
							case 0:
								break;
							default:
								cd179d7f18da070d164af6afcb0af45b5 = c572cf7344a04b7a54f8f90272f29db08.c652f269a40a2519b8658ffbade581b4e;
								c94f69126adc708dfc5f5fd5b63c52835 = 1;
								return true;
							}
						}
					}
					c6b873fb12adcd56b541329d39efa6daa.Push(c572cf7344a04b7a54f8f90272f29db08.c2d9d869d0c8fd5fb55f452375f580a4a);
					c6b873fb12adcd56b541329d39efa6daa.Push(c572cf7344a04b7a54f8f90272f29db08.c6d3fb34101b9e1ce92055c2914d2d1de);
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
				ResourcePools<c07a248008e037e41899c8b004ace7a50>.Stacks.Recycle(c6b873fb12adcd56b541329d39efa6daa);
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

	[CompilerGenerated]
	private sealed class ccdf73ae11d78eac711a5686f9b1906f4 : IEnumerable<c07a248008e037e41899c8b004ace7a50>, IEnumerator<c07a248008e037e41899c8b004ace7a50>, IEnumerable, IEnumerator, IDisposable
	{
		private c07a248008e037e41899c8b004ace7a50 cd179d7f18da070d164af6afcb0af45b5;

		private int c94f69126adc708dfc5f5fd5b63c52835;

		private int c0e8a058e779916e7715d7616fb0da9e7;

		public DynamicAabbTree<T> c1b0bbfd4af1b6a4c0c71a5dd3c509812;

		public Aabb cdc3439410d15f28489c964ee87b88fe9;

		public Aabb cc3ae004bdecc8a5e2b7f6f0efd06d1d5;

		public Stack<c07a248008e037e41899c8b004ace7a50> cc61cdbd80b9217ded227d4a36946e29a;

		public c07a248008e037e41899c8b004ace7a50 cd81d6538633a78743d4a40cdee053e95;

		c07a248008e037e41899c8b004ace7a50 IEnumerator<c07a248008e037e41899c8b004ace7a50>.Current
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
		public ccdf73ae11d78eac711a5686f9b1906f4(int c94f69126adc708dfc5f5fd5b63c52835)
		{
			this.c94f69126adc708dfc5f5fd5b63c52835 = c94f69126adc708dfc5f5fd5b63c52835;
			c0e8a058e779916e7715d7616fb0da9e7 = Thread.CurrentThread.ManagedThreadId;
		}

		[DebuggerHidden]
		private IEnumerator<c07a248008e037e41899c8b004ace7a50> c19b448e520151539c6c0d8bbd9d7e04a()
		{
			ccdf73ae11d78eac711a5686f9b1906f4 ccdf73ae11d78eac711a5686f9b1906f5;
			if (Thread.CurrentThread.ManagedThreadId == c0e8a058e779916e7715d7616fb0da9e7)
			{
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
					__ldtoken(DynamicAabbTree<>.ccdf73ae11d78eac711a5686f9b1906f4.c19b448e520151539c6c0d8bbd9d7e04a());
				}
				if (c94f69126adc708dfc5f5fd5b63c52835 == -2)
				{
					c94f69126adc708dfc5f5fd5b63c52835 = 0;
					ccdf73ae11d78eac711a5686f9b1906f5 = this;
					goto IL_004f;
				}
			}
			ccdf73ae11d78eac711a5686f9b1906f5 = new ccdf73ae11d78eac711a5686f9b1906f4(0)
			{
				c1b0bbfd4af1b6a4c0c71a5dd3c509812 = c1b0bbfd4af1b6a4c0c71a5dd3c509812
			};
			goto IL_004f;
			IL_004f:
			ccdf73ae11d78eac711a5686f9b1906f5.cdc3439410d15f28489c964ee87b88fe9 = cc3ae004bdecc8a5e2b7f6f0efd06d1d5;
			return ccdf73ae11d78eac711a5686f9b1906f5;
		}

		IEnumerator<c07a248008e037e41899c8b004ace7a50> IEnumerable<c07a248008e037e41899c8b004ace7a50>.GetEnumerator()
		{
			//ILSpy generated this explicit interface implementation from .override directive in c19b448e520151539c6c0d8bbd9d7e04a
			return this.c19b448e520151539c6c0d8bbd9d7e04a();
		}

		[DebuggerHidden]
		private IEnumerator c63811cf6ff5c0b86c54fd5037c6af6ad()
		{
			return c19b448e520151539c6c0d8bbd9d7e04a();
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
				c1b0bbfd4af1b6a4c0c71a5dd3c509812.Update(forceRebuild: false);
				if (c1b0bbfd4af1b6a4c0c71a5dd3c509812.c3cb2c0edc4d55dd1410e3483d1ef60d4 == null)
				{
					break;
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
				if (1 == 0)
				{
					__ldtoken(DynamicAabbTree<>.ccdf73ae11d78eac711a5686f9b1906f4.MoveNext());
				}
				cc61cdbd80b9217ded227d4a36946e29a = ResourcePools<c07a248008e037e41899c8b004ace7a50>.Stacks.Obtain();
				cc61cdbd80b9217ded227d4a36946e29a.Push(c1b0bbfd4af1b6a4c0c71a5dd3c509812.c3cb2c0edc4d55dd1410e3483d1ef60d4);
				goto IL_0111;
			case 1:
				{
					c94f69126adc708dfc5f5fd5b63c52835 = -1;
					goto IL_0111;
				}
				IL_0111:
				while (cc61cdbd80b9217ded227d4a36946e29a.Count > 0)
				{
					cd81d6538633a78743d4a40cdee053e95 = cc61cdbd80b9217ded227d4a36946e29a.Pop();
					if (!GeometryHelper.HaveContact(cd81d6538633a78743d4a40cdee053e95.c3e263e990bc8d6c3a2d636ac7a19c6c6, cdc3439410d15f28489c964ee87b88fe9))
					{
						continue;
					}
					while (true)
					{
						switch (4)
						{
						case 0:
							continue;
						}
						break;
					}
					if (cd81d6538633a78743d4a40cdee053e95.cfd73e16b19bb8bde65db991a787eb5ad)
					{
						while (true)
						{
							switch (7)
							{
							case 0:
								break;
							default:
								cd179d7f18da070d164af6afcb0af45b5 = cd81d6538633a78743d4a40cdee053e95;
								c94f69126adc708dfc5f5fd5b63c52835 = 1;
								return true;
							}
						}
					}
					cc61cdbd80b9217ded227d4a36946e29a.Push(cd81d6538633a78743d4a40cdee053e95.c2d9d869d0c8fd5fb55f452375f580a4a);
					cc61cdbd80b9217ded227d4a36946e29a.Push(cd81d6538633a78743d4a40cdee053e95.c6d3fb34101b9e1ce92055c2914d2d1de);
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
				ResourcePools<c07a248008e037e41899c8b004ace7a50>.Stacks.Recycle(cc61cdbd80b9217ded227d4a36946e29a);
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

	[CompilerGenerated]
	private sealed class ccce8bde3a443c4f5864a0c2100fcf735 : IEnumerable<T>, IEnumerator<T>, IEnumerable, IEnumerator, IDisposable
	{
		private T cd179d7f18da070d164af6afcb0af45b5;

		private int c94f69126adc708dfc5f5fd5b63c52835;

		private int c0e8a058e779916e7715d7616fb0da9e7;

		public DynamicAabbTree<T> c1b0bbfd4af1b6a4c0c71a5dd3c509812;

		public Ray c2d769fcfe2c7e7844e7de692907680c8;

		public Ray c8b1773219813f762be9a1bd49ee5cb56;

		public Vector3F c1219f1a4ca7941b5a0eaec7f1b079ede;

		public float c66888ba7dfd7d5b5265007b118f4cbf1;

		public Stack<c07a248008e037e41899c8b004ace7a50> cf0fa28cc5c1479e9fba49caf9f51782d;

		public c07a248008e037e41899c8b004ace7a50 c378928aec610c9312f758d5396bb1439;

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
		public ccce8bde3a443c4f5864a0c2100fcf735(int c94f69126adc708dfc5f5fd5b63c52835)
		{
			this.c94f69126adc708dfc5f5fd5b63c52835 = c94f69126adc708dfc5f5fd5b63c52835;
			c0e8a058e779916e7715d7616fb0da9e7 = Thread.CurrentThread.ManagedThreadId;
		}

		[DebuggerHidden]
		private IEnumerator<T> c0843ff4e059c4450bad00d5ba0b6ef48()
		{
			ccce8bde3a443c4f5864a0c2100fcf735 ccce8bde3a443c4f5864a0c2100fcf736;
			if (Thread.CurrentThread.ManagedThreadId == c0e8a058e779916e7715d7616fb0da9e7 && c94f69126adc708dfc5f5fd5b63c52835 == -2)
			{
				c94f69126adc708dfc5f5fd5b63c52835 = 0;
				ccce8bde3a443c4f5864a0c2100fcf736 = this;
			}
			else
			{
				ccce8bde3a443c4f5864a0c2100fcf736 = new ccce8bde3a443c4f5864a0c2100fcf735(0)
				{
					c1b0bbfd4af1b6a4c0c71a5dd3c509812 = c1b0bbfd4af1b6a4c0c71a5dd3c509812
				};
			}
			ccce8bde3a443c4f5864a0c2100fcf736.c2d769fcfe2c7e7844e7de692907680c8 = c8b1773219813f762be9a1bd49ee5cb56;
			return ccce8bde3a443c4f5864a0c2100fcf736;
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
			switch (c94f69126adc708dfc5f5fd5b63c52835)
			{
			case 0:
			{
				c94f69126adc708dfc5f5fd5b63c52835 = -1;
				c1b0bbfd4af1b6a4c0c71a5dd3c509812.Update(forceRebuild: false);
				if (c1b0bbfd4af1b6a4c0c71a5dd3c509812.c3cb2c0edc4d55dd1410e3483d1ef60d4 == null)
				{
					break;
				}
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
					__ldtoken(DynamicAabbTree<>.ccce8bde3a443c4f5864a0c2100fcf735.MoveNext());
				}
				ref Vector3F reference = ref c1219f1a4ca7941b5a0eaec7f1b079ede;
				reference = new Vector3F(1f / c2d769fcfe2c7e7844e7de692907680c8.Direction.X, 1f / c2d769fcfe2c7e7844e7de692907680c8.Direction.Y, 1f / c2d769fcfe2c7e7844e7de692907680c8.Direction.Z);
				c66888ba7dfd7d5b5265007b118f4cbf1 = Numeric.EpsilonF * (1f + c1b0bbfd4af1b6a4c0c71a5dd3c509812.Aabb.Extent.Length);
				cf0fa28cc5c1479e9fba49caf9f51782d = ResourcePools<c07a248008e037e41899c8b004ace7a50>.Stacks.Obtain();
				cf0fa28cc5c1479e9fba49caf9f51782d.Push(c1b0bbfd4af1b6a4c0c71a5dd3c509812.c3cb2c0edc4d55dd1410e3483d1ef60d4);
				goto IL_01a7;
			}
			case 1:
				{
					c94f69126adc708dfc5f5fd5b63c52835 = -1;
					goto IL_01a7;
				}
				IL_01a7:
				while (cf0fa28cc5c1479e9fba49caf9f51782d.Count > 0)
				{
					c378928aec610c9312f758d5396bb1439 = cf0fa28cc5c1479e9fba49caf9f51782d.Pop();
					if (GeometryHelper.cdd6d17271fec83883494ba8e43eede6a(c378928aec610c9312f758d5396bb1439.c3e263e990bc8d6c3a2d636ac7a19c6c6, c2d769fcfe2c7e7844e7de692907680c8.Origin, c1219f1a4ca7941b5a0eaec7f1b079ede, c2d769fcfe2c7e7844e7de692907680c8.Length, c66888ba7dfd7d5b5265007b118f4cbf1))
					{
						if (c378928aec610c9312f758d5396bb1439.cfd73e16b19bb8bde65db991a787eb5ad)
						{
							cd179d7f18da070d164af6afcb0af45b5 = c378928aec610c9312f758d5396bb1439.c652f269a40a2519b8658ffbade581b4e;
							c94f69126adc708dfc5f5fd5b63c52835 = 1;
							return true;
						}
						cf0fa28cc5c1479e9fba49caf9f51782d.Push(c378928aec610c9312f758d5396bb1439.c2d9d869d0c8fd5fb55f452375f580a4a);
						cf0fa28cc5c1479e9fba49caf9f51782d.Push(c378928aec610c9312f758d5396bb1439.c6d3fb34101b9e1ce92055c2914d2d1de);
					}
				}
				ResourcePools<c07a248008e037e41899c8b004ace7a50>.Stacks.Recycle(cf0fa28cc5c1479e9fba49caf9f51782d);
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

	private int ca72616798115178580ff53362112e351 = 128;

	private static readonly ResourcePool<c07a248008e037e41899c8b004ace7a50> c53fef33d7344726da08aeb063ec62121;

	private c07a248008e037e41899c8b004ace7a50 c3cb2c0edc4d55dd1410e3483d1ef60d4;

	private readonly c2313695eacdcb5ef30a3559ae721d868 cd311d3823f638b023eacb767b312cc3e = new c2313695eacdcb5ef30a3559ae721d868();

	private uint ceeb04bdde059d62834089429803fd6fd;

	private int c227cf8833b514efe5292f908b9579bb7;

	private float c4c5ce99a0b7c5f54b27211cfa6309d3c = 0.01f;

	private bool cf815c97786756b49e4fe2c44c0d6e959;

	private float ce015ddaf0368bba91d93d9ba6f4f4c00 = 0.05f;

	private float c90b5544119421efab2cd40c387c03d98 = 1f;

	[CompilerGenerated]
	private static Func<cbe5649b18b8b0caa00be9ad49188b193<T>> cc987207a074d1e8cac03f91dc517e783;

	[CompilerGenerated]
	private static Func<c07a248008e037e41899c8b004ace7a50> c745f9894b50d79a5d1f14fce7e21d482;

	[CompilerGenerated]
	private static Action<c07a248008e037e41899c8b004ace7a50> c685c37d36b63d7a12d8fc88f1ff6f7c1;

	/// <summary>
	///       Gets or sets the threshold that determines when a bottom-up tree build method is used.
	///       </summary>
	public int BottomUpBuildThreshold
	{
		get
		{
			return ca72616798115178580ff53362112e351;
		}
		set
		{
			ca72616798115178580ff53362112e351 = value;
		}
	}

	/// <summary>
	///       Gets or set the amount of incremental optimization per frame (time step).
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is out of range. Allowed range is [0, 1].
	///       </exception>
	public float OptimizationPerFrame
	{
		get
		{
			return c4c5ce99a0b7c5f54b27211cfa6309d3c;
		}
		set
		{
			if (!(value < 0f))
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
					__ldtoken(DynamicAabbTree<>.set_OptimizationPerFrame(float));
				}
				if (!(1f < value))
				{
					c4c5ce99a0b7c5f54b27211cfa6309d3c = value;
					return;
				}
			}
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1040), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1051));
		}
	}

	/// <summary>
	///       Gets a value indicating whether motion prediction is enabled.
	///       </summary>
	public bool EnableMotionPrediction
	{
		get
		{
			return cf815c97786756b49e4fe2c44c0d6e959;
		}
		set
		{
			if (cf815c97786756b49e4fe2c44c0d6e959 == value)
			{
				return;
			}
			cf815c97786756b49e4fe2c44c0d6e959 = value;
			if (cf815c97786756b49e4fe2c44c0d6e959)
			{
				return;
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
					__ldtoken(DynamicAabbTree<>.set_EnableMotionPrediction(bool));
				}
				Invalidate();
				return;
			}
		}
	}

	/// <summary>
	///       Gets or sets a relative margin that is added to the AABB of moving objects. (Only applied
	///       if motion prediction is enabled.)
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is out of range. Allowed range is [0, 1].
	///       </exception>
	public float RelativeMargin
	{
		get
		{
			return ce015ddaf0368bba91d93d9ba6f4f4c00;
		}
		set
		{
			if (value < 0f || 1f < value)
			{
				throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1040), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1174));
			}
			ce015ddaf0368bba91d93d9ba6f4f4c00 = value;
		}
	}

	/// <summary>
	///       Gets or sets the number of frames a linear motion is predicted into the future. (Only 
	///       applied if motion prediction is enabled.)
	///       </summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is negative.
	///       </exception>
	public float MotionPrediction
	{
		get
		{
			return c90b5544119421efab2cd40c387c03d98;
		}
		set
		{
			if (value <= 0f)
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
							__ldtoken(DynamicAabbTree<>.set_MotionPrediction(float));
						}
						throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1040), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1263));
					}
				}
			}
			c90b5544119421efab2cd40c387c03d98 = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Partitioning.DynamicAabbTree`1" /> class</summary>
	public DynamicAabbTree()
	{
	}

	static DynamicAabbTree()
	{
		if (c745f9894b50d79a5d1f14fce7e21d482 == null)
		{
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
				__ldtoken(DynamicAabbTree<>._002Ecctor());
			}
			c745f9894b50d79a5d1f14fce7e21d482 = () => new c07a248008e037e41899c8b004ace7a50();
		}
		Func<c07a248008e037e41899c8b004ace7a50> create = c745f9894b50d79a5d1f14fce7e21d482;
		if (c685c37d36b63d7a12d8fc88f1ff6f7c1 == null)
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
			c685c37d36b63d7a12d8fc88f1ff6f7c1 = (c07a248008e037e41899c8b004ace7a50 c7f587bd57d47897172f47483e6bcca68) =>
			{
				c7f587bd57d47897172f47483e6bcca68.c652f269a40a2519b8658ffbade581b4e = default;
				c7f587bd57d47897172f47483e6bcca68.c6d3fb34101b9e1ce92055c2914d2d1de = null;
				c7f587bd57d47897172f47483e6bcca68.c2d9d869d0c8fd5fb55f452375f580a4a = null;
				c7f587bd57d47897172f47483e6bcca68.c551fa69e7c0ec492ab91daa4199210a2 = null;
			};
		}
		c53fef33d7344726da08aeb063ec62121 = new ResourcePool<c07a248008e037e41899c8b004ace7a50>(create, null, c685c37d36b63d7a12d8fc88f1ff6f7c1);
	}

	private void ccf1870247a728556a633ff212007937e()
	{
		if (c3cb2c0edc4d55dd1410e3483d1ef60d4 != null)
		{
			IEnumerator<cbe5649b18b8b0caa00be9ad49188b193<T>> enumerator = c3cb2c0edc4d55dd1410e3483d1ef60d4.c203a21e1e7a1a0c4a86e2ff8bf4b61c1().GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					c07a248008e037e41899c8b004ace7a50 item = (c07a248008e037e41899c8b004ace7a50)enumerator.Current;
					c53fef33d7344726da08aeb063ec62121.Recycle(item);
				}
				while (true)
				{
					switch (3)
					{
					case 0:
						continue;
					}
					if (1 == 0)
					{
						__ldtoken(DynamicAabbTree<>.ccf1870247a728556a633ff212007937e());
					}
					break;
				}
			}
			finally
			{
				if (enumerator != null)
				{
					while (true)
					{
						switch (2)
						{
						case 0:
							continue;
						}
						enumerator.Dispose();
						break;
					}
				}
			}
			c3cb2c0edc4d55dd1410e3483d1ef60d4 = null;
			cd311d3823f638b023eacb767b312cc3e.Clear();
		}
		if (Count == 0)
		{
			return;
		}
		if (Count == 1)
		{
			T val = c0e601bcddbab14811b8bae6ae4b7b274[0];
			c3cb2c0edc4d55dd1410e3483d1ef60d4 = c53fef33d7344726da08aeb063ec62121.Obtain();
			c3cb2c0edc4d55dd1410e3483d1ef60d4.c3e263e990bc8d6c3a2d636ac7a19c6c6 = GetAabbForItem(val);
			c3cb2c0edc4d55dd1410e3483d1ef60d4.c652f269a40a2519b8658ffbade581b4e = val;
			cd311d3823f638b023eacb767b312cc3e.Add(c3cb2c0edc4d55dd1410e3483d1ef60d4);
			return;
		}
		int count = c0e601bcddbab14811b8bae6ae4b7b274.Count;
		for (int i = 0; i < count; i++)
		{
			T val2 = c0e601bcddbab14811b8bae6ae4b7b274[i];
			c07a248008e037e41899c8b004ace7a50 c07a248008e037e41899c8b004ace7a51 = c53fef33d7344726da08aeb063ec62121.Obtain();
			c07a248008e037e41899c8b004ace7a51.c3e263e990bc8d6c3a2d636ac7a19c6c6 = GetAabbForItem(val2);
			c07a248008e037e41899c8b004ace7a51.c652f269a40a2519b8658ffbade581b4e = val2;
			cd311d3823f638b023eacb767b312cc3e.Add(c07a248008e037e41899c8b004ace7a51);
		}
		List<cbe5649b18b8b0caa00be9ad49188b193<T>> list = ResourcePools<cbe5649b18b8b0caa00be9ad49188b193<T>>.Lists.Obtain();
		for (int j = 0; j < count; j++)
		{
			list.Add(cd311d3823f638b023eacb767b312cc3e[j]);
		}
		while (true)
		{
			switch (1)
			{
			case 0:
				continue;
			}
			if (cc987207a074d1e8cac03f91dc517e783 == null)
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
				cc987207a074d1e8cac03f91dc517e783 = () => c53fef33d7344726da08aeb063ec62121.Obtain();
			}
			c3cb2c0edc4d55dd1410e3483d1ef60d4 = (c07a248008e037e41899c8b004ace7a50)c5c139e98a37beb65b1cb5aea8ce81daf.ccf1870247a728556a633ff212007937e(list, cc987207a074d1e8cac03f91dc517e783, BottomUpBuildThreshold);
			ResourcePools<cbe5649b18b8b0caa00be9ad49188b193<T>>.Lists.Recycle(list);
			return;
		}
	}

	private bool c4331eef59d25a795f3b70890347a4c6c(c07a248008e037e41899c8b004ace7a50 c7f587bd57d47897172f47483e6bcca68, HashSet<T> c7a74c15b192971583f3f676b15968063)
	{
		bool flag = false;
		if (c7f587bd57d47897172f47483e6bcca68.cfd73e16b19bb8bde65db991a787eb5ad)
		{
			if (c7a74c15b192971583f3f676b15968063 == null || c7a74c15b192971583f3f676b15968063.Contains(c7f587bd57d47897172f47483e6bcca68.c652f269a40a2519b8658ffbade581b4e))
			{
				c7f587bd57d47897172f47483e6bcca68.c3e263e990bc8d6c3a2d636ac7a19c6c6 = GetAabbForItem(c7f587bd57d47897172f47483e6bcca68.c652f269a40a2519b8658ffbade581b4e);
				flag = true;
			}
		}
		else
		{
			bool flag2 = c4331eef59d25a795f3b70890347a4c6c(c7f587bd57d47897172f47483e6bcca68.c6d3fb34101b9e1ce92055c2914d2d1de, c7a74c15b192971583f3f676b15968063);
			bool flag3 = c4331eef59d25a795f3b70890347a4c6c(c7f587bd57d47897172f47483e6bcca68.c2d9d869d0c8fd5fb55f452375f580a4a, c7a74c15b192971583f3f676b15968063);
			int num;
			if (!flag2)
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
				if (1 == 0)
				{
					__ldtoken(DynamicAabbTree<>.c4331eef59d25a795f3b70890347a4c6c(c07a248008e037e41899c8b004ace7a50, HashSet<T>));
				}
				num = (flag3 ? 1 : 0);
			}
			else
			{
				num = 1;
			}
			flag = (byte)num != 0;
			if (flag)
			{
				Aabb c3e263e990bc8d6c3a2d636ac7a19c6c = c7f587bd57d47897172f47483e6bcca68.c6d3fb34101b9e1ce92055c2914d2d1de.c3e263e990bc8d6c3a2d636ac7a19c6c6;
				c3e263e990bc8d6c3a2d636ac7a19c6c.Grow(c7f587bd57d47897172f47483e6bcca68.c2d9d869d0c8fd5fb55f452375f580a4a.c3e263e990bc8d6c3a2d636ac7a19c6c6);
				c7f587bd57d47897172f47483e6bcca68.c3e263e990bc8d6c3a2d636ac7a19c6c6 = c3e263e990bc8d6c3a2d636ac7a19c6c;
			}
		}
		return flag;
	}

	private void c437cc650c1bbb3b5d6a346455cd9229c(c07a248008e037e41899c8b004ace7a50 cf3a7a02cd7f4211ef47c16b8b2f5064d, c07a248008e037e41899c8b004ace7a50 ce9beaab8ed4de70dd0e96a36b4c42105)
	{
		if (c3cb2c0edc4d55dd1410e3483d1ef60d4 == null)
		{
			c3cb2c0edc4d55dd1410e3483d1ef60d4 = ce9beaab8ed4de70dd0e96a36b4c42105;
			return;
		}
		c07a248008e037e41899c8b004ace7a50 c07a248008e037e41899c8b004ace7a51 = cf3a7a02cd7f4211ef47c16b8b2f5064d;
		while (!c07a248008e037e41899c8b004ace7a51.cfd73e16b19bb8bde65db991a787eb5ad)
		{
			c07a248008e037e41899c8b004ace7a51 = ((c1706d55e3116d38c16c012a797e57db7.c22645a0ac256427f72ed4eca4cbca2b7(ce9beaab8ed4de70dd0e96a36b4c42105.c3e263e990bc8d6c3a2d636ac7a19c6c6, c07a248008e037e41899c8b004ace7a51.c6d3fb34101b9e1ce92055c2914d2d1de.c3e263e990bc8d6c3a2d636ac7a19c6c6, c07a248008e037e41899c8b004ace7a51.c2d9d869d0c8fd5fb55f452375f580a4a.c3e263e990bc8d6c3a2d636ac7a19c6c6) == 0) ? c07a248008e037e41899c8b004ace7a51.c6d3fb34101b9e1ce92055c2914d2d1de : c07a248008e037e41899c8b004ace7a51.c2d9d869d0c8fd5fb55f452375f580a4a);
		}
		c07a248008e037e41899c8b004ace7a50 c551fa69e7c0ec492ab91daa4199210a = c07a248008e037e41899c8b004ace7a51.c551fa69e7c0ec492ab91daa4199210a2;
		c07a248008e037e41899c8b004ace7a50 c07a248008e037e41899c8b004ace7a52 = c53fef33d7344726da08aeb063ec62121.Obtain();
		c07a248008e037e41899c8b004ace7a52.c3e263e990bc8d6c3a2d636ac7a19c6c6 = Aabb.Merge(c07a248008e037e41899c8b004ace7a51.c3e263e990bc8d6c3a2d636ac7a19c6c6, ce9beaab8ed4de70dd0e96a36b4c42105.c3e263e990bc8d6c3a2d636ac7a19c6c6);
		c07a248008e037e41899c8b004ace7a52.c551fa69e7c0ec492ab91daa4199210a2 = c551fa69e7c0ec492ab91daa4199210a;
		c07a248008e037e41899c8b004ace7a52.c6d3fb34101b9e1ce92055c2914d2d1de = c07a248008e037e41899c8b004ace7a51;
		c07a248008e037e41899c8b004ace7a52.c2d9d869d0c8fd5fb55f452375f580a4a = ce9beaab8ed4de70dd0e96a36b4c42105;
		c07a248008e037e41899c8b004ace7a51.c551fa69e7c0ec492ab91daa4199210a2 = c07a248008e037e41899c8b004ace7a52;
		ce9beaab8ed4de70dd0e96a36b4c42105.c551fa69e7c0ec492ab91daa4199210a2 = c07a248008e037e41899c8b004ace7a52;
		if (c551fa69e7c0ec492ab91daa4199210a == null)
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
						__ldtoken(DynamicAabbTree<>.c437cc650c1bbb3b5d6a346455cd9229c(c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50));
					}
					c3cb2c0edc4d55dd1410e3483d1ef60d4 = c07a248008e037e41899c8b004ace7a52;
					return;
				}
			}
		}
		if (c551fa69e7c0ec492ab91daa4199210a.c6d3fb34101b9e1ce92055c2914d2d1de == c07a248008e037e41899c8b004ace7a51)
		{
			c551fa69e7c0ec492ab91daa4199210a.c6d3fb34101b9e1ce92055c2914d2d1de = c07a248008e037e41899c8b004ace7a52;
		}
		else
		{
			c551fa69e7c0ec492ab91daa4199210a.c2d9d869d0c8fd5fb55f452375f580a4a = c07a248008e037e41899c8b004ace7a52;
		}
		while (!c551fa69e7c0ec492ab91daa4199210a.c3e263e990bc8d6c3a2d636ac7a19c6c6.Contains(c07a248008e037e41899c8b004ace7a52.c3e263e990bc8d6c3a2d636ac7a19c6c6))
		{
			c551fa69e7c0ec492ab91daa4199210a.c3e263e990bc8d6c3a2d636ac7a19c6c6 = Aabb.Merge(c551fa69e7c0ec492ab91daa4199210a.c6d3fb34101b9e1ce92055c2914d2d1de.c3e263e990bc8d6c3a2d636ac7a19c6c6, c551fa69e7c0ec492ab91daa4199210a.c2d9d869d0c8fd5fb55f452375f580a4a.c3e263e990bc8d6c3a2d636ac7a19c6c6);
			c07a248008e037e41899c8b004ace7a52 = c551fa69e7c0ec492ab91daa4199210a;
			c551fa69e7c0ec492ab91daa4199210a = c551fa69e7c0ec492ab91daa4199210a.c551fa69e7c0ec492ab91daa4199210a2;
			if (c551fa69e7c0ec492ab91daa4199210a == null)
			{
				break;
			}
		}
	}

	private c07a248008e037e41899c8b004ace7a50 c3d1fce75fd975cd49156dca71180a13f(c07a248008e037e41899c8b004ace7a50 ce9beaab8ed4de70dd0e96a36b4c42105)
	{
		if (c3cb2c0edc4d55dd1410e3483d1ef60d4 == ce9beaab8ed4de70dd0e96a36b4c42105)
		{
			c3cb2c0edc4d55dd1410e3483d1ef60d4 = null;
			return null;
		}
		c07a248008e037e41899c8b004ace7a50 c551fa69e7c0ec492ab91daa4199210a = ce9beaab8ed4de70dd0e96a36b4c42105.c551fa69e7c0ec492ab91daa4199210a2;
		c07a248008e037e41899c8b004ace7a50 c551fa69e7c0ec492ab91daa4199210a2 = c551fa69e7c0ec492ab91daa4199210a.c551fa69e7c0ec492ab91daa4199210a2;
		c07a248008e037e41899c8b004ace7a50 c07a248008e037e41899c8b004ace7a51;
		if (c551fa69e7c0ec492ab91daa4199210a.c6d3fb34101b9e1ce92055c2914d2d1de != ce9beaab8ed4de70dd0e96a36b4c42105)
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
				__ldtoken(DynamicAabbTree<>.c3d1fce75fd975cd49156dca71180a13f(c07a248008e037e41899c8b004ace7a50));
			}
			c07a248008e037e41899c8b004ace7a51 = c551fa69e7c0ec492ab91daa4199210a.c6d3fb34101b9e1ce92055c2914d2d1de;
		}
		else
		{
			c07a248008e037e41899c8b004ace7a51 = c551fa69e7c0ec492ab91daa4199210a.c2d9d869d0c8fd5fb55f452375f580a4a;
		}
		c07a248008e037e41899c8b004ace7a50 c07a248008e037e41899c8b004ace7a52 = c07a248008e037e41899c8b004ace7a51;
		if (c551fa69e7c0ec492ab91daa4199210a2 == null)
		{
			c3cb2c0edc4d55dd1410e3483d1ef60d4 = c07a248008e037e41899c8b004ace7a52;
			c07a248008e037e41899c8b004ace7a52.c551fa69e7c0ec492ab91daa4199210a2 = null;
			c53fef33d7344726da08aeb063ec62121.Recycle(c551fa69e7c0ec492ab91daa4199210a);
			return c3cb2c0edc4d55dd1410e3483d1ef60d4;
		}
		if (c551fa69e7c0ec492ab91daa4199210a2.c6d3fb34101b9e1ce92055c2914d2d1de == c551fa69e7c0ec492ab91daa4199210a)
		{
			c551fa69e7c0ec492ab91daa4199210a2.c6d3fb34101b9e1ce92055c2914d2d1de = c07a248008e037e41899c8b004ace7a52;
		}
		else
		{
			c551fa69e7c0ec492ab91daa4199210a2.c2d9d869d0c8fd5fb55f452375f580a4a = c07a248008e037e41899c8b004ace7a52;
		}
		c07a248008e037e41899c8b004ace7a52.c551fa69e7c0ec492ab91daa4199210a2 = c551fa69e7c0ec492ab91daa4199210a2;
		c53fef33d7344726da08aeb063ec62121.Recycle(c551fa69e7c0ec492ab91daa4199210a);
		while (true)
		{
			Aabb c3e263e990bc8d6c3a2d636ac7a19c6c = c551fa69e7c0ec492ab91daa4199210a2.c3e263e990bc8d6c3a2d636ac7a19c6c6;
			c551fa69e7c0ec492ab91daa4199210a2.c3e263e990bc8d6c3a2d636ac7a19c6c6 = Aabb.Merge(c551fa69e7c0ec492ab91daa4199210a2.c6d3fb34101b9e1ce92055c2914d2d1de.c3e263e990bc8d6c3a2d636ac7a19c6c6, c551fa69e7c0ec492ab91daa4199210a2.c2d9d869d0c8fd5fb55f452375f580a4a.c3e263e990bc8d6c3a2d636ac7a19c6c6);
			if (!(c3e263e990bc8d6c3a2d636ac7a19c6c != c551fa69e7c0ec492ab91daa4199210a2.c3e263e990bc8d6c3a2d636ac7a19c6c6))
			{
				break;
			}
			while (true)
			{
				switch (3)
				{
				case 0:
					break;
				default:
					goto end_IL_00cf;
				}
				continue;
				end_IL_00cf:
				break;
			}
			c551fa69e7c0ec492ab91daa4199210a2 = c551fa69e7c0ec492ab91daa4199210a2.c551fa69e7c0ec492ab91daa4199210a2;
			if (c551fa69e7c0ec492ab91daa4199210a2 != null)
			{
				continue;
			}
			while (true)
			{
				switch (3)
				{
				case 0:
					continue;
				}
				break;
			}
			break;
		}
		return c551fa69e7c0ec492ab91daa4199210a2 ?? c3cb2c0edc4d55dd1410e3483d1ef60d4;
	}

	private void cceb3668cd65480d681d53a1cfb0988dd(int cc94d1e0aefe900a66a2cef216f23ec9c)
	{
		if (c3cb2c0edc4d55dd1410e3483d1ef60d4 == null)
		{
			return;
		}
		if (cc94d1e0aefe900a66a2cef216f23ec9c < 0)
		{
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
				__ldtoken(DynamicAabbTree<>.cceb3668cd65480d681d53a1cfb0988dd(int));
			}
			cc94d1e0aefe900a66a2cef216f23ec9c = Count;
		}
		for (int i = 0; i < cc94d1e0aefe900a66a2cef216f23ec9c; i++)
		{
			c07a248008e037e41899c8b004ace7a50 c07a248008e037e41899c8b004ace7a51 = c3cb2c0edc4d55dd1410e3483d1ef60d4;
			int num = 0;
			while (!c07a248008e037e41899c8b004ace7a51.cfd73e16b19bb8bde65db991a787eb5ad)
			{
				c07a248008e037e41899c8b004ace7a51 = ((((ceeb04bdde059d62834089429803fd6fd >> num) & 1) != 0) ? c07a248008e037e41899c8b004ace7a51.c2d9d869d0c8fd5fb55f452375f580a4a : c07a248008e037e41899c8b004ace7a51.c6d3fb34101b9e1ce92055c2914d2d1de);
				num = (num + 1) & 0x1F;
			}
			c9e1b6071a804095ee7ff5523d5d5fb61(c07a248008e037e41899c8b004ace7a51);
			ceeb04bdde059d62834089429803fd6fd++;
		}
	}

	private void c9e1b6071a804095ee7ff5523d5d5fb61(c07a248008e037e41899c8b004ace7a50 c7f587bd57d47897172f47483e6bcca68)
	{
		c07a248008e037e41899c8b004ace7a50 cf3a7a02cd7f4211ef47c16b8b2f5064d = c3d1fce75fd975cd49156dca71180a13f(c7f587bd57d47897172f47483e6bcca68);
		c437cc650c1bbb3b5d6a346455cd9229c(cf3a7a02cd7f4211ef47c16b8b2f5064d, c7f587bd57d47897172f47483e6bcca68);
	}

	private bool c9e1b6071a804095ee7ff5523d5d5fb61(c07a248008e037e41899c8b004ace7a50 c7f587bd57d47897172f47483e6bcca68, Aabb c0e2590e59719a898b36fff5591124a7a)
	{
		if (c7f587bd57d47897172f47483e6bcca68.c3e263e990bc8d6c3a2d636ac7a19c6c6.Contains(c0e2590e59719a898b36fff5591124a7a))
		{
			return false;
		}
		if (cf815c97786756b49e4fe2c44c0d6e959)
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
			if (1 == 0)
			{
				__ldtoken(DynamicAabbTree<>.c9e1b6071a804095ee7ff5523d5d5fb61(c07a248008e037e41899c8b004ace7a50, Aabb));
			}
			if (GeometryHelper.HaveContact(c7f587bd57d47897172f47483e6bcca68.c3e263e990bc8d6c3a2d636ac7a19c6c6, c0e2590e59719a898b36fff5591124a7a))
			{
				while (true)
				{
					switch (2)
					{
					case 0:
						continue;
					}
					break;
				}
				Vector3F c75c28b357dedcad9a44abd406f2361f = (c0e2590e59719a898b36fff5591124a7a.Center - c7f587bd57d47897172f47483e6bcca68.c3e263e990bc8d6c3a2d636ac7a19c6c6.Center) * MotionPrediction;
				ced731256f14ddf9eb1984e958e49155a(ref c0e2590e59719a898b36fff5591124a7a, RelativeMargin);
				ced731256f14ddf9eb1984e958e49155a(ref c0e2590e59719a898b36fff5591124a7a, ref c75c28b357dedcad9a44abd406f2361f);
			}
		}
		c7f587bd57d47897172f47483e6bcca68.c3e263e990bc8d6c3a2d636ac7a19c6c6 = c0e2590e59719a898b36fff5591124a7a;
		c9e1b6071a804095ee7ff5523d5d5fb61(c7f587bd57d47897172f47483e6bcca68);
		return true;
	}

	private static void ced731256f14ddf9eb1984e958e49155a(ref Aabb cdc3439410d15f28489c964ee87b88fe9, ref Vector3F c75c28b357dedcad9a44abd406f2361f3)
	{
		if (c75c28b357dedcad9a44abd406f2361f3.X >= 0f)
		{
			cdc3439410d15f28489c964ee87b88fe9.Maximum.X += c75c28b357dedcad9a44abd406f2361f3.X;
		}
		else
		{
			cdc3439410d15f28489c964ee87b88fe9.Minimum.X += c75c28b357dedcad9a44abd406f2361f3.X;
		}
		if (c75c28b357dedcad9a44abd406f2361f3.Y >= 0f)
		{
			cdc3439410d15f28489c964ee87b88fe9.Maximum.Y += c75c28b357dedcad9a44abd406f2361f3.Y;
		}
		else
		{
			cdc3439410d15f28489c964ee87b88fe9.Minimum.Y += c75c28b357dedcad9a44abd406f2361f3.Y;
		}
		if (c75c28b357dedcad9a44abd406f2361f3.Z >= 0f)
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
						__ldtoken(DynamicAabbTree<>.ced731256f14ddf9eb1984e958e49155a(ref Aabb, ref Vector3F));
					}
					cdc3439410d15f28489c964ee87b88fe9.Maximum.Z += c75c28b357dedcad9a44abd406f2361f3.Z;
					return;
				}
			}
		}
		cdc3439410d15f28489c964ee87b88fe9.Minimum.Z += c75c28b357dedcad9a44abd406f2361f3.Z;
	}

	private static void ced731256f14ddf9eb1984e958e49155a(ref Aabb cdc3439410d15f28489c964ee87b88fe9, float cc0ff223def9e1ebe5622809ebbe46eaf)
	{
		Vector3F vector3F = new Vector3F(cdc3439410d15f28489c964ee87b88fe9.Extent.LargestComponent * cc0ff223def9e1ebe5622809ebbe46eaf);
		cdc3439410d15f28489c964ee87b88fe9.Minimum -= vector3F;
		cdc3439410d15f28489c964ee87b88fe9.Maximum += vector3F;
	}

	/// <summary>
	///       Gets overlaps between all items of this spatial partition and the items of another spatial
	///       partition.
	///       </summary>
	/// <param name="otherPartition">The spatial partition to test against.</param>
	/// <returns>
	///       All pairwise overlaps between items of this spatial partition and
	///       <paramref name="otherPartition" />. In each returned <see cref="T:DigitalRune.Collections.Pair`1" /> the first item 
	///       (see <see cref="P:DigitalRune.Collections.Pair`1.First" />) is from this partition and the second item (see
	///       <see cref="P:DigitalRune.Collections.Pair`1.Second" />) is from <paramref name="otherPartition" />.
	///       </returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="otherPartition" /> is <see langword="null" />.
	///       </exception>
	public override IEnumerable<Pair<T>> GetOverlaps(ISpatialPartition<T> otherPartition)
	{
		if (otherPartition == null)
		{
			while (true)
			{
				switch (4)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(DynamicAabbTree<>.GetOverlaps(ISpatialPartition<T>));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(994));
				}
			}
		}
		otherPartition.Update(forceRebuild: false);
		Update(forceRebuild: false);
		if (c3cb2c0edc4d55dd1410e3483d1ef60d4 == null)
		{
			return Enumerable.Empty<Pair<T>>();
		}
		if (!(otherPartition is DynamicAabbTree<T> dynamicAabbTree))
		{
			return c0f8cf26383f225c6acffebdc67fb4382(otherPartition);
		}
		if (dynamicAabbTree.c3cb2c0edc4d55dd1410e3483d1ef60d4 == null)
		{
			while (true)
			{
				switch (7)
				{
				case 0:
					break;
				default:
					return Enumerable.Empty<Pair<T>>();
				}
			}
		}
		return c0f8cf26383f225c6acffebdc67fb4382(dynamicAabbTree);
	}

	private IEnumerable<Pair<T>> c0f8cf26383f225c6acffebdc67fb4382(ISpatialPartition<T> cfd8883387afb9cc25a2ac9150de0f66b)
	{
		IEnumerator<c07a248008e037e41899c8b004ace7a50> enumerator = c4d76d858205a75f799980f42e41cbe5b(cfd8883387afb9cc25a2ac9150de0f66b.Aabb).GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				c07a248008e037e41899c8b004ace7a50 current = enumerator.Current;
				IEnumerable<T> overlaps = cfd8883387afb9cc25a2ac9150de0f66b.GetOverlaps(current.c3e263e990bc8d6c3a2d636ac7a19c6c6);
				foreach (T item in overlaps)
				{
					Pair<T> pair = new Pair<T>(second: item, first: current.c652f269a40a2519b8658ffbade581b4e);
					if (Filter == null || Filter.Filter(pair))
					{
						yield return pair;
					}
				}
			}
		}
		finally
		{
			if (enumerator != null)
			{
				while (true)
				{
					switch (2)
					{
					case 0:
						continue;
					}
					if (1 == 0)
					{
						__ldtoken(DynamicAabbTree<>.ce338abf3716189437408a0ba88dc0602.ca0a60671becd1e0cef8a51aa281f5da6());
					}
					enumerator.Dispose();
					break;
				}
			}
		}
	}

	private IEnumerable<Pair<T>> c0f8cf26383f225c6acffebdc67fb4382(DynamicAabbTree<T> ca9802b6266a4b983f751fb2d730561ca)
	{
		Stack<Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50>> stack = ResourcePools<Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50>>.Stacks.Obtain();
		stack.Push(new Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50>(c3cb2c0edc4d55dd1410e3483d1ef60d4, ca9802b6266a4b983f751fb2d730561ca.c3cb2c0edc4d55dd1410e3483d1ef60d4));
		while (stack.Count > 0)
		{
			Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50> pair = stack.Pop();
			c07a248008e037e41899c8b004ace7a50 first = pair.First;
			c07a248008e037e41899c8b004ace7a50 second = pair.Second;
			if (first == second)
			{
				if (!first.cfd73e16b19bb8bde65db991a787eb5ad)
				{
					stack.Push(new Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50>(first.c2d9d869d0c8fd5fb55f452375f580a4a, first.c2d9d869d0c8fd5fb55f452375f580a4a));
					stack.Push(new Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50>(first.c6d3fb34101b9e1ce92055c2914d2d1de, first.c2d9d869d0c8fd5fb55f452375f580a4a));
					stack.Push(new Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50>(first.c6d3fb34101b9e1ce92055c2914d2d1de, first.c6d3fb34101b9e1ce92055c2914d2d1de));
				}
			}
			else
			{
				if (!GeometryHelper.HaveContact(first.c3e263e990bc8d6c3a2d636ac7a19c6c6, second.c3e263e990bc8d6c3a2d636ac7a19c6c6))
				{
					continue;
				}
				if (!first.cfd73e16b19bb8bde65db991a787eb5ad)
				{
					if (!second.cfd73e16b19bb8bde65db991a787eb5ad)
					{
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
							__ldtoken(DynamicAabbTree<>.c4d1a8923eecf70cd450199c7f8b8684a.MoveNext());
						}
						stack.Push(new Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50>(first.c2d9d869d0c8fd5fb55f452375f580a4a, second.c2d9d869d0c8fd5fb55f452375f580a4a));
						stack.Push(new Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50>(first.c6d3fb34101b9e1ce92055c2914d2d1de, second.c2d9d869d0c8fd5fb55f452375f580a4a));
						stack.Push(new Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50>(first.c2d9d869d0c8fd5fb55f452375f580a4a, second.c6d3fb34101b9e1ce92055c2914d2d1de));
						stack.Push(new Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50>(first.c6d3fb34101b9e1ce92055c2914d2d1de, second.c6d3fb34101b9e1ce92055c2914d2d1de));
					}
					else
					{
						stack.Push(new Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50>(first.c2d9d869d0c8fd5fb55f452375f580a4a, second));
						stack.Push(new Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50>(first.c6d3fb34101b9e1ce92055c2914d2d1de, second));
					}
				}
				else if (!second.cfd73e16b19bb8bde65db991a787eb5ad)
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
					stack.Push(new Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50>(first, second.c2d9d869d0c8fd5fb55f452375f580a4a));
					stack.Push(new Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50>(first, second.c6d3fb34101b9e1ce92055c2914d2d1de));
				}
				else
				{
					Pair<T> pair2 = new Pair<T>(first.c652f269a40a2519b8658ffbade581b4e, second.c652f269a40a2519b8658ffbade581b4e);
					if (Filter == null || Filter.Filter(pair2))
					{
						yield return pair2;
					}
				}
			}
		}
		while (true)
		{
			switch (6)
			{
			case 0:
				continue;
			}
			ResourcePools<Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50>>.Stacks.Recycle(stack);
			yield break;
		}
	}

	/// <summary>
	///       Gets overlaps between all items of this spatial partition and the items of another spatial 
	///       partition.
	///       </summary>
	/// <param name="scale">The scale of this spatial partition.</param>
	/// <param name="pose">The pose of this spatial partition.</param>
	/// <param name="otherPartition">The other spatial partition to test against.</param>
	/// <param name="otherScale">The scale of the <paramref name="otherPartition" />.</param>
	/// <param name="otherPose">The pose of the <paramref name="otherPartition" />.</param>
	/// <returns>
	///       All pairwise overlaps between items of this spatial partition and 
	///       <paramref name="otherPartition" />. In each returned <see cref="T:DigitalRune.Collections.Pair`1" /> the first item
	///       (see <see cref="P:DigitalRune.Collections.Pair`1.First" />) is from this partition and the second item (see 
	///       <see cref="P:DigitalRune.Collections.Pair`1.Second" />) is from <paramref name="otherPartition" />.
	///       </returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="otherPartition" /> is <see langword="null" />.
	///       </exception>
	public override IEnumerable<Pair<T>> GetOverlaps(Vector3F scale, Pose pose, ISpatialPartition<T> otherPartition, Vector3F otherScale, Pose otherPose)
	{
		if (otherPartition == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(994));
		}
		otherPartition.Update(forceRebuild: false);
		Update(forceRebuild: false);
		if (c3cb2c0edc4d55dd1410e3483d1ef60d4 == null)
		{
			return Enumerable.Empty<Pair<T>>();
		}
		if (!(otherPartition is DynamicAabbTree<T> dynamicAabbTree))
		{
			return c0f8cf26383f225c6acffebdc67fb4382(scale, otherPartition, otherScale, pose.Inverse * otherPose);
		}
		if (dynamicAabbTree.c3cb2c0edc4d55dd1410e3483d1ef60d4 == null)
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
						__ldtoken(DynamicAabbTree<>.GetOverlaps(Vector3F, Pose, ISpatialPartition<T>, Vector3F, Pose));
					}
					return Enumerable.Empty<Pair<T>>();
				}
			}
		}
		return c0f8cf26383f225c6acffebdc67fb4382(scale, dynamicAabbTree, otherScale, pose.Inverse * otherPose);
	}

	private IEnumerable<Pair<T>> c0f8cf26383f225c6acffebdc67fb4382(Vector3F ceabb47d7d959545d89c4300eabbdd7c2, ISpatialPartition<T> cfd8883387afb9cc25a2ac9150de0f66b, Vector3F c1b4918d9d3cc94171c67339d1fc78b0f, Pose cd2abebf7ff6ec7d035c28a04faaa459b)
	{
		Vector3F scale = Vector3F.One / ceabb47d7d959545d89c4300eabbdd7c2;
		Vector3F scale2 = Vector3F.One / c1b4918d9d3cc94171c67339d1fc78b0f;
		Pose pose = cd2abebf7ff6ec7d035c28a04faaa459b;
		Pose inverse = cd2abebf7ff6ec7d035c28a04faaa459b.Inverse;
		Aabb aabb = cfd8883387afb9cc25a2ac9150de0f66b.Aabb.GetAabb(c1b4918d9d3cc94171c67339d1fc78b0f, pose);
		aabb.Scale(scale);
		IEnumerator<c07a248008e037e41899c8b004ace7a50> enumerator = c4d76d858205a75f799980f42e41cbe5b(aabb).GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				c07a248008e037e41899c8b004ace7a50 current = enumerator.Current;
				Aabb aabb2 = current.c3e263e990bc8d6c3a2d636ac7a19c6c6.GetAabb(ceabb47d7d959545d89c4300eabbdd7c2, inverse);
				aabb2.Scale(scale2);
				using IEnumerator<T> enumerator2 = cfd8883387afb9cc25a2ac9150de0f66b.GetOverlaps(aabb2).GetEnumerator();
				while (enumerator2.MoveNext())
				{
					Pair<T> pair = new Pair<T>(second: enumerator2.Current, first: current.c652f269a40a2519b8658ffbade581b4e);
					if (Filter != null)
					{
						while (true)
						{
							switch (2)
							{
							case 0:
								continue;
							}
							break;
						}
						if (!Filter.Filter(pair))
						{
							continue;
						}
					}
					yield return pair;
					while (true)
					{
						switch (1)
						{
						case 0:
							continue;
						}
						break;
					}
					if (1 == 0)
					{
						__ldtoken(DynamicAabbTree<>.caca1bf20c97f469f311050b95e74ccd9.MoveNext());
					}
					int num;
					if (num != 3)
					{
						yield break;
					}
				}
				while (true)
				{
					switch (6)
					{
					case 0:
						continue;
					}
					break;
				}
			}
		}
		finally
		{
			if (enumerator != null)
			{
				while (true)
				{
					switch (3)
					{
					case 0:
						continue;
					}
					if (1 == 0)
					{
						__ldtoken(DynamicAabbTree<>.caca1bf20c97f469f311050b95e74ccd9.c2290ec7e707de69340902b58848608a4());
					}
					enumerator.Dispose();
					break;
				}
			}
		}
	}

	private IEnumerable<Pair<T>> c0f8cf26383f225c6acffebdc67fb4382(Vector3F ceabb47d7d959545d89c4300eabbdd7c2, DynamicAabbTree<T> ca9802b6266a4b983f751fb2d730561ca, Vector3F c1b4918d9d3cc94171c67339d1fc78b0f, Pose cd2abebf7ff6ec7d035c28a04faaa459b)
	{
		Stack<Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50>> stack = ResourcePools<Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50>>.Stacks.Obtain();
		stack.Push(new Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50>(c3cb2c0edc4d55dd1410e3483d1ef60d4, ca9802b6266a4b983f751fb2d730561ca.c3cb2c0edc4d55dd1410e3483d1ef60d4));
		while (stack.Count > 0)
		{
			Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50> pair = stack.Pop();
			c07a248008e037e41899c8b004ace7a50 first = pair.First;
			c07a248008e037e41899c8b004ace7a50 second = pair.Second;
			if (!cd0cfe897426aab73c90b8217df390607(first, ceabb47d7d959545d89c4300eabbdd7c2, second, c1b4918d9d3cc94171c67339d1fc78b0f, cd2abebf7ff6ec7d035c28a04faaa459b))
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
			if (1 == 0)
			{
				__ldtoken(DynamicAabbTree<>.c895d3751afb2627cd9239876fd166931.MoveNext());
			}
			if (!first.cfd73e16b19bb8bde65db991a787eb5ad)
			{
				if (!second.cfd73e16b19bb8bde65db991a787eb5ad)
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
					stack.Push(new Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50>(first.c2d9d869d0c8fd5fb55f452375f580a4a, second.c2d9d869d0c8fd5fb55f452375f580a4a));
					stack.Push(new Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50>(first.c6d3fb34101b9e1ce92055c2914d2d1de, second.c2d9d869d0c8fd5fb55f452375f580a4a));
					stack.Push(new Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50>(first.c2d9d869d0c8fd5fb55f452375f580a4a, second.c6d3fb34101b9e1ce92055c2914d2d1de));
					stack.Push(new Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50>(first.c6d3fb34101b9e1ce92055c2914d2d1de, second.c6d3fb34101b9e1ce92055c2914d2d1de));
				}
				else
				{
					stack.Push(new Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50>(first.c2d9d869d0c8fd5fb55f452375f580a4a, second));
					stack.Push(new Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50>(first.c6d3fb34101b9e1ce92055c2914d2d1de, second));
				}
			}
			else if (!second.cfd73e16b19bb8bde65db991a787eb5ad)
			{
				stack.Push(new Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50>(first, second.c2d9d869d0c8fd5fb55f452375f580a4a));
				stack.Push(new Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50>(first, second.c6d3fb34101b9e1ce92055c2914d2d1de));
			}
			else
			{
				Pair<T> pair2 = new Pair<T>(first.c652f269a40a2519b8658ffbade581b4e, second.c652f269a40a2519b8658ffbade581b4e);
				if (Filter == null || Filter.Filter(pair2))
				{
					yield return pair2;
				}
			}
		}
		while (true)
		{
			switch (4)
			{
			case 0:
				continue;
			}
			ResourcePools<Pair<c07a248008e037e41899c8b004ace7a50, c07a248008e037e41899c8b004ace7a50>>.Stacks.Recycle(stack);
			yield break;
		}
	}

	private static bool cc2503a1629586ff93a4137525e360640(c07a248008e037e41899c8b004ace7a50 c1f49a553317458268e1fa11d51bf4670, Vector3F c3bc7e17fda039e69573a5295fced7439, c07a248008e037e41899c8b004ace7a50 c29ba3cbb559c64c577242fda562d3024, Vector3F c547f06073c85c380fd3dccf550a6d536)
	{
		Vector3F vector3F = c1f49a553317458268e1fa11d51bf4670.c3e263e990bc8d6c3a2d636ac7a19c6c6.Extent * Vector3F.Absolute(c3bc7e17fda039e69573a5295fced7439);
		Vector3F vector3F2 = c29ba3cbb559c64c577242fda562d3024.c3e263e990bc8d6c3a2d636ac7a19c6c6.Extent * Vector3F.Absolute(c547f06073c85c380fd3dccf550a6d536);
		return vector3F.LargestComponent > vector3F2.LargestComponent;
	}

	private static bool cd0cfe897426aab73c90b8217df390607(c07a248008e037e41899c8b004ace7a50 c1f49a553317458268e1fa11d51bf4670, Vector3F c3bc7e17fda039e69573a5295fced7439, c07a248008e037e41899c8b004ace7a50 c29ba3cbb559c64c577242fda562d3024, Vector3F c547f06073c85c380fd3dccf550a6d536, Pose c689a79a5792495f42cbe3effa012780c)
	{
		Aabb c3e263e990bc8d6c3a2d636ac7a19c6c = c1f49a553317458268e1fa11d51bf4670.c3e263e990bc8d6c3a2d636ac7a19c6c6;
		c3e263e990bc8d6c3a2d636ac7a19c6c.Scale(c3bc7e17fda039e69573a5295fced7439);
		Aabb c3e263e990bc8d6c3a2d636ac7a19c6c2 = c29ba3cbb559c64c577242fda562d3024.c3e263e990bc8d6c3a2d636ac7a19c6c6;
		c3e263e990bc8d6c3a2d636ac7a19c6c2.Scale(c547f06073c85c380fd3dccf550a6d536);
		Vector3F extent = c3e263e990bc8d6c3a2d636ac7a19c6c2.Extent;
		Pose boxPose = c689a79a5792495f42cbe3effa012780c * new Pose(c3e263e990bc8d6c3a2d636ac7a19c6c2.Center);
		return GeometryHelper.HaveContact(c3e263e990bc8d6c3a2d636ac7a19c6c, extent, boxPose, makeEdgeTests: false);
	}

	/// <summary>
	///       Gets all items that are candidates for the smallest closest-point distance to items in a
	///       given partition.
	///       </summary>
	/// <param name="scale">The scale of this spatial partition.</param>
	/// <param name="pose">The pose of this spatial partition.</param>
	/// <param name="otherPartition">The other spatial partition to test against.</param>
	/// <param name="otherScale">The scale of the <paramref name="otherPartition" />.</param>
	/// <param name="otherPose">
	///       The pose of the <paramref name="otherPartition" /> relative to this spatial partition.
	///       </param>
	/// <param name="callback">
	///       The callback that is called with each found candidate item. The method must compute the
	///       closest-point on the candidate item and return the squared closest-point distance.
	///       </param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="otherPartition" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="callback" /> is <see langword="null" />.
	///       </exception>
	public void GetClosestPointCandidates(Vector3F scale, Pose pose, ISpatialPartition<T> otherPartition, Vector3F otherScale, Pose otherPose, Func<T, T, float> callback)
	{
		if (otherPartition == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(994));
		}
		if (callback == null)
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
						__ldtoken(DynamicAabbTree<>.GetClosestPointCandidates(Vector3F, Pose, ISpatialPartition<T>, Vector3F, Pose, Func<T, T, float>));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1023));
				}
			}
		}
		otherPartition.Update(forceRebuild: false);
		Update(forceRebuild: false);
		if (c3cb2c0edc4d55dd1410e3483d1ef60d4 == null)
		{
			while (true)
			{
				switch (6)
				{
				case 0:
					break;
				default:
					return;
				}
			}
		}
		if (otherPartition is DynamicAabbTree<T>)
		{
			while (true)
			{
				switch (3)
				{
				case 0:
					break;
				default:
				{
					DynamicAabbTree<T> dynamicAabbTree = (DynamicAabbTree<T>)otherPartition;
					float c2878235e00baff8166ebc90ef27400ae = float.PositiveInfinity;
					cb780b4b51062221f27d196ea0a7abde7(c3cb2c0edc4d55dd1410e3483d1ef60d4, scale, dynamicAabbTree.c3cb2c0edc4d55dd1410e3483d1ef60d4, otherScale, pose.Inverse * otherPose, callback, ref c2878235e00baff8166ebc90ef27400ae);
					return;
				}
				}
			}
		}
		if (otherPartition is ISupportClosestPointQueries<T>)
		{
			cb780b4b51062221f27d196ea0a7abde7(scale, pose, (ISupportClosestPointQueries<T>)otherPartition, otherScale, otherPose, callback);
		}
		else
		{
			cb780b4b51062221f27d196ea0a7abde7(otherPartition, callback);
		}
	}

	private void cb780b4b51062221f27d196ea0a7abde7(c07a248008e037e41899c8b004ace7a50 c1f49a553317458268e1fa11d51bf4670, Vector3F c3bc7e17fda039e69573a5295fced7439, c07a248008e037e41899c8b004ace7a50 c29ba3cbb559c64c577242fda562d3024, Vector3F c547f06073c85c380fd3dccf550a6d536, Pose c689a79a5792495f42cbe3effa012780c, Func<T, T, float> ce4b7628f9880cef87f99eae50a8132fe, ref float c2878235e00baff8166ebc90ef27400ae)
	{
		if (c1f49a553317458268e1fa11d51bf4670 == null)
		{
			return;
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
				__ldtoken(DynamicAabbTree<>.cb780b4b51062221f27d196ea0a7abde7(c07a248008e037e41899c8b004ace7a50, Vector3F, c07a248008e037e41899c8b004ace7a50, Vector3F, Pose, Func<T, T, float>, ref float));
			}
			if (c29ba3cbb559c64c577242fda562d3024 == null)
			{
				return;
			}
			while (true)
			{
				switch (3)
				{
				case 0:
					continue;
				}
				if (c2878235e00baff8166ebc90ef27400ae < 0f || (c2878235e00baff8166ebc90ef27400ae == 0f && !cd0cfe897426aab73c90b8217df390607(c1f49a553317458268e1fa11d51bf4670, c3bc7e17fda039e69573a5295fced7439, c29ba3cbb559c64c577242fda562d3024, c547f06073c85c380fd3dccf550a6d536, c689a79a5792495f42cbe3effa012780c)))
				{
					return;
				}
				if (c1f49a553317458268e1fa11d51bf4670.cfd73e16b19bb8bde65db991a787eb5ad && c29ba3cbb559c64c577242fda562d3024.cfd73e16b19bb8bde65db991a787eb5ad)
				{
					if (Filter != null)
					{
						if (!Filter.Filter(new Pair<T>(c1f49a553317458268e1fa11d51bf4670.c652f269a40a2519b8658ffbade581b4e, c29ba3cbb559c64c577242fda562d3024.c652f269a40a2519b8658ffbade581b4e)))
						{
							return;
						}
						while (true)
						{
							switch (4)
							{
							case 0:
								continue;
							}
							break;
						}
					}
					float val = ce4b7628f9880cef87f99eae50a8132fe(c1f49a553317458268e1fa11d51bf4670.c652f269a40a2519b8658ffbade581b4e, c29ba3cbb559c64c577242fda562d3024.c652f269a40a2519b8658ffbade581b4e);
					c2878235e00baff8166ebc90ef27400ae = Math.Min(val, c2878235e00baff8166ebc90ef27400ae);
					return;
				}
				if (!c29ba3cbb559c64c577242fda562d3024.cfd73e16b19bb8bde65db991a787eb5ad)
				{
					while (true)
					{
						switch (2)
						{
						case 0:
							continue;
						}
						break;
					}
					if (c1f49a553317458268e1fa11d51bf4670.cfd73e16b19bb8bde65db991a787eb5ad || !cc2503a1629586ff93a4137525e360640(c1f49a553317458268e1fa11d51bf4670, c3bc7e17fda039e69573a5295fced7439, c29ba3cbb559c64c577242fda562d3024, c547f06073c85c380fd3dccf550a6d536))
					{
						c07a248008e037e41899c8b004ace7a50 c6d3fb34101b9e1ce92055c2914d2d1de = c29ba3cbb559c64c577242fda562d3024.c6d3fb34101b9e1ce92055c2914d2d1de;
						c07a248008e037e41899c8b004ace7a50 c2d9d869d0c8fd5fb55f452375f580a4a = c29ba3cbb559c64c577242fda562d3024.c2d9d869d0c8fd5fb55f452375f580a4a;
						if (c2878235e00baff8166ebc90ef27400ae == 0f)
						{
							while (true)
							{
								switch (6)
								{
								case 0:
									break;
								default:
									cb780b4b51062221f27d196ea0a7abde7(c1f49a553317458268e1fa11d51bf4670, c3bc7e17fda039e69573a5295fced7439, c6d3fb34101b9e1ce92055c2914d2d1de, c547f06073c85c380fd3dccf550a6d536, c689a79a5792495f42cbe3effa012780c, ce4b7628f9880cef87f99eae50a8132fe, ref c2878235e00baff8166ebc90ef27400ae);
									cb780b4b51062221f27d196ea0a7abde7(c1f49a553317458268e1fa11d51bf4670, c3bc7e17fda039e69573a5295fced7439, c2d9d869d0c8fd5fb55f452375f580a4a, c547f06073c85c380fd3dccf550a6d536, c689a79a5792495f42cbe3effa012780c, ce4b7628f9880cef87f99eae50a8132fe, ref c2878235e00baff8166ebc90ef27400ae);
									return;
								}
							}
						}
						Aabb c3e263e990bc8d6c3a2d636ac7a19c6c = c1f49a553317458268e1fa11d51bf4670.c3e263e990bc8d6c3a2d636ac7a19c6c6;
						c3e263e990bc8d6c3a2d636ac7a19c6c.Scale(c3bc7e17fda039e69573a5295fced7439);
						Vector3F extent = c3e263e990bc8d6c3a2d636ac7a19c6c.Extent;
						Pose c689a79a5792495f42cbe3effa012780c2 = new Pose(c3e263e990bc8d6c3a2d636ac7a19c6c.Center);
						Aabb c3e263e990bc8d6c3a2d636ac7a19c6c2 = c6d3fb34101b9e1ce92055c2914d2d1de.c3e263e990bc8d6c3a2d636ac7a19c6c6;
						c3e263e990bc8d6c3a2d636ac7a19c6c2.Scale(c547f06073c85c380fd3dccf550a6d536);
						Vector3F extent2 = c3e263e990bc8d6c3a2d636ac7a19c6c2.Extent;
						Pose c2cca220b1cf789fcaae8dd1859badc = c689a79a5792495f42cbe3effa012780c * new Pose(c3e263e990bc8d6c3a2d636ac7a19c6c2.Center);
						float num = GeometryHelper.c863d4de1e2a6bbf028c3b0f9ed12c93f(extent2, c2cca220b1cf789fcaae8dd1859badc, extent, c689a79a5792495f42cbe3effa012780c2);
						Aabb c3e263e990bc8d6c3a2d636ac7a19c6c3 = c2d9d869d0c8fd5fb55f452375f580a4a.c3e263e990bc8d6c3a2d636ac7a19c6c6;
						c3e263e990bc8d6c3a2d636ac7a19c6c3.Scale(c547f06073c85c380fd3dccf550a6d536);
						extent2 = c3e263e990bc8d6c3a2d636ac7a19c6c3.Extent;
						Pose c2cca220b1cf789fcaae8dd1859badc2 = c689a79a5792495f42cbe3effa012780c * new Pose(c3e263e990bc8d6c3a2d636ac7a19c6c3.Center);
						float num2 = GeometryHelper.c863d4de1e2a6bbf028c3b0f9ed12c93f(extent2, c2cca220b1cf789fcaae8dd1859badc2, extent, c689a79a5792495f42cbe3effa012780c2);
						if (num < num2)
						{
							while (true)
							{
								switch (2)
								{
								case 0:
									break;
								default:
									if (num > c2878235e00baff8166ebc90ef27400ae)
									{
										while (true)
										{
											switch (5)
											{
											case 0:
												break;
											default:
												return;
											}
										}
									}
									cb780b4b51062221f27d196ea0a7abde7(c1f49a553317458268e1fa11d51bf4670, c3bc7e17fda039e69573a5295fced7439, c6d3fb34101b9e1ce92055c2914d2d1de, c547f06073c85c380fd3dccf550a6d536, c689a79a5792495f42cbe3effa012780c, ce4b7628f9880cef87f99eae50a8132fe, ref c2878235e00baff8166ebc90ef27400ae);
									if (!(num2 > c2878235e00baff8166ebc90ef27400ae))
									{
										cb780b4b51062221f27d196ea0a7abde7(c1f49a553317458268e1fa11d51bf4670, c3bc7e17fda039e69573a5295fced7439, c2d9d869d0c8fd5fb55f452375f580a4a, c547f06073c85c380fd3dccf550a6d536, c689a79a5792495f42cbe3effa012780c, ce4b7628f9880cef87f99eae50a8132fe, ref c2878235e00baff8166ebc90ef27400ae);
									}
									return;
								}
							}
						}
						if (num2 > c2878235e00baff8166ebc90ef27400ae)
						{
							return;
						}
						cb780b4b51062221f27d196ea0a7abde7(c1f49a553317458268e1fa11d51bf4670, c3bc7e17fda039e69573a5295fced7439, c2d9d869d0c8fd5fb55f452375f580a4a, c547f06073c85c380fd3dccf550a6d536, c689a79a5792495f42cbe3effa012780c, ce4b7628f9880cef87f99eae50a8132fe, ref c2878235e00baff8166ebc90ef27400ae);
						if (num > c2878235e00baff8166ebc90ef27400ae)
						{
							while (true)
							{
								switch (6)
								{
								case 0:
									break;
								default:
									return;
								}
							}
						}
						cb780b4b51062221f27d196ea0a7abde7(c1f49a553317458268e1fa11d51bf4670, c3bc7e17fda039e69573a5295fced7439, c6d3fb34101b9e1ce92055c2914d2d1de, c547f06073c85c380fd3dccf550a6d536, c689a79a5792495f42cbe3effa012780c, ce4b7628f9880cef87f99eae50a8132fe, ref c2878235e00baff8166ebc90ef27400ae);
						return;
					}
				}
				c07a248008e037e41899c8b004ace7a50 c6d3fb34101b9e1ce92055c2914d2d1de2 = c1f49a553317458268e1fa11d51bf4670.c6d3fb34101b9e1ce92055c2914d2d1de;
				c07a248008e037e41899c8b004ace7a50 c2d9d869d0c8fd5fb55f452375f580a4a2 = c1f49a553317458268e1fa11d51bf4670.c2d9d869d0c8fd5fb55f452375f580a4a;
				if (c2878235e00baff8166ebc90ef27400ae == 0f)
				{
					while (true)
					{
						switch (3)
						{
						case 0:
							break;
						default:
							cb780b4b51062221f27d196ea0a7abde7(c6d3fb34101b9e1ce92055c2914d2d1de2, c3bc7e17fda039e69573a5295fced7439, c29ba3cbb559c64c577242fda562d3024, c547f06073c85c380fd3dccf550a6d536, c689a79a5792495f42cbe3effa012780c, ce4b7628f9880cef87f99eae50a8132fe, ref c2878235e00baff8166ebc90ef27400ae);
							cb780b4b51062221f27d196ea0a7abde7(c2d9d869d0c8fd5fb55f452375f580a4a2, c3bc7e17fda039e69573a5295fced7439, c29ba3cbb559c64c577242fda562d3024, c547f06073c85c380fd3dccf550a6d536, c689a79a5792495f42cbe3effa012780c, ce4b7628f9880cef87f99eae50a8132fe, ref c2878235e00baff8166ebc90ef27400ae);
							return;
						}
					}
				}
				Aabb c3e263e990bc8d6c3a2d636ac7a19c6c4 = c29ba3cbb559c64c577242fda562d3024.c3e263e990bc8d6c3a2d636ac7a19c6c6;
				c3e263e990bc8d6c3a2d636ac7a19c6c4.Scale(c547f06073c85c380fd3dccf550a6d536);
				Vector3F extent3 = c3e263e990bc8d6c3a2d636ac7a19c6c4.Extent;
				Pose c689a79a5792495f42cbe3effa012780c3 = c689a79a5792495f42cbe3effa012780c * new Pose(c3e263e990bc8d6c3a2d636ac7a19c6c4.Center);
				Aabb c3e263e990bc8d6c3a2d636ac7a19c6c5 = c6d3fb34101b9e1ce92055c2914d2d1de2.c3e263e990bc8d6c3a2d636ac7a19c6c6;
				c3e263e990bc8d6c3a2d636ac7a19c6c5.Scale(c3bc7e17fda039e69573a5295fced7439);
				Vector3F extent4 = c3e263e990bc8d6c3a2d636ac7a19c6c5.Extent;
				Pose c2cca220b1cf789fcaae8dd1859badc3 = new Pose(c3e263e990bc8d6c3a2d636ac7a19c6c5.Center);
				float num3 = GeometryHelper.c863d4de1e2a6bbf028c3b0f9ed12c93f(extent4, c2cca220b1cf789fcaae8dd1859badc3, extent3, c689a79a5792495f42cbe3effa012780c3);
				Aabb c3e263e990bc8d6c3a2d636ac7a19c6c6 = c2d9d869d0c8fd5fb55f452375f580a4a2.c3e263e990bc8d6c3a2d636ac7a19c6c6;
				c3e263e990bc8d6c3a2d636ac7a19c6c6.Scale(c3bc7e17fda039e69573a5295fced7439);
				Vector3F extent5 = c3e263e990bc8d6c3a2d636ac7a19c6c6.Extent;
				Pose c2cca220b1cf789fcaae8dd1859badc4 = new Pose(c3e263e990bc8d6c3a2d636ac7a19c6c6.Center);
				float num4 = GeometryHelper.c863d4de1e2a6bbf028c3b0f9ed12c93f(extent5, c2cca220b1cf789fcaae8dd1859badc4, extent3, c689a79a5792495f42cbe3effa012780c3);
				if (num3 < num4)
				{
					while (true)
					{
						switch (4)
						{
						case 0:
							break;
						default:
							if (num3 > c2878235e00baff8166ebc90ef27400ae)
							{
								while (true)
								{
									switch (6)
									{
									case 0:
										break;
									default:
										return;
									}
								}
							}
							cb780b4b51062221f27d196ea0a7abde7(c6d3fb34101b9e1ce92055c2914d2d1de2, c3bc7e17fda039e69573a5295fced7439, c29ba3cbb559c64c577242fda562d3024, c547f06073c85c380fd3dccf550a6d536, c689a79a5792495f42cbe3effa012780c, ce4b7628f9880cef87f99eae50a8132fe, ref c2878235e00baff8166ebc90ef27400ae);
							if (num4 > c2878235e00baff8166ebc90ef27400ae)
							{
								while (true)
								{
									switch (5)
									{
									case 0:
										break;
									default:
										return;
									}
								}
							}
							cb780b4b51062221f27d196ea0a7abde7(c2d9d869d0c8fd5fb55f452375f580a4a2, c3bc7e17fda039e69573a5295fced7439, c29ba3cbb559c64c577242fda562d3024, c547f06073c85c380fd3dccf550a6d536, c689a79a5792495f42cbe3effa012780c, ce4b7628f9880cef87f99eae50a8132fe, ref c2878235e00baff8166ebc90ef27400ae);
							return;
						}
					}
				}
				if (num4 > c2878235e00baff8166ebc90ef27400ae)
				{
					while (true)
					{
						switch (5)
						{
						case 0:
							break;
						default:
							return;
						}
					}
				}
				cb780b4b51062221f27d196ea0a7abde7(c2d9d869d0c8fd5fb55f452375f580a4a2, c3bc7e17fda039e69573a5295fced7439, c29ba3cbb559c64c577242fda562d3024, c547f06073c85c380fd3dccf550a6d536, c689a79a5792495f42cbe3effa012780c, ce4b7628f9880cef87f99eae50a8132fe, ref c2878235e00baff8166ebc90ef27400ae);
				if (!(num3 > c2878235e00baff8166ebc90ef27400ae))
				{
					cb780b4b51062221f27d196ea0a7abde7(c6d3fb34101b9e1ce92055c2914d2d1de2, c3bc7e17fda039e69573a5295fced7439, c29ba3cbb559c64c577242fda562d3024, c547f06073c85c380fd3dccf550a6d536, c689a79a5792495f42cbe3effa012780c, ce4b7628f9880cef87f99eae50a8132fe, ref c2878235e00baff8166ebc90ef27400ae);
				}
				return;
			}
		}
	}

	private void cb780b4b51062221f27d196ea0a7abde7(Vector3F ceabb47d7d959545d89c4300eabbdd7c2, Pose cbc1b7e0b011ad6dcdb8beb392ec7a646, ISupportClosestPointQueries<T> cfd8883387afb9cc25a2ac9150de0f66b, Vector3F c1b4918d9d3cc94171c67339d1fc78b0f, Pose cd2abebf7ff6ec7d035c28a04faaa459b, Func<T, T, float> ce4b7628f9880cef87f99eae50a8132fe)
	{
		cc308d98a84a8563af043b321600339a2<T> cc308d98a84a8563af043b321600339a3 = cc308d98a84a8563af043b321600339a2<T>.c2d8ce8b1ccf2b905e3b18f8a5145fe8a();
		cc308d98a84a8563af043b321600339a3.OriginalCallback = ce4b7628f9880cef87f99eae50a8132fe;
		Pose pose = cd2abebf7ff6ec7d035c28a04faaa459b.Inverse * cbc1b7e0b011ad6dcdb8beb392ec7a646;
		Vector3F scale = Vector3F.One / c1b4918d9d3cc94171c67339d1fc78b0f;
		float num = float.PositiveInfinity;
		for (int i = 0; i < cd311d3823f638b023eacb767b312cc3e.Count; i++)
		{
			if (!(num >= 0f))
			{
				break;
			}
			c07a248008e037e41899c8b004ace7a50 c07a248008e037e41899c8b004ace7a51 = cd311d3823f638b023eacb767b312cc3e[i];
			cc308d98a84a8563af043b321600339a3.Item = c07a248008e037e41899c8b004ace7a51.c652f269a40a2519b8658ffbade581b4e;
			Aabb aabb = c07a248008e037e41899c8b004ace7a51.c3e263e990bc8d6c3a2d636ac7a19c6c6.GetAabb(ceabb47d7d959545d89c4300eabbdd7c2, pose);
			aabb.Scale(scale);
			num = cfd8883387afb9cc25a2ac9150de0f66b.GetClosestPointCandidates(aabb, num, cc308d98a84a8563af043b321600339a3.c6516654248581f959063ea51406f708b);
		}
		cc308d98a84a8563af043b321600339a3.ca18b9b5faa9851d6355f617787bb5859();
	}

	private void cb780b4b51062221f27d196ea0a7abde7(ISpatialPartition<T> cfd8883387afb9cc25a2ac9150de0f66b, Func<T, T, float> ce4b7628f9880cef87f99eae50a8132fe)
	{
		foreach (T item in c0e601bcddbab14811b8bae6ae4b7b274)
		{
			IEnumerator<T> enumerator2 = cfd8883387afb9cc25a2ac9150de0f66b.GetEnumerator();
			try
			{
				while (enumerator2.MoveNext())
				{
					T current2 = enumerator2.Current;
					float num = ce4b7628f9880cef87f99eae50a8132fe(item, current2);
					if (!(num < 0f))
					{
						continue;
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
							__ldtoken(DynamicAabbTree<>.cb780b4b51062221f27d196ea0a7abde7(ISpatialPartition<T>, Func<T, T, float>));
						}
						return;
					}
				}
				while (true)
				{
					switch (2)
					{
					case 0:
						break;
					default:
						goto end_IL_0062;
					}
					continue;
					end_IL_0062:
					break;
				}
			}
			finally
			{
				if (enumerator2 != null)
				{
					while (true)
					{
						switch (5)
						{
						case 0:
							continue;
						}
						enumerator2.Dispose();
						break;
					}
				}
			}
		}
	}

	/// <summary>
	///       Gets the items that touch the given AABB.
	///       </summary>
	/// <param name="aabb">The axis-aligned bounding box.</param>
	/// <returns>All items that touch the given AABB.</returns>
	public override IEnumerable<T> GetOverlaps(Aabb aabb)
	{
		Update(forceRebuild: false);
		if (c3cb2c0edc4d55dd1410e3483d1ef60d4 == null)
		{
			yield break;
		}
		Stack<c07a248008e037e41899c8b004ace7a50> stack = ResourcePools<c07a248008e037e41899c8b004ace7a50>.Stacks.Obtain();
		stack.Push(c3cb2c0edc4d55dd1410e3483d1ef60d4);
		while (stack.Count > 0)
		{
			c07a248008e037e41899c8b004ace7a50 c07a248008e037e41899c8b004ace7a51 = stack.Pop();
			if (!GeometryHelper.HaveContact(c07a248008e037e41899c8b004ace7a51.c3e263e990bc8d6c3a2d636ac7a19c6c6, aabb))
			{
				continue;
			}
			if (c07a248008e037e41899c8b004ace7a51.cfd73e16b19bb8bde65db991a787eb5ad)
			{
				while (true)
				{
					switch (1)
					{
					case 0:
						continue;
					}
					break;
				}
				if (1 == 0)
				{
					__ldtoken(DynamicAabbTree<>.c7d96806d88c569e449caf11ddcd2f6dc.MoveNext());
				}
				yield return c07a248008e037e41899c8b004ace7a51.c652f269a40a2519b8658ffbade581b4e;
			}
			else
			{
				stack.Push(c07a248008e037e41899c8b004ace7a51.c2d9d869d0c8fd5fb55f452375f580a4a);
				stack.Push(c07a248008e037e41899c8b004ace7a51.c6d3fb34101b9e1ce92055c2914d2d1de);
			}
		}
		ResourcePools<c07a248008e037e41899c8b004ace7a50>.Stacks.Recycle(stack);
	}

	private IEnumerable<T> ce38337505be42dfdd4b0b11dd3a5ea14(c07a248008e037e41899c8b004ace7a50 ce9beaab8ed4de70dd0e96a36b4c42105)
	{
		Update(forceRebuild: false);
		if (c3cb2c0edc4d55dd1410e3483d1ef60d4 == null)
		{
			yield break;
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
				__ldtoken(DynamicAabbTree<>.c1e9acb92d100bc5baf1f66966856de30.MoveNext());
			}
			Stack<c07a248008e037e41899c8b004ace7a50> stack = ResourcePools<c07a248008e037e41899c8b004ace7a50>.Stacks.Obtain();
			stack.Push(c3cb2c0edc4d55dd1410e3483d1ef60d4);
			while (stack.Count > 0)
			{
				c07a248008e037e41899c8b004ace7a50 c07a248008e037e41899c8b004ace7a51 = stack.Pop();
				if (c07a248008e037e41899c8b004ace7a51 == ce9beaab8ed4de70dd0e96a36b4c42105 || !GeometryHelper.HaveContact(c07a248008e037e41899c8b004ace7a51.c3e263e990bc8d6c3a2d636ac7a19c6c6, ce9beaab8ed4de70dd0e96a36b4c42105.c3e263e990bc8d6c3a2d636ac7a19c6c6))
				{
					continue;
				}
				if (c07a248008e037e41899c8b004ace7a51.cfd73e16b19bb8bde65db991a787eb5ad)
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
					yield return c07a248008e037e41899c8b004ace7a51.c652f269a40a2519b8658ffbade581b4e;
				}
				else
				{
					stack.Push(c07a248008e037e41899c8b004ace7a51.c2d9d869d0c8fd5fb55f452375f580a4a);
					stack.Push(c07a248008e037e41899c8b004ace7a51.c6d3fb34101b9e1ce92055c2914d2d1de);
				}
			}
			while (true)
			{
				switch (7)
				{
				case 0:
					continue;
				}
				ResourcePools<c07a248008e037e41899c8b004ace7a50>.Stacks.Recycle(stack);
				yield break;
			}
		}
	}

	private IEnumerable<c07a248008e037e41899c8b004ace7a50> c4d76d858205a75f799980f42e41cbe5b(Aabb cdc3439410d15f28489c964ee87b88fe9)
	{
		Update(forceRebuild: false);
		if (c3cb2c0edc4d55dd1410e3483d1ef60d4 == null)
		{
			yield break;
		}
		while (true)
		{
			switch (7)
			{
			case 0:
				continue;
			}
			if (1 == 0)
			{
				__ldtoken(DynamicAabbTree<>.ccdf73ae11d78eac711a5686f9b1906f4.MoveNext());
			}
			Stack<c07a248008e037e41899c8b004ace7a50> stack = ResourcePools<c07a248008e037e41899c8b004ace7a50>.Stacks.Obtain();
			stack.Push(c3cb2c0edc4d55dd1410e3483d1ef60d4);
			while (stack.Count > 0)
			{
				c07a248008e037e41899c8b004ace7a50 c07a248008e037e41899c8b004ace7a51 = stack.Pop();
				if (!GeometryHelper.HaveContact(c07a248008e037e41899c8b004ace7a51.c3e263e990bc8d6c3a2d636ac7a19c6c6, cdc3439410d15f28489c964ee87b88fe9))
				{
					continue;
				}
				while (true)
				{
					switch (4)
					{
					case 0:
						continue;
					}
					break;
				}
				if (c07a248008e037e41899c8b004ace7a51.cfd73e16b19bb8bde65db991a787eb5ad)
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
					yield return c07a248008e037e41899c8b004ace7a51;
				}
				else
				{
					stack.Push(c07a248008e037e41899c8b004ace7a51.c2d9d869d0c8fd5fb55f452375f580a4a);
					stack.Push(c07a248008e037e41899c8b004ace7a51.c6d3fb34101b9e1ce92055c2914d2d1de);
				}
			}
			while (true)
			{
				switch (5)
				{
				case 0:
					continue;
				}
				ResourcePools<c07a248008e037e41899c8b004ace7a50>.Stacks.Recycle(stack);
				yield break;
			}
		}
	}

	/// <summary>
	///       Gets the items that touch the given ray.
	///       </summary>
	/// <param name="ray">The ray.</param>
	/// <returns>All items that are hit by the ray.</returns>
	public override IEnumerable<T> GetOverlaps(Ray ray)
	{
		Update(forceRebuild: false);
		if (c3cb2c0edc4d55dd1410e3483d1ef60d4 == null)
		{
			yield break;
		}
		while (true)
		{
			switch (3)
			{
			case 0:
				continue;
			}
			if (1 == 0)
			{
				__ldtoken(DynamicAabbTree<>.ccce8bde3a443c4f5864a0c2100fcf735.MoveNext());
			}
			Vector3F c3ec982f192efbba86e42a1cea5c = new Vector3F(1f / ray.Direction.X, 1f / ray.Direction.Y, 1f / ray.Direction.Z);
			float c912da9d2d82e2a6523be60260abdfc = Numeric.EpsilonF * (1f + Aabb.Extent.Length);
			Stack<c07a248008e037e41899c8b004ace7a50> stack = ResourcePools<c07a248008e037e41899c8b004ace7a50>.Stacks.Obtain();
			stack.Push(c3cb2c0edc4d55dd1410e3483d1ef60d4);
			while (stack.Count > 0)
			{
				c07a248008e037e41899c8b004ace7a50 c07a248008e037e41899c8b004ace7a51 = stack.Pop();
				if (GeometryHelper.cdd6d17271fec83883494ba8e43eede6a(c07a248008e037e41899c8b004ace7a51.c3e263e990bc8d6c3a2d636ac7a19c6c6, ray.Origin, c3ec982f192efbba86e42a1cea5c, ray.Length, c912da9d2d82e2a6523be60260abdfc))
				{
					if (c07a248008e037e41899c8b004ace7a51.cfd73e16b19bb8bde65db991a787eb5ad)
					{
						yield return c07a248008e037e41899c8b004ace7a51.c652f269a40a2519b8658ffbade581b4e;
						continue;
					}
					stack.Push(c07a248008e037e41899c8b004ace7a51.c2d9d869d0c8fd5fb55f452375f580a4a);
					stack.Push(c07a248008e037e41899c8b004ace7a51.c6d3fb34101b9e1ce92055c2914d2d1de);
				}
			}
			ResourcePools<c07a248008e037e41899c8b004ace7a50>.Stacks.Recycle(stack);
			yield break;
		}
	}

	/// <summary>
	///       Gets all items that are candidates for the smallest closest-point distance to a given
	///       axis-aligned bounding box (AABB).
	///       </summary>
	/// <param name="aabb">The axis-aligned bounding box (AABB).</param>
	/// <param name="maxDistanceSquared">
	///       The allowed squared distance between two points. (This an optional parameter that is used 
	///       internally to improve performance. The <paramref name="callback" /> might still be called 
	///       with an item that has a distance greater than <c>Math.Sqrt(maxDistanceSquared)</c>! So use 
	///       this parameter with care. To check all items, set the parameter to 
	///       <see cref="F:System.Single.PositiveInfinity" />.)
	///       </param>
	/// <param name="callback">
	///       The callback that is called with each found candidate item. The method must compute the
	///       closest-point on the candidate item and return the squared closest-point distance.
	///       </param>
	/// <returns>
	///       The squared closest-point distance found during the search. -1 if the search was aborted or 
	///       the search space is empty.
	///       </returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="callback" /> is <see langword="null" />.
	///       </exception>
	public float GetClosestPointCandidates(Aabb aabb, float maxDistanceSquared, Func<T, float> callback)
	{
		if (callback == null)
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
						__ldtoken(DynamicAabbTree<>.GetClosestPointCandidates(Aabb, float, Func<T, float>));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1023));
				}
			}
		}
		Update(forceRebuild: false);
		if (c3cb2c0edc4d55dd1410e3483d1ef60d4 == null)
		{
			while (true)
			{
				switch (2)
				{
				case 0:
					break;
				default:
					return -1f;
				}
			}
		}
		float c2878235e00baff8166ebc90ef27400ae = maxDistanceSquared;
		cb780b4b51062221f27d196ea0a7abde7(c3cb2c0edc4d55dd1410e3483d1ef60d4, aabb, callback, ref c2878235e00baff8166ebc90ef27400ae);
		return c2878235e00baff8166ebc90ef27400ae;
	}

	private static void cb780b4b51062221f27d196ea0a7abde7(c07a248008e037e41899c8b004ace7a50 c7f587bd57d47897172f47483e6bcca68, Aabb cdc3439410d15f28489c964ee87b88fe9, Func<T, float> ce4b7628f9880cef87f99eae50a8132fe, ref float c2878235e00baff8166ebc90ef27400ae)
	{
		if (c2878235e00baff8166ebc90ef27400ae < 0f)
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
						__ldtoken(DynamicAabbTree<>.cb780b4b51062221f27d196ea0a7abde7(c07a248008e037e41899c8b004ace7a50, Aabb, Func<T, float>, ref float));
					}
					return;
				}
			}
		}
		if (c2878235e00baff8166ebc90ef27400ae == 0f && !GeometryHelper.HaveContact(cdc3439410d15f28489c964ee87b88fe9, c7f587bd57d47897172f47483e6bcca68.c3e263e990bc8d6c3a2d636ac7a19c6c6))
		{
			return;
		}
		if (c7f587bd57d47897172f47483e6bcca68.cfd73e16b19bb8bde65db991a787eb5ad)
		{
			float val = ce4b7628f9880cef87f99eae50a8132fe(c7f587bd57d47897172f47483e6bcca68.c652f269a40a2519b8658ffbade581b4e);
			c2878235e00baff8166ebc90ef27400ae = Math.Min(val, c2878235e00baff8166ebc90ef27400ae);
			return;
		}
		c07a248008e037e41899c8b004ace7a50 c6d3fb34101b9e1ce92055c2914d2d1de = c7f587bd57d47897172f47483e6bcca68.c6d3fb34101b9e1ce92055c2914d2d1de;
		c07a248008e037e41899c8b004ace7a50 c2d9d869d0c8fd5fb55f452375f580a4a = c7f587bd57d47897172f47483e6bcca68.c2d9d869d0c8fd5fb55f452375f580a4a;
		if (c2878235e00baff8166ebc90ef27400ae == 0f)
		{
			cb780b4b51062221f27d196ea0a7abde7(c6d3fb34101b9e1ce92055c2914d2d1de, cdc3439410d15f28489c964ee87b88fe9, ce4b7628f9880cef87f99eae50a8132fe, ref c2878235e00baff8166ebc90ef27400ae);
			cb780b4b51062221f27d196ea0a7abde7(c2d9d869d0c8fd5fb55f452375f580a4a, cdc3439410d15f28489c964ee87b88fe9, ce4b7628f9880cef87f99eae50a8132fe, ref c2878235e00baff8166ebc90ef27400ae);
			return;
		}
		float num = GeometryHelper.c6d56a7842537f534a2043d488cf4a1e6(cdc3439410d15f28489c964ee87b88fe9, c6d3fb34101b9e1ce92055c2914d2d1de.c3e263e990bc8d6c3a2d636ac7a19c6c6);
		float num2 = GeometryHelper.c6d56a7842537f534a2043d488cf4a1e6(cdc3439410d15f28489c964ee87b88fe9, c2d9d869d0c8fd5fb55f452375f580a4a.c3e263e990bc8d6c3a2d636ac7a19c6c6);
		if (num < num2)
		{
			if (num > c2878235e00baff8166ebc90ef27400ae)
			{
				return;
			}
			cb780b4b51062221f27d196ea0a7abde7(c6d3fb34101b9e1ce92055c2914d2d1de, cdc3439410d15f28489c964ee87b88fe9, ce4b7628f9880cef87f99eae50a8132fe, ref c2878235e00baff8166ebc90ef27400ae);
			if (num2 > c2878235e00baff8166ebc90ef27400ae)
			{
				while (true)
				{
					switch (2)
					{
					case 0:
						break;
					default:
						return;
					}
				}
			}
			cb780b4b51062221f27d196ea0a7abde7(c2d9d869d0c8fd5fb55f452375f580a4a, cdc3439410d15f28489c964ee87b88fe9, ce4b7628f9880cef87f99eae50a8132fe, ref c2878235e00baff8166ebc90ef27400ae);
		}
		else if (!(num2 > c2878235e00baff8166ebc90ef27400ae))
		{
			cb780b4b51062221f27d196ea0a7abde7(c2d9d869d0c8fd5fb55f452375f580a4a, cdc3439410d15f28489c964ee87b88fe9, ce4b7628f9880cef87f99eae50a8132fe, ref c2878235e00baff8166ebc90ef27400ae);
			if (!(num > c2878235e00baff8166ebc90ef27400ae))
			{
				cb780b4b51062221f27d196ea0a7abde7(c6d3fb34101b9e1ce92055c2914d2d1de, cdc3439410d15f28489c964ee87b88fe9, ce4b7628f9880cef87f99eae50a8132fe, ref c2878235e00baff8166ebc90ef27400ae);
			}
		}
	}

	/// <summary>
	///       When implemented in a derived class, creates a new instance of the 
	///       <see cref="T:DigitalRune.Geometry.Partitioning.BasePartition`1" /> derived class.
	///       </summary>
	/// <returns>The new instance.</returns>
	protected override BasePartition<T> CreateInstanceCore()
	{
		return new DynamicAabbTree<T>();
	}

	/// <summary>
	///       Makes the instance a clone (deep copy) of the specified <see cref="T:DigitalRune.Geometry.Partitioning.BasePartition`1" />.
	///       </summary>
	/// <param name="sourcePartition">The object to clone.</param>
	protected override void CloneCore(BasePartition<T> sourcePartition)
	{
		base.CloneCore(sourcePartition);
		DynamicAabbTree<T> dynamicAabbTree = (DynamicAabbTree<T>)sourcePartition;
		EnableMotionPrediction = dynamicAabbTree.EnableMotionPrediction;
		MotionPrediction = dynamicAabbTree.MotionPrediction;
		OptimizationPerFrame = dynamicAabbTree.OptimizationPerFrame;
		RelativeMargin = dynamicAabbTree.RelativeMargin;
	}

	internal override void ced5f5b0bd8302f3e8ade582954f3650d(bool ce8ca0a645441ac556aaf6b4574dbbb1f, HashSet<T> c049085d0fafa74bddedd7455b511e4d2, HashSet<T> c2176039f062c6700652655f4b1223258, HashSet<T> c7a74c15b192971583f3f676b15968063)
	{
		int num;
		if (EnableSelfOverlaps)
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
				__ldtoken(DynamicAabbTree<>.ced5f5b0bd8302f3e8ade582954f3650d(bool, HashSet<T>, HashSet<T>, HashSet<T>));
			}
			if (c7a74c15b192971583f3f676b15968063 != null)
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
				num = ((c7a74c15b192971583f3f676b15968063.Count > 0 || c049085d0fafa74bddedd7455b511e4d2.Count > 0) ? 1 : 0);
				goto IL_0046;
			}
		}
		num = 0;
		goto IL_0046;
		IL_0391:
		c527912fe7c25ec4be62695147b7edc1d();
		List<c07a248008e037e41899c8b004ace7a50> list;
		ce759ff2781e7c0601193b90d63f12c3c(c049085d0fafa74bddedd7455b511e4d2, c2176039f062c6700652655f4b1223258, c7a74c15b192971583f3f676b15968063, list);
		if (list != null)
		{
			ResourcePools<c07a248008e037e41899c8b004ace7a50>.Lists.Recycle(list);
		}
		return;
		IL_0046:
		bool flag = (byte)num != 0;
		object obj;
		if (!flag)
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
			obj = null;
		}
		else
		{
			obj = ResourcePools<c07a248008e037e41899c8b004ace7a50>.Lists.Obtain();
		}
		list = (List<c07a248008e037e41899c8b004ace7a50>)obj;
		if (c3cb2c0edc4d55dd1410e3483d1ef60d4 != null)
		{
			while (true)
			{
				switch (1)
				{
				case 0:
					continue;
				}
				break;
			}
			if (!ce8ca0a645441ac556aaf6b4574dbbb1f)
			{
				if (c2176039f062c6700652655f4b1223258.Count > 0)
				{
					using (Dictionary<T, bool>.KeyCollection.Enumerator enumerator = c2176039f062c6700652655f4b1223258.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							T current = enumerator.Current;
							c07a248008e037e41899c8b004ace7a50 c07a248008e037e41899c8b004ace7a51 = cd311d3823f638b023eacb767b312cc3e.c63830c034a936b954f36284b38a8762c(current);
							c3d1fce75fd975cd49156dca71180a13f(c07a248008e037e41899c8b004ace7a51);
							cd311d3823f638b023eacb767b312cc3e.Remove(c07a248008e037e41899c8b004ace7a51);
							c53fef33d7344726da08aeb063ec62121.Recycle(c07a248008e037e41899c8b004ace7a51);
						}
						while (true)
						{
							switch (6)
							{
							case 0:
								break;
							default:
								goto end_IL_0193;
							}
							continue;
							end_IL_0193:
							break;
						}
					}
					c227cf8833b514efe5292f908b9579bb7 = Count;
				}
				if (c7a74c15b192971583f3f676b15968063 == null)
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
					c4331eef59d25a795f3b70890347a4c6c(c3cb2c0edc4d55dd1410e3483d1ef60d4, c7a74c15b192971583f3f676b15968063);
					c227cf8833b514efe5292f908b9579bb7 = Count;
				}
				else
				{
					object obj2;
					if (!flag)
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
						obj2 = null;
					}
					else
					{
						obj2 = ResourcePools<T>.Lists.Obtain();
					}
					List<T> list2 = (List<T>)obj2;
					foreach (T item in c7a74c15b192971583f3f676b15968063)
					{
						c07a248008e037e41899c8b004ace7a50 c07a248008e037e41899c8b004ace7a52 = cd311d3823f638b023eacb767b312cc3e.c63830c034a936b954f36284b38a8762c(item);
						Aabb c0e2590e59719a898b36fff5591124a7a = GetAabbForItem(item);
						bool flag2 = c9e1b6071a804095ee7ff5523d5d5fb61(c07a248008e037e41899c8b004ace7a52, c0e2590e59719a898b36fff5591124a7a);
						if (!flag)
						{
							continue;
						}
						if (flag2)
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
							list.Add(c07a248008e037e41899c8b004ace7a52);
						}
						else
						{
							list2.Add(item);
						}
					}
					if (flag)
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
						using (List<T>.Enumerator enumerator3 = list2.GetEnumerator())
						{
							while (enumerator3.MoveNext())
							{
								T current3 = enumerator3.Current;
								c7a74c15b192971583f3f676b15968063.Remove(current3);
							}
							while (true)
							{
								switch (1)
								{
								case 0:
									break;
								default:
									goto end_IL_02bc;
								}
								continue;
								end_IL_02bc:
								break;
							}
						}
						ResourcePools<T>.Lists.Recycle(list2);
					}
					c227cf8833b514efe5292f908b9579bb7 = Count;
				}
				c2dd2cdc0dd26bb037b05b7a5696c0902();
				if (c049085d0fafa74bddedd7455b511e4d2.Count > 0)
				{
					foreach (T item2 in c049085d0fafa74bddedd7455b511e4d2)
					{
						c07a248008e037e41899c8b004ace7a50 c07a248008e037e41899c8b004ace7a53 = c53fef33d7344726da08aeb063ec62121.Obtain();
						c07a248008e037e41899c8b004ace7a53.c3e263e990bc8d6c3a2d636ac7a19c6c6 = GetAabbForItem(item2);
						c07a248008e037e41899c8b004ace7a53.c652f269a40a2519b8658ffbade581b4e = item2;
						c437cc650c1bbb3b5d6a346455cd9229c(c3cb2c0edc4d55dd1410e3483d1ef60d4, c07a248008e037e41899c8b004ace7a53);
						cd311d3823f638b023eacb767b312cc3e.Add(c07a248008e037e41899c8b004ace7a53);
						if (flag)
						{
							list.Add(c07a248008e037e41899c8b004ace7a53);
						}
					}
					c227cf8833b514efe5292f908b9579bb7 = Count;
				}
				goto IL_0391;
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
		}
		ccf1870247a728556a633ff212007937e();
		if (flag)
		{
			foreach (T item3 in c7a74c15b192971583f3f676b15968063)
			{
				list.Add(cd311d3823f638b023eacb767b312cc3e.c63830c034a936b954f36284b38a8762c(item3));
			}
			using Dictionary<T, bool>.KeyCollection.Enumerator enumerator6 = c049085d0fafa74bddedd7455b511e4d2.GetEnumerator();
			while (enumerator6.MoveNext())
			{
				T current6 = enumerator6.Current;
				list.Add(cd311d3823f638b023eacb767b312cc3e.c63830c034a936b954f36284b38a8762c(current6));
			}
			while (true)
			{
				switch (1)
				{
				case 0:
					break;
				default:
					goto end_IL_0109;
				}
				continue;
				end_IL_0109:
				break;
			}
		}
		c227cf8833b514efe5292f908b9579bb7 = 0;
		goto IL_0391;
	}

	private void c2dd2cdc0dd26bb037b05b7a5696c0902()
	{
		if (c227cf8833b514efe5292f908b9579bb7 > 0)
		{
			int val = (int)((float)cd311d3823f638b023eacb767b312cc3e.Count * OptimizationPerFrame);
			val = Math.Max(1, val);
			cceb3668cd65480d681d53a1cfb0988dd(val);
			c227cf8833b514efe5292f908b9579bb7 -= val;
		}
	}

	private void c527912fe7c25ec4be62695147b7edc1d()
	{
		Aabb = ((c3cb2c0edc4d55dd1410e3483d1ef60d4 != null) ? c3cb2c0edc4d55dd1410e3483d1ef60d4.c3e263e990bc8d6c3a2d636ac7a19c6c6 : default(Aabb));
	}

	private void ce759ff2781e7c0601193b90d63f12c3c(HashSet<T> c049085d0fafa74bddedd7455b511e4d2, HashSet<T> c2176039f062c6700652655f4b1223258, HashSet<T> c7a74c15b192971583f3f676b15968063, List<c07a248008e037e41899c8b004ace7a50> cf58a64beaaf4a5928040b63db7f27deb)
	{
		if (!EnableSelfOverlaps)
		{
			return;
		}
		if (c7a74c15b192971583f3f676b15968063 != null)
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
				__ldtoken(DynamicAabbTree<>.ce759ff2781e7c0601193b90d63f12c3c(HashSet<T>, HashSet<T>, HashSet<T>, List<c07a248008e037e41899c8b004ace7a50>));
			}
			if (c049085d0fafa74bddedd7455b511e4d2.Count + c7a74c15b192971583f3f676b15968063.Count <= Count / 2)
			{
				bool flag = c2176039f062c6700652655f4b1223258.Count > 0;
				bool flag2 = c7a74c15b192971583f3f676b15968063.Count > 0;
				if (!flag)
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
					if (!flag2)
					{
						goto IL_01f7;
					}
					while (true)
					{
						switch (6)
						{
						case 0:
							continue;
						}
						break;
					}
				}
				List<Pair<T>> list = ResourcePools<Pair<T>>.Lists.Obtain();
				using (Dictionary<Pair<T>, bool>.KeyCollection.Enumerator enumerator = c65379642bd949dc7e978580b9649b9bc.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Pair<T> current = enumerator.Current;
						if (!flag)
						{
							goto IL_0151;
						}
						if (!c2176039f062c6700652655f4b1223258.Contains(current.First))
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
							if (!c2176039f062c6700652655f4b1223258.Contains(current.Second))
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
								goto IL_0151;
							}
						}
						goto IL_017e;
						IL_017e:
						list.Add(current);
						continue;
						IL_0151:
						if (!flag2)
						{
							continue;
						}
						if (!c7a74c15b192971583f3f676b15968063.Contains(current.First))
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
							if (!c7a74c15b192971583f3f676b15968063.Contains(current.Second))
							{
								continue;
							}
						}
						goto IL_017e;
					}
					while (true)
					{
						switch (7)
						{
						case 0:
							break;
						default:
							goto end_IL_0194;
						}
						continue;
						end_IL_0194:
						break;
					}
				}
				foreach (Pair<T> item in list)
				{
					c65379642bd949dc7e978580b9649b9bc.Remove(item);
				}
				ResourcePools<Pair<T>>.Lists.Recycle(list);
				goto IL_01f7;
			}
		}
		c65379642bd949dc7e978580b9649b9bc.Clear();
		if (c3cb2c0edc4d55dd1410e3483d1ef60d4 == null)
		{
			return;
		}
		while (true)
		{
			switch (7)
			{
			case 0:
				continue;
			}
			using IEnumerator<Pair<T>> enumerator3 = GetOverlaps(this).GetEnumerator();
			while (enumerator3.MoveNext())
			{
				Pair<T> current3 = enumerator3.Current;
				if (c8092bfef08eaf2732b73394233ee5fd1(current3))
				{
					c65379642bd949dc7e978580b9649b9bc.Add(current3);
				}
			}
			while (true)
			{
				switch (7)
				{
				case 0:
					break;
				default:
					return;
				}
			}
		}
		IL_01f7:
		if (cf58a64beaaf4a5928040b63db7f27deb == null)
		{
			return;
		}
		while (true)
		{
			switch (1)
			{
			case 0:
				continue;
			}
			using List<c07a248008e037e41899c8b004ace7a50>.Enumerator enumerator4 = cf58a64beaaf4a5928040b63db7f27deb.GetEnumerator();
			while (enumerator4.MoveNext())
			{
				c07a248008e037e41899c8b004ace7a50 current4 = enumerator4.Current;
				IEnumerator<T> enumerator5 = ce38337505be42dfdd4b0b11dd3a5ea14(current4).GetEnumerator();
				try
				{
					while (enumerator5.MoveNext())
					{
						T current5 = enumerator5.Current;
						Pair<T> pair = new Pair<T>(current4.c652f269a40a2519b8658ffbade581b4e, current5);
						if (c8092bfef08eaf2732b73394233ee5fd1(pair))
						{
							c65379642bd949dc7e978580b9649b9bc.Add(pair);
						}
					}
					while (true)
					{
						switch (1)
						{
						case 0:
							break;
						default:
							goto end_IL_0276;
						}
						continue;
						end_IL_0276:
						break;
					}
				}
				finally
				{
					if (enumerator5 != null)
					{
						while (true)
						{
							switch (3)
							{
							case 0:
								continue;
							}
							enumerator5.Dispose();
							break;
						}
					}
				}
			}
			while (true)
			{
				switch (6)
				{
				case 0:
					break;
				default:
					return;
				}
			}
		}
	}

	[CompilerGenerated]
	private static cbe5649b18b8b0caa00be9ad49188b193<T> caec689d38e9bfa233d33fa98cc978d29()
	{
		return c53fef33d7344726da08aeb063ec62121.Obtain();
	}

	[CompilerGenerated]
	private static c07a248008e037e41899c8b004ace7a50 c0216c7a14f7386d869f5055d39047d73()
	{
		return new c07a248008e037e41899c8b004ace7a50();
	}

	[CompilerGenerated]
	private static void ca6709d558f09fa8a72546ec488bb3fea(c07a248008e037e41899c8b004ace7a50 c7f587bd57d47897172f47483e6bcca68)
	{
		c7f587bd57d47897172f47483e6bcca68.c652f269a40a2519b8658ffbade581b4e = default;
		c7f587bd57d47897172f47483e6bcca68.c6d3fb34101b9e1ce92055c2914d2d1de = null;
		c7f587bd57d47897172f47483e6bcca68.c2d9d869d0c8fd5fb55f452375f580a4a = null;
		c7f587bd57d47897172f47483e6bcca68.c551fa69e7c0ec492ab91daa4199210a2 = null;
	}
}
