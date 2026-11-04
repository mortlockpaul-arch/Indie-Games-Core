namespace _0011;

internal sealed class d : o
{
	private c fNB;

	public d()
	{
		fNB = new c();
	}

	~d()
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
