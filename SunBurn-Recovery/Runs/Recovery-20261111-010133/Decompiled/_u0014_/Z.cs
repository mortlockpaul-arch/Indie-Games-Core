namespace _u0014_;

internal class Z : I
{
	private _id_5 l8a;

	public Z()
	{
		l8a = new _id_5();
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
