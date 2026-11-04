namespace _0011;

internal class w : o
{
	private c fNB;

	public w()
	{
		fNB = new c();
	}

	protected override void HashCore(byte[] rgb, int start, int size)
	{
		State = 1;
		fNB.HashCore(rgb, start, size);
	}

	protected override byte[] HashFinal()
	{
		State = 0;
		return fNB.HashFinal();
	}

	public override void Initialize()
	{
		fNB.Initialize();
	}
}
