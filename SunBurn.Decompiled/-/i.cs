using s;

namespace _0011;

internal class i : g
{
	private a fNB;

	public override string Parameters
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public i()
	{
		fNB = null;
	}

	public i(B key)
	{
		SetKey(key);
	}

	public override byte[] DecryptKeyExchange(byte[] rgbData)
	{
		o hash = o.Create();
		byte[] array = s.a.Decrypt_OAEP(fNB, hash, rgbData);
		if (array != null)
		{
			return array;
		}
		throw new _0003("OAEP decoding error.");
	}

	public override void SetKey(B key)
	{
		fNB = (a)key;
	}
}
