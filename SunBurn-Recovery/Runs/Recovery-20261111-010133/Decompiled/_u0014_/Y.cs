using T;

namespace _u0014_;

internal class Y : N
{
	private _u0013_ l8a;

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

	public Y()
	{
		l8a = null;
	}

	public Y(a key)
	{
		SetKey(key);
	}

	public override byte[] DecryptKeyExchange(byte[] rgbData)
	{
		I hash = I.Create();
		byte[] array = T._u0013_.Decrypt_OAEP(l8a, hash, rgbData);
		if (array != null)
		{
			return array;
		}
		throw new L("OAEP decoding error.");
	}

	public override void SetKey(a key)
	{
		l8a = (_u0013_)key;
	}
}
