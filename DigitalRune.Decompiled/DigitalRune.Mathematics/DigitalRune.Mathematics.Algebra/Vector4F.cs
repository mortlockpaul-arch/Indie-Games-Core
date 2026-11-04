using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using A;
using DigitalRune.Mathematics.Algebra.Design;
using Microsoft.Xna.Framework;

namespace DigitalRune.Mathematics.Algebra;

/// <summary>
///       Defines a 4-dimensional vector (single-precision).
///       </summary>
[Serializable]
[TypeConverter(typeof(Vector4FConverter))]
public struct Vector4F : IEquatable<Vector4F>
{
	/// <summary>
	///       Returns a <see cref="T:DigitalRune.Mathematics.Algebra.Vector4F" /> with all of its components set to zero.
	///       </summary>
	public static readonly Vector4F Zero;

	/// <summary>
	///       Returns a <see cref="T:DigitalRune.Mathematics.Algebra.Vector4F" /> with all of its components set to one.
	///       </summary>
	public static readonly Vector4F One;

	/// <summary>
	///       Returns the x unit <see cref="T:DigitalRune.Mathematics.Algebra.Vector4F" /> (1, 0, 0, 0).
	///       </summary>
	public static readonly Vector4F UnitX;

	/// <summary>
	///       Returns the y unit <see cref="T:DigitalRune.Mathematics.Algebra.Vector4F" /> (0, 1, 0, 0).
	///       </summary>
	public static readonly Vector4F UnitY;

	/// <summary>
	///       Returns the z unit <see cref="T:DigitalRune.Mathematics.Algebra.Vector4F" /> (0, 0, 1, 0).
	///       </summary>
	public static readonly Vector4F UnitZ;

	/// <summary>
	///       Returns the w unit <see cref="T:DigitalRune.Mathematics.Algebra.Vector4F" /> (0, 0, 0, 1).
	///       </summary>
	public static readonly Vector4F UnitW;

	/// <summary>
	///       The x component.
	///       </summary>
	public float X;

	/// <summary>
	///       The y component.
	///       </summary>
	public float Y;

	/// <summary>
	///       The z component.
	///       </summary>
	public float Z;

	/// <summary>
	///       The w component.
	///       </summary>
	public float W;

	/// <summary>
	///       Gets or sets the component at the specified index.
	///       </summary>
	/// <param name="index">The index.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///       The <paramref name="index" /> is out of range.
	///       </exception>
	public float this[int index]
	{
		get
		{
			return index switch
			{
				0 => X, 
				1 => Y, 
				2 => Z, 
				3 => W, 
				_ => throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(622), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1175)), 
			};
		}
		set
		{
			switch (index)
			{
			case 0:
				X = value;
				break;
			case 1:
				Y = value;
				break;
			case 2:
				Z = value;
				break;
			case 3:
				W = value;
				break;
			default:
				throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(622), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1175));
			}
		}
	}

	/// <summary>
	///       Gets a value indicating whether a component of the vector is <see cref="F:System.Single.NaN" />.
	///       </summary>
	public bool IsNaN
	{
		get
		{
			if (!Numeric.IsNaN(X) && !Numeric.IsNaN(Y) && !Numeric.IsNaN(Z))
			{
				return Numeric.IsNaN(W);
			}
			return true;
		}
	}

	/// <summary>
	///       Returns a value indicating whether this vector is normalized (the length is numerically
	///       equal to 1).
	///       </summary>
	public bool IsNumericallyNormalized => Numeric.AreEqual(LengthSquared, 1f);

	/// <summary>
	///       Returns a value indicating whether this vector has zero size (the length is numerically
	///       equal to 0).
	///       </summary>
	public bool IsNumericallyZero => Numeric.IsZero(LengthSquared, Numeric.EpsilonFSquared);

	/// <summary>
	///       Gets or sets the length of this vector.
	///       </summary>
	/// <returns>The length of the this vector.</returns>
	/// <exception cref="T:DigitalRune.Mathematics.MathematicsException">
	///       The vector has a length of 0. The length cannot be changed.
	///       </exception>
	[XmlIgnore]
	public float Length
	{
		get
		{
			return (float)Math.Sqrt(X * X + Y * Y + Z * Z + W * W);
		}
		set
		{
			float length = Length;
			if (Numeric.IsZero(length))
			{
				throw new MathematicsException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(748));
			}
			float num = value / length;
			X *= num;
			Y *= num;
			Z *= num;
			W *= num;
		}
	}

	/// <summary>
	///       Returns the squared length of this vector.
	///       </summary>
	/// <returns>The squared length of this vector.</returns>
	public float LengthSquared => X * X + Y * Y + Z * Z + W * W;

	/// <summary>
	///       Returns the normalized vector.
	///       </summary>
	/// <exception cref="T:System.DivideByZeroException">
	///       The length of the vector is zero. The quaternion cannot be normalized.
	///       </exception>
	public Vector4F Normalized
	{
		get
		{
			Vector4F result = this;
			result.Normalize();
			return result;
		}
	}

	/// <summary>
	///       Gets the components x, y and z as a <see cref="T:DigitalRune.Mathematics.Algebra.Vector3F" />.
	///       </summary>
	public Vector3F XYZ => new Vector3F(X, Y, Z);

	/// <summary>
	///       Gets the value of the largest component.
	///       </summary>
	public float LargestComponent
	{
		get
		{
			if (X >= Y && X >= Z && X >= W)
			{
				return X;
			}
			if (Y >= Z && Y >= W)
			{
				return Y;
			}
			if (Z >= W)
			{
				return Z;
			}
			return W;
		}
	}

	/// <summary>
	///       Gets the index (zero-based) of the largest component.
	///       </summary>
	public int IndexOfLargestComponent
	{
		get
		{
			if (X >= Y && X >= Z && X >= W)
			{
				return 0;
			}
			if (Y >= Z && Y >= W)
			{
				return 1;
			}
			if (Z >= W)
			{
				return 2;
			}
			return 3;
		}
	}

	/// <summary>
	///       Gets the value of the largest component.
	///       </summary>
	public float SmallestComponent
	{
		get
		{
			if (X <= Y && X <= Z && X <= W)
			{
				return X;
			}
			if (Y <= Z && Y <= W)
			{
				return Y;
			}
			if (Z <= W)
			{
				return Z;
			}
			return W;
		}
	}

	/// <summary>
	///       Gets the index (zero-based) of the smallest component.
	///       </summary>
	public int IndexOfSmallestComponent
	{
		get
		{
			if (X <= Y && X <= Z && X <= W)
			{
				return 0;
			}
			if (Y <= Z && Y <= W)
			{
				return 1;
			}
			if (Z <= W)
			{
				return 2;
			}
			return 3;
		}
	}

	static Vector4F()
	{
		Zero = new Vector4F(0f, 0f, 0f, 0f);
		One = new Vector4F(1f, 1f, 1f, 1f);
		UnitX = new Vector4F(1f, 0f, 0f, 0f);
		UnitY = new Vector4F(0f, 1f, 0f, 0f);
		UnitZ = new Vector4F(0f, 0f, 1f, 0f);
		UnitW = new Vector4F(0f, 0f, 0f, 1f);
		TypeInitializer.Initialize(cb8acf96051824377b85f63be63e3c2a3.ce9f9bc9ecd5ed400a711abaa33452c25);
		TypeInitializer.CreateInstance(11, cb8acf96051824377b85f63be63e3c2a3.ccda5e1cdd8faa90b24f6bc28c9af6b28);
	}

	/// <summary>
	///       Initializes a new instance of <see cref="T:DigitalRune.Mathematics.Algebra.Vector4F" />.
	///       </summary>
	/// <param name="x">Initial value for the x component.</param>
	/// <param name="y">Initial value for the y component.</param>
	/// <param name="z">Initial value for the z component.</param>
	/// <param name="w">Initial value for the z component.</param>
	public Vector4F(float x, float y, float z, float w)
	{
		X = x;
		Y = y;
		Z = z;
		W = w;
	}

	/// <summary>
	///       Initializes a new instance of <see cref="T:DigitalRune.Mathematics.Algebra.Vector4F" />.
	///       </summary>
	/// <param name="componentValue">The initial value for 4 the vector components.</param>
	public Vector4F(float componentValue)
	{
		X = componentValue;
		Y = componentValue;
		Z = componentValue;
		W = componentValue;
	}

	/// <summary>
	///       Initializes a new instance of <see cref="T:DigitalRune.Mathematics.Algebra.Vector4F" />.
	///       </summary>
	/// <param name="components">
	///       Array with the initial values for the components x, y, z and w.
	///       </param>
	/// <exception cref="T:System.IndexOutOfRangeException">
	///   <paramref name="components" /> has less than 4 elements.
	///       </exception>
	/// <exception cref="T:System.NullReferenceException">
	///   <paramref name="components" /> must not be <see langword="null" />.
	///       </exception>
	public Vector4F(float[] components)
	{
		X = components[0];
		Y = components[1];
		Z = components[2];
		W = components[3];
	}

	/// <summary>
	///       Initializes a new instance of <see cref="T:DigitalRune.Mathematics.Algebra.Vector4F" />.
	///       </summary>
	/// <param name="components">
	///       List with the initial values for the components x, y, z and w.
	///       </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="components" /> has less than 4 elements.
	///       </exception>
	/// <exception cref="T:System.NullReferenceException">
	///   <paramref name="components" /> must not be <see langword="null" />.
	///       </exception>
	public Vector4F(IList<float> components)
	{
		X = components[0];
		Y = components[1];
		Z = components[2];
		W = components[3];
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Algebra.Vector4F" /> class.
	///       </summary>
	/// <param name="vector">The vector (x, y, z).</param>
	/// <param name="w">The w component.</param>
	public Vector4F(Vector3F vector, float w)
	{
		X = vector.X;
		Y = vector.Y;
		Z = vector.Z;
		W = w;
	}

	/// <summary>
	///       Returns the hash code for this instance.
	///       </summary>
	/// <returns>
	///       A 32-bit signed integer that is the hash code for this instance.
	///       </returns>
	public override int GetHashCode()
	{
		int hashCode = X.GetHashCode();
		hashCode = (hashCode * 397) ^ Y.GetHashCode();
		hashCode = (hashCode * 397) ^ Z.GetHashCode();
		return (hashCode * 397) ^ W.GetHashCode();
	}

	/// <summary>
	///       Indicates whether this instance and a specified object are equal.
	///       </summary>
	/// <param name="obj">Another object to compare to.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="obj" /> and this instance are the same 
	///       type and represent the same value; otherwise, <see langword="false" />.
	///       </returns>
	public override bool Equals(object obj)
	{
		if (obj is Vector4F)
		{
			return this == (Vector4F)obj;
		}
		return false;
	}

	/// <summary>
	///       Indicates whether the current object is equal to another object of the same type.
	///       </summary>
	/// <param name="other">An object to compare with this object.</param>
	/// <returns>
	///   <see langword="true" /> if the current object is equal to the other parameter; otherwise, 
	///       <see langword="false" />.
	///       </returns>
	public bool Equals(Vector4F other)
	{
		return this == other;
	}

	/// <summary>
	///       Returns the string representation of this vector.
	///       </summary>
	/// <returns>The string representation of this vector.</returns>
	public override string ToString()
	{
		return ToString(CultureInfo.CurrentCulture);
	}

	/// <summary>
	///       Returns the string representation of this vector using the specified 
	///       culture-specific format information.
	///       </summary>
	/// <param name="provider">
	///       An <see cref="T:System.IFormatProvider" /> that supplies culture-specific 
	///       formatting information
	///       </param>
	/// <returns>The string representation of this vector.</returns>
	public string ToString(IFormatProvider provider)
	{
		return string.Format(provider, c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2874), X, Y, Z, W);
	}

	/// <summary>
	///       Negates a vector.
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <returns>The negated vector.</returns>
	public static Vector4F operator -(Vector4F vector)
	{
		vector.X = 0f - vector.X;
		vector.Y = 0f - vector.Y;
		vector.Z = 0f - vector.Z;
		vector.W = 0f - vector.W;
		return vector;
	}

	/// <summary>
	///       Negates a vector.
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <returns>The negated vector.</returns>
	public static Vector4F Negate(Vector4F vector)
	{
		vector.X = 0f - vector.X;
		vector.Y = 0f - vector.Y;
		vector.Z = 0f - vector.Z;
		vector.W = 0f - vector.W;
		return vector;
	}

	/// <summary>
	///       Adds two vectors.
	///       </summary>
	/// <param name="vector1">The first vector.</param>
	/// <param name="vector2">The second vector.</param>
	/// <returns>The sum of the two vectors.</returns>
	public static Vector4F operator +(Vector4F vector1, Vector4F vector2)
	{
		vector1.X += vector2.X;
		vector1.Y += vector2.Y;
		vector1.Z += vector2.Z;
		vector1.W += vector2.W;
		return vector1;
	}

	/// <summary>
	///       Adds two vectors.
	///       </summary>
	/// <param name="vector1">The first vector.</param>
	/// <param name="vector2">The second vector.</param>
	/// <returns>The sum of the two vectors.</returns>
	public static Vector4F Add(Vector4F vector1, Vector4F vector2)
	{
		vector1.X += vector2.X;
		vector1.Y += vector2.Y;
		vector1.Z += vector2.Z;
		vector1.W += vector2.W;
		return vector1;
	}

	/// <summary>
	///       Subtracts a vector from a vector.
	///       </summary>
	/// <param name="minuend">The first vector (minuend).</param>
	/// <param name="subtrahend">The second vector (subtrahend).</param>
	/// <returns>The difference of the two vectors.</returns>
	public static Vector4F operator -(Vector4F minuend, Vector4F subtrahend)
	{
		minuend.X -= subtrahend.X;
		minuend.Y -= subtrahend.Y;
		minuend.Z -= subtrahend.Z;
		minuend.W -= subtrahend.W;
		return minuend;
	}

	/// <summary>
	///       Subtracts a vector from a vector.
	///       </summary>
	/// <param name="minuend">The first vector (minuend).</param>
	/// <param name="subtrahend">The second vector (subtrahend).</param>
	/// <returns>The difference of the two vectors.</returns>
	public static Vector4F Subtract(Vector4F minuend, Vector4F subtrahend)
	{
		minuend.X -= subtrahend.X;
		minuend.Y -= subtrahend.Y;
		minuend.Z -= subtrahend.Z;
		minuend.W -= subtrahend.W;
		return minuend;
	}

	/// <summary>
	///       Multiplies a vector by a scalar.
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <param name="scalar">The scalar.</param>
	/// <returns>The vector with each component multiplied by scalar.</returns>
	public static Vector4F operator *(Vector4F vector, float scalar)
	{
		vector.X *= scalar;
		vector.Y *= scalar;
		vector.Z *= scalar;
		vector.W *= scalar;
		return vector;
	}

	/// <summary>
	///       Multiplies a vector by a scalar.
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <param name="scalar">The scalar.</param>
	/// <returns>
	///       The vector with each component multiplied by <paramref name="scalar" />.
	///       </returns>
	public static Vector4F operator *(float scalar, Vector4F vector)
	{
		vector.X *= scalar;
		vector.Y *= scalar;
		vector.Z *= scalar;
		vector.W *= scalar;
		return vector;
	}

	/// <summary>
	///       Multiplies a vector by a scalar.
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <param name="scalar">The scalar.</param>
	/// <returns>
	///       The vector with each component multiplied by <paramref name="scalar" />.
	///       </returns>
	public static Vector4F Multiply(float scalar, Vector4F vector)
	{
		vector.X *= scalar;
		vector.Y *= scalar;
		vector.Z *= scalar;
		vector.W *= scalar;
		return vector;
	}

	/// <summary>
	///       Multiplies the components of two vectors by each other.
	///       </summary>
	/// <param name="vector1">The first vector.</param>
	/// <param name="vector2">The second vector.</param>
	/// <returns>The component-wise product of the two vectors.</returns>
	public static Vector4F operator *(Vector4F vector1, Vector4F vector2)
	{
		vector1.X *= vector2.X;
		vector1.Y *= vector2.Y;
		vector1.Z *= vector2.Z;
		vector1.W *= vector2.W;
		return vector1;
	}

	/// <summary>
	///       Multiplies the components of two vectors by each other.
	///       </summary>
	/// <param name="vector1">The first vector.</param>
	/// <param name="vector2">The second vector.</param>
	/// <returns>The component-wise product of the two vectors.</returns>
	public static Vector4F Multiply(Vector4F vector1, Vector4F vector2)
	{
		vector1.X *= vector2.X;
		vector1.Y *= vector2.Y;
		vector1.Z *= vector2.Z;
		vector1.W *= vector2.W;
		return vector1;
	}

	/// <summary>
	///       Divides a vector by a scalar.
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <param name="scalar">The scalar.</param>
	/// <returns>
	///       The vector with each component divided by <paramref name="scalar" />.
	///       </returns>
	public static Vector4F operator /(Vector4F vector, float scalar)
	{
		float num = 1f / scalar;
		vector.X *= num;
		vector.Y *= num;
		vector.Z *= num;
		vector.W *= num;
		return vector;
	}

	/// <summary>
	///       Divides a vector by a scalar.
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <param name="scalar">The scalar.</param>
	/// <returns>
	///       The vector with each component divided by <paramref name="scalar" />.
	///       </returns>
	public static Vector4F Divide(Vector4F vector, float scalar)
	{
		float num = 1f / scalar;
		vector.X *= num;
		vector.Y *= num;
		vector.Z *= num;
		vector.W *= num;
		return vector;
	}

	/// <summary>
	///       Divides the components of a vector by the components of another 
	///       vector.
	///       </summary>
	/// <param name="dividend">The first vector (dividend).</param>
	/// <param name="divisor">The second vector (divisor).</param>
	/// <returns>The component-wise product of the two vectors.</returns>
	public static Vector4F operator /(Vector4F dividend, Vector4F divisor)
	{
		dividend.X /= divisor.X;
		dividend.Y /= divisor.Y;
		dividend.Z /= divisor.Z;
		dividend.W /= divisor.W;
		return dividend;
	}

	/// <summary>
	///       Divides the components of a vector by the components of another 
	///       vector.
	///       </summary>
	/// <param name="dividend">The first vector (dividend).</param>
	/// <param name="divisor">The second vector (divisor).</param>
	/// <returns>The component-wise division of the two vectors.</returns>
	public static Vector4F Divide(Vector4F dividend, Vector4F divisor)
	{
		dividend.X /= divisor.X;
		dividend.Y /= divisor.Y;
		dividend.Z /= divisor.Z;
		dividend.W /= divisor.W;
		return dividend;
	}

	/// <summary>
	///       Tests if two vectors are equal.
	///       </summary>
	/// <param name="vector1">The first vector.</param>
	/// <param name="vector2">The second vector.</param>
	/// <returns>
	///   <see langword="true" /> if the vectors are equal; otherwise <see langword="false" />.
	///       </returns>
	public static bool operator ==(Vector4F vector1, Vector4F vector2)
	{
		if (vector1.X == vector2.X && vector1.Y == vector2.Y && vector1.Z == vector2.Z)
		{
			return vector1.W == vector2.W;
		}
		return false;
	}

	/// <summary>
	///       Tests if two vectors are not equal.
	///       </summary>
	/// <param name="vector1">The first vector.</param>
	/// <param name="vector2">The second vector.</param>
	/// <returns>
	///   <see langword="true" /> if the vectors are different; otherwise <see langword="false" />.
	///       </returns>
	public static bool operator !=(Vector4F vector1, Vector4F vector2)
	{
		if (vector1.X == vector2.X && vector1.Y == vector2.Y && vector1.Z == vector2.Z)
		{
			return vector1.W != vector2.W;
		}
		return true;
	}

	/// <summary>
	///       Tests if each component of a vector is greater than the corresponding 
	///       component of another vector.
	///       </summary>
	/// <param name="vector1">The first vector.</param>
	/// <param name="vector2">The second vector.</param>
	/// <returns>
	///       True if each component of <paramref name="vector1" /> is greater than its 
	///       counterpart in <paramref name="vector2" />.
	///       </returns>
	public static bool operator >(Vector4F vector1, Vector4F vector2)
	{
		if (vector1.X > vector2.X && vector1.Y > vector2.Y && vector1.Z > vector2.Z)
		{
			return vector1.W > vector2.W;
		}
		return false;
	}

	/// <summary>
	///       Tests if each component of a vector is greater or equal than the 
	///       corresponding component of another vector.
	///       </summary>
	/// <param name="vector1">The first vector.</param>
	/// <param name="vector2">The second vector.</param>
	/// <returns>
	///       True if each component of <paramref name="vector1" /> is greater or equal than
	///       its counterpart in <paramref name="vector2" />.
	///       </returns>
	public static bool operator >=(Vector4F vector1, Vector4F vector2)
	{
		if (vector1.X >= vector2.X && vector1.Y >= vector2.Y && vector1.Z >= vector2.Z)
		{
			return vector1.W >= vector2.W;
		}
		return false;
	}

	/// <summary>
	///       Tests if each component of a vector is less than the corresponding 
	///       component of another vector.
	///       </summary>
	/// <param name="vector1">The first vector.</param>
	/// <param name="vector2">The second vector.</param>
	/// <returns>
	///       True if each component of <paramref name="vector1" /> is less than its 
	///       counterpart in <paramref name="vector2" />.
	///       </returns>
	public static bool operator <(Vector4F vector1, Vector4F vector2)
	{
		if (vector1.X < vector2.X && vector1.Y < vector2.Y && vector1.Z < vector2.Z)
		{
			return vector1.W < vector2.W;
		}
		return false;
	}

	/// <summary>
	///       Tests if each component of a vector is less or equal than the 
	///       corresponding component of another vector.
	///       </summary>
	/// <param name="vector1">The first vector.</param>
	/// <param name="vector2">The second vector.</param>
	/// <returns>
	///       True if each component of <paramref name="vector1" /> is less or equal than
	///       its counterpart in <paramref name="vector2" />.
	///       </returns>
	public static bool operator <=(Vector4F vector1, Vector4F vector2)
	{
		if (vector1.X <= vector2.X && vector1.Y <= vector2.Y && vector1.Z <= vector2.Z)
		{
			return vector1.W <= vector2.W;
		}
		return false;
	}

	/// <summary>
	///       Converts a vector to an array of 4 float values.
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <returns>The array with 4 values.</returns>
	public static explicit operator float[](Vector4F vector)
	{
		return new float[4] { vector.X, vector.Y, vector.Z, vector.W };
	}

	/// <summary>
	///       Converts this vector to an array of 4 float values.
	///       </summary>
	/// <returns>The array with 4 values.</returns>
	public float[] ToArray()
	{
		return (float[])this;
	}

	/// <summary>
	///       Converts a vector to a list of 4 float values.
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <returns>The list with 4 values.</returns>
	public static explicit operator List<float>(Vector4F vector)
	{
		List<float> list = new List<float>(4);
		list.Add(vector.X);
		list.Add(vector.Y);
		list.Add(vector.Z);
		list.Add(vector.W);
		return list;
	}

	/// <summary>
	///       Converts this vector to a list of 4 float values.
	///       </summary>
	/// <returns>The list with 4 values.</returns>
	public List<float> ToList()
	{
		return (List<float>)this;
	}

	/// <summary>
	///       Performs an implicit conversion from <see cref="T:DigitalRune.Mathematics.Algebra.Vector4F" /> 
	///       to <see cref="T:DigitalRune.Mathematics.Algebra.Vector4D" />.
	///       </summary>
	/// <param name="vector">The DigitalRune <see cref="T:DigitalRune.Mathematics.Algebra.Vector4F" />.</param>
	/// <returns>The result of the conversion.</returns>
	public static implicit operator Vector4D(Vector4F vector)
	{
		return new Vector4D(vector.X, vector.Y, vector.Z, vector.W);
	}

	/// <summary>
	///       Converts this <see cref="T:DigitalRune.Mathematics.Algebra.Vector4F" /> to <see cref="T:DigitalRune.Mathematics.Algebra.Vector4D" />.
	///       </summary>
	/// <returns>The result of the conversion.</returns>
	public Vector4D ToVector4D()
	{
		return new Vector4D(X, Y, Z, W);
	}

	/// <summary>
	///       Performs an implicit conversion from <see cref="T:DigitalRune.Mathematics.Algebra.Vector4F" /> to <see cref="T:DigitalRune.Mathematics.Algebra.VectorF" />.
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <returns>The result of the conversion.</returns>
	public static implicit operator VectorF(Vector4F vector)
	{
		VectorF vectorF = new VectorF(4);
		vectorF[0] = vector.X;
		vectorF[1] = vector.Y;
		vectorF[2] = vector.Z;
		vectorF[3] = vector.W;
		return vectorF;
	}

	/// <summary>
	///       Converts this <see cref="T:DigitalRune.Mathematics.Algebra.Vector4F" /> to <see cref="T:DigitalRune.Mathematics.Algebra.VectorF" />.
	///       </summary>
	/// <returns>The result of the conversion.</returns>
	public VectorF ToVectorF()
	{
		return this;
	}

	/// <summary>
	///       Performs an conversion from <see cref="T:Microsoft.Xna.Framework.Vector4" /> (XNA Framework) to 
	///       <see cref="T:DigitalRune.Mathematics.Algebra.Vector4F" /> (DigitalRune Mathematics).
	///       </summary>
	/// <param name="vector">The <see cref="T:Microsoft.Xna.Framework.Vector4" /> (XNA Framework).</param>
	/// <returns>The <see cref="T:DigitalRune.Mathematics.Algebra.Vector4F" /> (DigitalRune Mathematics).</returns>
	public static explicit operator Vector4F(Vector4 vector)
	{
		return new Vector4F(vector.X, vector.Y, vector.Z, vector.W);
	}

	/// <summary>
	///       Converts this <see cref="T:DigitalRune.Mathematics.Algebra.Vector4F" /> (DigitalRune Mathematics) to <see cref="T:Microsoft.Xna.Framework.Vector4" /> 
	///       (XNA Framework).
	///       </summary>
	/// <param name="vector">The <see cref="T:Microsoft.Xna.Framework.Vector4" /> (XNA Framework).</param>
	/// <returns>The <see cref="T:DigitalRune.Mathematics.Algebra.Vector4F" /> (DigitalRune Mathematics).</returns>
	public static Vector4F FromXna(Vector4 vector)
	{
		return new Vector4F(vector.X, vector.Y, vector.Z, vector.W);
	}

	/// <summary>
	///       Performs an conversion from <see cref="T:DigitalRune.Mathematics.Algebra.Vector4F" /> 
	///       (DigitalRune Mathematics) to <see cref="T:Microsoft.Xna.Framework.Vector4" /> (XNA Framework).
	///       </summary>
	/// <param name="vector">The <see cref="T:DigitalRune.Mathematics.Algebra.Vector4F" /> (DigitalRune Mathematics).</param>
	/// <returns>The <see cref="T:Microsoft.Xna.Framework.Vector4" /> (XNA Framework).</returns>
	public static explicit operator Vector4(Vector4F vector)
	{
		return new Vector4(vector.X, vector.Y, vector.Z, vector.W);
	}

	/// <summary>
	///       Converts this <see cref="T:DigitalRune.Mathematics.Algebra.Vector4F" /> (DigitalRune Mathematics) to <see cref="T:Microsoft.Xna.Framework.Vector4" /> 
	///       (XNA Framework).
	///       </summary>
	/// <returns>The <see cref="T:Microsoft.Xna.Framework.Vector4" /> (XNA Framework).</returns>
	public Vector4 ToXna()
	{
		return new Vector4(X, Y, Z, W);
	}

	/// <summary>
	///       Sets each vector component to its absolute value.
	///       </summary>
	public void Absolute()
	{
		X = Math.Abs(X);
		Y = Math.Abs(Y);
		Z = Math.Abs(Z);
		W = Math.Abs(W);
	}

	/// <summary>
	///       Clamps the vector components to the range [min, max].
	///       </summary>
	/// <param name="min">The min limit.</param>
	/// <param name="max">The max limit.</param>
	public void Clamp(float min, float max)
	{
		X = MathHelper.Clamp(X, min, max);
		Y = MathHelper.Clamp(Y, min, max);
		Z = MathHelper.Clamp(Z, min, max);
		W = MathHelper.Clamp(W, min, max);
	}

	/// <summary>
	///       Clamps near-zero vector components to zero.
	///       </summary>
	public void ClampToZero()
	{
		X = Numeric.ClampToZero(X);
		Y = Numeric.ClampToZero(Y);
		Z = Numeric.ClampToZero(Z);
		W = Numeric.ClampToZero(W);
	}

	/// <summary>
	///       Clamps near-zero vector components to zero.
	///       </summary>
	/// <param name="epsilon">The tolerance value.</param>
	public void ClampToZero(float epsilon)
	{
		X = Numeric.ClampToZero(X, epsilon);
		Y = Numeric.ClampToZero(Y, epsilon);
		Z = Numeric.ClampToZero(Z, epsilon);
		W = Numeric.ClampToZero(W, epsilon);
	}

	/// <summary>
	///       Normalizes the vector.
	///       </summary>
	/// <exception cref="T:System.DivideByZeroException">
	///       The length of this vector is zero. The vector cannot be normalized.
	///       </exception>
	public void Normalize()
	{
		float length = Length;
		if (Numeric.IsZero(length))
		{
			throw new DivideByZeroException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(874));
		}
		float num = 1f / length;
		X *= num;
		Y *= num;
		Z *= num;
		W *= num;
	}

	/// <summary>
	///       Tries to normalize the vector.
	///       </summary>
	/// <returns>
	///   <see langword="true" /> if the vector was normalized; otherwise, <see langword="false" /> if 
	///       the vector could not be normalized. (The length is numerically zero.)
	///       </returns>
	public bool TryNormalize()
	{
		float lengthSquared = LengthSquared;
		if (Numeric.IsZero(lengthSquared, Numeric.EpsilonFSquared))
		{
			return false;
		}
		float num = (float)Math.Sqrt(lengthSquared);
		float num2 = 1f / num;
		X *= num2;
		Y *= num2;
		Z *= num2;
		W *= num2;
		return true;
	}

	/// <summary>
	///       Sets this vector to its projection onto the axis given by the target vector.
	///       </summary>
	/// <param name="target">The target vector.</param>
	public void ProjectTo(Vector4F target)
	{
		this = Dot(this, target) / target.LengthSquared * target;
	}

	/// <summary>
	///       Returns a vector with the absolute values of the elements of the given vector.
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <returns>A vector with the absolute values of the elements of the given vector.</returns>
	public static Vector4F Absolute(Vector4F vector)
	{
		return new Vector4F(Math.Abs(vector.X), Math.Abs(vector.Y), Math.Abs(vector.Z), Math.Abs(vector.W));
	}

	/// <summary>
	///       Determines whether two vectors are equal (regarding the 
	///       tolerance <see cref="P:DigitalRune.Mathematics.Numeric.EpsilonF" />).
	///       </summary>
	/// <param name="vector1">The first vector.</param>
	/// <param name="vector2">The second vector.</param>
	/// <returns>
	///   <see langword="true" /> if the vectors are equal (within the tolerance 
	///       <see cref="P:DigitalRune.Mathematics.Numeric.EpsilonF" />); otherwise, <see langword="false" />.
	///       </returns>
	public static bool AreNumericallyEqual(Vector4F vector1, Vector4F vector2)
	{
		if (Numeric.AreEqual(vector1.X, vector2.X) && Numeric.AreEqual(vector1.Y, vector2.Y) && Numeric.AreEqual(vector1.Z, vector2.Z))
		{
			return Numeric.AreEqual(vector1.W, vector2.W);
		}
		return false;
	}

	/// <summary>
	///       Determines whether two vectors are equal (regarding a specific 
	///       tolerance).
	///       </summary>
	/// <param name="vector1">The first vector.</param>
	/// <param name="vector2">The second vector.</param>
	/// <param name="epsilon">The tolerance value.</param>
	/// <returns>
	///   <see langword="true" /> if the vectors are equal (within the tolerance 
	///       <paramref name="epsilon" />); otherwise, <see langword="false" />.
	///       </returns>
	public static bool AreNumericallyEqual(Vector4F vector1, Vector4F vector2, float epsilon)
	{
		if (Numeric.AreEqual(vector1.X, vector2.X, epsilon) && Numeric.AreEqual(vector1.Y, vector2.Y, epsilon) && Numeric.AreEqual(vector1.Z, vector2.Z, epsilon))
		{
			return Numeric.AreEqual(vector1.W, vector2.W, epsilon);
		}
		return false;
	}

	/// <summary>
	///       Returns a vector with the vector components clamped to the range [min, max].
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <param name="min">The min limit.</param>
	/// <param name="max">The max limit.</param>
	/// <returns>A vector with clamped components.</returns>
	public static Vector4F Clamp(Vector4F vector, float min, float max)
	{
		return new Vector4F(MathHelper.Clamp(vector.X, min, max), MathHelper.Clamp(vector.Y, min, max), MathHelper.Clamp(vector.Z, min, max), MathHelper.Clamp(vector.W, min, max));
	}

	/// <summary>
	///       Returns a vector with near-zero vector components clamped to 0.
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <returns>
	///       The vector with small components clamped to zero.
	///       </returns>
	public static Vector4F ClampToZero(Vector4F vector)
	{
		vector.X = Numeric.ClampToZero(vector.X);
		vector.Y = Numeric.ClampToZero(vector.Y);
		vector.Z = Numeric.ClampToZero(vector.Z);
		vector.W = Numeric.ClampToZero(vector.W);
		return vector;
	}

	/// <summary>
	///       Returns a vector with near-zero vector components clamped to 0.
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <param name="epsilon">The tolerance value.</param>
	/// <returns>
	///       The vector with small components clamped to zero.
	///       </returns>
	public static Vector4F ClampToZero(Vector4F vector, float epsilon)
	{
		vector.X = Numeric.ClampToZero(vector.X, epsilon);
		vector.Y = Numeric.ClampToZero(vector.Y, epsilon);
		vector.Z = Numeric.ClampToZero(vector.Z, epsilon);
		vector.W = Numeric.ClampToZero(vector.W, epsilon);
		return vector;
	}

	/// <summary>
	///       Returns the dot product of two vectors.
	///       </summary>
	/// <param name="vector1">The first vector.</param>
	/// <param name="vector2">The second vector.</param>
	/// <returns>The dot product.</returns>
	public static float Dot(Vector4F vector1, Vector4F vector2)
	{
		return vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z + vector1.W * vector2.W;
	}

	/// <summary>
	///       Performs the homogeneous divide or perspective divide: X, Y and Z are divided by W.
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <returns>The vector (X/W, Y/W, Z/W).</returns>
	/// <exception cref="T:System.DivideByZeroException">Component W is 0.</exception>
	public static Vector3F HomogeneousDivide(Vector4F vector)
	{
		float w = vector.W;
		if (w == 1f)
		{
			return new Vector3F(vector.X, vector.Y, vector.Z);
		}
		float num = 1f / w;
		return new Vector3F(vector.X * num, vector.Y * num, vector.Z * num);
	}

	/// <summary>
	///       Returns a vector that contains the lowest value from each matching pair of components.
	///       </summary>
	/// <param name="vector1">The first vector.</param>
	/// <param name="vector2">The second vector.</param>
	/// <returns>The minimized vector.</returns>
	public static Vector4F Min(Vector4F vector1, Vector4F vector2)
	{
		vector1.X = Math.Min(vector1.X, vector2.X);
		vector1.Y = Math.Min(vector1.Y, vector2.Y);
		vector1.Z = Math.Min(vector1.Z, vector2.Z);
		vector1.W = Math.Min(vector1.W, vector2.W);
		return vector1;
	}

	/// <summary>
	///       Returns a vector that contains the highest value from each matching pair of components.
	///       </summary>
	/// <param name="vector1">The first vector.</param>
	/// <param name="vector2">The second vector.</param>
	/// <returns>The maximized vector.</returns>
	public static Vector4F Max(Vector4F vector1, Vector4F vector2)
	{
		vector1.X = Math.Max(vector1.X, vector2.X);
		vector1.Y = Math.Max(vector1.Y, vector2.Y);
		vector1.Z = Math.Max(vector1.Z, vector2.Z);
		vector1.W = Math.Max(vector1.W, vector2.W);
		return vector1;
	}

	/// <summary>
	///       Projects a vector onto an axis given by the target vector.
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <param name="target">The target vector.</param>
	/// <returns>
	///       The projection of <paramref name="vector" /> onto <paramref name="target" />.
	///       </returns>
	public static Vector4F ProjectTo(Vector4F vector, Vector4F target)
	{
		return Dot(vector, target) / target.LengthSquared * target;
	}

	/// <summary>
	///       Converts the string representation of a 4-dimensional vector to its 
	///       <see cref="T:DigitalRune.Mathematics.Algebra.Vector4F" /> equivalent.
	///       </summary>
	/// <param name="s">A string representation of a 4-dimensional vector.</param>
	/// <returns>
	///       A <see cref="T:DigitalRune.Mathematics.Algebra.Vector4F" /> that represents the vector specified by the 
	///       <paramref name="s" /> parameter.
	///       </returns>
	public static Vector4F Parse(string s)
	{
		return Parse(s, CultureInfo.CurrentCulture);
	}

	/// <summary>
	///       Converts the string representation of a 4-dimensional vector in a specified 
	///       culture-specific format to its <see cref="T:DigitalRune.Mathematics.Algebra.Vector4F" /> equivalent.
	///       </summary>
	/// <param name="s">A string representation of a 4-dimensional vector.</param>
	/// <param name="provider">
	///       An <see cref="T:System.IFormatProvider" /> that supplies culture-specific formatting 
	///       information about <paramref name="s" />. 
	///       </param>
	/// <returns>
	///       A <see cref="T:DigitalRune.Mathematics.Algebra.Vector4F" /> that represents the vector specified by the 
	///       <paramref name="s" /> parameter.</returns>
	/// <exception cref="T:System.FormatException">
	///   <paramref name="s" /> is not a valid <see cref="T:DigitalRune.Mathematics.Algebra.Vector4F" />.
	///       </exception>
	public static Vector4F Parse(string s, IFormatProvider provider)
	{
		Regex regex = new Regex(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2915), RegexOptions.None);
		Match match = regex.Match(s);
		if (match.Success)
		{
			return new Vector4F(float.Parse(match.Groups[c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1103)].Value, provider), float.Parse(match.Groups[c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1106)].Value, provider), float.Parse(match.Groups[c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1109)].Value, provider), float.Parse(match.Groups[c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1995)].Value, provider));
		}
		throw new FormatException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(11064));
	}
}
