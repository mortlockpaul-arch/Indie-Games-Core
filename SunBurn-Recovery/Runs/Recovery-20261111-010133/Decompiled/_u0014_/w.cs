using System;

namespace _u0014_;

internal class w : L
{
	public w()
		: base("Unexpected error occured during a cryptographic operation.")
	{
		HResult = -2146233295;
	}

	public w(string message)
		: base(message)
	{
		HResult = -2146233295;
	}

	public w(string message, Exception inner)
		: base(message, inner)
	{
		HResult = -2146233295;
	}

	public w(string format, string insert)
		: base(string.Format(format, insert))
	{
		HResult = -2146233295;
	}
}
internal sealed class W : I
{
	private _id_5 l8a;

	public W()
	{
		l8a = new _id_5();
	}

	~W()
	{
		Dispose(disposing: false);
	}

	protected override void Dispose(bool disposing)
	{
		base.Dispose(disposing);
	}

	protected override void HashCore(byte[] rgb, int start, int size)
	{
		State = 1;
		l8a.HashCore(rgb, start, size);
	}

	protected override byte[] HashFinal()
	{
		State = 0;
		return l8a.HashFinal();
	}

	public override void Initialize()
	{
		l8a.Initialize();
	}
}
