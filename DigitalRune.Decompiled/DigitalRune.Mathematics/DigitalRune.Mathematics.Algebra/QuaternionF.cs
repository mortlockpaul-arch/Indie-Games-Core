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
///       Defines a quaternion (single-precision).
///       </summary>
[Serializable]
[TypeConverter(typeof(QuaternionFConverter))]
public struct QuaternionF : IEquatable<QuaternionF>
{
	/// <summary>
	///       Returns a <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionF" /> with all of its components set to zero.
	///       </summary>
	public static readonly QuaternionF Zero;

	/// <summary>
	///       Returns the identity <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionF" /> (1, 0, 0, 0).
	///       </summary>
	public static readonly QuaternionF Identity;

	/// <summary>
	///       The w component.
	///       </summary>
	public float W;

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
	///       Gets or sets the angle of the rotation around <see cref="P:DigitalRune.Mathematics.Algebra.QuaternionF.Axis" />.
	///       </summary>
	[XmlIgnore]
	public float Angle
	{
		get
		{
			float w = W;
			if (Numeric.IsGreater(Math.Abs(w), 1f))
			{
				return float.NaN;
			}
			w = MathHelper.Clamp(w, -1f, 1f);
			return (float)Math.Acos(w) * 2f;
		}
		set
		{
			this = CreateRotation(Axis, value);
		}
	}

	/// <summary>
	///       Gets or sets the vector part (x, y, z).
	///       </summary>
	[XmlIgnore]
	public Vector3F V
	{
		get
		{
			return new Vector3F(X, Y, Z);
		}
		set
		{
			X = value.X;
			Y = value.Y;
			Z = value.Z;
		}
	}

	/// <summary>
	///       Gets or sets the normalized unit vector with the direction of the rotation 
	///       axis.
	///       </summary>
	[XmlIgnore]
	public Vector3F Axis
	{
		get
		{
			Vector3F result = new Vector3F(X, Y, Z);
			if (result.TryNormalize())
			{
				return result;
			}
			return Vector3F.Zero;
		}
		set
		{
			if (!value.IsNumericallyZero)
			{
				this = CreateRotation(value, Angle);
			}
			else
			{
				this = Identity;
			}
		}
	}

	/// <summary>
	///       Returns the conjugate of the quaternion.
	///       </summary>
	public QuaternionF Conjugated
	{
		get
		{
			QuaternionF result = this;
			result.Conjugate();
			return result;
		}
	}

	/// <summary>
	///       Returns the inverse of this quaternion.
	///       </summary>
	/// <exception cref="T:DigitalRune.Mathematics.MathematicsException">
	///       The length of the quaternion is zero. The quaternion cannot be inverted.
	///       </exception>
	public QuaternionF Inverse
	{
		get
		{
			QuaternionF result = this;
			result.Invert();
			return result;
		}
	}

	/// <summary>
	///       Returns the normalized quaternion.
	///       </summary>
	/// <exception cref="T:System.DivideByZeroException">
	///       The length of the quaternion is zero. The quaternion cannot be normalized.
	///       </exception>
	public QuaternionF Normalized
	{
		get
		{
			QuaternionF result = this;
			result.Normalize();
			return result;
		}
	}

	/// <summary>
	///       Returns the modulus (length).
	///       </summary>
	public float Modulus => (float)Math.Sqrt(W * W + X * X + Y * Y + Z * Z);

	/// <summary>
	///       Returns the norm (<i>length<sup>2</sup></i>).
	///       </summary>
	public float Norm => W * W + X * X + Y * Y + Z * Z;

	/// <summary>
	///       Gets a value indicating whether a component of the quaternion is <see cref="F:System.Single.NaN" />.
	///       </summary>
	public bool IsNaN
	{
		get
		{
			if (!Numeric.IsNaN(W) && !Numeric.IsNaN(X) && !Numeric.IsNaN(Y))
			{
				return Numeric.IsNaN(Z);
			}
			return true;
		}
	}

	/// <summary>
	///       Returns a value indicating whether this quaternion is normalized (the <see cref="P:DigitalRune.Mathematics.Algebra.QuaternionF.Modulus" /> 
	///       is numerically equal to 1).
	///       </summary>
	public bool IsNumericallyNormalized => Numeric.AreEqual(Norm, 1f);

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
				0 => W, 
				1 => X, 
				2 => Y, 
				3 => Z, 
				_ => throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(622), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1175)), 
			};
		}
		set
		{
			switch (index)
			{
			case 0:
				W = value;
				break;
			case 1:
				X = value;
				break;
			case 2:
				Y = value;
				break;
			case 3:
				Z = value;
				break;
			default:
				throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(622), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1175));
			}
		}
	}

	static QuaternionF()
	{
		Zero = new QuaternionF(0f, 0f, 0f, 0f);
		Identity = new QuaternionF(1f, 0f, 0f, 0f);
		TypeInitializer.Initialize(cb8acf96051824377b85f63be63e3c2a3.ce9f9bc9ecd5ed400a711abaa33452c25);
		TypeInitializer.CreateInstance(11, cb8acf96051824377b85f63be63e3c2a3.ccda5e1cdd8faa90b24f6bc28c9af6b28);
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionF" /> class.
	///       </summary>
	/// <param name="w">The initial value for the w component.</param>
	/// <param name="x">The initial value for the x component.</param>
	/// <param name="y">The initial value for the y component.</param>
	/// <param name="z">The initial value for the z component.</param>
	public QuaternionF(float w, float x, float y, float z)
	{
		W = w;
		X = x;
		Y = y;
		Z = z;
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionF" /> class.
	///       </summary>
	/// <param name="components">
	///       Array with the initial values for the components w, x, y and z.</param>
	/// <exception cref="T:System.IndexOutOfRangeException">
	///   <paramref name="components" /> has less than 4 elements.
	///       </exception>
	/// <exception cref="T:System.NullReferenceException">
	///   <paramref name="components" /> must not be <see langword="null" />.
	///       </exception>
	public QuaternionF(float[] components)
	{
		W = components[0];
		X = components[1];
		Y = components[2];
		Z = components[3];
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionF" /> class.
	///       </summary>
	/// <param name="components">
	///       List with the initial values for the components w, x, y and z.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="components" /> has less than 4 elements.
	///       </exception>
	/// <exception cref="T:System.NullReferenceException">
	///   <paramref name="components" /> must not be <see langword="null" />.
	///       </exception>
	public QuaternionF(IList<float> components)
	{
		W = components[0];
		X = components[1];
		Y = components[2];
		Z = components[3];
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionF" /> class.
	///       </summary>
	/// <param name="w">The initial value for scalar component w.</param>
	/// <param name="v">The initial values for the vector component (x, y, z).</param>
	public QuaternionF(float w, Vector3F v)
	{
		W = w;
		X = v.X;
		Y = v.Y;
		Z = v.Z;
	}

	/// <summary>
	///       Returns the hash code for this instance.
	///       </summary>
	/// <returns>
	///       A 32-bit signed integer that is the hash code for this instance.
	///       </returns>
	public override int GetHashCode()
	{
		int hashCode = W.GetHashCode();
		hashCode = (hashCode * 397) ^ X.GetHashCode();
		hashCode = (hashCode * 397) ^ Y.GetHashCode();
		return (hashCode * 397) ^ Z.GetHashCode();
	}

	/// <summary>
	///       Indicates whether this instance and a specified object are equal.
	///       </summary>
	/// <param name="obj">Another object to compare to.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="obj" /> and this instance are the same type and 
	///       represent the same value; otherwise, <see langword="false" />.
	///       </returns>
	public override bool Equals(object obj)
	{
		if (obj is QuaternionF)
		{
			return this == (QuaternionF)obj;
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
	public bool Equals(QuaternionF other)
	{
		return this == other;
	}

	/// <summary>
	///       Returns the string representation of this quaternion.
	///       </summary>
	/// <returns>The string representation of this quaternion.</returns>
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
	/// <returns>The string representation of this quaternion.</returns>
	public string ToString(IFormatProvider provider)
	{
		return string.Format(provider, c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1282), W, X, Y, Z);
	}

	/// <summary>
	///       Negates a quaternion.
	///       </summary>
	/// <param name="quaternion">The quaternion.</param>
	/// <returns>The negated quaternion.</returns>
	public static QuaternionF operator -(QuaternionF quaternion)
	{
		quaternion.X = 0f - quaternion.X;
		quaternion.Y = 0f - quaternion.Y;
		quaternion.Z = 0f - quaternion.Z;
		quaternion.W = 0f - quaternion.W;
		return quaternion;
	}

	/// <summary>
	///       Negates a quaternion.
	///       </summary>
	/// <param name="quaternion">The quaternion.</param>
	/// <returns>The negated quaternion.</returns>
	public static QuaternionF Negate(QuaternionF quaternion)
	{
		quaternion.X = 0f - quaternion.X;
		quaternion.Y = 0f - quaternion.Y;
		quaternion.Z = 0f - quaternion.Z;
		quaternion.W = 0f - quaternion.W;
		return quaternion;
	}

	/// <summary>
	///       Adds two quaternions.
	///       </summary>
	/// <param name="quaternion1">The first quaternion.</param>
	/// <param name="quaternion2">The second quaternion.</param>
	/// <returns>The sum of the two quaternions.</returns>
	public static QuaternionF operator +(QuaternionF quaternion1, QuaternionF quaternion2)
	{
		quaternion1.X += quaternion2.X;
		quaternion1.Y += quaternion2.Y;
		quaternion1.Z += quaternion2.Z;
		quaternion1.W += quaternion2.W;
		return quaternion1;
	}

	/// <summary>
	///       Adds two quaternions.
	///       </summary>
	/// <param name="quaternion1">The first quaternion.</param>
	/// <param name="quaternion2">The second quaternion.</param>
	/// <returns>The sum of the two quaternions.</returns>
	public static QuaternionF Add(QuaternionF quaternion1, QuaternionF quaternion2)
	{
		quaternion1.X += quaternion2.X;
		quaternion1.Y += quaternion2.Y;
		quaternion1.Z += quaternion2.Z;
		quaternion1.W += quaternion2.W;
		return quaternion1;
	}

	/// <summary>
	///       Subtracts a quaternion from a quaternion.
	///       </summary>
	/// <param name="minuend">The first quaternion (minuend).</param>
	/// <param name="subtrahend">The second quaternion (subtrahend).</param>
	/// <returns>The difference of the two quaternions.</returns>
	public static QuaternionF operator -(QuaternionF minuend, QuaternionF subtrahend)
	{
		minuend.X -= subtrahend.X;
		minuend.Y -= subtrahend.Y;
		minuend.Z -= subtrahend.Z;
		minuend.W -= subtrahend.W;
		return minuend;
	}

	/// <summary>
	///       Subtracts a quaternion from a quaternion.
	///       </summary>
	/// <param name="minuend">The first quaternion (minuend).</param>
	/// <param name="subtrahend">The second quaternion (subtrahend).</param>
	/// <returns>The difference of the two quaternions.</returns>
	public static QuaternionF Subtract(QuaternionF minuend, QuaternionF subtrahend)
	{
		minuend.X -= subtrahend.X;
		minuend.Y -= subtrahend.Y;
		minuend.Z -= subtrahend.Z;
		minuend.W -= subtrahend.W;
		return minuend;
	}

	/// <summary>
	///       Multiplies a quaternion by a scalar.
	///       </summary>
	/// <param name="quaternion">The quaternion.</param>
	/// <param name="scalar">The scalar.</param>
	/// <returns>
	///       The quaternion with each component multiplied by <paramref name="scalar" />.
	///       </returns>
	public static QuaternionF operator *(QuaternionF quaternion, float scalar)
	{
		quaternion.W *= scalar;
		quaternion.X *= scalar;
		quaternion.Y *= scalar;
		quaternion.Z *= scalar;
		return quaternion;
	}

	/// <summary>
	///       Multiplies a quaternion by a scalar.
	///       </summary>
	/// <param name="quaternion">The quaternion.</param>
	/// <param name="scalar">The scalar.</param>
	/// <returns>
	///       The quaternion with each component multiplied by <paramref name="scalar" />.
	///       </returns>
	public static QuaternionF operator *(float scalar, QuaternionF quaternion)
	{
		quaternion.W *= scalar;
		quaternion.X *= scalar;
		quaternion.Y *= scalar;
		quaternion.Z *= scalar;
		return quaternion;
	}

	/// <summary>
	///       Multiplies a quaternion by a scalar.
	///       </summary>
	/// <param name="quaternion">The quaternion.</param>
	/// <param name="scalar">The scalar.</param>
	/// <returns>
	///       The quaternion with each component multiplied by <paramref name="scalar" />.
	///       </returns>
	public static QuaternionF Multiply(float scalar, QuaternionF quaternion)
	{
		quaternion.W *= scalar;
		quaternion.X *= scalar;
		quaternion.Y *= scalar;
		quaternion.Z *= scalar;
		return quaternion;
	}

	/// <summary>
	///       Multiplies two quaternions.
	///       </summary>
	/// <param name="q1">The first quaternion.</param>
	/// <param name="q2">The second quaternion.</param>
	/// <returns>The product of the two quaternions.</returns>
	public static QuaternionF operator *(QuaternionF q1, QuaternionF q2)
	{
		QuaternionF result = default;
		result.W = q1.W * q2.W - q1.X * q2.X - q1.Y * q2.Y - q1.Z * q2.Z;
		result.X = q1.W * q2.X + q1.X * q2.W + q1.Y * q2.Z - q1.Z * q2.Y;
		result.Y = q1.W * q2.Y - q1.X * q2.Z + q1.Y * q2.W + q1.Z * q2.X;
		result.Z = q1.W * q2.Z + q1.X * q2.Y - q1.Y * q2.X + q1.Z * q2.W;
		return result;
	}

	/// <summary>
	///       Multiplies two quaternions.
	///       </summary>
	/// <param name="q1">The first quaternion.</param>
	/// <param name="q2">The second quaternion.</param>
	/// <returns>The product of the two quaternions.</returns>
	public static QuaternionF Multiply(QuaternionF q1, QuaternionF q2)
	{
		QuaternionF result = default;
		result.W = q1.W * q2.W - q1.X * q2.X - q1.Y * q2.Y - q1.Z * q2.Z;
		result.X = q1.W * q2.X + q1.X * q2.W + q1.Y * q2.Z - q1.Z * q2.Y;
		result.Y = q1.W * q2.Y - q1.X * q2.Z + q1.Y * q2.W + q1.Z * q2.X;
		result.Z = q1.W * q2.Z + q1.X * q2.Y - q1.Y * q2.X + q1.Z * q2.W;
		return result;
	}

	/// <summary>
	///       Divides a quaternion by a scalar.
	///       </summary>
	/// <param name="quaternion">The quaternion.</param>
	/// <param name="scalar">The scalar.</param>
	/// <returns>The quaternion with each component divided by scalar.</returns>
	public static QuaternionF operator /(QuaternionF quaternion, float scalar)
	{
		float num = 1f / scalar;
		quaternion.W *= num;
		quaternion.X *= num;
		quaternion.Y *= num;
		quaternion.Z *= num;
		return quaternion;
	}

	/// <summary>
	///       Divides a quaternion by a scalar.
	///       </summary>
	/// <param name="quaternion">The quaternion.</param>
	/// <param name="scalar">The scalar.</param>
	/// <returns>The quaternion with each component divided by scalar.</returns>
	public static QuaternionF Divide(QuaternionF quaternion, float scalar)
	{
		float num = 1f / scalar;
		quaternion.W *= num;
		quaternion.X *= num;
		quaternion.Y *= num;
		quaternion.Z *= num;
		return quaternion;
	}

	/// <summary>
	///       Divides a quaternions by another quaternion.
	///       </summary>
	/// <param name="dividend">The first quaternion (dividend).</param>
	/// <param name="divisor">The second quaternion (divisor).</param>
	/// <returns>The result of the division.</returns>
	/// <exception cref="T:DigitalRune.Mathematics.MathematicsException">
	///       The quaternion <paramref name="divisor" /> cannot be inverted.
	///       </exception>
	public static QuaternionF operator /(QuaternionF dividend, QuaternionF divisor)
	{
		divisor.Invert();
		return dividend * divisor;
	}

	/// <summary>
	///       Divides a quaternions by another quaternion.
	///       </summary>
	/// <param name="dividend">The first quaternion (dividend).</param>
	/// <param name="divisor">The second quaternion (divisor).</param>
	/// <returns>The result of the division.</returns>
	/// <exception cref="T:DigitalRune.Mathematics.MathematicsException">
	///       The quaternion <paramref name="divisor" /> cannot be inverted.
	///       </exception>
	public static QuaternionF Divide(QuaternionF dividend, QuaternionF divisor)
	{
		divisor.Invert();
		return dividend * divisor;
	}

	/// <summary>
	///       Tests if two quaternions are equal.
	///       </summary>
	/// <param name="q1">The first quaternion.</param>
	/// <param name="q2">The second quaternion.</param>
	/// <returns>
	///   <see langword="true" /> if the quaternions are equal; otherwise <see langword="false" />.
	///       </returns>
	public static bool operator ==(QuaternionF q1, QuaternionF q2)
	{
		if (q1.W == q2.W && q1.X == q2.X && q1.Y == q2.Y)
		{
			return q1.Z == q2.Z;
		}
		return false;
	}

	/// <summary>
	///       Tests if two quaternions are not equal.
	///       </summary>
	/// <param name="q1">The first quaternion.</param>
	/// <param name="q2">The second quaternion.</param>
	/// <returns>
	///   <see langword="true" /> if the quaternions are different; otherwise <see langword="false" />.
	///       </returns>
	public static bool operator !=(QuaternionF q1, QuaternionF q2)
	{
		if (q1.W == q2.W && q1.X == q2.X && q1.Y == q2.Y)
		{
			return q1.Z != q2.Z;
		}
		return true;
	}

	/// <summary>
	///       Converts the quaternion to an array of 4 float values: (w, x, y, z).
	///       </summary>
	/// <param name="quaternion">The quaternion.</param>
	/// <returns>The array with 4 values (w, x, y, z).</returns>
	public static explicit operator float[](QuaternionF quaternion)
	{
		return new float[4] { quaternion.W, quaternion.X, quaternion.Y, quaternion.Z };
	}

	/// <summary>
	///       Converts the quaternion to an array of 4 float values: (w, x, y, z).
	///       </summary>
	/// <returns>The array with 4 values (w, x, y, z).</returns>
	public float[] ToArray()
	{
		return (float[])this;
	}

	/// <summary>
	///       Converts the vector to a list of 4 float values: (w, x, y, z).
	///       </summary>
	/// <param name="quaternion">The quaternion.</param>
	/// <returns>The list with 4 values (w, x, y, z).</returns>
	public static explicit operator List<float>(QuaternionF quaternion)
	{
		List<float> list = new List<float>(4);
		list.Add(quaternion.W);
		list.Add(quaternion.X);
		list.Add(quaternion.Y);
		list.Add(quaternion.Z);
		return list;
	}

	/// <summary>
	///       Converts the vector to a list of 4 float values: (w, x, y, z).
	///       </summary>
	/// <returns>The list with 4 values (w, x, y, z).</returns>
	public List<float> ToList()
	{
		return (List<float>)this;
	}

	/// <summary>
	///       Performs an implicit conversion from <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionF" /> to <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionD" />.
	///       </summary>
	/// <param name="quaternion">The DigitalRune <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionF" />.</param>
	/// <returns>The result of the conversion.</returns>
	public static implicit operator QuaternionD(QuaternionF quaternion)
	{
		return new QuaternionD(quaternion.W, quaternion.X, quaternion.Y, quaternion.Z);
	}

	/// <summary>
	///       Converts this <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionF" /> to <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionD" />.
	///       </summary>
	/// <returns>The result of the conversion.</returns>
	public QuaternionD ToQuaternionD()
	{
		return new QuaternionD(W, X, Y, Z);
	}

	/// <summary>
	///       Performs an conversion from <see cref="T:Microsoft.Xna.Framework.Quaternion" /> (XNA Framework) to 
	///       <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionF" /> (DigitalRune Mathematics).
	///       </summary>
	/// <param name="quaternion">The <see cref="T:Microsoft.Xna.Framework.Quaternion" /> (XNA Framework).</param>
	/// <returns>The <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionF" /> (DigitalRune Mathematics).</returns>
	public static explicit operator QuaternionF(Quaternion quaternion)
	{
		return new QuaternionF(quaternion.W, quaternion.X, quaternion.Y, quaternion.Z);
	}

	/// <summary>
	///       Converts this <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionF" /> (DigitalRune Mathematics) to 
	///       <see cref="T:Microsoft.Xna.Framework.Quaternion" /> (XNA Framework).
	///       </summary>
	/// <param name="quaternion">The <see cref="T:Microsoft.Xna.Framework.Quaternion" /> (XNA Framework).</param>
	/// <returns>The <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionF" /> (DigitalRune Mathematics).</returns>
	public static QuaternionF FromXna(Quaternion quaternion)
	{
		return new QuaternionF(quaternion.W, quaternion.X, quaternion.Y, quaternion.Z);
	}

	/// <summary>
	///       Performs an conversion from <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionF" /> 
	///       (DigitalRune Mathematics) to <see cref="T:Microsoft.Xna.Framework.Quaternion" /> (XNA Framework).
	///       </summary>
	/// <param name="quaternion">The <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionF" /> (DigitalRune Mathematics).</param>
	/// <returns>The <see cref="T:Microsoft.Xna.Framework.Quaternion" /> (XNA Framework).</returns>
	public static explicit operator Quaternion(QuaternionF quaternion)
	{
		return new Quaternion(quaternion.X, quaternion.Y, quaternion.Z, quaternion.W);
	}

	/// <summary>
	///       Converts this <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionF" /> (DigitalRune Mathematics) to 
	///       <see cref="T:Microsoft.Xna.Framework.Quaternion" /> (XNA Framework).
	///       </summary>
	/// <returns>The <see cref="T:Microsoft.Xna.Framework.Quaternion" /> (XNA Framework).</returns>
	public Quaternion ToXna()
	{
		return new Quaternion(X, Y, Z, W);
	}

	/// <summary>
	///       Sets this quaternion to its conjugate.
	///       </summary>
	public void Conjugate()
	{
		X = 0f - X;
		Y = 0f - Y;
		Z = 0f - Z;
	}

	/// <summary>
	///       Inverts the quaternion.
	///       </summary>
	/// <exception cref="T:DigitalRune.Mathematics.MathematicsException">
	///       The length of the quaternion is zero. The quaternion cannot be inverted.
	///       </exception>
	public void Invert()
	{
		float norm = Norm;
		if (Numeric.IsZero(norm, Numeric.EpsilonFSquared))
		{
			throw new MathematicsException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1327));
		}
		float num = 1f / norm;
		W *= num;
		X = (0f - X) * num;
		Y = (0f - Y) * num;
		Z = (0f - Z) * num;
	}

	/// <summary>
	///       Normalizes the quaternion.
	///       </summary>
	/// <exception cref="T:System.DivideByZeroException">
	///       The length of the quaternion is zero. The quaternion cannot be normalized.
	///       </exception>
	public void Normalize()
	{
		float norm = Norm;
		if (Numeric.IsZero(norm))
		{
			throw new DivideByZeroException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1473));
		}
		float num = (float)(1.0 / Math.Sqrt(norm));
		W *= num;
		X *= num;
		Y *= num;
		Z *= num;
	}

	/// <summary>
	///       Tries to normalize the quaternion.
	///       </summary>
	/// <returns>
	///   <see langword="true" /> if the quaternion was normalized; otherwise, <see langword="false" /> 
	///       if the quaternion could not be normalized. (The norm is numerically zero.)
	///       </returns>
	public bool TryNormalize()
	{
		float norm = Norm;
		if (Numeric.IsZero(norm, Numeric.EpsilonFSquared))
		{
			return false;
		}
		float num = (float)(1.0 / Math.Sqrt(norm));
		W *= num;
		X *= num;
		Y *= num;
		Z *= num;
		return true;
	}

	/// <summary>
	///       Sets this quaternion to its exponential.
	///       </summary>
	public void Exp()
	{
		this = Exp(this);
	}

	/// <summary>
	///       Sets this quaternion to its natural logarithm.
	///       </summary>
	/// <exception cref="T:DigitalRune.Mathematics.MathematicsException">
	///       The quaternion is not a unit quaternion.
	///       </exception>
	public void Ln()
	{
		this = Ln(this);
	}

	/// <summary>
	///       Sets this unit quaternion to a power of itself.
	///       </summary>
	/// <param name="t">The exponent.</param>
	/// <returns>The power of the unit quaternion.</returns>
	public void Power(float t)
	{
		this = Power(this, t);
	}

	/// <summary>
	///       Rotates a vector.
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <returns>The rotated vector.</returns>
	public Vector3F Rotate(Vector3F vector)
	{
		Vector3F v = V;
		float w = W;
		float num = 0f - (v.X * vector.X + v.Y * vector.Y + v.Z * vector.Z);
		float num2 = v.Y * vector.Z - v.Z * vector.Y + w * vector.X;
		float num3 = v.Z * vector.X - v.X * vector.Z + w * vector.Y;
		float num4 = v.X * vector.Y - v.Y * vector.X + w * vector.Z;
		float num5 = 0f - X;
		float num6 = 0f - Y;
		float num7 = 0f - Z;
		return new Vector3F(num3 * num7 - num4 * num6 + num * num5 + w * num2, num4 * num5 - num2 * num7 + num * num6 + w * num3, num2 * num6 - num3 * num5 + num * num7 + w * num4);
	}

	/// <summary>
	///       Returns the 3 x 3 rotation matrix of this quaternion.
	///       </summary>
	/// <returns>The rotation matrix.</returns>
	public Matrix33F ToRotationMatrix33()
	{
		float num = 2f * X;
		float num2 = 2f * Y;
		float num3 = 2f * Z;
		float num4 = num * X;
		float num5 = num2 * Y;
		float num6 = num3 * Z;
		float num7 = num * Y;
		float num8 = num * Z;
		float num9 = num2 * Z;
		float num10 = num * W;
		float num11 = num2 * W;
		float num12 = num3 * W;
		Matrix33F result = default;
		result.M00 = 1f - (num5 + num6);
		result.M01 = num7 - num12;
		result.M02 = num11 + num8;
		result.M10 = num7 + num12;
		result.M11 = 1f - (num4 + num6);
		result.M12 = num9 - num10;
		result.M20 = num8 - num11;
		result.M21 = num10 + num9;
		result.M22 = 1f - (num4 + num5);
		return result;
	}

	/// <summary>
	///       Returns the 4 x 4 rotation matrix of this quaternion.
	///       </summary>
	/// <returns>The rotation matrix.</returns>
	public Matrix44F ToRotationMatrix44()
	{
		Matrix44F result = default;
		float num = 2f * X;
		float num2 = 2f * Y;
		float num3 = 2f * Z;
		float num4 = num * X;
		float num5 = num2 * Y;
		float num6 = num3 * Z;
		float num7 = num * Y;
		float num8 = num * Z;
		float num9 = num2 * Z;
		float num10 = num * W;
		float num11 = num2 * W;
		float num12 = num3 * W;
		result.M00 = 1f - (num5 + num6);
		result.M01 = num7 - num12;
		result.M02 = num11 + num8;
		result.M03 = 0f;
		result.M10 = num7 + num12;
		result.M11 = 1f - (num4 + num6);
		result.M12 = num9 - num10;
		result.M13 = 0f;
		result.M20 = num8 - num11;
		result.M21 = num10 + num9;
		result.M22 = 1f - (num4 + num5);
		result.M23 = 0f;
		result.M30 = 0f;
		result.M31 = 0f;
		result.M32 = 0f;
		result.M33 = 1f;
		return result;
	}

	/// <summary>
	///       Tests if two quaternions are equal (within the tolerance 
	///       <see cref="P:DigitalRune.Mathematics.Numeric.EpsilonF" />).
	///       </summary>
	/// <param name="q1">The first quaternion.</param>
	/// <param name="q2">The second quaternion.</param>
	/// <returns>
	///   <see langword="true" /> if the quaternions are equal within the tolerance 
	///       <see cref="P:DigitalRune.Mathematics.Numeric.EpsilonF" />; otherwise <see langword="false" />.
	///       </returns>
	public static bool AreNumericallyEqual(QuaternionF q1, QuaternionF q2)
	{
		if (Numeric.AreEqual(q1.W, q2.W) && Numeric.AreEqual(q1.X, q2.X) && Numeric.AreEqual(q1.Y, q2.Y))
		{
			return Numeric.AreEqual(q1.Z, q2.Z);
		}
		return false;
	}

	/// <summary>
	///       Tests if two quaternions are equal (with a specific tolerance).
	///       </summary>
	/// <param name="q1">The first quaternion.</param>
	/// <param name="q2">The second quaternion.</param>
	/// <param name="epsilon">The tolerance value.</param>
	/// <returns>
	///   <see langword="true" /> if the quaternions are equal within the tolerance 
	///       <paramref name="epsilon" />; otherwise <see langword="false" />.
	///       </returns>
	public static bool AreNumericallyEqual(QuaternionF q1, QuaternionF q2, float epsilon)
	{
		if (Numeric.AreEqual(q1.W, q2.W, epsilon) && Numeric.AreEqual(q1.X, q2.X, epsilon) && Numeric.AreEqual(q1.Y, q2.Y, epsilon))
		{
			return Numeric.AreEqual(q1.Z, q2.Z, epsilon);
		}
		return false;
	}

	/// <summary>
	///       Returns the dot product of two quaternions.
	///       </summary>
	/// <param name="q1">The first quaternion.</param>
	/// <param name="q2">The second quaternion.</param>
	/// <returns>The dot product of the two quaternions.</returns>
	public static float Dot(QuaternionF q1, QuaternionF q2)
	{
		return q1.W * q2.W + q1.X * q2.X + q1.Y * q2.Y + q1.Z * q2.Z;
	}

	/// <summary>
	///       Creates a unit quaternion that specifies a rotation given by axis and angle.
	///       </summary>
	/// <param name="axis">The axis. (Vector does not need to be normalized.)</param>
	/// <param name="angle">The angle.</param>
	/// <returns>
	///   <para>
	///       The created unit quaternion that describes a rotation by the 
	///       <paramref name="angle" /> radians around the <paramref name="axis" />. 
	///       (<paramref name="axis" /> will be normalized automatically.)
	///       </para>
	///   <para>
	///       The resulting quaternion is: <i>q</i> = (cos(<i>θ</i>/2), <i><b>v</b></i>sin(<i>θ</i>/2))
	///       </para>
	///   <para>
	///     <i>q</i> = (cos(<i>θ</i>/2), <i><b>v</b></i>sin(<i>θ</i>/2))
	///       </para>
	///       where <i>θ</i> is the angle and <i><b>v</b></i> is the normalized axis.
	///       </returns>
	/// <exception cref="T:DigitalRune.Mathematics.MathematicsException">
	///   <paramref name="axis" /> is a zero vector.</exception>
	public static QuaternionF CreateRotation(Vector3F axis, float angle)
	{
		float num = angle / 2f;
		axis.Normalize();
		QuaternionF result = default;
		result.W = (float)Math.Cos(num);
		float num2 = (float)Math.Sin(num);
		result.X = num2 * axis.X;
		result.Y = num2 * axis.Y;
		result.Z = num2 * axis.Z;
		return result;
	}

	/// <summary>
	///       Creates a unit quaternion that specifies a rotation by a given angle around
	///       the x-axis.
	///       </summary>
	/// <param name="angle">The rotation angle in radians.</param>
	/// <returns>
	///       The created unit quaternion that describes a rotation by the 
	///       <paramref name="angle" /> radians around the x-axis.
	///       </returns>
	public static QuaternionF CreateRotationX(float angle)
	{
		float num = angle / 2f;
		QuaternionF result = default;
		result.W = (float)Math.Cos(num);
		result.X = (float)Math.Sin(num);
		result.Y = 0f;
		result.Z = 0f;
		return result;
	}

	/// <summary>
	///       Creates a unit quaternion that specifies a rotation by a given angle around
	///       the y-axis.
	///       </summary>
	/// <param name="angle">The rotation angle in radians.</param>
	/// <returns>
	///       The created unit quaternion that describes a rotation by the 
	///       <paramref name="angle" /> radians around the y-axis.
	///       </returns>
	public static QuaternionF CreateRotationY(float angle)
	{
		float num = angle / 2f;
		QuaternionF result = default;
		result.W = (float)Math.Cos(num);
		result.X = 0f;
		result.Y = (float)Math.Sin(num);
		result.Z = 0f;
		return result;
	}

	/// <summary>
	///       Creates a unit quaternion that specifies a rotation by a given angle around
	///       the z-axis.
	///       </summary>
	/// <param name="angle">The rotation angle in radians.</param>
	/// <returns>
	///       The created unit quaternion that describes a rotation by the 
	///       <paramref name="angle" /> radians around the z-axis.
	///       </returns>
	public static QuaternionF CreateRotationZ(float angle)
	{
		float num = angle / 2f;
		QuaternionF result = default;
		result.W = (float)Math.Cos(num);
		result.X = 0f;
		result.Y = 0f;
		result.Z = (float)Math.Sin(num);
		return result;
	}

	/// <summary>
	///       Creates a unit quaternion that specifies the same rotation as the given rotation matrix .
	///       </summary>
	/// <param name="rotationMatrix">A orientation matrix that specifies a rotation.</param>
	/// <returns>
	///       The creates unit quaternion that describes the same rotation as the rotation matrix.
	///       </returns>
	public static QuaternionF CreateRotation(Matrix33F rotationMatrix)
	{
		float num = rotationMatrix.M00 + rotationMatrix.M11 + rotationMatrix.M22;
		float w;
		float x;
		float y;
		float z;
		if (num > 0f)
		{
			float num2 = 0.5f / (float)Math.Sqrt(num + 1f);
			w = 0.25f / num2;
			x = (rotationMatrix.M21 - rotationMatrix.M12) * num2;
			y = (rotationMatrix.M02 - rotationMatrix.M20) * num2;
			z = (rotationMatrix.M10 - rotationMatrix.M01) * num2;
		}
		else if (rotationMatrix.M00 > rotationMatrix.M11 && rotationMatrix.M00 > rotationMatrix.M22)
		{
			float num3 = 2f * (float)Math.Sqrt(1f + rotationMatrix.M00 - rotationMatrix.M11 - rotationMatrix.M22);
			w = (rotationMatrix.M21 - rotationMatrix.M12) / num3;
			x = 0.25f * num3;
			y = (rotationMatrix.M01 + rotationMatrix.M10) / num3;
			z = (rotationMatrix.M02 + rotationMatrix.M20) / num3;
		}
		else if (rotationMatrix.M11 > rotationMatrix.M22)
		{
			float num4 = 2f * (float)Math.Sqrt(1f + rotationMatrix.M11 - rotationMatrix.M00 - rotationMatrix.M22);
			w = (rotationMatrix.M02 - rotationMatrix.M20) / num4;
			x = (rotationMatrix.M01 + rotationMatrix.M10) / num4;
			y = 0.25f * num4;
			z = (rotationMatrix.M12 + rotationMatrix.M21) / num4;
		}
		else
		{
			float num5 = 2f * (float)Math.Sqrt(1f + rotationMatrix.M22 - rotationMatrix.M00 - rotationMatrix.M11);
			w = (rotationMatrix.M10 - rotationMatrix.M01) / num5;
			x = (rotationMatrix.M02 + rotationMatrix.M20) / num5;
			y = (rotationMatrix.M12 + rotationMatrix.M21) / num5;
			z = 0.25f * num5;
		}
		return new QuaternionF(w, x, y, z);
	}

	/// <summary>
	///       Creates a unit quaternion that specifies a rotation given by two vectors.
	///       </summary>
	/// <param name="startVector">The initial vector. (Vector does not need to be normalized.)</param>
	/// <param name="rotatedVector">The rotated vector. (Vector does not need to be normalized.)</param>
	/// <returns>The created unit quaternion that would rotate <paramref name="startVector" />
	///       to <paramref name="rotatedVector" /></returns>
	/// <exception cref="T:System.ArgumentException">The length of the <paramref name="startVector" /> and 
	///       <paramref name="rotatedVector" /> must not be <c>0</c>.</exception>
	public static QuaternionF CreateRotation(Vector3F startVector, Vector3F rotatedVector)
	{
		try
		{
			startVector.Normalize();
		}
		catch (DivideByZeroException)
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1572));
		}
		try
		{
			rotatedVector.Normalize();
		}
		catch (DivideByZeroException)
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1655));
		}
		Vector3F vector3F = Vector3F.Cross(startVector, rotatedVector);
		float num = Vector3F.Dot(startVector, rotatedVector);
		float w;
		float x;
		float y;
		float z;
		if (Numeric.AreEqual(num, -1f))
		{
			w = 0f;
			vector3F = startVector.Orthonormal1;
			x = vector3F.X;
			y = vector3F.Y;
			z = vector3F.Z;
			return new QuaternionF(w, x, y, z);
		}
		float num2 = (float)Math.Sqrt(2f * (num + 1f));
		w = num2 / 2f;
		vector3F /= num2;
		x = vector3F.X;
		y = vector3F.Y;
		z = vector3F.Z;
		return new QuaternionF(w, x, y, z);
	}

	/// <summary>
	///       Calculates the exponential.
	///       </summary>
	/// <param name="quaternion">The quaternion.</param>
	/// <returns>The exponential e<sup><i>q</i></sup>.</returns>
	public static QuaternionF Exp(QuaternionF quaternion)
	{
		float num = (float)Math.Sqrt(quaternion.X * quaternion.X + quaternion.Y * quaternion.Y + quaternion.Z * quaternion.Z);
		float w = (float)Math.Cos(num);
		if (num > Numeric.EpsilonF)
		{
			float num2 = (float)Math.Sin(num) / num;
			quaternion.W = w;
			quaternion.X *= num2;
			quaternion.Y *= num2;
			quaternion.Z *= num2;
		}
		else
		{
			quaternion.W = w;
		}
		return quaternion;
	}

	/// <summary>
	///       Gets an orientation quaternion from Euler angles (3 rotations around 3 axes).
	///       </summary>
	/// <param name="angle1">The first angle.</param>
	/// <param name="axis1">The first axis.</param>
	/// <param name="angle2">The second angle.</param>
	/// <param name="axis2">The second axis.</param>
	/// <param name="angle3">The third angle.</param>
	/// <param name="axis3">The third axis.</param>
	/// <param name="useGlobalAxes">
	///       If set to <see langword="true" /> then the rotation axes are fixed in world space. Otherwise 
	///       the rotation axes are fixed on the object and rotated with each rotation.
	///       </param>
	/// <returns>
	///       The orientation quaternion that describes the same orientation as the given Euler angles.
	///       </returns>
	/// <exception cref="T:DigitalRune.Mathematics.MathematicsException">
	///       The length of the axis vectors must not be <c>0</c>.
	///       </exception>
	public static QuaternionF CreateRotation(float angle1, Vector3F axis1, float angle2, Vector3F axis2, float angle3, Vector3F axis3, bool useGlobalAxes)
	{
		QuaternionF quaternionF = CreateRotation(axis1, angle1);
		QuaternionF quaternionF2 = CreateRotation(axis2, angle2);
		QuaternionF quaternionF3 = CreateRotation(axis3, angle3);
		if (useGlobalAxes)
		{
			return quaternionF3 * quaternionF2 * quaternionF;
		}
		return quaternionF * quaternionF2 * quaternionF3;
	}

	/// <summary>
	///       Calculates the natural logarithm.
	///       </summary>
	/// <param name="quaternion">The quaternion.</param>
	/// <returns>The natural logarithm ln(<i>q</i>).</returns>
	/// <exception cref="T:DigitalRune.Mathematics.MathematicsException">
	///       The given quaternion is not a unit quaternion.
	///       </exception>
	public static QuaternionF Ln(QuaternionF quaternion)
	{
		if (Numeric.IsLessOrEqual(Math.Abs(quaternion.W), 1f))
		{
			float num = (float)Math.Sqrt(quaternion.X * quaternion.X + quaternion.Y * quaternion.Y + quaternion.Z * quaternion.Z);
			float num2 = (float)Math.Atan(num / quaternion.W);
			if (!Numeric.IsZero(num))
			{
				float num3 = num2 / num;
				quaternion.W = 0f;
				quaternion.X *= num3;
				quaternion.Y *= num3;
				quaternion.Z *= num3;
			}
			else
			{
				quaternion.W = 0f;
			}
			return quaternion;
		}
		throw new MathematicsException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1742));
	}

	/// <summary>
	///       Calculates the power of a unit quaternion.
	///       </summary>
	/// <param name="quaternion">The quaternion.</param>
	/// <param name="t">The exponent.</param>
	/// <returns>The power of the unit quaternion.</returns>
	public static QuaternionF Power(QuaternionF quaternion, float t)
	{
		return Exp(Ln(quaternion) * t);
	}

	/// <summary>
	///       Calculates the angle between two quaternions.
	///       </summary>
	/// <param name="quaternion1">The first quaternion.</param>
	/// <param name="quaternion2">The second quaternion.</param>
	/// <returns>The angle between the given vectors, such that 0 ≤ angle ≤ <c>π</c>.</returns>
	public static float GetAngle(QuaternionF quaternion1, QuaternionF quaternion2)
	{
		float num = Dot(quaternion1, quaternion2);
		MathHelper.Clamp(num, -1f, 1f);
		if (num < 0f)
		{
			num *= -1f;
		}
		return (float)(2.0 * Math.Acos(num));
	}

	/// <summary>
	///       Converts the string representation of a quaternion to its <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionF" /> 
	///       equivalent.
	///       </summary>
	/// <param name="s">A string representation of a 4-dimensional vector.</param>
	/// <returns>
	///       A <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionF" /> that represents the vector specified by the 
	///       <paramref name="s" /> parameter.</returns>
	public static QuaternionF Parse(string s)
	{
		return Parse(s, CultureInfo.CurrentCulture);
	}

	/// <summary>
	///       Converts the string representation of a quaternion in a specified 
	///       culture-specific format to its <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionF" /> equivalent.
	///       </summary>
	/// <param name="s">A string representation of a 4-dimensional vector.</param>
	/// <param name="provider">
	///       An <see cref="T:System.IFormatProvider" /> that supplies culture-specific formatting 
	///       information about <paramref name="s" />. 
	///       </param>
	/// <returns>
	///       A <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionF" /> that represents the vector specified by the 
	///       <paramref name="s" /> parameter.
	///       </returns>
	/// <exception cref="T:System.FormatException">
	///   <paramref name="s" /> is not a valid <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionF" />.
	///       </exception>
	public static QuaternionF Parse(string s, IFormatProvider provider)
	{
		Regex regex = new Regex(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1896), RegexOptions.None);
		Match match = regex.Match(s);
		if (match.Success)
		{
			return new QuaternionF(float.Parse(match.Groups[c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1995)].Value, provider), float.Parse(match.Groups[c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1103)].Value, provider), float.Parse(match.Groups[c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1106)].Value, provider), float.Parse(match.Groups[c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1109)].Value, provider));
		}
		throw new FormatException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(10653));
	}
}
