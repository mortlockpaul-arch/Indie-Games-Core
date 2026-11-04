using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using A;

namespace DigitalRune.Mathematics.Algebra;

/// <summary>
///       Defines a 3 x 3 matrix (single-precision).
///       </summary>
[Serializable]
[TypeConverter(typeof(ExpandableObjectConverter))]
public struct Matrix33F : IEquatable<Matrix33F>
{
	/// <summary>
	///       Returns a <see cref="T:DigitalRune.Mathematics.Algebra.Matrix33F" /> with all of its components set to zero.
	///       </summary>
	public static readonly Matrix33F Zero;

	/// <summary>
	///       Returns a <see cref="T:DigitalRune.Mathematics.Algebra.Matrix33F" /> with all of its components set to one.
	///       </summary>
	public static readonly Matrix33F One;

	/// <summary>
	///       Returns the 3 x 3 identity matrix.
	///       </summary>
	public static readonly Matrix33F Identity;

	/// <summary>
	///       The element in first row, first column.
	///       </summary>
	public float M00;

	/// <summary>
	///       The element in first row, second column.
	///       </summary>
	public float M01;

	/// <summary>
	///       The element in first row, third column.
	///       </summary>
	public float M02;

	/// <summary>
	///       The element in second row, first column.
	///       </summary>
	public float M10;

	/// <summary>
	///       The element in second row, second column.
	///       </summary>
	public float M11;

	/// <summary>
	///       The element in second row, third column.
	///       </summary>
	public float M12;

	/// <summary>
	///       The element in third row, first column.
	///       </summary>
	public float M20;

	/// <summary>
	///       The element in third row, second column.
	///       </summary>
	public float M21;

	/// <summary>
	///       The element in third row, third column.
	///       </summary>
	public float M22;

	/// <summary>
	///       Gets or sets the element at the specified index.
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
				0 => M00, 
				1 => M01, 
				2 => M02, 
				3 => M10, 
				4 => M11, 
				5 => M12, 
				6 => M20, 
				7 => M21, 
				8 => M22, 
				_ => throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(622), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6835)), 
			};
		}
		set
		{
			switch (index)
			{
			case 0:
				M00 = value;
				break;
			case 1:
				M01 = value;
				break;
			case 2:
				M02 = value;
				break;
			case 3:
				M10 = value;
				break;
			case 4:
				M11 = value;
				break;
			case 5:
				M12 = value;
				break;
			case 6:
				M20 = value;
				break;
			case 7:
				M21 = value;
				break;
			case 8:
				M22 = value;
				break;
			default:
				throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(622), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6835));
			}
		}
	}

	/// <summary>
	///       Gets or sets the element at the specified index.
	///       </summary>
	/// <param name="row">The row index.</param>
	/// <param name="column">The column index.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///       The index [<paramref name="row" />, <paramref name="column" />] is out of range.
	///       </exception>
	public float this[int row, int column]
	{
		get
		{
			return row switch
			{
				0 => column switch
				{
					0 => M00, 
					1 => M01, 
					2 => M02, 
					_ => throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4824), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6942)), 
				}, 
				1 => column switch
				{
					0 => M10, 
					1 => M11, 
					2 => M12, 
					_ => throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4824), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6942)), 
				}, 
				2 => column switch
				{
					0 => M20, 
					1 => M21, 
					2 => M22, 
					_ => throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4824), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6942)), 
				}, 
				_ => throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4958), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7072)), 
			};
		}
		set
		{
			switch (row)
			{
			case 0:
				switch (column)
				{
				case 0:
					M00 = value;
					break;
				case 1:
					M01 = value;
					break;
				case 2:
					M02 = value;
					break;
				default:
					throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4824), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6942));
				}
				break;
			case 1:
				switch (column)
				{
				case 0:
					M10 = value;
					break;
				case 1:
					M11 = value;
					break;
				case 2:
					M12 = value;
					break;
				default:
					throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4824), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6942));
				}
				break;
			case 2:
				switch (column)
				{
				case 0:
					M20 = value;
					break;
				case 1:
					M21 = value;
					break;
				case 2:
					M22 = value;
					break;
				default:
					throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4824), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6942));
				}
				break;
			default:
				throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(4958), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7072));
			}
		}
	}

	/// <summary>
	///       Returns the determinant of this matrix.
	///       </summary>
	public float Determinant => M00 * (M11 * M22 - M12 * M21) - M01 * (M10 * M22 - M12 * M20) + M02 * (M10 * M21 - M20 * M11);

	/// <summary>
	///       Gets a value indicating whether an element of the matrix is <see cref="F:System.Single.NaN" />.
	///       </summary>
	public bool IsNaN
	{
		get
		{
			if (!Numeric.IsNaN(M00) && !Numeric.IsNaN(M01) && !Numeric.IsNaN(M02) && !Numeric.IsNaN(M10) && !Numeric.IsNaN(M11) && !Numeric.IsNaN(M12) && !Numeric.IsNaN(M20) && !Numeric.IsNaN(M21))
			{
				return Numeric.IsNaN(M22);
			}
			return true;
		}
	}

	/// <summary>
	///       Gets a value indicating whether this instance is orthogonal.
	///       </summary>
	public bool IsOrthogonal => AreNumericallyEqual(Identity, this * Transposed);

	/// <summary>
	///       Gets a value indicating whether this instance is a rotation matrix.
	///       </summary>
	public bool IsRotation
	{
		get
		{
			if (Numeric.AreEqual(1f, Determinant))
			{
				return IsOrthogonal;
			}
			return false;
		}
	}

	/// <summary>
	///       Gets a value indicating whether this matrix is symmetric.
	///       </summary>
	public bool IsSymmetric
	{
		get
		{
			if (M01 == M10 && M02 == M20)
			{
				return M12 == M21;
			}
			return false;
		}
	}

	/// <summary>
	///       Gets the matrix trace (the sum of the diagonal elements).
	///       </summary>
	public float Trace => M00 + M11 + M22;

	/// <summary>
	///       Returns the transposed of this matrix.
	///       </summary>
	/// <returns>The transposed of this matrix.</returns>
	public Matrix33F Transposed
	{
		get
		{
			Matrix33F result = this;
			result.Transpose();
			return result;
		}
	}

	/// <summary>
	///       Returns the inverse of this matrix.
	///       </summary>
	/// <exception cref="T:DigitalRune.Mathematics.MathematicsException">
	///       The matrix is singular (i.e. it is not invertible).
	///       </exception>
	public Matrix33F Inverse
	{
		get
		{
			Matrix33F result = this;
			result.Invert();
			return result;
		}
	}

	static Matrix33F()
	{
		Zero = new Matrix33F(0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f);
		One = new Matrix33F(1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f);
		Identity = new Matrix33F(1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 1f);
		TypeInitializer.Initialize(cb8acf96051824377b85f63be63e3c2a3.ce9f9bc9ecd5ed400a711abaa33452c25);
		TypeInitializer.CreateInstance(11, cb8acf96051824377b85f63be63e3c2a3.ccda5e1cdd8faa90b24f6bc28c9af6b28);
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Algebra.Matrix33F" /> struct.
	///       </summary>
	/// <param name="elementValue">The initial value for the matrix elements.</param>
	public Matrix33F(float elementValue)
		: this(elementValue, elementValue, elementValue, elementValue, elementValue, elementValue, elementValue, elementValue, elementValue)
	{
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Algebra.Matrix33F" /> class.
	///       </summary>
	/// <param name="m00">The element in the first row, first column.</param>
	/// <param name="m01">The element in the first row, second column.</param>
	/// <param name="m02">The element in the first row, third column.</param>
	/// <param name="m10">The element in the second row, first column.</param>
	/// <param name="m11">The element in the second row, second column.</param>
	/// <param name="m12">The element in the second row, third column.</param>
	/// <param name="m20">The element in the third row, first column.</param>
	/// <param name="m21">The element in the third row, second column.</param>
	/// <param name="m22">The element in the third row, third column.</param>
	public Matrix33F(float m00, float m01, float m02, float m10, float m11, float m12, float m20, float m21, float m22)
	{
		M00 = m00;
		M01 = m01;
		M02 = m02;
		M10 = m10;
		M11 = m11;
		M12 = m12;
		M20 = m20;
		M21 = m21;
		M22 = m22;
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Algebra.Matrix33F" /> struct.
	///       </summary>
	/// <param name="elements">The array with the initial values for the matrix elements.</param>
	/// <param name="order">The order of the matrix elements in <paramref name="elements" />.</param>
	/// <exception cref="T:System.IndexOutOfRangeException">
	///   <paramref name="elements" /> has less than 9 elements.
	///       </exception>
	/// <exception cref="T:System.NullReferenceException">
	///   <paramref name="elements" /> must not be <see langword="null" />.
	///       </exception>
	public Matrix33F(float[] elements, MatrixOrder order)
	{
		if (order == MatrixOrder.RowMajor)
		{
			M00 = elements[0];
			M01 = elements[1];
			M02 = elements[2];
			M10 = elements[3];
			M11 = elements[4];
			M12 = elements[5];
			M20 = elements[6];
			M21 = elements[7];
			M22 = elements[8];
		}
		else
		{
			M00 = elements[0];
			M10 = elements[1];
			M20 = elements[2];
			M01 = elements[3];
			M11 = elements[4];
			M21 = elements[5];
			M02 = elements[6];
			M12 = elements[7];
			M22 = elements[8];
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Algebra.Matrix33F" /> struct.
	///       </summary>
	/// <param name="elements">The list with the initial values for the matrix elements.</param>
	/// <param name="order">The order of the matrix elements in <paramref name="elements" />.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="elements" /> has less than 9 elements.
	///       </exception>
	/// <exception cref="T:System.NullReferenceException">
	///   <paramref name="elements" /> must not be <see langword="null" />.
	///       </exception>
	public Matrix33F(IList<float> elements, MatrixOrder order)
	{
		if (order == MatrixOrder.RowMajor)
		{
			M00 = elements[0];
			M01 = elements[1];
			M02 = elements[2];
			M10 = elements[3];
			M11 = elements[4];
			M12 = elements[5];
			M20 = elements[6];
			M21 = elements[7];
			M22 = elements[8];
		}
		else
		{
			M00 = elements[0];
			M10 = elements[1];
			M20 = elements[2];
			M01 = elements[3];
			M11 = elements[4];
			M21 = elements[5];
			M02 = elements[6];
			M12 = elements[7];
			M22 = elements[8];
		}
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Algebra.Matrix33F" /> struct.
	///       </summary>
	/// <param name="elements">The array with the initial values for the matrix elements.</param>
	/// <exception cref="T:System.IndexOutOfRangeException">
	///   <paramref name="elements" /> has less than 3x3 elements.
	///       </exception>
	/// <exception cref="T:System.NullReferenceException">
	///   <paramref name="elements" /> or the arrays in elements[0] must not be <see langword="null" />.
	///       </exception>
	[CLSCompliant(false)]
	public Matrix33F(float[,] elements)
	{
		M00 = elements[0, 0];
		M01 = elements[0, 1];
		M02 = elements[0, 2];
		M10 = elements[1, 0];
		M11 = elements[1, 1];
		M12 = elements[1, 2];
		M20 = elements[2, 0];
		M21 = elements[2, 1];
		M22 = elements[2, 2];
	}

	/// <summary>
	///       Initializes a new instance of the <see cref="T:DigitalRune.Mathematics.Algebra.Matrix33F" /> struct.
	///       </summary>
	/// <param name="elements">The array with the initial values for the matrix elements.</param>
	/// <exception cref="T:System.IndexOutOfRangeException">
	///   <paramref name="elements" /> has less than 3x3 elements.
	///       </exception>
	/// <exception cref="T:System.NullReferenceException">
	///   <paramref name="elements" /> or the arrays in elements[0] must not be <see langword="null" />.
	///       </exception>
	public Matrix33F(float[][] elements)
	{
		M00 = elements[0][0];
		M01 = elements[0][1];
		M02 = elements[0][2];
		M10 = elements[1][0];
		M11 = elements[1][1];
		M12 = elements[1][2];
		M20 = elements[2][0];
		M21 = elements[2][1];
		M22 = elements[2][2];
	}

	/// <summary>
	///       Returns the hash code for this instance.
	///       </summary>
	/// <returns>
	///       A 32-bit signed integer that is the hash code for this instance.
	///       </returns>
	public override int GetHashCode()
	{
		int hashCode = M00.GetHashCode();
		hashCode = (hashCode * 397) ^ M01.GetHashCode();
		hashCode = (hashCode * 397) ^ M02.GetHashCode();
		hashCode = (hashCode * 397) ^ M10.GetHashCode();
		hashCode = (hashCode * 397) ^ M11.GetHashCode();
		hashCode = (hashCode * 397) ^ M12.GetHashCode();
		hashCode = (hashCode * 397) ^ M20.GetHashCode();
		hashCode = (hashCode * 397) ^ M21.GetHashCode();
		return (hashCode * 397) ^ M22.GetHashCode();
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
		if (obj is Matrix33F)
		{
			return this == (Matrix33F)obj;
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
	public bool Equals(Matrix33F other)
	{
		return this == other;
	}

	/// <summary>
	///       Returns the string representation of this matrix.
	///       </summary>
	/// <returns>The string representation of this matrix.</returns>
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
	/// <returns>The string representation of this matrix.</returns>
	public string ToString(IFormatProvider provider)
	{
		return string.Format(provider, c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7195), M00, M01, M02, M10, M11, M12, M20, M21, M22);
	}

	/// <summary>
	///       Negates a matrix.
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <returns>The negated matrix.</returns>
	public static Matrix33F operator -(Matrix33F matrix)
	{
		matrix.M00 = 0f - matrix.M00;
		matrix.M01 = 0f - matrix.M01;
		matrix.M02 = 0f - matrix.M02;
		matrix.M10 = 0f - matrix.M10;
		matrix.M11 = 0f - matrix.M11;
		matrix.M12 = 0f - matrix.M12;
		matrix.M20 = 0f - matrix.M20;
		matrix.M21 = 0f - matrix.M21;
		matrix.M22 = 0f - matrix.M22;
		return matrix;
	}

	/// <summary>
	///       Negates a matrix.
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <returns>The negated matrix.</returns>
	public static Matrix33F Negate(Matrix33F matrix)
	{
		matrix.M00 = 0f - matrix.M00;
		matrix.M01 = 0f - matrix.M01;
		matrix.M02 = 0f - matrix.M02;
		matrix.M10 = 0f - matrix.M10;
		matrix.M11 = 0f - matrix.M11;
		matrix.M12 = 0f - matrix.M12;
		matrix.M20 = 0f - matrix.M20;
		matrix.M21 = 0f - matrix.M21;
		matrix.M22 = 0f - matrix.M22;
		return matrix;
	}

	/// <summary>
	///       Adds two matrices.
	///       </summary>
	/// <param name="matrix1">The first matrix.</param>
	/// <param name="matrix2">The second Matrix.</param>
	/// <returns>The sum of the two matrices.</returns>
	public static Matrix33F operator +(Matrix33F matrix1, Matrix33F matrix2)
	{
		matrix1.M00 += matrix2.M00;
		matrix1.M01 += matrix2.M01;
		matrix1.M02 += matrix2.M02;
		matrix1.M10 += matrix2.M10;
		matrix1.M11 += matrix2.M11;
		matrix1.M12 += matrix2.M12;
		matrix1.M20 += matrix2.M20;
		matrix1.M21 += matrix2.M21;
		matrix1.M22 += matrix2.M22;
		return matrix1;
	}

	/// <summary>
	///       Adds two matrices.
	///       </summary>
	/// <param name="matrix1">The first matrix.</param>
	/// <param name="matrix2">The second Matrix.</param>
	/// <returns>The sum of the two matrices.</returns>
	public static Matrix33F Add(Matrix33F matrix1, Matrix33F matrix2)
	{
		matrix1.M00 += matrix2.M00;
		matrix1.M01 += matrix2.M01;
		matrix1.M02 += matrix2.M02;
		matrix1.M10 += matrix2.M10;
		matrix1.M11 += matrix2.M11;
		matrix1.M12 += matrix2.M12;
		matrix1.M20 += matrix2.M20;
		matrix1.M21 += matrix2.M21;
		matrix1.M22 += matrix2.M22;
		return matrix1;
	}

	/// <summary>
	///       Subtracts two matrices.
	///       </summary>
	/// <param name="minuend">The first matrix (minuend).</param>
	/// <param name="subtrahend">The second matrix (subtrahend).</param>
	/// <returns>The difference of the two matrices.</returns>
	public static Matrix33F operator -(Matrix33F minuend, Matrix33F subtrahend)
	{
		minuend.M00 -= subtrahend.M00;
		minuend.M01 -= subtrahend.M01;
		minuend.M02 -= subtrahend.M02;
		minuend.M10 -= subtrahend.M10;
		minuend.M11 -= subtrahend.M11;
		minuend.M12 -= subtrahend.M12;
		minuend.M20 -= subtrahend.M20;
		minuend.M21 -= subtrahend.M21;
		minuend.M22 -= subtrahend.M22;
		return minuend;
	}

	/// <summary>
	///       Subtracts two matrices.
	///       </summary>
	/// <param name="minuend">The first matrix (minuend).</param>
	/// <param name="subtrahend">The second matrix (subtrahend).</param>
	/// <returns>The difference of the two matrices.</returns>
	public static Matrix33F Subtract(Matrix33F minuend, Matrix33F subtrahend)
	{
		minuend.M00 -= subtrahend.M00;
		minuend.M01 -= subtrahend.M01;
		minuend.M02 -= subtrahend.M02;
		minuend.M10 -= subtrahend.M10;
		minuend.M11 -= subtrahend.M11;
		minuend.M12 -= subtrahend.M12;
		minuend.M20 -= subtrahend.M20;
		minuend.M21 -= subtrahend.M21;
		minuend.M22 -= subtrahend.M22;
		return minuend;
	}

	/// <summary>
	///       Multiplies a matrix and a scalar.
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <param name="scalar">The scalar.</param>
	/// <returns>
	///       The matrix with each element multiplied by <paramref name="scalar" />.
	///       </returns>
	public static Matrix33F operator *(Matrix33F matrix, float scalar)
	{
		matrix.M00 *= scalar;
		matrix.M01 *= scalar;
		matrix.M02 *= scalar;
		matrix.M10 *= scalar;
		matrix.M11 *= scalar;
		matrix.M12 *= scalar;
		matrix.M20 *= scalar;
		matrix.M21 *= scalar;
		matrix.M22 *= scalar;
		return matrix;
	}

	/// <summary>
	///       Multiplies a matrix by a scalar.
	///       </summary>
	/// <param name="scalar">The scalar.</param>
	/// <param name="matrix">The matrix.</param>
	/// <returns>
	///       The matrix with each element multiplied by <paramref name="scalar" />.
	///       </returns>
	public static Matrix33F operator *(float scalar, Matrix33F matrix)
	{
		matrix.M00 *= scalar;
		matrix.M01 *= scalar;
		matrix.M02 *= scalar;
		matrix.M10 *= scalar;
		matrix.M11 *= scalar;
		matrix.M12 *= scalar;
		matrix.M20 *= scalar;
		matrix.M21 *= scalar;
		matrix.M22 *= scalar;
		return matrix;
	}

	/// <summary>
	///       Multiplies a matrix by a scalar.
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <param name="scalar">The scalar.</param>
	/// <returns>
	///       The matrix with each element multiplied by <paramref name="scalar" />.
	///       </returns>
	public static Matrix33F Multiply(float scalar, Matrix33F matrix)
	{
		matrix.M00 *= scalar;
		matrix.M01 *= scalar;
		matrix.M02 *= scalar;
		matrix.M10 *= scalar;
		matrix.M11 *= scalar;
		matrix.M12 *= scalar;
		matrix.M20 *= scalar;
		matrix.M21 *= scalar;
		matrix.M22 *= scalar;
		return matrix;
	}

	/// <summary>
	///       Multiplies two matrices.
	///       </summary>
	/// <param name="matrix1">The first matrix.</param>
	/// <param name="matrix2">The second matrix.</param>
	/// <returns>The matrix with the product the two matrices.</returns>
	public static Matrix33F operator *(Matrix33F matrix1, Matrix33F matrix2)
	{
		Matrix33F result = default;
		result.M00 = matrix1.M00 * matrix2.M00 + matrix1.M01 * matrix2.M10 + matrix1.M02 * matrix2.M20;
		result.M01 = matrix1.M00 * matrix2.M01 + matrix1.M01 * matrix2.M11 + matrix1.M02 * matrix2.M21;
		result.M02 = matrix1.M00 * matrix2.M02 + matrix1.M01 * matrix2.M12 + matrix1.M02 * matrix2.M22;
		result.M10 = matrix1.M10 * matrix2.M00 + matrix1.M11 * matrix2.M10 + matrix1.M12 * matrix2.M20;
		result.M11 = matrix1.M10 * matrix2.M01 + matrix1.M11 * matrix2.M11 + matrix1.M12 * matrix2.M21;
		result.M12 = matrix1.M10 * matrix2.M02 + matrix1.M11 * matrix2.M12 + matrix1.M12 * matrix2.M22;
		result.M20 = matrix1.M20 * matrix2.M00 + matrix1.M21 * matrix2.M10 + matrix1.M22 * matrix2.M20;
		result.M21 = matrix1.M20 * matrix2.M01 + matrix1.M21 * matrix2.M11 + matrix1.M22 * matrix2.M21;
		result.M22 = matrix1.M20 * matrix2.M02 + matrix1.M21 * matrix2.M12 + matrix1.M22 * matrix2.M22;
		return result;
	}

	/// <summary>
	///       Multiplies two matrices.
	///       </summary>
	/// <param name="matrix1">The first matrix.</param>
	/// <param name="matrix2">The second matrix.</param>
	/// <returns>The matrix with the product the two matrices.</returns>
	public static Matrix33F Multiply(Matrix33F matrix1, Matrix33F matrix2)
	{
		Matrix33F result = default;
		result.M00 = matrix1.M00 * matrix2.M00 + matrix1.M01 * matrix2.M10 + matrix1.M02 * matrix2.M20;
		result.M01 = matrix1.M00 * matrix2.M01 + matrix1.M01 * matrix2.M11 + matrix1.M02 * matrix2.M21;
		result.M02 = matrix1.M00 * matrix2.M02 + matrix1.M01 * matrix2.M12 + matrix1.M02 * matrix2.M22;
		result.M10 = matrix1.M10 * matrix2.M00 + matrix1.M11 * matrix2.M10 + matrix1.M12 * matrix2.M20;
		result.M11 = matrix1.M10 * matrix2.M01 + matrix1.M11 * matrix2.M11 + matrix1.M12 * matrix2.M21;
		result.M12 = matrix1.M10 * matrix2.M02 + matrix1.M11 * matrix2.M12 + matrix1.M12 * matrix2.M22;
		result.M20 = matrix1.M20 * matrix2.M00 + matrix1.M21 * matrix2.M10 + matrix1.M22 * matrix2.M20;
		result.M21 = matrix1.M20 * matrix2.M01 + matrix1.M21 * matrix2.M11 + matrix1.M22 * matrix2.M21;
		result.M22 = matrix1.M20 * matrix2.M02 + matrix1.M21 * matrix2.M12 + matrix1.M22 * matrix2.M22;
		return result;
	}

	/// <summary>
	///       Multiplies a matrix with a column vector.
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <param name="vector">The column vector.</param>
	/// <returns>The resulting column vector.</returns>
	public static Vector3F operator *(Matrix33F matrix, Vector3F vector)
	{
		Vector3F result = default;
		result.X = matrix.M00 * vector.X + matrix.M01 * vector.Y + matrix.M02 * vector.Z;
		result.Y = matrix.M10 * vector.X + matrix.M11 * vector.Y + matrix.M12 * vector.Z;
		result.Z = matrix.M20 * vector.X + matrix.M21 * vector.Y + matrix.M22 * vector.Z;
		return result;
	}

	/// <summary>
	///       Multiplies a matrix with a column vector.
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <param name="vector">The column vector.</param>
	/// <returns>The resulting column vector.</returns>
	public static Vector3F Multiply(Matrix33F matrix, Vector3F vector)
	{
		Vector3F result = default;
		result.X = matrix.M00 * vector.X + matrix.M01 * vector.Y + matrix.M02 * vector.Z;
		result.Y = matrix.M10 * vector.X + matrix.M11 * vector.Y + matrix.M12 * vector.Z;
		result.Z = matrix.M20 * vector.X + matrix.M21 * vector.Y + matrix.M22 * vector.Z;
		return result;
	}

	/// <summary>
	///       Multiplies the transposed of the given matrix with a column vector.
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <param name="vector">The column vector.</param>
	/// <returns>The resulting column vector.</returns>
	public static Vector3F MultiplyTransposed(Matrix33F matrix, Vector3F vector)
	{
		Vector3F result = default;
		result.X = matrix.M00 * vector.X + matrix.M10 * vector.Y + matrix.M20 * vector.Z;
		result.Y = matrix.M01 * vector.X + matrix.M11 * vector.Y + matrix.M21 * vector.Z;
		result.Z = matrix.M02 * vector.X + matrix.M12 * vector.Y + matrix.M22 * vector.Z;
		return result;
	}

	/// <summary>
	///       Divides a matrix by a scalar.
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <param name="scalar">The scalar.</param>
	/// <returns>The matrix with each element divided by scalar.</returns>
	public static Matrix33F operator /(Matrix33F matrix, float scalar)
	{
		float num = 1f / scalar;
		matrix.M00 *= num;
		matrix.M01 *= num;
		matrix.M02 *= num;
		matrix.M10 *= num;
		matrix.M11 *= num;
		matrix.M12 *= num;
		matrix.M20 *= num;
		matrix.M21 *= num;
		matrix.M22 *= num;
		return matrix;
	}

	/// <summary>
	///       Divides a matrix by a scalar.
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <param name="scalar">The scalar.</param>
	/// <returns>The matrix with each element divided by scalar.</returns>
	public static Matrix33F Divide(Matrix33F matrix, float scalar)
	{
		float num = 1f / scalar;
		matrix.M00 *= num;
		matrix.M01 *= num;
		matrix.M02 *= num;
		matrix.M10 *= num;
		matrix.M11 *= num;
		matrix.M12 *= num;
		matrix.M20 *= num;
		matrix.M21 *= num;
		matrix.M22 *= num;
		return matrix;
	}

	/// <summary>
	///       Tests if two matrices are equal.
	///       </summary>
	/// <param name="matrix1">The first matrix.</param>
	/// <param name="matrix2">The second matrix.</param>
	/// <returns>
	///   <see langword="true" /> if the matrices are equal; otherwise <see langword="false" />.
	///       </returns>
	public static bool operator ==(Matrix33F matrix1, Matrix33F matrix2)
	{
		if (matrix1.M00 == matrix2.M00 && matrix1.M01 == matrix2.M01 && matrix1.M02 == matrix2.M02 && matrix1.M10 == matrix2.M10 && matrix1.M11 == matrix2.M11 && matrix1.M12 == matrix2.M12 && matrix1.M20 == matrix2.M20 && matrix1.M21 == matrix2.M21)
		{
			return matrix1.M22 == matrix2.M22;
		}
		return false;
	}

	/// <summary>
	///       Tests if two matrices are not equal.
	///       </summary>
	/// <param name="matrix1">The first matrix.</param>
	/// <param name="matrix2">The second matrix.</param>
	/// <returns>
	///   <see langword="true" /> if the matrices are different; otherwise <see langword="false" />.
	///       </returns>
	public static bool operator !=(Matrix33F matrix1, Matrix33F matrix2)
	{
		if (matrix1.M00 == matrix2.M00 && matrix1.M01 == matrix2.M01 && matrix1.M02 == matrix2.M02 && matrix1.M10 == matrix2.M10 && matrix1.M11 == matrix2.M11 && matrix1.M12 == matrix2.M12 && matrix1.M20 == matrix2.M20 && matrix1.M21 == matrix2.M21)
		{
			return matrix1.M22 != matrix2.M22;
		}
		return true;
	}

	/// <summary>
	///       Performs an explicit conversion from <see cref="T:DigitalRune.Mathematics.Algebra.Matrix33F" /> 
	///       to a 2-dimensional float array.
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <returns>The result of the conversion.</returns>
	public static explicit operator float[,](Matrix33F matrix)
	{
		return new float[3, 3]
		{
			{ matrix.M00, matrix.M01, matrix.M02 },
			{ matrix.M10, matrix.M11, matrix.M12 },
			{ matrix.M20, matrix.M21, matrix.M22 }
		};
	}

	/// <summary>
	///       Converts this <see cref="T:DigitalRune.Mathematics.Algebra.Matrix33F" /> to a 2-dimensional float array.
	///       </summary>
	/// <returns>The result of the conversion.</returns>
	public float[,] ToArray2D()
	{
		return (float[,])this;
	}

	/// <summary>
	///       Performs an explicit conversion from <see cref="T:DigitalRune.Mathematics.Algebra.Matrix33F" /> to 
	///       a jagged float array.
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <returns>The result of the conversion.</returns>
	public static explicit operator float[][](Matrix33F matrix)
	{
		float[][] array = new float[3][]
		{
			new float[3],
			new float[3],
			new float[3]
		};
		array[0][0] = matrix.M00;
		array[0][1] = matrix.M01;
		array[0][2] = matrix.M02;
		array[1][0] = matrix.M10;
		array[1][1] = matrix.M11;
		array[1][2] = matrix.M12;
		array[2][0] = matrix.M20;
		array[2][1] = matrix.M21;
		array[2][2] = matrix.M22;
		return array;
	}

	/// <summary>
	///       Converts this <see cref="T:DigitalRune.Mathematics.Algebra.Matrix33F" /> to a jagged float array.
	///       </summary>
	/// <returns>The result of the conversion.</returns>
	public float[][] ToArrayJagged()
	{
		return (float[][])this;
	}

	/// <summary>
	///       Performs an implicit conversion from <see cref="T:DigitalRune.Mathematics.Algebra.Matrix33F" /> to <see cref="T:DigitalRune.Mathematics.Algebra.MatrixF" />.
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <returns>The result of the conversion.</returns>
	public static implicit operator MatrixF(Matrix33F matrix)
	{
		MatrixF matrixF = new MatrixF(3, 3);
		matrixF[0, 0] = matrix.M00;
		matrixF[0, 1] = matrix.M01;
		matrixF[0, 2] = matrix.M02;
		matrixF[1, 0] = matrix.M10;
		matrixF[1, 1] = matrix.M11;
		matrixF[1, 2] = matrix.M12;
		matrixF[2, 0] = matrix.M20;
		matrixF[2, 1] = matrix.M21;
		matrixF[2, 2] = matrix.M22;
		return matrixF;
	}

	/// <summary>
	///       Converts this <see cref="T:DigitalRune.Mathematics.Algebra.Matrix33F" /> to <see cref="T:DigitalRune.Mathematics.Algebra.MatrixF" />.
	///       </summary>
	/// <returns>The result of the conversion.</returns>
	public MatrixF ToMatrixF()
	{
		return this;
	}

	/// <summary>
	///       Performs an implicit conversion from <see cref="T:DigitalRune.Mathematics.Algebra.Matrix33F" /> to <see cref="T:DigitalRune.Mathematics.Algebra.Matrix33D" />.
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <returns>The result of the conversion.</returns>
	public static implicit operator Matrix33D(Matrix33F matrix)
	{
		return new Matrix33D(matrix.M00, matrix.M01, matrix.M02, matrix.M10, matrix.M11, matrix.M12, matrix.M20, matrix.M21, matrix.M22);
	}

	/// <summary>
	///       Converts this <see cref="T:DigitalRune.Mathematics.Algebra.Matrix33F" /> to <see cref="T:DigitalRune.Mathematics.Algebra.Matrix33D" />.
	///       </summary>
	/// <returns>The result of the conversion.</returns>
	public Matrix33D ToMatrix33D()
	{
		return this;
	}

	/// <summary>
	///       Sets each matrix element to its absolute value.
	///       </summary>
	public void Absolute()
	{
		M00 = Math.Abs(M00);
		M01 = Math.Abs(M01);
		M02 = Math.Abs(M02);
		M10 = Math.Abs(M10);
		M11 = Math.Abs(M11);
		M12 = Math.Abs(M12);
		M20 = Math.Abs(M20);
		M21 = Math.Abs(M21);
		M22 = Math.Abs(M22);
	}

	/// <summary>
	///       Clamps near-zero matrix elements to zero.
	///       </summary>
	public void ClampToZero()
	{
		M00 = Numeric.ClampToZero(M00);
		M01 = Numeric.ClampToZero(M01);
		M02 = Numeric.ClampToZero(M02);
		M10 = Numeric.ClampToZero(M10);
		M11 = Numeric.ClampToZero(M11);
		M12 = Numeric.ClampToZero(M12);
		M20 = Numeric.ClampToZero(M20);
		M21 = Numeric.ClampToZero(M21);
		M22 = Numeric.ClampToZero(M22);
	}

	/// <summary>
	///       Clamps near-zero matrix elements to zero.
	///       </summary>
	/// <param name="epsilon">The tolerance value.</param>
	public void ClampToZero(float epsilon)
	{
		M00 = Numeric.ClampToZero(M00, epsilon);
		M01 = Numeric.ClampToZero(M01, epsilon);
		M02 = Numeric.ClampToZero(M02, epsilon);
		M10 = Numeric.ClampToZero(M10, epsilon);
		M11 = Numeric.ClampToZero(M11, epsilon);
		M12 = Numeric.ClampToZero(M12, epsilon);
		M20 = Numeric.ClampToZero(M20, epsilon);
		M21 = Numeric.ClampToZero(M21, epsilon);
		M22 = Numeric.ClampToZero(M22, epsilon);
	}

	/// <summary>
	///       Gets a column as <see cref="T:DigitalRune.Mathematics.Algebra.Vector3F" />.
	///       </summary>
	/// <param name="index">The index of the column (0, 1, or 2).</param>
	/// <returns>The column vector</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///       The <paramref name="index" /> is out of range.
	///       </exception>
	public Vector3F GetColumn(int index)
	{
		Vector3F result = default;
		switch (index)
		{
		case 0:
			result.X = M00;
			result.Y = M10;
			result.Z = M20;
			break;
		case 1:
			result.X = M01;
			result.Y = M11;
			result.Z = M21;
			break;
		case 2:
			result.X = M02;
			result.Y = M12;
			result.Z = M22;
			break;
		default:
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(622), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6942));
		}
		return result;
	}

	/// <summary>
	///       Sets a column from a <see cref="T:DigitalRune.Mathematics.Algebra.Vector3F" />.
	///       </summary>
	/// <param name="index">The index of the column (0, 1, or 2).</param>
	/// <param name="columnVector">The column vector.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///       The <paramref name="index" /> is out of range.
	///       </exception>
	public void SetColumn(int index, Vector3F columnVector)
	{
		switch (index)
		{
		case 0:
			M00 = columnVector.X;
			M10 = columnVector.Y;
			M20 = columnVector.Z;
			break;
		case 1:
			M01 = columnVector.X;
			M11 = columnVector.Y;
			M21 = columnVector.Z;
			break;
		case 2:
			M02 = columnVector.X;
			M12 = columnVector.Y;
			M22 = columnVector.Z;
			break;
		default:
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(622), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(6942));
		}
	}

	/// <summary>
	///       Gets a row as <see cref="T:DigitalRune.Mathematics.Algebra.Vector3F" />.
	///       </summary>
	/// <param name="index">The index of the row (0, 1, or 2).</param>
	/// <returns>The row vector</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///       The <paramref name="index" /> is out of range.
	///       </exception>
	public Vector3F GetRow(int index)
	{
		Vector3F result = default;
		switch (index)
		{
		case 0:
			result.X = M00;
			result.Y = M01;
			result.Z = M02;
			break;
		case 1:
			result.X = M10;
			result.Y = M11;
			result.Z = M12;
			break;
		case 2:
			result.X = M20;
			result.Y = M21;
			result.Z = M22;
			break;
		default:
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(622), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7072));
		}
		return result;
	}

	/// <summary>
	///       Sets a row from a <see cref="T:DigitalRune.Mathematics.Algebra.Vector3F" />.
	///       </summary>
	/// <param name="index">The index of the row (0, 1, or 2).</param>
	/// <param name="rowVector">The row vector.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///       The <paramref name="index" /> is out of range.
	///       </exception>
	public void SetRow(int index, Vector3F rowVector)
	{
		switch (index)
		{
		case 0:
			M00 = rowVector.X;
			M01 = rowVector.Y;
			M02 = rowVector.Z;
			break;
		case 1:
			M10 = rowVector.X;
			M11 = rowVector.Y;
			M12 = rowVector.Z;
			break;
		case 2:
			M20 = rowVector.X;
			M21 = rowVector.Y;
			M22 = rowVector.Z;
			break;
		default:
			throw new ArgumentOutOfRangeException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(622), c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(7072));
		}
	}

	/// <summary>
	///       Inverts the matrix.
	///       </summary>
	/// <exception cref="T:DigitalRune.Mathematics.MathematicsException">
	///       The matrix is singular (i.e. it is not invertible).
	///       </exception>
	public void Invert()
	{
		if (!TryInvert())
		{
			throw new MathematicsException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5262));
		}
	}

	/// <summary>
	///       Re-orthogonalizes this instance.
	///       </summary>
	public void Orthogonalize()
	{
		Vector3F target = new Vector3F(M00, M10, M20);
		Vector3F vector3F = new Vector3F(M01, M11, M21);
		Vector3F vector3F2 = new Vector3F(M02, M12, M22);
		vector3F -= Vector3F.ProjectTo(vector3F, target);
		vector3F2 = vector3F2 - Vector3F.ProjectTo(vector3F2, target) - Vector3F.ProjectTo(vector3F2, vector3F);
		target.TryNormalize();
		vector3F.TryNormalize();
		vector3F2.TryNormalize();
		M00 = target.X;
		M01 = vector3F.X;
		M02 = vector3F2.X;
		M10 = target.Y;
		M11 = vector3F.Y;
		M12 = vector3F2.Y;
		M20 = target.Z;
		M21 = vector3F.Z;
		M22 = vector3F2.Z;
	}

	/// <summary>
	///       Converts this matrix to an array of float values.
	///       </summary>
	/// <param name="order">The order of the matrix elements in the array.</param>
	/// <returns>The result of the conversion.</returns>
	public float[] ToArray1D(MatrixOrder order)
	{
		float[] array = new float[9];
		if (order == MatrixOrder.ColumnMajor)
		{
			array[0] = M00;
			array[1] = M10;
			array[2] = M20;
			array[3] = M01;
			array[4] = M11;
			array[5] = M21;
			array[6] = M02;
			array[7] = M12;
			array[8] = M22;
		}
		else
		{
			array[0] = M00;
			array[1] = M01;
			array[2] = M02;
			array[3] = M10;
			array[4] = M11;
			array[5] = M12;
			array[6] = M20;
			array[7] = M21;
			array[8] = M22;
		}
		return array;
	}

	/// <summary>
	///       Converts this matrix to a list of float values.
	///       </summary>
	/// <param name="order">The order of the matrix elements in the list.</param>
	/// <returns>The result of the conversion.</returns>
	public IList<float> ToList(MatrixOrder order)
	{
		List<float> list = new List<float>(9);
		if (order == MatrixOrder.ColumnMajor)
		{
			list.Add(M00);
			list.Add(M10);
			list.Add(M20);
			list.Add(M01);
			list.Add(M11);
			list.Add(M21);
			list.Add(M02);
			list.Add(M12);
			list.Add(M22);
		}
		else
		{
			list.Add(M00);
			list.Add(M01);
			list.Add(M02);
			list.Add(M10);
			list.Add(M11);
			list.Add(M12);
			list.Add(M20);
			list.Add(M21);
			list.Add(M22);
		}
		return list;
	}

	/// <summary>
	///       Inverts the matrix if it is invertible.
	///       </summary>
	/// <returns>
	///   <see langword="true" /> if the matrix is invertible; otherwise <see langword="false" />.
	///       </returns>
	public bool TryInvert()
	{
		float num = M11 * M22 - M12 * M21;
		float num2 = M10 * M22 - M12 * M20;
		float num3 = M10 * M21 - M20 * M11;
		float num4 = M00 * num - M01 * num2 + M02 * num3;
		if (Numeric.IsZero(num4, Numeric.EpsilonFSquared * Numeric.EpsilonF))
		{
			return false;
		}
		Matrix33F matrix33F = default;
		matrix33F.M00 = num;
		matrix33F.M01 = 0f - (M01 * M22 - M21 * M02);
		matrix33F.M02 = M01 * M12 - M02 * M11;
		matrix33F.M10 = 0f - num2;
		matrix33F.M11 = M00 * M22 - M20 * M02;
		matrix33F.M12 = 0f - (M00 * M12 - M02 * M10);
		matrix33F.M20 = num3;
		matrix33F.M21 = 0f - (M00 * M21 - M01 * M20);
		matrix33F.M22 = M00 * M11 - M01 * M10;
		float num5 = 1f / num4;
		M00 = matrix33F.M00 * num5;
		M10 = matrix33F.M10 * num5;
		M20 = matrix33F.M20 * num5;
		M01 = matrix33F.M01 * num5;
		M11 = matrix33F.M11 * num5;
		M21 = matrix33F.M21 * num5;
		M02 = matrix33F.M02 * num5;
		M12 = matrix33F.M12 * num5;
		M22 = matrix33F.M22 * num5;
		return true;
	}

	/// <summary>
	///       Transposes this matrix.
	///       </summary>
	public void Transpose()
	{
		MathHelper.Swap(ref M01, ref M10);
		MathHelper.Swap(ref M02, ref M20);
		MathHelper.Swap(ref M12, ref M21);
	}

	/// <summary>
	///       Returns a matrix with the absolute values of the elements of the given matrix.
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <returns>A matrix with the absolute values of the elements of the given matrix.</returns>
	public static Matrix33F Absolute(Matrix33F matrix)
	{
		return new Matrix33F(Math.Abs(matrix.M00), Math.Abs(matrix.M01), Math.Abs(matrix.M02), Math.Abs(matrix.M10), Math.Abs(matrix.M11), Math.Abs(matrix.M12), Math.Abs(matrix.M20), Math.Abs(matrix.M21), Math.Abs(matrix.M22));
	}

	/// <summary>
	///       Determines whether two matrices are equal (regarding the 
	///       tolerance <see cref="P:DigitalRune.Mathematics.Numeric.EpsilonF" />).
	///       </summary>
	/// <param name="matrix1">The first matrix.</param>
	/// <param name="matrix2">The second matrix.</param>
	/// <returns>
	///   <see langword="true" /> if the matrices are equal (within the tolerance 
	///       <see cref="P:DigitalRune.Mathematics.Numeric.EpsilonF" />); otherwise, <see langword="false" />.
	///       </returns>
	public static bool AreNumericallyEqual(Matrix33F matrix1, Matrix33F matrix2)
	{
		if (Numeric.AreEqual(matrix1.M00, matrix2.M00) && Numeric.AreEqual(matrix1.M01, matrix2.M01) && Numeric.AreEqual(matrix1.M02, matrix2.M02) && Numeric.AreEqual(matrix1.M10, matrix2.M10) && Numeric.AreEqual(matrix1.M11, matrix2.M11) && Numeric.AreEqual(matrix1.M12, matrix2.M12) && Numeric.AreEqual(matrix1.M20, matrix2.M20) && Numeric.AreEqual(matrix1.M21, matrix2.M21))
		{
			return Numeric.AreEqual(matrix1.M22, matrix2.M22);
		}
		return false;
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
	public static bool AreNumericallyEqual(Matrix33F matrix1, Matrix33F matrix2, float epsilon)
	{
		if (Numeric.AreEqual(matrix1.M00, matrix2.M00, epsilon) && Numeric.AreEqual(matrix1.M01, matrix2.M01, epsilon) && Numeric.AreEqual(matrix1.M02, matrix2.M02, epsilon) && Numeric.AreEqual(matrix1.M10, matrix2.M10, epsilon) && Numeric.AreEqual(matrix1.M11, matrix2.M11, epsilon) && Numeric.AreEqual(matrix1.M12, matrix2.M12, epsilon) && Numeric.AreEqual(matrix1.M20, matrix2.M20, epsilon) && Numeric.AreEqual(matrix1.M21, matrix2.M21, epsilon))
		{
			return Numeric.AreEqual(matrix1.M22, matrix2.M22, epsilon);
		}
		return false;
	}

	/// <summary>
	///       Returns a matrix with the matrix elements clamped to the range [min, max].
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <returns>
	///       The matrix with small elements clamped to zero.
	///       </returns>
	public static Matrix33F ClampToZero(Matrix33F matrix)
	{
		matrix.M00 = Numeric.ClampToZero(matrix.M00);
		matrix.M01 = Numeric.ClampToZero(matrix.M01);
		matrix.M02 = Numeric.ClampToZero(matrix.M02);
		matrix.M10 = Numeric.ClampToZero(matrix.M10);
		matrix.M11 = Numeric.ClampToZero(matrix.M11);
		matrix.M12 = Numeric.ClampToZero(matrix.M12);
		matrix.M20 = Numeric.ClampToZero(matrix.M20);
		matrix.M21 = Numeric.ClampToZero(matrix.M21);
		matrix.M22 = Numeric.ClampToZero(matrix.M22);
		return matrix;
	}

	/// <summary>
	///       Returns a matrix with the matrix elements clamped to the range [min, max].
	///       </summary>
	/// <param name="matrix">The matrix.</param>
	/// <param name="epsilon">The tolerance value.</param>
	/// <returns>
	///       The matrix with small elements clamped to zero.
	///       </returns>
	public static Matrix33F ClampToZero(Matrix33F matrix, float epsilon)
	{
		matrix.M00 = Numeric.ClampToZero(matrix.M00, epsilon);
		matrix.M01 = Numeric.ClampToZero(matrix.M01, epsilon);
		matrix.M02 = Numeric.ClampToZero(matrix.M02, epsilon);
		matrix.M10 = Numeric.ClampToZero(matrix.M10, epsilon);
		matrix.M11 = Numeric.ClampToZero(matrix.M11, epsilon);
		matrix.M12 = Numeric.ClampToZero(matrix.M12, epsilon);
		matrix.M20 = Numeric.ClampToZero(matrix.M20, epsilon);
		matrix.M21 = Numeric.ClampToZero(matrix.M21, epsilon);
		matrix.M22 = Numeric.ClampToZero(matrix.M22, epsilon);
		return matrix;
	}

	/// <summary>
	///       Creates a uniform scaling matrix.
	///       </summary>
	/// <param name="scale">
	///       The uniform scale factor that is applied to the x-, y-, and z-axis.
	///       </param>
	/// <returns>The created scaling matrix.</returns>
	public static Matrix33F CreateScale(float scale)
	{
		return new Matrix33F
		{
			M00 = scale,
			M11 = scale,
			M22 = scale
		};
	}

	/// <summary>
	///       Creates a scaling matrix.
	///       </summary>
	/// <param name="scaleX">The value to scale by on the x-axis.</param>
	/// <param name="scaleY">The value to scale by on the y-axis.</param>
	/// <param name="scaleZ">The value to scale by on the z-axis.</param>
	/// <returns>The created scaling matrix.</returns>
	public static Matrix33F CreateScale(float scaleX, float scaleY, float scaleZ)
	{
		return new Matrix33F
		{
			M00 = scaleX,
			M11 = scaleY,
			M22 = scaleZ
		};
	}

	/// <summary>
	///       Creates a scaling matrix.
	///       </summary>
	/// <param name="scale">Amounts to scale by the x, y, and z-axis.</param>
	/// <returns>The created scaling matrix.</returns>
	public static Matrix33F CreateScale(Vector3F scale)
	{
		return new Matrix33F
		{
			M00 = scale.X,
			M11 = scale.Y,
			M22 = scale.Z
		};
	}

	/// <summary>
	///       Creates a rotation matrix from axis and angle.
	///       </summary>
	/// <param name="axis">The rotation axis. (Does not need to be normalized.)</param>
	/// <param name="angle">The rotation angle in radians.</param>
	/// <returns>The created rotation matrix.</returns>
	/// <exception cref="T:System.ArgumentException">The axis vector has 0 length.</exception>
	public static Matrix33F CreateRotation(Vector3F axis, float angle)
	{
		try
		{
			axis.Normalize();
		}
		catch (DivideByZeroException)
		{
			throw new ArgumentException(c954b1274c7b3adca7c38979452b2881a.c9172a3c82181eb071d606cdbf4b22a9f(5357));
		}
		float x = axis.X;
		float y = axis.Y;
		float z = axis.Z;
		float num = x * x;
		float num2 = y * y;
		float num3 = z * z;
		float num4 = x * y;
		float num5 = x * z;
		float num6 = y * z;
		float num7 = (float)Math.Cos(angle);
		float num8 = (float)Math.Sin(angle);
		float num9 = x * num8;
		float num10 = y * num8;
		float num11 = z * num8;
		float num12 = 1f - num7;
		Matrix33F result = default;
		result.M00 = num + num7 * (1f - num);
		result.M01 = num4 * num12 - num11;
		result.M02 = num5 * num12 + num10;
		result.M10 = num4 * num12 + num11;
		result.M11 = num2 + num7 * (1f - num2);
		result.M12 = num6 * num12 - num9;
		result.M20 = num5 * num12 - num10;
		result.M21 = num6 * num12 + num9;
		result.M22 = num3 + num7 * (1f - num3);
		return result;
	}

	/// <summary>
	///       Creates a rotation matrix from a unit quaternion.
	///       </summary>
	/// <param name="rotation">The rotation described by a unit quaternion.</param>
	/// <returns>The created rotation matrix.</returns>
	public static Matrix33F CreateRotation(QuaternionF rotation)
	{
		return rotation.ToRotationMatrix33();
	}

	/// <summary>
	///       Creates a matrix that specifies a rotation around the x-axis.
	///       </summary>
	/// <param name="angle">The rotation angle in radians.</param>
	/// <returns>The created rotation matrix.</returns>
	public static Matrix33F CreateRotationX(float angle)
	{
		float num = (float)Math.Cos(angle);
		float num2 = (float)Math.Sin(angle);
		return new Matrix33F(1f, 0f, 0f, 0f, num, 0f - num2, 0f, num2, num);
	}

	/// <summary>
	///       Creates a matrix that specifies a rotation around the y-axis.
	///       </summary>
	/// <param name="angle">The rotation angle in radians.</param>
	/// <returns>The created rotation matrix.</returns>
	public static Matrix33F CreateRotationY(float angle)
	{
		float num = (float)Math.Cos(angle);
		float num2 = (float)Math.Sin(angle);
		return new Matrix33F(num, 0f, num2, 0f, 1f, 0f, 0f - num2, 0f, num);
	}

	/// <summary>
	///       Creates a matrix that specifies a rotation around the z-axis.
	///       </summary>
	/// <param name="angle">The rotation angle in radians.</param>
	/// <returns>The created rotation matrix.</returns>
	public static Matrix33F CreateRotationZ(float angle)
	{
		float num = (float)Math.Cos(angle);
		float num2 = (float)Math.Sin(angle);
		return new Matrix33F(num, 0f - num2, 0f, num2, num, 0f, 0f, 0f, 1f);
	}
}
