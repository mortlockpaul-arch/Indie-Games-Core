using System;
using _0011;
using w;

namespace s;

internal class g : _0011.a
{
	public delegate void nNB(object sender, EventArgs e);

	private const int fNB = 1024;

	private bool fNa;

	private bool fNg = true;

	private bool fNf;

	private bool fN_0003;

	private w.B fNn;

	private w.B fN_0019;

	private w.B fNL;

	private w.B fNv;

	private w.B fN_0012;

	private w.B fN_0015;

	private w.B fN8;

	private w.B fNi;

	public override int KeySize
	{
		get
		{
			if (fNf)
			{
				int num = fN8.BitCount();
				if ((num & 7) != 0)
				{
					num += 8 - (num & 7);
				}
				return num;
			}
			return base.KeySize;
		}
	}

	public override string KeyExchangeAlgorithm => "RSA-PKCS1-KeyEx";

	public bool PublicOnly
	{
		get
		{
			if (fNf)
			{
				if (!(fNn == null))
				{
					return fN8 == null;
				}
				return true;
			}
			return false;
		}
	}

	public override string SignatureAlgorithm => "http://www.w3.org/2000/09/xmldsig#rsa-sha1";

	internal bool UseKeyBlinding
	{
		get
		{
			return fNg;
		}
		set
		{
			fNg = value;
		}
	}

	internal bool IsCrtPossible
	{
		get
		{
			if (fNf)
			{
				return fNa;
			}
			return true;
		}
	}

	public g()
		: this(1024)
	{
	}

	public g(int keySize)
	{
		LegalKeySizesValue = new _0011.v[1];
		LegalKeySizesValue[0] = new _0011.v(384, 16384, 8);
		base.KeySize = keySize;
	}

	~g()
	{
		Dispose(disposing: false);
	}

	public override byte[] DecryptValue(byte[] rgb)
	{
		if (fN_0003)
		{
			throw new ObjectDisposedException("private key");
		}
		if (!fNf)
		{
			throw new Exception("not supported");
		}
		w.B b = new w.B(rgb);
		w.B b2 = null;
		if (fNg)
		{
			b2 = w.B.GenerateRandom(fN8.BitCount());
			b = b2.ModPow(fNi, fN8) * b % fN8;
		}
		w.B b6;
		if (fNa)
		{
			w.B b3 = b.ModPow(fNv, fN_0019);
			w.B b4 = b.ModPow(fN_0012, fNL);
			if (b4 > b3)
			{
				w.B b5 = fN_0019 - (b4 - b3) * fN_0015 % fN_0019;
				b6 = b4 + fNL * b5;
			}
			else
			{
				w.B b5 = (b3 - b4) * fN_0015 % fN_0019;
				b6 = b4 + fNL * b5;
			}
		}
		else
		{
			if (PublicOnly)
			{
				throw new _0011._0003("Missing private key to decrypt value.");
			}
			b6 = b.ModPow(fNn, fN8);
		}
		if (fNg)
		{
			b6 = b6 * b2.ModInverse(fN8) % fN8;
			b2.Clear();
		}
		byte[] result = _0003j(b6, KeySize >> 3);
		b.Clear();
		b6.Clear();
		return result;
	}

	public override byte[] EncryptValue(byte[] rgb)
	{
		if (fN_0003)
		{
			throw new ObjectDisposedException("public key");
		}
		if (!fNf)
		{
			throw new Exception("not supported");
		}
		w.B b = new w.B(rgb);
		w.B b2 = b.ModPow(fNi, fN8);
		byte[] result = _0003j(b2, KeySize >> 3);
		b.Clear();
		b2.Clear();
		return result;
	}

	public override _0011._0005 ExportParameters(bool includePrivateParameters)
	{
		if (fN_0003)
		{
			throw new ObjectDisposedException("");
		}
		if (!fNf)
		{
			throw new Exception("not supported");
		}
		_0011._0005 result = new _0011._0005
		{
			Exponent = fNi.GetBytes(),
			Modulus = fN8.GetBytes()
		};
		if (includePrivateParameters)
		{
			if (fNn == null)
			{
				throw new _0011._0003("Missing private key");
			}
			result.D = fNn.GetBytes();
			if (result.D.Length != result.Modulus.Length)
			{
				byte[] array = new byte[result.Modulus.Length];
				Buffer.BlockCopy(result.D, 0, array, array.Length - result.D.Length, result.D.Length);
				result.D = array;
			}
			if (fN_0019 != null && fNL != null && fNv != null && fN_0012 != null && fN_0015 != null)
			{
				int num = KeySize >> 4;
				result.P = _0003j(fN_0019, num);
				result.Q = _0003j(fNL, num);
				result.DP = _0003j(fNv, num);
				result.DQ = _0003j(fN_0012, num);
				result.InverseQ = _0003j(fN_0015, num);
			}
		}
		return result;
	}

	public override void ImportParameters(_0011._0005 parameters)
	{
		if (fN_0003)
		{
			throw new ObjectDisposedException("");
		}
		if (parameters.Exponent == null)
		{
			throw new _0011._0003("Missing Exponent");
		}
		if (parameters.Modulus == null)
		{
			throw new _0011._0003("Missing Modulus");
		}
		fNi = new w.B(parameters.Exponent);
		fN8 = new w.B(parameters.Modulus);
		if (parameters.D != null)
		{
			fNn = new w.B(parameters.D);
		}
		if (parameters.DP != null)
		{
			fNv = new w.B(parameters.DP);
		}
		if (parameters.DQ != null)
		{
			fN_0012 = new w.B(parameters.DQ);
		}
		if (parameters.InverseQ != null)
		{
			fN_0015 = new w.B(parameters.InverseQ);
		}
		if (parameters.P != null)
		{
			fN_0019 = new w.B(parameters.P);
		}
		if (parameters.Q != null)
		{
			fNL = new w.B(parameters.Q);
		}
		fNf = true;
		fNa = fN_0019 != null && fNL != null && fNv != null && fN_0012 != null && fN_0015 != null;
	}

	protected override void Dispose(bool disposing)
	{
		if (!fN_0003)
		{
			if (fNn != null)
			{
				fNn.Clear();
				fNn = null;
			}
			if (fN_0019 != null)
			{
				fN_0019.Clear();
				fN_0019 = null;
			}
			if (fNL != null)
			{
				fNL.Clear();
				fNL = null;
			}
			if (fNv != null)
			{
				fNv.Clear();
				fNv = null;
			}
			if (fN_0012 != null)
			{
				fN_0012.Clear();
				fN_0012 = null;
			}
			if (fN_0015 != null)
			{
				fN_0015.Clear();
				fN_0015 = null;
			}
			if (disposing)
			{
				if (fNi != null)
				{
					fNi.Clear();
					fNi = null;
				}
				if (fN8 != null)
				{
					fN8.Clear();
					fN8 = null;
				}
			}
		}
		fN_0003 = true;
	}

	private byte[] _0003j(w.B P_0, int P_1)
	{
		byte[] bytes = P_0.GetBytes();
		if (bytes.Length >= P_1)
		{
			return bytes;
		}
		byte[] array = new byte[P_1];
		Buffer.BlockCopy(bytes, 0, array, P_1 - bytes.Length, bytes.Length);
		Array.Clear(bytes, 0, bytes.Length);
		return array;
	}
}
