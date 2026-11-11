using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SynapseGaming.LightingSystem.Core;

namespace B
{
	internal class a<T> where T : IWorldBoundingBoxObject
	{
		private enum w8a
		{
			X,
			Y,
			Z,
			None
		}

		private bool l8a;

		private int l8_u0013_;

		private BoundingBox l8N;

		private w8a l8l;

		private Plane l8L;

		private global::B._u0013_<T> l8w;

		private Dictionary<T, global::B.a<T>> l8J;

		private List<T> l8B = new List<T>();

		private global::B.a<T> l8s;

		private global::B.a<T> l8h;

		private static SystemStatistic l8_u0002_ = SystemConsole.GetStatistic("SceneGraph_CollectObjectsTraversedNodes", SystemStatisticCategory.SceneGraph);

		private static SystemStatistic l8v = SystemConsole.GetStatistic("SceneGraph_CollectObjectsRetrievedObjects", SystemStatisticCategory.SceneGraph);

		private static SystemStatistic l8Y = SystemConsole.GetStatistic("SceneGraph_CollectObjectsNodeContainTests", SystemStatisticCategory.SceneGraph);

		private static SystemStatistic l8_u0005_ = SystemConsole.GetStatistic("SceneGraph_CollectObjectsObjectContainTests", SystemStatisticCategory.SceneGraph);

		private static PooledObjectFactory<global::B.a<T>> l8b = new PooledObjectFactory<global::B.a<T>>();

		private static float[] l8I = new float[3];

		protected void Init(ref BoundingBox containervolume, int maxdepth, global::B._u0013_<T> parenttree)
		{
			l8w = parenttree;
			l8J = parenttree.l8a;
			_id_3();
			l8N = containervolume;
			l8_u0013_ = maxdepth;
			Vector3 vector = l8N.Max - l8N.Min;
			float num = vector.X;
			int num2 = 0;
			l8I[0] = vector.X;
			l8I[1] = vector.Y;
			l8I[2] = vector.Z;
			for (int i = 1; i < 3; i++)
			{
				if (!(num > l8I[i]))
				{
					num = l8I[i];
					num2 = i;
				}
			}
			l8l = (w8a)num2;
			l8I[0] = 0f;
			l8I[1] = 0f;
			l8I[2] = 0f;
			l8I[num2] = 1f;
			l8L.Normal.X = l8I[0];
			l8L.Normal.Y = l8I[1];
			l8L.Normal.Z = l8I[2];
			l8I[0] = l8N.Min.X;
			l8I[1] = l8N.Min.Y;
			l8I[2] = l8N.Min.Z;
			l8L.D = 0f - (l8I[num2] + num * 0.5f);
			l8a = l8N.Min.X == l8w.l8N.Min.X || l8N.Min.Y == l8w.l8N.Min.Y || l8N.Min.Z == l8w.l8N.Min.Z || l8N.Max.X == l8w.l8N.Max.X || l8N.Max.Y == l8w.l8N.Max.Y || l8N.Max.Z == l8w.l8N.Max.Z;
		}

		internal virtual void _id_3()
		{
			l8B.Clear();
			if (l8s != null)
			{
				l8s._id_3();
				l8b.Free(l8s);
				l8s = null;
			}
			if (l8h != null)
			{
				l8h._id_3();
				l8b.Free(l8h);
				l8h = null;
			}
		}

		internal void q()
		{
			if (l8s != null)
			{
				l8s.q();
				if (l8s.l8B.Count <= 0 && l8s.l8s == null && l8s.l8h == null)
				{
					l8b.Free(l8s);
					l8s = null;
				}
			}
			if (l8h != null)
			{
				l8h.q();
				if (l8h.l8B.Count <= 0 && l8h.l8s == null && l8h.l8h == null)
				{
					l8b.Free(l8h);
					l8h = null;
				}
			}
		}

		internal void _id_0(BoundingBox param_0, T param_1)
		{
			if (!l8J.ContainsKey(param_1))
			{
				global::B.a<T> a2 = d(ref param_0, 0);
				a2.l8B.Add(param_1);
				l8J.Add(param_1, a2);
			}
		}

		internal void R(BoundingBox param_0, T param_1)
		{
			if (l8J.TryGetValue(param_1, out var value))
			{
				global::B.a<T> a2 = d(ref param_0, 0);
				if (value != a2)
				{
					value.l8B.Remove(param_1);
					a2.l8B.Add(param_1);
					l8J[param_1] = a2;
				}
			}
		}

		internal void i(BoundingBox param_0, T param_1)
		{
			if (l8J.TryGetValue(param_1, out var value))
			{
				value.l8B.Remove(param_1);
				l8J.Remove(param_1);
			}
		}

		private global::B.a<T> d(ref BoundingBox param_0, int param_1)
		{
			if (param_1 == 0)
			{
				l8N.Contains(ref param_0, out var result);
				if (result != ContainmentType.Contains)
				{
					return this;
				}
			}
			bool flag = param_0.Max.X * l8L.Normal.X + param_0.Max.Y * l8L.Normal.Y + param_0.Max.Z * l8L.Normal.Z + l8L.D > 0f;
			bool flag2 = param_0.Min.X * l8L.Normal.X + param_0.Min.Y * l8L.Normal.Y + param_0.Min.Z * l8L.Normal.Z + l8L.D > 0f;
			if (flag != flag2 || param_1 >= l8_u0013_)
			{
				return this;
			}
			if (flag2)
			{
				if (l8s == null)
				{
					BoundingBox containervolume = l8N;
					containervolume.Min = CoreHelper.ReplaceVectorIndex(containervolume.Min, (int)l8l, 0f - l8L.D);
					l8s = l8b.New();
					l8s.Init(ref containervolume, l8_u0013_, l8w);
				}
				return l8s.d(ref param_0, param_1 + 1);
			}
			if (l8h == null)
			{
				BoundingBox containervolume2 = l8N;
				containervolume2.Max = CoreHelper.ReplaceVectorIndex(containervolume2.Max, (int)l8l, 0f - l8L.D);
				l8h = l8b.New();
				l8h.Init(ref containervolume2, l8_u0013_, l8w);
			}
			return l8h.d(ref param_0, param_1 + 1);
		}

		internal void _u0015_(ref BoundingBox param_0, bool param_1, List<T> param_2)
		{
			l8_u0002_.AccumulationValue++;
			ContainmentType result = ContainmentType.Contains;
			if (!param_1)
			{
				l8Y.AccumulationValue++;
				param_0.Contains(ref l8N, out result);
			}
			int count = l8B.Count;
			if (result == ContainmentType.Contains)
			{
				param_1 = true;
				for (int i = 0; i < count; i++)
				{
					param_2.Add(l8B[i]);
				}
				l8v.AccumulationValue += count;
			}
			else
			{
				param_1 = false;
				for (int j = 0; j < count; j++)
				{
					T item = l8B[j];
					if (param_0.Contains(item.WorldBoundingBox) != ContainmentType.Disjoint)
					{
						param_2.Add(item);
						l8v.AccumulationValue++;
					}
				}
				l8_u0005_.AccumulationValue += count;
			}
			if (l8s != null && param_0.Max.X * l8L.Normal.X + param_0.Max.Y * l8L.Normal.Y + param_0.Max.Z * l8L.Normal.Z + l8L.D > 0f)
			{
				l8s._u0015_(ref param_0, param_1, param_2);
			}
			if (l8h != null && param_0.Min.X * l8L.Normal.X + param_0.Min.Y * l8L.Normal.Y + param_0.Min.Z * l8L.Normal.Z + l8L.D < 0f)
			{
				l8h._u0015_(ref param_0, param_1, param_2);
			}
		}

		internal void _u0015_(ref BoundingFrustum param_0, ref BoundingBox param_1, bool param_2, List<T> param_3)
		{
			_u0015_(ref param_0, ref param_1, param_2: true, param_2, param_3);
		}

		private void _u0015_(ref BoundingFrustum param_0, ref BoundingBox param_1, bool param_2, bool param_3, List<T> param_4)
		{
			l8_u0002_.AccumulationValue++;
			ContainmentType result = ContainmentType.Contains;
			if (!param_3)
			{
				l8Y.AccumulationValue++;
				param_0.Contains(ref l8N, out result);
				if (result == ContainmentType.Disjoint && !param_2)
				{
					return;
				}
			}
			int count = l8B.Count;
			if (result == ContainmentType.Contains)
			{
				param_3 = true;
				for (int i = 0; i < count; i++)
				{
					param_4.Add(l8B[i]);
				}
				l8v.AccumulationValue += count;
			}
			else
			{
				param_3 = false;
				for (int j = 0; j < count; j++)
				{
					T item = l8B[j];
					if (param_0.Contains(item.WorldBoundingBox) != ContainmentType.Disjoint)
					{
						param_4.Add(item);
						l8v.AccumulationValue++;
					}
				}
				l8_u0005_.AccumulationValue += count;
			}
			if (l8s != null && param_1.Max.X * l8L.Normal.X + param_1.Max.Y * l8L.Normal.Y + param_1.Max.Z * l8L.Normal.Z + l8L.D > 0f)
			{
				l8s._u0015_(ref param_0, ref param_1, param_2: false, param_3, param_4);
			}
			if (l8h != null && param_1.Min.X * l8L.Normal.X + param_1.Min.Y * l8L.Normal.Y + param_1.Min.Z * l8L.Normal.Z + l8L.D < 0f)
			{
				l8h._u0015_(ref param_0, ref param_1, param_2: false, param_3, param_4);
			}
		}

		internal void r(List<T> param_0)
		{
			foreach (KeyValuePair<T, global::B.a<T>> item in l8J)
			{
				param_0.Add(item.Key);
			}
		}

		internal bool K()
		{
			int count = l8J.Count;
			if (count <= 50)
			{
				return false;
			}
			int num = M(0);
			float num2 = (float)num / (float)count;
			return num2 > 0.1f;
		}

		private int M(int param_0)
		{
			int num = 0;
			if (param_0 >= l8_u0013_)
			{
				if (l8a)
				{
					num = l8B.Count;
				}
			}
			else
			{
				if (l8s != null)
				{
					num += l8s.M(param_0 + 1);
				}
				if (l8h != null)
				{
					num += l8h.M(param_0 + 1);
				}
			}
			return num;
		}
	}
}
namespace b
{
	internal interface a
	{
		string MaterialFile { get; }
	}
}
