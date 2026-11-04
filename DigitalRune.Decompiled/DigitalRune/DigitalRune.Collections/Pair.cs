using System;
using System.Collections.Generic;
using A;

namespace DigitalRune.Collections;

/// <summary>
///       Stores two ordered related objects.
///       </summary>
/// <typeparam name="TFirst">The type of the first object.</typeparam>
/// <typeparam name="TSecond">The type of the second object.</typeparam>
[Serializable]
public struct Pair<TFirst, TSecond> : IEquatable<Pair<TFirst, TSecond>>
{
	private static readonly EqualityComparer<TFirst> EqualityComparer1 = EqualityComparer<TFirst>.Default;

	private static readonly EqualityComparer<TSecond> EqualityComparer2 = EqualityComparer<TSecond>.Default;

	private TFirst _first;

	private TSecond _second;

	/// <summary>
	///       Gets or sets the first object.
	///       </summary>
	public TFirst First
	{
		get
		{
			return _first;
		}
		set
		{
			_first = value;
		}
	}

	/// <summary>
	///       Gets or sets the second object.
	///       </summary>
	public TSecond Second
	{
		get
		{
			return _second;
		}
		set
		{
			_second = value;
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Collections.Pair`2" /> class with the given
	///       objects.
	///       </summary>
	/// <param name="first">The first object.</param>
	/// <param name="second">The second object.</param>
	public Pair(TFirst first, TSecond second)
	{
		_first = first;
		_second = second;
	}

	/// <summary>
	///       Determines whether the specified <see cref="T:System.Object" /> is equal to the current 
	///       <see cref="T:System.Object" />.
	///       </summary>
	/// <param name="obj">
	///       The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />.
	///       </param>
	/// <returns>
	///   <see langword="true" /> if the specified <see cref="T:System.Object" /> is equal to the current 
	///       <see cref="T:System.Object" />; otherwise, <see langword="false" />.
	///       </returns>
	public override bool Equals(object obj)
	{
		if (obj == null || !(obj is Pair<TFirst, TSecond>))
		{
			return false;
		}
		return this == (Pair<TFirst, TSecond>)obj;
	}

	/// <summary>
	///       Indicates whether the current object is equal to another object of the same type.
	///       </summary>
	/// <param name="other">An object to compare with this object.</param>
	/// <returns>
	///   <see langword="true" /> if the current object is equal to the other parameter; otherwise, 
	///       <see langword="false" />.
	///       </returns>
	public bool Equals(Pair<TFirst, TSecond> other)
	{
		if (EqualityComparer1.Equals(First, other.First))
		{
			return EqualityComparer2.Equals(Second, other.Second);
		}
		return false;
	}

	/// <summary>
	///       Compares two <see cref="T:DigitalRune.Collections.Pair`2" /> objects to determine whether they are the 
	///       same.
	///       </summary>
	/// <param name="pair1">The first pair.</param>
	/// <param name="pair2">The second pair.</param>
	/// <returns>
	///   <see langword="true" /> if the <paramref name="pair1" /> and <paramref name="pair2" /> are the 
	///       same; otherwise, <see langword="false" />.
	///       </returns>
	public static bool operator ==(Pair<TFirst, TSecond> pair1, Pair<TFirst, TSecond> pair2)
	{
		if (EqualityComparer1.Equals(pair1.First, pair2.First))
		{
			return EqualityComparer2.Equals(pair1.Second, pair2.Second);
		}
		return false;
	}

	/// <summary>
	///       Compares two <see cref="T:DigitalRune.Collections.Pair`2" /> objects to determine whether they are 
	///       different.
	///       </summary>
	/// <param name="pair1">The first pair.</param>
	/// <param name="pair2">The second pair.</param>
	/// <returns>
	///   <see langword="true" /> if the <paramref name="pair1" /> and <paramref name="pair2" /> are 
	///       different; otherwise, <see langword="false" />.
	///       </returns>
	public static bool operator !=(Pair<TFirst, TSecond> pair1, Pair<TFirst, TSecond> pair2)
	{
		if (EqualityComparer1.Equals(pair1.First, pair2.First))
		{
			return !EqualityComparer2.Equals(pair1.Second, pair2.Second);
		}
		return true;
	}

	/// <summary>
	///       Returns the hash code for this instance.
	///       </summary>
	/// <returns>
	///       A 32-bit signed integer that is the hash code for this instance.
	///       </returns>
	public override int GetHashCode()
	{
		int hashCode = EqualityComparer1.GetHashCode(First);
		return (hashCode * 397) ^ EqualityComparer2.GetHashCode(Second);
	}

	/// <summary>
	///       Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
	///       </summary>
	/// <returns>
	///       A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
	///       </returns>
	public override string ToString()
	{
		return string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6538), First, Second);
	}
}
/// <summary>
///       Stores two unordered related objects.
///       </summary>
/// <typeparam name="T">The type of the contained objects.</typeparam>
[Serializable]
public struct Pair<T> : IEquatable<Pair<T>>
{
	private static readonly EqualityComparer<T> EqualityComparer = EqualityComparer<T>.Default;

	private T _first;

	private T _second;

	/// <summary>
	///       Gets or sets the first object.
	///       </summary>
	public T First
	{
		get
		{
			return _first;
		}
		set
		{
			_first = value;
		}
	}

	/// <summary>
	///       Gets or sets the second object.
	///       </summary>
	public T Second
	{
		get
		{
			return _second;
		}
		set
		{
			_second = value;
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Collections.Pair`1" /> class with the given
	///       objects.
	///       </summary>
	/// <param name="first">The first object.</param>
	/// <param name="second">The second object.</param>
	public Pair(T first, T second)
	{
		_first = first;
		_second = second;
	}

	/// <summary>
	///       Determines whether the specified <see cref="T:System.Object" /> is equal to the current 
	///       <see cref="T:System.Object" />.
	///       </summary>
	/// <param name="obj">
	///       The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />.
	///       </param>
	/// <returns>
	///   <see langword="true" /> if the specified <see cref="T:System.Object" /> is equal to the current 
	///       <see cref="T:System.Object" />; otherwise, <see langword="false" />.
	///       </returns>
	public override bool Equals(object obj)
	{
		if (obj == null || !(obj is Pair<T>))
		{
			return false;
		}
		return this == (Pair<T>)obj;
	}

	/// <summary>
	///       Indicates whether the current object is equal to another object of the same type.
	///       </summary>
	/// <param name="other">An object to compare with this object.</param>
	/// <returns>
	///   <see langword="true" /> if the current object is equal to the other parameter; otherwise, 
	///       <see langword="false" />.
	///       </returns>
	public bool Equals(Pair<T> other)
	{
		if (!EqualityComparer.Equals(First, other.First) || !EqualityComparer.Equals(Second, other.Second))
		{
			if (EqualityComparer.Equals(First, other.Second))
			{
				return EqualityComparer.Equals(Second, other.First);
			}
			return false;
		}
		return true;
	}

	/// <summary>
	///       Compares two <see cref="T:DigitalRune.Collections.Pair`1" /> objects to determine whether they are the 
	///       same.
	///       </summary>
	/// <param name="pair1">The first pair.</param>
	/// <param name="pair2">The second pair.</param>
	/// <returns>
	///   <see langword="true" /> if the <paramref name="pair1" /> and <paramref name="pair2" /> are the 
	///       same; otherwise, <see langword="false" />.
	///       </returns>
	public static bool operator ==(Pair<T> pair1, Pair<T> pair2)
	{
		if (!EqualityComparer.Equals(pair1.First, pair2.First) || !EqualityComparer.Equals(pair1.Second, pair2.Second))
		{
			if (EqualityComparer.Equals(pair1.First, pair2.Second))
			{
				return EqualityComparer.Equals(pair1.Second, pair2.First);
			}
			return false;
		}
		return true;
	}

	/// <summary>
	///       Compares two <see cref="T:DigitalRune.Collections.Pair`1" /> objects to determine whether they are 
	///       different.
	///       </summary>
	/// <param name="pair1">The first pair.</param>
	/// <param name="pair2">The second pair.</param>
	/// <returns>
	///   <see langword="true" /> if the <paramref name="pair1" /> and <paramref name="pair2" /> are 
	///       different; otherwise, <see langword="false" />.
	///       </returns>
	public static bool operator !=(Pair<T> pair1, Pair<T> pair2)
	{
		if (!EqualityComparer.Equals(pair1.First, pair2.First) || !EqualityComparer.Equals(pair1.Second, pair2.Second))
		{
			if (EqualityComparer.Equals(pair1.First, pair2.Second))
			{
				return !EqualityComparer.Equals(pair1.Second, pair2.First);
			}
			return true;
		}
		return false;
	}

	/// <summary>
	///       Returns the hash code for this instance.
	///       </summary>
	/// <returns>
	///       A 32-bit signed integer that is the hash code for this instance.
	///       </returns>
	public override int GetHashCode()
	{
		int hashCode = EqualityComparer.GetHashCode(First);
		int hashCode2 = EqualityComparer.GetHashCode(Second);
		if (hashCode != hashCode2)
		{
			return hashCode ^ hashCode2;
		}
		return hashCode;
	}

	/// <summary>
	///       Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
	///       </summary>
	/// <returns>
	///       A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
	///       </returns>
	public override string ToString()
	{
		return string.Format(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6538), First, Second);
	}
}
