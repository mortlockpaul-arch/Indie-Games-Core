using System;
using T;

namespace _u0014_;

internal sealed class v : _u0013_
{
	private const int l8a = 1;

	private bool l8_u0013_ = true;

	private bool l8N;

	private T.N l8l;

	public override string KeyExchangeAlgorithm => "RSA-PKCS1-KeyEx";

	public override int KeySize
	{
		get
		{
			if (l8l == null)
			{
				return KeySizeValue;
			}
			return l8l.KeySize;
		}
	}

	public bool PersistKeyInCsp
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	internal bool PublicOnly => l8l.PublicOnly;

	public override string SignatureAlgorithm => "http://www.w3.org/2000/09/xmldsig#rsa-sha1";

	public v()
	{
		LB(1024);
	}

	private void LB(int param_0)
	{
		LegalKeySizesValue = new s[1];
		LegalKeySizesValue[0] = new s(384, 16384, 8);
		base.KeySize = param_0;
		l8l = new T.N(KeySize);
	}

	~v()
	{
		Dispose(disposing: false);
	}

	public byte[] Decrypt(byte[] rgb, bool fOAEP)
	{
		N n = null;
		n = ((!fOAEP) ? ((N)new b(l8l)) : ((N)new Y(l8l)));
		return n.DecryptKeyExchange(rgb);
	}

	public override byte[] DecryptValue(byte[] rgb)
	{
		if (!l8l.IsCrtPossible)
		{
			throw new L("Incomplete private key - missing CRT.");
		}
		return l8l.DecryptValue(rgb);
	}

	public override byte[] EncryptValue(byte[] rgb)
	{
		return l8l.EncryptValue(rgb);
	}

	public override _u0005_ ExportParameters(bool includePrivateParameters)
	{
		if (includePrivateParameters && !l8_u0013_)
		{
			throw new L("cannot export private key");
		}
		return l8l.ExportParameters(includePrivateParameters);
	}

	public override void ImportParameters(_u0005_ parameters)
	{
		l8l.ImportParameters(parameters);
	}

	private B lY(object param_0)
	{
		if (param_0 == null)
		{
			throw new ArgumentNullException("halg");
		}
		B b2 = null;
		if (param_0 is string)
		{
			return new W();
		}
		if (param_0 is B)
		{
			return (B)param_0;
		}
		if (param_0 is Type)
		{
			return (B)Activator.CreateInstance((Type)param_0);
		}
		throw new ArgumentException("halg");
	}

	public byte[] SignHash(byte[] rgbHash, string str)
	{
		if (rgbHash == null)
		{
			throw new ArgumentNullException("rgbHash");
		}
		B hash = new W();
		return T._u0013_.Sign_v15(this, hash, rgbHash);
	}

	public bool VerifyData(byte[] buffer, object halg, byte[] signature)
	{
		if (signature == null)
		{
			throw new ArgumentNullException("signature");
		}
		B b2 = lY(halg);
		byte[] hashValue = b2.ComputeHash(buffer);
		return T._u0013_.Verify_v15(this, b2, hashValue, signature);
	}

	public bool VerifyHash(byte[] rgbHash, string str, byte[] rgbSignature)
	{
		if (rgbHash == null)
		{
			throw new ArgumentNullException("rgbHash");
		}
		if (rgbSignature == null)
		{
			throw new ArgumentNullException("rgbSignature");
		}
		B hash = new W();
		return T._u0013_.Verify_v15(this, hash, rgbHash, rgbSignature);
	}

	protected override void Dispose(bool disposing)
	{
		if (!l8N)
		{
			if (l8l != null)
			{
				l8l.Clear();
			}
			l8N = true;
		}
	}

	private void Lh(object param_0, EventArgs param_1)
	{
	}

	public void ImportCspBlob(byte[] rawData)
	{
		if (rawData == null)
		{
			throw new ArgumentNullException("rawData");
		}
		_u0013_ u0013_ = T.a.FromCapiKeyBlob(rawData);
		if (u0013_ is v)
		{
			_u0005_ parameters = u0013_.ExportParameters(!(u0013_ as v).PublicOnly);
			ImportParameters(parameters);
			return;
		}
		try
		{
			_u0005_ parameters2 = u0013_.ExportParameters(include: true);
			ImportParameters(parameters2);
		}
		catch
		{
			_u0005_ parameters3 = u0013_.ExportParameters(include: false);
			ImportParameters(parameters3);
		}
	}
}
