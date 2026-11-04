using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using A;
using DigitalRune.Collections;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics;
using DigitalRune.Mathematics.Algebra;

namespace DigitalRune.Geometry.Partitioning;

/// <summary>
///       Represents a compressed and optimized bounding volume tree using axis-aligned bounding boxes 
///       (AABBs).
///       </summary>
public sealed class CompressedAabbTree : ICollection<int>, IEnumerable<int>, IEnumerable, ISpatialPartition<int>, ISupportClosestPointQueries<int>, ICloneable
{
	[StructLayout(LayoutKind.Explicit)]
	internal struct c3be4e279c85d04da07f3693866750bf4
	{
		[FieldOffset(0)]
		public ushort c0766ee351495379577e4bd426a93fd76;

		[FieldOffset(2)]
		public ushort ca908b2ef855c3e51cf79eb5717ba52da;

		[FieldOffset(4)]
		public ushort c2d1089e3916028efbe65eebdee0edea4;

		[FieldOffset(6)]
		public ushort ca496a3c2e4033c7383700937a074c8be;

		[FieldOffset(8)]
		public ushort c7d2a56de6e39281750b1f1c7a61a8868;

		[FieldOffset(10)]
		public ushort c95b253ba97348b0005f6ccc969f9c2e3;

		[FieldOffset(12)]
		internal int c69b0e87934525495e9ec68864d16eee1;

		public bool cfd73e16b19bb8bde65db991a787eb5ad => c69b0e87934525495e9ec68864d16eee1 >= 0;

		public int c652f269a40a2519b8658ffbade581b4e
		{
			get
			{
				return c69b0e87934525495e9ec68864d16eee1;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1040), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5906));
				}
				c69b0e87934525495e9ec68864d16eee1 = value;
			}
		}

		public int cfb8d7112d33accdca0964eca99e29475
		{
			get
			{
				return -c69b0e87934525495e9ec68864d16eee1;
			}
			set
			{
				c69b0e87934525495e9ec68864d16eee1 = -value;
			}
		}
	}

	[Flags]
	internal enum cf8d321a387e439baf196dca5797d9576
	{
		c1d44713bc4a88b22856928fb23e46d3f = 0,
		ce759ff2781e7c0601193b90d63f12c3c = 1,
		c5fd1b383396ba2ccedfa66b0f2eb1382 = 3
	}

	[CompilerGenerated]
	private sealed class c030c6eafad971e0859214358e6b9a580 : IEnumerable<int>, IEnumerator<int>, IEnumerable, IEnumerator, IDisposable
	{
		private int cd179d7f18da070d164af6afcb0af45b5;

		private int c94f69126adc708dfc5f5fd5b63c52835;

		private int c0e8a058e779916e7715d7616fb0da9e7;

		public CompressedAabbTree c1b0bbfd4af1b6a4c0c71a5dd3c509812;

		public Aabb cdc3439410d15f28489c964ee87b88fe9;

		public Aabb cc3ae004bdecc8a5e2b7f6f0efd06d1d5;

		public int c6dd72a3c27a2c1b9239f5bf86bbbc379;

		public c3be4e279c85d04da07f3693866750bf4 c87f28cfb1146d5c5d9f2c3f816d686ce;

		public bool c3405f551881f666b3ccd42755ec4d7b7;

		int IEnumerator<int>.Current
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
		public c030c6eafad971e0859214358e6b9a580(int c94f69126adc708dfc5f5fd5b63c52835)
		{
			this.c94f69126adc708dfc5f5fd5b63c52835 = c94f69126adc708dfc5f5fd5b63c52835;
			c0e8a058e779916e7715d7616fb0da9e7 = Thread.CurrentThread.ManagedThreadId;
		}

		[DebuggerHidden]
		private IEnumerator<int> c58760474dcc19e491557b79568629eb2()
		{
			c030c6eafad971e0859214358e6b9a580 c030c6eafad971e0859214358e6b9a581;
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
					__ldtoken(c030c6eafad971e0859214358e6b9a580.c58760474dcc19e491557b79568629eb2());
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
					c030c6eafad971e0859214358e6b9a581 = this;
					goto IL_005b;
				}
			}
			c030c6eafad971e0859214358e6b9a581 = new c030c6eafad971e0859214358e6b9a580(0)
			{
				c1b0bbfd4af1b6a4c0c71a5dd3c509812 = c1b0bbfd4af1b6a4c0c71a5dd3c509812
			};
			goto IL_005b;
			IL_005b:
			c030c6eafad971e0859214358e6b9a581.cdc3439410d15f28489c964ee87b88fe9 = cc3ae004bdecc8a5e2b7f6f0efd06d1d5;
			return c030c6eafad971e0859214358e6b9a581;
		}

		IEnumerator<int> IEnumerable<int>.GetEnumerator()
		{
			//ILSpy generated this explicit interface implementation from .override directive in c58760474dcc19e491557b79568629eb2
			return this.c58760474dcc19e491557b79568629eb2();
		}

		[DebuggerHidden]
		private IEnumerator c63811cf6ff5c0b86c54fd5037c6af6ad()
		{
			return c58760474dcc19e491557b79568629eb2();
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
				if (c1b0bbfd4af1b6a4c0c71a5dd3c509812.ccc439410c5678c542a116b1c7b4551b8 == 0)
				{
					break;
				}
				c6dd72a3c27a2c1b9239f5bf86bbbc379 = 0;
				goto IL_012c;
			case 1:
				{
					c94f69126adc708dfc5f5fd5b63c52835 = -1;
					goto IL_00db;
				}
				IL_00db:
				if (!c3405f551881f666b3ccd42755ec4d7b7)
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
					if (!c87f28cfb1146d5c5d9f2c3f816d686ce.cfd73e16b19bb8bde65db991a787eb5ad)
					{
						c6dd72a3c27a2c1b9239f5bf86bbbc379 += c87f28cfb1146d5c5d9f2c3f816d686ce.cfb8d7112d33accdca0964eca99e29475;
						goto IL_012c;
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
				}
				c6dd72a3c27a2c1b9239f5bf86bbbc379++;
				goto IL_012c;
				IL_012c:
				if (c6dd72a3c27a2c1b9239f5bf86bbbc379 >= c1b0bbfd4af1b6a4c0c71a5dd3c509812.cd26c0d77f8d7f857646d6821d2cb60a7.Length)
				{
					break;
				}
				c87f28cfb1146d5c5d9f2c3f816d686ce = c1b0bbfd4af1b6a4c0c71a5dd3c509812.cd26c0d77f8d7f857646d6821d2cb60a7[c6dd72a3c27a2c1b9239f5bf86bbbc379];
				c3405f551881f666b3ccd42755ec4d7b7 = GeometryHelper.HaveContact(c1b0bbfd4af1b6a4c0c71a5dd3c509812.cccf609c8efd00ebcc2b1a12189fe5e64(c87f28cfb1146d5c5d9f2c3f816d686ce), cdc3439410d15f28489c964ee87b88fe9);
				if (c3405f551881f666b3ccd42755ec4d7b7)
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
						__ldtoken(c030c6eafad971e0859214358e6b9a580.MoveNext());
					}
					if (c87f28cfb1146d5c5d9f2c3f816d686ce.cfd73e16b19bb8bde65db991a787eb5ad)
					{
						cd179d7f18da070d164af6afcb0af45b5 = c87f28cfb1146d5c5d9f2c3f816d686ce.c652f269a40a2519b8658ffbade581b4e;
						c94f69126adc708dfc5f5fd5b63c52835 = 1;
						return true;
					}
				}
				goto IL_00db;
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
	private sealed class cb6b9209f7873d79f4a597241616e8a30 : IEnumerable<c3be4e279c85d04da07f3693866750bf4>, IEnumerator<c3be4e279c85d04da07f3693866750bf4>, IEnumerable, IEnumerator, IDisposable
	{
		private c3be4e279c85d04da07f3693866750bf4 cd179d7f18da070d164af6afcb0af45b5;

		private int c94f69126adc708dfc5f5fd5b63c52835;

		private int c0e8a058e779916e7715d7616fb0da9e7;

		public CompressedAabbTree c1b0bbfd4af1b6a4c0c71a5dd3c509812;

		public Aabb cdc3439410d15f28489c964ee87b88fe9;

		public Aabb cc3ae004bdecc8a5e2b7f6f0efd06d1d5;

		public int ca6d679a85551032647e6960223cc9961;

		public c3be4e279c85d04da07f3693866750bf4 c33881c886cd7d3593cff18c7ff404a8a;

		public bool c3c68d405597c7af7130d53b97292d718;

		c3be4e279c85d04da07f3693866750bf4 IEnumerator<c3be4e279c85d04da07f3693866750bf4>.Current
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
		public cb6b9209f7873d79f4a597241616e8a30(int c94f69126adc708dfc5f5fd5b63c52835)
		{
			this.c94f69126adc708dfc5f5fd5b63c52835 = c94f69126adc708dfc5f5fd5b63c52835;
			c0e8a058e779916e7715d7616fb0da9e7 = Thread.CurrentThread.ManagedThreadId;
		}

		[DebuggerHidden]
		private IEnumerator<c3be4e279c85d04da07f3693866750bf4> c32557cc828f953a6f5368eed9df546b0()
		{
			cb6b9209f7873d79f4a597241616e8a30 cb6b9209f7873d79f4a597241616e8a31;
			if (Thread.CurrentThread.ManagedThreadId == c0e8a058e779916e7715d7616fb0da9e7 && c94f69126adc708dfc5f5fd5b63c52835 == -2)
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
					__ldtoken(cb6b9209f7873d79f4a597241616e8a30.c32557cc828f953a6f5368eed9df546b0());
				}
				c94f69126adc708dfc5f5fd5b63c52835 = 0;
				cb6b9209f7873d79f4a597241616e8a31 = this;
			}
			else
			{
				cb6b9209f7873d79f4a597241616e8a31 = new cb6b9209f7873d79f4a597241616e8a30(0)
				{
					c1b0bbfd4af1b6a4c0c71a5dd3c509812 = c1b0bbfd4af1b6a4c0c71a5dd3c509812
				};
			}
			cb6b9209f7873d79f4a597241616e8a31.cdc3439410d15f28489c964ee87b88fe9 = cc3ae004bdecc8a5e2b7f6f0efd06d1d5;
			return cb6b9209f7873d79f4a597241616e8a31;
		}

		IEnumerator<c3be4e279c85d04da07f3693866750bf4> IEnumerable<c3be4e279c85d04da07f3693866750bf4>.GetEnumerator()
		{
			//ILSpy generated this explicit interface implementation from .override directive in c32557cc828f953a6f5368eed9df546b0
			return this.c32557cc828f953a6f5368eed9df546b0();
		}

		[DebuggerHidden]
		private IEnumerator c63811cf6ff5c0b86c54fd5037c6af6ad()
		{
			return c32557cc828f953a6f5368eed9df546b0();
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
				if (c1b0bbfd4af1b6a4c0c71a5dd3c509812.ccc439410c5678c542a116b1c7b4551b8 == 0)
				{
					break;
				}
				ca6d679a85551032647e6960223cc9961 = 0;
				goto IL_011d;
			case 1:
				{
					c94f69126adc708dfc5f5fd5b63c52835 = -1;
					goto IL_00d2;
				}
				IL_00d2:
				if (!c3c68d405597c7af7130d53b97292d718)
				{
					if (!c33881c886cd7d3593cff18c7ff404a8a.cfd73e16b19bb8bde65db991a787eb5ad)
					{
						ca6d679a85551032647e6960223cc9961 += c33881c886cd7d3593cff18c7ff404a8a.cfb8d7112d33accdca0964eca99e29475;
						goto IL_011d;
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
				ca6d679a85551032647e6960223cc9961++;
				goto IL_011d;
				IL_011d:
				if (ca6d679a85551032647e6960223cc9961 < c1b0bbfd4af1b6a4c0c71a5dd3c509812.cd26c0d77f8d7f857646d6821d2cb60a7.Length)
				{
					c33881c886cd7d3593cff18c7ff404a8a = c1b0bbfd4af1b6a4c0c71a5dd3c509812.cd26c0d77f8d7f857646d6821d2cb60a7[ca6d679a85551032647e6960223cc9961];
					c3c68d405597c7af7130d53b97292d718 = GeometryHelper.HaveContact(c1b0bbfd4af1b6a4c0c71a5dd3c509812.cccf609c8efd00ebcc2b1a12189fe5e64(c33881c886cd7d3593cff18c7ff404a8a), cdc3439410d15f28489c964ee87b88fe9);
					if (c3c68d405597c7af7130d53b97292d718 && c33881c886cd7d3593cff18c7ff404a8a.cfd73e16b19bb8bde65db991a787eb5ad)
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
									__ldtoken(cb6b9209f7873d79f4a597241616e8a30.MoveNext());
								}
								cd179d7f18da070d164af6afcb0af45b5 = c33881c886cd7d3593cff18c7ff404a8a;
								c94f69126adc708dfc5f5fd5b63c52835 = 1;
								return true;
							}
						}
					}
					goto IL_00d2;
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
	private sealed class c15e4f34eee62f863844c7270d24c7b66 : IEnumerable<int>, IEnumerator<int>, IEnumerable, IEnumerator, IDisposable
	{
		private int cd179d7f18da070d164af6afcb0af45b5;

		private int c94f69126adc708dfc5f5fd5b63c52835;

		private int c0e8a058e779916e7715d7616fb0da9e7;

		public CompressedAabbTree c1b0bbfd4af1b6a4c0c71a5dd3c509812;

		public Ray c2d769fcfe2c7e7844e7de692907680c8;

		public Ray c8b1773219813f762be9a1bd49ee5cb56;

		public Vector3F cdd7d15a2f2586a83176eab98cd5da02d;

		public float c19de92f3cad030023133a029f1b216f4;

		public int cf08bff862c39f85c81965b6dd1e076d1;

		public c3be4e279c85d04da07f3693866750bf4 cbb954ae8212e534739edbdff18ff9257;

		public bool c0a2756227bb189ae9a3dc097bd4b1b39;

		int IEnumerator<int>.Current
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
		public c15e4f34eee62f863844c7270d24c7b66(int c94f69126adc708dfc5f5fd5b63c52835)
		{
			this.c94f69126adc708dfc5f5fd5b63c52835 = c94f69126adc708dfc5f5fd5b63c52835;
			c0e8a058e779916e7715d7616fb0da9e7 = Thread.CurrentThread.ManagedThreadId;
		}

		[DebuggerHidden]
		private IEnumerator<int> c58760474dcc19e491557b79568629eb2()
		{
			c15e4f34eee62f863844c7270d24c7b66 c15e4f34eee62f863844c7270d24c7b67;
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
					__ldtoken(c15e4f34eee62f863844c7270d24c7b66.c58760474dcc19e491557b79568629eb2());
				}
				if (c94f69126adc708dfc5f5fd5b63c52835 == -2)
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
					c94f69126adc708dfc5f5fd5b63c52835 = 0;
					c15e4f34eee62f863844c7270d24c7b67 = this;
					goto IL_0059;
				}
			}
			c15e4f34eee62f863844c7270d24c7b67 = new c15e4f34eee62f863844c7270d24c7b66(0)
			{
				c1b0bbfd4af1b6a4c0c71a5dd3c509812 = c1b0bbfd4af1b6a4c0c71a5dd3c509812
			};
			goto IL_0059;
			IL_0059:
			c15e4f34eee62f863844c7270d24c7b67.c2d769fcfe2c7e7844e7de692907680c8 = c8b1773219813f762be9a1bd49ee5cb56;
			return c15e4f34eee62f863844c7270d24c7b67;
		}

		IEnumerator<int> IEnumerable<int>.GetEnumerator()
		{
			//ILSpy generated this explicit interface implementation from .override directive in c58760474dcc19e491557b79568629eb2
			return this.c58760474dcc19e491557b79568629eb2();
		}

		[DebuggerHidden]
		private IEnumerator c63811cf6ff5c0b86c54fd5037c6af6ad()
		{
			return c58760474dcc19e491557b79568629eb2();
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
				if (c1b0bbfd4af1b6a4c0c71a5dd3c509812.ccc439410c5678c542a116b1c7b4551b8 == 0)
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
					__ldtoken(c15e4f34eee62f863844c7270d24c7b66.MoveNext());
				}
				ref Vector3F reference = ref cdd7d15a2f2586a83176eab98cd5da02d;
				reference = new Vector3F(1f / c2d769fcfe2c7e7844e7de692907680c8.Direction.X, 1f / c2d769fcfe2c7e7844e7de692907680c8.Direction.Y, 1f / c2d769fcfe2c7e7844e7de692907680c8.Direction.Z);
				c19de92f3cad030023133a029f1b216f4 = Numeric.EpsilonF * (1f + c1b0bbfd4af1b6a4c0c71a5dd3c509812.Aabb.Extent.Length);
				cf08bff862c39f85c81965b6dd1e076d1 = 0;
				goto IL_01c3;
			}
			case 1:
				{
					c94f69126adc708dfc5f5fd5b63c52835 = -1;
					goto IL_017c;
				}
				IL_017c:
				if (!c0a2756227bb189ae9a3dc097bd4b1b39)
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
					if (!cbb954ae8212e534739edbdff18ff9257.cfd73e16b19bb8bde65db991a787eb5ad)
					{
						cf08bff862c39f85c81965b6dd1e076d1 += cbb954ae8212e534739edbdff18ff9257.cfb8d7112d33accdca0964eca99e29475;
						goto IL_01c3;
					}
				}
				cf08bff862c39f85c81965b6dd1e076d1++;
				goto IL_01c3;
				IL_01c3:
				if (cf08bff862c39f85c81965b6dd1e076d1 < c1b0bbfd4af1b6a4c0c71a5dd3c509812.cd26c0d77f8d7f857646d6821d2cb60a7.Length)
				{
					cbb954ae8212e534739edbdff18ff9257 = c1b0bbfd4af1b6a4c0c71a5dd3c509812.cd26c0d77f8d7f857646d6821d2cb60a7[cf08bff862c39f85c81965b6dd1e076d1];
					c0a2756227bb189ae9a3dc097bd4b1b39 = GeometryHelper.cdd6d17271fec83883494ba8e43eede6a(c1b0bbfd4af1b6a4c0c71a5dd3c509812.cccf609c8efd00ebcc2b1a12189fe5e64(cbb954ae8212e534739edbdff18ff9257), c2d769fcfe2c7e7844e7de692907680c8.Origin, cdd7d15a2f2586a83176eab98cd5da02d, c2d769fcfe2c7e7844e7de692907680c8.Length, c19de92f3cad030023133a029f1b216f4);
					if (c0a2756227bb189ae9a3dc097bd4b1b39 && cbb954ae8212e534739edbdff18ff9257.cfd73e16b19bb8bde65db991a787eb5ad)
					{
						cd179d7f18da070d164af6afcb0af45b5 = cbb954ae8212e534739edbdff18ff9257.c652f269a40a2519b8658ffbade581b4e;
						c94f69126adc708dfc5f5fd5b63c52835 = 1;
						return true;
					}
					goto IL_017c;
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
	private sealed class ca7984d98487a225a157577646eaa88da : IEnumerable<Pair<int>>, IEnumerator<Pair<int>>, IEnumerable, IEnumerator, IDisposable
	{
		private Pair<int> cd179d7f18da070d164af6afcb0af45b5;

		private int c94f69126adc708dfc5f5fd5b63c52835;

		private int c0e8a058e779916e7715d7616fb0da9e7;

		public CompressedAabbTree c1b0bbfd4af1b6a4c0c71a5dd3c509812;

		public ISpatialPartition<int> cfd8883387afb9cc25a2ac9150de0f66b;

		public ISpatialPartition<int> cd1939b65df62ed5f7a2112466dabd8f0;

		public c3be4e279c85d04da07f3693866750bf4 c61d675ea8e36dfac16b2e13340d49c75;

		public IEnumerable<int> c6e3eed106e9586cf0840f5cea38ebde9;

		public int ccd258f830c64cbe30dd4d1b88129e5ff;

		public Pair<int> cf417c4305592e9b47dc903871a89a447;

		public IEnumerator<c3be4e279c85d04da07f3693866750bf4> c3949eec112dd76879c9c7d89e229af84;

		public IEnumerator<int> c005cfd48fcd7a06a9b3f9ff85949766a;

		Pair<int> IEnumerator<Pair<int>>.Current
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
		public ca7984d98487a225a157577646eaa88da(int c94f69126adc708dfc5f5fd5b63c52835)
		{
			this.c94f69126adc708dfc5f5fd5b63c52835 = c94f69126adc708dfc5f5fd5b63c52835;
			c0e8a058e779916e7715d7616fb0da9e7 = Thread.CurrentThread.ManagedThreadId;
		}

		[DebuggerHidden]
		private IEnumerator<Pair<int>> c3dccb34b8e6239e768a1be590aa08632()
		{
			ca7984d98487a225a157577646eaa88da ca7984d98487a225a157577646eaa88da2;
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
					__ldtoken(ca7984d98487a225a157577646eaa88da.c3dccb34b8e6239e768a1be590aa08632());
				}
				if (c94f69126adc708dfc5f5fd5b63c52835 == -2)
				{
					c94f69126adc708dfc5f5fd5b63c52835 = 0;
					ca7984d98487a225a157577646eaa88da2 = this;
					goto IL_004d;
				}
			}
			ca7984d98487a225a157577646eaa88da2 = new ca7984d98487a225a157577646eaa88da(0)
			{
				c1b0bbfd4af1b6a4c0c71a5dd3c509812 = c1b0bbfd4af1b6a4c0c71a5dd3c509812
			};
			goto IL_004d;
			IL_004d:
			ca7984d98487a225a157577646eaa88da2.cfd8883387afb9cc25a2ac9150de0f66b = cd1939b65df62ed5f7a2112466dabd8f0;
			return ca7984d98487a225a157577646eaa88da2;
		}

		IEnumerator<Pair<int>> IEnumerable<Pair<int>>.GetEnumerator()
		{
			//ILSpy generated this explicit interface implementation from .override directive in c3dccb34b8e6239e768a1be590aa08632
			return this.c3dccb34b8e6239e768a1be590aa08632();
		}

		[DebuggerHidden]
		private IEnumerator c63811cf6ff5c0b86c54fd5037c6af6ad()
		{
			return c3dccb34b8e6239e768a1be590aa08632();
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
							switch (2)
							{
							case 0:
								continue;
							}
							break;
						}
						if (1 == 0)
						{
							__ldtoken(ca7984d98487a225a157577646eaa88da.MoveNext());
						}
						goto IL_01a4;
					}
					c94f69126adc708dfc5f5fd5b63c52835 = 2;
					goto IL_016c;
				}
				c94f69126adc708dfc5f5fd5b63c52835 = -1;
				if (cfd8883387afb9cc25a2ac9150de0f66b == null)
				{
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(994));
				}
				cfd8883387afb9cc25a2ac9150de0f66b.Update(forceRebuild: false);
				c1b0bbfd4af1b6a4c0c71a5dd3c509812.Update(forceRebuild: false);
				c3949eec112dd76879c9c7d89e229af84 = c1b0bbfd4af1b6a4c0c71a5dd3c509812.c4d76d858205a75f799980f42e41cbe5b(cfd8883387afb9cc25a2ac9150de0f66b.Aabb).GetEnumerator();
				c94f69126adc708dfc5f5fd5b63c52835 = 1;
				goto IL_018e;
				IL_01a4:
				return false;
				IL_018e:
				if (c3949eec112dd76879c9c7d89e229af84.MoveNext())
				{
					c61d675ea8e36dfac16b2e13340d49c75 = c3949eec112dd76879c9c7d89e229af84.Current;
					c6e3eed106e9586cf0840f5cea38ebde9 = cfd8883387afb9cc25a2ac9150de0f66b.GetOverlaps(c1b0bbfd4af1b6a4c0c71a5dd3c509812.cccf609c8efd00ebcc2b1a12189fe5e64(c61d675ea8e36dfac16b2e13340d49c75));
					c005cfd48fcd7a06a9b3f9ff85949766a = c6e3eed106e9586cf0840f5cea38ebde9.GetEnumerator();
					c94f69126adc708dfc5f5fd5b63c52835 = 2;
					goto IL_016c;
				}
				cf5ed08492d286a4f4f2dc83362895f5f();
				goto IL_01a4;
				IL_016c:
				while (c005cfd48fcd7a06a9b3f9ff85949766a.MoveNext())
				{
					ccd258f830c64cbe30dd4d1b88129e5ff = c005cfd48fcd7a06a9b3f9ff85949766a.Current;
					ref Pair<int> reference = ref cf417c4305592e9b47dc903871a89a447;
					c3be4e279c85d04da07f3693866750bf4 c3be4e279c85d04da07f3693866750bf5 = c61d675ea8e36dfac16b2e13340d49c75;
					reference = new Pair<int>(c3be4e279c85d04da07f3693866750bf5.c652f269a40a2519b8658ffbade581b4e, ccd258f830c64cbe30dd4d1b88129e5ff);
					if (c1b0bbfd4af1b6a4c0c71a5dd3c509812.Filter != null)
					{
						if (!c1b0bbfd4af1b6a4c0c71a5dd3c509812.Filter.Filter(cf417c4305592e9b47dc903871a89a447))
						{
							continue;
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
					}
					cd179d7f18da070d164af6afcb0af45b5 = cf417c4305592e9b47dc903871a89a447;
					c94f69126adc708dfc5f5fd5b63c52835 = 3;
					return true;
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
				c157d38dbd6e52975d28a26c1bae28bf5();
				goto IL_018e;
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
							c157d38dbd6e52975d28a26c1bae28bf5();
						}
					}
					break;
				}
				finally
				{
					cf5ed08492d286a4f4f2dc83362895f5f();
				}
			}
		}

		void IDisposable.Dispose()
		{
			//ILSpy generated this explicit interface implementation from .override directive in c087ddb9300ad4d53672b7a60e6d76672
			this.c087ddb9300ad4d53672b7a60e6d76672();
		}

		private void cf5ed08492d286a4f4f2dc83362895f5f()
		{
			c94f69126adc708dfc5f5fd5b63c52835 = -1;
			if (c3949eec112dd76879c9c7d89e229af84 != null)
			{
				c3949eec112dd76879c9c7d89e229af84.Dispose();
			}
		}

		private void c157d38dbd6e52975d28a26c1bae28bf5()
		{
			c94f69126adc708dfc5f5fd5b63c52835 = 1;
			if (c005cfd48fcd7a06a9b3f9ff85949766a == null)
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
					__ldtoken(ca7984d98487a225a157577646eaa88da.c157d38dbd6e52975d28a26c1bae28bf5());
				}
				c005cfd48fcd7a06a9b3f9ff85949766a.Dispose();
				return;
			}
		}
	}

	[CompilerGenerated]
	private sealed class c99caab5536fcc545767837612af1d128 : IEnumerable<Pair<int>>, IEnumerator<Pair<int>>, IEnumerable, IEnumerator, IDisposable
	{
		private Pair<int> cd179d7f18da070d164af6afcb0af45b5;

		private int c94f69126adc708dfc5f5fd5b63c52835;

		private int c0e8a058e779916e7715d7616fb0da9e7;

		public CompressedAabbTree c1b0bbfd4af1b6a4c0c71a5dd3c509812;

		public Vector3F ceabb47d7d959545d89c4300eabbdd7c2;

		public Vector3F c6f203721b629fec4c93cc63ff1172fc4;

		public Pose cbc1b7e0b011ad6dcdb8beb392ec7a646;

		public Pose cfdb83fd0acea070e804f836eed1cc8f0;

		public ISpatialPartition<int> cfd8883387afb9cc25a2ac9150de0f66b;

		public ISpatialPartition<int> cd1939b65df62ed5f7a2112466dabd8f0;

		public Vector3F c1b4918d9d3cc94171c67339d1fc78b0f;

		public Vector3F cfd54ba1e25c70994847ab953f2228207;

		public Pose cd2abebf7ff6ec7d035c28a04faaa459b;

		public Pose c1c245528512f0ae5611e6e833a70158f;

		public Vector3F c4a3ea7d87c0bbf451cab805bfd2393b6;

		public Vector3F c656fa850d82728d3914145ec2d2c9b5a;

		public Pose cdfda3989a769e490644a157859f624cb;

		public Pose ca2f8d4e576dcdccefb0b76ae3ac09c21;

		public Aabb c84b7249503b0addf040f9bb1ff4f91e7;

		public c3be4e279c85d04da07f3693866750bf4 c9b2d1f7c8562553322ad969371b2ae58;

		public Aabb c5d71d029c8932048d8dde6cd9cf33f4c;

		public int c3035a6ce03b4db4ce9f47a582fbcd8e8;

		public Pair<int> c3f74c8a8246e03af98b65464027836b4;

		public IEnumerator<c3be4e279c85d04da07f3693866750bf4> c1f6ab1c07cc71c21c25e9889420e7bb1;

		public IEnumerator<int> c66ea124f4e519bc2fb70b3265ec033a4;

		Pair<int> IEnumerator<Pair<int>>.Current
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
		public c99caab5536fcc545767837612af1d128(int c94f69126adc708dfc5f5fd5b63c52835)
		{
			this.c94f69126adc708dfc5f5fd5b63c52835 = c94f69126adc708dfc5f5fd5b63c52835;
			c0e8a058e779916e7715d7616fb0da9e7 = Thread.CurrentThread.ManagedThreadId;
		}

		[DebuggerHidden]
		private IEnumerator<Pair<int>> c3dccb34b8e6239e768a1be590aa08632()
		{
			c99caab5536fcc545767837612af1d128 c99caab5536fcc545767837612af1d129;
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
					__ldtoken(c99caab5536fcc545767837612af1d128.c3dccb34b8e6239e768a1be590aa08632());
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
					c99caab5536fcc545767837612af1d129 = this;
					goto IL_005b;
				}
			}
			c99caab5536fcc545767837612af1d129 = new c99caab5536fcc545767837612af1d128(0)
			{
				c1b0bbfd4af1b6a4c0c71a5dd3c509812 = c1b0bbfd4af1b6a4c0c71a5dd3c509812
			};
			goto IL_005b;
			IL_005b:
			c99caab5536fcc545767837612af1d129.ceabb47d7d959545d89c4300eabbdd7c2 = c6f203721b629fec4c93cc63ff1172fc4;
			c99caab5536fcc545767837612af1d129.cbc1b7e0b011ad6dcdb8beb392ec7a646 = cfdb83fd0acea070e804f836eed1cc8f0;
			c99caab5536fcc545767837612af1d129.cfd8883387afb9cc25a2ac9150de0f66b = cd1939b65df62ed5f7a2112466dabd8f0;
			c99caab5536fcc545767837612af1d129.c1b4918d9d3cc94171c67339d1fc78b0f = cfd54ba1e25c70994847ab953f2228207;
			c99caab5536fcc545767837612af1d129.cd2abebf7ff6ec7d035c28a04faaa459b = c1c245528512f0ae5611e6e833a70158f;
			return c99caab5536fcc545767837612af1d129;
		}

		IEnumerator<Pair<int>> IEnumerable<Pair<int>>.GetEnumerator()
		{
			//ILSpy generated this explicit interface implementation from .override directive in c3dccb34b8e6239e768a1be590aa08632
			return this.c3dccb34b8e6239e768a1be590aa08632();
		}

		[DebuggerHidden]
		private IEnumerator c63811cf6ff5c0b86c54fd5037c6af6ad()
		{
			return c3dccb34b8e6239e768a1be590aa08632();
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
						__ldtoken(c99caab5536fcc545767837612af1d128.MoveNext());
					}
					if (num != 3)
					{
						goto IL_0273;
					}
					c94f69126adc708dfc5f5fd5b63c52835 = 2;
					goto IL_0233;
				}
				c94f69126adc708dfc5f5fd5b63c52835 = -1;
				if (cfd8883387afb9cc25a2ac9150de0f66b == null)
				{
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(994));
				}
				cfd8883387afb9cc25a2ac9150de0f66b.Update(forceRebuild: false);
				c1b0bbfd4af1b6a4c0c71a5dd3c509812.Update(forceRebuild: false);
				c4a3ea7d87c0bbf451cab805bfd2393b6 = Vector3F.One / ceabb47d7d959545d89c4300eabbdd7c2;
				c656fa850d82728d3914145ec2d2c9b5a = Vector3F.One / c1b4918d9d3cc94171c67339d1fc78b0f;
				cdfda3989a769e490644a157859f624cb = cbc1b7e0b011ad6dcdb8beb392ec7a646.Inverse * cd2abebf7ff6ec7d035c28a04faaa459b;
				ca2f8d4e576dcdccefb0b76ae3ac09c21 = cdfda3989a769e490644a157859f624cb.Inverse;
				c84b7249503b0addf040f9bb1ff4f91e7 = cfd8883387afb9cc25a2ac9150de0f66b.Aabb;
				c84b7249503b0addf040f9bb1ff4f91e7 = c84b7249503b0addf040f9bb1ff4f91e7.GetAabb(c1b4918d9d3cc94171c67339d1fc78b0f, cdfda3989a769e490644a157859f624cb);
				c84b7249503b0addf040f9bb1ff4f91e7.Scale(c4a3ea7d87c0bbf451cab805bfd2393b6);
				c1f6ab1c07cc71c21c25e9889420e7bb1 = c1b0bbfd4af1b6a4c0c71a5dd3c509812.c4d76d858205a75f799980f42e41cbe5b(c84b7249503b0addf040f9bb1ff4f91e7).GetEnumerator();
				c94f69126adc708dfc5f5fd5b63c52835 = 1;
				goto IL_0253;
				IL_0233:
				while (c66ea124f4e519bc2fb70b3265ec033a4.MoveNext())
				{
					c3035a6ce03b4db4ce9f47a582fbcd8e8 = c66ea124f4e519bc2fb70b3265ec033a4.Current;
					ref Pair<int> reference = ref c3f74c8a8246e03af98b65464027836b4;
					c3be4e279c85d04da07f3693866750bf4 c3be4e279c85d04da07f3693866750bf5 = c9b2d1f7c8562553322ad969371b2ae58;
					reference = new Pair<int>(c3be4e279c85d04da07f3693866750bf5.c652f269a40a2519b8658ffbade581b4e, c3035a6ce03b4db4ce9f47a582fbcd8e8);
					if (c1b0bbfd4af1b6a4c0c71a5dd3c509812.Filter != null)
					{
						if (!c1b0bbfd4af1b6a4c0c71a5dd3c509812.Filter.Filter(c3f74c8a8246e03af98b65464027836b4))
						{
							continue;
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
					}
					cd179d7f18da070d164af6afcb0af45b5 = c3f74c8a8246e03af98b65464027836b4;
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
				ccab8b80a7eddee20add0102ac16aeaea();
				goto IL_0253;
				IL_0253:
				if (c1f6ab1c07cc71c21c25e9889420e7bb1.MoveNext())
				{
					c9b2d1f7c8562553322ad969371b2ae58 = c1f6ab1c07cc71c21c25e9889420e7bb1.Current;
					c5d71d029c8932048d8dde6cd9cf33f4c = c1b0bbfd4af1b6a4c0c71a5dd3c509812.cccf609c8efd00ebcc2b1a12189fe5e64(c9b2d1f7c8562553322ad969371b2ae58);
					c5d71d029c8932048d8dde6cd9cf33f4c = c5d71d029c8932048d8dde6cd9cf33f4c.GetAabb(ceabb47d7d959545d89c4300eabbdd7c2, ca2f8d4e576dcdccefb0b76ae3ac09c21);
					c5d71d029c8932048d8dde6cd9cf33f4c.Scale(c656fa850d82728d3914145ec2d2c9b5a);
					c66ea124f4e519bc2fb70b3265ec033a4 = cfd8883387afb9cc25a2ac9150de0f66b.GetOverlaps(c5d71d029c8932048d8dde6cd9cf33f4c).GetEnumerator();
					c94f69126adc708dfc5f5fd5b63c52835 = 2;
					goto IL_0233;
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
				c02df1fa059596fcb7812d801ab78c644();
				goto IL_0273;
				IL_0273:
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
							ccab8b80a7eddee20add0102ac16aeaea();
						}
					}
					break;
				}
				finally
				{
					c02df1fa059596fcb7812d801ab78c644();
				}
			}
		}

		void IDisposable.Dispose()
		{
			//ILSpy generated this explicit interface implementation from .override directive in c087ddb9300ad4d53672b7a60e6d76672
			this.c087ddb9300ad4d53672b7a60e6d76672();
		}

		private void c02df1fa059596fcb7812d801ab78c644()
		{
			c94f69126adc708dfc5f5fd5b63c52835 = -1;
			if (c1f6ab1c07cc71c21c25e9889420e7bb1 != null)
			{
				c1f6ab1c07cc71c21c25e9889420e7bb1.Dispose();
			}
		}

		private void ccab8b80a7eddee20add0102ac16aeaea()
		{
			c94f69126adc708dfc5f5fd5b63c52835 = 1;
			if (c66ea124f4e519bc2fb70b3265ec033a4 != null)
			{
				c66ea124f4e519bc2fb70b3265ec033a4.Dispose();
			}
		}
	}

	private int ca72616798115178580ff53362112e351 = 128;

	private static readonly float c40d76dd1f2804439c844fd57f5cc67fc = Numeric.EpsilonF;

	internal cf8d321a387e439baf196dca5797d9576 c8c9b32159bf45aa88a32433dee5df2c1;

	internal int ccc439410c5678c542a116b1c7b4551b8;

	internal List<int> c650f95538a011c39773e3b9e0b147480;

	internal c3be4e279c85d04da07f3693866750bf4[] cd26c0d77f8d7f857646d6821d2cb60a7;

	internal HashSet<Pair<int>> cdb565a0d0e484a399660c47a4ce9d1cf;

	internal Aabb c3a708fca2e00c0f5027ce857e9da5624;

	internal Vector3F c559f787cd29aab31167fc46547165074;

	internal Vector3F c955716b7e41c5abb5a33946fe6b86eda;

	internal IPairFilter<int> c7215839bcbb23f1ded1a6a9876755f7f;

	private Func<int, Aabb> c2978eaeaefa7ff754c7d1b58eb6f5187;

	[CompilerGenerated]
	private static Func<cbe5649b18b8b0caa00be9ad49188b193<int>> c3dc8ee93a71c2dfdfe8378428b48d58c;

	[CompilerGenerated]
	private static Func<c3be4e279c85d04da07f3693866750bf4, bool> c105b51c1405d398499ff3d4da738eb27;

	[CompilerGenerated]
	private static Func<c3be4e279c85d04da07f3693866750bf4, int> cfda7c666f7ea206b5565e1c779fb5b4e;

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
	///       Gets the axis-aligned bounding box (AABB) that contains all items.
	///       </summary>
	public Aabb Aabb
	{
		get
		{
			Update(forceRebuild: false);
			return c3a708fca2e00c0f5027ce857e9da5624;
		}
	}

	/// <summary>
	///       Gets the number of items contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.
	///       </summary>
	public int Count
	{
		get
		{
			if (c650f95538a011c39773e3b9e0b147480 == null)
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
							__ldtoken(CompressedAabbTree.get_Count());
						}
						return ccc439410c5678c542a116b1c7b4551b8;
					}
				}
			}
			return c650f95538a011c39773e3b9e0b147480.Count;
		}
	}

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
			if (!value)
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
					__ldtoken(CompressedAabbTree.set_EnableSelfOverlaps(bool));
				}
				if (cdb565a0d0e484a399660c47a4ce9d1cf == null)
				{
					cdb565a0d0e484a399660c47a4ce9d1cf = new HashSet<Pair<int>>();
					c8c9b32159bf45aa88a32433dee5df2c1 |= cf8d321a387e439baf196dca5797d9576.ce759ff2781e7c0601193b90d63f12c3c;
				}
				else
				{
					cdb565a0d0e484a399660c47a4ce9d1cf = null;
				}
				return;
			}
		}
	}

	/// <summary>
	///       Gets or sets the filter that is used to filter overlaps of two items.
	///       </summary>
	public IPairFilter<int> Filter
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
					switch (2)
					{
					case 0:
						continue;
					}
					break;
				}
				if (1 == 0)
				{
					__ldtoken(CompressedAabbTree.set_Filter(IPairFilter<int>));
				}
				c7215839bcbb23f1ded1a6a9876755f7f.Changed -= c7800892d6d855908c9d25556ad846a02;
			}
			c7215839bcbb23f1ded1a6a9876755f7f = value;
			c7800892d6d855908c9d25556ad846a02(this, EventArgs.Empty);
			if (c7215839bcbb23f1ded1a6a9876755f7f != null)
			{
				c7215839bcbb23f1ded1a6a9876755f7f.Changed += c7800892d6d855908c9d25556ad846a02;
			}
		}
	}

	/// <summary>
	///       Gets or sets the method that computes the <see cref="T:DigitalRune.Geometry.Shapes.Aabb" /> of an item.
	///       </summary>
	public Func<int, Aabb> GetAabbForItem
	{
		get
		{
			return c2978eaeaefa7ff754c7d1b58eb6f5187;
		}
		set
		{
			c2978eaeaefa7ff754c7d1b58eb6f5187 = value;
		}
	}

	private bool ce35cb448186020be99e2d6b19b0e03d5 => false;

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Partitioning.CompressedAabbTree" /> class.
	///       </summary>
	public CompressedAabbTree()
	{
		c650f95538a011c39773e3b9e0b147480 = ResourcePools<int>.Lists.Obtain();
		c8c9b32159bf45aa88a32433dee5df2c1 = cf8d321a387e439baf196dca5797d9576.c1d44713bc4a88b22856928fb23e46d3f;
	}

	/// <summary>
	///       Gets the items that touch the given AABB.
	///       </summary>
	/// <param name="aabb">The axis-aligned bounding box.</param>
	/// <returns>All items that touch the given AABB.</returns>
	public IEnumerable<int> GetOverlaps(Aabb aabb)
	{
		Update(forceRebuild: false);
		if (ccc439410c5678c542a116b1c7b4551b8 == 0)
		{
			yield break;
		}
		int num = 0;
		while (num < cd26c0d77f8d7f857646d6821d2cb60a7.Length)
		{
			c3be4e279c85d04da07f3693866750bf4 c7f587bd57d47897172f47483e6bcca = cd26c0d77f8d7f857646d6821d2cb60a7[num];
			bool flag = GeometryHelper.HaveContact(cccf609c8efd00ebcc2b1a12189fe5e64(c7f587bd57d47897172f47483e6bcca), aabb);
			if (flag)
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
					__ldtoken(c030c6eafad971e0859214358e6b9a580.MoveNext());
				}
				if (c7f587bd57d47897172f47483e6bcca.cfd73e16b19bb8bde65db991a787eb5ad)
				{
					yield return c7f587bd57d47897172f47483e6bcca.c652f269a40a2519b8658ffbade581b4e;
				}
			}
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
				if (!c7f587bd57d47897172f47483e6bcca.cfd73e16b19bb8bde65db991a787eb5ad)
				{
					num += c7f587bd57d47897172f47483e6bcca.cfb8d7112d33accdca0964eca99e29475;
					continue;
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
			}
			num++;
		}
	}

	private IEnumerable<c3be4e279c85d04da07f3693866750bf4> c4d76d858205a75f799980f42e41cbe5b(Aabb cdc3439410d15f28489c964ee87b88fe9)
	{
		Update(forceRebuild: false);
		if (ccc439410c5678c542a116b1c7b4551b8 == 0)
		{
			yield break;
		}
		int num = 0;
		while (num < cd26c0d77f8d7f857646d6821d2cb60a7.Length)
		{
			c3be4e279c85d04da07f3693866750bf4 c3be4e279c85d04da07f3693866750bf5 = cd26c0d77f8d7f857646d6821d2cb60a7[num];
			bool flag = GeometryHelper.HaveContact(cccf609c8efd00ebcc2b1a12189fe5e64(c3be4e279c85d04da07f3693866750bf5), cdc3439410d15f28489c964ee87b88fe9);
			if (flag && c3be4e279c85d04da07f3693866750bf5.cfd73e16b19bb8bde65db991a787eb5ad)
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
					__ldtoken(cb6b9209f7873d79f4a597241616e8a30.MoveNext());
				}
				yield return c3be4e279c85d04da07f3693866750bf5;
			}
			if (!flag)
			{
				if (!c3be4e279c85d04da07f3693866750bf5.cfd73e16b19bb8bde65db991a787eb5ad)
				{
					num += c3be4e279c85d04da07f3693866750bf5.cfb8d7112d33accdca0964eca99e29475;
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
			}
			num++;
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

	/// <summary>
	///       Gets the items that touch the given item.
	///       </summary>
	/// <param name="item">
	///       The item. (The given item must be part of the spatial partition. External items are not 
	///       supported.)
	///       </param>
	/// <returns>All items that touch the given item.</returns>
	public IEnumerable<int> GetOverlaps(int item)
	{
		Aabb aabb = GetAabbForItem(item);
		foreach (int overlap in GetOverlaps(aabb))
		{
			if (c8092bfef08eaf2732b73394233ee5fd1(new Pair<int>(overlap, item)))
			{
				yield return overlap;
			}
		}
	}

	/// <summary>
	///       Gets the items that touch the given ray.
	///       </summary>
	/// <param name="ray">The ray.</param>
	/// <returns>All items that are hit by the ray.</returns>
	public IEnumerable<int> GetOverlaps(Ray ray)
	{
		Update(forceRebuild: false);
		if (ccc439410c5678c542a116b1c7b4551b8 == 0)
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
				__ldtoken(c15e4f34eee62f863844c7270d24c7b66.MoveNext());
			}
			Vector3F c3ec982f192efbba86e42a1cea5c = new Vector3F(1f / ray.Direction.X, 1f / ray.Direction.Y, 1f / ray.Direction.Z);
			float c912da9d2d82e2a6523be60260abdfc = Numeric.EpsilonF * (1f + Aabb.Extent.Length);
			int num = 0;
			while (num < cd26c0d77f8d7f857646d6821d2cb60a7.Length)
			{
				c3be4e279c85d04da07f3693866750bf4 c7f587bd57d47897172f47483e6bcca = cd26c0d77f8d7f857646d6821d2cb60a7[num];
				bool flag = GeometryHelper.cdd6d17271fec83883494ba8e43eede6a(cccf609c8efd00ebcc2b1a12189fe5e64(c7f587bd57d47897172f47483e6bcca), ray.Origin, c3ec982f192efbba86e42a1cea5c, ray.Length, c912da9d2d82e2a6523be60260abdfc);
				if (flag && c7f587bd57d47897172f47483e6bcca.cfd73e16b19bb8bde65db991a787eb5ad)
				{
					yield return c7f587bd57d47897172f47483e6bcca.c652f269a40a2519b8658ffbade581b4e;
				}
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
					if (!c7f587bd57d47897172f47483e6bcca.cfd73e16b19bb8bde65db991a787eb5ad)
					{
						num += c7f587bd57d47897172f47483e6bcca.cfb8d7112d33accdca0964eca99e29475;
						continue;
					}
				}
				num++;
			}
			while (true)
			{
				switch (6)
				{
				case 0:
					break;
				default:
					yield break;
				}
			}
		}
	}

	/// <summary>
	///       Gets overlaps of all items contained in this spatial partition.
	///       </summary>
	/// <returns>
	///       All pairs of overlapping items of this spatial partition.
	///       </returns>
	/// <exception cref="T:DigitalRune.Geometry.GeometryException">
	///   <see cref="P:DigitalRune.Geometry.Partitioning.CompressedAabbTree.EnableSelfOverlaps" /> is <see langword="false" />.
	///       </exception>
	public IEnumerable<Pair<int>> GetOverlaps()
	{
		if (!EnableSelfOverlaps)
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
						__ldtoken(CompressedAabbTree.GetOverlaps());
					}
					throw new GeometryException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(856));
				}
			}
		}
		Update(forceRebuild: false);
		return cdb565a0d0e484a399660c47a4ce9d1cf;
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
	public float GetClosestPointCandidates(Aabb aabb, float maxDistanceSquared, Func<int, float> callback)
	{
		if (callback == null)
		{
			while (true)
			{
				switch (2)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(CompressedAabbTree.GetClosestPointCandidates(Aabb, float, Func<int, float>));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1023));
				}
			}
		}
		Update(forceRebuild: false);
		if (ccc439410c5678c542a116b1c7b4551b8 == 0)
		{
			while (true)
			{
				switch (5)
				{
				case 0:
					break;
				default:
					return -1f;
				}
			}
		}
		float c2878235e00baff8166ebc90ef27400ae = maxDistanceSquared;
		cb780b4b51062221f27d196ea0a7abde7(0, aabb, callback, ref c2878235e00baff8166ebc90ef27400ae);
		return c2878235e00baff8166ebc90ef27400ae;
	}

	private void cb780b4b51062221f27d196ea0a7abde7(int c88a85a6fc8e0a66e19d046d4d51cd3f1, Aabb cdc3439410d15f28489c964ee87b88fe9, Func<int, float> ce4b7628f9880cef87f99eae50a8132fe, ref float c2878235e00baff8166ebc90ef27400ae)
	{
		if (c2878235e00baff8166ebc90ef27400ae < 0f)
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
						__ldtoken(CompressedAabbTree.cb780b4b51062221f27d196ea0a7abde7(int, Aabb, Func<int, float>, ref float));
					}
					return;
				}
			}
		}
		c3be4e279c85d04da07f3693866750bf4 c7f587bd57d47897172f47483e6bcca = cd26c0d77f8d7f857646d6821d2cb60a7[c88a85a6fc8e0a66e19d046d4d51cd3f1];
		if (c2878235e00baff8166ebc90ef27400ae == 0f)
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
			if (!GeometryHelper.HaveContact(cccf609c8efd00ebcc2b1a12189fe5e64(c7f587bd57d47897172f47483e6bcca), cdc3439410d15f28489c964ee87b88fe9))
			{
				while (true)
				{
					switch (1)
					{
					case 0:
						break;
					default:
						return;
					}
				}
			}
		}
		if (c7f587bd57d47897172f47483e6bcca.cfd73e16b19bb8bde65db991a787eb5ad)
		{
			float val = ce4b7628f9880cef87f99eae50a8132fe(c7f587bd57d47897172f47483e6bcca.c652f269a40a2519b8658ffbade581b4e);
			c2878235e00baff8166ebc90ef27400ae = Math.Min(val, c2878235e00baff8166ebc90ef27400ae);
			return;
		}
		int num = c88a85a6fc8e0a66e19d046d4d51cd3f1 + 1;
		c3be4e279c85d04da07f3693866750bf4 c7f587bd57d47897172f47483e6bcca2 = cd26c0d77f8d7f857646d6821d2cb60a7[num];
		int num2;
		if (!c7f587bd57d47897172f47483e6bcca2.cfd73e16b19bb8bde65db991a787eb5ad)
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
			num2 = num + c7f587bd57d47897172f47483e6bcca2.cfb8d7112d33accdca0964eca99e29475;
		}
		else
		{
			num2 = num + 1;
		}
		int num3 = num2;
		c3be4e279c85d04da07f3693866750bf4 c7f587bd57d47897172f47483e6bcca3 = cd26c0d77f8d7f857646d6821d2cb60a7[num3];
		if (c2878235e00baff8166ebc90ef27400ae == 0f)
		{
			while (true)
			{
				switch (5)
				{
				case 0:
					break;
				default:
					cb780b4b51062221f27d196ea0a7abde7(num, cdc3439410d15f28489c964ee87b88fe9, ce4b7628f9880cef87f99eae50a8132fe, ref c2878235e00baff8166ebc90ef27400ae);
					cb780b4b51062221f27d196ea0a7abde7(num3, cdc3439410d15f28489c964ee87b88fe9, ce4b7628f9880cef87f99eae50a8132fe, ref c2878235e00baff8166ebc90ef27400ae);
					return;
				}
			}
		}
		float num4 = GeometryHelper.c6d56a7842537f534a2043d488cf4a1e6(cccf609c8efd00ebcc2b1a12189fe5e64(c7f587bd57d47897172f47483e6bcca2), cdc3439410d15f28489c964ee87b88fe9);
		float num5 = GeometryHelper.c6d56a7842537f534a2043d488cf4a1e6(cccf609c8efd00ebcc2b1a12189fe5e64(c7f587bd57d47897172f47483e6bcca3), cdc3439410d15f28489c964ee87b88fe9);
		if (num4 < num5)
		{
			while (true)
			{
				switch (2)
				{
				case 0:
					break;
				default:
					if (!(num4 > c2878235e00baff8166ebc90ef27400ae))
					{
						cb780b4b51062221f27d196ea0a7abde7(num, cdc3439410d15f28489c964ee87b88fe9, ce4b7628f9880cef87f99eae50a8132fe, ref c2878235e00baff8166ebc90ef27400ae);
						if (!(num5 > c2878235e00baff8166ebc90ef27400ae))
						{
							cb780b4b51062221f27d196ea0a7abde7(num3, cdc3439410d15f28489c964ee87b88fe9, ce4b7628f9880cef87f99eae50a8132fe, ref c2878235e00baff8166ebc90ef27400ae);
						}
					}
					return;
				}
			}
		}
		if (num5 > c2878235e00baff8166ebc90ef27400ae)
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
		cb780b4b51062221f27d196ea0a7abde7(num3, cdc3439410d15f28489c964ee87b88fe9, ce4b7628f9880cef87f99eae50a8132fe, ref c2878235e00baff8166ebc90ef27400ae);
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
		cb780b4b51062221f27d196ea0a7abde7(num, cdc3439410d15f28489c964ee87b88fe9, ce4b7628f9880cef87f99eae50a8132fe, ref c2878235e00baff8166ebc90ef27400ae);
	}

	private void ccf1870247a728556a633ff212007937e()
	{
		if (GetAabbForItem == null)
		{
			throw new GeometryException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(678));
		}
		if (c650f95538a011c39773e3b9e0b147480.Count == 1)
		{
			int arg = c650f95538a011c39773e3b9e0b147480[0];
			Aabb cdc3439410d15f28489c964ee87b88fe = GetAabbForItem(arg);
			ca1120865bbe3cbefc3d2159b0e82ce43(cdc3439410d15f28489c964ee87b88fe);
			c3be4e279c85d04da07f3693866750bf4 c7f587bd57d47897172f47483e6bcca = new c3be4e279c85d04da07f3693866750bf4
			{
				c652f269a40a2519b8658ffbade581b4e = c650f95538a011c39773e3b9e0b147480[0]
			};
			c0b87c69e75c790bacc351ae22965cb02(ref c7f587bd57d47897172f47483e6bcca, Aabb);
			cd26c0d77f8d7f857646d6821d2cb60a7 = new c3be4e279c85d04da07f3693866750bf4[1] { c7f587bd57d47897172f47483e6bcca };
			ccc439410c5678c542a116b1c7b4551b8 = 1;
		}
		else
		{
			ccc439410c5678c542a116b1c7b4551b8 = c650f95538a011c39773e3b9e0b147480.Count;
			List<cbe5649b18b8b0caa00be9ad49188b193<int>> list = ResourcePools<cbe5649b18b8b0caa00be9ad49188b193<int>>.Lists.Obtain();
			for (int i = 0; i < ccc439410c5678c542a116b1c7b4551b8; i++)
			{
				int num = c650f95538a011c39773e3b9e0b147480[i];
				Aabb c3e263e990bc8d6c3a2d636ac7a19c6c = GetAabbForItem(num);
				list.Add(new AabbTree<int>.caa41fe1fc008c4cd467f2caa714d2784
				{
					c3e263e990bc8d6c3a2d636ac7a19c6c6 = c3e263e990bc8d6c3a2d636ac7a19c6c,
					c652f269a40a2519b8658ffbade581b4e = num
				});
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
				__ldtoken(CompressedAabbTree.ccf1870247a728556a633ff212007937e());
			}
			if (c3dc8ee93a71c2dfdfe8378428b48d58c == null)
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
				c3dc8ee93a71c2dfdfe8378428b48d58c = () => new AabbTree<int>.caa41fe1fc008c4cd467f2caa714d2784();
			}
			AabbTree<int>.caa41fe1fc008c4cd467f2caa714d2784 caa41fe1fc008c4cd467f2caa714d = (AabbTree<int>.caa41fe1fc008c4cd467f2caa714d2784)c5c139e98a37beb65b1cb5aea8ce81daf.ccf1870247a728556a633ff212007937e(list, c3dc8ee93a71c2dfdfe8378428b48d58c, BottomUpBuildThreshold);
			ca1120865bbe3cbefc3d2159b0e82ce43(caa41fe1fc008c4cd467f2caa714d.c3e263e990bc8d6c3a2d636ac7a19c6c6);
			List<c3be4e279c85d04da07f3693866750bf4> list2 = ResourcePools<c3be4e279c85d04da07f3693866750bf4>.Lists.Obtain();
			c5025a78ed1366f952f766cbf4d75ab89(list2, caa41fe1fc008c4cd467f2caa714d);
			cd26c0d77f8d7f857646d6821d2cb60a7 = list2.ToArray();
			ResourcePools<cbe5649b18b8b0caa00be9ad49188b193<int>>.Lists.Recycle(list);
			ResourcePools<c3be4e279c85d04da07f3693866750bf4>.Lists.Recycle(list2);
		}
		ResourcePools<int>.Lists.Recycle(c650f95538a011c39773e3b9e0b147480);
		c650f95538a011c39773e3b9e0b147480 = null;
	}

	private void c5025a78ed1366f952f766cbf4d75ab89(List<c3be4e279c85d04da07f3693866750bf4> c51237b6b262d031345829e4dd55f8163, AabbTree<int>.caa41fe1fc008c4cd467f2caa714d2784 c6d62a4b79e7cfa6f7a875b33c84f218a)
	{
		if (c6d62a4b79e7cfa6f7a875b33c84f218a.cfd73e16b19bb8bde65db991a787eb5ad)
		{
			c3be4e279c85d04da07f3693866750bf4 c7f587bd57d47897172f47483e6bcca = new c3be4e279c85d04da07f3693866750bf4
			{
				c652f269a40a2519b8658ffbade581b4e = c6d62a4b79e7cfa6f7a875b33c84f218a.c652f269a40a2519b8658ffbade581b4e
			};
			c0b87c69e75c790bacc351ae22965cb02(ref c7f587bd57d47897172f47483e6bcca, c6d62a4b79e7cfa6f7a875b33c84f218a.c3e263e990bc8d6c3a2d636ac7a19c6c6);
			c51237b6b262d031345829e4dd55f8163.Add(c7f587bd57d47897172f47483e6bcca);
			return;
		}
		int count = c51237b6b262d031345829e4dd55f8163.Count;
		c3be4e279c85d04da07f3693866750bf4 c7f587bd57d47897172f47483e6bcca2 = default;
		c0b87c69e75c790bacc351ae22965cb02(ref c7f587bd57d47897172f47483e6bcca2, c6d62a4b79e7cfa6f7a875b33c84f218a.c3e263e990bc8d6c3a2d636ac7a19c6c6);
		c51237b6b262d031345829e4dd55f8163.Add(c7f587bd57d47897172f47483e6bcca2);
		c5025a78ed1366f952f766cbf4d75ab89(c51237b6b262d031345829e4dd55f8163, c6d62a4b79e7cfa6f7a875b33c84f218a.c6d3fb34101b9e1ce92055c2914d2d1de);
		c5025a78ed1366f952f766cbf4d75ab89(c51237b6b262d031345829e4dd55f8163, c6d62a4b79e7cfa6f7a875b33c84f218a.c2d9d869d0c8fd5fb55f452375f580a4a);
		c7f587bd57d47897172f47483e6bcca2.cfb8d7112d33accdca0964eca99e29475 = c51237b6b262d031345829e4dd55f8163.Count - count;
		c51237b6b262d031345829e4dd55f8163[count] = c7f587bd57d47897172f47483e6bcca2;
	}

	private void c4331eef59d25a795f3b70890347a4c6c()
	{
		Aabb[] array = new Aabb[cd26c0d77f8d7f857646d6821d2cb60a7.Length];
		int c943673bf4a019321f5f0f6d2dfda2f = 0;
		c5aaa29b76f26758a3cab75797418e24d(array, 0, ref c943673bf4a019321f5f0f6d2dfda2f);
		ca1120865bbe3cbefc3d2159b0e82ce43(array[0]);
		for (int i = 0; i < cd26c0d77f8d7f857646d6821d2cb60a7.Length; i++)
		{
			c3be4e279c85d04da07f3693866750bf4 c7f587bd57d47897172f47483e6bcca = cd26c0d77f8d7f857646d6821d2cb60a7[i];
			c0b87c69e75c790bacc351ae22965cb02(ref c7f587bd57d47897172f47483e6bcca, array[i]);
			cd26c0d77f8d7f857646d6821d2cb60a7[i] = c7f587bd57d47897172f47483e6bcca;
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
				__ldtoken(CompressedAabbTree.c4331eef59d25a795f3b70890347a4c6c());
			}
			return;
		}
	}

	private void c5aaa29b76f26758a3cab75797418e24d(Aabb[] c57c5638d222c296424f9eacb83632fc6, int c88a85a6fc8e0a66e19d046d4d51cd3f1, ref int c943673bf4a019321f5f0f6d2dfda2f94)
	{
		c943673bf4a019321f5f0f6d2dfda2f94++;
		c3be4e279c85d04da07f3693866750bf4 c3be4e279c85d04da07f3693866750bf5 = cd26c0d77f8d7f857646d6821d2cb60a7[c88a85a6fc8e0a66e19d046d4d51cd3f1];
		if (c3be4e279c85d04da07f3693866750bf5.cfd73e16b19bb8bde65db991a787eb5ad)
		{
			ref Aabb reference = ref c57c5638d222c296424f9eacb83632fc6[c88a85a6fc8e0a66e19d046d4d51cd3f1];
			reference = GetAabbForItem(c3be4e279c85d04da07f3693866750bf5.c652f269a40a2519b8658ffbade581b4e);
			return;
		}
		int num = c88a85a6fc8e0a66e19d046d4d51cd3f1 + 1;
		c5aaa29b76f26758a3cab75797418e24d(c57c5638d222c296424f9eacb83632fc6, num, ref c943673bf4a019321f5f0f6d2dfda2f94);
		int num2 = c943673bf4a019321f5f0f6d2dfda2f94;
		c5aaa29b76f26758a3cab75797418e24d(c57c5638d222c296424f9eacb83632fc6, num2, ref c943673bf4a019321f5f0f6d2dfda2f94);
		ref Aabb reference2 = ref c57c5638d222c296424f9eacb83632fc6[c88a85a6fc8e0a66e19d046d4d51cd3f1];
		reference2 = Aabb.Merge(c57c5638d222c296424f9eacb83632fc6[num], c57c5638d222c296424f9eacb83632fc6[num2]);
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
	public IEnumerable<Pair<int>> GetOverlaps(ISpatialPartition<int> otherPartition)
	{
		if (otherPartition == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(994));
		}
		otherPartition.Update(forceRebuild: false);
		Update(forceRebuild: false);
		foreach (c3be4e279c85d04da07f3693866750bf4 item in c4d76d858205a75f799980f42e41cbe5b(otherPartition.Aabb))
		{
			IEnumerable<int> overlaps = otherPartition.GetOverlaps(cccf609c8efd00ebcc2b1a12189fe5e64(item));
			IEnumerator<int> enumerator2 = overlaps.GetEnumerator();
			try
			{
				while (enumerator2.MoveNext())
				{
					int current2 = enumerator2.Current;
					c3be4e279c85d04da07f3693866750bf4 c3be4e279c85d04da07f3693866750bf5 = item;
					Pair<int> pair = new Pair<int>(c3be4e279c85d04da07f3693866750bf5.c652f269a40a2519b8658ffbade581b4e, current2);
					if (Filter != null)
					{
						if (!Filter.Filter(pair))
						{
							continue;
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
					}
					yield return pair;
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
			}
			finally
			{
				if (enumerator2 != null)
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
							__ldtoken(ca7984d98487a225a157577646eaa88da.c157d38dbd6e52975d28a26c1bae28bf5());
						}
						enumerator2.Dispose();
						break;
					}
				}
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
	public IEnumerable<Pair<int>> GetOverlaps(Vector3F scale, Pose pose, ISpatialPartition<int> otherPartition, Vector3F otherScale, Pose otherPose)
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
		using IEnumerator<c3be4e279c85d04da07f3693866750bf4> enumerator = c4d76d858205a75f799980f42e41cbe5b(aabb).GetEnumerator();
		while (enumerator.MoveNext())
		{
			c3be4e279c85d04da07f3693866750bf4 current = enumerator.Current;
			Aabb aabb2 = cccf609c8efd00ebcc2b1a12189fe5e64(current).GetAabb(scale, inverse);
			aabb2.Scale(scale3);
			using IEnumerator<int> enumerator2 = otherPartition.GetOverlaps(aabb2).GetEnumerator();
			while (enumerator2.MoveNext())
			{
				int current2 = enumerator2.Current;
				c3be4e279c85d04da07f3693866750bf4 c3be4e279c85d04da07f3693866750bf5 = current;
				Pair<int> pair = new Pair<int>(c3be4e279c85d04da07f3693866750bf5.c652f269a40a2519b8658ffbade581b4e, current2);
				if (Filter != null)
				{
					if (!Filter.Filter(pair))
					{
						continue;
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
					__ldtoken(c99caab5536fcc545767837612af1d128.MoveNext());
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
			switch (2)
			{
			case 0:
				break;
			default:
				yield break;
			}
		}
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
	public void GetClosestPointCandidates(Vector3F scale, Pose pose, ISpatialPartition<int> otherPartition, Vector3F otherScale, Pose otherPose, Func<int, int, float> callback)
	{
		if (otherPartition == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(994));
		}
		if (callback == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1023));
		}
		otherPartition.Update(forceRebuild: false);
		Update(forceRebuild: false);
		if (ccc439410c5678c542a116b1c7b4551b8 == 0)
		{
			while (true)
			{
				switch (2)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(CompressedAabbTree.GetClosestPointCandidates(Vector3F, Pose, ISpatialPartition<int>, Vector3F, Pose, Func<int, int, float>));
					}
					return;
				}
			}
		}
		if (otherPartition is ISupportClosestPointQueries<int>)
		{
			while (true)
			{
				switch (3)
				{
				case 0:
					break;
				default:
					cb780b4b51062221f27d196ea0a7abde7(scale, pose, (ISupportClosestPointQueries<int>)otherPartition, otherScale, otherPose, callback);
					return;
				}
			}
		}
		cb780b4b51062221f27d196ea0a7abde7(otherPartition, callback);
	}

	private void cb780b4b51062221f27d196ea0a7abde7(Vector3F ceabb47d7d959545d89c4300eabbdd7c2, Pose cbc1b7e0b011ad6dcdb8beb392ec7a646, ISupportClosestPointQueries<int> cfd8883387afb9cc25a2ac9150de0f66b, Vector3F c1b4918d9d3cc94171c67339d1fc78b0f, Pose cd2abebf7ff6ec7d035c28a04faaa459b, Func<int, int, float> ce4b7628f9880cef87f99eae50a8132fe)
	{
		cc308d98a84a8563af043b321600339a2<int> cc308d98a84a8563af043b321600339a3 = cc308d98a84a8563af043b321600339a2<int>.c2d8ce8b1ccf2b905e3b18f8a5145fe8a();
		cc308d98a84a8563af043b321600339a3.OriginalCallback = ce4b7628f9880cef87f99eae50a8132fe;
		Pose pose = cd2abebf7ff6ec7d035c28a04faaa459b.Inverse * cbc1b7e0b011ad6dcdb8beb392ec7a646;
		Vector3F scale = Vector3F.One / c1b4918d9d3cc94171c67339d1fc78b0f;
		float maxDistanceSquared = float.PositiveInfinity;
		c3be4e279c85d04da07f3693866750bf4[] array = cd26c0d77f8d7f857646d6821d2cb60a7;
		for (int i = 0; i < array.Length; i++)
		{
			c3be4e279c85d04da07f3693866750bf4 c7f587bd57d47897172f47483e6bcca = array[i];
			if (c7f587bd57d47897172f47483e6bcca.cfd73e16b19bb8bde65db991a787eb5ad)
			{
				cc308d98a84a8563af043b321600339a3.Item = c7f587bd57d47897172f47483e6bcca.c652f269a40a2519b8658ffbade581b4e;
				Aabb aabb = cccf609c8efd00ebcc2b1a12189fe5e64(c7f587bd57d47897172f47483e6bcca).GetAabb(ceabb47d7d959545d89c4300eabbdd7c2, pose);
				aabb.Scale(scale);
				maxDistanceSquared = cfd8883387afb9cc25a2ac9150de0f66b.GetClosestPointCandidates(aabb, maxDistanceSquared, cc308d98a84a8563af043b321600339a3.c6516654248581f959063ea51406f708b);
			}
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
				__ldtoken(CompressedAabbTree.cb780b4b51062221f27d196ea0a7abde7(Vector3F, Pose, ISupportClosestPointQueries<int>, Vector3F, Pose, Func<int, int, float>));
			}
			cc308d98a84a8563af043b321600339a3.ca18b9b5faa9851d6355f617787bb5859();
			return;
		}
	}

	private void cb780b4b51062221f27d196ea0a7abde7(ISpatialPartition<int> cfd8883387afb9cc25a2ac9150de0f66b, Func<int, int, float> ce4b7628f9880cef87f99eae50a8132fe)
	{
		c3be4e279c85d04da07f3693866750bf4[] array = cd26c0d77f8d7f857646d6821d2cb60a7;
		for (int i = 0; i < array.Length; i++)
		{
			c3be4e279c85d04da07f3693866750bf4 c3be4e279c85d04da07f3693866750bf5 = array[i];
			if (!c3be4e279c85d04da07f3693866750bf5.cfd73e16b19bb8bde65db991a787eb5ad)
			{
				continue;
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
			if (1 == 0)
			{
				__ldtoken(CompressedAabbTree.cb780b4b51062221f27d196ea0a7abde7(ISpatialPartition<int>, Func<int, int, float>));
			}
			using IEnumerator<int> enumerator = cfd8883387afb9cc25a2ac9150de0f66b.GetEnumerator();
			while (enumerator.MoveNext())
			{
				int current = enumerator.Current;
				float num = ce4b7628f9880cef87f99eae50a8132fe(c3be4e279c85d04da07f3693866750bf5.c652f269a40a2519b8658ffbade581b4e, current);
				if (!(num < 0f))
				{
					continue;
				}
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
			while (true)
			{
				switch (1)
				{
				case 0:
					break;
				default:
					goto end_IL_0080;
				}
				continue;
				end_IL_0080:
				break;
			}
		}
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
	///       Creates a new <see cref="T:DigitalRune.Geometry.Partitioning.CompressedAabbTree" /> that is a clone (deep copy) of the current
	///       instance.
	///       </summary>
	/// <returns>
	///       A new <see cref="T:DigitalRune.Geometry.GeometricObject" /> that is a clone (deep copy) of the current instance.
	///       </returns>
	public CompressedAabbTree Clone()
	{
		CompressedAabbTree compressedAabbTree = new CompressedAabbTree();
		compressedAabbTree.EnableSelfOverlaps = EnableSelfOverlaps;
		compressedAabbTree.Filter = Filter;
		compressedAabbTree.GetAabbForItem = GetAabbForItem;
		return compressedAabbTree;
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

	/// <summary>
	///       Returns an enumerator that iterates through the collection.
	///       </summary>
	/// <returns>
	///       A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
	///       </returns>
	public IEnumerator<int> GetEnumerator()
	{
		if (c650f95538a011c39773e3b9e0b147480 != null)
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
						__ldtoken(CompressedAabbTree.GetEnumerator());
					}
					return c650f95538a011c39773e3b9e0b147480.GetEnumerator();
				}
			}
		}
		if (cd26c0d77f8d7f857646d6821d2cb60a7 != null)
		{
			return (from c7f587bd57d47897172f47483e6bcca68 in cd26c0d77f8d7f857646d6821d2cb60a7
				where c7f587bd57d47897172f47483e6bcca68.cfd73e16b19bb8bde65db991a787eb5ad
				select c7f587bd57d47897172f47483e6bcca68.c652f269a40a2519b8658ffbade581b4e).GetEnumerator();
		}
		return Enumerable.Empty<int>().GetEnumerator();
	}

	/// <summary>
	///       Adds an item to the <see cref="T:DigitalRune.Geometry.Partitioning.CompressedAabbTree" />.
	///       </summary>
	/// <param name="item">The object to add to the <see cref="T:DigitalRune.Geometry.Partitioning.CompressedAabbTree" />.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="item" /> is <see langword="null" />.
	///       </exception>
	public void Add(int item)
	{
		ce62d660e3e6c341d90029026afa5b2eb();
		c650f95538a011c39773e3b9e0b147480.Add(item);
	}

	/// <summary>
	///       Removes all items from the <see cref="T:DigitalRune.Geometry.Partitioning.CompressedAabbTree" />.
	///       </summary>
	public void Clear()
	{
		if (c650f95538a011c39773e3b9e0b147480 != null)
		{
			c650f95538a011c39773e3b9e0b147480.Clear();
		}
		else
		{
			c650f95538a011c39773e3b9e0b147480 = ResourcePools<int>.Lists.Obtain();
			cd26c0d77f8d7f857646d6821d2cb60a7 = null;
			ccc439410c5678c542a116b1c7b4551b8 = 0;
			if (cdb565a0d0e484a399660c47a4ce9d1cf != null)
			{
				cdb565a0d0e484a399660c47a4ce9d1cf.Clear();
			}
		}
		c3a708fca2e00c0f5027ce857e9da5624 = default;
		c8c9b32159bf45aa88a32433dee5df2c1 = cf8d321a387e439baf196dca5797d9576.c1d44713bc4a88b22856928fb23e46d3f;
	}

	/// <summary>
	///       Determines whether the <see cref="T:DigitalRune.Geometry.Partitioning.CompressedAabbTree" /> contains a specific value.
	///       </summary>
	/// <param name="item">The object to locate in the <see cref="T:DigitalRune.Geometry.Partitioning.CompressedAabbTree" />.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="item" /> is found in the 
	///       <see cref="T:DigitalRune.Geometry.Partitioning.CompressedAabbTree" />; otherwise, <see langword="false" />.
	///       </returns>
	public bool Contains(int item)
	{
		if (c650f95538a011c39773e3b9e0b147480 != null)
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
						__ldtoken(CompressedAabbTree.Contains(int));
					}
					return c650f95538a011c39773e3b9e0b147480.Contains(item);
				}
			}
		}
		c3be4e279c85d04da07f3693866750bf4[] array = cd26c0d77f8d7f857646d6821d2cb60a7;
		for (int i = 0; i < array.Length; i++)
		{
			c3be4e279c85d04da07f3693866750bf4 c3be4e279c85d04da07f3693866750bf5 = array[i];
			if (!c3be4e279c85d04da07f3693866750bf5.cfd73e16b19bb8bde65db991a787eb5ad)
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
			if (c3be4e279c85d04da07f3693866750bf5.c652f269a40a2519b8658ffbade581b4e != item)
			{
				continue;
			}
			while (true)
			{
				switch (5)
				{
				case 0:
					continue;
				}
				return true;
			}
		}
		while (true)
		{
			switch (4)
			{
			case 0:
				continue;
			}
			return false;
		}
	}

	/// <summary>
	///       Copies the elements of the tree to an <see cref="T:System.Array" />, starting at a particular 
	///       <see cref="T:System.Array" /> index.
	///       </summary>
	/// <param name="array">
	///       The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from 
	///       <see cref="T:DigitalRune.Geometry.Partitioning.CompressedAabbTree" />. The <see cref="T:System.Array" /> must have zero-based indexing.
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
	///   <paramref name="array" /> is multidimensional. Or <paramref name="arrayIndex" /> is equal to 
	///       or greater than the length of <paramref name="array" />. Or the number of elements in the 
	///       source <see cref="T:DigitalRune.Geometry.Partitioning.CompressedAabbTree" /> is greater than the available space from 
	///       <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />.
	///       </exception>
	public void CopyTo(int[] array, int arrayIndex)
	{
		if (c650f95538a011c39773e3b9e0b147480 != null)
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
						__ldtoken(CompressedAabbTree.CopyTo(int[], int));
					}
					c650f95538a011c39773e3b9e0b147480.CopyTo(array, arrayIndex);
					return;
				}
			}
		}
		if (arrayIndex < 0)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5885));
		}
		c3be4e279c85d04da07f3693866750bf4[] array2 = cd26c0d77f8d7f857646d6821d2cb60a7;
		for (int i = 0; i < array2.Length; i++)
		{
			c3be4e279c85d04da07f3693866750bf4 c3be4e279c85d04da07f3693866750bf5 = array2[i];
			if (!c3be4e279c85d04da07f3693866750bf5.cfd73e16b19bb8bde65db991a787eb5ad)
			{
				continue;
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
			if (arrayIndex >= array.Length)
			{
				throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5885));
			}
			array[arrayIndex] = c3be4e279c85d04da07f3693866750bf5.c652f269a40a2519b8658ffbade581b4e;
			arrayIndex++;
		}
	}

	private bool c8092bfef08eaf2732b73394233ee5fd1(Pair<int> c3cdd6711e3e96d722a86b2bfd486c923)
	{
		if (c3cdd6711e3e96d722a86b2bfd486c923.First == c3cdd6711e3e96d722a86b2bfd486c923.Second)
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
						__ldtoken(CompressedAabbTree.c8092bfef08eaf2732b73394233ee5fd1(Pair<int>));
					}
					return false;
				}
			}
		}
		if (Filter != null)
		{
			while (true)
			{
				switch (3)
				{
				case 0:
					break;
				default:
					return Filter.Filter(c3cdd6711e3e96d722a86b2bfd486c923);
				}
			}
		}
		return true;
	}

	/// <summary>
	///       Removes the first occurrence of a specific object from the <see cref="T:DigitalRune.Geometry.Partitioning.CompressedAabbTree" />.
	///       </summary>
	/// <param name="item">
	///       The object to remove from the <see cref="T:DigitalRune.Geometry.Partitioning.CompressedAabbTree" />.
	///       </param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="item" /> was successfully removed from the 
	///       <see cref="T:DigitalRune.Geometry.Partitioning.CompressedAabbTree" />; otherwise, <see langword="false" />. This method also 
	///       returns <see langword="false" /> if <paramref name="item" /> is not found in the original 
	///       <see cref="T:DigitalRune.Geometry.Partitioning.CompressedAabbTree" />.
	///       </returns>
	public bool Remove(int item)
	{
		ce62d660e3e6c341d90029026afa5b2eb();
		return c650f95538a011c39773e3b9e0b147480.Remove(item);
	}

	/// <summary>
	///       Invalidates the cached spatial information of all items in the spatial partition.
	///       </summary>
	public void Invalidate()
	{
		c8c9b32159bf45aa88a32433dee5df2c1 = cf8d321a387e439baf196dca5797d9576.c5fd1b383396ba2ccedfa66b0f2eb1382;
	}

	/// <summary>
	///       Invalidates the cached spatial information of the specified item.
	///       </summary>
	/// <param name="item">The item that has moved or changed its shape.</param>
	public void Invalidate(int item)
	{
		c8c9b32159bf45aa88a32433dee5df2c1 = cf8d321a387e439baf196dca5797d9576.c5fd1b383396ba2ccedfa66b0f2eb1382;
	}

	private void ce62d660e3e6c341d90029026afa5b2eb()
	{
		c8c9b32159bf45aa88a32433dee5df2c1 = cf8d321a387e439baf196dca5797d9576.c5fd1b383396ba2ccedfa66b0f2eb1382;
		if (c650f95538a011c39773e3b9e0b147480 != null)
		{
			return;
		}
		c650f95538a011c39773e3b9e0b147480 = ResourcePools<int>.Lists.Obtain();
		c3be4e279c85d04da07f3693866750bf4[] array = cd26c0d77f8d7f857646d6821d2cb60a7;
		for (int i = 0; i < array.Length; i++)
		{
			c3be4e279c85d04da07f3693866750bf4 c3be4e279c85d04da07f3693866750bf5 = array[i];
			if (!c3be4e279c85d04da07f3693866750bf5.cfd73e16b19bb8bde65db991a787eb5ad)
			{
				continue;
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
				__ldtoken(CompressedAabbTree.ce62d660e3e6c341d90029026afa5b2eb());
			}
			c650f95538a011c39773e3b9e0b147480.Add(c3be4e279c85d04da07f3693866750bf5.c652f269a40a2519b8658ffbade581b4e);
		}
		while (true)
		{
			switch (4)
			{
			case 0:
				continue;
			}
			cd26c0d77f8d7f857646d6821d2cb60a7 = null;
			ccc439410c5678c542a116b1c7b4551b8 = 0;
			if (cdb565a0d0e484a399660c47a4ce9d1cf != null)
			{
				cdb565a0d0e484a399660c47a4ce9d1cf.Clear();
			}
			return;
		}
	}

	internal void c7800892d6d855908c9d25556ad846a02(object c807f682152464d11dde83a885bf87124, EventArgs c7fefeb91d4632901bd52a1f245b70db1)
	{
		c8c9b32159bf45aa88a32433dee5df2c1 |= cf8d321a387e439baf196dca5797d9576.ce759ff2781e7c0601193b90d63f12c3c;
	}

	/// <summary>
	///       Updates the internal structure of this <see cref="T:DigitalRune.Geometry.Partitioning.ISpatialPartition`1" />.
	///       </summary>
	/// <param name="forceRebuild">
	///       If set to <see langword="true" /> the internal structure will be rebuilt from scratch. If set 
	///       to <see langword="false" /> the spatial partition can decide to rebuild everything or refit 
	///       only the invalidated parts.
	///       </param>
	public void Update(bool forceRebuild)
	{
		if (c8c9b32159bf45aa88a32433dee5df2c1 == cf8d321a387e439baf196dca5797d9576.c1d44713bc4a88b22856928fb23e46d3f)
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
						__ldtoken(CompressedAabbTree.Update(bool));
					}
					return;
				}
			}
		}
		if (forceRebuild)
		{
			ce62d660e3e6c341d90029026afa5b2eb();
		}
		if (c8c9b32159bf45aa88a32433dee5df2c1 == cf8d321a387e439baf196dca5797d9576.c5fd1b383396ba2ccedfa66b0f2eb1382)
		{
			c8c9b32159bf45aa88a32433dee5df2c1 = cf8d321a387e439baf196dca5797d9576.c1d44713bc4a88b22856928fb23e46d3f;
			if (c650f95538a011c39773e3b9e0b147480 != null)
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
				if (c650f95538a011c39773e3b9e0b147480.Count > 0)
				{
					while (true)
					{
						switch (7)
						{
						case 0:
							break;
						default:
							ccf1870247a728556a633ff212007937e();
							ce759ff2781e7c0601193b90d63f12c3c();
							return;
						}
					}
				}
			}
			if (ccc439410c5678c542a116b1c7b4551b8 <= 0)
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
				c4331eef59d25a795f3b70890347a4c6c();
				ce759ff2781e7c0601193b90d63f12c3c();
				return;
			}
		}
		c8c9b32159bf45aa88a32433dee5df2c1 = cf8d321a387e439baf196dca5797d9576.c1d44713bc4a88b22856928fb23e46d3f;
		ce759ff2781e7c0601193b90d63f12c3c();
	}

	private void ca1120865bbe3cbefc3d2159b0e82ce43(Aabb cdc3439410d15f28489c964ee87b88fe9)
	{
		c3a708fca2e00c0f5027ce857e9da5624 = cdc3439410d15f28489c964ee87b88fe9;
		Vector3F vector3F = new Vector3F(c40d76dd1f2804439c844fd57f5cc67fc);
		c3a708fca2e00c0f5027ce857e9da5624.Minimum = cdc3439410d15f28489c964ee87b88fe9.Minimum - vector3F;
		c3a708fca2e00c0f5027ce857e9da5624.Maximum = cdc3439410d15f28489c964ee87b88fe9.Maximum + vector3F;
		c559f787cd29aab31167fc46547165074 = new Vector3F(65533f) / c3a708fca2e00c0f5027ce857e9da5624.Extent;
		c955716b7e41c5abb5a33946fe6b86eda = Vector3F.One / c559f787cd29aab31167fc46547165074;
	}

	private Aabb cccf609c8efd00ebcc2b1a12189fe5e64(c3be4e279c85d04da07f3693866750bf4 c7f587bd57d47897172f47483e6bcca68)
	{
		Aabb aabb = Aabb;
		Aabb result = default;
		result.Minimum.X = aabb.Minimum.X + (float)(int)c7f587bd57d47897172f47483e6bcca68.c0766ee351495379577e4bd426a93fd76 * c955716b7e41c5abb5a33946fe6b86eda.X;
		result.Minimum.Y = aabb.Minimum.Y + (float)(int)c7f587bd57d47897172f47483e6bcca68.ca908b2ef855c3e51cf79eb5717ba52da * c955716b7e41c5abb5a33946fe6b86eda.Y;
		result.Minimum.Z = aabb.Minimum.Z + (float)(int)c7f587bd57d47897172f47483e6bcca68.c2d1089e3916028efbe65eebdee0edea4 * c955716b7e41c5abb5a33946fe6b86eda.Z;
		result.Maximum.X = aabb.Minimum.X + (float)(int)c7f587bd57d47897172f47483e6bcca68.ca496a3c2e4033c7383700937a074c8be * c955716b7e41c5abb5a33946fe6b86eda.X;
		result.Maximum.Y = aabb.Minimum.Y + (float)(int)c7f587bd57d47897172f47483e6bcca68.c7d2a56de6e39281750b1f1c7a61a8868 * c955716b7e41c5abb5a33946fe6b86eda.Y;
		result.Maximum.Z = aabb.Minimum.Z + (float)(int)c7f587bd57d47897172f47483e6bcca68.c95b253ba97348b0005f6ccc969f9c2e3 * c955716b7e41c5abb5a33946fe6b86eda.Z;
		return result;
	}

	private void c0b87c69e75c790bacc351ae22965cb02(ref c3be4e279c85d04da07f3693866750bf4 c7f587bd57d47897172f47483e6bcca68, Aabb cdc3439410d15f28489c964ee87b88fe9)
	{
		Vector3F vector3F = (cdc3439410d15f28489c964ee87b88fe9.Minimum - Aabb.Minimum) * c559f787cd29aab31167fc46547165074;
		Vector3F vector3F2 = (cdc3439410d15f28489c964ee87b88fe9.Maximum - Aabb.Minimum) * c559f787cd29aab31167fc46547165074;
		int c0766ee351495379577e4bd426a93fd;
		if (!(vector3F.X > 1f))
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
				__ldtoken(CompressedAabbTree.c0b87c69e75c790bacc351ae22965cb02(ref c3be4e279c85d04da07f3693866750bf4, Aabb));
			}
			c0766ee351495379577e4bd426a93fd = 0;
		}
		else
		{
			c0766ee351495379577e4bd426a93fd = (ushort)(vector3F.X - 1f);
		}
		c7f587bd57d47897172f47483e6bcca68.c0766ee351495379577e4bd426a93fd76 = (ushort)c0766ee351495379577e4bd426a93fd;
		c7f587bd57d47897172f47483e6bcca68.ca908b2ef855c3e51cf79eb5717ba52da = (ushort)((vector3F.Y > 1f) ? ((ushort)(vector3F.Y - 1f)) : 0);
		c7f587bd57d47897172f47483e6bcca68.c2d1089e3916028efbe65eebdee0edea4 = (ushort)((vector3F.Z > 1f) ? ((ushort)(vector3F.Z - 1f)) : 0);
		int ca496a3c2e4033c7383700937a074c8be;
		if (!(vector3F2.X < 65534f))
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
			ca496a3c2e4033c7383700937a074c8be = 65535;
		}
		else
		{
			ca496a3c2e4033c7383700937a074c8be = (ushort)(vector3F2.X + 1f);
		}
		c7f587bd57d47897172f47483e6bcca68.ca496a3c2e4033c7383700937a074c8be = (ushort)ca496a3c2e4033c7383700937a074c8be;
		c7f587bd57d47897172f47483e6bcca68.c7d2a56de6e39281750b1f1c7a61a8868 = ((vector3F2.Y < 65534f) ? ((ushort)(vector3F2.Y + 1f)) : ushort.MaxValue);
		c7f587bd57d47897172f47483e6bcca68.c95b253ba97348b0005f6ccc969f9c2e3 = ((vector3F2.Z < 65534f) ? ((ushort)(vector3F2.Z + 1f)) : ushort.MaxValue);
	}

	private void ce759ff2781e7c0601193b90d63f12c3c()
	{
		if (!EnableSelfOverlaps)
		{
			return;
		}
		cdb565a0d0e484a399660c47a4ce9d1cf.Clear();
		if (cd26c0d77f8d7f857646d6821d2cb60a7 == null)
		{
			return;
		}
		c3be4e279c85d04da07f3693866750bf4[] array = cd26c0d77f8d7f857646d6821d2cb60a7;
		for (int i = 0; i < array.Length; i++)
		{
			c3be4e279c85d04da07f3693866750bf4 c7f587bd57d47897172f47483e6bcca = array[i];
			if (!c7f587bd57d47897172f47483e6bcca.cfd73e16b19bb8bde65db991a787eb5ad)
			{
				continue;
			}
			Aabb aabb = cccf609c8efd00ebcc2b1a12189fe5e64(c7f587bd57d47897172f47483e6bcca);
			IEnumerator<int> enumerator = GetOverlaps(aabb).GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					int current = enumerator.Current;
					Pair<int> pair = new Pair<int>(c7f587bd57d47897172f47483e6bcca.c652f269a40a2519b8658ffbade581b4e, current);
					if (!c8092bfef08eaf2732b73394233ee5fd1(pair))
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
						break;
					}
					if (1 == 0)
					{
						__ldtoken(CompressedAabbTree.ce759ff2781e7c0601193b90d63f12c3c());
					}
					cdb565a0d0e484a399660c47a4ce9d1cf.Add(pair);
				}
				while (true)
				{
					switch (6)
					{
					case 0:
						break;
					default:
						goto end_IL_00b5;
					}
					continue;
					end_IL_00b5:
					break;
				}
			}
			finally
			{
				if (enumerator != null)
				{
					while (true)
					{
						switch (7)
						{
						case 0:
							continue;
						}
						enumerator.Dispose();
						break;
					}
				}
			}
		}
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

	[CompilerGenerated]
	private static cbe5649b18b8b0caa00be9ad49188b193<int> c1dc20c8432225d1f85a30ea8c980fb81()
	{
		return new AabbTree<int>.caa41fe1fc008c4cd467f2caa714d2784();
	}

	[CompilerGenerated]
	private static bool cd46d8a678ce9ab8b03d1187d6e222df1(c3be4e279c85d04da07f3693866750bf4 c7f587bd57d47897172f47483e6bcca68)
	{
		return c7f587bd57d47897172f47483e6bcca68.cfd73e16b19bb8bde65db991a787eb5ad;
	}

	[CompilerGenerated]
	private static int c67b13d21dc55c7d63fbca57d02079b36(c3be4e279c85d04da07f3693866750bf4 c7f587bd57d47897172f47483e6bcca68)
	{
		return c7f587bd57d47897172f47483e6bcca68.c652f269a40a2519b8658ffbade581b4e;
	}
}
