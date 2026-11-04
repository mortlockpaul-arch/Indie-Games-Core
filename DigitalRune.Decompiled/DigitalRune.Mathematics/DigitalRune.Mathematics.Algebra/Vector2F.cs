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
///       Defines a 2-dimensional vector (single-precision).
///       </summary>
[Serializable]
[TypeConverter(typeof(Vector2FConverter))]
public struct Vector2F : IEquatable<Vector2F>
{
	/// <summary>
	///       Returns a <see cref="T:DigitalRune.Mathematics.Algebra.Vector2F" /> with all of its components set to zero.
	///       </summary>
	public static readonly Vector2F Zero;

	/// <summary>
	///       Returns a <see cref="T:DigitalRune.Mathematics.Algebra.Vector2F" /> with all of its components set to one.
	///       </summary>
	public static readonly Vector2F One;

	/// <summary>
	///       Returns the x unit <see cref="T:DigitalRune.Mathematics.Algebra.Vector2F" /> (1, 0).
	///       </summary>
	public static readonly Vector2F UnitX;

	/// <summary>
	///       Returns the value2 unit <see cref="T:DigitalRune.Mathematics.Algebra.Vector2F" /> (0, 1).
	///       </summary>
	public static readonly Vector2F UnitY;

	/// <summary>
	///       The x component.
	///       </summary>
	public float X;

	/// <summary>
	///       The y component.
	///       </summary>
	public float Y;

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
				_ => throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(622), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(10154)), 
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
			default:
				throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(622), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(10154));
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
			if (!Numeric.IsNaN(X))
			{
				return Numeric.IsNaN(Y);
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
			return (float)Math.Sqrt(X * X + Y * Y);
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
		}
	}

	/// <summary>
	///       Returns the squared length of this vector.
	///       </summary>
	/// <returns>The squared length of this vector.</returns>
	public float LengthSquared => X * X + Y * Y;

	/// <summary>
	///       Returns the normalized vector.
	///       </summary>
	/// <exception cref="T:System.DivideByZeroException">
	///       The length of the vector is zero. The quaternion cannot be normalized.
	///       </exception>
	public Vector2F Normalized
	{
		get
		{
			Vector2F result = this;
			result.Normalize();
			return result;
		}
	}

	/// <summary>
	///       Returns an arbitrary normalized <see cref="T:DigitalRune.Mathematics.Algebra.Vector2F" /> that is orthogonal
	///       to this vector.
	///       </summary>
	public Vector2F Orthonormal
	{
		get
		{
			Vector2F result = default;
			result.X = 0f - Y;
			result.Y = X;
			result.Normalize();
			return result;
		}
	}

	/// <summary>
	///       Gets the value of the largest component.
	///       </summary>
	public float LargestComponent
	{
		get
		{
			if (X >= Y)
			{
				return X;
			}
			return Y;
		}
	}

	/// <summary>
	///       Gets the index (zero-based) of the largest component.
	///       </summary>
	public int IndexOfLargestComponent
	{
		get
		{
			if (X >= Y)
			{
				return 0;
			}
			return 1;
		}
	}

	/// <summary>
	///       Gets the value of the smallest component.
	///       </summary>
	public float SmallestComponent
	{
		get
		{
			if (X <= Y)
			{
				return X;
			}
			return Y;
		}
	}

	/// <summary>
	///       Gets the index (zero-based) of the smallest component.
	///       </summary>
	public int IndexOfSmallestComponent
	{
		get
		{
			if (X <= Y)
			{
				return 0;
			}
			return 1;
		}
	}

	static Vector2F()
	{
		Zero = new Vector2F(0f, 0f);
		One = new Vector2F(1f, 1f);
		UnitX = new Vector2F(1f, 0f);
		UnitY = new Vector2F(0f, 1f);
		TypeInitializer.Initialize(cb8acf96051824377b85f63be63e3c2a3.ce9f9bc9ecd5ed400a711abaa33452c25);
		TypeInitializer.CreateInstance(11, cb8acf96051824377b85f63be63e3c2a3.ccda5e1cdd8faa90b24f6bc28c9af6b28);
	}

	/// <summary>
	///       Initializes a new instance of <see cref="T:DigitalRune.Mathematics.Algebra.Vector2F" />.
	///       </summary>
	/// <param name="x">Initial value for the x component.</param>
	/// <param name="y">Initial value for the y component.</param>
	public Vector2F(float x, float y)
	{
		X = x;
		Y = y;
	}

	/// <summary>
	///       Initializes a new instance of <see cref="T:DigitalRune.Mathematics.Algebra.Vector2F" />.
	///       </summary>
	/// <param name="componentValue">The initial value for 2 the vector components.</param>
	public Vector2F(float componentValue)
	{
		X = componentValue;
		Y = componentValue;
	}

	/// <summary>
	///       Initializes a new instance of <see cref="T:DigitalRune.Mathematics.Algebra.Vector2F" />.
	///       </summary>
	/// <param name="components">
	///       Array with the initial values for the components x, and y.
	///       </param>
	/// <exception cref="T:System.IndexOutOfRangeException">
	///   <paramref name="components" /> has less than 2 elements.
	///       </exception>
	/// <exception cref="T:System.NullReferenceException">
	///   <paramref name="components" /> must not be <see langword="null" />.
	///       </exception>
	public Vector2F(float[] components)
	{
		X = components[0];
		Y = components[1];
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Algebra.Vector2F" /> class.
	///       </summary>
	/// <param name="components">
	///       List with the initial values for the components x, and y.
	///       </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="components" /> has less than 2 elements.
	///       </exception>
	/// <exception cref="T:System.NullReferenceException">
	///   <paramref name="components" /> must not be <see langword="null" />.
	///       </exception>
	public Vector2F(IList<float> components)
	{
		X = components[0];
		Y = components[1];
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
		return (hashCode * 397) ^ Y.GetHashCode();
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
		if (obj is Vector2F)
		{
			return this == (Vector2F)obj;
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
	public bool Equals(Vector2F other)
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
		return string.Format(provider, c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(10263), new object[2] { X, Y });
	}

	/// <summary>
	///       Negates a vector.
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <returns>The negated vector.</returns>
	public static Vector2F operator -(Vector2F vector)
	{
		vector.X = 0f - vector.X;
		vector.Y = 0f - vector.Y;
		return vector;
	}

	/// <summary>
	///       Negates a vector.
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <returns>The negated vector.</returns>
	public static Vector2F Negate(Vector2F vector)
	{
		vector.X = 0f - vector.X;
		vector.Y = 0f - vector.Y;
		return vector;
	}

	/// <summary>
	///       Adds two vectors.
	///       </summary>
	/// <param name="vector1">The first vector.</param>
	/// <param name="vector2">The second vector.</param>
	/// <returns>The sum of the two vectors.</returns>
	public static Vector2F operator +(Vector2F vector1, Vector2F vector2)
	{
		vector1.X += vector2.X;
		vector1.Y += vector2.Y;
		return vector1;
	}

	/// <summary>
	///       Adds two vectors.
	///       </summary>
	/// <param name="vector1">The first vector.</param>
	/// <param name="vector2">The second vector.</param>
	/// <returns>The sum of the two vectors.</returns>
	public static Vector2F Add(Vector2F vector1, Vector2F vector2)
	{
		vector1.X += vector2.X;
		vector1.Y += vector2.Y;
		return vector1;
	}

	/// <summary>
	///       Subtracts a vector from a vector.
	///       </summary>
	/// <param name="minuend">The first vector (minuend).</param>
	/// <param name="subtrahend">The second vector (subtrahend).</param>
	/// <returns>The difference of the two vectors.</returns>
	public static Vector2F operator -(Vector2F minuend, Vector2F subtrahend)
	{
		minuend.X -= subtrahend.X;
		minuend.Y -= subtrahend.Y;
		return minuend;
	}

	/// <summary>
	///       Subtracts a vector from a vector.
	///       </summary>
	/// <param name="minuend">The first vector (minuend).</param>
	/// <param name="subtrahend">The second vector (subtrahend).</param>
	/// <returns>The difference of the two vectors.</returns>
	public static Vector2F Subtract(Vector2F minuend, Vector2F subtrahend)
	{
		minuend.X -= subtrahend.X;
		minuend.Y -= subtrahend.Y;
		return minuend;
	}

	/// <summary>
	///       Multiplies a vector by a scalar.
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <param name="scalar">The scalar.</param>
	/// <returns>
	///       The vector with each component multiplied by <paramref name="scalar" />.
	///       </returns>
	public static Vector2F operator *(Vector2F vector, float scalar)
	{
		vector.X *= scalar;
		vector.Y *= scalar;
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
	public static Vector2F operator *(float scalar, Vector2F vector)
	{
		vector.X *= scalar;
		vector.Y *= scalar;
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
	public static Vector2F Multiply(float scalar, Vector2F vector)
	{
		vector.X *= scalar;
		vector.Y *= scalar;
		return vector;
	}

	/// <summary>
	///       Multiplies the components of two vectors by each other.
	///       </summary>
	/// <param name="vector1">The first vector.</param>
	/// <param name="vector2">The second vector.</param>
	/// <returns>The component-wise product of the two vectors.</returns>
	public static Vector2F operator *(Vector2F vector1, Vector2F vector2)
	{
		vector1.X *= vector2.X;
		vector1.Y *= vector2.Y;
		return vector1;
	}

	/// <summary>
	///       Multiplies the components of two vectors by each other.
	///       </summary>
	/// <param name="vector1">The first vector.</param>
	/// <param name="vector2">The second vector.</param>
	/// <returns>The component-wise product of the two vectors.</returns>
	public static Vector2F Multiply(Vector2F vector1, Vector2F vector2)
	{
		vector1.X *= vector2.X;
		vector1.Y *= vector2.Y;
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
	public static Vector2F operator /(Vector2F vector, float scalar)
	{
		float num = 1f / scalar;
		vector.X *= num;
		vector.Y *= num;
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
	public static Vector2F Divide(Vector2F vector, float scalar)
	{
		float num = 1f / scalar;
		vector.X *= num;
		vector.Y *= num;
		return vector;
	}

	/// <summary>
	///       Divides the components of a vector by the components of another 
	///       vector.
	///       </summary>
	/// <param name="dividend">The first vector (dividend).</param>
	/// <param name="divisor">The second vector (divisor).</param>
	/// <returns>The component-wise product of the two vectors.</returns>
	public static Vector2F operator /(Vector2F dividend, Vector2F divisor)
	{
		dividend.X /= divisor.X;
		dividend.Y /= divisor.Y;
		return dividend;
	}

	/// <summary>
	///       Divides the components of a vector by the components of another 
	///       vector.
	///       </summary>
	/// <param name="dividend">The first vector (dividend).</param>
	/// <param name="divisor">The second vector (divisor).</param>
	/// <returns>The component-wise division of the two vectors.</returns>
	public static Vector2F Divide(Vector2F dividend, Vector2F divisor)
	{
		dividend.X /= divisor.X;
		dividend.Y /= divisor.Y;
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
	public static bool operator ==(Vector2F vector1, Vector2F vector2)
	{
		if (vector1.X == vector2.X)
		{
			return vector1.Y == vector2.Y;
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
	public static bool operator !=(Vector2F vector1, Vector2F vector2)
	{
		if (vector1.X == vector2.X)
		{
			return vector1.Y != vector2.Y;
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
	public static bool operator >(Vector2F vector1, Vector2F vector2)
	{
		if (vector1.X > vector2.X)
		{
			return vector1.Y > vector2.Y;
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
	public static bool operator >=(Vector2F vector1, Vector2F vector2)
	{
		if (vector1.X >= vector2.X)
		{
			return vector1.Y >= vector2.Y;
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
	public static bool operator <(Vector2F vector1, Vector2F vector2)
	{
		if (vector1.X < vector2.X)
		{
			return vector1.Y < vector2.Y;
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
	public static bool operator <=(Vector2F vector1, Vector2F vector2)
	{
		if (vector1.X <= vector2.X)
		{
			return vector1.Y <= vector2.Y;
		}
		return false;
	}

	/// <summary>
	///       Converts a vector to an array of 2 float values.
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <returns>The array with 2 values.</returns>
	public static explicit operator float[](Vector2F vector)
	{
		return new float[2] { vector.X, vector.Y };
	}

	/// <summary>
	///       Converts this vector to an array of 2 float values.
	///       </summary>
	/// <returns>The array with 2 values.</returns>
	public float[] ToArray()
	{
		return (float[])this;
	}

	/// <summary>
	///       Converts a vector to a list of 2 float values.
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <returns>The result of the conversion.</returns>
	public static explicit operator List<float>(Vector2F vector)
	{
		List<float> list = new List<float>(2);
		list.Add(vector.X);
		list.Add(vector.Y);
		return list;
	}

	/// <summary>
	///       Converts this vector to a list of 2 float values.
	///       </summary>
	/// <returns>The list with 2 <see cref="T:System.Single" /> values.</returns>
	public List<float> ToList()
	{
		return (List<float>)this;
	}

	/// <summary>
	///       Performs an implicit conversion from <see cref="T:DigitalRune.Mathematics.Algebra.Vector2F" /> to <see cref="T:DigitalRune.Mathematics.Algebra.Vector2D" />.
	///       </summary>
	/// <param name="vector">The DigitalRune <see cref="T:DigitalRune.Mathematics.Algebra.Vector2F" />.</param>
	/// <returns>The result of the conversion.</returns>
	public static implicit operator Vector2D(Vector2F vector)
	{
		return new Vector2D(vector.X, vector.Y);
	}

	/// <summary>
	///       Converts this <see cref="T:DigitalRune.Mathematics.Algebra.Vector2F" /> to <see cref="T:DigitalRune.Mathematics.Algebra.Vector2D" />.
	///       </summary>
	/// <returns>The result of the conversion.</returns>
	public Vector2D ToVector2D()
	{
		return new Vector2D(X, Y);
	}

	/// <summary>
	///       Performs an implicit conversion from <see cref="T:DigitalRune.Mathematics.Algebra.Vector2F" /> 
	///       to <see cref="T:DigitalRune.Mathematics.Algebra.VectorF" />.
	///       </summary>
	/// <param name="vector">The DigitalRune <see cref="T:DigitalRune.Mathematics.Algebra.Vector2F" />.</param>
	/// <returns>The result of the conversion.</returns>
	public static implicit operator VectorF(Vector2F vector)
	{
		VectorF vectorF = new VectorF(2);
		vectorF[0] = vector.X;
		vectorF[1] = vector.Y;
		return vectorF;
	}

	/// <summary>
	///       Converts this <see cref="T:DigitalRune.Mathematics.Algebra.Vector2F" /> to <see cref="T:DigitalRune.Mathematics.Algebra.VectorF" />.
	///       </summary>
	/// <returns>The result of the conversion.</returns>
	public VectorF ToVectorF()
	{
		return this;
	}

	/// <summary>
	///       Performs an conversion from <see cref="T:Microsoft.Xna.Framework.Vector2" /> (XNA Framework) to 
	///       <see cref="T:DigitalRune.Mathematics.Algebra.Vector2F" /> (DigitalRune Mathematics).
	///       </summary>
	/// <param name="vector">The <see cref="T:Microsoft.Xna.Framework.Vector2" /> (XNA Framework).</param>
	/// <returns>The <see cref="T:DigitalRune.Mathematics.Algebra.Vector2F" /> (DigitalRune Mathematics).</returns>
	public static explicit operator Vector2F(Vector2 vector)
	{
		return new Vector2F(vector.X, vector.Y);
	}

	/// <summary>
	///       Converts this <see cref="T:DigitalRune.Mathematics.Algebra.Vector2F" /> (DigitalRune Mathematics) to <see cref="T:Microsoft.Xna.Framework.Vector2" /> 
	///       (XNA Framework).
	///       </summary>
	/// <param name="vector">The <see cref="T:Microsoft.Xna.Framework.Vector2" /> (XNA Framework).</param>
	/// <returns>The <see cref="T:DigitalRune.Mathematics.Algebra.Vector2F" /> (DigitalRune Mathematics).</returns>
	public static Vector2F FromXna(Vector2 vector)
	{
		return new Vector2F(vector.X, vector.Y);
	}

	/// <summary>
	///       Performs an conversion from <see cref="T:DigitalRune.Mathematics.Algebra.Vector2F" /> 
	///       (DigitalRune Mathematics) to <see cref="T:Microsoft.Xna.Framework.Vector2" /> (XNA Framework).
	///       </summary>
	/// <param name="vector">The <see cref="T:DigitalRune.Mathematics.Algebra.Vector2F" /> (DigitalRune Mathematics).</param>
	/// <returns>The <see cref="T:Microsoft.Xna.Framework.Vector2" /> (XNA Framework).</returns>
	public static explicit operator Vector2(Vector2F vector)
	{
		return new Vector2(vector.X, vector.Y);
	}

	/// <summary>
	///       Converts this <see cref="T:DigitalRune.Mathematics.Algebra.Vector2F" /> (DigitalRune Mathematics) to <see cref="T:Microsoft.Xna.Framework.Vector2" /> 
	///       (XNA Framework).
	///       </summary>
	/// <returns>The <see cref="T:Microsoft.Xna.Framework.Vector2" /> (XNA Framework).</returns>
	public Vector2 ToXna()
	{
		return new Vector2(X, Y);
	}

	/// <summary>
	///       Sets each vector component to its absolute value.
	///       </summary>
	public void Absolute()
	{
		X = Math.Abs(X);
		Y = Math.Abs(Y);
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
	}

	/// <summary>
	///       Clamps near-zero vector components to zero.
	///       </summary>
	public void ClampToZero()
	{
		X = Numeric.ClampToZero(X);
		Y = Numeric.ClampToZero(Y);
	}

	/// <summary>
	///       Clamps near-zero vector components to zero.
	///       </summary>
	/// <param name="epsilon">The tolerance value.</param>
	public void ClampToZero(float epsilon)
	{
		X = Numeric.ClampToZero(X, epsilon);
		Y = Numeric.ClampToZero(Y, epsilon);
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
		return true;
	}

	/// <summary>
	///       Sets this vector to its projection onto the axis given by the target vector.
	///       </summary>
	/// <param name="target">The target vector.</param>
	public void ProjectTo(Vector2F target)
	{
		this = Dot(this, target) / target.LengthSquared * target;
	}

	/// <summary>
	///       Returns a vector with the absolute values of the elements of the given vector.
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <returns>A vector with the absolute values of the elements of the given vector.</returns>
	public static Vector2F Absolute(Vector2F vector)
	{
		return new Vector2F(Math.Abs(vector.X), Math.Abs(vector.Y));
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
	public static bool AreNumericallyEqual(Vector2F vector1, Vector2F vector2)
	{
		if (Numeric.AreEqual(vector1.X, vector2.X))
		{
			return Numeric.AreEqual(vector1.Y, vector2.Y);
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
	public static bool AreNumericallyEqual(Vector2F vector1, Vector2F vector2, float epsilon)
	{
		if (Numeric.AreEqual(vector1.X, vector2.X, epsilon))
		{
			return Numeric.AreEqual(vector1.Y, vector2.Y, epsilon);
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
	public static Vector2F Clamp(Vector2F vector, float min, float max)
	{
		return new Vector2F(MathHelper.Clamp(vector.X, min, max), MathHelper.Clamp(vector.Y, min, max));
	}

	/// <summary>
	///       Returns a vector with near-zero vector components clamped to 0.
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <returns>
	///       The vector with small components clamped to zero.
	///       </returns>
	public static Vector2F ClampToZero(Vector2F vector)
	{
		vector.X = Numeric.ClampToZero(vector.X);
		vector.Y = Numeric.ClampToZero(vector.Y);
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
	public static Vector2F ClampToZero(Vector2F vector, float epsilon)
	{
		vector.X = Numeric.ClampToZero(vector.X, epsilon);
		vector.Y = Numeric.ClampToZero(vector.Y, epsilon);
		return vector;
	}

	/// <summary>
	///       Calculates the dot product of two vectors.
	///       </summary>
	/// <param name="vector1">The first vector.</param>
	/// <param name="vector2">The second vector.</param>
	/// <returns>The dot product.</returns>
	public static float Dot(Vector2F vector1, Vector2F vector2)
	{
		return vector1.X * vector2.X + vector1.Y * vector2.Y;
	}

	/// <summary>
	///       Calculates the angle between two vectors.
	///       </summary>
	/// <param name="vector1">The first vector.</param>
	/// <param name="vector2">The second vector.</param>
	/// <returns>The angle between the given vectors, such that 0 ≤ angle ≤ <c>π</c>.</returns>
	/// <exception cref="T:DigitalRune.Mathematics.MathematicsException">
	///   <paramref name="vector1" /> or <paramref name="vector2" /> has a length of 0.
	///       </exception>
	public static float GetAngle(Vector2F vector1, Vector2F vector2)
	{
		try
		{
			vector1.Normalize();
			vector2.Normalize();
		}
		catch (DivideByZeroException)
		{
			throw new MathematicsException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(955));
		}
		float num = Dot(vector1, vector2);
		MathHelper.Clamp(num, -1f, 1f);
		return (float)Math.Acos(num);
	}

	/// <summary>
	///       Returns a vector that contains the lowest value from each matching pair of components.
	///       </summary>
	/// <param name="vector1">The first vector.</param>
	/// <param name="vector2">The second vector.</param>
	/// <returns>The minimized vector.</returns>
	public static Vector2F Min(Vector2F vector1, Vector2F vector2)
	{
		vector1.X = Math.Min(vector1.X, vector2.X);
		vector1.Y = Math.Min(vector1.Y, vector2.Y);
		return vector1;
	}

	/// <summary>
	///       Returns a vector that contains the highest value from each matching pair of components.
	///       </summary>
	/// <param name="vector1">The first vector.</param>
	/// <param name="vector2">The second vector.</param>
	/// <returns>The maximized vector.</returns>
	public static Vector2F Max(Vector2F vector1, Vector2F vector2)
	{
		vector1.X = Math.Max(vector1.X, vector2.X);
		vector1.Y = Math.Max(vector1.Y, vector2.Y);
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
	public static Vector2F ProjectTo(Vector2F vector, Vector2F target)
	{
		return Dot(vector, target) / target.LengthSquared * target;
	}

	/// <summary>
	///       Converts the string representation of a 2-dimensional vector to its 
	///       <see cref="T:DigitalRune.Mathematics.Algebra.Vector2F" /> equivalent.
	///       </summary>
	/// <param name="s">A string representation of a 2-dimensional vector.</param>
	/// <returns>
	///       A <see cref="T:DigitalRune.Mathematics.Algebra.Vector2F" /> that represents the vector specified by the 
	///       <paramref name="s" /> parameter.
	///       </returns>
	public static Vector2F Parse(string s)
	{
		return Parse(s, CultureInfo.CurrentCulture);
	}

	/// <summary>
	///       Converts the string representation of a 2-dimensional vector in a specified 
	///       culture-specific format to its <see cref="T:DigitalRune.Mathematics.Algebra.Vector2F" /> equivalent.
	///       </summary>
	/// <param name="s">A string representation of a 2-dimensional vector.</param>
	/// <param name="provider">
	///       An <see cref="T:System.IFormatProvider" /> that supplies culture-specific formatting 
	///       information about <paramref name="s" />. 
	///       </param>
	/// <returns>
	///       A <see cref="T:DigitalRune.Mathematics.Algebra.Vector2F" /> that represents the vector specified by the 
	///       <paramref name="s" /> parameter.
	///       </returns>
	/// <exception cref="T:System.FormatException">
	///   <paramref name="s" /> is not a valid <see cref="T:DigitalRune.Mathematics.Algebra.Vector2F" />.
	///       </exception>
	public static Vector2F Parse(string s, IFormatProvider provider)
	{
		Regex regex = new Regex(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(10284), RegexOptions.None);
		Match match = regex.Match(s);
		if (match.Success)
		{
			return new Vector2F(float.Parse(match.Groups[c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1103)].Value, provider), float.Parse(match.Groups[c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1106)].Value, provider));
		}
		throw new FormatException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(10327));
	}
}
