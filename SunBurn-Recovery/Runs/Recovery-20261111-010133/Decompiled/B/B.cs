using System;
using System.Collections.Generic;
using SynapseGaming.LightingSystem.Core;

namespace B
{
	internal class B<Tkey, Tvalue>
	{
		internal static uint l8a = 2147483647u;

		internal Tkey l8_u0013_;

		internal global::B.B<Tkey, Tvalue> l8N;

		internal global::B.B<Tkey, Tvalue> l8l;

		internal List<Tvalue> l8L = new List<Tvalue>();

		internal uint l8w;

		private global::B.B<Tkey, Tvalue> l8J;

		private global::B.B<Tkey, Tvalue> l8B;

		private global::B.B<Tkey, Tvalue> l8s;

		private static PooledObjectFactory<global::B.B<Tkey, Tvalue>> l8h = new PooledObjectFactory<global::B.B<Tkey, Tvalue>>();

		public B(uint hashcode)
		{
			l8w = hashcode;
		}

		public B()
		{
			l8w = l8a;
		}

		internal global::B.B<Tkey, Tvalue> _u0013_v()
		{
			return _u0013__u0005_(default, 0u);
		}

		internal global::B.B<Tkey, Tvalue> _u0013_Y(Tkey param_0)
		{
			return _u0013__u0005_(param_0);
		}

		internal global::B.B<Tkey, Tvalue> _u0013__u0005_(Tkey param_0)
		{
			return _u0013__u0005_(param_0, (uint)param_0.GetHashCode());
		}

		internal global::B.B<Tkey, Tvalue> _u0013__u0005_(Tkey param_0, uint param_1)
		{
			if (param_1 == l8w)
			{
				if (EqualityComparer<Tkey>.Default.Equals(l8_u0013_, default))
				{
					l8_u0013_ = param_0;
				}
				return this;
			}
			if (param_1 > l8w)
			{
				if (l8J == null)
				{
					l8J = l8h.New();
					l8J.l8w = param_1;
					l8J.l8s = this;
					_u0013_b(l8N, this, l8J);
				}
				return l8J._u0013__u0005_(param_0, param_1);
			}
			if (l8B == null)
			{
				l8B = l8h.New();
				l8B.l8w = param_1;
				l8B.l8s = this;
				_u0013_b(this, l8l, l8B);
			}
			return l8B._u0013__u0005_(param_0, param_1);
		}

		internal void _id_3()
		{
			l8_u0013_ = default;
			l8L.Clear();
			l8w = l8a;
			l8N = null;
			l8l = null;
			l8s = null;
			if (l8J != null)
			{
				l8J._id_3();
				l8h.Free(l8J);
				l8J = null;
			}
			if (l8B != null)
			{
				l8B._id_3();
				l8h.Free(l8B);
				l8B = null;
			}
		}

		private void _u0013_b(global::B.B<Tkey, Tvalue> param_0, global::B.B<Tkey, Tvalue> param_1, global::B.B<Tkey, Tvalue> param_2)
		{
			if (param_0 != null)
			{
				param_0.l8l = param_2;
			}
			if (param_1 != null)
			{
				param_1.l8N = param_2;
			}
			param_2.l8N = param_0;
			param_2.l8l = param_1;
		}

		internal static void e(global::B.B<Tkey, Tvalue> param_0)
		{
			if (param_0.l8s == null)
			{
				return;
			}
			if (param_0.l8N != null)
			{
				param_0.l8N.l8l = param_0.l8l;
			}
			if (param_0.l8l != null)
			{
				param_0.l8l.l8N = param_0.l8N;
			}
			if (param_0.l8J == null)
			{
				if (param_0.Equals(param_0.l8s.l8J))
				{
					param_0.l8s.l8J = param_0.l8B;
				}
				else
				{
					param_0.l8s.l8B = param_0.l8B;
				}
				if (param_0.l8B != null)
				{
					param_0.l8B.l8s = param_0.l8s;
				}
			}
			else if (param_0.l8B == null)
			{
				if (param_0.Equals(param_0.l8s.l8J))
				{
					param_0.l8s.l8J = param_0.l8J;
				}
				else
				{
					param_0.l8s.l8B = param_0.l8J;
				}
				if (param_0.l8J != null)
				{
					param_0.l8J.l8s = param_0.l8s;
				}
			}
			else
			{
				if (param_0.Equals(param_0.l8s.l8J))
				{
					param_0.l8s.l8J = param_0.l8B;
				}
				else
				{
					param_0.l8s.l8B = param_0.l8B;
				}
				param_0.l8B.l8s = param_0.l8s;
				global::B.B<Tkey, Tvalue> b = param_0.l8l;
				while (b.l8J != null)
				{
					b = b.l8J;
				}
				b.l8J = param_0.l8J;
				param_0.l8J.l8s = b;
			}
			param_0.l8J = null;
			param_0.l8B = null;
			param_0._id_3();
			l8h.Free(param_0);
		}

		internal static void _u0013_I(global::B.B<Tkey, Tvalue> param_0)
		{
			global::B.B<Tkey, Tvalue> b = param_0;
			while (b.l8s != null)
			{
				b = b.l8s;
			}
			global::B.B<Tkey, Tvalue> b2 = b._u0013_v();
			uint num = b2.l8w;
			int num2 = 0;
			while (b2 != null)
			{
				if (num > b2.l8w || (num == b2.l8w && num != 0))
				{
					throw new Exception("Tree failed link verification.");
				}
				num2++;
				num = b2.l8w;
				b2 = b2.l8N;
			}
			int param_1 = 0;
			_u0013_5(b, ref param_1);
			if (num2 != param_1)
			{
				throw new Exception("Tree failed comparison verification.");
			}
		}

		internal static void _u0013_5(global::B.B<Tkey, Tvalue> param_0, ref int param_1)
		{
			bool flag = false;
			if (param_0.l8B != null && param_0.l8B.l8w >= param_0.l8w)
			{
				flag = true;
			}
			if (param_0.l8J != null && param_0.l8J.l8w <= param_0.l8w)
			{
				flag = true;
			}
			if (flag)
			{
				throw new Exception("Tree failed map verification.");
			}
			if (param_0.l8B != null)
			{
				_u0013_5(param_0.l8B, ref param_1);
			}
			if (param_0.l8J != null)
			{
				_u0013_5(param_0.l8J, ref param_1);
			}
			param_1++;
		}
	}
}
namespace b
{
	internal enum B
	{
		Point,
		Directional
	}
}
