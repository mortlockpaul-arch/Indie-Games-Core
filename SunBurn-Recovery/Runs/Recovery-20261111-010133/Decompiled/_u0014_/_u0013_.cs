using System;

namespace _u0014_;

internal abstract class _u0013_ : a
{
	public new static _u0013_ Create()
	{
		return new v();
	}

	public _u0013_()
	{
	}

	public abstract byte[] EncryptValue(byte[] rgb);

	public abstract byte[] DecryptValue(byte[] rgb);

	public abstract _u0005_ ExportParameters(bool include);

	public abstract void ImportParameters(_u0005_ parameters);

	internal void lU(_u0005_ param_0)
	{
		if (param_0.P != null)
		{
			Array.Clear(param_0.P, 0, param_0.P.Length);
		}
		if (param_0.Q != null)
		{
			Array.Clear(param_0.Q, 0, param_0.Q.Length);
		}
		if (param_0.DP != null)
		{
			Array.Clear(param_0.DP, 0, param_0.DP.Length);
		}
		if (param_0.DQ != null)
		{
			Array.Clear(param_0.DQ, 0, param_0.DQ.Length);
		}
		if (param_0.InverseQ != null)
		{
			Array.Clear(param_0.InverseQ, 0, param_0.InverseQ.Length);
		}
		if (param_0.D != null)
		{
			Array.Clear(param_0.D, 0, param_0.D.Length);
		}
	}
}
