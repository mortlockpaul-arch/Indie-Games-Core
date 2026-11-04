using s;

namespace _0011;

internal class j : g
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

	public j()
	{
		fNB = null;
	}

	public j(B key)
	{
		SetKey(key);
	}

	public override byte[] DecryptKeyExchange(byte[] rgbData)
	{
		if (fNB == null)
		{
			throw new n("No key pair available.");
		}
		byte[] array = s.a.Decrypt_v15(fNB, rgbData);
		if (array != null)
		{
			return array;
		}
		throw new _0003("PKCS1 decoding error.");
	}

	public override void SetKey(B key)
	{
		fNB = (a)key;
	}
}
