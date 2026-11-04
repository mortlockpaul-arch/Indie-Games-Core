using System;
using System.Collections;
using System.Collections.Generic;
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
///       Represents a bounding volume tree using axis-aligned bounding boxes (AABBs).
///       </summary>
/// <typeparam name="T">The type of item in the spatial partition.</typeparam>
public class AabbTree<T> : BasePartition<T>, ISupportClosestPointQueries<T>
{
	internal class caa41fe1fc008c4cd467f2caa714d2784 : cbe5649b18b8b0caa00be9ad49188b193<T>
	{
		public Aabb c3e263e990bc8d6c3a2d636ac7a19c6c6;

		public caa41fe1fc008c4cd467f2caa714d2784 c6d3fb34101b9e1ce92055c2914d2d1de;

		public caa41fe1fc008c4cd467f2caa714d2784 c2d9d869d0c8fd5fb55f452375f580a4a;

		public T c652f269a40a2519b8658ffbade581b4e;

		public bool cfd73e16b19bb8bde65db991a787eb5ad
		{
			get
			{
				if (c6d3fb34101b9e1ce92055c2914d2d1de == null)
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
								__ldtoken(AabbTree<>.caa41fe1fc008c4cd467f2caa714d2784.get_cfd73e16b19bb8bde65db991a787eb5ad());
							}
							return c2d9d869d0c8fd5fb55f452375f580a4a == null;
						}
					}
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
				c6d3fb34101b9e1ce92055c2914d2d1de = (caa41fe1fc008c4cd467f2caa714d2784)value;
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
				c2d9d869d0c8fd5fb55f452375f580a4a = (caa41fe1fc008c4cd467f2caa714d2784)value;
			}
		}

		private cbe5649b18b8b0caa00be9ad49188b193<T> cfff16699b0ff91e60fab908ef0f2ea0b
		{
			get
			{
				throw new NotSupportedException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4826));
			}
		}

		private T c477d5bc87b57a983b9d9da4eab43eb34 => c652f269a40a2519b8658ffbade581b4e;
	}

	[CompilerGenerated]
	private sealed class c94689abd9d0357142662ab887d9573a2 : IEnumerable<T>, IEnumerator<T>, IEnumerable, IEnumerator, IDisposable
	{
		private T cd179d7f18da070d164af6afcb0af45b5;

		private int c94f69126adc708dfc5f5fd5b63c52835;

		private int c0e8a058e779916e7715d7616fb0da9e7;

		public AabbTree<T> c1b0bbfd4af1b6a4c0c71a5dd3c509812;

		public Aabb cdc3439410d15f28489c964ee87b88fe9;

		public Aabb cc3ae004bdecc8a5e2b7f6f0efd06d1d5;

		public Stack<caa41fe1fc008c4cd467f2caa714d2784> c29808ca85b477266efbd755d02c677b9;

		public caa41fe1fc008c4cd467f2caa714d2784 c87f28cfb1146d5c5d9f2c3f816d686ce;

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
		public c94689abd9d0357142662ab887d9573a2(int c94f69126adc708dfc5f5fd5b63c52835)
		{
			this.c94f69126adc708dfc5f5fd5b63c52835 = c94f69126adc708dfc5f5fd5b63c52835;
			c0e8a058e779916e7715d7616fb0da9e7 = Thread.CurrentThread.ManagedThreadId;
		}

		[DebuggerHidden]
		private IEnumerator<T> c0843ff4e059c4450bad00d5ba0b6ef48()
		{
			c94689abd9d0357142662ab887d9573a2 c94689abd9d0357142662ab887d9573a3;
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
					__ldtoken(AabbTree<>.c94689abd9d0357142662ab887d9573a2.c0843ff4e059c4450bad00d5ba0b6ef48());
				}
				if (c94f69126adc708dfc5f5fd5b63c52835 == -2)
				{
					c94f69126adc708dfc5f5fd5b63c52835 = 0;
					c94689abd9d0357142662ab887d9573a3 = this;
					goto IL_004d;
				}
			}
			c94689abd9d0357142662ab887d9573a3 = new c94689abd9d0357142662ab887d9573a2(0)
			{
				c1b0bbfd4af1b6a4c0c71a5dd3c509812 = c1b0bbfd4af1b6a4c0c71a5dd3c509812
			};
			goto IL_004d;
			IL_004d:
			c94689abd9d0357142662ab887d9573a3.cdc3439410d15f28489c964ee87b88fe9 = cc3ae004bdecc8a5e2b7f6f0efd06d1d5;
			return c94689abd9d0357142662ab887d9573a3;
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
				c29808ca85b477266efbd755d02c677b9 = ResourcePools<caa41fe1fc008c4cd467f2caa714d2784>.Stacks.Obtain();
				c29808ca85b477266efbd755d02c677b9.Push(c1b0bbfd4af1b6a4c0c71a5dd3c509812.c3cb2c0edc4d55dd1410e3483d1ef60d4);
				goto IL_0108;
			case 1:
				{
					c94f69126adc708dfc5f5fd5b63c52835 = -1;
					goto IL_0108;
				}
				IL_0108:
				while (c29808ca85b477266efbd755d02c677b9.Count > 0)
				{
					c87f28cfb1146d5c5d9f2c3f816d686ce = c29808ca85b477266efbd755d02c677b9.Pop();
					if (!GeometryHelper.HaveContact(c87f28cfb1146d5c5d9f2c3f816d686ce.c3e263e990bc8d6c3a2d636ac7a19c6c6, cdc3439410d15f28489c964ee87b88fe9))
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
						__ldtoken(AabbTree<>.c94689abd9d0357142662ab887d9573a2.MoveNext());
					}
					if (c87f28cfb1146d5c5d9f2c3f816d686ce.cfd73e16b19bb8bde65db991a787eb5ad)
					{
						cd179d7f18da070d164af6afcb0af45b5 = c87f28cfb1146d5c5d9f2c3f816d686ce.c652f269a40a2519b8658ffbade581b4e;
						c94f69126adc708dfc5f5fd5b63c52835 = 1;
						return true;
					}
					c29808ca85b477266efbd755d02c677b9.Push(c87f28cfb1146d5c5d9f2c3f816d686ce.c2d9d869d0c8fd5fb55f452375f580a4a);
					c29808ca85b477266efbd755d02c677b9.Push(c87f28cfb1146d5c5d9f2c3f816d686ce.c6d3fb34101b9e1ce92055c2914d2d1de);
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
				ResourcePools<caa41fe1fc008c4cd467f2caa714d2784>.Stacks.Recycle(c29808ca85b477266efbd755d02c677b9);
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
	private sealed class c3c692121b732ef2839be650b87e15b46 : IEnumerable<T>, IEnumerator<T>, IEnumerable, IEnumerator, IDisposable
	{
		private T cd179d7f18da070d164af6afcb0af45b5;

		private int c94f69126adc708dfc5f5fd5b63c52835;

		private int c0e8a058e779916e7715d7616fb0da9e7;

		public AabbTree<T> c1b0bbfd4af1b6a4c0c71a5dd3c509812;

		public Ray c2d769fcfe2c7e7844e7de692907680c8;

		public Ray c8b1773219813f762be9a1bd49ee5cb56;

		public Vector3F cc72cbee218b1b71c1ebc5bba79dd8e62;

		public float cccf79275d9fc29bb19d86e26afa26ee7;

		public Stack<caa41fe1fc008c4cd467f2caa714d2784> c3700f1b228723b1ba51387c3f22d5508;

		public caa41fe1fc008c4cd467f2caa714d2784 ca5742ac62a4fd7da123715c840110095;

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
		public c3c692121b732ef2839be650b87e15b46(int c94f69126adc708dfc5f5fd5b63c52835)
		{
			this.c94f69126adc708dfc5f5fd5b63c52835 = c94f69126adc708dfc5f5fd5b63c52835;
			c0e8a058e779916e7715d7616fb0da9e7 = Thread.CurrentThread.ManagedThreadId;
		}

		[DebuggerHidden]
		private IEnumerator<T> c0843ff4e059c4450bad00d5ba0b6ef48()
		{
			c3c692121b732ef2839be650b87e15b46 c3c692121b732ef2839be650b87e15b47;
			if (Thread.CurrentThread.ManagedThreadId == c0e8a058e779916e7715d7616fb0da9e7)
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
					__ldtoken(AabbTree<>.c3c692121b732ef2839be650b87e15b46.c0843ff4e059c4450bad00d5ba0b6ef48());
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
					c3c692121b732ef2839be650b87e15b47 = this;
					goto IL_005b;
				}
			}
			c3c692121b732ef2839be650b87e15b47 = new c3c692121b732ef2839be650b87e15b46(0)
			{
				c1b0bbfd4af1b6a4c0c71a5dd3c509812 = c1b0bbfd4af1b6a4c0c71a5dd3c509812
			};
			goto IL_005b;
			IL_005b:
			c3c692121b732ef2839be650b87e15b47.c2d769fcfe2c7e7844e7de692907680c8 = c8b1773219813f762be9a1bd49ee5cb56;
			return c3c692121b732ef2839be650b87e15b47;
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
					switch (4)
					{
					case 0:
						continue;
					}
					break;
				}
				if (1 == 0)
				{
					__ldtoken(AabbTree<>.c3c692121b732ef2839be650b87e15b46.MoveNext());
				}
				ref Vector3F reference = ref cc72cbee218b1b71c1ebc5bba79dd8e62;
				reference = new Vector3F(1f / c2d769fcfe2c7e7844e7de692907680c8.Direction.X, 1f / c2d769fcfe2c7e7844e7de692907680c8.Direction.Y, 1f / c2d769fcfe2c7e7844e7de692907680c8.Direction.Z);
				cccf79275d9fc29bb19d86e26afa26ee7 = Numeric.EpsilonF * (1f + c1b0bbfd4af1b6a4c0c71a5dd3c509812.Aabb.Extent.Length);
				c3700f1b228723b1ba51387c3f22d5508 = ResourcePools<caa41fe1fc008c4cd467f2caa714d2784>.Stacks.Obtain();
				c3700f1b228723b1ba51387c3f22d5508.Push(c1b0bbfd4af1b6a4c0c71a5dd3c509812.c3cb2c0edc4d55dd1410e3483d1ef60d4);
				goto IL_01b3;
			}
			case 1:
				{
					c94f69126adc708dfc5f5fd5b63c52835 = -1;
					goto IL_01b3;
				}
				IL_01b3:
				while (c3700f1b228723b1ba51387c3f22d5508.Count > 0)
				{
					ca5742ac62a4fd7da123715c840110095 = c3700f1b228723b1ba51387c3f22d5508.Pop();
					if (!GeometryHelper.cdd6d17271fec83883494ba8e43eede6a(ca5742ac62a4fd7da123715c840110095.c3e263e990bc8d6c3a2d636ac7a19c6c6, c2d769fcfe2c7e7844e7de692907680c8.Origin, cc72cbee218b1b71c1ebc5bba79dd8e62, c2d769fcfe2c7e7844e7de692907680c8.Length, cccf79275d9fc29bb19d86e26afa26ee7))
					{
						continue;
					}
					if (ca5742ac62a4fd7da123715c840110095.cfd73e16b19bb8bde65db991a787eb5ad)
					{
						while (true)
						{
							switch (4)
							{
							case 0:
								break;
							default:
								cd179d7f18da070d164af6afcb0af45b5 = ca5742ac62a4fd7da123715c840110095.c652f269a40a2519b8658ffbade581b4e;
								c94f69126adc708dfc5f5fd5b63c52835 = 1;
								return true;
							}
						}
					}
					c3700f1b228723b1ba51387c3f22d5508.Push(ca5742ac62a4fd7da123715c840110095.c2d9d869d0c8fd5fb55f452375f580a4a);
					c3700f1b228723b1ba51387c3f22d5508.Push(ca5742ac62a4fd7da123715c840110095.c6d3fb34101b9e1ce92055c2914d2d1de);
				}
				ResourcePools<caa41fe1fc008c4cd467f2caa714d2784>.Stacks.Recycle(c3700f1b228723b1ba51387c3f22d5508);
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
	private sealed class ce0ba145385131c34e19847f81ed651e5 : IEnumerable<Pair<T>>, IEnumerator<Pair<T>>, IEnumerable, IEnumerator, IDisposable
	{
		private Pair<T> cd179d7f18da070d164af6afcb0af45b5;

		private int c94f69126adc708dfc5f5fd5b63c52835;

		private int c0e8a058e779916e7715d7616fb0da9e7;

		public AabbTree<T> c1b0bbfd4af1b6a4c0c71a5dd3c509812;

		public ISpatialPartition<T> cfd8883387afb9cc25a2ac9150de0f66b;

		public ISpatialPartition<T> cd1939b65df62ed5f7a2112466dabd8f0;

		public caa41fe1fc008c4cd467f2caa714d2784 c3a154ab027058cbc3456837d5401bdd3;

		public IEnumerable<T> cfb7f1b17163069a238f6ee93b55b7a09;

		public T ce9888132c88687c48fe34d7186bc8bc2;

		public Pair<T> c49c381c42e8098b499d0f80db9f5b6b1;

		public IEnumerator<caa41fe1fc008c4cd467f2caa714d2784> c731dfa26744967911ea1470dc372ff58;

		public IEnumerator<T> c413e672bb8901519262a34b9e489011e;

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
		public ce0ba145385131c34e19847f81ed651e5(int c94f69126adc708dfc5f5fd5b63c52835)
		{
			this.c94f69126adc708dfc5f5fd5b63c52835 = c94f69126adc708dfc5f5fd5b63c52835;
			c0e8a058e779916e7715d7616fb0da9e7 = Thread.CurrentThread.ManagedThreadId;
		}

		[DebuggerHidden]
		private IEnumerator<Pair<T>> c4bd63b962f346f189f5c6a4ad2e4ce8d()
		{
			ce0ba145385131c34e19847f81ed651e5 ce0ba145385131c34e19847f81ed651e6;
			if (Thread.CurrentThread.ManagedThreadId == c0e8a058e779916e7715d7616fb0da9e7 && c94f69126adc708dfc5f5fd5b63c52835 == -2)
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
					__ldtoken(AabbTree<>.ce0ba145385131c34e19847f81ed651e5.c4bd63b962f346f189f5c6a4ad2e4ce8d());
				}
				c94f69126adc708dfc5f5fd5b63c52835 = 0;
				ce0ba145385131c34e19847f81ed651e6 = this;
			}
			else
			{
				ce0ba145385131c34e19847f81ed651e6 = new ce0ba145385131c34e19847f81ed651e5(0)
				{
					c1b0bbfd4af1b6a4c0c71a5dd3c509812 = c1b0bbfd4af1b6a4c0c71a5dd3c509812
				};
			}
			ce0ba145385131c34e19847f81ed651e6.cfd8883387afb9cc25a2ac9150de0f66b = cd1939b65df62ed5f7a2112466dabd8f0;
			return ce0ba145385131c34e19847f81ed651e6;
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
						switch (5)
						{
						case 0:
							continue;
						}
						break;
					}
					if (1 == 0)
					{
						__ldtoken(AabbTree<>.ce0ba145385131c34e19847f81ed651e5.MoveNext());
					}
					if (num != 3)
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
						goto IL_017b;
					}
					c94f69126adc708dfc5f5fd5b63c52835 = 2;
					goto IL_0137;
				}
				c94f69126adc708dfc5f5fd5b63c52835 = -1;
				c731dfa26744967911ea1470dc372ff58 = c1b0bbfd4af1b6a4c0c71a5dd3c509812.c4d76d858205a75f799980f42e41cbe5b(cfd8883387afb9cc25a2ac9150de0f66b.Aabb).GetEnumerator();
				c94f69126adc708dfc5f5fd5b63c52835 = 1;
				goto IL_0159;
				IL_0137:
				while (c413e672bb8901519262a34b9e489011e.MoveNext())
				{
					ce9888132c88687c48fe34d7186bc8bc2 = c413e672bb8901519262a34b9e489011e.Current;
					ref Pair<T> reference = ref c49c381c42e8098b499d0f80db9f5b6b1;
					reference = new Pair<T>(c3a154ab027058cbc3456837d5401bdd3.c652f269a40a2519b8658ffbade581b4e, ce9888132c88687c48fe34d7186bc8bc2);
					if (c1b0bbfd4af1b6a4c0c71a5dd3c509812.Filter == null || c1b0bbfd4af1b6a4c0c71a5dd3c509812.Filter.Filter(c49c381c42e8098b499d0f80db9f5b6b1))
					{
						cd179d7f18da070d164af6afcb0af45b5 = c49c381c42e8098b499d0f80db9f5b6b1;
						c94f69126adc708dfc5f5fd5b63c52835 = 3;
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
					break;
				}
				c07d51b9e43033bdb7b8130acd0cc0211();
				goto IL_0159;
				IL_017b:
				return false;
				IL_0159:
				if (c731dfa26744967911ea1470dc372ff58.MoveNext())
				{
					c3a154ab027058cbc3456837d5401bdd3 = c731dfa26744967911ea1470dc372ff58.Current;
					cfb7f1b17163069a238f6ee93b55b7a09 = cfd8883387afb9cc25a2ac9150de0f66b.GetOverlaps(c3a154ab027058cbc3456837d5401bdd3.c3e263e990bc8d6c3a2d636ac7a19c6c6);
					c413e672bb8901519262a34b9e489011e = cfb7f1b17163069a238f6ee93b55b7a09.GetEnumerator();
					c94f69126adc708dfc5f5fd5b63c52835 = 2;
					goto IL_0137;
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
				cc29524e92fe00103fbb53aa07e2671dd();
				goto IL_017b;
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
							c07d51b9e43033bdb7b8130acd0cc0211();
						}
					}
					break;
				}
				finally
				{
					cc29524e92fe00103fbb53aa07e2671dd();
				}
			}
		}

		void IDisposable.Dispose()
		{
			//ILSpy generated this explicit interface implementation from .override directive in c087ddb9300ad4d53672b7a60e6d76672
			this.c087ddb9300ad4d53672b7a60e6d76672();
		}

		private void cc29524e92fe00103fbb53aa07e2671dd()
		{
			c94f69126adc708dfc5f5fd5b63c52835 = -1;
			if (c731dfa26744967911ea1470dc372ff58 != null)
			{
				c731dfa26744967911ea1470dc372ff58.Dispose();
			}
		}

		private void c07d51b9e43033bdb7b8130acd0cc0211()
		{
			c94f69126adc708dfc5f5fd5b63c52835 = 1;
			if (c413e672bb8901519262a34b9e489011e != null)
			{
				c413e672bb8901519262a34b9e489011e.Dispose();
			}
		}
	}

	[CompilerGenerated]
	private sealed class ca6428fbae1d5eb16a72015f2bbc2feae : IEnumerable<Pair<T>>, IEnumerator<Pair<T>>, IEnumerable, IEnumerator, IDisposable
	{
		private Pair<T> cd179d7f18da070d164af6afcb0af45b5;

		private int c94f69126adc708dfc5f5fd5b63c52835;

		private int c0e8a058e779916e7715d7616fb0da9e7;

		public AabbTree<T> c1b0bbfd4af1b6a4c0c71a5dd3c509812;

		public AabbTree<T> ca9802b6266a4b983f751fb2d730561ca;

		public AabbTree<T> c6b71d229c51c908f777d8166aeaf9947;

		public Stack<Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784>> cfdec9b982e8000c0a0382cc09e8c18c8;

		public Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784> c120bd69cf57889f52a1d63ed8576a502;

		public caa41fe1fc008c4cd467f2caa714d2784 c5ab5a69531d0a6ab0e47ff447eb8968b;

		public caa41fe1fc008c4cd467f2caa714d2784 c58e30274351086c3a723d8d3f33ec3f7;

		public Pair<T> cf0bdd1788658255670b654ade420cf58;

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
		public ca6428fbae1d5eb16a72015f2bbc2feae(int c94f69126adc708dfc5f5fd5b63c52835)
		{
			this.c94f69126adc708dfc5f5fd5b63c52835 = c94f69126adc708dfc5f5fd5b63c52835;
			c0e8a058e779916e7715d7616fb0da9e7 = Thread.CurrentThread.ManagedThreadId;
		}

		[DebuggerHidden]
		private IEnumerator<Pair<T>> c4bd63b962f346f189f5c6a4ad2e4ce8d()
		{
			ca6428fbae1d5eb16a72015f2bbc2feae ca6428fbae1d5eb16a72015f2bbc2feae2;
			if (Thread.CurrentThread.ManagedThreadId == c0e8a058e779916e7715d7616fb0da9e7 && c94f69126adc708dfc5f5fd5b63c52835 == -2)
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
					__ldtoken(AabbTree<>.ca6428fbae1d5eb16a72015f2bbc2feae.c4bd63b962f346f189f5c6a4ad2e4ce8d());
				}
				c94f69126adc708dfc5f5fd5b63c52835 = 0;
				ca6428fbae1d5eb16a72015f2bbc2feae2 = this;
			}
			else
			{
				ca6428fbae1d5eb16a72015f2bbc2feae2 = new ca6428fbae1d5eb16a72015f2bbc2feae(0)
				{
					c1b0bbfd4af1b6a4c0c71a5dd3c509812 = c1b0bbfd4af1b6a4c0c71a5dd3c509812
				};
			}
			ca6428fbae1d5eb16a72015f2bbc2feae2.ca9802b6266a4b983f751fb2d730561ca = c6b71d229c51c908f777d8166aeaf9947;
			return ca6428fbae1d5eb16a72015f2bbc2feae2;
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
				cfdec9b982e8000c0a0382cc09e8c18c8 = ResourcePools<Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784>>.Stacks.Obtain();
				cfdec9b982e8000c0a0382cc09e8c18c8.Push(new Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784>(c1b0bbfd4af1b6a4c0c71a5dd3c509812.c3cb2c0edc4d55dd1410e3483d1ef60d4, ca9802b6266a4b983f751fb2d730561ca.c3cb2c0edc4d55dd1410e3483d1ef60d4));
				goto IL_0330;
			case 1:
				{
					c94f69126adc708dfc5f5fd5b63c52835 = -1;
					goto IL_0330;
				}
				IL_0330:
				while (cfdec9b982e8000c0a0382cc09e8c18c8.Count > 0)
				{
					c120bd69cf57889f52a1d63ed8576a502 = cfdec9b982e8000c0a0382cc09e8c18c8.Pop();
					c5ab5a69531d0a6ab0e47ff447eb8968b = c120bd69cf57889f52a1d63ed8576a502.First;
					c58e30274351086c3a723d8d3f33ec3f7 = c120bd69cf57889f52a1d63ed8576a502.Second;
					if (c5ab5a69531d0a6ab0e47ff447eb8968b == c58e30274351086c3a723d8d3f33ec3f7)
					{
						if (!c5ab5a69531d0a6ab0e47ff447eb8968b.cfd73e16b19bb8bde65db991a787eb5ad)
						{
							cfdec9b982e8000c0a0382cc09e8c18c8.Push(new Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784>(c5ab5a69531d0a6ab0e47ff447eb8968b.c2d9d869d0c8fd5fb55f452375f580a4a, c5ab5a69531d0a6ab0e47ff447eb8968b.c2d9d869d0c8fd5fb55f452375f580a4a));
							cfdec9b982e8000c0a0382cc09e8c18c8.Push(new Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784>(c5ab5a69531d0a6ab0e47ff447eb8968b.c6d3fb34101b9e1ce92055c2914d2d1de, c5ab5a69531d0a6ab0e47ff447eb8968b.c2d9d869d0c8fd5fb55f452375f580a4a));
							cfdec9b982e8000c0a0382cc09e8c18c8.Push(new Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784>(c5ab5a69531d0a6ab0e47ff447eb8968b.c6d3fb34101b9e1ce92055c2914d2d1de, c5ab5a69531d0a6ab0e47ff447eb8968b.c6d3fb34101b9e1ce92055c2914d2d1de));
						}
					}
					else
					{
						if (!GeometryHelper.HaveContact(c5ab5a69531d0a6ab0e47ff447eb8968b.c3e263e990bc8d6c3a2d636ac7a19c6c6, c58e30274351086c3a723d8d3f33ec3f7.c3e263e990bc8d6c3a2d636ac7a19c6c6))
						{
							continue;
						}
						if (!c5ab5a69531d0a6ab0e47ff447eb8968b.cfd73e16b19bb8bde65db991a787eb5ad)
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
								__ldtoken(AabbTree<>.ca6428fbae1d5eb16a72015f2bbc2feae.MoveNext());
							}
							if (!c58e30274351086c3a723d8d3f33ec3f7.cfd73e16b19bb8bde65db991a787eb5ad)
							{
								cfdec9b982e8000c0a0382cc09e8c18c8.Push(new Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784>(c5ab5a69531d0a6ab0e47ff447eb8968b.c2d9d869d0c8fd5fb55f452375f580a4a, c58e30274351086c3a723d8d3f33ec3f7.c2d9d869d0c8fd5fb55f452375f580a4a));
								cfdec9b982e8000c0a0382cc09e8c18c8.Push(new Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784>(c5ab5a69531d0a6ab0e47ff447eb8968b.c6d3fb34101b9e1ce92055c2914d2d1de, c58e30274351086c3a723d8d3f33ec3f7.c2d9d869d0c8fd5fb55f452375f580a4a));
								cfdec9b982e8000c0a0382cc09e8c18c8.Push(new Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784>(c5ab5a69531d0a6ab0e47ff447eb8968b.c2d9d869d0c8fd5fb55f452375f580a4a, c58e30274351086c3a723d8d3f33ec3f7.c6d3fb34101b9e1ce92055c2914d2d1de));
								cfdec9b982e8000c0a0382cc09e8c18c8.Push(new Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784>(c5ab5a69531d0a6ab0e47ff447eb8968b.c6d3fb34101b9e1ce92055c2914d2d1de, c58e30274351086c3a723d8d3f33ec3f7.c6d3fb34101b9e1ce92055c2914d2d1de));
							}
							else
							{
								cfdec9b982e8000c0a0382cc09e8c18c8.Push(new Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784>(c5ab5a69531d0a6ab0e47ff447eb8968b.c2d9d869d0c8fd5fb55f452375f580a4a, c58e30274351086c3a723d8d3f33ec3f7));
								cfdec9b982e8000c0a0382cc09e8c18c8.Push(new Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784>(c5ab5a69531d0a6ab0e47ff447eb8968b.c6d3fb34101b9e1ce92055c2914d2d1de, c58e30274351086c3a723d8d3f33ec3f7));
							}
						}
						else if (!c58e30274351086c3a723d8d3f33ec3f7.cfd73e16b19bb8bde65db991a787eb5ad)
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
							cfdec9b982e8000c0a0382cc09e8c18c8.Push(new Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784>(c5ab5a69531d0a6ab0e47ff447eb8968b, c58e30274351086c3a723d8d3f33ec3f7.c2d9d869d0c8fd5fb55f452375f580a4a));
							cfdec9b982e8000c0a0382cc09e8c18c8.Push(new Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784>(c5ab5a69531d0a6ab0e47ff447eb8968b, c58e30274351086c3a723d8d3f33ec3f7.c6d3fb34101b9e1ce92055c2914d2d1de));
						}
						else
						{
							ref Pair<T> reference = ref cf0bdd1788658255670b654ade420cf58;
							reference = new Pair<T>(c5ab5a69531d0a6ab0e47ff447eb8968b.c652f269a40a2519b8658ffbade581b4e, c58e30274351086c3a723d8d3f33ec3f7.c652f269a40a2519b8658ffbade581b4e);
							if (c1b0bbfd4af1b6a4c0c71a5dd3c509812.Filter == null || c1b0bbfd4af1b6a4c0c71a5dd3c509812.Filter.Filter(cf0bdd1788658255670b654ade420cf58))
							{
								cd179d7f18da070d164af6afcb0af45b5 = cf0bdd1788658255670b654ade420cf58;
								c94f69126adc708dfc5f5fd5b63c52835 = 1;
								return true;
							}
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
				ResourcePools<Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784>>.Stacks.Recycle(cfdec9b982e8000c0a0382cc09e8c18c8);
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
	private sealed class c23c9ec162924befc07131ab8b16d9cdb : IEnumerable<Pair<T>>, IEnumerator<Pair<T>>, IEnumerable, IEnumerator, IDisposable
	{
		private Pair<T> cd179d7f18da070d164af6afcb0af45b5;

		private int c94f69126adc708dfc5f5fd5b63c52835;

		private int c0e8a058e779916e7715d7616fb0da9e7;

		public AabbTree<T> c1b0bbfd4af1b6a4c0c71a5dd3c509812;

		public Vector3F ceabb47d7d959545d89c4300eabbdd7c2;

		public Vector3F c6f203721b629fec4c93cc63ff1172fc4;

		public ISpatialPartition<T> cfd8883387afb9cc25a2ac9150de0f66b;

		public ISpatialPartition<T> cd1939b65df62ed5f7a2112466dabd8f0;

		public Vector3F c1b4918d9d3cc94171c67339d1fc78b0f;

		public Vector3F cfd54ba1e25c70994847ab953f2228207;

		public Pose cd2abebf7ff6ec7d035c28a04faaa459b;

		public Pose c1c245528512f0ae5611e6e833a70158f;

		public Vector3F c0efc1077d03e4d81a4363dde33d12244;

		public Vector3F cd5406811a1af6516104867ad65db181d;

		public Pose c37acfffd061282c628a01ecdba23de94;

		public Pose cbe28b956ce4033f19b95bbf80c514425;

		public Aabb c0cce8583c29a6dae051667cdb9762aeb;

		public caa41fe1fc008c4cd467f2caa714d2784 ce973ce02972e97a408da30339b6e16fd;

		public Aabb c6623c3617da5ac235d708d90f6a7f887;

		public T c6ed704099b74155d8d1303f784052c29;

		public Pair<T> cea95aac3ea48da552b7f685d6fd9c1e3;

		public IEnumerator<caa41fe1fc008c4cd467f2caa714d2784> c7f144bcea61e94aeae7767a0fba94f25;

		public IEnumerator<T> c9e7d5643c48037153a5a2f3c7387b95c;

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
		public c23c9ec162924befc07131ab8b16d9cdb(int c94f69126adc708dfc5f5fd5b63c52835)
		{
			this.c94f69126adc708dfc5f5fd5b63c52835 = c94f69126adc708dfc5f5fd5b63c52835;
			c0e8a058e779916e7715d7616fb0da9e7 = Thread.CurrentThread.ManagedThreadId;
		}

		[DebuggerHidden]
		private IEnumerator<Pair<T>> c4bd63b962f346f189f5c6a4ad2e4ce8d()
		{
			c23c9ec162924befc07131ab8b16d9cdb c23c9ec162924befc07131ab8b16d9cdb2;
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
					__ldtoken(AabbTree<>.c23c9ec162924befc07131ab8b16d9cdb.c4bd63b962f346f189f5c6a4ad2e4ce8d());
				}
				if (c94f69126adc708dfc5f5fd5b63c52835 == -2)
				{
					c94f69126adc708dfc5f5fd5b63c52835 = 0;
					c23c9ec162924befc07131ab8b16d9cdb2 = this;
					goto IL_004f;
				}
			}
			c23c9ec162924befc07131ab8b16d9cdb2 = new c23c9ec162924befc07131ab8b16d9cdb(0)
			{
				c1b0bbfd4af1b6a4c0c71a5dd3c509812 = c1b0bbfd4af1b6a4c0c71a5dd3c509812
			};
			goto IL_004f;
			IL_004f:
			c23c9ec162924befc07131ab8b16d9cdb2.ceabb47d7d959545d89c4300eabbdd7c2 = c6f203721b629fec4c93cc63ff1172fc4;
			c23c9ec162924befc07131ab8b16d9cdb2.cfd8883387afb9cc25a2ac9150de0f66b = cd1939b65df62ed5f7a2112466dabd8f0;
			c23c9ec162924befc07131ab8b16d9cdb2.c1b4918d9d3cc94171c67339d1fc78b0f = cfd54ba1e25c70994847ab953f2228207;
			c23c9ec162924befc07131ab8b16d9cdb2.cd2abebf7ff6ec7d035c28a04faaa459b = c1c245528512f0ae5611e6e833a70158f;
			return c23c9ec162924befc07131ab8b16d9cdb2;
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
						goto IL_0212;
					}
					c94f69126adc708dfc5f5fd5b63c52835 = 2;
					goto IL_01da;
				}
				c94f69126adc708dfc5f5fd5b63c52835 = -1;
				c0efc1077d03e4d81a4363dde33d12244 = Vector3F.One / ceabb47d7d959545d89c4300eabbdd7c2;
				cd5406811a1af6516104867ad65db181d = Vector3F.One / c1b4918d9d3cc94171c67339d1fc78b0f;
				c37acfffd061282c628a01ecdba23de94 = cd2abebf7ff6ec7d035c28a04faaa459b;
				cbe28b956ce4033f19b95bbf80c514425 = cd2abebf7ff6ec7d035c28a04faaa459b.Inverse;
				c0cce8583c29a6dae051667cdb9762aeb = cfd8883387afb9cc25a2ac9150de0f66b.Aabb;
				c0cce8583c29a6dae051667cdb9762aeb = c0cce8583c29a6dae051667cdb9762aeb.GetAabb(c1b4918d9d3cc94171c67339d1fc78b0f, c37acfffd061282c628a01ecdba23de94);
				c0cce8583c29a6dae051667cdb9762aeb.Scale(c0efc1077d03e4d81a4363dde33d12244);
				c7f144bcea61e94aeae7767a0fba94f25 = c1b0bbfd4af1b6a4c0c71a5dd3c509812.c4d76d858205a75f799980f42e41cbe5b(c0cce8583c29a6dae051667cdb9762aeb).GetEnumerator();
				c94f69126adc708dfc5f5fd5b63c52835 = 1;
				goto IL_01f0;
				IL_01f0:
				if (c7f144bcea61e94aeae7767a0fba94f25.MoveNext())
				{
					ce973ce02972e97a408da30339b6e16fd = c7f144bcea61e94aeae7767a0fba94f25.Current;
					c6623c3617da5ac235d708d90f6a7f887 = ce973ce02972e97a408da30339b6e16fd.c3e263e990bc8d6c3a2d636ac7a19c6c6.GetAabb(ceabb47d7d959545d89c4300eabbdd7c2, cbe28b956ce4033f19b95bbf80c514425);
					c6623c3617da5ac235d708d90f6a7f887.Scale(cd5406811a1af6516104867ad65db181d);
					c9e7d5643c48037153a5a2f3c7387b95c = cfd8883387afb9cc25a2ac9150de0f66b.GetOverlaps(c6623c3617da5ac235d708d90f6a7f887).GetEnumerator();
					c94f69126adc708dfc5f5fd5b63c52835 = 2;
					goto IL_01da;
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
				c24da2bb11edef9366fe96ba0c1f521c4();
				goto IL_0212;
				IL_01da:
				while (c9e7d5643c48037153a5a2f3c7387b95c.MoveNext())
				{
					c6ed704099b74155d8d1303f784052c29 = c9e7d5643c48037153a5a2f3c7387b95c.Current;
					ref Pair<T> reference = ref cea95aac3ea48da552b7f685d6fd9c1e3;
					reference = new Pair<T>(ce973ce02972e97a408da30339b6e16fd.c652f269a40a2519b8658ffbade581b4e, c6ed704099b74155d8d1303f784052c29);
					if (c1b0bbfd4af1b6a4c0c71a5dd3c509812.Filter != null)
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
								__ldtoken(AabbTree<>.c23c9ec162924befc07131ab8b16d9cdb.MoveNext());
							}
							if (!c1b0bbfd4af1b6a4c0c71a5dd3c509812.Filter.Filter(cea95aac3ea48da552b7f685d6fd9c1e3))
							{
								break;
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
							goto IL_01bc;
						}
						continue;
					}
					goto IL_01bc;
					IL_01bc:
					cd179d7f18da070d164af6afcb0af45b5 = cea95aac3ea48da552b7f685d6fd9c1e3;
					c94f69126adc708dfc5f5fd5b63c52835 = 3;
					return true;
				}
				ca0a60671becd1e0cef8a51aa281f5da6();
				goto IL_01f0;
				IL_0212:
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
							ca0a60671becd1e0cef8a51aa281f5da6();
						}
					}
					break;
				}
				finally
				{
					c24da2bb11edef9366fe96ba0c1f521c4();
				}
			}
		}

		void IDisposable.Dispose()
		{
			//ILSpy generated this explicit interface implementation from .override directive in c087ddb9300ad4d53672b7a60e6d76672
			this.c087ddb9300ad4d53672b7a60e6d76672();
		}

		private void c24da2bb11edef9366fe96ba0c1f521c4()
		{
			c94f69126adc708dfc5f5fd5b63c52835 = -1;
			if (c7f144bcea61e94aeae7767a0fba94f25 == null)
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
				if (1 == 0)
				{
					__ldtoken(AabbTree<>.c23c9ec162924befc07131ab8b16d9cdb.c24da2bb11edef9366fe96ba0c1f521c4());
				}
				c7f144bcea61e94aeae7767a0fba94f25.Dispose();
				return;
			}
		}

		private void ca0a60671becd1e0cef8a51aa281f5da6()
		{
			c94f69126adc708dfc5f5fd5b63c52835 = 1;
			if (c9e7d5643c48037153a5a2f3c7387b95c == null)
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
					__ldtoken(AabbTree<>.c23c9ec162924befc07131ab8b16d9cdb.ca0a60671becd1e0cef8a51aa281f5da6());
				}
				c9e7d5643c48037153a5a2f3c7387b95c.Dispose();
				return;
			}
		}
	}

	[CompilerGenerated]
	private sealed class c6aad3e71937a02f95c6602cffe97bbce : IEnumerable<Pair<T>>, IEnumerator<Pair<T>>, IEnumerable, IEnumerator, IDisposable
	{
		private Pair<T> cd179d7f18da070d164af6afcb0af45b5;

		private int c94f69126adc708dfc5f5fd5b63c52835;

		private int c0e8a058e779916e7715d7616fb0da9e7;

		public AabbTree<T> c1b0bbfd4af1b6a4c0c71a5dd3c509812;

		public Vector3F ceabb47d7d959545d89c4300eabbdd7c2;

		public Vector3F c6f203721b629fec4c93cc63ff1172fc4;

		public AabbTree<T> ca9802b6266a4b983f751fb2d730561ca;

		public AabbTree<T> c6b71d229c51c908f777d8166aeaf9947;

		public Vector3F c1b4918d9d3cc94171c67339d1fc78b0f;

		public Vector3F cfd54ba1e25c70994847ab953f2228207;

		public Pose cd2abebf7ff6ec7d035c28a04faaa459b;

		public Pose c1c245528512f0ae5611e6e833a70158f;

		public Vector3F cc7f6008f499a1e624379c6e48eb50231;

		public Vector3F cba6f40a65033c471f0e89e8472c0c42d;

		public Pose c6ef49c74368ffe80b719026aa9ef05df;

		public Stack<Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784>> ce09f3b34646e71633f1c5935ca95b5fa;

		public Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784> ccaaf2f6e3390b936304984e958aae9af;

		public caa41fe1fc008c4cd467f2caa714d2784 c7b9c4222e8fb6a8910f4ce86cb2ffc0a;

		public caa41fe1fc008c4cd467f2caa714d2784 c8e3f7610a8a457b891ed359a0101f2b4;

		public Pair<T> c93cf7cdde6c9548296d1a61f5bcdff26;

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
		public c6aad3e71937a02f95c6602cffe97bbce(int c94f69126adc708dfc5f5fd5b63c52835)
		{
			this.c94f69126adc708dfc5f5fd5b63c52835 = c94f69126adc708dfc5f5fd5b63c52835;
			c0e8a058e779916e7715d7616fb0da9e7 = Thread.CurrentThread.ManagedThreadId;
		}

		[DebuggerHidden]
		private IEnumerator<Pair<T>> c4bd63b962f346f189f5c6a4ad2e4ce8d()
		{
			c6aad3e71937a02f95c6602cffe97bbce c6aad3e71937a02f95c6602cffe97bbce2;
			if (Thread.CurrentThread.ManagedThreadId == c0e8a058e779916e7715d7616fb0da9e7 && c94f69126adc708dfc5f5fd5b63c52835 == -2)
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
					__ldtoken(AabbTree<>.c6aad3e71937a02f95c6602cffe97bbce.c4bd63b962f346f189f5c6a4ad2e4ce8d());
				}
				c94f69126adc708dfc5f5fd5b63c52835 = 0;
				c6aad3e71937a02f95c6602cffe97bbce2 = this;
			}
			else
			{
				c6aad3e71937a02f95c6602cffe97bbce2 = new c6aad3e71937a02f95c6602cffe97bbce(0)
				{
					c1b0bbfd4af1b6a4c0c71a5dd3c509812 = c1b0bbfd4af1b6a4c0c71a5dd3c509812
				};
			}
			c6aad3e71937a02f95c6602cffe97bbce2.ceabb47d7d959545d89c4300eabbdd7c2 = c6f203721b629fec4c93cc63ff1172fc4;
			c6aad3e71937a02f95c6602cffe97bbce2.ca9802b6266a4b983f751fb2d730561ca = c6b71d229c51c908f777d8166aeaf9947;
			c6aad3e71937a02f95c6602cffe97bbce2.c1b4918d9d3cc94171c67339d1fc78b0f = cfd54ba1e25c70994847ab953f2228207;
			c6aad3e71937a02f95c6602cffe97bbce2.cd2abebf7ff6ec7d035c28a04faaa459b = c1c245528512f0ae5611e6e833a70158f;
			return c6aad3e71937a02f95c6602cffe97bbce2;
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
				cc7f6008f499a1e624379c6e48eb50231 = ceabb47d7d959545d89c4300eabbdd7c2;
				cba6f40a65033c471f0e89e8472c0c42d = c1b4918d9d3cc94171c67339d1fc78b0f;
				c6ef49c74368ffe80b719026aa9ef05df = cd2abebf7ff6ec7d035c28a04faaa459b;
				ce09f3b34646e71633f1c5935ca95b5fa = ResourcePools<Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784>>.Stacks.Obtain();
				ce09f3b34646e71633f1c5935ca95b5fa.Push(new Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784>(c1b0bbfd4af1b6a4c0c71a5dd3c509812.c3cb2c0edc4d55dd1410e3483d1ef60d4, ca9802b6266a4b983f751fb2d730561ca.c3cb2c0edc4d55dd1410e3483d1ef60d4));
				goto IL_02c8;
			case 1:
				{
					c94f69126adc708dfc5f5fd5b63c52835 = -1;
					goto IL_02c8;
				}
				IL_02c8:
				while (ce09f3b34646e71633f1c5935ca95b5fa.Count > 0)
				{
					ccaaf2f6e3390b936304984e958aae9af = ce09f3b34646e71633f1c5935ca95b5fa.Pop();
					c7b9c4222e8fb6a8910f4ce86cb2ffc0a = ccaaf2f6e3390b936304984e958aae9af.First;
					c8e3f7610a8a457b891ed359a0101f2b4 = ccaaf2f6e3390b936304984e958aae9af.Second;
					if (!AabbTree<T>.cd0cfe897426aab73c90b8217df390607(c7b9c4222e8fb6a8910f4ce86cb2ffc0a, cc7f6008f499a1e624379c6e48eb50231, c8e3f7610a8a457b891ed359a0101f2b4, cba6f40a65033c471f0e89e8472c0c42d, c6ef49c74368ffe80b719026aa9ef05df))
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
						__ldtoken(AabbTree<>.c6aad3e71937a02f95c6602cffe97bbce.MoveNext());
					}
					if (!c7b9c4222e8fb6a8910f4ce86cb2ffc0a.cfd73e16b19bb8bde65db991a787eb5ad)
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
						if (!c8e3f7610a8a457b891ed359a0101f2b4.cfd73e16b19bb8bde65db991a787eb5ad)
						{
							ce09f3b34646e71633f1c5935ca95b5fa.Push(new Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784>(c7b9c4222e8fb6a8910f4ce86cb2ffc0a.c2d9d869d0c8fd5fb55f452375f580a4a, c8e3f7610a8a457b891ed359a0101f2b4.c2d9d869d0c8fd5fb55f452375f580a4a));
							ce09f3b34646e71633f1c5935ca95b5fa.Push(new Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784>(c7b9c4222e8fb6a8910f4ce86cb2ffc0a.c6d3fb34101b9e1ce92055c2914d2d1de, c8e3f7610a8a457b891ed359a0101f2b4.c2d9d869d0c8fd5fb55f452375f580a4a));
							ce09f3b34646e71633f1c5935ca95b5fa.Push(new Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784>(c7b9c4222e8fb6a8910f4ce86cb2ffc0a.c2d9d869d0c8fd5fb55f452375f580a4a, c8e3f7610a8a457b891ed359a0101f2b4.c6d3fb34101b9e1ce92055c2914d2d1de));
							ce09f3b34646e71633f1c5935ca95b5fa.Push(new Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784>(c7b9c4222e8fb6a8910f4ce86cb2ffc0a.c6d3fb34101b9e1ce92055c2914d2d1de, c8e3f7610a8a457b891ed359a0101f2b4.c6d3fb34101b9e1ce92055c2914d2d1de));
						}
						else
						{
							ce09f3b34646e71633f1c5935ca95b5fa.Push(new Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784>(c7b9c4222e8fb6a8910f4ce86cb2ffc0a.c2d9d869d0c8fd5fb55f452375f580a4a, c8e3f7610a8a457b891ed359a0101f2b4));
							ce09f3b34646e71633f1c5935ca95b5fa.Push(new Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784>(c7b9c4222e8fb6a8910f4ce86cb2ffc0a.c6d3fb34101b9e1ce92055c2914d2d1de, c8e3f7610a8a457b891ed359a0101f2b4));
						}
						continue;
					}
					if (!c8e3f7610a8a457b891ed359a0101f2b4.cfd73e16b19bb8bde65db991a787eb5ad)
					{
						ce09f3b34646e71633f1c5935ca95b5fa.Push(new Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784>(c7b9c4222e8fb6a8910f4ce86cb2ffc0a, c8e3f7610a8a457b891ed359a0101f2b4.c2d9d869d0c8fd5fb55f452375f580a4a));
						ce09f3b34646e71633f1c5935ca95b5fa.Push(new Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784>(c7b9c4222e8fb6a8910f4ce86cb2ffc0a, c8e3f7610a8a457b891ed359a0101f2b4.c6d3fb34101b9e1ce92055c2914d2d1de));
						continue;
					}
					ref Pair<T> reference = ref c93cf7cdde6c9548296d1a61f5bcdff26;
					reference = new Pair<T>(c7b9c4222e8fb6a8910f4ce86cb2ffc0a.c652f269a40a2519b8658ffbade581b4e, c8e3f7610a8a457b891ed359a0101f2b4.c652f269a40a2519b8658ffbade581b4e);
					if (c1b0bbfd4af1b6a4c0c71a5dd3c509812.Filter != null)
					{
						if (!c1b0bbfd4af1b6a4c0c71a5dd3c509812.Filter.Filter(c93cf7cdde6c9548296d1a61f5bcdff26))
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
					cd179d7f18da070d164af6afcb0af45b5 = c93cf7cdde6c9548296d1a61f5bcdff26;
					c94f69126adc708dfc5f5fd5b63c52835 = 1;
					return true;
				}
				ResourcePools<Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784>>.Stacks.Recycle(ce09f3b34646e71633f1c5935ca95b5fa);
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

	private caa41fe1fc008c4cd467f2caa714d2784 c3cb2c0edc4d55dd1410e3483d1ef60d4;

	private caa41fe1fc008c4cd467f2caa714d2784[] cd311d3823f638b023eacb767b312cc3e;

	private int ca72616798115178580ff53362112e351 = 128;

	[CompilerGenerated]
	private static Func<cbe5649b18b8b0caa00be9ad49188b193<T>> c296dcbf0032342588b88000f7db18d71;

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

	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Partitioning.AabbTree`1" /> class</summary>
	public AabbTree()
	{
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
		Stack<caa41fe1fc008c4cd467f2caa714d2784> stack = ResourcePools<caa41fe1fc008c4cd467f2caa714d2784>.Stacks.Obtain();
		stack.Push(c3cb2c0edc4d55dd1410e3483d1ef60d4);
		while (stack.Count > 0)
		{
			caa41fe1fc008c4cd467f2caa714d2784 caa41fe1fc008c4cd467f2caa714d2785 = stack.Pop();
			if (!GeometryHelper.HaveContact(caa41fe1fc008c4cd467f2caa714d2785.c3e263e990bc8d6c3a2d636ac7a19c6c6, aabb))
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
				__ldtoken(AabbTree<>.c94689abd9d0357142662ab887d9573a2.MoveNext());
			}
			if (caa41fe1fc008c4cd467f2caa714d2785.cfd73e16b19bb8bde65db991a787eb5ad)
			{
				yield return caa41fe1fc008c4cd467f2caa714d2785.c652f269a40a2519b8658ffbade581b4e;
				continue;
			}
			stack.Push(caa41fe1fc008c4cd467f2caa714d2785.c2d9d869d0c8fd5fb55f452375f580a4a);
			stack.Push(caa41fe1fc008c4cd467f2caa714d2785.c6d3fb34101b9e1ce92055c2914d2d1de);
		}
		while (true)
		{
			switch (2)
			{
			case 0:
				continue;
			}
			ResourcePools<caa41fe1fc008c4cd467f2caa714d2784>.Stacks.Recycle(stack);
			yield break;
		}
	}

	private IEnumerable<caa41fe1fc008c4cd467f2caa714d2784> c4d76d858205a75f799980f42e41cbe5b(Aabb cdc3439410d15f28489c964ee87b88fe9)
	{
		Update(forceRebuild: false);
		if (c3cb2c0edc4d55dd1410e3483d1ef60d4 == null)
		{
			yield break;
		}
		Stack<caa41fe1fc008c4cd467f2caa714d2784> stack = ResourcePools<caa41fe1fc008c4cd467f2caa714d2784>.Stacks.Obtain();
		stack.Push(c3cb2c0edc4d55dd1410e3483d1ef60d4);
		while (stack.Count > 0)
		{
			caa41fe1fc008c4cd467f2caa714d2784 caa41fe1fc008c4cd467f2caa714d2785 = stack.Pop();
			if (GeometryHelper.HaveContact(caa41fe1fc008c4cd467f2caa714d2785.c3e263e990bc8d6c3a2d636ac7a19c6c6, cdc3439410d15f28489c964ee87b88fe9))
			{
				if (caa41fe1fc008c4cd467f2caa714d2785.cfd73e16b19bb8bde65db991a787eb5ad)
				{
					yield return caa41fe1fc008c4cd467f2caa714d2785;
					continue;
				}
				stack.Push(caa41fe1fc008c4cd467f2caa714d2785.c2d9d869d0c8fd5fb55f452375f580a4a);
				stack.Push(caa41fe1fc008c4cd467f2caa714d2785.c6d3fb34101b9e1ce92055c2914d2d1de);
			}
		}
		ResourcePools<caa41fe1fc008c4cd467f2caa714d2784>.Stacks.Recycle(stack);
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
			switch (4)
			{
			case 0:
				continue;
			}
			if (1 == 0)
			{
				__ldtoken(AabbTree<>.c3c692121b732ef2839be650b87e15b46.MoveNext());
			}
			Vector3F c3ec982f192efbba86e42a1cea5c = new Vector3F(1f / ray.Direction.X, 1f / ray.Direction.Y, 1f / ray.Direction.Z);
			float c912da9d2d82e2a6523be60260abdfc = Numeric.EpsilonF * (1f + Aabb.Extent.Length);
			Stack<caa41fe1fc008c4cd467f2caa714d2784> stack = ResourcePools<caa41fe1fc008c4cd467f2caa714d2784>.Stacks.Obtain();
			stack.Push(c3cb2c0edc4d55dd1410e3483d1ef60d4);
			while (stack.Count > 0)
			{
				caa41fe1fc008c4cd467f2caa714d2784 caa41fe1fc008c4cd467f2caa714d2785 = stack.Pop();
				if (!GeometryHelper.cdd6d17271fec83883494ba8e43eede6a(caa41fe1fc008c4cd467f2caa714d2785.c3e263e990bc8d6c3a2d636ac7a19c6c6, ray.Origin, c3ec982f192efbba86e42a1cea5c, ray.Length, c912da9d2d82e2a6523be60260abdfc))
				{
					continue;
				}
				if (caa41fe1fc008c4cd467f2caa714d2785.cfd73e16b19bb8bde65db991a787eb5ad)
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
					yield return caa41fe1fc008c4cd467f2caa714d2785.c652f269a40a2519b8658ffbade581b4e;
				}
				else
				{
					stack.Push(caa41fe1fc008c4cd467f2caa714d2785.c2d9d869d0c8fd5fb55f452375f580a4a);
					stack.Push(caa41fe1fc008c4cd467f2caa714d2785.c6d3fb34101b9e1ce92055c2914d2d1de);
				}
			}
			ResourcePools<caa41fe1fc008c4cd467f2caa714d2784>.Stacks.Recycle(stack);
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
				switch (6)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(AabbTree<>.GetClosestPointCandidates(Aabb, float, Func<T, float>));
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
				switch (7)
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

	private static void cb780b4b51062221f27d196ea0a7abde7(caa41fe1fc008c4cd467f2caa714d2784 c7f587bd57d47897172f47483e6bcca68, Aabb cdc3439410d15f28489c964ee87b88fe9, Func<T, float> ce4b7628f9880cef87f99eae50a8132fe, ref float c2878235e00baff8166ebc90ef27400ae)
	{
		if (c2878235e00baff8166ebc90ef27400ae < 0f)
		{
			return;
		}
		if (c2878235e00baff8166ebc90ef27400ae == 0f)
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
				__ldtoken(AabbTree<>.cb780b4b51062221f27d196ea0a7abde7(caa41fe1fc008c4cd467f2caa714d2784, Aabb, Func<T, float>, ref float));
			}
			if (!GeometryHelper.HaveContact(cdc3439410d15f28489c964ee87b88fe9, c7f587bd57d47897172f47483e6bcca68.c3e263e990bc8d6c3a2d636ac7a19c6c6))
			{
				return;
			}
		}
		if (c7f587bd57d47897172f47483e6bcca68.cfd73e16b19bb8bde65db991a787eb5ad)
		{
			float val = ce4b7628f9880cef87f99eae50a8132fe(c7f587bd57d47897172f47483e6bcca68.c652f269a40a2519b8658ffbade581b4e);
			c2878235e00baff8166ebc90ef27400ae = Math.Min(val, c2878235e00baff8166ebc90ef27400ae);
			return;
		}
		caa41fe1fc008c4cd467f2caa714d2784 c6d3fb34101b9e1ce92055c2914d2d1de = c7f587bd57d47897172f47483e6bcca68.c6d3fb34101b9e1ce92055c2914d2d1de;
		caa41fe1fc008c4cd467f2caa714d2784 c2d9d869d0c8fd5fb55f452375f580a4a = c7f587bd57d47897172f47483e6bcca68.c2d9d869d0c8fd5fb55f452375f580a4a;
		if (c2878235e00baff8166ebc90ef27400ae == 0f)
		{
			while (true)
			{
				switch (7)
				{
				case 0:
					break;
				default:
					cb780b4b51062221f27d196ea0a7abde7(c6d3fb34101b9e1ce92055c2914d2d1de, cdc3439410d15f28489c964ee87b88fe9, ce4b7628f9880cef87f99eae50a8132fe, ref c2878235e00baff8166ebc90ef27400ae);
					cb780b4b51062221f27d196ea0a7abde7(c2d9d869d0c8fd5fb55f452375f580a4a, cdc3439410d15f28489c964ee87b88fe9, ce4b7628f9880cef87f99eae50a8132fe, ref c2878235e00baff8166ebc90ef27400ae);
					return;
				}
			}
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
		else
		{
			if (num2 > c2878235e00baff8166ebc90ef27400ae)
			{
				return;
			}
			cb780b4b51062221f27d196ea0a7abde7(c2d9d869d0c8fd5fb55f452375f580a4a, cdc3439410d15f28489c964ee87b88fe9, ce4b7628f9880cef87f99eae50a8132fe, ref c2878235e00baff8166ebc90ef27400ae);
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
			cb780b4b51062221f27d196ea0a7abde7(c6d3fb34101b9e1ce92055c2914d2d1de, cdc3439410d15f28489c964ee87b88fe9, ce4b7628f9880cef87f99eae50a8132fe, ref c2878235e00baff8166ebc90ef27400ae);
		}
	}

	/// <summary>
	///       When implemented in a derived class, creates a new instance of the 
	///       <see cref="T:DigitalRune.Geometry.Partitioning.BasePartition`1" /> derived class.
	///       </summary>
	/// <returns>The new instance.</returns>
	protected override BasePartition<T> CreateInstanceCore()
	{
		return new AabbTree<T>();
	}

	/// <summary>
	///       Makes the instance a clone (deep copy) of the specified <see cref="T:DigitalRune.Geometry.Partitioning.BasePartition`1" />.
	///       </summary>
	/// <param name="sourcePartition">The object to clone.</param>
	protected override void CloneCore(BasePartition<T> sourcePartition)
	{
		base.CloneCore(sourcePartition);
	}

	internal override void ced5f5b0bd8302f3e8ade582954f3650d(bool ce8ca0a645441ac556aaf6b4574dbbb1f, HashSet<T> c049085d0fafa74bddedd7455b511e4d2, HashSet<T> c2176039f062c6700652655f4b1223258, HashSet<T> c7a74c15b192971583f3f676b15968063)
	{
		if (!ce8ca0a645441ac556aaf6b4574dbbb1f && c049085d0fafa74bddedd7455b511e4d2.Count <= 0)
		{
			if (c2176039f062c6700652655f4b1223258.Count <= 0)
			{
				c4331eef59d25a795f3b70890347a4c6c(c3cb2c0edc4d55dd1410e3483d1ef60d4, c7a74c15b192971583f3f676b15968063);
				goto IL_0045;
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
				__ldtoken(AabbTree<>.ced5f5b0bd8302f3e8ade582954f3650d(bool, HashSet<T>, HashSet<T>, HashSet<T>));
			}
		}
		ccf1870247a728556a633ff212007937e();
		goto IL_0045;
		IL_0045:
		c527912fe7c25ec4be62695147b7edc1d();
		ce759ff2781e7c0601193b90d63f12c3c();
	}

	private void c527912fe7c25ec4be62695147b7edc1d()
	{
		if (c3cb2c0edc4d55dd1410e3483d1ef60d4 != null)
		{
			Aabb = c3cb2c0edc4d55dd1410e3483d1ef60d4.c3e263e990bc8d6c3a2d636ac7a19c6c6;
		}
		else
		{
			Aabb = default;
		}
	}

	private void ce759ff2781e7c0601193b90d63f12c3c()
	{
		if (!EnableSelfOverlaps)
		{
			return;
		}
		c65379642bd949dc7e978580b9649b9bc.Clear();
		IEnumerator<Pair<T>> enumerator = GetOverlaps(this).GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Pair<T> current = enumerator.Current;
				if (BasePartition<T>.cbbc6d542116e0e180b1f11cadb4505d2.Equals(current.First, current.Second))
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
					__ldtoken(AabbTree<>.ce759ff2781e7c0601193b90d63f12c3c());
				}
				c65379642bd949dc7e978580b9649b9bc.Add(current);
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
		finally
		{
			if (enumerator != null)
			{
				while (true)
				{
					switch (5)
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

	private void ccf1870247a728556a633ff212007937e()
	{
		c3cb2c0edc4d55dd1410e3483d1ef60d4 = null;
		cd311d3823f638b023eacb767b312cc3e = null;
		if (Count == 0)
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
						__ldtoken(AabbTree<>.ccf1870247a728556a633ff212007937e());
					}
					return;
				}
			}
		}
		if (Count == 1)
		{
			T val = c0e601bcddbab14811b8bae6ae4b7b274[0];
			c3cb2c0edc4d55dd1410e3483d1ef60d4 = new caa41fe1fc008c4cd467f2caa714d2784
			{
				c3e263e990bc8d6c3a2d636ac7a19c6c6 = GetAabbForItem(val),
				c652f269a40a2519b8658ffbade581b4e = val
			};
			cd311d3823f638b023eacb767b312cc3e = new caa41fe1fc008c4cd467f2caa714d2784[1] { c3cb2c0edc4d55dd1410e3483d1ef60d4 };
			return;
		}
		int count = c0e601bcddbab14811b8bae6ae4b7b274.Count;
		List<cbe5649b18b8b0caa00be9ad49188b193<T>> list = ResourcePools<cbe5649b18b8b0caa00be9ad49188b193<T>>.Lists.Obtain();
		for (int i = 0; i < count; i++)
		{
			T val2 = c0e601bcddbab14811b8bae6ae4b7b274[i];
			Aabb c3e263e990bc8d6c3a2d636ac7a19c6c = GetAabbForItem(val2);
			list.Add(new caa41fe1fc008c4cd467f2caa714d2784
			{
				c3e263e990bc8d6c3a2d636ac7a19c6c6 = c3e263e990bc8d6c3a2d636ac7a19c6c,
				c652f269a40a2519b8658ffbade581b4e = val2
			});
		}
		if (c296dcbf0032342588b88000f7db18d71 == null)
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
			c296dcbf0032342588b88000f7db18d71 = () => new caa41fe1fc008c4cd467f2caa714d2784();
		}
		c3cb2c0edc4d55dd1410e3483d1ef60d4 = (caa41fe1fc008c4cd467f2caa714d2784)c5c139e98a37beb65b1cb5aea8ce81daf.ccf1870247a728556a633ff212007937e(list, c296dcbf0032342588b88000f7db18d71, BottomUpBuildThreshold);
		cd311d3823f638b023eacb767b312cc3e = new caa41fe1fc008c4cd467f2caa714d2784[count];
		for (int num = 0; num < count; num++)
		{
			cd311d3823f638b023eacb767b312cc3e[num] = (caa41fe1fc008c4cd467f2caa714d2784)list[num];
		}
		while (true)
		{
			switch (7)
			{
			case 0:
				continue;
			}
			ResourcePools<cbe5649b18b8b0caa00be9ad49188b193<T>>.Lists.Recycle(list);
			return;
		}
	}

	private bool c4331eef59d25a795f3b70890347a4c6c(caa41fe1fc008c4cd467f2caa714d2784 c7f587bd57d47897172f47483e6bcca68, HashSet<T> c7a74c15b192971583f3f676b15968063)
	{
		bool flag = false;
		if (c7f587bd57d47897172f47483e6bcca68.cfd73e16b19bb8bde65db991a787eb5ad)
		{
			if (c7a74c15b192971583f3f676b15968063 != null)
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
					__ldtoken(AabbTree<>.c4331eef59d25a795f3b70890347a4c6c(caa41fe1fc008c4cd467f2caa714d2784, HashSet<T>));
				}
				if (!c7a74c15b192971583f3f676b15968063.Contains(c7f587bd57d47897172f47483e6bcca68.c652f269a40a2519b8658ffbade581b4e))
				{
					goto IL_00b0;
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
			c7f587bd57d47897172f47483e6bcca68.c3e263e990bc8d6c3a2d636ac7a19c6c6 = GetAabbForItem(c7f587bd57d47897172f47483e6bcca68.c652f269a40a2519b8658ffbade581b4e);
			flag = true;
		}
		else
		{
			bool flag2 = c4331eef59d25a795f3b70890347a4c6c(c7f587bd57d47897172f47483e6bcca68.c6d3fb34101b9e1ce92055c2914d2d1de, c7a74c15b192971583f3f676b15968063);
			bool flag3 = c4331eef59d25a795f3b70890347a4c6c(c7f587bd57d47897172f47483e6bcca68.c2d9d869d0c8fd5fb55f452375f580a4a, c7a74c15b192971583f3f676b15968063);
			flag = flag2 || flag3;
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
				Aabb c3e263e990bc8d6c3a2d636ac7a19c6c = c7f587bd57d47897172f47483e6bcca68.c6d3fb34101b9e1ce92055c2914d2d1de.c3e263e990bc8d6c3a2d636ac7a19c6c6;
				c3e263e990bc8d6c3a2d636ac7a19c6c.Grow(c7f587bd57d47897172f47483e6bcca68.c2d9d869d0c8fd5fb55f452375f580a4a.c3e263e990bc8d6c3a2d636ac7a19c6c6);
				c7f587bd57d47897172f47483e6bcca68.c3e263e990bc8d6c3a2d636ac7a19c6c6 = c3e263e990bc8d6c3a2d636ac7a19c6c;
			}
		}
		goto IL_00b0;
		IL_00b0:
		return flag;
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
				switch (1)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(AabbTree<>.GetOverlaps(ISpatialPartition<T>));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(994));
				}
			}
		}
		otherPartition.Update(forceRebuild: false);
		Update(forceRebuild: false);
		if (c3cb2c0edc4d55dd1410e3483d1ef60d4 == null)
		{
			while (true)
			{
				switch (4)
				{
				case 0:
					break;
				default:
					return Enumerable.Empty<Pair<T>>();
				}
			}
		}
		if (!(otherPartition is AabbTree<T> aabbTree))
		{
			while (true)
			{
				switch (4)
				{
				case 0:
					break;
				default:
					return c0f8cf26383f225c6acffebdc67fb4382(otherPartition);
				}
			}
		}
		if (aabbTree.c3cb2c0edc4d55dd1410e3483d1ef60d4 == null)
		{
			while (true)
			{
				switch (6)
				{
				case 0:
					break;
				default:
					return Enumerable.Empty<Pair<T>>();
				}
			}
		}
		return c0f8cf26383f225c6acffebdc67fb4382(aabbTree);
	}

	private IEnumerable<Pair<T>> c0f8cf26383f225c6acffebdc67fb4382(ISpatialPartition<T> cfd8883387afb9cc25a2ac9150de0f66b)
	{
		using IEnumerator<caa41fe1fc008c4cd467f2caa714d2784> enumerator = c4d76d858205a75f799980f42e41cbe5b(cfd8883387afb9cc25a2ac9150de0f66b.Aabb).GetEnumerator();
		while (enumerator.MoveNext())
		{
			caa41fe1fc008c4cd467f2caa714d2784 current = enumerator.Current;
			IEnumerable<T> overlaps = cfd8883387afb9cc25a2ac9150de0f66b.GetOverlaps(current.c3e263e990bc8d6c3a2d636ac7a19c6c6);
			using IEnumerator<T> enumerator2 = overlaps.GetEnumerator();
			while (enumerator2.MoveNext())
			{
				Pair<T> pair = new Pair<T>(second: enumerator2.Current, first: current.c652f269a40a2519b8658ffbade581b4e);
				if (Filter != null && !Filter.Filter(pair))
				{
					continue;
				}
				yield return pair;
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
					__ldtoken(AabbTree<>.ce0ba145385131c34e19847f81ed651e5.MoveNext());
				}
				int num;
				if (num == 3)
				{
					continue;
				}
				while (true)
				{
					switch (4)
					{
					case 0:
						break;
					default:
						yield break;
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
		}
		while (true)
		{
			switch (4)
			{
			case 0:
				break;
			default:
				yield break;
			}
		}
	}

	private IEnumerable<Pair<T>> c0f8cf26383f225c6acffebdc67fb4382(AabbTree<T> ca9802b6266a4b983f751fb2d730561ca)
	{
		Stack<Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784>> stack = ResourcePools<Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784>>.Stacks.Obtain();
		stack.Push(new Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784>(c3cb2c0edc4d55dd1410e3483d1ef60d4, ca9802b6266a4b983f751fb2d730561ca.c3cb2c0edc4d55dd1410e3483d1ef60d4));
		while (stack.Count > 0)
		{
			Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784> pair = stack.Pop();
			caa41fe1fc008c4cd467f2caa714d2784 first = pair.First;
			caa41fe1fc008c4cd467f2caa714d2784 second = pair.Second;
			if (first == second)
			{
				if (!first.cfd73e16b19bb8bde65db991a787eb5ad)
				{
					stack.Push(new Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784>(first.c2d9d869d0c8fd5fb55f452375f580a4a, first.c2d9d869d0c8fd5fb55f452375f580a4a));
					stack.Push(new Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784>(first.c6d3fb34101b9e1ce92055c2914d2d1de, first.c2d9d869d0c8fd5fb55f452375f580a4a));
					stack.Push(new Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784>(first.c6d3fb34101b9e1ce92055c2914d2d1de, first.c6d3fb34101b9e1ce92055c2914d2d1de));
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
						__ldtoken(AabbTree<>.ca6428fbae1d5eb16a72015f2bbc2feae.MoveNext());
					}
					if (!second.cfd73e16b19bb8bde65db991a787eb5ad)
					{
						stack.Push(new Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784>(first.c2d9d869d0c8fd5fb55f452375f580a4a, second.c2d9d869d0c8fd5fb55f452375f580a4a));
						stack.Push(new Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784>(first.c6d3fb34101b9e1ce92055c2914d2d1de, second.c2d9d869d0c8fd5fb55f452375f580a4a));
						stack.Push(new Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784>(first.c2d9d869d0c8fd5fb55f452375f580a4a, second.c6d3fb34101b9e1ce92055c2914d2d1de));
						stack.Push(new Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784>(first.c6d3fb34101b9e1ce92055c2914d2d1de, second.c6d3fb34101b9e1ce92055c2914d2d1de));
					}
					else
					{
						stack.Push(new Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784>(first.c2d9d869d0c8fd5fb55f452375f580a4a, second));
						stack.Push(new Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784>(first.c6d3fb34101b9e1ce92055c2914d2d1de, second));
					}
				}
				else if (!second.cfd73e16b19bb8bde65db991a787eb5ad)
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
					stack.Push(new Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784>(first, second.c2d9d869d0c8fd5fb55f452375f580a4a));
					stack.Push(new Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784>(first, second.c6d3fb34101b9e1ce92055c2914d2d1de));
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
			switch (4)
			{
			case 0:
				continue;
			}
			ResourcePools<Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784>>.Stacks.Recycle(stack);
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
		if (!(otherPartition is AabbTree<T> aabbTree))
		{
			return c0f8cf26383f225c6acffebdc67fb4382(scale, otherPartition, otherScale, pose.Inverse * otherPose);
		}
		if (aabbTree.c3cb2c0edc4d55dd1410e3483d1ef60d4 == null)
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
						__ldtoken(AabbTree<>.GetOverlaps(Vector3F, Pose, ISpatialPartition<T>, Vector3F, Pose));
					}
					return Enumerable.Empty<Pair<T>>();
				}
			}
		}
		return c0f8cf26383f225c6acffebdc67fb4382(scale, aabbTree, otherScale, pose.Inverse * otherPose);
	}

	private IEnumerable<Pair<T>> c0f8cf26383f225c6acffebdc67fb4382(Vector3F ceabb47d7d959545d89c4300eabbdd7c2, ISpatialPartition<T> cfd8883387afb9cc25a2ac9150de0f66b, Vector3F c1b4918d9d3cc94171c67339d1fc78b0f, Pose cd2abebf7ff6ec7d035c28a04faaa459b)
	{
		Vector3F scale = Vector3F.One / ceabb47d7d959545d89c4300eabbdd7c2;
		Vector3F scale2 = Vector3F.One / c1b4918d9d3cc94171c67339d1fc78b0f;
		Pose pose = cd2abebf7ff6ec7d035c28a04faaa459b;
		Pose inverse = cd2abebf7ff6ec7d035c28a04faaa459b.Inverse;
		Aabb aabb = cfd8883387afb9cc25a2ac9150de0f66b.Aabb.GetAabb(c1b4918d9d3cc94171c67339d1fc78b0f, pose);
		aabb.Scale(scale);
		IEnumerator<caa41fe1fc008c4cd467f2caa714d2784> enumerator = c4d76d858205a75f799980f42e41cbe5b(aabb).GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				caa41fe1fc008c4cd467f2caa714d2784 current = enumerator.Current;
				Aabb aabb2 = current.c3e263e990bc8d6c3a2d636ac7a19c6c6.GetAabb(ceabb47d7d959545d89c4300eabbdd7c2, inverse);
				aabb2.Scale(scale2);
				IEnumerator<T> enumerator2 = cfd8883387afb9cc25a2ac9150de0f66b.GetOverlaps(aabb2).GetEnumerator();
				try
				{
					while (enumerator2.MoveNext())
					{
						Pair<T> pair = new Pair<T>(second: enumerator2.Current, first: current.c652f269a40a2519b8658ffbade581b4e);
						if (Filter != null)
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
								__ldtoken(AabbTree<>.c23c9ec162924befc07131ab8b16d9cdb.MoveNext());
							}
							if (!Filter.Filter(pair))
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
							switch (4)
							{
							case 0:
								continue;
							}
							if (1 == 0)
							{
								__ldtoken(AabbTree<>.c23c9ec162924befc07131ab8b16d9cdb.ca0a60671becd1e0cef8a51aa281f5da6());
							}
							enumerator2.Dispose();
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
					switch (1)
					{
					case 0:
						continue;
					}
					if (1 == 0)
					{
						__ldtoken(AabbTree<>.c23c9ec162924befc07131ab8b16d9cdb.c24da2bb11edef9366fe96ba0c1f521c4());
					}
					enumerator.Dispose();
					break;
				}
			}
		}
	}

	private IEnumerable<Pair<T>> c0f8cf26383f225c6acffebdc67fb4382(Vector3F ceabb47d7d959545d89c4300eabbdd7c2, AabbTree<T> ca9802b6266a4b983f751fb2d730561ca, Vector3F c1b4918d9d3cc94171c67339d1fc78b0f, Pose cd2abebf7ff6ec7d035c28a04faaa459b)
	{
		Stack<Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784>> stack = ResourcePools<Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784>>.Stacks.Obtain();
		stack.Push(new Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784>(c3cb2c0edc4d55dd1410e3483d1ef60d4, ca9802b6266a4b983f751fb2d730561ca.c3cb2c0edc4d55dd1410e3483d1ef60d4));
		while (stack.Count > 0)
		{
			Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784> pair = stack.Pop();
			caa41fe1fc008c4cd467f2caa714d2784 first = pair.First;
			caa41fe1fc008c4cd467f2caa714d2784 second = pair.Second;
			if (!cd0cfe897426aab73c90b8217df390607(first, ceabb47d7d959545d89c4300eabbdd7c2, second, c1b4918d9d3cc94171c67339d1fc78b0f, cd2abebf7ff6ec7d035c28a04faaa459b))
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
				__ldtoken(AabbTree<>.c6aad3e71937a02f95c6602cffe97bbce.MoveNext());
			}
			if (!first.cfd73e16b19bb8bde65db991a787eb5ad)
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
				if (!second.cfd73e16b19bb8bde65db991a787eb5ad)
				{
					stack.Push(new Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784>(first.c2d9d869d0c8fd5fb55f452375f580a4a, second.c2d9d869d0c8fd5fb55f452375f580a4a));
					stack.Push(new Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784>(first.c6d3fb34101b9e1ce92055c2914d2d1de, second.c2d9d869d0c8fd5fb55f452375f580a4a));
					stack.Push(new Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784>(first.c2d9d869d0c8fd5fb55f452375f580a4a, second.c6d3fb34101b9e1ce92055c2914d2d1de));
					stack.Push(new Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784>(first.c6d3fb34101b9e1ce92055c2914d2d1de, second.c6d3fb34101b9e1ce92055c2914d2d1de));
				}
				else
				{
					stack.Push(new Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784>(first.c2d9d869d0c8fd5fb55f452375f580a4a, second));
					stack.Push(new Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784>(first.c6d3fb34101b9e1ce92055c2914d2d1de, second));
				}
				continue;
			}
			if (!second.cfd73e16b19bb8bde65db991a787eb5ad)
			{
				stack.Push(new Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784>(first, second.c2d9d869d0c8fd5fb55f452375f580a4a));
				stack.Push(new Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784>(first, second.c6d3fb34101b9e1ce92055c2914d2d1de));
				continue;
			}
			Pair<T> pair2 = new Pair<T>(first.c652f269a40a2519b8658ffbade581b4e, second.c652f269a40a2519b8658ffbade581b4e);
			if (Filter != null)
			{
				if (!Filter.Filter(pair2))
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
			yield return pair2;
		}
		ResourcePools<Pair<caa41fe1fc008c4cd467f2caa714d2784, caa41fe1fc008c4cd467f2caa714d2784>>.Stacks.Recycle(stack);
	}

	private static bool cc2503a1629586ff93a4137525e360640(caa41fe1fc008c4cd467f2caa714d2784 c1f49a553317458268e1fa11d51bf4670, Vector3F c3bc7e17fda039e69573a5295fced7439, caa41fe1fc008c4cd467f2caa714d2784 c29ba3cbb559c64c577242fda562d3024, Vector3F c547f06073c85c380fd3dccf550a6d536)
	{
		Vector3F vector3F = c1f49a553317458268e1fa11d51bf4670.c3e263e990bc8d6c3a2d636ac7a19c6c6.Extent * Vector3F.Absolute(c3bc7e17fda039e69573a5295fced7439);
		Vector3F vector3F2 = c29ba3cbb559c64c577242fda562d3024.c3e263e990bc8d6c3a2d636ac7a19c6c6.Extent * Vector3F.Absolute(c547f06073c85c380fd3dccf550a6d536);
		return vector3F.LargestComponent > vector3F2.LargestComponent;
	}

	private static bool cd0cfe897426aab73c90b8217df390607(caa41fe1fc008c4cd467f2caa714d2784 c1f49a553317458268e1fa11d51bf4670, Vector3F c3bc7e17fda039e69573a5295fced7439, caa41fe1fc008c4cd467f2caa714d2784 c29ba3cbb559c64c577242fda562d3024, Vector3F c547f06073c85c380fd3dccf550a6d536, Pose c689a79a5792495f42cbe3effa012780c)
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
			while (true)
			{
				switch (7)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(AabbTree<>.GetClosestPointCandidates(Vector3F, Pose, ISpatialPartition<T>, Vector3F, Pose, Func<T, T, float>));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(994));
				}
			}
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
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1023));
				}
			}
		}
		otherPartition.Update(forceRebuild: false);
		Update(forceRebuild: false);
		if (c3cb2c0edc4d55dd1410e3483d1ef60d4 == null)
		{
			return;
		}
		if (otherPartition is AabbTree<T>)
		{
			while (true)
			{
				switch (3)
				{
				case 0:
					break;
				default:
				{
					AabbTree<T> aabbTree = (AabbTree<T>)otherPartition;
					float c2878235e00baff8166ebc90ef27400ae = float.PositiveInfinity;
					cb780b4b51062221f27d196ea0a7abde7(c3cb2c0edc4d55dd1410e3483d1ef60d4, scale, aabbTree.c3cb2c0edc4d55dd1410e3483d1ef60d4, otherScale, pose.Inverse * otherPose, callback, ref c2878235e00baff8166ebc90ef27400ae);
					return;
				}
				}
			}
		}
		if (otherPartition is ISupportClosestPointQueries<T>)
		{
			while (true)
			{
				switch (6)
				{
				case 0:
					break;
				default:
					cb780b4b51062221f27d196ea0a7abde7(scale, pose, (ISupportClosestPointQueries<T>)otherPartition, otherScale, otherPose, callback);
					return;
				}
			}
		}
		cb780b4b51062221f27d196ea0a7abde7(otherPartition, callback);
	}

	private void cb780b4b51062221f27d196ea0a7abde7(caa41fe1fc008c4cd467f2caa714d2784 c1f49a553317458268e1fa11d51bf4670, Vector3F c3bc7e17fda039e69573a5295fced7439, caa41fe1fc008c4cd467f2caa714d2784 c29ba3cbb559c64c577242fda562d3024, Vector3F c547f06073c85c380fd3dccf550a6d536, Pose c689a79a5792495f42cbe3effa012780c, Func<T, T, float> ce4b7628f9880cef87f99eae50a8132fe, ref float c2878235e00baff8166ebc90ef27400ae)
	{
		if (c1f49a553317458268e1fa11d51bf4670 == null || c29ba3cbb559c64c577242fda562d3024 == null || c2878235e00baff8166ebc90ef27400ae < 0f)
		{
			return;
		}
		if (c2878235e00baff8166ebc90ef27400ae == 0f)
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
				__ldtoken(AabbTree<>.cb780b4b51062221f27d196ea0a7abde7(caa41fe1fc008c4cd467f2caa714d2784, Vector3F, caa41fe1fc008c4cd467f2caa714d2784, Vector3F, Pose, Func<T, T, float>, ref float));
			}
			if (!cd0cfe897426aab73c90b8217df390607(c1f49a553317458268e1fa11d51bf4670, c3bc7e17fda039e69573a5295fced7439, c29ba3cbb559c64c577242fda562d3024, c547f06073c85c380fd3dccf550a6d536, c689a79a5792495f42cbe3effa012780c))
			{
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
		}
		if (c1f49a553317458268e1fa11d51bf4670.cfd73e16b19bb8bde65db991a787eb5ad)
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
			if (c29ba3cbb559c64c577242fda562d3024.cfd73e16b19bb8bde65db991a787eb5ad)
			{
				while (true)
				{
					switch (6)
					{
					case 0:
						break;
					default:
					{
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
							if (!Filter.Filter(new Pair<T>(c1f49a553317458268e1fa11d51bf4670.c652f269a40a2519b8658ffbade581b4e, c29ba3cbb559c64c577242fda562d3024.c652f269a40a2519b8658ffbade581b4e)))
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
								break;
							}
						}
						float val = ce4b7628f9880cef87f99eae50a8132fe(c1f49a553317458268e1fa11d51bf4670.c652f269a40a2519b8658ffbade581b4e, c29ba3cbb559c64c577242fda562d3024.c652f269a40a2519b8658ffbade581b4e);
						c2878235e00baff8166ebc90ef27400ae = Math.Min(val, c2878235e00baff8166ebc90ef27400ae);
						return;
					}
					}
				}
			}
		}
		if (!c29ba3cbb559c64c577242fda562d3024.cfd73e16b19bb8bde65db991a787eb5ad)
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
			if (!c1f49a553317458268e1fa11d51bf4670.cfd73e16b19bb8bde65db991a787eb5ad)
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
				if (cc2503a1629586ff93a4137525e360640(c1f49a553317458268e1fa11d51bf4670, c3bc7e17fda039e69573a5295fced7439, c29ba3cbb559c64c577242fda562d3024, c547f06073c85c380fd3dccf550a6d536))
				{
					goto IL_010d;
				}
			}
			caa41fe1fc008c4cd467f2caa714d2784 c6d3fb34101b9e1ce92055c2914d2d1de = c29ba3cbb559c64c577242fda562d3024.c6d3fb34101b9e1ce92055c2914d2d1de;
			caa41fe1fc008c4cd467f2caa714d2784 c2d9d869d0c8fd5fb55f452375f580a4a = c29ba3cbb559c64c577242fda562d3024.c2d9d869d0c8fd5fb55f452375f580a4a;
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
					switch (7)
					{
					case 0:
						break;
					default:
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
					switch (7)
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
		goto IL_010d;
		IL_010d:
		caa41fe1fc008c4cd467f2caa714d2784 c6d3fb34101b9e1ce92055c2914d2d1de2 = c1f49a553317458268e1fa11d51bf4670.c6d3fb34101b9e1ce92055c2914d2d1de;
		caa41fe1fc008c4cd467f2caa714d2784 c2d9d869d0c8fd5fb55f452375f580a4a2 = c1f49a553317458268e1fa11d51bf4670.c2d9d869d0c8fd5fb55f452375f580a4a;
		if (c2878235e00baff8166ebc90ef27400ae == 0f)
		{
			while (true)
			{
				switch (4)
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
					if (!(num3 > c2878235e00baff8166ebc90ef27400ae))
					{
						cb780b4b51062221f27d196ea0a7abde7(c6d3fb34101b9e1ce92055c2914d2d1de2, c3bc7e17fda039e69573a5295fced7439, c29ba3cbb559c64c577242fda562d3024, c547f06073c85c380fd3dccf550a6d536, c689a79a5792495f42cbe3effa012780c, ce4b7628f9880cef87f99eae50a8132fe, ref c2878235e00baff8166ebc90ef27400ae);
						if (num4 > c2878235e00baff8166ebc90ef27400ae)
						{
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
						cb780b4b51062221f27d196ea0a7abde7(c2d9d869d0c8fd5fb55f452375f580a4a2, c3bc7e17fda039e69573a5295fced7439, c29ba3cbb559c64c577242fda562d3024, c547f06073c85c380fd3dccf550a6d536, c689a79a5792495f42cbe3effa012780c, ce4b7628f9880cef87f99eae50a8132fe, ref c2878235e00baff8166ebc90ef27400ae);
					}
					return;
				}
			}
		}
		if (num4 > c2878235e00baff8166ebc90ef27400ae)
		{
			return;
		}
		cb780b4b51062221f27d196ea0a7abde7(c2d9d869d0c8fd5fb55f452375f580a4a2, c3bc7e17fda039e69573a5295fced7439, c29ba3cbb559c64c577242fda562d3024, c547f06073c85c380fd3dccf550a6d536, c689a79a5792495f42cbe3effa012780c, ce4b7628f9880cef87f99eae50a8132fe, ref c2878235e00baff8166ebc90ef27400ae);
		if (num3 > c2878235e00baff8166ebc90ef27400ae)
		{
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
		cb780b4b51062221f27d196ea0a7abde7(c6d3fb34101b9e1ce92055c2914d2d1de2, c3bc7e17fda039e69573a5295fced7439, c29ba3cbb559c64c577242fda562d3024, c547f06073c85c380fd3dccf550a6d536, c689a79a5792495f42cbe3effa012780c, ce4b7628f9880cef87f99eae50a8132fe, ref c2878235e00baff8166ebc90ef27400ae);
	}

	private void cb780b4b51062221f27d196ea0a7abde7(Vector3F ceabb47d7d959545d89c4300eabbdd7c2, Pose cbc1b7e0b011ad6dcdb8beb392ec7a646, ISupportClosestPointQueries<T> cfd8883387afb9cc25a2ac9150de0f66b, Vector3F c1b4918d9d3cc94171c67339d1fc78b0f, Pose cd2abebf7ff6ec7d035c28a04faaa459b, Func<T, T, float> ce4b7628f9880cef87f99eae50a8132fe)
	{
		cc308d98a84a8563af043b321600339a2<T> cc308d98a84a8563af043b321600339a3 = cc308d98a84a8563af043b321600339a2<T>.c2d8ce8b1ccf2b905e3b18f8a5145fe8a();
		cc308d98a84a8563af043b321600339a3.OriginalCallback = ce4b7628f9880cef87f99eae50a8132fe;
		Pose pose = cd2abebf7ff6ec7d035c28a04faaa459b.Inverse * cbc1b7e0b011ad6dcdb8beb392ec7a646;
		Vector3F scale = Vector3F.One / c1b4918d9d3cc94171c67339d1fc78b0f;
		float num = float.PositiveInfinity;
		for (int i = 0; i < cd311d3823f638b023eacb767b312cc3e.Length; i++)
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
				__ldtoken(AabbTree<>.cb780b4b51062221f27d196ea0a7abde7(Vector3F, Pose, ISupportClosestPointQueries<T>, Vector3F, Pose, Func<T, T, float>));
			}
			if (num >= 0f)
			{
				caa41fe1fc008c4cd467f2caa714d2784 caa41fe1fc008c4cd467f2caa714d2785 = cd311d3823f638b023eacb767b312cc3e[i];
				cc308d98a84a8563af043b321600339a3.Item = caa41fe1fc008c4cd467f2caa714d2785.c652f269a40a2519b8658ffbade581b4e;
				Aabb aabb = caa41fe1fc008c4cd467f2caa714d2785.c3e263e990bc8d6c3a2d636ac7a19c6c6.GetAabb(ceabb47d7d959545d89c4300eabbdd7c2, pose);
				aabb.Scale(scale);
				num = cfd8883387afb9cc25a2ac9150de0f66b.GetClosestPointCandidates(aabb, num, cc308d98a84a8563af043b321600339a3.c6516654248581f959063ea51406f708b);
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
			break;
		}
		cc308d98a84a8563af043b321600339a3.ca18b9b5faa9851d6355f617787bb5859();
	}

	private void cb780b4b51062221f27d196ea0a7abde7(ISpatialPartition<T> cfd8883387afb9cc25a2ac9150de0f66b, Func<T, T, float> ce4b7628f9880cef87f99eae50a8132fe)
	{
		using List<T>.Enumerator enumerator = c0e601bcddbab14811b8bae6ae4b7b274.GetEnumerator();
		while (enumerator.MoveNext())
		{
			T current = enumerator.Current;
			using IEnumerator<T> enumerator2 = cfd8883387afb9cc25a2ac9150de0f66b.GetEnumerator();
			while (enumerator2.MoveNext())
			{
				T current2 = enumerator2.Current;
				float num = ce4b7628f9880cef87f99eae50a8132fe(current, current2);
				if (num < 0f)
				{
					return;
				}
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
					__ldtoken(AabbTree<>.cb780b4b51062221f27d196ea0a7abde7(ISpatialPartition<T>, Func<T, T, float>));
				}
				break;
			}
		}
		while (true)
		{
			switch (3)
			{
			case 0:
				break;
			default:
				return;
			}
		}
	}

	[CompilerGenerated]
	private static cbe5649b18b8b0caa00be9ad49188b193<T> cf271fc36080579bdd8f5c064cf151a78()
	{
		return new caa41fe1fc008c4cd467f2caa714d2784();
	}
}
