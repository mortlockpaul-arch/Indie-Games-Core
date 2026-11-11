using System;
using Z;
using _u0014_;

namespace T;

internal class N : _u0014_._u0013_
{
	public delegate void w8a(object sender, EventArgs e);

	private const int l8a = 1024;

	private bool l8_u0013_;

	private bool l8N = true;

	private bool l8l;

	private bool l8L;

	private Z.a l8w;

	private Z.a l8J;

	private Z.a l8B;

	private Z.a l8s;

	private Z.a l8h;

	private Z.a l8_u0002_;

	private Z.a l8v;

	private Z.a l8Y;

	public override int KeySize
	{
		get
		{
			if (l8l)
			{
				int num = l8v.BitCount();
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
			if (l8l)
			{
				if (!(l8w == null))
				{
					return l8v == null;
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
			return l8N;
		}
		set
		{
			l8N = value;
		}
	}

	internal bool IsCrtPossible
	{
		get
		{
			if (l8l)
			{
				return l8_u0013_;
			}
			return true;
		}
	}

	public N()
		: this(1024)
	{
	}

	public N(int keySize)
	{
		LegalKeySizesValue = new _u0014_.s[1];
		LegalKeySizesValue[0] = new _u0014_.s(384, 16384, 8);
		base.KeySize = keySize;
	}

	~N()
	{
		Dispose(disposing: false);
	}

	public override byte[] DecryptValue(byte[] rgb)
	{
		if (l8L)
		{
			throw new ObjectDisposedException("private key");
		}
		if (!l8l)
		{
			throw new Exception("not supported");
		}
		Z.a a2 = new Z.a(rgb);
		Z.a a3 = null;
		if (l8N)
		{
			a3 = Z.a.GenerateRandom(l8v.BitCount());
			a2 = a3.ModPow(l8Y, l8v) * a2 % l8v;
		}
		Z.a a7;
		if (l8_u0013_)
		{
			Z.a a4 = a2.ModPow(l8s, l8J);
			Z.a a5 = a2.ModPow(l8h, l8B);
			if (a5 > a4)
			{
				Z.a a6 = l8J - (a5 - a4) * l8_u0002_ % l8J;
				a7 = a5 + l8B * a6;
			}
			else
			{
				Z.a a6 = (a4 - a5) * l8_u0002_ % l8J;
				a7 = a5 + l8B * a6;
			}
		}
		else
		{
			if (PublicOnly)
			{
				throw new _u0014_.L("Missing private key to decrypt value.");
			}
			a7 = a2.ModPow(l8w, l8v);
		}
		if (l8N)
		{
			a7 = a7 * a3.ModInverse(l8v) % l8v;
			a3.Clear();
		}
		byte[] result = lc(a7, KeySize >> 3);
		a2.Clear();
		a7.Clear();
		return result;
	}

	public override byte[] EncryptValue(byte[] rgb)
	{
		if (l8L)
		{
			throw new ObjectDisposedException("public key");
		}
		if (!l8l)
		{
			throw new Exception("not supported");
		}
		Z.a a2 = new Z.a(rgb);
		Z.a a3 = a2.ModPow(l8Y, l8v);
		byte[] result = lc(a3, KeySize >> 3);
		a2.Clear();
		a3.Clear();
		return result;
	}

	public override _u0014_._u0005_ ExportParameters(bool includePrivateParameters)
	{
		if (l8L)
		{
			throw new ObjectDisposedException("");
		}
		if (!l8l)
		{
			throw new Exception("not supported");
		}
		_u0014_._u0005_ result = new _u0014_._u0005_
		{
			Exponent = l8Y.GetBytes(),
			Modulus = l8v.GetBytes()
		};
		if (includePrivateParameters)
		{
			if (l8w == null)
			{
				throw new _u0014_.L("Missing private key");
			}
			result.D = l8w.GetBytes();
			if (result.D.Length != result.Modulus.Length)
			{
				byte[] array = new byte[result.Modulus.Length];
				Buffer.BlockCopy(result.D, 0, array, array.Length - result.D.Length, result.D.Length);
				result.D = array;
			}
			if (l8J != null && l8B != null && l8s != null && l8h != null && l8_u0002_ != null)
			{
				int param_ = KeySize >> 4;
				result.P = lc(l8J, param_);
				result.Q = lc(l8B, param_);
				result.DP = lc(l8s, param_);
				result.DQ = lc(l8h, param_);
				result.InverseQ = lc(l8_u0002_, param_);
			}
		}
		return result;
	}

	public override void ImportParameters(_u0014_._u0005_ parameters)
	{
		if (l8L)
		{
			throw new ObjectDisposedException("");
		}
		if (parameters.Exponent == null)
		{
			throw new _u0014_.L("Missing Exponent");
		}
		if (parameters.Modulus == null)
		{
			throw new _u0014_.L("Missing Modulus");
		}
		l8Y = new Z.a(parameters.Exponent);
		l8v = new Z.a(parameters.Modulus);
		if (parameters.D != null)
		{
			l8w = new Z.a(parameters.D);
		}
		if (parameters.DP != null)
		{
			l8s = new Z.a(parameters.DP);
		}
		if (parameters.DQ != null)
		{
			l8h = new Z.a(parameters.DQ);
		}
		if (parameters.InverseQ != null)
		{
			l8_u0002_ = new Z.a(parameters.InverseQ);
		}
		if (parameters.P != null)
		{
			l8J = new Z.a(parameters.P);
		}
		if (parameters.Q != null)
		{
			l8B = new Z.a(parameters.Q);
		}
		l8l = true;
		l8_u0013_ = l8J != null && l8B != null && l8s != null && l8h != null && l8_u0002_ != null;
	}

	protected override void Dispose(bool disposing)
	{
		if (!l8L)
		{
			if (l8w != null)
			{
				l8w.Clear();
				l8w = null;
			}
			if (l8J != null)
			{
				l8J.Clear();
				l8J = null;
			}
			if (l8B != null)
			{
				l8B.Clear();
				l8B = null;
			}
			if (l8s != null)
			{
				l8s.Clear();
				l8s = null;
			}
			if (l8h != null)
			{
				l8h.Clear();
				l8h = null;
			}
			if (l8_u0002_ != null)
			{
				l8_u0002_.Clear();
				l8_u0002_ = null;
			}
			if (disposing)
			{
				if (l8Y != null)
				{
					l8Y.Clear();
					l8Y = null;
				}
				if (l8v != null)
				{
					l8v.Clear();
					l8v = null;
				}
			}
		}
		l8L = true;
	}

	private byte[] lc(Z.a param_0, int param_1)
	{
		byte[] bytes = param_0.GetBytes();
		if (bytes.Length >= param_1)
		{
			return bytes;
		}
		byte[] array = new byte[param_1];
		Buffer.BlockCopy(bytes, 0, array, param_1 - bytes.Length, bytes.Length);
		Array.Clear(bytes, 0, bytes.Length);
		return array;
	}
}
