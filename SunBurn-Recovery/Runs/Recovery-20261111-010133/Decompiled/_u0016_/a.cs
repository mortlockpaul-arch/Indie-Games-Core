using System;
using System.Collections;
using f;

namespace _u0016_;

[Serializable]
internal class a : IList, f.a, ICollection, IEnumerable
{
	private sealed class w8a : IEnumerator, f.a
	{
		private int l8a;

		private int l8_u0013_;

		private int l8N;

		private object l8l;

		private a l8L;

		private int l8w;

		public object Current
		{
			get
			{
				if (l8a == l8_u0013_ - 1)
				{
					throw new InvalidOperationException("Enumerator unusable (Reset pending, or past end of array.");
				}
				return l8l;
			}
		}

		public w8a(a list)
			: this(list, 0, list.Count)
		{
		}

		public object Clone()
		{
			return MemberwiseClone();
		}

		public w8a(a list, int index, int count)
		{
			l8L = list;
			l8_u0013_ = index;
			l8N = count;
			l8a = l8_u0013_ - 1;
			l8l = null;
			l8w = list.l8l;
		}

		public bool MoveNext()
		{
			if (l8L.l8l != l8w)
			{
				throw new InvalidOperationException("List has changed.");
			}
			l8a++;
			if (l8a - l8_u0013_ < l8N)
			{
				l8l = l8L[l8a];
				return true;
			}
			return false;
		}

		public void Reset()
		{
			l8l = null;
			l8a = l8_u0013_ - 1;
		}
	}

	private sealed class w8_u0013_ : IEnumerator, f.a
	{
		private a l8a;

		private int l8_u0013_;

		private int l8N;

		private object l8l;

		private static object l8L = new object();

		public object Current
		{
			get
			{
				if (l8l == l8L)
				{
					if (l8_u0013_ == -1)
					{
						throw new InvalidOperationException("Enumerator not started");
					}
					throw new InvalidOperationException("Enumerator ended");
				}
				return l8l;
			}
		}

		public w8_u0013_(a list)
		{
			l8a = list;
			l8_u0013_ = -1;
			l8N = list.l8l;
			l8l = l8L;
		}

		public object Clone()
		{
			return MemberwiseClone();
		}

		public bool MoveNext()
		{
			if (l8N != l8a.l8l)
			{
				throw new InvalidOperationException("List has changed.");
			}
			if (++l8_u0013_ < l8a.Count)
			{
				l8l = l8a[l8_u0013_];
				return true;
			}
			l8l = l8L;
			return false;
		}

		public void Reset()
		{
			if (l8N != l8a.l8l)
			{
				throw new InvalidOperationException("List has changed.");
			}
			l8l = l8L;
			l8_u0013_ = -1;
		}
	}

	[Serializable]
	private sealed class w8N : a
	{
		private new sealed class w8a : IEnumerator, f.a
		{
			private int l8a;

			private int l8_u0013_;

			private int l8N;

			private IEnumerator l8l;

			public object Current => l8l.Current;

			public w8a(IEnumerator enumerator, int index, int count)
			{
				l8_u0013_ = 0;
				l8a = index;
				l8N = count;
				l8l = enumerator;
				Reset();
			}

			public object Clone()
			{
				return MemberwiseClone();
			}

			public bool MoveNext()
			{
				if (l8_u0013_ >= l8N)
				{
					return false;
				}
				l8_u0013_++;
				return l8l.MoveNext();
			}

			public void Reset()
			{
				l8_u0013_ = 0;
				l8l.Reset();
				for (int i = 0; i < l8a; i++)
				{
					l8l.MoveNext();
				}
			}
		}

		private new IList l8a;

		public override object this[int index]
		{
			get
			{
				return l8a[index];
			}
			set
			{
				l8a[index] = value;
			}
		}

		public override int Count => l8a.Count;

		public override int Capacity
		{
			get
			{
				return l8a.Count;
			}
			set
			{
				if (value < l8a.Count)
				{
					throw new ArgumentException("capacity");
				}
			}
		}

		public override bool IsFixedSize => l8a.IsFixedSize;

		public override bool IsReadOnly => l8a.IsReadOnly;

		public override object SyncRoot => l8a.SyncRoot;

		public override bool IsSynchronized => l8a.IsSynchronized;

		public w8N(IList adaptee)
			: base(0, param_1: true)
		{
			l8a = adaptee;
		}

		public override int Add(object value)
		{
			return l8a.Add(value);
		}

		public override void Clear()
		{
			l8a.Clear();
		}

		public override bool Contains(object value)
		{
			return l8a.Contains(value);
		}

		public override int IndexOf(object value)
		{
			return l8a.IndexOf(value);
		}

		public override int IndexOf(object value, int startIndex)
		{
			return IndexOf(value, startIndex, l8a.Count - startIndex);
		}

		public override int IndexOf(object value, int startIndex, int count)
		{
			if (startIndex < 0 || startIndex > l8a.Count)
			{
				ln("startIndex", startIndex, "Does not specify valid index.");
			}
			if (count < 0)
			{
				ln("count", count, "Can't be less than 0.");
			}
			if (startIndex > l8a.Count - count)
			{
				throw new ArgumentOutOfRangeException("count", "Start index and count do not specify a valid range.");
			}
			if (value == null)
			{
				for (int i = startIndex; i < startIndex + count; i++)
				{
					if (l8a[i] == null)
					{
						return i;
					}
				}
			}
			else
			{
				for (int j = startIndex; j < startIndex + count; j++)
				{
					if (value.Equals(l8a[j]))
					{
						return j;
					}
				}
			}
			return -1;
		}

		public override int LastIndexOf(object value)
		{
			return LastIndexOf(value, l8a.Count - 1);
		}

		public override int LastIndexOf(object value, int startIndex)
		{
			return LastIndexOf(value, startIndex, startIndex + 1);
		}

		public override int LastIndexOf(object value, int startIndex, int count)
		{
			if (startIndex < 0)
			{
				ln("startIndex", startIndex, "< 0");
			}
			if (count < 0)
			{
				ln("count", count, "count is negative.");
			}
			if (startIndex - count + 1 < 0)
			{
				ln("count", count, "count is too large.");
			}
			if (value == null)
			{
				for (int num = startIndex; num > startIndex - count; num--)
				{
					if (l8a[num] == null)
					{
						return num;
					}
				}
			}
			else
			{
				for (int num2 = startIndex; num2 > startIndex - count; num2--)
				{
					if (value.Equals(l8a[num2]))
					{
						return num2;
					}
				}
			}
			return -1;
		}

		public override void Insert(int index, object value)
		{
			l8a.Insert(index, value);
		}

		public override void InsertRange(int index, ICollection c)
		{
			if (c == null)
			{
				throw new ArgumentNullException("c");
			}
			if (index > l8a.Count)
			{
				ln("index", index, "Index must be >= 0 and <= Count.");
			}
			foreach (object item in c)
			{
				l8a.Insert(index++, item);
			}
		}

		public override void Remove(object value)
		{
			l8a.Remove(value);
		}

		public override void RemoveAt(int index)
		{
			l8a.RemoveAt(index);
		}

		public override void RemoveRange(int index, int count)
		{
			lm(index, count, l8a.Count);
			for (int i = 0; i < count; i++)
			{
				l8a.RemoveAt(index);
			}
		}

		public override void Reverse()
		{
			Reverse(0, l8a.Count);
		}

		public override void Reverse(int index, int count)
		{
			lm(index, count, l8a.Count);
			for (int i = 0; i < count / 2; i++)
			{
				object value = l8a[i + index];
				l8a[i + index] = l8a[index + count - i + index - 1];
				l8a[index + count - i + index - 1] = value;
			}
		}

		public override void SetRange(int index, ICollection c)
		{
			if (c == null)
			{
				throw new ArgumentNullException("c");
			}
			if (index < 0 || index + c.Count > l8a.Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			int num = index;
			foreach (object item in c)
			{
				l8a[num++] = item;
			}
		}

		public override void CopyTo(Array array)
		{
			l8a.CopyTo(array, 0);
		}

		public override void CopyTo(Array array, int index)
		{
			l8a.CopyTo(array, index);
		}

		public override void CopyTo(int index, Array array, int arrayIndex, int count)
		{
			if (index < 0)
			{
				ln("index", index, "Can't be less than zero.");
			}
			if (arrayIndex < 0)
			{
				ln("arrayIndex", arrayIndex, "Can't be less than zero.");
			}
			if (count < 0)
			{
				ln("index", index, "Can't be less than zero.");
			}
			if (index >= l8a.Count)
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
			if (index > l8a.Count - count)
			{
				throw new ArgumentException("Index and count do not denote a valid range of elements.", "index");
			}
			for (int i = 0; i < count; i++)
			{
				array.SetValue(l8a[index + i], arrayIndex + i);
			}
		}

		public override IEnumerator GetEnumerator()
		{
			return l8a.GetEnumerator();
		}

		public override IEnumerator GetEnumerator(int index, int count)
		{
			lm(index, count, l8a.Count);
			return new w8a(l8a.GetEnumerator(), index, count);
		}

		public override void AddRange(ICollection c)
		{
			foreach (object item in c)
			{
				l8a.Add(item);
			}
		}

		public override int BinarySearch(object value)
		{
			return BinarySearch(value, null);
		}

		public override int BinarySearch(object value, IComparer comparer)
		{
			return BinarySearch(0, l8a.Count, value, comparer);
		}

		public override int BinarySearch(int index, int count, object value, IComparer comparer)
		{
			lm(index, count, l8a.Count);
			if (comparer == null)
			{
				comparer = _u0013_.Default;
			}
			int num = index;
			int num2 = index + count - 1;
			while (num <= num2)
			{
				int num3 = num + (num2 - num) / 2;
				int num4 = comparer.Compare(value, l8a[num3]);
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
			return new w8N(l8a);
		}

		public override a GetRange(int index, int count)
		{
			lm(index, count, l8a.Count);
			return new w8B(this, index, count);
		}

		public override void TrimToSize()
		{
		}

		public override void Sort()
		{
			Sort(_u0013_.Default);
		}

		public override void Sort(IComparer comparer)
		{
			Sort(0, l8a.Count, comparer);
		}

		public override void Sort(int index, int count, IComparer comparer)
		{
			lm(index, count, l8a.Count);
			if (comparer == null)
			{
				comparer = _u0013_.Default;
			}
			lP(l8a, index, index + count - 1, comparer);
		}

		private static void lk(IList param_0, int param_1, int param_2)
		{
			object value = param_0[param_1];
			param_0[param_1] = param_0[param_2];
			param_0[param_2] = value;
		}

		internal static void lP(IList param_0, int param_1, int param_2, IComparer param_3)
		{
			if (param_1 >= param_2)
			{
				return;
			}
			int num = param_1 + (param_2 - param_1) / 2;
			if (param_3.Compare(param_0[num], param_0[param_1]) < 0)
			{
				lk(param_0, num, param_1);
			}
			if (param_3.Compare(param_0[param_2], param_0[param_1]) < 0)
			{
				lk(param_0, param_2, param_1);
			}
			if (param_3.Compare(param_0[param_2], param_0[num]) < 0)
			{
				lk(param_0, param_2, num);
			}
			if (param_2 - param_1 + 1 <= 3)
			{
				return;
			}
			lk(param_0, param_2 - 1, num);
			object y = param_0[param_2 - 1];
			int num2 = param_1;
			int num3 = param_2 - 1;
			while (true)
			{
				if (param_3.Compare(param_0[++num2], y) >= 0)
				{
					while (param_3.Compare(param_0[--num3], y) > 0)
					{
					}
					if (num2 >= num3)
					{
						break;
					}
					lk(param_0, num2, num3);
				}
			}
			lk(param_0, param_2 - 1, num2);
			lP(param_0, param_1, num2 - 1, param_3);
			lP(param_0, num2 + 1, param_2, param_3);
		}

		public override object[] ToArray()
		{
			object[] array = new object[l8a.Count];
			l8a.CopyTo(array, 0);
			return array;
		}

		public override Array ToArray(Type elementType)
		{
			Array array = Array.CreateInstance(elementType, l8a.Count);
			l8a.CopyTo(array, 0);
			return array;
		}
	}

	[Serializable]
	private class w8l : a
	{
		protected a m_InnerArrayList;

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

		public w8l(a innerArrayList)
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

		public override a GetRange(int index, int count)
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
	private sealed class w8L : w8l
	{
		private new object l8a;

		public override object this[int index]
		{
			get
			{
				lock (l8a)
				{
					return m_InnerArrayList[index];
				}
			}
			set
			{
				lock (l8a)
				{
					m_InnerArrayList[index] = value;
				}
			}
		}

		public override int Count
		{
			get
			{
				lock (l8a)
				{
					return m_InnerArrayList.Count;
				}
			}
		}

		public override int Capacity
		{
			get
			{
				lock (l8a)
				{
					return m_InnerArrayList.Capacity;
				}
			}
			set
			{
				lock (l8a)
				{
					m_InnerArrayList.Capacity = value;
				}
			}
		}

		public override bool IsFixedSize
		{
			get
			{
				lock (l8a)
				{
					return m_InnerArrayList.IsFixedSize;
				}
			}
		}

		public override bool IsReadOnly
		{
			get
			{
				lock (l8a)
				{
					return m_InnerArrayList.IsReadOnly;
				}
			}
		}

		public override bool IsSynchronized => true;

		public override object SyncRoot => l8a;

		internal w8L(a param_0)
			: base(param_0)
		{
			l8a = param_0.SyncRoot;
		}

		public override int Add(object value)
		{
			lock (l8a)
			{
				return m_InnerArrayList.Add(value);
			}
		}

		public override void Clear()
		{
			lock (l8a)
			{
				m_InnerArrayList.Clear();
			}
		}

		public override bool Contains(object value)
		{
			lock (l8a)
			{
				return m_InnerArrayList.Contains(value);
			}
		}

		public override int IndexOf(object value)
		{
			lock (l8a)
			{
				return m_InnerArrayList.IndexOf(value);
			}
		}

		public override int IndexOf(object value, int startIndex)
		{
			lock (l8a)
			{
				return m_InnerArrayList.IndexOf(value, startIndex);
			}
		}

		public override int IndexOf(object value, int startIndex, int count)
		{
			lock (l8a)
			{
				return m_InnerArrayList.IndexOf(value, startIndex, count);
			}
		}

		public override int LastIndexOf(object value)
		{
			lock (l8a)
			{
				return m_InnerArrayList.LastIndexOf(value);
			}
		}

		public override int LastIndexOf(object value, int startIndex)
		{
			lock (l8a)
			{
				return m_InnerArrayList.LastIndexOf(value, startIndex);
			}
		}

		public override int LastIndexOf(object value, int startIndex, int count)
		{
			lock (l8a)
			{
				return m_InnerArrayList.LastIndexOf(value, startIndex, count);
			}
		}

		public override void Insert(int index, object value)
		{
			lock (l8a)
			{
				m_InnerArrayList.Insert(index, value);
			}
		}

		public override void InsertRange(int index, ICollection c)
		{
			lock (l8a)
			{
				m_InnerArrayList.InsertRange(index, c);
			}
		}

		public override void Remove(object value)
		{
			lock (l8a)
			{
				m_InnerArrayList.Remove(value);
			}
		}

		public override void RemoveAt(int index)
		{
			lock (l8a)
			{
				m_InnerArrayList.RemoveAt(index);
			}
		}

		public override void RemoveRange(int index, int count)
		{
			lock (l8a)
			{
				m_InnerArrayList.RemoveRange(index, count);
			}
		}

		public override void Reverse()
		{
			lock (l8a)
			{
				m_InnerArrayList.Reverse();
			}
		}

		public override void Reverse(int index, int count)
		{
			lock (l8a)
			{
				m_InnerArrayList.Reverse(index, count);
			}
		}

		public override void CopyTo(Array array)
		{
			lock (l8a)
			{
				m_InnerArrayList.CopyTo(array);
			}
		}

		public override void CopyTo(Array array, int index)
		{
			lock (l8a)
			{
				m_InnerArrayList.CopyTo(array, index);
			}
		}

		public override void CopyTo(int index, Array array, int arrayIndex, int count)
		{
			lock (l8a)
			{
				m_InnerArrayList.CopyTo(index, array, arrayIndex, count);
			}
		}

		public override IEnumerator GetEnumerator()
		{
			lock (l8a)
			{
				return m_InnerArrayList.GetEnumerator();
			}
		}

		public override IEnumerator GetEnumerator(int index, int count)
		{
			lock (l8a)
			{
				return m_InnerArrayList.GetEnumerator(index, count);
			}
		}

		public override void AddRange(ICollection c)
		{
			lock (l8a)
			{
				m_InnerArrayList.AddRange(c);
			}
		}

		public override int BinarySearch(object value)
		{
			lock (l8a)
			{
				return m_InnerArrayList.BinarySearch(value);
			}
		}

		public override int BinarySearch(object value, IComparer comparer)
		{
			lock (l8a)
			{
				return m_InnerArrayList.BinarySearch(value, comparer);
			}
		}

		public override int BinarySearch(int index, int count, object value, IComparer comparer)
		{
			lock (l8a)
			{
				return m_InnerArrayList.BinarySearch(index, count, value, comparer);
			}
		}

		public override object Clone()
		{
			lock (l8a)
			{
				return m_InnerArrayList.Clone();
			}
		}

		public override a GetRange(int index, int count)
		{
			lock (l8a)
			{
				return m_InnerArrayList.GetRange(index, count);
			}
		}

		public override void TrimToSize()
		{
			lock (l8a)
			{
				m_InnerArrayList.TrimToSize();
			}
		}

		public override void Sort()
		{
			lock (l8a)
			{
				m_InnerArrayList.Sort();
			}
		}

		public override void Sort(IComparer comparer)
		{
			lock (l8a)
			{
				m_InnerArrayList.Sort(comparer);
			}
		}

		public override void Sort(int index, int count, IComparer comparer)
		{
			lock (l8a)
			{
				m_InnerArrayList.Sort(index, count, comparer);
			}
		}

		public override object[] ToArray()
		{
			lock (l8a)
			{
				return m_InnerArrayList.ToArray();
			}
		}

		public override Array ToArray(Type elementType)
		{
			lock (l8a)
			{
				return m_InnerArrayList.ToArray(elementType);
			}
		}
	}

	[Serializable]
	private class w8w : w8l
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

		public w8w(a innerList)
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
	private sealed class w8J : w8w
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

		public w8J(a innerArrayList)
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
	private sealed class w8B : w8l
	{
		private new int l8a;

		private new int l8_u0013_;

		private new int l8N;

		public override bool IsSynchronized => false;

		public override object this[int index]
		{
			get
			{
				if (index < 0 || index > l8_u0013_)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return m_InnerArrayList[l8a + index];
			}
			set
			{
				if (index < 0 || index > l8_u0013_)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				m_InnerArrayList[l8a + index] = value;
			}
		}

		public override int Count
		{
			get
			{
				lD();
				return l8_u0013_;
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
				if (value < l8_u0013_)
				{
					throw new ArgumentOutOfRangeException();
				}
			}
		}

		public w8B(a innerList, int index, int count)
			: base(innerList)
		{
			l8a = index;
			l8_u0013_ = count;
			l8N = innerList.l8l;
		}

		private void lD()
		{
			if (l8N != m_InnerArrayList.l8l)
			{
				throw new InvalidOperationException("ArrayList view is invalid because the underlying ArrayList was modified.");
			}
		}

		public override int Add(object value)
		{
			lD();
			m_InnerArrayList.Insert(l8a + l8_u0013_, value);
			l8N = m_InnerArrayList.l8l;
			return ++l8_u0013_;
		}

		public override void Clear()
		{
			lD();
			m_InnerArrayList.RemoveRange(l8a, l8_u0013_);
			l8_u0013_ = 0;
			l8N = m_InnerArrayList.l8l;
		}

		public override bool Contains(object value)
		{
			return m_InnerArrayList.l_u0003_(value, l8a, l8_u0013_);
		}

		public override int IndexOf(object value)
		{
			return IndexOf(value, 0);
		}

		public override int IndexOf(object value, int startIndex)
		{
			return IndexOf(value, startIndex, l8_u0013_ - startIndex);
		}

		public override int IndexOf(object value, int startIndex, int count)
		{
			if (startIndex < 0 || startIndex > l8_u0013_)
			{
				ln("startIndex", startIndex, "Does not specify valid index.");
			}
			if (count < 0)
			{
				ln("count", count, "Can't be less than 0.");
			}
			if (startIndex > l8_u0013_ - count)
			{
				throw new ArgumentOutOfRangeException("count", "Start index and count do not specify a valid range.");
			}
			int num = m_InnerArrayList.IndexOf(value, l8a + startIndex, count);
			if (num == -1)
			{
				return -1;
			}
			return num - l8a;
		}

		public override int LastIndexOf(object value)
		{
			return LastIndexOf(value, l8_u0013_ - 1);
		}

		public override int LastIndexOf(object value, int startIndex)
		{
			return LastIndexOf(value, startIndex, startIndex + 1);
		}

		public override int LastIndexOf(object value, int startIndex, int count)
		{
			if (startIndex < 0)
			{
				ln("startIndex", startIndex, "< 0");
			}
			if (count < 0)
			{
				ln("count", count, "count is negative.");
			}
			int num = m_InnerArrayList.LastIndexOf(value, l8a + startIndex, count);
			if (num == -1)
			{
				return -1;
			}
			return num - l8a;
		}

		public override void Insert(int index, object value)
		{
			lD();
			if (index < 0 || index > l8_u0013_)
			{
				ln("index", index, "Index must be >= 0 and <= Count.");
			}
			m_InnerArrayList.Insert(l8a + index, value);
			l8_u0013_++;
			l8N = m_InnerArrayList.l8l;
		}

		public override void InsertRange(int index, ICollection c)
		{
			lD();
			if (index < 0 || index > l8_u0013_)
			{
				ln("index", index, "Index must be >= 0 and <= Count.");
			}
			m_InnerArrayList.InsertRange(l8a + index, c);
			l8_u0013_ += c.Count;
			l8N = m_InnerArrayList.l8l;
		}

		public override void Remove(object value)
		{
			lD();
			int num = IndexOf(value);
			if (num > -1)
			{
				RemoveAt(num);
			}
			l8N = m_InnerArrayList.l8l;
		}

		public override void RemoveAt(int index)
		{
			lD();
			if (index < 0 || index > l8_u0013_)
			{
				ln("index", index, "Index must be >= 0 and <= Count.");
			}
			m_InnerArrayList.RemoveAt(l8a + index);
			l8_u0013_--;
			l8N = m_InnerArrayList.l8l;
		}

		public override void RemoveRange(int index, int count)
		{
			lD();
			lm(index, count, l8_u0013_);
			m_InnerArrayList.RemoveRange(l8a + index, count);
			l8_u0013_ -= count;
			l8N = m_InnerArrayList.l8l;
		}

		public override void Reverse()
		{
			Reverse(0, l8_u0013_);
		}

		public override void Reverse(int index, int count)
		{
			lD();
			lm(index, count, l8_u0013_);
			m_InnerArrayList.Reverse(l8a + index, count);
			l8N = m_InnerArrayList.l8l;
		}

		public override void SetRange(int index, ICollection c)
		{
			lD();
			if (index < 0 || index > l8_u0013_)
			{
				ln("index", index, "Index must be >= 0 and <= Count.");
			}
			m_InnerArrayList.SetRange(l8a + index, c);
			l8N = m_InnerArrayList.l8l;
		}

		public override void CopyTo(Array array)
		{
			CopyTo(array, 0);
		}

		public override void CopyTo(Array array, int index)
		{
			CopyTo(0, array, index, l8_u0013_);
		}

		public override void CopyTo(int index, Array array, int arrayIndex, int count)
		{
			lm(index, count, l8_u0013_);
			m_InnerArrayList.CopyTo(l8a + index, array, arrayIndex, count);
		}

		public override IEnumerator GetEnumerator()
		{
			return GetEnumerator(0, l8_u0013_);
		}

		public override IEnumerator GetEnumerator(int index, int count)
		{
			lm(index, count, l8_u0013_);
			return m_InnerArrayList.GetEnumerator(l8a + index, count);
		}

		public override void AddRange(ICollection c)
		{
			lD();
			m_InnerArrayList.InsertRange(l8_u0013_, c);
			l8_u0013_ += c.Count;
			l8N = m_InnerArrayList.l8l;
		}

		public override int BinarySearch(object value)
		{
			return BinarySearch(0, l8_u0013_, value, _u0013_.Default);
		}

		public override int BinarySearch(object value, IComparer comparer)
		{
			return BinarySearch(0, l8_u0013_, value, comparer);
		}

		public override int BinarySearch(int index, int count, object value, IComparer comparer)
		{
			lm(index, count, l8_u0013_);
			return m_InnerArrayList.BinarySearch(l8a + index, count, value, comparer);
		}

		public override object Clone()
		{
			return new w8B((a)m_InnerArrayList.Clone(), l8a, l8_u0013_);
		}

		public override a GetRange(int index, int count)
		{
			lm(index, count, l8_u0013_);
			return new w8B(this, index, count);
		}

		public override void TrimToSize()
		{
			throw new NotSupportedException();
		}

		public override void Sort()
		{
			Sort(_u0013_.Default);
		}

		public override void Sort(IComparer comparer)
		{
			Sort(0, l8_u0013_, comparer);
		}

		public override void Sort(int index, int count, IComparer comparer)
		{
			lD();
			lm(index, count, l8_u0013_);
			m_InnerArrayList.Sort(l8a + index, count, comparer);
			l8N = m_InnerArrayList.l8l;
		}

		public override object[] ToArray()
		{
			object[] array = new object[l8_u0013_];
			m_InnerArrayList.CopyTo(l8a, array, 0, l8_u0013_);
			return array;
		}

		public override Array ToArray(Type elementType)
		{
			Array array = Array.CreateInstance(elementType, l8_u0013_);
			m_InnerArrayList.CopyTo(l8a, array, 0, l8_u0013_);
			return array;
		}
	}

	[Serializable]
	private class w8s : IList, ICollection, IEnumerable
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

		public w8s(IList innerList)
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
	private sealed class w8h : w8s
	{
		private object l8a;

		public override int Count
		{
			get
			{
				lock (l8a)
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
				lock (l8a)
				{
					return m_InnerList.SyncRoot;
				}
			}
		}

		public override bool IsFixedSize
		{
			get
			{
				lock (l8a)
				{
					return m_InnerList.IsFixedSize;
				}
			}
		}

		public override bool IsReadOnly
		{
			get
			{
				lock (l8a)
				{
					return m_InnerList.IsReadOnly;
				}
			}
		}

		public override object this[int index]
		{
			get
			{
				lock (l8a)
				{
					return m_InnerList[index];
				}
			}
			set
			{
				lock (l8a)
				{
					m_InnerList[index] = value;
				}
			}
		}

		public w8h(IList innerList)
			: base(innerList)
		{
			l8a = innerList.SyncRoot;
		}

		public override int Add(object value)
		{
			lock (l8a)
			{
				return m_InnerList.Add(value);
			}
		}

		public override void Clear()
		{
			lock (l8a)
			{
				m_InnerList.Clear();
			}
		}

		public override bool Contains(object value)
		{
			lock (l8a)
			{
				return m_InnerList.Contains(value);
			}
		}

		public override int IndexOf(object value)
		{
			lock (l8a)
			{
				return m_InnerList.IndexOf(value);
			}
		}

		public override void Insert(int index, object value)
		{
			lock (l8a)
			{
				m_InnerList.Insert(index, value);
			}
		}

		public override void Remove(object value)
		{
			lock (l8a)
			{
				m_InnerList.Remove(value);
			}
		}

		public override void RemoveAt(int index)
		{
			lock (l8a)
			{
				m_InnerList.RemoveAt(index);
			}
		}

		public override void CopyTo(Array array, int index)
		{
			lock (l8a)
			{
				m_InnerList.CopyTo(array, index);
			}
		}

		public override IEnumerator GetEnumerator()
		{
			lock (l8a)
			{
				return m_InnerList.GetEnumerator();
			}
		}
	}

	[Serializable]
	private class w8_u0002_ : w8s
	{
		protected virtual string ErrorMessage => "List is fixed-size.";

		public override bool IsFixedSize => true;

		public w8_u0002_(IList innerList)
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
	private sealed class w8v : w8_u0002_
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

		public w8v(IList innerList)
			: base(innerList)
		{
		}
	}

	private const int l8a = 16;

	private int l8_u0013_;

	private object[] l8N;

	private int l8l;

	public virtual object this[int index]
	{
		get
		{
			if (index < 0 || index >= l8_u0013_)
			{
				ln("index", index, "Index is less than 0 or more than or equal to the list count.");
			}
			return l8N[index];
		}
		set
		{
			if (index < 0 || index >= l8_u0013_)
			{
				ln("index", index, "Index is less than 0 or more than or equal to the list count.");
			}
			l8N[index] = value;
			l8l++;
		}
	}

	public virtual int Count => l8_u0013_;

	public virtual int Capacity
	{
		get
		{
			return l8N.Length;
		}
		set
		{
			if (value < l8_u0013_)
			{
				ln("Capacity", value, "Must be more than count.");
			}
			object[] destinationArray = new object[value];
			Array.Copy(l8N, 0, destinationArray, 0, l8_u0013_);
			l8N = destinationArray;
		}
	}

	public virtual bool IsFixedSize => false;

	public virtual bool IsReadOnly => false;

	public virtual bool IsSynchronized => false;

	public virtual object SyncRoot => this;

	public a()
	{
		l8N = new object[16];
	}

	public a(ICollection c)
	{
		if (c == null)
		{
			throw new ArgumentNullException("c");
		}
		if (c is Array { Rank: not 1 })
		{
			throw new RankException();
		}
		l8N = new object[c.Count];
		AddRange(c);
	}

	public a(int capacity)
	{
		if (capacity < 0)
		{
			ln("capacity", capacity, "The initial capacity can't be smaller than zero.");
		}
		if (capacity == 0)
		{
			capacity = 16;
		}
		l8N = new object[capacity];
	}

	private a(int param_0, bool param_1)
	{
		if (param_1)
		{
			l8N = null;
			return;
		}
		throw new InvalidOperationException("Use ArrayList(int)");
	}

	private a(object[] param_0, int param_1, int param_2)
	{
		if (param_2 == 0)
		{
			l8N = new object[16];
		}
		else
		{
			l8N = new object[param_2];
		}
		Array.Copy(param_0, param_1, l8N, 0, param_2);
		l8_u0013_ = param_2;
	}

	private void lV(int param_0)
	{
		if (param_0 > l8N.Length)
		{
			int num = l8N.Length << 1;
			if (num == 0)
			{
				num = 16;
			}
			while (num < param_0)
			{
				num <<= 1;
			}
			object[] destinationArray = new object[num];
			Array.Copy(l8N, 0, destinationArray, 0, l8N.Length);
			l8N = destinationArray;
		}
	}

	private void l_u0011_(int param_0, int param_1)
	{
		if (param_1 > 0)
		{
			if (l8_u0013_ + param_1 > l8N.Length)
			{
				int num;
				for (num = ((l8N.Length <= 0) ? 1 : (l8N.Length << 1)); num < l8_u0013_ + param_1; num <<= 1)
				{
				}
				object[] destinationArray = new object[num];
				Array.Copy(l8N, 0, destinationArray, 0, param_0);
				Array.Copy(l8N, param_0, destinationArray, param_0 + param_1, l8_u0013_ - param_0);
				l8N = destinationArray;
			}
			else
			{
				Array.Copy(l8N, param_0, l8N, param_0 + param_1, l8_u0013_ - param_0);
			}
		}
		else if (param_1 < 0)
		{
			int num2 = param_0 - param_1;
			Array.Copy(l8N, num2, l8N, param_0, l8_u0013_ - num2);
			Array.Clear(l8N, l8_u0013_ + param_1, -param_1);
		}
	}

	public virtual int Add(object value)
	{
		if (l8N.Length <= l8_u0013_)
		{
			lV(l8_u0013_ + 1);
		}
		l8N[l8_u0013_] = value;
		l8l++;
		return l8_u0013_++;
	}

	public virtual void Clear()
	{
		Array.Clear(l8N, 0, l8_u0013_);
		l8_u0013_ = 0;
		l8l++;
	}

	public virtual bool Contains(object item)
	{
		return IndexOf(item, 0, l8_u0013_) > -1;
	}

	internal virtual bool l_u0003_(object param_0, int param_1, int param_2)
	{
		return IndexOf(param_0, param_1, param_2) > -1;
	}

	public virtual int IndexOf(object value)
	{
		return IndexOf(value, 0);
	}

	public virtual int IndexOf(object value, int startIndex)
	{
		return IndexOf(value, startIndex, l8_u0013_ - startIndex);
	}

	public virtual int IndexOf(object value, int startIndex, int count)
	{
		if (startIndex < 0 || startIndex > l8_u0013_)
		{
			ln("startIndex", startIndex, "Does not specify valid index.");
		}
		if (count < 0)
		{
			ln("count", count, "Can't be less than 0.");
		}
		if (startIndex > l8_u0013_ - count)
		{
			throw new ArgumentOutOfRangeException("count", "Start index and count do not specify a valid range.");
		}
		return Array.IndexOf(l8N, value, startIndex, count);
	}

	public virtual int LastIndexOf(object value)
	{
		return LastIndexOf(value, l8_u0013_ - 1);
	}

	public virtual int LastIndexOf(object value, int startIndex)
	{
		return LastIndexOf(value, startIndex, startIndex + 1);
	}

	public virtual int LastIndexOf(object value, int startIndex, int count)
	{
		return Array.LastIndexOf(l8N, value, startIndex, count);
	}

	public virtual void Insert(int index, object value)
	{
		if (index < 0 || index > l8_u0013_)
		{
			ln("index", index, "Index must be >= 0 and <= Count.");
		}
		l_u0011_(index, 1);
		l8N[index] = value;
		l8_u0013_++;
		l8l++;
	}

	public virtual void InsertRange(int index, ICollection c)
	{
		if (c == null)
		{
			throw new ArgumentNullException("c");
		}
		if (index < 0 || index > l8_u0013_)
		{
			ln("index", index, "Index must be >= 0 and <= Count.");
		}
		int count = c.Count;
		if (l8N.Length < l8_u0013_ + count)
		{
			lV(l8_u0013_ + count);
		}
		if (index < l8_u0013_)
		{
			Array.Copy(l8N, index, l8N, index + count, l8_u0013_ - index);
		}
		if (this == c.SyncRoot)
		{
			Array.Copy(l8N, 0, l8N, index, index);
			Array.Copy(l8N, index + count, l8N, index << 1, l8_u0013_ - index);
		}
		else
		{
			c.CopyTo(l8N, index);
		}
		l8_u0013_ += c.Count;
		l8l++;
	}

	public virtual void Remove(object obj)
	{
		int num = IndexOf(obj);
		if (num > -1)
		{
			RemoveAt(num);
		}
		l8l++;
	}

	public virtual void RemoveAt(int index)
	{
		if (index < 0 || index >= l8_u0013_)
		{
			ln("index", index, "Less than 0 or more than list count.");
		}
		l_u0011_(index, -1);
		l8_u0013_--;
		l8l++;
	}

	public virtual void RemoveRange(int index, int count)
	{
		lm(index, count, l8_u0013_);
		l_u0011_(index, -count);
		l8_u0013_ -= count;
		l8l++;
	}

	public virtual void Reverse()
	{
		Array.Reverse(l8N, 0, l8_u0013_);
		l8l++;
	}

	public virtual void Reverse(int index, int count)
	{
		lm(index, count, l8_u0013_);
		Array.Reverse(l8N, index, count);
		l8l++;
	}

	public virtual void CopyTo(Array array)
	{
		Array.Copy(l8N, array, l8_u0013_);
	}

	public virtual void CopyTo(Array array, int arrayIndex)
	{
		CopyTo(0, array, arrayIndex, l8_u0013_);
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
		Array.Copy(l8N, index, array, arrayIndex, count);
	}

	public virtual IEnumerator GetEnumerator()
	{
		return new w8_u0013_(this);
	}

	public virtual IEnumerator GetEnumerator(int index, int count)
	{
		lm(index, count, l8_u0013_);
		return new w8a(this, index, count);
	}

	public virtual void AddRange(ICollection c)
	{
		InsertRange(l8_u0013_, c);
	}

	public virtual int BinarySearch(object value)
	{
		try
		{
			return Array.BinarySearch(l8N, 0, l8_u0013_, value);
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
			return Array.BinarySearch(l8N, 0, l8_u0013_, value, comparer);
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
			return Array.BinarySearch(l8N, index, count, value, comparer);
		}
		catch (InvalidOperationException ex)
		{
			throw new ArgumentException(ex.Message);
		}
	}

	public virtual a GetRange(int index, int count)
	{
		lm(index, count, l8_u0013_);
		if (IsSynchronized)
		{
			return Synchronized(new w8B(this, index, count));
		}
		return new w8B(this, index, count);
	}

	public virtual void SetRange(int index, ICollection c)
	{
		if (c == null)
		{
			throw new ArgumentNullException("c");
		}
		if (index < 0 || index + c.Count > l8_u0013_)
		{
			throw new ArgumentOutOfRangeException("index");
		}
		c.CopyTo(l8N, index);
		l8l++;
	}

	public virtual void TrimToSize()
	{
		if (l8N.Length > l8_u0013_)
		{
			object[] destinationArray = ((l8_u0013_ != 0) ? new object[l8_u0013_] : new object[16]);
			Array.Copy(l8N, 0, destinationArray, 0, l8_u0013_);
			l8N = destinationArray;
		}
	}

	public virtual void Sort()
	{
		Array.Sort(l8N, 0, l8_u0013_);
		l8l++;
	}

	public virtual void Sort(IComparer comparer)
	{
		Array.Sort(l8N, 0, l8_u0013_, comparer);
	}

	public virtual void Sort(int index, int count, IComparer comparer)
	{
		lm(index, count, l8_u0013_);
		Array.Sort(l8N, index, count, comparer);
	}

	public virtual object[] ToArray()
	{
		object[] array = new object[l8_u0013_];
		CopyTo(array);
		return array;
	}

	public virtual Array ToArray(Type type)
	{
		Array array = Array.CreateInstance(type, l8_u0013_);
		CopyTo(array);
		return array;
	}

	public virtual object Clone()
	{
		return new a(l8N, 0, l8_u0013_);
	}

	internal static void lm(int param_0, int param_1, int param_2)
	{
		if (param_0 < 0)
		{
			ln("index", param_0, "Can't be less than 0.");
		}
		if (param_1 < 0)
		{
			ln("count", param_1, "Can't be less than 0.");
		}
		if (param_0 > param_2 - param_1)
		{
			throw new ArgumentException("Index and count do not denote a valid range of elements.", "index");
		}
	}

	internal static void ln(string param_0, object param_1, string param_2)
	{
		throw new ArgumentOutOfRangeException(param_0, param_2);
	}

	public static a Adapter(IList list)
	{
		if (list == null)
		{
			throw new ArgumentNullException("list");
		}
		if (list is a result)
		{
			return result;
		}
		a a2 = new w8N(list);
		if (list.IsSynchronized)
		{
			return Synchronized(a2);
		}
		return a2;
	}

	public static a Synchronized(a list)
	{
		if (list == null)
		{
			throw new ArgumentNullException("list");
		}
		if (list.IsSynchronized)
		{
			return list;
		}
		return new w8L(list);
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
		return new w8h(list);
	}

	public static a ReadOnly(a list)
	{
		if (list == null)
		{
			throw new ArgumentNullException("list");
		}
		if (list.IsReadOnly)
		{
			return list;
		}
		return new w8J(list);
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
		return new w8v(list);
	}

	public static a FixedSize(a list)
	{
		if (list == null)
		{
			throw new ArgumentNullException("list");
		}
		if (list.IsFixedSize)
		{
			return list;
		}
		return new w8w(list);
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
		return new w8_u0002_(list);
	}

	public static a Repeat(object value, int count)
	{
		a a2 = new a(count);
		for (int i = 0; i < count; i++)
		{
			a2.Add(value);
		}
		return a2;
	}
}
