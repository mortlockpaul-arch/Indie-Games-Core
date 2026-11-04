using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace A;

internal struct c7180016b8fd0464565a072d2ef58eb1d
{
	public readonly WeakReference c4bbdd2ec94609ed106e71dd6f5679d67;

	public readonly MethodInfo c398ca7cf438dac411558bed96056a177;

	public readonly Type cae41735bf8b6e5574819efa591cd7d8d;

	public Delegate c200fbad0dca0e5295345b463dafbe426
	{
		get
		{
			if (c4bbdd2ec94609ed106e71dd6f5679d67 == null)
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
							__ldtoken(c7180016b8fd0464565a072d2ef58eb1d.get_c200fbad0dca0e5295345b463dafbe426());
						}
						return Delegate.CreateDelegate(cae41735bf8b6e5574819efa591cd7d8d, null, c398ca7cf438dac411558bed96056a177);
					}
				}
			}
			object target = c4bbdd2ec94609ed106e71dd6f5679d67.Target;
			if (target != null)
			{
				return Delegate.CreateDelegate(cae41735bf8b6e5574819efa591cd7d8d, target, c398ca7cf438dac411558bed96056a177);
			}
			return null;
		}
	}

	public bool ceb345e599e75ceeff1e14b0ac2cf0104
	{
		get
		{
			if (c4bbdd2ec94609ed106e71dd6f5679d67 != null)
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
							__ldtoken(c7180016b8fd0464565a072d2ef58eb1d.get_ceb345e599e75ceeff1e14b0ac2cf0104());
						}
						return c4bbdd2ec94609ed106e71dd6f5679d67.IsAlive;
					}
				}
			}
			return true;
		}
	}

	public c7180016b8fd0464565a072d2ef58eb1d(Delegate c8336ff8fb800aa64a24351b3d4592692)
	{
		if ((object)c8336ff8fb800aa64a24351b3d4592692 == null)
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
						__ldtoken(c7180016b8fd0464565a072d2ef58eb1d._002Ector(Delegate));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1));
				}
			}
		}
		c4bbdd2ec94609ed106e71dd6f5679d67 = ((c8336ff8fb800aa64a24351b3d4592692.Target != null) ? new WeakReference(c8336ff8fb800aa64a24351b3d4592692.Target) : null);
		c398ca7cf438dac411558bed96056a177 = c8336ff8fb800aa64a24351b3d4592692.Method;
		cae41735bf8b6e5574819efa591cd7d8d = c8336ff8fb800aa64a24351b3d4592692.GetType();
	}

	public void c0a27cdd8cef5186286b408128a586c9d(params object[] ccd344504be4b7d5035089f53b6ec299f)
	{
		if (c4bbdd2ec94609ed106e71dd6f5679d67 != null)
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
						__ldtoken(c7180016b8fd0464565a072d2ef58eb1d.c0a27cdd8cef5186286b408128a586c9d(object[]));
					}
					object target = c4bbdd2ec94609ed106e71dd6f5679d67.Target;
					if (target != null)
					{
						c398ca7cf438dac411558bed96056a177.Invoke(target, ccd344504be4b7d5035089f53b6ec299f);
					}
					return;
				}
				}
			}
		}
		c398ca7cf438dac411558bed96056a177.Invoke(null, ccd344504be4b7d5035089f53b6ec299f);
	}

	[Conditional("DEBUG")]
	private static void c51b45adf8df023aab07beaa494f56e16(MethodInfo cff63d05ca0a7499299825c200dda9553)
	{
		_ = cff63d05ca0a7499299825c200dda9553.DeclaringType.GetCustomAttributes(typeof(CompilerGeneratedAttribute), inherit: false).Length;
	}
}
