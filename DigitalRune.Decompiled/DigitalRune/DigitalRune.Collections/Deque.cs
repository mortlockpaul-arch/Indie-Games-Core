using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using A;

namespace DigitalRune.Collections;

/// <summary>
///       Represents a double-ended queue (deque) of objects. 
///       </summary>
/// <typeparam name="T">Specifies the type of elements in the deque.</typeparam>
public class Deque<T> : ICollection<T>, IEnumerable<T>, IList<T>, ICollection, IEnumerable
{
	/// <summary>
	///       Enumerates the elements of a <see cref="T:DigitalRune.Collections.Deque`1" />. 
	///       </summary>
	public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
	{
		private readonly Deque<T> ce215efc8579663de07e31479ddd79922;

		private readonly int cff6df43bfad2fc97c3a02183e19563e0;

		private int c2a3d603b430e66707dc56b455d6355a5;

		private T c209b142037e732ef8ff084df7ac98927;

		/// <summary>
		///       Gets the element in the collection at the current position of the enumerator.
		///       </summary>
		public T Current => c209b142037e732ef8ff084df7ac98927;

		private object c1bbc3372d25e0136d793f1306899a54b
		{
			get
			{
				if (c2a3d603b430e66707dc56b455d6355a5 < 0)
				{
					while (true)
					{
						switch (1)
						{
						case 0:
							break;
						default:
							if (1 == 0)
							{
								__ldtoken(Deque<>.Enumerator.get_c1bbc3372d25e0136d793f1306899a54b());
							}
							if (c2a3d603b430e66707dc56b455d6355a5 == -1)
							{
								throw new InvalidOperationException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4926));
							}
							throw new InvalidOperationException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5072));
						}
					}
				}
				return c209b142037e732ef8ff084df7ac98927;
			}
		}

		internal Enumerator(Deque<T> c11a62bea772d26039b233a98735261ab)
		{
			ce215efc8579663de07e31479ddd79922 = c11a62bea772d26039b233a98735261ab;
			cff6df43bfad2fc97c3a02183e19563e0 = c11a62bea772d26039b233a98735261ab.cff6df43bfad2fc97c3a02183e19563e0;
			c2a3d603b430e66707dc56b455d6355a5 = -1;
			c209b142037e732ef8ff084df7ac98927 = default;
		}

		/// <summary>
		///       Performs application-defined tasks associated with freeing, releasing, or resetting 
		///       unmanaged resources.
		///       </summary>
		public void Dispose()
		{
			c2a3d603b430e66707dc56b455d6355a5 = -2;
			c209b142037e732ef8ff084df7ac98927 = default;
		}

		/// <summary>
		///       Advances the enumerator to the next element of the collection.
		///       </summary>
		/// <returns>
		///   <see langword="true" /> if the enumerator was successfully advanced to the next element; 
		///       <see langword="false" /> if the enumerator has passed the end of the collection.
		///       </returns>
		/// <exception cref="T:System.InvalidOperationException">
		///       The collection was modified after the enumerator was created.
		///       </exception>
		public bool MoveNext()
		{
			if (cff6df43bfad2fc97c3a02183e19563e0 != ce215efc8579663de07e31479ddd79922.cff6df43bfad2fc97c3a02183e19563e0)
			{
				throw new InvalidOperationException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5214));
			}
			if (c2a3d603b430e66707dc56b455d6355a5 == -2)
			{
				while (true)
				{
					switch (5)
					{
					case 0:
						break;
					default:
						if (1 == 0)
						{
							__ldtoken(Deque<>.Enumerator.MoveNext());
						}
						return false;
					}
				}
			}
			c2a3d603b430e66707dc56b455d6355a5++;
			if (c2a3d603b430e66707dc56b455d6355a5 < ce215efc8579663de07e31479ddd79922.cb661c4ff8f251829cdaf0c9d9db2004c)
			{
				while (true)
				{
					switch (7)
					{
					case 0:
						break;
					default:
					{
						T[] c37adb342465be20dcf2c430c7d6aeb = ce215efc8579663de07e31479ddd79922.c37adb342465be20dcf2c430c7d6aeb68;
						int c244dfd57f19cc3001e478aa7234e2f = ce215efc8579663de07e31479ddd79922.c244dfd57f19cc3001e478aa7234e2f40;
						int num = c37adb342465be20dcf2c430c7d6aeb.Length;
						c209b142037e732ef8ff084df7ac98927 = c37adb342465be20dcf2c430c7d6aeb[(c244dfd57f19cc3001e478aa7234e2f + c2a3d603b430e66707dc56b455d6355a5) % num];
						return true;
					}
					}
				}
			}
			c2a3d603b430e66707dc56b455d6355a5 = -2;
			c209b142037e732ef8ff084df7ac98927 = default;
			return false;
		}

		/// <summary>
		///       Sets the enumerator to its initial position, which is before the first element in the 
		///       <see cref="T:DigitalRune.Collections.Deque`1" />.
		///       </summary>
		/// <exception cref="T:System.InvalidOperationException">
		///       The <see cref="T:DigitalRune.Collections.Deque`1" /> was modified after the enumerator was created.
		///       </exception>
		public void Reset()
		{
			if (cff6df43bfad2fc97c3a02183e19563e0 != ce215efc8579663de07e31479ddd79922.cff6df43bfad2fc97c3a02183e19563e0)
			{
				while (true)
				{
					switch (4)
					{
					case 0:
						break;
					default:
						if (1 == 0)
						{
							__ldtoken(Deque<>.Enumerator.Reset());
						}
						throw new InvalidOperationException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5214));
					}
				}
			}
			c2a3d603b430e66707dc56b455d6355a5 = -1;
			c209b142037e732ef8ff084df7ac98927 = default;
		}
	}

	private static readonly T[] ca9067283b58d0636dbeacce700866c29 = new T[0];

	private T[] c37adb342465be20dcf2c430c7d6aeb68;

	private int cb661c4ff8f251829cdaf0c9d9db2004c;

	private int c244dfd57f19cc3001e478aa7234e2f40;

	private int c2fc24f337b64adf7fcfafebc35829c3b;

	private int cff6df43bfad2fc97c3a02183e19563e0;

	private object c0a2e68bb92251653b0cbd8bf2fe72c28;

	/// <summary>
	///       Gets the number of items contained in the <see cref="T:DigitalRune.Collections.Deque`1" />.
	///       </summary>
	public int Count => cb661c4ff8f251829cdaf0c9d9db2004c;

	private object c9be56348c75e8eb93f4195f512c32f99
	{
		get
		{
			if (c0a2e68bb92251653b0cbd8bf2fe72c28 == null)
			{
				Interlocked.CompareExchange<object>(ref c0a2e68bb92251653b0cbd8bf2fe72c28, new object(), (object)null);
			}
			return c0a2e68bb92251653b0cbd8bf2fe72c28;
		}
	}

	private bool cb0b0651e317ddd8a13a67ef2b6b20484 => false;

	private bool c02aea6f65acc1ac2c5e3545134255a92 => false;

	/// <summary>
	///       Gets or sets the object at the beginning of the <see cref="T:DigitalRune.Collections.Deque`1" />.
	///       </summary>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:DigitalRune.Collections.Deque`1" /> is empty.</exception>
	public T Head
	{
		get
		{
			if (cb661c4ff8f251829cdaf0c9d9db2004c == 0)
			{
				throw new InvalidOperationException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4077));
			}
			return c37adb342465be20dcf2c430c7d6aeb68[c244dfd57f19cc3001e478aa7234e2f40];
		}
		set
		{
			if (cb661c4ff8f251829cdaf0c9d9db2004c == 0)
			{
				while (true)
				{
					switch (7)
					{
					case 0:
						break;
					default:
						if (1 == 0)
						{
							__ldtoken(Deque<>.set_Head(T));
						}
						throw new InvalidOperationException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4122));
					}
				}
			}
			c37adb342465be20dcf2c430c7d6aeb68[c244dfd57f19cc3001e478aa7234e2f40] = value;
			cff6df43bfad2fc97c3a02183e19563e0++;
		}
	}

	/// <summary>
	///       Gets or sets the object at the end of the <see cref="T:DigitalRune.Collections.Deque`1" />.
	///       </summary>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:DigitalRune.Collections.Deque`1" /> is empty.</exception>
	public T Tail
	{
		get
		{
			if (cb661c4ff8f251829cdaf0c9d9db2004c == 0)
			{
				while (true)
				{
					switch (6)
					{
					case 0:
						break;
					default:
						if (1 == 0)
						{
							__ldtoken(Deque<>.get_Tail());
						}
						throw new InvalidOperationException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4077));
					}
				}
			}
			int num = ((c2fc24f337b64adf7fcfafebc35829c3b == 0) ? (c37adb342465be20dcf2c430c7d6aeb68.Length - 1) : (c2fc24f337b64adf7fcfafebc35829c3b - 1));
			return c37adb342465be20dcf2c430c7d6aeb68[num];
		}
		set
		{
			if (cb661c4ff8f251829cdaf0c9d9db2004c == 0)
			{
				while (true)
				{
					switch (5)
					{
					case 0:
						break;
					default:
						if (1 == 0)
						{
							__ldtoken(Deque<>.set_Tail(T));
						}
						throw new InvalidOperationException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4077));
					}
				}
			}
			int num = ((c2fc24f337b64adf7fcfafebc35829c3b == 0) ? (c37adb342465be20dcf2c430c7d6aeb68.Length - 1) : (c2fc24f337b64adf7fcfafebc35829c3b - 1));
			c37adb342465be20dcf2c430c7d6aeb68[num] = value;
			cff6df43bfad2fc97c3a02183e19563e0++;
		}
	}

	/// <summary>
	///       Gets or sets the item at the specified index.
	///       </summary>
	/// <param name="index">The zero-based index of the item to get or set.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is less than 0 or equal to or greater than <see cref="P:DigitalRune.Collections.Deque`1.Count" />.
	///       </exception>
	public T this[int index]
	{
		get
		{
			if (index >= 0)
			{
				while (true)
				{
					switch (7)
					{
					case 0:
						continue;
					}
					break;
				}
				if (1 == 0)
				{
					__ldtoken(Deque<>.get_Item(int));
				}
				if (cb661c4ff8f251829cdaf0c9d9db2004c > index)
				{
					return c37adb342465be20dcf2c430c7d6aeb68[(c244dfd57f19cc3001e478aa7234e2f40 + index) % c37adb342465be20dcf2c430c7d6aeb68.Length];
				}
				while (true)
				{
					switch (2)
					{
					case 0:
						continue;
					}
					break;
				}
			}
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2804), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4167));
		}
		set
		{
			if (index >= 0)
			{
				if (cb661c4ff8f251829cdaf0c9d9db2004c > index)
				{
					c37adb342465be20dcf2c430c7d6aeb68[(c244dfd57f19cc3001e478aa7234e2f40 + index) % c37adb342465be20dcf2c430c7d6aeb68.Length] = value;
					cff6df43bfad2fc97c3a02183e19563e0++;
					return;
				}
				while (true)
				{
					switch (2)
					{
					case 0:
						continue;
					}
					break;
				}
				if (1 == 0)
				{
					__ldtoken(Deque<>.set_Item(int, T));
				}
			}
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2804), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4167));
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Collections.Deque`1" /> class that is empty and has the
	///       default initial capacity. 
	///       </summary>
	public Deque()
	{
		c37adb342465be20dcf2c430c7d6aeb68 = ca9067283b58d0636dbeacce700866c29;
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Collections.Deque`1" /> class that contains elements copied 
	///       from the specified collection and has sufficient capacity to accommodate the number of 
	///       elements copied. 
	///       </summary>
	/// <param name="collection">
	///       The collection whose elements are copied to the new <see cref="T:DigitalRune.Collections.Deque`1" />. 
	///       </param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="collection" /> is <see langword="null" />.
	///       </exception>
	public Deque(IEnumerable<T> collection)
	{
		if (collection == null)
		{
			while (true)
			{
				switch (1)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(Deque<>._002Ector(IEnumerable<T>));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1764));
				}
			}
		}
		c37adb342465be20dcf2c430c7d6aeb68 = new T[4];
		IEnumerator<T> enumerator = collection.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				T current = enumerator.Current;
				EnqueueTail(current);
			}
		}
		finally
		{
			if (enumerator != null)
			{
				while (true)
				{
					switch (5)
					{
					case 0:
						continue;
					}
					enumerator.Dispose();
					break;
				}
			}
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Collections.Deque`1" /> class is empty and has the 
	///       specified initial capacity. 
	///       </summary>
	/// <param name="capacity">
	///       The initial number of elements that the <see cref="T:DigitalRune.Collections.Deque`1" /> can contain.
	///       </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="capacity" /> is negative.
	///       </exception>
	public Deque(int capacity)
	{
		if (capacity < 0)
		{
			while (true)
			{
				switch (7)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(Deque<>._002Ector(int));
					}
					throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4796), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4813));
				}
			}
		}
		c37adb342465be20dcf2c430c7d6aeb68 = new T[capacity];
	}

	private void c3f80c67a8a6bedd96b3e03d9b9d3aafb(T cf51048ba330b8d3f481ea5bc25a03eb9)
	{
		EnqueueTail(cf51048ba330b8d3f481ea5bc25a03eb9);
	}

	void ICollection<T>.Add(T cf51048ba330b8d3f481ea5bc25a03eb9)
	{
		//ILSpy generated this explicit interface implementation from .override directive in c3f80c67a8a6bedd96b3e03d9b9d3aafb
		this.c3f80c67a8a6bedd96b3e03d9b9d3aafb(cf51048ba330b8d3f481ea5bc25a03eb9);
	}

	/// <summary>
	///       Removes all items from the <see cref="T:DigitalRune.Collections.Deque`1" />.
	///       </summary>
	public void Clear()
	{
		if (c244dfd57f19cc3001e478aa7234e2f40 < c2fc24f337b64adf7fcfafebc35829c3b)
		{
			Array.Clear(c37adb342465be20dcf2c430c7d6aeb68, c244dfd57f19cc3001e478aa7234e2f40, cb661c4ff8f251829cdaf0c9d9db2004c);
		}
		else
		{
			Array.Clear(c37adb342465be20dcf2c430c7d6aeb68, c244dfd57f19cc3001e478aa7234e2f40, c37adb342465be20dcf2c430c7d6aeb68.Length - c244dfd57f19cc3001e478aa7234e2f40);
			Array.Clear(c37adb342465be20dcf2c430c7d6aeb68, 0, c2fc24f337b64adf7fcfafebc35829c3b);
		}
		cb661c4ff8f251829cdaf0c9d9db2004c = 0;
		c244dfd57f19cc3001e478aa7234e2f40 = 0;
		c2fc24f337b64adf7fcfafebc35829c3b = 0;
		cff6df43bfad2fc97c3a02183e19563e0++;
	}

	/// <summary>
	///       Determines whether the <see cref="T:DigitalRune.Collections.Deque`1" /> contains a specific value.
	///       </summary>
	/// <param name="item">The object to locate in the <see cref="T:DigitalRune.Collections.Deque`1" />.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="item" /> is found in the <see cref="T:DigitalRune.Collections.Deque`1" />; 
	///       otherwise, <see langword="false" />.
	///       </returns>
	public bool Contains(T item)
	{
		return IndexOf(item) != -1;
	}

	/// <summary>
	///       Determines the index of a specific item in the <see cref="T:DigitalRune.Collections.Deque`1" />.
	///       </summary>
	/// <param name="item">The object to locate in the <see cref="T:DigitalRune.Collections.Deque`1" />.</param>
	/// <returns>
	///       The index of <paramref name="item" /> if found in the deque; otherwise, -1.
	///       </returns>
	public int IndexOf(T item)
	{
		EqualityComparer<T> equalityComparer = EqualityComparer<T>.Default;
		if (c244dfd57f19cc3001e478aa7234e2f40 < c2fc24f337b64adf7fcfafebc35829c3b)
		{
			for (int i = c244dfd57f19cc3001e478aa7234e2f40; i < c2fc24f337b64adf7fcfafebc35829c3b; i++)
			{
				if (!equalityComparer.Equals(item, c37adb342465be20dcf2c430c7d6aeb68[i]))
				{
					continue;
				}
				while (true)
				{
					switch (5)
					{
					case 0:
						continue;
					}
					if (1 == 0)
					{
						__ldtoken(Deque<>.IndexOf(T));
					}
					return i - c244dfd57f19cc3001e478aa7234e2f40;
				}
			}
		}
		else
		{
			for (int j = c244dfd57f19cc3001e478aa7234e2f40; j < c37adb342465be20dcf2c430c7d6aeb68.Length; j++)
			{
				if (!equalityComparer.Equals(item, c37adb342465be20dcf2c430c7d6aeb68[j]))
				{
					continue;
				}
				while (true)
				{
					switch (2)
					{
					case 0:
						continue;
					}
					return j - c244dfd57f19cc3001e478aa7234e2f40;
				}
			}
			for (int k = 0; k < c2fc24f337b64adf7fcfafebc35829c3b; k++)
			{
				if (!equalityComparer.Equals(item, c37adb342465be20dcf2c430c7d6aeb68[k]))
				{
					continue;
				}
				while (true)
				{
					switch (7)
					{
					case 0:
						continue;
					}
					return k + c37adb342465be20dcf2c430c7d6aeb68.Length - c244dfd57f19cc3001e478aa7234e2f40;
				}
			}
			while (true)
			{
				switch (3)
				{
				case 0:
					continue;
				}
				break;
			}
		}
		return -1;
	}

	/// <summary>
	///       Copies the elements of the <see cref="T:DigitalRune.Collections.Deque`1" /> to an <see cref="T:System.Array" />, starting at a 
	///       particular <see cref="T:System.Array" /> index.
	///       </summary>
	/// <param name="array">
	///       The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from 
	///       <see cref="T:DigitalRune.Collections.Deque`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.
	///       </param>
	/// <param name="arrayIndex">
	///       The zero-based index in <paramref name="array" /> at which copying begins.
	///       </param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="array" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="arrayIndex" /> is less than 0.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="array" /> is multidimensional. Or <paramref name="arrayIndex" /> is equal to 
	///       or greater than the length of <paramref name="array" />. Or the number of elements in the 
	///       source <see cref="T:DigitalRune.Collections.Deque`1" /> is greater than the available space from 
	///       <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />.
	///       </exception>
	public void CopyTo(T[] array, int arrayIndex)
	{
		((ICollection)this).CopyTo((Array)array, arrayIndex);
	}

	private void c44d0c5f4266290decda264ead4da31d9(Array c6278a2c6debbe0cf2d35fcb5ff5fd0d5, int c400871890da984ad2947a014526712c4)
	{
		if (c6278a2c6debbe0cf2d35fcb5ff5fd0d5 == null)
		{
			while (true)
			{
				switch (6)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(Deque<>.c44d0c5f4266290decda264ead4da31d9(Array, int));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4212));
				}
			}
		}
		if (c400871890da984ad2947a014526712c4 < 0)
		{
			while (true)
			{
				switch (6)
				{
				case 0:
					break;
				default:
					throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4223), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4244));
				}
			}
		}
		if (c6278a2c6debbe0cf2d35fcb5ff5fd0d5.Length > 0)
		{
			while (true)
			{
				switch (7)
				{
				case 0:
					continue;
				}
				break;
			}
			if (c6278a2c6debbe0cf2d35fcb5ff5fd0d5.Length <= c400871890da984ad2947a014526712c4)
			{
				throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4223), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4333));
			}
		}
		if (cb661c4ff8f251829cdaf0c9d9db2004c <= 0)
		{
			return;
		}
		if (c244dfd57f19cc3001e478aa7234e2f40 < c2fc24f337b64adf7fcfafebc35829c3b)
		{
			while (true)
			{
				switch (2)
				{
				case 0:
					break;
				default:
					Array.Copy(c37adb342465be20dcf2c430c7d6aeb68, c244dfd57f19cc3001e478aa7234e2f40, c6278a2c6debbe0cf2d35fcb5ff5fd0d5, c400871890da984ad2947a014526712c4, cb661c4ff8f251829cdaf0c9d9db2004c);
					return;
				}
			}
		}
		int num = c37adb342465be20dcf2c430c7d6aeb68.Length - c244dfd57f19cc3001e478aa7234e2f40;
		Array.Copy(c37adb342465be20dcf2c430c7d6aeb68, c244dfd57f19cc3001e478aa7234e2f40, c6278a2c6debbe0cf2d35fcb5ff5fd0d5, c400871890da984ad2947a014526712c4, num);
		Array.Copy(c37adb342465be20dcf2c430c7d6aeb68, 0, c6278a2c6debbe0cf2d35fcb5ff5fd0d5, c400871890da984ad2947a014526712c4 + num, c2fc24f337b64adf7fcfafebc35829c3b);
	}

	void ICollection.CopyTo(Array c6278a2c6debbe0cf2d35fcb5ff5fd0d5, int c400871890da984ad2947a014526712c4)
	{
		//ILSpy generated this explicit interface implementation from .override directive in c44d0c5f4266290decda264ead4da31d9
		this.c44d0c5f4266290decda264ead4da31d9(c6278a2c6debbe0cf2d35fcb5ff5fd0d5, c400871890da984ad2947a014526712c4);
	}

	/// <summary>
	///       Adds an object to the beginning of the <see cref="T:DigitalRune.Collections.Deque`1" />. 
	///       </summary>
	/// <param name="item">
	///       The object to add to the <see cref="T:DigitalRune.Collections.Deque`1" />. The value can be null for reference types.
	///       </param>
	public void EnqueueHead(T item)
	{
		if (cb661c4ff8f251829cdaf0c9d9db2004c == c37adb342465be20dcf2c430c7d6aeb68.Length)
		{
			while (true)
			{
				switch (1)
				{
				case 0:
					continue;
				}
				break;
			}
			if (1 == 0)
			{
				__ldtoken(Deque<>.EnqueueHead(T));
			}
			int num = c37adb342465be20dcf2c430c7d6aeb68.Length * 2;
			if (num < c37adb342465be20dcf2c430c7d6aeb68.Length + 4)
			{
				while (true)
				{
					switch (5)
					{
					case 0:
						continue;
					}
					break;
				}
				num = c37adb342465be20dcf2c430c7d6aeb68.Length + 4;
			}
			c6a2746463e155e5e9441b6ec353e4ded(num);
		}
		if (c244dfd57f19cc3001e478aa7234e2f40 == 0)
		{
			while (true)
			{
				switch (3)
				{
				case 0:
					continue;
				}
				break;
			}
			c244dfd57f19cc3001e478aa7234e2f40 = c37adb342465be20dcf2c430c7d6aeb68.Length - 1;
		}
		else
		{
			c244dfd57f19cc3001e478aa7234e2f40--;
		}
		c37adb342465be20dcf2c430c7d6aeb68[c244dfd57f19cc3001e478aa7234e2f40] = item;
		cb661c4ff8f251829cdaf0c9d9db2004c++;
		cff6df43bfad2fc97c3a02183e19563e0++;
	}

	/// <summary>
	///       Adds an object to the end of the <see cref="T:DigitalRune.Collections.Deque`1" />. 
	///       </summary>
	/// <param name="item">
	///       The object to add to the <see cref="T:DigitalRune.Collections.Deque`1" />. The value can be null for reference types.
	///       </param>
	public void EnqueueTail(T item)
	{
		if (cb661c4ff8f251829cdaf0c9d9db2004c == c37adb342465be20dcf2c430c7d6aeb68.Length)
		{
			int num = c37adb342465be20dcf2c430c7d6aeb68.Length * 2;
			if (num < c37adb342465be20dcf2c430c7d6aeb68.Length + 4)
			{
				while (true)
				{
					switch (3)
					{
					case 0:
						continue;
					}
					break;
				}
				if (1 == 0)
				{
					__ldtoken(Deque<>.EnqueueTail(T));
				}
				num = c37adb342465be20dcf2c430c7d6aeb68.Length + 4;
			}
			c6a2746463e155e5e9441b6ec353e4ded(num);
		}
		c37adb342465be20dcf2c430c7d6aeb68[c2fc24f337b64adf7fcfafebc35829c3b++] = item;
		if (c2fc24f337b64adf7fcfafebc35829c3b == c37adb342465be20dcf2c430c7d6aeb68.Length)
		{
			c2fc24f337b64adf7fcfafebc35829c3b = 0;
		}
		cb661c4ff8f251829cdaf0c9d9db2004c++;
		cff6df43bfad2fc97c3a02183e19563e0++;
	}

	/// <summary>
	///       Removes and returns the object at the beginning of the <see cref="T:DigitalRune.Collections.Deque`1" />. 
	///       </summary>
	/// <returns>
	///       The object that is removed from the beginning of the <see cref="T:DigitalRune.Collections.Deque`1" />. 
	///       </returns>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:DigitalRune.Collections.Deque`1" /> is empty.</exception>
	public T DequeueHead()
	{
		if (cb661c4ff8f251829cdaf0c9d9db2004c == 0)
		{
			while (true)
			{
				switch (5)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(Deque<>.DequeueHead());
					}
					throw new InvalidOperationException();
				}
			}
		}
		T result = c37adb342465be20dcf2c430c7d6aeb68[c244dfd57f19cc3001e478aa7234e2f40];
		c37adb342465be20dcf2c430c7d6aeb68[c244dfd57f19cc3001e478aa7234e2f40] = default;
		c244dfd57f19cc3001e478aa7234e2f40++;
		if (c244dfd57f19cc3001e478aa7234e2f40 == c37adb342465be20dcf2c430c7d6aeb68.Length)
		{
			while (true)
			{
				switch (6)
				{
				case 0:
					continue;
				}
				break;
			}
			c244dfd57f19cc3001e478aa7234e2f40 = 0;
		}
		cb661c4ff8f251829cdaf0c9d9db2004c--;
		cff6df43bfad2fc97c3a02183e19563e0++;
		return result;
	}

	/// <summary>
	///       Removes and returns the object at the end of the <see cref="T:DigitalRune.Collections.Deque`1" />. 
	///       </summary>
	/// <returns>
	///       The object that is removed from the end of the <see cref="T:DigitalRune.Collections.Deque`1" />. 
	///       </returns>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:DigitalRune.Collections.Deque`1" /> is empty.</exception>
	public T DequeueTail()
	{
		if (cb661c4ff8f251829cdaf0c9d9db2004c == 0)
		{
			while (true)
			{
				switch (3)
				{
				case 0:
					break;
				default:
					if (1 == 0)
					{
						__ldtoken(Deque<>.DequeueTail());
					}
					throw new InvalidOperationException();
				}
			}
		}
		if (c2fc24f337b64adf7fcfafebc35829c3b == 0)
		{
			while (true)
			{
				switch (2)
				{
				case 0:
					continue;
				}
				break;
			}
			c2fc24f337b64adf7fcfafebc35829c3b = c37adb342465be20dcf2c430c7d6aeb68.Length - 1;
		}
		else
		{
			c2fc24f337b64adf7fcfafebc35829c3b--;
		}
		T result = c37adb342465be20dcf2c430c7d6aeb68[c2fc24f337b64adf7fcfafebc35829c3b];
		c37adb342465be20dcf2c430c7d6aeb68[c2fc24f337b64adf7fcfafebc35829c3b] = default;
		cb661c4ff8f251829cdaf0c9d9db2004c--;
		cff6df43bfad2fc97c3a02183e19563e0++;
		return result;
	}

	/// <summary>
	///       Returns an enumerator that iterates through the collection.
	///       </summary>
	/// <returns>
	///       A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
	///       </returns>
	public Enumerator GetEnumerator()
	{
		return new Enumerator(this);
	}

	private IEnumerator<T> c0843ff4e059c4450bad00d5ba0b6ef48()
	{
		return GetEnumerator();
	}

	IEnumerator<T> IEnumerable<T>.GetEnumerator()
	{
		//ILSpy generated this explicit interface implementation from .override directive in c0843ff4e059c4450bad00d5ba0b6ef48
		return this.c0843ff4e059c4450bad00d5ba0b6ef48();
	}

	private IEnumerator c63811cf6ff5c0b86c54fd5037c6af6ad()
	{
		return GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		//ILSpy generated this explicit interface implementation from .override directive in c63811cf6ff5c0b86c54fd5037c6af6ad
		return this.c63811cf6ff5c0b86c54fd5037c6af6ad();
	}

	private void c6a2746463e155e5e9441b6ec353e4ded(int cdf1c72713adfe2dfb797cb0be7ebfb08)
	{
		T[] array = new T[cdf1c72713adfe2dfb797cb0be7ebfb08];
		CopyTo(array, 0);
		c37adb342465be20dcf2c430c7d6aeb68 = array;
		c244dfd57f19cc3001e478aa7234e2f40 = 0;
		int num;
		if (cb661c4ff8f251829cdaf0c9d9db2004c != cdf1c72713adfe2dfb797cb0be7ebfb08)
		{
			while (true)
			{
				switch (3)
				{
				case 0:
					continue;
				}
				break;
			}
			if (1 == 0)
			{
				__ldtoken(Deque<>.c6a2746463e155e5e9441b6ec353e4ded(int));
			}
			num = cb661c4ff8f251829cdaf0c9d9db2004c;
		}
		else
		{
			num = 0;
		}
		c2fc24f337b64adf7fcfafebc35829c3b = num;
	}

	/// <summary>
	///       Copies the <see cref="T:DigitalRune.Collections.Deque`1" /> elements to a new array.
	///       </summary>
	/// <returns>A new array containing elements copied from the <see cref="T:DigitalRune.Collections.Deque`1" />.</returns>
	public T[] ToArray()
	{
		T[] array = new T[cb661c4ff8f251829cdaf0c9d9db2004c];
		CopyTo(array, 0);
		return array;
	}

	/// <summary>
	///       Sets the capacity to the actual number of elements in the <see cref="T:DigitalRune.Collections.Deque`1" />, if that 
	///       number is less than 90 percent of current capacity.
	///       </summary>
	public void TrimExcess()
	{
		if (cb661c4ff8f251829cdaf0c9d9db2004c >= (int)(0.9 * (double)c37adb342465be20dcf2c430c7d6aeb68.Length))
		{
			return;
		}
		while (true)
		{
			switch (2)
			{
			case 0:
				continue;
			}
			if (1 == 0)
			{
				__ldtoken(Deque<>.TrimExcess());
			}
			c6a2746463e155e5e9441b6ec353e4ded(cb661c4ff8f251829cdaf0c9d9db2004c);
			return;
		}
	}

	private bool c463246342cd12c2664fe1e2540ab6cae(T cf51048ba330b8d3f481ea5bc25a03eb9)
	{
		throw new NotSupportedException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4436));
	}

	bool ICollection<T>.Remove(T cf51048ba330b8d3f481ea5bc25a03eb9)
	{
		//ILSpy generated this explicit interface implementation from .override directive in c463246342cd12c2664fe1e2540ab6cae
		return this.c463246342cd12c2664fe1e2540ab6cae(cf51048ba330b8d3f481ea5bc25a03eb9);
	}

	private void c1e6ba5ee3836c2675c13d9949a4ebb28(int c88a85a6fc8e0a66e19d046d4d51cd3f1, T cf51048ba330b8d3f481ea5bc25a03eb9)
	{
		throw new NotSupportedException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4618));
	}

	void IList<T>.Insert(int c88a85a6fc8e0a66e19d046d4d51cd3f1, T cf51048ba330b8d3f481ea5bc25a03eb9)
	{
		//ILSpy generated this explicit interface implementation from .override directive in c1e6ba5ee3836c2675c13d9949a4ebb28
		this.c1e6ba5ee3836c2675c13d9949a4ebb28(c88a85a6fc8e0a66e19d046d4d51cd3f1, cf51048ba330b8d3f481ea5bc25a03eb9);
	}

	private void c8b783ba3da47b2e942f9ba8893e256c3(int c88a85a6fc8e0a66e19d046d4d51cd3f1)
	{
		throw new NotSupportedException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4436));
	}

	void IList<T>.RemoveAt(int c88a85a6fc8e0a66e19d046d4d51cd3f1)
	{
		//ILSpy generated this explicit interface implementation from .override directive in c8b783ba3da47b2e942f9ba8893e256c3
		this.c8b783ba3da47b2e942f9ba8893e256c3(c88a85a6fc8e0a66e19d046d4d51cd3f1);
	}
}
