using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using A;
using DigitalRune.Collections;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Partitioning;

/// <summary>
///       Provides basic functionality of an <see cref="T:DigitalRune.Geometry.Partitioning.ISpatialPartition`1" />. (For internal use 
///       only.)
///       </summary>
/// <typeparam name="T">The type of items in the spatial partition.</typeparam>
public abstract class BasePartition<T> : ICollection<T>, IEnumerable<T>, IEnumerable, ISpatialPartition<T>, ICloneable
{
	[CompilerGenerated]
	private sealed class cc9a4ac9c6f0bf2531abb1d59b82c3c2f : IEnumerable<T>, IEnumerator<T>, IEnumerable, IEnumerator, IDisposable
	{
		private T cd179d7f18da070d164af6afcb0af45b5;

		private int c94f69126adc708dfc5f5fd5b63c52835;

		private int c0e8a058e779916e7715d7616fb0da9e7;

		public BasePartition<T> c1b0bbfd4af1b6a4c0c71a5dd3c509812;

		public T cf51048ba330b8d3f481ea5bc25a03eb9;

		public T c69d0fa2e8754a1aa449b5a0f88b1178d;

		public Aabb c7ac8911c74bd3aff26e66a07bfbe89cc;

		public T cfeccdd2cbc66d939e1d8e76a38acadab;

		public IEnumerator<T> c908c20a7dc77624948085be2fc57df8d;

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
		public cc9a4ac9c6f0bf2531abb1d59b82c3c2f(int c94f69126adc708dfc5f5fd5b63c52835)
		{
			this.c94f69126adc708dfc5f5fd5b63c52835 = c94f69126adc708dfc5f5fd5b63c52835;
			c0e8a058e779916e7715d7616fb0da9e7 = Thread.CurrentThread.ManagedThreadId;
		}

		[DebuggerHidden]
		private IEnumerator<T> c0843ff4e059c4450bad00d5ba0b6ef48()
		{
			cc9a4ac9c6f0bf2531abb1d59b82c3c2f cc9a4ac9c6f0bf2531abb1d59b82c3c2f2;
			if (Thread.CurrentThread.ManagedThreadId == c0e8a058e779916e7715d7616fb0da9e7)
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
					__ldtoken(BasePartition<>.cc9a4ac9c6f0bf2531abb1d59b82c3c2f.c0843ff4e059c4450bad00d5ba0b6ef48());
				}
				if (c94f69126adc708dfc5f5fd5b63c52835 == -2)
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
					c94f69126adc708dfc5f5fd5b63c52835 = 0;
					cc9a4ac9c6f0bf2531abb1d59b82c3c2f2 = this;
					goto IL_0057;
				}
			}
			cc9a4ac9c6f0bf2531abb1d59b82c3c2f2 = new cc9a4ac9c6f0bf2531abb1d59b82c3c2f(0)
			{
				c1b0bbfd4af1b6a4c0c71a5dd3c509812 = c1b0bbfd4af1b6a4c0c71a5dd3c509812
			};
			goto IL_0057;
			IL_0057:
			cc9a4ac9c6f0bf2531abb1d59b82c3c2f2.cf51048ba330b8d3f481ea5bc25a03eb9 = c69d0fa2e8754a1aa449b5a0f88b1178d;
			return cc9a4ac9c6f0bf2531abb1d59b82c3c2f2;
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
					c94f69126adc708dfc5f5fd5b63c52835 = -1;
					c7ac8911c74bd3aff26e66a07bfbe89cc = c1b0bbfd4af1b6a4c0c71a5dd3c509812.GetAabbForItem(cf51048ba330b8d3f481ea5bc25a03eb9);
					c908c20a7dc77624948085be2fc57df8d = c1b0bbfd4af1b6a4c0c71a5dd3c509812.GetOverlaps(c7ac8911c74bd3aff26e66a07bfbe89cc).GetEnumerator();
					c94f69126adc708dfc5f5fd5b63c52835 = 1;
					goto IL_00ce;
				case 2:
					{
						c94f69126adc708dfc5f5fd5b63c52835 = 1;
						goto IL_00ce;
					}
					IL_00ce:
					while (c908c20a7dc77624948085be2fc57df8d.MoveNext())
					{
						cfeccdd2cbc66d939e1d8e76a38acadab = c908c20a7dc77624948085be2fc57df8d.Current;
						if (!c1b0bbfd4af1b6a4c0c71a5dd3c509812.c8092bfef08eaf2732b73394233ee5fd1(new Pair<T>(cfeccdd2cbc66d939e1d8e76a38acadab, cf51048ba330b8d3f481ea5bc25a03eb9)))
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
							if (1 == 0)
							{
								__ldtoken(BasePartition<>.cc9a4ac9c6f0bf2531abb1d59b82c3c2f.MoveNext());
							}
							cd179d7f18da070d164af6afcb0af45b5 = cfeccdd2cbc66d939e1d8e76a38acadab;
							c94f69126adc708dfc5f5fd5b63c52835 = 2;
							return true;
						}
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
					c6b2b7bb0138c7a754f602e1325ab5a3a();
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
					c6b2b7bb0138c7a754f602e1325ab5a3a();
				}
			}
		}

		void IDisposable.Dispose()
		{
			//ILSpy generated this explicit interface implementation from .override directive in c087ddb9300ad4d53672b7a60e6d76672
			this.c087ddb9300ad4d53672b7a60e6d76672();
		}

		private void c6b2b7bb0138c7a754f602e1325ab5a3a()
		{
			c94f69126adc708dfc5f5fd5b63c52835 = -1;
			if (c908c20a7dc77624948085be2fc57df8d == null)
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
				if (1 == 0)
				{
					__ldtoken(BasePartition<>.cc9a4ac9c6f0bf2531abb1d59b82c3c2f.c6b2b7bb0138c7a754f602e1325ab5a3a());
				}
				c908c20a7dc77624948085be2fc57df8d.Dispose();
				return;
			}
		}
	}

	[CompilerGenerated]
	private sealed class c7c8d62f8ffafa6384df8cf453b17950c : IEnumerable<T>, IEnumerator<T>, IEnumerable, IEnumerator, IDisposable
	{
		private T cd179d7f18da070d164af6afcb0af45b5;

		private int c94f69126adc708dfc5f5fd5b63c52835;

		private int c0e8a058e779916e7715d7616fb0da9e7;

		public BasePartition<T> c1b0bbfd4af1b6a4c0c71a5dd3c509812;

		public Ray c2d769fcfe2c7e7844e7de692907680c8;

		public Ray c8b1773219813f762be9a1bd49ee5cb56;

		public Aabb cf24ce424b64aff5d6619d2a70b5d1e6e;

		public Vector3F c86127e49e03459707ac4c1f649e8b2b6;

		public float ca06df285c0cfef7b23114887a343ca8e;

		public T ccbd944440cba13268aed98ba2dd9c925;

		public IEnumerator<T> c8d63344aa4f76bfdcbbb972020e18599;

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
		public c7c8d62f8ffafa6384df8cf453b17950c(int c94f69126adc708dfc5f5fd5b63c52835)
		{
			this.c94f69126adc708dfc5f5fd5b63c52835 = c94f69126adc708dfc5f5fd5b63c52835;
			c0e8a058e779916e7715d7616fb0da9e7 = Thread.CurrentThread.ManagedThreadId;
		}

		[DebuggerHidden]
		private IEnumerator<T> c0843ff4e059c4450bad00d5ba0b6ef48()
		{
			c7c8d62f8ffafa6384df8cf453b17950c c7c8d62f8ffafa6384df8cf453b17950c2;
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
					__ldtoken(BasePartition<>.c7c8d62f8ffafa6384df8cf453b17950c.c0843ff4e059c4450bad00d5ba0b6ef48());
				}
				if (c94f69126adc708dfc5f5fd5b63c52835 == -2)
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
					c94f69126adc708dfc5f5fd5b63c52835 = 0;
					c7c8d62f8ffafa6384df8cf453b17950c2 = this;
					goto IL_0057;
				}
			}
			c7c8d62f8ffafa6384df8cf453b17950c2 = new c7c8d62f8ffafa6384df8cf453b17950c(0)
			{
				c1b0bbfd4af1b6a4c0c71a5dd3c509812 = c1b0bbfd4af1b6a4c0c71a5dd3c509812
			};
			goto IL_0057;
			IL_0057:
			c7c8d62f8ffafa6384df8cf453b17950c2.c2d769fcfe2c7e7844e7de692907680c8 = c8b1773219813f762be9a1bd49ee5cb56;
			return c7c8d62f8ffafa6384df8cf453b17950c2;
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
					ref Aabb reference = ref cf24ce424b64aff5d6619d2a70b5d1e6e;
					reference = new Aabb(c2d769fcfe2c7e7844e7de692907680c8.Origin, c2d769fcfe2c7e7844e7de692907680c8.Origin);
					cf24ce424b64aff5d6619d2a70b5d1e6e.Grow(c2d769fcfe2c7e7844e7de692907680c8.Origin + c2d769fcfe2c7e7844e7de692907680c8.Direction * c2d769fcfe2c7e7844e7de692907680c8.Length);
					ref Vector3F reference2 = ref c86127e49e03459707ac4c1f649e8b2b6;
					reference2 = new Vector3F(1f / c2d769fcfe2c7e7844e7de692907680c8.Direction.X, 1f / c2d769fcfe2c7e7844e7de692907680c8.Direction.Y, 1f / c2d769fcfe2c7e7844e7de692907680c8.Direction.Z);
					ca06df285c0cfef7b23114887a343ca8e = Numeric.EpsilonF * (1f + c1b0bbfd4af1b6a4c0c71a5dd3c509812.Aabb.Extent.Length);
					c8d63344aa4f76bfdcbbb972020e18599 = c1b0bbfd4af1b6a4c0c71a5dd3c509812.GetOverlaps(cf24ce424b64aff5d6619d2a70b5d1e6e).GetEnumerator();
					c94f69126adc708dfc5f5fd5b63c52835 = 1;
					goto IL_01b3;
				}
				case 2:
					{
						c94f69126adc708dfc5f5fd5b63c52835 = 1;
						goto IL_01b3;
					}
					IL_01b3:
					while (c8d63344aa4f76bfdcbbb972020e18599.MoveNext())
					{
						ccbd944440cba13268aed98ba2dd9c925 = c8d63344aa4f76bfdcbbb972020e18599.Current;
						if (!GeometryHelper.cdd6d17271fec83883494ba8e43eede6a(c1b0bbfd4af1b6a4c0c71a5dd3c509812.GetAabbForItem(ccbd944440cba13268aed98ba2dd9c925), c2d769fcfe2c7e7844e7de692907680c8.Origin, c86127e49e03459707ac4c1f649e8b2b6, c2d769fcfe2c7e7844e7de692907680c8.Length, ca06df285c0cfef7b23114887a343ca8e))
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
							if (1 == 0)
							{
								__ldtoken(BasePartition<>.c7c8d62f8ffafa6384df8cf453b17950c.MoveNext());
							}
							cd179d7f18da070d164af6afcb0af45b5 = ccbd944440cba13268aed98ba2dd9c925;
							c94f69126adc708dfc5f5fd5b63c52835 = 2;
							return true;
						}
					}
					cd65081c53ab454b2313d9fa45d40e6c7();
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
			if (c8d63344aa4f76bfdcbbb972020e18599 != null)
			{
				c8d63344aa4f76bfdcbbb972020e18599.Dispose();
			}
		}
	}

	[CompilerGenerated]
	private sealed class c9eebdaf74eb20a22ef45e392585ef96c : IEnumerable<Pair<T>>, IEnumerator<Pair<T>>, IEnumerable, IEnumerator, IDisposable
	{
		private Pair<T> cd179d7f18da070d164af6afcb0af45b5;

		private int c94f69126adc708dfc5f5fd5b63c52835;

		private int c0e8a058e779916e7715d7616fb0da9e7;

		public BasePartition<T> c1b0bbfd4af1b6a4c0c71a5dd3c509812;

		public ISpatialPartition<T> cfd8883387afb9cc25a2ac9150de0f66b;

		public ISpatialPartition<T> cd1939b65df62ed5f7a2112466dabd8f0;

		public IEnumerable<T> ca226ca06bb9acea66320f24a0bcf0c1c;

		public T c9b849a04f3b6d815b84ff09ee146314f;

		public Aabb c0717adc7d922e64aee700aa8d6c652c7;

		public IEnumerable<T> c038770a01b494ca13019ad3f1e73057b;

		public T c0b1d3aacbca5ba4f132e7483fbbe6ae1;

		public Pair<T> c6a5feaaa13455090834fb0d19e60472c;

		public IEnumerator<T> c7763114cbca861d35b02c504a0bbd200;

		public IEnumerator<T> c7459b448aace26accd840c751244d10d;

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
		public c9eebdaf74eb20a22ef45e392585ef96c(int c94f69126adc708dfc5f5fd5b63c52835)
		{
			this.c94f69126adc708dfc5f5fd5b63c52835 = c94f69126adc708dfc5f5fd5b63c52835;
			c0e8a058e779916e7715d7616fb0da9e7 = Thread.CurrentThread.ManagedThreadId;
		}

		[DebuggerHidden]
		private IEnumerator<Pair<T>> c4bd63b962f346f189f5c6a4ad2e4ce8d()
		{
			c9eebdaf74eb20a22ef45e392585ef96c c9eebdaf74eb20a22ef45e392585ef96c2;
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
					__ldtoken(BasePartition<>.c9eebdaf74eb20a22ef45e392585ef96c.c4bd63b962f346f189f5c6a4ad2e4ce8d());
				}
				if (c94f69126adc708dfc5f5fd5b63c52835 == -2)
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
					c94f69126adc708dfc5f5fd5b63c52835 = 0;
					c9eebdaf74eb20a22ef45e392585ef96c2 = this;
					goto IL_0059;
				}
			}
			c9eebdaf74eb20a22ef45e392585ef96c2 = new c9eebdaf74eb20a22ef45e392585ef96c(0)
			{
				c1b0bbfd4af1b6a4c0c71a5dd3c509812 = c1b0bbfd4af1b6a4c0c71a5dd3c509812
			};
			goto IL_0059;
			IL_0059:
			c9eebdaf74eb20a22ef45e392585ef96c2.cfd8883387afb9cc25a2ac9150de0f66b = cd1939b65df62ed5f7a2112466dabd8f0;
			return c9eebdaf74eb20a22ef45e392585ef96c2;
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
						switch (6)
						{
						case 0:
							continue;
						}
						break;
					}
					if (1 == 0)
					{
						__ldtoken(BasePartition<>.c9eebdaf74eb20a22ef45e392585ef96c.MoveNext());
					}
					if (num != 3)
					{
						goto IL_01cb;
					}
					c94f69126adc708dfc5f5fd5b63c52835 = 2;
					goto IL_0187;
				}
				c94f69126adc708dfc5f5fd5b63c52835 = -1;
				if (cfd8883387afb9cc25a2ac9150de0f66b == null)
				{
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(994));
				}
				cfd8883387afb9cc25a2ac9150de0f66b.Update(forceRebuild: false);
				c1b0bbfd4af1b6a4c0c71a5dd3c509812.Update(forceRebuild: false);
				ca226ca06bb9acea66320f24a0bcf0c1c = c1b0bbfd4af1b6a4c0c71a5dd3c509812.GetOverlaps(cfd8883387afb9cc25a2ac9150de0f66b.Aabb);
				c7763114cbca861d35b02c504a0bbd200 = ca226ca06bb9acea66320f24a0bcf0c1c.GetEnumerator();
				c94f69126adc708dfc5f5fd5b63c52835 = 1;
				goto IL_01a9;
				IL_0187:
				while (c7459b448aace26accd840c751244d10d.MoveNext())
				{
					c0b1d3aacbca5ba4f132e7483fbbe6ae1 = c7459b448aace26accd840c751244d10d.Current;
					ref Pair<T> reference = ref c6a5feaaa13455090834fb0d19e60472c;
					reference = new Pair<T>(c9b849a04f3b6d815b84ff09ee146314f, c0b1d3aacbca5ba4f132e7483fbbe6ae1);
					if (c1b0bbfd4af1b6a4c0c71a5dd3c509812.Filter != null)
					{
						while (true)
						{
							switch (1)
							{
							case 0:
								break;
							default:
								goto end_IL_013b;
							}
							continue;
							end_IL_013b:
							break;
						}
						if (!c1b0bbfd4af1b6a4c0c71a5dd3c509812.Filter.Filter(c6a5feaaa13455090834fb0d19e60472c))
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
					}
					cd179d7f18da070d164af6afcb0af45b5 = c6a5feaaa13455090834fb0d19e60472c;
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
				cb50c219bd6e0f7436b139e2243784d35();
				goto IL_01a9;
				IL_01a9:
				if (c7763114cbca861d35b02c504a0bbd200.MoveNext())
				{
					c9b849a04f3b6d815b84ff09ee146314f = c7763114cbca861d35b02c504a0bbd200.Current;
					c0717adc7d922e64aee700aa8d6c652c7 = c1b0bbfd4af1b6a4c0c71a5dd3c509812.GetAabbForItem(c9b849a04f3b6d815b84ff09ee146314f);
					c038770a01b494ca13019ad3f1e73057b = cfd8883387afb9cc25a2ac9150de0f66b.GetOverlaps(c0717adc7d922e64aee700aa8d6c652c7);
					c7459b448aace26accd840c751244d10d = c038770a01b494ca13019ad3f1e73057b.GetEnumerator();
					c94f69126adc708dfc5f5fd5b63c52835 = 2;
					goto IL_0187;
				}
				while (true)
				{
					switch (1)
					{
					case 0:
						continue;
					}
					break;
				}
				c179686adf68c7fa2a69bf04da75811c0();
				goto IL_01cb;
				IL_01cb:
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
							cb50c219bd6e0f7436b139e2243784d35();
						}
					}
					break;
				}
				finally
				{
					c179686adf68c7fa2a69bf04da75811c0();
				}
			}
		}

		void IDisposable.Dispose()
		{
			//ILSpy generated this explicit interface implementation from .override directive in c087ddb9300ad4d53672b7a60e6d76672
			this.c087ddb9300ad4d53672b7a60e6d76672();
		}

		private void c179686adf68c7fa2a69bf04da75811c0()
		{
			c94f69126adc708dfc5f5fd5b63c52835 = -1;
			if (c7763114cbca861d35b02c504a0bbd200 != null)
			{
				c7763114cbca861d35b02c504a0bbd200.Dispose();
			}
		}

		private void cb50c219bd6e0f7436b139e2243784d35()
		{
			c94f69126adc708dfc5f5fd5b63c52835 = 1;
			if (c7459b448aace26accd840c751244d10d != null)
			{
				c7459b448aace26accd840c751244d10d.Dispose();
			}
		}
	}

	[CompilerGenerated]
	private sealed class cb73e993320d1305cf214094636482085 : IEnumerable<Pair<T>>, IEnumerator<Pair<T>>, IEnumerable, IEnumerator, IDisposable
	{
		private Pair<T> cd179d7f18da070d164af6afcb0af45b5;

		private int c94f69126adc708dfc5f5fd5b63c52835;

		private int c0e8a058e779916e7715d7616fb0da9e7;

		public BasePartition<T> c1b0bbfd4af1b6a4c0c71a5dd3c509812;

		public Vector3F ceabb47d7d959545d89c4300eabbdd7c2;

		public Vector3F c6f203721b629fec4c93cc63ff1172fc4;

		public Pose cbc1b7e0b011ad6dcdb8beb392ec7a646;

		public Pose cfdb83fd0acea070e804f836eed1cc8f0;

		public ISpatialPartition<T> cfd8883387afb9cc25a2ac9150de0f66b;

		public ISpatialPartition<T> cd1939b65df62ed5f7a2112466dabd8f0;

		public Vector3F c1b4918d9d3cc94171c67339d1fc78b0f;

		public Vector3F cfd54ba1e25c70994847ab953f2228207;

		public Pose cd2abebf7ff6ec7d035c28a04faaa459b;

		public Pose c1c245528512f0ae5611e6e833a70158f;

		public Vector3F cefbcaf73ddf10a435683727cadb477bc;

		public Vector3F c2b9b4eb309d69afed9b1b064f45e343c;

		public Pose c9037c7ab1b4c0fdbd25a7586aafa0332;

		public Pose c17553a842bc3f36184e96a8c1691b06c;

		public Aabb c2397817d9d940df9237b96c710f430b9;

		public T c91b40a65ac87ac689039dc63d60d559f;

		public Aabb c1ed09b56cad6012eec4db080bc10814c;

		public T c2a685559938477879ab3cf468fb06f09;

		public Pair<T> c17adf2b3b5d554836c39de67e6684731;

		public IEnumerator<T> c6e99b7db15ab7fa02952ff58d5ff4111;

		public IEnumerator<T> c0309f8241d4ba516a85112f30073b7e3;

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
		public cb73e993320d1305cf214094636482085(int c94f69126adc708dfc5f5fd5b63c52835)
		{
			this.c94f69126adc708dfc5f5fd5b63c52835 = c94f69126adc708dfc5f5fd5b63c52835;
			c0e8a058e779916e7715d7616fb0da9e7 = Thread.CurrentThread.ManagedThreadId;
		}

		[DebuggerHidden]
		private IEnumerator<Pair<T>> c4bd63b962f346f189f5c6a4ad2e4ce8d()
		{
			cb73e993320d1305cf214094636482085 cb73e993320d1305cf2140946364820852;
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
					__ldtoken(BasePartition<>.cb73e993320d1305cf214094636482085.c4bd63b962f346f189f5c6a4ad2e4ce8d());
				}
				if (c94f69126adc708dfc5f5fd5b63c52835 == -2)
				{
					c94f69126adc708dfc5f5fd5b63c52835 = 0;
					cb73e993320d1305cf2140946364820852 = this;
					goto IL_0051;
				}
			}
			cb73e993320d1305cf2140946364820852 = new cb73e993320d1305cf214094636482085(0)
			{
				c1b0bbfd4af1b6a4c0c71a5dd3c509812 = c1b0bbfd4af1b6a4c0c71a5dd3c509812
			};
			goto IL_0051;
			IL_0051:
			cb73e993320d1305cf2140946364820852.ceabb47d7d959545d89c4300eabbdd7c2 = c6f203721b629fec4c93cc63ff1172fc4;
			cb73e993320d1305cf2140946364820852.cbc1b7e0b011ad6dcdb8beb392ec7a646 = cfdb83fd0acea070e804f836eed1cc8f0;
			cb73e993320d1305cf2140946364820852.cfd8883387afb9cc25a2ac9150de0f66b = cd1939b65df62ed5f7a2112466dabd8f0;
			cb73e993320d1305cf2140946364820852.c1b4918d9d3cc94171c67339d1fc78b0f = cfd54ba1e25c70994847ab953f2228207;
			cb73e993320d1305cf2140946364820852.cd2abebf7ff6ec7d035c28a04faaa459b = c1c245528512f0ae5611e6e833a70158f;
			return cb73e993320d1305cf2140946364820852;
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
							__ldtoken(BasePartition<>.cb73e993320d1305cf214094636482085.MoveNext());
						}
						goto IL_0260;
					}
					c94f69126adc708dfc5f5fd5b63c52835 = 2;
					goto IL_0232;
				}
				c94f69126adc708dfc5f5fd5b63c52835 = -1;
				if (cfd8883387afb9cc25a2ac9150de0f66b == null)
				{
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(994));
				}
				cfd8883387afb9cc25a2ac9150de0f66b.Update(forceRebuild: false);
				c1b0bbfd4af1b6a4c0c71a5dd3c509812.Update(forceRebuild: false);
				cefbcaf73ddf10a435683727cadb477bc = Vector3F.One / ceabb47d7d959545d89c4300eabbdd7c2;
				c2b9b4eb309d69afed9b1b064f45e343c = Vector3F.One / c1b4918d9d3cc94171c67339d1fc78b0f;
				c9037c7ab1b4c0fdbd25a7586aafa0332 = cbc1b7e0b011ad6dcdb8beb392ec7a646.Inverse * cd2abebf7ff6ec7d035c28a04faaa459b;
				c17553a842bc3f36184e96a8c1691b06c = c9037c7ab1b4c0fdbd25a7586aafa0332.Inverse;
				c2397817d9d940df9237b96c710f430b9 = cfd8883387afb9cc25a2ac9150de0f66b.Aabb;
				c2397817d9d940df9237b96c710f430b9 = c2397817d9d940df9237b96c710f430b9.GetAabb(c1b4918d9d3cc94171c67339d1fc78b0f, c9037c7ab1b4c0fdbd25a7586aafa0332);
				c2397817d9d940df9237b96c710f430b9.Scale(cefbcaf73ddf10a435683727cadb477bc);
				c6e99b7db15ab7fa02952ff58d5ff4111 = c1b0bbfd4af1b6a4c0c71a5dd3c509812.GetOverlaps(c2397817d9d940df9237b96c710f430b9).GetEnumerator();
				c94f69126adc708dfc5f5fd5b63c52835 = 1;
				goto IL_0248;
				IL_0260:
				return false;
				IL_0248:
				if (c6e99b7db15ab7fa02952ff58d5ff4111.MoveNext())
				{
					c91b40a65ac87ac689039dc63d60d559f = c6e99b7db15ab7fa02952ff58d5ff4111.Current;
					c1ed09b56cad6012eec4db080bc10814c = c1b0bbfd4af1b6a4c0c71a5dd3c509812.GetAabbForItem(c91b40a65ac87ac689039dc63d60d559f);
					c1ed09b56cad6012eec4db080bc10814c = c1ed09b56cad6012eec4db080bc10814c.GetAabb(ceabb47d7d959545d89c4300eabbdd7c2, c17553a842bc3f36184e96a8c1691b06c);
					c1ed09b56cad6012eec4db080bc10814c.Scale(c2b9b4eb309d69afed9b1b064f45e343c);
					c0309f8241d4ba516a85112f30073b7e3 = cfd8883387afb9cc25a2ac9150de0f66b.GetOverlaps(c1ed09b56cad6012eec4db080bc10814c).GetEnumerator();
					c94f69126adc708dfc5f5fd5b63c52835 = 2;
					goto IL_0232;
				}
				c5449f5ffb2d2fc1652ba8252da9ab019();
				goto IL_0260;
				IL_0232:
				while (c0309f8241d4ba516a85112f30073b7e3.MoveNext())
				{
					c2a685559938477879ab3cf468fb06f09 = c0309f8241d4ba516a85112f30073b7e3.Current;
					ref Pair<T> reference = ref c17adf2b3b5d554836c39de67e6684731;
					reference = new Pair<T>(c91b40a65ac87ac689039dc63d60d559f, c2a685559938477879ab3cf468fb06f09);
					if (c1b0bbfd4af1b6a4c0c71a5dd3c509812.Filter != null)
					{
						while (true)
						{
							switch (3)
							{
							case 0:
								break;
							default:
								goto end_IL_01f2;
							}
							continue;
							end_IL_01f2:
							break;
						}
						if (!c1b0bbfd4af1b6a4c0c71a5dd3c509812.Filter.Filter(c17adf2b3b5d554836c39de67e6684731))
						{
							continue;
						}
					}
					cd179d7f18da070d164af6afcb0af45b5 = c17adf2b3b5d554836c39de67e6684731;
					c94f69126adc708dfc5f5fd5b63c52835 = 3;
					return true;
				}
				cd1a446c7c13541a442f9e29567e95a95();
				goto IL_0248;
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
							cd1a446c7c13541a442f9e29567e95a95();
						}
					}
					break;
				}
				finally
				{
					c5449f5ffb2d2fc1652ba8252da9ab019();
				}
			}
		}

		void IDisposable.Dispose()
		{
			//ILSpy generated this explicit interface implementation from .override directive in c087ddb9300ad4d53672b7a60e6d76672
			this.c087ddb9300ad4d53672b7a60e6d76672();
		}

		private void c5449f5ffb2d2fc1652ba8252da9ab019()
		{
			c94f69126adc708dfc5f5fd5b63c52835 = -1;
			if (c6e99b7db15ab7fa02952ff58d5ff4111 != null)
			{
				c6e99b7db15ab7fa02952ff58d5ff4111.Dispose();
			}
		}

		private void cd1a446c7c13541a442f9e29567e95a95()
		{
			c94f69126adc708dfc5f5fd5b63c52835 = 1;
			if (c0309f8241d4ba516a85112f30073b7e3 == null)
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
					__ldtoken(BasePartition<>.cb73e993320d1305cf214094636482085.cd1a446c7c13541a442f9e29567e95a95());
				}
				c0309f8241d4ba516a85112f30073b7e3.Dispose();
				return;
			}
		}
	}

	private static readonly bool c11f11dfbe5a6995dac4a3303b1f04a3e;

	internal static readonly EqualityComparer<T> cbbc6d542116e0e180b1f11cadb4505d2;

	private bool cb7d5fd690c1958203743c742c4207f2f = true;

	private bool cd50322d58053efc3b6983abcc5a5ac4e = true;

	private bool c4a2b2c91672999c8ce25d9a21cbeec49;

	private HashSet<T> c9a7630cb36f902ce5e3bfef8e08a0885;

	private HashSet<T> c1d6d6cfb484f940e1f3a16dccc2cbbfd;

	private HashSet<T> c82dcf0bf8b882f1667e302d5bffa7b72;

	private Aabb c3a708fca2e00c0f5027ce857e9da5624;

	private IPairFilter<T> c7215839bcbb23f1ded1a6a9876755f7f;

	private Func<T, Aabb> c2978eaeaefa7ff754c7d1b58eb6f5187;

	private List<T> c650f95538a011c39773e3b9e0b147480;

	private HashSet<Pair<T>> cdb565a0d0e484a399660c47a4ce9d1cf;

	/// <summary>
	///       Gets the axis-aligned bounding box (AABB) that contains all items.
	///       </summary>
	public Aabb Aabb
	{
		get
		{
			Update(forceRebuild: false);
			return c3a708fca2e00c0f5027ce857e9da5624;
		}
		protected set
		{
			c3a708fca2e00c0f5027ce857e9da5624 = value;
		}
	}

	/// <summary>
	///       Gets the number of items contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.
	///       </summary>
	public int Count => c0e601bcddbab14811b8bae6ae4b7b274.Count;

	/// <summary>
	///       Gets or sets a value indicating whether self overlaps are computed.
	///       </summary>
	public bool EnableSelfOverlaps
	{
		get
		{
			return cdb565a0d0e484a399660c47a4ce9d1cf != null;
		}
		set
		{
			if (value)
			{
				if (cdb565a0d0e484a399660c47a4ce9d1cf == null)
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
						__ldtoken(BasePartition<>.set_EnableSelfOverlaps(bool));
					}
					cdb565a0d0e484a399660c47a4ce9d1cf = new HashSet<Pair<T>>();
					Invalidate();
					cd50322d58053efc3b6983abcc5a5ac4e = true;
				}
			}
			else
			{
				cdb565a0d0e484a399660c47a4ce9d1cf = null;
			}
			c0aa2b6671f7e92b9c129410d550f7b13();
		}
	}

	/// <summary>
	///       Gets or sets the filter that is used to filter overlaps of two items.
	///       </summary>
	public IPairFilter<T> Filter
	{
		get
		{
			return c7215839bcbb23f1ded1a6a9876755f7f;
		}
		set
		{
			if (c7215839bcbb23f1ded1a6a9876755f7f == value)
			{
				return;
			}
			if (c7215839bcbb23f1ded1a6a9876755f7f != null)
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
					__ldtoken(BasePartition<>.set_Filter(IPairFilter<T>));
				}
				c7215839bcbb23f1ded1a6a9876755f7f.Changed -= c7800892d6d855908c9d25556ad846a02;
			}
			c7215839bcbb23f1ded1a6a9876755f7f = value;
			c7800892d6d855908c9d25556ad846a02(this, EventArgs.Empty);
			c7800892d6d855908c9d25556ad846a02();
			if (c7215839bcbb23f1ded1a6a9876755f7f != null)
			{
				c7215839bcbb23f1ded1a6a9876755f7f.Changed += c7800892d6d855908c9d25556ad846a02;
			}
		}
	}

	/// <summary>
	///       Gets or sets the method that computes the <see cref="T:DigitalRune.Geometry.Shapes.Aabb" /> of an item.
	///       </summary>
	public Func<T, Aabb> GetAabbForItem
	{
		get
		{
			return c2978eaeaefa7ff754c7d1b58eb6f5187;
		}
		set
		{
			c2978eaeaefa7ff754c7d1b58eb6f5187 = value;
			ccb8f4732b3b508aae0aacb2b55c391cc();
		}
	}

	private bool c02aea6f65acc1ac2c5e3545134255a92 => false;

	internal List<T> c0e601bcddbab14811b8bae6ae4b7b274 => c650f95538a011c39773e3b9e0b147480;

	internal HashSet<Pair<T>> c65379642bd949dc7e978580b9649b9bc => cdb565a0d0e484a399660c47a4ce9d1cf;

	static BasePartition()
	{
		TypeInitializer.Initialize(ccb48cf63175e6724337dcebf74566228.ce9f9bc9ecd5ed400a711abaa33452c25);
		TypeInitializer.CreateInstance(12, ccb48cf63175e6724337dcebf74566228.ccda5e1cdd8faa90b24f6bc28c9af6b28);
		c11f11dfbe5a6995dac4a3303b1f04a3e = typeof(T).IsValueType;
		cbbc6d542116e0e180b1f11cadb4505d2 = EqualityComparer<T>.Default;
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Partitioning.BasePartition`1" /> class.
	///       </summary>
	protected BasePartition()
	{
		c650f95538a011c39773e3b9e0b147480 = new List<T>();
	}

	private object c48423685eed44c8f6f4708513cf05f2b()
	{
		return Clone();
	}

	object ICloneable.Clone()
	{
		//ILSpy generated this explicit interface implementation from .override directive in c48423685eed44c8f6f4708513cf05f2b
		return this.c48423685eed44c8f6f4708513cf05f2b();
	}

	/// <summary>
	///       Creates a new <see cref="T:DigitalRune.Geometry.Partitioning.BasePartition`1" /> that is a clone (deep copy) of the current
	///       instance.
	///       </summary>
	/// <returns>
	///       A new <see cref="T:DigitalRune.Geometry.GeometricObject" /> that is a clone (deep copy) of the current instance.
	///       </returns>
	public BasePartition<T> Clone()
	{
		BasePartition<T> basePartition = cec9b74d9244fb114116db0f3a6fbc279();
		basePartition.CloneCore(this);
		return basePartition;
	}

	private BasePartition<T> cec9b74d9244fb114116db0f3a6fbc279()
	{
		BasePartition<T> basePartition = CreateInstanceCore();
		if ((object)GetType() != basePartition.GetType())
		{
			while (true)
			{
				switch (2)
				{
				case 0:
					break;
				default:
				{
					if (1 == 0)
					{
						__ldtoken(BasePartition<>.cec9b74d9244fb114116db0f3a6fbc279());
					}
					string message = string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1), GetType());
					throw new InvalidOperationException(message);
				}
				}
			}
		}
		return basePartition;
	}

	/// <summary>
	///       When implemented in a derived class, creates a new instance of the 
	///       <see cref="T:DigitalRune.Geometry.Partitioning.BasePartition`1" /> derived class. 
	///       </summary>
	/// <returns>The new instance.</returns>
	protected abstract BasePartition<T> CreateInstanceCore();

	/// <summary>
	///       Makes the instance a clone (deep copy) of the specified <see cref="T:DigitalRune.Geometry.Partitioning.BasePartition`1" />.
	///       </summary>
	/// <param name="sourcePartition">The object to clone.</param>
	protected virtual void CloneCore(BasePartition<T> sourcePartition)
	{
		EnableSelfOverlaps = sourcePartition.EnableSelfOverlaps;
		Filter = sourcePartition.Filter;
		GetAabbForItem = sourcePartition.GetAabbForItem;
	}

	private static void c19552e58880375c19f9f067c10ae5eb9(ref HashSet<T> cb4d8415d192e6216e9129cc4adc2ec81)
	{
		if (cb4d8415d192e6216e9129cc4adc2ec81 != null)
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
			if (1 == 0)
			{
				__ldtoken(BasePartition<>.c19552e58880375c19f9f067c10ae5eb9(ref HashSet<T>));
			}
			cb4d8415d192e6216e9129cc4adc2ec81 = ResourcePools<T>.HashSets.Obtain();
			return;
		}
	}

	private static void c3ac53bb7bb015426cdd85f82e3623124(ref HashSet<T> cb4d8415d192e6216e9129cc4adc2ec81)
	{
		if (cb4d8415d192e6216e9129cc4adc2ec81 != null)
		{
			ResourcePools<T>.HashSets.Recycle(cb4d8415d192e6216e9129cc4adc2ec81);
			cb4d8415d192e6216e9129cc4adc2ec81 = null;
		}
	}

	private IEnumerator c63811cf6ff5c0b86c54fd5037c6af6ad()
	{
		return GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		//ILSpy generated this explicit interface implementation from .override directive in c63811cf6ff5c0b86c54fd5037c6af6ad
		return this.c63811cf6ff5c0b86c54fd5037c6af6ad();
	}

	private IEnumerator<T> c0843ff4e059c4450bad00d5ba0b6ef48()
	{
		return GetEnumerator();
	}

	IEnumerator<T> IEnumerable<T>.GetEnumerator()
	{
		//ILSpy generated this explicit interface implementation from .override directive in c0843ff4e059c4450bad00d5ba0b6ef48
		return this.c0843ff4e059c4450bad00d5ba0b6ef48();
	}

	/// <summary>
	///       Returns an enumerator that iterates through the collection.
	///       </summary>
	/// <returns>
	///       A enumerator that can be used to iterate through the collection.
	///       </returns>
	public List<T>.Enumerator GetEnumerator()
	{
		return c0e601bcddbab14811b8bae6ae4b7b274.GetEnumerator();
	}

	/// <summary>
	///       Adds an item to the <see cref="T:DigitalRune.Geometry.Partitioning.BasePartition`1" />.
	///       </summary>
	/// <param name="item">The object to add to the <see cref="T:DigitalRune.Geometry.Partitioning.BasePartition`1" />.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="item" /> is <see langword="null" />.
	///       </exception>
	public void Add(T item)
	{
		if (!c11f11dfbe5a6995dac4a3303b1f04a3e)
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
				__ldtoken(BasePartition<>.Add(T));
			}
			if (object.ReferenceEquals(item, null))
			{
				while (true)
				{
					switch (2)
					{
					case 0:
						break;
					default:
						throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(669));
					}
				}
			}
		}
		cb7d5fd690c1958203743c742c4207f2f = true;
		c0e601bcddbab14811b8bae6ae4b7b274.Add(item);
		if (c1d6d6cfb484f940e1f3a16dccc2cbbfd != null && c1d6d6cfb484f940e1f3a16dccc2cbbfd.Remove(item))
		{
			while (true)
			{
				switch (4)
				{
				case 0:
					break;
				default:
					if (!c4a2b2c91672999c8ce25d9a21cbeec49)
					{
						c19552e58880375c19f9f067c10ae5eb9(ref c82dcf0bf8b882f1667e302d5bffa7b72);
						c82dcf0bf8b882f1667e302d5bffa7b72.Add(item);
					}
					return;
				}
			}
		}
		c19552e58880375c19f9f067c10ae5eb9(ref c9a7630cb36f902ce5e3bfef8e08a0885);
		c9a7630cb36f902ce5e3bfef8e08a0885.Add(item);
	}

	/// <summary>
	///       Removes all items from the <see cref="T:DigitalRune.Geometry.Partitioning.BasePartition`1" />.
	///       </summary>
	public void Clear()
	{
		cb7d5fd690c1958203743c742c4207f2f = true;
		c3ac53bb7bb015426cdd85f82e3623124(ref c9a7630cb36f902ce5e3bfef8e08a0885);
		c19552e58880375c19f9f067c10ae5eb9(ref c1d6d6cfb484f940e1f3a16dccc2cbbfd);
		c1d6d6cfb484f940e1f3a16dccc2cbbfd.AddRange(c0e601bcddbab14811b8bae6ae4b7b274);
		c3ac53bb7bb015426cdd85f82e3623124(ref c82dcf0bf8b882f1667e302d5bffa7b72);
		c4a2b2c91672999c8ce25d9a21cbeec49 = false;
		c0e601bcddbab14811b8bae6ae4b7b274.Clear();
		if (c65379642bd949dc7e978580b9649b9bc != null)
		{
			c65379642bd949dc7e978580b9649b9bc.Clear();
		}
	}

	/// <summary>
	///       Determines whether the <see cref="T:DigitalRune.Geometry.Partitioning.BasePartition`1" /> contains a specific value.
	///       </summary>
	/// <param name="item">The object to locate in the <see cref="T:DigitalRune.Geometry.Partitioning.BasePartition`1" />.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="item" /> is found in the 
	///       <see cref="T:DigitalRune.Geometry.Partitioning.BasePartition`1" />; otherwise, <see langword="false" />.
	///       </returns>
	public bool Contains(T item)
	{
		return c0e601bcddbab14811b8bae6ae4b7b274.Contains(item);
	}

	/// <summary>
	///       Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1" /> to an <see cref="T:System.Array" />, starting 
	///       at a particular <see cref="T:System.Array" /> index.
	///       </summary>
	/// <param name="array">
	///       The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from 
	///       <see cref="T:System.Collections.Generic.ICollection`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.
	///       </param>
	/// <param name="arrayIndex">
	///       The zero-based index in <paramref name="array" /> at which copying begins.
	///       </param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="array" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="arrayIndex" /> is less than 0.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///       One of the following conditions:
	///       <list type="bullet"><item><description><paramref name="array" /> is multidimensional.
	///       </description></item><item><description><paramref name="arrayIndex" /> is equal to or greater than the length of 
	///       <paramref name="array" />.
	///       </description></item><item><description>
	///       The number of elements in the source <see cref="T:System.Collections.Generic.ICollection`1" /> is greater than the 
	///       available space from <paramref name="arrayIndex" /> to the end of the destination 
	///       <paramref name="array" />.
	///       </description></item><item><description>
	///       Type <typeparamref name="T" /> cannot be cast automatically to the type of the destination 
	///       <paramref name="array" />.
	///       </description></item></list></exception>
	public void CopyTo(T[] array, int arrayIndex)
	{
		c0e601bcddbab14811b8bae6ae4b7b274.CopyTo(array, arrayIndex);
	}

	/// <summary>
	///       Removes the first occurrence of a specific object from the <see cref="T:DigitalRune.Geometry.Partitioning.BasePartition`1" />.
	///       </summary>
	/// <param name="item">
	///       The object to remove from the <see cref="T:DigitalRune.Geometry.Partitioning.BasePartition`1" />.
	///       </param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="item" /> was successfully removed from the 
	///       <see cref="T:DigitalRune.Geometry.Partitioning.BasePartition`1" />; otherwise, <see langword="false" />. This method also 
	///       returns <see langword="false" /> if <paramref name="item" /> is not found in the original 
	///       <see cref="T:DigitalRune.Geometry.Partitioning.BasePartition`1" />.
	///       </returns>
	public bool Remove(T item)
	{
		if (!c0e601bcddbab14811b8bae6ae4b7b274.Remove(item))
		{
			return false;
		}
		if (c9a7630cb36f902ce5e3bfef8e08a0885 != null)
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
				__ldtoken(BasePartition<>.Remove(T));
			}
			if (c9a7630cb36f902ce5e3bfef8e08a0885.Remove(item))
			{
				while (true)
				{
					switch (1)
					{
					case 0:
						break;
					default:
						return true;
					}
				}
			}
		}
		cb7d5fd690c1958203743c742c4207f2f = true;
		if (c82dcf0bf8b882f1667e302d5bffa7b72 != null)
		{
			c82dcf0bf8b882f1667e302d5bffa7b72.Remove(item);
		}
		c19552e58880375c19f9f067c10ae5eb9(ref c1d6d6cfb484f940e1f3a16dccc2cbbfd);
		c1d6d6cfb484f940e1f3a16dccc2cbbfd.Add(item);
		return true;
	}

	internal bool c8092bfef08eaf2732b73394233ee5fd1(Pair<T> c3cdd6711e3e96d722a86b2bfd486c923)
	{
		if (cbbc6d542116e0e180b1f11cadb4505d2.Equals(c3cdd6711e3e96d722a86b2bfd486c923.First, c3cdd6711e3e96d722a86b2bfd486c923.Second))
		{
			return false;
		}
		if (Filter != null)
		{
			return Filter.Filter(c3cdd6711e3e96d722a86b2bfd486c923);
		}
		return true;
	}

	/// <summary>
	///       Invalidates the cached spatial information of all items in the spatial partition.
	///       </summary>
	public void Invalidate()
	{
		cb7d5fd690c1958203743c742c4207f2f = true;
		c3ac53bb7bb015426cdd85f82e3623124(ref c82dcf0bf8b882f1667e302d5bffa7b72);
		c4a2b2c91672999c8ce25d9a21cbeec49 = true;
	}

	/// <summary>
	///       Invalidates the cached spatial information of the specified item.
	///       </summary>
	/// <param name="item">The item that has moved or changed its shape.</param>
	public void Invalidate(T item)
	{
		cb7d5fd690c1958203743c742c4207f2f = true;
		if (c4a2b2c91672999c8ce25d9a21cbeec49)
		{
			return;
		}
		if (c9a7630cb36f902ce5e3bfef8e08a0885 != null)
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
				__ldtoken(BasePartition<>.Invalidate(T));
			}
			if (c9a7630cb36f902ce5e3bfef8e08a0885.Contains(item))
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
				break;
			}
		}
		c19552e58880375c19f9f067c10ae5eb9(ref c82dcf0bf8b882f1667e302d5bffa7b72);
		c82dcf0bf8b882f1667e302d5bffa7b72.Add(item);
	}

	/// <summary>
	///       Updates the internal structure of this <see cref="T:DigitalRune.Geometry.Partitioning.ISpatialPartition`1" />.
	///       </summary>
	/// <param name="forceRebuild">
	///       If set to <see langword="true" /> the internal structure will be rebuilt from scratch. If set 
	///       to <see langword="false" /> the spatial partition can decide to rebuild everything or refit 
	///       only the invalidated parts.
	///       </param>
	/// <exception cref="T:DigitalRune.Geometry.GeometryException">
	///       Cannot update spatial partition. The property <see cref="P:DigitalRune.Geometry.Partitioning.BasePartition`1.GetAabbForItem" /> is not set.
	///       </exception>
	public void Update(bool forceRebuild)
	{
		if (GetAabbForItem == null)
		{
			throw new GeometryException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(678));
		}
		if (!forceRebuild && !cb7d5fd690c1958203743c742c4207f2f)
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
				__ldtoken(BasePartition<>.Update(bool));
			}
			if (!cd50322d58053efc3b6983abcc5a5ac4e)
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
				break;
			}
		}
		bool ce8ca0a645441ac556aaf6b4574dbbb1f = forceRebuild || cd50322d58053efc3b6983abcc5a5ac4e;
		cb7d5fd690c1958203743c742c4207f2f = false;
		cd50322d58053efc3b6983abcc5a5ac4e = false;
		c3a708fca2e00c0f5027ce857e9da5624 = new Aabb(Vector3F.One, Vector3F.Zero);
		c19552e58880375c19f9f067c10ae5eb9(ref c9a7630cb36f902ce5e3bfef8e08a0885);
		c19552e58880375c19f9f067c10ae5eb9(ref c1d6d6cfb484f940e1f3a16dccc2cbbfd);
		HashSet<T> c7a74c15b192971583f3f676b;
		if (c4a2b2c91672999c8ce25d9a21cbeec49)
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
			c7a74c15b192971583f3f676b = null;
		}
		else
		{
			c19552e58880375c19f9f067c10ae5eb9(ref c82dcf0bf8b882f1667e302d5bffa7b72);
			c7a74c15b192971583f3f676b = c82dcf0bf8b882f1667e302d5bffa7b72;
		}
		ced5f5b0bd8302f3e8ade582954f3650d(ce8ca0a645441ac556aaf6b4574dbbb1f, c9a7630cb36f902ce5e3bfef8e08a0885, c1d6d6cfb484f940e1f3a16dccc2cbbfd, c7a74c15b192971583f3f676b);
		c3ac53bb7bb015426cdd85f82e3623124(ref c9a7630cb36f902ce5e3bfef8e08a0885);
		c3ac53bb7bb015426cdd85f82e3623124(ref c1d6d6cfb484f940e1f3a16dccc2cbbfd);
		c3ac53bb7bb015426cdd85f82e3623124(ref c82dcf0bf8b882f1667e302d5bffa7b72);
		c4a2b2c91672999c8ce25d9a21cbeec49 = false;
		if (!(c3a708fca2e00c0f5027ce857e9da5624.Minimum.X > c3a708fca2e00c0f5027ce857e9da5624.Maximum.X))
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
			c527912fe7c25ec4be62695147b7edc1d();
			return;
		}
	}

	private void c527912fe7c25ec4be62695147b7edc1d()
	{
		int count = c0e601bcddbab14811b8bae6ae4b7b274.Count;
		if (count == 0)
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
						__ldtoken(BasePartition<>.c527912fe7c25ec4be62695147b7edc1d());
					}
					Aabb = default;
					return;
				}
			}
		}
		Aabb aabb = GetAabbForItem(c0e601bcddbab14811b8bae6ae4b7b274[0]);
		for (int i = 1; i < count; i++)
		{
			T arg = c0e601bcddbab14811b8bae6ae4b7b274[i];
			aabb.Grow(GetAabbForItem(arg));
		}
		while (true)
		{
			switch (2)
			{
			case 0:
				continue;
			}
			Aabb = aabb;
			return;
		}
	}

	private void c7800892d6d855908c9d25556ad846a02(object c807f682152464d11dde83a885bf87124, EventArgs c7fefeb91d4632901bd52a1f245b70db1)
	{
		Invalidate();
		cd50322d58053efc3b6983abcc5a5ac4e = true;
	}

	internal virtual void c7800892d6d855908c9d25556ad846a02()
	{
	}

	internal virtual void c0aa2b6671f7e92b9c129410d550f7b13()
	{
	}

	internal virtual void ccb8f4732b3b508aae0aacb2b55c391cc()
	{
	}

	internal abstract void ced5f5b0bd8302f3e8ade582954f3650d(bool ce8ca0a645441ac556aaf6b4574dbbb1f, HashSet<T> c049085d0fafa74bddedd7455b511e4d2, HashSet<T> c2176039f062c6700652655f4b1223258, HashSet<T> c7a74c15b192971583f3f676b15968063);

	/// <summary>
	///       Gets the items that touch the given AABB.
	///       </summary>
	/// <param name="aabb">The axis-aligned bounding box.</param>
	/// <returns>All items that touch the given AABB.</returns>
	public abstract IEnumerable<T> GetOverlaps(Aabb aabb);

	/// <summary>
	///       Gets the items that touch the given item.
	///       </summary>
	/// <param name="item">
	///       The item. (Whether the given item must be a part of the spatial partition or whether it can
	///       be an external object depends on the <see cref="P:DigitalRune.Geometry.Partitioning.BasePartition`1.GetAabbForItem" /> callback. The 
	///       <see cref="P:DigitalRune.Geometry.Partitioning.BasePartition`1.GetAabbForItem" /> must be able to compute the AABB for the given item.)
	///       </param>
	/// <returns>All items that touch the given item.</returns>
	public IEnumerable<T> GetOverlaps(T item)
	{
		Aabb aabb = GetAabbForItem(item);
		IEnumerator<T> enumerator = GetOverlaps(aabb).GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				T current = enumerator.Current;
				if (!c8092bfef08eaf2732b73394233ee5fd1(new Pair<T>(current, item)))
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
				if (1 == 0)
				{
					__ldtoken(BasePartition<>.cc9a4ac9c6f0bf2531abb1d59b82c3c2f.MoveNext());
				}
				yield return current;
			}
			while (true)
			{
				switch (5)
				{
				case 0:
					break;
				default:
					yield break;
				}
			}
		}
		finally
		{
			if (enumerator != null)
			{
				while (true)
				{
					switch (4)
					{
					case 0:
						continue;
					}
					if (1 == 0)
					{
						__ldtoken(BasePartition<>.cc9a4ac9c6f0bf2531abb1d59b82c3c2f.c6b2b7bb0138c7a754f602e1325ab5a3a());
					}
					enumerator.Dispose();
					break;
				}
			}
		}
	}

	/// <summary>
	///       Gets the items that touch the given ray.
	///       </summary>
	/// <param name="ray">The ray.</param>
	/// <returns>All items that are hit by the ray.</returns>
	public virtual IEnumerable<T> GetOverlaps(Ray ray)
	{
		Aabb aabb = new Aabb(ray.Origin, ray.Origin);
		aabb.Grow(ray.Origin + ray.Direction * ray.Length);
		Vector3F c3ec982f192efbba86e42a1cea5c = new Vector3F(1f / ray.Direction.X, 1f / ray.Direction.Y, 1f / ray.Direction.Z);
		float c912da9d2d82e2a6523be60260abdfc = Numeric.EpsilonF * (1f + Aabb.Extent.Length);
		foreach (T overlap in GetOverlaps(aabb))
		{
			if (!GeometryHelper.cdd6d17271fec83883494ba8e43eede6a(GetAabbForItem(overlap), ray.Origin, c3ec982f192efbba86e42a1cea5c, ray.Length, c912da9d2d82e2a6523be60260abdfc))
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
			if (1 == 0)
			{
				__ldtoken(BasePartition<>.c7c8d62f8ffafa6384df8cf453b17950c.MoveNext());
			}
			yield return overlap;
		}
	}

	/// <summary>
	///       Gets overlaps of all items contained in this spatial partition.
	///       </summary>
	/// <returns>
	///       All pairs of overlapping items of this spatial partition.
	///       </returns>
	/// <exception cref="T:DigitalRune.Geometry.GeometryException">
	///   <see cref="P:DigitalRune.Geometry.Partitioning.BasePartition`1.EnableSelfOverlaps" /> is <see langword="false" />.
	///       </exception>
	public IEnumerable<Pair<T>> GetOverlaps()
	{
		if (!EnableSelfOverlaps)
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
						__ldtoken(BasePartition<>.GetOverlaps());
					}
					throw new GeometryException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(856));
				}
			}
		}
		Update(forceRebuild: false);
		return c65379642bd949dc7e978580b9649b9bc;
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
	public virtual IEnumerable<Pair<T>> GetOverlaps(ISpatialPartition<T> otherPartition)
	{
		if (otherPartition == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(994));
		}
		otherPartition.Update(forceRebuild: false);
		Update(forceRebuild: false);
		IEnumerable<T> overlaps = GetOverlaps(otherPartition.Aabb);
		using IEnumerator<T> enumerator = overlaps.GetEnumerator();
		while (enumerator.MoveNext())
		{
			T current = enumerator.Current;
			Aabb aabb = GetAabbForItem(current);
			IEnumerable<T> overlaps2 = otherPartition.GetOverlaps(aabb);
			using IEnumerator<T> enumerator2 = overlaps2.GetEnumerator();
			while (enumerator2.MoveNext())
			{
				T current2 = enumerator2.Current;
				Pair<T> pair = new Pair<T>(current, current2);
				if (Filter != null)
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
					if (!Filter.Filter(pair))
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
				}
				yield return pair;
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
					__ldtoken(BasePartition<>.c9eebdaf74eb20a22ef45e392585ef96c.MoveNext());
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
		while (true)
		{
			switch (1)
			{
			case 0:
				break;
			default:
				yield break;
			}
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
	public virtual IEnumerable<Pair<T>> GetOverlaps(Vector3F scale, Pose pose, ISpatialPartition<T> otherPartition, Vector3F otherScale, Pose otherPose)
	{
		if (otherPartition == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(994));
		}
		otherPartition.Update(forceRebuild: false);
		Update(forceRebuild: false);
		Vector3F scale2 = Vector3F.One / scale;
		Vector3F scale3 = Vector3F.One / otherScale;
		Pose pose2 = pose.Inverse * otherPose;
		Pose inverse = pose2.Inverse;
		Aabb aabb = otherPartition.Aabb.GetAabb(otherScale, pose2);
		aabb.Scale(scale2);
		foreach (T overlap in GetOverlaps(aabb))
		{
			Aabb aabb2 = GetAabbForItem(overlap).GetAabb(scale, inverse);
			aabb2.Scale(scale3);
			IEnumerator<T> enumerator2 = otherPartition.GetOverlaps(aabb2).GetEnumerator();
			try
			{
				while (enumerator2.MoveNext())
				{
					T current2 = enumerator2.Current;
					Pair<T> pair = new Pair<T>(overlap, current2);
					if (Filter != null)
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
						if (!Filter.Filter(pair))
						{
							continue;
						}
					}
					yield return pair;
				}
			}
			finally
			{
				if (enumerator2 != null)
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
							__ldtoken(BasePartition<>.cb73e993320d1305cf214094636482085.cd1a446c7c13541a442f9e29567e95a95());
						}
						enumerator2.Dispose();
						break;
					}
				}
			}
		}
	}
}
