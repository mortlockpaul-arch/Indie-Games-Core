using System;
using System.Collections.Generic;
using A;

namespace DigitalRune;

/// <summary>
///       Compares objects for equality by checking whether the specified <see cref="T:System.Object" /> instances 
///       are the same instance.
///       </summary>
/// <typeparam name="T">The type of the objects to compare.</typeparam>
public class ReferenceEqualityComparer<T> : IEqualityComparer<T> where T : class
{
	private static ReferenceEqualityComparer<T> c37dee29878b7612507030321132d2242;

	/// <summary>
	///       Gets a default <see cref="T:DigitalRune.ReferenceEqualityComparer`1" /> for the type specified by the 
	///       generic argument. 
	///       </summary>
	public static ReferenceEqualityComparer<T> Default
	{
		get
		{
			if (c37dee29878b7612507030321132d2242 == null)
			{
				while (true)
				{
					switch (4)
					{
					case 0:
						continue;
					}
					break;
				}
				if (1 == 0)
				{
					__ldtoken(ReferenceEqualityComparer<>.get_Default());
				}
				c37dee29878b7612507030321132d2242 = new ReferenceEqualityComparer<T>();
			}
			return c37dee29878b7612507030321132d2242;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:DigitalRune.ReferenceEqualityComparer`1" /> class</summary>
	public ReferenceEqualityComparer()
	{
	}

	/// <summary>
	///       Determines whether the specified objects are equal.
	///       </summary>
	/// <param name="x">
	///       The first object of type <typeparamref name="T" /> to compare.
	///       </param>
	/// <param name="y">
	///       The second object of type <typeparamref name="T" /> to compare.
	///       </param>
	/// <returns>
	///   <see langword="true" /> if the specified objects are equal; otherwise, 
	///       <see langword="false" />.
	///       </returns>
	public bool Equals(T x, T y)
	{
		return object.ReferenceEquals(x, y);
	}

	/// <summary>
	///       Returns a hash code for the specified object.
	///       </summary>
	/// <param name="obj">The <see cref="T:System.Object" /> for which a hash code is to be returned.</param>
	/// <returns>A hash code for the specified object.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///       The type of <paramref name="obj" /> is a reference type and <paramref name="obj" /> is 
	///       <see langword="null" />.
	///       </exception>
	public int GetHashCode(T obj)
	{
		if (obj == null)
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
						__ldtoken(ReferenceEqualityComparer<>.GetHashCode(T));
					}
					throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5906));
				}
			}
		}
		return obj.GetHashCode();
	}
}
