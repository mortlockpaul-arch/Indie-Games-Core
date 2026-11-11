using System;

namespace _u0014_;

internal abstract class a : IDisposable
{
	protected int KeySizeValue;

	protected s[] LegalKeySizesValue;

	public abstract string KeyExchangeAlgorithm { get; }

	public virtual int KeySize
	{
		get
		{
			return KeySizeValue;
		}
		set
		{
			if (!s.L_u0013_(LegalKeySizesValue, value))
			{
				throw new L("Key size not supported by algorithm.");
			}
			KeySizeValue = value;
		}
	}

	public virtual s[] LegalKeySizes => LegalKeySizesValue;

	public abstract string SignatureAlgorithm { get; }

	void IDisposable.Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}

	public void Clear()
	{
		Dispose(disposing: false);
	}

	protected abstract void Dispose(bool disposing);

	public static a Create()
	{
		return new v();
	}
}
