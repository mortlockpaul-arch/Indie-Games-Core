namespace _u0014_;

internal abstract class N
{
	public abstract string Parameters { get; set; }

	public N()
	{
	}

	public abstract byte[] DecryptKeyExchange(byte[] rgb);

	public abstract void SetKey(a key);
}
