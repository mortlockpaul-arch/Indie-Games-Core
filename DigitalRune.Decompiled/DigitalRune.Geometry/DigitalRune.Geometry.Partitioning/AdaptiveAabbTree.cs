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
///       Builds a bounding volume tree using axis-aligned bounding boxes (AABBs), which adapts
///       automatically when items are added, moved, or removed.
///       </summary>
/// <typeparam name="T">The type of item in the spatial partition.</typeparam>
public class AdaptiveAabbTree<T> : BasePartition<T>
{
	private class cff48f0c06d0d98efeafd069bbfaea07e : cbe5649b18b8b0caa00be9ad49188b193<T>
	{
		public Aabb c3e263e990bc8d6c3a2d636ac7a19c6c6;

		public cff48f0c06d0d98efeafd069bbfaea07e c6d3fb34101b9e1ce92055c2914d2d1de;

		public cff48f0c06d0d98efeafd069bbfaea07e c2d9d869d0c8fd5fb55f452375f580a4a;

		public T c652f269a40a2519b8658ffbade581b4e;

		public List<cff48f0c06d0d98efeafd069bbfaea07e> c9594e2c7b7942e1d22a230fdeee51f04;

		public bool c2c1eca2324d13154ef9733df393024a3;

		public bool c74dc8c78f0b932d691a8442fc47f4ad5;

		public bool cfd73e16b19bb8bde65db991a787eb5ad
		{
			get
			{
				if (c9594e2c7b7942e1d22a230fdeee51f04 == null && c6d3fb34101b9e1ce92055c2914d2d1de == null)
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
								__ldtoken(AdaptiveAabbTree<>.cff48f0c06d0d98efeafd069bbfaea07e.get_cfd73e16b19bb8bde65db991a787eb5ad());
							}
							return c2d9d869d0c8fd5fb55f452375f580a4a == null;
						}
					}
				}
				return false;
			}
		}

		public bool c8e71ef62104b5216a597dbb6b08779f3 => c9594e2c7b7942e1d22a230fdeee51f04 == null;

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
				c6d3fb34101b9e1ce92055c2914d2d1de = (cff48f0c06d0d98efeafd069bbfaea07e)value;
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
				c2d9d869d0c8fd5fb55f452375f580a4a = (cff48f0c06d0d98efeafd069bbfaea07e)value;
			}
		}

		private cbe5649b18b8b0caa00be9ad49188b193<T> cfff16699b0ff91e60fab908ef0f2ea0b
		{
			get
			{
				throw new NotSupportedException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7728));
			}
		}

		private T c477d5bc87b57a983b9d9da4eab43eb34 => c652f269a40a2519b8658ffbade581b4e;
	}

	[CompilerGenerated]
	private sealed class cbb4b4a4f3e5c5a88e0a7d6d05f43fb95 : IEnumerable<Pair<T>>, IEnumerator<Pair<T>>, IEnumerable, IEnumerator, IDisposable
	{
		private Pair<T> cd179d7f18da070d164af6afcb0af45b5;

		private int c94f69126adc708dfc5f5fd5b63c52835;

		private int c0e8a058e779916e7715d7616fb0da9e7;

		public AdaptiveAabbTree<T> c1b0bbfd4af1b6a4c0c71a5dd3c509812;

		public AdaptiveAabbTree<T> ca9802b6266a4b983f751fb2d730561ca;

		public AdaptiveAabbTree<T> c6b71d229c51c908f777d8166aeaf9947;

		public Stack<Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e>> c05cc3ad371080d21f96f832ae4ca19cb;

		public Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e> c5cd86eb0a014b8afaed556d7636fb4e9;

		public cff48f0c06d0d98efeafd069bbfaea07e c2d659fe72e4169cf1ed67e3e00ab4203;

		public cff48f0c06d0d98efeafd069bbfaea07e ce87c33617c995b624b5b055b251d1b11;

		public Pair<T> ce6bcb5774c0fa14930aa2b5d277099fd;

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
		public cbb4b4a4f3e5c5a88e0a7d6d05f43fb95(int c94f69126adc708dfc5f5fd5b63c52835)
		{
			this.c94f69126adc708dfc5f5fd5b63c52835 = c94f69126adc708dfc5f5fd5b63c52835;
			c0e8a058e779916e7715d7616fb0da9e7 = Thread.CurrentThread.ManagedThreadId;
		}

		[DebuggerHidden]
		private IEnumerator<Pair<T>> c4bd63b962f346f189f5c6a4ad2e4ce8d()
		{
			cbb4b4a4f3e5c5a88e0a7d6d05f43fb95 cbb4b4a4f3e5c5a88e0a7d6d05f43fb96;
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
					__ldtoken(AdaptiveAabbTree<>.cbb4b4a4f3e5c5a88e0a7d6d05f43fb95.c4bd63b962f346f189f5c6a4ad2e4ce8d());
				}
				if (c94f69126adc708dfc5f5fd5b63c52835 == -2)
				{
					c94f69126adc708dfc5f5fd5b63c52835 = 0;
					cbb4b4a4f3e5c5a88e0a7d6d05f43fb96 = this;
					goto IL_0051;
				}
			}
			cbb4b4a4f3e5c5a88e0a7d6d05f43fb96 = new cbb4b4a4f3e5c5a88e0a7d6d05f43fb95(0)
			{
				c1b0bbfd4af1b6a4c0c71a5dd3c509812 = c1b0bbfd4af1b6a4c0c71a5dd3c509812
			};
			goto IL_0051;
			IL_0051:
			cbb4b4a4f3e5c5a88e0a7d6d05f43fb96.ca9802b6266a4b983f751fb2d730561ca = c6b71d229c51c908f777d8166aeaf9947;
			return cbb4b4a4f3e5c5a88e0a7d6d05f43fb96;
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
				c05cc3ad371080d21f96f832ae4ca19cb = ResourcePools<Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e>>.Stacks.Obtain();
				c05cc3ad371080d21f96f832ae4ca19cb.Push(new Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e>(c1b0bbfd4af1b6a4c0c71a5dd3c509812.c3cb2c0edc4d55dd1410e3483d1ef60d4, ca9802b6266a4b983f751fb2d730561ca.c3cb2c0edc4d55dd1410e3483d1ef60d4));
				goto IL_0399;
			case 1:
				{
					c94f69126adc708dfc5f5fd5b63c52835 = -1;
					goto IL_0399;
				}
				IL_0399:
				while (c05cc3ad371080d21f96f832ae4ca19cb.Count > 0)
				{
					c5cd86eb0a014b8afaed556d7636fb4e9 = c05cc3ad371080d21f96f832ae4ca19cb.Pop();
					c2d659fe72e4169cf1ed67e3e00ab4203 = c5cd86eb0a014b8afaed556d7636fb4e9.First;
					ce87c33617c995b624b5b055b251d1b11 = c5cd86eb0a014b8afaed556d7636fb4e9.Second;
					c2d659fe72e4169cf1ed67e3e00ab4203.c2c1eca2324d13154ef9733df393024a3 = true;
					ce87c33617c995b624b5b055b251d1b11.c2c1eca2324d13154ef9733df393024a3 = true;
					if (c2d659fe72e4169cf1ed67e3e00ab4203 == ce87c33617c995b624b5b055b251d1b11)
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
							__ldtoken(AdaptiveAabbTree<>.cbb4b4a4f3e5c5a88e0a7d6d05f43fb95.MoveNext());
						}
						if (c2d659fe72e4169cf1ed67e3e00ab4203.cfd73e16b19bb8bde65db991a787eb5ad)
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
						AdaptiveAabbTree<T>.c18ac18e9f88283e6a56a9ec9cb6b9e55(c2d659fe72e4169cf1ed67e3e00ab4203);
						c05cc3ad371080d21f96f832ae4ca19cb.Push(new Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e>(c2d659fe72e4169cf1ed67e3e00ab4203.c2d9d869d0c8fd5fb55f452375f580a4a, c2d659fe72e4169cf1ed67e3e00ab4203.c2d9d869d0c8fd5fb55f452375f580a4a));
						c05cc3ad371080d21f96f832ae4ca19cb.Push(new Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e>(c2d659fe72e4169cf1ed67e3e00ab4203.c6d3fb34101b9e1ce92055c2914d2d1de, c2d659fe72e4169cf1ed67e3e00ab4203.c2d9d869d0c8fd5fb55f452375f580a4a));
						c05cc3ad371080d21f96f832ae4ca19cb.Push(new Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e>(c2d659fe72e4169cf1ed67e3e00ab4203.c6d3fb34101b9e1ce92055c2914d2d1de, c2d659fe72e4169cf1ed67e3e00ab4203.c6d3fb34101b9e1ce92055c2914d2d1de));
					}
					else
					{
						if (!GeometryHelper.HaveContact(c2d659fe72e4169cf1ed67e3e00ab4203.c3e263e990bc8d6c3a2d636ac7a19c6c6, ce87c33617c995b624b5b055b251d1b11.c3e263e990bc8d6c3a2d636ac7a19c6c6))
						{
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
						if (!c2d659fe72e4169cf1ed67e3e00ab4203.cfd73e16b19bb8bde65db991a787eb5ad)
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
							if (!ce87c33617c995b624b5b055b251d1b11.cfd73e16b19bb8bde65db991a787eb5ad)
							{
								AdaptiveAabbTree<T>.c18ac18e9f88283e6a56a9ec9cb6b9e55(c2d659fe72e4169cf1ed67e3e00ab4203);
								AdaptiveAabbTree<T>.c18ac18e9f88283e6a56a9ec9cb6b9e55(ce87c33617c995b624b5b055b251d1b11);
								c05cc3ad371080d21f96f832ae4ca19cb.Push(new Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e>(c2d659fe72e4169cf1ed67e3e00ab4203.c2d9d869d0c8fd5fb55f452375f580a4a, ce87c33617c995b624b5b055b251d1b11.c2d9d869d0c8fd5fb55f452375f580a4a));
								c05cc3ad371080d21f96f832ae4ca19cb.Push(new Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e>(c2d659fe72e4169cf1ed67e3e00ab4203.c6d3fb34101b9e1ce92055c2914d2d1de, ce87c33617c995b624b5b055b251d1b11.c2d9d869d0c8fd5fb55f452375f580a4a));
								c05cc3ad371080d21f96f832ae4ca19cb.Push(new Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e>(c2d659fe72e4169cf1ed67e3e00ab4203.c2d9d869d0c8fd5fb55f452375f580a4a, ce87c33617c995b624b5b055b251d1b11.c6d3fb34101b9e1ce92055c2914d2d1de));
								c05cc3ad371080d21f96f832ae4ca19cb.Push(new Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e>(c2d659fe72e4169cf1ed67e3e00ab4203.c6d3fb34101b9e1ce92055c2914d2d1de, ce87c33617c995b624b5b055b251d1b11.c6d3fb34101b9e1ce92055c2914d2d1de));
							}
							else
							{
								AdaptiveAabbTree<T>.c18ac18e9f88283e6a56a9ec9cb6b9e55(c2d659fe72e4169cf1ed67e3e00ab4203);
								c05cc3ad371080d21f96f832ae4ca19cb.Push(new Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e>(c2d659fe72e4169cf1ed67e3e00ab4203.c2d9d869d0c8fd5fb55f452375f580a4a, ce87c33617c995b624b5b055b251d1b11));
								c05cc3ad371080d21f96f832ae4ca19cb.Push(new Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e>(c2d659fe72e4169cf1ed67e3e00ab4203.c6d3fb34101b9e1ce92055c2914d2d1de, ce87c33617c995b624b5b055b251d1b11));
							}
							continue;
						}
						if (ce87c33617c995b624b5b055b251d1b11.cfd73e16b19bb8bde65db991a787eb5ad)
						{
							ref Pair<T> reference = ref ce6bcb5774c0fa14930aa2b5d277099fd;
							reference = new Pair<T>(c2d659fe72e4169cf1ed67e3e00ab4203.c652f269a40a2519b8658ffbade581b4e, ce87c33617c995b624b5b055b251d1b11.c652f269a40a2519b8658ffbade581b4e);
							if (c1b0bbfd4af1b6a4c0c71a5dd3c509812.Filter != null)
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
								if (!c1b0bbfd4af1b6a4c0c71a5dd3c509812.Filter.Filter(ce6bcb5774c0fa14930aa2b5d277099fd))
								{
									continue;
								}
							}
							cd179d7f18da070d164af6afcb0af45b5 = ce6bcb5774c0fa14930aa2b5d277099fd;
							c94f69126adc708dfc5f5fd5b63c52835 = 1;
							return true;
						}
						AdaptiveAabbTree<T>.c18ac18e9f88283e6a56a9ec9cb6b9e55(ce87c33617c995b624b5b055b251d1b11);
						c05cc3ad371080d21f96f832ae4ca19cb.Push(new Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e>(c2d659fe72e4169cf1ed67e3e00ab4203, ce87c33617c995b624b5b055b251d1b11.c2d9d869d0c8fd5fb55f452375f580a4a));
						c05cc3ad371080d21f96f832ae4ca19cb.Push(new Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e>(c2d659fe72e4169cf1ed67e3e00ab4203, ce87c33617c995b624b5b055b251d1b11.c6d3fb34101b9e1ce92055c2914d2d1de));
					}
				}
				ResourcePools<Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e>>.Stacks.Recycle(c05cc3ad371080d21f96f832ae4ca19cb);
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
	private sealed class c03b415ded886c3c5a1ba0440979bcdd5 : IEnumerable<Pair<T>>, IEnumerator<Pair<T>>, IEnumerable, IEnumerator, IDisposable
	{
		private Pair<T> cd179d7f18da070d164af6afcb0af45b5;

		private int c94f69126adc708dfc5f5fd5b63c52835;

		private int c0e8a058e779916e7715d7616fb0da9e7;

		public AdaptiveAabbTree<T> c1b0bbfd4af1b6a4c0c71a5dd3c509812;

		public Vector3F ceabb47d7d959545d89c4300eabbdd7c2;

		public Vector3F c6f203721b629fec4c93cc63ff1172fc4;

		public ISpatialPartition<T> cfd8883387afb9cc25a2ac9150de0f66b;

		public ISpatialPartition<T> cd1939b65df62ed5f7a2112466dabd8f0;

		public Vector3F c1b4918d9d3cc94171c67339d1fc78b0f;

		public Vector3F cfd54ba1e25c70994847ab953f2228207;

		public Pose cd2abebf7ff6ec7d035c28a04faaa459b;

		public Pose c1c245528512f0ae5611e6e833a70158f;

		public Vector3F c627c865255449b302ea5d4e57c541bac;

		public Vector3F cee960f0c1783e62f81ed3b57ce8bc8db;

		public Pose cba6ff7b0b103371a1ac9346351109d2a;

		public Pose cdeaa38d7a688e83f4875df37e9b278b4;

		public Aabb c003390ba045db1605d3972e270349aa0;

		public cff48f0c06d0d98efeafd069bbfaea07e c331daf4df19077e1da4ef2e31e448630;

		public Aabb cb4f10e91ac09da8777031538da821afc;

		public T cee9bfa84b8333878ebfaa30c3db01e06;

		public Pair<T> cc30c4b3009a09c1a9241b11b7c893d0d;

		public IEnumerator<cff48f0c06d0d98efeafd069bbfaea07e> cd540e50afb414c19e9c6a91e0e0771e9;

		public IEnumerator<T> cb52708b4b3b6db12324dc3c2787a3b59;

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
		public c03b415ded886c3c5a1ba0440979bcdd5(int c94f69126adc708dfc5f5fd5b63c52835)
		{
			this.c94f69126adc708dfc5f5fd5b63c52835 = c94f69126adc708dfc5f5fd5b63c52835;
			c0e8a058e779916e7715d7616fb0da9e7 = Thread.CurrentThread.ManagedThreadId;
		}

		[DebuggerHidden]
		private IEnumerator<Pair<T>> c4bd63b962f346f189f5c6a4ad2e4ce8d()
		{
			c03b415ded886c3c5a1ba0440979bcdd5 c03b415ded886c3c5a1ba0440979bcdd6;
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
					__ldtoken(AdaptiveAabbTree<>.c03b415ded886c3c5a1ba0440979bcdd5.c4bd63b962f346f189f5c6a4ad2e4ce8d());
				}
				if (c94f69126adc708dfc5f5fd5b63c52835 == -2)
				{
					c94f69126adc708dfc5f5fd5b63c52835 = 0;
					c03b415ded886c3c5a1ba0440979bcdd6 = this;
					goto IL_004f;
				}
			}
			c03b415ded886c3c5a1ba0440979bcdd6 = new c03b415ded886c3c5a1ba0440979bcdd5(0)
			{
				c1b0bbfd4af1b6a4c0c71a5dd3c509812 = c1b0bbfd4af1b6a4c0c71a5dd3c509812
			};
			goto IL_004f;
			IL_004f:
			c03b415ded886c3c5a1ba0440979bcdd6.ceabb47d7d959545d89c4300eabbdd7c2 = c6f203721b629fec4c93cc63ff1172fc4;
			c03b415ded886c3c5a1ba0440979bcdd6.cfd8883387afb9cc25a2ac9150de0f66b = cd1939b65df62ed5f7a2112466dabd8f0;
			c03b415ded886c3c5a1ba0440979bcdd6.c1b4918d9d3cc94171c67339d1fc78b0f = cfd54ba1e25c70994847ab953f2228207;
			c03b415ded886c3c5a1ba0440979bcdd6.cd2abebf7ff6ec7d035c28a04faaa459b = c1c245528512f0ae5611e6e833a70158f;
			return c03b415ded886c3c5a1ba0440979bcdd6;
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
						goto IL_0216;
					}
					c94f69126adc708dfc5f5fd5b63c52835 = 2;
					goto IL_01dc;
				}
				c94f69126adc708dfc5f5fd5b63c52835 = -1;
				c627c865255449b302ea5d4e57c541bac = Vector3F.One / ceabb47d7d959545d89c4300eabbdd7c2;
				cee960f0c1783e62f81ed3b57ce8bc8db = Vector3F.One / c1b4918d9d3cc94171c67339d1fc78b0f;
				cba6ff7b0b103371a1ac9346351109d2a = cd2abebf7ff6ec7d035c28a04faaa459b;
				cdeaa38d7a688e83f4875df37e9b278b4 = cd2abebf7ff6ec7d035c28a04faaa459b.Inverse;
				c003390ba045db1605d3972e270349aa0 = cfd8883387afb9cc25a2ac9150de0f66b.Aabb;
				c003390ba045db1605d3972e270349aa0 = c003390ba045db1605d3972e270349aa0.GetAabb(c1b4918d9d3cc94171c67339d1fc78b0f, cba6ff7b0b103371a1ac9346351109d2a);
				c003390ba045db1605d3972e270349aa0.Scale(c627c865255449b302ea5d4e57c541bac);
				cd540e50afb414c19e9c6a91e0e0771e9 = c1b0bbfd4af1b6a4c0c71a5dd3c509812.c4d76d858205a75f799980f42e41cbe5b(c003390ba045db1605d3972e270349aa0).GetEnumerator();
				c94f69126adc708dfc5f5fd5b63c52835 = 1;
				goto IL_01fe;
				IL_01fe:
				if (cd540e50afb414c19e9c6a91e0e0771e9.MoveNext())
				{
					c331daf4df19077e1da4ef2e31e448630 = cd540e50afb414c19e9c6a91e0e0771e9.Current;
					cb4f10e91ac09da8777031538da821afc = c331daf4df19077e1da4ef2e31e448630.c3e263e990bc8d6c3a2d636ac7a19c6c6.GetAabb(ceabb47d7d959545d89c4300eabbdd7c2, cdeaa38d7a688e83f4875df37e9b278b4);
					cb4f10e91ac09da8777031538da821afc.Scale(cee960f0c1783e62f81ed3b57ce8bc8db);
					cb52708b4b3b6db12324dc3c2787a3b59 = cfd8883387afb9cc25a2ac9150de0f66b.GetOverlaps(cb4f10e91ac09da8777031538da821afc).GetEnumerator();
					c94f69126adc708dfc5f5fd5b63c52835 = 2;
					goto IL_01dc;
				}
				cec828109e929d16f5e014baed326e6b3();
				goto IL_0216;
				IL_01dc:
				while (cb52708b4b3b6db12324dc3c2787a3b59.MoveNext())
				{
					cee9bfa84b8333878ebfaa30c3db01e06 = cb52708b4b3b6db12324dc3c2787a3b59.Current;
					ref Pair<T> reference = ref cc30c4b3009a09c1a9241b11b7c893d0d;
					reference = new Pair<T>(c331daf4df19077e1da4ef2e31e448630.c652f269a40a2519b8658ffbade581b4e, cee9bfa84b8333878ebfaa30c3db01e06);
					if (c1b0bbfd4af1b6a4c0c71a5dd3c509812.Filter != null)
					{
						while (true)
						{
							switch (5)
							{
							case 0:
								continue;
							}
							if (1 == 0)
							{
								__ldtoken(AdaptiveAabbTree<>.c03b415ded886c3c5a1ba0440979bcdd5.MoveNext());
							}
							if (!c1b0bbfd4af1b6a4c0c71a5dd3c509812.Filter.Filter(cc30c4b3009a09c1a9241b11b7c893d0d))
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
							goto IL_01be;
						}
						continue;
					}
					goto IL_01be;
					IL_01be:
					cd179d7f18da070d164af6afcb0af45b5 = cc30c4b3009a09c1a9241b11b7c893d0d;
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
				cd9f6b50624f9f31a64230a5913006f1f();
				goto IL_01fe;
				IL_0216:
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
							cd9f6b50624f9f31a64230a5913006f1f();
						}
					}
					break;
				}
				finally
				{
					cec828109e929d16f5e014baed326e6b3();
				}
			}
		}

		void IDisposable.Dispose()
		{
			//ILSpy generated this explicit interface implementation from .override directive in c087ddb9300ad4d53672b7a60e6d76672
			this.c087ddb9300ad4d53672b7a60e6d76672();
		}

		private void cec828109e929d16f5e014baed326e6b3()
		{
			c94f69126adc708dfc5f5fd5b63c52835 = -1;
			if (cd540e50afb414c19e9c6a91e0e0771e9 != null)
			{
				cd540e50afb414c19e9c6a91e0e0771e9.Dispose();
			}
		}

		private void cd9f6b50624f9f31a64230a5913006f1f()
		{
			c94f69126adc708dfc5f5fd5b63c52835 = 1;
			if (cb52708b4b3b6db12324dc3c2787a3b59 == null)
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
					__ldtoken(AdaptiveAabbTree<>.c03b415ded886c3c5a1ba0440979bcdd5.cd9f6b50624f9f31a64230a5913006f1f());
				}
				cb52708b4b3b6db12324dc3c2787a3b59.Dispose();
				return;
			}
		}
	}

	[CompilerGenerated]
	private sealed class cb0dd5f27e52a6601f14fdfd5df6b33b4 : IEnumerable<Pair<T>>, IEnumerator<Pair<T>>, IEnumerable, IEnumerator, IDisposable
	{
		private Pair<T> cd179d7f18da070d164af6afcb0af45b5;

		private int c94f69126adc708dfc5f5fd5b63c52835;

		private int c0e8a058e779916e7715d7616fb0da9e7;

		public AdaptiveAabbTree<T> c1b0bbfd4af1b6a4c0c71a5dd3c509812;

		public Vector3F ceabb47d7d959545d89c4300eabbdd7c2;

		public Vector3F c6f203721b629fec4c93cc63ff1172fc4;

		public AdaptiveAabbTree<T> ca9802b6266a4b983f751fb2d730561ca;

		public AdaptiveAabbTree<T> c6b71d229c51c908f777d8166aeaf9947;

		public Vector3F c1b4918d9d3cc94171c67339d1fc78b0f;

		public Vector3F cfd54ba1e25c70994847ab953f2228207;

		public Pose cd2abebf7ff6ec7d035c28a04faaa459b;

		public Pose c1c245528512f0ae5611e6e833a70158f;

		public Vector3F c597c2260a7f8c143e715055ccba6cd27;

		public Vector3F c34c2fe6bdb9dda55558b10abaf9055b0;

		public Pose cb1d72f6525d0570f5964171455872197;

		public Stack<Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e>> c57412f9764b16d56d49553fa98f127ce;

		public Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e> cb13985df0d9293de4df749dd96d0ba0a;

		public cff48f0c06d0d98efeafd069bbfaea07e cc9c9e3cedf32c97f92af85386c21874b;

		public cff48f0c06d0d98efeafd069bbfaea07e cf3cd5f2317bab0723f0e22c29f1c9ed0;

		public Pair<T> c999cbdb1cbf2da35858148e6c296103d;

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
		public cb0dd5f27e52a6601f14fdfd5df6b33b4(int c94f69126adc708dfc5f5fd5b63c52835)
		{
			this.c94f69126adc708dfc5f5fd5b63c52835 = c94f69126adc708dfc5f5fd5b63c52835;
			c0e8a058e779916e7715d7616fb0da9e7 = Thread.CurrentThread.ManagedThreadId;
		}

		[DebuggerHidden]
		private IEnumerator<Pair<T>> c4bd63b962f346f189f5c6a4ad2e4ce8d()
		{
			cb0dd5f27e52a6601f14fdfd5df6b33b4 cb0dd5f27e52a6601f14fdfd5df6b33b5;
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
					__ldtoken(AdaptiveAabbTree<>.cb0dd5f27e52a6601f14fdfd5df6b33b4.c4bd63b962f346f189f5c6a4ad2e4ce8d());
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
					cb0dd5f27e52a6601f14fdfd5df6b33b5 = this;
					goto IL_0059;
				}
			}
			cb0dd5f27e52a6601f14fdfd5df6b33b5 = new cb0dd5f27e52a6601f14fdfd5df6b33b4(0)
			{
				c1b0bbfd4af1b6a4c0c71a5dd3c509812 = c1b0bbfd4af1b6a4c0c71a5dd3c509812
			};
			goto IL_0059;
			IL_0059:
			cb0dd5f27e52a6601f14fdfd5df6b33b5.ceabb47d7d959545d89c4300eabbdd7c2 = c6f203721b629fec4c93cc63ff1172fc4;
			cb0dd5f27e52a6601f14fdfd5df6b33b5.ca9802b6266a4b983f751fb2d730561ca = c6b71d229c51c908f777d8166aeaf9947;
			cb0dd5f27e52a6601f14fdfd5df6b33b5.c1b4918d9d3cc94171c67339d1fc78b0f = cfd54ba1e25c70994847ab953f2228207;
			cb0dd5f27e52a6601f14fdfd5df6b33b5.cd2abebf7ff6ec7d035c28a04faaa459b = c1c245528512f0ae5611e6e833a70158f;
			return cb0dd5f27e52a6601f14fdfd5df6b33b5;
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
				c597c2260a7f8c143e715055ccba6cd27 = ceabb47d7d959545d89c4300eabbdd7c2;
				c34c2fe6bdb9dda55558b10abaf9055b0 = c1b4918d9d3cc94171c67339d1fc78b0f;
				cb1d72f6525d0570f5964171455872197 = cd2abebf7ff6ec7d035c28a04faaa459b;
				c57412f9764b16d56d49553fa98f127ce = ResourcePools<Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e>>.Stacks.Obtain();
				c57412f9764b16d56d49553fa98f127ce.Push(new Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e>(c1b0bbfd4af1b6a4c0c71a5dd3c509812.c3cb2c0edc4d55dd1410e3483d1ef60d4, ca9802b6266a4b983f751fb2d730561ca.c3cb2c0edc4d55dd1410e3483d1ef60d4));
				goto IL_0310;
			case 1:
				{
					c94f69126adc708dfc5f5fd5b63c52835 = -1;
					goto IL_0310;
				}
				IL_0310:
				while (c57412f9764b16d56d49553fa98f127ce.Count > 0)
				{
					cb13985df0d9293de4df749dd96d0ba0a = c57412f9764b16d56d49553fa98f127ce.Pop();
					cc9c9e3cedf32c97f92af85386c21874b = cb13985df0d9293de4df749dd96d0ba0a.First;
					cf3cd5f2317bab0723f0e22c29f1c9ed0 = cb13985df0d9293de4df749dd96d0ba0a.Second;
					cc9c9e3cedf32c97f92af85386c21874b.c2c1eca2324d13154ef9733df393024a3 = true;
					cf3cd5f2317bab0723f0e22c29f1c9ed0.c2c1eca2324d13154ef9733df393024a3 = true;
					if (!AdaptiveAabbTree<T>.cd0cfe897426aab73c90b8217df390607(cc9c9e3cedf32c97f92af85386c21874b, c597c2260a7f8c143e715055ccba6cd27, cf3cd5f2317bab0723f0e22c29f1c9ed0, c34c2fe6bdb9dda55558b10abaf9055b0, cb1d72f6525d0570f5964171455872197))
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
						__ldtoken(AdaptiveAabbTree<>.cb0dd5f27e52a6601f14fdfd5df6b33b4.MoveNext());
					}
					if (!cc9c9e3cedf32c97f92af85386c21874b.cfd73e16b19bb8bde65db991a787eb5ad)
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
						if (!cf3cd5f2317bab0723f0e22c29f1c9ed0.cfd73e16b19bb8bde65db991a787eb5ad)
						{
							AdaptiveAabbTree<T>.c18ac18e9f88283e6a56a9ec9cb6b9e55(cc9c9e3cedf32c97f92af85386c21874b);
							AdaptiveAabbTree<T>.c18ac18e9f88283e6a56a9ec9cb6b9e55(cf3cd5f2317bab0723f0e22c29f1c9ed0);
							c57412f9764b16d56d49553fa98f127ce.Push(new Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e>(cc9c9e3cedf32c97f92af85386c21874b.c2d9d869d0c8fd5fb55f452375f580a4a, cf3cd5f2317bab0723f0e22c29f1c9ed0.c2d9d869d0c8fd5fb55f452375f580a4a));
							c57412f9764b16d56d49553fa98f127ce.Push(new Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e>(cc9c9e3cedf32c97f92af85386c21874b.c6d3fb34101b9e1ce92055c2914d2d1de, cf3cd5f2317bab0723f0e22c29f1c9ed0.c2d9d869d0c8fd5fb55f452375f580a4a));
							c57412f9764b16d56d49553fa98f127ce.Push(new Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e>(cc9c9e3cedf32c97f92af85386c21874b.c2d9d869d0c8fd5fb55f452375f580a4a, cf3cd5f2317bab0723f0e22c29f1c9ed0.c6d3fb34101b9e1ce92055c2914d2d1de));
							c57412f9764b16d56d49553fa98f127ce.Push(new Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e>(cc9c9e3cedf32c97f92af85386c21874b.c6d3fb34101b9e1ce92055c2914d2d1de, cf3cd5f2317bab0723f0e22c29f1c9ed0.c6d3fb34101b9e1ce92055c2914d2d1de));
						}
						else
						{
							AdaptiveAabbTree<T>.c18ac18e9f88283e6a56a9ec9cb6b9e55(cc9c9e3cedf32c97f92af85386c21874b);
							c57412f9764b16d56d49553fa98f127ce.Push(new Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e>(cc9c9e3cedf32c97f92af85386c21874b.c2d9d869d0c8fd5fb55f452375f580a4a, cf3cd5f2317bab0723f0e22c29f1c9ed0));
							c57412f9764b16d56d49553fa98f127ce.Push(new Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e>(cc9c9e3cedf32c97f92af85386c21874b.c6d3fb34101b9e1ce92055c2914d2d1de, cf3cd5f2317bab0723f0e22c29f1c9ed0));
						}
						continue;
					}
					if (!cf3cd5f2317bab0723f0e22c29f1c9ed0.cfd73e16b19bb8bde65db991a787eb5ad)
					{
						AdaptiveAabbTree<T>.c18ac18e9f88283e6a56a9ec9cb6b9e55(cf3cd5f2317bab0723f0e22c29f1c9ed0);
						c57412f9764b16d56d49553fa98f127ce.Push(new Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e>(cc9c9e3cedf32c97f92af85386c21874b, cf3cd5f2317bab0723f0e22c29f1c9ed0.c2d9d869d0c8fd5fb55f452375f580a4a));
						c57412f9764b16d56d49553fa98f127ce.Push(new Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e>(cc9c9e3cedf32c97f92af85386c21874b, cf3cd5f2317bab0723f0e22c29f1c9ed0.c6d3fb34101b9e1ce92055c2914d2d1de));
						continue;
					}
					ref Pair<T> reference = ref c999cbdb1cbf2da35858148e6c296103d;
					reference = new Pair<T>(cc9c9e3cedf32c97f92af85386c21874b.c652f269a40a2519b8658ffbade581b4e, cf3cd5f2317bab0723f0e22c29f1c9ed0.c652f269a40a2519b8658ffbade581b4e);
					if (c1b0bbfd4af1b6a4c0c71a5dd3c509812.Filter != null)
					{
						if (!c1b0bbfd4af1b6a4c0c71a5dd3c509812.Filter.Filter(c999cbdb1cbf2da35858148e6c296103d))
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
					}
					cd179d7f18da070d164af6afcb0af45b5 = c999cbdb1cbf2da35858148e6c296103d;
					c94f69126adc708dfc5f5fd5b63c52835 = 1;
					return true;
				}
				ResourcePools<Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e>>.Stacks.Recycle(c57412f9764b16d56d49553fa98f127ce);
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
	private sealed class ca831cb15c64d60e3f4a4dd560aea08bf : IEnumerable<T>, IEnumerator<T>, IEnumerable, IEnumerator, IDisposable
	{
		private T cd179d7f18da070d164af6afcb0af45b5;

		private int c94f69126adc708dfc5f5fd5b63c52835;

		private int c0e8a058e779916e7715d7616fb0da9e7;

		public AdaptiveAabbTree<T> c1b0bbfd4af1b6a4c0c71a5dd3c509812;

		public Aabb cdc3439410d15f28489c964ee87b88fe9;

		public Aabb cc3ae004bdecc8a5e2b7f6f0efd06d1d5;

		public Stack<cff48f0c06d0d98efeafd069bbfaea07e> c4e439b9da8ccd597a6b084d977710fb0;

		public cff48f0c06d0d98efeafd069bbfaea07e c07ca91272260dc9c91529c6b41e3207e;

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
		public ca831cb15c64d60e3f4a4dd560aea08bf(int c94f69126adc708dfc5f5fd5b63c52835)
		{
			this.c94f69126adc708dfc5f5fd5b63c52835 = c94f69126adc708dfc5f5fd5b63c52835;
			c0e8a058e779916e7715d7616fb0da9e7 = Thread.CurrentThread.ManagedThreadId;
		}

		[DebuggerHidden]
		private IEnumerator<T> c0843ff4e059c4450bad00d5ba0b6ef48()
		{
			ca831cb15c64d60e3f4a4dd560aea08bf ca831cb15c64d60e3f4a4dd560aea08bf2;
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
					__ldtoken(AdaptiveAabbTree<>.ca831cb15c64d60e3f4a4dd560aea08bf.c0843ff4e059c4450bad00d5ba0b6ef48());
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
					ca831cb15c64d60e3f4a4dd560aea08bf2 = this;
					goto IL_0059;
				}
			}
			ca831cb15c64d60e3f4a4dd560aea08bf2 = new ca831cb15c64d60e3f4a4dd560aea08bf(0)
			{
				c1b0bbfd4af1b6a4c0c71a5dd3c509812 = c1b0bbfd4af1b6a4c0c71a5dd3c509812
			};
			goto IL_0059;
			IL_0059:
			ca831cb15c64d60e3f4a4dd560aea08bf2.cdc3439410d15f28489c964ee87b88fe9 = cc3ae004bdecc8a5e2b7f6f0efd06d1d5;
			return ca831cb15c64d60e3f4a4dd560aea08bf2;
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
				c4e439b9da8ccd597a6b084d977710fb0 = ResourcePools<cff48f0c06d0d98efeafd069bbfaea07e>.Stacks.Obtain();
				c4e439b9da8ccd597a6b084d977710fb0.Push(c1b0bbfd4af1b6a4c0c71a5dd3c509812.c3cb2c0edc4d55dd1410e3483d1ef60d4);
				goto IL_0106;
			case 1:
				{
					c94f69126adc708dfc5f5fd5b63c52835 = -1;
					goto IL_0106;
				}
				IL_0106:
				while (c4e439b9da8ccd597a6b084d977710fb0.Count > 0)
				{
					c07ca91272260dc9c91529c6b41e3207e = c4e439b9da8ccd597a6b084d977710fb0.Pop();
					c07ca91272260dc9c91529c6b41e3207e.c2c1eca2324d13154ef9733df393024a3 = true;
					if (GeometryHelper.HaveContact(c07ca91272260dc9c91529c6b41e3207e.c3e263e990bc8d6c3a2d636ac7a19c6c6, cdc3439410d15f28489c964ee87b88fe9))
					{
						if (c07ca91272260dc9c91529c6b41e3207e.cfd73e16b19bb8bde65db991a787eb5ad)
						{
							cd179d7f18da070d164af6afcb0af45b5 = c07ca91272260dc9c91529c6b41e3207e.c652f269a40a2519b8658ffbade581b4e;
							c94f69126adc708dfc5f5fd5b63c52835 = 1;
							return true;
						}
						AdaptiveAabbTree<T>.c18ac18e9f88283e6a56a9ec9cb6b9e55(c07ca91272260dc9c91529c6b41e3207e);
						c4e439b9da8ccd597a6b084d977710fb0.Push(c07ca91272260dc9c91529c6b41e3207e.c2d9d869d0c8fd5fb55f452375f580a4a);
						c4e439b9da8ccd597a6b084d977710fb0.Push(c07ca91272260dc9c91529c6b41e3207e.c6d3fb34101b9e1ce92055c2914d2d1de);
					}
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
					__ldtoken(AdaptiveAabbTree<>.ca831cb15c64d60e3f4a4dd560aea08bf.MoveNext());
				}
				ResourcePools<cff48f0c06d0d98efeafd069bbfaea07e>.Stacks.Recycle(c4e439b9da8ccd597a6b084d977710fb0);
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
	private sealed class c6ec9e72fe859503f5be53a128f1893fd : IEnumerable<T>, IEnumerator<T>, IEnumerable, IEnumerator, IDisposable
	{
		private T cd179d7f18da070d164af6afcb0af45b5;

		private int c94f69126adc708dfc5f5fd5b63c52835;

		private int c0e8a058e779916e7715d7616fb0da9e7;

		public AdaptiveAabbTree<T> c1b0bbfd4af1b6a4c0c71a5dd3c509812;

		public cff48f0c06d0d98efeafd069bbfaea07e ce9beaab8ed4de70dd0e96a36b4c42105;

		public cff48f0c06d0d98efeafd069bbfaea07e ca8b7e7cbb5469712cd98441c75a65ad8;

		public Stack<cff48f0c06d0d98efeafd069bbfaea07e> c306eb3bb1312a01c74d6b8221d2d3877;

		public cff48f0c06d0d98efeafd069bbfaea07e c0d7cacc95a156eb70183276c7b73c82a;

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
		public c6ec9e72fe859503f5be53a128f1893fd(int c94f69126adc708dfc5f5fd5b63c52835)
		{
			this.c94f69126adc708dfc5f5fd5b63c52835 = c94f69126adc708dfc5f5fd5b63c52835;
			c0e8a058e779916e7715d7616fb0da9e7 = Thread.CurrentThread.ManagedThreadId;
		}

		[DebuggerHidden]
		private IEnumerator<T> c0843ff4e059c4450bad00d5ba0b6ef48()
		{
			c6ec9e72fe859503f5be53a128f1893fd c6ec9e72fe859503f5be53a128f1893fd2;
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
					__ldtoken(AdaptiveAabbTree<>.c6ec9e72fe859503f5be53a128f1893fd.c0843ff4e059c4450bad00d5ba0b6ef48());
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
					c6ec9e72fe859503f5be53a128f1893fd2 = this;
					goto IL_0059;
				}
			}
			c6ec9e72fe859503f5be53a128f1893fd2 = new c6ec9e72fe859503f5be53a128f1893fd(0)
			{
				c1b0bbfd4af1b6a4c0c71a5dd3c509812 = c1b0bbfd4af1b6a4c0c71a5dd3c509812
			};
			goto IL_0059;
			IL_0059:
			c6ec9e72fe859503f5be53a128f1893fd2.ce9beaab8ed4de70dd0e96a36b4c42105 = ca8b7e7cbb5469712cd98441c75a65ad8;
			return c6ec9e72fe859503f5be53a128f1893fd2;
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
					switch (4)
					{
					case 0:
						continue;
					}
					break;
				}
				if (1 == 0)
				{
					__ldtoken(AdaptiveAabbTree<>.c6ec9e72fe859503f5be53a128f1893fd.MoveNext());
				}
				c306eb3bb1312a01c74d6b8221d2d3877 = ResourcePools<cff48f0c06d0d98efeafd069bbfaea07e>.Stacks.Obtain();
				c306eb3bb1312a01c74d6b8221d2d3877.Push(c1b0bbfd4af1b6a4c0c71a5dd3c509812.c3cb2c0edc4d55dd1410e3483d1ef60d4);
				goto IL_012d;
			case 1:
				{
					c94f69126adc708dfc5f5fd5b63c52835 = -1;
					goto IL_012d;
				}
				IL_012d:
				while (c306eb3bb1312a01c74d6b8221d2d3877.Count > 0)
				{
					c0d7cacc95a156eb70183276c7b73c82a = c306eb3bb1312a01c74d6b8221d2d3877.Pop();
					c0d7cacc95a156eb70183276c7b73c82a.c2c1eca2324d13154ef9733df393024a3 = true;
					if (c0d7cacc95a156eb70183276c7b73c82a != ce9beaab8ed4de70dd0e96a36b4c42105 && GeometryHelper.HaveContact(c0d7cacc95a156eb70183276c7b73c82a.c3e263e990bc8d6c3a2d636ac7a19c6c6, ce9beaab8ed4de70dd0e96a36b4c42105.c3e263e990bc8d6c3a2d636ac7a19c6c6))
					{
						if (c0d7cacc95a156eb70183276c7b73c82a.cfd73e16b19bb8bde65db991a787eb5ad)
						{
							cd179d7f18da070d164af6afcb0af45b5 = c0d7cacc95a156eb70183276c7b73c82a.c652f269a40a2519b8658ffbade581b4e;
							c94f69126adc708dfc5f5fd5b63c52835 = 1;
							return true;
						}
						AdaptiveAabbTree<T>.c18ac18e9f88283e6a56a9ec9cb6b9e55(c0d7cacc95a156eb70183276c7b73c82a);
						c306eb3bb1312a01c74d6b8221d2d3877.Push(c0d7cacc95a156eb70183276c7b73c82a.c2d9d869d0c8fd5fb55f452375f580a4a);
						c306eb3bb1312a01c74d6b8221d2d3877.Push(c0d7cacc95a156eb70183276c7b73c82a.c6d3fb34101b9e1ce92055c2914d2d1de);
					}
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
				ResourcePools<cff48f0c06d0d98efeafd069bbfaea07e>.Stacks.Recycle(c306eb3bb1312a01c74d6b8221d2d3877);
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
	private sealed class c8137f287f36c442d9765d959579fadce : IEnumerable<cff48f0c06d0d98efeafd069bbfaea07e>, IEnumerator<cff48f0c06d0d98efeafd069bbfaea07e>, IEnumerable, IEnumerator, IDisposable
	{
		private cff48f0c06d0d98efeafd069bbfaea07e cd179d7f18da070d164af6afcb0af45b5;

		private int c94f69126adc708dfc5f5fd5b63c52835;

		private int c0e8a058e779916e7715d7616fb0da9e7;

		public AdaptiveAabbTree<T> c1b0bbfd4af1b6a4c0c71a5dd3c509812;

		public Aabb cdc3439410d15f28489c964ee87b88fe9;

		public Aabb cc3ae004bdecc8a5e2b7f6f0efd06d1d5;

		public Stack<cff48f0c06d0d98efeafd069bbfaea07e> ce03673dd66404a7b3f8ce7639922d00f;

		public cff48f0c06d0d98efeafd069bbfaea07e c1eda30b1b2b6eb395e103f3d105d8713;

		cff48f0c06d0d98efeafd069bbfaea07e IEnumerator<cff48f0c06d0d98efeafd069bbfaea07e>.Current
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
		public c8137f287f36c442d9765d959579fadce(int c94f69126adc708dfc5f5fd5b63c52835)
		{
			this.c94f69126adc708dfc5f5fd5b63c52835 = c94f69126adc708dfc5f5fd5b63c52835;
			c0e8a058e779916e7715d7616fb0da9e7 = Thread.CurrentThread.ManagedThreadId;
		}

		[DebuggerHidden]
		private IEnumerator<cff48f0c06d0d98efeafd069bbfaea07e> c76e2dc0f88b785cf898ef8597e9c25df()
		{
			c8137f287f36c442d9765d959579fadce c8137f287f36c442d9765d959579fadce2;
			if (Thread.CurrentThread.ManagedThreadId == c0e8a058e779916e7715d7616fb0da9e7 && c94f69126adc708dfc5f5fd5b63c52835 == -2)
			{
				c94f69126adc708dfc5f5fd5b63c52835 = 0;
				c8137f287f36c442d9765d959579fadce2 = this;
			}
			else
			{
				c8137f287f36c442d9765d959579fadce2 = new c8137f287f36c442d9765d959579fadce(0)
				{
					c1b0bbfd4af1b6a4c0c71a5dd3c509812 = c1b0bbfd4af1b6a4c0c71a5dd3c509812
				};
			}
			c8137f287f36c442d9765d959579fadce2.cdc3439410d15f28489c964ee87b88fe9 = cc3ae004bdecc8a5e2b7f6f0efd06d1d5;
			return c8137f287f36c442d9765d959579fadce2;
		}

		IEnumerator<cff48f0c06d0d98efeafd069bbfaea07e> IEnumerable<cff48f0c06d0d98efeafd069bbfaea07e>.GetEnumerator()
		{
			//ILSpy generated this explicit interface implementation from .override directive in c76e2dc0f88b785cf898ef8597e9c25df
			return this.c76e2dc0f88b785cf898ef8597e9c25df();
		}

		[DebuggerHidden]
		private IEnumerator c63811cf6ff5c0b86c54fd5037c6af6ad()
		{
			return c76e2dc0f88b785cf898ef8597e9c25df();
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
					switch (5)
					{
					case 0:
						continue;
					}
					break;
				}
				if (1 == 0)
				{
					__ldtoken(AdaptiveAabbTree<>.c8137f287f36c442d9765d959579fadce.MoveNext());
				}
				ce03673dd66404a7b3f8ce7639922d00f = ResourcePools<cff48f0c06d0d98efeafd069bbfaea07e>.Stacks.Obtain();
				ce03673dd66404a7b3f8ce7639922d00f.Push(c1b0bbfd4af1b6a4c0c71a5dd3c509812.c3cb2c0edc4d55dd1410e3483d1ef60d4);
				goto IL_0118;
			case 1:
				{
					c94f69126adc708dfc5f5fd5b63c52835 = -1;
					goto IL_0118;
				}
				IL_0118:
				while (ce03673dd66404a7b3f8ce7639922d00f.Count > 0)
				{
					c1eda30b1b2b6eb395e103f3d105d8713 = ce03673dd66404a7b3f8ce7639922d00f.Pop();
					c1eda30b1b2b6eb395e103f3d105d8713.c2c1eca2324d13154ef9733df393024a3 = true;
					if (GeometryHelper.HaveContact(c1eda30b1b2b6eb395e103f3d105d8713.c3e263e990bc8d6c3a2d636ac7a19c6c6, cdc3439410d15f28489c964ee87b88fe9))
					{
						if (c1eda30b1b2b6eb395e103f3d105d8713.cfd73e16b19bb8bde65db991a787eb5ad)
						{
							cd179d7f18da070d164af6afcb0af45b5 = c1eda30b1b2b6eb395e103f3d105d8713;
							c94f69126adc708dfc5f5fd5b63c52835 = 1;
							return true;
						}
						AdaptiveAabbTree<T>.c18ac18e9f88283e6a56a9ec9cb6b9e55(c1eda30b1b2b6eb395e103f3d105d8713);
						ce03673dd66404a7b3f8ce7639922d00f.Push(c1eda30b1b2b6eb395e103f3d105d8713.c2d9d869d0c8fd5fb55f452375f580a4a);
						ce03673dd66404a7b3f8ce7639922d00f.Push(c1eda30b1b2b6eb395e103f3d105d8713.c6d3fb34101b9e1ce92055c2914d2d1de);
					}
				}
				ResourcePools<cff48f0c06d0d98efeafd069bbfaea07e>.Stacks.Recycle(ce03673dd66404a7b3f8ce7639922d00f);
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
	private sealed class cca2a86f2c74bbfda9787a352fc8b1fb1 : IEnumerable<T>, IEnumerator<T>, IEnumerable, IEnumerator, IDisposable
	{
		private T cd179d7f18da070d164af6afcb0af45b5;

		private int c94f69126adc708dfc5f5fd5b63c52835;

		private int c0e8a058e779916e7715d7616fb0da9e7;

		public AdaptiveAabbTree<T> c1b0bbfd4af1b6a4c0c71a5dd3c509812;

		public Ray c2d769fcfe2c7e7844e7de692907680c8;

		public Ray c8b1773219813f762be9a1bd49ee5cb56;

		public Vector3F c36e8fd39d8443bd41c3fa09f24ac129c;

		public float c106053e9f387fcc28b7ba7f04b2f9756;

		public Stack<cff48f0c06d0d98efeafd069bbfaea07e> c9a9efefc6a1ddc6e0abbf2648fa1be01;

		public cff48f0c06d0d98efeafd069bbfaea07e ce59c442576e743945854567c4d49a1ea;

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
		public cca2a86f2c74bbfda9787a352fc8b1fb1(int c94f69126adc708dfc5f5fd5b63c52835)
		{
			this.c94f69126adc708dfc5f5fd5b63c52835 = c94f69126adc708dfc5f5fd5b63c52835;
			c0e8a058e779916e7715d7616fb0da9e7 = Thread.CurrentThread.ManagedThreadId;
		}

		[DebuggerHidden]
		private IEnumerator<T> c0843ff4e059c4450bad00d5ba0b6ef48()
		{
			cca2a86f2c74bbfda9787a352fc8b1fb1 cca2a86f2c74bbfda9787a352fc8b1fb2;
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
					__ldtoken(AdaptiveAabbTree<>.cca2a86f2c74bbfda9787a352fc8b1fb1.c0843ff4e059c4450bad00d5ba0b6ef48());
				}
				c94f69126adc708dfc5f5fd5b63c52835 = 0;
				cca2a86f2c74bbfda9787a352fc8b1fb2 = this;
			}
			else
			{
				cca2a86f2c74bbfda9787a352fc8b1fb2 = new cca2a86f2c74bbfda9787a352fc8b1fb1(0)
				{
					c1b0bbfd4af1b6a4c0c71a5dd3c509812 = c1b0bbfd4af1b6a4c0c71a5dd3c509812
				};
			}
			cca2a86f2c74bbfda9787a352fc8b1fb2.c2d769fcfe2c7e7844e7de692907680c8 = c8b1773219813f762be9a1bd49ee5cb56;
			return cca2a86f2c74bbfda9787a352fc8b1fb2;
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
					switch (1)
					{
					case 0:
						continue;
					}
					break;
				}
				if (1 == 0)
				{
					__ldtoken(AdaptiveAabbTree<>.cca2a86f2c74bbfda9787a352fc8b1fb1.MoveNext());
				}
				ref Vector3F reference = ref c36e8fd39d8443bd41c3fa09f24ac129c;
				reference = new Vector3F(1f / c2d769fcfe2c7e7844e7de692907680c8.Direction.X, 1f / c2d769fcfe2c7e7844e7de692907680c8.Direction.Y, 1f / c2d769fcfe2c7e7844e7de692907680c8.Direction.Z);
				c106053e9f387fcc28b7ba7f04b2f9756 = Numeric.EpsilonF * (1f + c1b0bbfd4af1b6a4c0c71a5dd3c509812.Aabb.Extent.Length);
				c9a9efefc6a1ddc6e0abbf2648fa1be01 = ResourcePools<cff48f0c06d0d98efeafd069bbfaea07e>.Stacks.Obtain();
				c9a9efefc6a1ddc6e0abbf2648fa1be01.Push(c1b0bbfd4af1b6a4c0c71a5dd3c509812.c3cb2c0edc4d55dd1410e3483d1ef60d4);
				goto IL_01c6;
			}
			case 1:
				{
					c94f69126adc708dfc5f5fd5b63c52835 = -1;
					goto IL_01c6;
				}
				IL_01c6:
				while (c9a9efefc6a1ddc6e0abbf2648fa1be01.Count > 0)
				{
					ce59c442576e743945854567c4d49a1ea = c9a9efefc6a1ddc6e0abbf2648fa1be01.Pop();
					ce59c442576e743945854567c4d49a1ea.c2c1eca2324d13154ef9733df393024a3 = true;
					if (!GeometryHelper.cdd6d17271fec83883494ba8e43eede6a(ce59c442576e743945854567c4d49a1ea.c3e263e990bc8d6c3a2d636ac7a19c6c6, c2d769fcfe2c7e7844e7de692907680c8.Origin, c36e8fd39d8443bd41c3fa09f24ac129c, c2d769fcfe2c7e7844e7de692907680c8.Length, c106053e9f387fcc28b7ba7f04b2f9756))
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
					if (ce59c442576e743945854567c4d49a1ea.cfd73e16b19bb8bde65db991a787eb5ad)
					{
						cd179d7f18da070d164af6afcb0af45b5 = ce59c442576e743945854567c4d49a1ea.c652f269a40a2519b8658ffbade581b4e;
						c94f69126adc708dfc5f5fd5b63c52835 = 1;
						return true;
					}
					AdaptiveAabbTree<T>.c18ac18e9f88283e6a56a9ec9cb6b9e55(ce59c442576e743945854567c4d49a1ea);
					c9a9efefc6a1ddc6e0abbf2648fa1be01.Push(ce59c442576e743945854567c4d49a1ea.c2d9d869d0c8fd5fb55f452375f580a4a);
					c9a9efefc6a1ddc6e0abbf2648fa1be01.Push(ce59c442576e743945854567c4d49a1ea.c6d3fb34101b9e1ce92055c2914d2d1de);
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
				ResourcePools<cff48f0c06d0d98efeafd069bbfaea07e>.Stacks.Recycle(c9a9efefc6a1ddc6e0abbf2648fa1be01);
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

	private static readonly ResourcePool<cff48f0c06d0d98efeafd069bbfaea07e> c53fef33d7344726da08aeb063ec62121;

	private cff48f0c06d0d98efeafd069bbfaea07e c3cb2c0edc4d55dd1410e3483d1ef60d4;

	private readonly List<cff48f0c06d0d98efeafd069bbfaea07e> cd311d3823f638b023eacb767b312cc3e = new List<cff48f0c06d0d98efeafd069bbfaea07e>();

	[CompilerGenerated]
	private static Func<cbe5649b18b8b0caa00be9ad49188b193<T>> c753747c901f3e80c0e263e414196b8eb;

	[CompilerGenerated]
	private static Func<cff48f0c06d0d98efeafd069bbfaea07e> c7ceedd7faa993df5e1ba6d1f2f092879;

	[CompilerGenerated]
	private static Action<cff48f0c06d0d98efeafd069bbfaea07e> cd98e318a6e2ce0b87a7e69ed6ad43624;

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

	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.Geometry.Partitioning.AdaptiveAabbTree`1" /> class</summary>
	public AdaptiveAabbTree()
	{
	}

	static AdaptiveAabbTree()
	{
		Func<cff48f0c06d0d98efeafd069bbfaea07e> create = () => new cff48f0c06d0d98efeafd069bbfaea07e();
		if (cd98e318a6e2ce0b87a7e69ed6ad43624 == null)
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
				__ldtoken(AdaptiveAabbTree<>._002Ecctor());
			}
			cd98e318a6e2ce0b87a7e69ed6ad43624 = (cff48f0c06d0d98efeafd069bbfaea07e c7f587bd57d47897172f47483e6bcca68) =>
			{
				c7f587bd57d47897172f47483e6bcca68.c6d3fb34101b9e1ce92055c2914d2d1de = null;
				c7f587bd57d47897172f47483e6bcca68.c2d9d869d0c8fd5fb55f452375f580a4a = null;
				c7f587bd57d47897172f47483e6bcca68.c652f269a40a2519b8658ffbade581b4e = default;
				c7f587bd57d47897172f47483e6bcca68.c2c1eca2324d13154ef9733df393024a3 = false;
				c7f587bd57d47897172f47483e6bcca68.c74dc8c78f0b932d691a8442fc47f4ad5 = true;
				if (c7f587bd57d47897172f47483e6bcca68.c9594e2c7b7942e1d22a230fdeee51f04 != null)
				{
					ResourcePools<cff48f0c06d0d98efeafd069bbfaea07e>.Lists.Recycle(c7f587bd57d47897172f47483e6bcca68.c9594e2c7b7942e1d22a230fdeee51f04);
					c7f587bd57d47897172f47483e6bcca68.c9594e2c7b7942e1d22a230fdeee51f04 = null;
				}
			};
		}
		c53fef33d7344726da08aeb063ec62121 = new ResourcePool<cff48f0c06d0d98efeafd069bbfaea07e>(create, null, cd98e318a6e2ce0b87a7e69ed6ad43624);
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
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(994));
		}
		otherPartition.Update(forceRebuild: false);
		Update(forceRebuild: false);
		if (c3cb2c0edc4d55dd1410e3483d1ef60d4 == null)
		{
			return Enumerable.Empty<Pair<T>>();
		}
		if (!(otherPartition is AdaptiveAabbTree<T> adaptiveAabbTree))
		{
			return c0f8cf26383f225c6acffebdc67fb4382(otherPartition);
		}
		if (adaptiveAabbTree.c3cb2c0edc4d55dd1410e3483d1ef60d4 == null)
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
						__ldtoken(AdaptiveAabbTree<>.GetOverlaps(ISpatialPartition<T>));
					}
					return Enumerable.Empty<Pair<T>>();
				}
			}
		}
		return c0f8cf26383f225c6acffebdc67fb4382(adaptiveAabbTree);
	}

	private IEnumerable<Pair<T>> c0f8cf26383f225c6acffebdc67fb4382(ISpatialPartition<T> cfd8883387afb9cc25a2ac9150de0f66b)
	{
		foreach (cff48f0c06d0d98efeafd069bbfaea07e item in c4d76d858205a75f799980f42e41cbe5b(cfd8883387afb9cc25a2ac9150de0f66b.Aabb))
		{
			IEnumerable<T> overlaps = cfd8883387afb9cc25a2ac9150de0f66b.GetOverlaps(item.c3e263e990bc8d6c3a2d636ac7a19c6c6);
			foreach (T item2 in overlaps)
			{
				Pair<T> pair = new Pair<T>(second: item2, first: item.c652f269a40a2519b8658ffbade581b4e);
				if (Filter == null || Filter.Filter(pair))
				{
					yield return pair;
				}
			}
		}
	}

	private IEnumerable<Pair<T>> c0f8cf26383f225c6acffebdc67fb4382(AdaptiveAabbTree<T> ca9802b6266a4b983f751fb2d730561ca)
	{
		Stack<Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e>> stack = ResourcePools<Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e>>.Stacks.Obtain();
		stack.Push(new Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e>(c3cb2c0edc4d55dd1410e3483d1ef60d4, ca9802b6266a4b983f751fb2d730561ca.c3cb2c0edc4d55dd1410e3483d1ef60d4));
		while (stack.Count > 0)
		{
			Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e> pair = stack.Pop();
			cff48f0c06d0d98efeafd069bbfaea07e first = pair.First;
			cff48f0c06d0d98efeafd069bbfaea07e second = pair.Second;
			first.c2c1eca2324d13154ef9733df393024a3 = true;
			second.c2c1eca2324d13154ef9733df393024a3 = true;
			if (first == second)
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
					__ldtoken(AdaptiveAabbTree<>.cbb4b4a4f3e5c5a88e0a7d6d05f43fb95.MoveNext());
				}
				if (first.cfd73e16b19bb8bde65db991a787eb5ad)
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
				c18ac18e9f88283e6a56a9ec9cb6b9e55(first);
				stack.Push(new Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e>(first.c2d9d869d0c8fd5fb55f452375f580a4a, first.c2d9d869d0c8fd5fb55f452375f580a4a));
				stack.Push(new Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e>(first.c6d3fb34101b9e1ce92055c2914d2d1de, first.c2d9d869d0c8fd5fb55f452375f580a4a));
				stack.Push(new Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e>(first.c6d3fb34101b9e1ce92055c2914d2d1de, first.c6d3fb34101b9e1ce92055c2914d2d1de));
			}
			else
			{
				if (!GeometryHelper.HaveContact(first.c3e263e990bc8d6c3a2d636ac7a19c6c6, second.c3e263e990bc8d6c3a2d636ac7a19c6c6))
				{
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
				if (!first.cfd73e16b19bb8bde65db991a787eb5ad)
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
					if (!second.cfd73e16b19bb8bde65db991a787eb5ad)
					{
						c18ac18e9f88283e6a56a9ec9cb6b9e55(first);
						c18ac18e9f88283e6a56a9ec9cb6b9e55(second);
						stack.Push(new Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e>(first.c2d9d869d0c8fd5fb55f452375f580a4a, second.c2d9d869d0c8fd5fb55f452375f580a4a));
						stack.Push(new Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e>(first.c6d3fb34101b9e1ce92055c2914d2d1de, second.c2d9d869d0c8fd5fb55f452375f580a4a));
						stack.Push(new Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e>(first.c2d9d869d0c8fd5fb55f452375f580a4a, second.c6d3fb34101b9e1ce92055c2914d2d1de));
						stack.Push(new Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e>(first.c6d3fb34101b9e1ce92055c2914d2d1de, second.c6d3fb34101b9e1ce92055c2914d2d1de));
					}
					else
					{
						c18ac18e9f88283e6a56a9ec9cb6b9e55(first);
						stack.Push(new Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e>(first.c2d9d869d0c8fd5fb55f452375f580a4a, second));
						stack.Push(new Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e>(first.c6d3fb34101b9e1ce92055c2914d2d1de, second));
					}
					continue;
				}
				if (!second.cfd73e16b19bb8bde65db991a787eb5ad)
				{
					c18ac18e9f88283e6a56a9ec9cb6b9e55(second);
					stack.Push(new Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e>(first, second.c2d9d869d0c8fd5fb55f452375f580a4a));
					stack.Push(new Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e>(first, second.c6d3fb34101b9e1ce92055c2914d2d1de));
					continue;
				}
				Pair<T> pair2 = new Pair<T>(first.c652f269a40a2519b8658ffbade581b4e, second.c652f269a40a2519b8658ffbade581b4e);
				if (Filter != null)
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
					if (!Filter.Filter(pair2))
					{
						continue;
					}
				}
				yield return pair2;
			}
		}
		ResourcePools<Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e>>.Stacks.Recycle(stack);
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
			while (true)
			{
				switch (5)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(AdaptiveAabbTree<>.GetOverlaps(Vector3F, Pose, ISpatialPartition<T>, Vector3F, Pose));
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
		if (!(otherPartition is AdaptiveAabbTree<T> adaptiveAabbTree))
		{
			return c0f8cf26383f225c6acffebdc67fb4382(scale, otherPartition, otherScale, pose.Inverse * otherPose);
		}
		if (adaptiveAabbTree.c3cb2c0edc4d55dd1410e3483d1ef60d4 == null)
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
		return c0f8cf26383f225c6acffebdc67fb4382(scale, adaptiveAabbTree, otherScale, pose.Inverse * otherPose);
	}

	private IEnumerable<Pair<T>> c0f8cf26383f225c6acffebdc67fb4382(Vector3F ceabb47d7d959545d89c4300eabbdd7c2, ISpatialPartition<T> cfd8883387afb9cc25a2ac9150de0f66b, Vector3F c1b4918d9d3cc94171c67339d1fc78b0f, Pose cd2abebf7ff6ec7d035c28a04faaa459b)
	{
		Vector3F scale = Vector3F.One / ceabb47d7d959545d89c4300eabbdd7c2;
		Vector3F scale2 = Vector3F.One / c1b4918d9d3cc94171c67339d1fc78b0f;
		Pose pose = cd2abebf7ff6ec7d035c28a04faaa459b;
		Pose inverse = cd2abebf7ff6ec7d035c28a04faaa459b.Inverse;
		Aabb aabb = cfd8883387afb9cc25a2ac9150de0f66b.Aabb.GetAabb(c1b4918d9d3cc94171c67339d1fc78b0f, pose);
		aabb.Scale(scale);
		foreach (cff48f0c06d0d98efeafd069bbfaea07e item in c4d76d858205a75f799980f42e41cbe5b(aabb))
		{
			Aabb aabb2 = item.c3e263e990bc8d6c3a2d636ac7a19c6c6.GetAabb(ceabb47d7d959545d89c4300eabbdd7c2, inverse);
			aabb2.Scale(scale2);
			IEnumerator<T> enumerator2 = cfd8883387afb9cc25a2ac9150de0f66b.GetOverlaps(aabb2).GetEnumerator();
			try
			{
				while (enumerator2.MoveNext())
				{
					Pair<T> pair = new Pair<T>(second: enumerator2.Current, first: item.c652f269a40a2519b8658ffbade581b4e);
					if (Filter != null)
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
							__ldtoken(AdaptiveAabbTree<>.c03b415ded886c3c5a1ba0440979bcdd5.MoveNext());
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
							__ldtoken(AdaptiveAabbTree<>.c03b415ded886c3c5a1ba0440979bcdd5.cd9f6b50624f9f31a64230a5913006f1f());
						}
						enumerator2.Dispose();
						break;
					}
				}
			}
		}
	}

	private IEnumerable<Pair<T>> c0f8cf26383f225c6acffebdc67fb4382(Vector3F ceabb47d7d959545d89c4300eabbdd7c2, AdaptiveAabbTree<T> ca9802b6266a4b983f751fb2d730561ca, Vector3F c1b4918d9d3cc94171c67339d1fc78b0f, Pose cd2abebf7ff6ec7d035c28a04faaa459b)
	{
		Stack<Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e>> stack = ResourcePools<Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e>>.Stacks.Obtain();
		stack.Push(new Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e>(c3cb2c0edc4d55dd1410e3483d1ef60d4, ca9802b6266a4b983f751fb2d730561ca.c3cb2c0edc4d55dd1410e3483d1ef60d4));
		while (stack.Count > 0)
		{
			Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e> pair = stack.Pop();
			cff48f0c06d0d98efeafd069bbfaea07e first = pair.First;
			cff48f0c06d0d98efeafd069bbfaea07e second = pair.Second;
			first.c2c1eca2324d13154ef9733df393024a3 = true;
			second.c2c1eca2324d13154ef9733df393024a3 = true;
			if (!cd0cfe897426aab73c90b8217df390607(first, ceabb47d7d959545d89c4300eabbdd7c2, second, c1b4918d9d3cc94171c67339d1fc78b0f, cd2abebf7ff6ec7d035c28a04faaa459b))
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
				__ldtoken(AdaptiveAabbTree<>.cb0dd5f27e52a6601f14fdfd5df6b33b4.MoveNext());
			}
			if (!first.cfd73e16b19bb8bde65db991a787eb5ad)
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
				if (!second.cfd73e16b19bb8bde65db991a787eb5ad)
				{
					c18ac18e9f88283e6a56a9ec9cb6b9e55(first);
					c18ac18e9f88283e6a56a9ec9cb6b9e55(second);
					stack.Push(new Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e>(first.c2d9d869d0c8fd5fb55f452375f580a4a, second.c2d9d869d0c8fd5fb55f452375f580a4a));
					stack.Push(new Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e>(first.c6d3fb34101b9e1ce92055c2914d2d1de, second.c2d9d869d0c8fd5fb55f452375f580a4a));
					stack.Push(new Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e>(first.c2d9d869d0c8fd5fb55f452375f580a4a, second.c6d3fb34101b9e1ce92055c2914d2d1de));
					stack.Push(new Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e>(first.c6d3fb34101b9e1ce92055c2914d2d1de, second.c6d3fb34101b9e1ce92055c2914d2d1de));
				}
				else
				{
					c18ac18e9f88283e6a56a9ec9cb6b9e55(first);
					stack.Push(new Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e>(first.c2d9d869d0c8fd5fb55f452375f580a4a, second));
					stack.Push(new Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e>(first.c6d3fb34101b9e1ce92055c2914d2d1de, second));
				}
				continue;
			}
			if (!second.cfd73e16b19bb8bde65db991a787eb5ad)
			{
				c18ac18e9f88283e6a56a9ec9cb6b9e55(second);
				stack.Push(new Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e>(first, second.c2d9d869d0c8fd5fb55f452375f580a4a));
				stack.Push(new Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e>(first, second.c6d3fb34101b9e1ce92055c2914d2d1de));
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
					switch (6)
					{
					case 0:
						continue;
					}
					break;
				}
			}
			yield return pair2;
		}
		ResourcePools<Pair<cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e>>.Stacks.Recycle(stack);
	}

	private static bool cc2503a1629586ff93a4137525e360640(cff48f0c06d0d98efeafd069bbfaea07e c1f49a553317458268e1fa11d51bf4670, Vector3F c3bc7e17fda039e69573a5295fced7439, cff48f0c06d0d98efeafd069bbfaea07e c29ba3cbb559c64c577242fda562d3024, Vector3F c547f06073c85c380fd3dccf550a6d536)
	{
		Vector3F vector3F = c1f49a553317458268e1fa11d51bf4670.c3e263e990bc8d6c3a2d636ac7a19c6c6.Extent * Vector3F.Absolute(c3bc7e17fda039e69573a5295fced7439);
		Vector3F vector3F2 = c29ba3cbb559c64c577242fda562d3024.c3e263e990bc8d6c3a2d636ac7a19c6c6.Extent * Vector3F.Absolute(c547f06073c85c380fd3dccf550a6d536);
		return vector3F.LargestComponent > vector3F2.LargestComponent;
	}

	private static bool cd0cfe897426aab73c90b8217df390607(cff48f0c06d0d98efeafd069bbfaea07e c1f49a553317458268e1fa11d51bf4670, Vector3F c3bc7e17fda039e69573a5295fced7439, cff48f0c06d0d98efeafd069bbfaea07e c29ba3cbb559c64c577242fda562d3024, Vector3F c547f06073c85c380fd3dccf550a6d536, Pose c689a79a5792495f42cbe3effa012780c)
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
						__ldtoken(AdaptiveAabbTree<>.GetClosestPointCandidates(Vector3F, Pose, ISpatialPartition<T>, Vector3F, Pose, Func<T, T, float>));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(994));
				}
			}
		}
		if (callback == null)
		{
			while (true)
			{
				switch (2)
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
		if (otherPartition is AdaptiveAabbTree<T>)
		{
			while (true)
			{
				switch (3)
				{
				case 0:
					break;
				default:
				{
					AdaptiveAabbTree<T> adaptiveAabbTree = (AdaptiveAabbTree<T>)otherPartition;
					float c2878235e00baff8166ebc90ef27400ae = float.PositiveInfinity;
					cb780b4b51062221f27d196ea0a7abde7(c3cb2c0edc4d55dd1410e3483d1ef60d4, scale, adaptiveAabbTree.c3cb2c0edc4d55dd1410e3483d1ef60d4, otherScale, pose.Inverse * otherPose, callback, ref c2878235e00baff8166ebc90ef27400ae);
					return;
				}
				}
			}
		}
		if (otherPartition is ISupportClosestPointQueries<T>)
		{
			while (true)
			{
				switch (5)
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

	private void cb780b4b51062221f27d196ea0a7abde7(cff48f0c06d0d98efeafd069bbfaea07e c1f49a553317458268e1fa11d51bf4670, Vector3F c3bc7e17fda039e69573a5295fced7439, cff48f0c06d0d98efeafd069bbfaea07e c29ba3cbb559c64c577242fda562d3024, Vector3F c547f06073c85c380fd3dccf550a6d536, Pose c689a79a5792495f42cbe3effa012780c, Func<T, T, float> ce4b7628f9880cef87f99eae50a8132fe, ref float c2878235e00baff8166ebc90ef27400ae)
	{
		if (c1f49a553317458268e1fa11d51bf4670 == null || c29ba3cbb559c64c577242fda562d3024 == null || c2878235e00baff8166ebc90ef27400ae < 0f)
		{
			return;
		}
		c1f49a553317458268e1fa11d51bf4670.c2c1eca2324d13154ef9733df393024a3 = true;
		c29ba3cbb559c64c577242fda562d3024.c2c1eca2324d13154ef9733df393024a3 = true;
		if (c2878235e00baff8166ebc90ef27400ae == 0f)
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
				__ldtoken(AdaptiveAabbTree<>.cb780b4b51062221f27d196ea0a7abde7(cff48f0c06d0d98efeafd069bbfaea07e, Vector3F, cff48f0c06d0d98efeafd069bbfaea07e, Vector3F, Pose, Func<T, T, float>, ref float));
			}
			if (!cd0cfe897426aab73c90b8217df390607(c1f49a553317458268e1fa11d51bf4670, c3bc7e17fda039e69573a5295fced7439, c29ba3cbb559c64c577242fda562d3024, c547f06073c85c380fd3dccf550a6d536, c689a79a5792495f42cbe3effa012780c))
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
		}
		if (c1f49a553317458268e1fa11d51bf4670.cfd73e16b19bb8bde65db991a787eb5ad)
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
			if (c29ba3cbb559c64c577242fda562d3024.cfd73e16b19bb8bde65db991a787eb5ad)
			{
				while (true)
				{
					switch (4)
					{
					case 0:
						break;
					default:
					{
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
							if (!Filter.Filter(new Pair<T>(c1f49a553317458268e1fa11d51bf4670.c652f269a40a2519b8658ffbade581b4e, c29ba3cbb559c64c577242fda562d3024.c652f269a40a2519b8658ffbade581b4e)))
							{
								return;
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
			if (c1f49a553317458268e1fa11d51bf4670.cfd73e16b19bb8bde65db991a787eb5ad || !cc2503a1629586ff93a4137525e360640(c1f49a553317458268e1fa11d51bf4670, c3bc7e17fda039e69573a5295fced7439, c29ba3cbb559c64c577242fda562d3024, c547f06073c85c380fd3dccf550a6d536))
			{
				c18ac18e9f88283e6a56a9ec9cb6b9e55(c29ba3cbb559c64c577242fda562d3024);
				cff48f0c06d0d98efeafd069bbfaea07e c6d3fb34101b9e1ce92055c2914d2d1de = c29ba3cbb559c64c577242fda562d3024.c6d3fb34101b9e1ce92055c2914d2d1de;
				cff48f0c06d0d98efeafd069bbfaea07e c2d9d869d0c8fd5fb55f452375f580a4a = c29ba3cbb559c64c577242fda562d3024.c2d9d869d0c8fd5fb55f452375f580a4a;
				if (c2878235e00baff8166ebc90ef27400ae == 0f)
				{
					while (true)
					{
						switch (1)
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
					if (num2 > c2878235e00baff8166ebc90ef27400ae)
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
					cb780b4b51062221f27d196ea0a7abde7(c1f49a553317458268e1fa11d51bf4670, c3bc7e17fda039e69573a5295fced7439, c2d9d869d0c8fd5fb55f452375f580a4a, c547f06073c85c380fd3dccf550a6d536, c689a79a5792495f42cbe3effa012780c, ce4b7628f9880cef87f99eae50a8132fe, ref c2878235e00baff8166ebc90ef27400ae);
				}
				else
				{
					if (num2 > c2878235e00baff8166ebc90ef27400ae)
					{
						return;
					}
					cb780b4b51062221f27d196ea0a7abde7(c1f49a553317458268e1fa11d51bf4670, c3bc7e17fda039e69573a5295fced7439, c2d9d869d0c8fd5fb55f452375f580a4a, c547f06073c85c380fd3dccf550a6d536, c689a79a5792495f42cbe3effa012780c, ce4b7628f9880cef87f99eae50a8132fe, ref c2878235e00baff8166ebc90ef27400ae);
					if (num > c2878235e00baff8166ebc90ef27400ae)
					{
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
					cb780b4b51062221f27d196ea0a7abde7(c1f49a553317458268e1fa11d51bf4670, c3bc7e17fda039e69573a5295fced7439, c6d3fb34101b9e1ce92055c2914d2d1de, c547f06073c85c380fd3dccf550a6d536, c689a79a5792495f42cbe3effa012780c, ce4b7628f9880cef87f99eae50a8132fe, ref c2878235e00baff8166ebc90ef27400ae);
				}
				return;
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
		c18ac18e9f88283e6a56a9ec9cb6b9e55(c1f49a553317458268e1fa11d51bf4670);
		cff48f0c06d0d98efeafd069bbfaea07e c6d3fb34101b9e1ce92055c2914d2d1de2 = c1f49a553317458268e1fa11d51bf4670.c6d3fb34101b9e1ce92055c2914d2d1de;
		cff48f0c06d0d98efeafd069bbfaea07e c2d9d869d0c8fd5fb55f452375f580a4a2 = c1f49a553317458268e1fa11d51bf4670.c2d9d869d0c8fd5fb55f452375f580a4a;
		if (c2878235e00baff8166ebc90ef27400ae == 0f)
		{
			cb780b4b51062221f27d196ea0a7abde7(c6d3fb34101b9e1ce92055c2914d2d1de2, c3bc7e17fda039e69573a5295fced7439, c29ba3cbb559c64c577242fda562d3024, c547f06073c85c380fd3dccf550a6d536, c689a79a5792495f42cbe3effa012780c, ce4b7628f9880cef87f99eae50a8132fe, ref c2878235e00baff8166ebc90ef27400ae);
			cb780b4b51062221f27d196ea0a7abde7(c2d9d869d0c8fd5fb55f452375f580a4a2, c3bc7e17fda039e69573a5295fced7439, c29ba3cbb559c64c577242fda562d3024, c547f06073c85c380fd3dccf550a6d536, c689a79a5792495f42cbe3effa012780c, ce4b7628f9880cef87f99eae50a8132fe, ref c2878235e00baff8166ebc90ef27400ae);
			return;
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
			if (num3 > c2878235e00baff8166ebc90ef27400ae)
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
			cb780b4b51062221f27d196ea0a7abde7(c6d3fb34101b9e1ce92055c2914d2d1de2, c3bc7e17fda039e69573a5295fced7439, c29ba3cbb559c64c577242fda562d3024, c547f06073c85c380fd3dccf550a6d536, c689a79a5792495f42cbe3effa012780c, ce4b7628f9880cef87f99eae50a8132fe, ref c2878235e00baff8166ebc90ef27400ae);
			if (!(num4 > c2878235e00baff8166ebc90ef27400ae))
			{
				cb780b4b51062221f27d196ea0a7abde7(c2d9d869d0c8fd5fb55f452375f580a4a2, c3bc7e17fda039e69573a5295fced7439, c29ba3cbb559c64c577242fda562d3024, c547f06073c85c380fd3dccf550a6d536, c689a79a5792495f42cbe3effa012780c, ce4b7628f9880cef87f99eae50a8132fe, ref c2878235e00baff8166ebc90ef27400ae);
			}
		}
		else
		{
			if (num4 > c2878235e00baff8166ebc90ef27400ae)
			{
				return;
			}
			cb780b4b51062221f27d196ea0a7abde7(c2d9d869d0c8fd5fb55f452375f580a4a2, c3bc7e17fda039e69573a5295fced7439, c29ba3cbb559c64c577242fda562d3024, c547f06073c85c380fd3dccf550a6d536, c689a79a5792495f42cbe3effa012780c, ce4b7628f9880cef87f99eae50a8132fe, ref c2878235e00baff8166ebc90ef27400ae);
			if (num3 > c2878235e00baff8166ebc90ef27400ae)
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
			cb780b4b51062221f27d196ea0a7abde7(c6d3fb34101b9e1ce92055c2914d2d1de2, c3bc7e17fda039e69573a5295fced7439, c29ba3cbb559c64c577242fda562d3024, c547f06073c85c380fd3dccf550a6d536, c689a79a5792495f42cbe3effa012780c, ce4b7628f9880cef87f99eae50a8132fe, ref c2878235e00baff8166ebc90ef27400ae);
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
				__ldtoken(AdaptiveAabbTree<>.cb780b4b51062221f27d196ea0a7abde7(Vector3F, Pose, ISupportClosestPointQueries<T>, Vector3F, Pose, Func<T, T, float>));
			}
			if (!(num >= 0f))
			{
				break;
			}
			cff48f0c06d0d98efeafd069bbfaea07e cff48f0c06d0d98efeafd069bbfaea07e2 = cd311d3823f638b023eacb767b312cc3e[i];
			cc308d98a84a8563af043b321600339a3.Item = cff48f0c06d0d98efeafd069bbfaea07e2.c652f269a40a2519b8658ffbade581b4e;
			Aabb aabb = cff48f0c06d0d98efeafd069bbfaea07e2.c3e263e990bc8d6c3a2d636ac7a19c6c6.GetAabb(ceabb47d7d959545d89c4300eabbdd7c2, pose);
			aabb.Scale(scale);
			num = cfd8883387afb9cc25a2ac9150de0f66b.GetClosestPointCandidates(aabb, num, cc308d98a84a8563af043b321600339a3.c6516654248581f959063ea51406f708b);
		}
		cc308d98a84a8563af043b321600339a3.ca18b9b5faa9851d6355f617787bb5859();
	}

	private void cb780b4b51062221f27d196ea0a7abde7(ISpatialPartition<T> cfd8883387afb9cc25a2ac9150de0f66b, Func<T, T, float> ce4b7628f9880cef87f99eae50a8132fe)
	{
		foreach (T item in c0e601bcddbab14811b8bae6ae4b7b274)
		{
			using IEnumerator<T> enumerator2 = cfd8883387afb9cc25a2ac9150de0f66b.GetEnumerator();
			while (enumerator2.MoveNext())
			{
				T current2 = enumerator2.Current;
				float num = ce4b7628f9880cef87f99eae50a8132fe(item, current2);
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
					__ldtoken(AdaptiveAabbTree<>.cb780b4b51062221f27d196ea0a7abde7(ISpatialPartition<T>, Func<T, T, float>));
				}
				break;
			}
		}
	}

	private void ccf1870247a728556a633ff212007937e()
	{
		if (c3cb2c0edc4d55dd1410e3483d1ef60d4 != null)
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
				__ldtoken(AdaptiveAabbTree<>.ccf1870247a728556a633ff212007937e());
			}
			c830234da7541f9eac611858f0f59fe28(c3cb2c0edc4d55dd1410e3483d1ef60d4, null);
			c3cb2c0edc4d55dd1410e3483d1ef60d4 = null;
			cd311d3823f638b023eacb767b312cc3e.Clear();
		}
		if (Count == 0)
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
			cff48f0c06d0d98efeafd069bbfaea07e cff48f0c06d0d98efeafd069bbfaea07e2 = c53fef33d7344726da08aeb063ec62121.Obtain();
			cff48f0c06d0d98efeafd069bbfaea07e2.c3e263e990bc8d6c3a2d636ac7a19c6c6 = GetAabbForItem(val2);
			cff48f0c06d0d98efeafd069bbfaea07e2.c652f269a40a2519b8658ffbade581b4e = val2;
			cd311d3823f638b023eacb767b312cc3e.Add(cff48f0c06d0d98efeafd069bbfaea07e2);
		}
		while (true)
		{
			switch (7)
			{
			case 0:
				continue;
			}
			List<cbe5649b18b8b0caa00be9ad49188b193<T>> list = ResourcePools<cbe5649b18b8b0caa00be9ad49188b193<T>>.Lists.Obtain();
			for (int j = 0; j < count; j++)
			{
				list.Add(cd311d3823f638b023eacb767b312cc3e[j]);
			}
			if (c753747c901f3e80c0e263e414196b8eb == null)
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
				c753747c901f3e80c0e263e414196b8eb = () => c53fef33d7344726da08aeb063ec62121.Obtain();
			}
			c3cb2c0edc4d55dd1410e3483d1ef60d4 = (cff48f0c06d0d98efeafd069bbfaea07e)c5c139e98a37beb65b1cb5aea8ce81daf.ccf1870247a728556a633ff212007937e(list, c753747c901f3e80c0e263e414196b8eb, BottomUpBuildThreshold);
			ResourcePools<cbe5649b18b8b0caa00be9ad49188b193<T>>.Lists.Recycle(list);
			return;
		}
	}

	private void c1a764b9e30d2bf7320c1c41fa091b3a2(cff48f0c06d0d98efeafd069bbfaea07e c7f587bd57d47897172f47483e6bcca68)
	{
		if (c7f587bd57d47897172f47483e6bcca68.cfd73e16b19bb8bde65db991a787eb5ad)
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
				__ldtoken(AdaptiveAabbTree<>.c1a764b9e30d2bf7320c1c41fa091b3a2(cff48f0c06d0d98efeafd069bbfaea07e));
			}
			c7f587bd57d47897172f47483e6bcca68.c3e263e990bc8d6c3a2d636ac7a19c6c6 = GetAabbForItem(c7f587bd57d47897172f47483e6bcca68.c652f269a40a2519b8658ffbade581b4e);
		}
		else
		{
			if (c7f587bd57d47897172f47483e6bcca68.c8e71ef62104b5216a597dbb6b08779f3)
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
				if (c7f587bd57d47897172f47483e6bcca68.c2c1eca2324d13154ef9733df393024a3)
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
					if (!c7f587bd57d47897172f47483e6bcca68.c6d3fb34101b9e1ce92055c2914d2d1de.c2c1eca2324d13154ef9733df393024a3)
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
						c5708a4218c0c30fda412cf095ba965e9(c7f587bd57d47897172f47483e6bcca68);
					}
				}
			}
			if (!c7f587bd57d47897172f47483e6bcca68.c8e71ef62104b5216a597dbb6b08779f3)
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
				cff48f0c06d0d98efeafd069bbfaea07e cff48f0c06d0d98efeafd069bbfaea07e2 = c7f587bd57d47897172f47483e6bcca68.c9594e2c7b7942e1d22a230fdeee51f04[0];
				cff48f0c06d0d98efeafd069bbfaea07e2.c2c1eca2324d13154ef9733df393024a3 = false;
				cff48f0c06d0d98efeafd069bbfaea07e2.c3e263e990bc8d6c3a2d636ac7a19c6c6 = GetAabbForItem(cff48f0c06d0d98efeafd069bbfaea07e2.c652f269a40a2519b8658ffbade581b4e);
				c7f587bd57d47897172f47483e6bcca68.c3e263e990bc8d6c3a2d636ac7a19c6c6 = cff48f0c06d0d98efeafd069bbfaea07e2.c3e263e990bc8d6c3a2d636ac7a19c6c6;
				int count = c7f587bd57d47897172f47483e6bcca68.c9594e2c7b7942e1d22a230fdeee51f04.Count;
				for (int i = 1; i < count; i++)
				{
					cff48f0c06d0d98efeafd069bbfaea07e2 = c7f587bd57d47897172f47483e6bcca68.c9594e2c7b7942e1d22a230fdeee51f04[i];
					cff48f0c06d0d98efeafd069bbfaea07e2.c2c1eca2324d13154ef9733df393024a3 = false;
					cff48f0c06d0d98efeafd069bbfaea07e2.c3e263e990bc8d6c3a2d636ac7a19c6c6 = GetAabbForItem(cff48f0c06d0d98efeafd069bbfaea07e2.c652f269a40a2519b8658ffbade581b4e);
					c7f587bd57d47897172f47483e6bcca68.c3e263e990bc8d6c3a2d636ac7a19c6c6.Grow(cff48f0c06d0d98efeafd069bbfaea07e2.c3e263e990bc8d6c3a2d636ac7a19c6c6);
				}
			}
			else
			{
				c1a764b9e30d2bf7320c1c41fa091b3a2(c7f587bd57d47897172f47483e6bcca68.c6d3fb34101b9e1ce92055c2914d2d1de);
				c1a764b9e30d2bf7320c1c41fa091b3a2(c7f587bd57d47897172f47483e6bcca68.c2d9d869d0c8fd5fb55f452375f580a4a);
				c7f587bd57d47897172f47483e6bcca68.c3e263e990bc8d6c3a2d636ac7a19c6c6 = Aabb.Merge(c7f587bd57d47897172f47483e6bcca68.c6d3fb34101b9e1ce92055c2914d2d1de.c3e263e990bc8d6c3a2d636ac7a19c6c6, c7f587bd57d47897172f47483e6bcca68.c2d9d869d0c8fd5fb55f452375f580a4a.c3e263e990bc8d6c3a2d636ac7a19c6c6);
				c7f587bd57d47897172f47483e6bcca68.c74dc8c78f0b932d691a8442fc47f4ad5 = c74dc8c78f0b932d691a8442fc47f4ad5(c7f587bd57d47897172f47483e6bcca68);
			}
		}
		c7f587bd57d47897172f47483e6bcca68.c2c1eca2324d13154ef9733df393024a3 = false;
	}

	private bool c491a1eaa0478490b64a843d2289f78e1(cff48f0c06d0d98efeafd069bbfaea07e c7f587bd57d47897172f47483e6bcca68, HashSet<T> c7a74c15b192971583f3f676b15968063)
	{
		bool flag = false;
		if (c7f587bd57d47897172f47483e6bcca68.cfd73e16b19bb8bde65db991a787eb5ad)
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
				__ldtoken(AdaptiveAabbTree<>.c491a1eaa0478490b64a843d2289f78e1(cff48f0c06d0d98efeafd069bbfaea07e, HashSet<T>));
			}
			if (c7a74c15b192971583f3f676b15968063.Contains(c7f587bd57d47897172f47483e6bcca68.c652f269a40a2519b8658ffbade581b4e))
			{
				c7f587bd57d47897172f47483e6bcca68.c3e263e990bc8d6c3a2d636ac7a19c6c6 = GetAabbForItem(c7f587bd57d47897172f47483e6bcca68.c652f269a40a2519b8658ffbade581b4e);
				flag = true;
			}
		}
		else if (!c7f587bd57d47897172f47483e6bcca68.c8e71ef62104b5216a597dbb6b08779f3)
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
			cff48f0c06d0d98efeafd069bbfaea07e cff48f0c06d0d98efeafd069bbfaea07e2 = c7f587bd57d47897172f47483e6bcca68.c9594e2c7b7942e1d22a230fdeee51f04[0];
			cff48f0c06d0d98efeafd069bbfaea07e2.c2c1eca2324d13154ef9733df393024a3 = false;
			if (c7a74c15b192971583f3f676b15968063.Contains(cff48f0c06d0d98efeafd069bbfaea07e2.c652f269a40a2519b8658ffbade581b4e))
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
				cff48f0c06d0d98efeafd069bbfaea07e2.c3e263e990bc8d6c3a2d636ac7a19c6c6 = GetAabbForItem(cff48f0c06d0d98efeafd069bbfaea07e2.c652f269a40a2519b8658ffbade581b4e);
				flag = true;
			}
			c7f587bd57d47897172f47483e6bcca68.c3e263e990bc8d6c3a2d636ac7a19c6c6 = cff48f0c06d0d98efeafd069bbfaea07e2.c3e263e990bc8d6c3a2d636ac7a19c6c6;
			int count = c7f587bd57d47897172f47483e6bcca68.c9594e2c7b7942e1d22a230fdeee51f04.Count;
			for (int i = 1; i < count; i++)
			{
				cff48f0c06d0d98efeafd069bbfaea07e2 = c7f587bd57d47897172f47483e6bcca68.c9594e2c7b7942e1d22a230fdeee51f04[i];
				cff48f0c06d0d98efeafd069bbfaea07e2.c2c1eca2324d13154ef9733df393024a3 = false;
				if (c7a74c15b192971583f3f676b15968063.Contains(cff48f0c06d0d98efeafd069bbfaea07e2.c652f269a40a2519b8658ffbade581b4e))
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
					cff48f0c06d0d98efeafd069bbfaea07e2.c3e263e990bc8d6c3a2d636ac7a19c6c6 = GetAabbForItem(cff48f0c06d0d98efeafd069bbfaea07e2.c652f269a40a2519b8658ffbade581b4e);
					flag = true;
				}
				c7f587bd57d47897172f47483e6bcca68.c3e263e990bc8d6c3a2d636ac7a19c6c6.Grow(cff48f0c06d0d98efeafd069bbfaea07e2.c3e263e990bc8d6c3a2d636ac7a19c6c6);
			}
		}
		else
		{
			bool flag2 = c491a1eaa0478490b64a843d2289f78e1(c7f587bd57d47897172f47483e6bcca68.c6d3fb34101b9e1ce92055c2914d2d1de, c7a74c15b192971583f3f676b15968063);
			bool flag3 = c491a1eaa0478490b64a843d2289f78e1(c7f587bd57d47897172f47483e6bcca68.c2d9d869d0c8fd5fb55f452375f580a4a, c7a74c15b192971583f3f676b15968063);
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
				c7f587bd57d47897172f47483e6bcca68.c3e263e990bc8d6c3a2d636ac7a19c6c6 = Aabb.Merge(c7f587bd57d47897172f47483e6bcca68.c6d3fb34101b9e1ce92055c2914d2d1de.c3e263e990bc8d6c3a2d636ac7a19c6c6, c7f587bd57d47897172f47483e6bcca68.c2d9d869d0c8fd5fb55f452375f580a4a.c3e263e990bc8d6c3a2d636ac7a19c6c6);
				c7f587bd57d47897172f47483e6bcca68.c74dc8c78f0b932d691a8442fc47f4ad5 = c74dc8c78f0b932d691a8442fc47f4ad5(c7f587bd57d47897172f47483e6bcca68);
			}
		}
		c7f587bd57d47897172f47483e6bcca68.c2c1eca2324d13154ef9733df393024a3 = false;
		return flag;
	}

	private static void c5708a4218c0c30fda412cf095ba965e9(cff48f0c06d0d98efeafd069bbfaea07e c7f587bd57d47897172f47483e6bcca68)
	{
		if (c7f587bd57d47897172f47483e6bcca68.c8e71ef62104b5216a597dbb6b08779f3)
		{
			c7f587bd57d47897172f47483e6bcca68.c9594e2c7b7942e1d22a230fdeee51f04 = ResourcePools<cff48f0c06d0d98efeafd069bbfaea07e>.Lists.Obtain();
			c830234da7541f9eac611858f0f59fe28(c7f587bd57d47897172f47483e6bcca68.c6d3fb34101b9e1ce92055c2914d2d1de, c7f587bd57d47897172f47483e6bcca68.c9594e2c7b7942e1d22a230fdeee51f04);
			c830234da7541f9eac611858f0f59fe28(c7f587bd57d47897172f47483e6bcca68.c2d9d869d0c8fd5fb55f452375f580a4a, c7f587bd57d47897172f47483e6bcca68.c9594e2c7b7942e1d22a230fdeee51f04);
			c7f587bd57d47897172f47483e6bcca68.c6d3fb34101b9e1ce92055c2914d2d1de = null;
			c7f587bd57d47897172f47483e6bcca68.c2d9d869d0c8fd5fb55f452375f580a4a = null;
			c7f587bd57d47897172f47483e6bcca68.c74dc8c78f0b932d691a8442fc47f4ad5 = true;
		}
	}

	private static void c830234da7541f9eac611858f0f59fe28(cff48f0c06d0d98efeafd069bbfaea07e c7f587bd57d47897172f47483e6bcca68, List<cff48f0c06d0d98efeafd069bbfaea07e> cd4531a9ec3304b0c90a95f8f073929f4)
	{
		if (c7f587bd57d47897172f47483e6bcca68.cfd73e16b19bb8bde65db991a787eb5ad)
		{
			if (cd4531a9ec3304b0c90a95f8f073929f4 == null)
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
					__ldtoken(AdaptiveAabbTree<>.c830234da7541f9eac611858f0f59fe28(cff48f0c06d0d98efeafd069bbfaea07e, List<cff48f0c06d0d98efeafd069bbfaea07e>));
				}
				cd4531a9ec3304b0c90a95f8f073929f4.Add(c7f587bd57d47897172f47483e6bcca68);
				return;
			}
		}
		if (!c7f587bd57d47897172f47483e6bcca68.c8e71ef62104b5216a597dbb6b08779f3)
		{
			if (cd4531a9ec3304b0c90a95f8f073929f4 != null)
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
				cd4531a9ec3304b0c90a95f8f073929f4.AddRange(c7f587bd57d47897172f47483e6bcca68.c9594e2c7b7942e1d22a230fdeee51f04);
			}
		}
		else
		{
			c830234da7541f9eac611858f0f59fe28(c7f587bd57d47897172f47483e6bcca68.c6d3fb34101b9e1ce92055c2914d2d1de, cd4531a9ec3304b0c90a95f8f073929f4);
			c830234da7541f9eac611858f0f59fe28(c7f587bd57d47897172f47483e6bcca68.c2d9d869d0c8fd5fb55f452375f580a4a, cd4531a9ec3304b0c90a95f8f073929f4);
		}
		c53fef33d7344726da08aeb063ec62121.Recycle(c7f587bd57d47897172f47483e6bcca68);
	}

	private static bool c74dc8c78f0b932d691a8442fc47f4ad5(cff48f0c06d0d98efeafd069bbfaea07e c7f587bd57d47897172f47483e6bcca68)
	{
		if (c7f587bd57d47897172f47483e6bcca68.c6d3fb34101b9e1ce92055c2914d2d1de.cfd73e16b19bb8bde65db991a787eb5ad && c7f587bd57d47897172f47483e6bcca68.c2d9d869d0c8fd5fb55f452375f580a4a.cfd73e16b19bb8bde65db991a787eb5ad)
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
						__ldtoken(AdaptiveAabbTree<>.c74dc8c78f0b932d691a8442fc47f4ad5(cff48f0c06d0d98efeafd069bbfaea07e));
					}
					return false;
				}
			}
		}
		float volume = c7f587bd57d47897172f47483e6bcca68.c3e263e990bc8d6c3a2d636ac7a19c6c6.Volume;
		float volume2 = c7f587bd57d47897172f47483e6bcca68.c6d3fb34101b9e1ce92055c2914d2d1de.c3e263e990bc8d6c3a2d636ac7a19c6c6.Volume;
		float volume3 = c7f587bd57d47897172f47483e6bcca68.c2d9d869d0c8fd5fb55f452375f580a4a.c3e263e990bc8d6c3a2d636ac7a19c6c6.Volume;
		return volume / (volume2 + volume3) < 0.9f;
	}

	private void c437cc650c1bbb3b5d6a346455cd9229c(T cf51048ba330b8d3f481ea5bc25a03eb9)
	{
		cff48f0c06d0d98efeafd069bbfaea07e cff48f0c06d0d98efeafd069bbfaea07e2 = c53fef33d7344726da08aeb063ec62121.Obtain();
		cff48f0c06d0d98efeafd069bbfaea07e2.c3e263e990bc8d6c3a2d636ac7a19c6c6 = GetAabbForItem(cf51048ba330b8d3f481ea5bc25a03eb9);
		cff48f0c06d0d98efeafd069bbfaea07e2.c652f269a40a2519b8658ffbade581b4e = cf51048ba330b8d3f481ea5bc25a03eb9;
		cd311d3823f638b023eacb767b312cc3e.Add(cff48f0c06d0d98efeafd069bbfaea07e2);
		if (c3cb2c0edc4d55dd1410e3483d1ef60d4 == null)
		{
			c3cb2c0edc4d55dd1410e3483d1ef60d4 = cff48f0c06d0d98efeafd069bbfaea07e2;
			return;
		}
		if (c3cb2c0edc4d55dd1410e3483d1ef60d4.cfd73e16b19bb8bde65db991a787eb5ad)
		{
			while (true)
			{
				switch (1)
				{
				case 0:
					break;
				default:
				{
					if (1 == 0)
					{
						__ldtoken(AdaptiveAabbTree<>.c437cc650c1bbb3b5d6a346455cd9229c(T));
					}
					cff48f0c06d0d98efeafd069bbfaea07e c6d3fb34101b9e1ce92055c2914d2d1de = c3cb2c0edc4d55dd1410e3483d1ef60d4;
					c3cb2c0edc4d55dd1410e3483d1ef60d4 = c53fef33d7344726da08aeb063ec62121.Obtain();
					c3cb2c0edc4d55dd1410e3483d1ef60d4.c6d3fb34101b9e1ce92055c2914d2d1de = c6d3fb34101b9e1ce92055c2914d2d1de;
					c3cb2c0edc4d55dd1410e3483d1ef60d4.c2d9d869d0c8fd5fb55f452375f580a4a = cff48f0c06d0d98efeafd069bbfaea07e2;
					return;
				}
				}
			}
		}
		c437cc650c1bbb3b5d6a346455cd9229c(c3cb2c0edc4d55dd1410e3483d1ef60d4, cff48f0c06d0d98efeafd069bbfaea07e2);
	}

	private void c437cc650c1bbb3b5d6a346455cd9229c(cff48f0c06d0d98efeafd069bbfaea07e c7f587bd57d47897172f47483e6bcca68, cff48f0c06d0d98efeafd069bbfaea07e ce9beaab8ed4de70dd0e96a36b4c42105)
	{
		if (c7f587bd57d47897172f47483e6bcca68.c8e71ef62104b5216a597dbb6b08779f3)
		{
			if (c1706d55e3116d38c16c012a797e57db7.c22645a0ac256427f72ed4eca4cbca2b7(ce9beaab8ed4de70dd0e96a36b4c42105.c3e263e990bc8d6c3a2d636ac7a19c6c6, c7f587bd57d47897172f47483e6bcca68.c6d3fb34101b9e1ce92055c2914d2d1de.c3e263e990bc8d6c3a2d636ac7a19c6c6, c7f587bd57d47897172f47483e6bcca68.c2d9d869d0c8fd5fb55f452375f580a4a.c3e263e990bc8d6c3a2d636ac7a19c6c6) == 0)
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
					__ldtoken(AdaptiveAabbTree<>.c437cc650c1bbb3b5d6a346455cd9229c(cff48f0c06d0d98efeafd069bbfaea07e, cff48f0c06d0d98efeafd069bbfaea07e));
				}
				if (c7f587bd57d47897172f47483e6bcca68.c6d3fb34101b9e1ce92055c2914d2d1de.cfd73e16b19bb8bde65db991a787eb5ad)
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
					c5708a4218c0c30fda412cf095ba965e9(c7f587bd57d47897172f47483e6bcca68);
				}
				else
				{
					c437cc650c1bbb3b5d6a346455cd9229c(c7f587bd57d47897172f47483e6bcca68.c6d3fb34101b9e1ce92055c2914d2d1de, ce9beaab8ed4de70dd0e96a36b4c42105);
				}
			}
			else if (c7f587bd57d47897172f47483e6bcca68.c2d9d869d0c8fd5fb55f452375f580a4a.cfd73e16b19bb8bde65db991a787eb5ad)
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
				c5708a4218c0c30fda412cf095ba965e9(c7f587bd57d47897172f47483e6bcca68);
			}
			else
			{
				c437cc650c1bbb3b5d6a346455cd9229c(c7f587bd57d47897172f47483e6bcca68.c2d9d869d0c8fd5fb55f452375f580a4a, ce9beaab8ed4de70dd0e96a36b4c42105);
			}
		}
		if (!c7f587bd57d47897172f47483e6bcca68.c8e71ef62104b5216a597dbb6b08779f3)
		{
			c7f587bd57d47897172f47483e6bcca68.c9594e2c7b7942e1d22a230fdeee51f04.Add(ce9beaab8ed4de70dd0e96a36b4c42105);
		}
		cbf0ef68edde493f92f560e7401c5cc48(c7f587bd57d47897172f47483e6bcca68);
	}

	private void c3d1fce75fd975cd49156dca71180a13f(T cf51048ba330b8d3f481ea5bc25a03eb9)
	{
		if (cd311d3823f638b023eacb767b312cc3e.Count == 1)
		{
			cd311d3823f638b023eacb767b312cc3e.RemoveAt(0);
			c53fef33d7344726da08aeb063ec62121.Recycle(c3cb2c0edc4d55dd1410e3483d1ef60d4);
			c3cb2c0edc4d55dd1410e3483d1ef60d4 = null;
			return;
		}
		c3d1fce75fd975cd49156dca71180a13f(c3cb2c0edc4d55dd1410e3483d1ef60d4, cf51048ba330b8d3f481ea5bc25a03eb9);
		if (!c3cb2c0edc4d55dd1410e3483d1ef60d4.c8e71ef62104b5216a597dbb6b08779f3 && c3cb2c0edc4d55dd1410e3483d1ef60d4.c9594e2c7b7942e1d22a230fdeee51f04.Count == 1)
		{
			cff48f0c06d0d98efeafd069bbfaea07e cff48f0c06d0d98efeafd069bbfaea07e2 = c3cb2c0edc4d55dd1410e3483d1ef60d4.c9594e2c7b7942e1d22a230fdeee51f04[0];
			c53fef33d7344726da08aeb063ec62121.Recycle(c3cb2c0edc4d55dd1410e3483d1ef60d4);
			c3cb2c0edc4d55dd1410e3483d1ef60d4 = cff48f0c06d0d98efeafd069bbfaea07e2;
		}
	}

	private bool c3d1fce75fd975cd49156dca71180a13f(cff48f0c06d0d98efeafd069bbfaea07e c7f587bd57d47897172f47483e6bcca68, T cf51048ba330b8d3f481ea5bc25a03eb9)
	{
		bool flag = false;
		if (c7f587bd57d47897172f47483e6bcca68.c8e71ef62104b5216a597dbb6b08779f3)
		{
			if (c7f587bd57d47897172f47483e6bcca68.c6d3fb34101b9e1ce92055c2914d2d1de.cfd73e16b19bb8bde65db991a787eb5ad)
			{
				if (BasePartition<T>.cbbc6d542116e0e180b1f11cadb4505d2.Equals(c7f587bd57d47897172f47483e6bcca68.c6d3fb34101b9e1ce92055c2914d2d1de.c652f269a40a2519b8658ffbade581b4e, cf51048ba330b8d3f481ea5bc25a03eb9))
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
						__ldtoken(AdaptiveAabbTree<>.c3d1fce75fd975cd49156dca71180a13f(cff48f0c06d0d98efeafd069bbfaea07e, T));
					}
					c5708a4218c0c30fda412cf095ba965e9(c7f587bd57d47897172f47483e6bcca68);
					flag = c3d1fce75fd975cd49156dca71180a13f(c7f587bd57d47897172f47483e6bcca68.c9594e2c7b7942e1d22a230fdeee51f04, cf51048ba330b8d3f481ea5bc25a03eb9);
				}
			}
			else
			{
				flag = c3d1fce75fd975cd49156dca71180a13f(c7f587bd57d47897172f47483e6bcca68.c6d3fb34101b9e1ce92055c2914d2d1de, cf51048ba330b8d3f481ea5bc25a03eb9);
				if (flag && !c7f587bd57d47897172f47483e6bcca68.c6d3fb34101b9e1ce92055c2914d2d1de.c8e71ef62104b5216a597dbb6b08779f3 && c7f587bd57d47897172f47483e6bcca68.c6d3fb34101b9e1ce92055c2914d2d1de.c9594e2c7b7942e1d22a230fdeee51f04.Count == 1)
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
					c5708a4218c0c30fda412cf095ba965e9(c7f587bd57d47897172f47483e6bcca68);
				}
			}
			if (!flag)
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
				if (c7f587bd57d47897172f47483e6bcca68.c2d9d869d0c8fd5fb55f452375f580a4a.cfd73e16b19bb8bde65db991a787eb5ad)
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
					if (BasePartition<T>.cbbc6d542116e0e180b1f11cadb4505d2.Equals(c7f587bd57d47897172f47483e6bcca68.c2d9d869d0c8fd5fb55f452375f580a4a.c652f269a40a2519b8658ffbade581b4e, cf51048ba330b8d3f481ea5bc25a03eb9))
					{
						c5708a4218c0c30fda412cf095ba965e9(c7f587bd57d47897172f47483e6bcca68);
						flag = c3d1fce75fd975cd49156dca71180a13f(c7f587bd57d47897172f47483e6bcca68.c9594e2c7b7942e1d22a230fdeee51f04, cf51048ba330b8d3f481ea5bc25a03eb9);
					}
				}
				else
				{
					flag = c3d1fce75fd975cd49156dca71180a13f(c7f587bd57d47897172f47483e6bcca68.c2d9d869d0c8fd5fb55f452375f580a4a, cf51048ba330b8d3f481ea5bc25a03eb9);
					if (flag)
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
						if (!c7f587bd57d47897172f47483e6bcca68.c2d9d869d0c8fd5fb55f452375f580a4a.c8e71ef62104b5216a597dbb6b08779f3)
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
							if (c7f587bd57d47897172f47483e6bcca68.c2d9d869d0c8fd5fb55f452375f580a4a.c9594e2c7b7942e1d22a230fdeee51f04.Count == 1)
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
								c5708a4218c0c30fda412cf095ba965e9(c7f587bd57d47897172f47483e6bcca68);
							}
						}
					}
				}
			}
		}
		else
		{
			flag = c3d1fce75fd975cd49156dca71180a13f(c7f587bd57d47897172f47483e6bcca68.c9594e2c7b7942e1d22a230fdeee51f04, cf51048ba330b8d3f481ea5bc25a03eb9);
		}
		if (flag)
		{
			cbf0ef68edde493f92f560e7401c5cc48(c7f587bd57d47897172f47483e6bcca68);
		}
		return flag;
	}

	private bool c3d1fce75fd975cd49156dca71180a13f(List<cff48f0c06d0d98efeafd069bbfaea07e> cd4531a9ec3304b0c90a95f8f073929f4, T cf51048ba330b8d3f481ea5bc25a03eb9)
	{
		bool flag = false;
		cff48f0c06d0d98efeafd069bbfaea07e cff48f0c06d0d98efeafd069bbfaea07e2 = null;
		int count = cd4531a9ec3304b0c90a95f8f073929f4.Count;
		int num = 0;
		while (true)
		{
			if (num < count)
			{
				cff48f0c06d0d98efeafd069bbfaea07e2 = cd4531a9ec3304b0c90a95f8f073929f4[num];
				if (BasePartition<T>.cbbc6d542116e0e180b1f11cadb4505d2.Equals(cff48f0c06d0d98efeafd069bbfaea07e2.c652f269a40a2519b8658ffbade581b4e, cf51048ba330b8d3f481ea5bc25a03eb9))
				{
					flag = true;
					break;
				}
				num++;
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
				__ldtoken(AdaptiveAabbTree<>.c3d1fce75fd975cd49156dca71180a13f(List<cff48f0c06d0d98efeafd069bbfaea07e>, T));
			}
			break;
		}
		if (flag)
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
			cd4531a9ec3304b0c90a95f8f073929f4.RemoveAt(num);
			cd311d3823f638b023eacb767b312cc3e.Remove(cff48f0c06d0d98efeafd069bbfaea07e2);
		}
		return flag;
	}

	private static void cbf0ef68edde493f92f560e7401c5cc48(cff48f0c06d0d98efeafd069bbfaea07e c7f587bd57d47897172f47483e6bcca68)
	{
		if (c7f587bd57d47897172f47483e6bcca68.c8e71ef62104b5216a597dbb6b08779f3)
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
						__ldtoken(AdaptiveAabbTree<>.cbf0ef68edde493f92f560e7401c5cc48(cff48f0c06d0d98efeafd069bbfaea07e));
					}
					c7f587bd57d47897172f47483e6bcca68.c3e263e990bc8d6c3a2d636ac7a19c6c6 = Aabb.Merge(c7f587bd57d47897172f47483e6bcca68.c6d3fb34101b9e1ce92055c2914d2d1de.c3e263e990bc8d6c3a2d636ac7a19c6c6, c7f587bd57d47897172f47483e6bcca68.c2d9d869d0c8fd5fb55f452375f580a4a.c3e263e990bc8d6c3a2d636ac7a19c6c6);
					return;
				}
			}
		}
		c7f587bd57d47897172f47483e6bcca68.c3e263e990bc8d6c3a2d636ac7a19c6c6 = c7f587bd57d47897172f47483e6bcca68.c9594e2c7b7942e1d22a230fdeee51f04[0].c3e263e990bc8d6c3a2d636ac7a19c6c6;
		for (int i = 1; i < c7f587bd57d47897172f47483e6bcca68.c9594e2c7b7942e1d22a230fdeee51f04.Count; i++)
		{
			c7f587bd57d47897172f47483e6bcca68.c3e263e990bc8d6c3a2d636ac7a19c6c6.Grow(c7f587bd57d47897172f47483e6bcca68.c9594e2c7b7942e1d22a230fdeee51f04[i].c3e263e990bc8d6c3a2d636ac7a19c6c6);
		}
	}

	private static void c18ac18e9f88283e6a56a9ec9cb6b9e55(cff48f0c06d0d98efeafd069bbfaea07e c7f587bd57d47897172f47483e6bcca68)
	{
		if (c7f587bd57d47897172f47483e6bcca68.c8e71ef62104b5216a597dbb6b08779f3 && !c7f587bd57d47897172f47483e6bcca68.c74dc8c78f0b932d691a8442fc47f4ad5)
		{
			return;
		}
		c5708a4218c0c30fda412cf095ba965e9(c7f587bd57d47897172f47483e6bcca68);
		int count = c7f587bd57d47897172f47483e6bcca68.c9594e2c7b7942e1d22a230fdeee51f04.Count;
		int c970f878d2fc540f781119125607c4ce;
		int num;
		int num2;
		if (count == 2)
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
				__ldtoken(AdaptiveAabbTree<>.c18ac18e9f88283e6a56a9ec9cb6b9e55(cff48f0c06d0d98efeafd069bbfaea07e));
			}
			num = 1;
			num2 = 1;
			c970f878d2fc540f781119125607c4ce = 1;
			goto IL_0134;
		}
		int indexOfLargestComponent = c7f587bd57d47897172f47483e6bcca68.c3e263e990bc8d6c3a2d636ac7a19c6c6.Extent.IndexOfLargestComponent;
		float num3 = c7f587bd57d47897172f47483e6bcca68.c3e263e990bc8d6c3a2d636ac7a19c6c6.Center[indexOfLargestComponent];
		c9a76abd2a74eafc9b1ae5461a5f70d04(c7f587bd57d47897172f47483e6bcca68.c9594e2c7b7942e1d22a230fdeee51f04, indexOfLargestComponent, num3, out c970f878d2fc540f781119125607c4ce);
		num = c970f878d2fc540f781119125607c4ce;
		num2 = count - c970f878d2fc540f781119125607c4ce;
		float num4 = 0.8f * (float)count;
		if (!((float)num > num4))
		{
			if (!((float)num2 > num4))
			{
				goto IL_0134;
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
		if (num > num2)
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
			num3 = (c7f587bd57d47897172f47483e6bcca68.c3e263e990bc8d6c3a2d636ac7a19c6c6.Minimum[indexOfLargestComponent] + num3) / 2f;
		}
		else
		{
			num3 = (c7f587bd57d47897172f47483e6bcca68.c3e263e990bc8d6c3a2d636ac7a19c6c6.Maximum[indexOfLargestComponent] + num3) / 2f;
		}
		c9a76abd2a74eafc9b1ae5461a5f70d04(c7f587bd57d47897172f47483e6bcca68.c9594e2c7b7942e1d22a230fdeee51f04, indexOfLargestComponent, num3, out c970f878d2fc540f781119125607c4ce);
		num = c970f878d2fc540f781119125607c4ce;
		num2 = count - c970f878d2fc540f781119125607c4ce;
		if (!((float)num > num4))
		{
			if (!((float)num2 > num4))
			{
				goto IL_012e;
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
		c970f878d2fc540f781119125607c4ce = count / 2;
		goto IL_012e;
		IL_012e:
		num = c970f878d2fc540f781119125607c4ce;
		num2 = count - c970f878d2fc540f781119125607c4ce;
		goto IL_0134;
		IL_0134:
		if (num == 1)
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
			c7f587bd57d47897172f47483e6bcca68.c6d3fb34101b9e1ce92055c2914d2d1de = c7f587bd57d47897172f47483e6bcca68.c9594e2c7b7942e1d22a230fdeee51f04[0];
		}
		else
		{
			cff48f0c06d0d98efeafd069bbfaea07e cff48f0c06d0d98efeafd069bbfaea07e2 = c53fef33d7344726da08aeb063ec62121.Obtain();
			cff48f0c06d0d98efeafd069bbfaea07e2.c9594e2c7b7942e1d22a230fdeee51f04 = ResourcePools<cff48f0c06d0d98efeafd069bbfaea07e>.Lists.Obtain();
			cff48f0c06d0d98efeafd069bbfaea07e cff48f0c06d0d98efeafd069bbfaea07e3 = c7f587bd57d47897172f47483e6bcca68.c9594e2c7b7942e1d22a230fdeee51f04[0];
			cff48f0c06d0d98efeafd069bbfaea07e2.c9594e2c7b7942e1d22a230fdeee51f04.Add(cff48f0c06d0d98efeafd069bbfaea07e3);
			cff48f0c06d0d98efeafd069bbfaea07e2.c3e263e990bc8d6c3a2d636ac7a19c6c6 = cff48f0c06d0d98efeafd069bbfaea07e3.c3e263e990bc8d6c3a2d636ac7a19c6c6;
			for (int i = 1; i < c970f878d2fc540f781119125607c4ce; i++)
			{
				cff48f0c06d0d98efeafd069bbfaea07e3 = c7f587bd57d47897172f47483e6bcca68.c9594e2c7b7942e1d22a230fdeee51f04[i];
				cff48f0c06d0d98efeafd069bbfaea07e2.c9594e2c7b7942e1d22a230fdeee51f04.Add(cff48f0c06d0d98efeafd069bbfaea07e3);
				cff48f0c06d0d98efeafd069bbfaea07e2.c3e263e990bc8d6c3a2d636ac7a19c6c6.Grow(cff48f0c06d0d98efeafd069bbfaea07e3.c3e263e990bc8d6c3a2d636ac7a19c6c6);
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
			cff48f0c06d0d98efeafd069bbfaea07e2.c74dc8c78f0b932d691a8442fc47f4ad5 = true;
			c7f587bd57d47897172f47483e6bcca68.c6d3fb34101b9e1ce92055c2914d2d1de = cff48f0c06d0d98efeafd069bbfaea07e2;
		}
		if (num2 == 1)
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
			c7f587bd57d47897172f47483e6bcca68.c2d9d869d0c8fd5fb55f452375f580a4a = c7f587bd57d47897172f47483e6bcca68.c9594e2c7b7942e1d22a230fdeee51f04[c970f878d2fc540f781119125607c4ce];
		}
		else
		{
			cff48f0c06d0d98efeafd069bbfaea07e cff48f0c06d0d98efeafd069bbfaea07e4 = c53fef33d7344726da08aeb063ec62121.Obtain();
			cff48f0c06d0d98efeafd069bbfaea07e4.c9594e2c7b7942e1d22a230fdeee51f04 = ResourcePools<cff48f0c06d0d98efeafd069bbfaea07e>.Lists.Obtain();
			cff48f0c06d0d98efeafd069bbfaea07e cff48f0c06d0d98efeafd069bbfaea07e5 = c7f587bd57d47897172f47483e6bcca68.c9594e2c7b7942e1d22a230fdeee51f04[c970f878d2fc540f781119125607c4ce];
			cff48f0c06d0d98efeafd069bbfaea07e4.c9594e2c7b7942e1d22a230fdeee51f04.Add(cff48f0c06d0d98efeafd069bbfaea07e5);
			cff48f0c06d0d98efeafd069bbfaea07e4.c3e263e990bc8d6c3a2d636ac7a19c6c6 = cff48f0c06d0d98efeafd069bbfaea07e5.c3e263e990bc8d6c3a2d636ac7a19c6c6;
			for (int j = c970f878d2fc540f781119125607c4ce + 1; j < count; j++)
			{
				cff48f0c06d0d98efeafd069bbfaea07e5 = c7f587bd57d47897172f47483e6bcca68.c9594e2c7b7942e1d22a230fdeee51f04[j];
				cff48f0c06d0d98efeafd069bbfaea07e4.c9594e2c7b7942e1d22a230fdeee51f04.Add(cff48f0c06d0d98efeafd069bbfaea07e5);
				cff48f0c06d0d98efeafd069bbfaea07e4.c3e263e990bc8d6c3a2d636ac7a19c6c6.Grow(cff48f0c06d0d98efeafd069bbfaea07e5.c3e263e990bc8d6c3a2d636ac7a19c6c6);
			}
			cff48f0c06d0d98efeafd069bbfaea07e4.c74dc8c78f0b932d691a8442fc47f4ad5 = true;
			c7f587bd57d47897172f47483e6bcca68.c2d9d869d0c8fd5fb55f452375f580a4a = cff48f0c06d0d98efeafd069bbfaea07e4;
		}
		ResourcePools<cff48f0c06d0d98efeafd069bbfaea07e>.Lists.Recycle(c7f587bd57d47897172f47483e6bcca68.c9594e2c7b7942e1d22a230fdeee51f04);
		c7f587bd57d47897172f47483e6bcca68.c9594e2c7b7942e1d22a230fdeee51f04 = null;
		c7f587bd57d47897172f47483e6bcca68.c74dc8c78f0b932d691a8442fc47f4ad5 = false;
	}

	private static void c9a76abd2a74eafc9b1ae5461a5f70d04(List<cff48f0c06d0d98efeafd069bbfaea07e> cd4531a9ec3304b0c90a95f8f073929f4, int c59af201794f410b1aa702b1a6f9e7e5c, float cb76fbc629b698e3b1b873a267ebe714e, out int c970f878d2fc540f781119125607c4ce6)
	{
		int num = 0;
		c970f878d2fc540f781119125607c4ce6 = cd4531a9ec3304b0c90a95f8f073929f4.Count;
		while (num < c970f878d2fc540f781119125607c4ce6)
		{
			if (cd4531a9ec3304b0c90a95f8f073929f4[num].c3e263e990bc8d6c3a2d636ac7a19c6c6.Center[c59af201794f410b1aa702b1a6f9e7e5c] <= cb76fbc629b698e3b1b873a267ebe714e)
			{
				num++;
				continue;
			}
			c970f878d2fc540f781119125607c4ce6--;
			cff48f0c06d0d98efeafd069bbfaea07e value = cd4531a9ec3304b0c90a95f8f073929f4[num];
			cd4531a9ec3304b0c90a95f8f073929f4[num] = cd4531a9ec3304b0c90a95f8f073929f4[c970f878d2fc540f781119125607c4ce6];
			cd4531a9ec3304b0c90a95f8f073929f4[c970f878d2fc540f781119125607c4ce6] = value;
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
				__ldtoken(AdaptiveAabbTree<>.c9a76abd2a74eafc9b1ae5461a5f70d04(List<cff48f0c06d0d98efeafd069bbfaea07e>, int, float, ref int));
			}
			return;
		}
	}

	/// <summary>
	///       When implemented in a derived class, creates a new instance of the 
	///       <see cref="T:DigitalRune.Geometry.Partitioning.BasePartition`1" /> derived class.
	///       </summary>
	/// <returns>The new instance.</returns>
	protected override BasePartition<T> CreateInstanceCore()
	{
		return new AdaptiveAabbTree<T>();
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
		if (c3cb2c0edc4d55dd1410e3483d1ef60d4 != null)
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
				__ldtoken(AdaptiveAabbTree<>.ced5f5b0bd8302f3e8ade582954f3650d(bool, HashSet<T>, HashSet<T>, HashSet<T>));
			}
			if (!ce8ca0a645441ac556aaf6b4574dbbb1f)
			{
				int num;
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
					num = (((float)c7a74c15b192971583f3f676b15968063.Count > 0.66f * (float)Count) ? 1 : 0);
				}
				else
				{
					num = 1;
				}
				bool flag = (byte)num != 0;
				foreach (T item in c2176039f062c6700652655f4b1223258)
				{
					c3d1fce75fd975cd49156dca71180a13f(item);
				}
				if (c3cb2c0edc4d55dd1410e3483d1ef60d4 != null)
				{
					if (flag)
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
						c1a764b9e30d2bf7320c1c41fa091b3a2(c3cb2c0edc4d55dd1410e3483d1ef60d4);
					}
					else
					{
						c491a1eaa0478490b64a843d2289f78e1(c3cb2c0edc4d55dd1410e3483d1ef60d4, c7a74c15b192971583f3f676b15968063);
					}
				}
				foreach (T item2 in c049085d0fafa74bddedd7455b511e4d2)
				{
					c437cc650c1bbb3b5d6a346455cd9229c(item2);
				}
				goto IL_00fd;
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
		ccf1870247a728556a633ff212007937e();
		goto IL_00fd;
		IL_00fd:
		c527912fe7c25ec4be62695147b7edc1d();
		ce759ff2781e7c0601193b90d63f12c3c();
	}

	private void c527912fe7c25ec4be62695147b7edc1d()
	{
		Aabb = ((c3cb2c0edc4d55dd1410e3483d1ef60d4 != null) ? c3cb2c0edc4d55dd1410e3483d1ef60d4.c3e263e990bc8d6c3a2d636ac7a19c6c6 : default(Aabb));
	}

	private void ce759ff2781e7c0601193b90d63f12c3c()
	{
		if (!EnableSelfOverlaps)
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
				__ldtoken(AdaptiveAabbTree<>.ce759ff2781e7c0601193b90d63f12c3c());
			}
			c65379642bd949dc7e978580b9649b9bc.Clear();
			if (c3cb2c0edc4d55dd1410e3483d1ef60d4 == null)
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
				IEnumerator<Pair<T>> enumerator = GetOverlaps(this).GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						Pair<T> current = enumerator.Current;
						if (c8092bfef08eaf2732b73394233ee5fd1(current))
						{
							c65379642bd949dc7e978580b9649b9bc.Add(current);
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
		Stack<cff48f0c06d0d98efeafd069bbfaea07e> stack = ResourcePools<cff48f0c06d0d98efeafd069bbfaea07e>.Stacks.Obtain();
		stack.Push(c3cb2c0edc4d55dd1410e3483d1ef60d4);
		while (stack.Count > 0)
		{
			cff48f0c06d0d98efeafd069bbfaea07e cff48f0c06d0d98efeafd069bbfaea07e2 = stack.Pop();
			cff48f0c06d0d98efeafd069bbfaea07e2.c2c1eca2324d13154ef9733df393024a3 = true;
			if (GeometryHelper.HaveContact(cff48f0c06d0d98efeafd069bbfaea07e2.c3e263e990bc8d6c3a2d636ac7a19c6c6, aabb))
			{
				if (cff48f0c06d0d98efeafd069bbfaea07e2.cfd73e16b19bb8bde65db991a787eb5ad)
				{
					yield return cff48f0c06d0d98efeafd069bbfaea07e2.c652f269a40a2519b8658ffbade581b4e;
					continue;
				}
				c18ac18e9f88283e6a56a9ec9cb6b9e55(cff48f0c06d0d98efeafd069bbfaea07e2);
				stack.Push(cff48f0c06d0d98efeafd069bbfaea07e2.c2d9d869d0c8fd5fb55f452375f580a4a);
				stack.Push(cff48f0c06d0d98efeafd069bbfaea07e2.c6d3fb34101b9e1ce92055c2914d2d1de);
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
				__ldtoken(AdaptiveAabbTree<>.ca831cb15c64d60e3f4a4dd560aea08bf.MoveNext());
			}
			ResourcePools<cff48f0c06d0d98efeafd069bbfaea07e>.Stacks.Recycle(stack);
			yield break;
		}
	}

	private IEnumerable<T> ce38337505be42dfdd4b0b11dd3a5ea14(cff48f0c06d0d98efeafd069bbfaea07e ce9beaab8ed4de70dd0e96a36b4c42105)
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
				__ldtoken(AdaptiveAabbTree<>.c6ec9e72fe859503f5be53a128f1893fd.MoveNext());
			}
			Stack<cff48f0c06d0d98efeafd069bbfaea07e> stack = ResourcePools<cff48f0c06d0d98efeafd069bbfaea07e>.Stacks.Obtain();
			stack.Push(c3cb2c0edc4d55dd1410e3483d1ef60d4);
			while (stack.Count > 0)
			{
				cff48f0c06d0d98efeafd069bbfaea07e cff48f0c06d0d98efeafd069bbfaea07e2 = stack.Pop();
				cff48f0c06d0d98efeafd069bbfaea07e2.c2c1eca2324d13154ef9733df393024a3 = true;
				if (cff48f0c06d0d98efeafd069bbfaea07e2 != ce9beaab8ed4de70dd0e96a36b4c42105 && GeometryHelper.HaveContact(cff48f0c06d0d98efeafd069bbfaea07e2.c3e263e990bc8d6c3a2d636ac7a19c6c6, ce9beaab8ed4de70dd0e96a36b4c42105.c3e263e990bc8d6c3a2d636ac7a19c6c6))
				{
					if (cff48f0c06d0d98efeafd069bbfaea07e2.cfd73e16b19bb8bde65db991a787eb5ad)
					{
						yield return cff48f0c06d0d98efeafd069bbfaea07e2.c652f269a40a2519b8658ffbade581b4e;
						continue;
					}
					c18ac18e9f88283e6a56a9ec9cb6b9e55(cff48f0c06d0d98efeafd069bbfaea07e2);
					stack.Push(cff48f0c06d0d98efeafd069bbfaea07e2.c2d9d869d0c8fd5fb55f452375f580a4a);
					stack.Push(cff48f0c06d0d98efeafd069bbfaea07e2.c6d3fb34101b9e1ce92055c2914d2d1de);
				}
			}
			while (true)
			{
				switch (2)
				{
				case 0:
					continue;
				}
				ResourcePools<cff48f0c06d0d98efeafd069bbfaea07e>.Stacks.Recycle(stack);
				yield break;
			}
		}
	}

	private IEnumerable<cff48f0c06d0d98efeafd069bbfaea07e> c4d76d858205a75f799980f42e41cbe5b(Aabb cdc3439410d15f28489c964ee87b88fe9)
	{
		Update(forceRebuild: false);
		if (c3cb2c0edc4d55dd1410e3483d1ef60d4 == null)
		{
			yield break;
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
				__ldtoken(AdaptiveAabbTree<>.c8137f287f36c442d9765d959579fadce.MoveNext());
			}
			Stack<cff48f0c06d0d98efeafd069bbfaea07e> stack = ResourcePools<cff48f0c06d0d98efeafd069bbfaea07e>.Stacks.Obtain();
			stack.Push(c3cb2c0edc4d55dd1410e3483d1ef60d4);
			while (stack.Count > 0)
			{
				cff48f0c06d0d98efeafd069bbfaea07e cff48f0c06d0d98efeafd069bbfaea07e2 = stack.Pop();
				cff48f0c06d0d98efeafd069bbfaea07e2.c2c1eca2324d13154ef9733df393024a3 = true;
				if (GeometryHelper.HaveContact(cff48f0c06d0d98efeafd069bbfaea07e2.c3e263e990bc8d6c3a2d636ac7a19c6c6, cdc3439410d15f28489c964ee87b88fe9))
				{
					if (cff48f0c06d0d98efeafd069bbfaea07e2.cfd73e16b19bb8bde65db991a787eb5ad)
					{
						yield return cff48f0c06d0d98efeafd069bbfaea07e2;
						continue;
					}
					c18ac18e9f88283e6a56a9ec9cb6b9e55(cff48f0c06d0d98efeafd069bbfaea07e2);
					stack.Push(cff48f0c06d0d98efeafd069bbfaea07e2.c2d9d869d0c8fd5fb55f452375f580a4a);
					stack.Push(cff48f0c06d0d98efeafd069bbfaea07e2.c6d3fb34101b9e1ce92055c2914d2d1de);
				}
			}
			ResourcePools<cff48f0c06d0d98efeafd069bbfaea07e>.Stacks.Recycle(stack);
			yield break;
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
			switch (1)
			{
			case 0:
				continue;
			}
			if (1 == 0)
			{
				__ldtoken(AdaptiveAabbTree<>.cca2a86f2c74bbfda9787a352fc8b1fb1.MoveNext());
			}
			Vector3F c3ec982f192efbba86e42a1cea5c = new Vector3F(1f / ray.Direction.X, 1f / ray.Direction.Y, 1f / ray.Direction.Z);
			float c912da9d2d82e2a6523be60260abdfc = Numeric.EpsilonF * (1f + Aabb.Extent.Length);
			Stack<cff48f0c06d0d98efeafd069bbfaea07e> stack = ResourcePools<cff48f0c06d0d98efeafd069bbfaea07e>.Stacks.Obtain();
			stack.Push(c3cb2c0edc4d55dd1410e3483d1ef60d4);
			while (stack.Count > 0)
			{
				cff48f0c06d0d98efeafd069bbfaea07e cff48f0c06d0d98efeafd069bbfaea07e2 = stack.Pop();
				cff48f0c06d0d98efeafd069bbfaea07e2.c2c1eca2324d13154ef9733df393024a3 = true;
				if (!GeometryHelper.cdd6d17271fec83883494ba8e43eede6a(cff48f0c06d0d98efeafd069bbfaea07e2.c3e263e990bc8d6c3a2d636ac7a19c6c6, ray.Origin, c3ec982f192efbba86e42a1cea5c, ray.Length, c912da9d2d82e2a6523be60260abdfc))
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
				if (cff48f0c06d0d98efeafd069bbfaea07e2.cfd73e16b19bb8bde65db991a787eb5ad)
				{
					yield return cff48f0c06d0d98efeafd069bbfaea07e2.c652f269a40a2519b8658ffbade581b4e;
					continue;
				}
				c18ac18e9f88283e6a56a9ec9cb6b9e55(cff48f0c06d0d98efeafd069bbfaea07e2);
				stack.Push(cff48f0c06d0d98efeafd069bbfaea07e2.c2d9d869d0c8fd5fb55f452375f580a4a);
				stack.Push(cff48f0c06d0d98efeafd069bbfaea07e2.c6d3fb34101b9e1ce92055c2914d2d1de);
			}
			while (true)
			{
				switch (1)
				{
				case 0:
					continue;
				}
				ResourcePools<cff48f0c06d0d98efeafd069bbfaea07e>.Stacks.Recycle(stack);
				yield break;
			}
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
				switch (4)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(AdaptiveAabbTree<>.GetClosestPointCandidates(Aabb, float, Func<T, float>));
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

	private static void cb780b4b51062221f27d196ea0a7abde7(cff48f0c06d0d98efeafd069bbfaea07e c7f587bd57d47897172f47483e6bcca68, Aabb cdc3439410d15f28489c964ee87b88fe9, Func<T, float> ce4b7628f9880cef87f99eae50a8132fe, ref float c2878235e00baff8166ebc90ef27400ae)
	{
		if (c2878235e00baff8166ebc90ef27400ae < 0f)
		{
			return;
		}
		c7f587bd57d47897172f47483e6bcca68.c2c1eca2324d13154ef9733df393024a3 = true;
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
		c18ac18e9f88283e6a56a9ec9cb6b9e55(c7f587bd57d47897172f47483e6bcca68);
		cff48f0c06d0d98efeafd069bbfaea07e c6d3fb34101b9e1ce92055c2914d2d1de = c7f587bd57d47897172f47483e6bcca68.c6d3fb34101b9e1ce92055c2914d2d1de;
		cff48f0c06d0d98efeafd069bbfaea07e c2d9d869d0c8fd5fb55f452375f580a4a = c7f587bd57d47897172f47483e6bcca68.c2d9d869d0c8fd5fb55f452375f580a4a;
		if (c2878235e00baff8166ebc90ef27400ae == 0f)
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
						__ldtoken(AdaptiveAabbTree<>.cb780b4b51062221f27d196ea0a7abde7(cff48f0c06d0d98efeafd069bbfaea07e, Aabb, Func<T, float>, ref float));
					}
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
			while (true)
			{
				switch (1)
				{
				case 0:
					break;
				default:
					if (!(num > c2878235e00baff8166ebc90ef27400ae))
					{
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
					return;
				}
			}
		}
		if (num2 > c2878235e00baff8166ebc90ef27400ae)
		{
			return;
		}
		cb780b4b51062221f27d196ea0a7abde7(c2d9d869d0c8fd5fb55f452375f580a4a, cdc3439410d15f28489c964ee87b88fe9, ce4b7628f9880cef87f99eae50a8132fe, ref c2878235e00baff8166ebc90ef27400ae);
		if (num > c2878235e00baff8166ebc90ef27400ae)
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
		cb780b4b51062221f27d196ea0a7abde7(c6d3fb34101b9e1ce92055c2914d2d1de, cdc3439410d15f28489c964ee87b88fe9, ce4b7628f9880cef87f99eae50a8132fe, ref c2878235e00baff8166ebc90ef27400ae);
	}

	[CompilerGenerated]
	private static cbe5649b18b8b0caa00be9ad49188b193<T> c077d516cc0ec93dcbd062172861b23e1()
	{
		return c53fef33d7344726da08aeb063ec62121.Obtain();
	}

	[CompilerGenerated]
	private static cff48f0c06d0d98efeafd069bbfaea07e ccefec59c44b955da08f6a7becb3c953b()
	{
		return new cff48f0c06d0d98efeafd069bbfaea07e();
	}

	[CompilerGenerated]
	private static void cd0342a6f30d133ff7890a0a031d4a3b7(cff48f0c06d0d98efeafd069bbfaea07e c7f587bd57d47897172f47483e6bcca68)
	{
		c7f587bd57d47897172f47483e6bcca68.c6d3fb34101b9e1ce92055c2914d2d1de = null;
		c7f587bd57d47897172f47483e6bcca68.c2d9d869d0c8fd5fb55f452375f580a4a = null;
		c7f587bd57d47897172f47483e6bcca68.c652f269a40a2519b8658ffbade581b4e = default;
		c7f587bd57d47897172f47483e6bcca68.c2c1eca2324d13154ef9733df393024a3 = false;
		c7f587bd57d47897172f47483e6bcca68.c74dc8c78f0b932d691a8442fc47f4ad5 = true;
		if (c7f587bd57d47897172f47483e6bcca68.c9594e2c7b7942e1d22a230fdeee51f04 != null)
		{
			ResourcePools<cff48f0c06d0d98efeafd069bbfaea07e>.Lists.Recycle(c7f587bd57d47897172f47483e6bcca68.c9594e2c7b7942e1d22a230fdeee51f04);
			c7f587bd57d47897172f47483e6bcca68.c9594e2c7b7942e1d22a230fdeee51f04 = null;
		}
	}
}
