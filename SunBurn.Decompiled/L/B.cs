using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SynapseGaming.LightingSystem.Core;

namespace L;

internal class B<T> where T : IWorldBoundingBoxObject
{
	private enum nNB
	{
		X,
		Y,
		Z,
		None
	}

	private bool fNB;

	private int fNa;

	private BoundingBox fNg;

	private nNB fNf;

	private Plane fN_0003;

	private global::L.a<T> fNn;

	private Dictionary<T, global::L.B<T>> fN_0019;

	private List<T> fNL = new List<T>();

	private global::L.B<T> fNv;

	private global::L.B<T> fN_0012;

	private static SystemStatistic fN_0015 = SystemConsole.GetStatistic("SceneGraph_CollectObjectsTraversedNodes", SystemStatisticCategory.SceneGraph);

	private static SystemStatistic fN8 = SystemConsole.GetStatistic("SceneGraph_CollectObjectsRetrievedObjects", SystemStatisticCategory.SceneGraph);

	private static SystemStatistic fNi = SystemConsole.GetStatistic("SceneGraph_CollectObjectsNodeContainTests", SystemStatisticCategory.SceneGraph);

	private static SystemStatistic fN_0005 = SystemConsole.GetStatistic("SceneGraph_CollectObjectsObjectContainTests", SystemStatisticCategory.SceneGraph);

	private static PooledObjectFactory<global::L.B<T>> fNj = new PooledObjectFactory<global::L.B<T>>();

	private static float[] fNo = new float[3];

	protected void Init(ref BoundingBox containervolume, int maxdepth, global::L.a<T> parenttree)
	{
		fNn = parenttree;
		fN_0019 = parenttree.fNB;
		_0013();
		fNg = containervolume;
		fNa = maxdepth;
		Vector3 vector = fNg.Max - fNg.Min;
		float num = vector.X;
		int num2 = 0;
		fNo[0] = vector.X;
		fNo[1] = vector.Y;
		fNo[2] = vector.Z;
		for (int k = 1; k < 3; k++)
		{
			if (!(num > fNo[k]))
			{
				num = fNo[k];
				num2 = k;
			}
		}
		fNf = (nNB)num2;
		fNo[0] = 0f;
		fNo[1] = 0f;
		fNo[2] = 0f;
		fNo[num2] = 1f;
		fN_0003.Normal.X = fNo[0];
		fN_0003.Normal.Y = fNo[1];
		fN_0003.Normal.Z = fNo[2];
		fNo[0] = fNg.Min.X;
		fNo[1] = fNg.Min.Y;
		fNo[2] = fNg.Min.Z;
		fN_0003.D = 0f - (fNo[num2] + num * 0.5f);
		fNB = fNg.Min.X == fNn.fNg.Min.X || fNg.Min.Y == fNn.fNg.Min.Y || fNg.Min.Z == fNn.fNg.Min.Z || fNg.Max.X == fNn.fNg.Max.X || fNg.Max.Y == fNn.fNg.Max.Y || fNg.Max.Z == fNn.fNg.Max.Z;
	}

	internal virtual void _0013()
	{
		fNL.Clear();
		if (fNv != null)
		{
			fNv._0013();
			fNj.Free(fNv);
			fNv = null;
		}
		if (fN_0012 != null)
		{
			fN_0012._0013();
			fNj.Free(fN_0012);
			fN_0012 = null;
		}
	}

	internal void _3()
	{
		if (fNv != null)
		{
			fNv._3();
			if (fNv.fNL.Count <= 0 && fNv.fNv == null && fNv.fN_0012 == null)
			{
				fNj.Free(fNv);
				fNv = null;
			}
		}
		if (fN_0012 != null)
		{
			fN_0012._3();
			if (fN_0012.fNL.Count <= 0 && fN_0012.fNv == null && fN_0012.fN_0012 == null)
			{
				fNj.Free(fN_0012);
				fN_0012 = null;
			}
		}
	}

	internal void l(BoundingBox P_0, T P_1)
	{
		if (!fN_0019.ContainsKey(P_1))
		{
			global::L.B<T> b = _7(ref P_0, 0);
			b.fNL.Add(P_1);
			fN_0019.Add(P_1, b);
		}
	}

	internal void t(BoundingBox P_0, T P_1)
	{
		if (fN_0019.TryGetValue(P_1, out var value))
		{
			global::L.B<T> b = _7(ref P_0, 0);
			if (value != b)
			{
				value.fNL.Remove(P_1);
				b.fNL.Add(P_1);
				fN_0019[P_1] = b;
			}
		}
	}

	internal void _2(BoundingBox P_0, T P_1)
	{
		if (fN_0019.TryGetValue(P_1, out var value))
		{
			value.fNL.Remove(P_1);
			fN_0019.Remove(P_1);
		}
	}

	private global::L.B<T> _7(ref BoundingBox P_0, int P_1)
	{
		if (P_1 == 0)
		{
			fNg.Contains(ref P_0, out var result);
			if (result != ContainmentType.Contains)
			{
				return this;
			}
		}
		bool flag = P_0.Max.X * fN_0003.Normal.X + P_0.Max.Y * fN_0003.Normal.Y + P_0.Max.Z * fN_0003.Normal.Z + fN_0003.D > 0f;
		bool flag2 = P_0.Min.X * fN_0003.Normal.X + P_0.Min.Y * fN_0003.Normal.Y + P_0.Min.Z * fN_0003.Normal.Z + fN_0003.D > 0f;
		if (flag != flag2 || P_1 >= fNa)
		{
			return this;
		}
		if (flag2)
		{
			if (fNv == null)
			{
				BoundingBox containervolume = fNg;
				containervolume.Min = _0019.a_0017(containervolume.Min, (int)fNf, 0f - fN_0003.D);
				fNv = fNj.New();
				fNv.Init(ref containervolume, fNa, fNn);
			}
			return fNv._7(ref P_0, P_1 + 1);
		}
		if (fN_0012 == null)
		{
			BoundingBox containervolume2 = fNg;
			containervolume2.Max = _0019.a_0017(containervolume2.Max, (int)fNf, 0f - fN_0003.D);
			fN_0012 = fNj.New();
			fN_0012.Init(ref containervolume2, fNa, fNn);
		}
		return fN_0012._7(ref P_0, P_1 + 1);
	}

	internal void p(ref BoundingBox P_0, bool P_1, List<T> P_2)
	{
		fN_0015.AccumulationValue++;
		ContainmentType result = ContainmentType.Contains;
		if (!P_1)
		{
			fNi.AccumulationValue++;
			P_0.Contains(ref fNg, out result);
		}
		int count = fNL.Count;
		if (result == ContainmentType.Contains)
		{
			P_1 = true;
			for (int k = 0; k < count; k++)
			{
				P_2.Add(fNL[k]);
			}
			fN8.AccumulationValue += count;
		}
		else
		{
			P_1 = false;
			for (int l = 0; l < count; l++)
			{
				T item = fNL[l];
				if (P_0.Contains(item.WorldBoundingBox) != ContainmentType.Disjoint)
				{
					P_2.Add(item);
					fN8.AccumulationValue++;
				}
			}
			fN_0005.AccumulationValue += count;
		}
		if (fNv != null && P_0.Max.X * fN_0003.Normal.X + P_0.Max.Y * fN_0003.Normal.Y + P_0.Max.Z * fN_0003.Normal.Z + fN_0003.D > 0f)
		{
			fNv.p(ref P_0, P_1, P_2);
		}
		if (fN_0012 != null && P_0.Min.X * fN_0003.Normal.X + P_0.Min.Y * fN_0003.Normal.Y + P_0.Min.Z * fN_0003.Normal.Z + fN_0003.D < 0f)
		{
			fN_0012.p(ref P_0, P_1, P_2);
		}
	}

	internal void p(ref BoundingFrustum P_0, ref BoundingBox P_1, bool P_2, List<T> P_3)
	{
		p(ref P_0, ref P_1, true, P_2, P_3);
	}

	private void p(ref BoundingFrustum P_0, ref BoundingBox P_1, bool P_2, bool P_3, List<T> P_4)
	{
		fN_0015.AccumulationValue++;
		ContainmentType result = ContainmentType.Contains;
		if (!P_3)
		{
			fNi.AccumulationValue++;
			P_0.Contains(ref fNg, out result);
			if (result == ContainmentType.Disjoint && !P_2)
			{
				return;
			}
		}
		int count = fNL.Count;
		if (result == ContainmentType.Contains)
		{
			P_3 = true;
			for (int k = 0; k < count; k++)
			{
				P_4.Add(fNL[k]);
			}
			fN8.AccumulationValue += count;
		}
		else
		{
			P_3 = false;
			for (int l = 0; l < count; l++)
			{
				T item = fNL[l];
				if (P_0.Contains(item.WorldBoundingBox) != ContainmentType.Disjoint)
				{
					P_4.Add(item);
					fN8.AccumulationValue++;
				}
			}
			fN_0005.AccumulationValue += count;
		}
		if (fNv != null && P_1.Max.X * fN_0003.Normal.X + P_1.Max.Y * fN_0003.Normal.Y + P_1.Max.Z * fN_0003.Normal.Z + fN_0003.D > 0f)
		{
			fNv.p(ref P_0, ref P_1, false, P_3, P_4);
		}
		if (fN_0012 != null && P_1.Min.X * fN_0003.Normal.X + P_1.Min.Y * fN_0003.Normal.Y + P_1.Min.Z * fN_0003.Normal.Z + fN_0003.D < 0f)
		{
			fN_0012.p(ref P_0, ref P_1, false, P_3, P_4);
		}
	}

	internal void Z(List<T> P_0)
	{
		foreach (KeyValuePair<T, global::L.B<T>> item in fN_0019)
		{
			P_0.Add(item.Key);
		}
	}

	internal bool D()
	{
		int count = fN_0019.Count;
		if (count <= 50)
		{
			return false;
		}
		int num = U(0);
		float num2 = (float)num / (float)count;
		return num2 > 0.1f;
	}

	private int U(int P_0)
	{
		int num = 0;
		if (P_0 >= fNa)
		{
			if (fNB)
			{
				num = fNL.Count;
			}
		}
		else
		{
			if (fNv != null)
			{
				num += fNv.U(P_0 + 1);
			}
			if (fN_0012 != null)
			{
				num += fN_0012.U(P_0 + 1);
			}
		}
		return num;
	}
}
