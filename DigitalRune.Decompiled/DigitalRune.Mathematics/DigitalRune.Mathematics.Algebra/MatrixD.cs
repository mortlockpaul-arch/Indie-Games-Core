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
///       Defines an m x n matrix (double-precision).
///       </summary>
[Serializable]
[TypeConverter(typeof(ExpandableObjectConverter))]
public class MatrixD : ICloneable, IEquatable<MatrixD>, ISerializable, IXmlSerializable
{
	private double[,] _m;

	/// <summary>
	///       Gets or sets the element at the specified index.
	///       </summary>
	/// <param name="index">The index.</param>
	/// <exception cref="T:System.IndexOutOfRangeException">
	///       The <paramref name="index" /> is out of range.
	///       </exception>
	public double this[int index]
	{
		get
		{
			if (index < 0 || index >= NumberOfRows * NumberOfColumns)
			{
				throw new IndexOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7700));
			}
			return _m[index / NumberOfColumns, index % NumberOfColumns];
		}
		set
		{
			if (index < 0 || index >= NumberOfRows * NumberOfColumns)
			{
				throw new IndexOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7700));
			}
			_m[index / NumberOfColumns, index % NumberOfColumns] = value;
		}
	}

	/// <summary>
	///       Gets or sets the element at the specified index.
	///       </summary>
	/// <param name="row">The row index.</param>
	/// <param name="column">The column index.</param>
	/// <exception cref="T:System.IndexOutOfRangeException">
	///       The index [<paramref name="row" />, <paramref name="column" />] is out of range.
	///       </exception>
	public double this[int row, int column]
	{
		get
		{
			return _m[row, column];
		}
		set
		{
			_m[row, column] = value;
		}
	}

	/// <summary>
	///       Returns the determinant of this matrix.
	///       </summary>
	/// <exception cref="T:DigitalRune.Mathematics.MathematicsException">Matrix must be square.</exception>
	public double Determinant
	{
		get
		{
			if (IsSquare)
			{
				return new LUDecompositionD(this).Determinant;
			}
			throw new MathematicsException();
		}
	}

	/// <summary>
	///       Returns the inverse or pseudo-inverse of this matrix..
	///       </summary>
	/// <exception cref="T:DigitalRune.Mathematics.MathematicsException">
	///       The matrix is singular (i.e. it is not invertible).
	///       </exception>
	public MatrixD Inverse
	{
		get
		{
			try
			{
				return SolveLinearEquations(this, CreateIdentity(NumberOfRows, NumberOfRows));
			}
			catch (Exception innerException)
			{
				throw new MathematicsException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5262), innerException);
			}
		}
	}

	/// <summary>
	///       Gets a value indicating whether a component of the vector is <see cref="F:System.Double.NaN" />.
	///       </summary>
	public bool IsNaN
	{
		get
		{
			for (int i = 0; i < NumberOfRows; i++)
			{
				for (int j = 0; j < NumberOfColumns; j++)
				{
					if (Numeric.IsNaN(_m[i, j]))
					{
						return true;
					}
				}
			}
			return false;
		}
	}

	/// <summary>
	///       Gets a value indicating whether this matrix is a square matrix (number of rows is equal to 
	///       number of columns).
	///       </summary>
	public bool IsSquare => NumberOfRows == NumberOfColumns;

	/// <summary>
	///       Gets a value indicating whether this matrix is symmetric.
	///       </summary>
	public bool IsSymmetric
	{
		get
		{
			if (NumberOfColumns != NumberOfRows)
			{
				return false;
			}
			for (int i = 0; i < NumberOfRows; i++)
			{
				for (int j = 0; j < i; j++)
				{
					if (_m[i, j] != _m[j, i])
					{
						return false;
					}
				}
			}
			return true;
		}
	}

	/// <summary>
	///       Gets the one norm of this matrix.
	///       </summary>
	public double Norm1
	{
		get
		{
			double num = 0.0;
			for (int i = 0; i < NumberOfColumns; i++)
			{
				double num2 = 0.0;
				for (int j = 0; j < NumberOfRows; j++)
				{
					num2 += Math.Abs(_m[j, i]);
				}
				num = Math.Max(num, num2);
			}
			return num;
		}
	}

	/// <summary>
	///       Gets the Frobenius norm of this matrix.
	///       </summary>
	public double NormFrobenius
	{
		get
		{
			double num = 0.0;
			for (int i = 0; i < NumberOfRows; i++)
			{
				for (int j = 0; j < NumberOfColumns; j++)
				{
					num = MathHelper.Hypotenuse(num, _m[i, j]);
				}
			}
			return num;
		}
	}

	/// <summary>
	///       Gets the infinity norm of this matrix.
	///       </summary>
	public double NormInfinity
	{
		get
		{
			double num = 0.0;
			for (int i = 0; i < NumberOfRows; i++)
			{
				double num2 = 0.0;
				for (int j = 0; j < NumberOfColumns; j++)
				{
					num2 += Math.Abs(_m[i, j]);
				}
				num = Math.Max(num, num2);
			}
			return num;
		}
	}

	/// <summary>
	///       Gets the number of columns <i>n</i>.
	///       </summary>
	public int NumberOfColumns => _m.GetLength(1);

	/// <summary>
	///       Gets the number of rows <i>m</i>.
	///       </summary>
	public int NumberOfRows => _m.GetLength(0);

	/// <summary>
	///       Gets the matrix trace (the sum of the diagonal elements).
	///       </summary>
	public double Trace
	{
		get
		{
			int num = Math.Min(NumberOfRows, NumberOfColumns);
			double num2 = 0.0;
			for (int i = 0; i < num; i++)
			{
				num2 += _m[i, i];
			}
			return num2;
		}
	}

	/// <summary>
	///       Returns the transposed of this matrix.
	///       </summary>
	/// <returns>The transposed of this matrix.</returns>
	public MatrixD Transposed
	{
		get
		{
			MatrixD matrixD = Clone();
			matrixD.Transpose();
			return matrixD;
		}
	}

	static MatrixD()
	{
		TypeInitializer.Initialize(cb8acf96051824377b85f63be63e3c2a3.ce9f9bc9ecd5ed400a711abaa33452c25);
		TypeInitializer.CreateInstance(11, cb8acf96051824377b85f63be63e3c2a3.ccda5e1cdd8faa90b24f6bc28c9af6b28);
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Algebra.MatrixD" /> class with 4 x 4 matrix elements.
	///       </summary>
	public MatrixD()
		: this(4, 4)
	{
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Algebra.MatrixD" /> class.
	///       </summary>
	/// <param name="numberOfRows">The number of rows.</param>
	/// <param name="numberOfColumns">The number of columns.</param>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="numberOfRows" /> or <paramref name="numberOfColumns" /> is negative or 0.
	///       </exception>
	public MatrixD(int numberOfRows, int numberOfColumns)
	{
		if (numberOfRows <= 0)
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8894), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8979));
		}
		if (numberOfColumns <= 0)
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9004), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(9095));
		}
		_m = new double[numberOfRows, numberOfColumns];
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Algebra.MatrixD" /> class.
	///       Each element is set to <paramref name="value" />.
	///       </summary>
	/// <param name="numberOfRows">The number of rows.</param>
	/// <param name="numberOfColumns">The number of columns.</param>
	/// <param name="value">The initial value for the matrix elements.</param>
	public MatrixD(int numberOfRows, int numberOfColumns, double value)
		: this(numberOfRows, numberOfColumns)
	{
		for (int i = 0; i < NumberOfRows; i++)
		{
			for (int j = 0; j < NumberOfColumns; j++)
			{
				_m[i, j] = value;
			}
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Algebra.MatrixD" /> class.
	///       </summary>
	/// <param name="numberOfRows">The number of rows <i>m</i>.</param>
	/// <param name="numberOfColumns">The number of columns <i>n</i>.</param>
	/// <param name="elements">The array with the initial values for the matrix elements.</param>
	/// <param name="order">The order of the matrix elements in <paramref name="elements" />.</param>
	/// <exception cref="T:System.IndexOutOfRangeException">
	///   <paramref name="elements" /> has less than <i>m</i> x <i>n</i> elements.
	///       </exception>
	/// <exception cref="T:System.NullReferenceException">
	///   <paramref name="elements" /> must not be <see langword="null" />.
	///       </exception>
	public MatrixD(int numberOfRows, int numberOfColumns, double[] elements, MatrixOrder order)
		: this(numberOfRows, numberOfColumns)
	{
		if (order == MatrixOrder.RowMajor)
		{
			for (int i = 0; i < NumberOfRows; i++)
			{
				for (int j = 0; j < NumberOfColumns; j++)
				{
					_m[i, j] = elements[i * NumberOfColumns + j];
				}
			}
			return;
		}
		for (int k = 0; k < NumberOfColumns; k++)
		{
			for (int l = 0; l < NumberOfRows; l++)
			{
				_m[l, k] = elements[k * NumberOfRows + l];
			}
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Algebra.MatrixD" /> class.
	///       </summary>
	/// <param name="numberOfRows">The number of rows <i>m</i>.</param>
	/// <param name="numberOfColumns">The number of columns <i>n</i>.</param>
	/// <param name="elements">The list with the initial values for the matrix elements.</param>
	/// <param name="order">The order of the matrix elements in <paramref name="elements" />.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="elements" /> has less than <i>m</i> x <i>n</i> elements.
	///       </exception>
	/// <exception cref="T:System.NullReferenceException">
	///   <paramref name="elements" /> must not be <see langword="null" />.
	///       </exception>
	public MatrixD(int numberOfRows, int numberOfColumns, IList<double> elements, MatrixOrder order)
		: this(numberOfRows, numberOfColumns)
	{
		if (order == MatrixOrder.RowMajor)
		{
			for (int i = 0; i < NumberOfRows; i++)
			{
				for (int j = 0; j < NumberOfColumns; j++)
				{
					_m[i, j] = elements[i * NumberOfColumns + j];
				}
			}
			return;
		}
		for (int k = 0; k < NumberOfColumns; k++)
		{
			for (int l = 0; l < NumberOfRows; l++)
			{
				_m[l, k] = elements[k * NumberOfRows + l];
			}
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Algebra.MatrixD" /> class.
	///       </summary>
	/// <param name="elements">The array with the initial values for the matrix elements.</param>
	/// <exception cref="T:System.NullReferenceException">
	///   <paramref name="elements" /> must not be <see langword="null" />.
	///       </exception>
	[CLSCompliant(false)]
	public MatrixD(double[,] elements)
	{
		_m = new double[elements.GetLength(0), elements.GetLength(1)];
		for (int i = 0; i < NumberOfRows; i++)
		{
			for (int j = 0; j < NumberOfColumns; j++)
			{
				_m[i, j] = elements[i, j];
			}
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Algebra.MatrixD" /> class.
	///       </summary>
	/// <param name="elements">The array with the initial values for the matrix elements.</param>
	/// <exception cref="T:System.IndexOutOfRangeException">
	///       An array in <paramref name="elements" /> has less elements than the first array in 
	///       <paramref name="elements" />.
	///       </exception>
	/// <exception cref="T:System.NullReferenceException">
	///   <paramref name="elements" /> or the arrays in elements[0] must not be <see langword="null" />.
	///       </exception>
	public MatrixD(double[][] elements)
	{
		_m = new double[elements.Length, elements[0].Length];
		for (int i = 0; i < NumberOfRows; i++)
		{
			for (int j = 0; j < NumberOfColumns; j++)
			{
				_m[i, j] = elements[i][j];
			}
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
		for (int i = 0; i < NumberOfRows; i++)
		{
			for (int j = 0; j < NumberOfColumns; j++)
			{
				num = (num * 397) ^ _m[i, j].GetHashCode();
			}
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
		MatrixD matrixD = obj as MatrixD;
		if (matrixD == null)
		{
			return false;
		}
		return this == matrixD;
	}

	/// <summary>
	///       Indicates whether the current object is equal to another object of the same type.
	///       </summary>
	/// <param name="other">An object to compare with this object.</param>
	/// <returns>
	///   <see langword="true" /> if the current object is equal to the other parameter; otherwise, 
	///       <see langword="false" />.
	///       </returns>
	public bool Equals(MatrixD other)
	{
		return this == other;
	}

	/// <summary>
	///       Returns the string representation of this matrix.
	///       </summary>
	/// <returns>The string representation of this matrix</returns>
	public override string ToString()
	{
		return ToString(CultureInfo.CurrentCulture);
	}

	/// <summary>
	///       Returns the string representation of this matrix using the specified 
	///       culture-specific format information.
	///       </summary>
	/// <param name="provider">
	///       An <see cref="T:System.IFormatProvider" /> that supplies culture-specific 
	///       formatting information
	///       </param>
	/// <returns>The string representation of this matrix</returns>
	public string ToString(IFormatProvider provider)
	{
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < NumberOfRows; i++)
		{
			stringBuilder.Append(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7795));
			for (int j = 0; j < NumberOfColumns; j++)
			{
				stringBuilder.Append(_m[i, j]);
				if (j + 1 < NumberOfColumns)
				{
					stringBuilder.Append(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7798));
				}
			}
			stringBuilder.Append(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7803));
		}
		return string.Format(provider, stringBuilder.ToString());
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
		reader.ReadStartElement(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7808));
		int num = reader.ReadContentAsInt();
		reader.ReadEndElement();
		reader.ReadStartElement(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7817));
		int num2 = reader.ReadContentAsInt();
		reader.ReadEndElement();
		reader.ReadStartElement(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7832));
		_m = new double[num, num2];
		for (int i = 0; i < num; i++)
		{
			for (int j = 0; j < num2; j++)
			{
				_m[i, j] = reader.ReadElementContentAsDouble();
			}
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
		writer.WriteStartElement(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7808));
		writer.WriteValue(NumberOfRows);
		writer.WriteEndElement();
		writer.WriteStartElement(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7817));
		writer.WriteValue(NumberOfColumns);
		writer.WriteEndElement();
		writer.WriteStartElement(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7832));
		for (int i = 0; i < NumberOfRows; i++)
		{
			for (int j = 0; j < NumberOfColumns; j++)
			{
				writer.WriteStartElement(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7849));
				writer.WriteValue(_m[i, j]);
				writer.WriteEndElement();
			}
		}
		writer.WriteEndElement();
	}

	/// <summary>
	///       Negates a matrix.
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <returns>The negated matrix.</returns>
	public static MatrixD operator -(MatrixD matrix)
	{
		if (matrix == null)
		{
			return null;
		}
		MatrixD matrixD = matrix.Clone();
		for (int i = 0; i < matrixD.NumberOfRows; i++)
		{
			for (int j = 0; j < matrixD.NumberOfColumns; j++)
			{
				matrixD[i, j] = 0.0 - matrixD[i, j];
			}
		}
		return matrixD;
	}

	/// <summary>
	///       Negates a matrix.
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <returns>The negated matrix.</returns>
	public static MatrixD Negate(MatrixD matrix)
	{
		return -matrix;
	}

	/// <summary>
	///       Adds two matrices.
	///       </summary>
	/// <param name="matrix1">The first matrix.</param>
	/// <param name="matrix2">The second Matrix.</param>
	/// <returns>The sum of the two matrices.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="matrix1" /> or <paramref name="matrix2" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///       The numbers of rows or columns of the matrices do not match.
	///       </exception>
	public static MatrixD operator +(MatrixD matrix1, MatrixD matrix2)
	{
		if (matrix1 == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7852));
		}
		if (matrix2 == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7867));
		}
		c5461797bbc5e8934b246d0414a6cffb9(matrix1, matrix2);
		MatrixD matrixD = new MatrixD(matrix1.NumberOfRows, matrix1.NumberOfColumns);
		for (int i = 0; i < matrixD.NumberOfRows; i++)
		{
			for (int j = 0; j < matrixD.NumberOfColumns; j++)
			{
				matrixD[i, j] = matrix1[i, j] + matrix2[i, j];
			}
		}
		return matrixD;
	}

	/// <summary>
	///       Adds two matrices.
	///       </summary>
	/// <param name="matrix1">The first matrix.</param>
	/// <param name="matrix2">The second Matrix.</param>
	/// <returns>The sum of the two matrices.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="matrix1" /> or <paramref name="matrix2" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///       The numbers of rows or columns of the matrices do not match.
	///       </exception>
	public static MatrixD Add(MatrixD matrix1, MatrixD matrix2)
	{
		return matrix1 + matrix2;
	}

	/// <summary>
	///       Subtracts two matrices.
	///       </summary>
	/// <param name="minuend">The first matrix (minuend).</param>
	/// <param name="subtrahend">The second matrix (subtrahend).</param>
	/// <returns>The difference of the two matrices.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="minuend" /> or <paramref name="subtrahend" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///       The numbers of rows or columns of the matrices do not match.
	///       </exception>
	public static MatrixD operator -(MatrixD minuend, MatrixD subtrahend)
	{
		if (minuend == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7882));
		}
		if (subtrahend == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7897));
		}
		c5461797bbc5e8934b246d0414a6cffb9(minuend, subtrahend);
		MatrixD matrixD = new MatrixD(minuend.NumberOfRows, subtrahend.NumberOfColumns);
		for (int i = 0; i < matrixD.NumberOfRows; i++)
		{
			for (int j = 0; j < matrixD.NumberOfColumns; j++)
			{
				matrixD[i, j] = minuend[i, j] - subtrahend[i, j];
			}
		}
		return matrixD;
	}

	/// <summary>
	///       Subtracts two matrices.
	///       </summary>
	/// <param name="minuend">The first matrix (minuend).</param>
	/// <param name="subtrahend">The second matrix (subtrahend).</param>
	/// <returns>The difference of the two matrices.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="minuend" /> or <paramref name="subtrahend" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///       The numbers of rows or columns of the matrices do not match.
	///       </exception>
	public static MatrixD Subtract(MatrixD minuend, MatrixD subtrahend)
	{
		return minuend - subtrahend;
	}

	/// <summary>
	///       Multiplies a matrix and a scalar.
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <param name="scalar">The scalar.</param>
	/// <returns>
	///       The matrix with each element multiplied by <paramref name="scalar" />.
	///       </returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="matrix" /> is <see langword="null" />.
	///       </exception>
	public static MatrixD operator *(MatrixD matrix, double scalar)
	{
		if (matrix == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7918));
		}
		MatrixD matrixD = new MatrixD(matrix.NumberOfRows, matrix.NumberOfColumns);
		for (int i = 0; i < matrixD.NumberOfRows; i++)
		{
			for (int j = 0; j < matrixD.NumberOfColumns; j++)
			{
				matrixD[i, j] = matrix[i, j] * scalar;
			}
		}
		return matrixD;
	}

	/// <summary>
	///       Multiplies a matrix by a scalar.
	///       </summary>
	/// <param name="scalar">The scalar.</param>
	/// <param name="matrix">The matrix.</param>
	/// <returns>
	///       The matrix with each element multiplied by <paramref name="scalar" />.
	///       </returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="matrix" /> is <see langword="null" />.
	///       </exception>
	public static MatrixD operator *(double scalar, MatrixD matrix)
	{
		return matrix * scalar;
	}

	/// <summary>
	///       Multiplies a matrix by a scalar.
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <param name="scalar">The scalar.</param>
	/// <returns>
	///       The matrix with each element multiplied by <paramref name="scalar" />.
	///       </returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="matrix" /> is <see langword="null" />.
	///       </exception>
	public static MatrixD Multiply(double scalar, MatrixD matrix)
	{
		return matrix * scalar;
	}

	/// <summary>
	///       Multiplies two matrices.
	///       </summary>
	/// <param name="matrix1">The first matrix.</param>
	/// <param name="matrix2">The second matrix.</param>
	/// <returns>The matrix with the product the two matrices.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="matrix1" /> or <paramref name="matrix2" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///       The matrix dimensions are not suitable for a matrix multiplication.
	///       </exception>
	public static MatrixD operator *(MatrixD matrix1, MatrixD matrix2)
	{
		if (matrix1 == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7852));
		}
		if (matrix2 == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7867));
		}
		if (matrix1.NumberOfColumns != matrix2.NumberOfRows)
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7931));
		}
		MatrixD matrixD = new MatrixD(matrix1.NumberOfRows, matrix2.NumberOfColumns);
		for (int i = 0; i < matrixD.NumberOfRows; i++)
		{
			for (int j = 0; j < matrixD.NumberOfColumns; j++)
			{
				for (int k = 0; k < matrix1.NumberOfColumns; k++)
				{
					matrixD[i, j] += matrix1[i, k] * matrix2[k, j];
				}
			}
		}
		return matrixD;
	}

	/// <summary>
	///       Multiplies two matrices.
	///       </summary>
	/// <param name="matrix1">The first matrix.</param>
	/// <param name="matrix2">The second matrix.</param>
	/// <returns>The matrix with the product the two matrices.</returns>
	public static MatrixD Multiply(MatrixD matrix1, MatrixD matrix2)
	{
		return matrix1 * matrix2;
	}

	/// <summary>
	///       Multiplies a matrix with a column vector.
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <param name="vector">The column vector.</param>
	/// <returns>The resulting column vector.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="matrix" /> or <paramref name="vector" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///       The matrix and vector dimensions are not suitable for multiplication.
	///       </exception>
	public static VectorD operator *(MatrixD matrix, VectorD vector)
	{
		if (matrix == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7918));
		}
		if (vector == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8067));
		}
		if (matrix.NumberOfColumns != vector.NumberOfElements)
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8080));
		}
		VectorD vectorD = new VectorD(matrix.NumberOfRows);
		for (int i = 0; i < vectorD.NumberOfElements; i++)
		{
			for (int j = 0; j < matrix.NumberOfColumns; j++)
			{
				vectorD[i] += matrix[i, j] * vector[j];
			}
		}
		return vectorD;
	}

	/// <summary>
	///       Multiplies a matrix with a column vector.
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <param name="vector">The column vector.</param>
	/// <returns>The resulting column vector.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="matrix" /> or <paramref name="vector" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///       The matrix and vector dimensions are not suitable for multiplication.
	///       </exception>
	public static VectorD Multiply(MatrixD matrix, VectorD vector)
	{
		return matrix * vector;
	}

	/// <summary>
	///       Divides a matrix by a scalar.
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <param name="scalar">The scalar.</param>
	/// <returns>The matrix with each element divided by scalar.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="matrix" /> is <see langword="null" />.
	///       </exception>
	public static MatrixD operator /(MatrixD matrix, double scalar)
	{
		double num = 1.0 / scalar;
		return matrix * num;
	}

	/// <summary>
	///       Divides a matrix by a scalar.
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <param name="scalar">The scalar.</param>
	/// <returns>The matrix with each element divided by scalar.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="matrix" /> is <see langword="null" />.
	///       </exception>
	public static MatrixD Divide(MatrixD matrix, double scalar)
	{
		double num = 1.0 / scalar;
		return matrix * num;
	}

	/// <summary>
	///       Tests if two matrices are equal.
	///       </summary>
	/// <param name="matrix1">The first matrix.</param>
	/// <param name="matrix2">The second matrix.</param>
	/// <returns>
	///   <see langword="true" /> if the matrices are equal; otherwise <see langword="false" />.
	///       </returns>
	public static bool operator ==(MatrixD matrix1, MatrixD matrix2)
	{
		if (object.ReferenceEquals(matrix1, matrix2))
		{
			return true;
		}
		if (object.ReferenceEquals(matrix1, null) || object.ReferenceEquals(matrix2, null))
		{
			return false;
		}
		if (matrix1.NumberOfColumns != matrix2.NumberOfColumns)
		{
			return false;
		}
		if (matrix1.NumberOfRows != matrix2.NumberOfRows)
		{
			return false;
		}
		for (int i = 0; i < matrix1.NumberOfRows; i++)
		{
			for (int j = 0; j < matrix1.NumberOfColumns; j++)
			{
				if (matrix1[i, j] != matrix2[i, j])
				{
					return false;
				}
			}
		}
		return true;
	}

	/// <summary>
	///       Tests if two matrices are not equal.
	///       </summary>
	/// <param name="matrix1">The first matrix.</param>
	/// <param name="matrix2">The second matrix.</param>
	/// <returns>
	///   <see langword="true" /> if the matrices are different; otherwise <see langword="false" />.
	///       </returns>
	public static bool operator !=(MatrixD matrix1, MatrixD matrix2)
	{
		return !(matrix1 == matrix2);
	}

	/// <summary>
	///       Performs an explicit conversion from <see cref="T:DigitalRune.Mathematics.Algebra.MatrixD" /> to a 2-dimensional double array.
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <returns>The result of the conversion.</returns>
	public static explicit operator double[,](MatrixD matrix)
	{
		if (matrix == null)
		{
			return null;
		}
		double[,] array = new double[matrix.NumberOfRows, matrix.NumberOfColumns];
		for (int i = 0; i < matrix.NumberOfRows; i++)
		{
			for (int j = 0; j < matrix.NumberOfColumns; j++)
			{
				array[i, j] = matrix[i, j];
			}
		}
		return array;
	}

	/// <summary>
	///       Converts this <see cref="T:DigitalRune.Mathematics.Algebra.MatrixD" /> to a 2-dimensional double array.
	///       </summary>
	/// <returns>The result of the conversion.</returns>
	public double[,] ToArray2D()
	{
		return (double[,])this;
	}

	/// <summary>
	///       Performs an explicit conversion from <see cref="T:DigitalRune.Mathematics.Algebra.MatrixD" /> to jagged double array.
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <returns>The result of the conversion.</returns>
	public static explicit operator double[][](MatrixD matrix)
	{
		if (matrix == null)
		{
			return null;
		}
		double[][] array = new double[matrix.NumberOfRows][];
		for (int i = 0; i < matrix.NumberOfRows; i++)
		{
			array[i] = new double[matrix.NumberOfColumns];
			for (int j = 0; j < matrix.NumberOfColumns; j++)
			{
				array[i][j] = matrix[i, j];
			}
		}
		return array;
	}

	/// <summary>
	///       Converts this <see cref="T:DigitalRune.Mathematics.Algebra.MatrixD" /> to a jagged double array.
	///       </summary>
	/// <returns>The result of the conversion.</returns>
	public double[][] ToArrayJagged()
	{
		return (double[][])this;
	}

	/// <summary>
	///       Performs an explicit conversion from <see cref="T:DigitalRune.Mathematics.Algebra.MatrixD" /> to <see cref="T:DigitalRune.Mathematics.Algebra.Matrix22D" />.
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <returns>The result of the conversion.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="matrix" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.InvalidCastException">
	///   <paramref name="matrix" /> is not 2x2 matrix.
	///       </exception>
	public static explicit operator Matrix22D(MatrixD matrix)
	{
		if (matrix == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7918));
		}
		if (matrix.NumberOfRows != 2)
		{
			throw new InvalidCastException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2129));
		}
		if (matrix.NumberOfColumns != 2)
		{
			throw new InvalidCastException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8220));
		}
		return new Matrix22D(matrix[0, 0], matrix[0, 1], matrix[1, 0], matrix[1, 1]);
	}

	/// <summary>
	///       Converts this <see cref="T:DigitalRune.Mathematics.Algebra.MatrixD" /> to <see cref="T:DigitalRune.Mathematics.Algebra.Matrix22D" />.
	///       </summary>
	/// <returns>The result of the conversion.</returns>
	/// <exception cref="T:System.InvalidCastException">This matrix is not 2x2 matrix.</exception>
	public Matrix22D ToMatrix22D()
	{
		return (Matrix22D)this;
	}

	/// <summary>
	///       Performs an explicit conversion from <see cref="T:DigitalRune.Mathematics.Algebra.MatrixD" /> to <see cref="T:DigitalRune.Mathematics.Algebra.Matrix33D" />.
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <returns>The result of the conversion.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="matrix" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.InvalidCastException">
	///   <paramref name="matrix" /> is not 3x3 matrix.
	///       </exception>
	public static explicit operator Matrix33D(MatrixD matrix)
	{
		if (matrix == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7918));
		}
		if (matrix.NumberOfRows != 3)
		{
			throw new InvalidCastException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2129));
		}
		if (matrix.NumberOfColumns != 3)
		{
			throw new InvalidCastException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8220));
		}
		return new Matrix33D(matrix[0, 0], matrix[0, 1], matrix[0, 2], matrix[1, 0], matrix[1, 1], matrix[1, 2], matrix[2, 0], matrix[2, 1], matrix[2, 2]);
	}

	/// <summary>
	///       Converts this <see cref="T:DigitalRune.Mathematics.Algebra.MatrixD" /> to <see cref="T:DigitalRune.Mathematics.Algebra.Matrix33D" />.
	///       </summary>
	/// <returns>The result of the conversion.</returns>
	/// <exception cref="T:System.InvalidCastException">The matrix is not 3x3 matrix.</exception>
	public Matrix33D ToMatrix33D()
	{
		return (Matrix33D)this;
	}

	/// <summary>
	///       Performs an explicit conversion from <see cref="T:DigitalRune.Mathematics.Algebra.MatrixD" /> to <see cref="T:DigitalRune.Mathematics.Algebra.Matrix44D" />.
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <returns>The result of the conversion.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="matrix" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.InvalidCastException">
	///   <paramref name="matrix" /> is not 4x4 matrix.
	///       </exception>
	public static explicit operator Matrix44D(MatrixD matrix)
	{
		if (matrix == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7918));
		}
		if (matrix.NumberOfRows != 4)
		{
			throw new InvalidCastException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2129));
		}
		if (matrix.NumberOfColumns != 4)
		{
			throw new InvalidCastException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8220));
		}
		return new Matrix44D(matrix[0, 0], matrix[0, 1], matrix[0, 2], matrix[0, 3], matrix[1, 0], matrix[1, 1], matrix[1, 2], matrix[1, 3], matrix[2, 0], matrix[2, 1], matrix[2, 2], matrix[2, 3], matrix[3, 0], matrix[3, 1], matrix[3, 2], matrix[3, 3]);
	}

	/// <summary>
	///       Converts this <see cref="T:DigitalRune.Mathematics.Algebra.MatrixD" /> to <see cref="T:DigitalRune.Mathematics.Algebra.Matrix44D" />.
	///       </summary>
	/// <returns>The result of the conversion.</returns>
	/// <exception cref="T:System.InvalidCastException">This matrix is not 4x4 matrix.</exception>
	public Matrix44D ToMatrix44D()
	{
		return (Matrix44D)this;
	}

	/// <summary>
	///       Performs an explicit conversion from <see cref="T:DigitalRune.Mathematics.Algebra.MatrixD" /> to <see cref="T:DigitalRune.Mathematics.Algebra.MatrixF" />.
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <returns>The result of the conversion.</returns>
	public static explicit operator MatrixF(MatrixD matrix)
	{
		if (matrix == null)
		{
			return null;
		}
		MatrixF matrixF = new MatrixF(matrix.NumberOfRows, matrix.NumberOfColumns);
		for (int i = 0; i < matrix.NumberOfRows; i++)
		{
			for (int j = 0; j < matrix.NumberOfColumns; j++)
			{
				matrixF[i, j] = (float)matrix[i, j];
			}
		}
		return matrixF;
	}

	/// <summary>
	///       Converts this <see cref="T:DigitalRune.Mathematics.Algebra.MatrixD" /> to <see cref="T:DigitalRune.Mathematics.Algebra.MatrixF" />.
	///       </summary>
	/// <returns>The result of the conversion.</returns>
	public MatrixF ToMatrixF()
	{
		return (MatrixF)this;
	}

	/// <summary>
	///       Sets each matrix element to its absolute value.
	///       </summary>
	public void Absolute()
	{
		for (int i = 0; i < NumberOfRows; i++)
		{
			for (int j = 0; j < NumberOfColumns; j++)
			{
				_m[i, j] = Math.Abs(_m[i, j]);
			}
		}
	}

	/// <summary>
	///       Clamps near-zero matrix elements to zero.
	///       </summary>
	public void ClampToZero()
	{
		for (int i = 0; i < NumberOfRows; i++)
		{
			for (int j = 0; j < NumberOfColumns; j++)
			{
				_m[i, j] = Numeric.ClampToZero(_m[i, j]);
			}
		}
	}

	/// <summary>
	///       Clamps near-zero matrix elements to zero.
	///       </summary>
	/// <param name="epsilon">The tolerance value.</param>
	public void ClampToZero(double epsilon)
	{
		for (int i = 0; i < NumberOfRows; i++)
		{
			for (int j = 0; j < NumberOfColumns; j++)
			{
				_m[i, j] = Numeric.ClampToZero(_m[i, j], epsilon);
			}
		}
	}

	/// <summary>
	///       Clones this instance.
	///       </summary>
	/// <returns>A copy of this instance.</returns>
	public MatrixD Clone()
	{
		return new MatrixD(_m);
	}

	/// <summary>
	///       Gets the column with the given index.
	///       </summary>
	/// <param name="index">The index.</param>
	/// <returns>The column with the given index.</returns>
	public VectorD GetColumn(int index)
	{
		VectorD vectorD = new VectorD(NumberOfRows);
		for (int i = 0; i < NumberOfRows; i++)
		{
			vectorD[i] = _m[i, index];
		}
		return vectorD;
	}

	/// <summary>
	///       Sets a column.
	///       </summary>
	/// <param name="index">The index of the column.</param>
	/// <param name="columnVector">The column vector.</param>
	/// <exception cref="T:System.IndexOutOfRangeException">
	///       The <paramref name="index" /> is out of range or <paramref name="columnVector" /> has to few
	///       elements.
	///       </exception>
	/// <exception cref="T:System.NullReferenceException">
	///   <paramref name="columnVector" /> must not be <see langword="null" />.
	///       </exception>
	public void SetColumn(int index, VectorD columnVector)
	{
		for (int i = 0; i < NumberOfRows; i++)
		{
			_m[i, index] = columnVector[i];
		}
	}

	/// <summary>
	///       Gets the row with the given index.
	///       </summary>
	/// <param name="index">The index.</param>
	/// <returns>The row with the given index.</returns>
	public VectorD GetRow(int index)
	{
		VectorD vectorD = new VectorD(NumberOfColumns);
		for (int i = 0; i < NumberOfColumns; i++)
		{
			vectorD[i] = _m[index, i];
		}
		return vectorD;
	}

	/// <summary>
	///       Sets a row.
	///       </summary>
	/// <param name="index">The index of the row.</param>
	/// <param name="rowVector">The row vector.</param>
	/// <exception cref="T:System.IndexOutOfRangeException">
	///       The <paramref name="index" /> is out of range or <paramref name="rowVector" /> has to few
	///       elements.
	///       </exception>
	/// <exception cref="T:System.NullReferenceException">
	///   <paramref name="rowVector" /> must not be <see langword="null" />.
	///       </exception>
	public void SetRow(int index, VectorD rowVector)
	{
		for (int i = 0; i < NumberOfColumns; i++)
		{
			_m[index, i] = rowVector[i];
		}
	}

	/// <summary>
	///       Gets the minor matrix.
	///       </summary>
	/// <param name="row">The row.</param>
	/// <param name="column">The column.</param>
	/// <returns>The minor matrix.</returns>
	/// <exception cref="T:DigitalRune.Mathematics.MathematicsException">
	///       Cannot get the minor matrix of a 1x1 matrix.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="row" /> or <paramref name="column" /> is out of range.
	///       </exception>
	public MatrixD GetMinor(int row, int column)
	{
		if (NumberOfRows <= 1 || NumberOfColumns <= 1)
		{
			throw new MathematicsException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8295));
		}
		if (row >= NumberOfRows)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4958));
		}
		if (column >= NumberOfColumns)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4824));
		}
		MatrixD matrixD = new MatrixD(NumberOfRows - 1, NumberOfColumns - 1);
		int num = 0;
		for (int i = 0; i < NumberOfRows - 1; i++)
		{
			if (i == row)
			{
				num++;
			}
			int num2 = 0;
			for (int j = 0; j < NumberOfColumns - 1; j++)
			{
				if (j == column)
				{
					num2++;
				}
				matrixD[i, j] = _m[num, num2];
				num2++;
			}
			num++;
		}
		return matrixD;
	}

	/// <summary>
	///       Gets a submatrix of this matrix.
	///       </summary>
	/// <param name="startRow">The index of the start row.</param>
	/// <param name="endRow">The index of the end row (included in the submatrix).</param>
	/// <param name="startColumn">The index of the start column.</param>
	/// <param name="endColumn">The index of the end column (included in the submatrix).</param>
	/// <returns>The extracted submatrix.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="startRow" /> is greater than <paramref name="endRow" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="startColumn" /> is greater than <paramref name="endColumn" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="startRow" />, <paramref name="endRow" />, <paramref name="startColumn" />, or 
	///       <paramref name="endColumn" /> is out of range.
	///       </exception>
	public MatrixD GetSubmatrix(int startRow, int endRow, int startColumn, int endColumn)
	{
		if (startRow > endRow)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8380), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8397));
		}
		if (startColumn > endColumn)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8490), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8513));
		}
		if (startRow < 0 || startRow >= NumberOfRows)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8380));
		}
		if (endRow < 0 || endRow >= NumberOfRows)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8618));
		}
		if (startColumn < 0 || startColumn >= NumberOfColumns)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8490));
		}
		if (endColumn < 0 || endColumn >= NumberOfColumns)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8631));
		}
		MatrixD matrixD = new MatrixD(endRow - startRow + 1, endColumn - startColumn + 1);
		for (int i = startRow; i <= endRow; i++)
		{
			for (int j = startColumn; j <= endColumn; j++)
			{
				matrixD[i - startRow, j - startColumn] = _m[i, j];
			}
		}
		return matrixD;
	}

	/// <summary>
	///       Gets a submatrix of this matrix.
	///       </summary>
	/// <param name="startRow">The index of the start row.</param>
	/// <param name="endRow">The index of the end row (included in the submatrix).</param>
	/// <param name="columns">The indices of the columns.</param>
	/// <returns>The extracted submatrix.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="startRow" /> is greater than <paramref name="endRow" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="startRow" /> or <paramref name="endRow" /> is out of range.
	///       </exception>
	/// <exception cref="T:System.IndexOutOfRangeException">An index in <paramref name="columns" />
	///       is invalid.</exception>
	public MatrixD GetSubmatrix(int startRow, int endRow, int[] columns)
	{
		if (startRow > endRow)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8380), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8397));
		}
		if (startRow < 0 || startRow >= NumberOfRows)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8380));
		}
		if (endRow < 0 || endRow >= NumberOfRows)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8618));
		}
		if (columns == null)
		{
			return null;
		}
		MatrixD matrixD = new MatrixD(endRow - startRow + 1, columns.Length);
		for (int i = startRow; i <= endRow; i++)
		{
			for (int j = 0; j < columns.Length; j++)
			{
				matrixD[i - startRow, j] = _m[i, columns[j]];
			}
		}
		return matrixD;
	}

	/// <summary>
	///       Gets a submatrix of this matrix.
	///       </summary>
	/// <param name="rows">The indices of the rows.</param>
	/// <param name="startColumn">The index of the start column.</param>
	/// <param name="endColumn">The index of the end column (included in the submatrix).</param>
	/// <returns>The extracted submatrix.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="startColumn" /> is greater than <paramref name="endColumn" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="startColumn" /> or <paramref name="endColumn" /> is out of range.
	///       </exception>
	/// <exception cref="T:System.IndexOutOfRangeException">
	///       An index in <paramref name="rows" /> is invalid.
	///       </exception>
	public MatrixD GetSubmatrix(int[] rows, int startColumn, int endColumn)
	{
		if (startColumn > endColumn)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8490), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8513));
		}
		if (startColumn < 0 || startColumn >= NumberOfColumns)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8490));
		}
		if (endColumn < 0 || endColumn >= NumberOfColumns)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8631));
		}
		if (rows == null)
		{
			return null;
		}
		MatrixD matrixD = new MatrixD(rows.Length, endColumn - startColumn + 1);
		for (int i = 0; i < rows.Length; i++)
		{
			for (int j = startColumn; j <= endColumn; j++)
			{
				matrixD[i, j - startColumn] = _m[rows[i], j];
			}
		}
		return matrixD;
	}

	/// <summary>
	///       Gets a submatrix of this matrix.
	///       </summary>
	/// <param name="rows">The indices of the rows.</param>
	/// <param name="columns">The indices of the columns.</param>
	/// <returns>The extracted submatrix.</returns>
	/// <exception cref="T:System.IndexOutOfRangeException">
	///       An index in <paramref name="rows" /> or <paramref name="columns" /> is invalid.
	///       </exception>
	public MatrixD GetSubmatrix(int[] rows, int[] columns)
	{
		if (rows == null || columns == null)
		{
			return null;
		}
		MatrixD matrixD = new MatrixD(rows.Length, columns.Length);
		for (int i = 0; i < rows.Length; i++)
		{
			for (int j = 0; j < columns.Length; j++)
			{
				matrixD[i, j] = _m[rows[i], columns[j]];
			}
		}
		return matrixD;
	}

	/// <summary>
	///       Inverts the matrix.
	///       </summary>
	/// <exception cref="T:DigitalRune.Mathematics.MathematicsException">
	///       The matrix is singular (i.e. it is not invertible).
	///       </exception>
	public void Invert()
	{
		_m = Inverse._m;
	}

	/// <summary>
	///       Inverts the matrix if it is invertible.
	///       </summary>
	/// <returns>
	///   <see langword="true" /> if the matrix is invertible; otherwise <see langword="false" />.
	///       </returns>
	public bool TryInvert()
	{
		try
		{
			Invert();
		}
		catch (MathematicsException)
		{
			return false;
		}
		return true;
	}

	/// <summary>
	///       Sets the elements of this matrix.
	///       </summary>
	/// <param name="matrix">The matrix from which the elements are copied.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="matrix" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///       The number of rows or columns of the matrices do not match.
	///       </exception>
	public void Set(MatrixD matrix)
	{
		if (matrix == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7918));
		}
		c5461797bbc5e8934b246d0414a6cffb9(this, matrix);
		for (int i = 0; i < NumberOfRows; i++)
		{
			for (int j = 0; j < NumberOfColumns; j++)
			{
				_m[i, j] = matrix[i, j];
			}
		}
	}

	/// <summary>
	///       Sets the matrix elements to the specified value.
	///       </summary>
	/// <param name="value">The value.</param>
	public void Set(double value)
	{
		for (int i = 0; i < NumberOfRows; i++)
		{
			for (int j = 0; j < NumberOfColumns; j++)
			{
				_m[i, j] = value;
			}
		}
	}

	/// <summary>
	///       Sets the matrix elements to the values of the array.
	///       </summary>
	/// <param name="elements">The elements array.</param>
	/// <param name="order">The order of the matrix elements in the array.</param>
	/// <exception cref="T:System.IndexOutOfRangeException">
	///   <paramref name="elements" /> must have at least
	///        <see cref="P:DigitalRune.Mathematics.Algebra.MatrixD.NumberOfRows" /> * <see cref="P:DigitalRune.Mathematics.Algebra.MatrixD.NumberOfColumns" /> elements.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="elements" /> must not be <see langword="null" />.
	///       </exception>
	public void Set(double[] elements, MatrixOrder order)
	{
		if (elements == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8650));
		}
		if (order == MatrixOrder.RowMajor)
		{
			for (int i = 0; i < NumberOfRows; i++)
			{
				for (int j = 0; j < NumberOfColumns; j++)
				{
					_m[i, j] = elements[i * NumberOfColumns + j];
				}
			}
			return;
		}
		for (int k = 0; k < NumberOfColumns; k++)
		{
			for (int l = 0; l < NumberOfRows; l++)
			{
				_m[l, k] = elements[k * NumberOfRows + l];
			}
		}
	}

	/// <summary>
	///       Sets the matrix elements to the values of the list.
	///       </summary>
	/// <param name="elements">The elements list.</param>
	/// <param name="order">The order of the matrix elements in the list.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="elements" /> must have at least 
	///       <see cref="P:DigitalRune.Mathematics.Algebra.MatrixD.NumberOfRows" />*<see cref="P:DigitalRune.Mathematics.Algebra.MatrixD.NumberOfColumns" /> elements.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="elements" /> is <see langword="null" />.
	///       </exception>
	public void Set(IList<double> elements, MatrixOrder order)
	{
		if (elements == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8650));
		}
		if (order == MatrixOrder.RowMajor)
		{
			for (int i = 0; i < NumberOfRows; i++)
			{
				for (int j = 0; j < NumberOfColumns; j++)
				{
					_m[i, j] = elements[i * NumberOfColumns + j];
				}
			}
			return;
		}
		for (int k = 0; k < NumberOfColumns; k++)
		{
			for (int l = 0; l < NumberOfRows; l++)
			{
				_m[l, k] = elements[k * NumberOfRows + l];
			}
		}
	}

	/// <summary>
	///       Sets the matrix elements to the values of the array.
	///       </summary>
	/// <param name="elements">The elements array.</param>
	/// <exception cref="T:System.IndexOutOfRangeException">
	///   <paramref name="elements" /> must have at least 
	///       <see cref="P:DigitalRune.Mathematics.Algebra.MatrixD.NumberOfRows" /> * <see cref="P:DigitalRune.Mathematics.Algebra.MatrixD.NumberOfColumns" /> elements.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="elements" /> is <see langword="null" />.
	///       </exception>
	public void Set(double[][] elements)
	{
		if (elements == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8650));
		}
		for (int i = 0; i < NumberOfRows; i++)
		{
			for (int j = 0; j < NumberOfColumns; j++)
			{
				_m[i, j] = elements[i][j];
			}
		}
	}

	/// <summary>
	///       Sets the matrix elements to the values of the array.
	///       </summary>
	/// <param name="elements">The elements array.</param>
	/// <exception cref="T:System.IndexOutOfRangeException">
	///   <paramref name="elements" /> must have at least 
	///       <see cref="P:DigitalRune.Mathematics.Algebra.MatrixD.NumberOfRows" /> x <see cref="P:DigitalRune.Mathematics.Algebra.MatrixD.NumberOfColumns" /> elements.
	///       </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="elements" /> is <see langword="null" />.
	///       </exception>
	[CLSCompliant(false)]
	public void Set(double[,] elements)
	{
		if (elements == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8650));
		}
		for (int i = 0; i < NumberOfRows; i++)
		{
			for (int j = 0; j < NumberOfColumns; j++)
			{
				_m[i, j] = elements[i, j];
			}
		}
	}

	/// <summary>
	///       Sets this matrix to an identity matrix.
	///       </summary>
	public void SetIdentity()
	{
		for (int i = 0; i < NumberOfRows; i++)
		{
			for (int j = 0; j < NumberOfColumns; j++)
			{
				if (i == j)
				{
					_m[i, j] = 1.0;
				}
				else
				{
					_m[i, j] = 0.0;
				}
			}
		}
	}

	/// <summary>
	///       Sets a submatrix of this matrix.
	///       </summary>
	/// <param name="submatrix">The submatrix.</param>
	/// <param name="startRow">The index of the start row in this matrix.</param>
	/// <param name="startColumn">The index of the start column in this matrix.</param>
	/// <exception cref="T:System.IndexOutOfRangeException">
	///       The <paramref name="startRow" />, <paramref name="startColumn" /> or the dimensions of the 
	///       submatrix are to high, so that the submatrix does not fit into this matrix.
	///       </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="startRow" /> or <paramref name="startColumn" /> out of range.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="submatrix" /> is <see langword="null" />.
	///       </exception>
	public void SetSubmatrix(int startRow, int startColumn, MatrixD submatrix)
	{
		if (startRow < 0 || startRow >= NumberOfRows)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8380));
		}
		if (startColumn < 0 || startColumn >= NumberOfColumns)
		{
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8490));
		}
		if (submatrix == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8667));
		}
		for (int i = 0; i < submatrix.NumberOfRows; i++)
		{
			for (int j = 0; j < submatrix.NumberOfColumns; j++)
			{
				_m[i + startRow, j + startColumn] = submatrix[i, j];
			}
		}
	}

	/// <summary>
	///       Converts this matrix to an array of double values.
	///       </summary>
	/// <param name="order">The order of the matrix elements in the array.</param>
	/// <returns>The result of the conversion.</returns>
	public double[] ToArray1D(MatrixOrder order)
	{
		double[] array = new double[NumberOfRows * NumberOfColumns];
		if (order == MatrixOrder.ColumnMajor)
		{
			for (int i = 0; i < NumberOfRows; i++)
			{
				for (int j = 0; j < NumberOfColumns; j++)
				{
					array[j * NumberOfRows + i] = _m[i, j];
				}
			}
		}
		else
		{
			for (int k = 0; k < NumberOfRows; k++)
			{
				for (int l = 0; l < NumberOfColumns; l++)
				{
					array[k * NumberOfColumns + l] = _m[k, l];
				}
			}
		}
		return array;
	}

	/// <summary>
	///       Converts this matrix to a list of double values.
	///       </summary>
	/// <param name="order">The order of the matrix elements in the list.</param>
	/// <returns>The result of the conversion.</returns>
	public List<double> ToList(MatrixOrder order)
	{
		List<double> list = new List<double>(NumberOfRows * NumberOfColumns);
		if (order == MatrixOrder.ColumnMajor)
		{
			for (int i = 0; i < NumberOfColumns; i++)
			{
				for (int j = 0; j < NumberOfRows; j++)
				{
					list.Add(_m[j, i]);
				}
			}
		}
		else
		{
			for (int k = 0; k < NumberOfRows; k++)
			{
				for (int l = 0; l < NumberOfColumns; l++)
				{
					list.Add(_m[k, l]);
				}
			}
		}
		return list;
	}

	/// <summary>
	///       Transposes this matrix.
	///       </summary>
	public void Transpose()
	{
		double[,] array = new double[NumberOfColumns, NumberOfRows];
		for (int i = 0; i < NumberOfRows; i++)
		{
			for (int j = 0; j < NumberOfColumns; j++)
			{
				array[j, i] = _m[i, j];
			}
		}
		_m = array;
	}

	/// <summary>
	///       Returns a matrix with the absolute values of the elements of the given matrix.
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <returns>A matrix with the absolute values of the elements of the given matrix.</returns>
	public static MatrixD Absolute(MatrixD matrix)
	{
		if (matrix == null)
		{
			return null;
		}
		MatrixD matrixD = new MatrixD(matrix.NumberOfRows, matrix.NumberOfColumns);
		for (int i = 0; i < matrixD.NumberOfRows; i++)
		{
			for (int j = 0; j < matrixD.NumberOfColumns; j++)
			{
				matrixD[i, j] = Math.Abs(matrix[i, j]);
			}
		}
		return matrixD;
	}

	/// <summary>
	///       Determines whether two matrices are equal (regarding the 
	///       tolerance <see cref="P:DigitalRune.Mathematics.Numeric.EpsilonD" />).
	///       </summary>
	/// <param name="matrix1">The first matrix.</param>
	/// <param name="matrix2">The second matrix.</param>
	/// <returns>
	///   <see langword="true" /> if the matrices are equal (within the tolerance 
	///       <see cref="P:DigitalRune.Mathematics.Numeric.EpsilonD" />); otherwise, <see langword="false" />.
	///       </returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="matrix1" /> or <paramref name="matrix2" /> is <see langword="null" />.
	///       </exception>
	public static bool AreNumericallyEqual(MatrixD matrix1, MatrixD matrix2)
	{
		if (matrix1 == null && matrix2 == null)
		{
			return true;
		}
		if (matrix1 == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7852));
		}
		if (matrix2 == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7867));
		}
		if (matrix1.NumberOfColumns != matrix2.NumberOfColumns)
		{
			return false;
		}
		if (matrix1.NumberOfRows != matrix2.NumberOfRows)
		{
			return false;
		}
		for (int i = 0; i < matrix1.NumberOfRows; i++)
		{
			for (int j = 0; j < matrix2.NumberOfColumns; j++)
			{
				if (!Numeric.AreEqual(matrix1[i, j], matrix2[i, j]))
				{
					return false;
				}
			}
		}
		return true;
	}

	/// <summary>
	///       Determines whether two matrices are equal (regarding a specific 
	///       tolerance).
	///       </summary>
	/// <param name="matrix1">The first matrix.</param>
	/// <param name="matrix2">The second matrix.</param>
	/// <param name="epsilon">The tolerance value.</param>
	/// <returns>
	///   <see langword="true" /> if the matrices are equal (within the tolerance 
	///       <paramref name="epsilon" />); otherwise, <see langword="false" />.
	///       </returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="matrix1" /> or <paramref name="matrix2" /> is <see langword="null" />.
	///       </exception>
	public static bool AreNumericallyEqual(MatrixD matrix1, MatrixD matrix2, double epsilon)
	{
		if (matrix1 == null && matrix2 == null)
		{
			return true;
		}
		if (matrix1 == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7852));
		}
		if (matrix2 == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7867));
		}
		if (matrix1.NumberOfColumns != matrix2.NumberOfColumns)
		{
			return false;
		}
		if (matrix1.NumberOfRows != matrix2.NumberOfRows)
		{
			return false;
		}
		for (int i = 0; i < matrix1.NumberOfRows; i++)
		{
			for (int j = 0; j < matrix2.NumberOfColumns; j++)
			{
				if (!Numeric.AreEqual(matrix1[i, j], matrix2[i, j], epsilon))
				{
					return false;
				}
			}
		}
		return true;
	}

	private static void c5461797bbc5e8934b246d0414a6cffb9(MatrixD cd52a30e48bd752549c8f1f8ba0cb77d1, MatrixD c239a8248aee3726e81629fca82f0719d)
	{
		if (cd52a30e48bd752549c8f1f8ba0cb77d1.NumberOfRows != c239a8248aee3726e81629fca82f0719d.NumberOfRows)
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8686));
		}
		if (cd52a30e48bd752549c8f1f8ba0cb77d1.NumberOfColumns != c239a8248aee3726e81629fca82f0719d.NumberOfColumns)
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(8787));
		}
	}

	/// <summary>
	///       Returns a matrix with the matrix elements clamped to the range [min, max].
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <returns>
	///       The matrix with small elements clamped to zero.
	///       </returns>
	public static MatrixD ClampToZero(MatrixD matrix)
	{
		if (matrix == null)
		{
			return null;
		}
		MatrixD matrixD = new MatrixD(matrix.NumberOfRows, matrix.NumberOfColumns);
		for (int i = 0; i < matrix.NumberOfRows; i++)
		{
			for (int j = 0; j < matrix.NumberOfColumns; j++)
			{
				matrixD[i, j] = Numeric.ClampToZero(matrix[i, j]);
			}
		}
		return matrixD;
	}

	/// <summary>
	///       Returns a matrix with the matrix elements clamped to the range [min, max].
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <param name="epsilon">The tolerance value.</param>
	/// <returns>
	///       The matrix with small elements clamped to zero.
	///       </returns>
	public static MatrixD ClampToZero(MatrixD matrix, double epsilon)
	{
		if (matrix == null)
		{
			return null;
		}
		MatrixD matrixD = new MatrixD(matrix.NumberOfRows, matrix.NumberOfColumns);
		for (int i = 0; i < matrix.NumberOfRows; i++)
		{
			for (int j = 0; j < matrix.NumberOfColumns; j++)
			{
				matrixD[i, j] = Numeric.ClampToZero(matrix[i, j], epsilon);
			}
		}
		return matrixD;
	}

	/// <summary>
	///       Creates an identity matrix.
	///       </summary>
	/// <param name="numberOfRows">The number of rows.</param>
	/// <param name="numberOfColumns">The number of columns.</param>
	/// <returns>An identity matrix.</returns>
	public static MatrixD CreateIdentity(int numberOfRows, int numberOfColumns)
	{
		MatrixD matrixD = new MatrixD(numberOfRows, numberOfColumns);
		int num = Math.Min(numberOfRows, numberOfColumns);
		for (int i = 0; i < num; i++)
		{
			matrixD[i, i] = 1.0;
		}
		return matrixD;
	}

	/// <summary>
	///       Solves the linear set of equations A * X = B.
	///       </summary>
	/// <param name="matrixA">
	///       The matrix A. (Can be rectangular. Number of rows ≥ number of columns.)
	///       </param>
	/// <param name="matrixB">
	///       The matrix B with the same number of rows as A and any number of columns.
	///       </param>
	/// <returns>The matrix X.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="matrixA" /> or <paramref name="matrixB" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///       The number of rows does not match.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///       The number of rows in <paramref name="matrixA" /> must be greater than or equal to the number 
	///       of columns.
	///       </exception>
	/// <exception cref="T:DigitalRune.Mathematics.MathematicsException">
	///       The matrix A does not have full rank.
	///       </exception>
	public static MatrixD SolveLinearEquations(MatrixD matrixA, MatrixD matrixB)
	{
		if (matrixA == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2291));
		}
		if (matrixB == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2114));
		}
		if (matrixA.IsSquare)
		{
			return new LUDecompositionD(matrixA).SolveLinearEquations(matrixB);
		}
		if (matrixA.NumberOfRows > matrixA.NumberOfColumns)
		{
			return new QRDecompositionD(matrixA).SolveLinearEquations(matrixB);
		}
		throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2306), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2291));
	}

	/// <summary>
	///       Solves the linear set of equations A * x = b.
	///       </summary>
	/// <param name="matrixA">The matrix A. (Can be rectangular. Number of rows ≥ number of columns.)</param>
	/// <param name="vectorB">The column vector b with as many rows as A.</param>
	/// <returns>The vector x.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="vectorB" /> is <see langword="null" />.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///       The number of rows does not match.
	///       </exception>
	/// <exception cref="T:System.ArgumentException">
	///       The number of rows in matrix A must be greater than or equal to the number of columns.
	///       </exception>
	/// <exception cref="T:DigitalRune.Mathematics.MathematicsException">
	///       The matrix A does not have full rank.
	///       </exception>
	public static VectorD SolveLinearEquations(MatrixD matrixA, VectorD vectorB)
	{
		if (vectorB == null)
		{
			throw new ArgumentNullException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(2456));
		}
		return SolveLinearEquations(matrixA, vectorB.ToMatrixD()).GetColumn(0);
	}
}
