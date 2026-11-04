using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using A;

namespace DigitalRune.Mathematics.Algebra;

/// <summary>
///       Defines an n-dimensional vector (double-precision).
///       </summary>
[Serializable]
[TypeConverter(typeof(ExpandableObjectConverter))]
public class VectorD : ICloneable, IEquatable<VectorD>, ISerializable, IXmlSerializable
{
	private double[] _v;

	/// <summary>
	///       Gets or sets the internal array that is used to store the vector values.
	///       </summary>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="value" /> is <see langword="null" />.
	///       </exception>
	public double[] InternalArray
	{
		get
		{
			return _v;
		}
		set
		{
			if (value == null)
			{
				throw new ArgumentNullException();
			}
			_v = value;
		}
	}

	/// <summary>
	///       Gets or sets the component at the specified index.
	///       </summary>
	/// <param name="index">The index.</param>
	/// <exception cref="T:System.IndexOutOfRangeException">
	///       The <paramref name="index" /> is out of range.
	///       </exception>
	public double this[int index]
	{
		get
		{
			return _v[index];
		}
		set
		{
			_v[index] = value;
		}
	}

	/// <summary>
	///       Gets a value indicating whether a component of the vector is <see cref="F:System.Double.NaN" />.
	///       </summary>
	public bool IsNaN
	{
		get
		{
			for (int i = 0; i < _v.Length; i++)
			{
				if (Numeric.IsNaN(_v[i]))
				{
					return true;
				}
			}
			return false;
		}
	}

	/// <summary>
	///       Returns a value indicating whether this vector is normalized (the length is numerically
	///       equal to 1).
	///       </summary>
	public bool IsNumericallyNormalized => Numeric.AreEqual(LengthSquared, 1.0);

	/// <summary>
	///       Returns a value indicating whether this vector has zero size (the length is numerically
	///       equal to 0).
	///       </summary>
	public bool IsNumericallyZero => Numeric.IsZero(LengthSquared, Numeric.EpsilonDSquared);

	/// <summary>
	///       Gets or sets the length of this vector.
	///       </summary>
	/// <returns>The length of the this vector.</returns>
	/// <exception cref="T:DigitalRune.Mathematics.MathematicsException">
	///       The vector has a length of 0. The length cannot be changed.
	///       </exception>
	[XmlIgnore]
	public double Length
	{
		get
		{
			return Math.Sqrt(LengthSquared);
		}
		set
		{
			double length = Length;
			if (Numeric.IsZero(length))
			{
				throw new MathematicsException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(748));
			}
			double num = value / length;
			for (int i = 0; i < NumberOfElements; i++)
			{
				_v[i] *= num;
			}
		}
	}

	/// <summary>
	///       Returns the squared length of this vector.
	///       </summary>
	/// <returns>The squared length of this vector.</returns>
	public double LengthSquared => Dot(this, this);

	/// <summary>
	///       Returns the normalized vector.
	///       </summary>
	/// <exception cref="T:System.DivideByZeroException">
	///       The length of the vector is zero. The quaternion cannot be normalized.
	///       </exception>
	public VectorD Normalized
	{
		get
		{
			VectorD vectorD = Clone();
			vectorD.Normalize();
			return vectorD;
		}
	}

	/// <summary>
	///       Gets the number of elements <i>n</i>.
	///       </summary>
	public int NumberOfElements => _v.Length;

	/// <summary>
	///       Gets the value of the largest element.
	///       </summary>
	public double LargestElement
	{
		get
		{
			double num = _v[0];
			for (int i = 1; i < NumberOfElements; i++)
			{
				if (_v[i] > num)
				{
					num = _v[i];
				}
			}
			return num;
		}
	}

	/// <summary>
	///       Gets the index (zero-based) of the largest element.
	///       </summary>
	public int IndexOfLargestElement
	{
		get
		{
			double num = _v[0];
			int result = 0;
			for (int i = 1; i < NumberOfElements; i++)
			{
				if (_v[i] > num)
				{
					num = _v[i];
					result = i;
				}
			}
			return result;
		}
	}

	/// <summary>
	///       Gets the value of the smallest element.
	///       </summary>
	public double SmallestElement
	{
		get
		{
			double num = _v[0];
			for (int i = 1; i < NumberOfElements; i++)
			{
				if (_v[i] < num)
				{
					num = _v[i];
				}
			}
			return num;
		}
	}

	/// <summary>
	///       Gets the index (zero-based) of the smallest element.
	///       </summary>
	public int IndexOfSmallestElement
	{
		get
		{
			double num = _v[0];
			int result = 0;
			for (int i = 1; i < NumberOfElements; i++)
			{
				if (_v[i] < num)
				{
					num = _v[i];
					result = i;
				}
			}
			return result;
		}
	}

	static VectorD()
	{
		TypeInitializer.Initialize(cb8acf96051824377b85f63be63e3c2a3.ce9f9bc9ecd5ed400a711abaa33452c25);
		TypeInitializer.CreateInstance(11, cb8acf96051824377b85f63be63e3c2a3.ccda5e1cdd8faa90b24f6bc28c9af6b28);
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Algebra.VectorD" /> class with 4 vector elements.
	///       </summary>
	public VectorD()
		: this(4)
	{
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Algebra.VectorD" /> class.
	///       </summary>
	/// <param name="numberOfElements">The number of elements.</param>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="numberOfElements" /> must be greater than 0.
	///       </exception>
	public VectorD(int numberOfElements)
	{
		if (numberOfElements <= 0)
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9811), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9904));
		}
		_v = new double[numberOfElements];
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Algebra.VectorD" /> class.
	///       </summary>
	/// <param name="numberOfElements">The number of elements.</param>
	/// <param name="value">The initial value for the vector elements.</param>
	public VectorD(int numberOfElements, double value)
		: this(numberOfElements)
	{
		for (int i = 0; i < numberOfElements; i++)
		{
			_v[i] = value;
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Algebra.VectorD" /> class.
	///       </summary>
	/// <param name="elements">The array with the initial values for the vector elements.</param>
	/// <exception cref="T:System.NullReferenceException">
	///   <paramref name="elements" /> must not be <see langword="null" />.
	///       </exception>
	public VectorD(double[] elements)
		: this(elements.Length)
	{
		for (int i = 0; i < NumberOfElements; i++)
		{
			_v[i] = elements[i];
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Algebra.VectorD" /> class.
	///       </summary>
	/// <param name="elements">The list with the initial values for the vector elements.</param>
	/// <exception cref="T:System.NullReferenceException">
	///   <paramref name="elements" /> must not be <see langword="null" />.
	///       </exception>
	public VectorD(IList<double> elements)
		: this(elements.Count)
	{
		for (int i = 0; i < NumberOfElements; i++)
		{
			_v[i] = elements[i];
		}
	}

	/// <summary>
	///       Returns the hash code for this instance.
	///       </summary>
	/// <returns>
	///       A 32-bit signed integer that is the hash code for this instance.
	///       </returns>
	public override int GetHashCode()
	{
		int num = 0;
		for (int i = 0; i < NumberOfElements; i++)
		{
			num = (num * 397) ^ _v[i].GetHashCode();
		}
		return num;
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
		VectorD vectorD = obj as VectorD;
		if (vectorD == null)
		{
			return false;
		}
		return this == vectorD;
	}

	/// <summary>
	///       Indicates whether the current object is equal to another object of the same type.
	///       </summary>
	/// <param name="other">An object to compare with this object.</param>
	/// <returns>
	///   <see langword="true" /> if the current object is equal to the other parameter; otherwise, 
	///       <see langword="false" />.
	///       </returns>
	public bool Equals(VectorD other)
	{
		return this == other;
	}

	private object c48423685eed44c8f6f4708513cf05f2b()
	{
		return Clone();
	}

	object ICloneable.Clone()
	{
		//ILSpy generated this explicit interface implementation from .override directive in c48423685eed44c8f6f4708513cf05f2b
		return this.c48423685eed44c8f6f4708513cf05f2b();
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
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7795));
		for (int i = 0; i < NumberOfElements; i++)
		{
			stringBuilder.Append(_v[i]);
			if (i + 1 < NumberOfElements)
			{
				stringBuilder.Append(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7798));
			}
		}
		stringBuilder.Append(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9126));
		return string.Format(provider, stringBuilder.ToString());
	}

	/// <summary>
	///       This property is reserved, apply the <see cref="T:System.Xml.Serialization.XmlSchemaProviderAttribute" /> to the class 
	///       instead.
	///       </summary>
	/// <returns>
	///       An <see cref="T:System.Xml.Schema.XmlSchema" /> that describes the XML representation of the object that is 
	///       produced by the <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)" /> method and 
	///       consumed by the <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)" /> method.
	///       </returns>
	public XmlSchema GetSchema()
	{
		return null;
	}

	/// <summary>
	///       Generates an object from its XML representation.
	///       </summary>
	/// <param name="reader">
	///       The <see cref="T:System.Xml.XmlReader" /> stream from which the object is deserialized.
	///       </param>
	public void ReadXml(XmlReader reader)
	{
		reader.ReadStartElement();
		reader.ReadStartElement(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9129));
		int num = reader.ReadContentAsInt();
		reader.ReadEndElement();
		reader.ReadStartElement(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7832));
		_v = new double[num];
		for (int i = 0; i < num; i++)
		{
			_v[i] = reader.ReadElementContentAsDouble();
		}
		reader.ReadEndElement();
		reader.ReadEndElement();
	}

	/// <summary>
	///       Converts an object into its XML representation.
	///       </summary>
	/// <param name="writer">
	///       The <see cref="T:System.Xml.XmlWriter" /> stream to which the object is serialized.
	///       </param>
	public void WriteXml(XmlWriter writer)
	{
		writer.WriteStartElement(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9129));
		writer.WriteValue(NumberOfElements);
		writer.WriteEndElement();
		writer.WriteStartElement(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7832));
		for (int i = 0; i < NumberOfElements; i++)
		{
			writer.WriteStartElement(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7849));
			writer.WriteValue(_v[i]);
			writer.WriteEndElement();
		}
		writer.WriteEndElement();
	}

	/// <summary>
	///       Negates a vector.
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <returns>The negated vector.</returns>
	public static VectorD operator -(VectorD vector)
	{
		if (vector == null)
		{
			return null;
		}
		VectorD vectorD = vector.Clone();
		for (int i = 0; i < vectorD.NumberOfElements; i++)
		{
			vectorD[i] = 0.0 - vectorD[i];
		}
		return vectorD;
	}

	/// <summary>
	///       Negates a vector.
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <returns>The negated vector.</returns>
	public static VectorD Negate(VectorD vector)
	{
		return -vector;
	}

	/// <summary>
	///       Adds two vectors.
	///       </summary>
	/// <param name="vector1">The first vector.</param>
	/// <param name="vector2">The second vector.</param>
	/// <returns>The sum of the two vectors.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="vector1" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="vector2" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///       The numbers of elements of the two vectors do not match.
	///       </exception>
	public static VectorD operator +(VectorD vector1, VectorD vector2)
	{
		if (vector1 == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9148));
		}
		if (vector2 == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9163));
		}
		if (vector1.NumberOfElements != vector2.NumberOfElements)
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9178));
		}
		VectorD vectorD = new VectorD(vector1.NumberOfElements);
		for (int i = 0; i < vectorD.NumberOfElements; i++)
		{
			vectorD[i] = vector1[i] + vector2[i];
		}
		return vectorD;
	}

	/// <summary>
	///       Adds two vectors.
	///       </summary>
	/// <param name="vector1">The first vector.</param>
	/// <param name="vector2">The second vector.</param>
	/// <returns>The sum of the two vectors.</returns>
	public static VectorD Add(VectorD vector1, VectorD vector2)
	{
		return vector1 + vector2;
	}

	/// <summary>
	///       Subtracts a vector from a vector.
	///       </summary>
	/// <param name="minuend">The first vector (minuend).</param>
	/// <param name="subtrahend">The second vector (subtrahend).</param>
	/// <returns>The difference of the two vectors.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="minuend" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="subtrahend" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///       The numbers of elements of the two vectors do not match.
	///       </exception>
	public static VectorD operator -(VectorD minuend, VectorD subtrahend)
	{
		if (minuend == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7882));
		}
		if (subtrahend == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7897));
		}
		if (minuend.NumberOfElements != subtrahend.NumberOfElements)
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9178));
		}
		VectorD vectorD = new VectorD(minuend.NumberOfElements);
		for (int i = 0; i < vectorD.NumberOfElements; i++)
		{
			vectorD[i] = minuend[i] - subtrahend[i];
		}
		return vectorD;
	}

	/// <summary>
	///       Subtracts a vector from a vector.
	///       </summary>
	/// <param name="minuend">The first vector (minuend).</param>
	/// <param name="subtrahend">The second vector (subtrahend).</param>
	/// <returns>The difference of the two vectors.</returns>
	public static VectorD Subtract(VectorD minuend, VectorD subtrahend)
	{
		return minuend - subtrahend;
	}

	/// <summary>
	///       Multiplies a vector by a scalar.
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <param name="scalar">The scalar.</param>
	/// <returns>The vector with each element multiplied by scalar.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="vector" /> is <see langword="null" />.
	///       </exception>
	public static VectorD operator *(VectorD vector, double scalar)
	{
		if (vector == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8067));
		}
		VectorD vectorD = new VectorD(vector.NumberOfElements);
		for (int i = 0; i < vectorD.NumberOfElements; i++)
		{
			vectorD[i] = vector[i] * scalar;
		}
		return vectorD;
	}

	/// <summary>
	///       Multiplies a vector by a scalar.
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <param name="scalar">The scalar.</param>
	/// <returns>
	///       The vector with each element multiplied by <paramref name="scalar" />.
	///       </returns>
	public static VectorD operator *(double scalar, VectorD vector)
	{
		return vector * scalar;
	}

	/// <summary>
	///       Multiplies a vector by a scalar.
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <param name="scalar">The scalar.</param>
	/// <returns>
	///       The vector with each element multiplied by <paramref name="scalar" />.
	///       </returns>
	public static VectorD Multiply(double scalar, VectorD vector)
	{
		return vector * scalar;
	}

	/// <summary>
	///       Multiplies the components of two vectors by each other.
	///       </summary>
	/// <param name="vector1">The first vector.</param>
	/// <param name="vector2">The second vector.</param>
	/// <returns>The element-wise product of the two vectors.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="vector1" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="vector2" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///       The numbers of elements of the two vectors do not match.
	///       </exception>
	public static VectorD operator *(VectorD vector1, VectorD vector2)
	{
		if (vector1 == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9148));
		}
		if (vector2 == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9163));
		}
		if (vector1.NumberOfElements != vector2.NumberOfElements)
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9178));
		}
		VectorD vectorD = new VectorD(vector1.NumberOfElements);
		for (int i = 0; i < vectorD.NumberOfElements; i++)
		{
			vectorD[i] = vector1[i] * vector2[i];
		}
		return vectorD;
	}

	/// <summary>
	///       Multiplies the components of two vectors by each other.
	///       </summary>
	/// <param name="vector1">The first vector.</param>
	/// <param name="vector2">The second vector.</param>
	/// <returns>The element-wise product of the two vectors.</returns>
	public static VectorD Multiply(VectorD vector1, VectorD vector2)
	{
		return vector1 * vector2;
	}

	/// <summary>
	///       Divides a vector by a scalar.
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <param name="scalar">The scalar.</param>
	/// <returns>
	///       The vector with each element divided by <paramref name="scalar" />.
	///       </returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="vector" /> is <see langword="null" />.
	///       </exception>
	public static VectorD operator /(VectorD vector, double scalar)
	{
		if (vector == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8067));
		}
		double num = 1.0 / scalar;
		VectorD vectorD = new VectorD(vector.NumberOfElements);
		for (int i = 0; i < vectorD.NumberOfElements; i++)
		{
			vectorD[i] = vector[i] * num;
		}
		return vectorD;
	}

	/// <summary>
	///       Divides a vector by a scalar.
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <param name="scalar">The scalar.</param>
	/// <returns>
	///       The vector with each element divided by <paramref name="scalar" />.
	///       </returns>
	public static VectorD Divide(VectorD vector, double scalar)
	{
		return vector / scalar;
	}

	/// <summary>
	///       Divides the elements of a vector by the elements of another 
	///       vector.
	///       </summary>
	/// <param name="dividend">The first vector (dividend).</param>
	/// <param name="divisor">The second vector (divisor).</param>
	/// <returns>The element-wise product of the two vectors.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="dividend" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="divisor" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///       The numbers of elements of the two vectors do not match.
	///       </exception>
	public static VectorD operator /(VectorD dividend, VectorD divisor)
	{
		if (dividend == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9293));
		}
		if (divisor == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9310));
		}
		if (dividend.NumberOfElements != divisor.NumberOfElements)
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9178));
		}
		VectorD vectorD = new VectorD(dividend.NumberOfElements);
		for (int i = 0; i < vectorD.NumberOfElements; i++)
		{
			vectorD[i] = dividend[i] / divisor[i];
		}
		return vectorD;
	}

	/// <summary>
	///       Divides the elements of a vector by the elements of another 
	///       vector.
	///       </summary>
	/// <param name="dividend">The first vector (dividend).</param>
	/// <param name="divisor">The second vector (divisor).</param>
	/// <returns>The element-wise division of the two vectors.</returns>
	public static VectorD Divide(VectorD dividend, VectorD divisor)
	{
		return dividend / divisor;
	}

	/// <summary>
	///       Tests if two vectors are equal.
	///       </summary>
	/// <param name="vector1">The first vector.</param>
	/// <param name="vector2">The second vector.</param>
	/// <returns>
	///   <see langword="true" /> if the vectors are equal; otherwise <see langword="false" />.
	///       </returns>
	public static bool operator ==(VectorD vector1, VectorD vector2)
	{
		if (object.ReferenceEquals(vector1, vector2))
		{
			return true;
		}
		if (object.ReferenceEquals(vector1, null) || object.ReferenceEquals(vector2, null))
		{
			return false;
		}
		if (vector1.NumberOfElements != vector2.NumberOfElements)
		{
			return false;
		}
		for (int i = 0; i < vector1.NumberOfElements; i++)
		{
			if (vector1[i] != vector2[i])
			{
				return false;
			}
		}
		return true;
	}

	/// <summary>
	///       Tests if two vectors are not equal.
	///       </summary>
	/// <param name="vector1">The first vector.</param>
	/// <param name="vector2">The second vector.</param>
	/// <returns>
	///   <see langword="true" /> if the vectors are different; otherwise <see langword="false" />.
	///       </returns>
	public static bool operator !=(VectorD vector1, VectorD vector2)
	{
		return !(vector1 == vector2);
	}

	/// <summary>
	///       Tests if each element of a vector is greater than the corresponding 
	///       element of another vector.
	///       </summary>
	/// <param name="vector1">The first vector.</param>
	/// <param name="vector2">The second vector.</param>
	/// <returns>
	///       True if each element of <paramref name="vector1" /> is greater than its 
	///       counterpart in <paramref name="vector2" />.
	///       </returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="vector1" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="vector2" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///       The numbers of elements of the two vectors do not match.
	///       </exception>
	public static bool operator >(VectorD vector1, VectorD vector2)
	{
		if (vector1 == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9148));
		}
		if (vector2 == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9163));
		}
		if (vector1.NumberOfElements != vector2.NumberOfElements)
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9178));
		}
		VectorD vectorD = new VectorD(vector1.NumberOfElements);
		for (int i = 0; i < vectorD.NumberOfElements; i++)
		{
			if (vector1[i] <= vector2[i])
			{
				return false;
			}
		}
		return true;
	}

	/// <summary>
	///       Tests if each element of a vector is greater or equal than the 
	///       corresponding element of another vector.
	///       </summary>
	/// <param name="vector1">The first vector.</param>
	/// <param name="vector2">The second vector.</param>
	/// <returns>
	///       True if each element of <paramref name="vector1" /> is greater or equal than
	///       its counterpart in <paramref name="vector2" />.
	///       </returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="vector1" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="vector2" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///       The numbers of elements of the two vectors do not match.
	///       </exception>
	public static bool operator >=(VectorD vector1, VectorD vector2)
	{
		if (vector1 == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9148));
		}
		if (vector2 == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9163));
		}
		if (vector1.NumberOfElements != vector2.NumberOfElements)
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9178));
		}
		VectorD vectorD = new VectorD(vector1.NumberOfElements);
		for (int i = 0; i < vectorD.NumberOfElements; i++)
		{
			if (vector1[i] < vector2[i])
			{
				return false;
			}
		}
		return true;
	}

	/// <summary>
	///       Tests if each element of a vector is less than the corresponding 
	///       element of another vector.
	///       </summary>
	/// <param name="vector1">The first vector.</param>
	/// <param name="vector2">The second vector.</param>
	/// <returns>
	///       True if each element of <paramref name="vector1" /> is less than its 
	///       counterpart in <paramref name="vector2" />.
	///       </returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="vector1" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="vector2" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///       The numbers of elements of the two vectors do not match.
	///       </exception>
	public static bool operator <(VectorD vector1, VectorD vector2)
	{
		if (vector1 == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9148));
		}
		if (vector2 == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9163));
		}
		if (vector1.NumberOfElements != vector2.NumberOfElements)
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9178));
		}
		VectorD vectorD = new VectorD(vector1.NumberOfElements);
		for (int i = 0; i < vectorD.NumberOfElements; i++)
		{
			if (vector1[i] >= vector2[i])
			{
				return false;
			}
		}
		return true;
	}

	/// <summary>
	///       Tests if each element of a vector is less or equal than the 
	///       corresponding element of another vector.
	///       </summary>
	/// <param name="vector1">The first vector.</param>
	/// <param name="vector2">The second vector.</param>
	/// <returns>
	///       True if each element of <paramref name="vector1" /> is less or equal than
	///       its counterpart in <paramref name="vector2" />.
	///       </returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="vector1" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="vector2" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///       The numbers of elements of the two vectors do not match.
	///       </exception>
	public static bool operator <=(VectorD vector1, VectorD vector2)
	{
		if (vector1 == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9148));
		}
		if (vector2 == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9163));
		}
		if (vector1.NumberOfElements != vector2.NumberOfElements)
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9178));
		}
		VectorD vectorD = new VectorD(vector1.NumberOfElements);
		for (int i = 0; i < vectorD.NumberOfElements; i++)
		{
			if (vector1[i] > vector2[i])
			{
				return false;
			}
		}
		return true;
	}

	/// <summary>
	///       Converts a vector to an array of double values.
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <returns>The array.</returns>
	public static explicit operator double[](VectorD vector)
	{
		if (vector == null)
		{
			return null;
		}
		double[] array = new double[vector.NumberOfElements];
		for (int i = 0; i < vector.NumberOfElements; i++)
		{
			array[i] = vector[i];
		}
		return array;
	}

	/// <summary>
	///       Converts this vector to an array of double values.
	///       </summary>
	/// <returns>The array.</returns>
	public double[] ToArray()
	{
		return (double[])this;
	}

	/// <summary>
	///       Converts a vector to a list of double values.
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <returns>The list with of double values.</returns>
	public static explicit operator List<double>(VectorD vector)
	{
		if (vector == null)
		{
			return null;
		}
		List<double> list = new List<double>(vector.NumberOfElements);
		for (int i = 0; i < vector.NumberOfElements; i++)
		{
			list.Add(vector[i]);
		}
		return list;
	}

	/// <summary>
	///       Converts this vector to a list of double values.
	///       </summary>
	/// <returns>The list of double values.</returns>
	public List<double> ToList()
	{
		return (List<double>)this;
	}

	/// <summary>
	///       Performs an explicit conversion from <see cref="T:DigitalRune.Mathematics.Algebra.VectorD" /> to <see cref="T:DigitalRune.Mathematics.Algebra.Vector2D" />.
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <returns>The result of the conversion.</returns>
	/// <exception cref="T:System.InvalidCastException">This vector has more than 3 elements.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="vector" /> is <see langword="null" />.
	///       </exception>
	public static explicit operator Vector2D(VectorD vector)
	{
		if (vector == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8067));
		}
		if (vector.NumberOfElements != 2)
		{
			throw new InvalidCastException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9325));
		}
		return new Vector2D(vector[0], vector[1]);
	}

	/// <summary>
	///       Converts this <see cref="T:DigitalRune.Mathematics.Algebra.VectorD" /> to <see cref="T:DigitalRune.Mathematics.Algebra.Vector2D" />.
	///       </summary>
	/// <returns>The result of the conversion.</returns>
	/// <exception cref="T:System.InvalidCastException">This vector has more than 2 elements.</exception>
	public Vector2D ToVector2D()
	{
		return (Vector2D)this;
	}

	/// <summary>
	///       Performs an explicit conversion from <see cref="T:DigitalRune.Mathematics.Algebra.VectorD" /> to <see cref="T:DigitalRune.Mathematics.Algebra.Vector3D" />.
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <returns>The result of the conversion.</returns>
	/// <exception cref="T:System.InvalidCastException">This vector has more than 3 elements.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="vector" /> is <see langword="null" />.
	///       </exception>
	public static explicit operator Vector3D(VectorD vector)
	{
		if (vector == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8067));
		}
		if (vector.NumberOfElements != 3)
		{
			throw new InvalidCastException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9325));
		}
		return new Vector3D(vector[0], vector[1], vector[2]);
	}

	/// <summary>
	///       Converts this <see cref="T:DigitalRune.Mathematics.Algebra.VectorD" /> to <see cref="T:DigitalRune.Mathematics.Algebra.Vector3D" />.
	///       </summary>
	/// <returns>The result of the conversion.</returns>
	/// <exception cref="T:System.InvalidCastException">This vector has more than 3 elements.</exception>
	public Vector3D ToVector3D()
	{
		return (Vector3D)this;
	}

	/// <summary>
	///       Performs an explicit conversion from <see cref="T:DigitalRune.Mathematics.Algebra.VectorD" /> to <see cref="T:DigitalRune.Mathematics.Algebra.Vector4D" />.
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <returns>The result of the conversion.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="vector" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.InvalidCastException">
	///       This vector has more than 4 elements.
	///       </exception>
	public static explicit operator Vector4D(VectorD vector)
	{
		if (vector == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8067));
		}
		if (vector.NumberOfElements != 4)
		{
			throw new InvalidCastException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9325));
		}
		return new Vector4D(vector[0], vector[1], vector[2], vector[3]);
	}

	/// <summary>
	///       Converts this <see cref="T:DigitalRune.Mathematics.Algebra.VectorD" /> to <see cref="T:DigitalRune.Mathematics.Algebra.Vector4D" />.
	///       </summary>
	/// <returns>The result of the conversion.</returns>
	/// <exception cref="T:System.InvalidCastException">This vector has more than 4 elements.</exception>
	public Vector4D ToVector4D()
	{
		return (Vector4D)this;
	}

	/// <summary>
	///       Performs an explicit conversion from <see cref="T:DigitalRune.Mathematics.Algebra.VectorD" /> to <see cref="T:DigitalRune.Mathematics.Algebra.VectorF" />.
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <returns>The result of the conversion.</returns>
	public static explicit operator VectorF(VectorD vector)
	{
		if (vector == null)
		{
			return null;
		}
		VectorF vectorF = new VectorF(vector.NumberOfElements);
		for (int i = 0; i < vector.NumberOfElements; i++)
		{
			vectorF[i] = (float)vector[i];
		}
		return vectorF;
	}

	/// <summary>
	///       Converts this <see cref="T:DigitalRune.Mathematics.Algebra.VectorD" /> to <see cref="T:DigitalRune.Mathematics.Algebra.VectorF" />.
	///       </summary>
	/// <returns>The result of the conversion</returns>
	public VectorF ToVectorF()
	{
		return (VectorF)this;
	}

	/// <summary>
	///       Performs an explicit conversion from <see cref="T:DigitalRune.Mathematics.Algebra.VectorD" /> to <see cref="T:DigitalRune.Mathematics.Algebra.MatrixD" />.
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <returns>The result of the conversion.</returns>
	public static explicit operator MatrixD(VectorD vector)
	{
		if (vector == null)
		{
			return null;
		}
		MatrixD matrixD = new MatrixD(vector.NumberOfElements, 1);
		for (int i = 0; i < vector.NumberOfElements; i++)
		{
			matrixD[i, 0] = vector[i];
		}
		return matrixD;
	}

	/// <summary>
	///       Converts this <see cref="T:DigitalRune.Mathematics.Algebra.VectorD" /> to <see cref="T:DigitalRune.Mathematics.Algebra.MatrixD" />.
	///       </summary>
	/// <returns>The result of the conversion</returns>
	public MatrixD ToMatrixD()
	{
		return (MatrixD)this;
	}

	/// <summary>
	///       Sets each vector element to its absolute value.
	///       </summary>
	public void Absolute()
	{
		for (int i = 0; i < NumberOfElements; i++)
		{
			_v[i] = Math.Abs(_v[i]);
		}
	}

	/// <summary>
	///       Clamps the vector elements to the range [min, max].
	///       </summary>
	/// <param name="min">The min limit.</param>
	/// <param name="max">The max limit.</param>
	public void Clamp(double min, double max)
	{
		for (int i = 0; i < NumberOfElements; i++)
		{
			_v[i] = MathHelper.Clamp(_v[i], min, max);
		}
	}

	/// <summary>
	///       Clamps near-zero vector elements to zero.
	///       </summary>
	public void ClampToZero()
	{
		for (int i = 0; i < NumberOfElements; i++)
		{
			_v[i] = Numeric.ClampToZero(_v[i]);
		}
	}

	/// <summary>
	///       Clamps near-zero vector elements to zero.
	///       </summary>
	/// <param name="epsilon">The tolerance value.</param>
	public void ClampToZero(double epsilon)
	{
		for (int i = 0; i < NumberOfElements; i++)
		{
			_v[i] = Numeric.ClampToZero(_v[i], epsilon);
		}
	}

	/// <summary>
	///       Clones this instance.
	///       </summary>
	/// <returns>A copy of this instance.</returns>
	public VectorD Clone()
	{
		return new VectorD(_v);
	}

	/// <summary>
	///       Gets a subvector of this vector.
	///       </summary>
	/// <param name="startIndex">The index of the first element of the subvector.</param>
	/// <param name="subvectorLength">The length of the subvector.</param>
	/// <returns>The subvector.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///       The <paramref name="startIndex" /> is negative or equal to or greater than the 
	///       <see cref="P:DigitalRune.Mathematics.Algebra.VectorD.NumberOfElements" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///       The <paramref name="subvectorLength" /> is less than 1.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///       The <paramref name="startIndex" /> + <paramref name="subvectorLength" /> exceeds the 
	///       <see cref="P:DigitalRune.Mathematics.Algebra.VectorD.NumberOfElements" />.
	///       </exception>
	public VectorD GetSubvector(int startIndex, int subvectorLength)
	{
		if (startIndex < 0 || startIndex >= NumberOfElements)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9402), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9423));
		}
		if (subvectorLength <= 0)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9536), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9567));
		}
		if (startIndex + subvectorLength > NumberOfElements)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9536), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9654));
		}
		VectorD vectorD = new VectorD(subvectorLength);
		for (int i = startIndex; i < startIndex + subvectorLength; i++)
		{
			vectorD[i - startIndex] = _v[i];
		}
		return vectorD;
	}

	/// <summary>
	///       Normalizes the vector.
	///       </summary>
	/// <exception cref="T:System.DivideByZeroException">
	///       The length of this vector is zero. The vector cannot be normalized.
	///       </exception>
	public void Normalize()
	{
		double length = Length;
		if (Numeric.IsZero(length))
		{
			throw new DivideByZeroException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(874));
		}
		double num = 1.0 / length;
		for (int i = 0; i < NumberOfElements; i++)
		{
			_v[i] *= num;
		}
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
		double lengthSquared = LengthSquared;
		if (Numeric.IsZero(lengthSquared, Numeric.EpsilonDSquared))
		{
			return false;
		}
		double num = Math.Sqrt(lengthSquared);
		double num2 = 1.0 / num;
		for (int i = 0; i < NumberOfElements; i++)
		{
			_v[i] *= num2;
		}
		return true;
	}

	/// <summary>
	///       Sets this vector to its projection onto the axis given by the target vector.
	///       </summary>
	/// <param name="target">The target vector.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="target" /> is <see langword="null" />.
	///       </exception>
	public void ProjectTo(VectorD target)
	{
		if (target == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5541));
		}
		Set(Dot(this, target) / target.LengthSquared * target);
	}

	/// <summary>
	///       Sets this instance to a copy of the specified vector.
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="vector" /> must have at least <see cref="P:DigitalRune.Mathematics.Algebra.VectorD.NumberOfElements" /> elements.
	///       </exception>
	/// <exception cref="T:System.NullReferenceException">
	///   <paramref name="vector" /> must not be <see langword="null" />.
	///       </exception>
	public void Set(VectorD vector)
	{
		for (int i = 0; i < NumberOfElements; i++)
		{
			_v[i] = vector[i];
		}
	}

	/// <summary>
	///       Sets all vector elements to the specified value.
	///       </summary>
	/// <param name="value">The value.</param>
	public void Set(double value)
	{
		for (int i = 0; i < NumberOfElements; i++)
		{
			_v[i] = value;
		}
	}

	/// <summary>
	///       Sets the vector elements to the values of the array.
	///       </summary>
	/// <param name="elements">The elements array.</param>
	/// <exception cref="T:System.IndexOutOfRangeException">
	///   <paramref name="elements" /> must have at least <see cref="P:DigitalRune.Mathematics.Algebra.VectorD.NumberOfElements" /> elements.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="elements" /> is <see langword="null" />.
	///       </exception>
	public void Set(double[] elements)
	{
		if (elements == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8650));
		}
		for (int i = 0; i < NumberOfElements; i++)
		{
			_v[i] = elements[i];
		}
	}

	/// <summary>
	///       Sets the vector elements to the values of the list.
	///       </summary>
	/// <param name="elements">The elements list.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="elements" /> must have at least <see cref="P:DigitalRune.Mathematics.Algebra.VectorD.NumberOfElements" /> elements.
	///       </exception>
	/// <exception cref="T:System.NullReferenceException">
	///   <paramref name="elements" /> must not be <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="elements" /> is <see langword="null" />.
	///       </exception>
	public void Set(IList<double> elements)
	{
		if (elements == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8650));
		}
		for (int i = 0; i < NumberOfElements; i++)
		{
			_v[i] = elements[i];
		}
	}

	/// <summary>
	///       Sets a subvector of this instance.
	///       </summary>
	/// <param name="startIndex">The start index.</param>
	/// <param name="subvector">The subvector.</param>
	/// <exception cref="T:System.IndexOutOfRangeException">
	///       The <paramref name="startIndex" /> or the number of elements of the subvector is to high, so 
	///       that the subvector does not fit into this vector.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="subvector" /> is <see langword="null" />.
	///       </exception>
	public void SetSubvector(int startIndex, VectorD subvector)
	{
		if (subvector == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9792));
		}
		for (int i = 0; i < subvector.NumberOfElements; i++)
		{
			_v[i + startIndex] = subvector[i];
		}
	}

	/// <summary>
	///       Returns a vector with the absolute values of the elements of the given vector.
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <returns>A vector with the absolute values of the elements of the given vector.</returns>
	public static VectorD Absolute(VectorD vector)
	{
		if (vector == null)
		{
			return null;
		}
		VectorD vectorD = new VectorD(vector.NumberOfElements);
		for (int i = 0; i < vectorD.NumberOfElements; i++)
		{
			vectorD[i] = Math.Abs(vector[i]);
		}
		return vectorD;
	}

	/// <summary>
	///       Determines whether two vectors are equal (regarding the 
	///       tolerance <see cref="P:DigitalRune.Mathematics.Numeric.EpsilonD" />).
	///       </summary>
	/// <param name="vector1">The first vector.</param>
	/// <param name="vector2">The second vector.</param>
	/// <returns>
	///   <see langword="true" /> if the vectors are equal (within the tolerance 
	///       <see cref="P:DigitalRune.Mathematics.Numeric.EpsilonD" />); otherwise, <see langword="false" />.
	///       </returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="vector1" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="vector2" /> is <see langword="null" />.
	///       </exception>
	public static bool AreNumericallyEqual(VectorD vector1, VectorD vector2)
	{
		if (vector1 == null && vector2 == null)
		{
			return true;
		}
		if (vector1 == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9148));
		}
		if (vector2 == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9163));
		}
		if (vector1.NumberOfElements != vector2.NumberOfElements)
		{
			return false;
		}
		for (int i = 0; i < vector1.NumberOfElements; i++)
		{
			if (!Numeric.AreEqual(vector1[i], vector2[i]))
			{
				return false;
			}
		}
		return true;
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
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="vector1" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="vector2" /> is <see langword="null" />.
	///       </exception>
	public static bool AreNumericallyEqual(VectorD vector1, VectorD vector2, double epsilon)
	{
		if (vector1 == null && vector2 == null)
		{
			return true;
		}
		if (vector1 == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9148));
		}
		if (vector2 == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9163));
		}
		if (vector1.NumberOfElements != vector2.NumberOfElements)
		{
			return false;
		}
		for (int i = 0; i < vector1.NumberOfElements; i++)
		{
			if (!Numeric.AreEqual(vector1[i], vector2[i], epsilon))
			{
				return false;
			}
		}
		return true;
	}

	/// <summary>
	///       Returns a vector with the vector elements clamped to the range [min, max].
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <param name="min">The min limit.</param>
	/// <param name="max">The max limit.</param>
	/// <returns>A vector with clamped elements.</returns>
	public static VectorD Clamp(VectorD vector, double min, double max)
	{
		if (vector == null)
		{
			return null;
		}
		VectorD vectorD = new VectorD(vector.NumberOfElements);
		for (int i = 0; i < vector.NumberOfElements; i++)
		{
			vectorD[i] = MathHelper.Clamp(vector[i], min, max);
		}
		return vectorD;
	}

	/// <summary>
	///       Returns a vector with near-zero vector elements clamped to 0.
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <returns>
	///       The vector with small elements clamped to zero.
	///       </returns>
	public static VectorD ClampToZero(VectorD vector)
	{
		if (vector == null)
		{
			return null;
		}
		for (int i = 0; i < vector.NumberOfElements; i++)
		{
			vector[i] = Numeric.ClampToZero(vector[i]);
		}
		return vector;
	}

	/// <summary>
	///       Returns a vector with near-zero vector elements clamped to 0.
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <param name="epsilon">The tolerance value.</param>
	/// <returns>
	///       The vector with small elements clamped to zero.
	///       </returns>
	public static VectorD ClampToZero(VectorD vector, double epsilon)
	{
		if (vector == null)
		{
			return null;
		}
		for (int i = 0; i < vector.NumberOfElements; i++)
		{
			vector[i] = Numeric.ClampToZero(vector[i], epsilon);
		}
		return vector;
	}

	/// <summary>
	///       Returns the dot product of two vectors.
	///       </summary>
	/// <param name="vector1">The first vector.</param>
	/// <param name="vector2">The second vector.</param>
	/// <returns>The dot product.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="vector1" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="vector2" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///       The numbers of elements of the two vectors do not match.
	///       </exception>
	public static double Dot(VectorD vector1, VectorD vector2)
	{
		if (vector1 == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9148));
		}
		if (vector2 == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9163));
		}
		if (vector1.NumberOfElements != vector2.NumberOfElements)
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9178));
		}
		double num = 0.0;
		for (int i = 0; i < vector1.NumberOfElements; i++)
		{
			num += vector1[i] * vector2[i];
		}
		return num;
	}

	/// <summary>
	///       Returns a vector that contains the lowest value from each matching pair of elements.
	///       </summary>
	/// <param name="vector1">The first vector.</param>
	/// <param name="vector2">The second vector.</param>
	/// <returns>The minimized vector.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="vector1" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="vector2" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///       The numbers of elements of the two vectors do not match.
	///       </exception>
	public static VectorD Min(VectorD vector1, VectorD vector2)
	{
		if (vector1 == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9148));
		}
		if (vector2 == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9163));
		}
		if (vector1.NumberOfElements != vector2.NumberOfElements)
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9178));
		}
		VectorD vectorD = new VectorD(vector1.NumberOfElements);
		for (int i = 0; i < vectorD.NumberOfElements; i++)
		{
			vectorD[i] = Math.Min(vector1[i], vector2[i]);
		}
		return vectorD;
	}

	/// <summary>
	///       Returns a vector that contains the highest value from each matching pair of components.
	///       </summary>
	/// <param name="vector1">The first vector.</param>
	/// <param name="vector2">The second vector.</param>
	/// <returns>The maximized vector.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="vector1" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="vector2" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///       The numbers of elements of the two vectors do not match.
	///       </exception>
	public static VectorD Max(VectorD vector1, VectorD vector2)
	{
		if (vector1 == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9148));
		}
		if (vector2 == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9163));
		}
		if (vector1.NumberOfElements != vector2.NumberOfElements)
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9178));
		}
		VectorD vectorD = new VectorD(vector1.NumberOfElements);
		for (int i = 0; i < vectorD.NumberOfElements; i++)
		{
			vectorD[i] = Math.Max(vector1[i], vector2[i]);
		}
		return vectorD;
	}

	/// <summary>
	///       Projects a vector onto an axis given by the target vector.
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <param name="target">The target vector.</param>
	/// <returns>
	///       The projection of <paramref name="vector" /> onto <paramref name="target" />.
	///       </returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="vector" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="target" /> is <see langword="null" />.
	///       </exception>
	public static VectorD ProjectTo(VectorD vector, VectorD target)
	{
		if (vector == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8067));
		}
		if (target == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5541));
		}
		return Dot(vector, target) / target.LengthSquared * target;
	}
}
