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
///       Defines a quaternion (double-precision).
///       </summary>
[Serializable]
[TypeConverter(typeof(QuaternionDConverter))]
public struct QuaternionD : IEquatable<QuaternionD>
{
	/// <summary>
	///       Returns a <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionD" /> with all of its components set to zero.
	///       </summary>
	public static readonly QuaternionD Zero;

	/// <summary>
	///       Returns the identity <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionD" /> (1, 0, 0, 0).
	///       </summary>
	public static readonly QuaternionD Identity;

	/// <summary>
	///       The w component.
	///       </summary>
	public double W;

	/// <summary>
	///       The x component.
	///       </summary>
	public double X;

	/// <summary>
	///       The y component.
	///       </summary>
	public double Y;

	/// <summary>
	///       The z component.
	///       </summary>
	public double Z;

	/// <summary>
	///       Gets or sets the angle of the rotation around <see cref="P:DigitalRune.Mathematics.Algebra.QuaternionD.Axis" />.
	///       </summary>
	[XmlIgnore]
	public double Angle
	{
		get
		{
			double w = W;
			if (Numeric.IsGreater(Math.Abs(w), 1.0))
			{
				return double.NaN;
			}
			w = MathHelper.Clamp(w, -1.0, 1.0);
			return Math.Acos(w) * 2.0;
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
	public Vector3D V
	{
		get
		{
			return new Vector3D(X, Y, Z);
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
	public Vector3D Axis
	{
		get
		{
			Vector3D result = new Vector3D(X, Y, Z);
			if (result.TryNormalize())
			{
				return result;
			}
			return Vector3D.Zero;
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
	public QuaternionD Conjugated
	{
		get
		{
			QuaternionD result = this;
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
	public QuaternionD Inverse
	{
		get
		{
			QuaternionD result = this;
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
	public QuaternionD Normalized
	{
		get
		{
			QuaternionD result = this;
			result.Normalize();
			return result;
		}
	}

	/// <summary>
	///       Returns the modulus (length).
	///       </summary>
	public double Modulus => Math.Sqrt(W * W + X * X + Y * Y + Z * Z);

	/// <summary>
	///       Returns the norm (<i>length<sup>2</sup></i>).
	///       </summary>
	public double Norm => W * W + X * X + Y * Y + Z * Z;

	/// <summary>
	///       Gets a value indicating whether a component of the quaternion is <see cref="F:System.Double.NaN" />.
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
	///       Returns a value indicating whether this quaternion is normalized (the <see cref="P:DigitalRune.Mathematics.Algebra.QuaternionD.Modulus" /> 
	///       is numerically equal to 1).
	///       </summary>
	public bool IsNumericallyNormalized => Numeric.AreEqual(Norm, 1.0);

	/// <summary>
	///       Gets or sets the component at the specified index.
	///       </summary>
	/// <param name="index">The index.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///       The <paramref name="index" /> is out of range.
	///       </exception>
	public double this[int index]
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

	static QuaternionD()
	{
		Zero = new QuaternionD(0.0, 0.0, 0.0, 0.0);
		Identity = new QuaternionD(1.0, 0.0, 0.0, 0.0);
		TypeInitializer.Initialize(cb8acf96051824377b85f63be63e3c2a3.ce9f9bc9ecd5ed400a711abaa33452c25);
		TypeInitializer.CreateInstance(11, cb8acf96051824377b85f63be63e3c2a3.ccda5e1cdd8faa90b24f6bc28c9af6b28);
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionD" /> class.
	///       </summary>
	/// <param name="w">The initial value for the w component.</param>
	/// <param name="x">The initial value for the x component.</param>
	/// <param name="y">The initial value for the y component.</param>
	/// <param name="z">The initial value for the z component.</param>
	public QuaternionD(double w, double x, double y, double z)
	{
		W = w;
		X = x;
		Y = y;
		Z = z;
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionD" /> class.
	///       </summary>
	/// <param name="components">
	///       Array with the initial values for the components w, x, y and z.
	///       </param>
	/// <exception cref="T:System.IndexOutOfRangeException">
	///   <paramref name="components" /> has less than 4 elements.
	///       </exception>
	/// <exception cref="T:System.NullReferenceException">
	///   <paramref name="components" /> must not be <see langword="null" />.
	///       </exception>
	public QuaternionD(double[] components)
	{
		W = components[0];
		X = components[1];
		Y = components[2];
		Z = components[3];
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionD" /> class.
	///       </summary>
	/// <param name="components">
	///       List with the initial values for the components w, x, y and z.
	///       </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="components" /> has less than 4 elements.
	///       </exception>
	/// <exception cref="T:System.NullReferenceException">
	///   <paramref name="components" /> must not be <see langword="null" />.
	///       </exception>
	public QuaternionD(IList<double> components)
	{
		W = components[0];
		X = components[1];
		Y = components[2];
		Z = components[3];
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionD" /> class.
	///       </summary>
	/// <param name="w">The initial value for scalar component w.</param>
	/// <param name="v">The initial values for the vector component (x, y, z).</param>
	public QuaternionD(double w, Vector3D v)
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
		if (obj is QuaternionD)
		{
			return this == (QuaternionD)obj;
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
	public bool Equals(QuaternionD other)
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
	public static QuaternionD operator -(QuaternionD quaternion)
	{
		quaternion.X = 0.0 - quaternion.X;
		quaternion.Y = 0.0 - quaternion.Y;
		quaternion.Z = 0.0 - quaternion.Z;
		quaternion.W = 0.0 - quaternion.W;
		return quaternion;
	}

	/// <summary>
	///       Negates a quaternion.
	///       </summary>
	/// <param name="quaternion">The quaternion.</param>
	/// <returns>The negated quaternion.</returns>
	public static QuaternionD Negate(QuaternionD quaternion)
	{
		quaternion.X = 0.0 - quaternion.X;
		quaternion.Y = 0.0 - quaternion.Y;
		quaternion.Z = 0.0 - quaternion.Z;
		quaternion.W = 0.0 - quaternion.W;
		return quaternion;
	}

	/// <summary>
	///       Adds two quaternions.
	///       </summary>
	/// <param name="quaternion1">The first quaternion.</param>
	/// <param name="quaternion2">The second quaternion.</param>
	/// <returns>The sum of the two quaternions.</returns>
	public static QuaternionD operator +(QuaternionD quaternion1, QuaternionD quaternion2)
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
	public static QuaternionD Add(QuaternionD quaternion1, QuaternionD quaternion2)
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
	public static QuaternionD operator -(QuaternionD minuend, QuaternionD subtrahend)
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
	public static QuaternionD Subtract(QuaternionD minuend, QuaternionD subtrahend)
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
	public static QuaternionD operator *(QuaternionD quaternion, double scalar)
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
	public static QuaternionD operator *(double scalar, QuaternionD quaternion)
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
	public static QuaternionD Multiply(double scalar, QuaternionD quaternion)
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
	public static QuaternionD operator *(QuaternionD q1, QuaternionD q2)
	{
		QuaternionD result = default;
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
	public static QuaternionD Multiply(QuaternionD q1, QuaternionD q2)
	{
		QuaternionD result = default;
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
	public static QuaternionD operator /(QuaternionD quaternion, double scalar)
	{
		double num = 1.0 / scalar;
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
	public static QuaternionD Divide(QuaternionD quaternion, double scalar)
	{
		double num = 1.0 / scalar;
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
	public static QuaternionD operator /(QuaternionD dividend, QuaternionD divisor)
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
	public static QuaternionD Divide(QuaternionD dividend, QuaternionD divisor)
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
	public static bool operator ==(QuaternionD q1, QuaternionD q2)
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
	public static bool operator !=(QuaternionD q1, QuaternionD q2)
	{
		if (q1.W == q2.W && q1.X == q2.X && q1.Y == q2.Y)
		{
			return q1.Z != q2.Z;
		}
		return true;
	}

	/// <summary>
	///       Converts the quaternion to an array of 4 double values: (w, x, y, z).
	///       </summary>
	/// <param name="quaternion">The quaternion.</param>
	/// <returns>The array with 4 values (w, x, y, z).</returns>
	public static explicit operator double[](QuaternionD quaternion)
	{
		return new double[4] { quaternion.W, quaternion.X, quaternion.Y, quaternion.Z };
	}

	/// <summary>
	///       Converts the quaternion to an array of 4 double values: (w, x, y, z).
	///       </summary>
	/// <returns>The array with 4 values (w, x, y, z).</returns>
	public double[] ToArray()
	{
		return (double[])this;
	}

	/// <summary>
	///       Converts the vector to a list of 4 double values: (w, x, y, z).
	///       </summary>
	/// <param name="quaternion">The quaternion.</param>
	/// <returns>The list with 4 values (w, x, y, z).</returns>
	public static explicit operator List<double>(QuaternionD quaternion)
	{
		List<double> list = new List<double>(4);
		list.Add(quaternion.W);
		list.Add(quaternion.X);
		list.Add(quaternion.Y);
		list.Add(quaternion.Z);
		return list;
	}

	/// <summary>
	///       Converts the vector to a list of 4 double values: (w, x, y, z).
	///       </summary>
	/// <returns>The list with 4 values (w, x, y, z).</returns>
	public List<double> ToList()
	{
		return (List<double>)this;
	}

	/// <summary>
	///       Performs an explicit conversion from <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionD" /> to <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionF" />.
	///       </summary>
	/// <param name="quaternion">The DigitalRune <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionD" />.</param>
	/// <returns>The result of the conversion.</returns>
	public static explicit operator QuaternionF(QuaternionD quaternion)
	{
		return new QuaternionF((float)quaternion.W, (float)quaternion.X, (float)quaternion.Y, (float)quaternion.Z);
	}

	/// <summary>
	///       Converts this <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionD" /> to <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionF" />.
	///       </summary>
	/// <returns>The result of the conversion.</returns>
	public QuaternionF ToQuaternionF()
	{
		return new QuaternionF((float)W, (float)X, (float)Y, (float)Z);
	}

	/// <summary>
	///       Performs an conversion from <see cref="T:Microsoft.Xna.Framework.Quaternion" /> (XNA Framework) to 
	///       <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionD" /> (DigitalRune Mathematics).
	///       </summary>
	/// <param name="quaternion">The <see cref="T:Microsoft.Xna.Framework.Quaternion" /> (XNA Framework).</param>
	/// <returns>The <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionD" /> (DigitalRune Mathematics).</returns>
	public static explicit operator QuaternionD(Quaternion quaternion)
	{
		return new QuaternionD(quaternion.W, quaternion.X, quaternion.Y, quaternion.Z);
	}

	/// <summary>
	///       Converts this <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionD" /> (DigitalRune Mathematics) to 
	///       <see cref="T:Microsoft.Xna.Framework.Quaternion" /> (XNA Framework).
	///       </summary>
	/// <param name="quaternion">The <see cref="T:Microsoft.Xna.Framework.Quaternion" /> (XNA Framework).</param>
	/// <returns>The <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionD" /> (DigitalRune Mathematics).</returns>
	public static QuaternionD FromXna(Quaternion quaternion)
	{
		return new QuaternionD(quaternion.W, quaternion.X, quaternion.Y, quaternion.Z);
	}

	/// <summary>
	///       Performs an conversion from <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionD" /> 
	///       (DigitalRune Mathematics) to <see cref="T:Microsoft.Xna.Framework.Quaternion" /> (XNA Framework).
	///       </summary>
	/// <param name="quaternion">The <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionD" /> (DigitalRune Mathematics).</param>
	/// <returns>The <see cref="T:Microsoft.Xna.Framework.Quaternion" /> (XNA Framework).</returns>
	public static explicit operator Quaternion(QuaternionD quaternion)
	{
		return new Quaternion((float)quaternion.X, (float)quaternion.Y, (float)quaternion.Z, (float)quaternion.W);
	}

	/// <summary>
	///       Converts this <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionD" /> (DigitalRune Mathematics) to <see cref="T:Microsoft.Xna.Framework.Quaternion" /> 
	///       (XNA Framework).
	///       </summary>
	/// <returns>The <see cref="T:Microsoft.Xna.Framework.Quaternion" /> (XNA Framework).</returns>
	public Quaternion ToXna()
	{
		return new Quaternion((float)X, (float)Y, (float)Z, (float)W);
	}

	/// <summary>
	///       Sets this quaternion to its conjugate.
	///       </summary>
	public void Conjugate()
	{
		X = 0.0 - X;
		Y = 0.0 - Y;
		Z = 0.0 - Z;
	}

	/// <summary>
	///       Inverts the quaternion.
	///       </summary>
	/// <exception cref="T:DigitalRune.Mathematics.MathematicsException">
	///       The length of the quaternion is zero. The quaternion cannot be inverted.
	///       </exception>
	public void Invert()
	{
		double norm = Norm;
		if (Numeric.IsZero(norm, Numeric.EpsilonDSquared))
		{
			throw new MathematicsException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1327));
		}
		double num = 1.0 / norm;
		W *= num;
		X = (0.0 - X) * num;
		Y = (0.0 - Y) * num;
		Z = (0.0 - Z) * num;
	}

	/// <summary>
	///       Normalizes the quaternion.
	///       </summary>
	/// <exception cref="T:System.DivideByZeroException">
	///       The length of the quaternion is zero. The quaternion cannot be normalized.
	///       </exception>
	public void Normalize()
	{
		double norm = Norm;
		if (Numeric.IsZero(norm))
		{
			throw new DivideByZeroException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1473));
		}
		double num = 1.0 / Math.Sqrt(norm);
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
		double norm = Norm;
		if (Numeric.IsZero(norm, Numeric.EpsilonDSquared))
		{
			return false;
		}
		double num = 1.0 / Math.Sqrt(norm);
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
	public void Power(double t)
	{
		this = Power(this, t);
	}

	/// <summary>
	///       Rotates a vector.
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <returns>The rotated vector.</returns>
	public Vector3D Rotate(Vector3D vector)
	{
		Vector3D v = V;
		double w = W;
		double num = 0.0 - (v.X * vector.X + v.Y * vector.Y + v.Z * vector.Z);
		double num2 = v.Y * vector.Z - v.Z * vector.Y + w * vector.X;
		double num3 = v.Z * vector.X - v.X * vector.Z + w * vector.Y;
		double num4 = v.X * vector.Y - v.Y * vector.X + w * vector.Z;
		double num5 = 0.0 - X;
		double num6 = 0.0 - Y;
		double num7 = 0.0 - Z;
		return new Vector3D(num3 * num7 - num4 * num6 + num * num5 + w * num2, num4 * num5 - num2 * num7 + num * num6 + w * num3, num2 * num6 - num3 * num5 + num * num7 + w * num4);
	}

	/// <summary>
	///       Returns the 3 x 3 rotation matrix of this quaternion.
	///       </summary>
	/// <returns>The rotation matrix.</returns>
	public Matrix33D ToRotationMatrix33()
	{
		double num = 2.0 * X;
		double num2 = 2.0 * Y;
		double num3 = 2.0 * Z;
		double num4 = num * X;
		double num5 = num2 * Y;
		double num6 = num3 * Z;
		double num7 = num * Y;
		double num8 = num * Z;
		double num9 = num2 * Z;
		double num10 = num * W;
		double num11 = num2 * W;
		double num12 = num3 * W;
		Matrix33D result = default;
		result.M00 = 1.0 - (num5 + num6);
		result.M01 = num7 - num12;
		result.M02 = num11 + num8;
		result.M10 = num7 + num12;
		result.M11 = 1.0 - (num4 + num6);
		result.M12 = num9 - num10;
		result.M20 = num8 - num11;
		result.M21 = num10 + num9;
		result.M22 = 1.0 - (num4 + num5);
		return result;
	}

	/// <summary>
	///       Returns the 4 x 4 rotation matrix of this quaternion.
	///       </summary>
	/// <returns>The rotation matrix.</returns>
	public Matrix44D ToRotationMatrix44()
	{
		Matrix44D result = default;
		double num = 2.0 * X;
		double num2 = 2.0 * Y;
		double num3 = 2.0 * Z;
		double num4 = num * X;
		double num5 = num2 * Y;
		double num6 = num3 * Z;
		double num7 = num * Y;
		double num8 = num * Z;
		double num9 = num2 * Z;
		double num10 = num * W;
		double num11 = num2 * W;
		double num12 = num3 * W;
		result.M00 = 1.0 - (num5 + num6);
		result.M01 = num7 - num12;
		result.M02 = num11 + num8;
		result.M03 = 0.0;
		result.M10 = num7 + num12;
		result.M11 = 1.0 - (num4 + num6);
		result.M12 = num9 - num10;
		result.M13 = 0.0;
		result.M20 = num8 - num11;
		result.M21 = num10 + num9;
		result.M22 = 1.0 - (num4 + num5);
		result.M23 = 0.0;
		result.M30 = 0.0;
		result.M31 = 0.0;
		result.M32 = 0.0;
		result.M33 = 1.0;
		return result;
	}

	/// <summary>
	///       Tests if two quaternions are equal (within the tolerance 
	///       <see cref="P:DigitalRune.Mathematics.Numeric.EpsilonD" />).
	///       </summary>
	/// <param name="q1">The first quaternion.</param>
	/// <param name="q2">The second quaternion.</param>
	/// <returns>
	///   <see langword="true" /> if the quaternions are equal within the tolerance 
	///       <see cref="P:DigitalRune.Mathematics.Numeric.EpsilonD" />; otherwise <see langword="false" />.
	///       </returns>
	public static bool AreNumericallyEqual(QuaternionD q1, QuaternionD q2)
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
	public static bool AreNumericallyEqual(QuaternionD q1, QuaternionD q2, double epsilon)
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
	public static double Dot(QuaternionD q1, QuaternionD q2)
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
	public static QuaternionD CreateRotation(Vector3D axis, double angle)
	{
		double num = angle / 2.0;
		axis.Normalize();
		QuaternionD result = default;
		result.W = Math.Cos(num);
		double num2 = Math.Sin(num);
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
	public static QuaternionD CreateRotationX(double angle)
	{
		double num = angle / 2.0;
		QuaternionD result = default;
		result.W = Math.Cos(num);
		result.X = Math.Sin(num);
		result.Y = 0.0;
		result.Z = 0.0;
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
	public static QuaternionD CreateRotationY(double angle)
	{
		double num = angle / 2.0;
		QuaternionD result = default;
		result.W = Math.Cos(num);
		result.X = 0.0;
		result.Y = Math.Sin(num);
		result.Z = 0.0;
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
	public static QuaternionD CreateRotationZ(double angle)
	{
		double num = angle / 2.0;
		QuaternionD result = default;
		result.W = Math.Cos(num);
		result.X = 0.0;
		result.Y = 0.0;
		result.Z = Math.Sin(num);
		return result;
	}

	/// <summary>
	///       Creates a unit quaternion that specifies the same rotation as the given rotation matrix.
	///       </summary>
	/// <param name="rotationMatrix">A orientation matrix that specifies a rotation.</param>
	/// <returns>
	///       The creates unit quaternion that describes the same rotation as the rotation matrix.
	///       </returns>
	public static QuaternionD CreateRotation(Matrix33D rotationMatrix)
	{
		double num = rotationMatrix.M00 + rotationMatrix.M11 + rotationMatrix.M22;
		double w;
		double x;
		double y;
		double z;
		if (num > 0.0)
		{
			double num2 = 0.5 / Math.Sqrt(num + 1.0);
			w = 0.25 / num2;
			x = (rotationMatrix.M21 - rotationMatrix.M12) * num2;
			y = (rotationMatrix.M02 - rotationMatrix.M20) * num2;
			z = (rotationMatrix.M10 - rotationMatrix.M01) * num2;
		}
		else if (rotationMatrix.M00 > rotationMatrix.M11 && rotationMatrix.M00 > rotationMatrix.M22)
		{
			double num3 = 2.0 * Math.Sqrt(1.0 + rotationMatrix.M00 - rotationMatrix.M11 - rotationMatrix.M22);
			w = (rotationMatrix.M21 - rotationMatrix.M12) / num3;
			x = 0.25 * num3;
			y = (rotationMatrix.M01 + rotationMatrix.M10) / num3;
			z = (rotationMatrix.M02 + rotationMatrix.M20) / num3;
		}
		else if (rotationMatrix.M11 > rotationMatrix.M22)
		{
			double num4 = 2.0 * Math.Sqrt(1.0 + rotationMatrix.M11 - rotationMatrix.M00 - rotationMatrix.M22);
			w = (rotationMatrix.M02 - rotationMatrix.M20) / num4;
			x = (rotationMatrix.M01 + rotationMatrix.M10) / num4;
			y = 0.25 * num4;
			z = (rotationMatrix.M12 + rotationMatrix.M21) / num4;
		}
		else
		{
			double num5 = 2.0 * Math.Sqrt(1.0 + rotationMatrix.M22 - rotationMatrix.M00 - rotationMatrix.M11);
			w = (rotationMatrix.M10 - rotationMatrix.M01) / num5;
			x = (rotationMatrix.M02 + rotationMatrix.M20) / num5;
			y = (rotationMatrix.M12 + rotationMatrix.M21) / num5;
			z = 0.25 * num5;
		}
		return new QuaternionD(w, x, y, z);
	}

	/// <summary>
	///       Creates a unit quaterion that specifies a rotation given by two vectors.
	///       </summary>
	/// <param name="startVector">The initial vector. (Vector does not need to be normalized.)</param>
	/// <param name="rotatedVector">The rotated vector. (Vector does not need to be normalized.)</param>
	/// <returns>The created unit quaternion that would rotate <paramref name="startVector" />
	///       to <paramref name="rotatedVector" /></returns>
	/// <exception cref="T:System.ArgumentException">The length of the <paramref name="startVector" /> and 
	///       <paramref name="rotatedVector" /> must not be <c>0</c>.</exception>
	public static QuaternionD CreateRotation(Vector3D startVector, Vector3D rotatedVector)
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
		Vector3D vector3D = Vector3D.Cross(startVector, rotatedVector);
		double num = Vector3D.Dot(startVector, rotatedVector);
		double w;
		double x;
		double y;
		double z;
		if (Numeric.AreEqual(num, -1.0))
		{
			w = 0.0;
			vector3D = startVector.Orthonormal1;
			x = vector3D.X;
			y = vector3D.Y;
			z = vector3D.Z;
			return new QuaternionD(w, x, y, z);
		}
		double num2 = Math.Sqrt(2.0 * (num + 1.0));
		w = num2 / 2.0;
		vector3D /= num2;
		x = vector3D.X;
		y = vector3D.Y;
		z = vector3D.Z;
		return new QuaternionD(w, x, y, z);
	}

	/// <summary>
	///       Calculates the exponential.
	///       </summary>
	/// <param name="quaternion">The quaternion.</param>
	/// <returns>The exponential e<sup><i>q</i></sup>.</returns>
	public static QuaternionD Exp(QuaternionD quaternion)
	{
		double num = Math.Sqrt(quaternion.X * quaternion.X + quaternion.Y * quaternion.Y + quaternion.Z * quaternion.Z);
		double w = Math.Cos(num);
		if (num > Numeric.EpsilonD)
		{
			double num2 = Math.Sin(num) / num;
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
	public static QuaternionD CreateRotation(double angle1, Vector3D axis1, double angle2, Vector3D axis2, double angle3, Vector3D axis3, bool useGlobalAxes)
	{
		QuaternionD quaternionD = CreateRotation(axis1, angle1);
		QuaternionD quaternionD2 = CreateRotation(axis2, angle2);
		QuaternionD quaternionD3 = CreateRotation(axis3, angle3);
		if (useGlobalAxes)
		{
			return quaternionD3 * quaternionD2 * quaternionD;
		}
		return quaternionD * quaternionD2 * quaternionD3;
	}

	/// <summary>
	///       Calculates the natural logarithm.
	///       </summary>
	/// <param name="quaternion">The quaternion.</param>
	/// <returns>The natural logarithm ln(<i>q</i>).</returns>
	/// <exception cref="T:DigitalRune.Mathematics.MathematicsException">
	///       The given quaternion is not a unit quaternion.
	///       </exception>
	public static QuaternionD Ln(QuaternionD quaternion)
	{
		if (Numeric.IsLessOrEqual(Math.Abs(quaternion.W), 1.0))
		{
			double num = Math.Sqrt(quaternion.X * quaternion.X + quaternion.Y * quaternion.Y + quaternion.Z * quaternion.Z);
			double num2 = Math.Atan(num / quaternion.W);
			if (!Numeric.IsZero(num))
			{
				double num3 = num2 / num;
				quaternion.W = 0.0;
				quaternion.X *= num3;
				quaternion.Y *= num3;
				quaternion.Z *= num3;
			}
			else
			{
				quaternion.W = 0.0;
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
	public static QuaternionD Power(QuaternionD quaternion, double t)
	{
		return Exp(Ln(quaternion) * t);
	}

	/// <summary>
	///       Calculates the angle between two quaternions.
	///       </summary>
	/// <param name="quaternion1">The first quaternion.</param>
	/// <param name="quaternion2">The second quaternion.</param>
	/// <returns>The angle between the given vectors, such that 0 ≤ angle ≤ <c>π</c>.</returns>
	public static double GetAngle(QuaternionD quaternion1, QuaternionD quaternion2)
	{
		double num = Dot(quaternion1, quaternion2);
		MathHelper.Clamp(num, -1.0, 1.0);
		if (num < 0.0)
		{
			num *= -1.0;
		}
		return 2.0 * Math.Acos(num);
	}

	/// <summary>
	///       Converts the string representation of a quaternion to its <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionD" /> 
	///       equivalent.
	///       </summary>
	/// <param name="s">A string representation of a 4-dimensional vector.</param>
	/// <returns>
	///       A <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionD" /> that represents the vector specified by the 
	///       <paramref name="s" /> parameter.</returns>
	public static QuaternionD Parse(string s)
	{
		return Parse(s, CultureInfo.CurrentCulture);
	}

	/// <summary>
	///       Converts the string representation of a quaternion in a specified 
	///       culture-specific format to its <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionD" /> equivalent.
	///       </summary>
	/// <param name="s">A string representation of a 4-dimensional vector.</param>
	/// <param name="provider">
	///       An <see cref="T:System.IFormatProvider" /> that supplies culture-specific formatting 
	///       information about <paramref name="s" />. 
	///       </param>
	/// <returns>
	///       A <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionD" /> that represents the vector specified by the 
	///       <paramref name="s" /> parameter.
	///       </returns>
	/// <exception cref="T:System.FormatException">
	///   <paramref name="s" /> is not a valid <see cref="T:DigitalRune.Mathematics.Algebra.QuaternionD" />.
	///       </exception>
	public static QuaternionD Parse(string s, IFormatProvider provider)
	{
		Regex regex = new Regex(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1896), RegexOptions.None);
		Match match = regex.Match(s);
		if (match.Success)
		{
			return new QuaternionD(double.Parse(match.Groups[c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1995)].Value, provider), double.Parse(match.Groups[c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1103)].Value, provider), double.Parse(match.Groups[c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1106)].Value, provider), double.Parse(match.Groups[c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1109)].Value, provider));
		}
		throw new FormatException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(1998));
	}
}
