using System;
using System.Collections.Generic;
using SynapseGaming.LightingSystem.Core;

namespace L;

internal class _8<Tkey, Tvalue>
{
	internal static uint fNB = 2147483647u;

	internal Tkey fNa;

	internal global::L._8<Tkey, Tvalue> fNg;

	internal global::L._8<Tkey, Tvalue> fNf;

	internal List<Tvalue> fN_0003 = new List<Tvalue>();

	internal uint fNn;

	private global::L._8<Tkey, Tvalue> fN_0019;

	private global::L._8<Tkey, Tvalue> fNL;

	private global::L._8<Tkey, Tvalue> fNv;

	private static PooledObjectFactory<global::L._8<Tkey, Tvalue>> fN_0012 = new PooledObjectFactory<global::L._8<Tkey, Tvalue>>();

	public _8(uint hashcode)
	{
		fNn = hashcode;
	}

	public _8()
	{
		fNn = fNB;
	}

	internal global::L._8<Tkey, Tvalue> a_0001()
	{
		return a_0018(default, 0u);
	}

	internal global::L._8<Tkey, Tvalue> a_0002(Tkey P_0)
	{
		return a_0018(P_0);
	}

	internal global::L._8<Tkey, Tvalue> a_0018(Tkey P_0)
	{
		return a_0018(P_0, (uint)P_0.GetHashCode());
	}

	internal global::L._8<Tkey, Tvalue> a_0018(Tkey P_0, uint P_1)
	{
		if (P_1 == fNn)
		{
			if (EqualityComparer<Tkey>.Default.Equals(fNa, default))
			{
				fNa = P_0;
			}
			return this;
		}
		if (P_1 > fNn)
		{
			if (fN_0019 == null)
			{
				fN_0019 = fN_0012.New();
				fN_0019.fNn = P_1;
				fN_0019.fNv = this;
				aN(fNg, this, fN_0019);
			}
			return fN_0019.a_0018(P_0, P_1);
		}
		if (fNL == null)
		{
			fNL = fN_0012.New();
			fNL.fNn = P_1;
			fNL.fNv = this;
			aN(this, fNf, fNL);
		}
		return fNL.a_0018(P_0, P_1);
	}

	internal void _0013()
	{
		fNa = default;
		fN_0003.Clear();
		fNn = fNB;
		fNg = null;
		fNf = null;
		fNv = null;
		if (fN_0019 != null)
		{
			fN_0019._0013();
			fN_0012.Free(fN_0019);
			fN_0019 = null;
		}
		if (fNL != null)
		{
			fNL._0013();
			fN_0012.Free(fNL);
			fNL = null;
		}
	}

	private void aN(global::L._8<Tkey, Tvalue> P_0, global::L._8<Tkey, Tvalue> P_1, global::L._8<Tkey, Tvalue> P_2)
	{
		if (P_0 != null)
		{
			P_0.fNf = P_2;
		}
		if (P_1 != null)
		{
			P_1.fNg = P_2;
		}
		P_2.fNg = P_0;
		P_2.fNf = P_1;
	}

	internal static void C(global::L._8<Tkey, Tvalue> P_0)
	{
		if (P_0.fNv == null)
		{
			return;
		}
		if (P_0.fNg != null)
		{
			P_0.fNg.fNf = P_0.fNf;
		}
		if (P_0.fNf != null)
		{
			P_0.fNf.fNg = P_0.fNg;
		}
		if (P_0.fN_0019 == null)
		{
			if (P_0.Equals(P_0.fNv.fN_0019))
			{
				P_0.fNv.fN_0019 = P_0.fNL;
			}
			else
			{
				P_0.fNv.fNL = P_0.fNL;
			}
			if (P_0.fNL != null)
			{
				P_0.fNL.fNv = P_0.fNv;
			}
		}
		else if (P_0.fNL == null)
		{
			if (P_0.Equals(P_0.fNv.fN_0019))
			{
				P_0.fNv.fN_0019 = P_0.fN_0019;
			}
			else
			{
				P_0.fNv.fNL = P_0.fN_0019;
			}
			if (P_0.fN_0019 != null)
			{
				P_0.fN_0019.fNv = P_0.fNv;
			}
		}
		else
		{
			if (P_0.Equals(P_0.fNv.fN_0019))
			{
				P_0.fNv.fN_0019 = P_0.fNL;
			}
			else
			{
				P_0.fNv.fNL = P_0.fNL;
			}
			P_0.fNL.fNv = P_0.fNv;
			global::L._8<Tkey, Tvalue> obj = P_0.fNf;
			while (obj.fN_0019 != null)
			{
				obj = obj.fN_0019;
			}
			obj.fN_0019 = P_0.fN_0019;
			P_0.fN_0019.fNv = obj;
		}
		P_0.fN_0019 = null;
		P_0.fNL = null;
		P_0._0013();
		fN_0012.Free(P_0);
	}

	internal static void gB(global::L._8<Tkey, Tvalue> P_0)
	{
		global::L._8<Tkey, Tvalue> obj = P_0;
		while (obj.fNv != null)
		{
			obj = obj.fNv;
		}
		global::L._8<Tkey, Tvalue> obj2 = obj.a_0001();
		uint num = obj2.fNn;
		int num2 = 0;
		while (obj2 != null)
		{
			if (num > obj2.fNn || (num == obj2.fNn && num != 0))
			{
				throw new Exception("Tree failed link verification.");
			}
			num2++;
			num = obj2.fNn;
			obj2 = obj2.fNg;
		}
		int num3 = 0;
		ga(obj, ref num3);
		if (num2 != num3)
		{
			throw new Exception("Tree failed comparison verification.");
		}
	}

	internal static void ga(global::L._8<Tkey, Tvalue> P_0, ref int P_1)
	{
		bool flag = false;
		if (P_0.fNL != null && P_0.fNL.fNn >= P_0.fNn)
		{
			flag = true;
		}
		if (P_0.fN_0019 != null && P_0.fN_0019.fNn <= P_0.fNn)
		{
			flag = true;
		}
		if (flag)
		{
			throw new Exception("Tree failed map verification.");
		}
		if (P_0.fNL != null)
		{
			ga(P_0.fNL, ref P_1);
		}
		if (P_0.fN_0019 != null)
		{
			ga(P_0.fN_0019, ref P_1);
		}
		P_1++;
	}
}
