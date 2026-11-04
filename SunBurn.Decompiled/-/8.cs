using System;
using s;

namespace _0011;

internal sealed class _8 : a
{
	private const int fNB = 1;

	private bool fNa = true;

	private bool fNg;

	private s.g fNf;

	public override string KeyExchangeAlgorithm => "RSA-PKCS1-KeyEx";

	public override int KeySize
	{
		get
		{
			if (fNf == null)
			{
				return KeySizeValue;
			}
			return fNf.KeySize;
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

	internal bool PublicOnly => fNf.PublicOnly;

	public override string SignatureAlgorithm => "http://www.w3.org/2000/09/xmldsig#rsa-sha1";

	public _8()
	{
		_0003r(1024);
	}

	private void _0003r(int P_0)
	{
		LegalKeySizesValue = new v[1];
		LegalKeySizesValue[0] = new v(384, 16384, 8);
		base.KeySize = P_0;
		fNf = new s.g(KeySize);
	}

	~_8()
	{
		Dispose(disposing: false);
	}

	public byte[] Decrypt(byte[] rgb, bool fOAEP)
	{
		g g2 = null;
		g2 = ((!fOAEP) ? ((g)new j(fNf)) : ((g)new i(fNf)));
		return g2.DecryptKeyExchange(rgb);
	}

	public override byte[] DecryptValue(byte[] rgb)
	{
		if (!fNf.IsCrtPossible)
		{
			throw new _0003("Incomplete private key - missing CRT.");
		}
		return fNf.DecryptValue(rgb);
	}

	public override byte[] EncryptValue(byte[] rgb)
	{
		return fNf.EncryptValue(rgb);
	}

	public override _0005 ExportParameters(bool includePrivateParameters)
	{
		if (includePrivateParameters && !fNa)
		{
			throw new _0003("cannot export private key");
		}
		return fNf.ExportParameters(includePrivateParameters);
	}

	public override void ImportParameters(_0005 parameters)
	{
		fNf.ImportParameters(parameters);
	}

	private L f_0016(object P_0)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("halg");
		}
		L l = null;
		if (P_0 is string)
		{
			return new d();
		}
		if (P_0 is L)
		{
			return (L)P_0;
		}
		if (P_0 is Type)
		{
			return (L)Activator.CreateInstance((Type)P_0);
		}
		throw new ArgumentException("halg");
	}

	public byte[] SignHash(byte[] rgbHash, string str)
	{
		if (rgbHash == null)
		{
			throw new ArgumentNullException("rgbHash");
		}
		L hash = new d();
		return s.a.Sign_v15(this, hash, rgbHash);
	}

	public bool VerifyData(byte[] buffer, object halg, byte[] signature)
	{
		if (signature == null)
		{
			throw new ArgumentNullException("signature");
		}
		L l = f_0016(halg);
		byte[] hashValue = l.ComputeHash(buffer);
		return s.a.Verify_v15(this, l, hashValue, signature);
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
		L hash = new d();
		return s.a.Verify_v15(this, hash, rgbHash, rgbSignature);
	}

	protected override void Dispose(bool disposing)
	{
		if (!fNg)
		{
			if (fNf != null)
			{
				fNf.Clear();
			}
			fNg = true;
		}
	}

	private void _0003u(object P_0, EventArgs P_1)
	{
	}

	public void ImportCspBlob(byte[] rawData)
	{
		if (rawData == null)
		{
			throw new ArgumentNullException("rawData");
		}
		a a2 = s.B.FromCapiKeyBlob(rawData);
		if (a2 is _8)
		{
			_0005 parameters = a2.ExportParameters(!(a2 as _8).PublicOnly);
			ImportParameters(parameters);
			return;
		}
		try
		{
			_0005 parameters2 = a2.ExportParameters(include: true);
			ImportParameters(parameters2);
		}
		catch
		{
			_0005 parameters3 = a2.ExportParameters(include: false);
			ImportParameters(parameters3);
		}
	}
}
