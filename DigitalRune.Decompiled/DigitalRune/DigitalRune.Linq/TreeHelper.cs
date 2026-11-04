using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using A;

namespace DigitalRune.Linq;

/// <summary>
///       Provides new extension methods for traversing trees using LINQ.
///       </summary>
public static class TreeHelper
{
	[CompilerGenerated]
	private sealed class cab03a3b3daa1198443663791f7739f3f<c96c92d0bb3e5901860b75aee31593453> : IEnumerable<c96c92d0bb3e5901860b75aee31593453>, IEnumerator<c96c92d0bb3e5901860b75aee31593453>, IEnumerable, IEnumerator, IDisposable where c96c92d0bb3e5901860b75aee31593453 : class
	{
		private c96c92d0bb3e5901860b75aee31593453 cd179d7f18da070d164af6afcb0af45b5;

		private int c94f69126adc708dfc5f5fd5b63c52835;

		private int c0e8a058e779916e7715d7616fb0da9e7;

		public c96c92d0bb3e5901860b75aee31593453 c7f587bd57d47897172f47483e6bcca68;

		public c96c92d0bb3e5901860b75aee31593453 cca6e936a335e74dc2a64c39349c7d35d;

		public Func<c96c92d0bb3e5901860b75aee31593453, c96c92d0bb3e5901860b75aee31593453> c2b37eda0ee059d26493d34fdccbf3acf;

		public Func<c96c92d0bb3e5901860b75aee31593453, c96c92d0bb3e5901860b75aee31593453> cca16cf4c074d45711d528aa0a8b8a50c;

		public c96c92d0bb3e5901860b75aee31593453 cea2fda65479722779ea5e9f25ed6f727;

		public c96c92d0bb3e5901860b75aee31593453 c7c56eb7ce70088c674d1198f07a8dc58;

		c96c92d0bb3e5901860b75aee31593453 IEnumerator<c96c92d0bb3e5901860b75aee31593453>.Current
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
		public cab03a3b3daa1198443663791f7739f3f(int c94f69126adc708dfc5f5fd5b63c52835)
		{
			this.c94f69126adc708dfc5f5fd5b63c52835 = c94f69126adc708dfc5f5fd5b63c52835;
			c0e8a058e779916e7715d7616fb0da9e7 = Thread.CurrentThread.ManagedThreadId;
		}

		[DebuggerHidden]
		private IEnumerator<c96c92d0bb3e5901860b75aee31593453> c0843ff4e059c4450bad00d5ba0b6ef48()
		{
			cab03a3b3daa1198443663791f7739f3f<c96c92d0bb3e5901860b75aee31593453> cab03a3b3daa1198443663791f7739f3f2;
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
					__ldtoken(cab03a3b3daa1198443663791f7739f3f<>.c0843ff4e059c4450bad00d5ba0b6ef48());
				}
				c94f69126adc708dfc5f5fd5b63c52835 = 0;
				cab03a3b3daa1198443663791f7739f3f2 = this;
			}
			else
			{
				cab03a3b3daa1198443663791f7739f3f2 = new cab03a3b3daa1198443663791f7739f3f<c96c92d0bb3e5901860b75aee31593453>(0);
			}
			cab03a3b3daa1198443663791f7739f3f2.c7f587bd57d47897172f47483e6bcca68 = cca6e936a335e74dc2a64c39349c7d35d;
			cab03a3b3daa1198443663791f7739f3f2.c2b37eda0ee059d26493d34fdccbf3acf = cca16cf4c074d45711d528aa0a8b8a50c;
			return cab03a3b3daa1198443663791f7739f3f2;
		}

		IEnumerator<c96c92d0bb3e5901860b75aee31593453> IEnumerable<c96c92d0bb3e5901860b75aee31593453>.GetEnumerator()
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
				cea2fda65479722779ea5e9f25ed6f727 = c7f587bd57d47897172f47483e6bcca68;
				c7c56eb7ce70088c674d1198f07a8dc58 = c2b37eda0ee059d26493d34fdccbf3acf(cea2fda65479722779ea5e9f25ed6f727);
				goto IL_0085;
			case 1:
				{
					c94f69126adc708dfc5f5fd5b63c52835 = -1;
					cea2fda65479722779ea5e9f25ed6f727 = c7c56eb7ce70088c674d1198f07a8dc58;
					c7c56eb7ce70088c674d1198f07a8dc58 = c2b37eda0ee059d26493d34fdccbf3acf(cea2fda65479722779ea5e9f25ed6f727);
					goto IL_0085;
				}
				IL_0085:
				if (c7c56eb7ce70088c674d1198f07a8dc58 != null)
				{
					cd179d7f18da070d164af6afcb0af45b5 = c7c56eb7ce70088c674d1198f07a8dc58;
					c94f69126adc708dfc5f5fd5b63c52835 = 1;
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
				if (1 == 0)
				{
					__ldtoken(cab03a3b3daa1198443663791f7739f3f<>.MoveNext());
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
	private sealed class c3af5c2706daf14faf1833043d66c0e3a<c96c92d0bb3e5901860b75aee31593453> : IEnumerable<c96c92d0bb3e5901860b75aee31593453>, IEnumerator<c96c92d0bb3e5901860b75aee31593453>, IEnumerable, IEnumerator, IDisposable where c96c92d0bb3e5901860b75aee31593453 : class
	{
		private c96c92d0bb3e5901860b75aee31593453 cd179d7f18da070d164af6afcb0af45b5;

		private int c94f69126adc708dfc5f5fd5b63c52835;

		private int c0e8a058e779916e7715d7616fb0da9e7;

		public c96c92d0bb3e5901860b75aee31593453 c7f587bd57d47897172f47483e6bcca68;

		public c96c92d0bb3e5901860b75aee31593453 cca6e936a335e74dc2a64c39349c7d35d;

		public Func<c96c92d0bb3e5901860b75aee31593453, IEnumerable<c96c92d0bb3e5901860b75aee31593453>> c453449c4ca0a8d4e6d0d725121da8176;

		public Func<c96c92d0bb3e5901860b75aee31593453, IEnumerable<c96c92d0bb3e5901860b75aee31593453>> c39d096818913412e87f4d36f5752de2b;

		public bool c4b5f6b6b3f82eb1c77e59c40ea035d00;

		public bool c9170ffdf91fb9958c55e52d0ac2de49a;

		public Stack<c96c92d0bb3e5901860b75aee31593453> c04e6049d4b5805e1c1bbb6417f5b96a8;

		public c96c92d0bb3e5901860b75aee31593453 cb153065655f2c2f648df010f3e5b8290;

		public Queue<c96c92d0bb3e5901860b75aee31593453> c5bfbbafc3c74d72d609e390107d85f2a;

		public c96c92d0bb3e5901860b75aee31593453 c4c8524340e4ffebb8ca8a8dac82e222b;

		c96c92d0bb3e5901860b75aee31593453 IEnumerator<c96c92d0bb3e5901860b75aee31593453>.Current
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
		public c3af5c2706daf14faf1833043d66c0e3a(int c94f69126adc708dfc5f5fd5b63c52835)
		{
			this.c94f69126adc708dfc5f5fd5b63c52835 = c94f69126adc708dfc5f5fd5b63c52835;
			c0e8a058e779916e7715d7616fb0da9e7 = Thread.CurrentThread.ManagedThreadId;
		}

		[DebuggerHidden]
		private IEnumerator<c96c92d0bb3e5901860b75aee31593453> c0843ff4e059c4450bad00d5ba0b6ef48()
		{
			c3af5c2706daf14faf1833043d66c0e3a<c96c92d0bb3e5901860b75aee31593453> c3af5c2706daf14faf1833043d66c0e3a2;
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
					__ldtoken(c3af5c2706daf14faf1833043d66c0e3a<>.c0843ff4e059c4450bad00d5ba0b6ef48());
				}
				c94f69126adc708dfc5f5fd5b63c52835 = 0;
				c3af5c2706daf14faf1833043d66c0e3a2 = this;
			}
			else
			{
				c3af5c2706daf14faf1833043d66c0e3a2 = new c3af5c2706daf14faf1833043d66c0e3a<c96c92d0bb3e5901860b75aee31593453>(0);
			}
			c3af5c2706daf14faf1833043d66c0e3a2.c7f587bd57d47897172f47483e6bcca68 = cca6e936a335e74dc2a64c39349c7d35d;
			c3af5c2706daf14faf1833043d66c0e3a2.c453449c4ca0a8d4e6d0d725121da8176 = c39d096818913412e87f4d36f5752de2b;
			c3af5c2706daf14faf1833043d66c0e3a2.c4b5f6b6b3f82eb1c77e59c40ea035d00 = c9170ffdf91fb9958c55e52d0ac2de49a;
			return c3af5c2706daf14faf1833043d66c0e3a2;
		}

		IEnumerator<c96c92d0bb3e5901860b75aee31593453> IEnumerable<c96c92d0bb3e5901860b75aee31593453>.GetEnumerator()
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
			IEnumerator<c96c92d0bb3e5901860b75aee31593453> enumerator2;
			switch (c94f69126adc708dfc5f5fd5b63c52835)
			{
			case 0:
				c94f69126adc708dfc5f5fd5b63c52835 = -1;
				if (c4b5f6b6b3f82eb1c77e59c40ea035d00)
				{
					c04e6049d4b5805e1c1bbb6417f5b96a8 = new Stack<c96c92d0bb3e5901860b75aee31593453>();
					c04e6049d4b5805e1c1bbb6417f5b96a8.Push(c7f587bd57d47897172f47483e6bcca68);
					goto IL_010a;
				}
				c5bfbbafc3c74d72d609e390107d85f2a = new Queue<c96c92d0bb3e5901860b75aee31593453>();
				c5bfbbafc3c74d72d609e390107d85f2a.Enqueue(c7f587bd57d47897172f47483e6bcca68);
				goto IL_01f1;
			case 1:
				c94f69126adc708dfc5f5fd5b63c52835 = -1;
				goto IL_009a;
			case 3:
				{
					c94f69126adc708dfc5f5fd5b63c52835 = -1;
					goto IL_0194;
				}
				IL_0194:
				using (IEnumerator<c96c92d0bb3e5901860b75aee31593453> enumerator = c453449c4ca0a8d4e6d0d725121da8176(c4c8524340e4ffebb8ca8a8dac82e222b).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						c96c92d0bb3e5901860b75aee31593453 current = enumerator.Current;
						if (current != null)
						{
							c5bfbbafc3c74d72d609e390107d85f2a.Enqueue(current);
						}
					}
					while (true)
					{
						switch (7)
						{
						case 0:
							break;
						default:
							goto end_IL_01d9;
						}
						continue;
						end_IL_01d9:
						break;
					}
				}
				goto IL_01f1;
				IL_010a:
				if (c04e6049d4b5805e1c1bbb6417f5b96a8.Count > 0)
				{
					cb153065655f2c2f648df010f3e5b8290 = c04e6049d4b5805e1c1bbb6417f5b96a8.Pop();
					if (cb153065655f2c2f648df010f3e5b8290 != c7f587bd57d47897172f47483e6bcca68)
					{
						cd179d7f18da070d164af6afcb0af45b5 = cb153065655f2c2f648df010f3e5b8290;
						c94f69126adc708dfc5f5fd5b63c52835 = 1;
						return true;
					}
					goto IL_009a;
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
				IL_009a:
				enumerator2 = c453449c4ca0a8d4e6d0d725121da8176(cb153065655f2c2f648df010f3e5b8290).Reverse().GetEnumerator();
				try
				{
					while (enumerator2.MoveNext())
					{
						c96c92d0bb3e5901860b75aee31593453 current2 = enumerator2.Current;
						if (current2 == null)
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
							__ldtoken(c3af5c2706daf14faf1833043d66c0e3a<>.MoveNext());
						}
						c04e6049d4b5805e1c1bbb6417f5b96a8.Push(current2);
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
							enumerator2.Dispose();
							break;
						}
					}
				}
				goto IL_010a;
				IL_01f1:
				if (c5bfbbafc3c74d72d609e390107d85f2a.Count <= 0)
				{
					break;
				}
				c4c8524340e4ffebb8ca8a8dac82e222b = c5bfbbafc3c74d72d609e390107d85f2a.Dequeue();
				if (c4c8524340e4ffebb8ca8a8dac82e222b != c7f587bd57d47897172f47483e6bcca68)
				{
					cd179d7f18da070d164af6afcb0af45b5 = c4c8524340e4ffebb8ca8a8dac82e222b;
					c94f69126adc708dfc5f5fd5b63c52835 = 3;
					return true;
				}
				goto IL_0194;
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
	private sealed class cb79402d4f1c60d532adcb0c704ef0115<c96c92d0bb3e5901860b75aee31593453> : IEnumerable<c96c92d0bb3e5901860b75aee31593453>, IEnumerator<c96c92d0bb3e5901860b75aee31593453>, IEnumerable, IEnumerator, IDisposable where c96c92d0bb3e5901860b75aee31593453 : class
	{
		private c96c92d0bb3e5901860b75aee31593453 cd179d7f18da070d164af6afcb0af45b5;

		private int c94f69126adc708dfc5f5fd5b63c52835;

		private int c0e8a058e779916e7715d7616fb0da9e7;

		public c96c92d0bb3e5901860b75aee31593453 c7f587bd57d47897172f47483e6bcca68;

		public c96c92d0bb3e5901860b75aee31593453 cca6e936a335e74dc2a64c39349c7d35d;

		public Func<c96c92d0bb3e5901860b75aee31593453, IEnumerable<c96c92d0bb3e5901860b75aee31593453>> c453449c4ca0a8d4e6d0d725121da8176;

		public Func<c96c92d0bb3e5901860b75aee31593453, IEnumerable<c96c92d0bb3e5901860b75aee31593453>> c39d096818913412e87f4d36f5752de2b;

		public bool c4b5f6b6b3f82eb1c77e59c40ea035d00;

		public bool c9170ffdf91fb9958c55e52d0ac2de49a;

		public Stack<c96c92d0bb3e5901860b75aee31593453> c3700f1b228723b1ba51387c3f22d5508;

		public c96c92d0bb3e5901860b75aee31593453 ca258232dd8e1ef91b170bc7453ee8392;

		public Queue<c96c92d0bb3e5901860b75aee31593453> c9c280f9b824143da656e13026d1e1910;

		public c96c92d0bb3e5901860b75aee31593453 cd7187112faa5cb5ee198b63440c22727;

		c96c92d0bb3e5901860b75aee31593453 IEnumerator<c96c92d0bb3e5901860b75aee31593453>.Current
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
		public cb79402d4f1c60d532adcb0c704ef0115(int c94f69126adc708dfc5f5fd5b63c52835)
		{
			this.c94f69126adc708dfc5f5fd5b63c52835 = c94f69126adc708dfc5f5fd5b63c52835;
			c0e8a058e779916e7715d7616fb0da9e7 = Thread.CurrentThread.ManagedThreadId;
		}

		[DebuggerHidden]
		private IEnumerator<c96c92d0bb3e5901860b75aee31593453> c0843ff4e059c4450bad00d5ba0b6ef48()
		{
			cb79402d4f1c60d532adcb0c704ef0115<c96c92d0bb3e5901860b75aee31593453> cb79402d4f1c60d532adcb0c704ef116;
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
					__ldtoken(cb79402d4f1c60d532adcb0c704ef0115<>.c0843ff4e059c4450bad00d5ba0b6ef48());
				}
				c94f69126adc708dfc5f5fd5b63c52835 = 0;
				cb79402d4f1c60d532adcb0c704ef116 = this;
			}
			else
			{
				cb79402d4f1c60d532adcb0c704ef116 = new cb79402d4f1c60d532adcb0c704ef0115<c96c92d0bb3e5901860b75aee31593453>(0);
			}
			cb79402d4f1c60d532adcb0c704ef116.c7f587bd57d47897172f47483e6bcca68 = cca6e936a335e74dc2a64c39349c7d35d;
			cb79402d4f1c60d532adcb0c704ef116.c453449c4ca0a8d4e6d0d725121da8176 = c39d096818913412e87f4d36f5752de2b;
			cb79402d4f1c60d532adcb0c704ef116.c4b5f6b6b3f82eb1c77e59c40ea035d00 = c9170ffdf91fb9958c55e52d0ac2de49a;
			return cb79402d4f1c60d532adcb0c704ef116;
		}

		IEnumerator<c96c92d0bb3e5901860b75aee31593453> IEnumerable<c96c92d0bb3e5901860b75aee31593453>.GetEnumerator()
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
				if (c4b5f6b6b3f82eb1c77e59c40ea035d00)
				{
					c3700f1b228723b1ba51387c3f22d5508 = new Stack<c96c92d0bb3e5901860b75aee31593453>();
					c3700f1b228723b1ba51387c3f22d5508.Push(c7f587bd57d47897172f47483e6bcca68);
					goto IL_00fa;
				}
				c9c280f9b824143da656e13026d1e1910 = new Queue<c96c92d0bb3e5901860b75aee31593453>();
				c9c280f9b824143da656e13026d1e1910.Enqueue(c7f587bd57d47897172f47483e6bcca68);
				goto IL_01cf;
			case 1:
			{
				c94f69126adc708dfc5f5fd5b63c52835 = -1;
				IEnumerator<c96c92d0bb3e5901860b75aee31593453> enumerator2 = c453449c4ca0a8d4e6d0d725121da8176(ca258232dd8e1ef91b170bc7453ee8392).Reverse().GetEnumerator();
				try
				{
					while (enumerator2.MoveNext())
					{
						c96c92d0bb3e5901860b75aee31593453 current2 = enumerator2.Current;
						if (current2 == null)
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
						if (1 == 0)
						{
							__ldtoken(cb79402d4f1c60d532adcb0c704ef0115<>.MoveNext());
						}
						c3700f1b228723b1ba51387c3f22d5508.Push(current2);
					}
					while (true)
					{
						switch (3)
						{
						case 0:
							break;
						default:
							goto end_IL_00da;
						}
						continue;
						end_IL_00da:
						break;
					}
				}
				finally
				{
					if (enumerator2 != null)
					{
						while (true)
						{
							switch (7)
							{
							case 0:
								continue;
							}
							enumerator2.Dispose();
							break;
						}
					}
				}
				goto IL_00fa;
			}
			case 3:
				{
					c94f69126adc708dfc5f5fd5b63c52835 = -1;
					IEnumerator<c96c92d0bb3e5901860b75aee31593453> enumerator = c453449c4ca0a8d4e6d0d725121da8176(cd7187112faa5cb5ee198b63440c22727).GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							c96c92d0bb3e5901860b75aee31593453 current = enumerator.Current;
							if (current != null)
							{
								c9c280f9b824143da656e13026d1e1910.Enqueue(current);
							}
						}
						while (true)
						{
							switch (6)
							{
							case 0:
								break;
							default:
								goto end_IL_01ad;
							}
							continue;
							end_IL_01ad:
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
					goto IL_01cf;
				}
				IL_01cf:
				if (c9c280f9b824143da656e13026d1e1910.Count > 0)
				{
					cd7187112faa5cb5ee198b63440c22727 = c9c280f9b824143da656e13026d1e1910.Dequeue();
					cd179d7f18da070d164af6afcb0af45b5 = cd7187112faa5cb5ee198b63440c22727;
					c94f69126adc708dfc5f5fd5b63c52835 = 3;
					return true;
				}
				break;
				IL_00fa:
				if (c3700f1b228723b1ba51387c3f22d5508.Count > 0)
				{
					ca258232dd8e1ef91b170bc7453ee8392 = c3700f1b228723b1ba51387c3f22d5508.Pop();
					cd179d7f18da070d164af6afcb0af45b5 = ca258232dd8e1ef91b170bc7453ee8392;
					c94f69126adc708dfc5f5fd5b63c52835 = 1;
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
	private sealed class c05b483297f192a0ec998c973ea73e39f<c96c92d0bb3e5901860b75aee31593453> : IEnumerable<c96c92d0bb3e5901860b75aee31593453>, IEnumerator<c96c92d0bb3e5901860b75aee31593453>, IEnumerable, IEnumerator, IDisposable where c96c92d0bb3e5901860b75aee31593453 : class
	{
		private c96c92d0bb3e5901860b75aee31593453 cd179d7f18da070d164af6afcb0af45b5;

		private int c94f69126adc708dfc5f5fd5b63c52835;

		private int c0e8a058e779916e7715d7616fb0da9e7;

		public c96c92d0bb3e5901860b75aee31593453 c7f587bd57d47897172f47483e6bcca68;

		public c96c92d0bb3e5901860b75aee31593453 cca6e936a335e74dc2a64c39349c7d35d;

		public Func<c96c92d0bb3e5901860b75aee31593453, IEnumerable<c96c92d0bb3e5901860b75aee31593453>> c453449c4ca0a8d4e6d0d725121da8176;

		public Func<c96c92d0bb3e5901860b75aee31593453, IEnumerable<c96c92d0bb3e5901860b75aee31593453>> c39d096818913412e87f4d36f5752de2b;

		public Stack<c96c92d0bb3e5901860b75aee31593453> cdcc268b95d32e6e61572c0516c5c3399;

		public c96c92d0bb3e5901860b75aee31593453 cd635930546ff610fff5557a911d95a00;

		public IEnumerable<c96c92d0bb3e5901860b75aee31593453> c6562c79f79564bfe713d655674ba2cf1;

		c96c92d0bb3e5901860b75aee31593453 IEnumerator<c96c92d0bb3e5901860b75aee31593453>.Current
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
		public c05b483297f192a0ec998c973ea73e39f(int c94f69126adc708dfc5f5fd5b63c52835)
		{
			this.c94f69126adc708dfc5f5fd5b63c52835 = c94f69126adc708dfc5f5fd5b63c52835;
			c0e8a058e779916e7715d7616fb0da9e7 = Thread.CurrentThread.ManagedThreadId;
		}

		[DebuggerHidden]
		private IEnumerator<c96c92d0bb3e5901860b75aee31593453> c0843ff4e059c4450bad00d5ba0b6ef48()
		{
			c05b483297f192a0ec998c973ea73e39f<c96c92d0bb3e5901860b75aee31593453> c05b483297f192a0ec998c973ea73e39f2;
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
					__ldtoken(c05b483297f192a0ec998c973ea73e39f<>.c0843ff4e059c4450bad00d5ba0b6ef48());
				}
				c94f69126adc708dfc5f5fd5b63c52835 = 0;
				c05b483297f192a0ec998c973ea73e39f2 = this;
			}
			else
			{
				c05b483297f192a0ec998c973ea73e39f2 = new c05b483297f192a0ec998c973ea73e39f<c96c92d0bb3e5901860b75aee31593453>(0);
			}
			c05b483297f192a0ec998c973ea73e39f2.c7f587bd57d47897172f47483e6bcca68 = cca6e936a335e74dc2a64c39349c7d35d;
			c05b483297f192a0ec998c973ea73e39f2.c453449c4ca0a8d4e6d0d725121da8176 = c39d096818913412e87f4d36f5752de2b;
			return c05b483297f192a0ec998c973ea73e39f2;
		}

		IEnumerator<c96c92d0bb3e5901860b75aee31593453> IEnumerable<c96c92d0bb3e5901860b75aee31593453>.GetEnumerator()
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
				cdcc268b95d32e6e61572c0516c5c3399 = new Stack<c96c92d0bb3e5901860b75aee31593453>();
				cdcc268b95d32e6e61572c0516c5c3399.Push(c7f587bd57d47897172f47483e6bcca68);
				goto IL_0100;
			case 1:
				{
					c94f69126adc708dfc5f5fd5b63c52835 = -1;
					goto IL_0100;
				}
				IL_0100:
				while (cdcc268b95d32e6e61572c0516c5c3399.Count > 0)
				{
					cd635930546ff610fff5557a911d95a00 = cdcc268b95d32e6e61572c0516c5c3399.Pop();
					c6562c79f79564bfe713d655674ba2cf1 = c453449c4ca0a8d4e6d0d725121da8176(cd635930546ff610fff5557a911d95a00);
					if (!c6562c79f79564bfe713d655674ba2cf1.Any())
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
									__ldtoken(c05b483297f192a0ec998c973ea73e39f<>.MoveNext());
								}
								cd179d7f18da070d164af6afcb0af45b5 = cd635930546ff610fff5557a911d95a00;
								c94f69126adc708dfc5f5fd5b63c52835 = 1;
								return true;
							}
						}
					}
					IEnumerator<c96c92d0bb3e5901860b75aee31593453> enumerator = c6562c79f79564bfe713d655674ba2cf1.Reverse().GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							c96c92d0bb3e5901860b75aee31593453 current = enumerator.Current;
							if (current != null)
							{
								cdcc268b95d32e6e61572c0516c5c3399.Push(current);
							}
						}
						while (true)
						{
							switch (1)
							{
							case 0:
								break;
							default:
								goto end_IL_00e0;
							}
							continue;
							end_IL_00e0:
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

	/// <summary>
	///       Gets the ancestors of a certain node.
	///       </summary>
	/// <param name="node">
	///       The reference node where to start the search. (The reference node will not be included in 
	///       the resulting enumeration.)
	///       </param>
	/// <param name="getParent">
	///   <para>
	///       A method that retrieves the parent object for a node of type <typeparamref name="T" />.
	///       </para>
	///   <para>
	///       The method should return <see langword="null" /> to indicate that a node does not have a
	///       parent. <see cref="M:DigitalRune.Linq.TreeHelper.GetAncestors``1(``0,System.Func{``0,``0})" /> guarantees that <paramref name="getParent" /> is never 
	///       called with <see langword="null" /> as parameter.
	///       </para>
	/// </param>
	/// <typeparam name="T">The type of node that is enumerated.</typeparam>
	/// <returns>The ancestors of <paramref name="node" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="node" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="getParent" /> is <see langword="null" />.
	///       </exception>
	public static IEnumerable<T> GetAncestors<T>(T node, Func<T, T> getParent) where T : class
	{
		if (node == null)
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
						__ldtoken(TreeHelper.GetAncestors<T>(T, Func<T, T>));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5512));
				}
			}
		}
		if (getParent == null)
		{
			while (true)
			{
				switch (1)
				{
				case 0:
					break;
				default:
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5521));
				}
			}
		}
		return cc1ea8bb580384d30fd8668031285ebcf(node, getParent);
	}

	private static IEnumerable<c96c92d0bb3e5901860b75aee31593453> cc1ea8bb580384d30fd8668031285ebcf<c96c92d0bb3e5901860b75aee31593453>(c96c92d0bb3e5901860b75aee31593453 c7f587bd57d47897172f47483e6bcca68, Func<c96c92d0bb3e5901860b75aee31593453, c96c92d0bb3e5901860b75aee31593453> c2b37eda0ee059d26493d34fdccbf3acf) where c96c92d0bb3e5901860b75aee31593453 : class
	{
		c96c92d0bb3e5901860b75aee31593453 arg = c7f587bd57d47897172f47483e6bcca68;
		for (c96c92d0bb3e5901860b75aee31593453 val = c2b37eda0ee059d26493d34fdccbf3acf(arg); val != null; val = c2b37eda0ee059d26493d34fdccbf3acf(arg))
		{
			yield return val;
			arg = val;
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
				__ldtoken(cab03a3b3daa1198443663791f7739f3f<>.MoveNext());
			}
			yield break;
		}
	}

	/// <summary>
	///       Gets the descendants of a given node using a depth-first search.
	///       </summary>
	/// <param name="node">
	///       The reference node where to start the search. (The reference node will not be included 
	///       in the enumeration.)
	///       </param>
	/// <param name="getChildren">
	///   <para>
	///       A method that retrieves the children of an object of type <typeparamref name="T" />. 
	///       </para>
	///   <para>
	///     <see cref="M:DigitalRune.Linq.TreeHelper.GetDescendants``1(``0,System.Func{``0,System.Collections.Generic.IEnumerable{``0}})" /> guarantees that 
	///       <paramref name="getChildren" /> is never called with <see langword="null" /> as parameter. The
	///       enumeration returned by <paramref name="getChildren" /> may contain <see langword="null" />.
	///       </para>
	/// </param>
	/// <typeparam name="T">The type of node that is enumerated.</typeparam>
	/// <returns>The descendants of <paramref name="node" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="node" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="getChildren" /> is <see langword="null" />.
	///       </exception>
	public static IEnumerable<T> GetDescendants<T>(T node, Func<T, IEnumerable<T>> getChildren) where T : class
	{
		return GetDescendants(node, getChildren, depthFirst: true);
	}

	/// <summary>
	///       Gets the descendants of a given node using a depth-first search or a breadth-first 
	///       search.
	///       </summary>
	/// <param name="node">
	///       The reference node where to start the search. (The reference node will not be included in 
	///       the enumeration.)
	///       </param>
	/// <param name="getChildren">
	///   <para>
	///       A method that retrieves the children of an object of type <typeparamref name="T" />. 
	///       </para>
	///   <para>
	///     <see cref="M:DigitalRune.Linq.TreeHelper.GetDescendants``1(``0,System.Func{``0,System.Collections.Generic.IEnumerable{``0}},System.Boolean)" /> guarantees that 
	///       <paramref name="getChildren" /> is never called with <see langword="null" /> as parameter. The
	///       enumeration returned by <paramref name="getChildren" /> may contain <see langword="null" />.
	///       </para>
	/// </param>
	/// <param name="depthFirst">
	///       If set to <see langword="true" /> then a depth-first search for descendants will be made; 
	///       otherwise a breadth-first search will be made.
	///       </param>
	/// <typeparam name="T">The type of node that is enumerated.</typeparam>
	/// <returns>The descendants of <paramref name="node" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="node" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="getChildren" /> is <see langword="null" />.
	///       </exception>
	public static IEnumerable<T> GetDescendants<T>(T node, Func<T, IEnumerable<T>> getChildren, bool depthFirst) where T : class
	{
		if (node == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5512));
		}
		if (getChildren == null)
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
						__ldtoken(TreeHelper.GetDescendants<T>(T, Func<T, IEnumerable<T>>, bool));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5540));
				}
			}
		}
		return c081d3780ccf5a48f961a6210afde7751(node, getChildren, depthFirst);
	}

	private static IEnumerable<c96c92d0bb3e5901860b75aee31593453> c081d3780ccf5a48f961a6210afde7751<c96c92d0bb3e5901860b75aee31593453>(c96c92d0bb3e5901860b75aee31593453 c7f587bd57d47897172f47483e6bcca68, Func<c96c92d0bb3e5901860b75aee31593453, IEnumerable<c96c92d0bb3e5901860b75aee31593453>> c453449c4ca0a8d4e6d0d725121da8176, bool c4b5f6b6b3f82eb1c77e59c40ea035d00) where c96c92d0bb3e5901860b75aee31593453 : class
	{
		if (c4b5f6b6b3f82eb1c77e59c40ea035d00)
		{
			Stack<c96c92d0bb3e5901860b75aee31593453> stack = new Stack<c96c92d0bb3e5901860b75aee31593453>();
			stack.Push(c7f587bd57d47897172f47483e6bcca68);
			while (stack.Count > 0)
			{
				c96c92d0bb3e5901860b75aee31593453 val = stack.Pop();
				if (val != c7f587bd57d47897172f47483e6bcca68)
				{
					yield return val;
				}
				IEnumerator<c96c92d0bb3e5901860b75aee31593453> enumerator = c453449c4ca0a8d4e6d0d725121da8176(val).Reverse().GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						c96c92d0bb3e5901860b75aee31593453 current = enumerator.Current;
						if (current == null)
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
							__ldtoken(c3af5c2706daf14faf1833043d66c0e3a<>.MoveNext());
						}
						stack.Push(current);
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
					yield break;
				}
			}
		}
		Queue<c96c92d0bb3e5901860b75aee31593453> queue = new Queue<c96c92d0bb3e5901860b75aee31593453>();
		queue.Enqueue(c7f587bd57d47897172f47483e6bcca68);
		while (queue.Count > 0)
		{
			c96c92d0bb3e5901860b75aee31593453 val2 = queue.Dequeue();
			if (val2 != c7f587bd57d47897172f47483e6bcca68)
			{
				yield return val2;
			}
			using IEnumerator<c96c92d0bb3e5901860b75aee31593453> enumerator2 = c453449c4ca0a8d4e6d0d725121da8176(val2).GetEnumerator();
			while (enumerator2.MoveNext())
			{
				c96c92d0bb3e5901860b75aee31593453 current2 = enumerator2.Current;
				if (current2 != null)
				{
					queue.Enqueue(current2);
				}
			}
			while (true)
			{
				switch (7)
				{
				case 0:
					break;
				default:
					goto end_IL_01d9;
				}
				continue;
				end_IL_01d9:
				break;
			}
		}
	}

	/// <summary>
	///       Gets the subtree (the given node and all of its descendants) using a depth-first search.
	///       </summary>
	/// <param name="node">
	///       The reference node where to start the search. (The reference node will be the first 
	///       element in the enumeration.)
	///       </param>
	/// <param name="getChildren">
	///   <para>
	///       A method that retrieves the children of an object of type <typeparamref name="T" />. 
	///       </para>
	///   <para>
	///     <see cref="M:DigitalRune.Linq.TreeHelper.GetSubtree``1(``0,System.Func{``0,System.Collections.Generic.IEnumerable{``0}})" /> guarantees that 
	///       <paramref name="getChildren" /> is never called with <see langword="null" /> as parameter. The
	///       enumeration returned by <paramref name="getChildren" /> may contain <see langword="null" />.
	///       </para>
	/// </param>
	/// <typeparam name="T">The type of node that is enumerated.</typeparam>
	/// <returns>The subtree of <paramref name="node" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="node" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="getChildren" /> is <see langword="null" />.
	///       </exception>
	public static IEnumerable<T> GetSubtree<T>(T node, Func<T, IEnumerable<T>> getChildren) where T : class
	{
		return GetSubtree(node, getChildren, depthFirst: true);
	}

	/// <summary>
	///       Gets the subtree (the given node and all of its descendants) using a depth-first search or a 
	///       breadth-first search.
	///       </summary>
	/// <param name="node">
	///       The reference node where to start the search. (The reference node will be the first 
	///       element in the enumeration.)
	///       </param>
	/// <param name="getChildren">
	///   <para>
	///       A method that retrieves the children of an object of type <typeparamref name="T" />. 
	///       </para>
	///   <para>
	///     <see cref="M:DigitalRune.Linq.TreeHelper.GetSubtree``1(``0,System.Func{``0,System.Collections.Generic.IEnumerable{``0}})" /> guarantees that 
	///       <paramref name="getChildren" /> is never called with <see langword="null" /> as parameter. The
	///       enumeration returned by <paramref name="getChildren" /> may contain <see langword="null" />.
	///       </para>
	/// </param>
	/// <param name="depthFirst">
	///       If set to <see langword="true" /> then a depth-first search for descendants will be made; 
	///       otherwise a breadth-first search will be made.
	///       </param>
	/// <typeparam name="T">The type of node that is enumerated.</typeparam>
	/// <returns>The subtree of <paramref name="node" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="node" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="getChildren" /> is <see langword="null" />.
	///       </exception>
	public static IEnumerable<T> GetSubtree<T>(T node, Func<T, IEnumerable<T>> getChildren, bool depthFirst) where T : class
	{
		if (node == null)
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
						__ldtoken(TreeHelper.GetSubtree<T>(T, Func<T, IEnumerable<T>>, bool));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5512));
				}
			}
		}
		if (getChildren == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5540));
		}
		return c26ae360ca5cb216754075b3835d54870(node, getChildren, depthFirst);
	}

	private static IEnumerable<c96c92d0bb3e5901860b75aee31593453> c26ae360ca5cb216754075b3835d54870<c96c92d0bb3e5901860b75aee31593453>(c96c92d0bb3e5901860b75aee31593453 c7f587bd57d47897172f47483e6bcca68, Func<c96c92d0bb3e5901860b75aee31593453, IEnumerable<c96c92d0bb3e5901860b75aee31593453>> c453449c4ca0a8d4e6d0d725121da8176, bool c4b5f6b6b3f82eb1c77e59c40ea035d00) where c96c92d0bb3e5901860b75aee31593453 : class
	{
		if (c4b5f6b6b3f82eb1c77e59c40ea035d00)
		{
			Stack<c96c92d0bb3e5901860b75aee31593453> stack = new Stack<c96c92d0bb3e5901860b75aee31593453>();
			stack.Push(c7f587bd57d47897172f47483e6bcca68);
			while (stack.Count > 0)
			{
				c96c92d0bb3e5901860b75aee31593453 val = stack.Pop();
				yield return val;
				IEnumerator<c96c92d0bb3e5901860b75aee31593453> enumerator = c453449c4ca0a8d4e6d0d725121da8176(val).Reverse().GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						c96c92d0bb3e5901860b75aee31593453 current = enumerator.Current;
						if (current == null)
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
						if (1 == 0)
						{
							__ldtoken(cb79402d4f1c60d532adcb0c704ef0115<>.MoveNext());
						}
						stack.Push(current);
					}
					while (true)
					{
						switch (3)
						{
						case 0:
							break;
						default:
							goto end_IL_00da;
						}
						continue;
						end_IL_00da:
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
				switch (6)
				{
				case 0:
					break;
				default:
					yield break;
				}
			}
		}
		Queue<c96c92d0bb3e5901860b75aee31593453> queue = new Queue<c96c92d0bb3e5901860b75aee31593453>();
		queue.Enqueue(c7f587bd57d47897172f47483e6bcca68);
		while (queue.Count > 0)
		{
			c96c92d0bb3e5901860b75aee31593453 val2 = queue.Dequeue();
			yield return val2;
			IEnumerator<c96c92d0bb3e5901860b75aee31593453> enumerator2 = c453449c4ca0a8d4e6d0d725121da8176(val2).GetEnumerator();
			try
			{
				while (enumerator2.MoveNext())
				{
					c96c92d0bb3e5901860b75aee31593453 current2 = enumerator2.Current;
					if (current2 != null)
					{
						queue.Enqueue(current2);
					}
				}
				while (true)
				{
					switch (6)
					{
					case 0:
						break;
					default:
						goto end_IL_01ad;
					}
					continue;
					end_IL_01ad:
					break;
				}
			}
			finally
			{
				if (enumerator2 != null)
				{
					while (true)
					{
						switch (7)
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
	///       Gets the leaves of a given tree.
	///       </summary>
	/// <param name="node">The reference node where to start the search.</param>
	/// <param name="getChildren">
	///   <para>
	///       A method that retrieves the children of an object of type <typeparamref name="T" />.
	///       </para>
	///   <para>
	///     <see cref="M:DigitalRune.Linq.TreeHelper.GetLeaves``1(``0,System.Func{``0,System.Collections.Generic.IEnumerable{``0}})" /> guarantees that 
	///       <paramref name="getChildren" /> is never called with <see langword="null" /> as parameter. The
	///       enumeration returned by <paramref name="getChildren" /> may contain <see langword="null" />.
	///       </para>
	/// </param>
	/// <typeparam name="T">The type of node that is enumerated.</typeparam>
	/// <returns>The leaves of <paramref name="node" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="node" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="getChildren" /> is <see langword="null" />.
	///       </exception>
	public static IEnumerable<T> GetLeaves<T>(T node, Func<T, IEnumerable<T>> getChildren) where T : class
	{
		if (node == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5512));
		}
		if (getChildren == null)
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
						__ldtoken(TreeHelper.GetLeaves<T>(T, Func<T, IEnumerable<T>>));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5540));
				}
			}
		}
		return c6a08348d6bcb74c2a06a4756bdd60c87(node, getChildren);
	}

	private static IEnumerable<c96c92d0bb3e5901860b75aee31593453> c6a08348d6bcb74c2a06a4756bdd60c87<c96c92d0bb3e5901860b75aee31593453>(c96c92d0bb3e5901860b75aee31593453 c7f587bd57d47897172f47483e6bcca68, Func<c96c92d0bb3e5901860b75aee31593453, IEnumerable<c96c92d0bb3e5901860b75aee31593453>> c453449c4ca0a8d4e6d0d725121da8176) where c96c92d0bb3e5901860b75aee31593453 : class
	{
		Stack<c96c92d0bb3e5901860b75aee31593453> stack = new Stack<c96c92d0bb3e5901860b75aee31593453>();
		stack.Push(c7f587bd57d47897172f47483e6bcca68);
		while (stack.Count > 0)
		{
			c96c92d0bb3e5901860b75aee31593453 val = stack.Pop();
			IEnumerable<c96c92d0bb3e5901860b75aee31593453> source = c453449c4ca0a8d4e6d0d725121da8176(val);
			if (!source.Any())
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
					__ldtoken(c05b483297f192a0ec998c973ea73e39f<>.MoveNext());
				}
				yield return val;
				continue;
			}
			IEnumerator<c96c92d0bb3e5901860b75aee31593453> enumerator = source.Reverse().GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					c96c92d0bb3e5901860b75aee31593453 current = enumerator.Current;
					if (current != null)
					{
						stack.Push(current);
					}
				}
				while (true)
				{
					switch (1)
					{
					case 0:
						break;
					default:
						goto end_IL_00e0;
					}
					continue;
					end_IL_00e0:
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
		}
	}

	/// <summary>
	///       Gets the depth of the specified node in a tree.
	///       </summary>
	/// <param name="node">The node.</param>
	/// <param name="getParent">
	///   <para>
	///       A method that retrieves the parent object for a node of type <typeparamref name="T" />.
	///       </para>
	///   <para>
	///       The method should return <see langword="null" /> to indicate that a node does not have a
	///       parent. <see cref="M:DigitalRune.Linq.TreeHelper.GetDepth``1(``0,System.Func{``0,``0})" /> guarantees that <paramref name="getParent" /> is never 
	///       called with <see langword="null" /> as parameter.
	///       </para>
	/// </param>
	/// <typeparam name="T">The type of tree node.</typeparam>
	/// <returns>The depth of the node.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="node" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="getParent" /> is <see langword="null" />.
	///       </exception>
	public static int GetDepth<T>(T node, Func<T, T> getParent) where T : class
	{
		if (node == null)
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
						__ldtoken(TreeHelper.GetDepth<T>(T, Func<T, T>));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5512));
				}
			}
		}
		if (getParent == null)
		{
			while (true)
			{
				switch (2)
				{
				case 0:
					break;
				default:
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5521));
				}
			}
		}
		return cf56e92584a88f42237379ab52eb0d078(node, getParent);
	}

	private static int cf56e92584a88f42237379ab52eb0d078<c96c92d0bb3e5901860b75aee31593453>(c96c92d0bb3e5901860b75aee31593453 c7f587bd57d47897172f47483e6bcca68, Func<c96c92d0bb3e5901860b75aee31593453, c96c92d0bb3e5901860b75aee31593453> c2b37eda0ee059d26493d34fdccbf3acf) where c96c92d0bb3e5901860b75aee31593453 : class
	{
		c96c92d0bb3e5901860b75aee31593453 val = c2b37eda0ee059d26493d34fdccbf3acf(c7f587bd57d47897172f47483e6bcca68);
		if (val == null)
		{
			return 0;
		}
		return 1 + cf56e92584a88f42237379ab52eb0d078(val, c2b37eda0ee059d26493d34fdccbf3acf);
	}

	/// <summary>
	///       Gets the height of the the specified tree or subtree.
	///       </summary>
	/// <param name="tree">The root of a tree or subtree.</param>
	/// <param name="getChildren">
	///   <para>
	///       A method that retrieves the children of an object of type <typeparamref name="T" />. 
	///       </para>
	///   <para>
	///     <see cref="M:DigitalRune.Linq.TreeHelper.GetHeight``1(``0,System.Func{``0,System.Collections.Generic.IEnumerable{``0}})" /> guarantees that <paramref name="getChildren" /> is never called 
	///       with <see langword="null" /> as parameter. The enumeration returned by 
	///       <paramref name="getChildren" /> may contain <see langword="null" />.
	///       </para>
	/// </param>
	/// <typeparam name="T">The type of tree node.</typeparam>
	/// <returns>The height of the tree or subtree.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="tree" /> is <see langword="null" />.
	///       </exception>
	public static int GetHeight<T>(T tree, Func<T, IEnumerable<T>> getChildren) where T : class
	{
		if (tree == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5563));
		}
		if (getChildren == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5540));
		}
		return ceac5f90225dfea0377114bbb6f0011ee(tree, getChildren);
	}

	private static int ceac5f90225dfea0377114bbb6f0011ee<c96c92d0bb3e5901860b75aee31593453>(c96c92d0bb3e5901860b75aee31593453 cf003258544d2ad561927a565f5a6bb47, Func<c96c92d0bb3e5901860b75aee31593453, IEnumerable<c96c92d0bb3e5901860b75aee31593453>> c453449c4ca0a8d4e6d0d725121da8176) where c96c92d0bb3e5901860b75aee31593453 : class
	{
		IEnumerable<c96c92d0bb3e5901860b75aee31593453> enumerable = c453449c4ca0a8d4e6d0d725121da8176(cf003258544d2ad561927a565f5a6bb47);
		if (!enumerable.Any())
		{
			return 0;
		}
		int num = 0;
		using (IEnumerator<c96c92d0bb3e5901860b75aee31593453> enumerator = enumerable.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				c96c92d0bb3e5901860b75aee31593453 current = enumerator.Current;
				int num2 = ceac5f90225dfea0377114bbb6f0011ee(current, c453449c4ca0a8d4e6d0d725121da8176);
				if (num2 > num)
				{
					num = num2;
				}
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
					__ldtoken(TreeHelper.ceac5f90225dfea0377114bbb6f0011ee<c96c92d0bb3e5901860b75aee31593453>(c96c92d0bb3e5901860b75aee31593453, Func<c96c92d0bb3e5901860b75aee31593453, IEnumerable<c96c92d0bb3e5901860b75aee31593453>>));
				}
				break;
			}
		}
		return 1 + num;
	}
}
