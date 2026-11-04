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
///       Defines an n-dimensional vector (single-precision).
///       </summary>
[Serializable]
[TypeConverter(typeof(ExpandableObjectConverter))]
public class VectorF : ICloneable, IEquatable<VectorF>, ISerializable, IXmlSerializable
{
	private float[] _v;

	/// <summary>
	///       Gets or sets the internal array that is used to store the vector values.
	///       </summary>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="value" /> is <see langword="null" />.
	///       </exception>
	public float[] InternalArray
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
	public float this[int index]
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
	///       Gets a value indicating whether a component of the vector is <see cref="F:System.Single.NaN" />.
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
			return (float)Math.Sqrt(LengthSquared);
		}
		set
		{
			float length = Length;
			if (Numeric.IsZero(length))
			{
				throw new MathematicsException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(748));
			}
			float num = value / length;
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
	public float LengthSquared => Dot(this, this);

	/// <summary>
	///       Returns the normalized vector.
	///       </summary>
	/// <exception cref="T:System.DivideByZeroException">
	///       The length of the vector is zero. The quaternion cannot be normalized.
	///       </exception>
	public VectorF Normalized
	{
		get
		{
			VectorF vectorF = Clone();
			vectorF.Normalize();
			return vectorF;
		}
	}

	/// <summary>
	///       Gets the number of elements <i>n</i>.
	///       </summary>
	public int NumberOfElements => _v.Length;

	/// <summary>
	///       Gets the value of the largest element.
	///       </summary>
	public float LargestElement
	{
		get
		{
			float num = _v[0];
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
			float num = _v[0];
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
	public float SmallestElement
	{
		get
		{
			float num = _v[0];
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
			float num = _v[0];
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

	static VectorF()
	{
		TypeInitializer.Initialize(cb8acf96051824377b85f63be63e3c2a3.ce9f9bc9ecd5ed400a711abaa33452c25);
		TypeInitializer.CreateInstance(11, cb8acf96051824377b85f63be63e3c2a3.ccda5e1cdd8faa90b24f6bc28c9af6b28);
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Algebra.VectorF" /> class with 4 vector elements.
	///       </summary>
	public VectorF()
		: this(4)
	{
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Algebra.VectorF" /> class.
	///       </summary>
	/// <param name="numberOfElements">The number of elements.</param>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="numberOfElements" /> must be greater than 0.
	///       </exception>
	public VectorF(int numberOfElements)
	{
		if (numberOfElements <= 0)
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9811), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9904));
		}
		_v = new float[numberOfElements];
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Algebra.VectorF" /> class.
	///       </summary>
	/// <param name="numberOfElements">The number of elements.</param>
	/// <param name="value">The initial value for the vector elements.</param>
	public VectorF(int numberOfElements, float value)
		: this(numberOfElements)
	{
		for (int i = 0; i < numberOfElements; i++)
		{
			_v[i] = value;
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Algebra.VectorF" /> class.
	///       </summary>
	/// <param name="elements">The array with the initial values for the vector elements.</param>
	/// <exception cref="T:System.NullReferenceException">
	///   <paramref name="elements" /> must not be <see langword="null" />.
	///       </exception>
	public VectorF(float[] elements)
		: this(elements.Length)
	{
		for (int i = 0; i < NumberOfElements; i++)
		{
			_v[i] = elements[i];
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Algebra.VectorF" /> class.
	///       </summary>
	/// <param name="elements">The list with the initial values for the vector elements.</param>
	/// <exception cref="T:System.NullReferenceException">
	///   <paramref name="elements" /> must not be <see langword="null" />.
	///       </exception>
	public VectorF(IList<float> elements)
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
		VectorF vectorF = obj as VectorF;
		if (vectorF == null)
		{
			return false;
		}
		return this == vectorF;
	}

	/// <summary>
	///       Indicates whether the current object is equal to another object of the same type.
	///       </summary>
	/// <param name="other">An object to compare with this object.</param>
	/// <returns>
	///   <see langword="true" /> if the current object is equal to the other parameter; otherwise, 
	///       <see langword="false" />.
	///       </returns>
	public bool Equals(VectorF other)
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
		_v = new float[num];
		for (int i = 0; i < num; i++)
		{
			_v[i] = reader.ReadElementContentAsFloat();
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
	public static VectorF operator -(VectorF vector)
	{
		if (vector == null)
		{
			return null;
		}
		VectorF vectorF = vector.Clone();
		for (int i = 0; i < vectorF.NumberOfElements; i++)
		{
			vectorF[i] = 0f - vectorF[i];
		}
		return vectorF;
	}

	/// <summary>
	///       Negates a vector.
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <returns>The negated vector.</returns>
	public static VectorF Negate(VectorF vector)
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
	public static VectorF operator +(VectorF vector1, VectorF vector2)
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
		VectorF vectorF = new VectorF(vector1.NumberOfElements);
		for (int i = 0; i < vectorF.NumberOfElements; i++)
		{
			vectorF[i] = vector1[i] + vector2[i];
		}
		return vectorF;
	}

	/// <summary>
	///       Adds two vectors.
	///       </summary>
	/// <param name="vector1">The first vector.</param>
	/// <param name="vector2">The second vector.</param>
	/// <returns>The sum of the two vectors.</returns>
	public static VectorF Add(VectorF vector1, VectorF vector2)
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
	public static VectorF operator -(VectorF minuend, VectorF subtrahend)
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
		VectorF vectorF = new VectorF(minuend.NumberOfElements);
		for (int i = 0; i < vectorF.NumberOfElements; i++)
		{
			vectorF[i] = minuend[i] - subtrahend[i];
		}
		return vectorF;
	}

	/// <summary>
	///       Subtracts a vector from a vector.
	///       </summary>
	/// <param name="minuend">The first vector (minuend).</param>
	/// <param name="subtrahend">The second vector (subtrahend).</param>
	/// <returns>The difference of the two vectors.</returns>
	public static VectorF Subtract(VectorF minuend, VectorF subtrahend)
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
	public static VectorF operator *(VectorF vector, float scalar)
	{
		if (vector == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8067));
		}
		VectorF vectorF = new VectorF(vector.NumberOfElements);
		for (int i = 0; i < vectorF.NumberOfElements; i++)
		{
			vectorF[i] = vector[i] * scalar;
		}
		return vectorF;
	}

	/// <summary>
	///       Multiplies a vector by a scalar.
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <param name="scalar">The scalar.</param>
	/// <returns>
	///       The vector with each element multiplied by <paramref name="scalar" />.
	///       </returns>
	public static VectorF operator *(float scalar, VectorF vector)
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
	public static VectorF Multiply(float scalar, VectorF vector)
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
	public static VectorF operator *(VectorF vector1, VectorF vector2)
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
		VectorF vectorF = new VectorF(vector1.NumberOfElements);
		for (int i = 0; i < vectorF.NumberOfElements; i++)
		{
			vectorF[i] = vector1[i] * vector2[i];
		}
		return vectorF;
	}

	/// <summary>
	///       Multiplies the components of two vectors by each other.
	///       </summary>
	/// <param name="vector1">The first vector.</param>
	/// <param name="vector2">The second vector.</param>
	/// <returns>The element-wise product of the two vectors.</returns>
	public static VectorF Multiply(VectorF vector1, VectorF vector2)
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
	public static VectorF operator /(VectorF vector, float scalar)
	{
		if (vector == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8067));
		}
		float num = 1f / scalar;
		VectorF vectorF = new VectorF(vector.NumberOfElements);
		for (int i = 0; i < vectorF.NumberOfElements; i++)
		{
			vectorF[i] = vector[i] * num;
		}
		return vectorF;
	}

	/// <summary>
	///       Divides a vector by a scalar.
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <param name="scalar">The scalar.</param>
	/// <returns>
	///       The vector with each element divided by <paramref name="scalar" />.
	///       </returns>
	public static VectorF Divide(VectorF vector, float scalar)
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
	public static VectorF operator /(VectorF dividend, VectorF divisor)
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
		VectorF vectorF = new VectorF(dividend.NumberOfElements);
		for (int i = 0; i < vectorF.NumberOfElements; i++)
		{
			vectorF[i] = dividend[i] / divisor[i];
		}
		return vectorF;
	}

	/// <summary>
	///       Divides the elements of a vector by the elements of another 
	///       vector.
	///       </summary>
	/// <param name="dividend">The first vector (dividend).</param>
	/// <param name="divisor">The second vector (divisor).</param>
	/// <returns>The element-wise division of the two vectors.</returns>
	public static VectorF Divide(VectorF dividend, VectorF divisor)
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
	public static bool operator ==(VectorF vector1, VectorF vector2)
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
	public static bool operator !=(VectorF vector1, VectorF vector2)
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
	public static bool operator >(VectorF vector1, VectorF vector2)
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
		VectorF vectorF = new VectorF(vector1.NumberOfElements);
		for (int i = 0; i < vectorF.NumberOfElements; i++)
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
	public static bool operator >=(VectorF vector1, VectorF vector2)
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
		VectorF vectorF = new VectorF(vector1.NumberOfElements);
		for (int i = 0; i < vectorF.NumberOfElements; i++)
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
	public static bool operator <(VectorF vector1, VectorF vector2)
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
		VectorF vectorF = new VectorF(vector1.NumberOfElements);
		for (int i = 0; i < vectorF.NumberOfElements; i++)
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
	public static bool operator <=(VectorF vector1, VectorF vector2)
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
		VectorF vectorF = new VectorF(vector1.NumberOfElements);
		for (int i = 0; i < vectorF.NumberOfElements; i++)
		{
			if (vector1[i] > vector2[i])
			{
				return false;
			}
		}
		return true;
	}

	/// <summary>
	///       Converts a vector to an array of float values.
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <returns>The array.</returns>
	public static explicit operator float[](VectorF vector)
	{
		if (vector == null)
		{
			return null;
		}
		float[] array = new float[vector.NumberOfElements];
		for (int i = 0; i < vector.NumberOfElements; i++)
		{
			array[i] = vector[i];
		}
		return array;
	}

	/// <summary>
	///       Converts this vector to an array of float values.
	///       </summary>
	/// <returns>The array.</returns>
	public float[] ToArray()
	{
		return (float[])this;
	}

	/// <summary>
	///       Converts a vector to a list of float values.
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <returns>The list with of float values.</returns>
	public static explicit operator List<float>(VectorF vector)
	{
		if (vector == null)
		{
			return null;
		}
		List<float> list = new List<float>(vector.NumberOfElements);
		for (int i = 0; i < vector.NumberOfElements; i++)
		{
			list.Add(vector[i]);
		}
		return list;
	}

	/// <summary>
	///       Converts this vector to a list of float values.
	///       </summary>
	/// <returns>The list of float values.</returns>
	public List<float> ToList()
	{
		return (List<float>)this;
	}

	/// <summary>
	///       Performs an explicit conversion from <see cref="T:DigitalRune.Mathematics.Algebra.VectorF" /> to <see cref="T:DigitalRune.Mathematics.Algebra.Vector2F" />.
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <returns>The result of the conversion.</returns>
	/// <exception cref="T:System.InvalidCastException">This vector has more than 3 elements.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="vector" /> is <see langword="null" />.
	///       </exception>
	public static explicit operator Vector2F(VectorF vector)
	{
		if (vector == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8067));
		}
		if (vector.NumberOfElements != 2)
		{
			throw new InvalidCastException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9325));
		}
		return new Vector2F(vector[0], vector[1]);
	}

	/// <summary>
	///       Converts this <see cref="T:DigitalRune.Mathematics.Algebra.VectorF" /> to <see cref="T:DigitalRune.Mathematics.Algebra.Vector2F" />.
	///       </summary>
	/// <returns>The result of the conversion.</returns>
	/// <exception cref="T:System.InvalidCastException">This vector has more than 2 elements.</exception>
	public Vector2F ToVector2F()
	{
		return (Vector2F)this;
	}

	/// <summary>
	///       Performs an explicit conversion from <see cref="T:DigitalRune.Mathematics.Algebra.VectorF" /> to <see cref="T:DigitalRune.Mathematics.Algebra.Vector3F" />.
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <returns>The result of the conversion.</returns>
	/// <exception cref="T:System.InvalidCastException">This vector has more than 3 elements.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="vector" /> is <see langword="null" />.
	///       </exception>
	public static explicit operator Vector3F(VectorF vector)
	{
		if (vector == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8067));
		}
		if (vector.NumberOfElements != 3)
		{
			throw new InvalidCastException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9325));
		}
		return new Vector3F(vector[0], vector[1], vector[2]);
	}

	/// <summary>
	///       Converts this <see cref="T:DigitalRune.Mathematics.Algebra.VectorF" /> to <see cref="T:DigitalRune.Mathematics.Algebra.Vector3F" />.
	///       </summary>
	/// <returns>The result of the conversion.</returns>
	/// <exception cref="T:System.InvalidCastException">This vector has more than 3 elements.</exception>
	public Vector3F ToVector3F()
	{
		return (Vector3F)this;
	}

	/// <summary>
	///       Performs an explicit conversion from <see cref="T:DigitalRune.Mathematics.Algebra.VectorF" /> to <see cref="T:DigitalRune.Mathematics.Algebra.Vector4F" />.
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <returns>The result of the conversion.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="vector" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.InvalidCastException">This vector has more than 4 elements.</exception>
	public static explicit operator Vector4F(VectorF vector)
	{
		if (vector == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8067));
		}
		if (vector.NumberOfElements != 4)
		{
			throw new InvalidCastException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9325));
		}
		return new Vector4F(vector[0], vector[1], vector[2], vector[3]);
	}

	/// <summary>
	///       Converts this <see cref="T:DigitalRune.Mathematics.Algebra.VectorF" /> to <see cref="T:DigitalRune.Mathematics.Algebra.Vector4F" />.
	///       </summary>
	/// <returns>The result of the conversion.</returns>
	/// <exception cref="T:System.InvalidCastException">This vector has more than 4 elements.</exception>
	public Vector4F ToVector4F()
	{
		return (Vector4F)this;
	}

	/// <summary>
	///       Performs an implicit conversion from <see cref="T:DigitalRune.Mathematics.Algebra.VectorF" /> to <see cref="T:DigitalRune.Mathematics.Algebra.VectorD" />.
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <returns>The result of the conversion.</returns>
	public static implicit operator VectorD(VectorF vector)
	{
		if (vector == null)
		{
			return null;
		}
		VectorD vectorD = new VectorD(vector.NumberOfElements);
		for (int i = 0; i < vector.NumberOfElements; i++)
		{
			vectorD[i] = vector[i];
		}
		return vectorD;
	}

	/// <summary>
	///       Converts this <see cref="T:DigitalRune.Mathematics.Algebra.VectorF" /> to <see cref="T:DigitalRune.Mathematics.Algebra.VectorD" />.
	///       </summary>
	/// <returns>The result of the conversion</returns>
	public VectorD ToVectorD()
	{
		return this;
	}

	/// <summary>
	///       Performs an explicit conversion from <see cref="T:DigitalRune.Mathematics.Algebra.VectorF" /> to <see cref="T:DigitalRune.Mathematics.Algebra.MatrixF" />.
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <returns>The result of the conversion.</returns>
	public static explicit operator MatrixF(VectorF vector)
	{
		if (vector == null)
		{
			return null;
		}
		MatrixF matrixF = new MatrixF(vector.NumberOfElements, 1);
		for (int i = 0; i < vector.NumberOfElements; i++)
		{
			matrixF[i, 0] = vector[i];
		}
		return matrixF;
	}

	/// <summary>
	///       Converts this <see cref="T:DigitalRune.Mathematics.Algebra.VectorF" /> to <see cref="T:DigitalRune.Mathematics.Algebra.MatrixF" />.
	///       </summary>
	/// <returns>The result of the conversion</returns>
	public MatrixF ToMatrixF()
	{
		return (MatrixF)this;
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
	public void Clamp(float min, float max)
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
	public void ClampToZero(float epsilon)
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
	public VectorF Clone()
	{
		return new VectorF(_v);
	}

	/// <summary>
	///       Gets a subvector of this vector.
	///       </summary>
	/// <param name="startIndex">The index of the first element of the subvector.</param>
	/// <param name="subvectorLength">The length of the subvector.</param>
	/// <returns>The subvector.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///       The <paramref name="startIndex" /> is negative or equal to or greater than the 
	///       <see cref="P:DigitalRune.Mathematics.Algebra.VectorF.NumberOfElements" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///       The <paramref name="subvectorLength" /> is less than 1.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///       The <paramref name="startIndex" /> + <paramref name="subvectorLength" /> exceeds the 
	///       <see cref="P:DigitalRune.Mathematics.Algebra.VectorF.NumberOfElements" />.
	///       </exception>
	public VectorF GetSubvector(int startIndex, int subvectorLength)
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
		VectorF vectorF = new VectorF(subvectorLength);
		for (int i = startIndex; i < startIndex + subvectorLength; i++)
		{
			vectorF[i - startIndex] = _v[i];
		}
		return vectorF;
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
		float lengthSquared = LengthSquared;
		if (Numeric.IsZero(lengthSquared, Numeric.EpsilonFSquared))
		{
			return false;
		}
		float num = (float)Math.Sqrt(lengthSquared);
		float num2 = 1f / num;
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
	public void ProjectTo(VectorF target)
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
	///   <paramref name="vector" /> must have at least 
	///       <see cref="P:DigitalRune.Mathematics.Algebra.VectorF.NumberOfElements" /> elements.</exception>
	/// <exception cref="T:System.NullReferenceException">
	///   <paramref name="vector" /> must not be <see langword="null" />.
	///       </exception>
	public void Set(VectorF vector)
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
	public void Set(float value)
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
	///   <paramref name="elements" /> must have at least 
	///       <see cref="P:DigitalRune.Mathematics.Algebra.VectorF.NumberOfElements" /> elements.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="elements" /> is <see langword="null" />.
	///       </exception>
	public void Set(float[] elements)
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
	///   <paramref name="elements" /> must have at least 
	///       <see cref="P:DigitalRune.Mathematics.Algebra.VectorF.NumberOfElements" /> elements.</exception>
	/// <exception cref="T:System.NullReferenceException">
	///   <paramref name="elements" /> must not be <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="elements" /> is <see langword="null" />.
	///       </exception>
	public void Set(IList<float> elements)
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
	public void SetSubvector(int startIndex, VectorF subvector)
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
	public static VectorF Absolute(VectorF vector)
	{
		if (vector == null)
		{
			return null;
		}
		VectorF vectorF = new VectorF(vector.NumberOfElements);
		for (int i = 0; i < vectorF.NumberOfElements; i++)
		{
			vectorF[i] = Math.Abs(vector[i]);
		}
		return vectorF;
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
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="vector1" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="vector2" /> is <see langword="null" />.
	///       </exception>
	public static bool AreNumericallyEqual(VectorF vector1, VectorF vector2)
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
	public static bool AreNumericallyEqual(VectorF vector1, VectorF vector2, float epsilon)
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
	public static VectorF Clamp(VectorF vector, float min, float max)
	{
		if (vector == null)
		{
			return null;
		}
		VectorF vectorF = new VectorF(vector.NumberOfElements);
		for (int i = 0; i < vector.NumberOfElements; i++)
		{
			vectorF[i] = MathHelper.Clamp(vector[i], min, max);
		}
		return vectorF;
	}

	/// <summary>
	///       Returns a vector with near-zero vector elements clamped to 0.
	///       </summary>
	/// <param name="vector">The vector.</param>
	/// <returns>
	///       The vector with small elements clamped to zero.
	///       </returns>
	public static VectorF ClampToZero(VectorF vector)
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
	public static VectorF ClampToZero(VectorF vector, float epsilon)
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
	public static float Dot(VectorF vector1, VectorF vector2)
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
		float num = 0f;
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
	public static VectorF Min(VectorF vector1, VectorF vector2)
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
		VectorF vectorF = new VectorF(vector1.NumberOfElements);
		for (int i = 0; i < vectorF.NumberOfElements; i++)
		{
			vectorF[i] = Math.Min(vector1[i], vector2[i]);
		}
		return vectorF;
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
	public static VectorF Max(VectorF vector1, VectorF vector2)
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
		VectorF vectorF = new VectorF(vector1.NumberOfElements);
		for (int i = 0; i < vectorF.NumberOfElements; i++)
		{
			vectorF[i] = Math.Max(vector1[i], vector2[i]);
		}
		return vectorF;
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
	public static VectorF ProjectTo(VectorF vector, VectorF target)
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
