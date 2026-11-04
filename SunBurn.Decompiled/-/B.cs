using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using G;
using Microsoft.Xna.Framework;
using SynapseGaming.LightingSystem.Collision;
using SynapseGaming.LightingSystem.Core;

namespace _0003
{
	internal class B
	{
		internal static SystemStatistic fNB = SystemConsole.GetStatistic("Collision_MeshTestCount", SystemStatisticCategory.Collision);

		internal static SystemStatistic fNa = SystemConsole.GetStatistic("Collision_PolyTestCount", SystemStatisticCategory.Collision);

		internal static SystemStatistic fNg = SystemConsole.GetStatistic("Collision_PolyProjTestCount", SystemStatisticCategory.Collision);

		internal static SystemStatistic fNf = SystemConsole.GetStatistic("Collision_PolyProjPartialTestCount", SystemStatisticCategory.Collision);

		internal static SystemStatistic fN_0003 = SystemConsole.GetStatistic("Collision_PolyProjFullTestCount", SystemStatisticCategory.Collision);

		internal static SystemStatistic fNn = SystemConsole.GetStatistic("Collision_PolyDeepTestCount", SystemStatisticCategory.Collision);

		internal static Vector3 fN_0019 = Vector3.UnitX;

		internal static List<CollisionMesh.CollisionSurface> fNL = new List<CollisionMesh.CollisionSurface>();

		internal static bool A(ICollisionObject P_0, ICollisionObject P_1, CollisionPoint P_2)
		{
			if (P_0.CollisionType != CollisionType.Trigger && P_1.CollisionType != CollisionType.Trigger)
			{
				return false;
			}
			P_2.Triggers.Add(P_1);
			return true;
		}
	}
}
namespace _0019
{
	internal class B
	{
		public string Name = "";

		public string InnerText = "";

		public f Attributes = new f();

		public a ChildNodes = new a();

		public virtual void Load(XObject node)
		{
			Attributes.Clear();
			ChildNodes.Clear();
			if (node is XAttribute)
			{
				XAttribute xAttribute = node as XAttribute;
				Name = xAttribute.Name.LocalName;
				InnerText = xAttribute.Value;
			}
			else
			{
				if (!(node is XElement))
				{
					return;
				}
				XElement xElement = node as XElement;
				Name = xElement.Name.LocalName;
				if (xElement.HasAttributes)
				{
					IEnumerable<XAttribute> enumerable = xElement.Attributes();
					foreach (XAttribute item in enumerable)
					{
						g g2 = new g();
						Attributes.Add(g2);
						g2.Load(item);
					}
				}
				if (xElement.HasElements)
				{
					IEnumerable<XElement> enumerable2 = xElement.Elements();
					{
						foreach (XElement item2 in enumerable2)
						{
							_0003 obj = new _0003();
							ChildNodes.Add(obj);
							obj.Load(item2);
						}
						return;
					}
				}
				InnerText = xElement.Value;
			}
		}
	}
}
namespace _0012
{
	internal static class B
	{
		public const string VisibleDistance = "";

		public const string FogEnabled = "";

		public const string FogStart = "";

		public const string FogEnd = "";

		public const string FogColor = "";

		public const string ShadowFadeStart = "";

		public const string ShadowFadeEnd = "";

		public const string ShadowCasterDist = "";

		public const string BloomAmount = "";

		public const string BloomThreshold = "";

		public const string ExposureAmount = "";

		public const string DynamicRangeEnabled = "";

		public const string HdrTime = "";

		public const string HdrMax = "";

		public const string HdrMin = "";

		public const string HdrSaturation = "";

		public const string HdrDarken = "";

		public const string HdrCinematic = "";

		public const string Gravity = "";

		public const string EditorIconScale = "";

		public const string EditorMoveScale = "";

		public const string EditorRotationScale = "";

		public const string EditorDefaultObjectScale = "";
	}
}
namespace _0015
{
	internal class B
	{
		private float fNB = 10f;

		private float fNa = 10f;

		private bool fNg;

		private float fNf = 1f;

		public float IconScale
		{
			get
			{
				return fNf;
			}
			set
			{
				fNf = value;
			}
		}

		public float MoveScale
		{
			get
			{
				return fNB;
			}
			set
			{
				fNB = value;
			}
		}

		public float RotationScale
		{
			get
			{
				return fNa;
			}
			set
			{
				fNa = value;
			}
		}

		public bool UserHandledView
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
	}
}
namespace _0005
{
	internal static class B
	{
		public const string Diffuse1 = "";

		public const string Diffuse2 = "";

		public const string Diffuse3 = "";

		public const string Diffuse4 = "";

		public const string Normal1 = "";

		public const string Normal2 = "";

		public const string Normal3 = "";

		public const string Normal4 = "";

		public const string HeightMap = "";

		public const string BlendMap = "";

		public const string NormalStrength = "";

		public const string DiffuseScale = "";

		public const string HeightScale = "";

		public const string TileAmount = "";

		public const string SpecularAmount = "";

		public const string SpecularPower = "";

		public const string SpecularColor = "";

		public const string MeshSegments = "";
	}
}
namespace _0011
{
	internal abstract class B : IDisposable
	{
		protected int KeySizeValue;

		protected v[] LegalKeySizesValue;

		public abstract string KeyExchangeAlgorithm { get; }

		public virtual int KeySize
		{
			get
			{
				return KeySizeValue;
			}
			set
			{
				if (!v._0003W(LegalKeySizesValue, value))
				{
					throw new _0003("Key size not supported by algorithm.");
				}
				KeySizeValue = value;
			}
		}

		public virtual v[] LegalKeySizes => LegalKeySizesValue;

		public abstract string SignatureAlgorithm { get; }

		void IDisposable.Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		public void Clear()
		{
			Dispose(disposing: false);
		}

		protected abstract void Dispose(bool disposing);

		public static B Create()
		{
			return new _8();
		}
	}
}
namespace _0017
{
	[Serializable]
	internal class B : IList, G.B, ICollection, IEnumerable
	{
		private sealed class nNB : IEnumerator, G.B
		{
			private int fNB;

			private int fNa;

			private int fNg;

			private object fNf;

			private B fN_0003;

			private int fNn;

			public object Current
			{
				get
				{
					if (fNB == fNa - 1)
					{
						throw new InvalidOperationException("Enumerator unusable (Reset pending, or past end of array.");
					}
					return fNf;
				}
			}

			public nNB(B list)
				: this(list, 0, list.Count)
			{
			}

			public object Clone()
			{
				return MemberwiseClone();
			}

			public nNB(B list, int index, int count)
			{
				fN_0003 = list;
				fNa = index;
				fNg = count;
				fNB = fNa - 1;
				fNf = null;
				fNn = list.fNf;
			}

			public bool MoveNext()
			{
				if (fN_0003.fNf != fNn)
				{
					throw new InvalidOperationException("List has changed.");
				}
				fNB++;
				if (fNB - fNa < fNg)
				{
					fNf = fN_0003[fNB];
					return true;
				}
				return false;
			}

			public void Reset()
			{
				fNf = null;
				fNB = fNa - 1;
			}
		}

		private sealed class nNa : IEnumerator, G.B
		{
			private B fNB;

			private int fNa;

			private int fNg;

			private object fNf;

			private static object fN_0003 = new object();

			public object Current
			{
				get
				{
					if (fNf == fN_0003)
					{
						if (fNa == -1)
						{
							throw new InvalidOperationException("Enumerator not started");
						}
						throw new InvalidOperationException("Enumerator ended");
					}
					return fNf;
				}
			}

			public nNa(B list)
			{
				fNB = list;
				fNa = -1;
				fNg = list.fNf;
				fNf = fN_0003;
			}

			public object Clone()
			{
				return MemberwiseClone();
			}

			public bool MoveNext()
			{
				if (fNg != fNB.fNf)
				{
					throw new InvalidOperationException("List has changed.");
				}
				if (++fNa < fNB.Count)
				{
					fNf = fNB[fNa];
					return true;
				}
				fNf = fN_0003;
				return false;
			}

			public void Reset()
			{
				if (fNg != fNB.fNf)
				{
					throw new InvalidOperationException("List has changed.");
				}
				fNf = fN_0003;
				fNa = -1;
			}
		}

		[Serializable]
		private sealed class nNg : B
		{
			private new sealed class nNB : IEnumerator, G.B
			{
				private int fNB;

				private int fNa;

				private int fNg;

				private IEnumerator fNf;

				public object Current => fNf.Current;

				public nNB(IEnumerator enumerator, int index, int count)
				{
					fNa = 0;
					fNB = index;
					fNg = count;
					fNf = enumerator;
					Reset();
				}

				public object Clone()
				{
					return MemberwiseClone();
				}

				public bool MoveNext()
				{
					if (fNa >= fNg)
					{
						return false;
					}
					fNa++;
					return fNf.MoveNext();
				}

				public void Reset()
				{
					fNa = 0;
					fNf.Reset();
					for (int k = 0; k < fNB; k++)
					{
						fNf.MoveNext();
					}
				}
			}

			private new IList fNB;

			public override object this[int index]
			{
				get
				{
					return fNB[index];
				}
				set
				{
					fNB[index] = value;
				}
			}

			public override int Count => fNB.Count;

			public override int Capacity
			{
				get
				{
					return fNB.Count;
				}
				set
				{
					if (value < fNB.Count)
					{
						throw new ArgumentException("capacity");
					}
				}
			}

			public override bool IsFixedSize => fNB.IsFixedSize;

			public override bool IsReadOnly => fNB.IsReadOnly;

			public override object SyncRoot => fNB.SyncRoot;

			public override bool IsSynchronized => fNB.IsSynchronized;

			public nNg(IList adaptee)
				: base(0, true)
			{
				fNB = adaptee;
			}

			public override int Add(object value)
			{
				return fNB.Add(value);
			}

			public override void Clear()
			{
				fNB.Clear();
			}

			public override bool Contains(object value)
			{
				return fNB.Contains(value);
			}

			public override int IndexOf(object value)
			{
				return fNB.IndexOf(value);
			}

			public override int IndexOf(object value, int startIndex)
			{
				return IndexOf(value, startIndex, fNB.Count - startIndex);
			}

			public override int IndexOf(object value, int startIndex, int count)
			{
				if (startIndex < 0 || startIndex > fNB.Count)
				{
					_0003_0011("startIndex", startIndex, "Does not specify valid index.");
				}
				if (count < 0)
				{
					_0003_0011("count", count, "Can't be less than 0.");
				}
				if (startIndex > fNB.Count - count)
				{
					throw new ArgumentOutOfRangeException("count", "Start index and count do not specify a valid range.");
				}
				if (value == null)
				{
					for (int k = startIndex; k < startIndex + count; k++)
					{
						if (fNB[k] == null)
						{
							return k;
						}
					}
				}
				else
				{
					for (int l = startIndex; l < startIndex + count; l++)
					{
						if (value.Equals(fNB[l]))
						{
							return l;
						}
					}
				}
				return -1;
			}

			public override int LastIndexOf(object value)
			{
				return LastIndexOf(value, fNB.Count - 1);
			}

			public override int LastIndexOf(object value, int startIndex)
			{
				return LastIndexOf(value, startIndex, startIndex + 1);
			}

			public override int LastIndexOf(object value, int startIndex, int count)
			{
				if (startIndex < 0)
				{
					_0003_0011("startIndex", startIndex, "< 0");
				}
				if (count < 0)
				{
					_0003_0011("count", count, "count is negative.");
				}
				if (startIndex - count + 1 < 0)
				{
					_0003_0011("count", count, "count is too large.");
				}
				if (value == null)
				{
					for (int num = startIndex; num > startIndex - count; num--)
					{
						if (fNB[num] == null)
						{
							return num;
						}
					}
				}
				else
				{
					for (int num2 = startIndex; num2 > startIndex - count; num2--)
					{
						if (value.Equals(fNB[num2]))
						{
							return num2;
						}
					}
				}
				return -1;
			}

			public override void Insert(int index, object value)
			{
				fNB.Insert(index, value);
			}

			public override void InsertRange(int index, ICollection c)
			{
				if (c == null)
				{
					throw new ArgumentNullException("c");
				}
				if (index > fNB.Count)
				{
					_0003_0011("index", index, "Index must be >= 0 and <= Count.");
				}
				foreach (object item in c)
				{
					fNB.Insert(index++, item);
				}
			}

			public override void Remove(object value)
			{
				fNB.Remove(value);
			}

			public override void RemoveAt(int index)
			{
				fNB.RemoveAt(index);
			}

			public override void RemoveRange(int index, int count)
			{
				_0003s(index, count, fNB.Count);
				for (int k = 0; k < count; k++)
				{
					fNB.RemoveAt(index);
				}
			}

			public override void Reverse()
			{
				Reverse(0, fNB.Count);
			}

			public override void Reverse(int index, int count)
			{
				_0003s(index, count, fNB.Count);
				for (int k = 0; k < count / 2; k++)
				{
					object value = fNB[k + index];
					fNB[k + index] = fNB[index + count - k + index - 1];
					fNB[index + count - k + index - 1] = value;
				}
			}

			public override void SetRange(int index, ICollection c)
			{
				if (c == null)
				{
					throw new ArgumentNullException("c");
				}
				if (index < 0 || index + c.Count > fNB.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				int num = index;
				foreach (object item in c)
				{
					fNB[num++] = item;
				}
			}

			public override void CopyTo(Array array)
			{
				fNB.CopyTo(array, 0);
			}

			public override void CopyTo(Array array, int index)
			{
				fNB.CopyTo(array, index);
			}

			public override void CopyTo(int index, Array array, int arrayIndex, int count)
			{
				if (index < 0)
				{
					_0003_0011("index", index, "Can't be less than zero.");
				}
				if (arrayIndex < 0)
				{
					_0003_0011("arrayIndex", arrayIndex, "Can't be less than zero.");
				}
				if (count < 0)
				{
					_0003_0011("index", index, "Can't be less than zero.");
				}
				if (index >= fNB.Count)
				{
					throw new ArgumentException("Can't be more or equal to list count.", "index");
				}
				if (array.Rank > 1)
				{
					throw new ArgumentException("Can't copy into multi-dimensional array.");
				}
				if (arrayIndex >= array.Length)
				{
					throw new ArgumentException("arrayIndex can't be greater than array.Length - 1.");
				}
				if (array.Length - arrayIndex + 1 < count)
				{
					throw new ArgumentException("Destination array is too small.");
				}
				if (index > fNB.Count - count)
				{
					throw new ArgumentException("Index and count do not denote a valid range of elements.", "index");
				}
				for (int k = 0; k < count; k++)
				{
					array.SetValue(fNB[index + k], arrayIndex + k);
				}
			}

			public override IEnumerator GetEnumerator()
			{
				return fNB.GetEnumerator();
			}

			public override IEnumerator GetEnumerator(int index, int count)
			{
				_0003s(index, count, fNB.Count);
				return new nNB(fNB.GetEnumerator(), index, count);
			}

			public override void AddRange(ICollection c)
			{
				foreach (object item in c)
				{
					fNB.Add(item);
				}
			}

			public override int BinarySearch(object value)
			{
				return BinarySearch(value, null);
			}

			public override int BinarySearch(object value, IComparer comparer)
			{
				return BinarySearch(0, fNB.Count, value, comparer);
			}

			public override int BinarySearch(int index, int count, object value, IComparer comparer)
			{
				_0003s(index, count, fNB.Count);
				if (comparer == null)
				{
					comparer = a.Default;
				}
				int num = index;
				int num2 = index + count - 1;
				while (num <= num2)
				{
					int num3 = num + (num2 - num) / 2;
					int num4 = comparer.Compare(value, fNB[num3]);
					if (num4 < 0)
					{
						num2 = num3 - 1;
						continue;
					}
					if (num4 > 0)
					{
						num = num3 + 1;
						continue;
					}
					return num3;
				}
				return ~num;
			}

			public override object Clone()
			{
				return new nNg(fNB);
			}

			public override B GetRange(int index, int count)
			{
				_0003s(index, count, fNB.Count);
				return new nNL(this, index, count);
			}

			public override void TrimToSize()
			{
			}

			public override void Sort()
			{
				Sort(a.Default);
			}

			public override void Sort(IComparer comparer)
			{
				Sort(0, fNB.Count, comparer);
			}

			public override void Sort(int index, int count, IComparer comparer)
			{
				_0003s(index, count, fNB.Count);
				if (comparer == null)
				{
					comparer = a.Default;
				}
				_0003G(fNB, index, index + count - 1, comparer);
			}

			private static void _0003Q(IList P_0, int P_1, int P_2)
			{
				object value = P_0[P_1];
				P_0[P_1] = P_0[P_2];
				P_0[P_2] = value;
			}

			internal static void _0003G(IList P_0, int P_1, int P_2, IComparer P_3)
			{
				if (P_1 >= P_2)
				{
					return;
				}
				int num = P_1 + (P_2 - P_1) / 2;
				if (P_3.Compare(P_0[num], P_0[P_1]) < 0)
				{
					_0003Q(P_0, num, P_1);
				}
				if (P_3.Compare(P_0[P_2], P_0[P_1]) < 0)
				{
					_0003Q(P_0, P_2, P_1);
				}
				if (P_3.Compare(P_0[P_2], P_0[num]) < 0)
				{
					_0003Q(P_0, P_2, num);
				}
				if (P_2 - P_1 + 1 <= 3)
				{
					return;
				}
				_0003Q(P_0, P_2 - 1, num);
				object y = P_0[P_2 - 1];
				int num2 = P_1;
				int num3 = P_2 - 1;
				while (true)
				{
					if (P_3.Compare(P_0[++num2], y) >= 0)
					{
						while (P_3.Compare(P_0[--num3], y) > 0)
						{
						}
						if (num2 >= num3)
						{
							break;
						}
						_0003Q(P_0, num2, num3);
					}
				}
				_0003Q(P_0, P_2 - 1, num2);
				_0003G(P_0, P_1, num2 - 1, P_3);
				_0003G(P_0, num2 + 1, P_2, P_3);
			}

			public override object[] ToArray()
			{
				object[] array = new object[fNB.Count];
				fNB.CopyTo(array, 0);
				return array;
			}

			public override Array ToArray(Type elementType)
			{
				Array array = Array.CreateInstance(elementType, fNB.Count);
				fNB.CopyTo(array, 0);
				return array;
			}
		}

		[Serializable]
		private class nNf : B
		{
			protected B m_InnerArrayList;

			public override object this[int index]
			{
				get
				{
					return m_InnerArrayList[index];
				}
				set
				{
					m_InnerArrayList[index] = value;
				}
			}

			public override int Count => m_InnerArrayList.Count;

			public override int Capacity
			{
				get
				{
					return m_InnerArrayList.Capacity;
				}
				set
				{
					m_InnerArrayList.Capacity = value;
				}
			}

			public override bool IsFixedSize => m_InnerArrayList.IsFixedSize;

			public override bool IsReadOnly => m_InnerArrayList.IsReadOnly;

			public override bool IsSynchronized => m_InnerArrayList.IsSynchronized;

			public override object SyncRoot => m_InnerArrayList.SyncRoot;

			public nNf(B innerArrayList)
			{
				m_InnerArrayList = innerArrayList;
			}

			public override int Add(object value)
			{
				return m_InnerArrayList.Add(value);
			}

			public override void Clear()
			{
				m_InnerArrayList.Clear();
			}

			public override bool Contains(object value)
			{
				return m_InnerArrayList.Contains(value);
			}

			public override int IndexOf(object value)
			{
				return m_InnerArrayList.IndexOf(value);
			}

			public override int IndexOf(object value, int startIndex)
			{
				return m_InnerArrayList.IndexOf(value, startIndex);
			}

			public override int IndexOf(object value, int startIndex, int count)
			{
				return m_InnerArrayList.IndexOf(value, startIndex, count);
			}

			public override int LastIndexOf(object value)
			{
				return m_InnerArrayList.LastIndexOf(value);
			}

			public override int LastIndexOf(object value, int startIndex)
			{
				return m_InnerArrayList.LastIndexOf(value, startIndex);
			}

			public override int LastIndexOf(object value, int startIndex, int count)
			{
				return m_InnerArrayList.LastIndexOf(value, startIndex, count);
			}

			public override void Insert(int index, object value)
			{
				m_InnerArrayList.Insert(index, value);
			}

			public override void InsertRange(int index, ICollection c)
			{
				m_InnerArrayList.InsertRange(index, c);
			}

			public override void Remove(object value)
			{
				m_InnerArrayList.Remove(value);
			}

			public override void RemoveAt(int index)
			{
				m_InnerArrayList.RemoveAt(index);
			}

			public override void RemoveRange(int index, int count)
			{
				m_InnerArrayList.RemoveRange(index, count);
			}

			public override void Reverse()
			{
				m_InnerArrayList.Reverse();
			}

			public override void Reverse(int index, int count)
			{
				m_InnerArrayList.Reverse(index, count);
			}

			public override void SetRange(int index, ICollection c)
			{
				m_InnerArrayList.SetRange(index, c);
			}

			public override void CopyTo(Array array)
			{
				m_InnerArrayList.CopyTo(array);
			}

			public override void CopyTo(Array array, int index)
			{
				m_InnerArrayList.CopyTo(array, index);
			}

			public override void CopyTo(int index, Array array, int arrayIndex, int count)
			{
				m_InnerArrayList.CopyTo(index, array, arrayIndex, count);
			}

			public override IEnumerator GetEnumerator()
			{
				return m_InnerArrayList.GetEnumerator();
			}

			public override IEnumerator GetEnumerator(int index, int count)
			{
				return m_InnerArrayList.GetEnumerator(index, count);
			}

			public override void AddRange(ICollection c)
			{
				m_InnerArrayList.AddRange(c);
			}

			public override int BinarySearch(object value)
			{
				return m_InnerArrayList.BinarySearch(value);
			}

			public override int BinarySearch(object value, IComparer comparer)
			{
				return m_InnerArrayList.BinarySearch(value, comparer);
			}

			public override int BinarySearch(int index, int count, object value, IComparer comparer)
			{
				return m_InnerArrayList.BinarySearch(index, count, value, comparer);
			}

			public override object Clone()
			{
				return m_InnerArrayList.Clone();
			}

			public override B GetRange(int index, int count)
			{
				return m_InnerArrayList.GetRange(index, count);
			}

			public override void TrimToSize()
			{
				m_InnerArrayList.TrimToSize();
			}

			public override void Sort()
			{
				m_InnerArrayList.Sort();
			}

			public override void Sort(IComparer comparer)
			{
				m_InnerArrayList.Sort(comparer);
			}

			public override void Sort(int index, int count, IComparer comparer)
			{
				m_InnerArrayList.Sort(index, count, comparer);
			}

			public override object[] ToArray()
			{
				return m_InnerArrayList.ToArray();
			}

			public override Array ToArray(Type elementType)
			{
				return m_InnerArrayList.ToArray(elementType);
			}
		}

		[Serializable]
		private sealed class nN_0003 : nNf
		{
			private new object fNB;

			public override object this[int index]
			{
				get
				{
					lock (fNB)
					{
						return m_InnerArrayList[index];
					}
				}
				set
				{
					lock (fNB)
					{
						m_InnerArrayList[index] = value;
					}
				}
			}

			public override int Count
			{
				get
				{
					lock (fNB)
					{
						return m_InnerArrayList.Count;
					}
				}
			}

			public override int Capacity
			{
				get
				{
					lock (fNB)
					{
						return m_InnerArrayList.Capacity;
					}
				}
				set
				{
					lock (fNB)
					{
						m_InnerArrayList.Capacity = value;
					}
				}
			}

			public override bool IsFixedSize
			{
				get
				{
					lock (fNB)
					{
						return m_InnerArrayList.IsFixedSize;
					}
				}
			}

			public override bool IsReadOnly
			{
				get
				{
					lock (fNB)
					{
						return m_InnerArrayList.IsReadOnly;
					}
				}
			}

			public override bool IsSynchronized => true;

			public override object SyncRoot => fNB;

			internal nN_0003(B P_0)
				: base(P_0)
			{
				fNB = P_0.SyncRoot;
			}

			public override int Add(object value)
			{
				lock (fNB)
				{
					return m_InnerArrayList.Add(value);
				}
			}

			public override void Clear()
			{
				lock (fNB)
				{
					m_InnerArrayList.Clear();
				}
			}

			public override bool Contains(object value)
			{
				lock (fNB)
				{
					return m_InnerArrayList.Contains(value);
				}
			}

			public override int IndexOf(object value)
			{
				lock (fNB)
				{
					return m_InnerArrayList.IndexOf(value);
				}
			}

			public override int IndexOf(object value, int startIndex)
			{
				lock (fNB)
				{
					return m_InnerArrayList.IndexOf(value, startIndex);
				}
			}

			public override int IndexOf(object value, int startIndex, int count)
			{
				lock (fNB)
				{
					return m_InnerArrayList.IndexOf(value, startIndex, count);
				}
			}

			public override int LastIndexOf(object value)
			{
				lock (fNB)
				{
					return m_InnerArrayList.LastIndexOf(value);
				}
			}

			public override int LastIndexOf(object value, int startIndex)
			{
				lock (fNB)
				{
					return m_InnerArrayList.LastIndexOf(value, startIndex);
				}
			}

			public override int LastIndexOf(object value, int startIndex, int count)
			{
				lock (fNB)
				{
					return m_InnerArrayList.LastIndexOf(value, startIndex, count);
				}
			}

			public override void Insert(int index, object value)
			{
				lock (fNB)
				{
					m_InnerArrayList.Insert(index, value);
				}
			}

			public override void InsertRange(int index, ICollection c)
			{
				lock (fNB)
				{
					m_InnerArrayList.InsertRange(index, c);
				}
			}

			public override void Remove(object value)
			{
				lock (fNB)
				{
					m_InnerArrayList.Remove(value);
				}
			}

			public override void RemoveAt(int index)
			{
				lock (fNB)
				{
					m_InnerArrayList.RemoveAt(index);
				}
			}

			public override void RemoveRange(int index, int count)
			{
				lock (fNB)
				{
					m_InnerArrayList.RemoveRange(index, count);
				}
			}

			public override void Reverse()
			{
				lock (fNB)
				{
					m_InnerArrayList.Reverse();
				}
			}

			public override void Reverse(int index, int count)
			{
				lock (fNB)
				{
					m_InnerArrayList.Reverse(index, count);
				}
			}

			public override void CopyTo(Array array)
			{
				lock (fNB)
				{
					m_InnerArrayList.CopyTo(array);
				}
			}

			public override void CopyTo(Array array, int index)
			{
				lock (fNB)
				{
					m_InnerArrayList.CopyTo(array, index);
				}
			}

			public override void CopyTo(int index, Array array, int arrayIndex, int count)
			{
				lock (fNB)
				{
					m_InnerArrayList.CopyTo(index, array, arrayIndex, count);
				}
			}

			public override IEnumerator GetEnumerator()
			{
				lock (fNB)
				{
					return m_InnerArrayList.GetEnumerator();
				}
			}

			public override IEnumerator GetEnumerator(int index, int count)
			{
				lock (fNB)
				{
					return m_InnerArrayList.GetEnumerator(index, count);
				}
			}

			public override void AddRange(ICollection c)
			{
				lock (fNB)
				{
					m_InnerArrayList.AddRange(c);
				}
			}

			public override int BinarySearch(object value)
			{
				lock (fNB)
				{
					return m_InnerArrayList.BinarySearch(value);
				}
			}

			public override int BinarySearch(object value, IComparer comparer)
			{
				lock (fNB)
				{
					return m_InnerArrayList.BinarySearch(value, comparer);
				}
			}

			public override int BinarySearch(int index, int count, object value, IComparer comparer)
			{
				lock (fNB)
				{
					return m_InnerArrayList.BinarySearch(index, count, value, comparer);
				}
			}

			public override object Clone()
			{
				lock (fNB)
				{
					return m_InnerArrayList.Clone();
				}
			}

			public override B GetRange(int index, int count)
			{
				lock (fNB)
				{
					return m_InnerArrayList.GetRange(index, count);
				}
			}

			public override void TrimToSize()
			{
				lock (fNB)
				{
					m_InnerArrayList.TrimToSize();
				}
			}

			public override void Sort()
			{
				lock (fNB)
				{
					m_InnerArrayList.Sort();
				}
			}

			public override void Sort(IComparer comparer)
			{
				lock (fNB)
				{
					m_InnerArrayList.Sort(comparer);
				}
			}

			public override void Sort(int index, int count, IComparer comparer)
			{
				lock (fNB)
				{
					m_InnerArrayList.Sort(index, count, comparer);
				}
			}

			public override object[] ToArray()
			{
				lock (fNB)
				{
					return m_InnerArrayList.ToArray();
				}
			}

			public override Array ToArray(Type elementType)
			{
				lock (fNB)
				{
					return m_InnerArrayList.ToArray(elementType);
				}
			}
		}

		[Serializable]
		private class nNn : nNf
		{
			protected virtual string ErrorMessage => "Can't add or remove from a fixed-size list.";

			public override int Capacity
			{
				get
				{
					return base.Capacity;
				}
				set
				{
					throw new NotSupportedException(ErrorMessage);
				}
			}

			public override bool IsFixedSize => true;

			public nNn(B innerList)
				: base(innerList)
			{
			}

			public override int Add(object value)
			{
				throw new NotSupportedException(ErrorMessage);
			}

			public override void AddRange(ICollection c)
			{
				throw new NotSupportedException(ErrorMessage);
			}

			public override void Clear()
			{
				throw new NotSupportedException(ErrorMessage);
			}

			public override void Insert(int index, object value)
			{
				throw new NotSupportedException(ErrorMessage);
			}

			public override void InsertRange(int index, ICollection c)
			{
				throw new NotSupportedException(ErrorMessage);
			}

			public override void Remove(object value)
			{
				throw new NotSupportedException(ErrorMessage);
			}

			public override void RemoveAt(int index)
			{
				throw new NotSupportedException(ErrorMessage);
			}

			public override void RemoveRange(int index, int count)
			{
				throw new NotSupportedException(ErrorMessage);
			}

			public override void TrimToSize()
			{
				throw new NotSupportedException(ErrorMessage);
			}
		}

		[Serializable]
		private sealed class nN_0019 : nNn
		{
			protected override string ErrorMessage => "Can't modify a readonly list.";

			public override bool IsReadOnly => true;

			public override object this[int index]
			{
				get
				{
					return m_InnerArrayList[index];
				}
				set
				{
					throw new NotSupportedException(ErrorMessage);
				}
			}

			public nN_0019(B innerArrayList)
				: base(innerArrayList)
			{
			}

			public override void Reverse()
			{
				throw new NotSupportedException(ErrorMessage);
			}

			public override void Reverse(int index, int count)
			{
				throw new NotSupportedException(ErrorMessage);
			}

			public override void SetRange(int index, ICollection c)
			{
				throw new NotSupportedException(ErrorMessage);
			}

			public override void Sort()
			{
				throw new NotSupportedException(ErrorMessage);
			}

			public override void Sort(IComparer comparer)
			{
				throw new NotSupportedException(ErrorMessage);
			}

			public override void Sort(int index, int count, IComparer comparer)
			{
				throw new NotSupportedException(ErrorMessage);
			}
		}

		[Serializable]
		private sealed class nNL : nNf
		{
			private new int fNB;

			private new int fNa;

			private new int fNg;

			public override bool IsSynchronized => false;

			public override object this[int index]
			{
				get
				{
					if (index < 0 || index > fNa)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					return m_InnerArrayList[fNB + index];
				}
				set
				{
					if (index < 0 || index > fNa)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					m_InnerArrayList[fNB + index] = value;
				}
			}

			public override int Count
			{
				get
				{
					_0003_0017();
					return fNa;
				}
			}

			public override int Capacity
			{
				get
				{
					return m_InnerArrayList.Capacity;
				}
				set
				{
					if (value < fNa)
					{
						throw new ArgumentOutOfRangeException();
					}
				}
			}

			public nNL(B innerList, int index, int count)
				: base(innerList)
			{
				fNB = index;
				fNa = count;
				fNg = innerList.fNf;
			}

			private void _0003_0017()
			{
				if (fNg != m_InnerArrayList.fNf)
				{
					throw new InvalidOperationException("ArrayList view is invalid because the underlying ArrayList was modified.");
				}
			}

			public override int Add(object value)
			{
				_0003_0017();
				m_InnerArrayList.Insert(fNB + fNa, value);
				fNg = m_InnerArrayList.fNf;
				return ++fNa;
			}

			public override void Clear()
			{
				_0003_0017();
				m_InnerArrayList.RemoveRange(fNB, fNa);
				fNa = 0;
				fNg = m_InnerArrayList.fNf;
			}

			public override bool Contains(object value)
			{
				return m_InnerArrayList._0003w(value, fNB, fNa);
			}

			public override int IndexOf(object value)
			{
				return IndexOf(value, 0);
			}

			public override int IndexOf(object value, int startIndex)
			{
				return IndexOf(value, startIndex, fNa - startIndex);
			}

			public override int IndexOf(object value, int startIndex, int count)
			{
				if (startIndex < 0 || startIndex > fNa)
				{
					_0003_0011("startIndex", startIndex, "Does not specify valid index.");
				}
				if (count < 0)
				{
					_0003_0011("count", count, "Can't be less than 0.");
				}
				if (startIndex > fNa - count)
				{
					throw new ArgumentOutOfRangeException("count", "Start index and count do not specify a valid range.");
				}
				int num = m_InnerArrayList.IndexOf(value, fNB + startIndex, count);
				if (num == -1)
				{
					return -1;
				}
				return num - fNB;
			}

			public override int LastIndexOf(object value)
			{
				return LastIndexOf(value, fNa - 1);
			}

			public override int LastIndexOf(object value, int startIndex)
			{
				return LastIndexOf(value, startIndex, startIndex + 1);
			}

			public override int LastIndexOf(object value, int startIndex, int count)
			{
				if (startIndex < 0)
				{
					_0003_0011("startIndex", startIndex, "< 0");
				}
				if (count < 0)
				{
					_0003_0011("count", count, "count is negative.");
				}
				int num = m_InnerArrayList.LastIndexOf(value, fNB + startIndex, count);
				if (num == -1)
				{
					return -1;
				}
				return num - fNB;
			}

			public override void Insert(int index, object value)
			{
				_0003_0017();
				if (index < 0 || index > fNa)
				{
					_0003_0011("index", index, "Index must be >= 0 and <= Count.");
				}
				m_InnerArrayList.Insert(fNB + index, value);
				fNa++;
				fNg = m_InnerArrayList.fNf;
			}

			public override void InsertRange(int index, ICollection c)
			{
				_0003_0017();
				if (index < 0 || index > fNa)
				{
					_0003_0011("index", index, "Index must be >= 0 and <= Count.");
				}
				m_InnerArrayList.InsertRange(fNB + index, c);
				fNa += c.Count;
				fNg = m_InnerArrayList.fNf;
			}

			public override void Remove(object value)
			{
				_0003_0017();
				int num = IndexOf(value);
				if (num > -1)
				{
					RemoveAt(num);
				}
				fNg = m_InnerArrayList.fNf;
			}

			public override void RemoveAt(int index)
			{
				_0003_0017();
				if (index < 0 || index > fNa)
				{
					_0003_0011("index", index, "Index must be >= 0 and <= Count.");
				}
				m_InnerArrayList.RemoveAt(fNB + index);
				fNa--;
				fNg = m_InnerArrayList.fNf;
			}

			public override void RemoveRange(int index, int count)
			{
				_0003_0017();
				_0003s(index, count, fNa);
				m_InnerArrayList.RemoveRange(fNB + index, count);
				fNa -= count;
				fNg = m_InnerArrayList.fNf;
			}

			public override void Reverse()
			{
				Reverse(0, fNa);
			}

			public override void Reverse(int index, int count)
			{
				_0003_0017();
				_0003s(index, count, fNa);
				m_InnerArrayList.Reverse(fNB + index, count);
				fNg = m_InnerArrayList.fNf;
			}

			public override void SetRange(int index, ICollection c)
			{
				_0003_0017();
				if (index < 0 || index > fNa)
				{
					_0003_0011("index", index, "Index must be >= 0 and <= Count.");
				}
				m_InnerArrayList.SetRange(fNB + index, c);
				fNg = m_InnerArrayList.fNf;
			}

			public override void CopyTo(Array array)
			{
				CopyTo(array, 0);
			}

			public override void CopyTo(Array array, int index)
			{
				CopyTo(0, array, index, fNa);
			}

			public override void CopyTo(int index, Array array, int arrayIndex, int count)
			{
				_0003s(index, count, fNa);
				m_InnerArrayList.CopyTo(fNB + index, array, arrayIndex, count);
			}

			public override IEnumerator GetEnumerator()
			{
				return GetEnumerator(0, fNa);
			}

			public override IEnumerator GetEnumerator(int index, int count)
			{
				_0003s(index, count, fNa);
				return m_InnerArrayList.GetEnumerator(fNB + index, count);
			}

			public override void AddRange(ICollection c)
			{
				_0003_0017();
				m_InnerArrayList.InsertRange(fNa, c);
				fNa += c.Count;
				fNg = m_InnerArrayList.fNf;
			}

			public override int BinarySearch(object value)
			{
				return BinarySearch(0, fNa, value, a.Default);
			}

			public override int BinarySearch(object value, IComparer comparer)
			{
				return BinarySearch(0, fNa, value, comparer);
			}

			public override int BinarySearch(int index, int count, object value, IComparer comparer)
			{
				_0003s(index, count, fNa);
				return m_InnerArrayList.BinarySearch(fNB + index, count, value, comparer);
			}

			public override object Clone()
			{
				return new nNL((B)m_InnerArrayList.Clone(), fNB, fNa);
			}

			public override B GetRange(int index, int count)
			{
				_0003s(index, count, fNa);
				return new nNL(this, index, count);
			}

			public override void TrimToSize()
			{
				throw new NotSupportedException();
			}

			public override void Sort()
			{
				Sort(a.Default);
			}

			public override void Sort(IComparer comparer)
			{
				Sort(0, fNa, comparer);
			}

			public override void Sort(int index, int count, IComparer comparer)
			{
				_0003_0017();
				_0003s(index, count, fNa);
				m_InnerArrayList.Sort(fNB + index, count, comparer);
				fNg = m_InnerArrayList.fNf;
			}

			public override object[] ToArray()
			{
				object[] array = new object[fNa];
				m_InnerArrayList.CopyTo(fNB, array, 0, fNa);
				return array;
			}

			public override Array ToArray(Type elementType)
			{
				Array array = Array.CreateInstance(elementType, fNa);
				m_InnerArrayList.CopyTo(fNB, array, 0, fNa);
				return array;
			}
		}

		[Serializable]
		private class nNv : IList, ICollection, IEnumerable
		{
			protected IList m_InnerList;

			public virtual object this[int index]
			{
				get
				{
					return m_InnerList[index];
				}
				set
				{
					m_InnerList[index] = value;
				}
			}

			public virtual int Count => m_InnerList.Count;

			public virtual bool IsSynchronized => m_InnerList.IsSynchronized;

			public virtual object SyncRoot => m_InnerList.SyncRoot;

			public virtual bool IsFixedSize => m_InnerList.IsFixedSize;

			public virtual bool IsReadOnly => m_InnerList.IsReadOnly;

			public nNv(IList innerList)
			{
				m_InnerList = innerList;
			}

			public virtual int Add(object value)
			{
				return m_InnerList.Add(value);
			}

			public virtual void Clear()
			{
				m_InnerList.Clear();
			}

			public virtual bool Contains(object value)
			{
				return m_InnerList.Contains(value);
			}

			public virtual int IndexOf(object value)
			{
				return m_InnerList.IndexOf(value);
			}

			public virtual void Insert(int index, object value)
			{
				m_InnerList.Insert(index, value);
			}

			public virtual void Remove(object value)
			{
				m_InnerList.Remove(value);
			}

			public virtual void RemoveAt(int index)
			{
				m_InnerList.RemoveAt(index);
			}

			public virtual void CopyTo(Array array, int index)
			{
				m_InnerList.CopyTo(array, index);
			}

			public virtual IEnumerator GetEnumerator()
			{
				return m_InnerList.GetEnumerator();
			}
		}

		[Serializable]
		private sealed class nN_0012 : nNv
		{
			private object fNB;

			public override int Count
			{
				get
				{
					lock (fNB)
					{
						return m_InnerList.Count;
					}
				}
			}

			public override bool IsSynchronized => true;

			public override object SyncRoot
			{
				get
				{
					lock (fNB)
					{
						return m_InnerList.SyncRoot;
					}
				}
			}

			public override bool IsFixedSize
			{
				get
				{
					lock (fNB)
					{
						return m_InnerList.IsFixedSize;
					}
				}
			}

			public override bool IsReadOnly
			{
				get
				{
					lock (fNB)
					{
						return m_InnerList.IsReadOnly;
					}
				}
			}

			public override object this[int index]
			{
				get
				{
					lock (fNB)
					{
						return m_InnerList[index];
					}
				}
				set
				{
					lock (fNB)
					{
						m_InnerList[index] = value;
					}
				}
			}

			public nN_0012(IList innerList)
				: base(innerList)
			{
				fNB = innerList.SyncRoot;
			}

			public override int Add(object value)
			{
				lock (fNB)
				{
					return m_InnerList.Add(value);
				}
			}

			public override void Clear()
			{
				lock (fNB)
				{
					m_InnerList.Clear();
				}
			}

			public override bool Contains(object value)
			{
				lock (fNB)
				{
					return m_InnerList.Contains(value);
				}
			}

			public override int IndexOf(object value)
			{
				lock (fNB)
				{
					return m_InnerList.IndexOf(value);
				}
			}

			public override void Insert(int index, object value)
			{
				lock (fNB)
				{
					m_InnerList.Insert(index, value);
				}
			}

			public override void Remove(object value)
			{
				lock (fNB)
				{
					m_InnerList.Remove(value);
				}
			}

			public override void RemoveAt(int index)
			{
				lock (fNB)
				{
					m_InnerList.RemoveAt(index);
				}
			}

			public override void CopyTo(Array array, int index)
			{
				lock (fNB)
				{
					m_InnerList.CopyTo(array, index);
				}
			}

			public override IEnumerator GetEnumerator()
			{
				lock (fNB)
				{
					return m_InnerList.GetEnumerator();
				}
			}
		}

		[Serializable]
		private class nN_0015 : nNv
		{
			protected virtual string ErrorMessage => "List is fixed-size.";

			public override bool IsFixedSize => true;

			public nN_0015(IList innerList)
				: base(innerList)
			{
			}

			public override int Add(object value)
			{
				throw new NotSupportedException(ErrorMessage);
			}

			public override void Clear()
			{
				throw new NotSupportedException(ErrorMessage);
			}

			public override void Insert(int index, object value)
			{
				throw new NotSupportedException(ErrorMessage);
			}

			public override void Remove(object value)
			{
				throw new NotSupportedException(ErrorMessage);
			}

			public override void RemoveAt(int index)
			{
				throw new NotSupportedException(ErrorMessage);
			}
		}

		[Serializable]
		private sealed class nN8 : nN_0015
		{
			protected override string ErrorMessage => "List is read-only.";

			public override bool IsReadOnly => true;

			public override object this[int index]
			{
				get
				{
					return m_InnerList[index];
				}
				set
				{
					throw new NotSupportedException(ErrorMessage);
				}
			}

			public nN8(IList innerList)
				: base(innerList)
			{
			}
		}

		private const int fNB = 16;

		private int fNa;

		private object[] fNg;

		private int fNf;

		public virtual object this[int index]
		{
			get
			{
				if (index < 0 || index >= fNa)
				{
					_0003_0011("index", index, "Index is less than 0 or more than or equal to the list count.");
				}
				return fNg[index];
			}
			set
			{
				if (index < 0 || index >= fNa)
				{
					_0003_0011("index", index, "Index is less than 0 or more than or equal to the list count.");
				}
				fNg[index] = value;
				fNf++;
			}
		}

		public virtual int Count => fNa;

		public virtual int Capacity
		{
			get
			{
				return fNg.Length;
			}
			set
			{
				if (value < fNa)
				{
					_0003_0011("Capacity", value, "Must be more than count.");
				}
				object[] destinationArray = new object[value];
				Array.Copy(fNg, 0, destinationArray, 0, fNa);
				fNg = destinationArray;
			}
		}

		public virtual bool IsFixedSize => false;

		public virtual bool IsReadOnly => false;

		public virtual bool IsSynchronized => false;

		public virtual object SyncRoot => this;

		public B()
		{
			fNg = new object[16];
		}

		public B(ICollection c)
		{
			if (c == null)
			{
				throw new ArgumentNullException("c");
			}
			if (c is Array { Rank: not 1 })
			{
				throw new RankException();
			}
			fNg = new object[c.Count];
			AddRange(c);
		}

		public B(int capacity)
		{
			if (capacity < 0)
			{
				_0003_0011("capacity", capacity, "The initial capacity can't be smaller than zero.");
			}
			if (capacity == 0)
			{
				capacity = 16;
			}
			fNg = new object[capacity];
		}

		private B(int P_0, bool P_1)
		{
			if (P_1)
			{
				fNg = null;
				return;
			}
			throw new InvalidOperationException("Use ArrayList(int)");
		}

		private B(object[] P_0, int P_1, int P_2)
		{
			if (P_2 == 0)
			{
				fNg = new object[16];
			}
			else
			{
				fNg = new object[P_2];
			}
			Array.Copy(P_0, P_1, fNg, 0, P_2);
			fNa = P_2;
		}

		private void _0003c(int P_0)
		{
			if (P_0 > fNg.Length)
			{
				int num = fNg.Length << 1;
				if (num == 0)
				{
					num = 16;
				}
				while (num < P_0)
				{
					num <<= 1;
				}
				object[] destinationArray = new object[num];
				Array.Copy(fNg, 0, destinationArray, 0, fNg.Length);
				fNg = destinationArray;
			}
		}

		private void _0003d(int P_0, int P_1)
		{
			if (P_1 > 0)
			{
				if (fNa + P_1 > fNg.Length)
				{
					int num;
					for (num = ((fNg.Length <= 0) ? 1 : (fNg.Length << 1)); num < fNa + P_1; num <<= 1)
					{
					}
					object[] destinationArray = new object[num];
					Array.Copy(fNg, 0, destinationArray, 0, P_0);
					Array.Copy(fNg, P_0, destinationArray, P_0 + P_1, fNa - P_0);
					fNg = destinationArray;
				}
				else
				{
					Array.Copy(fNg, P_0, fNg, P_0 + P_1, fNa - P_0);
				}
			}
			else if (P_1 < 0)
			{
				int num2 = P_0 - P_1;
				Array.Copy(fNg, num2, fNg, P_0, fNa - num2);
				Array.Clear(fNg, fNa + P_1, -P_1);
			}
		}

		public virtual int Add(object value)
		{
			if (fNg.Length <= fNa)
			{
				_0003c(fNa + 1);
			}
			fNg[fNa] = value;
			fNf++;
			return fNa++;
		}

		public virtual void Clear()
		{
			Array.Clear(fNg, 0, fNa);
			fNa = 0;
			fNf++;
		}

		public virtual bool Contains(object item)
		{
			return IndexOf(item, 0, fNa) > -1;
		}

		internal virtual bool _0003w(object P_0, int P_1, int P_2)
		{
			return IndexOf(P_0, P_1, P_2) > -1;
		}

		public virtual int IndexOf(object value)
		{
			return IndexOf(value, 0);
		}

		public virtual int IndexOf(object value, int startIndex)
		{
			return IndexOf(value, startIndex, fNa - startIndex);
		}

		public virtual int IndexOf(object value, int startIndex, int count)
		{
			if (startIndex < 0 || startIndex > fNa)
			{
				_0003_0011("startIndex", startIndex, "Does not specify valid index.");
			}
			if (count < 0)
			{
				_0003_0011("count", count, "Can't be less than 0.");
			}
			if (startIndex > fNa - count)
			{
				throw new ArgumentOutOfRangeException("count", "Start index and count do not specify a valid range.");
			}
			return Array.IndexOf(fNg, value, startIndex, count);
		}

		public virtual int LastIndexOf(object value)
		{
			return LastIndexOf(value, fNa - 1);
		}

		public virtual int LastIndexOf(object value, int startIndex)
		{
			return LastIndexOf(value, startIndex, startIndex + 1);
		}

		public virtual int LastIndexOf(object value, int startIndex, int count)
		{
			return Array.LastIndexOf(fNg, value, startIndex, count);
		}

		public virtual void Insert(int index, object value)
		{
			if (index < 0 || index > fNa)
			{
				_0003_0011("index", index, "Index must be >= 0 and <= Count.");
			}
			_0003d(index, 1);
			fNg[index] = value;
			fNa++;
			fNf++;
		}

		public virtual void InsertRange(int index, ICollection c)
		{
			if (c == null)
			{
				throw new ArgumentNullException("c");
			}
			if (index < 0 || index > fNa)
			{
				_0003_0011("index", index, "Index must be >= 0 and <= Count.");
			}
			int count = c.Count;
			if (fNg.Length < fNa + count)
			{
				_0003c(fNa + count);
			}
			if (index < fNa)
			{
				Array.Copy(fNg, index, fNg, index + count, fNa - index);
			}
			if (this == c.SyncRoot)
			{
				Array.Copy(fNg, 0, fNg, index, index);
				Array.Copy(fNg, index + count, fNg, index << 1, fNa - index);
			}
			else
			{
				c.CopyTo(fNg, index);
			}
			fNa += c.Count;
			fNf++;
		}

		public virtual void Remove(object obj)
		{
			int num = IndexOf(obj);
			if (num > -1)
			{
				RemoveAt(num);
			}
			fNf++;
		}

		public virtual void RemoveAt(int index)
		{
			if (index < 0 || index >= fNa)
			{
				_0003_0011("index", index, "Less than 0 or more than list count.");
			}
			_0003d(index, -1);
			fNa--;
			fNf++;
		}

		public virtual void RemoveRange(int index, int count)
		{
			_0003s(index, count, fNa);
			_0003d(index, -count);
			fNa -= count;
			fNf++;
		}

		public virtual void Reverse()
		{
			Array.Reverse(fNg, 0, fNa);
			fNf++;
		}

		public virtual void Reverse(int index, int count)
		{
			_0003s(index, count, fNa);
			Array.Reverse(fNg, index, count);
			fNf++;
		}

		public virtual void CopyTo(Array array)
		{
			Array.Copy(fNg, array, fNa);
		}

		public virtual void CopyTo(Array array, int arrayIndex)
		{
			CopyTo(0, array, arrayIndex, fNa);
		}

		public virtual void CopyTo(int index, Array array, int arrayIndex, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException("Must have only 1 dimensions.", "array");
			}
			Array.Copy(fNg, index, array, arrayIndex, count);
		}

		public virtual IEnumerator GetEnumerator()
		{
			return new nNa(this);
		}

		public virtual IEnumerator GetEnumerator(int index, int count)
		{
			_0003s(index, count, fNa);
			return new nNB(this, index, count);
		}

		public virtual void AddRange(ICollection c)
		{
			InsertRange(fNa, c);
		}

		public virtual int BinarySearch(object value)
		{
			try
			{
				return Array.BinarySearch(fNg, 0, fNa, value);
			}
			catch (InvalidOperationException ex)
			{
				throw new ArgumentException(ex.Message);
			}
		}

		public virtual int BinarySearch(object value, IComparer comparer)
		{
			try
			{
				return Array.BinarySearch(fNg, 0, fNa, value, comparer);
			}
			catch (InvalidOperationException ex)
			{
				throw new ArgumentException(ex.Message);
			}
		}

		public virtual int BinarySearch(int index, int count, object value, IComparer comparer)
		{
			try
			{
				return Array.BinarySearch(fNg, index, count, value, comparer);
			}
			catch (InvalidOperationException ex)
			{
				throw new ArgumentException(ex.Message);
			}
		}

		public virtual B GetRange(int index, int count)
		{
			_0003s(index, count, fNa);
			if (IsSynchronized)
			{
				return Synchronized(new nNL(this, index, count));
			}
			return new nNL(this, index, count);
		}

		public virtual void SetRange(int index, ICollection c)
		{
			if (c == null)
			{
				throw new ArgumentNullException("c");
			}
			if (index < 0 || index + c.Count > fNa)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			c.CopyTo(fNg, index);
			fNf++;
		}

		public virtual void TrimToSize()
		{
			if (fNg.Length > fNa)
			{
				object[] destinationArray = ((fNa != 0) ? new object[fNa] : new object[16]);
				Array.Copy(fNg, 0, destinationArray, 0, fNa);
				fNg = destinationArray;
			}
		}

		public virtual void Sort()
		{
			Array.Sort(fNg, 0, fNa);
			fNf++;
		}

		public virtual void Sort(IComparer comparer)
		{
			Array.Sort(fNg, 0, fNa, comparer);
		}

		public virtual void Sort(int index, int count, IComparer comparer)
		{
			_0003s(index, count, fNa);
			Array.Sort(fNg, index, count, comparer);
		}

		public virtual object[] ToArray()
		{
			object[] array = new object[fNa];
			CopyTo(array);
			return array;
		}

		public virtual Array ToArray(Type type)
		{
			Array array = Array.CreateInstance(type, fNa);
			CopyTo(array);
			return array;
		}

		public virtual object Clone()
		{
			return new B(fNg, 0, fNa);
		}

		internal static void _0003s(int P_0, int P_1, int P_2)
		{
			if (P_0 < 0)
			{
				_0003_0011("index", P_0, "Can't be less than 0.");
			}
			if (P_1 < 0)
			{
				_0003_0011("count", P_1, "Can't be less than 0.");
			}
			if (P_0 > P_2 - P_1)
			{
				throw new ArgumentException("Index and count do not denote a valid range of elements.", "index");
			}
		}

		internal static void _0003_0011(string P_0, object P_1, string P_2)
		{
			throw new ArgumentOutOfRangeException(P_0, P_2);
		}

		public static B Adapter(IList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (list is B result)
			{
				return result;
			}
			B b = new nNg(list);
			if (list.IsSynchronized)
			{
				return Synchronized(b);
			}
			return b;
		}

		public static B Synchronized(B list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (list.IsSynchronized)
			{
				return list;
			}
			return new nN_0003(list);
		}

		public static IList Synchronized(IList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (list.IsSynchronized)
			{
				return list;
			}
			return new nN_0012(list);
		}

		public static B ReadOnly(B list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (list.IsReadOnly)
			{
				return list;
			}
			return new nN_0019(list);
		}

		public static IList ReadOnly(IList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (list.IsReadOnly)
			{
				return list;
			}
			return new nN8(list);
		}

		public static B FixedSize(B list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (list.IsFixedSize)
			{
				return list;
			}
			return new nNn(list);
		}

		public static IList FixedSize(IList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (list.IsFixedSize)
			{
				return list;
			}
			return new nN_0015(list);
		}

		public static B Repeat(object value, int count)
		{
			B b = new B(count);
			for (int k = 0; k < count; k++)
			{
				b.Add(value);
			}
			return b;
		}
	}
}
namespace _0013
{
	internal class B
	{
		internal enum nNB
		{
			Note,
			Warning,
			Error
		}

		internal static void nO(string P_0, nNB P_1)
		{
			switch (P_1)
			{
			}
		}

		internal static void nu<T>(ref T P_0) where T : IDisposable
		{
			if (P_0 != null)
			{
				P_0.Dispose();
				P_0 = default;
			}
		}
	}
}
