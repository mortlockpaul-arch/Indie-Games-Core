using System;
using System.Text;

namespace _u0014_;

internal sealed class _u0002_ : h
{
	private static object l8a;

	private IntPtr l8_u0013_;

	private static Random l8N;

	static _u0002_()
	{
		l8N = new Random();
		if (Ll())
		{
			l8a = new object();
		}
	}

	public _u0002_()
	{
		l8_u0013_ = LL(null);
		LN();
	}

	public _u0002_(byte[] rgb)
	{
		l8_u0013_ = LL(rgb);
		LN();
	}

	public _u0002_(string str)
	{
		if (str == null)
		{
			l8_u0013_ = LL(null);
		}
		else
		{
			l8_u0013_ = LL(Encoding.UTF8.GetBytes(str));
		}
		LN();
	}

	private void LN()
	{
		if (l8_u0013_ == IntPtr.Zero)
		{
			throw new L("Couldn't access random source.");
		}
	}

	private static bool Ll()
	{
		return true;
	}

	private static IntPtr LL(byte[] param_0)
	{
		if (param_0 == null || param_0.Length < 1)
		{
			return new IntPtr(1);
		}
		l8N = new Random(param_0[0]);
		return new IntPtr(1);
	}

	private static IntPtr Lw(IntPtr param_0, byte[] param_1)
	{
		l8N.NextBytes(param_1);
		return new IntPtr(1);
	}

	private static void LJ(IntPtr param_0)
	{
	}

	public override void GetBytes(byte[] data)
	{
		if (data == null)
		{
			throw new ArgumentNullException("data");
		}
		if (l8a == null)
		{
			l8_u0013_ = Lw(l8_u0013_, data);
		}
		else
		{
			lock (l8a)
			{
				l8_u0013_ = Lw(l8_u0013_, data);
			}
		}
		LN();
	}

	public override void GetNonZeroBytes(byte[] data)
	{
		if (data == null)
		{
			throw new ArgumentNullException("data");
		}
		byte[] array = new byte[data.Length * 2];
		int num = 0;
		while (num < data.Length)
		{
			l8_u0013_ = Lw(l8_u0013_, array);
			LN();
			for (int i = 0; i < array.Length; i++)
			{
				if (num == data.Length)
				{
					break;
				}
				if (array[i] != 0)
				{
					data[num++] = array[i];
				}
			}
		}
	}

	~_u0002_()
	{
		if (l8_u0013_ != IntPtr.Zero)
		{
			LJ(l8_u0013_);
			l8_u0013_ = IntPtr.Zero;
		}
	}
}
