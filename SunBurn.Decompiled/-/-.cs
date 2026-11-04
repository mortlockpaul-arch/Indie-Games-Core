using System;
using System.Text;

namespace _0019
{
	internal class _0003 : B
	{
	}
}
namespace _0011
{
	internal class _0003 : SystemException
	{
		public _0003()
			: base("Error occured during a cryptographic operation.")
		{
			HResult = -2146233296;
		}

		public _0003(int hr)
		{
			HResult = hr;
		}

		public _0003(string message)
			: base(message)
		{
			HResult = -2146233296;
		}

		public _0003(string message, Exception inner)
			: base(message, inner)
		{
			HResult = -2146233296;
		}

		public _0003(string format, string insert)
			: base(string.Format(format, insert))
		{
			HResult = -2146233296;
		}
	}
	internal interface _0019 : IDisposable
	{
		bool CanReuseTransform { get; }

		bool CanTransformMultipleBlocks { get; }

		int InputBlockSize { get; }

		int OutputBlockSize { get; }

		int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset);

		byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount);
	}
	internal abstract class _0012
	{
		public _0012()
		{
		}

		public static _0012 Create()
		{
			return new _0015();
		}

		public abstract void GetBytes(byte[] data);

		public abstract void GetNonZeroBytes(byte[] data);
	}
	internal sealed class _0015 : _0012
	{
		private static object fNB;

		private IntPtr fNa;

		private static Random fNg;

		static _0015()
		{
			fNg = new Random();
			if (_0003_0013())
			{
				fNB = new object();
			}
		}

		public _0015()
		{
			fNa = _0003q(null);
			_00039();
		}

		public _0015(byte[] rgb)
		{
			fNa = _0003q(rgb);
			_00039();
		}

		public _0015(string str)
		{
			if (str == null)
			{
				fNa = _0003q(null);
			}
			else
			{
				fNa = _0003q(Encoding.UTF8.GetBytes(str));
			}
			_00039();
		}

		private void _00039()
		{
			if (fNa == IntPtr.Zero)
			{
				throw new _0003("Couldn't access random source.");
			}
		}

		private static bool _0003_0013()
		{
			return true;
		}

		private static IntPtr _0003q(byte[] P_0)
		{
			if (P_0 == null || P_0.Length < 1)
			{
				return new IntPtr(1);
			}
			fNg = new Random(P_0[0]);
			return new IntPtr(1);
		}

		private static IntPtr _0003_000F(IntPtr P_0, byte[] P_1)
		{
			fNg.NextBytes(P_1);
			return new IntPtr(1);
		}

		private static void _0003_0014(IntPtr P_0)
		{
		}

		public override void GetBytes(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (fNB == null)
			{
				fNa = _0003_000F(fNa, data);
			}
			else
			{
				lock (fNB)
				{
					fNa = _0003_000F(fNa, data);
				}
			}
			_00039();
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
				fNa = _0003_000F(fNa, array);
				_00039();
				for (int k = 0; k < array.Length; k++)
				{
					if (num == data.Length)
					{
						break;
					}
					if (array[k] != 0)
					{
						data[num++] = array[k];
					}
				}
			}
		}

		~_0015()
		{
			if (fNa != IntPtr.Zero)
			{
				_0003_0014(fNa);
				fNa = IntPtr.Zero;
			}
		}
	}
	internal struct _0005
	{
		public byte[] P;

		public byte[] Q;

		public byte[] D;

		public byte[] DP;

		public byte[] DQ;

		public byte[] InverseQ;

		public byte[] Modulus;

		public byte[] Exponent;
	}
}
