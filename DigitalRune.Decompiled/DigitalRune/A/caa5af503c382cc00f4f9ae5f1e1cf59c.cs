using System;
using System.Collections.Generic;

namespace A;

internal class caa5af503c382cc00f4f9ae5f1e1cf59c<c8749090fcc19beeda57becdff9da0d93> where c8749090fcc19beeda57becdff9da0d93 : class
{
	private const int c37f7a571e85c6efe5ea46a981bd40b11 = 4;

	private const int cd095e8302e793acd150fba380d718b36 = 2;

	private c8749090fcc19beeda57becdff9da0d93[] c066e078dfd05ca33d169e63f701db8d0;

	private int ccc439410c5678c542a116b1c7b4551b8;

	public int ca2ab9bfbe566d4bbb2f79385a1dc216b => ccc439410c5678c542a116b1c7b4551b8;

	public caa5af503c382cc00f4f9ae5f1e1cf59c()
	{
		c066e078dfd05ca33d169e63f701db8d0 = new c8749090fcc19beeda57becdff9da0d93[4];
		ccc439410c5678c542a116b1c7b4551b8 = 0;
	}

	public void c0a4aead06b14dcb81618057a59d4a433()
	{
		Array.Clear(c066e078dfd05ca33d169e63f701db8d0, 0, ccc439410c5678c542a116b1c7b4551b8);
		ccc439410c5678c542a116b1c7b4551b8 = 0;
	}

	public bool ce475b78d124a2feef9cd2aab44c161c4(c8749090fcc19beeda57becdff9da0d93 cf51048ba330b8d3f481ea5bc25a03eb9)
	{
		EqualityComparer<c8749090fcc19beeda57becdff9da0d93> equalityComparer = EqualityComparer<c8749090fcc19beeda57becdff9da0d93>.Default;
		for (int i = 0; i < ccc439410c5678c542a116b1c7b4551b8; i++)
		{
			if (equalityComparer.Equals(c066e078dfd05ca33d169e63f701db8d0[i], cf51048ba330b8d3f481ea5bc25a03eb9))
			{
				return true;
			}
		}
		return false;
	}

	public c8749090fcc19beeda57becdff9da0d93 c8291bb0ce023b5713dd6741dca85a2cc()
	{
		if (ccc439410c5678c542a116b1c7b4551b8 == 0)
		{
			return null;
		}
		ccc439410c5678c542a116b1c7b4551b8--;
		c8749090fcc19beeda57becdff9da0d93 result = c066e078dfd05ca33d169e63f701db8d0[ccc439410c5678c542a116b1c7b4551b8];
		c066e078dfd05ca33d169e63f701db8d0[ccc439410c5678c542a116b1c7b4551b8] = null;
		return result;
	}

	public void c22e6a1d6a12c03e9bfc60b749df7f18b(c8749090fcc19beeda57becdff9da0d93 cf51048ba330b8d3f481ea5bc25a03eb9)
	{
		if (ccc439410c5678c542a116b1c7b4551b8 == c066e078dfd05ca33d169e63f701db8d0.Length)
		{
			c8749090fcc19beeda57becdff9da0d93[] destinationArray = new c8749090fcc19beeda57becdff9da0d93[2 * c066e078dfd05ca33d169e63f701db8d0.Length];
			Array.Copy(c066e078dfd05ca33d169e63f701db8d0, 0, destinationArray, 0, ccc439410c5678c542a116b1c7b4551b8);
			c066e078dfd05ca33d169e63f701db8d0 = destinationArray;
		}
		c066e078dfd05ca33d169e63f701db8d0[ccc439410c5678c542a116b1c7b4551b8] = cf51048ba330b8d3f481ea5bc25a03eb9;
		ccc439410c5678c542a116b1c7b4551b8++;
	}
}
