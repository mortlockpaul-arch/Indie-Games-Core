using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Linq;
using DigitalRune.Mathematics.Algebra;

namespace A;

internal static class c1706d55e3116d38c16c012a797e57db7
{
	[CompilerGenerated]
	private sealed class c1b6e8563d870a0e64e753acea86c4cba<c96c92d0bb3e5901860b75aee31593453> : IEnumerable<cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453>>, IEnumerator<cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453>>, IEnumerable, IEnumerator, IDisposable
	{
		private cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453> cd179d7f18da070d164af6afcb0af45b5;

		private int c94f69126adc708dfc5f5fd5b63c52835;

		private int c0e8a058e779916e7715d7616fb0da9e7;

		public cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453> c7f587bd57d47897172f47483e6bcca68;

		public cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453> cca6e936a335e74dc2a64c39349c7d35d;

		cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453> IEnumerator<cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453>>.Current
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
		public c1b6e8563d870a0e64e753acea86c4cba(int c94f69126adc708dfc5f5fd5b63c52835)
		{
			this.c94f69126adc708dfc5f5fd5b63c52835 = c94f69126adc708dfc5f5fd5b63c52835;
			c0e8a058e779916e7715d7616fb0da9e7 = Thread.CurrentThread.ManagedThreadId;
		}

		[DebuggerHidden]
		private IEnumerator<cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453>> c87926c59acd612c57eb84f701e7ebeca()
		{
			c1b6e8563d870a0e64e753acea86c4cba<c96c92d0bb3e5901860b75aee31593453> c1b6e8563d870a0e64e753acea86c4cba2;
			if (Thread.CurrentThread.ManagedThreadId == c0e8a058e779916e7715d7616fb0da9e7)
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
					__ldtoken(c1b6e8563d870a0e64e753acea86c4cba<>.c87926c59acd612c57eb84f701e7ebeca());
				}
				if (c94f69126adc708dfc5f5fd5b63c52835 == -2)
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
					c94f69126adc708dfc5f5fd5b63c52835 = 0;
					c1b6e8563d870a0e64e753acea86c4cba2 = this;
					goto IL_004f;
				}
			}
			c1b6e8563d870a0e64e753acea86c4cba2 = new c1b6e8563d870a0e64e753acea86c4cba<c96c92d0bb3e5901860b75aee31593453>(0);
			goto IL_004f;
			IL_004f:
			c1b6e8563d870a0e64e753acea86c4cba2.c7f587bd57d47897172f47483e6bcca68 = cca6e936a335e74dc2a64c39349c7d35d;
			return c1b6e8563d870a0e64e753acea86c4cba2;
		}

		IEnumerator<cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453>> IEnumerable<cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453>>.GetEnumerator()
		{
			//ILSpy generated this explicit interface implementation from .override directive in c87926c59acd612c57eb84f701e7ebeca
			return this.c87926c59acd612c57eb84f701e7ebeca();
		}

		[DebuggerHidden]
		private IEnumerator c63811cf6ff5c0b86c54fd5037c6af6ad()
		{
			return c87926c59acd612c57eb84f701e7ebeca();
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
				if (c7f587bd57d47897172f47483e6bcca68.cfd73e16b19bb8bde65db991a787eb5ad)
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
					if (1 == 0)
					{
						__ldtoken(c1b6e8563d870a0e64e753acea86c4cba<>.MoveNext());
					}
					cd179d7f18da070d164af6afcb0af45b5 = c7f587bd57d47897172f47483e6bcca68.c6d3fb34101b9e1ce92055c2914d2d1de;
					c94f69126adc708dfc5f5fd5b63c52835 = 1;
					return true;
				}
			case 1:
				c94f69126adc708dfc5f5fd5b63c52835 = -1;
				cd179d7f18da070d164af6afcb0af45b5 = c7f587bd57d47897172f47483e6bcca68.c2d9d869d0c8fd5fb55f452375f580a4a;
				c94f69126adc708dfc5f5fd5b63c52835 = 2;
				return true;
			case 2:
				c94f69126adc708dfc5f5fd5b63c52835 = -1;
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
	private sealed class cfdc5c466899668d4a4337d4398a2de37<c96c92d0bb3e5901860b75aee31593453>
	{
		public cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453> c7f587bd57d47897172f47483e6bcca68;

		public cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453> c9357f2701a8e3d7067e2cf0074e89cb2(cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453> cad632b6eef0c46e2f07e5911266d468e)
		{
			return c7f587bd57d47897172f47483e6bcca68.c551fa69e7c0ec492ab91daa4199210a2;
		}
	}

	public static IEnumerable<cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453>> c35f75f4d7b7ac3bc3fc4f727cd6c4017<c96c92d0bb3e5901860b75aee31593453>(this cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453> c7f587bd57d47897172f47483e6bcca68)
	{
		return TreeHelper.GetAncestors(c7f587bd57d47897172f47483e6bcca68, (cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453> cad632b6eef0c46e2f07e5911266d468e) => cad632b6eef0c46e2f07e5911266d468e.c551fa69e7c0ec492ab91daa4199210a2);
	}

	public static IEnumerable<cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453>> c0cf85ef66b73fde744babad31deb840f<c96c92d0bb3e5901860b75aee31593453>(this cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453> c7f587bd57d47897172f47483e6bcca68)
	{
		if (c7f587bd57d47897172f47483e6bcca68 == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(14042));
		}
		return cd7b732c11180e8c7cd68c59956e581e1(c7f587bd57d47897172f47483e6bcca68);
	}

	private static IEnumerable<cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453>> cd7b732c11180e8c7cd68c59956e581e1<c96c92d0bb3e5901860b75aee31593453>(cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453> c7f587bd57d47897172f47483e6bcca68)
	{
		if (c7f587bd57d47897172f47483e6bcca68.cfd73e16b19bb8bde65db991a787eb5ad)
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
				__ldtoken(c1b6e8563d870a0e64e753acea86c4cba<>.MoveNext());
			}
			yield return c7f587bd57d47897172f47483e6bcca68.c6d3fb34101b9e1ce92055c2914d2d1de;
			yield return c7f587bd57d47897172f47483e6bcca68.c2d9d869d0c8fd5fb55f452375f580a4a;
			yield break;
		}
	}

	public static IEnumerable<cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453>> c8c709365f902106a7ec27d02ec159d9a<c96c92d0bb3e5901860b75aee31593453>(this cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453> c7f587bd57d47897172f47483e6bcca68)
	{
		return TreeHelper.GetDescendants(c7f587bd57d47897172f47483e6bcca68, cd7b732c11180e8c7cd68c59956e581e1);
	}

	public static IEnumerable<cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453>> c8c709365f902106a7ec27d02ec159d9a<c96c92d0bb3e5901860b75aee31593453>(this cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453> c7f587bd57d47897172f47483e6bcca68, bool c4b5f6b6b3f82eb1c77e59c40ea035d00)
	{
		return TreeHelper.GetDescendants(c7f587bd57d47897172f47483e6bcca68, cd7b732c11180e8c7cd68c59956e581e1, c4b5f6b6b3f82eb1c77e59c40ea035d00);
	}

	public static IEnumerable<cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453>> c203a21e1e7a1a0c4a86e2ff8bf4b61c1<c96c92d0bb3e5901860b75aee31593453>(this cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453> c7f587bd57d47897172f47483e6bcca68)
	{
		return TreeHelper.GetSubtree(c7f587bd57d47897172f47483e6bcca68, cd7b732c11180e8c7cd68c59956e581e1);
	}

	public static IEnumerable<cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453>> c203a21e1e7a1a0c4a86e2ff8bf4b61c1<c96c92d0bb3e5901860b75aee31593453>(this cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453> c7f587bd57d47897172f47483e6bcca68, bool c4b5f6b6b3f82eb1c77e59c40ea035d00)
	{
		return TreeHelper.GetSubtree(c7f587bd57d47897172f47483e6bcca68, cd7b732c11180e8c7cd68c59956e581e1, c4b5f6b6b3f82eb1c77e59c40ea035d00);
	}

	public static IEnumerable<cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453>> c61563a55da5ea80093654e96ba5b1919<c96c92d0bb3e5901860b75aee31593453>(this cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453> c7f587bd57d47897172f47483e6bcca68)
	{
		return TreeHelper.GetLeaves(c7f587bd57d47897172f47483e6bcca68, cd7b732c11180e8c7cd68c59956e581e1);
	}

	public static int c8f9766cca635fcc9827a4a8956741060<c96c92d0bb3e5901860b75aee31593453>(this cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453> c7f587bd57d47897172f47483e6bcca68)
	{
		return TreeHelper.GetDepth(c7f587bd57d47897172f47483e6bcca68, (cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453> cad632b6eef0c46e2f07e5911266d468e) => c7f587bd57d47897172f47483e6bcca68.c551fa69e7c0ec492ab91daa4199210a2);
	}

	public static int ca6905e8d2f7056e25e40451585e99ba4<c96c92d0bb3e5901860b75aee31593453>(this cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453> c4685a88fbea6559872031aa4fdfc199c)
	{
		return TreeHelper.GetHeight(c4685a88fbea6559872031aa4fdfc199c, cd7b732c11180e8c7cd68c59956e581e1);
	}

	private static float c33bad845b6d3e84bcc70be353acf647e(Aabb c53c34f98e81f56eec1feba2e1612161e, Aabb c7348308c73f921441266c677fe295f0b)
	{
		Vector3F vector3F = c53c34f98e81f56eec1feba2e1612161e.Minimum + c53c34f98e81f56eec1feba2e1612161e.Maximum - (c7348308c73f921441266c677fe295f0b.Minimum + c7348308c73f921441266c677fe295f0b.Maximum);
		return Math.Abs(vector3F.X) + Math.Abs(vector3F.Y) + Math.Abs(vector3F.Z);
	}

	public static int c22645a0ac256427f72ed4eca4cbca2b7(Aabb c3968d3d184148129d73e888f77461428, Aabb c53c34f98e81f56eec1feba2e1612161e, Aabb c7348308c73f921441266c677fe295f0b)
	{
		float num = c33bad845b6d3e84bcc70be353acf647e(c3968d3d184148129d73e888f77461428, c53c34f98e81f56eec1feba2e1612161e);
		float num2 = c33bad845b6d3e84bcc70be353acf647e(c3968d3d184148129d73e888f77461428, c7348308c73f921441266c677fe295f0b);
		if (!(num < num2))
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
						__ldtoken(c1706d55e3116d38c16c012a797e57db7.c22645a0ac256427f72ed4eca4cbca2b7(Aabb, Aabb, Aabb));
					}
					return 1;
				}
			}
		}
		return 0;
	}

	[CompilerGenerated]
	private static cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453> c5a04d4eb5a58f41681988d20d3a3d1e8<c96c92d0bb3e5901860b75aee31593453>(cbe5649b18b8b0caa00be9ad49188b193<c96c92d0bb3e5901860b75aee31593453> cad632b6eef0c46e2f07e5911266d468e)
	{
		return cad632b6eef0c46e2f07e5911266d468e.c551fa69e7c0ec492ab91daa4199210a2;
	}
}
